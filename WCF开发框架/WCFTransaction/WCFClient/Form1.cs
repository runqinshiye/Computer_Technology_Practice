using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Transactions;
using System.Windows.Forms;
using WCFClient.OrderService;

namespace WCFClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Customer customer = null;
        private List<Customer> customers = null;
        private Product product = null;
        private List<Product> products = null;
        private OrderServiceClient proxy = null;
        private Order order = null;
        private string result = String.Empty;

        private void Form1_Load(object sender, EventArgs e)
        {
            proxy = new OrderServiceClient("WSHttpBinding_IOrderService");
            GetCustomersAndProducts();
        }

        private void GetCustomersAndProducts()
        {
            customers = proxy.GetCustomers().ToList<Customer>();
            customerBindingSource.DataSource = customers;

            products = proxy.GetProducts().ToList<Product>();
            productBindingSource.DataSource = products;
        }

        private void placeOrderButton_Click(object sender, EventArgs e)
        {
            customer = (Customer)this.customerBindingSource.Current;
            product = (Product)this.productBindingSource.Current;
            Int32 quantity = Convert.ToInt32(quantityTextBox.Text);

            order = new Order();
            order.CustomerId = customer.CustomerId;
            order.ProductId = product.ProductId;
            order.Price = product.Price;
            order.Quantity = quantity;
            order.Amount = order.Price * Convert.ToDecimal(order.Quantity);
            
            // 事务处理 
            using (var tranScope = new TransactionScope())
            {
                proxy = new OrderServiceClient("WSHttpBinding_IOrderService");
                {
                    try
                    {
                        result = proxy.PlaceOrder(order);
                        MessageBox.Show(result);

                        result = proxy.AdjustInventory(product.ProductId, quantity);
                        MessageBox.Show(result);

                        result = proxy.AdjustBalance(customer.CustomerId,
                          Convert.ToDecimal(quantity) * order.Price);
                        MessageBox.Show(result);

                        proxy.Close();
                        tranScope.Complete(); // Cmmmit transaction
                    }
                    catch (FaultException faultEx)
                    {
                        MessageBox.Show(faultEx.Message +
                          "\n\nThe order was not placed");                  
                    }
                    catch (ProtocolException protocolEx)
                    {
                        MessageBox.Show(protocolEx.Message +
                          "\n\nThe order was not placed");
                    }
                }
            }

            // 成功提交后强制刷新界面
            quantityTextBox.Clear();
            try
            {
                proxy = new OrderServiceClient("WSHttpBinding_IOrderService");
                GetCustomersAndProducts();
            }
            catch (FaultException faultEx)
            {
                MessageBox.Show(faultEx.Message);
            }
        }
    }
}
