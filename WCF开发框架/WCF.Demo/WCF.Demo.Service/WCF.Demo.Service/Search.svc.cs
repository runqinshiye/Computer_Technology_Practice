using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF.Demo.Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Search" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Search.svc or Search.svc.cs at the Solution Explorer and start debugging.
    public class Search : ISearch
    {
        List<Model.User> ISearch.DoWork()
        {
            return DAL.User.GetUsers();
        }
    }
}
