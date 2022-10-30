using System;
using System.ServiceModel;

namespace WCFServiceHostByConsoleApp
{
    // 服务宿主的实现，把WCF服务宿主在控制台程序中
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(WCFService.CompleteInstrumentationService)))
            {
                host.Opened += delegate
                {
                    Console.WriteLine("Service Started");
                };

                // 打开通信通道
                host.Open();
                Console.Read();
            }
          
        }
    }
}
