<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="MaterialGradeMaster.aspx.cs" Inherits="Pages_MaterialGradeMaster" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function deleteConfirm(MGMID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Material Grade Name will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                __doPostBack('deletegvrow', MGMID);
            });
            return false;
        }

        function ShowInputSection() {
            document.getElementById('<%=divAdd.ClientID %>').style.display = "none";
            document.getElementById('<%=divInput.ClientID %>').style.display = "block";
            document.getElementById('<%=divOutput.ClientID %>').style.display = "none";
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
                                    <h3 class="page-title-head d-inline-block">Material Grade Master</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Stores</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Material Grade Master</li>
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
                                <asp:LinkButton ID="btnAddNew" runat="server" Text="Add New Grade Name" OnClientClick="return ShowInputSection();"
                                    CssClass="btn ic-w-btn add-btn"></asp:LinkButton>
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Grade Name</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:TextBox ID="txtGradeName" runat="server" Width="70%" CssClass="form-control mandatoryfield"
                                            AutoComplete="off" ToolTip="Enter Grade Name" placeholder="Enter Grade Name">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Specific weight</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:TextBox ID="txtSpecificWeight" runat="server" Width="70%" CssClass="form-control mandatoryfield"
                                            AutoComplete="off" ToolTip="Enter Specific Weight" placeholder="Enter Specific Weight">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Cost</label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtCost" runat="server" Width="70%" CssClass="form-control mandatoryfield"
                                            AutoComplete="off" ToolTip="Enter Cost" placeholder="Enter Cost">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <%--   <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            UOM</label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtUOM" runat="server" Width="70%" CssClass="form-control mandatoryfield"
                                            AutoComplete="off" ToolTip="Enter UOM" placeholder="Enter UOM">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>--%>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Material Category Name</label>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control mandatoryfield"
                                            ToolTip="Select Category Name" AppendDataBoundItems="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-20">
                                    <asp:LinkButton ID="btnSave" runat="server" Text="Save" CssClass="btn btn-cons btn-save AlignTop"
                                        OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divInput');" OnClick="btnSave_Click"></asp:LinkButton>
                                    <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                        CssClass="btn btn-cons btn-danger AlignTop"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="">
                                            <p class="h-style">
                                                <asp:Label ID="lbltitle" runat="server" Text="Material Grade Details"></asp:Label>
                                            </p>
                                            <div class="f-right">
                                            </div>
                                            <div class="clear">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10 ex-icons" id="divDownload" runat="server" visible="false">
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvMaterialGradeMaster" runat="server" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                HeaderStyle-HorizontalAlign="Center" OnRowCommand="gvMaterialGradeMaster_RowCommand"
                                                DataKeyNames="MGMID,CID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="MGMID" HeaderStyle-CssClass="text-left"
                                                        ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMGMID" runat="server" Text='<%# Eval("MGMID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Material Category Name" HeaderStyle-CssClass="text-left"
                                                        ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMaterialCategoryName" runat="server" Text='<%# Eval("CategoryName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Material Grade Name" HeaderStyle-CssClass="text-left"
                                                        ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMaterialGradeName" runat="server" Text='<%# Eval("GradeName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Specific Weight" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSpecificWeight" runat="server" Text='<%# Eval("SpecificWeight")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cost" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCost" runat="server" Text='<%# Eval("Cost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--  <asp:TemplateField HeaderText="UOM" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="EditMaterialGroup">
                                                           <img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDelete" runat="server" OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("MGMID")) %>'>
                                                            <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnMGMId" runat="server" Value="0" />
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
