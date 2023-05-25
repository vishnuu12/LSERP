<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="SecondaryJobOrderQCClearence.aspx.cs" Inherits="Pages_SecondaryJobOrderQCClearence"
    ClientIDMode="Predictable" ValidateRequest="false" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function ShowAddJobCardPartPopUp() {
            //$('#mpePartDetails').modal({
            //    backdrop: 'static'
            //});
            $('#mpePartDetails').show();
            $('div').removeClass('modal-backdrop');
            return false;
        }

        function ShowJobCardAttachementPopUp() {
            $('#mpejobcardAddAttach').show();
            return false;
        }

        function HideJobCardAttachementPopUp() {
            $('#mpejobcardAddAttach').hide();
            return false;
        }

        function ShowApproveQCPopUp() {
            //            $('#mpeApproveQCList').modal({
            //               backdrop: 'static'
            //            });
            $('#mpeApproveQCList').show();
            $('#mpePartDetails').hide();
            return false;
        }

        function hidePartDetailsPopUp() {
            $('#mpePartDetails').hide();
            return false;
        }

        function HideApproveQCPopUp() {
            $('#mpeApproveQCList').hide();
            ShowAddJobCardPartPopUp();
            return false;
        }

        function HidePartDetails() {
            $('#mpePartDetails').hide();
            return false;
        }

        function ShowMarkingAndCuttingQCStagePopup() {
            $('#mpeQCMarkingAndCuttingStage').show();
            return false;
        }

        function HideMarkingAndCuttingQCStagePopup() {
            $('#mpeQCMarkingAndCuttingStage').hide();
            return false;
        }

        function SHowFabricationWeldingPopUp() {
            $('#mpefabricationweldingpopup').show();
            return false;
        }

        function HideFabricationWeldingPopUp() {
            $('#mpefabricationweldingpopup').hide();
            return false;
        }

        function ShowBellowFormingPopUp() {
            $('#mpebellowformingandtangentcutting').show();
            return false;
        }

        function HideBellowFormingPopup() {
            $('#mpebellowformingandtangentcutting').hide();
            return false;
        }

        function ValidateSave() {
            if ($('#ContentPlaceHolder1_gvJobCardPartDetails').find('[type="checkbox"]:checked').length > 0) {
                $('#ContentPlaceHolder1_gvJobCardPartDetails').find('[type="checkbox"]').each(function (index, value) {
                    if ($(this).is(":checked")) {
                        if ($(this).closest('tr').find(".radio").find('input:checked').length > 0) {
                            Valid = true;
                        }
                        else {
                            ErrorMessage('Error', 'Select Any Approve Or Rework');
                            Valid = false;
                            return false;
                        }
                    }
                });
                if (Valid) {
                    var msg = Mandatorycheck('ContentPlaceHolder1_divQCInput');
                    if (msg) {
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            else {
                ErrorMessage('Error', 'No Part Selected');
                return false;
            }
        }

        function ValidateQCApprove(div) {
            var Valid;
            if ($('#ContentPlaceHolder1_gvApproveQC').find('[type="checkbox"]:checked').length > 0) {
                $('#ContentPlaceHolder1_gvApproveQC').find('[type="checkbox"]').each(function (index, value) {
                    if ($(this).is(":checked")) {
                        if ($(this).closest('tr').find(".radio").find('input:checked').length > 0) {
                            Valid = true;
                        }
                        else {
                            ErrorMessage('Error', 'Select Any Approve Or Rework');
                            Valid = false;
                            return false;
                        }
                    }
                });

                if (Valid) {
                    var msg = Mandatorycheck(div);
                    if (msg) {
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            else {
                ErrorMessage('Error', 'No Part Selected');
                return false;
            }
        }

        function ValidateTypeOfCheck(status) {
            if ($('#ContentPlaceHolder1_gvJobCardPartDetails').find('[type="checkbox"]:checked').not('#ContentPlaceHolder1_gvJobCardPartDetails_chkall').length > 0) {
                var msg = Mandatorycheck('ContentPlaceHolder1_divProcessTypeOfCheck');
                if (msg) {
                    var CheckedCount = $('#ContentPlaceHolder1_gvApproveQC').find('[type="checkbox"]:checked').not('#ContentPlaceHolder1_gvApproveQC_chkall').length;
                    if (status == 'A') {
                        var RowCount = $('#ContentPlaceHolder1_gvApproveQC').find('tr').length - 1;
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
                    if (status == "HOLD") {
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

        function rblChanged() {
            if ($(event.target).val() == "R") {
                $(event.target).parent().closest('table').parent().parent().find('[type="text"]').before('<span class="reqfield" style="color:red">*</span>');
                $(event.target).parent().closest('table').parent().parent().find('[type="text"]').addClass("mandatoryfield")
            }
            else {
                $(event.target).parent().closest('table').parent().parent().find('[type="text"]').closest('td').find('.reqfield').remove()
                $(event.target).parent().closest('table').parent().parent().find('[type="text"]').removeClass("mandatoryfield");
            }
        }

        function mandatoryfield() {
            if ($(event.target).is(':checked')) {
                if ($(event.target).closest('tr').find('[type="file"]').hasClass('md')) {
                    $(event.target).closest('tr').find('[type="file"]').before('<span class="reqfield" style="color:red">*</span>');
                    $(event.target).closest('tr').find('[type="file"]').addClass("mandatoryfield");
                }
                $(event.target).closest('tr').find('[type="text"]').not('.chosen-search-input').before('<span class="reqfield" style="color:red">*</span>');
                $(event.target).closest('tr').find('[type="text"]').not('.chosen-search-input').addClass("mandatoryfield");
            }
            else {
                $(event.target).closest('tr').find('[type="text"]').closest('td').find('.reqfield').remove();
                $(event.target).closest('tr').find('[type="text"]').not('.chosen-search-input').removeClass("mandatoryfield");
                $(event.target).closest('tr').find('[type="file"]').removeClass("mandatoryfield");
            }
        }

        function checkAllItems(ele) {
            debugger;
            if ($(ele).is(":checked")) {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', true);

                $(ele).closest('table').find('.md').before('<span class="reqfield" style="color:red">*</span>');
                $(ele).closest('table').find('.md').addClass("mandatoryfield");

                $(ele).closest('table').find('[type="text"]').not('.chosen-search-input').before('<span class="reqfield" style="color:red">*</span>');
                $(ele).closest('table').find('[type="text"]').not('.chosen-search-input').addClass("mandatoryfield");
            }
            else {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', false);

                $(ele).closest('table').find('[type="text"]').closest('td').find('.reqfield').remove();
                $(ele).closest('table').find('[type="text"]').not('.chosen-search-input').removeClass("mandatoryfield");
                $(ele).closest('table').find('.md').removeClass("mandatoryfield");
            }
        }

        function printjobcard(JCHID, index) {
            __doPostBack("printjobcard", JCHID + '/' + index);
            return false;
        }

        function PrintHtmlFile() {
            var cotent = $('#ContentPlaceHolder1_hdnpdfContent').val();
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');
            winprint.document.write(cotent);
            setTimeout(function () {
                winprint.document.close();
                winprint.focus();
                winprint.print();
                // winprint.close();
            }, 1000);

            return false;
        }

        function PrintMarkinAndCutting(QRCode, Mode) {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var Address = $('#ContentPlaceHolder1_hdnAddress').val();
            var PhoneAndFaxNo = $('#ContentPlaceHolder1_hdnPhoneAndFaxNo').val();
            var Email = $('#ContentPlaceHolder1_hdnEmail').val();
            var WebSite = $('#ContentPlaceHolder1_hdnWebsite').val();

            var DocNo = $('#ContentPlaceHolder1_hdnDocNo').val();
            var RevNo = $('#ContentPlaceHolder1_hdnRevNo').val();
            var RevDate = $('#ContentPlaceHolder1_hdnRevDate').val();

            var FabricationWelding = $('#ContentPlaceHolder1_divFabricationAndWeldingPDF').html();

            var MarkingAndCutting = $('#ContentPlaceHolder1_divMarkingAndCuttingPDF').html();

            var SheetMarkingAndCutting = $('#ContentPlaceHolder1_divSheetMarkingAndCuttingPDF').html();

            var SheetWelding = $('#ContentPlaceHolder1_divSheetWeldingPDF').html();

            var BellowFormingTangentCutting = $('#ContentPlaceHolder1_divBellowFormingAndTangentCuttingPDF').html();

            winprint.document.write("<html><head><title>");
            winprint.document.write("Offer");
            winprint.document.write("</title>");
            //  winprint.document.write("<link rel='stylesheet' type='text/css' href='" + epstyleurl + "'/>");
            // winprint.document.write("<link rel='stylesheet' href='" + Main + "' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet'  href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/printg.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            //   winprint.document.write("<link rel='stylesheet' href='" + topstrip + "' type='text/css'/>");           
            winprint.document.write("<style type='text/css'> label { font-weight: bold ! important;color: #000 ! important; } .contractorborder label{padding: 4px 6px; } .contractorborder { padding-top:5px; border: 1px solid;} .sideheading{ display:flex;align-items:flex-start;justify-content:center;margin-top:15px; } .lbl_h{ width:35%; } .lbl_v{ width:65%; } .Qrcode {height: 60px;width: 80px;margin-right: 10px;padding-left: 20px;} </style>");

            winprint.document.write("<div class='print-page' style='position:fixed;border:1px solid #000;width:200mm;height:287mm;margin-top:5mm;margin-bottom:5mm;margin-left:5mm;margin-right:5mm'></div>");
            winprint.document.write("<table style='width:200mm;height:287mm;margin-left:20px;border:0px !important'><thead><tr><td>");
            winprint.document.write("<div class='col-sm-12' style='text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;height:112px'>");
            winprint.document.write("<div>");
            winprint.document.write("<div>");
            winprint.document.write("<div class='col-sm-12 padding0' id='divLSERPLogo' style='margin:20px auto;display:block;'>");
            //  winprint.document.write("<img src='" + topstrip + "' alt='' height='100px;' style='object-fit:contain;width:100%;display:none'>");
            winprint.document.write("<div class='row' style='position:fixed;width:200mm;left:5mm;border-bottom:1px solid #000;'>");
            winprint.document.write("<div class='col-sm-3'>");
            winprint.document.write("<img src='../Assets/images/lonestar.jpeg' alt='lonestar-image' width='90px' style='margin:5px 0;'>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-6 text-center'>");
            winprint.document.write("<h3 style='font-weight:600;font-size:24px;font-style:italic;color:#000;font-family: Arial;display: contents;'>LONESTAR</h3>");
            winprint.document.write("<span style='font-weight:600;font-size:24px ! important;font-family: Times New Roman;'>INDUSTRIES</span>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;width: 100%;padding:0 0px'>" + Address + "</p>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + PhoneAndFaxNo + "</p>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + Email + "</p>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + WebSite + "</p>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-3'>");
            winprint.document.write("<img src='../Assets/images/iso.jpeg' alt='lonestar-image' height='90px' style='margin:5px 0;'>");
            winprint.document.write("</div></div>");
            winprint.document.write("</div></div></div>");
            winprint.document.write("</td></tr></thead>");
            winprint.document.write("<tbody><tr><td>");

            winprint.document.write("<div style='margin:0 5mm;'>");
            if (Mode == "SMC")
                winprint.document.write(SheetMarkingAndCutting);
            else if (Mode == "MC")
                winprint.document.write(MarkingAndCutting);
            else if (Mode == "FW")
                winprint.document.write(FabricationWelding);
            else if (Mode == "SW")
                winprint.document.write(SheetWelding);
            else if (Mode == "BFTC")
                winprint.document.write(BellowFormingTangentCutting);

            winprint.document.write("</div>");

            winprint.document.write("</td></tr></tbody>");
            winprint.document.write("<tfoot><tr><td>");
            winprint.document.write("<div style='height:30mm;'>");
            winprint.document.write("<div class='row' style='margin-bottom:20px;width:200mm;bottom:0px;position:fixed;'>");

            winprint.document.write("<div class='col-sm-2 text-center'>");
            winprint.document.write("<label style='color:black; font-weight:bolder;'>Quality Incharge</label>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-7 text-center'>");
            winprint.document.write("<label style='width:30%;color:black;float:left; font-weight:bolder;border: 1px solid black;'>Doc No : " + DocNo + "</label>");
            winprint.document.write("<label style='width:25%;color:black;float:left; font-weight:bolder;border: 1px solid black;'>Rev No : " + RevNo + "</label>");
            winprint.document.write("<label style='width:40%;color:black;float:left; font-weight:bolder;border: 1px solid black;'>Rev Date : " + RevDate + "</label>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-3 text-center'>");
            winprint.document.write("<label style='color:black; font-weight:bolder;'> Production Incharge</label><img src='" + QRCode + "' class='Qrcode'/>");
            winprint.document.write("</div>");

            winprint.document.write("</div>");
            winprint.document.write("</div>");
            winprint.document.write("</td></tr></tfoot></table>");
            winprint.document.write("</body></html>");

            setTimeout(function () {
                winprint.document.close();
                winprint.focus();
                winprint.print();
                // winprint.close();
            }, 1000);
        }


        function ViewJobCardattachFileName(index) {
            __doPostBack("viewjobcardAttach", index);
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

        .jobcardpriviewdetails label {
            color: darkgoldenrod;
            font-weight: bolder;
            width: 30%;
        }

        .jobcardpriviewdetails span {
            color: #000;
            font-weight: bold;
        }

        .jobcardpriviewdetails {
            background: azure;
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
                                    <h3 class="page-title-head d-inline-block">Secondary Job Order QC Clearence Details</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Production</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Secondary Job Order QC Clearence Details</li>
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
                                            <asp:GridView ID="gvJobCardHeaderDetails" runat="server" AutoGenerateColumns="False"
                                                OnRowCommand="gvJobCardHeaderDetails_OnRowCommand" OnRowDataBound="gvJobCardHeaderDetails_OnRowDataBound"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                DataKeyNames="JCHID,PartName,SECONDARYJID,ProcessName">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Item Name" HeaderStyle-CssClass="text-left"
                                                        ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitemname" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Part Name" HeaderStyle-CssClass="text-left"
                                                        ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartName" runat="server" Text='<%# Eval("PartName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Job No" HeaderStyle-CssClass="text-center"
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

                                                    <asp:TemplateField HeaderText="QC Status" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQCStatus" runat="server" Text='<%# Eval("QCStatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Add QC" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnAddQC" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="ADDQC"><img src="../Assets/images/add1.png"/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Print" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnprint" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                OnClientClick='<%# string.Format("return printjobcard({0},{1});",Eval("JCHID"),((GridViewRow) Container).RowIndex) %>' CommandName="print"><img src="../Assets/images/pdf.png"/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Add Attach" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnaddattach" runat="server"
                                                                CommandArgument='<%#((GridViewRow) Container).RowIndex %>' CommandName="addjobattch"><img src="../Assets/images/add1.png"/>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--   <asp:TemplateField HeaderText="Add Stage" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnAddStage" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="AddStage"><img src="../Assets/images/add1.png"/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnJCHID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnpdfContent" runat="server" Value="0" />

                                            <asp:HiddenField ID="hdnAddress" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnPhoneAndFaxNo" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnEmail" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnWebsite" runat="server" Value="" />

                                            <asp:HiddenField ID="hdnDocNo" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnRevNo" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnRevDate" runat="server" Value="" />

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

    <div class="modal" id="mpePartDetails" style="overflow-y: scroll;">
        <div style="max-width: 96%; padding-left: 2%; padding-right: 2%; padding-top: 5%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">
                                <asp:Label ID="lblJobCardNumber_P" runat="server" Style="padding-left: 19px; padding-top: 10px; color: #c12424;"></asp:Label></h4>
                            <button type="button" class="close btn-primary-purple" onclick="HidePartDetails();"
                                data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="Div3" class="docdiv">
                                <div class="inner-container">
                                    <div id="divQCInput" runat="server">
                                        <div id="div5" class="divInput" runat="server">
                                            <div class="ip-div text-center">
                                            </div>
                                        </div>
                                        <div id="div6" runat="server">
                                            <div id="divMarkingAndCutting_pv_mc" runat="server" visible="false" class="col-sm-12 p-t-10 jobcardpriviewdetails">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-4" style="display: flex;">
                                                        <label>Item Name </label>
                                                        <asp:Label ID="lblItemname_pv_mc" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Size </label>
                                                        <asp:Label ID="lblsize_pv_mc" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Drawing Name </label>
                                                        <asp:Label ID="lbldrawingname_pv_mc" runat="server"> </asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-4" style="display: flex;">
                                                        <label>Part Name </label>
                                                        <asp:Label ID="lblpartName_pv_mc" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Process name </label>
                                                        <asp:Label ID="lblprocessname_pv_mc" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Category </label>
                                                        <asp:Label ID="lblcategory_pv_mc" runat="server"> </asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-4" style="display: flex;">
                                                        <label>Thickness </label>
                                                        <asp:Label ID="lblthk_pv_mc" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Grade </label>
                                                        <asp:Label ID="lblgrade_pv_mc" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>RFP No </label>
                                                        <asp:Label ID="lblRFPNo_pv_mc" runat="server"> </asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-4" style="display: flex;">
                                                        <label>Tube ID </label>
                                                        <asp:Label ID="lblTubeID_pv_mc" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Stage Of Activity </label>
                                                        <asp:Label ID="lblstageofactivity_pv_mc" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Issue Date </label>
                                                        <asp:Label ID="lblIssueDate_pv_mc" runat="server"> </asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-4">
                                                        <label>Target Date </label>
                                                        <asp:Label ID="lblTargetdate_pv_mc" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Job Card Raised By </label>
                                                        <asp:Label ID="lbljobcardraisedby_pv_mc" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="divMarkingAndCutting_pv_smc" runat="server" visible="false" class="col-sm-12 p-t-10 jobcardpriviewdetails">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-4" style="display: flex;">
                                                        <label>Item Name </label>
                                                        <asp:Label ID="lblItemname_pv_smc" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Size </label>
                                                        <asp:Label ID="lblsize_pv_smc" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Drawing Name </label>
                                                        <asp:Label ID="lbldrawingname_pv_smc" runat="server"> </asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-4" style="display: flex;">
                                                        <label>Part Name </label>
                                                        <asp:Label ID="lblpartName_pv_smc" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Process name </label>
                                                        <asp:Label ID="lblprocessname_pv_smc" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Category </label>
                                                        <asp:Label ID="lblcategory_pv_smc" runat="server"> </asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-4" style="display: flex;">
                                                        <label>Thickness </label>
                                                        <asp:Label ID="lblthk_pv_smc" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Grade </label>
                                                        <asp:Label ID="lblgrade_pv_smc" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>RFP No </label>
                                                        <asp:Label ID="lblRFPNo_pv_smc" runat="server"> </asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-4" style="display: flex;">
                                                        <label>Tube ID </label>
                                                        <asp:Label ID="lblTubeID_pv_smc" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4" style="display: flex;">
                                                        <label>Stage Of Activity </label>
                                                        <asp:Label ID="lblstageofactivity_pv_smc" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Issue Date </label>
                                                        <asp:Label ID="lblIssueDate_pv_smc" runat="server"> </asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-4">
                                                        <label>Target Date </label>
                                                        <asp:Label ID="lblTargetdate_pv_smc" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Job Card Raised By </label>
                                                        <asp:Label ID="lbljobcardraisedby_pv_smc" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="divfabricationwelding_pv_fw" runat="server" visible="false" class="col-sm-12 p-t-10 jobcardpriviewdetails">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-4" style="display: flex;">
                                                        <label>Item Name </label>
                                                        <asp:Label ID="lblItemname_pv_fw" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Size </label>
                                                        <asp:Label ID="lblsize_pv_fw" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Drawing Name </label>
                                                        <asp:Label ID="lbldrawingname_pv_fw" runat="server"> </asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-4" style="display: flex;">
                                                        <label>Part Name </label>
                                                        <asp:Label ID="lblpartName_pv_fw" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Process name </label>
                                                        <asp:Label ID="lblprocessname_pv_fw" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Category </label>
                                                        <asp:Label ID="lblcategory_pv_fw" runat="server"> </asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-4" style="display: flex;">
                                                        <label>Thickness </label>
                                                        <asp:Label ID="lblthk_pv_fw" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Grade </label>
                                                        <asp:Label ID="lblgrade_pv_fw" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>RFP No </label>
                                                        <asp:Label ID="lblRFPNo_pv_fw" runat="server"> </asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-4" style="display: flex;">
                                                        <label>NOP </label>
                                                        <asp:Label ID="lblNOP_pv_fw" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4" style="display: flex;">
                                                        <label>Stage Of Activity </label>
                                                        <asp:Label ID="lblstageofactivity_pv_fw" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Issue Date </label>
                                                        <asp:Label ID="lblIssueDate_pv_fw" runat="server"> </asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-4">
                                                        <label>Target Date </label>
                                                        <asp:Label ID="lblTargetdate_pv_fw" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Job Card Raised By </label>
                                                        <asp:Label ID="lbljobcardraisedby_pv_fw" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="divfabricationwelding_pv_sw" runat="server" visible="false" class="col-sm-12 p-t-10 jobcardpriviewdetails">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-4" style="display: flex;">
                                                        <label>Item Name </label>
                                                        <asp:Label ID="lblItemname_pv_sw" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Size </label>
                                                        <asp:Label ID="lblsize_pv_sw" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Drawing Name </label>
                                                        <asp:Label ID="lbldrawingname_pv_sw" runat="server"> </asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-4" style="display: flex;">
                                                        <label>Part Name </label>
                                                        <asp:Label ID="lblpartName_pv_sw" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Process name </label>
                                                        <asp:Label ID="lblprocessname_pv_sw" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Category </label>
                                                        <asp:Label ID="lblcategory_pv_sw" runat="server"> </asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-4" style="display: flex;">
                                                        <label>Thickness </label>
                                                        <asp:Label ID="lblthk_pv_sw" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Grade </label>
                                                        <asp:Label ID="lblgrade_pv_sw" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>RFP No </label>
                                                        <asp:Label ID="lblRFPNo_pv_sw" runat="server"> </asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-4" style="display: flex;">
                                                        <label>NOP </label>
                                                        <asp:Label ID="lblNOP_pv_sw" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4" style="display: flex;">
                                                        <label>Stage Of Activity </label>
                                                        <asp:Label ID="lblstageofactivity_pv_sw" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Issue Date </label>
                                                        <asp:Label ID="lblIssueDate_pv_sw" runat="server"> </asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-4">
                                                        <label>Target Date </label>
                                                        <asp:Label ID="lblTargetdate_pv_sw" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Job Card Raised By </label>
                                                        <asp:Label ID="lbljobcardraisedby_pv_sw" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="divBellowFormingAndTangentCutting_pv_BFTC" runat="server" visible="false" class="col-sm-12 p-t-10 jobcardpriviewdetails">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-4" style="display: flex;">
                                                        <label>Item Name </label>
                                                        <asp:Label ID="lblItemname_pv_BFTC" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Size </label>
                                                        <asp:Label ID="lblsize_pv_BFTC" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Drawing Name </label>
                                                        <asp:Label ID="lbldrawingname_pv_BFTC" runat="server"> </asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-4" style="display: flex;">
                                                        <label>Part Name </label>
                                                        <asp:Label ID="lblpartName_pv_BFTC" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Process name </label>
                                                        <asp:Label ID="lblprocessname_pv_BFTC" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Category </label>
                                                        <asp:Label ID="lblcategory_pv_BFTC" runat="server"> </asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-4" style="display: flex;">
                                                        <label>Thickness </label>
                                                        <asp:Label ID="lblthk_pv_BFTC" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Grade </label>
                                                        <asp:Label ID="lblgrade_pv_BFTC" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>RFP No </label>
                                                        <asp:Label ID="lblRFPNo_pv_BFTC" runat="server"> </asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-4" style="display: flex;">
                                                        <label>NOP </label>
                                                        <asp:Label ID="lblNOP_pv_BFTC" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4" style="display: flex;">
                                                        <label>Stage Of Activity </label>
                                                        <asp:Label ID="lblstageofactivity_pv_BFTC" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Issue Date </label>
                                                        <asp:Label ID="lblIssueDate_pv_BFTC" runat="server"> </asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-4">
                                                        <label>Target Date </label>
                                                        <asp:Label ID="lblTargetdate_pv_BFTC" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Forming Method </label>
                                                        <asp:Label ID="lblformingmethod_pv_BFTC" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Job Card Raised By </label>
                                                        <asp:Label ID="lbljobcardraisedby_pv_BFTC" runat="server"> </asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="divDimensionDetails" visible="false" runat="server">
                                                <div class="col-sm-12 text-center p-t-10">
                                                    <label>Dimension Details </label>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <asp:GridView ID="gvJobCardQCDimensionDetails" runat="server" AutoGenerateColumns="False"
                                                        ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found"
                                                        OnRowEditing="gvJobCardQCDimensionDetails_RowEditing"
                                                        OnRowCancelingEdit="gvJobCardQCDimensionDetails_RowCancelingEdit"
                                                        OnRowUpdating="gvJobCardQCDimensionDetails_RowUpdating"
                                                        CssClass="table table-hover table-bordered medium pagingfalse" DataKeyNames="Name">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No" ItemStyle-Width="15%" HeaderStyle-CssClass="text-center"
                                                                ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Name" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="As Per Drawing" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAsPerDrawing" runat="server" Text='<%# Eval("AsPerDrawing")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Actual(mm)" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblActual" runat="server" Text='<%# Eval("Actual")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txtActual"  runat="server" Text='<%# Eval("Actual")%>'></asp:TextBox>
                                                                </EditItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:CommandField ButtonType="Image" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"
                                                                ShowEditButton="true" EditText="<img src='~/images/icon_edit.png' title='Edit' />"
                                                                EditImageUrl="../Assets/images/edit-ec.png" CancelImageUrl="../Assets/images/icon_cancel.png"
                                                                UpdateImageUrl="../Assets/images/icon_update.png" ItemStyle-Wrap="false" ControlStyle-Width="20px"
                                                                ControlStyle-Height="20px" HeaderText="Edit" ValidationGroup="edit" HeaderStyle-Width="7%">
                                                                <ControlStyle CssClass="UsersGridViewButton" />
                                                            </asp:CommandField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                        <asp:GridView ID="gvJobCardPartDetails" runat="server" AutoGenerateColumns="False"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" OnRowDataBound="gvJobCardPartDetails_OnRowDataBound"
                                            CssClass="table table-hover table-bordered medium pagingfalse"
                                            DataKeyNames="JCDID">
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
                                                <asp:TemplateField HeaderText="QC Status" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQCStatus" runat="server" Text='<%# Eval("QCStatus")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--   <asp:TemplateField HeaderText="QC" ItemStyle-Width="20px" HeaderStyle-Width="20px"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:RadioButtonList ID="rblQC" runat="server" OnChange="rblChanged();" CssClass="radio radio-success"
                                                                    RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Approval" Value="A"></asp:ListItem>
                                                                    <asp:ListItem Text="Rework" Value="R"></asp:ListItem>
                                                                    <asp:ListItem Text="Reject" Value="RJ"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                <%--    <asp:TemplateField HeaderText="Report Path" HeaderStyle-Width="25%" ItemStyle-Width="25%" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:FileUpload ID="fpQCReport" runat="server" TabIndex="12" CssClass="form-control"
                                                                    onchange="DocValidation(this);" ClientIDMode="Static" Width="95%"></asp:FileUpload>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                <%--  <asp:TemplateField HeaderText="QC Type" HeaderStyle-Width="20%" ItemStyle-Width="20%"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:DropDownList ID="ddlQCType" runat="server" AutoPostBack="true" CssClass="form-control"
                                                                    Width="70%" ToolTip="Select QC Type">
                                                                    <asp:ListItem Text="Internal" Value="Internal"></asp:ListItem>
                                                                    <asp:ListItem Text="Customer" Value="Customer"></asp:ListItem>
                                                                    <asp:ListItem Text="Qc" Value="Qc"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                <%--  <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="20%" ItemStyle-Width="20%"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtRemarks" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                <%--   <asp:TemplateField HeaderText="Approve QC" ItemStyle-Width="20px" HeaderStyle-Width="20px"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnView" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                    CommandName="CheckQC"><img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                    <div runat="server" id="divProcessTypeOfCheck">
                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvApproveQC" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" OnRowDataBound="gvApproveQC_OnRowDataBound"
                                                CssClass="table table-hover table-bordered medium" DataKeyNames="QCPDID,RFPQPDID">
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
                                                    <asp:TemplateField HeaderText="Type Of Check" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCheck" runat="server" Text='<%# Eval("TypeOfCheck")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Report Path" HeaderStyle-Width="25%" ItemStyle-Width="25%" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:FileUpload ID="fpQCReport" runat="server" TabIndex="12" CssClass="form-control"
                                                                onchange="DocValidation(this);" Width="95%"></asp:FileUpload>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="QC Type" HeaderStyle-Width="20%" ItemStyle-Width="20%" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <%--  <asp:DropDownList ID="ddlQCType" runat="server" AutoPostBack="false" CssClass="form-control"
                                                                        Width="70%" ToolTip="Select QC Type">
                                                                        <asp:ListItem Text="Internal" Value="Internal"></asp:ListItem>
                                                                        <asp:ListItem Text="Customer" Value="Customer"></asp:ListItem>
                                                                        <asp:ListItem Text="Qc" Value="Qc"></asp:ListItem>
                                                                    </asp:DropDownList>--%>
                                                            <asp:Label ID="lblQCType" runat="server" Text='<%# Eval("QCType")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="20%" ItemStyle-Width="20%"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRemarks" CssClass="form-control" Text='<%# Eval("QCRemarks")%>'
                                                                runat="server"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
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
                                            CommandName="A" runat="server" OnClientClick="return ValidateTypeOfCheck('A')"
                                            OnClick="btnSaveQCApprove_Click" />
                                        <asp:LinkButton Text="Rework" CssClass="btn btn-cons btn-save  AlignTop" ID="btnRework"
                                            CommandName="RW" runat="server" OnClientClick="return ValidateTypeOfCheck('RW')"
                                            OnClick="btnSaveQCApprove_Click" />
                                        <asp:LinkButton Text="Rejected" CssClass="btn btn-cons btn-save  AlignTop" ID="btnRejected"
                                            CommandName="RJ" runat="server" OnClientClick="return ValidateTypeOfCheck('RJ')"
                                            OnClick="btnSaveQCApprove_Click" />
                                        <asp:LinkButton Text="HOLD" CssClass="btn btn-cons btn-save  AlignTop" ID="btnHOLD"
                                            CommandName="HOLD" runat="server" OnClientClick="return ValidateTypeOfCheck('HOLD')"
                                            OnClick="btnSaveQCApprove_Click" />
                                    </div>

                                    <div class="col-sm-12 p-t-20 text-center">
                                    </div>

                                    <div class="col-sm-12 p-t-20 text-center">
                                        <label style="font-size: 20px; color: brown;">
                                            QUALITY STAGE INSPECTION DETAILS
                                        </label>
                                    </div>

                                    <div id="divStages_QC" runat="server">
                                        <div id="divmarkingAndCuttingqCStage_QC" runat="server" visible="false">
                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvQCStageDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
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
                                                <asp:LinkButton ID="btnQCStage" Text="Save" CssClass="btn btn-cons btn-save  AlignTop" CommandName="MC"
                                                    runat="server" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divmarkingAndCuttingqCStage_QC');"
                                                    OnClick="btnQCStage_Click" />
                                            </div>
                                        </div>

                                        <div id="divFabricationAndWelding_QC" runat="server" visible="false">
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
                                                        EmptyDataText="No Records Found" OnRowDataBound="gvApproveQC_OnRowDataBound"
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
                                                        EmptyDataText="No Records Found" OnRowDataBound="gvApproveQC_OnRowDataBound"
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

                                        <div id="divBellowFormingAndTangentCutting_QC" runat="server" visible="false">
                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvbellowformingtangentcutting" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No Records Found" OnRowDataBound="gvApproveQC_OnRowDataBound"
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
                                            <div class="col-sm-12 p-t-10 text-center">
                                                <asp:LinkButton ID="btnBFTC_QC" Text="Save" CssClass="btn btn-cons btn-save  AlignTop"
                                                    runat="server" OnClientClick="return Mandatorycheck('divBellowFormingAndTangentCutting_QC');" CommandName="BFTC"
                                                    OnClick="btnQCStage_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <asp:HiddenField ID="hdnJCDID" Value="0" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="col-sm-12 text-center">
                                <%--    <asp:LinkButton Text="Save" CssClass="btn btn-cons btn-save  AlignTop" ID="btnSaveJobCard"
                                            runat="server" OnClientClick="return ValidateSave('ContentPlaceHolder1_divQCInput');"
                                            OnClick="btnSaveQCJobCard_Click" />--%>
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

    <%--  <div class="modal" id="mpeQCMarkingAndCuttingStage" style="overflow-y: scroll;">
        <div style="max-width: 81%; padding-left: 10%; padding-top: 5%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">
                                <asp:Label ID="lblprocessname_QC" runat="server" Style="padding-left: 19px; padding-top: 10px; color: #c12424;"></asp:Label></h4>
                            <button type="button" class="close btn-primary-purple" onclick="HideMarkingAndCuttingQCStagePopup();"
                                data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="Div2" class="docdiv">
                                <div class="inner-container">
                                    <div id="divQCApprove" runat="server">
                                        <div id="div8" class="divInput" runat="server">
                                            <div class="ip-div text-center">
                                            </div>
                                        </div>
                                        <div id="div9" runat="server">
                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvQCStageDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
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
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <div class="col-sm-12 text-center">
                                        <asp:LinkButton ID="btnQCStage" Text="Save" CssClass="btn btn-cons btn-save  AlignTop" CommandName="MC"
                                            runat="server" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divQCApprove');"
                                            OnClick="btnQCStage_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="HiddenField2" runat="server" Value="0" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>--%>

    <%--  <div class="modal" id="mpefabricationweldingpopup" style="overflow-y: scroll;">
        <div style="max-width: 81%; padding-left: 10%; padding-top: 5%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">
                                <asp:Label ID="lblfw_h" runat="server" Style="padding-left: 19px; padding-top: 10px; color: #c12424;"></asp:Label></h4>
                            <button type="button" class="close btn-primary-purple" onclick="HideFabricationWeldingPopUp();"
                                data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="divQCFW_S" class="docdiv">
                                <div class="inner-container">
                                    <div id="div1" runat="server">
                                        <div id="div4" class="divInput" runat="server">
                                            <div class="ip-div text-center">
                                            </div>
                                        </div>
                                        <div id="div7" runat="server">
                                            <div id="divFW1" runat="server">
                                                <div class="col-sm-12">
                                                    <label>Before Welding </label>
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
                                                    <asp:LinkButton ID="btnsaveBeforeWelding" Text="Save" CssClass="btn btn-cons btn-save  AlignTop" CommandName="BW"
                                                        runat="server" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divFW1');"
                                                        OnClick="btnQCStage_Click" />
                                                </div>
                                            </div>
                                            <div id="divFW2" runat="server">
                                                <div class="col-sm-12">
                                                    <label>During Welding </label>
                                                </div>
                                                <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                    <asp:GridView ID="gvDuringwelding" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                        EmptyDataText="No Records Found" OnRowDataBound="gvApproveQC_OnRowDataBound"
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
                                                    <asp:LinkButton ID="btnsaveduringwelding" Text="Save" CssClass="btn btn-cons btn-save  AlignTop"
                                                        runat="server" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divFW2');" CommandName="DW"
                                                        OnClick="btnQCStage_Click" />
                                                </div>
                                            </div>
                                            <div id="divFW3">
                                                <div class="col-sm-12">
                                                    <label>Final Welding </label>
                                                </div>
                                                <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                    <asp:GridView ID="gvfinalwelding" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                        EmptyDataText="No Records Found" OnRowDataBound="gvApproveQC_OnRowDataBound"
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
                                                    <asp:LinkButton ID="btnsavefinalwelding" Text="Save" CssClass="btn btn-cons btn-save  AlignTop"
                                                        runat="server" OnClientClick="return Mandatorycheck('divFW3');" CommandName="FW"
                                                        OnClick="btnQCStage_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="HiddenField3" runat="server" Value="0" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>--%>

    <%-- <div class="modal" id="mpebellowformingandtangentcutting" style="overflow-y: scroll;">
        <div style="max-width: 81%; padding-left: 10%; padding-top: 5%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">
                                <asp:Label ID="lblbft_h" runat="server" Style="padding-left: 19px; padding-top: 10px; color: #c12424;"></asp:Label></h4>
                            <button type="button" class="close btn-primary-purple" onclick="HideBellowFormingPopup();"
                                data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="divbftg" class="docdiv">
                                <div class="inner-container">
                                    <div id="div10" runat="server">
                                        <div id="div11" class="divInput" runat="server">
                                            <div class="ip-div text-center">
                                            </div>
                                        </div>
                                        <div id="div12" runat="server">
                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvbellowformingtangentcutting" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No Records Found" OnRowDataBound="gvApproveQC_OnRowDataBound"
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
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <div class="col-sm-12 text-center">
                                        <asp:LinkButton ID="btnsaveBFTC" Text="Save" CssClass="btn btn-cons btn-save  AlignTop"
                                            runat="server" OnClientClick="return Mandatorycheck('divbftg');" CommandName="BFTC"
                                            OnClick="btnQCStage_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="HiddenField4" runat="server" Value="0" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>--%>

    <div id="divMarkingAndCuttingPDF" runat="server" style="display: none;">
        <div id="div16" class="FrontPagepopupcontent" runat="server">
            <div class="text-center" style="background-color: #b0acac; padding-top: 10px; padding-bottom: 10px;">
                <asp:Label Style="color: Black; font-weight: bolder; font-size: large ! important;" ID="lblProcessNameHeader_MC_P"
                    runat="server" Text="">
                </asp:Label>
            </div>
            <div style="margin-top: 10px;">
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Job Order ID</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblJobOrderID_MC_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Date
                        </label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblDate_MC_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            RFP No</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblRFPNo_MC_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Contractor Name:</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblContractorName_MC_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Contractor Team Member Name:</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblContractorTeamMemberName_MC_P" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6" style="overflow-wrap: break-word;">
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Item Name/Size</label>
                        <asp:Label ID="lblItemNameSize_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Drawing Name</label>
                        <asp:Label ID="lblDrawingname_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Part Name</label>
                        <asp:Label ID="lblPartName_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Part Qty</label>
                        <asp:Label ID="lblPartQty_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Process Name</label>
                        <asp:Label ID="lblProcessName_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Material Category</label>
                        <asp:Label ID="lblMaterialCategory_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Material Grade</label>
                        <asp:Label ID="lblmaterialGrade_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Thickness</label>
                        <asp:Label ID="lblThickness_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            MRN Number</label>
                        <asp:Label ID="lblMRNNumber_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Stage Of Activity
                        </label>
                        <asp:Label ID="lblJobOrderRemarks_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Total Cost</label>
                        <asp:Label ID="lblTotalCost_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Dead Line Date
                        </label>
                        <asp:Label ID="lblDeadlineDate_MC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="row p-t-10">
                        <label style="color: Black;">
                            Issue Details</label>
                    </div>
                    <div class="row p-t-10">
                        <asp:GridView ID="gvIssueDetails_MC_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MRN Number" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMRNNumber" runat="server" Text='<%# Eval("MRNNumber")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issued Weight" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIssuedWeight" runat="server" Text='<%# Eval("IssuedWeight")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Length" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLength" runat="server" Text='<%# Eval("Length")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Width" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWidth" runat="server" Text='<%# Eval("Width")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material Return" ItemStyle-HorizontalAlign="left"
                                    HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMatRtn" runat="server" Text='<%# Eval("MaterialReturn")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="row p-t-10">
                        <label>Layout Marking Sketch </label>
                        <div style="min-height: 100px; width: 100%; border: 1px solid;">
                        </div>
                    </div>
                    <div class="row p-t-10">
                        <label style="color: Black;">
                            Part Seriel Number</label>
                    </div>
                    <div class="row">
                        <asp:GridView ID="gvPartSerialNo_MC_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item SNO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemSNO" runat="server" Text='<%# Eval("ItemSNO")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Part S.NO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPartSNO" runat="server" Text='<%# Eval("PartSNo")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>

                    <div style="padding-left: 5px;">
                        <div class="row p-t-10">
                            <label style="color: Black;">
                                Offer QC test</label>
                        </div>
                        <div class="row text-left">
                            <asp:Label ID="lblOfferQC_MC_P" runat="server"></asp:Label>
                        </div>
                        <div class="row p-t-10">
                            <asp:Label ID="lblFabricationType_MC_P" Style="font-weight: bold;" runat="server"></asp:Label>
                        </div>
                        <div id="divfabrication_MC_P" runat="server">
                            <%--  <div class="col-sm-12">
                            <div class="col-sm-6">
                                <label>fabricationName</label>
                            </div>
                            <div class="col-sm-1">
                                <label>:</label>
                            </div>
                            <div class="col-sm-5">
                                <label>fabricationValue</label>
                            </div>
                        </div>--%>
                        </div>

                    </div>
                    <div style="padding-left: 2px;">
                        <label>
                            Remarks
                        </label>
                        <asp:Label ID="lblOverallremarks_MC_P" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-12 p-t-5">
                        <label>Status </label>
                        <asp:Label ID="lbljobcardStatus_MC_P" runat="server"></asp:Label>
                    </div>
                </div>

            </div>

            <div class="col-sm-12 p-t-10">
                <asp:GridView ID="gvQCObservationDetails_MC_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                    EmptyDataText="No Records Found"
                    CssClass="table table-hover table-bordered medium">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center"
                            ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Stage / Activity">
                            <ItemTemplate>
                                <asp:Label ID="lblstageactivity" runat="server" Text='<%# Eval("Stage")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Verification/Availability">
                            <ItemTemplate>
                                <asp:Label ID="txtverificationavailability" Text='<%# Eval("VerficationAvailability")%>' CssClass="mandatoryfield" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reference/Observation">
                            <ItemTemplate>
                                <asp:Label ID="txtreferenceobservation" Text='<%# Eval("ReferenceObservation")%>' CssClass="mandatoryfield"
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Verified / By" HeaderStyle-Width="20%" ItemStyle-Width="20%"
                            HeaderStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="txtverifiedBy" Text='<%# Eval("VerifiedBy")%>' CssClass="mandatoryfield"
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>


        </div>
    </div>

    <div id="divSheetWeldingPDF" runat="server" style="display: none;">
        <div id="d" class="FrontPagepopupcontent" runat="server">
            <div class="col-sm-12 text-center" style="background-color: #b0acac; padding-top: 10px; padding-bottom: 10px;">
                <asp:Label Style="color: Black; font-weight: bolder; font-size: large ! important;" ID="lblProcessName_SW_P_H"
                    runat="server" Text="">
                </asp:Label>
            </div>
            <div>
                <div class="row p-t-10" style="border: 2px solid; margin-top: 10px;">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Job Order ID</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblJobOrderID_SW_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Date
                        </label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblDate_SW_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            RFP No</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblRFPNo_SW_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Contractor Name:</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblContractorName_SW_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Contractor Team Member Name:</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblContractorTeamname_SW_P" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6" style="overflow-wrap: break-word;">
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Item Name/Size</label>
                        <asp:Label ID="lblItemNameSize_SW_P" runat="server" CssClass="lbl_v"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Drawing Name</label>
                        <asp:Label ID="lblDrawingName_SW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Part Name</label>
                        <asp:Label ID="lblPartname_SW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Part Qty</label>
                        <asp:Label ID="lblPartQty_SW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Process Name</label>
                        <asp:Label ID="lblProcessname_SW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Material Category</label>

                        <asp:Label ID="lblMaterialCategory_SW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>

                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Material Grade</label>

                        <asp:Label ID="lblMaterialGrade_SW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Thickness</label>

                        <asp:Label ID="lblThickness_SW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>

                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            NOP</label>

                        <asp:Label ID="lblNOP_SW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>

                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            MRN Number</label>

                        <asp:Label ID="lblMRNNumber_SW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>

                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Stage Of Activity 
                        </label>
                        <asp:Label ID="lblJobOrderRemarks_SW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Total Cost</label>
                        <asp:Label ID="lblTotalCost_SW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Dead Line Date
                        </label>
                        <asp:Label ID="lblDeadlineDate_SW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="row p-t-10">
                        <label>Layout Marking Sketch </label>
                        <div style="min-height: 100px; width: 100%; border: 1px solid;">
                        </div>
                    </div>
                    <div class="col-sm-12 p-t-10">
                        <label style="color: Black;">
                            Part Seriel Number</label>
                    </div>
                    <div class="col-sm-12">
                        <asp:GridView ID="gvPartSno_SW_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item SNO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemSNO" runat="server" Text='<%# Eval("ItemSNO")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Part S.NO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPartSNO" runat="server" Text='<%# Eval("PartSNo")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-sm-12 p-t-10">
                        <label style="color: Black;">
                            Offer QC test</label>
                    </div>
                    <div class="col-sm-12 text-left">
                        <asp:Label ID="lblOfferQCTest_SW_P" runat="server"></asp:Label>
                    </div>

                    <div style="padding-left: 2px;">
                        <label>
                            Remarks
                        </label>
                        <asp:Label ID="lblRemarks_SW_P" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-12 p-t-5">
                        <label>Status </label>
                        <asp:Label ID="lbljobcardstatus_SW_P" runat="server"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="col-sm-12 p-t-10">
                <label style="color: Black;">
                    WPS Details</label>
            </div>
            <div class="col-sm-12 p-t-10">
                <asp:GridView ID="gvWPSDetails_SW_P" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="True"
                    EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                    <Columns>
                        <asp:TemplateField HeaderText="WPS Number" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblMRNNumber" runat="server" Text='<%# Eval("WPSNumber")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Material Grade" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblIssuedWeight" runat="server" Text='<%# Eval("MaterialGrade")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="THK" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblLength" runat="server" Text='<%# Eval("Thickness")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Process" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblWidth" runat="server" Text='<%# Eval("Process")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Filler Grade" ItemStyle-HorizontalAlign="left"
                            HeaderStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblMatRtn" runat="server" Text='<%# Eval("FillerGrade")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amps" ItemStyle-HorizontalAlign="left"
                            HeaderStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblMatRtn" runat="server" Text='<%# Eval("Amps")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Polarity" ItemStyle-HorizontalAlign="left"
                            HeaderStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblMatRtn" runat="server" Text='<%# Eval("Polarity")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Gas Level" ItemStyle-HorizontalAlign="left"
                            HeaderStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblMatRtn" runat="server" Text='<%# Eval("Gaslevel")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <div class="text-center">
                <label>BEFORE WELDING </label>
            </div>
            <div class="col-sm-12 p-t-10">
                <asp:GridView ID="gvbeforewelding_SW_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                    EmptyDataText="No Records Found"
                    CssClass="table table-hover table-bordered medium">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center"
                            ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Stage / Activity">
                            <ItemTemplate>
                                <asp:Label ID="lblstageactivity" runat="server" Text='<%# Eval("Stage")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Verification/Availability">
                            <ItemTemplate>
                                <asp:Label ID="txtverificationavailability" Text='<%# Eval("VerficationAvailability")%>' CssClass="mandatoryfield" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reference/Observation">
                            <ItemTemplate>
                                <asp:Label ID="txtreferenceobservation" Text='<%# Eval("ReferenceObservation")%>' CssClass="mandatoryfield"
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Verified / By" HeaderStyle-Width="20%" ItemStyle-Width="20%"
                            HeaderStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="txtverifiedBy" Text='<%# Eval("VerifiedBy")%>' CssClass="mandatoryfield"
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <div class="text-center p-t-10">
                <label>DURING WELDING </label>
            </div>
            <div class="col-sm-12 p-t-10">
                <asp:GridView ID="gvduringwelding_SW_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                    EmptyDataText="No Records Found"
                    CssClass="table table-hover table-bordered medium">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center"
                            ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Stage / Activity">
                            <ItemTemplate>
                                <asp:Label ID="lblstageactivity" runat="server" Text='<%# Eval("Stage")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Verification/Availability">
                            <ItemTemplate>
                                <asp:Label ID="txtverificationavailability" Text='<%# Eval("VerficationAvailability")%>' CssClass="mandatoryfield" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reference/Observation">
                            <ItemTemplate>
                                <asp:Label ID="txtreferenceobservation" Text='<%# Eval("ReferenceObservation")%>' CssClass="mandatoryfield"
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Verified / By" HeaderStyle-Width="20%" ItemStyle-Width="20%"
                            HeaderStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="txtverifiedBy" Text='<%# Eval("VerifiedBy")%>' CssClass="mandatoryfield"
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <div class="text-center p-t-10">
                <label>FINAL WELDING </label>
            </div>
            <div class="col-sm-12 p-t-10">
                <asp:GridView ID="gvfinalwelding_SW_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                    EmptyDataText="No Records Found"
                    CssClass="table table-hover table-bordered medium">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center"
                            ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Stage / Activity">
                            <ItemTemplate>
                                <asp:Label ID="lblstageactivity" runat="server" Text='<%# Eval("Stage")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Verification/Availability">
                            <ItemTemplate>
                                <asp:Label ID="txtverificationavailability" Text='<%# Eval("VerficationAvailability")%>' CssClass="mandatoryfield" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reference/Observation">
                            <ItemTemplate>
                                <asp:Label ID="txtreferenceobservation" Text='<%# Eval("ReferenceObservation")%>' CssClass="mandatoryfield"
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Verified / By" HeaderStyle-Width="20%" ItemStyle-Width="20%"
                            HeaderStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="txtverifiedBy" Text='<%# Eval("VerifiedBy")%>' CssClass="mandatoryfield"
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

        </div>
    </div>

    <div id="divSheetMarkingAndCuttingPDF" runat="server" style="display: none;">
        <div id="div13" class="FrontPagepopupcontent" runat="server">
            <div class="col-sm-12 text-center" style="background-color: #b0acac; padding-top: 10px; padding-bottom: 10px;">
                <asp:Label Style="color: Black; font-weight: bolder; font-size: large ! important;" ID="lblProcessName_SMC_P_H"
                    runat="server" Text="">
                </asp:Label>
            </div>
            <div>
                <div class="row p-t-10" style="border: 2px solid; margin-top: 10px;">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Job Order ID</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblJobOrderID_SMC_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Date
                        </label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblDate_SMC_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            RFP No</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblRFPNo_SMC_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Contractor Name:</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblContractorName_SMC_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Contractor Team Member Name:</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblContractorTeamname_SMC_P" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6" style="overflow-wrap: break-word;">
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Item Name/Size</label>
                        <asp:Label ID="lblItemNameSize_SMC_P" runat="server" CssClass="lbl_v"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Drawing Name</label>
                        <asp:Label ID="lblDrawingName_SMC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Part Name</label>
                        <asp:Label ID="lblPartname_SMC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Part Qty</label>
                        <asp:Label ID="lblPartQty_SMC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Process Name</label>
                        <asp:Label ID="lblProcessname_SMC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Material Category</label>

                        <asp:Label ID="lblMaterialCategory_SMC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>

                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Material Grade</label>

                        <asp:Label ID="lblMaterialGrade_SMC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Thickness</label>

                        <asp:Label ID="lblThickness_SMC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            MRN Number</label>

                        <asp:Label ID="lblMRNNumber_SMC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Tube ID</label>

                        <asp:Label ID="lblTubeID_SMC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Tube Length</label>

                        <asp:Label ID="lblTubeLength_SMC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Stage Of Activity
                        </label>

                        <asp:Label ID="lblJobOrderRemarks_SMC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Total Cost</label>
                        <asp:Label ID="lblTotalCost_SMC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Dead Line Date
                        </label>
                        <asp:Label ID="lbldeadlineDate_SMC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-12 p-t-10">
                        <label style="color: Black;">
                            Issue Details</label>
                    </div>
                    <div class="col-sm-12 p-t-10">
                        <asp:GridView ID="gvMRNIssueDetails_SMC_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MRN Number" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMRNNumber" runat="server" Text='<%# Eval("MRNNumber")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issued Weight" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIssuedWeight" runat="server" Text='<%# Eval("IssuedWeight")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Length" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLength" runat="server" Text='<%# Eval("Length")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Width" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWidth" runat="server" Text='<%# Eval("Width")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material Return" ItemStyle-HorizontalAlign="left"
                                    HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMatRtn" runat="server" Text='<%# Eval("MaterialReturn")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="row p-t-10">
                        <label>Layout Marking Sketch </label>
                        <div style="min-height: 100px; width: 100%; border: 1px solid;">
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <label style="color: Black;">
                            Part Seriel Number</label>
                    </div>
                    <div class="col-sm-12">
                        <asp:GridView ID="gvPartSno_SMC_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item SNO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemSNO" runat="server" Text='<%# Eval("ItemSNO")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Part S.NO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPartSNO" runat="server" Text='<%# Eval("PartSNo")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-sm-12 p-t-10">
                        <asp:GridView ID="gvPLYDetails_SMC_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="NOP" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNOP" runat="server" Text='<%# Eval("NOP")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Circumference" HeaderStyle-CssClass="text-center"
                                    ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCircumference" runat="server" Text='<%# Eval("Circumferance")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Length" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLength" runat="server" Text='<%# Eval("Length")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="THK" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTHK" runat="server" Text='<%# Eval("THK")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Weight" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWeight" runat="server" Text='<%# Eval("Weight")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>

                    <div class="col-sm-12 p-t-10">
                        <label style="color: Black;">
                            Offer QC test</label>
                    </div>
                    <div class="col-sm-12 text-left">
                        <asp:Label ID="lblOfferQCTest_SMC_P" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-12 p-t-5">
                        <label>
                            Remarks
                        </label>
                        <asp:Label ID="lblOverAllRemarks_SMC_P" runat="server">
                        </asp:Label>
                    </div>
                    <div class="col-sm-12 p-t-5">
                        <label>Status </label>
                        <asp:Label ID="lbljobcardstatus_SMC_P" runat="server"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="col-sm-12 p-t-10">
                <asp:GridView ID="gvQCObservationdetails_SMC_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                    EmptyDataText="No Records Found"
                    CssClass="table table-hover table-bordered medium">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center"
                            ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Stage / Activity">
                            <ItemTemplate>
                                <asp:Label ID="lblstageactivity" runat="server" Text='<%# Eval("Stage")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Verification/Availability">
                            <ItemTemplate>
                                <asp:Label ID="txtverificationavailability" Text='<%# Eval("VerficationAvailability")%>' CssClass="mandatoryfield" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reference/Observation">
                            <ItemTemplate>
                                <asp:Label ID="txtreferenceobservation" Text='<%# Eval("ReferenceObservation")%>' CssClass="mandatoryfield"
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Verified / By" HeaderStyle-Width="20%" ItemStyle-Width="20%"
                            HeaderStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="txtverifiedBy" Text='<%# Eval("VerifiedBy")%>' CssClass="mandatoryfield"
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

    <div id="divFabricationAndWeldingPDF" runat="server" style="display: none;">
        <div id="d1" class="FrontPagepopupcontent" runat="server">
            <div class="col-sm-12 text-center" style="background-color: #b0acac; padding-top: 10px; padding-bottom: 10px;">
                <asp:Label Style="color: Black; font-weight: bolder; font-size: large ! important;" ID="lblProcessName_FW_P_H"
                    runat="server" Text="">
                </asp:Label>
            </div>
            <div>
                <div class="row p-t-10" style="border: 2px solid; margin-top: 10px;">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Job Order ID</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblJobOrderID_FW_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Date
                        </label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblDate_FW_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            RFP No</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblRFPNo_FW_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Contractor Name:</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblContractorName_FW_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Contractor Team Member Name:</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblContractorTeamname_FW_P" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6" style="overflow-wrap: break-word;">
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Item Name/Size</label>
                        <asp:Label ID="lblItemNameSize_FW_P" runat="server" CssClass="lbl_v"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Drawing Name</label>
                        <asp:Label ID="lblDrawingName_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Part Name</label>
                        <asp:Label ID="lblPartname_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Part Qty</label>
                        <asp:Label ID="lblPartQty_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Process Name</label>
                        <asp:Label ID="lblProcessname_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Material Category</label>

                        <asp:Label ID="lblMaterialCategory_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>

                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Material Grade</label>

                        <asp:Label ID="lblMaterialGrade_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Thickness</label>

                        <asp:Label ID="lblThickness_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>

                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            NOP</label>

                        <asp:Label ID="lblNOP_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>

                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            MRN Number</label>

                        <asp:Label ID="lblMRNNumber_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>

                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Stage Of Activity
                        </label>
                        <asp:Label ID="lblJobOrderRemarks_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Total Cost</label>
                        <asp:Label ID="lblTotalCost_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Dead Line Date
                        </label>
                        <asp:Label ID="lblDeadLineDate_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="row p-t-10">
                        <label>Weld Joint Sketch </label>
                        <div style="min-height: 100px; width: 100%; border: 1px solid;">
                        </div>
                    </div>
                    <div class="col-sm-12 p-t-10">
                        <label style="color: Black;">
                            Part Seriel Number</label>
                    </div>
                    <div class="col-sm-12">
                        <asp:GridView ID="gvPartSno_FW_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item SNO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemSNO" runat="server" Text='<%# Eval("ItemSNO")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Part S.NO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPartSNO" runat="server" Text='<%# Eval("PartSNo")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-sm-12 p-t-10">
                        <label style="color: Black;">
                            Offer QC test</label>
                    </div>
                    <div class="col-sm-12 text-left">
                        <asp:Label ID="lblOfferQCTest_FW_P" runat="server"></asp:Label>
                    </div>

                    <div class="col-sm-12 p-t-10">
                        <asp:Label ID="lblFabricationType_FW_P" Style="font-weight: bold;" runat="server"></asp:Label>
                    </div>
                    <div id="divfabrication_FW_P" runat="server">
                    </div>

                    <div style="padding-left: 2px;">
                        <label>
                            Remarks
                        </label>
                        <asp:Label ID="lblRemarks_FW_P" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-12 p-t-5">
                        <label>Status </label>
                        <asp:Label ID="lbljobcardStatus_FW_P" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 p-t-10">
                <label style="color: Black;">
                    WPS Details</label>
            </div>
            <div class="col-sm-12 p-t-10">
                <asp:GridView ID="gvWPSDetails_FW_P" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="True"
                    EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                    <Columns>
                        <asp:TemplateField HeaderText="WPS Number" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblMRNNumber" runat="server" Text='<%# Eval("WPSNumber")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Material Grade" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblIssuedWeight" runat="server" Text='<%# Eval("MaterialGrade")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="THK" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblLength" runat="server" Text='<%# Eval("Thickness")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Process" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblWidth" runat="server" Text='<%# Eval("Process")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Filler Grade" ItemStyle-HorizontalAlign="left"
                            HeaderStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblMatRtn" runat="server" Text='<%# Eval("FillerGrade")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amps" ItemStyle-HorizontalAlign="left"
                            HeaderStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblMatRtn" runat="server" Text='<%# Eval("Amps")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Polarity" ItemStyle-HorizontalAlign="left"
                            HeaderStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblMatRtn" runat="server" Text='<%# Eval("Polarity")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Gas Level" ItemStyle-HorizontalAlign="left"
                            HeaderStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblMatRtn" runat="server" Text='<%# Eval("Gaslevel")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <div class="text-center">
                <label>BEFORE WELDING </label>
            </div>
            <div class="col-sm-12 p-t-10">
                <asp:GridView ID="gvBeforeWelding_FW_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                    EmptyDataText="No Records Found"
                    CssClass="table table-hover table-bordered medium">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center"
                            ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Stage / Activity">
                            <ItemTemplate>
                                <asp:Label ID="lblstageactivity" runat="server" Text='<%# Eval("Stage")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Verification/Availability">
                            <ItemTemplate>
                                <asp:Label ID="txtverificationavailability" Text='<%# Eval("VerficationAvailability")%>' CssClass="mandatoryfield" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reference/Observation">
                            <ItemTemplate>
                                <asp:Label ID="txtreferenceobservation" Text='<%# Eval("ReferenceObservation")%>' CssClass="mandatoryfield"
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Verified / By" HeaderStyle-Width="20%" ItemStyle-Width="20%"
                            HeaderStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="txtverifiedBy" Text='<%# Eval("VerifiedBy")%>' CssClass="mandatoryfield"
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <div class="text-center p-t-10">
                <label>DURING WELDING </label>
            </div>
            <div class="col-sm-12 p-t-10">
                <asp:GridView ID="gvDuringWelding_FW_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                    EmptyDataText="No Records Found"
                    CssClass="table table-hover table-bordered medium">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center"
                            ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Stage / Activity">
                            <ItemTemplate>
                                <asp:Label ID="lblstageactivity" runat="server" Text='<%# Eval("Stage")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Verification/Availability">
                            <ItemTemplate>
                                <asp:Label ID="txtverificationavailability" Text='<%# Eval("VerficationAvailability")%>' CssClass="mandatoryfield" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reference/Observation">
                            <ItemTemplate>
                                <asp:Label ID="txtreferenceobservation" Text='<%# Eval("ReferenceObservation")%>' CssClass="mandatoryfield"
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Verified / By" HeaderStyle-Width="20%" ItemStyle-Width="20%"
                            HeaderStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="txtverifiedBy" Text='<%# Eval("VerifiedBy")%>' CssClass="mandatoryfield"
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <div class="text-center p-t-10">
                <label>FINAL WELDING </label>
            </div>
            <div class="col-sm-12 p-t-10">
                <asp:GridView ID="gvFinalWelding_FW_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                    EmptyDataText="No Records Found"
                    CssClass="table table-hover table-bordered medium">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center"
                            ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Stage / Activity">
                            <ItemTemplate>
                                <asp:Label ID="lblstageactivity" runat="server" Text='<%# Eval("Stage")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Verification/Availability">
                            <ItemTemplate>
                                <asp:Label ID="txtverificationavailability" Text='<%# Eval("VerficationAvailability")%>' CssClass="mandatoryfield" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reference/Observation">
                            <ItemTemplate>
                                <asp:Label ID="txtreferenceobservation" Text='<%# Eval("ReferenceObservation")%>' CssClass="mandatoryfield"
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Verified / By" HeaderStyle-Width="20%" ItemStyle-Width="20%"
                            HeaderStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="txtverifiedBy" Text='<%# Eval("VerifiedBy")%>' CssClass="mandatoryfield"
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

    <div id="divBellowFormingAndTangentCuttingPDF" runat="server" style="display: none;">
        <div id="BFTC" class="FrontPagepopupcontent" runat="server">
            <div class="col-sm-12 text-center" style="background-color: #b0acac; padding-top: 10px; padding-bottom: 10px;">
                <asp:Label Style="color: Black; font-weight: bolder; font-size: large ! important;" ID="lblProcessName_BFTC_P_H"
                    runat="server" Text="">
                </asp:Label>
            </div>
            <div>
                <div class="row p-t-10" style="border: 2px solid; margin-top: 10px;">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Job Order ID</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblJobOrderID_BFTC_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Date
                        </label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblDate_BFTC_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            RFP No</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblRFPNo_BFTC_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Contractor Name:</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblContractorName_BFTC_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Contractor Team Member Name:</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblContractorTeamname_BFTC_P" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6" style="overflow-wrap: break-word;">
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Item Name/Size</label>
                        <asp:Label ID="lblItemNameSize_BFTC_P" runat="server" CssClass="lbl_v"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Drawing Name</label>
                        <asp:Label ID="lblDrawingName_BFTC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Part Name</label>
                        <asp:Label ID="lblPartname_BFTC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Part Qty</label>
                        <asp:Label ID="lblPartQty_BFTC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Process Name</label>
                        <asp:Label ID="lblProcessname_BFTC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Material Category</label>

                        <asp:Label ID="lblMaterialCategory_BFTC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>

                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Material Grade</label>

                        <asp:Label ID="lblMaterialGrade_BFTC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Thickness</label>

                        <asp:Label ID="lblThickness_BFTC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>

                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            NOP</label>

                        <asp:Label ID="lblNOP_BFTC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>

                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            MRN Number</label>

                        <asp:Label ID="lblMRNNumber_BFTC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>

                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Stage Of Activity
                        </label>
                        <asp:Label ID="lblJobOrderRemarks_BFTC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>

                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Total Cost</label>
                        <asp:Label ID="lblTotalCost_BFTC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>

                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Dead Line Date
                        </label>
                        <asp:Label ID="lbldeadlineDate_BFTC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>

                </div>
                <div class="col-sm-6">
                    <div class="col-sm-12 p-t-10">
                        <label style="color: Black;">
                            Part Seriel Number</label>
                    </div>
                    <div class="col-sm-12">
                        <asp:GridView ID="gvPartSno_BFTC_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item SNO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemSNO" runat="server" Text='<%# Eval("ItemSNO")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Part S.NO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPartSNO" runat="server" Text='<%# Eval("PartSNo")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-sm-12 p-t-10">
                        <label style="color: Black;">
                            Offer QC test</label>
                    </div>
                    <div class="col-sm-12 text-left">
                        <asp:Label ID="lblOfferQCTest_BFTC_P" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-12 p-t-10">
                        <asp:Label ID="lblBellowDetails_BFTC_P" Style="font-weight: bold;" runat="server"></asp:Label>
                    </div>
                    <div id="divBellowDetails_BFTC_P" runat="server">
                    </div>
                    <div style="padding-left: 2px;">
                        <label>
                            Remarks
                        </label>
                        <asp:Label ID="lblremarks_BETC_P" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-12 p-t-5">
                        <label>Status </label>
                        <asp:Label ID="lbljobcardstatus_BFTC_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div id="divNumberOfStages_BFTC_P" runat="server" style="display: none;">
                    <div class="col-sm-12 p-t-10">
                        <asp:GridView ID="gvNumberOfStages_BFTC_P" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="True"
                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                            <Columns>
                                <asp:TemplateField HeaderText="Stages" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMRNNumber" runat="server" Text='<%# Eval("Stages")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inner" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMRNNdumber" runat="server" Text='<%# Eval("Inner")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Outer" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMRdfNNumber" runat="server" Text='<%# Eval("outer")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Gap" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMRNsfNumber" runat="server" Text='<%# Eval("gap")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <div class="col-sm-12 p-t-10">
                <asp:GridView ID="gvStageActivity_BFTC_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                    EmptyDataText="No Records Found"
                    CssClass="table table-hover table-bordered medium">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center"
                            ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Stage / Activity">
                            <ItemTemplate>
                                <asp:Label ID="lblstageactivity" runat="server" Text='<%# Eval("Stage")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Verification/Availability">
                            <ItemTemplate>
                                <asp:Label ID="txtverificationavailability" Text='<%# Eval("VerficationAvailability")%>' CssClass="mandatoryfield" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reference/Observation">
                            <ItemTemplate>
                                <asp:Label ID="txtreferenceobservation" Text='<%# Eval("ReferenceObservation")%>' CssClass="mandatoryfield"
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Verified / By" HeaderStyle-Width="20%" ItemStyle-Width="20%"
                            HeaderStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="txtverifiedBy" Text='<%# Eval("VerifiedBy")%>' CssClass="mandatoryfield"
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

        </div>
    </div>

    <div class="modal" id="mpejobcardAddAttach" style="overflow-y: scroll;">
        <div style="max-width: 81%; padding-left: 10%; padding-top: 5%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSaveAttachements" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">
                                <asp:Label ID="lbljobcardattchheader_h" runat="server" Style="padding-left: 19px; padding-top: 10px; color: #c12424;"></asp:Label></h4>
                            <button type="button" class="close btn-primary-purple" onclick="HideJobCardAttachementPopUp();"
                                data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="divjobcardattach" class="docdiv">
                                <div class="inner-container">
                                    <div id="div10" runat="server">
                                        <div id="div11" class="divInput" runat="server">
                                            <div class="ip-div text-center">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-3">
                                                <label class="form-label mandatorylbl">
                                                    Attachment Description
                                                </label>
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtDescription" runat="server" TabIndex="6" placeholder="Enter Attachement Description"
                                                    ToolTip="Enter Attachment Description" TextMode="MultiLine" CssClass="form-control mandatoryfield"
                                                    MaxLength="300" autocomplete="nope"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-3">
                                                <label class="form-label mandatorylbl">
                                                    Attachement
                                                </label>
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:FileUpload ID="fAttachment" runat="server" TabIndex="12" CssClass="form-control mandatoryfield"
                                                    onchange="DocValidation(this);" ClientIDMode="Static" Width="95%"></asp:FileUpload>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-3">
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <asp:LinkButton Text="Submit" CssClass="btn btn-cons btn-save  AlignTop" ID="btnSaveAttachements"
                                                        runat="server" OnClientClick="return Mandatorycheck('divjobcardattach');"
                                                        OnClick="btnSaveAttchement_Click" />
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                            </div>
                                            <div class="col-sm-3">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvAttachments" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                OnRowCommand="gvAttachments_OnRowCommand"
                                                DataKeyNames="AttachementID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDescription_V" runat="server" Text='<%# Eval("Description")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Attach Date" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAtachementsCreatedOn" runat="server" Text='<%# Eval("AttachOn")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFileName_V" runat="server" Visible="false" Text='<%# Eval("FileName")%>'></asp:Label>
                                                            <asp:LinkButton ID="lbtnView" runat="server"
                                                                CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                OnClientClick='<%# string.Format("return ViewJobCardattachFileName({0});",((GridViewRow) Container).RowIndex) %>'><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="DeleteAttachement">
                                                           <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                    </div>
                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="HiddenField4" runat="server" Value="0" />
                        <iframe id="ifrm" style="display: none;" runat="server"></iframe>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

</asp:Content>
