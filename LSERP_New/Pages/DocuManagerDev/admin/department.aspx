<%@ page title="" language="C#" masterpagefile="~/DocuManager.master" autoeventwireup="true" inherits="admin_department, App_Web_1kqnuvut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="JavaScript" type="text/javascript">
        function DeleteConfirmation() {
            if (confirm("Are you sure want to delete department?")) {
                ShowLoader();
                return true;
            }
            else {
                return false;
            }
        }
        function savevalidation() {
            if (Page_ClientValidate("Department")) {
                ShowLoader();
            }
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="smDepartment" runat="server">
    </asp:ScriptManager>
    <table cellpadding="0" cellspacing="0" width="720px" height="438px">
        <tr>
            <td align="center" valign="top">
                <asp:UpdatePanel runat="server" ID="upnlDepartment" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="tblDepartmentList" runat="server" Width="720px" ScrollBars="Auto"
                            Height="430px">
                            <br />
                            <div style="width: 100%;">
                                <table width="690px" cellpadding="3" cellspacing="0">
                                    <tr style="background: rgb(235, 235, 235); border-top: solid 1px #d9d9d9;">
                                        <td align="left" width="50%" valign="top">
                                        </td>
                                        <td align="right" width="50%" valign="top">
                                            <asp:ImageButton ID="btnAddNew" runat="server" ImageUrl="~/icons/addnew.png" Width="32px"
                                                Height="32px" ToolTip="Click to Add Department" OnClick="btnAddNew_Click" OnClientClick="ShowLoader();" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" height="1px">
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="gridviewTable">
                                <asp:GridView ID="grvDepartment" runat="server" GridLines="Vertical" AllowPaging="True"
                                    AllowSorting="True" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" PageSize="8"
                                    CellPadding="3" Width="690px" OnRowCommand="grvDepartment_RowCommand"
                                    OnSorting="grvDepartment_Sorting" OnPageIndexChanging="grvDepartment_Paging" AlternatingRowStyle-CssClass="alternateRow">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40px"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="40px"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" SortExpression="DepartmentName">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="360px"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="360px"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField SortExpression="Status" HeaderText="Status">
                                            <ItemTemplate>
                                                <%--<span id="spanStatus" class='<%#fngetImageUrl(Eval("Status").ToString())%>' runat="server" ></span> --%>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status")%>' CssClass='<%#fngetImageUrl(Eval("Status").ToString())%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="190px"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="190px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:LinkButton CommandArgument='<%# Bind("DepartmentId")%>' CssClass="view_icon"
                                                    ID="lbtnView" CommandName="ViewDepartment" runat="server" OnClientClick="ShowLoader();" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton CommandArgument='<%# Bind("DepartmentId")%>' CssClass="delete_icon"
                                                    ID="lbtnDelete" CommandName="DeleteDepartment" runat="server" OnClientClick="return DeleteConfirmation();" />
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
                        <asp:Panel ID="tblDepartment" runat="server" Width="720px" ScrollBars="Auto" Height="430px"
                            Visible="false">
                            <br />
                            <div style="width: 100%; height: 95%;">
                                <table width="690px" cellpadding="3" cellspacing="0" height="100%" style="border: solid 5px rgb(226, 226, 226);">
                                    <tr style="background: rgb(235, 235, 235); border-top: solid 1px #d9d9d9;">
                                        <td align="left" width="690px" valign="top">
                                           <table class="grid-bottom" width="680px" cellpadding="3" cellspacing="0">
                                                <%--<tr>
                                                    <td style="border-bottom:0px"  align="right" width="10px">
                                                    </td> 
                                                </tr> --%>
                                                <tr>
                                                    <td align="right" width="150px">
                                                        <span class="lblCaption">Department Name :</span>
                                                    </td>
                                                    <td align="left" width="200px">
                                                        <asp:TextBox ID="txtDepartment" runat="server" CssClass="txtBox" TabIndex="1" Width="200px" MaxLength="50" ></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        <asp:RequiredFieldValidator ID="rfvLocation" runat="server" ControlToValidate="txtDepartment"
                                                            ErrorMessage="*" CssClass="reqField" ToolTip="Please Enter Department Name" ValidationGroup="Department"
                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="lblCaption">Status :</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:RadioButtonList ID="rbtnStatus" runat="server" TabIndex="2" CausesValidation="false"
                                                            CssClass="radioButtons" RepeatDirection="Horizontal">
                                                            <asp:ListItem Selected="True" Value="Y">Active</asp:ListItem>
                                                            <asp:ListItem Value="N">InActive</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td align="left">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">
                                                    </td>
                                                    <td align="left" colspan="2">
                                                        <asp:Button ID="btnSave" runat="server" Text="Save" TabIndex="3" CssClass="actionButtons"
                                                             ValidationGroup="Department" OnClick="btnSave_Click" OnClientClick="savevalidation();" />
                                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" TabIndex="5" CssClass="actionButtons"
                                                            OnClick="btnDelete_Click" OnClientClick="return DeleteConfirmation();" />
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="4" CssClass="actionButtons"
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
                                        <img src="../images/loader.gif" alt=''  />
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
                        <asp:HiddenField ID="hfDepartmentId" runat="server" Value="0" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
