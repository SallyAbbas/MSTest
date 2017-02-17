using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lemo.DAL;
using Lemo.Classes;

namespace Lemo.Controls
{
    public partial class MapForLiveReservation : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JobWrapper jobWrapper = new JobWrapper();
            if (jobWrapper.JobDetailsObject == null)
                Response.Redirect("~/default.aspx");
            else
            {

            }
            //ToDo all driver have avaliable status 
            //if (!IsPostBack)
            //{
                string driverLocationLatLan = string.Empty;
                LimoEntitiesEntityFremwork limoEntities = new LimoEntitiesEntityFremwork();
                List<Car> carList = limoEntities.Cars.ToList();
                Car car;
                List<DriverLocation> driverLocationsListq =
                   limoEntities.DriverLocations.ToList();
                int x = driverLocationsListq.Count;
                List<DriverLocation> driverLocationsList =
                    limoEntities.DriverLocations.Where(xxx => xxx.Driver.ShowOnMap.HasValue && xxx.Driver.ShowOnMap.Value).ToList();
                string photoNames = string.Empty;
                string isAvailable = "false";
                int noPassenger = jobWrapper.JobDetailsObject.UserNumberINT;
                string companyName = string.Empty;
                string carNumber = string.Empty;
                NOPassenger = noPassenger.ToString();
                foreach (var driverLocation in driverLocationsList)
                {
                    car = carList.Where(xxx => driverLocation.Driver.CarID.HasValue && xxx.CarID == driverLocation.Driver.CarID.Value).FirstOrDefault();
                    if (car != null && car.NoPassengers >= noPassenger)
                    {
                        photoNames = car.PhotoName;
                        isAvailable = driverLocation.Driver.IsAvailable.HasValue
                                          ? driverLocation.Driver.IsAvailable.Value.ToString().ToLower()
                                          : "false";
                        if (car.CompanyName != null)
                            companyName = car.CompanyName;
                        else
                            companyName = string.Empty;
                        if (!string.IsNullOrEmpty(car.CarNumber))
                            carNumber = car.CarNumber;
                        else
                            carNumber = string.Empty;
                        driverLocationLatLan += driverLocation.Lat + "_" + driverLocation.Lng + "_" + photoNames + "_" + car.CarID + "_" + isAvailable +
                           "_" + "Operator by " + companyName + "_" + carNumber + ",";
                    }
                }
                if (driverLocationLatLan.Length > 0)
                    DriverLocationLatLan = driverLocationLatLan.Remove(driverLocationLatLan.Length - 1);
            //}
        }

        protected string DriverLocationLatLan
        {
            get
            {
                if (ViewState["MapForLiveReservation_DriverLocationLatLan"] == null) 
                    return string.Empty;
                return ViewState["MapForLiveReservation_DriverLocationLatLan"].ToString();
            }
            set { ViewState["MapForLiveReservation_DriverLocationLatLan"] = value; }
        }

        protected string FromAddress
        {
            get
            {
                JobWrapper jobWrapper = new JobWrapper();
                if (jobWrapper.JobDetailsObject != null)
                    return jobWrapper.JobDetailsObject.FromAddress;
                else
                    return string.Empty;
            }
        }

        void ProcessData()
        {
            //int id = int.Parse(((Button)sender).CommandName);
            ////int id = int.Parse(hfCarID.Value);
            //DAL.Car car = CarsList.Where(xx => xx.CarID == id).FirstOrDefault();
            //JobWrapper jobWrapper = new JobWrapper();
            //jobWrapper.JobDetailsObject.JobCompanyName = car.CompanyName;
            //jobWrapper.JobDetailsObject.JobCarType = car.CarName;
            //jobWrapper.JobDetailsObject.JobBasePrise = car.TotalPrice;
            //jobWrapper.JobDetailsObject.CarID = car.CarID;
            //jobWrapper.JobDetailsObject.CompanyID = car.CompanyID;
            //Response.Redirect("~/Pages/CompleteReservation.aspx");
        }

        protected string NOPassenger
        {
            get
            {
                if (ViewState["MapForLiveReservation_NOPassenger"] == null)
                    return string.Empty;
                return ViewState["MapForLiveReservation_NOPassenger"].ToString();
            }
            set { ViewState["MapForLiveReservation_NOPassenger"] = value; }
        }
    }
}