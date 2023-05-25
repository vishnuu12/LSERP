using eplus.core;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_RFPItemReadyToDispatch : System.Web.UI.Page
{

    #region"Declaration"

    cSession objSession = new cSession();
    cProduction objP;

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
                objP = new cProduction();
                DataSet dsRFPHID = new DataSet();
                DataSet dsCustomer = new DataSet();

                dsCustomer = objP.GetRFPCustomerNameByUserIDAndReadyToDispatch(Convert.ToInt32(objSession.employeeid), ddlCustomerName);
                dsRFPHID = objP.GetRFPDetailsByUserIDAndReadyToDispatch(Convert.ToInt32(objSession.employeeid), ddlRFPNo);

                ViewState["RFPDetails"] = dsRFPHID.Tables[0];
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

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

            bindItemSnoByAssemplyPlanningCompleted();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void bindItemSnoByAssemplyPlanningCompleted()
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objP.GetItemSnoByRFPHIDAndAssemplyPlanningCompleted();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvSecondaryJobOrderDetails.DataSource = ds.Tables[0];
                gvSecondaryJobOrderDetails.DataBind();
            }
            else
            {
                gvSecondaryJobOrderDetails.DataSource = "";
                gvSecondaryJobOrderDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    //LS_GetItemDetailsByRFPHIDInReadyToDispatch

    //private void BindItemDetails()
    //{
    //    DataSet ds = new DataSet();
    //    objP = new cProduction();
    //    try
    //    {
    //        objP.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
    //        ds = objP.GetItemDetailsByRFPHIDForAssemplyPlanning();

    //        if (ds.Tables[0].Rows.Count > 0)
    //        {
    //            gvItemDetails.DataSource = ds.Tables[0];
    //            gvItemDetails.DataBind();
    //        }
    //        else
    //        {
    //            gvItemDetails.DataSource = "";
    //            gvItemDetails.DataBind();
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Log.Message(ex.ToString());
    //    }
    //}

    public Color randomcolorgenerate()
    {
        Random random = new Random();
        return Color.FromArgb(random.Next(200, 255), random.Next(150, 255), random.Next(150, 255));
    }

    protected void btnReadyToDispatch_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        string PRIDIDs = "";
        try
        {
            foreach (GridViewRow row in gvSecondaryJobOrderDetails.Rows)
            {
                CheckBox chk = (CheckBox)row.FindControl("chkQC");
                if (chk.Checked)
                {
                    if (PRIDIDs == "")
                        PRIDIDs = gvSecondaryJobOrderDetails.DataKeys[row.RowIndex].Values[0].ToString();
                    else
                        PRIDIDs = PRIDIDs + ',' + gvSecondaryJobOrderDetails.DataKeys[row.RowIndex].Values[0].ToString();
                }
            }

            ds = objP.UpdateItemSnoCompletedStatusByPRIDIDs(PRIDIDs);

            //ds = objP.UpdateItemCompletedStatusByPRIDID();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Item Completed Successfully');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
            bindItemSnoByAssemplyPlanningCompleted();
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

    protected void gvSecondaryJobOrderDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox chk = (CheckBox)e.Row.FindControl("chkQC");

                rfpdidnew = Convert.ToInt32(dr["RFPDID"].ToString());
                if (rfpdidnew == rfpdidold)
                {
                    e.Row.BackColor = grpcolor;
                }
                else
                {
                    Color randomColor = randomcolorgenerate();
                    while (grpcolor == randomColor)
                    {
                        randomColor = randomcolorgenerate();
                    }
                    e.Row.BackColor = randomColor;
                    grpcolor = randomColor;

                }
                rfpdidold = rfpdidnew;

                if (dr["Enable"].ToString() == "Yes" && dr["ItemAssempled"].ToString() == "0")
                    chk.Visible = true;
                else
                    chk.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion
}