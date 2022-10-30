using System.ServiceModel;

namespace WCFContractAndService
{
    // 回调契约的定义，此时回调契约不需要应用ServiceContractAttribute特性
    public interface ICallback
    {
        [OperationContract(IsOneWay = true)]
        void DisplayResult(double x, double y, double result);
    }
}
