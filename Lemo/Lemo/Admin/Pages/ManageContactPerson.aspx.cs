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
    public partial class ManageContactPerson : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AdminHelper adminHelper = new AdminHelper();
                if (adminHelper.CompanyObject != null)
                {
                    LimoEntitiesEntityFremwork dc = new LimoEntitiesEntityFremwork();
                    ContactPersonList = dc.ContactPersons.Where(xx => xx.CompanyID == adminHelper.CompanyObject.CompanyID).OrderBy(yyy=>yyy.FirstName).ToList();
                    GridView1.DataSource = ContactPersonList;
                    GridView1.DataBind();
                }
            }
        }

        protected void butEdit_onClick(object sender, EventArgs e)
        {
            AdminHelper adminHelper = new AdminHelper();
            int id = int.Parse(((Button) sender).CommandName);
            Response.Redirect("~/admin/Pages/EditContactPerson.aspx?id=" + id.ToString());
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.DataSource = ContactPersonList;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }

        public List<ContactPerson> ContactPersonList
        {
            get
            {
                if (Session["ContactPersonList_ManageContactPerson"] != null)
                    return (List<ContactPerson>)Session["ContactPersonList_ManageContactPerson"];
                return new List<ContactPerson>();
            }
            set { Session["ContactPersonList_ManageContactPerson"] = value; }
        }

        protected void butSignUp_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/admin/Pages/AddContactPerspn.aspx");
        }
    }
}