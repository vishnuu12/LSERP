<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="WorkOrderIndentStatusReport.aspx.cs"
    Inherits="Pages_WorkOrderIndentStatusReport" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {
            BindDate();
        });

        function BindDate() {
            //$('#tblCompanySummaryReport tfoot').append("<tr></tr>");
            //$('#tblCompanySummaryReport thead th').each(function () {
            //    var title = $(this).text();
            //    $('#tblCompanySummaryReport tfoot tr').append('<th> <input type="text" class="form-control" placeholder="Search ' + title + '" /> </th>');
            //});
            $('#tblWOIStatusReport').DataTable({
                //initComplete: function () {
                //    // Apply the search
                //    this.api().columns().every(function () {
                //        var that = this;
                //        $('input', this.footer()).on('keyup change clear', function () {
                //            if (that.search() !== this.value) {
                //                that
                //                    .search(this.value)
                //                    .draw();
                //            }
                //        });
                //    });
                //},
                "retrieve": true,
                "processing": true,
                "serverSide": true,
                // "stateSave": true,
                //"ordering": false,
                //"bSort": false,               
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
                    url: "WorkOrderIndentStatusReport.aspx/GetData", //It calls our web method  
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
                            //return "<a href='#'  onclick='return opennewtab('''+data.WoDrawingname+''');'><img src='../Assets/images/view.png'></a>";
                            //return '<a href="#"  onclick="return opennewtab(`${data}`);"><img src="../Assets/images/view.png"></a>';
                            return `<a href="#" onclick="return opennewtab('${data.WoDrawingname}')";><img src= "../Assets/images/view.png"></a>`
                        }
                    },
                    { 'data': 'RFPNo' },
                    { 'data': 'INDNo' },
                    { 'data': 'INDDate' },
                    { 'data': 'IndentBy' },
                    { 'data': 'INDQty' },
                    { 'data': 'ItemName' },
                    { 'data': 'PartName' },
                    { 'data': 'WONo' },
                    { 'data': 'WODate' },
                    { 'data': 'POQty' },
                    { 'data': 'DeliveryDate' },
                    { 'data': 'DCNo' },
                    { 'data': 'DCDate' },
                    { 'data': 'IssuedQty' },
                    { 'data': 'VendorName' },
                    { 'data': 'InwardQty' },
                    { 'data': 'QCStatus' },
                    { 'data': 'DCNumber' },
                    { 'data': 'inWardBy' },
                    { 'data': 'QCClearedBy' },
                    { 'data': 'InwardDate' },
                    { 'data': 'QCClearedDate' },
                ],
            });
        }

        function opennewtab(Filepath) {
            window.open('http://183.82.33.21/LSERPDocs/WorkOrderIndent/' + Filepath, '_blank');
        }

    </script>

    <style type="text/css">
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
                                    <h3 class="page-title-head d-inline-block">WOI Status Report </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Reports</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">WOI Status Report</li>
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
                                        <%--  DCNumber,inWardBy,QCClearedBy	
                                        InwardDate,QCClearedDate--%>
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <table class="table table-hover table-bordered medium uniquedatatable"
                                                id="tblWOIStatusReport"
                                                width="100%">
                                                <thead>
                                                    <tr>
                                                        <th>Drawing</th>
                                                        <th>RFPNo</th>
                                                        <th>INDNo</th>
                                                        <th>INDDate</th>
                                                        <th>IndentBy</th>
                                                        <th>INDQty</th>
                                                        <th>ItemName</th>
                                                        <th>PartName</th>
                                                        <th>WONo </th>
                                                        <th>WODate</th>
                                                        <th>POQty</th>
                                                        <th>DeliveryDate </th>
                                                        <th>DCNo</th>
                                                        <th>DCDate</th>
                                                        <th>IssuedQty</th>
                                                        <th>VendorName</th>
                                                        <th>InwardQty</th>
                                                        <th>QCStatus</th>
                                                        <th>DCNumber</th>
                                                        <th>inWardBy</th>
                                                        <th>QCClearedBy</th>
                                                        <th>InwardDate</th>
                                                        <th>QCClearedDate</th>
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


