<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="PurchaseIndentApproval.aspx.cs" Inherits="Pages_PurchaseIndentApproval" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function UpdatePIStatus() {
            swal({
                title: "No Further Edit Once Shared.",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Save it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('UpdatePIStatus', null);
            });
            return false;
        }

        function checkAll(ele) {
            debugger;
            if ($(ele).is(":checked")) {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', true);
            }
            else {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', false);
            }
        }

        function ViewFileName(RowIndex) {
            __doPostBack("ViewIndentCopy", RowIndex);
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
                                    <h3 class="page-title-head d-inline-block">Purchase Indent Approval</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Production</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Purchase Indent Approval</li>
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
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" runat="server">
                            <div class="ip-div text-center">
                            </div>
                        </div>
                        <div id="divradio" runat="server">
                            <div class="text-center">
                                <div class="col-sm-12 text-center" style="padding-left: 35%;">
                                </div>
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Indent Number</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlIndentNumber" runat="server" AutoPostBack="true" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlIndentNumber_OnselectedIndexChanged" Width="70%" ToolTip="Select Indent Number">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
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
                                        <div runat="server">
                                            <div class="row">
                                                <div class="col-md-6">
                                                    <p class="h-style">
                                                        <asp:Label ID="lbltitle" runat="server"></asp:Label>
                                                    </p>
                                                </div>
                                                <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-3">
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:LinkButton ID="btnApprove" CssClass="btn btn-success" runat="server" Text="Approve"
                                                        CommandName="Approve" OnClick="btnApprove_Click"></asp:LinkButton>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:LinkButton ID="btnReject" CssClass="btn btn-success" runat="server" Text="Reject"
                                                        CommandName="Reject" OnClick="btnApprove_Click"></asp:LinkButton>
                                                </div>
                                                <div class="col-sm-3">
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <label>RFP No: </label>
                                                <asp:Label ID="lblRFPNo" Style="font-weight: bold; font-size: 15px;" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvViewPurchaseIndent" runat="server" AutoGenerateColumns="False"
                                                    ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                    HeaderStyle-HorizontalAlign="Center" DataKeyNames="PID,IndentFileName">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="" ControlStyle-CssClass="nosort" ItemStyle-HorizontalAlign="Center">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkall" runat="server" onclick="return checkAll(this);" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkitems" runat="server" AutoPostBack="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="RFP No" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRFPNo" runat="server" Text=' <%# Eval("RFPNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Material Grade" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMaterialGrade" runat="server" Text=' <%# Eval("GradeName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Material Thickness" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMaterialThickness" runat="server" Text=' <%# Eval("THKValue")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Material Category" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMaterialCategory" runat="server" Text=' <%# Eval("CategoryName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Material Type" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMaterialType" runat="server" Text=' <%# Eval("TypeName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Measurment" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblmeasurment" runat="server" Text=' <%# Eval("Measurment")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="UOM Name" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblUOMName" runat="server" Text=' <%# Eval("UOMName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delivery Dates" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDeliveryDate" runat="server" Text=' <%# Eval("DeliveryDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Drawing Name" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDrawingname" runat="server" Text=' <%# Eval("Drawingname")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRemarks" runat="server" Text=' <%# Eval("Remarks")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="QC List" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQCList" runat="server" Text=' <%# Eval("Certificates")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Required QTY" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRequiredWeight" runat="server" Text=' <%# Eval("RequiredWeight")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="View File" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnDeviationFile" runat="server"
                                                                    OnClientClick='<%# string.Format("return ViewFileName({0});",((GridViewRow) Container).RowIndex) %>'>       
                                                                    <img src="../Assets/images/view.png" />
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <label class="form-label mandatorylbl">
                                                        Remarks</label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-4">
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-6">
                                                    <lable class="form-label">Indent By</lable>
                                                    <asp:Label ID="lblIndentBy" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-6 text-right">
                                                    <lable class="form-label">Indent Date</lable>
                                                    <asp:Label ID="lblIndentDate" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hdnBOMId" Value="0" runat="server" />
                    <iframe id="ifrm" style="display: none;" runat="server"></iframe>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>
