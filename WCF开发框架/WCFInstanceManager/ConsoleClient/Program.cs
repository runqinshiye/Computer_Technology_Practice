using System;
using System.ServiceModel;
using WCFContractAndService;

namespace ConsoleClient
{
    // 客户端程序实现
    class Program
    {
        static void Main(string[] args)
        {
            // Use ChannelFactory<ICalculator> to create WCF Service proxy 
            ChannelFactory<ICalculator> calculatorChannelFactory = new ChannelFactory<ICalculator>("HttpEndPoint");
            Console.WriteLine("Create a calculator proxy :proxy1");
            ICalculator proxy1 = calculatorChannelFactory.CreateChannel();
            Console.WriteLine("Invoke proxy1.Increate() method");
            proxy1.Increase();
            Console.WriteLine("Invoke proxy1.Increate() method again");
            proxy1.Increase();
            Console.WriteLine("The result return via proxy1.GetResult() is: {0}", proxy1.GetResult());

            Console.WriteLine("Create another calculator proxy: proxy2");
            ICalculator proxy2 = calculatorChannelFactory.CreateChannel();
            Console.WriteLine("Invoke proxy2.Increate() method");
            proxy2.Increase();
            Console.WriteLine("Invoke proxy2.Increate() method again");
            proxy2.Increase();
            Console.WriteLine("The result return via proxy2.GetResult() is: {0}", proxy2.GetResult());

            Console.ReadLine();
        }
    }
}
