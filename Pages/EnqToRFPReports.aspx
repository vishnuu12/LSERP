<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="EnqToRFPReports.aspx.cs" 
    Inherits="Pages_EnqToRFPReports" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">     
    <style type="text/css">
                   table#tblEnqToRfpReport td {
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
            font-family: serif;
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
            $('#tblEnqToRfpReport').DataTable({

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
                    url: "EnqToRFPReports.aspx/GetData", //It calls our web method
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
                    { 'data': 'EnquiryID' },
                    { 'data': 'CustomerEnquiryNumber' },
                    { 'data': 'LseNumber' },
                    { 'data': 'ProspectName' },
                    { 'data': 'EnquiryTypeName' },
                    { 'data': 'ConTactPerson' },
                    { 'data': 'ContactNumber', },
                    { 'data': 'Email' },
                    { 'data': 'ReceivedDate' },
                    { 'data': 'ClosingDate' },
                    { 'data': 'ProjectDescription' },
                    { 'data': 'EnquiryLocation' },
                    { 'data': 'SourceName' },
                    { 'data': 'EmployeeName' },
                    { 'data': 'OfferSubmissionDate' },
                    { 'data': 'Staff' },
                    { 'data': 'Offer No' },
                    { 'data': 'Offer Date', },
                    { 'data': 'Order No' },
                    { 'data': 'Order Date' },
                    { 'data': 'ItemCount' },
                    { 'data': 'RFPNo' },
                ],
            });
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
                                    <h3 class="page-title-head d-inline-block">Enquiry Report </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Reports</a></li>
                                <li class="active breadcrumb-item" aria-current="page">Enquiry Report</li>
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
                                                    <asp:Label ID="lbltitle" runat="server" Text="Enquiry To RFP Reports"></asp:Label>
                                                </p>
                                            </div>
                                             <div class="col-md-6 ex-icons" id="divDownload" runat="server">
                                                <asp:LinkButton ID="imgExceldownload" runat="server" CssClass="excel_bg" ToolTip="Excel Download"
                                                    OnClick="btnExcel_Click" />
                                            </div>
                                        </div>
                                        <div id="divEnqReports">

                                            <div class="col-sm-12 p-t-10"  style="overflow-x: scroll;">
                                                <table class="table table-hover table-bordered medium"
                                                    id="tblEnqToRfpReport" style="width: 100%;">
                                                    <thead>
                                                        <tr>
                                                        <th>EnqID </th>
                                                        <th>CustomerEnquiry </th>
                                                        <th>LSENo</th>
                                                        <th>Prospect Name</th>
                                                        <th>Type</th>
                                                        <th>ConTact Person</th>
                                                        <th>Contact Number</th>
                                                        <th>Email</th>
                                                        <th>ReceivedDate</th>
                                                        <th>ClosingDate</th>
                                                        <th>ProjectDescription</th>
                                                        <th>Location</th>
                                                        <th>Source</th>
                                                        <th>Employee Name</th>
                                                        <th>Offer Sub Date</th>
                                                        <th>Sales Staff</th>
                                                        <th>Offer No</th>
                                                        <th>Offer Date</th>
                                                        <th>Order No</th>
                                                        <th>Order Date</th>
                                                        <th>Item Count</th>
                                                        <th>RFP No</th>
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
                    <asp:HiddenField ID="hdnEnquiryId" Value="0" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

