<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="ViewMessage.aspx.cs" Inherits="Pages_ViewMessage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function ShowReplyPopUp(element) {
            $('#mpeView').modal({
                backdrop: 'static'
            });
            var ECID = element.id.split('_');
            $('#<%=hdnECID.ClientID %>').attr('value', ECID[2]);
            $('#<%=lblEnquiryNumber_V.ClientID%>').html($('#<%=ddlEnquiry_Customer_OrderNumber.ClientID%> option:selected').text());
            return false;
        }

        function ValidateReplyMessage() {
            var txtMessage = $('#<%=txtMessage.ClientID %>');
            if (txtMessage.val() == "") {
                txtMessage.notify('Field Required.', { arrowShow: true, position: 't,r', autoHide: true });
                return false;
            }
            else {
                showLoader();
                return true;
            }
        }

        function ValidateControl(controlID) {
            var message = controlID.split('/');
            $('#' + message[0]).notify(message[1], { arrowShow: true, position: 'r,r', autoHide: true, autoHideDelay: 5000 });

        }

        function ShowClosePopUp() {
            $("#mpeView").modal("hide");
        }


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
                                    <h3 class="page-title-head d-inline-block">
                                        View Message</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">MyCorner</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">View Message</li>
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
            <asp:UpdatePanel ID="upDocumenttype" runat="server">
                <Triggers>
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divInput" runat="server">
                            <div class="ip-div text-left">
                                <div class="col-sm-4">
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlEnquiry_Customer_OrderNumber" OnSelectedIndexChanged="ddlEnquiry_Customer_OrderNumber_SelectedIndexChanged"
                                        CssClass="form-control" runat="server" OnChange="showLoader();" AutoPostBack="true"
                                        ToolTip="Select Enquiry/Cust Number">
                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                </div>
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div id="DivReceiver" runat="server" class="col-sm-12">
                                        </div>
                                        <div id="DivSender" runat="server" class="col-sm-12">
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
    <div class="modal" id="mpeView">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="Updateview" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H1">
                                <div class="text-center">
                                    Send Communication /
                                    <asp:Label ID="lblEnquiryNumber_V" Text="" runat="server"></asp:Label>
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal">
                                x</button>
                        </div>
                        <div class="modal-body">
                            <div class="inner-container">
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4">
                                        <label class="form-label">
                                            Header</label>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtHeader" runat="server" TextMode="MultiLine" Rows="1" Width="200%"
                                            placeHolder="Enter Header" ToolTip="Enter Header"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4">
                                        <label class="form-label">
                                            Add Message</label>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Rows="3" Width="200%"
                                            placeHolder="Enter Message" ToolTip="Enter Message"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="col-sm-12 text-center">
                                <asp:LinkButton ID="btnSendMail" Text="Send Mail" OnClientClick="return ValidateReplyMessage();"
                                    OnClick="btnReplyMessage_Click" CssClass="btn btn-cons btn-save  AlignTop" runat="server" />
                                <%--  <asp:LinkButton ID="lbtnCancel" runat="server" Text="Cancel" CssClass="btn btn-cons btn-danger AlignTop"
                                    OnClientClick="return ClearFields();"></asp:LinkButton>--%>
                            </div>
                        </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:HiddenField ID="hdnECID" runat="server" Value="" />
            </div>
        </div>
    </div>
</asp:Content>
