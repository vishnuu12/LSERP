using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_MRNLifeCycleReport : System.Web.UI.Page
{
    #region "Declaration"

    cReports objRpt;

    #endregion

    #region "PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                objRpt = new cReports();
                objRpt.GetMrnNoDetails(ddlMRNNo);
                divOutput.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnprint_Click(object sender, EventArgs e)
    {
        try
        {
            var page = HttpContext.Current.CurrentHandler as Page;
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 1].ToString();

            string Page = url.Replace(Replacevalue, "MRNPrintDetails.aspx?MIDID=" + hdnMIDID.Value + "");

            string s = "window.open('" + Page + "','_blank');";
            this.ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Drop Down Events"

    protected void ddlMRNNo_OnSelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlMRNNo.SelectedIndex > 0)
            {
                BindMRNLifeCycleReportDetails();
                divOutput.Visible = true;
            }
            else
                divOutput.Visible = false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindMRNLifeCycleReportDetails()
    {
        DataSet ds = new DataSet();
        objRpt = new cReports();
        try
        {
            objRpt.MRNID = ddlMRNNo.SelectedValue;
            ds = objRpt.GetMRNLifeCycleReportDetails();

            hdnMIDID.Value = ds.Tables[0].Rows[0]["MIDID"].ToString();

            lblMRNNo.Text = ds.Tables[0].Rows[0]["MRNNo"].ToString();
            lblcategoryname.Text = ds.Tables[0].Rows[0]["CategoryName"].ToString();
            lblGradeName.Text = ds.Tables[0].Rows[0]["GradeName"].ToString();
            lbluom.Text = ds.Tables[0].Rows[0]["UOM"].ToString();
            lblInwardqty.Text = ds.Tables[0].Rows[0]["InwardQty"].ToString();
            lblThickness.Text = ds.Tables[0].Rows[0]["Thickness"].ToString();

            lblLocation.Text = ds.Tables[0].Rows[0]["Location"].ToString();

            lblTotalBlockedQty.Text = ds.Tables[2].Rows[0]["InHand"].ToString();
            lblTotalIssuedQty.Text = ds.Tables[2].Rows[0]["IssuedQty"].ToString();
            lblInstockQty.Text = ds.Tables[2].Rows[0]["Instock"].ToString();

            lblPONo.Text = ds.Tables[0].Rows[0]["PONO"].ToString();
            lblunitcost.Text = ds.Tables[0].Rows[0]["UnitCost"].ToString();
            lblsuppliername.Text = ds.Tables[0].Rows[0]["SupplierName"].ToString();

            lblinwardtype.Text = ds.Tables[0].Rows[0]["InwardType"].ToString();

            if (ds.Tables[1].Rows.Count > 0)
            {
                gvJobCardIssueDetails.DataSource = ds.Tables[1];
                gvJobCardIssueDetails.DataBind();
            }
            else
            {
                gvJobCardIssueDetails.DataSource = "";
                gvJobCardIssueDetails.DataBind();
            }

            if (ds.Tables[3].Rows.Count > 0)
            {
                gvGeneralMRNIssueDetails.DataSource = ds.Tables[3];
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

    #region"PageLoad Events"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvJobCardIssueDetails.Rows.Count > 0)
        {
            gvJobCardIssueDetails.UseAccessibleHeader = true;
            gvJobCardIssueDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    #endregion
}