<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="InterDevelopementApproval.aspx.cs" Inherits="Pages_InterDevelopementApproval" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">

        function checkAll(ele) {
            debugger;
            if ($(ele).is(":checked")) {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', true);
            }
            else {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', false);
            }
        }

        function ValidateApproval() {
            if ($('#ContentPlaceHolder1_gvInternalDevelopementApprovalDetails').find('input:checked').not('#ContentPlaceHolder1_gvInternalDevelopementApprovalDetails_chkall').length < 1) {
                ErrorMessage('Error', 'No Part Slected');
                return false;
            }
            else {
                return true;
            }
        }

        $(document).ready(function () {
            $('#ContentPlaceHolder1_gvInternalDevelopementApprovedDetails').DataTable({
                "retrieve": true,
                "pageLength": 50,
                "bSort": false,  
                //responsive: true,            
                responsive: true,
                columnDefs: [
                    { responsivePriority: 1, targets: 0 },
                    { responsivePriority: 10001, targets: 7 },
                    { responsivePriority: 2, targets: -2 }
                ]
            });
        });

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
                                    <h3 class="page-title-head d-inline-block">Internal Developement Approval</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Production</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Internal Developement Approval</li>
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
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" class="btnAddNew" runat="server">
                            <div class="text-center">
                            </div>
                        </div>
                        <div id="divInput" class="divInput" runat="server">
                            <div class="ip-div text-center">
                            </div>
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
                                        <div class="col-sm-12 text-center">
                                            <asp:Button ID="btnApprove" CssClass="btn btn-cons btn-save" runat="server" Text="Approve"
                                                OnClientClick="return ValidateApproval();" CommandName="Approve" OnClick="btnApprove_Click"></asp:Button>
                                            <asp:Button ID="btnReject" CssClass="btn btn-cons btn-save" runat="server" Text="Reject"
                                                OnClientClick="return ValidateApproval();" CommandName="Reject" OnClick="btnApprove_Click"></asp:Button>
                                        </div>
                                        <div class="col-sm-12 text-center">
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvInternalDevelopementApprovalDetails" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                OnRowDataBound="gvInternalDevelopementApprovalDetails_OnRowDataBound" HeaderStyle-HorizontalAlign="Center"
                                                DataKeyNames="DDID,BOMID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" ControlStyle-CssClass="nosort" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkall" runat="server" onclick="return checkAll(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkitems" runat="server" AutoPostBack="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="RFP No" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="200px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRFPNo" runat="server" Text='<%# Eval("RFPNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Name" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="200px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Part Name" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="200px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartname" runat="server" Text='<%# Eval("MaterialName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Requested QTY" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="200px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRequestedQty" runat="server" Text='<%# Eval("Quantity")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Total Weight" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="200px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltotalweight" runat="server" Text='<%# Eval("WT")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Requested By" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="200px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRequestedBy" runat="server" Text='<%# Eval("RaisedBy")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Requested Date" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="200px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRequestedDate" runat="server" Text='<%# Eval("RaisedDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                

                                                    <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="200px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Reason" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="200px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIDBOMRemarks" Text='<%# Eval("BomPartRemarks")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <label style="font-weight: bold; color: brown;">INTERNAL DEVELOPEMENT APPROVED DETAILS  </label>
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvInternalDevelopementApprovedDetails" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium uniquedatatable"
                                                OnRowDataBound="gvInternalDevelopementApprovalDetails_OnRowDataBound" HeaderStyle-HorizontalAlign="Center"
                                                DataKeyNames="DDID,BOMID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="RFP No" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="200px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRFPNo" runat="server" Text='<%# Eval("RFPNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Name" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="200px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Part Name" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="200px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartname" runat="server" Text='<%# Eval("MaterialName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Requested QTY" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="200px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRequestedQty" runat="server" Text='<%# Eval("Quantity")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Total Weight" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="200px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltotalweight" runat="server" Text='<%# Eval("WT")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Approved By" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="200px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblapprovedBy" runat="server" Text='<%# Eval("ApprovedBy")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Approved Date" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="200px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblapprovedDate" runat="server" Text='<%# Eval("ApprovedDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Requested By" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="200px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRequestedBy" runat="server" Text='<%# Eval("RaisedBy")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Requested Date" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="200px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRequestedDate" runat="server" Text='<%# Eval("RaisedDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Reason" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="200px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIDBOMRemarks" Text='<%# Eval("BomPartRemarks")%>' runat="server"></asp:Label>
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>


