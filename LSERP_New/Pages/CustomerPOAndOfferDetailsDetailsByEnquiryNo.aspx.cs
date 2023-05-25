using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_CustomerPOAndOfferDetailsDetailsByEnquiryNo : System.Web.UI.Page
{
    #region"Declaration"

    cReports objRpt;

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ViewState["EnquiryID"] = Request.QueryString["EnquiryID"].ToString();
                BindCustomerPOAndOfferDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindCustomerPOAndOfferDetails()
    {
        DataSet ds = new DataSet();
        objRpt = new cReports();
        try
        {
            objRpt.EnquiryID = Convert.ToInt32(ViewState["EnquiryID"].ToString());
            ds = objRpt.GetCustomerPOAndOfferEnquiryNoDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvCustomerPODetails.DataSource = ds.Tables[0];
                gvCustomerPODetails.DataBind();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLoader", "AddFooter();", true);
            }
            else
            {
                gvCustomerPODetails.DataSource = "";
                gvCustomerPODetails.DataBind();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                gvEnquiryOfferDetails.DataSource = ds.Tables[1];
                gvEnquiryOfferDetails.DataBind();
            }
            else
            {
                gvEnquiryOfferDetails.DataSource = "";
                gvEnquiryOfferDetails.DataBind();
            }



        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}