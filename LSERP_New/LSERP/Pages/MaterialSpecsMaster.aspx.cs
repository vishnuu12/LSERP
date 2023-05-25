using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using eplus.core;

public partial class Pages_MaterialSpecsMaster : System.Web.UI.Page
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
                BindMaterialSpecsDetails();
            }
            else
                if (target == "deletegvrow")
                {
                    objMat = new cMaterials();
                    objc = new cCommon();
                    objMat.MSMID = Convert.ToInt32(arg);
                    DataSet ds = objMat.DeleteMaterialSpecs();
                    if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Specs Name Deleted successfully');", true);

                    objc.ShowOutputSection(divAdd, divInput, divOutput);
                    BindMaterialSpecsDetails();
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
                ds = objMat.SaveMaterialSpecsDetails();
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Save", "SuccessMessage('Success','Specs Name Saved successfully');hideLoader();", true);

                BindMaterialSpecsDetails();
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

            gvMaterialSpecsDetails.UseAccessibleHeader = true;
            gvMaterialSpecsDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "showDataTable();", true);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvMaterialSpecsDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
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

    private void BindMaterialSpecsDetails()
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            ds = objMat.GetMaterialSpecsDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvMaterialSpecsDetails.DataSource = ds.Tables[0];
                gvMaterialSpecsDetails.DataBind();
                gvMaterialSpecsDetails.UseAccessibleHeader = true;
                gvMaterialSpecsDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "showDataTable();", true);
            }
            else
            {
                gvMaterialSpecsDetails.DataSource = "";
                gvMaterialSpecsDetails.DataBind();
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
}