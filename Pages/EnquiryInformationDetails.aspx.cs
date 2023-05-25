using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Pages_EnquiryInformationDetails : System.Web.UI.Page
{
    #region"PageInit Events"

    cReports objRpt;

    #endregion

    #region "PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ViewState["EnquiryID"] = Request.QueryString["EnquiryID"].ToString();
                BindEnquiryInformationDetailsByEnquiryNumber();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region "Common Methods"    

    private void BindEnquiryInformationDetailsByEnquiryNumber()
    {
        DataSet ds = new DataSet();
        objRpt = new cReports();
        try
        {
            objRpt.EnquiryID = Convert.ToInt32(ViewState["EnquiryID"].ToString());
            ds = objRpt.GetEnquiryInformationDetailsByEnquiryNumber();

            if (ds.Tables[1].Rows.Count > 0)
            {
                gvItemDetails.DataSource = ds.Tables[1];
                gvItemDetails.DataBind();
            }
            else
            {
                gvItemDetails.DataSource = "";
                gvItemDetails.DataBind();
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                gvOfferRev.DataSource = ds.Tables[2];
                gvOfferRev.DataBind();
            }
            else
            {
                gvOfferRev.DataSource = "";
                gvOfferRev.DataBind();
            }

            lblheading_p.Text = ds.Tables[0].Rows[0]["EnquiryNo"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}