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

public partial class Pages_CustomerType : System.Web.UI.Page
{
    #region "Declaration"

    DataSet dsCustomerTypeDetails = new DataSet();
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
            bindCustomerType();
            _objCommon.AccessPrintPermissions(btnprint, imgExcel, imgPdf, _objSession.employeeid);
            _objc.ShowOutputSection(divAdd, divInput, divOutput);
        }
        else
        {
            if (target == "deletegvrow")
            {
                int CustomerTypeId = Convert.ToInt32(arg);
                DataSet ds = _objSales.deleteCustomerType(CustomerTypeId);
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Customer Type Name Deleted successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "InfoMessage('Information','You Cannot Delete The Record');", true);
                }

                bindCustomerType();
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
            if ((txtCustomerType.Text.Trim() != "") && (txtDescription.Text.Trim() != ""))
            {
                _objSales.CustomerTypeId = Convert.ToInt32(hdnCustomerTypeID.Value);
                _objSales.CustomerTypeName = txtCustomerType.Text.Trim();
                _objSales.Description = txtDescription.Text.Trim();

                DataSet ds = _objSales.saveCustomerType();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "AE")
                {
                    if (this.gvCustomerType.Rows.Count > 0)
                    {
                        gvCustomerType.UseAccessibleHeader = true;
                        gvCustomerType.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Customer Type Already Exists');hideLoader();", true);

                    txtCustomerType.Focus();
                }
                else
                {
                    if (Convert.ToInt32(hdnCustomerTypeID.Value) == 0)
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Customer Type Saved successfully');hideLoader();", true);
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Customer Type Updated successfully');hideLoader();", true);

                    bindCustomerType();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErroMessage('Error','Field is required');hideLoader();", true);
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
            dt.Columns.Remove("CustomerTypeId");

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


            _objc.SaveExcelFile("CustomerType.aspx", LetterName, _objSession.employeeid);

            bindCustomerType();

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

            gvCustomerType.Columns[3].Visible = false;
            gvCustomerType.Columns[4].Visible = false;

            var sb = new StringBuilder();
            ////divPrintReceiptDetails.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            gvCustomerType.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            string div = sb.ToString();
            //string div = divPrintReceiptDetails1.InnerHtml;
            gvCustomerType.Columns[3].Visible = true;
            gvCustomerType.Columns[4].Visible = true;

            MAXPDFID = _objc.GetMaximumNumberPDF();

            string htmlfile = MAXPDFID + ".html";
            string pdffile = MAXPDFID + ".pdf";
            string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
            string strFile = LetterPath + pdffile;

            string URL = LetterPath + htmlfile;         

            _objc.SaveHtmlFile(URL, "Customer Type Details", lbltitle.Text, div);

            _objc.GenerateAndSavePDF(LetterPath, strFile, pdffile, URL);

       
            _objc.SavePDFFile("EnquiryType.aspx",pdffile,_objSession.employeeid);

            bindCustomerType();

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

    protected void gvCustomerType_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gvCustomerType_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToString() == "EditCustomerType")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                string CUstomerTypeID = gvCustomerType.DataKeys[index].Values[0].ToString();
                txtCustomerType.Text = ((Label)gvCustomerType.Rows[index].FindControl("lblCustomerType")).Text;
                txtDescription.Text = ((Label)gvCustomerType.Rows[index].FindControl("lblDescription")).Text;
                hdnCustomerTypeID.Value = CUstomerTypeID;
                if (this.gvCustomerType.Rows.Count > 0)
                {
                    gvCustomerType.UseAccessibleHeader = true;
                    gvCustomerType.HeaderRow.TableSection = TableRowSection.TableHeader;
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

    #region "Common Methods"

    private void bindCustomerType()
    {
        try
        {
            dsCustomerTypeDetails = _objSales.GetCustomerType();

            ViewState["DataTable"] = dsCustomerTypeDetails.Tables[0];

            if (dsCustomerTypeDetails.Tables[0].Rows.Count > 0)
            {
                divDownload.Visible = true;
                gvCustomerType.DataSource = dsCustomerTypeDetails.Tables[0];
                gvCustomerType.DataBind();
                gvCustomerType.UseAccessibleHeader = true;
                gvCustomerType.HeaderRow.TableSection = TableRowSection.TableHeader;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "function", "showDataTable();", true);
            }

            else
            {
                divDownload.Visible = false;
                gvCustomerType.DataSource = "";
                gvCustomerType.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "hide loader", "hideLoader();", true);
            }

            hdnCustomerTypeID.Value = "0";
            txtCustomerType.Text = "";
            txtDescription.Text = "";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

}