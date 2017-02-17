using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lemo.Classes;
using Lemo.DAL;

namespace Lemo.Pages
{
    public partial class CompleteReservation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JobWrapper jobWrapper = new JobWrapper();
            if (jobWrapper.JobDetailsObject != null)
            {
                jobWrapper.ISReservationExist = true;
                PrepareCompanyInformation(jobWrapper.JobDetailsObject);
                //if (JobWrapper.UserNumber == null)
                //    Response.Redirect("~/Pages/Login.aspx");
            }
            else
                Response.Redirect("~/Default.aspx");
        }

        private void PrepareCompanyInformation(JobObjectDetails jobObjectDetails)
        {
            int? companyID = jobObjectDetails.CompanyID;
            if(companyID.HasValue)
            {
                LimoEntitiesEntityFremwork limoEntities = new LimoEntitiesEntityFremwork();
                Company company = limoEntities.Companies.Where(xxx => xxx.CompanyID == companyID.Value).FirstOrDefault();
                if (company != null)
                {
                    imgLogo.ImageUrl = company.imagePath != null ? company.imagePath : string.Empty;
                    lblCompanyName.Text = company.CompanyName != null ? company.CompanyName : string.Empty;

                    divAboutUs.InnerText = company.About_Us != null ? company.About_Us : string.Empty;

                    divDescription.InnerText = company.Description != null ? company.Description : string.Empty;

                    divOurService.InnerText = company.OUR_SERVICE != null ? company.OUR_SERVICE : string.Empty;

                    divOurFleet.InnerText = company.OUR_FLEET != null ? company.OUR_FLEET : string.Empty;

                    divContactUs.InnerText = company.Contact_Us != null ? company.Contact_Us : string.Empty;
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            if (!IsPostBack)
            {
                int id = 0;
                int.TryParse(Request.QueryString["id"], out id);
                //int id = int.Parse(hfCarID.Value);
                JobWrapper jobWrapper = new JobWrapper();
                if (id != 0 && jobWrapper.JobDetailsObject.CarID == 0)
                {
                    LimoEntitiesEntityFremwork limoEnitiies = new LimoEntitiesEntityFremwork();
                    DAL.Car car = limoEnitiies.Cars.Where(xx => xx.CarID == id).FirstOrDefault();
                    if (car != null)
                    {
                        double distancePrice = jobWrapper.JobDetailsObject.Distance * (car.PricePerMaile != null ? car.PricePerMaile.Value : 1);
                        double hourPrice = jobWrapper.JobDetailsObject.JobDurationPerHours * (car.PricePerHore != null ? car.PricePerHore.Value : 1);
                        double tempPrice = distancePrice + hourPrice;
                        car.TotalPrice = tempPrice;

                        jobWrapper.JobDetailsObject.JobCompanyName = car.CompanyName;
                        jobWrapper.JobDetailsObject.JobCarType = car.CarName;
                        jobWrapper.JobDetailsObject.JobBasePrise = car.TotalPrice;
                        jobWrapper.JobDetailsObject.CarID = car.CarID;
                        jobWrapper.JobDetailsObject.CompanyID = car.CompanyID;
                    }
                }
            }
        }
    }
}