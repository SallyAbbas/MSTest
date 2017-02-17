using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lemo.Classes;
using Lemo.Admin.Helper;

namespace Lemo.Admin
{
    public partial class AdminMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AdminHelper adminHelper = new AdminHelper();
            JobWrapper jobWrapper = new JobWrapper();
            if (adminHelper.AdminUser != null)
            {
                jobWrapper.UserNumber = 0;
                lbLogin.Text = "Logout";
                if(adminHelper.IsAdmin)
                {
                    hlContactPerson.Visible = false;
                    hlMyCompany.Visible = false;
                    hlServiceArea.Visible = false;
                    hlCars.Visible = false;
                    hlManageDriver.Visible = false;
                    hlManageCompany.Visible = true;
                    hlManageCorporate.Visible = true;
                    hlAddCompany.Visible = true;
                    hlAddCorporate.Visible = true;
                    hlManageSettle.Visible = true;
                    hlReservation.Visible = true;
                }
                else if(adminHelper.CompanyObject != null)
                {
                    adminHelper.CorporateObject = null;
                    hlContactPerson.Visible = true;
                    hlMyCompany.Visible = true;
                    hlServiceArea.Visible = true;
                    hlCars.Visible = true;
                    hlManageDriver.Visible = true;
                    hlManageCompany.Visible = false;
                    hlManageCorporate.Visible = false;
                    hlAddCompany.Visible = false;
                    hlAddCorporate.Visible = false;
                    hlManageSettle.Visible = true;
                    hlReservation.Visible = true;
                    if (adminHelper.CompanyObject != null && !string.IsNullOrEmpty(adminHelper.CompanyObject.ZipCode) &&
                        adminHelper.CompanyObject.IsSupportAllState.HasValue && adminHelper.CompanyObject.IsSupportAllState.Value)
                        hlServiceArea.Enabled = false;
                    else
                        hlServiceArea.Enabled = true;
                }
                else if (adminHelper.CorporateObject != null)
                {
                    adminHelper.CompanyObject = null;
                    hlContactPerson.Visible = false;
                    hlMyCompany.Visible = false;
                    hlServiceArea.Visible = false;
                    hlCars.Visible = false;
                    hlManageDriver.Visible = false;
                    hlManageCompany.Visible = false;
                    hlManageCorporate.Visible = false;
                    hlAddCompany.Visible = false;
                    hlAddCorporate.Visible = false;
                    hlManageSettle.Visible = false;
                    //hlReservation.Visible = false;
                }
            }
            else
            {
                lbLogin.Text = "Login";
                if (!Request.Url.AbsoluteUri.ToLower().Contains("login.aspx".ToLower()))
                    Response.Redirect("~/Pages/Login.aspx");
            }
            if (Request.Url.AbsoluteUri.ToLower().Contains("login.aspx".ToLower()))
            {
                hlContactPerson.Visible = false;
                hlMyCompany.Visible = false;
                hlServiceArea.Visible = false;
                hlCars.Visible = false;
                hlManageCompany.Visible = false;
                hlManageCorporate.Visible = false;
                hlAddCompany.Visible = false;
                hlAddCorporate.Visible = false;
                hlReservation.Visible = false;
                hlDespath.Visible = false;
                hlManageDriver.Visible = false;
                hlManageSettle.Visible = false;
            }
            //hlReservation.Visible = false;
        }

        protected void lbLogin_Click(object sender, EventArgs e)
        {
            AdminHelper adminHelper = new AdminHelper();
            if (adminHelper.AdminUser != null)
            {
                Session.Clear();
                adminHelper.AdminUser = null;
                //Session.Clear();
                lbLogin.Text = "Login";
                Response.Redirect("~/Pages/Login.aspx");
            }
            else
            {
                Response.Redirect("~/Pages/Login.aspx");
            }
        }
    }
}
