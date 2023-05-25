<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="RFPItemExpensesSheet.aspx.cs"
    Inherits="Pages_RFPItemExpensesSheet" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">

        function AddFooter() {
            $('#ContentPlaceHolder1_gvItemRFPExpensesSheet').find('tbody').
                append('<tr><td colspan="7" class="text-right"></td><td class="text-right">'
                    + parseFloat($('#ContentPlaceHolder1_gvItemRFPExpensesSheet').find('.totalitembomcost').toArray().map(x => Number(x.innerHTML)).reduce((sum, x) => sum + x)).toFixed(2) +
                    '</td><td class="text-right">'+ parseFloat($('#ContentPlaceHolder1_gvItemRFPExpensesSheet').find('.contractorexpenses').toArray().map(x => Number(x.innerHTML)).reduce((sum, x) => sum + x)).toFixed(2) +'</td><td class="text-right">'+ parseFloat($('#ContentPlaceHolder1_gvItemRFPExpensesSheet').find('.materialexpenses').toArray().map(x => Number(x.innerHTML)).reduce((sum, x) => sum + x)).toFixed(2) +'</td><td class="text-right">'+ parseFloat($('#ContentPlaceHolder1_gvItemRFPExpensesSheet').find('.workorderexpenses').toArray().map(x => Number(x.innerHTML)).reduce((sum, x) => sum + x)).toFixed(2) +'</td><td class="text-right">'
                    + parseFloat($('#ContentPlaceHolder1_gvItemRFPExpensesSheet').find('.totalitemexpensecost').toArray().map(x => Number(x.innerHTML)).reduce((sum, x) => sum + x)).toFixed(2) +
                    '</td></tr>')

            $('table').find('.totalitemexpensecost').closest('td').css('background', 'violet')
            $('table').find('.totalitembomcost').closest('td').css('background', 'greenyellow')
        }

    </script>

    <style type="text/css">
        table {
            color: #000;
            font-weight: bold;
        }

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
                                    <h3 class="page-title-head d-inline-block">Item RFP Expenses Sheet </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Reports</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page"> RFP Expenses Sheet </li>
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
                    <asp:PostBackTrigger ControlID="gvItemRFPExpensesSheet" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" class="btnAddNew" runat="server">
                            <div class="text-center">
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="col-sm-12 text-center p-t-10">
                                            <asp:Label ID="lblrfpno" Style="font-weight: bold; color: brown; font-size: 20px;"
                                                runat="server"> </asp:Label>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvItemRFPExpensesSheet" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" OnRowCommand="gvItemRFPExpensesSheet_OnrowCommand"
                                                CssClass="table table-hover orderingfalse table-bordered medium" DataKeyNames="RFPDID,ItemName">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Name" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnRFPNo" CssClass="blink_me" runat="server" CommandName="ViewItemExpenses"
                                                                CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                                <asp:Label ID="lblitemname" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Material Cost">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmaterialcost" runat="server" Text='<%# Eval("MCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Labour Cost">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbllabourcost" runat="server" Text='<%# Eval("LCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="M/C Cost">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmccost" runat="server" Text='<%# Eval("MeachningCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Unit BOM Cost">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblunitbomcost" runat="server" Text='<%# Eval("UnitBOmCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblqty" runat="server" Text='<%# Eval("QTY")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total BOM Cost" HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltotalbomcost" CssClass="totalitembomcost" runat="server" Text='<%# Eval("TotalBOMCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Contract Expense" HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcurrencyvalue" runat="server" CssClass="contractorexpenses" Text='<%# Eval("ContractorExpense")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Material Expense" HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmaterialexpenses" runat="server" CssClass="materialexpenses" Text='<%# Eval("MaterialExpenses")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Work Order Expense" HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblinvoicevalue" runat="server" CssClass="workorderexpenses" Text='<%# Eval("WorkOrderExpense")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Expense Cost" HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltotalexpensecost" CssClass="totalitemexpensecost" runat="server" Text='<%# Eval("TotalExpenseCost")%>'></asp:Label>
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

