using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;
using System.Configuration;
using System.IO;
using System.Text;
using SelectPdf;

public partial class Pages_ViewCosting : System.Web.UI.Page
{
    #region"Declaration"

    cMaterials objMat;
    cDesign objDesign;
    cSession objSession;
    cCommon objc;

    #endregion

    #region"PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            objSession = Master.csSession;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];
            if (!IsPostBack)
            {
                ddlEnquiryLoad();
                divOutput.Visible = false;
                //objc = new cCommon();
                //DataSet dsEnquiryNumber = new DataSet();
                //DataSet dsCustomer = new DataSet();
                //dsCustomer = objc.getCustomerNameByUserID(Convert.ToInt32(objSession.employeeid), ddlCustomerName, "LS_GetCustomerNameByEmployeeID");
                //ViewState["CustomerDetails"] = dsCustomer.Tables[1];
                //dsEnquiryNumber = objc.GetEnquiryNumberByUserID(Convert.ToInt32(objSession.employeeid), ddlEnquiryNumber, "LS_GetEnquiryIDByUserID");
                //ViewState["EnquiryDetails"] = dsEnquiryNumber.Tables[0];
            }
            if (target == "updateSharedWithHODStatus")
                UpdateSharedWithHODStatus();
            if (target == "ViewDrawing")
            {
                int index = Convert.ToInt32(arg.ToString());
                ViewState["FileName"] = gvViewCosting.DataKeys[index].Values[1].ToString();
                ViewDrawingFilename();
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
        ddlEnquiryLoad();
        divOutput.Visible = false;
    }

    #endregion

    #region"DropDown Events"

