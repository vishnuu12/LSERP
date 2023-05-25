<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="MaterialTypeMaster.aspx.cs" Inherits="Pages_MaterialTypeMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="../Assets/css/datatables2.css" />
    <script type="text/javascript" src="../Assets/scripts/datatables2.js"></script>
    <script type="text/javascript">
        function deleteConfirm(MaterialTypeId) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Material Type will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deletegvrow', MaterialTypeId);
            });
            return false;
        }

        function showDataTable() {
            $('#<%=gvMaterial.ClientID %>').DataTable();
        }

        function ValidateAdd() {
            var msg = "0";

            if ($('#<%=txtMaterialType.ClientID %>')[0].value == "") {
                $('#<%=txtMaterialType.ClientID %>').notify('Field Required.', { arrowShow: true, position: 't,r', autoHide: true });
                msg = "1";
            }
            else if ($('#<%=txtDescription.ClientID %>')[0].value == "") {
                $('#<%=txtDescription.ClientID %>').notify('Field Required.', { arrowShow: true, position: 't,r', autoHide: true });
                msg = "1";
            }

            if (msg == "0") {
                showLoader();
                return true;
            }
            else
                return false;
        }

        function ClearFields() {
            $('#<%=txtMaterialType.ClientID %>')[0].value = "";
            $('#<%=txtDescription.ClientID %>')[0].value = "";

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
                                        MaterialType</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Master Setup</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">MaterialType</li>
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
                    <asp:PostBackTrigger ControlID="imgExcel" />
                    <asp:PostBackTrigger ControlID="imgPdf" />
                </Triggers>
                <ContentTemplate>
                    <div id="divInput" runat="server">
                        <div class="ip-div text-center">
                            <div class="col-sm-12">
                                <div class="col-sm-4 text-right">
                                    <label class="form-label">
                                        MaterialType</label>
                                </div>
                                <div class="col-sm-6 text-left">
                                    <asp:TextBox ID="txtMaterialType" runat="server" Width="70%" CssClass="form-control"
                                        ToolTip="Enter Material Type" placeholder="Enter Material Type">
                                    </asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                </div>
                            </div>
                            <div class="col-sm-12 p-t-10">
                                <div class="col-sm-4 text-right">
                                    <label class="form-label">
                                        Description</label>
                                </div>
                                <div class="col-sm-6 text-left">
                                    <asp:TextBox runat="server" ID="txtDescription" CssClass="form-control" Width="70%"
                                        ToolTip="Enter Description" placeholder="Enter Description" TextMode="MultiLine" />
                                </div>
                                <div class="col-sm-2">
                                </div>
                            </div>
                            <div class="col-sm-12 p-t-20">
                                <asp:LinkButton ID="btnSave" runat="server" Text="Save" CssClass="btn btn-cons btn-save AlignTop"
                                    OnClientClick="return ValidateAdd()" OnClick="btnSave_Click"></asp:LinkButton>
                                <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-cons btn-danger AlignTop"
                                    OnClientClick="return ClearFields()"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div id="divOutput" class="output_section" runat="server">
                        <div class="page-container" style="float: left; width: 100%">
                            <div class="main-card">
                                <div class="card-body">
                                    <div class="">
                                        <p class="h-style">
                                            <asp:Label ID="lbltitle" runat="server" Text="Material Type Details"></asp:Label></p>
                                        <div class="f-right">
                                        </div>
                                        <div class="clear">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10 ex-icons" id="divDownload" runat="server" visible="false">
                                        <asp:LinkButton ID="btnprint" runat="server" CssClass="print_bg" ToolTip="print" />
                                        <asp:LinkButton ID="imgExcel" runat="server" CssClass="excel_bg" ToolTip="Excel Download"
                                            OnClick="btnExcelDownload_Click" />
                                        <asp:LinkButton ID="imgPdf" runat="server" CssClass="pdf_bg" ToolTip="PDF Download"
                                            OnClick="btnPDFDownload_Click" />
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <asp:GridView ID="gvMaterial" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                            HeaderStyle-HorizontalAlign="Center" OnRowDataBound="gvMaterial_RowDataBound"
                                            OnRowCommand="gvMaterial_RowCommand" DataKeyNames="MaterialTypeId">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Material Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMaterialType" runat="server" Text='<%# Eval("TypeName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            CommandName="EditMaterialType">
                                                           <img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnDelete" runat="server" OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("MaterialTypeId")) %>'>
                                                            <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:HiddenField ID="hdnMaterialTypeID" runat="server" Value="0" />
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
