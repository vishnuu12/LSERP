using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;
using System.Configuration;

public partial class Pages_InvoiceDispatch : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cCommon objcommon;
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
                objc = new cCommon();
                DataSet dsPOHID = new DataSet();
                DataSet dsCustomer = new DataSet();
                DataSet dsLocation = new DataSet();

                dsCustomer = objc.getCustomerNameByUserID(Convert.ToInt32(objSession.employeeid), ddlCustomerName);
                dsPOHID = objc.GetCustomerPODetailsByUserID(Convert.ToInt32(objSession.employeeid), ddlCustomerPO);

                ViewState["Customer"] = dsCustomer.Tables[0];
                ViewState["PO"] = dsPOHID.Tables[0];

                ShowHideControls("input");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlCustomerPO_SelectIndexChanged(object sender, EventArgs e)
    {
        cDesign objDesign = new cDesign();
        try
        {
            if (ddlCustomerPO.SelectedIndex > 0)
            {
                objDesign.POHID = Convert.ToInt32(ddlCustomerPO.SelectedValue);
                string ProspectID = objDesign.GetProspectNameByPOHID();
                ddlCustomerName.SelectedValue = ProspectID;

                bindinvoiceApprovedDetails();
                ShowHideControls("input,view");
            }
            else
            {
                ddlCustomerName.SelectedIndex = 0;
                ShowHideControls("input");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlCustomerName_OnSelectIndexChanged(object sender, EventArgs e)
    {
        DataTable dt;
        try
        {
            dt = (DataTable)ViewState["PO"];
            if (ddlCustomerName.SelectedIndex > 0)
            {
                string ProspectID = ddlCustomerName.SelectedValue;
                dt.DefaultView.RowFilter = "ProspectID='" + ProspectID + "'";
                dt.DefaultView.ToTable();
            }

            ddlCustomerPO.DataSource = dt;
            ddlCustomerPO.DataTextField = "PORefNo";
            ddlCustomerPO.DataValueField = "POHID";
            ddlCustomerPO.DataBind();
            ddlCustomerPO.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnInvoiceDispatch_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        objSales = new cSales();
        try
        {
            DataRow dr;
            dt.Columns.Add("InvoiceID");
            dt.Columns.Add("InvoiceType");

            foreach (GridViewRow row in gvInvoiceDetails.Rows)
            {
                CheckBox chkitems = (CheckBox)row.FindControl("chkitems");
                if (chkitems.Checked)
                {
                    dr = dt.NewRow();
                    dr["InvoiceID"] = gvInvoiceDetails.DataKeys[row.RowIndex].Values[1].ToString();
                    dr["InvoiceType"] = gvInvoiceDetails.DataKeys[row.RowIndex].Values[0].ToString();
                    dt.Rows.Add(dr);
                }
            }
            objSales.dt = dt;
            ds = objSales.UpdateInvoiceDispatchedDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Invoice Dispatched successfully');", true);
                bindinvoiceApprovedDetails();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Errror','Error Occcured');", true);
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvInvoiceDetails_OnRowCommand(object sender, CommandEventArgs e)
    {
        DataSet ds = new DataSet();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName == "ViewPDF")
            {
                string InVoiceFileName = gvInvoiceDetails.DataKeys[index].Values[2].ToString();
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

    private void bindinvoiceApprovedDetails()
    {
        DataSet ds = new DataSet();
        objSales = new cSales();
        try
        {
            objSales.POHID = ddlCustomerPO.SelectedValue;
            ds = objSales.GetInvoiceApprovedDetailsForMakeDispatchByPOHID();

            if (ds.Tables[0].Rows.Count > 0)
                gvInvoiceDetails.DataSource = ds.Tables[0];
            else
                gvInvoiceDetails.DataSource = "";
            gvInvoiceDetails.DataBind();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }


    private void ShowHideControls(string divids)
    {
        divInput.Visible = divOutput.Visible = false;
        string[] mode = divids.Split(',');
        try
        {
            for (int i = 0; i < mode.Length; i++)
            {
                switch (mode[i].ToLower())
                {
                    case "view":
                        divOutput.Visible = true;
                        break;
                    case "input":
                        divInput.Visible = true;
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
}