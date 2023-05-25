using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_RFPQCSItemStatusCard : System.Web.UI.Page
{

    #region  "Declaration"

    cSession objSession = new cSession();
    cReports objR;

    #endregion

    #region "Page Load Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                ViewState["RFPDID"] = Request.QueryString["RFPDID"].ToString();
                BindRFPQCItemStatusCard();
                BindAssemplyWeldingQCCardDetails();
                GetItemName();
            }
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Button Events"

    protected void btnprint_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataSet dsQC = new DataSet();
        objR = new cReports();
        DataTable dt;
        try
        {
            objR.RFPDID = Convert.ToInt32(ViewState["RFPDID"].ToString());

            ds = objR.GetRFPQCItemStatusCardPrintDetails();

            dt = (DataTable)ViewState["RFPQCStateCard"];

            lblRFPNo_p.Text = ds.Tables[0].Rows[0]["RFPNo"].ToString();
            lblcustomer_p.Text = ds.Tables[0].Rows[0]["ProspectName"].ToString();
            lblqty_p.Text = ds.Tables[0].Rows[0]["QTY"].ToString();
            lblitemname_p.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
            lblsize_p.Text = ds.Tables[0].Rows[0]["ItemSize"].ToString();
            lbldrawingname_p.Text = ds.Tables[0].Rows[0]["DrawingName"].ToString();

            List<Dictionary<string, object>> li = new List<Dictionary<string, object>>();

            Dictionary<string, object> row = new Dictionary<string, object>();

            DateTimeOffset d1 = DateTimeOffset.Now;

            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                li.Add(row);
            }

            if (li.Where(p => p["MPStatus"].ToString().Contains("InComplete")).ToList().Count > 0)
            {
                lblRawMaterialInspectionStatus_p.Text = "InComplete";
                if (li.Where(p => p["CategoryName"].ToString().Contains("BOUGHTOUT ITEMS")).ToList().Count > 0)
                    lblBoughtOutItemInspectionstatus_p.Text = "InComplete";
                else
                    lblBoughtOutItemInspectionstatus_p.Text = "NA";
            }
            else
            {
                if ((li.Where(p => p["MRNQCStatus"].ToString().Contains("InComplete") && p["CategoryName"].ToString().Contains("RAW MATERIAL")).ToList()).Count > 0)
                {
                    lblRawMaterialInspectionStatus_p.Text = "InComplete";
                }
                else
                {
                    dsQC = new DataSet();

                    objR.RFPDID = Convert.ToInt32(ViewState["RFPDID"].ToString());
                    objR.CID = "1";

                    dsQC = objR.GetRFPItemRawmatQCClearedByAndDateByRFPDID();

                    if (dsQC.Tables[0].Rows.Count > 0)
                    {
                        lblRawMaterialInspectionQCDoneBy_p.Text = dsQC.Tables[0].Rows[0]["QCDoneBy"].ToString();
                        lblRawMaterialInspectionQCDoneOn_p.Text = dsQC.Tables[0].Rows[0]["QCDoneOn"].ToString();
                    }
                    else
                    {
                        lblRawMaterialInspectionQCDoneBy_p.Text = "";
                        lblRawMaterialInspectionQCDoneOn_p.Text = "";
                    }

                    lblRawMaterialInspectionStatus_p.Text = "Completed";
                }

                if (li.Where(p => p["CategoryName"].ToString().Contains("BOUGHTOUT ITEMS")).ToList().Count > 0)
                {
                    if (li.Where(p => p["CategoryName"].ToString().Contains("BOUGHTOUT ITEMS") && p["MRNQCStatus"].ToString().Contains("InComplete")).ToList().Count > 0)
                    {
                        lblBoughtOutItemInspectionstatus_p.Text = "InComplete";
                    }
                    else
                    {
                        dsQC = new DataSet();

                        objR.RFPDID = Convert.ToInt32(ViewState["RFPDID"].ToString());
                        objR.CID = "7";

                        dsQC = objR.GetRFPItemRawmatQCClearedByAndDateByRFPDID();

                        if (dsQC.Tables[0].Rows.Count > 0)
                        {
                            lblBoughtOutItemInspectionQCDoneBy_p.Text = dsQC.Tables[0].Rows[0]["QCDoneBy"].ToString();
                            lblBoughtOutItemInspectionQCDoneOn_p.Text = dsQC.Tables[0].Rows[0]["QCDoneOn"].ToString();
                        }
                        else
                        {
                            lblBoughtOutItemInspectionQCDoneBy_p.Text = "";
                            lblBoughtOutItemInspectionQCDoneOn_p.Text = "";
                        }

                        lblBoughtOutItemInspectionstatus_p.Text = "Completed";
                    }
                }
                else
                    lblBoughtOutItemInspectionstatus_p.Text = "NA";
            }

            if (li.Where(p => p["MID"].ToString().Equals("1")).ToList().Count > 0)
            {
                if ((li.Where(p => p["MID"].ToString().Equals("1") && p["CuttingQC"].ToString().Contains("InComplete")).ToList()).Count > 0)
                    lblSheetMarkingAndCuttingProcessstatus_p.Text = "InComplete";
                else
                {
                    if (li.Where(p => p["MID"].ToString() == "1" && p["CuttingQC"].ToString().Contains("Not Started")).ToList().Count > 0)
                        lblSheetMarkingAndCuttingProcessstatus_p.Text = "Not Started";
                    else
                    {
                        if (li.Where(p => p["MID"].ToString() == "1" && p["CuttingQC"].ToString().Contains("Completed")).ToList().Count > 0)
                        {
                            dsQC = new DataSet();
                            objR.RFPDID = Convert.ToInt32(ViewState["RFPDID"].ToString());
                            objR.PMID = 1;
                            dsQC = objR.GetRFPItemJobQCClearedByAndDateByRFPDIDAndProcessName();

                            if (dsQC.Tables[0].Rows.Count > 0)
                            {
                                lblsheetmarkingandcuttingQCDoneBy_p.Text = dsQC.Tables[0].Rows[0]["QCDoneBy"].ToString();
                                lblsheetmarkingandcuttingQCDoneOn_p.Text = dsQC.Tables[0].Rows[0]["QCDoneOn"].ToString();
                            }
                            else
                            {
                                lblsheetmarkingandcuttingQCDoneBy_p.Text = "";
                                lblsheetmarkingandcuttingQCDoneOn_p.Text = "";
                            }

                            lblSheetMarkingAndCuttingProcessstatus_p.Text = "Completed";
                        }
                        else
                            lblSheetMarkingAndCuttingProcessstatus_p.Text = "NA";
                    }
                }

                if ((li.Where(p => p["MID"].ToString().Equals("1") && p["WeldingQC"].ToString().Contains("InComplete")).ToList()).Count > 0)
                    lblsheetweldingProcessStatus_p.Text = "InComplete";
                else
                {

                    if (li.Where(p => p["MID"].ToString() == "1" && p["WeldingQC"].ToString().Contains("Not Started")).ToList().Count > 0)
                        lblsheetweldingProcessStatus_p.Text = "Not Started";
                    else
                    {
                        if (li.Where(p => p["MID"].ToString() == "1" && p["WeldingQC"].ToString().Contains("Completed")).ToList().Count > 0)
                        {
                            dsQC = new DataSet();
                            objR.RFPDID = Convert.ToInt32(ViewState["RFPDID"].ToString());
                            objR.PMID = 2;
                            dsQC = objR.GetRFPItemJobQCClearedByAndDateByRFPDIDAndProcessName();

                            if (dsQC.Tables[0].Rows.Count > 0)
                            {
                                lblsheetweldingQCDoneBy_p.Text = dsQC.Tables[0].Rows[0]["QCDoneBy"].ToString();
                                lblSheetWeldingQCDoneOn.Text = dsQC.Tables[0].Rows[0]["QCDoneOn"].ToString();
                            }
                            else
                            {
                                lblsheetweldingQCDoneBy_p.Text = "";
                                lblSheetWeldingQCDoneOn.Text = "";
                            }
                            lblsheetweldingProcessStatus_p.Text = "Completed";
                        }
                        else
                            lblsheetweldingProcessStatus_p.Text = "NA";
                    }
                }
                if ((li.Where(p => p["MID"].ToString().Equals("1") && p["FormingQC"].ToString().Contains("InComplete")).ToList()).Count > 0)
                    lblBellowFormingProcessStatus_p.Text = "InComplete";
                else
                {
                    if (li.Where(p => p["MID"].ToString() == "1" && p["FormingQC"].ToString().Contains("Not Started")).ToList().Count > 0)
                        lblBellowFormingProcessStatus_p.Text = "Not Started";
                    else
                    {
                        if (li.Where(p => p["MID"].ToString() == "1" && p["FormingQC"].ToString().Contains("Completed")).ToList().Count > 0)
                        {
                            dsQC = new DataSet();
                            objR.RFPDID = Convert.ToInt32(ViewState["RFPDID"].ToString());
                            objR.PMID = 3;
                            dsQC = objR.GetRFPItemJobQCClearedByAndDateByRFPDIDAndProcessName();

                            if (dsQC.Tables[0].Rows.Count > 0)
                            {
                                lblBellowFormingQCDoneBy_p.Text = dsQC.Tables[0].Rows[0]["QCDoneBy"].ToString();
                                lblBellowFormingQCDoneOn_p.Text = dsQC.Tables[0].Rows[0]["QCDoneOn"].ToString();
                            }
                            else
                            {
                                lblBellowFormingQCDoneBy_p.Text = "";
                                lblBellowFormingQCDoneOn_p.Text = "";
                            }
                            lblBellowFormingProcessStatus_p.Text = "Completed";
                        }
                        else
                            lblBellowFormingProcessStatus_p.Text = "NA";
                    }
                }
            }
            else
            {
                lblSheetMarkingAndCuttingProcessstatus_p.Text = "NA";
                lblsheetweldingProcessStatus_p.Text = "NA";
                lblBellowFormingProcessStatus_p.Text = "NA";
            }

            if (li.Where(p => p["MID"].ToString() != "1").ToList().Count > 0)
            {
                if ((li.Where(p => p["MID"].ToString() != "1" && p["CuttingQC"].ToString().Contains("InComplete")).ToList()).Count > 0)
                    lblMarkingAndCuttingProcessStatus_p.Text = "InComplete";
                else
                {
                    if (li.Where(p => p["MID"].ToString() != "1" && p["CuttingQC"].ToString().Contains("Not Started")).ToList().Count > 0)
                        lblMarkingAndCuttingProcessStatus_p.Text = "Not Started";
                    else
                    {
                        if (li.Where(p => p["MID"].ToString() != "1" && p["CuttingQC"].ToString().Contains("Completed")).ToList().Count > 0)
                        {
                            dsQC = new DataSet();
                            objR.RFPDID = Convert.ToInt32(ViewState["RFPDID"].ToString());
                            objR.PMID = 4;
                            dsQC = objR.GetRFPItemJobQCClearedByAndDateByRFPDIDAndProcessName();

                            if (dsQC.Tables[0].Rows.Count > 0)
                            {
                                lblMarkingAndCuttingProcessQCDoneBy_p.Text = dsQC.Tables[0].Rows[0]["QCDoneBy"].ToString();
                                lblMarkingAndCuttingProcessQCDoneOn_p.Text = dsQC.Tables[0].Rows[0]["QCDoneOn"].ToString();
                            }
                            else
                            {
                                lblMarkingAndCuttingProcessQCDoneBy_p.Text = "";
                                lblMarkingAndCuttingProcessQCDoneOn_p.Text = "";
                            }
                            lblMarkingAndCuttingProcessStatus_p.Text = "Completed";
                        }
                        else
                            lblMarkingAndCuttingProcessStatus_p.Text = "NA";
                    }
                }

                if ((li.Where(p => p["MID"].ToString() != "1" && p["WeldingQC"].ToString().Contains("InComplete")).ToList()).Count > 0)
                    lblfabricationandweldingprocessstatus_p.Text = "InComplete";
                else
                {
                    if (li.Where(p => p["MID"].ToString() != "1" && p["WeldingQC"].ToString().Contains("Not Started")).ToList().Count > 0)
                        lblfabricationandweldingprocessstatus_p.Text = "Not Started";
                    else
                    {
                        if (li.Where(p => p["MID"].ToString() != "1" && p["WeldingQC"].ToString().Contains("Completed")).ToList().Count > 0)
                        {
                            dsQC = new DataSet();
                            objR.RFPDID = Convert.ToInt32(ViewState["RFPDID"].ToString());
                            objR.PMID = 5;
                            dsQC = objR.GetRFPItemJobQCClearedByAndDateByRFPDIDAndProcessName();

                            if (dsQC.Tables[0].Rows.Count > 0)
                            {
                                lblfabricationandweldingprocessQCDoneBy_p.Text = dsQC.Tables[0].Rows[0]["QCDoneBy"].ToString();
                                lblfabricationandweldingprocessQCDoneOn_p.Text = dsQC.Tables[0].Rows[0]["QCDoneOn"].ToString();
                            }
                            else
                            {
                                lblfabricationandweldingprocessQCDoneBy_p.Text = "";
                                lblfabricationandweldingprocessQCDoneOn_p.Text = "";
                            }

                            lblfabricationandweldingprocessstatus_p.Text = "Completed";
                        }
                        else
                            lblfabricationandweldingprocessstatus_p.Text = "NA";
                    }
                }
            }
            else
            {
                lblMarkingAndCuttingProcessStatus_p.Text = "NA";
                lblfabricationandweldingprocessstatus_p.Text = "NA";
            }

            dt = (DataTable)ViewState["AssemplyWelding"];

            li = new List<Dictionary<string, object>>();

            row = new Dictionary<string, object>();

            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                li.Add(row);
            }

            if (ViewState["APStatus"].ToString() == "Completed")
            {
                if (li.Where(p => p["Welding"].ToString() == "MainAssemplyWelding").ToList().Count > 0)
                {
                    if (li.Where(p => p["Welding"].ToString() == "MainAssemplyWelding" && p["QCStatus"].ToString().Contains("InComplete")).ToList().Count > 0)
                    {
                        lblBellowToPipeWeldingProcessstatus_p.Text = "InComplete";
                    }
                    else
                    {
                        if (li.Where(p => p["Welding"].ToString() == "MainAssemplyWelding" && p["QCStatus"].ToString().Contains("Not Started")).ToList().Count > 0)
                            lblBellowToPipeWeldingProcessstatus_p.Text = "Not Started";
                        else
                        {
                            if (li.Where(p => p["Welding"].ToString() == "MainAssemplyWelding" && p["QCStatus"].ToString().Contains("Not Need")).ToList().Count > 0)
                                lblBellowToPipeWeldingProcessstatus_p.Text = "NA";
                            else
                            {

                                Dictionary<string, object> BellowToPipe = (from pa in li
                                                                           where pa["Welding"].ToString() == "MainAssemplyWelding"
                                                                           orderby pa["PAPDID"] descending
                                                                           select pa).First();

                                lblBellowToPipeWeldingProcessQCDoneBy_p.Text = BellowToPipe["QCDoneBy"].ToString();
                                lblBellowToPipeWeldingProcessQCDoneOn_p.Text = BellowToPipe["QCDoneOn"].ToString();

                                lblBellowToPipeWeldingProcessstatus_p.Text = "Completed";
                            }
                        }
                    }
                }
                else
                    lblBellowToPipeWeldingProcessstatus_p.Text = "NA";

                if (li.Where(p => p["Welding"].ToString() == "OTHERWelding").ToList().Count > 0)
                {
                    if (li.Where(p => p["Welding"].ToString() == "OTHERWelding" && p["QCStatus"].ToString().Contains("InComplete")).ToList().Count > 0)
                    {
                        lblotherpartweldingprocessstatus_p.Text = "InComplete";
                    }
                    else
                    {
                        if (li.Where(p => p["Welding"].ToString() == "OTHERWelding" && p["QCStatus"].ToString().Contains("Not Started")).ToList().Count > 0)
                            lblotherpartweldingprocessstatus_p.Text = "Not Started";
                        else
                        {
                            if (li.Where(p => p["Welding"].ToString() == "OTHERWelding" && p["QCStatus"].ToString().Contains("Completed")).ToList().Count > 0)
                            {
                                Dictionary<string, object> OtherWelding = (from pa in li
                                                                           where pa["Welding"].ToString() == "OTHERWelding"
                                                                           orderby pa["PAPDID"] descending
                                                                           select pa).First();

                                lblotherpartweldingprocessQCDoneBy_p.Text = OtherWelding["QCDoneBy"].ToString();
                                lblotherpartweldingprocessQCDoneOn_p.Text = OtherWelding["QCDoneOn"].ToString();

                                lblotherpartweldingprocessstatus_p.Text = "Completed";
                            }
                            else
                                lblotherpartweldingprocessstatus_p.Text = "NA";
                        }
                    }
                }
                else
                    lblotherpartweldingprocessstatus_p.Text = "NA";
            }
            else
            {
                if (ViewState["APStatus"].ToString() == "No Need")
                {
                    lblBellowToPipeWeldingProcessstatus_p.Text = "NA";
                    lblotherpartweldingprocessstatus_p.Text = "NA";
                }
                else
                {
                    lblBellowToPipeWeldingProcessstatus_p.Text = "Not Started";
                    lblotherpartweldingprocessstatus_p.Text = "Not Started";
                }
            }

            lblProcessName_P.Text = "ITEM STATUS CARD";

            hdnAddress.Value = ds.Tables[1].Rows[0]["Address"].ToString();
            hdnPhoneAndFaxNo.Value = ds.Tables[1].Rows[0]["PhoneAndFaxNo"].ToString();
            hdnEmail.Value = ds.Tables[1].Rows[0]["Email"].ToString();
            hdnWebsite.Value = ds.Tables[1].Rows[0]["WebSite"].ToString();

            hdnDocNo.Value = ds.Tables[2].Rows[0]["DocNo"].ToString();
            hdnRevNo.Value = ds.Tables[2].Rows[0]["RevNo"].ToString();
            hdnRevDate.Value = ds.Tables[2].Rows[0]["RevDate"].ToString();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "HideLoader", "PrintRFPItemStatusCard();", true);
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

    #region"Common Methods"

    private void BindAssemplyWeldingQCCardDetails()
    {
        DataSet ds = new DataSet();
        objR = new cReports();
        try
        {
            objR.RFPDID = Convert.ToInt32(ViewState["RFPDID"].ToString());

            ds = objR.GetRFPItemAssemplyWeldingQCClearanceStatusCardByRFPDID();

            ViewState["APStatus"] = ds.Tables[0].Rows[0]["APStatus"].ToString();

            if (ds.Tables[1].Rows.Count > 0)
            {
                gvRFPItemAssemplyWeldingQCStatusCard.DataSource = ds.Tables[1];
                gvRFPItemAssemplyWeldingQCStatusCard.DataBind();
            }
            else
            {
                gvRFPItemAssemplyWeldingQCStatusCard.DataSource = "";
                gvRFPItemAssemplyWeldingQCStatusCard.DataBind();
            }

            ViewState["AssemplyWelding"] = ds.Tables[1];
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void BindRFPQCItemStatusCard()
    {
        DataSet ds = new DataSet();
        objR = new cReports();
        try
        {
            objR.RFPDID = Convert.ToInt32(ViewState["RFPDID"].ToString());
            ds = objR.GetRFPQCItemStatusCard();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvRFPItemQCStatusCard.DataSource = ds.Tables[0];
                gvRFPItemQCStatusCard.DataBind();
            }
            else
            {
                gvRFPItemQCStatusCard.DataSource = "";
                gvRFPItemQCStatusCard.DataBind();
            }

            ViewState["RFPQCStateCard"] = ds.Tables[0];
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    private void GetItemName()
    {
        cReports objRpt = new cReports();
        DataSet ds = new DataSet();
        try
        {
            objRpt.RFPDID = Convert.ToInt32(ViewState["RFPDID"].ToString());
            ds = objRpt.GetItemNameByRFPDID();

            lblRFPNo.Text = ds.Tables[0].Rows[0]["ItemName"].ToString();
        }
        catch (Exception ex)
        {
            Log.Message(ex.ToString());
        }
    }

    #endregion

}