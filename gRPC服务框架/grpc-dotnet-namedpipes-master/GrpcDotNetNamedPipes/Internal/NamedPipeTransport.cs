/*
 * Copyright 2020 Google LLC
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * https://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;
using Google.Protobuf;
using Google.Protobuf.Collections;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcDotNetNamedPipes.Generated;

namespace GrpcDotNetNamedPipes.Internal
{
    internal class NamedPipeTransport
    {
        private const int PayloadInSeparatePacketThreshold = 15 * 1024; // 15 kiB
        private const int MessageBufferSize = 16 * 1024; // 16 kiB

        private readonly byte[] _messageBuffer = new byte[MessageBufferSize];
        private readonly PipeStream _pipeStream;

        public NamedPipeTransport(PipeStream pipeStream)
        {
            _pipeStream = pipeStream;
        }

        private async Task<MemoryStream> ReadPacketFromPipe()
        {
            var packet = new MemoryStream();
            if (PlatformConfig.SizePrefix)
            {
                await ReadPacketWithSizePrefix(packet);
            }
            else
            {
                await ReadPacketWithMessage(packet);
            }

            packet.Position = 0;
            return packet;
        }

        private async Task ReadPacketWithMessage(MemoryStream packet)
        {
            do
            {
                int readBytes = await _pipeStream.ReadAsync(_messageBuffer, 0, MessageBufferSize).ConfigureAwait(false);
                packet.Write(_messageBuffer, 0, readBytes);
            } while (!_pipeStream.IsMessageComplete);
        }

        private async Task ReadPacketWithSizePrefix(MemoryStream packet)
        {
            int bytesToRead = await ReadSizePrefix();
            do
            {
                var bytesToReadIntoBuffer = Math.Min(bytesToRead, MessageBufferSize);
                int readBytes = await _pipeStream.ReadAsync(_messageBuffer, 0, bytesToReadIntoBuffer)
                    .ConfigureAwait(false);
                if (readBytes == 0)
                {
                    throw new EndOfPipeException();
                }
                packet.Write(_messageBuffer, 0, readBytes);
                bytesToRead -= readBytes;
            } while (bytesToRead > 0);
        }

        private async Task<int> ReadSizePrefix()
        {
            int readBytes = await _pipeStream.ReadAsync(_messageBuffer, 0, 4).ConfigureAwait(false);
            if (readBytes == 0)
            {
                throw new EndOfPipeException();
            }
            if (readBytes != 4)
            {
                throw new InvalidOperationException("Unexpected size prefix");
            }
            return BitConverter.ToInt32(_messageBuffer, 0);
        }

        public async Task Read(TransportMessageHandler messageHandler)
        {
            var packet = await ReadPacketFromPipe().ConfigureAwait(false);
            while (packet.Position < packet.Length)
            {
                var message = new TransportMessage();
                message.MergeDelimitedFrom(packet);
                switch (message.DataCase)
                {
                    case TransportMessage.DataOneofCase.RequestInit:
                        messageHandler.HandleRequestInit(message.RequestInit.MethodFullName,
                            message.RequestInit.Deadline?.ToDateTime());
                        break;
                    case TransportMessage.DataOneofCase.Headers:
                        var headerMetadata = ConstructMetadata(message.Headers.Metadata);
                        messageHandler.HandleHeaders(headerMetadata);
                        break;
                    case TransportMessage.DataOneofCase.PayloadInfo:
                        var payload = new byte[message.PayloadInfo.Size];
                        if (message.PayloadInfo.InSamePacket)
                        {
                            packet.Read(payload, 0, payload.Length);
                        }
                        else
                        {
                            _pipeStream.Read(payload, 0, payload.Length);
                        }

                        messageHandler.HandlePayload(payload);
                        break;
                    case TransportMessage.DataOneofCase.RequestControl:
                        switch (message.RequestControl)
                        {
                            case RequestControl.Cancel:
                                messageHandler.HandleCancel();
                                break;
                            case RequestControl.StreamEnd:
                                messageHandler.HandleStreamEnd();
                                break;
                        }

                        break;
                    case TransportMessage.DataOneofCase.Trailers:
                        var trailerMetadata = ConstructMetadata(message.Trailers.Metadata);
                        var status = new Status((StatusCode) message.Trailers.StatusCode,
                            message.Trailers.StatusDetail);
                        messageHandler.HandleTrailers(trailerMetadata, status);
                        break;
                }
            }
        }

        private static Metadata ConstructMetadata(RepeatedField<MetadataEntry> entries)
        {
            var metadata = new Metadata();
            foreach (var entry in entries)
            {
                switch (entry.ValueCase)
                {
                    case MetadataEntry.ValueOneofCase.ValueString:
                        metadata.Add(new Metadata.Entry(entry.Name, entry.ValueString));
                        break;
                    case MetadataEntry.ValueOneofCase.ValueBytes:
                        metadata.Add(new Metadata.Entry(entry.Name, entry.ValueBytes.ToByteArray()));
                        break;
                }
            }

            return metadata;
        }

        public WriteTransaction Write()
        {
            return new WriteTransaction(_pipeStream);
        }

        internal class WriteTransaction
        {
            private readonly PipeStream _pipeStream;
            private readonly MemoryStream _packetBuffer = new MemoryStream();
            private readonly List<byte[]> _trailingPayloads = new List<byte[]>();

            public WriteTransaction(PipeStream pipeStream)
            {
                _pipeStream = pipeStream;
            }

            private WriteTransaction AddMessage(TransportMessage message)
            {
                message.WriteDelimitedTo(_packetBuffer);
                return this;
            }

            public void Commit()
            {
                lock (_pipeStream)
                {
                    if (_packetBuffer.Length > 0)
                    {
                        if (PlatformConfig.SizePrefix)
                        {
                            _pipeStream.Write(BitConverter.GetBytes(_packetBuffer.Length), 0, 4);
                        }
                        _packetBuffer.WriteTo(_pipeStream);
                    }

                    foreach (var payload in _trailingPayloads)
                    {
                        _pipeStream.Write(payload, 0, payload.Length);
                    }
                }
            }

            public WriteTransaction RequestInit(string methodFullName, DateTime? deadline)
            {
                return AddMessage(new TransportMessage
                {
                    RequestInit = new RequestInit
                    {
                        MethodFullName = methodFullName,
                        Deadline = deadline != null ? Timestamp.FromDateTime(deadline.Value) : null
                    }
                });
            }

            private void ToTransportMetadata(Metadata metadata, RepeatedField<MetadataEntry> transportMetadata)
            {
                foreach (var entry in metadata ?? new Metadata())
                {
                    var transportEntry = new MetadataEntry
                    {
                        Name = entry.Key
                    };
                    if (entry.IsBinary)
                    {
                        transportEntry.ValueBytes = ByteString.CopyFrom(entry.ValueBytes);
                    }
                    else
                    {
                        transportEntry.ValueString = entry.Value;
                    }

                    transportMetadata.Add(transportEntry);
                }
            }

            public WriteTransaction Headers(Metadata headers)
            {
                var transportHeaders = new Headers();
                ToTransportMetadata(headers, transportHeaders.Metadata);
                return AddMessage(new TransportMessage
                {
                    Headers = transportHeaders
                });
            }

            public WriteTransaction Trailers(StatusCode statusCode, string statusDetail, Metadata trailers)
            {
                var transportTrailers = new Trailers
                {
                    StatusCode = (int) statusCode,
                    StatusDetail = statusDetail
                };
                ToTransportMetadata(trailers, transportTrailers.Metadata);
                return AddMessage(new TransportMessage
                {
                    Trailers = transportTrailers
                });
            }

            public WriteTransaction Cancel()
            {
                return AddMessage(new TransportMessage
                {
                    RequestControl = RequestControl.Cancel
                });
            }

            public WriteTransaction RequestStreamEnd()
            {
                return AddMessage(new TransportMessage
                {
                    RequestControl = RequestControl.StreamEnd
                });
            }

            public WriteTransaction Payload(byte[] payload)
            {
                // TODO: Why doesn't this work on Unix?
                if (payload.Length > PayloadInSeparatePacketThreshold &&
                    Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    // For large payloads, writing the payload outside the packet saves extra copying. 
                    AddMessage(new TransportMessage
                    {
                        PayloadInfo = new PayloadInfo
                        {
                            Size = payload.Length,
                            InSamePacket = false
                        }
                    });
                    _trailingPayloads.Add(payload);
                }
                else
                {
                    // For small payloads, including the payload in the packet reduces the number of reads.
                    AddMessage(new TransportMessage
                    {
                        PayloadInfo = new PayloadInfo
                        {
                            Size = payload.Length,
                            InSamePacket = true
                        }
                    });
                    _packetBuffer.Write(payload, 0, payload.Length);
                }

                return this;
            }
        }
    }
}