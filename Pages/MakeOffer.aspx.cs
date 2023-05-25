using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Net;
using eplus.data;
using System.IO;
using eplus.core;

public partial class Pages_MakeOffer : System.Web.UI.Page
{
    #region "Declaration"

    DataSet dsAnnexure = new DataSet();
    cSession _objSession = new cSession();
    cSales _objSales = new cSales();
    cCommon _objc = new cCommon();
    cCommonMaster _objCommon = new cCommonMaster();
    #endregion

    #region "Page Events"

    protected void Page_Init(object sender, EventArgs e)
    {

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string target = Request["__EVENTTARGET"];
        string arg = Request["__EVENTARGUMENT"];

        if (IsPostBack == false)
        {
            ddlcustomerload();
            bindAnnexures();            
        }
        else
        {

        }
    }

    #endregion

    #region "GridView Events"

    protected void gvAnnexure1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find the DropDownList in the Row
                string sowhead = (e.Row.FindControl("lblSOWHeader") as Label).Text;
                DropDownList ddlsowpoints = (e.Row.FindControl("ddlsowpoints") as DropDownList);
                ddlsowpoints.DataSource = _objSales.GetddlData("Headername", sowhead, "LS_SowDropdown");
                ddlsowpoints.DataTextField = "Point";
                ddlsowpoints.DataValueField = "Point";
                ddlsowpoints.DataBind();
                string sowpoint = (e.Row.FindControl("lblsowPoint") as Label).Text;
                if (ddlsowpoints.Items.Count < 1) (e.Row.FindControl("chksow") as CheckBox).Checked = false;
                ddlsowpoints.Items.FindByValue(sowpoint).Selected = true;                
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
    }
    protected void gvAnnexure3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Find the DropDownList in the Row
                string tachead = (e.Row.FindControl("lblTACHeader") as Label).Text;
                DropDownList ddltacpoints = (e.Row.FindControl("ddltacpoints") as DropDownList);
                ddltacpoints.DataSource = _objSales.GetddlData("Headername", tachead, "LS_TACDropdown");
                if (ddltacpoints.DataSource == null) (e.Row.FindControl("chktac") as CheckBox).Checked = false;
                ddltacpoints.DataTextField = "Point";
                ddltacpoints.DataValueField = "Point";
                ddltacpoints.DataBind();
                string sowpoint = (e.Row.FindControl("lblTACPoint") as Label).Text;
                ddltacpoints.Items.FindByValue(sowpoint).Selected = true;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex);
        }
    }

    #endregion

    #region "Button Events"



    #endregion

    #region "Dropdown Events"
    
    protected void ddlcustomers_IndexChanged(object sender, EventArgs e)
    {
        try
        {
            clearfields();
            if (ddlcustomers.SelectedValue != "0")
            {
                ddlenquiries.DataSource = _objSales.GetddlData("Customerid", "1", "LS_GetEnquirynumber");
                ddlenquiries.DataTextField = "CustomerEnquiryNumber";
                ddlenquiries.DataValueField = "EnquiryID";
                ddlenquiries.DataBind();
                ddlenquiries.Items.Insert(0, new ListItem("--Select--", "0"));
            }            
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Hide Loader", "hideLoader();", true);
    }
    protected void ddlenquiries_IndexChanged(object sender, EventArgs e)
    {
        try
        {
            clearfields();
            if (ddlenquiries.SelectedValue != "0")
            {
                try
                {
                    cDataAccess DAL1 = new cDataAccess();
                    DataSet ds1 = new DataSet();
                    SqlCommand c1 = new SqlCommand();
                    c1.CommandType = CommandType.StoredProcedure;
                    c1.CommandText = "LS_Getoffercost";
                    c1.Parameters.AddWithValue("@Enquiryid", ddlenquiries.SelectedValue);
                    DAL1.GetDataset(c1, ref ds1);
                    gvAnnexure2.DataSource = ds1.Tables[0];
                    gvAnnexure2.DataBind();
                }
                catch (Exception ec)
                {
                }
                cDataAccess DAL = new cDataAccess();
                DataSet ds = new DataSet();
                SqlCommand c = new SqlCommand();
                c.CommandType = CommandType.StoredProcedure;
                c.CommandText = "LS_GetEnquirydetails";
                c.Parameters.AddWithValue("@Enquiryid", ddlenquiries.SelectedValue);
                DAL.GetDataset(c, ref ds);
                DataTable dt = ds.Tables[0];
                txt_FaxNo.Text = dt.Rows[0]["Faxno"].ToString();
                txt_CustomerphoneNo.Text = dt.Rows[0]["CustomerphoneNo"].ToString();
                txt_CustomerEmail.Text = dt.Rows[0]["CustomerEmail"].ToString();
                txt_ContactPerson.Text = dt.Rows[0]["ContactPerson"].ToString();
                txt_CustomerMobile.Text = dt.Rows[0]["CustomerMobile"].ToString();
                txt_ContactPersonEmail.Text = dt.Rows[0]["ContactPersonEmail"].ToString();

                DataTable dt1 = ds.Tables[2];
                for (int dtl = 0; dtl < dt1.Rows.Count; dtl++)
                {
                    if (dt1.Rows[dtl]["Emptype"].ToString() == "Employee")
                    {
                        txt_MarktEngg.Text = dt1.Rows[dtl]["MarktEngg"].ToString();
                        txt_Marktdesignation.Text = dt1.Rows[dtl]["Marktdesignation"].ToString();
                        txt_MarktMobileNo.Text = dt1.Rows[dtl]["MarktMobileNo"].ToString();
                        txt_Marktemail.Text = dt1.Rows[dtl]["Marktemail"].ToString();
                        txt_MarktOfficePhoneNo.Text = dt1.Rows[dtl]["MarktOfficePhoneNo"].ToString();
                    }
                    else if (dt1.Rows[dtl]["Emptype"].ToString() == "Head")
                    {
                        txt_HeadName.Text = dt1.Rows[0]["MarktEngg"].ToString();
                        txt_HDesignation.Text = dt1.Rows[0]["Marktdesignation"].ToString();
                        txt_HMobileNo.Text = dt1.Rows[0]["MarktMobileNo"].ToString();
                        txt_HEmail.Text = dt1.Rows[0]["Marktemail"].ToString();
                    }
                    else if (dt1.Rows[dtl]["Emptype"].ToString() == "Subhead")
                    {
                        txt_SubHeadName.Text = dt1.Rows[0]["MarktEngg"].ToString();
                        txt_SHdesignation.Text = dt1.Rows[0]["Marktdesignation"].ToString();
                        txt_SHMobileNo.Text = dt1.Rows[0]["MarktMobileNo"].ToString();
                        txt_SHEmail.Text = dt1.Rows[0]["Marktemail"].ToString();
                    }
                }                                
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Hide Loader", "hideLoader();", true);
    }
    
    #endregion

    #region Common Methods

    private void ddlcustomerload()
    {
        try
        {
            DataSet ds = _objSales.GetCustomerslist();
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlcustomers.DataSource = ds.Tables[0];
                ddlcustomers.DataValueField = "CustomerID";
                ddlcustomers.DataTextField = "CustomerName";
                ddlcustomers.DataBind();
                ddlcustomers.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            ddlcustomers.Focus();
        }
        catch (Exception ec)
        {           
        }
    }
    private void bindAnnexures()
    {
        int[] anxrs = { 1, 3 };
        for (int i = 0; i < anxrs.Length; i++)
        {
            try
            {
                int anxrno = anxrs[i];
                GridView gv = new GridView();
                if(anxrno==1) gv = gvAnnexure1;
                else if (anxrno == 3) gv = gvAnnexure3;
                dsAnnexure = _objSales.GetAnnexure(anxrno);
                if (dsAnnexure.Tables[0].Rows.Count > 0)
                {
                    gv.DataSource = dsAnnexure.Tables[0];
                    gv.DataBind();
                    gv.UseAccessibleHeader = true;
                    gv.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                else
                {
                    gv.DataSource = "";
                    gv.DataBind();
                }
            }
            catch (Exception ex)
            {
                Log.Message(ex);
            }         
        }
    }
    protected void clearfields()
    {
        try
        {
            txt_FaxNo.Text = "";
            txt_CustomerphoneNo.Text = "";
            txt_CustomerEmail.Text = "";
            txt_ContactPerson.Text = "";
            txt_CustomerMobile.Text = "";
            txt_ContactPersonEmail.Text = "";


            txt_MarktEngg.Text = "";
            txt_Marktdesignation.Text = "";
            txt_MarktMobileNo.Text = "";
            txt_Marktemail.Text = "";
            txt_MarktOfficePhoneNo.Text = "";

            txt_HeadName.Text = "";
            txt_HDesignation.Text = "";
            txt_HMobileNo.Text = "";
            txt_HEmail.Text = "";


            txt_SubHeadName.Text = "";
            txt_SHdesignation.Text = "";
            txt_SHMobileNo.Text = "";
            txt_SHEmail.Text = "";
        }
        catch (Exception ec)
        {
        }
        
     }
    //protected void ddlsowpoints_IndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        DropDownList ddlsow = (DropDownList)sender;
    //        GridViewRow row = (GridViewRow)ddlsow.NamingContainer;
    //        Label lblPoint = (Label)row.FindControl("lblPoint");
    //        lblPoint.Text = ddlsow.SelectedValue;
    //    }
    //    catch (Exception ec) { }
    //}
    #endregion
}