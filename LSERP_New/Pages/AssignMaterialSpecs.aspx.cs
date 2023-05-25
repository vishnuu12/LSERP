using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using common;
using System.Data;

public partial class Pages_AssignMaterialSpecs : System.Web.UI.Page
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
                objMat.GetMaterialList(ddlMaterialName);
                BindMaterialSpecsDetails();
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
            foreach (ListItem li in chkMaterialSpecs.Items)
                li.Selected = false;

            if (ddlMaterialName.SelectedIndex > 0)
            {
                objMat.MID = Convert.ToInt32(ddlMaterialName.SelectedValue);
                ds = objMat.GetMaterialSpecsListByMaterialID();

                foreach (ListItem li in chkMaterialSpecs.Items)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        if ((li.Value).ToString() == dr["MSMID"].ToString())
                            li.Selected = true;
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
                for (int i = 0; i < chkMaterialSpecs.Items.Count; i++)
                {
                    if (chkMaterialSpecs.Items[i].Selected == true)
                    {
                        if (strMSMIds != "")
                            strMSMIds = strMSMIds + "," + chkMaterialSpecs.Items[i].Value;
                        else
                            strMSMIds = chkMaterialSpecs.Items[i].Value;
                    }
                }

                objMat.MID = Convert.ToInt32(ddlMaterialName.SelectedValue);
                objMat.MSMIds = strMSMIds;
                ds = objMat.saveAssignedMaterialSpecsDetails();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Save", "SuccessMessage('Success','Material Specs Saved successfully');hideLoader();", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Save", "ServerSideValidation('" + ddlMaterialName.ClientID + '/' + "Field Required" + "');hideLoader();", true);
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
            chkMaterialSpecs.DataSource = ds.Tables[0];
            chkMaterialSpecs.DataTextField = "ShortCode";
            chkMaterialSpecs.DataValueField = "MSMID";
            chkMaterialSpecs.DataBind();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

}