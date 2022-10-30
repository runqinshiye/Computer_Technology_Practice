using System.ServiceModel;

namespace WCFService
{
    // 服务契约，继承于ISimpleInstrumentation这个服务契约
    [ServiceContract]
    public interface ICompleteInstrumentation :ISimpleInstrumentation
    {
        [OperationContract]
        string IncreatePerformanceCounter();
    }
}
