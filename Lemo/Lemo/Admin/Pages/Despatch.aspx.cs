using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lemo.Classes;
using Lemo.Admin.Helper;
using Lemo.DAL;
using System.Drawing;
using System.Text;
using Lemo.Helper;
using System.Data.Objects;

namespace Lemo.Admin.Pages
{
    public partial class Despatch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                ProcessData();
            }
            BookEngine1.IsDespatch = true;
            AdminHelper adminHelper = new AdminHelper();
            if (adminHelper.CorporateObject != null)
            {
                BookEngine1.CorPorateID = adminHelper.CorporateObject.ID;
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            GridView1.Columns[0].Visible = false;
            //GridView1.Columns[0].Visible = !IsAdmin;
            divAddDespatch.Visible = IsAdmin || (!IsAdmin && _adminHelper.CompanyObject != null && _adminHelper.CompanyObject.IsDespath.HasValue
                && _adminHelper.CompanyObject.IsDespath.Value) || (!IsAdmin && _adminHelper.CorporateObject != null);
        }

        private void ProcessData()
        {
            JobWrapper jobWrapper = new JobWrapper();
            LimoEntitiesEntityFremwork limoEntity = new LimoEntitiesEntityFremwork();
            //List<DAL.Car> JobCarsList = new List<Car>();
            // if (jobWrapper.UserNumber != 0)
            //     JobCarsList = limoEntity.Cars.Where(obj => obj.CompanyID == jobWrapper.UserNumber).ToList();
            //JobCarsList.Insert(0, new DAL.Car() { CarID = 0, CarName = "Select .." });
            //ddlStatus.DataSource = JobCarsList;
            //ddlStatus.DataTextField = "JobStatus";
            //ddlStatus.DataValueField = "JobStatusID";
            //ddlStatus.DataBind();
            //ddlStatus.SelectedValue = "0";

            List<Job> jobsList = new List<Job>();
            string doneStatus = Limo_Gloabel.JobStatus.Done.ToString().ToLower();
            var dateString = DateTime.Now.ToString("mm/dd/yyyy");
            var dateNow = DateTime.Now.Date;
            jobsList = limoEntity.Jobs.Where(obj => obj.IsDespath.HasValue && obj.IsDespath.Value && obj.JobDate.HasValue && EntityFunctions.TruncateTime(obj.JobDate.Value) >= dateNow).ToList();
            ReservationList = jobsList;
            //List<JobWrapperDespatch> jobWrapperDespatchList = new List<JobWrapperDespatch>();
            //foreach (var item in ReservationList)
            //{
            //    jobWrapperDespatchList.Add(new JobWrapperDespatch() { CarsList = JobCarsList, jobWrapperDespatch = item });
            //}            
            GridView1.DataSource = ReservationList;
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.DataSource = ReservationList;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }

        public List<Job> ReservationList
        {
            get
            {
                if (Session["ReservationList_Despatch"] != null)
                    return (List<Job>)Session["ReservationList_Despatch"];
                return new List<Job>();
            }
            set { Session["ReservationList_Despatch"] = value; }
        }

        protected void butAccept_onClick(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)sender).CommandName);
            //Response.Redirect("~/admin/Pages/Reservation.aspx?id=" + id.ToString());
            if (id != 0)
            {
                LimoEntitiesEntityFremwork limoEntities = new LimoEntitiesEntityFremwork();
                DAL.Job tempJob = limoEntities.Jobs.Where(xx => xx.JobID == id).ToList().
                                             FirstOrDefault();
                if (tempJob != null)
                {
                    tempJob.JobStatusID = (int)Limo_Gloabel.JobStatus.Accepted;
                    //tempJob.CompanyCarID = (int) ddl
                    //limoEntities.SaveChanges();
                }
            }
            Response.Redirect("~/admin/Pages/ManageReservation.aspx?id=" + id.ToString());
        }

        AdminHelper _adminHelper = new AdminHelper();
        public bool IsAdmin
        {
            get
            {
                return _adminHelper.IsAdmin;
            }
        }

        protected void butCancel_onClick(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)sender).CommandName);
            //Response.Redirect("~/admin/Pages/Reservation.aspx?id=" + id.ToString());
            if (id != 0)
            {
                LimoEntitiesEntityFremwork limoEntities = new LimoEntitiesEntityFremwork();
                DAL.Job tempJob = limoEntities.Jobs.Where(xx => xx.JobID == id).ToList().
                                             FirstOrDefault();
                if (tempJob != null)
                {
                    limoEntities.Jobs.DeleteObject(tempJob);
                    limoEntities.SaveChanges();
                }
            }
            Response.Redirect("~/admin/Pages/Despatch.aspx");
        }
    }

    class JobWrapperDespatch 
    {
        public Job jobWrapperDespatch { get; set; }
        public List<Car> CarsList { get; set; }
    }
}