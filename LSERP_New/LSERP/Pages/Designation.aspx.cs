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

public partial class Pages_Designation : System.Web.UI.Page
{
    #region "Declaration"

    DataSet dsDesignation = new DataSet();
    cSession _objSession = new cSession();
    c_HR _objHR = new c_HR();
    cCommon _objc = new cCommon();
    cCommonMaster _objCommon = new cCommonMaster();

    #endregion

    #region "Page Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        txtDesignationName.Attributes.Add("autocomplete", "off");        

        _objSession = Master.csSession;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string target = Request["__EVENTTARGET"];
        string arg = Request["__EVENTARGUMENT"];

        if (IsPostBack == false)
        {
            bindDesignation();
            _objCommon.AccessPrintPermissions(btnprint, imgExcel, imgPdf, _objSession.employeeid);
            divAdd.Style.Add("display", "block");
            divInput.Style.Add("display", "none");
            divOutput.Style.Add("display", "block");
        }
        else
        {
            if (target == "deletegvrow")
            {
                int DesignationID = Convert.ToInt32(arg);
                DataSet ds = _objHR.deleteDesignation(DesignationID);
                if (ds.Tables[0].Rows[0]["Message"] == "Deleted")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Designation Deleted successfully');hideLoader();", true);
                }

                divAdd.Style.Add("display", "block");
                divInput.Style.Add("display", "none");
                divOutput.Style.Add("display", "block");

                bindDesignation();
            }
        }
    }

    #endregion

    #region "GridView Events"

    protected void gvDesignation_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToString() == "ViewDesignation")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                string DesignationID = gvDesignation.DataKeys[index].Values[0].ToString();
                txtDesignationName.Text = ((Label)gvDesignation.Rows[index].FindControl("lblDesignation")).Text;                
                hdnDesignationID.Value = DesignationID;
                if (this.gvDesignation.Rows.Count > 0)
                {
                    gvDesignation.UseAccessibleHeader = true;
                    gvDesignation.HeaderRow.TableSection = TableRowSection.TableHeader;
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
            if (txtDesignationName.Text.Trim() != "")
            {
                _objHR.DesignationID = Convert.ToInt32(hdnDesignationID.Value);
                _objHR.DesignationName = txtDesignationName.Text.Trim();
                DataSet ds = _objHR.saveDesignation();

                if (ds.Tables[0].Rows[0]["Message"] == "AE")
                {
                    if (this.gvDesignation.Rows.Count > 0)
                    {
                        gvDesignation.UseAccessibleHeader = true;
                        gvDesignation.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Designation Already Exists'); ShowPopup();showDataTable();", true);

                    txtDesignationName.Focus();
                }
                else
                {

                    if (Convert.ToInt32(hdnDesignationID.Value) == 0)
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Designation Saved successfully');hideLoader();", true);
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Designation Updated successfully');hideLoader();", true);

                    bindDesignation();
                }
            }

            else
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErroMessage('Error','Field is required'); ShowPopup();showDataTable();", true);
            }

            divInput.Style.Add("display", "block");
            divAdd.Style.Add("display", "none");
            divOutput.Style.Add("display", "none");

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
            dt.Columns.Remove("DesignationID");

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

            _objc.SaveExcelFile("Department.aspx", LetterName, _objSession.employeeid);

            bindDesignation();

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

            gvDesignation.Columns[3].Visible = false;
            gvDesignation.Columns[4].Visible = false;

            var sb = new StringBuilder();
            ////divPrintReceiptDetails.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            gvDesignation.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            string div = sb.ToString();
            //string div = divPrintReceiptDetails1.InnerHtml;
            gvDesignation.Columns[3].Visible = true;
            gvDesignation.Columns[4].Visible = true;

            MAXPDFID = _objc.GetMaximumNumberPDF();

            string htmlfile = MAXPDFID + ".html";
            string pdffile = MAXPDFID + ".pdf";
            string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
            string strFile = LetterPath + pdffile;

            string URL = LetterPath + htmlfile;         

            _objc.SaveHtmlFile(URL,"Designation Details",lbltitle.Text,div);

            _objc.GenerateAndSavePDF(LetterPath, strFile, pdffile, URL);

            _objc.SavePDFFile("Department.aspx", pdffile, _objSession.employeeid);

            bindDesignation();

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region Common Methods

    private void bindDesignation()
    {

        try
        {
            dsDesignation = _objHR.getDesignation();
            ViewState["DataTable"] = dsDesignation.Tables[0];
            //ddlEmployee = _objCommon.GetEmployeeName(ddlEmployee);
            if (dsDesignation.Tables[0].Rows.Count > 0)
            {
                gvDesignation.DataSource = dsDesignation.Tables[0];
                gvDesignation.DataBind();
                gvDesignation.UseAccessibleHeader = true;
                gvDesignation.HeaderRow.TableSection = TableRowSection.TableHeader;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "hide loader", "showDataTable();", true);
            }
            else
            {
                gvDesignation.DataSource ="";
                gvDesignation.DataBind();
            }
            hdnDesignationID.Value = "0";

            txtDesignationName.Text = "";
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