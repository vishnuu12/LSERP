<%@ Page Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="SalesPoreviewChecklist.aspx.cs" Inherits="Pages_SalesPoreviewChecklist" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="../Assets/css/datatables2.css" />
    <script type="text/javascript" src="../Assets/scripts/datatables2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=txtDate.ClientID %>').datepicker({
                format: 'dd/mm/yyyy'
            });
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
            $('#<%=gvEnquiryReviewCheckList.ClientID %>').DataTable({
                'aoColumnDefs': [{
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

        function ShowEnquiryAttachementsPopUp() {
            $('#mpeEnquiryAttachements').modal({
                backdrop: 'static'
            });
            return false;
        }

        function PrintSalesPOReviewCheckList() {
            var Address = $('#ContentPlaceHolder1_hdnAddress').val();
            var CompanyName = $('#ContentPlaceHolder1_hdnCompanyName').val();
            var PhoneAndFaxNo = $('#ContentPlaceHolder1_hdnPhoneAndFaxNo').val();
            var Email = $('#ContentPlaceHolder1_hdnEmail').val();
            var WebSite = $('#ContentPlaceHolder1_hdnWebsite').val();

            var div = $('#ContentPlaceHolder1_divCheckListPrint').html();

            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');
            winprint.document.write("<html><head><title>");
            winprint.document.write("Offer");
            winprint.document.write("</title>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='../Assets/css/ep-style.css'/>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='../Assets/css/main.css' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/print.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");

            winprint.document.write("<style type='text/css'> .heading_p{ font-size: 15px !important;color: #000 !important;margin: 0;font-weight: bold; }");
            winprint.document.write(".form-label{ margin-left: 20px; } .header{ width: 191mm;left: 8.5mm;border: 0px solid #000; } .page{border: 0px solid #000;}</style>");

            winprint.document.write("<div class='page'>");
            winprint.document.write("<table><thead><tr><td>");
            winprint.document.write("<div class='col-sm-12 header-space' style='text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;height:120px'>");
            winprint.document.write("<div class='header' style='border-bottom:1px solid;background:transparent'>");
            winprint.document.write("<div>");
            winprint.document.write("<div class='col-sm-12 padding0' id='divLSERPLogo' style='margin:0px auto;display:block;padding:5px 0px;'>");
            winprint.document.write("<div class='row'>");
            winprint.document.write("<div class='col-sm-2'>");
            winprint.document.write("<img src='../Assets/images/lonestar.jpeg' alt='lonestar-image' width='90px'>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-8 text-center'>");
            winprint.document.write("<h3 style='font-weight:600;font-size:22px;color:#000;font-family: Arial;display: contents;'>" + CompanyName + "</span > </h3>");
            winprint.document.write("<p style='font-weight:500;color:#000;width: 100%;'>" + Address + "</p>");
            winprint.document.write("<p style='font-weight:500;color:#000'>" + PhoneAndFaxNo + "</p>");
            winprint.document.write("<p style='font-weight:500;color:#000'>" + Email + "</p>");
            winprint.document.write("<p style='font-weight:500;color:#000'>" + WebSite + "</p>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-2'>");
            winprint.document.write("<img src='../Assets/images/iso.jpeg' alt='lonestar-image' height='90px'>");
            winprint.document.write("</div></div>");
            winprint.document.write("</div></div></div>");
            winprint.document.write("</td></tr></thead>");
            winprint.document.write("<tbody><tr><td>");
            winprint.document.write("<div class='col-sm-12 padding:0' style='padding:10px;'>");
            winprint.document.write(div);
            winprint.document.write("</div>");
            winprint.document.write("</td></tr></tbody>");
            winprint.document.write("<tfoot><tr><td>");
            winprint.document.write("</td></tr></tfoot></table>");
            winprint.document.write("</div>");
            winprint.document.write("</html>");
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
                                    <h3 class="page-title-head d-inline-block">Sales PO Review</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Sales</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Sales PO Review</li>
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
                    <asp:PostBackTrigger ControlID="gvEnquiryReviewCheckList" />
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
                                        <h3 class="sub-title">PO Specification</h3>
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
                                                    Payment Terms
                                                </label>
                                            </div>
                                            <div class="col-sm-5  p-t-10  p-b-10">
                                                <asp:RadioButtonList ID="rblpayment" runat="server" onclick="GetRadioButtonListSelectedValue(this);"
                                                    CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Accepted" Value="Accepted"></asp:ListItem>
                                                    <asp:ListItem Text="Not Accepted" Value="Not Accepted"></asp:ListItem>
                                                    <asp:ListItem Text="Write to Customer" Value="Write to Customer" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-4  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtpayment" runat="server" TextMode="MultiLine" placeHolder="Enter Payement Remarks"
                                                    ToolTip="Enter Payement Remarks"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    Packing Charges</label>
                                            </div>
                                            <div class="col-sm-5  p-t-10  p-b-10">
                                                <asp:RadioButtonList ID="rblpackingcharges" runat="server" onclick="GetRadioButtonListSelectedValue(this);"
                                                    CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Extra" Value="Extra"></asp:ListItem>
                                                    <asp:ListItem Text="Included" Value="Included"></asp:ListItem>
                                                    <asp:ListItem Text="Write to Customer" Value="Write to Customer" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-4  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtpackingcharges" runat="server" TextMode="MultiLine" placeHolder="Enter Packing Charges Remarks"
                                                    ToolTip="Enter Packing Charges Remarks"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    Inspection
                                                </label>
                                            </div>
                                            <div class="col-sm-5  p-t-10  p-b-10">
                                                <asp:RadioButtonList ID="rblInspection" runat="server" onclick="GetRadioButtonListSelectedValue(this);"
                                                    CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Internal" Value="Internal"></asp:ListItem>
                                                    <asp:ListItem Text="Customer" Value="Customer"></asp:ListItem>
                                                    <asp:ListItem Text="Third Party" Value="Third Party" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-4  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtInspection" runat="server" TextMode="MultiLine" PlaceHolder="Enter Inspection Remarks"
                                                    ToolTip="Enter Inspection Remarks"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    L D Clause
                                                </label>
                                            </div>
                                            <div class="col-sm-5  p-t-10  p-b-10">
                                                <asp:RadioButtonList ID="rblLDClause" runat="server" onclick="GetRadioButtonListSelectedValue(this);"
                                                    CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Applicable" Value="Applicable"></asp:ListItem>
                                                    <asp:ListItem Text="Not Applicable" Value="Not Applicable"></asp:ListItem>
                                                    <asp:ListItem Text="Write to Customer" Value="Write to Customer" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-4  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtLDClause" runat="server" TextMode="MultiLine" placeHolder="Enter L D Clause Remarks"
                                                    ToolTip="Enter L D Clause Remarks"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    Insurance
                                                </label>
                                            </div>
                                            <div class="col-sm-5  p-t-10  p-b-10">
                                                <asp:RadioButtonList ID="rblInsurance" runat="server" onclick="GetRadioButtonListSelectedValue(this);"
                                                    CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Applicable" Value="Applicable"></asp:ListItem>
                                                    <asp:ListItem Text="Not Applicable" Value="Not Applicable"></asp:ListItem>
                                                    <asp:ListItem Text="Write to Customer" Value="Write to Customer" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-4  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtInsurance" runat="server" TextMode="MultiLine" placeHolder="Enter Insurance Details"
                                                    ToolTip="Enter Insurance Details"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    Transporter
                                                </label>
                                            </div>
                                            <div class="col-sm-5  p-t-10  p-b-10">
                                                <asp:RadioButtonList ID="rblTransporter" runat="server" onclick="GetRadioButtonListSelectedValue(this);"
                                                    CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Available" Value="Available"></asp:ListItem>
                                                    <asp:ListItem Text="Not Available" Value="Not Available"></asp:ListItem>
                                                    <asp:ListItem Text="Write to Customer" Value="Write to Customer" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-4  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtTransporter" runat="server" TextMode="MultiLine" placeHolder="Enter Transporter Remarks"
                                                    ToolTip="Enter Transporter Remarks"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    E-Way Bill
                                                </label>
                                            </div>
                                            <div class="col-sm-5  p-t-10  p-b-10">
                                                <asp:RadioButtonList ID="rblRoadPermit" runat="server" onclick="GetRadioButtonListSelectedValue(this);"
                                                    CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Required" Value="Required"></asp:ListItem>
                                                    <asp:ListItem Text="Not Required" Value="Not Required"></asp:ListItem>
                                                    <asp:ListItem Text="Write to Customer" Value="Write to Customer" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-4  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtRoadPermit" runat="server" TextMode="MultiLine" placeHolder="Enter Road Permit Details"
                                                    ToolTip="Enter Road Permit Details"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 m-w-b">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    Delivery Schedule
                                                </label>
                                            </div>
                                            <div class="col-sm-5  p-t-10  p-b-10">
                                                <asp:RadioButtonList ID="rblDeliverySchedule" runat="server" onclick="GetRadioButtonListSelectedValue(this);"
                                                    CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Accepted" Value="Accepted"></asp:ListItem>
                                                    <asp:ListItem Text="Not Accepted" Value="Not Accepted"></asp:ListItem>
                                                    <asp:ListItem Text="Write to Customer" Value="Write to Customer" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-4  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtDeliverySchedule" runat="server" TextMode="MultiLine" placeHolder="Enter Delivery Schedule Remarks"
                                                    ToolTip="Enter Delivery Schedule Remarks"></asp:TextBox>
                                            </div>
                                        </div>
										<div class="col-sm-12 m-w-b">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    Guarantee Terms
                                                </label>
                                            </div>
                                            <div class="col-sm-5  p-t-10  p-b-10">
                                                <asp:RadioButtonList ID="rblGuaranteeTerms" runat="server" onclick="GetRadioButtonListSelectedValue(this);"
                                                    CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Applicable" Value="Applicable"></asp:ListItem>
                                                    <asp:ListItem Text="Not Applicable" Value="Not Applicable"></asp:ListItem>
                                                    <asp:ListItem Text="Write to Customer" Value="Write to Customer" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-4  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtGuaranteeRemarks" runat="server" TextMode="MultiLine" placeHolder="Enter Guarantee Terms Remarks"
                                                    ToolTip="Enter Guarantee Terms Remarks"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 m-w-b">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    GST
                                                </label>
                                            </div>
                                            <div class="col-sm-5  p-t-10  p-b-10">
                                                <asp:RadioButtonList ID="rblgst" runat="server" onclick="GetRadioButtonListSelectedValue(this);"
                                                    CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="SGST/CGST" Value="SGST/CGST"></asp:ListItem>
                                                    <asp:ListItem Text="IGST" Value="IGST"></asp:ListItem>
                                                    <asp:ListItem Text="NA" Value="NA" Selected="True"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-4  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtgst" runat="server" TextMode="MultiLine" placeHolder="Enter GST Remarks"
                                                    ToolTip="Enter GST Remarks"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-3  p-t-10  p-b-10">
                                                <label class="form-label">
                                                    Addtional Note</label>
                                            </div>
                                            <div class="col-sm-5  p-t-10  p-b-10">
                                                &nbsp;
                                            </div>
                                            <div class="col-sm-4  p-t-10  p-b-10">
                                                <asp:TextBox ID="txtAddtionalNote" runat="server" TextMode="MultiLine" placeHolder="Enter Additional Remarks"
                                                    ToolTip="Enter Addtional Remarks" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-50">
                                        <div class="col-sm-2">
                                            <div class="text-left">
                                                <label class="form-label mandatorylbl">
                                                    Date</label>
                                            </div>
                                        </div>
                                        <div class=" col-sm-4 text-left">
                                            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control mandatoryfield datepicker"
                                                TabIndex="10" autocomplete="nope" PlaceHolder="Date"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-6">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-50">
                                        <div class="col-sm-6">
                                            <asp:LinkButton ID="btndiscrepancy" runat="server" Text="Submit" CssClass="btn btn-cons btn-save AlignTop"
                                                OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divMainform')" OnClick="btndiscrepancye_Click"></asp:LinkButton>
                                            <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-cons btn-danger AlignTop"
                                                OnClick="btnCancel_Click"></asp:LinkButton>
                                        </div>
                                        <div class=" col-sm-6 text-right">
                                            <asp:LinkButton ID="btnnodiscrepancy" runat="server" Text="Discrepancy" CssClass="btn btn-cons btn-save AlignTop"
                                                OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divMainform')" OnClick="btnnodiscrepancy_Click"></asp:LinkButton>
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
                                                    <asp:Label ID="lbltitle" runat="server" Text="Sales PO Review Check List Details"></asp:Label>
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
                                                OnRowCommand="gvEnquiryReviewCheckList_OnRowCommand" DataKeyNames="POHID,EnquiryNumber,PoCopy,PoCopyWithoutPrice">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PO Number" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("PONumber")%>'></asp:Label>
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
                                                    <asp:TemplateField HeaderText="Designer Review Status" ItemStyle-HorizontalAlign="left"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCompletionStatus" runat="server" Text='<%# Eval("Completionsattus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sales Review Status" ItemStyle-HorizontalAlign="left"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSalesCompletionStatus" runat="server" Text='<%# Eval("SalesCompletionstatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View Review Details" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnView" runat="server" CommandName="ViewEnquiryReviewCheckList"
                                                                CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                           <img src="../Assets/images/view.png" alt="" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View Enq Attachements" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnViewEnqAttch" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="ViewEnqAttachements"><img src="../Assets/images/view.png" /></asp:LinkButton>
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
                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="EditEnquiryReviewCheckList"
                                                                CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                           <img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PDF Review" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnPdf" runat="server" CommandName="PDFReviewChecklist" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                          <img src="../Assets/images/pdf.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <asp:HiddenField ID="hdnClarifyID" runat="server" Value="0" />

                                        <asp:HiddenField ID="hdnAddress" runat="server" Value="" />
                                        <asp:HiddenField ID="hdnPhoneAndFaxNo" runat="server" Value="" />
                                        <asp:HiddenField ID="hdnEmail" runat="server" Value="" />
                                        <asp:HiddenField ID="hdnWebsite" runat="server" Value="" />
										<asp:HiddenField ID="hdnCompanyName" runat="server" Value="" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12" id="divCheckListPrint" runat="server" style="display: none;">
                        <div class="col-sm-12 text-right">
                            <asp:Label ID="lblControlofRecords_p" Style="font-weight: 900;" runat="server"></asp:Label>
                        </div>
                        <div class="col-sm-12 text-center">
                            <label class="heading_p">
                                CONTRACT REVIEW</label>
                        </div>
                        <div class="col-sm-12 text-right">
                            <asp:Label ID="lblPRDate_p" runat="server"></asp:Label>
                        </div>
                        <div class="col-sm-12 m-tb-5">
                            <div class="col-sm-4">
                                <label class="form-label" style="font-weight: 700">
                                    Customer Name</label>
                            </div>
                            <div class="col-sm-1 text-center colon">
                                :
                            </div>
                            <div class="col-sm-7">
                                <asp:Label ID="lblNameOftheCustomer_P" Text="" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 m-tb-5">
                            <div class="col-sm-4">
                                <label class="form-label" style="font-weight: 700">
                                    Customer PO No</label>
                            </div>
                            <div class="col-sm-1 text-center colon">
                                :
                            </div>
                            <div class="col-sm-7">
                                <asp:Label ID="lblCustomerPONo_p" Text="" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12  m-tb-5">
                            <div class="col-sm-4">
                                <label class="form-label" style="font-weight: 700">
                                    Item</label>
                            </div>
                            <div class="col-sm-1 text-center colon">
                                :
                            </div>
                            <div class="col-sm-7">
                                <asp:Label ID="lblItem_p" Text="" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 m-tb-5">
                            <div class="col-sm-4">
                                <label class="form-label" style="font-weight: 700">
                                    Units</label>
                            </div>
                            <div class="col-sm-1 text-center colon">
                                :
                            </div>
                            <div class="col-sm-7">
                                <asp:Label ID="lblUnits_p" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 m-tb-5">
                            <div class="col-sm-4">
                                <label class="form-label" style="font-weight: 700">
                                    U Stamp Required</label>
                            </div>
                            <div class="col-sm-1 text-center colon">
                                :
                            </div>
                            <div class="col-sm-7">
                                <asp:Label ID="lblUStampRequired_p" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 m-tb-5">
                            <div class="col-sm-4">
                                <label class="form-label" style="font-weight: 700">
                                    U2 Stamp Required
                                </label>
                            </div>
                            <div class="col-sm-1 text-center colon">
                                :
                            </div>
                            <div class="col-sm-7">
                                <asp:Label ID="lblU2StampRequired_p" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 m-t-5">
                            <div class="col-sm-4">
                                <label class="form-label" style="font-weight: 700">
                                    PED Required (As Per 2014/68/EU)
                                </label>
                            </div>
                            <div class="col-sm-1 text-center colon">
                                :
                            </div>
                            <div class="col-sm-7">
                                <asp:Label ID="lblPEDRequired_p" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 m-tb-5">
                            <div class="col-sm-4">
                                <label class="form-label" style="font-weight: 700">
                                    ISO 3438-2 Required
                                </label>
                            </div>
                            <div class="col-sm-1 text-center colon">
                                :
                            </div>
                            <div class="col-sm-7">
                                <asp:Label ID="lblISO_p" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 m-tb-5">
                            <div class="col-sm-4">
                                <label class="form-label" style="font-weight: 700">
                                    IBR/Non-IBR Required
                                </label>
                            </div>
                            <div class="col-sm-1 text-center colon">
                                :
                            </div>
                            <div class="col-sm-7">
                                <asp:Label ID="lblIBRRequired_p" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 m-tb-5">
                            <div class="col-sm-4">
                                <label class="form-label" style="font-weight: 700">
                                    Checked with Offer
                                </label>
                            </div>
                            <div class="col-sm-1 text-center colon">
                                :
                            </div>
                            <div class="col-sm-7">
                                <asp:Label ID="lblCheckedwithoffer_p" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 m-tb-5">
                            <div class="col-sm-4">
                                <label class="form-label" style="font-weight: 700">
                                    Technical Feasibility
                                </label>
                            </div>
                            <div class="col-sm-1 text-center colon">
                                :
                            </div>
                            <div class="col-sm-7">
                                <asp:Label ID="lblTechnicalFeasibility_p" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 m-tb-5">
                            <div class="col-sm-4">
                                <label class="form-label" style="font-weight: 700">
                                    Information like Drg Design data,QAP etc
                                </label>
                            </div>
                            <div class="col-sm-1 text-center colon">
                                :
                            </div>
                            <div class="col-sm-7">
                                <asp:Label ID="lblQAp_p" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 m-tb-5">
                            <div class="col-sm-4">
                                <label class="form-label" style="font-weight: 700">
                                    Statutory/Regulatory
                                </label>
                            </div>
                            <div class="col-sm-1 text-center colon">
                                :
                            </div>
                            <div class="col-sm-7">
                                <asp:Label ID="lblStatutary_p" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 m-tb-5">
                            <div class="col-sm-4">
                                <label class="form-label" style="font-weight: 700">
                                    Special Information
                                </label>
                            </div>
                            <div class="col-sm-1 text-center colon">
                                :
                            </div>
                            <div class="col-sm-7">
                                <asp:Label ID="lblSpecialInformation_p" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="text-center" style="padding: 10px 0px; font-weight: 700; color: Black;">
                            Commercial Terms:
                        </div>
						
                        <div class="col-sm-12 m-tb-5">
                            <div class="col-sm-4">
                                <label class="form-label" style="font-weight: 700">
                                    Payment Terms
                                </label>
                            </div>
                            <div class="col-sm-1 text-center colon">
                                :
                            </div>
                            <div class="col-sm-7">
                                <asp:Label ID="lblPaymentTerms_p" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 m-tb-5">
                            <div class="col-sm-4">
                                <label class="form-label" style="font-weight: 700">
                                    Packing Charges</label>
                            </div>
                            <div class="col-sm-1 text-center colon">
                                :
                            </div>
                            <div class="col-sm-7">
                                <asp:Label ID="lblPackingCharges_p" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 m-tb-5">
                            <div class="col-sm-4">
                                <label class="form-label" style="font-weight: 700">
                                    Inspection
                                </label>
                            </div>
                            <div class="col-sm-1 text-center colon">
                                :
                            </div>
                            <div class="col-sm-7">
                                <asp:Label ID="lblInspection_p" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 m-tb-5">
                            <div class="col-sm-4">
                                <label class="form-label" style="font-weight: 700">
                                    LD Clause</label>
                            </div>
                            <div class="col-sm-1 text-center colon">
                                :
                            </div>
                            <div class="col-sm-7">
                                <asp:Label ID="lblLDClause_p" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 m-tb-5">
                            <div class="col-sm-4">
                                <label class="form-label" style="font-weight: 700">
                                    Insurance</label>
                            </div>
                            <div class="col-sm-1 text-center colon">
                                :
                            </div>
                            <div class="col-sm-7">
                                <asp:Label ID="lblInsurance_p" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 m-tb-5">
                            <div class="col-sm-4">
                                <label class="form-label" style="font-weight: 700">
                                    E-Way Bill</label>
                            </div>
                            <div class="col-sm-1 text-center colon">
                                :
                            </div>
                            <div class="col-sm-7">
                                <asp:Label ID="lblEwayBill_p" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 m-tb-5">
                            <div class="col-sm-4">
                                <label class="form-label" style="font-weight: 700">
                                    Trans Porter</label>
                            </div>
                            <div class="col-sm-1 text-center colon">
                                :
                            </div>
                            <div class="col-sm-7">
                                <asp:Label ID="lblTransporter_p" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 m-tb-5">
                            <div class="col-sm-4">
                                <label class="form-label" style="font-weight: 700">
                                    Delivery Schdule</label>
                            </div>
                            <div class="col-sm-1 text-center colon">
                                :
                            </div>
                            <div class="col-sm-7">
                                <asp:Label ID="lblDeliverySchedule_p" runat="server"></asp:Label>
                            </div>
                        </div>
						<div class="col-sm-12 m-tb-5">
                            <div class="col-sm-4">
                                <label class="form-label" style="font-weight: 700">
                                    GuaranteeTerms
                                </label>
                            </div>
                            <div class="col-sm-1 text-center colon">
                                :
                            </div>
                            <div class="col-sm-7">
                                <asp:Label ID="lblGuaranteeTerms_p" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 m-tb-5">
                            <div class="col-sm-4">
                                <label class="form-label" style="font-weight: 700">
                                    GST
                                </label>
                            </div>
                            <div class="col-sm-1 text-center colon">
                                :
                            </div>
                            <div class="col-sm-7">
                                <asp:Label ID="lblGSt_p" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 text-left m-tb-5">
                            <div class="col-sm-4">
                                <label class="form-label" style="font-weight: 700">
                                    Remarks
                                </label>
                            </div>
                            <div class="col-sm-1 text-center colon">
                                :
                            </div>
                            <div class="col-sm-7">
                                <asp:Label ID="lblAddtionalNote_p" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 text-left m-tb-5">
                            <label class="form-label" style="font-weight: 700">
                                Signature
                            </label>
                        </div>
                        <div class="col-sm-12 text-left">
                            <label class="form-label" style="font-weight: 700">
                                Date
                            </label>
                        </div>
                        <div class="col-sm-12 text-center" style="font-weight: 700; margin-top: 50px">
                            <div class="col-sm-6 text-center">
                                <label>
                                    Marketing
                                </label>
                            </div>
                            <div class="col-sm-6 text-center">
                                <label>
                                    Design
                                </label>
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
                                        <asp:Label ID="lblponumber_V" Style="font-weight: bold;" Text="" runat="server" />
                                    </div>
                                </div>
                                <div class="p-t-10 col-sm-12">
                                    <div class="col-sm-4  p-t-10">
                                        <label class="form-label">
                                            Po Date</label><asp:Label ID="lblPoDate_V" Style="font-weight: bold;" Text="" runat="server" />
                                    </div>
                                    <div class="col-sm-4  p-t-10">
                                        <label class="form-label">
                                            Completion Status</label><asp:Label ID="lblcompletion_V" Style="font-weight: bold;"
                                                Text="" runat="server" />
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
                                        <h3 class="sub-title">Design PO Review Clarrification</h3>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                U Stamp Required</label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblUStamp_V" runat="server" />
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblUStampRemarks_V" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-sm-12 m-w-b">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                U2 Stamp Required</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:Label ID="lblU2Stamp_V" runat="server" />
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblU2StampRemarks_V" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                PED Required (as per 2014/68/EU)</label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblPED_V" runat="server" />
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblPEDRemarks_V" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                IBR/Non-IBR Required</label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblIBR_V" runat="server" />
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblIBRRemarks_V" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                ISO 3834-2 Required</label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblISO_V" runat="server" />
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblISORemarks_V" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Special Information</label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblsplinfo_V" runat="server" />
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblsplinfoRemarks_V" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Units</label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblUnits_V" runat="server" />
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblUnitsRemarks_V" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Checked with Offer</label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lbloffercheck_V" runat="server" />
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lbloffercheckRemarks_V" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Technical Feasibility</label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lbltechnicalfeasibile_V" runat="server" />
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lbltechnicalfeasibileRemarks_V" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Information like Drg Design data,QAP etc</label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblinfo_V" runat="server" />
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblinfoRemarks_V" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Statutory/Regulatory</label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblStatutory_V" runat="server" />
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblStatutoryRemarks_V" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                PRDate</label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            &nbsp;
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="Label20" runat="server" />
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
                                            <asp:Label ID="lblAddtionalNote_v" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <h3 class="sub-title">Sales PO Review Clarrification</h3>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Payment</label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblPayment_V" runat="server" />
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblPaymentRemarks_V" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Packing charges</label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblPackingCharges_V" runat="server" />
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblPackingChargesRemarks_V" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Inspection</label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblInspection_V" runat="server" />
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblInspectionRemarks_V" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                LDclause</label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblLDClause_V" runat="server" />
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblLDClauseRemarks_V" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Insurance</label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblInsurance_V" runat="server" />
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblInsuranceRemarks_V" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Transporter</label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblTransporter_V" runat="server" />
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblTransporterRemarks_V" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Roadpermit</label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblRoadpermit" runat="server" />
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblRoadpermitRemarks_V" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                Deliveryschedule</label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblDeliveryschedule_V" runat="server" />
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblDeliveryscheduleRemarks_V" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <label class="form-label">
                                                GST</label>
                                        </div>
                                        <div class="col-sm-3  p-t-10  p-b-10">
                                            <asp:Label ID="lblGST_V" runat="server" />
                                        </div>
                                        <div class="col-sm-6  p-t-10  p-b-10">
                                            <asp:Label ID="lblGSTRemarks_V" runat="server" />
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
                                            <asp:Label ID="lblAddtionalnote" runat="server" />
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10 p-b-10">
                                        <div class="col-sm-6">
                                            <asp:LinkButton ID="lbtnSendCommunication" runat="server" Text="SendCommunication"
                                                OnClientClick="return ShowSendCommunicationPopUp();" CssClass="btn btn-con btn-send" />
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:LinkButton ID="lbtnViewCommunication" runat="server" Text="ViewCommunication"
                                                OnClick="btnViewCommunication_Click" CssClass="btn btn-con btn-view" />
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
                                    <asp:Label ID="Label2" Text="" runat="server"></asp:Label>
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
    <div class="modal" id="mpeEnquiryAttachements" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="upView" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">Enquiry Attachements Details</h4>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="docdiv" class="docdiv">
                                <div class="col-sm-12 p-t-10">
                                    <asp:GridView ID="gvAttachments" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                        EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                        OnRowCommand="gvAttachments_OnRowCommand" OnRowDataBound="gvAttachments_OnRowDataBound"
                                        DataKeyNames="AttachementID">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Attachement Type Name" ItemStyle-HorizontalAlign="left"
                                                HeaderStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAttachementTypeName_V" runat="server" Text='<%# Eval("AttachementTypeName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescription_V" runat="server" Text='<%# Eval("Description")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFileName_V" runat="server" Visible="false" Text='<%# Eval("FileName")%>'></asp:Label>
                                                    <asp:ImageButton ID="imgbtnView" runat="server" ImageUrl="" CommandName="ViewDocs"
                                                        Width="20px" Height="20px" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hdnAttachementID" runat="server" Value="0" />
                        <div class="modal-footer">
                            <div class="col-sm-12 p-t-10">
                                <div class="col-sm-10">
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hdnAttachementFlag" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
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
