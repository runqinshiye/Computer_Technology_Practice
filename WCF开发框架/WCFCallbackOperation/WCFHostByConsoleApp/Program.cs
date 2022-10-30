using System;
using System.ServiceModel;
using WCFContractAndService;

namespace WCFHostByConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(CalculatorService)))
            {
                host.Opened += delegate
                {
                    Console.WriteLine("Service start now....");
                };

                host.Open();
                Console.Read();
            }
        }
    }
}
