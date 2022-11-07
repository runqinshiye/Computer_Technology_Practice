﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF.Demo.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRemove" in both code and config file together.
    [ServiceContract]
    public interface IRemove
    {
        [OperationContract]
        bool DoWork(int UserID);
    }
}
