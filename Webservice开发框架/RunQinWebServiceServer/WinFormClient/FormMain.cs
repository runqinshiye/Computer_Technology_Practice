using RunQin.Restful;
using RunQinBusiness.DataInterface;
using RunQinBusiness.Remoting;
using System;
using System.Net;
using System.Windows.Forms;
using System.Xml;

namespace WinFormClient
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ServicePointManager.ServerCertificateValidationCallback += (senderIns, certificate, chain, errors) => true;
            ServicePointManager.Expect100Continue = true;
            //.Net 4.5以上版本可以直接使用
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            //.Net 4.0版本使用下面代码
            //ServicePointManager.SecurityProtocol = (SecurityProtocolType)192 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;
            var clientR = new RestClient("http://localhost:58289/ServiceMain.asmx");
            var request = new RestRequest("hagedu", Method.POST);
            request.Timeout = 10000;
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("para", txtHttpPara.Text.Trim());
            var response = clientR.Execute(request);
            if (response.StatusCode.ToString().Equals("OK"))
            {
                var content = response.Content;
                /*
                 <?xml version="1.0" encoding="UTF-8"?>
                 <string xmlns="http://www.hagedu.com/RunQinWebServiceServer">由牙醫创造，为牙醫服务-admin</string>
                 */
                XmlDocument xmldc = new XmlDocument();
                xmldc.LoadXml(content);
                //注册命名空间
                XmlNamespaceManager xnm = new XmlNamespaceManager(xmldc.NameTable);
                xnm.AddNamespace("x", "http://www.hagedu.com/RunQinWebServiceServer");
                //取值的时候一定要把x加进去
                string value = xmldc.SelectSingleNode("/x:string", xnm).InnerText;
                MessageBox.Show(value, "正确", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(response.Content, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radBtnDB.Checked)
            {
                RemotingClient.RemotingRole = RemotingRole.ClientDirect;
            }
            else
            {
                RemotingClient.RemotingRole = RemotingRole.ClientWeb;
            }
            CentralConnection.SetDatabaseConnectionInfo();
            ZipCode ZipCodeCur = new ZipCode();
            ZipCodeCur.City = "BinZhou";
            ZipCodeCur.State = "PengLi";
            ZipCodeCur.ZipCodeDigits = "960000";
            ZipCodeCur.IsFrequent = false;
            try
            {
                ZipCodes.Insert(ZipCodeCur);
                MessageBox.Show(ZipCodeCur.ZipCodeNum.ToString());
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
