<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="Design.aspx.cs" Inherits="Pages_Design" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function ShowAddPopUp() {
            $('#mpeAdd').modal({
                backdrop: 'static'
            });
            return false;
        }

        //        function ShowDataTableDesignDetails() {
        //            $('#<%=gvDesignDetails.ClientID %>').DataTable();
        //        }
        //        function ShowDataTableRevisedList() {
        //            $('#<%=gvRevisedList.ClientID %>').DataTable();
        //        }

        function ShowViewPopUp() {
            $('#mpeView').modal({
                backdrop: 'static'
            });
            return false;
        }

        function ValidateAttchements() {

            var msg = true;
            if ($('#<%=fAttachment.ClientID %>').val() == "") {
                $('#<%=fAttachment.ClientID %>').notify('Please Upload File', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }

            if ($('#<%=txtDrawingName.ClientID %>').val() == "") {
                $('#<%=txtDrawingName.ClientID %>').notify('Fields Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }
            if ($('#<%=txtDesignNumber.ClientID %>').val() == "") {
                $('#<%=txtDesignNumber.ClientID %>').notify('Fields Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }
            else if ($('#<%=fAttachment.ClientID %>').val() != "") {
                var fileExtension = ['jpeg', 'jpg', 'png', 'pdf'];
                if ($.inArray($('#<%=fAttachment.ClientID %>').val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                    $('#<%=fAttachment.ClientID %>').notify('Invalid File.\n Please upload a File with extension: .jpg , .jpeg , .png , .pdf', { arrowShow: true, position: 'r,r', autoHide: true });
                    msg = false;
                }
            }


            if (msg == true) {
                return true;
            }
            else {
                return false;
            }
        }

        function deleteConfirm(AttacheID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Attachement will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deletegvrowAttachID', AttacheID);
            });
            return false;
        }

        function checkAll(ele) {
            debugger;
            if ($(ele).is(":checked")) {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', true);
            }
            else {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', false);
            }
        }

        function ValidateItem() {
            if ($('#ContentPlaceHolder1_gvDesignDetails').find("[type=checkbox]:checked").not('#ContentPlaceHolder1_gvDesignDetails_chkall').length > 0) {
                return true;
            }
            else {
                ErrorMessage('Error', 'No Item Selected');
                return false;
            }
        }

    </script>

    <style type="text/css">
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
                                    <h3 class="page-title-head d-inline-block">Design</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Design</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Design Upload</li>
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
            <asp:UpdatePanel ID="upQuote" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:PostBackTrigger ControlID="gvDesignDetails" />
                    <%--  <asp:PostBackTrigger ControlID="ddlEnquiryNumber" />--%>
                    <asp:PostBackTrigger ControlID="gvRevisedList" />
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
                        <div id="divAdd" runat="server">
                            <div class="ip-div text-center">
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Customer Name</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="true" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlCustomerName_SelectIndexChanged" Width="70%" ToolTip="Select Enquiry Number">
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
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <%--  <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Item Name</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlItemName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlItemName_SelectIndexChanged"
                                            CssClass="form-control" Width="70%" OnChange="showLoader();" ToolTip="Select Item Name">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>--%>
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                                    <asp:Label ID="lbltitle" runat="server"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>
                                        <div id="divInfo" runat="server">
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-3">
                                                    <label class="form-label">
                                                        Customer Order Number</label>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:Label ID="lblCustomerOrderNumber" Text="" runat="server" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-3">
                                                    <label class="form-label">
                                                        Client Name</label>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:Label ID="lblClientName" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-3">
                                                    <label class="form-label">
                                                        Project Name</label>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:Label ID="lblProjectName" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-3">
                                                    <label class="form-label">
                                                        Last Drawing Submitted On</label>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:Label ID="lblLastDrawingSubmittedOn" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-3">
                                                    <label class="form-label">
                                                        Sales Agent</label>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:Label ID="lblSalesAgent" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-3">
                                                    <label class="form-label">
                                                        Offer ID</label>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:Label ID="lblOfferID" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="divGrid" runat="server">
                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvDesignDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium pagingfalse"
                                                    OnRowCommand="gvDesignDetails_OnRowCommand" OnRowDataBound="gvDesignDetails_RowDataBound"
                                                    HeaderStyle-HorizontalAlign="Center" DataKeyNames="FileName,EDID,Version,AttachementID,DDID,TagNo,ApprovalStatus,SharedWithHOD">
                                                    <Columns>
                                                        <%--   <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="" ControlStyle-CssClass="nosort" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkall" runat="server" onclick="return checkAll(this);" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkitems" runat="server"
                                                                    AutoPostBack="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Revision Number" ItemStyle-CssClass="text-center"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVersionNumber" runat="server" Text='<%# Eval("Version")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Name" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Submitted On" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSubmittedOn" runat="server" Text='<%# Eval("CreatedOn")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Shared With Customer" ItemStyle-CssClass="text-center"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSharedWithCustomer" runat="server" Text='<%# Eval("SharedWithCustomer")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Customer Approval Status" ItemStyle-CssClass="text-center"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCustomerApprovalStatus" runat="server" Text='<%# Eval("CustomerApprovalStatus")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Drawing Attached" ItemStyle-CssClass="text-center"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDrawingAttached" runat="server" Text='<%# Eval("DrawingAttached")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Shared With Sales" ItemStyle-CssClass="text-center"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSharedWithSales" runat="server" Text='<%# Eval("SharedWithSales")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="HOD Status" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSharedWithHODStatus" runat="server" Text='<%# Eval("SharedWithHODStatus")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Drawing Name" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldrawingname" runat="server" Text='<%# Eval("DrawingName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="View" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgbtnView" runat="server" ImageUrl="" Width="20px" Height="20px"
                                                                    CommandName="ViewDocs" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'></asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Attach Drawings" ItemStyle-CssClass="text-center"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnNew" runat="server" Text="Add" CommandName="AddDocs" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                                <img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <%-- <asp:TemplateField HeaderText="Review Drawing" ItemStyle-CssClass="text-center"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnReiewDrawing" runat="server" Text="Review Drawing"
                                                                    CommandName="ReviewDrawing"
                                                                    CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="col-sm-4">
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:LinkButton ID="btnSaveAndShare" CssClass="btn btn-success" runat="server"
                                                        Text="Shared With HOD" OnClientClick="return ValidateItem();" OnClick="btnSaveAndShare_Click"></asp:LinkButton>
                                                </div>
                                                <div class="col-sm-4">
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                </div>
                                                <div class="col-sm-4">
                                                    <label class="form-label">
                                                        Revised List</label>
                                                </div>
                                                <div class="col-sm-4">
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <asp:GridView ID="gvRevisedList" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                    OnRowCommand="gvRevisedList_OnRowCommand" HeaderStyle-HorizontalAlign="Center"
                                                    OnRowDataBound="gvRevisedList_RowDataBound" DataKeyNames="FileName,EDID">
                                                    <Columns>
                                                        <%--   <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Revision Number" ItemStyle-CssClass="text-center"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVersionNumber" runat="server" Text='<%# Eval("Version")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="ItemName" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="SubmittedOn" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSubmittedOn" runat="server" Text='<%# Eval("CreatedOn")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Shared With Customer" ItemStyle-CssClass="text-center"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSharedWithCustomer" runat="server" Text='<%# Eval("SharedWithCustomer")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Customer Approval Status" ItemStyle-CssClass="text-center"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCustomerApprovalStatus" runat="server" Text='<%# Eval("CustomerApprovalStatus")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Shared With Sales" ItemStyle-CssClass="text-center"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSharedWithSales" runat="server" Text='<%# Eval("SharedWithSales")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Drawing Name" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldrawingname" runat="server" Text='<%# Eval("DrawingName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="View" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgbtnView" runat="server" ImageUrl="" Width="20px" Height="20px"
                                                                    CommandName="ViewDocs" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'></asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <asp:HiddenField ID="hdnDDID" Value="0" runat="server" />
                                            <div class="col-sm-12">
                                                <div class="col-sm-4">
                                                </div>
                                                <%--OnClick="lbtnNewDrawing_Click"--%>
                                                <div class="col-sm-4" visible="false" runat="server">
                                                    <asp:LinkButton ID="lbtnNewDrawing" CssClass="btn btn-success add-emp" runat="server"
                                                        Text="Add New" OnClientClick="return ShowAddPopUp();"></asp:LinkButton>
                                                </div>
                                                <div class="col-sm-4">
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
    <div class="modal" id="mpeAdd">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="Updateview" runat="server" UpdateMode="Always">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSaveDrawing" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H1">
                                <div class="text-center">
                                    Add New Drawings/
                                    <asp:Label ID="lblEnquiryNumber_A" Style="color: brown; font-size: small;" Text=""
                                        runat="server"></asp:Label>
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal">
                                x</button>
                        </div>
                        <div class="modal-body">
                            <div class="inner-container">
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-3">
                                        <label class="form-label">
                                            Revision Number</label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblVersionNumber_A" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-3">
                                        <label class="form-label mandatorylbl">
                                            Drawing Name/No</label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtDrawingName" runat="server" Width="95%" CssClass="form-control mandatoryfield"
                                            ToolTip="Enter Drawing Name" AutoComplete="off" placeholder="Enter Drawing Name">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-3">
                                        <label class="form-label mandatorylbl">
                                            Design Number</label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtDesignNumber" runat="server" Width="95%" CssClass="form-control mandatoryfield"
                                            ToolTip="Enter Design Number" AutoComplete="off" placeholder="Enter Design Number">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-3">
                                        <label class="form-label">
                                            Design Code</label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtDesignCode" runat="server" Width="95%" CssClass="form-control"
                                            ToolTip="Enter Design Code" AutoComplete="off" placeholder="Enter Design Code">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10" id="divtagno" runat="server" visible="false">
                                    <div class="col-sm-3">
                                        <label class="form-label">
                                            Tag Number</label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtTagNumber" runat="server" Width="95%" CssClass="form-control"
                                            ToolTip="Enter Tag Number" AutoComplete="off" placeholder="Enter Tag Number">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-3">
                                        <label class="form-label">
                                            Over All Length</label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtOverAllLength" runat="server" Width="95%" CssClass="form-control"
                                            ToolTip="Enter Over All Length" AutoComplete="off" placeholder="Enter Over All Length">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-3">
                                        <label class="form-label mandatorylbl">
                                            Drawing File</label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:FileUpload ID="fAttachment" onchange="DocValidation(this);" runat="server" class="form-control MandatoryFields"
                                            ClientIDMode="Static" Width="95%" />
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-3">
                                        <label class="form-label">
                                            List Of Deviation</label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtListOfDeviation" runat="server" Width="95%" CssClass="form-control"
                                            ToolTip="Enter Over All Length" AutoComplete="off" placeholder="Enter Over All Length">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-3">
                                        <label class="form-label">
                                            Attached Deviation</label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:FileUpload ID="fDeviationAttachement" runat="server" class="form-control" ClientIDMode="Static"
                                            Width="95%" />
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-3">
                                        <label class="form-label">
                                            Add Remarks</label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtRemarks_A" runat="server" TextMode="MultiLine" Rows="3" Width="95%"
                                            CssClass="form-control" placeHolder="Enter Remarks" AutoComplete="off" ToolTip="Enter remarks"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-3">
                                        <label class="form-label">
                                            BOM Cost Approve</label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:CheckBox ID="chkBOMCostApprove" Checked="true" runat="server" />
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                </div>
                                <%--   <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-3">
                                        <label class="form-label mandatorylbl">
                                            Type Name
                                        </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlTypeName" runat="server" TabIndex="1" ToolTip="Select Attachement type"
                                            CssClass="form-control mandatoryfield">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Enquiry Related"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="Financial Documents"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="Others"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-3">
                                        <label class="form-label mandatorylbl">
                                            Attachment Description
                                        </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtDescription" runat="server" TabIndex="6" placeholder="Enter Attachement Description"
                                            ToolTip="Enter Attachment Description" TextMode="MultiLine" CssClass="form-control mandatoryfield"
                                            MaxLength="300" autocomplete="nope"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-3">
                                        <label class="form-label mandatorylbl">
                                            Attachement
                                        </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:FileUpload ID="fMultiAttach" runat="server" TabIndex="12" onchange="DocValidation(this);" CssClass="form-control mandatoryfield"
                                            ClientIDMode="Static" Width="95%"></asp:FileUpload>
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                </div>--%>
                                <div class="col-sm-12 text-center p-t-10">
                                    <asp:LinkButton ID="btnSaveDrawing" runat="server" Text="Submit" OnClientClick="return ValidateAttchements();"
                                        OnClick="btnSaveDrawing_Click" CssClass="btn btn-cons btn-save  AlignTop" />
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-12 p-t-10">
                                        <%--  <asp:GridView ID="gvAttachments" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                            OnRowCommand="gvAttachments_OnRowCommand" DataKeyNames="AttachementID">
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
                                                <asp:TemplateField HeaderText="View" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFileName_V" runat="server" Visible="false" Text='<%# Eval("FileName")%>'></asp:Label>
                                                        <asp:ImageButton ID="imgbtnView" runat="server" ImageUrl="../Assets/images/view.png"
                                                            CommandName="ViewDocs" Width="20px" Height="20px" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                        </asp:ImageButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnDelete" runat="server" OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("AttachementID")) %>'>
                                                                                    <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="col-sm-12 text-center">
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
    <%--   <div class="modal" id="mpeView">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSaveDrawing" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H2">
                                <div class="text-center">
                                    Drawings
                                    <asp:Label ID="Label1" Text="" runat="server"></asp:Label>
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal">
                                x</button>
                        </div>
                        <div class="modal-body">
                            <div class="inner-container">
                                <div class="col-sm-12">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-8">
                                        <asp:Image ID="imgDocs" ImageUrl="" Height="100%" Width="100%" runat="server" />
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                        </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:HiddenField ID="HiddenField1" runat="server" Value="" />
            </div>
        </div>
    </div>--%>
    <div class="modal" id="mpeView">
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
