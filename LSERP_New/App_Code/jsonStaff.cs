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
using System.Globalization;

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
    public string Alerts(string EmployeeID)
    {
        SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        using (SqlCommand cmd = new SqlCommand("LS_GetEmpAlertDetailsforMobile_MQ", c))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            string BasehttpPath = "";
            int i = 0;
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
        string sourcePath = ConfigurationManager.AppSettings["FTPPath"].ToString() + "\\" + Attachment;
        string descPath = "";
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
            if (!string.IsNullOrEmpty(EmployeeID) && !string.IsNullOrEmpty(Subject) && !string.IsNullOrEmpty(Message) && !string.IsNullOrEmpty(Email))
            //if (!string.IsNullOrEmpty(Attachment))
            {
                if (!string.IsNullOrEmpty(Attachment))
                {
                    string extension = Path.GetExtension(Attachment);
                    descPath = ConfigurationManager.AppSettings["CommunicationSavePath"].ToString() + "\\";
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
                    try
                    {
                        alerts.Status = 1;
                        if (!string.IsNullOrEmpty(Attachment))
                            SendMail(dt, dtSettings, Subject, Message, descPath + "\\" + Attachment);
                        else
                            SendMail(dt, dtSettings, Subject, Message, "");
                    }
                    catch (Exception ec)
                    {
                        alerts.Status = 0;
                    }
                    alerts.AlertType = "Mail";
                    alerts.EntryMode = "Individual";
                    alerts.file = Attachment;
                    alerts.reciverID = ReceiverID;
                    alerts.reciverType = "Staff";
                    alerts.EmailID = Email;
                    alerts.Subject = Subject;
                    alerts.Message = Message;
                    alerts.userID = EmployeeID;
                    alerts.GroupID = 0;
                    alerts.ReceiverGroup = "";
                    alerts.macid = MacID;
                    alerts.SaveCommunicationEmailAlertDetails();
                    //alerts.SaveAlertDetails("Mail", SchoolMasterID, EmployeeID, SenderName, "Individual", "Student", ReceiverID, ReceiverName, "", Email, "", "0", "", "0", "", "0", Subject, Message, Attachment);
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


    public void SendMail(DataTable dt, DataTable dtSettings, string Subject, string body, string attachment)
    {
        try
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = dtSettings.Rows[0]["smtpAddress"].ToString().Trim();
            //smtpClient.Credentials = (ICredentialsByHost)new NetworkCredential(dtSettings.Rows[0]["senderMailId"].ToString().Trim(), dtSettings.Rows[0]["senderPwd"].ToString().Trim());
            smtpClient.Credentials = new System.Net.NetworkCredential(dtSettings.Rows[0]["senderMailId"].ToString().Trim(), dtSettings.Rows[0]["senderPwd"].ToString().Trim());
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    MailMessage message = new MailMessage();
                    message.From = new MailAddress(dtSettings.Rows[0]["senderMailId"].ToString().Trim());
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(dataRow[0])))
                        {
                            message.To.Clear();
                            if (!string.IsNullOrEmpty(attachment))
                                message.Attachments.Add(new Attachment(attachment));
                            //if (attachmentFilename != null)
                            //{
                            //    Attachment attachment = new Attachment(attachmentFilename, MediaTypeNames.Application.Octet);
                            //    ContentDisposition disposition = attachment.ContentDisposition;
                            //    disposition.CreationDate = File.GetCreationTime(attachmentFilename);
                            //    disposition.ModificationDate = File.GetLastWriteTime(attachmentFilename);
                            //    disposition.ReadDate = File.GetLastAccessTime(attachmentFilename);
                            //    disposition.FileName = Path.GetFileName(attachmentFilename);
                            //    disposition.Size = new FileInfo(attachmentFilename).Length;
                            //    disposition.DispositionType = DispositionTypeNames.Attachment;
                            //    message.Attachments.Add(attachment);
                            //}
                            message.To.Add(new MailAddress(dataRow[0].ToString()));
                            message.Subject = Subject;
                            message.Body = body;
                            message.IsBodyHtml = true;
                            smtpClient.Port = Convert.ToInt32(dtSettings.Rows[0]["smtpPort"].ToString());
                            if (dtSettings.Rows[0]["EnableSSI"].ToString() == "1")
                                smtpClient.EnableSsl = true;
                            else
                                smtpClient.EnableSsl = false;
                            smtpClient.Send(message);

                        }
                    }
                    message.Dispose();
                }
            }

            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
        }
        catch (Exception ex)
        {

        }
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

    [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public string Getmasterdetails()
    {
        SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        DataSet ds = new DataSet();
        string[] spnames = { "LS_GetProspectDetailsForEnquiryProcess", "LS_GetEnquiryTypeName", "LS_GetEnquiryLocation", "LS_GetHowSourcedEnquiry", "LS_GetSalesAndMarketingDepartmentEmployee", "LS_GetRFPGroupName" };
        string[] tableName = { "Prospect", "EnquiryType", "EnquiryLocation", "SourcedEnquiry", "MarketingEmployee", "RFPGroup" };
        for (int s = 0; s < spnames.Length; s++)
        {
            using (SqlCommand cmd = new SqlCommand(spnames[s], c))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                ds.Tables.Add(dt);
            }
        }
        DataTable dt1 = new DataTable();
        DataTable dt2 = new DataTable();
        DataTable dt3 = new DataTable();
        DataTable dt4 = new DataTable();
        DataTable dt5 = new DataTable();
        DataTable dt6 = new DataTable();
        dt1 = ds.Tables[0];
        dt2 = ds.Tables[1];
        dt3 = ds.Tables[2];
        dt4 = ds.Tables[3];
        dt5 = ds.Tables[4];
        dt6 = ds.Tables[5];

        string JSONString = string.Empty;
        if (dt1.Rows.Count > 0)
        {
            {
                JSONString = "'Prospect' :" + JsonConvert.SerializeObject(dt1);
            }
            if (dt2.Rows.Count > 0)
            {
                JSONString = JSONString + ",'EnquiryType' : " + JsonConvert.SerializeObject(dt2);

            }
            if (dt3.Rows.Count > 0)
            {
                JSONString = JSONString + ",'EnquiryLocation' : " + JsonConvert.SerializeObject(dt3);

            }
            if (dt4.Rows.Count > 0)
            {
                JSONString = JSONString + ",'SourcedEnquiry' : " + JsonConvert.SerializeObject(dt4);

            }
            if (dt5.Rows.Count > 0)
            {
                JSONString = JSONString + ",'MarketingEmployee' : " + JsonConvert.SerializeObject(dt5);

            }
            if (dt6.Rows.Count > 0)
            {
                JSONString = JSONString + ",'RFPGroup' : " + JsonConvert.SerializeObject(dt6);

            }
            JSONString = JSONString.Replace("]},{[", "] , [");


            return JSONString;
        }
        //System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        //List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        //List<Dictionary<string, object>> rows1 = new List<Dictionary<string, object>>();
        //Dictionary<string, object> dtrows1 = new Dictionary<string, object>();
        //for (int i = 0; i < ds.Tables.Count; i++)
        //{
        //    Dictionary<string, object> row;

        //    rows = new List<Dictionary<string, object>>();

        //    foreach (DataRow dr in ds.Tables[i].Rows)
        //    {
        //        row = new Dictionary<string, object>();
        //        foreach (DataColumn col in ds.Tables[i].Columns)
        //        {
        //            row.Add(col.ColumnName, dr[col]);
        //        }
        //        rows.Add(row);
        //    }

        //    dtrows1 = new Dictionary<string, object>();
        //    dtrows1.Add(tableName[i], rows);
        //    rows1.Add(dtrows1);
        //}
        //if (rows1.Count > 0)
        //    return serializer.Serialize(rows1);
        else
            return "NO DATA";
    }

    [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public string GetProspectDetailsBySearchKey(string ProspectName)
    {
        SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        using (SqlCommand cmd = new SqlCommand("LS_GetProspectNamebySearchKey", c))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProspectName", ProspectName);
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

    [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public string GetEmployeeCommunicationDetails()
    {
        SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        using (SqlCommand cmd = new SqlCommand("LS_GetEmployeeCommunicationDetails", c))
        {
            cmd.CommandType = CommandType.StoredProcedure;
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

    //[WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    //public string GetProspectName()
    //{
    //    return Getmasterdetails("LS_GetProspectDetailsForEnquiryProcess");
    //}

    //[WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    //public string GetEnquiryType()
    //{
    //    return Getmasterdetails("LS_GetEnquiryTypeName");
    //}
    //[WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    //public string GetEnquiryLocation()
    //{
    //    return Getmasterdetails("LS_GetEnquiryLocation");
    //}

    //[WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    //public string GetHowSourced()
    //{
    //    return Getmasterdetails("LS_GetHowSourcedEnquiry");
    //}
    //[WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    //public string GetSalesResource()
    //{
    //    return Getmasterdetails("LS_GetSalesAndMarketingDepartmentEmployee");
    //}
    //[WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    //public string GetRFPGroupName()
    //{
    //    return Getmasterdetails("LS_GetRFPGroupName");
    //}

    [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public string SaveEnquiryDetails(string CustomerEnquiryNumber, int ProspectID, string ProjectDescription, int CommercialOffer,
        int EnquiryTypeId, int EMDAmount, string ReceivedDate, string DeadLineDate, int LocationID, int BudgetaryEnquiry,
        int SourceID, int HowDidYouHearAboutUs, int RFPGroupID, string AttachmentName)
    {
        string msg = "";
        try
        {
            DataSet ds = new DataSet();

            cSales objSales = new cSales();
            // objSales.EnquiryID = EnquiryID;
            objSales.EnquiryTypeId = EnquiryTypeId;
            objSales.EnquiryNumber = CustomerEnquiryNumber;
            objSales.CommercialOffer = CommercialOffer;
            //  objSales.LseNumber = LseNumber;
            objSales.ProspectID = ProspectID;
            //objSales.ContactPerson = ContactPerson;
            //objSales.ContactNumber = ContactNumber;
            //objSales.EmailId = EmailId;
            objSales.ProjectDescription = ProjectDescription;
            objSales.EMDAmount = EMDAmount;
            objSales.ReceivedDate = DateTime.ParseExact(ReceivedDate.ToString().Replace('-', '/'), "yyyy/MM/dd", CultureInfo.InvariantCulture);
            objSales.ClosingDate = DateTime.ParseExact(DeadLineDate.ToString().Replace('-', '/'), "yyyy/MM/dd", CultureInfo.InvariantCulture);
            // objSales.AlternateContactNumber = AlternateContactNumber;
            //  objSales.Drawingoffer = DrawingOffer;
            objSales.budgetaryoffer = BudgetaryEnquiry;
            //  objSales.notinterested = notinterested;
            objSales.EnquiryLocation = LocationID;
            //  objSales.UserID = UserID;
            objSales.AttachementName = AttachmentName;
            //  objSales.AttachementTypeName = AttachementTypeName;
            //  objSales.AttachementDescription = AttachementDescription;
            objSales.Source = HowDidYouHearAboutUs;
            objSales.SalesResource = SourceID;
            objSales.RfpGroupID = RFPGroupID;
            //  objSales.ItemDescription = ItemDescription;
            ds = objSales.SaveCustomerEnquiryDetails();
            if (ds.Tables[0].Rows[0]["msg"].ToString() == "Added")
                msg = "Added";
            else
                msg = "updated";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return msg;
    }

    [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public string GetDashBoardDetails(string UserID)
    {
        string JSONString = string.Empty;
        try
        {
            DataSet ds = new DataSet();
            SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);

            using (SqlCommand cmd = new SqlCommand("LS_GetMobileAppDashBoardDetails", c))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@ProspectName", ProspectName);
                cmd.Parameters.AddWithValue("@Emplid", UserID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(ds);
            }
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            DataTable dt0 = new DataTable();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            DataTable dt3 = new DataTable();
            DataTable dt4 = new DataTable();
            DataTable dt5 = new DataTable();
            DataTable dt6 = new DataTable();
            DataTable dt7 = new DataTable();
            DataTable dt8 = new DataTable();
            DataTable dt9 = new DataTable();
            DataTable dt10 = new DataTable();

            dt0 = ds.Tables[0];
            dt1 = ds.Tables[1];
            dt2 = ds.Tables[2];
            dt3 = ds.Tables[3];
            dt4 = ds.Tables[4];
            dt5 = ds.Tables[5];
            dt6 = ds.Tables[6];
            dt7 = ds.Tables[7];
            dt8 = ds.Tables[8];

            if (dt0.Rows.Count > 0)
                JSONString = JsonConvert.ToString("ApprovalCount") + ":" + JsonConvert.SerializeObject(dt0);
            else
                JSONString = JsonConvert.ToString("ApprovalCount") + ":" + JsonConvert.Null;

            if (dt1.Rows.Count > 0)
                JSONString = JSONString + "," + JsonConvert.ToString("InBoxCount") + ":" + JsonConvert.SerializeObject(dt1);
            else
                JSONString = JSONString + "," + JsonConvert.ToString("InBoxCount") + ":" + JsonConvert.Null;

            if (dt2.Rows.Count > 0)
                JSONString = JSONString + "," + JsonConvert.ToString("TaskCount") + ":" + JsonConvert.SerializeObject(dt2);
            else
                JSONString = JSONString + "," + JsonConvert.ToString("TaskCount") + ":" + JsonConvert.Null;

            if (dt3.Rows.Count > 0)
                JSONString = JSONString + "," + JsonConvert.ToString("AlertCount") + ":" + JsonConvert.SerializeObject(dt3);
            else
                JSONString = JSONString + "," + JsonConvert.ToString("AlertCount") + ":" + JsonConvert.Null;

            if (dt4.Rows.Count > 0)
            {
                JSONString = JSONString + "," + JsonConvert.ToString("Sales") + ":" + "[{";

                JSONString = JSONString + JsonConvert.ToString("TotalEnquiry") + ":" + JsonConvert.SerializeObject(dt4);
                JSONString = JSONString + "," + JsonConvert.ToString("Source") + ":" + JsonConvert.SerializeObject(dt5);
                JSONString = JSONString + "," + JsonConvert.ToString("Order") + ":" + JsonConvert.SerializeObject(dt6);

                JSONString = JSONString + "}]";
            }
            else
                JSONString = JsonConvert.ToString("Sales") + ":" + JsonConvert.Null;

            if (dt7.Rows.Count > 0)
            {
                JSONString = JSONString + "," + JsonConvert.ToString("Design") + ":" + "[{";
                JSONString = JSONString + JsonConvert.ToString("Order") + ":" + JsonConvert.SerializeObject(dt7);
                JSONString = JSONString + "}]";
            }

            //JSONString = JSONString + "," + JsonConvert.ToString("Design") + ":" + JsonConvert.Null;
            JSONString = JSONString + "," + JsonConvert.ToString("Production") + ":" + JsonConvert.Null;
            JSONString = JSONString + "," + JsonConvert.ToString("Quality") + ":" + JsonConvert.Null;

            if (dt8.Rows.Count > 0)
                JSONString = JSONString + "," + JsonConvert.ToString("DisplayStatus") + ":" + JsonConvert.SerializeObject(dt8);
            else
                JSONString = JsonConvert.ToString("DisplayStatus") + ":" + JsonConvert.Null;

            JSONString = JSONString.Replace("]},{[", "] , [");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return JSONString;
    }

    [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public string SaveTaskDetails(string Taskname, string CompletetionDate, string UserID)
    {
        string msg = "";
        SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        using (SqlCommand cmd = new SqlCommand("LS_InsertTasks", c))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@strTask", Taskname);
            if (!string.IsNullOrEmpty(CompletetionDate))
                cmd.Parameters.AddWithValue("@CompletionDate", DateTime.ParseExact(CompletetionDate.ToString().Replace('-', '/'), "yyyy/MM/dd", CultureInfo.InvariantCulture));
            else
                cmd.Parameters.AddWithValue("@CompletionDate", DBNull.Value);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                msg = "Added";
            else
                msg = "ErrorOccured";
        }
        return msg;
    }

    [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public string GetTaskDetails(string UserID)
    {
        string msg = "";
        SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        using (SqlCommand cmd = new SqlCommand("LS_GetMyTasks", c))
        {

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@userid", UserID);
            cmd.Parameters.AddWithValue("@status", DBNull.Value);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dt.Columns.Remove("RowOrder");
            dt.Columns.Remove("EntryDate");
            dt.Columns.Remove("UserID");

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            if (dt.Rows.Count > 0)
            {
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

    [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public string UpdateTasks(string TaskID, string Status)
    {
        string msg = "";
        SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
        c.Open();

        string spname = "";

        if (Status == "Checked" || Status == "UnChecked")
        {
            spname = "LS_UpdateTasks";
            Status = Status == "Checked" ? "1" : "0";
        }
        else if (Status == "Delete")
            spname = "LS_DeleteTasks";

        SqlCommand cmd = new SqlCommand(spname, c);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@strTaskID", TaskID);
        if (Status != "Delete")
            cmd.Parameters.AddWithValue("@strStatus", Status);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        c.Close();

        if (dt.Rows[0]["Message"].ToString() == "Updated")
            return msg = "Updated";
        else if (dt.Rows[0]["Message"].ToString() == "Deleted")
            return msg = "Deleted";
        else
            return msg = "ErrorOccured";
    }

    string lblpassword = "";
    [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public string PasswordChange(string userid, string oldpwd, string newpwd, string confirmpwd)
    {
        try
        {
            PasswordDetails(userid);
            cCommonMaster objCom = new cCommonMaster();
            if (ValidationPassword(userid, oldpwd, newpwd, confirmpwd))
            {
                string oldpassword = oldpwd;
                objCom.Password = oldpassword;
                objCom.newpassword = newpwd;
                objCom.confirmpassword = confirmpwd;
                objCom.UserName = userid;
                DataSet ds = objCom.changepassword();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["output"].ToString() == "1")
                    {
                        return "Changed";
                    }
                    else
                    {
                        return "Failed";
                    }
                }
            }
            else
            {
                return "mandatory error";
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return "Failed";
    }

    public bool ValidationPassword(string userid, string oldpwd, string newpwd, string confirmpwd)
    {
        string oldpassword = oldpwd;
        bool valid = true;
        if (oldpwd == "")
        {
            valid = false;
        }
        else if (oldpassword != lblpassword)
        {
            valid = false;
        }
        if (newpwd == "")
        {
            valid = false;
        }
        if (confirmpwd == "")
        {
            valid = false;
        }
        else if (newpwd != confirmpwd)
        {
            valid = false;
        }
        return valid;
    }

    public void PasswordDetails(string userid)
    {
        cCommonMaster objCom = new cCommonMaster();
        objCom.UserID = userid;
        DataSet ds = objCom.getuserdetails();
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblpassword = ds.Tables[0].Rows[0]["Password"].ToString();
        }
    }

    [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public string ViewEnquiryDetails(string EnquiryID)
    {
        string JSONString = string.Empty;
        try
        {
            SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            c.Open();
            SqlCommand cmd = new SqlCommand("LS_GetEnquiryDetailsByEnquiryID", c);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EnquiryID", EnquiryID);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            c.Close();
            if (dt.Rows.Count > 0)
                JSONString = JSONString + ",'EnquiryDetails':" + JsonConvert.SerializeObject(dt);
            JSONString = JSONString.Replace("]},{[", "] , [");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return JSONString;
    }

    [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public string GetEmployeeProfileInFo(string UserID)
    {
        string JSONString = string.Empty;
        try
        {
            SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            c.Open();
            SqlCommand cmd = new SqlCommand("LS_GetEmployeeProfileDataByUserID", c);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", UserID);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            c.Close();
            if (dt.Rows.Count > 0)
                JSONString = JSONString + ",'EmployeeProfile':" + JsonConvert.SerializeObject(dt);
            JSONString = JSONString.Replace("]},{[", "] , [");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return JSONString;
    }

    [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public string GetCustomerDetails()
    {
        string JSONString = string.Empty;
        try
        {
            SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            c.Open();
            SqlCommand cmd = new SqlCommand("LS_GetProsPectDetailsForMobileApp", c);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            c.Close();
            if (dt.Rows.Count > 0)
                JSONString = JSONString + JsonConvert.SerializeObject(dt);
            JSONString = JSONString.Replace("]},{[", "] , [");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return JSONString;
    }

    public string GetEnquiryStatusReportDetails(string UserID)
    {
        string JSONString = string.Empty;
        try
        {
            SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            c.Open();
            SqlCommand cmd = new SqlCommand("LS_GetEnquiryStatusReportForMobileApp", c);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", UserID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            c.Close();
            if (dt.Rows.Count > 0)
                JSONString = JSONString + JsonConvert.SerializeObject(dt);
            JSONString = JSONString.Replace("]},{[", "] , [");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return JSONString;
    }

    [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public string GetApprovalSumaryDetailsByTypeAndName(string Type, string Name)
    {
        string JSONString = string.Empty;
        try
        {
            SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            c.Open();
            SqlCommand cmd = new SqlCommand("LS_GetApprovalSummaryDetailsByTypeAndName", c);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Type", Type);
            cmd.Parameters.AddWithValue("@Name", Name);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            c.Close();
            if (dt.Rows.Count > 0)
                JSONString = JsonConvert.SerializeObject(dt);
            JSONString = JSONString.Replace("]},{[", "] , [");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return JSONString;
    }

    [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public string GetCompanyDetailsReport()
    {
        string JSONString = string.Empty;
        try
        {
            SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            c.Open();
            SqlCommand cmd = new SqlCommand("LS_GetCompanyDetailsReport", c);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            c.Close();
            if (dt.Rows.Count > 0)
                JSONString = JsonConvert.SerializeObject(dt);
            JSONString = JSONString.Replace("]},{[", "] , [");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return JSONString;
    }

    [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public string GetSalesSummaryReportDetails()
    {
        string JSONString = string.Empty;
        try
        {
            SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            c.Open();
            SqlCommand cmd = new SqlCommand("LS_GetSalesSummaryStatusReport", c);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            c.Close();
            if (dt.Rows.Count > 0)
                JSONString = JSONString + JsonConvert.SerializeObject(dt);
            JSONString = JSONString.Replace("]},{[", "] , [");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return JSONString;
    }

    [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public string GetDesignSummaryReportDetails()
    {
        string JSONString = string.Empty;
        try
        {
            SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            c.Open();
            SqlCommand cmd = new SqlCommand("LS_DesignSummaryReportDetails", c);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            c.Close();
            if (dt.Rows.Count > 0)
                JSONString = JSONString + JsonConvert.SerializeObject(dt);
            JSONString = JSONString.Replace("]},{[", "] , [");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return JSONString;
    }

    [WebMethod, ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public string GetRFPSummarySheetDetails()
    {
        string JSONString = string.Empty;
        try
        {
            SqlConnection c = new SqlConnection(ConfigurationManager.ConnectionStrings["con"].ConnectionString);
            c.Open();
            SqlCommand cmd = new SqlCommand("LS_GetRFPSummarySheetDetails", c);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            c.Close();
            if (dt.Rows.Count > 0)
                JSONString = JSONString + JsonConvert.SerializeObject(dt);

            JSONString = JSONString.Replace("]},{[", "] , [");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return JSONString;
    }
}