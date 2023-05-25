using eplus.core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_RFPDispatch : System.Web.UI.Page
{
    #region"Declaration"

    cReports objR;
    cSession objSession = new cSession();

    string RFPDocsSavePath = ConfigurationManager.AppSettings["RFPDocumentArchiveSavePath"].ToString();
    string RFPDocsHttpPath = ConfigurationManager.AppSettings["RFPDocumentArchiveHTTPPath"].ToString();

    #endregion
    #region "Page Init Events"

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

            if (!IsPostBack)
            {
                objR = new cReports();
                DataSet dsRFPHID = new DataSet();
                DataSet dsCustomer = new DataSet();

                dsRFPHID = objR.GetRFPNOForDispatch(Convert.ToInt32(objSession.employeeid), ddlRFPNo);
                dsCustomer = objR.GetCustomernameForDispatch(Convert.ToInt32(objSession.employeeid), ddlCustomerName);

                ViewState["RFPDetails"] = dsRFPHID.Tables[0];

                BindRFPDispatchDetails();
            }
            if (target == "ViewRFPDocs")
            {
                viewWorkOrderDrawingFile(arg.ToString());
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Drop Events"    

    protected void ddlCustomerName_OnSelectIndexChanged(object sender, EventArgs e)
    {
        DataTable dt;
        try
        {
            dt = (DataTable)ViewState["RFPDetails"];
            if (ddlCustomerName.SelectedIndex > 0)
            {
                string ProspectID = ddlCustomerName.SelectedValue;
                dt.DefaultView.RowFilter = "ProspectID='" + ProspectID + "'";
                dt.DefaultView.ToTable();
            }

            ddlRFPNo.DataSource = dt;
            ddlRFPNo.DataTextField = "RFPNo";
            ddlRFPNo.DataValueField = "RFPHID";
            ddlRFPNo.DataBind();
            ddlRFPNo.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlRFPNo_SelectIndexChanged(object sender, EventArgs e)
    {
        cMaterials objMat = new cMaterials();
        try
        {
            objMat.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            string ProspectID = objMat.GetProspectNameByRFPHID();
            ddlCustomerName.SelectedValue = ProspectID;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button EVents"

    protected void btnRFPDispatch_Click(object sender, EventArgs e)
    {
        objR = new cReports();
        DataSet ds = new DataSet();
        try
        {
            objR.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            objR.UserID = objSession.employeeid;
            objR.Remarks = txtRemarks.Text;

            string AttachmentName = "";
            string FileName = "";
            string[] extension;
            string Attchname = "";

            if (fAttachment.HasFile)
            {
                string extn = Path.GetExtension(fAttachment.PostedFile.FileName).ToUpper();
                AttachmentName = Path.GetFileName(fAttachment.PostedFile.FileName);
                extension = AttachmentName.Split('.');
                Attchname = Regex.Replace(extension[0].ToString(), @"[^0-9a-zA-Z]+", "");
                Attchname = Attchname + "RFPProjectFile" + '.' + extension[extension.Length - 1];
                objR.AttachementName = Attchname;
            }
            else
                objR.AttachementName = "";

            if (ddlRFPNo.SelectedIndex > 0)
            {
                ds = objR.UpdateRFPDispatchDetailsByRFPHID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','RFP Successfully Moved Into Dispatched List');", true);

                    string StrStaffDocumentPath = RFPDocsSavePath + "RFPDispatchDocs" + "\\";

                    if (!Directory.Exists(StrStaffDocumentPath))
                        Directory.CreateDirectory(StrStaffDocumentPath);
                    if (Attchname != "")
                        fAttachment.SaveAs(StrStaffDocumentPath + Attchname);

                    BindRFPDispatchDetails();
                }
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','select RFP No');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Grid View Events"

    protected void gvRFPDispatchDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnViewDocs = (LinkButton)e.Row.FindControl("btnViewDocs");
                if (dr["RFPDocs"].ToString() == "")
                    btnViewDocs.Visible = false;
                else
                    btnViewDocs.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common methods"

    private void viewWorkOrderDrawingFile(string index)
    {
        cCommon objc = new cCommon();
        try
        {
            string FileName = gvRFPDispatchDetails.DataKeys[Convert.ToInt32(index)].Values[0].ToString();
            // objc.ViewFileName(RFPDocsSavePath, RFPDocsHttpPath, FileName, "RFPDispatchDocs", ifrm);//
            cCommon.DownLoad(FileName, RFPDocsSavePath + "RFPDispatchDocs" + "\\" + FileName);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindRFPDispatchDetails()
    {
        DataSet ds = new DataSet();
        objR = new cReports();
        try
        {
            ds = objR.GetRFPDispatchDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvRFPDispatchDetails.DataSource = ds.Tables[0];
                gvRFPDispatchDetails.DataBind();
            }
            else
            {
                gvRFPDispatchDetails.DataSource = "";
                gvRFPDispatchDetails.DataBind();
            }
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
        if (gvRFPDispatchDetails.Rows.Count > 0)
        {
            gvRFPDispatchDetails.UseAccessibleHeader = true;
            gvRFPDispatchDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    #endregion

}