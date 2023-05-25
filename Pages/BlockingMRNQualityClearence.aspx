<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="BlockingMRNQualityClearence.aspx.cs" Inherits="Pages_BlockingMRNQualityClearence"
    ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function ShowQCPopUp() {
            $('#mpeQC').modal({
                backdrop: 'static'
            });
            return false;
        }

        function ShowAssemplyPlanningPopUp() {
            $('#mpeAssemplyPlanning').modal({
                backdrop: 'static'
            });
            return false;
        }

        function UpdateRFPQPStatus() {
            swal({
                title: "No Further Edit Once Shared.",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Save it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('UpdateRFPQPStatus', null);
            });
            return false;
        }

        function deleteConfirm(PAPDID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Assemply Planning will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deletegvrow', PAPDID);
            });
            return false;
        }


        function Validate() {
            //var rowcount = $('#ContentPlaceHolder1_gvQualityClearence').find('tbody tr').not('.odd').not('.even').length;
            //var radiocount = $('#ContentPlaceHolder1_gvQualityClearence').find('[type="radio"]:checked').length;

            var Count = $('#ContentPlaceHolder1_gvQualityClearence').find('[type="checkbox"]:checked').length;
            var msg = Mandatorycheck('ContentPlaceHolder1_divQCClearance')
            if (Count > 0) {
                if (msg == true) {
                    // if (rowcount == radiocount) {
                    SaveBlockingMRNQualityClearance();
                    return true;
                    // }
                    // else {
                    //ErrorMessage('Error', 'Please Select All MRN');
                    //   return false;
                    // }
                }
                else {
                    hideLoader();
                    return false;
                }
            }
            else {
                ErrorMessage('Error', 'Please Select Atleast One Item');
                hideLoader();
                return false;
            }
        }

        function rblApproveChange() {
            if ($(event.target).val() == 'R') {
                $(event.target).closest('table').closest('tr').find('[type="text"]').addClass('mandatoryfield');
                $(event.target).closest('table').closest('tr').find('[type="text"]').before('<span class="reqfield" style="color:red">*</span>');
            }
            else {
                $(event.target).closest('table').closest('tr').find('[type="text"]').removeClass('mandatoryfield');
                $(event.target).closest('table').closest('tr').find('.reqfield').remove();
            }
        }

        function SaveBlockingMRNQualityClearance() {
            swal({
                title: "No Further Edit Once Approved Records.",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Save it!",
                closeOnConfirm: false
            }, function () {
                showLoader();
                __doPostBack('BMRNQCStatus', null);
            });
            return false;
            hideLoader();
        }

    </script>
    <style type="text/css">
        label {
            padding-left: 18px !important;
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
                                    <h3 class="page-title-head d-inline-block">Material Planning MRN QC</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Quality</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page"> Material Planning QC </li>
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
                            <div class="text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Customer Name</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="true" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlCustomerName_OnSelectedIndexChanged" Width="70%" ToolTip="Select Customer Number">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            RFP No</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlRFPNo" runat="server" AutoPostBack="true" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlRFPNo_SelectIndexChanged" Width="70%" ToolTip="Select RFP No">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
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
                                        <div class="col-md-12 row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvItemDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                OnRowCommand="gvItemDetails_OnRowCommand" OnRowDataBound="gvItemDetails_OnRowDataBound"
                                                DataKeyNames="RFPDID">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Name" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Status" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblstatus" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--Text='<%# Eval("BQCStatus")%>'--%>

                                                    <asp:TemplateField HeaderText="Quality Clearence" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnView" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="QCView"><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--   <asp:TemplateField HeaderText="QC Status" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPlanningStatus" CssClass="PlanningStatus" runat="server" Text='<%# Eval("QCStatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <%--       <asp:TemplateField HeaderText="Assemply Planning" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnAPView" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="APView"><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <%--   <asp:TemplateField HeaderText="AP Status" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAPStatus" runat="server" Text='<%# Eval("APStatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnEDID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnRFPDID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnPAPDID" runat="server" Value="0" />
                                        </div>
                                        <div class="col-sm-12 p-t-20 text-center" style="display: none;">
                                            <asp:LinkButton ID="btnSaveAndShare" runat="server" OnClick="btnSaveAndShare_OnClick"
                                                Text="Save RFP QC Status" CssClass="btn btn-cons btn-success"></asp:LinkButton>
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
    <div class="modal" id="mpeAssemplyPlanning" style="overflow-y: scroll;">
        <div style="max-width: 81%; padding-left: 10%; padding-top: 5%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD"></h4>
                            <asp:Label ID="lblItemName_AP" runat="server" Style="font-weight: bold; padding-left: 30px; color: brown;"></asp:Label>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="divInput_AP" class="docdiv">
                                <div class="inner-container">
                                    <div class="col-sm-12">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label mandatorylbl">
                                                Part Name 1
                                            </label>
                                        </div>
                                        <div class="col-sm-4 text-left">
                                            <asp:DropDownList ID="ddlPartName1_AP" runat="server" CssClass="form-control mandatoryfield"
                                                Style="width: 336px;" ToolTip="Select Part Name" TabIndex="1">
                                                <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label mandatorylbl">
                                                Part Name 2
                                            </label>
                                        </div>
                                        <div class="col-sm-4 text-left">
                                            <asp:DropDownList ID="ddlPartName2_AP" runat="server" CssClass="form-control mandatoryfield"
                                                Style="width: 336px;" ToolTip="Select Part Name" TabIndex="1">
                                                <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label mandatorylbl">
                                                Select WPS Number
                                            </label>
                                        </div>
                                        <div class="col-sm-4 text-left">
                                            <asp:DropDownList ID="ddlWPSnumber_AP" runat="server" CssClass="form-control mandatoryfield"
                                                ToolTip="Select WPS Number">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label mandatorylbl">
                                                Remarks
                                            </label>
                                        </div>
                                        <div class="col-sm-4 text-left">
                                            <asp:TextBox ID="txtRemarks_AP" runat="server" CssClass="form-control mandatoryfield"
                                                ToolTip="Enter Remarks" Width="70%" TextMode="MultiLine" Rows="2" placeholder="Enter Remarks">
                                            </asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-20 text-center">
                                        <asp:LinkButton ID="btnSaveAP" runat="server" Text="Save" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divInput_AP')"
                                            OnClick="btnSaveAP_Click" CssClass="btn btn-cons btn-success"></asp:LinkButton>
                                    </div>
                                    <div class="col-sm-12 p-t-10" id="div9" runat="server" style="overflow-x: scroll;">
                                        <asp:GridView ID="gvAssemplyPlanningDetails_AP" runat="server" AutoGenerateColumns="False"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" OnRowCommand="gvAssemplyPlanningDetails_AP_OnRowCommand"
                                            CssClass="table table-hover table-bordered medium" DataKeyNames="PAPDID">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Part1" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPart1" runat="server" Text='<%# Eval("PartName1")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Part2" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPart2" runat="server" Text='<%# Eval("PartName2")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="WPS Number" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWPSNumber" runat="server" Text='<%# Eval("WPSNumber")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            CommandName="EditAP"><img src="../Assets/images/edit-ec.png" style="height:20px" alt=""></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete" runat="server" OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("PAPDID")) %>'>
                                                        <img src="../Assets/images/del-ec.png" alt="" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                        </div>
                        </div> </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal" id="mpeQC" style="overflow-y: scroll;">
        <div style="max-width: 98%; padding-left: 10%; padding-top: 5%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD"></h4>
                            <asp:Label ID="lblItemName_QC" runat="server" Style="font-weight: bold; padding-left: 30px; color: brown;"></asp:Label>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="Div2" class="docdiv">
                                <div class="inner-container">
                                    <div id="Div3" runat="server">
                                        <div id="div4" runat="server">
                                            <div class="col-sm-12 p-t-10" id="divQCClearance" runat="server" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvQualityClearence" runat="server" AutoGenerateColumns="False"
                                                    ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                    OnRowDataBound="gvQualityClearence_OnRowDataBound" DataKeyNames="MPMD">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="" ControlStyle-CssClass="nosort" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderTemplate>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkitems" runat="server"
                                                                    AutoPostBack="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MRN Number" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMRNNumber" CssClass="rowCount" runat="server" Text='<%# Eval("MRNNumber")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Part Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPartName" runat="server" Text='<%# Eval("PartName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Part Qty" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPartQty" runat="server" Text='<%# Eval("PartQty")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Material Name(Production)" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMaterialnameProd" runat="server" Text='<%# Eval("MaterialGradeNameProduction")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Material Category(Production)" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMaterialCategory" runat="server" Text='<%# Eval("CategoryNameProduction")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Material Name(Design)" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMaterialNameDesign" runat="server" Text='<%# Eval("MaterialGradeNameDesign")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Required Weight" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRequiredWeight" runat="server" Text='<%# Eval("RequiredWeight")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Thickness" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblThickness" runat="server" Text='<%# Eval("Thickness")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Blocked By" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBlockedBy" runat="server" Text='<%# Eval("BlockedBy")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Blocked Date" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBlockedDate" runat="server" Text='<%# Eval("BlockedDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="QC Approval" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:RadioButtonList ID="rblApprove" runat="server" CssClass="radio radio-success" OnChange="rblApproveChange();"
                                                                    RepeatDirection="Horizontal">
                                                                    <asp:ListItem Text="Approval" Value="A" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="Reject" Value="R"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtRemarks" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:HiddenField ID="hdnBOMID" runat="server" Value="0" />
                                            </div>
                                            <div class="col-sm-12 p-t-20 text-center">
                                                <asp:LinkButton ID="btnSaveItemQCStatus" runat="server" Text="SaveQCStatus" OnClientClick="return Validate();"
                                                    CssClass="btn btn-cons btn-success"></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                        </div>
                        </div> </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
