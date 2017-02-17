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
    public partial class ManageCorporate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LimoEntitiesEntityFremwork dc = new LimoEntitiesEntityFremwork();
                CompanyList = dc.Corporates.OrderBy(xx => xx.Name).ToList();
                GridView1.DataSource = CompanyList;
                GridView1.DataBind();
            }
        }

        protected void butEdit_onClick(object sender, EventArgs e)
        {
            AdminHelper adminHelper = new AdminHelper();
            int id = int.Parse(((Button)sender).CommandName);
            //LimoEntities dc = new LimoEntities();
            //adminHelper.CompanyObject = dc.Companies.Where(xx => xx.CompanyID == id).FirstOrDefault();
            Response.Redirect("~/admin/Pages/AddCorporate.aspx?id=" + id.ToString());
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.DataSource = CompanyList;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }

        public List<Corporate> CompanyList
        {
            get
            {
                if (Session["CompanyList_ManageCorporate"] != null)
                    return (List<Corporate>)Session["CompanyList_ManageCorporate"];
                return new List<Corporate>();
            }
            set { Session["CompanyList_ManageCorporate"] = value; }
        }

        protected void butLoginAfflite_onClick(object sender, EventArgs e)
        {
            AdminHelper adminHelper = new AdminHelper();
            int id = int.Parse(((Button)sender).CommandName);
            var company = CompanyList.Where(xx => xx.ID == id).FirstOrDefault();
            if (company != null)
            {
                adminHelper.AdminUser = company.UserName;
                adminHelper.IsAdmin = false;
                Response.Redirect("~/admin/Pages/Despatch.aspx");
            }
        }

        protected void butDelete_onClick(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(((Button)sender).CommandName);
                //AdminHelper adminHelper = new AdminHelper();
                LimoEntitiesEntityFremwork limoEntities = new LimoEntitiesEntityFremwork();
                DAL.Corporate companyTemp = limoEntities.Corporates.Where(xx => xx.ID == id).FirstOrDefault();
                if (companyTemp != null)
                {
                    limoEntities.Corporates.DeleteObject(companyTemp);
                    limoEntities.SaveChanges();
                }
                Response.Redirect("~/admin/Pages/ManageCorporate.aspx");
            }
            catch (Exception ex)
            {
                divProblem.Visible = true;
            }
        }

        protected void butSearch_Click(object sender, EventArgs e)
        {
            var companyFilterList = CompanyList.Where(obj => obj.Phone != null && obj.Phone.Trim() == txtFilter.Text.Trim()).ToList();
            GridView1.DataSource = companyFilterList;
            GridView1.DataBind();
        }

        protected void butClear_Click(object sender, EventArgs e)
        {
            txtFilter.Text = string.Empty;
            GridView1.DataSource = CompanyList;
            GridView1.DataBind();
        }
    }
}