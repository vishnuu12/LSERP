using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_ViewRFPMaterialPlanningDetails : System.Web.UI.Page
{
    #region"Declaration"

    cReports objRpt;

    #endregion

    #region "Declaration"

    cSession objSession = new cSession();

    #endregion

    #region"Page Load Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ViewState["RFPDID"] = Request.QueryString["RFPDID"].ToString();
                BindRFPMaterialPlanningDetails();
                GetItemName();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region "Common Methods"

    private void BindRFPMaterialPlanningDetails()
    {
        DataSet ds = new DataSet();
        objRpt = new cReports();
        try
        {
            objRpt.RFPDID = Convert.ToInt32(ViewState["RFPDID"].ToString());
            ds = objRpt.GetRFPMaterialPlanningDetails();

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

            if (ds.Tables[1].Rows.Count > 0)
            {
                gvBlockingMRNQualityClearance.DataSource = ds.Tables[1];
                gvBlockingMRNQualityClearance.DataBind();
            }
            else
            {
                gvBlockingMRNQualityClearance.DataSource = "";
                gvBlockingMRNQualityClearance.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void GetItemName()
    {
        cReports objRpt = new cReports();
        DataSet ds = new DataSet();
        string ItemName = "";
        try
        {
            objRpt.RFPDID = Convert.ToInt32(ViewState["RFPDID"].ToString());
            ds = objRpt.GetItemNameByRFPDID();

            lblItemName.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}