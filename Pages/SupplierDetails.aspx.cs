using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;
using System.IO;
using System.Configuration;
using GemBox.Spreadsheet;

public partial class Pages_SupplierDetails : System.Web.UI.Page
{

    #region"Declaration"

    cCommon objc;
    cMaterials objMat;
    cPurchase objPur;
    string SupplierDocsSavepath = ConfigurationManager.AppSettings["SupplierDocsSavepath"].ToString();
    string SupplierDocsHttpPath = ConfigurationManager.AppSettings["SupplierDocsHttpPath"].ToString();

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
                bindSupplierDetails();
                ShowHideControls("view");
            }
            else
            {
                if (target == "deletegvrow")
                {
                    objPur = new cPurchase();

                    objPur.SUPID = Convert.ToInt32(arg);
                    DataSet ds = objPur.DeleteSupplierDetails();

                    if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Supplier Name Deleted successfully');", true);
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);

                    bindSupplierDetails();
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

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        ShowHideControls("add");
        hdnSUPID.Value = "0";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ShowHideControls("view");
        ClearValues();
        hdnSUPID.Value = "0";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objc = new cCommon();
        // objMat = new cMaterials();
        objPur = new cPurchase();
        try
        {

            objPur.SUPID = Convert.ToInt32(hdnSUPID.Value);
            objPur.SupplierName = txtSuppliername.Text;
            objPur.Description = txtDescription.Text;
            objPur.Address = txtAddress.Text;
            objPur.ContactPerson = txtContactPerson.Text;
            objPur.ContactNumber = txtContactNo.Text;
            objPur.FaxNo = txtFaxNo.Text;
            objPur.EmailID = txtEmailId.Text;
            objPur.GSTNo = txtGSTNo.Text;
            objPur.SubVendorFlag = rblSubvendorFlag.SelectedValue;

            string AttachmentName = "";

            if (fGSTNAttach.HasFile)
            {
                string extn = Path.GetExtension(fGSTNAttach.PostedFile.FileName).ToUpper();
                AttachmentName = Path.GetFileName(fGSTNAttach.PostedFile.FileName);
            }
            string[] extension = AttachmentName.Split('.');
            AttachmentName = extension[0] + '_' + txtSuppliername.Text + '.' + extension[extension.Length - 1];

            string SupplierGSTDocsPath = SupplierDocsSavepath + "GSTDocs" + "\\";

            if (!Directory.Exists(SupplierGSTDocsPath))
                Directory.CreateDirectory(SupplierGSTDocsPath);

            if (AttachmentName != "")
                fGSTNAttach.SaveAs(SupplierGSTDocsPath + AttachmentName);
            objPur.AttachementName = AttachmentName;

            ds = objPur.SaveSuppliereDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "SuccessMessage('Success','Supplier Name Saved Successfully');hideLoader();", true);

            else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "SuccessMessage('Success','Supplier Name Updated Successfully');hideLoader();", true);

            else if (ds.Tables[0].Rows[0]["Message"].ToString() == "AE")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "InfoMessage('Information','Supplier Name Already Exists');hideLoader();", true);

            ShowHideControls("view");
            ClearValues();
            bindSupplierDetails();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "ErrorMessage('Error','Error Occured');hideLoader();", true);
            Log.Message(ex.ToString());
        }
    }

    protected void btnviewGSTDocs_Click(object sender, EventArgs e)
    {
        try
        {
            objc = new cCommon();
            objc.ViewFileName(SupplierDocsSavepath, SupplierDocsHttpPath, ViewState["GSTDocs"].ToString(), "GSTDocs", ifrm);
            //else
            //    cCommon.DownLoad(FileName, cc + ViewState["EnquiryNumber"].ToString() + "\\" + FileName);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "ShowViewPopup();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnExcelDownload_Click(object sender, EventArgs e)
    {
        try
        {
            string MAXEXID = "";
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["SupplierDetails"];

            dt.Columns.Remove("SUPID");

            int rowcount = Convert.ToInt32(dt.Rows.Count);
            int ColumnCount = Convert.ToInt32(dt.Columns.Count);

            string strFile = "";
            string LetterName = "SupplierDetailsList" + ".xls";
            string LetterPath = ConfigurationManager.AppSettings["ExcelSavePath"].ToString();
            string GemBoxKey = ConfigurationManager.AppSettings["GemBoxKey"].ToString();

            strFile = LetterPath + LetterName;

            if (!Directory.Exists(LetterPath))
                Directory.CreateDirectory(LetterPath);

            if (File.Exists(strFile))
                File.Delete(strFile);

            exportExcel(dt, rowcount, ColumnCount, strFile, LetterName, "", "Supplier Details List", 2, GemBoxKey);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"gridView Events"

    protected void gvSupplier_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        // objMat = new cMaterials();
        objPur = new cPurchase();
        int index;
        try
        {
            index = Convert.ToInt32(e.CommandArgument.ToString());

            hdnSUPID.Value = gvSupplier.DataKeys[index].Values[0].ToString();
            objPur.SUPID = Convert.ToInt32(hdnSUPID.Value);
            ds = objPur.GetSupllierDetailsBySUPID();

            if (e.CommandName.ToString() == "EditSupplierDetails")
            {
                txtSuppliername.Text = ds.Tables[0].Rows[0]["SupplierName"].ToString();
                txtDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();

                txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                txtContactPerson.Text = ds.Tables[0].Rows[0]["ContactPerson"].ToString();
                txtContactNo.Text = ds.Tables[0].Rows[0]["ContactNo"].ToString();
                txtFaxNo.Text = ds.Tables[0].Rows[0]["FaxNo"].ToString();
                txtEmailId.Text = ds.Tables[0].Rows[0]["EmailID"].ToString();
                txtGSTNo.Text = ds.Tables[0].Rows[0]["GSTNo"].ToString();
                rblSubvendorFlag.SelectedValue = ds.Tables[0].Rows[0]["SubVendorFlagEdit"].ToString();
                ShowHideControls("add");
            }
            else if (e.CommandName == "ViewSupplierDetails")
            {
                lblSuppliername_h.Text = ds.Tables[0].Rows[0]["SupplierName"].ToString();

                lblAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
                lblContactperson.Text = ds.Tables[0].Rows[0]["ContactPerson"].ToString();
                lblcontactno.Text = ds.Tables[0].Rows[0]["ContactNo"].ToString();
                lblfax.Text = ds.Tables[0].Rows[0]["FaxNo"].ToString();
                lblEmailId.Text = ds.Tables[0].Rows[0]["EmailID"].ToString();
                lblGSTNo.Text = ds.Tables[0].Rows[0]["GSTNo"].ToString();
                lblSubVendor.Text = ds.Tables[0].Rows[0]["SubVendorFlagView"].ToString();

                ViewState["GSTDocs"] = ds.Tables[0].Rows[0]["GSTDocs"].ToString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "ShowViewPopup();", true);
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
                var worksheet = workbook.Worksheets.Add("SupplierDetails");

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
                    worksheet.Columns[i].AutoFit(1, worksheet.Rows[1], worksheet.Rows[worksheet.Rows.Count - 1]);
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

    private void bindSupplierDetails()
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            ds = objMat.GetSupllierDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvSupplier.DataSource = ds.Tables[0];
                gvSupplier.DataBind();

                ViewState["SupplierDetails"] = ds.Tables[0];

                gvSupplier.UseAccessibleHeader = true;
                gvSupplier.HeaderRow.TableSection = TableRowSection.TableHeader;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "ShowDataTable();", true);
            }
            else
            {
                gvSupplier.DataSource = "";
                gvSupplier.DataBind();
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
                    txtSuppliername.Focus();
                    break;
                case "edit":
                    divInput.Visible = true;
                    txtSuppliername.Focus();
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
        hdnSUPID.Value = "0";
        txtSuppliername.Text = "";
        txtDescription.Text = "";
    }

    #endregion

    #region"PageLoad Complete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvSupplier.Rows.Count > 0)
        {
            gvSupplier.UseAccessibleHeader = true;
            gvSupplier.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    #endregion
}