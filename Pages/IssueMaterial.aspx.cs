using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;
using System.Globalization;
using System.IO;
using System.Configuration;

public partial class Pages_IssueMaterial : System.Web.UI.Page
{
    #region"Declaration"

    cStores objSt;
    string MRNDocumentSavePath = ConfigurationManager.AppSettings["MRNDocsSavePath"].ToString();
    string MRNDocumentHttppath = ConfigurationManager.AppSettings["MRNDocsHttpPath"].ToString();

    string StoresDocsSavePath = ConfigurationManager.AppSettings["StoresDocsSavePath"].ToString();
    string StoresDocsHttpPath = ConfigurationManager.AppSettings["StoresDocsHttpPath"].ToString();

    cCommon objc;
    cSession objSession = new cSession();

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        string target = Request["__EVENTTARGET"];
        string arg = Request["__EVENTARGUMENT"];
        try
        {
            if (IsPostBack == false)
            {
                BindJobCardDetails();
            }
            if (target == "UpdateStock")
            {

            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Radio Events"

    //protected void rblMaterialIssueReturn_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (rblMaterialIssueReturn.SelectedValue == "I")
    //    {
    //        divMaterialIssue.Visible = true;
    //        divMaterialReturn.Visible = false;
    //    }
    //    else
    //    {
    //        divMaterialIssue.Visible = false;
    //        divMaterialReturn.Visible = true;
    //    }
    //}

    #endregion

    #region"Button Events"

    protected void btnIssueUpdate_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objSt = new cStores();
        try
        {
            objSession = (cSession)HttpContext.Current.Session["LoginDetails"];
            foreach (GridViewRow row in gvIssuedMaterial.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkQC");
                if (chk.Checked)
                {
                    objSt.JCMRNID = Convert.ToInt32(gvIssuedMaterial.DataKeys[row.RowIndex].Values[4].ToString());
                    objSt.IssuedBy = objSession.employeeid;
                    ds = objSt.saveMaterialReturnDetails();

                    if (ds.Tables[0].Rows[0]["Message"].ToString() != "Added")
                        break;
                }
            }

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Material Issued Details Saved successfully');", true);
                BindJobCardDetails();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnMaterialIssue_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objSt = new cStores();
        bool MtrnDate = true;
        try
        {
            objSt.JCHID = Convert.ToInt32(hdnJCHID.Value);
            objSt.SIWeight = txtIssuedQty.Text;
            objSt.SILength = txtIsuedLength.Text;
            objSt.SIWidth = txtIssuedWidth.Text;

            if (txtReturnableDate.Text != "")
                objSt.MaterialRtnDate = DateTime.ParseExact(txtReturnableDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            else
                MtrnDate = false;
            ds = objSt.SaveMaterialIssueDetails(MtrnDate);

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Material Issued Details Saved successfully');CloseIssueMaterialPopUP();", true);
                BindJobCardDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnMaterialReturn_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objSt = new cStores();
        string FileName = "";
        try
        {
            objSt.JCHID = Convert.ToInt32(hdnJCHID.Value);
            objSt.RtnQty = txtReturnableQty.Text;
            objSt.RtnWidth = txtReturnableWidth.Text;
            objSt.RtnLength = txtReturnableLength.Text;

            if (fAttachement.HasFile)
            {
                cSales ojSales = new cSales();
                cCommon objc = new cCommon();
                objc.Foldername = MRNDocumentSavePath;
                FileName = Path.GetFileName(fAttachement.PostedFile.FileName);
                string MaximumAttacheID = ojSales.GetMaximumAttachementID();
                string[] extension = FileName.Split('.');
                FileName = extension[0] + '_' + MaximumAttacheID + '.' + extension[1];
                objc.FileName = FileName;
                objc.PID = ViewState["MRNNumber"].ToString();
                objc.AttachementControl = fAttachement;
                objc.SaveFiles();
            }

            objSt.BalanceMRNLayOut = FileName;
            ds = objSt.saveMaterialReturnDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Material Return Details Saved successfully');CloseIssueMaterialPopUP();", true);
                BindJobCardDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvIssuedMaterial_OnRowDataBound(object sende, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnAdd = (LinkButton)e.Row.FindControl("btnAdd");
                if (dr["MaterialReturnStatus"].ToString() == "Yes")
                    btnAdd.CssClass = "aspNetDisabled";
                else
                    btnAdd.CssClass = "";
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvIssuedMaterial_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dt;
        objc = new cCommon();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            string MRNLocationname = gvIssuedMaterial.DataKeys[index].Values[3].ToString();

            if (e.CommandName == "CutLayout")
            {
                string CutLayout = gvIssuedMaterial.DataKeys[index].Values[2].ToString();
                string PID = "IssuedLayout" + "\\" + MRNLocationname;
                objc.ViewFileName(StoresDocsSavePath, StoresDocsHttpPath, CutLayout, PID, ifrm);
            }
            else if (e.CommandName == "ReturnedLayout")
            {
                string ReturnedLayout = gvIssuedMaterial.DataKeys[index].Values[1].ToString();
                string PID = "MRNDocs" + "\\" + MRNLocationname;
                objc.ViewFileName(StoresDocsSavePath, StoresDocsHttpPath, ReturnedLayout, PID, ifrm);
            }

            //string JCHID = gvIssuedMaterial.DataKeys[index].Values[0].ToString();

            //dt = (DataTable)ViewState["JobCard"];
            //dt.DefaultView.RowFilter = "JCHID='" + JCHID + "'";

            //hdnJCHID.Value = JCHID;

            //ViewState["MRNNumber"] = dt.DefaultView.ToTable().Rows[0]["MRNNumber"].ToString();

            //string IssuedStatus = dt.DefaultView.ToTable().Rows[0]["IssuedStatus"].ToString();

            //if (IssuedStatus == "No")
            //{
            //    divMaterialIssue.Visible = true;
            //    divMaterialReturn.Visible = true;
            //    lblMaterialIssueReturn.Text = "Material Issue";
            //}
            //else
            //{
            //    divMaterialIssue.Visible = false;
            //    divMaterialReturn.Visible = true;
            //    lblMaterialIssueReturn.Text = "Material Return";

            //    lblIssuedQty.Text = dt.DefaultView.ToTable().Rows[0]["SIWeight"].ToString();
            //    lblIssuedLength.Text = dt.DefaultView.ToTable().Rows[0]["SILength"].ToString();
            //    lblIssuedWidth.Text = dt.DefaultView.ToTable().Rows[0]["SIWidth"].ToString();

            //    txtReturnableQty.Text = Convert.ToString(Convert.ToDecimal(lblIssuedQty.Text) - Convert.ToDecimal(lblRequiredQty.Text));
            //}

            //lblHeader.Text = lblMRNo.Text = dt.DefaultView.ToTable().Rows[0]["MRNNumber"].ToString();
            //lblRequiredQty.Text = hdnRequiredQuantity.Value = dt.DefaultView.ToTable().Rows[0]["ISSUEDWeight"].ToString();
            //lblRequiredLength.Text = dt.DefaultView.ToTable().Rows[0]["LEngth"].ToString();
            //lblRequiredWidth.Text = dt.DefaultView.ToTable().Rows[0]["Width"].ToString();

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ShowAddPopUp();", true);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindJobCardDetails()
    {
        DataSet ds = new DataSet();
        objSt = new cStores();
        try
        {
            ds = objSt.GetJobCardDetails();

            ViewState["JobCard"] = ds.Tables[0];
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvIssuedMaterial.DataSource = ds.Tables[0];
                gvIssuedMaterial.DataBind();
            }
            else
            {
                gvIssuedMaterial.DataSource = "";
                gvIssuedMaterial.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"PageLoadComplete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvIssuedMaterial.Rows.Count > 0)
            gvIssuedMaterial.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion


}