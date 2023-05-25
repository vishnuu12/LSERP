using eplus.core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_AddRFPPaymentCollection : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cSales objSales;
    cCommon objc;

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
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];
            if (IsPostBack == false)
            {
                DataSet ds = new DataSet();
                DataSet dsCust = new DataSet();
                objSales = new cSales();
                objc = new cCommon();

                dsCust = objSales.GetCustomerPODetailsByPaymentStatusUpdate(ddlCustomerName);
                ds = objSales.BindInvoiceNoForPayment(ddlInvoiceNo);
                objSales.GetPaymentModeDetails(ddlPaymentMode);
                BindInvoiceDatas();
            }
            //if (objSession.DepID == 3) { }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Radio Events"

    protected void rblPaymenttype_OnSelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (rblPaymenttype.SelectedValue == "Domestic")
            {
                divTDS.Visible = true;
                divExchangerate.Visible = false;
            }
            else
            {
                divTDS.Visible = false;
                divExchangerate.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"


    protected void ddlInvoiceNo_OnSelectIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objSales = new cSales();
        try
        {
            if (ddlInvoiceNo.SelectedValue != "AD" && ddlInvoiceNo.SelectedValue != "0")
            {
                //objSales.InvoiceRFPHID = ddlRFPNo.SelectedValue;
                objSales.IDID = ddlInvoiceNo.SelectedValue;
                ds = objSales.GetInvoiceAmountDetailsbyInvoiceIDPayment();
                //BalanceAmt PaidAmt

                ddlCustomerName.SelectedValue = ds.Tables[1].Rows[0]["ProspectID"].ToString();
                lblpaidamt.Text = ds.Tables[0].Rows[0]["PaidAmt"].ToString();
                lblbalanceamt.Text = ds.Tables[0].Rows[0]["BalanceAmt"].ToString();
                lblInvoiceValue.Text = ds.Tables[0].Rows[0]["InvoiceValue"].ToString();
            }
            else
            {
                lblInvoiceValue.Text = "";
                lblpaidamt.Text = "";
                lblbalanceamt.Text = "";
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    protected void ddlCustomerName_SelectIndexChanged(object sender, EventArgs e)
    {
        DataTable dt;
        try
        {
            /* dt = (DataTable)ViewState["RFPDetails"];
            if (ddlCustomerName.SelectedIndex > 0)
            {
                string ProspectID = ddlCustomerName.SelectedValue;
                dt.DefaultView.RowFilter = "ProspectID='" + ProspectID + "'";
                dt.DefaultView.ToTable();
            } */
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"
    protected void btnCollection_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objSales = new cSales();
        try
        {
            //objSales.InvoiceRFPHID = ddlRFPNo.SelectedValue;
            objSales.CollectionAmount = txtCollectionAmount.Text;
            objSales.PaymentMode = ddlPaymentMode.SelectedValue;
            objSales.Remarks = txtPaymentstatusremarks.Text;
            objSales.CreatedBy = objSession.employeeid;

            objSales.TDSDeducted = txtTDSDeducted.Text;

            objSales.currencyExchangerate = txtCurrencyExchangerate.Text;

            objSales.IDID = ddlInvoiceNo.SelectedValue;
            objSales.InVoiceType = rblPaymenttype.SelectedValue;

            objSales.RefNo = txtrefNo.Text;
            objSales.PaymentDate = DateTime.ParseExact(txtpaymentdate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            ds = objSales.SavePaymentCollectionDetailsPayment(rblPaymenttype.SelectedValue);

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Collection Details Saved Succesfully');hideCollectionPopup();", true);
            //ddlinvoicetype_OnSelectIndexChanged(null, null);
            BindInvoiceDatas();
            ClearDatas();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindInvoiceDatas()
    {
        DataSet ds = new DataSet();
        objSales = new cSales();
        try
        {
            ds = objSales.BindInvoiceDatas();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvColectionDetails.DataSource = ds.Tables[0];
                gvColectionDetails.DataBind();
            }
            else
            {
                gvColectionDetails.DataSource = "";
                gvColectionDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ClearDatas()
    {
        ddlCustomerName.SelectedValue = "0";
        ddlInvoiceNo.SelectedValue = "0";
        ddlPaymentMode.SelectedValue = "0";
        lblInvoiceValue.Text = "0";
        lblpaidamt.Text = "0";
        lblbalanceamt.Text = "0";
        txtCollectionAmount.Text = "";
        txtTDSDeducted.Text = "";
        txtCurrencyExchangerate.Text = "";
        txtrefNo.Text = "";
        txtpaymentdate.Text = "";
        txtPaymentstatusremarks.Text = "";
    }
        #endregion

    #region"GridView Events"

        protected void gvColectionDetails_OnrowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName.ToString() == "DeleteCollectionPayment")
            {
                objSales = new cSales();
                DataSet ds = new DataSet();
                objSales.PCDID = gvColectionDetails.DataKeys[index].Values[0].ToString();

                ds = objSales.DeleteCollectionPaymentDetailsByPCDID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Invoice Details Deleted');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
                //ddlinvoicetype_OnSelectIndexChanged(null, null);
                BindInvoiceDatas();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    protected void gvColectionDetails_OnrowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //LinkButton btnaddinvoiceno = (LinkButton)e.Row.FindControl("btnaddinvoiceno");
                //if (dr["InvoiceNo"].ToString() == "Advance")
                //    btnaddinvoiceno.Visible = true;
                //else
                //    btnaddinvoiceno.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    #endregion
}