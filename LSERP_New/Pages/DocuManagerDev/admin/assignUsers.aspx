<%@ page title="" language="C#" masterpagefile="~/DocuManager.master" autoeventwireup="true" inherits="admin_assignUsers, App_Web_1kqnuvut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="JavaScript" type="text/javascript">
        function savevalidation() {
            ShowLoader();
        }
        function ValidateUserRights(source, args) {
            var chkListModules = document.getElementById('<%= cblUserRights.ClientID %>');
            var chkListinputs = chkListModules.getElementsByTagName("input");
            if (chkListinputs.length > 0) {
                for (var i = 0; i < chkListinputs.length; i++) {
                    if (chkListinputs[i].checked) {
                        args.IsValid = true;
                        return;
                    }
                }
                args.IsValid = false;
            }
            else {
                args.IsValid = false;
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
                        <asp:Panel ID="tblUserList" runat="server" Width="720px" ScrollBars="Auto" Height="430px">
                            <br />
                            <div style="width: 100%;">
                                <table width="690px" cellpadding="3" cellspacing="0">
                                    <tr style="background: rgb(235, 235, 235); border-top: solid 1px #d9d9d9;">
                                        <td align="left" width="50%" valign="top">
                                        </td>
                                        <td align="right" width="50%" valign="top">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" height="1px">
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="gridviewTable">
                                <asp:GridView ID="grdUser" runat="server" GridLines="Vertical" AllowPaging="True"
                                    AllowSorting="True" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" PageSize="8"
                                    CellPadding="3" Width="690px" OnSorting="grdUser_Sorting" OnRowCommand="grdUser_RowCommand"
                                    OnPageIndexChanging="grdUser_Paging" AlternatingRowStyle-CssClass="alternateRow">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="40px"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="160px"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="160px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PhoneNo" HeaderText="Phone No">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="EmailId" HeaderText="Email">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="190px"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="190px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField SortExpression="Status" HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status")%>' CssClass='<%#fngetImageUrl(Eval("Status").ToString())%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:LinkButton CommandArgument='<%# Bind("UserID")%>' CssClass="view_icon" ID="lbtnView"
                                                    CommandName="ViewUser" runat="server" OnClientClick="ShowLoader();" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        No records to display
                                    </EmptyDataTemplate>
                                    <PagerStyle CssClass="pagination" />
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="tblUser" runat="server" Width="720px" ScrollBars="Auto" Height="850px"
                            Visible="false">
                            <br />
                            <div style="width: 100%; height: 95%;">
                                <table width="690px" cellpadding="3" cellspacing="0" height="100%" style="border: solid 5px rgb(226, 226, 226);">
                                    <tr style="background: rgb(235, 235, 235); border-top: solid 1px #d9d9d9;">
                                        <td align="left" width="690px" valign="top">
                                            <table class="grid-bottom" width="680px" cellpadding="3" cellspacing="0">
                                                <tr>
                                                    <td align="right" width="175px">
                                                        <span class="lblCaptionBold">First Name :</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="lblFirstName" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="lblCaptionBold">Last Name : </span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="lblLastName" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="lblCaptionBold">Phone Number : </span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="lblPhoneNumber" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="lblCaptionBold">Email Address : </span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="lblEmailAddress" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="lblCaptionBold">Department : </span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="lblDepartment" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="lblCaptionBold">User Level : </span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="lblUserLevel" runat="server" Text=""></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" valign="top">
                                                        <span class="lblCaptionBold">User Rights :</span>
                                                    </td>
                                                    <td align="left" valign="top">
                                                        <asp:CheckBoxList ID="cblUserRights" runat="server" CssClass="Checkboxlist" TabIndex="15"
                                                            Enabled="false">
                                                        </asp:CheckBoxList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" valign="top">
                                                        <span class="lblCaptionBold">Document Recieved From :</span>
                                                    </td>
                                                    <td align="left" valign="top" height="160px">
                                                        <asp:Panel Width="480px" Height="150px" ScrollBars="Auto" runat="server" ID="pnlRecivedDocument">
                                                            <div class="gridviewTableSmall">
                                                                <asp:GridView ID="grvReceivedDocument" runat="server" GridLines="Vertical" AllowPaging="False"
                                                                    AllowSorting="True" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" PageSize="8"
                                                                    CellPadding="3" Width="450px" AlternatingRowStyle-CssClass="alternateRow">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="S.No">
                                                                            <ItemTemplate>
                                                                                <%# Container.DataItemIndex + 1 %>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="40px"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40px"></ItemStyle>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="UserName">
                                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="200px"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="UserLevel" HeaderText="User Level" SortExpression="UserLevel">
                                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="210px"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="210px"></ItemStyle>
                                                                        </asp:BoundField>
                                                                       <%-- <asp:TemplateField HeaderText="View">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkAssignStatus" runat="server" Checked="false" />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px"></ItemStyle>
                                                                        </asp:TemplateField>--%>
                                                                    </Columns>
                                                                    <EmptyDataTemplate>
                                                                        No records to display
                                                                    </EmptyDataTemplate>
                                                                    <PagerStyle CssClass="pagination" />
                                                                </asp:GridView>
                                                            </div>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" valign="top">
                                                        <span class="lblCaptionBold">Document Send To :</span>
                                                    </td>
                                                    <td align="left" valign="top"height="200px">
                                                        <asp:Panel Width="480px" Height="180px" ScrollBars="Auto" runat="server" ID="pnlSendDocument">
                                                            <div class="gridviewTableSmall">
                                                                <asp:GridView ID="grvSendDocument" runat="server" GridLines="Vertical" AllowPaging="False"
                                                                    AllowSorting="True" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" PageSize="8"
                                                                    CellPadding="3" Width="450px" AlternatingRowStyle-CssClass="alternateRow">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="S.No">
                                                                            <ItemTemplate>
                                                                                <%# Container.DataItemIndex + 1 %>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="40px"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40px"></ItemStyle>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="UserName">
                                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="235px"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="235px"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="UserLevel" HeaderText="User Level" SortExpression="UserLevel">
                                                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="160px"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="160px"></ItemStyle>
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField HeaderText="Assign">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox ID="chkAssignStatus" runat="server" Checked='<%#bool.Parse(Eval("AssignStatus").ToString()== "1" ? "true" : "false")%>' />
                                                                                <asp:HiddenField ID="hdnSendToUserId" runat="server" Value='<%# Eval("UserID") %>' />
                                                                                <asp:HiddenField ID="hdnSendToUserLevelId" runat="server" Value='<%# Eval("UserLevelId") %>' />
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></HeaderStyle>
                                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px"></ItemStyle>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EmptyDataTemplate>
                                                                        No records to display
                                                                    </EmptyDataTemplate>
                                                                    <PagerStyle CssClass="pagination" />
                                                                </asp:GridView>
                                                            </div>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                    </td>
                                                    <td align="left">
                                                        <asp:Button ID="btnSave" runat="server" Text="Assign" TabIndex="17" CssClass="actionButtons"
                                                            OnClick="btnSave_Click" OnClientClick="savevalidation();" />
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="19" CssClass="actionButtons"
                                                            OnClick="btnCancel_Click" OnClientClick="ShowLoader();" />
                                                    </td>
                                                </tr>
                                            </table>
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
                        <asp:HiddenField ID="hfUserId" runat="server" Value="0" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
