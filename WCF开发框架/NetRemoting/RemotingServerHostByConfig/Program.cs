using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;

namespace RemotingServerHostByConfig
{
    class Program
    {
        static void Main(string[] args)
        {
            RemotingConfiguration.Configure("RemotingServerHostByConfig.exe.config", false);

            foreach (var channel in ChannelServices.RegisteredChannels)
            {
                // 打印通道的名称
                Console.WriteLine("The name of the Channel is {0}", channel.ChannelName);
                // 打印通道的优先级
                Console.WriteLine("The priority of the Channel is {0}", channel.ChannelPriority);
            }
            Console.WriteLine("按任意键退出……");
            Console.ReadLine();
        }
    }
}
