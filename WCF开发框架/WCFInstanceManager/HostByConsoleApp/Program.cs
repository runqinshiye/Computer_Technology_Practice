using System;
using System.ServiceModel;
using WCFContractAndService;

namespace HostByConsoleApp
{
    // 服务宿主程序
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(CalculatorService)))
            {
                host.Opened += delegate
                {
                    Console.WriteLine("The Calculator Service has been started, begun to listen request..."); 
                };

                host.Open();
                Console.ReadLine();
            }
        }
    }
}
