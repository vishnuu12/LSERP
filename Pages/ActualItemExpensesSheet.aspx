<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="ActualItemExpensesSheet.aspx.cs"
    Inherits="Pages_ActualItemExpensesSheet" ClientIDMode="Predictable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">

        function AddFooter() {

            let colspan = "0";

            const t1 = $('#ContentPlaceHolder1_gvJobCardDetails').find('tr').toArray().map(x => x.cells.length);
            colspan = t1[0] - 1;
            $('#ContentPlaceHolder1_gvJobCardDetails').find('tbody').append('<tr><td colspan="' + colspan + '" class="text-right" style="color: forestgreen;font-weight: bold;">Total Material Expenses For The Item </td><td class="text-right">'
                    + parseFloat($('#ContentPlaceHolder1_gvJobCardDetails').find('.materialexpensestotal').toArray().map(x => Number(x.innerHTML)).reduce((sum, x) => sum + x)).toFixed(2) +
                    '</td></tr>')

            const t2 = $('#ContentPlaceHolder1_gvworkorderpo').find('tr').toArray().map(x => x.cells.length);
            colspan = t2[0] - 1;
            $('#ContentPlaceHolder1_gvworkorderpo').find('tbody').append('<tr><td colspan="' + colspan + '" class="text-right" style="color: forestgreen;font-weight: bold;">Total Work Order Expenses For The Item </td><td class="text-right">'
                    + parseFloat($('#ContentPlaceHolder1_gvworkorderpo').find('.wpoexpensestotal').toArray().map(x => Number(x.innerHTML)).reduce((sum, x) => sum + x)).toFixed(2) +
                    '</td></tr>')

            const t3 = $('#ContentPlaceHolder1_gvContractorexpensesdetails').find('tr').toArray().map(x => x.cells.length);
            colspan = t3[0] - 1;
            $('#ContentPlaceHolder1_gvContractorexpensesdetails').find('tbody').append('<tr><td colspan="' + colspan + '" class="text-right" style="color: forestgreen;font-weight: bold;">Total Contractor Expenses For The Item </td><td class="text-right">'
                    + parseFloat($('#ContentPlaceHolder1_gvContractorexpensesdetails').find('.totalcontractorexpenses').toArray().map(x => Number(x.innerHTML)).reduce((sum, x) => sum + x)).toFixed(2) +
                    '</td></tr>')
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
                                        <img src="../Assets/images/pages.png" alt="" width="16px" />
                                    </span>
                                    <h3 class="page-title-head d-inline-block">Item Expenses Sheet </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Reports</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page"> Item Expenses Sheet </li>
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
                            <div class="text-center">
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="col-sm-12 text-center p-t-10">
                                            <asp:Label ID="lblrfpnoitemname" Style="font-weight: bold; color: brown; font-size: 20px;"
                                                runat="server"> </asp:Label>
                                        </div>
                                        <div class="co-sm-12 p-t-10">
                                            <label style="font-weight: bold; color: brown; font-size: 16px;padding-left: 16px;">
                                                Material Expenses Details
                                            </label>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvJobCardDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover orderingfalse table-bordered medium">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Job Card No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJobCardNo" runat="server" Text='<%# Eval("JobCardNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Part Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpartname" runat="server" Text='<%# Eval("PartName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MRN No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMRNNo" runat="server" Text='<%# Eval("MRNNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Issue Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIssueqty" runat="server" Text='<%# Eval("ISSUEDWEIGHT")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Issued On">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblissuedon" runat="server" Text='<%# Eval("MRNIssuedOn")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Unit Cost">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblunitcost" runat="server" Text='<%# Eval("UnitQuoteCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Total Cost" HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltotalcost" CssClass="materialexpensestotal" runat="server" Text='<%# Eval("TotalCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="col-sm-12 p-t-10 text-right">
                                            <asp:Label ID="lbltotalmtlexpenses" Style="text-align: center; color: green; font-weight: bold; font-size: 22px;"
                                                runat="server">  </asp:Label>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <label style="font-weight: bold; color: brown; font-size: 16px;">Work Order PO Expenses </label>
                                        </div>

                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvworkorderpo" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover orderingfalse table-bordered medium">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Indent By">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIndentBy" runat="server" Text='<%# Eval("IndentBy")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Indent No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblindentno" runat="server" Text='<%# Eval("IndentNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="WPO No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblwpono" runat="server" Text='<%# Eval("PONo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PO Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpoqty" runat="server" Text='<%# Eval("POQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit Cost">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUnitCost" runat="server" Text='<%# Eval("UnitCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Cost" HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltotalcost" CssClass="wpoexpensestotal" runat="server" Text='<%# Eval("TotalCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                        <div class="col-sm-12 p-t-10 text-right">
                                            <asp:Label ID="lblwpoexpenses" Style="text-align: center; color: green; font-weight: bold; font-size: 22px;" runat="server">  </asp:Label>
                                        </div>

                                        <div class="col-sm-12 p-t-10">
                                            <label style="font-weight: bold; color: brown; font-size: 16px;">Job Card Contractor Expenses Details </label>
                                        </div>

                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvContractorexpensesdetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover orderingfalse table-bordered medium">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Job Card No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljobcardno" runat="server" Text='<%# Eval("JobCardNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Contractor Team Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcontractorteamname" runat="server" Text='<%# Eval("ContractorTeamName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="User Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblusername" runat="server" Text='<%# Eval("UserName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpoqty" runat="server" Text='<%# Eval("Qty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit Cost">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUnitCost" runat="server" Text='<%# Eval("UnitCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Cost" HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltotalcost" CssClass="totalcontractorexpenses" runat="server" Text='<%# Eval("TotalCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                        <div class="col-sm-12 p-t-10 text-right">
                                            <asp:Label ID="lbljocardratedetails" Style="text-align: center; color: green; font-weight: bold; font-size: 22px;" runat="server">  </asp:Label>
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

