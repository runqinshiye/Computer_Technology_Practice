using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace BusinessEntity
{
    public interface  IOrder
    {
        Guid ID { get; set; }

        DateTime Date { get; set; }

        string Customer { get; set; }
    }

    [DataContract]
    //[KnownType(typeof(Order))]
    public abstract class OrderBase : IOrder
    {
        [DataMember]
        public Guid ID { get; set;}

        [DataMember]
        public DateTime Date { get; set; }

        [DataMember]
        public string Customer { get; set; }

        [DataMember]
        public string ShipAddress { get; set; }
    }

    [DataContract]
    public class Order : OrderBase
    {
        [DataMember]
        public double TotalPrice { get; set; }
    }
}
