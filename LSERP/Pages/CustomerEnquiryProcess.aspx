<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="CustomerEnquiryProcess.aspx.cs" Inherits="Pages_CustomerEnquiryProcess" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="../Assets/css/datatables2.css" />
    <script type="text/javascript" src="../Assets/scripts/datatables2.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $('#<%=txtReceivedDate.ClientID %>').datepicker({
                format: 'dd/mm/yyyy'
            });
            $('#<%=txtDeadLineDate.ClientID %>').datepicker({
                format: 'dd/mm/yyyy'
            });
        });

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                $('.datepicker').datepicker({ format: 'dd/mm/yyyy' });
            }
        });

        function showDataTable() {
            $('#<%=gvCustomerEnquiry.ClientID %>').DataTable({ 'aoColumnDefs': [{
                'bSortable': false,
                'aTargets': [-1, -2] /* 1st one, start by the right */
            }]
            });
        }


        //        function ValidateControl(controlID) {

        //            var ClientID = 'ContentPlaceHolder1';

        //            var ControlID = control.split(',');

        //            $.each(ControlID, function (i) {
        //                var message = ControlID[i].split('/');
        //                $(ClientID + '_' + message[0]).notify(message[1], { arrowShow: true, position: 'r,r', autoHide: true });
        //            });
        //        }    


        function ValidateAdd() {

            var msg = true;
            var Receiveddate = $('#<%=txtReceivedDate.ClientID %>').val();

            var todayDate = new Date();

            if ($('#<%=txtCustomerEnquiryNumber.ClientID %>').val() == "") {
                $('#<%=txtCustomerEnquiryNumber.ClientID %>').notify('Field Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }

            if ($('#<%=ddlProspect.ClientID %>')[0].selectedIndex == 0) {
                $('#<%=ddlProspect.ClientID %>').notify('Field Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }
            if ($('#<%=txtContactPerson.ClientID %>').val() == "") {
                $('#<%=txtContactPerson.ClientID %>').notify('Field Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }
            if ($('#<%=txtContactNumber.ClientID %>').val() == "") {
                $('#<%=txtContactNumber.ClientID %>').notify('Field Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }

            if ($('#<%=txtEmail.ClientID %>').val() == "") {
                $('#<%=txtEmail.ClientID %>').notify('Field Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }

            else if (isValidEmail($('#<%=txtEmail.ClientID %>').val()) == false) {
                $('#<%=txtEmail.ClientID %>').notify('Please Enter Valid Email.', { arrowShow: true, position: 'r,r', autoHide: true });
                $("<%=txtEmail.ClientID %>").focus();
                msg = false;
            }

            if ($('#<%=txtProjectDescription.ClientID %>').val() == "") {
                $('#<%=txtProjectDescription.ClientID %>').notify('Field Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }
            if ($('#<%=ddlEnquiryType.ClientID %>')[0].selectedIndex == 0) {
                $('#<%=ddlEnquiryType.ClientID %>').notify('Field Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }

            if ($('#<%=txtReceivedDate.ClientID %>').val() == "") {
                $('#<%=txtReceivedDate.ClientID %>').notify('Field Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }

            else if ($('#<%=txtReceivedDate.ClientID %>').val() != "") {
                var RDParts = Receiveddate.split("/");
                var d1 = new Date(RDParts[1] + "/" + RDParts[0] + "/" + RDParts[2]);

                if (d1 >= todayDate) {
                    $('#<%=txtReceivedDate.ClientID %>').notify('The Received Date Should Be Less Than Equal To Current Date.', { arrowShow: true, position: 'r,r', autoHide: true });
                    msg = false;
                }

            }
            if ($('#<%=txtDeadLineDate.ClientID %>').val() == "") {
                $('#<%=txtDeadLineDate.ClientID %>').notify('Field Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }

            else if ($('#<%=txtDeadLineDate.ClientID %>').val() != "") {
                var DeadLineDate = $('#<%=txtDeadLineDate.ClientID %>').val();
                var DDParts = DeadLineDate.split("/");
                var d1 = new Date(RDParts[1] + "/" + RDParts[0] + "/" + RDParts[2]);
                var d2 = new Date(DDParts[1] + "/" + DDParts[0] + "/" + DDParts[2]);
                if (d1 > d2) {
                    $('#<%=txtDeadLineDate.ClientID %>').notify('The Deadline Date Should Be greater Than Received  Date.', { arrowShow: true, position: 'r,r', autoHide: true });
                    msg = false;
                }

            }

            if ($('#<%=ddlEnquiryLocation.ClientID %>')[0].selectedIndex == 0) {
                $('#<%=ddlEnquiryLocation.ClientID %>').notify('Field Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }

            if ($('#<%=ddlSalesResource.ClientID %>')[0].selectedIndex == 0) {
                $('#<%=ddlSalesResource.ClientID %>').notify('Field Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }
            if ($('#<%=ddlSource.ClientID %>')[0].selectedIndex == 0) {
                $('#<%=ddlSource.ClientID %>').notify('Field Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }
            if ($('#<%=ddlRfpGroupID.ClientID %>')[0].selectedIndex == 0) {
                $('#<%=ddlRfpGroupID.ClientID %>').notify('Field Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }

            if ($('#<%=fAttachment.ClientID %>').val() != "") {
                var validFilesTypes = ["doc", "pdf", "docx", "jpg", "jpeg", "xlsx", "xls"];
                if ($.inArray($('#<%=fAttachment.ClientID %>').val().split('.').pop().toLowerCase(), validFilesTypes) == -1) {
                    $('#<%=fAttachment.ClientID %>').notify('Invalid File.\n Please upload a File with extension: .doc , .docx ,.xlsx,.xls, .pdf , .jpg , .jpeg', { arrowShow: true, position: 'r,r', autoHide: true });
                    msg = false;
                }
            }
            if (msg == true) {
                showLoader();
                return true;
            }
            else {
                return false;
            }
        }
        function ValidateAttchements() {

            var msg = true;

            if ($('#<%=ddlTypeName.ClientID %>')[0].selectedIndex == 0) {
                $('#<%=ddlTypeName.ClientID %>').notify('Fields Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }
            if ($('#<%=txtDescription.ClientID %>').val() == "") {
                $('#<%=txtDescription.ClientID %>').notify('Fields Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }
            if ($('#<%=fAttachment.ClientID %>').val() == "") {
                $('#<%=fAttachment.ClientID %>').notify('Please Upload File', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }
            else if ($('#<%=fAttachment.ClientID %>').val() != "") {
                var validFilesTypes = ["doc", "pdf", "docx", "jpg", "jpeg", "xlsx", "xls"];
                var file = $('#<%=fAttachment.ClientID%>');
                var path = file.value;
                //      var ext = path.substring(path.lastIndexOf(".") + 1, path.length).toLowerCase();
                var ext = file.value.substring(file.value.lastIndexOf('.') + 1).toLowerCase();
                var isValidFile = false;
                for (var i = 0; i < validFilesTypes.length; i++) {
                    if (ext == validFilesTypes[i]) {
                        isValidFile = true;
                        break;
                    }
                }
                if (path != "") {
                    if (!isValidFile) {
                        $('#<%=fAttachment.ClientID %>').notify('Invalid File.\n Please upload a File with extension: .doc , .docx ,.xlsx,.xls, .pdf , .jpg , .jpeg', { arrowShow: true, position: 't,r', autoHide: true });
                        msg = false;
                    }
                }
            }
            if (msg == true) {
                return true;
            }
            else {
                return false;
            }
        }

        function ShowCloseViewPopUp() {
            $("#mpeView").modal("hide");
            ShowAddPopup();
            $('#<%=btnSaveAttachements.ClientID %>').css("display", "block");
            $('#<%=btnCancelAttachements.ClientID %>').css("display", "block");
            return false;

        }

        function ShowAddPopup() {
            $('#mpeAdd').modal({
                backdrop: 'static'
            });
            $('#<%=btnSaveAttachements.ClientID %>').css("display", "none");
            $('#<%=btnCancelAttachements.ClientID %>').css("display", "none");
            return false;
        }

        function ShowViewPopUp() {
            $('#mpeView').modal({
                backdrop: 'static'

            });
            return false;
        }

        function ShowViewDocs() {
            $('#divView').modal({
                backdrop: 'static'
            });

            return false;
        }

        function showAdd() {
            $('#mpeAdd').modal({
                backdrop: 'static'
            });
        }

        function ValidateControl(controlID) {
            var message = controlID.split('/');
            $('#' + message[0]).notify(message[1], { arrowShow: true, position: 'r,r', autoHide: true, autoHideDelay: 5000 });

        }

        function btnClearAttachements() {
            $('#<%=ddlTypeName.ClientID %>')[0].selectedIndex = 0;
            $('#<%=txtDescription.ClientID %>')[0].value = "";
            $('#<%=fAttachment.ClientID %>').value = "";
            $("#mpeAdd").modal("hide");
            ShowViewPopUp();
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
                                        Enquiry Process</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Leads & Enquiry</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Enquiry Process</li>
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
                    <asp:PostBackTrigger ControlID="btnSave" />
                    <%--<asp:PostBackTrigger ControlID="imgPdf" />--%>
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" runat="server">
                            <div class="text-center">
                                <asp:LinkButton ID="btnAddNew" runat="server" Text="Add New Enquiry" OnClick="btnAddNew_Click"
                                    CssClass="btn btn-success add-emp"></asp:LinkButton>
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div id="divEnquiryNumber" class="col-sm-12" runat="server">
                                    <div class="col-sm-6 text-right">
                                        <label class="form-label">
                                            Enquiry Number :
                                        </label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:Label ID="lblEnquiryNumber" Style="font-weight: bold;" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4 text-right">
                                        <div class="text-left">
                                            <label class="form-label">
                                                Customer Enquiry Number</label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtCustomerEnquiryNumber" runat="server" CssClass="form-control"
                                                ToolTip="Enter Customer Enquiry Number" autocomplete="nope" TabIndex="1" placeholder="Enter Customer Enquiry Number">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 text-right">
                                        <div class="text-left">
                                            <label class="form-label">
                                                Prospect Name</label>
                                        </div>
                                        <div>
                                            <asp:DropDownList ID="ddlProspect" runat="server" TabIndex="2" ToolTip="Select Prospect"
                                                CssClass="form-control">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <asp:Label ID="lblContactPerson" runat="server" class="form-label" Text="Contact Person"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtContactPerson" runat="server" placeholder="Enter Contact Person"
                                                CssClass="form-control" MaxLength="100" TabIndex="3" autocomplete="nope"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <asp:Label ID="lblEmail" runat="server" class="form-label" Text="Email id"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtEmail" runat="server" TabIndex="4" placeholder="Enter Email"
                                                CssClass="form-control" MaxLength="100" onkeypress="return validateEmail(this);"
                                                autocomplete="nope"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <asp:Label ID="lblContactNumber" runat="server" class="form-label" Text="Contact Number"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtContactNumber" runat="server" TabIndex="5" placeholder="Enter Contact Number"
                                                ToolTip="Enter Contact Number" CssClass="form-control" MaxLength="15" onkeypress="return validationNumeric(this);"
                                                autocomplete="nope"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <asp:Label ID="lblProjectDescription" runat="server" class="form-label" Text="Project Description"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtProjectDescription" runat="server" TabIndex="6" placeholder="Enter Project Description"
                                                ToolTip="Project Description" TextMode="MultiLine" CssClass="form-control" MaxLength="300"
                                                autocomplete="nope"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <asp:Label ID="lblCommercialOffer" runat="server" class="form-label" Text="Commercial Offer"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:RadioButtonList ID="rblCommercialOffer" runat="server" CssClass="radio radio-success"
                                                RepeatDirection="Horizontal" RepeatLayout="Flow" TabIndex="7">
                                                <asp:ListItem Selected="True" Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label">
                                                Enquiry Type</label>
                                        </div>
                                        <div>
                                            <asp:DropDownList ID="ddlEnquiryType" runat="server" TabIndex="8" ToolTip="Select Enquiry Type"
                                                CssClass="form-control">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label">
                                                EMD Amount</label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtEMDAmount" runat="server" TabIndex="9" placeholder="Enter Amount"
                                                ToolTip="Enter Amount" CssClass="form-control" onkeypress="return validationNumeric(this);"
                                                autocomplete="nope"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label">
                                                Received Date</label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtReceivedDate" runat="server" CssClass="form-control datepicker"
                                                TabIndex="10" autocomplete="nope" PlaceHolder="Received Date"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label">
                                                Customer DeadLine Date</label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtDeadLineDate" runat="server" TabIndex="9" placeholder="Enter Deadline Date"
                                                ToolTip="Enter Deadline Date" CssClass="form-control datepicker" autocomplete="nope"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label">
                                                Enquiry Location</label>
                                        </div>
                                        <div>
                                            <asp:DropDownList ID="ddlEnquiryLocation" runat="server" TabIndex="10" ToolTip="Select Enquiry Location"
                                                CssClass="form-control">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Domestic"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="InterNational"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label">
                                                Budgetary Enquiry</label>
                                        </div>
                                        <div>
                                            <asp:RadioButtonList ID="rblBudgetary" runat="server" CssClass="radio radio-success"
                                                RepeatDirection="Horizontal" RepeatLayout="Flow" TabIndex="11">
                                                <asp:ListItem Selected="True" Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Value="0">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label">
                                                How Sourced</label>
                                        </div>
                                        <div>
                                            <asp:DropDownList ID="ddlSource" runat="server" ToolTip="Select Source" TabIndex="9"
                                                AutoPostBack="false" CssClass="form-control">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Adverdisement/Enquiry"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Referral"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Sales Cold Call"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="Other---"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label">
                                                Sales Resource</label>
                                        </div>
                                        <div>
                                            <asp:DropDownList ID="ddlSalesResource" runat="server" TabIndex="10" ToolTip="Select Sales Resource"
                                                CssClass="form-control">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4" id="divAttachements" runat="server">
                                        <div class="text-left">
                                            <label class="form-label">
                                                Attachments</label>
                                        </div>
                                        <div>
                                            <asp:Label ID="lblAttachements" runat="server"></asp:Label>
                                            <asp:LinkButton ID="lbtnAddAttachments" runat="server" Text="Add Attachments" OnClientClick="return ShowAddPopup('lbtnAddAttachments');"
                                                CssClass="btn btn-success add-emp"></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label">
                                                Rfp Group Name</label>
                                        </div>
                                        <div>
                                            <asp:DropDownList ID="ddlRfpGroupID" runat="server" TabIndex="11" ToolTip="Select Rfp GroupID"
                                                CssClass="form-control">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="A"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="B"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="C"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="D"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-20">
                                    <asp:LinkButton ID="btnSave" runat="server" Text="Save" OnClientClick="return ValidateAdd();"
                                        OnClick="btnSave_Click" CssClass="btn btn-cons btn-save AlignTop"></asp:LinkButton>
                                    <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                        CssClass="btn btn-cons btn-danger AlignTop"></asp:LinkButton>
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
                                                    <asp:Label ID="lbltitle" runat="server" Text="Customer Enquiry Details"></asp:Label></p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvCustomerEnquiry" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" OnRowCommand="gvCustomerEnquiry_OnRowCommand"
                                                CssClass="table table-hover table-bordered medium" DataKeyNames="EnquiryID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Customer Enquiry Number" ItemStyle-HorizontalAlign="left"
                                                        HeaderStyle-Width="200px" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCustomerEnquiryNumber" runat="server" Text='<%# Eval("CustomerEnquiryNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Prospect Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProspectName" runat="server" Text='<%# Eval("ProspectName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Contact Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblContactName" runat="server" Text='<%# Eval("ConTactPerson")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rceived Date" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReceivedDate" runat="server" Text='<%# Eval("ReceivedDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Enquiry Location" HeaderStyle-Width="86px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEnquiryLocation" runat="server" Text='<%# Eval("EnquiryLocation")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnView" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="View"><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="EditEnquiry">
                                                           <img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnLseNumberMaxID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnEnquiryID" runat="server" Value="0" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="modal" id="mpeAdd">
            <div class="modal-dialog">
                <div class="modal-content">
                    <asp:UpdatePanel ID="upAdd" runat="server" UpdateMode="Always">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnSaveAttachements" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="modal-header">
                                <h4 class="modal-title ADD">
                                    Add New Attachments</h4>
                                <button id="btnClosePopup" type="button" class="close btn-primary-purple" data-dismiss="modal"
                                    aria-hidden="true">
                                    ×</button>
                            </div>
                            <div class="modal-body" style="padding: 0px;">
                                <div class="inner-container">
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-3">
                                            <label class="form-label">
                                                Type Name
                                            </label>
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:DropDownList ID="ddlTypeName" runat="server" TabIndex="1" ToolTip="Select Attachement type"
                                                CssClass="form-control">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Enquiry Related"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="Drawing"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="Financial Documents"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="Others"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-3">
                                            <label class="form-label">
                                                Attachment Description
                                            </label>
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:TextBox ID="txtDescription" runat="server" TabIndex="6" placeholder="Enter Attachement Description"
                                                ToolTip="Enter Attachment Description" TextMode="MultiLine" CssClass="form-control"
                                                MaxLength="300" autocomplete="nope"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-3">
                                            <label class="form-label">
                                                Attachement
                                            </label>
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:FileUpload ID="fAttachment" runat="server" TabIndex="12" class="form-control"
                                                ClientIDMode="Static" Width="95%" />
                                            <asp:Label ID="lblAttachementFileName" runat="server"></asp:Label>
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
                                            <asp:LinkButton Text="Save" CssClass="btn btn-cons btn-save  AlignTop" ID="btnSaveAttachements"
                                                runat="server" OnClientClick="return ValidateAttchements();" OnClick="btnSaveAttchement_Click" />
                                        </div>
                                    </div>
                                    <div class="col-sm-3">
                                        <asp:LinkButton Text="Cancel" CssClass="btn btn-cons btn-save  AlignTop" ID="btnCancelAttachements"
                                            runat="server" OnClientClick="return btnClearAttachements();" />
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
        <div class="modal" id="mpeView">
            <div class="modal-dialog">
                <div class="modal-content">
                    <asp:UpdatePanel ID="upView" runat="server" UpdateMode="Always">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="gvAttachments" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="modal-header">
                                <h4 class="modal-title ADD">
                                    Enquiry Details</h4>
                                <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                    ×</button>
                            </div>
                            <div class="modal-body" style="padding: 0px;">
                                <div class="inner-container">
                                    <div class="col-sm-12">
                                        <div class="col-sm-4">
                                            <div>
                                                <label class="form-label">
                                                    Enquiry Number</label>
                                            </div>
                                            <div>
                                                <asp:Label ID="lblEnquiryNumber_V" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <label class="form-label">
                                                    Customer Enquiry Number</label>
                                            </div>
                                            <div>
                                                <asp:Label ID="lblCustomerEnquiryNumber_V" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <label class="form-label">
                                                    Prospect Name</label>
                                            </div>
                                            <div>
                                                <asp:Label ID="lblProspectName_V" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <label class="form-label">
                                                    Contact Person</label>
                                            </div>
                                            <div>
                                                <asp:Label ID="lblContactPerson_V" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <label class="form-label">
                                                    Contact Number</label>
                                            </div>
                                            <div>
                                                <asp:Label ID="lblContactNumber_V" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <label class="form-label">
                                                    Email ID</label>
                                            </div>
                                            <div>
                                                <asp:Label ID="lblEmailID_V" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <label class="form-label">
                                                    Project Description
                                                </label>
                                            </div>
                                            <div>
                                                <asp:Label ID="lblProjectDescription_V" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <label class="form-label">
                                                    Commercial Offer</label>
                                            </div>
                                            <div>
                                                <asp:Label ID="lblCommercialOffer_V" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <label class="form-label">
                                                    Enquiry Type Name
                                                </label>
                                            </div>
                                            <div>
                                                <asp:Label ID="lblEnquiryTypeName_V" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <label class="form-label">
                                                    EMD Amount
                                                </label>
                                            </div>
                                            <div>
                                                <asp:Label ID="lblEMDAmount_V" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <label class="form-label">
                                                    Received Date</label>
                                            </div>
                                            <div>
                                                <asp:Label ID="lblReceivedDate_V" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <label class="form-label">
                                                    Customer DeadLine Date</label>
                                            </div>
                                            <div>
                                                <asp:Label ID="lblClosingDate_V" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <label class="form-label">
                                                    Enquiry Location
                                                </label>
                                            </div>
                                            <div>
                                                <asp:Label ID="lblEnquiyLocation_V" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <label class="form-label">
                                                    Budgetary Enquiry</label>
                                            </div>
                                            <div>
                                                <asp:Label ID="lblBudgetaryEnquiry_V" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <label class="form-label">
                                                    Sales Resource</label>
                                            </div>
                                            <div>
                                                <asp:Label ID="lblSalesResource_V" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
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
                                                            Width="20px" Height="20px" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                        </asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnDelete_V" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            CommandName="DeleteAttachement">
                                                           <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-sm-12 text-center">
                                        <asp:LinkButton Text="Add Attachement" CssClass="btn btn-success add-emp" runat="server"
                                            ID="lbtnAddNewAttachments" OnClientClick="return ShowCloseViewPopUp();" />
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="modal" id="divView">
            <div class="modal-dialog">
                <div class="modal-content">
                    <asp:UpdatePanel ID="Updateview" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-header">
                                <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal">
                                    x</button>
                                <h4 class="modal-title ADD" style="color: #fff;" id="H1">
                                    Documnet
                                </h4>
                            </div>
                            <div class="modal-body">
                                <div class="inner-container">
                                    <asp:Image ID="imgViewDocs" ImageUrl="" Style="height: 50%; width: 50%;" runat="server"
                                        alt=""></asp:Image>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" data-dismiss="modal">
                                    Close</button>
                            </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
