﻿using System;
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
using System.IO;
using System.Configuration;

public partial class Pages_EnquiryReviewCheckList : System.Web.UI.Page
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
                objSales.GetOfferTypeDetails(ddlOfferType);
                GetEnquiryReviewCheckListDetails();
                CompletionStatus();
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

    #region"Radio Events"

    protected void rblEnquiryChange_OnSelectedChanged(object sender, EventArgs e)
    {
        try
        {
            GetEnquiryReviewCheckListDetails();
            CompletionStatus();
            ShowHideControls("view");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string AttachmentName = null;
        try
        {
            if (ValidateFields())
            {
                DataSet ds = new DataSet();
                objSales.ClarifyID = Convert.ToInt32(hdnClarifyID.Value);
                objSales.CustomerID = Convert.ToInt32(ViewState["ProspectID"]);
                objSales.EnquiryID = Convert.ToInt32(ViewState["EnquiryNumber"]);
                objSales.DaedlineDate = lblDeadLineDate.Text;
                objSales.Material = rblMaterial.SelectedValue;
                objSales.MaterialRemarks = txtMaterialRemarks.Text;
                objSales.Pressure = rblPressure.SelectedValue;
                objSales.PressureRemarks = txtPressureRemarks.Text;
                objSales.Temprature = rblTemparature.SelectedValue;
                objSales.TempratureRemarks = txtTemparatureRemarks.Text;
                objSales.Movements = rblMovements.SelectedValue;
                objSales.MovementsRemarks = txtMovementsremarks.Text;
                objSales.Connection = rblConnection.SelectedValue;
                objSales.ConnectionRemarks = txtConnectionDetailsRemarks.Text;
                objSales.OverAllLength = rblLength.SelectedValue;
                objSales.OverAllLengthRemarks = txtLengthRemarks.Text;
                objSales.FlowMedium = rblFlowMedium.SelectedValue;
                objSales.FlowMediumRemarks = txtFlowMediumRemarks.Text;
                objSales.Application = rblApplication.SelectedValue;
                objSales.ApplicationRemarks = txtApplicationRemarks.Text;
                objSales.PipingLayout = rblPipingLayout.SelectedValue;
                objSales.PipingLayoutRemarks = txtPipingLayout.Text;
                objSales.Painting = rblPaintingRequirtments.SelectedValue;
                objSales.PaintingRemarks = txtPaintingRemarks.Text;
                objSales.Statuary = rblStatutory.SelectedValue;
                objSales.StatuaryRemarks = txtStatutoryDetails.Text;
                objSales.InspectionAndTesting = rblInspectionAndTesting.SelectedValue;
                objSales.InspectionAndTestingRemarks = txtInspectionAndTestingRemarks.Text;
                objSales.Clarrification = rblClarrification.SelectedValue;
                objSales.ClarrificationRemarks = txtClarrificationRemarks.Text;
                objSales.Stamping = rblStampingRequirtments.SelectedValue;
                objSales.StampingRemarks = txtStampingRemarks.Text;
                objSales.AddtionalNote = txtAddtionalNote.Text;
                //objSales.BudgetaryEnquiry = rblBudgetary.SelectedValue;

                objSales.SizeOfEJ = rblSizeOfExpansionJoints.SelectedValue;
                objSales.SizeOfEJRemarks = txtSizeOfExpansionjoints.Text;

                objSales.TypeOfEJ = rblTypeOfExpansionJoints.SelectedValue;
                objSales.TypeOfEJRemarks = txtTypeOfExpansionjoints.Text;

                objSales.OfferTypeID = Convert.ToInt32(ddlOfferType.SelectedValue);

                if (fAttachment.HasFile)
                {
                    _objc = new cCommon();
                    string MaxAttachementId = objSales.GetMaximumAttachementID();
                    string extn = Path.GetExtension(fAttachment.PostedFile.FileName).ToUpper();
                    AttachmentName = Path.GetFileName(fAttachment.PostedFile.FileName);
                    string[] extension = AttachmentName.Split('.');

                    AttachmentName = extension[0] + '_' + MaxAttachementId + '.' + extension[1];

                    _objc.Foldername = CusstomerEnquirySavePath;
                    _objc.PID = ViewState["EnquiryNumber"].ToString();
                    _objc.FileName = AttachmentName;
                    _objc.AttachementControl = fAttachment;

                    _objc.SaveFiles();
                }
                objSales.AttachementName = AttachmentName;
                CompletionStatus();

                objSales.completionStatus = "Completed";


                objSales.UserID = Convert.ToInt32(_objSession.employeeid);

                ds = objSales.SaveEnquiryReviewClarrification();

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

            objSales.Type = "EnquiryCheckList";

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
            string ClarifyID = gvEnquiryReviewCheckList.DataKeys[index].Values[0].ToString();

            DataTable dt = (DataTable)ViewState["EnquiryReviewCheckList"];
            dt.DefaultView.RowFilter = "ClarifyID='" + ClarifyID + "'";

            if (e.CommandName == "CheckListPDF")
            {
                int EnquiryID = Convert.ToInt32(dt.DefaultView.ToTable().Rows[0]["EnquiryID"].ToString());

                string EnuiryDate = dt.DefaultView.ToTable().Rows[0]["EnquiryDate"].ToString();
                lblNameOftheCustomer_P.Text = dt.DefaultView.ToTable().Rows[0]["ProspectName"].ToString();
                lblEnquiryNumberAndDate_P.Text = dt.DefaultView.ToTable().Rows[0]["CustomerEnquiryNumber"].ToString() + "&" + EnuiryDate;
                lblCustDeadLineDate_P.Text = dt.DefaultView.ToTable().Rows[0]["DaedlineDate"].ToString();

                lblSizeOfEJ_P.Text = dt.DefaultView.ToTable().Rows[0]["SizeOfEJ"].ToString() == "Y" ? "Yes" : "No"; ;
                lblTypeofEJ_P.Text = dt.DefaultView.ToTable().Rows[0]["TypeOfEJ"].ToString() == "Y" ? "Yes" : "No"; ;

                lblMaterial_P.Text = dt.DefaultView.ToTable().Rows[0]["Material"].ToString() == "Y" ? "Yes" : "No"; ;

                lblPressure_P.Text = dt.DefaultView.ToTable().Rows[0]["Pressure"].ToString() == "Y" ? "Yes" : "No";

                lblTemparatue_P.Text = dt.DefaultView.ToTable().Rows[0]["Temprature"].ToString() == "Y" ? "Yes" : "No";

                lblOverAllLength_P.Text = dt.DefaultView.ToTable().Rows[0]["OverAllLength"].ToString() == "Y" ? "Yes" : "No";

                lblMovements_P.Text = dt.DefaultView.ToTable().Rows[0]["Movements"].ToString() == "Y" ? "Yes" : "No";

                lblFlowMedium_P.Text = dt.DefaultView.ToTable().Rows[0]["FlowMedium"].ToString() == "Y" ? "Yes" : "No";

                lblPipingLayout_P.Text = dt.DefaultView.ToTable().Rows[0]["PipingLayout"].ToString() == "Y" ? "Yes" : "No";

                lblPaintingRequirtments_P.Text = dt.DefaultView.ToTable().Rows[0]["Painting"].ToString() == "Y" ? "Yes" : "No";

                lblInspectionAndTesting_P.Text = dt.DefaultView.ToTable().Rows[0]["InspectionAndTesting"].ToString() == "Y" ? "Yes" : "No";

                lblClarrificationRequired_P.Text = dt.DefaultView.ToTable().Rows[0]["Clarrification"].ToString() == "Y" ? "Yes" : "No";

                lblStampingRequirtments_P.Text = dt.DefaultView.ToTable().Rows[0]["Stamping"].ToString() == "Y" ? "Yes" : "No";

                lblStatutaryDetailsSpecified_P.Text = dt.DefaultView.ToTable().Rows[0]["Statuary"].ToString() == "Y" ? "Yes" : "No";

                lblEndConnectionDetails_P.Text = dt.DefaultView.ToTable().Rows[0]["Connection"].ToString() == "Y" ? "Yes" : "No";

                lblAddtionalNote_P.Text = dt.DefaultView.ToTable().Rows[0]["AddtionalNote"].ToString();

                ViewState["EnquiryID"] = EnquiryID.ToString();
                GeneratePDFFile();
            }

            if (e.CommandName == "ViewAttachement")
            {
                string FileName = gvEnquiryReviewCheckList.DataKeys[index].Values[1].ToString();
                string Savepath = CusstomerEnquirySavePath;
                string httpPath = CustomerEnquiryHttpPath;
                string EnquiryID = dt.DefaultView.ToTable().Rows[0]["EnquiryID"].ToString();
                _objc.ViewFileName(Savepath, httpPath, FileName, EnquiryID, ifrm);
            }

            if (e.CommandName == "EditEnquiryReviewCheckList")
            {
                BindEnquiryReviewCheckList(ClarifyID);
                CompletionStatus();
                ShowHideControls("edit");
            }
            else if (e.CommandName == "ViewEnquiryReviewCheckList")
            {

                int EnquiryID = Convert.ToInt32(dt.DefaultView.ToTable().Rows[0]["EnquiryID"].ToString());

                lblCustomerName_V.Text = dt.DefaultView.ToTable().Rows[0]["ProspectName"].ToString();
                lblEnquiryNumber_V.Text = EnquiryID.ToString();
                lblCustomerEnquiryNumber_V.Text = dt.DefaultView.ToTable().Rows[0]["CustomerEnquiryNumber"].ToString();
                lblDeadlineDate_V.Text = dt.DefaultView.ToTable().Rows[0]["DaedlineDate"].ToString();
                lblReceivedDate_V.Text = dt.DefaultView.ToTable().Rows[0]["ReceivedDate"].ToString();

                lblMaterial_V.Text = dt.DefaultView.ToTable().Rows[0]["Material"].ToString() == "Y" ? "Yes" : "No"; ;
                lblMaterial_V.ForeColor = lblMaterial_V.Text == "Yes" ? Color.Green : Color.Red;
                lblMaterialRemarks_V.Text = dt.DefaultView.ToTable().Rows[0]["MaterialRemarks"].ToString();

                lblPressure_V.Text = dt.DefaultView.ToTable().Rows[0]["Pressure"].ToString() == "Y" ? "Yes" : "No";
                lblPressure_V.ForeColor = lblPressure_V.Text == "Yes" ? Color.Green : Color.Red;
                lblPressureRemarks_V.Text = dt.DefaultView.ToTable().Rows[0]["PressureRemarks"].ToString();


                lblTemprature_V.Text = dt.DefaultView.ToTable().Rows[0]["Temprature"].ToString() == "Y" ? "Yes" : "No";
                lblTemprature_V.ForeColor = lblTemprature_V.Text == "Yes" ? Color.Green : Color.Red;
                lblTempratureRemarks_V.Text = dt.DefaultView.ToTable().Rows[0]["TempratureRemarks"].ToString();

                lblLength_V.Text = dt.DefaultView.ToTable().Rows[0]["OverAllLength"].ToString() == "Y" ? "Yes" : "No";
                lblLength_V.ForeColor = lblLength_V.Text == "Yes" ? Color.Green : Color.Red;
                lblLengthRemarks_V.Text = dt.DefaultView.ToTable().Rows[0]["OverAllLengthRemarks"].ToString();


                lblMovements_V.Text = dt.DefaultView.ToTable().Rows[0]["Movements"].ToString() == "Y" ? "Yes" : "No";
                lblMovements_V.ForeColor = lblMovements_V.Text == "Yes" ? Color.Green : Color.Red;
                lblMovementsRemarks_V.Text = dt.DefaultView.ToTable().Rows[0]["MovementsRemarks"].ToString();

                lblFlowMedium_V.Text = dt.DefaultView.ToTable().Rows[0]["FlowMedium"].ToString() == "Y" ? "Yes" : "No";
                lblFlowMedium_V.ForeColor = lblFlowMedium_V.Text == "Yes" ? Color.Green : Color.Red;
                lblFlowMediumRemarks_V.Text = dt.DefaultView.ToTable().Rows[0]["FlowMediumRemarks"].ToString();

                lblApplication_V.Text = dt.DefaultView.ToTable().Rows[0]["Application"].ToString() == "Y" ? "Yes" : "No";
                lblApplication_V.ForeColor = lblApplication_V.Text == "Yes" ? Color.Green : Color.Red;
                lblApplicationRemarks_V.Text = dt.DefaultView.ToTable().Rows[0]["ApplicationRemarks"].ToString();

                lblPipingLayout_V.Text = dt.DefaultView.ToTable().Rows[0]["PipingLayout"].ToString() == "Y" ? "Yes" : "No";
                lblPipingLayout_V.ForeColor = lblPipingLayout_V.Text == "Yes" ? Color.Green : Color.Red;
                lblPipingLayoutRemarks_V.Text = dt.DefaultView.ToTable().Rows[0]["PipingLayoutRemarks"].ToString();


                lblPainting_V.Text = dt.DefaultView.ToTable().Rows[0]["Painting"].ToString() == "Y" ? "Yes" : "No";
                lblPainting_V.ForeColor = lblPainting_V.Text == "Yes" ? Color.Green : Color.Red;
                lblPaintingRemarks_V.Text = dt.DefaultView.ToTable().Rows[0]["PaintingRemarks"].ToString();


                lblInspectionAndTesting_V.Text = dt.DefaultView.ToTable().Rows[0]["InspectionAndTesting"].ToString() == "Y" ? "Yes" : "No";
                lblInspectionAndTesting_V.ForeColor = lblInspectionAndTesting_V.Text == "Yes" ? Color.Green : Color.Red;
                lblInspectionAndTestingRemarks_V.Text = dt.DefaultView.ToTable().Rows[0]["InspectionAndTestingRemarks"].ToString();


                lblClarrification_V.Text = dt.DefaultView.ToTable().Rows[0]["Clarrification"].ToString() == "Y" ? "Yes" : "No";
                lblClarrification_V.ForeColor = lblClarrification_V.Text == "Yes" ? Color.Green : Color.Red;
                lblClarrificationRemarks_V.Text = dt.DefaultView.ToTable().Rows[0]["ClarrificationRemarks"].ToString();


                lblStamping_V.Text = dt.DefaultView.ToTable().Rows[0]["Stamping"].ToString() == "Y" ? "Yes" : "No";
                lblStamping_V.ForeColor = lblStamping_V.Text == "Yes" ? Color.Green : Color.Red;
                lblStampingRemarks_V.Text = dt.DefaultView.ToTable().Rows[0]["StampingRemarks"].ToString();



                lblSizeOfEJ_V.Text = dt.DefaultView.ToTable().Rows[0]["SizeOfEJ"].ToString() == "Y" ? "Yes" : "No";
                lblSizeOfEJ_V.ForeColor = lblSizeOfEJ_V.Text == "Yes" ? Color.Green : Color.Red;
                lblSizeOfEJRemarks_V.Text = dt.DefaultView.ToTable().Rows[0]["SizeOfEJRemarks"].ToString();

                lblTypeOfEJ_V.Text = dt.DefaultView.ToTable().Rows[0]["TypeOfEJ"].ToString() == "Y" ? "Yes" : "No";
                lblTypeOfEJ_V.ForeColor = lblTypeOfEJ_V.Text == "Yes" ? Color.Green : Color.Red;
                lblTypeOfEJRemarks_V.Text = dt.DefaultView.ToTable().Rows[0]["TypeOfEJRemarks"].ToString();


                lblStatutory_V.Text = dt.DefaultView.ToTable().Rows[0]["Statuary"].ToString() == "Y" ? "Yes" : "No";
                lblStatutory_V.ForeColor = lblStatutory_V.Text == "Yes" ? Color.Green : Color.Red;
                lblStatutoryRemarks_V.Text = dt.DefaultView.ToTable().Rows[0]["StatuaryRemarks"].ToString();


                lblConnection_V.Text = dt.DefaultView.ToTable().Rows[0]["Connection"].ToString() == "Y" ? "Yes" : "No";
                lblConnection_V.ForeColor = lblConnection_V.Text == "Yes" ? Color.Green : Color.Red;
                lblConnectionRemarks_V.Text = dt.DefaultView.ToTable().Rows[0]["ConnectionRemarks"].ToString();

                lblAddtionalNote_v.Text = dt.DefaultView.ToTable().Rows[0]["AddtionalNote"].ToString();

                lblOfferType_V.Text = dt.DefaultView.ToTable().Rows[0]["OfferType"].ToString();

                //try
                //{
                //    if (dt.DefaultView.ToTable().Rows[0]["Budgetary"].ToString() == "1") lblAddtionalBudgetary_v.Text = "Yes";
                //    else if (dt.DefaultView.ToTable().Rows[0]["Budgetary"].ToString() == "0") lblAddtionalBudgetary_v.Text = "No";
                //}
                //catch (Exception ec) { }

                gvEnquiryReviewCheckList.UseAccessibleHeader = true;
                gvEnquiryReviewCheckList.HeaderRow.TableSection = TableRowSection.TableHeader;

                ds = objSales.GetEmployeeListForEnquiryCommunication(EnquiryID, ddlReceiverGroup);

                ViewState["EmployeeListBySendCommunication"] = ds.Tables[0];
                ViewState["EnquiryNumber"] = EnquiryID;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "ShowEnquiryCheckListViewPopUp();showDataTable();", true);
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

            if (_objSession.type == 4)
            {
                e.Row.Cells[7].Enabled = false;
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (dr["CompletionStatus"].ToString() == "Incomplete")
                    e.Row.Attributes.Add("style", "Background-color:#ffd400");
                if (dr["Budgetaryoffer"].ToString() == "1")
                {
                    Label lbl = new Label();
                    lbl.Text = "Budgetary";
                    lbl.Attributes.Add("class", "blinking budgetaryhighligt");
                    e.Row.Cells[0].Controls.Add(lbl);
                }

            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void GeneratePDFFile()
    {
        try
        {
            string MAXPDFID = "";
            DataSet ds = new DataSet();
            var sb = new StringBuilder();
            ////divPrintReceiptDetails.RenderControl(new HtmlTextWriter(new StringWriter(sb)));

            _objc = new cCommon();
            _objc.ISORecordsCodeNo = "ERC";
            ds = _objc.GetISODocNoDetails();
            //ControlNo,,
            lblDocNo_P.Text = ds.Tables[0].Rows[0]["ControlNo"].ToString();
            lblRevNo_P.Text = ds.Tables[0].Rows[0]["RevNo"].ToString();
            lblRevDate_P.Text = ds.Tables[0].Rows[0]["RevDate"].ToString();

            cQRcode objQr = new cQRcode();

            string QrNumber = objQr.generateQRNumber(9);
            string displayQrnumber = objQr.getDisplayQRNumber(QrNumber, 3, '-');

            string Code = displayQrnumber + "/" + _objSession.employeeid + "/" + DateTime.Now.ToString();

            string QrCode = objQr.QRcodeGeneration(Code);
            if (QrCode != "")
            {
                imgQrcode.Attributes.Add("style", "display:block;");
                imgQrcode.ImageUrl = QrCode;
                objQr.QRNumber = displayQrnumber;
                objQr.fileName = "EnquiryReviewCheckList";
                objQr.createdBy = _objSession.employeeid;
                string pageName = Path.GetFileName(Page.AppRelativeVirtualPath);
                objQr.Pagename = pageName;
                objQr.saveQRNumber();
            }
            else
                imgQrcode.Attributes.Add("style", "display:none;");

            divCheckListPrint.Attributes.Add("style", "display:block;");

            divCheckListPrint.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            string div = sb.ToString();

            MAXPDFID = _objc.GetMaximumNumberPDF();
            string htmlfile = "EnquiryCheckList" + "_" + ViewState["EnquiryID"].ToString() + ".html";
            string pdffile = "EnquiryCheckList" + "_" + ViewState["EnquiryID"].ToString() + ".pdf";
            string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
            string strFile = LetterPath + pdffile;

            string URL = LetterPath + htmlfile;
            //    URL = URL.ToLower();
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();
            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 2].ToString() + "/" + pagename[pagename.Length - 1].ToString();

            string epstyleurl = url.Replace(Replacevalue, "Assets/css/ep-style.css");
            string style = url.Replace(Replacevalue, "Assets/css/style.css");
            string Main = url.Replace(Replacevalue, "Assets/css/main.css");
            string Print = url.Replace(Replacevalue, "Assets/css/print.css");
            string topstrip = url.Replace(Replacevalue, "Assets/images/topstrrip.jpg");

            _objc.url = url;
            _objc.epstyleurl = epstyleurl;
            _objc.style = style;
            _objc.Main = Main;
            _objc.Print = Print;
            _objc.topstrip = topstrip;

            SaveHtmlFile(URL, "Enquiry Check List Details", lbltitle.Text, div);

            //_objc.GenerateAndSavePDF(LetterPath, strFile, pdffile, URL);

            divCheckListPrint.Attributes.Add("style", "display:none;");

           // _objc.SavePDFFile("EnquiryReviewCheckList.aspx", pdffile, _objSession.employeeid);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void SaveHtmlFile(string URL, string HeaderTitle, string lbltitle, string div)
    {
        try
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Function", "PrintEnquiryReviewCheckList();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private bool ValidateFields()
    {
        bool isValid = true;
        int Flag = 0;
        try
        {
            //   List<int,string> dct=new Dictionary<int,string>;
            string error = "";

            foreach (Control c in divInput.Controls)
            {
                if (c is RadioButtonList)
                {
                    RadioButtonList rbl = (RadioButtonList)c;

                    if (rbl.SelectedValue == "N")
                    {
                        Flag = 1;
                    }
                }
            }

            if (Flag == 1)
            {
                if (rblYesNoAll.SelectedValue == "N")
                {
                    if (txtMaterialRemarks.Text == "")
                    {
                        error = "Material Remarks\n";
                        isValid = false;
                    }
                    if (txtPressureRemarks.Text == "")
                    {
                        error = error + "Pressure Remarks\n";
                        isValid = false;
                    }
                    if (txtMovementsremarks.Text == "")
                    {
                        error = error + "Movement Remarks\n";
                        isValid = false;
                    }
                    if (txtTemparatureRemarks.Text == "")
                    {
                        error = error + "Temparature Remarks\n";
                        isValid = false;
                    }
                    if (txtLengthRemarks.Text == "")
                    {
                        error = error + "Length Remarks\n";
                        isValid = false;
                    }
                    if (txtConnectionDetailsRemarks.Text == "")
                    {
                        error = error + "Connection Remarks\n";
                        isValid = false;
                    }
                    if (txtFlowMediumRemarks.Text == "")
                    {
                        error = error + "Flow Medium Remarks\n";
                        isValid = false;
                    }
                    if (txtApplicationRemarks.Text == "")
                    {
                        error = error + "Application Remarks\n";
                        isValid = false;
                    }
                    if (txtPipingLayout.Text == "")
                    {
                        error = error + "PipingLayoutRemarks\n";
                        isValid = false;
                    }
                    if (txtPaintingRemarks.Text == "")
                    {
                        error = error + "Painting Remarks\n";
                        isValid = false;
                    }
                    if (txtStatutoryDetails.Text == "")
                    {
                        error = error + "Statutory Details\n";
                        isValid = false;
                    }
                    if (txtInspectionAndTestingRemarks.Text == "")
                    {
                        error = error + "Inspection And Testing Remarks\n";
                        isValid = false;
                    }
                    if (txtClarrificationRemarks.Text == "")
                    {
                        error = error + "Clarrification Remarks\n";
                        isValid = false;
                    }
                    if (txtStampingRemarks.Text == "")
                    {
                        error = error + "Stamping Remarks\n";
                        isValid = false;
                    }

                    gvEnquiryReviewCheckList.UseAccessibleHeader = true;
                    gvEnquiryReviewCheckList.HeaderRow.TableSection = TableRowSection.TableHeader;

                    if (isValid == false)
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "ErrorMessage('Error','Field Required " + error + "');showDataTable();", true);
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return isValid;
    }

    private void BindEnquiryReviewCheckList(string ClarifyID)
    {
        try
        {
            DataTable dt = (DataTable)ViewState["EnquiryReviewCheckList"];

            dt.DefaultView.RowFilter = "ClarifyID='" + ClarifyID + "'";

            ViewState["ProspectID"] = dt.DefaultView.ToTable().Rows[0]["CustomerID"].ToString();
            ViewState["EnquiryNumber"] = dt.DefaultView.ToTable().Rows[0]["EnquiryID"].ToString();
            hdnClarifyID.Value = ClarifyID.ToString();

            lblCustomerName.Text = dt.DefaultView.ToTable().Rows[0]["ProspectName"].ToString();
            lblEnquiryNumber.Text = dt.DefaultView.ToTable().Rows[0]["EnquiryID"].ToString();
            lblCustomerEnquiryNumber.Text = dt.DefaultView.ToTable().Rows[0]["CustomerEnquiryNumber"].ToString();
            lblDeadLineDate.Text = dt.DefaultView.ToTable().Rows[0]["DaedlineDate"].ToString();
            lblReceivedDate_D.Text = dt.DefaultView.ToTable().Rows[0]["ReceivedDate"].ToString();

            rblMaterial.SelectedValue = dt.DefaultView.ToTable().Rows[0]["Material"].ToString();

            rblSizeOfExpansionJoints.SelectedValue = dt.DefaultView.ToTable().Rows[0]["SizeOfEJ"].ToString();
            txtSizeOfExpansionjoints.Text = dt.DefaultView.ToTable().Rows[0]["SizeOfEJRemarks"].ToString();

            rblTypeOfExpansionJoints.SelectedValue = dt.DefaultView.ToTable().Rows[0]["TypeOfEJ"].ToString();
            txtTypeOfExpansionjoints.Text = dt.DefaultView.ToTable().Rows[0]["TypeOfEJRemarks"].ToString();

            txtMaterialRemarks.Text = dt.DefaultView.ToTable().Rows[0]["MaterialRemarks"].ToString();
            rblPressure.SelectedValue = dt.DefaultView.ToTable().Rows[0]["Pressure"].ToString();
            txtPressureRemarks.Text = dt.DefaultView.ToTable().Rows[0]["PressureRemarks"].ToString();
            rblTemparature.SelectedValue = dt.DefaultView.ToTable().Rows[0]["Temprature"].ToString();
            txtTemparatureRemarks.Text = dt.DefaultView.ToTable().Rows[0]["TempratureRemarks"].ToString();
            rblLength.SelectedValue = dt.DefaultView.ToTable().Rows[0]["OverAllLength"].ToString();
            txtLengthRemarks.Text = dt.DefaultView.ToTable().Rows[0]["OverAllLengthRemarks"].ToString();
            rblMovements.SelectedValue = dt.DefaultView.ToTable().Rows[0]["Movements"].ToString();
            txtMovementsremarks.Text = dt.DefaultView.ToTable().Rows[0]["MovementsRemarks"].ToString();
            rblFlowMedium.SelectedValue = dt.DefaultView.ToTable().Rows[0]["FlowMedium"].ToString();
            txtFlowMediumRemarks.Text = dt.DefaultView.ToTable().Rows[0]["FlowMediumRemarks"].ToString();
            rblApplication.SelectedValue = dt.DefaultView.ToTable().Rows[0]["Application"].ToString();
            txtApplicationRemarks.Text = dt.DefaultView.ToTable().Rows[0]["ApplicationRemarks"].ToString();

            rblPipingLayout.SelectedValue = dt.DefaultView.ToTable().Rows[0]["PipingLayout"].ToString();
            txtPipingLayout.Text = dt.DefaultView.ToTable().Rows[0]["PipingLayoutRemarks"].ToString();
            rblPaintingRequirtments.SelectedValue = dt.DefaultView.ToTable().Rows[0]["Painting"].ToString();
            txtPaintingRemarks.Text = dt.DefaultView.ToTable().Rows[0]["PaintingRemarks"].ToString();
            rblInspectionAndTesting.SelectedValue = dt.DefaultView.ToTable().Rows[0]["InspectionAndTesting"].ToString();
            txtInspectionAndTestingRemarks.Text = dt.DefaultView.ToTable().Rows[0]["InspectionAndTestingRemarks"].ToString();
            rblClarrification.SelectedValue = dt.DefaultView.ToTable().Rows[0]["Clarrification"].ToString();
            txtClarrificationRemarks.Text = dt.DefaultView.ToTable().Rows[0]["ClarrificationRemarks"].ToString();
            rblStampingRequirtments.SelectedValue = dt.DefaultView.ToTable().Rows[0]["Stamping"].ToString();
            txtStampingRemarks.Text = dt.DefaultView.ToTable().Rows[0]["StampingRemarks"].ToString();

            rblStatutory.SelectedValue = dt.DefaultView.ToTable().Rows[0]["Statuary"].ToString();
            txtStatutoryDetails.Text = dt.DefaultView.ToTable().Rows[0]["StatuaryRemarks"].ToString();
            rblConnection.SelectedValue = dt.DefaultView.ToTable().Rows[0]["Connection"].ToString();
            txtConnectionDetailsRemarks.Text = dt.DefaultView.ToTable().Rows[0]["ConnectionRemarks"].ToString();
            txtAddtionalNote.Text = dt.DefaultView.ToTable().Rows[0]["AddtionalNote"].ToString();
            ddlOfferType.SelectedValue = dt.DefaultView.ToTable().Rows[0]["OfferTypeID"].ToString();

            //   rblBudgetary.SelectedValue = dt.DefaultView.ToTable().Rows[0]["Budgetary"].ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "fieldreqadd();", true);
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

            lblCustomerName.Text = "";
            lblEnquiryNumber.Text = "";
            lblCustomerEnquiryNumber.Text = "";
            lblDeadLineDate.Text = "";
            lblReceivedDate_D.Text = "";

            rblMaterial.SelectedValue = "N";

            txtMaterialRemarks.Text = "";
            rblPressure.SelectedValue = "N";
            txtPressureRemarks.Text = "";
            rblTemparature.SelectedValue = "N";
            txtTemparatureRemarks.Text = "";
            rblLength.SelectedValue = "N";
            txtLengthRemarks.Text = "";
            rblMovements.SelectedValue = "N";
            txtMovementsremarks.Text = "";
            rblFlowMedium.SelectedValue = "N";
            txtFlowMediumRemarks.Text = "";
            rblApplication.SelectedValue = "N";
            txtApplicationRemarks.Text = "";

            rblPipingLayout.SelectedValue = "N";
            txtPipingLayout.Text = "";
            rblPaintingRequirtments.SelectedValue = "N";
            txtPaintingRemarks.Text = "";
            rblInspectionAndTesting.SelectedValue = "N";
            txtInspectionAndTestingRemarks.Text = "";
            rblClarrification.SelectedValue = "N";
            txtClarrificationRemarks.Text = "";
            rblStampingRequirtments.SelectedValue = "N";
            txtStampingRemarks.Text = "";

            rblStatutory.SelectedValue = "N";
            txtStatutoryDetails.Text = "";
            rblConnection.SelectedValue = "N";
            txtConnectionDetailsRemarks.Text = "";
            txtAddtionalNote.Text = "";
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

            objSales.CurrentStatus = rblEnquiryChange.SelectedValue;
            dsEnquiryReviewCheckList = objSales.GetEnquiryReviewCheckListDetails();

            ViewState["EnquiryReviewCheckList"] = dsEnquiryReviewCheckList.Tables[0];

            ViewState["UserTypeID"] = dsEnquiryReviewCheckList.Tables[1].Rows[0]["UserTypeID"].ToString();

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

    private void CompletionStatus()
    {
        try
        {
            if (rblSizeOfExpansionJoints.SelectedValue == "N" && rblMaterial.SelectedValue == "N" && rblMovements.SelectedValue == "N" && rblPressure.SelectedValue == "N" &&
                rblTemparature.SelectedValue == "N" && rblConnection.SelectedValue == "N" && rblLength.SelectedValue == "N" &&
                rblFlowMedium.SelectedValue == "N" && rblApplication.SelectedValue == "N" && rblPipingLayout.SelectedValue == "N" &&
                rblPaintingRequirtments.SelectedValue == "N" && rblStatutory.SelectedValue == "N" && rblInspectionAndTesting.SelectedValue == "N" &&
                rblClarrification.SelectedValue == "N" && rblStampingRequirtments.SelectedValue == "N")
            {
                lblCompletionStatus.Text = "Incomplete";
                lblCompletionStatus.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                lblCompletionStatus.Text = "Completed";
                lblCompletionStatus.ForeColor = System.Drawing.Color.Green;
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
                    txtMaterialRemarks.Focus();
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

}