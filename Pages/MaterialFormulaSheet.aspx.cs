using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using eplus.core;

public partial class Pages_MaterialFormulaSheet : System.Web.UI.Page
{
    #region"Declarartion"

    cMaterials objMat;
    cCommon objc;

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                objMat = new cMaterials();
                objc = new cCommon();
                objMat.GetMaterialNameDetails(ddlMaterialName);
                bindMaterialFormula();
                objc.ShowOutputSection(divAdd, divInput, divOutput);
                // SplitTheFormula();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlMaterialName_SelectIndexChanged(object sender, EventArgs e)
    {
        objMat = new cMaterials();
        DataSet ds = new DataSet();
        try
        {
            if (ddlMaterialName.SelectedIndex > 0)
            {
                objMat.MID = Convert.ToInt32(ddlMaterialName.SelectedValue);
                ds = objMat.GetMaterialSpecsListByMGID();

                ddlMaterialSpecsName.Items.Clear();

                ddlMaterialSpecsName.DataSource = ds.Tables[1];
                ddlMaterialSpecsName.DataTextField = "MaterialSpecsName";
                ddlMaterialSpecsName.DataValueField = "MSMID";
                ddlMaterialSpecsName.DataBind();
                ddlMaterialSpecsName.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            else
            {
                ddlMaterialSpecsName.DataSource = "";
                ddlMaterialSpecsName.DataBind();
                ddlMaterialSpecsName.Items.Insert(0, new ListItem("--Select--", "0"));
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Save", "hideLoader();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        objc = new cCommon();
        try
        {
            objc.ShowInputSection(divAdd, divInput, divOutput);
            ddlMaterialName.Enabled = true;
            ddlMaterialSpecsName.Enabled = true;
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
                objMat.MFID = Convert.ToInt32(hdnMFID.Value);
                objMat.MID = Convert.ToInt32(ddlMaterialName.SelectedValue);
                objMat.MSMID = Convert.ToInt32(ddlMaterialSpecsName.SelectedValue);
                objMat.FormulaName = txtFormula.Text;

                ds = objMat.SaveMaterialFormulaDetails();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Save", "SuccessMessage('Success','Material Formula Saved Successfully');hideLoader();", true);
                else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                {
                    ddlMaterialName.Enabled = true;
                    ddlMaterialSpecsName.Enabled = true;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Save", "SuccessMessage('Success','Material Formula Updated Successfully');hideLoader();", true);
                }

                else if (ds.Tables[0].Rows[0]["Message"].ToString() == "AlreadyExists")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Save", "InfoMessage('information','Formula Already Exists In This Material');hideLoader();", true);

                else if (ds.Tables[0].Rows[0]["Message"].ToString() == "FormulaWrong")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Save", "ErrorMessage('Error','Formula can contain only input/formula fields.');hideLoader();", true);

                bindMaterialFormula();
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
        objc = new cCommon();
        try
        {
            objc.ShowOutputSection(divAdd, divInput, divOutput);
            ClearFields();
            gvMaterialFormula.UseAccessibleHeader = true;
            gvMaterialFormula.HeaderRow.TableSection = TableRowSection.TableHeader;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "showDataTable();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvMaterialFormula_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        objc = new cCommon();
        try
        {
            objMat.MFID = Convert.ToInt32(e.CommandArgument.ToString());
            hdnMFID.Value = e.CommandArgument.ToString();
            ds = objMat.GetMaterialFormulaByFormulaID();

            ddlMaterialName.SelectedValue = ds.Tables[0].Rows[0]["MID"].ToString();
            ddlMaterialName_SelectIndexChanged(null, null);
            ddlMaterialSpecsName.SelectedValue = ds.Tables[0].Rows[0]["MSMID"].ToString();
            txtFormula.Text = ds.Tables[0].Rows[0]["Formula"].ToString();
          //  ddlMaterialName.Enabled = false;
          //  ddlMaterialSpecsName.Enabled = false;
            objc.ShowInputSection(divAdd, divInput, divOutput);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion


    #region"Common Methods"

    private void bindMaterialFormula()
    {
        objMat = new cMaterials();
        DataSet ds = new DataSet();
        try
        {
            ds = objMat.GetMaterialFormulaDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvMaterialFormula.DataSource = ds.Tables[0];
                gvMaterialFormula.DataBind();
                gvMaterialFormula.UseAccessibleHeader = true;
                gvMaterialFormula.HeaderRow.TableSection = TableRowSection.TableHeader;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "showDataTable();", true);
            }
            else
            {
                gvMaterialFormula.DataSource = "";
                gvMaterialFormula.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ClearFields()
    {
        hdnMFID.Value = "0";
        txtFormula.Text = "";
        //ddlMaterialName.SelectedIndex = 0;
        //ddlMaterialSpecsName.SelectedIndex = 0;
    }

    private bool ValidateControls()
    {
        bool isValid = true;
        string error = "";
        try
        {
            if (txtFormula.Text == "")
                error = txtFormula.ClientID + '/' + "Field Required";
            else if (ddlMaterialName.SelectedIndex == 0)
                error = ddlMaterialName.ClientID + '/' + "Field Required";
            else if (ddlMaterialSpecsName.SelectedIndex == 0)
                error = ddlMaterialSpecsName.ClientID + '/' + "Field Required";

            if (error != "")
            {
                isValid = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "ServerSideValidation('" + error + "');", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return isValid;
    }

    //private void SplitTheFormula()
    //{
    //    try
    //    {
    //        string formula = "3.142/4*(square(od)-square(id))*THK*QTY*SWT/1000000";
    //        string[] split = formula.Split(new Char[] { '(', ')', '/', '+', '*', '-', },
    //                             StringSplitOptions.RemoveEmptyEntries);
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    #endregion
}