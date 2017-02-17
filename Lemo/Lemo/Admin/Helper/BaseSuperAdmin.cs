using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lemo.Admin.Helper
{
    public class BaseSuperAdmin : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            AdminHelper adminHelper = new AdminHelper();
            if (!adminHelper.IsAdmin)
            {
                Session.Clear();
                Response.Redirect("~/Pages/Login.aspx");
            }
        }
    }
}