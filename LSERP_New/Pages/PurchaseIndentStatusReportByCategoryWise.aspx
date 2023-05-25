<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="PurchaseIndentStatusReportByCategoryWise.aspx.cs"
    Inherits="Pages_PurchaseIndentStatusReportByCategoryWise" ClientIDMode="Predictable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function ShowAddPopUp() {
            $('#mpeView').modal({
                backdrop: 'static'
            });
            $('div').removeClass('modal-backdrop in');
            return false;
        }

        $(document).ready(function () {
            BindCategoryNameList();
        });

        function BindCategoryNameList() {
            jQuery.ajax({
                type: "GET",
                url: "PurchaseIndentStatusReportByCategoryWise.aspx/GetCategoryNameListDetails", //It calls our Method      
                contentType: "application/json; charset=utf-8",
                dataType: "JSON",
                success: function (data) {
                    if (data.d != "") {
                        var d = JSON.parse(data.d);
                        var mySelect = $('#ddlCategoryName');
                        mySelect.append(
                            $('<option></option>').val("0").html("- - SELECT - -")
                        );
                        $.each(d, function (i, text) {
                            mySelect.append(
                                $('<option></option>').val(d[i].CID).html(d[i].CategoryName)
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
            let CID = $('#ddlCategoryName').val();
            let FromDate = $('#ContentPlaceHolder1_txtFromDate').val();
            let ToDate = $('#ContentPlaceHolder1_txtToDate').val();

            let msg = true;

            if (CID == "0") {
                ErrorMessage('Error', 'Please Select Category Name');
                msg = false;
            }
            else if (FromDate == "") {
                ErrorMessage('Error', 'Enter From Date');
                msg = false;
            }
            else if (ToDate == "") {
                ErrorMessage('Error', 'Enter To Date');
                msg = false;
            }
            //InwardDate,InwardBy,POIssuedDate
            $('#tblPIStatusReport').DataTable().destroy();
            if (msg) {
                $('#divPIStatus').attr('style', 'display:block;');

                $('#tblPIStatusReport').DataTable({
                    "retrieve": true,
                    "processing": true,
                    "serverSide": true,
                    "stateSave": true,
                    "bSort": false,
                    "pageLength": 50,
                    "order": [],
                    "ajax": ({
                        type: "GET",
                        url: "PurchaseIndentStatusReportByCategoryWise.aspx/GetData", // It calls our web method //                   
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        "data": function (d) {
                            d.CID = CID;
                            d.FromDate = FromDate;
                            d.ToDate = ToDate;
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
                        { 'data': 'IndentBy' },
                        { 'data': 'IndentNo' },
                        { 'data': 'IndentDate' },
                        { 'data': 'GradeName' },
                        { 'data': 'IndentQty' },
                        { 'data': 'SupplierName' },
                        { 'data': 'POYear' },
                        { 'data': 'POIssuedDate' },
                        { 'data': 'POQty' },
                        { 'data': 'MRNNo' },
                        { 'data': 'InwardBy' },
                        { 'data': 'InwardDate' },
                        { 'data': 'InwardQty' },
                        { 'data': 'UnitCost' },
                        { 'data': 'InwardTCost' },
                        { 'data': 'POEnterQty' },
                    ],
                    "rowCallback": function (row, data, displayIndex, displayIndexFull) {
                        // Bold the grade for all 'A' grade browsers
                        if (data.MRNNo != null)
                            $(row).css('background-color', 'greenyellow');
                    },
                });
            }
            else
                $('#divPIStatus').attr('style', 'display:none;');
            return false;
        }

        function ExcelDownload() {
            jQuery.ajax({
                type: "GET",
                url: "PurchaseIndentStatusReportByCategoryWise.aspx/ExcelDownload", //It calls our Method      
                contentType: "application/json; charset=utf-8",
                dataType: "JSON",
                success: function (data) {
                    if (data.d != "") {
                        window.open(data.d);
                        SuccessMessage('Success', 'Excel Downloaded Succesfully');
                    }
                },
                error: function (data) {
                    ErrorMessage('Error', 'Error Occured');
                }
            });
            return false;
        }

    </script>

    <style type="text/css">
      
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
                                    <h3 class="page-title-head d-inline-block">Category Wise Indent Status Report </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Purchase</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Category Wise Indent Status Report </li>
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
                    <%-- <asp:PostBackTrigger ControlID="btnSave" />--%>
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" class="btnAddNew" runat="server">
                            <div class="text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right p-t-5">
                                        <label class="form-label">
                                            Category Name</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <select id="ddlCategoryName"
                                            title="Select Category Name" class="form-control chosenfalse">
                                        </select>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">Form Date </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker">  </asp:TextBox>
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
                                <div class="col-sm-12 p-t-10">
                                    <label class="totalInwardCost"></label>
                                </div>
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div id="divPIStatus" style="display: none;">
                                            <div class="col-sm-12 p-t-10">
                                                <asp:LinkButton ID="imgExcel" runat="server" CssClass="excel_bg" OnClientClick="return ExcelDownload();" ToolTip="Excel Download" />
                                            </div>
                                            <div class="col-sm-12" style="overflow-x: scroll;" runat="server">
                                                <table class="table table-hover table-bordered medium uniquedatatable" style="width: 100%;"
                                                    id="tblPIStatusReport">
                                                    <thead>
                                                        <tr>
                                                            <%-- <th>PID</th>--%>
                                                            <th>Indent By</th>
                                                            <th>Indent No</th>
                                                            <th>Indent Date</th>
                                                            <th>Grade Name</th>
                                                            <th>Indent Qty</th>
                                                            <th>Supplier Name</th>
                                                            <th>PO NO</th>
                                                            <th>Po Date </th>
                                                            <th>PO Qty</th>
                                                            <th>MRN No</th>
                                                            <th>Inward By </th>
                                                            <th>Inward Date</th>
                                                            <th>Inward Qty </th>
                                                            <th>Unit Price</th>
                                                            <th>Total Price </th>
                                                            <th>PO Enter Qty</th>
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
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal" id="mpeView" style="overflow-y: scroll;">
        <div style="max-width: 81%; padding-left: 10%; padding-top: 5%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="upView" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">Certficate Details</h4>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="docdiv" class="docdiv">
                                <div class="inner-container">
                                    <div id="Certificates" runat="server">
                                        <div id="divAddItems" class="divInput" runat="server">
                                            <div class="ip-div text-center">
                                            </div>
                                        </div>
                                        <div id="divOutputsItems" runat="server">
                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvCertificates" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Certificate Name" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCertificatename" runat="server" Text='<%# Eval("CertificateName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CertficateNo" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCertficateNo" Width="50px" runat="server" Text='<%# Eval("CertficateNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hdnSPODID" runat="server" Value="0" />
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

