using eplus.core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_DailyActivitiesQualityStageInspectionReport : System.Web.UI.Page
{
    #region"Declarartion"

    cSession objSession = new cSession();
    cQuality objQ;
    cCommon objcommon;

    #endregion

    #region"PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        objSession = Master.csSession;
    }

    #endregion

    #region"Page Load Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];

            if (!IsPostBack)
            {
                objQ = new cQuality();
                DataSet dsEnquiryNumber = new DataSet();
                DataSet ds = new DataSet();
                ds = objQ.GetDailyActivitiesQualityRFPDetailsByEmployeeID(Convert.ToInt32(objSession.employeeid), ddlRFPNo, ddlRaisedBy);
                //ViewState["EnquiryDetails"] = ds.Tables[0];
                //ViewState["CustomerDetails"] = ds.Tables[1];
                ShowHideControls("add");
            }

            if (target == "Delete")
            {
                objcommon = new cCommon();
                int PartId = Convert.ToInt32(arg);
                DataSet ds = objcommon.deleteDailyActivitiesReportDetailsDetailsByID(PartId, "LS_DeleteDailyActivitiesProductionRFPDetailsById");
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Part Name Deleted successfully');", true);
                BindDailyActivitiesProductionDetails();
            }
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
        DataSet ds = new DataSet();
        objQ = new cQuality();
        DataTable dcustomr;
        DataTable denquiry;
        try
        {
            if (ddlRFPNo.SelectedIndex > 0)
            {
                BindDailyActivitiesProductionDetails();
                ShowHideControls("add,addnew,view,input");
            }
            else
                ShowHideControls("add");
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
        objQ = new cQuality();
        DataSet ds = new DataSet();
        bool rsdate = true;
        try
        {

            objQ.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            objQ.StageInspection = ddlstageInspection.SelectedValue;

            objQ.Remarks = txtRemarks.Text;
            objQ.UserID = objSession.employeeid;
            objQ.RaisedBy = ddlRaisedBy.SelectedValue;

            ds = objQ.SaveQualityStageInspectionReportDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Details Saved Succesfully');", true);
            BindDailyActivitiesProductionDetails();
            // ShowHideControls("add,view");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        // ShowHideControls("add,view,input");
    }

    #endregion

    #region"Common Methods"

    private void BindDailyActivitiesProductionDetails()
    {
        DataSet ds = new DataSet();
        objQ = new cQuality();
        try
        {
            objQ.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objQ.GetQualityStageInspectionReportDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDailyActivitiesReport.DataSource = ds.Tables[0];
                gvDailyActivitiesReport.DataBind();
            }
            else
            {
                gvDailyActivitiesReport.DataSource = "";
                gvDailyActivitiesReport.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ShowHideControls(string divids)
    {
        try
        {
            divAdd.Visible = divAddnew.Visible = divInput.Visible = divOutput.Visible = false;
            string[] mode = divids.Split(',');
            for (int i = 0; i < mode.Length; i++)
            {
                switch (mode[i].ToLower())
                {
                    case "add":
                        divAdd.Visible = true;
                        break;
                    case "view":
                        divOutput.Visible = true;
                        break;
                    case "input":
                        divInput.Visible = true;
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}