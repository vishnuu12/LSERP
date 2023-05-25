<%@ page title="" language="C#" masterpagefile="~/DocuManager.master" autoeventwireup="true" inherits="user_approveDocuments, App_Web_f3kf0do4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>
    <script type="text/javascript" src="../js/imagepanner.js"> </script>
    <script type="text/javascript" src="../js/CommonFunctions.js"> </script>
    <script language="JavaScript" type="text/javascript">
        function RejectConfirmation() {
            //            if (confirm("Are you sure want to reject document?")) {
            //                ShowLoader();
            //                return true;
            //            }
            //            else {
            //                return false;
            //            }
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
        function RejectDocument() {
            if (Page_ClientValidate("Reject")) {
                ShowLoader();
            }
        }
    </script>
    <style type="text/css">
        /*Default CSS for pan containers*/
        .pancontainer
        {
            position: relative; /*keep this intact*/
            overflow: hidden; /*keep this intact*/
            width: 240px;
            height: 240px;
            border: 0px solid black;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="smApproveDoc" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <table cellpadding="3" cellspacing="3" width="720px" height="438px">
        <tr>
            <td align="center" height="420px" valign="top">
                <br />
                <asp:Panel ID="pnlDocumentList" runat="server" Width="720px" ScrollBars="Auto" Height="400px">
                    <div style="width: 690px;">
                        <%-- background:#ebebeb;border: solid 1px #d9d9d9; background:#ebebeb;border: solid 1px #d9d9d9; --%>
                        <table cellpadding="2" cellspacing="2">
                            <tr>
                                <td style="width: 175px;" align="center">
                                </td>
                                <td style="width: 175px;" align="center">
                                    <asp:ImageButton ID="ibtnPendingDocument" runat="server" ImageUrl="../icons/Document-pending.png"
                                        Width="73px" Height="71px" ToolTip="Pending Document" OnClick="ibtnPendingDocument_Click"
                                        OnClientClick="ShowLoader();" />
                                    <br />
                                    <span class="despanvaluecaption">Pending Documents</span> &nbsp;&nbsp;<asp:Label ID="lblPendingDocument"
                                        runat="server" Text="(0)" CssClass="despanvalue"></asp:Label>
                                </td>
                                <td style="width: 175px;" align="center">
                                    <asp:ImageButton ID="ibtnRejectedDocument" runat="server" ImageUrl="../icons/Document-Rejected.png"
                                        Width="73px" Height="71px" ToolTip="Rejected Document" OnClick="ibtnRejectedDocument_Click"
                                        OnClientClick="ShowLoader();" />
                                    <br />
                                    <span class="despanvaluecaption">Rejected Documents</span> &nbsp;&nbsp;<asp:Label
                                        ID="lblRejected" runat="server" Text="(0)" CssClass="despanvalue"></asp:Label>
                                </td>
                                <td style="width: 175px;" align="center">
                                    <%--<asp:ImageButton ID="ibtnReviewDocument" runat="server" ImageUrl="../icons/Document-Rejected.png"
                                        Width="73px" Height="71px" ToolTip="Rejected Document" OnClick="ibtnReviewDocument_Click"
                                        OnClientClick="alert('under Process');" />
                                    <br />
                                    <span class="despanvaluecaption">Review Documents</span> &nbsp;&nbsp;<asp:Label
                                        ID="lblReview" runat="server" Text="(0)" CssClass="despanvalue"></asp:Label>--%>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <div class="gridviewTable">
                        <%--<asp:GridView ID="grvDataEntry" runat="server" GridLines="None" AllowPaging="True"
                            AllowSorting="True" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" PageSize="10"
                            CellPadding="3" Width="690px" OnRowCommand="grvDataEntry_RowCommand" OnPageIndexChanging="grvDataEntry_Paging"
                            AlternatingRowStyle-CssClass="alternateRow">
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
                                <asp:BoundField DataField="BillNo" HeaderText="Bill No" SortExpression="BillNo">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="80px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="BillDate" HeaderText="Bill Date" SortExpression="BillDate">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="90px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="90px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="BillValue" HeaderText="BillValue" SortExpression="BillValue">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="80px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DataEntryBy" HeaderText="DataEntry By" SortExpression="DataEntryBy">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DataEntryOn" HeaderText="DataEntry On" SortExpression="DataEntryOn">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="View">
                                    <ItemTemplate>
                                        <asp:LinkButton CommandArgument='<%# Bind("DocumentId")%>' CssClass="view_icon" ID="lbtnDocument"
                                            CommandName="ViewDocument" runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="50px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Width="50px"></ItemStyle>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                No records to display
                            </EmptyDataTemplate>
                            <PagerStyle CssClass="pagination" />
                        </asp:GridView>--%>
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
                                <asp:BoundField DataField="BillNo" HeaderText="Bill No" SortExpression="BillNo">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="80px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="BillDate" HeaderText="Bill Date" SortExpression="BillDate">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="90px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="90px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="BillValue" HeaderText="BillValue" SortExpression="BillValue">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="80px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="80px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DataEntryBy" HeaderText="DataEntry By" SortExpression="DataEntryBy">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="100px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="100px"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DataEntryOn" HeaderText="DataEntry On" SortExpression="DataEntryOn">
                                    <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="150px"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="150px"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="View">
                                    <ItemTemplate>
                                        <asp:LinkButton CommandArgument='<%# Bind("DocumentId")%>' CssClass="view_icon" ID="lbtnDocument"
                                            CommandName="ViewDocument" runat="server" />
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
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td align="left" width="100px" valign="middle" height="25px">
                                            <span class="lblDECaption">Document No : </span>
                                        </td>
                                        <td align="left" width="200px" valign="middle">
                                            <asp:Label ID="lblDocumentNo" runat="server" Text="" ForeColor="#0d717f" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td align="left" width="120px" valign="middle">
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
                                            <span class="lblDECaption">Data Entry By : </span>
                                        </td>
                                        <td align="left" valign="middle">
                                            <asp:Label ID="lblDataEntryBy" runat="server" Text="" ForeColor="#0d717f" Font-Bold="true"></asp:Label>
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
                                            <span class="lblCaptionBold">Bill No : </span>
                                        </td>
                                        <td align="left" width="160px" valign="top">
                                            <asp:Label ID="lblBillNo" runat="server" Text="" CssClass="approveLabel"></asp:Label>
                                        </td>
                                        <td align="left" valign="top">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" height="35px">
                                            <span class="lblCaptionBold">Bill Date : </span>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblBillDate" runat="server" Text="" CssClass="approveLabel"></asp:Label>
                                        </td>
                                        <td align="left" valign="top">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" height="35px">
                                            <span class="lblCaptionBold">Vendor Code : </span>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblVendorCode" runat="server" Text="" CssClass="approveLabel"></asp:Label>
                                        </td>
                                        <td align="left" valign="top">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" height="35px">
                                            <span class="lblCaptionBold">Vendor Name : </span>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblVendorName" runat="server" Text="" CssClass="approveLabel"></asp:Label>
                                        </td>
                                        <td align="left" valign="top">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" height="55px">
                                            <span class="lblCaptionBold">Vendor Address : </span>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblVendorAddress" runat="server" Text="" CssClass="approveLabel"></asp:Label>
                                        </td>
                                        <td align="left" valign="top">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" height="35px">
                                            <span class="lblCaptionBold">Department : </span>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblDepartment" runat="server" Text="" CssClass="approveLabel"></asp:Label>
                                        </td>
                                        <td align="left" valign="top">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" height="35px">
                                            <span class="lblCaptionBold">Bill Value : </span>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblBillValue" runat="server" Text="" CssClass="approveLabel"></asp:Label>
                                        </td>
                                        <td align="left" valign="top">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" height="35px">
                                            <span class="lblCaptionBold">Bill Value Approved : </span>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblBillValueApproved" runat="server" Text="" CssClass="approveLabel"></asp:Label>
                                        </td>
                                        <td align="left" valign="top">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" height="55px">
                                            <span class="lblCaptionBold">Purpose : </span>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblPurpose" runat="server" Text="" CssClass="approveLabel"></asp:Label>
                                        </td>
                                        <td align="left" valign="top">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" valign="top" height="35px">
                                            <span class="lblCaptionBold">Type of Payment : </span>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:Label ID="lblPaymentType" runat="server" Text="" CssClass="approveLabel"></asp:Label>
                                        </td>
                                        <td align="left" valign="top">
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
                                                <asp:Button ID="btnApprove" runat="server" Text="Approve" TabIndex="10" CssClass="actionButtons"
                                                    OnClick="btnApprove_Click" OnClientClick="ShowLoader();" />
                                                &nbsp;<asp:Button ID="btnReject" runat="server" Text="Reject"  TabIndex="11" CssClass="actionButtons" OnClientClick="return RejectConfirmation();" />  <%--OnClick="btnReject_Click"
                                                    OnClientClick="return RejectConfirmation();"--%>
                                                &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                                    TabIndex="12" CssClass="actionButtons" />
                                            </div>
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
        height: 230px;border:3px solid #0B8899;display:none;" >
        <table>
            <%--<tr>
                <td align="center">
                    <input id="Image1" onclick="return CloseRejectConfirmation();" src="../icons/close-icon.png"  type="image" />
                </td>
            </tr>--%>
            <tr>
                <td align="center">
                       <table cellpadding="3" cellspacing="3" style="width: 480px; height: 85%;">
                            <tr>
                                <td align="left" colspan="2">
                                    <strong>Are you sure want to reject the Document?</strong>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2">
                                    <hr />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="130px" valign="top">
                                    Reject Comments :
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtRejectedComments" runat="server" TextMode="MultiLine" Height="80px"
                                        Width="250px" TabIndex="10" CssClass="txtAddress"></asp:TextBox>
                                    &nbsp;
                                 <asp:RequiredFieldValidator ID="rfvRejectComments" runat="server" ControlToValidate="txtRejectedComments"
                                    CssClass="reqField" ErrorMessage="*" ToolTip="Please Enter Reject Comments" ValidationGroup="Reject"
                                    SetFocusOnError="True"></asp:RequiredFieldValidator> 
                                </td>
                            </tr>
                            <tr>
                                <td align="left" width="130px" valign="top">
                                </td> 
                                <td align="left">
                                    <asp:Button ID="btnRejectDoc" runat="server" Text="Reject" OnClick="btnReject_Click" TabIndex="11" CssClass="actionButtons" ValidationGroup="Reject" OnClientClick="RejectDocument();" />
                                    &nbsp;<asp:Button ID="btnCancelDoc" runat="server" Text="Cancel" OnClientClick="return CloseRejectConfirmation();"
                                        TabIndex="12" CssClass="actionButtons" />
                                </td>
                            </tr>
                        </table>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:HiddenField ID="hfViewDocumentType" runat="server" />
    <asp:HiddenField ID="hfDocumentId" runat="server" Value="0" />
</asp:Content>
