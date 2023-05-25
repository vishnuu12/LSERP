<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="CustomerEnquiryView.aspx.cs" Inherits="Pages_CustomerEnquiryView" %>


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
        function showDataTable() {
           <%-- $('#<%=gvCustomerEnquiry.ClientID %>').DataTable({
                'aoColumnDefs': [{
                    'bSortable': false,
                    'aTargets': [-1, -2] /* 1st one, start by the right */
                }]
            });--%>
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

        //        function ValidateControl(controlID) {

        //            var ClientID = 'ContentPlaceHolder1';

        //            var ControlID = control.split(',');

        //            $.each(ControlID, function (i) {
        //                var message = ControlID[i].split('/');
        //                $(ClientID + '_' + message[0]).notify(message[1], { arrowShow: true, position: 'r,r', autoHide: true });
        //            });
        //        }    

        function ValidateAttchements() {
            //debugger;
            //var msg = true;
            //$('.manadatary').each(function () {
            //    if ($(this).val() == '') msg = false;                    
            //    else if ($(this).prop('type') == 'select-one' && $(this).val() == 0) msg = false;
            //    if (msg == false) $(this).notify('Fields Required.', { arrowShow: true, position: 'r,r', autoHide: true });
            //});
            //if (msg == false) return false;
            <%--if ($('#<%=ddlTypeName.ClientID %>')[0].selectedIndex == 0) {
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
                var fileExtension = ['jpeg', 'jpg', 'png', 'doc', 'docx', 'xlsx', 'xls', 'pdf'];
                if ($.inArray($('#<%=fAttachment.ClientID %>').val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                    $('#<%=fAttachment.ClientID %>').notify('Invalid File.\n Please upload a File with extension: .doc ,.png,.docx ,.xlsx,.xls, .pdf , .jpg , .jpeg', { arrowShow: true, position: 'r,r', autoHide: true });
                    msg = false;
                }
            }
            if ($('#<%=hdnAttachementFlag.ClientID %>').val() == "V") {
                if (msg == true) {
                    return true;
                }
                else {
                    return false;
                }

            }
            else if ($('#<%=hdnAttachementFlag.ClientID %>').val() == "A") {--%>
            //if (msg == true) {
            //    $("#mpeAdd").modal("hide");
            //    return false;
            //}
            //else {
            //    return false;
            //}
            //}
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

        $(document).ready(function () {

            BindDate();

        });

        function BindDate() {
            // $('#gvStockMonitorDetails tfoot').empty();
            // $('.dataTables_scrollFoot table tfoot').empty();
            // $('#gvStockMonitorDetails').DataTable().destroy();
            //  $('#gvStockMonitorDetails tfoot').append("<tr></tr>");
            $('#gvCustomerEnquiry').DataTable({
                //initComplete: function () {
                //    //    this.api().columns().every(function (i, k) {
                //    //    });
                //    //    $('.table thead th select').chosen();
                //},
                "retrieve": true,
                "processing": true,
                "serverSide": true,
                // "stateSave": true,
                "ordering": false,
                "bSort": false,
                "bDeferRender": true,
                "pageLength": 50,
                "bStateSave": true,
                "fnStateSave": function (oSettings, oData) {
                    localStorage.setItem('DataTables_' + window.location.pathname, JSON.stringify(oData));
                },
                "fnStateLoad": function (oSettings) {
                    var data = localStorage.getItem('DataTables_' + window.location.pathname);
                    return JSON.parse(data);
                },
                "stateSaveParams": function (settings, data) {
                    delete data.order;
                    data.length = 50;
                    data.order = [];
                },
                "ajax": ({
                    type: "GET",
                    url: "CustomerEnquiryView.aspx/GetData", //It calls our web method                  
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",

                    "data": function (d) {
                        return d;
                    },

                    "dataSrc": function (json) {
                        json.draw = json.d.draw;
                        json.recordsTotal = json.d.recordsTotal;
                        json.recordsFiltered = json.d.recordsFiltered;
                        json.Ldata = json.d.Ldata;
                        var return_data = json;
                        return return_data.Ldata;
                    },

                }),
                "columns": [

                    { 'data': 'CustomerEnquiryNumber' },
                    { 'data': 'ProspectName' },
                    { 'data': 'ProjectDescription' },
                    { 'data': 'ConTactPerson' },
                    { 'data': 'ReceivedDate' },
                    { 'data': 'EnquiryLocation' },
                    { 'data': 'OfferSubmissionDate' },
                    { 'data': 'CreatedEmployeeName' },
                    { 'data': 'CreatedDate' },
                    {
                        'data': null,
                        "mRender": function (data, type, full) {
                            return `<a href="#" onclick="return OpenCustomerEnquiry('${data.EnquiryID}')";><img src="../Assets/images/view.png" /></a>`

                        }
                    },
                    {
                        'data': null,
                        "mRender": function (data, type, full) {
                            return `<a href="#" onclick="return EditCustomerEnquiry('${data.EnquiryID}')";><img src="../Assets/images/edit-ec.png" /></a>`

                        }
                    }

                ],
            });
        }

        function OpenCustomerEnquiry(EnquiryID) {
                __doPostBack('OpenCustomerEnquiry', EnquiryID);
           
            return false;
        }
        function EditCustomerEnquiry(EnquiryID) {
            __doPostBack('EditCustomerEnquiry', EnquiryID);

            return false;
        }

    </script>
    <style type="text/css">
        .modal-content {
            width: 137% !important;
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
                                    <h3 class="page-title-head d-inline-block">Enquiry Process Report</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Leads & Enquiry</a></li>
                                <li class="active breadcrumb-item" aria-current="page">Enquiry Process Report</li>
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
                        <div id="divAdd" class="btnAddNew" runat="server">
<%--                            <div class="text-center">
                                <asp:LinkButton ID="btnAddNew" runat="server" Text="Add New Enquiry" OnClick="btnAddNew_Click"
                                    CausesValidation="false" CssClass="btn btn-success add-emp"></asp:LinkButton>
                            </div>--%>
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
                                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEmail"
                                            ErrorMessage="Please enter valid email" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"> </asp:RegularExpressionValidator>--%>
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
                                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtContactNumber" ForeColor="Red"
                                            ErrorMessage="Please enter valid Mobile number!" ValidationExpression="^([0|\+[0-9]{1,5})?([7-9][0-9]{9})$"></asp:RegularExpressionValidator>--%>
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
                                    <%--<div class="col-sm-4">
                                          <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Drawing with offer</label>
                                        </div>
                                        <div>
                                            <asp:RadioButtonList ID="rbl_drawingoffer" runat="server" CssClass="radio radio-success"
                                                RepeatDirection="Horizontal" RepeatLayout="Flow" TabIndex="11">
                                                <asp:ListItem Value="1">Yes</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>--%>
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
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <table class="table table-hover table-bordered medium uniquedatatable" style="width: 100%;"
                                                id="gvCustomerEnquiry">
                                                <thead>
                                                    <tr>
                                                        <th>Enquiry Number</th>
                                                        <th>Customer Name</th>
                                                        <th>Project Description</th>
                                                        <th>Contact Name</th>
                                                        <th>Received Date</th>
                                                        <th>Enquiry Location</th>
                                                        <th>Offer Submission Date</th>
                                                        <th>Created By</th>
                                                        <th>Created On</th>
                                                        <th>View</th>
                                                        <th>Edit</th>
                                                    </tr>
                                                </thead>
                                                <%-- <tfoot>
                                                </tfoot>--%>
                                            </table>
                                        </div>
                                            <asp:HiddenField ID="hdnLseNumberMaxID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnEnquiryID" runat="server" Value="0" />
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
                                                <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator6" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.doc|.docx|.pdf|.xls|.dwg|.png)$"
                                                ControlToValidate="fAttachment" runat="server" ForeColor="Red" ErrorMessage="Please select a valid .doc|.docx|.pdf|.xls|.dwg|.png file."
                                                Display="Dynamic" />--%>
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
                                                           <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
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
