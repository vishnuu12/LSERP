<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="IMStock.aspx.cs" Inherits="Pages_IMStock" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function SaveIndent() {
            var mancheck = Mandatorycheck('ContentPlaceHolder1_divInput');
            if (mancheck == false) return false;
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

        function ShowAddPopUp() {
            $('#mpeView').show();
            $('div').removeClass('modal-backdrop in');
            $('.datepicker').datepicker({
                format: 'dd/mm/yyyy'
            });
            return false;
        }

        function HideAddPopUp() {
            $('#mpeView').hide();
            return false;
        }

        function OldMRNApplicable() {
            if ($('#ContentPlaceHolder1_chkQC').is(":checked") == true) {
                $('#ContentPlaceHolder1_divOldMRNNumber').hide();
                //  $('#ContentPlaceHolder1_ddlOldMRNNumber').removeClass('mandatoryfield')
            }
            else {
                $('#ContentPlaceHolder1_divOldMRNNumber').show();
                // $('#ContentPlaceHolder1_ddlOldMRNNumber').addClass('mandatoryfield')
            }
        }

        function ValidateQCCheckedQty(ele) {
            //if ($('#ContentPlaceHolder1_hdnQCCheckedQty').val() != 'NA') {
            var qty = $(ele).val();
            var QCCheckedQty = $('#ContentPlaceHolder1_hdnQCCheckedQty').val();

            if (parseFloat(qty) > parseFloat(QCCheckedQty) || parseFloat(qty) <= 0) {
                ErrorMessage('Error', 'Entered Qty ' + (qty) + ' Should Not Great QCChecked Qty');
                $(ele).val('');
            }
            //  }
        }

        function ShareConfirm(SPODID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the IM will be Share permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Share it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('ShareIM', SPODID);
            });
            return false;
        }

    </script>

    <style type="text/css">
        #ContentPlaceHolder1_divStockAvailability label {
            color: #000;
            font-weight: bold;
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
                                    <h3 class="page-title-head d-inline-block">IM Stock</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Stores</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">IM Stock</li>
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
                    <asp:PostBackTrigger ControlID="btnIMStock" />
                    <%-- <asp:PostBackTrigger ControlID="gvIMStock" />--%>
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" class="btnAddNew" runat="server">
                            <div class="text-center">
                            </div>
                        </div>
                        <div class="col-sm-12 text-center p-t-10" id="divAddNew" runat="server">
                            <asp:LinkButton ID="btnAddNew" runat="server" Text="Add New" CssClass="btn btn-success add-emp"
                                OnClick="btnAddNew_Click"></asp:LinkButton>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12" style="padding-left: 20%;">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4 text-Center">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Category Name</label>
                                        </div>
                                        <div>
                                            <asp:DropDownList ID="ddlMaterialCategory" runat="server" ToolTip="Select Material Category Name"
                                                Width="70%" AutoPostBack="true" OnSelectedIndexChanged="ddlMaterialCategory_OnSelectedIndexChanged"
                                                CssClass="form-control mandatoryfield">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 text-left">
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
                                                CssClass="form-control mandatoryfield">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 text-left" runat="server">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Thickness
                                            </label>
                                        </div>
                                        <div class="text-left">
                                            <asp:DropDownList ID="ddlThickness" runat="server" ToolTip="Select Thickness Value"
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
                                    <div class="col-sm-4 text-left" id="matrltype" runat="server">
                                        <div class="text-left">
                                            <label class="form-label">
                                                Material Type</label>
                                        </div>
                                        <div>
                                            <asp:DropDownList ID="ddlMaterialType" runat="server" ToolTip="Select Material type"
                                                CssClass="form-control" OnSelectedIndexChanged="ddlMaterialType_OnSelectIndexChanged"
                                                AutoPostBack="true">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 text-left">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Expiry Date</label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtExpiryDate" runat="server" CssClass="form-control datepicker mandatoryfield"
                                                AutoComplete="off" ToolTip="Enter Delivery Date" placeholder="Enter Delivery Date">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div id="divMTFields" runat="server">
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2" style="margin-top: 38px;">
                                        <asp:Label ID="lblreqweight" CssClass="lablqty" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-4 text-left">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Inwarded Quantity</label>
                                        </div>
                                        <div class="text-left">
                                            <asp:TextBox ID="txtInwardedQty" runat="server" CssClass="form-control mandatoryfield"
                                                onkeypress="return validationDecimal(this);" AutoComplete="off" ToolTip="Enter Qty" placeholder="Enter Weight">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 text-left">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Customer Material
                                            </label>
                                        </div>
                                        <div>
                                            <asp:RadioButtonList ID="rblCustomerMaterial" runat="server" CssClass="radio radio-success"
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                            </asp:RadioButtonList>
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
                                            <label class="form-label">
                                                Material Request QC</label>
                                        </div>
                                        <div>
                                            <asp:CheckBox ID="chkQC" runat="server" Checked="true" AutoPostBack="false" OnChange="return OldMRNApplicable();"></asp:CheckBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 text-left" style="display: none;" id="divOldMRNNumber" runat="server">
                                        <div class="text-left">
                                            <label class="form-label">
                                                Old MRN Number
                                            </label>
                                        </div>
                                        <div>
                                            <asp:DropDownList ID="ddlOldMRNNumber" runat="server" ToolTip="Select UOM" CssClass="form-control">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10" id="partname" runat="server">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4 text-left">
                                        <div class="text-left">
                                            <label class="form-label">
                                                Qty In Numbers</label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtQuantityInNumbers" runat="server" CssClass="form-control" AutoComplete="off"
                                                onkeypress="return validationDecimal(this);" ToolTip="Enter Quantity In Numbers" placeholder="Enter Unit Cost">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 text-left">
                                        <div class="text-left">
                                            <label class="form-label">
                                                Document Name</label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtDocumentname" runat="server" CssClass="form-control" AutoComplete="off"
                                                ToolTip="Enter Document Name" placeholder="Enter Document name">
                                            </asp:TextBox>
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
                                                Select Location</label>
                                        </div>
                                        <div>
                                            <asp:DropDownList ID="ddlLocation" runat="server" ToolTip="Select UOM" CssClass="form-control mandatoryfield">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 text-left">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                UOM</label>
                                        </div>
                                        <div class="text-left">
                                            <asp:DropDownList ID="ddlUOM" runat="server" ToolTip="Select UOM" CssClass="form-control mandatoryfield">
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
                                            <label class="form-label">
                                                Attachement</label>
                                        </div>
                                        <div>
                                            <asp:FileUpload ID="fpAttachement" runat="server" class="form-control" Width="70%" />
                                        </div>
                                    </div>
                                    <div class="col-sm-4 text-left">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Remarks
                                            </label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control mandatoryfield"
                                                AutoComplete="off" ToolTip="Enter Delivery Date" placeholder="Enter Remarks">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-20">
                                    <asp:LinkButton ID="btnIMStock" runat="server" Text="Save" OnClientClick="return SaveIndent();"
                                        OnClick="btnSaveIMStock_Click" CssClass="btn btn-cons btn-save AlignTop"></asp:LinkButton>
                                    <asp:LinkButton ID="btnCancelIMStock" runat="server" Text="Cancel" CausesValidation="False"
                                        OnClick="btnCancelIMStock_Click" CssClass="btn btn-cons btn-danger AlignTop btnCancel"></asp:LinkButton>
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
                                                    <asp:Label ID="lbltitle" runat="server" Text=""></asp:Label>
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvIMStock" runat="server" AutoGenerateColumns="False"
                                                    OnRowCommand="gvIMStock_OnRowCommand" OnRowDataBound="gvIMStock_OnRowDataBound"
                                                    ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                    DataKeyNames="SPODID,FileName,MRNID,IM_LocationID">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="QC Status" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQCStatus" runat="server" Text='<%# Eval("QCStatus")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                            HeaderText="Add Stock">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnAdd" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                    CommandName="AddStockAvailability">
                                                                <img src="../Assets/images/add1.png" alt="" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MRN Number" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMRNNumber" runat="server" Text='<%# Eval("MRNNumber")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Category Name" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("CategoryName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Material Type Name" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTypeName" runat="server" Text='<%# Eval("TypeName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Material Name" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGradeName" runat="server" Text='<%# Eval("GradeName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Material Thickness" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMaterialThickness" runat="server" Text='<%# Eval("THKValue")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Measurement" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMeasurement" runat="server" Text='<%# Eval("Measurment").ToString().Replace(",", "<br/>")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Location" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("Location")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="OLD MRN Number" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOldMRNNumber" runat="server" Text='<%# Eval("OldMRNNumber")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("IM_Remarks")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Required Qty" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRW" Width="50px" runat="server" Text='<%# Eval("InwardedQty")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Stock Inward Status" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <%--<asp:Label ID="lblStockInwardStatus" CssClass="fa fa-square" runat="server" Text='<%# Eval("Status")%>'></asp:Label>--%>
                                                                <asp:Label ID="lblMaterialRequestQC" runat="server" Style="display: none;" Text='<%# Eval("IM_MaterialRequestQC")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Customer Material" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCustomerMaterial" runat="server" Text='<%# Eval("IM_CustomerMaterial")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="View Attach" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnAttachView" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                    CommandName="ViewAttachFile">
                                                            <img src="../Assets/images/view.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                            HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                    CommandName="EditIMStock">
                                                                <img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="MRN Report" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnView" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                    CommandName="ViewMRNReport">
                                                                                             <img src="../Assets/images/view.png" alt="" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Share" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnShare" Text="Share" runat="server" CssClass="btn btn-cons btn-success"
                                                                    OnClientClick='<%# string.Format("return ShareConfirm({0});",Eval("SPODID")) %>'>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <asp:HiddenField ID="hdnQCCheckedQty" Value="0" runat="server" />
                                            <asp:HiddenField ID="hdnSPODID" Value="0" runat="server" />
                                            <asp:HiddenField ID="hdnMRNID" Value="0" runat="server" />
                                        </div>
                                        <div class="col-sm-12 p-t-20 text-center">
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


    <asp:HiddenField ID="hdn_MTFIDS" Value="0" runat="server" />
    <asp:HiddenField ID="hdn_MTFIDsValue" Value="0" runat="server" />


    <asp:HiddenField ID="hdnPdfContent" Value="0" runat="server" />

    <div class="modal" id="mpeView" style="overflow-y: scroll;">
        <div style="max-width: 81%; padding-left: 10%; padding-top: 5%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="upView" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header" style="padding-left: 50%; color: brown;">
                            <h4 class="modal-title ADD">
                                <asp:Label ID="lblMRNNumber_p" runat="server"></asp:Label></h4>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true"
                                onclick="HideAddPopUp();">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="docdiv" class="docdiv">
                                <div class="inner-container">
                                    <div id="Item" runat="server">
                                        <div id="divAddNewItem" runat="server">
                                            <div class="inner-container">
                                                <%--OnClientClick="return ShowAdditemdetailspopup();"--%>
                                                <div class="col-sm-12 text-center p-t-10" id="div5" runat="server">
                                                </div>
                                            </div>
                                        </div>
                                        <div id="divStockAvailability" class="divInput" runat="server">
                                            <div class="ip-div text-center">

                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-4">
                                                        <label>category Name</label>
                                                        <asp:Label ID="lblCategoryName_v" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Type </label>
                                                        <asp:Label ID="lblType_v" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Grade Name</label>
                                                        <asp:Label ID="lblGradeName_v" runat="server"> </asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-4">
                                                        <label>Thickness </label>
                                                        <asp:Label ID="lblThickness_v" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Measurment </label>
                                                        <asp:Label ID="lblMeasurment_v" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Location </label>
                                                        <asp:Label ID="lblLocation_v" runat="server"> </asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-4">
                                                        <label>UOM </label>
                                                        <asp:Label ID="lblUOM_v" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <label>Customer Material </label>
                                                        <asp:Label ID="lblCustomermaterial_v" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-4">
                                                    </div>
                                                </div>


                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-2">
                                                    </div>
                                                    <div class="col-sm-4 text-left">
                                                        <div class="text-left">
                                                            <asp:Label class="form-label mandatorylbl" ID="lblQtyAndUom" runat="server">                                                               
                                                            </asp:Label>
                                                        </div>
                                                        <div>
                                                            <asp:TextBox ID="txtQty" runat="server" CssClass="form-control mandatoryfield" AutoComplete="off"
                                                                onkeypress="return validationDecimal(this);" onkeyup="ValidateQCCheckedQty(this);" ToolTip="Enter Qty" placeholder="Enter Qty">
                                                            </asp:TextBox>
                                                            <asp:Label ID="lblQCCheckedQty" Style="color: brown; font-size: 12px;"
                                                                runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4 text-left" runat="server">
                                                        <div class="text-left">
                                                            <label class="form-label mandatorylbl">
                                                                Unit Cost
                                                            </label>
                                                        </div>
                                                        <div class="text-left">
                                                            <asp:TextBox ID="txtUnitCost" runat="server" CssClass="form-control mandatoryfield" AutoComplete="off"
                                                                onkeypress="return validationDecimal(this);" ToolTip="Enter Unit Cost" placeholder="Enter Unit Cost">
                                                            </asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-2">
                                                    </div>
                                                </div>

                                                <%--<div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-2">
                                                    </div>
                                                    <div class="col-sm-4 text-left" id="Div1" runat="server">
                                                        <div class="text-left">
                                                            <label class="form-label mandatorylbl">
                                                                Recepit Date</label>
                                                        </div>
                                                        <div>
                                                            <asp:TextBox ID="txtReceiptDate" runat="server" CssClass="form-control datepicker mandatoryfield" AutoComplete="off"
                                                                ToolTip="Enter Receipt Date" placeholder="Enter Receipt Date">
                                                            </asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4 text-left">
                                                        <div class="text-left">
                                                            <label class="form-label">
                                                                Attachement</label>
                                                        </div>
                                                        <div>
                                                            <asp:FileUpload ID="fbStockUpload" runat="server" class="form-control" Width="70%" />
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-2">
                                                    </div>
                                                </div>--%>
                                                <div class="col-sm-12 p-t-20">
                                                    <asp:LinkButton ID="btnSaveStockAvailability" runat="server" Text="Save"
                                                        OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divStockAvailability')"
                                                        OnClick="btnSaveStockAvailability_Click" CssClass="btn btn-cons btn-save AlignTop"></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="divOutputsItems" runat="server">
                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvStockAvailabilty" runat="server" AutoGenerateColumns="False"
                                                    ShowHeaderWhenEmpty="True" OnRowCommand="gvStockAvailabilty_OnRowCommand" OnRowDataBound="gvStockAvailabilty_OnRowDataBound"
                                                    EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium" DataKeyNames="SIID">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Qty" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Qty")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Unit Cost" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUnitCost" runat="server" Text='<%# Eval("UnitQuoteCost")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%-- <asp:TemplateField HeaderText="Qty In Numbers" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQtyInNumbers" runat="server" Text='<%# Eval("IM_QtyInNumbers")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Document Name" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDocumentname" runat="server" Text='<%# Eval("IM_DocumentName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Receipt Date" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblReceiptDate" runat="server" Text='<%# Eval("ReceiptDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                            HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDelete" runat="server" CommandName="DeleteIM" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                                <img src="../Assets/images/del-ec.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <div class="col-sm-12 p-t-20 text-center">
                                        <asp:LinkButton ID="btnShareInward" runat="server" Text="Share InWard"
                                            OnClick="btnShareInward_Click" CssClass="btn btn-cons btn-save btn-success"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>

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

</asp:Content>

