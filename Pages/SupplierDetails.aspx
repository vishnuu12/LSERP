<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/CommonMaster.master" CodeFile="SupplierDetails.aspx.cs"
    Inherits="Pages_SupplierDetails" ClientIDMode="Predictable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">

        function deleteConfirm(SUPID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Supplier Name will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deletegvrow', SUPID);
            });
            return false;
        }

        function Validate() {
            if (Mandatorycheck('ContentPlaceHolder1_divInput')) {
                showLoader();
                return true;
            }
            else {
                return false;
            }
        }

        function ShowViewPopup() {
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
                                    <h3 class="page-title-head d-inline-block">Supplier Details</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Stores</a></li>
                                <li class="active breadcrumb-item" aria-current="page">Supplier Details</li>
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
                                                Supplier Name</label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtSuppliername" runat="server" Width="70%" CssClass="form-control mandatoryfield"
                                                ToolTip="Enter Supplier Name" AutoComplete="off" placeholder="Enter Supplier Name">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label">
                                                Description</label>
                                        </div>
                                        <div>
                                            <asp:TextBox runat="server" ID="txtDescription" AutoComplete="off" CssClass="form-control"
                                                Width="70%" placeholder="Enter Description" />
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
                                                Address
                                            </label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtAddress" runat="server" Width="70%" CssClass="form-control mandatoryfield"
                                                ToolTip="Enter Address" AutoComplete="off" placeholder="Enter Address">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label">
                                                Contact Person</label>
                                        </div>
                                        <div>
                                            <asp:TextBox runat="server" ID="txtContactPerson" AutoComplete="off" CssClass="form-control"
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
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label">
                                                Fax No</label>
                                        </div>
                                        <div>
                                            <asp:TextBox runat="server" ID="txtFaxNo" AutoComplete="off" CssClass="form-control"
                                                Width="70%" placeholder="Enter Fax No" />
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
                                                Email Id
                                            </label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtEmailId" runat="server" Width="70%" CssClass="form-control mandatoryfield"
                                                ToolTip="Enter Email Id" AutoComplete="off" placeholder="Enter Email Id">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label">
                                                GST No</label>
                                        </div>
                                        <div>
                                            <asp:TextBox runat="server" ID="txtGSTNo" AutoComplete="off" CssClass="form-control"
                                                Width="70%" placeholder="Enter GST No" />
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
                                                Sub Vendor
                                            </label>
                                        </div>
                                        <div>
                                            <asp:RadioButtonList ID="rblSubvendorFlag" runat="server"
                                                CssClass="radio radio-success" RepeatDirection="Horizontal">
                                                <asp:ListItem Text="No" Value="0" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Both" Value="2"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label">
                                                Attach GSTN Details
                                            </label>
                                        </div>
                                        <div>
                                            <asp:FileUpload ID="fGSTNAttach" runat="server" onchange="DocValidation(this);" TabIndex="12"
                                                CssClass="form-control" Width="95%"></asp:FileUpload>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-20">
                                    <asp:LinkButton ID="btnSave" runat="server" Text="Save" CssClass="btn btn-cons btn-save AlignTop"
                                        OnClientClick="return Validate();" OnClick="btnSave_Click"></asp:LinkButton>
                                    <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                        CssClass="btn btn-cons btn-danger AlignTop"></asp:LinkButton>
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
                                                    <asp:Label ID="lbltitle" runat="server" Text="Supplier Details"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server">
                                                <asp:LinkButton ID="imgExcel" runat="server" CssClass="excel_bg" ToolTip="Excel Download"
                                                    OnClick="btnExcelDownload_Click" />
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">

                                            <asp:GridView ID="gvSupplier" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                HeaderStyle-HorizontalAlign="Center" OnRowCommand="gvSupplier_RowCommand" DataKeyNames="SUPID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Supplier Name" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSize" runat="server" Text='<%# Eval("SupplierName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Description" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description")%>'></asp:Label>
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
                                                            <asp:Label ID="lblcontactpno" runat="server" Text='<%# Eval("ContactNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="View" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnview" runat="server" CommandName="ViewSupplierDetails" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                            <img src="../Assets/images/view.png" alt="" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="EditSupplierDetails" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                           <img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Delete" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDelete" runat="server" OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("SUPID")) %>'>
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
                    <asp:HiddenField ID="hdnSUPID" Value="0" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="modal" id="mpeView" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="upView" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnviewGSTDocs" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">Supplier Details -  
                                <asp:Label ID="lblSuppliername_h" runat="server" />
                            </h4>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="docdiv" class="docdiv">
                                <div class="inner-container" style="text-transform: uppercase;">
                                    <div id="enquirydetailsdiv" class="enquirydetailsdiv" runat="server">
                                        <div class="col-sm-12">
                                            <div class="col-sm-4">
                                                <div>
                                                    <label class="form-label">
                                                        Address 
                                                    </label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="lblAddress" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="text-left">
                                                    <label class="form-label">
                                                        Contact Person
                                                    </label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="lblContactperson" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="text-left">
                                                    <label class="form-label">
                                                        Contact No
                                                    </label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="lblcontactno" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <div class="text-left">
                                                    <label class="form-label">
                                                        Fax
                                                    </label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="lblfax" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="text-left">
                                                    <label class="form-label">
                                                        Email Id</label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="lblEmailId" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="text-left">
                                                    <label class="form-label">
                                                        GST No</label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="lblGSTNo" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <div class="text-left">
                                                    <label class="form-label">
                                                        Sub Vendor
                                                    </label>
                                                </div>
                                                <div>
                                                    <asp:Label ID="lblSubVendor" runat="server" />
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="text-left">
                                                    <label class="form-label">
                                                        GSTN Details</label>
                                                </div>
                                                <div>
                                                    <asp:LinkButton ID="btnviewGSTDocs" OnClick="btnviewGSTDocs_Click" runat="server">   
                                                        <img src="../Assets/images/view.png" alt="" />
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hdnAttachementID" runat="server" Value="0" />
                        <div class="modal-footer">
                            <div class="col-sm-12 p-t-10">
                                <div class="col-sm-10">
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                    </div>
                                </div>
                                <iframe id="ifrm" visible="false" runat="server"></iframe>
                            </div>
                        </div>
                        <asp:HiddenField ID="hdnAttachementFlag" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>


</asp:Content>
