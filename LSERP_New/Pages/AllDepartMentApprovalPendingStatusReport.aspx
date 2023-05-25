<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master"
    AutoEventWireup="true" CodeFile="AllDepartMentApprovalPendingStatusReport.aspx.cs"
    Inherits="Pages_AllDepartMentApprovalPendingStatusReport" ClientIDMode="Predictable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                                    <h3 class="page-title-head d-inline-block">Approval pending Status Report </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Reports</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Approval Pending Status</li>
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
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                        <label class="mandatorylbl">Approval Name </label>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlApprovalname" runat="server" ToolTip="Select Country" TabIndex="6" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlApprovalname_OnSelectIndexChanged" CssClass="form-control mandatoryfield">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            <asp:ListItem Value="BOM" Text="BOM"></asp:ListItem>
                                            <asp:ListItem Value="Drawing" Text="Drawing"></asp:ListItem>
                                            <asp:ListItem Value="SalesCost" Text="SalesCost"></asp:ListItem>
                                            <asp:ListItem Value="RFP" Text="RFP"></asp:ListItem>
                                            <asp:ListItem Value="MaterialPlanningQC" Text="MPQC"></asp:ListItem>
                                            <asp:ListItem Value="JobOrderQC" Text="JOBQC"></asp:ListItem>
                                            <asp:ListItem Value="AssemplyWeldingQC" Text="AW"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="col-md-12 row">
                                        </div>
                                        <div class="col-sm-12">
                                            <asp:GridView ID="gvAllDepartMentApprovalPendingStatus" runat="server" CssClass="table table-bordered table-hover no-more-tables"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" AutoGenerateColumns="true">
                                                <Columns>
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

