using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lemo.DAL;
using Lemo.Admin.Helper;
using System.IO;

namespace Lemo.Admin.Controls
{
    public partial class CompanyInformation : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LimoEntitiesEntityFremwork limoEntities = new LimoEntitiesEntityFremwork();
                List<DAL.State> stateList = limoEntities.States.ToList();
                ddlState.DataSource = stateList;
                ddlState.DataTextField = "StateName";
                ddlState.DataValueField = "state_code";
                ddlState.DataBind();
                AdminHelper adminHelper = new AdminHelper();
                DAL.Company company;
                int id = 0;
                int.TryParse(Request.QueryString["id"], out id);
                if (id != 0)
                {
                    CompanyID = id;
                }
                if (adminHelper.IsAdmin)
                {
                    tdIsActive.Visible = true;
                    tdAllowDespatch.Visible = true;
                    adminHelper.CompanyObject = null;
                    company = limoEntities.Companies.Where(xxx => xxx.CompanyID == CompanyID).FirstOrDefault();
                    if (company != null)
                    {
                        cbIsActive.Checked = company.IsActive.HasValue ? company.IsActive.Value : false;
                        cbIsTop.Checked = company.IsTop.HasValue ? company.IsTop.Value : false;
                        cbAllowDespatch.Checked = company.IsDespath.HasValue ? company.IsDespath.Value : false;
                    }
                }
               else
               {
                   tdIsActive.Visible = false;
                   tdAllowDespatch.Visible = false;
                   company = adminHelper.CompanyObject;
               }
                if ( company != null)
                {
                    txtCompanyName.Text = company.CompanyName;
                    txtUsername.Text = company.UserName;
                    ddlState.SelectedValue = company.stateName;
                    txtCityTown.Text = company.CityName;
                    txtPrimaryAddress.Text = company.Address;
                    txtZipPost.Text = company.ZipCode;
                    cbAllState.Checked = company.IsSupportAllState.HasValue
                                             ? company.IsSupportAllState.Value
                                             : false;
                    cbIsAvailable.Checked = company.IsAvailable.HasValue
                                             ? company.IsAvailable.Value
                                             : false;
                    txtEmail.Text = company.Email;
                    txtMobilePhone.Text = company.Phone;
                    txtLimoAPIID.Text = company.LimoAPIID;
                    txtLimoAPIKey.Text = company.LimoAPIKey;
                    imgLogo.ImageUrl = company.imagePath != null ? company.imagePath : string.Empty;
                    txtDescription.Text = company.Description != null ? company.Description : string.Empty;
                    //txtPassword.Text = company.Password;

                    txtAbout_Us.Text = company.About_Us != null ? company.About_Us : string.Empty;
                    txtOUR_SERVICE.Text = company.OUR_SERVICE != null ? company.OUR_SERVICE : string.Empty;
                    txtOUR_FLEET.Text = company.OUR_FLEET != null ? company.OUR_FLEET : string.Empty;
                    txtContact_Us.Text = company.Contact_Us != null ? company.Contact_Us : string.Empty;

                    txtTaxState.Text = company.State_Tax.HasValue ? company.State_Tax.ToString() : string.Empty;
                    txtTaxCity.Text = company.City_Tax.HasValue? company.City_Tax.ToString() : string.Empty;

                    txtOtherTax1.Text = company.OtherTax1.HasValue ? company.OtherTax1.ToString() : string.Empty;
                    txtOtherTax1Note.Text = company.Tax1Note != null ? company.Tax1Note : string.Empty;
                    txtOtherTax2.Text = company.OtherTax2.HasValue ? company.OtherTax2.ToString() : string.Empty;
                    txtOtherTax2Note.Text = company.Tax2Note != null ? company.Tax2Note : string.Empty;
                    txtGratuity.Text = company.Gratuity.HasValue ? company.Gratuity.ToString() : string.Empty;
                    txtGratuityNote.Text = company.GratuityNote != null ? company.GratuityNote : string.Empty;
                }
                if(IsOperationSucceed)
                {
                    IsOperationSucceed = false;
                    divConfirmation.Visible = true;
                }
            }
        }

        protected void butSignUp_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                LimoEntitiesEntityFremwork limoEntities = new LimoEntitiesEntityFremwork();
                AdminHelper adminHelper = new AdminHelper();
                Company company;
                if (adminHelper.IsAdmin)
                {
                    adminHelper.CompanyObject = null;
                    company =
                   limoEntities.Companies.Where(xx => xx.CompanyID == CompanyID).
                       FirstOrDefault();
                    if (company != null)
                    {
                        company.IsActive = cbIsActive.Checked; 
                        company.IsTop = cbIsTop.Checked;
                        company.IsDespath = cbAllowDespatch.Checked;
                    }
                }
                else
                {
                    company =
                        limoEntities.Companies.Where(xx => xx.CompanyID == adminHelper.CompanyObject.CompanyID).
                            FirstOrDefault();
                }
                if (company != null)
                {
                    company.CompanyName = txtCompanyName.Text;
                    company.UserName = txtUsername.Text;
                    company.stateName = ddlState.SelectedValue;
                    company.CityName = txtCityTown.Text;
                    company.Address = txtPrimaryAddress.Text;
                    company.ZipCode = txtZipPost.Text;
                    company.IsSupportAllState = cbAllState.Checked;
                    company.IsAvailable = cbIsAvailable.Checked;
                    company.Email = txtEmail.Text;
                    company.Phone = txtMobilePhone.Text;
                    company.LimoAPIID = txtLimoAPIID.Text;
                    company.LimoAPIKey = txtLimoAPIKey.Text;

                    company.About_Us = txtAbout_Us.Text;
                    company.OUR_SERVICE = txtOUR_SERVICE.Text;
                    company.OUR_FLEET = txtOUR_FLEET.Text;
                    company.Contact_Us = txtContact_Us.Text;

                    company.State_Tax = !string.IsNullOrEmpty(txtTaxState.Text)? double.Parse(txtTaxState.Text): new double?();
                    company.City_Tax = !string.IsNullOrEmpty(txtTaxCity.Text) ? double.Parse(txtTaxCity.Text) : new double?();

                    company.OtherTax1 = !string.IsNullOrEmpty(txtOtherTax1.Text) ? double.Parse(txtOtherTax1.Text) : new double?();
                    company.Tax1Note = txtOtherTax1Note.Text;
                    company.OtherTax2 = !string.IsNullOrEmpty(txtOtherTax2.Text) ? double.Parse(txtOtherTax2.Text) : new double?();
                    company.Tax2Note = txtOtherTax2Note.Text;
                    company.Gratuity = !string.IsNullOrEmpty(txtGratuity.Text) ? double.Parse(txtGratuity.Text) : new double?();
                    company.GratuityNote = txtGratuityNote.Text;
                    
                    string imagPublishedPath = string.Empty;
                    if (fu_Image.HasFile)
                    {
                        imagPublishedPath = SaveImg(fu_Image);
                    }
                    if (!string.IsNullOrEmpty(imagPublishedPath))
                        company.imagePath = imagPublishedPath;
                    company.Description = txtDescription.Text;
                    limoEntities.SaveChanges();
                    if (adminHelper.CompanyObject != null)
                        adminHelper.CompanyObject = company;
                }
                //company.Password = txtPassword.Text;
                IsOperationSucceed = true;
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception)
            {
                divError.Visible = true;
            }
        }

        private string SaveImg(FileUpload fu_Img)
        {
            string filename = Path.GetFileName(fu_Img.FileName);
            string temp = @"d:\inetpub\vhosts\limoallover.net\httpdocs\AppImages\company\logos\";
            string imagPublishedPath =
                string.Format(@"http://limoallover.net/AppImages/company/logos/" + txtCompanyName.Text + fu_Img.FileName);
            string tempFile = temp + txtCompanyName.Text + filename;
            fu_Img.SaveAs(tempFile);
            bool isExist = CheckExistFile(tempFile);
            if (isExist)
            {
                return imagPublishedPath;
            }
            else
            {
                return string.Empty;
            }
        }

        protected bool CheckExistFile(string filePath)
        {
            bool isExist = false;
            try
            {
                File.ReadAllText(filePath);
                isExist = true;
            }
            catch (Exception ex)
            {
                isExist = false;
            }
            return isExist;
        }

        private bool IsOperationSucceed
        {
            get
            {
                if (Session["IsOperationSucceed"] != null)
                    return (bool)Session["IsOperationSucceed"];
                return false;
            }
            set
            {
                if (value)
                    Session["IsOperationSucceed"] = value;
                else
                    Session["IsOperationSucceed"] = null;
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
    }
}