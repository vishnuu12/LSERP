using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;
using System.Globalization;
using System.IO;

public partial class Pages_StockInward : System.Web.UI.Page
{

    #region"Declaration"

    cStores objSt;
    cMaterials objMat;
    cCommon objc;
    cCommonMaster objcommon;
    cSales objSales;
    cSession objSession = new cSession();

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
                //objSt = new cStores();
                //objSt.GetMRNNumberDetails(ddlMRNNumber);
                BindStockInwardDetailsByMRNID();
                ShowHideControls("view");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"DropDown Events"

    protected void ddlMRNNumber_OnSelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlMRNNumber.SelectedIndex > 0)
            {
                BindStockInwardDetailsByMRNID();
                if (gvStockInwardDetails.Rows.Count > 0)
                    ShowHideControls("add,view");
                else
                    ShowHideControls("add,view,addnew");
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

    protected void btnShareStock_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objSt = new cStores();
        string SIID = "";
        try
        {
            objSession = (cSession)HttpContext.Current.Session["LoginDetails"];
            foreach (GridViewRow row in gvStockInwardDetails.Rows)
            {
                // TextBox txtReceiptDate = (TextBox)row.FindControl("txtReceiptDate");
                Label lblMRNNo = (Label)row.FindControl("lblMRNNo");

                CheckBox chk = (CheckBox)row.FindControl("chkQC");

                if (chk.Checked)
                {
                    SIID = gvStockInwardDetails.DataKeys[row.RowIndex].Values[0].ToString();
                    objSt.SIID = Convert.ToInt32(SIID);
                    objSt.UserID = Convert.ToInt32(objSession.employeeid);
                    // objSt.ReceiptDate = DateTime.ParseExact(txtReceiptDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    ds = objSt.UpdateSIStatusBySIID();

                    if (ds.Tables[0].Rows[0]["Message"].ToString() != "Updated")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "-" + lblMRNNo.Text + "');", true);
                        break;
                    }
                }
            }

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Stock Inward Status Updated Successfully');", true);
                ShowHideControls("view");
            }
            BindStockInwardDetailsByMRNID();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Gridview Events"

    protected void gvStockInwardDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnEdit = (LinkButton)e.Row.FindControl("lbtnEdit");
                if (dr["SIStatus"].ToString() == "1")
                {
                    // btnEdit.Attributes.Add("class", "aspNetDisabled");
                    btnEdit.CssClass = "aspNetDisabled";
                    ViewState["SIStaus"] = "1";
                }
                else
                {
                    //btnEdit.Attributes.Add("Class", "");
                    btnEdit.CssClass = "";
                    ViewState["SIStaus"] = "0";
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindStockInwardDetailsByMRNID()
    {
        DataSet ds = new DataSet();
        objSt = new cStores();
        try
        {
            //objSt.MRNID = Convert.ToInt32(ddlMRNNumber.SelectedValue);

            ds = objSt.GetStockInwardDetailsByMRNID();

            ViewState["StockInward"] = ds.Tables[0];

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvStockInwardDetails.DataSource = ds.Tables[0];
                gvStockInwardDetails.DataBind();
            }
            else
            {
                gvStockInwardDetails.DataSource = "";
                gvStockInwardDetails.DataBind();
            }
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

    #region"PageLoad Complete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvStockInwardDetails.Rows.Count > 0)
            gvStockInwardDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion

}