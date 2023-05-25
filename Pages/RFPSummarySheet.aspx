<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="RFPSummarySheet.aspx.cs" Inherits="Pages_RFPSummarySheet" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {
            BindDate();
        });

        function BindDate() {
            $('#tblRFPSummaryReport tfoot').append("<tr></tr>");
            $('#tblRFPSummaryReport thead th').each(function () {
                var title = $(this).text();
                $('#tblRFPSummaryReport tfoot tr').append('<th> <input type="text" class="form-control" placeholder="Search ' + title + '" /> </th>');
            });
            $('#tblRFPSummaryReport').DataTable({
                initComplete: function () {
                    // Apply the search
                    this.api().columns().every(function () {
                        var that = this;
                        $('input', this.footer()).on('keyup change clear', function () {
                            if (that.search() !== this.value) {
                                that
                                    .search(this.value)
                                    .draw();
                            }
                        });
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
                    url: "RFPSummarySheet.aspx/GetData", //It calls our web method  
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
                            return '<a href="#" id="' + data.RFPHID + '" onclick="return showpopup()";><img src="../Assets/images/view.png"></a>';
                        }
                    },
                    { 'data': 'RFPNo' },
                    { 'data': 'Location' },
                    { 'data': 'Quality' },
                    { 'data': 'Production' },
                    { 'data': 'QPStart' },
                    { 'data': 'MPStart', },
                    { 'data': 'IndentRaised' },
                    { 'data': 'PrimaryJobOrderStart' },
                    { 'data': 'JobCardStart' },
                    { 'data': 'AssemplyStart' },
                ],

                //"aoColumnDefs": [
                //    {
                //        "aTargets": [5],
                //        "mData": null,
                //        "mRender": function (data, type, full) {
                //            return '<button href="#"' + 'id="' + data.RFPHID + '" onclick="return showpopup()";></button>';
                //        }
                //    }
                //],

                scrollY: 550,
                scrollX: 550,
                fixedHeader: true
            });
        }

        function showpopup() {
            var val = $(event.target).closest('a').attr('id');
            var RFPNo = $(event.target).closest('tr').find('td:eq(1)').text();
            $('#tbItemdetails tbody').find('tr').remove();
            jQuery.ajax({
                type: "GET",
                url: "RFPSummarySheet.aspx/GetItemStatusDetailsByRFPHID", //It calls our web method  
                contentType: "application/json; charset=utf-8",
                dataType: "JSON",
                data: {
                    RFPHID: val,
                },
                success: function (data) {
                    if (data.d != "") {
                        var d = JSON.parse(data.d);
                        //QPStage1Status	QPStage2Status	MPStatus                    
                        $.each(d, function (i, val) {
                            $('#tbItemdetails tbody').append('<tr><td>' + parseInt(i) + ' </td> <td><a href="#" id="' + d[i].RFPDID + '" onclick="return ShowPartPopUp()";><img src="../Assets/images/view.png"></a></td> <td> '
                                + d[i].ItemName + ' </td> <td> ' + d[i].DrawingName + ' </td> <td> '
                                + d[i].QPStage1Status + '</td> <td>' + d[i].QPStage2Status + '</td> <td> ' + d[i].MPStatus + ' </td> </tr>');
                        });
                        BindDatatable('tbItemdetails');
                    }
                    else {
                        $('#tbItemdetails tbody').append('<tr><td colspan="6"> No Data Found </td> </tr>');
                    }
                    $('#lblRFPNo_h').text(RFPNo);
                },
                error: function (data) {
                }
            });
            $('#mpeDetailPopup').modal('show');
        }

        function BindDatatable(id) {
            $('#' + id + '').DataTable({
                "retrieve": true,
                "pageLength": 50,
                //  responsive: true                             
            });
        }

        function ShowPartPopUp() {
            var val = $(event.target).closest('a').attr('id');
            var ItemName = $(event.target).closest('tr').find('td:eq(2)').text();
            var drg = $(event.target).closest('tr').find('td:eq(3)').text();
            jQuery.ajax({
                type: "GET",
                url: "RFPSummarySheet.aspx/GetItemPartStatusDetailsByRFPDID", //It calls our web method  
                contentType: "application/json; charset=utf-8",
                dataType: "JSON",
                data: {
                    RFPDID: val,
                },
                success: function (data) {
                    var d = JSON.parse(data.d);
                    if (data.d != "") {
                        //PartName	QPStage1Status	QPStage2Status	MPStatus	Quantity TotalPartCompletedCount	JobCardStatus
                        $('#tblPartDetails tbody').find('tr').remove();
                        $.each(d, function (i, val) {
                            $('#tblPartDetails tbody').append('<tr><td>' + parseInt(i) + ' </td><td>' + d[i].PartName + ' </td><td>' + d[i].QPStage1Status + ' </td> <td>'
                                + d[i].QPStage2Status + '</td><td>' + d[i].MPStatus + '</td><td> ' + d[i].JobCardStatus + ' </td> </tr>');
                        });
                        BindDatatable('tblPartDetails');

                        $('#lblPartHeading').text($('#lblRFPNo_h').text() + '/' + ItemName + '/' + drg);
                    }
                    else {
                        $('#tblPartDetails tbody').append('<tr><td colspan="6"> No Data Found </td> </tr>');
                    }
                    $('#mpepartdetailspopup').modal('show');
                },
                error: function (data) {
                }
            });
        }

    </script>

    <style type="text/css">
        .dataTables_scrollHeadInner {
            height: 40px;
        }

            .dataTables_scrollHeadInner table thead th {
                vertical-align: initial;
            }

        .dataTables_scroll thead tr th:nth-child(1), .dataTables_scroll tbody tr td:nth-child(1),
        .dataTables_scroll tfoot tr th:nth-child(1), .dataTables_scroll tfoot tr th:nth-child(1) input {
            width: 78px !important;
        }

        .modal-dialog {
            margin-left: 2%;
            max-width: 98%;
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
                                    <h3 class="page-title-head d-inline-block">RFP Summary Report </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Reports</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">RFP Summary Report</li>
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

                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <table class="table table-hover table-bordered medium uniquedatatable"
                                                id="tblRFPSummaryReport"
                                                width="100%">
                                                <thead>
                                                    <tr>
                                                        <th>Item Status </th>
                                                        <th>RFP No</th>
                                                        <th>Location</th>
                                                        <th>Quality</th>
                                                        <th>Production</th>
                                                        <th>QP Start</th>
                                                        <th>MP Start</th>
                                                        <th>Indent Raised</th>
                                                        <th>Primary Job Order Start</th>
                                                        <th>Job Card Start</th>
                                                        <th>Assemply Start</th>
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
                    <asp:HiddenField ID="hdnBOMId" Value="0" runat="server" />

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="modal" id="mpeDetailPopup" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="upView" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD" style="margin-left: 48%;">
                                <label style="color: brown; font-weight: bold;" id="lblRFPNo_h"></label>
                            </h4>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="docdiv" class="docdiv">
                                <div class="inner-container">
                                    <div id="enquirydetailsdiv" class="enquirydetailsdiv" runat="server">
                                        <div class="col-sm-12">
                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <table class="table table-hover table-bordered medium uniquedatatable"
                                                    id="tbItemdetails"
                                                    width="100%">
                                                    <thead>
                                                        <tr>
                                                            <th>S.No </th>
                                                            <th>Status </th>
                                                            <th>Item Name </th>
                                                            <th>Drawing Name </th>
                                                            <th>QP Stage 1 </th>
                                                            <th>QP Stage 2 </th>
                                                            <th>MP Status </th>
                                                            <%--   <th>Job Card Status </th>--%>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div class="modal" id="mpepartdetailspopup" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">
                                <label id="lblPartHeading" style="color: brown;"></label>
                            </h4>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div class="docdiv">
                                <div class="inner-container">
                                    <div id="Div1" class="enquirydetailsdiv" runat="server">
                                        <div class="col-sm-12">
                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <table class="table table-hover table-bordered medium uniquedatatable"
                                                    id="tblPartDetails"
                                                    width="100%">
                                                    <thead>
                                                        <tr>
                                                            <th>S.No     </th>
                                                            <th>Part Name </th>
                                                            <th>QP Stage 1 </th>
                                                            <th>QP Stage 2 </th>
                                                            <th>MP Status  </th>
                                                            <th>Job Card Status  </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

</asp:Content>

