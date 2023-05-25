<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="AssemplyWeldingQCClearance.aspx.cs"
    Inherits="Pages_AssemplyWeldingQCClearance" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function ShowAssemplyJobcardPopUp() {
            $('#mpeAssemplyJobCardDetails').modal('show');
            return false;
        }

        function ShowAssemplyPlanningQCPopUP() {
            $('#mpeAssemplyPlanningQC').modal('show');
            $('#mpeAssemplyJobCardDetails').modal('hide');
            return false;
        }

        function hideQCPopup() {
            $('#mpeAssemplyPlanningQC').modal('hide');
            $('#mpeAssemplyJobCardDetails').modal('show');
            return false;
        }

        function checkAllItems(ele) {
            debugger;
            if ($(ele).is(":checked")) {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', true);

                $(ele).closest('table').find('[type="text"]').not('.chosen-search-input').addClass("mandatoryfield");;
                $(ele).closest('table').find('[type="text"]').not('.chosen-search-input').before('<span class="reqfield" style="color:red">*</span>');

                //$(ele).closest('table').find('[type="file"]').before('<span class="reqfield" style="color:red">*</span>');
                //$(ele).closest('table').find('[type="file"]').addClass("mandatoryfield");
            }
            else {
                $(ele).closest('table').find('[type="text"]').closest('td').find('.reqfield').remove();
                //$(ele).closest('table').find('[type="file"]').closest('td').find('.reqfield').remove()
                //$(ele).closest('table').find('[type="file"]').removeClass("mandatoryfield");
                $(ele).closest('table').find('[type="text"]').not('.chosen-search-input').removeClass("mandatoryfield")
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', false);
            }
        }

        function MandatoryField() {
            if ($(event.target).is(':checked')) {
                //if ($(event.target).closest('tr').find('[type="file"]').hasClass('md')) {
                //  $(event.target).closest('tr').find('[type="file"]').before('<span class="reqfield" style="color:red">*</span>');
                //   $(event.target).closest('tr').find('[type="file"]').addClass("mandatoryfield");
                // }
                $(event.target).closest('tr').find('[type="text"]').not('.chosen-search-input').before('<span class="reqfield" style="color:red">*</span>');
                $(event.target).closest('tr').find('[type="text"]').not('.chosen-search-input').addClass("mandatoryfield");
            }
            else {
                $(event.target).closest('tr').find('[type="text"]').closest('td').find('.reqfield').remove();
                $(event.target).closest('tr').find('[type="text"]').not('.chosen-search-input').removeClass("mandatoryfield");
                //$(event.target).closest('tr').find('[type="file"]').removeClass("mandatoryfield");
                //$(event.target).closest('tr').find('[type="file"]').closest('td').find('.reqfield').remove();
            }
        }

        function ValidateTypeOfCheck(status) {
            var msg = Mandatorycheck('divAJQC');

            var ItemSnoCount = $('#ContentPlaceHolder1_gvBellowSno_AJD').find('[type="checkbox"]:checked').not('#ContentPlaceHolder1_gvBellowSno_AJD_chkall').length;

            if (parseInt(ItemSnoCount) > 0) {
                if (msg) {
                    var CheckedCount = $('#ContentPlaceHolder1_gvAssemplyPlanningQCDetails').find('[type="checkbox"]:checked').not('#ContentPlaceHolder1_gvAssemplyPlanningQCDetails_chkall').length;
                    if (status == 'A') {
                        var RowCount = $('#ContentPlaceHolder1_gvAssemplyPlanningQCDetails').find('tr').length - 1;
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
            else
                ErrorMessage('Error', 'No Item Selected');
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

        function printjobcard(JCHID, Index) {
            __doPostBack("PrintAssemplyJobCard", JCHID);
            return false;
        }

        function printPartToPartjobcard(PAPDID, index) {
            __doPostBack("PartToPartAssemplyJobCardPrint", PAPDID);
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
                                        <img src="../Assets/images/pages.png" alt="" width="16px" />
                                    </span>
                                    <h3 class="page-title-head d-inline-block">Assemply Welding QC Clearance</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="AdminHome.aspx">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Quality</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Assemply Welding QC Clearence Details</li>
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
                        <div id="divradio" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label" style="padding-top: 7px;">
                                            Select Type
                                        </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:RadioButtonList ID="rblRFPChange" runat="server" CssClass="radio radio-success" RepeatDirection="Horizontal"
                                            AutoPostBack="true" OnSelectedIndexChanged="rblRFPChange_OnSelectedChanged" RepeatLayout="Flow">
                                            <asp:ListItem Selected="True" Value="0">PENDING</asp:ListItem>
                                            <asp:ListItem Value="1">COMPLETED</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divAdd" class="btnAddNew" runat="server">
                            <div class="text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Customer Name</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="true" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlCustomerName_OnSelectIndexChanged" Width="70%" ToolTip="Select Customer Number">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
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
                                        <asp:DropDownList ID="ddlRFPNo" runat="server" AutoPostBack="true" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlRFPNo_SelectIndexChanged" Width="70%" ToolTip="Select RFP No">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divInput" class="divInput" runat="server">
                            <div class="ip-div text-center">
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <%--  <div class="col-md-12 row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                                    <asp:Label ID="lbltitle" runat="server" Text=""></asp:Label></p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>--%>
                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvAssemplyJobCardHeaderDetails" runat="server" AutoGenerateColumns="False"
                                                OnRowCommand="gvAssemplyJobCardHeaderDetails_OnRowCommand" OnRowDataBound="gvAssemplyJobCardHeaderDetails_OnRowDataBound"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                DataKeyNames="JCHID,SECONDARYJID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Job Order Number" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJobOrderNumber" runat="server" Text='<%# Eval("SECONDARYJID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Process Name" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProcessName" runat="server" Text='<%# Eval("ProcessName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--    <asp:TemplateField HeaderText="QC Status" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQCStatus" runat="server" Text='<%# Eval("QCStatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Add QC" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnAddQC" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="ADDQC"><img src="../Assets/images/add1.png"/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--   <asp:TemplateField HeaderText="Print" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnprint" runat="server"
                                                                CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                OnClientClick='<%# string.Format("return printjobcard({0},{1});",Eval("JCHID"),((GridViewRow) Container).RowIndex) %>' CommandName="print"><img src="../Assets/images/pdf.png"/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnJCHID" runat="server" Value="0" />
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

    <div class="modal" id="mpeAssemplyJobCardDetails" style="overflow-y: scroll;">
        <div style="max-width: 97%; padding-left: 3%; padding-top: 5%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD"></h4>
                            <asp:Label ID="lblJobNo_AJD" runat="server" Style="font-weight: bold; padding-left: 30px; color: brown;"></asp:Label>
                            <button type="button" class="close btn-primary-purple"
                                data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="divAJD" class="docdiv">
                                <div class="inner-container">
                                    <div class="col-sm-12 p-t-10 text-left" id="div17" runat="server">
                                        <asp:GridView ID="gvAJDetails_AJD" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                            OnRowCommand="gvAJDetails_AJD_OnRowCommand" DataKeyNames="PAPDID">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Part To Part" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPartToPart" runat="server" Text='<%# Eval("PartToPart")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Sno Qty" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemSnoQty" runat="server" Text='<%# Eval("ItemSnoQty")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="QC Status" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQCStatus" runat="server" Text='<%# Eval("QCStatus")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="QC Status" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnAddAJQC" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            CommandName="ViewAssemplyQC"><img src="../Assets/images/add1.png"/></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Job Card Print" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnjobcardprint" runat="server"
                                                            CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            OnClientClick='<%# string.Format("return printPartToPartjobcard({0},{1});",Eval("PAPDID"),((GridViewRow) Container).RowIndex) %>' CommandName="print"><img src="../Assets/images/pdf.png"/></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--   <asp:TemplateField HeaderText="QC Status" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQCStatus" runat="server" Text='<%# Eval("Status")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div class="modal" id="mpeAssemplyPlanningQC" style="overflow-y: scroll;">
        <div style="max-width: 81%; padding-left: 10%; padding-top: 5%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">
                                <asp:Label ID="lblasqcheading_h" runat="server" Style="font-weight: bold; padding-left: 30px; color: brown;"></asp:Label>
                            </h4>

                            <button type="btnc" class="close btn-primary-purple" onclick="return hideQCPopup()"
                                data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="divAJQC" class="docdiv">
                                <div class="inner-container">
                                    <div class="col-sm-12 p-t-10">
                                        <asp:GridView ID="gvBellowSno_AJD" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                            OnRowDataBound="gvBellowSno_AJD_OnRowDataBound" DataKeyNames="PRIDID">
                                            <Columns>
                                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkall" runat="server" onclick="return checkAllItems(this);" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkitems" runat="server" onclick="return MandatoryField(this);"
                                                            AutoPostBack="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bellow Sno"
                                                    HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBellowSno" runat="server" Text='<%# Eval("BellowSno")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--    <asp:TemplateField HeaderText="QC Status" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnAddAJQC" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            CommandName="ViewAssemplyQC"><img src="../Assets/images/add1.png"/></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-sm-12 p-t-10 text-left" id="div1" runat="server">
                                        <asp:GridView ID="gvAssemplyPlanningQCDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                            OnRowDataBound="gvAssemplyPlanningQCDetails_OnRowDataBound" DataKeyNames="QPDID">
                                            <Columns>
                                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
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
                                                <asp:TemplateField HeaderText="Type Of Check" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTypeOfCheck" runat="server" Text='<%# Eval("TypeOfCheck")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Report Path" HeaderStyle-Width="25%" ItemStyle-Width="25%" ItemStyle-CssClass="text-center"
                                                    HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:FileUpload ID="fpQCReport" runat="server" TabIndex="12" CssClass="form-control"
                                                            onchange="DocValidation(this);" Width="95%"></asp:FileUpload>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="20%" ItemStyle-Width="20%"
                                                    HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtRemarks" CssClass="form-control"
                                                            runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--    <asp:TemplateField HeaderText="QC Status" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnAddAJQC" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            CommandName="ViewAssemplyQC"><img src="../Assets/images/add1.png"/></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4 text-right">
                                            <label style="color: #000;">Remarks </label>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtOverallRemarks" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10 text-center">
                                        <asp:LinkButton Text="Accepted" CssClass="btn btn-cons btn-save  AlignTop" ID="btnAccepted"
                                            CommandName="A" runat="server" OnClientClick="return ValidateTypeOfCheck('A')" />
                                        <asp:LinkButton Text="Rework" CssClass="btn btn-cons btn-save  AlignTop" ID="btnRework"
                                            CommandName="RW" runat="server" OnClientClick="return ValidateTypeOfCheck('RW')" />
                                        <asp:LinkButton Text="Rejected" CssClass="btn btn-cons btn-save  AlignTop" ID="btnRejected"
                                            CommandName="RJ" runat="server" OnClientClick="return ValidateTypeOfCheck('RJ')" />
                                    </div>

                                    <div id="divFabricationAndWelding_QC" runat="server">
                                        <div id="divBW_QC" runat="server">
                                            <div class="col-sm-12">
                                                <label style="color: #000; font-size: large; text-transform: uppercase;">
                                                    Before Welding
                                                </label>
                                            </div>
                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvBeforWelding" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No Records Found"
                                                    CssClass="table table-hover table-bordered medium" DataKeyNames="JQMID">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-Width="15%" HeaderStyle-CssClass="text-center"
                                                            ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Stage / Activity" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblstageactivity" runat="server" Text='<%# Eval("Stage")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="V/A" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtverificationavailability" Text='<%# Eval("VerficationAvailability")%>' CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Reference/Observation" HeaderStyle-Width="20%" ItemStyle-Width="20%"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtreferenceobservation" Text='<%# Eval("ReferenceObservation")%>' CssClass="form-control mandatoryfield"
                                                                    runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Verified / By" HeaderStyle-Width="20%" ItemStyle-Width="20%"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtverifiedBy" Text='<%# Eval("VerifiedBy")%>' CssClass="form-control mandatoryfield"
                                                                    runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>

                                            <div class="col-sm-12 text-center">
                                                <asp:LinkButton ID="btn" Text="Save" CssClass="btn btn-cons btn-save  AlignTop" CommandName="BW"
                                                    runat="server" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divBW_QC');"
                                                    OnClick="btnQCStage_Click" />
                                            </div>
                                        </div>
                                        <div id="divDW_QC" runat="server">
                                            <div class="col-sm-12">
                                                <label style="color: #000; font-size: large; text-transform: uppercase;">
                                                    During Welding
                                                </label>
                                            </div>
                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvDuringwelding" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No Records Found"
                                                    CssClass="table table-hover table-bordered medium" DataKeyNames="JQMID">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-Width="15%" HeaderStyle-CssClass="text-center"
                                                            ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Stage / Activity" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblstageactivity" runat="server" Text='<%# Eval("Stage")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="V/A" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtverificationavailability" Text='<%# Eval("VerficationAvailability")%>' CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Reference / Observation" HeaderStyle-Width="20%" ItemStyle-Width="20%"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtreferenceobservation" Text='<%# Eval("ReferenceObservation")%>' CssClass="form-control mandatoryfield"
                                                                    runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Verified / By" HeaderStyle-Width="20%" ItemStyle-Width="20%"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtverifiedBy" Text='<%# Eval("VerifiedBy")%>' CssClass="form-control mandatoryfield"
                                                                    runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-12 text-center">
                                                <asp:LinkButton ID="LinkButton2" Text="Save" CssClass="btn btn-cons btn-save  AlignTop"
                                                    runat="server" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divDW_QC');" CommandName="DW"
                                                    OnClick="btnQCStage_Click" />
                                            </div>
                                        </div>
                                        <div id="divFW_QC">
                                            <div class="col-sm-12">
                                                <label style="color: #000; font-size: large; text-transform: uppercase;">
                                                    Final Welding
                                                </label>
                                            </div>
                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvfinalwelding" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No Records Found"
                                                    CssClass="table table-hover table-bordered medium" DataKeyNames="JQMID">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-Width="15%" HeaderStyle-CssClass="text-center"
                                                            ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Stage / Activity" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblstageactivity" runat="server" Text='<%# Eval("Stage")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="V/A" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtverificationavailability" Text='<%# Eval("VerficationAvailability")%>'
                                                                    CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Reference / Observation" HeaderStyle-Width="20%" ItemStyle-Width="20%"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtreferenceobservation" Text='<%# Eval("ReferenceObservation")%>'
                                                                    CssClass="form-control mandatoryfield"
                                                                    runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Verified / By" HeaderStyle-Width="20%" ItemStyle-Width="20%"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtverifiedBy" Text='<%# Eval("VerifiedBy")%>' CssClass="form-control mandatoryfield"
                                                                    runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-12 text-center">
                                                <asp:LinkButton ID="LinkButton3" Text="Save" CssClass="btn btn-cons btn-save  AlignTop"
                                                    runat="server" OnClientClick="return Mandatorycheck('divFW_QC');" CommandName="FW"
                                                    OnClick="btnQCStage_Click" />
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>


</asp:Content>

