using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lemo.DAL
{
    public partial class Job
    {
        //private string _companyName;
        public string CompanyName
        {
            get
            {
                if (this.Car != null && this.Car.Company != null)
                {
                    return this.Car.Company.CompanyName;
                }
                return string.Empty;
            }
            //set { _companyName = value; }
        }

        //private string _companyPhone;
        public string CompanyPhone
        {
            get
            {
                if(this.Car != null && this.Car.Company != null)
                {
                    return this.Car.Company.Phone;
                }
                return string.Empty;
            }
            //set { _companyPhone = value; }
        }

        public string CompanyEmail
        {
            get
            {
                if (this.Car != null && this.Car.Company != null)
                {
                    return this.Car.Company.Email;
                }
                return string.Empty;
            }
            //set { _companyPhone = value; }
        }

        //private string _driverPhone;
        public string DriverPhone
        {
            get { return this.Driver != null ? this.Driver.Phone : string.Empty; }
            //set { _driverPhone = value; }
        }

        //private string _driverFullName;
        public string DriverFullName
        {
            get { return this.Driver != null? this.Driver.FName + " " + this.Driver.LName: string.Empty; }
            //set { _driverFullName = value; }
        }

        public string DriverEmail
        {
            get { return this.Driver != null ? this.Driver.Email : string.Empty; }
            //set { _driverFullName = value; }
        }

        public string PassengerFullName
        {
            get
            {
                if (this.User != null)
                {
                    return this.User.FirstName + " " + this.User.LastName;
                }
                else
                {
                    return this.FirstName + " " + this.LastName;
                }
            }
        }

        public string PassengerPhone
        {
            get
            {
                if (this.User != null)
                {
                    return this.User.Mobile;
                }
                else
                {
                    return this.Mobile;
                }
            }
        }

        public string PassengerEmail
        {
            get
            {
                if (this.User != null)
                {
                    return this.User.Email;
                }
                else
                {
                    return this.Email;
                }
            }
        }

        public string Status
        {
            get
            {
                if (this.JobStatu != null)
                {
                    return this.JobStatu.JobStatus;
                }
                return string.Empty;
            }
        }
    }
}