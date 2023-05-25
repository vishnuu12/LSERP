<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="WorkOrderInwardQCClearanceaspx.aspx.cs"
    Inherits="Pages_WorkOrderInwardQCClearanceaspx" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function ViewWODrawingFile(rowindex) {
            __doPostBack('ViewWODrawingFile', rowindex);
            return false;
        }

        function ShowQCClearancePopUp() {
            $('#mpeWorkOrderQCClearanceDetails').modal('show');
        }

        function HideQCClearancePopUp() {
            $('#mpeWorkOrderQCClearanceDetails').modal('hide');
        }

        function checkAllItems(ele) {
            debugger;
            if ($(ele).is(":checked")) {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', true);

                //$(ele).closest('table').find('[type="text"]').addClass("mandatoryfield");
                //$(ele).closest('table').find('[type="text"]').before('<span class="textboxmandatory" style="color: red;">*</span>');
            }
            else {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', false);
                //$(ele).closest('table').find('[type="text"]').removeClass("mandatoryfield");
                //$(ele).closest('table').find('[type="text"]').closest('td').find('.textboxmandatory').remove()
            }
        }

        function ValidateQCQty(ele) {
            var QCAvailQty = parseInt($('#ContentPlaceHolder1_hdnQCAvailQty').val());
            var CheckLength = parseInt($('#ContentPlaceHolder1_gvWorkOrderPartSnoDetails').find('[type="checkbox"]:checked').length);
            if (CheckLength > QCAvailQty) {
                ErrorMessage('Error', 'QC Avail Qty Shou Not Greater Checked Qty');
                $(ele).prop('checked', false);
            }
        }

        function ValidateTypeOfCheck(status) {
            if ($('#ContentPlaceHolder1_gvWorkOrderPartSnoDetails').find('[type="checkbox"]:checked').not('#ContentPlaceHolder1_gvWorkOrderPartSnoDetails_chkall').length > 0) {
                var msg = Mandatorycheck('divQCClearence');
                if (msg) {
                    var CheckedCount = $('#ContentPlaceHolder1_gvQCApplicableList').find('[type="checkbox"]:checked').not('#ContentPlaceHolder1_gvQCApplicableList_chkall').length;
                    if (status == 'A') {
                        var RowCount = $('#ContentPlaceHolder1_gvQCApplicableList').find('tr').length - 1;
                        if (RowCount == CheckedCount) {
                            SaveConfirm(status);
                            hideLoader();
                            return false;
                        }
                        else {
                            ErrorMessage('Error', 'If you Approved Select All QC List');
                            hideLoader();
                            return false;
                        }
                    }
                    if (status == 'RW') {
                        if (CheckedCount > 0) {
                            SaveConfirm(status);
                            hideLoader();
                            return false;
                        }
                        else {
                            ErrorMessage('Error', 'Select Type Of QC');
                            hideLoader();
                            return false;
                        }
                    }
                    if (status == 'RJ') {
                        if (CheckedCount > 0) {
                            SaveConfirm(status);
                            hideLoader();
                            return false;
                        }
                        else {
                            ErrorMessage('Error', 'Select Type Of QC');
                            hideLoader();
                            return false;
                        }
                    }
                }
                else
                    return false;
            }
            else {
                ErrorMessage('Error', 'Select Part Sno');
                return false;
            }
        }
        function SaveConfirm(status) {
            swal({
                title: "Are you sure?",
                text: "If Yes, No Further Edit Once Saved",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Approved it!",
                closeOnConfirm: false
            }, function () {
                __doPostBack("QCApprove", status);
            });
            return false;
        }

        function ValidateInwardQty(ele) {
            if ($(ele).val() != "") {
                var DCQty = parseInt($(ele).val());
                var AvailQty = parseInt($(ele).closest('tr').find('[type="hidden"]').val());
                if (DCQty > AvailQty && DCQty > 0) {
                    ErrorMessage('Error', 'Entered Quantity Should Not Greater Than Available Qty');
                    $(ele).val('');
                }
            }
        }

        function MandatoryField(ele) {
            if ($(ele).is(":checked")) {
                $(ele).closest('tr').find('[type="text"]').addClass("mandatoryfield");
                $(ele).closest('tr').find('[type="text"]').before('<span class="textboxmandatory" style="color: red;">*</span>');
            }
            else {
                $(ele).closest('tr').find('[type="text"]').removeClass("mandatoryfield");
                $(ele).closest('tr').find('[type="text"]').closest('td').find('.textboxmandatory').remove();
            }
        }

        function checkAllItems(ele) {
            debugger;
            if ($(ele).is(":checked")) {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', true);
            }
            else {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', false);
            }
        }

        function UpdateInwardQtyAll(WOIHID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, Should I Update Inward Qty Entirely",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Update it!",
                closeOnConfirm: false
            }, function () {
                __doPostBack("UpdateInwardQty", WOIHID);
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
                                    <h3 class="page-title-head d-inline-block">Work Order Inward QC</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="AdminHome.aspx">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Stores</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Work Order Inward QC</li>
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
                            </div>
                        </div>
                        <div id="divAddNew" runat="server">
                            <div class="ip-div text-center p-t-10">
                                <%--<asp:LinkButton ID="btnAddNew" runat="server" Text="Add New"
                                    CssClass="btn ic-w-btn add-btn"></asp:LinkButton>--%>
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12" style="overflow-x: scroll;">
                                    <asp:GridView ID="gvWorkOrderInwardedIndentDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                        EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium tablestatesave"
                                        OnRowCommand="gvWorkOrderInwardedIndentDetails_OnRowCommand"
                                        OnRowDataBound="gvWorkOrderInwardedIndentDetails_OnRowDataBound"
                                        HeaderStyle-HorizontalAlign="Center" DataKeyNames="WorkOrderDrawingFile,IndentNo,WOIHID">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Indent No" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIndentNo" runat="server" Text='<%# Eval("IndentNo")%>'></asp:Label>
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
                                            <asp:TemplateField HeaderText="WO Drawing File" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnView" runat="server"
                                                        OnClientClick='<%# string.Format("return ViewWODrawingFile({0});",((GridViewRow) Container).RowIndex) %>'>
                                                            <img src="../Assets/images/view.png" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Inward Date" ItemStyle-HorizontalAlign="left"
                                                HeaderStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInwardDate" runat="server" Text='<%# Eval("InwardDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Indent Qty" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotalIndenQty" runat="server" Text='<%# Eval("ActualQty")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total PO Qty" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPoQty" runat="server" Text='<%# Eval("POQty")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total DC Qty" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldcqty" runat="server" Text='<%# Eval("DCQty")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Inward Qty" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInwardQty" runat="server" Text='<%# Eval("InwardQty")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Avail Qty" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAvailQty" runat="server" Text='<%# Eval("AvailQty")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="JobDescription" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblJobDescription" runat="server" Text='<%# Eval("JobDescription")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Add QC" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnAdd" runat="server" CommandName="AddQC" CommandArgument="<%#((GridViewRow) Container).RowIndex %>">
                                                            <img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Update Inward Qty All" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnUpdateAll" runat="server" CommandName="AddQC"
                                                        OnClientClick='<%# string.Format("return UpdateInwardQtyAll({0});",Eval("WOIHID")) %>'>
                                                            <img src="../Assets/images/icon_update.png" style="width:50px;" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
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
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hdnQCAvailQty" runat="server" Value="0" />
                        <asp:HiddenField ID="hdnWOIHID" runat="server" Value="0" />
                        <iframe id="ifrm" runat="server" style="display: none;"></iframe>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal" id="mpeWorkOrderQCClearanceDetails" style="overflow-y: scroll;">
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
                                    <div id="divQCClearence">
                                        <div id="Item" runat="server">
                                            <div id="divOutputsItems" runat="server">

                                                <div class="col-sm-12">
                                                    <div class="col-sm-6">
                                                        <label>Indent Qty: </label>
                                                        <asp:Label ID="lblIndentQty" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <label>Inward Qty: </label>
                                                        <asp:Label ID="lblInwardQty" runat="server"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-6">
                                                        <label>Available Qty: </label>
                                                        <asp:Label ID="lblAvailableQty" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <label>Processed Qty: </label>
                                                        <asp:Label ID="lblProcessedQty" runat="server"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                    <asp:GridView ID="gvWorkOrderPartSnoDetails" runat="server" AutoGenerateColumns="False"
                                                        ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" OnRowDataBound="gvWorkOrderPartSnoDetails_OnRowDataBound"
                                                        CssClass="table table-hover table-bordered medium"
                                                        DataKeyNames="WOIDID">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                                ItemStyle-Width="15%">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkall" runat="server" onclick="return checkAllItems(this);" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkQC" runat="server" onclick="return MandatoryField(this);" AutoPostBack="false" />
                                                                    <%--onclick="return ValidateQCQty(this);"--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="S.No" ItemStyle-Width="15%" HeaderStyle-CssClass="text-center"
                                                                ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Part SNO" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPartSNO" runat="server" Text='<%# Eval("PartSNO")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Job Qty" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblJobQty" runat="server" Text='<%# Eval("WOI_JobQty")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Avail Qty" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAvailQty" runat="server" Text='<%# Eval("JobAvailQty")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Inward Qty" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtInwardQty" runat="server"
                                                                        onkeypress="return fnAllowNumeric(this);"
                                                                        onkeyup="return ValidateInwardQty(this);" Value='<%# Eval("JobAvailQty")%>' CssClass="form-control"></asp:TextBox>
                                                                    <asp:HiddenField ID="hdnAvailQty" runat="server" Value='<%# Eval("JobAvailQty")%>'></asp:HiddenField>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="QC Status" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQCStatus" runat="server" Text='<%# Eval("QCStatus")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Processed Qty"
                                                                ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblProcessedQty" runat="server" Text='<%# Eval("WOI_InwardQty")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                                <div runat="server" id="divProcessTypeOfCheck">
                                                    <div class="col-sm-12 p-t-10">
                                                        <asp:GridView ID="gvQCApplicableList" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                            EmptyDataText="No Records Found" OnRowDataBound="gvQCApplicableList_OnRowDataBound"
                                                            CssClass="table table-hover table-bordered medium" DataKeyNames="WOQCLMID">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                                    ItemStyle-Width="15%">
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkall" runat="server" onclick="return checkAllItems(this);" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkQC" runat="server" AutoPostBack="false" onchange="mandatoryfield();" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="S.No" ItemStyle-Width="15%" HeaderStyle-CssClass="text-center"
                                                                    ItemStyle-CssClass="text-center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Job Operation Name" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblJobOperationList" runat="server" Text='<%# Eval("ProcessName")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%--  <asp:TemplateField HeaderText="Report Path" HeaderStyle-Width="25%" ItemStyle-Width="25%" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:FileUpload ID="fpQCReport" runat="server" TabIndex="12" CssClass="form-control"
                                                                        onchange="DocValidation(this);" Width="95%"></asp:FileUpload>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                                <asp:TemplateField HeaderText="QC Name" HeaderStyle-Width="20%" ItemStyle-Width="20%" ItemStyle-CssClass="text-center"
                                                                    HeaderStyle-CssClass="text-center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblQCType" runat="server" Text='<%# Eval("QCName")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>

                                                    <div class="col-sm-12 p-t-10">
                                                        <div class="col-sm-4">
                                                            <label class="mandatorylbl">Remarks </label>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtQCRemarks" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-2">
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-12 p-t-10">
                                                        <div class="col-sm-4">
                                                            <label>QC Report </label>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <asp:FileUpload ID="fpQCReport" runat="server" TabIndex="12" CssClass="form-control"
                                                                onchange="DocValidation(this);" Width="95%"></asp:FileUpload>
                                                        </div>
                                                        <div class="col-sm-2">
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="col-sm-12 p-t-10 text-center">
                                                    <asp:LinkButton Text="Accepted" CssClass="btn btn-cons btn-save  AlignTop" ID="btnAccepted"
                                                        CommandName="A" runat="server" OnClientClick="return ValidateTypeOfCheck('A')"
                                                        OnClick="btnSaveQCApprove_Click" />
                                                    <asp:LinkButton Text="Rework" CssClass="btn btn-cons btn-save  AlignTop" ID="btnRework"
                                                        CommandName="RW" runat="server" OnClientClick="return ValidateTypeOfCheck('RW')"
                                                        OnClick="btnSaveQCApprove_Click" />
                                                    <asp:LinkButton Text="Rejected" CssClass="btn btn-cons btn-save  AlignTop" ID="btnRejected"
                                                        CommandName="RJ" runat="server" OnClientClick="return ValidateTypeOfCheck('RJ')"
                                                        OnClick="btnSaveQCApprove_Click" />
                                                </div>
                                                <asp:HiddenField ID="hdnJCDID" Value="0" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>
                        </div>
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

