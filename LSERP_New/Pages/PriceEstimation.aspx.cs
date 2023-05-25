using eplus.core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_PriceEstimation : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cSales objSales;

    string PriceEstimationSavepath = ConfigurationManager.AppSettings["PriceEstimationSavepath"].ToString();
    string PriceEstimationHttppath = ConfigurationManager.AppSettings["PriceEstimationHttppath"].ToString();

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
                BindCustomername();
                BindEnquiryNoDetails();
                BindPriceEstimationDetails();
            }
            if (target == "ViewPriceAttach")
                viewWorkOrderDrawingFile(arg.ToString());
            if (target == "deletegvrow")
            {
                try
                {
                    objSales = new cSales();
                    string EPPEID = arg.ToString();
                    DataSet ds = new DataSet();
                    objSales.EPPEID = EPPEID;

                    ds = objSales.DeletePriceEstimationDetailsByEPPEID();

                    if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLoader", "SuccessMessage('Success','Price Estimation Deleted Succesfully');", true);
                        BindPriceEstimationDetails();
                    }
                }
                catch (Exception ex)
                {
                    Log.Message(ex.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Radio Events"

    protected void rblestimationtype_OnSelectedChanged(object sender, EventArgs e)
    {
        try
        {
            if (rblestimationtype.SelectedValue == "G")
            {
                ShowHideControls("addnew,input");
            }
            else
                ShowHideControls("addnew,add,input");
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
        objSales = new cSales();
        DataView dv;
        DataTable dcustomr = new DataTable();
        DataTable denquiry = new DataTable();
        try
        {
            dcustomr = (DataTable)ViewState["CustomerDetails"];
            denquiry = (DataTable)ViewState["EnquiryDetails"];

            if (ddlCustomerName.SelectedIndex > 0)
            {
                dv = new DataView(denquiry);
                dv.RowFilter = "ProspectID='" + ddlCustomerName.SelectedValue + "'";
                dcustomr = dv.ToTable();

                ddlEnquiryNumber.DataSource = dcustomr;
                ddlEnquiryNumber.DataTextField = "CustomerEnquiryNumber";
                ddlEnquiryNumber.DataValueField = "EnquiryID";
                ddlEnquiryNumber.DataBind();
            }
            else
            {
                ddlEnquiryNumber.DataSource = denquiry;
                ddlEnquiryNumber.DataTextField = "CustomerEnquiryNumber";
                ddlEnquiryNumber.DataValueField = "EnquiryID";
                ddlEnquiryNumber.DataBind();
            }

            ddlEnquiryNumber.Items.Insert(0, new ListItem("--Select--", "0"));

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
        DataTable dtEnquiry;
        try
        {
            if (ddlEnquiryNumber.SelectedIndex > 0)
            {
                ViewState["EnquiryNumber"] = ddlEnquiryNumber.SelectedValue;

                dtEnquiry = (DataTable)ViewState["EnquiryDetails"];

                if (ddlEnquiryNumber.SelectedIndex > 0)
                {
                    DataView dv = new DataView(dtEnquiry);
                    dv.RowFilter = "EnquiryID='" + ddlEnquiryNumber.SelectedValue + "'";
                    dtEnquiry = dv.ToTable();
                    ddlCustomerName.SelectedValue = dtEnquiry.Rows[0]["ProspectID"].ToString();
                }
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Hide Loader", "hideLoader();", true);
    }

    #endregion

    #region"Button Events"
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        objSales = new cSales();
        DataSet ds = new DataSet();
        try
        {
            objSales.EPPEID = hdnEPPEID.Value;
            objSales.EnquiryID = Convert.ToInt32(ddlEnquiryNumber.SelectedValue);
            objSales.Description = txtdescription.Text;
            objSales.CreatedBy = objSession.employeeid;

            string AttachmentName = "";
            string[] extension;

            if (fAttachment.HasFile)
            {
                string extn = Path.GetExtension(fAttachment.PostedFile.FileName).ToUpper();
                string Attchname = "";
                AttachmentName = Path.GetFileName(fAttachment.PostedFile.FileName);
                extension = AttachmentName.Split('.');

                Attchname = Regex.Replace(extension[0].ToString(), @"[^0-9a-zA-Z]+", "");
                AttachmentName = Attchname + "DRA" + '.' + extension[extension.Length - 1];
            }

            objSales.AttachementName = AttachmentName;

            ds = objSales.SaveEnquiryPurchasePriceEstimation();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLoader", "SuccessMessage('Success','Price Estimation Saved Succesfully');", true);
            }
            else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLoader", "SuccessMessage('Success','Price Estimation Updated Succesfully');", true);

            string StrStaffDocumentPath = ConfigurationManager.AppSettings["PriceEstimationSavepath"].ToString() + ddlEnquiryNumber.SelectedValue + "\\";

            if (!Directory.Exists(StrStaffDocumentPath))
                Directory.CreateDirectory(StrStaffDocumentPath);
            if (AttachmentName != "")
                fAttachment.SaveAs(StrStaffDocumentPath + AttachmentName);

            BindPriceEstimationDetails();
            hdnEPPEID.Value = "0";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        ds.Dispose();
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        hdnEPPEID.Value = "0";
        txtdescription.Text = "";
    }

    #endregion

    #region"GridView Events"

    protected void gvPriceestimationDetails_OnrowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName == "EditPriceEstimation")
            {
                hdnEPPEID.Value = gvPriceestimationDetails.DataKeys[index].Values[0].ToString();
                EditPriceEstimationDetailsByEPPID();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void viewWorkOrderDrawingFile(string index)
    {
        cCommon objc = new cCommon();
        try
        {
            string FileName = gvPriceestimationDetails.DataKeys[Convert.ToInt32(index)].Values[1].ToString();
            string EnquiryNo = gvPriceestimationDetails.DataKeys[Convert.ToInt32(index)].Values[2].ToString();

            objc.ViewFileName(PriceEstimationSavepath, PriceEstimationHttppath, FileName, EnquiryNo, ifrm);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void EditPriceEstimationDetailsByEPPID()
    {
        DataSet ds = new DataSet();
        objSales = new cSales();
        try
        {
            objSales.EPPEID = hdnEPPEID.Value;
            ds = objSales.GetPriceEstimationDetailsbyEPPEID();
            // EnquiryNo,Description
            ddlEnquiryNumber.SelectedValue = ds.Tables[0].Rows[0]["EnquiryNo"].ToString();
            txtdescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindCustomername()
    {
        DataSet ds = new DataSet();
        objSales = new cSales();
        try
        {
            ds = objSales.GetCustomerNameDetails(ddlCustomerName);
            ViewState["CustomerDetails"] = ds.Tables[0];
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindEnquiryNoDetails()
    {
        DataSet ds = new DataSet();
        objSales = new cSales();
        try
        {
            ds = objSales.GetEnquiryNoDetails(ddlEnquiryNumber);
            ViewState["EnquiryDetails"] = ds.Tables[0];
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindPriceEstimationDetails()
    {
        DataSet ds = new DataSet();
        objSales = new cSales();
        try
        {
            ds = objSales.GetPriceEstimationDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvPriceestimationDetails.DataSource = ds.Tables[0];
                gvPriceestimationDetails.DataBind();
            }
            else
            {
                gvPriceestimationDetails.DataSource = "";
                gvPriceestimationDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ShowHideControls(string divids)
    {
        divAdd.Visible = divInput.Visible = divOutput.Visible = divaddnew.Visible = false;
        try
        {
            string[] mode = divids.Split(',');
            for (int i = 0; i < mode.Length; i++)
            {
                switch (mode[i].ToLower())
                {
                    case "addnew":
                        divaddnew.Visible = true;
                        break;
                    case "add":
                        divAdd.Visible = true;
                        break;
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