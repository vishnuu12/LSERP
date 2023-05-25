﻿using GemBox.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_DesignKPIReports_OverAll : System.Web.UI.Page
{
    #region "Declaration"

    cSales objSales;
    cDesign objDesign;
    cReports objRpt;
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
                GetFinanaceYear();
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
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["DesignKPIReports"];

            // dt.Columns.Remove("SUPID");

            int rowcount = Convert.ToInt32(dt.Rows.Count);
            int ColumnCount = Convert.ToInt32(dt.Columns.Count);

            string strFile = "";
            string LetterName = "DesignKPIReports" + ".xls";
            string LetterPath = ConfigurationManager.AppSettings["ExcelSavePath"].ToString();
            string GemBoxKey = ConfigurationManager.AppSettings["GemBoxKey"].ToString();

            strFile = LetterPath + LetterName;

            if (!Directory.Exists(LetterPath))
                Directory.CreateDirectory(LetterPath);

            if (File.Exists(strFile))
                File.Delete(strFile);

            exportExcel(dt, strFile, LetterName, GemBoxKey);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region "Common Methods"
    private void BindDesignKPIReports()
    {
        DataSet ds = new DataSet();
        objDesign = new cDesign();
        try
        {
            objDesign.FYID = ddlFinanceYear.SelectedValue;
            objDesign.UserID = Convert.ToInt32(objSesion.employeeid);

            ds = objDesign.GetDesignKPIReports();

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["DesignKPIReports"] = ds.Tables[0];
                gvDesignKPIReports.DataSource = ds.Tables[0];
                gvDesignKPIReports.DataBind();
            }
            else
            {
                gvDesignKPIReports.DataSource = "";
                gvDesignKPIReports.DataBind();
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLoader", "AddFooter();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void GetFinanaceYear()
    {
        DataSet ds = new DataSet();
        objSales = new cSales();
        try
        {
            ds = objSales.GetFinanceYearDetails(ddlFinanceYear);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindDesignKPIReportDetails(string Month, string Year)
    {
        DataSet ds = new DataSet();
        try
        {
            var page = HttpContext.Current.CurrentHandler as Page;
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 1].ToString();

            string Page = url.Replace(Replacevalue, "DesignKPIReportDetails.aspx?Month=" + Month + "&&Year=" + Year + "&&FYID=" + ddlFinanceYear.SelectedValue + "");

            string s = "window.open('" + Page + "','_blank');";
            this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void exportExcel(DataTable dt, string fileName, string LetterName, string gemBoxKey)
    {
        try
        {
            if ((dt.Rows.Count > 0))
            {
                string _key = gemBoxKey;
                SpreadsheetInfo.SetLicense(_key);

                ExcelFile workbook = new ExcelFile();
                ExcelWorksheet SheetLocation1;

                SheetLocation1 = workbook.Worksheets.Add("DesignKPI");

                if (dt.Rows.Count > 0)
                {
                    SheetLocation1 = exportex(dt, dt.Rows.Count, dt.Columns.Count, LetterName,
                        "Design KPI Reports " + ddlFinanceYear.SelectedItem.Text, "", 2, SheetLocation1);
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

    public ExcelWorksheet exportex(DataTable dt, int rowCount, int columnCount, string LetterName, string schoolName, string reportName, int startRow, ExcelWorksheet worksheet)
    {
        try
        {
            worksheet.Rows[startRow].Style.Font.Weight = ExcelFont.BoldWeight;

            columnCount = columnCount - 1;

            worksheet.Cells.GetSubrange("A" + (startRow + 1).ToString(), worksheet.Cells[rowCount + startRow, columnCount].ToString()).Style.Borders.SetBorders(MultipleBorders.All, SpreadsheetColor.FromName(ColorName.Black), LineStyle.Thin);

            worksheet.Cells.GetSubrange("A" + (startRow + 1).ToString(), worksheet.Cells[rowCount + startRow, columnCount].ToString()).Style.Font.Name = "Times New Roman";

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
                worksheet.Columns[i].AutoFit(1, worksheet.Rows[1], worksheet.Rows[worksheet.Rows.Count - 1]);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return worksheet;
    }


    #endregion

    #region"GridView Events"

    protected void gvDesignKPIReports_OnrowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string Month = gvDesignKPIReports.DataKeys[index].Values[0].ToString();
            string Year = gvDesignKPIReports.DataKeys[index].Values[1].ToString();
            BindDesignKPIReportDetails(Month, Year);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Methods"

    protected void ddlFinanceYear_OnSelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlFinanceYear.SelectedIndex > 0)
            {
                BindDesignKPIReports();
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

}