<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="GeneralWorkOrderDC.aspx.cs" Inherits="Pages_GeneralWorkOrderDC" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" type="text/css" href="../Assets/css/datatables2.css" />
    <script type="text/javascript" src="../Assets/scripts/datatables2.js"></script>
     <style type="text/css">
        .radio-success label:nth-child(2) {
            color: red !important;
        }

        .radio-success label:nth-child(4) {
            color: #0acc0a !important;
        }

    </style>
   <script type="text/javascript">
       function showDataTable() {
           $('#<%=gvGeneralWorkOrderDCPending.ClientID %>').DataTable({
                'aoColumnDefs': [{
                    'bSortable': false,
                    'aTargets': [-1] /* 1st one, start by the right */
                }]
            });
        }

       function rblChange() {

       }

       function PrintDC(index) {
           alert(index);
           __doPostBack("PrintDC", index);
           return false;
       }

       function POPrint(index) {
           __doPostBack("PrintPO", index);
           return false;
       }
       
   </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                                    <h3 class="page-title-head d-inline-block">General Work Order DC</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Purchase</a></li>
                                <li class="active breadcrumb-item" aria-current="page">General Work Order DC</li>
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
            <asp:UpdatePanel ID="upQuote" runat="server">
                <Triggers>
                    <%--<asp:PostBackTrigger ControlID="gvVendorDC" />--%>
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                     
                         <div id="divAddNew" runat="server">
                            <div class="ip-div text-center">
                            </div>
                        </div>
                          <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4 text-right">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                DC Date
                                            </label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtDCDate" runat="server" CssClass="form-control mandatoryfield datepicker"
                                                ToolTip="Enter DC Date" autocomplete="nope" placeholder="Enter DC Date">
                                            </asp:TextBox>
                                            <asp:TextBox ID="txtGDCID" runat="server" CssClass="form-control" Visible="false">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label ">
                                                Location Name : 
                                            </label>
                                          </div>
                                        <div>
                                            <asp:DropDownList ID="ddlLocationName" runat="server" CssClass="form-control mandatoryfield"
                                                Width="70%" ToolTip="Select Location Name" Enabled="False">
                                            </asp:DropDownList>
                                             <asp:Label ID="txtGWOIDA" runat="server" CssClass="form-control" Visible="false">
                                            </asp:Label>
                                        </div>
                                    </div>

                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label">
                                                E-Way Bill No
                                            </label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtFormJJNNo" runat="server" CssClass="form-control"
                                                ToolTip="Enter E-Way Bill No" autocomplete="nope" placeholder="Enter E-Way Bill No">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 text-right">
                                        <div class="text-left">
                                            <label class="form-label">
                                                E-Way Bill Date
                                            </label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtDutyDetailsDate" runat="server" CssClass="form-control datepicker"
                                                ToolTip="Enter Duty Details date" autocomplete="nope" placeholder="Enter Duty Details date">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Duration ( Days )
                                            </label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtDuration" runat="server" CssClass="form-control mandatoryfield"
                                                Onkeypress="return fnAllowNumeric();" ToolTip="Enter Duration" autocomplete="nope"
                                                placeholder="Enter Duration">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 text-right">
                                        <div class="text-left">
                                            <label class="form-label">
                                                Tarrif Classification
                                            </label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtTarrifClassification" runat="server" CssClass="form-control"
                                                Onkeypress="return fnAllowNumeric();" ToolTip="Enter Taarif Classification" autocomplete="nope"
                                                placeholder="Enter Tarrif Classsification">
                                            </asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-20">
                                    <asp:LinkButton ID="btnSaveDC" runat="server" Text="Save" CssClass="btn btn-cons btn-save AlignTop"
                                        OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divInput');" OnClick="btnSaveDC_Click"></asp:LinkButton>
                                    <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                        CssClass="btn btn-cons btn-danger AlignTop"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                         <div id="divRadio" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-2">
                                        <label class="form-label" style="padding-top: 7px;">
                                            Select Type
                                        </label>
                                    </div>
                                    <div class="col-sm-8">
                                        <asp:RadioButtonList ID="rblGWPONoChange" runat="server" CssClass="radio radio-success" RepeatDirection="Horizontal"
                                            AutoPostBack="true" onchange="rblChange();" OnSelectedIndexChanged="rblGWPONoChange_OnSelectedChanged"
                                            RepeatLayout="Flow">
                                            <asp:ListItem Selected="True" Value="0">PO NOT APPROVED</asp:ListItem>
                                            <asp:ListItem Value="1">PO APPROVED</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server" >
                              
                        <div class="col-sm-12 p-t-10" style="overflow-x:auto;">
                                            
                                            <asp:GridView ID="gvGeneralWorkOrderDCPending" runat="server" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" OnRowCommand="gvGeneralWorkOrderDCPending_RowCommand" 
                                                CssClass="table table-hover table-bordered medium" Font-Names="arial" Font-Size="12px" DataKeyNames="SGWOID,GWOID,GWPOID"  >
                                                <Columns>
                                                      <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Add New DC" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                           <asp:LinkButton ID="btnAddNew" runat="server" Visible='<%# Eval("BStatus").ToString() == "1" ? true : false %>'
                                                               CommandArgument='<%# Container.DataItemIndex %>'
                                    CommandName="btnAddNew_Click" ><img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                  
                                                    <asp:TemplateField HeaderText="PO Status" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPOStatus" runat="server" Text='<%# Eval("POStatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Indent No" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblGWPOID" runat="server" Text='<%# Eval("GWPOID")%>' Visible="false"></asp:Label>
                                                             <asp:Label ID="lblIndentNo" Text=' <%# Eval("GWI")%>' runat="server"></asp:Label>
                                                               
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Vendor Name" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVendorName" runat="server" Text='<%# Eval("VendorName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Location" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LocationID" runat="server" Text='<%# Eval("LocationID")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("Location")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Indent Date" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGWOIndentDate" runat="server" Text='<%# Eval("GWOIndentDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Delivery Date" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDeliveryDate" runat="server" Text='<%# Eval("DeliveryDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Level 1" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblL1" runat="server" Text='<%# Eval("L1")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="L1 Approved Date" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblL1ApprovedDate" runat="server" Text='<%# Eval("L1 Approved Date")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Level 2" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblL2" runat="server" Text='<%# Eval("L2")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="L2 Approved Date" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblL2ApprovedDate" runat="server" Text='<%# Eval("L2 Approved Date")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                        </div>
                         <asp:HiddenField ID="hdnSGWOID" Value="0" runat="server" />
                   <asp:HiddenField ID="hdnGWOID" Value="0" runat="server" />
                   <asp:HiddenField ID="hdnGWPOID" Value="0" runat="server" />
                                    </div>

                         <div id="divDcData" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                            </div>
                                            <div class="col-md-6 ex-icons" id="div6" runat="server" visible="false">
                                            </div>
                                        </div>

                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvDcData"  runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                OnRowCommand="gvDcData_RowCommand" 
                                                 CssClass="table table-hover table-bordered medium" Font-Names="arial" Font-Size="12px" DataKeyNames="GDCID">
                                               <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'
                                                                Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DC No" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDCNo" runat="server" Text='<%# Eval("DCNo")%>' Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DC Date" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDCDate" runat="server" Text='<%# Eval("DCDate")%>' Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="E-Way Bill No" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFormJJNo" runat="server" Text='<%# Eval("FormJJNo")%>' Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tarrif Classification" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTarrifClassification" runat="server" Text='<%# Eval("TariffClassification")%>'
                                                                Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Duration" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDuration" runat="server" Text='<%# Eval("Duration")%>' Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="E-way Bill Date" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDutyDetailsDate" runat="server" Text='<%# Eval("DutyDetailsDate")%>'
                                                                Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Location" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                           <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("Location")%>' Style="text-align: center"></asp:Label>
                                                            
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="RM QTY" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                         
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Value" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                         
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# Container.DataItemIndex %>'
                                                                Style="text-align: center" CommandName="EditDC">
                                                            <img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Add Job" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                           <asp:LinkButton ID="btnAdd" runat="server" Text="Add DC" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                    OnClick="btnAdd_Click" ><img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Print DC">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnPDF" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                OnClientClick='<%# string.Format("return PrintDC({0});",((GridViewRow) Container).RowIndex) %>'>  <img src="../Assets/images/pdf.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="PO Print">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnPOPrint" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                OnClientClick='<%# string.Format("return POPrint({0});",((GridViewRow) Container).RowIndex) %>'>
                                                                <img src="../Assets/images/pdf.png" />
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                             <asp:HiddenField ID="hdnGDCID" Value="0" runat="server" />
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

