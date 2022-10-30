using System.ServiceModel;

namespace WCFContractAndService
{
    // 服务契约的定义
    [ServiceContract(SessionMode= SessionMode.Required)] // 显式使服务契约支持Session
    public interface ICalculator
    {
        // IsInitiating:该值指示方法是否实现可在服务器上启动会话（如果存在会话）的操作,默认值是true
        // IsTerminating:获取或设置一个值，该值指示服务操作在发送答复消息（如果存在）后，是否会导致服务器关闭会话，默认值是false
        [OperationContract(IsOneWay = true, IsInitiating =true, IsTerminating=false )]
        void Increase();

        [OperationContract(IsInitiating = true, IsTerminating = false)]
        int GetResult();
    }
}
