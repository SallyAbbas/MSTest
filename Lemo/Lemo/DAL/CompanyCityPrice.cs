using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lemo.DAL
{
    public partial class CompanyCityPrice
    {
        private string _stateName;

        public string StateName
        {
            get { return _stateName; }
            set { _stateName = value; }
        }

        private string _cityName;

        public string CityName
        {
            get { return _cityName; }
            set { _cityName = value; }
        }
    }
}