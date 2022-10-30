using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Http;
using System.Runtime.Remoting.Channels.Ipc;
using System.Runtime.Remoting.Channels.Tcp;

namespace RemotingServerHost
{
    // 第二步：创建宿主应用程序
    class Server
    {
        static void Main(string[] args)
        {
            // 1.创建三种通道

            // 创建Tcp通道，端口号9001
            TcpChannel tcpChannel = new TcpChannel(9001);
            
            // 创建Http通道，端口号9002
            HttpChannel httpChannel = new HttpChannel(9002);

            // 创建IPC通道，端口号9003
            IpcChannel ipcChannel = new IpcChannel("IpcTest");

            // 2.注册通道
            ChannelServices.RegisterChannel(tcpChannel, false);
            ChannelServices.RegisterChannel(httpChannel, false);
            ChannelServices.RegisterChannel(ipcChannel, false);

            // 打印通道信息
            // 打印Tcp通道的名称
            Console.WriteLine("The name of the TcpChannel is {0}", tcpChannel.ChannelName);
            // 打印Tcp通道的优先级
            Console.WriteLine("The priority of the TcpChannel is {0}", tcpChannel.ChannelPriority);

            Console.WriteLine("The name of the HttpChannel is {0}", httpChannel.ChannelName);
            Console.WriteLine("The priority of the httpChannel is {0}", httpChannel.ChannelPriority);

            Console.WriteLine("The name of the IpcChannel is {0}", ipcChannel.ChannelName);
            Console.WriteLine("The priority of the IpcChannel is {0}", ipcChannel.ChannelPriority);

            // 3. 注册对象
            // 注册MyRemotingObject到.NET Remoting运行库中
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(RemotingObject.MyRemotingObject), "MyRemotingObject", WellKnownObjectMode.Singleton);
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }
    }
}
