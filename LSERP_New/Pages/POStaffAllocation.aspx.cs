using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;
using System.Globalization;
using System.Configuration;
using System.IO;

public partial class Pages_POStaffAllocation : System.Web.UI.Page
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
                objCommon.EnquiryID = Convert.ToInt32(gvStaffAssignmentDetails.DataKeys[e.RowIndex].Values[1].ToString().Split('/')[0]);
                objCommon.EmployeeID = Convert.ToInt32(((DropDownList)gvStaffAssignmentDetails.Rows[e.RowIndex].FindControl("ddlEmployeeName")).SelectedValue);
                objCommon.DepartmentID = Convert.ToInt32(ViewState["DepartmentID"].ToString());
                objCommon.PQDeadLineDate = true;

                objCommon.QualityAndProductionDeadLineDate = DateTime.ParseExact(txtDeadLineDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                ds = objCommon.UpdateStaffAssignMentDetails(false);

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
                e.Row.Cells[6].ToolTip = "Edit";
                if (e.Row.RowState == DataControlRowState.Edit || e.Row.RowState.ToString() == "Alternate, Edit")
                {
                    foreach (TableCell cell in e.Row.Cells)
                    {
                        if (e.Row.Cells.GetCellIndex(cell) == 6)
                        {
                            ((System.Web.UI.WebControls.ImageButton)(e.Row.Cells[6].Controls[0])).ToolTip = "Update";
                            ((System.Web.UI.LiteralControl)(e.Row.Cells[6].Controls[1])).Text = "&nbsp;";
                            ((System.Web.UI.WebControls.ImageButton)(e.Row.Cells[6].Controls[2])).ToolTip = "Close";
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
                string FileName = gvStaffAssignmentDetails.DataKeys[index].Values[3].ToString();
                ViewState["FileName"] = FileName;
                ifrm.Attributes.Add("src", BaseHtttpPath + FileName);

                if (File.Exists(CusstomerEnquirySavePath + EnquiryNumber + "//" + FileName))
                {
                    ViewState["ifrmsrc"] = BaseHtttpPath + FileName;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewdocsPopUp();", true);
                }
                else
                {
                    ifrm.Attributes.Add("src", "");
                    ViewState["ifrmsrc"] = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Po Copy Not Found');", true);
                }
            }
            if (e.CommandName == "ViewPoCopyWithoutPrice")
            {
                string FileName = gvStaffAssignmentDetails.DataKeys[index].Values[4].ToString();
                ViewState["FileName"] = FileName;
                ifrm.Attributes.Add("src", BaseHtttpPath + FileName);

                if (File.Exists(CusstomerEnquirySavePath + EnquiryNumber + "//" + FileName))
                {
                    ViewState["ifrmsrc"] = BaseHtttpPath + FileName;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewdocsPopUp();", true);
                }
                else
                {
                    ifrm.Attributes.Add("src", "");
                    ViewState["ifrmsrc"] = "";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Po copy Without Price Not Found');", true);
                }
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
        }
    }

    protected void gvAttachments_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        cSales objSales = new cSales();
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
                if (File.Exists(imgname))
                {
                    //ViewState["ifrmsrc"] = imgname;
                    ViewState["ifrmsrc"] = BasehttpPath + FileName;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "ShowViewdocsPopUp();HideViewPopUp();", true);
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
            Log.Message(ex.ToString());
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
        if (File.Exists(CusstomerEnquirySavePath + EnquiryNumber + '/' + DrawingName))
        {
            ViewState["ifrmsrc"] = FileName;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowViewdocsPopUp();", true);
        }
        else
        {
            ViewState["ifrmsrc"] = "";
            ifrm.Attributes.Add("src", FileName);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "InfoMessage('Information','Attach Not Found');", true);
        }
    }

    #endregion

    #region"Common Methods"

    private void BindStaffAssignmentEnquiryDetails()
    {
        objCommon = new cCommonMaster();
        DataSet ds = new DataSet();
        try
        {
            objCommon.EmployeeID = Convert.ToInt32(_objSess.employeeid);
            ds = objCommon.GetStaffAssignmentEnquiryDetails();
            ViewState["EmployeeListByDept"] = ds.Tables[1];
            ViewState["DepartmentID"] = ds.Tables[2].Rows[0]["DepartmentID"].ToString();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvStaffAssignmentDetails.DataSource = ds.Tables[0];
                gvStaffAssignmentDetails.DataBind();
                gvStaffAssignmentDetails.UseAccessibleHeader = true;
                gvStaffAssignmentDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "showDataTable();", true);
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


    #endregion
}