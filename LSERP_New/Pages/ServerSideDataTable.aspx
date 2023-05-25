<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="ServerSideDataTable.aspx.cs" Inherits="Pages_ServerSideDataTable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--  <script type="text/javascript" src="https://code.jquery.com/jquery-3.5.1.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.21/js/jquery.dataTables.min.js"></script>
    <link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/1.10.21/css/jquery.dataTables.min.css" />--%>

  <%--  <script type="text/javascript" src="../Assets/scripts/jquery-3.3.1.js"></script>
    <link rel="stylesheet" type="text/css" href="../Assets/css/datatables2.css" />
    <script type="text/javascript" src="../Assets/scripts/datatables2.js?v=4"></script>--%>
    <script type="text/javascript">

        function deleteConfirm(DocumentTypeId) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Document Type will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deletegvrow', DocumentTypeId);
            });
            return false;
        }

      
        $(document).ready(function () {
            BindDate();
        });


        function BindDate() {
            $('#gvDocumentType').DataTable({
                // "retrieve": true,
                "processing": true,
                "serverSide": true,
                "stateSave": true,
                // "ordering": false,
               // "bSort": false,
                "ajax": ({
                    type: "GET",
                    url: "ServerSideDataTable.aspx/GetData", //It calls our web method  
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    "data": function (d) {
                        return d;
                    },
                    "dataSrc": function (json) {
                        json.draw = json.d.draw;
                        json.recordsTotal = json.d.recordsTotal;
                        json.recordsFiltered = json.d.recordsFiltered;
                        json.data = json.d.data;
                        var return_data = json;
                        return return_data.data;
                    },
                }),
                "columns": [
                    { "data": 'EmployeeID' },
                    { 'data': 'Type' },
                    { 'data': 'Extension' }
                ]
                //"aoColumnDefs": [
                //    {
                //        "aTargets": [3],
                //        "mData": null,
                //        "mRender": function (data, type, full) {
                //            return '<button href="#"' + 'id="' + data.EmployeeID + '" onclick="return showpopup()";><img src="../Assets/images/add1.png"></button>';
                //        }
                //    }
                //]
            });
        }

        function ValidateAdd() {
            var msg = "0";

            if ($('#<%=txtDocumentType.ClientID %>')[0].value == "") {
                $('#<%=txtDocumentType.ClientID %>').notify('Field Required.', { arrowShow: true, position: 't,r', autoHide: true });
                msg = "1";
            }
            else if ($('#<%=txtExtension.ClientID %>')[0].value == "") {
                $('#<%=txtExtension.ClientID %>').notify('Field Required.', { arrowShow: true, position: 't,r', autoHide: true });
                msg = "1";
            }

            if (msg == "0") {
                showLoader();
                return true;
            }
            else
                return false;
        }

        function showpopup() {
            document.getElementById('<%=btnpost.ClientID %>').click();
            return true;
        }

   <%--     function ClearFields() {
            $('#<%=txtDocumentType.ClientID %>')[0].value = "";
            $('#<%=txtExtension.ClientID %>')[0].value = "";


            document.getElementById('<%=divInput.ClientID %>').style.display = "none";
            document.getElementById('<%=divOutput.ClientID %>').style.display = "block";
            document.getElementById('<%=divAdd.ClientID %>').style.display = "block";

            return false;
        }

        function ShowInputSection(Name) {

            if (Name == "divAdd") {
                document.getElementById('<%=divAdd.ClientID %>').style.display = "none";
                document.getElementById('<%=divInput.ClientID %>').style.display = "block";
                document.getElementById('<%=divOutput.ClientID %>').style.display = "none";
                return false;
            }
        }--%>

        //sample  Test call In Ajax 

        //jQuery.ajax({
        //        type: "POST",data: '{iuser: "karthik" }', 
        //        url: "ServerSideDataTable.aspx/checkUserNameAvail", //It calls our web method
        //       contentType: "application/json; charset=utf-8",
        //        success: function (d) {
        //            alert(d);
        //        },
        //        error: function (d) {
        //        }
        //    });

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
                                    <h3 class="page-title-head d-inline-block">Document Type</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Master Setup</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Document Type</li>
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
                    <div class="card-container">
                        <div id="divAdd" runat="server">
                            <div class="text-center">
                                <asp:LinkButton ID="btnAddNew" runat="server" Text="Add New" OnClientClick="return ShowInputSection('divAdd');"
                                    CssClass="btn btn-success add-emp"></asp:LinkButton>
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Document Type</label>
                                    </div>
                                    <div class="col-sm-8 text-left">
                                        <asp:TextBox ID="txtDocumentType" runat="server" Width="70%" CssClass="form-control"
                                            ToolTip="Enter Document Type" placeholder="Enter Document Type">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Extension</label>
                                    </div>
                                    <div class="col-sm-8 text-left">
                                        <asp:TextBox runat="server" ID="txtExtension" CssClass="form-control" Width="70%"
                                            ToolTip="Enter Extension" placeholder="Enter Extension" />
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
                                        <div class="col-md-12 row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                                    <asp:Label ID="lbltitle" runat="server" Text="Document Type Details"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                                <asp:LinkButton ID="btnprint" runat="server" CssClass="print_bg" OnClientClick="return fnvalidate();"
                                                    ToolTip="print" />
                                                <asp:LinkButton ID="imgExcel" runat="server" CssClass="excel_bg" OnClick="btnExcelDownload_Click"
                                                    ToolTip="Excel Download" />
                                                <asp:LinkButton ID="imgPdf" runat="server" CssClass="pdf_bg" ToolTip="PDF Download"
                                                    OnClick="btnPDFDownload_Click" />
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <table class="table table-hover table-bordered medium"
                                                id="gvDocumentType"
                                                width="100%">
                                                <thead>
                                                    <tr>
                                                        <th>EmployeeID</th>
                                                        <th>Ty pe</th>
                                                        <th>Exte nsion</th>
                                                       
                                                    </tr>
                                                </thead>
                                            </table>

                                            <%--        <asp:GridView ID="gvDocumentType" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                HeaderStyle-HorizontalAlign="Center" OnRowDataBound="gvDocumentType_RowDataBound"
                                                UseAccessibleHeader="true" OnRowCommand="gvDocumentType_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Document Type" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDocumentType" runat="server" Text='<%# Eval("TypeName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Extension">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblExtension" runat="server" Text='<%# Eval("Extension")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="EditDocumentType">
                                                           <img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDelete" runat="server">
                                                            <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>--%>
                                            <asp:HiddenField ID="hdnDocumentTypeID" runat="server" Value="0" />

                                            <div style="display: none;">
                                                <asp:LinkButton ID="btnpost" runat="server" Text="post" CssClass="btn btn-cons btn-save AlignTop"
                                                    OnClick="btnpost_Click"></asp:LinkButton>
                                            </div>
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
