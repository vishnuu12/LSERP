<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="WorkOrderInward.aspx.cs" Inherits="Pages_WorkOrderInward" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function ShowAddPopUp() {
            //            $('#mpeWorkOrderInwardDetails').modal({
            //                backdrop: 'static'
            //            });
            $('#mpeWorkOrderInwardDetails').show();
            return false;
        }

        function HideAddPopUp() {
            $('#mpeWorkOrderInwardDetails').hide();
            return false;
        }

        function MakeMandatory(ele) {
            try {
                if ($(ele).is(":checked")) {
                    $(ele).closest('tr').find('[type="text"]').addClass("mandatoryfield");
                    $(ele).closest('tr').find('[type="text"]').before('<span class="textboxmandatory" style="color: red;">*</span>');
                }
                else {
                    $(ele).closest('tr').find('[type="text"]').removeClass("mandatoryfield");
                    $(ele).closest('tr').find('.textboxmandatory').remove();
                }
            } catch (er) { }
        }

        function ValidateDCQty(ele) {
            var APDC = $(ele).closest('td').find('.APDC').text();

            var AvailableQty = $(ele).closest('td').find('input[type="hidden"]').val()
            if (parseInt(AvailableQty) < parseInt($(ele).val()) || parseInt($(ele).val()) <= 0) {
                InfoMessage("Entered Quantity shouldn't greater than Available Quantity'" + $(ele).val() + "'");
                $(ele).val('');
            }
            if (APDC == "0") {
                if (parseInt($(ele).val()) < parseInt(AvailableQty)) {
                    ErrorMessage('Error', 'Entered Quantity Should Not Less Than Available Qty Contact Adminstartor');
                    $(ele).val('');
                }
            }
            return false;
        }

        function checkAllItems(ele) {
            debugger;
            if ($(ele).is(":checked")) {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', true);

                $(ele).closest('table').find('[type="text"]').addClass("mandatoryfield");
                $(ele).closest('table').find('[type="text"]').before('<span class="textboxmandatory" style="color: red;">*</span>');
            }
            else {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', false);
                $(ele).closest('table').find('[type="text"]').removeClass("mandatoryfield");
                $(ele).closest('table').find('[type="text"]').closest('td').find('.textboxmandatory').remove()
            }
        }

        function ValidateWorkOrderInward(ele) {
            var msg = Mandatorycheck('ContentPlaceHolder1_Item');
            var po = parseInt($('#ContentPlaceHolder1_gvVendorDCdetails').find('[type="checkbox"]:checked').not('#ContentPlaceHolder1_gvVendorDCdetails_chkall').length);
            if (msg) {
                if (po > 0) {
                    return true;
                }
                else {
                    ErrorMessage('Error', 'No Indent Selected');
                    hideLoader();
                    return false;
                }
            }
            else
                return false;
        }

        function MandatoryField(ele) {
            if ($(ele).is(":checked")) {
                $(ele).closest('tr').find('[type="text"]').addClass("mandatoryfield");
                $(ele).closest('tr').find('[type="text"]').before('<span class="textboxmandatory" style="color: red;">*</span>');
            }
            else {
                $(ele).closest('tr').find('[type="text"]').removeClass("mandatoryfield");
                $(ele).closest('tr').find('[type="text"]').closest('td').find('.textboxmandatory').remove()
            }
        }

        function ShareInwardDC() {
            swal({
                title: "Are you sure?",
                text: "If Yes, the inward DC Will be permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Share it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('ShareInwardDC', "");
            });
            return false;
        }

        function deleteConfirmWOIH(WorkOrderInwardHeaderID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the inward DC Will be Delete permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('DeleteWOIH', WorkOrderInwardHeaderID);
            });
            return false;
        }

    </script>

    <style type="text/css">
        .radio-success label:nth-child(2) {
            color: red !important;
        }

        .radio-success label:nth-child(4) {
            color: #0acc0a !important;
        }
    </style>

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
                                    <h3 class="page-title-head d-inline-block">Work Order Inward</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Stores</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Work Order Inward</li>
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
            <asp:UpdatePanel ID="upDocumenttype" runat="server">
                <Triggers>
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">

                        <div id="divAdd" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12">
                                    <label style="text-align: left; font-size: 20px;"> WARNING MESSAGE : </label>
                                </div>
                                <div class="col-sm-12">

                                    <label>
                                        <ol>
                                            <li style="text-align: left; font-size: 20px; color: red;">Please discuss With Purchase Team Because If Anything to be PO Amendment:     </li>
                                            <li style="text-align: left; font-size: 20px; color: red;">Once  Work Order Inwarded -  PO  Amendment can't be  Possible. </li>
                                        </ol>
                                    </label>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label" style="padding-top: 7px;">
                                            Select Type
                                        </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:RadioButtonList ID="rblWPONoChange" runat="server" CssClass="radio radio-success" RepeatDirection="Horizontal"
                                            AutoPostBack="true" OnSelectedIndexChanged="rblWPONoChange_OnSelectedChanged" RepeatLayout="Flow">
                                            <asp:ListItem Selected="True" Value="0">PENDING</asp:ListItem>
                                            <asp:ListItem Value="1">COMPLETED</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Vendor Name</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlVendorName" runat="server" ToolTip="Select Vendor Name" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlVendorName_OnSelectIndexChanged" Width="70%" CssClass="form-control mandatoryfield">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            DC No</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlDCNo" runat="server" ToolTip="Select Location Name" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlDCNo_OnSelectIndexChanged" Width="70%" CssClass="form-control mandatoryfield">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divAddNew" runat="server">
                            <div class="ip-div text-center p-t-10">
                                <asp:LinkButton ID="btnAddNew" runat="server" Text="Add New" OnClick="btnAddNew_Click"
                                    CssClass="btn ic-w-btn add-btn"></asp:LinkButton>
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            DC Number</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:TextBox ID="txtDCNumber" runat="server" CssClass="form-control  mandatoryfield"
                                            ToolTip="Enter DC Number" placeholder="Enter DC Number">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            DC Date</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:TextBox ID="txtDCDate" runat="server" CssClass="form-control datepicker mandatoryfield"
                                            placeholder="Enter DC Date">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Invoice Number</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:TextBox ID="txtInVoiceNumber" runat="server" CssClass="form-control  mandatoryfield"
                                            ToolTip="Enter DC Number" placeholder="Enter Invoice Number">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Invoice Date</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:TextBox ID="txtInvoiceDate" runat="server" CssClass="form-control datepicker mandatoryfield"
                                            placeholder="Enter Invoice Date">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            E-Way Bill No
                                        </label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:TextBox ID="txtEwayBillNo" runat="server" CssClass="form-control mandatoryfield"
                                            placeholder="Enter E-Way Bill No">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            DC Copy</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:FileUpload ID="fpDCAttach" runat="server" class="form-control mandatoryfield" />
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-20">
                                    <asp:LinkButton ID="btnSave" runat="server" Text="Save" CssClass="btn btn-cons btn-save AlignTop"
                                        OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divInput');" OnClick="btnSaveMI_Click"></asp:LinkButton>
                                    <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                        CssClass="btn btn-cons btn-danger AlignTop"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvWorkOrderInward" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                OnRowCommand="gvWorkOrderInward_OnRowCommand" OnRowDataBound="gvWorkOrderInward_OnRowDataBound"
                                                HeaderStyle-HorizontalAlign="Center" DataKeyNames="WOInwardHeaderID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DC No" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDCNo" runat="server" Text='<%# Eval("DCNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DC Date" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDCDate" runat="server" Text='<%# Eval("DCDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="InVoice Number" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblInVoiceNumber" runat="server" Text='<%# Eval("InVoiceNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="InVoice Date" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblInVoiceDate" runat="server" Text='<%# Eval("InVoiceDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Add Inward" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnAdd" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="Add">
                                                            <img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="EDitWI">
                                                           <img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDelete" runat="server"
                                                                OnClientClick='<%# string.Format("return deleteConfirmWOIH({0});",Eval("WOInwardHeaderID")) %>'>
                                                            <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnWOIHeaderID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnDCSubTotalQty" runat="server" Value="0" />

                                            <asp:HiddenField ID="hdnWOIDID" runat="server" Value="0" />

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal" id="mpeWorkOrderInwardDetails" style="overflow-y: scroll;">
        <div style="max-width: 92%; padding-left: 10%; padding-top: 5%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="upView" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">
                                <asp:Label ID="lblheadername_p" Style="color: brown;" runat="server"></asp:Label></h4>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" onclick="HideAddPopUp();"
                                aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="docdiv" class="docdiv">
                                <div class="inner-container">
                                    <div id="Item" runat="server">
                                        <div id="divOutputsItems" runat="server">
                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvVendorDCdetails" runat="server"
                                                    AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    OnRowDataBound="gvVendorDCdetails_OnRowDataBound" OnRowCommand="gvVendorDCdetails_OnRowCommand"
                                                    EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                    DataKeyNames="WOIHID,VDCDID">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkall" runat="server" onclick="return checkAllItems(this);" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkitems" runat="server" onclick="return MandatoryField(this);"
                                                                    AutoPostBack="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Indent No" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblindentNo" runat="server" Text='<%# Eval("IndentNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="RFP No" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRFPNo" runat="server" Text='<%# Eval("RFPNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Part Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPartName" runat="server" Text='<%# Eval("PartName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PO QTY" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblActualQty" runat="server" Text='<%# Eval("IssuedQty")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Avail Qty" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAvailPOQty" runat="server" Text='<%# Eval("AvailQty")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Inward Qty" HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblJobDCQty" runat="server" CssClass="APDC" Text='<%# Eval("APDCQty")%>' Style="display: none;"></asp:Label>
                                                                <asp:HiddenField ID="hdnAvailQty" runat="server" Value='<%# Eval("AvailQty")%>'></asp:HiddenField>
                                                                <asp:TextBox ID="txtInwardQty" onkeypress="return fnAllowNumeric(this);" onblur="return ValidateDCQty(this);" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="JobDescription" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblJobDescription" runat="server" Text='<%# Eval("JobDescription")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Allow/UnAllow" HeaderStyle-CssClass="text-center"
                                                            ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnAllow" Visible="false" runat="server" CommandName="Allow"
                                                                    CssClass="btn btn-cons btn-success" Text="Allow"
                                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">                                                                                    
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-12 p-t-20 text-center">
                                                <asp:LinkButton ID="btnSaveWOIDetails" runat="server" Text="Save DC" CssClass="btn btn-cons btn-save AlignTop"
                                                    OnClick="btnSaveWOIDetails_Click" OnClientClick="return ValidateWorkOrderInward(this);"></asp:LinkButton>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <asp:GridView ID="gvWorkOrderInwardedDetails" runat="server" AutoGenerateColumns="False"
                                                    ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" OnRowCommand="gvWorkOrderInwardedDetails_OnRowCommand"
                                                    OnRowDataBound="gvWorkOrderInwardedDetails_OnRowDataBound" CssClass="table table-hover table-bordered medium"
                                                    DataKeyNames="WOIDID,IndentNo,RFPHID">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"
                                                            ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="RFP No" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRFPNo" runat="server" Text='<%# Eval("RFPNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Part Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPartName" runat="server" Text='<%# Eval("PartName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Inward Qty" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInwardQty" runat="server" Text='<%# Eval("InwardQty")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="JobDescription" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblJobDescription" runat="server" Text='<%# Eval("JobDescription")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                    CommandName="DeleteInwardDetails">
                                                          <img src="../Assets/images/del-ec.png" alt="" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-12 p-t-20 text-center">
                                                <asp:Button ID="btnShareInwardDC" Text="Share DC" CssClass="btn btn-cons btn-success" OnClientClick="ShareInwardDC();"
                                                    runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hdnSPODID" runat="server" Value="0" />
                        <div class="modal-footer">
                            <div class="col-sm-12 p-t-10">
                                <div class="col-sm-10">
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
