<%@ page title="" language="C#" masterpagefile="~/DocuManager.master" autoeventwireup="true" inherits="admin_workflow, App_Web_1kqnuvut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="JavaScript" type="text/javascript">
        function onchangetext(event, txtbox_1, txtbox_2) {
            if (validationCurrency(event)) {
                var charCode = (event.which) ? event.which : event.keyCode
                var strvalue = String.fromCharCode(charCode);
                var txtbox1 = $get(txtbox_1.id);
                var txtbox2 = $get('ContentPlaceHolder1_' + txtbox_2);
                var strtxtValue = txtbox1.value + strvalue;
                if (strtxtValue.length <= 10)
                    txtbox2.value = strtxtValue;

                return true;
            }
            else {
                return false;
            }
        }
        function onchangebackspace(txtbox_1, txtbox_2) {
            if ((event.keyCode == 8) || (event.keyCode == 46)) {
                var txtbox1 = $get(txtbox_1.id);
                var txtbox2 = $get('ContentPlaceHolder1_' + txtbox_2);
                txtbox2.value = txtbox1.value;
            }
        }
        function savevalidation() {
            if (Page_ClientValidate("workflow")) {
                ShowLoader();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="smUser" runat="server">
    </asp:ScriptManager>
    <table cellpadding="0" cellspacing="0" width="720px" height="438px">
        <tr>
            <td align="center" valign="top">
                <asp:UpdatePanel runat="server" ID="upnlUser" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="tblUserList" runat="server" Width="720px" ScrollBars="Auto" Height="510px">
                            <br />
                            <div class="levelgrid">
                                <table width="690" border="0" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <th width="200" align="left" valign="top" colspan="2">
                                            Primary Level
                                        </th>
                                        <th width="190px" align="left" valign="top">
                                            &nbsp;
                                        </th>
                                        <th align="left" valign="top" width="300px">
                                            &nbsp;
                                        </th>
                                    </tr>
                                    <tr class="alternavtive-bg2">
                                        <td align="left" valign="middle" width="50px">
                                        </td>
                                        <td align="left" valign="middle">
                                            WorkFlow
                                        </td>
                                        <td align="left" valign="middle">
                                            Value
                                        </td>
                                        <td align="left" valign="middle">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr class="alternavtive-bg">
                                        <td>
                                        </td>
                                        <td  align="left" valign="middle">
                                            L1
                                            <img src="../icons/arrow.png" width="8" height="5" />
                                            L2
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:DropDownList ID="ddlPrimary_Value1" runat="server" Width="40px" CssClass="dropdown"
                                                TabIndex="101">
                                                <%--<asp:ListItem Value="0">=</asp:ListItem>
                                                                        <asp:ListItem Value="1">&lt;</asp:ListItem>
                                                                        <asp:ListItem Value="2">&lt;=</asp:ListItem>
                                                                        <asp:ListItem Value="3">&gt;</asp:ListItem>
                                                                        <asp:ListItem Value="4">&gt;=</asp:ListItem>--%>
                                                <asp:ListItem Value="0">&lt;</asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;<asp:TextBox ID="txtPrimary_Value1" runat="server" CssClass="txtBox" TabIndex="1"
                                                Width="100px" MaxLength="10" onkeyup="onchangebackspace(this,'txtPrimary_Value2');"
                                                onKeypress="return onchangetext(event,this,'txtPrimary_Value2');"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:RequiredFieldValidator ID="rfvPValue1" runat="server" ControlToValidate="txtPrimary_Value1"
                                                ErrorMessage="Please enter value" CssClass="reqField" ToolTip="Please Enter Primary Value 1" ValidationGroup="workflow"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="alternavtive-bg1">
                                        <td>
                                        </td>
                                        <td height="37" align="left" valign="middle">
                                            L1
                                            <img src="../icons/arrow.png" width="8" height="5" />
                                            L2
                                            <img src="../icons/arrow.png" width="8" height="5" />
                                            L3
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:DropDownList ID="ddlPrimary_Value2" runat="server" Width="40px" CssClass="dropdown"
                                                TabIndex="102">
                                                <asp:ListItem Value="0">&gt;=</asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;<asp:TextBox ID="txtPrimary_Value2" runat="server" CssClass="txtBox" TabIndex="103"
                                                Width="100px" ReadOnly="false" onKeypress="return false;" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:RequiredFieldValidator ID="rfvPValue2" runat="server" ControlToValidate="txtPrimary_Value2"
                                                ErrorMessage="Please enter value" CssClass="reqField" ToolTip="Please Enter Primary Value 2" ValidationGroup="workflow"
                                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <br />
                            <div class="levelgrid">
                                <table width="690" border="0" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <th width="200" align="left" valign="top" colspan="2">
                                            Secondary Level
                                        </th>
                                        <th width="190px" align="left" valign="top">
                                            &nbsp;
                                        </th>
                                        <th align="left" valign="top" width="300px">
                                            &nbsp;
                                        </th>
                                    </tr>
                                    <tr class="alternavtive-bg2">
                                        <td align="left" valign="middle" width="50px">
                                        </td>
                                        <td align="left" valign="middle">
                                            WorkFlow
                                        </td>
                                        <td align="left" valign="middle">
                                            Value
                                        </td>
                                        <td align="left" valign="middle">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr class="alternavtive-bg">
                                        <td>
                                        </td>
                                        <td height="37" align="left" valign="middle">
                                            L3
                                            <img src="../icons/arrow.png" width="8" height="5" />
                                            L5
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:DropDownList ID="ddlSecondary_Value1" runat="server" Width="40px" CssClass="dropdown"
                                                TabIndex="104">
                                                <asp:ListItem Value="0">&lt;</asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;<asp:TextBox ID="txtSecondary_Value1" runat="server" CssClass="txtBox" TabIndex="2"
                                                Width="100px" MaxLength="10" onkeyup="onchangebackspace(this,'txtSecondary_Value2');"
                                                onKeypress="return onchangetext(event,this,'txtSecondary_Value2');"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:RequiredFieldValidator ID="rfvSecValue1" runat="server" ControlToValidate="txtSecondary_Value1"
                                                ErrorMessage="Please enter value" CssClass="reqField" ToolTip="Please Enter Secondary Value 1"
                                                ValidationGroup="workflow" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="alternavtive-bg1">
                                        <td>
                                        </td>
                                        <td height="37" align="left" valign="middle">
                                            L3
                                            <img src="../icons/arrow.png" width="8" height="5" />
                                            L4
                                            <img src="../icons/arrow.png" width="8" height="5" />
                                            L5
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:DropDownList ID="ddlSecondary_Value2" runat="server" Width="40px" CssClass="dropdown"
                                                TabIndex="105">
                                                <asp:ListItem Value="0">&gt;=</asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;<asp:TextBox ID="txtSecondary_Value2" runat="server" CssClass="txtBox" TabIndex="106"
                                                Width="100px" ReadOnly="false" onKeypress="return false;" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:RequiredFieldValidator ID="rfvSecValue2" runat="server" ControlToValidate="txtSecondary_Value2"
                                                ErrorMessage="Please enter value" CssClass="reqField" ToolTip="Please Enter Secondary Value 2"
                                                ValidationGroup="workflow" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <br />
                            <div class="levelgrid">
                                <table width="690" border="0" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <th width="200" align="left" valign="top" colspan="2">
                                            Final Level
                                        </th>
                                       <th width="190px" align="left" valign="top">
                                            &nbsp;
                                        </th>
                                        <th align="left" valign="top" width="300px">
                                            &nbsp;
                                        </th>
                                    </tr>
                                    <tr class="alternavtive-bg2">
                                        <td align="left" valign="middle" width="50px">
                                        </td>
                                        <td align="left" valign="middle">
                                            WorkFlow
                                        </td>
                                        <td align="left" valign="middle">
                                            Value
                                        </td>
                                        <td align="left" valign="middle">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr class="alternavtive-bg">
                                        <td>
                                        </td>
                                        <td height="37" align="left" valign="middle">
                                            L5 = L6
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:DropDownList ID="ddlFinal_Value1" runat="server" Width="40px" CssClass="dropdown"
                                                TabIndex="107">
                                                <asp:ListItem Value="0">&lt;</asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;<asp:TextBox ID="txtFinal_Value1" runat="server" CssClass="txtBox" TabIndex="3"
                                                Width="100px" MaxLength="10" onkeyup="onchangebackspace(this,'txtFinal_Value2');"
                                                onKeypress="return onchangetext(event,this,'txtFinal_Value2');"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:RequiredFieldValidator ID="rfvFinalValue1" runat="server" ControlToValidate="txtFinal_Value1"
                                                ErrorMessage="Please enter value" CssClass="reqField" ToolTip="Please Enter Secondary Value 2"
                                                ValidationGroup="workflow" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="alternavtive-bg1">
                                        <td>
                                        </td>
                                        <td height="37" align="left" valign="middle">
                                            L5
                                            <img src="../icons/arrow.png" width="8" height="5" />
                                            L6
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:DropDownList ID="ddlFinal_Value2" runat="server" Width="40px" CssClass="dropdown"
                                                TabIndex="108">
                                                <asp:ListItem Value="0">&gt;=</asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;<asp:TextBox ID="txtFinal_Value2" runat="server" CssClass="txtBox" TabIndex="109"
                                                Width="100px" ReadOnly="false" onKeypress="return false;" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:RequiredFieldValidator ID="rfvFinalValue2" runat="server" ControlToValidate="txtFinal_Value2"
                                                ErrorMessage="Please enter value" CssClass="reqField" ToolTip="Please Enter Secondary Value 2"
                                                ValidationGroup="workflow" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div>
                                <table width="690px">
                                    <tr>
                                        <td align="left" width="175px">
                                        </td>
                                        <td align="left" colspan="2">
                                            <asp:Button ID="btnSave" runat="server" Text="Save" TabIndex="17" CssClass="actionButtons"
                                                ValidationGroup="Workflow" OnClick="btnSave_Click" OnClientClick="savevalidation();" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
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
                        <asp:HiddenField ID="hfCompanyId" runat="server" Value="0" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