    protected void ddlCustomerName_SelectIndexChanged(object sender, EventArgs e)
    {
        objc = new cCommon();
        DataView dv;
        DataTable dcustomr = new DataTable();
        DataTable denquiry = new DataTable();
        try
        {
            dcustomr = (DataTable)ViewState["CustomerDetails"];
            denquiry = (DataTable)ViewState["EnquiryDetails"];

            if (ddlCustomerName.SelectedIndex > 0)
            {
                dv = new DataView(denquiry);
                dv.RowFilter = "ProspectID='" + ddlCustomerName.SelectedValue + "'";
                dcustomr = dv.ToTable();

                ddlEnquiryNumber.DataSource = dcustomr;
                ddlEnquiryNumber.DataTextField = "EnquiryNumber";
                ddlEnquiryNumber.DataValueField = "EnquiryID";
                ddlEnquiryNumber.DataBind();
            }
            else
            {
                ddlEnquiryNumber.DataSource = denquiry;
                ddlEnquiryNumber.DataTextField = "EnquiryNumber";
                ddlEnquiryNumber.DataValueField = "EnquiryID";
                ddlEnquiryNumber.DataBind();
            }

            ddlEnquiryNumber.Items.Insert(0, new ListItem("--Select--", "0"));

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
        objMat = new cMaterials();
        DataSet ds = new DataSet();
        DataTable dtEnquiry;
        try
        {
            if (ddlEnquiryNumber.SelectedIndex > 0)
            {
                divOutput.Visible = true;

                //objc.enquiryddlchange(ddlEnquiryNumber, ddlCustomerName, (DataTable)ViewState["CustomerDetails"], (DataTable)ViewState["EnquiryDetails"]);

                dtEnquiry = (DataTable)ViewState["EnquiryDetails"];
                if (ddlEnquiryNumber.SelectedIndex > 0)
                {
                    DataView dv = new DataView(dtEnquiry);
                    dv.RowFilter = "EnquiryID='" + ddlEnquiryNumber.SelectedValue + "'";
                    dtEnquiry = dv.ToTable();
                    ddlCustomerName.SelectedValue = dtEnquiry.Rows[0]["ProspectID"].ToString();
                }

                objMat.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue);

                ds = objMat.GetViewCostingDetails();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    btnShareBOM.Visible = true;
                    gvViewCosting.DataSource = ds.Tables[0];
                    gvViewCosting.DataBind();
                }
                else
                {
                    btnShareBOM.Visible = false;
                    gvViewCosting.DataSource = "";
                    gvViewCosting.DataBind();
                }

                //if (ds.Tables[0].Rows[0]["BOMStatus"].ToString() == "Completed")
                //{
                //    btnShareBOM.Enabled = false;
                //    btnShareBOM.ToolTip = "Enquiry Moved Into Next Stage";
                //}
            }
            else
            {
                btnShareBOM.Visible = false;
                divOutput.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnShareBOM_Click(object sender, EventArgs e)
    {
        int count = 0;
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            for (int i = 0; i < gvViewCosting.Rows.Count; i++)
            {
                Label lblCostEstimationStatus = (Label)gvViewCosting.Rows[i].FindControl("lblCostEstimationStatus");
                if (lblCostEstimationStatus.Text == "Completed")
                    count++;
            }
            if (count != gvViewCosting.Rows.Count)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "InfoMessage('Information','all item should have Complete BOM Cost Estimation');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "BOMStatus", "UpdateSharedWithHODStatus();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnViewDrawingFile(object sender, EventArgs e)
    {
        ViewDrawingFilename();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUP", "ShowViewPopUP();", true);
    }

    protected void btndownloaddocs_Click(object sender, EventArgs e)
    {
        GeneratePDDF();
    }

    #endregion

    #region"GridView Events"

    protected void gvViewCosting_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        DataRow dr;
        DataView dv;
        DataTable dtItemBomCost;
        DataTable dtAddtionalBomCost;
        DataTable dtIssueBomCost;
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            ViewState["FileName"] = gvViewCosting.DataKeys[index].Values[1].ToString();

            if (e.CommandName == "ViewCosting")
            {
                ViewState["DDID"] = gvViewCosting.DataKeys[index].Values[0].ToString();
                objMat.DDID = Convert.ToInt32(gvViewCosting.DataKeys[index].Values[0].ToString());
                ds = objMat.GetViewCostingDetailsByItemID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "RecordsFound")
                {

                    gvItemCostingDetails.DataSource = ds.Tables[1];

                    ds.Tables[1].Columns.Remove("BOMID");
                    ds.Tables[1].Columns.Remove("ETCID");
                    ds.Tables[1].Columns.Remove("DDID");
                    ds.Tables[1].Columns.Remove("MID");
                    ds.Tables[1].Columns.Remove("BOMStatus");
                    //  ds.Tables[1].Columns.Remove("AddtionalPart");

                    dv = new DataView(ds.Tables[1]);
                    dv.RowFilter = "AddtionalPart='No'";
                    dtItemBomCost = dv.ToTable();

                    dr = dtItemBomCost.NewRow();
                    dr["MCOST"] = ds.Tables[2].Rows[0]["MCOST"].ToString();
                    dr["LCOST"] = ds.Tables[2].Rows[0]["LCOST"].ToString();
                    if (dtItemBomCost.Columns.Contains("MCRate"))
                        dr["MCRate"] = ds.Tables[2].Rows[0]["MCRate"].ToString();

                    dr["AWT"] = ds.Tables[2].Rows[0]["AWT"].ToString();
                    dr["WT"] = ds.Tables[2].Rows[0]["WT"].ToString();

                    dtItemBomCost.Rows.InsertAt(dr, dtItemBomCost.Rows.Count + 1);

                    if (dtItemBomCost.Columns.Contains("MCRate"))
                        dtItemBomCost.Columns["MCRate"].ColumnName = "M/C RATE";

                    dtItemBomCost.Columns["DrawingSequenceNumber"].ColumnName = "Part No";

                    dtItemBomCost.Columns.Remove("AddtionalPart");

                    gvItemCostingDetails.DataSource = dtItemBomCost;
                    gvItemCostingDetails.DataBind();

                    lblBOMCost.Text = lblBOMCost_pdf.Text = "BOM Cost : " + ds.Tables[2].Rows[0]["TotalBOMCost"].ToString();
                    //lblBOMCost_pdf.Text =

                    //FIM Part

                    dv = new DataView(ds.Tables[1]);
                    dv.RowFilter = "AddtionalPart='Issue'";
                    dtIssueBomCost = dv.ToTable();

                    dr = dtIssueBomCost.NewRow();
                    dr["MCOST"] = ds.Tables[4].Rows[0]["MCOST"].ToString();
                    dr["LCOST"] = ds.Tables[4].Rows[0]["LCOST"].ToString();
                    if (dtIssueBomCost.Columns.Contains("MCRATE"))
                        dr["MCRATE"] = ds.Tables[4].Rows[0]["MCRate"].ToString();
                    dr["AWT"] = ds.Tables[4].Rows[0]["AWT"].ToString();
                    dr["WT"] = ds.Tables[4].Rows[0]["WT"].ToString();

                    dtIssueBomCost.Rows.InsertAt(dr, dtIssueBomCost.Rows.Count + 1);

                    if (dtIssueBomCost.Columns.Contains("MCRATE"))
                        dtIssueBomCost.Columns["MCRate"].ColumnName = "M/C RATE";
                    dtIssueBomCost.Columns["DrawingSequenceNumber"].ColumnName = "Part No";

                    dtIssueBomCost.Columns.Remove("AddtionalPart");

                    gvIssuePartDetails.DataSource = dtIssueBomCost;
                    gvIssuePartDetails.DataBind();
                    lblIssuePartCost.Text = lblIssuePartCost_pdf.Text = "FIM Part Cost : " + Convert.ToDecimal(ds.Tables[4].Rows[0]["IssuePartCost"].ToString());

                    //End Code

                    dv = new DataView(ds.Tables[1]);
                    dv.RowFilter = "AddtionalPart='Yes'";
                    dtAddtionalBomCost = dv.ToTable();

                    dr = dtAddtionalBomCost.NewRow();
                    dr["MCOST"] = ds.Tables[3].Rows[0]["MCOST"].ToString();
                    dr["LCOST"] = ds.Tables[3].Rows[0]["LCOST"].ToString();
                    if (dtAddtionalBomCost.Columns.Contains("MCRATE"))
                        dr["MCRATE"] = ds.Tables[3].Rows[0]["MCRate"].ToString();
                    dr["AWT"] = ds.Tables[3].Rows[0]["AWT"].ToString();
                    dr["WT"] = ds.Tables[3].Rows[0]["WT"].ToString();

                    dtAddtionalBomCost.Rows.InsertAt(dr, dtAddtionalBomCost.Rows.Count + 1);

                    if (dtAddtionalBomCost.Columns.Contains("MCRATE"))
                        dtAddtionalBomCost.Columns["MCRate"].ColumnName = "M/C RATE";
                    dtAddtionalBomCost.Columns["DrawingSequenceNumber"].ColumnName = "Part No";

                    dtAddtionalBomCost.Columns.Remove("AddtionalPart");

                    gvAddtionalPartDetails.DataSource = dtAddtionalBomCost;
                    gvAddtionalPartDetails.DataBind();

                    //   gvAddtionalPartDetails_pdf.DataSource = dtAddtionalBomCost;
                    //  gvAddtionalPartDetails_pdf.DataBind();

                    Decimal TotalCost = Convert.ToDecimal(ds.Tables[2].Rows[0]["TotalBOMCost"].ToString()) + Convert.ToDecimal(ds.Tables[3].Rows[0]["AddtionalPartCost"].ToString()) + Convert.ToDecimal(ds.Tables[4].Rows[0]["IssuePartCost"].ToString());
                    lblAddtionalPartCost.Text = lblAddtionalPartCost_pdf.Text = "Addtional Part Cost : " + Convert.ToDecimal(ds.Tables[3].Rows[0]["AddtionalPartCost"].ToString());

                    lblTotalBOMCost.Text = lblTotalBOMCost_pdf.Text = "Total Bom Cost : " + TotalCost;

                    Label lblItemName = (Label)gvViewCosting.Rows[index].FindControl("lblItemName");
                    LinkButton lbtnDrawingname = (LinkButton)gvViewCosting.Rows[index].FindControl("lbtnDeviationFile");
                    Label lblTagNo = (Label)gvViewCosting.Rows[index].FindControl("lblItemTagNumber");
                    Label lblTotalQty = (Label)gvViewCosting.Rows[index].FindControl("lblQuantity");
                    TextBox txtDeadInventoryRemarks = (TextBox)gvViewCosting.Rows[index].FindControl("txtDeadInventoryRemarks");

                    string CostEstimatedBy = gvViewCosting.DataKeys[index].Values[2].ToString();
                    string CostEstimatedDate = gvViewCosting.DataKeys[index].Values[3].ToString();
                    string OverAllLength = gvViewCosting.DataKeys[index].Values[4].ToString();
                    string LseNumber = gvViewCosting.DataKeys[index].Values[5].ToString();

                    lblCustomerName_p.Text = lblCustomerName_pdf.Text = ddlCustomerName.SelectedItem.Text;
                    lblItemname_p.Text = lblItemname_pdf.Text = lblItemName.Text;
                    lblDrawingname_p.Text = lblDrawingname_pdf.Text = gvViewCosting.DataKeys[index].Values[6].ToString();
                    lblTagNo_p.Text = lblTagNo_pdf.Text = lblTagNo.Text;
                    lblTotalQty_p.Text = lblTotalQty_pdf.Text = lblTotalQty.Text;
                    lblDeadInventoryRemarks_p.Text = lblDeadInventoryRemarks_pdf.Text = txtDeadInventoryRemarks.Text;
                    lblUserName_p.Text = lblUserName_pdf.Text = CostEstimatedBy;
                    lblDate_p.Text = lblDate_pdf.Text = CostEstimatedDate;
                    lblOverAllLength_p.Text = lblOverAllLength_pdf.Text = OverAllLength;
                    lblLSENumber_p.Text = lblLSENumber_pdf.Text = LseNumber;

                    ds.Tables[1].Columns.Remove("DrawingSequenceNumber");
                    ds.Tables[1].Columns["AddtionalPart"].ColumnName = "AP";
                    ds.Tables[1].Columns["GradeName"].ColumnName = "MOC";

                    dr = ds.Tables[1].NewRow();
                    dr["MCOST"] = ds.Tables[2].Rows[0]["MCOST"].ToString();
                    dr["LCOST"] = ds.Tables[2].Rows[0]["LCOST"].ToString();
                    if (ds.Tables[1].Columns.Contains("MCRate"))
                        dr["MCRate"] = ds.Tables[2].Rows[0]["MCRate"].ToString();

                    dr["AWT"] = ds.Tables[2].Rows[0]["AWT"].ToString();
                    dr["WT"] = ds.Tables[2].Rows[0]["WT"].ToString();

                    ds.Tables[1].Rows.InsertAt(dr, ds.Tables[1].Rows.Count + 1);

                    ViewState["RowCount"] = ds.Tables[1].Rows.Count - 1;

                    gvItemCostingDetails_pdf.DataSource = ds.Tables[1];
                    gvItemCostingDetails_pdf.DataBind();
                }
                else
                {
                    gvItemCostingDetails.DataSource = "";
                    gvItemCostingDetails.DataBind();
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUP", "ShowViewPopUP();", true);
            }
            if (e.CommandName == "ViewDeviationFile")
            {
                ViewDrawingFilename();
            }
        }

        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvViewCosting_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataRowView dr = (DataRowView)e.Row.DataItem;

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chk = (CheckBox)e.Row.FindControl("chkitems");

            if (dr["DesignHApproval"].ToString() == "9" || dr["DesignHApproval"].ToString() == "0")
            {
                if (dr["CostEstimated"].ToString() == "Completed")
                    chk.Visible = true;
                else
                    chk.Visible = false;
            }
            else if (dr["DesignHApproval"].ToString() == "1")
                chk.Visible = false;

            if (string.IsNullOrEmpty(dr["CostEstimated"].ToString()))
                chk.Visible = false;

            TextBox txtDeadInvenAmount = (TextBox)e.Row.FindControl("txtDeadInventoryAmount");
            TextBox txtDeadInvenRemarks = (TextBox)e.Row.FindControl("txtDeadInventoryRemarks");

            txtDeadInvenAmount.Text = dr["DeadInventoryAmount"].ToString();
            txtDeadInvenRemarks.Text = dr["DeadInventoryRemarks"].ToString();
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

    #region"Common Methods"

    private void ddlEnquiryLoad()
    {
        objc = new cCommon();
        try
        {
            objc = new cCommon();
            DataSet dsEnquiryNumber = new DataSet();
            DataSet dsCustomer = new DataSet();
            dsCustomer = objc.GetCustomerNameByPendingList(Convert.ToInt32(objSession.employeeid), ddlCustomerName, "LS_GetCustomerNameByViewCostingSheet", rblEnquiryChange.SelectedValue);
            ViewState["CustomerDetails"] = dsCustomer.Tables[0];

            dsEnquiryNumber = objc.GetEnquiryNumberByPendingList(Convert.ToInt32(objSession.employeeid), ddlEnquiryNumber, "LS_GetEnquiryIDByViewCostingSheet", rblEnquiryChange.SelectedValue);
            ViewState["EnquiryDetails"] = dsEnquiryNumber.Tables[0];
        }
        catch (Exception ec)
        {
            Log.Message(ec.ToString());
        }
    }

    private void UpdateSharedWithHODStatus()
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            for (int i = 1; i <= gvViewCosting.Rows.Count; i++)
            {
                CheckBox chk = (CheckBox)gvViewCosting.Rows[i - 1].FindControl("chkitems");
                Label lblDrawingAttchStatus = (Label)gvViewCosting.Rows[i - 1].FindControl("lblCostEstimationStatus");
                TextBox txtDeadInvenAmount = (TextBox)gvViewCosting.Rows[i - 1].FindControl("txtDeadInventoryAmount");
                TextBox txtDeadInvenRemarks = (TextBox)gvViewCosting.Rows[i - 1].FindControl("txtDeadInventoryRemarks");

                if (chk.Checked)
                {
                    objMat.DDID = Convert.ToInt32(gvViewCosting.DataKeys[i - 1].Values[0].ToString());
                    objMat.Mode = "Share";
                    objMat.Remarks = null;
                    if (txtDeadInvenAmount.Text != "")
                        objMat.DeadinventoryAmount = Convert.ToInt32(txtDeadInvenAmount.Text);
                    else
                        objMat.DeadinventoryAmount = 0;
                    objMat.DeadinventoryRemarks = txtDeadInvenRemarks.Text;

                    ds = objMat.UpdateDesignHApprovalStatus();
                }
            }
            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ddlEnquiryNumber_SelectIndexChanged(null, null);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','BOM Status Updated Successfully');", true);
                SaveAlertDetails();
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
        objc = new cCommon();
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

            objc = new cCommon();

            objc.ViewFileName(DrawingDocumentSavePath, DrawingDocumentHttpPath, ViewState["FileName"].ToString(), EnquiryID, ifrm);

            //if (File.Exists(imgname))
            //{
            //    ViewState["ifrmsrc"] = httpPath + ViewState["FileName"].ToString();
            //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewdocsPopUp();", true);
            //    string s = "window.open('" + httpPath + EnquiryID + "/" + ViewState["FileName"].ToString() + "','_blank');ShowViewPopUP();";
            //    this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
            //}
            //else
            //{
            //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewdocsPopUp();", true);                
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Drawing Not Found');", true);
            //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewPopUP();", true);
            //}
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void SaveAlertDetails()
    {
        EmailAndSmsAlerts objAlerts = new EmailAndSmsAlerts();
        DataSet ds = new DataSet();
        try
        {
            objAlerts.EntryMode = "Group";
            objAlerts.AlertType = "Mail";
            objAlerts.userID = objSession.employeeid;
            objAlerts.reciverType = "Selected Group";
            objAlerts.file = "";
            objAlerts.reciverID = "0";
            objAlerts.EmailID = "";
            objAlerts.GroupID = 11;
            objAlerts.Subject = "BOM Approval Alert";
            objAlerts.Message = "BOM Approval Request From Enquiry Number " + ddlEnquiryNumber.SelectedValue;
            objAlerts.Status = 0;
            objAlerts.SaveCommunicationEmailAlertDetails();
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
        if (gvViewCosting.Rows.Count > 0)
            gvViewCosting.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (gvItemCostingDetails.Rows.Count > 0)
            gvItemCostingDetails.HeaderRow.TableSection = TableRowSection.TableHeader;

        if (gvAddtionalPartDetails.Rows.Count > 0)
            gvAddtionalPartDetails.HeaderRow.TableSection = TableRowSection.TableHeader;

    }

    #endregion
}