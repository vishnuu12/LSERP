using eplus.core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_ItemSpecificationDetails : System.Web.UI.Page
{
    #region"Declaration"

    cSession _objSess = new cSession();
    cCommonMaster objCommon;
    cCommon objc;
    cDesign objDesign;
    string DrawingDocumentSavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString();
    string DrawingDocumentHttpPath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString().Trim();

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
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (IsPostBack == false)
            {
                objc = new cCommon();
                DataSet dsEnquiryNumber = new DataSet();
                DataSet dsCustomer = new DataSet();
                dsCustomer = objc.getCustomerNameByUserID(Convert.ToInt32(_objSess.employeeid), ddlCustomerName, "LS_GetCustomerNameByEmployeeID");
                ViewState["CustomerDetails"] = dsCustomer.Tables[1];
                dsEnquiryNumber = objc.GetEnquiryNumberByUserID(Convert.ToInt32(_objSess.employeeid), ddlEnquiryNumber, "LS_GetEnquiryIDByUserID");
                ViewState["EnquiryDetails"] = dsEnquiryNumber.Tables[0];
                ShowHideControls("add");
            }
            else
            {
                if (target == "ShareFile")
                {
                    ShareProcessStatus();
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
        objc = new cCommon();
        try
        {
            objc.customerddlchnage(ddlCustomerName, ddlEnquiryNumber, (DataTable)ViewState["CustomerDetails"], (DataTable)ViewState["EnquiryDetails"]);
            ddlEnquiryNumber_SelectIndexChanged(this, EventArgs.Empty);
            ShowHideControls("add");
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
        objc = new cCommon();
        try
        {
            if (ddlEnquiryNumber.SelectedIndex > 0)
            {
                ViewState["EnquiryNumber"] = ddlEnquiryNumber.SelectedValue;
                objc.enquiryddlchange(ddlEnquiryNumber, ddlCustomerName, (DataTable)ViewState["CustomerDetails"], (DataTable)ViewState["EnquiryDetails"]);

                BindSpecificationDetails("Design");
                ShowHideControls("add,view");
            }
            else
                ShowHideControls("add");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objDesign = new cDesign();
        try
        {
            string EDID = "";
            string FileName = "";
            string AttachmentName = "";
            string[] extension;
            string path;
            if (fpAttach.HasFile)
            {
                foreach (GridViewRow row in gvItemnamelist.Rows)
                {
                    CheckBox chk = (CheckBox)row.FindControl("chkitems");
                    if (chk.Checked)
                    {
                        if (EDID == "")
                            EDID = gvItemnamelist.DataKeys[row.RowIndex].Values[0].ToString();
                        else
                            EDID = EDID + ',' + gvItemnamelist.DataKeys[row.RowIndex].Values[0].ToString();
                    }
                }

                string extn = Path.GetExtension(fpAttach.PostedFile.FileName).ToUpper();
                FileName = Path.GetFileName(fpAttach.PostedFile.FileName);

                extension = FileName.Split('.');
                AttachmentName = extension[0].ToString() + '_' + hdnTabName.Value + "Docs" + '_' + '.' + extension[1];

                path = DrawingDocumentSavePath + ddlEnquiryNumber.SelectedValue + "\\";

                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                fpAttach.SaveAs(path + AttachmentName);

                objDesign.AttachementName = AttachmentName;
                objDesign.tabname = hdnTabName.Value;
                objDesign.UserID = Convert.ToInt32(_objSess.employeeid);
                ds = objDesign.SaveItemSpecsDocsDetails(EDID);

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "add", "SuccessMessage('Success','Docs Saved Successfully');", true);
                BindSpecificationDetails(hdnTabName.Value);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "add", "ErrorMessage('Error','Upload Docs');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tabs", "ActiveTab('" + hdnTabName.Value + "');", true);
        }
    }

    protected void btntab_Click(object sender, EventArgs e)
    {
        try
        {
            BindSpecificationDetails(hdnTabName.Value);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common methods"

    private void BindSpecificationDetails(string tabname)
    {
        DataSet ds = new DataSet();
        objDesign = new cDesign();
        try
        {
            objDesign.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue);
            objDesign.tabname = tabname;
            objDesign.UserID = Convert.ToInt32(_objSess.employeeid);
            ds = objDesign.GetItemDetailsAndItemSpecificationDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvItemnamelist.DataSource = ds.Tables[0];
                gvItemnamelist.DataBind();
            }
            else
            {
                gvItemnamelist.DataSource = "";
                gvItemnamelist.DataBind();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                if (Convert.ToInt32(_objSess.employeeid) == Convert.ToInt32(ds.Tables[1].Rows[0]["btnvisible"].ToString()))
                {
                    btnShareFile.Visible = true;
                    btnSave.Visible = true;
                }
                else
                {
                    btnShareFile.Visible = false;
                    btnSave.Visible = false;
                }
            }
            else
            {
                btnShareFile.Visible = false;
                btnSave.Visible = false;
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                gvItemSpecDetails.DataSource = ds.Tables[2];
                gvItemSpecDetails.DataBind();
            }
            else
            {
                gvItemSpecDetails.DataSource = "";
                gvItemSpecDetails.DataBind();
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "tabs", "ActiveTab('" + tabname + "');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "tabs", "ActiveTab('" + tabname + "');", true);
        }
    }

    private void ShareProcessStatus()
    {
        DataSet ds = new DataSet();
        objDesign = new cDesign();
        try
        {
            objDesign.tabname = hdnTabName.Value;
            objDesign.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue);

            if (gvItemSpecDetails.Rows.Count > 0)
                ds = objDesign.UpdateProcessStatusByEnquiryNumberAndItemID();
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "add", "ErrorMessage('Error','No Records');", true);

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "add", "SuccessMessage('Success','Process Shared Successfully');", true);
            ddlEnquiryNumber_SelectIndexChanged(null, null);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ShowHideControls(string divids)
    {
        divAdd.Visible = divOutput.Visible = false;
        string[] mode = divids.Split(',');
        try
        {
            for (int i = 0; i < mode.Length; i++)
            {
                switch (mode[i].ToLower())
                {
                    case "add":
                        divAdd.Visible = true;
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

    #endregion

}