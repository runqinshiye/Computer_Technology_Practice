using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace WCFContractAndService
{
    [ServiceContract(SessionMode= SessionMode.Required)]
    //[ServiceBehavior(ReleaseServiceInstanceOnTransactionComplete = false)]
    public interface IOrderService
    {
        // 操作契约
        [OperationContract]
        // 控制客户端的事务是否传播到服务
        // TransactionFlow的值会包含在服务发布的元数据上
        [TransactionFlow(TransactionFlowOption.NotAllowed)]
        List<Customer> GetCustomers();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.NotAllowed)]
        List<Product> GetProducts();

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Mandatory)]
        string PlaceOrder(Order order);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Mandatory)]
        string AdjustInventory(int productId, int quantity);

        [OperationContract]
        [TransactionFlow(TransactionFlowOption.Mandatory)]
        string AdjustBalance(int customerId, decimal amount);
    }

    [DataContract]
    public class Customer
    {
        [DataMember]
        public int CustomerId { get; set; }

        [DataMember]
        public string CompanyName { get; set; }

        [DataMember]
        public decimal Balance { get; set; }
    }

    [DataContract]
    public class Product
    {
        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public int OnHand { get; set; }
    }

    [DataContract]
    public class Order
    {
        [DataMember]
        public int CustomerId { get; set; }

        [DataMember]
        public int ProductId { get; set; }

        [DataMember]
        public decimal Price { get; set; }

        [DataMember]
        public int Quantity { get; set; }

        [DataMember]
        public decimal Amount { get; set; }
    }
}
