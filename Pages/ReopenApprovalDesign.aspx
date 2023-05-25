<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="ReopenApprovalDesign.aspx.cs" Inherits="Pages_ReopenApprovalDesign" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {
            $('#<%=txtReScheduledDate.ClientID %>').datepicker({
                format: 'dd/mm/yyyy'
            });
            $('.datepicker').datepicker({ format: 'dd/mm/yyyy' });
        });

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                $('.datepicker').datepicker({ format: 'dd/mm/yyyy' });
            }
        });

        function ValidateApproval() {
            var msg = true;
            if ($('#<%=txtChangesRequested.ClientID %>').val() == "") {
                $('#<%=txtChangesRequested.ClientID %>').notify('Field Required', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }
            if ($('#<%=rblCommercialImpact.ClientID %> input:checked').val() == "1") {
                if ($('#<%=txtStateAmount.ClientID %>').val() == "") {
                    $('#<%=txtStateAmount.ClientID %>').notify('Field Required', { arrowShow: true, position: 'r,r', autoHide: true });
                    msg = false;
                }
            }
            if ($('#<%=rblDelayIsImplementation.ClientID %> input:checked').val() == "1") {
                if ($('#<%=txtReScheduledDate.ClientID %>').val() == "") {
                    $('#<%=txtReScheduledDate.ClientID %>').notify('Field Required', { arrowShow: true, position: 'r,r', autoHide: true });
                    msg = false;
                }
            }

            if (msg) {
                showLoader();
                return true;
            }
            else {
                return false;
            }
        }

        function btnValidateReason() {
            var msg = true;

            $('#<%=divRejectReason.ClientID %>').css("display", "block");

            if ($('#<%=txtChangesRequested.ClientID %>').val() == "") {
                $('#<%=txtChangesRequested.ClientID %>').notify('Field Required', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }
            if ($('#<%=rblCommercialImpact.ClientID %> input:checked').val() == "1") {
                if ($('#<%=txtStateAmount.ClientID %>').val() == "") {
                    $('#<%=txtStateAmount.ClientID %>').notify('Field Required', { arrowShow: true, position: 'r,r', autoHide: true });
                    msg = false;
                }
            }
            if ($('#<%=rblDelayIsImplementation.ClientID %> input:checked').val() == "1") {
                if ($('#<%=txtReScheduledDate.ClientID %>').val() == "") {
                    $('#<%=txtReScheduledDate.ClientID %>').notify('Field Required', { arrowShow: true, position: 'r,r', autoHide: true });
                    msg = false;
                }
            }

            if ($('#<%=txtRejectReason.ClientID %>').val() == "") {
                $('#<%=txtRejectReason.ClientID %>').notify('Field Required', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }

            if (msg) {
                showLoader();
                return true;
            }
            else
                return false;
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
                                        Approved Design Drawings</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);"><asp:Label ID="lblModuleName" runat="server"></asp:Label></a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Design ReApproval</li>
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
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Enquiry Number</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlEnquiryNumber" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEnquiryNumber_SelectIndexChanged"
                                            CssClass="form-control" Width="70%" OnChange="showLoader();" ToolTip="Select Enquiry Number"
                                            TabIndex="1">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="col-sm-12" id="divCustomerInformation" runat="server" style="background-color: #a6ded9;">
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-3">
                                                    <label class="form-label">
                                                        Customer Name</label>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:Label ID="lblCustomerName" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-3">
                                                    <label class="form-label">
                                                        Enquiry Number</label>
                                                </div>
                                                <div class="col-m-3">
                                                    <asp:Label ID="lblEnquiryNumber" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-3">
                                                    <label class="form-label">
                                                        Version Number</label>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:Label ID="lblVersionNumber" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-3">
                                                    <label class="form-label">
                                                        Approved On</label>
                                                </div>
                                                <div class="col-m-3">
                                                    <asp:Label ID="lblApprovedOn" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-3">
                                                    <label class="form-label">
                                                        Current Status</label>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:Label ID="lblCurrentStatus" runat="server" Text="Approved"></asp:Label>
                                                </div>
                                                <div class="col-sm-3">
                                                </div>
                                                <div class="col-sm-3">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10" id="divSaveApproval" runat="server">
                                            <div class="col-sm-12">
                                                <div class="col-sm-4">
                                                    <label class="form-label">
                                                        Changes Requested</label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:TextBox ID="txtChangesRequested" runat="server" placeHolder="Enter Changes Requested"
                                                        CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-4">
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <label class="form-label">
                                                        Commercial Impact</label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:RadioButtonList ID="rblCommercialImpact" CssClass="radio radio-success" AutoPostBack="true"
                                                        OnSelectedIndexChanged="rblCommercialImpact_SelectedIndexChanged" RepeatDirection="Horizontal"
                                                        runat="server">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="col-sm-4">
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10" id="divSaveStateAmount" runat="server" visible="false">
                                                <div class="col-sm-4">
                                                    <label class="form-label">
                                                        State Amount</label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:TextBox ID="txtStateAmount" runat="server" placeHolder="Enter State Amount"
                                                        onkeypress="return validationNumeric(this);" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-4">
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <label class="form-label">
                                                        Delay In Implementation</label>
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:RadioButtonList ID="rblDelayIsImplementation" CssClass="radio radio-success"
                                                        AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rblDelayIsImplementation_SelectedIndexChanged"
                                                        runat="server">
                                                        <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10" id="divSaveResheduledDate" runat="server" visible="false">
                                                <div class="col-sm-4">
                                                    <label class="form-label">
                                                        ReScheduled Date
                                                    </label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:TextBox ID="txtReScheduledDate" runat="server" placeholder="Enter ReScheduled Date"
                                                        CssClass="form-control datepicker"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-4">
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10" id="divRejectReason" runat="server" style="display: none;">
                                                <div class="col-sm-4">
                                                    <label class="form-label">
                                                        Reject Reason
                                                    </label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:TextBox ID="txtRejectReason" runat="server" Rows="2" placeholder="Enter Reject reason"
                                                        CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-4">
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:LinkButton ID="btnApprove" runat="server" Text="Approval" OnClientClick="return ValidateApproval();"
                                                        OnClick="btnSave_Click" CssClass="btn btn-cons btn-save AlignTop"></asp:LinkButton>
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:LinkButton ID="btnDisApprove" runat="server" Text="Reject" OnClientClick="return btnValidateReason();"
                                                        OnClick="btnSave_Click" CssClass="btn btn-cons btn-save AlignTop"></asp:LinkButton>
                                                </div>
                                            </div>
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
</asp:Content>
