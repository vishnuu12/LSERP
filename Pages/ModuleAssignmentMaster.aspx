<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="ModuleAssignmentMaster.aspx.cs" Inherits="Pages_ModuleAssignmentMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   <script type="text/javascript">
        function ZeroElimation(value) {
            var id = value.id;
            if ((value.value == "0") || (value.value == "")) {
                //                document.getElementById(id).value = '';
                $('#' + id).notify('Any number greater than 0.', { arrowShow: true, position: 't,r', autoHide: true });
                $('#' + id).focus();
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<Message:Uc ID="ucMessage" runat="server" />--%>
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
                                        Assignment Master</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Access Control</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Module Assignament</li>
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
                    <asp:PostBackTrigger ControlID="btnSave" />
                 </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divInput" runat="server">
                            <div class="ip-div text-left">
                                <div class="col-sm-4">
                                </div>
                                <div class="col-sm-4">
                                    <label class="form-label">
                                            User Role</label>
                                    <asp:DropDownList ID="ddlUserType" OnSelectedIndexChanged="ddlUserType_IndexChanged"
                                        CssClass="form-control" runat="server" OnChange="showLoader();" AutoPostBack="true" ToolTip="Select Enquiry/Cust Number">
                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                </div>
                            </div>
                        </div>
                        <div class="grid simple">                            
                            <div class="grid-body">
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-1">
                                    </div>
                                    <div class="col-sm-10">
                                        <asp:GridView ID="gvModuleName" runat="server" CssClass="table table-bordered table-hover no-more-tables"
                                            AutoGenerateColumns="false" DataKeyNames="MID,Access" OnRowDataBound="gvModuleName_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Module Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblModuleName" runat="server" Text='<%#Eval("ModuleName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Check Module Names to Display" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkModules" runat="server" AutoPostBack="true" OnCheckedChanged="chkModule_CheckChanged" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Display Order">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDisplayOrder" runat="server" CssClass="form-control" onkeypress="return validationNumeric(this);"
                                                            onblur="return ZeroElimation(this)" Text='<%#Eval("DisplayOrder") %>' MaxLength="2"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-5">
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success btn-cons" OnClientClick="ShowLoader();"
                                            Text="Save" OnClick="btnSave_Click" />
                                    </div>
                                    <div class="col-sm-4">
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
