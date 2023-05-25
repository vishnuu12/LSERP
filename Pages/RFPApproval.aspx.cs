using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using eplus.core;
using System.Configuration;
using System.IO;
using System.Text;

public partial class Pages_RFPApproval : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cCommon objc = new cCommon();
    cDesign objDesign;
    cSales objSales;
    c_HR objHR;
    EmailAndSmsAlerts objAlerts;
    string CusstomerEnquirySavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString();
    string CustomerEnquiryHttpPath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString();

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
                ddlRFPCustomerNameLoad();
                ShowHideControls("add");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"radio Events"

    protected void rblRFPChange_OnSelectedChanged(object sender, EventArgs e)
    {
        ddlRFPCustomerNameLoad();
        ShowHideControls("add");
    }

    #endregion

    #region "DropDown Events"

    protected void ddlCustomerName_SelectIndexChanged(object sender, EventArgs e)
    {
        DataTable dt;
        try
        {
            dt = (DataTable)ViewState["PONoDetails"];
            if (ddlCustomerName.SelectedIndex > 0)
            {
                string ProspectID = ddlCustomerName.SelectedValue;
                dt.DefaultView.RowFilter = "ProspectID='" + ProspectID + "'";
                dt.DefaultView.ToTable();
            }

            ddlCustomerPO.DataSource = dt;
            ddlCustomerPO.DataTextField = "PORefNo";
            ddlCustomerPO.DataValueField = "POHID";
            ddlCustomerPO.DataBind();
            ddlCustomerPO.Items.Insert(0, new ListItem("--Select--", "0"));

            ShowHideControls("add");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlCustomerPO_SelectIndexChanged(object sender, EventArgs e)
    {
        objDesign = new cDesign();
        try
        {
            if (ddlCustomerPO.SelectedIndex > 0)
            {
                objDesign.POHID = Convert.ToInt32(ddlCustomerPO.SelectedValue);
                string ProspectID = objDesign.GetProspectNameByPOHID();
                ddlCustomerName.SelectedValue = ProspectID;
                BindRFPDetails();
                ShowHideControls("view");
            }
            else
            {
                ShowHideControls("add");
                ddlCustomerName.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btndownloaddocs_Click(object sender, EventArgs e)
    {
        try
        {
            cCommon.DownLoad(ViewState["FileName"].ToString(), ViewState["ifrmsrc"].ToString());
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        DataSet dsApprove = new DataSet();
        objDesign = new cDesign();
        try
        {
            bool itemselected = false;
            foreach (GridViewRow row in gvRFPHeader.Rows)
            {
                CheckBox chkditems = (CheckBox)row.FindControl("chkitems");
                if (chkditems.Checked)
                {
                    itemselected = true;
                    objDesign.RFPHID = Convert.ToInt32(gvRFPHeader.DataKeys[row.RowIndex].Values[0].ToString());
                    objDesign.StatusFlag = "Approve";
                    objDesign.UserID = Convert.ToInt32(objSession.employeeid);
                    dsApprove = objDesign.UpdateRFPStatus("LS_UpdateRFPApprovalStatus");
                }
            }
            if (itemselected == true)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "SuccessMessage('Success','RFP Approved Successfully');", true);
                SaveAlertDetails(dsApprove.Tables[0].Rows[0]["RFPNo"].ToString());
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','No Items Selected');", true);
            BindRFPDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

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
            string Designer = ViewState["Sales"].ToString();
            ds = objHR.GetEmployeeCommunicationDetailsEmployeeID(Convert.ToInt32(Designer));
            if (ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    foreach (GridViewRow row in gvRFPHeader.Rows)
                    {
                        try
                        {
                            CheckBox chkditems = (CheckBox)row.FindControl("chkitems");
                            if (chkditems.Checked)
                            {
                                itemselected = true;
                                DataSet dsApprove = new DataSet();
                                objDesign = new cDesign();
                                objDesign.RFPHID = Convert.ToInt32(Convert.ToInt32(gvRFPHeader.DataKeys[row.RowIndex].Values[0].ToString()));
                                objDesign.StatusFlag = "Reply";
                                objDesign.UserID = Convert.ToInt32(objSession.employeeid);
                                objDesign.UpdateRFPStatus("LS_UpdateRFPApprovalStatus");
                            }
                        }
                        catch (Exception ec) { }
                    }
                }
                catch (Exception ec)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error Occured');", true);
                }
                if (itemselected == true)
                {
                    objAlerts.file = "";
                    objAlerts.dtSettings = objAlerts.GetEmailSettings();
                    objAlerts.EmailID = ds.Tables[0].Rows[0]["Email"].ToString();// "karthik@innovasphere.in";
                    objAlerts.Subject = txtHeader_R.Text;
                    objAlerts.Message = txtMessage_R.Text;
                    objAlerts.userID = objSession.employeeid;
                    // objAlerts.SendIndividualMail();

                    Message = "Mail Dispatched Successfully";

                    DataTable dt = (DataTable)ViewState["RFPHeaderDetails"];

                    objAlerts.EnquiryNumber = Convert.ToInt32(dt.Rows[0]["EnquiryNumber"].ToString());
                    objAlerts.userID = objSession.employeeid;
                    objAlerts.reciverType = "I";
                    objAlerts.reciverID = ds.Tables[0].Rows[0]["EmployeeID"].ToString();
                    objAlerts.Header = txtHeader_R.Text;
                    objAlerts.Message = txtMessage_R.Text;
                    objAlerts.AlertType = "Mail";
                    //  objAlerts.SaveAlertDetails();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "hideLoader();SuccessMessage('Success','" + Message + "')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','No Items Selected');", true);
                }
            }
            else
            {
                Message = "Mail Not Sending";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "hideLoader();ErrorMessage('Error','" + Message + "')", true);
            }

            BindRFPDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "hideLoader();ErrorMessage('Error','Unknown Error Occured')", true);
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvRFPHeader_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataRowView dr = (DataRowView)e.Row.DataItem;
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkditems = (CheckBox)e.Row.FindControl("chkitems");

                if (dr["RFPStatus"].ToString() == "1" || dr["RFPStatus"].ToString() == "9")
                    //chkditems.Enabled = false;
                    chkditems.Visible = false;
                else if (dr["RFPStatus"].ToString() == "7")
                    //chkditems.Enabled = true;
                    chkditems.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvRFPHeader_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dt;
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string RFPHID = gvRFPHeader.DataKeys[index].Values[0].ToString();

            dt = (DataTable)ViewState["RFPHeaderDetails"];
            hdnRFPHID.Value = RFPHID.ToString();
            dt.DefaultView.RowFilter = "RFPHID='" + RFPHID + "'";

            if (e.CommandName == "ViewRFP")
            {
                ViewState["EnquiryID"] = dt.DefaultView.ToTable().Rows[0]["EnquiryNumber"].ToString();
                ViewState["RFPHID"] = dt.DefaultView.ToTable().Rows[0]["RFPHID"].ToString();

                lblQAPApproval_V.Text = dt.DefaultView.ToTable().Rows[0]["QAPApprovalName"].ToString();
                lblQAPRefNo_V.Text = dt.DefaultView.ToTable().Rows[0]["QAPRefNo"].ToString();
                lblDrawingApprova_V.Text = dt.DefaultView.ToTable().Rows[0]["DrawingApprovalName"].ToString();
                lblInspectionRequirtment_V.Text = dt.DefaultView.ToTable().Rows[0]["InspectionRequirtmentName"].ToString();
                lblLDClause_V.Text = dt.DefaultView.ToTable().Rows[0]["LDClauseName"].ToString();
                lblDespatchDetails_V.Text = dt.DefaultView.ToTable().Rows[0]["DespatchDetails"].ToString();
                lblNotesSummary_V.Text = dt.DefaultView.ToTable().Rows[0]["NotesSummary"].ToString();

                BindAttachements(RFPHID);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUP", "ShowViewPopUp();", true);
            }

            int EnquiryNumber = Convert.ToInt32(gvRFPHeader.DataKeys[index].Values[2].ToString());
            ViewState["Enquirynumber"] = EnquiryNumber;
            string BaseHtttpPath = CustomerEnquiryHttpPath + EnquiryNumber + "/";


            if (e.CommandName == "ViewPOCopy")
            {
                objc = new cCommon();

                string FileName = gvRFPHeader.DataKeys[index].Values[3].ToString();
                ViewState["FileName"] = FileName;
                ifrm.Attributes.Add("src", BaseHtttpPath + FileName);

                if (File.Exists(CusstomerEnquirySavePath + EnquiryNumber + "//" + FileName))
                {
                    //ViewState["ifrmsrc"] = BaseHtttpPath + FileName;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewdocsPopUp();", true);

                    objc.ViewFileName(CusstomerEnquirySavePath, CustomerEnquiryHttpPath, FileName, EnquiryNumber.ToString(), ifrm);
                }
                else
                {
                    ifrm.Attributes.Add("src", "");
                    ViewState["ifrmsrc"] = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Po Copy Not Found');", true);
                }
            }

            if (e.CommandName == "ViewPoCopyWithoutPrice")
            {
                string FileName = gvRFPHeader.DataKeys[index].Values[4].ToString();
                ViewState["FileName"] = FileName;
                ifrm.Attributes.Add("src", BaseHtttpPath + FileName);

                if (File.Exists(CusstomerEnquirySavePath + EnquiryNumber + "//" + FileName))
                {
                    ViewState["ifrmsrc"] = BaseHtttpPath + FileName;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewdocsPopUp();", true);
                }
                else
                {
                    ifrm.Attributes.Add("src", "");
                    ViewState["ifrmsrc"] = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Po copy Without Price Not Found');", true);
                }
            }

            if (e.CommandName == "ViewRFPPDF")
            {
                objSales = new cSales();
                DataSet dsItem = new DataSet();
                dsItem = objSales.GetRFPItemdetails(RFPHID);

                DataSet ds = new DataSet();
                DataTable dtItemDetails = new DataTable();

                objSales = new cSales();
                objSales.RFPHID = Convert.ToInt32(RFPHID);
                ds = objSales.getBellowsDetailsByRFPHID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "RecordsFound" || dsItem.Tables[0].Rows.Count > 0)
                {
                    lblRFPNo_P.Text = dt.DefaultView.ToTable().Rows[0]["RFPNo"].ToString() + " & " + ds.Tables[1].Rows[0]["RFPCreateddate"].ToString();
                    //  lblcreateddate_p.Text = ds.Tables[1].Rows[0]["RFPCreateddate"].ToString();
                    lblCustomerName_p.Text = ddlCustomerName.SelectedItem.Text;
                    string[] PONumber = ddlCustomerPO.SelectedItem.Text.Split('/');
                    lblCustomerOrderNo_p.Text = ddlCustomerPO.SelectedItem.Text.Replace("/" + PONumber[PONumber.Length - 1], "") + " & " + ds.Tables[1].Rows[0]["CustomerOrderDate"].ToString();
                    //lblOrderDate_p.Text = ds.Tables[1].Rows[0]["CustomerOrderDate"].ToString();
                    lblProject_p.Text = dt.DefaultView.ToTable().Rows[0]["ProjectName"].ToString();
                    lblNumberOfItems_p.Text = dsItem.Tables[1].Rows[0]["TotalQuantity"].ToString();

                    if (dsItem.Tables[0].Rows.Count > 0)
                    {
                        dtItemDetails = new DataTable();
                        dtItemDetails = (DataTable)dsItem.Tables[0];

                        gvItemDetails_p.DataSource = dtItemDetails;
                        gvItemDetails_p.DataBind();

                        gvAnnexureItemList_p.DataSource = dtItemDetails;
                        gvAnnexureItemList_p.DataBind();

                        if (ds.Tables[0].Rows[0]["Message"].ToString() == "RecordsFound")
                        {
                            ds.Tables[0].Columns.Remove("Message");
                            dtItemDetails = new DataTable();
                            dtItemDetails = (DataTable)ds.Tables[0];

                            if (dtItemDetails.Columns.Contains("Gradename"))
                                dtItemDetails.Columns["Gradename"].ColumnName = "Grade Name";
                            dtItemDetails.Columns["SNo"].ColumnName = "SL.No";

                            gvBellowDetails_p.DataSource = dtItemDetails;
                            gvBellowDetails_p.DataBind();

                            gvAnnexureBellowDetails_p.DataSource = dtItemDetails;
                            gvAnnexureBellowDetails_p.DataBind();
                        }
                        else
                        {
                            gvBellowDetails_p.DataSource = "";
                            gvBellowDetails_p.DataBind();

                            gvAnnexureBellowDetails_p.DataSource = "";
                            gvAnnexureBellowDetails_p.DataBind();
                        }
                    }
                    else
                    {
                        gvItemDetails_p.DataSource = "";
                        gvItemDetails_p.DataBind();

                        gvBellowDetails_p.DataSource = "";
                        gvBellowDetails_p.DataBind();
                    }

                    lblQAPRefNo_p.Text = dt.DefaultView.ToTable().Rows[0]["QAPRefNo"].ToString();
                    lblQAPApproval_p.Text = dt.DefaultView.ToTable().Rows[0]["QAPApprovalName"].ToString();
                    lblDrawingApproval_p.Text = dt.DefaultView.ToTable().Rows[0]["DrawingApprovalName"].ToString();
                    lblInspectionRequirtment_p.Text = dt.DefaultView.ToTable().Rows[0]["InspectionRequirtmentName"].ToString();
                    lblLDClause_p.Text = dt.DefaultView.ToTable().Rows[0]["LDClauseName"].ToString();
                    lblDespatchDetails_p.Text = dt.DefaultView.ToTable().Rows[0]["DespatchDetails"].ToString();
                    lblMarketing_p.Text = ds.Tables[1].Rows[0]["SalesE"].ToString();
                    lblDesign_p.Text = ds.Tables[1].Rows[0]["DesignE"].ToString();
                    // lblDataEntry_p.Text = ds.Tables[1].Rows[0]["PurchaseE"].ToString();
                    lblProjectIncharge_p.Text = ds.Tables[1].Rows[0]["SalesE"].ToString();
                    lblApprovedBy_p.Text = ds.Tables[1].Rows[0]["RFPApprovedBy"].ToString();
                    lblDueDateForDispatch_p.Text = dt.DefaultView.ToTable().Rows[0]["DeliveryDate"].ToString();//ds.Tables[1].Rows[0][""].ToString();
                    lblNotesSummary_p.Text = dt.DefaultView.ToTable().Rows[0]["NotesSummary"].ToString();
                    lblLocationName_p.Text = dt.DefaultView.ToTable().Rows[0]["LocationName"].ToString();
                    GeneratePDF();

                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Add RFP item Details');", true);
                //objc = new cCommon();
                //string PDFSavePath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
                //string PDFhttpPath = ConfigurationManager.AppSettings["PDFPath"].ToString();

                //string FileName = gvRFPHeader.DataKeys[index].Values[5].ToString().Replace('/', '-') + ".html";
                //ViewState["FileName"] = FileName;
                //ifrm.Attributes.Add("src", PDFhttpPath + FileName);

                //objc.ReadhtmlFile(FileName, hdnPdfContent);

                ////if (File.Exists(PDFSavePath + "//" + FileName))
                ////{
                ////    ViewState["ifrmsrc"] = PDFhttpPath + FileName;
                ////    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewdocsPopUp();", true);
                ////}
                ////else
                ////{
                ////    ifrm.Attributes.Add("src", "");
                ////    ViewState["ifrmsrc"] = "";
                ////    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','File Not Found');", true);
                ////}
            }

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvAttachments_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        objSales = new cSales();
        objc = new cCommon();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            int AttachementID = Convert.ToInt32(gvAttachments.DataKeys[index].Values[0].ToString());

            if (e.CommandName.ToString() == "ViewDocs")
            {
                string BasehttpPath = CustomerEnquiryHttpPath + ViewState["EnquiryID"].ToString() + "/";
                string FileName = ((Label)gvAttachments.Rows[index].FindControl("lblFileName_V")).Text.ToString();
                ViewState["FileName"] = FileName;
                ifrm.Attributes.Add("src", BasehttpPath + FileName);
                string imgname = CusstomerEnquirySavePath + ViewState["EnquiryID"].ToString() + "\\" + FileName;
                if (File.Exists(imgname))
                {
                    //ViewState["ifrmsrc"] = imgname;
                    //ViewState["ifrmsrc"] = BasehttpPath + FileName;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewdocsPopUp();", true);
                    objc.ViewFileName(CusstomerEnquirySavePath, CustomerEnquiryHttpPath, FileName, ViewState["EnquiryID"].ToString(), ifrm);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewPopUp();", true);
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
            Log.Message(ex.ToString());
        }
        finally
        {
            objSales = null;
        }
    }

    protected void gvDrawingFiles_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            objc = new cCommon();

            int index = Convert.ToInt32(e.CommandArgument.ToString());

            string EnquiryNumber = gvDrawingFiles.DataKeys[index].Values[1].ToString();
            string DrawingName = gvDrawingFiles.DataKeys[index].Values[0].ToString();

            string BasehttpPath = CustomerEnquiryHttpPath + EnquiryNumber + "/";
            string FileName = BasehttpPath + DrawingName;

            ViewState["FileName"] = FileName;
            ifrm.Attributes.Add("src", FileName);
            if (File.Exists(CusstomerEnquirySavePath + EnquiryNumber + '/' + DrawingName))
            {
                //ViewState["ifrmsrc"] = FileName;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowViewdocsPopUp();", true);
                objc.ViewFileName(CusstomerEnquirySavePath, CustomerEnquiryHttpPath, DrawingName, EnquiryNumber.ToString(), ifrm);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewPopUp();", true);
            }
            else
            {
                ViewState["ifrmsrc"] = "";
                ifrm.Attributes.Add("src", FileName);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Attach Not Found');", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvBellowDetails_p_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[1].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[2].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[3].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[4].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[5].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[6].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[7].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[8].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[9].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[10].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[11].Attributes.Add("style", "text-align:center;");
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[1].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[2].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[3].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[4].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[5].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[6].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[7].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[8].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[9].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[10].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[11].Attributes.Add("style", "text-align:center;");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvAnnexureBellowDetails_p_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[1].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[2].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[3].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[4].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[5].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[6].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[7].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[8].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[9].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[10].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[11].Attributes.Add("style", "text-align:center;");
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[1].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[2].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[3].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[4].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[5].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[6].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[7].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[8].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[9].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[10].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[11].Attributes.Add("style", "text-align:center;");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void ddlRFPCustomerNameLoad()
    {
        try
        {
            objc = new cCommon();
            DataSet dsPOHID = new DataSet();
            DataSet dsCustomer = new DataSet();
            //dsCustomer = objc.getCustomerNameByUserID(Convert.ToInt32(objSession.employeeid), ddlCustomerName, "LS_GetCustomernameByUserIDForRFPApprovalspage");
            //dsPOHID = objc.GetCustomerPODetailsByUserID(Convert.ToInt32(objSession.employeeid), ddlCustomerPO, "LS_GetPONoByUserIDForRFPApprovalPage");

            dsCustomer = objc.GetCustomerNameByPendingList(Convert.ToInt32(objSession.employeeid), ddlCustomerName, "LS_GetCustomernameByUserIDForRFPApprovalspage", rblRFPChange.SelectedValue);

            ViewState["CustomerDetails"] = dsCustomer.Tables[0];

            dsPOHID = objc.GetRFPNoByPendingList(Convert.ToInt32(objSession.employeeid), ddlCustomerPO, "LS_GetPONoByUserIDForRFPApprovalPage", rblRFPChange.SelectedValue);

            ViewState["PONoDetails"] = dsPOHID.Tables[0];
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void GeneratePDF()
    {
        DataSet ds = new DataSet();
        objc = new cCommon();
        string Flag;
        try
        {
            var sbPurchaseOrder = new StringBuilder();

            var sbGvItemAnnexure = new StringBuilder();
            var sbBellowAnnexure = new StringBuilder();

            cQRcode objQr = new cQRcode();

            string QrNumber = objQr.generateQRNumber(9);
            string displayQrnumber = objQr.getDisplayQRNumber(QrNumber, 3, '-');

            string Code = displayQrnumber + "/" + objSession.employeeid + "/" + DateTime.Now.ToString();

            string QrCode = objQr.QRcodeGeneration(Code);
            if (QrCode != "")
            {
                //imgQrcode.ImageUrl = QrCode;
                objQr.QRNumber = displayQrnumber;
                objQr.fileName = "RFPApproval";
                objQr.createdBy = objSession.employeeid;
                string pageName = Path.GetFileName(Page.AppRelativeVirtualPath);
                objQr.Pagename = pageName;
                objQr.saveQRNumber();
            }

            // divPrint.Attributes.Add("style", "display:block;height:287mm;width:200mm;margin:15px;padding:15px;");
            //divPrint.RenderControl(new HtmlTextWriter(new StringWriter(sbPurchaseOrder)));

            //if (gvAnnexureItemList_p.Rows.Count > 0)
            //{
            //    Flag = "multi";
            //    divAnnexureItemList_p.Attributes.Add("style", "display:block;height:287mm;width:200mm;float:left");
            //    divAnnexureBellowList_p.Attributes.Add("style", "display:block;height:287mm;width:200mm;float:left");

            //    divAnnexureItemList_p.RenderControl(new HtmlTextWriter(new StringWriter(sbGvItemAnnexure)));
            //    divAnnexureBellowList_p.RenderControl(new HtmlTextWriter(new StringWriter(sbGvItemAnnexure)));
            //}
            //else
            //{
            //    Flag = "single";
            //    divAnnexureItemList_p.Attributes.Add("style", "display:none;");
            //    divAnnexureBellowList_p.Attributes.Add("style", "display:none;");
            //}

            //string htmlfile = ViewState["RFPNo"].ToString().Replace('/', '-') + ".html";
            //string pdffile = ViewState["RFPNo"].ToString().Replace('/', '-') + ".pdf";
            //string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
            //string pdfFileURL = LetterPath + pdffile;

            //string htmlfileURL = LetterPath + htmlfile;

            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 2].ToString() + "/" + pagename[pagename.Length - 1].ToString();

            string Main = url.Replace(Replacevalue, "Assets/css/main.css");
            string epstyleurl = url.Replace(Replacevalue, "Assets/css/ep-style.css");
            string style = url.Replace(Replacevalue, "Assets/css/style.css");
            string Print = url.Replace(Replacevalue, "Assets/css/print.css");
            string topstrip = url.Replace(Replacevalue, "Assets/images/topstrrip.jpg");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "RFPPrint('" + epstyleurl + "','" + Main + "','" + QrCode + "');", true);

            //SaveHtmlFile(htmlfileURL, "PO", "", sbPurchaseOrder.ToString(), sbGvItemAnnexure.ToString(), sbBellowAnnexure.ToString(), Flag);
            //objc.GenerateAndSavePDF(LetterPath, pdfFileURL, pdffile, htmlfileURL);
            //objc.ReadhtmlFile(htmlfile, hdnPdfContent);
            // divPrint.Attributes.Add("style", "display:none;");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void BindAttachements(string RFPHID)
    {
        try
        {
            objSales = new cSales();
            DataSet dsGetAttachementsDetails = new DataSet();

            objSales.RFPHID = Convert.ToInt32(RFPHID);

            dsGetAttachementsDetails = objSales.GetAttachementDetailsByRFPHID();

            if (dsGetAttachementsDetails.Tables[0].Rows.Count > 0)
            {
                ViewState["Attachement"] = dsGetAttachementsDetails.Tables[0];
                gvAttachments.DataSource = dsGetAttachementsDetails.Tables[0];
                gvAttachments.DataBind();

                gvDrawingFiles.DataSource = dsGetAttachementsDetails.Tables[1];
                gvDrawingFiles.DataBind();
            }
            else
            {
                gvAttachments.DataSource = "";
                gvAttachments.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindRFPDetails()
    {
        DataSet ds = new DataSet();
        objDesign = new cDesign();
        try
        {
            objDesign.POHID = Convert.ToInt32(ddlCustomerPO.SelectedValue);
            ds = objDesign.GetRFPHeaderDetails();

            ViewState["RFPHeaderDetails"] = ds.Tables[0];
            ViewState["Sales"] = ds.Tables[1].Rows[0]["Sales"].ToString();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvRFPHeader.DataSource = ds.Tables[0];
                gvRFPHeader.DataBind();
            }
            else
            {
                gvRFPHeader.DataSource = "";
                gvRFPHeader.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ShowHideControls(string Mode)
    {
        divAdd.Visible = divInput.Visible = divOutput.Visible = false;
        try
        {
            switch (Mode.ToLower())
            {
                case "add":
                    divAdd.Visible = true;
                    break;
                case "view":
                    divAdd.Visible = true;
                    divOutput.Visible = true;
                    break;
                case "input":
                    divInput.Visible = true;
                    break;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

    }

    public void SaveAlertDetails(string RFPNo)
    {
        EmailAndSmsAlerts objAlerts = new EmailAndSmsAlerts();
        DataSet ds = new DataSet();
        cCommon objc = new cCommon();
        try
        {
            //objc.EnquiryID = RFPNo;
            ds = objc.GetEmployeeIDDetailsByUserTypeIDSANDErpUserType("11", 3);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                objAlerts.EntryMode = "Individual";
                objAlerts.AlertType = "Mail";
                objAlerts.userID = objSession.employeeid;
                objAlerts.reciverType = "Staff";
                objAlerts.file = "";
                objAlerts.reciverID = ds.Tables[0].Rows[i]["EmployeeID"].ToString();
                objAlerts.EmailID = "";
                objAlerts.GroupID = 0;
                objAlerts.Subject = "Alert For RFP Staff Assignment";
                objAlerts.Message = "RFP Relesed Allocate The Resources" + RFPNo;
                objAlerts.Status = 0;
                objAlerts.SaveCommunicationEmailAlertDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"PageLoadComplete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvRFPHeader.Rows.Count > 0)
            gvRFPHeader.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (gvAttachments.Rows.Count > 0)
            gvAttachments.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}