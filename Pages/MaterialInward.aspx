<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="MaterialInward.aspx.cs" Inherits="Pages_MaterialInward" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function ShowAddPopUp() {
            $('#mpeView').modal({
                backdrop: 'static'
            });
            return false;
        }

        function DCqtyvalidate(ele) {
            if (parseFloat($(ele).closest('tr').find('.POAvailableAvalibleQty').text()) < parseFloat($(ele).val())) {
                InfoMessage("DCQuantity shouldn't greater than Ordered Quantity");
                $(ele).val('');
            }
        }

        function checkAllandMakeMandatory(ele) {
            debugger;
            if ($(ele).is(":checked")) {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', true);
                $(ele).closest('tr').find('[type="file"]').addClass('mandatoryfield');
            }
            else {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', false);
                $(ele).closest('tr').find('[type="file"]').removeClass('mandatoryfield');
            }
            MakeMandatory('all');
        }
        function UpdateInwardStatus() {
            swal({
                title: "No Further Edit Once Shared.",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Save it!",
                closeOnConfirm: false
            }, function () {
                showLoader();
                document.getElementById('<%=btnSaveInwardhidden.ClientID %>').click();
            });
            return false;
        }
        function OpenTab(Name) {
            var names = ["Documents", "Item"];
            var text = document.getElementById('<%=Documents.ClientID %>');

            for (var i = 0; i < names.length; i++) {
                if (Name == names[i]) {
                    var a = text.id.replace('Documents', Name);
                    document.getElementById(a).style.display = "block";
                    document.getElementById('li' + names[i]).className = "active";
                }
                else {
                    var b = text.id.replace('Documents', names[i]);
                    document.getElementById(b).style.display = "none";
                    document.getElementById('li' + names[i]).className = "";
                }
            }
            if (Name == 'Item') {
                $('#liDocuments').removeClass('active');
                $('#liDocuments a').removeClass('active');
                $('#liItem a').addClass('active');
                $('#liItem').addClass('active');
            }
            else {
                $('#liDocuments').addClass('active');
                $('#liDocuments a').addClass('active');
                $('#liItem a').removeClass('active');
                $('#liItem').removeClass('active');
            }
            return false;
        }
        function MakeMandatory(ele) {
            try {
                if (ele == 'all') {
                    $('#ContentPlaceHolder1_gvSupplierPOItemDetails').find('[type="checkbox"]').each(function () {
                        if ($(this).closest('tr').find('.txtAvalibleQty').length > 0 && $(this).closest('tr').find('.txtAvalibleQty').val() < 1) {
                            $(this).prop('checked', false);
                            //$(this).attr('disabled', true);
                        }
                        else {
                            if ($(this).is(":checked")) {
                                $(this).closest('tr').find('[type="text"]').addClass("mandatoryfield");
                                $(this).closest('tr').find('[type="text"]').before('<span class="textboxmandatory" style="color: red;">*</span>');
                            }
                            else {
                                $(this).closest('tr').find('[type="text"]').removeClass("mandatoryfield");
                                $(this).closest('tr').find('.textboxmandatory').remove();
                            }
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

        function ShowAttachementPopUp() {
            $('#mpeAttachements').modal('show');
            return false;
        }

        function deleteConfirm(PartId) {
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
                __doPostBack('deletegvrow', PartId);
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
                                        <%--<i class="fa fa-th-large opacity-6 yellow"></i>--%>
                                        <img src="../Assets/images/pages.png" alt="" width="16px" />
                                    </span>
                                    <h3 class="page-title-head d-inline-block">Material Inward</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Purchase</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Material Inward</li>
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
                        <div id="divAdd" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Suplier Name</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlSuplierName" runat="server" CssClass="form-control" Width="70%"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlSuplierName_OnSelectIndexChanged"
                                            ToolTip="Select Supplier Name">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Location Name</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlLocationName" runat="server" ToolTip="Select Location Name"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlLocationName_OnSelectIndexChanged"
                                            Width="70%" CssClass="form-control mandatoryfield">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divAddNew" runat="server">
                            <div class="ip-div text-center p-t-10">
                                <asp:LinkButton ID="btnAddNew" runat="server" Text="Add New" OnClick="btnAddNew_Click"
                                    CssClass="btn ic-w-btn add-btn"></asp:LinkButton>
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            DC Number</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:TextBox ID="txtDCNumber" runat="server" CssClass="form-control mandatoryfield"
                                            ToolTip="Enter DC Number" placeholder="Enter DC Number">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            DC Date</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:TextBox ID="txtDCDate" runat="server" CssClass="form-control datepicker mandatoryfield"
                                            ToolTip="Enter DC Date" placeholder="Enter DC Date">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Invoice Number</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:TextBox ID="txtInVoiceNumber" runat="server" CssClass="form-control  mandatoryfield"
                                            ToolTip="Enter DC Number" placeholder="Enter Invoice Number">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Invoice Date</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:TextBox ID="txtInvoiceDate" runat="server" CssClass="form-control datepicker mandatoryfield"
                                            placeholder="Enter Invoice Date">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <%--  <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            DC Copy</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:FileUpload ID="fpDCAttach" runat="server" class="form-control" Width="70%" />
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>--%>
                                <div class="col-sm-12 p-t-20">
                                    <asp:LinkButton ID="btnSave" runat="server" Text="Save" CssClass="btn btn-cons btn-save AlignTop"
                                        OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divInput');" OnClick="btnSaveMI_Click"></asp:LinkButton>
                                    <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                        CssClass="btn btn-cons btn-danger AlignTop"></asp:LinkButton>
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
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvMaterialInward" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                OnRowCommand="gvMaterialInward_OnRowCommand" HeaderStyle-HorizontalAlign="Center"
                                                DataKeyNames="MIHID,FileName">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DC Number" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDCNumber" runat="server" Text='<%# Eval("DCNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DC Date" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDCDate" runat="server" Text='<%# Eval("DCDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="InVoice Number" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblInVoiceNumber" runat="server" Text='<%# Eval("InVoiceNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="InVoice Date" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblInVoiceDate" runat="server" Text='<%# Eval("InVoiceDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Add Attach" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnAddAttach" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="AddDocs">
                                                            <img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Add DC Qty" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnAdd" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="Add">
                                                            <img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="EditMI" Enabled='<%# Eval("BtnEnable").ToString() == "1" ? false: true%>'>
                                                           <img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnMIHID" runat="server" Value="0" />
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
    <div class="modal" id="mpeView" style="overflow-y: scroll;">
        <div style="max-width: 92%; padding-left: 2%; padding-top: 5%;">
            <div class="modal-content" style="width: 109%;">
                <asp:UpdatePanel ID="upView" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSaveMIDetail" />
                        <asp:PostBackTrigger ControlID="gvaddeditems" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">Supplier PO Details</h4>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="docdiv" class="docdiv">
                                <div class="inner-container">
                                    <ul class="nav nav-tabs lserptabs" style="display: inline-block; width: 100%; background-color: cadetblue; text-align: right; font-size: x-large; font-weight: bold; color: whitesmoke;">
                                        <li id="liItem"><a href="#Item" class="tab-content" data-toggle="tab" onclick="OpenTab('Item');">
                                            <p style="margin-left: 10px; text-align: center; color: black;">
                                                Add Items
                                            </p>
                                        </a></li>
                                        <li id="liDocuments" class="active"><a href="#Documents" class="tab-content active"
                                            data-toggle="tab" onclick="OpenTab('Documents');">
                                            <p style="margin-left: 10px; text-align: center; color: black;">
                                                Items Added
                                            </p>
                                        </a></li>
                                    </ul>
                                    <div id="Documents" runat="server">
                                        <div class="col-sm-12 p-t-10" id="div1" runat="server">
                                            <div class="col-sm-4">
                                            </div>
                                            <div class="col-sm-4 text-left">
                                                <asp:LinkButton ID="btnAddnewItems" runat="server" Text="Add New" OnClientClick="return OpenTab('Item');"
                                                    CssClass="btn ic-w-btn add-btn">
                                                </asp:LinkButton>
                                            </div>
                                            <div class="col-sm-4 text-left">
                                            </div>
                                        </div>
                                        <div id="div2" runat="server">
                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvaddeditems" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium" OnRowCommand="gvaddeditems_OnRowCommand"
                                                    DataKeyNames="SPOID,SPODID,MIDID,FileName">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="RFPNo" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRFPNo" runat="server" Text='<%# Eval("RFPNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Supplier Order Number" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblsponumber" runat="server" Text='<%# Eval("SPONumber")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MRN Number" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                            HeaderStyle-HorizontalAlign="left">
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
                                                        <asp:TemplateField HeaderText="Grade Name" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
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
                                                        <asp:TemplateField HeaderText="Quote Cost" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQuoteCost" runat="server" Text='<%# Eval("Cost")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Measurement" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMeasurement" runat="server" Text='<%# Eval("Measurment").ToString().Replace(",", "<br/>")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Material Type Name" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTypeName" runat="server" Text='<%# Eval("TypeName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--  <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQuantity" CssClass="lblqty" runat="server" Text='<%# Eval("Quantity")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="DC Quantity" HeaderStyle-Width="10%" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDCQuantity" runat="server" CssClass="lbldcqty" Text='<%# Eval("DCQuantity")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PO Qty" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRW" Width="50px" runat="server" Text='<%# Eval("ReqWeight")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Unit Cost" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUnitCost" runat="server" Text='<%# Eval("UnitCost")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="View Attach" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnView" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                    CommandName="ViewAttachFile">
                                                            <img src="../Assets/images/view.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-12 p-t-20 text-center">
                                                <asp:LinkButton ID="btnSaveInwardhidden" runat="server" OnClick="btnsaveshare_Click"
                                                    Style="display: none;"></asp:LinkButton>
                                                <asp:LinkButton ID="btnSaveInward" runat="server" Text="Save and Share" OnClientClick="return UpdateInwardStatus();"
                                                    CssClass="btn btn-cons btn-save AlignTop"></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="Item" runat="server">
                                        <div class="col-sm-12 p-t-10" id="divSPONumber" runat="server">
                                            <div class="col-sm-2">
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="form-label mandatorylbl">
                                                    SPO Number
                                                </label>
                                            </div>
                                            <div class="col-sm-4 text-left">
                                                <asp:DropDownList ID="ddlSPONumber" runat="server" ToolTip="Select SPO Number" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlSPONumber_OnSelectIndexChanged" CssClass="form-control">
                                                    <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-4 text-left">
                                            </div>
                                        </div>
                                        <div id="divOutputsItems" runat="server" visible="false">
                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvSupplierPOItemDetails" runat="server" AutoGenerateColumns="False"
                                                    ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                    OnRowDataBound="gvSupplierPOItemDetails_OnRowDataBound" DataKeyNames="SPOID,SPODID,RJMIDID">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkall" runat="server" onclick="return checkAllandMakeMandatory(this);" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkitems" runat="server" AutoPostBack="false" onclick="return MakeMandatory(this);" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="RFPNo" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
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
                                                        <asp:TemplateField HeaderText="Grade Name" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
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
                                                        <asp:TemplateField HeaderText="Quote Cost" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQuoteCost" runat="server" Text='<%# Eval("Cost")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Measurement" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMeasurement" runat="server" Text='<%# Eval("Measurment").ToString().Replace(",", "<br/>")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Material Type Name" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTypeName" runat="server" Text='<%# Eval("TypeName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="PO Qty" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRW" CssClass="lblReqWeight" Width="50px" runat="server" Text='<%# Eval("ReqWeight")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="DC Qty" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="10%" ItemStyle-Width="10%"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <%--Text='<%# Eval("DCQuantity")%>'--%>
                                                                <asp:TextBox ID="txtDCQuantity" Text='<%# Eval("AvailableQty")%>' runat="server"
                                                                    CssClass="form-control txtAvalibleQty" onkeypress="return validationDecimal(this);" onblur="DCqtyvalidate(this);"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PO Remaining Qty" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <%--Text='<%# Eval("DCQuantity")%>'--%>
                                                                <asp:Label ID="lblPORemainngQty" Text='<%# Eval("AvailableQty")%>' runat="server"
                                                                    CssClass="POAvailableAvalibleQty"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DC Inwarded Quantity" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQuantity" CssClass="lblqty" runat="server" Text='<%# Eval("DCQty")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Add" HeaderStyle-Width="15%" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:FileUpload ID="fpDCMRNAttach" runat="server" TabIndex="12" CssClass="form-control"
                                                                    onchange="DocValidation(this);" ClientIDMode="Static" Width="95%"></asp:FileUpload>
                                                                <asp:Label ID="Label1" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtRemarks" runat="server"
                                                                    CssClass="form-control"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-12 p-t-20 text-center">
                                                <asp:LinkButton ID="btnSaveMIDetail" runat="server" Text="Save" CssClass="btn btn-cons btn-save AlignTop"
                                                    OnClick="btnSaveMIDetail_Click" Visible="false" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_Item');"></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hdnSPODID" runat="server" Value="0" />
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

    <div class="modal" id="mpeAttachements" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSaveAttachements" />
                        <asp:PostBackTrigger ControlID="gvAttachments" />

                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD"></h4>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="divDocuments" class="docdiv">
                                <div class="inner-container">
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-3">
                                            <label class="form-label mandatorylbl">
                                                Type Name
                                            </label>
                                        </div>
                                        <div class="col-sm-9">
                                            <asp:DropDownList ID="ddlTypeName" runat="server" TabIndex="1" ToolTip="Select Attachement type"
                                                CssClass="form-control mandatoryfield">
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
                                            <asp:FileUpload ID="fAttachment" runat="server" TabIndex="12" CssClass="form-control mandatoryfield"
                                                onchange="DocValidation(this);" ClientIDMode="Static" Width="95%"></asp:FileUpload>
                                            <asp:Label ID="lblAttachementFileName" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <asp:GridView ID="gvAttachments" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                            OnRowCommand="gvAttachments_OnRowCommand"
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
                                                        <asp:Label ID="lblAttachementTypeName_V" runat="server" Text='<%# Eval("TypeName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDescription_V" runat="server" Text='<%# Eval("Description")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFileName_V" runat="server" Visible="false" Text='<%# Eval("FileName")%>'></asp:Label>
                                                        <%--     <asp:ImageButton ID="imgbtnView" runat="server" ImageUrl="" 
                                                            Width="20px" Height="20px" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                        </asp:ImageButton>--%>
                                                        <asp:LinkButton ID="lbtnView" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            CommandName="ViewDocs"><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnDelete" runat="server" OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("AttachementID")) %>'
                                                            CommandName="DeleteAttachement">
                                                           <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-3">
                                        </div>
                                        <div class="col-sm-3">
                                            <div class="form-group">
                                                <asp:LinkButton Text="Submit" CssClass="btn btn-cons btn-save  AlignTop" ID="btnSaveAttachements"
                                                    runat="server" OnClientClick="return Mandatorycheck('divDocuments');"
                                                    OnClick="btnSaveAttchement_Click" />
                                            </div>
                                        </div>
                                        <div class="col-sm-3">
                                        </div>
                                        <div class="col-sm-3">
                                        </div>
                                    </div>

                                </div>
                                <div class="modal-footer">
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
                        <asp:HiddenField ID="hdnPoqty" runat="server" Value="0" />
                        <asp:HiddenField ID="hdnAttachementFlag" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <iframe id="ifrm" runat="server" visible="false"></iframe>
</asp:Content>
