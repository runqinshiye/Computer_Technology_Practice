using System;
using WebServiceClient.LHWebService;

namespace WebServiceClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // 实例化一个Soap协议的头
            MySoapHeader mySoapHeader = new MySoapHeader() { Token = "LearningHard"};
            string sResult = string.Empty;
            LearningHardWebServiceSoapClient learningHardWebSer = null;
            try
            {
                // 实例化Web服务的客户端代理类
                learningHardWebSer = new LearningHardWebServiceSoapClient();
                // 调用Web服务上的方法
                sResult= learningHardWebSer.HelloLearningHard(ref mySoapHeader);
                // 输出结果
                Console.WriteLine(sResult);
            }
            catch(Exception ex)
            {
                Console.WriteLine("调用Web服务失败!" + ex.Message);
            }
            finally
            {
                // 释放托管资源
                if (learningHardWebSer != null)
                {
                    learningHardWebSer.Close();
                }
            }

            Console.WriteLine("请按任意键结束...");
            Console.ReadLine();
        }
    }
}
