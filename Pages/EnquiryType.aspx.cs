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

public partial class Pages_EnquiryType : System.Web.UI.Page
{

    #region "Declaration"

    DataSet dsEnquiryTypeDetails = new DataSet();
    cSession _objSession = new cSession();
    cCommonMaster _objCommon = new cCommonMaster();
    cSales _objSales = new cSales();
    cCommon _objc = new cCommon();

    #endregion

    #region "PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        _objSession = Master.csSession;
    }

    #endregion

    #region "PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        string target = Request["__EVENTTARGET"];
        string arg = Request["__EVENTARGUMENT"];

        if (IsPostBack == false)
        {
            bindEnquiryType();
            _objCommon.AccessPrintPermissions(btnprint, imgExcel, imgPdf, _objSession.employeeid);
            _objc.ShowOutputSection(divAdd,divInput,divOutput);
        }
        else
        {
            if (target == "deletegvrow")
            {
                int EnquiryTypeId = Convert.ToInt32(arg);
                DataSet ds = _objSales.deleteEnquiryType(EnquiryTypeId);
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Enquiry Type Name Deleted successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "InfoMessage('Information','You Cannot Delete The Record');", true);
                }

                bindEnquiryType();

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
            if ((txtEnquiryType.Text.Trim() != "") && (txtDescription.Text.Trim() != ""))
            {
                _objSales.EnquiryTypeId = Convert.ToInt32(hdnEnquiryTypeID.Value);
                _objSales.EnquiryTypeName = txtEnquiryType.Text.Trim();
                _objSales.Description = txtDescription.Text.Trim();

                DataSet ds = _objSales.saveEnquiryType();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "AE")
                {
                    if (this.gvEnquiryType.Rows.Count > 0)
                    {
                        gvEnquiryType.UseAccessibleHeader = true;
                        gvEnquiryType.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','EnquiryType Already Exists');hideLoader();", true);

                    txtEnquiryType.Focus();
                }
                else
                {
                    if (Convert.ToInt32(hdnEnquiryTypeID.Value) == 0)
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','EnquiryType Saved successfully');hideLoader();", true);
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','EnquiryType Updated successfully');hideLoader();", true);
                }

                bindEnquiryType();
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
            dt.Columns.Remove("EnquiryTypeId");

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

            _objc.SaveExcelFile("EnquiryType.aspx", LetterName, _objSession.employeeid);

            bindEnquiryType();

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

            gvEnquiryType.Columns[3].Visible = false;
            gvEnquiryType.Columns[4].Visible = false;

            var sb = new StringBuilder();
            ////divPrintReceiptDetails.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            gvEnquiryType.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            string div = sb.ToString();
            //string div = divPrintReceiptDetails1.InnerHtml;
            gvEnquiryType.Columns[3].Visible = true;
            gvEnquiryType.Columns[4].Visible = true;

            MAXPDFID = _objc.GetMaximumNumberPDF();

            string htmlfile = MAXPDFID + ".html";
            string pdffile = MAXPDFID + ".pdf";
            string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
            string strFile = LetterPath + pdffile;

            string URL = LetterPath + htmlfile;          

            _objc.SaveHtmlFile(URL, "Enquiry Type Details", lbltitle.Text, div);

            _objc.GenerateAndSavePDF(LetterPath, strFile, pdffile, URL);

            _objc.SavePDFFile("EnquiryType.aspx", pdffile, _objSession.employeeid);

            bindEnquiryType();

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

    #region "GridView Events"

    protected void gvEnquiryType_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gvEnquiryType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToString() == "EditEnquiryType")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                string DocumentTypeID = gvEnquiryType.DataKeys[index].Values[0].ToString();
                txtEnquiryType.Text = ((Label)gvEnquiryType.Rows[index].FindControl("lblEnquiryType")).Text;
                txtDescription.Text = ((Label)gvEnquiryType.Rows[index].FindControl("lblDescription")).Text;
                hdnEnquiryTypeID.Value = DocumentTypeID;
                if (this.gvEnquiryType.Rows.Count > 0)
                {
                    gvEnquiryType.UseAccessibleHeader = true;
                    gvEnquiryType.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
               
            }

            _objc.ShowInputSection(divAdd,divInput,divOutput);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "showDataTable();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
    }

    #endregion

    #region "Common Methods"

    private void bindEnquiryType()
    {
        try
        {
            dsEnquiryTypeDetails = _objSales.GetEnquiryType();

            ViewState["DataTable"] = dsEnquiryTypeDetails.Tables[0];

            if (dsEnquiryTypeDetails.Tables[0].Rows.Count > 0)
            {
                divDownload.Visible = true;
                gvEnquiryType.DataSource = dsEnquiryTypeDetails.Tables[0];
                gvEnquiryType.DataBind();
                gvEnquiryType.UseAccessibleHeader = true;
                gvEnquiryType.HeaderRow.TableSection = TableRowSection.TableHeader;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "hide loader", "showDataTable();", true);
            }

            else
            {
                divDownload.Visible = false;
                gvEnquiryType.DataSource = "";
                gvEnquiryType.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "hideloader", "HideLoader();", true);
            }

            hdnEnquiryTypeID.Value = "0";
            txtEnquiryType.Text = "";
            txtDescription.Text = "";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

}