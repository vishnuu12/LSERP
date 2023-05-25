<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="QuotationSubmission.aspx.cs" Inherits="Pages_QuatetionSubmission" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function ShowAddPopUp() {
            $('#mpeAdd').modal({
                backdrop: 'static'
            });
            MakeCostMandatory('all');
            return false;
        }

        function checkAllandMakeMandatory(ele) {
            debugger;
            if ($(ele).is(":checked")) {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', true);
            }
            else {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', false);
            }
            MakeCostMandatory('all');
        }

        function MakeCostMandatory(ele) {
            try {
                if (ele == 'all') {
                    $('#ContentPlaceHolder1_gvQuateSubmissionDetails').find('[type="checkbox"]').each(function () {
                        if ($(this).is(":checked")) {
                            $(this).closest('tr').find('[type="text"]').addClass("mandatoryfield");
                            $(this).closest('tr').find('[type="text"]').before('<span class="textboxmandatory" style="color: red;">*</span>');
                        }
                        else {
                            $(this).closest('tr').find('[type="text"]').removeClass("mandatoryfield");
                            $(this).closest('tr').find('.textboxmandatory').remove();
                        }
                    });
                }
                else {
                    if ($(ele).is(":checked")) {
                        $(ele).closest('tr').find('[type="text"]').addClass("mandatoryfield");
                        $(ele).closest('tr').find('[type="text"]').before('<span class="textboxmandatory" style="color: red;">*</span>');
                    }
                    else {
                        $(ele).closest('tr').find('[type="text"]').removeClass("mandatoryfield");
                        $(ele).closest('tr').find('.textboxmandatory').remove();
                    }
                }
            } catch (er) { }
        }


        function CalculateTotalCost(ele) {
            var i;
            if ($(ele).attr('class').includes('UnitCost')) {
                i = 1;
            }
            else {
                i = 0;
            }
            var ReqWeigh = $(ele).closest('tr').find('[type="text"]')[i].value;
            var UnitCost = $('#' + ele.id).val();
            var res = parseFloat(ReqWeigh * UnitCost).toFixed(2);
            $('#' + ele.id).closest('tr').find('.TotalCost').text(res);
            return false;
        }

        function UpdateQuoteStatus() {
            swal({
                title: "No Further Edit Once Shared.",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Save it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('UpdateQuoteStatus', null);
            });
            return false;
        }

        function checkAllItems(ele) {
            debugger;
            if ($(ele).is(":checked")) {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', true);
            }
            else {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', false);
            }
        }

        function validateShareQuote() {
            if ($('#ContentPlaceHolder1_gvQuateSubmission').find('[type="checkbox"]:checked').not('#ContentPlaceHolder1_gvQuateSubmission_chkall').length > 0) {
                ShareQuotation();
                return false;
            }
            else {
                ErrorMessage('Error', 'No Indent Selected');
                hideLoader();
                return false;
            }
        }

        function ShareQuotation() {
            swal({
                title: "No Further Edit Once Quotation Shared.",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Share it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('UpdateQuoteStatus', null);
            });
            return false;
        }
        function ViewFileName(RowIndex) {
            __doPostBack("ViewIndentCopy", RowIndex);
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
                                    <h3 class="page-title-head d-inline-block">Quate Submission</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Production</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Quate Submission</li>
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
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" runat="server">
                            <div class="ip-div text-center">
                                <div class="ip-div text-center">
                                    <div class="col-sm-12">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label">
                                                RFP No</label>
                                        </div>
                                        <div class="col-sm-6 text-left">
                                            <asp:DropDownList ID="ddlRFPno" runat="server" CssClass="form-control" Width="70%"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlRFPno_SelectedIndexChanged" ToolTip="Select RFP Number">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label">
                                                Indent No</label>
                                        </div>
                                        <div class="col-sm-6 text-left">
                                            <asp:DropDownList ID="ddlPIIndentNumber" runat="server" AutoPostBack="true" CssClass="form-control"
                                                OnSelectedIndexChanged="ddlPIIndentNumber_SelectIndexChanged" Width="70%" ToolTip="Select Indent No">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                        </div>
                        <div id="divAddNew" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12 p-t-10">
                                    <asp:LinkButton ID="btnAddNew" CssClass="btn btn-success" runat="server" Text="Quote"
                                        OnClick="btnAddNew_Click"></asp:LinkButton>
                                </div>
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
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvQuateSubmission" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                OnRowCommand="gvQuateSubmission_OnRowCommand" OnRowDataBound="gvQuateSubmission_OnRowDataBound"
                                                HeaderStyle-HorizontalAlign="Center" DataKeyNames="PID,IndentDrawingName">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-Width="15%">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkall" runat="server" onclick="return checkAllItems(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkQC" runat="server" AutoPostBack="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQuateStatus" runat="server" Text='<%# Eval("QuoteStatusNew")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Supplier Name" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            
                                                            <asp:Label ID="lblSupplierName" runat="server" style="white-space:nowrap;font-weight: bold;font-size: small;" Text='<%# Eval("SupplierName").ToString().Replace(",", "<br/>")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="PO No" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPONo" runat="server" Text='<%# Eval("PONo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="RFP No" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRFPNo" runat="server" Text='<%# Eval("RFPNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CATG Name" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCategoryName" runat="server" Text=' <%# Eval("CategoryName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="MTL TYP" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMaterialType" runat="server" Text=' <%# Eval("TypeName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="MTL Grade" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMaterialGrade" runat="server" Text=' <%# Eval("GradeName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Measurement" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMeasurment" runat="server" Text=' <%# Eval("Measurment").ToString().Replace(",", "<br/>")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="MTL THK" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMaterialThickness" runat="server" Text=' <%# Eval("THKValue")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Req Qty" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReqQty" runat="server" Text=' <%# Eval("RequiredWeight")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUOM" runat="server" Text=' <%# Eval("uomname")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="DWG" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDeviationFile" runat="server"
                                                                OnClientClick='<%# string.Format("return ViewFileName({0});",((GridViewRow) Container).RowIndex) %>'>       
                                                                    <img src="../Assets/images/view.png" />
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Remarks" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIndentRemarks" runat="server" Text='<%# Eval("IndentRemarks")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Review Quotation" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnReviewQuotation" CssClass="btn btn-success" runat="server"
                                                                Text="Review" CommandName="Review" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="hdnPID" runat="server" Value="0" />
                            <div class="ip-div text-center">
                                <div class="col-sm-12 p-t-10">
                                    <asp:LinkButton ID="btnShareQuote" CssClass="btn btn-success" runat="server" OnClientClick="return validateShareQuote();"
                                        Text="Share Quotation"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hdnBOMId" Value="0" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal" id="mpeAdd" style="overflow-y: scroll;">
        <div class="modal-dialog" style="max-width: 95% !important; margin-left: 2% !important;">
            <div class="modal-content">
                <asp:UpdatePanel ID="upView" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSaveQPDetails" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">Supplier Details</h4>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;" id="divQPDetails">
                            <div class="inner-container">
                                <div runat="server">
                                    <div class="col-sm-12">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label mandatorylbl">
                                                Category Name</label>
                                        </div>
                                        <div class="col-sm-8 text-left">
                                            <asp:DropDownList ID="ddlCategoryName" runat="server" CssClass="form-control mandatoryfield"
                                                Width="70%" ToolTip="Select Category Name" OnSelectedIndexChanged="ddlCategoryName_SelectedIndexChanged"
                                                AutoPostBack="true">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label mandatorylbl">
                                                Supplier Name</label>
                                        </div>
                                        <div class="col-sm-8 text-left">
                                            <asp:DropDownList ID="ddlSupplierName" runat="server" CssClass="form-control mandatoryfield"
                                                Width="70%" ToolTip="Select Supplier Name" OnSelectedIndexChanged="ddlSupplierName_SelectedIndexChanged"
                                                AutoPostBack="true">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label mandatorylbl" id="lblfileuploader" runat="server">
                                                Attachements</label>
                                        </div>
                                        <div class="col-sm-8 text-left">
                                            <asp:FileUpload ID="fpSupplierAttachements" runat="server" class="form-control mandatoryfield"
                                                Width="70%" />
                                            <%-- <asp:Label ID="lblDrawingName" runat="server"></asp:Label>--%>
                                        </div>
                                    </div>
                                    <%--    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4 text-right">
                                        </div>
                                        <div class="col-sm-8 text-left">
                                            <asp:LinkButton ID="lnkAttachmentFile" runat="server" Text=""></asp:LinkButton>
                                        </div>
                                    </div>--%>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label">
                                                Remarks</label>
                                        </div>
                                        <div class="col-sm-8 text-left">
                                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" AutoComplete="off"
                                                ToolTip="Enter Remarks" placeholder="Enter Remarks" Width="70%">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <asp:GridView ID="gvQuateSubmissionDetails" runat="server" AutoGenerateColumns="False"
                                        ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                        HeaderStyle-HorizontalAlign="Center" DataKeyNames="PID">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="" ControlStyle-CssClass="nosort" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkall" runat="server" onclick="return checkAllandMakeMandatory(this);" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkitems" runat="server" AutoPostBack="false" Checked='<%# Eval("Checked").ToString() == "true" ? true: false%>'
                                                        onclick="return MakeCostMandatory(this);" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Material Category" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMaterialCategory" runat="server" Text=' <%# Eval("CategoryName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Material Grade" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMaterialGrade" runat="server" Text=' <%# Eval("GradeName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Material Thickness" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMaterialThickness" runat="server" Text=' <%# Eval("THKValue")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Uom" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUom" runat="server" Text=' <%# Eval("uomname")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Cost/Kg" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtCost" runat="server" CssClass="form-control UnitCost" onkeypress="return fnAllowNumeric();"
                                                        onkeyup="return CalculateTotalCost(this);" Text=' <%# Eval("QuoteCost").ToString()=="0"?"":Eval("QuoteCost")  %>'
                                                        autocomplete="off"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Required Quantity" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequiredWeight" runat="server" Text=' <%# Eval("RequiredWeight") %>'></asp:Label>
                                                    <%--    <asp:TextBox ID="txtRequiredWeight" runat="server" CssClass="form-control Requiredweight"
                                                        onkeypress="return fnAllowNumeric();" onkeyup="return CalculateTotalCost(this);"
                                                        autocomplete="off" Text=' <%# Eval("RequiredWeight") %>'></asp:TextBox>--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Material Type" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMaterialType" runat="server" Text=' <%# Eval("TypeName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Measurement" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMeasurment" runat="server" Text=' <%# Eval("Measurment").ToString().Replace(",", "<br/>") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Indent Remarks" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIndentRemarks" runat="server" Text=' <%# Eval("IndentRemarks")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Cost" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotalCost" runat="server" CssClass="TotalCost" Text=' <%# Eval("TotalCost") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="col-sm-12 p-t-10 text-center">
                                    <asp:LinkButton Text="Save" CssClass="btn btn-cons btn-save  AlignTop" ID="btnSaveQPDetails"
                                        runat="server" OnClientClick="return Mandatorycheck('divQPDetails');" OnClick="btnSave_Click" />
                                    <%--     <asp:LinkButton Text="Cancel" CssClass="btn btn-cons btn-save  AlignTop" ID="btnCancelQPDetails"
                                            runat="server" OnClick="btnCancel_Click" />--%>
                                </div>
                            </div>
                            <div class="modal-footer">
                            </div>
                        </div>
                        <asp:HiddenField ID="hdnQPDID" runat="server" Value="0" />
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
    <div class="modal" id="mpeShowDocument">
        <div class="modal-dialog" style="height: 100%;">
            <div class="modal-content" style="height: 100%;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" Style="height: 100%;">
                    <Triggers>
                        <%--  <asp:PostBackTrigger ControlID="btndownloaddocs" />--%>
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
                                    <%--  <asp:LinkButton Text="Download" CssClass="btn btn-save btn-cons AlignTop" ID="btndownloaddocs"
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
