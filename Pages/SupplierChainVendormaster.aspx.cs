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

public partial class Pages_LSE_SupplierChainVendormaster : System.Web.UI.Page
{

    #region"Declaration"

    DocumentType objdyt;
    cSession objSession = new cSession();
    string SupplierChainDocsSavepath = ConfigurationManager.AppSettings["SupplierChainDocsSavepath"].ToString();
    string SupplierDocsHttpPath = ConfigurationManager.AppSettings["SupplierDocsHttpPath"].ToString();

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
        if (IsPostBack == false)
            {
                BindTestSupplierDetails();
                ShowHideControls("view");
                
            }
            else
            {
                if (target == "deletegvrow")
                {
                    objdyt = new DocumentType();
                    objdyt.SCVMID = Convert.ToInt32(arg);
               
                DataSet ds = objdyt.DeleteSupplierChainDetails();

                  if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Vendor Details Deleted successfully');", true);
                  else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Vendor Details Not Deleted');", true);

                  BindTestSupplierDetails();
                  ShowHideControls("view");
            }
        }
       
    }

    #endregion

    #region"Button Events"

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        ShowHideControls("add");
        SUPVENID.Value = "0";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataSet dsa = new DataSet();
        objdyt = new DocumentType();
        try
        {
            objdyt.SCVMID = Convert.ToInt32(SUPVENID.Value);
            objdyt.txtVendorname = txtVendorname.Text;
            objdyt.txtContactPerson = txtContactPerson.Text;
            objdyt.txtAddress = txtAddress.Text;
             objdyt.txtContactNo = txtContactNo.Text;
            objdyt.txtGSTNo = txtGSTNo.Text;
            objdyt.txtEmailId = txtEmailId.Text;

            string AttachmentName = "";

            if (fbUpload.HasFile)
            {
                string extn = Path.GetExtension(fbUpload.PostedFile.FileName).ToUpper();
                AttachmentName = Path.GetFileName(fbUpload.PostedFile.FileName);
            }
            string[] extension = AttachmentName.Split('.');
            AttachmentName = extension[0] + '_' + txtVendorname.Text + '.' + extension[extension.Length - 1];

            string SupplierChainDocsPath = SupplierChainDocsSavepath + "GSTDocs" + "\\";

            if (!Directory.Exists(SupplierChainDocsPath))
               Directory.CreateDirectory(SupplierChainDocsPath);

            if (AttachmentName != "")
                fbUpload.SaveAs(SupplierChainDocsPath + AttachmentName);
            objdyt.fbUpload = AttachmentName;
          //  lblMessage.Text = Path.GetFileName(file_upload.FileName) + " has been uploaded.";

            dsa = objdyt.VSupplierChainVendormaster();

            if (dsa.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Records Saved Succeessfully');", true);
                
            else if (dsa.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "SuccessMessage('Success','Records Updated Successfully');hideLoader();", true);
           
            else if (dsa.Tables[0].Rows[0]["Message"].ToString() == "AE")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "InfoMessage('Information','Vendor Name Already Exists');hideLoader();", true);

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
            SUPVENID.Value = "0";
          
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
            DataTable ds = new DataTable();
            ds = (DataTable)ViewState["SupplierChainVendorDetails"];

            ds.Columns.Remove("SCVMID");

            int rowcount = Convert.ToInt32(ds.Rows.Count);
            int ColumnCount = Convert.ToInt32(ds.Columns.Count);

            string strFile = "";
            string LetterName = "SupplierChainVendorDetailsList" + ".xls";
            string LetterPath = ConfigurationManager.AppSettings["ExcelSavePath"].ToString();
            string GemBoxKey = ConfigurationManager.AppSettings["GemBoxKey"].ToString();

            strFile = LetterPath + LetterName;

             if (!Directory.Exists(LetterPath))
                Directory.CreateDirectory(LetterPath);

            if (File.Exists(strFile))
                File.Delete(strFile);

            exportExcel(ds, rowcount, ColumnCount, strFile, LetterName, "", "Supplier Chain Vendor Details List", 2, GemBoxKey);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }


    #endregion

    #region"gridView Events"

    protected void gvSupplierChain_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnedit = (LinkButton)e.Row.FindControl("lbtnEdit");
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("lbtnDelete");

                if (objSession.type == 1) {
                    btnedit.Visible = true;
                    btnDelete.Visible = true;
                }
                   
                else {
                    btnedit.Visible = false;
                    btnDelete.Visible = false;
                }
                    

               
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvSupplierChain_RowDataCommand(object sender, GridViewRowEventArgs e)
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
    protected void gvSupplierChain_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        objdyt = new DocumentType();
        int index;
        try
        {
            index = Convert.ToInt32(e.CommandArgument.ToString());
            SUPVENID.Value = gvSupplierChain.DataKeys[index].Values[0].ToString();
            objdyt.SCVMID = Convert.ToInt32(SUPVENID.Value);
            ds = objdyt.GetSupllierChainDetailsBySUPID();

            if (e.CommandName.ToString() == "EditSupplierChainDetails")
            {
               
                Label lblVendorName = (Label)gvSupplierChain.Rows[index].FindControl("lblvendor");
                Label lblAddress = (Label)gvSupplierChain.Rows[index].FindControl("lbladdress");
                Label lblContactPerson = (Label)gvSupplierChain.Rows[index].FindControl("lblcontactperson");
                Label lblcontactpno = (Label)gvSupplierChain.Rows[index].FindControl("lblcontactpno");
                Label lblEmail = (Label)gvSupplierChain.Rows[index].FindControl("lblEmail");
                Label lblGSTNo = (Label)gvSupplierChain.Rows[index].FindControl("lblGSTNo");
                SUPVENID.Value = gvSupplierChain.DataKeys[index].Values[0].ToString();
                txtVendorname.Text = lblVendorName.Text.ToString();
                txtAddress.Text = lblAddress.Text.ToString();
                txtContactPerson.Text = lblContactPerson.Text.ToString();
                txtContactNo.Text = lblcontactpno.Text.ToString();
                txtEmailId.Text = lblEmail.Text.ToString();
                txtGSTNo.Text = lblGSTNo.Text.ToString();
                if (this.gvSupplierChain.Rows.Count > 0)
                {
                    gvSupplierChain.UseAccessibleHeader = true;
                    gvSupplierChain.HeaderRow.TableSection = TableRowSection.TableHeader;
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

    public void exportExcel(DataTable dt, int rowCount, int columnCount, string fileName, string LetterName, string schoolName, string reportName, int startRow, string gemBoxKey)
    {
        try
        {
            if ((dt.Rows.Count > 0) && (rowCount != 0) && (columnCount != 0))
            {
                string _key = gemBoxKey;
                SpreadsheetInfo.SetLicense(_key);

                var workbook = new ExcelFile();
                var worksheet = workbook.Worksheets.Add("SupplierChainVendorDetails");

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

    private void BindTestSupplierDetails()
    {
        DataSet ds = new DataSet();
        objdyt = new DocumentType();
        try
        {
            ds = objdyt.GetSupllierChainDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvSupplierChain.DataSource = ds.Tables[0];
                gvSupplierChain.DataBind();
                ViewState["SupplierChainVendorDetails"] = ds.Tables[0];
                gvSupplierChain.UseAccessibleHeader = true;
                gvSupplierChain.HeaderRow.TableSection = TableRowSection.TableHeader;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "ShowDataTable();", true);
            }
            else
            {
                gvSupplierChain.DataSource = "";
                gvSupplierChain.DataBind();
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
                    txtVendorname.Focus();
                    break;
                case "edit":
                    divInput.Visible = true;
                    txtVendorname.Focus();
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
        SUPVENID.Value = "0";
        txtVendorname.Text = "";
        txtContactPerson.Text = "";
        txtAddress.Text = "";
        txtContactNo.Text = "";
        txtGSTNo.Text = "";
        txtEmailId.Text = "";

    }

    #endregion

    #region"PageLoad Complete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvSupplierChain.Rows.Count > 0)
        {
            gvSupplierChain.UseAccessibleHeader = true;
            gvSupplierChain.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    #endregion
}