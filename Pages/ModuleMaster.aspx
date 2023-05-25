<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master"  AutoEventWireup="true"
    CodeFile="ModuleMaster.aspx.cs" Inherits="Pages_ModuleMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="Server">
    <link rel="stylesheet" type="text/css" href="../Assets/css/faSelectorStyle.min.css" />
    <script type="text/javascript" src="../Assets/scripts/faSelectorWidget.js"></script>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.2/css/all.css" />
    
    <script type="text/javascript">
        //Font awesome picker related codings start
        //document.querySelectorAll("ul.icon_types > li").forEach(function (event) {
        //    event.addEventListener("click", function (event) {
        //        document.querySelectorAll("ul.icon_types > li").forEach(function (el) {
        //            if (el.classList.contains("active")) { el.classList.toggle("active") }
        //        })
        //        event.target.classList.toggle("active");
        //    })
        //})

        function selectFaIcon(icon, event) {
            var clone = icon.cloneNode(),   
            container = event.target.parentElement;
            clone.removeAttribute("onclick");
            $('#<% =iconvalue.ClientID %>').attr('Value', $(clone).attr('class'));
            container.style.display = "none";
            $('#iconview,#ContentPlaceHolder1_iconview').html(clone);
        }
	//Font awesome picker related codings end
        function ShowPopup() {
            document.getElementById("<%=txtModuleName.ClientID %>").value = "";
            document.getElementById("<%=btnSaveApprover.ClientID %>").value = "Save";            
            document.getElementById("<%= hdnMID.ClientID%>").value = "";
            $('#modelAddModule').modal({
                backdrop: 'static'
            });
            return false;
        }

        function CheckValidation() {
            var valid = true;
            if (document.getElementById("<%=txtModuleName.ClientID %>").value == "") {
                $('#<%=txtModuleName.ClientID %>').notify('Field Required', { arrowShow: true, position: 't,r', autoHide: true });
                valid = false;
            }
            if (Boolean(valid) == true)
                return true;
            else
                return false;
        }
        function ShowData(id) {
            $('#modelAddModule').modal({
                backdrop: 'static'
            });
        }

        function message(title, message, type) {
            swal(title, message, type);
        }
        function deleteConfirm(mid) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Module will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                    showLoader();
                    __doPostBack('deletemodule');                    
            });
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
                                        Module Master</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Access Control</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Module Master</li>
                                                </ol>
                                     </nav>
                        <a id="help" href="" alt="" style="margin-top: 4px;">
                            <img src="../Assets/images/help.png" />
                        </a>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-12">
                <div class="grid simple">
                    <div class="grid-title no-border">
                    </div>
                    <div class="tools">
                    </div>
                    <div class="grid-body">
                        <div class="col-sm-12">
                            <div class="col-sm-9">
                            </div>
                            <div class="ip-div text-center">
                                <asp:Button ID="btnADD" runat="server" CssClass="btn btn-cons btn-success" Text="Add New"
                                    OnClientClick="return ShowPopup();" />
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <div class="col-sm-1">
                            </div>
                            <div class="col-sm-10">
                                <asp:GridView ID="gvModuleMaster" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-hover no-more-tables"
                                    OnRowDataBound="gvModuleMaster_RowDataBound" OnRowCommand="gvModuleMaster_RowCommand" HeaderStyle-HorizontalAlign="Center">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Module Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblModuleName" runat="server" Text='<%#Eval("ModuleName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="20%" HeaderText="Module Icon" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblModuleIcon" runat="server" Text='<%#Eval("ModuleLogo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="10%"  HeaderText="Edit" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMID" runat="server" Text='<%#Eval("MID")%>' Visible="false"></asp:Label>
                                                <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditDetails" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Visible="true" Style="text-align: center"><img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="10%"  HeaderText="Delete" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDelete" runat="server" CommandName="DeleteDetails" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" Visible='<%#Eval("Visible").Equals(0)? true:false %>' Style="text-align: center"><img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal" id="modelAddModule">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title bold" style="color: #800b4f; margin: auto; text-align: center;" id="tournamentNameTeam">Add Module</h4>
                    <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true" style="margin: -1rem 1rem -1rem 1rem;">
                        ×</button>
                </div>
                <div class="">
                    <div class="inner-container">
                        <div class="col-sm-12" style="padding-top: 30px;">
                            <div class="col-sm-1">
                            </div>
                            <div class="col-sm-3" style="font-weight: 600; padding-top: 10px;">
                                Module Name</div>
                            <div class="col-sm-5">
                                <asp:TextBox ID="txtModuleName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-3">
                            </div>
                        </div>
                        <div class="col-sm-12" style="padding-top: 10px;" id="trLevel4" runat="server">
                            <div class="col-sm-1">
                            </div>
                            <div class="col-sm-3" style="font-weight: 600; padding-top: 10px;">
                                Module Icon</div>
                            <div class="col-sm-3">
                                <span class="btn" onclick="openFaSelector(this,event)" id="fa-selector" data-theme="classic" data-num="1">Click for Icon</span>
                            </div>
                            <span id="iconview" runat="server"></span>
                        </div>
                        <div class="col-sm-12" style="padding-top: 10px;">
                            <div class="col-sm-4">
                            </div>
                            <div class="col-sm-8">
                                <asp:Button ID="btnSaveApprover" runat="server" Text="Save" CssClass="btn btn-cons btn-success"
                                    OnClientClick="return CheckValidation();" OnClick="btnSaveApprover_Click" />
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdnvalue" runat="server" Value="" />
    <asp:HiddenField ID="hdnMID" runat="server" Value="" />
    <asp:HiddenField ID="iconvalue" runat="server" Value="" />
</asp:Content>
