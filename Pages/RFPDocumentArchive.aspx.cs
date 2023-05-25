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

public partial class Pages_RFPDocumentArchive : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cSales objSales;
    cCommonMaster objCommonmaster;
    cCommon objc;

    string PriceEstimationSavepath = ConfigurationManager.AppSettings["RFPDocumentArchiveSavePath"].ToString();
    string PriceEstimationHttppath = ConfigurationManager.AppSettings["RFPDocumentArchiveHTTPPath"].ToString();

    #endregion

    #region "PageInit Events"

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
                BindRFPNoAndOfferNoDetails();
                BindRFPDocumentArchiveDetails();
            }
            if (target == "ViewRFPDocs")
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
                        BindRFPDocumentArchiveDetails();
                    }
                }
                catch (Exception ex)
                {
                    Log.Message(ex.ToString());
                }
            }
            if (target == "deleteRFPDocumentArchive")
            {
                try
                {
                    objSales = new cSales();
                    string RFPDAID = arg.ToString();
                    DataSet ds = new DataSet();
                    objSales.RFPDAID = RFPDAID;

                    ds = objSales.DeleteRFPDocumentArchiveDetailsByRFPDAID();

                    if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLoader", "SuccessMessage('Success','Deleted Succesfully');", true);
                        BindRFPDocumentArchiveDetails();
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
                objc = new cCommon();
                dv = new DataView(denquiry);
                dv.RowFilter = "ProspectID='" + ddlCustomerName.SelectedValue + "'";
                dcustomr = dv.ToTable();

                ddlEnquiryNumber.DataSource = dcustomr;
                ddlEnquiryNumber.DataTextField = "CustomerEnquiryNumber";
                ddlEnquiryNumber.DataValueField = "EnquiryID";
                ddlEnquiryNumber.DataBind();

                ddlEnquiryNumber.Items.Insert(0, new ListItem("--Select--", "0"));

                objc.EmptyDropDownList(ddlOfferNo);
                objc.EmptyDropDownList(ddlRFPNo);
            }
            else
            {
                ddlEnquiryNumber.DataSource = denquiry;
                ddlEnquiryNumber.DataTextField = "CustomerEnquiryNumber";
                ddlEnquiryNumber.DataValueField = "EnquiryID";
                ddlEnquiryNumber.DataBind();

                BindRFPNoAndOfferNoDetails();

                ddlEnquiryNumber.Items.Insert(0, new ListItem("--Select--", "0"));

                ddlEnquiryNumber.SelectedIndex = 0;
            }

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
                BindRFPNoAndOfferNoDetails();

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

    protected void ddlRFPNo_SelectIndexChanged(object sender, EventArgs e)
    {
        objCommonmaster = new cCommonMaster();
        DataSet ds = new DataSet();
        try
        {
            objCommonmaster.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objCommonmaster.GetEnquiryNoAndOfferNoByRFPHID();

            ddlOfferNo.SelectedValue = ds.Tables[0].Rows[0]["EODID"].ToString();
            ddlEnquiryNumber.SelectedValue = ds.Tables[0].Rows[0]["EnquiryNo"].ToString();
            ddlCustomerName.SelectedValue = ds.Tables[0].Rows[0]["ProspectID"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlOfferNo_SelectIndexChanged(object sender, EventArgs e)
    {
        objCommonmaster = new cCommonMaster();
        DataSet ds = new DataSet();
        try
        {
            objCommonmaster.EODID = ddlOfferNo.SelectedValue;
            ds = objCommonmaster.GetEnquiryNoAndCustomerNameByEODID();

            ddlEnquiryNumber.SelectedValue = ds.Tables[0].Rows[0]["EnquiryNumber"].ToString();
            ddlCustomerName.SelectedValue = ds.Tables[0].Rows[0]["ProspectID"].ToString();

            ddlRFPNo.DataSource = ds.Tables[1];
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
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        objSales = new cSales();
        DataSet ds = new DataSet();
        try
        {
            objSales.EnquiryID = Convert.ToInt32(ddlEnquiryNumber.SelectedValue);
            objSales.EODID = Convert.ToInt32(ddlOfferNo.SelectedValue);
            objSales.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            objSales.DepartmentID = Convert.ToInt32(ddldepartment.SelectedValue);
            objSales.Remarks = txtremarks.Text;
            objSales.FileName = txtfilename.Text;
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
                AttachmentName = Attchname + "RFPDocArchive" + '.' + extension[extension.Length - 1];
            }

            objSales.AttachementName = AttachmentName;

            ds = objSales.SaveRFPDocumentArchiveDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLoader", "SuccessMessage('Success','RFP Document Archive Saved Succesfully');", true);
            }
            else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLoader", "SuccessMessage('Success','RFP Document Archive Updated Succesfully');", true);

            string StrStaffDocumentPath = PriceEstimationSavepath + ddlEnquiryNumber.SelectedValue + "\\";

            if (!Directory.Exists(StrStaffDocumentPath))
                Directory.CreateDirectory(StrStaffDocumentPath);
            if (AttachmentName != "")
                fAttachment.SaveAs(StrStaffDocumentPath + AttachmentName);

            BindRFPDocumentArchiveDetails();
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
        // txtdescription.Text = "";
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
                hdnEPPEID.Value = gvRFPDocumentArchiveDetails.DataKeys[index].Values[0].ToString();
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
            string FileName = gvRFPDocumentArchiveDetails.DataKeys[Convert.ToInt32(index)].Values[0].ToString();
            string EnquiryNo = gvRFPDocumentArchiveDetails.DataKeys[Convert.ToInt32(index)].Values[1].ToString();

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
            // txtdescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
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

    private void BindRFPNoAndOfferNoDetails()
    {
        DataSet ds = new DataSet();
        objCommonmaster = new cCommonMaster();
        try
        {
            objCommonmaster.EnquiryID = Convert.ToInt32(ddlEnquiryNumber.SelectedValue);
            ds = objCommonmaster.GetRFPNoDetailsAndOfferNoDetailsByRFPDocumentArchive(ddlRFPNo, ddlOfferNo, ddldepartment);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindRFPDocumentArchiveDetails()
    {
        DataSet ds = new DataSet();
        objSales = new cSales();
        try
        {
            ds = objSales.GetRFPDocumentArchiveDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvRFPDocumentArchiveDetails.DataSource = ds.Tables[0];
                gvRFPDocumentArchiveDetails.DataBind();
            }
            else
            {
                gvRFPDocumentArchiveDetails.DataSource = "";
                gvRFPDocumentArchiveDetails.DataBind();
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

    #region "PageLoadComplete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvRFPDocumentArchiveDetails.Rows.Count > 0)
            gvRFPDocumentArchiveDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}