<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="RawMaterialInspectionReport.aspx.cs" Inherits="Pages_RawMaterialInspectionReport" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function Validate() {
            var res = true;
            var msg = Mandatorycheck('ContentPlaceHolder1_divInput');
            var Report = Mandatorycheck('ContentPlaceHolder1_divLPIReport');
            if (res)
                return true;
            else {
                hideLoader();
                return false;
            }
        }

        function clearFields() {
            $('#ContentPlaceHolder1_gvPartDetails').find('[type="text"]').val('')
        }

        function deleteConfirm(RIRHID) {
            swal({
                title: "Are you sure?",
                text: "If Yes,record will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deleteVERID', RIRHID);
            });
            return false;
        }

        function Print(RIRHID) {
            __doPostBack('Print', RIRHID);
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
                                    <h3 class="page-title-head d-inline-block">Raw Material Inspection Report</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Quality</a></li>
                                <li class="active breadcrumb-item" aria-current="page">Raw Material Inspection Report</li>
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
            <asp:UpdatePanel ID="upQuote" runat="server">
                <Triggers>
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="div" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="" id="divInput" runat="server">
                                        <div id="divAdd" runat="server">
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4 text-right">
                                                    <label class="form-label">
                                                        Customer Name</label>
                                                </div>
                                                <div class="col-sm-6 text-left">
                                                    <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="true" CssClass="form-control"
                                                        OnSelectedIndexChanged="ddlCustomerName_OnSelectedIndexChanged" Width="70%" ToolTip="Select Customer Number">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4 text-right">
                                                    <label class="form-label">
                                                        RFP No</label>
                                                </div>
                                                <div class="col-sm-6 text-left">
                                                    <asp:DropDownList ID="ddlRFPNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRFPNo_SelectIndexChanged"
                                                        CssClass="form-control" Width="70%" ToolTip="Select RFP No">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4 text-right">
                                                    <label class="form-label">
                                                        Item Name
                                                    </label>
                                                </div>
                                                <div class="col-sm-6 text-left">
                                                    <asp:DropDownList ID="ddlItemName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlItemName_SelectIndexChanged"
                                                        CssClass="form-control" Width="70%" ToolTip="Select Item No">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-2">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="p-t-10 p-l-0 p-r-0">
                                            <div id="divRawMaterialInspection"
                                                runat="server">
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-4">
                                                        <div class="text-left mandatorylbl">
                                                            <label>Test Date </label>
                                                        </div>
                                                        <div>
                                                            <asp:TextBox ID="txtTestDate" CssClass="form-control datepicker mandatoryfield" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="text-left">
                                                            <label class="mandatorylbl">BSL No  </label>
                                                        </div>
                                                        <div>
                                                            <asp:TextBox ID="txtBSLNo" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                        <div class="text-left">
                                                            <label class="mandatorylbl">QTY </label>
                                                        </div>
                                                        <div>
                                                            <asp:TextBox ID="txtQTY" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <asp:GridView ID="gvPartDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                        EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium" DataKeyNames="MPID">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                                HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Part No" ItemStyle-HorizontalAlign="left"
                                                                HeaderStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPartNo" runat="server" Text='<%# Eval("PartNo")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Part Name" ItemStyle-HorizontalAlign="left"
                                                                HeaderStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPartName" runat="server" Text='<%# Eval("PartName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Category Name" ItemStyle-HorizontalAlign="left"
                                                                HeaderStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcategoryName" runat="server" Text='<%# Eval("CategoryName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="THK" ItemStyle-HorizontalAlign="left"
                                                                HeaderStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblthk" runat="server" Text='<%# Eval("THKValue")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Grade Name" ItemStyle-HorizontalAlign="left"
                                                                HeaderStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgradeName" runat="server" Text='<%# Eval("GradeName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MRN No" ItemStyle-HorizontalAlign="left"
                                                                HeaderStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtMRNNo" runat="server" CssClass="form-control mandatoryfield" Text='<%# Eval("MRNNumber")%>'></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Material TC No" ItemStyle-HorizontalAlign="left"
                                                                HeaderStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtMatTCNo" CssClass="form-control mandatoryfield" Text='<%# Eval("MaterialTCNo")%>' runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Heat/Plate No" ItemStyle-HorizontalAlign="left"
                                                                HeaderStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtheatplateNo" CssClass="form-control mandatoryfield" Text='<%# Eval("PlateNo")%>' runat="server"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-4">
                                                        <label class="mandatorylbl">Remarks </label>
                                                    </div>
                                                    <div class="col-sm-8">
                                                        <asp:TextBox ID="txtRemarks" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                    </div>

                                                </div>
                                                <div class="col-sm-12 text-center p-t-10">
                                                    <asp:LinkButton ID="btnSaveRawMIR" runat="server" CssClass="btn btn-cons btn-save AlignTop"
                                                        Text="Save" OnClientClick="return Validate('ContentPlaceHolder1_divRawMaterialInspection');"
                                                        OnClick="btnSaveRawMIR_Click">
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-cons btn-save AlignTop" Text="Cancel"
                                                        OnClick="btnCancel_Click">
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="divOutPut" class="col-sm-12 p-t-10" style="overflow-x: scroll;" runat="server">
                                        <asp:GridView ID="gvRawMIRHeader" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                            OnRowCommand="gvRawMIRHeader_OnrowCommand" DataKeyNames="RIRHID">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno1" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Report No" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                    HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReportNo" runat="server" Text='<%# Eval("ReportNo")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Report Date" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                    HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReportDate" runat="server" Text='<%# Eval("ReportDate")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditRMI" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                         <img src="../Assets/images/edit-ec.png" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnAdd" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("RIRHID")) %>'>
                                                         <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PDF" ItemStyle-CssClass="text-center" HeaderStyle-Width="10%"
                                                    ItemStyle-Width="10%" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnpdf" runat="server"
                                                            OnClientClick='<%# string.Format("return Print({0});",Eval("RIRHID")) %>'
                                                            CommandName="PDFRMI"><img src="../Assets/images/pdf.png" />
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                        <asp:HiddenField ID="hdnRIRHID" runat="server" Value="0" />
                                        <asp:HiddenField ID="hdnTotalItemQty" runat="server" Value="0" />
                                        <asp:HiddenField ID="hdnAddress" runat="server" Value="" />
                                        <asp:HiddenField ID="hdnPhoneAndFaxNo" runat="server" Value="" />
                                        <asp:HiddenField ID="hdnEmail" runat="server" Value="" />
                                        <asp:HiddenField ID="hdnWebsite" runat="server" Value="" />
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

