using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lemo.Classes;

namespace Lemo
{
    public partial class Master : System.Web.UI.MasterPage
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            JobWrapper jobWrapper = new JobWrapper();
            if (jobWrapper.UserNumber != 0)
            {
                lbLogin.Text = "Logout";
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