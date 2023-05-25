<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="GeneralWorkOrderReport.aspx.cs" Inherits="Pages_GeneralWorkOrderReport" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        table#tblGeneralWorkOrderReport td {
            color: #000;
            font-weight: bold;
            font-size: smaller;
            white-space: normal;
        }

        .dataTable > thead > tr > th[class*="sort"]:before,
        .dataTable > thead > tr > th[class*="sort"]:after {
            content: "" !important;
        }

        .table th:nth-child(1) {
            width: 20px !important;
        }

        .table th:nth-child(7), th:nth-child(10) {
            width: 80px !important;
        }

        .table th:nth-child(2), th:nth-child(3), th:nth-child(4), th:nth-child(5), th:nth-child(9), th:nth-child(7), th:nth-child(6) {
            width: 50px !important;
        }

        .table th:nth-child(11), th:nth-child(12), th:nth-child(13), th:nth-child(14), th:nth-child(15), th:nth-child(16), th:nth-child(17), th:nth-child(18) {
            width: 35px !important;
        }

        .table {
            font-size: 13px;
            color: #000;
            border-collapse: collapse;
            border-spacing: 0;
            width: 100%;
            border: 1px solid #ddd;
            text-align: left;
            padding: 8px;
            white-space: nowrap;
        }


            .table tfoot input[type=text] {
                font-size: 10px;
                width: inherit;
            }

        .dataTables_scrollFoot tfoot input[type=text] {
            width: -webkit-fill-available !important;
        }

        .chosen-drop {
            width: max-content !important;
        }

        .chosen-search-input {
            color: #000;
        }

        .chosen-container a span {
            color: #000;
        }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {
            BindDate();
        });

        function BindDate() {
            $('#tblGeneralWorkOrderReport').DataTable({

                "retrieve": true,
                "processing": true,
                "serverSide": true,
                "bDeferRender": true,
                "pageLength": 100,

                "bStateSave": true,
                "fnStateSave": function (oSettings, oData) {
                    localStorage.setItem('DataTables_' + window.location.pathname, JSON.stringify(oData));
                },
                "fnStateLoad": function (oSettings) {
                    var data = localStorage.getItem('DataTables_' + window.location.pathname);
                    return JSON.parse(data);
                },
                "stateSaveParams": function (settings, data) {
                    delete data.order;
                    data.length = 100;
                    data.order = [];
                },

                "ajax": ({
                    type: "GET",
                    url: "GeneralWorkOrderReport.aspx/GetData", //It calls our web method
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    "data": function (d) {
                        return d;
                    },
                    "dataSrc": function (json) {
                        json.draw = json.d.draw;
                        json.recordsTotal = json.d.recordsTotal;
                        json.recordsFiltered = json.d.recordsFiltered;
                        json.Ldata = json.d.Ldata;
                        var return_data = json;
                        return return_data.Ldata;
                    },
                }),
                "columns": [
                    { 'data': 'GWONO' },
                    { 'data': 'ServiceDescription' },
                    { 'data': 'Quantity' },
                    { 'data': 'JobList' },
                    { 'data': 'Remarks' },
                    { 'data': 'CreatedBy' },
                    { 'data': 'IndentDate' },
                    { 'data': 'Status' },
                    { 'data': 'StatusDate' },
                    { 'data': 'IndentStatusName' },
                    { 'data': 'PONo' },
                    { 'data': 'VendorName' },
                    { 'data': 'QuateReferenceNumber' },
                    { 'data': 'DeliveryDate' },
                    { 'data': 'PaymentDays' },
                    { 'data': 'POQuantity' },
                    { 'data': 'Unitcost' },
                    {
                        'data': null,
                        "mRender": function (data, type, full) {
                            return `<a href="#" onclick="return OpenPOPrint('${data.GDCID}')";><img src= "../Assets/images/print.png"></a>`
                        }
                    },
                ],
            });
        }



        function OpenPOPrint(GDCID) {
            __doPostBack('POPrint', GDCID);
            return false;
        }

        function ShowPoPrintPopUp() {
            $('#mpePoDetails_p').modal({
                backdrop: 'static'
            });
            return false;
        }

        function PrintWorkOrderPO(Approvedtime, POStatus, ApprovedBy) {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var Address = $('#ContentPlaceHolder1_hdnAddress').val();
            var PhoneAndFaxNo = $('#ContentPlaceHolder1_hdnPhoneAndFaxNo').val();
            var Email = $('#ContentPlaceHolder1_hdnEmail').val();
            var WebSite = $('#ContentPlaceHolder1_hdnWebsite').val();

            var WPOContent = $('#ContentPlaceHolder1_divWorkOrderPoPrint_p').html();

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
            winprint.document.write("<div class='col-sm-12 print-generateoffer' style='border-bottom:1px solid #000;text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;height:110px'>");
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
            winprint.document.write(WPOContent);
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
                    winprint.document.write("<div style='text-align: end;' class='p-r-10'><label style='font-weight:bold;'> " + "UMAMAGESH G" + " </label></div>");
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
                                    <h3 class="page-title-head d-inline-block">General WorkOrder Report </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Reports</a></li>
                                <li class="active breadcrumb-item" aria-current="page">General WorkOrder Report</li>
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
            <asp:UpdatePanel ID="upQuote" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:PostBackTrigger ControlID="imgExceldownload" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" runat="server">
                            <div class="ip-div text-center">
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="col-sm-12 row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                                    <asp:Label ID="lbltitle" runat="server" Text="General WorkOrder Reports"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server">
                                                <asp:LinkButton ID="imgExceldownload" runat="server" CssClass="excel_bg" ToolTip="Excel Download"
                                                    OnClick="btnExcel_Click" />
                                            </div>
                                        </div>
                                        <div id="divGeneralWorkOrderReport">

                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <table class="table table-hover table-bordered medium"
                                                    id="tblGeneralWorkOrderReport" style="width: 100%;">
                                                    <thead>
                                                        <tr>
                                                            <th>GWONO </th>
                                                            <th>ServiceDescription </th>
                                                            <th>Qty </th>
                                                            <th>Job </th>
                                                            <th>Remarks </th>
                                                            <th>CreatedBy </th>
                                                            <th>IndentDate </th>
                                                            <th>Status </th>
                                                            <th>StatusDate </th>
                                                            <th>HOD </th>
                                                            <th>PONo </th>
                                                            <th>Vendor </th>
                                                            <th>QuateRefNo </th>
                                                            <th>DeliveryDate </th>
                                                            <th>PaymentDays </th>
                                                            <th>POQty </th>
                                                            <th>Unitcost </th>
                                                            <th>PO Print </th>
                                                        </tr>
                                                    </thead>
                                                </table>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hdnGWOId" Value="0" runat="server" />
                    <asp:HiddenField ID="hdnAddress" runat="server" Value="" />
                    <asp:HiddenField ID="hdnPhoneAndFaxNo" runat="server" Value="" />
                    <asp:HiddenField ID="hdnEmail" runat="server" Value="" />
                    <asp:HiddenField ID="hdnWebsite" runat="server" Value="" />

                    <asp:HiddenField ID="hdnCompanyName" runat="server" Value="" />
                    <asp:HiddenField ID="hdnFormalCompanyname" runat="server" Value="" />
                    <asp:HiddenField ID="hdnCompanyNameFooter" runat="server" Value="" />

                    <asp:HiddenField ID="hdnLonestarLogo" runat="server" Value="" />
                    <asp:HiddenField ID="hdnISOLogo" runat="server" Value="" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal" id="mpePoDetails_p">
        <div class="modal-dialog" style="height: 100%;">
            <div class="modal-content" style="width: 125%;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always" Style="height: 100%;">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnDownLoad" />
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
                        <div class="modal-body" style="height: 100%; overflow-x: auto;">
                            <div class="inner-container" style="height: 100%;">
                                <div class="ip-div text-center" style="margin-bottom: 10px;">
                                    <asp:LinkButton Text="Download" ID="btnDownLoad" OnClick="btndownloaddocs_Click"
                                        runat="server">   <img src="../Assets/images/pdf.png" /></asp:LinkButton>
                                </div>
                                <div id="divWorkOrderPoPrint_p" runat="server" style="padding: 10px; width: 200mm;">
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
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6">
                                            </div>
                                            <div class="col-sm-6">
                                                <label style="font-weight: bold; display: inline-block;">
                                                    QUOTE Ref No</label>
                                                <asp:Label ID="lblQuoteRefNo_p" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                        <div class="p-t-10" style="overflow-x: auto;">
                                            <asp:GridView ID="gvWorkOrderPOItemDetails_p" runat="server" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
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
                                                    <asp:Label ID="lblNote_p" runat="server"></asp:Label>
                                                </li>
                                            </ol>
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
</asp:Content>

