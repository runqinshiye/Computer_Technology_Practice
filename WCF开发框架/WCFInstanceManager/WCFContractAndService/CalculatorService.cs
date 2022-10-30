using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFContractAndService
{
    // 契约的实现
    // ServiceBehavior属性只能应用在类上
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)] // 显示指定PerSingle方式
    public class CalculatorService : ICalculator, IDisposable
    {
        private int _nCount = 0;

        public CalculatorService()
        {
            Console.WriteLine("CalulatorService object has been created");
        }

        // 为了看出服务实例的释放情况
        public void Dispose()
        {
            Console.WriteLine("CalulatorService object has been Disposed");
        }

        #region ICalulator Members
        public void Increase()
        {
            // 输出Session ID 
            Console.WriteLine("The Add method is invoked and the current session ID is: {0}", OperationContext.Current.SessionId);
            this._nCount++;
        }

        public int GetResult()
        {
            Console.WriteLine("The GetResult method is invoked and the current session ID is: {0}", OperationContext.Current.SessionId);
            return this._nCount;
        }
        #endregion 
    }
}
