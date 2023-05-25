<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="EnquiryInformationDetails.aspx.cs"
    Inherits="Pages_EnquiryInformationDetails" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
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
                                    <h3 class="page-title-head d-inline-block">Enquiry Status Report</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Sales</a></li>
                                <li class="active breadcrumb-item" aria-current="page">Enquiry</li>
                            </ol>
                        </nav>
                        <a id="help" href="" alt="" style="margin-top: 4px;">
                            <img src="../Assets/images/help.png" />
                        </a>
                    </div>
                </div>
            </div>
        </div>

        <asp:UpdatePanel ID="upDocumenttype" runat="server">
            <Triggers>
            </Triggers>
            <ContentTemplate>
                <div class="card-container">
                    <div id="divAdd" runat="server">
                        <div class="ip-div text-center">
                            <asp:Label ID="lblheading_p" Style="color: brown; font-size: 25px; font-weight: bold;"
                                runat="server"></asp:Label>
                        </div>
                    </div>
                    <div id="divInput" runat="server">
                        <div class="ip-div text-center">
                        </div>
                    </div>
                    <div id="divOutput" class="output_section" runat="server">
                        <div class="page-container" style="float: left; width: 100%">
                            <div class="main-card">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <p class="h-style">
                                            </p>
                                        </div>
                                        <div class="col-md-6 ex-icons" id="divDownload" runat="server">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 text-left p-t-5">
                                        <label style="color: #000; font-size: 20px; font-weight: bold;">Item Details </label>
                                    </div>
                                    <div class="col-sm-12">
                                        <asp:GridView ID="gvItemDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                            HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Offer No" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOfferNo" runat="server" Text='<%# Eval("OfferNo")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Offer Rev No" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOfferNoRev" runat="server" Text='<%# Eval("OfferRev")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Name" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblitemname" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Size" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblitemsize" runat="server" Text='<%# Eval("ItemSize")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DRG Rev No" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldrgrevno" runat="server" Text='<%# Eval("DrawingRevision")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DRG Name" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldrawingname" runat="server" Text='<%# Eval("DrawingName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BOM Status" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBOMStatus" runat="server" Text='<%# Eval("BOMStatus")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cost Status" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsalescoststatus" runat="server" Text='<%# Eval("SalesCostStatus")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="BOM Approved On" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbomapprovedOn" runat="server" Text='<%# Eval("BOMApprovedOn")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Final Cost Approved On" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblfinalcostapprovedOn" runat="server" Text='<%# Eval("FinalCostApprovedOn")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Final Cost" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFinalCost" runat="server" Text='<%# Eval("FinalCost")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:HiddenField ID="hdnEnquiryTypeID" runat="server" Value="0" />
                                    </div>
                                    <div class="col-sm-12 text-left p-t-5">
                                        <label style="color: #000; font-size: 20px; font-weight: bold;">Offer Revision Details </label>
                                    </div>
                                    <div class="col-sm-12">
                                        <asp:GridView ID="gvOfferRev" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                            HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Offer No" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOfferNo" runat="server" Text='<%# Eval("OfferNo")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Offer No Rev" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOfferNo" runat="server" Text='<%# Eval("OfferNoRevision")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Offer Cost" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="offercost" runat="server" Text='<%# Eval("OfferCost")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Offer Dispatch Status" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="offerdispatch" runat="server" Text='<%# Eval("OfferDispatch")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Offer Generated On" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbloffergeneratedOn" runat="server" Text='<%# Eval("OfferGeneratedOn")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Generated By" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbloffergeneratedBy" runat="server" Text='<%# Eval("OfferGeneratedBy")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Offer Dispatch Date" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblofferdispatchOn" runat="server" Text='<%# Eval("DispatchOn")%>'></asp:Label>
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
</asp:Content>

