<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="MonthWiseStockReportDetails.aspx.cs" Inherits="Pages_MonthWiseStockReportDetails" ClientIDMode="Predictable" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%-- <link rel="stylesheet" type="text/css" href="../Assets/css/datatables2.css" />
<script type="text/javascript" src="../Assets/scripts/datatables2.js"></script>--%>
    <script type="text/javascript">

        function CalculateStockCost(ele) {
            $('#ContentPlaceHolder1_gvMonthlyStockReportCostDetails').find('.openingvalue').each(function () {

            });
        }

        function checkAllItems(ele) {
            debugger;
            if ($(ele).is(":checked")) {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', true);
            }
            else {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', false);
            }
        }

        function validateCheckRows() {
            if ($('#ContentPlaceHolder1_gvMonthlyStockReportCostDetails').find('[type="checkbox"]:checked').not('#ContentPlaceHolder1_gvMonthlyStockReportCostDetails_chkall').length > 0) {
                return true;
            }
            else {
                ErrorMessage('Error', 'No Location Selected');
                hideLoader();
                return false;
            }
        }

    </script>

    <style type="text/css">
        table#ContentPlaceHolder1_gvMonthlyStockReportCostDetails td {
            color: #000;
            font-weight: bold;
        }
    </style>

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
                                    <h3 class="page-title-head d-inline-block">Month Wise Stock Report Details </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Reports</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Month Wise Stock Report Details</li>
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
                    <asp:PostBackTrigger ControlID="btnMonthlyConsumption" />
                    <asp:PostBackTrigger ControlID="btnMonthlyInward" />
                    <asp:PostBackTrigger ControlID="imgExcel" />
                    <asp:PostBackTrigger ControlID="btnMonthlyOpenningStock" />
                    <asp:PostBackTrigger ControlID="btnStockPending" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divInput" runat="server">
                            <div class="ip-div text-left">
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-center">
                                        <label class="form-label" style="color: brown;">Select Year </label>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlYear_SelectIndexChanged" Width="70%" ToolTip="Select Year">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-center">
                                        <label class="form-label" style="color: brown;">Select Month Year </label>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlMonthYear" runat="server" AutoPostBack="true" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlMonthYear_SelectIndexChanged" Width="70%" ToolTip="Select Month Year">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>
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
                                                    OnClientClick="return validateCheckRows();" OnClick="btnExcelDownload_Click" />
                                            </div>
                                        </div>
                                        <div class="col-sm-12 text-center">
                                            <div class="col-sm-3">
                                                <asp:LinkButton ID="btnMonthlyOpenningStock" runat="server" Text="Monthly Opening"
                                                    OnClick="btnMonthlyOpenningStock_Click" CssClass="btn btn-cons btn-success"></asp:LinkButton>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:LinkButton ID="btnMonthlyConsumption" runat="server" Text="Monthly Consumption"
                                                    OnClick="btnMonthlyConsumption_Click" CssClass="btn btn-cons btn-success"></asp:LinkButton>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:LinkButton ID="btnStockPending" runat="server" Text="Stock Pending Inward"
                                                    OnClick="btnStockPending_Click" CssClass="btn btn-cons btn-success"></asp:LinkButton>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:LinkButton ID="btnMonthlyInward" runat="server" Text="Monthly Stock Inward"
                                                    OnClick="btnMonthlyInward_Click" CssClass="btn btn-cons btn-success"></asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvMonthlyStockReportCostDetails" runat="server"
                                                CssClass="table table-bordered table-hover no-more-tables pagingfalse"
                                                AutoGenerateColumns="false" DataKeyNames="CID,LocationID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-Width="15%">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkall" runat="server" onclick="return checkAllItems(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkQC" onclick="return CalculateStockCost(this);" runat="server" AutoPostBack="false" />
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="S.No" HeaderStyle-CssClass="text-center" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Location" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left" HeaderStyle-Width="15%"
                                                        ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("Location")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Category Name" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left" HeaderStyle-Width="20%"
                                                        ItemStyle-Width="20%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("CategoryName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Opening Value" ItemStyle-CssClass="text-right" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblopeneningstock" runat="server" CssClass="openingvalue" Text='<%# Eval("OpeningStock")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Inward Value" ItemStyle-CssClass="text-right" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblinwardvalue" runat="server" CssClass="inwardvalue" Text='<%# Eval("InwardCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Consumption Value" ItemStyle-CssClass="text-right" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblconsumptionvalue" runat="server" CssClass="consumptionvalue" Text='<%# Eval("ConsumptionCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Closing Stock" ItemStyle-CssClass="text-right" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblclosingstock" runat="server" CssClass="closingvalue" Text='<%# Eval("ClosingStock")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                        </FooterTemplate>
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


