using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;
using System.Configuration;

public partial class Pages_QuotationApproval : System.Web.UI.Page
{
    #region"Declaration"

    cPurchase objPur;
    cSession objSession;
    cCommon objc;
    bool IsPageRefresh = false;
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

    #region"pageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];
            if (!IsPostBack)
            {
                objPur = new cPurchase();
                DataSet dsPINum = new DataSet();
                dsPINum = objPur.GetQNNumber(ddlQNNumber, Convert.ToInt32(objSession.type));
                ShowHideControls("add");
                ViewState["postids"] = System.Guid.NewGuid().ToString();
                Session["postid"] = ViewState["postids"].ToString();
            }
            else
            {
                if (ViewState["postids"].ToString() != Session["postid"].ToString())
                {
                    IsPageRefresh = true;
                }
                Session["postid"] = System.Guid.NewGuid().ToString();
                ViewState["postids"] = Session["postid"].ToString();
            }
            if (target == "UpdateApproval" && IsPageRefresh == false) UpdateApproval();
            //else if(target == "viewdocs" && IsPageRefresh == false)
            //{
            //    VieAttachment();
            //}
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button events"
    protected void VieAttachment_Click(object sender, EventArgs e)
    {
        objc = new cCommon();
        try
        {
            //byte[] imageBytes = System.IO.File.ReadAllBytes(Session["PurchaseDocsSavePath"].ToString() +ddlQNNumber.SelectedValue.Split('/')[1].ToString() + "\\" + hdnFileName.Value);
            //string base64String = Convert.ToBase64String(imageBytes);
            //ifrm.Attributes.Add("Src", Session["PurchaseDocsSavePath"].ToString() + "PI1" + "\\" + hdnFileName.Value);

            //imgDocs.ImageUrl = Session["PurchaseDocsSavePath"].ToString() + "PI1" + "\\" + hdnFileName.Value;
            string HttpPath = Session["PurchaseDocsHttpPath"].ToString();
            string SavePath = Session["PurchaseDocsSavePath"].ToString();
            objc.ViewFileName(SavePath, HttpPath, hdnFileName.Value, ddlQNNumber.SelectedValue, ifrm);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void UpdateApproval()
    {
        DataSet ds = new DataSet();
        objPur = new cPurchase();
        try
        {
            for (int i = 0; i < gvQuateSubmission.Rows.Count; i++)
            {
                HiddenField hdnsupid = (HiddenField)gvQuateSubmission.Rows[i].FindControl("hdn_SUPID");
                objPur.QNSUPID += hdnsupid.Value + ",";
                objPur.QNPID += gvQuateSubmission.DataKeys[i].Values[0] + ",";
            }
            objPur.QNSUPID = objPur.QNSUPID.TrimEnd(',');
            objPur.QNPID = objPur.QNPID.TrimEnd(',');
            objPur.QHID = Convert.ToInt32(ddlQNNumber.SelectedValue);
            objPur.CreatedBy = objSession.employeeid;
            objPur.VendorType = objSession.type;

            ds = objPur.SaveQnApprovalStatus();
            if (ds.Tables[0].Rows[0]["MSG"].ToString() == "Approved")
            {
                bindPurchaseIndentDetailsByQNumber();
                btnQNApproval.Enabled = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Loader", "SuccessMessage('Success','Quotation Approved Successfully');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "ErrorMessage('Error','Error Occured');", true);
            Log.Message(ex.ToString());
        }
    }
    #endregion

    #region"DropDown Events"

    protected void ddlQNNumber_SelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlQNNumber.SelectedIndex > 0)
            {
                bindPurchaseIndentDetailsByQNumber();
                ShowHideControls("add,input,view");
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

    #region"Comon Methods"

    private void bindPurchaseIndentDetailsByQNumber()
    {
        objPur = new cPurchase();
        DataSet ds = new DataSet();
        try
        {
            //objPur.QNumber = ddlQNNumber.SelectedValue.Split('/')[0].ToString();
            objPur.QHID = Convert.ToInt32(ddlQNNumber.SelectedValue);
            objPur.VendorType = objSession.type;
            ds = objPur.GetPurchaseIndentDetailsByQNNumber();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvQuateSubmission.DataSource = ds.Tables[0];
                gvQuateSubmission.DataBind();
                //lblL1Approved.Text = ds.Tables[0].Rows[0]["AppStatus"].ToString();
                //lblL2Approved.Text = ds.Tables[0].Rows[0]["AppStatus2"].ToString();
            }
            else
            {
                gvQuateSubmission.DataSource = "";
                gvQuateSubmission.DataBind();
            }
            /*  if (ds.Tables[1].Rows.Count > 0)
             {
                 if (ds.Tables[1].Rows[0]["BtnVisibleStatus"].ToString() == "1") btnQNApproval.Enabled = false;
                 else btnQNApproval.Enabled = true;
             } */
            /*    if (ds.Tables[1].Rows.Count > 0)
               {
                   if (ds.Tables[1].Rows[0]["BtnVisibleStatus"].ToString() == "1") btnQNApproval.Enabled = false;
                   else btnQNApproval.Enabled = true;

                   if(objSession.type == 9 || objSession.type == 1)
                       btnQNApproval.Enabled = true;
                   else
                       btnQNApproval.Enabled = false;
               } */


            if (ds.Tables[1].Rows.Count > 0)
            {
                //if (ds.Tables[1].Rows[0]["BtnVisibleStatus"].ToString() == "1") btnQNApproval.Enabled = false;
                //else btnQNApproval.Enabled = true;

                if ((objSession.type == 1) || (objSession.type == 7 && (ds.Tables[1].Rows[0]["BtnVisibleStatus"].ToString() == "2") && (ds.Tables[0].Rows[0]["L1ApprovedBy"].ToString() != "")))
                    btnQNApproval.Enabled = true;
                else if (objSession.type == 9 && (ds.Tables[1].Rows[0]["BtnVisibleStatus"].ToString() == "0") && (ds.Tables[0].Rows[0]["L1ApprovedBy"].ToString() == " "))
                    btnQNApproval.Enabled = true;
                else
                    btnQNApproval.Enabled = false;
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                lblRFPNo.Text = ds.Tables[2].Rows[0]["RFPNo"].ToString();
                lblindentby.Text = ds.Tables[2].Rows[0]["IndentBy"].ToString();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ShowHideControls(string divids)
    {
        divAdd.Visible = divInput.Visible = divOutput.Visible = false;
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

    #region"PageLoadComplete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvQuateSubmission.Rows.Count > 0)
            gvQuateSubmission.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}