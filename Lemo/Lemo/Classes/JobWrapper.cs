using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lemo.Classes
{
    public class JobWrapper
    {
        //public static double ConverttoMile = 0.621371;
        public JobObjectDetails JobDetailsObject
        {
            get
            {
                if (HttpContext.Current.Session != null && HttpContext.Current.Session["JobWrapper"] != null)
                    return (JobObjectDetails)HttpContext.Current.Session["JobWrapper"];
                return null;
            }
            set 
            {
                HttpContext.Current.Session["JobWrapper"] = value;
            }
        }

        public int UserNumber
        {
            get {
                if (HttpContext.Current.Session["AcctNumber"] != null)
                    return (int)HttpContext.Current.Session["AcctNumber"];
                else
                    return 0;
            }
            set { HttpContext.Current.Session["AcctNumber"] = value; }
        }

        public bool ISReservationExist
        {
            get
            {
                if (HttpContext.Current.Session["ISReservationExist"] != null)
                    return (bool)HttpContext.Current.Session["ISReservationExist"];
                else
                    return false;
            }
            set { HttpContext.Current.Session["ISReservationExist"] = value; }
        }

        public enum ServiceTypeObject
        {
            PTP
        }

        public enum VehicleTypeObject
        {
            SEDAN,
            SUV
        }

        public int CorporateNumber
        {
            get
            {
                if (HttpContext.Current.Session["CorporateNumber"] != null)
                    return (int)HttpContext.Current.Session["CorporateNumber"];
                else
                    return 0;
            }
            set { HttpContext.Current.Session["CorporateNumber"] = value; }
        }
    }

    public class JobObjectDetails 
    {
        public string FromAddress
        { get; set; }

        public string ToAddress
        { get; set; }

        public double Distance
        { get; set; }

        public string UserNumber
        { get; set; }

        public string JobHours
        { get; set; }

        public string JobMinuts
        { get; set; }

        public string JobTime
        { get; set; }

        public DateTime JobDate
        { get; set; }

        public double JobDurationPerHours
        { get; set; }

        public string JobDurationPerMinuts
        { get; set; }

        public double JobBasePrise
        { get; set; }

        public string JobCompanyName
        { get; set; }

        public string JobCarType
        { get; set; }

        public double JobTotalPrise
        { get; set; }

        public bool IsCarSeatAdd
        { get; set; }

        public bool IsInsidePickup
        { get; set; }

        public double JobGratuity
        { get; set; }
        public double JobProcessingFees
        { get; set; }
        public double JobStateTaxes
        { get; set; }
        public double JobLocalTxes
        { get; set; }
        public double JobDriversWorker
        { get; set; }
        public double JobTolls
        { get; set; }

        public Lemo.Classes.JobWrapper.VehicleTypeObject VehicleTypeObj
        { get; set; }

        public Lemo.Classes.JobWrapper.ServiceTypeObject ServiceTypeObj
        { get; set; }

        public string JobMappedTime
        { get; set; }

        public int UserNumberINT { get; set; }

        public int CarID { get; set; }

        public int? CompanyID { get; set; }

        public int? userID { get; set; }

        public int? cityID { get; set; }

        public string FromAddressLat
        { get; set; }

        public string ToAddressLat
        { get; set; }

        public string FromAddressLNG
        { get; set; }

        public string ToAddressLNG
        { get; set; }
    }
}