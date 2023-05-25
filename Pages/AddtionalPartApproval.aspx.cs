using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using eplus.core;
using System.Configuration;

public partial class Pages_AddtionalPartApproval : System.Web.UI.Page
{
    #region"Declaration"

    cMaterials objMat;
    cDesign objDesign;
    cSession objSession;
    cCommon objc;
    cProduction objP;
    string DrawingDocumentSavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString();
    string DrawingDocumentHttppath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString();

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

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                bindBOMCostDetails();
                BindAddtionalPartApprovedDetails();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        objMat = new cMaterials();
        try
        {
            string CommandName = ((Button)sender).CommandName;
            DataRow dr;
            dt.Columns.Add("BOMID");
            dt.Columns.Add("Remarks");
            foreach (GridViewRow row in gvBOMCostDetails.Rows)
            {
                CheckBox chkitems = (CheckBox)row.FindControl("chkitems");
                TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                if (chkitems.Checked)
                {
                    dr = dt.NewRow();
                    dr["BOMID"] = gvBOMCostDetails.DataKeys[row.RowIndex].Values[1].ToString();
                    dr["Remarks"] = txtRemarks.Text;
                    dt.Rows.Add(dr);
                }
            }

            objMat.dt = dt;
            if (CommandName == "Approve")
                objMat.Flag = "Approve";
            else if (CommandName == "Reject")
                objMat.Flag = "Reject";

            objMat.CreatedBy = objSession.employeeid;
            ds = objMat.UpdateProductionAddtionalPartRequestByBOMID();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','Addtional Part Requested  Successfully');", true);
                bindBOMCostDetails();
                BindAddtionalPartApprovedDetails();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "InfoMessage('information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    //protected void gvBOMCostDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    try
    //    {
    //        DataRowView dr = (DataRowView)e.Row.DataItem;

    //        if (e.Row.RowType == DataControlRowType.Header)
    //        {
    //            e.Row.Cells[2].Visible = false;
    //            e.Row.Cells[3].Visible = false;
    //            e.Row.Cells[4].Visible = false;
    //            e.Row.Cells[5].Visible = false;
    //            //    e.Row.Cells[6].Visible = false;
    //            e.Row.Cells[7].Visible = false;

    //            e.Row.Cells[8].Visible = false;
    //            e.Row.Cells[9].Visible = false;
    //            e.Row.Cells[10].Visible = false;
    //            e.Row.Cells[11].Visible = false;
    //        }

    //        if (e.Row.RowType == DataControlRowType.DataRow)
    //        {
    //            //  LinkButton btnEdit = (LinkButton)e.Row.FindControl("btnedit");
    //            //LinkButton btnDelete = (LinkButton)e.Row.FindControl("btnDelete");
    //            //CheckBox chk = (CheckBox)e.Row.FindControl("chkitems");

    //            e.Row.Cells[2].Visible = false;
    //            e.Row.Cells[3].Visible = false;
    //            e.Row.Cells[4].Visible = false;
    //            e.Row.Cells[5].Visible = false;
    //            //    e.Row.Cells[6].Visible = false;
    //            e.Row.Cells[7].Visible = false;

    //            e.Row.Cells[8].Visible = false;
    //            e.Row.Cells[9].Visible = false;
    //            e.Row.Cells[10].Visible = false;
    //            e.Row.Cells[11].Visible = false;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    #endregion

    #region"Common Methods"

    private void bindBOMCostDetails()
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            ds = objMat.GetProductionAddtionalPartRequestDetails();

            //if (ds.Tables[0].Rows[0]["Message"].ToString() == "RecordsFound")
            //{
            //if (ds.Tables[0].Columns.Contains("MOC"))
            //    ds.Tables[1].Columns.Remove("MOC");
            //ds.Tables[1].Columns.Remove("DrawingSequenceNumber");
            //ds.Tables[1].Columns.Remove("AddtionalPart");
            //ds.Tables[1].Columns.Remove("Part Remarks");

            //ds.Tables[1].Columns.Remove("MCOST");
            //ds.Tables[1].Columns.Remove("LCOST");

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvBOMCostDetails.DataSource = ds.Tables[0];
                gvBOMCostDetails.DataBind();
            }
            else
            {
                gvBOMCostDetails.DataSource = "";
                gvBOMCostDetails.DataBind();
            }

            //lblItemBomTotalCost.Text = ds.Tables[3].Rows[0]["ItemBOMCost"].ToString();

            //if (ds.Tables[4].Rows.Count > 0)
            //    lblCost.Text = ds.Tables[4].Rows[0]["TotalBOMCost"].ToString();

            //lblDrawingNumber.Text = " ( " + ds.Tables[5].Rows[0]["DrawingNumber"].ToString() + " ) ";
            //lblItemQty.Text = " Qty: " + ds.Tables[5].Rows[0]["Quantity"].ToString() + "";

            //}
            //else
            //{
            //    gvBOMCostDetails.DataSource = "";
            //    gvBOMCostDetails.DataBind();

            //    //lblItemBomTotalCost.Text = lblCost.Text = "0.00";

            //    //lblDrawingNumber.Text = " ( " + ds.Tables[1].Rows[0]["DrawingNumber"].ToString() + " ) ";
            //    //lblItemQty.Text = "  Qty: " + ds.Tables[1].Rows[0]["Quantity"].ToString() + "";
            //}
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindAddtionalPartApprovedDetails()
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            ds = objMat.GetAddtionalpartApprovedDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvApprovedDetails.DataSource = ds.Tables[0];
                gvApprovedDetails.DataBind();
            }
            else
            {
                gvApprovedDetails.DataSource = "";
                gvApprovedDetails.DataBind();
            }
            //if (gvApprovedDetails.Rows.Count > 0)
            //    gvApprovedDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowDatatable();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"PageLoad_Complete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvApprovedDetails.Rows.Count > 0)
            gvApprovedDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
        if (gvBOMCostDetails.Rows.Count > 0)
            gvBOMCostDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion

}