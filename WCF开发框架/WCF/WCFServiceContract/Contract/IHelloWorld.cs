using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Contract
{
    [ServiceContract(Name = "HellworldService", Namespace = "http://www.Learninghard.com")]
    public interface IHelloWorld
    {
        [OperationContract(Name = "GetHelloWorldWithoutParam")]
        string GetHelloWorld();

        [OperationContract(Name = "GetHelloWorldWithParam")]
        string GetHelloWorld(string name);           
    }
  
}
