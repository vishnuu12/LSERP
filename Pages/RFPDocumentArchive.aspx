<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="RFPDocumentArchive.aspx.cs" Inherits="Pages_RFPDocumentArchive" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">

        function ViewPriceAttach(index) {
            __doPostBack('ViewRFPDocs', index);
            return false;
        }

        function deleteConfirm(PartId) {
            swal({
                title: "Are you sure?",
                text: "If Yes,the Document will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deleteRFPDocumentArchive', PartId);
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
                                        <img src="../Assets/images/pages.png" alt="" width="16px" />
                                    </span>
                                    <h3 class="page-title-head d-inline-block">RFP Document Archive</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Archive</a></li>
                                <li class="active breadcrumb-item" aria-current="page">RFP Document Archive</li>
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
                    <asp:PostBackTrigger ControlID="btnsubmit" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divaddnew" runat="server">
                            <div class="ip-div text-center">
                            </div>
                        </div>
                        <div id="divAdd" runat="server">
                            <div class="ip-div text-center">
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div id="divRFPDoc" runat="server" style="background: lightblue; height: 284px;">
                                    <div class="col-sm-12">
                                        <div class="col-sm-4 text-left">
                                            <label class="form-label">
                                                Customer Name</label>
                                            <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="true" CssClass="form-control mandatoryfield"
                                                OnSelectedIndexChanged="ddlCustomerName_SelectIndexChanged" Width="70%" ToolTip="Select Enquiry Number">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4 text-left">
                                            <label class="form-label mandatorylbl">
                                                Enquiry Number</label>
                                            <asp:DropDownList ID="ddlEnquiryNumber" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEnquiryNumber_SelectIndexChanged"
                                                CssClass="form-control mandatoryfield" Width="70%" ToolTip="Select Enquiry Number">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4 text-left">
                                            <label class="form-label mandatorylbl">Offer No </label>
                                            <asp:DropDownList ID="ddlOfferNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOfferNo_SelectIndexChanged"
                                                CssClass="form-control mandatoryfield" Width="70%" ToolTip="Select Offer No">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4  text-left">
                                            <label class="form-label mandatorylbl">
                                                RFP No
                                            </label>
                                            <asp:DropDownList ID="ddlRFPNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRFPNo_SelectIndexChanged"
                                                CssClass="form-control mandatoryfield" Width="70%" ToolTip="Select RFP No">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4 text-left">
                                            <label class="form-label mandatorylbl">Department </label>
                                            <asp:DropDownList ID="ddldepartment" runat="server" AutoPostBack="false"
                                                CssClass="form-control mandatoryfield" Width="70%" ToolTip="Select Department Name">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4 text-left">
                                            <label class="form-label mandatorylbl">Attachment </label>
                                            <asp:FileUpload ID="fAttachment" runat="server" TabIndex="12" CssClass="form-control mandatoryfield Attachement"
                                                onchange="DocValidation(this);"></asp:FileUpload>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4  text-left">
                                            <label class="form-label mandatorylbl">File Name </label>
                                            <asp:TextBox ID="txtfilename" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-8 text-left">
                                            <label class="form-label mandatorylbl">Remarks </label>
                                            <asp:TextBox ID="txtremarks" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control mandatoryfield"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-20">
                                    <div class="col-sm-3">
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:LinkButton ID="btnsubmit" CssClass="btn btn-success" runat="server"
                                            Text="Submit" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divInput')" OnClick="btnsubmit_Click"></asp:LinkButton>
                                        <asp:LinkButton ID="btncancel" CssClass="btn btn-success" runat="server"
                                            Text="Cancel" OnClick="btncancel_Click"></asp:LinkButton>
                                    </div>
                                    <div class="col-sm-3">
                                    </div>
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
                                                </p>
                                            </div>

                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                            <div class="col-sm-12 blink_me text-center">
                                            </div>

                                            <div class="col-sm-12 p-t-10">
                                                <asp:GridView ID="gvRFPDocumentArchiveDetails" OnRowCommand="gvPriceestimationDetails_OnrowCommand"
                                                    runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium orderingfalse"
                                                    DataKeyNames="AttachementName,EnquiryID">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="EnquiryNo" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left"
                                                            HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEnquiryNo" runat="server" Text='<%# Eval("EnquiryID")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="RFP No" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left"
                                                            HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRFPNo" runat="server" Text='<%# Eval("RFPNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Customer Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left"
                                                            HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcustomername" runat="server" Text='<%# Eval("ProspectName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Department Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbldepartmentname" runat="server" Text='<%# Eval("DepartmentName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="View Attach">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnWOView" runat="server"
                                                                    OnClientClick='<%# string.Format("return ViewPriceAttach({0});",((GridViewRow) Container).RowIndex) %>'><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--    <asp:TemplateField HeaderText="Edit">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnEdit" runat="server"
                                                                    CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                    CommandName="EditPriceEstimation">
                                                                    <img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnDelete" runat="server"
                                                                    OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("EPPEID")) %>'>
                                                            <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Delete">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnDelete" runat="server"
                                                                    OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("RFPDAID")) %>'>
                                                            <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:HiddenField runat="server" ID="hdnEPPEID" Value="0" />
                                                <iframe id="ifrm" style="display: none;" runat="server"></iframe>
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

