<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="AddtionalPartApproval.aspx.cs" Inherits="Pages_AddtionalPartApproval" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function checkAll(ele) {
            debugger;
            if ($(ele).is(":checked")) {
                //if ($(ele).closest('table').find('[type="checkbox"]').closest('span').attr('css'))
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', true);
            }
            else {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', false);
            }
        }

        function ValidateApproval() {
            if ($('#ContentPlaceHolder1_gvBOMCostDetails').find('input:checked').not('#ContentPlaceHolder1_gvBOMCostDetails_chkall').length < 1) {
                ErrorMessage('Error', 'No Part Slected');
                return false;
            }
            else {
                return true;
            }
        }
        //function ShowDatatable() {
        //    $('.table').DataTable({
        //        "retrieve": true,
        //        "pageLength": 50,
        //        "bStateSave": true,
        //        "fnStateSave": function (oSettings, oData) {
        //            localStorage.setItem('DataTables_' + window.location.pathname, JSON.stringify(oData));
        //        },
        //        "fnStateLoad": function (oSettings) {
        //            var data = localStorage.getItem('DataTables_' + window.location.pathname);
        //            return JSON.parse(data);
        //        },
        //        "stateSaveParams": function (settings, data) {
        //            data.length = 50;
        //        },
        //    });
        //}

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
                                    <h3 class="page-title-head d-inline-block">Addtional Part Approval</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Production</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Addtional Part Approval</li>
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
                                        <%--OnRowDataBound="gvBOMCostDetails_OnRowDataBound"--%>
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvBOMCostDetails" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium uniquedatatable"
                                                HeaderStyle-HorizontalAlign="Center"
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
                                                    <asp:TemplateField HeaderText="RFP No" HeaderStyle-Width="200px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRFPNo" runat="server" Text='<%# Eval("RFPNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Part Name" HeaderStyle-Width="200px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartName" runat="server" Text='<%# Eval("MaterialName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Name" HeaderStyle-Width="200px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="QTY" HeaderStyle-Width="200px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblqty" runat="server" Text='<%# Eval("QTY")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="AWT" HeaderStyle-Width="200px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAWT" runat="server" Text='<%# Eval("AWT")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="WT" HeaderStyle-Width="200px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblWT" runat="server" Text='<%# Eval("WT")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Grade Name" HeaderStyle-Width="200px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGradeName" runat="server" Text='<%# Eval("GradeName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="THK" HeaderStyle-Width="200px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTHK" runat="server" Text='<%# Eval("THK")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Request On" HeaderStyle-Width="200px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPAPRequestedDate" runat="server" Text='<%# Eval("PAPRequestedDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Request by" HeaderStyle-Width="200px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRequestBy" runat="server" Text='<%# Eval("RequestedBy")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="200px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvApprovedDetails" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                HeaderStyle-HorizontalAlign="Center"
                                                DataKeyNames="DDID,BOMID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="RFP No" HeaderStyle-Width="200px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRFPNo" runat="server" Text='<%# Eval("RFPNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Part Name" HeaderStyle-Width="200px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartName" runat="server" Text='<%# Eval("MaterialName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Name" HeaderStyle-Width="200px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="QTY" HeaderStyle-Width="200px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblqty" runat="server" Text='<%# Eval("QTY")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="AWT" HeaderStyle-Width="200px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAWT" runat="server" Text='<%# Eval("AWT")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="WT" HeaderStyle-Width="200px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblWT" runat="server" Text='<%# Eval("WT")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Grade Name" HeaderStyle-Width="200px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGradeName" runat="server" Text='<%# Eval("GradeName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="THK" HeaderStyle-Width="200px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTHK" runat="server" Text='<%# Eval("THK")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Request On" HeaderStyle-Width="200px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPAPRequestedDate" runat="server" Text='<%# Eval("PAPRequestedDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Request by" HeaderStyle-Width="200px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRequestBy" runat="server" Text='<%# Eval("RequestedBy")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Approved by" HeaderStyle-Width="200px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblapprovedby" runat="server" Text='<%# Eval("ApprovedBy")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Aproved On" HeaderStyle-Width="200px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblapprovedon" runat="server" Text='<%# Eval("ApprovedOn")%>'></asp:Label>
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
