using eplus.core;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_AddWorkOrderIndentDetails : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cCommon objc;
    cProduction objP;
    cMaterials objMat;
    cSales objSales;

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
            if (IsPostBack == false)
            {
                BindPartDetailsByRFPDID();
                BindWorkOrderIndentDetailsByItemID();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlPartName_OnSelectedChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlPartName.SelectedIndex > 0)
                BindPartDetailsByMPID();
            else
            {
                gvPartSno.DataSource = "";
                gvPartSno.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnSaveWOI_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        string AttachmentName = "";
        string PRPDIDs = "";
        string JobListID = "";
        try
        {
            objP.WOIHID = Convert.ToInt32(hdnWOIHID.Value);
            objP.MPID = Convert.ToInt32(ddlPartName.SelectedValue);
            objP.jobDescription = txtJobDescription.Text;
            objP.Remarks = txtremarks.Text;
            objP.RawMaterialQuantity = Convert.ToInt32(txtRawmaterialQuantity.Text);
            objP.RFPDID = Convert.ToInt32(Session["RFPDID"].ToString());
            objP.RFPHID = Convert.ToInt32(Session["RFPHID"].ToString());

            if (fAttachment.HasFile)
            {
                string extn = Path.GetExtension(fAttachment.PostedFile.FileName).ToUpper();

                AttachmentName = Path.GetFileName(fAttachment.PostedFile.FileName);
                string[] extension = AttachmentName.Split('.');
                AttachmentName = extension[0] + '_' + "WODrawing" + '.' + extension[1];
            }
            objP.AttachementName = AttachmentName;

            foreach (GridViewRow row in gvPartSno.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkQC");
                if (chk.Checked)
                {
                    if (PRPDIDs == "")
                        PRPDIDs = gvPartSno.DataKeys[row.RowIndex].Values[0].ToString();
                    else
                        PRPDIDs = PRPDIDs + "," + gvPartSno.DataKeys[row.RowIndex].Values[0].ToString();
                }
            }

            foreach (ListItem li in LiJobOperationList.Items)
            {
                if (li.Selected)
                {
                    if (JobListID == "")
                        JobListID = li.Value;
                    else if (JobListID != "")
                        JobListID = JobListID + ',' + li.Value;
                }
            }
            objP.CreatedBy = Convert.ToInt32(objSession.employeeid);
            objP.WOjobListID = JobListID;
            objP.PRPDIDs = PRPDIDs;

            ds = objP.SaveWorkOrderIndentDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Indent Saved Successfully');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvWorkOrderIndentDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "EditWO")
            {
                hdnWOIHID.Value = gvWorkOrderIndentDetails.DataKeys[index].Values[0].ToString();
                EditWorkOrderIndentDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindPartDetailsByRFPDID()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.RFPDID = Convert.ToInt32(Session["RFPDID"]);
            objP.GetPartDetailsByRFPDID(ddlPartName, LiJobOperationList);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindWorkOrderIndentDetailsByItemID()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.RFPDID = Convert.ToInt32(Session["RFPDID"]);
            ds = objP.GetWorkOrderIndentDetailsByRFPDID();

            gvWorkOrderIndentDetails.DataSource = ds.Tables[0];
            gvWorkOrderIndentDetails.DataBind();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindPartDetailsByMPID()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.MPID = Convert.ToInt32(ddlPartName.SelectedValue);
            objP.WOIHID = Convert.ToInt32(hdnWOIHID.Value);
            ds = objP.GetPartSnoByMPID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvPartSno.DataSource = ds.Tables[0];
                gvPartSno.DataBind();
            }
            else
            {
                gvPartSno.DataSource = "";
                gvPartSno.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void EditWorkOrderIndentDetails()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.WOIHID = Convert.ToInt32(hdnWOIHID.Value);
            ds = objP.GetWorkOrderIndentDetailsByWOIHID();

            ddlPartName.SelectedValue = ds.Tables[0].Rows[0]["MPID"].ToString();
            BindPartDetailsByMPID();


        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}