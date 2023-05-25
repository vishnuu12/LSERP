<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="Annexure_SOW.aspx.cs" Inherits="Annexure_SOW" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="../Assets/css/datatables2.css" />
    <script type="text/javascript" src="../Assets/scripts/datatables.js"></script>
    <script type="text/javascript" src="../Assets/scripts/datatables2.js"></script>
    <script language="JavaScript" type="text/javascript">

        function deleteConfirm(SOWID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Scope Header will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deletegvrow', SOWID);
            });
            return false;
        }

        function showDataTable() {
            $('#<%=gvAnnexure.ClientID %>').DataTable({ 'aoColumnDefs': [{
                'bSortable': false,
                'aTargets': [-1, -2] /* 1st one, start by the right */
            }]
            });
        }


        function ClearFields() {
            $('#<%=txtHeaderName.ClientID %>')[0].value = "";

            document.getElementById('<%=hdnSOWID.ClientID %>').value = "0";

            document.getElementById('<%=divInput.ClientID %>').style.display = "none";
            document.getElementById('<%=divOutput.ClientID %>').style.display = "block";
            document.getElementById('<%=divAdd.ClientID %>').style.display = "block";
            return false;
        }

        function ValidateAdd() {
            var msg = "0";

            if ($('#<%=txtHeaderName.ClientID %>')[0].value == "") {
                $('#<%=txtHeaderName.ClientID %>').notify('Field Required.', { arrowShow: true, position: 't,r', autoHide: true });
                msg = "1";
            }

            if (msg == "0") {
                showLoader();
                return true;
            }
            else
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
                                    <h3 class="page-title-head d-inline-block">
                                        Scope of Work Master</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Sales</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Annexure SOW</li>
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
            <asp:UpdatePanel ID="upQuote" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="imgExcel" />
                    <asp:PostBackTrigger ControlID="imgPdf" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" runat="server">
                            <div class="ip-div text-center">
                                <asp:LinkButton ID="btnAddNew" runat="server" Text="Add New Scope Header" OnClientClick="return ShowInputSection();"
                                    CssClass="btn ic-w-btn add-btn"></asp:LinkButton>
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4">
                                        <label class="form-label">
                                            Annexure Scope Header
                                        </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtHeaderName" runat="server" CssClass="form-control" ToolTip="Enter Scope Header"
                                            placeholder="Enter Scope Header" MaxLength="100"></asp:TextBox>
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
                                        <div class="row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                                    <asp:Label ID="lbltitle" runat="server" Text=" Annexure Scope Header"></asp:Label></p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                                <asp:LinkButton ID="btnprint" runat="server" CssClass="print_bg" ToolTip="print" />
                                                <asp:LinkButton ID="imgExcel" runat="server" CssClass="excel_bg" ToolTip="Excel Download"
                                                    OnClick="btnExcelDownload_Click" />
                                                <asp:LinkButton ID="imgPdf" runat="server" CssClass="pdf_bg" ToolTip="PDF Download"
                                                    OnClick="btnPDFDownload_Click" />
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10 p-l-0 p-r-0">
                                            <asp:GridView ID="gvAnnexure" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                CssClass="table table-hover table-bordered medium" OnRowCommand="gvAnnexure_RowCommand"
                                                HeaderStyle-HorizontalAlign="Center" EmptyDataText="No Records Found" DataKeyNames="SOWID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'
                                                                Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Annexure Name" HeaderText="Annexure Name / Title"
                                                        ItemStyle-Width="25%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAnnexure" runat="server" Text='<%# Eval("HeaderName")%>' Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%# Container.DataItemIndex %>'
                                                                Style="text-align: center" CommandName="ViewAnnexure">
                                                            <img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDelete" runat="server" Style="text-align: center" OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("SOWID")) %>'>
                                                            <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnSOWID" runat="server" Value="0" />
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
