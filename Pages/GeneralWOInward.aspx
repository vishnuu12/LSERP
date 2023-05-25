<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" 
    CodeFile="GeneralWOInward.aspx.cs" Inherits="Pages_GeneralWOInward" %>


<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">

        function ShowAddPopUp() {
            $('#mpeWorkOrderInwardDetails').show();
            return false;
        }

        function HideAddPopUp() {
            $('#mpeWorkOrderInwardDetails').hide();
            return false;
        }

        function MakeMandatory(ele) {
            try {
                if ($(ele).is(":checked")) {
                    $(ele).closest('tr').find('[type="text"]').addClass("mandatoryfield");
                    $(ele).closest('tr').find('[type="text"]').before('<span class="textboxmandatory" style="color: red;">*</span>');
                }
                else {
                    $(ele).closest('tr').find('[type="text"]').removeClass("mandatoryfield");
                    $(ele).closest('tr').find('.textboxmandatory').remove();
                }
            } catch (er) { }
        }

        function ValidateDCQty(ele) {

            var APDC = $(ele).closest('td').find('.APDC').text();

            if ($(ele).val() != "") {
                var DCQty = parseInt($(ele).val());
                var AvailQty = parseInt($(ele).closest('tr').find('.DCAvailQty').text());
                if (DCQty > APDC && DCQty > 0) {
                    ErrorMessage('Error', 'Entered Quantity Should Not Greater Than Available Qty');
                    $(ele).val('');
                }
            }
            return false;
        }

        function checkAllItems(ele) {
            debugger;
            if ($(ele).is(":checked")) {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', true);

                $(ele).closest('table').find('[type="text"]').addClass("mandatoryfield");
                $(ele).closest('table').find('[type="text"]').before('<span class="textboxmandatory" style="color: red;">*</span>');
            }
            else {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', false);
                $(ele).closest('table').find('[type="text"]').removeClass("mandatoryfield");
                $(ele).closest('table').find('[type="text"]').closest('td').find('.textboxmandatory').remove()
            }
        }



        function MandatoryField(ele) {
            if ($(ele).is(":checked")) {
                $(ele).closest('tr').find('[type="text"]').addClass("mandatoryfield");
                $(ele).closest('tr').find('[type="text"]').before('<span class="textboxmandatory" style="color: red;">*</span>');
            }
            else {
                $(ele).closest('tr').find('[type="text"]').removeClass("mandatoryfield");
                $(ele).closest('tr').find('[type="text"]').closest('td').find('.textboxmandatory').remove()
            }
        }

        function ShareInwardDC() {
            swal({
                title: "Are you sure?",
                text: "If Yes, the inward DC Will be permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Share it!",
                closeOnConfirm: false
            }, function () {
                __doPostBack('ShareInwardDC', "");
            });
            return false;
        }

        function deleteConfirmGWOIH(GIHD) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Inward DC Will be Delete permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                __doPostBack('DeleteGWOIH', GIHD);
            });
            return false;
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
                                        <img src="../Assets/images/pages.png" alt="" width="16px" />
                                    </span>
                                    <h3 class="page-title-head d-inline-block">General Material Inward</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Purchase</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">General Material Inward</li>
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
                     

                         <div id="divRadio" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-2">
                                        <label class="form-label" style="padding-top: 7px;">
                                            Select Type
                                        </label>
                                    </div>
                                    <div class="col-sm-8">
                                        <asp:RadioButtonList ID="rblGWPONoChange" runat="server" CssClass="radio radio-success" RepeatDirection="Horizontal"
                                            AutoPostBack="true" onchange="rblChange();" OnSelectedIndexChanged="rblGWPONoChange_OnSelectedChanged"
                                            RepeatLayout="Flow">
                                            <asp:ListItem Selected="True" Value="0">PENDING</asp:ListItem>
                                            <asp:ListItem Value="1">COMPLETED</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
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
                                        <asp:TextBox ID="txtDCNumber" runat="server" CssClass="form-control  mandatoryfield"
                                            ToolTip="Enter DC Number" placeholder="Enter DC Number">
                                        </asp:TextBox>
                                            <asp:TextBox ID="txtGDCID" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
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
                                            placeholder="Enter DC Date">
                                        </asp:TextBox>
                                             <asp:Label ID="txtGWOIDA" runat="server" CssClass="form-control" Visible="false">
                                            </asp:Label>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            E-Way Bill No
                                        </label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:TextBox ID="txtEwayBillNo" runat="server" CssClass="form-control mandatoryfield"
                                            placeholder="Enter E-Way Bill No">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            DC Copy</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                         <asp:FileUpload ID="FileUp" runat="server" onchange="DocValidation(this);" TabIndex="12"
                                                CssClass="form-control mandatoryfield" Width="95%"></asp:FileUpload>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
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
                                            <asp:GridView ID="gvGeneralWorkOrderInward" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                OnRowCommand="gvGeneralWorkOrderInward_OnRowCommand"
                                                HeaderStyle-HorizontalAlign="Center" DataKeyNames="GIHD,GDCID,GWPOID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vendor Name" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVendorName" runat="server" Text='<%# Eval("VendorName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="General DC No" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGeneralDCNo" runat="server" Text='<%# Eval("DCNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="General DC Date" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGeneralDCDate" runat="server" Text='<%# Eval("DCDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Supplier DC No" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSupplierDCNo" runat="server" Text='<%# Eval("DCNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Supplier DC Date" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSupplierDCDate" runat="server" Text='<%# Eval("SDCDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Supplier E-Way Bill No" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSupplierEWayBillNo" runat="server" Text='<%# Eval("EwayBillNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Add Supplier DC" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnSupplierDC" runat="server" Visible='<%# Eval("btnVisible").ToString() == "0" ? true : false %>'  CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="SupplierDC">
                                                            <img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Add Inward" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnAdd" runat="server" Visible='<%# Eval("ShareInwardDC").ToString() == "0" ? true : false %>'  CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="Add">
                                                            <img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEdit" runat="server" Visible='<%# Eval("btnVisible").ToString() == "0" ? false : true %>' CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="EDitGWI">
                                                           <img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDelete" runat="server" Visible='<%# Eval("btnVisible").ToString() == "0" ? false : true %>'
                                                                OnClientClick='<%# string.Format("return deleteConfirmGWOIH({0});",Eval("GIHD")) %>'>
                                                            <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                    </div>
                                </div>
                            </div>
                         
                        <asp:HiddenField ID="hdnGIHD" runat="server" Value="0" />
                        <asp:HiddenField ID="hdnGDCID" runat="server" Value="0" />
                        <asp:HiddenField ID="hdnGWPOID" runat="server" Value="0" />
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

    <iframe id="ifrm" runat="server" visible="false"></iframe>

        <div class="modal" id="mpeWorkOrderInwardDetails" style="overflow-y: scroll;">
        <div style="max-width: 92%; padding-left: 10%; padding-top: 5%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="upView" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">
                                <asp:Label ID="lblheadername_p" Style="color: brown;" runat="server"></asp:Label></h4>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" onclick="HideAddPopUp();"
                                aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="docdiv" class="docdiv">
                                <div class="inner-container">
                                    <div id="Item" runat="server">
                                        <div id="divOutputsItems" runat="server">
                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvGeneralMaterialInwardDetails" runat="server"
                                                    AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    OnRowDataBound="gvGeneralMaterialInwardDetails_OnRowDataBound"
                                                    EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                    >
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Indent No" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGWONO" runat="server" Text='<%# Eval("GWONO")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="GPO No" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGPONO" runat="server" Text='<%# Eval("GPONO")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Job List" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblJobList" runat="server" Text='<%# Eval("JobList")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PO QTY" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPOQty" runat="server" Text='<%# Eval("POQty")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DC Qty" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAvailPOQty" runat="server" Text='<%# Eval("DcQuantity")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Inward Qty" HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblJobDCQty" runat="server" CssClass="APDC" Text='<%# Eval("DcQuantity")%>' Style="display: none;"></asp:Label>
                                                              
                                                                <asp:TextBox ID="txtInwardQty" onkeypress="return fnAllowNumeric(this);" onblur="return ValidateDCQty(this);" 
                                                                    CssClass="form-control" Visible='<%# Eval("DcQuantity").ToString() == "0" ? false : true %>' runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="JobDescription" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblJobDescription" runat="server" Text='<%# Eval("ServiceDescription")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Allow/UnAllow" HeaderStyle-CssClass="text-center"
                                                            ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnAllow" Visible="false" runat="server" CommandName="Allow"
                                                                    CssClass="btn btn-cons btn-success" Text="Allow"
                                                                    CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">                                                                                    
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-12 p-t-20 text-center">
                                                <asp:LinkButton ID="btnSaveGWOIDetails" runat="server" Text="Save DC" CssClass="btn btn-cons btn-save AlignTop"
                                                    OnClick="btnSaveGWOIDetails_Click"  OnClientClick="return ValidateWorkOrderInward(this);"></asp:LinkButton>
                                            </div>
                                            <div class="col-sm-12 p-t-20 text-center">
                                                <asp:Button ID="btnShareInwardDC" Text="Share DC" CssClass="btn btn-cons btn-danger" OnClientClick="ShareInwardDC();"
                                                    runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
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

