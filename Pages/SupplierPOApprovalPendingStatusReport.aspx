<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="SupplierPOApprovalPendingStatusReport.aspx.cs" ClientIDMode="Predictable" 
    Inherits="Pages_SupplierPOApprovalPendingStatusReport" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function ValidateAll() {
            //var checkbox = $('#ContentPlaceHolder1_gvViewCosting').find('[type="checkbox"]').not('#ContentPlaceHolder1_gvViewCosting_chkall').length;
            var checked = $('#ContentPlaceHolder1_gvSupplierPODetails').find('input:checked').not('#ContentPlaceHolder1_gvSupplierPODetails_chkall').length;
            if (checked > 0) {
                if ($(event.target).attr('id') == 'ContentPlaceHolder1_btnReject') {
                    if ($('#ContentPlaceHolder1_txtRemarks').val() == '') {
                        ErrorMessage('Error', 'Plese Enter Remarks');
                        return false;
                    }
                    else {
                        return true;
                    }
                }
                else
                    return true;
            }
            else {
                ErrorMessage('Error', 'Please Select Item');
                return false;
            }
        }

        function POPrint(OtherCharges, Taxes) {
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

            var cnt = 1;

            if (parseInt(RowLen) > 2)
                cnt = 2;

            winprint.document.write("<html><head><title>");
            ///   winprint.document.write("Offer");
            winprint.document.write("</title>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='../Assets/css/ep-style.css'/>");
            winprint.document.write("<link rel='stylesheet' href='../Assets/css/main.css' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet'  href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/printg.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/images/topstrrip.jpg") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<style type='text/css'> label{ font-weight: 900; } table{ border: solid; } .header{ width: 205mm;left: 2mm;border: 0px solid #000; } </style>");

            winprint.document.write("</head><body>");

            for (var k = 1; k <= parseInt(cnt); k++) {
                winprint.document.write("<div class='print-page'>");
                winprint.document.write("<table>");

                winprint.document.write("<thead><tr><td>");
                winprint.document.write("<div class='col-sm-12 print-generateoffer' style='text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;height:102px'>");

                winprint.document.write("<div class='col-sm-12 padding0' id='divLSERPLogo' style='margin:5px auto;display:block;'>");
                // winprint.document.write("<img src='" + topstrip + "' alt='' height='100px;' style='object-fit:contain;width:100%;display:none'>");
                winprint.document.write("<div class='row' style='position:fixed;width:200mm;left:5mm;'>");
                winprint.document.write("<div class='col-sm-3'>");
                winprint.document.write("<img src='../Assets/images/lonestar.jpeg' alt='lonestar-image' width='90px'>");
                winprint.document.write("</div>");
                winprint.document.write("<div class='col-sm-6 text-center'>");
                winprint.document.write("<h3 style='font-weight:600;font-size:24px;font-style:italic;color:#000;font-family: Arial;display: contents;'>LONESTAR</h3>");
                winprint.document.write("<span style='font-weight:600;font-size:24px ! important;font-family: Times New Roman;'>INDUSTRIES</span>");
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

                winprint.document.write("<div class='page_generateoffer' style='width:190mm;margin:5mm'>");
                if (k == 1) {
                    winprint.document.write(MainContent);
                }
                if (k == 1) {
                    winprint.document.write("<div style='width:96%;padding-top: 10px;'>");
                    winprint.document.write("<table class='table table-hover table-bordered medium' cellspacing='0' rules='all' border='1' id='ContentPlaceHolder1_gvPurchaseOrder_P' style='border-collapse:collapse;'>");
                    winprint.document.write("<tbody>");
                    for (var i = 0; i < parseInt(RowLen); i++)
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
                }
                if (parseInt(cnt) == 1) {
                    winprint.document.write(QualityRequirtments);
                    winprint.document.write(TermsAndCondtions);
                }

                if (parseInt(cnt) == 2 && k == 2) {
                    winprint.document.write("<div class='row'> <div class='col-sm-6'> <label> Indent No </label> <label> : </label> <span>" + $('#ContentPlaceHolder1_lblIndentNo_P').text() + "</span> </div> <div class='col-sm-6' style='text-align:end;'> <label> PO Number </label> <label> : </label> <span>" + $('#ContentPlaceHolder1_lblPONumber_P').text() + "</span></div></div>");
                    winprint.document.write("<div class='row'> <div class='col-sm-6'> <label>RFP No</label><label>:</label> <span>" + $('#ContentPlaceHolder1_lblRFPNo_P').text() + "</span></div>");
                    winprint.document.write("<div class='col-sm-6' style='text-align:end;'> <label> PO Date </label> <label> : </label> <span>" + $('#ContentPlaceHolder1_lblPOSharedDate_P').text() + "</span> </div></div>");
                    winprint.document.write(QualityRequirtments);
                    winprint.document.write(TermsAndCondtions);
                }
                winprint.document.write("</div>");

                winprint.document.write("</td></tr></tbody>");

                winprint.document.write("<tfoot><tr><td>");
                winprint.document.write("<div style='height:30mm;'>");
                winprint.document.write(FooterContent);
                winprint.document.write("</div>");
                winprint.document.write("</td></tr></tfoot>");

                winprint.document.write("</table>");
                winprint.document.write("</div>");
            }
            winprint.document.write("</body></html>");

            setTimeout(function () {
                winprint.document.close();
                winprint.focus();
                winprint.print();
                // winprint.close();
            }, 1000);
        }

        function PrintPurchaseOrder(index) {
            __doPostBack("PrintPO", index);
            return false;
        }

        function PrintAMDPurchaseOrder(index) {
            __doPostBack("PrintAMDPO", index);
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
                                        <img src="../Assets/images/pages.png" alt="" width="16px" />
                                    </span>
                                    <h3 class="page-title-head d-inline-block">SPO Approval Pending Status Report</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Purchase</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">PO Approval</li>
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
                    <asp:PostBackTrigger ControlID="gvSupplierPODetails" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" class="btnAddNew" runat="server">
                        </div>
                        <div id="divInput" runat="server">
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="col-md-12 row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                                    <asp:Label ID="lbltitle" runat="server"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvSupplierPODetails" runat="server" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found"
                                                OnRowDataBound="gvSupplierPODetails_OnRowDatabound" CssClass="table table-hover table-bordered medium" OnRowCommand="gvSupplierPODetails_OnRowCommand"
                                                DataKeyNames="SPOID,PoRevision,SPONumber">
                                                <Columns>
                                                  
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SPO Number" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSPONumber" runat="server" Text='<%# Eval("SPONumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Issue Date" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIssueDate" runat="server" Text='<%# Eval("IssueDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Delivery Date" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDeliveryDate" runat="server" Text='<%# Eval("DeliveryDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Quate Reference Number">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQuateReferenceNumber" runat="server" Text='<%# Eval("QuateReferenceNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="L2 Status" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblL2Status" runat="server" Text='<%# Eval("L2Status")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="L2 Approved By" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="level2ApprovedBy" runat="server" Text='<%# Eval("Level2ApprovedBy")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="L2 Approved On" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="level2ApprovedOn" runat="server" Text='<%# Eval("POLevel2ApprovedDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="PDF">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnPDF" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                OnClientClick='<%# string.Format("return PrintPurchaseOrder({0});",((GridViewRow) Container).RowIndex) %>'>
                                                                <img src="../Assets/images/pdf.png" />                                                              
                                                            </asp:LinkButton>
                                                            <asp:Label ID="lblamdpdf" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="OLD PDF" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnoldPDF" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                OnClientClick='<%# string.Format("return PrintAMDPurchaseOrder({0});",((GridViewRow) Container).RowIndex) %>'> 
                                                                <img src="../Assets/images/pdf.png" />
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnSPOID" runat="server" Value="0" />
                                        </div>
                                        <div class="col-sm-12 text-center p-t-10">
                                            <div class="col-sm-4">
                                                <label>Remarks</label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control mandatoryfield"
                                                    TextMode="MultiLine" Rows="3" autocomplete="nope" placeholder="Enter Remarks">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 text-center p-t-10">
                                            <asp:LinkButton ID="btnApprove" Text="Approve" CssClass="btn btn-cons btn-success" runat="server" OnClientClick="return ValidateAll();"
                                                OnClick="btnApprovalReject_Click" CommandName="Approve"></asp:LinkButton>
                                            <asp:LinkButton ID="btnReject" CssClass="btn btn-cons btn-success" Text="Reject" runat="server" OnClientClick="return ValidateAll();"
                                                OnClick="btnApprovalReject_Click" CommandName="Reject"></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div id="divPurchaseOrder_PDF" class="p-page" runat="server" style="display: none;">
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

                                                                <h3 style='font-weight: 600; font-size: 24px; font-style: italic; color: #000; font-family: Arial; display: contents;'>LONESTAR </h3>

                                                                <span style='font-weight: 600; font-size: 24px ! important; font-family: Times New Roman;'>INDUSTRIES</span>
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

                                                        <div class="row">
                                                            <label style="width: 25%;">
                                                                AMD
                                                            </label>
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
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                        <div id="divPurchaseorderDescription_p">
                                            <div class="row div-sec p-t-10" style="width: 96%;">
                                                <asp:GridView ID="gvPurchaseOrder_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
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

                                                <div class="p-t-10" style="width: 96%;">
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
                                                            <asp:Label ID="lblRemarks_P" Style="display: contents; font-weight: bold;" runat="server"></asp:Label></li>
                                                    </ol>
                                                </div>
                                            </div>
                                        </div>

                                    </td>
                                </tr>
                            </tbody>
                            <tfoot>
                                <tr>
                                    <td>
                                        <div id="divFooterContent_p" runat="server">
                                            <div style="margin-bottom: 4%; position: fixed; width: 200mm; bottom: 0px;">
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
                                                <div class="p-t-15 p-r-10" style="text-align: end;">
                                                    <label style="font-size: 12px; font-weight: 700">
                                                        For LONESTAR INDUSTRIES</label>
                                                </div>
                                                <div class="p-t-10 p-l-10">
                                                    <label style="width: 38%;">PREPARED BY </label>

                                                    <label style="width: 30%;">CHECKED BY </label>


                                                    <label style="padding-top: 30px; font-size: 12px; font-weight: 700; width: 30%; text-align: end;">
                                                        AUTHORISED SIGNATORY</label>
                                                </div>

                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            </tfoot>
                        </table>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>



</asp:Content>


