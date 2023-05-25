<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="POAndInwardStatusReport.aspx.cs" Inherits="Pages_POAndInwardStatusReport" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        // var oTable;
        $(document).ready(function () {
            BindDate();
            // oTable = $('#tblPOInwardStatusReport').dataTable();
        });

        function BindDate() {
            $('#tblPOInwardStatusReport').DataTable({
                "retrieve": true,
                "processing": true,
                "serverSide": true,
                // "stateSave": true,
                //"ordering": false,
                //"bSort": false,               
                "bDeferRender": true,
                "pageLength": 100,
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
                    url: "POAndInwardStatusReport.aspx/GetData", //It calls our web method                      
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
                    { 'data': 'PONO' },
                    { 'data': 'PODate' },
                    { 'data': 'SupplierName' },
                    {
                        'data': null,
                        "mRender": function (data, type, full) {
                            return `<a href="#" onclick="return viewPODetails('${data.SPOID}')";>${data.TotalCost}</a>`
                        }
                    },
                    {
                        'data': null,
                        "mRender": function (data, type, full) {
                            if (data.InwardCost === null)
                                return `<a href="#" onclick="return openSPOPrint('${data.SPOID}')";></a>`
                            else
                                return `<a href="#" onclick="return ViewInwardDetails('${data.SPOID}')";>${data.InwardCost}</a>`
                        }
                    },
                    { 'data': 'InwardStatus' },
                    {
                        'data': null,
                        "mRender": function (data, type, full) {
                            return `<a href="#" onclick="return openSPOPrint('${data.SPOID}')";><img src="../Assets/images/print.png" /></a>`
                        }
                    },
                ],

                "rowCallback": function (row, data, displayIndex, displayIndexFull) {
                    if (data.TotalCost != data.InwardCost) {
                        if (data.InwardCost == null)
                            $(row).css('background-color', 'none');
                        else
                            $(row).css('background-color', '#10fafa');
                    }
                    else
                        $(row).css('background-color', 'lightgreen');
                },
            });
        }

        function BindDatatable(id) {
            $('#' + id + '').DataTable({
                "retrieve": true,
                "pageLength": 50,
                //  responsive: true                             
            });
        }

        function ExcelDownload() {
            jQuery.ajax({
                type: "GET",
                url: "POAndInwardStatusReport.aspx/ExcelDownLoad", //It calls our web method  
                contentType: "application/json; charset=utf-8",
                dataType: "JSON",
                success: function (data) {
                    //var response = JSON.parse(data.d);
                    window.location.href = "/Pages/POAndInwardStatusReport.aspx?FileName=" + data.d.Data.FileName + "";
                },
                error: function (data) {
                }
            });
            return false;
        }

        function openSPOPrint(SPOID) {
            var url = window.location.href;
            var pagename = url.split('/');
            var Replacevalue = pagename[pagename.length - 1];
            var Page = url.replace(Replacevalue, "SupplierPOPrint.aspx?SPOID=" + SPOID + "");
            window.open(Page, '_blank');
        }

        function viewPODetails(SPOID) {
            var PONO = $(event.target).closest('tr').find('td:eq(0)').text();
            var SupplierName = $(event.target).closest('tr').find('td:eq(2)').text();

            $('#tblsupplierpodetails tbody').find('tr').remove();
            jQuery.ajax({
                type: "GET",
                url: "POAndInwardStatusReport.aspx/GetSupplierPODetailsBySPOID", //It ca
                contentType: "application/json; charset=utf-8",
                dataType: "JSON",
                data: {
                    SPOID: SPOID,
                },
                success: function (data) {
                    if (data.d != "") {
                        var d = JSON.parse(data.d);
                        $.each(d, function (i, val) {
                            $('#tblsupplierpodetails tbody').append('<tr><td>' + parseInt(i + 1) + ' </td> <td>' + d[i].GradeName + '</td><td> '
                                + d[i].THKValue + ' </td> <td> ' + d[i].CategoryName + ' </td> <td> '
                                + d[i].TypeName + '</td> <td>' + d[i].UOM + '</td> <td> ' + d[i].Measurment + ' </td><td>' + d[i].UnitQuoteCost + '</td><td>' + d[i].ReqWeight + '</td><td>' + d[i].TotalCost + '</td> </tr>');
                        });
                        //  BindDatatable('tbItemdetails');
                    }
                    else {
                        $('#tblsupplierpodetails tbody').append('<tr><td colspan="6"> No Data Found </td> </tr>');
                    }
                    $('#lblPONo_h').text("Po Details " + PONO + "/" + SupplierName);
                },
                error: function (data) {
                }
            });
            $('#mpesupplierpodetails').modal('show');
        }

        function ViewInwardDetails(SPOID) {
            var PONO = $(event.target).closest('tr').find('td:eq(0)').text();
            var SupplierName = $(event.target).closest('tr').find('td:eq(2)').text();

            $('#tblInwardDetails tbody').find('tr').remove();
            jQuery.ajax({
                type: "GET",
                url: "POAndInwardStatusReport.aspx/GetInwardDetails", //It ca
                contentType: "application/json; charset=utf-8",
                dataType: "JSON",
                data: {
                    SPOID: SPOID,
                },
                success: function (data) {
                    if (data.d != "") {
                        var d = JSON.parse(data.d);
                        $.each(d, function (i, val) {
                            $('#tblInwardDetails tbody').append('<tr><td>' + parseInt(i + 1) + ' </td> <td> ' + d[i].MRNNumber + ' </td> <td>' + d[i].InwardBy + '</td> <td> ' + d[i].InwardOn + ' </td> <td>' + d[i].GradeName + '</td><td> '
                                + d[i].THKValue + ' </td> <td> ' + d[i].CategoryName + ' </td> <td> '
                                + d[i].TypeName + '</td> <td>' + d[i].UOM + '</td> <td> ' + d[i].Measurment + ' </td><td>' + d[i].UnitCost + '</td><td>' + d[i].DCQuantity + '</td><td>' + d[i].TotalCost + '</td> </tr>');
                        });
                        //  BindDatatable('tbItemdetails');
                    }
                    else {
                        $('#tblInwardDetails tbody').append('<tr><td colspan="6"> No Data Found </td> </tr>');
                    }
                    $('#lblPONo_h_inward').text("Inward Details " + PONO + "/" + SupplierName);
                },
                error: function (data) {
                }
            });
            $('#mpeInwarddetails').modal('show');
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

        .table {
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
                                    <h3 class="page-title-head d-inline-block">PO Inward Status Report </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Reports</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">PO Inward Status Report</li>
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
                                        <div class="col-sm-12 p-t-10">
                                            <span style="font-size: 16px; color: red; font-weight: bold;">Only Approved PO No Show Total Cost And Inward Cost Without  Tax And Other Charges Value </span>
                                        </div>
                                        <div class="col-sm-12 p-t-5">
                                            <div class="col-sm-6">
                                            </div>
                                            <div class="col-sm-6 text-right">
                                                <%--  <asp:LinkButton ID="imgExcel" runat="server" CssClass="excel_bg" ToolTip="Excel Download"
                                                OnClick="btnExcelDownload_Click" />--%>
                                                <button class="excel_bg" onclick="return ExcelDownload();"></button>
                                            </div>
                                        </div>

                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <table class="table table-hover table-bordered medium uniquedatatable"
                                                id="tblPOInwardStatusReport"
                                                width="100%">
                                                <thead>
                                                    <tr>
                                                        <th>PO No </th>
                                                        <th>PO Date </th>
                                                        <th>Supplier Name</th>
                                                        <th>Total Cost</th>
                                                        <th>Inward Cost</th>
                                                        <th>Inward Status</th>
                                                        <th>Print PO</th>
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>


    <div class="modal" id="mpesupplierpodetails" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="upView" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD" style="margin-left: 48%;">
                                <label style="color: brown; font-weight: bold;" id="lblPONo_h"></label>
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
                                                    id="tblsupplierpodetails"
                                                    width="100%">
                                                    <thead>
                                                        <tr>
                                                            <th>S.No </th>
                                                            <th>Grade Name </th>
                                                            <th>THK </th>
                                                            <th>Category Name </th>
                                                            <th>Type Name </th>
                                                            <th>UOM </th>
                                                            <th>Measurment </th>
                                                            <th>Unit Cost </th>
                                                            <th>Req Weight </th>
                                                            <th>Total Cost </th>
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


    <div class="modal" id="mpeInwarddetails" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD" style="margin-left: 48%;">
                                <label style="color: brown; font-weight: bold;" id="lblPONo_h_inward"></label>
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
                                                    id="tblInwardDetails"
                                                    width="100%">
                                                    <thead>
                                                        <tr>
                                                            <th>S.No </th>
                                                            <th>MRN No </th>
                                                            <th>Inward By </th>
                                                            <th>Inward On </th>
                                                            <th>Grade Name </th>
                                                            <th>THK </th>
                                                            <th>Category Name </th>
                                                            <th>Type Name </th>
                                                            <th>UOM </th>
                                                            <th>Measurment </th>
                                                            <th>Unit Cost </th>
                                                            <th>DC Qty </th>
                                                            <th>Total Cost </th>
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
