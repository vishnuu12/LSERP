<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master"
    AutoEventWireup="true" CodeFile="OverAllSupplierEvaluvationSummarySheet.aspx.cs"
    Inherits="Pages_OverAllSupplierEvaluvationSummarySheet" ClientIDMode="Predictable" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
</script>
    <style type="text/css">
        table td {
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
                                    <h3 class="page-title-head d-inline-block">Over All Supplier Evaluvation Report </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Reports</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page"> Over All Supplier Evaluvation Report</li>
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
                    <asp:PostBackTrigger ControlID="imgExcel" />
                    <asp:PostBackTrigger ControlID="gvOverAllSupplierEvaluvationReportDetails" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divInput" runat="server">
                            <div class="ip-div text-left">
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <label class="form-label mandatorylbl">
                                                    From Date</label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="mandatoryfield form-control datepicker"
                                                    PlaceHolder="Enter From Date" ToolTip="Enter From Date" AutoComplete="off"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <label class="form-label mandatorylbl">
                                                    To Date</label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtToDate" runat="server" placeholder="Enter To Date" ToolTip="Enter Deadline Date"
                                                    AutoComplete="off" CssClass="mandatoryfield form-control datepicker"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10 text-center">
                                            <asp:LinkButton ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divOutput');" OnClick="btnSubmit_Click"
                                                CssClass="btn btn-cons btn-success" />
                                        </div>

                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-6">
                                            </div>
                                            <div class="col-sm-6 text-right">
                                                <asp:LinkButton ID="imgExcel" runat="server" CssClass="excel_bg" ToolTip="Excel Download"
                                                    OnClick="btnExcelDownload_Click" />
                                            </div>
                                        </div>

                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvOverAllSupplierEvaluvationReportDetails" runat="server" AutoGenerateColumns="false" 
                                                CssClass="table table-bordered table-hover no-more-tables pagingfalse"
                                                OnRowCommand="gvOverAllSupplierEvaluvationReportDetails_OnRowCommand" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" DataKeyNames="SUPID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Supplier Name">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbltnsuppliername" runat="server" CommandName="ViewEvaluvation" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                                <asp:Label ID="lblSupplierName" runat="server" Text='<%# Eval("SupplierName")%>'></asp:Label>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Avg">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAverage" runat="server" Text='<%# Eval("Average")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
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
