using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lemo.Classes;
using Lemo.Admin.Helper;

namespace Lemo
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //imgLogo.Attributes.Add("src", VirtualPathUtility.ToAbsolute("~/Image/logo.png"));
            JobWrapper jobWrapper = new JobWrapper();
            AdminHelper adminHelper = new AdminHelper();
            if (jobWrapper.UserNumber != 0)
            {
                lbLogin.Text = "Logout";
                adminHelper.AdminUser = null;
            }
            else
            {
                lbLogin.Text = "Customer/Affiliate Login";
            }
        }

        protected void lbLogin_Click(object sender, EventArgs e)
        {
            JobWrapper jobWrapper = new JobWrapper();
            if (jobWrapper.UserNumber != 0)
            {
                jobWrapper.UserNumber = 0;
                lbLogin.Text = "Customer/Affiliate Login";
                Response.Redirect("~/Pages/Login.aspx");
            }
            else
            {
                Response.Redirect("~/Pages/Login.aspx");
            }
        }
    }
}
