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

public partial class Pages_TaxMaster : System.Web.UI.Page
{
    #region "Declaration"

    cSession _objSession = new cSession();
    cCommonMaster _objCommon = new cCommonMaster();
    c_Finance _objFin = new c_Finance();
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
            bindTaxMaster();
            _objCommon.AccessPrintPermissions(btnprint, imgExcel, imgPdf, _objSession.employeeid);
            divAdd.Style.Add("display", "block");
            divInput.Style.Add("display", "none");
            divOutput.Style.Add("display", "block");
        }
        else
        {
            if (target == "deletegvrow")
            {
                int TaxId = Convert.ToInt32(arg);
                DataSet ds = _objFin.deleteTaxMaster(TaxId);
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Tax Name Deleted successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "InfoMessage('Information','You Cannot Delete The Record');", true);
                }

                divAdd.Style.Add("display", "block");
                divInput.Style.Add("display", "none");
                divOutput.Style.Add("display", "block");

                bindTaxMaster();
            }
        }
    }

    #endregion

    #region "Button Events"

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if ((txtTaxName.Text.Trim() != "") && (txtDescription.Text.Trim() != ""))
            {
                _objFin.TaxId = Convert.ToInt32(hdnTaxID.Value);
                _objFin.TaxName = txtTaxName.Text.Trim();
                _objFin.Description = txtDescription.Text.Trim();

                DataSet ds = _objFin.saveTaxMaster();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "AE")
                {
                    if (this.gvTaxMaster.Rows.Count > 0)
                    {
                        gvTaxMaster.UseAccessibleHeader = true;
                        gvTaxMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','TaxName Already Exists');hideLoader();", true);

                    txtTaxName.Focus();
                }
                else
                {
                    if (Convert.ToInt32(hdnTaxID.Value) == 0)
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "hideLoader();SuccessMessage('Success','TaxName Saved successfully');", true);
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "hideLoader();SuccessMessage('Success','TaxName Updated successfully');", true);

                    bindTaxMaster();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErroMessage('Error','Field is required');showDataTable();", true);
            }

            divInput.Style.Add("display", "block");
            divAdd.Style.Add("display", "none");
            divOutput.Style.Add("display", "none");
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
            dt.Columns.Remove("TaxId");

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

            _objc.SaveExcelFile("TaxMaster.aspx", LetterName, _objSession.employeeid);

            bindTaxMaster();

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

            gvTaxMaster.Columns[3].Visible = false;
            gvTaxMaster.Columns[4].Visible = false;

            var sb = new StringBuilder();
            ////divPrintReceiptDetails.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            gvTaxMaster.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            string div = sb.ToString();
            //string div = divPrintReceiptDetails1.InnerHtml;
            gvTaxMaster.Columns[3].Visible = true;
            gvTaxMaster.Columns[4].Visible = true;

            MAXPDFID = _objc.GetMaximumNumberPDF();

            string htmlfile = MAXPDFID + ".html";
            string pdffile = MAXPDFID + ".pdf";
            string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
            string strFile = LetterPath + pdffile;

            string URL = LetterPath + htmlfile;          

            _objc.SaveHtmlFile(URL,"Tax Master Details",lbltitle.Text,div);

            _objc.GenerateAndSavePDF(LetterPath, strFile, pdffile, URL);

            _objc.SavePDFFile("TaxMaster.aspx", pdffile, _objSession.employeeid);

            bindTaxMaster();

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

    protected void gvTaxMaster_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gvTaxMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToString() == "EditTaxName")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                string taxId = gvTaxMaster.DataKeys[index].Values[0].ToString();
                txtTaxName.Text = ((Label)gvTaxMaster.Rows[index].FindControl("lblTaxName")).Text;
                txtDescription.Text = ((Label)gvTaxMaster.Rows[index].FindControl("lblDescription")).Text;
                hdnTaxID.Value = taxId;
                if (this.gvTaxMaster.Rows.Count > 0)
                {
                    gvTaxMaster.UseAccessibleHeader = true;
                    gvTaxMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "showDataTable();", true);

                divAdd.Style.Add("display", "none");
                divInput.Style.Add("display", "block");
                divOutput.Style.Add("display", "none");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
    }

    #endregion

    #region "Common Methods"

    private void bindTaxMaster()
    {
        try
        {
            DataSet dsTaxDetails = new DataSet();
            dsTaxDetails = _objFin.GetTaxMasterDetails();

            ViewState["DataTable"] = dsTaxDetails.Tables[0];

            if (dsTaxDetails.Tables[0].Rows.Count > 0)
            {
                divDownload.Visible = true;
                gvTaxMaster.DataSource = dsTaxDetails.Tables[0];
                gvTaxMaster.DataBind();

                gvTaxMaster.UseAccessibleHeader = true;
                gvTaxMaster.HeaderRow.TableSection = TableRowSection.TableHeader;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "function", "showDataTable();", true);
            }

            else
            {
                divDownload.Visible = false;
                gvTaxMaster.DataSource = "";
                gvTaxMaster.DataBind();
            }

            hdnTaxID.Value = "0";
            txtTaxName.Text = "";
            txtDescription.Text = "";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}