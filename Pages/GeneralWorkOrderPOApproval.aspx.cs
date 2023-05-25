using eplus.core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Drawing;

public partial class Pages_GeneralWorkOrderPOApproval : System.Web.UI.Page
{
    #region"Declaration"

    GeneralWorkOrderIndentApproval gwoia;
    cSession objSession = new cSession();
    string PDFSavePath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
    string PDFHttpPath = "http://183.82.33.21/LSERPDocs/PDFFiles/GSTDocs/";

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
                GeneralWorkOrderPOApproval();

            }
            if (target == "ViewIndentAttach")
            {
                int index = Convert.ToInt32(arg.ToString());
                ViewState["FileName"] = gvGeneralWorkOrderPOApproval.DataKeys[index].Values[2].ToString();
                ViewState["SGWOID"] = gvGeneralWorkOrderPOApproval.DataKeys[index].Values[0].ToString();
                ViewDrawingFilename();
            }
           
            
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnApprovalReject_Click(object sender, EventArgs e)
    {
        gwoia = new GeneralWorkOrderIndentApproval();
        DataSet ds = new DataSet();
        try
        {
            LinkButton btn = sender as LinkButton;
            string CommandName = btn.CommandName;
            string GWOIDA = "";


            foreach (GridViewRow row in gvGeneralWorkOrderPOApproval.Rows)
            {
                CheckBox chkitems = (CheckBox)row.FindControl("chkitems");

                if (chkitems.Checked)
                {
                    if (GWOIDA == "")
                    {
                        GWOIDA = gvGeneralWorkOrderPOApproval.DataKeys[row.RowIndex].Values[0].ToString();

                    }
                    else
                    {
                        GWOIDA = GWOIDA + ',' + gvGeneralWorkOrderPOApproval.DataKeys[row.RowIndex].Values[0].ToString();
                    }
                }
            }
            if (CommandName.ToString() == "Approve")
                gwoia.GWOApprovalStatus = objSession.type == 1 ? 1 : 8;
            else
                gwoia.GWOApprovalStatus = 9;

            gwoia.UserID = Convert.ToInt32(objSession.employeeid);
            gwoia.txtRemarks = txtRemarks.Text;

            ds = gwoia.UpdateGWOPOApprovalByID("LS_UpdateGWOPOApprovalByID", GWOIDA);

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                if (CommandName == "Approve")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "SuccessMessage('Success','Indent Approved Successfuly')", true);
                  
                    GeneralWorkOrderPOApproval();
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "SuccessMessage('Success','Indent Rejected Successfuly')", true);
               
                GeneralWorkOrderPOApproval();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }



    #endregion

    #region"GridView Events"

    protected void gvGeneralWorkOrderPOApproval_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            DataSet ds = new DataSet();
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    public int rfpdidold;
    public int rfpdidnew;
    Color grpcolor;

    protected void gvGeneralWorkOrderPOApproval_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        gwoia = new GeneralWorkOrderIndentApproval();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            Label lblcode = (Label)gvGeneralWorkOrderPOApproval.Rows[index].FindControl("GWPOL1ApprovedBy");
           
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion


    #region"Common Methods"

    public Color randomcolorgenerate()
    {
        Random random = new Random();
        return Color.FromArgb(random.Next(200, 255), random.Next(150, 255), random.Next(150, 255));
    }

    private void GeneralWorkOrderPOApproval()
    {
        DataSet ds = new DataSet();
        gwoia = new GeneralWorkOrderIndentApproval();
        try
        {
            gwoia.UserID = objSession.type;
            ds = gwoia.GetGeneralWorkOrderPOApproval();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvGeneralWorkOrderPOApproval.DataSource = ds.Tables[0];
                gvGeneralWorkOrderPOApproval.DataBind();
                ViewState["GeneralWorkOrderIndentDetails"] = ds.Tables[0];
                gvGeneralWorkOrderPOApproval.UseAccessibleHeader = true;
                gvGeneralWorkOrderPOApproval.HeaderRow.TableSection = TableRowSection.TableHeader;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "ShowDataTable();", true);
            }
            else
            {
                gvGeneralWorkOrderPOApproval.DataSource = "";
                gvGeneralWorkOrderPOApproval.DataBind();
            }
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
            cCommon objc = new cCommon();
            objc.GeneralViewFileName(PDFSavePath, PDFHttpPath, ViewState["FileName"].ToString(), ViewState["SGWOID"].ToString(), ifrm);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}