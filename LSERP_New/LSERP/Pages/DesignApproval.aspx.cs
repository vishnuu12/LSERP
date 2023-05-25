using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

public partial class Pages_DesignApproval : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cDesign objDesign = new cDesign();
    EmailAndSmsAlerts objAlerts;
    c_Finance objFinance;
    c_HR objHR;
    cSales objSales;
    string DrawingDocumentSavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString();
    string DrawingDocumentHttppath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString();

    #endregion

    #region"PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        objSession = Master.csSession;
    }

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                objDesign.GetEnquiryNumberBySalesUserID(Convert.ToInt32(objSession.employeeid), ddlEnquiryNumber);
                //BindDesignDocumentDetailsByUserID();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlEnquiryNumber_SelectIndexChanged(object sender, EventArgs e)
    {
        objDesign = new cDesign();
        DataSet ds = new DataSet();
        try
        {
            if (ddlEnquiryNumber.SelectedIndex > 0)
            {
                divOutput.Visible = true;

                objDesign.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue.Split('/')[0].ToString());

                ds = objDesign.GetDesignDetailsByEnquiryIDForApproval();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvDesignApprovalDetails.DataSource = ds.Tables[0];
                    gvDesignApprovalDetails.DataBind();
                }
                else
                {
                    gvDesignApprovalDetails.DataSource = "";
                    gvDesignApprovalDetails.DataBind();
                }
            }
            else
                divOutput.Visible = false;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "hideLoader();", true);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnSendmailReply_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objHR = new c_HR();
        objDesign = new cDesign();
        objAlerts = new EmailAndSmsAlerts();
        string Message = "";
        try
        {
            int index = Convert.ToInt32(hdnRowIndex.Value);
            string Designer = gvDesignApprovalDetails.DataKeys[index].Values[3].ToString();

            ds = objHR.GetEmployeeCommunicationDetailsEmployeeID(Convert.ToInt32(Designer));
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Email"].ToString() != "")
                {
                    objAlerts.file = "";
                    objAlerts.dtSettings = objAlerts.GetEmailSettings();
                    objAlerts.EmailID = ds.Tables[0].Rows[0]["Email"].ToString();// "karthik@innovasphere.in";
                    objAlerts.Subject = txtHeader_R.Text;
                    objAlerts.Message = txtMessage_R.Text;
                    objAlerts.userID = objSession.employeeid;
                    objAlerts.SendIndividualMail();

                    Message = "Mail Dispatched Successfully";

                    objAlerts.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue.Split('/')[0].ToString());
                    objAlerts.userID = objSession.employeeid;
                    objAlerts.reciverType = "I";
                    objAlerts.reciverID = ds.Tables[0].Rows[0]["EmployeeID"].ToString();
                    objAlerts.Header = txtHeader_R.Text;
                    objAlerts.Message = txtMessage_R.Text;
                    objAlerts.AlertType = "Mail";
                    objAlerts.SaveAlertDetails();
                }
            }
            else
                Message = "Mail Not Sending";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "hideLoader();SuccessMessage('Success','" + Message + "')", true);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"gridView Events"

    protected void gvDesignApprovalDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string extension = "";
        string BasehttpPath;
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblEnquiryNumber = (Label)e.Row.FindControl("lblEnquiryNumber");

                // BasehttpPath = DrawingDocumentSavePath + lblEnquiryNumber.Text + "\\";

                BasehttpPath = DrawingDocumentHttppath + lblEnquiryNumber.Text + "/";

                extension = dr["FileName"].ToString().Split('.')[1].ToUpper();
                ImageButton imgbtn = (ImageButton)e.Row.FindControl("imgbtnView");

                byte[] imageBytes = System.IO.File.ReadAllBytes(BasehttpPath + dr["FileName"].ToString());
                string base64String = Convert.ToBase64String(imageBytes);
                imgbtn.ImageUrl = String.Format(@"data:image/jpeg;base64,{0}", base64String);

                imgbtn.ToolTip = dr["FileName"].ToString();

                Button btnSendMail = (Button)e.Row.FindControl("btnSendMail");
                Button btnReply = (Button)e.Row.FindControl("btnReplyToDesigner");

                if (dr["RowNumber"].ToString() == "1")
                {
                    btnSendMail.Visible = true;
                    btnReply.Visible = true;
                }
                else
                {
                    btnSendMail.Visible = false;
                    btnReply.Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvDesignApprovalDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        objDesign = new cDesign();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            Label lblEnquiryNumber = (Label)gvDesignApprovalDetails.Rows[index].FindControl("lblEnquiryNumber");
            Label lblversionNumber = (Label)gvDesignApprovalDetails.Rows[index].FindControl("lblVersionNumber");

            string DDID = gvDesignApprovalDetails.DataKeys[index].Values[2].ToString();
            string AttachementID = gvDesignApprovalDetails.DataKeys[index].Values[1].ToString();

            //   string BasehttpPath = DrawingDocumentSavePath + lblEnquiryNumber.Text + "\\";
            string BasehttpPath = DrawingDocumentHttppath + lblEnquiryNumber.Text + "/";

            string FileName = gvDesignApprovalDetails.DataKeys[index].Values[0].ToString();

            if (e.CommandName == "ViewDocs")
            {
                byte[] imageBytes = System.IO.File.ReadAllBytes(BasehttpPath + FileName);
                string base64String = Convert.ToBase64String(imageBytes);
                imgDocs.ImageUrl = String.Format(@"data:image/jpeg;base64,{0}", base64String);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewPopUp();", true);
            }

            else if (e.CommandName == "SendMail")
            {
                //Send Individual Mail
                DataSet dsEmail = new DataSet();
                DataSet dsMessage = new DataSet();
                DataSet dsUpdate = new DataSet();
                objFinance = new c_Finance();
                objAlerts = new EmailAndSmsAlerts();
                string Message = "";
                dsEmail = objFinance.GetProspctDetailsByProspectID(Convert.ToInt32(ddlEnquiryNumber.SelectedValue.Split('/')[1].ToString()));

                if (dsEmail.Tables[0].Rows.Count > 0)
                {
                    if (dsEmail.Tables[0].Rows[0]["EmailID"].ToString() != "")
                    {
                        objAlerts.MessageID = 2;

                        dsMessage = objAlerts.GetAutomatedMessageMail();
                        Message = dsMessage.Tables[0].Rows[0]["Message"].ToString();
                        Message = Message.Replace("<CustomerName>", dsEmail.Tables[0].Rows[0]["ProspectName"].ToString());
                        Message = Message.Replace("<Version>", lblversionNumber.Text);
                        Message = Message.Replace("<EnquiryNumber>", ddlEnquiryNumber.SelectedValue.Split('/')[0].ToString());

                        objAlerts.AttachementID = Convert.ToInt32(AttachementID);

                        objAlerts.file = BasehttpPath + FileName;

                        objAlerts.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue.Split('/')[0].ToString());
                        objAlerts.AlertType = "Mail";

                        objAlerts.reciverID = dsEmail.Tables[0].Rows[0]["ProspectID"].ToString();
                        objAlerts.reciverType = "E";
                        objAlerts.GroupID = 0;
                        objAlerts.dtSettings = objAlerts.GetEmailSettings();
                        objAlerts.EmailID = dsEmail.Tables[0].Rows[0]["EmailID"].ToString();//"karthik@innovasphere.in";;
                        objAlerts.Subject = "Drawing Attachements";
                        objAlerts.Message = Message;
                        objAlerts.userID = objSession.employeeid;
                        //send Mail
                        objAlerts.SendIndividualMail();

                        //save Alert Details
                        objAlerts.SaveAlertDetails();

                        //update Shared With Customer Status In Design Document Details
                        dsUpdate = objDesign.UpdateForwardToCustomerStatusInDesignDocumentDetails(Convert.ToInt32(DDID));

                        if (dsUpdate.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "SuccessMessage('Success','Mail Dispatched Successfully');hideLoader();", true);
                    }
                }
            }

            else if (e.CommandName == "Approve")
            {
                DataSet dsApprove = new DataSet();
                objDesign = new cDesign();

                dsApprove = objDesign.UpdateApproveDesignStatus(Convert.ToInt32(DDID));

                if (dsApprove.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "SuccessMessage('Success','Drawing Approved Successfully');hideLoader();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion


    #region"Common Methods"

    #endregion
}