<%@ page title="" language="C#" masterpagefile="~/DocuManager.master" autoeventwireup="true" inherits="user_uploaddocument, App_Web_f3kf0do4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="JavaScript" type="text/javascript">
        function uploadvalidation() {
            if (Page_ClientValidate("Upload")) {
                ShowLoader();
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <asp:ScriptManager ID="smUpload" runat="server">
    </asp:ScriptManager>
    <table cellpadding="2" cellspacing="2" width="720px" height="438px">
        <tr>
            <td align="center" valign="top">
                <table cellpadding="2" cellspacing="2" width="600px">
                    <tr>
                        <td align="left" colspan="3" height="80px">
                        </td>
                    </tr>
                    <tr>
                        <td align="left" width="150px">
                            <span class="lblCaptionNormal">Select files to Upload :</span>
                        </td>
                        <td align="left" width="450px">
                            <input type="text" id="fileName" class="file_input_textbox" readonly="readonly">
                            <div class="file_input_div">
                                <input type="button" value="" class="file_input_button" />
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
                                ValidationGroup="Upload"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                        </td>
                        <td align="left">
                            <asp:ImageButton ID="btnUpload" runat="server" ImageUrl="~/images/upload_img.png"
                                ToolTip="Upload Document" ValidationGroup="Upload" OnClick="btnUpload_Click" OnClientClick="uploadvalidation();" />
                            <%-- <asp:Button ID="btnUpload" runat="server" Text="Upload" TabIndex="2" CssClass="actionButtons"
                                Width="80px" ValidationGroup="Upload" OnClick="btnUpload_Click" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" height="150px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
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
</asp:Content>
