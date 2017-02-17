using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lemo.Classes;
using Lemo.DAL;

namespace Lemo.Controls
{
    public partial class FeesDetais : System.Web.UI.UserControl
    {
        static double _total = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            JobWrapper jobWrapper = new JobWrapper();
            if (jobWrapper.JobDetailsObject != null)
            {
                tdFrom.InnerHtml = "<span style='font-weight: bold;color:#49BDFA;font-size:26px;'>From:</span> <br />" + jobWrapper.JobDetailsObject.FromAddress;
                tdTo.InnerHtml = "<span style='font-weight: bold;color:#49BDFA;font-size:26px;'>To:</span> <br />" + jobWrapper.JobDetailsObject.ToAddress;

                int? id = jobWrapper.JobDetailsObject.CompanyID;
                double price = jobWrapper.JobDetailsObject.JobBasePrise;               
                double processingFees = 3;
                double stateTaxes = 0; // = Math.Round((price * 7 / 100), 2);//Nj state Taxes : 7%
                //Ne state taxes
                //double localTxes = Math.Round((price * 0 / 100), 2);//local taxes will be added if applied
                //double driversWorker = Math.Round((price * 3.5 / 100), 2);
                //double tolls = 6.5;
                double gratuity = 0;
                if(id.HasValue)
                {
                    LimoEntitiesEntityFremwork limoEntities = new LimoEntitiesEntityFremwork();
                    Company company =
                        limoEntities.Companies.Where(xxx => xxx.CompanyID == id.Value).FirstOrDefault();
                    if (company != null)
                    {
                        double tax = 0;
                        tax += company.State_Tax.HasValue ? company.State_Tax.Value : 0;
                        tax += company.City_Tax.HasValue ? company.City_Tax.Value : 0;
                        tax += company.OtherTax1.HasValue ? company.OtherTax1.Value : 0;
                        tax += company.OtherTax2.HasValue ? company.OtherTax2.Value : 0;
                        //tax += company.OtherTax3.HasValue ? company.OtherTax3.Value : 0;
                        stateTaxes = Math.Round((price * tax / 100), 2);
                        double gratuityPersentage = company.Gratuity.HasValue ? company.Gratuity.Value : 0;
                        gratuity = Math.Round((price * gratuityPersentage / 100), 2);
                    }
                }               
                jobWrapper.JobDetailsObject.JobGratuity = gratuity;
                jobWrapper.JobDetailsObject.JobProcessingFees = processingFees;
                jobWrapper.JobDetailsObject.JobStateTaxes = stateTaxes;
                //JobWrapper.JobDetailsObject.JobLocalTxes = localTxes;
                //JobWrapper.JobDetailsObject.JobDriversWorker = driversWorker;
                //JobWrapper.JobDetailsObject.JobTolls = tolls;


                _total = price + gratuity + processingFees + stateTaxes; // +localTxes + driversWorker;// +tolls;
                _total = Math.Round(_total, 2);
                tdBaseFees.InnerText = "$ " + price;
                tdGratuity.InnerText = "$ " + gratuity;
                tdProcessingFees.InnerText = "$ " + processingFees;
                tdStateTaxes.InnerText = "$ " + stateTaxes;
                //tdLocalTxes.InnerText = "$ " + localTxes;
                //tdDriversWorker.InnerText = "$ " + driversWorker;
                //tdTolls.InnerText = "$ " + tolls;
                tdTaxes.InnerText = "$ " + stateTaxes; // (stateTaxes + localTxes + driversWorker).ToString();
                string script = "var Total =" + _total + ";";
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "script", script, true);

                if (IsReadOnly)
                {
                    butPayment.Visible = false;
                    ImagebutPayment.Visible = false;
                    imgButCoentWithCorporat.Visible = false;
                    cbCarSeat.Visible = false;
                    lblCarSeat.Visible = jobWrapper.JobDetailsObject.IsCarSeatAdd;
                    cbInsidePickup.Visible = false;
                    lblInsidePickup.Visible = jobWrapper.JobDetailsObject.IsInsidePickup;
                    _total = jobWrapper.JobDetailsObject.JobTotalPrise;
                }
                lblTotal.Text = "$ " + _total;
            }       
        }

        protected void butPayment_Click(object sender, EventArgs e)
        {
            JobWrapper jobWrapper = new JobWrapper();
            jobWrapper.JobDetailsObject.JobTotalPrise = int.Parse(hfCarSeatValue.Value) + _total + int.Parse(hfInsidePickup.Value);
            jobWrapper.JobDetailsObject.IsCarSeatAdd = cbCarSeat.Checked;
            jobWrapper.JobDetailsObject.IsInsidePickup = cbInsidePickup.Checked;
            Response.Redirect("~/Pages/Payment.aspx");
        }

        public bool IsReadOnly
        {
            get;
            set;
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            JobWrapper jobWrapper = new JobWrapper();
            jobWrapper.JobDetailsObject.JobTotalPrise = int.Parse(hfCarSeatValue.Value) + _total + int.Parse(hfInsidePickup.Value);
            jobWrapper.JobDetailsObject.IsCarSeatAdd = cbCarSeat.Checked;
            jobWrapper.JobDetailsObject.IsInsidePickup = cbInsidePickup.Checked;
            Response.Redirect("~/Pages/Payment.aspx");
        }

        protected void imgButCoentWithCorporat_Click(object sender, ImageClickEventArgs e)
        {
            JobWrapper jobWrapper = new JobWrapper();
            jobWrapper.JobDetailsObject.JobTotalPrise = int.Parse(hfCarSeatValue.Value) + _total + int.Parse(hfInsidePickup.Value);
            jobWrapper.JobDetailsObject.IsCarSeatAdd = cbCarSeat.Checked;
            jobWrapper.JobDetailsObject.IsInsidePickup = cbInsidePickup.Checked;
            Response.Redirect("~/Pages/CorporatePayment.aspx");
        }
    }
}