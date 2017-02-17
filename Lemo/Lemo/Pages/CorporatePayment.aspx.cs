using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lemo.Classes;
using Lemo.DAL;
using Lemo.Helper;
using Lemo.Mylimobiz;
using System.Text;
using System.Configuration;

namespace Lemo.Pages
{
    public partial class CorporatePayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JobWrapper jobWrapper = new JobWrapper();
            if (jobWrapper.JobDetailsObject != null)
            {
                jobWrapper.ISReservationExist = true;
              
            }
            else
                Response.Redirect("~/Default.aspx");
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
                Lemo.DAL.Corporate corporate = limoEntity.Corporates.Where(
                    xx => xx.UserName.ToLower() == txtUsername.Text.ToLower() && xx.Password.ToLower() == txtPassword.Text.ToLower()).
                    ToList().FirstOrDefault();
                if (corporate != null)
                {
                    lblError.Visible = false;
                    JobWrapper jobWrapper = new JobWrapper();
                    //jobWrapper.JobDetailsObject.userID = user.UserID;
                    jobWrapper.CorporateNumber = corporate.ID;
                    if (jobWrapper.JobDetailsObject != null && jobWrapper.ISReservationExist)
                    {
                        AddJob();
                        jobWrapper.JobDetailsObject = null;
                        butLogin.Visible = false;
                        //Response.Redirect("~/Pages/CompleteReservation.aspx");
                    }
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

        void AddJob()
        {
            JobWrapper jobWrapper = new JobWrapper();
            if (jobWrapper.JobDetailsObject != null)
            {

                LimoEntitiesEntityFremwork limmoEntity = new LimoEntitiesEntityFremwork();
                Company company =
                    limmoEntity.Companies.Where(xx => xx.CompanyID == jobWrapper.JobDetailsObject.CompanyID).ToList().
                        FirstOrDefault();
                string tripConfirmationNumber = string.Empty;
                if (company != null)
                {
                    string apiId = company.LimoAPIID; //Your Api ID
                    string apiKey = company.LimoAPIKey; //Your Api Key
                    tripConfirmationNumber = AddJobtoCompanyLimoAnyWhere(apiId, apiKey, company);

                    string limoAPIId = ConfigurationSettings.AppSettings["apiId"]; //Your Api ID
                    string LimoAPIapiKey = ConfigurationSettings.AppSettings["apiKey"]; //Your Api Key
                    if (company.LimoAPIID != limoAPIId && company.LimoAPIKey != LimoAPIapiKey)
                    {
                        AddJobtoCompanyLimoAnyWhere(limoAPIId, LimoAPIapiKey, company, tripConfirmationNumber);
                    }
                }

                //string tripConfirmationNumber = AddJobtoCompanyLimoAnyWhere();
                Job job = SaveJobInDatabase(tripConfirmationNumber);
                SendEmail(job);
                divConfirmation.Visible = true;
                divProblem.Visible = false;
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        private Job SaveJobInDatabase(string tripConfirmationNumber)
        {
            LimoEntitiesEntityFremwork limoEntity = new LimoEntitiesEntityFremwork();
            JobWrapper jobWrapper = new JobWrapper();
            Job job = new Job();
            job.CompanyCarID = jobWrapper.JobDetailsObject.CarID;
            job.UserID = jobWrapper.UserNumber > 0 ? jobWrapper.UserNumber : new int?(); //.JobDetailsObject.userID;
            job.FromAddress = jobWrapper.JobDetailsObject.FromAddress;
            job.ToAddress = jobWrapper.JobDetailsObject.ToAddress;
            job.CityID = jobWrapper.JobDetailsObject.cityID;
            job.JobDate = jobWrapper.JobDetailsObject.JobDate;
            job.JobTime = jobWrapper.JobDetailsObject.JobMappedTime;
            job.TotalPrice = jobWrapper.JobDetailsObject.JobTotalPrise;
            job.EstimatedFarePrice = jobWrapper.JobDetailsObject.JobBasePrise;
            job.GratuityPrice = jobWrapper.JobDetailsObject.JobGratuity;
            job.ProcessingFee = jobWrapper.JobDetailsObject.JobProcessingFees;
            job.Taxes = jobWrapper.JobDetailsObject.JobStateTaxes;
                        
            if (Corporate != null)
            {
                job.FirstName = Corporate.UserName;
                job.LastName = Corporate.UserName;
                job.CorporateID = Corporate.ID;
            }

            job.ComapnyConfirmNum = tripConfirmationNumber;
            job.AuthorizationCode = "";
            job.AuthorizInvoiceNumber = "";

            //job.OtherPrice = jobWrapper.JobDetailsObject.otherPrice;
            if (limoEntity.JobStatus != null)
            {
                string pending = Limo_Gloabel.JobStatus.Pending.ToString().ToLower();
                DAL.JobStatu jobStatus =
                    limoEntity.JobStatus.Where(obj => obj.JobStatus.ToLower() == pending).FirstOrDefault();
                if (jobStatus != null)
                    job.JobStatusID = jobStatus.JobStatusID;
            }
            int idTemp = jobWrapper.JobDetailsObject != null ? jobWrapper.JobDetailsObject.CarID : 0;
            var car =
                limoEntity.Cars.Where(xxx => xxx.CarID == idTemp).
                    FirstOrDefault();
            int driverID = 0;
            if (car != null)
            {
                driverID = car.Drivers != null && car.Drivers.Count > 0 ? car.Drivers.ToList()[0].DriverID : 0;
                //if (driverID > 0)
                    //job.DriverID = driverID;
            }
            job.JobBy = 1;
            job.NoPassenger = int.Parse(jobWrapper.JobDetailsObject.UserNumber);

            job.FromLat = jobWrapper.JobDetailsObject.FromAddressLat;
            job.FromLng = jobWrapper.JobDetailsObject.FromAddressLNG;
            job.ToLat = jobWrapper.JobDetailsObject.ToAddressLat;
            job.ToLng = jobWrapper.JobDetailsObject.ToAddressLNG;

            limoEntity.Jobs.AddObject(job);
            limoEntity.SaveChanges();
            try
            {
                if (driverID > 0)
                {
                    //Service1.LimoAllOver ccc = new Service1.LimoAllOver();
                    MobileService.LimoAllOver ccc = new MobileService.LimoAllOver();
                    //ccc.SendNotifcationToDriver("android", driverID);
                    ccc.SendJobToDriver(driverID, job.JobID);
                }
            }
            catch (Exception)
            {
            }

            if (string.IsNullOrEmpty(job.ComapnyConfirmNum))
            {
                job.ComapnyConfirmNum = job.JobID.ToString() + DateTime.Now.Day.ToString() +
                                        DateTime.Now.Month.ToString() +
                                        DateTime.Now.Year.ToString().Substring(2);
            }
            job.LimoConfirmNumber = "L" + job.JobID.ToString() + DateTime.Now.Day.ToString() +
                                    DateTime.Now.Month.ToString() +
                                    DateTime.Now.Year.ToString().Substring(2);
            limoEntity.SaveChanges();
            return job;
        }

        private string AddJobtoCompanyLimoAnyWhere(string apiId, string apiKey, Company company, string affliateTripConfirmationNumber = "")
        {
            JobWrapper jobWrapper = new JobWrapper();         
            if (!string.IsNullOrEmpty(apiId) && !string.IsNullOrEmpty(apiId))
            {
                double gratuity = company.Gratuity.HasValue?company.Gratuity.Value:0;
                var apiService = new Mylimobiz.ApiService();
                Mylimobiz.VehicleTypeResponse cehicleTypeResponse = apiService.GetVehicleTypes(apiId, apiKey);
                string vehicleType = cehicleTypeResponse.VehicleTypes != null &&
                                     cehicleTypeResponse.VehicleTypes.Length > 0
                                         ? cehicleTypeResponse.VehicleTypes[0].VehTypeCode
                                         : string.Empty;
                //apiService.mylimobiz.book.ApiService laApi = new com.mylimobiz.book.ApiService();
                string[] fromAddress = jobWrapper.JobDetailsObject.FromAddress.Trim().Split(',');
                string fromStreet = "", fromCity = "", fromState = "", fromZIPCode = "";
                if (fromAddress.Length > 0)
                {
                    if (fromAddress.Length >= 3)
                    {
                        fromStreet = fromAddress[0];
                        fromCity = fromAddress[1];
                        string[] temp = fromAddress[2].Trim().Split(' ');
                        fromState = temp[0];
                        if (temp.Length == 2)
                            fromZIPCode = temp[1];
                    }
                    else if (fromAddress.Length == 2)
                    {
                        fromCity = fromAddress[0];
                        string[] temp = fromAddress[1].Trim().Split(' ');
                        fromState = temp[0];
                        if (temp.Length == 2)
                            fromZIPCode = temp[1];
                    }
                }
                string[] toAddress = jobWrapper.JobDetailsObject.ToAddress.Trim().Split(',');
                string toStreet = "", toCity = "", toState = "", toZIPCode = "";
                if (toAddress.Length > 0)
                {
                    if (toAddress.Length >= 3)
                    {
                        toStreet = toAddress[0];
                        toCity = toAddress[1];
                        string[] temp = toAddress[2].Trim().Split(' ');
                        toState = temp[0];
                        if (temp.Length == 2)
                            toZIPCode = temp[1];
                    }
                    else if (toAddress.Length == 2)
                    {
                        toCity = toAddress[0];
                        string[] temp = toAddress[1].Trim().Split(' ');
                        toState = temp[0];
                        if (temp.Length == 2)
                            toZIPCode = temp[1];
                    }
                }
                double carSeatInsidePickupFee = 0;
                if (jobWrapper.JobDetailsObject.IsInsidePickup)
                    carSeatInsidePickupFee += 15;
                if (jobWrapper.JobDetailsObject.IsCarSeatAdd)
                    carSeatInsidePickupFee += 30;
                carSeatInsidePickupFee += jobWrapper.JobDetailsObject.JobProcessingFees;
                Mylimobiz.Ride ride = new Mylimobiz.Ride()
                {
                    RideSource = "Limoallover",
                    RideType = "RES",
                    ServiceType = jobWrapper.JobDetailsObject.ServiceTypeObj.ToString(),
                    // done
                    VehicleType = vehicleType,
                    //jobWrapper.JobDetailsObject.JobCarType.ToString(),
                    PickUpDate = jobWrapper.JobDetailsObject.JobDate,
                    PickUpTime = jobWrapper.JobDetailsObject.JobMappedTime,

                    NumberOfPax = int.Parse(jobWrapper.JobDetailsObject.UserNumber),
                    //done
                    PassengerFirstName = Corporate != null ? Corporate.UserName : "Corporate",
                    //done
                    PassengerLastName = Corporate != null ? Corporate.UserName : "Corporate",
                    //done
                    PassengerEmail = Corporate != null ? Corporate.Email : "Corporate",
                    // done
                    PassengerPhone = Corporate != null ? Corporate.Phone : "Corporate",
                    // done
                    BillingContact = "Limo All Over",
                    //txtFirstName.Text + " " + txtLastName.Text, //done
                    Currency = "USD",
                    //done
                    //RideNumber = DateTime.Now.Ticks.ToString(),
                    SpecialChildSeat =
                        jobWrapper.JobDetailsObject.IsCarSeatAdd ? "Toddler" : "No",
                    // done

                    RatesMapping = new RatesMapping()
                    {
                        BaseRate =
                            new RateFixed()
                            {
                                Rate =
                                    jobWrapper.JobDetailsObject.
                                    JobBasePrise
                            },
                        PerHourRate = new RateMultiply() { RatePerUnit = 0 },
                        PerMileRate =
                            new RateMultiply() { RatePerUnit = 0, Units = 0, Total = 0 },
                        PerPassengerRate =
                            new RateMultiply() { RatePerUnit = 0, Units = 0, Total = 0 },
                        ExtraStops = new RateFixed() { Rate = 0 },
                        Discount2 = new RateFixed() { Rate = 0 },
                        FuelSurcharge =
                            new RatePercentage() { PercentageAmount = 3 },
                        Gratuity =
                            new RatePercentage() { PercentageAmount = gratuity },
                        Misc1 =
                            new RateMisc() { Rate = 0, RateName = "Tolls" },
                        Misc2 =
                            new RateMisc() { Rate = 0, RateName = "Parking" },
                        Misc3 = new RateMisc()
                        {
                            Rate =
                                jobWrapper.JobDetailsObject.
                                    JobDriversWorker +
                                jobWrapper.JobDetailsObject.
                                    JobLocalTxes +
                                jobWrapper.JobDetailsObject.
                                    JobStateTaxes,
                            RateName = "Taxes"
                        },
                        Misc4 =
                            new RateMisc()
                            {
                                Rate = carSeatInsidePickupFee,
                                RateName =
                                    "ProcessingFees / CarSeat / InsidePickupFee"
                            },
                        OvertimeWaitTime = new RateFixed() { Rate = 0 },
                        STCSurcharge =
                            new RatePercentage() { PercentageAmount = 0 },
                        Total = jobWrapper.JobDetailsObject.JobTotalPrise
                    },
                    RideRouteBlock = new RoutingItem[]
                                                                   {
                                                                       new RoutingItem()
                                                                           {
                                                                               RIType = "PU",
                                                                               LocationType = "ADDR",
                                                                               RIAddr1 = fromStreet,
                                                                               RICity = fromCity,
                                                                               RIState = fromState,
                                                                               RIZip = fromZIPCode
                                                                           },
                                                                       new RoutingItem()
                                                                           {
                                                                               RIType = "DO",
                                                                               LocationType = "ADDR",
                                                                               RIAddr1 = toStreet,
                                                                               RICity = toCity,
                                                                               RIState = toState,
                                                                               RIZip = toZIPCode
                                                                           }
                                                                   },
                    //credit card
                    //////////////CreditCardNumber = txtCardNumber.Text,
                    //////////////CCExpDate = ddlExpirationMonth.SelectedValue + "/" + ddlExpirationYear.SelectedValue,
                    //////////////CCName = txtFirstName.Text + " " + txtLastName.Text,
                    //////////////CCState = ddlState.SelectedValue,
                    //////////////CCBillingAddr = txtAdddress.Text,
                    //////////////CCZip = txtZipCode.Text  //,
                    //////////////CCNotes = txtCardVerifNumber.Text
                };
                if (!string.IsNullOrEmpty(affliateTripConfirmationNumber))
                {
                    ride.TripDispatchNotes = "Company Name: " + company.CompanyName + ", Company Trip Confirmation Number" + affliateTripConfirmationNumber;
                }
                Mylimobiz.RideResponse response = apiService.ImportReservation(apiId, apiKey, ride);
                if (response.ResponseCode == 0)
                {
                    //OK. Reservation was imported
                    //Source...
                    return response.TripInfo.TripConfirmationNumber;
                }
                else
                {
                    return string.Empty;
                }
            }
            return string.Empty;
        }

        private void SendEmail(DAL.Job job)
        {
            string userEmail, CompanyEmail;
            if (job.UserID.HasValue)
                userEmail = job.User.Email;
            else
                userEmail = job.Email;
            //send email to user
            EmailManager.SendEmail("Limoallover", CreateEmailMessageBody(job.LimoConfirmNumber, job.ComapnyConfirmNum), userEmail, true);

            CompanyEmail = job.CompanyEmail;
            //send email to company
            EmailManager.SendEmail("Limoallover", CreateEmailMessageBody(job.LimoConfirmNumber, job.ComapnyConfirmNum), CompanyEmail, true);

            //send email to admin
            EmailManager.SendEmail("Limoallover", CreateEmailMessageBody(job.LimoConfirmNumber, job.ComapnyConfirmNum), "", true, true);

            //send email to markting
            EmailManager.SendEmail("Limoallover", CreateEmailMessageBody(job.LimoConfirmNumber, job.ComapnyConfirmNum), "rockims27@gmail.com", true);
        }

        private string CreateEmailMessageBody(string limoNumber, string CompanyNumber)
        {
            StringBuilder bodyMessage = new StringBuilder(); //Build Message Body   
            try
            {
                bodyMessage.Append("Dear sir,<br/>" +
                                   "We wanted to let you know that new job #" + limoNumber + "has been added.");
                bodyMessage.Append("<br/><br/><br/>Thanks, LimoAllOver.<br/> 201-585-0808");
                return bodyMessage.ToString();
            }
            catch (Exception)
            {
                return bodyMessage.ToString();
            }
        }

        Lemo.DAL.Corporate _corporate;
        private Lemo.DAL.Corporate Corporate
        {
            get 
            {
                JobWrapper jobWrapper = new JobWrapper();
                if (jobWrapper.CorporateNumber != 0 && _corporate == null)
                {
                    LimoEntitiesEntityFremwork limoEntity = new LimoEntitiesEntityFremwork();
                    _corporate = limoEntity.Corporates.Where(xx => xx.ID == jobWrapper.CorporateNumber).ToList().FirstOrDefault();
                }

                return _corporate;
            }
        }
    }
}