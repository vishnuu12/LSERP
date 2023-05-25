<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="MaterialNameOfPreviousPriceReport.aspx.cs"
    Inherits="Pages_MaterialNameOfPreviousPriceReport" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">

        table {
            color: #000;
            font-weight: bold;
        }

    </style>

    <script type="text/javascript">

        $(document).ready(function() {
            BindMaterialNameDetails();
            $('#divPIStatus').attr('style','display:none;');
            // bindIndentDetails();
        });

        function BindMaterialNameDetails() {
            jQuery.ajax({
                type: "GET",
                url: "MaterialNameOfPreviousPriceReport.aspx/GetMaterialNameDetails", //It calls our Method      
                contentType: "application/json; charset=utf-8",
                dataType: "JSON",
                success: function (data) {
                    if (data.d != "") {
                        var d = JSON.parse(data.d);
                        var mySelect = $('#ddlMaterialName');
                        mySelect.append(
                            $('<option></option>').val("0").html("- - SELECT - -")
                        );
                        $.each(d, function (i, text) {
                            mySelect.append(
                                $('<option></option>').val(d[i].MGMID).html(d[i].GradeName)
                            );
                        });
                        $('select').chosen();
                    }
                },
                error: function (data) {
                }
            });
        }

        function bindIndentDetails() {

            $('#tblMaterialPreviouspricereportDetails').DataTable({
                "retrieve": true,
                "processing": true,
                "serverSide": true,
                "stateSave": true,
                "bSort": false,
                "pageLength": 50,
                "order": [],
                "ajax": ({
                    type: "GET",
                    url: "MaterialNameOfPreviousPriceReport.aspx/GetData", // It calls our web method //                   
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    "data": function (d) {
                        d.MGMID = $('#ddlMaterialName').val();
                        return d;
                    },
                    "dataSrc": function (json) {
                        json.draw = json.d.draw;
                        json.recordsTotal = json.d.recordsTotal;
                        json.recordsFiltered = json.d.recordsFiltered;
                        json.data = json.d.data;
                        json.TotalInwardCost = json.d.TotalInwardCost;
                        var return_data = json;
                        return return_data.data;
                    },
                }),
                "columns": [
                    //{ 'data': 'PID' },
                    { 'data': 'SupplierName' },
                    {
                        'data': null,
                        "mRender": function (data, type, full) {
                            return '<a href="#" onclick="return openSPOPrint(' + data.SPOID + ')";>' + data.PONo + '</a>';
                        }
                    },
                    { 'data': 'UnitCost' },
                    { 'data': 'CategoryName' },
                    { 'data': 'THKValue' },
                    { 'data': 'QuotationApprovedOn' },
                    { 'data': 'QuoteApprovedBy' }
                ],
                //"rowCallback": function (row, data, displayIndex, displayIndexFull) {
                //    // Bold the grade for all 'A' grade browsers
                //    if (data.MRNNo != null)
                //        $(row).css('background-color', 'greenyellow');
                //},
            });
        }

        function openSPOPrint(SPOID) {
            var url = window.location.href;
            var pagename = url.split('/');
            var Replacevalue = pagename[pagename.length - 1];
            var Page = url.replace(Replacevalue, "SupplierPOPrint.aspx?SPOID=" + SPOID + "");
            window.open(Page, '_blank');
        }

        function drawdatatable() {
            if ($('#ddlMaterialName').val() != "0") {
                $('#divPIStatus').attr('style', 'display:block;');
                bindIndentDetails();
                $('#tblMaterialPreviouspricereportDetails').DataTable().draw();
            }
            else
                $('#divPIStatus').attr('style', 'display:none;');
        }

    </script>

    <style type="text/css">
        td:nth-child(1) {
            text-align: left;
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
                                        <img src="../Assets/images/pages.png" alt="" width="16px" />
                                    </span>
                                    <h3 class="page-title-head d-inline-block">Supplier Material Price Report </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Purchase</a></li>
                                <li class="active breadcrumb-item" aria-current="page">Price</li>
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
                        <div id="divAdd" class="btnAddNew" runat="server">
                        </div>
                        <div id="divInput" runat="server">
                            <div class="col-sm-12">
                                <div class="col-sm-4 text-right p-t-5">
                                    <label class="form-label">
                                        Material Name</label>
                                </div>
                                <div class="col-sm-6 text-left">
                                    <select id="ddlMaterialName" onchange="drawdatatable();"
                                        title="Select Material Name" class="form-control chosenfalse">
                                    </select>
                                </div>
                                <div class="col-sm-2">
                                </div>
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="col-md-12 row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                                    <asp:Label ID="lbltitle" runat="server"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>

                                        <div class="col-sm-12 p-t-10">
                                        </div>

                                        <div class="col-sm-12 text-center p-t-10" id="divPIStatus">
                                            <table class="table table-hover table-bordered medium uniquedatatable"
                                                id="tblMaterialPreviouspricereportDetails"
                                                width="100%">
                                                <thead>
                                                    <tr>
                                                        <th style="text-align: left;">Supplier Name </th>
                                                        <th>PO No </th>
                                                        <th>Unit Cost  </th>
                                                        <th>Category Name </th>
                                                        <th>THKValue </th>
                                                        <th>Quote Approved On </th>
                                                        <th>Quote Approved BY </th>
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

