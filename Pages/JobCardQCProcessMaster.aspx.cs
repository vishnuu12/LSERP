using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_JobCardQCProcessMaster : System.Web.UI.Page
{
    #region"Declaration"

    cCommonMaster objCommon;
    cSession objSession = new cSession();

    #endregion

    #region"PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        objSession = Master.csSession;
    }

    #endregion

    #region "PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];
            if (!IsPostBack)
            {
                objCommon = new cCommonMaster();
                objCommon.GetJobProcessNameMaster(ddlProcessName);
            }

            if (target == "deletegvrow")
            {
                DataSet ds = new DataSet();
                objCommon = new cCommonMaster();
                objCommon.JQMID = arg.ToString();
                ds = objCommon.DeleteJobCardProcessQCStageDetailsByJQMID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Process QC Stage Deleted Successfully');", true);
                BindJobCardProcessQCStageMaster();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"PageLoad Events"

    protected void ddlProcessName_SelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindJobCardProcessQCStageMaster();
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
        objCommon = new cCommonMaster();
        try
        {
            objCommon.JQMID = hdnJQMID.Value;
            objCommon.PMID = Convert.ToInt32(ddlProcessName.SelectedValue);
            objCommon.stage = txtStage.Text;
            objCommon.CreatedBy = objSession.employeeid;
            ds = objCommon.SaveJobCardProcessNameQCStageDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Stage Saved Successfully');", true);
                BindJobCardProcessQCStageMaster();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindJobCardProcessQCStageMaster()
    {
        objCommon = new cCommonMaster();
        DataSet ds = new DataSet();
        try
        {
            objCommon.PMID = Convert.ToInt32(ddlProcessName.SelectedValue);
            ds = objCommon.GetJobCardProcessQCStageMaster();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvJobCardQCProcessMaster.DataSource = ds.Tables[0];
                gvJobCardQCProcessMaster.DataBind();
            }
            else
            {
                gvJobCardQCProcessMaster.DataSource = "";
                gvJobCardQCProcessMaster.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

}