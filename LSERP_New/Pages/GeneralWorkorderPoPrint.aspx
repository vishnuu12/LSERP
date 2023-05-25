<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="GeneralWorkorderPoPrint.aspx.cs" Inherits="Pages_GeneralWorkorderPoPrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function PrintJobDC(epstyleurl, Main, style, print, topstrip, Approvedtime, POStatus, ApprovedBy) {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var Address = $('#ContentPlaceHolder1_hdnAddress').val();
            var PhoneAndFaxNo = $('#ContentPlaceHolder1_hdnPhoneAndFaxNo').val();
            var Email = $('#ContentPlaceHolder1_hdnEmail').val();
            var WebSite = $('#ContentPlaceHolder1_hdnWebsite').val();

            var MrnContent = $('#ContentPlaceHolder1_divGeneralWorkOrderPoPrint_p').html();
            var MainContent = $('#divmaincontent_p').html();
            var SpecificationTax = $('#divspecificationandtax').html();

            var RowLen = $('#ContentPlaceHolder1_gvWorkOrderPOItemDetails_p').find('tr').length;

            winprint.document.write("<html><head><title>");
            winprint.document.write("Offer");
            winprint.document.write("</title>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='../Assets/css/ep-style.css'/>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='../Assets/css/main.css' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet'  href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/printg.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");

            winprint.document.write("<style type='text/css'>  .page_generateoffer { margin:5mm; } .lbltaxothercharges { margin-left:15px; } .spntaxothercharges { margin-right:6px; } </style>");

            winprint.document.write("<div class='print-page' style='position:fixed;border:2px solid #000;width:200mm;height:287mm;margin-top:5mm;margin-bottom:5mm;margin-left:5mm;margin-right:5mm'></div>");
            winprint.document.write("<table style='width:200mm;height:287mm;margin-left:20px;border:0px !important'><thead><tr><td>");
            winprint.document.write("<div class='col-sm-12 print-generateoffer' style='border-bottom:1px solid #000;text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;height:102px'>");
            winprint.document.write("<div>");
            winprint.document.write("<div>");
            winprint.document.write("<div class='col-sm-12 padding0' id='divLSERPLogo' style='margin:20px auto;display:block;padding:5px 0px;'>");
            winprint.document.write("<div class='row' style='position:fixed;width:200mm;left:5mm;'>");
            winprint.document.write("<div class='col-sm-3'>");
            winprint.document.write("<img src=" + $('#ContentPlaceHolder1_hdnLonestarLogo').val() + " alt='lonestar-image' width='90px'>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-6 text-center'>");
            winprint.document.write("<h3 style='font-weight:600;font-size:20px;color:#000;font-family: Times New Roman;display: contents;'>" + $('#ContentPlaceHolder1_hdnCompanyName').val() + "</h3>");
            winprint.document.write("<span style='font-weight:600;font-size:11px ! important;font-family: Times New Roman;'>" + $('#ContentPlaceHolder1_hdnFormalCompanyname').val() + "</span>");

            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;width: 100%;padding:0 0px'>" + Address + "</p>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + PhoneAndFaxNo + "</p>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + Email + "</p>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + WebSite + "</p>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-3'>");
            winprint.document.write("<img src=" + $('#ContentPlaceHolder1_hdnISOLogo').val() + " alt='lonestar-image' height='90px'>");
            winprint.document.write("</div></div>");
            winprint.document.write("</div></div></div>");
            winprint.document.write("</td></tr></thead>");
            winprint.document.write("<tbody><tr><td>");
            winprint.document.write("<div class='page_generateoffer'>");
            winprint.document.write(MrnContent);
            winprint.document.write("</div>");

            winprint.document.write("</td></tr></tbody>");
            winprint.document.write("<tfoot><tr><td>");
            winprint.document.write("<div class='row' style='border-top:1px solid #000;padding-top:10px;height:30mm;'>");
            winprint.document.write("<div class='text-center' style='color:#000;padding-left:50px;font-weight:bold;'>KINDLY RETURN THE DUPLICATE COPY OF THE ORDER DULY SIGNED AS A TOKEN OFACCEPTANCE</div>");
            //style='height:100px;display:flex;align-items:flex-end;justify-content:flex-start'
            winprint.document.write("<div class='col-sm-4 p-t-85'>");
            winprint.document.write("<label style='padding-left:15px;font-weight:bold;'>PREPARED BY</label>");
            winprint.document.write("</div>");

            winprint.document.write("<div class='col-sm-4 p-t-85'>");
            winprint.document.write("<label style='padding-left:15px;font-weight:bold;'> CHECKED BY</label>");
            winprint.document.write("</div>");

            winprint.document.write("<div class='col-sm-4 text-center'>");
            winprint.document.write("<label style='display:block;width:100%;font-weight:bold;padding-top:5px;padding-left:84px;'>" + $('#ContentPlaceHolder1_hdnCompanyNameFooter').val() + "</label>");
            if (POStatus == "1") {
                if (ApprovedBy != '') {
                    winprint.document.write("<div id='divdigitalsign' class='p-t-10' style='display: block;'>");
                    winprint.document.write("<div style='text-align: end;' class='p-r-10'><label style='text-align: end;font-weight:bold;'> Digitally Signed By </label></div>");
                    winprint.document.write("<div style='text-align: end;' class='p-r-10'><label style='font-weight:bold;'> " + ApprovedBy + " </label></div>");
                    winprint.document.write("<div style='text-align: end;' class='p-r-10'><label>" + Approvedtime + "</label></div></div>");
                }
            }
            if (ApprovedBy != '')
                winprint.document.write("<label style='display:block;width:100%;padding-top:10px;font-weight:bold;padding-left:84px;'>AUTHORISED SIGNATORY</label>");
            else
                winprint.document.write("<label style='display:block;width:100%;padding-top:50px;font-weight:bold;'>AUTHORISED SIGNATORY</label>");

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
                                    <h3 class="page-title-head d-inline-block">General Workorder PO Details</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Print</a></li>
                                <li class="active breadcrumb-item" aria-current="page">GWPO</li>
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
                 <%--   <asp:PostBackTrigger ControlID="btnGeneralWorkOrderPrint" />--%>
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">

                        <div class="text-center">
                            <asp:LinkButton ID="btnWorkOrderPrint" runat="server" Text="Print" CssClass="btn btn-cons btn-success"
                                OnClick="btnGeneralWorkOrderPrint_Click"></asp:LinkButton>
                        </div>
                        <div id="divGeneralWorkOrderPoPrint_p" runat="server" style="padding: 10px; width: 200mm;">
                            <div id="divmaincontent_p">
                                <div class="text-center">
                                    <label style="font-size: 20px !important; text-decoration: underline; font-weight: 700; margin-bottom: 10px;">
                                       General Work Order</label>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <label style="display: block; font-weight: 700">
                                            To</label>
                                        <asp:Label ID="lblSupplierChainVendorName_p" runat="server" Style="margin-top: 10px; margin-left: 20px"></asp:Label>
                                        <asp:Label ID="lblReceiverAddress_p" runat="server" Style="margin-top: 10px; margin-left: 20px"></asp:Label>
                                        <div class="p-t-10">
                                            <asp:Label ID="lblGSTNumber_p" Style="margin-left: 20px;" CssClass="p-l-20" runat="server"></asp:Label>
                                        </div>
                                        <div class="p-t-5" style="margin-right: 10px; margin-top: 5px; padding: 5px; border: 1px solid;">
                                            <asp:Label ID="lblRange_p" Style="font-weight: bold;" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div id="divGSTREV0" runat="server" visible="false">
                                            <div style="display: inline-block; border: 1px solid #000; padding: 10px">
                                                <label style="font-size: 20px; font-weight: 700; text-decoration: underline;">
                                                    Consignee Address
                                                </label>
                                                <asp:Label ID="lblConsigneeAddress_p" Style="padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                <asp:Label ID="lblTNGSTNo" Style="display: block; padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                <asp:Label ID="lblCSTNo" Style="display: block; padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                <asp:Label ID="lblECCNo" Style="display: block; padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                <asp:Label ID="lblTINNo" Style="display: block; padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                <asp:Label ID="lblGSTNo" Style="padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div id="divGSTREV1" runat="server" visible="false">
                                            <div style="display: inline-block; border: 1px solid #000; padding: 10px">
                                                <label style="font-size: 20px; font-weight: 700; text-decoration: underline;">
                                                    Consignee Address
                                                </label>
                                                <asp:Label ID="lblConsigneeAddressRev1_p" Style="padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                <asp:Label ID="lblCINNo_P" Style="display: block; padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                <asp:Label ID="lblPANNo_p" Style="display: block; padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                <asp:Label ID="lblTANNo_p" Style="display: block; padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                <asp:Label ID="lblGSTIN_p" Style="display: block; padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                    </div>
                                    <div class="col-sm-6">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                    </div>
                                    <div class="col-sm-6">
                                        <label style="font-weight: bold; display: inline-block;">
                                            GWO No</label>
                                        <asp:Label ID="lblWoNo_p" runat="server" Style="font-weight: bold"></asp:Label>

                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                    </div>
                                    <div class="col-sm-6">
                                        <label style="font-weight: bold; display: inline-block;">
                                            GWO Date</label>
                                        <asp:Label ID="lblWODate_p" runat="server" Style="font-weight: bold"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                    </div>
                                    <div class="col-sm-6">
                                        QUOTE Ref No</label>
                                                                       <asp:Label ID="lblQuoteRefNo_p" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div>
                                <div class="p-t-10" style="overflow-x: auto;">
                                    <asp:GridView ID="gvGeneralWorkOrderPOItemDetails_p" runat="server" AutoGenerateColumns="False"
                                        ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                        DataKeyNames="SGWOID">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Indent No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIndentNo" runat="server" Text='<%# Eval("IndentNo")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPartName" runat="server" Text='<%# Eval("MaterialName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="QTY" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPartQuantity" runat="server" Text='<%# Eval("Quantity")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Cost" HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUnitCost" runat="server" Text='<%# Eval("UnitCost")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Cost" HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotalCost" runat="server" Text='<%# Eval("TotalCost")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                     <asp:HiddenField ID="hdnSGWOID" runat="server" Value="0" />
                        <asp:HiddenField ID="hdnAddress" runat="server" Value="" />
                        <asp:HiddenField ID="hdnPhoneAndFaxNo" runat="server" Value="" />
                        <asp:HiddenField ID="hdnEmail" runat="server" Value="" />
                        <asp:HiddenField ID="hdnWebsite" runat="server" Value="" />

                        <asp:HiddenField ID="hdnCompanyName" runat="server" Value="" />
                        <asp:HiddenField ID="hdnFormalCompanyname" runat="server" Value="" />
                        <asp:HiddenField ID="hdnCompanyNameFooter" runat="server" Value="" />
                        <asp:HiddenField ID="hdnLonestarLogo" runat="server" Value="" />
                        <asp:HiddenField ID="hdnISOLogo" runat="server" Value="" />
                                </div>
                            </div>
                            <div id="divspecificationandtax">
                                <div class="row m-t-10">
                                    <div class="col-sm-6" style="font-weight: 700">
                                        <div class="m-t-10">
                                            <label>
                                                Rupees In Words</label>
                                            <asp:Label ID="lblRupeesInwords_p" runat="server"></asp:Label>
                                        </div>
                                        <div class="m-t-10">
                                            <label>
                                                Delievery</label>
                                            <asp:Label ID="lblDeliveryDate_p" runat="server"></asp:Label>
                                        </div>
                                        <div class="m-t-10">
                                            <label>
                                                Payment</label>
                                            <asp:Label ID="lblPayment_p" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-6" style="border: 1px solid #000; padding-left: 0 !important; padding-right: 0 !important; margin-top: -10px;">
                                        <div id="divothercharges_p" class="divothercharges" runat="server">
                                        </div>
                                        <div class="m-t-10" style="width: 100%; float: left">
                                            <label style="float: left; margin-left: 15px">
                                                Sub Total INR</label>
                                            <asp:Label ID="lblSubTotal_p" runat="server" Style="float: right; margin-right: 6px"></asp:Label>
                                        </div>
                                        <div id="divtax_p" class="divtax" runat="server">
                                        </div>
                                        <div class="m-t-10 padd-lr-15 p-t-10" style="width: 100%; float: left; border: 1px solid #000">
                                            <label style="font-weight: bold;">
                                                Total Amount</label>
                                            <asp:Label ID="lblTotalAmount_p" runat="server" Style="float: right; font-weight: bold;"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="m-t-10" style="color: #000">
                                    <h6 style="text-decoration: underline; font-weight: 700">Specifictaion Enclosed</h6>
                                    <ol style="padding-left: 15px">
                                        <li>Test certificate must be submitted for approval prior to dispatch.</li>
                                        <li>Material should be Dispatched Strictly as per dispatch instruction</li>
                                        <li>Material received at store without proper documentation will not be accepted and
                                                    all waiting charges will be on supplier account</li>
                                        <li>Kindly return the Duplicate copy of the order duly signed as a token of acceptance.</li>
                                        <li>Please Mention our Workorder Number in Your Invoice.</li>
                                        <li>Offer terms will be as per our GTC.</li>
                                        <li>
                                            <asp:Label ID="lblNote_p" runat="server"></asp:Label></li>
                                    </ol>
                                </div>
                            </div>
                        </div>

                       

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

