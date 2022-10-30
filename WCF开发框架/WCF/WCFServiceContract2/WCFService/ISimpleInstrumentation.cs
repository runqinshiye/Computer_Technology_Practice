using System.ServiceModel;

namespace WCFService
{
    // 服务契约
    [ServiceContract]
    public interface ISimpleInstrumentation
    {      
        [OperationContract]
        string WriteEventLog();
    }
}
