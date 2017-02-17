using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Lemo.Classes;
using Lemo.DAL;

namespace Lemo.Controls
{
    public partial class SignupControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // for phone number \d{5}\-\d{3}           
        }

        protected void butSignUp_Click(object sender, ImageClickEventArgs e)
        {
            //string apiId = ConfigurationSettings.AppSettings["apiId"]; //Your Api ID
            //string apiKey = ConfigurationSettings.AppSettings["apiKey"]; //Your Api Key
            //ShortAccountInfo shortAccountInfo = new ShortAccountInfo();
            //shortAccountInfo.FirstName = txtFirstName.Text;
            //shortAccountInfo.LastName = txtLastName.Text;
            //shortAccountInfo.MobilePhone = txtMobilePhone.Text;

            //AccountEmail accountEmail = new AccountEmail();
            //accountEmail.Email = txtEmail.Text;
            //AccountEmail[] accountEmailList = new AccountEmail[1];
            //accountEmailList[0] = accountEmail;
            //shortAccountInfo.EmailList = accountEmailList;
            //shortAccountInfo.Email = txtEmail.Text;            

            //shortAccountInfo.Password = txtPassword.Text;
            //shortAccountInfo.VerifyPassword = txtConfirmPassword.Text;
            //shortAccountInfo.UserName = txtUsername.Text;
            //shortAccountInfo.Country = ddlCountry.SelectedItem.Text;
            //shortAccountInfo.CityTown = txtCityTown.Text;
            //shortAccountInfo.ZipPost = txtZipPost.Text;
            //shortAccountInfo.PrimaryAddress = txtPrimaryAddress.Text;


            //var apiService = new ApiService();
            //AccountInfoResponse response = apiService.CreateAccount(apiId, apiKey, shortAccountInfo);

            LimoEntitiesEntityFremwork limoEntity = new LimoEntitiesEntityFremwork();
            Lemo.DAL.User user = new Lemo.DAL.User();
            JobWrapper jobWrapper = new JobWrapper();
            try
            {
                Lemo.DAL.User tempUser = limoEntity.Users.Where(
                    xx => xx.UserName.ToLower() == txtUsername.Text.ToLower()).
                    ToList().FirstOrDefault();
                if (tempUser == null)
                {
                    user.FirstName = txtFirstName.Text;
                    user.LastName = txtLastName.Text;
                    user.Mobile = txtMobilePhone.Text;
                    user.Email = txtEmail.Text;
                    user.Passwords = txtPassword.Text;
                    user.UserName = txtUsername.Text;
                    //user.Country = ddlCountry.SelectedItem.Text;
                    //user.CityTown = txtCityTown.Text;
                    user.ZipCode = txtZipPost.Text;
                    user.Address = txtPrimaryAddress.Text;
                    limoEntity.Users.AddObject(user);
                    limoEntity.SaveChanges();
                    jobWrapper.UserNumber = user.UserID;
                    jobWrapper.UserNumber = user.UserID;
                    lblError.Visible = false;
                    if (jobWrapper.JobDetailsObject != null && jobWrapper.ISReservationExist)
                        Response.Redirect("~/Pages/CompleteReservation.aspx");
                    else
                        Response.Redirect("~/Pages/MyAccount.aspx");
                }
                else
                {
                    lblError.Visible = true;
                }
            }
            catch (Exception)
            {
                lblError.Visible = true;
            }
        }
    }
}