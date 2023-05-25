<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="PaymentStatusUpdate.aspx.cs"
    Inherits="Pages_PaymentStatusUpdate"
    ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function AddInvoicePopUp() {
            $('#mpeinvoicePopup').modal('show');
            return false;
        }

        function AddCollectionPopup() {
            $('#mpeCollectionPopup').modal('show');
            return false;
        }

        function hideInvoicePopUp() {
            $('#mpeinvoicePopup').modal('hide');
            return false;
        }

        function hideCollectionPopup() {
            $('#mpeCollectionPopup').modal('hide');
            return false;
        }

        function addInvoiceNo() {
            $('#mpeAddInvoiceNo').modal('show');
            return false;
        }

    </script>


    <style type="text/css">
        .blink_me {
            animation: blinker 1s linear infinite;
        }

        @keyframes blinker {
            50% {
                opacity: 0;
            }
        }

        .radio-success {
            text-align: center;
            color: #000;
            font-weight: bold;
        }

            .radio-success label {
                color: brown;
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
                        <div id="divAdd" runat="server">
                            <div class="ip-div text-center">

                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Customer Name
                                        </label>
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
                                        <label class="form-label">
                                            RFP No
                                        </label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlRFPNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRFPNo_SelectIndexChanged"
                                            CssClass="form-control" ToolTip="Select Customer PO">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="mandatorylbl">
                                            Invoice Type
                                        </label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlinvoicetype" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlinvoicetype_OnSelectIndexChanged" CssClass="form-control mandatoryfield" ToolTip="Select Invoice No">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>

                            </div>
                        </div>

                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4">
                                        <asp:LinkButton ID="btnAddInvoice" CssClass="btn btn-success" runat="server"
                                            Text="Add Invoice" OnClientClick="return AddInvoicePopUp();"></asp:LinkButton>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:LinkButton ID="btnAddPayment" CssClass="btn btn-success" runat="server"
                                            Text="Add Payment" OnClientClick="return AddCollectionPopup();"></asp:LinkButton>
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
                                                <p class="h-style">
                                                    <asp:Label ID="lbltitle" runat="server"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                            <div class="col-sm-12 blink_me text-center">
                                                <label style="color: green; font-weight: bold; font-size: 20px;">
                                                    PO Total Value 
                                                </label>
                                                <asp:Label ID="lblPoTotalValue" Style="font-weight: bold; color: #000; font-size: 20px;"
                                                    runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-12 p-t-10 text-center">
                                                <label style="color: brown; font-size: 20px;">Invoice Details </label>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <asp:GridView ID="gvInvoiceDetails" OnRowCommand="gvInvoiceDetails_OnRowCommand" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium" DataKeyNames="IDID">
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

                                                        <asp:TemplateField HeaderText="Type Of Invoice" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltypeofinvoice" runat="server" Text='<%# Eval("TypeName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Invoice Date" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblinvoicedate" runat="server" Text='<%# Eval("InvoiceDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Basic Value" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblbasicvalue" runat="server" Text='<%# Eval("BasicValue")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invoice Value" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblinvoicevalue" runat="server" Text='<%# Eval("InvoiceValue")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Invoice Remarks" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblinvoiceremarks" runat="server" Text='<%# Eval("InvoiceRemarks")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Location" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbllocation" runat="server" Text='<%# Eval("Location")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="View File" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblviewfile" runat="server" Text='<%# Eval("InvoiceFileName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDeleteInvoice" runat="server" Text="Add" CommandName="DeleteInvoice"
                                                                    CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                               <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnEditPayment" runat="server" Text="Edit" CommandName="EditPaymentStatus"
                                                                    CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                               <img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="text-center col-sm-12 p-t-10">
                                                <label style="color: brown; font-size: 20px;">Collection Details </label>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <asp:GridView ID="gvColectionDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No Records Found" OnRowDataBound="gvColectionDetails_OnrowDataBound" OnRowCommand="gvColectionDetails_OnrowCommand"
                                                    CssClass="table table-hover table-bordered medium" DataKeyNames="PCDID">
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
                                                        <asp:TemplateField HeaderText="Invoice No" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbladdinvoiceno" Text='<%# Eval("AdvanceInvoiceNo")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Add InvoiceNo" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnaddinvoiceno" runat="server" Text="Add" CommandName="AddInvoiceNo"
                                                                    CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                                <img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
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

                                            <%--                                            <div class="col-sm-12 text-center">
                                                <label style="color: green; font-weight: bold; font-size: 20px;">
                                                    Total Invoice Value
                                                </label>
                                                <asp:Label ID="lbltotalinvoicevalue" Style="font-weight: bold; color: #000; font-size: 20px;"
                                                    runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-12 text-center">
                                                <label style="color: green; font-weight: bold; font-size: 20px;">
                                                    Total Collection Value
                                                </label>
                                                <asp:Label ID="lbltotalcollectionvalue" Style="font-weight: bold; color: #000; font-size: 20px;"
                                                    runat="server"></asp:Label>
                                            </div>--%>
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

    <div class="modal" id="mpeinvoicePopup" style="overflow-y: scroll;">
        <div style="max-width: 81%; padding-left: 10%; padding-top: 5%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="upView" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnPayment" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">Invoice  </h4>
                            <button type="button" class="close btn-primary-purple" onclick="ClosePartItemPopUP();"
                                data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="divPayment" runat="server">


                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="mandatorylbl">
                                            Invoice No
                                        </label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:TextBox ID="txtInvoiceNo" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="mandatorylbl">Invoice Date </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtinvoicedate" runat="server" CssClass="form-control datepicker mandatoryfield"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="mandatorylbl">Basic Value </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtbasicvalue" runat="server" onkeypress="return validationDecimal();" CssClass="form-control mandatoryfield"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="mandatorylbl">Invoice Value </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtinvoicevalue" runat="server" onkeypress="return validationDecimal();" CssClass="form-control mandatoryfield"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="mandatorylbl">Attachement </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:FileUpload ID="fbinvoiceaatch" CssClass="form-control mandatoryfield" runat="server" />
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label>Invoice Remarks </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtRemarks" Rows="3" TextMode="MultiLine"
                                            CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label>Location </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlLocation" CssClass="form-control mandatoryfield" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label>Payment Days </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlpaymentdays" CssClass="form-control mandatoryfield" runat="server">
                                            <asp:ListItem Text="--select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label>Payment Remarks </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtpaymentremarks" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4">
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:LinkButton ID="btnPayment" CssClass="btn btn-success" runat="server"
                                            Text="Save Invoice" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divPayment');" OnClick="btnPayment_Click"></asp:LinkButton>
                                        <asp:LinkButton ID="btninvoicecancel" CssClass="btn btn-success" runat="server"
                                            Text="Cancel" OnClick="btninvoicecancel_Click"></asp:LinkButton>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
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
                            <div id="divcollection" runat="server">
                                <div class="col-sm-12">
                                    <asp:RadioButtonList ID="rblPaymenttype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rblPaymenttype_OnSelectIndexChanged" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="radio radio-success">
                                        <asp:ListItem Value="Domestic" Selected="True"> DOMESTIC </asp:ListItem>
                                        <asp:ListItem Value="International"> INTERNATIONAL </asp:ListItem>
                                    </asp:RadioButtonList>
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
                        <div class="modal-footer">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>


    <div class="modal" id="mpeAddInvoiceNo" style="overflow-y: scroll;">
        <div style="max-width: 81%; padding-left: 10%; padding-top: 5%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">Invoice  </h4>
                            <button type="button" class="close btn-primary-purple" onclick="ClosePartItemPopUP();"
                                data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="div1" runat="server">

                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label>Invoice No </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlAddInvoiceNo" CssClass="form-control"
                                            runat="server">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4">
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:LinkButton ID="btnAddinvoiceNo" CssClass="btn btn-success" runat="server"
                                            Text="Save Invoice" OnClick="btnAddinvoiceNo_Click"></asp:LinkButton>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

</asp:Content>

