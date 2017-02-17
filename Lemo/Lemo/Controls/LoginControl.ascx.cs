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
    public partial class LoginControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void butSignUp_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/Pages/Signup.aspx");
        }

        protected void butLogin_Click(object sender, ImageClickEventArgs e)
        {
            //string apiId = ConfigurationSettings.AppSettings["apiId"]; //Your Api ID
            //string apiKey = ConfigurationSettings.AppSettings["apiKey"]; //Your Api Key

            //var apiService = new ApiService();
            //LoginResponse response = apiService.LoginAccount(apiId, apiKey, txtUsername.Text, txtPassword.Text);

            //if (string.IsNullOrEmpty(response.AcctNumber))
            //    lblError.Visible = true;
            //else
            //{
            try
            {
                LimoEntitiesEntityFremwork limoEntity = new LimoEntitiesEntityFremwork();
                Lemo.DAL.User user = limoEntity.Users.Where(
                    xx => xx.UserName.ToLower() == txtUsername.Text.ToLower() && xx.Passwords.ToLower() == txtPassword.Text.ToLower()).
                    ToList().FirstOrDefault();
                if (user != null)
                {
                    lblError.Visible = false;
                    JobWrapper jobWrapper = new JobWrapper();
                    //jobWrapper.JobDetailsObject.userID = user.UserID;
                    jobWrapper.UserNumber = user.UserID;
                    if (jobWrapper.JobDetailsObject != null && jobWrapper.ISReservationExist)
                        Response.Redirect("~/Pages/CompleteReservation.aspx");
                    else
                        Response.Redirect("~/Pages/ManageReservation.aspx");
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