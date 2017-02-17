using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lemo.DAL
{
    public partial class Car
    {
        string _companyName = string.Empty;
        public string CompanyName
        {
            get { return this.Company.CompanyName; }
            set { _companyName = value; }
        }

        private double _totalPrice;
        public double TotalPrice
        {
            get { return _totalPrice; }
            set { _totalPrice = value; }
        }

        public string CarNameWithNumber
        {
            get
            {
                string carNumberTemp = string.Empty;
                if(!string.IsNullOrEmpty(this.CarNumber))
                    carNumberTemp = " [" + this.CarNumber + "]";
                return this.CarName + carNumberTemp;
            }
        }
    }
}