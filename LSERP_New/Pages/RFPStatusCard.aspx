<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="RFPStatusCard.aspx.cs" Inherits="Pages_RFPStatusCard" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        $(document).ready(function () {
            BindDate();
        });

        function BindDate() {
            $('#tblRFPNoList').DataTable({
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
                    url: "RFPStatusCard.aspx/GetData", //It calls our web method                     
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
                            return '<a href="#" id="' + data.RFPHID + '" onclick="return opennewtab(' + data.RFPHID + ')";>' + data.RFPNo + '</a>';
                        }
                    },
                    { 'data': 'ProspectName' },
                    { 'data': 'Location' },
                    {
                        'data': null,
                        "mRender": function (data, type, full) {
                            if (data.NC != null)
                                return '<a href="#"  onclick="return CAPAReport(' + data.RFPHID + ')";><img src="../Assets/images/view.png" /></a>';
                            else
                                return 'NA'
                        }
                    }
                ],
            });
        }

        function opennewtab(RFPHID) {
            var str = window.location.href.split('/');
            var replacevalue = str[str.length - 1];
            window.open(window.location.href.replace(replacevalue, 'RFPItemStatusCard.aspx?RFPHID=' + RFPHID + ''), '_blank');
        }
         function CAPAReport(RFPHID) {
            var str = window.location.href.split('/');
            var replacevalue = str[str.length - 1];
            window.open(window.location.href.replace(replacevalue, 'NCReportDetailsByRFPHID.aspx?RFPHID=' + RFPHID + ''), '_blank');
        }

    </script>
    <style type="text/css">
        table {
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
                                    <h3 class="page-title-head d-inline-block">RFP Status Card </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Reports</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">RFP Status Card</li>
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
                                                id="tblRFPNoList"
                                                width="100%">
                                                <thead>
                                                    <tr>
                                                        <th>RFP No</th>
                                                        <th>Customer Name </th>
                                                        <th>Location</th>
                                                        <th>NC </th>
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

