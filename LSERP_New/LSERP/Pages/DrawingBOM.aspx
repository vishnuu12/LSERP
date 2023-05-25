<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="DrawingBOM.aspx.cs" Inherits="Pages_DrawingBOM" %>

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

        function showDataTable() {
            $('#<%=gvDrawingBOMDetails.ClientID %>').DataTable({ 'aoColumnDefs': [{
                'bSortable': false,
                'aTargets': [-1, -2] /* 1st one, start by the right */
            }]
            });
        }

        function Validate() {
            var msg = true;

            if ($('#<%=ddlMaterialName.ClientID %>')[0].selectedIndex == 0) {
                $('#<%=ddlMaterialName.ClientID %>').notify('Fields Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }
            if ($('#<%=txtQuantity.ClientID %>').val() == "") {
                $('#<%=txtQuantity.ClientID %>').notify('Fields Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }

            if ($('#<%=ddlDrawingSequenceNumber.ClientID %>')[0].selectedIndex == 0) {
                $('#<%=ddlDrawingSequenceNumber.ClientID %>').notify('Fields Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }

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
                                    <h3 class="page-title-head d-inline-block">
                                        Drawing Bill Of Material</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Design</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Drawing BOM</li>
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
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Enquiry Number</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlEnquiryNumber" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEnquiryNumber_SelectIndexChanged"
                                            CssClass="form-control" Width="70%" OnChange="showLoader();" ToolTip="Select Enquiry Number"
                                            TabIndex="1">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Version Number</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlVersionNumber" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlVersionNumber_SelectIndexChanged"
                                            CssClass="form-control" Width="70%" OnChange="showLoader();" ToolTip="Select Enquiry Number"
                                            TabIndex="1">
                                            <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div id="divViewDrawing" runat="server" visible="false">
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
                                    <div class="col-sm-12 text-center p-t-10">
                                        <asp:LinkButton ID="btnAddNew" runat="server" Text="Add BOM" OnClick="btnAddNew_Click"
                                            CssClass="btn btn-success add-emp"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-4">
                                        <label class="form-label">
                                            Material Name
                                        </label>
                                    </div>
                                    <div class="col-sm-6 text-center">
                                        <asp:DropDownList ID="ddlMaterialName" runat="server" CssClass="form-control" Width="70%"
                                            ToolTip="Select Material Name" TabIndex="1">
                                            <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 text-center p-t-10">
                                    <div class="col-sm-4">
                                        <label class="form-label">
                                            Quantity
                                        </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" ToolTip="Enter Quantity"
                                            autocomplete="nope" Width="70%" onkeypress="return validationNumeric(this);"
                                            placeholder="Enter Quantity">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4">
                                        <label class="form-label">
                                            Drawing Sequence Number
                                        </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:DropDownList ID="ddlDrawingSequenceNumber" runat="server" CssClass="form-control"
                                            Width="70%" ToolTip="Select Material Name" TabIndex="1">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <asp:LinkButton ID="btnSave" CssClass="btn btn-cons btn-save AlignTop" runat="server"
                                        Text="Save" OnClientClick="return Validate();" OnClick="btnSave_Click"></asp:LinkButton>
                                    <asp:LinkButton ID="btnCancel" CssClass="btn btn-cons btn-danger AlignTop" runat="server"
                                        OnClick="btnCancel_Click" Text="Cancel"></asp:LinkButton>
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
                                                    <asp:Label ID="lbltitle" runat="server"></asp:Label></p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvDrawingBOMDetails" runat="server" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" OnRowCommand="gvDrawingBOMDetails_OnRowCommand"
                                                OnRowDataBound="gvDrawingBOMDetails_OnRowDataBound" CssClass="table table-hover table-bordered medium"
                                                HeaderStyle-HorizontalAlign="Center" DataKeyNames="BOMID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Name" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVersionNumber" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Material Name" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMaterialName" runat="server" Text='<%# Eval("MaterialName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Part Number" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSubmittedOn" runat="server" Text='<%# Eval("MaterialPartNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Drawing Sequence Number" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSequenceNumber" runat="server" Text='<%# Eval("DrawingSequenceNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# Container.DataItemIndex %>'
                                                                Style="text-align: center" CommandName="EditBOM">
                                                            <img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDelete" runat="server" Style="text-align: center" OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("BOMID")) %>'>
                                                            <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="col-sm-12 p-t-20">
                                            <div class="col-sm-3">
                                            </div>
                                            <div class="col-sm-6">
                                                <label class="form-label">
                                                    * Please Add The Material In order Of Drawing Sheet</label>
                                            </div>
                                            <div class="col-sm-3">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hdnBOMId" Value="0" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal" id="mpeView">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H2">
                                <div class="text-center">
                                    View Drawing
                                    <asp:Label ID="Label1" Text="" runat="server"></asp:Label>
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal">
                                x</button>
                        </div>
                        <div class="modal-body">
                            <div class="inner-container">
                                <div class="col-sm-12">
                                    <div class="col-sm-8">
                                        <asp:Image ID="imgDocs" ImageUrl="" Height="100%" Width="100%" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                        </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
