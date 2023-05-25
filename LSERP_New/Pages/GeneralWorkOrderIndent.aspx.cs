using eplus.core;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_GeneralWorkOrderIndent : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    GeneralWorkOrderIndent gwoi;
    string gwoiDocsSavepath = ConfigurationManager.AppSettings["PDFSavePath"].ToString();
     string gwoiDocsSavepathHttpPath = "http://183.82.33.21/LSERPDocs/PDFFiles/GSTDocs/";

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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "showDataTable();", true);
            BindTestGeneralWOI();
            hdnGWOID.Value = "0";
            BindDropDownDetails();
            ShowHideControls("view");

        }
        else
        {
            if (target == "deletegvrow")
            {
                gwoi = new GeneralWorkOrderIndent();
                gwoi.GWOID = Convert.ToInt32(arg);

                DataSet ds = gwoi.DeleteGeneralWorkOrderDetails();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Row Deleted successfully');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Row Details Not Deleted');", true);
                BindTestGeneralWOI();

            }
            if (target == "ViewIndentAttach")
            {
                //viewWorkOrderDrawingFile(arg);
                int index = Convert.ToInt32(arg);
                ViewState["GWOID"] = gvGeneralWorkOrder.DataKeys[index].Values[0].ToString();
                ViewState["FileName"] = gvGeneralWorkOrder.DataKeys[index].Values[1].ToString();
                ViewDrawingFilename();
            }
            if (target == "deletegvrowfor")
            {
                gwoi = new GeneralWorkOrderIndent();
                gwoi.SGWOID = Convert.ToInt32(arg);

                DataSet ds = gwoi.DeleteGeneralWorkOrderDetailsforSGWOID();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Row Deleted successfully');", true);
                    bindGWIndentDetailsByGWOID();
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Row Details Not Deleted');", true);
                BindTestGeneralWOI();

            }
        }  

    }

    #endregion

    #region"Button Events"

    protected void btnEditSave_Click(object sender, EventArgs e)
    {
        DataSet dsa = new DataSet();

        gwoi = new GeneralWorkOrderIndent();
        try
        {
            gwoi.SGWOID = Convert.ToInt32(hdnGWOID.Value);
            gwoi.txtsdescription1 = txtsdescription1.Text;
            gwoi.GWONO = GWONO.Text;
            gwoi.GWIId = GWIId.Text;
            gwoi.txtsremark1 = txtsremark1.Text;
            gwoi.txtquantity1 = txtquantity1.Text;
            gwoi.ddlUOM1 = Convert.ToInt32(ddlUOM1.SelectedValue);
            gwoi.LiJobList1 = Convert.ToInt32(LiJobList1.SelectedValue);
            gwoi.employeeid = Convert.ToInt32(objSession.employeeid);
            string AttachmentName1 = "";

            if (FileUpload1.HasFile)
            {
                string extn = Path.GetExtension(FileUpload1.PostedFile.FileName).ToUpper();
                AttachmentName1 = Path.GetFileName(FileUpload1.PostedFile.FileName);
            }
            string[] extension = AttachmentName1.Split('.');
            AttachmentName1 = extension[0] + '_' + txtsdescription1.Text + '.' + extension[extension.Length - 1];

            string gwoiDocsSavepathstr = gwoiDocsSavepath + "GSTDocs" + "\\";

            if (!Directory.Exists(gwoiDocsSavepathstr))
                Directory.CreateDirectory(gwoiDocsSavepathstr);

            if (AttachmentName1 != "")
                FileUpload1.SaveAs(gwoiDocsSavepathstr + AttachmentName1);
            gwoi.FileUpload1 = AttachmentName1;



            dsa = gwoi.SaveGeneralWorkOrderIndentforGWOID();

            if (dsa.Tables[0].Rows[0]["Message"].ToString() == "Added")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Records Saved Succeessfully');", true);

            }
            else if (dsa.Tables[0].Rows[0]["Message"].ToString() == "Updated") { 
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "SuccessMessage('Success','Records Updated Successfully');hideLoader();", true);
               
            }
            ClearEditValues();
            bindGWIndentDetailsByGWOID();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataSet dsa = new DataSet();

        gwoi = new GeneralWorkOrderIndent();
        try
       {
                    gwoi.GWOID = Convert.ToInt32(hdnGWOID.Value);
                   gwoi.txtsdescription = txtsdescription.Text;
                    gwoi.txtsremark = txtsremark.Text;
                    gwoi.txtquantity = txtquantity.Text;
           gwoi.ddlUOM = Convert.ToInt32(ddlUOM.SelectedValue);
           gwoi.LiJobList = Convert.ToInt32(LiJobList.SelectedValue);
            gwoi.ddlIndentTo = Convert.ToInt32(ddlIndentTo.SelectedValue);
            gwoi.employeeid = Convert.ToInt32(objSession.employeeid);
            string AttachmentName = "";

            if (documentUpload.HasFile)
            {
                string extn = Path.GetExtension(documentUpload.PostedFile.FileName).ToUpper();
                AttachmentName = Path.GetFileName(documentUpload.PostedFile.FileName);
            }
            string[] extension = AttachmentName.Split('.');
            AttachmentName = extension[0] + '_' + txtsdescription.Text + '.' + extension[extension.Length - 1];

            string gwoiDocsSavepathstr = gwoiDocsSavepath + "GSTDocs" + "\\";

            if (!Directory.Exists(gwoiDocsSavepathstr))
                Directory.CreateDirectory(gwoiDocsSavepathstr);

            if (AttachmentName != "")
                documentUpload.SaveAs(gwoiDocsSavepathstr + AttachmentName);
            gwoi.documentUpload = AttachmentName;



            dsa = gwoi.SaveGeneralWorkOrderIndent();

                    if (dsa.Tables[0].Rows[0]["Message"].ToString() == "Added")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Delete", "SuccessMessage('Success','Records Saved Succeessfully');", true);

                    else if (dsa.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "SuccessMessage('Success','Records Updated Successfully');hideLoader();", true);

                    ClearValues();
                    BindTestGeneralWOI();
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
            ClearValues();
            hdnGWOID.Value = "0";

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
       }
   }
    protected void btnEditCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ClearEditValues();
            hdnGWOID.Value = "0";

            BindTestGeneralWOI();
            ShowHideControls("view");

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }





    #endregion   

    #region"gridView Events"

    protected void gvGeneralWorkOrder_RowDataCommand(object sender, GridViewRowEventArgs e)
    {
        try
        {
            
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvViewAllGWDetails_RowDataCommand(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton btnedit = (LinkButton)e.Row.FindControl("lbtnEditfor");
                LinkButton btnDelete = (LinkButton)e.Row.FindControl("lbtnDeletefor");

                if (objSession.type == 1)
                {
                    btnedit.Visible = true;
                    btnDelete.Visible = true;
                }

                else
                {
                    btnedit.Visible = false;
                    btnDelete.Visible = false;
                }



            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    protected void gvGeneralWorkOrder_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        gwoi = new GeneralWorkOrderIndent();
        int index;
        try
        {
            index = Convert.ToInt32(e.CommandArgument.ToString());
            hdnGWOID.Value = gvGeneralWorkOrder.DataKeys[index].Values[0].ToString();
            gwoi.GWOID = Convert.ToInt32(hdnGWOID.Value);
            if (e.CommandName.ToString() == "EditGeneralWorkOrder")
            {
                DataTable dt = (DataTable)ViewState["GeneralWOIndentDetails"];
                dt.DefaultView.RowFilter = "GWOID='" + gwoi.GWOID + "'";
                hdnGWOID.Value = gwoi.GWOID.ToString();
                txtsdescription.Text = dt.DefaultView.ToTable().Rows[0]["ServiceDescription"].ToString();
                txtsremark.Text = dt.DefaultView.ToTable().Rows[0]["Remarks"].ToString();
                ddlUOM.SelectedValue = dt.DefaultView.ToTable().Rows[0]["QuantityUnitId"].ToString();
                LiJobList.SelectedValue = dt.DefaultView.ToTable().Rows[0]["JobListId"].ToString();
                ddlIndentTo.SelectedValue = dt.DefaultView.ToTable().Rows[0]["IndentTo"].ToString();
                txtquantity.Text = dt.DefaultView.ToTable().Rows[0]["QuantityUnit"].ToString();

                if (this.gvGeneralWorkOrder.Rows.Count > 0)
                {
                    gvGeneralWorkOrder.UseAccessibleHeader = true;
                    gvGeneralWorkOrder.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "showDataTable();", true);
                BindForEditDropDownDetails();

            }
            else if (e.CommandName.ToString() == "ViewGWIndent")
            {
                bindGWIndentDetailsByGWOID();
                DataTable dt = (DataTable)ViewState["GeneralAllWOIndentDetails"];
                hdnGWOID.Value = gwoi.GWOID.ToString();

                lblGWOI.Text = dt.DefaultView.ToTable().Rows[0]["GWI"].ToString() + ' ' + dt.DefaultView.ToTable().Rows[0]["GWOIDate"].ToString();
                GWIId.Text = dt.DefaultView.ToTable().Rows[0]["GWOID"].ToString();
                GWONO.Text = dt.DefaultView.ToTable().Rows[0]["GWI"].ToString();
                BindDropDownDetailsforGWOID();

                ShowHideControls("edit");


                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowIndentPopUp();", true);

            }
          
            else
            {
                gvGeneralWorkOrder.UseAccessibleHeader = true;
                gvGeneralWorkOrder.HeaderRow.TableSection = TableRowSection.TableHeader;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','You Cannot Edit The Record');showDataTable();", true);

            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvViewAllGWDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataSet ds = new DataSet();
        gwoi = new GeneralWorkOrderIndent();
        int index;
        try
        {
            index = Convert.ToInt32(e.CommandArgument.ToString());
            hdnGWOID.Value = gvViewAllGWDetails.DataKeys[index].Values[0].ToString();
            gwoi.SGWOID = Convert.ToInt32(hdnGWOID.Value);
            if (e.CommandName.ToString() == "EditGeneralWorkOrderfor")
            {
                DataTable dt = (DataTable)ViewState["GeneralAllWOIndentDetails"];
                dt.DefaultView.RowFilter = "SGWOID='" + gwoi.SGWOID + "'";
                hdnGWOID.Value = gwoi.SGWOID.ToString();
                txtsdescription1.Text = dt.DefaultView.ToTable().Rows[0]["ServiceDescription"].ToString();
                txtsremark1.Text = dt.DefaultView.ToTable().Rows[0]["Remarks"].ToString();
                ddlUOM1.SelectedValue = dt.DefaultView.ToTable().Rows[0]["QuantityUnitId"].ToString();
                LiJobList1.SelectedValue = dt.DefaultView.ToTable().Rows[0]["JobListId"].ToString();
                txtquantity1.Text = dt.DefaultView.ToTable().Rows[0]["QuantityUnit"].ToString();

                if (this.gvViewAllGWDetails.Rows.Count > 0)
                {
                    gvViewAllGWDetails.UseAccessibleHeader = true;
                    gvViewAllGWDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowAdd", "showDataTable();", true);
                BindDropDownDetailsforGWOID();
            }
            else
            {
                gvViewAllGWDetails.UseAccessibleHeader = true;
                gvViewAllGWDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "InfoMessage('Information','You Cannot Edit The Record');showDataTable();", true);

            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }


    #endregion

    #region"DropDown Events"

    #endregion

    #region"Common Methods"

    private void viewWorkOrderDrawingFile(string index)
    {
        cCommon objc = new cCommon();
        try
        {
            string FileName = gvGeneralWorkOrder.DataKeys[Convert.ToInt32(index)].Values[1].ToString();
             //objc.ViewFileName(gwoiDocsSavepath, gwoiDocsSavepathHttpPath, FileName, "RFPDispatchDocs", ifrm);//
            cCommon.DownLoad(FileName, gwoiDocsSavepath + "Documents" + "\\" + FileName);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void bindGWIndentDetailsByGWOID()
    {
        DataSet ds = new DataSet();
        gwoi = new GeneralWorkOrderIndent();
        try
        {
            gwoi.GWOID = Convert.ToInt32(hdnGWOID.Value);
            gwoi.PageNameFlag = "ViewAllGWI";
            ds = gwoi.GetGWIndentDetailsByGWOID();
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvViewAllGWDetails.DataSource = ds.Tables[0];
                gvViewAllGWDetails.DataBind();
                ViewState["GeneralAllWOIndentDetails"] = ds.Tables[0];
                gvViewAllGWDetails.UseAccessibleHeader = true;
                gvViewAllGWDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                BindForEditDropDownDetails();
                divInput1.Visible = true;
                txtsdescription1.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "ShowDataTable();", true);
            }
            else
            {
                gvGeneralWorkOrder.DataSource = "";
                gvGeneralWorkOrder.DataBind();

                gvGeneralWorkOrder.DataSource = "";
                gvGeneralWorkOrder.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindDropDownDetailsforGWOID()
    {
        gwoi = new GeneralWorkOrderIndent();
        DataSet ds = new DataSet();
        try
        {
            gwoi.EmpID = Convert.ToInt32(objSession.employeeid);

            gwoi.GetUnitsDetails(ddlUOM1);
            gwoi.GetJobOperationListDetails(LiJobList1);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    private void BindDropDownDetails()
    {
        gwoi = new GeneralWorkOrderIndent();
        DataSet ds = new DataSet();
        try
        {
            gwoi.EmpID = Convert.ToInt32(objSession.employeeid);

            gwoi.GetUnitsDetails(ddlUOM);
            gwoi.GetIndentToDetails(ddlIndentTo);
            gwoi.GetJobOperationListDetails(LiJobList);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }
    private void BindForEditDropDownDetails()
    {
        gwoi = new GeneralWorkOrderIndent();
        DataSet ds = new DataSet();
        try
        {
            gwoi.EmpID = Convert.ToInt32(objSession.employeeid);

            gwoi.GetUnitsDetails(ddlUOM);
            gwoi.GetIndentToDetails(ddlIndentTo);
            gwoi.GetJobOperationListDetails(LiJobList);

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindTestGeneralWOI()
    {
        DataSet ds = new DataSet();
        gwoi = new GeneralWorkOrderIndent();
        try
        {
            ds = gwoi.GetGeneralWOIndent();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvGeneralWorkOrder.DataSource = ds.Tables[0];
                gvGeneralWorkOrder.DataBind();
                ViewState["GeneralWOIndentDetails"] = ds.Tables[0];
                gvGeneralWorkOrder.UseAccessibleHeader = true;
                gvGeneralWorkOrder.HeaderRow.TableSection = TableRowSection.TableHeader;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "ShowDataTable();", true);
            }
            else
            {
                gvGeneralWorkOrder.DataSource = "";
                gvGeneralWorkOrder.DataBind();
            }

        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void ViewDrawingFilename()
    {
        try
        {
            cCommon objc = new cCommon();
            objc.GeneralViewFileName(gwoiDocsSavepath, gwoiDocsSavepathHttpPath, ViewState["FileName"].ToString(), ViewState["GWOID"].ToString(), ifrm);
            //cCommon.DownLoad(ViewState["FileName"].ToString(), gwoiDocsSavepath + "RFPDispatchDocs" + "\\" + ViewState["FileName"].ToString());
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
            divAdd.Visible = divInput.Visible = divOutput.Visible = divInput1.Visible = divOutput1.Visible = false;
            switch (mode.ToLower())
            {
                case "edit":
                    divInput1.Visible = divOutput1.Visible = true;
                    break;
                case "view":
                    divInput.Visible = divOutput.Visible = true;
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
        hdnGWOID.Value = "0";
        txtsdescription.Text = "";
        txtquantity.Text = "";
        txtsremark.Text = "";
        ddlUOM.SelectedValue = "0";
        LiJobList.SelectedValue = "0";
        ddlIndentTo.SelectedValue = "0";


    }

    private void ClearEditValues()
    {
        hdnGWOID.Value = "0";
        txtsdescription1.Text = "";
        txtquantity1.Text = "";
        txtsremark1.Text = "";
        ddlUOM1.SelectedValue = "0";
        LiJobList1.SelectedValue = "0";
        ddlIndentTo.SelectedValue = "0";


    }

    #endregion
}