using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Web.Script.Services;
using Newtonsoft.Json;
using System.IO;
using System.Collections;
using eplus.data;

/// <summary>
/// Summary description for jsonStaff
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class jsonStaff : System.Web.Services.WebService
{

    public jsonStaff()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public string Login(string UserID, string Password, string VerificationCode, string MacID)
    {
        SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        using (SqlCommand cmd = new SqlCommand("LS_check_login_staff_mob_MQ", c))
        {
            string DeviceID = "", BrandName = "", ModelName = "", AndroidVersion = "", Version = "";
            if (MacID.Contains(','))
            {
                string[] Mac = MacID.Split(',');
                DeviceID = Mac[0];
                BrandName = Mac[1];
                ModelName = Mac[2];
                AndroidVersion = Mac[3];
                Version = Mac[4];
            }
            else
                DeviceID = MacID;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@uid", UserID);
            cmd.Parameters.AddWithValue("@Pwd", Password);
            cmd.Parameters.AddWithValue("@VerificationCode", VerificationCode);
            cmd.Parameters.AddWithValue("@MacID", DeviceID);
            cmd.Parameters.AddWithValue("@BrandName", BrandName);
            cmd.Parameters.AddWithValue("@ModelName", ModelName);
            cmd.Parameters.AddWithValue("@AndroidVersion", AndroidVersion);
            cmd.Parameters.AddWithValue("@Version", Version);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                string BasehttpPath = "";
                //if (dt.Rows[0]["Message"].ToString() == "Success")
                //{
                //    string strstudentdocpath = ConfigurationManager.AppSettings["EmployeeDocs"].ToString() + "\\" + dt.Rows[0]["UserID"].ToString() + "\\" + dt.Rows[0]["EmpPhoto"].ToString();
                //    if (File.Exists(strstudentdocpath))
                //    {
                //        BasehttpPath = dt.Rows[0]["UserID"].ToString() + "\\" + dt.Rows[0]["EmpPhoto"].ToString();
                //    }
                //    else
                //    {
                        
                //        BasehttpPath = "";
                        
                //    }
                //}
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        if (col.ToString() == "EmpPhoto")
                            row.Add(col.ColumnName, BasehttpPath.Replace(" ", "%20"));
                        else
                            row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }

                return serializer.Serialize(rows);

            }
            else
            {
                return "NO DATA";
            }
        }
    }

    [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public string EmpLoginVerfication(string UserID, string Password, string MacID)
    {
        EmailAndSmsAlerts alerts = new EmailAndSmsAlerts();
        SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        using (SqlCommand cmd = new SqlCommand("LS_check_loginverification_staff_mob_MQ", c))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@uid", UserID);
            cmd.Parameters.AddWithValue("@Pwd", Password);
            cmd.Parameters.AddWithValue("@MacID", MacID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                string mobileno = dt.Rows[0]["MobileNumber"].ToString();
  
                int RandomNumber = GenerateRandomNo();
                if (dt.Rows[0]["Message"].ToString() == "Success")
                {
                    if (CheckRandomNumber(UserID, RandomNumber) == 0)
                    {
                        using (SqlCommand cmdSave = new SqlCommand("LS_SaveEmployeeMobileValidation", c))
                        {
                            cmdSave.CommandType = CommandType.StoredProcedure;
                            cmdSave.Parameters.AddWithValue("@LoginName", UserID);
                            cmdSave.Parameters.AddWithValue("@VerificationCode ", RandomNumber);
                            SqlDataAdapter daSave = new SqlDataAdapter(cmdSave);
                            DataTable dtSave = new DataTable();
                            daSave.Fill(dtSave);
                            if (dtSave.Rows[0]["Save"].ToString() == "1")
                            {
                                DataTable dtsettings = new DataTable();
                                dtsettings = alerts.GetSMSSettings();
                                int smsbalance = 0;
                                if (dtsettings.Rows.Count > 0)
                                {
                                    string smsbal = getSMSBalance(dtsettings.Rows[0]["WorkingKey"].ToString());
                                    if (!string.IsNullOrEmpty(smsbal))
                                        smsbalance = Convert.ToInt32(smsbal);
                                }
                                if (smsbalance == 0)
                                    dtsettings = alerts.GetSMSSettings();
                                DataTable dtMobile = new DataTable();
                                dtMobile.Columns.Add("mobileNo");
                                dtMobile.Rows.Clear();
                                dtMobile.Rows.Add(mobileno);
                                string msg = "Alert Code For Mobile Site Login " + RandomNumber.ToString();
                                if (mobileno.Length >= 10)
                                {
                                    //alerts.SaveAlertDetails("SMS", "individual", "Employee", UserID, "", mobileno, "","", "", "", "Mobile Verification", msg, "");
                                    alerts.SendSMS(dtMobile, dtsettings, msg);
                                }
                            }
                        }
                    }
                }
                string JSONString = string.Empty;
                JSONString = JsonConvert.SerializeObject(dt);
                return JSONString;
            }
            else
            {
                return "NO DATA";
            }
        }
    }

    [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public string Authentication(string UserID, string Password, string MacID)
    {
        SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        using (SqlCommand cmd = new SqlCommand("LS_check_Authentication_staff_mob_MQ", c))
        {
            string DeviceID = "", BrandName = "", ModelName = "", AndroidVersion = "", Version = "";
            if (MacID.Contains(','))
            {
                string[] Mac = MacID.Split(',');
                DeviceID = Mac[0];
                BrandName = Mac[1];
                ModelName = Mac[2];
                AndroidVersion = Mac[3];
                Version = Mac[4];
            }
            else
                DeviceID = MacID;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@uid", UserID);
            cmd.Parameters.AddWithValue("@Pwd", Password);
            cmd.Parameters.AddWithValue("@MacID", DeviceID);
            cmd.Parameters.AddWithValue("@BrandName", BrandName);
            cmd.Parameters.AddWithValue("@ModelName", ModelName);
            cmd.Parameters.AddWithValue("@AndroidVersion", AndroidVersion);
            cmd.Parameters.AddWithValue("@Version", Version);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                string BasehttpPath = "";
                if (dt.Rows[0]["Message"].ToString() == "Success")
                {
                    //string strstudentdocpath = ConfigurationManager.AppSettings["StaffDocuments"].ToString() + "\\" + dt.Rows[0]["UserID"].ToString() + "\\" + dt.Rows[0]["EmpPhoto"].ToString();
                    //if (File.Exists(strstudentdocpath))
                    //{
                    //    BasehttpPath = dt.Rows[0]["UserID"].ToString() + "\\" + dt.Rows[0]["EmpPhoto"].ToString();
                    //}
                    //else
                    //{
                    //    strstudentdocpath = ConfigurationManager.AppSettings["StaffDocuments"].ToString() + "\\" + dt.Rows[0]["LoginName"].ToString() + "\\" + dt.Rows[0]["EmpPhoto"].ToString();

                    //    if (File.Exists(strstudentdocpath))
                    //    {
                    //        BasehttpPath = dt.Rows[0]["LoginName"].ToString() + "\\" + dt.Rows[0]["EmpPhoto"].ToString();
                    //    }
                    //    else
                    //    {
                    //        BasehttpPath = "";
                    //    }
                    //}
                }
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        if (col.ToString() == "EmpPhoto")
                            row.Add(col.ColumnName, BasehttpPath.Replace(" ", "%20"));
                        else
                            row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }

                return serializer.Serialize(rows);

            }
            else
            {
                return "NO DATA";
            }
        }
    }

    [WebMethod]
    public string New_Password(string EmployeeID, string LoginID, string NewPassword)
    {
        SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        using (SqlCommand cmd = new SqlCommand("LS_SavePassword_MQ", c))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", EmployeeID);
            cmd.Parameters.AddWithValue("@LoginID", LoginID);
            cmd.Parameters.AddWithValue("@NewPassword", NewPassword);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }

                return serializer.Serialize(rows);
            }
            else
            {
                return "NO DATA";
            }
        }
    }

    [WebMethod]
    public string MobileUserPages(string UserID)
    {
        SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        using (SqlCommand cmd = new SqlCommand("LS_MobileUserPages_MQ", c))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", UserID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }

                return serializer.Serialize(rows);
            }
            else
            {
                return "NO DATA";
            }
        }
    }

    [WebMethod]
    public string GetEnquiryList(string EmployeeID)
    {
        SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        using (SqlCommand cmd = new SqlCommand("LS_GetEnquiryIDByUserID_MQ", c))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }

                return serializer.Serialize(rows);
            }
            else
            {
                return "NO DATA";
            }
        }
    }

    [WebMethod]
    public string SaveNewEnquiry(string CustEnqNo)
    {
        SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        using (SqlCommand cmd = new SqlCommand("LS_SaveCustomerEnquiryDetails", c))
        {
            cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@@EnquiryID", 0);
        //    cmd.Parameters.AddWithValue("@@CustomerEnquiryNumber", CustEnqNo);
        //    cmd.Parameters.AddWithValue("@@LseNumber", LSENumber);
        //    cmd.Parameters.AddWithValue("@@ProspectID", ProspectID);
        //    cmd.Parameters.AddWithValue("@@EnquiryTypeID", EmployeeID);
        //    cmd.Parameters.AddWithValue("@@ContactPerson", EmployeeID);
        //    cmd.Parameters.AddWithValue("@@ContactNumber", EmployeeID);
        //    cmd.Parameters.AddWithValue("@@Email", EmployeeID);
        //    cmd.Parameters.AddWithValue("@@ProjectDescription", EmployeeID);
        //    cmd.Parameters.AddWithValue("@@EMDAmount", EmployeeID);
        //    cmd.Parameters.AddWithValue("@@ReceivedDate", EmployeeID);
        //    cmd.Parameters.AddWithValue("@@ClosingDate", EmployeeID);
        //    cmd.Parameters.AddWithValue("@@CommercialOffer", EmployeeID);
        //    cmd.Parameters.AddWithValue("@@EnquiryLocation", EmployeeID);
        //    cmd.Parameters.AddWithValue("@@Budgetary", EmployeeID);
        //    cmd.Parameters.AddWithValue("@@UserID", EmployeeID);
        //    cmd.Parameters.AddWithValue("@@AttachmentName", EmployeeID);
        //    cmd.Parameters.AddWithValue("@@AttachementTypeName", EmployeeID);
        //    cmd.Parameters.AddWithValue("@@AttachementDescription", EmployeeID);
        //    cmd.Parameters.AddWithValue("@@Source", EmployeeID);
        //    cmd.Parameters.AddWithValue("@@SalesResource", EmployeeID);
        //    cmd.Parameters.AddWithValue("@@@RfpGroupID", EmployeeID);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }

                return serializer.Serialize(rows);
            }
            else
            {
                return "NO DATA";
            }
        }
    }

    [WebMethod]
    public string GetCommunicationList(string EmployeeID, string EnquiryID)
    {
        SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        using (SqlCommand cmd = new SqlCommand("LS_GetCommunicationList_MQ", c))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
            cmd.Parameters.AddWithValue("@EnquiryID", EnquiryID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
                Dictionary<string, object> row;
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, dr[col]);
                    }
                    rows.Add(row);
                }

                return serializer.Serialize(rows);
            }
            else
            {
                return "NO DATA";
            }
        }
    }

    [WebMethod]
    public string SendEnquiryMail(string EmployeeID, string Type, string SenderName, string ReceiverID, string ReceiverName, string Email, string EnquiryID, string Subject, string Message, string Attachment, string MacID)
    {
        int check = 0;
        Log.Message("test1");
        string sourcePath = ConfigurationManager.AppSettings["FTPPath"].ToString() + Attachment, descPath = "";
        DataTable dtSettings = new DataTable();
        EmailAndSmsAlerts alerts = new EmailAndSmsAlerts();
        dtSettings = alerts.GetEmailSettings();
        if (Type == "Internal")
        {
            //Email = "sowjanya@innovasphere.in";
            Log.Message("test2");
            DataTable dt = new DataTable();
            dt.Columns.Add("Mail");
            DataRow row = dt.NewRow();
            row[0] = Email;
            dt.Rows.Add(row);
            //if (!string.IsNullOrEmpty(EmployeeID) && !string.IsNullOrEmpty(SenderName) && !string.IsNullOrEmpty(ReceiverName) && !string.IsNullOrEmpty(Subject) && !string.IsNullOrEmpty(Message) && !string.IsNullOrEmpty(Email))
            if (!string.IsNullOrEmpty(Attachment))
            {
                if (!string.IsNullOrEmpty(Attachment))
                {
                    string extension = Path.GetExtension(Attachment);
                    descPath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString() + "\\";
                    if (!(Directory.Exists(descPath)))
                        Directory.CreateDirectory(descPath);
                    if (File.Exists(descPath + "\\" + Attachment))
                        Attachment = GetRandomNumber(extension);
                    File.Copy(sourcePath, descPath + "\\" + Attachment);

                    if (File.Exists(descPath + "\\" + Attachment) && File.Exists(sourcePath))
                    {
                        check = 1;
                        File.Delete(sourcePath);
                    }
                    else
                        check = 0;
                }
                else
                    check = 1;
                if (check == 1)
                {
                    //alerts.SaveAlertDetails("Mail", SchoolMasterID, EmployeeID, SenderName, "Individual", "Student", ReceiverID, ReceiverName, "", Email, "", "0", "", "0", "", "0", Subject, Message, Attachment);
                    //if (!string.IsNullOrEmpty(Attachment))
                    //    SendMail(dt, dtSettings, Subject, Message, descPath + "\\" + Attachment);
                    //else
                    //    SendMail(dt, dtSettings, Subject, Message, "");
                    return "Success";
                }
                else
                    return "FileNotFound";
            }
            else
                return "Empty";
        }
     
        else
            return "Empty";

    }

    private string GetRandomNumber(string Extension)
    {
        string str = Convert.ToString(new Random().Next(1, 9999999));

        if (File.Exists(ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString() + "\\Attachment\\" + str + Extension))
                GetRandomNumber(Extension);
        
        return str + Extension;
    }
    public int GenerateRandomNo()
    {
        int _min = 1000;
        int _max = 9999;
        Random _rdm = new Random();
        return _rdm.Next(_min, _max);
    }
    private string getSMSBalance(string WorkingKey)
    {
        string str = "http://hpsms.dial4sms.com/api/credits.php?workingkey=" + WorkingKey;
        HttpWebResponse httpWebResponse = (HttpWebResponse)WebRequest.Create(str).GetResponse();
        StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
        str = streamReader.ReadToEnd();
        streamReader.Close();
        httpWebResponse.Close();
        str = str.Replace("Your available credits is", "");
        str = str.Split('.')[0].ToString();
        return str;
    }
    public int CheckRandomNumber(string UserID, int VerficationCode)
    {
        int Exists = -1;
        SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        using (SqlCommand cmd = new SqlCommand("LS_CheckRandomNumberByLoginName", c))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@LoginName", UserID);
            cmd.Parameters.AddWithValue("@VerficationCode", VerficationCode);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Exists = Convert.ToInt32(dt.Rows[0]["Exists"]);
                if (Exists == 1)
                {
                    CheckRandomNumber(UserID, GenerateRandomNo());
                }
            }
        }
        return Exists;
    }
}
