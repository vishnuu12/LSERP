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
using System.Globalization;
using System.Configuration;
using System.IO;

public partial class Pages_SalesPoreviewChecklist : System.Web.UI.Page
{
    #region"Declaration"

    cSession _objSession = new cSession();
    cCommonMaster _objCommon = new cCommonMaster();
    cCommon _objc = new cCommon();
    cSales objSales = new cSales();
    EmailAndSmsAlerts _objAlert;
    string CusstomerEnquirySavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString();
    string CustomerEnquiryHttpPath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString();


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

    protected void gvAttachments_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dtAttachement;
        cSales objSales = new cSales();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            int AttachementID = Convert.ToInt32(gvAttachments.DataKeys[index].Values[0].ToString());

            dtAttachement = (DataTable)ViewState["Attachement"];
            dtAttachement.DefaultView.RowFilter = "AttachementID='" + AttachementID + "'";
            dtAttachement.DefaultView.ToTable();

            //gvCustomerEnquiry.UseAccessibleHeader = true;
            //gvCustomerEnquiry.HeaderRow.TableSection = TableRowSection.TableHeader;

            if (e.CommandName.ToString() == "ViewDocs")
            {
                string BasehttpPath = CustomerEnquiryHttpPath + ViewState["EnquiryNumber"].ToString() + "/";
                string FileName = ((Label)gvAttachments.Rows[index].FindControl("lblFileName_V")).Text.ToString();
                ViewState["FileName"] = FileName;
                ifrm.Attributes.Add("src", BasehttpPath + FileName);
                string imgname = CusstomerEnquirySavePath + ViewState["EnquiryNumber"].ToString() + "\\" + FileName;
                if (File.Exists(imgname))
                {
                    //ViewState["ifrmsrc"] = imgname;
                    ViewState["ifrmsrc"] = BasehttpPath + FileName;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewdocsPopUp();", true);
                }
                else
                {
                    ifrm.Attributes.Add("src", "");
                    ViewState["ifrmsrc"] = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Drawing Not Found');", true);
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message("CustomerEnquiryProcess" + " " + "gvAttachments_OnRowCommandex" + "" + " " + ex.ToString());
        }
        finally
        {
            dtAttachement = null;
            objSales = null;
        }
    }


    protected void gvAttachments_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            // string BasehttpPath = CusstomerEnquirySavePath + ViewState["EnquiryNumber"].ToString() + "\\";
            string BasehttpPath = CustomerEnquiryHttpPath + ViewState["EnquiryNumber"].ToString() + "/";
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string extension = dr["FileName"].ToString().Split('.')[1].ToUpper();
                ImageButton imgbtn = (ImageButton)e.Row.FindControl("imgbtnView");
                if (extension == "PDF")
                {
                    byte[] imageBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Assets/images/pdf.png"));
                    string base64String = Convert.ToBase64String(imageBytes);
                    imgbtn.ImageUrl = String.Format(@"data:image/jpeg;base64,{0}", base64String);
                }
                else if (extension == "DOC" || extension == "DOCX")
                {
                    byte[] imageBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Assets/images/word-ls.png"));
                    string base64String = Convert.ToBase64String(imageBytes);
                    imgbtn.ImageUrl = String.Format(@"data:image/jpeg;base64,{0}", base64String);

                }
                else if (extension == "XLS" || extension == "XLSX")
                {
                    byte[] imageBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Assets/images/excel-ls.png"));
                    string base64String = Convert.ToBase64String(imageBytes);
                    imgbtn.ImageUrl = String.Format(@"data:image/jpeg;base64,{0}", base64String);
                }
                else
                {
                    //byte[] imageBytes = System.IO.File.ReadAllBytes(BasehttpPath + dr["FileName"].ToString());
                    //string base64String = Convert.ToBase64String(imageBytes);
                    //imgbtn.ImageUrl = String.Format(@"data:image/jpeg;base64,{0}", base64String);
                    imgbtn.ImageUrl = BasehttpPath + dr["FileName"].ToString();
                }

                imgbtn.ToolTip = dr["FileName"].ToString();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvEnquiryReviewCheckList_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();

        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            objSales.POHID = gvEnquiryReviewCheckList.DataKeys[index].Values[0].ToString();
            ViewState["POHID"] = objSales.POHID;

            ViewState["EnquiryNumber"] = gvEnquiryReviewCheckList.DataKeys[index].Values[1].ToString();

            LinkButton btn = (LinkButton)gvEnquiryReviewCheckList.Rows[index].FindControl("lbtnEdit");

