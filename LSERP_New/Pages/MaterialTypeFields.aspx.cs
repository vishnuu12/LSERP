using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;

public partial class Pages_MaterialTypeFields : System.Web.UI.Page
{
    #region"Declarartion"

    cMaterials objMat;
    cCommon objc;

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
                objc = new cCommon();
                objc.ShowOutputSection(divAdd, divInput, divOutput);
                BindMaterialTypeSpecsDetails();
            }
            else
                if (target == "deletegvrow")
                {
                    objMat = new cMaterials();
                    objc = new cCommon();
                    objMat.MTFID = Convert.ToInt32(arg);
                    DataSet ds = objMat.DeleteMaterialTypeSpecs();
                    if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Specs Name Deleted successfully');", true);

                    objc.ShowOutputSection(divAdd, divInput, divOutput);
                    BindMaterialTypeSpecsDetails();
                }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            if (ValidateControls())
            {
                objMat.SpecsName = txtSpecsName.Text;
                objMat.SpecsShortCode = txtSpecsShortCode.Text;

                ds = objMat.SaveMaterialTypeSpecsDetails();
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Save", "SuccessMessage('Success','Specs Name Saved successfully');", true);
                else if (ds.Tables[0].Rows[0]["Message"].ToString() == "AlreadyExists")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Save", "InfoMessage('Information','Specs Name Already Exists');", true);

                BindMaterialTypeSpecsDetails();
                clearFields();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        objc = new cCommon();
        try
        {
            objc.ShowInputSection(divAdd, divInput, divOutput);
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
            clearFields();
            objc.ShowOutputSection(divAdd, divInput, divOutput);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvMaterialTypeSpecsDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = (DataRowView)e.Row.DataItem;
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("lbtnDelete");

                if (dr["Flag"].ToString() == "1")
                {
                    // btnDelete.Attributes.Add("onblur", "DisableLinkButton('" + btnDelete.ClientID + "')");
                    btnDelete.Visible = false;
                    //  btnDelete.ToolTip = "Specs Already Uses";
                }
            }

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindMaterialTypeSpecsDetails()
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            ds = objMat.GetMaterialTypeFieldsDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvMaterialTypeSpecsDetails.DataSource = ds.Tables[0];
                gvMaterialTypeSpecsDetails.DataBind();
            }
            else
            {
                gvMaterialTypeSpecsDetails.DataSource = "";
                gvMaterialTypeSpecsDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void clearFields()
    {
        txtSpecsName.Text = "";
        txtSpecsShortCode.Text = "";
    }

    private bool ValidateControls()
    {
        bool isValid = true;
        string error = "";
        try
        {
            if (txtSpecsName.Text == "")
                error = txtSpecsName.ClientID + '/' + "Field Required";
            else if (txtSpecsShortCode.Text == "")
                error = txtSpecsShortCode.ClientID + '/' + "Field Required";

            if (error != "")
            {
                isValid = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Validate", "ServerSideValidation('" + error + "');", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        return isValid;
    }

    #endregion

    #region"PageLoad Complete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvMaterialTypeSpecsDetails.Rows.Count > 0)
            gvMaterialTypeSpecsDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}