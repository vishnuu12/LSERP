using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using eplus.core;
using System.Configuration;
using System.IO;

public partial class Pages_BOMApprovalSheet : System.Web.UI.Page
{
    #region"Declaration"

    cMaterials objMat;
    cDesign objDesign;
    cSession objSession = new cSession();
    cCommon objc;

    string CusstomerEnquirySavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString();
    string CustomerEnquiryHttpPath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString();

    #endregion

    #region"PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        try
        {
            objSession = Master.csSession;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];
            if (!IsPostBack)
            {
                //objc = new cCommon();
                //DataSet dsEnquiryNumber = new DataSet();
                //DataSet dsCustomer = new DataSet();
                //dsCustomer = objc.getCustomerNameByUserID(Convert.ToInt32(objSession.employeeid), ddlCustomerName, "LS_GetCustomernameByUserIDForBOMApprovalPage");
                //ViewState["CustomerDetails"] = dsCustomer.Tables[0];
                //dsEnquiryNumber = objc.GetEnquiryNumberByUserID(Convert.ToInt32(objSession.employeeid), ddlEnquiryNumber, "LS_GetEnquiryNumberByUserIDForBOMApprovalPage");
                //ViewState["EnquiryDetails"] = dsEnquiryNumber.Tables[0];
                ddlCustomerNameEnquiryLoad();
                ShowHideControls("input");
            }
            if (target == "UpdateBOMStatus")
                UpdateBOMStatus();
            if (target == "ViewDrawing")
            {
                int index = Convert.ToInt32(arg.ToString());
                ViewState["FileName"] = gvViewCosting.DataKeys[index].Values[1].ToString();
                ViewDrawingFilename();
            }
            if (target == "updatebomreview")
            {
                BomReview();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Radio Events"

    protected void rblEnquiryChange_OnSelectedChanged(object sender, EventArgs e)
    {
        ddlCustomerNameEnquiryLoad();
        ShowHideControls("input");
    }

    #endregion

    #region"DropDown Events"

    protected void ddlCustomerName_SelectIndexChanged(object sender, EventArgs e)
    {
        objc = new cCommon();
        DataView dv;
        DataTable dcustomr = new DataTable();
        DataTable denquiry = new DataTable();
        try
        {
            // objc.customerddlchnage(ddlCustomerName, ddlEnquiryNumber, (DataTable)ViewState["CustomerDetails"], (DataTable)ViewState["EnquiryDetails"]);
            dcustomr = (DataTable)ViewState["CustomerDetails"];
            denquiry = (DataTable)ViewState["EnquiryDetails"];

            if (ddlCustomerName.SelectedIndex > 0)
            {
                dv = new DataView(denquiry);
                dv.RowFilter = "ProspectID='" + ddlCustomerName.SelectedValue + "'";
                dcustomr = dv.ToTable();

                ddlEnquiryNumber.DataSource = dcustomr;
                ddlEnquiryNumber.DataTextField = "EnquiryNumber";
                ddlEnquiryNumber.DataValueField = "EnquiryID";
                ddlEnquiryNumber.DataBind();
            }
            else
            {
                ddlEnquiryNumber.DataSource = denquiry;
                ddlEnquiryNumber.DataTextField = "EnquiryNumber";
                ddlEnquiryNumber.DataValueField = "EnquiryID";
                ddlEnquiryNumber.DataBind();
            }

            ddlEnquiryNumber.Items.Insert(0, new ListItem("--Select--", "0"));

            ShowHideControls("input");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlEnquiryNumber_SelectIndexChanged(object sender, EventArgs e)
    {
        objc = new cCommon();
        objMat = new cMaterials();
        DataSet ds = new DataSet();
        DataTable dtEnquiry;
        try
        {
            if (ddlEnquiryNumber.SelectedIndex > 0)
            {
                ShowHideControls("view,input");

                dtEnquiry = (DataTable)ViewState["EnquiryDetails"];
                if (ddlEnquiryNumber.SelectedIndex > 0)
                {
                    DataView dv = new DataView(dtEnquiry);
                    dv.RowFilter = "EnquiryID='" + ddlEnquiryNumber.SelectedValue + "'";
                    dtEnquiry = dv.ToTable();
                    ddlCustomerName.SelectedValue = dtEnquiry.Rows[0]["ProspectID"].ToString();
                }

                // objc.enquiryddlchange(ddlEnquiryNumber, ddlCustomerName, (DataTable)ViewState["CustomerDetails"], (DataTable)ViewState["EnquiryDetails"]);
                objMat.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue);
                ds = objMat.GetViewCostingDetails();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //  btnShareBOM.Visible = true;
                    gvViewCosting.DataSource = ds.Tables[0];
                    gvViewCosting.DataBind();

                    if (objSession.type == 1)
                        gvViewCosting.Columns[18].Visible = true;
                    else
                        gvViewCosting.Columns[18].Visible = false;
                }
                else
                {
                    //  btnShareBOM.Visible = false;
                    gvViewCosting.DataSource = "";
                    gvViewCosting.DataBind();
                }

                if (ds.Tables[0].Rows[0]["BOMStatus"].ToString() == "Completed")
                {
                    //btnShareBOM.Enabled = false;
                    //btnShareBOM.ToolTip = "Enquiry Moved Into Next Stage";
                }
            }
            else
            {
                ShowHideControls("input");
                gvViewCosting.DataSource = "";
                gvViewCosting.DataBind();
            }
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

    protected void btnReject_Click(object sender, EventArgs e)
    {
        bool itemselected = false;
        objMat = new cMaterials();
        try
        {
            foreach (GridViewRow row in gvViewCosting.Rows)
            {
                CheckBox chkditems = (CheckBox)row.FindControl("chkitems");
                if (chkditems.Checked)
                {
                    itemselected = true;
                    DataSet dsApprove = new DataSet();
                    objMat.DDID = Convert.ToInt32(Convert.ToInt32(gvViewCosting.DataKeys[row.RowIndex].Values[0].ToString()));
                    objMat.Mode = "Reject";
                    TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                    objMat.Remarks = txtRemarks.Text;

                    objMat.UpdateDesignHApprovalStatus();
                }
            }

            ddlEnquiryNumber_SelectIndexChanged(null, null);

            if (itemselected == true)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','BOM Rejected Successfully')", true);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error Occured');", true);
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvViewCosting_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());

            if (e.CommandName == "ViewEnqAttachements")
            {
                bindEnquiryAttachementDetails();
            }
            if (e.CommandName == "ViewCosting")
            {

                DataSet ds = new DataSet();
                objMat = new cMaterials();
                DataRow dr;
                DataView dv;
                DataTable dtItemBomCost;
                DataTable dtAddtionalBomCost;
				DataTable dtIssueBomCost;

                objMat.DDID = Convert.ToInt32(e.CommandArgument.ToString());
                ds = objMat.GetViewCostingDetailsByItemID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "RecordsFound")
                {
                    gvItemCostingDetails.DataSource = ds.Tables[1];

                    ds.Tables[1].Columns.Remove("BOMID");
                    ds.Tables[1].Columns.Remove("ETCID");
                    ds.Tables[1].Columns.Remove("DDID");
                    ds.Tables[1].Columns.Remove("MID");
                    ds.Tables[1].Columns.Remove("BOMStatus");

                    dv = new DataView(ds.Tables[1]);
                    dv.RowFilter = "AddtionalPart='No'";
                    dtItemBomCost = dv.ToTable();

                    dr = dtItemBomCost.NewRow();
                    dr["MCOST"] = ds.Tables[2].Rows[0]["MCOST"].ToString();
                    dr["LCOST"] = ds.Tables[2].Rows[0]["LCOST"].ToString();
                    if (dtItemBomCost.Columns.Contains("MCRate"))
                        dr["MCRate"] = ds.Tables[2].Rows[0]["MCRate"].ToString();
                    dr["AWT"] = ds.Tables[2].Rows[0]["AWT"].ToString();
                    dr["WT"] = ds.Tables[2].Rows[0]["WT"].ToString();

                    dtItemBomCost.Rows.InsertAt(dr, dtItemBomCost.Rows.Count + 1);

                    if (dtItemBomCost.Columns.Contains("MCRate"))
                        dtItemBomCost.Columns["MCRate"].ColumnName = "M/C RATE";
                    dtItemBomCost.Columns["DrawingSequenceNumber"].ColumnName = "Part No";


                    gvItemCostingDetails.DataSource = dtItemBomCost;
                    gvItemCostingDetails.DataBind();

                    lblBOMCost.Text = "BOM Cost : " + ds.Tables[2].Rows[0]["TotalBOMCost"].ToString();


                    //FIM Part

                    dv = new DataView(ds.Tables[1]);
                    dv.RowFilter = "AddtionalPart='Issue'";
                    dtIssueBomCost = dv.ToTable();

                    dr = dtIssueBomCost.NewRow();
                    dr["MCOST"] = ds.Tables[4].Rows[0]["MCOST"].ToString();
                    dr["LCOST"] = ds.Tables[4].Rows[0]["LCOST"].ToString();
                    if (dtIssueBomCost.Columns.Contains("MCRATE"))
                        dr["MCRATE"] = ds.Tables[4].Rows[0]["MCRate"].ToString();
                    dr["AWT"] = ds.Tables[4].Rows[0]["AWT"].ToString();
                    dr["WT"] = ds.Tables[4].Rows[0]["WT"].ToString();

                    dtIssueBomCost.Rows.InsertAt(dr, dtIssueBomCost.Rows.Count + 1);

                    if (dtIssueBomCost.Columns.Contains("MCRATE"))
                        dtIssueBomCost.Columns["MCRate"].ColumnName = "M/C RATE";
                    dtIssueBomCost.Columns["DrawingSequenceNumber"].ColumnName = "Part No";

                    dtIssueBomCost.Columns.Remove("AddtionalPart");

                    gvIssuePartDetails.DataSource = dtIssueBomCost;
                    gvIssuePartDetails.DataBind();
                    lblIssuePartCost.Text = "FIM Part Cost : " + Convert.ToDecimal(ds.Tables[4].Rows[0]["MCOST"].ToString());

                    //End Code

                    dv = new DataView(ds.Tables[1]);
                    dv.RowFilter = "AddtionalPart='Yes'";
                    dtAddtionalBomCost = dv.ToTable();

                    dr = dtAddtionalBomCost.NewRow();
                    dr["MCOST"] = ds.Tables[3].Rows[0]["MCOST"].ToString();
                    dr["LCOST"] = ds.Tables[3].Rows[0]["LCOST"].ToString();
                    if (dtAddtionalBomCost.Columns.Contains("MCRATE"))
                        dr["MCRATE"] = ds.Tables[3].Rows[0]["MCRate"].ToString();
                    dr["AWT"] = ds.Tables[3].Rows[0]["AWT"].ToString();
                    dr["WT"] = ds.Tables[3].Rows[0]["WT"].ToString();

                    dtAddtionalBomCost.Rows.InsertAt(dr, dtAddtionalBomCost.Rows.Count + 1);

                    if (dtAddtionalBomCost.Columns.Contains("MCRATE"))
                        dtAddtionalBomCost.Columns["MCRate"].ColumnName = "M/C RATE";
                    dtAddtionalBomCost.Columns["DrawingSequenceNumber"].ColumnName = "Part No";

                    gvAddtionalPartDetails.DataSource = dtAddtionalBomCost;
                    gvAddtionalPartDetails.DataBind();

                    lblAddtionalPartCost.Text = "Addtional Part Cost : " + ds.Tables[3].Rows[0]["AddtionalPartCost"].ToString();

                    Decimal TotalCost = Convert.ToDecimal(ds.Tables[2].Rows[0]["TotalBOMCost"].ToString()) + Convert.ToDecimal(ds.Tables[3].Rows[0]["AddtionalPartCost"].ToString()) + Convert.ToDecimal(ds.Tables[4].Rows[0]["IssuePartCost"].ToString());

                    lblTotalBOMCost.Text = "Total Bom Cost:" + TotalCost;
                }
                else
                {
                    gvItemCostingDetails.DataSource = "";
                    gvItemCostingDetails.DataBind();
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUP", "ShowViewPopUP();", true);
            }
            if (e.CommandName == "ViewDeviationFile")
            {
                //string DrawingDocumentSavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString().Trim();
                //string DrawingDocumentHttpPath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString().Trim();

                //string FileName = gvViewCosting.DataKeys[index].Values[1].ToString();
                //string Savepath = DrawingDocumentSavePath;
                //string httpPath = DrawingDocumentHttpPath;
                //string EnquiryID = ddlEnquiryNumber.SelectedValue;

                //ifrm.Attributes.Add("src", httpPath + EnquiryID + "/" + FileName);
                //string imgname = DrawingDocumentSavePath + ddlEnquiryNumber.SelectedValue + "\\" + FileName;
                //if (File.Exists(imgname))
                //{
                //    ViewState["ifrmsrc"] = httpPath + FileName;
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewdocsPopUp();", true);
                //}
                //else
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Drawing Not Found');", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvViewCosting_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chkditems = (CheckBox)e.Row.FindControl("chkitems");
                TextBox txtremarks = (TextBox)e.Row.FindControl("txtRemarks");
                CheckBox chkreviewbomitems = (CheckBox)e.Row.FindControl("chkreviewbomitems");

                if (dr["DesignHApproval"].ToString() == "0" || dr["DesignHApproval"].ToString() == "9" || dr["BOMStatus"].ToString() == "Completed")
                    chkditems.Visible = false;
                else
                {
                    if (dr["SharedWithSales"].ToString() == "1")
                        chkditems.Visible = true;
                    else
                        chkditems.Visible = false;
                }

                if (dr["DesignHApproval"].ToString() == "9")
                    txtremarks.Text = dr["DesignHODRemarks"].ToString();
                if (dr["BOMStatus"].ToString() == "Completed" && dr["SalesCostStatus"].ToString() == "InComplete")
                    chkreviewbomitems.Visible = true;
                else
                    chkreviewbomitems.Visible = false;

            }
            //&& dr["SalesCostStatus"].ToString() == "InComplete"
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvAttachments_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dtAttachement;
        cSales objSales = new cSales();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            int AttachementID = Convert.ToInt32(gvAttachments.DataKeys[index].Values[0].ToString());

            dtAttachement = (DataTable)ViewState["Attachement"];
            dtAttachement.DefaultView.RowFilter = "AttachementID='" + AttachementID + "'";
            dtAttachement.DefaultView.ToTable();

            //gvCustomerEnquiry.UseAccessibleHeader = true;
            //gvCustomerEnquiry.HeaderRow.TableSection = TableRowSection.TableHeader;

            if (e.CommandName.ToString() == "ViewDocs")
            {
                string BasehttpPath = CustomerEnquiryHttpPath + ddlEnquiryNumber.SelectedValue + "/";
                string FileName = ((Label)gvAttachments.Rows[index].FindControl("lblFileName_V")).Text.ToString();
                ViewState["FileName"] = FileName;
                ifrm.Attributes.Add("src", BasehttpPath + FileName);
                string imgname = CusstomerEnquirySavePath + ddlEnquiryNumber.SelectedValue + "\\" + FileName;

                cCommon.DownLoad(FileName, CusstomerEnquirySavePath + ddlEnquiryNumber.SelectedValue.ToString() + "\\" + FileName);

                //if (File.Exists(imgname))
                //{                   
                //    ViewState["ifrmsrc"] = BasehttpPath + FileName;
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewdocsPopUp();", true);
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
            Log.Message("CustomerEnquiryProcess" + " " + "gvAttachments_OnRowCommandex" + "" + " " + ex.ToString());
        }
        finally
        {
            dtAttachement = null;
            objSales = null;
        }
    }

    protected void gvAttachments_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            // string BasehttpPath = CusstomerEnquirySavePath + ViewState["EnquiryNumber"].ToString() + "\\";
            string BasehttpPath = CustomerEnquiryHttpPath + ViewState["EnquiryNumber"].ToString() + "/";
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string extension = dr["FileName"].ToString().Split('.')[1].ToUpper();
                ImageButton imgbtn = (ImageButton)e.Row.FindControl("imgbtnView");
                if (extension == "PDF")
                {
                    byte[] imageBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Assets/images/pdf.png"));
                    string base64String = Convert.ToBase64String(imageBytes);
                    imgbtn.ImageUrl = String.Format(@"data:image/jpeg;base64,{0}", base64String);
                }
                else if (extension == "DOC" || extension == "DOCX")
                {
                    byte[] imageBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Assets/images/word-ls.png"));
                    string base64String = Convert.ToBase64String(imageBytes);
                    imgbtn.ImageUrl = String.Format(@"data:image/jpeg;base64,{0}", base64String);

                }
                else if (extension == "XLS" || extension == "XLSX")
                {
                    byte[] imageBytes = System.IO.File.ReadAllBytes(Server.MapPath("~/Assets/images/excel-ls.png"));
                    string base64String = Convert.ToBase64String(imageBytes);
                    imgbtn.ImageUrl = String.Format(@"data:image/jpeg;base64,{0}", base64String);
                }
                else
                {
                    //byte[] imageBytes = System.IO.File.ReadAllBytes(BasehttpPath + dr["FileName"].ToString());
                    //string base64String = Convert.ToBase64String(imageBytes);
                    //imgbtn.ImageUrl = String.Format(@"data:image/jpeg;base64,{0}", base64String);
                    imgbtn.ImageUrl = BasehttpPath + dr["FileName"].ToString();
                }

                imgbtn.ToolTip = dr["FileName"].ToString();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void bindEnquiryAttachementDetails()
    {
        cSales objSales = new cSales();
        DataSet dsGetAttachementsDetails = new DataSet();
        dsGetAttachementsDetails = objSales.GetEnquiryprocessDetails(ddlEnquiryNumber.SelectedValue, "LS_GetAttachementsDetails", false);
        ViewState["Attachement"] = dsGetAttachementsDetails.Tables[0];
        try
        {
            if (dsGetAttachementsDetails.Tables[0].Rows.Count > 0)
            {
                gvAttachments.DataSource = dsGetAttachementsDetails.Tables[0];
                gvAttachments.DataBind();
            }
            else
            {
                gvAttachments.DataSource = "";
                gvAttachments.DataBind();
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowEnquiryAttachementsPopUp();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        finally
        {
            objSales = null;
            dsGetAttachementsDetails = null;
        }
    }

    private void UpdateBOMStatus()
    {
        DataSet ds = new DataSet();
        try
        {
            objMat = new cMaterials();
            foreach (GridViewRow row in gvViewCosting.Rows)
            {
                CheckBox chkditems = (CheckBox)row.FindControl("chkitems");
                if (chkditems.Checked)
                {
                    objMat.DDID = Convert.ToInt32(gvViewCosting.DataKeys[row.RowIndex].Values[0].ToString());
                    objMat.UserID = Convert.ToInt32(objSession.employeeid);
                    ds = objMat.UpdateBOMStatusByDDID();
                }
            }
            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','BOM Status Updated Successfully');", true);
            SaveAlertDetails();
            ddlEnquiryNumber_SelectIndexChanged(null, null);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ViewDrawingFilename()
    {
        try
        {
            string DrawingDocumentSavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString().Trim();
            string DrawingDocumentHttpPath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString().Trim();

            string Savepath = DrawingDocumentSavePath;
            string httpPath = DrawingDocumentHttpPath;
            string EnquiryID = ddlEnquiryNumber.SelectedValue;

            ifrm.Attributes.Add("src", httpPath + EnquiryID + "/" + ViewState["FileName"].ToString());
            string imgname = DrawingDocumentSavePath + ddlEnquiryNumber.SelectedValue + "\\" + ViewState["FileName"].ToString();

            objc = new cCommon();

            objc.ViewFileName(DrawingDocumentSavePath, DrawingDocumentHttpPath, ViewState["FileName"].ToString(), EnquiryID, ifrm);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ShowHideControls(string div)
    {
        try
        {
            string[] mode = div.Split(',');
            divInput.Visible = divOutput.Visible = false;
            for (int i = 0; i < mode.Length; i++)
            {
                switch (mode[i].ToLower())
                {
                    case "input":
                        divInput.Visible = true;
                        break;
                    case "view":
                        divOutput.Visible = true;
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BomReview()
    {
        bool itemselected = false;
        objMat = new cMaterials();
        DataSet ds = new DataSet();
        try
        {
            foreach (GridViewRow row in gvViewCosting.Rows)
            {
                CheckBox chkditems = (CheckBox)row.FindControl("chkreviewbomitems");
                if (chkditems.Checked)
                {
                    itemselected = true;
                    DataSet dsApprove = new DataSet();
                    objMat.DDID = Convert.ToInt32(Convert.ToInt32(gvViewCosting.DataKeys[row.RowIndex].Values[0].ToString()));
                    TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                    objMat.Remarks = txtRemarks.Text;

                    ds = objMat.UpdateBOMreviewStatus();
                }
            }
            if (itemselected == true)
            {
                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','BOM Reversed Successfully');", true);
                    ddlEnquiryNumber_SelectIndexChanged(null, null);
                }
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Please Select The Item');", true);
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
            objAlerts.GroupID = 12;
            objAlerts.Subject = "Sales Cost Approval Alert";
            objAlerts.Message = "Sales Cost Approval Request From Enquiry Number " + ddlEnquiryNumber.SelectedValue;
            objAlerts.Status = 0;
            objAlerts.SaveCommunicationEmailAlertDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ddlCustomerNameEnquiryLoad()
    {
        try
        {
            objc = new cCommon();
            DataSet dsEnquiryNumber = new DataSet();
            DataSet dsCustomer = new DataSet();
            dsCustomer = objc.GetCustomerNameByPendingList(Convert.ToInt32(objSession.employeeid), ddlCustomerName, "LS_GetCustomernameByUserIDForBOMApprovalPage", rblEnquiryChange.SelectedValue);
            ViewState["CustomerDetails"] = dsCustomer.Tables[0];
            dsEnquiryNumber = objc.GetEnquiryNumberByPendingList(Convert.ToInt32(objSession.employeeid), ddlEnquiryNumber, "LS_GetEnquiryNumberByUserIDForBOMApprovalPage", rblEnquiryChange.SelectedValue);
            ViewState["EnquiryDetails"] = dsEnquiryNumber.Tables[0];
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
        if (gvViewCosting.Rows.Count > 0)
            gvViewCosting.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (gvItemCostingDetails.Rows.Count > 0)
            gvItemCostingDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (gvAddtionalPartDetails.Rows.Count > 0)
            gvAddtionalPartDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}