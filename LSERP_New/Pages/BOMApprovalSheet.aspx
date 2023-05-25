<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="BOMApprovalSheet.aspx.cs" Inherits="Pages_BOMApprovalSheet" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function ShowViewPopUP() {
            $('#mpeView').modal({
                backdrop: 'static'
            });
        }

        function ShowEnquiryAttachementsPopUp() {
            $('#mpeEnquiryAttachements').modal({
                backdrop: 'static'
            });
        }

        function ShowViewdocsPopUp() {
            $('#mpeViewdocs').modal({
                backdrop: 'static'
            });
        }

        function UpdateBOMStatus() {
            swal({
                title: "No Further Edit Once Approved.",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Save it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('UpdateBOMStatus', null);
            });
            return false;
        }

        function CheckConfirm() {
            swal({
                title: "Are you Sure to Change The Costing Details.",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Change it!",
                closeOnConfirm: false
            }, function () {
                __doPostBack('updatebomreview', null);
            });
            return false;
        }

        function ValidateAllItemChecked() {
            var msg = false;
            if ($('#ContentPlaceHolder1_gvViewCosting').find('[type="checkbox"]:checked').not('#ContentPlaceHolder1_gvViewCosting_chkall').length > 0) {
                msg = true;
            }
            else {
                ErrorMessage('Error', 'All Item Should Have Checked');
                msg = false;
            }

            if (msg) {
                UpdateBOMStatus();
            }
            else {
                return false;
            }
        }

        function btnReject_ClientClick() {
            var msg = false;
            $('#<%=gvViewCosting.ClientID %>').find('[type="checkbox"]').each(function () {
                if ($(this).is(":checked")) {
                    $(this).closest('tr').find('[type="text"]').addClass("mandatoryfield");
                    $(this).closest('tr').find('[type="text"]').before('<span class="textboxmandatory" style="color: red;">*</span>');

                    var Remarks = $(this).closest('tr').find('[type="text"]').val();
                    var id = $(this).closest('tr').find('[type="text"]').attr('id');
                    if (Remarks != '')
                        msg = true;
                    else {
                        $('#' + id).notify('Field Required.', { arrowShow: true, position: 't,r', autoHide: true });
                        msg = 'remarks';
                        return false;
                    }
                }
                else {
                    $(this).closest('tr').find('[type="text"]').removeClass("mandatoryfield");
                    $(this).closest('tr').find('.textboxmandatory').remove();
                }
            });

            if (msg == true) {
                return true;
            }
            else if (msg == false) {
                ErrorMessage('Error', 'Please Select The Item');
                return false;
            }
            else if (msg == 'remarks') {
                return false;
            }
        }

        function ViewFileName(RowIndex) {
            __doPostBack("ViewDrawing", RowIndex);
        }

        function checkAllItem(ele) {
            debugger;
            if ($(ele).is(":checked")) {
                $('#ContentPlaceHolder1_gvViewCosting').find('.checkapprove').find('[type="checkbox"]').prop('checked', true);
            }
            else {
                $('#ContentPlaceHolder1_gvViewCosting').find('.checkapprove').find('[type="checkbox"]').prop('checked', false);
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
                                    <h3 class="page-title-head d-inline-block">BOM Approval Sheet</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Design</a></li>
                                <li class="active breadcrumb-item" aria-current="page">BOM Approval Sheet</li>
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
                    <asp:PostBackTrigger ControlID="gvAttachments" />
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
                                            <asp:ListItem Value="1">APPROVED</asp:ListItem>
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
                        <div id="divInput" runat="server" visible="false">
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
                                        <div class="row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                                    <asp:Label ID="lbltitle" runat="server"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>
                                        <%-- <div class="col-sm-12 p-t-10 text-center">
                                            <label class="form-label" style="color: Blue;">
                                                View Costing
                                            </label>
                                        </div>--%>
                                        <div class="col-sm-12 text-center">
                                            <asp:LinkButton ID="btnReviewBom" CssClass="btn btn-success" runat="server" Text="Bom Review" OnClientClick="return CheckConfirm();"></asp:LinkButton>
                                            <asp:LinkButton ID="btnApprove" CssClass="btn btn-success" runat="server" Text="Approve"
                                                OnClientClick="return ValidateAllItemChecked();"></asp:LinkButton>
                                            <asp:LinkButton ID="btnReject" CssClass="btn btn-success" runat="server" Text="Reject"
                                                OnClientClick="return btnReject_ClientClick();" OnClick="btnReject_Click"></asp:LinkButton>
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvViewCosting" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium pagingfalse"
                                                OnRowCommand="gvViewCosting_OnRowCommand" OnRowDataBound="gvViewCosting_OnRowDataBound"
                                                HeaderStyle-HorizontalAlign="Center" DataKeyNames="DDID,FileName">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkall" runat="server" onclick="checkAllItem(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkitems" runat="server" CssClass="checkapprove" AutoPostBack="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Drawing Status" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDrawingStatus" runat="server" Text='<%# Eval("DrawingStatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="View Costing" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnView" runat="server" CommandArgument='<%# Eval("DDID")%>'
                                                                CommandName="ViewCosting"><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View Enq Attachements" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnViewEnqAttch" runat="server" CommandArgument='<%# Eval("DDID")%>'
                                                                CommandName="ViewEnqAttachements"><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Staff" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStaff" runat="server" Text='<%# Eval("DesignerName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bom Completed Date" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBomCompletedDate" runat="server" Text='<%# Eval("DesignSharedDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ItemName" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Size" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemSize" runat="server" Text='<%# Eval("ItemSize")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cost Estimated Status" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCostEstimationStatus" runat="server" Text='<%# Eval("CostEstimated")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--CommandName="ViewDeviationFile"--%>
                                                    <asp:TemplateField HeaderText="Drawing Number" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDeviationFile" runat="server"
                                                                OnClientClick='<%# string.Format("return ViewFileName({0});",((GridViewRow) Container).RowIndex) %>'>
                                                                <asp:Label ID="lblDrawingNumber" runat="server" Text='<%# Eval("DrawingName")%>'></asp:Label>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
													<asp:TemplateField HeaderText="Tag Number/Item/Material Code" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
														<ItemTemplate>
															<asp:Label ID="lblItemTagNumber" runat="server" Text='<%# Eval("ItemTagNo")%>'></asp:Label>
														</ItemTemplate>
													</asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Design Number" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDesignNumber" runat="server" Text='<%# Eval("DesignNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit Bom Cost" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBOMCost" runat="server" Text='<%# Eval("UnitBomCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Addtional Part Cost" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAddtionalPartCost" runat="server" Text='<%# Eval("AdditionalPartBomCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="FIM Cost" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFIMCost" runat="server" Text='<%# Eval("IssuePartBomCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRemarks" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Dead Inventory Amount" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDeadInventoryAmount" runat="server" Text='<%# Eval("DeadInventoryAmount")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Dead Inventory Remarks" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDeadInventoryRemarks" runat="server" Text='<%# Eval("DeadInventoryRemarks")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Cost" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalCost" runat="server" Text='<%# Eval("TotalCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Review Bom" ControlStyle-CssClass="nosort" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkreviewbomitems" runat="server" AutoPostBack="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hdnBOMId" Value="0" runat="server" />
                    <asp:HiddenField ID="hdnBomreview" Value="0" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal" id="mpeView">
        <div class="modal-dialog" style="max-width: 95%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H2">
                                <div class="text-center">
                                    <asp:Label ID="Label1" Text="" runat="server"></asp:Label>
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal">
                                x</button>
                        </div>
                        <div class="modal-body">
                            <div class="inner-container">
                                <div class="col-sm-12 p-t-10 text-center">
                                    <label class="form-label" style="color: Blue;">
                                        Item BOM Cost Details
                                    </label>
                                </div>
                                <div class="col-sm-12" style="overflow-x: auto;">
                                    <asp:GridView ID="gvItemCostingDetails" runat="server" AutoGenerateColumns="true"
                                        ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                        HeaderStyle-HorizontalAlign="Center">
                                        <Columns>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Label ID="lblBOMCost" Text="" runat="server" Style="color: darkgreen; font-size: x-large; font-family: fantasy;"></asp:Label>
                                </div>
                                    <div class="col-sm-12 p-t-10 text-center">
                                        <label class="form-label" style="color: Blue;">
                                            FIM Cost Details
                                        </label>
                                    </div>
                                    <div class="col-sm-12 p-t-10" id="IssuePartiv" style="overflow-x: scroll;">
                                        <asp:GridView ID="gvIssuePartDetails" runat="server" AutoGenerateColumns="true"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium orderingfalse"
                                            HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:Label ID="lblIssuePartCost" Text="" runat="server" Style="color: darkgreen; font-size: x-large; font-family: fantasy;"></asp:Label>
                                    </div>
                                <div class="col-sm-12 p-t-10 text-center">
                                    <label class="form-label" style="color: Blue;">
                                        Addtional Part BOM Cost Details
                                    </label>
                                </div>
                                <div class="col-sm-12 p-t-10" id="AddtionalPartiv" style="overflow-x: scroll;">
                                    <asp:GridView ID="gvAddtionalPartDetails" runat="server" AutoGenerateColumns="true"
                                        ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                        HeaderStyle-HorizontalAlign="Center">
                                        <Columns>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Label ID="lblAddtionalPartCost" Text="" runat="server" Style="color: darkgreen; font-size: x-large; font-family: fantasy;"></asp:Label>
                                </div>
                                <div class="col-sm-12 text-center">
                                    <asp:Label ID="lblTotalBOMCost" Text="" runat="server" Style="color: brown; font-size: x-large; font-family: fantasy;"></asp:Label>
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
                        <asp:PostBackTrigger ControlID="btndownloaddocs" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H1">
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
