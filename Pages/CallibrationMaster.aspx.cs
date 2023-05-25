using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;
using System.Drawing;
using System.IO;
using System.Configuration;
using GemBox.Spreadsheet;
using System.Globalization;

public partial class Pages_CallibrationMaster : System.Web.UI.Page
{
   
    #region"Declaration"

        CallibrationType ctyp;
    cSession objSession = new cSession();
    string QualityReportSavePath = ConfigurationManager.AppSettings["QualityReportSavePath"].ToString();
    string QualityReportHttpPath = ConfigurationManager.AppSettings["QualityReportHttpPath"].ToString();

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
                    BindTestSupplierDetails();
                    ShowHideControls("view");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowDataTable", "showDataTable();", true);

            }
            if (target == "ViewIndentAttach")
            {
                int index = Convert.ToInt32(arg);
                string FileName = gvCallibration.DataKeys[index].Values[1].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "opennewtab('" + FileName + "');", true);
            }
            else
                {
                if (target == "deletegvrow")
                {
                    ctyp = new CallibrationType();
                    ctyp.CID = Convert.ToInt32(arg);

                    DataSet ds = ctyp.DeleteCallibrationDetails();

                    if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Callibration Details Deleted successfully');showDataTable();", true);
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Callibration Details Not Deleted');showDataTable();", true);

                    BindTestSupplierDetails();
                    ShowHideControls("view");
                }

            }
            }
            catch (Exception ex)
            {
                Log.Message(ex.ToString());
            }
        }

    #endregion

    #region"Button Events"


    protected void btnExcel_Click(object sender, EventArgs e)
    {
        try
        {
            string MAXEXID = "";
            DataTable ds = new DataTable();
             ds = (DataTable)ViewState["CallibrationDetails"];

            int rowcount = Convert.ToInt32(ds.Rows.Count);
            int ColumnCount = Convert.ToInt32(ds.Columns.Count);

            string strFile = "";
            string LetterName = "CallibrationMasterList" + ".xls";
            string LetterPath = ConfigurationManager.AppSettings["ExcelSavePath"].ToString();
            string GemBoxKey = ConfigurationManager.AppSettings["GemBoxKey"].ToString();

            strFile = LetterPath + LetterName;

            if (!Directory.Exists(LetterPath))
                Directory.CreateDirectory(LetterPath);

            if (File.Exists(strFile))
                File.Delete(strFile);

            exportExcel(ds, rowcount, ColumnCount, strFile, LetterName, "", "Callibration Master List", 2, GemBoxKey);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
        {
            ShowHideControls("add");
            CIDHDN.Value = "0";
        }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataSet dsw = new DataSet();
        ctyp = new CallibrationType();
        try
        {
            ctyp.CID = Convert.ToInt32(CIDHDN.Value);
            ctyp.txtCodeno = txtCodeno.Text;
            ctyp.txtRange = txtRange.Text;
            ctyp.txtcertificateno = txtcertificateno.Text;
            ctyp.txtCalibrationdon = txtCalibrationdon.Text;
            ctyp.txtCalibrationdue = txtCalibrationdue.Text;
            //ctyp.txtCalibrationdon = DateTime.ParseExact(txtCalibrationdon.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //ctyp.txtCalibrationdue = DateTime.ParseExact(txtCalibrationdue.Text.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            ctyp.UserId = Convert.ToInt32(objSession.employeeid);

            string AttachmentName = "";

            if (documentUpload.HasFile)
            {
                string extn = Path.GetExtension(documentUpload.PostedFile.FileName).ToUpper();
                AttachmentName = Path.GetFileName(documentUpload.PostedFile.FileName);
            }
            string[] extension = AttachmentName.Split('.');
            AttachmentName = extension[0] + '_' + txtCodeno.Text + '.' + extension[extension.Length - 1];

            string gwoiDocsSavepathstr = QualityReportSavePath + "QualityReportSavePath" + "\\";

            if (!Directory.Exists(gwoiDocsSavepathstr))
                Directory.CreateDirectory(gwoiDocsSavepathstr);

            if (AttachmentName != "")
                documentUpload.SaveAs(gwoiDocsSavepathstr + AttachmentName);
            ctyp.fbUpload = AttachmentName;
            dsw = ctyp.VCallibrationMaster();

            if (dsw.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Records Saved Successfully');showDataTable();", true);

            else if (dsw.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "SuccessMessage('Success','Records Updated Successfully');hideLoader();showDataTable();", true);

            else if (dsw.Tables[0].Rows[0]["Message"].ToString() == "AE")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "InfoMessage('Information','Records Already Exists');hideLoader();", true);

            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "ErrorMessage('Error','Error');hideLoader();", true);

            ShowHideControls("view");
            ClearValues();
            BindTestSupplierDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ShowHideControls("view");
            ClearValues();
            CIDHDN.Value = "0";
            //SUPVENID.Value = "0";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowDataTable", "showDataTable();", true);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"gridView Events"

    protected void gvCallibration_RowDataCommand(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[1].Attributes.Add("style", "text-align:left;");
                e.Row.Cells[2].Attributes.Add("style", "text-align:left;");
                e.Row.Cells[3].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[4].Attributes.Add("style", "text-align:center;");
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[1].Attributes.Add("style", "text-align:left;");
                e.Row.Cells[2].Attributes.Add("style", "text-align:left;");
                e.Row.Cells[3].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[4].Attributes.Add("style", "text-align:center;");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    protected void gvCallibration_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        ctyp = new CallibrationType();
        int index;
        try
        {
            index = Convert.ToInt32(e.CommandArgument.ToString());

            CIDHDN.Value = gvCallibration.DataKeys[index].Values[0].ToString();
            ctyp.CID = Convert.ToInt32(CIDHDN.Value);
            ds = ctyp.GetCallibrationDetails();
            if (e.CommandName.ToString() == "EditCallibrationDetails")
            {
                //Label lblcode = (Label)gvCallibration.Rows[index].FindControl("lblcode");
                //Label lblRange = (Label)gvCallibration.Rows[index].FindControl("lblRange");
                //Label lblCertificateNo = (Label)gvCallibration.Rows[index].FindControl("lblCertificateNo");
                //Label lblCallibrationDone = (Label)gvCallibration.Rows[index].FindControl("lblCallibrationDone");
                //Label lblCallibrationDue = (Label)gvCallibration.Rows[index].FindControl("lblCallibrationDue");

                //CIDHDN.Value = gvCallibration.DataKeys[index].Values[0].ToString();
                //txtCodeno.Text = lblcode.Text.ToString();
                //txtRange.Text = lblRange.Text.ToString();
                //txtcertificateno.Text = lblCertificateNo.Text.ToString();
                //txtCalibrationdon.Text = DateTime.ParseExact(lblCallibrationDone.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString();
                //txtCalibrationdue.Text = DateTime.ParseExact(lblCallibrationDue.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString();
                //txtCalibrationdue.Text = lblCallibrationDue.Text.ToString();

                CIDHDN.Value = ds.Tables[0].Rows[0]["CID"].ToString();
                txtCodeno.Text = ds.Tables[0].Rows[0]["CodeNo"].ToString();
                txtRange.Text = ds.Tables[0].Rows[0]["Range"].ToString();
                txtcertificateno.Text = ds.Tables[0].Rows[0]["CerfificateNo"].ToString();
                txtCalibrationdon.Text = ds.Tables[0].Rows[0]["CalibrationDon"].ToString();
                txtCalibrationdue.Text = ds.Tables[0].Rows[0]["CalibrationDue"].ToString();

                if (this.gvCallibration.Rows.Count > 0)
                {
                    gvCallibration.UseAccessibleHeader = true;
                    gvCallibration.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "showDataTable();", true);
                ShowHideControls("add");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void viewWorkOrderDrawingFile(string index)
    {
        try
        {
            string FileName = gvCallibration.DataKeys[Convert.ToInt32(index)].Values[1].ToString();
            // objc.ViewFileName(RFPDocsSavePath, RFPDocsHttpPath, FileName, "RFPDispatchDocs", ifrm);//
            cCommon.DownLoad(FileName, QualityReportSavePath + "Documents" + "\\" + FileName);

           // cCommon objc = new cCommon();
            //.ViewFileName(QualityReportSavePath, QualityReportHttpPath, ViewState["FileName"].ToString(), ViewState["GWOID"].ToString(), ifrm);
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
                var worksheet = workbook.Worksheets.Add("CallibrationDetails");

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
                    worksheet.Cells[1, 0].Row.Height = 500;
                    worksheet.Cells.GetSubrangeAbsolute(1, 0, 1, columnCount).Merged = true;
                    worksheet.Cells.GetSubrangeAbsolute(1, 0, 1, columnCount).Style.Font.Weight = ExcelFont.BoldWeight;
                    worksheet.Cells.GetSubrangeAbsolute(1, 0, 1, columnCount).Style.Font.Size = 18 * 20;
                    worksheet.Cells.GetSubrangeAbsolute(1, 0, 1, columnCount).Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
                }

                if (startRow == 0)
                    startRow = 2;

                worksheet.Cells.GetSubrangeAbsolute(2, 0, 2, columnCount).Style.Font.Name = "Times New Roman";
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
    private void BindTestSupplierDetails()
    {
        DataSet ds = new DataSet();
        ctyp = new CallibrationType();
        try
        {
            ctyp.CID = Convert.ToInt32(null);
            ds = ctyp.GetCallibrationDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvCallibration.DataSource = ds.Tables[0];
                gvCallibration.DataBind();
                ViewState["CallibrationDetails"] = ds.Tables[0];
                gvCallibration.UseAccessibleHeader = true;
                gvCallibration.HeaderRow.TableSection = TableRowSection.TableHeader;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "ShowDataTable();", true);
            }
            else
            {
                gvCallibration.DataSource = "";
                gvCallibration.DataBind();
            }

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    private void ShowHideControls(string mode)
    {
        try
        {
            divAdd.Visible = divInput.Visible = divOutput.Visible = false;

            switch (mode.ToLower())
            {
                case "add":
                    divInput.Visible = true;
                    txtCodeno.Focus();
                    break;
                case "edit":
                    divInput.Visible = true;
                    txtCodeno.Focus();
                    break;
                case "view":
                    divAdd.Visible = divOutput.Visible = true;
                    break;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    private void ClearValues()
    {
        CIDHDN.Value = "0";
        txtCodeno.Text = "";
        txtRange.Text = "";
        txtcertificateno.Text = "";
        txtCalibrationdon.Text = "";
        txtCalibrationdue.Text = "";
       
    }

    #endregion

    #region"PageLoad Complete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvCallibration.Rows.Count > 0)
        {
            gvCallibration.UseAccessibleHeader = true;
            gvCallibration.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    #endregion
}