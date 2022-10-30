using Contract;
using Services;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Hosting
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(HelloWorldService)))
            {
                // 如果采用配置文件的方式，Region中代码就可以注释点
                #region 
                host.AddServiceEndpoint(typeof(IHelloWorld), new WSHttpBinding(), "http://127.0.0.1:8888/HelloWorldService");
                if (host.Description.Behaviors.Find<ServiceMetadataBehavior>() == null)
                {
                    ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                    behavior.HttpGetEnabled = true;
                    behavior.HttpGetUrl = new Uri("http://127.0.0.1:8888/HelloWorldService/metadata");
                    host.Description.Behaviors.Add(behavior);
                }
                #endregion 

                host.Opened += delegate
                {
                    Console.WriteLine("HelloWorldService 已经启动, 按任意键终止服务！");
                };

                host.Open();
                Console.Read();
            }
        }
    }
}
