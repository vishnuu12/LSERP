using eplus.core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_DailyActivitiesReportForDesignEnquiry : System.Web.UI.Page
{
    #region"Declarartion"

    cSession objSession = new cSession();
    cDesign objDesign;
    cCommon objcommon = new cCommon();
    EmailAndSmsAlerts objAlerts;

    #endregion

    #region"PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        objSession = Master.csSession;
    }

    #endregion

    #region"Page Load Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                cDesign objDesign = new cDesign();
                DataSet dsEnquiryNumber = new DataSet();
                DataSet ds = new DataSet();
                ds = objDesign.getPendingDesignEnquiryIDByUserID(Convert.ToInt32(objSession.employeeid), ddlCustomerName, ddlEnquiryNumber);
                ViewState["EnquiryDetails"] = ds.Tables[0];
                ViewState["CustomerDetails"] = ds.Tables[1];

                ShowHideControls("add");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlCustomerName_SelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objcommon.customerddlchnage(ddlCustomerName, ddlEnquiryNumber, (DataTable)ViewState["CustomerDetails"], (DataTable)ViewState["EnquiryDetails"]);
            divOutput.Visible = false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlEnquiryNumber_SelectIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt;
        try
        {
            if (ddlEnquiryNumber.SelectedIndex > 0)
            {
                objcommon.enquiryddlchange(ddlEnquiryNumber, ddlCustomerName, (DataTable)ViewState["CustomerDetails"], (DataTable)ViewState["EnquiryDetails"]); divOutput.Visible = true;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "function", " btnCalculateCost();", true);
            }
            else
                divOutput.Visible = false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Hide Loader", "hideLoader();", true);
    }

    #endregion

    #region"Radio Button Events"

    protected void rblReSchduleSubmissiondate_OnSelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rblReSchduleSubmissiondate.SelectedValue == "No")
                divreschdule.Visible = false;
            else
                divreschdule.Visible = true;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion


    #region"Button Events"

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        ShowHideControls("input");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        objDesign = new cDesign();
        DataSet ds = new DataSet();
        bool rsdate = true;
        try
        {
            objDesign.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue);
            objDesign.CurrentStatus = ddlCurentStatus.SelectedValue;
            objDesign.ReScheduledSubmissiondate = rblReSchduleSubmissiondate.SelectedValue;
            objDesign.Remarks = txtRemarks.Text;
            objDesign.UserID = Convert.ToInt32(objSession.employeeid);

            objDesign.RescheduledateReason = txtReason.Text;
            objDesign.ReScheduledDate = DateTime.ParseExact(txtRescheduleDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (rblReSchduleSubmissiondate.SelectedValue == "Yes")
            {
                objDesign.ReScheduledDate = DateTime.ParseExact(txtRescheduleDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                rsdate = true;
            }
            else
                rsdate = false;

            ds = objDesign.SaveDailyActivitiesDesignEnquiryDetails(rsdate);

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Details Saved Succesfully');", true);
                if (rblReSchduleSubmissiondate.SelectedValue == "Yes")
                    SendRescheduleMailBySalesAndProjectTeam();
            }
            ddlEnquiryNumber_SelectIndexChanged(null, null);
            ShowHideControls("add,view");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ShowHideControls("add,input,view");
    }

    #endregion

    #region"Common Methods"

    private void ShowHideControls(string divids)
    {
        try
        {
            divAdd.Visible = divAddnew.Visible = divInput.Visible = divOutput.Visible = false;
            string[] mode = divids.Split(',');
            for (int i = 0; i < mode.Length; i++)
            {
                switch (mode[i].ToLower())
                {
                    case "add":
                        divAdd.Visible = true;
                        break;
                    case "view":
                        divOutput.Visible = true;
                        break;
                    case "input":
                        divInput.Visible = true;
                        break;
                    case "addnew":
                        divAddnew.Visible = true;
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void SendRescheduleMailBySalesAndProjectTeam()
    {
        DataSet ds = new DataSet();
        try
        {
            //Send Individual Mail
            DataSet dsEmail = new DataSet();
            DataSet dsMessage = new DataSet();
            DataSet dsUpdate = new DataSet();
            objAlerts = new EmailAndSmsAlerts();
            objDesign = new cDesign();
            string Message = "";

            dsEmail = objDesign.GetSalesStaffDetailsByEnquiryID();

            if (dsEmail.Tables[0].Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dsEmail.Tables[0].Rows[0]["Email"].ToString()))
                {
                    objAlerts.MessageID = 19;

                    dsMessage = objAlerts.GetAutomatedMessageMail();
                    Message = dsMessage.Tables[0].Rows[0]["Message"].ToString();
                    Message = Message.Replace("<CustomerName>", ddlCustomerName.SelectedItem.Text);
                    //Message = Message.Replace("<Version>", lblversionNumber.Text);                  
                    Message = Message.Replace("<EnquiryNumber>", ddlEnquiryNumber.SelectedValue);

                    foreach (DataRow dr in dsEmail.Tables[0].Rows)
                    {
                        objAlerts.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue);
                        objAlerts.AlertType = "Mail";

                        objAlerts.reciverID = dr["Email"].ToString();
                        objAlerts.reciverType = "E";
                        objAlerts.GroupID = 0;
                        objAlerts.dtSettings = objAlerts.GetEmailSettings();
                        objAlerts.EmailID = "karthik@innovasphere.in";//dsEmail.Tables[0].Rows[0]["EmailID"].ToString();;
                        objAlerts.Subject = "ReSchedule Submission Date";
                        objAlerts.Message = Message + "/" + txtReason.Text + "/" + txtRemarks.Text;
                        objAlerts.userID = objSession.employeeid;
                        objAlerts.file = "";
                        //save Alert Details
                        //objAlerts.SaveAlertDetails();        
                        //objAlerts.SaveAlertDetails();        
                        objAlerts.SendIndividualMail();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
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

    #endregion
}