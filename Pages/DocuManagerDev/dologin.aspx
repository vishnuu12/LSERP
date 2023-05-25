<%@ page language="C#" autoeventwireup="true" inherits="dologin, App_Web_4n0dulas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Docu Manager</title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <script src="js/CommonFunctions.js" type="text/javascript"></script>
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
        function SignInvalidation() {
            if (Page_ClientValidate("SignIn")) {
                ShowLoaderWOMPage();
            }
        }
    </script>
</head>
<body style="background-color: #fff">
    <form id="form1" runat="server">
     <asp:ScriptManager ID="smUser" runat="server">
    </asp:ScriptManager>
    <div style="background: #ffffff url(icons/Document-Manager_dull.jpg) no-repeat center;
        width: auto; height: auto;">
        <table width="100%">
            <tr>
                <td>
                </td>
                <td width="950px" height="650px" valign="middle" align="center">
                    <table>
                        <tr>
                            <td style="background-image: url(images/Login.png);" width="378px" height="362px"
                                valign="top" align="left">
                                <table border="0">
                                    <tr>
                                        <td width="43px" height="115px">
                                        </td>
                                        <td width="182px">
                                        </td>
                                        <td width="150px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="20px">
                                        </td>
                                        <td colspan="2" valign="top" class="remeber-txt">
                                            Username
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="40px">
                                        </td>
                                        <td valign="top" colspan="2">
                                            <asp:TextBox ID="txtUserName" runat="server" TabIndex="1" Width="240px" BorderWidth="0"
                                                Height="20px" Style="margin-left: 6px; margin-top: 3px;" MaxLength="50"></asp:TextBox>
                                            &nbsp;
                                            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName"
                                                CssClass="reqField" ErrorMessage="*" ToolTip="Please Enter Username" ValidationGroup="SignIn"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="25px">
                                        </td>
                                        <td colspan="2" valign="top" class="remeber-txt">
                                            Password
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="35px">
                                        </td>
                                        <td valign="top" colspan="2">
                                            <asp:TextBox ID="txtPassword" runat="server" TabIndex="2" Width="240px" BorderWidth="0"
                                                Height="20px" TextMode="Password" Style="margin-left: 6px; margin-top: 3px;"
                                                MaxLength="20"></asp:TextBox>
                                            &nbsp;
                                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                                                CssClass="reqField" ErrorMessage="*" ToolTip="Please Enter Password" ValidationGroup="SignIn"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="40px">
                                        </td>
                                        <td valign="middle">
                                            <asp:CheckBox ID="cbxRemember" runat="server" TabIndex="3" Text="Remember Me" ToolTip="Check if you want to remember username and password"
                                                CssClass="remeber-txt" />
                                        </td>
                                        <td valign="top">
                                            <asp:ImageButton ID="ibtnSignIn" runat="server" ImageUrl="images/login_button.png"
                                                ToolTip="Click to enter into the site." ValidationGroup="SignIn" OnClick="ibtnSignIn_Click"
                                                OnClientClick="SignInvalidation();" Style="margin-top: 7px;" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="15px">
                                        </td>
                                        <td valign="middle">
                                            <asp:LinkButton ID="lbtnForgot" CssClass="forget-pass-txt" runat="server" OnClick="lbtnForgot_Click">Forgot password?</asp:LinkButton>
                                        </td>
                                        <td valign="top">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                </td>
            </tr>
        </table>
        <asp:Panel ID="mpLoaderPanel" runat="server" Style="display: none;">
            <table>
                <tr>
                    <td align="center">
                        <img src="images/loader.gif" alt='' width="70" height="70" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <input type="button" id="btnMPOk" runat="server" value="OkButton" style="display: none;" />
        <input type="button" id="btnMPCancel" runat="server" value="CancelBut" style="display: none;" />
        <ajaxTK:ModalPopupExtender ID="mpeLoader" BackgroundCssClass="loaderModalBackground"
            runat="server" OkControlID="btnMPOk" CancelControlID="btnMPCancel" PopupControlID="mpLoaderPanel"
            TargetControlID="btnMPOk">
        </ajaxTK:ModalPopupExtender>
    </div>
    </form>
</body>
</html>
