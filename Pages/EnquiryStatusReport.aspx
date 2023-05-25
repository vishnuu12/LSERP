<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="EnquiryStatusReport.aspx.cs" Inherits="Pages_EnquiryStatusReport" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {
        });

        function bindIndentDetails() {
            let FromDate = $('#ContentPlaceHolder1_txtFromDate').val();
            let ToDate = $('#ContentPlaceHolder1_txtToDate').val();

            let msg = true;

            if (FromDate == "") {
                ErrorMessage('Error', 'Enter From Date');
                msg = false;
            }

            else if (ToDate == "") {
                ErrorMessage('Error', 'Enter To Date');
                msg = false;
            }

            $('#tblSalesSummaryReport').DataTable().destroy();

            if (msg) {
                $('#divEnqStatus').attr('style', 'display:block;');
                $('#tblSalesSummaryReport').DataTable({
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
                        url: "EnquiryStatusReport.aspx/GetData", //It Calls Web method                          
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        "data": function (d) {
                            d.FromDate = FromDate;
                            d.ToDate = ToDate;
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
                                return '<a href="#" id="' + data.EnquiryNumber + '" onclick="return opennewtab(' + data.EnquiryNumber + ')";>' + data.EnquiryNumber + '</a>';
                            }
                        },
                        { 'data': 'ProspectName' },
                        { 'data': 'ReceivedDate' },
                        { 'data': 'OfferDeadLineDate' },
                        { 'data': 'EnqStatus' },
                        { 'data': 'OrderStatus' },
                    ],
                    "rowCallback": function (row, data, displayIndex, displayIndexFull) {
                        //if (data.DelayOffer == "Deleay") {
                        //    //if (data.InwardCost == null)
                        //    $(row).css('background-color', 'red');
                        //    //else
                        //    //   $(row).css('background-color', '#10fafa');
                        //}
                        //else
                        //    $(row).css('background-color', 'lightgreen');
                    },
                });

                ViewEnquiryOverAllInformationDetails();
            }
            else
                $('#divEnqStatus').attr('style', 'display:none;');

            $('.table').parent().addClass('overflow');
            return false;
        }

        function opennewtab(EnquiryID) {
            var str = window.location.href.split('/');
            var replacevalue = str[str.length - 1];
            window.open(window.location.href.replace(replacevalue, 'EnquiryInformationDetails.aspx?EnquiryID=' + EnquiryID + ''), '_blank');
        }

        function ViewEnquiryOverAllInformationDetails() {
            let msg = true;

            let FromDate = $('#ContentPlaceHolder1_txtFromDate').val();
            let ToDate = $('#ContentPlaceHolder1_txtToDate').val();

            if (FromDate == "") {
                ErrorMessage('Error', 'Enter From Date');
                msg = false;
            }

            else if (ToDate == "") {
                ErrorMessage('Error', 'Enter To Date');
                msg = false;
            }

            if (msg) {
                $('#tblenquiryreport tbody').find('tr').remove();

                const obj = [{ "FromDate": FromDate, "ToDate": ToDate }];
                var datatext = { "date": JSON.stringify(obj) };
                jQuery.ajax({
                    type: "GET",
                    url: "EnquiryStatusReport.aspx/GetEnquiryDetailsReport", //It Calls Our Web Method
                    contentType: "application/json; charset=utf-8",
                    dataType: "JSON",
                    data: datatext,
                    success: function (data) {
                        if (data.d != "") {
                            var d = JSON.parse(data.d);
                            $.each(d, function (i, val) {
                                $('#tblenquiryreport tbody').append('<tr><td>' + d[i].Status + ' </td> <td>' + d[i].NoOfEnquiry + '</td></tr>');
                            });
                        }
                        else {
                            $('#tblenquiryreport tbody').append('<tr><td colspan="6"> No Data Found </td> </tr>');
                        }
                    },
                    error: function (data) {
                    }
                });
            }
            return false;
        }

    </script>
    <style type="text/css">
        .overflow {
            max-height: 67vh;
            overflow: auto;
            margin-top: 10px;
        }

            .overflow table thead tr {
                position: sticky;
                top: 0;
            }

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
                                    <h3 class="page-title-head d-inline-block">Enquiry Status Report </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Reports</a></li>
                                <li class="active breadcrumb-item" aria-current="page">Enquiry Status Report</li>
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
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">Form Date </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker"> </asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">To Date </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker">  </asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <asp:LinkButton ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return bindIndentDetails();"
                                        CssClass="btn btn-cons btn-success" />
                                </div>
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div id="divEnqStatus" style="display: none;">

                                            <div class="col-sm-12 p-t-10">
                                                <table class="table table-hover table-bordered medium uniquedatatable"
                                                    id="tblenquiryreport" width="100%">
                                                    <thead>
                                                        <tr>
                                                            <th>Status </th>
                                                            <th>Count  </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                    </tbody>
                                                </table>
                                            </div>

                                            <div class="col-sm-12 p-t-10">
                                                <table class="table table-hover table-bordered medium uniquedatatable"
                                                    id="tblSalesSummaryReport" width="100%">
                                                    <thead>
                                                        <tr>
                                                            <th>Enquiry No</th>
                                                            <th>Customer Name</th>
                                                            <th>Received Date</th>
                                                            <th>Offer Dead Line Date </th>
                                                            <th>Enquiry Status </th>
                                                            <th>Order Status </th>
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
                    <asp:HiddenField ID="hdnBOMId" Value="0" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

