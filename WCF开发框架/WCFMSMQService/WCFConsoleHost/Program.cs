using System;
using System.Messaging;
using System.ServiceModel;
using WCFContractAndService;

namespace WCFConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @".\private$\LearningHardWCFMSMQ";
            if (!MessageQueue.Exists(path))
            {
                MessageQueue.Create(path, true);
            }

            using (ServiceHost host = new ServiceHost(typeof(WCFMSMQService)))
            {
                host.Opened += delegate
                {
                    Console.WriteLine("Service has begun to listen\n\n");
                };

                host.Open();

                Console.Read();
            }
        }
    }
}
