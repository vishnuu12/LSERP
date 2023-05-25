<%@ Page Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="PageMaster.aspx.cs" Inherits="Pages_Pagemaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Contentp1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">


        function multiselect(multivalue) {
            var test = multivalue.trim();
            var testArray = test.split(',');
            //$('#ContentPlaceHolder1_liCertificates').val(testArray);
            $('#ContentPlaceHolder1_gvModuleName').find('select').val(testArray);
        }

        function ZeroElimation(value) {
            var id = value.id;
            if ((value.value == "0") || (value.value == "")) {
                //                document.getElementById(id).value = '';
                $('#' + id).notify('Any number greater than 0.', { arrowShow: true, position: 't,r', autoHide: true });
                $('#' + id).focus();
                return false;
            }
        }
        function ShowPopup() {
            $('#modelAddModule').modal({
                backdrop: 'static'
            });
            return false;
        }
        function hidepopup() {
            hideLoader();
            $('#modelAddModule').modal('toggle');
            return false;
        }

        function addnew() {
            document.getElementById('<%=addnew.ClientID %>').style.display = "block";
            document.getElementById('<%=gvModuleName.ClientID %>').style.display = "none";
            return false;
        }

        function CheckfieldValidation() {
            var valid = true;
            if (document.getElementById("<%=txtPageName.ClientID %>").value == "") {
                $('#<%=txtPageName.ClientID %>').notify('All Fields Required', { arrowShow: true, position: 't,r', autoHide: true });
                valid = false;
            }
            if (document.getElementById("<%=txtPageReference.ClientID %>").value == "") {
                $('#<%=txtPageName.ClientID %>').notify('All Fields Required', { arrowShow: true, position: 't,r', autoHide: true });
                valid = false;
            }
            if (document.getElementById("<%=txtDisplayName.ClientID %>").value == "") {
                $('#<%=txtPageName.ClientID %>').notify('All Fields Required', { arrowShow: true, position: 't,r', autoHide: true });
                valid = false;
            }
            if (document.getElementById("<%=txtDisplaySeq.ClientID %>").value == "") {
                $('#<%=txtPageName.ClientID %>').notify('All Fields Required', { arrowShow: true, position: 't,r', autoHide: true });
                valid = false;
            }
            if (document.getElementById("<%=ddladdmodule.ClientID %>").value == 0) {
                $('#<%=txtPageName.ClientID %>').notify('All Fields Required', { arrowShow: true, position: 't,r', autoHide: true });
                valid = false;
            }
            if (Boolean(valid) == true) {
                $('#modelAddModule').modal('toggle');
                return true;
            }
            else
                return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Contentp2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                    <h3 class="page-title-head d-inline-block">Page Master</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Access Control</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Page Assignament</li>
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
                    <asp:AsyncPostBackTrigger ControlID="ddlmenuname" EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Menu Name</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlmenuname" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlmenuname_SelectIndexChanged"
                                            CssClass="form-control" Width="70%" ToolTip="Select Enquiry Number">
                                            <asp:ListItem Text="--Select--" Value="0" />
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2" />
                                </div>
                            </div>
                            <div class="ip-div text-center" style="margin-top: 12px;">
                                <asp:LinkButton ID="btnAddNew" runat="server" Text="Add New Page" OnClientClick="return ShowPopup();"
                                    CssClass="btn ic-w-btn add-btn" />
                            </div>
                        </div>
                        <div class="grid simple">
                            <div class="grid-body">
                                <div class="col-sm-12 p-t-10" style="overflow-x: scroll ! important; overflow-wrap: anywhere;">
                                    <asp:GridView ID="gvModuleName" runat="server" CssClass="table table-bordered table-hover no-more-tables"
                                        AutoGenerateColumns="false" DataKeyNames="MID,Access,UserType,UserTypeIDS" OnRowDataBound="gvModuleName_RowDataBound"
                                        OnRowEditing="gvModuleName_RowEditing" OnRowCancelingEdit="gvModuleName_RowCancelingEdit"
                                        OnRowUpdating="gvModuleName_RowUpdating" HeaderStyle-HorizontalAlign="Center">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Page Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpageName" runat="server" Text='<%#Eval("PageName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Page Reference">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpageReference" runat="server" Text='<%#Eval("PageReference") %>'></asp:Label>
                                                    <asp:TextBox ID="txtpageReference" CssClass="form-control" runat="server" Text='<%#Eval("PageReference") %>'
                                                        Visible="false"></asp:TextBox>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtpageReference_E" CssClass="form-control" runat="server" Text='<%#Eval("PageReference") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Display Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpageDisplay" runat="server" Text='<%#Eval("DisplayName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtDisplayName" CssClass="form-control" runat="server" Text='<%#Eval("DisplayName") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesignation" runat="server" Text='<%#Eval("UserType") %>'></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:ListBox ID="liDesignation" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Menu" ItemStyle-HorizontalAlign="Center">
                                                <%--OnCheckedChanged="chkModule_CheckChanged"--%>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkModules" runat="server" AutoPostBack="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Display Order">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtDisplayOrder" runat="server" CssClass="form-control" onkeypress="return validationNumeric(this);"
                                                        onblur="return ZeroElimation(this)" Text='<%#Eval("DisplayOrder") %>' MaxLength="2"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ButtonType="Image" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"
                                                ShowEditButton="true" EditText="<img src='~/images/icon_edit.png' title='Edit' />"
                                                EditImageUrl="../Assets/images/edit-ec.png" CancelImageUrl="../Assets/images/icon_cancel.png"
                                                UpdateImageUrl="../Assets/images/icon_update.png" ItemStyle-Wrap="false" ControlStyle-Width="20px"
                                                ControlStyle-Height="20px" HeaderText="Edit" ValidationGroup="edit" HeaderStyle-Width="7%">
                                                <ControlStyle CssClass="UsersGridViewButton" />
                                            </asp:CommandField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-5">
                                    </div>
                                    <div class="col-sm-4">
                                        <%-- <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success btn-cons" OnClientClick="ShowLoader();"
                                            Text="Save" OnClick="btnSave_Click" />--%>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>
                                <div class="modal" id="modelAddModule">
                                    <div class="modal-dialog">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <h4 class="modal-title bold" style="color: #800b4f; margin: auto; text-align: center;"
                                                    id="tournamentNameTeam">Add New Page</h4>
                                                <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true"
                                                    style="margin: -1rem 1rem -1rem 1rem;">
                                                    ×</button>
                                            </div>
                                            <div class="">
                                                <div class="inner-container">
                                                    <div id="addnew" runat="server">
                                                        <div class="ip-div text-center">
                                                            <div class="col-sm-12 p-t-10">
                                                                <div class="col-sm-4">
                                                                    <label class="form-label">
                                                                        Page Name
                                                                    </label>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <asp:TextBox ID="txtPageName" runat="server" CssClass="form-control" ToolTip="Enter Page Name"
                                                                        placeholder="Enter Page Name" MaxLength="100"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-2">
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12 p-t-10">
                                                                <div class="col-sm-4">
                                                                    <label class="form-label">
                                                                        Page Reference
                                                                    </label>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <asp:TextBox ID="txtPageReference" runat="server" CssClass="form-control" ToolTip="Enter Page Reference"
                                                                        placeholder="Enter Page Reference" MaxLength="100"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-2">
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12 p-t-10">
                                                                <div class="col-sm-4">
                                                                    <label class="form-label">
                                                                        Module Name
                                                                    </label>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <asp:DropDownList ID="ddladdmodule" runat="server" CssClass="form-control">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <div class="col-sm-2">
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12 p-t-10">
                                                                <div class="col-sm-4">
                                                                    <label class="form-label">
                                                                        Display Name
                                                                    </label>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <asp:TextBox ID="txtDisplayName" runat="server" CssClass="form-control" placeholder="Display Name"
                                                                        ToolTip="Enter Display Name"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-2">
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12 p-t-10">
                                                                <div class="col-sm-4">
                                                                    <label class="form-label">
                                                                        Display Order
                                                                    </label>
                                                                </div>
                                                                <div class="col-sm-6">
                                                                    <asp:TextBox ID="txtDisplaySeq" runat="server" CssClass="form-control" placeholder="Display Order"
                                                                        ToolTip="Enter Display Order" MaxLength="7"></asp:TextBox>
                                                                </div>
                                                                <div class="col-sm-2">
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12 p-t-20">
                                                                <asp:LinkButton ID="btn_saveaddnew" runat="server" Text="Save" CssClass="btn btn-cons btn-save AlignTop"
                                                                    OnClick="btnSaveadd_Click" OnClientClick="return CheckfieldValidation();"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="modal-footer">
                                            </div>
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
