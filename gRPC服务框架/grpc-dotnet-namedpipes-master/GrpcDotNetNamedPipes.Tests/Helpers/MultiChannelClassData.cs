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
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GrpcDotNetNamedPipes.Tests.Helpers
{
    public class MultiChannelClassData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {new NamedPipeChannelContextFactory()};
#if NET6_0_OR_GREATER
            if (RuntimeInformation.OSArchitecture == Architecture.Arm64 && !OperatingSystem.IsLinux())
            {
                // No grpc implementation available for comparison
                yield break;
            }
#endif
            yield return new object[] {new HttpChannelContextFactory()};
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}