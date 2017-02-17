using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Lemo.Admin.Helper;
using Lemo.DAL;

namespace Lemo.Admin.Controls
{
    public partial class Login : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void butLogin_Click(object sender, ImageClickEventArgs e)
        {
            AdminHelper adminHelper = new AdminHelper();
            LimoEntitiesEntityFremwork limoEntities = new LimoEntitiesEntityFremwork();
            string adminUserName = ConfigurationSettings.AppSettings["AdminUserName"]; //Your Api ID
            string adminPassword = ConfigurationSettings.AppSettings["AdminPassword"]; //Your Api Key
            if (cbCorporate.Checked)
            {
                adminHelper.CorporateObject = limoEntities.Corporates.Where(xx => xx.UserName.ToLower() == txtUsername.Text.ToLower() &&
                                                                                                       xx.Password == txtPassword.Text && xx.IsActive.HasValue && xx.IsActive.Value).FirstOrDefault();
                if (adminHelper.CorporateObject != null)
                {
                    lblError.Visible = false;
                    adminHelper.AdminUser = adminHelper.CorporateObject.UserName;
                    adminHelper.IsAdmin = false;
                    Response.Redirect("~/admin/Pages/Despatch.aspx");
                }
                else
                {
                    lblError.Visible = true;
                    adminHelper.AdminUser = null;
                    adminHelper.IsAdmin = false;
                }
            }
            else
            {
                if (txtUsername.Text.ToLower() == adminUserName.ToLower() && txtPassword.Text == adminPassword)
                {
                    lblError.Visible = false;
                    adminHelper.AdminUser = adminUserName;
                    adminHelper.IsAdmin = true;
                    Response.Redirect("~/admin/Pages/ManageCompany.aspx");
                }
                else
                {
                    adminHelper.CompanyObject = limoEntities.Companies.Where(xx => xx.UserName.ToLower() == txtUsername.Text.ToLower() &&
                                                                                                           xx.Password == txtPassword.Text && xx.IsActive.HasValue && xx.IsActive.Value).FirstOrDefault();
                    if (adminHelper.CompanyObject != null)
                    {
                        lblError.Visible = false;
                        adminHelper.AdminUser = adminHelper.CompanyObject.UserName;
                        adminHelper.IsAdmin = false;
                        Response.Redirect("~/admin/Pages/CompanyProfile.aspx");
                    }
                    else
                    {
                        lblError.Visible = true;
                        adminHelper.AdminUser = null;
                        adminHelper.IsAdmin = false;
                    }
                }
            }          
        }

        protected void butSignUp_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Pages/CompanySignup.aspx");
        }
    }
}