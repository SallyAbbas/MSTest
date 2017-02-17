using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lemo.Admin.Helper;
using Lemo.DAL;

namespace Lemo.Admin.Pages
{
    public partial class ManageCompany : BaseSuperAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LimoEntitiesEntityFremwork dc = new LimoEntitiesEntityFremwork();
                CompanyList = dc.Companies.OrderBy(xx => xx.CompanyName).ToList();
                GridView1.DataSource = CompanyList;
                GridView1.DataBind();
            }
        }

        protected void butEdit_onClick(object sender, EventArgs e)
        {
            AdminHelper adminHelper = new AdminHelper();
            int id = int.Parse(((Button) sender).CommandName);
            //LimoEntities dc = new LimoEntities();
            //adminHelper.CompanyObject = dc.Companies.Where(xx => xx.CompanyID == id).FirstOrDefault();
            Response.Redirect("~/admin/Pages/CompanyProfile.aspx?id=" + id.ToString());
        }
        
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.DataSource = CompanyList;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }

        public List<Company> CompanyList
        {
            get
            {
                if (Session["CompanyList_ManageCompany"] != null)
                    return (List<Company>)Session["CompanyList_ManageCompany"];
                return new List<Company>();
            }
            set { Session["CompanyList_ManageCompany"] = value; }
        }

        protected void butLoginAfflite_onClick(object sender, EventArgs e)
        {
            AdminHelper adminHelper = new AdminHelper();
            int id = int.Parse(((Button)sender).CommandName);
            var company = CompanyList.Where(xx => xx.CompanyID == id).FirstOrDefault();
            if (company != null)
            {
                adminHelper.CompanyObject = company;
                if (adminHelper.CompanyObject != null)
                {
                    adminHelper.AdminUser = adminHelper.CompanyObject.UserName;
                    adminHelper.IsAdmin = false;
                    Response.Redirect("~/admin/Pages/CompanyProfile.aspx");
                }
            }
        }

        protected void butDelete_onClick(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(((Button)sender).CommandName);
                //AdminHelper adminHelper = new AdminHelper();
                LimoEntitiesEntityFremwork limoEntities = new LimoEntitiesEntityFremwork();
                DAL.Company companyTemp = limoEntities.Companies.Where(xx => xx.CompanyID == id).FirstOrDefault();
                if (companyTemp != null)
                {
                    limoEntities.Companies.DeleteObject(companyTemp);
                    limoEntities.SaveChanges();
                }
                Response.Redirect("~/admin/Pages/ManageCompany.aspx");
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