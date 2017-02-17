using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Web.Script.Services;
using System.Xml;
using System.Text;
using System.Collections;
using System.IO;
using System.Collections.Specialized;


/// <summary>
/// Summary description for LimoAllOver
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class LimoAllOver : System.Web.Services.WebService
{

    private SqlConnection sqlCon = new SqlConnection("Data Source=162.144.66.219;Initial Catalog=maged_LimoDB;User ID=asaid;Password=Asa01111028251");
    LogHandling log = new LogHandling();
    public LimoAllOver()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 


    }

    #region GetAllCars
    [WebMethod]
    public List<Car> GetAllCars(string cityFrom, string cityTo, int NumOfPassangers, int numberOfHour, int numberOfMile)
    {
        List<Car> list = new List<Car>();
        try
        {
            string cmdText = "SELECT dbo.Company.CompanyID, dbo.Company.CompanyName, dbo.Company.LimoAPIID, dbo.Company.LimoAPIKey,  dbo.Company.IsActive, dbo.Car.PricePerMaile, dbo.Car.PricePerHore, dbo.Company.IsAvailable, dbo.Car.CarName, dbo.Car.CarID, dbo.Car.Description, dbo.Car.NoPassengers, dbo.Car.PhotoName FROM dbo.Car INNER JOIN dbo.Company ON dbo.Car.CompanyID = dbo.Company.CompanyID WHERE (dbo.Company.IsActive = 1) AND (dbo.Company.IsAvailable = 1) AND (dbo.Car.NoPassengers >= " + NumOfPassangers.ToString() + ")";
            if (this.sqlCon.State != ConnectionState.Open)
                this.sqlCon.Open();
            SqlDataReader sqlDataReader1 = new SqlCommand(cmdText, this.sqlCon).ExecuteReader();
            while (sqlDataReader1.Read())
                list.Add(new Car()
                {
                    CarID = sqlDataReader1.GetInt32(sqlDataReader1.GetOrdinal("CarID")),
                    CarName = sqlDataReader1.GetString(sqlDataReader1.GetOrdinal("CarName")),
                    CompanyID = sqlDataReader1.GetInt32(sqlDataReader1.GetOrdinal("CompanyID")),
                    CompanyName = sqlDataReader1.GetString(sqlDataReader1.GetOrdinal("CompanyName")),
                    Description = sqlDataReader1.GetString(sqlDataReader1.GetOrdinal("Description")),
                    LimoAPIID = sqlDataReader1.GetString(sqlDataReader1.GetOrdinal("LimoAPIID")),
                    LimoAPIKey = sqlDataReader1.GetString(sqlDataReader1.GetOrdinal("LimoAPIKey")),
                    NoPassengers = sqlDataReader1.GetInt32(sqlDataReader1.GetOrdinal("NoPassengers")),
                    PricePerHore = (float)sqlDataReader1.GetDouble(sqlDataReader1.GetOrdinal("PricePerHore")),
                    PricePerMaile = (float)sqlDataReader1.GetDouble(sqlDataReader1.GetOrdinal("PricePerMaile")),
                    ImagePath = sqlDataReader1.GetString(sqlDataReader1.GetOrdinal("PhotoName")),
                    TotalPrice = ""
                });
            sqlDataReader1.Close();
            for (int index = 0; index < list.Count; ++index)
            {
                if (numberOfHour > 1 && numberOfHour < 2)
                    numberOfHour = 2;
                else if (numberOfHour <= 1)
                    numberOfHour = 1;
                float num = (float)((double)list[index].PricePerHore * (double)numberOfHour + (double)list[index].PricePerMaile * (double)numberOfMile);
                SqlDataReader sqlDataReader2 = new SqlCommand("SELECT dbo.CompanyCityPrice.CompanyID, dbo.CompanyCityPrice.Price, dbo.CompanyCityPrice.IsActive, dbo.cities_extended.CityName  FROM  dbo.cities_extended INNER JOIN dbo.CompanyCityPrice  ON dbo.cities_extended.CityID = dbo.CompanyCityPrice.CityID WHERE (dbo.CompanyCityPrice.IsActive = 1) AND   (dbo.CompanyCityPrice.CompanyID = " + (object)list[index].CompanyID + ") AND (dbo.CompanyCityPrice.Price IS NOT NULL)AND ( (CHARINDEX(dbo.cities_extended.CityName, '" + cityFrom + "') > 0) OR (CHARINDEX(dbo.cities_extended.CityName, '" + cityTo + "') > 0) )", this.sqlCon).ExecuteReader();
                while (sqlDataReader2.Read())
                {
                    if ((double)num < sqlDataReader2.GetDouble(sqlDataReader2.GetOrdinal("Price")))
                        num = (float)sqlDataReader2.GetDouble(sqlDataReader2.GetOrdinal("Price"));
                }
                list[index].TotalPrice = num.ToString();
            }
        }
        catch (Exception ex)
        {
            log.Add2ExLog(ex.Message + " " + ex.Source + " " + ex.InnerException);
        }

        this.sqlCon.Close();
        return list;
    }

    #endregion


    #region GetJobDetailsBeforeAssign
    [WebMethod]
    public List<Job> GetJobDetailsBeforeAssign(int JobID)
    {
        List<Job> list = new List<Job>();

        DataTable dtJob = GetJobDetailsInfo(JobID);

        string cmdText = "SELECT * from Job" + " Where  JobID = " + JobID.ToString();
        if (this.sqlCon.State != ConnectionState.Open)
            this.sqlCon.Open();
        SqlDataReader sqlDataReader = new SqlCommand(cmdText, this.sqlCon).ExecuteReader();
        while (sqlDataReader.Read())
        {



            list.Add(new Job()
            {
                UserID = sqlDataReader.IsDBNull(1) ? -1 : sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("UserID")),
                CompanyCarID = sqlDataReader.IsDBNull(2) ? -1 : sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("CompanyCarID")),
                FromAddress = sqlDataReader.IsDBNull(3) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("FromAddress")),
                ToAddress = sqlDataReader.IsDBNull(4) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("ToAddress")),
                CityID = sqlDataReader.IsDBNull(5) ? -1 : sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("CityID")),
                TimeHour = sqlDataReader.IsDBNull(6) ? -1 : sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("TimeHour")),
                TimeMinute = sqlDataReader.IsDBNull(7) ? -1 : sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("TimeMinute")),
                TotalPrice = sqlDataReader.IsDBNull(8) ? -1f : (float)sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("TotalPrice")),
                EstimatedFarePrice = sqlDataReader.IsDBNull(9) ? -1f : (float)sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("EstimatedFarePrice")),
                GratuityPrice = sqlDataReader.IsDBNull(10) ? -1f : (float)sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("GratuityPrice")),
                ProcessingFee = sqlDataReader.IsDBNull(11) ? -1f : (float)sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("ProcessingFee")),
                Taxes = sqlDataReader.IsDBNull(12) ? -1f : (float)sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("Taxes")),
                OtherPrice = sqlDataReader.IsDBNull(13) ? -1f : (float)sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("OtherPrice")),
                Notes = sqlDataReader.IsDBNull(14) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("Notes")),
                LimoConfirmNumber = sqlDataReader.IsDBNull(15) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("LimoConfirmNumber")),
                ComapnyConfirmNum = sqlDataReader.IsDBNull(16) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("ComapnyConfirmNum")),
                FirstName = sqlDataReader.IsDBNull(17) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("FirstName")),
                LastName = sqlDataReader.IsDBNull(18) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("LastName")),
                Mobile = sqlDataReader.IsDBNull(19) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("Mobile")),
                Email = sqlDataReader.IsDBNull(20) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("Email")),
                JobDate = sqlDataReader.IsDBNull(21) ? "-1" : sqlDataReader.GetDateTime(sqlDataReader.GetOrdinal("JobDate")).ToString(),
                JobTime = sqlDataReader.IsDBNull(22) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("JobTime")),
                PriceNote = sqlDataReader.IsDBNull(23) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("PriceNote")),
                DriverID = sqlDataReader.IsDBNull(24) ? -1 : sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("DriverID")),
                TripConfirmationNumber = sqlDataReader.IsDBNull(25) ? -1 : sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("TripConfirmationNumber")),
                AuthorizationCode = sqlDataReader.IsDBNull(26) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("AuthorizationCode")),
                AuthorizInvoiceNumber = sqlDataReader.IsDBNull(27) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("AuthorizInvoiceNumber")),
                JobStatusID = sqlDataReader.IsDBNull(28) ? -1 : sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("JobStatusID"))
            });

        }


        sqlDataReader.Close();
        this.sqlCon.Close();
        return list;
    }

    #endregion


    [WebMethod]
    public List<Job> GetJobDetails(int JobID)
    {
        List<Job> list = new List<Job>();

        DataTable dtJob = GetJobDetailsInfo(JobID);

        string cmdText = "SELECT     maged_LimoDB.Driver.FName, maged_LimoDB.Driver.LName, maged_LimoDB.CarType.Name, " +
                         " maged_LimoDB.CarType.ImagePath, Job.* " +
                         " FROM Job INNER JOIN " +
                         " maged_LimoDB.Driver ON Job.DriverID = maged_LimoDB.Driver.DriverID INNER JOIN " +
                         " maged_LimoDB.CarType ON maged_LimoDB.Driver.CarTypeID = maged_LimoDB.CarType.CarTypeID " +
                         " Where  JobID = " + JobID.ToString();
        if (this.sqlCon.State != ConnectionState.Open)
            this.sqlCon.Open();
        SqlDataReader sqlDataReader = new SqlCommand(cmdText, this.sqlCon).ExecuteReader();
        while (sqlDataReader.Read())
        {


            list.Add(new Job()
            {
                UserID = sqlDataReader.IsDBNull(5) ? -1 : sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("UserID")),
                CompanyCarID = sqlDataReader.IsDBNull(6) ? -1 : sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("CompanyCarID")),
                FromAddress = sqlDataReader.IsDBNull(7) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("FromAddress")),
                ToAddress = sqlDataReader.IsDBNull(8) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("ToAddress")),
                CityID = sqlDataReader.IsDBNull(9) ? -1 : sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("CityID")),
                TimeHour = sqlDataReader.IsDBNull(10) ? -1 : sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("TimeHour")),
                TimeMinute = sqlDataReader.IsDBNull(11) ? -1 : sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("TimeMinute")),
                TotalPrice = sqlDataReader.IsDBNull(12) ? -1f : (float)sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("TotalPrice")),
                EstimatedFarePrice = sqlDataReader.IsDBNull(13) ? -1f : (float)sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("EstimatedFarePrice")),
                GratuityPrice = sqlDataReader.IsDBNull(14) ? -1f : (float)sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("GratuityPrice")),
                ProcessingFee = sqlDataReader.IsDBNull(15) ? -1f : (float)sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("ProcessingFee")),
                Taxes = sqlDataReader.IsDBNull(16) ? -1f : (float)sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("Taxes")),
                OtherPrice = sqlDataReader.IsDBNull(17) ? -1f : (float)sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("OtherPrice")),
                Notes = sqlDataReader.IsDBNull(18) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("Notes")),
                LimoConfirmNumber = sqlDataReader.IsDBNull(19) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("LimoConfirmNumber")),
                ComapnyConfirmNum = sqlDataReader.IsDBNull(20) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("ComapnyConfirmNum")),
                FirstName = sqlDataReader.IsDBNull(21) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("FirstName")),
                LastName = sqlDataReader.IsDBNull(22) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("LastName")),
                Mobile = sqlDataReader.IsDBNull(23) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("Mobile")),
                Email = sqlDataReader.IsDBNull(24) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("Email")),
                JobDate = sqlDataReader.IsDBNull(21) ? "-1" : sqlDataReader.GetDateTime(sqlDataReader.GetOrdinal("JobDate")).ToString(),
                JobTime = sqlDataReader.IsDBNull(22) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("JobTime")),
                PriceNote = sqlDataReader.IsDBNull(27) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("PriceNote")),
                DriverID = sqlDataReader.IsDBNull(28) ? -1 : sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("DriverID")),
                TripConfirmationNumber = sqlDataReader.IsDBNull(29) ? -1 : sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("TripConfirmationNumber")),
                AuthorizationCode = sqlDataReader.IsDBNull(30) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("AuthorizationCode")),
                AuthorizInvoiceNumber = sqlDataReader.IsDBNull(31) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("AuthorizInvoiceNumber")),

                DriverFName = sqlDataReader.IsDBNull(0) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("FName")),
                DriverLName = sqlDataReader.IsDBNull(1) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("LName")),
                DriverCarType = sqlDataReader.IsDBNull(2) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("Name")),
                DriverCarImage = sqlDataReader.IsDBNull(3) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("ImagePath")),

                JobStatusID = sqlDataReader.IsDBNull(32) ? -1 : sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("JobStatusID"))
            });

        }


        sqlDataReader.Close();
        this.sqlCon.Close();
        return list;
    }

    [WebMethod]
    public List<Company> GetAllCompanies()
    {
        List<Company> list = new List<Company>();
        string cmdText = "SELECT dbo.Company.CompanyID, dbo.Company.CompanyName, dbo.Company.LimoAPIID, dbo.Company.LimoAPIKey,  dbo.Company.IsActive FROM dbo.Company WHERE ((dbo.Company.IsActive = 1) AND (dbo.Company.IsAvailable = 1) ) ";
        if (this.sqlCon.State != ConnectionState.Open)
            this.sqlCon.Open();
        SqlDataReader sqlDataReader = new SqlCommand(cmdText, this.sqlCon).ExecuteReader();
        while (sqlDataReader.Read())
        {
            Company company = new Company();
            company.CompanyID = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("CompanyID"));
            company.CompanyName = sqlDataReader.GetString(sqlDataReader.GetOrdinal("CompanyName"));
            string str1 = "";
            string str2 = "";
            if (sqlDataReader.GetString(sqlDataReader.GetOrdinal("LimoAPIID")) != null)
                str1 = sqlDataReader.GetString(sqlDataReader.GetOrdinal("LimoAPIID"));
            if (sqlDataReader.GetString(sqlDataReader.GetOrdinal("LimoAPIKey")) != null)
                str2 = sqlDataReader.GetString(sqlDataReader.GetOrdinal("LimoAPIKey"));
            company.LimoAPIID = str1;
            company.LimoAPIKey = str2;
            list.Add(company);
        }
        sqlDataReader.Close();
        this.sqlCon.Close();
        return list;
    }

    [ScriptMethod(UseHttpGet = true)]
    [WebMethod]
    public List<Driver> GetAllDrivers()
    {
        List<Driver> list = new List<Driver>();
        string cmdText = "SELECT Driver.FName, Driver.LName, Driver.Email, Driver.Phone, Driver.DeviceToken, dbo.Company.CompanyName, Driver.IsAvailable,  Driver.ShowOnMap, Driver.DrvierImagePath, dbo.Car.NoPassengers, dbo.Car.CarName, dbo.Car.Description, dbo.Car.PhotoName, dbo.Car.PricePerHore, dbo.Car.PricePerMaile, dbo.Car.CarNumber, DriverLocation.Lat, DriverLocation.Lng,Driver.DriverID FROM dbo.Car INNER JOIN  Driver ON dbo.Car.CarID = Driver.CarID INNER JOIN  DriverLocation ON Driver.DriverID = DriverLocation.DriverID INNER JOIN  dbo.Company ON dbo.Car.CompanyID = dbo.Company.CompanyID AND dbo.Car.CompanyID = dbo.Company.CompanyID AND  Driver.CompanyID = dbo.Company.CompanyID WHERE (Driver.ShowOnMap = 1)";
        if (this.sqlCon.State != ConnectionState.Open)
            this.sqlCon.Open();
        SqlDataReader sqlDataReader1 = new SqlCommand(cmdText, this.sqlCon).ExecuteReader();
        while (sqlDataReader1.Read())
        {
            Driver driver = new Driver();
            string str1 = "";
            if (!sqlDataReader1.IsDBNull(10))
                str1 = sqlDataReader1.GetString(sqlDataReader1.GetOrdinal("CarName"));
            driver.CarName = str1;
            string str2 = "";
            if (!sqlDataReader1.IsDBNull(11))
                str2 = sqlDataReader1.GetString(sqlDataReader1.GetOrdinal("Description"));
            driver.Description = str2;
            int num1 = -1;
            if (!sqlDataReader1.IsDBNull(9))
                num1 = sqlDataReader1.GetInt32(sqlDataReader1.GetOrdinal("NoPassengers"));
            driver.NoPassengers = num1;
            string str3 = "";
            if (!sqlDataReader1.IsDBNull(12))
                str3 = sqlDataReader1.GetString(sqlDataReader1.GetOrdinal("PhotoName"));
            driver.PhotoName = str3;
            float num2 = -1f;
            if (!sqlDataReader1.IsDBNull(13))
                num2 = (float)sqlDataReader1.GetDouble(sqlDataReader1.GetOrdinal("PricePerHore"));
            driver.PricePerHore = num2;
            float num3 = -1f;
            if (!sqlDataReader1.IsDBNull(14))
                num3 = (float)sqlDataReader1.GetDouble(sqlDataReader1.GetOrdinal("PricePerMaile"));
            driver.PricePerMaile = num3;
            string str4 = "";
            if (!sqlDataReader1.IsDBNull(0))
                str4 = sqlDataReader1.GetString(sqlDataReader1.GetOrdinal("FName"));
            driver.FName = str4;
            string str5 = "";
            if (!sqlDataReader1.IsDBNull(1))
                str5 = sqlDataReader1.GetString(sqlDataReader1.GetOrdinal("LName"));
            driver.LName = str5;
            string str6 = "";
            if (!sqlDataReader1.IsDBNull(2))
                str6 = sqlDataReader1.GetString(sqlDataReader1.GetOrdinal("Email"));
            driver.Email = str6;
            string str7 = "";
            if (!sqlDataReader1.IsDBNull(3))
                str7 = sqlDataReader1.GetString(sqlDataReader1.GetOrdinal("Phone"));
            driver.Phone = str7;
            string str8 = "";
            if (!sqlDataReader1.IsDBNull(4))
                str8 = sqlDataReader1.GetString(sqlDataReader1.GetOrdinal("DeviceToken"));
            driver.DeviceToken = str8;
            bool flag1 = false;
            if (!sqlDataReader1.IsDBNull(7))
                flag1 = sqlDataReader1.GetBoolean(sqlDataReader1.GetOrdinal("ShowOnMap"));
            driver.ShowOnMap = flag1;
            string str9 = "";
            if (!sqlDataReader1.IsDBNull(16))
                str9 = sqlDataReader1.GetString(sqlDataReader1.GetOrdinal("Lat"));
            driver.Lat = str9;
            string str10 = "";
            if (!sqlDataReader1.IsDBNull(17))
                str10 = sqlDataReader1.GetString(sqlDataReader1.GetOrdinal("Lng"));
            driver.Lng = str10;
            bool flag2 = false;
            if (!sqlDataReader1.IsDBNull(14))
                flag2 = sqlDataReader1.GetBoolean(sqlDataReader1.GetOrdinal("IsAvailable"));
            driver.JobStatus = flag2;
            string str11 = "";
            if (!sqlDataReader1.IsDBNull(8))
                str11 = sqlDataReader1.GetString(sqlDataReader1.GetOrdinal("DrvierImagePath"));
            driver.DrvierImagePath = str11;
            int num4 = -1;
            if (!sqlDataReader1.IsDBNull(18))
                num4 = sqlDataReader1.GetInt32(sqlDataReader1.GetOrdinal("DriverID"));
            driver.DriverID = num4;
            list.Add(driver);
        }
        sqlDataReader1.Close();
        for (int index = 0; index < list.Count; ++index)
        {
            SqlDataReader sqlDataReader2 = new SqlCommand("SELECT    SUM(tbRate.Rate) as totalRate , COUNT(tbRate.Rate) as totalCount FROM  Driver INNER JOIN tbRate ON Driver.DriverID = tbRate.DriverID AND Driver.DriverID = tbRate.DriverID AND Driver.DriverID = tbRate.DriverID AND Driver.DriverID = " + (object)list[index].DriverID, this.sqlCon).ExecuteReader();
            int num1 = -1;
            int num2 = -1;
            while (sqlDataReader2.Read())
            {
                if (!sqlDataReader2.IsDBNull(0))
                {
                    num1 = sqlDataReader2.GetInt32(sqlDataReader2.GetOrdinal("totalRate"));
                    num2 = sqlDataReader2.GetInt32(sqlDataReader2.GetOrdinal("totalCount"));
                }
            }
            double num3 = (double)(num1 / num2);
            list[index].TotalRate = num3.ToString();
        }
        this.sqlCon.Close();
        return list;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public List<DriverLocation> GetDriverLocation()
    {
        List<DriverLocation> list = new List<DriverLocation>();
        string cmdText = "Select * FROM DriverLocation";
        if (this.sqlCon.State != ConnectionState.Open)
            this.sqlCon.Open();
        SqlDataReader sqlDataReader = new SqlCommand(cmdText, this.sqlCon).ExecuteReader();
        while (sqlDataReader.Read())
            list.Add(new DriverLocation()
            {
                DriverLocationID = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("DriverLocationID")),
                Lat = sqlDataReader.GetString(sqlDataReader.GetOrdinal("Lat")),
                Lng = sqlDataReader.GetString(sqlDataReader.GetOrdinal("Lng")),
                DriverID = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("DriverID"))
            });
        sqlDataReader.Close();
        this.sqlCon.Close();
        return list;
    }

    [ScriptMethod(UseHttpGet = true)]
    [WebMethod]
    public string UpdateDriverLocation(string Lat, string Lng, int DriverID)
    {
        string cmdText1 = "UPDATE maged_LimoDB.DriverLocation SET maged_LimoDB.DriverLocation.Lat = N'" + Lat + "', maged_LimoDB.DriverLocation.Lng = N'" + Lng + "' WHERE (maged_LimoDB.DriverLocation.DriverID = " + DriverID.ToString() + ")";
        if (this.sqlCon.State != ConnectionState.Open)
            this.sqlCon.Open();
        int num = new SqlCommand(cmdText1, this.sqlCon).ExecuteNonQuery();
        if (num <= 0)
        {
            string cmdText2 = "Insert into maged_LimoDB.DriverLocation (maged_LimoDB.DriverLocation.Lat,maged_LimoDB.DriverLocation.Lng,maged_LimoDB.DriverLocation.DriverID) values(N'" + Lat + "',N'" + Lng + "' ," + DriverID.ToString() + ")";
            if (this.sqlCon.State != ConnectionState.Open)
                this.sqlCon.Open();
            num = new SqlCommand(cmdText2, this.sqlCon).ExecuteNonQuery();
        }
        string str = num.ToString();
        this.sqlCon.Close();
        return str;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string ChangeDriverStatus(int jobID, int JobStatusID, int DriverID)
    {
        string cmdText = "UPDATE dbo.Job SET JobStatusID = " + JobStatusID.ToString() + " WHERE (JobID = " + jobID.ToString() + ") AND (DriverID = " + DriverID.ToString() + ")";
        if (this.sqlCon.State != ConnectionState.Open)
            this.sqlCon.Open();
        string str = new SqlCommand(cmdText, this.sqlCon).ExecuteNonQuery().ToString();
        this.sqlCon.Close();
        return str;
    }

    [ScriptMethod(UseHttpGet = true)]
    [WebMethod]
    public string ChangeDriverWorkStatus(bool StatusID, int DriverID)
    {
        string cmdText = "UPDATE maged_LimoDB.Driver SET maged_LimoDB.Driver.ShowOnMap = '" + StatusID.ToString() + "' WHERE  (maged_LimoDB.Driver.DriverID = " + DriverID.ToString() + ")";
        if (this.sqlCon.State != ConnectionState.Open)
            this.sqlCon.Open();
        string str = new SqlCommand(cmdText, this.sqlCon).ExecuteNonQuery().ToString();
        this.sqlCon.Close();
        return str;
    }

    [ScriptMethod(UseHttpGet = true)]
    [WebMethod]
    public string DriverStatus(bool StatusID, int DriverID)
    {
        string cmdText = "UPDATE Driver SET IsAvailable = '" + StatusID.ToString() + "' WHERE  (DriverID = " + DriverID.ToString() + ")";
        if (this.sqlCon.State != ConnectionState.Open)
            this.sqlCon.Open();
        string str = new SqlCommand(cmdText, this.sqlCon).ExecuteNonQuery().ToString();
        this.sqlCon.Close();
        return str;
    }

    [ScriptMethod(UseHttpGet = true)]
    [WebMethod]
    public string assignDriverToJob(int jobID, int JobStatusID, int DriverID)
    {
        string cmdText = "UPDATE dbo.Job SET JobStatusID = " + (object)JobStatusID + ", DriverID = " + (string)(object)DriverID + " WHERE (JobID = " + (string)(object)jobID + ")";
        if (this.sqlCon.State != ConnectionState.Open)
            this.sqlCon.Open();
        string str = new SqlCommand(cmdText, this.sqlCon).ExecuteNonQuery().ToString();
        this.sqlCon.Close();
        return str;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string registerDriver(string FName, string LName, string Phone, string Email, string Password, int StatusID, int CarTypeID, string DeviceToken, int CompanyID)
    {
        string cmdText = "INSERT INTO Driver (FName, LName, Phone, Email, Password, Status, CarTypeID, DeviceToken, CompanyID)VALUES (N'" + (object)FName + "', N'" + LName + "', N'" + Phone + "', N'" + Email + "', N'" + Password + "', " + (string)(object)StatusID + ", " + (string)(object)CarTypeID + ", N'" + DeviceToken + "', " + (string)(object)CompanyID + ")";
        if (this.sqlCon.State != ConnectionState.Open)
            this.sqlCon.Open();
        string str = new SqlCommand(cmdText, this.sqlCon).ExecuteNonQuery().ToString();
        this.sqlCon.Close();
        return str;
    }

    #region DriverLogin

    [WebMethod]

    [ScriptMethod(UseHttpGet = true)]
    public List<DriverLogin> DriverLogin(string Phone, string Password, string driverToken, string devicetype)
    {
        int nDriverId = 0;
        List<DriverLogin> list = new List<DriverLogin>();
        string cmdText2 = "SELECT  maged_LimoDB.Driver.Phone, maged_LimoDB.Driver.DriverID,maged_LimoDB.Driver.Password,dbo.Car.CarID, dbo.Car.CarName, dbo.Car.NoPassengers, dbo.Car.PricePerHore, dbo.Car.PricePerMaile, dbo.Car.CarNumber FROM dbo.Car INNER JOIN maged_LimoDB.Driver ON dbo.Car.CarID = maged_LimoDB.Driver.CarID  WHERE (maged_LimoDB.Driver.Phone = N'" + Phone + "') AND (maged_LimoDB.Driver.Password = N'" + Password + "')";
        if (this.sqlCon.State != ConnectionState.Open)
            this.sqlCon.Open();
        SqlDataReader sqldr = new SqlCommand(cmdText2, this.sqlCon).ExecuteReader();
        while (sqldr.Read())
        {
            nDriverId = sqldr.GetInt32(sqldr.GetOrdinal("DriverID"));
            list.Add(new DriverLogin()
            {
                CarName = sqldr.GetString(sqldr.GetOrdinal("CarName")),
                DriverID = sqldr.GetInt32(sqldr.GetOrdinal("DriverID")),
                CarNumber = sqldr.GetString(sqldr.GetOrdinal("CarNumber")),
                NoPassengers = sqldr.GetInt32(sqldr.GetOrdinal("NoPassengers")),
                PricePerHore = (float)sqldr.GetDouble(sqldr.GetOrdinal("PricePerHore")),
                PricePerMaile = (float)sqldr.GetDouble(sqldr.GetOrdinal("PricePerMaile")),
                CarID = sqldr.GetInt32(sqldr.GetOrdinal("CarID"))
            });
        }
        sqldr.Close();
        if (nDriverId > 0)
        {
            string cmdText3 = "UPDATE maged_LimoDB.Driver SET ShowOnMap = 'true' ,DeviceType='"+ devicetype +"' WHERE  (maged_LimoDB.Driver.DriverID = " + nDriverId.ToString() + ")";
            if (this.sqlCon.State != ConnectionState.Open)
                this.sqlCon.Open();
            new SqlCommand(cmdText3, this.sqlCon).ExecuteNonQuery();
        }

        this.sqlCon.Close();
        return list;
    }

    #endregion

    #region DriverCLogin
    [ScriptMethod(UseHttpGet = true)]
    [WebMethod]
    public List<DriverLogin> DriverCLogin(string Phone, string Password, string CompanyName)
    {
        string s = "-1";
        string cmdText1 = "SELECT * FROM Company WHERE (CompanyName = N'" + CompanyName + "')";
        if (this.sqlCon.State != ConnectionState.Open)
            this.sqlCon.Open();
        string str = "";
        SqlDataReader sqlDataReader1 = new SqlCommand(cmdText1, this.sqlCon).ExecuteReader();
        int int32;
        while (sqlDataReader1.Read())
        {
            int32 = sqlDataReader1.GetInt32(sqlDataReader1.GetOrdinal("CompanyID"));
            str = int32.ToString();
        }
        sqlDataReader1.Close();
        List<DriverLogin> list = new List<DriverLogin>();
        string cmdText2 = "SELECT  maged_LimoDB.Driver.Phone, maged_LimoDB.Driver.DriverID,maged_LimoDB.Driver.Password,dbo.Car.CarID, dbo.Car.CarName, dbo.Car.NoPassengers, dbo.Car.PricePerHore, dbo.Car.PricePerMaile, dbo.Car.CarNumber FROM dbo.Car INNER JOIN maged_LimoDB.Driver ON dbo.Car.CarID = maged_LimoDB.Driver.CarID  WHERE (maged_LimoDB.Driver.Phone = N'" + Phone + "') AND (maged_LimoDB.Driver.Password = N'" + Password + "') AND (maged_LimoDB.Driver.CompanyID = " + str + ")";
        if (this.sqlCon.State != ConnectionState.Open)
            this.sqlCon.Open();
        SqlDataReader sqlDataReader2 = new SqlCommand(cmdText2, this.sqlCon).ExecuteReader();
        while (sqlDataReader2.Read())
        {
            int32 = sqlDataReader2.GetInt32(sqlDataReader2.GetOrdinal("DriverID"));
            s = int32.ToString();
            list.Add(new DriverLogin()
            {
                CarName = sqlDataReader2.GetString(sqlDataReader2.GetOrdinal("CarName")),
                DriverID = sqlDataReader2.GetInt32(sqlDataReader2.GetOrdinal("DriverID")),
                CarNumber = sqlDataReader2.GetString(sqlDataReader2.GetOrdinal("CarNumber")),
                NoPassengers = sqlDataReader2.GetInt32(sqlDataReader2.GetOrdinal("NoPassengers")),
                PricePerHore = (float)sqlDataReader2.GetDouble(sqlDataReader2.GetOrdinal("PricePerHore")),
                PricePerMaile = (float)sqlDataReader2.GetDouble(sqlDataReader2.GetOrdinal("PricePerMaile")),
                CarID = sqlDataReader2.GetInt32(sqlDataReader2.GetOrdinal("CarID"))
            });
        }
        if (int.Parse(s) > 0)
        {
            sqlDataReader2.Close();
            string cmdText3 = "UPDATE maged_LimoDB.Driver SET ShowOnMap = 'true' WHERE  (maged_LimoDB.Driver.DriverID = " + s + ")";
            if (this.sqlCon.State != ConnectionState.Open)
                this.sqlCon.Open();
            new SqlCommand(cmdText3, this.sqlCon).ExecuteNonQuery();
        }
        this.sqlCon.Close();
        return list;
    }

    #endregion

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string SendNotifcationToDriver(string NotifcationMsg, int DriverID)
    {
        string str1 = "-1";
        SendNotificationToDevices(DriverID, NotifcationMsg, "");
        this.sqlCon.Close();
        return str1;
    }

    [WebMethod]
    [ScriptMethod(UseHttpGet = true)]
    public string UpdateDriverToken(string driverToken, string DriverID, string DeviceType)
    {
        string cmdText = "UPDATE maged_LimoDB.Driver SET maged_LimoDB.Driver.DeviceToken = N'" + driverToken + "' ,maged_LimoDB.Driver.DeviceType = N'" + DeviceType + "' WHERE (maged_LimoDB.Driver.DriverID = " + ((object)DriverID).ToString() + ")";
        if (this.sqlCon.State != ConnectionState.Open)
            this.sqlCon.Open();
        string str = new SqlCommand(cmdText, this.sqlCon).ExecuteNonQuery().ToString();
        this.sqlCon.Close();
        return str;
    }

    [ScriptMethod(UseHttpGet = true)]
    [WebMethod]
    public string UpdateSetting(string CPH, string CPM, string CarID, bool StatusID, string DriverID)
    {

        string cmdText = "UPDATE Car SET PricePerHore = N'" + CPH + "',PricePerMaile = N'" + CPM + "' WHERE (CarID = " + ((object)CarID).ToString() + ");"
            + "UPDATE maged_LimoDB.Driver SET maged_LimoDB.Driver.IsAvailable = '" + StatusID.ToString() + "' WHERE  (maged_LimoDB.Driver.DriverID = " + DriverID.ToString() + ")";
        if (this.sqlCon.State != ConnectionState.Open)
            this.sqlCon.Open();
        string str = new SqlCommand(cmdText, this.sqlCon).ExecuteNonQuery().ToString();
        this.sqlCon.Close();
        return str;
    }

    [ScriptMethod(UseHttpGet = true)]
    [WebMethod]
    public List<Job> AcceptCall(int nDriverID, int JobID, bool bAcceptStatus, string sCarId)
    {
        List<Job> list = new List<Job>();

        // check if call not accepted yet
        string cmdText = "SELECT maged_LimoDB.Driver.FName, maged_LimoDB.Driver.LName, Job.JobID " +
                           "FROM  Job INNER JOIN" +
                            " maged_LimoDB.Driver ON Job.DriverID = maged_LimoDB.Driver.DriverID" +
                            " WHERE     (Job.JobID = " + JobID.ToString() + ")";
        string str2 = "";
        if (this.sqlCon.State != ConnectionState.Open)
            this.sqlCon.Open();
        SqlDataReader sqlDataReader1 = new SqlCommand(cmdText, this.sqlCon).ExecuteReader();
        if (sqlDataReader1.HasRows)
        {
            str2 = "<AcceptCallStatus>False</AcceptCallStatus>";

            sqlDataReader1.Close();

            this.sqlCon.Close();

        }
        else
        {
            sqlDataReader1.Close();
            // Insert Driver to job and return Success
            string cmdText1 = "UPDATE Job SET DriverID = " + nDriverID.ToString() + " , IsDespath = 'false' ,CompanyCarID = " + sCarId + " , JobStatusID = 11 WHERE (JobID = " + JobID.ToString() + ")";

            if (this.sqlCon.State != ConnectionState.Open)
                this.sqlCon.Open();
            int num = new SqlCommand(cmdText1, this.sqlCon).ExecuteNonQuery();

            if (num <= 0)
            {
                // Error
                str2 = "<AcceptCallStatus>False</AcceptCallStatus>";
            }
            else
            {
                str2 = "<AcceptCallStatus>Success</AcceptCallStatus>";



            }
            string str = num.ToString();
            this.sqlCon.Close();
        }

        return list;
    }

    [WebMethod]
    public List<Job> AcceptCall2(int nDriverID, int JobID, bool bAcceptStatus, string sCarId)
    {
        List<Job> list = new List<Job>();

        // check if call not accepted yet
        string cmdText = "SELECT maged_LimoDB.Driver.FName, maged_LimoDB.Driver.LName, Job.JobID " +
                           "FROM  Job INNER JOIN" +
                            " maged_LimoDB.Driver ON Job.DriverID = maged_LimoDB.Driver.DriverID" +
                            " WHERE     (Job.JobID = " + JobID.ToString() + ")";
        string str2 = "";
        if (this.sqlCon.State != ConnectionState.Open)
            this.sqlCon.Open();
        SqlDataReader sqlDataReader1 = new SqlCommand(cmdText, this.sqlCon).ExecuteReader();
        if (sqlDataReader1.HasRows)
        {
            str2 = "<AcceptCallStatus>False</AcceptCallStatus>";

            sqlDataReader1.Close();

            this.sqlCon.Close();

        }
        else
        {
            sqlDataReader1.Close();
            // Insert Driver to job and return Success
            string cmdText1 = "UPDATE Job SET DriverID = " + nDriverID.ToString() + " , IsDespath = 'false' ,CompanyCarID = " + sCarId
                + " , JobStatusID = 11 WHERE (JobID = " + JobID.ToString() + ")";

            if (this.sqlCon.State != ConnectionState.Open)
                this.sqlCon.Open();
            int num = new SqlCommand(cmdText1, this.sqlCon).ExecuteNonQuery();

            if (num <= 0)
            {
                // Error
                str2 = "<AcceptCallStatus>False</AcceptCallStatus>";
            }
            else
            {
                str2 = "<AcceptCallStatus>Success</AcceptCallStatus>";

                DataTable dtJob = GetJobDetailsInfo(JobID);

                cmdText = "SELECT     maged_LimoDB.Driver.FName, maged_LimoDB.Driver.LName, maged_LimoDB.CarType.Name, " +
                         " maged_LimoDB.CarType.ImagePath, Job.* " +
                         " FROM Job INNER JOIN " +
                         " maged_LimoDB.Driver ON Job.DriverID = maged_LimoDB.Driver.DriverID INNER JOIN " +
                         " maged_LimoDB.CarType ON maged_LimoDB.Driver.CarTypeID = maged_LimoDB.CarType.CarTypeID " +
                         " Where  JobID = " + JobID.ToString();
                if (this.sqlCon.State != ConnectionState.Open)
                    this.sqlCon.Open();
                SqlDataReader sqlDataReader = new SqlCommand(cmdText, this.sqlCon).ExecuteReader();
                while (sqlDataReader.Read())
                {


                    list.Add(new Job()
                    {
                        JobID = JobID,
                        UserID = sqlDataReader.IsDBNull(5) ? -1 : sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("UserID")),
                        CompanyCarID = sqlDataReader.IsDBNull(6) ? -1 : sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("CompanyCarID")),
                        FromAddress = sqlDataReader.IsDBNull(7) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("FromAddress")),
                        ToAddress = sqlDataReader.IsDBNull(8) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("ToAddress")),
                        CityID = sqlDataReader.IsDBNull(9) ? -1 : sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("CityID")),
                        TimeHour = sqlDataReader.IsDBNull(10) ? -1 : sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("TimeHour")),
                        TimeMinute = sqlDataReader.IsDBNull(11) ? -1 : sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("TimeMinute")),
                        TotalPrice = sqlDataReader.IsDBNull(12) ? -1f : (float)sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("TotalPrice")),
                        EstimatedFarePrice = sqlDataReader.IsDBNull(13) ? -1f : (float)sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("EstimatedFarePrice")),
                        GratuityPrice = sqlDataReader.IsDBNull(14) ? -1f : (float)sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("GratuityPrice")),
                        ProcessingFee = sqlDataReader.IsDBNull(15) ? -1f : (float)sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("ProcessingFee")),
                        Taxes = sqlDataReader.IsDBNull(16) ? -1f : (float)sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("Taxes")),
                        OtherPrice = sqlDataReader.IsDBNull(17) ? -1f : (float)sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("OtherPrice")),
                        Notes = sqlDataReader.IsDBNull(18) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("Notes")),
                        LimoConfirmNumber = sqlDataReader.IsDBNull(19) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("LimoConfirmNumber")),
                        ComapnyConfirmNum = sqlDataReader.IsDBNull(20) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("ComapnyConfirmNum")),
                        FirstName = sqlDataReader.IsDBNull(21) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("FirstName")),
                        LastName = sqlDataReader.IsDBNull(22) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("LastName")),
                        Mobile = sqlDataReader.IsDBNull(23) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("Mobile")),
                        Email = sqlDataReader.IsDBNull(24) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("Email")),
                        JobDate = sqlDataReader.IsDBNull(21) ? "-1" : sqlDataReader.GetDateTime(sqlDataReader.GetOrdinal("JobDate")).ToString(),
                        JobTime = sqlDataReader.IsDBNull(22) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("JobTime")),
                        PriceNote = sqlDataReader.IsDBNull(27) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("PriceNote")),
                        DriverID = sqlDataReader.IsDBNull(28) ? -1 : sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("DriverID")),
                        TripConfirmationNumber = sqlDataReader.IsDBNull(29) ? -1 : sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("TripConfirmationNumber")),
                        AuthorizationCode = sqlDataReader.IsDBNull(30) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("AuthorizationCode")),
                        AuthorizInvoiceNumber = sqlDataReader.IsDBNull(31) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("AuthorizInvoiceNumber")),

                        DriverFName = sqlDataReader.IsDBNull(0) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("FName")),
                        DriverLName = sqlDataReader.IsDBNull(1) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("LName")),
                        DriverCarType = sqlDataReader.IsDBNull(2) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("Name")),
                        DriverCarImage = sqlDataReader.IsDBNull(3) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("ImagePath")),

                        JobStatusID = sqlDataReader.IsDBNull(32) ? -1 : sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("JobStatusID"))
                    });

                }


                sqlDataReader.Close();
                this.sqlCon.Close();

            }
            string str = num.ToString();
            this.sqlCon.Close();
        }
        sqlCon.Close();
        return list;
    }

    [WebMethod]
    public string AcceptCall3(int nDriverID, int JobID, bool bAcceptStatus, string sCarId)
    {
        List<Job> list = new List<Job>();

        // check if call not accepted yet
        string cmdText = "SELECT maged_LimoDB.Driver.FName, maged_LimoDB.Driver.LName, Job.JobID " +
                           "FROM  Job INNER JOIN" +
                            " maged_LimoDB.Driver ON Job.DriverID = maged_LimoDB.Driver.DriverID" +
                            " WHERE     (Job.JobID = " + JobID.ToString() + ")";
        string str2 = "";
        if (this.sqlCon.State != ConnectionState.Open)
            this.sqlCon.Open();
        SqlDataReader sqlDataReader1 = new SqlCommand(cmdText, this.sqlCon).ExecuteReader();
        if (sqlDataReader1.HasRows)
        {
            str2 = "<AcceptCallStatus>False</AcceptCallStatus>";
            //while (sqlDataReader1.Read())
            //{
            //    if (!sqlDataReader1.IsDBNull(24))
            //    {
            //        //str2 = sqlDataReader1.GetString(sqlDataReader1.GetOrdinal("DriverID"));
            //        str2 = "<AcceptCallStatus>False</AcceptCallStatus>";

            //    }
            //    else
            //    {
            //        str2 = "<AcceptCallStatus>False</AcceptCallStatus>";
            //    }
            //}

            sqlDataReader1.Close();

            this.sqlCon.Close();

        }
        else
        {
            sqlDataReader1.Close();
            // Insert Driver to job and return Success
            string cmdText1 = "UPDATE Job SET DriverID = " + nDriverID.ToString() + " , IsDespath = 'false' ,CompanyCarID = " + sCarId + " , JobStatusID = 11 WHERE (JobID = " + JobID.ToString() + ")";

            if (this.sqlCon.State != ConnectionState.Open)
                this.sqlCon.Open();
            int num = new SqlCommand(cmdText1, this.sqlCon).ExecuteNonQuery();

            if (num <= 0)
            {
                // Error
                str2 = "<AcceptCallStatus>False</AcceptCallStatus>";
            }
            else
            {
                str2 = "<AcceptCallStatus>Success</AcceptCallStatus>";

                DataTable dtJob = GetJobDetailsInfo(JobID);

                cmdText = "SELECT     maged_LimoDB.Driver.FName, maged_LimoDB.Driver.LName, maged_LimoDB.CarType.Name, " +
                                 " maged_LimoDB.CarType.ImagePath, Job.* " +
                                 " FROM Job INNER JOIN " +
                                 " maged_LimoDB.Driver ON Job.DriverID = maged_LimoDB.Driver.DriverID INNER JOIN " +
                                 " maged_LimoDB.CarType ON maged_LimoDB.Driver.CarTypeID = maged_LimoDB.CarType.CarTypeID " +
                                 " Where  JobID = " + JobID.ToString();
                if (this.sqlCon.State != ConnectionState.Open)
                    this.sqlCon.Open();
                SqlDataReader sqlDataReader = new SqlCommand(cmdText, this.sqlCon).ExecuteReader();
                while (sqlDataReader.Read())
                {
                    list.Add(new Job()
                    {
                        UserID = sqlDataReader.IsDBNull(5) ? -1 : sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("UserID")),
                        CompanyCarID = sqlDataReader.IsDBNull(6) ? -1 : sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("CompanyCarID")),
                        FromAddress = sqlDataReader.IsDBNull(7) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("FromAddress")),
                        ToAddress = sqlDataReader.IsDBNull(8) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("ToAddress")),
                        CityID = sqlDataReader.IsDBNull(9) ? -1 : sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("CityID")),
                        TimeHour = sqlDataReader.IsDBNull(10) ? -1 : sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("TimeHour")),
                        TimeMinute = sqlDataReader.IsDBNull(11) ? -1 : sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("TimeMinute")),
                        TotalPrice = sqlDataReader.IsDBNull(12) ? -1f : (float)sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("TotalPrice")),
                        EstimatedFarePrice = sqlDataReader.IsDBNull(13) ? -1f : (float)sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("EstimatedFarePrice")),
                        GratuityPrice = sqlDataReader.IsDBNull(14) ? -1f : (float)sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("GratuityPrice")),
                        ProcessingFee = sqlDataReader.IsDBNull(15) ? -1f : (float)sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("ProcessingFee")),
                        Taxes = sqlDataReader.IsDBNull(16) ? -1f : (float)sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("Taxes")),
                        OtherPrice = sqlDataReader.IsDBNull(17) ? -1f : (float)sqlDataReader.GetDouble(sqlDataReader.GetOrdinal("OtherPrice")),
                        Notes = sqlDataReader.IsDBNull(18) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("Notes")),
                        LimoConfirmNumber = sqlDataReader.IsDBNull(19) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("LimoConfirmNumber")),
                        ComapnyConfirmNum = sqlDataReader.IsDBNull(20) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("ComapnyConfirmNum")),
                        FirstName = sqlDataReader.IsDBNull(21) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("FirstName")),
                        LastName = sqlDataReader.IsDBNull(22) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("LastName")),
                        Mobile = sqlDataReader.IsDBNull(23) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("Mobile")),
                        Email = sqlDataReader.IsDBNull(24) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("Email")),
                        JobDate = sqlDataReader.IsDBNull(25) ? "-1" : sqlDataReader.GetDateTime(sqlDataReader.GetOrdinal("JobDate")).ToString(),
                        JobTime = sqlDataReader.IsDBNull(26) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("JobTime")),
                        PriceNote = sqlDataReader.IsDBNull(27) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("PriceNote")),
                        DriverID = sqlDataReader.IsDBNull(28) ? -1 : sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("DriverID")),
                        TripConfirmationNumber = sqlDataReader.IsDBNull(29) ? -1 : sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("TripConfirmationNumber")),
                        AuthorizationCode = sqlDataReader.IsDBNull(30) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("AuthorizationCode")),
                        AuthorizInvoiceNumber = sqlDataReader.IsDBNull(31) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("AuthorizInvoiceNumber")),

                        DriverFName = sqlDataReader.IsDBNull(0) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("FName")),
                        DriverLName = sqlDataReader.IsDBNull(1) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("LName")),
                        DriverCarType = sqlDataReader.IsDBNull(2) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("Name")),
                        DriverCarImage = sqlDataReader.IsDBNull(3) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("ImagePath")),

                        JobStatusID = sqlDataReader.IsDBNull(32) ? -1 : sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("JobStatusID"))
                    });

                }


                sqlDataReader.Close();
                this.sqlCon.Close();

            }
            string str = num.ToString();
            this.sqlCon.Close();
        }

        return str2;
    }

    #region GetDriversNearMe


    [WebMethod]
    public XmlDocument GetDriversNearMe(string Longitude, string Latitude, string DistanceWithMile, int nJobId)
    {
        XmlDocument xmlDocument = new XmlDocument();
        try
        {
            StringBuilder sb = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sb);

            writer.WriteStartDocument();
            #region ROOT XML
            writer.WriteStartElement("root");
            DataTable dt = GetDrivers(Longitude, Latitude, DistanceWithMile);
            DataTable dtJob = GetJobDetailsInfoForNotifications(nJobId);
            string sNotificationMsg = "";

            sNotificationMsg = "newjob-" + dtJob.Rows[0]["FromAddress"].ToString() + "-" + dtJob.Rows[0]["ToAddress"].ToString() + "-" +
                dtJob.Rows[0]["TotalPrice"].ToString() + "-" + nJobId.ToString() + "-newcall" + "-" + dtJob.Rows[0]["JobDate"].ToString() + "-" + dtJob.Rows[0]["JobTime"].ToString();


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // get job details for job sent 

                SendNotificationToDevices(int.Parse(dt.Rows[i]["DriverID"].ToString()), sNotificationMsg, "NewCall");

                writer.WriteStartElement("Drivers");
                writer.WriteElementString("DriverId", dt.Rows[i]["DriverID"].ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();//End root

            #endregion
            writer.WriteEndDocument();
            writer.Flush();
            xmlDocument.LoadXml(sb.ToString());
        }
        catch (Exception ex)
        {
            log.Add2ExLog(ex.Message + " " + ex.Source + " " + ex.InnerException);
        }

        return xmlDocument;
    }

    #endregion

    [WebMethod]
    public XmlDocument SendMessgaeToDrivers(string sMessage)
    {
        XmlDocument xmlDocument = new XmlDocument();
        try
        {
            StringBuilder sb = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sb);

            writer.WriteStartDocument();
            #region ROOT XML
            writer.WriteStartElement("root");
            DataTable dt = GetAllDriversSP();
            string sNotificationMsg = "";

            sNotificationMsg = "newmessage-" + sMessage;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // get job details for job sent 

                SendNotificationToDevices(int.Parse(dt.Rows[i]["DriverID"].ToString()), sNotificationMsg, "New Message");

                writer.WriteStartElement("Drivers");
                writer.WriteElementString("DriverId", dt.Rows[i]["DriverID"].ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();//End root

            #endregion
            writer.WriteEndDocument();
            writer.Flush();
            xmlDocument.LoadXml(sb.ToString());
        }
        catch (Exception ex)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(ex.Message + " " + ex.Source + " " + ex.InnerException);
            xmlDocument.LoadXml(sb.ToString());
            return xmlDocument;
            //log.Add2ExLog(ex.Message + " " + ex.Source + " " + ex.InnerException);
        }

        return xmlDocument;
    }

    #region SendJobToDriver
    [WebMethod]
    public XmlDocument SendJobToDriver(int nDriverID, int nJobId)
    {
        XmlDocument xmlDocument = new XmlDocument();
        try
        {
            StringBuilder sb = new StringBuilder();
            XmlWriter writer = XmlWriter.Create(sb);

            writer.WriteStartDocument();
            #region ROOT XML
            writer.WriteStartElement("root");
            DataTable dt = GetDriverDetails(nDriverID);
            DataTable dtJob = GetJobDetailsInfoForNotifications(nJobId);
            string sNotificationMsg = "";

            sNotificationMsg = "newjob-" + dtJob.Rows[0]["FromAddress"].ToString() + "-" + dtJob.Rows[0]["ToAddress"].ToString() + "-" +
                dtJob.Rows[0]["TotalPrice"].ToString() + "-" + nJobId.ToString() + "-newcall" + "-" + dtJob.Rows[0]["JobDate"].ToString() + "-" + dtJob.Rows[0]["JobTime"].ToString();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                // get job details for job sent 

                SendNotificationToDevices(int.Parse(dt.Rows[i]["DriverID"].ToString()), sNotificationMsg, "NewCall");

                writer.WriteStartElement("Drivers");
                writer.WriteElementString("DriverId", dt.Rows[i]["DriverID"].ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();//End root

            #endregion
            writer.WriteEndDocument();
            writer.Flush();
            xmlDocument.LoadXml(sb.ToString());
        }
        catch (Exception ex)
        {
            log.Add2ExLog(ex.Message + " " + ex.Source + " " + ex.InnerException);
        }

        return xmlDocument;
    }

    #endregion

    private DataTable GetDrivers(string Longitude, string Latitude, string DistanceWithMile)
    {
        DataTable catDT = new DataTable();
        try
        {
            SqlCommand Sqlcomm = new SqlCommand();
            Sqlcomm.Connection = sqlCon;
            Sqlcomm.CommandType = CommandType.StoredProcedure;
            Sqlcomm.CommandText = "SP_GetDriversInRange";
            Sqlcomm.Parameters.Add("@Latitude", SqlDbType.Decimal);
            Sqlcomm.Parameters["@Latitude"].Value = Decimal.Parse(Latitude);
            Sqlcomm.Parameters.Add("@Longitude", SqlDbType.Decimal);
            Sqlcomm.Parameters["@Longitude"].Value = Decimal.Parse(Longitude);
            Sqlcomm.Parameters.Add("@DistanceWithMile", SqlDbType.Decimal);
            Sqlcomm.Parameters["@DistanceWithMile"].Value = Decimal.Parse(DistanceWithMile);
            Sqlcomm.Connection = sqlCon;
            SqlDataAdapter DA = new SqlDataAdapter(Sqlcomm);
            DA.Fill(catDT);
        }
        catch (Exception ex)
        {
            string s = ex.Message + " " + ex.Source + " " + ex.InnerException;
            //log.Add2ExLog(ex.Message + " " + ex.Source + " " + ex.InnerException);
        }
        return catDT;
    }



    private DataTable GetAllDriversSP()
    {
        DataTable catDT = new DataTable();
        try
        {
            SqlCommand Sqlcomm = new SqlCommand();
            Sqlcomm.Connection = sqlCon;
            Sqlcomm.CommandType = CommandType.StoredProcedure;
            Sqlcomm.CommandText = "SP_GetAllDrivers";
            Sqlcomm.Connection = sqlCon;
            SqlDataAdapter DA = new SqlDataAdapter(Sqlcomm);
            DA.Fill(catDT);
        }
        catch (Exception ex)
        {
            string s = ex.Message + " " + ex.Source + " " + ex.InnerException;
            //log.Add2ExLog(ex.Message + " " + ex.Source + " " + ex.InnerException);
        }
        return catDT;
    }


    private DataTable GetDriverDetails(int nDriverId)
    {
        DataTable catDT = new DataTable();
        try
        {
            SqlCommand Sqlcomm = new SqlCommand();
            Sqlcomm.Connection = sqlCon;
            Sqlcomm.CommandType = CommandType.StoredProcedure;
            Sqlcomm.CommandText = "SP_GetDriverDetails";
            Sqlcomm.Parameters.Add("@DriverID", SqlDbType.Int);
            Sqlcomm.Parameters["@DriverID"].Value = Decimal.Parse(nDriverId.ToString());
            Sqlcomm.Connection = sqlCon;
            SqlDataAdapter DA = new SqlDataAdapter(Sqlcomm);
            DA.Fill(catDT);
        }
        catch (Exception ex)
        {
            string s = ex.Message + " " + ex.Source + " " + ex.InnerException;
            //log.Add2ExLog(ex.Message + " " + ex.Source + " " + ex.InnerException);
        }
        return catDT;
    }

    private DataTable GetJobDetailsInfoForNotifications(int nJobId)
    {
        DataTable catDT = new DataTable();
        try
        {
            SqlCommand Sqlcomm = new SqlCommand();
            Sqlcomm.Connection = sqlCon;
            Sqlcomm.CommandType = CommandType.StoredProcedure;
            Sqlcomm.CommandText = "SP_GetJobInfo";
            Sqlcomm.Parameters.Add("@JobID", SqlDbType.Int);
            Sqlcomm.Parameters["@JobID"].Value = Int32.Parse(nJobId.ToString());
            Sqlcomm.Connection = sqlCon;
            SqlDataAdapter DA = new SqlDataAdapter(Sqlcomm);
            DA.Fill(catDT);
        }
        catch (Exception ex)
        {
            //log.Add2ExLog(ex.Message + " " + ex.Source + " " + ex.InnerException);
        }
        return catDT;
    }

    private DataTable GetJobDetailsInfo(int nJobId)
    {
        DataTable catDT = new DataTable();
        try
        {
            SqlCommand Sqlcomm = new SqlCommand();
            Sqlcomm.Connection = sqlCon;
            Sqlcomm.CommandType = CommandType.StoredProcedure;
            Sqlcomm.CommandText = "SP_GetJobDetails";
            Sqlcomm.Parameters.Add("@JobID", SqlDbType.Int);
            Sqlcomm.Parameters["@JobID"].Value = Int32.Parse(nJobId.ToString());
            Sqlcomm.Connection = sqlCon;
            SqlDataAdapter DA = new SqlDataAdapter(Sqlcomm);
            DA.Fill(catDT);
        }
        catch (Exception ex)
        {
            //log.Add2ExLog(ex.Message + " " + ex.Source + " " + ex.InnerException);
        }
        return catDT;
    }

    private string SendNotificationToDevices(int DriverID, string sNotifcationMsg, string smsgHeader)
    {
        string deviceType = "";
        string str1 = "-1";
        string cmdText = "SELECT * FROM maged_LimoDB.Driver WHERE (DriverID = " + DriverID.ToString() + ")";
        if (this.sqlCon.State != ConnectionState.Open)
            this.sqlCon.Open();
        SqlDataReader sqlDataReader = new SqlCommand(cmdText, this.sqlCon).ExecuteReader();
        string str2 = "";
        while (sqlDataReader.Read())
        {
            str2 = sqlDataReader.IsDBNull(7) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("DeviceToken"));
            deviceType = sqlDataReader.IsDBNull(14) ? "-1" : sqlDataReader.GetString(sqlDataReader.GetOrdinal("DeviceType"));
        }

        if (deviceType == "android")
        {
            SendCommandToPhone(sNotifcationMsg, str2);
            //SendGCMNotification(sNotifcationMsg,str2);
            //string folder = Server.MapPath("~/Php/");
            //new WebClient().DownloadString("http://dailyinfo.visions-tech.net/Limo/sendNo.php?DeviceToken=" + str2 + "&MSG=" + sNotifcationMsg);
            /*
            WebClient client = new WebClient();

            client.Proxy = new WebProxy(new Uri("http://proxy.dailyinfo.visions-tech.net:8080"), true);

            client.DownloadString("http://dailyinfo.visions-tech.net/Limo/sendNo.php?DeviceToken=" + str2 + "&MSG=" + sNotifcationMsg);
            */
            //new WebClient().DownloadString("http://dailyinfo.visions-tech.net/Limo/sendNo.php?DeviceToken=" + str2 + "&MSG=" + sNotifcationMsg);
        }

        else if (deviceType == "Iphone")
        {
            ArrayList list = new ArrayList();
            list.AddRange(sNotifcationMsg.Split(new char[] { '-' }));
            
            sNotifcationMsg = list[list.Count - 1].ToString() + "-" + list[list.Count - 2].ToString();
            new WebClient().DownloadString("http://limoallover.net/Php/SendIphoneNotifcation.php?DeviceToken="
                + str2 + "&MSG=" + sNotifcationMsg + "&msgHeader=" + smsgHeader);
             
        }

        this.sqlCon.Close();
        return str1;
    }

    [ScriptMethod(UseHttpGet = true)]
    [WebMethod]
    public int UpdateJobStatus(string Lat, string Lng, int DriverID, int nJobStatus, int nJobId)
    {
        int myReturn = new int();
        try
        {
            SqlCommand Sqlcomm = new SqlCommand();
            Sqlcomm.Connection = sqlCon;
            Sqlcomm.CommandType = CommandType.StoredProcedure;
            Sqlcomm.CommandText = "SP_UpdateJobStatus";
            Sqlcomm.Parameters.Add("@JobID", SqlDbType.Int);
            Sqlcomm.Parameters["@JobId"].Value = Int32.Parse(nJobId.ToString());

            Sqlcomm.Parameters.Add("@DriverId", SqlDbType.Int);
            Sqlcomm.Parameters["@DriverId"].Value = Int32.Parse(DriverID.ToString());

            Sqlcomm.Parameters.Add("@JobStatusId", SqlDbType.Int);
            Sqlcomm.Parameters["@JobStatusId"].Value = Int32.Parse(nJobStatus.ToString());

            Sqlcomm.Parameters.Add("@Lat", SqlDbType.NVarChar);
            Sqlcomm.Parameters["@Lat"].Value = Lat.ToString();

            Sqlcomm.Parameters.Add("@Lng", SqlDbType.NVarChar);
            Sqlcomm.Parameters["@Lng"].Value = Lng.ToString();

            sqlCon.Open();
            myReturn = Sqlcomm.ExecuteNonQuery();
            sqlCon.Close();
        }
        catch (Exception ex)
        {
            //log.Add2ExLog(ex.Message + " " + ex.Source + " " + ex.InnerException);
        }

        sqlCon.Close();
        return myReturn;

    }


    [ScriptMethod(UseHttpGet = true)]
    [WebMethod]
    public int UpdateJobExtraValues(string sTolls, string sParking, string sExtraGr, string sMisc1, string sMisc2, string sOtherPrice, int nJobId)
    {
        int myReturn = new int();
        try
        {
            SqlCommand Sqlcomm = new SqlCommand();
            Sqlcomm.Connection = sqlCon;
            Sqlcomm.CommandType = CommandType.StoredProcedure;
            Sqlcomm.CommandText = "SP_UpdateJobExtraValues";
            Sqlcomm.Parameters.Add("@JobID", SqlDbType.Int);
            Sqlcomm.Parameters["@JobId"].Value = Int32.Parse(nJobId.ToString());

            Sqlcomm.Parameters.Add("@Tolls", SqlDbType.Float);
            Sqlcomm.Parameters["@Tolls"].Value = Int32.Parse(sTolls.ToString());

            Sqlcomm.Parameters.Add("@Parking", SqlDbType.Float);
            Sqlcomm.Parameters["@Parking"].Value = Int32.Parse(sParking.ToString());

            Sqlcomm.Parameters.Add("@ExtraGr", SqlDbType.Float);
            Sqlcomm.Parameters["@ExtraGr"].Value = sExtraGr.ToString();

            Sqlcomm.Parameters.Add("@Misc1", SqlDbType.Float);
            Sqlcomm.Parameters["@Misc1"].Value = sMisc1.ToString();

            Sqlcomm.Parameters.Add("@Misc2", SqlDbType.Float);
            Sqlcomm.Parameters["@Misc2"].Value = sMisc2.ToString();

            Sqlcomm.Parameters.Add("@OtherPrice", SqlDbType.Float);
            Sqlcomm.Parameters["@OtherPrice"].Value = sOtherPrice.ToString();

            sqlCon.Open();
            myReturn = Sqlcomm.ExecuteNonQuery();
            sqlCon.Close();
        }
        catch (Exception ex)
        {
            //log.Add2ExLog(ex.Message + " " + ex.Source + " " + ex.InnerException);
        }

        sqlCon.Close();
        return myReturn;

    }

    [ScriptMethod(UseHttpGet = true)]
    [WebMethod]
    public string CallAnotherWebSevice(string CompanyAlias, string UserName, string Password, string DeviceId, string DeviceType
        , string AppVersion, string EnvType, string DeviceName, string OsVersion)
    {
        string sReturned = "";

        string URI = "http://manage.mylimobiz.com/WebServices/DA/account/authenticate/";
        string sParameters = "CompanyAlias=" + CompanyAlias;
        sParameters = sParameters + "&" + "UserName=" + UserName;
        sParameters = sParameters + "&" + "Password=" + Password;
        sParameters = sParameters + "&" + "DeviceId=" + DeviceId;
        sParameters = sParameters + "&" + "DeviceType=" + DeviceType;
        sParameters = sParameters + "&" + "AppVersion=" + AppVersion;
        sParameters = sParameters + "&" + "EnvType=" + EnvType;
        sParameters = sParameters + "&" + "DeviceName=" + DeviceName;
        sParameters = sParameters + "&" + "OsVersion=" + OsVersion;

        sReturned = POST(URI, sParameters);
        /*
        using (WebClient wcCall = new WebClient())
        {
            wcCall.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            string HtmlResult = wcCall.UploadString(URI, sParameters);
        }*/

        return sReturned;

    }

    private string POST(string url, string data)
    {
        HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
        req.Method = "POST";
        req.Headers.Add(HttpRequestHeader.AcceptLanguage, "de-DE,de;q=0.8,en-US;q=0.7,en;q=0.3");

        req.Timeout = req.ReadWriteTimeout = 15000;

        ASCIIEncoding encoding = new ASCIIEncoding();
        byte[] DataBytes = encoding.GetBytes(data);
        req.ContentLength = DataBytes.Length;
        Stream stream = req.GetRequestStream();
        stream.Write(DataBytes, 0, DataBytes.Length);
        stream.Close();

        return req.GetResponse().ToString();
    }

    public void SendCommandToPhone(String sCommand, string DeviceID)
    {

        // your RegistrationID paste here which is received from GCM server.                                                               
        string regId = DeviceID;
        // applicationID means google Api key                                                                                                     
        string applicationID = "AIzaSyAG_Si9g_C4YabcccDKK6N139Bi0nOASHw"; //which is allowed  for any app
        // SENDER_ID is nothing but your ProjectID (from API Console- google code)//                                          
        string SENDER_ID = "My Project ID";
        string value = sCommand; //message text box                                                                               
        WebRequest tRequest;
        tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
        tRequest.Method = "post";
        tRequest.ContentType = " application/x-www-form-urlencoded;charset=UTF-8";

        tRequest.Headers.Add("Authorization", "key=" + applicationID);
        //tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

        //Data post to server                                                                                                                                         
        string postData =
       "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message="
              + value + "&data.time=" + System.DateTime.Now.ToString() + "&registration_id=" +
                 regId + "";
        Console.WriteLine(postData);
        Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
        tRequest.ContentLength = byteArray.Length;
        Stream dataStream = tRequest.GetRequestStream();
        dataStream.Write(byteArray, 0, byteArray.Length);
        dataStream.Close();
        WebResponse tResponse = tRequest.GetResponse();
        dataStream = tResponse.GetResponseStream();

        if (tResponse.Equals(HttpStatusCode.Unauthorized) ||
                tResponse.Equals(HttpStatusCode.Forbidden))
        {
            string text = "Unauthorized - need new token";
        }
        else if (tResponse.Equals(HttpStatusCode.OK) == false)
        {
            string text = "Response from web service isn't OK";
        }
        StreamReader tReader = new StreamReader(dataStream);

        String sResponseFromServer = tReader.ReadToEnd();   //Get response from GCM server.



        tReader.Close();

        dataStream.Close();
        tResponse.Close();

        /*
        WebRequest tRequest;
        tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
        tRequest.Method = "post";
        tRequest.ContentType = "application/x-www-form-urlencoded";
        tRequest.Headers.Add(string.Format("key={0}", "AIzaSyAG_Si9g_C4YabcccDKK6N139Bi0nOASHw"));
 
        String collaspeKey = Guid.NewGuid().ToString("n");
        //String postData=string.Format("registration_id={0}&data.payload={1}&collapse_key={2}", DeviceID, "Pickup Message", collaspeKey);
        String postData = string.Format("registration_id={0}&data.payload={1}&collapse_key={2}", DeviceID, sCommand, collaspeKey);


        while (postData.IndexOf(" ") != -1) {
            postData = postData.Replace(" ","%20");
        }

        Byte[] byteArray = Encoding.UTF8.GetBytes(postData);
        tRequest.ContentLength = byteArray.Length;
 
        Stream dataStream = tRequest.GetRequestStream();
        dataStream.Write(byteArray, 0, byteArray.Length);
        dataStream.Close();
 
        WebResponse tResponse = tRequest.GetResponse();
 
        dataStream = tResponse.GetResponseStream();
 
        StreamReader tReader = new StreamReader(dataStream);
 
        String sResponseFromServer = tReader.ReadToEnd();
 
        tReader.Close();
        dataStream.Close();
        tResponse.Close();
         */
    }





}
