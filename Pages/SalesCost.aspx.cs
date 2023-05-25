using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Net;
using eplus.data;
using System.IO;
using eplus.core;
using SelectPdf;

public partial class Pages_SalesCost : System.Web.UI.Page
{
    #region "Declaration"

    DataSet dscostsheet = new DataSet();
    cSession objSession = new cSession();
    cSales _objSales = new cSales();
    cCommon _objc;
    c_HR objHR;
    cMaterials objMat;
    EmailAndSmsAlerts objAlerts;
    cCommon objcommon = new cCommon();
    string DrawingDocumentSavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString();
    string DrawingDocumentHttpPath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString().Trim();

    #endregion

    #region "Page Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        objSession = Master.csSession;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string target = Request["__EVENTTARGET"];
        string arg = Request["__EVENTARGUMENT"];

        if (IsPostBack == false)
        {
            //   btn_previewcostsheet.Visible = false;
            //  btnAccepted.Visible = false;
            //  btnReAproval.Visible = false;
            lbl_waitforaproaval.Visible = false;
            lbl_offermade.Visible = false;
            ddlenquiryload();
            divOutput.Visible = false;
            BindCurrencyMaster();
        }
        else
        {
            if (target == "savecost")
            {
                int enqid = Convert.ToInt32(arg);
            }
            if (target == "CostAccepted")
            {
                CostShare();
            }
        }
    }

    #endregion

    #region "GridView Events"

    protected void gvsalescost_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        DataRow dr;
        DataView dv;
        DataTable dtItemBomCost;
        DataTable dtAddtionalBomCost;
        try
        {

            int index = Convert.ToInt32(e.CommandArgument.ToString());
            ViewState["FileName"] = gvsalescost.DataKeys[index].Values[2].ToString();

            if (e.CommandName == "ViewBOMCost")
            {
                ViewState["DDID"] = gvsalescost.DataKeys[index].Values[1].ToString();

                try
                {
                    var page = HttpContext.Current.CurrentHandler as Page;
                    string url = HttpContext.Current.Request.Url.AbsoluteUri;
                    url = url.ToLower();

                    string[] pagename = url.ToString().Split('/');
                    string Replacevalue = pagename[pagename.Length - 1].ToString();

                    string Page = url.Replace(Replacevalue, "ViewCostingSheet.aspx?DDID=" + ViewState["DDID"].ToString() + "");

                    string s = "window.open('" + Page + "','_blank');";
                    this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
                }
                catch (Exception ex)
                {
                    Log.Message(ex.ToString());
                }

                //objMat.DDID = Convert.ToInt32(gvsalescost.DataKeys[index].Values[1].ToString());
                //ds = objMat.GetViewCostingDetailsByItemID();

                //if (ds.Tables[0].Rows[0]["Message"].ToString() == "RecordsFound")
                //{

                //    gvItemCostingDetails.DataSource = ds.Tables[1];

                //    ds.Tables[1].Columns.Remove("BOMID");
                //    ds.Tables[1].Columns.Remove("ETCID");
                //    ds.Tables[1].Columns.Remove("DDID");
                //    ds.Tables[1].Columns.Remove("MID");
                //    ds.Tables[1].Columns.Remove("BOMStatus");

                //    dv = new DataView(ds.Tables[1]);
                //    dv.RowFilter = "AddtionalPart='No'";
                //    dtItemBomCost = dv.ToTable();

                //    dr = dtItemBomCost.NewRow();
                //    dr["MCOST"] = ds.Tables[2].Rows[0]["MCOST"].ToString();
                //    dr["LCOST"] = ds.Tables[2].Rows[0]["LCOST"].ToString();
                //    if (dtItemBomCost.Columns.Contains("MCRate"))
                //        dr["MCRate"] = ds.Tables[2].Rows[0]["MCRate"].ToString();

                //    dr["AWT"] = ds.Tables[2].Rows[0]["AWT"].ToString();
                //    dr["WT"] = ds.Tables[2].Rows[0]["WT"].ToString();

                //    dtItemBomCost.Rows.InsertAt(dr, dtItemBomCost.Rows.Count + 1);

                //    if (dtItemBomCost.Columns.Contains("MCRate"))
                //        dtItemBomCost.Columns["MCRate"].ColumnName = "M/C RATE";

                //    dtItemBomCost.Columns["DrawingSequenceNumber"].ColumnName = "Part No";

                //    gvItemCostingDetails.DataSource = dtItemBomCost;
                //    gvItemCostingDetails.DataBind();

                //    dtItemBomCost.Columns.Remove("Part No");
                //    dtItemBomCost.Columns["AddtionalPart"].ColumnName = "AP";
                //    gvItemCostingDetails_pdf.DataSource = dtItemBomCost;
                //    gvItemCostingDetails_pdf.DataBind();

                //    lblBOMCost.Text = lblBOMCost_pdf.Text = "BOM Cost : " + ds.Tables[2].Rows[0]["TotalBOMCost"].ToString();


                //    dv = new DataView(ds.Tables[1]);
                //    dv.RowFilter = "AddtionalPart='Yes'";
                //    dtAddtionalBomCost = dv.ToTable();

                //    dr = dtAddtionalBomCost.NewRow();
                //    dr["MCOST"] = ds.Tables[3].Rows[0]["MCOST"].ToString();
                //    dr["LCOST"] = ds.Tables[3].Rows[0]["LCOST"].ToString();
                //    if (dtAddtionalBomCost.Columns.Contains("MCRATE"))
                //        dr["MCRATE"] = ds.Tables[3].Rows[0]["MCRate"].ToString();
                //    dr["AWT"] = ds.Tables[3].Rows[0]["AWT"].ToString();
                //    dr["WT"] = ds.Tables[3].Rows[0]["WT"].ToString();

                //    dtAddtionalBomCost.Rows.InsertAt(dr, dtAddtionalBomCost.Rows.Count + 1);

                //    if (dtAddtionalBomCost.Columns.Contains("MCRATE"))
                //        dtAddtionalBomCost.Columns["MCRate"].ColumnName = "M/C RATE";
                //    dtAddtionalBomCost.Columns["DrawingSequenceNumber"].ColumnName = "Part No";

                //    gvAddtionalPartDetails.DataSource = dtAddtionalBomCost;
                //    gvAddtionalPartDetails.DataBind();

                //    //  gvAddtionalPartDetails_pdf.DataSource = dtAddtionalBomCost;
                //    // gvAddtionalPartDetails_pdf.DataBind();

                //    Decimal TotalCost = Convert.ToDecimal(ds.Tables[2].Rows[0]["TotalBOMCost"].ToString()) + Convert.ToDecimal(ds.Tables[3].Rows[0]["AddtionalPartCost"].ToString());
                //    lblAddtionalPartCost.Text = lblAddtionalPartCost_pdf.Text = "Addtional Part Cost : " + Convert.ToDecimal(ds.Tables[3].Rows[0]["AddtionalPartCost"].ToString());

                //    lblTotalBOMCost.Text = lblTotalBOMCost_pdf.Text = "Total Bom Cost:" + TotalCost;

                //    Label lblItemName = (Label)gvsalescost.Rows[index].FindControl("lblPoint");
                //    LinkButton lbtnDrawingname = (LinkButton)gvsalescost.Rows[index].FindControl("lbtnDeviationFile");
                //    Label lblTagNo = (Label)gvsalescost.Rows[index].FindControl("lblTagNoItemCodeMat");
                //    //Label lblTotalQty = (Label)gvsalescost.Rows[index].FindControl("lblQuantity");
                //    Label txtDeadInventoryRemarks = (Label)gvsalescost.Rows[index].FindControl("lblDeadInventoryRemarks");

                //    string CostEstimatedBy = gvsalescost.DataKeys[index].Values[3].ToString();
                //    string CostEstimatedDate = gvsalescost.DataKeys[index].Values[4].ToString();
                //    string OverAllLength = gvsalescost.DataKeys[index].Values[5].ToString();
                //    string LseNumber = gvsalescost.DataKeys[index].Values[6].ToString();

                //    lblCustomerName_p.Text = lblCustomerName_pdf.Text = ddlCustomerName.SelectedItem.Text;
                //    lblItemname_p.Text = lblItemname_pdf.Text = lblItemName.Text;
                //    lblDrawingname_p.Text = lblDrawingname_pdf.Text = gvsalescost.DataKeys[index].Values[7].ToString();
                //    lblTagNo_p.Text = lblTagNo_pdf.Text = lblTagNo.Text;
                //    lblTotalQty_p.Text = lblTotalQty_pdf.Text = gvsalescost.DataKeys[index].Values[8].ToString();
                //    lblDeadInventoryRemarks_p.Text = lblDeadInventoryRemarks_pdf.Text = txtDeadInventoryRemarks.Text;
                //    lblUserName_p.Text = lblUserName_pdf.Text = CostEstimatedBy;
                //    lblDate_p.Text = lblDate_pdf.Text = CostEstimatedDate;
                //    lblOverAllLength_p.Text = lblOverAllLength_pdf.Text = OverAllLength;
                //    lblLSENumber_p.Text = lblLSENumber_pdf.Text = LseNumber;

                //    cQRcode objQr = new cQRcode();

                //    string QrNumber = objQr.generateQRNumber(9);
                //    string displayQrnumber = objQr.getDisplayQRNumber(QrNumber, 3, '-');

                //    string Code = displayQrnumber + "/" + objSession.employeeid + "/" + DateTime.Now.ToString();
                //    string QrCode = objQr.QRcodeGeneration(Code);

                //    if (QrCode != "")
                //    {
                //        imgQrcode.Attributes.Add("style", "display:block;");
                //        imgQrcode.ImageUrl = QrCode;

                //        //imgQrcode_pdf.Attributes.Add("style", "display:block;");
                //        //imgQrcode_pdf.ImageUrl = QrCode;

                //        objQr.QRNumber = displayQrnumber;
                //        objQr.fileName = "";
                //        objQr.createdBy = objSession.employeeid;
                //        objQr.saveQRNumber();
                //    }
                //    //else
                //    //{
                //    //    imgQrcode.Attributes.Add("style", "display:none;");
                //    //    imgQrcode_pdf.Attributes.Add("style", "display:none;");
                //    //}

                //}
                //else
                //{
                //    gvItemCostingDetails.DataSource = "";
                //    gvItemCostingDetails.DataBind();
                //}

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUP", "ShowBOMCostDetailsPopUp();", true);
            }
            if (e.CommandName == "ViewDeviationFile")
            {
                _objc = new cCommon();

                string FileName = gvsalescost.DataKeys[index].Values[2].ToString();
                string Savepath = DrawingDocumentSavePath;
                string httpPath = DrawingDocumentHttpPath;
                string EnquiryID = ddlEnquiryNumber.SelectedValue;
                _objc.ViewFileName(Savepath, httpPath, FileName, EnquiryID, ifrm);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvsalescost_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (ViewState["CurrencySymbol"].ToString() == "")
                {
                    e.Row.Cells[10].Text = "Unit Price <span style='display:block;'>(INR/Foreign Currency)</span>";
                    e.Row.Cells[14].Text = "Total Price <span style='display:block;'>(INR/Foreign Currency)</span>";
                }
                else
                {
                    e.Row.Cells[10].Text = "Unit Price <span style='display:block;'>(" + ViewState["CurrencySymbol"].ToString() + ")</span>";
                    e.Row.Cells[14].Text = "Total Price <span style='display:block;'>(" + ViewState["CurrencySymbol"].ToString() + ")</span>";
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtMarketingOverHead = (TextBox)e.Row.FindControl("txtMarketingOverHead");
                TextBox txtPackagingCost = (TextBox)e.Row.FindControl("txtPackagingCost");
                TextBox txtRecommendedCost = (TextBox)e.Row.FindControl("txtRecommendedCost");

                Label lblmarketingOverHeadCost = (Label)e.Row.FindControl("lblmarketingOverHeadCost");
                Label lblPackagingCosthead = (Label)e.Row.FindControl("lblPackagingCosthead");
                Label lblrevisedreason = (Label)e.Row.FindControl("lbl_revisedreason");

                lblrevisedreason.Text = dr["Revisedreason"].ToString();
                txtMarketingOverHead.Text = dr["MarketingCostPercentage"].ToString();
                txtPackagingCost.Text = dr["PackagingPercentage"].ToString();
                txtRecommendedCost.Text = dr["Recommendedcost"].ToString();

                lblmarketingOverHeadCost.Text = dr["MarketingCost"].ToString();
                lblPackagingCosthead.Text = dr["PackagingCost"].ToString();

                if (ViewState["offerDispatched"].ToString() == "0")
                {
                    if (dr["SalesHApproval"].ToString() == "7" || dr["SalesHApproval"].ToString() == "1")
                    {
                        btnAccepted.CssClass = "btn btn-cons btn-success btnAproval aspNetDisabled";
                        btn_previewcostsheet.CssClass = "btn btn-cons btn-success btnAproval aspNetDisabled";
                    }
                    else if (string.IsNullOrEmpty(dr["SalesHApproval"].ToString()))
                    {
                        btnAccepted.CssClass = "btn btn-cons btn-success btnAproval aspNetDisabled";
                        btn_previewcostsheet.CssClass = "btn btn-cons btn-success btnAproval";
                    }
                    else if (dr["SalesHApproval"].ToString() == "9")
                    {
                        btnAccepted.CssClass = "btn btn-cons btn-success btnAproval";
                        btn_previewcostsheet.CssClass = "btn btn-cons btn-success btnAproval";
                    }
                }
                else
                {
                    if (ViewState["offerDispatched"].ToString() == "1")
                    {
                        if (dr["SalesHApproval"].ToString() == "1")
                        {
                            btnAccepted.CssClass = "btn btn-cons btn-success btnAproval aspNetDisabled";
                            btn_previewcostsheet.CssClass = "btn btn-cons btn-success btnAproval";
                        }
                        if (dr["SalesHApproval"].ToString() == "7")
                        {
                            btnAccepted.CssClass = "btn btn-cons btn-success btnAproval aspNetDisabled";
                            btn_previewcostsheet.CssClass = "btn btn-cons btn-success btnAproval aspNetDisabled";
                        }
                        else if (dr["SalesHApproval"].ToString() == "9")
                        {
                            btnAccepted.CssClass = "btn btn-cons btn-success btnAproval";
                            btn_previewcostsheet.CssClass = "btn btn-cons btn-success btnAproval";
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
    }

    protected void gvItemCostingDetails_pdf_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Attributes.Add("style", "width:5%;");
                e.Row.Cells[1].Attributes.Add("style", "width:5%;");
                e.Row.Cells[23].Attributes.Add("style", "width:6%;");
                e.Row.Cells[16].Attributes.Add("style", "width:24px;");
                e.Row.Cells[2].Attributes.Add("style", "width:16px;");

                e.Row.Cells[7].Attributes.Add("style", "width:22px;");
                e.Row.Cells[8].Attributes.Add("style", "width:23px;");

            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.RowIndex == Convert.ToInt32(ViewState["RowCount"].ToString()))
                    e.Row.Attributes.Add("style", "color:white;");
                e.Row.Cells[23].Attributes.Add("style", "width:6%;");
                e.Row.Cells[16].Attributes.Add("style", "width:24px;");
                e.Row.Cells[2].Attributes.Add("style", "width:16px;");

                e.Row.Cells[7].Attributes.Add("style", "width:22px;");
                e.Row.Cells[8].Attributes.Add("style", "width:23px;");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvItemCostingDetails_pdf_OnDataBound(object sender, EventArgs e)
    {
        try
        {
            GridViewRow row = gvItemCostingDetails_pdf.Rows[gvItemCostingDetails_pdf.Rows.Count - 1];
            for (int j = 1; j < row.Cells.Count - 1; j++)
            {
                if (row.Cells[j].Text.Replace("&nbsp;", "") != "")
                {
                    row.Cells[j - 1].Text = row.Cells[j].Text;
                    row.Cells[j - 1].ColumnSpan = 2;
                    row.Cells[j - 1].Attributes.Add("style", "text-align:end;");
                    row.Cells[j].Visible = false;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region "Button Events"

    protected void btn_saveclick(object sender, EventArgs e)
    {
        try
        {
            //_objSales.DDID = Convert.ToInt32(hdn_ddid.Value);
            //_objSales.SalesItemid = Convert.ToInt32(hdn_itemid.Value);
            //if (txt_prodoverhead.Text != "") _objSales.ProductionOverhead = Convert.ToInt32(txt_prodoverhead.Text);
            //if (txt_conscost.Text != "") _objSales.ConsumableCost = Convert.ToInt32(txt_conscost.Text);
            //if (txt_packcost.Text != "") _objSales.PackagingCost = Convert.ToInt32(txt_packcost.Text);
            //if (txt_makoverhead.Text != "") _objSales.MakingOverhead = Convert.ToInt32(txt_makoverhead.Text);
            //if (txt_misc.Text != "") _objSales.MiscExpense = Convert.ToInt32(txt_misc.Text);
            //if (hdn_TotalCost.Value != "") _objSales.Totalcost = Convert.ToInt32(hdn_TotalCost.Value);
            //if (txt_reccost.Text != "") _objSales.Recommendedcost = Convert.ToInt32(txt_reccost.Text);
            _objSales.saveSalesCost();
            bindcostsheet(Convert.ToInt32(ddlEnquiryNumber.SelectedValue));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "updatetotalcost();", true);
        }
        catch (Exception ec)
        {
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Hide Loader", "hideLoader();", true);
    }

    protected void btnAproval_Click(object sender, EventArgs e)
    {
        try
        {

            string[] strMarket = hdnMarket.Value.Split(',');
            string[] strPackage = hdnPackag.Value.Split(',');
            string[] strRecommanded = hdnRecommandedCost.Value.Split(',');
            string[] strDDIDQtyCost = hdnDDIDCostQty.Value.Split(',');
            string[] strUnitCostForignCurrency = hdnUnitCostForignCurrency.Value.Split(',');
            int i = 0;

            foreach (GridViewRow row in gvsalescost.Rows)
            {
                int DDID = Convert.ToInt32(strDDIDQtyCost[i].Split('_')[0].ToString());
                decimal AddtionalPartCost = Convert.ToDecimal(strDDIDQtyCost[i].Split('_')[1].ToString());
                decimal IssuePartCost = Convert.ToDecimal(strDDIDQtyCost[i].Split('_')[2].ToString());
                int ItemQty = Convert.ToInt32(strDDIDQtyCost[i].Split('_')[3].ToString());

                decimal marketingCostPercentage = Convert.ToDecimal(strMarket[i].ToString().Split('_')[0]);
                decimal marketingCost = Convert.ToDecimal(strMarket[i].ToString().Split('_')[1]);

                decimal PackagingPer = Convert.ToDecimal(strPackage[i].ToString().Split('_')[0]);
                decimal packagingCost = Convert.ToDecimal(strPackage[i].ToString().Split('_')[1]);

                decimal RecommendedCost = Convert.ToDecimal(strRecommanded[i].ToString());
                decimal UnitCostForignCurrency = Convert.ToDecimal(strUnitCostForignCurrency[i].ToString());

                Label lblProductid = (Label)row.FindControl("lblProductid");
                _objSales.SalesItemid = Convert.ToInt32(lblProductid.Text);

                _objSales.MarketingCostPercentage = marketingCostPercentage;
                _objSales.MarketingCost = marketingCost;
                _objSales.PackagingPercentage = PackagingPer;
                _objSales.PackagingCost = packagingCost;
                _objSales.unitcostForignCurrency = UnitCostForignCurrency;

                _objSales.UnitCost = Convert.ToDecimal(AddtionalPartCost + marketingCost + packagingCost - IssuePartCost);
                _objSales.Totalcost = Convert.ToDecimal(ItemQty * (AddtionalPartCost + marketingCost + packagingCost - IssuePartCost));

                _objSales.CID = Convert.ToInt32(ddlCurrency.SelectedValue.Split('/')[0]);
                _objSales.INRvalue = Convert.ToDecimal(ddlCurrency.SelectedValue.Split('/')[1]);

                _objSales.SalesCostNote = txtNote.Text;

                _objSales.Recommendedcost = RecommendedCost;

                _objSales.DDID = Convert.ToInt32(DDID);
                _objSales.CostVersion = Convert.ToInt32(ViewState["CostRevision"].ToString());
                _objSales.ItemQty = ItemQty.ToString();
                _objSales.PageFlag = "SC";
                ////_objSales.UpdateSalesCost();
                _objSales.saveSalesCost();
                i++;
            }
            bindcostsheet(Convert.ToInt32(ddlEnquiryNumber.SelectedValue));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Approval Request Submitted successfully');btnCalculateCost();", true);
            //DataSet dsEmail = objHR.GetEmployeeCommunicationDetailsEmployeeID(Convert.ToInt32(ViewState["EmployeeID"].ToString()));

            //if (dsEmail.Tables[0].Rows.Count > 0)
            //{
            //    if (dsEmail.Tables[0].Rows[0]["Email"].ToString() != "")
            //    {
            //        objAlerts.MessageID = 1;

            //        DataSet dsMessage = objAlerts.GetAutomatedMessageMail();
            //        str Message = dsMessage.Tables[0].Rows[0]["Message"].ToString();
            //        Message = Message.Replace("<EmployeeName>", lblSalesAgent.Text);
            //        Message = Message.Replace("<Version>", ViewState["NewDrawingVersionNumber"].ToString());
            //        Message = Message.Replace("<EnquiryNumber>", ddlEnquiryNumber.SelectedValue);
            //        Message = Message.Replace("<OrderNumber>", lblCustomerOrderNumber.Text);

            //        objAlerts.AttachementID = Convert.ToInt32(ds.Tables[0].Rows[0]["AttachementID"].ToString());

            //        objAlerts.file = BaseHttpPath + AttachmentName;

            //        objAlerts.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue);
            //        objAlerts.AlertType = "Mail";

            //        objAlerts.reciverID = dsEmail.Tables[0].Rows[0]["EmployeeID"].ToString();
            //        objAlerts.reciverType = "I";
            //        objAlerts.GroupID = 0;
            //        objAlerts.dtSettings = objAlerts.GetEmailSettings();
            //        objAlerts.EmailID = dsEmail.Tables[0].Rows[0]["Email"].ToString(); //dsEmail.Tables[0].Rows[0]["Email"].ToString();//"karthik@innovasphere.in";
            //        objAlerts.Subject = "Drawing Attachements";
            //        objAlerts.Message = Message;
            //        objAlerts.userID = objSession.employeeid;

            //        //send Mail
            //        objAlerts.SendIndividualMail();

            //        //save Alert Details
            //        objAlerts.Message = null;
            //        objAlerts.SaveAlertDetails();
            //    }
            //}            
        }
        catch (Exception ec)
        {
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Hide Loader", "hideLoader();", true);
    }

    protected void btnAccepted_Click(object sender, EventArgs e)
    {

    }

    //protected void imgViewDrawing_Click(object sender, EventArgs e)
    //{
    //    string BaseHttpPath = "";
    //    try
    //    {
    //        BaseHttpPath = CustomerEnquiryHttpPath + ddlenquiry.SelectedValue + "/" + lbl_drawingname.Text;
    //        imgDocs.ImageUrl = BaseHttpPath;

    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowViewPopUp();", true);
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    protected void btndownloaddocs_Click(object sender, EventArgs e)
    {
        GeneratePDDF();
    }

    protected void btnViewDrawingFile(object sender, EventArgs e)
    {
        ViewDrawingFilename();
    }

    #endregion

    #region"Radio Events"

    protected void rblenquirychange_OnSelectedChanged(object sender, EventArgs e)
    {
        try
        {
            ddlenquiryload();
            divOutput.Visible = false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region Dropdown Methods

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
                objcommon.enquiryddlchange(ddlEnquiryNumber, ddlCustomerName, (DataTable)ViewState["CustomerDetails"], (DataTable)ViewState["EnquiryDetails"]);
                bindcostsheet(Convert.ToInt32(ddlEnquiryNumber.SelectedValue));
                divOutput.Visible = true;

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

    #region Common Methods

    private void BindCurrencyMaster()
    {
        _objSales = new cSales();
        DataSet ds = new DataSet();
        DataTable dt;
        DataView dv;
        try
        {
            ds = _objSales.CurrencyDetails(0, ddlCurrency);

            dt = (DataTable)ds.Tables[0];

            ViewState["CurrencyMaster"] = dt;
            dv = new DataView(dt);

            dv.RowFilter = "CurrencySymbol IN ('INR')";
            dt = dv.ToTable();

            ddlCurrency.SelectedValue = dt.Rows[0]["INRValue"].ToString();
        }
        catch (Exception ec)
        {
            Log.Message(ec.ToString());
        }
    }

    private void ddlenquiryload()
    {
        try
        {
            DataSet dsEnquiryNumber = new DataSet();
            DataSet dsCustomer = new DataSet();
            //dsCustomer = _objSales.getCustomerNameForSalesCost(Convert.ToInt32(objSession.employeeid), ddlCustomerName);
            dsCustomer = objcommon.getCustomerNameByUserIDAndType(Convert.ToInt32(objSession.employeeid), ddlCustomerName, "LS_GetCustomerNameByUserIDForSalesCostPage", rblenquirychange.SelectedValue);
            ViewState["CustomerDetails"] = dsCustomer.Tables[0];
            dsEnquiryNumber = objcommon.GetEnquiryNumberByUserIDAndType(Convert.ToInt32(objSession.employeeid), ddlEnquiryNumber, "LS_GetEnquiryNumberByUserIDForSalesCostPage", rblenquirychange.SelectedValue);
            //dsEnquiryNumber = _objSales.GetEnquiryNumberForSalesCost(Convert.ToInt32(objSession.employeeid), ddlEnquiryNumber);
            ViewState["EnquiryDetails"] = dsEnquiryNumber.Tables[0];
        }
        catch (Exception ec)
        {
            Log.Message(ec.ToString());
        }
    }

    private void bindcostsheet(int enqid)
    {
        DataTable dt;
        DataView dv;
        try
        {
            //   btnAccepted.Visible = false;
            //    btnReAproval.Visible = false;
            //   btn_previewcostsheet.Visible = false;
            lbl_waitforaproaval.Visible = false;
            lbl_offermade.Visible = false;
            dscostsheet = _objSales.getCostsheet(enqid, "LS_GetTCostsheet");
            ViewState["DataTable"] = dscostsheet.Tables[0];
            if (dscostsheet.Tables[0].Rows.Count > 0)
            {
                txtNote.Text = dscostsheet.Tables[0].Rows[0]["Note"].ToString();
                if (dscostsheet.Tables[0].Rows[0]["SalesCostStatus"].ToString() == "Completed" && dscostsheet.Tables[0].Rows[0]["Finalcost"].ToString() != "")
                {
                    gvsalescost.Columns[6].Visible = false;
                    gvsalescost.Columns[7].Visible = false;
                    //  btnAccepted.Visible = false;
                    //   btnReAproval.Visible = false;
                    //   btn_previewcostsheet.Visible = false;
                    if (dscostsheet.Tables[0].Rows[0]["Approvalstatus"].ToString() == "Not Verified")
                    {
                        lbl_waitforaproaval.Visible = true;

                    }
                    else
                    {
                        lbl_offermade.Visible = true;
                        gvsalescost.Columns[6].Visible = true;
                        gvsalescost.Columns[7].Visible = true;
                    }
                }
                else
                {
                    if (dscostsheet.Tables[0].Rows[0]["SalesHApproval"].ToString() != "")
                    {
                        //btnAccepted.Visible = true;
                        //    btnReAproval.Visible = true;
                        //  btn_previewcostsheet.Visible = false;
                    }
                    else
                    {
                        //  btnAccepted.Visible = false;
                        //    btnReAproval.Visible = false;
                        //   btn_previewcostsheet.Visible = true;
                    }
                    gvsalescost.Columns[6].Visible = true;
                    gvsalescost.Columns[7].Visible = true;
                    lbl_waitforaproaval.Visible = false;
                    lbl_offermade.Visible = false;
                }

                ViewState["offerDispatched"] = dscostsheet.Tables[1].Rows[0]["offerDispatched"].ToString();
                ViewState["CostRevision"] = dscostsheet.Tables[1].Rows[0]["CostRevision"].ToString();
                ViewState["EODID"] = dscostsheet.Tables[1].Rows[0]["EODID"].ToString();

                //                if (string.IsNullOrEmpty(dscostsheet.Tables[0].Rows[0]["CurrencySymbol"].ToString().Trim()))
                //                  ddlCurrency.SelectedIndex = 0;

                ViewState["CurrencySymbol"] = dscostsheet.Tables[0].Rows[0]["CurrencySymbol"].ToString();

                gvsalescost.DataSource = dscostsheet.Tables[0];
                gvsalescost.DataBind();

                if (ViewState["offerDispatched"].ToString() == "1")
                    lblOfferVersionNote.Text = "New Version Of Offer '" + ViewState["CostRevision"].ToString() + "'";
                else
                    lblOfferVersionNote.CssClass = "";

                dt = (DataTable)ViewState["CurrencyMaster"];
                dv = new DataView(dt);
                dv.RowFilter = "CurrencySymbol IN ('" + ViewState["CurrencySymbol"].ToString() + "')";
                dt = dv.ToTable();

                ddlCurrency.SelectedValue = dt.Rows[0]["INRValue"].ToString();

                lblCurrencyName.Text = "INR" + " : " + dt.Rows[0]["INRValue"].ToString().Split('/')[1].ToString();
            }
            else
            {
                gvsalescost.DataSource = "";
                gvsalescost.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Hide Loader", "InfoMessage('Information','Customer Not Approved for this Enquiry')", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
        finally
        {

        }
    }

    //protected void btnviewcost_Click(object sender, EventArgs e)
    //{
    //    DataSet ds = new DataSet();
    //    objMat = new cMaterials();
    //    DataRow dr;
    //    try
    //    {
    //        objMat.DDID = Convert.ToInt32(hdn_ddid.Value);
    //        ds = objMat.GetViewCostingDetailsByItemID();

    //        if (ds.Tables[0].Rows[0]["Message"].ToString() == "RecordsFound")
    //        {
    //            gvItemCostingDetails.DataSource = ds.Tables[1];

    //            ds.Tables[1].Columns.Remove("ETCID");
    //            ds.Tables[1].Columns.Remove("DDID");
    //            ds.Tables[1].Columns.Remove("MID");
    //            ds.Tables[1].Columns.Remove("AddtionalPart");
    //            ds.Tables[1].Columns.Remove("BOMStatus");

    //            dr = ds.Tables[1].NewRow();
    //            dr["MCOST"] = ds.Tables[2].Rows[0]["MCOST"].ToString();
    //            dr["LCOST"] = ds.Tables[3].Rows[0]["LCOST"].ToString();
    //            ds.Tables[1].Rows.Add(dr);

    //            lblTotalBOMCost.Text = ds.Tables[4].Rows[0]["TotalBOMCost"].ToString();
    //        }
    //        else
    //            gvItemCostingDetails.DataSource = "";

    //        gvItemCostingDetails.DataBind();
    //        bomdiv.Style.Add("display", "block");

    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUP", "ShowViewPopUp();", true);
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    private void GeneratePDDF()
    {
        DataSet ds = new DataSet();
        objcommon = new cCommon();
        try
        {
            StringBuilder sbCosting = new StringBuilder();

            divViewCostingPrint_pdf.RenderControl(new HtmlTextWriter(new StringWriter(sbCosting)));

            string htmlfile = "ViewCosting_" + ViewState["DDID"].ToString() + ".html";
            string pdffile = "ViewCosting_" + ViewState["DDID"].ToString() + ".pdf";
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

            // SaveHtmlFile(htmlfileURL, Main, epstyleurl, style, Print, topstrip, sbCosting.ToString());

            cQRcode objQr = new cQRcode();

            string QrNumber = objQr.generateQRNumber(9);
            string displayQrnumber = objQr.getDisplayQRNumber(QrNumber, 3, '-');

            string Code = displayQrnumber + "/" + objSession.employeeid + "/" + DateTime.Now.ToString();
            string QrCode = objQr.QRcodeGeneration(Code);

            if (QrCode != "")
            {
                imgQrcode.Attributes.Add("style", "display:block;");
                imgQrcode.ImageUrl = QrCode;

                //imgQrcode_pdf.Attributes.Add("style", "display:block;");
                //imgQrcode_pdf.ImageUrl = QrCode;

                objQr.QRNumber = displayQrnumber;
                objQr.fileName = "";
                objQr.createdBy = objSession.employeeid;
                objQr.saveQRNumber();
            }
            //else
            //{
            //    imgQrcode.Attributes.Add("style", "display:none;");
            //    imgQrcode_pdf.Attributes.Add("style", "display:none;");
            //}

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Print", "ViewCostingprint('" + epstyleurl + "','" + Main + "','" + QrCode + "');", true);

            //GenerateAndSavePDF(LetterPath, pdfFileURL, pdffile, htmlfileURL);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    public void SaveHtmlFile(string URL, string Main, string epstyleurl, string style, string Print, string topstrip, string div)
    {
        try
        {
            StreamWriter w;
            w = File.CreateText(URL);
            w.WriteLine("<html><head><title>");
            w.WriteLine("</title>");
            w.WriteLine("<link rel='stylesheet' type='text/css' href='" + epstyleurl + "'/>");
            w.WriteLine("<link rel='stylesheet' href='" + style + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + Main + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + Print + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + topstrip + "' type='text/css'/>");

            w.WriteLine("</head><body>");
            w.WriteLine("<div class='col-sm-12' style='padding-top:10px;padding-left:10px !important;margin-right:10px !important;'>");
            w.WriteLine(div);
            w.WriteLine("<div>");
            w.WriteLine("</body></html>");
            w.Flush();
            w.Close();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void GenerateAndSavePDF(string LetterPath, string strFile, string pdffile, string URL)
    {
        HtmlToPdf converter = new HtmlToPdf();
        PdfDocument doc = new PdfDocument();
        try
        {
            if (!Directory.Exists(LetterPath))
                Directory.CreateDirectory(LetterPath);
            if (File.Exists(strFile))
                File.Delete(strFile);

            converter.Options.PdfPageSize = PdfPageSize.A3;
            converter.Options.PdfPageOrientation = PdfPageOrientation.Landscape;
            converter.Options.MarginLeft = 0;
            converter.Options.MarginRight = 0;
            converter.Options.MarginTop = 0;
            converter.Options.MarginBottom = 0;
            converter.Options.WebPageWidth = 700;
            converter.Options.WebPageHeight = 0;
            converter.Options.WebPageFixedSize = false;

            doc = converter.ConvertUrl(URL);
            doc.Save(strFile);

            var ms = new System.IO.MemoryStream();

            HttpContext.Current.Response.ContentType = "Application/pdf";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + pdffile);
            HttpContext.Current.Response.TransmitFile(strFile);
            HttpContext.Current.ApplicationInstance.CompleteRequest();

            doc.Close();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        finally
        {
            converter = null;
            doc = null;
            LetterPath = null;
            strFile = null;
            URL = null;
        }
    }

    private void ViewDrawingFilename()
    {
        try
        {
            string DrawingDocumentSavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString().Trim();
            string DrawingDocumentHttpPath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString().Trim();

            string Savepath = DrawingDocumentSavePath;
            string httpPath = DrawingDocumentHttpPath;
            string EnquiryID = ddlEnquiryNumber.SelectedValue;

            ifrm.Attributes.Add("src", httpPath + EnquiryID + "/" + ViewState["FileName"].ToString());
            string imgname = DrawingDocumentSavePath + ddlEnquiryNumber.SelectedValue + "\\" + ViewState["FileName"].ToString();
            if (File.Exists(imgname))
            {
                ViewState["ifrmsrc"] = httpPath + ViewState["FileName"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewdocsPopUp();", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Drawing Not Found');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void CostShare()
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        try
        {
            DataRow dr;
            dt.Columns.Add("DDID");
            dt.Columns.Add("EnquiryNumber");
            dt.Columns.Add("FinalCost");
            foreach (GridViewRow row in gvsalescost.Rows)
            {
                dr = dt.NewRow();
                Label lblDDID = (Label)row.FindControl("lblDDID");
                dr["DDID"] = Convert.ToInt32(lblDDID.Text);
                dr["EnquiryNumber"] = ddlEnquiryNumber.SelectedValue;
                Label lbl_Approvedcost = (Label)row.FindControl("lbl_Approvedcost");
                dr["FinalCost"] = Convert.ToDecimal(lbl_Approvedcost.Text);

                dt.Rows.Add(dr);
            }
            _objSales.dt = dt;
            _objSales.CostVersion = Convert.ToInt32(ViewState["CostRevision"].ToString());
            _objSales.CreatedBy = objSession.employeeid;

            ds = _objSales.UpdateOfferCost();
            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Accepted")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Cost Accepted');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
        }
        catch (Exception ec)
        {

        }
        bindcostsheet(Convert.ToInt32(ddlEnquiryNumber.SelectedValue));
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Hide Loader", "hideLoader();", true);
    }

    #endregion

    #region"PageLoad Complete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvsalescost.Rows.Count > 0)
            gvsalescost.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (gvItemCostingDetails.Rows.Count > 0)
            gvItemCostingDetails.HeaderRow.TableSection = TableRowSection.TableHeader;

        if (gvAddtionalPartDetails.Rows.Count > 0)
            gvAddtionalPartDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}