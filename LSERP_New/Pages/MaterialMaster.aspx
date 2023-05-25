<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="MaterialMaster.aspx.cs" Inherits="Pages_MaterialMaster" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="../Assets/css/datatables2.css" />
    <script type="text/javascript" src="../Assets/scripts/datatables2.js"></script>
    <script type="text/javascript">
        function deleteConfirm(MID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Material Name will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deletegvrow', MID);
            });
            return false;
        }

        function showDataTable() {
            $('#<%=gvMaterialMaster.ClientID %>').DataTable({
                'aoColumnDefs': [{
                    'bSortable': false,
                    'aTargets': [-1, -2] /* 1st one, start by the right */
                }]
            });
        }

        function ValidateAdd() {
            var msg = "0";

            if (document.getElementById('<%=txtMaterialName.ClientID %>').disabled == false) {
                if ($('#<%=txtMaterialName.ClientID %>')[0].value == "") {
                    $('#<%=txtMaterialName.ClientID %>').notify('Field Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                    msg = "1";
                }

                if ($('#<%=ddlMaterialType.ClientID %>')[0].selectedIndex == 0) {
                    $('#<%=ddlMaterialType.ClientID %>').notify('Field Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                    msg = "1";
                }
            }
            if (msg == "0") {
                showLoader();
                return true;
            }
            else
                return false;
        }

        function ClearFields() {
            $('#<%=txtMaterialName.ClientID %>')[0].value = "";
            document.getElementById('<%=ddlMaterialType.ClientID %>').selectedIndex = 0;

            $('#<%=txtMaterialName.ClientID %>').attr('disabled', false);
            $('#<%=ddlMaterialType.ClientID %>').attr('disabled', false);
            document.getElementById('<%=hdnMID.ClientID %>').value = "0";

            document.getElementById('<%=divInput.ClientID %>').style.display = "none";
            document.getElementById('<%=divOutput.ClientID %>').style.display = "block";
            document.getElementById('<%=divAdd.ClientID %>').style.display = "block";
            return false;
        }

        function ShowInputSection() {
            document.getElementById('<%=divAdd.ClientID %>').style.display = "none";
            document.getElementById('<%=divInput.ClientID %>').style.display = "block";
            document.getElementById('<%=divOutput.ClientID %>').style.display = "none";
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
                                    <h3 class="page-title-head d-inline-block">Part Master</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Master Setup</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Part Master</li>
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
            <asp:UpdatePanel ID="upMaterialGroup" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="imgExcel" />
                    <asp:PostBackTrigger ControlID="imgPdf" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" runat="server">
                            <div class="ip-div text-center">
                                <asp:LinkButton ID="btnAddNew" runat="server" Text="Add New Part" OnClientClick="return ShowInputSection();"
                                    CssClass="btn ic-w-btn add-btn"></asp:LinkButton>
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <%--  <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            MaterialGroup</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlMaterialGroup" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMaterialGroup_SelectIndexChanged"
                                            CssClass="form-control" Width="70%" ToolTip="Select Material Group" TabIndex="1"
                                            AppendDataBoundItems="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>--%>
                                <%--    <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Classification Norm/HSN Code</label>
                                    </div>
                                    <div class="col-sm-4 text-left">
                                        <asp:DropDownList ID="ddlMaterialClassificationNom" runat="server" AutoPostBack="true"
                                            CssClass="form-control" OnSelectedIndexChanged="ddlMatrialClassificationNorm_SelectIndexChanged"
                                            Width="108%" ToolTip="Select Material Group" TabIndex="2" AppendDataBoundItems="true">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtAddNorm" runat="server" Style="text-align: left;" Width="70%"
                                            CssClass="form-control" ToolTip="Enter Classification Norm" placeholder="Enter Classification Norm">
                                        </asp:TextBox>
                                    </div>
                                </div>--%>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Material Type</label>
                                    </div>
                                    <div class="col-sm-4 text-left">
                                        <asp:DropDownList ID="ddlMaterialType" runat="server" CssClass="form-control" ToolTip="Select Material Type"
                                            Width="108%" TabIndex="2">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Manufacture Items" Value="M"></asp:ListItem>
                                            <asp:ListItem Text="Inventory Items" Value="I"></asp:ListItem>
                                            <asp:ListItem Text="Bought Out Items" Value="B"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Part Name</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:TextBox ID="txtMaterialName" runat="server" Width="70%" CssClass="form-control"
                                            ToolTip="Enter Material Name" TabIndex="2" placeholder="Enter Material Name">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>

                                <%--<div class="col-sm-12 p-t-10">
                                <div class="col-sm-4 text-right">
                                    <label class="form-label">
                                        Material Part Number</label>
                                </div>
                                <div class="col-sm-6 text-left">
                                    <asp:TextBox ID="txtMaterialPartNumber" runat="server" Width="70%" CssClass="form-control"
                                        ToolTip="Enter Material Part Number" TabIndex="2" placeholder="Enter Material Part Number"
                                        Enabled="false">
                                    </asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                </div>
                            </div>--%>
                                <div class="col-sm-12 p-t-20">
                                    <asp:LinkButton ID="btnSave" runat="server" Text="Save" CssClass="btn btn-cons btn-save AlignTop"
                                        TabIndex="3" OnClientClick="return ValidateAdd()" OnClick="btnSave_Click"></asp:LinkButton>
                                    <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-cons btn-danger AlignTop"
                                        TabIndex="4" OnClientClick="return ClearFields()"></asp:LinkButton>
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
                                                    <asp:Label ID="lbltitle" runat="server" Text="Part Master Details"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                                <asp:LinkButton ID="btnprint" runat="server" CssClass="print_bg" ToolTip="print" />
                                                <asp:LinkButton ID="imgExcel" runat="server" CssClass="excel_bg" ToolTip="Excel Download"
                                                    OnClick="btnExcelDownload_Click" />
                                                <asp:LinkButton ID="imgPdf" runat="server" CssClass="pdf_bg" ToolTip="PDF Download"
                                                    OnClick="btnPDFDownload_Click" />
                                            </div>
                                        </div>
                                        <div class="col-md-12 p-t-10">
                                            <asp:GridView ID="gvMaterialMaster" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                HeaderStyle-HorizontalAlign="Center" OnRowDataBound="gvMaterialMaster_RowDataBound"
                                                OnRowCommand="gvMaterialMaster_RowCommand" DataKeyNames="MID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--  <asp:TemplateField HeaderText="Classification Norm/HSN Code" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblClassificationNom" runat="server" Text='<%# Eval("ClassificationNorm")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Material Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMaterialType" runat="server" Style="display: none;" Text='<%# Eval("MaterialType")%>'></asp:Label>
                                                            <asp:Label ID="lblMatrialTypeName" runat="server" Text='<%# Eval("MaterialTypeName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Material Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMaterialName" runat="server" Text='<%# Eval("MaterialName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Part Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartType" runat="server" Text='<%# Eval("PartType")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="EditMaterialName">
                                                           <img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDelete" runat="server" OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("MID")) %>'>
                                                            <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnMID" runat="server" Value="0" />
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
</asp:Content>
