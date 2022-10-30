using System;
using WCFClient.WCFCallbackService;

namespace WCFClient
{
    // 客户端中对回调契约的实现
    public class CallbackWCFService : ICalculatorCallback
    {
        public void DisplayResult(double a, double b, double result)
        {
            Console.WriteLine("{0} * {1} = {2}", a, b, result);
        }
    }
}
