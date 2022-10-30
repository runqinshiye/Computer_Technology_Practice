using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Contract
{
    [ServiceContract(Name = "HellworldService", Namespace = "http://www.Learninghard.com")]
    public interface IHelloWorld
    {
        [OperationContract()]
        string GetHelloWorld();
    }
}
