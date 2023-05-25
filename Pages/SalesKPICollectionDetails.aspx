﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="SalesKPICollectionDetails.aspx.cs"
    Inherits="Pages_SalesKPICollectionDetails" ClientIDMode="Predictable" %>


<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript"> 

        function AddFooter() {
            let table = document.querySelectorAll('table th')
            table = parseInt(table.length) - 1;

            $('#ContentPlaceHolder1_gvSalesKPICollectionDetails').find('tbody').
                append('<tr style="background: lightgray;"><td colspan="' + table + '" class="text-right"> CUMULATIVE </td><td class="text-right">'
                    + parseFloat($('#ContentPlaceHolder1_gvSalesKPICollectionDetails').find('.ordervalue').toArray().map(x => Number(x.innerHTML)).reduce((sum, x) => sum + x)).toFixed(2) + '</td></tr>')
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
                                                    <li class="active breadcrumb-item" aria-current="page"> Monthly Order Details </li>
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
                    <asp:PostBackTrigger ControlID="gvSalesKPICollectionDetails" />
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
                                        <div class="col-sm-12">
                                            <asp:GridView ID="gvSalesKPICollectionDetails" runat="server"
                                                CssClass="table table-bordered table-hover no-more-tables"
                                                AutoGenerateColumns="false">
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
                                                    <asp:TemplateField HeaderText="RFP No" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRFPNo" runat="server" Text='<%# Eval("RFPNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Value" HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblamount" CssClass="ordervalue" runat="server" Text='<%# Eval("Amount")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Collected By" HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcollectedby" CssClass="ordervalue" runat="server" Text='<%# Eval("CollectedBy")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                       <asp:TemplateField HeaderText="Received On" HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblreceivedon" CssClass="ordervalue" runat="server" Text='<%# Eval("ReceivedOn")%>'></asp:Label>
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

