﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="MaterialFormulaSheet.aspx.cs" Inherits="Pages_MaterialFormulaSheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="../Assets/css/datatables2.css" />
    <script type="text/javascript" src="../Assets/scripts/datatables2.js"></script>
    <script type="text/javascript">
        function showDataTable() {
            $('#<%=gvMaterialFormula.ClientID %>').DataTable({ 'aoColumnDefs': [{
                'bSortable': false,
                'aTargets': [-1, -2] /* 1st one, start by the right */
            }]
            });
        }
        function Validate() {
            var msg = true;
            if ($('#<%=ddlMaterialName.ClientID %>')[0].selectedIndex == 0) {
                $('#<%=ddlMaterialName.ClientID %>').notify('Fields Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }
            if ($('#<%=txtFormula.ClientID %>').val() == "") {
                $('#<%=txtFormula.ClientID %>').notify('Fields Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }
            if ($('#<%=ddlMaterialSpecsName.ClientID %>')[0].selectedIndex == 0) {
                $('#<%=ddlMaterialSpecsName.ClientID %>').notify('Fields Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }

            if (msg) {
                showLoader();
                return true;
            }
            else
                return false;
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
                                        <%--<i class="fa fa-th-large opacity-6 yellow"></i>--%>
                                        <img src="../Assets/images/pages.png" alt="" width="16px" />
                                    </span>
                                    <h3 class="page-title-head d-inline-block">
                                        Material Formula Sheet</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Stores</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">MaterialFormulaSheet</li>
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
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" runat="server">
                            <div class="ip-div text-center">
                                <asp:LinkButton ID="btnAddNew" runat="server" Text="Add New Material Formula" OnClick="btnAddNew_Click"
                                    CssClass="btn ic-w-btn add-btn"></asp:LinkButton>
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4">
                                        <label class="form-label">
                                            Material Name
                                        </label>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlMaterialName" runat="server" OnChange="showLoader();" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlMaterialName_SelectIndexChanged" ToolTip="Select Material Name"
                                            CssClass="form-control">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4">
                                        <label class="form-label">
                                            Material Specs Name
                                        </label>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlMaterialSpecsName" runat="server" AutoPostBack="true" ToolTip="Select Material Specs Name"
                                            CssClass="form-control">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4">
                                        <label class="form-label">
                                            Formula
                                        </label>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtFormula" runat="server" CssClass="form-control" ToolTip="Enter Formula"
                                            AutoComplete="off" placeholder="Enter Specs Short Code"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-20">
                                    <asp:LinkButton ID="btnSave" runat="server" Text="Save" CssClass="btn btn-cons btn-save AlignTop"
                                        OnClientClick="return Validate();" OnClick="btnSave_Click"></asp:LinkButton>
                                    <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                        CssClass="btn btn-cons btn-danger AlignTop"></asp:LinkButton>
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
                                                    <asp:Label ID="lbltitle" runat="server" Text="Material Formula Details"></asp:Label></p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10 p-l-0 p-r-0">
                                            <asp:GridView ID="gvMaterialFormula" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" OnRowCommand="gvMaterialFormula_OnRowCommand"
                                                CssClass="table table-hover table-bordered medium" HeaderStyle-HorizontalAlign="Center">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'
                                                                Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="text-center" HeaderText="Material Name" ItemStyle-Width="25%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSpecsName" runat="server" Text='<%# Eval("MaterialName")%>' Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Specs Name" ItemStyle-CssClass="text-center" ItemStyle-Width="35%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSpecsShortCode" runat="server" Text='<%# Eval("SpecsName")%>' Font-Bold="false"
                                                                Font-Italic="false" Style="font-family: Arial; font-size: 12px; text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Formula Name" ItemStyle-CssClass="text-center" ItemStyle-Width="35%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFormula" runat="server" Text='<%# Eval("Formula")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# Eval("MFID") %>'>
                                                           <img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnMFID" runat="server" Value="0" />
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
