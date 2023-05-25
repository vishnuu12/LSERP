using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using eplus.core;
using System.IO;

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
    cCommon objc;
    cMaterials objMat;

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
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];

            if (IsPostBack == false)
            {
                objc = new cCommon();
                DataSet dsEnquiryNumber = new DataSet();
                DataSet dsCustomer = new DataSet();
                dsCustomer = objc.getCustomerNameByUserID(Convert.ToInt32(objSession.employeeid), ddlCustomerName, "LS_GetCustomerNameByEmployeeID");
                ViewState["CustomerDetails"] = dsCustomer.Tables[1];
                dsEnquiryNumber = objc.GetEnquiryNumberByUserID(Convert.ToInt32(objSession.employeeid), ddlEnquiryNumber, "LS_GetEnquiryIDByUserID");
                ViewState["EnquiryDetails"] = dsEnquiryNumber.Tables[0];
                //objDesign.GetEnquiryNumberBySalesUserID(Convert.ToInt32(objSession.employeeid), ddlEnquiryNumber);
                //BindDesignDocumentDetailsByUserID();
            }
            if (target == "NewRevision")
                UpdateNewRevisionDetails();
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
        objc = new cCommon();
        try
        {
            objc.customerddlchnage(ddlCustomerName, ddlEnquiryNumber, (DataTable)ViewState["CustomerDetails"], (DataTable)ViewState["EnquiryDetails"]);
            divOutput.Visible = false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlEnquiryNumber_SelectIndexChanged(object sender, EventArgs e)
    {
        objc = new cCommon();
        DataSet ds = new DataSet();
        objDesign = new cDesign();
        try
        {
            if (ddlEnquiryNumber.SelectedIndex > 0)
            {
                objc.enquiryddlchange(ddlEnquiryNumber, ddlCustomerName, (DataTable)ViewState["CustomerDetails"], (DataTable)ViewState["EnquiryDetails"]);
                divOutput.Visible = true;

                ds = objDesign.GetDesignDetailsByEDIDForApproval(Convert.ToInt32(ddlEnquiryNumber.SelectedValue));

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Message"].ToString() == "NA")
                    {
                        ViewState["Designer"] = ds.Tables[0].Rows[0]["Designer"].ToString();
                        gvDesignApprovalDetails.DataSource = ds.Tables[0];
                        gvDesignApprovalDetails.DataBind();
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
                }
                else
                {
                    gvDesignApprovalDetails.DataSource = "";
                    gvDesignApprovalDetails.DataBind();
                }
            }
            else
                divOutput.Visible = false;
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
        bool itemselected = false;
        try
        {
            int index = Convert.ToInt32(hdnRowIndex.Value);
            //string Designer = ViewState["Designer"].ToString();
            ////string DDID = gvDesignApprovalDetails.DataKeys[index].Values[2].ToString();
            //ds = objHR.GetEmployeeCommunicationDetailsEmployeeID(Convert.ToInt32(Designer));
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    try
            //    {
            foreach (GridViewRow row in gvDesignApprovalDetails.Rows)
            {
                CheckBox chkditems = (CheckBox)row.FindControl("chkitems");
                if (chkditems.Checked)
                {
                    DropDownList ddlRevisionNum = (DropDownList)row.FindControl("ddlRevisionNumber");
                    itemselected = true;
                    DataSet dsApprove = new DataSet();
                    objDesign = new cDesign();
                    objDesign.DDID = Convert.ToInt32(Convert.ToInt32(gvDesignApprovalDetails.DataKeys[row.RowIndex].Values[2].ToString()));
                    objDesign.VersionNumber = Convert.ToInt32(ddlRevisionNum.SelectedValue);
                    objDesign.UpdateDrawingCustomerResponseStatus();
                }
            }
            //    }
            //    catch (Exception ec)
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error Occured');", true);
            //    }
            //    if (itemselected == true)
            //    {
            //        objAlerts.file = "";
            //        objAlerts.dtSettings = objAlerts.GetEmailSettings();
            //        objAlerts.EmailID = ds.Tables[0].Rows[0]["Email"].ToString();// "karthik@innovasphere.in";
            //        objAlerts.Subject = txtHeader_R.Text;
            //        objAlerts.Message = txtMessage_R.Text;
            //        objAlerts.userID = objSession.employeeid;
            //        objAlerts.SendIndividualMail();

            //        Message = "Mail Dispatched Successfully";

            //        objAlerts.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue.Split('/')[0].ToString());
            //        objAlerts.userID = objSession.employeeid;
            //        objAlerts.reciverType = "I";
            //        objAlerts.reciverID = ds.Tables[0].Rows[0]["EmployeeID"].ToString();
            //        objAlerts.Header = txtHeader_R.Text;
            //        objAlerts.Message = txtMessage_R.Text;
            //        objAlerts.AlertType = "Mail";
            //        objAlerts.SaveAlertDetails();
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "hideLoader();SuccessMessage('Success','" + Message + "')", true);
            //    }
            //    else
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','No Items Selected');", true);
            //    }
            //}
            //else
            //{
            //    Message = "Mail Not Sending";
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "hideLoader();ErrorMessage('Error','" + Message + "')", true);
            //}
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "hideLoader();ErrorMessage('Error','Unknown Error Occured')", true);
        }
    }

    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        try
        {
            //Send Individual Mail
            DataSet dsEmail = new DataSet();
            DataSet dsMessage = new DataSet();
            DataSet dsUpdate = new DataSet();
            objFinance = new c_Finance();
            objAlerts = new EmailAndSmsAlerts();
            string Message = "";
            dsEmail = objFinance.GetProspctDetailsByProspectID(Convert.ToInt32(ddlCustomerName.SelectedValue));

            if (dsEmail.Tables[0].Rows.Count > 0)
            {
                if (dsEmail.Tables[0].Rows[0]["EmailID"].ToString() != "")
                {
                    objAlerts.MessageID = 2;

                    dsMessage = objAlerts.GetAutomatedMessageMail();
                    Message = dsMessage.Tables[0].Rows[0]["Message"].ToString();
                    Message = Message.Replace("<CustomerName>", dsEmail.Tables[0].Rows[0]["ProspectName"].ToString());
                    //Message = Message.Replace("<Version>", lblversionNumber.Text);
                    Message = Message.Replace("<Version>", "TEST");
                    Message = Message.Replace("<EnquiryNumber>", ddlEnquiryNumber.SelectedValue);
                    string BaseSavepPath = DrawingDocumentSavePath + ddlEnquiryNumber.SelectedValue + "\\";
                    bool itemselected = false;
                    foreach (GridViewRow row in gvDesignApprovalDetails.Rows)
                    {
                        try
                        {
                            CheckBox chkditems = (CheckBox)row.FindControl("chkitems");
                            if (chkditems.Checked)
                            {
                                itemselected = true;
                                string FileName = gvDesignApprovalDetails.DataKeys[row.RowIndex].Values[0].ToString();
                                objAlerts.file += BaseSavepPath + FileName + "|";
                                objAlerts.AttachementID = Convert.ToInt32(gvDesignApprovalDetails.DataKeys[row.RowIndex].Values[1].ToString());

                                objAlerts.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue);
                                objAlerts.AlertType = "Mail";

                                objAlerts.reciverID = dsEmail.Tables[0].Rows[0]["ProspectID"].ToString();
                                objAlerts.reciverType = "E";
                                objAlerts.GroupID = 0;
                                objAlerts.dtSettings = objAlerts.GetEmailSettings();
                                objAlerts.EmailID = dsEmail.Tables[0].Rows[0]["EmailID"].ToString();//"karthik@innovasphere.in";;
                                objAlerts.Subject = "Drawing Attachements";
                                objAlerts.Message = Message;
                                objAlerts.userID = objSession.employeeid;
                                //save Alert Details
                                ////  objAlerts.SaveAlertDetails();
                                //update Shared With Customer Status In Design Document Details
                                dsUpdate = objDesign.UpdateForwardToCustomerStatusInDesignDocumentDetails(Convert.ToInt32(gvDesignApprovalDetails.DataKeys[row.RowIndex].Values[2].ToString()));
                            }
                        }
                        catch (Exception ec) { }
                    }
                    if (itemselected == true)
                    {
                        objAlerts.file = objAlerts.file.Remove(objAlerts.file.LastIndexOf("|"));
                        //send Mail
                        //    objAlerts.SendIndividualMail();
                        // ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "SuccessMessage('Success','Mail Dispatched Successfully');", true);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "SuccessMessage('Success','Drawing Shared Successfully');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','No Items Selected');", true);
                    }
                }
            }
        }
        catch (Exception ec)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error Occured');", true);
        }
    }
    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            bool itemselected = false;
            foreach (GridViewRow row in gvDesignApprovalDetails.Rows)
            {
                try
                {
                    CheckBox chkditems = (CheckBox)row.FindControl("chkitems");
                    if (chkditems.Checked)
                    {
                        itemselected = true;
                        DataSet dsApprove = new DataSet();
                        objDesign = new cDesign();
                        dsApprove = objDesign.UpdateApproveDesignStatus(Convert.ToInt32(gvDesignApprovalDetails.DataKeys[row.RowIndex].Values[2].ToString()), 1);
                    }
                }
                catch (Exception ec) { }
            }
            if (itemselected == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "SuccessMessage('Success','Drawing Approved Successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','No Items Selected');", true);
            }
        }
        catch (Exception ec)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error Occured');", true);
        }
    }
    protected void btnHold_Click(object sender, EventArgs e)
    {
        try
        {
            bool itemselected = false;
            foreach (GridViewRow row in gvDesignApprovalDetails.Rows)
            {
                try
                {
                    CheckBox chkditems = (CheckBox)row.FindControl("chkitems");
                    if (chkditems.Checked)
                    {
                        itemselected = true;
                        DataSet dsApprove = new DataSet();
                        objDesign = new cDesign();
                        dsApprove = objDesign.UpdateApproveDesignStatus(Convert.ToInt32(gvDesignApprovalDetails.DataKeys[row.RowIndex].Values[2].ToString()), 7);
                    }
                }
                catch (Exception ec) { }
            }
            if (itemselected == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "SuccessMessage('Success','Drawings Holded Successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','No Items Selected');", true);
            }
        }
        catch (Exception ec)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error Occured');", true);
        }
    }

    #endregion

    #region"gridView Events"

    protected void gvDesignApprovalDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string BasehttpPath;
        string BaseSavepPath;
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Attributes.Add("Class", "nosort");
            }
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                BaseSavepPath = DrawingDocumentSavePath + ddlEnquiryNumber.SelectedValue + "\\";
                BasehttpPath = DrawingDocumentHttppath + ddlEnquiryNumber.SelectedValue + "/";
                ImageButton imgbtn = (ImageButton)e.Row.FindControl("imgbtnView");
                DropDownList ddlRevision = (DropDownList)e.Row.FindControl("ddlRevisionNumber");
                objDesign.DDID = Convert.ToInt32(gvDesignApprovalDetails.DataKeys[e.Row.RowIndex].Values[2].ToString());

                objDesign.GetRevisionNumberDetailsByDDID(ddlRevision);

                if (File.Exists(BaseSavepPath + dr["FileName"].ToString())) imgbtn.ImageUrl = BasehttpPath + dr["FileName"].ToString();
                imgbtn.ToolTip = dr["FileName"].ToString();
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

            //  Label lblEnquiryNumber = (Label)gvDesignApprovalDetails.Rows[index].FindControl("lblEnquiryNumber");
            Label lblversionNumber = (Label)gvDesignApprovalDetails.Rows[index].FindControl("lblVersionNumber");

            string DDID = gvDesignApprovalDetails.DataKeys[index].Values[2].ToString();
            string AttachementID = gvDesignApprovalDetails.DataKeys[index].Values[1].ToString();

            //   string BasehttpPath = DrawingDocumentSavePath + lblEnquiryNumber.Text + "\\";
            string BasehttpPath = DrawingDocumentHttppath + ddlEnquiryNumber.SelectedValue + "/";

            string FileName = gvDesignApprovalDetails.DataKeys[index].Values[0].ToString();

            if (e.CommandName == "ViewDocs")
            {
                objc = new cCommon();
                //byte[] imageBytes = System.IO.File.ReadAllBytes(BasehttpPath + FileName);
                //string base64String = Convert.ToBase64String(imageBytes);
                //imgDocs.ImageUrl = String.Format(@"data:image/jpeg;base64,{0}", base64String);
                ////ifrm.Attributes.Add("src", BasehttpPath + FileName);
                ////string imgname = DrawingDocumentSavePath + ddlEnquiryNumber.SelectedValue + "\\" + FileName;
                ////if (File.Exists(imgname))
                ////    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewPopUp();", true);
                ////else
                ////{
                ////    ifrm.Attributes.Add("src", BasehttpPath + FileName);
                ////    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Drawing Not Found');", true);
                ////}
                objc.ViewFileName(DrawingDocumentSavePath, DrawingDocumentHttppath, FileName, ddlEnquiryNumber.SelectedValue.ToString(), ifrm);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void UpdateNewRevisionDetails()
    {
        DataSet ds = new DataSet();
        objHR = new c_HR();
        objDesign = new cDesign();
        objAlerts = new EmailAndSmsAlerts();
        string Message = "";
        bool itemselected = false;
        try
        {
            int index = Convert.ToInt32(hdnRowIndex.Value);
            foreach (GridViewRow row in gvDesignApprovalDetails.Rows)
            {
                CheckBox chkditems = (CheckBox)row.FindControl("chkitems");
                if (chkditems.Checked)
                {
                    DropDownList ddlRevisionNum = (DropDownList)row.FindControl("ddlRevisionNumber");
                    if (ddlRevisionNum.SelectedIndex > 0)
                    {
                        itemselected = true;
                        DataSet dsApprove = new DataSet();
                        objDesign = new cDesign();
                        objDesign.DDID = Convert.ToInt32(Convert.ToInt32(gvDesignApprovalDetails.DataKeys[row.RowIndex].Values[2].ToString()));
                        objDesign.VersionNumber = Convert.ToInt32(ddlRevisionNum.SelectedValue);
                        ds = objDesign.UpdateDrawingCustomerResponseStatus();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ErrorMessage('Error','Select Drawing revision Number');", true);
                        break;
                    }
                }
            }
            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "SuccessMessage('Success','revision Changed Succesfully');", true);
            ddlEnquiryNumber_SelectIndexChanged(null, null);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "hideLoader();ErrorMessage('Error','Unknown Error Occured')", true);
        }
    }

    #endregion

    #region"PageLoad Complete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvDesignApprovalDetails.Rows.Count > 0)
            gvDesignApprovalDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion

}