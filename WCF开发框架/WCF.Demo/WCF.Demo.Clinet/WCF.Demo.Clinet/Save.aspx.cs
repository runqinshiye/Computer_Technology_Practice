using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WCF.Demo.Client
{
    public partial class Save : System.Web.UI.Page
    {
        Service.SaveClient saveClient = new Service.SaveClient();
        Service.GetClient getClient = new Service.GetClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack && !string.IsNullOrEmpty(Request.QueryString["UserID"]))
            {
                GetUser();
            }
        }

        protected void GetUser()
        {
            int UserID = Convert.ToInt32(Request.QueryString["UserID"]);
            Model.User user = getClient.DoWork(UserID);
            this.txtUserName.Text = user.UserName;
            this.txtDiscribe.Text = user.Discribe;
        }

        //提交
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int UserID = Convert.ToInt32(Request.QueryString["UserID"]);
            Model.User user = getClient.DoWork(UserID);
            user.UserName = this.txtUserName.Text;
            user.Discribe = this.txtDiscribe.Text;
            saveClient.DoWork(user);
            Response.Write("修改成功！");
        }
    }
}