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

public partial class Pages_MaterialTypeMaster : System.Web.UI.Page
{
    #region "Declaration"

    DataSet dsMaterialTypeDetails = new DataSet();
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
            bindMaterialType();
            ShowHideControls("add,view");
        }
        else
        {
            if (target == "deletegvrow")
            {
                int MaterialTypeId = Convert.ToInt32(arg);
                _objMat.MaterialTypeId = MaterialTypeId;
                DataSet ds = _objMat.DeleteMaterialType();
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Material Type Deleted successfully');", true);
                bindMaterialType();
            }
        }
    }

    #endregion

    #region "Button Events"

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        try
        {
            ShowHideControls("input");
            BindmaterialTypeSpecs();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ShowHideControls("add,view");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string MTFields = "";
        try
        {
            if ((txtMaterialType.Text.Trim() != "") && (txtDescription.Text.Trim() != ""))
            {
                _objMat.MaterialTypeId = Convert.ToInt32(hdnMaterialTypeID.Value);
                _objMat.MaterialtypeName = txtMaterialType.Text.Trim();
                _objMat.Description = txtDescription.Text.Trim();

                foreach (ListItem li in chkMTFields.Items)
                {
                    if (li.Selected)
                    {
                        if (MTFields == "")
                            MTFields = li.Value;
                        else
                            MTFields = MTFields + ',' + li.Value;
                    }
                }
                _objMat.MTFields = MTFields;

                DataSet ds = _objMat.saveMaterialType();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "AE")
                {
                    if (this.gvMaterial.Rows.Count > 0)
                    {
                        gvMaterial.UseAccessibleHeader = true;
                        gvMaterial.HeaderRow.TableSection = TableRowSection.TableHeader;
                    }

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','MaterialType Already Exists');hideLoader();", true);

                    txtMaterialType.Focus();
                }
                else
                {
                    if (Convert.ToInt32(hdnMaterialTypeID.Value) == 0)
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "hideLoader();SuccessMessage('Success','MaterialType Saved successfully');", true);
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "hideLoader();SuccessMessage('Success','MaterialType Updated successfully');", true);

                    bindMaterialType();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErroMessage('Error','Field is required');showDataTable();", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErroMessage('Error','Error Occured');showDataTable();", true);
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
            dt.Columns.Remove("MaterialTypeId");

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

            _objc.SaveExcelFile("MaterialTypeMaster.aspx", LetterName, _objSession.employeeid);

            bindMaterialType();

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

            gvMaterial.Columns[3].Visible = false;
            gvMaterial.Columns[4].Visible = false;

            var sb = new StringBuilder();
            ////divPrintReceiptDetails.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            gvMaterial.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            string div = sb.ToString();
            //string div = divPrintReceiptDetails1.InnerHtml;
            gvMaterial.Columns[3].Visible = true;
            gvMaterial.Columns[4].Visible = true;

            MAXPDFID = _objc.GetMaximumNumberPDF();

            string htmlfile = MAXPDFID + ".html";
            string pdffile = MAXPDFID + ".pdf";
            string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
            string strFile = LetterPath + pdffile;

            string URL = LetterPath + htmlfile;

            StreamWriter w;
            w = File.CreateText(URL);
            w.WriteLine("<html><head><title>");
            w.WriteLine("Material Type Details");
            w.WriteLine("</title>");
            w.WriteLine("<link rel='stylesheet' type='text/css' href='http://localhost:52012/lonestar/Assets/css/ep-style.css'/>");
            w.WriteLine("<link rel='stylesheet' href='http://localhost:52012/lonestar/Assets/css/style.css' type='text/css'/>");
            w.WriteLine("</head><body>");
            w.WriteLine("<div style='text-align:center;padding-top:10px;font-size:20px;color:#00BCD4;'>");
            w.WriteLine("LoneStar");
            w.WriteLine("</div>");
            w.WriteLine("<div class='col-sm-12' style='text-align:center;padding-top:10px;font-size:20px;font-weight:bold;'>");
            w.WriteLine(lbltitle.Text);
            w.WriteLine("<div>");
            w.WriteLine("<div class='col-sm-12' style='padding-top:10px;'>");
            w.WriteLine(div);
            w.WriteLine("<div>");
            w.WriteLine("</body></html>");
            w.Flush();
            w.Close();

            _objc.GenerateAndSavePDF(LetterPath, strFile, pdffile, URL);

            _objc.SavePDFFile("MaterialTypeMaster.aspx", pdffile, _objSession.employeeid);

            bindMaterialType();

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

    protected void gvMaterial_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gvMaterial_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        try
        {
            if (e.CommandName.ToString() == "EditMaterialType")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                string MaterialTypeID = gvMaterial.DataKeys[index].Values[0].ToString();
                txtMaterialType.Text = ((Label)gvMaterial.Rows[index].FindControl("lblMaterialType")).Text;
                txtDescription.Text = ((Label)gvMaterial.Rows[index].FindControl("lblDescription")).Text;
                hdnMaterialTypeID.Value = MaterialTypeID;
                _objMat.MaterialTypeId = Convert.ToInt32(MaterialTypeID);

                ds = _objMat.GetMaterialTypeMasterDetailsBymaterialtypeID();

                BindmaterialTypeSpecs();

                string[] MTFID = ds.Tables[0].Rows[0]["MTFID"].ToString().Split(',');

                for (int i = 0; i < MTFID.Length; i++)
                {
                    foreach (ListItem li in chkMTFields.Items)
                    {
                        if (li.Value.ToString() == MTFID[i])
                        {
                            li.Selected = true;
                        }
                    }
                }

                ShowHideControls("input");

                if (this.gvMaterial.Rows.Count > 0)
                {
                    gvMaterial.UseAccessibleHeader = true;
                    gvMaterial.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "showDataTable();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
    }

    #endregion

    #region "Common Methods"

    private void BindmaterialTypeSpecs()
    {
        DataSet ds = new DataSet();
        try
        {
            ds = _objMat.GetMaterialTypeFieldsDetails();

            chkMTFields.DataSource = ds.Tables[0];
            chkMTFields.DataTextField = "ShortCode";
            chkMTFields.DataValueField = "MTFID";
            chkMTFields.DataBind();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindMaterialType()
    {
        try
        {
            dsMaterialTypeDetails = _objMat.GetMaterialType();

            ViewState["DataTable"] = dsMaterialTypeDetails.Tables[0];

            if (dsMaterialTypeDetails.Tables[0].Rows.Count > 0)
            {


                divDownload.Visible = true;
                gvMaterial.DataSource = dsMaterialTypeDetails.Tables[0];
                gvMaterial.DataBind();

                gvMaterial.UseAccessibleHeader = true;
                gvMaterial.HeaderRow.TableSection = TableRowSection.TableHeader;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "function", "showDataTable();", true);
            }

            else
            {
                divDownload.Visible = false;
                gvMaterial.DataSource = "";
                gvMaterial.DataBind();
            }

            hdnMaterialTypeID.Value = "0";
            txtMaterialType.Text = "";
            txtDescription.Text = "";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ShowHideControls(string divids)
    {
        divAdd.Visible = divInput.Visible = divOutput.Visible = false;
        string[] mode = divids.Split(',');
        try
        {
            for (int i = 0; i < mode.Length; i++)
            {
                switch (mode[i].ToLower())
                {
                    case "add":
                        divAdd.Visible = true;
                        break;
                    case "view":
                        divOutput.Visible = true;
                        break;
                    case "input":
                        divInput.Visible = true;
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"PageLoad Complete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvMaterial.Rows.Count > 0)
            gvMaterial.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion

}