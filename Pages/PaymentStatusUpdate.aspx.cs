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

public partial class Pages_PaymentStatusUpdate : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cSales objSales;
    cCommon objc;
    string InvoiceDocsSavePath = ConfigurationManager.AppSettings["InvoiceDocumentSavePath"].ToString();
    string InvoiceDocsHttpPath = ConfigurationManager.AppSettings["InvoiceDocumentHttpPath"].ToString();

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
                ds = objSales.GetRFPNODetails(ddlRFPNo);
                objSales.GetInvoiceTypeName(ddlinvoicetype);

                ViewState["CustomerDetails"] = dsCust.Tables[0];
                ViewState["RFPDetails"] = ds.Tables[0];

                objSales.GetPaymentModeDetails(ddlPaymentMode);
                objSales.GetPaymentDays(ddlpaymentdays);
                objc.GetLocationDetails(ddlLocation);

                divOutput.Visible = false;
                divInput.Visible = false;
            }
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

    //protected void ddlPONo_SelectIndexChanged(object sender, EventArgs e)
    //{
    //    DataSet ds = new DataSet();
    //    objSales = new cSales();
    //    try
    //    {
    //        if (ddlPONo.SelectedIndex > 0)
    //        {
    //            objSales.POHID = ddlPONo.SelectedValue;
    //            ds = objSales.GetInvoiceDetailsByPOHID();

    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                gvInvoiceDetails.DataSource = ds.Tables[0];
    //                gvInvoiceDetails.DataBind();
    //            }
    //            else
    //            {
    //                gvInvoiceDetails.DataSource = "";
    //                gvInvoiceDetails.DataBind();
    //            }

    //            if (ds.Tables[1].Rows.Count > 0)
    //            {
    //                gvColectionDetails.DataSource = ds.Tables[1];
    //                gvColectionDetails.DataBind();
    //            }
    //            else
    //            {
    //                gvColectionDetails.DataSource = "";
    //                gvColectionDetails.DataBind();
    //            }

    //            lblPoTotalValue.Text = ds.Tables[2].Rows[0]["PoTotalValue"].ToString();
    //        }
    //        else
    //        {
    //            divOutput.Visible = false;
    //            divInput.Visible = false;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    //protected void ddlRFPNo_SelectIndexChanged(object sender, EventArgs e)
    //{
    //    DataSet ds = new DataSet();
    //    objSales = new cSales();
    //    try
    //    {
    //        divOutput.Visible = true;
    //        divInput.Visible = true;
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    protected void ddlRFPNo_SelectIndexChanged(object sender, EventArgs e)
    {
        objSales = new cSales();
        DataSet ds = new DataSet();
        DataTable dt;
        try
        {
            ddlinvoicetype.SelectedIndex = 0;
            if (ddlRFPNo.SelectedIndex > 0)
            {
                objSales.InvoiceRFPHID = ddlRFPNo.SelectedValue;
                /// objSales.POHID = ddlPONo.SelectedValue;
                ds = objSales.GetCustomerNameByRFPHIDPaymentStatusUpdate();

                if (ddlRFPNo.SelectedValue == "G")
                    ddlCustomerName.SelectedValue = "G";
                else
                    ddlCustomerName.SelectedValue = ds.Tables[0].Rows[0]["ProspectID"].ToString();

                //if (ds.Tables[0].Rows.Count > 0)
                //{
                //    gvInvoiceDetails.DataSource = ds.Tables[0];
                //    gvInvoiceDetails.DataBind();
                //}
                //else
                //{
                //    gvInvoiceDetails.DataSource = "";
                //    gvInvoiceDetails.DataBind();
                //}

                //if (ds.Tables[1].Rows.Count > 0)
                //{
                //    gvColectionDetails.DataSource = ds.Tables[1];
                //    gvColectionDetails.DataBind();
                //}
                //else
                //{
                //    gvColectionDetails.DataSource = "";
                //    gvColectionDetails.DataBind();
                //}

                //lblPoTotalValue.Text = ds.Tables[2].Rows[0]["PoTotalValue"].ToString();

                //BindInvoiceNo();
            }

            divOutput.Visible = false;
            divInput.Visible = false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlinvoicetype_OnSelectIndexChanged(object sender, EventArgs e)
    {
        objSales = new cSales();
        DataSet ds = new DataSet();
        try
        {
            if (ddlinvoicetype.SelectedIndex > 0)
            {
                divOutput.Visible = true;
                divInput.Visible = true;

                if (ddlinvoicetype.SelectedValue == "2" || ddlinvoicetype.SelectedValue == "3")
                {
                    ddlRFPNo.SelectedValue = "G";
                    ddlCustomerName.SelectedValue = "G";
                }

                objSales.InvoiceRFPHID = ddlRFPNo.SelectedValue;
                objSales.InVoiceType = ddlinvoicetype.SelectedValue;
                ds = objSales.GetInvoiceDetailsByPOHID();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvInvoiceDetails.DataSource = ds.Tables[0];
                    gvInvoiceDetails.DataBind();
                }
                else
                {
                    gvInvoiceDetails.DataSource = "";
                    gvInvoiceDetails.DataBind();
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    gvColectionDetails.DataSource = ds.Tables[1];
                    gvColectionDetails.DataBind();
                }
                else
                {
                    gvColectionDetails.DataSource = "";
                    gvColectionDetails.DataBind();
                }

                lblPoTotalValue.Text = ds.Tables[2].Rows[0]["PoTotalValue"].ToString();

                BindInvoiceNo();

            }
            else
            {
                divOutput.Visible = false;
                divInput.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlCustomerName_SelectIndexChanged(object sender, EventArgs e)
    {
        divOutput.Visible = false;
        divInput.Visible = false;
        DataTable dt;
        ddlinvoicetype.SelectedIndex = 0;
        try
        {
            dt = (DataTable)ViewState["RFPDetails"];
            if (ddlCustomerName.SelectedIndex > 0)
            {
                string ProspectID = ddlCustomerName.SelectedValue;
                dt.DefaultView.RowFilter = "ProspectID='" + ProspectID + "'";
                dt.DefaultView.ToTable();
            }

            ddlRFPNo.DataSource = dt;
            ddlRFPNo.DataTextField = "RFPNo";
            ddlRFPNo.DataValueField = "RFPHID";
            ddlRFPNo.DataBind();
            ddlRFPNo.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnPayment_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objSales = new cSales();
        try
        {
            objSales.IDID = hdnIDID.Value;
            objSales.InvoiceRFPHID = ddlRFPNo.SelectedValue;
            objSales.InvoiceNo = txtInvoiceNo.Text;
            // objSales.InvoiceAmount = txtamount.Text;
            objSales.BasicValue = txtbasicvalue.Text;
            objSales.InvoiceValue = txtinvoicevalue.Text;
            objSales.Remarks = txtRemarks.Text;
            objSales.InvoiceDate = DateTime.ParseExact(txtinvoicedate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objSales.LocationID = ddlLocation.SelectedValue;
            objSales.CompanyID = Convert.ToInt32(objSession.CompanyID);

            string AttachmentName = "";
            string FileName = "";
            string[] extension;

            if (fbinvoiceaatch.HasFile)
            {
                objc = new cCommon();
                string extn = Path.GetExtension(fbinvoiceaatch.PostedFile.FileName).ToUpper();
                FileName = Path.GetFileName(fbinvoiceaatch.PostedFile.FileName);

                extension = FileName.Split('.');
                AttachmentName = "InvoiceAttach" + "_" + extension[0] + '.' + extension[1];
                objSales.AttachementName = AttachmentName;
            }
            else
            {
                AttachmentName = "";
                objSales.AttachementName = "";
            }

            if (!Directory.Exists(InvoiceDocsSavePath))
                Directory.CreateDirectory(InvoiceDocsSavePath);

            if (AttachmentName != "")
                fbinvoiceaatch.SaveAs(InvoiceDocsSavePath + AttachmentName);

            objSales.CreatedBy = objSession.employeeid;
            objSales.InVoiceType = ddlinvoicetype.SelectedValue;

            objSales.PaymentRemarks = txtpaymentremarks.Text;
            objSales.IPDID = ddlpaymentdays.SelectedValue;

            ds = objSales.SaveInvoicePaymentDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Invoice Details Saved Succesfully');hideInvoicePopUp();", true);
                hdnIDID.Value = "0";
            }
            else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Invoice Details Updated Succesfully');hideInvoicePopUp();", true);
                hdnIDID.Value = "0";
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
            ddlinvoicetype_OnSelectIndexChanged(null, null);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnCollection_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objSales = new cSales();
        try
        {
            objSales.InvoiceRFPHID = ddlRFPNo.SelectedValue;
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

            ds = objSales.SavePaymentCollectionDetails(rblPaymenttype.SelectedValue);

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Collection Details Saved Succesfully');hideCollectionPopup();", true);
            ddlinvoicetype_OnSelectIndexChanged(null, null);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnAddinvoiceNo_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objSales = new cSales();
        try
        {
            objSales.IDID = ddlAddInvoiceNo.SelectedValue;
            objSales.PCDID = hdnPCDID.Value;
            ds = objSales.UpdateInvoiceNoDetailsByPCDID();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Invoiice No Added Succesfully');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Dropown Events"

    protected void ddlInvoiceNo_OnSelectIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objSales = new cSales();
        try
        {
            if (ddlInvoiceNo.SelectedValue != "AD" && ddlInvoiceNo.SelectedValue != "0")
            {
                objSales.InvoiceRFPHID = ddlRFPNo.SelectedValue;
                objSales.IDID = ddlInvoiceNo.SelectedValue;
                ds = objSales.GetInvoiceAmountDetailsbyInvoiceID();
                //BalanceAmt PaidAmt

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

    #endregion

    #region"Button Events"

    protected void btninvoicecancel_Click(object sender, EventArgs e)
    {
        hdnIDID.Value = "0";
    }

    #endregion

    #region"Gridview Events"

    protected void gvColectionDetails_OnrowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName.ToString() == "AddInvoiceNo")
            {
                string PCDID = gvColectionDetails.DataKeys[index].Values[0].ToString();

                hdnPCDID.Value = PCDID;

                objSales = new cSales();
                DataSet ds = new DataSet();
                objSales.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);

                ds = objSales.GetInvoiceNoListDetailsByRFPHID(ddlAddInvoiceNo);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "addInvoiceNo();", true);
            }
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
                ddlinvoicetype_OnSelectIndexChanged(null, null);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvInvoiceDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName.ToString() == "DeleteInvoice")
            {
                objSales = new cSales();
                DataSet ds = new DataSet();
                objSales.IDID = gvInvoiceDetails.DataKeys[index].Values[0].ToString();

                ds = objSales.DeleteInvoiceDetailsByIDID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Invoice Details Deleted');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
                ddlinvoicetype_OnSelectIndexChanged(null, null);
            }
            if (e.CommandName.ToString() == "EditPaymentStatus")
            {
                objSales = new cSales();
                DataSet ds = new DataSet();

                objSales.IDID = gvInvoiceDetails.DataKeys[index].Values[0].ToString();
                hdnIDID.Value = gvInvoiceDetails.DataKeys[index].Values[0].ToString();
                ds = objSales.GetInvoiceDetailsByIDID();
                //InvoiceRemarks,BasicValue,InvoiceValue,LocationID,IPDID,PaymentRemarks
                txtInvoiceNo.Text = ds.Tables[0].Rows[0]["InvoiceNo"].ToString();
                txtinvoicedate.Text = ds.Tables[0].Rows[0]["InvoiceDate"].ToString();
                txtRemarks.Text = ds.Tables[0].Rows[0]["InvoiceRemarks"].ToString();
                ddlLocation.SelectedValue = ds.Tables[0].Rows[0]["LocationID"].ToString();
                ddlpaymentdays.SelectedValue = ds.Tables[0].Rows[0]["IPDID"].ToString();
                txtpaymentremarks.Text = ds.Tables[0].Rows[0]["PaymentRemarks"].ToString();
                txtbasicvalue.Text = ds.Tables[0].Rows[0]["BasicValue"].ToString();
                txtinvoicevalue.Text = ds.Tables[0].Rows[0]["InvoiceValue"].ToString();
                txtRemarks.Text = txtRemarks.Text;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "AddInvoicePopUp();", true);
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
                LinkButton btnaddinvoiceno = (LinkButton)e.Row.FindControl("btnaddinvoiceno");
                if (dr["InvoiceNo"].ToString() == "Advance")
                    btnaddinvoiceno.Visible = true;
                else
                    btnaddinvoiceno.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Commmon Methods"

    private void BindInvoiceNo()
    {
        DataSet ds = new DataSet();
        objSales = new cSales();
        try
        {
            objSales.InvoiceRFPHID = ddlRFPNo.SelectedValue;
            objSales.InVoiceType = ddlinvoicetype.SelectedValue;
            objSales.BindInvoiceNo(ddlInvoiceNo);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}