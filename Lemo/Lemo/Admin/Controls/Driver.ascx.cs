using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lemo.Admin.Helper;
using Lemo.DAL;

namespace Lemo.Admin.Controls
{
    public partial class Driver : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AdminHelper adminHelper = new AdminHelper();
                LimoEntitiesEntityFremwork limoEntities = new LimoEntitiesEntityFremwork();

                var carsList = limoEntities.Cars.Where(xx => xx.CompanyID == adminHelper.CompanyObject.CompanyID).ToList();
                carsList.Insert(0, new DAL.Car() { CarID = 0, CarName = "Select .." });
                ddlCars.DataSource = carsList;
                ddlCars.DataTextField = "CarNameWithNumber";
                ddlCars.DataValueField = "CarID";
                ddlCars.DataBind();

                int id = 0;
                int.TryParse(Request.QueryString["id"], out id);
                if (id != 0)
                {
                    DriverID = id;
                    DAL.Driver driverTemp =
                        limoEntities.Drivers.Where(xx => xx.DriverID == DriverID).ToList().FirstOrDefault();
                    if (driverTemp != null)
                    {
                        txtFirstName.Text = driverTemp.FName;
                        txtLastName.Text = driverTemp.LName;
                        txtEmail.Text = driverTemp.Email;
                        txtMobilePhone.Text = driverTemp.Phone;
                        cbShowOnMap.Checked = driverTemp.ShowOnMap.HasValue ? driverTemp.ShowOnMap.Value : false;
                        txtPassword.Text = driverTemp.Password;
                        ddlCars.SelectedValue = driverTemp.CarID.HasValue ? driverTemp.CarID.Value.ToString() : "0";
                    }
                }
            }
        }

        protected void butSignUp_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                AdminHelper adminHelper = new AdminHelper();
                LimoEntitiesEntityFremwork limoEntities = new LimoEntitiesEntityFremwork();
                if (DriverID == 0)
                {
                    DAL.Driver driver = new DAL.Driver();
                    driver.FName = txtFirstName.Text;
                    driver.LName = txtLastName.Text;
                    driver.Email = txtEmail.Text;
                    driver.Phone = txtMobilePhone.Text.Trim();
                    driver.CompanyID = adminHelper.CompanyObject.CompanyID;
                    driver.ShowOnMap = cbShowOnMap.Checked;
                    driver.Password = txtPassword.Text.Trim();
                    int carID = int.Parse(ddlCars.SelectedValue);
                    driver.CarID = carID != 0 ? carID : new int?();
                    limoEntities.Drivers.AddObject(driver);
                    limoEntities.SaveChanges();
                    Response.Redirect("~/admin/Pages/ManageDrivers.aspx");
                }
                else
                {
                    DAL.Driver contactTemp =
                        limoEntities.Drivers.Where(xx => xx.DriverID == DriverID).ToList().
                            FirstOrDefault();
                    if (contactTemp != null)
                    {
                        contactTemp.FName = txtFirstName.Text;
                        contactTemp.LName = txtLastName.Text;
                        contactTemp.Email = txtEmail.Text;
                        contactTemp.Phone = txtMobilePhone.Text.Trim();
                        contactTemp.ShowOnMap = cbShowOnMap.Checked;
                        contactTemp.Password = txtPassword.Text.Trim();
                        int carID = int.Parse(ddlCars.SelectedValue);
                        contactTemp.CarID = carID != 0 ? carID : new int?();
                        limoEntities.SaveChanges();
                        Response.Redirect("~/admin/Pages/ManageDrivers.aspx");
                    }
                }
            }
            catch (Exception)
            {
                lblError.Visible = true;
                lblError.Text = "There are problem with your information.";
            }
        }

        public int DriverID
        {
            get
            {
                if (ViewState["DriverID"] != null)
                    return (int)ViewState["DriverID"];
                else
                    return 0;
            }
            set { ViewState["DriverID"] = value; }
        }
    }
}