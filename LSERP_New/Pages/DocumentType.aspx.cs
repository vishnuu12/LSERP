using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using eplus.core;
using System.Configuration;
using System.IO;
using System.Text;


public partial class Pages_DocumentType : System.Web.UI.Page
{

    #region "Declarations"

    DataSet dsDocumentTypeDetails = new DataSet();
    cSession _objSess = new cSession();
    cCommonMaster _objCommon = new cCommonMaster();
    cCommon _objc = new cCommon();

    #endregion

    #region "Page Init Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        _objSess = Master.csSession;
    }

    #endregion

    #region "Page Load Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        string target = Request["__EVENTTARGET"];
        string arg = Request["__EVENTARGUMENT"];

        if (IsPostBack == false)
        {
            bindDocumentType();
            _objCommon.AccessPrintPermissions(btnprint, imgExcel, imgPdf, _objSess.employeeid);
            _objc.ShowOutputSection(divAdd, divInput, divOutput);
            loadBox();
        }
        else
        {
            if (target == "deletegvrow")
            {
                int DocumentTypeId = Convert.ToInt32(arg);
                DataSet ds = _objCommon.deleteDocumentType(DocumentTypeId);

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','DocumentType Deleted successfully');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "InfoMessage('Information','You Cannot Delete The Record');", true);

                bindDocumentType();

                _objc.ShowOutputSection(divAdd, divInput, divOutput);
            }
        }
    }

    #endregion

    #region "Button Events"

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if ((txtDocumentType.Text.Trim() != "") && (txtExtension.Text.Trim() != ""))
            {
                _objCommon.DocumentTypeId = Convert.ToInt32(hdnDocumentTypeID.Value);
                _objCommon.DocumentType = txtDocumentType.Text.Trim();
                _objCommon.Extension = txtExtension.Text.Trim();

                DataSet ds = _objCommon.saveDocumentType();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "AE")
                {
                    if (this.gvDocumentType.Rows.Count > 0)
                    {
                        gvDocumentType.UseAccessibleHeader = true;
                        gvDocumentType.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "hideLoader();ErrorMessage('Error','DocumentType Already Exists');showDataTable();", true);

                    txtDocumentType.Focus();
                }
                else
                {
                    if (Convert.ToInt32(hdnDocumentTypeID.Value) == 0)
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "hideLoader();SuccessMessage('Success','DocumentType Saved successfully');", true);
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "hideLoader();SuccessMessage('Success','DocumentType Updated successfully');", true);

                    bindDocumentType();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErroMessage('Error','Field is required');showDataTable();", true);
            }

            _objc.ShowInputSection(divAdd, divInput, divOutput);

        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
    }

    protected void btnExcelDownload_Click(object sender, EventArgs e)
    {
        try
        {
            string MAXEXID = "";
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["DataTable"];
            dt.Columns.Remove("DocumentTypeId");

            MAXEXID = _objc.GetMaximumNumberExcel();

            int rowcount = Convert.ToInt32(dt.Rows.Count);
            int ColumnCount = Convert.ToInt32(dt.Columns.Count);

            string strFile = "";

            string LetterName = MAXEXID + ".xlsx";

            string LetterPath = ConfigurationManager.AppSettings["ExcelSavePath"].ToString();

            string GemBoxKey = ConfigurationManager.AppSettings["GemBoxKey"].ToString();

            strFile = LetterPath + LetterName;

            if (!Directory.Exists(LetterPath))
                Directory.CreateDirectory(LetterPath);

            if (File.Exists(strFile))
            {
                File.Delete(strFile);
            }

            _objc.exportExcel(dt, rowcount, ColumnCount, strFile, LetterName, "LoneStar", lbltitle.Text, 2, GemBoxKey);
            _objc.SaveExcelFile("DocumentType.aspx", LetterName, _objSess.employeeid);

            bindDocumentType();

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnPDFDownload_Click(object sender, EventArgs e)
    {
        try
        {
            string MAXPDFID = "";

            gvDocumentType.Columns[3].Visible = false;
            gvDocumentType.Columns[4].Visible = false;

            var sb = new StringBuilder();
            ////divPrintReceiptDetails.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            gvDocumentType.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            string div = sb.ToString();
            //string div = divPrintReceiptDetails1.InnerHtml;
            gvDocumentType.Columns[3].Visible = true;
            gvDocumentType.Columns[4].Visible = true;

            MAXPDFID = _objc.GetMaximumNumberPDF();

            string htmlfile = MAXPDFID + ".html";
            string pdffile = MAXPDFID + ".pdf";
            string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
            string strFile = LetterPath + pdffile;

            string URL = LetterPath + htmlfile;

            _objc.SaveHtmlFile(URL, "Document Type Details", lbltitle.Text, div);

            _objc.GenerateAndSavePDF(LetterPath, strFile, pdffile, URL);

            _objc.SavePDFFile("DocumentType.aspx", pdffile, _objSess.employeeid);

            bindDocumentType();

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    #endregion

    #region"GridView Events"

    protected void gvDocumentType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //if (e.Row.RowType == DataControlRowType.Header)
            //{
            //    e.Row.Cells[0].Attributes.Add("style", "text-align:center;");
            //    e.Row.Cells[1].Attributes.Add("style", "text-align:left;");
            //    e.Row.Cells[2].Attributes.Add("style", "text-align:left;");
            //    e.Row.Cells[3].Attributes.Add("style", "text-align:center;");
            //    e.Row.Cells[4].Attributes.Add("style", "text-align:center;");
            //}
            //else if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    e.Row.Cells[0].Attributes.Add("style", "text-align:center;");
            //    e.Row.Cells[1].Attributes.Add("style", "text-align:left;");
            //    e.Row.Cells[2].Attributes.Add("style", "text-align:left;");
            //    e.Row.Cells[3].Attributes.Add("style", "text-align:center;");
            //    e.Row.Cells[4].Attributes.Add("style", "text-align:center;");
            //}
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvDocumentType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToString() == "EditDocumentType")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                string DocumentTypeID = gvDocumentType.DataKeys[index].Values[0].ToString();
                txtDocumentType.Text = ((Label)gvDocumentType.Rows[index].FindControl("lblDocumentType")).Text;
                txtExtension.Text = ((Label)gvDocumentType.Rows[index].FindControl("lblExtension")).Text;
                hdnDocumentTypeID.Value = DocumentTypeID;
                if (this.gvDocumentType.Rows.Count > 0)
                {
                    gvDocumentType.UseAccessibleHeader = true;
                    gvDocumentType.HeaderRow.TableSection = TableRowSection.TableHeader;
                }

                _objc.ShowInputSection(divAdd, divInput, divOutput);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "showDataTable();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
    }

    #endregion

    #region "Common Methods"

    private void bindDocumentType()
    {
        try
        {
            dsDocumentTypeDetails = _objCommon.GetDocumnetType();

            ViewState["DataTable"] = dsDocumentTypeDetails.Tables[0];

            if (dsDocumentTypeDetails.Tables[0].Rows.Count > 0)
            {
                divDownload.Visible = true;
                gvDocumentType.DataSource = dsDocumentTypeDetails.Tables[0];
                gvDocumentType.DataBind();

                gvDocumentType.UseAccessibleHeader = true;
                gvDocumentType.HeaderRow.TableSection = TableRowSection.TableHeader;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "hide loader", "showDataTable();", true);
            }

            else
            {
                divDownload.Visible = false;
                gvDocumentType.DataSource = "";
                gvDocumentType.DataBind();
            }

            hdnDocumentTypeID.Value = "0";
            txtDocumentType.Text = "";
            txtExtension.Text = "";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }


    private void loadBox()
    {
        c_HR objH = new c_HR();
        DataSet ds = new DataSet();
        try
        {
            ds = objH.getDepartment();
            if (ds.Tables[0].Rows.Count > 0)
            {
                lstDepartments.DataSource = ds.Tables[0];
                lstDepartments.DataTextField = "DepartmentName";
                lstDepartments.DataValueField = "DepartmentID";
                lstDepartments.DataBind();

                lstDepartments.Items.Insert(0, new ListItem("--Select--", "0"));
            }

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

}