<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="CustomerPO.aspx.cs" Inherits="Pages_CustomerPO" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function deleteConfirm(PODID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Customer PO Details Delete permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                __doPostBack('deletegvrow', PODID);
            });
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
            MakefieldsMandatory('all');
        }
        function MakefieldsMandatory(ele) {
            try {
                if (ele == 'all') {
                    $('#ContentPlaceHolder1_gvCustomerPOdetails').find('[type="checkbox"]').each(function () {
                        if ($(this).is(":checked")) {
                            $(this).closest('tr').find('[type="text"]').addClass("mandatoryfield");
                            $(this).closest('tr').find('[type="text"]').after('<span class="textboxmandatory" style="color: red;">*</span>');
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
                        $(ele).closest('tr').find('[type="text"]').after('<span class="textboxmandatory" style="color: red;">*</span>');
                    }
                    else {
                        $(ele).closest('tr').find('[type="text"]').removeClass("mandatoryfield");
                        $(ele).closest('tr').find('.textboxmandatory').remove();
                    }
                }
            } catch (er) { }
        }
        $(document).ready(function () {
            $('#<%=txtPODate.ClientID %>').datepicker({
                format: 'dd/mm/yyyy',
                startDate: new Date('2019-12-5'),
                endDate: new Date('2020-7-12')
            });
            $('#<%=gvCustomerPOdetails.ClientID %>').find('input:text[id$="txtDateOfDelivery"]').datepicker({
                format: 'dd/mm/yyyy'
            });
        });

        function ShowAddPopUp() {
            $('#mpeView').modal({
                backdrop: 'static'
            });
            MakefieldsMandatory('all');
            return false;
        }

        function HideCustomerPoPopUp() {
            $('#mpeView').hide();
            $('div').removeClass('modal-backdrop');
            return false;
        }

        function ShowViewPopUp() {
            $('#mpeShowDocument').modal({
                backdrop: 'static'
            });
            return false;
        }

        function ValidateCustomerPO() {
            if (Mandatorycheck('ContentPlaceHolder1_divInput')) {
                var PODate = $('#<%=txtPODate.ClientID %>').val();
                var PO = new Data(PODate);
                var CurrentDate = new Date();
                // var FDparts = FromDate.split("/");
                // var TDParts = CurrentDate.split("/");
                // var PO = new Date(FDparts[1] + "/" + FDparts[0] + "/" + FDparts[2]);
                // var CD = new Date(TDParts[1] + "/" + TDParts[0] + "/" + TDParts[2]);



                if (PO > CurrentDate) {
                    $('#<%=txtPODate.ClientID %>').notify('The PO Date Should Not Be greater Than Current Date.', { arrowShow: true, position: 'r,r', autoHide: true });
                    return false;
                }
                else {
                    return true;
                }
            }
            else {
                return false;
            }
        }
        function ViewPOCopy(RowIndex) {
            __doPostBack("ViewPOCopy", RowIndex);
        }

        function ViewPOCopyWithoutPrice(RowIndex) {
            __doPostBack("ViewPOCopyWithoutPrice", RowIndex);
        }

        <%--function ClearCustomerPODetails() {
            $('#<%=ddlitemname.ClientID %>').val('').trigger("chosen:updated");;
            $('#<%=txtDateOfDelivery.ClientID %>')[0].value = "";
            $('#<%=txtUnitPrice.ClientID %>')[0].value = "";
            $('#<%=txt_quantity.ClientID %>')[0].value = "";
            return false;
        }--%>
        function SharePO(POHID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, Once PO Shared No Further Edit",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Share it!",
                closeOnConfirm: false
            }, function () {
                __doPostBack('SharePO', POHID);
            });
            return false;
        }

        function DeleteCustomerPO(POHID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Customer PO  Delete permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                __doPostBack('deleteCustomerPO', POHID);
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
                                    <h3 class="page-title-head d-inline-block">Customer PO</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Sales</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Customer PO</li>
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
            <%--ChildrenAsTriggers="true"--%>
            <asp:UpdatePanel ID="upDocumenttype" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                    <asp:PostBackTrigger ControlID="btndownloadPDF" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" runat="server">
                            <div class="text-center">
                                <asp:LinkButton ID="btnAddNew" runat="server" Text="Add New PO" OnClick="btnAddNew_Click"
                                    CssClass="btn btn-success add-emp"></asp:LinkButton>
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Po Ref No</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:TextBox ID="txtRefNo" runat="server" Width="70%" CssClass="form-control mandatoryfield"
                                            ToolTip="Enter Po Ref No" placeholder="Enter PO Ref No">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Customer Name</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="true" CssClass="form-control mandatoryfield"
                                            OnSelectedIndexChanged="ddlCustomerName_SelectIndexChanged" Width="70%" ToolTip="Select Enquiry Number">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Enquiry Number
                                        </label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlEnquiryNumber" runat="server" AutoPostBack="true" CssClass="form-control mandatoryfield"
                                            OnSelectedIndexChanged="ddlEnquiryNumber_SelectIndexChanged" Width="70%" ToolTip="Select Enquiry Number">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Offer Number
                                        </label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlOfferNumber" runat="server" AutoPostBack="true" CssClass="form-control mandatoryfield"
                                            Width="70%" OnSelectedIndexChanged="ddlOfferNumber_OnSelectedIndexChanged" ToolTip="Select Enquiry Number">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2 text-center" id="divPDF" runat="server" visible="false">
                                        <asp:LinkButton ID="btndownloadPDF" runat="server" OnClick="btndownloadPDF_Click">  <img src="../Assets/images/pdf.png" /></asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            PO Date</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:TextBox ID="txtPODate" runat="server" Width="70%" CssClass="form-control datepicker mandatoryfield"
                                            AutoComplete="off" ToolTip="Enter PO Date" placeholder="Enter PO Date">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            PO Copy</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:FileUpload ID="fPoCopy" onchange="DocValidation(this);" runat="server" class="form-control mandatoryfield" />
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            PO Copy Without Price</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:FileUpload ID="fPoCopyWithoutPrice" onchange="DocValidation(this);" runat="server"
                                            class="form-control mandatoryfield" />
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-20">
                                    <asp:LinkButton ID="btnSave" runat="server" Text="Save" CssClass="btn btn-cons btn-save AlignTop"
                                        OnClientClick="return ValidateCustomerPO();" OnClick="btnSave_Click"></asp:LinkButton>
                                    <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-cons btn-danger AlignTop"
                                        OnClick="btnCancel_Click"></asp:LinkButton>
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
                                                    <asp:Label ID="lbltitle" runat="server" Text="Customer PO Details"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvCustomerPo" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                HeaderStyle-HorizontalAlign="Center" OnRowCommand="gvCustomerPo_RowCommand" OnRowDataBound="gvCustomerPo_OnRowDataBound" DataKeyNames="POHID,EnquiryNumber,PoCopy,PoCopyWithoutPrice,EODID,POSharedStatus">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Customer Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProspectName" runat="server" Text='<%# Eval("ProspectName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Customer Enquiry Number">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCustomerEnquiryNumber" runat="server" Text='<%# Eval("CustomerEnquiryNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PO Ref No" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDocumentType" runat="server" Text='<%# Eval("PORefNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PO Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPODate" runat="server" Text='<%# Eval("PODate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PO Created Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPoCreatedDate" runat="server" Text='<%# Eval("POCreatedDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Offer Number">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOfferNumber" runat="server" Text='<%# Eval("OfferNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PoCopy" ItemStyle-CssClass="text-center" HeaderStyle-Width="10%"
                                                        ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnPoCopy" runat="server" OnClientClick='<%# string.Format("return ViewPOCopy({0});",((GridViewRow) Container).RowIndex) %>'
                                                                CommandName="ViewPOCopy"><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PoCopy Without Price" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnPoCopyWithoutPrice" runat="server" OnClientClick='<%# string.Format("return ViewPOCopyWithoutPrice({0});",((GridViewRow) Container).RowIndex) %>'
                                                                CommandName="ViewPoCopyWithoutPrice"><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Add PO Item Details" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnAdd" runat="server" ToolTip="Add" CommandName="AddPODetails"
                                                                CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                                <img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-CssClass="text-center" HeaderStyle-Width="10%"
                                                        ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="EditCustomerPO">
                                                           <img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Share PO" ItemStyle-CssClass="text-center" HeaderStyle-Width="10%"
                                                        ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnSharePO" runat="server" OnClientClick='<%# string.Format("return SharePO({0});",Eval("POHID")) %>'
                                                                CommandName="SharePO">
                                                           <img src="../Assets/images/icon_update.png" width="40px" alt="" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete PO" ItemStyle-CssClass="text-center" HeaderStyle-Width="10%"
                                                        ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnDeletePO" runat="server"
                                                                OnClientClick='<%# string.Format("return DeleteCustomerPO({0});",Eval("POHID")) %>'
                                                                CommandName="SharePO">
                                                           <img src="../Assets/images/del-ec.png" width="40px" alt="" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnPOHID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnEODID" runat="server" Value="0" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal" id="mpeView" style="padding-left: 0px !important;">
                        <div class="modal-dialog" style="max-width: 91% !important;">
                            <div class="modal-content">
                                <asp:UpdatePanel ID="upView" runat="server" UpdateMode="Always">
                                    <Triggers>
                                    </Triggers>
                                    <ContentTemplate>
                                        <div class="modal-header">
                                            <h4 id="popuplbl" runat="server" class="modal-title ADD">Customer PO Details</h4>
                                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                                ×</button>
                                        </div>
                                        <div class="modal-body" style="padding: 0px;">
                                            <div id="detailsdiv" runat="server">
                                                <%-- <div class="inner-container">
                                                    <div class="col-sm-12 p-t-10">
                                                        <div class="col-sm-3">
                                                            <label class="form-label mandatorylbl">
                                                                Item Name
                                                            </label>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList ID="ddlitemname" runat="server" TabIndex="1" ToolTip="Select Attachement type"
                                                                OnSelectedIndexChanged="ddlitemname_SelectIndexChanged" CssClass="form-control mandatoryfield">
                                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-3">
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-12 p-t-10">
                                                        <div class="col-sm-3">
                                                            <label class="form-label mandatorylbl">
                                                                Quantity
                                                            </label>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txt_quantity" runat="server" TabIndex="6" placeholder="Enter Item Quantity"
                                                                ToolTip="Enter Item Quantity" CssClass="form-control mandatoryfield" MaxLength="300"
                                                                autocomplete="nope"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-3">
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-12 p-t-10">
                                                        <div class="col-sm-3">
                                                            <label class="form-label mandatorylbl">
                                                                Unit Price
                                                            </label>
                                                        </div>
                                                        <div class="col-sm-6">
                                                            <asp:TextBox ID="txtUnitPrice" runat="server" TabIndex="6" placeholder="Enter Unit Price"
                                                                ToolTip="Enter Unit Price" CssClass="form-control mandatoryfield" autocomplete="nope"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-3">
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-12 p-t-10">
                                                        <div class="col-sm-3">
                                                            <label class="form-label mandatorylbl">
                                                                Date Of Delivery</label>
                                                        </div>
                                                        <div class="col-sm-6 text-left">
                                                            <asp:TextBox ID="txtDateOfDelivery" runat="server" CssClass="form-control datepicker mandatoryfield"
                                                                ToolTip="Enter Date Of Delivery" placeholder="Enter Date Of Delivery">
                                                            </asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-3">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="modal-footer">
                                                    <div class="col-sm-12 p-t-10">
                                                        <div class="col-sm-3">
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <div class="form-group">
                                                                <asp:LinkButton Text="Submit" CssClass="btn btn-cons btn-save  AlignTop" ID="btndetails"
                                                                    OnClick="btndetails_click" runat="server" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_detailsdiv');" />
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-3">
                                                            <asp:LinkButton Text="Cancel" CssClass="btn btn-cons btn-save  AlignTop" ID="btnCancelAttachements"
                                                                runat="server" OnClientClick="return ClearCustomerPODetails();" />
                                                        </div>
                                                        <div class="col-sm-3">
                                                        </div>
                                                    </div>
                                                </div>--%>
                                                <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                    <asp:GridView ID="gvCustomerPOdetails" runat="server" AutoGenerateColumns="False"
                                                        ShowHeaderWhenEmpty="True" OnRowCommand="gvCustomerPOdetails_OnRowCommand" EmptyDataText="No Records Found"
                                                        CssClass="table table-hover table-bordered medium" DataKeyNames="PODID,EDID,DDID">
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
                                                                    <asp:CheckBox ID="chkitems" runat="server" AutoPostBack="false" Checked='<%# Eval("Checked").ToString() != "0" ? true: false%>'
                                                                        onclick="return MakefieldsMandatory(this);" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Item Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblitemname" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Tag No" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTagNo" runat="server" Text='<%# Eval("ItemTagNo")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Drawing Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblDrawingname" runat="server" Text='<%# Eval("DrawingName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblitemsize" runat="server" Text='<%# Eval("ItemSize")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQuantity" runat="server" Style="color: brown;" Text='<%# Eval("EnquiryItemQty")%>'></asp:Label>
                                                                    <asp:TextBox ID="txtqty" runat="server" CssClass="form-control" Text='<%# Eval("Quantity")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Unit Price" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtunitprice" runat="server" CssClass="form-control" Text='<%# Eval("UnitPrice")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Date Of Delivery" ItemStyle-HorizontalAlign="left"
                                                                HeaderStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtDateOfDelivery" runat="server" CssClass="form-control datepicker"
                                                                        Text='<%# Eval("DateOfDelivery")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnEdit_itemdetails" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                        CommandName="EditItem">
                                                           <img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnDelete_itemdetails" runat="server" OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("PODID")) %>'
                                                                        CommandName="DeleteItem">
                                                           <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-4">
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="form-group">
                                                            <asp:LinkButton Text="Submit" CssClass="btn btn-cons btn-save  AlignTop" ID="btndetails"
                                                                OnClick="btndetails_click" runat="server" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_gvCustomerPOdetails');" />
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:HiddenField ID="hdnPODID" runat="server" Value="0" />
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
                                        <asp:HiddenField ID="PODID" runat="server" />
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
