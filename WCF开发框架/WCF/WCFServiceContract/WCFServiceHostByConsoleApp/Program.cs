using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace WCFServiceHostByConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(Services.HelloWorldService)))
            {
                host.Opened += delegate
                {
                    Console.WriteLine("服务已开启，按任意键继续....");
                };

                host.Open();
                Console.ReadLine();
            }
        }
    }
}
