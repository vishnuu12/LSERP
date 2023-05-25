using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using eplus.core;

public partial class Pages_InvoiceApproval : System.Web.UI.Page
{

    #region"Declaration"

    cSales objSales;

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                bindInVoiceApprovalDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvInvoiceApproval_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "SaveInvoice")
            {
                objSales = new cSales();
                DataSet ds = new DataSet();
                RadioButtonList rblApprove = (RadioButtonList)gvInvoiceApproval.Rows[index].FindControl("rblApprove");
                TextBox txtRemarks = (TextBox)gvInvoiceApproval.Rows[index].FindControl("txtRemarks");

                objSales.InVoiceType = gvInvoiceApproval.DataKeys[index].Values[0].ToString();
                objSales.InVoiceID = Convert.ToInt32(gvInvoiceApproval.DataKeys[index].Values[1].ToString());
                objSales.ApprovalStatus = rblApprove.SelectedValue;
                objSales.InVoiceRemarks = txtRemarks.Text;

                ds = objSales.UpdateInvoiceApproval();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Invoice Status Updated successfully');", true);
                    bindInVoiceApprovalDetails();
                }
            }
            if (e.CommandName == "PDF")
            {
                string InVoiceFileName = gvInvoiceApproval.DataKeys[index].Values[2].ToString();
                InVoiceFileName = InVoiceFileName.Replace("/", "");

                string pdffile = InVoiceFileName + ".pdf";
                string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
                string pdfFileURL = LetterPath + pdffile;

                cCommon.DownLoad(pdffile, pdfFileURL);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion


    #region"Common Methods"

    private void bindInVoiceApprovalDetails()
    {
        DataSet ds = new DataSet();
        objSales = new cSales();
        try
        {
            ds = objSales.getInVoiceApprovalDetails();
            if (ds.Tables[0].Rows.Count > 0)
                gvInvoiceApproval.DataSource = ds.Tables[0];
            else
                gvInvoiceApproval.DataSource = "";
            gvInvoiceApproval.DataBind();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}