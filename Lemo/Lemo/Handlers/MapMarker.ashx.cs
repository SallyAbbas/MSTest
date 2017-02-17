using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lemo.DAL;
using Lemo.Classes;

namespace Lemo.Handlers
{
    /// <summary>
    /// Summary description for MapMarker
    /// </summary>
    public class MapMarker : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/html";
            string noPassengerString = context.Request.QueryString["NOPassenger"];
            //JobWrapper jobWrapper = new JobWrapper();
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
            int noPassenger = 0; // jobWrapper.JobDetailsObject.UserNumberINT;
            if (!string.IsNullOrEmpty(noPassengerString))
                int.TryParse(noPassengerString, out noPassenger);
            string companyName = string.Empty;
            string carNumber = string.Empty;
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
            
            context.Response.Write( driverLocationLatLan);
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}