            Label lblCompletionStatus = (Label)gvEnquiryReviewCheckList.Rows[index].FindControl("lblCompletionStatus");

            if (e.CommandName == "EditEnquiryReviewCheckList")
            {
                if (lblCompletionStatus.Text == "Completed")
                {
                    BindEnquiryReviewCheckList();
                    ShowHideControls("edit");
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Discrepancy in designer review for this PO');", true);

            }
            else if (e.CommandName == "ViewEnquiryReviewCheckList")
            {
                ShowEnquiryReviewCheckList();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "  ShowEnquiryAttachementsPopUp();", true);

            }

            if (e.CommandName == "ViewEnqAttachements")
            {
                bindEnquiryAttachementDetails();
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
            if (e.CommandName == "PDFReviewChecklist")
            {
                objSales = new cSales();
                ds = new DataSet();
                objSales.POHID = gvEnquiryReviewCheckList.DataKeys[index].Values[0].ToString();
                ds = objSales.GetSalesPoReviewCheckListdetailsByPOHIDForPDf();

                ViewState["Address"] = ds.Tables[2];

                if (ds.Tables[1].Rows.Count > 0)
                {
                    lblNameOftheCustomer_P.Text = ds.Tables[0].Rows[0]["ProspectName"].ToString();
                    lblCustomerPONo_p.Text = ds.Tables[0].Rows[0]["PORefNo"].ToString();
                    lblItem_p.Text = ds.Tables[0].Rows[0]["ItemQty"].ToString();
                    lblUnits_p.Text = ds.Tables[0].Rows[0]["Units"].ToString();
                    lblUStampRequired_p.Text = ds.Tables[0].Rows[0]["UStamp"].ToString();
                    lblU2StampRequired_p.Text = ds.Tables[0].Rows[0]["U2Stamp"].ToString();
                    lblPEDRequired_p.Text = ds.Tables[0].Rows[0]["PED"].ToString();
                    lblISO_p.Text = ds.Tables[0].Rows[0]["ISO"].ToString();
                    lblIBRRequired_p.Text = ds.Tables[0].Rows[0]["IBR"].ToString();
                    lblCheckedwithoffer_p.Text = ds.Tables[0].Rows[0]["CheckedwithOffer"].ToString();
                    lblTechnicalFeasibility_p.Text = ds.Tables[0].Rows[0]["TechnicalFeasibility"].ToString();
                    lblQAp_p.Text = ds.Tables[0].Rows[0]["Information"].ToString();
                    lblStatutary_p.Text = ds.Tables[0].Rows[0]["Statutory"].ToString();
                    // lblStatutaryDetailsSpecified_P.Text = ds.Tables[0].Rows[0]["StatutoryRemarks"].ToString();
                    lblSpecialInformation_p.Text = ds.Tables[0].Rows[0]["SpecialInformation"].ToString();
                    lblAddtionalNote_p.Text = ds.Tables[0].Rows[0]["AdditionalNote"].ToString();
                    lblControlofRecords_p.Text = ds.Tables[0].Rows[0]["ControlOfRecords"].ToString();

                    lblGuaranteeTerms_p.Text = ds.Tables[1].Rows[0]["GuaranteeTerms"].ToString();
                    lblGSt_p.Text = ds.Tables[1].Rows[0]["GST"].ToString();
                    lblPaymentTerms_p.Text = ds.Tables[1].Rows[0]["Payment"].ToString();
                    lblPackingCharges_p.Text = ds.Tables[1].Rows[0]["Packingcharges"].ToString();
                    lblInspection_p.Text = ds.Tables[1].Rows[0]["Inspection"].ToString();
                    lblLDClause_p.Text = ds.Tables[1].Rows[0]["LDclause"].ToString();
                    lblInsurance_p.Text = ds.Tables[1].Rows[0]["Insurance"].ToString();
                    lblTransporter_p.Text = ds.Tables[1].Rows[0]["Transporter"].ToString();
                    lblEwayBill_p.Text = ds.Tables[1].Rows[0]["Roadpermit"].ToString();
                    lblDeliverySchedule_p.Text = ds.Tables[1].Rows[0]["Deliveryschedule"].ToString();
                    lblPRDate_p.Text = ds.Tables[1].Rows[0]["Date"].ToString();

                    GeneratePDDF();
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Complete The Check List Details');", true);
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
                    e.Row.Cells[9].Attributes.Add("style", "display:none;");
                else if (_objSession.type == 3 || _objSession.type == 5)
                    e.Row.Cells[10].Attributes.Add("style", "display:none;");
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (dr["SalesCompletionstatus"].ToString() == "Incomplete")
                    e.Row.Attributes.Add("style", "Background-color:#ffd400");

                if (_objSession.type == 2 || _objSession.type == 4)
                    e.Row.Cells[9].Attributes.Add("style", "display:none;");
                else if (_objSession.type == 3 || _objSession.type == 5)
                    e.Row.Cells[10].Attributes.Add("style", "display:none;");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void bindEnquiryAttachementDetails()
    {
        cSales objSales = new cSales();
        DataSet dsGetAttachementsDetails = new DataSet();
        dsGetAttachementsDetails = objSales.GetEnquiryprocessDetails(ViewState["EnquiryNumber"].ToString(), "LS_GetAttachementsDetails", false);
        ViewState["Attachement"] = dsGetAttachementsDetails.Tables[0];
        try
        {
            if (dsGetAttachementsDetails.Tables[0].Rows.Count > 0)
            {
                gvAttachments.DataSource = dsGetAttachementsDetails.Tables[0];
                gvAttachments.DataBind();
            }
            else
            {
                gvAttachments.DataSource = "";
                gvAttachments.DataBind();
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowEnquiryAttachementsPopUp();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        finally
        {
            objSales = null;
            dsGetAttachementsDetails = null;
        }
    }

    public void saveporeviewdetails(int completion)
    {
        try
        {
            DataSet ds = new DataSet();
            objSales.POHID = ViewState["POHID"].ToString();
            objSales.Payment = rblpayment.SelectedValue;
            objSales.PaymentRemarks = txtpayment.Text;
            objSales.Packingcharges = rblpackingcharges.SelectedValue;
            objSales.PackingchargesRemarks = txtpackingcharges.Text;
            objSales.Inspection = rblInspection.SelectedValue;
            objSales.InspectionRemarks = txtInspection.Text;
            objSales.LDclause = rblLDClause.SelectedValue;
            objSales.LDclauseRemarks = txtLDClause.Text;
            objSales.Insurance = rblInsurance.SelectedValue;
            objSales.InsuranceRemarks = txtInsurance.Text;
            objSales.Transporter = rblTransporter.SelectedValue;
            objSales.TransporterRemarks = txtTransporter.Text;
            objSales.Roadpermit = rblRoadPermit.SelectedValue;
            objSales.RoadpermitRemarks = txtRoadPermit.Text;
            objSales.Deliveryschedule = rblDeliverySchedule.SelectedValue;
            objSales.DeliveryscheduleRemarks = txtDeliverySchedule.Text;
            objSales.GuaranteeTerms = rblGuaranteeTerms.SelectedValue;
            objSales.GuaranteeRemarks = txtGuaranteeRemarks.Text;
            objSales.GST = rblgst.SelectedValue;
            objSales.GSTRemarks = txtgst.Text;
            objSales.Additionalnote = txtAddtionalNote.Text;
            objSales.PRDate = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objSales.SalesPoreviwecompletionstatus = completion;

            ds = objSales.SaveSalesPOReviewClarrification();

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
                rblpayment.SelectedValue = dtable.Rows[0]["Payment"].ToString();
                txtpayment.Text = dtable.Rows[0]["PaymentRemarks"].ToString();
                rblpackingcharges.SelectedValue = dtable.Rows[0]["Packingcharges"].ToString();
                txtpackingcharges.Text = dtable.Rows[0]["PackingchargesRemarks"].ToString();
                rblInspection.SelectedValue = dtable.Rows[0]["Inspection"].ToString();
                txtInspection.Text = dtable.Rows[0]["InspectionRemarks"].ToString();
                rblLDClause.SelectedValue = dtable.Rows[0]["LDclause"].ToString();
                txtLDClause.Text = dtable.Rows[0]["LDclauseRemarks"].ToString();
                rblInsurance.SelectedValue = dtable.Rows[0]["Insurance"].ToString();
                txtInsurance.Text = dtable.Rows[0]["InsuranceRemarks"].ToString();
                rblTransporter.SelectedValue = dtable.Rows[0]["Transporter"].ToString();
                txtTransporter.Text = dtable.Rows[0]["TransporterRemarks"].ToString();
                rblRoadPermit.SelectedValue = dtable.Rows[0]["Roadpermit"].ToString();
                txtRoadPermit.Text = dtable.Rows[0]["RoadpermitRemarks"].ToString();
                rblDeliverySchedule.SelectedValue = dtable.Rows[0]["Deliveryschedule"].ToString();
                txtDeliverySchedule.Text = dtable.Rows[0]["DeliveryscheduleRemarks"].ToString();
                rblgst.SelectedValue = dtable.Rows[0]["GST"].ToString();
                txtgst.Text = dtable.Rows[0]["GSTRemarks"].ToString();
                txtAddtionalNote.Text = dtable.Rows[0]["Additionalnote"].ToString();
                txtDate.Text = ds.Tables[0].Rows[0]["PRDate"].ToString();
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

            objSales.POHID = dt.DefaultView.ToTable().Rows[0]["POHID"].ToString();

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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Function", "InfoMessage('Information','No Records');", true);

            DataSet dsSales = new DataSet();
            dsSales = objSales.GetPOReviewCheckListDetails("LS_GetSalesPOReviewCheckListDetails");

            if (dsSales.Tables[0].Rows.Count > 0)
            {
                lblPayment_V.Text = dsSales.Tables[0].Rows[0]["Payment"].ToString();
                lblPaymentRemarks_V.Text = dsSales.Tables[0].Rows[0]["PaymentRemarks"].ToString();
                lblPackingCharges_V.Text = dsSales.Tables[0].Rows[0]["Packingcharges"].ToString();
                lblPackingChargesRemarks_V.Text = dsSales.Tables[0].Rows[0]["PackingchargesRemarks"].ToString();
                lblInspection_V.Text = dsSales.Tables[0].Rows[0]["Inspection"].ToString();
                lblInspectionRemarks_V.Text = dsSales.Tables[0].Rows[0]["InspectionRemarks"].ToString();
                lblTransporter_V.Text = dsSales.Tables[0].Rows[0]["Transporter"].ToString();
                lblTransporterRemarks_V.Text = dsSales.Tables[0].Rows[0]["TransporterRemarks"].ToString();
                lblRoadpermit.Text = dsSales.Tables[0].Rows[0]["Roadpermit"].ToString();
                lblRoadpermitRemarks_V.Text = dsSales.Tables[0].Rows[0]["RoadpermitRemarks"].ToString();
                lblDeliveryschedule_V.Text = dsSales.Tables[0].Rows[0]["Deliveryschedule"].ToString();
                lblDeliveryscheduleRemarks_V.Text = dsSales.Tables[0].Rows[0]["DeliveryscheduleRemarks"].ToString();
                lblGST_V.Text = dsSales.Tables[0].Rows[0]["GST"].ToString();
                lblGSTRemarks_V.Text = dsSales.Tables[0].Rows[0]["GSTRemarks"].ToString();
                lblAddtionalnote.Text = dsSales.Tables[0].Rows[0]["Additionalnote"].ToString();
                //lblprdate=dsSales.Tables[0].Rows[0]["PRDate"].ToString();
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
                    txtpayment.Focus();
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

    private void GeneratePDDF()
    {
        DataSet ds = new DataSet();
        cCommon objc = new cCommon();
        try
        {
            divCheckListPrint.Attributes.Add("style", "display:block;");

            StringBuilder sbCosting = new StringBuilder();
            divCheckListPrint.RenderControl(new HtmlTextWriter(new StringWriter(sbCosting)));

            string htmlfile = "SalesPoReviewCheckList_" + ViewState["POHID"].ToString() + ".html";
            string pdffile = "SalesPoReviewCheckList_" + ViewState["POHID"].ToString() + ".pdf";
            string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
            string pdfFileURL = LetterPath + pdffile;

            string htmlfileURL = LetterPath + htmlfile;

            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 2].ToString() + "/" + pagename[pagename.Length - 1].ToString();

            string Main = url.Replace(Replacevalue, "Assets/css/main.css");
            string epstyleurl = url.Replace(Replacevalue, "Assets/css/ep-style.css");
            string style = url.Replace(Replacevalue, "Assets/css/style.css");
            string Print = url.Replace(Replacevalue, "Assets/css/print.css");
            string topstrip = url.Replace(Replacevalue, "Assets/images/topstrrip.jpg");

            string LeftLogo = url.Replace(Replacevalue, "Assets/images/lonestar.jpeg");
            string RightLogo = url.Replace(Replacevalue, "Assets/images/iso.jpeg");

            SaveHtmlFile(htmlfileURL, Main, epstyleurl, style, Print, topstrip, sbCosting.ToString(), LeftLogo, RightLogo);

            //   objc.GenerateAndSavePDF(LetterPath, pdfFileURL, pdffile, htmlfileURL);

            divCheckListPrint.Attributes.Add("style", "display:none;");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    public void SaveHtmlFile(string URL, string Main, string epstyleurl, string style, string Print, string topstrip, string div, string leftlogo, string RightLogo)
    {
        try
        {
            DataTable dtAddress = new DataTable();
            dtAddress = (DataTable)ViewState["Address"];

            hdnAddress.Value = dtAddress.Rows[0]["Address"].ToString();
            hdnPhoneAndFaxNo.Value = dtAddress.Rows[0]["PhoneAndFaxNo"].ToString();
            hdnEmail.Value = dtAddress.Rows[0]["Email"].ToString();
            hdnWebsite.Value = dtAddress.Rows[0]["WebSite"].ToString();
            hdnCompanyName.Value = dtAddress.Rows[0]["CompanyName"].ToString();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Function", "PrintSalesPOReviewCheckList();", true);

            //StreamWriter w;
            //w = File.CreateText(URL);
            ////w.WriteLine("<html><head><title>");       
            //w.WriteLine("<html><head><title>");
            //w.WriteLine("Offer");
            //w.WriteLine("</title>");
            //w.WriteLine("<link rel='stylesheet' type='text/css' href='" + epstyleurl + "'/>");
            //w.WriteLine("<link rel='stylesheet' href='" + Main + "' type='text/css'/>");
            //w.WriteLine("<link rel='stylesheet'  href='" + style + "' type='text/css'/>");
            //w.WriteLine("<link rel='stylesheet' href='" + Print + "' type='text/css'/>");
            //w.WriteLine("<link rel='stylesheet' href='" + topstrip + "' type='text/css'/>");

            //w.WriteLine("<style type='text/css'> .heading_p{ font-size: 15px !important;color: #000 !important;margin: 0;font-weight: bold; }");
            //w.WriteLine(".form-label{ margin-left: 20px; } .header{ width: 191mm;left: 8.5mm;border: 0px solid #000; } .page{border: 0px solid #000;}</style>");

            //w.WriteLine("<div class='page'>");
            //w.WriteLine("<table><thead><tr><td>");
            //w.WriteLine("<div class='col-sm-12 header-space' style='text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;height:120px'>");
            //w.WriteLine("<div class='header' style='border-bottom:1px solid;background:transparent'>");
            //w.WriteLine("<div>");
            //w.WriteLine("<div class='col-sm-12 padding0' id='divLSERPLogo' style='margin:0px auto;display:block;padding:5px 0px;'>");
            //w.WriteLine("<div class='row'>");
            //w.WriteLine("<div class='col-sm-2'>");
            //w.WriteLine("<img src='" + leftlogo + "' alt='lonestar-image' width='90px'>");
            //w.WriteLine("</div>");
            //w.WriteLine("<div class='col-sm-8 text-center'>");
            ////w.WriteLine("<h3 style='font-weight:600;font-size:24px;font-style:italic;color:#000;font-family: Arial;display: contents;'>LONESTAR INDUSTRIES</h3>");
            //w.WriteLine("<h3 style='font-weight:600;font-size:24px;font-style:italic;color:#000;font-family: Arial;display: contents;'>LONESTAR <span style='font-weight:600;font-size:24px ! important;font-family: Times New Roman;'>INDUSTRIES</span> </h3>");
            ////w.WriteLine("");
            //w.WriteLine("<p style='font-weight:500;color:#000;width: 100%;'>" + Address + "</p>");
            //w.WriteLine("<p style='font-weight:500;color:#000'>" + PhoneAndFaxNo + "</p>");
            //w.WriteLine("<p style='font-weight:500;color:#000'>" + Email + "</p>");
            //w.WriteLine("<p style='font-weight:500;color:#000'>" + WebSite + "</p>");
            //w.WriteLine("</div>");
            //w.WriteLine("<div class='col-sm-2'>");
            //w.WriteLine("<img src='" + RightLogo + "' alt='lonestar-image' height='90px'>");
            //w.WriteLine("</div></div>");
            //w.WriteLine("</div></div></div>");
            //w.WriteLine("</td></tr></thead>");
            //w.WriteLine("<tbody><tr><td>");
            //w.WriteLine("<div class='col-sm-12 padding:0' style='padding:10px;'>");
            //w.WriteLine(div);
            //w.WriteLine("</div>");
            //w.WriteLine("</td></tr></tbody>");
            //w.WriteLine("<tfoot><tr><td>");
            //w.WriteLine("</td></tr></tfoot></table>");
            //w.WriteLine("</div>");
            //w.WriteLine("</html>");

            //w.Flush();
            //w.Close();
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