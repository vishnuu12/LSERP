<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="PurchaseIndentMaterialInwardStatusReport.aspx.cs"
    Inherits="Pages_PurchaseIndentMaterialInwardStatusReport" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%-- <script type="text/javascript" src="https://code.jquery.com/jquery-3.5.1.js"></script>
    <link rel="stylesheet" type="text/css" href=" https://cdn.datatables.net/1.10.25/css/jquery.dataTables.min.css" />
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.25/js/jquery.dataTables.min.js"></script>--%>

    <script type="text/javascript">

        $(document).ready(function () {
            BindDate();
        });

        function BindDate() {
            $('#tblPIStatusReport').DataTable({
                "retrieve": true,
                "processing": true,
                "serverSide": true,
                "stateSave": true,
                "pageLength": 50,
                //"ordering": false,
                //"bSort": false,               
                "bDeferRender": true,
                "ajax": ({
                    type: "GET",
                    url: "PurchaseIndentMaterialInwardStatusReport.aspx/GetData", //It calls our web method  
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
                        json.data = json.d.data;
                        var return_data = json;
                        return return_data.data;
                    },
                }),
                "columns": [
                    { 'data': 'RFPNo' },
                    { 'data': 'INDNo' },
                    { 'data': 'PartName' },
                    { 'data': 'PONo' },
                    { 'data': 'PODate' },
                    { 'data': 'POQty' },
                    { 'data': 'PODeliveryDate' },
                    { 'data': 'SupplierName' },
                    { 'data': 'MRNNo' },
                    { 'data': 'DCQty' },
                    { 'data': 'InwardQty' },
                    { 'data': 'SI_InwardBy' },
                    { 'data': 'SI_InwardDate' },
                    { 'data': 'QCClearedDate' },
                    { 'data': 'QCClearedBy' },
                    { 'data': 'DCNo' }
                ]

                //"aoColumnDefs": [
                //    {
                //        "aTargets": [10],
                //        "mData": null,
                //        "mRender": function (data, type, full) {
                //            return '<button href="#"' + 'id="' + data.SPODID + '" onclick="return ShowPopUp()";><img src="../Assets/images/add1.png"></button>';
                //        }
                //    }
                //]
            });
        }

        function ShowPopUp() {
            var SPODID = '34';
            jQuery.ajax({
                type: "POST",
                url: "Login.aspx/checkUserNameAvail", //It calls our web method  
                contentType: "application/json; charset=utf-8",
                data: "{'iuser':'" + SPODID + "'}",
                dataType: "json",
                success: function (msg) {
                    $(msg).find("Table").each(function () {
                        var username = $(this).find('UserName').text();
                        if (username != '') {
                            //window.location.replace('/iCalendar.aspx');  
                            alert('This username already taken..');
                            $("#reguser").val('');
                            $("#reguser").focus();
                        }
                        else {
                        }
                    });
                },
                error: function (d) {
                }
            });
            return false;
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
                                    <h3 class="page-title-head d-inline-block">Purchase Indent Status Report</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Reports</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">PI report</li>
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
            <asp:UpdatePanel ID="upDocumenttype" runat="server">
                <Triggers>
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" runat="server">
                            <div class="text-center">
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="col-md-12 row">
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <table class="table table-hover table-bordered medium serversidedatatable"
                                                id="tblPIStatusReport"
                                                width="100%">
                                                <thead>
                                                    <tr>
                                                        <th>RFPNo</th>
                                                        <th>INDNo</th>
                                                        <th>PartName</th>
                                                        <th>PONo</th>
                                                        <th>PODate</th>
                                                        <th>POQty</th>
                                                        <th>PODeliveryDate</th>
                                                        <th>SupplierName</th>
                                                        <th>MRNNo</th>
                                                        <th>DCQty</th>
                                                        <th>InwardQty</th>
                                                        <th>SI_InwardBy</th>
                                                        <th>SI_InwardDate</th>
                                                        <th>QCClearedDate</th>
                                                        <th>QCClearedBy</th>
                                                        <th>DCNo</th>
                                                        <%-- <th>View
                                                        </th>--%>
                                                    </tr>
                                                </thead>
                                            </table>

                                            <div style="display: none;">
                                                <%--  <asp:LinkButton ID="btnpost" runat="server" Text="post" CssClass="btn btn-cons btn-save AlignTop"
                                                    OnClick="btnpost_Click"></asp:LinkButton>--%>
                                            </div>
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

