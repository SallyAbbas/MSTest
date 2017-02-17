using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lemo.DAL;
using Lemo.Admin.Helper;

namespace Lemo.Admin.Pages
{
    public partial class ManageDrivers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AdminHelper adminHelper = new AdminHelper();
                if (adminHelper.CompanyObject != null)
                {
                    LimoEntitiesEntityFremwork dc = new LimoEntitiesEntityFremwork();
                    DriversList = dc.Drivers.Where(xx => xx.CompanyID == adminHelper.CompanyObject.CompanyID).ToList();
                    if (DriversList.Count > 0)
                        DriversList = DriversList.OrderBy(yyy => yyy.FullName).ToList();
                    foreach (var driver in DriversList)
                    {
                        if (!driver.ShowOnMap.HasValue)
                            driver.ShowOnMap = false;
                    }
                    GridView1.DataSource = DriversList;
                    GridView1.DataBind();
                }
            }
        }

        protected void butEdit_onClick(object sender, EventArgs e)
        {
            AdminHelper adminHelper = new AdminHelper();
            int id = int.Parse(((Button)sender).CommandName);
            Response.Redirect("~/admin/Pages/Driver.aspx?id=" + id.ToString());
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.DataSource = DriversList;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }

        public List<DAL.Driver> DriversList
        {
            get
            {
                if (Session["DriversList_ManageDrivers"] != null)
                    return (List<DAL.Driver>)Session["DriversList_ManageDrivers"];
                return new List<DAL.Driver>();
            }
            set { Session["DriversList_ManageDrivers"] = value; }
        }

        protected void butSignUp_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/admin/Pages/Driver.aspx");
        }
    }
}