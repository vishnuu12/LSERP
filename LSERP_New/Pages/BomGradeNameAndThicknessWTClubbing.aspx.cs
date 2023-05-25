using eplus.core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_BomGradeNameAndThicknessWTClubbing : System.Web.UI.Page
{
    #region "Declaration"

    cSession objSession = new cSession();
    cProduction objProd;
    cCommon objcommon;

    #endregion

    #region "PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        objSession = Master.csSession;
    }

    #endregion

    #region "PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        string target = Request["__EVENTTARGET"];
        string arg = Request["__EVENTARGUMENT"];

        if (IsPostBack == false)
        {
            objProd = new cProduction();
            DataSet dsEnquiryNumber = new DataSet();
            DataSet dsCustomer = new DataSet();
            dsCustomer = objProd.getCustomerNameByUserIDForProduction(Convert.ToInt32(objSession.employeeid), ddlCustomerName);
            ViewState["CustomerDetails"] = dsCustomer.Tables[1];
            dsEnquiryNumber = objProd.GetEnquiryNumberByUserIDForProduction(Convert.ToInt32(objSession.employeeid), ddlEnquiryNumber);
            ViewState["EnquiryDetails"] = dsEnquiryNumber.Tables[0];
            divOutput.Visible = false;
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlCustomerName_SelectIndexChanged(object sender, EventArgs e)
    {
        objcommon = new cCommon();
        try
        {
            objcommon.customerddlchnage(ddlCustomerName, ddlEnquiryNumber, (DataTable)ViewState["CustomerDetails"], (DataTable)ViewState["EnquiryDetails"]);
            divOutput.Visible = false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlEnquiryNumber_SelectIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt;
        objcommon = new cCommon();
        try
        {
            if (ddlEnquiryNumber.SelectedIndex > 0)
            {
                objcommon.enquiryddlchange(ddlEnquiryNumber, ddlCustomerName, (DataTable)ViewState["CustomerDetails"], (DataTable)ViewState["EnquiryDetails"]);
                // lblEnquiryNumber_A.Text = ddlEnquiryNumber.SelectedItem.Text;
                bindBomGradeNameAndThicknessClubbingWTDetails();
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

    #region "Common Methods"

    private void bindBomGradeNameAndThicknessClubbingWTDetails()
    {
        DataSet ds = new DataSet();
        objProd = new cProduction();
        try
        {
            ds = objProd.GetBomGradenameAndThicknessClubbingWTDetails(Convert.ToInt32(ddlEnquiryNumber.SelectedValue));

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvBomWTClubbing.DataSource = ds.Tables[0];
                gvBomWTClubbing.DataBind();
            }
            else
            {
                gvBomWTClubbing.DataSource = "";
                gvBomWTClubbing.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}