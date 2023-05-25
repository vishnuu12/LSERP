using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;

public partial class Pages_PartProcessMaster : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cProduction objP;
    cMaterials objMat;

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
                objMat = new cMaterials();
                objMat.GetMaterialNameDetails(ddlMaterialName);
                BindProcessDetails();
            }
            else
            {
                if (target == "deletegvrow")
                {
                    DataSet ds = new DataSet();
                    objP.PMID = Convert.ToInt32(arg.ToString());
                    ds = objP.DeletePartProcessNameByPMID();

                    if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Part Process Name  Deleted Successfully');", true);
                    BindProcessDetails();
                }
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
        try
        {
            BindProcessDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnSaveProcessDetails_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.ProcessName = txtProcessName.Text;

            ds = objP.SavePartProcessName();
            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                BindProcessDetails();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Process Name Saved Successfully');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('information','Process Name Already Exists');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        objP = new cProduction();
        try
        {
            string[] ProcessRate = hdnProcessRate.Value.Split(',');

            objP.MID = Convert.ToInt32(ddlMaterialName.SelectedValue);

            DataRow dr;

            dt.Columns.Add("MID");
            dt.Columns.Add("PMID");
            dt.Columns.Add("ProcessRate");

            int i = 0;
            foreach (GridViewRow row in gvPartProcessDetails.Rows)
            {
                CheckBox chkitems = (CheckBox)row.FindControl("chkitems");
                // TextBox txtProcessRate = (TextBox)row.FindControl("txtProcessRate");

                if (chkitems.Checked)
                {
                    dr = dt.NewRow();
                    dr["MID"] = Convert.ToInt32(ddlMaterialName.SelectedValue);
                    dr["ProcessRate"] = ProcessRate[i];
                    dr["PMID"] = Convert.ToInt32(gvPartProcessDetails.DataKeys[row.RowIndex].Values[0].ToString());

                    dt.Rows.Add(dr);

                    i++;
                }
            }

            objP.dt = dt;

            ds = objP.SavePartProcessDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Part Process Name Saved Successfully');", true);
                BindProcessDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvPartProcessDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkitems = (CheckBox)e.Row.FindControl("chkitems");
                TextBox txtProcessRate = (TextBox)e.Row.FindControl("txtProcessRate");
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("lbtnDelete");

                chkitems.CssClass = "";

                if (dr["CheckedStatus"].ToString() == "checked")
                {
                    chkitems.Checked = true;
                    txtProcessRate.Text = dr["ProcessRate"].ToString();
                    chkitems.CssClass = dr["AssignedFlagPPD"].ToString() == "1" ? "aspNetDisabled" : "";
                    txtProcessRate.CssClass = "form-control mandatoryfield";
                }
                else
                {
                    chkitems.Checked = false;
                    txtProcessRate.Text = "";
                    txtProcessRate.CssClass = "form-control";
                }

                btnDelete.CssClass = dr["AssignedFlagPPM"].ToString() == "1" ? "aspNetDisabled" : "";
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindProcessDetails()
    {
        objP = new cProduction();
        DataSet ds = new DataSet();
        try
        {
            objP.MID = Convert.ToInt32(ddlMaterialName.SelectedValue);
            ds = objP.GetPartProcessMasterDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvPartProcessDetails.DataSource = ds.Tables[0];
                gvPartProcessDetails.DataBind();
            }
            else
            {
                gvPartProcessDetails.DataSource = "";
                gvPartProcessDetails.DataBind();
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
        if (gvPartProcessDetails.Rows.Count > 0)
            gvPartProcessDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}