using System.ServiceModel;

namespace WCFContractAndService
{
    [ServiceContract]
    public interface IWCFMSMQService
    {
        // 操作契约，必须为单向操作
        [OperationContract(IsOneWay = true)]
        void SayHello(string message);
    }
}
