using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lemo.Admin.Helper;
using Lemo.Classes;
using Lemo.DAL;

namespace Lemo.Admin.Controls
{
    public partial class ManageReservation : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDatepicker.Text = DateTime.Now.ToString("MM/dd/yyyy");
                ProcessData();
            }
        }

        private void ProcessData()
        {
            JobWrapper jobWrapper = new JobWrapper();
            AdminHelper adminHelper = new AdminHelper();
            LimoEntitiesEntityFremwork limoEntity = new LimoEntitiesEntityFremwork();
            List<Job> jobsList = new List<Job>();
            string doneStatus = Limo_Gloabel.JobStatus.Done.ToString().ToLower();
            DateTime date = GetSelectedDate();
            string dateString = date.ToString("MM/dd/yyyy");
            if (adminHelper.IsAdmin)
            {
                jobsList = limoEntity.Jobs.Where(obj => obj.JobDate == date && obj.JobStatu != null && obj.JobStatu.JobStatus != null &&
                             obj.JobStatu.JobStatus.ToLower() != doneStatus && (!obj.IsDespath.HasValue || (obj.IsDespath.HasValue && !obj.IsDespath.Value))).ToList();
            }
            else if (jobWrapper.UserNumber != 0)
            {
                jobsList =
                         limoEntity.Jobs.Where(xx => xx.UserID.HasValue && xx.UserID == jobWrapper.UserNumber && xx.JobDate == date).ToList();
            }
            else
            {
                if (adminHelper.CompanyObject != null)
                {
                    jobsList =
                         limoEntity.Jobs.Where(xx => xx.JobDate == date && xx.Car.CompanyID == adminHelper.CompanyObject.CompanyID &&
                             xx.JobStatu != null && xx.JobStatu.JobStatus != null &&
                             xx.JobStatu.JobStatus.ToLower() != doneStatus && (!xx.IsDespath.HasValue || (xx.IsDespath.HasValue && !xx.IsDespath.Value))).ToList();
                }
                else if (adminHelper.CorporateObject != null) {
                    jobsList =
                             limoEntity.Jobs.Where(xx => xx.JobDate == date && xx.CorporateID == adminHelper.CorporateObject.ID &&
                                 xx.JobStatu != null && xx.JobStatu.JobStatus != null &&
                                 xx.JobStatu.JobStatus.ToLower() != doneStatus && (!xx.IsDespath.HasValue || (xx.IsDespath.HasValue && !xx.IsDespath.Value))).ToList();
                }
            }
            //foreach (var job in jobsList)
            //{
            //    job.CompanyName = job.Car.Company.CompanyName;
            //}
            ReservationList = jobsList;
            GridView1.DataSource = ReservationList;
            GridView1.DataBind();
        }

        protected void butEdit_onClick(object sender, EventArgs e)
        {
            JobWrapper jobWrapper = new JobWrapper();
            AdminHelper adminHelper = new AdminHelper();
            int id = int.Parse(((Button)sender).CommandName);
            if (adminHelper.AdminUser != null)
            {
                Response.Redirect("~/admin/Pages/Reservation.aspx?id=" + id.ToString());
            }
            else if (jobWrapper.UserNumber != 0)
            {
                Response.Redirect("~/Pages/MyReservation.aspx?id=" + id.ToString());
            }
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
                if (Session["ReservationList_ManageReservation"] != null)
                    return (List<Job>)Session["ReservationList_ManageReservation"];
                return new List<Job>();
            }
            set { Session["ReservationList_ManageReservation"] = value; }
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
                    tempJob.JobStatusID = (int) Limo_Gloabel.JobStatus.Accepted;
                    limoEntities.SaveChanges();
                }
            }
            Response.Redirect("~/admin/Pages/ManageReservation.aspx?id=" + id.ToString());
        }

        protected void butReject_onClick(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)sender).CommandName);
            if (id != 0)
            {
                LimoEntitiesEntityFremwork limoEntities = new LimoEntitiesEntityFremwork();
                DAL.Job tempJob = limoEntities.Jobs.Where(xx => xx.JobID == id).ToList().
                                             FirstOrDefault();
                if (tempJob != null)
                {
                    tempJob.JobStatusID = (int)Limo_Gloabel.JobStatus.Rejected;
                    limoEntities.SaveChanges();
                }
            }
            Response.Redirect("~/admin/Pages/ManageReservation.aspx?id=" + id.ToString());
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DAL.Job job = (DAL.Job)e.Row.DataItem;
                if (job != null)
                {
                    if (job.JobStatu != null && job.JobStatu.JobStatus.ToLower() != Limo_Gloabel.JobStatus.Pending.ToString().ToLower())
                    {
                        var actionCell = e.Row.Cells[0];
                        var accept = actionCell.FindControl("butAccept");
                        if (accept != null) accept.Visible = false;
                        var reject = actionCell.FindControl("butReject");
                        if (reject != null) reject.Visible = false;
                        if (job.JobStatu != null && job.JobStatu.JobStatus.ToLower() == Limo_Gloabel.JobStatus.Rejected.ToString().ToLower())
                        {
                            actionCell.BackColor = Color.Red;
                            AdminHelper adminHelper = new AdminHelper();
                            if (!adminHelper.IsAdmin)
                            {
                                var edit = e.Row.Cells[1].FindControl("butEdit");
                                if (edit != null) edit.Visible = false;
                            }
                        }
                        else
                            actionCell.BackColor = Color.Green;

                        if (!string.IsNullOrEmpty(job.JobStatu.color))
                        {
                            Color jobStatusColor = ColorTranslator.FromHtml(job.JobStatu.color);
                            var statusColor = e.Row.Cells[5];
                            statusColor.BackColor = jobStatusColor;
                            e.Row.BackColor = jobStatusColor;
                        }
                    }
                    else
                    {
                        AdminHelper adminHelper = new AdminHelper();
                        if (!adminHelper.IsAdmin)
                        {
                            var edit = e.Row.Cells[1].FindControl("butEdit");
                            if (edit != null) edit.Visible = false;
                        }
                    }
                    e.Row.Cells[10].ToolTip = "Email: " + job.CompanyEmail + "\n Phone: " + job.CompanyPhone;

                    e.Row.Cells[9].ToolTip = "Email: " + job.DriverEmail + "\n Phone: " + job.DriverPhone;

                    e.Row.Cells[6].ToolTip = "Email: " + job.PassengerEmail + "\n Phone: " + job.PassengerPhone;
                }
            }
        }

        protected void txtDatepicker_TextChanged(object sender, EventArgs e)
        {
            ProcessData();
        }

        private DateTime GetSelectedDate()
        {
            if (string.IsNullOrEmpty(txtDatepicker.Text))
            {
                txtDatepicker.Text = DateTime.Now.ToString("MM/dd/yyyy");
            }
            string dateString = txtDatepicker.Text;
            if (!string.IsNullOrEmpty(dateString))
            {
                string[] dateArray = dateString.Split('/');
                int year = int.Parse(dateArray[2].Length > 4 ? dateArray[2].Substring(0, 4) : dateArray[2]);
                int month = int.Parse(dateArray[0]);
                int day = int.Parse(dateArray[1]);
                return new DateTime(year, month, day);
            }
            return DateTime.Now;
        }
    }
}