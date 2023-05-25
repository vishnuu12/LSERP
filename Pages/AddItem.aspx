<%@ Page Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="AddItem.aspx.cs" Inherits="Pages_AddItem" ClientIDMode="Predictable" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="JavaScript" type="text/javascript">
        //$(document).ready(function () {
        //    try {
        //        $(document).on("click", ".btnAddNew", function () {
        //            debugger;
        //            $('.detailsdiv').show();
        //            $('.output_section').hide();
        //            $('.input_section').hide();
        //            return false;
        //        });
        //        $(document).on("click", ".btncancel", function () {
        //            $('.detailsdiv').hide();
        //            $('.output_section').show();
        //            $('.input_section').show();
        //            return false;
        //        });
        //        //                $(document).on("change", ".chckspecification", function () {
        //        //                    showspecdiv();
        //        //                });
        //    } catch (er) { }
        //});

        function ShareItem() {
            swal({
                title: "Are you sure?",
                text: "If Yes, Once Shared No Further Modification",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Share it!",
                closeOnConfirm: false
            }, function () {
                __doPostBack('ShareItem', '');
            });
        }

        function ShowViewPopUp() {
            $('#mpeView').modal({
                backdrop: 'static'
            });
            return false;
        }

        function rblTagNoItemCodeMatCode() {
            var val = $('#ContentPlaceHolder1_rblTagNoItemCodeMatCode').find('[type="radio"]:checked').val();
            if (val == "None") {
                $('#ContentPlaceHolder1_txt_tagno').val('NA');
            }
        }

        //        function showspecdiv() {
        //            try {
        //                debugger;
        //                if ($('.chckspecification').find('[type="checkbox"]').is(':checked')) {
        //                    $('.specificationdiv').show();
        //                    $('.reqtxt').each(function () {
        //                        $(this).addClass("mandatoryfield");
        //                    });
        //                }
        //                else {
        //                    $('.specificationdiv').hide();
        //                    $('.reqtxt').each(function () {
        //                        $(this).removeClass("mandatoryfield");
        //                    });
        //                }
        //            } catch (er) { }
        //        }
        //function showdetails() {
        //    debugger;
        //    $('.btnAddNew').trigger('click');
        //    showspecdiv();
        //}

        //function ClearValues() {
        //    $('textarea').val('');
        //}

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
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                    <h3 class="page-title-head d-inline-block">Add Item</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Design</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Add Item</li>
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
            <asp:UpdatePanel ID="upQuote" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlEnquiryNumber" EventName="SelectedIndexChanged" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div>
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
                            <div class="ip-div text-center input_section" id="divAdd" runat="server" visible="false">
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
                            <div class="col-sm-12 text-center p-t-10" id="divbtngroup" runat="server" visible="false">
                                <asp:LinkButton ID="btnAddNew" runat="server" Text="Add Item" CssClass="btn btn-success add-emp btnAddNew"
                                    OnClick="btnAddNew_Click" Style="margin: 5px;"></asp:LinkButton>
                                <asp:LinkButton ID="btnshowdocs" runat="server" Text="View Attachments" OnClick="btnshowdocs_Click"
                                    CssClass="btn btn-success btnshowdocs" Style="margin: 5px;">
                                </asp:LinkButton>
                                <asp:LinkButton ID="btnshowitems" runat="server" Text="View Items" OnClick="btnshowitems_Click"
                                    CssClass="btn btn-success btnshowitems" Style="margin: 5px;">
                                </asp:LinkButton>
                            </div>

                            <div id="divdetails" class="detailsdiv ip-div text-center" runat="server" visible="false">
                                <div class="inner-container">
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-2 text-right">
                                            <label class="form-label mandatorylbl">
                                                Item Name</label>
                                        </div>
                                        <div class="col-sm-4 text-left">
                                            <asp:DropDownList ID="ddlitemname" runat="server" TabIndex="1" ToolTip="Select Attachement type"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlitemname_OnSelectIndexChanged" CssClass="form-control mandatoryfield">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --" />
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2 text-left">
                                            <asp:CheckBox ID="chckStandarditem" runat="server" AutoPostBack="true" OnCheckedChanged="chckStandarditem_OnCheckedChanged" /><label class="form-label" style="width: 70% !important; float: initial; margin-left: 5px;">Standard Item</label>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-2 text-right">
                                            <label class="form-label mandatorylbl">
                                                Size</label>
                                        </div>
                                        <div class="col-sm-4 text-left">
                                            <asp:DropDownList ID="ddlsize" runat="server" ToolTip="Select Attachement type" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlsize_OnSelectIndexChanged" CssClass="form-control mandatoryfield">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --" />
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4">
                                            <asp:RadioButtonList ID="rblTagNoItemCodeMatCode" runat="server"
                                                onchange="rblTagNoItemCodeMatCode();" CssClass="radio radio-success"
                                                RepeatDirection="Horizontal">
                                                <asp:ListItem Text=" Tag No" Value="TN"></asp:ListItem>
                                                <asp:ListItem Text="Item Code" Value="IC"></asp:ListItem>
                                                <asp:ListItem Text="Material Code" Value="MC"></asp:ListItem>
                                                <asp:ListItem Text="None" Value="None" Selected="True"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="col-sm-4 text-left">
                                            <asp:TextBox ID="txt_tagno" runat="server" TabIndex="6" placeholder="Enter Code"
                                                ToolTip="Enter Tag no" TextMode="MultiLine" CssClass="form-control mandatoryfield" MaxLength="300"
                                                autocomplete="nope" />
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-2 text-right">
                                            <label class="form-label">
                                                Description</label>
                                        </div>
                                        <div class="col-sm-4 text-left">
                                            <asp:TextBox ID="txt_description" runat="server" TabIndex="6" placeholder="Enter Item Description"
                                                ToolTip="Enter Attachment Description" TextMode="MultiLine" CssClass="form-control"
                                                MaxLength="300" autocomplete="nope" />
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-2 text-right">
                                            <label class="form-label">
                                                Revision Number</label>
                                        </div>
                                        <div class="col-sm-4 text-left">
                                            <asp:Label ID="lblrevision" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-2 text-right">
                                            <label class="form-label mandatorylbl">
                                                Quantity</label>
                                        </div>
                                        <div class="col-sm-4 text-left">
                                            <asp:TextBox ID="txt_quantity" runat="server" TabIndex="6" placeholder="Enter Item Quantity"
                                                ToolTip="Enter Attachment Description" TextMode="MultiLine" CssClass="form-control mandatoryfield"
                                                MaxLength="300" autocomplete="nope" />
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4">
                                        </div>
                                        <div class="col-sm-4 text-left">
                                            <asp:CheckBox ID="chckspecification" Style="display: none;" class="chckspecification"
                                                runat="server" /><label class="form-label" style="width: 70% !important; float: initial; margin-left: 5px;">Item Specifications</label>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                </div>
                                <div id="specdiv" runat="server" class="specificationdiv">
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-2 text-right">
                                            <label class="form-label mandatorylbl">
                                                Pressure
                                            </label>
                                            <span style="color: coral;"></span>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txt_pressure" runat="server" TabIndex="6" placeholder="Enter Item Pressure"
                                                ToolTip="Enter Item Pressure" TextMode="MultiLine" CssClass="form-control mandatoryfield"
                                                MaxLength="300" autocomplete="nope" />
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:DropDownList ID="ddlPresureUnits" runat="server" ToolTip="Select Presure Units"
                                                CssClass="form-control mandatoryfield">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --" />
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-2 text-right">
                                            <label class="form-label mandatorylbl">
                                                Temperature</label><span style="color: coral;">(Deg. C)</span>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txt_temperature" runat="server" TabIndex="6" placeholder="Enter Item Temperature"
                                                ToolTip="Enter Item Temperature" TextMode="MultiLine" CssClass="form-control mandatoryfield"
                                                MaxLength="300" autocomplete="nope" />
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4" style="text-align: end;">
                                            <label class="form-label mandatorylbl">
                                                <%--    <asp:RadioButtonList ID="rblItemMovement" runat="server" CssClass="radio radio-success"
                                                    RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Axizl Extansion" Value="AX"></asp:ListItem>
                                                    <asp:ListItem Text="Axial Compression" Value="AC"></asp:ListItem>
                                                    <asp:ListItem Text="Lateral & Angular" Value="LA"></asp:ListItem>
                                                </asp:RadioButtonList>--%>
                                                Movement
                                            </label>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txt_movement" runat="server" TabIndex="6" placeholder="Enter Item Movement"
                                                ToolTip="Enter Item Movement" TextMode="MultiLine" CssClass="form-control mandatoryfield"
                                                MaxLength="300" autocomplete="nope" />
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-2 text-right">
                                            <label class="form-label">
                                                Material Warning</label>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txt_matrlwarning" runat="server" TabIndex="6" placeholder="Enter Material Warning"
                                                ToolTip="Enter Material Warning" TextMode="MultiLine" CssClass="form-control"
                                                MaxLength="300" autocomplete="nope" />
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-2 text-right">
                                            <label class="form-label mandatorylbl">
                                                Location</label>
                                        </div>
                                        <div class="col-sm-4">
                                            <%--  <asp:DropDownList ID="ddlLocation" runat="server" TabIndex="1" ToolTip="Select Location"
                                                CssClass="form-control reqtxt">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --" />
                                            </asp:DropDownList>--%>
                                            <asp:TextBox ID="txtLocation" runat="server" TabIndex="6" placeholder="Enter Location"
                                                TextMode="MultiLine" CssClass="form-control reqtxt" />
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-2 text-right">
                                            <label class="form-label mandatorylbl">
                                                Enquiry Application</label>
                                        </div>
                                        <div class="col-sm-4">
                                            <asp:TextBox ID="txtEnquiryApplication" runat="server" TabIndex="6" placeholder="Enter Application"
                                                TextMode="MultiLine" CssClass="form-control reqtxt" autocomplete="nope" />
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4">
                                        </div>
                                        <div class="col-sm-2">
                                            <div class="form-group">
                                                <asp:LinkButton Text="Submit" CssClass="btn btn-cons btn-save  AlignTop" ID="btndetails"
                                                    runat="server" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_detailsdiv');"
                                                    OnClick="btndetails_click" />
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:LinkButton Text="Cancel" CssClass="btn btn-cons btn-save  AlignTop btncancel" OnClick="btnCancelAttachements_Click"
                                                ID="btnCancelAttachements" runat="server" />
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="divOutput" class="output_section" runat="server" visible="false">
                                <div class="page-container" style="float: left; width: 100%">
                                    <div class="main-card">
                                        <div class="card-body">
                                            <div class="col-sm-12 p-t-10 p-l-0 p-r-0" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvitemdetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    OnRowCommand="gvitemdetails_OnRowCommand" OnRowDataBound="gvitemdetails_OnRowDataBound" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                    DataKeyNames="EDID,PUID,SIEHID,ItemID,SID,TagNoEdit,PressureEdit">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left"
                                                            HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitemname" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Size" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitemsize" runat="server" Text='<%# Eval("ItemSize")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tag No" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltagno" runat="server" Text='<%# Eval("TagNo").ToString().Replace("-TagNo", "")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldescription" runat="server" Text='<%# Eval("Description")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Revision Number" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblrevisionno" runat="server" Text='<%# Eval("RevisionNumber")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblqty" runat="server" Text='<%# Eval("Quantity")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Pressure" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpressure" runat="server" Text='<%# Eval("Pressure")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Temperature" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbltemp" runat="server" Text='<%# Eval("Temperature")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Movement" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblmovement" runat="server" Text='<%# Eval("Movement")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Material Warning" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblmatrlwarning" runat="server" Text='<%# Eval("MaterialWarning")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Location Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLocationName" runat="server" Text='<%# Eval("LocationName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Enquiry Application" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEnquirApplication" runat="server" Text='<%# Eval("EnquiryApplication")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnEdit_itemdetails" runat="server" OnClientClick="showLoader();"
                                                                    CommandArgument='<%#((GridViewRow) Container).RowIndex %>' CommandName="EditItem"><img src="../Assets/images/edit-ec.png" alt="" />
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnDelete_itemdetails" runat="server" OnClientClick="showLoader();"
                                                                    CommandArgument='<%#((GridViewRow) Container).RowIndex %>' CommandName="DeleteItem"><img src="../Assets/images/del-ec.png" alt=""/>
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:GridView ID="gvAttachments" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                    OnRowCommand="gvAttachments_OnRowCommand" OnRowDataBound="gvAttachments_OnRowDataBound"
                                                    DataKeyNames="AttachementID,FileName" Visible="false">
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
                                                                <asp:ImageButton ID="imgbtnView" runat="server" ImageUrl="../Assets/images/attach.png"
                                                                    Width="20px" Height="20px" CommandName="ViewDocs" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'></asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-12 p-t-10 text-center" id="divsharebutton" runat="server" visible="false">
                                                <asp:LinkButton Text="Share Item" CssClass="btn btn-cons btn-save  AlignTop" ID="btnShare"
                                                    runat="server" OnClientClick="return ShareItem();" />
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
