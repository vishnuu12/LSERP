<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" 
    CodeFile="gvOverAllSupplierEvaluvationReports.aspx.cs" Inherits="Pages_gvOverAllSupplierEvaluvationReports" ClientIDMode="Predictable"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <script type="text/javascript">
      </script>
    <style type="text/css">
        table td {
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
                                    <h3 class="page-title-head d-inline-block">Over All Supplier Evaluvation Report </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Reports</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page"> Over All Supplier Evaluvation Report</li>
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
                    <asp:PostBackTrigger ControlID="gvOverAllSupplierEvaluvationReports" />
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
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <label class="form-label mandatorylbl">
                                                    From Date</label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="mandatoryfield form-control datepicker"
                                                    PlaceHolder="Enter From Date" ToolTip="Enter From Date" AutoComplete="off"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <label class="form-label mandatorylbl">
                                                    To Date</label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtToDate" runat="server" placeholder="Enter To Date" ToolTip="Enter Deadline Date"
                                                    AutoComplete="off" CssClass="mandatoryfield form-control datepicker"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10 text-center">
                                            <asp:LinkButton ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divOutput');" OnClick="btnSubmit_Click"
                                                CssClass="btn btn-cons btn-success" />
                                        </div>

                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-6">
                                            </div>
                                            <div class="col-sm-6 text-right">
                                                <asp:LinkButton ID="imgExcel" runat="server" CssClass="excel_bg" ToolTip="Excel Download"
                                                    OnClick="btnExcelDownload_Click" />
                                            </div>
                                        </div>

                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvOverAllSupplierEvaluvationReports" runat="server" AutoGenerateColumns="false" 
                                                CssClass="table table-bordered table-hover no-more-tables pagingfalse"
                                                ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found"  DataKeyNames="SUPID">
                                                <Columns>
                                                 <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                           <asp:TemplateField HeaderText="Supplier Name">
                                                        <ItemTemplate>
                                                          
                                                                <asp:Label ID="lblSupplierName" runat="server" Text='<%# Eval("SupplierName")%>'></asp:Label>
                                                         
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PO No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPONo" runat="server" Text='<%# Eval("PONo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PO Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPODate" runat="server" Text='<%# Eval("PODate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PO Delivery Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPODeliveryDate" runat="server" Text='<%# Eval("DeliveryDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Inward Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblinwarddate" runat="server" Text='<%# Eval("InwardDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MRN No" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMRNNo" runat="server" Text='<%# Eval("MRNNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No Of Days Delay">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNoOfDaysDelay" runat="server" Text='<%# Eval("NoOfDaysDelay")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quality (50)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQuality" runat="server" Text='<%# Eval("Quality")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delivery Mark (25)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDeliveryMark" runat="server" Text='<%# Eval("DeliveryMark")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Packing Mark (5)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpackingmark" runat="server" Text='<%# Eval("PackingMark")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vendor IDF Mark (5)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblvendoridfmark" runat="server" Text='<%# Eval("VendorIDFMark")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Visual Check Mark (10)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDeliveryMark" runat="server" Text='<%# Eval("VisualCheckMark")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Supplier Support Mark (5)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSupplierSupportMark" runat="server" Text='<%# Eval("SupplierSupportMark")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Mark">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalmark" runat="server" Text='<%# Eval("Totalmark")%>'></asp:Label>
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

