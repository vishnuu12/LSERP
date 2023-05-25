using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_GeneralMaterialMRNIssueDetails : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cStores objSt;

    #endregion

    #region"PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        objSession = Master.csSession;
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
                BindRFPNoAndMRNNoDetails();
                BindGeneralMRNMaterialIssueDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlMRNNo_SelectIndexChanged(object sender, EventArgs e)
    {
        objSt = new cStores();
        DataSet ds = new DataSet();
        try
        {
            objSt.MRNID = Convert.ToInt32(ddlMRNNo.SelectedValue.Split('/')[0].ToString());
            objSt.LocationID = Convert.ToInt32(ddlMRNNo.SelectedValue.Split('/')[1].ToString());

            ds = objSt.GetStockMonitorReportDetailsbyMRNID();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ds.Tables[0].Columns.Remove("Cost");

                gvStockMonitorDetails.DataSource = ds.Tables[0];
                gvStockMonitorDetails.DataBind();
                hdnstockqty.Value = ds.Tables[0].Rows[0]["ActualStock"].ToString();

            }
            else
            {
                gvStockMonitorDetails.DataSource = "";
                gvStockMonitorDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnSaveGeneralIssue_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objSt = new cStores();
        try
        {
            objSt.RFPHID = ddlRFPNo.SelectedValue;
            objSt.MRNID = Convert.ToInt32(ddlMRNNo.SelectedValue.Split('/')[0].ToString());
            objSt.GeneralIssuedqty = Convert.ToDecimal(txtIssuedQty.Text);
            objSt.CreatedBy = Convert.ToInt32(objSession.employeeid);
            objSt.MRNLocationID = Convert.ToInt32(ddlMRNNo.SelectedValue.Split('/')[1].ToString());

            objSt.Remarks = txtRemarks.Text;

            ds = objSt.SaveGeneralMRNIssueDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','General Material Issue Details Saved successfully');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
            BindGeneralMRNMaterialIssueDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvGeneralMRNIssueDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName.ToString() == "DeleteMRN")
            {
                DataSet ds = new DataSet();
                objSt = new cStores();
                string GMIDID = gvGeneralMRNIssueDetails.DataKeys[index].Values[0].ToString();
                objSt.GMIDID = GMIDID;

                ds = objSt.DeleteGeneralMaterialMRNIssueDetailsByGMIDID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','General Material Issue Details Deleted successfully');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "InfoMessage('information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
                BindGeneralMRNMaterialIssueDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindRFPNoAndMRNNoDetails()
    {
        DataSet ds = new DataSet();
        objSt = new cStores();
        try
        {
            objSt.GetRFPNoDetailsByGeneralMRNIssue(ddlRFPNo);
            objSt.BindMRNNoDetailsByGeneralMRNIssue(ddlMRNNo);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindGeneralMRNMaterialIssueDetails()
    {
        DataSet ds = new DataSet();
        objSt = new cStores();
        try
        {
            ds = objSt.BindGeneralMRNMaterialIssueDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvGeneralMRNIssueDetails.DataSource = ds.Tables[0];
                gvGeneralMRNIssueDetails.DataBind();
            }
            else
            {
                gvGeneralMRNIssueDetails.DataSource = "";
                gvGeneralMRNIssueDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

}