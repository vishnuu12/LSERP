<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="MRNLifeCycleReport.aspx.cs" Inherits="Pages_MRNLifeCycleReport" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        #divMRNBasicInformation label {
            width: 40%;
            color: brown;
        }

        div#ContentPlaceHolder1_divOutput span {
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
                                    <h3 class="page-title-head d-inline-block">MRN Life Cycle Report </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Purchase</a></li>
                                <li class="active breadcrumb-item" aria-current="page">MRN Life Cycle Report </li>
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
                    <asp:PostBackTrigger ControlID="btnprint" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" class="btnAddNew" runat="server">
                            <div class="text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right p-t-5">
                                        <label class="form-label">
                                            MRN No
                                        </label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlMRNNo" runat="server" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlMRNNo_OnSelectIndexChanged" AutoPostBack="true"
                                            ToolTip="Select MRN No">
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
                                        <div id="divMRNBasicInformation">

                                            <div class="col-sm-12 text-center p-t-10">
                                                <label style="width: 10%; color: violet; font-weight: bolder;">
                                                    GET MRN PRINT
                                                </label>
                                                <asp:LinkButton ID="btnprint" runat="server"
                                                    OnClick="btnprint_Click">
                                                    <img src="../Assets/images/print.png" />
                                                </asp:LinkButton>
                                            </div>

                                            <div class="col-sm-12 text-center p-t-10">
                                                <label style="color: green; font-weight: bold;">MRN BASIC INFORMATION </label>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <label>MRN No </label>
                                                    <asp:Label ID="lblMRNNo" runat="server"> </asp:Label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label>Location </label>
                                                    <asp:Label ID="lblLocation" runat="server"> </asp:Label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label>UOM </label>
                                                    <asp:Label ID="lbluom" runat="server"> </asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10" runat="server">
                                                <div class="col-sm-4" style="display: flex;">
                                                    <label>Grade Name </label>
                                                    <asp:Label ID="lblGradeName" runat="server"> </asp:Label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label>Thickness </label>
                                                    <asp:Label ID="lblThickness" runat="server"> </asp:Label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label>Category Name </label>
                                                    <asp:Label ID="lblcategoryname" runat="server"> </asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10" runat="server">
                                                <div class="col-sm-4" style="display: flex;">
                                                    <label>Inward Qty </label>
                                                    <asp:Label ID="lblInwardqty" runat="server"> </asp:Label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label>Blocked Qty </label>
                                                    <asp:Label ID="lblTotalBlockedQty" runat="server"> </asp:Label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label>Issued Qty </label>
                                                    <asp:Label ID="lblTotalIssuedQty" runat="server"> </asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10" runat="server">
                                                <div class="col-sm-4" style="display: flex;">
                                                    <label>Instock Qty </label>
                                                    <asp:Label ID="lblInstockQty" runat="server"> </asp:Label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label>Po No </label>
                                                    <asp:Label ID="lblPONo" runat="server"> </asp:Label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label>Unit Cost </label>
                                                    <asp:Label ID="lblunitcost" runat="server"> </asp:Label>
                                                </div>
                                            </div>

                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4" style="display: flex;">
                                                    <label>Supplier Name </label>
                                                    <asp:Label ID="lblsuppliername" runat="server"> </asp:Label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <label>Inward Type </label>
                                                    <asp:Label ID="lblinwardtype" runat="server"> </asp:Label>
                                                </div>
                                                <div class="col-sm-4">
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-12 text-center p-t-10">
                                            <label style="color: green; font-weight: bold;">JOB CARD ISSUE DETAILS </label>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvJobCardIssueDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium orderingfalse">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Job Card No" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJobCardNo" runat="server" Text='<%# Eval("JobCardNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="RFP No" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRFPNo" runat="server" Text='<%# Eval("RFPNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MRN Issued Date" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMRNIssuedDate" runat="server" Text='<%# Eval("MRNIssueDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Issued By" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMRNIssuedBy" runat="server" Text='<%# Eval("MRNIssuedBy")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Job Card Raised By" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMRNIssuedBy" runat="server" Text='<%# Eval("JobCardRaisedBy")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Issued Qty" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIssuedQty" Width="50px" runat="server" Text='<%# Eval("IssuedQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                        <div class="col-sm-12 text-center p-t-10">
                                            <label style="color: green; font-weight: bold;">STORE ISSUE DETAILS </label>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvGeneralMRNIssueDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found"
                                                CssClass="table table-hover table-bordered medium" DataKeyNames="GMIDID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="RFP No" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRFPNo" runat="server" Text='<%# Eval("RFPNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MRN No" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMRNNo" runat="server" Text='<%# Eval("MRNNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Issued Qty" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIssuedQty" runat="server" Text='<%# Eval("IssuedQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Issued By" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIssuedBy" runat="server" Text='<%# Eval("IssuedBy")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Issued On" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIssuedON" runat="server" Text='<%# Eval("IssuedOn")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnMIDID" runat="server" Value="0" />
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

