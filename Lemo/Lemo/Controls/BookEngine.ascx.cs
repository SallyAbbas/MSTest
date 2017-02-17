using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Lemo.Classes;
using Lemo.DAL;
using Lemo.Mylimobiz;

namespace Lemo.Control
{
    public partial class BookEngine : System.Web.UI.UserControl, IPostBackEventHandler
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bool isHourEncrese = false;
                int minute = DateTime.Now.Minute;
                if (minute <= 15 && minute > 0)
                {
                    ddlMinutes.SelectedIndex = 2;
                    MinMinutsIndex = 2;
                }
                else if (minute <= 30)
                {
                    ddlMinutes.SelectedIndex = 3;
                    MinMinutsIndex = 3;
                }
                else if (minute <= 45)
                {
                    ddlMinutes.SelectedIndex = 4;
                    MinMinutsIndex = 4;
                }
                else
                {
                    ddlMinutes.SelectedIndex = 1;
                    MinMinutsIndex = 1;
                    isHourEncrese = true;
                }
                AdjustTime(isHourEncrese);
            }
        }

        protected void imgbutCalculatePrise_Click(object sender, ImageClickEventArgs e)
        {
        }

        public void RaisePostBackEvent(string eventArgument)
        {
            if (IsDespatch)
                ProcessDespatchData();
            else
                ProcessData(eventArgument);
        }

        private void ProcessData(string eventArgument)
        {
            string lat = string.Empty, lng = string.Empty, latTo = string.Empty, lngTo = string.Empty;
            if (!string.IsNullOrEmpty(hfLAtLng.Value) && !string.IsNullOrEmpty(hfLAtLngTo.Value))
            {

                var tempLatLng = hfLAtLng.Value.Split(',');
                lat = tempLatLng[0];
                lng = tempLatLng[1];
                var tempLatLngTo = hfLAtLngTo.Value.Split(',');
                latTo = tempLatLngTo[0];
                lngTo = tempLatLngTo[1];
            }
            JobWrapper jobWrapper = new JobWrapper();
            if (eventArgument == "LimoBookEngine")
            {
                if (hfDistance.Value != null)
                {
                    if (hfDistance.Value == "-1" || string.IsNullOrEmpty(hfDistance.Value))
                    {
                        string message = "Google Map could not be created for the entered parameters. Please be specific while providing the destination location.";
                        string script = "alert('" + message + "');";
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", script, true);
                    }
                    else if (!IsValidDate)
                    {
                        string message = " Invalid Date / Time.";
                        string script = "alert('" + message + "');";
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", script, true);
                    }
                    else
                    {
                        double temp = 0, duration = 1;
                        double minMile = double.Parse(ConfigurationSettings.AppSettings["MinMile"]);
                        double.TryParse(hfDistance.Value, out temp);
                        temp = temp == 0 || temp < minMile ? minMile : temp;
                        string[] arrTemp = hfDuration.Value.Split(' ');
                        if (arrTemp.Length == 4)
                        {
                            duration = (double.Parse(arrTemp[0])) * 60 + double.Parse(arrTemp[2]);
                        }
                        else if (arrTemp.Length == 2)
                        {
                            double.TryParse(hfDuration.Value.Split(' ')[0], out duration);
                        }
                        duration = duration * 1 / 60;
                        duration = duration <= 1 ? 1 : duration <= 2 ? 2 : duration;
                        jobWrapper.JobDetailsObject = new JobObjectDetails();
                        jobWrapper.JobDetailsObject.FromAddress = txtSource.Text;// +", NEW YORK, NY, USA";
                        jobWrapper.JobDetailsObject.ToAddress = txtDistenation.Text;// +", NEW YORK, NY, USA";
                        jobWrapper.JobDetailsObject.JobDate = DateTime.Parse(txtDatepicker.Text);
                        jobWrapper.JobDetailsObject.JobHours = ddlHour.SelectedItem.Text;
                        jobWrapper.JobDetailsObject.JobMinuts = ddlMinutes.SelectedItem.Text;
                        jobWrapper.JobDetailsObject.UserNumber = ddlNOPassenger.SelectedItem.Text;
                        jobWrapper.JobDetailsObject.UserNumberINT = int.Parse(ddlNOPassenger.SelectedValue);
                        jobWrapper.JobDetailsObject.Distance = temp;
                        jobWrapper.JobDetailsObject.JobTime = ddlHour.SelectedItem.Text.Substring(0, 2) +
                            ddlMinutes.SelectedItem.Text + ddlHour.SelectedItem.Text.Substring(2);
                        jobWrapper.JobDetailsObject.JobMappedTime = ddlHour.SelectedValue + ddlMinutes.SelectedItem.Text;
                        jobWrapper.JobDetailsObject.JobDurationPerHours = duration;
                        jobWrapper.JobDetailsObject.JobDurationPerMinuts = hfDuration.Value;
                        jobWrapper.JobDetailsObject.ServiceTypeObj = JobWrapper.ServiceTypeObject.PTP;

                        jobWrapper.JobDetailsObject.FromAddressLat = lat;
                        jobWrapper.JobDetailsObject.FromAddressLNG = lng;
                        jobWrapper.JobDetailsObject.ToAddressLat = latTo;
                        jobWrapper.JobDetailsObject.ToAddressLNG = lngTo;
                        if (hfBookType.Value == "RideLater")
                            Response.Redirect("~/Pages/JobDetails.aspx");
                        else
                            Response.Redirect("~/Pages/LiveReservation.aspx");
                    }
                }
            }
        }

        private bool IsValidDate
        {
            get
            {
                try
                {
                    string[] dateArray = txtDatepicker.Text.Split('/');
                    int year = int.Parse(dateArray[2].Length > 4 ? dateArray[2].Substring(0, 4) : dateArray[2]);
                    int month = int.Parse(dateArray[0]);
                    int day = int.Parse(dateArray[1]);
                    DateTime selectedDate = new DateTime(year, month, day);
                    if (IsMidNight)
                    {
                        if (selectedDate.Date > MinDate.Date)
                            return true;
                        if (selectedDate.Date == MinDate.Date && ddlHour.SelectedIndex > MinHour)
                            return true;
                        if (selectedDate.Date == MinDate.Date && ddlHour.SelectedIndex == MinHour && ddlMinutes.SelectedIndex >= MinMinutsIndex)
                            return true;
                    }
                    else
                    {
                        if (selectedDate.Date > MinDate.Date)
                            return true;
                        if (selectedDate.Date == MinDate.Date && ddlHour.SelectedIndex > MinHour)
                            return true;
                        if (selectedDate.Date == MinDate.Date && ddlHour.SelectedIndex == MinHour && ddlMinutes.SelectedIndex >= MinMinutsIndex)
                            return true;
                    }
                    return false;
                }
                catch (Exception)
                {

                    return true;
                }
            }
        }

        private void AdjustTime(bool isHourEncrese)
        {
            int allowHoure = 0;
            //if(rbReservationLater.Checked)
            allowHoure = int.Parse(ConfigurationSettings.AppSettings["AllowHoure"]);
            //else
            //    allowHoure = int.Parse(ConfigurationSettings.AppSettings["AllowLiveHoure"]);
            int hour = DateTime.Now.Hour;
            if (isHourEncrese)
                hour += 1;
            if (hour + allowHoure <= 24)
            {
                int temp = hour + allowHoure;
                int index = ddlHour.Items.IndexOf(ddlHour.Items.FindByValue(temp.ToString()));
                index = index == -1 ? 1 : index;
                ddlHour.SelectedIndex = index;
                //string tempDate = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
                txtDatepicker.Text = DateTime.Now.ToString("MM/dd/yyyy");
                MinDate = DateTime.Now;
                MinHour = index;
                IsMidNight = false;
            }
            else
            {
                int temp = hour + allowHoure - 24;
                int index = ddlHour.Items.IndexOf(ddlHour.Items.FindByValue(temp.ToString()));
                ddlHour.SelectedIndex = index;
                //DateTime dateTomo = DateTime.Now.Date.AddDays(1);
                //string tempDate = dateTomo.Day.ToString() + "/" + dateTomo.Month.ToString() + "/" + dateTomo.Year.ToString();
                txtDatepicker.Text = DateTime.Now.AddDays(1).ToString("MM/dd/yyyy"); ;
                MinDate = DateTime.Now.AddDays(1);
                MinHour = index;
                IsMidNight = true;
            }
        }

        private DateTime MinDate
        {
            set { ViewState["BookEngine_MinDate"] = value; }
            get
            {
                if (ViewState["BookEngine_MinDate"] != null)
                    return (DateTime)ViewState["BookEngine_MinDate"];
                else
                    return DateTime.Now;
            }
        }

        private int MinHour
        {
            set { ViewState["BookEngine_MinHour"] = value; }
            get
            {
                if (ViewState["BookEngine_MinHour"] != null)
                    return (int)ViewState["BookEngine_MinHour"];
                else
                    return 0;
            }
        }

        private bool IsMidNight
        {
            set { ViewState["BookEngine_IsMidNight"] = value; }
            get
            {
                if (ViewState["BookEngine_IsMidNight"] != null)
                    return (bool)ViewState["BookEngine_IsMidNight"];
                else
                    return false;
            }
        }

        private int MinMinutsIndex
        {
            set { ViewState["BookEngine_MinMinutsIndex"] = value; }
            get
            {
                if (ViewState["BookEngine_MinMinutsIndex"] != null)
                    return (int)ViewState["BookEngine_MinMinutsIndex"];
                else
                    return 0;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string eventArgument = "LimoBookEngine";
            if (IsDespatch)
                ProcessDespatchData();
            else
                ProcessData(eventArgument);
        }

        protected void butAddDespatch_Click(object sender, EventArgs e)
        {
            string eventArgument = "LimoBookEngine";
            if (IsDespatch)
                ProcessDespatchData();
            else
                ProcessData(eventArgument);
        }

        public bool IsDespatch
        {
            set { ViewState["BookEngine_IsDespatch"] = value; }
            get
            {
                if (ViewState["BookEngine_IsDespatch"] != null)
                    return (bool)ViewState["BookEngine_IsDespatch"];
                else
                    return false;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            butCalc.Visible = !IsDespatch;
            butAddDespatch.Visible = IsDespatch;
            trResrvationType.Visible = !IsDespatch;
            trPassengerInformation.Visible = IsDespatch;
        }

        private void ProcessDespatchData()
        {
            if (!string.IsNullOrEmpty(hfLAtLng.Value) && !string.IsNullOrEmpty(hfLAtLngTo.Value))
            {
                var tempLatLng = hfLAtLng.Value.Split(',');
                var lat = tempLatLng[0];
                var lng = tempLatLng[1];
                var tempLatLngTo = hfLAtLngTo.Value.Split(',');
                var latTo = tempLatLngTo[0];
                var lngTo = tempLatLngTo[1];
                JobWrapper jobWrapper = new JobWrapper();
                if (hfDistance.Value != null)
                {
                    if (hfDistance.Value == "-1" || string.IsNullOrEmpty(hfDistance.Value))
                    {
                        string message = "Google Map could not be created for the entered parameters. Please be specific while providing the destination location.";
                        string script = "alert('" + message + "');";
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", script, true);
                    }
                    else if (!IsValidDate)
                    {
                        string message = " Invalid Date / Time.";
                        string script = "alert('" + message + "');";
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "alert", script, true);
                    }
                    else
                    {
                        double temp = 0, duration = 1;
                        double minMile = double.Parse(ConfigurationSettings.AppSettings["MinMile"]);
                        double.TryParse(hfDistance.Value, out temp);
                        temp = temp == 0 || temp < minMile ? minMile : temp;
                        string[] arrTemp = hfDuration.Value.Split(' ');
                        if (arrTemp.Length == 4)
                        {
                            duration = (double.Parse(arrTemp[0])) * 60 + double.Parse(arrTemp[2]);
                        }
                        else if (arrTemp.Length == 2)
                        {
                            double.TryParse(hfDuration.Value.Split(' ')[0], out duration);
                        }
                        duration = duration * 1 / 60;
                        duration = duration <= 1 ? 1 : duration <= 2 ? 2 : duration;

                        try
                        {
                            AddJobtoLimoAnyWhere();

                        }
                        catch (Exception)
                        {
                            
                        }
                       
                        LimoEntitiesEntityFremwork limoEntity = new LimoEntitiesEntityFremwork();
                        Job job = new Job();
                        job.FromAddress = txtSource.Text;// +", NEW YORK, NY, USA";
                        job.ToAddress = txtDistenation.Text;// +", NEW YORK, NY, USA";
                        job.JobDate = DateTime.Parse(txtDatepicker.Text);
                        //job.NOUsers = int.Parse(ddlNOPassenger.SelectedValue);                  
                        job.JobTime = ddlHour.SelectedItem.Text.Substring(0, 2) +
                            ddlMinutes.SelectedItem.Text + ddlHour.SelectedItem.Text.Substring(2);
                        double price = 0;
                        double.TryParse(txtTotalPrice.Text, out price);
                        job.TotalPrice = price;
                        job.FirstName = txtFirstName.Text;
                        job.LastName = txtLastName.Text;
                        job.Email = txtPassEmail.Text;
                        job.Mobile = txtPassMobile.Text;
                        string pending = Limo_Gloabel.JobStatus.Pending.ToString().ToLower();
                        DAL.JobStatu jobStatus =
                            limoEntity.JobStatus.Where(obj => obj.JobStatus.ToLower() == pending).FirstOrDefault();
                        if (jobStatus != null)
                            job.JobStatusID = jobStatus.JobStatusID;
                        job.IsDespath = true;
                        job.JobBy = 1;
                        var noPass = ddlNOPassenger.SelectedItem.Text;
                        if (noPass.StartsWith("0"))
                            noPass = noPass.Remove(0, 1);
                        job.NoPassenger = int.Parse(noPass);
                        if (CorPorateID > 0)
                            job.CorporateID = CorPorateID;
                        job.FromLat = lat;
                        job.FromLng = lng;
                        job.ToLat = latTo;
                        job.ToLng = lngTo;

                        job.Notes = txtNote.Text;

                        limoEntity.Jobs.AddObject(job);
                        limoEntity.SaveChanges();
                        job.ComapnyConfirmNum = job.JobID.ToString() + DateTime.Now.Day.ToString() +
                                                  DateTime.Now.Month.ToString() +
                                                  DateTime.Now.Year.ToString().Substring(2);

                        job.LimoConfirmNumber = "L" + job.JobID.ToString() + DateTime.Now.Day.ToString() +
                                                DateTime.Now.Month.ToString() +
                                                DateTime.Now.Year.ToString().Substring(2);
                        limoEntity.SaveChanges();

                        //Service1.LimoAllOver ccc = new Service1.LimoAllOver();
                        MobileService.LimoAllOver ccc = new MobileService.LimoAllOver();
                        ccc.GetDriversNearMe(lng, lat, "1", job.JobID);
                        Response.Redirect("~/Admin/Pages/Despatch.aspx");
                    }
                }
            }
        }

        public int CorPorateID
        {
            set { ViewState["BookEngine_CorPorateID"] = value; }
            get
            {
                if (ViewState["BookEngine_CorPorateID"] != null)
                    return (int)ViewState["BookEngine_CorPorateID"];
                else
                    return 0;
            }
        }

        private void AddJobtoLimoAnyWhere()
        {
            JobWrapper jobWrapper = new JobWrapper();
            string apiId = ConfigurationSettings.AppSettings["apiId"]; //Your Api ID
            string apiKey = ConfigurationSettings.AppSettings["apiKey"]; //Your Api Key
            //LimoEntities limmoEntity = new LimoEntities();
            //Company company = limmoEntity.Companies.Where(xx=>xx.CompanyID == jobWrapper.JobDetailsObject.CompanyID ).ToList().FirstOrDefault();
            var apiService = new ApiService();

            //LimoEntities limoEntity = new LimoEntities();
            //Company company =
            //limoEntity.Companies.Where(obj => obj.CompanyID == jobWrapper.JobDetailsObject.CompanyID).
            //    FirstOrDefault();
            //string zipCode = string.Empty;
            //if (company != null)
            //    zipCode = company.ZipCode;
            //AffiliateResponse affiliateResponse =
            //    apiService.GetAffiliates(apiId, apiKey).Affiliates.Where(obj => obj.Zip == zipCode).FirstOrDefault();
            //AffliateID = affiliateResponse != null ? affiliateResponse.Id : 0;

            VehicleTypeResponse cehicleTypeResponse = apiService.GetVehicleTypes(apiId, apiKey);
            string vehicleType = cehicleTypeResponse.VehicleTypes != null &&
                                 cehicleTypeResponse.VehicleTypes.Length > 0
                                     ? cehicleTypeResponse.VehicleTypes[0].VehTypeCode
                                     : string.Empty;
            //apiService.mylimobiz.book.ApiService laApi = new com.mylimobiz.book.ApiService();
            string[] fromAddress = txtSource.Text.Trim().Split(',');
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
            string[] toAddress = txtDistenation.Text.Trim().Split(',');
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

            double price = 0;
            double.TryParse(txtTotalPrice.Text, out price);
            //if (jobWrapper.JobDetailsObject.IsInsidePickup)
            //    carSeatInsidePickupFee += 15;
            //if (jobWrapper.JobDetailsObject.IsCarSeatAdd)
            //    carSeatInsidePickupFee += 30;
            //carSeatInsidePickupFee += jobWrapper.JobDetailsObject.JobProcessingFees;
            Ride ride = new Ride()
                            {
                                RideSource = "Limoallover",
                                RideType = "RES",
                                ServiceType = JobWrapper.ServiceTypeObject.PTP.ToString(),
                                // done
                                VehicleType = vehicleType,
                                //"SEDAN",//jobWrapper.JobDetailsObject.JobCarType.ToString(),
                                PickUpDate = DateTime.Parse(txtDatepicker.Text),
                                PickUpTime = ddlHour.SelectedValue + ddlMinutes.SelectedItem.Text,

                                NumberOfPax = int.Parse(ddlNOPassenger.SelectedItem.Text),
                                //done
                                PassengerFirstName = txtFirstName.Text,
                                //done
                                PassengerLastName = txtLastName.Text,
                                //done
                                PassengerEmail = txtPassEmail.Text,
                                // done
                                PassengerPhone = txtPassMobile.Text,
                                // done
                                BillingContact = txtFirstName.Text + " " + txtLastName.Text,
                                //done
                                Currency = "USD",
                                //done
                                //RideNumber = DateTime.Now.Ticks.ToString(),
                                SpecialChildSeat = "No", //jobWrapper.JobDetailsObject.IsCarSeatAdd ? "Toddler" : "No",
                                // done

                                RatesMapping = new RatesMapping()
                                                   {
                                                       BaseRate =
                                                           new RateFixed() { Rate = price },
                                                       PerHourRate = new RateMultiply() { RatePerUnit = 0 },
                                                       PerMileRate =
                                                           new RateMultiply() { RatePerUnit = 0, Units = 0, Total = 0 },
                                                       PerPassengerRate =
                                                           new RateMultiply() { RatePerUnit = 0, Units = 0, Total = 0 },
                                                       ExtraStops = new RateFixed() { Rate = 0 },
                                                       Discount2 = new RateFixed() { Rate = 0 },
                                                       FuelSurcharge = new RatePercentage() { PercentageAmount = 0 },
                                                       Gratuity = new RatePercentage() { PercentageAmount = 0 },
                                                       Misc1 = new RateMisc() { Rate = 0, RateName = "Tolls" },
                                                       Misc2 = new RateMisc() { Rate = 0, RateName = "Parking" },
                                                       Misc3 = new RateMisc()
                                                                   {
                                                                       Rate =   0,
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
                                                       STCSurcharge = new RatePercentage() { PercentageAmount = 0 },
                                                       Total = price
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
                                //CreditCardNumber = txtCardNumber.Text,
                                //CCExpDate = ddlExpirationMonth.SelectedValue + "/" + ddlExpirationYear.SelectedValue,
                                //CCName = txtFirstName.Text + " " + txtLastName.Text,
                                //CCState = ddlState.SelectedValue,
                                //CCBillingAddr = txtAdddress.Text,
                                //CCZip = txtZipCode.Text //,
                                //CCNotes = txtCardVerifNumber.Text
                            };
            RideResponse response = apiService.ImportReservation(apiId, apiKey, ride);
        }
    }
}