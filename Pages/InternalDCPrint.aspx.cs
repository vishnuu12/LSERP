using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_InternalDCPrint : System.Web.UI.Page
{
    #region"Declaration"

    cStores objSt;
    cSession objSession;

    #endregion

    #region"Page Load Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                hdnIDCID.Value = Request.QueryString["IDCID"];
                BindInternalDCDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region "Button Events"
    protected void btnprint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "InternalDCPrint();", true);
    }

    #endregion

    #region"Gridview Events"

   

    #endregion

    #region"Common Methods"

    private void BindInternalDCDetails()
    {
        try
        {
            DataSet ds = new DataSet();
            objSt = new cStores();
            objSt.IDCID = Convert.ToInt32(hdnIDCID.Value);

            ds = objSt.getInternalDCAndItemDetailsByDCSharedStatus();

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblfootercompanyname_p.Text = ds.Tables[0].Rows[0]["FooterCompanyName"].ToString();
                hdnCompanyname.Value = ds.Tables[0].Rows[0]["CompanyName"].ToString();
                hdnFormalCompanyName.Value = ds.Tables[0].Rows[0]["FormalCompanyName"].ToString();
                hdnFromUnitAddress.Value = ds.Tables[0].Rows[0]["FromUnitAddress"].ToString();
                //LonestarLOGO
                hdnlonestarlogo.Value = ds.Tables[0].Rows[0]["LonestarLOGO"].ToString();

                lbltocompanyname_p.Text = ds.Tables[0].Rows[0]["ToCompanyName"].ToString();
                lblDCNo_p.Text = ds.Tables[0].Rows[0]["DCNumber"].ToString();
                lblDCDate_p.Text = ds.Tables[0].Rows[0]["DCDate"].ToString();
                lblToUnitAddress_p.Text = ds.Tables[0].Rows[0]["ToUnitAddress"].ToString();
                lblExpectedDuration_p.Text = ds.Tables[0].Rows[0]["Duration"].ToString();
                lblReasonRemarks_p.Text = ds.Tables[0].Rows[0]["remarks"].ToString();

                gvItemDescriptionDetails_p.DataSource = ds.Tables[1];
                gvItemDescriptionDetails_p.DataBind();

                lblReceivedBy_p.Text = ds.Tables[0].Rows[0]["ReceiverTo"].ToString();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','Share DC Details')", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}