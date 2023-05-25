<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="PurchaseIndentStatusDemoReport.aspx.cs" Inherits="Pages_PurchaseIndentStatusDemoReport" ClientIDMode="Predictable" %>

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
                //"ordering": false,
                //"bSort": false,               
                "bDeferRender": true,
                "ajax": ({
                    type: "GET",
                    url: "PurchaseIndentStatusDemoReport.aspx/GetData", //It calls our web method  
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
                    { 'data': 'RFPNo' },
                    { 'data': 'INDNo' },
                    { 'data': 'PartName' },
                    { 'data': 'PONo' },
                    { 'data': 'PODate' },
                    { 'data': 'POQty' },
                    { 'data': 'PODeliveryDate' },
                    { 'data': 'SupplierName' },
                    { 'data': 'MRNNo' },
                    { 'data': 'DCQty' }
                ]
                //"aoColumnDefs": [
                //    {
                //        "aTargets": [3],
                //        "mData": null,
                //        "mRender": function (data, type, full) {
                //            return '<button href="#"' + 'id="' + data.EmployeeID + '" onclick="return showpopup()";><img src="../Assets/images/add1.png"></button>';
                //        }
                //    }
                //]
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
                                        <div class="col-sm-12 p-t-10">

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

