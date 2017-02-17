using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lemo.Classes;

namespace Lemo.Controls
{
    public partial class ResevationInformation : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JobWrapper jobWrapper = new JobWrapper();
            if (jobWrapper.JobDetailsObject != null)
            {
                lblFrom.Text = jobWrapper.JobDetailsObject.FromAddress;
                lblDate.Text = jobWrapper.JobDetailsObject.JobDate.Date.ToString("MM/dd/yyyy");  //.Split(' ')[0];
                lblTo.Text = jobWrapper.JobDetailsObject.ToAddress;
                lblTime.Text = jobWrapper.JobDetailsObject.JobTime;
                lblNOPassenger.Text = jobWrapper.JobDetailsObject.UserNumber;
                lblDuration.Text = jobWrapper.JobDetailsObject.JobDurationPerMinuts;
            }
        }
    }
}