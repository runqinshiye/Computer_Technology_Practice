using RemotingObject;
using System;
using System.Runtime.Remoting;

namespace RemotingClientByConfig
{
    class Program
    {
        static void Main(string[] args)
        {
            //使用HTTP通道得到远程对象
            RemotingConfiguration.Configure("RemotingClientByConfig.exe.config", false);
            MyRemotingObject proxyobj1 = new MyRemotingObject();
            if (proxyobj1 == null)
            {
                Console.WriteLine("连接服务器失败");
            }

            Console.WriteLine("This call object by TcpChannel, 100 + 200 = {0}", proxyobj1.AddForTcpTest(100, 200));
            Console.WriteLine("This call object by HttpChannel, 100 - 200 = {0}", proxyobj1.MinusForHttpTest(100, 200));
            Console.WriteLine("This call object by IpcChannel, 100 * 200 = {0}", proxyobj1.MultipleForIPCTest(100, 200));
            Console.WriteLine("Press any key to exit!");
            Console.ReadLine();
        }
    }
}
