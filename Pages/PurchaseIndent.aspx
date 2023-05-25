<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="PurchaseIndent.aspx.cs" Inherits="Pages_PurchaseIndent" EnableEventValidation="false"
    ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function multiselect(multivalue) {
            debugger;
            var test = multivalue.trim();
            var testArray = test.split(',');
            $('#ContentPlaceHolder1_liCertificates').val(testArray);
        }

        function ShowViewPopUp() {
            $('#mpeShowDocument').modal({
                backdrop: 'static'
            });
            return false;
        }

        function SaveIndent() {
            var mancheck = Mandatorycheck('ContentPlaceHolder1_divInput');
            if (mancheck == false)
                return false;

            var mtfid = '';
            var mtfidsval = '';
            $('#ContentPlaceHolder1_divMTFields').find('input[type="text"]').each(function (index) {
                debugger;
                mtfid = mtfid + $(this).attr('id').split(/[\s_]+/).pop() + ',';
                mtfidsval = mtfidsval + $(this).val() + ',';
            });
            $('#ContentPlaceHolder1_hdn_MTFIDS').val(mtfid.replace(/.$/, ""));
            $('#ContentPlaceHolder1_hdn_MTFIDsValue').val(mtfidsval.replace(/.$/, ""));
        }

        function dynamicvalueretain() {
            var i = 0;
            $('#ContentPlaceHolder1_hdn_MTFIDsValue').val().split(',').forEach(function (arrval) {
                $($('#ContentPlaceHolder1_divMTFields').find('input[type="text"]')[i]).val(arrval);
                i++;
            });
        }

        function ShowRFPListPOpUp() {
            $('#mpeAddRFPList').modal('show');
            return false;
        }

        function rblpurchase(type) {
            //$('#ContentPlaceHolder1_ddlmaterialGrdae').addClass('mandatoryfield');
            //$('#ContentPlaceHolder1_ddlThickness').addClass('mandatoryfield');
        }

        function ShowRFPPopUp() {
            $('#mpeAddRFPList').modal('show');
            return false;
        }

        function ValidateReqWeight(ele) {
            if ($('#ContentPlaceHolder1_hdnrblType').val() == "R") {
                var Qty = parseFloat($(event.target).val());
                var ReqQty = parseFloat($('#ContentPlaceHolder1_hdnReqWeight').val());

                if (Qty < ReqQty) {
                    ErrorMessage('Error', 'Entered Qty Should Not Less Than Req Qty');
                    $(event.target).val(ReqQty);
                }
            }
        }

        function ViewFileName(RowIndex) {
            __doPostBack("ViewIndentCopy", RowIndex);
        }

        function deleteConfirmMPMD(PID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Indentwill be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deletegvrowMPMD', PID);
            });
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
                                        <img src="../Assets/images/pages.png" alt="" width="16px" />
                                    </span>
                                    <h3 class="page-title-head d-inline-block">Purchase Indent Details</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Production</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Purchase Indent</li>
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
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divradio" runat="server">
                            <div class="text-center">
                                <div class="col-sm-12 text-center" style="padding-left: 35%;">
                                    <asp:RadioButtonList ID="rblpurchase" runat="server" CssClass="radio radio-success"
                                        OnChange="rblpurchase();" AutoPostBack="true" OnSelectedIndexChanged="rblpurchase_SelectIndexChanged"
                                        RepeatDirection="Horizontal">
                                        <asp:ListItem Text="General" Value="G" Selected="True"></asp:ListItem>
                                        <%--   <asp:ListItem Text="RFP" Value="R"></asp:ListItem>--%>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                        </div>
                        <div id="divAdd" class="btnAddNew" runat="server">
                            <div class="text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Customer Name</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="true" CssClass="form-control"
                                            Width="70%" ToolTip="Select Customer Number">
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
                                <%-- <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Item Name</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlItemName" runat="server" ToolTip="Select Item Name" Width="70%"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlItemName_OnSelectIndexChanged"
                                            CssClass="form-control mandatoryfield">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>--%>
                            </div>
                        </div>
                        <div class="col-sm-12 text-center p-t-10" id="divAddNew" runat="server">
                            <asp:LinkButton ID="btnAddNew" runat="server" Text="Add New" CssClass="btn btn-success add-emp"
                                OnClick="btnAddNew_Click"></asp:LinkButton>
                        </div>
                        <div id="divInput" class="divInput" runat="server">
                            <div class="col-sm-12 p-t-10">
                                <div class="col-sm-4">
                                </div>
                                <div class="col-sm-4 text-left" visible="false" runat="server">
                                    <asp:CheckBox ID="chckworkorderindent" AutoPostBack="true" OnCheckedChanged="ChckedChanged"
                                        runat="server" /><label class="form-label" style="width: 70% !important; float: initial; margin-left: 5px;">Work Order Indent</label>
                                </div>
                                <div class="col-sm-4">
                                </div>
                            </div>
                            <div class="ip-div text-center">
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4 text-right">
                                        <div class="text-center">
                                            <label class="form-label">
                                                Indent By</label>
                                        </div>
                                        <div>
                                            <asp:Label ID="lblIndentby" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 text-left" visible="false" runat="server">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Indent To</label>
                                        </div>
                                        <div class="text-left">
                                            <asp:DropDownList ID="ddlIndentTo" runat="server" ToolTip="Select Indent To" CssClass="form-control">
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
                                    <div class="col-sm-4 text-left">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Material Grade
                                            </label>
                                        </div>
                                        <div>
                                            <asp:DropDownList ID="ddlmaterialGrdae" runat="server" ToolTip="Select material grade"
                                                AutoPostBack="true" CssClass="form-control mandatoryfield" OnSelectedIndexChanged="ddlmaterialGrdae_OnSelectIndexChanged">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 text-left">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Thickness
                                            </label>
                                        </div>
                                        <div class="text-left">
                                            <asp:DropDownList ID="ddlThickness" runat="server" ToolTip="Select Thickness Value"
                                                AutoPostBack="false" CssClass="form-control mandatoryfield" OnSelectedIndexChanged="ddlThickness_OnSelectIndexChanged">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10" runat="server">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4 text-left">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Material Category
                                            </label>
                                        </div>
                                        <div>
                                            <asp:DropDownList ID="ddlMaterialCategory" runat="server" ToolTip="Select Material Category Name"
                                                CssClass="form-control mandatoryfield" AutoPostBack="true" OnSelectedIndexChanged="ddlMaterialCategory_OnSelectIndexChanged">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 text-left" id="partname" runat="server">
                                        <div class="text-left">
                                            <label class="form-label">
                                                Part Name</label>
                                        </div>
                                        <div>
                                            <asp:HiddenField ID="hdnmpids" runat="server" />
                                            <asp:Label ID="lblpartname" runat="server"></asp:Label>
                                            <%-- <asp:DropDownList ID="ddlPartName" runat="server" ToolTip="Select Part Name" CssClass="form-control mandatoryfield"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlPartName_OnSelectIndexChanged">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>--%>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                        <asp:Label ID="lblreqweight" CssClass="lablqty" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-4 text-left">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Required Quantity</label>
                                        </div>
                                        <div class="text-left">
                                            <asp:TextBox ID="txtrequiredWeight" runat="server" CssClass="form-control mandatoryfield"
                                                AutoComplete="off"
                                                onblur="return ValidateReqWeight(this);" onkeypress="return validationDecimal(this);" ToolTip="Enter Quantity" placeholder="Enter Quantity">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 text-left" id="matrltype" runat="server">
                                        <div class="text-left">
                                            <label class="form-label">
                                                Material Type</label>
                                        </div>
                                        <div>
                                            <asp:DropDownList ID="ddlMaterialType" runat="server" ToolTip="Select Material type"
                                                CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlMaterialType_OnSelectIndexChanged">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div id="divMTFields" runat="server">
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4 text-left" id="mrnno" runat="server" visible="false">
                                        <div class="text-left">
                                            <label class="form-label">
                                                MRN Number</label>
                                        </div>
                                        <div>
                                            <asp:DropDownList ID="ddlmrnno" runat="server" ToolTip="Select Material type" CssClass="form-control"
                                                Enabled="false">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 text-left">
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2" style="margin-top: 38px;">
                                    </div>
                                    <div class="col-sm-4 text-left">


                                        <div>
                                            <label class="form-label mandatorylbl">
                                                UOM Name</label>
                                        </div>
                                        <div>
                                            <asp:DropDownList ID="ddlUOM" runat="server" ToolTip="Select UOM" CssClass="form-control mandatoryfield">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="col-sm-4 text-left">
                                        <div class="text-left">
                                            <label class="form-label">
                                                Remarks</label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" AutoComplete="off"
                                                ToolTip="Enter Remarks" placeholder="Enter Remarks">
                                            </asp:TextBox>
                                        </div>
                                        <%--<div class="text-left">
                                            <label class="form-label">
                                                Drawing Name</label>
                                        </div>
                                        <div>
                                            <asp:Label ID="lblDrawingName" runat="server"></asp:Label>
                                        </div>--%>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2" style="margin-top: 38px;">
                                        <asp:Label ID="Label3" CssClass="lablqty" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-4 text-left">
                                        <div class="text-left">
                                            <label class="form-label">
                                                Upload</label>
                                        </div>
                                        <div>
                                            <asp:FileUpload ID="fPUpload" runat="server" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="col-sm-4 text-left">
                                        <div class="text-left">
                                            <label class="form-label">
                                                Drawing name</label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtDrawingname" runat="server" CssClass="form-control" AutoComplete="off"
                                                placeholder="Enter Drawing name">
                                            </asp:TextBox>
                                        </div>
                                        <%--<div class="text-left">
                                            <label class="form-label">
                                                Drawing Name</label>
                                        </div>
                                        <div>
                                            <asp:Label ID="lblDrawingName" runat="server"></asp:Label>
                                        </div>--%>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4 text-left">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Delivery Date</label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtDeliveryDate" runat="server" CssClass="form-control datepicker mandatoryfield"
                                                AutoComplete="off" ToolTip="Enter Delivery Date" placeholder="Enter Delivery Date">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 text-left">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Available Certificates</label>
                                        </div>
                                        <div>
                                            <asp:ListBox ID="liCertificates" runat="server" CssClass="form-control mandatoryfield"
                                                SelectionMode="Multiple"></asp:ListBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4 text-left" id="divgeneralPurchaseRFPNoList" runat="server" visible="false">
                                    </div>
                                    <div class="col-sm-4 text-left" id="jobdescription" runat="server" visible="false">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Job Description</label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtJobdescription" runat="server" CssClass="form-control mandatoryfield"
                                                AutoComplete="off" ToolTip="Enter Job Description" placeholder="Enter Job Description">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-20">
                                    <asp:HiddenField ID="hdn_MTFIDS" runat="server" />
                                    <asp:HiddenField ID="hdn_MTFIDsValue" runat="server" />
                                    <asp:LinkButton ID="btnSave" runat="server" Text="Save" OnClientClick="return SaveIndent();"
                                        OnClick="btnSave_Click" CssClass="btn btn-cons btn-save AlignTop"></asp:LinkButton>
                                    <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" CausesValidation="False"
                                        OnClick="btncancel_Click" CssClass="btn btn-cons btn-danger AlignTop btnCancel"></asp:LinkButton>
                                </div>
                            </div>
                            <asp:HiddenField ID="hdnReqWeight" runat="server" Value="0" />
                            <asp:HiddenField ID="hdnrblType" runat="server" Value="0" />
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="col-md-12 row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                                    <asp:Label ID="lbltitle" runat="server" Text="Purchase Indent"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server">
                                            </div>
                                        </div>

                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvPurchaseIndentDetails" runat="server" AutoGenerateColumns="False"
                                                OnRowCommand="gvPurchaseIndentDetails_OnRowCommand" OnRowDataBound="gvPurchaseIndentDetails_OnRowDataBound"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium orderingfalse"
                                                DataKeyNames="PID,FileName">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Indent Status" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRNo" runat="server" Text='<%# Eval("PID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Indent No" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIndentNo" runat="server" Text='<%# Eval("IndentNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="RFP No" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRFPNo" runat="server" Text='<%# Eval("RFPNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Category Name" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("CategoryName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Part Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitemname" runat="server" Text='<%# Eval("PartName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Material Type Name" ItemStyle-HorizontalAlign="left"
                                                        HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTypeName" runat="server" Text='<%# Eval("TypeName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Grade Name" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGradeName" runat="server" Text='<%# Eval("GradeName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="THK" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTHK" runat="server" Text='<%# Eval("THKValue")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Measurments" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMeasurments" runat="server" Text=' <%# Eval("Measurment").ToString().Replace(",", "<br/>")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemarks" runat="server"
                                                                Text='<%# Eval("Remarks").ToString() %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Indent By" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIndentBy" runat="server" Text='<%# Eval("EmpIndentBy")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Indent To" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIndentTo" runat="server" Text='<%# Eval("EmpIndentTo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delivery Date" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDeliveryDate" runat="server" Text='<%# Eval("DeliveryDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cerificates" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCertificates" runat="server" Text='<%# Eval("Certificate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Required Qty" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReqQty" runat="server" Text='<%# Eval("RequiredWeight")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                        HeaderText="View">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnView" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                OnClientClick='<%# string.Format("return ViewFileName({0});",((GridViewRow) Container).RowIndex) %>'>
                                                                <img src="../Assets/images/view.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                        HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEdit" runat="server" Visible='<%# Eval("PidStatus").ToString() == "Having Record" ? false : true %>' CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="EditPI"><img src="../Assets/images/edit-ec.png"/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                        HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDelete" runat="server" Visible='<%# Eval("PidStatus").ToString() == "Having Record" ? false : true %>' CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                OnClientClick='<%# string.Format("return deleteConfirmMPMD({0});",Eval("PID")) %>'>   <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                        HeaderText="Add RFP">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnAddRFPNo" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="AddRFP"><img src="../Assets/images/add1.png"/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnPID" runat="server" Value="0" />
                                        </div>
                                        <div class="col-sm-12 text-center">
                                            <%--   <asp:LinkButton Text="Save Purchase Indent" CssClass="btn btn-cons btn-success" ID="btnPurchaseIndentStatus"
                                                OnClick="btnPurchaseIndentStatus_Click" runat="server" />--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal" id="mpeShowDocument">
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
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal" id="mpePurchseIndent_p">
        <div class="modal-dialog" style="height: 100%;">
            <div class="modal-content" style="height: 100%;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always" Style="height: 100%;">
                    <Triggers>
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
                                    <div id="divPurchaseIndentPrint_p" runat="server">
                                        <div class="col-sm-12" style="text-align: center; font-size: 20px !important; font-weight: 700; margin: 1em 0px">
                                            Purchase Indent
                                        </div>
                                        <div class="col-sm-12" style="width: 94%; margin-left: 3%">
                                            <div class="col-sm-3">
                                                <label style="font-weight: 700">
                                                    Indent By</label>
                                                <asp:Label ID="lblIndentBy_p" runat="server" Style="font-weight: 700"></asp:Label>
                                            </div>
                                            <div class="col-sm-6">
                                                <label style="font-weight: 700">
                                                    RFP Number</label>
                                                <asp:Label ID="lblRFPNo_p" runat="server" Style="font-weight: 700"></asp:Label>
                                            </div>
                                            <div class="col-sm-3">
                                                <label style="font-weight: 700">
                                                    Indent Date</label>
                                                <asp:Label ID="lblIndentDate_p" runat="server" Style="font-weight: 700"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvPurchseIndentDetails_p" runat="server" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                Style="margin-left: 3%; width: 94%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Indent No" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIndentNo" runat="server" Text='<%# Eval("PINumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Category Name" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("CategoryName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Grade Name" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGradeName" runat="server" Text='<%# Eval("GradeName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Material Type Name" ItemStyle-HorizontalAlign="left"
                                                        HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTypeName" runat="server" Text='<%# Eval("TypeName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Measurment" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMeasurment" runat="server" Text='<%# Eval("Measurment")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delivery Date" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDeliveryDate" runat="server" Text='<%# Eval("DeliveryDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cerificates" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCertificates" runat="server" Text='<%# Eval("Certificate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="col-sm-12" style="margin-left: 3%; width: 94%; font-size: 12px; font-weight: 700; margin-top: 10px; color: #000">
                                            Note: This indent is Computer Generated and this indent is raised by:
                                            <asp:Label ID="lblNote_p" runat="server" Style="font-size: 12px; font-weight: 700"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal" id="mpeGeneralPurchaseIndentRFPNo">
        <div class="modal-dialog" style="height: 100%;">
            <div class="modal-content" style="height: 100%;">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always" Style="height: 100%;">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H3">
                                <div class="text-center">
                                    Add RFPNo
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal">
                                x</button>
                        </div>
                        <div class="modal-body" style="height: 100%;">
                            <div class="inner-container" style="height: 100%;">
                                <div class="ip-div text-center" style="margin-bottom: 10px;">
                                    <div class="col-sm-12">
                                        <div class="col-sm-4">
                                            <label class="form-label">
                                                RFP No</label>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:ListBox ID="liRFPNo" runat="server" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal" id="mpeAddRFPList">
        <div class="modal-dialog" style="height: 100%;">
            <div class="modal-content" style="height: 100%;">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always" Style="height: 100%;">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnRFPQCPage" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H4">
                                <div class="text-center">
                                    <asp:Label ID="Label4" Text="" runat="server"></asp:Label>
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal">
                                x</button>
                        </div>
                        <div class="modal-body" style="height: 100%;">
                            <div class="inner-container" style="height: 100%;">
                                <div class="ip-div text-center" style="margin-bottom: 10px;">
                                    <div class="col-sm-12">
                                        <div class="col-sm-4">
                                            <label class="form-label">
                                                RFP No
                                            </label>
                                        </div>
                                        <div class="col-sm-6">
                                            <asp:ListBox ID="lstRFPList" runat="server" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:LinkButton ID="btnRFPQCPage"
                                                OnClick="btnAddRFP_Click" runat="server"> Add RFP Material QC </asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 text-center p-t-10">
                                        <asp:LinkButton Text="Add RFP" CssClass="btn btn-cons btn-success" ID="btnAddRFP"
                                            OnClick="btnAddRFP_Click" runat="server" />
                                    </div>
                                    <div class="col-sm-12 text-center p-t-10">
                                        <div class="col-sm-4">
                                            <label class="form-label">
                                            You Selected RFP No List</labe> 
                                        </div>
                                        <div class="col-sm-6 text-left">
                                            <asp:Label ID="lblRFPList" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
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
