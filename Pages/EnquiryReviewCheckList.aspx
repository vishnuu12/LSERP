<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="EnquiryReviewCheckList.aspx.cs" Inherits="Pages_EnquiryReviewCheckList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="../Assets/css/datatables2.css" />
    <script type="text/javascript" src="../Assets/scripts/datatables2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            debugger
        });
        function fieldreqadd() {
            $('[type="radio"]:checked').each(function () {
                $(this).closest('.col-sm-12').find('span.reqfield').remove();
                $(this).closest('.col-sm-12').find('textarea').removeClass("mandatoryfield");
                if ($(this).val() == 'N') {
                    //  $(this).closest('.col-sm-12').find('textarea').before('<span class="reqfield" style="color:red">*</span>');
                    //  $(this).closest('.col-sm-12').find('textarea').attr('class', 'mandatoryfield');
                }
            });
        }

        function GetRadioButtonListSelectedValue(radioButtonList) {
            debugger;
            try {
                $(radioButtonList).closest('.col-sm-12').find('span.reqfield').remove();
                $(radioButtonList).closest('.col-sm-12').find('textarea').removeClass("mandatoryfield");
                //if ($(radioButtonList).find('[type="radio"]:checked').val() == 'N') {
                //    //  $(radioButtonList).closest('.col-sm-12').find('textarea').before('<span class="reqfield" style="color:red">*</span>');
                //    // $(radioButtonList).closest('.col-sm-12').find('textarea').attr('class', 'mandatoryfield');
                //}
                if ($('.c-elements').find('[value="Y"]:checked').length > 0)
                    $('#ContentPlaceHolder1_lblCompletionStatus').text('Completed');
                else
                    $('#ContentPlaceHolder1_lblCompletionStatus').text('Incomplete');

            } catch (er) { }
        }

        function showDataTable() {
            $('#<%=gvEnquiryReviewCheckList.ClientID %>').DataTable({
                'aoColumnDefs': [{
                    'bSortable': false,
                    'aTargets': [-1, -2] /* 1st one, start by the right */
                }]
            });
        }

        function ShowViewPopUp() {
            $('#mpeViewdocs').modal({
                backdrop: 'static'

            });
            return false;
        }

        function ShowEnquiryCheckListViewPopUp() {
            $('#mpeViewEnquiryCheckListDetails').modal({
                backdrop: 'static'
            });
            return false;
        }

        function ShowCloseViewPopUp() {

            $("#mpeView").modal("hide");
            return false;
        }

        function CloseCheckListViewPopUp() {

            $("#mpeViewEnquiryCheckListDetails").modal("hide");
            return false;
        }

        function ShowSendCommunicationPopUp() {

            $('#mpeAddMessage').modal({
                backdrop: 'static'
            });
            CloseCheckListViewPopUp();
            return false;
        }

        function ShowViewCommunicationPopUp() {
            $('#mpeViewCommunication').modal({
                backdrop: 'static'
            });
            CloseCheckListViewPopUp();
        }

        function CloseViewCommunicationPopUp() {
            $("#mpeViewCommunication").modal("hide");
            return false;
        }

        function ShowReplyPopUp(element) {
            $('#mpeReplyMessage').modal({
                backdrop: 'static'
            });
            var ECID = element.id.split('_');
            $('#<%=hdnECID.ClientID %>').attr('value', ECID[2]);
            CloseViewCommunicationPopUp();
            return false;
        }

        function CloseReplyPopUp() {
            $("#mpeReplyMessage").modal("hide");
            ShowViewCommunicationPopUp();
            return false;
        }

        function ValidateSendMail() {
            var msg = 0;
            if ($('#<%=ddlReceiverGroup.ClientID %>')[0].selectedIndex == "0") {
                $('#<%=ddlReceiverGroup.ClientID %>').notify('Field Required.', { arrowShow: true, position: 't,r', autoHide: true });
                msg = 1;
            }
            else if ($('#<%=txtAddHeader.ClientID %>').value == "") {

                $('#<%=txtAddHeader.ClientID %>').notify('Field Required.', { arrowShow: true, position: 't,r', autoHide: true });
                msg = 1;
            }
            else if ($('#<%=txtMessage.ClientID %>').value == "") {

                $('#<%=txtMessage.ClientID %>').notify('Field Required.', { arrowShow: true, position: 't,r', autoHide: true });
                msg = 1;
            }
            if (msg == 0) {
                showLoader();
                return true;
            }
            else {
                return false;
            }
        }

        function ValidateReplyControl(controlID) {
            var message = controlID.split('/');
            $('#' + message[0]).notify(message[1], { arrowShow: true, position: 'r,r', autoHide: true, autoHideDelay: 5000 });
        }

        function rblChanged(ele) {
            if ($(ele).find('[type="radio"]:checked').val() == 'N') {
                $('[value="Y"]').prop('checked', false);
                $('[value="N"]').prop('checked', true);

            }
            else {

                $('[value="Y"]').prop('checked', true);
                $('[value="N"]').prop('checked', false);

            }
            fieldreqadd();
        }

        function PrintEnquiryReviewCheckList() {

            var div = $('#ContentPlaceHolder1_divCheckListPrint').html();

            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');
            winprint.document.write("<html><head><title>");
            winprint.document.write("</title>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='../Assets/css/ep-style.css'/>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='../Assets/css/main.css' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/print.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("</head><body>");
            winprint.document.write("<div style='text-align:center;font-size:20px;color:#00BCD4;'>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-12' style='font-size:20px;font-weight:bold;'>");
            winprint.document.write("<div>");
            winprint.document.write("<div class='col-sm-12' style='padding-top:10px;'>");
            winprint.document.write(div);
            winprint.document.write("<div>");
            winprint.document.write("</body></html>");

            setTimeout(function () {
                winprint.document.close();
                winprint.focus();
                winprint.print();
                // winprint.close();
            }, 1000);
        }

    </script>
    <style type="text/css">
        textarea {
            width: 100%;
            background: transparent;
        }

        .radio-success label:nth-child(2) {
            color: red !important;
        }

        .radio-success label:nth-child(4) {
            color: #0acc0a !important;
        }
    </style>
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
                                    <h3 class="page-title-head d-inline-block">Enquiry Review Check List</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Design</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Enquiry Review CheckList</li>
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
                    <asp:PostBackTrigger ControlID="imgExcel" />
                    <asp:PostBackTrigger ControlID="imgPdf" />
                    <asp:PostBackTrigger ControlID="btnSave" />
                    <asp:PostBackTrigger ControlID="gvEnquiryReviewCheckList" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divradio" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label" style="padding-top: 7px;">
                                            Select Type
                                        </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:RadioButtonList ID="rblEnquiryChange" runat="server" CssClass="radio radio-success" RepeatDirection="Horizontal"
                                            AutoPostBack="true" OnSelectedIndexChanged="rblEnquiryChange_OnSelectedChanged" RepeatLayout="Flow">
                                            <asp:ListItem Selected="True" Value="0">PENDING</asp:ListItem>
                                            <asp:ListItem Value="1">COMPLETED</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-left">
                                <div class="col-sm-12" id="divCusorderNumberCustName" runat="server">
                                </div>
                                <div class="m-t-10" id="divMainform" runat="server">
                                    <div class="col-sm-12">
                                        <div class="col-sm-6  p-t-10" style="color: #163633; font-size: 20px;">
                                            <asp:Label ID="lblCustomerName" Style="font-weight: bold;" Text="" runat="server"></asp:Label>
                                            <span>/ </span>
                                            <asp:Label ID="lblEnquiryNumber" Style="font-weight: bold;" Text="" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="p-t-10 col-sm-12">
                                        <div class="col-sm-3  p-t-10">
                                            <label class="form-label">
                                                CustomerEnquiry Number</label>
                                            <asp:Label ID="lblCustomerEnquiryNumber" Style="font-weight: bold;" Text="" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-3  p-t-10">
                                            <label class="form-label">
                                                Received Date</label>
                                            <asp:Label ID="lblReceivedDate_D" Style="font-weight: bold;" Text="" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-3  p-t-10">
                                            <label class="form-label">
                                                DeadLine Date</label>
                                            <asp:Label ID="lblDeadLineDate" Style="font-weight: bold;" Text="" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-3  p-t-10">
                                            <label class="form-label">
                                                Completion Status</label>
                                            <asp:Label ID="lblCompletionStatus" Style="font-weight: bold;" Text="Incomplete"
                                                runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 m-t-10">
                                        <h3 class="sub-title">Technical Specification</h3>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3">
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:RadioButtonList ID="rblYesNoAll" runat="server" OnChange="rblChanged(this);"
                                                CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="Yes All" Value="Y"></asp:ListItem>
                                                <asp:ListItem Text="No All" Value="N"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="col-sm-3">
                                        </div>
                                        <div class="col-sm-3">
                                        </div>
                                    </div>
                                    <div class="c-elements">
                                        <div class="col-sm-12" style="background: #50a698; padding: 5px 0px;">
                                            <div class="col-sm-3 p-t-10" style="border-right: 0; min-height: 48px">
                                                <label class="form-label">
                                                    Specifications</label>
                                            </div>
                                            <div class="col-sm-3 p-t-10" style="border-right: 0; min-height: 48px">
                                                <label class="form-label mandatorylbl">
                                                    Status</label>
                                            </div>
                                            <div class="col-sm-6 p-t-10">
                                                <label class="form-label">
                                                    Remarks</label>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    Size Of Expansion Joints</label>
                                            </div>
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <asp:RadioButtonList ID="rblSizeOfExpansionJoints" runat="server" onclick="GetRadioButtonListSelectedValue(this);"
                                                    CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-6  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtSizeOfExpansionjoints" runat="server" TextMode="MultiLine" placeHolder="Enter Size Of Expansion Joints"
                                                    ToolTip="Enter Size Of Expansion Joints"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    Material</label>
                                            </div>
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <asp:RadioButtonList ID="rblMaterial" runat="server" onclick="GetRadioButtonListSelectedValue(this);"
                                                    CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-6  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtMaterialRemarks" runat="server" TextMode="MultiLine" placeHolder="Enter Material Remarks"
                                                    ToolTip="Enter Material Remarks"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 m-w-b">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    Pressure
                                                </label>
                                            </div>
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <asp:RadioButtonList ID="rblPressure" runat="server" onclick="GetRadioButtonListSelectedValue(this);"
                                                    CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-6  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtPressureRemarks" runat="server" TextMode="MultiLine" placeHolder="Enter Pressure Remarks"
                                                    ToolTip="Enter Pressure Remarks"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    Movements
                                                </label>
                                            </div>
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <asp:RadioButtonList ID="rblMovements" runat="server" onclick="GetRadioButtonListSelectedValue(this);"
                                                    CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-6  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtMovementsremarks" runat="server" TextMode="MultiLine" PlaceHolder="Enter Movements Remarks"
                                                    ToolTip="Enter Movements Remarks"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    Temperature
                                                </label>
                                            </div>
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <asp:RadioButtonList ID="rblTemparature" runat="server" onclick="GetRadioButtonListSelectedValue(this);"
                                                    CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-6  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtTemparatureRemarks" runat="server" TextMode="MultiLine" placeHolder="Enter Temparature Remarks"
                                                    ToolTip="Enter Temparature Remarks"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    End Connection Details
                                                </label>
                                            </div>
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <asp:RadioButtonList ID="rblConnection" runat="server" onclick="GetRadioButtonListSelectedValue(this);"
                                                    CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-6  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtConnectionDetailsRemarks" runat="server" TextMode="MultiLine"
                                                    placeHolder="Enter End Connection Details" ToolTip="Enter End Connection Details"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    OverAll Length
                                                </label>
                                            </div>
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <asp:RadioButtonList ID="rblLength" runat="server" onclick="GetRadioButtonListSelectedValue(this);"
                                                    CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-6  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtLengthRemarks" runat="server" TextMode="MultiLine" placeHolder="Enter Over All Length"
                                                    ToolTip="Enter Over All Length"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    Flow Medium/Flow Velocity/Flow Rate
                                                </label>
                                            </div>
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <asp:RadioButtonList ID="rblFlowMedium" runat="server" onclick="GetRadioButtonListSelectedValue(this);"
                                                    CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-6  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtFlowMediumRemarks" runat="server" TextMode="MultiLine" placeHolder="Enter Flow Medium"
                                                    ToolTip="Enter Flow Medium"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    Type Of Expansion Joints</label>
                                            </div>
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <asp:RadioButtonList ID="rblTypeOfExpansionJoints" runat="server" onclick="GetRadioButtonListSelectedValue(this);"
                                                    CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-6  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtTypeOfExpansionjoints" runat="server" TextMode="MultiLine" placeHolder="Enter Type Of Expansion Joints"
                                                    ToolTip="Enter Size Of Expansion Joints"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    Application / Installation
                                                </label>
                                            </div>
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <asp:RadioButtonList ID="rblApplication" runat="server" onclick="GetRadioButtonListSelectedValue(this);"
                                                    CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-6  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtApplicationRemarks" runat="server" TextMode="MultiLine" placeHolder="Enter Application Remarks"
                                                    ToolTip="Entre Application Remarks"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    Piping Layout / Relevant Drawings Availability
                                                </label>
                                            </div>
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <asp:RadioButtonList ID="rblPipingLayout" runat="server" onclick="GetRadioButtonListSelectedValue(this);"
                                                    CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-6  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtPipingLayout" runat="server" TextMode="MultiLine" placeHolder="Enter Piping Layout Remarks"
                                                    ToolTip="Enter Piping Layout Remarks"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <h3 class="sub-title">Other Information
                                            </h3>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    Painting Requirtments
                                                </label>
                                            </div>
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <asp:RadioButtonList ID="rblPaintingRequirtments" runat="server" onclick="GetRadioButtonListSelectedValue(this);"
                                                    CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-6  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtPaintingRemarks" runat="server" TextMode="MultiLine" placeHolder="Enter Painting  Remarks"
                                                    ToolTip="Enter Painting Remarks"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    Statutory Details Specified
                                                </label>
                                            </div>
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <asp:RadioButtonList ID="rblStatutory" runat="server" onclick="GetRadioButtonListSelectedValue(this);"
                                                    CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-6  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtStatutoryDetails" runat="server" TextMode="MultiLine" placeHolder="Enter Statutory  Remarks"
                                                    ToolTip="Enter Statutory Remarks"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    Inspection And Testing
                                                </label>
                                            </div>
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <asp:RadioButtonList ID="rblInspectionAndTesting" runat="server" onclick="GetRadioButtonListSelectedValue(this);"
                                                    CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-6  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtInspectionAndTestingRemarks" runat="server" TextMode="MultiLine"
                                                    placeHolder="Enter Inspection And Testing  Remarks" ToolTip="Enter Inspection And Testing  Remarks"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    Clarrification required Fro the Following Details
                                                </label>
                                            </div>
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <asp:RadioButtonList ID="rblClarrification" runat="server" onclick="GetRadioButtonListSelectedValue(this);"
                                                    CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-6  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtClarrificationRemarks" runat="server" TextMode="MultiLine" placeHolder="Enter Clarrification Remarks"
                                                    ToolTip="Enter Clarrification Remarks"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    Stamping Requirements(U/U2/PED/IBR etc.,)
                                                </label>
                                            </div>
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <asp:RadioButtonList ID="rblStampingRequirtments" runat="server" onclick="GetRadioButtonListSelectedValue(this);"
                                                    CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-6  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtStampingRemarks" runat="server" TextMode="MultiLine" placeHolder="Enter Stamping Remarks"
                                                    ToolTip="Enter Stamping Remarks"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    Addtional Note</label>
                                            </div>
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                &nbsp;
                                            </div>
                                            <div class="col-sm-6  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtAddtionalNote" runat="server" TextMode="MultiLine" placeHolder="Enter Additional Remarks"
                                                    ToolTip="Enter Addtional Remarks"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-50">
                                        <div class="col-sm-2">
                                            <div class="text-left">
                                                <label class="form-label mandatorylbl">
                                                    Offer Type</label>
                                            </div>
                                        </div>
                                        <div class=" col-sm-10 text-left">
                                            <asp:DropDownList ID="ddlOfferType" CssClass="form-control" runat="server" ToolTip="Offer Type">
                                                <asp:ListItem Selected="True" Value="0">--Select--</asp:ListItem>
                                            </asp:DropDownList>
                                            <%--    <asp:RadioButtonList ID="rblBudgetary" runat="server" CssClass="radio radio-success"
                                                RepeatDirection="Horizontal" RepeatLayout="Flow" TabIndex="11" Style="margin-left: -42px;
                                                margin-top: 4px;">
                                                <asp:ListItem Selected="True" Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                            </asp:RadioButtonList>--%>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-50" runat="server" id="divAttach" visible="false">
                                        <div class="col-sm-2">
                                            <div class="text-left">
                                                <label class="form-label mandatorylbl">
                                                    Attachements</label>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 text-left">
                                            <asp:FileUpload ID="fAttachment" runat="server" onchange="DocValidation(this);" TabIndex="12"
                                                CssClass="form-control mandatoryfield" Width="95%"></asp:FileUpload>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-50">
                                        <div class="col-sm-6">
                                            <asp:LinkButton ID="btnSave" runat="server" Text="Save" CssClass="btn btn-cons btn-save AlignTop"
                                                OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divMainform')" OnClick="btnSave_Click"></asp:LinkButton>
                                            <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-cons btn-danger AlignTop"
                                                OnClick="btnCancel_Click"></asp:LinkButton>
                                        </div>
                                        <div class=" col-sm-6 text-right">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="col-md-12 row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                                    <asp:Label ID="lbltitle" runat="server" Text="Enquiry Review Check List Details"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                                <asp:LinkButton ID="btnprint" runat="server" CssClass="print_bg" OnClientClick="return fnvalidate();"
                                                    ToolTip="print" />
                                                <asp:LinkButton ID="imgExcel" runat="server" CssClass="excel_bg" ToolTip="Excel Download" />
                                                <%--OnClick="btnExcelDownload_Click"--%>
                                                <asp:LinkButton ID="imgPdf" runat="server" CssClass="pdf_bg" ToolTip="PDF Download" />
                                                <%-- OnClick="btnPDFDownload_Click" --%>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll">
                                            <asp:GridView ID="gvEnquiryReviewCheckList" runat="server" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" ShowHeader="true" EmptyDataText="No Records Found"
                                                CssClass="table table-hover table-bordered medium" OnRowDataBound="gvEnquiryReviewCheckList_OnRowDataBound"
                                                OnRowCommand="gvEnquiryReviewCheckList_OnRowCommand" DataKeyNames="ClarifyID,FileName">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Customer Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProspectName" runat="server" Text='<%# Eval("ProspectName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Customer Enquiry Number">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCustomerEnquiryNumber_gv" runat="server" Text='<%# Eval("CustomerEnquiryNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Designer Dead line Date" ItemStyle-HorizontalAlign="left"
                                                        HeaderStyle-CssClass="text-left" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDaedlineDate_gv" runat="server" Text='<%# Eval("DesignerDaedlineDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Attachement" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnAttachement" runat="server" CommandName="ViewAttachement"
                                                                CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                           <img src="../Assets/images/view.png" alt="" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCompletionStatus" runat="server" Text='<%# Eval("CompletionStatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnView" runat="server" CommandName="ViewEnquiryReviewCheckList"
                                                                CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                           <img src="../Assets/images/view.png" alt="" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="EditEnquiryReviewCheckList"
                                                                CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                           <img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PDF" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnPDF" runat="server" CommandName="CheckListPDF" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                               <img src="../Assets/images/pdf.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <asp:HiddenField ID="hdnClarifyID" runat="server" Value="0" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12" id="divCheckListPrint" runat="server" style="display: block;">
                            <div style="padding: 10px; width: 200mm; height: 287mm; border: 1px solid #000; margin: 10px 20px">
                                <div class="col-sm-12 text-center">
                                    <label style="font-weight: 600; font-size: 24px ! important; font-style: italic; color: #000; font-family: Arial; display: contents;">
                                        LONESTAR
                                    </label>
                                    <label style="font-weight: 600; font-size: 24px ! important; font-family: Times New Roman;">
                                        INDUSTRIES</label>
                                </div>
                                <div class="col-sm-12">
                                    <label class="form-label text-center" style="font-size: 18px !important; font-weight: 900; margin-bottom: 1em; text-decoration: underline">
                                        Enquiry Review Check List</label>
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-8">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label">
                                                Name Of The Customer:</label>
                                        </div>
                                        <div class="col-sm-8 text-left">
                                            <asp:Label ID="lblNameOftheCustomer_P" Text="" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="col-sm-6 text-right">
                                            <label class="form-label">
                                                DeadLine Date :</label>
                                        </div>
                                        <div class="col-sm-6 text-left">
                                            <asp:Label ID="lblCustDeadLineDate_P" Text="" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-left">
                                        <label class="form-label">
                                            Enquiry Number And Date</label>
                                    </div>
                                    <div class="col-sm-2 text-center">
                                        :
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:Label ID="lblEnquiryNumberAndDate_P" Text="" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-4">
                                        <label class="form-label">
                                            Size Of Expansion Joints</label>
                                    </div>
                                    <div class="col-sm-2 text-center">
                                        :
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblSizeOfEJ_P" CssClass="text-left" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-4">
                                        <label class="form-label">
                                            Material</label>
                                    </div>
                                    <div class="col-sm-2 text-center">
                                        :
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblMaterial_P" CssClass="text-left" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-4  p-t-10  p-b-10">
                                        <label class="form-label">
                                            Pressure
                                        </label>
                                    </div>
                                    <div class="col-sm-2 text-center">
                                        :
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblPressure_P" CssClass="text-left" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-4">
                                        <label class="form-label">
                                            Movements
                                        </label>
                                    </div>
                                    <div class="col-sm-2 text-center">
                                        :
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblMovements_P" CssClass="text-left" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-4">
                                        <label class="form-label">
                                            Temperature
                                        </label>
                                    </div>
                                    <div class="col-sm-2 text-center">
                                        :
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblTemparatue_P" CssClass="text-left" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-4">
                                        <label class="form-label">
                                            OverAll Length
                                        </label>
                                    </div>
                                    <div class="col-sm-2 text-center">
                                        :
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblOverAllLength_P" CssClass="text-left" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-4">
                                        <label class="form-label">
                                            End Fittings Details
                                        </label>
                                    </div>
                                    <div class="col-sm-2 text-center">
                                        :
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblEndConnectionDetails_P" CssClass="text-left" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-4">
                                        <label class="form-label">
                                            Flow Medium /Flow Rate
                                        </label>
                                    </div>
                                    <div class="col-sm-2 text-center">
                                        :
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblFlowMedium_P" CssClass="text-left" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-4">
                                        <label class="form-label">
                                            Type Of Expansion/Joints
                                        </label>
                                    </div>
                                    <div class="col-sm-2 text-center">
                                        :
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblTypeofEJ_P" CssClass="text-left" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-4">
                                        <label class="form-label">
                                            Piping Layout / Relevant Drawings Availability
                                        </label>
                                    </div>
                                    <div class="col-sm-2 text-center">
                                        :
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblPipingLayout_P" CssClass="text-left" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-4">
                                        <label class="form-label">
                                            Statutory Details Specified
                                        </label>
                                    </div>
                                    <div class="col-sm-2 text-center">
                                        :
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblStatutaryDetailsSpecified_P" CssClass="text-left" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-4">
                                        <label class="form-label">
                                            Painting Requirtments
                                        </label>
                                    </div>
                                    <div class="col-sm-2 text-center">
                                        :
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblPaintingRequirtments_P" CssClass="text-left" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-4">
                                        <label class="form-label">
                                            Stamping Requirements(U/U2/PED/IBR etc.,)
                                        </label>
                                    </div>
                                    <div class="col-sm-2 text-center">
                                        :
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblStampingRequirtments_P" CssClass="text-left" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-4">
                                        <label class="form-label">
                                            Inspection And Testing Requirtments
                                        </label>
                                    </div>
                                    <div class="col-sm-2 text-center">
                                        :
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblInspectionAndTesting_P" CssClass="text-left" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-4">
                                        <label class="form-label">
                                            Addtional Note</label>
                                    </div>
                                    <div class="col-sm-2 text-center">
                                        :
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblAddtionalNote_P" CssClass="text-left" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-4">
                                        <label class="form-label">
                                            Clarrification required Fro the Following Details
                                        </label>
                                    </div>
                                    <div class="col-sm-2 text-center">
                                        :
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblClarrificationRequired_P" CssClass="text-left" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12" style="margin-top: 70px">
                                    <div class="col-sm-6">
                                        <label class="form-label text-center">
                                            Marketing by:
                                        </label>
                                        <asp:Label ID="lblMarketingBy_P" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-6">
                                        <label class="form-label text-center">
                                            Designer By:</label>
                                        <asp:Label ID="lblDesignerBy_P" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12" style="border: 1px solid #000; padding: 5px; margin-top: 70px">
                                    <div class="col-sm-4">
                                        <label class="form-label text-center" style="display: contents;">
                                            Doc.No</label>
                                        <asp:Label ID="lblDocNo_P" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-4">
                                        <label class="form-label text-center" style="display: contents;">
                                            Rev.No</label>
                                        <asp:Label ID="lblRevNo_P" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-4">
                                        <label class="form-label text-center" style="display: contents;">
                                            Rev.Date</label>
                                        <asp:Label ID="lblRevDate_P" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12 text-center p-t-10">
                                    <asp:Image ID="imgQrcode" class="Qrcode" ImageUrl="" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal" id="mpeAddMessage">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="Add" runat="server" UpdateMode="Always">
                    <Triggers>
                        <%-- <asp:PostBackTrigger ControlID="" />--%>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">Send Message</h4>
                            <button id="btnClosePopup" type="button" onclick="ShowEnquiryCheckListViewPopUp();"
                                class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div class="inner-container">
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4">
                                        <label class="form-label">
                                            Select ReceiverGroup</label>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlReceiverGroup" CssClass="form-control" runat="server" ToolTip="ReceiverGroup">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4">
                                        <label class="form-label">
                                            Add Header</label>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtAddHeader" runat="server" Width="200%" TextMode="MultiLine" Rows="1"
                                            placeHolder="Enter Header Name" ToolTip="Enter Header Name"></asp:TextBox>
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
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4">
                                        <label class="form-label">
                                            Reply Required</label>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:RadioButtonList ID="rblReplyStatus" runat="server" onclick="GetRadioButtonListSelectedValue(this);"
                                            CssClass="radio radio-success" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Yes" Value="Y" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="No" Value="N"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="col-sm-12 p-t-10">
                                <div class="col-sm-3">
                                </div>
                                <div class="col-sm-3">
                                    <div class="form-group">
                                        <asp:LinkButton ID="btnSendMail" Text="SendMail" OnClientClick="return ValidateSendMail();"
                                            OnClick="btnSendMail_Click" CssClass="btn btn-cons btn-save  AlignTop" runat="server" />
                                    </div>
                                </div>
                                <div class="col-sm-3">
                                </div>
                                <div class="col-sm-3">
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal" id="mpeViewEnquiryCheckListDetails" style="overflow-y: auto;">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="upEnquiryReviewchecklist" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H2">
                                <div class="text-center">
                                    Enquiry Review Check List Details
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal">
                                x</button>
                        </div>
                        <div class="modal-body">
                            <div class="inner-container">
                                <div class="col-sm-12">
                                    <div class="col-sm-6  p-t-10" style="color: #163633; font-size: 20px;">
                                        <asp:Label ID="lblCustomerName_V" Style="font-weight: bold;" Text="" runat="server"></asp:Label>
                                        <span>/ </span>
                                        <asp:Label ID="lblEnquiryNumber_V" Style="font-weight: bold;" Text="" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="p-t-10 col-sm-12">
                                    <div class="col-sm-4  p-t-10">
                                        <label class="form-label">
                                            CustomerEnquiry Number</label>
                                        <asp:Label ID="lblCustomerEnquiryNumber_V" Style="font-weight: bold;" Text="" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-4  p-t-10">
                                        <label class="form-label">
                                            Received Date</label>
                                        <asp:Label ID="lblReceivedDate_V" Style="font-weight: bold;" Text="" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-4  p-t-10">
                                        <label class="form-label">
                                            DeadLine Date</label>
                                        <asp:Label ID="lblDeadlineDate_V" Style="font-weight: bold;" Text="" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="c-elements">
                                    <div class="col-sm-12" style="background: #50a698; padding: 5px 0px;">
                                        <div class="col-sm-3 p-t-10" style="border-right: 0; min-height: 48px">
                                            <label class="form-label">
                                                Specifications</label>
                                        </div>
                                        <div class="col-sm-3 p-t-10" style="border-right: 0; min-height: 48px">
                                            <label class="form-label">
                                                Status</label>
                                        </div>
                                        <div class="col-sm-6 p-t-10">
                                            <label class="form-label">
                                                Remarks</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Size Of Expansion joints</label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblSizeOfEJ_V" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblSizeOfEJRemarks_V" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Material</label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblMaterial_V" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblMaterialRemarks_V" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 m-w-b">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Pressure
                                            </label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:Label ID="lblPressure_V" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblPressureRemarks_V" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Movements
                                            </label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblMovements_V" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblMovementsRemarks_V" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Temperature
                                            </label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblTemprature_V" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblTempratureRemarks_V" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Type Of Expansion joints</label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblTypeOfEJ_V" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblTypeOfEJRemarks_V" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                End Connection Details
                                            </label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblConnection_V" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblConnectionRemarks_V" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                OverAll Length
                                            </label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblLength_V" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblLengthRemarks_V" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Flow Medium
                                            </label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblFlowMedium_V" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblFlowMediumRemarks_V" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Application / Installation
                                            </label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblApplication_V" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblApplicationRemarks_V" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Piping Layout / Relevant Drawings Availability
                                            </label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblPipingLayout_V" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblPipingLayoutRemarks_V" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <h3 class="sub-title">Other Information
                                        </h3>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Painting Requirtments
                                            </label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblPainting_V" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblPaintingRemarks_V" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Statutory Details Specified
                                            </label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblStatutory_V" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblStatutoryRemarks_V" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Inspection And Testing
                                            </label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblInspectionAndTesting_V" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblInspectionAndTestingRemarks_V" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Clarrification required Fro the Following Details
                                            </label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblClarrification_V" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblClarrificationRemarks_V" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Stamping Requirements(U/U2/PED/IBR etc.,)
                                            </label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblStamping_V" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblStampingRemarks_V" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Addtional Note</label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            &nbsp;
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblAddtionalNote_v" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <%--    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                With Offer Price</label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            &nbsp;
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblAddtionalBudgetary_v" runat="server"></asp:Label>
                                        </div>
                                    </div>--%>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Offer Type</label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            &nbsp;
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblOfferType_V" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10 p-b-10">
                                        <div class="col-sm-6">
                                            <asp:LinkButton ID="lbtnSendCommunication" runat="server" Text="SendCommunication"
                                                OnClientClick="return ShowSendCommunicationPopUp();" CssClass="btn btn-con btn-send">
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:LinkButton ID="lbtnViewCommunication" runat="server" Text="ViewCommunication"
                                                OnClick="btnViewCommunication_Click" CssClass="btn btn-con btn-view">
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <%--   <button type="button" class="btn btn-default" data-dismiss="modal">
                                Close</button>--%>
                        </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal" id="mpeViewCommunication" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">View Communication</h4>
                            <button id="Button1" type="button" onclick="ShowEnquiryCheckListViewPopUp();" class="close btn-primary-purple"
                                data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body">
                            <div class="inner-container">
                                <div id="DivReceiver" runat="server" class="col-sm-12">
                                </div>
                                <div id="DivSender" runat="server" class="col-sm-12">
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="col-sm-12 text-center">
                            </div>
                        </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:HiddenField ID="hdnECID" runat="server" Value="" />
            </div>
        </div>
    </div>
    <div class="modal" id="mpeReplyMessage">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="Updateview" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H1">
                                <div class="text-center">
                                    Send Reply
                                    <asp:Label ID="Label1" Text="" runat="server"></asp:Label>
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" onclick="CloseReplyPopUp();"
                                data-dismiss="modal">
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
                                        <asp:TextBox ID="txtReplyMessage" runat="server" TextMode="MultiLine" Rows="3" Width="200%"
                                            placeHolder="Enter Message" ToolTip="Enter Message"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="col-sm-12 text-center">
                                <asp:LinkButton ID="LinkButton1" Text="Send Mail" OnClientClick="return ValidateReplyMessage();"
                                    OnClick="btnReplyMessage_Click" CssClass="btn btn-cons btn-save  AlignTop" runat="server" />
                                <%--  <asp:LinkButton ID="lbtnCancel" runat="server" Text="Cancel" CssClass="btn btn-cons btn-danger AlignTop"
                                    OnClientClick="return ClearFields();"></asp:LinkButton>--%>
                            </div>
                        </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:HiddenField ID="HiddenField1" runat="server" Value="" />
            </div>
        </div>
    </div>
    <div class="modal" id="mpeViewdocs" style="height: 125%; top: -10%;">
        <div class="modal-dialog" style="height: 100%;">
            <div class="modal-content" style="height: 100%;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always" Style="height: 100%;">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btndownloaddocs" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H3">
                                <div class="text-center">
                                    Documents
                                    <asp:Label ID="Label2" Text="" runat="server"></asp:Label>
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
</asp:Content>
