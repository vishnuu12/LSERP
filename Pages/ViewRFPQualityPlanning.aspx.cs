using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_ViewRFPQualityPlanning : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cReports objR;

    #endregion

    #region"Page Load Events"
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ViewState["RFPDID"] = Request.QueryString["RFPDID"].ToString();
                BindRFPQualityPlanningDetails();
                BindRFPAssemplyPlanningDetails();
                GetItemName();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindRFPQualityPlanningDetails()
    {
        objR = new cReports();
        DataSet ds = new DataSet();
        try
        {
            objR.RFPDID = Convert.ToInt32(ViewState["RFPDID"].ToString());
            ds = objR.GetRFPQualityPlanningDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvPartNameDetails.DataSource = ds.Tables[0];
                gvPartNameDetails.DataBind();
            }
            else
            {
                gvPartNameDetails.DataSource = "";
                gvPartNameDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindRFPAssemplyPlanningDetails()
    {
        objR = new cReports();
        DataSet ds = new DataSet();
        try
        {
            objR.RFPDID = Convert.ToInt32(ViewState["RFPDID"].ToString());
            ds = objR.GetRFPAssemplyPlanningDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvAssemplyPlanningDetails_AP.DataSource = ds.Tables[0];
                gvAssemplyPlanningDetails_AP.DataBind();
            }
            else
            {
                gvAssemplyPlanningDetails_AP.DataSource = "";
                gvAssemplyPlanningDetails_AP.DataBind();
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