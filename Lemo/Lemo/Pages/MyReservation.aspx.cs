using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lemo.Classes;

namespace Lemo.Pages
{
    public partial class MyReservation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JobWrapper jobWrapper = new JobWrapper();
            if (jobWrapper.UserNumber == 0)
                Response.Redirect("~/Pages/Login.aspx");
            //else
            //{
        }
    }
}