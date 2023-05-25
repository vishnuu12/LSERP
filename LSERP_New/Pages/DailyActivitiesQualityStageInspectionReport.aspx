<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="DailyActivitiesQualityStageInspectionReport.aspx.cs"
    Inherits="Pages_DailyActivitiesQualityStageInspectionReport" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function GetRadioButtonListSelectedValue(ele) {
            if ($('#ContentPlaceHolder1_rblReSchduleSubmissiondate input:checked').val() == "Yes") {
                $('#divRSDate').attr('style', 'display:block;');
                $('#ContentPlaceHolder1_txtreschduledate').addClass('mandatoryfield');
            }
            else {
                $('#divRSDate').attr('style', 'display:none;');
                $('#ContentPlaceHolder1_txtreschduledate').removeClass('mandatoryfield');
            }
        }

        function deleteConfirm(DASEDID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Part Name will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('Delete', DASEDID);
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
                                    <h3 class="page-title-head d-inline-block">QC Stage Inspection Report</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Quality</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">QC Stage Inspection Report</li>
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
            <asp:UpdatePanel ID="upCustomers" runat="server">
                <Triggers>
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            RFP No
                                        </label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlRFPNo" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlRFPNo_SelectIndexChanged"
                                            CssClass="form-control" Width="70%" ToolTip="Select RFP No">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divAddnew" runat="server">
                            <div class="ip-div text-center">
                                <%-- <div class="col-sm-12 p-t-10">
                                    <asp:LinkButton ID="btnAddNew" runat="server" Text="Add New" OnClick="btnAddNew_Click"
                                        CssClass="btn ic-w-btn add-btn"></asp:LinkButton>
                                </div>--%>
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12 p-t-20">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Stage Inspection
                                        </label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlstageInspection" runat="server"
                                            CssClass="form-control mandatoryfield" Width="70%" ToolTip="Select Enquiry Number">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Stage 1" Value="Stage 1"></asp:ListItem>
                                            <asp:ListItem Text="Stage 2" Value="Stage 2"></asp:ListItem>
                                            <asp:ListItem Text="Stage 3" Value="Stage 3"></asp:ListItem>
                                            <asp:ListItem Text="Raw Material Inspection" Value="Raw Material Inspection"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Raised By
                                        </label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlRaisedBy" runat="server"
                                            CssClass="form-control" Width="70%" ToolTip="Select Raised By">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Remarks
                                        </label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:TextBox ID="txtRemarks" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <asp:LinkButton ID="btnSave" runat="server" Text="Save"
                                        OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divInput');"
                                        CssClass="btn btn-cons btn-success" OnClick="btnSave_Click"></asp:LinkButton>
                                    <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel"
                                        CssClass="btn btn-cons btn-success" OnClick="btnCancel_Click"></asp:LinkButton>
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
                                                    <asp:Label ID="lbltitle" runat="server" Text=""></asp:Label>
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server">
                                            </div>
                                        </div>

                                        <div class="col-sm-12" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvDailyActivitiesReport" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover no-more-tables"
                                                Width="100%" ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" DataKeyNames="DAQSIRDID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="RFP No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRFPNo" runat="server" Text='<%# Eval("RFPNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Stage Inspection">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStageInspection" runat="server" Text='<%# Eval("StageInspection") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblremarks" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDelete" runat="server"
                                                                OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("DAQSIRDID")) %>'>
                                                            <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnCustomerID" runat="server" Value="0" />
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


