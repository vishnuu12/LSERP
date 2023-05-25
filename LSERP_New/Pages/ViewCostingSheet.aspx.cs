using eplus.core;
using SelectPdf;
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

public partial class Pages_ViewCostingSheet : System.Web.UI.Page
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
                ViewState["DDID"] = Request.QueryString["DDID"].ToString();
                bindcostingSheetDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"
	
	
    protected void btnViewDrawingFile(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        string DrawingDocumentSavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString().Trim();
        string DrawingDocumentHttpPath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString().Trim();

        string Savepath = DrawingDocumentSavePath;
        string httpPath = DrawingDocumentHttpPath;
        string EnquiryID = ViewState["EnquiryIDD"].ToString();
        objMat.EnquiryNumber = Convert.ToInt32(ViewState["EnquiryIDD"]);
        ds = objMat.GetViewCostingDetails();

        ViewState["FileName"] = ds.Tables[0].Rows[0]["FileName"].ToString();
        string imgname = httpPath + EnquiryID + "/" + ViewState["FileName"].ToString();


        ScriptManager.RegisterStartupScript(this, this.GetType(), "Print", "OpenDrawingFile('" + imgname + "');", true);
    }

    protected void btndownloaddocs_Click(object sender, EventArgs e)
    {
        GeneratePDDF();
    }

    #endregion


    #region "Common Methods"

    private void bindcostingSheetDetails()
    {
        DataSet ds = new DataSet();
        objDesign = new cDesign();
        DataRow dr;
        DataView dv;
        DataTable dtItemBomCost;
        DataTable dtAddtionalBomCost;
        DataTable dtIssueBomCost;
        try
        {
            objDesign.DDID = Convert.ToInt32(ViewState["DDID"].ToString());
            ds = objDesign.GetViewCostingDetailsByItemID();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "RecordsFound")
            {
                gvItemCostingDetails.DataSource = ds.Tables[1];

                ds.Tables[1].Columns.Remove("BOMID");
                ds.Tables[1].Columns.Remove("ETCID");
                ds.Tables[1].Columns.Remove("DDID");
                ds.Tables[1].Columns.Remove("MID");
                ds.Tables[1].Columns.Remove("BOMStatus");

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

                //Decimal TotalCost = Convert.ToDecimal(ds.Tables[2].Rows[0]["TotalBOMCost"].ToString()) + Convert.ToDecimal(ds.Tables[3].Rows[0]["AddtionalPartCost"].ToString());
                lblAddtionalPartCost.Text = lblAddtionalPartCost_pdf.Text = "Addtional Part Cost : " + Convert.ToDecimal(ds.Tables[3].Rows[0]["AddtionalPartCost"].ToString());

                //New Code

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

                Decimal TotalCost = Convert.ToDecimal(ds.Tables[2].Rows[0]["TotalBOMCost"].ToString()) + Convert.ToDecimal(ds.Tables[3].Rows[0]["AddtionalPartCost"].ToString()) + Convert.ToDecimal(ds.Tables[4].Rows[0]["IssuePartCost"].ToString());
                lblIssuePartCost.Text = lblIssuePartCost_pdf.Text = "FIM Cost : " + Convert.ToDecimal(ds.Tables[4].Rows[0]["IssuePartCost"].ToString());

                //End Code
                lblTotalBOMCost.Text = lblTotalBOMCost_pdf.Text = "Total Bom Cost : " + TotalCost;

                lblCustomerName_p.Text = lblCustomerName_pdf.Text = ds.Tables[0].Rows[0]["ProspectName"].ToString();
                lblItemname_p.Text = lblItemname_pdf.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
                lblDrawingname_p.Text = lblDrawingname_pdf.Text = ds.Tables[0].Rows[0]["DrawingName"].ToString();
                lblTagNo_p.Text = lblTagNo_pdf.Text = ds.Tables[0].Rows[0]["TagNo"].ToString();
                lblTotalQty_p.Text = lblTotalQty_pdf.Text = ds.Tables[5].Rows[0]["Quantity"].ToString();
                lblDeadInventoryRemarks_p.Text = lblDeadInventoryRemarks_pdf.Text = ds.Tables[0].Rows[0]["DeadInventoryRemarks"].ToString();
                lblUserName_p.Text = lblUserName_pdf.Text = ds.Tables[0].Rows[0]["CostEstimatedBy"].ToString();
                lblDate_p.Text = lblDate_pdf.Text = ds.Tables[0].Rows[0]["CostEstimatedDate"].ToString();
                lblOverAllLength_p.Text = lblOverAllLength_pdf.Text = ds.Tables[0].Rows[0]["OverAllLength"].ToString();
                lblLSENumber_p.Text = lblLSENumber_pdf.Text = ds.Tables[0].Rows[0]["EnquiryNo"].ToString();
				
				ViewState["EnquiryIDD"] = Convert.ToInt32(ds.Tables[0].Rows[0]["EnquiryNo"].ToString());

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
            // StringBuilder sbCosting = new StringBuilder();

            // divViewCostingPrint_pdf.RenderControl(new HtmlTextWriter(new StringWriter(sbCosting)));

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

    #endregion

    #region"GridView Events"  

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

    #region"PageLoadComplete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvItemCostingDetails.Rows.Count > 0)
            gvItemCostingDetails.HeaderRow.TableSection = TableRowSection.TableHeader;

        if (gvAddtionalPartDetails.Rows.Count > 0)
            gvAddtionalPartDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}