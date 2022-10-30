using System;
using System.Threading;
using System.Transactions;
using WCFClient.WCFMSMQService;

namespace WCFClient
{
    class Program
    {
        static void Main(string[] args)
        {
            WCFMSMQServiceClient proxy = new WCFMSMQServiceClient("NetMsmqBinding_IWCFMSMQService");
            using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required))
            {
                Console.WriteLine("WCF First Call at:{0}", DateTime.Now);
                proxy.SayHello("World");

                Thread.Sleep(2000);//客户端休眠两秒，继续下一次调用
                Console.WriteLine("WCF Second Call at:{0}", DateTime.Now);
                proxy.SayHello("Learning Hard");

                scope.Complete();
            }       

            Console.Read();
        }
    }
}
