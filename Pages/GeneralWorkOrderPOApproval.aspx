<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="GeneralWorkOrderPOApproval.aspx.cs" Inherits="Pages_GeneralWorkOrderPOApproval" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <script type="text/javascript" >
        function ValidateAll() {
            var checked = $('#ContentPlaceHolder1_gvGeneralWorkOrderPOApproval').find('input:checked').not('#ContentPlaceHolder1_gvGeneralWorkOrderPOApproval_chkall').length;
            if (checked > 0) {
                if ($(event.target).attr('id') == 'ContentPlaceHolder1_btnReject') {
                    if ($('#ContentPlaceHolder1_txtRemarks').val() == '') {
                        ErrorMessage('Error', 'Plese Enter Remarks');
                        return false;
                    }
                    else {
                        return true;
                    }
                }
                else
                    return true;
            }
            else {
                ErrorMessage('Error', 'Please Select Item');
                return false;
            }
        }
            function checkAllItems(ele) {
            debugger;
            if ($(ele).is(":checked")) {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', true);

                $(ele).closest('table').find('[type="text"]').addClass("mandatoryfield");
                $(ele).closest('table').find('[type="text"]').before('<span class="textboxmandatory" style="color: red;">*</span>');
            }
            else {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', false);
                $(ele).closest('table').find('[type="text"]').removeClass("mandatoryfield");
                $(ele).closest('table').find('[type="text"]').closest('td').find('.textboxmandatory').remove()
            }
            }

            function ViewIndentAttach(index) {
                __doPostBack('ViewIndentAttach', index);
                return false;
            }
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                                    <h3 class="page-title-head d-inline-block">General Work Order PO Approval</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Purchase</a></li>
                                <li class="active breadcrumb-item" aria-current="page">General Work Order PO Approval</li>
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
                    <asp:PostBackTrigger ControlID="gvGeneralWorkOrderPOApproval" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" class="btnAddNew" runat="server">
                        </div>
                        <div id="divInput" runat="server">
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="col-md-12 row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                                    <asp:Label ID="lbltitle" runat="server"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="overflow-x:auto;">
                                            
                                            <asp:GridView ID="gvGeneralWorkOrderPOApproval" runat="server" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found"
                                                OnRowDataBound="gvGeneralWorkOrderPOApproval_OnRowDataBound"
                                                 OnRowCommand="gvGeneralWorkOrderPOApproval_OnRowCommand"
                                                CssClass="table table-hover table-bordered medium" Font-Names="arial" Font-Size="10px" DataKeyNames="SGWOID,GWOID,FileName"  >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkall" HeaderText="Check List" runat="server" onclick="return checkAllItems(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkitems" runat="server"
                                                                AutoPostBack="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="POStatus" runat="server" Text='<%# Eval("POStatus")%>' Visible="false"></asp:Label>
                                                            <asp:Label ID="lblApprovalStatus" runat="server" Text='<%# Eval("ApprovalStatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="GWI No" SortExpression="General Work Order Indent Number" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                             <asp:Label ID="lblGWOID" runat="server" Text='<%# Eval("GWOID") %>' Visible="false" ></asp:Label>
                                                                    <asp:Label ID="lblGWI" Text=' <%# Eval("GWI")%>' runat="server"></asp:Label>
                                                               
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Material Quantity" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMaterialQuantity" runat="server" Text='<%# Eval("MaterialQuantity")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Material Unitcost" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMaterialUnitcost" runat="server" Text='<%# Eval("MaterialUnitcost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="GWO Indent Date" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGWOIDate" runat="server" Text='<%# Eval("GWOIndentDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="GWO Indent By" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGWOIBy" runat="server" Text='<%# Eval("FirstName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="GWO Indent To" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGWOITo" runat="server" Text='<%# Eval("IndentName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Service Description" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblServiceDescription" runat="server" Text='<%# Eval("ServiceDescription")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Quantity" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("QuantityUnit")%>' ></asp:Label>
                                                             <asp:Label ID="Name" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Job List" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="JobListId" runat="server" Value='<%# Bind("JobListId") %>' />
                                                            <asp:Label ID="lblJobList" runat="server" Text='<%# Eval("JobList")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Remarks" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View Attach">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnViewIndentAttach" runat="server"
                                                                Text="View Attach"
                                                                OnClientClick='<%# string.Format("return ViewIndentAttach({0});",((GridViewRow) Container).RowIndex) %>'> <img src="../Assets/images/view.png" alt=""/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     
                                                </Columns>
                                            </asp:GridView>
                                            
                        <iframe id="ifrm" visible="false" runat="server"></iframe>
                                        </div>
                                        <div class="col-sm-12 text-center p-t-10">
                                            <div class="col-sm-4">
                                                <label id="lblRemarks">Remarks</label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control mandatoryfield"
                                                    TextMode="MultiLine" Rows="3" autocomplete="nope" placeholder="Enter Remarks">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 text-center p-t-10">
                                            <asp:LinkButton ID="btnApprove" Text="Approve" CssClass="btn btn-cons btn-success" runat="server" OnClientClick="return ValidateAll();"
                                                OnClick="btnApprovalReject_Click" CommandName="Approve"></asp:LinkButton>
                                            <asp:LinkButton ID="btnReject" CssClass="btn btn-cons btn-success" Text="Reject" runat="server" OnClientClick="return ValidateAll();"
                                                OnClick="btnApprovalReject_Click" CommandName="Reject"></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                   <asp:HiddenField ID="hdnSGWOID" Value="0" runat="server" />
                   <asp:HiddenField ID="hdnGWOID" Value="0" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>


