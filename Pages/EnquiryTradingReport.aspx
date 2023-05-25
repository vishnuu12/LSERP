<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="EnquiryTradingReport.aspx.cs" Inherits="Pages_EnquiryTradingReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function Validate() {
            if (Mandatorycheck('ContentPlaceHolder1_divAdd')) {
                var FromDate = $('#<%=txtFromDate.ClientID %>').val();
                var ToDate = $('#<%=txtToDate.ClientID %>').val();
                var FDparts = FromDate.split("/");
                var TDParts = ToDate.split("/");
                var FD = new Date(FDparts[1] + "/" + FDparts[0] + "/" + FDparts[2]);
                var TD = new Date(TDParts[1] + "/" + TDParts[0] + "/" + TDParts[2]);
                if (FD > TD) {
                    $('#<%=txtToDate.ClientID %>').notify('The To Date Should Be greater Than From Date.', { arrowShow: true, position: 'r,r', autoHide: true });
                    return false;
                }
                else {
                    return true;
                }
            }
            else {
                return false;
            }
        }

    </script>
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
                                    <h3 class="page-title-head d-inline-block">
                                        Enquiry trading Report Details</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Sales</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Enquiry Trading Report</li>
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
                    <%-- <asp:PostBackTrigger ControlID="btnSave" />--%>
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" class="btnAddNew" runat="server">
                            <div class="text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right p-t-5">
                                        <label class="form-label">
                                            Select Date</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:RadioButtonList ID="rblDate" runat="server" CssClass="radio radio-success" RepeatDirection="Horizontal"
                                            AutoPostBack="true" OnSelectedIndexChanged="rblDate_OnSelectedChanged" RepeatLayout="Flow">
                                            <asp:ListItem Selected="True" Value="Today">Today</asp:ListItem>
                                            <asp:ListItem Value="Week">Week</asp:ListItem>
                                            <asp:ListItem Value="Custom">Custom</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right p-t-5">
                                        <label class="form-label mandatorylbl">
                                            From date</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:TextBox ID="txtFromDate" runat="server" Class="form-control costinclude datepicker mandatoryfield"
                                            placeholder="Enter From date"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right p-t-5">
                                        <label class="form-label mandatorylbl">
                                            To date</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:TextBox ID="txtToDate" runat="server" Class="form-control costinclude datepicker mandatoryfield"
                                            placeholder="Enter To date"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right p-t-5">
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:LinkButton ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return Validate();"
                                            OnClick="btnSubmit_Click" CssClass="btn btn-cons btn-success" />
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
                                        <div class="col-md-12 row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-4 text-center">
                                                <label class="form-label" style="display: inline-block">
                                                    Total Number Of Enquiries
                                                </label>
                                                <asp:Label ID="lblTotNoOfEnq" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-4 text-center">
                                                <label class="form-label" style="display: inline-block">
                                                    Total Number Of Budgetary Enquiries
                                                </label>
                                                <asp:Label ID="lblTotNoOfBudgetaryEnquiries" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-4 text-center">
                                                <label class="form-label" style="display: inline-block">
                                                    Purchase Order Enquiries
                                                </label>
                                                <asp:Label ID="lblPurOrderEnquiries" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvEnquiryTradingReport" runat="server" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Customer Enquiry Number">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCustEnqNum" runat="server" Text='<%# Eval("CustomerEnquiryNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Enquiry Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEnquiryDate" runat="server" Text='<%# Eval("EnquiryDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Received Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReceivedDate" runat="server" Text='<%# Eval("ReceivedDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sales Staff Allocated Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSalesStaffAssignedDat" runat="server" Text='<%# Eval("SalesAssignedDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Designer Staff Allocated Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDSADate" runat="server" Text='<%# Eval("DesignerAssignedDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Drawing Completion Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDrawingUploadDate" runat="server" Text='<%# Eval("DesignUploadedDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Design HOD Drawing Approval Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDesignApprovalDate" runat="server" Text='<%# Eval("DesignHODApprovalDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Customer Response Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCustResponseDate" runat="server" Text='<%# Eval("CustomerRepliedDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PO Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPODate" runat="server" Text='<%# Eval("PODate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="col-sm-12 text-center">
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
