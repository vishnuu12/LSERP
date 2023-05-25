using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using eplus.core;


public partial class Pages_DrawingBOM : System.Web.UI.Page
{
    #region"Declaration"

    cSession objSession = new cSession();
    cMaterials objMat = new cMaterials();
    string DrawingDocumentSavePath = ConfigurationManager.AppSettings["CustomerEnquirySavePath"].ToString();
    string DrawingDocumentHttppath = ConfigurationManager.AppSettings["CustomerEnquiry"].ToString();

    cCommon objc = new cCommon();

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
                objMat.GetEnquiryNumber(ddlEnquiryNumber);
                ShowHideControls("view");
            }
            else
            {
                if (target == "deletegvrow")
                {
                    DataSet ds = new DataSet();
                    int BOMID = Convert.ToInt32(arg);
                    ds = objMat.DeleteBOMDetailsByBOMID(BOMID);
                    if (ds.Tables[0].Rows[0]["Message"].ToString() == "Deleted")
                    {
                        gvDrawingBOMDetails.UseAccessibleHeader = true;
                        gvDrawingBOMDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "SuccessMessage('Success','BOM Deleted successfully');showDataTable();", true);
                    }
                    BindBOMDetails();
                    ShowHideControls("View");
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

    protected void ddlEnquiryNumber_SelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlEnquiryNumber.SelectedIndex > 0)
            {
                objMat.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue);
                objMat.GetDrawingVersionNumberbyEnquiryNumber(ddlVersionNumber);
            }
            else
            {
                ddlVersionNumber.DataSource = "";
                ddlVersionNumber.DataBind();
                ddlVersionNumber.Items.Insert(0, new ListItem("--Select--", "0"));
            }

            divViewDrawing.Visible = false;
            ShowHideControls("divadd");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "hide", "hideLoader();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void ddlVersionNumber_SelectIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlVersionNumber.SelectedIndex > 0)
            {
               // string BasehttpPath = DrawingDocumentSavePath + ddlEnquiryNumber.SelectedValue + "\\";

                string BasehttpPath = DrawingDocumentHttppath + ddlEnquiryNumber.SelectedValue + "/";

                string FileName = BasehttpPath + ddlVersionNumber.SelectedValue;

                byte[] imageBytes = System.IO.File.ReadAllBytes(FileName);
                string base64String = Convert.ToBase64String(imageBytes);
                imgViewDrawing.ImageUrl = String.Format(@"data:image/jpeg;base64,{0}", base64String);
                imgViewDrawing.ToolTip = ddlVersionNumber.SelectedValue;
                divViewDrawing.Visible = true;

                BindBOMDetails();
                ShowHideControls("view");
            }
            else
            {
                divViewDrawing.Visible = false;
                ShowHideControls("divadd");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "hideLoader();", true);
            }

            if (gvDrawingBOMDetails.Rows.Count > 0)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "showDataTable();hideLoader();", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "hideLoader();", true);
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
        try
        {
            hdnBOMId.Value = "0";
            objMat.GetMaterialList(ddlMaterialName);
            BindDrawingSequenceNumber();
            ShowHideControls("add");
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        try
        {
            if (ValidateControl())
            {
                objMat.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue);
                objMat.versionNumber = Convert.ToInt32(ddlVersionNumber.SelectedItem.Text);
                objMat.MID = Convert.ToInt32(ddlMaterialName.SelectedValue);
                objMat.Quantity = Convert.ToInt32(txtQuantity.Text);
                objMat.BOMID = Convert.ToInt32(hdnBOMId.Value);
                objMat.DrawingSequenceNumber = Convert.ToInt32(ddlDrawingSequenceNumber.SelectedValue);

                ds = objMat.SaveDrawingBOMMaterialDetails();

                if (ds.Tables[0].Rows[0]["Message"].ToString() == "Added")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','BOM Saved Successfully');hideLoader();showDataTable();", true);
                else if (ds.Tables[0].Rows[0]["Message"].ToString() == "AE")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Info", "InfoMessage('Information','Meterial Name Already Exists In This Enquiry Number');hideLoader();showDataTable();", true);
                else if (ds.Tables[0].Rows[0]["Message"].ToString() == "Updated")
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Sucess", "SuccessMessage('Success','BOM Updated Successfully');hideLoader();showDataTable();", true);

                BindBOMDetails();
                BindDrawingSequenceNumber();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void imgViewDrawing_Click(object sender, EventArgs e)
    {
        try
        {
            // string BasehttpPath = DrawingDocumentSavePath + ddlEnquiryNumber.SelectedValue + "\\";
            string BasehttpPath = DrawingDocumentHttppath + ddlEnquiryNumber.SelectedValue + "/";
            string FileName = BasehttpPath + ddlVersionNumber.SelectedValue;

            byte[] imageBytes = System.IO.File.ReadAllBytes(FileName);
            string base64String = Convert.ToBase64String(imageBytes);
            imgDocs.ImageUrl = String.Format(@"data:image/jpeg;base64,{0}", base64String);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUp", "ShowViewPopUp();", true);
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
            ddlMaterialName.SelectedIndex = 0;
            ddlDrawingSequenceNumber.SelectedIndex = 0;
            txtQuantity.Text = "";
            ShowHideControls("view");
            gvDrawingBOMDetails.UseAccessibleHeader = true;
            gvDrawingBOMDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "DataTable", "showDataTable();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"GridView Events"

    protected void gvDrawingBOMDetails_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            DataRowView dr = (DataRowView)e.Row.DataItem;

            LinkButton lbtnEdit = (LinkButton)e.Row.FindControl("lbtnEdit");
            LinkButton lbtnDelete = (LinkButton)e.Row.FindControl("lbtnDelete");
            if (dr["Flag"].ToString() == "1")
            {
                lbtnEdit.Enabled = lbtnDelete.Enabled = false;
                lbtnEdit.ToolTip = lbtnDelete.ToolTip = "BOM Already Uses";
            }
            else
            {
                lbtnEdit.ToolTip = "Edit";
                lbtnDelete.ToolTip = "Delete";
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    protected void gvDrawingBOMDetails_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        objMat = new cMaterials();
        DataSet ds = new DataSet();
        int BOMID;
        int index;
        try
        {
            index = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "EditBOM")
            {
                hdnBOMId.Value = gvDrawingBOMDetails.DataKeys[index].Values[0].ToString();
                BOMID = Convert.ToInt32(hdnBOMId.Value);
                ds = objMat.GetBOMDetailsByBOMID(BOMID);

                ddlMaterialName.SelectedValue = ds.Tables[0].Rows[0]["MID"].ToString();
                txtQuantity.Text = ds.Tables[0].Rows[0]["Quantity"].ToString();

                BindDrawingSequenceNumber();

                ShowHideControls("add");
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindBOMDetails()
    {
        objMat = new cMaterials();
        DataSet ds = new DataSet();
        try
        {
            objMat.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue);
            objMat.versionNumber = Convert.ToInt32(ddlVersionNumber.SelectedItem.Text);

            ds = objMat.GetBOMDetailsbyEnquiryNumberAndVersionnumber();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvDrawingBOMDetails.DataSource = ds.Tables[0];
                gvDrawingBOMDetails.DataBind();
                gvDrawingBOMDetails.UseAccessibleHeader = true;
                gvDrawingBOMDetails.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            else
            {
                gvDrawingBOMDetails.DataSource = "";
                gvDrawingBOMDetails.DataBind();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private bool ValidateControl()
    {
        bool isValid = true;
        string error = "";
        try
        {
            if (ddlMaterialName.SelectedIndex == 0)
                error = ddlMaterialName.ClientID + '/' + "Field Required";
            else if (txtQuantity.Text == "")
                error = txtQuantity.ClientID + '/' + "Field Required";
            else if (ddlDrawingSequenceNumber.SelectedIndex == 0)
                error = ddlDrawingSequenceNumber.ClientID + '/' + "Field Required";

            if (error != "")
            {
                isValid = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Validate", "ServerSideValidation('" + error + "');hideLoader();", true);
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
        return isValid;
    }

    private void ShowHideControls(string mode)
    {
        objMat = new cMaterials();
        DataSet ds = new DataSet();
        try
        {
            divAdd.Visible = divInput.Visible = divOutput.Visible = false;

            switch (mode.ToLower())
            {
                case "add":
                    divAdd.Visible = false;
                    divInput.Visible = true;
                    divOutput.Visible = false;
                    break;
                case "view":
                    divAdd.Visible = true;
                    divInput.Visible = false;
                    divOutput.Visible = true;
                    break;
                case "divadd":
                    divAdd.Visible = true;
                    divInput.Visible = false;
                    divOutput.Visible = false;
                    break;
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindDrawingSequenceNumber()
    {
        DataSet ds = new DataSet();
        objMat = new cMaterials();
        DataTable dt = new DataTable();
        int j = 1;
        try
        {
            ddlDrawingSequenceNumber.Items.Clear();

            objMat.EnquiryNumber = Convert.ToInt32(ddlEnquiryNumber.SelectedValue);
            objMat.versionNumber = Convert.ToInt32(ddlVersionNumber.SelectedItem.Text);
            ds = objMat.GetDrawingSequencenumberByEnquiryIDAndVersionnumber();

            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    dt = ds.Tables[0];
            //    DataRow dr;

            //    int count = dt.Rows.Count;

            //    for (int i = 0; i <= 30 - count; i++)
            //    {
            //        dr = dt.NewRow();
            //        dr["DrawingSequenceNumber"] = 0;
            //        dt.Rows.Add(dr);
            //    }

            //    for (int i = 1; i <= 30; i++)
            //    {
            //        if (Convert.ToInt32(dt.Rows[i - 1]["DrawingSequenceNumber"].ToString()) != i)
            //        {
            //            ddlDrawingSequenceNumber.Items.Insert(j, new ListItem(i.ToString(), i.ToString()));
            //            j++;

            //        }
            //    }
            //}
            //else
            //{
            //    for (int i = 1; i <= 30; i++)
            //    {
            //        ddlDrawingSequenceNumber.Items.Insert(i, new ListItem(i.ToString(), i.ToString()));
            //    }
            //}

            ddlDrawingSequenceNumber.DataSource = ds.Tables[0];
            ddlDrawingSequenceNumber.DataTextField = "DrawingSequenceNumber";
            ddlDrawingSequenceNumber.DataValueField = "DrawingSequenceNumber";
            ddlDrawingSequenceNumber.DataBind();
            ddlDrawingSequenceNumber.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlDrawingSequenceNumber.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

}