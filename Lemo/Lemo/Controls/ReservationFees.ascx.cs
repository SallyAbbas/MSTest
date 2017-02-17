using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lemo.Classes;
using System.Configuration;
using Lemo.DAL;
using System.Text;

namespace Lemo.Controls
{
    public partial class ReservationFees : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {               
            //Request.Params.Get()
            if (!IsPostBack)
            {
                CarsList = new List<Car>();
                JobWrapper jobWrapper = new JobWrapper();
                if (jobWrapper.JobDetailsObject != null)
                {
                    LimoEntitiesEntityFremwork limoEntities = new LimoEntitiesEntityFremwork();
                    IList<Car> carTempFrom = null, carTempTo = null;

                    string[] fromAddress = jobWrapper.JobDetailsObject.FromAddress.ToLower().Trim().Split(',');
                    foreach (var fromState in fromAddress)
                    {
                        var tempFromState = fromState.Trim();
                        var temp = fromState.Trim().Split(' ');
                        if (fromState.Trim().Length == 8 && temp[0].Length == 2)
                        {
                            tempFromState = temp[0];
                        }
                        
                       carTempFrom = limoEntities.Cars.Where(
                           xx =>
                           xx.Company.IsActive != null && xx.Company.IsActive.Value && xx.Company.IsAvailable != null &&
                           xx.Company.IsAvailable.Value && xx.NoPassengers != null &&
                           xx.NoPassengers.Value >= jobWrapper.JobDetailsObject.UserNumberINT &&
                           !string.IsNullOrEmpty(xx.Company.stateName) &&
                                   tempFromState.ToLower() == xx.Company.stateName.ToLower()).ToList();
                       if (carTempFrom != null && carTempFrom.Count > 0)
                       {
                           break;
                       }                       
                    }
                    string[] toAddress = jobWrapper.JobDetailsObject.ToAddress.ToLower().Trim().Split(',');
                    foreach (var toState in toAddress)
                    {
                        var tempToState = toState.Trim();
                        var temp = toState.Trim().Split(' ');
                        if (toState.Trim().Length == 8 && temp[0].Length == 2)
                        {
                            tempToState = temp[0];
                        }
                        carTempTo = limoEntities.Cars.Where(
                            xx =>
                            xx.Company.IsActive != null && xx.Company.IsActive.Value && xx.Company.IsAvailable != null &&
                            xx.Company.IsAvailable.Value && xx.NoPassengers != null &&
                            xx.NoPassengers.Value >= jobWrapper.JobDetailsObject.UserNumberINT &&
                            !string.IsNullOrEmpty(xx.Company.stateName) &&
                                    tempToState.ToLower() == xx.Company.stateName.ToLower()).ToList();
                        if (carTempTo != null && carTempTo.Count > 0)
                        {                            
                            break;
                        }    
                   
                    }
                    if (carTempFrom != null && carTempTo != null)
                        CarsList.AddRange(carTempFrom.Union(carTempTo));
                    else
                    {
                        if (carTempFrom != null)
                            CarsList.AddRange(carTempFrom);
                        else if (carTempTo != null)
                            CarsList.AddRange(carTempTo);
                    }
                    foreach (var car in CarsList)
                    {
                        car.CompanyName = car.Company.CompanyName;
                        double distancePrice = jobWrapper.JobDetailsObject.Distance * (car.PricePerMaile != null? car.PricePerMaile.Value: 1);
                        double hourPrice = jobWrapper.JobDetailsObject.JobDurationPerHours * (car.PricePerHore != null ? car.PricePerHore.Value : 1);
                        double tempPrice = distancePrice + hourPrice;
                                                 
                        List<CompanyCityPrice> companyCityPricesList = car.Company.CompanyCityPrices.ToList();
                        if (companyCityPricesList.Count > 0)
                        {
                            foreach (var companyCityPrice in companyCityPricesList)
                            {
                                if (
                                    (jobWrapper.JobDetailsObject.FromAddress.ToLower().Contains(
                                        companyCityPrice.cities_extended.CityName.ToLower()) &&
                                     (jobWrapper.JobDetailsObject.FromAddress.ToLower().Contains(
                                         companyCityPrice.cities_extended.State.StateName.ToLower()) ||
                                      jobWrapper.JobDetailsObject.FromAddress.ToLower().Contains(
                                          companyCityPrice.cities_extended.State.state_code.ToLower()))) ||
                                    (jobWrapper.JobDetailsObject.ToAddress.ToLower().Contains(
                                        companyCityPrice.cities_extended.CityName.ToLower()) &&
                                     (jobWrapper.JobDetailsObject.ToAddress.ToLower().Contains(
                                         companyCityPrice.cities_extended.State.StateName.ToLower()) ||
                                      jobWrapper.JobDetailsObject.ToAddress.ToLower().Contains(
                                          companyCityPrice.cities_extended.State.state_code.ToLower()))))
                                {
                                    var temp = companyCityPrice.Price.HasValue ? companyCityPrice.Price.Value : 0;
                                    if (temp > tempPrice)
                                        tempPrice = temp;
                                    jobWrapper.JobDetailsObject.cityID = companyCityPrice.CityID;
                                }
                            }
                        }
                        //string zipCode = car.Company.ZipCode;
                        car.TotalPrice = Math.Ceiling(tempPrice);
                    }
                    CarsList = CarsList.OrderBy(xx => xx.TotalPrice).ToList();
                    //GridView1.DataSource = CarsList;
                    //GridView1.DataBind();
                    GridView1.DataSource = CarsList;
                    GridView1.DataBind();
                    var topCompaniesList = CarsList.Where(xxx => xxx.Company != null && xxx.Company.IsTop.HasValue && xxx.Company.IsTop.Value).ToList();
                    if (topCompaniesList.Count > 0)
                    {
                        spanTopCompainies.Visible = true; ;
                        DataList1.DataSource = CarsList;
                        DataList1.DataBind();
                    }
                    else
                        spanTopCompainies.Visible = false;
                }
                //BuildGridsForCars(CarsList);
                //divMain.InnerHtml = HtmlCarsList;
            }
        }

