using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eplus.core;
using System.Data;
using System.Globalization;

public partial class Pages_PreservationAndPackingInspectionReport : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cQuality objQt;
    cCommon objc;
    cSales objSales;
    cMaterials objMat;

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
                objc = new cCommon();
                DataSet dsPOHID = new DataSet();
                DataSet dsCustomer = new DataSet();
                DataSet dsLocation = new DataSet();

                dsCustomer = objc.getCustomerNameByUserID(Convert.ToInt32(objSession.employeeid), ddlCustomerName);
                dsPOHID = objc.GetCustomerPODetailsByUserID(Convert.ToInt32(objSession.employeeid), ddlCustomerPO);

                ViewState["Customer"] = dsCustomer.Tables[0];
                ViewState["PO"] = dsPOHID.Tables[0];
                ShowHideControls("input");
                //  bindRGSReportDetails();
            }

            if (target == "deleteVERID")
            {
                objQt = new cQuality();
                DataSet ds = objQt.DeleteQualityTestReportDetailsByPrimaryID("LS_DeletePreservationAndInspectionReportByPPIRID", arg.ToString());

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Records Deleted successfully');", true);
                    ddlRFPNo_SelectIndexChanged(null, null);
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

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        DataRow dr;
        try
        {
            if (ddlItemName.SelectedIndex > 0)
            {
                dt = new DataTable();

                dt.Columns.Add("PPIRDID");
                dt.Columns.Add("PPIRID");
                dt.Columns.Add("PartName");
                dt.Columns.Add("Size");
                dt.Columns.Add("BSLNo");
                dt.Columns.Add("QTY");

                if (gvItemDetails.Rows.Count != Convert.ToInt32(txtNumberOfRows.Text))
                {
                    if (gvItemDetails.Rows.Count > 0)
                    {
                        foreach (GridViewRow row in gvItemDetails.Rows)
                        {
                            string PPIRDID = gvItemDetails.DataKeys[row.RowIndex].Values[0].ToString();
                            string PPIRID = gvItemDetails.DataKeys[row.RowIndex].Values[1].ToString();
                            TextBox txtPartName = (TextBox)gvItemDetails.Rows[row.RowIndex].FindControl("txtPartName");
                            TextBox txtSize = (TextBox)gvItemDetails.Rows[row.RowIndex].FindControl("txtSize");
                            TextBox txtItemSno = (TextBox)gvItemDetails.Rows[row.RowIndex].FindControl("txtBSLNo");
                            TextBox txtQty = (TextBox)gvItemDetails.Rows[row.RowIndex].FindControl("txtQTY");
                            CheckBox chkpart = (CheckBox)gvItemDetails.Rows[row.RowIndex].FindControl("chkitems");

                            if (Convert.ToInt32(txtNumberOfRows.Text) > gvItemDetails.Rows.Count)
                            {
                                dr = dt.NewRow();
                                if (chkpart.Checked)
                                {
                                    dr = dt.NewRow();

                                    dr["PPIRDID"] = 0;
                                    dr["PPIRID"] = 0;
                                    dr["PartName"] = txtPartName.Text;
                                    dr["Size"] = txtSize.Text;
                                    dr["BSLNo"] = txtItemSno.Text;
                                    dr["QTY"] = txtPartName.Text;

                                    dt.Rows.Add(dr);
                                }
                            }
                            else
                            {
                                if (Convert.ToInt32(txtNumberOfRows.Text) > row.RowIndex)
                                {
                                    dr = dt.NewRow();

                                    dr["PPIRDID"] = 0;
                                    dr["PPIRID"] = 0;
                                    dr["PartName"] = txtPartName.Text;
                                    dr["Size"] = txtSize.Text;
                                    dr["BSLNo"] = txtItemSno.Text;
                                    dr["QTY"] = txtPartName.Text;

                                    dt.Rows.Add(dr);
                                }

                            }
                        }
                    }
                    if (Convert.ToInt32(txtNumberOfRows.Text) > gvItemDetails.Rows.Count)
                    {
                        for (int i = dt.Rows.Count; i < Convert.ToInt32(txtNumberOfRows.Text); i++)
                        {
                            dr = dt.NewRow();

                            dr["PPIRDID"] = 0;
                            dr["PPIRID"] = 0;
                            dr["PartName"] = "";
                            dr["Size"] = "";
                            dr["BSLNo"] = "";
                            dr["QTY"] = "";

                            dt.Rows.Add(dr);
                        }
                    }

                    gvItemDetails.DataSource = dt;
                    gvItemDetails.DataBind();
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Errror','Given rows Rows Cant same of Grid view rows');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Errror','Please select the Item');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }


    protected void btnSavePPIR_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        DataTable dt;
        try
        {
            objQt.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            objQt.RFPDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[0].ToString());
            objQt.PPIRID = hdnPPIRID.Value;
            objQt.ReportDate = DateTime.ParseExact(txtReportDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            objQt.ItemName = txtItemname.Text;
            objQt.DrawingNo = txtDrawingNo.Text;
            objQt.Customername = txtCustomername.Text;
            objQt.JobDescription = txtJobDescription.Text;
            objQt.PONo = txtPONo.Text;
            objQt.QAPNo = txtQAPNo.Text;
            objQt.CreatedBy = objSession.employeeid;
            objQt.RFPNo = txtRFPNo.Text;

            objQt.observed = txtRFPNo.Text;
            objQt.Packing = txtRFPNo.Text;
            objQt.Marking = txtRFPNo.Text;

            dt = new DataTable();
            DataRow dr;

            dt.Columns.Add("PPIRDID");
            dt.Columns.Add("PPIRID");
            dt.Columns.Add("PartName");
            dt.Columns.Add("Size");
            dt.Columns.Add("BSLNo");
            dt.Columns.Add("QTY");

            foreach (GridViewRow row in gvItemDetails.Rows)
            {
                string PPIRDID = gvItemDetails.DataKeys[row.RowIndex].Values[0].ToString();
                string PPIRID = gvItemDetails.DataKeys[row.RowIndex].Values[1].ToString();
                TextBox txtPartName = (TextBox)gvItemDetails.Rows[row.RowIndex].FindControl("txtPartName");
                TextBox txtSize = (TextBox)gvItemDetails.Rows[row.RowIndex].FindControl("txtSize");
                TextBox txtItemSno = (TextBox)gvItemDetails.Rows[row.RowIndex].FindControl("txtBSLNo");
                TextBox txtQty = (TextBox)gvItemDetails.Rows[row.RowIndex].FindControl("txtQTY");
                CheckBox chkpart = (CheckBox)gvItemDetails.Rows[row.RowIndex].FindControl("chkitems");

                dr = dt.NewRow();
                if (chkpart.Checked)
                {
                    dr = dt.NewRow();

                    dr["PPIRDID"] = PPIRDID;
                    dr["PPIRID"] = PPIRID;
                    dr["PartName"] = txtPartName.Text;
                    dr["Size"] = txtSize.Text;
                    dr["BSLNo"] = txtItemSno.Text;
                    dr["QTY"] = txtPartName.Text;

                    dt.Rows.Add(dr);
                }

            }
            objQt.dt = dt;
            ds = objQt.SavePreservationAndPackingInspectionReportDetails();

            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','PMI Report Saved successfully');", true);
                ddlRFPNo_SelectIndexChanged(null, null);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }


    #endregion

    #region"DropDown Events"

    protected void ddlCustomerPO_SelectIndexChanged(object sender, EventArgs e)
    {
        cDesign objDesign = new cDesign();
        objSales = new cSales();
        try
        {
            if (ddlCustomerPO.SelectedIndex > 0)
            {
                objDesign.POHID = Convert.ToInt32(ddlCustomerPO.SelectedValue);
                string ProspectID = objDesign.GetProspectNameByPOHID();
                ddlCustomerName.SelectedValue = ProspectID;

                objSales.POHID = ddlCustomerPO.SelectedValue;
                objSales.GetRFPDetailsByPOHID(ddlRFPNo);

                ShowHideControls("input,view");
            }
            else
            {
                ddlCustomerName.SelectedIndex = 0;
                ShowHideControls("input");
            }

            objc = new cCommon();
            objc.EmptyDropDownList(ddlItemName);
            ShowHideControls("input");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlCustomerName_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt;
        try
        {
            dt = (DataTable)ViewState["PO"];
            if (ddlCustomerName.SelectedIndex > 0)
            {
                string ProspectID = ddlCustomerName.SelectedValue;
                dt.DefaultView.RowFilter = "ProspectID='" + ProspectID + "'";
                dt.DefaultView.ToTable();
            }

            ddlCustomerPO.DataSource = dt;
            ddlCustomerPO.DataTextField = "PORefNo";
            ddlCustomerPO.DataValueField = "POHID";
            ddlCustomerPO.DataBind();
            ddlCustomerPO.Items.Insert(0, new ListItem("--Select--", "0"));

            objc = new cCommon();
            objc.EmptyDropDownList(ddlItemName);
            ShowHideControls("input");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlRFPNo_SelectIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        try
        {
            if (ddlRFPNo.SelectedIndex > 0)
            {
                objMat.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
                ds = objMat.GetItemDetailsByRFPHID(ddlItemName, "LS_GetItemDetailsByRadioGraphicExaminationReportHeader");

                bindWallThiningReportDetails();
                ShowHideControls("input,view");
            }
            else
            {
                objc = new cCommon();
                objc.EmptyDropDownList(ddlItemName);
                ShowHideControls("input");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlItemName_SelectIndexChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            objQt.RFPDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[0].ToString());
            objQt.EDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[1].ToString());
            ds = objQt.GetItemSpecificationDetailsByRFPDIDAndEDID(null, null, null);

            txtDrawingNo.Text = ds.Tables[0].Rows[0]["DrawingNo"].ToString();
            txtJobDescription.Text = ds.Tables[0].Rows[0]["Description"].ToString();
            //txtJobSize.Text = ds.Tables[0].Rows[0]["ItemSize"].ToString();
            //     txtBSLNo.Text = ds.Tables[0].Rows[0]["PartSno"].ToString();
            txtItemname.Text = ddlItemName.SelectedItem.Text;
            txtCustomername.Text = ddlCustomerName.SelectedItem.Text;

            bindWallThinningItemDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void bindWallThiningReportDetails()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            objQt.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            ds = objQt.GetTestReportDetailsByRFPHID("LS_GetPrservationAndPackingInspectionReportDetails");
            gvPPReportHeader.DataSource = ds.Tables[0];
            gvPPReportHeader.DataBind();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindWallThinningItemDetails()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            ds = objQt.GetQualityTestReportNo("LS_GetPrservationAndPackingInspectionItemDetails");

            gvItemDetails.DataSource = "";
            gvItemDetails.DataBind();

            lblReportNo.Text = ds.Tables[1].Rows[0]["ReportNo"].ToString();
            txtConvolutionOfRecords.Text = ds.Tables[1].Rows[0]["ControlID"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }




    private void ShowHideControls(string divids)
    {
        divInput.Visible = divOutPut.Visible = false;
        string[] mode = divids.Split(',');
        try
        {
            for (int i = 0; i < mode.Length; i++)
            {
                switch (mode[i].ToLower())
                {
                    case "view":
                        divOutPut.Visible = true;
                        break;
                    case "input":
                        divInput.Visible = true;
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
}