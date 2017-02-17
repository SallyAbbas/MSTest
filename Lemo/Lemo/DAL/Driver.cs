using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lemo.DAL
{
    public partial class Driver
    {
        private string _fullName;
        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(_fullName))
                    return this.FName + " " + this.LName;
                return _fullName;
            }
            set { _fullName = value; }
        }
    }
}