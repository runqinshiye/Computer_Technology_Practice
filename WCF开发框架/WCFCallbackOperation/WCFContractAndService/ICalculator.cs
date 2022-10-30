using System.ServiceModel;

namespace WCFContractAndService
{
    // 指定回调契约为ICallback
    [ServiceContract(Namespace="http://cnblog.com/zhili/", CallbackContract=typeof(ICallback))] 
    public interface ICalculator
    {
        [OperationContract(IsOneWay = true)]
        void Multiple(double a, double b);
    }
}
