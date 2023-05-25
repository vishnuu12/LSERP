<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="VendorDC.aspx.cs" Inherits="Pages_VendorDC" ClientIDMode="Predictable" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function ShareDC() {
            swal({
                title: "Are you sure?",
                text: "If Yes, the DC Will be permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Share it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('ShareDC', "");
            });
            return false;
        }

        function ShowDCPopUp() {
            $('#mpeAddVendorDC').modal('show');
            return false;
        }

        function HideDCPopup() {
            $('#mpeAddVendorDC').modal('hide');
            return false;
        }

        function ValidatePOItemDetails() {
            var msg = Mandatorycheck('ContentPlaceHolder1_divInput_P');
            if (msg) {
                if ($('#ContentPlaceHolder1_gvWorkOrderIndent').find('[type="checkbox"]').not('#ContentPlaceHolder1_gvWorkOrderIndent_chkall').is(':checked')) {
                    return true;
                }
                else {
                    ErrorMessage('Error', 'No PO Selected');
                    hideLoader();
                    return false;
                }
            }
            else
                return false;
        }

        function ValidateDCQty(ele) {

            var APDC = $(ele).closest('td').find('.APDC').text();

            if ($(ele).val() != "") {
                var DCQty = parseInt($(ele).val());
                var AvailQty = parseInt($(ele).closest('tr').find('.DCAvailQty').text());
                if (DCQty > AvailQty && DCQty > 0) {
                    ErrorMessage('Error', 'Entered Quantity Should Not Greater Than Available Qty');
                    $(ele).val('');
                }
                if (APDC == "0") {
                    if (parseInt(DCQty) < parseInt(AvailQty)) {
                        ErrorMessage('Error', 'Entered Quantity Should Not Less Than Available Qty Contact Adminstartor');
                        $(ele).val('');
                    }
                }
            }
            return false;
        }

        function ShowItemNameDetails() {
            $('#mpeWorkOrderIndentDetails').modal('show');
            // $('#mpeAddVendorDC').modal('hide');
            $('div').removeClass('modal-backdrop');
            return false;
        }

        function HideItemnameDetails() {
            $('#mpeWorkOrderIndentDetails').modal('hide');
            // $('div').removeClass('modal-backdrop');
            return false;
        }

        function MandatoryField(ele) {
            if ($(ele).is(":checked")) {
                $(ele).closest('tr').find('[type="text"]').addClass("mandatoryfield");
                $(ele).closest('tr').find('textarea').addClass("mandatoryfield");
                $(ele).closest('tr').find('[type="text"]').before('<span class="textboxmandatory" style="color: red;">*</span>');
                $(ele).closest('tr').find('textarea').before('<span class="textboxmandatory" style="color: red;">*</span>');
            }
            else {
                $(ele).closest('tr').find('[type="text"]').removeClass("mandatoryfield");
                $(ele).closest('tr').find('textarea').removeClass("mandatoryfield");
                $(ele).closest('tr').find('[type="text"]').closest('td').find('.textboxmandatory').remove();
                $(ele).closest('tr').find('textarea').closest('td').find('.textboxmandatory').remove();
            }
        }

        function VendorDC(index) {
            __doPostBack("PrintVendorDC", index);
            return false;
        }

        function PrintVendorDC() {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var TINNo = $('#ContentPlaceHolder1_hdnTINNo').val();
            var CodeNo = $('#ContentPlaceHolder1_hdnCodeNo').val();
            var CSTNo = $('#ContentPlaceHolder1_hdnCSTNo').val();
            var GSTNumber = $('#ContentPlaceHolder1_hdnGSTNumber').val();
            var Address = $('#ContentPlaceHolder1_hdnAddress').val();
            var CompanyName = $('#ContentPlaceHolder1_hdnCompanyName').val();

            var divc1 = $('#ContentPlaceHolder1_divc1').html();
            var divc3 = $('#ContentPlaceHolder1_divc3').html();


            winprint.document.write("<html><head><title>");
            winprint.document.write("Offer");
            winprint.document.write("</title>");

            winprint.document.write("<link rel='stylesheet' type='text/css' href='../Assets/css/ep-style.css'/>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='../Assets/css/main.css' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet'  href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/print.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");

            winprint.document.write("<div class='print-page'>");
            winprint.document.write("<table style='border-width:0px'><thead><tr><td>");
            winprint.document.write("<div class='col-sm-12 header-space' style='text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;height:120px'>");
            winprint.document.write("<div class='header' style='background:transparent;border:0px solid #000'>");
            winprint.document.write("<div>");
            winprint.document.write("<div class='col-sm-12 padding0' id='divLSERPLogo' style='margin:0px auto;display:block;padding:5px 0px;'>");

            winprint.document.write("<div class='col-sm-12 text-center'>");
            winprint.document.write("<h3 style='font-weight:600;font-size:24px;color:#000;font-family: Arial;display: contents;font-weight:700'>" + CompanyName + "</h3>");
            winprint.document.write("<p style='font-weight:500;color:#000; word-break: break-word;'>" + Address + " <span> | </span>GST No:" + GSTNumber + " <span> | </span></p>");
            
            winprint.document.write("</div>");
            winprint.document.write("</div></div></div>");
            winprint.document.write("</td></tr></thead>");
            winprint.document.write("<tbody><tr><td>");
            winprint.document.write("<div class='col-sm-12 padding:0' style='padding-top:0px;'>");

            winprint.document.write(divc1);

            winprint.document.write("<div class='col-sm-12 p-t-10'>");

            winprint.document.write("<table class='table table-hover table-bordered medium' cellspacing='0' rules='all' border='1' id='ContentPlaceHolder1_gvPurchaseOrder_P' style='border-collapse:collapse;'>");
            winprint.document.write("<tbody>");

            var mergerowlen = $('#ContentPlaceHolder1_gvWorkOrderPOItemDetails_p').find('tr').length - 1;

            $('#ContentPlaceHolder1_gvWorkOrderPOItemDetails_p > tbody > tr').each(function (index, tr) {
                if (index == 0)
                    winprint.document.write("<tr align='center'>" + $('#ContentPlaceHolder1_gvWorkOrderPOItemDetails_p').find('tr')[0].innerHTML + "</tr>");
                else {
                    winprint.document.write("<tr align='center'><td>" + $(this).find('td')[0].innerHTML + "</td>");
                    winprint.document.write("<td>" + $(this).find('td')[1].innerHTML + "</td>");
                    if (index == 1) {
                        winprint.document.write("<td rowspan='" + mergerowlen + "'>" + $(this).find('td')[2].innerHTML + "</td>");
                        winprint.document.write("<td rowspan='" + mergerowlen + "'>" + $(this).find('td')[3].innerHTML + "</td>");
                    }
                    winprint.document.write("</tr>");
                }
            });

            //" + parseInt(RowLen) - 1 + "

            winprint.document.write("</tbody>");
            winprint.document.write("</table>");

            winprint.document.write("</div>");

            winprint.document.write(divc3);

            winprint.document.write("</div>");
            winprint.document.write("</td></tr></tbody>");
            winprint.document.write("<tfoot><tr><td>");
            winprint.document.write("</td></tr></tfoot></table>");
            winprint.document.write("</div>");
            winprint.document.write("</html>");

            //winprint.document.write("</div></div>");

            setTimeout(function () {
                winprint.document.close();
                winprint.focus();
                winprint.print();
                // winprint.close();
            }, 1000);
        }

        function POPrint(index) {
            __doPostBack("PrintPO", index);
            return false;
        }

        function ViewAttach(index) {
            __doPostBack("ViewIndentAttach", index);
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
                                    <h3 class="page-title-head d-inline-block">Vendor DC</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Purchase</a></li>
                                <li class="active breadcrumb-item" aria-current="page">Vendor DC</li>
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
                    <%--<asp:PostBackTrigger ControlID="gvVendorDC" />--%>
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divradio" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-2">
                                        <label class="form-label" style="padding-top: 7px;">
                                            Select Type
                                        </label>
                                    </div>
                                    <div class="col-sm-8">
                                        <asp:RadioButtonList ID="rblWPONoChange" runat="server" CssClass="radio radio-success" RepeatDirection="Horizontal"
                                            AutoPostBack="true" OnSelectedIndexChanged="rblWPONoChange_OnSelectedChanged" RepeatLayout="Flow">
                                            <asp:ListItem Selected="True" Value="0">PENDING</asp:ListItem>
                                            <asp:ListItem Value="1">COMPLETED</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divAdd" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                        <label class="form-label mandatorylbl">
                                            Vendor Name
                                        </label>
                                    </div>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddlVendorName" runat="server" AutoPostBack="true" CssClass="form-control mandatoryfield"
                                            OnSelectedIndexChanged="ddlVendorName_OnSelectIndexChanged" Width="70%" ToolTip="Select Supplier Name">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                        <label class="form-label mandatorylbl">
                                            WPO No
                                        </label>
                                    </div>
                                    <div class="col-sm-8">
                                        <asp:DropDownList ID="ddlWPONo" runat="server" AutoPostBack="true" CssClass="form-control mandatoryfield"
                                            OnSelectedIndexChanged="ddlWPONo_OnSelectIndexChanged" Width="70%" ToolTip="Select WPO No">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divAddNew" class="p-t-10" runat="server">
                            <div class="ip-div text-center">
                                <asp:LinkButton ID="btnAddNew" runat="server" Text="Add New DC" CssClass="btn ic-w-btn add-btn"
                                    OnClick="btnAddNew_Click"></asp:LinkButton>
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4 text-right">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                DC Date
                                            </label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtDCDate" runat="server" CssClass="form-control mandatoryfield datepicker"
                                                ToolTip="Enter DC Date" autocomplete="nope" placeholder="Enter DC Date">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Location Name
                                            </label>
                                        </div>
                                        <div>
                                            <asp:DropDownList ID="ddlLocationName" runat="server" CssClass="form-control mandatoryfield"
                                                Width="70%" ToolTip="Select Location Name">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label">
                                                E-Way Bill No
                                            </label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtFormJJNNo" runat="server" CssClass="form-control"
                                                ToolTip="Enter E-Way Bill No" autocomplete="nope" placeholder="Enter E-Way Bill No">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 text-right">
                                        <div class="text-left">
                                            <label class="form-label">
                                                E-Way Bill Date
                                            </label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtDutyDetailsDate" runat="server" CssClass="form-control datepicker"
                                                ToolTip="Enter Duty Details date" autocomplete="nope" placeholder="Enter Duty Details date">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Duration ( Days )
                                            </label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtDuration" runat="server" CssClass="form-control mandatoryfield"
                                                Onkeypress="return fnAllowNumeric();" ToolTip="Enter Duration" autocomplete="nope"
                                                placeholder="Enter Duration">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 text-right">
                                        <div class="text-left">
                                            <label class="form-label">
                                                Tarrif Classification
                                            </label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtTarrifClassification" runat="server" CssClass="form-control"
                                                Onkeypress="return fnAllowNumeric();" ToolTip="Enter Taarif Classification" autocomplete="nope"
                                                placeholder="Enter Tarrif Classsification">
                                            </asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-20">
                                    <asp:LinkButton ID="btnSaveDC" runat="server" Text="Save" CssClass="btn btn-cons btn-save AlignTop"
                                        OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divInput');" OnClick="btnSaveDC_Click"></asp:LinkButton>
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
                                        <div class="col-sm-12 p-t-10 p-l-0 p-r-0">
                                            <asp:GridView ID="gvVendorDC" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                CssClass="table table-hover table-bordered medium" OnRowCommand="gvVendorDC_RowCommand"
                                                HeaderStyle-HorizontalAlign="Center" EmptyDataText="No Records Found" DataKeyNames="VDCID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'
                                                                Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DC No" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDCNo" runat="server" Text='<%# Eval("DCNo")%>' Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DC Date" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDCDate" runat="server" Text='<%# Eval("DCDate")%>' Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--    <asp:TemplateField HeaderText="WPO No" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblWONo" runat="server" Text='<%# Eval("Wonumber")%>' Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="E-Way Bill No" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFormJJNo" runat="server" Text='<%# Eval("FormJJNo")%>' Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tarrif Classification" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTarrifClassification" runat="server" Text='<%# Eval("TariffClassification")%>'
                                                                Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Duration" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDuration" runat="server" Text='<%# Eval("Duration")%>' Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="E-way Bill Date" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDutyDetailsDate" runat="server" Text='<%# Eval("DutyDetailsDate")%>'
                                                                Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Location" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("Location")%>' Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="RM QTY" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRawMatQty" runat="server" Text='<%# Eval("RawMatQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Value" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblValue" runat="server" Text='<%# Eval("Value")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                        HeaderText="Add DC">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnAddDC" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="AddDC"><img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# Container.DataItemIndex %>'
                                                                Style="text-align: center" CommandName="EditVendorDC">
                                                            <img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Print DC">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnPDF" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                OnClientClick='<%# string.Format("return VendorDC({0});",((GridViewRow) Container).RowIndex) %>'>  <img src="../Assets/images/pdf.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="PO Print">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnPOPrint" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                OnClientClick='<%# string.Format("return POPrint({0});",((GridViewRow) Container).RowIndex) %>'>
                                                                <img src="../Assets/images/pdf.png" />
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnVDCID" runat="server" Value="0" />

                                            <asp:HiddenField ID="hdnCodeNo" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnTINNo" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnCSTNo" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnGSTNumber" runat="server" Value="0" />

                                            <asp:HiddenField ID="hdnAddress" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnCompanyName" runat="server" Value="0" />

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
    <div class="modal" id="mpeVendorDC_p">
        <div class="modal-dialog" style="height: 100%;">
            <div class="modal-content" style="height: 100%;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always" Style="height: 100%;">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H1">
                                <div class="text-center">
                                    Documents
                                    <asp:Label ID="Label2" Text="" runat="server"></asp:Label>
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal">
                                x</button>
                        </div>
                        <div class="modal-body" style="height: 100%;">
                            <div class="inner-container" style="height: 100%;">
                                <div class="ip-div text-center" style="margin-bottom: 10px;">
                                </div>
                                <div id="divVendorDCPrint_p" runat="server">

                                    <div id="divc1" runat="server">

                                        <div class="col-sm-12">
                                            <label>
                                                Vendor DC</label>
                                        </div>
                                        <div class="col-sm-12 text-center m-t-10">
                                            <label style="font-size: 16px !important; font-weight: 700">
                                                Bellows Devision
                                            </label>
                                            <br />
                                            <label style="font-size: 16px !important; font-weight: 700">
                                                Delivery Challan
                                            </label>
                                        </div>
                                        <div class="col-sm-12 m-t-10">
                                            <label>
                                                [Challan for movement of inputs or partially processed goods under Rule 57F (4)
                                            and / or Notification No. 214/86, Dated 25.03.1986 from one factory to another factory
                                            for further processing / operation]
                                            </label>
                                        </div>
                                        <div class="col-sm-12" style="border: 1px solid #000; padding: 2px">
                                            <div class="col-sm-6" style="font-weight: 600; border: 1px solid #000">
                                                <label>
                                                    W.O.No:</label>
                                                <asp:Label ID="lblWONo_p" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-6" style="font-weight: 600; border: 1px solid #000">
                                                <label>
                                                    Form JJ No:</label>
                                                <asp:Label ID="lblFormJJno_p" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-12" style="border: 1px solid #000; padding: 2px">
                                            <div class="col-sm-6" style="font-weight: 600; border: 1px solid #000">
                                                <label>
                                                    D.C.No:</label>
                                                <asp:Label ID="lblDCno_p" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-6" style="font-weight: 600; border: 1px solid #000">
                                                <label>
                                                    Date:</label>
                                                <asp:Label ID="lblDate_p" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-12" style="font-weight: 600; border: 1px solid #000; padding: 2px">
                                            <label>
                                                To</label>
                                            <asp:Label ID="lblSuppliername_p" runat="server"></asp:Label>
                                            <div class="col-sm-12">
                                                <asp:Label ID="lblSupplierAddress_p" Style="font-weight: 400;" runat="server"></asp:Label>
                                            </div>
                                        </div>

                                    </div>

                                    <div id="divc2" runat="server">
                                        <div class="col-sm-12 p-t-10" style="overflow-x: auto;">
                                            <asp:GridView ID="gvWorkOrderPOItemDetails_p" runat="server" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartName" runat="server" Text='<%# Eval("ItemDescription")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Issued Quantity" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIssuedQty" runat="server" Text='<%# Eval("IssuedQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Value" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblValue" runat="server" Text='<%# Eval("Value")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                    <div id="divc3" runat="server">

                                        <div class="col-sm-12">
                                            Not for sale
                                        </div>
                                        <div class="col-sm-12" style="border: 1px solid #000; padding: 2px">
                                            <div class="col-sm-6 text-left" style="border: 1px solid #000">
                                                <label>
                                                    Tariff Classification</label>
                                            </div>
                                            <div class="col-sm-6" style="border: 1px solid #000">
                                                <asp:Label ID="lbltarrifClassification_p" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-12" style="border: 1px solid #000; padding: 2px">
                                            <div class="col-sm-6 text-left" style="border: 1px solid #000">
                                                <label>
                                                    Expected Duration Of Processing / Manufacturing</label>
                                            </div>
                                            <div class="col-sm-6" style="border: 1px solid #000">
                                                <asp:Label ID="lblExpectedDurationofProcessing_p" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-6">
                                                <label style="padding-top: 50px">
                                                    Received By</label>
                                            </div>
                                            <div class="col-sm-6 text-center">
                                                <div style="display: flex;justify-content: center;"> 
                                                    <p style="text-indent: 5px; font-size:11px !important;">FOR</p>
                                                <p style="text-indent: 5px; font-size:11px !important;" align="right" ID="CompanyName_P" runat="server"></p>
                                                </div>
                                                <label style="padding-top: 30px">
                                                    Signature</label>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 text-center" style="margin-top: 20px">
                                            <label style="font-weight: 700;">
                                                TO BE FILLED BY THE PROCESSING FACTORY IN ORIGINAL AND DUPLICATE CHALLAN</label>
                                        </div>
                                        <div class="col-sm-12 text-left" style="border: 1px solid #000; color: #000">
                                            <div class="col-sm-8" style="border-right: 1px solid #000; font-size: 12px">
                                                Date and time of dispatch of finished goods to parent factory / another manufacturer
                                                and Entry No. and date of receipt in the account in the processing factory or Date
                                                & time of dispatch of finished goods without payment of duty for export under bond
                                                or on payment of duty for export or on payment of duty for home consumption G.P
                                                No. and date. Quantum of duty paid (Both figures & words)
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 text-left" style="border: 1px solid #000; color: #000">
                                            <div class="col-sm-8" style="border-right: 1px solid #000; font-size: 12px">
                                                Quantity dispatch (Nos. Weight / Litre / Metre) as entered in Account
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 text-left" style="border: 1px solid #000; color: #000">
                                            <div class="col-sm-8" style="border-right: 1px solid #000; font-size: 12px">
                                                Nature of processing / Manufacturing done
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 text-left" style="border: 1px solid #000; color: #000">
                                            <div class="col-sm-8" style="border-right: 1px solid #000; font-size: 12px">
                                                Quantity of waste material returned to the parent factory or cleared for home consumption
                                                Invoice No. & Date. Quantum of duty paid (Both figure & words)
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                        <div class="col-sm-12" style="margin-top: 20px; color: #000">
                                            <div class="col-sm-6 text-left">
                                                Place:
                                            </div>
                                            <div class="col-sm-6 text-center">
                                                Signature Of Processor
                                            </div>
                                        </div>
                                        <div class="col-sm-12" style="margin-top: 50px; color: #000">
                                            <div class="col-sm-6 text-left">
                                                Date:
                                            </div>
                                            <div class="col-sm-6 text-center">
                                                Name Of Factory:
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
    </div>

    <div class="modal" id="mpeAddVendorDC" style="overflow-y: scroll;">
        <div style="max-width: 95%; padding-left: 10%; padding-top: 5%; height: fit-content;">
            <div class="modal-content">
                <asp:UpdatePanel ID="upView" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">
                                <asp:Label ID="lblWPONo_H" runat="server"></asp:Label></h4>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="docdiv" class="docdiv">
                                <div class="inner-container">
                                    <div id="Item" runat="server">
                                        <div id="divAdd_P" runat="server">
                                            <div class="ip-div text-center">
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-2">
                                                    </div>
                                                    <%-- <div class="col-sm-8 text-right">
                                                        <div class="text-left">
                                                            <label class="form-label mandatorylbl">
                                                                Work Order Supplier Name
                                                            </label>
                                                        </div>
                                                        <div>
                                                            <asp:DropDownList ID="ddlSupplierChainVendor" runat="server" AutoPostBack="true" CssClass="form-control mandatoryfield"
                                                                OnSelectedIndexChanged="ddlSupplierChainVendor_OnSelectIndexChanged" Width="70%" ToolTip="Select Supplier Vendor">
                                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>--%>
                                                    <%--  <div class="col-sm-4">
                                                       <div class="text-left">
                                                            <label class="form-label mandatorylbl">
                                                                WPO No
                                                            </label>
                                                        </div>
                                                        <div>
                                                            <asp:DropDownList ID="ddlWPONo" runat="server" AutoPostBack="true" CssClass="form-control mandatoryfield"
                                                                OnSelectedIndexChanged="ddlWPONo_OnSelectIndexChanged" Width="70%" ToolTip="Select WPO No">
                                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>--%>
                                                    <div class="col-sm-2">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="divInput_P" runat="server">
                                            <%--          <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvWorkOrderPO" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                    OnRowCommand="gvWorkOrderPO_OnRowCommand"
                                                    DataKeyNames="WPOID">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkitems" runat="server" onclick="return MandatoryField(this);"
                                                                    AutoPostBack="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="S.No" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="WPO No" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblWoNo" runat="server" Text='<%# Eval("Wonumber")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PO Qty" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPOQty" runat="server" Text='<%# Eval("POQty")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Avail Qty" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAvailPOQty" runat="server" CssClass="DCAvailQty" Text='<%# Eval("POAvailQty")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DC Qty" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtDCQty" onkeypress="return fnAllowNumeric(this);" onkeyup="return ValidateDCQty(this);" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Description" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtDescription" TextMode="MultiLine" Rows="3" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Value" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtValue" onkeypress="return fnAllowNumeric(this);" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="View PO Details">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnviewPoDetails" runat="server" CommandArgument='<%# Container.DataItemIndex %>'
                                                                    Style="text-align: center" CommandName="viewPODetails">
                                                            <img src="../Assets/images/view.png" alt="" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                            </div>--%>

                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvWorkOrderIndent" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No Records Found" OnRowCommand="gvWorkOrderIndent_OnRowCommand"
                                                    OnRowDataBound="gvWorkOrderIndent_onRowDataBound"
                                                    CssClass="table table-hover table-bordered medium" DataKeyNames="WOIHID,FileName,IndentNo">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="">
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

                                                        <asp:TemplateField HeaderText="PO QTY" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblActualQty" runat="server" Text='<%# Eval("POQty")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Avail Qty" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAvailPOQty" runat="server" CssClass="DCAvailQty" Text='<%# Eval("AvailQty")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Job DC Qty" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblJobDCQty" runat="server" CssClass="APDC" Text='<%# Eval("APDCQty")%>' Style="display: none;"></asp:Label>
                                                                <asp:TextBox ID="txtDCQty" onkeypress="return fnAllowNumeric(this);" onblur="return ValidateDCQty(this);" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Description" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtDescription" TextMode="MultiLine" Rows="3" CssClass="form-control" runat="server"></asp:TextBox>
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

                                                        <asp:TemplateField HeaderText="View Attach" HeaderStyle-CssClass="text-center"
                                                            ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <%-- <asp:LinkButton ID="" Visible="false" runat="server"
                                                                    CssClass="btn btn-cons btn-success" Text="Allow">                                                                                    
                                                                </asp:LinkButton>--%>
                                                                <asp:LinkButton ID="btnViewAttach" runat="server"
                                                                    OnClientClick='<%# string.Format("return ViewAttach({0});",((GridViewRow) Container).RowIndex) %>'> <img src="../Assets/images/view.png" alt=""/></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                            </div>

                                            <div class="col-sm-12 p-t-20">
                                                <div class="col-sm-3">
                                                    <label class="mandatorylbl">Raw Material Qty </label>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtRawMaterialQty" onkeypress="return fnAllowNumeric(this);" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-3">
                                                    <label class="mandatorylbl">Value </label>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtValue" onkeypress="return fnAllowNumeric(this);" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-sm-12 p-t-20 text-center">
                                                <asp:Button ID="btnSaveDCItem" Text="Save DC" CssClass="btn btn-cons btn-success"
                                                    OnClientClick="return ValidatePOItemDetails();"
                                                    OnClick="btnSaveDCItem_Click" runat="server" />
                                            </div>
                                        </div>

                                        <div id="divOutput_P" runat="server">
                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvVendorDCitemDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    OnRowCommand="gvVendorDCitemDetails_OnRowCommand" EmptyDataText="No Records Found" OnRowDataBound="gvVendorDCitemDetails_OnRowDataBound"
                                                    CssClass="table table-hover table-bordered medium" DataKeyNames="VDCDID">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Description" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblItemDescription" runat="server" Text='<%# Eval("ItemDescription")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Issued Quantity" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIssuedQty" runat="server" Text='<%# Eval("IssuedQty")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%# Container.DataItemIndex %>'
                                                                    CommandName="DeletePOItem">
                                                            <img src="../Assets/images/del-ec.png" alt="" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-12 p-t-20 text-center">
                                                <asp:Button ID="btnShareDC" Text="Share DC" CssClass="btn btn-cons btn-success" OnClientClick="ShareDC();"
                                                    runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>
                            <asp:HiddenField ID="hdnSPODID" runat="server" Value="0" />
                            <div class="modal-footer">
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-10">
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                            <iframe id="ifrm" runat="server" style="display: none;"></iframe>
                                        </div>
                                    </div>
                                </div>
                            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div class="modal" id="mpeWorkOrderIndentDetails" style="overflow-y: scroll;">
        <div style="max-width: 95%; padding-left: 10%; padding-top: 5%; height: fit-content;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">
                                <asp:Label ID="Label1" runat="server"></asp:Label></h4>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" onclick="return HideItemnameDetails();" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div class="inner-container">
                                <div class="ip-div text-center">
                                    <div id="divOutputsItems_WOI" runat="server">
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
