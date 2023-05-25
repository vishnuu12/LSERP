using eplus.core;
using eplus.data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_DimensionsInspectionReport : System.Web.UI.Page
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
                bindReportNo();
            }
            if (target == "deleteDIRDID")
            {
                objQt = new cQuality();
                DataSet ds = new DataSet();
                objQt.DIRDID = Convert.ToInt32(arg.ToString());
                ds = objQt.DeleteDimensionInspectionReportdetailsByDIRDID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "SuccessMessage('Success','Dimension Details Deleted');", true);
                    GetDimensionInspectionReportDetails();
                }
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
                ds = objMat.GetItemDetailsByRFPHID(ddlItemName, "LS_GetItemDetailsByRFPHIDINDeflectionTestReportDetails");
                //bindDeflectionTestReportDetails();
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
            ds = objQt.GetBellowSnoByRFPDID();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvBellowSnoDetails.DataSource = ds.Tables[0];
                gvBellowSnoDetails.DataBind();
            }
            else
            {
                gvBellowSnoDetails.DataSource = "";
                gvBellowSnoDetails.DataBind();
            }
            //txtDrawingNo.Text = ds.Tables[0].Rows[0]["DrawingNo"].ToString();
            //txtProjectName.Text = ds.Tables[0].Rows[0]["Description"].ToString();
            //txtJobSize.Text = ds.Tables[0].Rows[0]["ItemSize"].ToString();
            //txtBellowSNo.Text = ds.Tables[0].Rows[0]["PartSno"].ToString();
            //txtItemName.Text = ddlItemName.SelectedItem.Text;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnGetDIR_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            GetDimensionInspectionReportDetails();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnPrintDIR_click(object sender, EventArgs e)
    {
        objQt = new cQuality();
        DataSet ds = new DataSet();
        try
        {
            objQt.RFPHID = Convert.ToInt32(ddlRFPNo.SelectedValue);
            objQt.RFPDID = Convert.ToInt32(ddlItemName.SelectedValue.Split('/')[0].ToString());
            ds = objQt.GetDimensionInspectionReportdetailsPrint();

            lblRFPNO_p.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
            lblCustomerName_p.Text = ddlCustomerName.SelectedItem.Text;
            lblPONo_p.Text = ds.Tables[0].Rows[0]["PORefNo"].ToString();
            lblProject_p.Text = ds.Tables[0].Rows[0]["ProjectName"].ToString();
            lblQAPNo_p.Text = ds.Tables[0].Rows[0]["QAPRefNo"].ToString();

            //  gvDimensions_p.AutoGenerateColumns = true;
            gvDimensions_p.DataSource = ds.Tables[2];
            gvDimensions_p.DataBind();

            string Address = ds.Tables[3].Rows[0]["Address"].ToString();
            string PhoneAndFaxNo = ds.Tables[3].Rows[0]["PhoneAndFaxNo"].ToString();
            string Email = ds.Tables[3].Rows[0]["Email"].ToString();
            string WebSite = ds.Tables[3].Rows[0]["WebSite"].ToString();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "PrintDIReport('" + Address + "','" + PhoneAndFaxNo + "','" + Email + "','" + WebSite + "');", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void bindReportNo()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            ds = objQt.GetQualityTestReportNo("LS_GetDimensionInspectionReportNo");

            lblReportNo.Text = ds.Tables[0].Rows[0]["ReportNo"].ToString();
            txtConvolutionOfRecords.Text = ds.Tables[0].Rows[0]["ControlID"].ToString();
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

    private void GetDimensionInspectionReportDetails()
    {
        DataSet ds = new DataSet();
        objQt = new cQuality();
        try
        {
            ds = objQt.GetDimensionInspectionReportDetails();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDimensionInspectionDetails.DataSource = ds.Tables[0];
                gvDimensionInspectionDetails.DataBind();
            }
            else
            {
                gvDimensionInspectionDetails.DataSource = "";
                gvDimensionInspectionDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    //[WebMethod(enableSession: true)]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    //public static string SaveDimensionInspectionReport(string Content)
    //{
    //    try
    //    {
    //        //This is where i am returning my data from DB  


    //        // return (ds.GetXml());
    //    }
    //    catch
    //    {
    //        return null;
    //    }

    //    return "";
    //}

    #region"Web Methods"
    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string SaveDimensionInspectionReport(string Content, string BellowNoColumn, string BslContent, string RFPHID, string RFPDID)
    {
        string ReturnMsg = "";
        string JSONString = "";
        DataSet ds = new DataSet();
        try
        {
            cDataAccess DAL = new cDataAccess();
            SqlCommand c = new SqlCommand();
            c.CommandType = CommandType.StoredProcedure;
            c.CommandText = "LS_SaveDimensionInspectionReport";
            c.Parameters.Add("@Content", Content);
            c.Parameters.Add("@BellowNoColumn", BellowNoColumn);
            c.Parameters.Add("@BslContent", BslContent);

            c.Parameters.Add("@RFPHID", RFPHID);
            c.Parameters.Add("@RFPDID", RFPDID);

            DAL.GetDataset(c, ref ds);
            JSONString = JsonConvert.SerializeObject(ds.Tables[0]);

            // ReturnMsg = ds.Tables[0].Rows[0]["Message"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return JSONString;
    }

    #endregion
}