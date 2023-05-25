using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;
using System.Text;
using System.Configuration;
using System.IO;

public partial class Pages_MaterialMaster : System.Web.UI.Page
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
            bindMaterialMaster();
            _objCommon.AccessPrintPermissions(btnprint, imgExcel, imgPdf, _objSession.employeeid);
            _objc.ShowOutputSection(divAdd, divInput, divOutput);
        }
        else
        {
            if (target == "deletegvrow")
            {
                int MID = Convert.ToInt32(arg);
                DataSet ds = _objMat.deleteMaterialMaster(MID);
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Material Master Deleted successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "InfoMessage('Information','Material Already In Use');", true);
                }
                bindMaterialMaster();
                _objc.ShowOutputSection(divAdd, divInput, divOutput);
            }
        }
        ddlMaterialClassificationNom.Items[ddlMaterialClassificationNom.Items.Count - 1].Attributes.Add("style", "font-weight:bold;");
    }

    #endregion

    #region "DropDown Events"

    protected void ddlMaterialGroup_SelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtAddNorm.Visible = false;
            txtMaterialName.Enabled = true;
            if (ddlMaterialGroup.SelectedIndex > 0)
            {
                DataSet ds = new DataSet();
                _objMat.MGID = Convert.ToInt32(ddlMaterialGroup.SelectedValue.Split('/')[0].ToString());
                _objMat.Prefix = ddlMaterialGroup.SelectedValue.Split('/')[2].ToString();
                // ds = _objMat.GetMaterialPartNumber();
                // txtMaterialPartNumber.Text = ds.Tables[0].Rows[0]["MaxMID"].ToString();
                // ddlMaterialClassificationNom.SelectedValue = ds.Tables[1].Rows[0]["CMID"].ToString();
            }
            else
            {
                ddlMaterialClassificationNom.SelectedIndex = 0;
            }
            gvMaterialMaster.UseAccessibleHeader = true;
            gvMaterialMaster.HeaderRow.TableSection = TableRowSection.TableHeader;


            _objc.ShowInputSection(divAdd,divInput,divOutput);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "showDataTable();", true);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlMatrialClassificationNorm_SelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlMaterialClassificationNom.SelectedValue == "-1")
            {
                txtMaterialName.Enabled = false;
                txtAddNorm.Visible = true;
            }

            else
            {
                txtMaterialName.Enabled = true;
                txtAddNorm.Visible = false;
            }

            gvMaterialMaster.UseAccessibleHeader = true;
            gvMaterialMaster.HeaderRow.TableSection = TableRowSection.TableHeader;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "showDataTable();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region "Button Events"

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            if ((ddlMaterialGroup.SelectedValue != "0"))
            {
                int NormFlag = 0;
                int PartNumber = Convert.ToInt32(ddlMaterialGroup.SelectedValue.Split('/')[1].ToString()) + 1;

                _objMat.MID = Convert.ToInt32(hdnMID.Value);
                _objMat.MaterialName = txtMaterialName.Text.Trim();
                _objMat.MaterialPartNumber = ddlMaterialGroup.SelectedValue.Split('/')[2].ToString() + PartNumber;
                _objMat.MGID = Convert.ToInt32(ddlMaterialGroup.SelectedValue.Split('/')[0].ToString());

                if (ddlMaterialClassificationNom.SelectedValue == "-1")
                {
                    NormFlag = 1;
                    _objMat.ClassificationNomName = txtAddNorm.Text;
                    ds = _objMat.SaveMaterialClassificationMaster();
                }
                else
                {
                    _objMat.CMID = Convert.ToInt32(ddlMaterialClassificationNom.SelectedValue.Split('/')[0].ToString());
                    // _objMat.ClassificationNomName = ddlMaterialClassificationNom.SelectedValue.ToString();
                    ds = _objMat.SaveMaterialMaster();

                }

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "AE")
                {
                    if (this.gvMaterialMaster.Rows.Count > 0)
                    {
                        gvMaterialMaster.UseAccessibleHeader = true;
                        gvMaterialMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                    if (NormFlag == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Norm Name Already Exists');hideLoader();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Material Name Already Exists');hideLoader();", true);
                    }
                    txtMaterialName.Focus();
                }
                else
                {
                    if (Convert.ToInt32(hdnMID.Value) == 0)
                    {
                        if (NormFlag == 1)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "hideLoader();SuccessMessage('Success','Norm Name Saved successfully');", true);
                            txtAddNorm.Text = "";
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "hideLoader();SuccessMessage('Success','Material Name Saved successfully');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "hideLoader();SuccessMessage('Success','Material Name  Updated successfully');", true);
                    }
                    bindMaterialMaster();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "hideLoader();ErrorMessage('Error','Field is required');showDataTable();", true);
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
            dt.Columns.Remove("CMID");
            dt.Columns.Remove("MGID");
            dt.Columns.Remove("MID");
            dt.Columns.Remove("MaterialGroupName");

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

            _objc.SaveExcelFile("MaterialMaster.aspx", LetterName, _objSession.employeeid);

            bindMaterialMaster();

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

            gvMaterialMaster.Columns[5].Visible = false;
            gvMaterialMaster.Columns[6].Visible = false;

            var sb = new StringBuilder();
            ////divPrintReceiptDetails.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            gvMaterialMaster.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            string div = sb.ToString();
            //string div = divPrintReceiptDetails1.InnerHtml;
            gvMaterialMaster.Columns[5].Visible = true;
            gvMaterialMaster.Columns[6].Visible = true;

            MAXPDFID = _objc.GetMaximumNumberPDF();

            string htmlfile = MAXPDFID + ".html";
            string pdffile = MAXPDFID + ".pdf";
            string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
            string strFile = LetterPath + pdffile;

            string URL = LetterPath + htmlfile;

            _objc.SaveHtmlFile(URL, "Material Master Details", lbltitle.Text, div);

            _objc.GenerateAndSavePDF(LetterPath, strFile, pdffile, URL);

            _objc.SavePDFFile("MaterialMaster.aspx", pdffile, _objSession.employeeid);

            bindMaterialMaster();
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

    protected void gvMaterialMaster_RowDataBound(object sender, GridViewRowEventArgs e)
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
                e.Row.Cells[5].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[6].Attributes.Add("style", "text-align:center;");
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[1].Attributes.Add("style", "text-align:left;");
                e.Row.Cells[2].Attributes.Add("style", "text-align:left;");
                e.Row.Cells[3].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[4].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[5].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[6].Attributes.Add("style", "text-align:center;");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvMaterialMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToString() == "EditMaterialName")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                Label lblMaterialName = (Label)gvMaterialMaster.Rows[index].FindControl("lblMaterialName");
                hdnMID.Value = gvMaterialMaster.DataKeys[index].Values[0].ToString();
                ddlMaterialGroup.SelectedValue = gvMaterialMaster.DataKeys[index].Values[2].ToString();
                ddlMaterialClassificationNom.SelectedValue = gvMaterialMaster.DataKeys[index].Values[1].ToString();
                txtMaterialName.Text = lblMaterialName.Text.ToString();
                if (this.gvMaterialMaster.Rows.Count > 0)
                {
                    gvMaterialMaster.UseAccessibleHeader = true;
                    gvMaterialMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
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

    private void bindMaterialMaster()
    {
        try
        {
            DataSet dsMaterialmasterDetails = new DataSet();

            dsMaterialmasterDetails = _objMat.GetMaterialMaster();

            ddlMaterialGroup.Items.Clear();
            ddlMaterialClassificationNom.Items.Clear();

            _objMat.GetMaterialGroupName(ddlMaterialGroup);
            _objMat.GetMaterialClassificationNom(ddlMaterialClassificationNom);

            ViewState["DataTable"] = dsMaterialmasterDetails.Tables[0];

            if (dsMaterialmasterDetails.Tables[0].Rows.Count > 0)
            {
                divDownload.Visible = true;
                gvMaterialMaster.DataSource = dsMaterialmasterDetails.Tables[0];
                gvMaterialMaster.DataBind();

                gvMaterialMaster.UseAccessibleHeader = true;
                gvMaterialMaster.HeaderRow.TableSection = TableRowSection.TableHeader;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "function", "showDataTable();", true);
            }

            else
            {
                divDownload.Visible = false;
                gvMaterialMaster.DataSource = "";
                gvMaterialMaster.DataBind();
            }

            hdnMID.Value = "0";
            txtMaterialName.Text = "";
            ddlMaterialGroup.SelectedValue = "0";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

}