using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using BusinessEntity;

namespace WCFServiceAndHost
{
    // 服务契约
    [ServiceContract]
    //[ServiceKnownType(typeof(Order))] // 这是为了演示WCF已知类型
    public interface IUserValidationService
    {
        [OperationContract]
        bool AddNewUser(User user);

        [OperationContract]
        User GetUserByName(string name);
        
        // 为了演示已知类型的操作方法
        //[OperationContract]
        //[ServiceKnownType(typeof(Order))]
        bool AddOrder(OrderBase order);
    }
}

   
