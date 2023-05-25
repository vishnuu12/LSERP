using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Drawing;

public partial class Pages_ViewMessage : System.Web.UI.Page
{

    #region"Declaration"

    cSession _objSession = new cSession();
    cSales objSales;
    EmailAndSmsAlerts objAlerts;

    #endregion

    #region "PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        _objSession = Master.csSession;
    }

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        objSales = new cSales();
        try
        {

            if (IsPostBack == false)
                objSales.GetEnquiryCustOrderNumber(ddlEnquiry_Customer_OrderNumber);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlEnquiry_Customer_OrderNumber_SelectedIndexChanged(object sender, EventArgs e)
    {
        objSales = new cSales();
        DataSet ds = new DataSet();
        StringBuilder sbDivReceiver = new StringBuilder();
        StringBuilder sbDivSender = new StringBuilder();
        try
        {
            if (ddlEnquiry_Customer_OrderNumber.SelectedIndex > 0)
            {
                DivReceiver.Visible = true;
                objSales.EnquiryNumber = ddlEnquiry_Customer_OrderNumber.SelectedValue;
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

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Function", "hideLoader();", true);
                }

                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Function", "InfoMessage('Information','No Message');hideLoader();", true);
            }
            else
            {
                DivReceiver.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Function", "hideLoader();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnReplyMessage_Click(object sender, EventArgs e)
    {
        DataTable dt;
        DataSet ds = new DataSet();
        objAlerts = new EmailAndSmsAlerts();
        objSales = new cSales();
        string Message = "";
        try
        {
            if (ValidateField())
            {
                int ECID = Convert.ToInt32(hdnECID.Value);
                dt = (DataTable)ViewState["EnquiryCommunicationDetails"];
                DataView dv = new DataView(dt);
                dv.RowFilter = "ECID='" + ECID + "'";
                dt = dv.ToTable();
                string Email = dt.Rows[0]["Email"].ToString();
                objSales.Message = txtMessage.Text;
                if (Email != "")
                {
                    objAlerts.file = "";
                    objAlerts.dtSettings = objAlerts.GetEmailSettings();
                    objAlerts.EmailID = dt.Rows[0]["Email"].ToString();// "karthik@innovasphere.in";
                    objAlerts.Subject = txtHeader.Text;
                    objAlerts.Message = txtMessage.Text;
                    objAlerts.userID = _objSession.employeeid;
                    objAlerts.SendIndividualMail();
                    Message = "Mail Dispatched";
                }
                else
                {
                    Message = "Mail Not Sending";
                }

                //save Message Details
                objSales.ECID = ECID;
                objSales.Header = txtHeader.Text;
                objSales.Message = txtMessage.Text;

                ds = objSales.SaveEnquiryCommunicationReplyMessage();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Replied")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Function", "SuccessMessage('Success','Mail Dispatched Successfully " + Message + "');ShowClosePopUp();hideLoader();", true);

                ClearMessageValues();
                ddlEnquiry_Customer_OrderNumber_SelectedIndexChanged(null, null);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        finally
        {
            dt = null;
            ds = null;
            objAlerts = null;
            objSales = null;
        }
    }

    #endregion

    #region "Common Methods"

    private bool ValidateField()
    {
        bool isvalid = true;
        string error = "";
        if (txtHeader.Text == "")
            error = txtHeader.ClientID + "/" + "Field Required";
        else if (txtMessage.Text == "")
            error = txtMessage.ClientID + "/" + "Field Required";

        if (error != "")
        {
            isvalid = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Function", "ValidateControl('" + error + "');", true);
        }

        return isvalid;

    }

    private void ClearMessageValues()
    {
        txtHeader.Text = "";
        txtMessage.Text = "";
    }

    #endregion

}