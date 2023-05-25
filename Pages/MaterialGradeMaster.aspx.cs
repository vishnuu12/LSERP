using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;

public partial class Pages_MaterialGradeMaster : System.Web.UI.Page
{
    #region "Declaration"

    cSession _objSession = new cSession();
    cCommonMaster objCommon;
    cMaterials objMat;
    cCommon objc;

    #endregion

    #region "PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        _objSession = Master.csSession;
    }

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        string target = Request["__EVENTTARGET"];
        string arg = Request["__EVENTARGUMENT"];

        if (IsPostBack == false)
        {
            objMat = new cMaterials();
            objc = new cCommon();
            objMat.GetMaterialCategoryNameDetails(ddlCategory);
            objc.ShowOutputSection(divAdd, divInput, divOutput);
            BindMaterialGradeDetails();
        }
        else
        {
            if (target == "deletegvrow")
            {
                objc = new cCommon();
                objMat = new cMaterials();
                int MGMID = Convert.ToInt32(arg);
                DataSet ds = objMat.deleteMaterialGradeNameByMGMID(MGMID);
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Material Grade Name Deleted successfully');", true);
                    BindMaterialGradeDetails();
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);

                objc.ShowOutputSection(divAdd, divInput, divOutput);
            }
        }
    }

    #endregion

    #region"Button Events"

    protected void btnSave_Click(object sender, EventArgs e)
    {
        objMat = new cMaterials();
        DataSet ds = new DataSet();
        objc = new cCommon();
        try
        {
            if (objc.Validate(divInput))
            {
                objMat.MGMID = Convert.ToInt32(hdnMGMId.Value);
                objMat.CID = Convert.ToInt32(ddlCategory.SelectedValue);
                objMat.GradeName = txtGradeName.Text;
                objMat.SpecificWeight = Convert.ToDecimal(txtSpecificWeight.Text);
                objMat.Cost = Convert.ToDecimal(txtCost.Text);
                //    objMat.UOM = txtUOM.Text;

                ds = objMat.SaveMaterialGradeDetails();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "SuccessMessage('Success','Material Grade Name Saved successfully');", true);
                else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "SuccessMessage('Success','Material Grade Name Updated SuccessFully');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "InfoMessage('Information','Material Grade Name Already Exists');", true);

                objc.ShowOutputSection(divAdd, divInput, divOutput);
                BindMaterialGradeDetails();
                ClearValues();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        objc = new cCommon();
        try
        {
            objc.ShowOutputSection(divAdd, divInput, divOutput);
            ClearValues();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvMaterialGradeMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        objc = new cCommon();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string MGMID = gvMaterialGradeMaster.DataKeys[index].Values[0].ToString();

            txtGradeName.Text = ((Label)gvMaterialGradeMaster.Rows[index].FindControl("lblMaterialGradeName")).Text;
            txtSpecificWeight.Text = ((Label)gvMaterialGradeMaster.Rows[index].FindControl("lblSpecificWeight")).Text;
            txtCost.Text = ((Label)gvMaterialGradeMaster.Rows[index].FindControl("lblCost")).Text;
            // txtUOM.Text = ((Label)gvMaterialGradeMaster.Rows[index].FindControl("lblUOM")).Text;
            ddlCategory.SelectedValue = gvMaterialGradeMaster.DataKeys[index].Values[1].ToString();
            hdnMGMId.Value = MGMID;

            objc.ShowInputSection(divAdd, divInput, divOutput);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindMaterialGradeDetails()
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        objc = new cCommon();
        try
        {
            ds = objMat.GetMaterialGradeDetails();
            if (ds.Tables[0].Rows.Count > 0)
                gvMaterialGradeMaster.DataSource = ds.Tables[0];
            else
                gvMaterialGradeMaster.DataSource = "";

            gvMaterialGradeMaster.DataBind();
            objc.ShowOutputSection(divAdd, divInput, divOutput);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ClearValues()
    {
        ddlCategory.SelectedIndex = 0;
        txtGradeName.Text = "";
        txtCost.Text = "";
        txtSpecificWeight.Text = "";
        hdnMGMId.Value = "0";
    }

    #endregion

    #region"PageLoadComplete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvMaterialGradeMaster.Rows.Count > 0)
            gvMaterialGradeMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}