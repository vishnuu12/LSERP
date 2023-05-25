<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="CustomerEnquiryProcessAlterPage.aspx.cs" Inherits="Pages_CustomerEnquiryProcessAlterPage" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="../Assets/css/datatables2.css" />
    <script type="text/javascript" src="../Assets/scripts/datatables2.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            $('#ContentPlaceHolder1_gvCustomerEnquiry').dataTable({
                "iDisplayLength": 10,
                "bJQueryUI": true,
                "bAutoWidth": false,
                "scrollX": true,
                "autoWidth": false
            });
        });

        $(document).ready(function () {
            $('#<%=txtReceivedDate.ClientID %>').datepicker({
                format: 'dd/mm/yyyy'
            });
            $('#<%=txtDeadLineDate.ClientID %>').datepicker({
                format: 'dd/mm/yyyy'
            });
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                $('.datepicker').datepicker({ format: 'dd/mm/yyyy' });
            }
        });
        function Hideenqform() {
            $('.output_section').show();
            $('.btnAddNew').show();
            $('.divInput').hide();
            $('.btnAddNew').focus();
        }
        function ShowViewdocsPopUp() {
            $('#mpeViewdocs').modal({
                backdrop: 'static'
            });
            return false;
        }


        function DateValidation() {
            if (new Date("2020/7/15") <= new Date("2020/7/11")) {
                alert('true');
            }
            else {
                alert('false');
            }
        }
        function ShowCloseViewPopUp() {
            $("#mpeView").modal("hide");
            ShowAddPopup();
            $('#<%=btnSaveAttachements.ClientID %>').css("display", "block");
            return false;
        }

        function ShowAddPopup() {
            $('#mpeAdd').modal({
                backdrop: 'static'
            });
            $('#<%=btnSaveAttachements.ClientID %>').css("display", "block");
            return false;
        }

        function ShowViewPopUp() {
            $('select').chosen();
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

        function ValidContactNumber(ele) {
            debugger;
            var len = $(ele).val().length;
            $(ele).removeClass("Invalid");
            $(ele).parent().find('.textboxInvalid').remove();
            if (parseInt(len) < 10 || parseInt(len) > 20) {
                $(ele).addClass("Invalid");
                $(ele).before('<span class="textboxInvalid" style="color: red;position: absolute;margin-left: -120%;margin-top: 10%;font-weight: bold;">Invalid Number</span>');
            }
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

        function SaveEnquiryHeaderDetails() {
            jQuery.ajax({
                type: "GET",
                url: "POAndInwardStatusReport.aspx/GetSupplierPODetailsBySPOID", //It ca
                contentType: "application/json; charset=utf-8",
                dataType: "JSON",
                data: {
                    SPOID: SPOID,
                },
                success: function (data) {
                    if (data.d != "") {
                        var d = JSON.parse(data.d);
                    }
                },
                error: function (data) {
                }
            });
        }

        function script2() {
            window.open("CustomerEnquiryView.aspx");
        }


    </script>
    <style type="text/css">
        .modal-content {
            width: 137% !important;
        }

        table#ContentPlaceHolder1_gvCustomerEnquiry td {
            color: #000;
            font-weight: bold;
            font-size: smaller;
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
                                        <img src="../Assets/images/pages.png" alt="" width="16px" />
                                    </span>
                                    <h3 class="page-title-head d-inline-block">Enquiry Process</h3>
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
                    <%--<asp:PostBackTrigger ControlID="btndownloaddocs" />--%>
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" class="btnAddNew" runat="server">
                            <div class="ip-div text-center input_section" id="div1" runat="server">
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Enquiry Number</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlEnquiryNumber" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEnquiryNumber_SelectIndexChanged"
                                            CssClass="form-control" Width="70%" ToolTip="Select Enquiry Number">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divInput" class="divInput" runat="server">
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
                                            <label class="form-label mandatorylbl">
                                                Customer Enquiry Number</label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtCustomerEnquiryNumber" runat="server" CssClass="form-control mandatoryfield"
                                                ToolTip="Enter Customer Enquiry Number" autocomplete="nope" TabIndex="1" placeholder="Enter Customer Enquiry Number">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Customer Name</label>
                                        </div>
                                        <div>
                                            <asp:DropDownList ID="ddlProspect" runat="server" TabIndex="2" ToolTip="Select Prospect"
                                                OnSelectedIndexChanged="ddlProspect_SelectedIndexChanged" AutoPostBack="true"
                                                CssClass="form-control mandatoryfield">
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
                                            <label class="form-label ">
                                                Contact Person</label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtContactPerson" runat="server" placeholder="Enter Contact Person"
                                                CssClass="form-control" MaxLength="100" TabIndex="3" autocomplete="nope"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Email ID
                                            </label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtEmail" runat="server" TabIndex="4" placeholder="Enter Email"
                                                CssClass="form-control mandatoryfield" MaxLength="100" onkeypress="return validateEmail(this);"
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
                                            <label class="form-label mandatorylbl">
                                                Contact Number</label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtContactNumber" runat="server" TabIndex="5" placeholder="Enter Contact Number"
                                                onkeypress="return validatePhoneNo(this);" onblur="ValidContactNumber(this);" ToolTip="Enter Contact Number"
                                                CssClass="form-control mandatoryfield" MaxLength="15" autocomplete="nope"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Project Description</label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtProjectDescription" runat="server" TabIndex="6" placeholder="Enter Project Description"
                                                ToolTip="Project Description" TextMode="MultiLine" CssClass="form-control mandatoryfield"
                                                MaxLength="300" autocomplete="nope"></asp:TextBox>
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
                                                Alternate Contact Number</label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtalternatenumber" runat="server" TabIndex="5" placeholder="Enter Contact Number"
                                                onkeypress="return validatePhoneNo(this);" onblur="ValidContactNumber(this);" ToolTip="Enter Contact Number"
                                                CssClass="form-control" MaxLength="15" autocomplete="nope"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Enquiry Type</label>
                                        </div>
                                        <div>
                                            <asp:DropDownList ID="ddlEnquiryType" runat="server" TabIndex="8" ToolTip="Select Enquiry Type"
                                                CssClass="form-control mandatoryfield">
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
                                            <label class="form-label mandatorylbl">
                                                Received Date</label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtReceivedDate" runat="server" CssClass="form-control datepicker mandatoryfield"
                                                TabIndex="10" autocomplete="nope" PlaceHolder="Received Date" data-date-start-date="-1w"
                                                data-date-end-date="0d"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Customer DeadLine Date</label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtDeadLineDate" runat="server" TabIndex="9" placeholder="Enter Deadline Date"
                                                ToolTip="Enter Deadline Date" CssClass="form-control datepicker mandatoryfield"
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
                                            <label class="form-label mandatorylbl">
                                                Offer Submission Date</label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtOfferSubmissionDate" runat="server" TabIndex="9" placeholder="Enter Offer SubMission Date"
                                                ToolTip="Enter Deadline Date" CssClass="form-control datepicker mandatoryfield"
                                                autocomplete="nope"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Enquiry Location</label>
                                        </div>
                                        <div>
                                            <asp:DropDownList ID="ddlEnquiryLocation" runat="server" TabIndex="10" ToolTip="Select Enquiry Location"
                                                CssClass="form-control mandatoryfield">
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
                                            <label class="form-label mandatorylbl">
                                                How Sourced</label>
                                        </div>
                                        <div>
                                            <asp:DropDownList ID="ddlSource" runat="server" ToolTip="Select Source" TabIndex="9"
                                                AutoPostBack="false" CssClass="form-control mandatoryfield">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Sales Resource</label>
                                        </div>
                                        <div>
                                            <asp:DropDownList ID="ddlSalesResource" runat="server" TabIndex="10" ToolTip="Select Sales Resource"
                                                CssClass="form-control mandatoryfield">
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
                                    <div class="col-sm-4" runat="server">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Rfp Group Name</label>
                                        </div>
                                        <div>
                                            <asp:DropDownList ID="ddlRfpGroupID" runat="server" TabIndex="11" ToolTip="Select Rfp GroupID"
                                                CssClass="form-control mandatoryfield">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Product Name</label>
                                        </div>
                                        <div>
                                            <asp:DropDownList ID="ddlProductName" runat="server" TabIndex="11" ToolTip="Select Product Name"
                                                CssClass="form-control mandatoryfield">
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
                                            <label class="form-label mandatorylbl">
                                                Not Interested</label>
                                        </div>
                                        <div>
                                            <asp:RadioButtonList ID="rbl_notinterested" runat="server" CssClass="radio radio-success"
                                                RepeatDirection="Horizontal" RepeatLayout="Flow" TabIndex="11">
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Budgetary offer</label>
                                        </div>
                                        <div>
                                            <asp:RadioButtonList ID="rbl_budgetary" runat="server" CssClass="radio radio-success"
                                                RepeatDirection="Horizontal" RepeatLayout="Flow" TabIndex="11">
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                            </asp:RadioButtonList>
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
                                    <div class="col-sm-4" runat="server">
                                        <div class="text-left">
                                            <label class="form-label">
                                                Item Description</label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtItemDescription" runat="server" TabIndex="9" placeholder="Enter Item Description"
                                                TextMode="MultiLine" ToolTip="Enter Amount" CssClass="form-control" autocomplete="nope"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-20">
                                    <asp:LinkButton ID="btnSave" runat="server" Text="Save" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divInput','noprogress');"
                                        OnClick="btnSave_Click" CssClass="btn btn-cons btn-save AlignTop"></asp:LinkButton>
                                    <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" OnClientClick="showLoader();"
                                        OnClick="btncancel_Click" CausesValidation="False" CssClass="btn btn-cons btn-danger AlignTop btnCancel"></asp:LinkButton>
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
                                                    <asp:Label ID="lbltitle" runat="server" Text="Customer Enquiry Details"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="ove flow-x: auto;">
                                            <asp:GridView ID="gvCustomerEnquiry" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" OnRowCommand="gvCustomerEnquiry_OnRowCommand"
                                                OnRowDataBound="gvCustomerEnquiry_OnRowDataBound" CssClass="table table-hover table-bordered medium tablestatesave"
                                                DataKeyNames="EnquiryID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Enquiry Number" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="200px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCustomerEnquiryNumber" runat="server" Text='<%# Eval("CustomerEnquiryNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Customer Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProspectName" runat="server" Text='<%# Eval("ProspectName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Project Description" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProjectDescription" runat="server" Text='<%# Eval("ProjectDescription")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Contact Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblContactName" runat="server" Text='<%# Eval("ConTactPerson")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Received Date" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
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
                                                    <asp:TemplateField HeaderText="Offer Submission Date" ItemStyle-HorizontalAlign="left"
                                                        HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOfferSubmissionDate" runat="server" Text='<%# Eval("OfferSubmissionDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnView" runat="server" OnClientClick="showLoader();" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="View"><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEdit" runat="server" OnClientClick="showLoader();" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="EditEnquiry"><img src="../Assets/images/edit-ec.png"/></asp:LinkButton>
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
        <div class="modal" id="mpeView" style="overflow-y: scroll;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <asp:UpdatePanel ID="upView" runat="server">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnSaveAttachements" />
                            <asp:PostBackTrigger ControlID="gvAttachments" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="modal-header">
                                <h4 class="modal-title ADD">Enquiry Details</h4>
                                <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                    ×</button>
                            </div>
                            <div class="modal-body" style="padding: 0px;">
                                <div id="docdiv" class="docdiv">
                                    <div class="inner-container" style="text-transform: uppercase;">
                                        <div id="enquirydetailsdiv" class="enquirydetailsdiv" runat="server">
                                            <div class="col-sm-12">
                                                <div class="col-sm-4">
                                                    <div>
                                                        <label class="form-label">
                                                            Enquiry Number</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lblEnquiryNumber_V" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="form-label">
                                                            Customer Enquiry Number</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lblCustomerEnquiryNumber_V" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="form-label">
                                                            Customer Name</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lblProspectName_V" runat="server" />
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
                                                        <asp:Label ID="lblContactPerson_V" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="form-label">
                                                            Contact Number</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lblContactNumber_V" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="form-label">
                                                            Email ID</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lblEmailID_V" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="form-label">
                                                            Project Description</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lblProjectDescription_V" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="form-label">
                                                            Alternate Contact Number</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lblalternatenumber" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="form-label">
                                                            Enquiry Type Name</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lblEnquiryTypeName_V" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="form-label">
                                                            EMD Amount</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lblEMDAmount_V" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="form-label">
                                                            Received Date</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lblReceivedDate_V" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="form-label">
                                                            Customer DeadLine Date</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lblClosingDate_V" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="form-label">
                                                            Enquiry Location</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lblEnquiyLocation_V" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="form-label">
                                                            Rfp Group Name</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lblRfpGroup_V" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="form-label">
                                                            Sales Resource</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lblSalesResource_V" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="form-label">
                                                            Drawing With Offer</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lbldrawingoffer_V" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="form-label">
                                                            Not Interested</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lblnotinterested_V" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="form-label">
                                                            Budgetary Offer</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lblbudgetary" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="form-label">
                                                            Item description</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lblItemDescription" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="form-label">
                                                            Product Name</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lblProductName_V" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                </div>
                                            </div>
                                        </div>
                                        <ul class="nav nav-tabs lserptabs" style="display: inline-block; width: 100%; background-color: cadetblue; text-align: right; font-size: x-large; font-weight: bold; color: whitesmoke;">
                                            <li>Documents</li>
                                        </ul>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-3">
                                                <label class="form-label mandatorylbl">
                                                    Type Name
                                                </label>
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="ddlTypeName" runat="server" TabIndex="1" ToolTip="Select Attachement type"
                                                    CssClass="form-control mandatoryfield">
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
                                                <label class="form-label mandatorylbl">
                                                    Attachment Description
                                                </label>
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtDescription" runat="server" TabIndex="6" placeholder="Enter Attachement Description"
                                                    ToolTip="Enter Attachment Description" TextMode="MultiLine" CssClass="form-control mandatoryfield"
                                                    MaxLength="300" autocomplete="nope"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-3">
                                                <label class="form-label mandatorylbl">
                                                    Attachement
                                                </label>
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:FileUpload ID="fAttachment" runat="server" TabIndex="12" CssClass="form-control mandatoryfield Attachement"
                                                    onchange="DocValidation(this);" ClientIDMode="Static" Width="95%"></asp:FileUpload>

                                                <asp:Label ID="lblAttachementFileName" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-3">
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <asp:LinkButton Text="Submit" CssClass="btn btn-cons btn-save  AlignTop" ID="btnSaveAttachements"
                                                        runat="server" OnClientClick="return Mandatorycheck('docdiv');" OnClick="btnSaveAttchement_Click" />
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                            </div>
                                            <div class="col-sm-3">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <asp:GridView ID="gvAttachments" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                            OnRowCommand="gvAttachments_OnRowCommand" OnRowDataBound="gvAttachments_OnRowDataBound"
                                            DataKeyNames="AttachementID,FileName,EnquiryID">
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

                                                <asp:TemplateField HeaderText="File Upload Date" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCreatedDate_V" runat="server" Text='<%# Eval("CreatedDate")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFileName_V" runat="server" Visible="false" Text='<%# Eval("FileName")%>'></asp:Label>
                                                        <asp:ImageButton ID="imgbtnView" runat="server" ImageUrl="" CommandName="ViewDocs"
                                                            Width="20px" Height="20px" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'></asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnDelete_V" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            CommandName="DeleteAttachement">
                                                            <img src="../Assets/images/del-ec.png" alt="" /></asp:LinkButton>
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
    </div>
    <div class="modal" id="mpeViewdocs" style="height: 125%; top: -10%;">
        <div class="modal-dialog" style="height: 100%;">
            <div class="modal-content" style="height: 100%;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" Style="height: 100%;">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btndownloaddocs" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H2">
                                <div class="text-center">
                                    Documents
                                    <asp:Label ID="Label1" Text="" runat="server"></asp:Label>
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal">
                                x</button>
                        </div>
                        <div class="modal-body" style="height: 100%;">
                            <div class="inner-container" style="height: 100%;">
                                <div class="ip-div text-center" style="margin-bottom: 10px;">
                                    <asp:LinkButton Text="Download" CssClass="btn btn-save btn-cons AlignTop" ID="btndownloaddocs"
                                        OnClick="btndownloaddocs_Click" runat="server" />
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

