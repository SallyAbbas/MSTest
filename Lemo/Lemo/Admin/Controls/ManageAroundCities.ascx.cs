using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Lemo.DAL;
using Lemo.Admin.Helper;

namespace Lemo.Admin.Controls
{
    public partial class ManageAroundCities : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AdminHelper adminHelper = new AdminHelper();
            if(!IsPostBack)
            {
                using (var DC = new LimoEntitiesEntityFremwork())
                {
                    ddlDistance.SelectedValue = adminHelper.CompanyObject.DistancePerMile.HasValue
                                                    ? adminHelper.CompanyObject.DistancePerMile.Value.ToString()
                                                    : "0";
                    CitiesList =
                            DC.CompanyCityPrices.Where(
                                xx =>
                                xx.IsActive.HasValue && xx.CompanyID == adminHelper.CompanyObject.CompanyID &&
                                xx.IsActive.Value).ToList();
                    foreach (var companyCityPrice in CitiesList)
                    {
                        companyCityPrice.CityName = companyCityPrice.cities_extended.CityName;
                        companyCityPrice.StateName = companyCityPrice.cities_extended.State.StateName;
                    }
                    GridView1.DataSource = CitiesList;
                    GridView1.DataBind();
                }
            }

            if (adminHelper.CompanyObject != null && !string.IsNullOrEmpty(adminHelper.CompanyObject.ZipCode) &&
                !(adminHelper.CompanyObject.IsSupportAllState.HasValue && adminHelper.CompanyObject.IsSupportAllState.Value))
            {
                string script = "var ZipCodeValue='" + adminHelper.CompanyObject.ZipCode + "';";
                this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "ZipCodeScript", script, true);
            }
            else
            {
                Response.Redirect("~/admin/Pages/CompanyProfile.aspx");
            }
        }

        protected void ddlDistance_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillListWithCities();
        }

        private void FillListWithCities()
        {
            string temp = hfLatLon.Value;
            string[] list = temp.Split('&');
            list = list.Distinct().ToArray();
            AdminHelper adminHelper = new AdminHelper();
            if (adminHelper.CompanyObject != null)
            {
                using (var DC = new LimoEntitiesEntityFremwork())
                {
                    Company company = DC.Companies.Where(xx=>xx.CompanyID == adminHelper.CompanyObject.CompanyID).ToList()[0];
                    double distance;
                    company.DistancePerMile = double.TryParse(ddlDistance.SelectedValue, out distance)? distance: 0;
                    var oldCities =
                        DC.CompanyCityPrices.Where(xx => xx.CompanyID == adminHelper.CompanyObject.CompanyID).ToList();
                    var citiesList =
                        DC.cities_extended.Where(
                            ob => list.Contains(ob.latitude.Substring(0, 5) + "," + ob.longitude.Substring(0, 5))).
                            OrderBy(elem => elem.state_code).ToList();
                    if (citiesList.Count > 0)
                    {
                        citiesList = citiesList.DistinctBy(xx => xx.CityStateName).ToList();
                        CompanyCityPrice companycityPrice;
                        foreach (var oldCity in oldCities)
                        {
                            var dummy = citiesList.Where(xx => xx.CityID == oldCity.CityID).ToList().Count;
                            if (dummy > 0)
                            {
                                oldCity.IsActive = true;
                                citiesList.Remove(citiesList.Where(xx => xx.CityID == oldCity.CityID).ToList()[0]);
                            }
                            else
                                oldCity.IsActive = false;
                        }
                        foreach (var citiesExtended in citiesList)
                        {
                            companycityPrice = new CompanyCityPrice
                                                       {
                                                           CompanyID = adminHelper.CompanyObject.CompanyID,
                                                           CityID = citiesExtended.CityID,
                                                           IsActive = true
                                                       };
                            DC.CompanyCityPrices.AddObject(companycityPrice);
                        }
                        DC.SaveChanges();
                        var cityPriceList =
                            DC.CompanyCityPrices.Where(xx => xx.IsActive.HasValue && xx.CompanyID == adminHelper.CompanyObject.CompanyID && xx.IsActive.Value).ToList
                                ();
                        foreach (var companyCityPrice in cityPriceList)
                        {
                            companyCityPrice.CityName = companyCityPrice.cities_extended.CityName;
                            companyCityPrice.StateName = companyCityPrice.cities_extended.State.StateName;
                        }
                        CitiesList = cityPriceList;
                        GridView1.DataSource = CitiesList;
                        GridView1.DataBind();
                        adminHelper.CompanyObject = company;
                    }
                }
            }
        }

        public void RaisePostBackEvent(string eventArgument)
        {
            //if (eventArgument == "GetCities")
            //{
            //    FillListWithCities();
            //}
        }

        protected void butLogin_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                AdminHelper adminHelper = new AdminHelper();
                double tempPrice;
                double? nullPrice = null;
                int tempID = 0;
                CompanyCityPrice companyCityPriceTemp;
                List<CompanyCityPrice> tempCCPL = new List<CompanyCityPrice>();
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    double.TryParse(((TextBox)GridView1.Rows[i].Cells[2].FindControl("txtPrice")).Text, out tempPrice);
                    int.TryParse(((HiddenField)GridView1.Rows[i].Cells[2].FindControl("hfID")).Value, out tempID);
                    companyCityPriceTemp = CitiesList.Where(xx => xx.CompanyCityPriceID == tempID).FirstOrDefault();
                    if (companyCityPriceTemp != null)
                    {
                        companyCityPriceTemp.Price = tempPrice > 0 ? tempPrice : nullPrice;
                        tempCCPL.Add(companyCityPriceTemp);
                    }
                }
                using (var dc = new LimoEntitiesEntityFremwork())
                {
                    List<int> tempIDsList = tempCCPL.Select(yy => yy.CompanyCityPriceID).ToList();
                    List<CompanyCityPrice> dbList =
                        dc.CompanyCityPrices.Where(xx => tempIDsList.Contains(xx.CompanyCityPriceID)).ToList();
                    foreach (var companyCityPrice in dbList)
                    {
                        CompanyCityPrice tempTemp =
                            tempCCPL.Where(xx => xx.CompanyCityPriceID == companyCityPrice.CompanyCityPriceID).Single();
                        companyCityPrice.Price = tempTemp.Price;
                    }
                    dc.SaveChanges();
                    CitiesList =
                        dc.CompanyCityPrices.Where(
                            xx =>
                            xx.IsActive.HasValue && xx.CompanyID == adminHelper.CompanyObject.CompanyID && xx.IsActive.Value)
                            .ToList
                            ();
                    foreach (var companyCityPrice in CitiesList)
                    {
                        companyCityPrice.CityName = companyCityPrice.cities_extended.CityName;
                        companyCityPrice.StateName = companyCityPrice.cities_extended.State.StateName;
                    }
                }
                int pageIndex = GridView1.PageIndex;
                GridView1.DataSource = CitiesList;
                GridView1.DataBind();
                GridView1.PageIndex = pageIndex;
                divConfirmation.Visible = true;
            }
            catch (Exception)
            {
                divError.Visible = true;
            }
        }

        public List<CompanyCityPrice> CitiesList
        {
            get
            {
                if(Session["CitiesList_ManageAccountList"] != null)
                    return (List<CompanyCityPrice>)Session["CitiesList_ManageAccountList"];
                return new List<CompanyCityPrice>();
            }
            set { Session["CitiesList_ManageAccountList"] = value; }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.DataSource = CitiesList;
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }
    }
}