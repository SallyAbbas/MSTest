using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lemo.Admin.Helper;
using Lemo.Classes;
using Lemo.DAL;
using Lemo.Helper;
using System.Text;

namespace Lemo.Admin.Controls
{
    public partial class Reservation : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                ProcessData();
        }

        private void ProcessData(DAL.Job job = null)
        {
            AdminHelper adminHelper = new AdminHelper();
            JobWrapper jobWrapper = new JobWrapper();
            if (adminHelper.AdminUser != null)
            {
                divPriceDetails.Visible = true;
            }
            else if (jobWrapper.UserNumber != 0)
            {
                divPriceDetails.Visible = false;
                ddlDrivers.Enabled = false;
                ddlStatus.Enabled = false;
            }
            int id = 0;
            int.TryParse(Request.QueryString["id"], out id);
            if (id != 0 || job != null)
            {
                LimoEntitiesEntityFremwork limoEntities = new LimoEntitiesEntityFremwork();
                JobID = id;
                DAL.Job tempJob = job ?? limoEntities.Jobs.Where(xx => xx.JobID == JobID).ToList().
                                             FirstOrDefault();
                if (tempJob != null)
                {
                    DriversList = limoEntities.Drivers.Where(xx => xx.CompanyID == tempJob.Car.CompanyID).ToList();
                    DriversList.Insert(0, new DAL.Driver() { DriverID = 0, FullName = "Select .." });
                    ddlDrivers.DataSource = DriversList;
                    ddlDrivers.DataTextField = "FullName";
                    ddlDrivers.DataValueField = "DriverID";
                    ddlDrivers.DataBind();

                    List<DAL.JobStatu> JobStatuList = limoEntities.JobStatus.Where(obj => obj.JobStatusID != (int)Limo_Gloabel.JobStatus.Pending &&
                        //obj.JobStatusID != (int)Limo_Gloabel.JobStatus.Accepted &&
                        obj.JobStatusID != (int)Limo_Gloabel.JobStatus.Rejected).ToList();
                    JobStatuList.Insert(0, new DAL.JobStatu() { JobStatusID = 0, JobStatus = "Select .." });
                    ddlStatus.DataSource = JobStatuList;
                    ddlStatus.DataTextField = "JobStatus";
                    ddlStatus.DataValueField = "JobStatusID";
                    ddlStatus.DataBind();

                    txtFirstName.Text = tempJob.FirstName;
                    txtLastName.Text = tempJob.LastName;
                    txtPH.Text = tempJob.Mobile;
                    txUserEmail.Text = tempJob.Email;

                    txtFrom.Text = tempJob.FromAddress;
                    txtTo.Text = tempJob.ToAddress;
                    txtPickUpDate.Text = tempJob.JobDate.Value.ToString("MM/dd/yyyy");
                    txtPickUpTime.Text = tempJob.JobTime;
                    txtGratuity.Text = tempJob.GratuityPrice.HasValue
                                           ? tempJob.GratuityPrice.Value.ToString()
                                           : string.Empty;
                    txtEstimatedFarePrice.Text = tempJob.EstimatedFarePrice.HasValue
                                                     ? tempJob.EstimatedFarePrice.Value.ToString()
                                                     : string.Empty;
                    txtProcessingFee.Text = tempJob.ProcessingFee.HasValue
                                                ? tempJob.ProcessingFee.Value.ToString()
                                                : string.Empty;
                    txtTaxes.Text = tempJob.Taxes.HasValue ? tempJob.Taxes.Value.ToString() : string.Empty;
                    txtOtherPrice.Text = tempJob.OtherPrice.HasValue
                                             ? tempJob.OtherPrice.Value.ToString()
                                             : string.Empty;
                    txtOtherPriceNote.Text = tempJob.PriceNote;

                    ddlDrivers.SelectedValue = tempJob.DriverID.HasValue ? tempJob.DriverID.ToString() : "0";

                    ddlStatus.SelectedValue = tempJob.JobStatusID.HasValue ? tempJob.JobStatusID.ToString() : "0";

                    if (tempJob.CorporateID.HasValue && tempJob.TotalPrice.HasValue)
                    {
                        lblEstimatedFarePrice.Text = "Total Price";
                        txtEstimatedFarePrice.Text = tempJob.TotalPrice.Value.ToString();
                        txtEstimatedFarePrice.ReadOnly = true;
                        txtGratuity.Text = "0";                        
                        txtProcessingFee.Text = "0";
                        txtTaxes.Text = "0";

                        txtGratuity.ReadOnly = true;
                        txtProcessingFee.ReadOnly = true;
                        txtTaxes.ReadOnly = true;
                        txtOtherPrice.ReadOnly = true;
                        txtOtherPriceNote.ReadOnly = true;
                    }
                }
            }
        }

        public int JobID
        {
            get
            {
                if (ViewState["JobID"] != null)
                    return (int)ViewState["JobID"];
                else
                    return 0;
            }
            set { ViewState["JobID"] = value; }
        }

        protected void butSignUp_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                LimoEntitiesEntityFremwork limoEntities = new LimoEntitiesEntityFremwork();
                DAL.Job tempJob =
                    limoEntities.Jobs.Where(xx => xx.JobID == JobID).ToList().
                        FirstOrDefault();
                if (tempJob != null)
                {
                    tempJob.FirstName = txtFirstName.Text;
                    tempJob.LastName = txtLastName.Text;
                    tempJob.Mobile = txtPH.Text;
                    tempJob.Email = txUserEmail.Text;

                    tempJob.FromAddress = txtFrom.Text;
                    tempJob.ToAddress = txtTo.Text;
                    tempJob.JobDate = DateTime.Parse(txtPickUpDate.Text);
                    tempJob.JobTime = txtPickUpTime.Text;

                    double temp;
                    tempJob.GratuityPrice = double.TryParse(txtGratuity.Text, out temp) ? temp : new double?();
                    tempJob.EstimatedFarePrice = double.TryParse(txtEstimatedFarePrice.Text, out temp) ? temp : new double?();
                    tempJob.ProcessingFee = double.TryParse(txtProcessingFee.Text, out temp) ? temp : new double?();
                    tempJob.Taxes = double.TryParse(txtTaxes.Text, out temp) ? temp : new double?();
                    tempJob.OtherPrice = double.TryParse(txtOtherPrice.Text, out temp) ? temp : new double?();
                    tempJob.PriceNote = txtOtherPriceNote.Text;
                    int driverID = int.Parse(ddlDrivers.SelectedValue);
                    tempJob.DriverID = driverID != 0 ? driverID : new int?();
                    int statusID = int.Parse(ddlStatus.SelectedValue);
                    tempJob.JobStatusID = statusID != 0 ? statusID : new int?();

                    LogTime logTime = new LogTime();
                    logTime.DriverId = tempJob.DriverID;
                    logTime.JobId = tempJob.JobID;
                    logTime.LogTime1 = DateTime.Now;
                    logTime.JobStatusId = tempJob.JobStatusID;
                    limoEntities.LogTimes.AddObject(logTime);
                    limoEntities.SaveChanges();

                    divError.Visible = false;
                    divConfirmation.Visible = true;
                    ProcessData(tempJob);
                    SendEmail(tempJob);
                    try
                    {
                        var driver = DriversList.Where(xxx => xxx.DriverID == driverID).FirstOrDefault();
                        string password = driver != null ? driver.Password : string.Empty;
                        //Service1.LimoAllOver ccc = new Service1.LimoAllOver();
                        MobileService.LimoAllOver ccc = new MobileService.LimoAllOver();
                        ccc.SendNotifcationToDriver("android", driverID);
                    }
                    catch (Exception)
                    {
                        
                    }
                }
            }
            catch (Exception)
            {
                divError.Visible = true;
                divConfirmation.Visible = false;
            }
        }

        private void SendEmail(DAL.Job job)
        {
            string userEmail, CompanyEmail;
            if (job.UserID.HasValue)
                userEmail = job.User.Email;
            else
                userEmail = job.Email;
            //send email to user
            EmailManager.SendEmail("Limoallover", CreateEmailMessageBody(job.LimoConfirmNumber, job.ComapnyConfirmNum), userEmail, true);

            CompanyEmail = job.CompanyEmail;
            //send email to company
            EmailManager.SendEmail("Limoallover", CreateEmailMessageBody(job.LimoConfirmNumber, job.ComapnyConfirmNum), CompanyEmail, true);

            //send email to admin
            EmailManager.SendEmail("Limoallover", CreateEmailMessageBody(job.LimoConfirmNumber, job.ComapnyConfirmNum), "", true, true);
        }

        private string CreateEmailMessageBody(string limoNumber,string CompanyNumber)
        {
            StringBuilder bodyMessage = new StringBuilder(); //Build Message Body   
            try
            {
                bodyMessage.Append("Dear sir,<br/>" +
                                   "We wanted to let you know that job #" + limoNumber + "has been updated.");
                bodyMessage.Append("<br/><br/><br/>Thanks, LimoAllOver.<br/> 201-585-0808");
                return bodyMessage.ToString();
            }
            catch (Exception)
            {
                return bodyMessage.ToString();
            }
        }
        public List<DAL.Driver> DriversList
        {
            get
            {
                if (Session["DriversList_Reservation"] != null)
                    return (List<DAL.Driver>)Session["DriversList_Reservation"];
                return new List<DAL.Driver>();
            }
            set { Session["DriversList_Reservation"] = value; }
        }
    }
}