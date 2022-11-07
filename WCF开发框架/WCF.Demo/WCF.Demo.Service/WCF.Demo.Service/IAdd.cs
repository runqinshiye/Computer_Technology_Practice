using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF.Demo.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAdd" in both code and config file together.
    [ServiceContract]
    public interface IAdd
    {
        [OperationContract]
        bool DoWork(Model.User user);
    }
}
