<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="RFPItemJobCardStatusReport.aspx.cs" Inherits="Pages_RFPItemJobCardStatusReport" ClientIDMode="Predictable" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {
            BindDate();
        });

        function BindDate() {
            $('#tblRFPItemJobCardStatus').DataTable({
                "retrieve": true,
                "processing": true,
                "serverSide": true,
                "bDeferRender": true,
                "pageLength": 100,
                "ordering": false,
                "bSort": false,
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
                    url: "RFPItemJobCardStatusReport.aspx/GetData", //It calls our web method                     
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
                        $('#ContentPlaceHolder1_lblItemName').text(json.d.ItemName);
                        var return_data = json;
                        return return_data.Ldata;
                    },
                }),
                "columns": [
                    {
                        'data': null,
                        "mRender": function (data, type, full) {
                            return '<a href="#"  onclick="return opennewtab(' + data.JCHID + ',' + data.PMID + ')";>' + data.JobCardNo + '</a>';
                        }
                    },
                    { 'data': 'ProcessName' },
                    { 'data': 'PartName' },
                    { 'data': 'PARTQTY' },
                    { 'data': 'QCStatus' },
                    { 'data': 'Jobstatus' },
                    { 'data': 'JobCardDate' },
                    { 'data': 'JobCardRaisedBy' },
                    { 'data': 'CategoryName' },
                    { 'data': 'GradeNameProduction' },
                    { 'data': 'JobOrderRemarks' },
                    { 'data': 'JobCardQCRequestBy' },
                    { 'data': 'JobCardQCRequestOn' },
                    { 'data': 'QCDoneOn' },
                    { 'data': 'QCDoneBy' },
                ],
                "rowCallback": function (row, data, displayIndex, displayIndexFull) {
                    if (data.Jobstatus == 'Completed')
                        $(row).css('background-color', 'lightgreen');
                    else
                        $(row).css('background-color', 'orangered');
                },
            });
        }

        function opennewtab(JCHID, PMID) {
            var str = window.location.href.split('/');
            var replacevalue = str[str.length - 1];
            if (PMID == 1)
                window.open(window.location.href.replace(replacevalue, 'SheetmarkingAndCuttingProcessCardPrint.aspx?JCHID=' + JCHID + ''), '_blank');
            else if (PMID == 2)
                window.open(window.location.href.replace(replacevalue, 'SheetWeldingCardPrint.aspx?JCHID=' + JCHID + ''), '_blank');
            else if (PMID == 3)
                window.open(window.location.href.replace(replacevalue, 'BellowFormingAndTangentCuttingPrint.aspx?JCHID=' + JCHID + ''), '_blank');
            else if (PMID == 4)
                window.open(window.location.href.replace(replacevalue, 'MarkingAndCuttingPrint.aspx?JCHID=' + JCHID + ''), '_blank');
            else if (PMID == 5)
                window.open(window.location.href.replace(replacevalue, 'FabricationAndWelding.aspx?JCHID=' + JCHID + ''), '_blank');
        }

    </script>
    <style type="text/css">
        table {
            color: #000;
            font-weight: bold;
        }

        table {
            color: #000;
            font-weight: bold;
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
                                    <h3 class="page-title-head d-inline-block">RFP Item Job Card Status </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Reports</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">RFP Item Job Card Status</li>
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
                                <asp:Label ID="lblItemName" Style="color: blueviolet; font-weight: bold; font-size: 20px;"
                                    runat="server"></asp:Label>
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="col-sm-12 p-t-10 text-center" style="background: yellow;">
                                            <label style="color: brown; font-size: 20px;">
                                                RFP ITEM JOB CARD STATUS
                                            </label>
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <table class="table table-hover table-bordered medium uniquedatatable"
                                                id="tblRFPItemJobCardStatus"
                                                width="100%">
                                                <thead>
                                                    <tr>
                                                        <th>Job No              </th>
                                                        <th>Process </th>
                                                        <th>PartName               </th>

                                                        <th>QTY                </th>
                                                        <th>QCStatus               </th>
                                                        <th>Jobstatus              </th>
                                                        <th>Job Date            </th>
                                                        <th>JobRaisedBy        </th>
                                                        <th>Category           </th>
                                                        <th>Grade Name    </th>
                                                        <th>Job Remarks        </th>
                                                        <th>QC Request By     </th>
                                                        <th>Requested On </th>
                                                        <th>QCDoneOn               </th>
                                                        <th>QCDoneBy               </th>
                                                    </tr>
                                                </thead>
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

