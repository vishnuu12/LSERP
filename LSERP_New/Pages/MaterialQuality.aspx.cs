using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using eplus.core;

public partial class Pages_MaterialQuality : System.Web.UI.Page
{

    #region"Declaration"

    cMaterials objMat;
    cDesign objDesign;
    cSession objSession;
    cCommon objc;

    #endregion

    #region"PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            objSession = Master.csSession;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];
            if (!IsPostBack)
            {
                objc = new cCommon();
                DataSet dsRFPHID = new DataSet();
                DataSet dsCustomer = new DataSet();
                dsCustomer = objc.getCustomerNameByUserID(Convert.ToInt32(objSession.employeeid), ddlCustomerName);
                dsRFPHID = objc.GetRFPDetailsByUserID(Convert.ToInt32(objSession.employeeid), ddlRFPNo);
            }

            if (target == "UpdateRFPStatus")
                UpdateRFPStatus();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlRFPNo_SelectIndexChanged(object sender, EventArgs e)
    {
        objMat = new cMaterials();
        objc = new cCommon();
        try
        {
            if (ddlRFPNo.SelectedIndex > 0)
            {
                objMat.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
                string ProspectID = objMat.GetProspectNameByRFPHID();
                ddlCustomerName.SelectedValue = ProspectID;
                BindItemDetailsByRFPHID();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnShareRFP_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        Label lblMaterialPlanningStatus;
        string RFPStatus = "";
        try
        {
            for (int i = 0; i < gvViewMaterialPlanning.Rows.Count; i++)
            {
                lblMaterialPlanningStatus = (Label)gvViewMaterialPlanning.Rows[i].FindControl("lblMaterialPlanningStatus");
                if (lblMaterialPlanningStatus.Text == "Completed")
                    RFPStatus = "Completed";
                else
                {
                    RFPStatus = "Incomplete";
                    break;
                }
            }
            if (RFPStatus == "Incomplete")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "InfoMessage('Information','all item should have Completed to Request For production');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "RFPStatus", "UpdateRFPStatus();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvViewMaterialPlanning_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            if (e.CommandName == "View")
            {
                objMat.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
                objMat.RFPDID = Convert.ToInt32(e.CommandArgument.ToString());
                ds = objMat.GetMaterialPlanningDetailsByRFPHIDAndEDID();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvMaterialPlanningDetails.DataSource = ds.Tables[0];
                    gvMaterialPlanningDetails.DataBind();
                }
                else
                {
                    gvMaterialPlanningDetails.DataSource = "";
                    gvMaterialPlanningDetails.DataBind();
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowViewPopUP();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void UpdateRFPStatus()
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            objMat.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objMat.updateRFPStatus();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Material Planning RFP Status Completed SuccessFully')", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindItemDetailsByRFPHID()
    {
        objMat = new cMaterials();
        DataSet ds = new DataSet();
        try
        {
            objMat.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objMat.GetItemDetailsByRFPHIDforViewMaterialPlanning("LS_GetItemDetailsByRFPHIDforViewMaterialPlanning");

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvViewMaterialPlanning.DataSource = ds.Tables[0];
                gvViewMaterialPlanning.DataBind();
                btnShareRFP.Visible = true;
            }
            else
            {
                gvViewMaterialPlanning.DataSource = "";
                gvViewMaterialPlanning.DataBind();
                btnShareRFP.Visible = false;
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
        if (gvViewMaterialPlanning.Rows.Count > 0)
            gvViewMaterialPlanning.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (gvMaterialPlanningDetails.Rows.Count > 0)
            gvMaterialPlanningDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}