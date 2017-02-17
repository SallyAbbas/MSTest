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
    public partial class ManageCars : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AdminHelper adminHelper = new AdminHelper();
                if (adminHelper.CompanyObject != null)
                {
                    LimoEntitiesEntityFremwork dc = new LimoEntitiesEntityFremwork();
                    CarsList = dc.Cars.Where(xx => xx.CompanyID == adminHelper.CompanyObject.CompanyID).OrderBy(yyy=>yyy.CarName).ToList();
                    GridView1.DataSource = CarsList;
                    GridView1.DataBind();
                }
            }
        }

        protected void butEdit_onClick123(object sender, EventArgs e)
        {
            AdminHelper adminHelper = new AdminHelper();
            int id = int.Parse(((Button) sender).CommandName);
            Response.Redirect("~/admin/Pages/EditCar.aspx?id=" + id.ToString());
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.DataSource = CarsList;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }

        public List<DAL.Car> CarsList
        {
            get
            {
                if (Session["CarsList_ManageCars"] != null)
                    return (List<DAL.Car>)Session["CarsList_ManageCars"];
                return new List<DAL.Car>();
            }
            set { Session["CarsList_ManageCars"] = value; }
        }

        protected void butAddNewcar_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/admin/Pages/AddCar.aspx");
        }

        protected void butDelete_onClick(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(((Button)sender).CommandName);
                //AdminHelper adminHelper = new AdminHelper();
                LimoEntitiesEntityFremwork limoEntities = new LimoEntitiesEntityFremwork();
                DAL.Car carTemp = limoEntities.Cars.Where(xx => xx.CarID == id).FirstOrDefault();
                if (carTemp != null)
                {
                    limoEntities.Cars.DeleteObject(carTemp);
                    limoEntities.SaveChanges();
                }
                Response.Redirect("~/admin/Pages/ManageCars.aspx");
            }
            catch (Exception ex)
            {
                divProblem.Visible = true;
            }
        }
    }
}