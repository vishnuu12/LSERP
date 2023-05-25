<%@ page title="" language="C#" masterpagefile="~/DocuManager.master" autoeventwireup="true" inherits="admin_vendor, App_Web_1kqnuvut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="JavaScript" type="text/javascript">
        function DeleteConfirmation() {
            if (confirm("Are you sure want to delete Vendor?")) {
                ShowLoader();
                return true;
            }
            else {
                return false;
            }
        }
        function savevalidation() {
            if (Page_ClientValidate("Vendor")) {
                ShowLoader();
            }
        }
        function checkWebsiteURL(txtcontrol) {
            var txtWebURL = $get(txtcontrol.id);
            if (txtWebURL.value != "") {
                if (!isValidURL(txtWebURL.value)) {
                    alert("Please enter a valid URL");
                    txtWebURL.focus();
                }
            }
        }
        function checkEmailAddress(txtcontrol) {
            var txtEmail = $get(txtcontrol.id);
            if (txtEmail.value != "") {
                if (!isValidEmail(txtEmail.value)) {
                    alert("Please enter a valid Email Address");
                    txtEmail.focus();
                }
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
                <asp:UpdatePanel runat="server" ID="upnlVendor" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Panel ID="tblVendorList" runat="server" Width="720px" ScrollBars="Auto" Height="430px">
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
                            <div  class="gridviewTable">
                                <asp:GridView ID="grdVendor" runat="server" GridLines="Vertical" AllowPaging="True"
                                    AllowSorting="True" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" PageSize="8"
                                    CellPadding="3" Width="690px" OnSorting="grdVendor_Sorting"
                                    OnRowCommand="grdVendor_RowCommand" OnPageIndexChanging="grdVendor_Paging" AlternatingRowStyle-CssClass="alternateRow">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="40px"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="VendorCode" HeaderText="Vendor Code" SortExpression="VendorCode">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="110px"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="110px">
                                            </ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="VendorName" HeaderText="Vendor Name" SortExpression="VendorName">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="170px"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="170px">
                                            </ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PhoneNo" HeaderText="Phone No">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="180px"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="180px">
                                            </ItemStyle>
                                        </asp:BoundField>
                                        <%--<asp:BoundField DataField="EmailId" HeaderText="Email">
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px" CssClass="gridcellText"></ItemStyle>
                                        </asp:BoundField>--%>
                                        <asp:TemplateField SortExpression="Status" HeaderText="Status">
                                            <ItemTemplate>
                                                <%--<span id="spanStatus" class='<%#fngetImageUrl(Eval("Status").ToString())%>' runat="server" ></span> --%>
                                                <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("Status")%>' CssClass='<%#fngetImageUrl(Eval("Status").ToString())%>'></asp:Label>
                                                <asp:Label ID="lblDepartmentName" runat="server" Text='<%# Bind("VendorName")%>'
                                                    Style="display: none;"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px">
                                            </ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:LinkButton CommandArgument='<%# Bind("VendorId")%>' CssClass="view_icon" ID="lbtnView"
                                                    CommandName="ViewDepartment" runat="server" OnClientClick="ShowLoader();" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton CommandArgument='<%# Bind("VendorId")%>' CssClass="delete_icon" ID="lbtnDelete"
                                                    CommandName="DeleteDepartment" runat="server" OnClientClick="return DeleteConfirmation();" />
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
                        <asp:Panel ID="tblVendor" runat="server" Width="720px" ScrollBars="Auto" Height="600px"
                            Visible="false">
                            <br />
                            <div style="width: 100%; height: 95%;">
                                <table width="690px" cellpadding="3" cellspacing="0" height="100%" style="border: solid 5px rgb(226, 226, 226);">
                                    <tr style="background: rgb(235, 235, 235); border-top: solid 1px #d9d9d9;">
                                        <td align="left" width="690px" valign="top">
                                            <table class="grid-bottom" width="680px" cellpadding="3" cellspacing="0">
                                                <tr>
                                                    <td align="right" width="150px">
                                                        <span class="lblCaption">Vendor Code :</span>
                                                    </td>
                                                    <td align="left" width="200px">
                                                        <asp:TextBox ID="txtVendorCode" runat="server" CssClass="txtBox" TabIndex="1" Width="200px"
                                                            MaxLength="10" onKeypress="return validateCode(event);"></asp:TextBox>
                                                    </td>
                                                    <td align="left">
                                                        <asp:RequiredFieldValidator ID="rfvVendorCode" runat="server" ControlToValidate="txtVendorCode"
                                                            ErrorMessage="*" CssClass="reqField" ToolTip="Please Enter Vendor Code" ValidationGroup="Vendor"
                                                            SetFocusOnError="True"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="lblCaption">Vendor Name : </span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtVendorName" runat="server" CssClass="txtBox" TabIndex="2" Width="200px"
                                                            MaxLength="100" onKeypress="return validateName(event);"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator ID="rfvVendorName" runat="server" ControlToValidate="txtVendorName"
                                                            CssClass="reqField" ErrorMessage="*" ToolTip="Please Enter Vendor Name" ValidationGroup="Vendor"
                                                            SetFocusOnError="True">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" valign="top">
                                                        <span class="lblCaption">Address : </span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtAddress" runat="server" CssClass="txtAddress" TabIndex="3" TextMode="MultiLine"
                                                            MaxLength="200" onkeyDown="return checkTextAreaMaxLength(this,event,'200');"
                                                            onKeypress="return validateAddress(event);" Width="198px"></asp:TextBox>
                                                    </td>
                                                    <td valign="top">
                                                        <asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtAddress"
                                                            CssClass="reqField" ErrorMessage="*" ToolTip="Please Enter Address" ValidationGroup="Vendor"
                                                            SetFocusOnError="True">
                                                        </asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="lblCaption">City : </span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtCity" runat="server" CssClass="txtBox" TabIndex="4" Width="200px"
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
                                                        <asp:DropDownList ID="ddlCountry" CssClass="dropdown" runat="server" ValidationGroup="Vendor"
                                                            AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"
                                                            TabIndex="5" Width="200px">
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
                                                        <asp:DropDownList ID="ddlState" runat="server" ValidationGroup="Vendor" TabIndex="6"
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
                                                        <asp:TextBox ID="txtPostalCode" runat="server" CssClass="txtBox" TabIndex="7" Width="200px"
                                                            MaxLength="6" onKeypress="return validationNumeric(event);"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="lblCaption">Contact Name : </span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtContactName" runat="server" CssClass="txtBox" TabIndex="8" Width="200px"
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
                                                        <asp:TextBox ID="txtPhoneNo" runat="server" CssClass="txtBox" TabIndex="9" Width="200px"
                                                            MaxLength="25" onKeypress="return validatePhoneNo(event);"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="lblCaption">Fax Number : </span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtFaxNo" runat="server" CssClass="txtBox" TabIndex="10" Width="200px"
                                                            MaxLength="20" onKeypress="return validateFAXNo(event);"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="lblCaption">Email Address : </span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="txtBox" TabIndex="11" Width="200px"
                                                            MaxLength="50" onKeypress="return validateEmail(event);" onblur="checkEmailAddress(this)"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="lblCaption">Website Address : </span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtWebsite" runat="server" CssClass="txtBox" TabIndex="12" Width="200px"
                                                            MaxLength="50" onKeypress="return validateWebsite(event);" onblur="checkWebsiteURL(this)"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <span class="lblCaption">Status :</span>
                                                    </td>
                                                    <td align="left">
                                                        <asp:RadioButtonList ID="rbtnStatus" runat="server" TabIndex="13" CausesValidation="false"
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
                                                        <asp:Button ID="btnSave" runat="server" Text="Save" TabIndex="14" CssClass="actionButtons"
                                                            ValidationGroup="Vendor" OnClick="btnSave_Click" OnClientClick="savevalidation();" />
                                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" TabIndex="15" CssClass="actionButtons"
                                                            OnClick="btnDelete_Click" OnClientClick="return DeleteConfirmation();" />
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="16" CssClass="actionButtons"
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
                                        <img src="../images/loader.gif" alt='' />
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
                        <asp:HiddenField ID="hfVendorId" runat="server" Value="0" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
</asp:Content>
