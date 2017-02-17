using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lemo.Admin.Helper;
using Lemo.DAL;

namespace Lemo.Admin.Pages
{
    public partial class CompanyProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void butSignUp_Click(object sender, ImageClickEventArgs e)
        {
            LimoEntitiesEntityFremwork limoEntities = new LimoEntitiesEntityFremwork();
            AdminHelper adminHelper = new AdminHelper();
            int id = 0;
            int.TryParse(Request.QueryString["id"], out id);
            Company company;
            if (adminHelper.IsAdmin)
            {
                company =
                    limoEntities.Companies.Where(xx => xx.CompanyID == id &&
                                                       xx.Password == txtOldPassword.Text).FirstOrDefault();
            }
            else
            {
                if (adminHelper.CompanyObject != null)
                    id = adminHelper.CompanyObject.CompanyID;
                else
                    id = 0;
                company =
                    limoEntities.Companies.Where(xx => xx.CompanyID == id &&
                                                       xx.Password == txtOldPassword.Text).FirstOrDefault();
            }
            if (company != null)
            {
                company.Password = txtPassword.Text;
                limoEntities.SaveChanges();
                divChangePasswordConfirmation.Visible = true;
                divProblemChangePasswordConfirmation.Visible = false;
            }
            else
            {
                divProblemChangePasswordConfirmation.Visible = true;
                divChangePasswordConfirmation.Visible = false;
            }
        }
    }
}