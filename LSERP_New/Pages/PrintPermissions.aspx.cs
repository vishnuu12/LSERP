using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;

public partial class Pages_PrintPermissions : System.Web.UI.Page
{
    #region "Declaration"

    DataSet dsPrintPermission = new DataSet();
    cSession _objSess = new cSession();
    cCommonMaster _objCommon = new cCommonMaster();
    cCommon _objc = new cCommon();

    #endregion

    #region "PageInit Events"

    protected void Page_Init(object sender, EventArgs e)
    {
        _objSess = Master.csSession;
    }

    #endregion

    #region "PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            bindPrintPermissions();
            
            divOutput.Visible = true;
        }
    }

    #endregion

    #region

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            foreach (GridViewRow row in gvPrintPermission.Rows)
            {
                _objCommon.EmpID = Convert.ToInt32(gvPrintPermission.DataKeys[row.RowIndex].Values[0].ToString());
                CheckBox chkPrint = (CheckBox)row.FindControl("chkPrintPer");
                CheckBox chkExcel = (CheckBox)row.FindControl("chkExcelPer");
                CheckBox chkPDF = (CheckBox)row.FindControl("chkPDFPer");
                _objCommon.Print_Per = chkPrint.Checked == true ? 1 : 0;
                _objCommon.Excel_Per = chkExcel.Checked == true ? 1 : 0;
                _objCommon.PDF_Per = chkPDF.Checked == true ? 1 : 0;

                ds = _objCommon.SavePrintPermissions();                          
            }

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "hideLoader();SuccessMessage('Success','Permissions Saved successfully');", true);
            }

            //showDataTable();

            bindPrintPermissions();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion


    #region"GridView Events"

    protected void gvPrintPermission_RowCommand(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[1].Attributes.Add("style", "text-align:left;");
                e.Row.Cells[2].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[3].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[4].Attributes.Add("style", "text-align:center;");
            }
            else if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[1].Attributes.Add("style", "text-align:left;");
                e.Row.Cells[2].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[3].Attributes.Add("style", "text-align:center;");
                e.Row.Cells[4].Attributes.Add("style", "text-align:center;");

                CheckBox chkprint = (CheckBox)e.Row.FindControl("chkPrintPer");
                CheckBox chkExcel = (CheckBox)e.Row.FindControl("chkExcelPer");
                CheckBox chkPDF = (CheckBox)e.Row.FindControl("chkPDFPer");

                chkprint.Checked = dr["Per_Print"].ToString() == "0" ? false : true;
                chkExcel.Checked = dr["Per_Excel"].ToString() == "0" ? false : true;
                chkPDF.Checked = dr["Per_PDF"].ToString() == "0" ? false : true;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region "Common Methods"

    private void bindPrintPermissions()
    {
        try
        {

            dsPrintPermission = _objCommon.GetPrintPermissionDetails();

            if (dsPrintPermission.Tables[0].Rows.Count > 0)
            {
                gvPrintPermission.DataSource = dsPrintPermission.Tables[0];
                gvPrintPermission.DataBind();

                gvPrintPermission.UseAccessibleHeader = true;
                gvPrintPermission.HeaderRow.TableSection = TableRowSection.TableHeader;

               ScriptManager.RegisterStartupScript(this, this.GetType(), "datatable", "showDataTable();", true);
            }
            else
            {
                gvPrintPermission.DataSource = "";
                gvPrintPermission.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }

    }

    #endregion
}