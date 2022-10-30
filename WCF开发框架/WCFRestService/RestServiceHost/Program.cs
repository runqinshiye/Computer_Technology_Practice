using System;
using System.ServiceModel.Web;
using WCFContractAndService;

namespace RestServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            // Rest服务使用WebServiceHost类来为服务提供宿主
            using (WebServiceHost webHost = new WebServiceHost(typeof(EmployeesService)))
            {
                webHost.Opened += delegate
                {
                    Console.WriteLine("Rest Employee Service 开启成功!");
                };

                webHost.Open();
                Console.Read();
            }
        }
    }
}
