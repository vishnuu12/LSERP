using eplus.core;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;

public partial class Pages_GeneralWorkOrderIndentApproval : System.Web.UI.Page
{
    #region"Declaration"

    GeneralWorkOrderIndentApproval gwoia;
    cSession objSession = new cSession();
    cCommon objc;
    cCommonMaster objcommon;
    cProduction objP;
    cMaterials objMat;
    cSales objSales;
    string GWOISavePath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
    string GWOIHttpPath = "http://183.82.33.21/LSERPDocs/PDFFiles/GSTDocs/";

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
                showDatas("input");
                // GeneralWorkOrderIndentPODetails();

            }
            if (target == "ViewIndentAttach")
            {
                int index = Convert.ToInt32(arg.ToString());
                ViewState["FileName"] = gvGeneralWorkOrderIndent.DataKeys[index].Values[2].ToString();
                ViewState["GID"] = gvGeneralWorkOrderIndent.DataKeys[index].Values[0].ToString();
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

    protected void btnPending_Click(object sender, EventArgs e)
    {
        //showDatas("input");
        //showDatas("output");
        GeneralWorkOrderIndentPODetails(0);
        showDatas("output");
    }

    protected void btnApproved_Click(object sender, EventArgs e)
    {
        // showDatas("add");
        // showDatas("remarks");
        GeneralWorkOrderIndentPODetails(1);
        showDatas("output");
    }

    protected void btnApprovalReject_Click(object sender, EventArgs e)
    {
        gwoia = new GeneralWorkOrderIndentApproval();
        DataSet ds = new DataSet();
        try
        {
            LinkButton btn = sender as LinkButton;
            string CommandName = btn.CommandName;
            string GWOIDA = "";
            //string GWOID = "";


            foreach (GridViewRow row in gvGeneralWorkOrderIndent.Rows)
            {
                CheckBox chkitems = (CheckBox)row.FindControl("chkitems");

                if (chkitems.Checked)
                {
                    if (GWOIDA == "")
                    {
                        GWOIDA = gvGeneralWorkOrderIndent.DataKeys[row.RowIndex].Values[0].ToString();

                    }
                    else
                    {
                        GWOIDA = GWOIDA + ',' + gvGeneralWorkOrderIndent.DataKeys[row.RowIndex].Values[0].ToString();
                    }
                }
            }
            if (CommandName.ToString() == "Approve")
                gwoia.GWOApprovalStatus = objSession.type == 1 ? 7 : 8;
            else
                gwoia.GWOApprovalStatus = 9;

            gwoia.UserID = Convert.ToInt32(objSession.employeeid);
            gwoia.txtRemarks = txtRemarks.Text;

            ds = gwoia.UpdateGWOApprovalStatusByID("LS_UpdateGWOApprovalStatusByID", GWOIDA);

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                if (CommandName == "Approve")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "SuccessMessage('Success','Indent Approved Successfuly')", true);

                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "SuccessMessage('Success','Indent Rejected Successfuly')", true);
                GeneralWorkOrderIndentPODetails(0);
                showDatas("output");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }




    #endregion

    #region"GridView Events"

    public int rfpdidold;
    public int rfpdidnew;
    Color grpcolor;

    protected void gvGeneralWorkOrderIndent_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            CheckBox box = (CheckBox)e.Row.FindControl("chkitems");
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                rfpdidnew = Convert.ToInt32(dr["GWOID"].ToString());
                if (rfpdidnew == rfpdidold)
                    e.Row.Cells[3].BackColor = grpcolor;
                else
                {
                    Color randomColor = randomcolorgenerate();
                    while (grpcolor == randomColor)
                    {
                        randomColor = randomcolorgenerate();
                    }
                    e.Row.Cells[3].BackColor = grpcolor;
                    grpcolor = randomColor;

                }
                rfpdidold = rfpdidnew;

                int status = Convert.ToInt32(dr["Status"].ToString());
                if (status == 1)
                {
                    box.Visible = false;
                }
                else
                {
                    box.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvGeneralWorkOrderIndent_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        gwoia = new GeneralWorkOrderIndentApproval();
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            hdnSGWOID.Value = gvGeneralWorkOrderIndent.DataKeys[index].Values[0].ToString();
            gwoia.SGWOID = Convert.ToInt32(hdnSGWOID.Value);


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
        return Color.FromArgb(random.Next(200, 255), random.Next(150, 255), random.Next(150, 255), random.Next(150, 255));
    }

    private void GeneralWorkOrderIndentPODetails(int status)
    {
        DataSet ds = new DataSet();
        gwoia = new GeneralWorkOrderIndentApproval();
        try
        {
            gwoia.ChargesID = status;
            ds = gwoia.GetGeneralWorkOrderIndentApprovalDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvGeneralWorkOrderIndent.DataSource = ds.Tables[0];
                gvGeneralWorkOrderIndent.DataBind();
                ViewState["GeneralWorkOrderIndentDetails"] = ds.Tables[0];
                gvGeneralWorkOrderIndent.UseAccessibleHeader = true;
                gvGeneralWorkOrderIndent.HeaderRow.TableSection = TableRowSection.TableHeader;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "ShowDataTable();", true);
            }
            else
            {
                gvGeneralWorkOrderIndent.DataSource = "";
                gvGeneralWorkOrderIndent.DataBind();
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
            objc.GeneralViewFileName(GWOISavePath, GWOIHttpPath, ViewState["FileName"].ToString(), ViewState["GID"].ToString(), ifrm);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    private void showDatas(string mode)
    {
        try
        {
            divAdd.Visible = divInput.Visible = divOutput.Visible = false;
            switch (mode.ToLower())
            {
                case "output":
                    divInput.Visible = true;
                    divOutput.Visible = true;
                    break;
                case "add":
                    divAdd.Visible = true;
                    break;
                case "input":
                    divInput.Visible = true;
                    break;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    #endregion
}