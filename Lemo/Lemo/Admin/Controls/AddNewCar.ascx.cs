using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lemo.Admin.Helper;
using Lemo.DAL;
using System.IO;

namespace Lemo.Admin.Controls
{
    public partial class AddNewCar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AdminHelper adminHelper = new AdminHelper();
                LimoEntitiesEntityFremwork limoEntities = new LimoEntitiesEntityFremwork();
                int id = 0;
                int.TryParse(Request.QueryString["id"], out id);
                if (id != 0)
                {
                    CarID = id;
                    DAL.Car carTemp =
                        limoEntities.Cars.Where(xx => xx.CarID == CarID).ToList().
                            FirstOrDefault();
                    if (carTemp != null)
                    {
                        txtCarName.Text = carTemp.CarName;
                        txtDescription.Text = carTemp.Description;
                        //txtPrice.Text = carTemp.MinPrice.HasValue ? carTemp.MinPrice.Value.ToString() : string.Empty;
                        txtPricePerHour.Text = carTemp.PricePerHore.HasValue
                                                   ? carTemp.PricePerHore.Value.ToString()
                                                   : string.Empty;
                        txtPricePerMile.Text = carTemp.PricePerMaile.HasValue
                                                   ? carTemp.PricePerMaile.Value.ToString()
                                                   : string.Empty;
                        ddlNoPassengers.SelectedValue = carTemp.NoPassengers != null
                                                            ? carTemp.NoPassengers.ToString()
                                                            : "1";
                        imgLogo.ImageUrl = carTemp.PhotoName ?? string.Empty;
                        GetCarType(carTemp.CarName);
                        txtCarNumber.Text = carTemp.CarNumber;
                    }
                }
                else
                {
                    rbSedan.Checked = true;
                }
            }
        }

        private bool GetCarType(string carName)
        {
            carName = carName.Replace(" ", "_");
            Limo_Gloabel.CarType carType;
            bool IsKnownType = System.Enum.TryParse(carName, out carType);
            //Limo_Gloabel.CarType carType = new Limo_Gloabel.CarType();
            if (IsKnownType)
            {
                switch (carType)
                {
                    case Limo_Gloabel.CarType.Sedan:
                        rbSedan.Checked = true;
                        break;
                    case Limo_Gloabel.CarType.Suv:
                        rbSuv.Checked = true;
                        break;
                    case Limo_Gloabel.CarType.Van:
                        rbVan.Checked = true;
                        break;
                    case Limo_Gloabel.CarType.Stretch_SUV:
                        rbStretchSUV.Checked = true;
                        break;
                    case Limo_Gloabel.CarType.Stretch_Limo:
                        rbStretchLimo.Checked = true;
                        break;
                    case Limo_Gloabel.CarType.Mini_Bus:
                        rbMinibus.Checked = true;
                        break;
                    case Limo_Gloabel.CarType.Bus:
                        rbBus.Checked = true;
                        break;
                    case Limo_Gloabel.CarType.BMW:
                        rbBMW.Checked = true;
                        break;
                    case Limo_Gloabel.CarType.Cadilac:
                        rbCadilac.Checked = true;
                        break;
                    case Limo_Gloabel.CarType.Mercedes:
                        rbMercedes.Checked = true;
                        break;
                    default:
                        rbOther.Checked = true;
                        break;
                }
            }
            else
                rbOther.Checked = true;
            return IsKnownType;
        }

        protected void butSignUp_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                bool isKnownType = GetCarType(txtCarName.Text);
                LimoEntitiesEntityFremwork limoEntities = new LimoEntitiesEntityFremwork();
                AdminHelper adminHelper = new AdminHelper();
                if (CarID != 0)
                {
                    DAL.Car carTemp =
                        limoEntities.Cars.Where(
                            xx => xx.CompanyID == adminHelper.CompanyObject.CompanyID && xx.CarID == CarID).
                            FirstOrDefault();
                    if (carTemp != null)
                    {
                        //if (carTemp.CarName.ToLower() == txtCarName.Text.ToLower() || (carTemp.CarName != txtCarName.Text &&
                        //                                           limoEntities.Cars.Where(
                        //                                               xx =>
                        //                                               xx.CarName.ToLower() == txtCarName.Text.ToLower()).
                        //                                               ToList().Count == 0))
                        //{
                        carTemp.CarName = txtCarName.Text;
                        carTemp.Description = txtDescription.Text;
                        //double priceTemp;
                        double? nullPrice = null;
                        int noPassengerTemp;
                        //carTemp.MinPrice = double.TryParse(txtPrice.Text, out priceTemp) ? priceTemp : nullPrice;
                        double pricePerMile, pricePerHour;
                        carTemp.PricePerHore = double.TryParse(txtPricePerHour.Text, out pricePerHour)
                                                   ? pricePerHour
                                                   : nullPrice;
                        carTemp.PricePerMaile = double.TryParse(txtPricePerMile.Text, out pricePerMile)
                                                    ? pricePerMile
                                                    : nullPrice;
                        carTemp.NoPassengers = int.TryParse(ddlNoPassengers.SelectedValue, out noPassengerTemp)
                                                   ? noPassengerTemp
                                                   : 0;
                        string imagPublishedPath = string.Empty;
                        if (!isKnownType)
                        {
                            if (fu_Image.HasFile)
                            {
                                imagPublishedPath = SaveImg(fu_Image);
                            }
                        }
                        else
                        {
                            imagPublishedPath = GetImagePath(txtCarName.Text);
                        }
                        if (!string.IsNullOrEmpty(imagPublishedPath))
                            carTemp.PhotoName = imagPublishedPath;
                        carTemp.CarNumber = txtCarNumber.Text;
                        limoEntities.SaveChanges();
                        Response.Redirect("~/admin/Pages/ManageCars.aspx");
                        //}
                    }
                    else
                        lblError.Visible = true;
                }
                else 
                    //if (limoEntities.Cars.Where(xx => xx.CarName.ToLower() == txtCarName.Text.ToLower()).ToList().Count == 0)
                {
                    DAL.Car newCar = new DAL.Car();
                    newCar.CompanyID = adminHelper.CompanyObject.CompanyID;
                    newCar.CarName = txtCarName.Text;
                    newCar.Description = txtDescription.Text;
                    double priceTemp;
                    double? nullPrice = null;
                    int noPassengerTemp;
                    //newCar.MinPrice = double.TryParse(txtPrice.Text, out priceTemp) ? priceTemp : nullPrice;
                    newCar.NoPassengers = int.TryParse(ddlNoPassengers.SelectedValue, out noPassengerTemp)
                                              ? noPassengerTemp
                                              : 1;
                    double pricePerMile, pricePerHour;
                    newCar.PricePerHore = double.TryParse(txtPricePerHour.Text, out pricePerHour)
                                              ? pricePerHour
                                              : nullPrice;
                    newCar.PricePerMaile = double.TryParse(txtPricePerMile.Text, out pricePerMile)
                                               ? pricePerMile
                                               : nullPrice;
                    string imagPublishedPath = string.Empty;
                    if (!isKnownType)
                    {
                        if (fu_Image.HasFile)
                        {
                            imagPublishedPath = SaveImg(fu_Image);
                        }
                    }
                    else
                    {
                        imagPublishedPath = GetImagePath(txtCarName.Text);
                    }
                    if (!string.IsNullOrEmpty(imagPublishedPath))
                        newCar.PhotoName = imagPublishedPath;
                    newCar.CarNumber = txtCarNumber.Text;
                    limoEntities.Cars.AddObject(newCar);
                    limoEntities.SaveChanges();
                    Response.Redirect("~/admin/Pages/ManageCars.aspx");
                }
                //else
                //    lblError.Visible = true;
            }
            catch (Exception)
            {
                lblError.Visible = true;
                lblError.Text = "There are problem with your information.";
            }
        }

        private string SaveImg(FileUpload fu_Img)
        {
            string filename = Path.GetFileName(fu_Img.FileName);
            string temp = @"d:\inetpub\vhosts\limoallover.net\httpdocs\AppImages\company\cars\";
            string imagPublishedPath =
                string.Format(@"http://limoallover.net/AppImages/company/cars/" + txtCarName.Text + fu_Img.FileName);
            string tempFile = temp + txtCarName.Text + filename;
            fu_Img.SaveAs(tempFile);
            bool isExist = CheckExistFile(tempFile);
            if (isExist)
            {
                return imagPublishedPath;
            }
            else
            {
                return string.Empty;
            }
        }

        protected bool CheckExistFile(string filePath)
        {
            bool isExist = false;
            try
            {
                File.ReadAllText(filePath);
                isExist = true;
            }
            catch (Exception ex)
            {
                isExist = false;
            }
            return isExist;
        }

        public int CarID
        {
            get
            {
                if (ViewState["CarID"] != null)
                    return (int) ViewState["CarID"];
                else
                    return 0;
            }
            set { ViewState["CarID"] = value; }
        }

        private string GetImagePath(string carName)
        {
            string imagPublishedPath =
                string.Format(@"http://limoallover.net/AppImages/company/cars/KnownCars/" + carName + ".png");
            return imagPublishedPath;
        }
    }
}