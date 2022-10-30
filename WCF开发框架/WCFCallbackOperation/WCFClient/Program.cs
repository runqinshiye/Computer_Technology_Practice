using System;
using System.ServiceModel;
using WCFClient.WCFCallbackService;

namespace WCFClient
{
    // 客户端实现，测试回调操作
    class Program
    {
        static void Main(string[] args)
        {
            InstanceContext instanceContex = new InstanceContext(new CallbackWCFService());
            CalculatorClient proxy = new CalculatorClient(instanceContex);
            proxy.Multiple(2,3);

            Console.Read();
        }
    }
}
