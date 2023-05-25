using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Text;

public partial class Pages_RFPHeader : System.Web.UI.Page
{

    #region"Declaration"

    cSession objSession = new cSession();
    cCommon objc = new cCommon();
    cDesign objDesign;
    cSales objSales;
    string CusstomerEnquirySavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString();
    string CustomerEnquiryHttpPath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString();

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
                dsCustomer = objc.getCustomerNameByUserID(Convert.ToInt32(objSession.employeeid), ddlCustomerName, "LS_GetCustomerNameByEmployeeID");
                dsPOHID = objc.GetCustomerPODetailsByUserID(Convert.ToInt32(objSession.employeeid), ddlCustomerPO, "LS_GetPOHIDByUserID");

                ViewState["CustomerList"] = dsCustomer.Tables[0];
                ViewState["PONumber"] = dsPOHID.Tables[0];

                objc.GetLocationDetails(ddlLocation);
                ShowHideControls("add");
                txtitemqty.Text = "";
                lblitemqty.InnerText = "";
            }
            else
            {
                if (target == "deletegvrow")
                {
                    objSales = new cSales();
                    objSales.AttachementID = Convert.ToInt32(arg);
                    string Message = objSales.DeleteAttachement();

                    if (Message == "Deleted")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Attachements Deleted successfully');ShowViewPopUp('Documents');", true);

                    BindAttachements(ViewState["RFPHID"].ToString());
                }
                if (target == "UpdateRFPStatus")
                    UpdateRFPStatus();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region "DropDown Events"

    protected void ddlitemname_SelectIndexChanged(object sender, EventArgs e)
    {
        DataTable dt;
        try
        {
            if (ddlitemname.SelectedIndex > 0)
            {
                dt = (DataTable)ViewState["POItemQty"];
                if (dt.Rows.Count > 0)
                {
                    dt.DefaultView.RowFilter = "PODID='" + ddlitemname.SelectedValue + "'";
                    txtitemqty.Text = dt.DefaultView.ToTable().Rows[0]["Quantity"].ToString();

                    lblitemqty.InnerText = "(" + txtitemqty.Text + ")";
                }
                else
                {
                    txtitemqty.Text = "";
                    lblitemqty.InnerText = "";
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUP", "OpenTab('Item');", true);
            }
            else
            {
                txtitemqty.Text = "";
                lblitemqty.InnerText = "";
            }
            if (txtitemqty.Text != "")
                hdnPoqty.Value = txtitemqty.Text;
            else
                hdnPoqty.Value = "0";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    protected void ddlCustomerPO_SelectIndexChanged(object sender, EventArgs e)
    {
        objDesign = new cDesign();
        try
        {
            if (ddlCustomerPO.SelectedIndex > 0)
            {
                btnAddNew.Visible = true;
                objDesign.POHID = Convert.ToInt32(ddlCustomerPO.SelectedValue);
                string ProspectID = objDesign.GetProspectNameByPOHID();
                ddlCustomerName.SelectedValue = ProspectID;
                BindRFPDetails();
                ShowHideControls("view");
            }
            else
            {
                ShowHideControls("add");
                ddlCustomerName.SelectedIndex = 0;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlCustomerName_SelectIndexChanged(object sender, EventArgs e)
    {
        objDesign = new cDesign();
        try
        {
            DataTable dt;
            dt = (DataTable)ViewState["PONumber"];
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

            ShowHideControls("add");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btndownloaddocs_Click(object sender, EventArgs e)
    {
        try
        {
            cCommon.DownLoad(ViewState["FileName"].ToString(), ViewState["ifrmsrc"].ToString());
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        objDesign = new cDesign();
        DataSet ds = new DataSet();
        try
        {
            objDesign.POHID = Convert.ToInt32(ddlCustomerPO.SelectedValue);
            ds = objDesign.GetMaxRFPNoINRFPHeader();
            lblRFPNo.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
            txtProjectName.Text = ds.Tables[1].Rows[0]["ProjectName"].ToString();
            ShowHideControls("input");
            hdnRFPHID.Value = "0";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objDesign = new cDesign();
        objc = new cCommon();
        try
        {
            if (objc.Validate(divInput))
            {
                objDesign.RFPHID = Convert.ToInt32(hdnRFPHID.Value);
                objDesign.POHID = Convert.ToInt32(ddlCustomerPO.SelectedValue);
                objDesign.RFPNo = lblRFPNo.Text;
                objDesign.LocationID = Convert.ToInt32(ddlLocation.SelectedValue);
                objDesign.ProjectName = txtProjectName.Text;
                objDesign.QAPRefNo = txtQAPRefNo.Text;
                objDesign.QAPApproval = Convert.ToInt32(ddlQAPAproval.SelectedValue);
                objDesign.DrawingApproval = Convert.ToInt32(ddlDrawingApproval.SelectedValue);
                if (txtDeliveryDate.Text != "")
                {
                    objDesign.Date = true;
                    objDesign.DeliveryDate = DateTime.ParseExact(txtDeliveryDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                else
                    objDesign.Date = false;
                objDesign.InspectionRequirtment = Convert.ToInt32(ddlInspectionRequirtment.SelectedValue);
                objDesign.LDClause = Convert.ToInt32(ddlLDClause.SelectedValue);
                objDesign.DespatchDetails = txtDespatchDetails.Text;
                objDesign.NotesSummary = txtNotesSummary.Text;

                ds = objDesign.SaveRFPHeaderDetails();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','RFP Header Saved Successfully');", true);
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','RFP Header Updated Successfully');", true);

                ShowHideControls("view");
                BindRFPDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSaveAttchement_Click(object sender, EventArgs e)
    {
        objSales = new cSales();
        objc = new cCommon();
        DataSet ds = new DataSet();
        try
        {

            objSales.EnquiryID = Convert.ToInt32(ViewState["EnquiryID"]);
            objSales.AttachementTypeName = Convert.ToInt32(ddlTypeName.SelectedValue);
            objSales.Description = txtDescription.Text;
            //   objSales.AttachementID = Convert.ToInt32(hdnAttachementID.Value);
            objSales.RFPHID = Convert.ToInt32(ViewState["RFPHID"]);

            string MaxAttachementId = objSales.GetMaximumAttachementID();

            string AttachmentName = "";

            if (fAttachment.HasFile)
            {
                string extn = Path.GetExtension(fAttachment.PostedFile.FileName).ToUpper();
                AttachmentName = Path.GetFileName(fAttachment.PostedFile.FileName);
            }

            string[] extension = AttachmentName.Split('.');

            AttachmentName = "RFP" + '_' + MaxAttachementId + '.' + extension[1];

            objSales.AttachementName = AttachmentName;

            objSales.CreatedBy = objSession.employeeid;

            ds = objSales.SaveRFPAttachements();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Attachement Details Saved successfully');OpenTab('Documents');", true);


            string StrStaffDocumentPath = CusstomerEnquirySavePath + ViewState["EnquiryID"].ToString() + "\\";

            if (!Directory.Exists(StrStaffDocumentPath))
                Directory.CreateDirectory(StrStaffDocumentPath);

            if (AttachmentName != "")
                fAttachment.SaveAs(StrStaffDocumentPath + AttachmentName);

            BindAttachements(ViewState["RFPHID"].ToString());
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Error occured');", true);
            Log.Message(ex.ToString());
        }

        finally
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUP", "ShowViewPopUp('Documents');", true);
            objSales = null;
            ds = null;
        }
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        try
        {
            hdnRFPHID.Value = "0";
            ShowHideControls("view");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSaveRFPHID_Click(object sender, EventArgs e)
    {
        objSales = new cSales();
        DataSet ds = new DataSet();
        try
        {
            objSales.RFPHID = Convert.ToInt32(ViewState["RFPHID"]);
            objSales.PODID = Convert.ToInt32(ddlitemname.SelectedValue);
            objSales.RFPQTY = Convert.ToInt32(txtitemqty.Text);
            if (ViewState["RFPDID"] == null)
            {
                objSales.InsertRFPItemdetails();
            }
            else
            {
                objSales.UpdateRFPItemdetails(ViewState["RFPDID"].ToString());
                ViewState["RFPDID"] = null;
            }
            BindRFPItemDetails(ViewState["RFPHID"].ToString());
            txtitemqty.Text = "";
            lblitemqty.InnerText = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "SuccessMessage('Success','Item Details Updated Successfully');HideRFPItemDetailsPopUp();", true);
            // BindRFPItemDetails();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ErrorMessage('Error','Error occured');OpenTab('Item');", true);
            Log.Message(ex.ToString());
        }
    }

    protected void btnShareRFPRequest_Click(object sender, EventArgs e)
    {
        objSales = new cSales();
        DataSet ds = new DataSet();
        try
        {
            objSales.POHID = ddlCustomerPO.SelectedValue;
            ds = objSales.ShareRFPStatusByPOHID();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','RFP Header Saved Successfully');", true);

            ShowHideControls("view");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvRFPItemdetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        objSales = new cSales();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string RFPDID = gvRFPItemdetails.DataKeys[index].Values[0].ToString();
            //if (e.CommandName.ToString() == "EditItem")
            //{
            //    ddlitemname.SelectedValue = podid;
            //    txtitemqty.Text = ((Label)gvRFPItemdetails.Rows[index].FindControl("lblqty")).Text.ToString();
            //}
            if (e.CommandName.ToString() == "DeleteItem")
            {
                objSales.DeleteRFPItemdetails(RFPDID);
                BindRFPItemDetails(ViewState["RFPHID"].ToString());
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "SuccessMessage('Success','Item Details Updated Successfully');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "SuccessMessage('Success','Item Deleted successfully');OpenTab('Item');", true);
            }

        }
        catch (Exception ex)
        {

        }
    }

    protected void gvRFPHeader_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dt;
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            string RFPHID = gvRFPHeader.DataKeys[index].Values[0].ToString();

            dt = (DataTable)ViewState["RFPHeaderDetails"];
            hdnRFPHID.Value = RFPHID.ToString();
            dt.DefaultView.RowFilter = "RFPHID='" + RFPHID + "'";

            int EnquiryNumber = Convert.ToInt32(gvRFPHeader.DataKeys[index].Values[2].ToString());
            ViewState["Enquirynumber"] = EnquiryNumber;
            string BaseHtttpPath = CustomerEnquiryHttpPath + EnquiryNumber + "/";

            ViewState["RFPNo"] = dt.DefaultView.ToTable().Rows[0]["RFPNo"].ToString();

            if (e.CommandName == "EditRFP")
            {
                lblRFPNo.Text = dt.DefaultView.ToTable().Rows[0]["RFPNo"].ToString();
                ddlLocation.SelectedValue = dt.DefaultView.ToTable().Rows[0]["LocationID"].ToString();
                txtProjectName.Text = dt.DefaultView.ToTable().Rows[0]["ProjectName"].ToString();
                txtQAPRefNo.Text = dt.DefaultView.ToTable().Rows[0]["QAPRefNo"].ToString();
                ddlDrawingApproval.SelectedValue = dt.DefaultView.ToTable().Rows[0]["DrawingApproval"].ToString();
                ddlQAPAproval.SelectedValue = dt.DefaultView.ToTable().Rows[0]["QAPApproval"].ToString();
                txtDeliveryDate.Text = dt.DefaultView.ToTable().Rows[0]["DeliveryDateWithTime"].ToString();
                ddlInspectionRequirtment.SelectedValue = dt.DefaultView.ToTable().Rows[0]["InspectionRequirtment"].ToString();
                ddlLDClause.SelectedValue = dt.DefaultView.ToTable().Rows[0]["LDClause"].ToString();
                txtDespatchDetails.Text = dt.DefaultView.ToTable().Rows[0]["DespatchDetails"].ToString();
                txtNotesSummary.Text = dt.DefaultView.ToTable().Rows[0]["NotesSummary"].ToString();

                ShowHideControls("input");
            }

            if (e.CommandName == "ViewRFP")
            {
                objSales = new cSales();

                ViewState["EnquiryID"] = dt.DefaultView.ToTable().Rows[0]["EnquiryNumber"].ToString();
                ViewState["RFPHID"] = dt.DefaultView.ToTable().Rows[0]["RFPHID"].ToString();
                ViewState["RFPStatus"] = dt.DefaultView.ToTable().Rows[0]["RFPStatus"].ToString();

                lblQAPApproval_V.Text = dt.DefaultView.ToTable().Rows[0]["QAPApprovalName"].ToString();
                lblQAPRefNo_V.Text = dt.DefaultView.ToTable().Rows[0]["QAPRefNo"].ToString();
                lblDrawingApprova_V.Text = dt.DefaultView.ToTable().Rows[0]["DrawingApprovalName"].ToString();
                lblInspectionRequirtment_V.Text = dt.DefaultView.ToTable().Rows[0]["InspectionRequirtmentName"].ToString();
                lblLDClause_V.Text = dt.DefaultView.ToTable().Rows[0]["LDClauseName"].ToString();
                lblDespatchDetails_V.Text = dt.DefaultView.ToTable().Rows[0]["DespatchDetails"].ToString();
                lblNotesSummary_V.Text = dt.DefaultView.ToTable().Rows[0]["NotesSummary"].ToString();

                BindAttachements(RFPHID);
                BindRFPItemDetails(RFPHID);

                objSales.GetSalesAttachementTypeName(ddlTypeName);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUP", "ShowViewPopUp('NA');", true);
            }

            if (e.CommandName == "ViewPOCopy")
            {
                objc = new cCommon();
                string FileName = gvRFPHeader.DataKeys[index].Values[3].ToString();
                ViewState["FileName"] = FileName;
                ifrm.Attributes.Add("src", BaseHtttpPath + FileName);

                objc.ViewFileName(CusstomerEnquirySavePath, CustomerEnquiryHttpPath, FileName, EnquiryNumber.ToString(), ifrm);

                //if (File.Exists(CusstomerEnquirySavePath + EnquiryNumber + "//" + FileName))
                //{
                //    ViewState["ifrmsrc"] = BaseHtttpPath + FileName;
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewdocsPopUp();", true);
                //}
                //else
                //{
                //    ifrm.Attributes.Add("src", "");
                //    ViewState["ifrmsrc"] = "";
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Po Copy Not Found');", true);
                //}
            }
            if (e.CommandName == "ViewPoCopyWithoutPrice")
            {
                string FileName = gvRFPHeader.DataKeys[index].Values[4].ToString();
                ViewState["FileName"] = FileName;
                ifrm.Attributes.Add("src", BaseHtttpPath + FileName);

                objc.ViewFileName(CusstomerEnquirySavePath, CustomerEnquiryHttpPath, FileName, EnquiryNumber.ToString(), ifrm);

                //if (File.Exists(CusstomerEnquirySavePath + EnquiryNumber + "//" + FileName))
                //{
                //    ViewState["ifrmsrc"] = BaseHtttpPath + FileName;
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewdocsPopUp();", true);
                //}
                //else
                //{
                //    ifrm.Attributes.Add("src", "");
                //    ViewState["ifrmsrc"] = "";
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Po copy Without Price Not Found');", true);
                //}
            }

            if (e.CommandName == "print")
            {
                objSales = new cSales();
                DataSet dsItem = new DataSet();
                dsItem = objSales.GetRFPItemdetails(RFPHID);

                DataSet ds = new DataSet();
                DataTable dtItemDetails = new DataTable();

                objSales = new cSales();
                objSales.RFPHID = Convert.ToInt32(RFPHID);
                ds = objSales.getBellowsDetailsByRFPHID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "RecordsFound" || dsItem.Tables[0].Rows.Count > 0)
                {
                    lblRFPNo_P.Text = dt.DefaultView.ToTable().Rows[0]["RFPNo"].ToString() + " & " + ds.Tables[1].Rows[0]["RFPCreateddate"].ToString();
                    //  lblcreateddate_p.Text = ds.Tables[1].Rows[0]["RFPCreateddate"].ToString();
                    lblCustomerName_p.Text = ddlCustomerName.SelectedItem.Text;
                    string[] PONumber = ddlCustomerPO.SelectedItem.Text.Split('/');
                    lblCustomerOrderNo_p.Text = ddlCustomerPO.SelectedItem.Text.Replace("/" + PONumber[PONumber.Length - 1], "");
                    //lblOrderDate_p.Text = ds.Tables[1].Rows[0]["CustomerOrderDate"].ToString();
                    lblProject_p.Text = dt.DefaultView.ToTable().Rows[0]["ProjectName"].ToString();
                    lblNumberOfItems_p.Text = dsItem.Tables[1].Rows[0]["TotalQuantity"].ToString();

                    if (dsItem.Tables[0].Rows.Count > 0)
                    {
                        dtItemDetails = new DataTable();
                        dtItemDetails = (DataTable)dsItem.Tables[0];

                        gvItemDetails_p.DataSource = dtItemDetails;
                        gvItemDetails_p.DataBind();

                        gvAnnexureItemList_p.DataSource = dtItemDetails;
                        gvAnnexureItemList_p.DataBind();

                        if (ds.Tables[0].Rows[0]["Message"].ToString() == "RecordsFound")
                        {
                            ds.Tables[0].Columns.Remove("Message");
                            dtItemDetails = new DataTable();
                            dtItemDetails = (DataTable)ds.Tables[0];

                            if (dtItemDetails.Columns.Contains("Gradename"))
                                dtItemDetails.Columns["Gradename"].ColumnName = "Grade Name";
                            dtItemDetails.Columns["SNo"].ColumnName = "SL.No";

                            gvBellowDetails_p.DataSource = dtItemDetails;
                            gvBellowDetails_p.DataBind();

                            gvAnnexureBellowDetails_p.DataSource = dtItemDetails;
                            gvAnnexureBellowDetails_p.DataBind();
                        }
                        else
                        {
                            gvBellowDetails_p.DataSource = "";
                            gvBellowDetails_p.DataBind();

                            gvAnnexureBellowDetails_p.DataSource = "";
                            gvAnnexureBellowDetails_p.DataBind();
                        }
                    }
                    else
                    {
                        gvItemDetails_p.DataSource = "";
                        gvItemDetails_p.DataBind();

                        gvBellowDetails_p.DataSource = "";
                        gvBellowDetails_p.DataBind();
                    }

                    lblQAPRefNo_p.Text = dt.DefaultView.ToTable().Rows[0]["QAPRefNo"].ToString();
                    lblQAPApproval_p.Text = dt.DefaultView.ToTable().Rows[0]["QAPApprovalName"].ToString();
                    lblDrawingApproval_p.Text = dt.DefaultView.ToTable().Rows[0]["DrawingApprovalName"].ToString();
                    lblInspectionRequirtment_p.Text = dt.DefaultView.ToTable().Rows[0]["InspectionRequirtmentName"].ToString();
                    lblLDClause_p.Text = dt.DefaultView.ToTable().Rows[0]["LDClauseName"].ToString();
                    lblDespatchDetails_p.Text = dt.DefaultView.ToTable().Rows[0]["DespatchDetails"].ToString();
                    lblMarketing_p.Text = ds.Tables[1].Rows[0]["SalesE"].ToString();
                    lblDesign_p.Text = ds.Tables[1].Rows[0]["DesignE"].ToString();
                    // lblDataEntry_p.Text = ds.Tables[1].Rows[0]["PurchaseE"].ToString();
                    lblProjectIncharge_p.Text = ds.Tables[1].Rows[0]["SalesE"].ToString();
                    lblApprovedBy_p.Text = ds.Tables[1].Rows[0]["RFPApprovedBy"].ToString();
                    lblDueDateForDispatch_p.Text = dt.DefaultView.ToTable().Rows[0]["DeliveryDate"].ToString();//ds.Tables[1].Rows[0][""].ToString();
                    lblNotesSummary_p.Text = dt.DefaultView.ToTable().Rows[0]["NotesSummary"].ToString();
                    lblLocationName_p.Text = dt.DefaultView.ToTable().Rows[0]["LocationName"].ToString();
                    GeneratePDF();

                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Add RFP item Details');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ErrorMessage('Error','Errror Occured');", true);
            Log.Message(ex.ToString());
        }
    }

    protected void gvAttachments_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        objSales = new cSales();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            int AttachementID = Convert.ToInt32(gvAttachments.DataKeys[index].Values[0].ToString());

            if (e.CommandName.ToString() == "ViewDocs")
            {
                string BasehttpPath = CustomerEnquiryHttpPath + ViewState["EnquiryID"].ToString() + "/";
                string FileName = ((Label)gvAttachments.Rows[index].FindControl("lblFileName_V")).Text.ToString();
                ViewState["FileName"] = FileName;
                ifrm.Attributes.Add("src", BasehttpPath + FileName);
                string imgname = CusstomerEnquirySavePath + ViewState["EnquiryID"].ToString() + "\\" + FileName;

                objc.ViewFileName(CusstomerEnquirySavePath, CustomerEnquiryHttpPath, FileName, ViewState["EnquiryID"].ToString(), ifrm);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowViewPopUp('NA');", true);

                //if (File.Exists(imgname))
                //{
                //    //ViewState["ifrmsrc"] = imgname;
                //    ViewState["ifrmsrc"] = BasehttpPath + FileName;
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewdocsPopUp();", true);
                //}
                //else
                //{
                //    ifrm.Attributes.Add("src", BasehttpPath + FileName);
                //    ViewState["ifrmsrc"] = "";
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Attach Not Found');", true);
                //}
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        finally
        {
            objSales = null;
        }
    }

    protected void gvRFPItemDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {

        //  DataRowView dr = (DataRowView)e.Row.DataItem;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnDeleteItemDetails = (LinkButton)e.Row.FindControl("lbtnDelete_itemdetails");

            if (ViewState["RFPStatus"].ToString() == "7" || ViewState["RFPStatus"].ToString() == "1")
            {
                btnDeleteItemDetails.Visible = false;
                btnSaveRFPHID.Visible = false;
                lbtnShareRFP.Visible = false;
            }
            else
            {
                btnDeleteItemDetails.Visible = true;
                btnSaveRFPHID.Visible = true;
                lbtnShareRFP.Visible = true;
            }
        }
    }

    protected void gvAttachments_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        //  DataRowView dr = (DataRowView)e.Row.DataItem;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnDelete = (LinkButton)e.Row.FindControl("lbtnDelete");

            if (ViewState["RFPStatus"].ToString() == "7" || ViewState["RFPStatus"].ToString() == "1")
            {
                lbtnDelete.Visible = false;
                //btnSaveAttachements.Visible = false;
            }
            else
            {
                lbtnDelete.Visible = true;
                //btnSaveAttachements.Visible = true;
            }
            if (objSession.type == 1)
                lbtnDelete.Visible = true;
        }
    }

    protected void gvRFPHeader_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        DataRowView dr = (DataRowView)e.Row.DataItem;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnEdit = (LinkButton)e.Row.FindControl("lbtnEdit");

            if (dr["RFPStatus"].ToString() == "7" || dr["RFPStatus"].ToString() == "1")
            {
                lbtnEdit.Visible = false;
                btnAddNew.Visible = false;
            }
            else
            {
                lbtnEdit.Visible = true;
                btnAddNew.Visible = true;
            }
        }
    }

    protected void gvDrawingFiles_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument.ToString());

        string EnquiryNumber = gvDrawingFiles.DataKeys[index].Values[1].ToString();
        string DrawingName = gvDrawingFiles.DataKeys[index].Values[0].ToString();

        string BasehttpPath = CustomerEnquiryHttpPath + EnquiryNumber + "/";
        string FileName = BasehttpPath + DrawingName;

        ViewState["FileName"] = FileName;
        ifrm.Attributes.Add("src", FileName);

        objc.ViewFileName(CusstomerEnquirySavePath, CustomerEnquiryHttpPath, DrawingName, EnquiryNumber, ifrm);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowViewPopUp('NA');", true);

        //if (File.Exists(CusstomerEnquirySavePath + EnquiryNumber + '/' + DrawingName))
        //{
        //    ViewState["ifrmsrc"] = FileName;
        ////    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowViewdocsPopUp();", true);
        //}
        //else
        //{
        //    ViewState["ifrmsrc"] = "";
        //    ifrm.Attributes.Add("src", FileName);
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Attach Not Found');", true);
        //}
    }

    protected void gvAnnexureBellowDetails_p_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[1].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[2].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[3].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[4].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[5].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[6].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[7].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[8].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[9].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[10].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[11].Attributes.Add("style", "text-align:center;");
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[1].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[2].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[3].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[4].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[5].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[6].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[7].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[8].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[9].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[10].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[11].Attributes.Add("style", "text-align:center;");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvBellowDetails_p_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[1].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[2].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[3].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[4].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[5].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[6].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[7].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[8].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[9].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[10].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[11].Attributes.Add("style", "text-align:center;");
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[1].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[2].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[3].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[4].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[5].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[6].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[7].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[8].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[9].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[10].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[11].Attributes.Add("style", "text-align:center;");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    public void BindAttachements(string RFPHID)
    {
        try
        {
            objSales = new cSales();
            DataSet dsGetAttachementsDetails = new DataSet();

            objSales.RFPHID = Convert.ToInt32(RFPHID);

            dsGetAttachementsDetails = objSales.GetAttachementDetailsByRFPHID();

            if (dsGetAttachementsDetails.Tables[0].Rows.Count > 0)
            {
                ViewState["Attachement"] = dsGetAttachementsDetails.Tables[0];

                gvAttachments.DataSource = dsGetAttachementsDetails.Tables[0];
                gvAttachments.DataBind();

                gvDrawingFiles.DataSource = dsGetAttachementsDetails.Tables[1];
                gvDrawingFiles.DataBind();
            }
            else
            {
                gvAttachments.DataSource = "";
                gvAttachments.DataBind();
            }
            if (dsGetAttachementsDetails.Tables[1].Rows.Count > 0)
            {
                gvDrawingFiles.DataSource = dsGetAttachementsDetails.Tables[1];
                gvDrawingFiles.DataBind();
            }
            else
            {
                gvDrawingFiles.DataSource = "";
                gvDrawingFiles.DataBind();
            }

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindRFPDetails()
    {
        DataSet ds = new DataSet();
        objDesign = new cDesign();
        try
        {
            objDesign.POHID = Convert.ToInt32(ddlCustomerPO.SelectedValue);
            ds = objDesign.GetRFPHeaderDetails();

            ViewState["RFPHeaderDetails"] = ds.Tables[0];

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvRFPHeader.DataSource = ds.Tables[0];
                gvRFPHeader.DataBind();
            }
            else
            {
                gvRFPHeader.DataSource = "";
                gvRFPHeader.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindRFPItemDetails(string RFPHID)
    {
        objSales = new cSales();
        DataSet ds = new DataSet();
        try
        {
            ddlitemnameload();
            objSales.POHID = ddlCustomerPO.SelectedValue;
            ds = objSales.GetRFPItemdetails(RFPHID);
            ViewState["CustomerPODetails"] = ds.Tables[0];

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvRFPItemdetails.DataSource = ds.Tables[0];
                gvRFPItemdetails.DataBind();
            }
            else
            {
                gvRFPItemdetails.DataSource = "";
                gvRFPItemdetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ddlitemnameload()
    {
        objSales = new cSales();
        DataSet ds = new DataSet();
        try
        {
            objSales.POHID = ddlCustomerPO.SelectedValue;
            ds = objSales.getPOItemnameforRFP();
            ViewState["POItemQty"] = ds.Tables[0];
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlitemname.DataSource = ds.Tables[0];
                ddlitemname.DataTextField = "ItemName";
                ddlitemname.DataValueField = "PODID";
                ddlitemname.DataBind();
                ddlitemname.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            ddlitemname.SelectedIndex = 0;

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ShowHideControls(string Mode)
    {
        divAdd.Visible = divInput.Visible = divOutput.Visible = false;
        try
        {
            switch (Mode.ToLower())
            {
                case "add":
                    divAdd.Visible = true;
                    break;
                case "view":
                    divAdd.Visible = true;
                    divOutput.Visible = true;
                    break;
                case "input":
                    divInput.Visible = true;
                    break;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void UpdateRFPStatus()
    {
        DataSet dsApprove = new DataSet();
        objDesign = new cDesign();
        bool Valid = true;
        try
        {

            if (gvRFPItemdetails.Rows.Count > 0)
            {
                if (gvAttachments.Rows.Count > 0)
                {
                    objDesign.RFPHID = Convert.ToInt32(hdnRFPHID.Value);
                    objDesign.StatusFlag = "Request";
                    objDesign.UserID = Convert.ToInt32(objSession.employeeid);
                    dsApprove = objDesign.UpdateRFPStatus("LS_UpdateRFPApprovalStatus");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','No Atttachements Added');", true);
                    Valid = false;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','No Item Added');", true);
                Valid = false;
            }
            if (Valid)
            {
                if (dsApprove.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "SuccessMessage('Success','RFP Requested Successfully');", true);
                    SaveAlertDetails();
                    ddlCustomerPO_SelectIndexChanged(null, null);
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error Occured');", true);
        }
    }

    public void GeneratePDF()
    {
        DataSet ds = new DataSet();
        objc = new cCommon();
        string Flag;
        try
        {
            var sbPurchaseOrder = new StringBuilder();

            var sbGvItemAnnexure = new StringBuilder();
            var sbBellowAnnexure = new StringBuilder();

            cQRcode objQr = new cQRcode();

            string QrNumber = objQr.generateQRNumber(9);
            string displayQrnumber = objQr.getDisplayQRNumber(QrNumber, 3, '-');

            string Code = displayQrnumber + "/" + objSession.employeeid + "/" + DateTime.Now.ToString();

            string QrCode = objQr.QRcodeGeneration(Code);
            if (QrCode != "")
            {
                //imgQrcode.ImageUrl = QrCode;
                objQr.QRNumber = displayQrnumber;
                objQr.fileName = "RFPRequest";
                objQr.createdBy = objSession.employeeid;
                string pageName = Path.GetFileName(Page.AppRelativeVirtualPath);
                objQr.Pagename = pageName;
                objQr.saveQRNumber();
            }

            // divPrint.Attributes.Add("style", "display:block;height:287mm;width:200mm;margin:15px;padding:15px;");
            //divPrint.RenderControl(new HtmlTextWriter(new StringWriter(sbPurchaseOrder)));

            //if (gvAnnexureItemList_p.Rows.Count > 0)
            //{
            //    Flag = "multi";
            //    divAnnexureItemList_p.Attributes.Add("style", "display:block;height:287mm;width:200mm;float:left");
            //    divAnnexureBellowList_p.Attributes.Add("style", "display:block;height:287mm;width:200mm;float:left");

            //    divAnnexureItemList_p.RenderControl(new HtmlTextWriter(new StringWriter(sbGvItemAnnexure)));
            //    divAnnexureBellowList_p.RenderControl(new HtmlTextWriter(new StringWriter(sbGvItemAnnexure)));
            //}
            //else
            //{
            //    Flag = "single";
            //    divAnnexureItemList_p.Attributes.Add("style", "display:none;");
            //    divAnnexureBellowList_p.Attributes.Add("style", "display:none;");
            //}

            string htmlfile = ViewState["RFPNo"].ToString().Replace('/', '-') + ".html";
            string pdffile = ViewState["RFPNo"].ToString().Replace('/', '-') + ".pdf";
            string LetterPath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
            string pdfFileURL = LetterPath + pdffile;

            string htmlfileURL = LetterPath + htmlfile;

            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 2].ToString() + "/" + pagename[pagename.Length - 1].ToString();

            string Main = url.Replace(Replacevalue, "Assets/css/main.css");
            string epstyleurl = url.Replace(Replacevalue, "Assets/css/ep-style.css");
            string style = url.Replace(Replacevalue, "Assets/css/style.css");
            string Print = url.Replace(Replacevalue, "Assets/css/print.css");
            string topstrip = url.Replace(Replacevalue, "Assets/images/topstrrip.jpg");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "RFPPrint('" + epstyleurl + "','" + Main + "','" + QrCode + "');", true);

            //SaveHtmlFile(htmlfileURL, "PO", "", sbPurchaseOrder.ToString(), sbGvItemAnnexure.ToString(), sbBellowAnnexure.ToString(), Flag);
            //objc.GenerateAndSavePDF(LetterPath, pdfFileURL, pdffile, htmlfileURL);
            //objc.ReadhtmlFile(htmlfile, hdnPdfContent);
            // divPrint.Attributes.Add("style", "display:none;");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }

    public void SaveHtmlFile(string URL, string HeaderTitle, string lbltitle, string div, string ItemAnnexure, string BellowsAnnexure, string Flag)
    {
        try
        {
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            url = url.ToLower();

            string[] pagename = url.ToString().Split('/');
            string Replacevalue = pagename[pagename.Length - 2].ToString() + "/" + pagename[pagename.Length - 1].ToString();

            string Main = url.Replace(Replacevalue, "Assets/css/main.css");
            string epstyleurl = url.Replace(Replacevalue, "Assets/css/ep-style.css");
            string style = url.Replace(Replacevalue, "Assets/css/style.css");
            string Print = url.Replace(Replacevalue, "Assets/css/print.css");
            string topstrip = url.Replace(Replacevalue, "Assets/images/topstrrip.jpg");

            StreamWriter w;
            w = File.CreateText(URL);
            w.WriteLine("<html><head><title>");
            w.WriteLine(HeaderTitle);
            w.WriteLine("</title>");
            w.WriteLine("<link rel='stylesheet' type='text/css' href='" + epstyleurl + "'/>");
            w.WriteLine("<link rel='stylesheet' href='" + Main + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + style + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + Print + "' type='text/css'/>");
            w.WriteLine("<link rel='stylesheet' href='" + topstrip + "' type='text/css'/>");

            w.WriteLine("<style type='text/css'>@media print,screen { label,table th{ font-weight: bold; font-size: 15px !important;font-family:Times New Roman;color:#000 !important; }}</style>");
            // w.WriteLine("table#ContentPlaceHolder1_gvItemDetails_p th{ font-family: Times New Roman;font-size: 15px; } }</style>");
            w.WriteLine("</head><body>");
            w.WriteLine("<div class='col-sm-12'>");
            // w.WriteLine("<div class='bor-fixed'></div>");
            w.WriteLine("<div style=''>");
            w.WriteLine(div);
            if (Flag == "multi")
            {
                w.WriteLine("<div style='margin:15px;padding:15px;'>");
                w.WriteLine("<div class='col-sm-12'>");
                w.WriteLine("<label>Requestion For Production</label>");
                w.WriteLine("</div>");
                w.WriteLine("<div class='col-sm-12'>");
                w.WriteLine("<label>RFP No:'" + lblRFPNo_P.Text + "'<label>");
                w.WriteLine("</div>");
                w.WriteLine("<div class='col-sm-12'>");
                w.WriteLine("<label>Purchase Order No And Date:'" + lblCustomerOrderNo_p.Text + "'</label>");
                w.WriteLine("</div>");

                w.WriteLine("<div class='col-sm-12'>");
                w.WriteLine(ItemAnnexure);
                w.WriteLine("</div>");
                w.WriteLine("</div>");

                w.WriteLine("<div style='width:200mm;padding:15px;margin:15px;float:left;display:none'>");
                w.WriteLine("<div class='col-sm-12'>");
                w.WriteLine("Bellows Details");
                w.WriteLine("</div>");
                w.WriteLine("<div class='col-sm-12'>");
                w.WriteLine(BellowsAnnexure);
                w.WriteLine("</div></div>");
            }
            w.WriteLine("</div></div></body></html>");
            w.Flush();
            w.Close();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void SaveAlertDetails()
    {
        EmailAndSmsAlerts objAlerts = new EmailAndSmsAlerts();
        DataSet ds = new DataSet();
        try
        {
            objAlerts.EntryMode = "Group";
            objAlerts.AlertType = "Mail";
            objAlerts.userID = objSession.employeeid;
            objAlerts.reciverType = "Selected Group";
            objAlerts.file = "";
            objAlerts.reciverID = "0";
            objAlerts.EmailID = "";
            objAlerts.GroupID = 13;
            objAlerts.Subject = "RFP Approval Alert";
            objAlerts.Message = "RFP Approval Request From PO Number " + ddlCustomerPO.SelectedItem.Text;
            objAlerts.Status = 0;
            objAlerts.SaveCommunicationEmailAlertDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"PageLoadComplete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvRFPHeader.Rows.Count > 0)
            gvRFPHeader.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (gvAttachments.Rows.Count > 0)
            gvAttachments.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion

}