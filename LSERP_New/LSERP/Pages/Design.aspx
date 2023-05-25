<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="Design.aspx.cs" Inherits="Pages_Design" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function ShowAddPopUp() {
            $('#mpeAdd').modal({
                backdrop: 'static'
            });
            return false;
        }

        function ShowViewPopUp() {
            $('#mpeView').modal({
                backdrop: 'static'
            });
            return false;
        }

        function ValidateAttchements() {

            var msg = true;

            if ($('#<%=txtRemarks_A.ClientID %>').val() == "") {
                $('#<%=txtRemarks_A.ClientID %>').notify('Fields Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }
            if ($('#<%=fAttachment.ClientID %>').val() == "") {
                $('#<%=fAttachment.ClientID %>').notify('Please Upload File', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }

            else if ($('#<%=fAttachment.ClientID %>').val() != "") {
                var fileExtension = ['jpeg', 'jpg', 'png', ];
                if ($.inArray($('#<%=fAttachment.ClientID %>').val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                    $('#<%=fAttachment.ClientID %>').notify('Invalid File.\n Please upload a File with extension: .jpg , .jpeg , .png', { arrowShow: true, position: 'r,r', autoHide: true });
                    msg = false;
                }
            }

            if (msg == true) {
                return true;
            }
            else {
                return false;
            }
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
                                        Design</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Design</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Design Upload</li>
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
                    <asp:PostBackTrigger ControlID="gvDesignDetails" />
                    <%--  <asp:PostBackTrigger ControlID="ddlEnquiryNumber" />--%>
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" runat="server">
                            <div class="ip-div text-center">
                            </div>
                        </div>
                        <div id="divInput" runat="server">
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
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server" visible="false">
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
                                            <div class="col-sm-3">
                                                <label class="form-label">
                                                    Customer Order Number</label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:Label ID="lblCustomerOrderNumber" Text="" runat="server" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-3">
                                                <label class="form-label">
                                                    Client Name</label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:Label ID="lblClientName" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-3">
                                                <label class="form-label">
                                                    Project Name</label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:Label ID="lblProjectName" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-3">
                                                <label class="form-label">
                                                    Last Drawing Submitted On</label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:Label ID="lblLastDrawingSubmittedOn" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-3">
                                                <label class="form-label">
                                                    Sales Agent</label>
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:Label ID="lblSalesAgent" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-3">
                                            </div>
                                            <div class="col-sm-3">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvDesignDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                OnRowCommand="gvDesignDetails_OnRowCommand" OnRowDataBound="gvDesignDetails_RowDataBound"
                                                HeaderStyle-HorizontalAlign="Center" DataKeyNames="FileName">
                                                <Columns>
                                                    <%--   <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Version Number" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVersionNumber" runat="server" Text='<%# Eval("Version")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SubmittedOn" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSubmittedOn" runat="server" Text='<%# Eval("CreatedOn")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Shared With Customer" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSharedWithCustomer" runat="server" Text='<%# Eval("SharedWithCustomer")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Customer Approval Status" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCustomerApprovalStatus" runat="server" Text='<%# Eval("CustomerApprovalStatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgbtnView" runat="server" ImageUrl="" Width="20px" Height="20px"
                                                                CommandArgument='<%#((GridViewRow) Container).RowIndex %>'></asp:ImageButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-4">
                                            </div>
                                            <%--OnClick="lbtnNewDrawing_Click"--%>
                                            <div class="col-sm-4">
                                                <asp:LinkButton ID="lbtnNewDrawing" CssClass="btn btn-success add-emp" runat="server"
                                                    Text="Add New" Visible="false" OnClientClick="return ShowAddPopUp();"></asp:LinkButton>
                                            </div>
                                            <div class="col-sm-4">
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
    <div class="modal" id="mpeAdd">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="Updateview" runat="server" UpdateMode="Always">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSaveDrawing" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H1">
                                <div class="text-center">
                                    Add New Drawings/
                                    <asp:Label ID="lblEnquiryNumber_A" Text="" runat="server"></asp:Label>
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal">
                                x</button>
                        </div>
                        <div class="modal-body">
                            <div class="inner-container">
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4">
                                        <label class="form-label">
                                            New Version Of Drawing Number</label>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Label ID="lblVersionNumber_A" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-3">
                                        <label class="form-label">
                                            Add File</label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:FileUpload ID="fAttachment" runat="server" class="form-control" ClientIDMode="Static"
                                            Width="95%" />
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-3">
                                        <label class="form-label">
                                            Add Remarks</label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtRemarks_A" runat="server" TextMode="MultiLine" Rows="3" Width="95%"
                                            placeHolder="Enter Remarks" ToolTip="Enter remarks"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="col-sm-12 text-center">
                                <asp:LinkButton ID="btnSaveDrawing" runat="server" Text="Save&Share" OnClientClick="return ValidateAttchements();"
                                    OnClick="btnSaveDrawing_Click" CssClass="btn btn-cons btn-save  AlignTop" />
                                <%--  <asp:LinkButton ID="lbtnCancel" runat="server" Text="Cancel" CssClass="btn btn-cons btn-danger AlignTop"
                                    OnClientClick="return ClearFields();"></asp:LinkButton>--%>
                            </div>
                        </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:HiddenField ID="hdnECID" runat="server" Value="" />
            </div>
        </div>
    </div>
    <div class="modal" id="mpeView">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSaveDrawing" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H2">
                                <div class="text-center">
                                    Drawings
                                    <asp:Label ID="Label1" Text="" runat="server"></asp:Label>
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal">
                                x</button>
                        </div>
                        <div class="modal-body">
                            <div class="inner-container">
                                <div class="col-sm-12">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-8">
                                        <asp:Image ID="imgDocs" ImageUrl="" Height="100%" Width="100%" runat="server" />
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                        </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:HiddenField ID="HiddenField1" runat="server" Value="" />
            </div>
        </div>
    </div>
</asp:Content>
