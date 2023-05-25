using eplus.core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_RFPStaffAllocationQuality : System.Web.UI.Page
{
    #region"Declaration"

    cSession _objSess = new cSession();
    cCommonMaster objCommon;
    cCommon objc;
    string CusstomerEnquirySavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString();
    string CustomerEnquiryHttpPath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString();

    #endregion

    #region"PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        _objSess = Master.csSession;
    }

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (IsPostBack == false)
            {
                BindStaffAssignmentEnquiryDetails();
            }
            else
                return;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"radio Events"

    protected void rblRFPChange_OnSelectedChanged(object sender, EventArgs e)
    {
        BindStaffAssignmentEnquiryDetails();
    }

    #endregion

    #region"Button Events"

    protected void btnSaveStaff_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objCommon = new cCommonMaster();
        try
        {
            objCommon.SAID = Convert.ToInt32(hdnSAID.Value);
            //objCommon.EnquiryID = Convert.ToInt32(gvStaffAssignmentDetails.DataKeys[e.RowIndex].Values[1].ToString().Split('/')[0]);
            /// objCommon.EmployeeID = hdnEmployeeIDs.Value;            
            objCommon.EmployeeIds = hdnEmployeeIDs.Value;
            //objCommon.DepartmentID = Convert.ToInt32(ViewState["DepartmentID"].ToString());
            //objCommon.PQDeadLineDate = true;
            objCommon.QualityAndProductionDeadLineDate = DateTime.ParseExact(txtDeadLineDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objCommon.CreatedBy = _objSess.employeeid;
            objCommon.DepartmentName = "Quality";

            objCommon.LocationName = hdnLocationType.Value;

            ds = objCommon.UpdateStaffRFPAllocation("LS_UpdateRFPAllocationStaffAssignment");

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myFunction", "SuccessMessage('Success !','Records Updated Successfully');CloseStaffPopUp();", true);

            BindStaffAssignmentEnquiryDetails();
            SaveAlertDetails(hdnEmployeeIDs.Value);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvStaffAssignmentDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        DataTable dtEmployeeListByDept;

        try
        {
            //Set the edit index.
            gvStaffAssignmentDetails.EditIndex = e.NewEditIndex;
            //Bind data to the GridView control.
            BindStaffAssignmentEnquiryDetails();

            DropDownList dl = new DropDownList();
            dl = (DropDownList)gvStaffAssignmentDetails.Rows[gvStaffAssignmentDetails.EditIndex].FindControl("ddlEmployeeName");

            dtEmployeeListByDept = (DataTable)ViewState["EmployeeListByDept"];
            dl.DataSource = dtEmployeeListByDept;
            dl.DataTextField = "EmployeeName";
            dl.DataValueField = "EmployeeID";
            dl.DataBind();
            dl.Items.Insert(0, new ListItem("-- Select Employee Name --", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvStaffAssignmentDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            gvStaffAssignmentDetails.EditIndex = -1;
            BindStaffAssignmentEnquiryDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvStaffAssignmentDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        objCommon = new cCommonMaster();
        DataSet ds = new DataSet();
        objc = new cCommon();
        try
        {
            DropDownList ddl = ((DropDownList)gvStaffAssignmentDetails.Rows[e.RowIndex].FindControl("ddlEmployeeName"));
            TextBox txtDeadLineDate = (TextBox)gvStaffAssignmentDetails.Rows[e.RowIndex].FindControl("txtDeadLineDate");
            if (ddl.SelectedIndex != 0 && txtDeadLineDate.Text != "")
            {
                objCommon.SAID = Convert.ToInt32(gvStaffAssignmentDetails.DataKeys[e.RowIndex].Values[0].ToString());
                //objCommon.EnquiryID = Convert.ToInt32(gvStaffAssignmentDetails.DataKeys[e.RowIndex].Values[1].ToString().Split('/')[0]);
                objCommon.EmployeeID = Convert.ToInt32(((DropDownList)gvStaffAssignmentDetails.Rows[e.RowIndex].FindControl("ddlEmployeeName")).SelectedValue);
                //objCommon.DepartmentID = Convert.ToInt32(ViewState["DepartmentID"].ToString());
                //objCommon.PQDeadLineDate = true;
                objCommon.QualityAndProductionDeadLineDate = DateTime.ParseExact(txtDeadLineDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                objCommon.CreatedBy = _objSess.employeeid;
                objCommon.DepartmentName = "Quality";
                ds = objCommon.UpdateStaffRFPAllocation("LS_UpdateRFPAllocationStaffAssignment");

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myFunction", "SuccessMessage('Success !','Records Updated Successfully');", true);

                gvStaffAssignmentDetails.EditIndex = -1;
                BindStaffAssignmentEnquiryDetails();

            }
            else
            {
                if (ddl.SelectedIndex == 0)
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Employee Name Required!');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Dead Line Date Required!');", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvStaffAssignmentDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = (DataRowView)e.Row.DataItem;
                e.Row.Cells[9].ToolTip = "Edit";
                if (e.Row.RowState == DataControlRowState.Edit || e.Row.RowState.ToString() == "Alternate, Edit")
                {
                    foreach (TableCell cell in e.Row.Cells)
                    {
                        if (e.Row.Cells.GetCellIndex(cell) == 9)
                        {
                            ((System.Web.UI.WebControls.ImageButton)(e.Row.Cells[9].Controls[0])).ToolTip = "Update";
                            ((System.Web.UI.LiteralControl)(e.Row.Cells[9].Controls[1])).Text = "&nbsp;";
                            ((System.Web.UI.WebControls.ImageButton)(e.Row.Cells[9].Controls[2])).ToolTip = "Close";
                        }
                    }
                }
                if (dr["Staff"].ToString() == "Not Assigned")
                    e.Row.Attributes.Add("style", "Background-color:#ffd400");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvStaffAssignmentDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "Edit")
        {
            // gvStaffAssignmentDetails_RowEditing(sender, (GridViewEditEventArgs)((e)));
        }
        else if (e.CommandName == "Cancel")
        {

        }
        else if (e.CommandName == "Update")
        {

        }
        else
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            int EnquiryNumber = Convert.ToInt32(gvStaffAssignmentDetails.DataKeys[index].Values[2].ToString());
            string BaseHtttpPath = CustomerEnquiryHttpPath + EnquiryNumber + "/";

            if (e.CommandName == "ViewPOCopy")
            {
                objc = new cCommon();
                string FileName = gvStaffAssignmentDetails.DataKeys[index].Values[3].ToString();
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
                objc = new cCommon();
                string FileName = gvStaffAssignmentDetails.DataKeys[index].Values[4].ToString();
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

            if (e.CommandName == "ViewRFPPDF")
            {
                //string PDFSavePath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
                //string PDFhttpPath = ConfigurationManager.AppSettings["PDFPath"].ToString();

                //string FileName = gvStaffAssignmentDetails.DataKeys[index].Values[5].ToString().Replace('/', '-') + ".pdf";
                //ViewState["FileName"] = FileName;
                //ifrm.Attributes.Add("src", PDFhttpPath + FileName);

                //if (File.Exists(PDFSavePath + "//" + FileName))
                //{
                //    ViewState["ifrmsrc"] = PDFhttpPath + FileName;
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewdocsPopUp();", true);
                //}
                //else
                //{
                //    ifrm.Attributes.Add("src", "");
                //    ViewState["ifrmsrc"] = "";
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','File Not Found');", true);
                //}

                objc = new cCommon();
                //string PDFSavePath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
                //string PDFhttpPath = ConfigurationManager.AppSettings["PDFPath"].ToString();

                string RFPNo = gvStaffAssignmentDetails.DataKeys[index].Values[5].ToString();
                string htmlfile = RFPNo.ToString().Replace('/', '-') + ".html";

                //ViewState["FileName"] = FileName;
                //ifrm.Attributes.Add("src", PDFhttpPath + FileName);

                //if (File.Exists(PDFSavePath + "//" + FileName))
                //{
                //    ViewState["ifrmsrc"] = PDFhttpPath + FileName;
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewdocsPopUp();", true);
                //}
                //else
                //{
                //    ifrm.Attributes.Add("src", "");
                //    ViewState["ifrmsrc"] = "";
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','File Not Found');", true);
                //}
                objc.ReadhtmlFile(htmlfile, hdnPdfContent);
            }
            if (e.CommandName == "ViewAttachements")
            {
                string RFPHID = gvStaffAssignmentDetails.DataKeys[index].Values[6].ToString();
                BindAttachements(RFPHID);
                ViewState["EnquiryID"] = EnquiryNumber;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewPopUp();", true);
            }

            if (e.CommandName == "UnitAStaff")
            {
                cProduction objp = new cProduction();
                DataSet ds = new DataSet();

                Label lblRFPNo = (Label)gvStaffAssignmentDetails.Rows[index].FindControl("lblRFPNo");

                ViewState["AlertRFPNo"] = lblRFPNo.Text;

                lblRFPNumber_staff.Text = lblRFPNo.Text;

                hdnSAID.Value = gvStaffAssignmentDetails.DataKeys[index].Values[0].ToString();
                objp.USERID = _objSess.employeeid;
                objp.LocationID = "1";
                hdnLocationType.Value = "UNIT-A";

                ds = objp.GetQualityEmployeeList();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowStaffPopUp('" + ds.Tables[0].Rows[0]["Res"].ToString() + "');", true);
            }

            if (e.CommandName == "UnitBStaff")
            {
                cProduction objp = new cProduction();
                DataSet ds = new DataSet();

                Label lblRFPNo = (Label)gvStaffAssignmentDetails.Rows[index].FindControl("lblRFPNo");

                ViewState["AlertRFPNo"] = lblRFPNo.Text;

                lblRFPNumber_staff.Text = lblRFPNo.Text;

                hdnSAID.Value = gvStaffAssignmentDetails.DataKeys[index].Values[0].ToString();
                objp.USERID = _objSess.employeeid;
                objp.LocationID = "2";
                hdnLocationType.Value = "UNIT-B";

                ds = objp.GetQualityEmployeeList();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowStaffPopUp('" + ds.Tables[0].Rows[0]["Res"].ToString() + "');", true);
            }

            if (e.CommandName == "print")
            {
                string RFPHID = gvStaffAssignmentDetails.DataKeys[index].Values[6].ToString();
                Label lblCustomerName = (Label)gvStaffAssignmentDetails.Rows[index].FindControl("lblCustomerName");

                cSales objSales = new cSales();
                DataSet dsItem = new DataSet();
                dsItem = objSales.GetRFPItemdetails(RFPHID);

                DataSet ds = new DataSet();
                DataTable dtItemDetails = new DataTable();

                objSales = new cSales();
                objSales.RFPHID = Convert.ToInt32(RFPHID);
                ds = objSales.getBellowsDetailsByRFPHID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "RecordsFound" || dsItem.Tables[0].Rows.Count > 0)
                {
                    lblRFPNo_P.Text = dsItem.Tables[2].Rows[0]["RFPNo"].ToString() + " & " + ds.Tables[1].Rows[0]["RFPCreateddate"].ToString();
                    //  lblcreateddate_p.Text = ds.Tables[1].Rows[0]["RFPCreateddate"].ToString();
                    lblCustomerName_p.Text = lblCustomerName.Text;
                    //string[] PONumber = ddlCustomerPO.SelectedItem.Text.Split('/');
                    lblCustomerOrderNo_p.Text = dsItem.Tables[2].Rows[0]["PONo"].ToString() + " & " + ds.Tables[1].Rows[0]["CustomerOrderDate"].ToString();
                    //lblOrderDate_p.Text = ds.Tables[1].Rows[0]["CustomerOrderDate"].ToString();
                    lblProject_p.Text = dsItem.Tables[2].Rows[0]["ProjectName"].ToString();
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

                    lblQAPRefNo_p.Text = dsItem.Tables[2].Rows[0]["QAPRefNo"].ToString();
                    lblQAPApproval_p.Text = dsItem.Tables[2].Rows[0]["QAPApprovalName"].ToString();
                    lblDrawingApproval_p.Text = dsItem.Tables[2].Rows[0]["DrawingApprovalName"].ToString();
                    lblInspectionRequirtment_p.Text = dsItem.Tables[2].Rows[0]["InspectionRequirtmentName"].ToString();
                    lblLDClause_p.Text = dsItem.Tables[2].Rows[0]["LDClauseName"].ToString();
                    lblDespatchDetails_p.Text = dsItem.Tables[2].Rows[0]["DespatchDetails"].ToString();
                    lblMarketing_p.Text = ds.Tables[1].Rows[0]["SalesE"].ToString();
                    lblDesign_p.Text = ds.Tables[1].Rows[0]["DesignE"].ToString();
                    // lblDataEntry_p.Text = ds.Tables[1].Rows[0]["PurchaseE"].ToString();
                    lblProjectIncharge_p.Text = ds.Tables[1].Rows[0]["SalesE"].ToString();
                    lblApprovedBy_p.Text = ds.Tables[1].Rows[0]["RFPApprovedBy"].ToString();
                    lblDueDateForDispatch_p.Text = dsItem.Tables[2].Rows[0]["DeliveryDate"].ToString();//ds.Tables[1].Rows[0][""].ToString();
                    lblNotesSummary_p.Text = dsItem.Tables[2].Rows[0]["NotesSummary"].ToString();
                    lblLocationName_p.Text = dsItem.Tables[2].Rows[0]["LocationName"].ToString();

                    ViewState["RFPNo"] = dsItem.Tables[2].Rows[0]["RFPNo"].ToString();

                    GeneratePDF();
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Add RFP item Details');", true);
            }
        }
    }

    protected void gvAttachments_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        cSales objSales = new cSales();
        objc = new cCommon();
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewPopUp();", true);

                //if (File.Exists(imgname))
                //{
                //    //ViewState["ifrmsrc"] = imgname;
                //    ViewState["ifrmsrc"] = BasehttpPath + FileName;
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewdocsPopUp();HideViewPopUp();", true);
                //}
                //else
                //{
                //    ifrm.Attributes.Add("src", "");
                //    ViewState["ifrmsrc"] = "";
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Drawing Not Found');", true);
                //}
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvDrawingFiles_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            objc = new cCommon();
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            string EnquiryNumber = gvDrawingFiles.DataKeys[index].Values[1].ToString();
            string DrawingName = gvDrawingFiles.DataKeys[index].Values[0].ToString();

            string BasehttpPath = CustomerEnquiryHttpPath + EnquiryNumber + "/";
            string FileName = BasehttpPath + DrawingName;

            ViewState["FileName"] = FileName;
            ifrm.Attributes.Add("src", FileName);

            objc.ViewFileName(CusstomerEnquirySavePath, CustomerEnquiryHttpPath, DrawingName, EnquiryNumber, ifrm);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewPopUp();", true);

            //if (File.Exists(CusstomerEnquirySavePath + EnquiryNumber + '/' + DrawingName))
            //{
            //    ViewState["ifrmsrc"] = FileName;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowViewdocsPopUp();", true);
            //}
            //else
            //{
            //    ViewState["ifrmsrc"] = "";
            //    ifrm.Attributes.Add("src", FileName);
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Attach Not Found');", true);
            //}
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

    #endregion

    #region"Common Methods"

    public void SaveAlertDetails(string EmployeeIds)
    {
        EmailAndSmsAlerts objAlerts = new EmailAndSmsAlerts();
        DataSet ds = new DataSet();
        cCommon objc = new cCommon();
        try
        {
            string[] str = EmployeeIds.Split(',');
            for (int i = 0; i < str.Length; i++)
            {
                objAlerts.EntryMode = "Individual";
                objAlerts.AlertType = "Mail";
                objAlerts.userID = _objSess.employeeid;
                objAlerts.reciverType = "Staff";
                objAlerts.file = "";
                objAlerts.reciverID = str[i];
                objAlerts.EmailID = "";
                objAlerts.GroupID = 0;
                objAlerts.Subject = "RFP Assignment Alert";
                objAlerts.Message = "RFP Allotted RFP No" + ViewState["AlertRFPNo"].ToString();
                objAlerts.Status = 0;
                objAlerts.SaveCommunicationEmailAlertDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindStaffAssignmentEnquiryDetails()
    {
        objCommon = new cCommonMaster();
        DataSet ds = new DataSet();
        try
        {
            objCommon.EmployeeID = Convert.ToInt32(_objSess.employeeid);
            objCommon.status = rblRFPChange.SelectedValue;

            ds = objCommon.GetRFPStaffAllocationDetails("LS_GetQualityEmployeeDetails");
            ViewState["EmployeeListByDept"] = ds.Tables[1];
            //ViewState["DepartmentID"] = ds.Tables[2].Rows[0]["DepartmentID"].ToString();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvStaffAssignmentDetails.DataSource = ds.Tables[0];
                gvStaffAssignmentDetails.DataBind();
                //gvStaffAssignmentDetails.UseAccessibleHeader = true;
                //gvStaffAssignmentDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "showDataTable();", true);
            }
            else
            {
                gvStaffAssignmentDetails.DataSource = "";
                gvStaffAssignmentDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    public void BindAttachements(string RFPHID)
    {
        cSales objSales = new cSales();
        try
        {
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
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
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

            string Code = displayQrnumber + "/" + _objSess.employeeid + "/" + DateTime.Now.ToString();

            string QrCode = objQr.QRcodeGeneration(Code);
            if (QrCode != "")
            {
                //imgQrcode.ImageUrl = QrCode;
                objQr.QRNumber = displayQrnumber;
                objQr.fileName = "RFPRequest";
                objQr.createdBy = _objSess.employeeid;
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

    #endregion

    #region"PageLoadComplete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvStaffAssignmentDetails.Rows.Count > 0)
            gvStaffAssignmentDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion

}