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

public partial class Annexure_TC_Points : System.Web.UI.Page
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
        txtPoint.Attributes.Add("autocomplete", "off");

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

            _objc.ShowOutputSection(divAdd, divInput, divOutput);
        }
        else
        {
            if (target == "deletegvrow")
            {
                int TACPID = Convert.ToInt32(arg);
                DataSet ds = _objSales.deleteTCPoints(TACPID);
                if (ds.Tables[0].Rows[0]["Message"] == "Deleted")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','TAC Point Deleted successfully');hideLoader();", true);
                }
                else if (ds.Tables[0].Rows[0]["Message"] == "AU")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','TAC Point Already in use. Cannot be deleted');hideLoader();", true);
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
                string TACPID = gvAnnexure.DataKeys[index].Values[0].ToString();
                string TACID = ((Label)gvAnnexure.Rows[index].FindControl("lblTACID")).Text;
                txtPoint.Text = ((Label)gvAnnexure.Rows[index].FindControl("lblPoint")).Text;
                _objSales.GetTACList(ddlTAC);
                ddlTAC.ClearSelection(); //making sure the previous selection has been cleared
                ddlTAC.Items.FindByValue(TACID).Selected = true;
                hdnTACPID.Value = TACPID;
                if (this.gvAnnexure.Rows.Count > 0)
                {
                    gvAnnexure.UseAccessibleHeader = true;
                    gvAnnexure.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "showDataTable();", true);

                _objc.ShowInputSection(divAdd, divInput, divOutput);
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
            if (txtPoint.Text.Trim() != "")
            {
                _objSales.TACPID = Convert.ToInt32(hdnTACPID.Value);
                _objSales.TACID = Convert.ToInt32(ddlTAC.SelectedValue);
                _objSales.Points = txtPoint.Text.Trim();
                DataSet ds = _objSales.saveTCPoints();

                if (ds.Tables[0].Rows[0]["Message"] == "AE")
                {
                    if (this.gvAnnexure.Rows.Count > 0)
                    {
                        gvAnnexure.UseAccessibleHeader = true;
                        gvAnnexure.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','TAC Header Already Exists'); ShowPopup();showDataTable();", true);

                    txtPoint.Focus();
                }
                else
                {

                    if (Convert.ToInt32(hdnTACPID.Value) == 0)
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','TAC Header Saved successfully');hideLoader();", true);
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','TAC Header Updated successfully');hideLoader();", true);

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
            dt.Columns.Remove("TACPID");

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

            _objc.SaveExcelFile("Annexure_TAC_Points.aspx", LetterName, _objSession.employeeid);

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

            gvAnnexure.Columns[3].Visible = false;
            gvAnnexure.Columns[4].Visible = false;

            var sb = new StringBuilder();
            ////divPrintReceiptDetails.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            gvAnnexure.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            string div = sb.ToString();
            //string div = divPrintReceiptDetails1.InnerHtml;
            gvAnnexure.Columns[3].Visible = true;
            gvAnnexure.Columns[4].Visible = true;

            MAXPDFID = _objc.GetMaximumNumberPDF();

            string htmlfile = MAXPDFID + ".html";
            string pdffile = MAXPDFID + ".pdf";
            string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
            string strFile = LetterPath + pdffile;

            string URL = LetterPath + htmlfile;

            _objc.SaveHtmlFile(URL, "Annexture Points", lbltitle.Text, div);

            _objc.GenerateAndSavePDF(LetterPath, strFile, pdffile, URL);

            _objc.SavePDFFile("Annexure_TAC_Points.aspx", pdffile, _objSession.employeeid);

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
            dsAnnexure = _objSales.getTCPoints();
            ViewState["DataTable"] = dsAnnexure.Tables[0];
            _objSales.GetTACList(ddlTAC);
            if (dsAnnexure.Tables[0].Rows.Count > 0)
            {
                divDownload.Visible = true;
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
            hdnTACPID.Value = "0";

            txtPoint.Text = "";
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