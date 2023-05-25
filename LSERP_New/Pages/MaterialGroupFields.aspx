<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="MaterialGroupFields.aspx.cs" Inherits="Pages_MaterialGroupFields" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function ValidateAdd() {
            var msg = true;
            if ($('#<%=ddlMaterialName.ClientID %>')[0].selectedIndex == 0) {
                $('#<%=ddlMaterialName.ClientID %>').notify('Fields Required.', { arrowShow: true, position: 'r,r', autoHide: true });
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
    <style type="text/css">
        input[type="checkbox"]
        {
            box-sizing: border-box;
            padding: 0;
            margin-right: 20px;
        }
        
        .chkOutputFields, tr
        {
            margin-left: 15px; /* float: left; */
            display: flex; /* align-items: center; */
        }
        .chkOutputFields, tr, td
        {
            display: flex;
            margin-right: 20px;
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
                                    <h3 class="page-title-head d-inline-block">
                                        Material Group Specs Assignment</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Stores</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Material Group Fields</li>
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
            <asp:UpdatePanel ID="upMaterialGroup" runat="server">
                <Triggers>
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" runat="server">
                            <div class="ip-div text-center">
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Material Name
                                        </label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlMaterialName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMaterialName_SelectIndexChanged"
                                            CssClass="form-control" Width="70%" onchange="showLoader();" ToolTip="Select Material Name"
                                            TabIndex="1">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-20">
                                    <div class="col-sm-6">
                                        <div class="col-sm-12">
                                            <div class="col-sm-4 text-right">
                                                <label class="form-label">
                                                    Required Input Fields</label>
                                            </div>
                                            <div class="col-sm-8 p-l-0">
                                                <asp:CheckBoxList ID="chkInputFields" Style="margin-right: 20px; margin-left: 20px;"
                                                    runat="server" RepeatDirection="Vertical" RepeatColumns="3" CssClass="check-success chkOutputFields">
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-20">
                                            <div class="col-sm-4 text-right">
                                                <label class="form-label">
                                                    Required Formula Fields</label>
                                            </div>
                                            <div class="col-sm-8 p-l-0">
                                                <asp:CheckBoxList ID="chkFormulaFields" Style="margin-right: 20px; margin-left: 20px;"
                                                    runat="server" RepeatDirection="Vertical" RepeatColumns="3" CssClass="check-success">
                                                </asp:CheckBoxList>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-20 text-center">
                                            <asp:LinkButton ID="btnSave" runat="server" Text="Save" CssClass="btn btn-cons btn-save AlignTop"
                                                TabIndex="3" OnClientClick="return ValidateAdd()" OnClick="btnSave_Click"></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-sm-6" style="padding-left: 25%;">
                                        <asp:GridView ID="gvSpecsDescription" runat="server" AutoGenerateColumns="False"
                                            ShowHeaderWhenEmpty="True" ShowHeader="true" EmptyDataText="No Records Found"
                                            CssClass="table table-hover table-bordered medium">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Specs Description" ItemStyle-HorizontalAlign="left"
                                                    HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSpecsDescription" runat="server" Text='<%# Eval("SpecsDescription")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                            <div id="divOutput" class="output_section" runat="server">
                            </div>
                        </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
