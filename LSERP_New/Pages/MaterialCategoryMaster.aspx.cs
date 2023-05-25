using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;
using System.Configuration;
using System.IO;
using System.Text;

public partial class Pages_MaterialCategoryMaster : System.Web.UI.Page
{

    #region"Declaration"

    cCommon objC;
    cMaterials objMat;
    cSession objSession;
    cCommonMaster objCommon;

    #endregion

    #region"PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        objSession = Master.csSession;
    }

    #endregion


    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        string target = Request["__EVENTTARGET"];
        string arg = Request["__EVENTARGUMENT"];
        try
        {
            if (!IsPostBack)
            {
                objC = new cCommon();
                objCommon = new cCommonMaster();
                objC.ShowOutputSection(divAdd, divInput, divOutput);
                objCommon.AccessPrintPermissions(btnprint, btnExcel, btnPdf, objSession.employeeid);
                BindMaterialCategoryDetails();
            }
            else
            {
                if (target == "deletegvrow")
                {
                    objMat = new cMaterials();
                    objMat.CID = Convert.ToInt32(arg);
                    DataSet ds = objMat.DeleteMaterialCategoryDetailsByCID();
                    if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Material Category Name Deleted successfully');", true);

                    BindMaterialCategoryDetails();
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnExcelDownload_Click(object sender, EventArgs e)
    {
        objC = new cCommon();
        try
        {
            string MAXEXID = "";
            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["DataTable"];
            dt.Columns.Remove("CID");
            dt.Columns.Remove("Flag");

            MAXEXID = objC.GetMaximumNumberExcel();

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
                File.Delete(strFile);

            objC.exportExcel(dt, rowcount, ColumnCount, strFile, LetterName, "LoneStar", lbltitle.Text, 2, GemBoxKey);

            objC.SaveExcelFile("MaterialCategoryMaster.aspx", LetterName, objSession.employeeid);

            BindMaterialCategoryDetails();

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnPDFDownload_Click(object sender, EventArgs e)
    {
        objC = new cCommon();
        try
        {
            string MAXPDFID = "";

            gvMaterialCategory.Columns[3].Visible = false;
            gvMaterialCategory.Columns[4].Visible = false;

            var sb = new StringBuilder();
            ////divPrintReceiptDetails.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            gvMaterialCategory.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            string div = sb.ToString();
            //string div = divPrintReceiptDetails1.InnerHtml;
            gvMaterialCategory.Columns[3].Visible = true;
            gvMaterialCategory.Columns[4].Visible = true;

            MAXPDFID = objC.GetMaximumNumberPDF();

            string htmlfile = MAXPDFID + ".html";
            string pdffile = MAXPDFID + ".pdf";
            string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
            string strFile = LetterPath + pdffile;

            string URL = LetterPath + htmlfile;

            objC.SaveHtmlFile(URL, "Material Category Details", lbltitle.Text, div);

            objC.GenerateAndSavePDF(LetterPath, strFile, pdffile, URL);

            objC.SavePDFFile("MaterialCategoryMaster.aspx", pdffile, objSession.employeeid);

            BindMaterialCategoryDetails();

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        objC = new cCommon();
        try
        {
            objC.ShowInputSection(divAdd, divInput, divOutput);
            ClearFields();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        objMat = new cMaterials();
        DataSet ds = new DataSet();
        try
        {
            if (ValidateControls())
            {
                objMat.CID = Convert.ToInt32(hdnCID.Value);
                objMat.CategoryName = txtCatagoryName.Text;
                objMat.Description = txtDescription.Text;

                ds = objMat.SaveMaterialCategoryDetails();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Save", "SuccessMessage('Success','Material Category Saved Succesfully');hideLoader();", true);
                else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Save", "SuccessMessage('Success','Material Category Updated Succesfully');hideLoader();", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Save", "InfoMessage('information','Material Category Name Already Exists');hideLoader();", true);

                BindMaterialCategoryDetails();
                ClearFields();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        objC = new cCommon();
        try
        {
            objC.ShowOutputSection(divAdd, divInput, divOutput);
            ClearFields();
            gvMaterialCategory.HeaderRow.TableSection = TableRowSection.TableHeader;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "showDataTable();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    #endregion

    #region"GridView Events"

    protected void gvMaterialCategory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        objMat = new cMaterials();
        objC = new cCommon();
        DataSet ds = new DataSet();
        try
        {
            if (e.CommandName == "EditCategory")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                objMat.CID = Convert.ToInt32(gvMaterialCategory.DataKeys[index].Values[0].ToString());
                ds = objMat.GetMaterialCategoryMasterDetailsByCategoryID();

                hdnCID.Value = ds.Tables[0].Rows[0]["CID"].ToString();
                txtCatagoryName.Text = ds.Tables[0].Rows[0]["CategoryName"].ToString();
                txtDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();

                objC.ShowInputSection(divAdd, divInput, divOutput);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvMaterialCategory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = (DataRowView)e.Row.DataItem;
                LinkButton lbtnDelete = (LinkButton)e.Row.FindControl("lbtnDelete");

                if (dr["Flag"].ToString() == "1")
                    lbtnDelete.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindMaterialCategoryDetails()
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            ds = objMat.GetMaterialCategoryDetails();

            ViewState["DataTable"] = ds.Tables[0];

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvMaterialCategory.DataSource = ds.Tables[0];
                gvMaterialCategory.DataBind();
                gvMaterialCategory.UseAccessibleHeader = true;
                gvMaterialCategory.HeaderRow.TableSection = TableRowSection.TableHeader;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "showDataTable();", true);
            }
            else
            {
                gvMaterialCategory.DataSource = "";
                gvMaterialCategory.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private bool ValidateControls()
    {
        bool isValid = true;
        string error = "";

        if (txtCatagoryName.Text == "")
            error = txtCatagoryName.ClientID + '/' + "Field Required";
        else if (txtDescription.Text == "")
            error = txtDescription.ClientID + '/' + "Field Required";
        if (error != "")
        {
            isValid = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Validate", "ServerSideValidation('" + error + "')", true);
        }
        return isValid;
    }


    private void ClearFields()
    {
        hdnCID.Value = "0";
        txtCatagoryName.Text = "";
        txtDescription.Text = "";
    }


    #endregion
}