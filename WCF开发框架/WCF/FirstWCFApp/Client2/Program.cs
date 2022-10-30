using Client2.HelloWorldService;
using System;

namespace Client2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (HellworldServiceClient proxy = new HellworldServiceClient())
            {
                Console.WriteLine("服务返回的结果是: {0}", proxy.GetHelloWorld());
            }

            Console.Read();
        }
    }
}
