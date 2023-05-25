using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Mail;
using eplus.data;
using System.Web.UI.WebControls;

public class EmailAndSmsAlerts
{
    #region Declarations

    DataSet ds = new DataSet();
    public SqlDataReader dr;
    cDataAccess DAL = new cDataAccess();
    SqlCommand c = new SqlCommand();
    private SqlCommand cmd;
    SqlDataAdapter da = new SqlDataAdapter();
    #endregion

    #region Properties

    public string Title { get; set; }
    public DateTime date { get; set; }
    public Int16 Status { get; set; }
    public Int16 DisplayWeb { get; set; }
    public Int64 NotificationID { get; set; }
    public Int16 DisplayTo { get; set; }
    public string file { get; set; }
    public string UserTypeID { get; set; }
    public string DepartmentIDs { get; set; }
    public string BatchIDs { get; set; }
    public string CreatedBy { get; set; }
    public string returnvalue { get; set; }
    public string AlertType { get; set; }
    public string EntryMode { get; set; }
    public string reciverID { get; set; }
    public string reciverType { get; set; }
    public string ReciverName { get; set; }
    public string EmailID { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
    public string strAttachment { get; set; }
    public string userID { get; set; }
    public string ReceiverGroup { get; set; }
    public DataTable dtSettings { get; set; }
    public DataTable dtGroupEmailID { get; set; }
    public HttpPostedFile httpPostedFile { get; set; }
    public Int32 GroupID { get; set; }
    public string MobileNo { get; set; }
    public int MessageID { get; set; }
    public int EnquiryNumber { get; set; }
    public int AttachementID { get; set; }

    public string Header { get; set; }
    public string type { get; set; }

    public DataTable dt { get; set; }
    public string GroupName { get; set; }
    public string GroupType { get; set; }

    public int CGNID { get; set; }
    public string macid { get; set; }
    public string DailyStatus { get; set; }

    #endregion
    public DataTable GetSMSSettings()
    {
        DataSet ds = new DataSet();
        try
        {
            cDataAccess DAL = new cDataAccess();
            SqlCommand c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SMSSetting";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        DataTable dt = new DataTable();
        dt = ds.Tables[0];
        return dt;
    }
    public DataTable GetEmailSettings()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            SqlCommand c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetMailSetting";
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        DataTable dt = new DataTable();
        dt = ds.Tables[0];
        return dt;
    }

    public void SendIndividualMail()
    {
        try
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = dtSettings.Rows[0]["smtpAddress"].ToString().Trim();
            smtpClient.Credentials = (ICredentialsByHost)new NetworkCredential(dtSettings.Rows[0]["senderMailId"].ToString().Trim(), dtSettings.Rows[0]["senderPWD"].ToString().Trim());
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(dtSettings.Rows[0]["senderMailId"].ToString());

                message.To.Clear();
                message.To.Add(new MailAddress(EmailID));
                message.Subject = Subject;
                if (file != "")
                {
                    if (file.Split('|').Length > 1)
                    {
                        for (int i = 0; i < file.Split('|').Length; i++)
                        {
                            string filen = file.Split('|')[i].ToString();
                            if (File.Exists(filen)) message.Attachments.Add(new Attachment(filen));
                        }
                    }
                    else
                    {
                        //string File = Path.GetFileName(objAlert.file);
                        if (File.Exists(file)) message.Attachments.Add(new Attachment(file));
                    }
                }

                message.Body = Message;
                message.IsBodyHtml = true;
                if (dtSettings.Rows[0]["EnableSSI"].ToString() == "1")
                    smtpClient.EnableSsl = true;
                else
                    smtpClient.EnableSsl = false;
                smtpClient.Send(message);

                message.Dispose();
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public static bool ValidateEmail(string Email)
    {
        try
        {
            MailAddress m = new MailAddress(Email);

            return true;
        }
        catch (FormatException)
        {
            return false;
        }

    }
    public void SendMail(DataTable dt, DataTable dtSettings, string Subject, string body)
    {
        try
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = dtSettings.Rows[0]["smtpAdd"].ToString().Trim();
            smtpClient.Credentials = (ICredentialsByHost)new NetworkCredential(dtSettings.Rows[0]["senderMailId"].ToString().Trim(), dtSettings.Rows[0]["senderPwd"].ToString().Trim());
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
                            message.To.Add(new MailAddress(dataRow[0].ToString()));
                            message.Subject = Subject;
                            message.Body = body;
                            message.IsBodyHtml = true;
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
            Log.Message(ex.ToString());
        }
    }
    public void SendIndividualEmailwithAttachment()
    {
        try
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = dtSettings.Rows[0]["smtpAdd"].ToString().Trim();
            smtpClient.Credentials = (ICredentialsByHost)new NetworkCredential(dtSettings.Rows[0]["senderMailId"].ToString().Trim(), dtSettings.Rows[0]["senderPwd"].ToString().Trim());
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress(dtSettings.Rows[0]["senderMailId"].ToString().Trim());

                message.To.Clear();
                message.To.Add(new MailAddress(EmailID));
                message.Subject = Subject;
                message.Body = Message;
                message.IsBodyHtml = true;
                if (httpPostedFile.FileName != "" && httpPostedFile.FileName != null)
                {
                    string File = Path.GetFileName(httpPostedFile.FileName);
                    message.Attachments.Add(new Attachment(httpPostedFile.InputStream, File));
                }
                if (dtSettings.Rows[0]["EnableSSI"].ToString() == "1")
                    smtpClient.EnableSsl = true;
                else
                    smtpClient.EnableSsl = false;
                smtpClient.Send(message);
                message.Dispose();
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    public string SendSMS(DataTable dt, DataTable dtSettings, string Message)
    {
        try
        {
            // Log.Message("hi");
            //string str = "principal@sms12";
            if (dtSettings != null && dtSettings.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    try
                    {
                        string str = "";
                        str = "http://hpsms.dial4sms.com/api/web2sms.php?username=" + dtSettings.Rows[0]["userId"].ToString().Trim() + "&password=" + dtSettings.Rows[0]["password"].ToString().Trim() + " &to=" + dataRow[0].ToString().Trim() + "&sender=" + dtSettings.Rows[0]["senderId"].ToString().Trim() + "&message=" + Message;

                        //Log.Message(str);

                        HttpWebResponse httpWebResponse = (HttpWebResponse)WebRequest.Create(str).GetResponse();
                        StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
                        streamReader.ReadToEnd();
                        streamReader.Close();
                        httpWebResponse.Close();
                    }
                    catch (Exception ex)
                    {
                        Log.Message(ex.ToString());
                        return "error";
                    }
                }
                return "sent";
            }
            else
            {
                return "error";
            }

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
            return "error";
        }
        return "error";
    }

    public void SaveAlertDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_InsertEmailAlerts";
            // c.Parameters.AddWithValue("@EnquiryNumber", EnquiryNumber);
            c.Parameters.AddWithValue("@AlertType", AlertType);
            c.Parameters.AddWithValue("@ReceiverId", reciverID);
            c.Parameters.AddWithValue("@RecType", reciverType);

            if (reciverType == "E")
                c.Parameters.AddWithValue("@Email", EmailID);
            else
                c.Parameters.AddWithValue("@Email", null);

            c.Parameters.Add("@Header", Header);
            c.Parameters.Add("@Message", Message);
            c.Parameters.AddWithValue("@AttachementID", AttachementID);
            c.Parameters.AddWithValue("@userid", userID);
            c.Parameters.AddWithValue("@ReceiverGroupID", GroupID);

            if (MessageID != 0)
                c.Parameters.AddWithValue("@MessageID", MessageID);
            else
                c.Parameters.AddWithValue("@MessageID", DBNull.Value);
            returnvalue = DAL.GetScalar(c);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public string SaveCommunicationEmailAlertDetails()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveCommunicationEmailAlerts";
            c.Parameters.AddWithValue("@AlertType", AlertType);
            c.Parameters.AddWithValue("@EntryMode", EntryMode);
            c.Parameters.AddWithValue("@File", file);
            c.Parameters.AddWithValue("@ReceiverId", reciverID);
            c.Parameters.AddWithValue("@RecType", reciverType);

            c.Parameters.AddWithValue("@Email", EmailID);

            c.Parameters.Add("@Header", Subject);
            c.Parameters.Add("@Message", Message);
            c.Parameters.AddWithValue("@userid", userID);
            c.Parameters.AddWithValue("@ReceiverGroupID", GroupID);
            if (string.IsNullOrEmpty(ReceiverGroup))
                c.Parameters.AddWithValue("@ReceiverGroup", DBNull.Value);
            else
                c.Parameters.AddWithValue("@ReceiverGroup", ReceiverGroup);

            c.Parameters.AddWithValue("@Status", Status);
            c.Parameters.AddWithValue("@macid", DBNull.Value);

            returnvalue = DAL.GetScalar(c);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return returnvalue;
    }

