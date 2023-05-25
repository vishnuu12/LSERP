﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="UltraSonicExaminationReport.aspx.cs" Inherits="Pages_UltraSonicExaminationReport" ClientIDMode="Predictable" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function ddlPenetrantChange() {
            var Val = $(event.target).val();
            if ($(event.target).id == "ContentPlaceHolder1_ddlPenetrantBrand")
                $('#ContentPlaceHolder1_txtBatchNo').val(Val.split('/')[1]);
            else if ($(event.target).id == "ContentPlaceHolder1_ddlCleanerBrand")
                $('#ContentPlaceHolder1_txtCleanerBatch').val(Val.split('/')[1]);
            else if ($(event.target).id == "ContentPlaceHolder1_ddlDeveloperBrand")
                $('#ContentPlaceHolder1_txtDeveloperBatchNo').val(Val.split('/')[1]);
        }

        function MakeMandatory(ele) {
            debugger;
            if ($(ele).is(":checked")) {
                $(ele).closest('tr').find('[type="text"]').before('<span class="textboxmandatory" style="color: red;">*</span>');
                $(ele).closest('tr').find('[type="text"]').addClass('mandatoryfield');
            }
            else {
                $(ele).closest('tr').find('[type="text"]').closest('td').find('.textboxmandatory').remove();
                $(ele).closest('tr').find('[type="text"]').removeClass('mandatoryfield');
            }
        }

        function Validate() {
            var res = true;
            var msg = Mandatorycheck('ContentPlaceHolder1_divInput');
            var Report = Mandatorycheck('ContentPlaceHolder1_divLPIReport');

            if (parseInt($('#ContentPlaceHolder1_gvUserDetails').find('[type="checkbox"]:checked').length) > 0) {
                if (msg == false) {
                    res = false;
                }
                if (Report == false)
                    res = false;
            }
            else {
                ErrorMessage('Error', 'UT Details Not Selected');
                res = false;
            }
            if (res)
                return true;
            else {
                hideLoader();
                return false;
            }
        }

        function ItemQtyValidation(ele) {
            var TotalItemQty = $('#ContentPlaceHolder1_hdnTotalItemQty').val();
            var Qty = $(ele).val();

            if (parseInt(TotalItemQty) < parseInt(Qty)) {
                ErrorMessage('Error', 'Total Item Qty ' + TotalItemQty + 'should Less Than Equal to Entered Qty');
                $(ele).val('');
            }
        }

        function deleteConfirm(VERID) {
            swal({
                title: "Are you sure?",
                text: "If Yes,record will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deleteVERID', VERID);
            });
            return false;
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container main-container_close">
        <div class="app-main__outer">
            <div class="app-main__inner">
                <div class="app-page-title">
                    <div class="page-title-left page-title-wrapper">
                        <div class="page-title-heading">
                            <div>
                                <div class="page-title-head center-elem">
                                    <span class="d-inline-block pr-2">
                                        <%--<i class="fa fa-th-large opacity-6 yellow"></i>--%>
                                        <img src="../Assets/images/pages.png" alt="" width="16px" />
                                    </span>
                                    <h3 class="page-title-head d-inline-block">Ultra Sonic Examination Report</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Quality</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Ultra Sonic Examination Report</li>
                                                </ol>
                                     </nav>
                        <a id="help" href="" alt="" style="margin-top: 4px;">
                            <img src="../Assets/images/help.png" />
                        </a>
                    </div>
                </div>
            </div>
        </div>
        <div>
            <asp:UpdatePanel ID="upQuote" runat="server">
                <Triggers>
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="div" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="" id="divInput" runat="server">
                                        <div class="col-sm-12 p-t-10" style="padding-left: 35%;">
                                            <div class="col-sm-6 text-left">
                                                <label class="form-label">
                                                    Report No</label>
                                                <asp:Label ID="lblReportNo" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-6 text-left">
                                                <label class="form-label">
                                                    Convolution Of Records</label>
                                                <asp:TextBox ID="txtConvolutionOfRecords" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4 text-right">
                                                <label class="form-label">
                                                    Customer Name</label>
                                            </div>
                                            <div class="col-sm-6 text-left">
                                                <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="true" CssClass="form-control"
                                                    OnSelectedIndexChanged="ddlCustomerName_OnSelectedIndexChanged" Width="70%" ToolTip="Select Customer Number">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-2">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4 text-right">
                                                <label class="form-label">
                                                    Customer PO</label>
                                            </div>
                                            <div class="col-sm-6 text-left">
                                                <asp:DropDownList ID="ddlCustomerPO" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomerPO_SelectIndexChanged"
                                                    CssClass="form-control" Width="70%" ToolTip="Select Customer PO">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-2">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4 text-right">
                                                <label class="form-label">
                                                    RFP No</label>
                                            </div>
                                            <div class="col-sm-6 text-left">
                                                <asp:DropDownList ID="ddlRFPNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRFPNo_SelectIndexChanged"
                                                    CssClass="form-control" Width="70%" ToolTip="Select RFP No">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-2">
                                            </div>
                                        </div>
                                        <div id="divInterNational" runat="server" visible="true">
                                            <div class="col-sm-12 p-t-10 p-l-0 p-r-0">
                                                <table align="center" id="tbl_customerDetails" class="tr_class table" style="text-transform: none;">
                                                    <tbody>
                                                        <tr class="eqheading">
                                                            <td>
                                                                <div class="offerFormHeading1">
                                                                    Ultra Sonic Examination Report
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span3" class="eqdetailslabel">Report Date</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtReportDate" CssClass="form-control mandatoryfield datepicker"
                                                                    runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="lblIV" class="eqdetailslabel">Item Name</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:DropDownList ID="ddlItemName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlItemName_SelectIndexChanged"
                                                                    CssClass="form-control" Width="70%" ToolTip="Select Item No">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span5" class="eqdetailslabel">If Specify Item Name</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtItemName" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span13" class="eqdetailslabel">Customer Name</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtCustomername" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span16" class="eqdetailslabel">Calibration Block</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtCalibrationBlock" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span20" class="eqdetailslabel">Reference Block</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtReferanceBlock" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="label" class="eqdetailslabel">Job Description</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtJobDescription" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="lblTestDate" class="eqdetailslabel">Test Date</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtTestDate" CssClass="form-control datepicker" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span14" class="eqdetailslabel">Drawing No</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtDrawingNo" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span15" class="eqdetailslabel">Material</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtMaterial" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span1" class="eqdetailslabel">Thickness</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtThickness" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span2" class="eqdetailslabel">Procedure And Rev No</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtProceduerAndRevNo" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr class="eqheading">
                                                            <td>
                                                                <div class="offerFormHeading1">
                                                                    Calibration Range
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span6" class="eqdetailslabel">Normal</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtCalibrationNormal" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span7" class="eqdetailslabel">Angle</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtCalibrationAngle" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span8" class="eqdetailslabel">Equipment</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtEquipment" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span24" class="eqdetailslabel">Model</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtModel" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr class="eqheading">
                                                            <td>
                                                                <div class="offerFormHeading1">
                                                                    Search Unit
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span10" class="eqdetailslabel">Normal</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtSearchUnitNormal" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span11" class="eqdetailslabel">Angle</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtSearchUnitAngle" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span22" class="eqdetailslabel">Size And Identification</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtSizeAndIdentification" CssClass="form-control mandatoryfield"
                                                                    runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span23" class="eqdetailslabel">Serial No</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtSerielNo" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span9" class="eqdetailslabel">Frequency</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtFrequency" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span30" class="eqdetailslabel">Surface Condition</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtSurfaceCondition" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr class="eqheading">
                                                            <td>
                                                                <div class="offerFormHeading1">
                                                                    Gain(dB)
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span26" class="eqdetailslabel">Referance</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtGainReference" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span27" class="eqdetailslabel">Scanning</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtGainScanning" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span21" class="eqdetailslabel">Couplant</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtCouplant" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span25" class="eqdetailslabel">Search Unit Cable Length</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtSearchUnitCablelength" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span28" class="eqdetailslabel">Extent Of Test</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtExtentOfTest" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span29" class="eqdetailslabel">Rejection Level</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtRejectionLevel" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span31" class="eqdetailslabel">Acceptance Standard</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtAcceptanceStandard" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left"></td>
                                                            <td align="left"></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                <div id="divLPIReport" runat="server">
                                                                    <div id="Div1" class="col-sm-12 p-t-10" runat="server">
                                                                        <asp:GridView ID="gvUserDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                                            CssClass="table table-hover table-bordered medium" EmptyDataText="No Records Found"
                                                                            DataKeyNames="USERDID,USERID">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="" ItemStyle-Width="2%">
                                                                                    <HeaderTemplate>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkitems" runat="server" AutoPostBack="false" onclick="return MakeMandatory(this);" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'
                                                                                            Style="text-align: center"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Size / Length">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtsizelength" runat="server" CssClass="form-control" Text='<%# Eval("SizeLength")%>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Type Of Discontinuity">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtTypeOfDiscontinuty" CssClass="form-control" runat="server" Text='<%# Eval("TypeOfDiscontinuity")%>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Evaluvation">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtEvaluvation" CssClass="form-control" runat="server" Text='<%# Eval("Evaluvation")%>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span17" class="eqdetailslabel">Scanning Sketch</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtScanningSketch" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr class="eqheading">
                                                            <td>
                                                                <div class="offerFormHeading1">
                                                                    For Lone Star Industries
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr class="eqheading">
                                                            <td>
                                                                <label>
                                                                    Performed BY
                                                                </label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="lblSketchOfIndications" class="mandatorylbl">Name</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtPreparedName" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="lblDeclaration" class="mandatorylbl">Prepared Level</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtPrefaredLevel" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr class="eqheading">
                                                            <td>
                                                                <label>
                                                                    Evaluvated BY
                                                                </label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span4" class="mandatorylbl">Name</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtEvaluvatedname" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span12" class="mandatorylbl">Designation</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtEvaluvatedDesignation" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr class="eqheading">
                                                            <td>
                                                                <label>
                                                                    For Inspection Authority/ Authorized Inspector
                                                                </label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span18" class="mandatorylbl">Name</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtAIName" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span19" class="mandatorylbl">Designation</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtAIDesignation" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 text-center">
                                        <asp:LinkButton ID="btnSaveUTR" runat="server" CssClass="btn btn-cons btn-save AlignTop"
                                            Text="Save" OnClientClick="return Validate();" OnClick="btnSaveUTR_Click">
                                        </asp:LinkButton>
                                    </div>
                                    <div id="divOutPut" class="col-sm-12 p-t-10" style="overflow-x: scroll;" runat="server">
                                        <asp:GridView ID="gvUTReportHeader" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No Records Found" OnRowCommand="gvUTReportHeader_OnRowCommand" CssClass="table table-hover table-bordered medium"
                                            DataKeyNames="USERID">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--   <asp:TemplateField HeaderText="Item Name" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                    HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIfSpecifyItemName" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Report No" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                    HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReportNo" runat="server" Text='<%# Eval("ReportNo")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Report Date" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                    HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReportDate" runat="server" Text='<%# Eval("ReportDate")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditUTRH"
                                                            CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                         <img src="../Assets/images/edit-ec.png" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnAdd" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("USERID")) %>'>
                                                         <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PDF" ItemStyle-CssClass="text-center" HeaderStyle-Width="10%"
                                                    ItemStyle-Width="10%" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnPDF" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            CommandName="PDFUTRH"><img src="../Assets/images/pdf.png" />
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:HiddenField ID="hdnUserID" runat="server" Value="0" />
                                        <asp:HiddenField ID="hdnTotalItemQty" runat="server" Value="0" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>