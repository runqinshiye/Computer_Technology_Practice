using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WCF.Demo.Client
{
    public partial class SearchAndRemove : System.Web.UI.Page
    {
        Service.SearchClient searchClient = new Service.SearchClient();
        Service.RemoveClient removeClient = new Service.RemoveClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetUsers();
            }
        }

        protected void GetUsers()
        {
            this.repUsers.DataSource = searchClient.DoWork();
            this.repUsers.DataBind();
        }

        protected void lbtnRemoveCommand(object sender, CommandEventArgs e)
        {
            int UserID = Convert.ToInt32(e.CommandName);
            removeClient.DoWork(UserID);
            Response.Write("删除成功~");
            GetUsers();
        }
    }
}