using Client.HostByWindowsService;
using System;


namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var proxy = new HellworldServiceClient())
            {
                // 通过代理类来调用进行服务方法的访问
                Console.WriteLine("服务返回的结果是: {0}", proxy.GetHelloWorldWithoutParam());
                Console.WriteLine("服务返回的结果是: {0}", proxy.GetHelloWorldWithParam("Learning Hard"));
            }
           
            Console.Read();
        }
        
    }
}
