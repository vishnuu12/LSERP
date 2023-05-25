using eplus.core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_OfferStatusHistory : System.Web.UI.Page
{

    #region"Declaration"

    cSession objSession = new cSession();
    cSales objSales;
    cCommon objc;
    string DrawingDocumentSavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString();
    string DrawingDocumentHttpPath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString().Trim();

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];
            if (IsPostBack == false)
            {
                objSession = (cSession)HttpContext.Current.Session["LoginDetails"];

                objSales = new cSales();
                DataSet dsEnquiryNumber = new DataSet();
                DataSet dsCustomer = new DataSet();
                dsCustomer = objSales.GetCustomerNameByUserIDAndUnRaisedPO(Convert.ToInt32(objSession.employeeid), ddlCustomerName);
                ViewState["CustomerDetails"] = dsCustomer.Tables[1];
                dsEnquiryNumber = objSales.GetEnquiryNumberByUserIDAndUnRaisedPO(Convert.ToInt32(objSession.employeeid), ddlEnquiryNumber);
                ViewState["EnquiryDetails"] = dsEnquiryNumber.Tables[0];

                objSales.GetOfferStatusType(rblOfferStatusType);

                bindOfferStatusDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"radio Events"

    protected void rblOfferStatusType_OnSelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rblOfferStatusType.SelectedValue == "1" || rblOfferStatusType.SelectedValue == "7")
            {
                objSales = new cSales();
                divOfferLost.Visible = true;
                divDate.Visible = false;

                objSales.GetOrderLostDetails(ddlOfferLost);
            }
            else if (rblOfferStatusType.SelectedValue == "2" || rblOfferStatusType.SelectedValue == "3" || rblOfferStatusType.SelectedValue == "4" || rblOfferStatusType.SelectedValue == "8")
            {
                divOfferLost.Visible = false;
                divDate.Visible = true;
            }
            else
            {
                divOfferLost.Visible = false;
                divDate.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlCustomerName_SelectIndexChanged(object sender, EventArgs e)
    {
        objc = new cCommon();
        try
        {
            objc.customerddlchnage(ddlCustomerName, ddlEnquiryNumber, (DataTable)ViewState["CustomerDetails"], (DataTable)ViewState["EnquiryDetails"]);

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
        objc = new cCommon();
        objSales = new cSales();
        try
        {
            if (ddlEnquiryNumber.SelectedIndex > 0)
            {
                objc.enquiryddlchange(ddlEnquiryNumber, ddlCustomerName, (DataTable)ViewState["CustomerDetails"], (DataTable)ViewState["EnquiryDetails"]);

                objSales.EnquiryNumber = ddlEnquiryNumber.SelectedValue;

                objSales.GetOfferNoDetailsByEnquiryNumber(ddlOfferNo);


            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnOfferReviewStatus_Click(object sender, EventArgs e)
    {
        objSales = new cSales();
        DataSet ds = new DataSet();
        string msg = "1";
        try
        {
            if (rblOfferStatusType.SelectedValue != "")
            {
                objSession = (cSession)HttpContext.Current.Session["LoginDetails"];

                objSales.OSHID = Convert.ToInt32(hdnOSHID.Value);
                objSales.EODID = Convert.ToInt32(ddlOfferNo.SelectedValue);
                objSales.EnquiryNumber = ddlEnquiryNumber.SelectedValue;
                objSales.OSTID = Convert.ToInt32(rblOfferStatusType.SelectedValue);

                if (rblOfferStatusType.SelectedValue == "1")
                {
                    msg = "1";
                    objSales.OLDID = Convert.ToInt32(ddlOfferLost.SelectedValue);
                }
                else if (rblOfferStatusType.SelectedValue == "2" || rblOfferStatusType.SelectedValue == "3" || rblOfferStatusType.SelectedValue == "4")
                {
                    msg = "2";
                    objSales.ReviewDate = DateTime.ParseExact(txtNextReviewDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                else
                    msg = "3";

                objSales.Remarks = txtRemarks.Text;
                objSales.CreatedBy = objSession.employeeid;

                ds = objSales.SaveOfferStatusHistoryDetails(msg);

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Offer Details Saved successfully');", true);
                else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Offer Details Updated successfully');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);

                bindOfferStatusDetails();

                clearValues();

            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Select Offer Type');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        clearValues();
    }

    #endregion

    #region"GridView Events"

    protected void gvOfferStatusHistory_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName.ToString() == "EditOffer")
            {
                objSales = new cSales();
                DataSet ds = new DataSet();

                objSales.OSHID = Convert.ToInt32(gvOfferStatusHistory.DataKeys[index].Values[0].ToString());

                ds = objSales.GetOfferStatusDetailsByOSHID();

                hdnOSHID.Value = ds.Tables[0].Rows[0]["OSHID"].ToString();

                ddlEnquiryNumber.SelectedValue = ds.Tables[0].Rows[0]["EnquiryNumber"].ToString();
                ddlCustomerName.SelectedValue = ds.Tables[0].Rows[0]["ProspectID"].ToString();

                ddlEnquiryNumber_SelectIndexChanged(null, null);

                ddlOfferNo.SelectedValue = ds.Tables[0].Rows[0]["EODID"].ToString();

                rblOfferStatusType.SelectedValue = ds.Tables[0].Rows[0]["OSTID"].ToString();

                rblOfferStatusType_OnSelectIndexChanged(null, null);

                //  txtNextReviewDate.Text = ds.Tables[0].Rows[0]["ReviewDateEdit"].ToString();

                txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common methods"

    private void bindOfferStatusDetails()
    {
        DataSet ds = new DataSet();
        objSales = new cSales();
        try
        {
            ds = objSales.GetOfferStatusDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvOfferStatusHistory.DataSource = ds.Tables[0];
                gvOfferStatusHistory.DataBind();
            }
            else
            {
                gvOfferStatusHistory.DataSource = "";
                gvOfferStatusHistory.DataBind();
            }

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void clearValues()
    {
        hdnOSHID.Value = "0";
        ddlEnquiryNumber.SelectedIndex = 0;
        ddlCustomerName.SelectedIndex = 0;
        ddlOfferNo.SelectedIndex = 0;
        rblOfferStatusType.ClearSelection();
        ddlOfferLost.SelectedIndex = 0;
        txtRemarks.Text = "";
        txtNextReviewDate.Text = "";
    }

    #endregion

    #region"PageLoad Complete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvOfferStatusHistory.Rows.Count > 0)
        {
            gvOfferStatusHistory.UseAccessibleHeader = true;
            gvOfferStatusHistory.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    #endregion


}