<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="DispatchAndInVoiceDetails.aspx.cs" Inherits="Pages_DispatchAndInVoiceDetails"
    EnableEventValidation="false" ClientIDMode="Predictable" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function ClearFields() {
            $('[type="text"]').val('');
            $('#ContentPlaceHolder1_hdnDIDID').val('0');
            $('textarea').val('');
        }

        function showdispatchpopup() {
            $('#mpeRFPDispatchDetails').modal('show');
        }

        function calculatetotalrate(ele) {
            var qty = $(ele).val();
            var unitrate = $('#ContentPlaceHolder1_hdnunitrate').val();
            var TotatlRate = parseFloat(qty * unitrate).toFixed(2);
            var AvailQty = $('#ContentPlaceHolder1_hdndispatchqty').val();

            $('#ContentPlaceHolder1_lblTotalRate').text(TotatlRate);

            if (parseInt($(ele).val()) > parseInt(AvailQty)) {
                ErrorMessage('Error', 'Dispatch Qty SHould Not Grater Then Invoice Qty');
                $(ele).val(AvailQty)
            }
        }

        function deleteInvoiceitemdetailsConfirm(RIID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Part Name will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deleteinvoiceitemrow', RIID);
            });
            return false;
        }

        function ShowTaxPopUp() {
            $('#mpeAddtax').modal('show');
            $('div').removeClass('modal-backdrop in');
            return false;
        }

        function HideAddTaxPopUp() {
            $('#mpeAddtax').modal('hide');
            $('div').removeClass('modal-backdrop in');
            return false;
        }

        function OpenTab(Name) {
            var names = ["OtherCharges", "Tax"];
            var text = document.getElementById('<%=Tax.ClientID %>');

            for (var i = 0; i < names.length; i++) {
                if (Name == names[i]) {
                    var a = text.id.replace('Tax', Name);
                    document.getElementById(a).style.display = "block";
                    document.getElementById('li' + names[i]).className = "active";
                }
                else {
                    var b = text.id.replace('Tax', names[i]);
                    document.getElementById(b).style.display = "none";
                    document.getElementById('li' + names[i]).className = "";
                }
            }
            if (Name == 'Tax') {
                $('#liOtherCharges').removeClass('active');
                $('#liOtherCharges a').removeClass('active');
                $('#liTax a').addClass('active');
                $('#liTax').addClass('active');
            }
            else {
                $('#liTax').removeClass('active');
                $('#liTax a').removeClass('active');
                $('#liOtherCharges a').addClass('active');
                $('#liOtherCharges').addClass('active');
            }
        }

        function deleteConfirmOtherCharges(SPOOCDID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Item will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deletegvRowotherCharges', SPOOCDID);
            });
            return false;
        }

        function deleteConfirmTaxDetailsCharges(SPOTDID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Item will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deletegvRowtaxCharges', SPOTDID);
            });
            return false;
        }

        function deleteConfirmInvoice(DIDID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Item will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deleteDIDID', DIDID);
            });
            return false;
        }

        function print(DIDID) {
            __doPostBack('PrintInvoice', DIDID);
        }

        //therCharges, Taxes, Currencyname
        function InvoicePrint(OtherCharges, Taxes, ) {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');
            var Content = $('#ContentPlaceHolder1_divmaincontent_d').html();
            var tc = $('#divtermsconditions_d').html();
            var FooterContent = $('#ContentPlaceHolder1_divFooterContent_p').html();
            var oc = [];
            var Tc = [];
            oc = OtherCharges.split('/');
            Tc = Taxes.split('/');
            var RowLen = $('#ContentPlaceHolder1_gvinvoiceinvoiceDescription_d').find('tr').length;

            var addressandamount = $('#divfactoryaddressandamountwords').html();

            winprint.document.write("<html><head><title>");
            ///   winprint.document.write("Offer");
            winprint.document.write("</title>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='../Assets/css/ep-style.css'/>");
            winprint.document.write("<link rel='stylesheet' href='../Assets/css/main.css' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet'  href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/printg.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/images/topstrrip.jpg") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<style type='text/css'> label{ font-weight: 900; } table{border-style: none;} .divrow1_d span,label{ padding:5px ! important } .divrow2_d span,label {padding: 5px ! important;} </style>");

            winprint.document.write("</head><body>");

            winprint.document.write("<div class='print-page' style='position:fixed;border:1px solid #000;width:200mm;height:287mm;margin-top:5mm;margin-bottom:5mm;margin-left:5mm;margin-right:5mm'></div>");
            winprint.document.write("<table>");

            winprint.document.write("<thead><tr><td>");
            winprint.document.write("<div class='col-sm-12' style='text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;height:102px;width: 200mm;margin-top: 6mm;margin-left: 5mm;margin-right: 5mm;margin-bottom: 8mm;'>");

            winprint.document.write("<div class='col-sm-12 padding0' id='divLSERPLogo' style='display:block;'>");
            // winprint.document.write("<img src='" + topstrip + "' alt='' height='100px;' style='object-fit:contain;width:100%;display:none'>");
            winprint.document.write("<div class='row print-generateoffer' style='position:fixed;width:200mm;'>");
            winprint.document.write("<div class='col-sm-3'>");
            winprint.document.write("<img src='../Assets/images/lonestar.jpeg' alt='lonestar-image' width='90px' style='margin-top:5%;'>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-6 text-center'>");
            winprint.document.write("<h3 style='font-weight:600;font-size:24px;color:#000;font-family: Times New Roman;margin-bottom: 0px ! important;'>TAX INVOICE</h3>");
            winprint.document.write("<h3 style='font-weight:600;font-size:22px;color:#000;font-family: Arial;display: contents;'>LSI-MECH ENGINEERS PVT. LTD.</h3>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;width: 100%;padding:0 0px'>" + $('#ContentPlaceHolder1_lblAddress_h').text() + "</p>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + $('#ContentPlaceHolder1_lblPhoneAndFaxNo_h').text() + "</p>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + $('#ContentPlaceHolder1_lblEmail_h').text() + "</p>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + $('#ContentPlaceHolder1_lblwebsite_h').text() + "</p>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-3'>");
            winprint.document.write("<img src='../Assets/images/iso.jpeg' alt='lonestar-image' height='90px' style='margin-top:5%;'>");
            winprint.document.write("</div>");
            winprint.document.write("</div></div>");
            winprint.document.write("</td></tr></thead>");
            winprint.document.write("<tbody><tr><td>");

            winprint.document.write("<div class='' style='width: 190mm;margin: 0mm 10mm;'>");
            winprint.document.write(Content);

            winprint.document.write("<div style='width:100%;padding-top: 10px;'>");
            winprint.document.write("<table class='table table-hover table-bordered medium' cellspacing='0' rules='all' border='1' id='ContentPlaceHolder1_gvinvoiceinvoiceDescription' style='border-collapse:collapse;'>");
            winprint.document.write("<tbody>");

            for (var i = 0; i < parseInt(RowLen); i++)
                winprint.document.write("<tr align='center'>" + $('#ContentPlaceHolder1_gvinvoiceinvoiceDescription_d').find('tr')[i].innerHTML + "</tr>");

            winprint.document.write("<tr align='center'><td colspan='2' style='text-align:left;color: #000 !important;'></td><td style='text-align:center;color: #000 !important;'> " + $('#ContentPlaceHolder1_lblinvoiceitemqty_d').text() + " </td><td style='text-align:end;color: #000 !important;width:10%;'> Sub Total </td><td style='text-align:end;color: #000 !important;'>" + $('#ContentPlaceHolder1_lblsubtotal_d').text() + "</td></tr>");

            if (OtherCharges != "") {
                $.each(oc, function (index, value) {
                    winprint.document.write("<tr align='center'><td colspan='4' style='text-align:center;color: #000 !important;font-weight: bold;padding-left: 35%;'>" + "Add : " + value.split('#')[0] + " </td><td style='text-align:end;color: #000 !important;'> " + value.split('#')[1] + " </td></tr>");
                });

                winprint.document.write("<tr align='center'><td colspan='4' style='text-align:end;color: #000 !important;'> Taxable Value: </td><td style='text-align:end;color: #000 !important;'> " + $('#ContentPlaceHolder1_lbltaxablevalue_d').text() + " </td></tr>");
            }

            if (Taxes != "") {
                $.each(Tc, function (index, value) {
                    winprint.document.write("<tr align='center'><td colspan='4' style='text-align:center;color: #000 !important;font-weight: bold;padding-left: 35%;'>" + "Add : " + value.split('#')[0] + "  </td><td style='text-align:end;color: #000 !important;'> " + value.split('#')[1] + " </td></tr>");
                });
            }

            winprint.document.write("<tr align='center'><td colspan='4' style='text-align:end;color: #000 !important;color: #000 !important;font-weight: bold;'> Total: </td><td style='text-align:end;color: #000 !important;color: #000 !important;font-weight: bold;'> " + $('#ContentPlaceHolder1_lbltotalamount_d').text() + " </td></tr>");

            winprint.document.write("</tbody>");
            winprint.document.write("</table>");

            //winprint.document.write("<table class='table table-hover table-bordered medium' cellspacing='0' rules='all' border='1' id='gvinvoiceinvoiceDescription_details' style='border-collapse:collapse;'>");
            //winprint.document.write("<tbody>");

            //if (OtherCharges != "") {
            //    $.each(oc, function (index, value) {
            //        winprint.document.write("<tr align='center'><td style='text-align:left;color: #000 !important;'>" + value.split('#')[0] + " </td><td style='text-align:end;color: #000 !important;'> " + value.split('#')[1] + " </td></tr>");
            //    });
            //}


            //winprint.document.write("<tr align='center'><td style='text-align:left;color: #000 !important;'> Sub Total: </td><td style='text-align:end;color: #000 !important;'>" + $('#ContentPlaceHolder1_lblsubtotal_d').text() + " </td></tr>");

            //if (Taxes != "") {
            //    $.each(Tc, function (index, value) {
            //        winprint.document.write("<tr align='center'><td style='text-align:left;color: #000 !important;'>" + value.split('#')[0] + "  </td><td style='text-align:end;color: #000 !important;'> " + value.split('#')[1] + " </td></tr>");
            //    });
            //}

            //winprint.document.write("</tbody>");
            //winprint.document.write("</table>");

            //winprint.document.write("<div style='text-align:end;padding-top: 10px;'>");
            //winprint.document.write("<label style='font-size: 15px ! important;'> Total Amount</label> <label style='padding-right: 20px;'> : </label><label style='color: #000 !important;font-size: 15px !important;'>" + $('#ContentPlaceHolder1_lbltotalamount_d').text() + "</label>");
            //winprint.document.write("</div>");

            winprint.document.write("<div class='div-sec p-t-10 row col-sm-12'>");
            winprint.document.write(addressandamount);
            winprint.document.write("</div>");
            winprint.document.write("</div>");

            winprint.document.write(tc);
            winprint.document.write("</div>");

            winprint.document.write("</td></tr></tbody>");

            winprint.document.write("<tfoot><tr><td>");
            winprint.document.write("<div style='height: 40mm;margin-bottom: 5mm;'>");
            winprint.document.write(FooterContent);
            winprint.document.write("</div>");
            winprint.document.write("</td></tr></tfoot>");

            winprint.document.write("</table>");

            winprint.document.write("</body></html>");

            setTimeout(function () {
                winprint.document.close();
                winprint.focus();
                winprint.print();
                // winprint.close();
            }, 1000);
        }

    </script>

    <style type="text/css">
        #ContentPlaceHolder1_divdomesticBasicInfo label {
            color: #000;
            font-weight: bold;
        }

        .Billingdetails label {
            color: #000;
            font-weight: bold;
        }

        .consigneedetails label {
            color: #000;
            font-weight: bold;
        }

        #ContentPlaceHolder1_divinterBasciinfo label {
            color: #000;
            font-weight: bold;
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
                                    <h3 class="page-title-head d-inline-block">Dispatch And Invoice</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Sales</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Dispatch And Invoice Details</li>
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
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">

                                    <div id="divinput" runat="server">

                                        <div class="col-sm-12 p-t-10">
                                            <asp:RadioButtonList ID="rblDispatchType" runat="server" CssClass="radio radio-success"
                                                AutoPostBack="true" OnSelectedIndexChanged="rblDispatchType_OnSelectindexchganged"
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Text="InterNational" Value="I">InterNational</asp:ListItem>
                                                <asp:ListItem Text="Domestic" Value="D">Domestic</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>

                                        <div class="col-sm-12 p-t-10 text-center">
                                            <label style="font-weight: bold; color: #000; font-size: 15px; color: brown;">
                                                BASIC INFO
                                            </label>
                                        </div>

                                        <div id="divdomesticBasicInfo" runat="server">
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left mandatorylbl">
                                                        <label>InVoice Type </label>
                                                    </div>
                                                    <div>
                                                        <asp:DropDownList ID="ddlInvoicetype_d" runat="server" ToolTip="Select Country" TabIndex="6"
                                                            CssClass="form-control mandatoryfield">
                                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                            <asp:ListItem Value="RFP" Text="RFP"></asp:ListItem>
                                                            <asp:ListItem Value="General" Text="General"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">InVoice Date </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtInvoicedate_d" runat="server" CssClass="form-control mandatoryfield datepicker"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">HSN No </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtHSNNo_d" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">Payment </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtPayment_d" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">Transportation Mode </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txttransportmode_d" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">Location </label>
                                                    </div>
                                                    <div>
                                                        <asp:DropDownList ID="ddlLocation_d" runat="server"
                                                            CssClass="form-control mandatoryfield">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">Transporter Name </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txttransportername_d" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">Freight </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtFreight_d" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">LR No </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtLR_d" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">vehicle No </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtvehicleNo_d" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <%--   <div class="text-left">
                                                        <label class="mandatorylbl">Time Of Supply </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txttimeOfSupply_d" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>--%>
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">Electronic Reference No </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtelectronicreferenceNo_d" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">Place Of Supply </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtplaceofsupply_d" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="">Remarks </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtRemarks_d" runat="server" Rows="3" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">Invoice No Type </label>
                                                    </div>
                                                    <div>
                                                        <asp:DropDownList ID="ddlInvoiceNoType_d" runat="server" ToolTip="Select Invoice No Type"
                                                            CssClass="form-control mandatoryfield">
                                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                            <asp:ListItem Value="I" Text="I"></asp:ListItem>
                                                            <asp:ListItem Value="L" Text="L"></asp:ListItem>
                                                            <asp:ListItem Value="EA" Text="EA"></asp:ListItem>
                                                            <asp:ListItem Value="EB" Text="EB"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">Invoice No </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtinvoiceNo_d" runat="server" onkeypress="return fnAllowNumeric();" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div id="divinterBasciinfo" runat="server">
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">InVoice Type </label>
                                                    </div>
                                                    <div>
                                                        <asp:DropDownList ID="ddlInvoicetype_I" runat="server" ToolTip="Select Country" TabIndex="6"
                                                            CssClass="form-control mandatoryfield">
                                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                            <asp:ListItem Value="RFP" Text="RFP"></asp:ListItem>
                                                            <asp:ListItem Value="General" Text="General"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">InVoice Date </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtInvoicedate_I" runat="server" CssClass="form-control mandatoryfield datepicker"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">HSN No </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtHSNNo_I" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">Payment </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtPayment_I" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">Transportation Mode </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txttransportmode_I" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">Location </label>
                                                    </div>
                                                    <div>
                                                        <asp:DropDownList ID="ddlLocation_I" runat="server"
                                                            CssClass="form-control mandatoryfield">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">Buyer Order No </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtBuyerOrderNo_I" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">Buyer Date </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtBuyerDate_I" runat="server" CssClass="form-control mandatoryfield datepicker"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">Pre Carriaged By </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtpriCarriagedBy_I" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">Place Of Receipt </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtPlaceOfReceipt_I" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">Port Loading </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtPortLoading_I" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">Port Of Discharge </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtPortOfDischarge_I" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">Terms Of Delivery </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txttermsOfDelivery_I" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">Vessel / Flight No </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtvesselFlightNo_I" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">Country Of Orgin Goods </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtcountryoforgingoods_I" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">Country Of Final Destination </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtCountryofFinaldestination_I" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">Kind Package </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtkindpackage_I" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">Currency </label>
                                                    </div>
                                                    <div>
                                                        <asp:DropDownList ID="ddlCurrency_I" runat="server" ToolTip="Select Country" TabIndex="6"
                                                            CssClass="form-control mandatoryfield">
                                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">Remarks </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtRemarks_I" runat="server" Rows="3" TextMode="MultiLine" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">Invoice No Type </label>
                                                    </div>
                                                    <div>
                                                        <asp:DropDownList ID="ddlinvoiceNotype_I" runat="server" ToolTip="Select Invoice No Type"
                                                            CssClass="form-control mandatoryfield">
                                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                            <asp:ListItem Value="I" Text="I"></asp:ListItem>
                                                            <asp:ListItem Value="L" Text="L"></asp:ListItem>
                                                            <asp:ListItem Value="EA" Text="EA"></asp:ListItem>
                                                            <asp:ListItem Value="EB" Text="EB"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">Invoice No </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtInvoiceNo_I" runat="server" onkeypress="return fnAllowNumeric();" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">Electronic Reference No </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtElectornicReferenceNo_I" runat="server" Rows="3" TextMode="MultiLine" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                </div>
                                                <div class="col-sm-4">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-12 p-t-10 text-center">
                                            <label style="font-weight: bold; color: #000; font-size: 15px; color: brown;">
                                                DETAILS OF RECEIVER (BILLING TO)
                                            </label>
                                        </div>

                                        <div class="Billingdetails">
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">Name </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtBillingName" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">Address </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtBillingAddress" Rows="3" TextMode="MultiLine" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">State </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtbillingstate" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">State Code </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtBillingstatecode" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">GSTIN No </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtBillingGSTINNo" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="">PO No </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtBillingPONo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="">PAN No </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtBillingPANNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                </div>
                                                <div class="col-sm-4">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-12 p-t-10 text-center">
                                            <label style="font-weight: bold; color: #000; font-size: 15px; color: brown;">
                                                DETAILS OF CONSIGNEE (SHIPPED TO)
                                            </label>
                                        </div>

                                        <div class="consigneedetails">
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">Name </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtConsigneename" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">Address </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtconsigneeaddress" Rows="3" TextMode="MultiLine" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">State </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtconsigneestate" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">State Code </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtconsigneestatecode" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">GSTIN No </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtconsigneeGSTINo" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="">PO No </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtconsigneepoNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="">PAN No </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtconsigneePANNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                </div>
                                                <div class="col-sm-4">
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-12 p-t-10 text-center">
                                        <asp:LinkButton ID="btnsaveInvoice" runat="server" Text="Save" CssClass="btn btn-cons btn-save AlignTop"
                                            OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divinput');" OnClick="btnsaveInvoice_Click"></asp:LinkButton>
                                        <asp:LinkButton ID="btncancelinvoice" runat="server" Text="Cancel"
                                            OnClientClick="return ClearFields();" CssClass="btn btn-cons btn-save AlignTop"></asp:LinkButton>
                                    </div>

                                    <div class="col-sm-12 p-t-10">
                                        <asp:GridView ID="gvDespatchAndInvoiceDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium uniquedatatable"
                                            HeaderStyle-HorizontalAlign="Center"
                                            OnRowCommand="gvDespatchAndInvoiceDetails_OnRowCommand" DataKeyNames="DIDID">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Invoice Entry Date" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblinvoiceDate" runat="server" Text='<%# Eval("InVoiceDate")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Invoice Created By" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblinvoicecreatedby" runat="server" Text='<%# Eval("CreatedBy")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Invoice No" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInvoiceNo" runat="server" Text='<%# Eval("InvoiceNo")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Invoice Type" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblinvoicetype" runat="server" Text='<%# Eval("InVoiceType")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="DesPatch Type" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldespatchtype" runat="server" Text='<%# Eval("DespathcType")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="view" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnview" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            CommandName="viewDispatchRFPDetails">
                                                           <img src="../Assets/images/view.png" alt="" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Add Tax" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnaddtax" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            CommandName="AddTax">
                                                           <img src="../Assets/images/add1.png" alt="" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            CommandName="EditDespatchDetails">
                                                           <img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnDelete" runat="server"
                                                            OnClientClick='<%# string.Format("return deleteConfirmInvoice({0});",Eval("DIDID")) %>'>
                                                            <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="PDF" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnPDF" runat="server"
                                                            OnClientClick='<%# string.Format("return print({0});",Eval("DIDID")) %>'>                                                                    
                                                            <img src="../Assets/images/pdf.png" alt=""/></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                    <asp:HiddenField ID="hdnDIDID" Value="0" runat="server" />
                                    <asp:HiddenField ID="hdndispatchqty" runat="server" />

                                    <asp:HiddenField ID="hdnunitrate" runat="server" />

                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div id="divdomesticinvoice_d" runat="server" style="display: none">
            <div id="divmaincontent_d" style="display: none;" runat="server">
                <div class="col-md-12 row divrow1_d">
                    <div class="col-sm-6">
                        <div class="col-sm-12">
                            <div class="col-sm-12 row">
                                <div class="col-sm-6">
                                    <label>Invoice No </label>
                                </div>
                                <div class="col-sm-6">
                                    <asp:Label ID="lblInvoiceNo_d" runat="server"> </asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12 row">
                                <div class="col-sm-6">
                                    <label>Invoice Date </label>
                                </div>
                                <div class="col-sm-6">
                                    <asp:Label ID="lblinvoicedate_d" runat="server"> </asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12 row">
                                <div class="col-sm-6">
                                    <label>HSN No </label>
                                </div>
                                <div class="col-sm-6">
                                    <asp:Label ID="lblhsnNo_d" runat="server"> </asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="col-sm-12">
                            <div class="col-sm-12 row">
                                <div class="col-sm-6">
                                    <label>Transportation Mode </label>
                                </div>
                                <div class="col-sm-6">
                                    <asp:Label ID="lbltransportationmode_d" runat="server"> </asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12 row">
                                <div class="col-sm-6">
                                    <label>Vehicle No </label>
                                </div>
                                <div class="col-sm-6">
                                    <asp:Label ID="lblvechileNo_d" runat="server"> </asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12 row">
                                <div class="col-sm-6">
                                    <label>Place Of Supply </label>
                                </div>
                                <div class="col-sm-6">
                                    <asp:Label ID="lblplaceofsupply_d" runat="server"> </asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12 row">
                                <div class="col-sm-6">
                                    <label>Transporter Name </label>
                                </div>
                                <div class="col-sm-6">
                                    <asp:Label ID="lbltransportername_d" runat="server"> </asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12 row">
                                <div class="col-sm-6">
                                    <label>Fright </label>
                                </div>
                                <div class="col-sm-6">
                                    <asp:Label ID="lblfright_d" runat="server"> </asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12 row">
                                <div class="col-sm-6">
                                    <label>payment </label>
                                </div>
                                <div class="col-sm-6">
                                    <asp:Label ID="lblpayment_d" runat="server"> </asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row p-t-10">
                    <div class="col-sm-6 text-left">
                        <label>DETAILS OF RECEIVER ( BILLED TO ) </label>
                    </div>
                    <div class="col-sm-6 text-left">
                        <label>DETAILS OF CONSIGNEE ( SHIPPED TO ) </label>
                    </div>
                </div>
                <div class="row p-t-10 divrow2_d">
                    <div class="col-sm-6">
                        <div class="col-sm-12 row">
                            <div class="col-sm-3">
                                <label>Name </label>
                            </div>
                            <div class="col-sm-9">
                                <asp:Label ID="lblbillingname_d" runat="server"> </asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 row">
                            <div class="col-sm-3">
                                <label>Address </label>
                            </div>
                            <div class="col-sm-9">
                                <asp:Label ID="lblbillingaddress_d" runat="server"> </asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-12 row">
                            <div class="col-sm-3">
                                <label>State </label>

                            </div>
                            <div class="col-sm-9">
                                <asp:Label ID="lblbillingstate_d" runat="server"> </asp:Label>
                                <label>State Code </label>
                                <asp:Label ID="lblbillingstatecode_d" runat="server"> </asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 row">
                            <div class="col-sm-3">
                                <label style="display: block;">GSTIN No </label>
                            </div>
                            <div class="col-sm-9">
                                <asp:Label ID="lblbillinggstinNo_d" runat="server"> </asp:Label>
                            </div>
                        </div>

                        <div class="col-sm-12 row">
                            <div class="col-sm-3">
                                <label style="display: block;">PO No </label>
                            </div>
                            <div class="col-sm-9">
                                <asp:Label ID="lblbillingPONo_d" runat="server"> </asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <div class="col-sm-12 row">
                            <div class="col-sm-3">
                                <label>Name </label>
                            </div>
                            <div class="col-sm-9">
                                <asp:Label ID="lblconsigneename_d" runat="server"> </asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 row">
                            <div class="col-sm-3">
                                <label>Address </label>
                            </div>
                            <div class="col-sm-9">
                                <asp:Label ID="lblconsigeeaddress_d" runat="server"> </asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 row">
                            <div class="col-sm-3">
                                <label>State </label>

                            </div>
                            <div class="col-sm-9">
                                <asp:Label ID="lblconsigneestate_d" runat="server"> </asp:Label>
                                <label>State Code </label>
                                <asp:Label ID="lblconsigneestatecode_d" runat="server"> </asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 row">
                            <div class="col-sm-3">
                                <label>GSTIN No </label>
                            </div>
                            <div class="col-sm-9">
                                <asp:Label ID="lblConsigneeGSTINNo_d" runat="server"> </asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 row">
                            <div class="col-sm-3">
                                <label>PO No </label>
                            </div>
                            <div class="col-sm-9">
                                <asp:Label ID="lblConsigneePoNo_d" runat="server"> </asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-sm-12 p-t-10" id="divinvoicedescriptiondetails_d" style="display: none;">
                <asp:GridView ID="gvinvoiceinvoiceDescription_d" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                    EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium uniquedatatable"
                    HeaderStyle-HorizontalAlign="Center">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No">
                            <ItemTemplate>
                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lbldescription" runat="server" Text='<%# Eval("Description")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Qty / Nos" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblqtyNos" runat="server" Text='<%# Eval("invoiceQty")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Unit Rate" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblunitrate" runat="server" Text='<%# Eval("UnitRate")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Amount" HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right">
                            <ItemTemplate>
                                <asp:Label ID="lblamount" runat="server" Text='<%# Eval("TotalRate")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <asp:Label ID="lblsubtotal_d" runat="server"> </asp:Label>
                <asp:Label ID="lbltaxablevalue_d" runat="server"> </asp:Label>
                <asp:Label ID="lblinvoiceitemqty_d" runat="server"> </asp:Label>
                <asp:Label ID="lbltotalamount_d" runat="server"> </asp:Label>

                <asp:Label ID="lblAddress_h" runat="server"></asp:Label>
                <asp:Label ID="lblPhoneAndFaxNo_h" runat="server"></asp:Label>
                <asp:Label ID="lblEmail_h" runat="server"></asp:Label>
                <asp:Label ID="lblwebsite_h" runat="server"></asp:Label>

            </div>

            <div class="p-t-10" id="divfactoryaddressandamountwords">

                <div class="col-sm-6">
                    <label style="display: block;">FACTORY </label>
                    <asp:Label ID="lblfactoryaddress_d" runat="server"></asp:Label>
                </div>
                <div class="col-sm-6">
                    <label style="display: block;">Rupees </label>
                    <asp:Label ID="lblamountinwords_d" Style="color: #000; font-weight: bold;" runat="server"> </asp:Label>
                </div>
            </div>

            <div id="divtermsconditions_d">
                <div class="row p-t-10" style="">
                    <div class="col-sm-6" style="border: 1px solid black; padding: 5px; color: #000; font-weight: bold;">
                        Certified that The Particular given Above Are True and Correct and the amount Indicated Represents The Price actually Charged And That There Is No Flow Of 
                Addtional Consideration directly and indirectly from the buyer
                    </div>
                    <div class="col-sm-6" style="border: 1px solid black; padding: 5px;">
                        <span style="font-weight: bold; padding-left: 5%;">Electoronic Reference No</span>
                    </div>
                </div>
            </div>

            <div id="divFooterContent_p" runat="server">
                <div style="position: fixed; width: 190mm;margin: 5mm 10mm; bottom: 0px;">
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="div-sec" style="padding-top: 10px;">
                                <label style="font-size: 12px; font-weight: 700">
                                    TERMS & CONDITIONS OF SALE  : 
                                </label>
                                <%--  <ol style="list-style-position: inside; margin-bottom: 0px;">
                                    <li>Cheque/Drafts to be Drawn In Favor Of Lone Star Industries </li>
                                    <li>Our Responsibility ceases after materials are delivered to shipping to shipping to dispatching are non delivered in transist</li>
                                    <li>Bills Not Paid Within 2 Weeks from the date of Issue Will Carry intrest @ 18% Per annum.</li>
                                    <li>SUBJECT TO CHENNAI JURISDICTIONS.</li>
                                    <li>Payment strictly by Crossed Cheque (Payees Account Only) or by RTGS to payees Account only </li>
                                    <li>payment By cash or Bearer Cheque not acceptable.</li>
                                </ol>--%>
                                <p style="font-weight: bold; color: #000 ! important;">
                                    1.Cheque/Drafts to be Drawn In Favor Of Lone Star Industries 
                                2.Our Responsibility ceases after materials are delivered to shipping to shipping to dispatching are non delivered in transist
                                3.Bills Not Paid Within 2 Weeks from the date of Issue Will Carry intrest @ 18% Per annum.
                                4.SUBJECT TO CHENNAI JURISDICTIONS.
                                5.Payment strictly by Crossed Cheque (Payees Account Only) or by RTGS to payees Account only 
                                6.payment By cash or Bearer Cheque not acceptable.
                                </p>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="p-t-5 p-r-10 text-center">
                                <label style="font-size: 12px; font-weight: 700">
                                    For LSI-MECH ENGINEERS PVT. LTD.</label>
                            </div>
                            <div class="p-t-10 p-l-10 text-center" style="padding-top: 103px;">
                                <label style="font-size: 12px; font-weight: 700;">
                                    AUTHORISED SIGNATORY</label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal" id="mpeRFPDispatchDetails" style="overflow-y: scroll;">
            <div style="max-width: 81%; padding-left: 10%; padding-top: 5%;">
                <div class="modal-content">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <Triggers>
                        </Triggers>
                        <ContentTemplate>
                            <div class="modal-header">
                                <h4 class="modal-title ADD">
                                    <asp:Label ID="lblinvoiceno_p" runat="server" Style="color: brown;"></asp:Label>
                                </h4>
                                <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                    ×</button>
                            </div>
                            <div class="modal-body" style="padding: 0px;">
                                <div id="Div1" class="docdiv">
                                    <div class="inner-container">
                                        <div id="Certificates" runat="server">
                                            <div id="divAddItems" class="divInput" runat="server">
                                                <div class="ip-div text-center">
                                                    <div class="col-sm-12 p-t-10">
                                                        <div class="col-sm-2">
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <div class="text-left">
                                                                <label class="mandatorylbl">Customer Name </label>
                                                            </div>
                                                            <div>
                                                                <asp:DropDownList ID="ddlcustomername" runat="server" ToolTip="Select " TabIndex="6" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlcustomername_OnSelectindexChanged" CssClass="form-control mandatoryfield">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <div class="text-left">
                                                                <label class="mandatorylbl">RFP No </label>
                                                            </div>
                                                            <div>
                                                                <asp:DropDownList ID="ddlRFPNo" runat="server" ToolTip="Select " TabIndex="6" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlRFPNo_OnSelectindexChanged" CssClass="form-control mandatoryfield">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-2">
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-12 p-t-10">
                                                        <div class="col-sm-2">
                                                            <asp:Label ID="lblTotalItemQty" Style="color: brown; font-weight: bold;" runat="server"> </asp:Label>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <div class="text-left">
                                                                <label class="mandatorylbl">Item Name </label>
                                                            </div>
                                                            <div>
                                                                <asp:DropDownList ID="ddlitemname" runat="server" ToolTip="Select" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlitemname_OnSelectIndexChanged" CssClass="form-control mandatoryfield">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <div class="text-left">
                                                                <label class="mandatorylbl">Invoice Qty </label>
                                                            </div>
                                                            <div>
                                                                <asp:TextBox ID="txtinvoiceqty" onkeypress="return validationNumeric(this);"
                                                                    onkeyup="return calculatetotalrate(this);" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-2">
                                                            <asp:Label ID="lblDispatchQty" Style="color: brown; font-weight: bold;" runat="server"></asp:Label>
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-12 p-t-10">
                                                        <div class="col-sm-2">
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <div class="text-left">
                                                                <label>Unit Rate </label>
                                                            </div>
                                                            <div>
                                                                <asp:Label ID="lblunitrate" Style="color: brown;" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <div class="text-left">
                                                                <label class="mandatorylbl">Description </label>
                                                            </div>
                                                            <div>
                                                                <asp:TextBox ID="txtdescription" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control mandatoryfield"></asp:TextBox>
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
                                                                <label>Total Rate </label>
                                                            </div>
                                                            <div>
                                                                <asp:Label ID="lblTotalRate" Style="color: brown;" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-4">
                                                        </div>
                                                        <div class="col-sm-2">
                                                        </div>
                                                    </div>

                                                    <div class="col-sm-12 p-t-10">
                                                        <asp:LinkButton ID="btnSaveRFPInvoiceItem" runat="server" Text="Save" CssClass="btn btn-cons btn-save AlignTop"
                                                            OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divAddItems');" OnClick="btnSaveRFPInvoiceItem_Click"></asp:LinkButton>
                                                        <asp:LinkButton ID="btncancelRFPinvoice" runat="server" Text="Cancel"
                                                            OnClientClick="return ClearFields();" CssClass="btn btn-cons btn-save AlignTop"></asp:LinkButton>
                                                    </div>

                                                </div>
                                            </div>

                                            <div id="divOutputsItems" runat="server">
                                                <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                    <asp:GridView ID="gvinvoiceItemDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                        OnRowCommand="gvinvoiceItemDetails_OnRowCommand" EmptyDataText="No Records Found"
                                                        CssClass="table table-hover table-bordered medium" DataKeyNames="RIIDID">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="RFP No">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRFPNo" runat="server" Text='<%# Eval("RFPNo")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item Name" HeaderStyle-Width="30%" ItemStyle-Width="30%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblitemname" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Invoice Qty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblinvoiceqty" runat="server" Text='<%# Eval("invoiceQty")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Description" HeaderStyle-Width="50%" ItemStyle-Width="50%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldescription" runat="server" Text='<%# Eval("Description")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnDelete" runat="server"
                                                                        OnClientClick='<%# string.Format("return deleteInvoiceitemdetailsConfirm({0});",Eval("RIIDID")) %>'>
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
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="hdnSPODID" runat="server" Value="0" />
                            <asp:HiddenField ID="hdnRIIDID" Value="0" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

        <div class="modal" id="mpeAddtax" style="overflow-y: scroll;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <Triggers>
                        </Triggers>
                        <ContentTemplate>
                            <div class="modal-header">
                                <h4 class="modal-title ADD">
                                    <asp:Label runat="server" ID="lblSpoNumber_T"></asp:Label></h4>
                                <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                    ×</button>
                            </div>
                            <div class="modal-body" style="padding: 0px;">
                                <div id="docdiv_t" class="docdiv">
                                    <div class="inner-container">
                                        <ul class="nav nav-tabs lserptabs" style="display: inline-block; width: 100%; background-color: cadetblue; text-align: right; font-size: x-large; font-weight: bold; color: whitesmoke;">
                                            <li id="liOtherCharges" class="active"><a href="#OtherCharges" class="tab-content" data-toggle="tab" onclick="OpenTab('OtherCharges');">
                                                <p style="margin-left: 10px; text-align: center; color: black;">
                                                    Other Charges
                                                </p>
                                            </a></li>
                                            <li id="liTax"><a href="#Tax" class="tab-content active"
                                                data-toggle="tab" onclick="OpenTab('Tax');">
                                                <p style="margin-left: 10px; text-align: center; color: black;">
                                                    Tax
                                                </p>
                                            </a></li>
                                        </ul>
                                        <div id="OtherCharges" runat="server" style="display: none;">
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-3">
                                                    <label class="form-label mandatorylbl">
                                                        Other Charges
                                                    </label>
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:DropDownList ID="ddlOtherCharges_t" runat="server" TabIndex="1" ToolTip="Select Item Name"
                                                        CssClass="form-control mandatoryfield">
                                                        <asp:ListItem Selected="True" Value="0" Text="-- Select --" />
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-3">
                                                    <label class="form-label mandatorylbl">
                                                        Value
                                                    </label>
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtOtherCharges_t" runat="server" TabIndex="6" placeholder="** Please Type value(EX:100)"
                                                        onkeypress="return validationDecimal(this);"
                                                        CssClass="form-control mandatoryfield"
                                                        autocomplete="nope"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-3">
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <asp:LinkButton Text="Save" CssClass="btn btn-cons btn-save  AlignTop" ID="btnSaveOtherCharges"
                                                            CommandName="othercharges" runat="server" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_OtherCharges');"
                                                            OnClick="btnSaveTaxAndOtherCharges_Click" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                </div>
                                                <div class="col-sm-3">
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvSupplierPOOtherCharges" runat="server" AutoGenerateColumns="False"
                                                    ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                    DataKeyNames="IOCDID">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("ChargesName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Value" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblValue" runat="server" Text='<%# Eval("Value")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                            HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                    OnClientClick='<%# string.Format("return deleteConfirmOtherCharges({0});",Eval("IOCDID")) %>'>   <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div id="Tax" runat="server">
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-3">
                                                    <label class="form-label mandatorylbl">
                                                        Tax
                                                    </label>
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:DropDownList ID="ddlTax_t" runat="server" TabIndex="1" ToolTip="Select Tax Name"
                                                        CssClass="form-control mandatoryfield">
                                                        <asp:ListItem Selected="True" Value="0" Text="-- Select --" />
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-3">
                                                    <label class="form-label mandatorylbl">
                                                        Value
                                                    </label>
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtTaxValue_t" runat="server" TabIndex="6" placeholder="** Please Type Percentage value(EX:4)"
                                                        onkeypress="return validationNumeric(this);"
                                                        CssClass="form-control mandatoryfield"
                                                        autocomplete="nope"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-3">
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <asp:LinkButton Text="Save" CssClass="btn btn-cons btn-save  AlignTop" ID="btnSavetaxDetails"
                                                            CommandName="tax" runat="server" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_Tax');"
                                                            OnClick="btnSaveTaxAndOtherCharges_Click" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                </div>
                                                <div class="col-sm-3">
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvTaxDetails" runat="server" AutoGenerateColumns="False"
                                                    ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                    DataKeyNames="ITDID">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("TaxName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Value" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblValue" runat="server" Text='<%# Eval("Value")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                            HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                    OnClientClick='<%# string.Format("return deleteConfirmTaxDetailsCharges({0});",Eval("ITDID")) %>'>   <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <div class="col-sm-12 text-center">
                                            <asp:Label ID="lblPOAmount_T" Style="font-size: large; font-weight: bold;" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="hdnAttachementID" runat="server" Value="0" />
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
                            <asp:HiddenField ID="hdnPoqty" runat="server" Value="0" />
                            <asp:HiddenField ID="hdnAttachementFlag" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
</asp:Content>
