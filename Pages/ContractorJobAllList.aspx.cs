using eplus.core;
using System;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_ContractorJobAllList : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cCommon objc;
    cMaterials objMat;
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
            string target = Request["__EVENTTARGET"];
            string arg = Request["__EVENTARGUMENT"];

            if (IsPostBack == false)
            {
                ViewState["RFPDID"] = Request.QueryString["RFPDID"].ToString();
                //BindContractorJobOrderDetails();
                BindDropDownDetails();
                ShowHideControls("drop");
            }
            if (target == "ViewJobComplete")
            {
                BindContractorJobComplete(arg.ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowPartPopUp();", true);
            }
            if (target == "ViewJobInComplete")
            {
                BindContractorJobInComplete(arg.ToString());
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowPartPopUp();", true);
            }
            else
            {
                if (target == "deletegvrow")
                {
                    objP = new cProduction();
                    objP.CTPDID = Convert.ToInt32(arg);

                    DataSet ds = objP.DeleteContractorJobOrderFormDetails();

                    if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Row Deleted successfully');", true);
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Row Details Not Deleted');", true);
                        BindContractorJobOrderFormDetails();
                    ClearValues();

                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Dropdown Events"

    private void BindDropDownDetails()
    {
        objP = new cProduction();
        DataSet ds = new DataSet();
        try
        {
            objP.RFPDID = Convert.ToInt32(ViewState["RFPDID"]);
            objP.GetContractorName(ddContractorName, objP.RFPDID);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddContractorName_OnSelectIndexChanged(object sender, EventArgs e)
    {
        cProduction objP = new cProduction();
        DataSet ds = new DataSet();
        try
        {
            if (ddContractorName.SelectedIndex > 0)
            {
                //divOutput.Visible = true;
                objP.CMID = Convert.ToInt32(ddContractorName.SelectedValue);
                BindContractorJobOrderDetails(objP.CMID);
                ShowHideControls("view");

            }
            //else
                //divOutput.Visible = false;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnSaveContractor_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.CTDID = Convert.ToInt32(hdnCTDID.Value);
            objP.RFPDID = Convert.ToInt32(ViewState["RFPDID"]);
            objP.AmountPercentage = Convert.ToInt32(txtAmountPercentage.Text);
            objP.Amount = Convert.ToDecimal(txtAmount.Text);
            objP.PaymentDate = DateTime.ParseExact(txtPaymentDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objP.Remarks = txtRemarks.Text.ToString();
            objP.ContractorName = txtContractorName.Text.ToString();
            ds = objP.SaveContractorPercentage();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Records Saved Successfully');", true);
            else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Records Updated Successfully');", true);
            BindContractorJobOrderFormDetails();
            ClearValues();
        }
        catch
        {

        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ClearValues();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }


    #endregion

    #region"gridView Events"

    protected void gvContractorJobOrderDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            if (e.CommandName.ToString() == "ContractorAmount")
            {
                txtContractorName.Text = Convert.ToString(e.CommandArgument.ToString());
                //Determine the RowIndex of the Row whose Button was clicked.
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                //Reference the GridView Row.
                GridViewRow row = gvContractorJobOrderDetails.Rows[rowIndex];

                Label lblContractorName = (Label)gvContractorJobOrderDetails.Rows[rowIndex].FindControl("lblContractorName");

                Label lblTotalUnitRate = (Label)gvContractorJobOrderDetails.Rows[rowIndex].FindControl("lblTotalUnitRate");
                txtContractorName.Text = lblContractorName.Text.ToString();
                lblAmount.Text = lblTotalUnitRate.Text.ToString();
                lblBalanceAmount.Text = lblTotalUnitRate.Text.ToString();
                BindContractorJobOrderFormDetails();
                ShowHideControls("edit");
                ClearValues();
            }
        }
        catch
        {

        }
    }

    protected void gvContractorForm_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            //if (e.CommandName.ToString() == "Edit")
            //{
            //    //Determine the RowIndex of the Row whose Button was clicked.
            //    int index = Convert.ToInt32(e.CommandArgument.ToString());
            //    hdnCTPDID.Value = gvContractorForm.DataKeys[index].Values[0].ToString();
            //    objP.CTPDID = Convert.ToInt32(hdnCTPDID.Value);

            //    DataTable dt = (DataTable)ViewState["ContractorJobOrderFormDetails"];
            //    dt.DefaultView.RowFilter = "CTPDID='" + objP.CTPDID + "'";
            //    hdnCTPDID.Value = objP.CTPDID.ToString();

            //    //Reference the GridView Row.
            //    GridViewRow row = gvContractorForm.Rows[index];

            //    txtAmountPercentage.Text = dt.DefaultView.ToTable().Rows[0]["AmountInPercentage"].ToString();
            //    ShowHideControls("editcon");
            //}
        }
        catch
        {

        }
    }

    #endregion

    #region"Common Methods"

    private void BindContractorJobComplete(string RFPDID)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.RFPDID = Convert.ToInt32(RFPDID);

            ds = objP.GetBindContractorJobComplete();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvMaterialPlanningDetails.DataSource = ds.Tables[0];
                gvMaterialPlanningDetails.DataBind();
                //btnMaterialPlanningStatus.Visible = true;
            }
            else
            {
                gvMaterialPlanningDetails.DataSource = "";
                gvMaterialPlanningDetails.DataBind();
                // btnMaterialPlanningStatus.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindContractorJobInComplete(string RFPDID)
    {
        DataSet ds = new DataSet();
        objP = new cProduction();
        try
        {
            objP.RFPDID = Convert.ToInt32(RFPDID);

            ds = objP.GetBindContractorJobInComplete();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvMaterialPlanningDetails.DataSource = ds.Tables[0];
                gvMaterialPlanningDetails.DataBind();
                //btnMaterialPlanningStatus.Visible = true;
            }
            else
            {
                gvMaterialPlanningDetails.DataSource = "";
                gvMaterialPlanningDetails.DataBind();
                // btnMaterialPlanningStatus.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    private void BindContractorJobOrderDetails(int CMID)
    {
        objP = new cProduction();
        DataSet ds = new DataSet();
        try
        {
            objP.RFPDID = Convert.ToInt32(ViewState["RFPDID"].ToString());
            objP.CMID = CMID;
            ds = objP.BindContractorJobOrderDetails();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvContractorJobOrderDetails.DataSource = ds.Tables[0];
                gvContractorJobOrderDetails.DataBind();
                ViewState["ContractorJobOrderDetails"] = ds.Tables[0];
            }
            else
            {
                gvContractorJobOrderDetails.DataSource = "";
                gvContractorJobOrderDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindContractorJobOrderFormDetails()
    {
        objP = new cProduction();
        DataSet ds = new DataSet();
        try
        {
            objP.RFPDID = Convert.ToInt32(ViewState["RFPDID"].ToString());
            objP.ContractorName = txtContractorName.Text.ToString();
            ds = objP.BindContractorJobOrderFormDetails();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvContractorForm.DataSource = ds.Tables[0];
                gvContractorForm.DataBind();
                ViewState["ContractorJobOrderFormDetails"] = ds.Tables[0];
            }
            else
            {
                gvContractorForm.DataSource = "";
                gvContractorForm.DataBind();
            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                lblPaidAmount.Text = ds.Tables[1].Rows[0]["PAID"].ToString();
                lblBalanceAmount.Text = ds.Tables[1].Rows[0]["BALANCE"].ToString();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ShowHideControls(string mode)
    {
        objP = new cProduction();
        DataSet ds = new DataSet();
        try
        {
            divView.Visible = divInput.Visible = divAdd.Visible = divOutput.Visible = false;

            switch (mode.ToLower())
            {
                case "edit":
                    divView.Visible = divAdd.Visible = divInput.Visible = divOutput.Visible = true;
                    break;
                case "view":
                    divView.Visible = divAdd.Visible = true;
                    break;
                case "editcon":
                    divInput.Visible = true;
                    break;
                case "drop":
                    divView.Visible = true;
                    break;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ClearValues()
    {
        hdnCTPDID.Value = "0";
        txtAmountPercentage.Text = "";
        txtAmount.Text = "";
        txtPaymentDate.Text = "";
        txtRemarks.Text = "";

    }

    #endregion

    #region"PageLoad Complete"

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        if (gvContractorJobOrderDetails.Rows.Count > 0)
            gvContractorJobOrderDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #endregion
}