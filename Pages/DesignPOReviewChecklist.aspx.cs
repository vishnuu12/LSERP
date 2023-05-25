using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Web.UI.HtmlControls;
using eplus.data;
using System.Data;
using System.Drawing;
using System.Text;
using System.Configuration;
using System.IO;

public partial class Pages_DesignPOReviewChecklist : System.Web.UI.Page
{
    #region"Declaration"

    cSession _objSession = new cSession();
    cCommonMaster _objCommon = new cCommonMaster();
    cCommon _objc = new cCommon();
    cSales objSales = new cSales();
    EmailAndSmsAlerts _objAlert;

    #endregion

    #region "PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        _objSession = Master.csSession;
    }

    #endregion

    #region "PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                GetEnquiryReviewCheckListDetails();
                ShowHideControls("view");
            }
            else
            {
                return;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btndiscrepancye_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidateFields())
            {
                saveporeviewdetails(1);
            }

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnnodiscrepancy_Click(object sender, EventArgs e)
    {
        try
        {
            if (ValidateFields())
            {
                saveporeviewdetails(0);
            }

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        _objAlert = new EmailAndSmsAlerts();
        try
        {
            string ReceiverGroup;
            int Receiver = 0;
            string Message = "";
            DataTable dt;

            if (ddlReceiverGroup.SelectedValue == "All")
            {
                dt = (DataTable)ViewState["EmployeeListBySendCommunication"];
                ReceiverGroup = "G";
                objSales.ReplyRequired = "N";
            }
            else
            {
                ReceiverGroup = "I";

                dt = (DataTable)ViewState["EmployeeListBySendCommunication"];

                int EmployeeID = Convert.ToInt32(ddlReceiverGroup.SelectedValue);

                DataView dv = new DataView(dt);

                dv.RowFilter = "EmployeeID='" + EmployeeID + "'";

                dt = dv.ToTable();

                Receiver = EmployeeID;

                objSales.ReplyRequired = rblReplyStatus.SelectedValue;
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["Email"].ToString() != "")
                {
                    _objAlert.file = "";
                    _objAlert.dtSettings = _objAlert.GetEmailSettings();
                    _objAlert.EmailID = dt.Rows[i]["Email"].ToString();// "karthik@innovasphere.in";
                    _objAlert.Subject = txtAddHeader.Text;
                    _objAlert.Message = txtMessage.Text;
                    _objAlert.userID = _objSession.employeeid;
                    _objAlert.SendIndividualMail();
                    Message = "Mail Dispatched";
                }
                else
                    Message = "Mail Not Sending";

            }

            objSales.EnquiryNumber = ViewState["EnquiryNumber"].ToString();
            objSales.sender = Convert.ToInt32(_objSession.employeeid);
            objSales.ReceiverType = Convert.ToChar(ReceiverGroup);
            objSales.Receiver = Receiver;
            objSales.Header = txtAddHeader.Text;
            objSales.Message = txtMessage.Text;
            objSales.AlertType = "Email";
            objSales.Type = "POReview";

            objSales.saveEnquiryCommunication();

            ClearMailMessage();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "hideLoader();SuccessMessage('Success','Message Saved Succesfully " + Message + "');", true);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnViewCommunication_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objSales = new cSales();
        StringBuilder sbDivReceiver = new StringBuilder();
        try
        {
            objSales.EnquiryNumber = ViewState["EnquiryNumber"].ToString();
            objSales.UserID = Convert.ToInt32(_objSession.employeeid);

            ds = objSales.GetEnquiryCommunicationMessage();

            ViewState["EnquiryCommunicationDetails"] = ds.Tables[0];

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    sbDivReceiver.Append("<div class='col-sm-12 p-t-30' id='divsender" + i.ToString() + "'>");
                    sbDivReceiver.Append("<div class='col-sm-5' style='background-color:#e1d3d3;'>");

                    sbDivReceiver.Append("<div class='col-sm-12'>");
                    sbDivReceiver.Append("<div class='col-sm-6' style='padding-left:0px;'>");
                    sbDivReceiver.Append("<label class='form-label'>Sender</label>");
                    sbDivReceiver.Append("<span>" + ds.Tables[0].Rows[i]["SenderName"].ToString() + "</span>");
                    sbDivReceiver.Append("</div>");
                    sbDivReceiver.Append("<div class='col-sm-6'>");
                    sbDivReceiver.Append("<label class='form-label'>PostedOn</label>");
                    sbDivReceiver.Append("<span>" + ds.Tables[0].Rows[i]["PostedOn"].ToString() + "</span>");
                    sbDivReceiver.Append("</div>");
                    sbDivReceiver.Append("</div>");
                    sbDivReceiver.Append("<div class='col-sm-12'>");
                    sbDivReceiver.Append("<label class='form-label'>Header</label>");
                    sbDivReceiver.Append("<span style='overflow-wrap: break-word;'>" + ds.Tables[0].Rows[i]["Header"].ToString() + "</span>");
                    sbDivReceiver.Append("</div>");
                    sbDivReceiver.Append("<div class='col-sm-12'>");
                    sbDivReceiver.Append("<label class='form-label'>Message</label>");
                    sbDivReceiver.Append("<span style='overflow-wrap: break-word;'>" + ds.Tables[0].Rows[i]["Message"].ToString() + "</span>");
                    sbDivReceiver.Append("</div>");

                    if (ds.Tables[0].Rows[i]["ReplyRequired"].ToString() == "Y" && ds.Tables[0].Rows[i]["ReplyStatus"].ToString() == "0")
                    {
                        if ((ds.Tables[0].Rows[i]["ERPUserType"].ToString() == "3" && ds.Tables[0].Rows[i]["Receiver"].ToString() == _objSession.employeeid)
                            || (ds.Tables[0].Rows[i]["ERPUserType"].ToString() == "2" && ds.Tables[0].Rows[i]["Receiver"].ToString() == _objSession.employeeid))
                        {
                            if (ds.Tables[0].Rows[i]["Color"].ToString() == "Red")
                            {
                                sbDivReceiver.Append("<div class='col-sm-12'>");
                                sbDivReceiver.Append("<input ID='ContentPlaceHolder1_btnReply_" + ds.Tables[0].Rows[i]["ECID"].ToString() + "' style='width:50%;color:red;' value='Reply' type='button' onclick='ShowReplyPopUp(this);'></input>");
                                sbDivReceiver.Append("</div>");
                            }
                            else
                            {
                                sbDivReceiver.Append("<div class='col-sm-12'>");
                                sbDivReceiver.Append("<input ID='ContentPlaceHolder1_btnReply_" + ds.Tables[0].Rows[i]["ECID"].ToString() + "' style='width:50%;color:Green;' value='Reply' type='button' Onclick='ShowReplyPopUp(this);'></input>");
                                sbDivReceiver.Append("</div>");
                            }
                        }
                        else if (ds.Tables[0].Rows[i]["ERPUserType"].ToString() == "1")
                        {
                            if (ds.Tables[0].Rows[i]["Color"].ToString() == "Red")
                            {
                                sbDivReceiver.Append("<div class='col-sm-12'>");
                                sbDivReceiver.Append("<input ID='ContentPlaceHolder1_btnReply_" + ds.Tables[0].Rows[i]["ECID"].ToString() + "' style='width:50%;color:red;' value='Reply' type='button' onclick='ShowReplyPopUp(this);'></input>");
                                sbDivReceiver.Append("</div>");
                            }
                            else
                            {
                                sbDivReceiver.Append("<div class='col-sm-12'>");
                                sbDivReceiver.Append("<input ID='ContentPlaceHolder1_btnReply_" + ds.Tables[0].Rows[i]["ECID"].ToString() + "' style='width:50%;color:Green;' value='Reply' type='button' Onclick='ShowReplyPopUp(this);'></input>");
                                sbDivReceiver.Append("</div>");
                            }
                        }
                    }

                    sbDivReceiver.Append("</div>");
                    sbDivReceiver.Append("<div class='col-sm-2'></div>");
                    sbDivReceiver.Append("<div class='col-sm-5' style='background-color:#8f858524;'>");
                    if (ds.Tables[0].Rows[i]["RepliedOn"].ToString() != "")
                    {
                        sbDivReceiver.Append("<div class='col-sm-12'>");
                        sbDivReceiver.Append("<div class='col-sm-6' style='padding-left:0px;'>");

                        sbDivReceiver.Append("<label class='form-label'>Receiver</label>");
                        sbDivReceiver.Append("<span>" + ds.Tables[0].Rows[i]["ReceiverName"].ToString() + "</span>");
                        sbDivReceiver.Append("</div>");

                        sbDivReceiver.Append("<div class='col-sm-6'>");
                        sbDivReceiver.Append("<label class='form-label'>RepliedOn</label>");
                        sbDivReceiver.Append("<span>" + ds.Tables[0].Rows[i]["RepliedOn"].ToString() + "</span>");
                        sbDivReceiver.Append("</div>");
                        sbDivReceiver.Append("</div>");

                        sbDivReceiver.Append("<div class='col-sm-12'>");
                        sbDivReceiver.Append("<label class='form-label'>Header</label>");
                        sbDivReceiver.Append("<span style='overflow-wrap: break-word;'>" + ds.Tables[0].Rows[i]["RepliedHeader"].ToString() + "</span>");
                        sbDivReceiver.Append("</div>");
                        sbDivReceiver.Append("<div class='col-sm-12'>");
                        sbDivReceiver.Append("<label class='form-label'>Message</label>");
                        sbDivReceiver.Append("<span style='overflow-wrap: break-word;'>" + ds.Tables[0].Rows[i]["RepliedMessage"].ToString() + "</span>");
                        sbDivReceiver.Append("</div>");

                        sbDivReceiver.Append("</div>");
                    }
                    sbDivReceiver.Append("</div>");
                    sbDivReceiver.Append("</div>");
                }
                DivReceiver.InnerHtml = sbDivReceiver.ToString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Function", "ShowViewCommunicationPopUp();", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Function", "InfoMessage('Information','No Message');", true);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnReplyMessage_Click(object sender, EventArgs e)
    {
        DataTable dt;
        DataSet ds = new DataSet();
        _objAlert = new EmailAndSmsAlerts();
        objSales = new cSales();
        string Message = "";
        try
        {
            if (ValidateReplyMessageField())
            {
                int ECID = Convert.ToInt32(hdnECID.Value);
                dt = (DataTable)ViewState["EnquiryCommunicationDetails"];
                DataView dv = new DataView(dt);
                dv.RowFilter = "ECID='" + ECID + "'";
                dt = dv.ToTable();
                string Email = dt.Rows[0]["Email"].ToString();
                objSales.Message = txtReplyMessage.Text;

                if (Email != "")
                {
                    _objAlert.file = "";
                    _objAlert.dtSettings = _objAlert.GetEmailSettings();
                    _objAlert.EmailID = Email;//"karthik@innovasphere.in";
                    _objAlert.Subject = txtHeader.Text;
                    _objAlert.Message = txtReplyMessage.Text;
                    _objAlert.userID = _objSession.employeeid;
                    _objAlert.SendIndividualMail();
                    Message = "Mail Dispatched";
                }
                else
                    Message = "Mail Not Sending";


                //save Message Details
                objSales.ECID = ECID;
                objSales.Header = txtHeader.Text;
                objSales.Message = txtReplyMessage.Text;

                ds = objSales.SaveEnquiryCommunicationReplyMessage();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Replied")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Function", "SuccessMessage('Success','Message Saved Successfully " + Message + "');", true);

                ClearReplyMessageValues();
                btnViewCommunication_Click(null, null);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ShowHideControls("view");
        gvEnquiryReviewCheckList.UseAccessibleHeader = true;
        gvEnquiryReviewCheckList.HeaderRow.TableSection = TableRowSection.TableHeader;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "showDataTable();", true);
    }

    #endregion

    #region "GridView Events"

    protected void gvEnquiryReviewCheckList_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();

        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            objSales.POHID = gvEnquiryReviewCheckList.DataKeys[index].Values[0].ToString();
            ViewState["POHID"] = objSales.POHID;

            string CusstomerEnquirySavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString();
            string CustomerEnquiryHttpPath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString();

            ViewState["EnquiryNumber"] = gvEnquiryReviewCheckList.DataKeys[index].Values[1].ToString();

            if (e.CommandName == "EditEnquiryReviewCheckList")
            {
                BindEnquiryReviewCheckList();
                ShowHideControls("edit");
            }
            else if (e.CommandName == "ViewEnquiryReviewCheckList")
            {
                ShowEnquiryReviewCheckList();
            }

            string BaseHtttpPath = CustomerEnquiryHttpPath + ViewState["EnquiryNumber"].ToString() + "/";
            string FileName = "";
            if (e.CommandName == "viewpocopy")
            {
                FileName = gvEnquiryReviewCheckList.DataKeys[index].Values[2].ToString();
                ViewState["FileName"] = FileName;
                ifrm.Attributes.Add("src", BaseHtttpPath + FileName);

                if (File.Exists(CusstomerEnquirySavePath + ViewState["EnquiryNumber"].ToString() + "//" + FileName))
                {
                    ViewState["ifrmsrc"] = BaseHtttpPath + FileName;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewdocsPopUp();", true);
                }
                else
                {
                    ifrm.Attributes.Add("src", "");
                    ViewState["ifrmsrc"] = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','PO Copy Not Found');", true);
                }
            }
            if (e.CommandName == "viewpocopywithoutprice")
            {
                FileName = gvEnquiryReviewCheckList.DataKeys[index].Values[3].ToString();
                ViewState["FileName"] = FileName;
                ifrm.Attributes.Add("src", BaseHtttpPath + FileName);

                if (File.Exists(CusstomerEnquirySavePath + ViewState["EnquiryNumber"].ToString() + "//" + FileName))
                {
                    ViewState["ifrmsrc"] = BaseHtttpPath + FileName;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewdocsPopUp();", true);
                }
                else
                {
                    ifrm.Attributes.Add("src", "");
                    ViewState["ifrmsrc"] = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','PO Copy Without Not Found');", true);
                }
            }


        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    protected void gvEnquiryReviewCheckList_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (_objSession.type == 2 || _objSession.type == 4)
                    e.Row.Cells[6].Attributes.Add("style", "display:none;");
                else if (_objSession.type == 3 || _objSession.type == 5)
                    e.Row.Cells[7].Attributes.Add("style", "display:none;");
            }
            //if (ViewState["UserTypeID"].ToString() != "4")
            //{
            //    e.Row.Cells[6].Enabled = false;
            //}
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (dr["Completionsattus"].ToString() == "Incomplete")
                    e.Row.Attributes.Add("style", "Background-color:#ffd400");
                if (_objSession.type == 2 || _objSession.type == 4)
                    e.Row.Cells[6].Attributes.Add("style", "display:none;");
                else if (_objSession.type == 3 || _objSession.type == 5)
                    e.Row.Cells[7].Attributes.Add("style", "display:none;");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"
    public void saveporeviewdetails(int completion)
    {
        try
        {
            DataSet ds = new DataSet();
            objSales.POHID = ViewState["POHID"].ToString();
            objSales.UStamp = rblUStamp.SelectedValue;
            objSales.UStampRemarks = txtUStampRemarks.Text;
            objSales.U2Stamp = rblU2Stamp.SelectedValue;
            objSales.U2StampRemarks = txtU2StampRemarks.Text;
            objSales.PED = rblPED.SelectedValue;
            objSales.PEDRemarks = txtPEDremarks.Text;
            objSales.IBR = rblIBR.SelectedValue;
            objSales.IBRRemarks = txtIBRRemarks.Text;
            objSales.ISO = rblISO.SelectedValue;
            objSales.ISORemarks = txtISODetailsRemarks.Text;
            objSales.SpecialInformation = rblsplinfo.SelectedValue;
            objSales.SpecialInformationRemarks = txtsplinfo.Text;
            objSales.Units = rblunits.SelectedValue;
            objSales.UnitsRemarks = txtunits.Text;
            objSales.CheckedwithOffer = ddloffercheck.SelectedValue;
            objSales.CheckedwithOfferRemarks = txtoffercheck.Text;
            objSales.TechnicalFeasibility = ddltechnicalfeasibile.SelectedValue;
            objSales.TechnicalFeasibilityRemarks = txttechnicalfeasibile.Text;
            objSales.Information = ddlinfo.SelectedValue;
            objSales.InformationRemarks = txtinfo.Text;
            objSales.Statutory = ddlStatutory.SelectedValue;
            objSales.StatutoryRemarks = txtStatutory.Text;
            objSales.AdditionalNote = txtAddtionalNote.Text;
            objSales.Poreviwecompletionstatus = completion;

            ds = objSales.SaveDesignPOReviewClarrification();
            //objSales.completionStatus = lblCompletionStatus.Text;


            //objSales.UserID = Convert.ToInt32(_objSession.employeeid);

            //  ds = objSales.SaveEnquiryReviewClarrification();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Function", "SuccessMessage('Success','Enquiry Clarrification Details Saved Successfully');hideLoader();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Function", "SuccessMessage('Success','Enquiry Clarrification Details Updated Successfully');hideLoader();", true);
            }

            clearFields();
            GetEnquiryReviewCheckListDetails();
            ShowHideControls("view");
            if (completion == 1)
                SaveAlertDetails(ds.Tables[0].Rows[0]["EnquiryNo"].ToString());
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    private bool ValidateFields()
    {
        bool isValid = true;
        try
        {
            //   List<int,string> dct=new Dictionary<int,string>;
            string error = "";

            foreach (Control c in divInput.Controls)
            {
                if (c is TextBox)
                {
                    TextBox txtbox = (TextBox)c;
                    if (txtbox.Attributes["class"].Contains("mandatoryfield") && txtbox.Text == "")
                    {
                        isValid = false;
                        error = "Remarks";
                    }
                }
                if (c is TextBox)
                {
                    DropDownList ddl = (DropDownList)c;
                    if (ddl.SelectedValue == "0")
                    {
                        isValid = false;
                        error = "Dropdown";
                    }
                }
            }
            if (isValid == false) ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "ErrorMessage('Error','Field Required " + error + "');showDataTable();", true);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return isValid;
    }


    private void BindEnquiryReviewCheckList()
    {
        try
        {
            DataTable dt = (DataTable)ViewState["EnquiryReviewCheckList"];

            dt.DefaultView.RowFilter = "POHID='" + objSales.POHID + "'";
            lblCustomerName.Text = dt.DefaultView.ToTable().Rows[0]["ProspectName"].ToString();
            ViewState["EnquiryNumber"] = dt.DefaultView.ToTable().Rows[0]["EnquiryNumber"].ToString();
            lblPoNumber.Text = dt.DefaultView.ToTable().Rows[0]["PONumber"].ToString();
            lblPoDate.Text = dt.DefaultView.ToTable().Rows[0]["PODate"].ToString();
            lblcompletion.Text = dt.DefaultView.ToTable().Rows[0]["Completionsattus"].ToString();

            DataSet ds = new DataSet();
            ds = objSales.GetPOReviewCheckListDetails("LS_GetDesignPOReviewCheckListDetails");
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtable = ds.Tables[0];
                rblUStamp.SelectedValue = dtable.Rows[0]["UStamp"].ToString();
                txtUStampRemarks.Text = dtable.Rows[0]["UStampRemarks"].ToString();
                rblU2Stamp.SelectedValue = dtable.Rows[0]["U2Stamp"].ToString();
                txtU2StampRemarks.Text = dtable.Rows[0]["U2StampRemarks"].ToString();
                rblPED.SelectedValue = dtable.Rows[0]["PED"].ToString();
                txtPEDremarks.Text = dtable.Rows[0]["PEDRemarks"].ToString();
                rblIBR.SelectedValue = dtable.Rows[0]["IBR"].ToString();
                txtIBRRemarks.Text = dtable.Rows[0]["IBRRemarks"].ToString();
                rblISO.SelectedValue = dtable.Rows[0]["ISO"].ToString();
                txtISODetailsRemarks.Text = dtable.Rows[0]["ISORemarks"].ToString();
                rblsplinfo.SelectedValue = dtable.Rows[0]["SpecialInformation"].ToString();
                txtsplinfo.Text = dtable.Rows[0]["SpecialInformationRemarks"].ToString();
                rblunits.SelectedValue = dtable.Rows[0]["Units"].ToString();
                txtunits.Text = dtable.Rows[0]["UnitsRemarks"].ToString();

                ddloffercheck.SelectedValue = dtable.Rows[0]["CheckedwithOffer"].ToString();
                txtoffercheck.Text = dtable.Rows[0]["CheckedwithOfferRemarks"].ToString();
                ddltechnicalfeasibile.SelectedValue = dtable.Rows[0]["TechnicalFeasibility"].ToString();
                txttechnicalfeasibile.Text = dtable.Rows[0]["TechnicalFeasibilityRemarks"].ToString();
                ddlinfo.SelectedValue = dtable.Rows[0]["Information"].ToString();
                txtinfo.Text = dtable.Rows[0]["InformationRemarks"].ToString();
                ddlStatutory.SelectedValue = dtable.Rows[0]["Statutory"].ToString();
                txtStatutory.Text = dtable.Rows[0]["StatutoryRemarks"].ToString();
                txtAddtionalNote.Text = dtable.Rows[0]["AdditionalNote"].ToString();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "fieldreqadd();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    private void ShowEnquiryReviewCheckList()
    {
        try
        {
            DataTable dt = (DataTable)ViewState["EnquiryReviewCheckList"];
            int EnquiryID = Convert.ToInt32(dt.DefaultView.ToTable().Rows[0]["EnquiryNumber"].ToString());
            dt.DefaultView.RowFilter = "POHID='" + objSales.POHID + "'";
            lblCustomerName_V.Text = dt.DefaultView.ToTable().Rows[0]["ProspectName"].ToString();
            lblponumber_V.Text = dt.DefaultView.ToTable().Rows[0]["PONumber"].ToString();
            lblPoDate_V.Text = dt.DefaultView.ToTable().Rows[0]["PODate"].ToString();
            lblcompletion_V.Text = dt.DefaultView.ToTable().Rows[0]["Completionsattus"].ToString();

            DataSet ds = new DataSet();
            ds = objSales.GetPOReviewCheckListDetails("LS_GetDesignPOReviewCheckListDetails");
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dtable = ds.Tables[0];
                lblUStamp_V.Text = dtable.Rows[0]["UStamp"].ToString();
                lblUStampRemarks_V.Text = dtable.Rows[0]["UStampRemarks"].ToString();
                lblU2Stamp_V.Text = dtable.Rows[0]["U2Stamp"].ToString();
                lblU2StampRemarks_V.Text = dtable.Rows[0]["U2StampRemarks"].ToString();
                lblPED_V.Text = dtable.Rows[0]["PED"].ToString();
                lblPEDRemarks_V.Text = dtable.Rows[0]["PEDRemarks"].ToString();
                lblIBR_V.Text = dtable.Rows[0]["IBR"].ToString();
                lblIBRRemarks_V.Text = dtable.Rows[0]["IBRRemarks"].ToString();
                lblISO_V.Text = dtable.Rows[0]["ISO"].ToString();
                lblISORemarks_V.Text = dtable.Rows[0]["ISORemarks"].ToString();
                lblsplinfo_V.Text = dtable.Rows[0]["SpecialInformation"].ToString();
                lblsplinfoRemarks_V.Text = dtable.Rows[0]["SpecialInformationRemarks"].ToString();
                lblUnits_V.Text = dtable.Rows[0]["Units"].ToString();
                lblUnitsRemarks_V.Text = dtable.Rows[0]["UnitsRemarks"].ToString();

                lbloffercheck_V.Text = dtable.Rows[0]["CheckedwithOffer"].ToString();
                lbloffercheckRemarks_V.Text = dtable.Rows[0]["CheckedwithOfferRemarks"].ToString();
                lbltechnicalfeasibile_V.Text = dtable.Rows[0]["TechnicalFeasibility"].ToString();
                lbltechnicalfeasibileRemarks_V.Text = dtable.Rows[0]["TechnicalFeasibilityRemarks"].ToString();
                lblinfo_V.Text = dtable.Rows[0]["Information"].ToString();
                lblinfoRemarks_V.Text = dtable.Rows[0]["InformationRemarks"].ToString();
                lblStatutory_V.Text = dtable.Rows[0]["Statutory"].ToString();
                lblStatutoryRemarks_V.Text = dtable.Rows[0]["StatutoryRemarks"].ToString();
                lblAddtionalNote_v.Text = dtable.Rows[0]["AdditionalNote"].ToString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "ShowEnquiryCheckListViewPopUp();showDataTable();", true);
                ds = objSales.GetEmployeeListForEnquiryCommunication(EnquiryID, ddlReceiverGroup);

                ViewState["EmployeeListBySendCommunication"] = ds.Tables[0];
                ViewState["EnquiryNumber"] = EnquiryID;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Function", "InfoMessage('Information','No Records');", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ClearMailMessage()
    {
        try
        {
            txtAddHeader.Text = "";
            txtMessage.Text = "";
            ddlReceiverGroup.SelectedValue = "0";
            rblReplyStatus.SelectedValue = "N";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }


    private void clearFields()
    {
        try
        {
            ViewState["ProspectID"] = "";
            ViewState["EnquiryNumber"] = "";
            hdnClarifyID.Value = "0";
            ViewState["POHID"] = "";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private bool ValidateReplyMessageField()
    {
        bool isvalid = true;
        string error = "";
        if (txtHeader.Text == "")
            error = txtHeader.ClientID + "/" + "Field Required";
        else if (txtReplyMessage.Text == "")
            error = txtReplyMessage.ClientID + "/" + "Field Required";

        if (error != "")
        {
            isvalid = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Function", "ValidateReplyControl('" + error + "');", true);
        }

        return isvalid;

    }

    private void ClearReplyMessageValues()
    {
        txtHeader.Text = "";
        txtReplyMessage.Text = "";
    }

    private void GetEnquiryReviewCheckListDetails()
    {
        try
        {
            DataSet dsEnquiryReviewCheckList = new DataSet();
            objSales.UserID = Convert.ToInt32(_objSession.employeeid);

            dsEnquiryReviewCheckList = objSales.GetPOReviewDetails("LS_GetDesignPOReviewDetails");

            ViewState["EnquiryReviewCheckList"] = dsEnquiryReviewCheckList.Tables[0];

            if (dsEnquiryReviewCheckList.Tables[0].Rows.Count > 0)
            {
                gvEnquiryReviewCheckList.DataSource = dsEnquiryReviewCheckList.Tables[0];
                gvEnquiryReviewCheckList.DataBind();
                gvEnquiryReviewCheckList.UseAccessibleHeader = true;
                gvEnquiryReviewCheckList.HeaderRow.TableSection = TableRowSection.TableHeader;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "showDataTable();", true);
            }
            else
            {
                gvEnquiryReviewCheckList.DataSource = "";
                gvEnquiryReviewCheckList.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void SaveAlertDetails(string EnquiryID)
    {
        EmailAndSmsAlerts objAlerts = new EmailAndSmsAlerts();
        DataSet ds = new DataSet();
        cCommon objc = new cCommon();
        try
        {
            objc.EnquiryID = EnquiryID;
            ds = objc.GetStaffNameDetailsByEnquiryID();

            objAlerts.EntryMode = "Individual";
            objAlerts.AlertType = "Mail";
            objAlerts.userID = _objSession.employeeid;
            objAlerts.reciverType = "Staff";
            objAlerts.file = "";
            objAlerts.reciverID = ds.Tables[0].Rows[0]["Sales"].ToString();
            objAlerts.EmailID = "";
            objAlerts.GroupID = 0;
            objAlerts.Subject = "Alert For Sales PO Review";
            objAlerts.Message = "Sales PO Review Request From Enquiry Number" + EnquiryID;
            objAlerts.Status = 0;
            objAlerts.SaveCommunicationEmailAlertDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ShowHideControls(string mode)
    {
        objSales = new cSales();
        DataSet ds = new DataSet();
        try
        {
            divInput.Visible = divOutput.Visible = false;

            switch (mode.ToLower())
            {
                case "edit":
                    divInput.Visible = true;
                    txtUStampRemarks.Focus();
                    divMainform.Visible = true;
                    break;
                case "view":
                    divOutput.Visible = true;
                    hdnClarifyID.Value = "0";
                    break;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"PageLoad Complete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvEnquiryReviewCheckList.Rows.Count > 0)
        {
            gvEnquiryReviewCheckList.UseAccessibleHeader = true;
            gvEnquiryReviewCheckList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    #endregion

}