    public string SaveSmsAlertDetails()
    {
        //string AlertType, string EntryMode, string ReceiverID, string ReceiverType, string MobileNo, 
        //string EmailID, string Subject, string Message, string File, string UserID, string ReceiverGroup, string GroupID, string Status
        string value = "";
        try
        {
            cDataAccess DAL = new cDataAccess();
            SqlCommand c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_InsertSMSAlerts";
            SqlParameter sqlParameter = new SqlParameter("@AlertID", SqlDbType.VarChar, 20);
            sqlParameter.Direction = ParameterDirection.Output;
            c.Parameters.Add(sqlParameter);
            c.Parameters.AddWithValue("@AlertType", "SMS");
            c.Parameters.AddWithValue("@EntryMode", EntryMode);
            c.Parameters.AddWithValue("@ReceiverId", reciverID);
            c.Parameters.AddWithValue("@RecType", reciverType);
            c.Parameters.AddWithValue("@Mobile", MobileNo);
            // c.Parameters.AddWithValue("@EmailID", EmailID);
            //   c.Parameters.AddWithValue("@Subject", Subject);
            c.Parameters.AddWithValue("@Message", Message);
            //  c.Parameters.AddWithValue("@CollegeMasterID", File);
            c.Parameters.AddWithValue("@userid", userID);
            c.Parameters.AddWithValue("@ReceiverGroup", ReceiverGroup);
            c.Parameters.AddWithValue("@GroupID", GroupID);
            value = DAL.GetScalar(c);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return value;
    }

    public DataSet GetAutomatedMessageMail()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetAutomatedMessageMail";
            c.Parameters.Add("@MessageID", MessageID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetAlertDetailsByUserID()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetAlertDetailsByReceiverID";
            c.Parameters.Add("@UserID", userID);
            c.Parameters.Add("@type", type);
            c.Parameters.Add("@DailyStatus", DailyStatus);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet SaveEmployeeEmailAndSMSGroup()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveEmailAndSMSGroup";
            c.Parameters.Add("@tblEmployeeID", dt);
            c.Parameters.Add("@GroupName", GroupName);
            c.Parameters.Add("@GroupType", GroupType);
            c.Parameters.Add("@CGNID", CGNID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    public DataSet GetSMSGroupAndEmailGroupDetailsByGroupType()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetEmailAndSMSGroupDetailsbyGroupType";
            c.Parameters.Add("@GroupType", GroupType);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet GetGroupMembersDetailsByGroupID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetGroupMembersDetailsByCGNID";
            c.Parameters.Add("@CGNID", CGNID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return ds;
    }

    public DataSet DeleteCommunicationGroupDetailsByCGNID()
    {
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "DeleteCommunicationGroupDetailsByCGNID";
            c.Parameters.Add("@CGNID", CGNID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }
    public DataSet GetEmployeeList()
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetEmployeeListByCommunicationGroupName";
            c.Parameters.Add("@CGNID", CGNID);
            DAL.GetDataset(c, ref ds);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    #region"DropDown List"

    public DataSet GetCommunicationGroupType(DropDownList ddlCommunicationGroupType)
    {
        DataSet ds = new DataSet();
        try
        {
            DAL = new cDataAccess();
            c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_GetCommunicationGroupType";
            DAL.GetDataset(c, ref ds);

            ddlCommunicationGroupType.DataSource = ds.Tables[0];
            ddlCommunicationGroupType.DataTextField = "GroupTypeName";
            ddlCommunicationGroupType.DataValueField = "GroupTypeID";
            ddlCommunicationGroupType.DataBind();
            ddlCommunicationGroupType.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return ds;
    }

    #endregion

}

