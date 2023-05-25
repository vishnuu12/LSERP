<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="StaffAllocationByItem.aspx.cs" Inherits="Pages_StaffAllocationByItem" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function ShowItemlistPopUp() {
            $('#mpeitemlist').modal('show');
            return false;
        }
        function HideItemlistPopUp() {
            $('#mpeitemlist').modal('hide');
            return false;
        }
        //function ValidateItem() {
        //    if ($('#ContentPlaceHolder1_gvItemnamelist').find('[type="checkbox"]:checked').length > 0) {
        //        return true;
        //    }
        //    else {
        //        ErrorMessage('Error', 'No Item Selected');
        //        hideLoader();
        //        return false;
        //    }
        //}

        function updateOverAllItemStatus(EDID) {
            swal({
                title: "Are you sure?",
                text: "If Yes,  No Further Modification Once Shared",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Share it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('updateOverAllItemStatus', EDID);
            });
            return false;
        }

    </script>
    <style type="text/css">
        .staffdetails span {
            color: brown;
            padding-left: 20%;
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
                                    <h3 class="page-title-head d-inline-block">Staff Allocation By Item</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Design</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Staff Allocation By Item</li>
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
            <asp:UpdatePanel ID="upStaffAssignment" runat="server">
                <Triggers>
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" class="ip-div text-center input_section" runat="server" visible="false">
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
                        </div>

                        <div id="divInput" runat="server" visible="false">
                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                <asp:GridView ID="gvProcessList" runat="server" Width="100%" AutoGenerateColumns="false"
                                    ShowFooter="false" ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found"
                                    CssClass="table table-bordered table-hover no-more-tables"
                                    OnRowCommand="gvProcessList_OnRowCommand" OnRowDataBound="gvProcessList_OnRowDataBound"
                                    DataKeyNames="ProcessID">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SNo" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <%#Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Process Name" HeaderStyle-CssClass="text-left"
                                            ItemStyle-CssClass="text-left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProcess" runat="server" Text='<%# Eval("ProcessName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Employee Name" HeaderStyle-CssClass="text-left"
                                            ItemStyle-CssClass="text-left">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlEmployeeName" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dead Line" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDeadLineDate" runat="server" CssClass="form-control datepicker"
                                                    AutoComplete="off" placeholder="Enter Date"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Assign Item" ItemStyle-CssClass="text-center"
                                            HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnViewEnqAttch" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                    CommandName="AssignItem"><img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                <asp:LinkButton ID="btnUpdate" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                    CommandName="UpdateProcess"><img src="../Assets/images/icon_update.png" width="30px" /></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
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
                                        <div class="staffdetails">
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-6">
                                                    <label class="form-label" style="display: contents;">DRAFTING</label>
                                                    <asp:Label ID="lblDrafterName" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-6">
                                                    <label class="form-label" style="display: contents;">FEA</label>
                                                    <asp:Label ID="lblFEAName" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-6">
                                                    <label class="form-label" style="display: contents;">QUALITY</label>
                                                    <asp:Label ID="lblQualityname" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-6">
                                                    <label class="form-label" style="display: contents;">REFRACTORY</label>
                                                    <asp:Label ID="lblrefractoryname" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-6">
                                                    <label class="form-label" style="display: contents;">HEAT TREATMENT</label>
                                                    <asp:Label ID="lblheattreatmentname" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-6">
                                                    <label class="form-label" style="display: contents;">PAINTING</label>
                                                    <asp:Label ID="lblpaintingname" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvItemStaffDetails" runat="server" Width="100%" AutoGenerateColumns="false"
                                                ShowFooter="false" ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" OnRowDataBound="gvItemStaffDetails_OnRowDataBound"
                                                CssClass="table table-bordered table-hover no-more-tables" DataKeyNames="EDID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Name" HeaderStyle-CssClass="text-left"
                                                        ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Design 1" HeaderStyle-CssClass="text-left"
                                                        ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDesign1" runat="server" Text='<%# Eval("Design1") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Design 2" HeaderStyle-CssClass="text-left"
                                                        ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDesign2" runat="server" Text='<%# Eval("Design2") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Design 3" HeaderStyle-CssClass="text-left"
                                                        ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDesign3" runat="server" Text='<%# Eval("Design3") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Design 4" HeaderStyle-CssClass="text-left"
                                                        ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDesign4" runat="server" Text='<%# Eval("Design4") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Update" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnUpdate" runat="server" OnClientClick='<%# string.Format("return updateOverAllItemStatus({0});",Eval("EDID")) %>'>
                                                                <img src="../Assets/images/icon_update.png" width="30px" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Drafting" HeaderStyle-CssClass="text-left"
                                                        ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDrafting" runat="server" Text='<%# Eval("Drafting") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="FEA" HeaderStyle-CssClass="text-left"
                                                        ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblfea" runat="server" Text='<%# Eval("FEA") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quality" HeaderStyle-CssClass="text-left"
                                                        ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblquality" runat="server" Text='<%# Eval("Quality") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Refractory" HeaderStyle-CssClass="text-left"
                                                        ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrefractory" runat="server" Text='<%# Eval("Refractory") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Heat Tratement" HeaderStyle-CssClass="text-left"
                                                        ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblheattratment" runat="server" Text='<%# Eval("HeatTratement") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Painting" HeaderStyle-CssClass="text-left"
                                                        ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPainting" runat="server" Text='<%# Eval("Painting") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                </Columns>
                                            </asp:GridView>
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

    <div class="modal" id="mpeitemlist" style="padding-left: 0px !important;">
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
                                <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                    <asp:GridView ID="gvItemnamelist" runat="server" AutoGenerateColumns="False"
                                        ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found"
                                        OnRowDataBound="gvItemnamelist_OnRowDataBound" CssClass="table table-hover table-bordered medium" DataKeyNames="EDID">
                                        <Columns>
                                            <asp:TemplateField HeaderText="" ControlStyle-CssClass="nosort" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <%--  <asp:CheckBox ID="chkall" runat="server" onclick="return checkAllandMakeMandatory(this);" />--%>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkitems" runat="server" AutoPostBack="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblitemname" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuantity" runat="server" Style="color: brown;" Text='<%# Eval("Quantity")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <%--OnClientClick="return ValidateItem();"--%>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4">
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="form-group">
                                            <asp:LinkButton Text="Save" CssClass="btn btn-cons btn-success" ID="btndetails"
                                                OnClick="btnSave_Click" runat="server" />
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

</asp:Content>


