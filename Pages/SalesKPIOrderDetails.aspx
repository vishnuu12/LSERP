﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master"
    AutoEventWireup="true" CodeFile="SalesKPIOrderDetails.aspx.cs"
    Inherits="Pages_SalesKPIOrderDetails" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript"> 
        function AddFooter() {
            let table = document.querySelectorAll('table th')
            table = parseInt(table.length) - 1;

            $('#ContentPlaceHolder1_gvMonthlyOrderPODetails').find('tbody').
                append('<tr style="background: lightgray;"><td colspan="' + table + '" class="text-right"> CUMULATIVE </td><td class="text-right">'
                    + parseFloat($('#ContentPlaceHolder1_gvMonthlyOrderPODetails').find('.ordervalue').toArray().map(x => Number(x.innerHTML)).reduce((sum, x) => sum + x)).toFixed(2) + '</td></tr>')
        }
    </script>
    <style type="text/css">
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

        table#ContentPlaceHolder1_gvMonthlyOrderPODetails td {
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
                                    <h3 class="page-title-head d-inline-block">Sales KPI Monthly Order Details </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Reports</a></li>
                                <li class="active breadcrumb-item" aria-current="page">Monthly Order Details </li>
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
                    <asp:PostBackTrigger ControlID="gvMonthlyOrderPODetails" />
                    <asp:PostBackTrigger ControlID="imgExcel" />

                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divInput" runat="server">
                            <div class="ip-div text-left">
                                <div class="text-center">
                                    <asp:Label ID="lblMonthYearName" Style="color: brown; font-weight: bold; font-size: 20px;"
                                        runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <asp:LinkButton ID="imgExcel" runat="server" CssClass="excel_bg" ToolTip="Excel Download"
                                            OnClick="btnExcelDownload_Click" />
                                    </div>
                                    <div class="col-sm-12">
                                        <asp:GridView ID="gvMonthlyOrderPODetails" runat="server"
                                            OnRowCommand="gvMonthlyOrderPODetails_OnRowCommand"
                                            OnRowDataBound="gvMonthlyOrderPODetails_OnRowDataBounds"
                                            CssClass="table table-bordered table-hover no-more-tables"
                                            AutoGenerateColumns="false" DataKeyNames="EnquiryNumber">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Prospect Name" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblprospectname" runat="server" Text='<%# Eval("ProspectName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Enquiry Number" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnPONo" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            CommandName="viewCustomerPODetails">
                                                            <asp:Label ID="lblEnquiryNumber" runat="server" Text='<%# Eval("EnquiryNumber")%>'></asp:Label>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Enquiry Received On" ItemStyle-HorizontalAlign="Center"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEnquiryDate" runat="server" Text='<%# Eval("EnquiryDate")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PO No" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPONo" runat="server" Text='<%# Eval("PORefNo")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Order By" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpocreatedby" runat="server" Text='<%# Eval("POCreatedBy")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Offer Dispatch On" ItemStyle-HorizontalAlign="Center"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOfferDispatchedon" runat="server" Text='<%# Eval("OfferDispatchedOn")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--   <asp:TemplateField HeaderText="Offer Cost" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbloffercost" runat="server" Text='<%# Eval("OfferCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="PO Date" ItemStyle-HorizontalAlign="Center"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpodate" runat="server" Text='<%# Eval("PODate")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Order Entry Date" ItemStyle-HorizontalAlign="Center"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblorderentrydate" runat="server" Text='<%# Eval("OrderEntryDate")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Currency Name" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcurrencyname" runat="server" Text='<%# Eval("CurrencySymbol")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Order Cost" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblordercost" runat="server" Text='<%# Eval("OrderCost")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Currency Value" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcurrencyvalue" runat="server" Text='<%# Eval("Curr")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="OrderValue (INR)" HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblordercostINR" CssClass="ordervalue" runat="server" Text='<%# Eval("OrderValue")%>'></asp:Label>
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
