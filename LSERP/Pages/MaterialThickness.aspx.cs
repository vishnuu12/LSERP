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

public partial class Pages_MaterialThickness : System.Web.UI.Page
{
    #region "Declaration"

    DataSet dsMaterialThicknessDetails = new DataSet();
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
            bindMaterialThickness();
            _objCommon.AccessPrintPermissions(btnprint, imgExcel, imgPdf, _objSession.employeeid);
            divAdd.Style.Add("display", "block");
            divInput.Style.Add("display", "none");
            divOutput.Style.Add("display", "block");
        }
        else
        {
            if (target == "deletegvrow")
            {
                int THKId = Convert.ToInt32(arg);
                DataSet ds = _objMat.DeleteMaterialThicknessValue(THKId);
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Thickness Value Deleted successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "InfoMessage('Information','You Cannot Delete The Record');", true);
                }

                divAdd.Style.Add("display", "block");
                divInput.Style.Add("display", "none");
                divOutput.Style.Add("display", "block");

                bindMaterialThickness();
            }
        }
    }

    #endregion

    #region "Button Events"

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtMaterialThickness.Text.Trim() != "")
            {
                _objMat.THKId = Convert.ToInt32(hdnTHKId.Value);
                _objMat.THKValue = Convert.ToDecimal(txtMaterialThickness.Text.Trim());

                DataSet ds = _objMat.saveMaterialThicknessValue();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "AE")
                {
                    if (this.gvMaterialThickness.Rows.Count > 0)
                    {
                        gvMaterialThickness.UseAccessibleHeader = true;
                        gvMaterialThickness.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "hideLoader();ErrorMessage('Error','Thickness Value Already Exists');showDataTable();", true);

                    txtMaterialThickness.Focus();
                }
                else
                {
                    if (Convert.ToInt32(hdnTHKId.Value) == 0)
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "hideLoader();SuccessMessage('Success','Thickness Value Saved successfully');", true);
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "hideLoader();SuccessMessage('Success','Thickness Value Updated successfully');", true);

                    bindMaterialThickness();
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
            dt.Columns.Remove("THKId");

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

            _objc.SaveExcelFile("MaterialThickness.aspx", LetterName, _objSession.employeeid);

            bindMaterialThickness();

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

            gvMaterialThickness.Columns[2].Visible = false;
            gvMaterialThickness.Columns[3].Visible = false;

            var sb = new StringBuilder();
            ////divPrintReceiptDetails.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            gvMaterialThickness.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            string div = sb.ToString();
            //string div = divPrintReceiptDetails1.InnerHtml;
            gvMaterialThickness.Columns[2].Visible = true;
            gvMaterialThickness.Columns[3].Visible = true;

            MAXPDFID = _objc.GetMaximumNumberPDF();

            string htmlfile = MAXPDFID + ".html";
            string pdffile = MAXPDFID + ".pdf";
            string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
            string strFile = LetterPath + pdffile;

            string URL = LetterPath + htmlfile;

            _objc.SaveHtmlFile(URL,"Material Thickness Details", lbltitle.Text, div);

            _objc.GenerateAndSavePDF(LetterPath, strFile, pdffile, URL);

            _objc.SavePDFFile("MaterialThickness.aspx", pdffile, _objSession.employeeid);

            bindMaterialThickness();
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

    protected void gvMaterialThickness_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[1].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[2].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[3].Attributes.Add("style", "text-align:center;");
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[1].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[2].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[3].Attributes.Add("style", "text-align:center;");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvMaterialThickness_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToString() == "EditThicknessValue")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                string THKid = gvMaterialThickness.DataKeys[index].Values[0].ToString();
                txtMaterialThickness.Text = ((Label)gvMaterialThickness.Rows[index].FindControl("lblTHKValue")).Text;
                hdnTHKId.Value = THKid;
                if (this.gvMaterialThickness.Rows.Count > 0)
                {
                    gvMaterialThickness.UseAccessibleHeader = true;
                    gvMaterialThickness.HeaderRow.TableSection = TableRowSection.TableHeader;
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

    private void bindMaterialThickness()
    {
        try
        {
            dsMaterialThicknessDetails = _objMat.GetMaterialThicknessDetails();

            ViewState["DataTable"] = dsMaterialThicknessDetails.Tables[0];

            if (dsMaterialThicknessDetails.Tables[0].Rows.Count > 0)
            {


                divDownload.Visible = true;
                gvMaterialThickness.DataSource = dsMaterialThicknessDetails.Tables[0];
                gvMaterialThickness.DataBind();

                gvMaterialThickness.UseAccessibleHeader = true;
                gvMaterialThickness.HeaderRow.TableSection = TableRowSection.TableHeader;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "hide loader", "showDataTable();", true);
            }

            else
            {
                divDownload.Visible = false;
                gvMaterialThickness.DataSource = "";
                gvMaterialThickness.DataBind();
            }

            hdnTHKId.Value = "0";
            txtMaterialThickness.Text = "";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

}