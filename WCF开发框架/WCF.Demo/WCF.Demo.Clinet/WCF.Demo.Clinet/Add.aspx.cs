using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Service = WCF.Demo.Service;

namespace WCF.Demo.Client
{
    public partial class Add : System.Web.UI.Page
    {
        Service.AddClient addClient = new Service.AddClient();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //提交
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Model.User user = new Model.User();
            user.UserName = this.txtUserName.Text;
            user.Password = this.txtPassword.Text;
            user.Discribe = this.txtDiscribe.Text;
            user.SubmitTime = System.DateTime.Now;
            addClient.DoWork(user);
            Response.Write("添加成功！");
        }
    }
}