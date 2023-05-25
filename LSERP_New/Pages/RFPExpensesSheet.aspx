<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="RFPExpensesSheet.aspx.cs"
    Inherits="Pages_RFPExpensesSheet" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        table {
            color: #000;
            font-weight: bold;
        }

        .blink_me {
            animation: blinker 1s linear infinite;
        }

        @keyframes blinker {
            50% {
                opacity: 0;
            }
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
                                    <h3 class="page-title-head d-inline-block">RFP Expenses Sheet </h3>
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
                    <asp:PostBackTrigger ControlID="gvRFPExpensesDetails" />
                    <asp:PostBackTrigger ControlID="imgExcel" />
                    <asp:PostBackTrigger ControlID="btnExcel" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" class="btnAddNew" runat="server">
                            <div class="text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right p-t-5">
                                        <label class="form-label">
                                            RFP No
                                        </label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlRFPNo" runat="server" AutoPostBack="true" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlRFPNo_SelectIndexChanged" Width="70%" ToolTip="Select RFP No">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="col-sm-12 p-t-10" style="background: greenyellow;">
                                            <asp:GridView ID="gvRFPExpensesDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                OnRowCommand="gvRFPExpensesDetails_OnRowCommand"
                                                EmptyDataText="No Records Found" CssClass="table table-hover orderingfalse table-bordered medium">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="RFP No" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnRFPNo" CssClass="blink_me" runat="server" Text='<%# Eval("RFPNo")%>' CommandName="ViewItemWise"
                                                                CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="BOM Cost">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblbomcost" runat="server" Text='<%# Eval("TotalBOMCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="RFP Expenses">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRFPexpenses" runat="server" Text='<%# Eval("RFPExpenses")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="INR Value">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblinrvalue" runat="server" Text='<%# Eval("INRValue")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Currency Value">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcurrencyvalue" runat="server" Text='<%# Eval("CurrencyValue")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Order Value">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblordervalue" runat="server" Text='<%# Eval("OrderValue")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Invoice Value">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblinvoicevalue" runat="server" Text='0'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>


                                        <div class="col-sm-12 row">
                                            <div class="col-md-6">
                                            <label style="font-weight: bold; color: brown; font-size: 16px;">
                                                Material Expenses Details
                                            </label>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server">
                                                <asp:LinkButton ID="imgExcel" runat="server" CssClass="excel_bg" ToolTip="Excel Download"
                                                    OnClick="btnExcelDownload_Click" />
                                            </div>
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

                                                    <asp:TemplateField HeaderText="Item Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitemname" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
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
                                                            <asp:Label ID="lbltotalcost" runat="server" Text='<%# Eval("TotalCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                        <div class="col-sm-12 p-t-10 text-right">
                                            <asp:Label ID="lbltotalmtlexpenses" Style="text-align: center; color: green; font-weight: bold; font-size: 22px;"
                                                runat="server">  </asp:Label>
                                        </div>

                                        <div class="co-sm-12 p-t-10">
                                            <label style="font-weight: bold; color: brown; font-size: 16px;">
                                                Store Issue Details
                                            </label>
                                        </div>

                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvstoreissuedetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover orderingfalse table-bordered medium">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MRN No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIndentBy" runat="server" Text='<%# Eval("MRNNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Issued Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblissuedqty" runat="server" Text='<%# Eval("IssuedQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="RFP No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrfpno" runat="server" Text='<%# Eval("RFPNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit Cost">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblunitcost" runat="server" Text='<%# Eval("UnitQuoteCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Cost" HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltotalcost" runat="server" Text='<%# Eval("TotalCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                        <div class="col-sm-12 p-t-10 text-right">
                                            <asp:Label ID="lblgmissue" Style="text-align: center; color: green; font-weight: bold; font-size: 22px;"
                                                runat="server"></asp:Label>
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
                                                            <asp:Label ID="lbltotalcost" runat="server" Text='<%# Eval("TotalCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                        <div class="col-sm-12 p-t-10 text-right">
                                            <asp:Label ID="lblwpoexpenses" Style="text-align: center; color: green; font-weight: bold; font-size: 22px;" runat="server">  </asp:Label>
                                        </div>
										
										<div class="row" style="border: 2px solid red;border-radius: 12px;padding: 5px;">
                                       
                                        <div class="col-sm-12 row">
                                            <div class="col-md-6">
                                            <label style="font-weight: bold; color: brown; font-size: 16px;">Supplier PO Expenses </label>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="div1" runat="server">
                                                <asp:LinkButton ID="btnExcel" runat="server" CssClass="excel_bg" ToolTip="Excel Download"
                                                    OnClick="btnExcelDownload_ClickSupplier" />
                                            </div>
                                        </div>
                                        
                                        <div class="col-sm-12 p-t-10" style="background-color: hsla(0, 100%, 90%, 0.3);">
                                            <asp:GridView ID="gvsupplierpo" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MRNNumber">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMRNNumber" runat="server" Text='<%# Eval("MRNNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Indent No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblINDNo" runat="server" Text='<%# Eval("INDNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Part Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartName" runat="server" Text='<%# Eval("PartName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="RFP No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRFPNo" runat="server" Text='<%# Eval("RFPNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SPONumber">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSPONumber" runat="server" Text='<%# Eval("SPONumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PO Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPOQty" runat="server" Text='<%# Eval("POQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Inward Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblInwardQty" runat="server" Text='<%# Eval("InwardQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="UOM">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit Cost">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUnitCost" runat="server" Text='<%# Eval("UnitCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Cost" HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltotalcost" runat="server" Text='<%# Eval("TotalCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                        <div class="col-sm-12 p-t-10 text-right">
                                            <asp:Label ID="lbltotalsplexpenses" Style="text-align: center; color: green; font-weight: bold; font-size: 22px;" runat="server">  </asp:Label>
                                        </div>
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
                                                            <asp:Label ID="lbltotalcost" runat="server" Text='<%# Eval("TotalCost")%>'></asp:Label>
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

