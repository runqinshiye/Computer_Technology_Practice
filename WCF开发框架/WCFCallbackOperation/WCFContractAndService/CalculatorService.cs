using System.ServiceModel;

namespace WCFContractAndService
{
    // 服务契约的实现
    public class CalculatorService : ICalculator
    {
        #region ICalculator Members
        public void Multiple(double a, double b)
        {
            double result = a * b;
            // 通过客户端实例通道
            ICallback callback = OperationContext.Current.GetCallbackChannel<ICallback>();

            // 对客户端操作进行回调
            callback.DisplayResult(a, b, result);
        }
        #endregion
    }
}
