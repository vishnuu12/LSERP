<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="changepassword.aspx.cs" Inherits="Pages_changepassword" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function PasswordValidation() {
            var msg = "0";
            if (document.getElementById('<%=txtOldPassword.ClientID %>').value == '') {
                $('#<%=txtOldPassword.ClientID %>').notify('Field Required.', { arrowShow: true, position: 't,r', autoHide: true });
                msg = "1";
            }
            if (document.getElementById('<%=txtNewPassword.ClientID %>').value == '') {
                $('#<%=txtNewPassword.ClientID %>').notify('Field Required.', { arrowShow: true, position: 't,r', autoHide: true });
                msg = "1";
            }
            else if ((document.getElementById('<%=txtOldPassword.ClientID %>').value) == (document.getElementById('<%=txtNewPassword.ClientID %>').value)) {
                $('#<%=txtNewPassword.ClientID %>').notify('Cannot Give Same Password.', { arrowShow: true, position: 't,r', autoHide: true });
                msg = "1";
                return false;
            }
            if (document.getElementById('<%=txtConfirmpassword.ClientID %>').value == '') {
                $('#<%=txtConfirmpassword.ClientID %>').notify('Field Required.', { arrowShow: true, position: 't,r', autoHide: true });
                msg = "1";
            }
            else if (document.getElementById('<%=txtNewPassword.ClientID %>').value != document.getElementById('<%=txtConfirmpassword.ClientID %>').value) {
                $('#<%=txtConfirmpassword.ClientID %>').notify('Confirm Password Mismatch.', { arrowShow: true, position: 't,r', autoHide: true });
                msg = "1";
            }

            if (msg == "0") {
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
                                        Change Password</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Human Resources</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Change Password</li>
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
                        <div class="col-sm-12">
                            <div class="col-sm-2">
                                <label class="form-label">
                                    User Name</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtUserName" runat="server" Enabled="false" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-7">
                            </div>
                        </div>
                        <div class="col-sm-12 paddingtop-10">
                            <div class="col-sm-2">
                                <label class="form-label">
                                    Old Password</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password" autocomplete="off"
                                    AutoCompleteType="Disabled" CssClass="form-control"></asp:TextBox>
                                <asp:Label ID="lblpassword" runat="server" Style="display: none;"></asp:Label>
                            </div>
                            <div class="col-sm-7">
                            </div>
                        </div>
                        <div class="col-sm-12 paddingtop-10">
                            <div class="col-sm-2">
                                <label class="form-label">
                                    New Password</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-7">
                            </div>
                        </div>
                        <div class="col-sm-12 paddingtop-10">
                            <div class="col-sm-2">
                                <label class="form-label">
                                    Confirm New Password</label>
                            </div>
                            <div class="col-sm-3">
                                <asp:TextBox ID="txtConfirmpassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="col-sm-7">
                            </div>
                        </div>
                        <div class="col-sm-12 paddingtop-10">
                            <div class="col-sm-2">
                            </div>
                            <div class="col-sm-2">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" Text="Submit"
                                    OnClientClick="return PasswordValidation();" OnClick="btnSubmit_Click" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
