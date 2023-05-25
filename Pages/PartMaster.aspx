<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="PartMaster.aspx.cs" Inherits="Pages_PartMaster" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="../Assets/css/datatables2.css" />
    <script type="text/javascript" src="../Assets/scripts/datatables2.js"></script>
    <script type="text/javascript">
        function deleteConfirm(PartId) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Part Name will be deleted permanently",
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

        function showDataTable() {
            $('#<%=gvPartMaster.ClientID %>').DataTable({ 'aoColumnDefs': [{
                'bSortable': false,
                'aTargets': [-1, -2] /* 1st one, start by the right */
            }]
            });
        }

        function ValidateAdd() {
            var msg = "0";

            if ($('#<%=txtPartName.ClientID %>')[0].value == "") {
                $('#<%=txtPartName.ClientID %>').notify('Field Required.', { arrowShow: true, position: 't,r', autoHide: true });
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
            $('#<%=txtPartName.ClientID %>')[0].value = "";
            $('#<%=txtDescription.ClientID %>')[0].value = "";
            document.getElementById('<%=hdnPartID.ClientID %>').value = "0";

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
                                    <h3 class="page-title-head d-inline-block">
                                        Part Master</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Stores</a></li>
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
            <asp:UpdatePanel ID="upPartMaster" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="imgExcel" />
                    <asp:PostBackTrigger ControlID="imgPdf" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" runat="server">
                            <div class="ip-div text-center">
                                <asp:LinkButton ID="btnAddNew" runat="server" Text="Add New Part Name" OnClientClick="return ShowInputSection();"
                                    CssClass="btn ic-w-btn add-btn"></asp:LinkButton>
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Part Name</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:TextBox ID="txtPartName" runat="server" Width="70%" CssClass="form-control"
                                            ToolTip="Enter Part Name" placeholder="Enter Part Name">
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
                                        <div class="row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                                    <asp:Label ID="lbltitle" runat="server" Text="Part Name Details"></asp:Label></p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                                <asp:LinkButton ID="btnprint" runat="server" CssClass="print_bg" ToolTip="print" />
                                                <asp:LinkButton ID="imgExcel" runat="server" CssClass="excel_bg" ToolTip="Excel Download"
                                                    OnClick="btnExcelDownload_Click" />
                                                <asp:LinkButton ID="imgPdf" runat="server" CssClass="pdf_bg" ToolTip="PDF Download"
                                                    OnClick="btnPDFDownload_Click" />
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvPartMaster" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                HeaderStyle-HorizontalAlign="Center" OnRowDataBound="gvPartMaster_RowDataBound"
                                                OnRowCommand="gvPartMaster_RowCommand" DataKeyNames="PartId">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Part Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartName" runat="server" Text='<%# Eval("PartName")%>'></asp:Label>
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
                                                                CommandName="EditPartType">
                                                           <img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDelete" runat="server" OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("PartId")) %>'>
                                                            <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnPartID" runat="server" Value="0" />
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
