using JqDatatablesWebForm.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_PartWiseRFPStatusReport : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cReports objR;
    cMaterials objMat;

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
                BindPartDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlPartName_OnSelectIndexChanged(object sender, EventArgs e)
    {
        if (ddlPartName.SelectedIndex > 0)
        {
            divOutput.Visible = true;
            bindPartWiseRFPStatusReportDetails();
        }
        else
            divOutput.Visible = false;
    }

    #endregion

    #region"Common Methods"

    private void bindPartWiseRFPStatusReportDetails()
    {
        DataSet ds = new DataSet();
        objR = new cReports();
        try
        {
            objR.MID = ddlPartName.SelectedValue;
            ds = objR.GetPartWiseRFPStatusReportDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvRFPDetails.DataSource = ds.Tables[0];
                gvRFPDetails.DataBind();
            }
            else
            {
                gvRFPDetails.DataSource = "";
                gvRFPDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindPartDetails()
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            objMat.EDID = 0;
            objMat.versionNumber = 0;
            ds = objMat.GetMaterialList(ddlPartName);
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
        if (gvRFPDetails.Rows.Count > 0)
        {
            gvRFPDetails.UseAccessibleHeader = true;
            gvRFPDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    #endregion
}