using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Net;
using eplus.data;
using System.IO;
using eplus.core;

public partial class Annexure_TC : System.Web.UI.Page
{
    #region "Declaration"

    DataSet dsAnnexure = new DataSet();
    cSession _objSession = new cSession();
    cSales _objSales = new cSales();
    cCommon _objc = new cCommon();
    cCommonMaster _objCommon = new cCommonMaster();

    #endregion

    #region "Page Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        txtHeaderName.Attributes.Add("autocomplete", "off");

        _objSession = Master.csSession;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string target = Request["__EVENTTARGET"];
        string arg = Request["__EVENTARGUMENT"];

        if (IsPostBack == false)
        {
            bindAnnexure();
            _objCommon.AccessPrintPermissions(btnprint, imgExcel, imgPdf, _objSession.employeeid);

            _objc.ShowOutputSection(divAdd,divInput,divOutput);
        }
        else
        {
            if (target == "deletegvrow")
            {
                int TACID = Convert.ToInt32(arg);
                DataSet ds = _objSales.deleteTC(TACID);
                if (ds.Tables[0].Rows[0]["Message"] == "Deleted")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','T&C Header Deleted successfully');hideLoader();", true);
                }
                else if (ds.Tables[0].Rows[0]["Message"] == "AU")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','T&C Header Already in use. Cannot be deleted');hideLoader();", true);

                }


                _objc.ShowOutputSection(divAdd, divInput, divOutput);

                bindAnnexure();
            }
        }
    }

    #endregion

    #region "GridView Events"

    protected void gvAnnexure_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToString() == "ViewAnnexure")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                string TACID = gvAnnexure.DataKeys[index].Values[0].ToString();
                txtHeaderName.Text = ((Label)gvAnnexure.Rows[index].FindControl("lblAnnexure")).Text;
                hdnTACID.Value = TACID;
                if (this.gvAnnexure.Rows.Count > 0)
                {
                    gvAnnexure.UseAccessibleHeader = true;
                    gvAnnexure.HeaderRow.TableSection = TableRowSection.TableHeader;
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

    #region "Button Events"

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtHeaderName.Text.Trim() != "")
            {
                _objSales.TACID = Convert.ToInt32(hdnTACID.Value);
                _objSales.HeaderName = txtHeaderName.Text.Trim();
                DataSet ds = _objSales.saveTC();

                if (ds.Tables[0].Rows[0]["Message"] == "AE")
                {
                    if (this.gvAnnexure.Rows.Count > 0)
                    {
                        gvAnnexure.UseAccessibleHeader = true;
                        gvAnnexure.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','T&C Header Already Exists'); ShowPopup();showDataTable();", true);

                    txtHeaderName.Focus();
                }
                else
                {

                    if (Convert.ToInt32(hdnTACID.Value) == 0)
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','T&C Header Saved successfully');hideLoader();", true);
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','T&C Header Updated successfully');hideLoader();", true);

                    bindAnnexure();
                }
            }

            else
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErroMessage('Error','Field is required'); ShowPopup();showDataTable();", true);
            }

            _objc.ShowOutputSection(divAdd, divInput, divOutput);

        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
        finally
        {

        }
    }

    protected void btnExcelDownload_Click(object sender, EventArgs e)
    {
        try
        {
            string MAXEXID = "";
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["DataTable"];
            dt.Columns.Remove("TACID");

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

            _objc.SaveExcelFile("Annexure_TC.aspx", LetterName, _objSession.employeeid);

            bindAnnexure();

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

            gvAnnexure.Columns[2].Visible = false;
            gvAnnexure.Columns[3].Visible = false;

            var sb = new StringBuilder();
            ////divPrintReceiptDetails.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            gvAnnexure.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            string div = sb.ToString();
            //string div = divPrintReceiptDetails1.InnerHtml;
            gvAnnexure.Columns[2].Visible = true;
            gvAnnexure.Columns[3].Visible = true;

            MAXPDFID = _objc.GetMaximumNumberPDF();

            string htmlfile = MAXPDFID + ".html";
            string pdffile = MAXPDFID + ".pdf";
            string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
            string strFile = LetterPath + pdffile;

            string URL = LetterPath + htmlfile;         

            _objc.SaveHtmlFile(URL, "Annexure_TC", lbltitle.Text, div);

            _objc.GenerateAndSavePDF(LetterPath, strFile, pdffile, URL);

            _objc.SavePDFFile("Annexure_TC.aspx", pdffile, _objSession.employeeid);

            bindAnnexure();

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region Common Methods

    private void bindAnnexure()
    {

        try
        {
            dsAnnexure = _objSales.getTC();
            ViewState["DataTable"] = dsAnnexure.Tables[0];
            //ddlEmployee = _objCommon.GetEmployeeName(ddlEmployee);
            if (dsAnnexure.Tables[0].Rows.Count > 0)
            {
                gvAnnexure.DataSource = dsAnnexure.Tables[0];
                gvAnnexure.DataBind();
                gvAnnexure.UseAccessibleHeader = true;
                gvAnnexure.HeaderRow.TableSection = TableRowSection.TableHeader;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "hide loader", "showDataTable();", true);
            }
            else
            {
                gvAnnexure.DataSource = "";
                gvAnnexure.DataBind();
            }
            hdnTACID.Value = "0";

            txtHeaderName.Text = "";
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
        finally
        {

        }
    }

    #endregion

}