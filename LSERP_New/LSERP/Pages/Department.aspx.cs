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
using eplus.core;
using System.IO;
//using eplus.core;

public partial class Pages_Department : System.Web.UI.Page
{
    #region "Declaration"

    DataSet dsDepartment = new DataSet();
    cSession _objSession = new cSession();
    c_HR _objHR = new c_HR();
    cCommon _objc = new cCommon();
    cCommonMaster _objCommon = new cCommonMaster();

    #endregion

    #region "Page Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        txtDepartmentName.Attributes.Add("autocomplete", "off");
        txtDepartmentPrefix.Attributes.Add("autocomplete", "off");

        _objSession = Master.csSession;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string target = Request["__EVENTTARGET"];
        string arg = Request["__EVENTARGUMENT"];

        if (IsPostBack == false)
        {
            bindDepartment();
            _objCommon.AccessPrintPermissions(btnprint, imgExcel, imgPdf, _objSession.employeeid);
            _objc.ShowOutputSection(divAdd, divInput, divOutput);
        }
        else
        {
            if (target == "deletegvrow")
            {
                int DepartmentID = Convert.ToInt32(arg);
                DataSet ds = _objHR.deleteDepartment(DepartmentID);
                if (ds.Tables[0].Rows[0]["Message"] == "Deleted")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Department Deleted successfully');", true);
                }

                _objc.ShowOutputSection(divAdd, divInput, divOutput);

                bindDepartment();
            }
        }
    }

    #endregion

    #region "GridView Events"

    protected void gvDepartment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToString() == "ViewDepartment")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                string DepartmentID = gvDepartment.DataKeys[index].Values[0].ToString();
                txtDepartmentName.Text = ((Label)gvDepartment.Rows[index].FindControl("lblDepartment")).Text;
                //txtDepartmentPrefix.Text = ((Label)gvDepartment.Rows[index].FindControl("lblHOD")).Text;
                txtDepartmentPrefix.Text = ((Label)gvDepartment.Rows[index].FindControl("lblPrefix")).Text;
                hdnDepartmentID.Value = DepartmentID;
                if (this.gvDepartment.Rows.Count > 0)
                {
                    gvDepartment.UseAccessibleHeader = true;
                    gvDepartment.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", " ShowPopup(); showDataTable();", true);

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
            if ((txtDepartmentName.Text.Trim() != "") && (txtDepartmentPrefix.Text.Trim() != ""))
            {
                _objHR.DepartmentID = Convert.ToInt32(hdnDepartmentID.Value);
                _objHR.DepartmentName = txtDepartmentName.Text.Trim();
                _objHR.HOD = Convert.ToInt32(ddlEmployee.SelectedValue);
                _objHR.DepartmentPrefix = txtDepartmentPrefix.Text.Trim();
                DataSet ds = _objHR.saveDepartment();

                if (ds.Tables[0].Rows[0]["Message"] == "AE")
                {
                    if (this.gvDepartment.Rows.Count > 0)
                    {
                        gvDepartment.UseAccessibleHeader = true;
                        gvDepartment.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Department Already Exists');showDataTable();", true);

                    txtDepartmentName.Focus();
                }
                else
                {

                    if (Convert.ToInt32(hdnDepartmentID.Value) == 0)
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Department Saved successfully');hideLoader();", true);
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Department Updated successfully');hideLoader();", true);

                    bindDepartment();
                }
            }

            else
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErroMessage('Error','Field is required'); ShowPopup();showDataTable();", true);
            }

            _objc.ShowInputSection(divAdd, divInput, divOutput);

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
            dt.Columns.Remove("DepartmentID");

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

            bindDepartment();

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

            gvDepartment.Columns[3].Visible = false;
            gvDepartment.Columns[4].Visible = false;

            var sb = new StringBuilder();
            ////divPrintReceiptDetails.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            gvDepartment.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            string div = sb.ToString();
            //string div = divPrintReceiptDetails1.InnerHtml;
            gvDepartment.Columns[3].Visible = true;
            gvDepartment.Columns[4].Visible = true;

            MAXPDFID = _objc.GetMaximumNumberPDF();

            string htmlfile = MAXPDFID + ".html";
            string pdffile = MAXPDFID + ".pdf";
            string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
            string strFile = LetterPath + pdffile;

            string URL = LetterPath + htmlfile;

            _objc.SaveHtmlFile(URL, "Department Details", lbltitle.Text, div);

            _objc.GenerateAndSavePDF(LetterPath, strFile, pdffile, URL);

            _objc.SavePDFFile("Department.aspx", pdffile, _objSession.employeeid);

            bindDepartment();

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

    #region Common Methods

    private void bindDepartment()
    {
        try
        {
            dsDepartment = _objHR.getDepartment();

            ViewState["DataTable"] = dsDepartment.Tables[0];

            ddlEmployee = _objCommon.GetEmployeeName(ddlEmployee);

            if (dsDepartment.Tables[0].Rows.Count > 0)
            {
                divDownload.Visible = true;
                gvDepartment.DataSource = dsDepartment.Tables[0];
                gvDepartment.DataBind();
                gvDepartment.UseAccessibleHeader = true;
                gvDepartment.HeaderRow.TableSection = TableRowSection.TableHeader;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "hideloader", "showDataTable();", true);
            }

            hdnDepartmentID.Value = "0";

            txtDepartmentName.Text = "";
            txtDepartmentPrefix.Text = "";
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