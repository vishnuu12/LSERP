<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="AllPOPrint.aspx.cs" Inherits="Pages_AllPOPrint" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        /*.table > tbody > tr > td, .table > tbody > tr > th,
        .table > tfoot > tr > td, .table > tfoot > tr > th,
        .table > thead > tr > td, .table > thead > tr > th {
            padding: 3px 8px;
            line-height: 1.2;
            font-size: 13px;
        }*/

        .radio-success label {
            color: brown !important;
            font-weight:bold;
        }

    </style>

    <script type="text/javascript">

        function rblChange() {
            var Mode = $("input[type='radio']:checked").val();
            if (Mode == "SPO") {
                $('#divWPO').attr('style', 'display:none;');
                $('#divSPO').attr('style', 'display:block;');
                BindSPODetails(Mode);
            }
            else if (Mode == "WPO") {
                $('#divWPO').attr('style', 'display:block;');
                $('#divSPO').attr('style', 'display:none;');
                BindWPODetails(Mode);
            }
        }

        $(document).ready(function () {
            $('#divWPO').attr('style', 'display:none;');
            $('#divSPO').attr('style', 'display:block;');
            BindSPODetails("SPO");
        });

        function BindSPODetails(Mode) {
            $('#tblSupplierPO').DataTable({
                "retrieve": true,
                "processing": true,
                "serverSide": true,
                "stateSave": true,
                //"ordering": false,
                //"bSort": false,    
                "bSort": false,
                "order": [],
                "bDeferRender": true,
                "ajax": ({
                    type: "GET",
                    url: "AllPOPrint.aspx/BindSPO", //It calls our web method  
                    // url:"https://innovasphere.com/LSERP/pages/PurchaseIndentMaterialInwardStatusReport/GetData",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    "data": function (d) {
                        d.Mode = Mode;
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
                    { 'data': 'PONo' },
                    { 'data': 'SupplierName' },
                    { 'data': 'IssueDate' },
                    {
                        'data': null,
                        "mRender": function (data, type, full) {
                            return `<a href="#" onclick="return openSPOPrint('${data.SPOID}')";><img src="../Assets/images/print.png" /></a>`
                        }
                    },
                ],
            });
        }

        function BindWPODetails(Mode) {
            $('#tblWPO').DataTable({
                "retrieve": true,
                "processing": true,
                "serverSide": true,
                "stateSave": true,
                //"ordering": false,
                //"bSort": false,    
                "bSort": false,
                "order": [],
                "bDeferRender": true,
                "ajax": ({
                    type: "GET",
                    url: "AllPOPrint.aspx/BindWPO", //It calls our web method  
                    // url:"https://innovasphere.com/LSERP/pages/PurchaseIndentMaterialInwardStatusReport/GetData",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    "data": function (d) {
                        d.Mode = Mode;
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
                    { 'data': 'WPONo' },
                    { 'data': 'VendorName' },
                    { 'data': 'IssueDate' },
                    {
                        'data': null,
                        "mRender": function (data, type, full) {
                            return `<a href="#" onclick="return openWPOPrint('${data.WPOID}')";><img src="../Assets/images/print.png" /></a>`
                        }
                    },
                ],
            });
        }

        function openSPOPrint(SPOID) {
            var url = window.location.href;
            var pagename = url.split('/');
            var Replacevalue = pagename[pagename.length - 1];
            var Page = url.replace(Replacevalue, "SupplierPOPrint.aspx?SPOID=" + SPOID + "");
            window.open(Page, '_blank');
        }

        function openWPOPrint(WPOID) {
            var url = window.location.href;
            var pagename = url.split('/');
            var Replacevalue = pagename[pagename.length - 1];
            var Page = url.replace(Replacevalue, "WorkorderPoPrint.aspx?WPOID=" + WPOID + "");
            window.open(Page, '_blank');
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
                                        <%--<i class="fa fa-th-large opacity-6 yellow"></i>--%>
                                        <img src="../Assets/images/pages.png" alt="" width="16px" />
                                    </span>
                                    <h3 class="page-title-head d-inline-block"> PO Print </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Print</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Purchase</li>
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
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" runat="server">
                            <div class="col-sm-12">
                                <div class="col-sm-4 text-right p-t-5">
                                    <label class="form-label">
                                        Select Mode</label>
                                </div>
                                <div class="col-sm-6 text-left">
                                    <asp:RadioButtonList ID="rblMode" runat="server" CssClass="radio radio-success" RepeatDirection="Horizontal"
                                        onchange="rblChange();" RepeatLayout="Flow"
                                        TabIndex="5">
                                        <asp:ListItem Selected="True" Value="SPO">SPO</asp:ListItem>
                                        <asp:ListItem Value="WPO">WPO</asp:ListItem>
                                        <%--    <asp:ListItem Value="PI">PI</asp:ListItem>--%>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col-sm-2">
                                </div>
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
                                        <div style="overflow-x: scroll; display: none;" id="divSPO">
                                            <div class="col-sm-12 p-t-10">
                                                <label>Supplier PO </label>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <table class="table table-hover table-bordered medium uniquedatatable"
                                                    id="tblSupplierPO"
                                                    width="100%">
                                                    <thead>
                                                        <tr>
                                                            <th>PO No</th>
                                                            <th>Supplier Name</th>
                                                            <th>Issue Date </th>
                                                            <th>Print </th>
                                                        </tr>
                                                    </thead>
                                                    <tfoot>
                                                    </tfoot>
                                                </table>
                                            </div>

                                        </div>

                                        <div style="overflow-x: scroll; display: none;" id="divWPO">
                                            <div class="col-sm-12 p-t-10">
                                                <label>Work Order PO </label>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <table class="table table-hover table-bordered medium uniquedatatable"
                                                    id="tblWPO"
                                                    width="100%">
                                                    <thead>
                                                        <tr>
                                                            <th>PO No</th>
                                                            <th>Vendor Name</th>
                                                            <th>Issue Date </th>
                                                            <th>Print </th>
                                                        </tr>
                                                    </thead>
                                                    <tfoot>
                                                    </tfoot>
                                                </table>
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
        </div>
    </div>
</asp:Content>


