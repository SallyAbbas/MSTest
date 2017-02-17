using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lemo.DAL;
using Lemo.Admin.Helper;

namespace Lemo.Admin.Controls
{
    public partial class AddContactPerson : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AdminHelper adminHelper = new AdminHelper();
                LimoEntitiesEntityFremwork limoEntities = new LimoEntitiesEntityFremwork();
                 int id = 0;
                int.TryParse(Request.QueryString["id"], out id);
                if (id != 0)
                {
                    ContactPersonID = id;
                        ContactPerson contactTemp =
                            limoEntities.ContactPersons.Where(xx => xx.ContactPersonID == ContactPersonID).ToList().FirstOrDefault();
                        if (contactTemp != null)
                        {
                            txtFirstName.Text = contactTemp.FirstName;
                            txtLastName.Text = contactTemp.LastName;
                            txtEmail.Text = contactTemp.Email;
                            txtMobilePhone.Text = contactTemp.Phone;
                            txtJob.Text = contactTemp.Job;
                        }
                    
                }
            }
        }

        protected void butSignUp_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                AdminHelper adminHelper = new AdminHelper();
                LimoEntitiesEntityFremwork limoEntities = new LimoEntitiesEntityFremwork();
                if (ContactPersonID == 0)
                {
                    ContactPerson contactPerson = new ContactPerson();
                    contactPerson.FirstName = txtFirstName.Text;
                    contactPerson.LastName = txtLastName.Text;
                    contactPerson.Email = txtEmail.Text;
                    contactPerson.Phone = txtMobilePhone.Text;
                    contactPerson.Job = txtJob.Text;
                    contactPerson.CompanyID = adminHelper.CompanyObject.CompanyID;
                    limoEntities.ContactPersons.AddObject(contactPerson);
                    limoEntities.SaveChanges();
                    Response.Redirect("~/admin/Pages/ManageContactPerson.aspx");
                }
                else
                {
                    ContactPerson contactTemp =
                        limoEntities.ContactPersons.Where(xx => xx.ContactPersonID == ContactPersonID).ToList().
                            FirstOrDefault();
                    if (contactTemp != null)
                    {
                        contactTemp.FirstName = txtFirstName.Text;
                        contactTemp.LastName = txtLastName.Text;
                        contactTemp.Email = txtEmail.Text;
                        contactTemp.Phone = txtMobilePhone.Text;
                        contactTemp.Job = txtJob.Text;
                        limoEntities.SaveChanges();
                        Response.Redirect("~/admin/Pages/ManageContactPerson.aspx");
                    }
                }
            }
            catch (Exception)
            {
                lblError.Visible = true;
                lblError.Text = "There are problem with your information.";
            }
        }

        public int ContactPersonID
        {
            get
            {
                if (ViewState["ContactPersonID"] != null)
                    return (int)ViewState["ContactPersonID"];
                else
                    return 0;
            }
            set { ViewState["ContactPersonID"] = value; }
        }
    }
}