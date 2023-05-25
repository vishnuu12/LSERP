using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;

public partial class Pages_GeneralWOInward : System.Web.UI.Page
{
    #region"Declaration"

    cStores objSt;
    cSession objSession = new cSession();
    GeneralWorkOrderIndentApproval gwoia;
    string gwoiDocsSavepath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
    string gwoiDocsSavepathHttpPath = ConfigurationManager.AppSettings["PDFPath"].ToString();

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
        string target = Request["__EVENTTARGET"];
        string arg = Request["__EVENTARGUMENT"];

        if (IsPostBack == false)
        {
            // Do Something
            BindGeneralWorkOrderInwardfor(0);
            ShowHideControls("Radio");
            ShowHideControls("view");

        }
        if (target == "DeleteGWOIH")
        {
            DeleteGWOInward(Convert.ToInt32(arg.ToString()));
        }
        if (target == "ShareInwardDC")
        {
            ShareGWOInwardDC();
        }

    }

    #endregion

    #region"Radio Events"

    protected void rblGWPONoChange_OnSelectedChanged(object sender, EventArgs e)
    {
        BindGeneralWorkOrderInward();
    }

    #endregion

    #region"Button Events"
    protected void btnSaveMI_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataTable dt;
        gwoia = new GeneralWorkOrderIndentApproval();
        try
        {
            
            gwoia.ChargesType = hdnGIHD.Value;
            if (gwoia.ChargesType != "")
            {
                gwoia.GIHD = Convert.ToInt32(hdnGIHD.Value);
            }
            else {
                gwoia.GIHD = 0;
            }
            gwoia.GDCID = Convert.ToInt32(hdnGDCID.Value);
            gwoia.GWPOID = Convert.ToInt32(hdnGWPOID.Value);
            gwoia.DCNo = txtDCNumber.Text;
            gwoia.DCDate = txtDCDate.Text;
            gwoia.EwayBillNo = txtEwayBillNo.Text;
            gwoia.UserID = Convert.ToInt32(objSession.employeeid);


            if (FileUp.HasFile)
            {
                cSales objSales = new cSales();
                gwoia.Foldername = Session["StoresDocsSavePath"].ToString();
                string Name = Path.GetFileName(FileUp.PostedFile.FileName);
                string MaximumAttacheID = objSales.GetMaximumAttachementID();
                string[] extension = Name.Split('.');
                Name = extension[0] + '_' + MaximumAttacheID + '.' + extension[1];
                gwoia.FileName = Name;
                gwoia.PID = "WororderInwardDCFile";
                gwoia.AttachementControl = FileUp;
                gwoia.SaveFiles();
                gwoia.DCCopy = Name;
            }
            else
                gwoia.DCCopy = null;

            ds = gwoia.SaveGeneralMaterialInward();
            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added") { 
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "SuccessMessage('Success','Records Saved Succeessfully');", true);
                BindGeneralWorkOrderInwardfor(0);
                ShowHideControls("Radio");
                ShowHideControls("view");
                ClearValues();
            }

            else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated") { 
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "SuccessMessage('Success','Records Updated Successfully');hideLoader();", true);
                BindGeneralWorkOrderInwardfor(0);
                ShowHideControls("Radio");
                ShowHideControls("view");
                ClearValues();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSaveGWOIDetails_Click(object sender, EventArgs e)
    {
        try
        {
            DataSet ds = new DataSet();
            gwoia = new GeneralWorkOrderIndentApproval();
            foreach (GridViewRow row in gvGeneralMaterialInwardDetails.Rows)
            {
                gwoia.GWOIDAID = Convert.ToInt32(hdnGIHD.Value);
                gwoia.GDCID = Convert.ToInt32(hdnGDCID.Value);
                gwoia.GWPOID = Convert.ToInt32(hdnGWPOID.Value);
                Label lblAvailPOQty = (Label)row.FindControl("lblAvailPOQty");
                gwoia.lblDCQty = lblAvailPOQty.Text;
                TextBox InwardQty = (TextBox)row.FindControl("txtInwardQty");
                gwoia.InwardQty = Convert.ToInt32(InwardQty.Text);
                ds = gwoia.SaveGeneralWorkOrderInwardQty();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "SuccessMessage('Success','Records Updated Successfully');hideLoader();", true);
                    
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Error');", true);

            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            hdnGDCID.Value = "0";
            hdnGWPOID.Value = "0";
            ShowHideControls("radio");
            ShowHideControls("view");
            ClearValues();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvGeneralWorkOrderInward_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable dt;
        try
        {
            int index = Convert.ToInt32(e.CommandArgument.ToString());
            hdnGIHD.Value = gvGeneralWorkOrderInward.DataKeys[index].Values[0].ToString();
            hdnGDCID.Value = gvGeneralWorkOrderInward.DataKeys[index].Values[1].ToString();
            hdnGWPOID.Value = gvGeneralWorkOrderInward.DataKeys[index].Values[2].ToString();
            if (e.CommandName == "Add")
            {

                string s = hdnGIHD.Value;
                if (s != "") {
                    int f = Convert.ToInt32(hdnGIHD.Value);
                    bindGWInwardDetailsForModal(f);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ShowAddPopUp();", true);
                } else {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "ErrorMessage('Error','Please Add Supplier DC');", true);
                }
            }

            if (e.CommandName == "SupplierDC")
            {
                ShowHideControls("Input");
            }
            if (e.CommandName == "EDitGWI")
            {
                Label lblSupplierDCNo = (Label)gvGeneralWorkOrderInward.Rows[index].FindControl("lblSupplierDCNo");
                Label lblSupplierDCDate = (Label)gvGeneralWorkOrderInward.Rows[index].FindControl("lblSupplierDCDate");
                Label lblSupplierEWayBillNo = (Label)gvGeneralWorkOrderInward.Rows[index].FindControl("lblSupplierEWayBillNo");
                txtDCNumber.Text = lblSupplierDCNo.Text.ToString();
                txtDCDate.Text = lblSupplierDCDate.Text.ToString();
                txtEwayBillNo.Text = lblSupplierEWayBillNo.Text.ToString();
                ShowHideControls("Input");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    protected void gvGeneralMaterialInwardDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnAllow = (LinkButton)e.Row.FindControl("btnAllow");

                if (objSession.type == 1)
                    btnAllow.Visible = true;
                else
                    btnAllow.Visible = false;

                if (dr[8].ToString() == "0")
                    btnSaveGWOIDetails.Visible = false;
                // btnAllow.Text = "Allow";
                else
                    btnSaveGWOIDetails.Visible = true;
                //btnAllow.Text = "Un Allow";


            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void DeleteGWOInward(int GHID)
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        try
        {
            int GWOIDA = GHID;
            gwoia = new GeneralWorkOrderIndentApproval();
            ds = gwoia.generalDeleteGWOInward(GWOIDA);
            if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "SuccessMessage('Success','Records Deleted Succeessfully');", true);
                BindGeneralWorkOrderInwardfor(0);
                ShowHideControls("Radio");
                ShowHideControls("view");
                ClearValues();
            }

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ShareGWOInwardDC()
    {
        DataSet ds = new DataSet();
        gwoia = new GeneralWorkOrderIndentApproval();
        try
        {
           
            if (gvGeneralMaterialInwardDetails.Rows.Count > 0)
            {
                gwoia.GDCID = Convert.ToInt32(hdnGDCID.Value);
                ds = gwoia.generalShareGWOInwardDC();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "SuccessMessage('Success','DC Shared Successfully');HideDCPopup();", true);
                    
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "InfoMessage('Information','" + ds.Tables[0].Rows[0]["Message"].ToString() + "');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "InfoMessage('Information','DC Has No records');", true);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindGeneralWorkOrderInwardfor(int a)
    {
        gwoia = new GeneralWorkOrderIndentApproval();
        DataSet ds = new DataSet();
        try
        {
            string b = a.ToString();
            ds = gwoia.GetGeneralWorkOrderInward(b);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvGeneralWorkOrderInward.DataSource = ds.Tables[0];
                gvGeneralWorkOrderInward.DataBind();
            }
            else
            {
                gvGeneralWorkOrderInward.DataSource = "";
                gvGeneralWorkOrderInward.DataBind();
            }

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindGWInwardDetailsForModal(int c)
    {
        DataSet ds = new DataSet();
        gwoia = new GeneralWorkOrderIndentApproval();
        try
        {
            int d = c;
            ds = gwoia.GetGWInwardDetailsForModal(d);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvGeneralMaterialInwardDetails.DataSource = ds.Tables[0];
                gvGeneralMaterialInwardDetails.DataBind();
            }
            else
            {
                gvGeneralMaterialInwardDetails.DataSource = "";
                gvGeneralMaterialInwardDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }


    private void BindGeneralWorkOrderInward()
    {
        gwoia = new GeneralWorkOrderIndentApproval();
        DataSet ds = new DataSet();
        try
        {
            ds = gwoia.GetGeneralWorkOrderInward(rblGWPONoChange.SelectedValue);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvGeneralWorkOrderInward.DataSource = ds.Tables[0];
                gvGeneralWorkOrderInward.DataBind();
            }
            else
            {
                gvGeneralWorkOrderInward.DataSource = "";
                gvGeneralWorkOrderInward.DataBind();
            }

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ClearValues()
    {
        hdnGIHD.Value = "0";
        txtDCNumber.Text = "";
        txtDCDate.Text = "";
        txtEwayBillNo.Text = "";

    }
    private void ShowHideControls(string mode)
    {
        try
        {
            divRadio.Visible = divInput.Visible = divOutput.Visible = false;
            switch (mode.ToLower())
            {
                case "radio":
                    divRadio.Visible = true;
                    break;
                case "input":
                    divInput.Visible = true;
                    txtDCNumber.Focus();
                    break;
                case "view":
                    divRadio.Visible = divOutput.Visible = true;
                    break;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }


    #endregion
}