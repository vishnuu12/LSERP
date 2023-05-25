<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="WorkOrderReports.aspx.cs"
    Inherits="Pages_WorkOrderReports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="../Assets/css/NewDataTable.css" />
    <link rel="stylesheet" type="text/css" href="../Assets/css/NewDataTableMin.css" />
    <script type="text/javascript" src="../Assets/scripts/NewDataTable1.js"></script>
    <script type="text/javascript" src="../Assets/scripts/NewDataTable2.js"></script>
    <script type="text/javascript" src="../Assets/scripts/NewDataTable3.js"></script>
    <script type="text/javascript" src="../Assets/scripts/NewDataTable4.js"></script>
    <script type="text/javascript" src="../Assets/scripts/NewDataTableButton.js"></script>
    <script type="text/javascript" src="../Assets/scripts/NewDataTableButton1.js"></script>
    <script type="text/javascript" src="../Assets/scripts/NewDataTablePrint.js"></script>

    <style type="text/css">
        div.dataTables_scrollBody > table {
            border-top: none;
            margin-top: 0px !important;
            margin-bottom: 0 !important;
            white-space: nowrap;
        }

        .dataTables_scrollBody.overflow {
            margin-top: 0%;
        }

        table#WorkOrderReports td {
            color: black;
            font-weight: bold;
            font-size: smaller;
        }

        .overflow {
            max-height: 65vh;
            overflow: auto;
            margin-top: 10px;
        }

            .overflow table thead tr {
                position: sticky;
                top: 0;
            }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {
            BindDate();
        });

        function BindDate() {
            $('#WorkOrderReports').DataTable({
                "dom": 'Bfrtip',
                "buttons": [
                    'csv', 'excel', 'print'
                ],
                "scrollX": true,
                "fnInitComplete": function () {

                    // Enable THEAD scroll bars
                    $('.dataTables_scrollHead').css('overflow', 'auto');

                    // Sync THEAD scrolling with TBODY
                    $('.dataTables_scrollHead').on('scroll', function () {
                        $('.dataTables_scrollBody').scrollLeft($(this).scrollLeft());
                    });
                },
                "retrieve": true,
                "processing": true,
                "serverSide": true,
                // "stateSave": true,
                //"ordering": false,
                //"bSort": false,               
                "bDeferRender": true,
                "pageLength": 100,
                "columnDefs": [
                    { "width": "10%", "targets": 0 },
                    { "width": "10%", "targets": 2 }
                ],
                "bSort": false,
                "order": [],
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
                    url: "WorkOrderReports.aspx/GetData", //It calls our web method
                    // url:"https://innovasphere.com/LSERP/pages/PurchaseIndentMaterialInwardStatusReport/GetData",
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
                    {
                        'data': null,
                        "mRender": function (data, type, full) {
                            return `<a href="#" onclick="return opennewtab('${data.WoDrawingname}')";><img src= "../Assets/images/view.png"></a>`
                        }
                    },
                    { 'data': 'QCStatus' },
                    { 'data': 'InwardStatus' },
                    { 'data': 'DCNo' },
                    { 'data': 'DCDate' },
                    { 'data': 'InwardDCNo' },
                    { 'data': 'InwardDate' },
                    { 'data': 'InwardBy' },
                    { 'data': 'WONo' },
                    { 'data': 'WODate' },
                    { 'data': 'INDNo' },
                    { 'data': 'INDDate' },
                    { 'data': 'IndentBy' },
                    { 'data': 'RFPNo' },
                    { 'data': 'VendorName' },
                    { 'data': 'Location' },
                    { 'data': 'InwardQty' },
                    { 'data': 'POQty' },
                    { 'data': 'INDQty' },
                ],
            });

            $('.table').parent().addClass('overflow');
            return false;
        }

        function opennewtab(Filepath) {
            window.open('http://lonestarindia.in///LSERPDocs/WorkOrderIndent/' + Filepath, '_blank');
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
                                    <h3 class="page-title-head d-inline-block">WorkOrder Report </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Reports</a></li>
                                <li class="active breadcrumb-item" aria-current="page">WorkOrder Report</li>
                            </ol>
                        </nav>
                        <a id="help" href="#" alt="" style="margin-top: 4px;">
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
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <table class="table table-hover table-bordered medium uniquedatatable"
                                                id="WorkOrderReports"
                                                width="100%">
                                                <thead>
                                                    <tr>
                                                        <th>Drawing</th>
                                                        <th>QC Status</th>
                                                        <th>Inward Status</th>
                                                        <th>DCNo</th>
                                                        <th>DCDate</th>
                                                        <th>InwardDCNo</th>
                                                        <th>InwardDate</th>
                                                        <th>InWardBy</th>
                                                        <th>WONo </th>
                                                        <th>WODate</th>
                                                        <th>INDNo</th>
                                                        <th>INDDate</th>
                                                        <th>IndentBy</th>
                                                        <th>RFPNo</th>
                                                        <th>VendorName</th>
                                                        <th>Location</th>
                                                        <th>InwardQty</th>
                                                        <th>POQty</th>
                                                        <th>INDQty</th>
                                                    </tr>
                                                </thead>
                                            </table>
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

