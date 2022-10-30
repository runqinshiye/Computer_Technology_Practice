using System;

namespace Client2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var proxy = new HellworldServiceClient())
            {
                // 通过代理类来调用进行服务方法的访问
                Console.WriteLine("服务返回的结果是: {0}", proxy.GetHelloWorld());
                Console.WriteLine("服务返回的结果是: {0}", proxy.GetHelloWorld("Learning Hard"));
            }

            Console.Read();
        }
    }
}
