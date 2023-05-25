<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="PrimaryJobOrder.aspx.cs" Inherits="Pages_PrimaryJobOrder" EnableEventValidation="false"
    ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        //$("#search").on("keyup", function () {
        //    if (this.value.length > 0) {
        //        $("li").hide().filter(function () {
        //            return $(this).text().toLowerCase().indexOf($("#search").val().toLowerCase()) != -1;
        //        }).show();
        //    }
        //    else {
        //        $("li").show();
        //    }
        //});

        function ShowViewPopUp() {
            $('#mpeViewdocs').modal({
                backdrop: 'static'
            });
            return false;
        }

        function hidePartDetailpopup() {
            $('#mpePartDetails').modal("hide");
            $('div').removeClass('modal-backdrop');
            return false;
        }

        function PartDetailsShowPopUp() {
            $('#mpePartDetails').modal({
                backdrop: 'static'
            });
            return false;
        }

        function ShowBellowsPopUp() {
            $('#mpeBellowCostDetails').modal({
                backdrop: 'static'
            });
            $('#mpePartDetails').modal("hide");
            $('div').removeClass('modal-backdrop');
            return false;
        }

        function hideBellowDetailsPopUp() {
            $('#mpeBellowCostDetails').modal("hide");
            $('#mpePartDetails').modal("show");
            return false;
        }


        function ValidateBellowCost() {
            if (($('[value="Roll"]').prop('checked') || $('[value="Expandal"]').prop('checked')) && $('[value="Plasma"]').prop('checked') || $('[value="Scisscor"]').prop('checked')) {
                return true;
            }
            else {
                ErrorMessage('Error', 'Please Select The Forming And Tanget Cutting');
                return false;
            }
        }

        function ValidateJobOrderDetails() {
            var msg = Mandatorycheck('ContentPlaceHolder1_divInput');

            if (parseInt($('#ContentPlaceHolder1_gvPartNameDetails').find('[type="checkbox"]:checked').not('#ContentPlaceHolder1_gvPartNameDetails_chkall').length) > 0) {
                msg = true;

            }
            else {
                hideLoader();
                ErrorMessage('Error', 'Select part name');
                msg = false;
            }

           <%-- if (msg == true) {
                $('#ContentPlaceHolder1_gvPartNameDetails').find('.PartUnitRate').each(function () {
                    if ($(this).closest('tr').find('a').attr('href')) {
                        if ($(this).text() == "") {
                            ErrorMessage('Error', 'Calculate below Cost');
                            msg = false;
                            return false;
                        }
                    }
                    else {
                        if ($(this).text() == "") {
                            ErrorMessage('Error', 'please Calculate Part Unit Rate');
                            msg = false;
                            return false;
                        }
                    }
                });
            }
            else {
                msg = false;
            }

            if (msg) {
                var str = "";
                $('#<%=hdnUnitRate.ClientID %>').val($('#<%=lblUnitRate.ClientID %>').text());
                $('#ContentPlaceHolder1_gvPartNameDetails').find('.BOMID').each(function () {
                    if (!$(this).closest('td').find('a').attr('href')) {
                        var BOMID = $(this).text();
                        var PartUnitRate = $(this).closest('tr').find('.PartUnitRate').text();
                        var PartTotalRate = $(this).closest('tr').find('.PartTotalRate').text();
                        var PRDID = $(this).closest('tr').find('.PRDID').text();
                        if (str == "") {
                            str = BOMID + "-" + PartUnitRate + "-" + PartTotalRate + "-" + PRDID;
                        }
                        else {
                            str = str + ',' + BOMID + "-" + PartUnitRate + "-" + PartTotalRate + "-" + PRDID;
                        }
                    }
                });
                $('#<%=hdnPartRateDetails.ClientID %>').val(str);
                return true;
            }
            else {
                hideLoader();
                return false;
            }--%>

            if (msg) {
                return true;
            }
            else {
                return false;
            }
        }

        function PartRateCalculation(ele) {
            var PartTotalRate = '';
            var Rate = parseFloat($(ele).val().split('/')[1]);
            $('#ContentPlaceHolder1_gvPartNameDetails').find('.CalcWeight').each(function () {
                if (!$(this).closest('tr').find('a').attr('href')) {
                    var Weight = parseFloat($(this).val());
                    var PartQty = parseInt($(this).closest('tr').find('.PartQty').text());
                    $(this).closest('tr').find('.PartUnitRate').text(parseFloat(Rate * Weight).toFixed(2));
                    $(this).closest('tr').find('.PartTotalRate').text(parseFloat(Rate * Weight * PartQty).toFixed(2));
                }
            });

            $('#ContentPlaceHolder1_gvPartNameDetails').find('.PartTotalRate').each(function () {
                if (PartTotalRate == '') {
                    PartTotalRate = parseFloat($(this).text());
                }
                else {
                    PartTotalRate = PartTotalRate + parseFloat($(this).text());
                }
            });

            $('#ContentPlaceHolder1_lblUnitRate').text(parseFloat(PartTotalRate).toFixed(2));
        }

        function ShowJobCardDetailsPopUP() {
            $('#mpePaymentDetails').modal('show');
            return false;
        }

        function CalculateContractAmt() {
            var per = $(event.target).val();
            if ($(event.target).val() != "") {
                if (parseFloat(per) <= 100) {
                    var BalanceAmt = parseFloat($('#ContentPlaceHolder1_hdnBalanceAmt').val()).toFixed(2);
                    var per = $(event.target).val();
                    var res = parseFloat(per * (BalanceAmt / 100)).toFixed(2);
                    $('.txtpaidamount').val(res);
                    //if ($('.txtpaidamount').val() > BalanceAmt) {
                    //    $('.txtpaidamount').val('');
                    //    ErrorMessage('Error', 'Enterd Amount Should Not Exceed Balance Amount');
                    //}
                }
                else
                    $(event.target).val(100);
            }
            return false;
        }

        function ValidatePayment(ele) {
            //var msg = Mandatorycheck('ContentPlaceHolder1_divcontractorpaymentInput');
            var EnteredAmt = $(ele).val();
            var BalanceAmt = $(ele).closest('tr').find('[type="hidden"]').val();
            if (parseFloat(BalanceAmt) < parseFloat(EnteredAmt)) {
                $(ele).val('');
                ErrorMessage('Error', 'Enterd Amount Should Not Exceed Balance Amount');
            }
        }

        function deleteConfirm(CPDID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Payment will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deletePayment', CPDID);
            });
            return false;
        }

        function SharePrimaryJoborder() {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Share Primary Job order permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Share it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('SharePJO', '');
            });
            return false;
        }

        function ValidateShareAllPJO() {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Share All item permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Share All it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('ShareAllPJO', '');
            });
            return false;
        }

        function ValidateSendForApproval() {
            if ($('#ContentPlaceHolder1_gvContractorPaymentDetails').find('[type="checkbox"]:checked').not('#ContentPlaceHolder1_gvContractorPaymentDetails_chkall').length > 0)
                return true;
            else {
                ErrorMessage('Error', 'Please select payment');
                return false;
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

        function CalculatePartrate(ele) {
            var ProdAWT = $(ele).val();
            var RateGroup = $('#ContentPlaceHolder1_ddlRateGroup').val().split('/')[1];
            var Qty = parseInt($(ele).closest('tr').find('.PartQty').text());

            var ratetype = $(ele).closest('tr').find('.PJOratetype').text();
            if (ratetype != "F") {
                $(ele).closest('tr').find('.PartUnitRate').text(parseFloat(ProdAWT * RateGroup).toFixed(2));
                $(ele).closest('tr').find('.PartTotalRate').text(parseFloat(ProdAWT * Qty * RateGroup).toFixed(2));
            }
        }

        function checkAllPart(ele) {
            debugger;
            if ($(ele).is(":checked")) {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', true);
                $(ele).closest('table').find('.mn').addClass("mandatoryfield");
                $(ele).closest('table').find('.mn').before('<span class="textboxmandatory" style="color: red;">*</span>');
            }
            else {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', false);
                $(ele).closest('table').find('.mn').removeClass("mandatoryfield");
                $(ele).closest('table').find('.mn').closest('td').find('.textboxmandatory').remove();
            }
        }

        function ValidateItem() {
            if ($('#ContentPlaceHolder1_gvDesignDetails').find("[type=checkbox]:checked").not('#ContentPlaceHolder1_gvDesignDetails_chkall').length > 0) {
                return true;
            }
            else {
                ErrorMessage('Error', 'No Item Selected');
                return false;
            }
        }

        //Mandatorycheck('ContentPlaceHolder1_divcontractorpaymentInput')

        function ValidateSavePayment() {
            var msg = Mandatorycheck('ContentPlaceHolder1_divcontractorpaymentInput');

            var t1 = $('#ContentPlaceHolder1_gvJobCardProcessDetails').find("[type=checkbox]:checked").
                not('#ContentPlaceHolder1_gvJobCardProcessDetails_chkall').length;
            var t2 = $('#ContentPlaceHolder1_gvAssemplyJobCard').find("[type=checkbox]:checked").
                not('#ContentPlaceHolder1_gvAssemplyJobCard_chkall').length;
            if (msg) {
                if (parseInt(t1) > 0 || parseInt(t2) > 0) {
                    return true;
                }
                else {
                    ErrorMessage('Error', 'No Job Card Selected');
                    hideLoader();
                    return false;
                }
            }
            else
                return false;
        }

        ////!$(this).closest('tr').find('a').attr('href')

        //        function CalculateRate() {
        //            var Rate = $('#ContentPlaceHolder1_ddlRateGroup').val().split('/')[1];
        //            var CalculatedWeight = $('#<%=lblUnitCalculatedWeight.ClientID %>').text();
        //            $('#<%=lblUnitRate.ClientID %>').text(parseFloat(parseFloat(Rate).toFixed(2) * parseFloat(CalculatedWeight).toFixed(2)).toFixed(2))
        //            var UnitRate = $('#<%=lblUnitRate.ClientID %>').text();

        //            $('#<%=hdnUnitRate.ClientID %>').val(UnitRate);

        //            return false;
        //        }

        function MandatoryField(ele) {
            if ($(ele).is(":checked")) {
                $(ele).closest('tr').find('.mn').addClass("mandatoryfield");
                $(ele).closest('tr').find('.mn').before('<span class="textboxmandatory" style="color: red;">*</span>');
            }
            else {
                $(ele).closest('tr').find('.mn').removeClass("mandatoryfield");
                $(ele).closest('tr').find('.mn').closest('td').find('.textboxmandatory').remove();
            }
        }



    </script>
    <style type="text/css">
        #ContentPlaceHolder1_gvJobCardProcessDetails {
            table-layout: auto;
        }

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
                                    <h3 class="page-title-head d-inline-block">Primary Job Order</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Production</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Primary Job Order</li>
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

                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvPrimaryJobOrderDetails" runat="server" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                OnRowCommand="gvPrimaryJobOrderDetails_OnRowCommand" OnRowDataBound="gvPrimaryJobOrderDetails_OnRowDataBound"
                                                DataKeyNames="PJOID,RFPDID,PJOStatus">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Job Order ID" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnJobNo" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="Payment">
                                                                <asp:Label ID="lbljoborderID" runat="server" Text='<%# Eval("JobOrderID")%>'></asp:Label>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Name" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Drawing Name" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDrawingName" runat="server" Text='<%# Eval("DrawingName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="QTY" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQTY" runat="server" Text='<%# Eval("QTY")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--    <asp:TemplateField HeaderText="PDF" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnPDF" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="pdf"> <img src="../Assets/images/view.png"/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                    <%--   <asp:TemplateField HeaderText="Unit Rate" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUnitRate" runat="server" Text='<%# Eval("UnitRate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Rate" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalRate" runat="server" Text='<%# Eval("TotalUnitRate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit Calculated Weight" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="54px" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUnitCalculatedWeight" runat="server" Text='<%# Eval("UnitCalculatedWeight")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Calculated Weight" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="54px" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalCalculatedWeight" runat="server" Text='<%# Eval("TotalUnitCaculateWeight")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit AWT Weight" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="54px" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUnitAWTWeight" runat="server" Text='<%# Eval("UnitAWTWeight")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total AWT Weight" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="54px" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalAWTWeight" runat="server" Text='<%# Eval("TotalUnitAWTWeight")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <%--         <asp:TemplateField HeaderText="Rate Group" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRateGroup" runat="server" Text='<%# Eval("GroupName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Job Date" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJobDate" runat="server" Text='<%# Eval("JobDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Add Job Order" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnAddJO" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="AddJobOrder"><img src="../Assets/images/add1.png"/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnPJOID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnEDID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnRFPDID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnItemQTY" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnPartRateDetails" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnUnitRate" runat="server" Value="0" />

                                            <asp:HiddenField ID="hdnContractorAmount" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnPaidAmount" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnBalanceAmt" runat="server" Value="0" />
                                        </div>

                                        <div class="col-sm-12 p-t-20 text-center">
                                            <asp:LinkButton ID="btnShareAllPJO" runat="server" Text="Share PJO" OnClientClick="ValidateShareAllPJO();"
                                                CssClass="btn btn-cons btn-save AlignTop"></asp:LinkButton>
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
    <div class="modal" id="mpeViewdocs" style="height: 125%; top: -10%;">
        <div class="modal-dialog" style="height: 100%;">
            <div class="modal-content" style="height: 100%;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" Style="height: 100%;">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btndownloaddocs" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H2">
                                <div class="text-center">
                                    Documents
                                    <asp:Label ID="Label1" Text="" runat="server"></asp:Label>
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal">
                                x</button>
                        </div>
                        <div class="modal-body" style="height: 100%;">
                            <div class="inner-container" style="height: 100%;">
                                <div class="ip-div text-center" style="margin-bottom: 10px;">
                                    <asp:LinkButton ID="btndownloaddocs" OnClick="btndownloaddocs_Click" ToolTip="PDF DownLoad"
                                        runat="server">
                                       <img src="../Assets/images/pdf.png" /> </asp:LinkButton>
                                </div>
                                <div id="divJoborderPdf" class="FrontPagepopupcontent" runat="server">
                                    <div class="col-sm-12" id="divLSERPLogo" style="padding-top: 30px;" runat="server">
                                        <img src="../Assets/images/topstrrip.jpg" alt="" width="100%">
                                    </div>
                                    <div class="col-sm-12 text-center" style="">
                                        <label style="color: Black; font-weight: bolder; font-size: 20px !important; text-decoration: underline; padding: 10px 0px">
                                            Primary Job Order</label>
                                    </div>
                                    <div style="border: 1px solid #000; float: left; margin-bottom: 20px; padding: 15px 0px">
                                        <div class="col-sm-12">
                                            <div class="col-sm-5 text-left">
                                                <label style="color: black; font-weight: 700">
                                                    Job Order ID</label>
                                            </div>
                                            <div class="col-sm-2 text-center" style="font-weight: bold">
                                                :
                                            </div>
                                            <div class="col-sm-5">
                                                <asp:Label ID="lblJobOrderID" runat="server" Style="font-weight: bold"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-5 text-left">
                                                <label style="color: black; font-weight: 700">
                                                    Date
                                                </label>
                                            </div>
                                            <div class="col-sm-2 text-center" style="font-weight: bold">
                                                :
                                            </div>
                                            <div class="col-sm-5">
                                                <asp:Label ID="lblDate" runat="server" Style="font-weight: bold"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-5 text-left">
                                                <label style="color: black; font-weight: 700">
                                                    RFP No</label>
                                            </div>
                                            <div class="col-sm-2 text-center" style="font-weight: bold">
                                                :
                                            </div>
                                            <div class="col-sm-5">
                                                <asp:Label ID="lblRFPNo" runat="server" Style="font-weight: bold"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-5 text-left">
                                                <label style="color: black; font-weight: 700">
                                                    Item Name:</label>
                                            </div>
                                            <div class="col-sm-2 text-center" style="font-weight: bold">
                                                :
                                            </div>
                                            <div class="col-sm-5">
                                                <asp:Label ID="lblItemName" runat="server" Style="font-weight: bold"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-5 text-left">
                                                <label style="color: black; font-weight: 700">
                                                    Item Quantity:</label>
                                            </div>
                                            <div class="col-sm-2 text-center" style="font-weight: bold">
                                                :
                                            </div>
                                            <div class="col-sm-5" style="font-weight: bold">
                                                <asp:Label ID="lblItemQuantity" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="p-t-10" id="divPartDetailsContent" runat="server">
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-5 text-left">
                                            <label style="color: black; font-weight: 700">
                                                Unit Weight</label>
                                        </div>
                                        <div class="col-sm-2 text-center" style="color: black; font-weight: 700">
                                            :
                                        </div>
                                        <div class="col-sm-5">
                                            <asp:Label ID="lblUnitWeight" runat="server" Style="color: black; font-weight: 700"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-5 text-left">
                                            <label style="color: black; font-weight: 700">
                                                Total Weight</label>
                                        </div>
                                        <div class="col-sm-2 text-center" style="color: black; font-weight: 700">
                                            :
                                        </div>
                                        <div class="col-sm-5" style="color: black; font-weight: 700">
                                            <asp:Label ID="lblTotalWeight_P" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-5 text-left">
                                            <label style="color: black; font-weight: 700">
                                                Unit Rate</label>
                                        </div>
                                        <div class="col-sm-2 text-center" style="color: black; font-weight: 700">
                                            :
                                        </div>
                                        <div class="col-sm-5" style="color: black; font-weight: 700">
                                            <asp:Label ID="lblUnitRate_P" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-5 text-left" style="color: black; font-weight: 700">
                                            <label style="color: black; font-weight: 700">
                                                Total Rate</label>
                                        </div>
                                        <div class="col-sm-2 text-center" style="color: black; font-weight: 700">
                                            :
                                        </div>
                                        <div class="col-sm-5">
                                            <asp:Label ID="lblTotalRate_P" runat="server" Style="color: black; font-weight: 700"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-5 text-left">
                                            <label style="color: black; font-weight: 700">
                                                Remarks</label>
                                        </div>
                                        <div class="col-sm-2 text-center" style="color: black; font-weight: 700">
                                            :
                                        </div>
                                        <div class="col-sm-5" style="color: black; font-weight: 700">
                                            <asp:Label ID="lblRemarks" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-20 text-right">
                                        <label style="color: black; font-weight: bolder; position: fixed; right: 50px; bottom: 40px">
                                            Production Incharge</label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div class="modal" id="mpePartDetails" style="height: 125%; top: -10%;">
        <div class="modal-dialog" style="max-width: 95%; padding-left: 5%; padding-top: 5%;">
            <div class="modal-content" style="margin-bottom: 10%;">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD" style="color: brown;">Primary  Job Order Only For Bellow Part Other Part Not Applicable</h4>
                            <asp:Label ID="lblItemName_P" runat="server" Style="padding-left: 19px; padding-top: 10px; color: #c12424;"></asp:Label>
                            <button type="button" class="close btn-primary-purple" onclick="hidePartDetailpopup();"
                                data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="Div3" class="docdiv">
                                <div class="inner-container">
                                    <div id="divInput" class="divInput" runat="server">
                                        <div class="ip-div text-center">
                                            <div id="divrateGroup" runat="server">
                                                <div class="col-sm-12" style="display: none;">
                                                    <div class="col-sm-4 text-right">
                                                        <label class="form-label">
                                                            Rate Group
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <%-- OnSelectedIndexChanged="ddlRateGroup_SelectIndexChanged"--%>
                                                        <asp:DropDownList ID="ddlRateGroup" runat="server" ToolTip="Select Rate Group"
                                                            CssClass="form-control"
                                                            onChange="PartRateCalculation(this);">
                                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-4">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <asp:LinkButton ID="btnSharePJO" runat="server" Text="Share PJO"
                                                    OnClientClick="SharePrimaryJoborder();"
                                                    CssClass="btn btn-cons btn-save AlignTop"></asp:LinkButton>
                                            </div>
                                            <div class="col-sm-12 p-t-10" id="divpartgrid">
                                                <asp:GridView ID="gvPartNameDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium pagingfalse"
                                                    OnRowCommand="gvPartNameDetails_OnRowCommand" OnRowDataBound="gvPartNameDetails_OnRowDataBound"
                                                    DataKeyNames="BOMID,PRDID,QTY,PJORateType,AWT">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                            ItemStyle-Width="15%">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkall" runat="server" onclick="return checkAllItems(this);" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkQC" runat="server" AutoPostBack="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Part Name" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnBellowsCard" runat="server" Visible="false" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                    CommandName="BellowsCost"><asp:Label runat="server" Text='<%# Eval("PartName")%>'></asp:Label></asp:LinkButton>
                                                                <asp:Label ID="lblPartName" runat="server" Visible="false" Text='<%# Eval("PartName")%>'></asp:Label>
                                                                <asp:Label ID="lblBOMID" CssClass="BOMID" runat="server" Style="display: none" Text='<%# Eval("BOMID")%>'></asp:Label>
                                                                <asp:Label ID="lblPRDID" CssClass="PRDID" runat="server" Style="display: none" Text='<%# Eval("PRDID")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="QTY" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQty" CssClass="PartQty" runat="server" Text='<%# Eval("QTY")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--   <asp:TemplateField HeaderText="Unit AWT" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAWT" runat="server" Text='<%# Eval("AWT")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Prod Unit AWT" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtCalWT" runat="server" onkeyup="return CalculatePartrate(this);"
                                                                    onkeypress="return validationDecimal(this);"
                                                                    CssClass="CalcWeight form-control mandatoryfield" Text='<%# Eval("AWT")%>'></asp:TextBox>
                                                                <asp:Label ID="lblPJOrate" runat="server" Text='<%# Eval("PJORateType")%>' CssClass="PJOratetype" Style="display: none;"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Part Unit Rate" HeaderStyle-CssClass="text-center"
                                                            ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPartUnitRate" CssClass="PartUnitRate" Text='<%# Eval("PartUnitCost")%>'
                                                                    runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Part Total Rate" HeaderStyle-CssClass="text-center"
                                                            ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPartTotalRate" CssClass="PartTotalRate" Text='<%# Eval("PartTotalCost")%>'
                                                                    runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4 text-right">
                                                    <label class="form-label mandatorylbl">
                                                        Remarks</label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control mandatoryfield"
                                                        TextMode="MultiLine" autocomplete="nope" PlaceHolder="Enter Remarks"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-4">
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10" style="display: none;">
                                                <div class="col-sm-4">
                                                    <label class="form-label" style="display: contents;">
                                                        Item Unit AWT Weight
                                                    </label>
                                                    <asp:Label ID="lblUnitAWTWeight" CssClass="p-l-50" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label class="form-label" style="display: contents;">
                                                        Item Unit Calculated Weight
                                                    </label>
                                                    <asp:Label ID="lblUnitCalculatedWeight" CssClass="p-l-50" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label class="form-label mandatorylbl" style="display: contents;">
                                                        Unit Rate</label>
                                                    <asp:Label ID="lblUnitRate" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-20">
                                                <asp:LinkButton ID="btnSave" runat="server" Text="Save" OnClientClick="return ValidateJobOrderDetails();"
                                                    OnClick="btnSave_Click" CssClass="btn btn-cons btn-save AlignTop"></asp:LinkButton>
                                            </div>

                                            <div class="col-sm-12 p-t-10">
                                                <asp:GridView ID="gvPrimaryJobOrderPartCostDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No Records Found"
                                                    OnRowCommand="gvPrimaryJobOrderPartCostDetails_OnRowCommand"
                                                    OnRowDataBound="gvPrimaryJobOrderPartCostDetails_OnRowDataBound"
                                                    CssClass="table table-hover table-bordered medium" DataKeyNames="RFPDID,BOMID">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Part Name" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPartname" runat="server" Text='<%# Eval("PartName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="QTY" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Quantity")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Part Unit Rate" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPartUnitRate" runat="server" Text='<%# Eval("PartUnitRate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Part Total Rate" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPartTotalrate" runat="server" Text='<%# Eval("PartTotalRate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <%--  <asp:TemplateField HeaderText="Production AWT" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProductionAWT" runat="server" Text='<%# Eval("AWT")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="AWT Deviation Status" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAWTDeviationApproval" runat="server" Text='<%# Eval("DeviationStatus")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Rate Group" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRateGroup" runat="server" Text='<%# Eval("RateGroupName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>

                                                        <asp:TemplateField HeaderText="PJO Status" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPJOStatus" runat="server" Text='<%# Eval("PJOStatus")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                    CommandName="DeletePart">
                                                            <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <div class="col-sm-12 text-center">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal" id="mpeBellowCostDetails">
        <div style="max-width: 81%; padding-left: 10%; padding-top: 5%;">
            <div class="modal-content" style="margin-bottom: 15%;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">Bellow Cost Details</h4>
                            <asp:Label ID="lblItemName_b" runat="server" Style="padding-left: 19px; padding-top: 10px; color: #c12424;"></asp:Label>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true"
                                onclick="hideBellowDetailsPopUp();">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="Div2" class="docdiv">
                                <div class="inner-container">
                                    <div id="div4" class="divInput" runat="server">
                                        <div class="ip-div text-center">
                                            <div class="col-sm-12">
                                                <asp:RadioButtonList ID="rblBellowFormingType_b" runat="server" CssClass="radio radio-success rblBellowForming"
                                                    OnSelectedIndexChanged="rblBellowFormingType_b_OnSelectedIndexChanged" RepeatDirection="Horizontal"
                                                    AutoPostBack="true" RepeatLayout="Flow">
                                                    <asp:ListItem Value="Roll">Roll</asp:ListItem>
                                                    <asp:ListItem Value="Expandal">Expandal</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <label class="form-label" style="color: black; font-size: large; font-weight: bolder;">
                                                    Sheet Marking And Cutting Price
                                                </label>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-6 text-right">
                                                    <label class="form-label">
                                                        Number Of Ply
                                                    </label>
                                                </div>
                                                <div class="col-sm-6 text-left">
                                                    <asp:Label ID="lblNumberOfPly_b" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="col-sm-6 text-right">
                                                    <label class="form-label">
                                                        Thickness
                                                    </label>
                                                </div>
                                                <div class="col-sm-6 text-left">
                                                    <asp:Label ID="lblThickness_b" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="col-sm-6 text-right">
                                                    <label class="form-label">
                                                        Quantity
                                                    </label>
                                                </div>
                                                <div class="col-sm-6 text-left">
                                                    <asp:Label ID="lblQuantity_b" Text="single" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="col-sm-6 text-right">
                                                    <label class="form-label">
                                                        Calculated Weight
                                                    </label>
                                                </div>
                                                <div class="col-sm-6 text-left">
                                                    <asp:Label ID="lblCalculatedWeight_b" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="col-sm-6 text-right">
                                                    <label class="form-label">
                                                        Sheet Marketing & Cutting Total Cost
                                                    </label>
                                                </div>
                                                <div class="col-sm-6 text-left">
                                                    <asp:Label ID="lblSheetMarkingCuttingTotalCost_b" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <label class="form-label" style="color: black; font-size: large; font-weight: bolder;">
                                                    Bellow Forming
                                                </label>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <%--     <asp:RadioButtonList ID="rblBellowFormingType_b" runat="server" CssClass="radio radio-success rblBellowForming"
                                                    OnSelectedIndexChanged="rblBellowFormingType_b_OnSelectedIndexChanged" RepeatDirection="Horizontal"
                                                    AutoPostBack="true" RepeatLayout="Flow">
                                                    <asp:ListItem Value="Roll">Roll</asp:ListItem>
                                                    <asp:ListItem Value="Expandal">Expandal</asp:ListItem>
                                                </asp:RadioButtonList>--%>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <asp:Label ID="lblFormTypeCost_b" CssClass="form-label" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <label class="form-label" style="color: black; font-size: large; font-weight: bolder;">
                                                    Tanget Cutting
                                                </label>
                                            </div>
                                            <div class="col-sm-12">
                                                <asp:RadioButtonList ID="rblTangetCuttingCost_b" runat="server" CssClass="radio radio-success rblTangetCutting"
                                                    OnSelectedIndexChanged="rblTangetCuttingCost_b_OnSelectedIndexChanged" RepeatDirection="Horizontal"
                                                    AutoPostBack="true" RepeatLayout="Flow">
                                                    <asp:ListItem Value="Plasma">Plasma</asp:ListItem>
                                                    <asp:ListItem Value="Scisscor">Scisscor</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-12">
                                                <asp:Label ID="lblTangetCuttingTotalCost_b" CssClass="form-label" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <div class="col-sm-12 text-center">
                                    <div class="col-sm-12 p-t-20">
                                        <asp:LinkButton ID="btnSaveBellowCost" runat="server" Text="Save" OnClientClick="return ValidateBellowCost();"
                                            OnClick="btnSaveBellowCost_Click" CssClass="btn btn-cons btn-save AlignTop"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div class="modal" id="mpePaymentDetails" style="height: 125%; top: -10%;">
        <div class="modal-dialog" style="max-width: 95%; padding-top: 5%;">
            <div class="modal-content" style="margin-bottom: 10%;">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD"></h4>
                            <asp:Label ID="lblPJOID_PM" runat="server" Style="padding-left: 19px; padding-top: 10px; color: #c12424;"></asp:Label>
                            <button type="button" class="close btn-primary-purple"
                                data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="dd" class="docdiv">
                                <div class="inner-container">
                                    <div id="divcontractorpaymentInput" class="div" runat="server">
                                        <div class="ip-div text-center">
                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <%--   <asp:GridView ID="gvJobCardProcessDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Job ID" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblJobID" runat="server" Text='<%# Eval("JobID")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Part Name" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPartName" runat="server" Text='<%# Eval("PartName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Qty" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTotalQty" runat="server" Text='<%# Eval("TotalPartQty")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Completed Qty" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcompletedQty" runat="server" Text='<%# Eval("CompletedPartQty")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Process Name" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProcessName" runat="server" Text='<%# Eval("ProcessName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Contractor Name" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblContractorName" runat="server" Text='<%# Eval("ContractorName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Contractor Team List" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblContractorteamname" runat="server" Text='<%# Eval("ContractorTeamName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="User Name" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>--%>
                                                <asp:GridView ID="gvJobCardProcessDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium orderingfalse" DataKeyNames="JCHID,CTDID">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="" ControlStyle-CssClass="nosort" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkall" runat="server" onclick="return checkAllPart(this);" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkitems" onclick="MandatoryField(this);" runat="server" AutoPostBack="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="JobCard No" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblJobCardNo" runat="server" Text='<%# Eval("JobCardNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Part Name" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPartName" runat="server" Text='<%# Eval("PartName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Qty" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbljobqty" runat="server" Text='<%# Eval("Qty")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="UOM" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbluom" runat="server" Text='<%# Eval("UOM")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Unit Cost" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblunitcost" runat="server" Text='<%# Eval("UnitCost")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Process Name" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProcessName" runat="server" Text='<%# Eval("ProcessName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--   <asp:TemplateField HeaderText="Contractor Name" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblContractorName" runat="server" Text='<%# Eval("ContractorName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Contractor Team List" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblContractorteamname" runat="server" Text='<%# Eval("ContractorTeamName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("TotalCost")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Balance Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblbalanceamount" runat="server" Text='<%# Eval("BalanceAmount")%>'></asp:Label>
                                                                <asp:HiddenField ID="hdnBalanceAmount" runat="server"
                                                                    Value='<%# Eval("BalanceAmount")%>'></asp:HiddenField>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Enter Amount" HeaderStyle-Width="100px" ItemStyle-Width="100px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtEnterAmount" runat="server" Width="100px"
                                                                    onkeypress="return validationDecimal(this);" onkeyup="ValidatePayment(this);"
                                                                    CssClass="form-control mn"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Paid Amount" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPaidAmt" runat="server" Text='<%# Eval("PaidAmt")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Payment Date" HeaderStyle-Width="100px" ItemStyle-Width="100px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtpaymentdate" runat="server" Width="100px" CssClass="form-control mn datepicker"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtremarks" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <label>Assemply Job Card List </label>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <asp:GridView ID="gvAssemplyJobCard" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium orderingfalse" DataKeyNames="JCHID,CTDID">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="" ControlStyle-CssClass="nosort" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkall" runat="server" onclick="return checkAllPart(this);" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkitems" onclick="MandatoryField(this);" runat="server" AutoPostBack="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Job CardNo" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblJobCardNo" runat="server" Text='<%# Eval("JobCardNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Process Name" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProcessName" runat="server" Text='<%# Eval("JobName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Contractor Team List" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblContractorteamname" runat="server" Text='<%# Eval("ContractorTeamname")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Cost" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTotalCost" runat="server" Text='<%# Eval("TotalCost")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Balance Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblbalanceamount" runat="server" Text='<%# Eval("BalanceAmount")%>'></asp:Label>
                                                                <asp:HiddenField ID="hdnBalanceAmount" runat="server"
                                                                    Value='<%# Eval("BalanceAmount")%>'></asp:HiddenField>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Enter Amount" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtEnterAmount" runat="server"
                                                                    onkeypress="return validationDecimal(this);" onkeyup="ValidatePayment(this);"
                                                                    CssClass="form-control mn"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Paid Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPaidAmt" runat="server" Text='<%# Eval("PaidAmt")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Payment Date">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtpaymentdate" runat="server" CssClass="form-control mn datepicker"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtremarks" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div id="divpj" style="display: none;" runat="server">
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-4 text-right">
                                                        <label class="form-label mandatorylbl">
                                                            Contractor Name</label>
                                                    </div>
                                                    <div class="col-sm-6 text-left">
                                                        <asp:DropDownList ID="ddlContractorName" runat="server" AutoPostBack="true" CssClass="form-control"
                                                            OnSelectedIndexChanged="ddlContractorName_OnSelectIndexChanged" Width="70%" ToolTip="Select Contractor Name">
                                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-2">
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10 text-center" style="font-size: 17px; color: black; font-weight: bold;">
                                                    Bill In details
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-2 text-right">
                                                        <label class="form-label">
                                                            Amount In Percentage ( % )</label>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtAmountInPercentage" onkeyup="return CalculateContractAmt();" onkeypress="return validationDecimal(this);" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label class="form-label">
                                                            Amount
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtAmount" runat="server" onkeypress="return validationDecimal(this);" CssClass="form-control txtpaidamount"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2" style="display: grid; color: brown; font-weight: bold;">
                                                        <asp:Label ID="lblContractorAmt" runat="server"></asp:Label>
                                                        <asp:Label ID="lblPaidAmt" runat="server"></asp:Label>
                                                        <asp:Label ID="lblBalanceAmt" runat="server"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-2 text-right">
                                                        <label class="form-label">
                                                            Payment Date
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtPaymentDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label class="form-label">
                                                            Remarks</label>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtPaymentRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-2">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-20">
                                                <asp:LinkButton ID="btnSavePayment" runat="server" Text="Save"
                                                    OnClientClick="return ValidateSavePayment();"
                                                    OnClick="btnSavePayment_Click" CssClass="btn btn-cons btn-save AlignTop"></asp:LinkButton>
                                            </div>
                                            <div id="divContractorPayment" runat="server">
                                                <div class="col-sm-12 p-t-10">
                                                    <asp:GridView ID="gvContractorPaymentDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                        EmptyDataText="No Records Found" OnRowDataBound="gvContractorPaymentDetails_OnRowDataBound"
                                                        CssClass="table table-hover table-bordered medium orderingfalse" DataKeyNames="CPDID">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="" ControlStyle-CssClass="nosort" ItemStyle-HorizontalAlign="Center">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkall" runat="server" onclick="return checkAllItems(this);" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkitems" runat="server" AutoPostBack="false" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                                HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Voucher No" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVoucherNo" runat="server" Text='<%# Eval("VoucherNo")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Payment Date" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPaymentDate" runat="server" Text='<%# Eval("PaymentDate")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Approval Status" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblApprovalStatus" runat="server" Text='<%# Eval("ApprovalStatus")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("PaidStatus")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Paid Date" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPaidDate" runat="server" Text='<%# Eval("PaidDate")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Remarks" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Job Card No" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbljobcardNo" runat="server" Text='<%# Eval("JobCardNo")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Process Name" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblprocessname" runat="server" Text='<%# Eval("ProcessName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Part Name" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPartName" runat="server" Text='<%# Eval("PartName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnDelete" runat="server" OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("CPDID")) %>'>
                                                            <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                                <div class="col-sm-12">
                                                    <asp:LinkButton ID="btnSendForApproval" runat="server" Text="Send Approval"
                                                        OnClientClick="return ValidateSendForApproval();"
                                                        OnClick="btnSendForApproval_Click" CssClass="btn btn-cons btn-success"></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <div class="col-sm-12 text-center">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="HiddenField2" runat="server" Value="0" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

</asp:Content>
