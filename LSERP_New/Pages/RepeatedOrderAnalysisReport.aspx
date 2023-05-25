<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="RepeatedOrderAnalysisReport.aspx.cs" Inherits="Pages_RepeatedOrderAnalysisReport" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        // var oTable;
        $(document).ready(function () {
            BindDate();
            //oTable = $('#tblPOInwardStatusReport').dataTable();
        });

        function BindDate() {
            $('#tblRepeatedOrderAnalysisReport').DataTable({
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
                    url: "RepeatedOrderAnalysisReport.aspx/GetData", //It calls our web method                      
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
                    { 'data': 'ProspectName' },
                    {
                        'data': null,
                        "mRender": function (data, type, full) {
                            return `<a href="#" onclick="return viewPODetails('${data.ProspectID}')";>${data.OrderCount}</a>`
                        }
                    },
                ],
            });
        }

        function viewPODetails(ProspectID) {
            var url = window.location.href;
            var pagename = url.split('/');
            var Replacevalue = pagename[pagename.length - 1];
            var Page = url.replace(Replacevalue, "ViewCustomerOrderDetails.aspx?ProspectID=" + ProspectID + "");
            window.open(Page, '_blank');
        }

        function ExcelDownload() {
            jQuery.ajax({
                type: "GET",
                url: "RepeatedOrderAnalysisReport.aspx/ExcelDownLoad", //It calls our web method  
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
                                    <h3 class="page-title-head d-inline-block">Repeat Order Analysis Report </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Reports</a></li>
                                <li class="active breadcrumb-item" aria-current="page">ROA Report</li>
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
                                                id="tblRepeatedOrderAnalysisReport"
                                                width="100%">
                                                <thead>
                                                    <tr>
                                                        <th>Customer Name </th>
                                                        <th>Order Count </th>
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
</asp:Content>

