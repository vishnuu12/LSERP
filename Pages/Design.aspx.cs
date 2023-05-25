using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Configuration;
using eplus.core;
using System.Text.RegularExpressions;

public partial class Pages_Design : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cDesign objDesign = new cDesign();
    EmailAndSmsAlerts objAlerts;
    cCommon objcommon = new cCommon();
    c_HR objHR;
    cSales objSales;
    cCommon objc;
    string DrawingDocumentSavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString();
    string DrawingDocumentHttpPath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString().Trim();

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
                ddlenquiryload();
                //DataSet dsEnquiryNumber = new DataSet();
                //DataSet dsCustomer = new DataSet();
                //dsCustomer = objcommon.getCustomerNameByUserID(Convert.ToInt32(objSession.employeeid), ddlCustomerName, "LS_GetCustomerNameByEmployeeID");
                //ViewState["CustomerDetails"] = dsCustomer.Tables[1];
                //dsEnquiryNumber = objcommon.GetEnquiryNumberByUserID(Convert.ToInt32(objSession.employeeid), ddlEnquiryNumber, "LS_GetEnquiryIDByUserID");
                //ViewState["EnquiryDetails"] = dsEnquiryNumber.Tables[0];
                divOutput.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region "DropDown Events"   
    protected void rblEnquiryChange_OnSelectedChanged(object sender, EventArgs e)
    {
        try
        {
            ddlenquiryload();
            divOutput.Visible = false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlCustomerName_SelectIndexChanged(object sender, EventArgs e)
    {
        objc = new cCommon();
        DataView dv;
        DataTable dcustomr = new DataTable();
        DataTable denquiry = new DataTable();
        try
        {
            //objcommon.customerddlchnage(ddlCustomerName, ddlEnquiryNumber, (DataTable)ViewState["CustomerDetails"], (DataTable)ViewState["EnquiryDetails"]);
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
                // objcommon.enquiryddlchange(ddlEnquiryNumber, ddlCustomerName,(DataTable)ViewState["CustomerDetails"], (DataTable)ViewState["EnquiryDetails"]);
                // lblEnquiryNumber_A.Text = ddlEnquiryNumber.SelectedItem.Text;
                objDesign.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue);

                ViewState["EnquiryNumber"] = ddlEnquiryNumber.SelectedValue;
                dtEnquiry = (DataTable)ViewState["EnquiryDetails"];

                if (ddlEnquiryNumber.SelectedIndex > 0)
                {
                    DataView dv = new DataView(dtEnquiry);
                    dv.RowFilter = "EnquiryID='" + ddlEnquiryNumber.SelectedValue + "'";
                    dtEnquiry = dv.ToTable();
                    ddlCustomerName.SelectedValue = dtEnquiry.Rows[0]["ProspectID"].ToString();
                }

                ds = objDesign.GetDesignDocumentDetailsByEnquiryID();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    divOutput.Visible = true;

                    lblClientName.Text = ds.Tables[0].Rows[0]["ClientName"].ToString();
                    lblProjectName.Text = ds.Tables[0].Rows[0]["ProjectName"].ToString();
                    lblSalesAgent.Text = ds.Tables[0].Rows[0]["SalesAgent"].ToString();
                    ViewState["EmployeeID"] = ds.Tables[0].Rows[0]["Sales"].ToString();
                    lblOfferID.Text = ds.Tables[0].Rows[0]["OfferID"].ToString();
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        gvDesignDetails.DataSource = ds.Tables[1];
                        gvDesignDetails.DataBind();

                        //if (ds.Tables[1].Rows[0]["SharedWithSales"].ToString() == "Yes")
                        //    btnSaveAndShare.CssClass = "btn btn-success aspNetDisabled";
                        //else
                        //    btnSaveAndShare.CssClass = "btn btn-success";

                        // ds.Tables[1].Columns.Contains("")
                    }
                    else
                    {
                        gvDesignDetails.DataSource = "";
                        gvDesignDetails.DataBind();
                    }
                    //gvDesignDetails.UseAccessibleHeader = true;
                    //gvDesignDetails.HeaderRow.TableSection = TableRowSection.TableHeader;

                    //   ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "ShowDataTableDesignDetails();", true);

                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        gvRevisedList.DataSource = ds.Tables[2];
                        gvRevisedList.DataBind();
                        //gvRevisedList.UseAccessibleHeader = true;
                        //gvRevisedList.HeaderRow.TableSection = TableRowSection.TableHeader;
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "ShowDataTableDesignDetails();ShowDataTableRevisedList();", true);
                    }
                    else
                    {
                        gvRevisedList.DataSource = "";
                        gvRevisedList.DataBind();

                    }

                    dt = (DataTable)ViewState["EnquiryDetails"];

                    if (dt.Rows[0]["ERPUserType"].ToString() == "1")
                        lbtnNewDrawing.Visible = true;

                    else if (dt.Rows[0]["ERPUserType"].ToString() == "3")
                        lbtnNewDrawing.Visible = false;

                    else if (dt.Rows[0]["ERPUserType"].ToString() == "2" && dt.Rows[0]["own"].ToString() == "0")
                        lbtnNewDrawing.Visible = true;
                    else
                        lbtnNewDrawing.Visible = false;
                }
                else
                    divOutput.Visible = false;
            }
            else
                divOutput.Visible = false;

            //if (sender != "load")

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnSaveAndShare_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objDesign = new cDesign();
        try
        {
            if (gvDesignDetails.Rows.Count > 0)
            {

                UpdateSharedWithHODStatus();

            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSaveDrawing_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objAlerts = new EmailAndSmsAlerts();
        objHR = new c_HR();
        string FileName = "";
        string[] extension;
        string AttachmentName;
        string path;
        string DeviationName = "";
        try
        {
            if (ValidateFields())
            {
                string extn = Path.GetExtension(fAttachment.PostedFile.FileName).ToUpper();
                FileName = Path.GetFileName(fAttachment.PostedFile.FileName);

                string Attchname = "";
                extension = FileName.Split('.');
                Attchname = Regex.Replace(extension[0].ToString(), @"[^0-9a-zA-Z]+", "");

                AttachmentName = Attchname + '_' + ddlEnquiryNumber.SelectedValue + '_' + ViewState["EDID"].ToString() + '_' + lblVersionNumber_A.Text + '.' + extension[extension.Length - 1];

                objDesign.EDID = Convert.ToInt32(ViewState["EDID"].ToString());

                if (chkBOMCostApprove.Checked == true)
                    objDesign.BOMCostApprove = 1;
                else
                    objDesign.BOMCostApprove = 0;

                //+ ddlItemName.SelectedValue + '_'
                objDesign.AttachementName = AttachmentName;
                objDesign.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue);

                objDesign.drawingName = txtDrawingName.Text;
                objDesign.DesignNumber = txtDesignNumber.Text;

                objDesign.DesignCode = txtDesignCode.Text == "" ? null : txtDesignCode.Text;
                objDesign.Tag = null;
                objDesign.OverAllLength = txtOverAllLength.Text == "" ? null : txtOverAllLength.Text;

                objDesign.ListOfDeviation = txtListOfDeviation.Text;

                //    objDesign.ItemID = Convert.ToInt32(ddlItemName.SelectedValue);

                if (fDeviationAttachement.HasFile)
                {
                    string deviation = Path.GetExtension(fDeviationAttachement.PostedFile.FileName).ToUpper();
                    FileName = Path.GetFileName(fDeviationAttachement.PostedFile.FileName);

                    extension = FileName.Split('.');
                    DeviationName = extension[0].ToString() + '_' + ddlEnquiryNumber.SelectedValue + '_' + ViewState["EDID"].ToString() + '_' + lblVersionNumber_A.Text + '.' + extension[1];
                    objDesign.DeviationAttachementName = DeviationName;
                }
                else
                    objDesign.DeviationAttachementName = null;

                // objDesign.VersionNumber = Convert.ToInt32(ViewState["NewDrawingVersionNumber"].ToString());

                objDesign.VersionNumber = Convert.ToInt32(lblVersionNumber_A.Text);
                objDesign.Remarks = txtRemarks_A.Text;

                objDesign.UserID = Convert.ToInt32(objSession.employeeid);

                ////  objDesign.Description = txtDescription.Text;
                /////  objDesign.Attachementtype = Convert.ToInt32(ddlTypeName.SelectedValue);

                ds = objDesign.SaveDesignDocumentDetails();

                if (ds.Tables[0].Rows[0]["AttachementID"].ToString() != "")
                {
                    hdnDDID.Value = ds.Tables[1].Rows[0]["DDID"].ToString();
                    //  SaveAttachements(hdnDDID.Value);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Design Details Saved Successfully');ShowAddPopUp();", true);
                }

                path = DrawingDocumentSavePath + ddlEnquiryNumber.SelectedValue + "\\";

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                fAttachment.SaveAs(path + AttachmentName);
                if (!string.IsNullOrEmpty(DeviationName))
                    fDeviationAttachement.SaveAs(path + DeviationName);

                //Send Individual Mail           

                txtRemarks_A.Text = "";
                ddlEnquiryNumber_SelectIndexChanged(null, null);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btndownloaddocs_Click(object sender, EventArgs e)
    {
        cCommon.DownLoad(ViewState["FileName"].ToString(), ViewState["ifrmsrc"].ToString());
    }

    #endregion

    #region"GridView Events"

    protected void gvDesignDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string extension = "";
        // string BasehttpPath = DrawingDocumentSavePath + ddlEnquiryNumber.SelectedValue + "\\";
        string BasehttpPath = DrawingDocumentHttpPath + ddlEnquiryNumber.SelectedValue + "/";
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgbtn = (ImageButton)e.Row.FindControl("imgbtnView");
                LinkButton lbtnAdd = (LinkButton)e.Row.FindControl("lbtnNew");
                CheckBox chkitem = (CheckBox)e.Row.FindControl("chkitems");

                if (dr["SharedWithHOD"].ToString() == "9" || dr["SharedWithHOD"].ToString() == "0")
                    chkitem.Visible = true;
                else if (dr["ApprovalStatus"].ToString() == "1" || dr["SharedWithHOD"].ToString() == "1")
                    chkitem.Visible = false;

                if (!string.IsNullOrEmpty(dr["FileName"].ToString()))
                {
                    extension = dr["FileName"].ToString().Split('.')[1].ToUpper();
                    // byte[] imageBytes = System.IO.File.ReadAllBytes(BasehttpPath + dr["FileName"].ToString());

                    //    string base64String = Convert.ToBase64String(imageBytes);
                    //  imgbtn.ImageUrl = String.Format(@"data:image/jpeg;base64,{0}", base64String);
                    imgbtn.ImageUrl = BasehttpPath + dr["FileName"].ToString();
                    if (File.Exists(DrawingDocumentSavePath + ddlEnquiryNumber.SelectedValue + "\\" + dr["FileName"].ToString()))
                    {
                        imgbtn.ToolTip = dr["FileName"].ToString();
                    }
                    else
                    {
                        imgbtn.ImageUrl = "../Assets/images/NoImage.png";
                    }
                }
                else
                    imgbtn.Visible = false;
                if (string.IsNullOrEmpty(dr["Version"].ToString()))
                {
                    Label lblversion = (Label)gvDesignDetails.FindControl("lblVersionNumber");
                    lblversion.Text = "0";
                }

                if (dr["SharedWithCustomer"].ToString() == "Yes")
                {
                    //lbtnAdd.Enabled = false;
                    lbtnAdd.ToolTip = "Waiting for Customer Drawing Approval";
                }
                //else
                // lbtnAdd.Enabled = true;

                //if (dr["SharedWithSales"].ToString() == "No")
                //{
                //    btnSaveAndShare.CssClass = "btn btn-success";
                //}
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

    }

    protected void gvRevisedList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string extension = "";
        string BasehttpPath = DrawingDocumentHttpPath + ddlEnquiryNumber.SelectedValue + "/";
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton imgbtn = (ImageButton)e.Row.FindControl("imgbtnView");

                if (!string.IsNullOrEmpty(dr["FileName"].ToString()))
                {
                    extension = dr["FileName"].ToString().Split('.')[1].ToUpper();
                    imgbtn.ImageUrl = BasehttpPath + dr["FileName"].ToString();
                    if (File.Exists(DrawingDocumentSavePath + ddlEnquiryNumber.SelectedValue + "\\" + dr["FileName"].ToString()))
                        imgbtn.ToolTip = dr["FileName"].ToString();
                    else
                        imgbtn.ImageUrl = "../Assets/images/NoImage.png";
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvDesignDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "ViewDocs")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                objc = new cCommon();
                //  string BasehttpPath = DrawingDocumentSavePath + ddlEnquiryNumber.SelectedValue + "\\";

                string BasehttpPath = DrawingDocumentHttpPath + ddlEnquiryNumber.SelectedValue + "/";

                string FileName = gvDesignDetails.DataKeys[index].Values[0].ToString();

                ViewState["FileName"] = FileName;
                //   byte[] imageBytes = System.IO.File.ReadAllBytes(BasehttpPath + FileName);
                // string base64String = Convert.ToBase64String(imageBytes);
                //   imgDocs.ImageUrl = String.Format(@"data:image/jpeg;base64,{0}", base64String);

                //  imgDocs.ImageUrl = BasehttpPath + FileName;

                ifrm.Attributes.Add("src", BasehttpPath + FileName);

                objc.ViewFileName(DrawingDocumentSavePath, DrawingDocumentHttpPath, FileName, ddlEnquiryNumber.SelectedValue.ToString(), ifrm);

                //if (File.Exists(DrawingDocumentSavePath + ddlEnquiryNumber.SelectedValue + "\\" + FileName))
                //{
                //    ViewState["ifrmsrc"] = BasehttpPath + FileName;
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewPopUp();", true);
                //}
                //else
                //{
                //    //imgDocs.ImageUrl = "";
                //    ifrm.Attributes.Add("src", "");
                //    ViewState["ifrmsrc"] = "";
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Attachements Not Found');", true);
                //}

                // divContent.InnerHtml = "<iframe src='http://docs.google.com/gview?url=" + BasehttpPath + FileName + "&embedded=true' style='width:100%; height:100%;' frameborder='0' id='frame1'></iframe>";

                // cCommon.DownLoad(FileName, BasehttpPath + FileName);
            }

            if (e.CommandName == "AddDocs")
            {
                DataSet ds = new DataSet();
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                ViewState["EDID"] = Convert.ToInt32(gvDesignDetails.DataKeys[index].Values[1].ToString());
                Label lblRevNo = ((Label)gvDesignDetails.Rows[index].FindControl("lblVersionNumber"));
                lblVersionNumber_A.Text = lblRevNo.Text;
                int DDID = Convert.ToInt32(gvDesignDetails.DataKeys[index].Values[4].ToString());

                hdnDDID.Value = gvDesignDetails.DataKeys[index].Values[4].ToString();

                string ApprovalStatus = gvDesignDetails.DataKeys[index].Values[6].ToString();
                string SharedwithHOD = gvDesignDetails.DataKeys[index].Values[7].ToString();

                if (ApprovalStatus == "1" || SharedwithHOD == "1")
                    btnSaveDrawing.Visible = false;
                else if (SharedwithHOD == "9" || SharedwithHOD == "0")
                    btnSaveDrawing.Visible = true;

                if (objSession.type == 1)
                    btnSaveDrawing.Visible = true;

                string TagNo = gvDesignDetails.DataKeys[index].Values[5].ToString();

                if (DDID != 0)
                {
                    objDesign.DDID = DDID;
                    ds = objDesign.GetDesignDetailsByDDID();

                    lblVersionNumber_A.Text = ds.Tables[0].Rows[0]["Version"].ToString();
                    txtDrawingName.Text = ds.Tables[0].Rows[0]["DrawingName"].ToString();
                    txtDesignNumber.Text = ds.Tables[0].Rows[0]["DesignNumber"].ToString();
                    txtDesignCode.Text = ds.Tables[0].Rows[0]["DesignCode"].ToString();
                    txtTagNumber.Text = ds.Tables[0].Rows[0]["TagNo"].ToString();
                    txtOverAllLength.Text = ds.Tables[0].Rows[0]["overAllLength"].ToString();
                    txtListOfDeviation.Text = ds.Tables[0].Rows[0]["ListOfDeviation"].ToString();
                    chkBOMCostApprove.Checked = ds.Tables[0].Rows[0]["BOMCostApprove"].ToString() == "1" ? true : false;
                    txtRemarks_A.Text = ds.Tables[0].Rows[0]["remarks"].ToString();
                }

                Label lblItemName = (Label)gvDesignDetails.Rows[index].FindControl("lblItemName");

                lblEnquiryNumber_A.Text = ddlEnquiryNumber.SelectedItem.Text + "/" + lblItemName.Text + "/" + TagNo;

                ViewState["ItemName"] = lblItemName.Text;

                // 23-july-20   BindAttachements(DDID.ToString());

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ShowAddPopUp();", true);
            }

            if (e.CommandName.ToString() == "ReviewDrawing")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                DataSet ds = new DataSet();

                //ds = objDesign.ReviewDrawingByDDID();

            }

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvRevisedList_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "ViewDocs")
            {
                objc = new cCommon();
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                //  string BasehttpPath = DrawingDocumentSavePath + ddlEnquiryNumber.SelectedValue + "\\";

                string BasehttpPath = DrawingDocumentHttpPath + ddlEnquiryNumber.SelectedValue + "/";

                string FileName = gvRevisedList.DataKeys[index].Values[0].ToString();
                ViewState["FileName"] = FileName;
                //   byte[] imageBytes = System.IO.File.ReadAllBytes(BasehttpPath + FileName);
                // string base64String = Convert.ToBase64String(imageBytes);
                //   imgDocs.ImageUrl = String.Format(@"data:image/jpeg;base64,{0}", base64String);

                objc.ViewFileName(DrawingDocumentSavePath, DrawingDocumentHttpPath, FileName, ddlEnquiryNumber.SelectedValue.ToString(), ifrm);

                //ifrm.Attributes.Add("src", BasehttpPath + FileName);
                //if (File.Exists(DrawingDocumentSavePath + ddlEnquiryNumber.SelectedValue + "\\" + FileName))
                //{
                //    ViewState["ifrmsrc"] = BasehttpPath + FileName;
                //    ViewState["ifrmsrc"] = "";
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewPopUp();", true);
                //}
                //else
                //{
                //    ifrm.Attributes.Add("src", "");
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Drawing Not Found');", true);
                //}
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    //protected void gvAttachments_OnRowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    objc = new cCommon();
    //    string FileName = "";
    //    try
    //    {
    //        int index = Convert.ToInt32(e.CommandArgument.ToString());
    //        if (e.CommandName == "ViewDocs")
    //        {
    //            FileName = gvAttachments.DataKeys[index].Values[0].ToString();
    //            ViewState["FileName"] = FileName;
    //            objc.ViewFileName(DrawingDocumentSavePath, DrawingDocumentHttpPath, FileName, ddlEnquiryNumber.SelectedValue, ifrm);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    #endregion

    #region "Common Methods"

    private void ddlenquiryload()
    {
        objc = new cCommon();
        try
        {
            DataSet dsEnquiryNumber = new DataSet();
            DataSet dsCustomer = new DataSet();

            dsCustomer = objc.GetCustomerNameByPendingList(Convert.ToInt32(objSession.employeeid), ddlCustomerName, "LS_GetCustomerNameByDrawingUpload", rblEnquiryChange.SelectedValue);
            ViewState["CustomerDetails"] = dsCustomer.Tables[0];
            dsEnquiryNumber = objc.GetEnquiryNumberByPendingList(Convert.ToInt32(objSession.employeeid), ddlEnquiryNumber, "LS_GetEnquiryIDByDrawingUpload", rblEnquiryChange.SelectedValue);
            ViewState["EnquiryDetails"] = dsEnquiryNumber.Tables[0];
        }
        catch (Exception ec)
        {
            Log.Message(ec.ToString());
        }
    }

    //private void SaveAttachements(string DDID)
    //{
    //    objSales = new cSales();
    //    objDesign = new cDesign();
    //    DataSet ds = new DataSet();
    //    string AttachmentName = "";
    //    try
    //    {

    //        objDesign.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue);
    //        objDesign.Attachementtype = Convert.ToInt32(ddlTypeName.SelectedValue);
    //        objDesign.Description = txtDescription.Text;
    //        objDesign.DDID = Convert.ToInt32(DDID);
    //        //objcommon.primarykey = "DDID";
    //        //objcommon.table = "LSE_DesignDocuments";

    //        string MaxAttachementId = objSales.GetMaximumAttachementID();

    //        if (fMultiAttach.HasFile)
    //        {
    //            objc = new cCommon();
    //            AttachmentName = Path.GetFileName(fAttachment.PostedFile.FileName);
    //            string[] extension = AttachmentName.Split('.');
    //            AttachmentName = extension[0] + '_' + MaxAttachementId + '.' + extension[1];

    //            objc.Foldername = DrawingDocumentSavePath;
    //            objc.FileName = AttachmentName;
    //            objc.PID = ddlEnquiryNumber.SelectedValue;
    //            objc.AttachementControl = fMultiAttach;
    //            objc.SaveFiles();
    //        }

    //        objDesign.AttachementName = AttachmentName == "" ? null : AttachmentName;


    //        ds = objDesign.saveDesignMultiAttachementDetails();

    //        BindAttachements(DDID);
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Error occured');", true);
    //        Log.Message(ex.ToString());
    //    }
    //}

    //private void BindAttachements(string DDID)
    //{
    //    try
    //    {
    //        objDesign = new cDesign();
    //        DataSet ds = new DataSet();

    //        objDesign.DDID = Convert.ToInt32(DDID);
    //        //objcommon.primarykey = "DDID";
    //        //objcommon.table = "LSE_DesignDocuments";

    //        ds = objDesign.GetAttachementsDetailsByDDID();

    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            ViewState["Attachement"] = ds.Tables[0];

    //            gvAttachments.DataSource = ds.Tables[0];
    //            gvAttachments.DataBind();
    //        }
    //        else
    //        {
    //            gvAttachments.DataSource = "";
    //            gvAttachments.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    private bool ValidateFields()
    {
        string error = "";
        bool isvalid = true;
        string extn = Path.GetExtension(fAttachment.PostedFile.FileName).ToUpper();
        try
        {
            if (fAttachment.HasFile == false)
                error = "Please Upload File";
            else if (txtDrawingName.Text == "")
                error = "Please Enter Drawing Name";
            else if (txtDesignNumber.Text == "")
                error = "Please Enter Design Number";
            if (error != "")
            {
                isvalid = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Field Required " + error + "');ShowAddPopUp();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return isvalid;
    }

    private void UpdateSharedWithHODStatus()
    {
        DataSet ds = new DataSet();
        objDesign = new cDesign();
        try
        {
            for (int i = 0; i < gvDesignDetails.Rows.Count; i++)
            {
                //Label lblVersion = (Label)gvDesignDetails.Rows[i].FindControl("lblVersionNumber");
                string Version = gvDesignDetails.DataKeys[i].Values[2].ToString();
                CheckBox chk = (CheckBox)gvDesignDetails.Rows[i].FindControl("chkitems");
                if (chk.Checked)
                {
                    objDesign.VersionNumber = Convert.ToInt32(Version);
                    objDesign.EDID = Convert.ToInt32(gvDesignDetails.DataKeys[i].Values[1].ToString());
                    ds = objDesign.UpdateSharedWithHODStatus();
                }
            }

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Drawing Shared Successfully');", true);
                SaveAlertDetails();
            }

            ddlEnquiryNumber_SelectIndexChanged(null, null);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error Occured');", true);
            Log.Message(ex.ToString());
        }
    }

    public void SaveAlertDetails()
    {
        objAlerts = new EmailAndSmsAlerts();
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
            objAlerts.GroupID = 10;
            objAlerts.Subject = "Drawing Approval Alert";
            objAlerts.Message = "Drawing Approval Request From Enquiry Number " + ddlEnquiryNumber.SelectedValue;
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
        if (gvDesignDetails.Rows.Count > 0)
            gvDesignDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (gvRevisedList.Rows.Count > 0)
            gvRevisedList.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion

}