using RemotingObject;
using System;

namespace RemotingClient
{
    class Client
    {
        static void Main(string[] args)
        {
            // 使用Tcp通道得到远程对象
            //TcpChannel tcpChannel = new TcpChannel();
            //ChannelServices.RegisterChannel(tcpChannel, false);
            MyRemotingObject proxyobj1 = Activator.GetObject(typeof(MyRemotingObject), "tcp://localhost:9001/MyRemotingObject") as MyRemotingObject;
            if (proxyobj1 == null)
            {
                Console.WriteLine("连接TCP服务器失败");    
            }

            //HttpChannel httpChannel = new HttpChannel();
            //ChannelServices.RegisterChannel(httpChannel, false);
            MyRemotingObject proxyobj2 = Activator.GetObject(typeof(MyRemotingObject), "http://localhost:9002/MyRemotingObject") as MyRemotingObject;
            if (proxyobj2 == null)
            {
                Console.WriteLine("连接Http服务器失败");
            }

            //IpcChannel ipcChannel = new IpcChannel();
            //ChannelServices.RegisterChannel(ipcChannel, false);
            MyRemotingObject proxyobj3 = Activator.GetObject(typeof(MyRemotingObject), "ipc://IpcTest/MyRemotingObject") as MyRemotingObject;
            if (proxyobj3 == null)
            {
                Console.WriteLine("连接Ipc服务器失败");
            }
            // 输出信息
            Console.WriteLine("This call object by TcpChannel, 100 + 200 = {0}", proxyobj1.AddForTcpTest(100, 200));
            Console.WriteLine("This call object by HttpChannel, 100 - 200 = {0}", proxyobj2.MinusForHttpTest(100, 200));
            Console.WriteLine("This call object by IpcChannel, 100 * 200 = {0}", proxyobj1.MultipleForIPCTest(100, 200));
            Console.WriteLine("Press any key to exit!");
            Console.ReadLine();
        }
    }
}
