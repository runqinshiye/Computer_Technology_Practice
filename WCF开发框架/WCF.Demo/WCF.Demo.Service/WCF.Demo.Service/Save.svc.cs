using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF.Demo.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Save" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Save.svc or Save.svc.cs at the Solution Explorer and start debugging.
    public class Save : ISave
    {
        public bool DoWork(Model.User user)
        {
            return DAL.User.Save(user);
        }
    }
}