        public List<DAL.Car> CarsList
        {
            get
            {
                if (Session["CarsList"] != null)
                {
                    return (List<DAL.Car>) Session["CarsList"];
                }
                return null;
            }
            set { Session["CarsList"] = value; }
        }

        void BuildGridsForCars(List<DAL.Car> carsList)
        {
            StringBuilder carsGrid = new StringBuilder();
            carsGrid.Append("<table width='100%'>");
            int noPassenger = 1, noBag = 1;
            foreach (var car in carsList)
            {
                if (car.NoPassengers.HasValue) noPassenger = car.NoPassengers.Value;
                //if(car.
                carsGrid.Append("<tr class='ResCarType'>" +
                      "<td width='25%'>" +
                      "<div class='TitleInnerPage'>" + "<input id='Radio1" + car.CarID + "' type='radio' name='rdSelectCar'" + "/>" + car.CarName + "</div>" +
                                " <div><img src='../Image/Passenger/" + noPassenger + ".png' class='features' " + "/>" +
                                "<img src='../Image/Passenger/Passenger.png' class='features' " + "/>" +
                                "<img src='../Image/Passenger/Bag.png' class='features' " + "/></div>" +
                                "  <div class='Description'>" + car.Description + "</div>" +
                                "</td>" +
                                "<td width='40%'>" +
                                "   <img src='" + car.PhotoName + "' width='210px' height='80px'"+ " />" +
                                "</td>" +
                                "<td width='25%'>" +
                                //  "<div>" + "<input id='Radio1" + car.CarID + "' type='radio' name='rdSelectCar'" + "/>" +
                                //"  </div>" +
                                "<div class='Price'>" +
                                "$" + car.TotalPrice +
                                " </div> " +
                                "  <div class='Description'>" +
                                "       Operator by " + car.CompanyName + "</div>" +
                                "</td>" +
                                "<td width='10%'>" +
                                "<input id='Button1" + car.CarID + "' type='button' value='Continue' class='butContinue Hidden' carID='" + car.CarID +
                                "' />" +
                                "</td>" +
                                "</tr>");
            }
            carsGrid.Append("</table>");
            HtmlCarsList = carsGrid.ToString();
        }

        public string HtmlCarsList
        {
            get
            {
                if (ViewState["HtmlCarsList"] != null)
                {
                    return ViewState["HtmlCarsList"].ToString();
                }
                return string.Empty;
            }
            set { ViewState["HtmlCarsList"] = value; }
        }

        protected void butContino_Click(object sender, EventArgs e)
        {
            //int id = int.Parse(hfCarID.Value);
            //DAL.Car car = CarsList.Where(xx => xx.CarID == id).FirstOrDefault();
            //JobWrapper jobWrapper = new JobWrapper();
            //jobWrapper.JobDetailsObject.JobCompanyName = car.CompanyName;
            //jobWrapper.JobDetailsObject.JobCarType = car.CarName;
            //jobWrapper.JobDetailsObject.JobBasePrise = car.TotalPrice;
            //jobWrapper.JobDetailsObject.CarID = car.CarID;
            //jobWrapper.JobDetailsObject.CompanyID = car.CompanyID;
            //Response.Redirect("~/Pages/CompleteReservation.aspx");
        }


        protected void butEdit_onClick123(object sender, EventArgs e)
        {
            //AdminHelper adminHelper = new AdminHelper();
            int id = int.Parse(((Button)sender).CommandName);
            //int id = int.Parse(hfCarID.Value);
            DAL.Car car = CarsList.Where(xx => xx.CarID == id).FirstOrDefault();
            JobWrapper jobWrapper = new JobWrapper();
            jobWrapper.JobDetailsObject.JobCompanyName = car.CompanyName;
            jobWrapper.JobDetailsObject.JobCarType = car.CarName;
            jobWrapper.JobDetailsObject.JobBasePrise = car.TotalPrice;
            jobWrapper.JobDetailsObject.CarID = car.CarID;
            jobWrapper.JobDetailsObject.CompanyID = car.CompanyID;
            Response.Redirect("~/Pages/CompleteReservation.aspx");
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.DataSource = CarsList;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }

        string GetState(string address)
        {
            string[] toAddress = address.Trim().Split(',');
            string toStreet = "", toCity = "", toState = "", toZIPCode = "";
            if (toAddress.Length > 0)
            {
                if (toAddress.Length == 3)
                {
                    toStreet = toAddress[0];
                    toCity = toAddress[1];
                    string[] temp = toAddress[2].Trim().Split(' ');
                    toState = temp[0];
                    if (temp.Length > 2)
                        toState = temp[0] + temp[1];
                    else if (temp.Length == 2)
                        toZIPCode = temp[1];
                }
                else if (toAddress.Length == 2)
                {
                    toCity = toAddress[0];
                    string[] temp = toAddress[1].Trim().Split(' ');
                    toState = temp[0];
                    if (temp.Length > 2)
                        toState = temp[0] + temp[1];
                    else if (temp.Length == 2)
                        toZIPCode = temp[1];
                }
                else if (toAddress.Length == 4)
                {
                    toCity = toAddress[0];
                    string[] temp = toAddress[2].Trim().Split(' ');
                    toState = temp[0];
                    if (temp.Length > 2)
                        toState = temp[0] + temp[1];
                    else if (temp.Length == 2)
                        toZIPCode = temp[1];
                }
                return toState;
            }
            return string.Empty;
        }
    }
}