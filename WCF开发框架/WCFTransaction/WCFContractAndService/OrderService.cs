using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ServiceModel;
using System.Transactions;

namespace WCFContractAndService
{
    [ServiceBehavior(
        TransactionIsolationLevel = IsolationLevel.Serializable,
        TransactionTimeout= "00:00:30",
        InstanceContextMode = InstanceContextMode.PerSession,
        TransactionAutoCompleteOnSessionClose = true)]
    public class OrderService :IOrderService
    {
        private List<Customer> customers = null;
        private List<Product> products = null;
        private int orderId = 0;
        private string conString = Properties.Settings.Default.TransactionsConnectionString;

        public List<Customer> GetCustomers()
        {
           
            customers = new List<Customer>();
            using (var cnn = new SqlConnection(conString))
            {
                using (var cmd = new SqlCommand("SELECT * " + "FROM Customers ORDER BY CustomerId", cnn))
                {
                    cnn.Open();
                    using (SqlDataReader CustomersReader = cmd.ExecuteReader())
                    {
                        while (CustomersReader.Read())
                        {
                            var customer = new Customer();
                            customer.CustomerId = CustomersReader.GetInt32(0);
                            customer.CompanyName = CustomersReader.GetString(1);
                            customer.Balance = CustomersReader.GetDecimal(2);
                            customers.Add(customer);
                        }                  
                    }
                }
            }

            return customers;
        }

        public List<Product> GetProducts()
        {
            products = new List<Product>();
            using (var cnn = new SqlConnection(conString))
            {
                using (var cmd = new SqlCommand(
                  "SELECT * " +
                  "FROM Products ORDER BY ProductId", cnn))
                {
                    cnn.Open();
                    using (SqlDataReader productsReader =
                      cmd.ExecuteReader())
                    {
                        while (productsReader.Read())
                        {
                            var product = new Product();
                            product.ProductId = productsReader.GetInt32(0);
                            product.ProductName = productsReader.GetString(1);
                            product.Price = productsReader.GetDecimal(2);
                            product.OnHand = productsReader.GetInt16(3);
                            products.Add(product);
                        }
                    }
                }
            }
            return products;
        }

        // 设置服务的环境事务
        // 使用Client模式,即使用客户端的事务
        [OperationBehavior(TransactionScopeRequired =true, TransactionAutoComplete = false)]
        public string PlaceOrder(Order order)
        {
            using (var conn = new SqlConnection(conString))
            {
                var cmd = new SqlCommand(
                  "Insert Orders (CustomerId, ProductId, " +
                  "Quantity, Price, Amount) " + "Values( " +
                  "@customerId, @productId, @quantity, " +
                  "@price, @amount)", conn);

                cmd.Parameters.Add(new SqlParameter(
                  "@customerId", order.CustomerId));
                cmd.Parameters.Add(new SqlParameter(
                  "@productid", order.ProductId));
                cmd.Parameters.Add(new SqlParameter(
                  "@price", order.Price));
                cmd.Parameters.Add(new SqlParameter(
                  "@quantity", order.Quantity));
                cmd.Parameters.Add(new SqlParameter(
                  "@amount", order.Amount));

                try
                {
                    conn.Open();
                    if (cmd.ExecuteNonQuery() <= 0)
                    {
                        return "The order was not placed";
                    }

                    cmd = new SqlCommand(
                      "Select Max(OrderId) From Orders " +
                      "Where CustomerId = @customerId", conn);
                    cmd.Parameters.Add(new SqlParameter(
                      "@customerId", order.CustomerId));

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            orderId = Convert.ToInt32(reader[0].ToString());
                        }
                    }
                    return string.Format("Order {0} was placed", orderId);
                }
                catch (Exception ex)
                {
                    throw new FaultException(ex.Message);
                }
            }
        }

        // 使用Client模式,即使用客户端的事务
        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = false)]
        public string AdjustInventory(int productId, int quantity)
        {
            using (var conn = new SqlConnection(conString))
            {
                var cmd = new SqlCommand(
                  "Update Products Set OnHand = " +
                  "OnHand - @quantity " +
                  "Where ProductId = @productId", conn);
                cmd.Parameters.Add(new SqlParameter(
                  "@quantity", quantity));
                cmd.Parameters.Add(new SqlParameter(
                  "@productid", productId));

                try
                {
                    conn.Open();
                    if (cmd.ExecuteNonQuery() <= 0)
                    {
                        return "The inventory was not updated";
                    }
                    else
                    {
                        return "The inventory was updated";
                    }
                }
                catch (Exception ex)
                {
                    throw new FaultException(ex.Message);
                }
            }
        }

        // 使用Client模式,即使用客户端的事务
        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = false)]
        public string AdjustBalance(int customerId, decimal amount)
        {
            using (var conn = new SqlConnection(conString))
            {
                var cmd = new SqlCommand(
                  "Update Customers Set Balance = " +
                  "Balance - @amount " +
                  "Where CustomerId = @customerId", conn);
                cmd.Parameters.Add(new SqlParameter(
                  "@amount", amount));
                cmd.Parameters.Add(new SqlParameter(
                  "@customerId", customerId));

                try
                {
                    conn.Open();
                    if (cmd.ExecuteNonQuery() <= 0)
                    {
                        return "The balance was not updated";
                    }
                    else
                    {
                        return "The balance was updated";
                    }
                }
                catch (Exception ex)
                {
                    throw new FaultException(ex.Message);
                }
            }
        }     
    }
}
