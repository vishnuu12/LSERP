using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Data;

public partial class Pages_EnquiryTradingReport : System.Web.UI.Page
{
    #region"Declaration"

    cSales objSales;

    #endregion

    #region"PageLoad Eevnts"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFromDate.Enabled = txtToDate.Enabled = btnSubmit.Enabled = false;
            GetEnquiryTradingReportDetails();
        }
    }

    #endregion

    #region"radioButon Events"

    protected void rblDate_OnSelectedChanged(object sender, EventArgs e)
    {
        if (rblDate.SelectedValue == "Custom")
        {
            txtFromDate.Enabled = txtToDate.Enabled = btnSubmit.Enabled = true;
            txtFromDate.Text = txtToDate.Text = "";
            ShowHideControls("add");
        }
        else
        {
            txtFromDate.Enabled = txtToDate.Enabled = btnSubmit.Enabled = false;
            GetEnquiryTradingReportDetails();
            ShowHideControls("add,view");
        }
    }

    #endregion

    #region"Button Events"

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        GetEnquiryTradingReportDetails();
        ShowHideControls("add,view");
    }

    #endregion


    #region"Common Methods"

    private void GetEnquiryTradingReportDetails()
    {
        objSales = new cSales();
        DataSet ds = new DataSet();
        try
        {
            objSales.Mode = rblDate.SelectedValue;

            if (rblDate.SelectedValue == "Custom")
            {
                objSales.FromDate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture); ;
                objSales.ToDate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            ds = objSales.GetEnquiryTradingReportDetails();

            lblTotNoOfEnq.Text = ds.Tables[0].Rows[0]["TotalEnquiryID"].ToString();
            lblTotNoOfBudgetaryEnquiries.Text = ds.Tables[1].Rows[0]["TotalBudgetaryEnquiry"].ToString();
            lblPurOrderEnquiries.Text = ds.Tables[2].Rows[0]["TotalPurchaseOrderEnquiry"].ToString();

            if (ds.Tables[3].Rows.Count > 0)
            {
                gvEnquiryTradingReport.DataSource = ds.Tables[3];
                gvEnquiryTradingReport.DataBind();
            }
            else
            {
                gvEnquiryTradingReport.DataSource = "";
                gvEnquiryTradingReport.DataBind();
            }

            if (rblDate.SelectedValue == "Today")
                txtFromDate.Text = txtToDate.Text = ds.Tables[4].Rows[0]["Today"].ToString();
            if (rblDate.SelectedValue == "Week")
            {
                txtFromDate.Text = ds.Tables[4].Rows[0]["FromDate"].ToString();
                txtToDate.Text = ds.Tables[4].Rows[0]["ToDate"].ToString();
            }

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void ShowHideControls(string divids)
    {
        try
        {
            string[] mode = divids.Split(',');
            divAdd.Visible = divOutput.Visible = false;
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
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"PageLoadComplete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvEnquiryTradingReport.Rows.Count > 0)
            gvEnquiryTradingReport.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion

}