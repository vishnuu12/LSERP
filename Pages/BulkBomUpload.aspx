<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="BulkBomUpload.aspx.cs"
    Inherits="Pages_BulkBomUpload" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="../Assets/css/datatables2.css" />
    <script type="text/javascript" src="../Assets/scripts/datatables.js"></script>
    <script type="text/javascript" src="../Assets/scripts/datatables2.js"></script>
    <script type="text/javascript">

        function deleteConfirm(BOMId) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the BOMID Delete permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                __doPostBack('deletegvrow', BOMId);
            });
            return false;
        }

        function deleteAllPartBomDetails(BOMId) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the BOMID Delete permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                __doPostBack('deleteAllBomPart');
            });
            return false;
        }

        function SaveBOMCost() {
            swal({
                title: "Are you sure?",
                text: "If You Once Save The BOM Cost Did not able Visible Again",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Save it!",
                closeOnConfirm: false
            });
            return false;

        }

        function Validate() {
            var msg = true;

            if (msg) {
                showLoader();
                return true;
            }
            else
                return false;
        }

        function ShowViewPopUp() {
            $('#mpeView').modal({
                backdrop: 'static'
            });
            return false;
        }
        //
        function ShowAttachementPopUp() {
            $('#mpeAddAttachements').modal('show');
            return false;
        }

    </script>

    <style type="text/css">
        .btn-save, .btn-save:hover, .btn-save:focus {
            width: 24% !important;
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
                                    <h3 class="page-title-head d-inline-block">BOM Bulk Upload</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Design</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">BOM Bulk Upload</li>
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
                    <%-- <asp:PostBackTrigger ControlID="imgViewDrawing" />--%>
                    <asp:PostBackTrigger ControlID="btnBomBulkUpload" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" runat="server">
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
                                <div class="col-sm-12  p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Item Name</label>
                                        <asp:Label ID="lblDrawingNumber" Style="color: brown;" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlItemName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlItemName_SelectIndexChanged"
                                            CssClass="form-control" Width="70%" ToolTip="Select Item Name">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblItemQty" Style="color: brown;" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-2 text-left" style="margin-left: -11%;">
                                        <asp:Label ID="lblMaterialWarning" CssClass="blinking budgetaryhighligt lablqty"
                                            Style="width: 180%;" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Revision Number</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlVersionNumber" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlVersionNumber_SelectIndexChanged"
                                            CssClass="form-control" Width="70%" ToolTip="Select Enquiry Number" TabIndex="1">
                                            <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Layout Attachements
                                        </label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:FileUpload ID="fpbombulk" runat="server" onchange="DocValidation(this);"
                                            CssClass="form-control mandatoryfield" Width="70%"></asp:FileUpload>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>

                                <div class="ip-div text-center p-t-10" style="margin-bottom: 10px;">
                                    <asp:LinkButton Text="Bom Excel Upload" CssClass="btn btn-save btn-cons AlignTop" ID="btnBomBulkUpload"
                                        OnClick="btnBomBulkUpload_Click" runat="server" />
                                </div>

                            </div>
                        </div>
                        <%--<div id="divDrawing" runat="server" visible="false">
                            <div class="col-sm-12 p-t-10">
                                <div class="col-sm-4 text-right">
                                    <label class="form-label">
                                        View Drawing</label>
                                </div>
                                <div class="col-sm-6 text-left">
                                    <asp:ImageButton ID="imgViewDrawing" ImageUrl="" Style="height: 20px; width: 20px;"
                                        runat="server" OnClick="imgViewDrawing_Click" />
                                </div>
                                <div class="col-sm-2">
                                </div>
                            </div>
                        </div>--%>
                    </div>
                    <asp:HiddenField ID="hdnBOMId" Value="0" runat="server" />
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
    <div class="modal" id="mpeAddAttachements">
        <div class="modal-dialog" style="height: 100%;">
            <div class="modal-content" style="height: 100%;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always" Style="height: 100%;">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSaveAttachements" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H1">
                                <div class="text-center">
                                    Layout Attachements
                                    <asp:Label ID="Label2" Text="" runat="server"></asp:Label>
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal">
                                x</button>
                        </div>
                        <div class="modal-body" style="height: 100%;">
                            <div class="inner-container" style="height: 100%;">
                                <div class="ip-div text-center" style="margin-bottom: 10px;">
                                </div>
                                <div id="divAttach" runat="server">
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label mandatorylbl">
                                                Layout Attachements
                                            </label>
                                        </div>
                                        <div class="col-sm-6 text-left">
                                            <asp:FileUpload ID="fbLayoutAttach_p" runat="server" onchange="DocValidation(this);"
                                                CssClass="form-control mandatoryfield" Width="70%"></asp:FileUpload>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10 text-center">
                                        <asp:LinkButton ID="btnSaveAttachements" CssClass="btn btn-cons btn-save AlignTop"
                                            runat="server" Text="Save" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divAttach');"
                                            OnClick="btnSaveLayOutAttachement_Click"></asp:LinkButton>
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
