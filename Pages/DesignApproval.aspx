<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="DesignApproval.aspx.cs" Inherits="Pages_DesignApproval" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function ShowViewPopUp() {
            $('#mpeView').modal({
                backdrop: 'static'
            });
            return false;
        }
        function checkboxselection(ele, replybtn) {
            if ($('#ContentPlaceHolder1_gvDesignApprovalDetails').find('input:checked').length < 1) {
                InfoMessage('No Item Slected');
                return false;
            }
          <%--  else if (replybtn == 'replybtn') {
                $('#mpeReplyPopUp').modal({
                    backdrop: 'static'
                });
                var resID = elmid.id.split("_");
                $('#<%=hdnRowIndex.ClientID %>').attr('value', resID[3]);
                return false;
            }--%>
        }

        function RevisionChange() {
            var msg = Mandatorycheck('ContentPlaceHolder1_divdesignapprove');
            if (msg) {
                if ($('#ContentPlaceHolder1_gvDesignApprovalDetails').find('input:checked').length < 1) {
                    InfoMessage('No Item Slected');
                    hideLoader();
                    return false;
                }
                else {
                    swal({
                        title: "Are you sure?",
                        text: "If Yes, Once Revision Changed No Further Edit",
                        type: "warning",
                        showCancelButton: true,
                        confirmButtonColor: "#DD6B55",
                        confirmButtonText: "Yes, Create it!",
                        closeOnConfirm: false
                    }, function () {
                        //showLoader();
                        __doPostBack('NewRevision', "0");
                    });
                    hideLoader();
                    return false;
                }
            }
            else
                return false;
        }

        function ValidateMessage() {
            var msg = true;
            if ($('#<%=txtHeader_R.ClientID %>').val() == "") {
                $('#<%=txtHeader_R.ClientID %>').notify('Fields Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }
            if ($('#<%=txtMessage_R.ClientID %>').val() == "") {
                $('#<%=txtMessage_R.ClientID %>').notify('Fields Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }
            if (msg) {
                showLoader();
                return true;
            }
            else {
                return false;
            }
        }
        function checkallrevison(ele) {
            debugger;
            if ($(ele).is(":checked")) {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', true);
                $('#ContentPlaceHolder1_gvDesignApprovalDetails_chkall').closest('table').find('select').addClass("mandatoryfield");
            }
            else {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', false);
                $('#ContentPlaceHolder1_gvDesignApprovalDetails_chkall').closest('table').find('select').removeClass("mandatoryfield");
            }
        }

        function MandatoryField(ele) {
            if ($(ele).is(":checked")) {
                $(ele).closest('tr').find('select').addClass("mandatoryfield");
                $(ele).closest('tr').find('select').before('<span class="textboxmandatory" style="color: red;">*</span>');
            }
            else {
                $(ele).closest('tr').find('select').removeClass("mandatoryfield");
                $(ele).closest('tr').find('select').closest('td').find('.textboxmandatory').remove()
            }
        }

        //        function ShowDataTable() {
        //            $('#<%=gvDesignApprovalDetails.ClientID %>').DataTable();
        //        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container main-container_close">
        <div class="app-main__outer">
            <div class="app-main__inner">
                <div class="app-page-title">
                    <div class="page-title-left page-title-wrapper">
                        <div class="page-title-heading">
                            <div>
                                <div class="page-title-head center-elem">
                                    <span class="d-inline-block pr-2">
                                        <%--<i class="fa fa-th-large opacity-6 yellow"></i>--%>
                                        <img src="../Assets/images/pages.png" alt="" width="16px" />
                                    </span>
                                    <h3 class="page-title-head d-inline-block">Drawing Revision Change</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Leads & Enquiry</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Design Approval</li>
                                                </ol>
                                     </nav>
                        <a id="help" href="" alt="" style="margin-top: 4px;">
                            <img src="../Assets/images/help.png" />
                        </a>
                    </div>
                </div>
            </div>
        </div>
        <div>
            <asp:UpdatePanel ID="upQuote" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="gvDesignApprovalDetails" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Customer Name</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomerName_SelectIndexChanged"
                                            CssClass="form-control" Width="70%" ToolTip="Select Customer Number">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Enquiry Number</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlEnquiryNumber" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEnquiryNumber_SelectIndexChanged"
                                            CssClass="form-control" Width="70%" ToolTip="Select Enquiry Number">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server" visible="false">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="ip-div text-center">
                                            <asp:Button ID="btnSendMail" CssClass="btn btn-cons btn-save" OnClick="btnSendMail_Click"
                                                OnClientClick="return checkboxselection(this);" runat="server" Text="Send Mail to Customer"></asp:Button>
                                            <asp:Button ID="btnReplyToDesigner" CssClass="btn btn-cons btn-save" runat="server"
                                                OnClientClick="return RevisionChange();" OnClick="btnSendmailReply_Click" Text="Revision Change"></asp:Button>
                                            <asp:Button ID="btnApprove" CssClass="btn btn-cons btn-save" runat="server" OnClientClick="return checkboxselection(this);"
                                                OnClick="btnApprove_Click" Text="Approve"></asp:Button>
                                            <asp:Button ID="btnhold" CssClass="btn btn-cons btn-save" runat="server" OnClientClick="return checkboxselection(this);"
                                                OnClick="btnHold_Click" Text="Hold"></asp:Button>
                                        </div>
                                        <div class="col-sm-12 p-t-10" id="divdesignapprove" runat="server">
                                            <asp:GridView ID="gvDesignApprovalDetails" runat="server" AutoGenerateColumns="False"
                                                OnRowDataBound="gvDesignApprovalDetails_RowDataBound" ShowHeaderWhenEmpty="True"
                                                OnRowCommand="gvDesignApprovalDetails_OnRowCommand" EmptyDataText="No Records Found"
                                                CssClass="table table-hover table-bordered medium" HeaderStyle-HorizontalAlign="Center"
                                                DataKeyNames="FileName,AttachementID,DDID,Designer">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" ControlStyle-CssClass="nosort" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkall" runat="server" onclick="return checkallrevison(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkitems" runat="server" onclick="return MandatoryField(this);" AutoPostBack="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Version Number" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVersionNumber" runat="server" Text='<%# Eval("Version")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Revision Number" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlRevisionNumber" runat="server"
                                                                CssClass="form-control" Width="70%" ToolTip="Select Revision Number">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Name" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitemname" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Size" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitemsize" runat="server" Text='<%# Eval("ItemSize")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SubmittedOn" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSubmittedOn" runat="server" Text='<%# Eval("CreatedOn")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ActionTo Be Taken" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("Status")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgbtnView" runat="server" ImageUrl="../Assets/images/attach.png"
                                                                Width="20px" Height="20px" CommandName="ViewDocs" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'></asp:ImageButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnRowIndex" runat="server" Value="3" />
                                        </div>
                                        <div class="col-sm-12">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal" id="mpeView" style="height: 125%; top: -10%;">
        <div class="modal-dialog" style="height: 100%;">
            <div class="modal-content" style="height: 100%;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" Style="height: 100%;">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btndownloaddocs" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H3">
                                <div class="text-center">
                                    Drawing
                                    <asp:Label ID="Label1" Text="" runat="server"></asp:Label>
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal">
                                x</button>
                        </div>
                        <div class="modal-body" style="height: 100%;">
                            <div class="inner-container" style="height: 100%;">
                                <div class="ip-div text-center" style="margin-bottom: 10px;">
                                    <%-- OnClick="btndownloaddocs_Click"--%>
                                    <asp:LinkButton Text="Download" CssClass="btn btn-save btn-cons AlignTop" ID="btndownloaddocs"
                                        runat="server" />
                                </div>
                                <div class="col-sm-12" style="height: 100%;">
                                    <iframe runat="server" id="ifrm" src="" style="width: 100%; height: 80%;" frameborder="0"></iframe>
                                </div>
                            </div>
                        </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal" id="mpeReplyPopUp">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H1">
                                <div class="text-center">
                                    Reply Message
                                    <asp:Label ID="Label2" Text="" runat="server"></asp:Label>
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal">
                                x</button>
                        </div>
                        <div class="modal-body">
                            <div class="inner-container">
                                <div class="col-sm-12">
                                    <div class="col-sm-4">
                                        <label class="form-label">
                                            Header
                                        </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtHeader_R" runat="server" CssClass="form-control" TextMode="MultiLine"
                                            Rows="1"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4">
                                        <label class="form-label">
                                            Send Message
                                        </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtMessage_R" runat="server" CssClass="form-control" TextMode="MultiLine"
                                            Rows="3"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="col-sm-12">
                                <div class="col-sm-4">
                                </div>
                                <div class="col-sm-4">
                                    <asp:LinkButton ID="btnSendmailR" runat="server" Text="Send Mail" OnClientClick="return ValidateMessage();"
                                        OnClick="btnSendmailReply_Click" CssClass="btn btn-cons btn-save  AlignTop" />
                                </div>
                                <div class="col-sm-4">
                                </div>
                            </div>
                        </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
