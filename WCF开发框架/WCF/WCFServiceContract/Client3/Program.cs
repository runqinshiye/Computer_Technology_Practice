using Client3.ServiceReference;
using System;

namespace Client3
{
    class Program
    {
        static void Main(string[] args)
        {
            using (HellworldServiceClient helloWorldProxy = new HellworldServiceClient())
            {
                Console.WriteLine("服务返回的结果是: {0}", helloWorldProxy.GetHelloWorld());
                Console.WriteLine("服务返回的结果是: {0}", helloWorldProxy.GetHelloWorld("Learning Hard"));
            }

            Console.ReadLine();
        }
    }
}
