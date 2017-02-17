using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lemo.DAL;

namespace Lemo.Admin.Helper
{
    public class AdminHelper
    {
        public string AdminUser
        {
            get
            {
                if (HttpContext.Current.Session["AdminUser"] != null)
                    return HttpContext.Current.Session["AdminUser"].ToString();
                else
                    return null;
            }
            set { HttpContext.Current.Session["AdminUser"] = value; }
        }

        public bool IsAdmin
        {
            get
            {
                if (HttpContext.Current.Session["IsAdmin"] != null)
                    return (bool)HttpContext.Current.Session["IsAdmin"];
                else
                    return false;
            }
            set { HttpContext.Current.Session["IsAdmin"] = value; }
        }

        public Company CompanyObject
        {
            get
            {
                if (HttpContext.Current.Session["CompanyObject"] != null)
                    return (Company)HttpContext.Current.Session["CompanyObject"];
                else
                    return null;
            }
            set { HttpContext.Current.Session["CompanyObject"] = value; }
        }

        //public bool IsEdit
        //{
        //    get
        //    {
        //        if (HttpContext.Current.Session["IsEdit"] != null)
        //            return (bool)HttpContext.Current.Session["IsEdit"];
        //        else
        //            return false;
        //    }
        //    set { HttpContext.Current.Session["IsEdit"] = value; }
        //}

        //public int ContactPersonID
        //{
        //    get
        //    {
        //        if (HttpContext.Current.Session["ContactPersonID"] != null)
        //            return (int)HttpContext.Current.Session["ContactPersonID"];
        //        else
        //            return 0;
        //    }
        //    set { HttpContext.Current.Session["ContactPersonID"] = value; }
        //}

        //public int CarID
        //{
        //    get
        //    {
        //        if (HttpContext.Current.Session["CarID"] != null)
        //            return (int)HttpContext.Current.Session["CarID"];
        //        else
        //            return 0;
        //    }
        //    set { HttpContext.Current.Session["CarID"] = value; }
        //}

        public Corporate CorporateObject
        {
            get
            {
                if (HttpContext.Current.Session["CorporateObject"] != null)
                    return (Corporate)HttpContext.Current.Session["CorporateObject"];
                else
                    return null;
            }
            set { HttpContext.Current.Session["CorporateObject"] = value; }
        }
    }
}