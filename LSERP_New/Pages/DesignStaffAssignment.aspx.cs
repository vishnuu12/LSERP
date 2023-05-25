using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using eplus.core;
using System.Configuration;
using System.IO;
using eplus.data;

public partial class Pages_DesignStaffAssignment : System.Web.UI.Page
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
                ViewState["EnquiryID"] = txtEmployeeID.Text.Split('/')[0].ToString();
                BindStaffAssignmentEnquiryDetails();
                AssignModuleName();
                divauto.Visible = false;
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

    #region"Radio Events"

    protected void rblEnquirychange_OnSelectedChanged(object sender, EventArgs e)
    {
        if (rblEnquirychange.SelectedValue == "0")
        {
            divOutput.Visible = true;
            divauto.Visible = false;

            BindStaffAssignmentEnquiryDetails();
        }
        else
        {
            divauto.Visible = true;
            divOutput.Visible = false;
        }
        txtEmployeeID.Text = "";
    }

    #endregion

    #region"Button Events"

    protected void btnget_click(object sender, EventArgs e)
    {
        try
        {
            ViewState["EnquiryID"] = txtEmployeeID.Text.Split('/')[0].ToString();
            BindStaffAssignmentEnquiryDetails();
            divOutput.Visible = true;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSaveStaff_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objCommon = new cCommonMaster();
        try
        {
            objCommon.SAID = Convert.ToInt32(hdnSAID.Value);
            objCommon.EmployeeIds = hdnEmployeeIDs.Value;
            objCommon.DesignerDeadLineDate = DateTime.ParseExact(txtDeadLineDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objCommon.CreatedBy = _objSess.employeeid;
            objCommon.EnquiryID = Convert.ToInt32(hdnEnquiryID.Value);

            ds = objCommon.UpdateStaffAllocationDesign("LS_SaveDesignStaffAssignmentDetails");

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myFunction", "SuccessMessage('Success !','Staff Assigned Successfully');CloseStaffPopUp();", true);
                BindStaffAssignmentEnquiryDetails();
                SaveAlertDetails(hdnEnquiryID.Value, hdnEmployeeIDs.Value);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myFunction", "InfoMessage('Information !','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');CloseStaffPopUp();", true);
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
        string error = "";
        bool designerdeadline = true;
        try
        {
            DropDownList ddl = ((DropDownList)gvStaffAssignmentDetails.Rows[e.RowIndex].FindControl("ddlEmployeeName"));
            TextBox txt = ((TextBox)gvStaffAssignmentDetails.Rows[e.RowIndex].FindControl("txtDesignerDeadLineDate"));

            if (ddl.SelectedIndex != 0 && (txt.Text != "" || _objSess.type == 5))
            {
                objCommon.SAID = Convert.ToInt32(gvStaffAssignmentDetails.DataKeys[e.RowIndex].Values[0].ToString());
                objCommon.EnquiryID = Convert.ToInt32(gvStaffAssignmentDetails.DataKeys[e.RowIndex].Values[1].ToString().Split('/')[0]);
                objCommon.EmployeeID = Convert.ToInt32(((DropDownList)gvStaffAssignmentDetails.Rows[e.RowIndex].FindControl("ddlEmployeeName")).SelectedValue);
                if (_objSess.type != 5) objCommon.DesignerDeadLineDate = DateTime.ParseExact((((TextBox)gvStaffAssignmentDetails.Rows[e.RowIndex].FindControl("txtDesignerDeadLineDate")).Text), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                else designerdeadline = false;
                objCommon.DepartmentID = Convert.ToInt32(ViewState["DepartmentID"].ToString());
                objCommon.CreatedBy = _objSess.employeeid;
                ds = objCommon.UpdateStaffAssignMentDetails(designerdeadline);

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myFunction", "SuccessMessage('Success !','Records Updated Successfully');", true);
                    SaveAlertDetails(objCommon.EnquiryID.ToString(), objCommon.EmployeeID.ToString());
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myFunction", "InfoMessage('InforMation !','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);

                gvStaffAssignmentDetails.EditIndex = -1;
                BindStaffAssignmentEnquiryDetails();
            }

            else
            {
                if (ddl.SelectedIndex == 0)
                {
                    error = ddl.ClientID + '/' + "Field Required";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Employee Name Required!');", true);
                }
                else if (txt.Text == "")
                {
                    error = txt.ClientID + '/' + "Field Required";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Designer Deadline Date Required!');", true);
                }

            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvStaffAssignmentDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //    ("DesignerDeadLineDate");
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (_objSess.type == 5) e.Row.Cells[7].Attributes.Add("style", "display:none;");
                else if (_objSess.type == 3)
                {
                    // e.Row.Cells[5].Attributes.Add("style", "display:none;");
                    ///e.Row.Cells[9].Attributes.Add("style", "display:none;");
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (_objSess.type == 5) e.Row.Cells[7].Attributes.Add("style", "display:none;");
                else if (_objSess.type == 3)
                {
                    //  e.Row.Cells[5].Attributes.Add("style", "display:none;");
                    //  e.Row.Cells[9].Attributes.Add("style", "display:none;");
                }
                DataRowView dr = (DataRowView)e.Row.DataItem;
                e.Row.Cells[8].ToolTip = "Edit";
                if (e.Row.RowState == DataControlRowState.Edit || e.Row.RowState.ToString() == "Alternate, Edit")
                {
                    foreach (TableCell cell in e.Row.Cells)
                    {
                        if (e.Row.Cells.GetCellIndex(cell) == 10)
                        {
                            ((System.Web.UI.WebControls.ImageButton)(e.Row.Cells[10].Controls[0])).ToolTip = "Update";
                            ((System.Web.UI.LiteralControl)(e.Row.Cells[10].Controls[1])).Text = "&nbsp;";
                            ((System.Web.UI.WebControls.ImageButton)(e.Row.Cells[10].Controls[2])).ToolTip = "Close";
                        }
                    }
                }
                if (dr["Staff"].ToString() == "Not Assigned")
                    e.Row.Attributes.Add("style", "Background-color:#ffd400");

                if (dr["Budgetaryoffer"].ToString() == "1")
                {
                    Label lbl = new Label();
                    lbl.Text = "Budgetary";
                    lbl.Attributes.Add("class", "blinking budgetaryhighligt");
                    e.Row.Cells[0].Controls.Add(lbl);
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvStaffAssignmentDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
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

            if (e.CommandName == "ViewEnqAttachements")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                ViewState["EnquiryID"] = gvStaffAssignmentDetails.DataKeys[index].Values[1].ToString().Split('/')[0].ToString();
                bindEnquiryAttachementDetails();
            }
            if (e.CommandName == "addstaff")
            {
                int index = Convert.ToInt32(e.CommandArgument.ToString());
                cDesign objD = new cDesign();

                DataSet ds = new DataSet();
                Label lblRFPNo = (Label)gvStaffAssignmentDetails.Rows[index].FindControl("lblEnquiryNumber");

                ViewState["AlertRFPNo"] = lblRFPNo.Text;

                lblRFPNumber_staff.Text = lblRFPNo.Text;

                hdnSAID.Value = gvStaffAssignmentDetails.DataKeys[index].Values[0].ToString();
                hdnEnquiryID.Value = gvStaffAssignmentDetails.DataKeys[index].Values[1].ToString();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowStaffPopUp('" + ViewState["EmployeeListByDept"].ToString() + "');", true);
            }
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
                string BasehttpPath = CustomerEnquiryHttpPath + ViewState["EnquiryID"].ToString() + "/";
                string FileName = ((Label)gvAttachments.Rows[index].FindControl("lblFileName_V")).Text.ToString();
                ViewState["FileName"] = FileName;
                ifrm.Attributes.Add("src", BasehttpPath + FileName);
                string imgname = CusstomerEnquirySavePath + ViewState["EnquiryID"].ToString() + "\\" + FileName;
                if (File.Exists(imgname))
                {
                    //ViewState["ifrmsrc"] = imgname;
                    ViewState["ifrmsrc"] = BasehttpPath + FileName;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewdocsPopUp();", true);
                }
                else
                {
                    ifrm.Attributes.Add("src", "");
                    ViewState["ifrmsrc"] = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Drawing Not Found');", true);
                }
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
            //string BasehttpPath = CusstomerEnquirySavePath + ViewState["EnquiryNumber"].ToString() + "\\";
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

    public void SaveAlertDetails(string EnquiryID, string EmployeeID)
    {
        EmailAndSmsAlerts objAlerts = new EmailAndSmsAlerts();
        DataSet ds = new DataSet();
        cCommon objc = new cCommon();
        try
        {
            string[] strempid = EmployeeID.Split(',');

            for (int i = 0; i < strempid.Length; i++)
            {
                objAlerts.EntryMode = "Individual";
                objAlerts.AlertType = "Mail";
                objAlerts.userID = _objSess.employeeid;
                objAlerts.reciverType = "Staff";
                objAlerts.file = "";
                objAlerts.reciverID = strempid[i].ToString();
                objAlerts.EmailID = "";
                objAlerts.GroupID = 0;
                objAlerts.Subject = "Enquiry Allotted";
                objAlerts.Message = "You Have New Enquiry Allotted " + EnquiryID;
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
            if (rblEnquirychange.SelectedValue == "1")
                objCommon.EnquiryID = Convert.ToInt32(ViewState["EnquiryID"].ToString());
            else
                objCommon.EnquiryID = 0;

            objCommon.EmployeeID = Convert.ToInt32(_objSess.employeeid);
            objCommon.status = rblEnquirychange.SelectedValue;

            ds = objCommon.GetDesignStaffAssignmentEnquiryDetails();

            ViewState["EmployeeListByDept"] = ds.Tables[1].Rows[0]["EmpName"].ToString();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (rblEnquirychange.SelectedValue == "1")
                {
                    IEnumerable<DataRow> row = (from r in ds.Tables[0].AsEnumerable()
                                                where (r.Field<int>("EnquiryID") == Convert.ToInt32(ViewState["EnquiryID"].ToString()))
                                                select r);
                    if (row.Count() > 0)
                    {
                        DataTable dt = new DataTable();
                        dt = row.CopyToDataTable();

                        gvStaffAssignmentDetails.DataSource = dt;
                        gvStaffAssignmentDetails.DataBind();
                    }
                    else
                    {
                        gvStaffAssignmentDetails.DataSource = "";
                        gvStaffAssignmentDetails.DataBind();
                    }
                }
                else
                {
                    gvStaffAssignmentDetails.DataSource = ds.Tables[0];
                    gvStaffAssignmentDetails.DataBind();
                }
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

    private void AssignModuleName()
    {
        DataTable dt;
        try
        {
            dt = (DataTable)Session["UserModules"];
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["MID"].ToString() == "11")
                    lblModuleName.Text = "Sales";
                else if (dr["MID"].ToString() == "4")
                    lblModuleName.Text = "Design";
                else if (dr["MID"].ToString() == "6")
                    lblModuleName.Text = "Production";
                else if (dr["MID"].ToString() == "7")
                    lblModuleName.Text = "Procurement";
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindEnquiryAttachementDetails()
    {
        cSales objSales = new cSales();
        DataSet dsGetAttachementsDetails = new DataSet();
        dsGetAttachementsDetails = objSales.GetEnquiryprocessDetails(ViewState["EnquiryID"].ToString(), "LS_GetAttachementsDetails", false);
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

    #endregion

    #region"Web Methods"

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> GetEmployee(string prefixText)
    {
        List<string> EmployeeName = new List<string>();
        try
        {
            cDataAccess DAL = new cDataAccess();
            DataSet ds = new DataSet();

            DAL = new cDataAccess();

            string[] paramNames = { "@EnquiryID" };
            object[] paramValue = { prefixText };

            DAL.GetDataset("LS_GetAllEnquirySearch", paramValue, paramNames, ref ds);

            DataTable dt = new DataTable();

            dt = ds.Tables[0];

            if (dt.Rows.Count == 0)
                EmployeeName.Add("Record not available");
            else
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                    EmployeeName.Add(dt.Rows[i]["EnquiryNo"].ToString());
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return EmployeeName;
    }

    #endregion

    #region"PageLoad Complete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvStaffAssignmentDetails.Rows.Count > 0)
            gvStaffAssignmentDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion

}