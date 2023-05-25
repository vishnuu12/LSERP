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

public partial class Pages_PartMaster : System.Web.UI.Page
{
    #region "Declaration"

    cSession _objSession = new cSession();
    cCommonMaster _objCommon = new cCommonMaster();
    cMaterials _objMat = new cMaterials();
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
            bindPartMaster();
            _objCommon.AccessPrintPermissions(btnprint, imgExcel, imgPdf, _objSession.employeeid);
            divAdd.Style.Add("display", "block");
            divInput.Style.Add("display", "none");
            divOutput.Style.Add("display", "block");
        }
        else
        {
            if (target == "deletegvrow")
            {
                int PartId = Convert.ToInt32(arg);
                DataSet ds = _objMat.deletePartMaster(PartId);
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Part Name Deleted successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "InfoMessage('Information','You Cannot Delete The Record');", true);
                }

                divAdd.Style.Add("display", "block");
                divInput.Style.Add("display", "none");
                divOutput.Style.Add("display", "block");

                bindPartMaster();
            }
        }
    }

    #endregion

    #region "Button Events"

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if ((txtPartName.Text.Trim() != "") && (txtDescription.Text.Trim() != ""))
            {
                _objMat.PartId = Convert.ToInt32(hdnPartID.Value);
                _objMat.PartName = txtPartName.Text.Trim();
                _objMat.Description = txtDescription.Text.Trim();

                DataSet ds = _objMat.savePartMaster();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "AE")
                {
                    if (this.gvPartMaster.Rows.Count > 0)
                    {
                        gvPartMaster.UseAccessibleHeader = true;
                        gvPartMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Part Name Already Exists');hideLoader();", true);

                    txtPartName.Focus();
                }
                else
                {
                    if (Convert.ToInt32(hdnPartID.Value) == 0)
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "hideLoader();SuccessMessage('Success','Part Name Saved successfully');", true);
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "hideLoader();SuccessMessage('Success','Part Name Updated successfully');", true);

                    bindPartMaster();
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
            dt.Columns.Remove("PartId");

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

            _objc.SaveExcelFile("PartMaster.aspx", LetterName, _objSession.employeeid);

            bindPartMaster();

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

            gvPartMaster.Columns[3].Visible = false;
            gvPartMaster.Columns[4].Visible = false;

            var sb = new StringBuilder();
            ////divPrintReceiptDetails.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            gvPartMaster.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            string div = sb.ToString();
            //string div = divPrintReceiptDetails1.InnerHtml;
            gvPartMaster.Columns[3].Visible = true;
            gvPartMaster.Columns[4].Visible = true;

            MAXPDFID = _objc.GetMaximumNumberPDF();

            string htmlfile = MAXPDFID + ".html";
            string pdffile = MAXPDFID + ".pdf";
            string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
            string strFile = LetterPath + pdffile;

            string URL = LetterPath + htmlfile;
       
            _objc.SaveHtmlFile(URL, "Part Master Details", lbltitle.Text, div);

            _objc.GenerateAndSavePDF(LetterPath, strFile, pdffile, URL);

            _objc.SavePDFFile("PartMaster.aspx", pdffile, _objSession.employeeid);

            bindPartMaster();

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

    protected void gvPartMaster_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gvPartMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToString() == "EditPartType")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                string partId = gvPartMaster.DataKeys[index].Values[0].ToString();
                txtPartName.Text = ((Label)gvPartMaster.Rows[index].FindControl("lblPartName")).Text;
                txtDescription.Text = ((Label)gvPartMaster.Rows[index].FindControl("lblDescription")).Text;
                hdnPartID.Value = partId;
                if (this.gvPartMaster.Rows.Count > 0)
                {
                    gvPartMaster.UseAccessibleHeader = true;
                    gvPartMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
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

    private void bindPartMaster()
    {
        try
        {
            DataSet dsPartDetails = new DataSet();

            dsPartDetails = _objMat.GetPartMasterDetails();

            ViewState["DataTable"] = dsPartDetails.Tables[0];

            if (dsPartDetails.Tables[0].Rows.Count > 0)
            {

                divDownload.Visible = true;
                gvPartMaster.DataSource = dsPartDetails.Tables[0];
                gvPartMaster.DataBind();

                gvPartMaster.UseAccessibleHeader = true;
                gvPartMaster.HeaderRow.TableSection = TableRowSection.TableHeader;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "function", "showDataTable();", true);
            }

            else
            {
                divDownload.Visible = false;
                gvPartMaster.DataSource = "";
                gvPartMaster.DataBind();
            }

            hdnPartID.Value = "0";
            txtPartName.Text = "";
            txtDescription.Text = "";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

}