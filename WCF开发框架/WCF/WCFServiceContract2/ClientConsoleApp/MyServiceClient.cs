using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsoleApp
{
    // 自定义服务契约，使其保持与服务端一样的继承结果
    [ServiceContract]
    public interface ISimpleInstrumentation
    {
        [OperationContract]
        string WriteEventLog();
    }

    [ServiceContract]
    public interface ICompleteInstrumentation : ISimpleInstrumentation
    {
        [OperationContract]
        string IncreatePerformanceCounter();
    }
}
