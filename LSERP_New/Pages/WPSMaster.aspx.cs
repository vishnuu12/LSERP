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

public partial class Pages_WPSMaster : System.Web.UI.Page
{

    #region"Declaration"

    cCommonMaster objCommon;
    cCommon objc;

    string WPSSavepath = ConfigurationManager.AppSettings["WPSSavepath"].ToString();
    string WPSHttpPath = ConfigurationManager.AppSettings["WPSHttpPath"].ToString();

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
                BindWPSDetails();
                ShowHideControls("view,add");
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
            }
            if (target == "ViewWPSAttach")
                ViewWPSFileName(Convert.ToInt32(arg.ToString()));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnWPS_Click(object sender, EventArgs e)
    {
        objCommon = new cCommonMaster();
        string AttachmentName = "";
        DataSet ds = new DataSet();
        bool msg = true;
        try
        {

            if (fAttachment.HasFile)
            {
                string extn = Path.GetExtension(fAttachment.PostedFile.FileName).ToUpper();
                AttachmentName = Path.GetFileName(fAttachment.PostedFile.FileName);
            }

            if (hdnWPSID.Value == "0")
            {
                if (fAttachment.HasFile)
                    msg = true;
                else
                    msg = false;
            }

            if (msg)
            {
                objCommon.WpsId = hdnWPSID.Value;
                objCommon.MaterialGrade = txtMaterialGrade.Text;
                objCommon.Thickness = txtThickness.Text;
                objCommon.Process = txtProcess.Text;
                objCommon.FillerGrade = txtFillerGrade.Text;
                objCommon.Amps = txtAmps.Text;
                objCommon.Polarity = txtPolarity.Text;
                objCommon.Gaslevel = txtGasLevel.Text;
                objCommon.AttachementName = AttachmentName;
                objCommon.WPSNumber = txtWPSNo.Text;
                objCommon.Voltage = txtvoltage.Text;

                ds = objCommon.SaveWPSDetails();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','WPS Details Saved successfully');", true);
                else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','WPS Details Updated successfully');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);

                string WPSPath = WPSSavepath;

                if (!Directory.Exists(WPSPath))
                    Directory.CreateDirectory(WPSPath);

                if (AttachmentName != "")
                    fAttachment.SaveAs(WPSPath + AttachmentName);

                BindWPSDetails();
                ClearValues();

                ShowHideControls("add,view");
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Attachement Is Mandatory');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error Occured');", true);
        }
    }

    protected void btnAddNew_click(object sender, EventArgs e)
    {
        try
        {
            ShowHideControls("input");
            ClearValues();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ShowHideControls("add,view");
            ClearValues();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvWPSMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName.ToString() == "EditWPS")
            {
                objCommon = new cCommonMaster();
                DataSet ds = new DataSet();

                objCommon.WpsId = gvWPSMaster.DataKeys[index].Values[0].ToString();

                ds = objCommon.GetWpsDetailsByWPSID();

                hdnWPSID.Value = ds.Tables[0].Rows[0]["WpsId"].ToString();
                txtMaterialGrade.Text = ds.Tables[0].Rows[0]["MaterialGrade"].ToString();
                txtThickness.Text = ds.Tables[0].Rows[0]["Thickness"].ToString();
                txtProcess.Text = ds.Tables[0].Rows[0]["Process"].ToString();
                txtFillerGrade.Text = ds.Tables[0].Rows[0]["FillerGrade"].ToString();
                txtAmps.Text = ds.Tables[0].Rows[0]["Amps"].ToString();
                txtPolarity.Text = ds.Tables[0].Rows[0]["Polarity"].ToString();
                txtGasLevel.Text = ds.Tables[0].Rows[0]["Gaslevel"].ToString();
                txtWPSNo.Text = ds.Tables[0].Rows[0]["WPSNumber"].ToString();
                txtvoltage.Text = ds.Tables[0].Rows[0]["Voltage"].ToString();

                ShowHideControls("input");
            }

            else if (e.CommandName.ToString() == "ViewWPSFile")
            {

            }

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvWPSMaster_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion



    #region"Common methods"

    private void ShowHideControls(string divid)
    {
        string[] mode = divid.Split(',');
        try
        {
            divAdd.Visible = divInput.Visible = divOutput.Visible = false;
            for (int i = 0; i < mode.Length; i++)
            {
                switch (mode[i].ToLower())
                {
                    case "add":
                        divAdd.Visible = true;
                        break;
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

    private void BindWPSDetails()
    {
        DataSet ds = new DataSet();
        objCommon = new cCommonMaster();
        try
        {
            ds = objCommon.GetWPSDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvWPSMaster.DataSource = ds.Tables[0];
                gvWPSMaster.DataBind();
            }
            else
            {
                gvWPSMaster.DataSource = "";
                gvWPSMaster.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ClearValues()
    {
        try
        {
            hdnWPSID.Value = "0";
            txtMaterialGrade.Text = "";
            txtThickness.Text = "";
            txtProcess.Text = "";
            txtFillerGrade.Text = "";
            txtAmps.Text = "";
            txtPolarity.Text = "";
            txtGasLevel.Text = "";
            txtWPSNo.Text = "";
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ViewWPSFileName(int index)
    {
        try
        {
            objc = new cCommon();
            string FileName = gvWPSMaster.DataKeys[index].Values[1].ToString();

            objc.ViewFileName(WPSSavepath, WPSHttpPath, FileName, "", ifrm);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"PageLoad Complete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvWPSMaster.Rows.Count > 0)
        {
            gvWPSMaster.UseAccessibleHeader = true;
            gvWPSMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    #endregion
}