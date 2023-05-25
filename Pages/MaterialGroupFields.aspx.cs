using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Pages_MaterialGroupFields : System.Web.UI.Page
{
    #region"Declaration"

    cMaterials objMat;

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                objMat = new cMaterials();
                objMat.GetMaterialNameDetails(ddlMaterialName);
                BindMaterialSpecsFields();
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
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            foreach (ListItem li in chkInputFields.Items)
            {
                li.Selected = false;
                li.Enabled = true;
            }

            foreach (ListItem li in chkFormulaFields.Items)
            {
                li.Enabled = true;
                li.Selected = false;
            }

            if (ddlMaterialName.SelectedIndex > 0)
            {
                objMat.MID = Convert.ToInt32(ddlMaterialName.SelectedValue);
                ds = objMat.GetMaterialSpecsListByMGID();

                foreach (ListItem li in chkInputFields.Items)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if ((li.Value).ToString() == dr["MSMID"].ToString())
                        {
                            li.Selected = true;
                            li.Enabled = dr["FormulaAssignment"].ToString() == "1" ? false : true;
                        }
                    }
                }

                foreach (ListItem li in chkFormulaFields.Items)
                {
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        if (li.Value.ToString() == dr["MSMID"].ToString())
                        {
                            li.Selected = true;
                            li.Enabled = dr["FormulaAssignment"].ToString() == "1" ? false : true;
                        }
                    }
                }
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

    protected void btnSave_Click(object sender, EventArgs e)
    {
        objMat = new cMaterials();
        string strMSMIds = "";
        DataSet ds = new DataSet();
        try
        {
            if (ddlMaterialName.SelectedIndex > 0)
            {
                for (int i = 0; i < chkInputFields.Items.Count; i++)
                {
                    if (chkInputFields.Items[i].Selected == true)
                    {
                        if (strMSMIds != "")
                            strMSMIds = strMSMIds + "," + chkInputFields.Items[i].Value;
                        else
                            strMSMIds = chkInputFields.Items[i].Value;
                    }
                }

                for (int i = 0; i < chkFormulaFields.Items.Count; i++)
                {
                    if (chkFormulaFields.Items[i].Selected == true)
                    {
                        if (strMSMIds != "")
                            strMSMIds = strMSMIds + "," + chkFormulaFields.Items[i].Value;
                        else
                            strMSMIds = chkFormulaFields.Items[i].Value;
                    }
                }

                objMat.MID = Convert.ToInt32(ddlMaterialName.SelectedValue);
                objMat.MSMIds = strMSMIds;
                if (strMSMIds != "")
                    ds = objMat.saveAssignedMaterialSpecsDetails();
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Save", "ErrorMessage('Error','Please Select Specs Name');hideLoader();", true);

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Save", "SuccessMessage('Success','Material Specs Saved successfully');hideLoader();", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "InfoMessage('Information','Formula can contain only input/formula fields.');hideLoader();", true);
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "Save", "ServerSideValidation('" + ddlMaterialName.ClientID + '/' + "Field Required" + "');hideLoader();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindMaterialSpecsFields()
    {
        DataSet ds = new DataSet();
        DataTable dt;
        objMat = new cMaterials();
        DataView dv;
        try
        {
            ds = objMat.GetMaterialSpecsDetails();

            dv = new DataView(ds.Tables[0]);
            dv.RowFilter = "FieldType='I'";
            dt = new DataTable();
            dt = dv.ToTable();

            chkInputFields.DataSource = dt;
            chkInputFields.DataTextField = "Name";
            chkInputFields.DataValueField = "MSMID";
            chkInputFields.DataBind();

            dv = new DataView(ds.Tables[0]);
            dv.RowFilter = "FieldType='F'";
            dt = new DataTable();
            dt = dv.ToTable();

            chkFormulaFields.DataSource = dt;
            chkFormulaFields.DataTextField = "Name";
            chkFormulaFields.DataValueField = "MSMID";
            chkFormulaFields.DataBind();


            gvSpecsDescription.DataSource = ds.Tables[1];
            gvSpecsDescription.DataBind();

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

        finally
        {
            ds = null;
            dt = null;
            dv = null;
            objMat = null;
        }
    }


    #endregion

    #region"PageLoad Complete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvSpecsDescription.Rows.Count > 0)
        {
            gvSpecsDescription.UseAccessibleHeader = true;
            gvSpecsDescription.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    #endregion
}