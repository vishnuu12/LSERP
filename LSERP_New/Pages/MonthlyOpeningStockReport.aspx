<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="MonthlyOpeningStockReport.aspx.cs"
    Inherits="Pages_MonthlyOpeningStockReport" ClientIDMode="Predictable" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%-- <link rel="stylesheet" type="text/css" href="../Assets/css/datatables2.css" />
<script type="text/javascript" src="../Assets/scripts/datatables2.js"></script>--%>
    <script type="text/javascript">
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<Message:Uc ID="ucMessage" runat="server" />--%>
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
                                    <h3 class="page-title-head d-inline-block">Monthly Opening Stock Report Details </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Reports</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Monthly Opening Stock Report Details</li>
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
                                        <div class="row">
                                            <div class="col-md-6">
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server">
                                                <asp:LinkButton ID="imgExcel" runat="server" CssClass="excel_bg" ToolTip="Excel Download"
                                                    OnClick="btnExcelDownload_Click" />
                                            </div>
                                        </div>
                                        <%--  <th>CategoryName </th>
                                                        <th>MRN No</th>
                                                        <th>Type</th>
                                                        <th>Grade</th>
                                                        <th>LW</th>
                                                        <th>THK</th>
                                                        <th>RFP No</th>
                                                        <th>Location</th>
                                                        <th>Inward Qty</th>
                                                        <th>UOM</th>
                                                        <th>Blocked Qty</th>
                                                        <th>Consumed Qty</th>
                                                        <th>Actual Stock</th>
                                                        <th>InStock Qty</th>
                                                        <th>Unit Cost</th>
                                                        <th>Total Cost</th>--%>
                                        <div class="col-sm-12" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvMonthlyOpeningStockreportDetails" runat="server"
                                                CssClass="table table-bordered table-hover no-more-tables"
                                                AutoGenerateColumns="false">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" HeaderStyle-CssClass="text-center" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Category Name" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left" HeaderStyle-Width="15%"
                                                        ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcategoryname" runat="server" Text='<%# Eval("CategoryName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MRN No" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left" HeaderStyle-Width="15%"
                                                        ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMRNNo" runat="server" Text='<%# Eval("MRNNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Type" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left" HeaderStyle-Width="15%"
                                                        ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltype" runat="server" Text='<%# Eval("MaterialTypeName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Grade" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left" HeaderStyle-Width="15%"
                                                        ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGrade" runat="server" Text='<%# Eval("GradeName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="LW" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left" HeaderStyle-Width="15%"
                                                        ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLW" runat="server" Text='<%# Eval("Measurment")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="THK" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left" HeaderStyle-Width="15%"
                                                        ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTHK" runat="server" Text='<%# Eval("THKValue")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="RFP No" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left" HeaderStyle-Width="15%"
                                                        ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRFPNo" runat="server" Text='<%# Eval("RFPNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Location" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left" HeaderStyle-Width="15%"
                                                        ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbllocation" runat="server" Text='<%# Eval("LocationName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Total Cost" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left" HeaderStyle-Width="15%"
                                                        ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalCost" runat="server" Text='<%# Eval("TotalCost")%>'></asp:Label>
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
