<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="ProductionAndQualityKPIReports.aspx.cs"
    Inherits="Pages_ProductionAndQualityKPIReports" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {

        });

        function btnGetReports() {
            let FromDate = $('#ContentPlaceHolder1_txtfromDate').val();
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
            //InwardDate,InwardBy,POIssuedDate

            $('#tblRFPKPIDispatchDetails').DataTable().destroy();
            if (msg) {
                $('#divPIStatus').attr('style', 'display:block;');

                $('#tblRFPKPIDispatchDetails').DataTable({
                    "retrieve": true,
                    "processing": true,
                    "serverSide": true,
                    "stateSave": true,
                    "bSort": false,
                    "pageLength": 50,
                    "order": [],
                    "ajax": ({
                        type: "GET",
                        url: "ProductionAndQualityKPIReports.aspx/GetData", // It calls our web method  //                   
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
                            json.data = json.d.data;
                            var return_data = json;
                            return return_data.data;
                        },
                    }),
                    "columns": [
                        { 'data': 'ProspectName' },
                        { 'data': 'RFPNo' },
                        { 'data': 'RFPApprovedOn' },
                        { 'data': 'DeliveryOn' },
                        { 'data': 'InvoiceEntryDate' },
                        { 'data': 'RFPDispatchedOn' },
                        { 'data': 'RFPDispatchedBy' },
                    ],
                    //"rowCallback": function (row, data, displayIndex, displayIndexFull) {
                    //    // Bold the grade for all 'A' grade browsers
                    //    if (data.MRNNo != null)
                    //        $(row).css('background-color', 'greenyellow');
                    //},
                });
            }
            else
                $('#divPIStatus').attr('style', 'display:none;');
            return false;
        }

        function ExcelDownload() {
            jQuery.ajax({
                type: "GET",
                url: "ProductionAndQualityKPIReports.aspx/ExcelDownload", //It calls our Method      
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
        table#tblRFPKPIDispatchDetails td {
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
                                    <h3 class="page-title-head d-inline-block">RFP KPI </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Reports</a></li>
                                <li class="active breadcrumb-item" aria-current="page">RFP KPI </li>
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
                    <asp:PostBackTrigger ControlID="gvRFPKpiReports" />
                    <asp:PostBackTrigger ControlID="imgExcel" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divInput" runat="server">
                            <div class="ip-div text-left">

                                <div class="col-sm-12">
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:RadioButtonList ID="rblKPIChange" runat="server" CssClass="radio radio-success" RepeatDirection="Horizontal"
                                            AutoPostBack="true" OnSelectedIndexChanged="rblKPIChange_OnSelectedChanged" RepeatLayout="Flow">
                                            <asp:ListItem Selected="True" Value="0"> Finance Year </asp:ListItem>
                                            <asp:ListItem Value="1"> Specific Date </asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                </div>

                                <div id="divFinanceYear" runat="server" visible="true">
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label mandatorylbl">
                                                Finance Year
                                            </label>
                                        </div>
                                        <div class="col-sm-6 text-left">
                                            <asp:DropDownList ID="ddlFinanceYear" runat="server" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlFinanceYear_OnSelectIndexChanged"
                                                CssClass="form-control mandatoryfield" ToolTip="Select Finance Year">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <asp:LinkButton ID="imgExcel" runat="server" CssClass="excel_bg" ToolTip="Excel Download"
                                            OnClick="btnExcelDownload_Click" />
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <asp:GridView ID="gvRFPKpiReports" runat="server"
                                            CssClass="table table-bordered table-hover no-more-tables"
                                            ShowHeaderWhenEmpty="True" OnRowCommand="gvRFPKpiReports_OnRowCommand"
                                            EmptyDataText="No Records Found" AutoGenerateColumns="false" DataKeyNames="Month,Year">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Month Name" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMonthName" runat="server" Text='<%# Eval("MonthName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No Of RFP Released" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnRFPReleased" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            CommandName="viewNoOfRFPReleased">
                                                            <asp:Label ID="lblordercost" runat="server"
                                                                CssClass="ordercost" Text='<%# Eval("RFPReleasedCount")%>'></asp:Label>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="No Of RFP Dispatched" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnRFPDispatched" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            CommandName="ViewRFPDispatchedDetails">
                                                            <asp:Label ID="lblNoOfRFPDispatched" runat="server" Text='<%# Eval("RFPDispatchedCount")%>'></asp:Label>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--   <asp:TemplateField HeaderText="Pending Estimation" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpending" runat="server" Text='<%# Eval("BOMNA")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delayed Estimation" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldelay" runat="server" Text='<%# Eval("Deleay")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="On Time Estimation" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblontime" runat="server" Text='<%# Eval("OnTime")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>

                                <div id="divspecficdate" runat="server" visible="false">

                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label mandatorylbl">
                                                From Date
                                            </label>
                                        </div>
                                        <div class="col-sm-6 text-left">
                                            <asp:TextBox ID="txtfromDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label mandatorylbl">
                                                To Date
                                            </label>
                                        </div>
                                        <div class="col-sm-6 text-left">
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>

                                    <div class="col-sm-12 text-center p-t-10">
                                        <asp:LinkButton ID="btnGetReports" CssClass="btn btn-success" runat="server"
                                            Text="GET" OnClientClick="return btnGetReports();"></asp:LinkButton>
                                    </div>

                                    <div id="divPIStatus" style="display:none;">
                                         <div class="col-sm-12 p-t-10">
                                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="excel_bg" OnClientClick="return ExcelDownload();" ToolTip="Excel Download" />
                                            </div>
                                        <div class="col-sm-12 p-t-10">
                                            <table class="table table-hover table-bordered medium uniquedatatable" style="width: 100%;"
                                                id="tblRFPKPIDispatchDetails">
                                                <thead>
                                                    <tr>
                                                        <th>Prospect Name </th>
                                                        <th>RFP No </th>
                                                        <th>RFP Approved On </th>
                                                        <th>Delivery On </th>
                                                        <th>Invoice Entry Date </th>
                                                        <th>RFPDispatchedOn </th>
                                                        <th>RFPDispatchedBy </th>
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
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
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

