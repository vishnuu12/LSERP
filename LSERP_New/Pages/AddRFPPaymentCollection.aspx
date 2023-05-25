<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="AddRFPPaymentCollection.aspx.cs" Inherits="Pages_AddRFPPaymentCollection" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" type="text/css" href="../Assets/css/datatables2.css" />
    <script type="text/javascript" src="../Assets/scripts/datatables2.js"></script>
    <script type="text/javascript">

        // A $( document ).ready() block.
        $(document).ready(function () {
            $("#ContentPlaceHolder1_gvColectionDetails").DataTable();
        });
    </script>
        <style type="text/css">
        table#ContentPlaceHolder1_gvColectionDetails td {
            color: #000;
            font-weight: bold;
            font-size: small;
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
                                    <h3 class="page-title-head d-inline-block"> Invoice Payment Update </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Sales</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Payment Status Update</li>
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
            <asp:UpdatePanel ID="upQuote" runat="server" UpdateMode="Always">
                <Triggers>
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divInput" runat="server">
                            
                            <div id="divcollection" runat="server">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                    </div>
                                    <div class="col-sm-6 text-right">
                                    <asp:RadioButtonList ID="rblPaymenttype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblPaymenttype_OnSelectIndexChanged" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="radio radio-success">
                                        <asp:ListItem Value="Domestic" Selected="True"> DOMESTIC </asp:ListItem>
                                        <asp:ListItem Value="International"> INTERNATIONAL </asp:ListItem>
                                    </asp:RadioButtonList>
                                    </div>
                                    <div class="col-sm-2 text-right">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="mandatorylbl">Customer Name </label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlCustomerName_SelectIndexChanged"
                                            CssClass="form-control" ToolTip="Select Customer PO">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="mandatorylbl">Invoice No </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlInvoiceNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlInvoiceNo_OnSelectIndexChanged"
                                            CssClass="form-control mandatoryfield" ToolTip="Select Invoice No">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="mandatorylbl">
                                            Invoice Value
                                        </label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:Label ID="lblInvoiceValue" Style="color: darkgreen; font-weight: bold; font-size: large;" runat="server"> </asp:Label>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="mandatorylbl">
                                            Received Amt
                                        </label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:Label ID="lblpaidamt" Style="display: block; color: darkgreen; font-weight: bold; font-size: large;"
                                            runat="server"> </asp:Label>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="mandatorylbl">
                                            balance Amount
                                        </label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:Label ID="lblbalanceamt" Style="color: darkgreen; font-weight: bold; font-size: large;" runat="server"> </asp:Label>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="mandatorylbl">
                                            Amount
                                        </label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:TextBox ID="txtCollectionAmount" runat="server" onkeypress="return validationDecimal();" CssClass="form-control mandatoryfield"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div id="divTDS" runat="server" visible="true">
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4 text-right">
                                            <label class="mandatorylbl">If Anything TDS Deducted </label>
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtTDSDeducted" runat="server" onkeypress="return validationDecimal();"
                                                CssClass="form-control mandatoryfield"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                </div>

                                <div id="divExchangerate" runat="server" visible="false">
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4 text-right">
                                            <label class="mandatorylbl">Currency Exchange Rate </label>
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtCurrencyExchangerate" runat="server" onkeypress="return validationDecimal();"
                                                CssClass="form-control mandatoryfield"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="mandatorylbl">Payment Mode </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlPaymentMode" runat="server" AutoPostBack="false"
                                            CssClass="form-control mandatoryfield" ToolTip="Select Payment Mode">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label>Ref No </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtrefNo"
                                            CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label>Payment Date </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtpaymentdate"
                                            CssClass="form-control datepicker" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label>Remarks </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtPaymentstatusremarks" Rows="3" TextMode="MultiLine"
                                            CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4">
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:LinkButton ID="btnCollection" CssClass="btn btn-success" runat="server"
                                            Text="Save Collection" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divcollection');"
                                            OnClick="btnCollection_Click"></asp:LinkButton>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divOutput" runat="server">
                                            <div class="text-center col-sm-12 p-t-10">
                                                <label style="font-size: 20px;">Invoice Payment Collection Details </label>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <asp:GridView ID="gvColectionDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No Records Found" OnRowDataBound="gvColectionDetails_OnrowDataBound" OnRowCommand="gvColectionDetails_OnrowCommand"
                                                    CssClass="table table-bordered table-hover medium" DataKeyNames="PCDID">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invoice No" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblinvoiceno" runat="server" Text='<%# Eval("InvoiceNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Account Name" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblaccountname" runat="server" Text='<%# Eval("AccountName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Value" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblamount" runat="server" Text='<%# Eval("Amount")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TDS Dedeucted" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTDSDedeucted" runat="server" Text='<%# Eval("TDSDeducted")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Currency Exchange rate" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcurrencyexchangerate" runat="server" Text='<%# Eval("CurrencyExchangerate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblinvoiceremarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invoice Type" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblInvoiceType" runat="server" Text='<%# Eval("InvoiceType")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                       <%--  <asp:TemplateField HeaderText="Advance Invoice No" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbladdinvoiceno" Text='<%# Eval("AdvanceInvoiceNo")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDeletepayment" runat="server" Text="Add" CommandName="DeleteCollectionPayment"
                                                                    CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                                <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>

                                                <asp:HiddenField ID="hdnPCDID" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdnIDID" runat="server" Value="0" />
                                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="modal" id="mpeCollectionPopup" style="overflow-y: scroll;">
        <div style="max-width: 81%; padding-left: 10%; padding-top: 5%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">Payment Status Update </h4>
                            <button type="button" class="close btn-primary-purple"
                                data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                        </div>
                        <div class="modal-footer">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

</asp:Content>

