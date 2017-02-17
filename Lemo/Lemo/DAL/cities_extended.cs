using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects.DataClasses;

namespace Lemo.DAL
{
    public partial class cities_extended 
    {
        public string CityStateName
        {
            get { return this.CityName + ", " + this.state_code; }
        }
    }
}