<%@ page title="" language="C#" masterpagefile="~/DocuManager.master" autoeventwireup="true" inherits="user_dataentry, App_Web_f3kf0do4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
    <script type="text/javascript" src="../js/imagepanner.js"> </script>
    <script type="text/javascript" src="../js/CommonFunctions.js"> </script>
    <script language="JavaScript" type="text/javascript">
        //        function RejectConfirmation() {
        //            if (confirm("Are you sure want to delete document?")) {
        //                ShowLoader();
        //                return true;
        //            }
        //            else {
        //                return false;
        //            }
        //        }
        function RejectConfirmation() {
            var txtRejectedComments = $get('<%= txtRejectedComments.ClientID %>');
            txtRejectedComments.value = '';
            $find("ContentPlaceHolder1_mpeReject").show();
            return false;
        }
        function CloseRejectConfirmation() {
            var txtRejectedComments = $get('<%= txtRejectedComments.ClientID %>');
            txtRejectedComments.value = '';
            $find("ContentPlaceHolder1_mpeReject").hide();
            return false;
        }
        function keypressVendorCode() {
            var hdnVendorId = $get('<%= hdnVendorId.ClientID %>');
            var txtVendorCode = $get('<%= txtVendorCode.ClientID %>');
            var txtVendorName = $get('<%= txtVendorName.ClientID %>');
            var txtVendorAddress = $get('<%= txtVendorAddress.ClientID %>');
            hdnVendorId.value = "0";
            txtVendorName.value = "";
            txtVendorAddress.value = "";
        }
        function keyupVendorCode(txtbox) {
            if ((event.keyCode == 8) || (event.keyCode == 46)) {
                keypressVendorCode();
            }
        }
        function acVendorSelected(sender, e) {
            var hdnVendorId = $get('<%= hdnVendorId.ClientID %>');
            var txtVendorCode = $get('<%= txtVendorCode.ClientID %>');
            var txtVendorName = $get('<%= txtVendorName.ClientID %>');
            var txtVendorAddress = $get('<%= txtVendorAddress.ClientID %>');
            var ddlDepartment = $get('<%= ddlDepartment.ClientID %>');

            //alert(e.get_value());

            var strVendorDetails = e.get_value();
            var VendorDetails = strVendorDetails.split("|");

            hdnVendorId.value = VendorDetails[0];
            txtVendorCode.value = VendorDetails[1];
            txtVendorName.value = VendorDetails[2];
            txtVendorAddress.value = VendorDetails[3];

            ddlDepartment.focus();
        }
        function checkDate(sender, args) {
            var toDate = new Date();
            toDate.setMinutes(0);
            toDate.setSeconds(0);
            toDate.setHours(0);
            toDate.setMilliseconds(0);
            if (sender._selectedDate < toDate) {
                alert("You cannot select a day earlier than today!");
                sender._selectedDate = toDate;
                //set the date back to the current date
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }

            //            if (sender._selectedDate < new Date()) {
            //                alert("You cannot select a day earlier than today!");
            //                sender._selectedDate = new Date();
            //                // set the date back to the current date
            //                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            //            }
        }
        function Approvevalidation() {
            if (Page_ClientValidate("DataEntry")) {
                var hdnVendorId = $get('<%= hdnVendorId.ClientID %>');
                var txtVendorCode = $get('<%= txtVendorCode.ClientID %>');
                if (hdnVendorId.value > 0) {
                    ShowLoader();
                    return true;
                }
                else {
                    alert("Invalid Vendor Details");
                    txtVendorCode.focus();
                    return false;
                }
            }
        }
        function RejectDocument() {
            if (Page_ClientValidate("Reject")) {
                $find("ContentPlaceHolder1_mpeReject").hide();
                ShowLoader();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="smDataEntry" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <table cellpadding="0" cellspacing="0" width="720px" height="438px">
        <tr>
            <td align="center" height="420px" valign="top">
                <br />
                <asp:Panel ID="pnlDocumentList" runat="server" Width="720px" ScrollBars="Auto" Height="400px">
                    <div style="width: 690px;">
                        <table cellpadding="2" cellspacing="2">
                            <tr>
                                <td style="width: 175px;" height="85px" align="center">
                                    <asp:ImageButton ID="ibtnNewDocument" runat="server" ImageUrl="../icons/new-document-upload.png"
                                        Width="73px" Height="71px" ToolTip="Upload New Document" OnClick="ibtnNewDocument_Click"
                                        OnClientClick="ShowLoader();" />
                                    <br />
                                    <span class="despanvaluecaption">Upload New Document</span>
                                </td>
                                <td style="width: 175px;" align="center">
                                    <asp:ImageButton ID="ibtnPendingDocument" runat="server" ImageUrl="../icons/Document-pending.png"
                                        Width="73px" Height="71px" ToolTip="Pending Document" OnClick="ibtnPendingDocument_Click"
                                        OnClientClick="ShowLoader();" />
                                    <br />
                                    <span class="despanvaluecaption">Pending Document</span> &nbsp;&nbsp;<asp:Label ID="lblPendingDocument"
                                        runat="server" Text="(0)" CssClass="despanvalue"></asp:Label>
                                </td>
                                <%--<td style="width: 175px;" align="center">
                                    <asp:ImageButton ID="ibtnReeditDocument" runat="server" ImageUrl="~/icons/Document-edit.png"
                                        Width="73px" Height="71px" ToolTip="Reedit Document" OnClick="ibtnReeditDocument_Click"
                                        OnClientClick="ShowLoader();" />
                                    <br />
                                    <span class="despanvaluecaption">Re-edit Document</span> &nbsp;&nbsp;<asp:Label ID="lblReeditDocument"
                                        runat="server" Text="(0)" CssClass="despanvalue"></asp:Label>
                                </td>--%>
                                <td style="width: 175px;" align="center">
                                    <asp:ImageButton ID="ibtnRejectedDocument" runat="server" ImageUrl="../icons/Document-Rejected.png"
                                        Width="73px" Height="71px" ToolTip="Rejected Document" OnClick="ibtnRejectedDocument_Click"
                                        OnClientClick="ShowLoader();" />
                                    <br />
                                    <span class="despanvaluecaption">Rejected Document</span> &nbsp;&nbsp;<asp:Label
                                        ID="lblRejected" runat="server" Text="(0)" CssClass="despanvalue"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <div class="gridviewTable">
                        <asp:GridView ID="grvDataEntry" runat="server" GridLines="None" AllowPaging="True"
                            AllowSorting="True" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" PageSize="10"
                            CellPadding="3" Width="690px" OnRowCommand="grvDataEntry_RowCommand" OnPageIndexChanging="grvDataEntry_Paging"
                            AlternatingRowStyle-CssClass="alternateRow">
                            <%-- OnSorting="grvDataEntry_Sorting" --%>
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="40px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="40px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:BoundField DataField="DocumentNo" HeaderText="Document No" SortExpression="DocumentNo">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="OrginalFileName" HeaderText="File Name" SortExpression="OrginalFileName">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="190px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="190px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="UploadedBy" HeaderText="Uploaded By" SortExpression="UploadedBy">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="UploadedOn" HeaderText="Uploaded On" SortExpression="UploadedOn">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="160px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="160px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="UploadType" HeaderText="Mode" SortExpression="UploadType">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="View">
                                    <ItemTemplate>
                                        <asp:LinkButton CommandArgument='<%# Bind("DocumentId")%>' CssClass="view_icon" ID="lbtnDocument"
                                            CommandName="ViewDocument" runat="server" OnClientClick="ShowLoader();" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px"></HeaderStyle>
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
                <asp:Panel ID="pnlDataEntry" runat="server" Width="710px" ScrollBars="Auto" Height="650px"
                    Visible="false" cellpadding="3" cellspacing="3">
                    <table>
                        <tr>
                            <td style="background: rgb(235, 235, 235); padding: 10px;" colspan="2">
                                <table width="100%" cellpadding="0" cellspacing="0" id="tblDataEntry" runat="server" visible="True">
                                    <tr>
                                        <td align="left" width="100px" valign="middle" height="25px">
                                            <span class="lblDECaption">Document No : </span>
                                        </td>
                                        <td align="left" width="200px" valign="middle">
                                            <asp:Label ID="lblDocumentNo" runat="server" Text="" ForeColor="#0d717f" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="left" width="80px" valign="middle">
                                            <span class="lblDECaption">File Name : </span>
                                        </td>
                                        <td align="left" width="300px" valign="middle">
                                            <asp:Label ID="lblFileName" runat="server" Text="" ForeColor="#0d717f" Font-Bold="true"></asp:Label>
                                            <asp:Label ID="lblDocFileName" runat="server" Text="" ForeColor="#0d717f" Font-Bold="true"
                                                Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="middle" height="25px">
                                            <span class="lblDECaption">Uploaded By : </span>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:Label ID="lblUploadedBy" runat="server" Text="" ForeColor="#0d717f" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="left" valign="middle">
                                        </td>
                                        <td align="left" valign="middle">
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" cellpadding="0" cellspacing="0" id="tblUpload" runat="server" visible="false">
                                    <tr>
                                        <td align="left" width="150px">
                                            <span class="lblCaptionNormal">Select files to Upload :</span>
                                        </td>
                                        <td align="left" width="450px">
                                            <input type="text" id="fileName" class="file_input_textbox" readonly="readonly">
                                            <div class="file_input_div">
                                                <input type="button" value="" class="file_input_button" style="margin-top: 25px;" />
                                                <asp:FileUpload ID="fuDocument" runat="server" TabIndex="1" class="file_input_hidden"
                                                    onchange="javascript: document.getElementById('fileName').value = this.value" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td align="left">
                                            &nbsp;&nbsp;<asp:RequiredFieldValidator ID="rfvUpload" runat="server" ControlToValidate="fuDocument"
                                                ErrorMessage="Please Select files to Upload" CssClass="reqField" ToolTip="Please Select files to Upload"
                                                ValidationGroup="DataEntry"  SetFocusOnError="True"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" height="10px">
                            </td>
                        </tr>
                        <tr>
                            <td width="300px" valign="top" style="border: solid 1px #d7d7d7; background: #e9e9e9;">
                                <table cellpadding="0" cellspacing="0" width="300px">
                                    <tr>
                                        <td height="20px" colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" width="130px" valign="top" height="35px">
                                            <span class="lblCaption">Bill No : </span>
                                        </td>
                                        <td align="left" width="160px" valign="top">
                                            <asp:TextBox ID="txtBillNo" runat="server" CssClass="txtBox" TabIndex="1" Width="150px"
                                                MaxLength="10" onKeypress="return validateCode(event);"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:RequiredFieldValidator ID="rfvBillNo" runat="server" ControlToValidate="txtBillNo"
                                                CssClass="reqField" ErrorMessage="*" ToolTip="Please Enter Bill No" ValidationGroup="DataEntry"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" height="35px">
                                            <span class="lblCaption">Bill Date : </span>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtBillDate" runat="server" CssClass="txtBox" TabIndex="2" Width="100px"
                                                onKeypress="return validateDate(event);" MaxLength="10"></asp:TextBox>
                                            <asp:ImageButton ID="imgbtnCalendar" runat="server" ImageUrl="~/images/calendar.jpg" />
                                            <ajaxTK:CalendarExtender ID="calBillDate" TargetControlID="txtBillDate" PopupButtonID="imgbtnCalendar"
                                                runat="server" Format="dd/MM/yyyy" />
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:RequiredFieldValidator ID="rfvBillDate" runat="server" ControlToValidate="txtBillDate"
                                                CssClass="reqField" ErrorMessage="*" ToolTip="Please Select Bill Date" ValidationGroup="DataEntry"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" height="50px">
                                            <span class="lblCaption">Vendor Code : </span>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtVendorCode" runat="server" CssClass="txtBox" TabIndex="3" Width="150px"
                                                AutoComplete="off" onkeypress="keypressVendorCode();" onkeyup="keyupVendorCode(this);"></asp:TextBox>
                                            <br />
                                            <span style="font-size: 10px;">( Autocomplete Textbox ) </span>
                                            <asp:HiddenField ID="hdnVendorId" runat="server" />
                                            <div id="divwidth">
                                            </div>
                                            <ajaxTK:AutoCompleteExtender ID="acVendor" runat="server" DelimiterCharacters=""
                                                Enabled="True" ServiceMethod="GetVendorDetails" TargetControlID="txtVendorCode"
                                                MinimumPrefixLength="1" CompletionInterval="1000" EnableCaching="true" CompletionSetCount="1"
                                                CompletionListCssClass="AutoExtender" CompletionListItemCssClass="AutoExtenderList"
                                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight" CompletionListElementID="divwidth"
                                                OnClientItemSelected="acVendorSelected" FirstRowSelected="false">
                                            </ajaxTK:AutoCompleteExtender>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:RequiredFieldValidator ID="rfvVendorCode" runat="server" ControlToValidate="txtVendorCode"
                                                CssClass="reqField" ErrorMessage="*" ToolTip="Please Enter Vendor Code" ValidationGroup="DataEntry"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" height="35px">
                                            <span class="lblCaption">Vendor Name : </span>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtVendorName" runat="server" CssClass="txtBox" TabIndex="4" Width="150px"
                                                onKeypress="return false;" Enabled="false"></asp:TextBox>
                                            <%--<asp:Label ID="txtVendorName" runat="server" Text=""></asp:Label>--%>
                                        </td>
                                        <td align="left" valign="top">
                                            <%--<asp:RequiredFieldValidator ID="rfvVendorName" runat="server" ControlToValidate="txtVendorName"
                                                CssClass="reqField" ErrorMessage="*" ToolTip="Please Enter Vendor Name" ValidationGroup="DataEntry"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" height="65px">
                                            <span class="lblCaption">Vendor Address : </span>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtVendorAddress" runat="server" TabIndex="5" Width="150px" CssClass="txtAddress"
                                                onKeypress="return false;" Height="50px" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                            <%--<asp:Label ID="txtVendorAddress" runat="server" Text=""></asp:Label>--%>
                                        </td>
                                        <td align="left" valign="top">
                                            <%--<asp:RequiredFieldValidator ID="rfvVendorAddress" runat="server" ControlToValidate="txtVendorAddress"
                                                CssClass="reqField" ErrorMessage="*" ToolTip="Please Enter Vendor Address" ValidationGroup="DataEntry"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" height="35px">
                                            <span class="lblCaption">Department : </span>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:DropDownList ID="ddlDepartment" CssClass="dropdown" runat="server" TabIndex="6"
                                                Width="155px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" ControlToValidate="ddlDepartment"
                                                CssClass="reqField" ErrorMessage="*" ToolTip="Please Select Department" ValidationGroup="DataEntry"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" height="35px">
                                            <span class="lblCaption">Initiator : </span>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:DropDownList ID="ddlInitiator" CssClass="dropdown" runat="server" TabIndex="7"
                                                Width="155px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:RequiredFieldValidator ID="rfvInitiator" runat="server" ControlToValidate="ddlInitiator"
                                                CssClass="reqField" ErrorMessage="*" ToolTip="Please Select Initiator" ValidationGroup="DataEntry"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" height="35px">
                                            <span class="lblCaption">Bill Value : </span>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtBillValue" runat="server" CssClass="txtBox" TabIndex="8" Width="150px"
                                                MaxLength="10" OnKeypress="return validationCurrency(event);"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:RequiredFieldValidator ID="rfvBillValue" runat="server" ControlToValidate="txtBillValue"
                                                CssClass="reqField" ErrorMessage="*" ToolTip="Please Enter Bill Value" ValidationGroup="DataEntry"
                                                SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" height="35px">
                                            <span class="lblCaption">Bill Value Approved : </span>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:DropDownList ID="ddlBillValueApproved" CssClass="dropdown" runat="server" TabIndex="9"
                                                AutoPostBack="false" Width="155px">
                                                <asp:ListItem Value="0" Selected="True">:: Select ::</asp:ListItem>
                                                <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                                <asp:ListItem Value="No">No</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" valign="top">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" height="65px">
                                            <span class="lblCaption">Purpose : </span>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtPurpose" runat="server" CssClass="txtAddress" TabIndex="10" Width="150px"
                                                Height="50px" TextMode="MultiLine" onkeyDown="return checkTextAreaMaxLength(this,event,'150');"
                                                onKeypress="return validateAddress(event);"></asp:TextBox>
                                        </td>
                                        <td align="left" valign="top">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" height="35px">
                                            <span class="lblCaption">Type of Payment : </span>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:DropDownList ID="ddlTypeOfPayment" CssClass="dropdown" runat="server" TabIndex="11"
                                                AutoPostBack="false" Width="155px">
                                                <asp:ListItem Value="" Selected="True">:: Select ::</asp:ListItem>
                                                <asp:ListItem Value="Advance">Advance</asp:ListItem>
                                                <asp:ListItem Value="Interim">Interim</asp:ListItem>
                                                <asp:ListItem Value="Final Payment">Final Payment</asp:ListItem>
                                                <asp:ListItem Value="Penalty">Penalty</asp:ListItem>
                                                <asp:ListItem Value="Others">Others</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:RequiredFieldValidator ID="rfvPaymentType" runat="server" ControlToValidate="ddlTypeOfPayment"
                                                CssClass="reqField" ErrorMessage="*" ToolTip="Please Select Type of Payment"
                                                ValidationGroup="DataEntry" SetFocusOnError="True">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td align="right" valign="top">
                                            <span class="lblCaption">Approval Status : </span>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:DropDownList ID="ddlStatus" CssClass="dropdownlist" runat="server" TabIndex="9"
                                                AutoPostBack="true" Width="200px">
                                                <asp:ListItem Value="0" Selected="True">:: Select ::</asp:ListItem>
                                                <asp:ListItem Value="1">Approved</asp:ListItem>
                                                <asp:ListItem Value="2">Rejected</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" valign="top">
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td style="padding-left: 10px;" align="right" colspan="3" valign="top" height="30px">
                                            <div style="float: right;">
                                                <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="DataEntry" TabIndex="12"
                                                    OnClick="btnSave_Click" CssClass="actionButtons" OnClientClick="return Approvevalidation();" />
                                                &nbsp;<asp:Button ID="btnReject" runat="server" Text="Delete" OnClientClick="return RejectConfirmation();"
                                                    TabIndex="13" CssClass="actionButtons" />
                                                &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                                    TabIndex="14" CssClass="actionButtons" OnClientClick="ShowLoader();" /></div>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="420px" valign="top">
                                <div class="retrievalImageView">
                                    <table width="100%" cellpadding="0" cellspacing="0" runat="server" id="tblImageViewer"
                                        visible="true">
                                        <tr>
                                            <td valign="middle" align="center">
                                                <div class="pancontainer" data-orient="center" data-canzoom="yes" style="width: 360px;
                                                    height: 535px;">
                                                    <div style="position: relative; top: 5px;">
                                                        <asp:Image ID="imgScannedImage" runat="server" ImageUrl="~/Images/NoImage.jpg" Width="340px"
                                                            Height="465px" />
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="100%" cellpadding="0" cellspacing="0" runat="server" id="tblPDFViewer"
                                        visible="false">
                                        <tr>
                                            <td valign="middle" align="center">
                                                <div class="pancontainer" data-orient="center" data-canzoom="yes" style="width: 360px;
                                                    height: 535px;">
                                                    <div style="background: #f5f5f5; position: absolute; width: 31px; height: 30px; right: 10px;
                                                        top: 10px; z-index: 10;">
                                                    </div>
                                                    <iframe runat="server" id="IFViewer" width="340" height="420" style="border: none;
                                                        margin-top: 10px;" src=""></iframe>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hfViewDocumentType" runat="server" />
    <asp:HiddenField ID="hfDocumentId" runat="server" Value="0" />
    <asp:HiddenField ID="hfDocumentNo" runat="server" Value="0" />
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
    <input type="button" id="btnModalRejectOk" runat="server" value="OkButton" style="display: none;" />
    <input type="button" id="btnModalRejectCancel" runat="server" value="CancelBut" style="display: none;" />
    <ajaxTK:ModalPopupExtender ID="mpeReject" runat="server" TargetControlID="btnModalRejectOk"
        PopupControlID="pnlRejectData" BackgroundCssClass="loaderModalBackground" OkControlID="btnModalRejectOk"
        CancelControlID="btnModalRejectCancel">
    </ajaxTK:ModalPopupExtender>
    <asp:Panel ID="pnlRejectData" runat="server" BackColor="#F0F0F0" Style="width: 500px;
        height: 220px; border: 3px solid #0B8899; display: none;">
        <table>
            <tr>
                <td align="center">
                    <%--<input id="Image1" onclick="return btnModalClose_onclick();" src="../icons/close-icon.png"  type="image" />--%>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <table cellpadding="3" cellspacing="3" style="width: 95%; height: 85%;">
                        <tr>
                            <td align="left" colspan="3">
                                <strong>Document will be deleted from the database permenantly and cannot be reselected.
                                    Do you want to proceed with it?</strong>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="3">
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" width="100px" valign="top">
                                Comments :
                            </td>
                            <td align="left" width="200px">
                                <asp:TextBox ID="txtRejectedComments" runat="server" TextMode="MultiLine" Height="80px"
                                    Width="250px" TabIndex="10" CssClass="txtAddress" ></asp:TextBox>
                            </td>
                            <td valign="top" align="left">
                                &nbsp;
                                <asp:RequiredFieldValidator ID="rfvRejectComments" runat="server" ControlToValidate="txtRejectedComments"
                                    CssClass="reqField" ErrorMessage="*" ToolTip="Please Enter Comments" ValidationGroup="Reject"
                                    SetFocusOnError="True">
                                </asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                            </td>
                            <td align="left" colspan="2">
                                <asp:Button ID="btnRejectDoc" runat="server" Text="Ok" OnClick="btnReject_Click"
                                    TabIndex="11" CssClass="actionButtons" ValidationGroup="Reject" OnClientClick="RejectDocument();" />
                                &nbsp;<asp:Button ID="btnCancelDoc" runat="server" Text="Cancel" OnClientClick="return CloseRejectConfirmation();"
                                    TabIndex="12" CssClass="actionButtons" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
