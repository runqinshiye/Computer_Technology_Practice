using Client.HelloWorldServices;
using System;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            // HelloworldServiceClient就是VS为我们创建的服务代理类
            using (HellworldServiceClient proxy = new HellworldServiceClient())
            {
                // 通过代理类来调用进行服务方法的访问
                Console.WriteLine("服务返回的结果是: {0}", proxy.GetHelloWorld());
            }

            Console.Read();
        }
    }
}
