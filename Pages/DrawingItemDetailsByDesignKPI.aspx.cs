using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_DrawingItemDetailsByDesignKPI : System.Web.UI.Page
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
            {
                divOutput.Visible = true;
                divInput.Visible = true;

                ViewState["EnquiryID"] = Request.QueryString["EnquiryID"].ToString();
                BindDrawingItemDetailsByDesignKPI();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvEnquiryProjectAssignmentReport_DataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //  e.Row.Cells[0].Attributes.Add("style", "text-align:center;");

                if (string.IsNullOrEmpty(dr["Offer No"].ToString()))
                {
                    e.Row.Cells[3].Text = "NA";
                    e.Row.CssClass = "warning";
                }

                if (string.IsNullOrEmpty(dr["Order No"].ToString()))
                    e.Row.Cells[5].Text = "NA";
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindDrawingItemDetailsByDesignKPI()
    {
        DataSet ds = new DataSet();
        objR = new cReports();
        try
        {
            objR.EnquiryID = Convert.ToInt32(ViewState["EnquiryID"].ToString());
            ds = objR.GetDrawingItemDetailsDesignKPIByEnquiryID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDrawingItemDetailsDesignKPI.DataSource = ds.Tables[0];
                gvDrawingItemDetailsDesignKPI.DataBind();
            }
            else
            {
                gvDrawingItemDetailsDesignKPI.DataSource = "";
                gvDrawingItemDetailsDesignKPI.DataBind();
            }

            lblenquiryNo.Text = ds.Tables[1].Rows[0]["EnquiryID"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

}