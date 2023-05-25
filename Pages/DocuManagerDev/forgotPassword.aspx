<%@ page language="C#" autoeventwireup="true" inherits="forgotPassword, App_Web_4n0dulas" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Docu Manager</title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <script src="js/CommonFunctions.js" type="text/javascript"></script>
    <script type="text/javascript">
        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };
        function Forgotvalidation() {
            if (Page_ClientValidate("Forgot")) {
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
                            <td style="background-image: url(images/forgot_bg.png);" width="378px" height="285px"
                                valign="top" align="left">
                                <table border="0" width="325px">
                                    <tr>
                                        <td width="43px" height="115px">
                                        </td>
                                        <td width="182px">
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="20px">
                                        </td>
                                        <td colspan="2" valign="top" class="remeber-txt">
                                            Enter Your Email
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="40px">
                                        </td>
                                        <td valign="top" colspan="2">
                                            <asp:TextBox ID="txtUserName" runat="server" TabIndex="1" Width="240px" BorderWidth="0"
                                                Height="20px" Style="margin-left: 6px;" MaxLength="50"></asp:TextBox>
                                            &nbsp;
                                            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName"
                                                CssClass="reqField" ErrorMessage="*" ToolTip="Please Enter Your Email" ValidationGroup="Forgot"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="40px">
                                        </td>
                                        <td valign="middle" colspan="2" align="right">
                                            <asp:ImageButton ID="ibtnSubmit" runat="server" ImageUrl="images/submit.png" ToolTip="Submit"
                                                ValidationGroup="Forgot" Style="margin-top: 7px;" OnClick="ibtnSubmit_Click"
                                                OnClientClick="Forgotvalidation();" />
                                            <asp:ImageButton ID="ibtnBack" runat="server" ImageUrl="images/back.png" ToolTip="Back"
                                                ValidationGroup="SignIn" Style="margin-top: 7px;" OnClick="ibtnBack_Click" />
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
