<%@ Page Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="DesignPOReviewChecklist.aspx.cs" Inherits="Pages_DesignPOReviewChecklist" %>

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
                    $(this).closest('.col-sm-12').find('textarea').before('<span class="reqfield" style="color:red">*</span>');
                    $(this).closest('.col-sm-12').find('textarea').attr('class', 'mandatoryfield');

                }
            });
        }

        function GetRadioButtonListSelectedValue(radioButtonList) {
            debugger;
            try {
                $(radioButtonList).closest('.col-sm-12').find('span.reqfield').remove();
                $(radioButtonList).closest('.col-sm-12').find('textarea').removeClass("mandatoryfield");
                if ($(radioButtonList).find('[type="radio"]:checked').val() == 'N') {
                    $(radioButtonList).closest('.col-sm-12').find('textarea').before('<span class="reqfield" style="color:red">*</span>');
                    $(radioButtonList).closest('.col-sm-12').find('textarea').attr('class', 'mandatoryfield');
                }
            } catch (er) { }
        }
        function showDataTable() {
            $('#<%=gvEnquiryReviewCheckList.ClientID %>').DataTable({ 'aoColumnDefs': [{
                'bSortable': false,
                'aTargets': [-1, -2] /* 1st one, start by the right */
            }]
            });
        }

        function ShowViewPopUp() {
            $('#mpeView').modal({
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


        function ShowViewdocsPopUp() {
            $('#mpeViewdocs').modal({
                backdrop: 'static'
            });
            return false;
        }
                
    </script>
    <style type="text/css">
        textarea
        {
            width: 100%;
            background: transparent;
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
                                    <h3 class="page-title-head d-inline-block">
                                        Design PO Review</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Design</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Design PO Review</li>
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
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divInput" runat="server">
                            <div class="ip-div text-left">
                                <div class="col-sm-12" id="divCusorderNumberCustName" runat="server">
                                </div>
                                <div class="m-t-10" id="divMainform" runat="server">
                                    <div class="col-sm-12">
                                        <div class="col-sm-6  p-t-10" style="color: #163633; font-size: 20px;">
                                            <asp:Label ID="lblCustomerName" Style="font-weight: bold;" Text="" runat="server"></asp:Label>
                                            <span>/ </span>
                                            <asp:Label ID="lblPoNumber" Style="font-weight: bold;" Text="" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="p-t-10 col-sm-12">
                                        <div class="col-sm-3  p-t-10">
                                            <label class="form-label">
                                                PO Date</label>
                                            <asp:Label ID="lblPoDate" Style="font-weight: bold;" Text="" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-3  p-t-10">
                                            <label class="form-label">
                                                Review Status</label>
                                            <asp:Label ID="lblcompletion" Style="font-weight: bold;" Text="Incomplete" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 m-t-10">
                                        <h3 class="sub-title">
                                            PO Specification</h3>
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
                                        <div class="col-sm-12 m-w-b">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    U Stamp Required
                                                </label>
                                            </div>
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <asp:RadioButtonList ID="rblUStamp" runat="server" onclick="GetRadioButtonListSelectedValue(this);"
                                                    CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-6  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtUStampRemarks" runat="server" TextMode="MultiLine" placeHolder="Enter UStamp Remarks"
                                                    ToolTip="Enter UStamp Remarks"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    U2 Stamp Required</label>
                                            </div>
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <asp:RadioButtonList ID="rblU2Stamp" runat="server" onclick="GetRadioButtonListSelectedValue(this);"
                                                    CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-6  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtU2StampRemarks" runat="server" TextMode="MultiLine" placeHolder="Enter U2Stamp Remarks"
                                                    ToolTip="Enter U2Stamp Remarks"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    PED Required (as per 2014/68/EU)
                                                </label>
                                            </div>
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <asp:RadioButtonList ID="rblPED" runat="server" onclick="GetRadioButtonListSelectedValue(this);"
                                                    CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-6  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtPEDremarks" runat="server" TextMode="MultiLine" PlaceHolder="Enter PED Remarks"
                                                    ToolTip="Enter PED Remarks"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    IBR/Non-IBR Required
                                                </label>
                                            </div>
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <asp:RadioButtonList ID="rblIBR" runat="server" onclick="GetRadioButtonListSelectedValue(this);"
                                                    CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-6  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtIBRRemarks" runat="server" TextMode="MultiLine" placeHolder="Enter IBR Remarks"
                                                    ToolTip="Enter IBR Remarks"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    ISO 3834-2 Required
                                                </label>
                                            </div>
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <asp:RadioButtonList ID="rblISO" runat="server" onclick="GetRadioButtonListSelectedValue(this);"
                                                    CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="N" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-6  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtISODetailsRemarks" runat="server" TextMode="MultiLine" placeHolder="Enter End ISO Details"
                                                    ToolTip="Enter End ISO Details"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    Special Information
                                                </label>
                                            </div>
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <asp:RadioButtonList ID="rblsplinfo" runat="server" onclick="GetRadioButtonListSelectedValue(this);"
                                                    CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Applicable" Value="Y"></asp:ListItem>
                                                    <asp:ListItem Text="Not Applicable" Value="N" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-6  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtsplinfo" runat="server" TextMode="MultiLine" placeHolder="Enter Special Remarks"
                                                    ToolTip="Enter Special Remarks"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    Units
                                                </label>
                                            </div>
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <asp:RadioButtonList ID="rblunits" runat="server" onclick="GetRadioButtonListSelectedValue(this);"
                                                    CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="SI Units" Value="SI"></asp:ListItem>
                                                    <asp:ListItem Text="Metric Units" Value="Metric" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-6  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtunits" runat="server" TextMode="MultiLine" placeHolder="Enter Unit Details"
                                                    ToolTip="Enter Unit Details"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <h3 class="sub-title">
                                                Other Information
                                            </h3>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    Checked with Offer</label>
                                            </div>
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <asp:DropDownList ID="ddloffercheck" runat="server" TabIndex="1" ToolTip="Select Offer Checked"
                                                    CssClass="form-control mandatoryfield">
                                                    <asp:ListItem Selected="True" Value="0" Text="-- Select --" />
                                                    <asp:ListItem Value="Ok" Text="Ok" />
                                                    <asp:ListItem Value="Difference Noted" Text="Difference Noted" />
                                                    <asp:ListItem Value="Clarification Required" Text="Clarification Required" />
                                                    <asp:ListItem Value="Others" Text="Others" />
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-6  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtoffercheck" runat="server" TextMode="MultiLine" placeHolder="Enter Offer Check  Remarks"
                                                    ToolTip="Enter Offer Check Remarks" />
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    Technical Feasibility</label>
                                            </div>
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <asp:DropDownList ID="ddltechnicalfeasibile" runat="server" TabIndex="1" ToolTip="Select Technical Feasibility"
                                                    CssClass="form-control mandatoryfield">
                                                    <asp:ListItem Selected="True" Value="0" Text="-- Select --" />
                                                    <asp:ListItem Value="Ok" Text="Ok" />
                                                    <asp:ListItem Value="To be Verified" Text="To be Verified" />
                                                    <asp:ListItem Value="Write to Customer" Text="Write to Customer" />
                                                    <asp:ListItem Value="Others" Text="Others" />
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-6  p-t-10  p-b-10">
                                                <asp:TextBox ID="txttechnicalfeasibile" runat="server" TextMode="MultiLine" placeHolder="Enter technical feasibile  Remarks"
                                                    ToolTip="Enter technical feasibile Remarks" />
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    Information like Drg Design data,QAP etc</label>
                                            </div>
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <asp:DropDownList ID="ddlinfo" runat="server" TabIndex="1" ToolTip="Select Info type"
                                                    CssClass="form-control mandatoryfield">
                                                    <asp:ListItem Selected="True" Value="0" Text="-- Select --" />
                                                    <asp:ListItem Value="Required" Text="Required" />
                                                    <asp:ListItem Value="Not Required" Text="Not Required" />
                                                    <asp:ListItem Value="Write to Customer" Text="Write to Customer" />
                                                    <asp:ListItem Value="Others" Text="Others" />
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-6  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtinfo" runat="server" TextMode="MultiLine" placeHolder="Enter Information Remarks"
                                                    ToolTip="Enter Information Remarks" />
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    Statutory/Regulatory</label>
                                            </div>
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <asp:DropDownList ID="ddlStatutory" runat="server" TabIndex="1" ToolTip="Select Statutory type"
                                                    CssClass="form-control mandatoryfield">
                                                    <asp:ListItem Selected="True" Value="0" Text="-- Select --" />
                                                    <asp:ListItem Value="Applicable" Text="Applicable" />
                                                    <asp:ListItem Value="Not Applicable" Text="Not Applicable" />
                                                    <asp:ListItem Value="Write to Customer" Text="Write to Customer" />
                                                    <asp:ListItem Value="Others" Text="Others" />
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-6  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtStatutory" runat="server" TextMode="MultiLine" placeHolder="Enter Statutory Remarks"
                                                    ToolTip="Enter Statutory Remarks" />
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    Addtional Note</label>
                                            </div>
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                &nbsp;</div>
                                            <div class="col-sm-6  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtAddtionalNote" runat="server" TextMode="MultiLine" placeHolder="Enter Additional Remarks"
                                                    ToolTip="Enter Addtional Remarks" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-50">
                                        <div class="col-sm-6">
                                            <asp:LinkButton ID="btnnodiscrepancy" runat="server" Text="Discrepancy" CssClass="btn btn-cons btn-save AlignTop"
                                                OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divMainform')" OnClick="btnnodiscrepancy_Click"></asp:LinkButton>
                                            <asp:LinkButton ID="btndiscrepancy" runat="server" Text="No Discrepancy" CssClass="btn btn-cons btn-save AlignTop"
                                                OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divMainform')" OnClick="btndiscrepancye_Click"></asp:LinkButton>
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
                                                    <asp:Label ID="lbltitle" runat="server" Text="Design PO Review Check List Details"></asp:Label></p>
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
                                                OnRowCommand="gvEnquiryReviewCheckList_OnRowCommand" DataKeyNames="POHID,EnquiryNumber,PoCopy,PoCopyWithoutPrice">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PO Number" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPonumber" runat="server" Text='<%# Eval("PONumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Customer Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProspectName" runat="server" Text='<%# Eval("ProspectName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Enquiry Number">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCustomerEnquiryNumber_gv" runat="server" Text='<%# Eval("CustomerEnquiryNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delivery Date" ItemStyle-HorizontalAlign="left" HeaderStyle-CssClass="text-left"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDaedlineDate_gv" runat="server" Text='<%# Eval("PODATE")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Review Status" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCompletionStatus" runat="server" Text='<%# Eval("Completionsattus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View PO Copy" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnViewPOCopy" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="viewpocopy"><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View PO Copy Without Price" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnViewPOCopyWithoutPrice" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="viewpocopywithoutprice"><img src="../Assets/images/view.png" /></asp:LinkButton>
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
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <asp:HiddenField ID="hdnClarifyID" runat="server" Value="0" />
                                    </div>
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
                            <h4 class="modal-title ADD">
                                Send Message</h4>
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
                                    Design PO Review Check List Details
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal">
                                x</button>
                        </div>
                        <div class="modal-body">
                            <div class="inner-container">
                                <div class="col-sm-12">
                                    <div class="col-sm-6  p-t-10" style="color: #163633; font-size: 20px;">
                                        <asp:Label ID="lblCustomerName_V" Style="font-weight: bold;" Text="" runat="server" /><span>/
                                        </span>
                                        <asp:Label ID="lblponumber_V" Style="font-weight: bold;" Text="" runat="server" /></div>
                                </div>
                                <div class="p-t-10 col-sm-12">
                                    <div class="col-sm-4  p-t-10">
                                        <label class="form-label">
                                            Po Date</label><asp:Label ID="lblPoDate_V" Style="font-weight: bold;" Text="" runat="server" /></div>
                                    <div class="col-sm-4  p-t-10">
                                        <label class="form-label">
                                            Completion Status</label><asp:Label ID="lblcompletion_V" Style="font-weight: bold;"
                                                Text="" runat="server" /></div>
                                </div>
                                <div class="c-elements">
                                    <div class="col-sm-12" style="background: #50a698; padding: 5px 0px;">
                                        <div class="col-sm-3 p-t-10" style="border-right: 0; min-height: 48px">
                                            <label class="form-label">
                                                Specifications</label></div>
                                        <div class="col-sm-3 p-t-10" style="border-right: 0; min-height: 48px">
                                            <label class="form-label">
                                                Status</label></div>
                                        <div class="col-sm-6 p-t-10">
                                            <label class="form-label">
                                                Remarks</label></div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                U Stamp Required</label></div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblUStamp_V" runat="server" /></div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblUStampRemarks_V" runat="server" /></div>
                                    </div>
                                    <div class="col-sm-12 m-w-b">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                U2 Stamp Required</label></div>
                                        <div class="col-sm-3">
                                            <asp:Label ID="lblU2Stamp_V" runat="server" /></div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblU2StampRemarks_V" runat="server" /></div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                PED Required (as per 2014/68/EU)</label></div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblPED_V" runat="server" /></div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblPEDRemarks_V" runat="server" /></div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                IBR/Non-IBR Required</label></div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblIBR_V" runat="server" /></div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblIBRRemarks_V" runat="server" /></div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                ISO 3834-2 Required</label></div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblISO_V" runat="server" /></div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblISORemarks_V" runat="server" /></div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Special Information</label></div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblsplinfo_V" runat="server" /></div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblsplinfoRemarks_V" runat="server" /></div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Units</label></div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblUnits_V" runat="server" /></div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblUnitsRemarks_V" runat="server" /></div>
                                    </div>
                                    <div class="col-sm-12">
                                        <h3 class="sub-title">
                                            Other Information</h3>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Checked with Offer</label></div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lbloffercheck_V" runat="server" /></div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lbloffercheckRemarks_V" runat="server" /></div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Technical Feasibility</label></div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lbltechnicalfeasibile_V" runat="server" /></div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lbltechnicalfeasibileRemarks_V" runat="server" /></div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Information like Drg Design data,QAP etc</label></div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblinfo_V" runat="server" /></div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblinfoRemarks_V" runat="server" /></div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Statutory/Regulatory</label></div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblStatutory_V" runat="server" /></div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblStatutoryRemarks_V" runat="server" /></div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Addtional Note</label></div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            &nbsp;</div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblAddtionalNote_v" runat="server" /></div>
                                    </div>
                                    <div class="col-sm-12 p-t-10 p-b-10">
                                        <div class="col-sm-6">
                                            <asp:LinkButton ID="lbtnSendCommunication" runat="server" Text="SendCommunication"
                                                OnClientClick="return ShowSendCommunicationPopUp();" CssClass="btn btn-con btn-send" /></div>
                                        <div class="col-sm-6">
                                            <asp:LinkButton ID="lbtnViewCommunication" runat="server" Text="ViewCommunication"
                                                OnClick="btnViewCommunication_Click" CssClass="btn btn-con btn-view" /></div>
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
                            <h4 class="modal-title ADD">
                                View Communication</h4>
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
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H3">
                                <div class="text-center">
                                    Documents
                                    <asp:Label ID="Label3" Text="" runat="server"></asp:Label>
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal">
                                x</button>
                        </div>
                        <div class="modal-body" style="height: 100%;">
                            <div class="inner-container" style="height: 100%;">
                                <div class="ip-div text-center" style="margin-bottom: 10px;">
                                    <%--    <asp:LinkButton Text="Download" CssClass="btn btn-save btn-cons AlignTop" ID="btndownloaddocs"
                                        OnClick="btndownloaddocs_Click" runat="server" />--%>
                                </div>
                                <div class="col-sm-12" style="height: 100%;">
                                    <iframe runat="server" id="ifrm" src="" style="width: 100%; height: 80%;" frameborder="0">
                                    </iframe>
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
