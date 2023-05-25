using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;

public partial class Pages_CurrencyMaster : System.Web.UI.Page
{
    #region"Declaration"

    cCommon objc =  new cCommon();
    cSales objsales=new cSales();
    
    #endregion

    #region"PageLoad Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        string target = Request["__EVENTTARGET"];
        string arg = Request["__EVENTARGUMENT"];
        try
        {
            if (IsPostBack == false)
            {
                bindCurrencyDetails();
                ShowHideControls("view");
            }
            else
            {
                if (target == "deletegvrow")
                {
                    objsales = new cSales();

                    objsales.CID = Convert.ToInt32(arg);
                    DataSet ds = objsales.DeleteCurrencydetails();

                    if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Item Size Deleted successfully');", true);

                    bindCurrencyDetails();
                    ShowHideControls("view");
                }
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        ShowHideControls("add");
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ShowHideControls("view");
        ClearValues();
        gvCurrencyMaster.UseAccessibleHeader = true;
        gvCurrencyMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "ShowDataTable();", true);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objsales = new cSales();
        try
        {
            if (objc.Validate(divInput))
            {
                objsales.CID = Convert.ToInt32(hdnCID.Value);
                objsales.Currencyname = txt_currencyname.Text;
                objsales.Currencysymbol = txt_currencysymbol.Text;
                objsales.INRvalue = Convert.ToDecimal(txt_inrvalue.Text);



                ds = objsales.SaveCurrencyDetails();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "SuccessMessage('Success','Currency Details Saved Successfully');hideLoader();", true);
                                                                                                                    
                else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")                                   
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "SuccessMessage('Success','Currency Details Updated Successfully');hideLoader();", true);

                else if (ds.Tables[0].Rows[0]["Message"].ToString() == "AE")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "InfoMessage('Information','Currency Details Already Exists');hideLoader();", true);

                ShowHideControls("view");
                ClearValues();
                bindCurrencyDetails();
            }
            else
                ShowHideControls("add");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "ErrorMessage('Error','Error Occured');hideLoader();", true);
            Log.Message(ex.ToString());
        }
    }


    #endregion

    #region"gridView Events"

    protected void gvCurrencyMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        objsales = new cSales();
        int index;
        try
        {
            index = Convert.ToInt32(e.CommandArgument.ToString());
            hdnCID.Value = gvCurrencyMaster.DataKeys[index].Values[0].ToString();
            objsales.CID = Convert.ToInt32(hdnCID.Value);
            ds = objsales.CurrencyDetails(objsales.CID);
               
            txt_currencyname.Text = ds.Tables[0].Rows[0]["CurrencyName"].ToString();
            txt_currencysymbol.Text = ds.Tables[0].Rows[0]["CurrencySymbol"].ToString();
            txt_inrvalue.Text = ds.Tables[0].Rows[0]["INRvalue"].ToString();

            ShowHideControls("add");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion


    #region"Common Methods"

    private void bindCurrencyDetails()
    {
        DataSet ds = new DataSet();
        objsales = new cSales();
        try
        {
            ds = objsales.CurrencyDetails(0);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvCurrencyMaster.DataSource = ds.Tables[0];
                gvCurrencyMaster.DataBind();
                gvCurrencyMaster.UseAccessibleHeader = true;
                gvCurrencyMaster.HeaderRow.TableSection = TableRowSection.TableHeader;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "ShowDataTable();", true);
            }
            else
            {
                gvCurrencyMaster.DataSource = "";
                gvCurrencyMaster.DataBind();
            }

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }


    private void ShowHideControls(string mode)
    {
        try
        {
            divAdd.Visible = divInput.Visible = divOutput.Visible = false;

            switch (mode.ToLower())
            {
                case "add":
                    divInput.Visible = true;
                    txt_currencyname.Focus();
                    break;
                case "edit":
                    divInput.Visible = true;
                    txt_currencyname.Focus();
                    break;
                case "view":
                    divAdd.Visible = divOutput.Visible = true;
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
        hdnCID.Value = "0";
        txt_currencyname.Text = "";
        txt_currencysymbol.Text = "";
        txt_inrvalue.Text = "";
    }

    #endregion
}