﻿using eplus.core;
using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_SupplierEvaluvationReportaspx : System.Web.UI.Page
{
    #region"DecLaration"

    cSales objSales;
    cSession objSession = new cSession();
    cReports objR;

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
                ViewState["SUPID"] = Request.QueryString["SUPID"];
                ViewState["FromDate"] = Request.QueryString["FromDate"];
                ViewState["Todate"] = Request.QueryString["ToDate"];

                BindSupplierEvaluvationReportDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            BindSupplierEvaluvationReportDetails();
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
        cCommon _objc = new cCommon();
        try
        {
            string MAXEXID = "";
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["DataTable"];

            dt.Columns.Remove("MID_CreatedDate");

            MAXEXID = _objc.GetMaximumNumberExcel();

            int rowcount = Convert.ToInt32(dt.Rows.Count);
            int ColumnCount = Convert.ToInt32(dt.Columns.Count);

            string strFile = "";

            string LetterName = "SupplierEvaluvation" + "-" + ViewState["SupplierName"].ToString() + "-" + MAXEXID + ".xlsx";

            string LetterPath = ConfigurationManager.AppSettings["ExcelSavePath"].ToString();

            string GemBoxKey = ConfigurationManager.AppSettings["GemBoxKey"].ToString();

            strFile = LetterPath + LetterName;

            if (!Directory.Exists(LetterPath))
                Directory.CreateDirectory(LetterPath);

            if (File.Exists(strFile))
                File.Delete(strFile);

            exportExcel(dt, rowcount, ColumnCount, strFile, LetterName, "Supplier Evaluvation Report" + " For " + ViewState["SupplierName"].ToString() + " Date Between " + ViewState["FromDate"].ToString() + " And " + ViewState["Todate"].ToString(), "", 2, GemBoxKey);

            _objc.SaveExcelFile("SupplierEvaluvationReportaspx.aspx", LetterName, objSession.employeeid);

            //bindMaterialMaster();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvEnquiryProjectAssignmentReport_DataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    public void exportExcel(DataTable dt, int rowCount, int columnCount, string fileName, string LetterName, string schoolName, string reportName, int startRow, string gemBoxKey)
    {
        try
        {
            if ((dt.Rows.Count > 0) && (rowCount != 0) && (columnCount != 0))
            {
                string _key = gemBoxKey;
                SpreadsheetInfo.SetLicense(_key);

                var workbook = new ExcelFile();
                var worksheet = workbook.Worksheets.Add("Supplier Evaluvation");

                worksheet.Rows[startRow].Style.Font.Weight = ExcelFont.BoldWeight;

                columnCount = columnCount - 1;

                worksheet.Cells.GetSubrange("A" + (startRow + 1).ToString(), worksheet.Cells[rowCount + startRow, columnCount].ToString()).Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);

                if (schoolName != "")
                {
                    worksheet.Cells[0, 0].Value = schoolName;
                    worksheet.Cells[0, 0].Style.Font.Name = "Times New Roman";
                    worksheet.Cells.GetSubrangeAbsolute(0, 0, 0, columnCount).Merged = true;
                    worksheet.Cells.GetSubrangeAbsolute(0, 0, 0, columnCount).Style.Font.Color = SpreadsheetColor.FromName(ColorName.DarkRed);
                    worksheet.Cells.GetSubrangeAbsolute(0, 0, 0, columnCount).Style.Font.Size = 18 * 20;
                    worksheet.Cells.GetSubrangeAbsolute(0, 0, 0, columnCount).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
                }

                if (reportName != "")
                {
                    worksheet.Cells[1, 0].Value = reportName;
                    worksheet.Cells.GetSubrangeAbsolute(1, 0, 1, columnCount).Merged = true;
                    worksheet.Cells.GetSubrangeAbsolute(1, 0, 1, columnCount).Style.Font.Weight = ExcelFont.BoldWeight;
                    worksheet.Cells.GetSubrangeAbsolute(1, 0, 1, columnCount).Style.Font.Size = 18 * 20;
                    worksheet.Cells.GetSubrangeAbsolute(1, 0, 1, columnCount).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
                }

                if (startRow == 0)
                    startRow = 2;

                worksheet.Cells.GetSubrangeAbsolute(2, 0, 2, columnCount).Style.Font.Weight = ExcelFont.BoldWeight;

                // Insert DataTable to an Excel worksheet . //
                worksheet.InsertDataTable(dt,
                   new InsertDataTableOptions()
                   {
                       ColumnHeaders = true,
                       StartRow = 2
                   });

                for (int i = 0; i < worksheet.CalculateMaxUsedColumns(); i++)
                {
                    worksheet.Columns[i].AutoFit(1, worksheet.Rows[1], worksheet.Rows[worksheet.Rows.Count - 1]);
                }

                workbook.Save(fileName);

                var response = HttpContext.Current.Response;
                var options = SaveOptions.XlsxDefault;

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

    private void BindSupplierEvaluvationReportDetails()
    {
        objR = new cReports();
        DataSet ds = new DataSet();
        try
        {
            //objR.SUPID = Convert.ToInt32(ddlSupplierName.SelectedValue);
            //objR.FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //objR.ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            objR.SUPID = Convert.ToInt32(ViewState["SUPID"].ToString());
            objR.FromDate = DateTime.ParseExact(ViewState["FromDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objR.ToDate = DateTime.ParseExact(ViewState["Todate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

            ds = objR.GetSupplierEvaluvationReportBySUPIDAndDate();

            ViewState["DataTable"] = ds.Tables[0];

            ViewState["SupplierName"] = ds.Tables[1].Rows[0]["SupplierName"].ToString();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvSupplierEvaluvationReportDetails.DataSource = ds.Tables[0];
                gvSupplierEvaluvationReportDetails.DataBind();
            }
            else
            {
                gvSupplierEvaluvationReportDetails.DataSource = "";
                gvSupplierEvaluvationReportDetails.DataBind();
            }

            Double sum = ds.Tables[0].AsEnumerable().Sum(x => x.Field<Double>("Totalmark"));
            int Count = ds.Tables[0].AsEnumerable().Count();

            Double res = sum / Count;

            lblavgmarks.Text = ds.Tables[1].Rows[0]["SupplierName"].ToString() + " Average marks " + Math.Round(res, 2).ToString();
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
        if (gvSupplierEvaluvationReportDetails.Rows.Count > 0)
        {
            gvSupplierEvaluvationReportDetails.UseAccessibleHeader = true;
            gvSupplierEvaluvationReportDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    #endregion
}