using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lemo.DAL;
using Lemo.Admin.Helper;
using System.Net.Mail;
using System.Configuration;
using Lemo.Helper;
using System.Text;

namespace Lemo.Admin.Controls
{
    public partial class AddCompany : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (IsCorporate)
                    GetCorporateInformation();
                else
                    GetCompanyInformation();
            }
            if (IsSingnUP && !IsCorporate)
            {
                tdIsActive.Visible = false;
                tdIsAvailable.Visible = false;
                tdIsTop.Visible = false;
                tdIsDespatch.Visible = false;
            }
            else if (IsCorporate)
            {
                tdIsAvailable.Visible = false;
                tdIsTop.Visible = false;
                tdIsDespatch.Visible = false;
            }
        }

        private void GetCorporateInformation()
        {
            AdminHelper adminHelper = new AdminHelper();
            int id = 0;
            int.TryParse(Request.QueryString["id"], out id);
            if (id != 0)
            {
                CompanyID = id;
                LimoEntitiesEntityFremwork imoEntities = new LimoEntitiesEntityFremwork();
                var company = imoEntities.Corporates.Where(xx => xx.ID == id).ToList().FirstOrDefault();
                if (company != null)
                {
                    txtCompanyName.Text = company.Name;
                    txtUserName.Text = company.UserName;
                    cbIsActive.Checked = company.IsActive.HasValue ? company.IsActive.Value : false;               
                    txtEmail.Text = company.Email;
                    txtMobilePhone.Text = company.Phone;                    
                }
            }
        }
        private void GetCompanyInformation()
        {
            AdminHelper adminHelper = new AdminHelper();
            int id = 0;
            int.TryParse(Request.QueryString["id"], out id);
            if (id != 0)
            {
                CompanyID = id;
                LimoEntitiesEntityFremwork imoEntities = new LimoEntitiesEntityFremwork();
                Company company = imoEntities.Companies.Where(xx => xx.CompanyID == id).ToList().FirstOrDefault();
                if (company != null)
                {
                    adminHelper.CompanyObject = company;
                    txtCompanyName.Text = company.CompanyName;
                    txtUserName.Text = company.UserName;
                    cbIsActive.Checked = company.IsActive.HasValue ? company.IsActive.Value : false;
                    cbIsAvailable.Checked = company.IsAvailable.HasValue ? company.IsAvailable.Value : false;
                    cbIsTop.Checked = company.IsTop.HasValue ? company.IsTop.Value : false;
                    txtEmail.Text = company.Email;
                    txtMobilePhone.Text = company.Phone;
                    cbAllowDespatch.Checked = company.IsDespath.HasValue ? company.IsDespath.Value : false;
                }
            }
        }

        protected void butSignUp_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (IsCorporate)
                    ManageCorporate();
                else
                    ManageCompany();              
            }
            catch (Exception)
            {
                lblError.Visible = true;
                lblError.Text = "There are problem with your information.";
            }
        }

        private void ManageCorporate()
        {
            LimoEntitiesEntityFremwork limoEntities = new LimoEntitiesEntityFremwork();
            AdminHelper adminHelper = new AdminHelper();
            if (CompanyID != 0)
            {
                var company =
                limoEntities.Corporates.Where(xx => xx.ID == CompanyID).ToList().FirstOrDefault();
                if (company != null)
                    if (company.Name.ToLower() == txtCompanyName.Text.ToLower() || (company.Name != txtCompanyName.Text &&
                    limoEntities.Companies.Where(xx => xx.CompanyName.ToLower() == txtCompanyName.Text.ToLower()).ToList().Count == 0))
                    {
                        if (company.UserName.ToLower() == txtUserName.Text.ToLower() || (company.UserName != txtUserName.Text &&
                            limoEntities.Companies.Where(xx => xx.UserName.ToLower() == txtUserName.Text.ToLower()).ToList().Count == 0))
                        {
                            company.Name = txtCompanyName.Text;
                            company.UserName = txtUserName.Text;
                            company.Password = txtPassword.Text;
                            company.IsActive = cbIsActive.Checked;                            
                            company.Email = txtEmail.Text;
                            company.Phone = txtMobilePhone.Text;                            
                            limoEntities.SaveChanges();
                            adminHelper.CompanyObject = null;
                            EmailManager.SendEmail("Limoallover Account", CreateUpdateEmailMessageBody(), txtEmail.Text, true);
                            Response.Redirect("~/admin/Pages/ManageCorporate.aspx");
                        }
                        else
                        {
                            lblError.Text = "User Name Already Exist";
                            lblError.Visible = true;
                        }
                    }
                    else
                        lblError.Visible = true;
            }
            else if (limoEntities.Corporates.Where(xx => xx.Name.ToLower() == txtCompanyName.Text.ToLower()).ToList().Count == 0)
            {
                if (limoEntities.Corporates.Where(xx => xx.UserName.ToLower() == txtUserName.Text.ToLower()).ToList().Count == 0)
                {
                    var company = new Corporate();
                    company.Name = txtCompanyName.Text;
                    company.UserName = txtUserName.Text;
                    company.Password = txtPassword.Text;
                    company.IsActive = cbIsActive.Checked;                    
                    company.Email = txtEmail.Text;
                    company.Phone = txtMobilePhone.Text;                   
                    limoEntities.Corporates.AddObject(company);
                    limoEntities.SaveChanges();
                    adminHelper.CompanyObject = null;
                    EmailManager.SendEmail("Limoallover Account", CreateEmailMessageBody(), txtEmail.Text, true);
                    EmailManager.SendEmail("Limoallover Account", CreateAdminEmailMessageBody(), "", true, true);
                    if (!IsSingnUP)
                        Response.Redirect("~/admin/Pages/ManageCorporate.aspx");
                    //else
                    //    Response.Redirect("~/admin/Pages/CompanyProfile.aspx");
                }
                else
                {
                    lblError.Text = "User Name Already Exist";
                    lblError.Visible = true;
                }
            }
            else
                lblError.Visible = true;
        }

        private void ManageCompany()
        {
            LimoEntitiesEntityFremwork limoEntities = new LimoEntitiesEntityFremwork();
            AdminHelper adminHelper = new AdminHelper();
            if (CompanyID != 0 && adminHelper.CompanyObject != null)
            {
                Company company =
                limoEntities.Companies.Where(xx => xx.CompanyID == adminHelper.CompanyObject.CompanyID).ToList().FirstOrDefault();
                if (company != null)
                    if (company.CompanyName.ToLower() == txtCompanyName.Text.ToLower() || (company.CompanyName != txtCompanyName.Text &&
                    limoEntities.Companies.Where(xx => xx.CompanyName.ToLower() == txtCompanyName.Text.ToLower()).ToList().Count == 0))
                    {
                        if (company.UserName.ToLower() == txtUserName.Text.ToLower() || (company.UserName != txtUserName.Text &&
                            limoEntities.Companies.Where(xx => xx.UserName.ToLower() == txtUserName.Text.ToLower()).ToList().Count == 0))
                        {
                            company.CompanyName = txtCompanyName.Text;
                            company.UserName = txtUserName.Text;
                            company.Password = txtPassword.Text;
                            company.IsActive = cbIsActive.Checked;
                            company.IsAvailable = cbIsAvailable.Checked;
                            company.IsTop = cbIsTop.Checked;
                            company.Email = txtEmail.Text;
                            company.Phone = txtMobilePhone.Text;
                            company.IsDespath = cbAllowDespatch.Checked;
                            limoEntities.SaveChanges();
                            adminHelper.CompanyObject = null;
                            EmailManager.SendEmail("Limoallover Account", CreateUpdateEmailMessageBody(), txtEmail.Text, true);
                            Response.Redirect("~/admin/Pages/ManageCompany.aspx");
                        }
                        else
                        {
                            lblError.Text = "User Name Already Exist";
                            lblError.Visible = true;
                        }
                    }
                    else
                        lblError.Visible = true;
            }
            else if (limoEntities.Companies.Where(xx => xx.CompanyName.ToLower() == txtCompanyName.Text.ToLower()).ToList().Count == 0)
            {
                if (limoEntities.Companies.Where(xx => xx.UserName.ToLower() == txtUserName.Text.ToLower()).ToList().Count == 0)
                {
                    Company company = new Company();
                    company.CompanyName = txtCompanyName.Text;
                    company.UserName = txtUserName.Text;
                    company.Password = txtPassword.Text;
                    company.IsActive = cbIsActive.Checked;
                    company.IsAvailable = cbIsAvailable.Checked;
                    company.IsTop = cbIsTop.Checked;
                    company.Email = txtEmail.Text;
                    company.Phone = txtMobilePhone.Text;
                    company.IsDespath = cbAllowDespatch.Checked;
                    limoEntities.Companies.AddObject(company);
                    limoEntities.SaveChanges();
                    adminHelper.CompanyObject = null;
                    EmailManager.SendEmail("Limoallover Account", CreateEmailMessageBody(), txtEmail.Text, true);
                    EmailManager.SendEmail("Limoallover Account", CreateAdminEmailMessageBody(), "", true, true);
                    if (!IsSingnUP)
                        Response.Redirect("~/admin/Pages/ManageCompany.aspx");
                    else
                        Response.Redirect("~/admin/Pages/CompanyProfile.aspx");
                }
                else
                {
                    lblError.Text = "User Name Already Exist";
                    lblError.Visible = true;
                }
            }
            else
                lblError.Visible = true;
        }

        private string CreateUpdateEmailMessageBody()
        {
            StringBuilder bodyMessage = new StringBuilder(); //Build Message Body   
            try
            {
                bodyMessage.Append("Dear affiliate,<br/>" +
                                   "We wanted to let you know that we have made some changes to your profile. Please login and take a minute to look over and " +
                                   "verify that these changes are accurate. If you have any questions please feel free to contact us at 201-585-0808.");
                bodyMessage.Append("<br/><br/><br/>Thanks, LimoAllOver.<br/> 201-585-0808");
                return bodyMessage.ToString();
            }
            catch (Exception)
            {
                return bodyMessage.ToString();
            }
        }

        public int CompanyID
        {
            get
            {
                if (ViewState["CompanyID"] != null)
                    return (int)ViewState["CompanyID"];
                else
                    return 0;
            }
            set { ViewState["CompanyID"] = value; }
        }

        /// <summary>
        /// create mail body
        /// </summary>
        /// <param name="exception"></param>
        /// <returns>email body</returns>
        /// <remarks>Belal Salah El-Din 2-2012</remarks>
        private string CreateEmailMessageBody()
        {
            StringBuilder bodyMessage = new StringBuilder(); //Build Message Body   
            try
            {
                bodyMessage.Append("Dear,<br/>" + 
                    "1.       LOGIN YOUR ACCOUNT USING YOUR PERSONALIZED USERNAME AND PASSWORD VIA " +
                    "User name: " + "&nbsp;&nbsp;" + txtUserName.Text);
                bodyMessage.Append("<br/>" + "Password:" + "&nbsp;&nbsp;" + txtPassword.Text);
                bodyMessage .Append( "<br/>" + "Please complete your information using this link <a href='http://limoallover.net/Pages/Login.aspx'>Profile</a>" + "&nbsp;&nbsp;" + txtPassword.Text);
                bodyMessage.Append("<div style='text-align:center;'><img src='http://limoallover.net/Image/Email/ConfirmationEmail.png' class='' width='700px' height='405px' " + "/></div>");
                //bodyMessage .Append(
                //    "ADD YOUR COMPANY INFORMATION TO CREATE YOUR COMPANY PROFILE. BE SURE TO INCLUDE A DETAILED DESCRIPTION TO BETTER HELP CUSTOMERS WHEN THEY ARE CHOOSING THE PROVIDER THEY SEE BEST FIT FOR THEIR NEEDS.");
                //bodyMessage.Append("<div style='text-align:center;'><img src='http://limoallover.net/Image/Email/2.png' class='' width='700px' height='405px' " + "/></div>");
                //bodyMessage .Append( ". CLICK ON MANAGE CARS TO ADD YOUR FLEET.");
                //bodyMessage.Append("<div style='text-align:center;'><img src='http://limoallover.net/Image/Email/3.png' class='' width='700px' height='405px' " + "/></div>");
                //bodyMessage.Append("<div style='text-align:center;'><img src='http://limoallover.net/Image/Email/4.png' class='' width='700px' height='405px' " + "/></div>");
                //bodyMessage .Append( "4.  SELECT YOUR SERVICE AREA. YOU CAN SET THE RADUIS ANYWHERE BETWEEN 10-30 MILES");
                //bodyMessage.Append("<div style='text-align:center;'><img src='http://limoallover.net/Image/Email/5.png' class='' width='700px' height='405px' " + "/></div>");
                //bodyMessage .Append( "5. BE SURE TO INCLUDE A CONTACT PERSON WHERE THE CUSTOMER CAN BEST REACH YOU. <br />");
                //bodyMessage .Append( "To " + "<a href='http://limoallover.net/'>limoallover.net</a>");
                //bodyMessage .Append( " members this is a follow up email with detailed instructions on how to login to your personal account. We hope that through this account with us you will be able to obtain lots of business  and we look forward to working with you !");
                bodyMessage .Append( "<br/><br/><br/>Thanks, LimoAllOver.");
                return bodyMessage.ToString();
            }
            catch (Exception)
            {
                return bodyMessage.ToString();
            }
        }

        public bool IsSingnUP { get; set; }

        private string CreateAdminEmailMessageBody()
        {
            StringBuilder bodyMessage = new StringBuilder(); //Build Message Body   
            try
            {
                bodyMessage.Append("Dear Admin,<br/>" +
                                   "We wanted to let you know that " + txtCompanyName.Text +"has been registered.");
                bodyMessage.Append("<br/><br/><br/>Thanks, LimoAllOver.<br/> 201-585-0808");
                return bodyMessage.ToString();
            }
            catch (Exception)
            {
                return bodyMessage.ToString();
            }
        }

        public bool IsCorporate { get; set; }
    }
}