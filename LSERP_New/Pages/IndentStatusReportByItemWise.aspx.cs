using eplus.core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_IndentStatusReportByItemWise : System.Web.UI.Page
{
    #region"Declaration"

    cReports objRp;

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                DataSet ds = new DataSet();
                objRp = new cReports();

                ds = objRp.GetRFPNoDetails(ddlRFPNo);
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
        objRp = new cReports();
        try
        {
            if (ddlRFPNo.SelectedIndex > 0)
            {
                objRp.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
                ds = objRp.GetItemNameDetailsByRFPHID(ddlItemName);
            }
            else
            {
                cCommon objc = new cCommon();
                objc.EmptyDropDownList(ddlItemName);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlItemName_SelectIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objRp = new cReports();
        try
        {
            objRp.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            objRp.RFPDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[0].ToString());

            ds = objRp.GetPartDetailsByRFPHIDAndRFPDID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvpartDetails.DataSource = ds.Tables[0];
                gvpartDetails.DataBind();
            }
            else
            {
                gvpartDetails.DataSource = "";
                gvpartDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvpartDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

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
        if (gvpartDetails.Rows.Count > 0)
        {
            gvpartDetails.UseAccessibleHeader = true;
            gvpartDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    #endregion
}