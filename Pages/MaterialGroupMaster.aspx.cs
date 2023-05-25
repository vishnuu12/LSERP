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

public partial class Pages_MaterialGroupMaster : System.Web.UI.Page
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
            _objMat.GetMaterialCategoryNameDetails(ddlCategory);
            bindMaterialGroup();
            bindPrefixName();
            _objCommon.AccessPrintPermissions(btnprint, imgExcel, imgPdf, _objSession.employeeid);
            _objc.ShowOutputSection(divAdd, divInput, divOutput);
        }
        else
        {
            if (target == "deletegvrow")
            {
                int MGID = Convert.ToInt32(arg);
                DataSet ds = _objMat.deleteMaterialGroup(MGID);
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Material Group Deleted successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "InfoMessage('Information','Material Group Already In Use');", true);
                }

                bindMaterialGroup();

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
            if (ValidateControls())
            {
                _objMat.MGID = Convert.ToInt32(hdnMGId.Value);
                _objMat.MateriyalGroupName = txtMaterialGroup.Text.Trim();
                _objMat.Prefix = ddlPrefix.SelectedValue;
                _objMat.CID = Convert.ToInt32(ddlCategory.SelectedValue);

                DataSet ds = _objMat.saveMaterialGroup();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "AE")
                {
                    if (this.gvMaterialGroup.Rows.Count > 0)
                    {
                        gvMaterialGroup.UseAccessibleHeader = true;
                        gvMaterialGroup.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','Material Group Name Already Exists');hideLoader();", true);

                    txtMaterialGroup.Focus();
                }
                else if (ds.Tables[0].Rows[0]["Message"].ToString() == "AEP")
                {
                    if (this.gvMaterialGroup.Rows.Count > 0)
                    {
                        gvMaterialGroup.UseAccessibleHeader = true;
                        gvMaterialGroup.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('!Information','Prefix Name Already Exists');hideLoader();", true);

                    txtMaterialGroup.Focus();
                }
                else
                {
                    if (Convert.ToInt32(hdnMGId.Value) == 0)
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "hideLoader();SuccessMessage('Success','Material Group Name Saved successfully');", true);
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "hideLoader();SuccessMessage('Success','Material Group Name Updated successfully');", true);

                    bindMaterialGroup();
                }
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
            dt.Columns.Remove("MGID");

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

            _objc.SaveExcelFile("MaterialGroupMaster.aspx", LetterName, _objSession.employeeid);

            bindMaterialGroup();

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

            gvMaterialGroup.Columns[4].Visible = false;
            gvMaterialGroup.Columns[5].Visible = false;

            var sb = new StringBuilder();
            ////divPrintReceiptDetails.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            gvMaterialGroup.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            string div = sb.ToString();
            //string div = divPrintReceiptDetails1.InnerHtml;
            gvMaterialGroup.Columns[4].Visible = true;
            gvMaterialGroup.Columns[5].Visible = true;

            MAXPDFID = _objc.GetMaximumNumberPDF();

            string htmlfile = MAXPDFID + ".html";
            string pdffile = MAXPDFID + ".pdf";
            string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
            string strFile = LetterPath + pdffile;

            string URL = LetterPath + htmlfile;

            _objc.SaveHtmlFile(URL, "Material Group Details", lbltitle.Text, div);

            _objc.GenerateAndSavePDF(LetterPath, strFile, pdffile, URL);

            _objc.SavePDFFile("MaterialGroupMaster.aspx", pdffile, _objSession.employeeid);

            bindMaterialGroup();
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

    protected void gvMaterialGroup_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName.ToString() == "EditMaterialGroup")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                string MGID = gvMaterialGroup.DataKeys[index].Values[0].ToString();
                txtMaterialGroup.Text = ((Label)gvMaterialGroup.Rows[index].FindControl("lblMaterialGroup")).Text;
                ddlPrefix.SelectedValue = ((Label)gvMaterialGroup.Rows[index].FindControl("lblPrefix")).Text;
                ddlCategory.SelectedValue = ((Label)gvMaterialGroup.Rows[index].FindControl("lblCID")).Text;
                hdnMGId.Value = MGID;
                if (this.gvMaterialGroup.Rows.Count > 0)
                {
                    gvMaterialGroup.UseAccessibleHeader = true;
                    gvMaterialGroup.HeaderRow.TableSection = TableRowSection.TableHeader;
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

    private bool ValidateControls()
    {
        bool isvalid;
        isvalid = true;
        string error = "";
        if (txtMaterialGroup.Text == "")
            error = txtMaterialGroup.ClientID + '/' + "Field Required";
        else if (ddlPrefix.SelectedIndex == 0)
            error = txtMaterialGroup.ClientID + '/' + "Field Required";
        else if (ddlCategory.SelectedIndex == 0)
            error = ddlCategory.ClientID + '/' + "Field Required";
        if (error != "")
        {
            isvalid = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "ServerSideValidation('" + error + "');", true);
        }

        return isvalid;
    }

    private void bindMaterialGroup()
    {
        try
        {
            DataSet dsMaterialGroupDetails = new DataSet();

            dsMaterialGroupDetails = _objMat.GetMaterialGroup();

            ViewState["DataTable"] = dsMaterialGroupDetails.Tables[0];

            if (dsMaterialGroupDetails.Tables[0].Rows.Count > 0)
            {
                divDownload.Visible = true;
                gvMaterialGroup.DataSource = dsMaterialGroupDetails.Tables[0];
                gvMaterialGroup.DataBind();

                gvMaterialGroup.UseAccessibleHeader = true;
                gvMaterialGroup.HeaderRow.TableSection = TableRowSection.TableHeader;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "function", "showDataTable();", true);
            }

            else
            {
                divDownload.Visible = false;
                gvMaterialGroup.DataSource = "";
                gvMaterialGroup.DataBind();
            }

            hdnMGId.Value = "0";
            txtMaterialGroup.Text = "";
            ddlPrefix.SelectedValue = "0";
            ddlCategory.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void bindPrefixName()
    {
        try
        {
            //  ddlPrefix.Items.Insert(0, new ListItem("--Select Month / Year--", "0"));
            DataTable dt = new DataTable();
            dt.Columns.Add("Prefix");
            dt.Columns.Add("Value");

            DataRow dr = null;

            for (int i = 65; i < 91; i++)
            {
                dr = dt.NewRow();
                dr["Prefix"] = ((Convert.ToChar(i)).ToString());
                dr["Value"] = ((Convert.ToChar(i)).ToString());
                dt.Rows.Add(dr);
            }

            ddlPrefix.DataSource = dt;
            ddlPrefix.DataValueField = "Value";
            ddlPrefix.DataTextField = "Prefix";
            ddlPrefix.DataBind();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

}
