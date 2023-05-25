using eplus.core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_AssemplyCompletedRFPReport : System.Web.UI.Page
{
    #region"DecLaration"

    cSales objSales;
    cSession objSession = new cSession();
    cReports objR;

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
            if (IsPostBack == false)
                BindAssemplyCompletedRFPReportDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion


    #region"Common Methods"

    private void BindAssemplyCompletedRFPReportDetails()
    {
        objR = new cReports();
        DataSet ds = new DataSet();
        try
        {
            ds = objR.GetAssemplyCompletedRFPReportDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvAssemplyRFPReport.DataSource = ds.Tables[0];
                gvAssemplyRFPReport.DataBind();
            }
            else
            {
                gvAssemplyRFPReport.DataSource = "";
                gvAssemplyRFPReport.DataBind();
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
        if (gvAssemplyRFPReport.Rows.Count > 0)
        {
            gvAssemplyRFPReport.UseAccessibleHeader = true;
            gvAssemplyRFPReport.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    #endregion
}