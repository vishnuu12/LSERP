<%@ page title="" language="C#" masterpagefile="~/DocuManager.master" autoeventwireup="true" inherits="user_myaccounts, App_Web_f3kf0do4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="JavaScript" type="text/javascript">
        function savevalidation() {
            if (Page_ClientValidate("User")) {
                ShowLoader();
            }
        }
        function chgPwdvalidation() {
            if (Page_ClientValidate("Password")) {
                ShowLoader();
            }
        }
        
    </script>
    <style type="text/css">
        .TabContainer
        {
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            font-weight: bold;
            text-decoration: none;
            width: 680px;
            margin-top: 10px;
            margin-bottom: 6px;
        }
        .TabItemInactive
        {
            border-top: 1px solid white;
            border-left: 1px solid white;
            border-right: 1px solid #aaaaaa;
            border-bottom: none;
            background-color: #d3d3d3;
            text-align: center;
            text-decoration: none;
            padding: 6px 6px 6px 6px;
            color:#464646;
        }
          
        
        .TabItemInactive:hover
        {
            background: #008BA4;
            color:#fff;
        }
        .TabItemActive
        {
            border-top: 1px solid white;
            border-left: none;
            border-right: 1px solid #aaaaaa;
            border-bottom: none;
            text-decoration: none;
            background-color: #008BA4;
            text-align: center;
            padding: 6px 6px 6px 6px;
            color:#fff;
        }
        
        .ContentPanel
        {
            padding: 0px 0px 0px 0px;
            width: 680px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="smUser" runat="server">
    </asp:ScriptManager>
    <table cellpadding="0" cellspacing="0" width="720px" height="438px">
        <tr>
            <td align="left" valign="top" style="padding: 5px 10px 10px 10px">
                <asp:UpdatePanel runat="server" ID="upnlUser" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:MultiView ID="mviewMain" runat="Server" ActiveViewIndex="0">
                            <asp:View ID="CustomerView" runat="Server">
                                <asp:Panel ID="panelNavigatonView1" runat="server" CssClass="TabContainer">
                                    <asp:Label ID="labOne" runat="Server" CssClass="TabItemActive" Text="Edit Profile" />
                                    <asp:LinkButton ID="lnkb_DefaultBook" CssClass="TabItemInactive" Text="Change Password"
                                        runat="Server" OnCommand="LinkButton_Command" CommandName="Password" />
                                </asp:Panel>
                                <asp:Panel ID="tblUser" runat="server" CssClass="ContentPanel">
                                    <table width="695px" cellpadding="3" cellspacing="0" height="100%" style="border: solid 5px rgb(226, 226, 226);">
                                        <tr style="background: rgb(235, 235, 235); border-top: solid 1px #d9d9d9;">
                                            <td align="left" width="695px" valign="top">
                                                <table class="grid-bottom" width="680px" cellpadding="3" cellspacing="0">
                                                    <tr>
                                                        <td align="right" width="150px">
                                                            <span class="lblCaption">First Name :</span>
                                                        </td>
                                                        <td align="left" width="200px">
                                                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtBox" TabIndex="1" Width="200px"
                                                                MaxLength="50" onKeypress="return validateName(event);"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName"
                                                                ErrorMessage="*" CssClass="reqField" ToolTip="Please Enter First Name" ValidationGroup="User"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="lblCaption">Last Name : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtLastName" runat="server" CssClass="txtBox" TabIndex="2" Width="200px"
                                                                MaxLength="50" onKeypress="return validateName(event);"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="lblCaption">Phone Number : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="txtBox" TabIndex="3" Width="200px"
                                                                MaxLength="25" onKeypress="return validatePhoneNo(event);"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="rfvPhoneNo" runat="server" ControlToValidate="txtPhoneNo"
                                                                ErrorMessage="*" CssClass="reqField" ToolTip="Please Enter Phone Number" ValidationGroup="User"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="lblCaption">Email Address : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="txtBox" TabIndex="4" Width="200px"
                                                                MaxLength="50" onKeypress="return validateEmail(event);" onblur="checkEmailAddress(this)"
                                                                Enabled="False"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                                                                ErrorMessage="*" CssClass="reqField" ToolTip="Please Enter Email Address" ValidationGroup="User"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" valign="top">
                                                            <span class="lblCaption">Address : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtAddress" runat="server" CssClass="txtAddress" TabIndex="5" TextMode="MultiLine"
                                                                MaxLength="200" onkeyDown="return checkTextAreaMaxLength(this,event,'200');"
                                                                onKeypress="return validateAddress(event);" Width="198px"></asp:TextBox>
                                                        </td>
                                                        <td valign="top">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="lblCaption">City : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtCity" runat="server" CssClass="txtBox" TabIndex="6" Width="200px"
                                                                MaxLength="50" onKeypress="return validateCity(event);"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="lblCaption">Country : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlCountry" CssClass="dropdown" runat="server" ValidationGroup="User"
                                                                AutoPostBack="True" TabIndex="7" Width="200px" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="lblCaption">State : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlState" runat="server" ValidationGroup="User" TabIndex="8"
                                                                Width="200px" CssClass="dropdown">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="lblCaption">Postal Code : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtPostalCode" runat="server" CssClass="txtBox" TabIndex="9" Width="200px"
                                                                MaxLength="6" onKeypress="return validationNumeric(event);"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="lblCaption">Department : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtDepartment" runat="server" CssClass="txtBox" TabIndex="10" Width="200px"
                                                                Enabled="False"></asp:TextBox>
                                                      </td>
                                                        <td>
                                                            <asp:Label ID="lblDepartmentId" runat="server" Text="0" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="lblCaption">User Level : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtUserLevel" runat="server" CssClass="txtBox" TabIndex="11" Width="200px"
                                                                Enabled="False"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                           <asp:Label ID="lblUserLevelID" runat="server" Text="0" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" valign="top">
                                                            <span class="lblCaption">User Rights :</span>
                                                        </td>
                                                        <td align="left" valign="top">
                                                            <asp:CheckBoxList ID="cblUserRights" runat="server" CssClass="Checkboxlist" TabIndex="12"
                                                                Enabled="False">
                                                            </asp:CheckBoxList>
                                                        </td>
                                                        <td align="left" valign="top">
                                                        <asp:Label ID="lblPassword" runat="server" Text="" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                        </td>
                                                        <td align="left" colspan="2">
                                                            <asp:Button ID="btnSave" runat="server" Text="Save" TabIndex="17" CssClass="actionButtons"
                                                                ValidationGroup="User" OnClick="btnSave_Click" OnClientClick="savevalidation();" />
                                                            <asp:Button ID="btnRSet" runat="server" Text="Reset" TabIndex="18" CssClass="actionButtons"
                                                                OnClick="btnRSet_Click" OnClientClick="ShowLoader();" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </asp:View>
                            <asp:View ID="BookView" runat="Server">
                                <asp:Panel ID="panelNavigatonView2" runat="server" CssClass="TabContainer">
                                    <asp:LinkButton ID="lnkb_BookCustomer" runat="Server" CssClass="TabItemInactive"
                                        Text="Edit Profile" OnCommand="LinkButton_Command" CommandName="EditInfo" />
                                    <asp:Label ID="Label3" runat="Server" CssClass="TabItemActive" Text="Change Password" />
                                </asp:Panel>
                                <asp:Panel ID="Panel1" runat="server" CssClass="ContentPanel" Height="438px">
                                    <table width="695px" cellpadding="3" cellspacing="0" height="100%" style="border: solid 5px rgb(226, 226, 226);">
                                        <tr style="background: rgb(235, 235, 235); border-top: solid 1px #d9d9d9;">
                                            <td align="left" width="695px" valign="top">
                                                <table class="grid-bottom" width="680px" cellpadding="3" cellspacing="0">
                                                    <tr>
                                                        <td align="right" width="150px">
                                                            <span class="lblCaption">Old Password :</span>
                                                        </td>
                                                        <td align="left" width="200px">
                                                            <asp:TextBox ID="txtOldPassword" runat="server" CssClass="txtBox" TabIndex="51" Width="200px"
                                                                MaxLength="20" TextMode="Password"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <asp:RequiredFieldValidator ID="rfvOldPassword" runat="server" ControlToValidate="txtOldPassword"
                                                                ErrorMessage="*" CssClass="reqField" ToolTip="Please Enter Old Password" ValidationGroup="Password"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="lblCaption">New Password : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtNewPassword" runat="server" CssClass="txtBox" TabIndex="52" Width="200px"
                                                                MaxLength="20" TextMode="Password"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="rfvNewPassword" runat="server" ControlToValidate="txtNewPassword"
                                                                ErrorMessage="*" CssClass="reqField" ToolTip="Please Enter New Password" ValidationGroup="Password"
                                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span class="lblCaption">Confirm Password : </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="txtBox" TabIndex="53"
                                                                Width="200px" MaxLength="20" TextMode="Password"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:RequiredFieldValidator ID="rfvConfirmPwd" runat="server" ControlToValidate="txtConfirmPassword"
                                                                ErrorMessage="*" CssClass="reqField" ToolTip="Please Enter Confirm Password"
                                                                ValidationGroup="Password" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                        </td>
                                                        <td align="left" colspan="2">
                                                            <asp:Button ID="btnSavePassword" runat="server" Text="Save" TabIndex="54" CssClass="actionButtons"
                                                                ValidationGroup="Password" OnClick="btnSavePassword_Click" OnClientClick="chgPwdvalidation();" />
                                                            <asp:Button ID="btnReset" runat="server" Text="Reset" TabIndex="55" CssClass="actionButtons"
                                                                OnClick="btnReset_Click" OnClientClick="ShowLoader();" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </asp:View>
                        </asp:MultiView>
                        <br />
                        <asp:Panel ID="mpLoaderPanel" runat="server" Style="display: none;">
                            <table>
                                <tr>
                                    <td align="center">
                                        <img src="../images/loader.gif" alt='' width="70" height="70" />
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
                        <asp:HiddenField ID="hfUserId" runat="server" Value="0" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
