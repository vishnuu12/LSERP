<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="SalesRevenueTargetEmployeeWise.aspx.cs" Inherits="Pages_SalesRevenueTargetEmployeeWise"
    ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">      
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
                                        <%--<i class="fa fa-th-large opacity-6 yellow"></i>--%>
                                        <img src="../Assets/images/pages.png" alt="" width="16px" />
                                    </span>
                                    <h3 class="page-title-head d-inline-block">Sales Target Revenue For Finance Year</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Reports</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Sales Target Revenue For Finance Year </li>
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
                        <div id="divInput" runat="server">
                            <div class="ip-div text-left">
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4 text-right">
                                                <label class="form-label">
                                                    Finance Yaer
                                                </label>
                                            </div>
                                            <div class="col-sm-6 text-left">
                                                <asp:Label ID="lblfinanceyear" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-2">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4 text-right">
                                                <label class="form-label mandatorylbl">
                                                    Employee Name
                                                </label>
                                            </div>
                                            <div class="col-sm-6 text-left">
                                                <asp:DropDownList ID="ddlsalesemployeename" runat="server" AutoPostBack="false"
                                                    CssClass="form-control mandatoryfield" Width="70%" ToolTip="Select Employee Name">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-2">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4 text-right">
                                                <label class="form-label mandatorylbl">
                                                    Target Value
                                                </label>
                                            </div>
                                            <div class="col-sm-6 text-left">
                                                <asp:TextBox ID="txttargetvalue" runat="server" CssClass="form-control mandatoryfield"> </asp:TextBox>
                                            </div>
                                            <div class="col-sm-2">
                                            </div>
                                        </div>

                                        <div class="col-sm-12 p-t-10 text-center">
                                            <asp:LinkButton ID="btnSave" runat="server" Text="Save" CssClass="btn btn-cons btn-save AlignTop"
                                                OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divOutput');" OnClick="btnSave_Click"></asp:LinkButton>
                                        </div>

                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvSalesrenuetargetdetails" runat="server" 
                                                CssClass="table table-bordered table-hover no-more-tables"
                                                ShowHeaderWhenEmpty="True" OnRowCommand="gvSalesrenuetargetdetails_OnRowCommand" EmptyDataText="No Records Found" AutoGenerateColumns="false" DataKeyNames="SEFYTG">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Name" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblempname" runat="server" Text='<%# Eval("EmployeeName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Finance Year" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblfinanceyaer" runat="server" Text='<%# Eval("FinanceYear")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Target Value" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltargetvalue" runat="server" Text='<%# Eval("TargetValue")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtndelete" runat="server"
                                                                CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="DeleteSalesRevenue">
                                                                    <img src="../Assets/images/del-ec.png" alt=""/>
                                                            </asp:LinkButton>
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


