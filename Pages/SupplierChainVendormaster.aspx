<%--/*----------------------------------------------------------------------------------------------------                                                                                                                   
 Developer Name          : Vishnu S                                                                                                                
 Purpose                 : Forms and Reports for Supplier Vendor Master
 Created Date            : 17-10-2022
 Modified Date           :                                                                                                                  
 Modified Developer Name :                                                                                                                    
--------------------------------------------------------------------------------------------------------*/       --%>


<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="SupplierChainVendormaster.aspx.cs" Inherits="Pages_LSE_SupplierChainVendormaster" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="../Assets/css/datatables2.css" />
    <script type="text/javascript" src="../Assets/scripts/datatables2.js"></script>
    <script type="text/javascript">
        function deleteConfirm(SCVMID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Row will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deletegvrow', SCVMID);
            });
            return false;
        }

        function showDataTable() {
            $('#<%=gvSupplierChain.ClientID %>').DataTable();
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
                                    <h3 class="page-title-head d-inline-block">Supplier Chain Vendor Master</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Master</a></li>
                                <li class="active breadcrumb-item" aria-current="page">Supplier Chain Vendor master</li>
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
                    <asp:PostBackTrigger ControlID="btnSave" />
                    <asp:PostBackTrigger ControlID="imgExcel" />
                </Triggers>
                <ContentTemplate>

                    <div class="card-container">
                        <div id="divAdd" runat="server">
                            <div class="text-center">
                                <asp:LinkButton ID="btnAddNew" runat="server" Text="Add New" OnClick="btnAddNew_Click"
                                    CssClass="btn btn-success add-emp"></asp:LinkButton>
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4 text-right">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Vendor Name</label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtVendorname" runat="server" Width="70%" CssClass="form-control mandatoryfield"
                                                ToolTip="Enter Vendor Name" AutoComplete="off" placeholder="Enter Vendor Name">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Contact Person</label>
                                        </div>
                                        <div>
                                            <asp:TextBox runat="server" ID="txtContactPerson" AutoComplete="off" CssClass="form-control mandatoryfield"
                                                Width="70%" placeholder="Enter Contact Person" />
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4 text-right">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Contact No
                                            </label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtContactNo" runat="server" Width="70%" CssClass="form-control mandatoryfield"
                                                ToolTip="Enter Contact No" AutoComplete="off" placeholder="Enter Contact No">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4 text-right">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Email Id
                                            </label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtEmailId" runat="server" Width="70%" CssClass="form-control mandatoryfield"
                                                ToolTip="Enter Email Id" AutoComplete="off" placeholder="Enter Email Id">
                                            </asp:TextBox>
                                        </div>
                                    </div>


                                    <div class="col-sm-2">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4 text-right">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                GST No</label>
                                        </div>
                                        <div>
                                            <asp:TextBox runat="server" ID="txtGSTNo" AutoComplete="off" CssClass="form-control mandatoryfield"
                                                Width="70%" placeholder="Enter GST No" />
                                        </div>
                                    </div>
                                    <div class="col-sm-4 text-right">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Address
                                            </label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtAddress" runat="server" TabIndex="6" placeholder="Enter Address"
                                                ToolTip="Enter Address" TextMode="MultiLine" CssClass="form-control mandatoryfield"
                                                MaxLength="300" autocomplete="nope" />
                                        </div>
                                    </div>


                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Attach GSTN Details
                                            </label>
                                        </div>
                                        <div>
                                            <asp:FileUpload ID="fbUpload" runat="server" onchange="DocValidation(this);" TabIndex="12"
                                                CssClass="form-control mandatoryfield" Width="95%"></asp:FileUpload>

                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>

                                <div class="col-sm-12 text-center p-t-10">
                                    <asp:LinkButton ID="btnSave" Text="Save" CssClass="btn btn-cons btn-save AlignTop"
                                        OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divInput');" OnClick="btnSave_Click"
                                        runat="server" />
                                    <asp:LinkButton ID="btnCancel" Text="Cancel" CssClass="btn btn-cons btn-danger AlignTop" OnClick="btnCancel_Click"
                                        runat="server" />
                                </div>
                                <%-- <asp:Label ID="lblMessage" ForeColor="Green" runat="server" />--%>
                            </div>
                        </div>

                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="col-sm-12 row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                                    <asp:Label ID="lbltitle" runat="server" Text="Vendor Details"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server">
                                                <asp:LinkButton ID="imgExcel" runat="server" CssClass="excel_bg" ToolTip="Excel Download"
                                                    OnClick="btnExcelDownload_Click" />
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">

                                            <asp:GridView ID="gvSupplierChain" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                HeaderStyle-HorizontalAlign="Center" OnRowCommand="gvSupplierChain_RowCommand"
                                                OnRowDataBound="gvSupplierChain_OnRowDataBound" DataKeyNames="SCVMID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Vendor Name" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblvendor" runat="server" Text='<%# Eval("VendorName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Address" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbladdress" runat="server" Text='<%# Eval("Address")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Contact Person" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcontactperson" runat="server" Text='<%# Eval("ContactPerson")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Contact Number" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcontactpno" runat="server" Text='<%# Eval("ContactNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Email Id" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblemail" runat="server" Text='<%# Eval("EmailId")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="GST No" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgstno" runat="server" Text='<%# Eval("GSTNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="EditSupplierChainDetails" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                           <img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDelete" runat="server" OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("SCVMID")) %>'>
                                                            <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>


                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="SUPVENID" Value="0" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>

