using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_RFPExpensesSheet : System.Web.UI.Page
{
    #region"Declaration"

    cReports objRp;
    cSession objSesion = new cSession();

    #endregion

    #region"Page Init Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        objSesion = Master.csSession;
    }

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                DataSet ds = new DataSet();
                objRp = new cReports();

                ds = objRp.GetRFPNoDetails(ddlRFPNo);

                divOutput.Visible = false;
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
            {
                BindRFPExpensesReportDetails();
                BindRFPExpensesReportDetailsSupplier();
                divOutput.Visible = true;
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

    #region"GridView Events"

    protected void gvRFPExpensesDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            var page = HttpContext.Current.CurrentHandler as Page;
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 1].ToString();

            string Page = url.Replace(Replacevalue, "RFPItemExpensesSheet.aspx?RFPHID=" + ddlRFPNo.SelectedValue + "");

            string s = "window.open('" + Page + "','_blank');";
            this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
	
											

    #region"Button Events"

    protected void btnExcelDownload_Click(object sender, EventArgs e)
    {
        try
        {
            string MAXEXID = "";
            DataTable ds = new DataTable();
            ds = (DataTable)ViewState["MaterialExpensesDetails"];

            ds.Columns.Remove("JCHID");
            ds.Columns.Remove("RFPHID");
            ds.Columns.Remove("IssuedBy");
            ds.Columns.Remove("DrawingSequenceNumber");
            ds.Columns.Remove("DDID");

            int rowcount = Convert.ToInt32(ds.Rows.Count);
            int ColumnCount = Convert.ToInt32(ds.Columns.Count);

            string strFile = "";
            string LetterName = "MaterialExpensesDetails" + ".xls";
            string LetterPath = ConfigurationManager.AppSettings["ExcelSavePath"].ToString();
            string GemBoxKey = ConfigurationManager.AppSettings["GemBoxKey"].ToString();

            strFile = LetterPath + LetterName;

            if (!Directory.Exists(LetterPath))
                Directory.CreateDirectory(LetterPath);

            if (File.Exists(strFile))
                File.Delete(strFile);

            exportExcel(ds, rowcount, ColumnCount, strFile, LetterName, "", "Material Expenses Details", 2, GemBoxKey);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnExcelDownload_ClickSupplier(object sender, EventArgs e)
    {
        try
        {
            string MAXEXID = "";
            DataTable ds = new DataTable();
            ds = (DataTable)ViewState["SupplierExpensesDetails"];

            ds.Columns.Remove("BtnEnable");
            ds.Columns.Remove("SPODID");
            ds.Columns.Remove("SPOID");
            ds.Columns.Remove("Quantity");
            ds.Columns.Remove("DCQuantity");
            ds.Columns.Remove("CategoryName");
            ds.Columns.Remove("GradeName");
            ds.Columns.Remove("THKValue");
            ds.Columns.Remove("TypeName");
            //ds.Columns.Remove("UOM");
            ds.Columns.Remove("ReqWeight");
            ds.Columns.Remove("DeliveryDate");
            ds.Columns.Remove("Measurment");
            ds.Columns.Remove("ReqWeight1");
            ds.Columns.Remove("FileName");
            ds.Columns.Remove("MIDID");
            ds.Columns.Remove("RFPNo1");
            ds.Columns.Remove("InwardBy");
            ds.Columns.Remove("InwardOn");

            int rowcount = Convert.ToInt32(ds.Rows.Count);
            int ColumnCount = Convert.ToInt32(ds.Columns.Count);

            string strFile = "";
            string LetterName = "SupplierExpensesDetails" + ".xls";
            string LetterPath = ConfigurationManager.AppSettings["ExcelSavePath"].ToString();
            string GemBoxKey = ConfigurationManager.AppSettings["GemBoxKey"].ToString();

            strFile = LetterPath + LetterName;

            if (!Directory.Exists(LetterPath))
                Directory.CreateDirectory(LetterPath);

            if (File.Exists(strFile))
                File.Delete(strFile);

            exportExcelForSupplier(ds, rowcount, ColumnCount, strFile, LetterName, "", "Supplier Expenses Details", 2, GemBoxKey);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    #endregion

    #region"Common Methods"
	
	
    public void exportExcelForSupplier(DataTable dt, int rowCount, int columnCount, string fileName, string LetterName, string schoolName, string reportName, int startRow, string gemBoxKey)
    {
        try
        {
            if ((dt.Rows.Count > 0) && (rowCount != 0) && (columnCount != 0))
            {
                string _key = gemBoxKey;
                SpreadsheetInfo.SetLicense(_key);

                var workbook = new ExcelFile();
                var worksheet = workbook.Worksheets.Add("SupplierExpensesDetails");

                worksheet.Rows[startRow].Style.Font.Weight = ExcelFont.BoldWeight;
                columnCount = columnCount - 1;

                worksheet.Cells.GetSubrange("A" + (startRow + 1).ToString(), worksheet.Cells[rowCount + startRow, columnCount].ToString()).Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);

                if (schoolName != "")
                {
                    worksheet.Cells[0, 0].Value = schoolName;
                    worksheet.Cells.GetSubrangeAbsolute(0, 0, 0, columnCount).Merged = true;
                    worksheet.Cells.GetSubrangeAbsolute(0, 0, 0, columnCount).Style.Font.Name = "Times New Roman";
                    worksheet.Cells.GetSubrangeAbsolute(0, 0, 0, columnCount).Style.Font.Weight = ExcelFont.BoldWeight;
                    worksheet.Cells.GetSubrangeAbsolute(0, 0, 0, columnCount).Style.Font.Size = 18 * 20;
                    worksheet.Cells.GetSubrangeAbsolute(0, 0, 0, columnCount).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
                }

                if (reportName != "")
                {
                    worksheet.Cells[1, 0].Value = reportName;
                    worksheet.Cells[1, 0].Row.Height = 500;
                    worksheet.Cells.GetSubrangeAbsolute(1, 0, 1, columnCount).Merged = true;
                    worksheet.Cells.GetSubrangeAbsolute(1, 0, 1, columnCount).Style.Font.Name = "Times New Roman";
                    worksheet.Cells.GetSubrangeAbsolute(1, 0, 1, columnCount).Style.Font.Weight = ExcelFont.BoldWeight;
                    worksheet.Cells.GetSubrangeAbsolute(1, 0, 1, columnCount).Style.Font.Size = 18 * 20;
                    worksheet.Cells.GetSubrangeAbsolute(1, 0, 1, columnCount).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
                }

                if (startRow == 0)
                    startRow = 2;

                worksheet.Cells.GetSubrangeAbsolute(2, 0, 2, columnCount).Style.Font.Name = "Times New Roman";
                worksheet.Cells.GetSubrangeAbsolute(2, 0, 2, columnCount).Style.Font.Weight = ExcelFont.BoldWeight;

                // Insert DataTable to an Excel worksheet . // ,
                worksheet.InsertDataTable(dt,
                   new InsertDataTableOptions()
                   {
                       ColumnHeaders = true,
                       StartRow = 2
                   });

                for (int i = 0; i < worksheet.CalculateMaxUsedColumns(); i++)
                {
                    worksheet.Columns[i].AutoFit(1, worksheet.Rows[1], worksheet.Rows[worksheet.Rows.Count - 1]);
                    worksheet.Rows[startRow + 1 + i].Style.Font.Weight = ExcelFont.MinWeight;
                    worksheet.Rows[startRow + 1 + i].Style.Font.Name = "Times New Roman";
                }
                workbook.Save(fileName);

                var response = HttpContext.Current.Response;
                var options = SaveOptions.XlsDefault;

                response.Clear();
                response.ContentType = options.ContentType;
                response.AddHeader("Content-Disposition", "attachment; filename=" + LetterName);

                var ms = new System.IO.MemoryStream();
                workbook.Save(ms, options);
                ms.WriteTo(response.OutputStream);

                response.Flush();
                response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
	
   public void exportExcel(DataTable dt, int rowCount, int columnCount, string fileName, string LetterName, string schoolName, string reportName, int startRow, string gemBoxKey)
    {
        try
        {
            if ((dt.Rows.Count > 0) && (rowCount != 0) && (columnCount != 0))
            {
                string _key = gemBoxKey;
                SpreadsheetInfo.SetLicense(_key);

                var workbook = new ExcelFile();
                var worksheet = workbook.Worksheets.Add("MaterialExpensesDetails");

                worksheet.Rows[startRow].Style.Font.Weight = ExcelFont.BoldWeight;
                columnCount = columnCount - 1;

                worksheet.Cells.GetSubrange("A" + (startRow + 1).ToString(), worksheet.Cells[rowCount + startRow, columnCount].ToString()).Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);

                if (schoolName != "")
                {
                    worksheet.Cells[0, 0].Value = schoolName;
                    worksheet.Cells.GetSubrangeAbsolute(0, 0, 0, columnCount).Merged = true;
                    worksheet.Cells.GetSubrangeAbsolute(0, 0, 0, columnCount).Style.Font.Name = "Times New Roman";
                    worksheet.Cells.GetSubrangeAbsolute(0, 0, 0, columnCount).Style.Font.Weight = ExcelFont.BoldWeight;
                    worksheet.Cells.GetSubrangeAbsolute(0, 0, 0, columnCount).Style.Font.Size = 18 * 20;
                    worksheet.Cells.GetSubrangeAbsolute(0, 0, 0, columnCount).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
                }

                if (reportName != "")
                {
                    worksheet.Cells[1, 0].Value = reportName;
                    worksheet.Cells[1, 0].Row.Height = 500;
                    worksheet.Cells.GetSubrangeAbsolute(1, 0, 1, columnCount).Merged = true;
                    worksheet.Cells.GetSubrangeAbsolute(1, 0, 1, columnCount).Style.Font.Name = "Times New Roman";
                    worksheet.Cells.GetSubrangeAbsolute(1, 0, 1, columnCount).Style.Font.Weight = ExcelFont.BoldWeight;
                    worksheet.Cells.GetSubrangeAbsolute(1, 0, 1, columnCount).Style.Font.Size = 18 * 20;
                    worksheet.Cells.GetSubrangeAbsolute(1, 0, 1, columnCount).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
                }

                if (startRow == 0)
                    startRow = 2;

                worksheet.Cells.GetSubrangeAbsolute(2, 0, 2, columnCount).Style.Font.Name = "Times New Roman";
                worksheet.Cells.GetSubrangeAbsolute(2, 0, 2, columnCount).Style.Font.Weight = ExcelFont.BoldWeight;

                // Insert DataTable to an Excel worksheet . // ,
                worksheet.InsertDataTable(dt,
                   new InsertDataTableOptions()
                   {
                       ColumnHeaders = true,
                       StartRow = 2
                   });

                for (int i = 0; i < worksheet.CalculateMaxUsedColumns(); i++) {
                    worksheet.Columns[i].AutoFit(1, worksheet.Rows[1], worksheet.Rows[worksheet.Rows.Count - 1]);
                    worksheet.Rows[startRow + 1 + i].Style.Font.Weight = ExcelFont.MinWeight;
                    worksheet.Rows[startRow + 1 + i].Style.Font.Name = "Times New Roman";
                }
                workbook.Save(fileName);

                var response = HttpContext.Current.Response;
                var options = SaveOptions.XlsDefault;

                response.Clear();
                response.ContentType = options.ContentType;
                response.AddHeader("Content-Disposition", "attachment; filename=" + LetterName);

                var ms = new System.IO.MemoryStream();
                workbook.Save(ms, options);
                ms.WriteTo(response.OutputStream);

                response.Flush();
                response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindRFPExpensesReportDetails()
    {
        DataSet ds = new DataSet();
        objRp = new cReports();
        try
        {
            objRp.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objRp.GetRFPExpensesReportDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["MaterialExpensesDetails"] = ds.Tables[0];
                gvJobCardDetails.DataSource = ds.Tables[0];
                gvJobCardDetails.DataBind();
            }
            else
            {
                gvJobCardDetails.DataSource = "";
                gvJobCardDetails.DataBind();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                gvworkorderpo.DataSource = ds.Tables[1];
                gvworkorderpo.DataBind();
            }
            else
            {
                gvworkorderpo.DataSource = "";
                gvworkorderpo.DataBind();
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                gvContractorexpensesdetails.DataSource = ds.Tables[2];
                gvContractorexpensesdetails.DataBind();
            }
            else
            {
                gvContractorexpensesdetails.DataSource = "";
                gvContractorexpensesdetails.DataBind();
            }

            if (ds.Tables[3].Rows.Count > 0)
            {
                gvstoreissuedetails.DataSource = ds.Tables[3];
                gvstoreissuedetails.DataBind();
            }
            else
            {
                gvstoreissuedetails.DataSource = "";
                gvstoreissuedetails.DataBind();
            }

            lbltotalmtlexpenses.Text = "Total Material Expenses   = " + ds.Tables[4].Rows[0]["MaterialExpenses"].ToString();
            lblwpoexpenses.Text = "Total Work Order Expenses      = " + ds.Tables[4].Rows[0]["WPOExpenses"].ToString();
            lbljocardratedetails.Text = "Total Job Card Payment Expenses   = " + ds.Tables[4].Rows[0]["JobCardPaymentExpenses"].ToString();
            lblgmissue.Text = "Store Issue = " + ds.Tables[4].Rows[0]["GeneralIssueExpenses"].ToString();

            if (ds.Tables[5].Rows.Count > 0)
            {
                gvRFPExpensesDetails.DataSource = ds.Tables[5];
                gvRFPExpensesDetails.DataBind();
            }
            else
            {
                gvRFPExpensesDetails.DataSource = "";
                gvRFPExpensesDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
	
    private void BindRFPExpensesReportDetailsSupplier()
    {
        DataSet ds = new DataSet();
        objRp = new cReports();
        try
        {
            objRp.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objRp.GetRFPExpensesReportDetailsSupplier();

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["SupplierExpensesDetails"] = ds.Tables[0];
                gvsupplierpo.DataSource = ds.Tables[0];
                gvsupplierpo.DataBind();
            }
            else
            {
                gvsupplierpo.DataSource = "";
                gvsupplierpo.DataBind();
            }
            if (ds.Tables[1].Rows.Count > 0)
                lbltotalsplexpenses.Text = "Total Supplier Expenses   = " + ds.Tables[1].Rows[0]["TotalSupplierCost"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}