using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_fatigue : System.Web.UI.Page
{
    #region"Declaration"

    cDesign objDesign;

    #endregion
    #region "Declarartion"
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                objDesign = new cDesign();
                objDesign.BindMOCDetails(ddlMOC);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region "DropDown Events"

    protected void ddlMOC_OnSelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            lblfatiguelifecycle.Text = "";
            if (ddlMOC.SelectedIndex > 0)
            {
                DataSet ds = new DataSet();
                objDesign = new cDesign();
                objDesign.MOCID = ddlMOC.SelectedValue;
                ds = objDesign.GetNoteTemparaturerangeDetailsByMOCID();

                lbltemparaturerange.Text = ds.Tables[0].Rows[0]["Message"].ToString();
            }
            else
                lbltemparaturerange.Text = "";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btncalculate_Click(object sender, EventArgs e)
    {
        objDesign = new cDesign();
        DataSet ds = new DataSet();
        try
        {
            objDesign.MOCID = ddlMOC.SelectedValue;
            objDesign.Temprature = txtTemparature.Text;
            objDesign.Stress = txtstress.Text;

            ds = objDesign.GetDesignFatigueValuesDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "get")
                lblfatiguelifecycle.Text = ds.Tables[0].Rows[0]["NoOfCycles"].ToString();
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "print", "ErrorMessage('Error','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
                lblfatiguelifecycle.Text = "";
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    #endregion
}