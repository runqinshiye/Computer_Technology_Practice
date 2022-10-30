using System;
using System.ServiceModel;

namespace WCFContractAndService
{
    // 契约实现
    public class WCFMSMQService : IWCFMSMQService
    {
        public WCFMSMQService()
        {
            Console.WriteLine("WCF MSMQ Service instance was created at: {0}", DateTime.Now);
        }

        #region IOrderProcessor Members

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = true)]
        public void SayHello(string message)
        {
            Console.WriteLine("Hello! {0},调用WCF操作的时间为：{1}", message, DateTime.Now);
        }

        #endregion
    }
}
