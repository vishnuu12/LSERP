<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="SupplierPOPrint.aspx.cs"
    Inherits="Pages_SupplierPOPrint" ClientIDMode="Predictable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function POPrint(OtherCharges, Taxes, Currencyname) {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');
            var Content = $('#ContentPlaceHolder1_divPurchaseOrder_PDF').html();

            var FooterContent = $('#ContentPlaceHolder1_divFooterContent_p').html();

            var MainContent = $('#divMainContent_p').html();
            var QualityRequirtments = $('#divQualityRequirtments').html();
            var TermsAndCondtions = $('#divtermsandcondtions').html();

            var RowLen = $('#ContentPlaceHolder1_gvPurchaseOrder_P').find('tr').length;

            var PurchaseOrderDiscription = $('#divPurchaseorderDescription_p').html();
            var taxdetails = $('#divtaxdetails_p').html();

            var oc = [];
            var Tc = [];

            oc = OtherCharges.split('/');
            Tc = Taxes.split('/');

            winprint.document.write("<html><head><title>");
            winprint.document.write("</title>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='../Assets/css/ep-style.css'/>");
            winprint.document.write("<link rel='stylesheet' href='../Assets/css/main.css' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet'  href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/printg.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/images/topstrrip.jpg") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<style type='text/css'> #ContentPlaceHolder1_gvPurchaseOrder_P_0 th { background-color: white ! important;color: #000;font-weight: 600; } label{ font-weight: 900; } table{ border: solid; } .header{ width: 205mm;left: 2mm;border: 0px solid #000; } </style>");

            winprint.document.write("</head><body>");

            winprint.document.write("<div class='print-page' style='position:fixed;border:1px solid #000;width:200mm;height:287mm;margin-top:5mm;margin-bottom:5mm;margin-left:5mm;margin-right:5mm'></div>");
            winprint.document.write("<table style='border:none;'>");

            winprint.document.write("<thead><tr><td>");
            winprint.document.write("<div class='col-sm-12 print-generateoffer' style='text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;height:28mm;width: 200mm;margin-top: 6mm;margin-left: 5mm;margin-right: 5mm;margin-bottom: 5mm;float: left;'>");

            winprint.document.write("<div class='col-sm-12 padding0' id='divLSERPLogo' style=''>");
            winprint.document.write("<div class='row' style='position:fixed;width:200mm;'>");
            winprint.document.write("<div class='col-sm-3'>");
            winprint.document.write("<img src='../Assets/images/lonestar.jpeg' alt='lonestar-image' width='90px'>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-6 text-center'>");
            winprint.document.write("<h3 style='font-weight:600;font-size:20px;color:#000;font-family: Times New Roman;display: contents;'>" + $('#ContentPlaceHolder1_lblCompanyName_h').text() + "</h3>");
            winprint.document.write("<span style='font-weight:600;font-size:11px ! important;font-family: Times New Roman;'>" + $('#ContentPlaceHolder1_lblFormarcompanyName_h').text() + "</span>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;width: 100%;padding:0 0px'>" + $('#ContentPlaceHolder1_lblAddress_h').text() + "</p>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + $('#ContentPlaceHolder1_lblPhoneAndFaxNo_h').text() + "</p>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + $('#ContentPlaceHolder1_lblEmail_h').text() + "</p>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + $('#ContentPlaceHolder1_lblwebsite_h').text() + "</p>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-3'>");
            winprint.document.write("<img src='../Assets/images/iso.jpeg' alt='lonestar-image' height='90px'>");

            winprint.document.write("</div>");
            winprint.document.write("</div></div>");
            winprint.document.write("</td></tr></thead>");

            winprint.document.write("<tbody><tr><td>");

            winprint.document.write("<div class='' style='width: 190mm;margin: 0mm 10mm;'>");

            winprint.document.write(MainContent);

            winprint.document.write("<div style='width:100%;padding-top: 10px;'>");
            winprint.document.write("<table class='table table-hover table-bordered medium' cellspacing='0' rules='all' border='1' id='ContentPlaceHolder1_gvPurchaseOrder_P_" + 0 + "' style='border-collapse:collapse;'>");
            winprint.document.write("<thead>");
            winprint.document.write($('#ContentPlaceHolder1_gvPurchaseOrder_P').find('tr:first').html());
            winprint.document.write("</thead>");

            winprint.document.write("<tbody>");

            for (var i = 1; i < parseInt(RowLen); i++)
                winprint.document.write("<tr align='center'>" + $('#ContentPlaceHolder1_gvPurchaseOrder_P').find('tr')[i].innerHTML + "</tr>");

            if (OtherCharges != "") {
                $.each(oc, function (index, value) {
                    winprint.document.write("<tr align='center'><td colspan='7' style='text-align:left;color: #000 !important;'>" + value.split('#')[0] + " </td><td style='text-align:end;color: #000 !important;'> " + value.split('#')[1] + " </td></tr>");
                });
            }

            winprint.document.write("<tr align='center'><td colspan='7' style='text-align:left;color: #000 !important;'> Sub Total: </td><td style='text-align:end;color: #000 !important;'> " + $('#ContentPlaceHolder1_lblSubTotal_P').text() + " </td></tr>");

            if (Taxes != "") {
                $.each(Tc, function (index, value) {
                    winprint.document.write("<tr align='center'><td colspan='7' style='text-align:left;color: #000 !important;'>" + value.split('#')[0] + "  </td><td style='text-align:end;color: #000 !important;'> " + value.split('#')[1] + " </td></tr>");
                });
            }

            winprint.document.write("</tbody>");
            winprint.document.write("</table>");

            winprint.document.write("<div style='text-align:end;padding-top: 10px;'>");
            winprint.document.write("<label style='font-size: 15px ! important;'> Total Amount</label> <label style='padding-right: 20px;'> : </label><label style='color: #000 !important;font-size: 15px !important;'>" + $('#ContentPlaceHolder1_lblTotalAmount_P').text() + "</label>");
            winprint.document.write("</div>");

            winprint.document.write("<div class='div-sec p-t-10'>");
            winprint.document.write("<label style='width:15%;'>Rupees in words:</label>" + "<sapn>" + $('#ContentPlaceHolder1_lblAmonutInWords_P').text() + "</span>");
            winprint.document.write("</div>");
            winprint.document.write("</div>");

            winprint.document.write(QualityRequirtments);

            if (Currencyname == "INR")
                winprint.document.write(TermsAndCondtions);

            winprint.document.write("</div>");

            winprint.document.write("</td></tr></tbody>");

            winprint.document.write("<tfoot><tr><td>");
            winprint.document.write("<div style='height:35mm;'>");
            winprint.document.write(FooterContent);
            winprint.document.write("</div>");
            winprint.document.write("</td></tr></tfoot>");

            winprint.document.write("</table>");
            winprint.document.write("</div>");

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
        .app-container {
            display: none;
        }

        .app-header {
            display: none;
        }

        .main-container_close {
            width: 98% !important;
            margin-left: 2% !important;
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
                                    <h3 class="page-title-head d-inline-block">Supplier PO Print</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Print</a></li>
                                <li class="active breadcrumb-item" aria-current="page">SPO</li>
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
                    <asp:PostBackTrigger ControlID="btnSPOPrint" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">

                        <div class="text-center">
                            <asp:LinkButton ID="btnSPOPrint" runat="server" Text="Print" CssClass="btn btn-cons btn-success"
                                OnClick="btnSPOPrint_Click"></asp:LinkButton>
                        </div>

                        <div id="divPurchaseOrder_PDF" class="p-page" runat="server" style="display: block;">
                            <table>
                                <thead>
                                    <tr>
                                        <td>
                                            <div class='row header-space' style='text-align: center; font-size: 20px; font-weight: bold; padding-left: 0; padding-right: 0; height: 120px'>
                                                <div class='header' style='border-bottom: 1px solid;'>
                                                    <div>
                                                        <div class='row padding0' id='divLSERPLogo' style='margin: 0px auto; display: block; padding: 5px 0px;'>
                                                            <img src='" + topstrip + "' alt='' height='100px;' style='object-fit: contain; width: 100%; display: none'>
                                                            <div class='row'>

                                                                <div class='col-sm-3'>

                                                                    <img src='../Assets/images/lonestar.jpeg' alt='lonestar-image' width='90px'>
                                                                </div>

                                                                <div class='col-sm-6 text-center'>
                                                                    <h3 style='font-weight: 600; font-size: 24px; font-style: italic; color: #000; font-family: Arial; display: contents;'>
                                                                        <asp:Label ID="lblCompanyName_h" runat="server"></asp:Label>
                                                                    </h3>

                                                                    <span style='font-weight: 600; font-size: 24px ! important; font-family: Times New Roman;'>
                                                                        <asp:Label ID="lblFormarcompanyName_h" runat="server"></asp:Label>
                                                                    </span>

                                                                    <p style='font-weight: 500; color: #000; width: 103%;'>
                                                                        <asp:Label ID="lblAddress_h" runat="server"></asp:Label>
                                                                    </p>

                                                                    <p style='font-weight: 500; color: #000'>
                                                                        <asp:Label ID="lblPhoneAndFaxNo_h" runat="server"></asp:Label>
                                                                    </p>

                                                                    <p style='font-weight: 500; color: #000'>
                                                                        <asp:Label ID="lblEmail_h" runat="server"></asp:Label>
                                                                    </p>

                                                                    <p style='font-weight: 500; color: #000'>
                                                                        <asp:Label ID="lblwebsite_h" runat="server"></asp:Label>
                                                                    </p>
                                                                </div>
                                                                <div class='col-sm-3'>

                                                                    <img src='../Assets/images/iso.jpeg' alt='lonestar-image' height='90px'>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <div id="divMainContent_p">
                                                <div>
                                                    <div class="text-center div-sec" style="height: 31px; padding-top: 4px; border: 3px solid; border-radius: 10px; margin-left: 39%; margin-right: 39%;">
                                                        <label style="font-weight: 700; font-size: 15px ! important;">
                                                            PURCHASE ORDER</label>
                                                    </div>
                                                    <div class="row div-sec" style="padding-right: 15px; padding-top: 12px;">
                                                        <div class="col-sm-6" style="padding-top: 10px;">
                                                            <label style="font-size: 12px; font-weight: 700">
                                                                To:</label>
                                                            <div class="row">
                                                                <asp:Label ID="lblSupplierName_P" Style="font-weight: bold;" runat="server"></asp:Label>
                                                                <asp:Label ID="lblsupplierAddress_P" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="row">
                                                                <label style="width: 15%;">
                                                                    PH</label>
                                                                <label style="width: 3%;">
                                                                    :</label>
                                                                <asp:Label ID="lblPhoneNumber_P" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="row">
                                                                <label style="width: 15%;">
                                                                    Email</label>
                                                                <label style="width: 3%;">
                                                                    :</label>
                                                                <asp:Label ID="lblEmail_P" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="row" style="padding-top: 10px;">
                                                                <label style="width: 15%;">
                                                                    GST No</label>
                                                                <label style="width: 3%;">
                                                                    :</label>
                                                                <asp:Label ID="lblSupGSTNumber_P" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6" style="padding-left: 8%;">
                                                            <div class="row">
                                                                <label style="width: 25%;">
                                                                    PO No</label>
                                                                <label style="width: 3%;">
                                                                    :</label>
                                                                <asp:Label ID="lblPONumber_P" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="row">
                                                                <label style="width: 25%;">
                                                                    PO Date</label>
                                                                <label style="width: 3%;">
                                                                    :</label>
                                                                <asp:Label ID="lblPOSharedDate_P" runat="server"></asp:Label>
                                                            </div>

                                                            <div>
                                                                <label style="width: 25%;">
                                                                    AMD</label>
                                                                <label style="width: 3%;">
                                                                    :</label>
                                                                <asp:Label ID="lblAmdno_p" runat="server"></asp:Label>
                                                            </div>

                                                            <div class="row">
                                                                <label style="width: 25%;">
                                                                    Quote Ref No</label>
                                                                <label style="width: 3%;">
                                                                    :</label>
                                                                <asp:Label ID="lblQuoteReferenceNumber_P" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="row p-t-10" style="border: solid black; margin-top: 10px; text-align: center; padding-bottom: 10px; padding-left: 5px;">
                                                                <label style="font-size: 12px; font-weight: 700">
                                                                    Consignee Address</label>
                                                                <asp:Label ID="lblConsigneeAddress_P" Style="padding-top: 5px !important; text-align: left;" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row div-sec p-t-10">
                                                        <div class="col-sm-6">
                                                            <div class="row" style="border: solid black; padding-left: 22px; padding: 5px;">
                                                                <%-- <p class="p-t-5">
                                                                    Perungudi IV Range: The Supdt. Of Central Excise
                                                                </p>
                                                                <p class="p-t-5">
                                                                    Perungudi Range, Perungudi Division, New No. 690, Anna Salai
                                                                </p>
                                                                <p class="p-t-5">
                                                                    Nandanam, Chennai - 600035.
                                                                </p>--%>
                                                                <asp:Label ID="lblRange_P" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6 p-l-10" style="padding-left: 7%;">
                                                            <div id="divGSTREV0" runat="server" visible="false">
                                                                <div class="p-t-10">
                                                                    <label style="width: 25%;">
                                                                        TNGST No</label>
                                                                    <label style="width: 3%;">
                                                                        :</label>
                                                                    <asp:Label ID="lblTNGSTNumber_P" runat="server"></asp:Label>
                                                                </div>
                                                                <div>
                                                                    <label style="width: 25%;">
                                                                        CST No</label>
                                                                    <label style="width: 3%;">
                                                                        :</label>
                                                                    <asp:Label ID="lblCSTNumber_P" runat="server"></asp:Label>
                                                                </div>
                                                                <div>
                                                                    <label style="width: 25%;">
                                                                        ECC No</label>
                                                                    <label style="width: 3%;">
                                                                        :</label>
                                                                    <asp:Label ID="lblECCNo_P" runat="server"></asp:Label>
                                                                </div>
                                                                <div>
                                                                    <label style="width: 25%;">
                                                                        TIN No</label>
                                                                    <label style="width: 3%;">
                                                                        :</label>
                                                                    <asp:Label ID="lblTINNumber_P" runat="server"></asp:Label>
                                                                </div>
                                                                <div>
                                                                    <label style="width: 25%;">
                                                                        GST No</label>
                                                                    <label style="width: 3%;">
                                                                        :</label>
                                                                    <asp:Label ID="lblLonestatrGSTNo" runat="server"></asp:Label>
                                                                </div>
                                                            </div>

                                                            <div id="divGSTREV1" runat="server" visible="false">
                                                                <div class="p-t-10">
                                                                    <label style="width: 25%;">
                                                                        CIN
                                                                    </label>
                                                                    <label style="width: 3%;">
                                                                        :</label>
                                                                    <asp:Label ID="lblCINNo_P" runat="server"></asp:Label>
                                                                </div>
                                                                <div>
                                                                    <label style="width: 25%;">
                                                                        PAN 
                                                                    </label>
                                                                    <label style="width: 3%;">
                                                                        :</label>
                                                                    <asp:Label ID="lblPANNo_p" runat="server"></asp:Label>
                                                                </div>
                                                                <div>
                                                                    <label style="width: 25%;">
                                                                        TAN
                                                                    </label>
                                                                    <label style="width: 3%;">
                                                                        :</label>
                                                                    <asp:Label ID="lblTANNo_p" runat="server"></asp:Label>
                                                                </div>
                                                                <div>
                                                                    <label style="width: 25%;">
                                                                        GSTIN
                                                                    </label>
                                                                    <label style="width: 3%;">
                                                                        :</label>
                                                                    <asp:Label ID="lblGSTIN_p" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row p-t-10 div-sec">
                                                        <div class="col-sm-4">
                                                            <label style="font-size: 12px; font-weight: 700">
                                                                Indent No</label>
                                                            <asp:Label ID="lblIndentNo_P" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <label style="font-size: 12px; font-weight: 700">
                                                                RFP No</label>
                                                            <asp:Label ID="lblRFPNo_P" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <label style="font-size: 12px; font-weight: 700">
                                                                Amd Reason
                                                            </label>
                                                            <asp:Label ID="lblamdreason_p" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="divPurchaseorderDescription_p">
                                                <div class="row div-sec p-t-10" style="width: 100%;">
                                                    <asp:GridView ID="gvPurchaseOrder_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                        OnRowDataBound="gvPurchaseOrder_P_OnRowDataBound" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                                HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Grade">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGrade" runat="server" Text='<%# Eval("GradeName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Material Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMatType" runat="server" Text='<%# Eval("TypeName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Measurment">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMeasurement" runat="server" Text='<%# Eval("Measurment")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="QTY" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQTY" runat="server" Text='<%# Eval("ReqWeight")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="UOM" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Unit Price" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUnitPrice" runat="server" Text='<%# Eval("UnitCost")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Amount" HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTotalAmount" runat="server" Text='<%# Eval("TotalCost")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div id="divtaxdetails_p">
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        &nbsp;
                                                    </div>
                                                    <div class="col-sm-6 sub-table" runat="server" id="divCharges_P">

                                                        <div id="divothercharges_p" runat="server">
                                                        </div>
                                                        <div class="row">
                                                            <label class="col-sm-9" style="border: 1px solid #000;">
                                                                SUB Total</label>
                                                            <asp:Label ID="lblSubTotal_P" CssClass="col-sm-3" Style="border: 1px solid #000; text-align: end;"
                                                                runat="server"></asp:Label>
                                                        </div>
                                                        <div id="divtax_p" runat="server">
                                                        </div>

                                                        <div class="row">
                                                            <label class="col-sm-9" style="border: 1px solid #000;">
                                                                Total Amount</label>
                                                            <asp:Label ID="lblTotalAmount_P" CssClass="col-sm-3" Style="border: 1px solid #000; text-align: end;"
                                                                runat="server"></asp:Label>
                                                        </div>
                                                        <div class="div-sec p-t-10">
                                                            <label style="width: 15%;">
                                                                Rupees in words:</label>
                                                            <asp:Label ID="lblAmonutInWords_P" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="divbottomcontent_p">
                                                <div id="divQualityRequirtments">
                                                    <div class="p-t-10 text-center" style="width: 100%;">
                                                        <label>Quality Requirtments</label>
                                                    </div>

                                                    <div class="p-t-10" style="width: 100%;">
                                                        <asp:GridView ID="gvQualityRequirtments_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Certificate Name" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCertificate" runat="server" Text='<%# Eval("Certificate")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                    <div class="div-sec">
                                                        <div class="p-t-5">
                                                            <label>
                                                                Drawing Ref:</label>
                                                            <asp:Label ID="lblDrawingRef_P" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="p-t-5">
                                                            <label>
                                                                Delivery:</label>
                                                            <asp:Label ID="lblDelivery_P" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="p-t-5">
                                                            <label>
                                                                Payment Terms:</label>
                                                            <asp:Label ID="lblPaymentTerms_P" runat="server"></asp:Label>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div id="divtermsandcondtions">
                                                    <div class="div-sec" style="padding-top: 10px;">
                                                        <label style="font-size: 12px; font-weight: 700">
                                                            Terms & Conditions</label>
                                                        <ol style="list-style-position: inside; margin-bottom: 0px;">
                                                            <li>Test certificate must be submitted for approval prior to dispatch</li>
                                                            <li>Material should be Dispatched Strictly as per dispatch instruction.</li>
                                                            <li>Material received at store without proper documentation will not be accepted and
                                                            all waiting charges will be on supplier</li>
                                                            <li>Kindly return the Duplicate copy of the order duly signed as a token of acceptance.</li>
                                                            <li>Please Mention our Purchase Order Number in Your Invoice.</li>
                                                            <li>Offer terms will be as per our GTC.</li>
                                                            <li>
                                                                <asp:Label ID="lblRemarks_P" runat="server"></asp:Label></li>
                                                        </ol>
                                                    </div>
                                                </div>

                                                <div id="divforignPTC" style="display: none;">
                                                    <asp:GridView ID="gvForignPOTC_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                        EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Heading" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblheading" runat="server" Text='<%# Eval("Heading")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Details" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldetails" runat="server" Text='<%# Eval("Details")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>

                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                                <tfoot>
                                    <tr>
                                        <td>
                                            <div id="divFooterContent_p" runat="server">
                                                <div style="margin-bottom: 7mm; position: fixed; width: 200mm; bottom: 0px; left: 5mm;">
                                                    <%--   <p class="text-center" style="margin-bottom: 10px !important">
                                                      <label>KINDLY RETURN THE DUPLICATE COPY OF THE ORDER DULY SIGNED AS A TOKEN OF YOUR ACCEPTANCE</label> 
                                                    </p>--%>
                                                    <div style="text-align: center;">
                                                        <label>KINDLY RETURN THE DUPLICATE COPY OF THE ORDER DULY SIGNED AS A TOKEN OF YOUR ACCEPTANCE</label>
                                                    </div>
                                                    <%-- <div class="row">
                                                        <div class="col-sm-4 text-left p-t-45" style="padding-left: 10px; padding-right: 10px; font-size: 12px; font-weight: 700; color: #000 !important;">
                                                            PREPARED BY
                                                        </div>
                                                        <div class="col-sm-4 text-left p-t-45" style="padding-left: 10px; padding-right: 10px; font-size: 12px; font-weight: 700; color: #000 !important;">
                                                            CHECKED BY
                                                        </div>
                                                        <div class="col-sm-4 text-right" style="padding-left: 10px; padding-right: 10px; font-size: 12px">
                                                            <label class="d-block" style="font-size: 12px; font-weight: 700">
                                                                For LONESTAR INDUSTRIES</label>
                                                            <label class="d-block" style="padding-top: 30px; font-size: 12px; font-weight: 700">
                                                                AUTHORISED SIGNATORY</label>
                                                        </div>
                                                    </div>--%>
                                                    <%--   <div class="row">
                                                        <div class="col-sm-4 text-left">
                                                            PREPARED BY
                                                        </div>
                                                        <div class="col-sm-4 text-left">
                                                            CHECKED BY
                                                        </div>
                                                        <div class="col-sm-4 text-right">
                                                            <label style="font-size: 12px; font-weight: 700">
                                                                For LONESTAR INDUSTRIES</label>
                                                            <label style="padding-top: 30px; font-size: 12px; font-weight: 700">
                                                                AUTHORISED SIGNATORY</label>
                                                        </div>
                                                    </div>--%>
                                                    <div class="p-t-5 p-r-10" style="text-align: end;">
                                                        <label style="font-size: 12px; font-weight: 700">
                                                            <asp:Label ID="lblcompanynamefooter_p" runat="server"></asp:Label>
                                                        </label>
                                                    </div>

                                                    <div id="divdigitalsign" class="p-t-5" style="display: none;" runat="server">
                                                        <div style="text-align: end;" class="p-r-10">
                                                            <label style="text-align: end;">Digitally Signed By </label>
                                                        </div>
                                                        <div style="text-align: end;" class="p-r-10">
                                                            <label>G. UMA MAGESH </label>
                                                        </div>
                                                        <div style="text-align: end;" class="p-r-10">
                                                            <asp:Label ID="lblApprovedDatetime_p" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="p-t-10 p-l-10">
                                                        <label style="width: 38%;">PREPARED BY </label>

                                                        <label style="width: 30%;">CHECKED BY </label>


                                                        <label style="font-size: 12px; font-weight: 700; width: 30%; text-align: end;">
                                                            AUTHORISED SIGNATORY</label>
                                                    </div>

                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>

                        <asp:HiddenField ID="hdnSPOID" Value="0" runat="server" />

                        <asp:HiddenField ID="hdnWPOID" runat="server" Value="0" />
                        <asp:HiddenField ID="hdnAddress" runat="server" Value="" />
                        <asp:HiddenField ID="hdnPhoneAndFaxNo" runat="server" Value="" />
                        <asp:HiddenField ID="hdnEmail" runat="server" Value="" />
                        <asp:HiddenField ID="hdnWebsite" runat="server" Value="" />

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

