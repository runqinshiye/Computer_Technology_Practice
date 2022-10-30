using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using ClientConsoleApp.ServiceReference;

namespace ClientConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (SimpleInstrumentationClient proxy1 = new SimpleInstrumentationClient())
            //{
            //    Console.WriteLine(proxy1.WriteEventLog());
            //}
            //using (CompleteInstrumentationClient proxy2 = new CompleteInstrumentationClient())
            //{
            //    Console.WriteLine(proxy2.IncreatePerformanceCounter());
            //}
            using (ChannelFactory<ISimpleInstrumentation> simpleChannelFactory = new ChannelFactory<ISimpleInstrumentation>("ISimpleInstrumentation"))
            {
                ISimpleInstrumentation simpleProxy = simpleChannelFactory.CreateChannel();
                using (simpleProxy as IDisposable)
                {
                    Console.WriteLine(simpleProxy.WriteEventLog());
                }
            }
            using (ChannelFactory<ICompleteInstrumentation> completeChannelFactor = new ChannelFactory<ICompleteInstrumentation>("ICompleteInstrumentation"))
            {
                ICompleteInstrumentation completeProxy = completeChannelFactor.CreateChannel();
                using (completeProxy as IDisposable)
                {
                    Console.WriteLine(completeProxy.IncreatePerformanceCounter());
                }
            }

            //Console.WriteLine();
            //Console.WriteLine("---Use Genergate Client by VS Tool to call method of WCF service---");
            //using (CompleteInstrumentationClient proxy = new CompleteInstrumentationClient())
            //{
            //    Console.WriteLine(proxy.WriteEventLog());
            //    Console.WriteLine(proxy.IncreatePerformanceCounter());
            //}
            
            Console.Read();
        }
    }
}
