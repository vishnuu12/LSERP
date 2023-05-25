<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="ViewCostingSheet.aspx.cs"
    Inherits="Pages_ViewCostingSheet" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function OpenDrawingFile(imgname) {
            window.open(imgname, "_blank");
        }

        function ShowViewPopUP() {
            $('#mpeView').modal({
                backdrop: 'static'
            });
        }

        function ShowViewdocsPopUp() {
            $('#mpeViewdocs').modal({
                backdrop: 'static'
            });
        }

        function ViewFileName(RowIndex) {
            __doPostBack("ViewDrawing", RowIndex);
        }

        function ViewCostingprint(epstyleurl, Main, QrCode) {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var RowLength = $('#ContentPlaceHolder1_gvItemCostingDetails_pdf').find('tr').length - 1;

            var PageLength = parseInt(RowLength / 50);

            var CostingHeader = $('#ContentPlaceHolder1_divcostingheader').html();
            var divcostbottomcontent = $('#ContentPlaceHolder1_divcostbottomcontent').html();

            var k = 1;
            var lastrecord = false;
            winprint.document.write("<html><head><title>");
            winprint.document.write("Offer");
            winprint.document.write("</title>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='" + epstyleurl + "'/>");
            winprint.document.write("<link rel='stylesheet' href='" + Main + "' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet'  href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/printg.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<style type='text/css'>@media print,screen { label,table th{ font-weight: bold; font-size: 15px !important;font-family:Times New Roman;color:#000 !important; } @page {size: landscape} } .row{ padding-top:10px; } .page_generateoffer{ margin: 6mm; } table th{ vertical-align: middle; } .page_generateoffer table td {font-size: 8px; font-weight: bold;color: #000;} .table th,.table td {  padding: 2px !important; } </style>");
            winprint.document.write("<style type='text/css'> .page_generateoffer table th { font-size: 8px ! important; font-weight: bold;color: #000 ! important;vertical-align: middle;text-align: center;  background: #fff !important;border: 1px solid #000 !important;}.page_generateoffer{margin-top: 0px !important} </style>");

            //winprint.document.write("<div class='print-page' style='position:fixed;width:200mm;height:287mm;margin-top:5mm;margin-bottom:5mm;margin-left:5mm;margin-right:5mm'></div>");
            winprint.document.write("<table style='width:287mm;height:200mm;margin-left:20px;border:0px !important'>");
            winprint.document.write("<thead><tr><td style='height: 90px;'>");

            winprint.document.write(CostingHeader);

            winprint.document.write("</td></tr></thead>");
            winprint.document.write("<tbody><tr><td>");

            //class='page_generateoffer'

            //   for (var i = 0; i <= PageLength; i++) {
            winprint.document.write("<div class='page_generateoffer'>");
            winprint.document.write("<div style='padding-right: 15px !important; padding-left: 0px !important'>")
            winprint.document.write("<table class='table table-hover medium' cellspacing='0' rules='all' border='1' id='ContentPlaceHolder1_gvItemCostingDetails_pdf' style='border-collapse:collapse;table-layout: fixed;width: 287mm;'>");
            winprint.document.write("<thead>");
            winprint.document.write($('#ContentPlaceHolder1_gvItemCostingDetails_pdf').find('tr:first').html());
            winprint.document.write("</thead>");
            winprint.document.write("<tbody>");

            for (var j = k; j < RowLength; j++) {
                //if (RowLength == j) {
                //winprint.document.write("<tr align='center' style='border:white;'>" + $('#ContentPlaceHolder1_gvItemCostingDetails_pdf').find('tr')[j].innerHTML + "</tr>");
                //    lastrecord = true;
                //    break;
                //}
                //else
                    winprint.document.write("<tr align='center'>" + $('#ContentPlaceHolder1_gvItemCostingDetails_pdf').find('tr')[j].innerHTML + "</tr>");
            }

            winprint.document.write("</tbody>");
            winprint.document.write("</table>");
            winprint.document.write("</div>");

            //if (lastrecord) {
            winprint.document.write(divcostbottomcontent);
            // }
            winprint.document.write("</div>");

            //if (lastrecord) {
            //    break;
            //}
            //  k = k + 50;
            // }

            winprint.document.write("</td></tr></tbody>");
            winprint.document.write("<tfoot><tr><td>");
            winprint.document.write("<div style='height:25mm;'>");
            winprint.document.write("<div class='row' style='margin-bottom:20px;position:fixed;width:200mm;bottom:0px;'>");
            winprint.document.write("<img  class='Qrcode' style='height:80%;' src='" + QrCode + "' />");
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

    <style type="text/css">
        table#gvItemCostingDetails_pdf tr td {
            font-weight: bold;
            color: black;
        }

        table#gvAddtionalPartDetails_pdf tr td {
            font-weight: bold;
            color: black;
        }

        table#gvIssuePartDetails_pdf tr td {
            font-weight: bold;
            color: black;
        }


        @media print {
            .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
                padding: 5px 3px !important;
            }
        }

        #ContentPlaceHolder1_gvItemCostingDetails {
            font-size: 12px;
            font-weight: bold;
            color: #000;
        }

        #ContentPlaceHolder1_gvAddtionalPartDetails {
            font-size: 12px;
            font-weight: bold;
            color: #000;
        }

        #ContentPlaceHolder1_gvIssuePartDetails {
            font-size: 12px;
            font-weight: bold;
            color: #000;
        }

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

        table {
            font-weight: bold;
            color: #000;
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
                                    <h3 class="page-title-head d-inline-block">View Costing Sheet</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Design</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">View Costing Sheet</li>
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
                    <asp:PostBackTrigger ControlID="btndownloaddocs" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">

                        <div id="divAdd" runat="server">
                            <div class="ip-div text-center">
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div id="divViewCostingPrint_p" runat="server">
                                            <div class="col-sm-12 text-center">
                                                <asp:LinkButton ID="btndownloaddocs"
                                                    OnClick="btndownloaddocs_Click" ToolTip="PDF DownLoad" runat="server">
                                                        <img src="../Assets/images/pdf.png" /> </asp:LinkButton>
                                            </div>
                                            <div class="col-sm-12 p-t-10 text-center">
                                                <asp:Label ID="lblCustomerName_p" runat="server">                                        
                                                </asp:Label>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-3 p-t-10">
                                                    <asp:Label ID="lblItemname_p" runat="server">                                        
                                                    </asp:Label>
                                                </div>
                                                <div class="col-sm-3 p-t-10">
                                                    <label>
                                                        Drawing Name:</label>
                                            <asp:LinkButton ID="lblDrawingname_p" OnClick="btnViewDrawingFile" runat="server" >
                                            </asp:LinkButton>
                                                </div>
                                                <div class="col-sm-3 p-t-10">
                                                    <label>
                                                        Tag No:</label>
                                                    <asp:Label ID="lblTagNo_p" runat="server">                                        
                                                    </asp:Label>
                                                </div>
                                                <div class="col-sm-3 p-t-10">
                                                    <label>
                                                        Total Quantity:</label>
                                                    <asp:Label ID="lblTotalQty_p" runat="server">                                        
                                                    </asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-12" style="overflow-x: auto;">
                                                <asp:GridView ID="gvItemCostingDetails" runat="server" AutoGenerateColumns="true"
                                                    ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium orderingfalse"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <Columns>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:Label ID="lblBOMCost" Text="" runat="server" Style="color: darkgreen; font-size: x-large; font-family: fantasy;"></asp:Label>
                                            </div>
                                            <div class="col-sm-12 p-t-10 text-center">
                                                <label class="form-label" style="color: Blue;">
                                                    Free Issue Part BOM Cost Details
                                                </label>
                                            </div>
                                            <div class="col-sm-12 p-t-10" id="IssuePartiv" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvIssuePartDetails" runat="server" AutoGenerateColumns="true"
                                                    ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium orderingfalse"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <Columns>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:Label ID="lblIssuePartCost" Text="" runat="server" Style="color: darkgreen; font-size: x-large; font-family: fantasy;"></asp:Label>
                                            </div>
                                            <div class="col-sm-12 text-center">
                                                <asp:Label ID="Label2" Text="" runat="server" Style="color: brown; font-size: x-large; font-family: fantasy;"></asp:Label>
                                            </div>
                                            <div class="col-sm-12 p-t-10 text-center">
                                                <label class="form-label" style="color: Blue;">
                                                    Addtional Part BOM Cost Details
                                                </label>
                                            </div>
                                            <div class="col-sm-12 p-t-10" id="AddtionalPartiv" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvAddtionalPartDetails" runat="server" AutoGenerateColumns="true"
                                                    ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium orderingfalse"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <Columns>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:Label ID="lblAddtionalPartCost" Text="" runat="server" Style="color: darkgreen; font-size: x-large; font-family: fantasy;"></asp:Label>
                                            </div>
                                            <div class="col-sm-12 text-center">
                                                <asp:Label ID="lblTotalBOMCost" Text="" runat="server" Style="color: brown; font-size: x-large; font-family: fantasy;"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label>
                                                    Over All Length:</label>
                                                <asp:Label ID="lblOverAllLength_p" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label>
                                                    LSE Number:</label>
                                                <asp:Label ID="lblLSENumber_p" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label>
                                                    Dead Inventory Remarks:</label>
                                                <asp:Label ID="lblDeadInventoryRemarks_p" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label>
                                                    Remarks:</label>
                                                <asp:Label ID="lblRemarks_p" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label>
                                                    Date:</label>
                                                <asp:Label ID="lblDate_p" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label>
                                                    User Name:</label>
                                                <asp:Label ID="lblUserName_p" runat="server"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:Image ID="imgQrcode" class="Qrcode" ImageUrl="" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hdnBOMId" Value="0" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <div>
                <div class="col-sm-12">
                </div>
            </div>
        </div>
    </div>

    <div class="modal" id="mpeCosting_pdf">
        <div class="modal-dialog" style="max-width: 95%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H3">
                                <div class="text-center">
                                    <asp:Label ID="Label3" Text="" runat="server"></asp:Label>
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal">
                                x</button>
                        </div>
                        <div class="modal-body">
                            <div class="inner-container">
                                <div id="divViewCostingPrint_pdf" runat="server" style="float: left; width: 100%; margin-right: 10px;">
                                    <div id="divcostingheader" runat="server">
                                        <div class="row text-center" style="padding-left: 0 !important; padding-right: 0px !important">
                                        </div>
                                        <div class="row p-t-10 text-center" style="margin-left: 40%;">
                                            <asp:Label ID="lblCustomerName_pdf" runat="server" Style="font-size: 20px !important; font-weight: 700">                                        
                                            </asp:Label>
                                        </div>
                                        <div class="row p-t-10 text-center" style="display: flex; align-items: center; justify-content: space-around">
                                            <asp:Label ID="lblItemname_pdf" runat="server" Style="font-size: 14px !important; font-weight: 700 !important">                                        
                                            </asp:Label>
                                            <asp:Label ID="lblDrawingname_pdf" runat="server">                                        
                                            </asp:Label>
                                            <label>
                                                Tag No:</label>
                                            <asp:Label ID="lblTagNo_pdf" runat="server">                                        
                                            </asp:Label>
                                            <label>
                                                Total Quantity:</label>
                                            <asp:Label ID="lblTotalQty_pdf" runat="server">                                        
                                            </asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12" style="padding-right: 15px !important; padding-left: 0px !important">
                                        <asp:GridView ID="gvItemCostingDetails_pdf" runat="server" AutoGenerateColumns="true"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium table-bold"
                                            OnRowDataBound="gvItemCostingDetails_pdf_OnRowDataBound" OnDataBound="gvItemCostingDetails_pdf_OnDataBound" Style="font-size: 9px; width: 100% !important" HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <%--<div class="col-sm-12 p-t-10 text-center">
                                        <label class="form-label" style="color: Blue;">
                                            Addtional Part BOM Cost Details
                                        </label>
                                    </div>
                                    <div class="col-sm-12 p-t-10" id="AddtionalPartiv_pdf">
                                        <asp:GridView ID="gvAddtionalPartDetails_pdf" runat="server" AutoGenerateColumns="true"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium table-bold"
                                            Style="font-size: 9px; width: auto !important" HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:Label ID="lblAddtionalPartCost_pdf" Text="" runat="server" Style="color: darkgreen; font-size: x-large; font-family: fantasy;"></asp:Label>
                                    </div>--%>
                                    <div id="divcostbottomcontent" runat="server">
                                        <%--<div class="row text-center" style="padding-right: 15px">
                                            <asp:Label ID="lblTotalBOMCost_pdf" Text="" runat="server" Style="color: brown; font-size: x-large; float: right; font-family: fantasy;"></asp:Label>
                                        </div>--%>
                                        <div class="row">
                                            <label style="font-weight: 700; width: 16%;">
                                                Over All Length:</label>
                                            <asp:Label ID="lblOverAllLength_pdf" runat="server" Style="font-weight: 700; width: 20%;"></asp:Label>
                                            <asp:Label ID="lblBOMCost_pdf" Text="" runat="server" Style="color: darkgreen !important; font-size: x-large; float: right; font-family: fantasy; text-align: end; width: 62%; font-size: 16px ! important;"></asp:Label>
                                        </div>
                                        <div class="row">
                                            <label style="font-weight: 700; width: 16%;">
                                                LSE Number:</label>
                                            <asp:Label ID="lblLSENumber_pdf" runat="server" Style="font-weight: 700; width: 20%;"></asp:Label>
                                            <asp:Label ID="lblIssuePartCost_pdf" Text="" runat="server" Style="color: darkgreen !important; font-size: x-large; font-family: fantasy; text-align: end; width: 62%; font-size: 16px ! important;"></asp:Label>
                                        </div>
                                        <div class="row">
                                            <label style="font-weight: 700; width: 16%;">
                                                Dead Inventory Remarks:</label>
                                            <asp:Label ID="lblDeadInventoryRemarks_pdf" runat="server" Style="font-weight: 700; width: 20%;"></asp:Label>
                                            <asp:Label ID="lblTotalBOMCost_pdf" Text="" runat="server" Style="color: brown !important; font-size: x-large; font-family: fantasy; font-size: 16px ! important;"></asp:Label>
                                            <asp:Label ID="lblAddtionalPartCost_pdf" Text="" runat="server" Style="color: darkgreen !important; font-size: x-large; float: right; font-family: fantasy; text-align: end; width: 46%; font-size: 16px ! important;"></asp:Label>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <label style="font-weight: 700">
                                                    Remarks:</label>
                                                <asp:Label ID="lblRemarks_pdf" runat="server" Style="font-weight: 700"></asp:Label>
                                            </div>
                                            <div class="col-sm-6" style="padding-right: 15px">
                                                <div class="col-sm-12 text-right">
                                                    <label style="font-weight: 700">
                                                        Date:</label>
                                                    <asp:Label ID="lblDate_pdf" runat="server" Style="font-weight: 700"></asp:Label>
                                                </div>
                                                <div class="col-sm-12 text-right">
                                                    <label style="font-weight: 700">
                                                        User Name:</label>
                                                    <asp:Label ID="lblUserName_pdf" runat="server" Style="font-weight: 700"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:HiddenField ID="HiddenField2" runat="server" Value="" />
            </div>
        </div>
    </div>
</asp:Content>

