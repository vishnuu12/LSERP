using eplus.core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_JobCardExpensesSheet : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cProduction objP;

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
        string target = Request["__EVENTTARGET"];
        string arg = Request["__EVENTARGUMENT"];
        try
        {
            if (IsPostBack == false)
            {
                objP = new cProduction();
                DataSet dsRFPHID = new DataSet();
                DataSet dsCustomer = new DataSet();
                //dsRFPHID = objP.GetRFPDetailsByUserIDAndPJOCompleted(Convert.ToInt32(objSession.employeeid), ddlRFPNo);
                //ViewState["RFPDetails"] = dsRFPHID.Tables[0];
                ShowHideControls("view,input");
                BindJobCardNoProcessCostDetails();
                btnAddNew_OnClick(null, null);
            }
            if (target == "deletegvrow")
            {
                objP = new cProduction();
                DataSet ds = new DataSet();
                objP.CJPCDID = Convert.ToInt32(arg.ToString());

                ds = objP.DeleteJObcardDetailsByCJPCDID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Rate Deleted Successfully');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "InfoMessage('Information','You Cannot Delete The Record');", true);

                BindJobCardNoProcessCostDetails();
            }

            if (target == "printJobcard")
            {
                int index = Convert.ToInt32(arg.ToString());
                ViewState["ProcessName"] = gvJobCardNoProcessCostDetails.DataKeys[index].Values[1].ToString();
                hdnJCHID.Value = Session["JCHID"].ToString(); //ddlJobCardNo.SelectedValue;
                //hdnCJPCDID.Value = CJPCDID;
                hdnCJPCDID.Value = gvJobCardNoProcessCostDetails.DataKeys[index].Values[0].ToString();
                PrintJobCardDetailsByJCHID();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlRFPNo_SelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlRFPNo.SelectedIndex > 0)
                BindJobOrderNoDetailsByRFPHID();
            else
            {
                cCommon objc = new cCommon();
                objc.EmptyDropDownList(ddlJobCardNo);
                //  ShowHideControls("add");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlJobCardNo_OnSelectIndexChanged(object sender, EventArgs e)
    {
        //ShowHideControls("add,addnew,view");
        //BindJobCardNoProcessCostDetails();
    }

    protected void ddlContractorTeamMemberName_J_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindRateDetailsByTeamnameAndProcessName();
    }

    protected void ddlJobProcessName_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        BindRateDetailsByTeamnameAndProcessName();
    }

    #endregion

    #region"Button EVents"

    protected void btnAddNew_OnClick(object sender, EventArgs e)
    {
        try
        {
            objP = new cProduction();
            DataSet ds = new DataSet();
            objP.GetContractorDetails(null, ddlContractorTeamMemberName_AP, null);
            objP.GetJobCardProcessNameDetails(ddlJobProcessName);

            objP.JCHID = Convert.ToInt32(Session["JCHID"].ToString());
            ds = objP.GetRFPNoAndJobcardNoProcessnameDetailsByJCHID();

            lblRFPNo.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
            lbljobcardNo.Text = ds.Tables[0].Rows[0]["JobCardNo"].ToString();

            // ShowHideControls("input");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSaveExpenses_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.CJPCDID = Convert.ToInt32(hdnCJPCDID.Value);
            objP.RFPHID = Convert.ToInt32(Session["RFPHID"].ToString());
            objP.JCHID = Convert.ToInt32(Session["JCHID"].ToString());
            objP.CTDID = Convert.ToInt32(ddlContractorTeamMemberName_AP.SelectedValue);
            objP.JCPNMID = Convert.ToInt32(ddlJobProcessName.SelectedValue);
            objP.Qty = Convert.ToDecimal(txtqty.Text);
            objP.Remarks = txtRemarks.Text;
            objP.UserID = Convert.ToInt32(objSession.employeeid);
            ds = objP.SaveJobCardNoProcessCostDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Job Card Details Saved SuccessFully');", true);
            else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Job Card Details updaed SuccessFully');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);

            BindJobCardNoProcessCostDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        hdnCJPCDID.Value = "0";
        // ShowHideControls("addnew,view");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        // ShowHideControls("addnew,view");
        hdnCJPCDID.Value = "0";
        BindJobCardNoProcessCostDetails();
    }

    #endregion

    #region"GridView Events"

    protected void gvJobCardNoProcessCostDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "EditJobCardRate")
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            hdnCJPCDID.Value = gvJobCardNoProcessCostDetails.DataKeys[index].Values[0].ToString();

            bindJobCardProcessCostDetailsByCJPCDID();
            //ShowHideControls("input");
        }
    }

    #endregion

    #region"Common Methods"

    private void PrintJobCardDetailsByJCHID()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        StringBuilder sb = new StringBuilder();
        try
        {
            if (ViewState["ProcessName"].ToString() == "Marking & Cutting")
            {
                objP.JCHID = Convert.ToInt32(Session["JCHID"].ToString());
                objP.CJPCDID = Convert.ToInt32(hdnCJPCDID.Value);

                ds = objP.GetJobCardNoProcessCostPrintDetails("LS_GetMarkingAndCuttingDetailsByJCHIDAndCJPCDID_PRINT");

                lblProcessNameHeader_MC_P.Text = ds.Tables[1].Rows[0]["PartName"].ToString() + " / " + ds.Tables[0].Rows[0]["ProcessName"].ToString();
                lblJobOrderID_MC_P.Text = ds.Tables[0].Rows[0]["SecondaryID"].ToString();
                lblProcessName_MC_P.Text = ds.Tables[0].Rows[0]["ProcessName"].ToString();
                ViewState["JobOrderID"] = ds.Tables[0].Rows[0]["SecondaryID"].ToString();

                lblDate_MC_P.Text = ds.Tables[7].Rows[0]["IssuedDate"].ToString();
                lblRFPNo_MC_P.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
				//lblRFPNo_AP_PDF.Text = ds.Tables[6].Rows[0]["RFPNo"].ToString();
                lblContractorName_MC_P.Text = ds.Tables[7].Rows[0]["ContractorName"].ToString();
                lblContractorTeamMemberName_MC_P.Text = ds.Tables[7].Rows[0]["ContractorTeamName"].ToString();

                lblItemNameSize_MC_P.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                lblDrawingname_MC_P.Text = ds.Tables[2].Rows[0]["FileName"].ToString();
                lblPartName_MC_P.Text = ds.Tables[1].Rows[0]["PartName"].ToString();
                //  lblPartQty_p.Text = ds.Tables[0].Rows[0]["PartQty"].ToString();
                lblMaterialCategory_MC_P.Text = ds.Tables[1].Rows[0]["CategoryName"].ToString();
                lblmaterialGrade_MC_P.Text = ds.Tables[1].Rows[0]["GradeName"].ToString();
                lblThickness_MC_P.Text = ds.Tables[1].Rows[0]["THKValue"].ToString();
                lblMRNNumber_MC_P.Text = ds.Tables[8].Rows[0]["MRNNumber"].ToString();
                lblJobOrderRemarks_MC_P.Text = ds.Tables[7].Rows[0]["JobOrderRemarks"].ToString();

                lblOverallremarks_MC_P.Text = ds.Tables[0].Rows[0]["OverAllRemarks"].ToString();
                lblDeadlineDate_MC_P.Text = ds.Tables[0].Rows[0]["DELIVERYDATE"].ToString();
                lbljobcardStatus_MC_P.Text = ds.Tables[0].Rows[0]["JobCardStatus"].ToString();

                if (ds.Tables[6].Rows.Count > 0)
                {
                    gvPartSerialNo_MC_P.DataSource = ds.Tables[6];
                    gvPartSerialNo_MC_P.DataBind();
                }

                string dynamicctrls = "<div class=\"col-sm-12 p-t-10\"><span style='width:35%;'>fabricationName</span><span style='width:5%;'>:</span><span>fabricationValue</span></div>";
                string result = "";

                lblFabricationType_MC_P.Text = ds.Tables[5].Rows[0]["FabricationTypeName"].ToString();

                foreach (DataRow dr in ds.Tables[5].Rows)
                {
                    result = "";
                    result = dynamicctrls.Replace("fabricationName", dr["Name"].ToString()).Replace("fabricationValue", dr["Value"].ToString());
                    sb.Append(result);
                }
                divfabrication_MC_P.InnerHtml = sb.ToString();

                if (ds.Tables[9].Rows.Count > 0)
                {
                    gvIssueDetails_MC_P.DataSource = ds.Tables[9];
                    gvIssueDetails_MC_P.DataBind();
                }

                if (!string.IsNullOrEmpty(ds.Tables[4].Rows[0]["OfferQC"].ToString()))
                    lblOfferQC_MC_P.Text = ds.Tables[4].Rows[0]["OfferQC"].ToString().Replace(",", "</br>");
                else
                    lblOfferQC_MC_P.Text = "";

                lblTotalCost_MC_P.Text = ds.Tables[8].Rows[0]["ProcessTotalCost"].ToString();
                lblPartQty_MC_P.Text = ds.Tables[8].Rows[0]["PartQty"].ToString();

                ViewState["Address"] = ds.Tables[10];

                gvQCObservationDetails_MC_P.DataSource = ds.Tables[11];
                gvQCObservationDetails_MC_P.DataBind();

                cQRcode objQr = new cQRcode();

                string QrNumber = objQr.generateQRNumber(9);
                string displayQrnumber = objQr.getDisplayQRNumber(QrNumber, 3, '-');

                string Code = displayQrnumber + "/" + objSession.employeeid + "/" + DateTime.Now.ToString();

                string QrCode = objQr.QRcodeGeneration(Code);
                if (QrCode != "")
                {
                    //imgQrcode.Attributes.Add("style", "display:block;");
                    //imgQrcode.ImageUrl = QrCode;
                    ViewState["QrCode"] = QrCode;
                    objQr.QRNumber = displayQrnumber;
                    objQr.fileName = ViewState["ProcessName"].ToString() + "/" + ViewState["JobOrderID"].ToString();
                    objQr.createdBy = objSession.employeeid;
                    string pageName = Path.GetFileName(Page.AppRelativeVirtualPath);
                    objQr.Pagename = pageName;
                    objQr.saveQRNumber();
                }
                else
                    ViewState["QrCode"] = "";

                DataTable dtAddress = new DataTable();
                dtAddress = (DataTable)ViewState["Address"];

                hdnAddress.Value = dtAddress.Rows[0]["Address"].ToString();
                hdnPhoneAndFaxNo.Value = dtAddress.Rows[0]["PhoneAndFaxNo"].ToString();
                hdnEmail.Value = dtAddress.Rows[0]["Email"].ToString();
                hdnWebsite.Value = dtAddress.Rows[0]["WebSite"].ToString();

                //gvQCObservationDetails_MC_P.DataSource = ds.Tables[];
                //gvQCObservationDetails_MC_P.DataBind();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "PrintMarkinAndCutting('" + ViewState["QrCode"].ToString() + "','MC');", true);

                //  GeneratePDFFile("FabricationMarkingAndCutting");
            }

            else if (ViewState["ProcessName"].ToString() == "Fabrication & Welding")
            {
                objP.CJPCDID = Convert.ToInt32(hdnCJPCDID.Value);
                objP.JCHID = Convert.ToInt32(Session["JCHID"].ToString());
                ds = objP.GetJobCardNoProcessCostPrintDetails("LS_GetFabricationAndWeldingDetailsByJCHIDAndCJPCDID_PRINT");

                lblProcessName_FW_P_H.Text = ds.Tables[1].Rows[0]["PartName"].ToString() + " / " + ds.Tables[0].Rows[0]["ProcessName"].ToString();
                lblJobOrderID_FW_P.Text = ds.Tables[0].Rows[0]["SecondaryID"].ToString();
                lblProcessname_FW_P.Text = ds.Tables[0].Rows[0]["ProcessName"].ToString();
                ViewState["JobOrderID"] = ds.Tables[0].Rows[0]["SecondaryID"].ToString();

                lblDate_FW_P.Text = ds.Tables[7].Rows[0]["IssuedDate"].ToString();
                lblRFPNo_FW_P.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
                lblContractorName_FW_P.Text = ds.Tables[7].Rows[0]["ContractorName"].ToString();
                lblContractorTeamname_FW_P.Text = ds.Tables[7].Rows[0]["ContractorTeamName"].ToString();

                lblItemNameSize_FW_P.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();//ViewState["ItemName"].ToString();
                lblDrawingName_FW_P.Text = ds.Tables[2].Rows[0]["FileName"].ToString();
                lblPartname_FW_P.Text = ds.Tables[1].Rows[0]["PartName"].ToString();
                //  lblPartQty_p.Text = ds.Tables[0].Rows[0]["PartQty"].ToString();
                lblMaterialCategory_FW_P.Text = ds.Tables[1].Rows[0]["CategoryName"].ToString();
                lblMaterialGrade_FW_P.Text = ds.Tables[1].Rows[0]["GradeName"].ToString();
                lblThickness_FW_P.Text = ds.Tables[1].Rows[0]["THKValue"].ToString();
                lblMRNNumber_FW_P.Text = ds.Tables[8].Rows[0]["MRNNumber"].ToString();
                lblJobOrderRemarks_FW_P.Text = ds.Tables[7].Rows[0]["JobOrderRemarks"].ToString();

                lblRemarks_FW_P.Text = ds.Tables[0].Rows[0]["OverAllRemarks"].ToString();
                lblDeadLineDate_FW_P.Text = ds.Tables[0].Rows[0]["DELIVERYDATE"].ToString();
                lbljobcardStatus_FW_P.Text = ds.Tables[0].Rows[0]["JobCardStatus"].ToString();

                if (ds.Tables[6].Rows.Count > 0)
                {
                    gvPartSno_FW_P.DataSource = ds.Tables[6];
                    gvPartSno_FW_P.DataBind();
                }

                gvWPSDetails_FW_P.DataSource = ds.Tables[5];
                gvWPSDetails_FW_P.DataBind();

                //if (ds.Tables[9].Rows.Count > 0)
                //{
                //    gvMRNIssueDetails_SMC_P.DataSource = ds.Tables[9];
                //    gvMRNIssueDetails_SMC_P.DataBind();
                //}
                if (!string.IsNullOrEmpty(ds.Tables[4].Rows[0]["OfferQC"].ToString()))
                    lblOfferQCTest_FW_P.Text = ds.Tables[4].Rows[0]["OfferQC"].ToString().Replace(",", "</br>");
                else
                    lblOfferQCTest_FW_P.Text = "";

                lblTotalCost_FW_P.Text = ds.Tables[8].Rows[0]["ProcessTotalCost"].ToString();
                lblPartQty_FW_P.Text = ds.Tables[8].Rows[0]["PartQty"].ToString();
                lblNOP_FW_P.Text = ds.Tables[8].Rows[0]["NOP"].ToString();
                ViewState["Address"] = ds.Tables[10];

                string dynamicctrls = "<div class=\"col-sm-12 p-t-10\"><span style='width:35%;'>fabricationName</span><span style='width:5%;'>:</span><span>fabricationValue</span></div>";
                string result = "";

                if (ds.Tables[9].Rows.Count > 0)
                    lblFabricationType_FW_P.Text = ds.Tables[9].Rows[0]["FabricationTypeName"].ToString();
                else
                    lblFabricationType_FW_P.Text = "";

                foreach (DataRow dr in ds.Tables[9].Rows)
                {
                    result = "";
                    result = dynamicctrls.Replace("fabricationName", dr["Name"].ToString()).Replace("fabricationValue", dr["Value"].ToString());
                    sb.Append(result);
                }

                divfabrication_FW_P.InnerHtml = sb.ToString();

                if (ds.Tables[11].Rows.Count > 0)
                {
                    gvBeforeWelding_FW_P.DataSource = ds.Tables[11];
                    gvBeforeWelding_FW_P.DataBind();
                }
                else
                {
                    gvBeforeWelding_FW_P.DataSource = "";
                    gvBeforeWelding_FW_P.DataBind();
                }

                if (ds.Tables[12].Rows.Count > 0)
                {
                    gvDuringWelding_FW_P.DataSource = ds.Tables[12];
                    gvDuringWelding_FW_P.DataBind();
                }
                else
                {
                    gvDuringWelding_FW_P.DataSource = "";
                    gvDuringWelding_FW_P.DataBind();
                }

                if (ds.Tables[13].Rows.Count > 0)
                {
                    gvFinalWelding_FW_P.DataSource = ds.Tables[13];
                    gvFinalWelding_FW_P.DataBind();
                }
                else
                {
                    gvFinalWelding_FW_P.DataSource = "";
                    gvFinalWelding_FW_P.DataBind();
                }

                cQRcode objQr = new cQRcode();

                string QrNumber = objQr.generateQRNumber(9);
                string displayQrnumber = objQr.getDisplayQRNumber(QrNumber, 3, '-');

                string Code = displayQrnumber + "/" + objSession.employeeid + "/" + DateTime.Now.ToString();

                string QrCode = objQr.QRcodeGeneration(Code);
                if (QrCode != "")
                {
                    //imgQrcode.Attributes.Add("style", "display:block;");
                    //imgQrcode.ImageUrl = QrCode;
                    ViewState["QrCode"] = QrCode;
                    objQr.QRNumber = displayQrnumber;
                    objQr.fileName = ViewState["ProcessName"].ToString() + "/" + ViewState["JobOrderID"].ToString();
                    objQr.createdBy = objSession.employeeid;
                    string pageName = Path.GetFileName(Page.AppRelativeVirtualPath);
                    objQr.Pagename = pageName;
                    objQr.saveQRNumber();
                }
                else
                    ViewState["QrCode"] = "";

                DataTable dtAddress = new DataTable();
                dtAddress = (DataTable)ViewState["Address"];

                hdnAddress.Value = dtAddress.Rows[0]["Address"].ToString();
                hdnPhoneAndFaxNo.Value = dtAddress.Rows[0]["PhoneAndFaxNo"].ToString();
                hdnEmail.Value = dtAddress.Rows[0]["Email"].ToString();
                hdnWebsite.Value = dtAddress.Rows[0]["WebSite"].ToString();

                //gvQCObservationDetails_MC_P.DataSource = ds.Tables[];
                //gvQCObservationDetails_MC_P.DataBind();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "PrintMarkinAndCutting('" + ViewState["QrCode"].ToString() + "','FW');", true);

                // GeneratePDFFile("FabricationAndWelding");
            }

            else if (ViewState["ProcessName"].ToString() == "Assembly")
            {
                objP.JCHID = Convert.ToInt32(Session["JCHID"].ToString());
                objP.CJPCDID = Convert.ToInt32(hdnCJPCDID.Value);

                ds = objP.GetJobCardNoProcessCostPrintDetails("LS_GetAssemplyWeldingDetailsByJCHIDAndCJPCDID_PRINT");

                lblProcessName_AP_PDFHeader.Text = "Sub Assembly Welding";
                lblJobOrderID_AP_PDF.Text = ds.Tables[0].Rows[0]["AssemplyJobCardID"].ToString();
                lblDate_AP_PDF.Text = ds.Tables[0].Rows[0]["IssueDate"].ToString();
                //lblRFPNo_AP_PDF.Text = ddlRFPNo.SelectedItem.Text;
				lblRFPNo_AP_PDF.Text = ds.Tables[6].Rows[0]["RFPNo"].ToString();
                lblContractorName_AP_PDF.Text = ds.Tables[0].Rows[0]["ContractorName"].ToString();//ddlContractorName_AP.SelectedItem.Text;
                lblContractorTeamMemberName_AP_PDF.Text = ds.Tables[0].Rows[0]["ContractorTeamName"].ToString();//ddlContractorTeamMemberName_AP.SelectedItem.Text;
                lblItemNameSize_AP_PDF.Text = "";// lblItemNameSize_AP.Text;
                lblDrawingName_AP_PDF.Text = ds.Tables[1].Rows[0]["DrawingName"].ToString();
                lblPartName_AP_PDF.Text = "Sub Assembly";
                lblItemQty_AP_PDF.Text = "1";
                lblProcessName_AP_PDF.Text = "Sub Assembly";

                //gvPartSerielNumber_AP_PDF.DataSource = ds.Tables[1];
                //gvPartSerielNumber_AP_PDF.DataBind();
                gvWPSDetails_AP_PDF.DataSource = ds.Tables[2];
                gvWPSDetails_AP_PDF.DataBind();

                gvBoughtOutItemIssuedDetails_AP_PDF.DataSource = ds.Tables[3];
                gvBoughtOutItemIssuedDetails_AP_PDF.DataBind();

                lblJobOrderRemarks_AP_PDF.Text = ds.Tables[0].Rows[0]["JobOrderRemarks"].ToString();
                lblTotalCost_AP_PDF.Text = ds.Tables[0].Rows[0]["TotalCost"].ToString();

                cQRcode objQr = new cQRcode();

                string QrNumber = objQr.generateQRNumber(9);
                string displayQrnumber = objQr.getDisplayQRNumber(QrNumber, 3, '-');

                string Code = displayQrnumber + "/" + objSession.employeeid + "/" + DateTime.Now.ToString();

                string QrCode = objQr.QRcodeGeneration(Code);
                if (QrCode != "")
                {
                    // imgQrcode.Attributes.Add("style", "display:block;");
                    //  imgQrcode.ImageUrl = QrCode;
                    ViewState["QrCode"] = QrCode;
                    objQr.QRNumber = displayQrnumber;
                    objQr.fileName = "SubAssemplyWelding" + "/" + ds.Tables[0].Rows[0]["AssemplyJobCardID"].ToString();
                    objQr.createdBy = objSession.employeeid;
                    string pageName = Path.GetFileName(Page.AppRelativeVirtualPath);
                    objQr.Pagename = pageName;
                    objQr.saveQRNumber();
                }
                else
                    ViewState["QrCode"] = "";

                string Address = ds.Tables[5].Rows[0]["Address"].ToString();
                string PhoneAndFaxNo = ds.Tables[5].Rows[0]["PhoneAndFaxNo"].ToString();
                string Email = ds.Tables[5].Rows[0]["Email"].ToString();
                string WebSite = ds.Tables[5].Rows[0]["WebSite"].ToString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "PrintAssemplyPlanningPDF('" + QrCode + "','" + Address + "','" + PhoneAndFaxNo + "','" + Email + "','" + WebSite + "');", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindJobCardProcessCostDetailsByCJPCDID()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.CJPCDID = Convert.ToInt32(hdnCJPCDID.Value);
            ds = objP.GetJobCardProcessCostDetailsByCJPCDID();

            objP.GetContractorDetails(null, ddlContractorTeamMemberName_AP, null);
            objP.GetJobCardProcessNameDetails(ddlJobProcessName);

            ddlJobProcessName.SelectedValue = ds.Tables[0].Rows[0]["JCPNMID"].ToString();
            ddlContractorTeamMemberName_AP.SelectedValue = ds.Tables[0].Rows[0]["CTDID"].ToString();
            txtqty.Text = ds.Tables[0].Rows[0]["Qty"].ToString();
            txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();

            BindRateDetailsByTeamnameAndProcessName();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindRateDetailsByTeamnameAndProcessName()
    {
        objP = new cProduction();
        DataSet ds = new DataSet();
        try
        {
            if (ddlContractorTeamMemberName_AP.SelectedIndex > 0 && ddlJobProcessName.SelectedIndex > 0)
            {
                objP.CTDID = Convert.ToInt32(ddlContractorTeamMemberName_AP.SelectedValue);
                objP.JCPNMID = Convert.ToInt32(ddlJobProcessName.SelectedValue);

                ds = objP.GetRateDetailsByTeamNameAndProcessName();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblrate.Text = ds.Tables[0].Rows[0]["Rate"].ToString();
                    lbluom.Text = ds.Tables[0].Rows[0]["UomName"].ToString();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('information','Add Rate Details For Contractor This Process');", true);
                    lblrate.Text = "";
                    lbluom.Text = "";
                }
            }
            else
            {
                lblrate.Text = "";
                lbluom.Text = "";
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindJobOrderNoDetailsByRFPHID()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objP.GetJobOrderNoDetailsByRFPHID(ddlJobCardNo);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindJobCardNoProcessCostDetails()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            //objP.JCHID = Convert.ToInt32(ddlJobCardNo.SelectedValue);
            //objP.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            //Session["JCHID"]
            //Session["RFPHID"]
            objP.JCHID = Convert.ToInt32(Session["JCHID"].ToString());
            objP.RFPHID = Convert.ToInt32(Session["RFPHID"].ToString());

            ds = objP.GetJobCardNoProcessCostDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvJobCardNoProcessCostDetails.DataSource = ds.Tables[0];
                gvJobCardNoProcessCostDetails.DataBind();
            }
            else
            {
                gvJobCardNoProcessCostDetails.DataSource = "";
                gvJobCardNoProcessCostDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }


    private void GeneratePDFFile(string ProcessName)
    {
        cCommon objc = new cCommon();
        try
        {
            string MAXPDFID = "";
            var sb = new StringBuilder();
            ////divPrintReceiptDetails.RenderControl(new HtmlTextWriter(new StringWriter(sb)));           

            // divLSERPLogo.Visible = false;

            cQRcode objQr = new cQRcode();

            string QrNumber = objQr.generateQRNumber(9);
            string displayQrnumber = objQr.getDisplayQRNumber(QrNumber, 3, '-');

            string Code = displayQrnumber + "/" + objSession.employeeid + "/" + DateTime.Now.ToString();

            string QrCode = objQr.QRcodeGeneration(Code);
            if (QrCode != "")
            {
                // imgQrcode.Attributes.Add("style", "display:block;");
                //  imgQrcode.ImageUrl = QrCode;
                ViewState["QrCode"] = QrCode;
                objQr.QRNumber = displayQrnumber;
                objQr.fileName = ProcessName + "/" + ViewState["JobOrderID"].ToString();
                objQr.createdBy = objSession.employeeid;
                string pageName = Path.GetFileName(Page.AppRelativeVirtualPath);
                objQr.Pagename = pageName;
                objQr.saveQRNumber();
            }
            else
                ViewState["QrCode"] = "";

            if (ProcessName == "FabricationMarkingAndCutting")
            {
                divMarkingAndCuttingPDF.Visible = true;
                divMarkingAndCuttingPDF.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
                divMarkingAndCuttingPDF.Visible = false;
            }

            else if (ProcessName == "FabricationAndWelding")
            {
                divFabricationAndWeldingPDF.Visible = true;
                divFabricationAndWeldingPDF.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
                divFabricationAndWeldingPDF.Visible = false;
            }

            string div = sb.ToString();

            MAXPDFID = objc.GetMaximumNumberPDF();
            string htmlfile = ProcessName + "_" + ViewState["JobOrderID"].ToString() + ".html";
            string pdffile = ProcessName + "_" + ViewState["JobOrderID"].ToString() + ".pdf";
            string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
            string strFile = LetterPath + pdffile;

            string URL = LetterPath + htmlfile;

            SaveHtmlFile(URL, "Job Card Expenses Print Docs", "", div);
            //  GenerateAndSavePDF(LetterPath, strFile, pdffile, URL);

            objc.ReadhtmlFile(htmlfile, hdnpdfContent);

            //divLSERPLogo.Visible = true;

            objc.SavePDFFile("JobCardExpensesSheet.aspx", pdffile, null);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    public void SaveHtmlFile(string URL, string HeaderTitle, string lbltitle, string div)
    {
        try
        {
            DataTable dtAddress = new DataTable();
            dtAddress = (DataTable)ViewState["Address"];

            string Address = dtAddress.Rows[0]["Address"].ToString();
            string PhoneAndFaxNo = dtAddress.Rows[0]["PhoneAndFaxNo"].ToString();
            string Email = dtAddress.Rows[0]["Email"].ToString();
            string WebSite = dtAddress.Rows[0]["WebSite"].ToString();

            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();
            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 2].ToString() + "/" + pagename[pagename.Length - 1].ToString();

            string epstyleurl = url.Replace(Replacevalue, "Assets/css/ep-style.css");
            string style = url.Replace(Replacevalue, "Assets/css/style.css");
            string Print = url.Replace(Replacevalue, "Assets/css/print.css");
            string Main = url.Replace(Replacevalue, "Assets/css/main.css");
            string topstrip = url.Replace(Replacevalue, "Assets/images/topstrrip.jpg");

            StreamWriter w;
            w = File.CreateText(URL);
            w.WriteLine("<html><head><title>");
            w.WriteLine("");
            w.WriteLine("</title>");
            w.WriteLine("<link rel='stylesheet' type='text/css' href='" + epstyleurl + "'/>");
            w.WriteLine("<link rel='stylesheet'  href='" + style + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + Print + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + Main + "' type='text/css'/>");
            w.WriteLine("<style type='text/css'> label { font-weight: bold ! important;color: #000 ! important; }   .col-sm-12.contractorborder { padding-top:5px; border-left: 2px solid;border-right: 2px solid; border-bottom: 2px solid;} .sideheading{ display:flex;align-items:flex-start;justify-content:center;margin-top:15px; } .lbl_h{ width:35%; } .lbl_v{ width:65%; } .Qrcode {height: 60px;width: 80px;margin-right: 10px;padding-left: 20px;} </style>");
            w.WriteLine("</head><body>");

            w.WriteLine("<div class='print-page'>");
            w.WriteLine("<table><thead><tr><td>");
            w.WriteLine("<div class='col-sm-12 header-space' style='text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;height:120px'>");
            w.WriteLine("<div class='header' style='border-bottom:1px solid;'>");
            w.WriteLine("<div>");
            w.WriteLine("<div class='col-sm-12 padding0' id='divLSERPLogo' style='margin:0px auto;display:block;padding:5px 0px;'>");
            //  winprint.document.write("<img src='" + topstrip + "' alt='' height='100px;' style='object-fit:contain;width:100%;display:none'>");
            w.WriteLine("<div class='row'>");
            w.WriteLine("<div class='col-sm-3'>");
            w.WriteLine("<img src='../Assets/images/lonestar.jpeg' alt='lonestar-image' width='90px'>");
            w.WriteLine("</div>");
            w.WriteLine("<div class='col-sm-6 text-center'>");
            w.WriteLine("<h3 style='font-weight:600;font-size:24px;font-style:italic;color:#000;font-family: Arial;display: contents;'>LONESTAR </h3>");
            w.WriteLine("<span style='font-weight:600;font-size:24px ! important;font-family: Times New Roman;'>INDUSTRIES</span>");
            w.WriteLine("<p style='font-weight:500;color:#000;width: 103%;'>" + Address + "</p>");
            w.WriteLine("<p style='font-weight:500;color:#000'>" + PhoneAndFaxNo + "</p>");
            w.WriteLine("<p style='font-weight:500;color:#000'>" + Email + "</p>");
            w.WriteLine("<p style='font-weight:500;color:#000'>" + WebSite + "</p>");
            w.WriteLine("</div>");
            w.WriteLine("<div class='col-sm-3'>");
            w.WriteLine("<img src='../Assets/images/iso.jpeg' alt='lonestar-image' height='90px'>");
            w.WriteLine("</div></div>");
            w.WriteLine("</div></div></div>");
            w.WriteLine("</td></tr></thead>");
            w.WriteLine("<tbody><tr><td>");
            w.WriteLine("<div class='col-sm-12 padding0' style='padding-top:0px;'>");
            w.WriteLine(div);
            w.WriteLine("</div>");
            w.WriteLine("</td></tr></tbody>");
            w.WriteLine("<tfoot class='footer-space'><tr><td>");
            //
            w.WriteLine("<div class='col-sm-12'>");
            // w.WriteLine("<div class='footer' style='border: 0px solid #000 ! important;'>");
            w.WriteLine("<div class='col-sm-12' style='padding-top:50px;'>");
            w.WriteLine("<div class='col-sm-6 p-t-20'><label style='color:black; font-weight:bolder;float:left;'>Quality Incharge</label></div>");
            w.WriteLine("<div class='col-sm-6' style='padding-left:16%;'><label style='color:black; font-weight:bolder;'> Production Incharge</label><img src='" + ViewState["QrCode"].ToString() + "' class='Qrcode' /></div>");
            //  w.WriteLine("</div>");
            w.WriteLine("</div></div>");
            w.WriteLine("</td></tr></tfoot></table>");
            w.WriteLine("</div>");

            //w.WriteLine("<div style='text-align:center;padding-top:10px;font-size:20px;color:#00BCD4;'>");
            //w.WriteLine("</div>");
            //w.WriteLine("<div class='col-sm-12' style='text-align:center;padding-top:10px;font-size:20px;font-weight:bold;'>");
            //w.WriteLine("");
            //w.WriteLine("<div>");
            //w.WriteLine("<div class='col-sm-12' id='divLSERPLogo' style='padding-top: 30px;' runat='server'>");
            //w.WriteLine("<img src='" + topstrip + "' alt='' height='140px;'>");
            //w.WriteLine("</div>");
            //w.WriteLine(div);
            //w.WriteLine("<div>");
            w.WriteLine("</body></html>");
            w.Flush();
            w.Close();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ShowHideControls(string divids)
    {
        divAdd.Visible = divInput.Visible = divAddNew.Visible = divOutput.Visible = false;
        string[] mode = divids.Split(',');
        try
        {
            for (int i = 0; i < mode.Length; i++)
            {
                switch (mode[i].ToLower())
                {
                    case "addnew":
                        divAddNew.Visible = true;
                        break;
                    case "add":
                        divAdd.Visible = true;
                        break;
                    case "view":
                        divOutput.Visible = true;
                        break;
                    case "input":
                        divInput.Visible = true;
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}