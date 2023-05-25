﻿using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Globalization;
using eplus.data;

public partial class UserLoginDetails : System.Web.UI.Page
{

    #region Declaration

    cSession _objSession = new cSession();
    cCommonMaster _objCommon = new cCommonMaster();

    #endregion

    #region Page Load

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;
        try
        {
            txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            
        }
        catch (Exception ex)
        {
            Log.Message(ex.Message);
        }
    }

    
    #endregion

    #region Button Events

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            divLoginDetails.Visible = true;
            //trUserLoginDetails.Visible = true;

            _objCommon = new cCommonMaster();
            _objCommon.fromDate = Convert.ToString(DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
            _objCommon.toDate = Convert.ToString(DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
            DataSet ds = _objCommon.getUserLoginData();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvUserLoginDetails.DataSource = ds.Tables[0];
                gvUserLoginDetails.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "DataTable();", true);
                gvUserLoginDetails.UseAccessibleHeader = true;
                gvUserLoginDetails.HeaderRow.TableSection = TableRowSection.TableHeader;

            }
            else
            {
                gvUserLoginDetails.DataSource = "";
                gvUserLoginDetails.DataBind();
            }

        }
        catch (Exception ex)
        {
            Log.Message(ex.Message);
        }
    }


    #endregion

}