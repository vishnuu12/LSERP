<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="WPSMaster.aspx.cs"
    Inherits="Pages_WPSMaster" ClientIDMode="Predictable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function deleteConfirm(CustomerID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Customer Name will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deletegvrow', CustomerID);
            });
            return false;
        }
        function ViewWPSFile(index) {
            __doPostBack("ViewWPSAttach", index);
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
                                    <h3 class="page-title-head d-inline-block">WPS Master </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Accounts</a></li>
                                <li class="active breadcrumb-item" aria-current="page">WPS</li>
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
                    <asp:PostBackTrigger ControlID="btnWPS" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" runat="server">
                            <div class="ip-div text-center">
                                <asp:LinkButton ID="btnAddNew" runat="server" Text="Add New WPS" OnClick="btnAddNew_click"
                                    CssClass="btn ic-w-btn add-btn"></asp:LinkButton>
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">

                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label>WPS No  </label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtWPSNo" placeholder="WPS No" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label>Material Grade </label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtMaterialGrade" placeholder="Material Grade" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
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
                                            <label>Thickness </label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtThickness" placeholder="Thickness" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label>Process </label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtProcess" placeholder="Process" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
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
                                            <label>Filler Grade </label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtFillerGrade" placeholder="Enter Filler Grade" runat="server" autocomplete="nope"
                                                CssClass="form-control mandatoryfield"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label>Amps </label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtAmps" placeholder="Enter Amps" runat="server" autocomplete="nope"
                                                CssClass="form-control mandatoryfield"></asp:TextBox>
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
                                            <label>Polarity </label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtPolarity" runat="server" placeholder="Enter Polarity"
                                                CssClass="form-control mandatoryfield" autocomplete="nope"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label>Gas Level </label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtGasLevel" runat="server" placeholder="Enter Gas Level"
                                                CssClass="form-control mandatoryfield" autocomplete="nope"></asp:TextBox>
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
                                            <label>Attach File </label>
                                        </div>
                                        <div>
                                            <asp:FileUpload ID="fAttachment" runat="server" TabIndex="12" CssClass="form-control"
                                                onchange="DocValidation(this);" ClientIDMode="Static" Width="95%"></asp:FileUpload>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label>VOLT </label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtvoltage" runat="server" placeholder="Enter voltage"
                                                CssClass="form-control mandatoryfield" autocomplete="nope"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10 text-center">
                                    <asp:LinkButton ID="btnWPS" runat="server" CssClass="btn btn-cons btn-save AlignTop"
                                        OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divInput');" OnClick="btnWPS_Click" Text="Save WPS" />
                                    <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-cons btn-save AlignTop"
                                        OnClick="btnCancel_Click" Text="Cancel" />
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
                                                    <asp:Label ID="lbltitle" runat="server" Text="Customer Details"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>
                                        <div class="col-sm-12" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvWPSMaster" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover no-more-tables"
                                                Width="100%" ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" OnRowCommand="gvWPSMaster_RowCommand"
                                                OnRowDataBound="gvWPSMaster_OnRowDataBound" DataKeyNames="WPSID,AttachementName">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="WPS No" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblWPSNo" runat="server" Text='<%# Eval("WPSNumber") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Material Grade">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMaterialgrade" runat="server" Text='<%# Eval("MaterialGrade") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Thickness">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblThickness" runat="server" Text='<%# Eval("Thickness") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Process">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblprocess" runat="server" Text='<%# Eval("Process") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Filler Grade">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFillerGrade" runat="server" Text='<%# Eval("FillerGrade") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amps">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmps" runat="server" Text='<%# Eval("Amps") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="VOLT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVOLT" runat="server" Text='<%# Eval("Voltage") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Polarity">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPolarity" runat="server" Text='<%# Eval("Polarity") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Gas Level">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGasLevel" runat="server" Text='<%# Eval("Gaslevel") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="WPS File">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnWPSFile" runat="server" CommandName="ViewWPSFile"
                                                                OnClientClick='<%# string.Format("return ViewWPSFile({0});",((GridViewRow) Container).RowIndex) %>'>
                                                                <img src="../Assets/images/view.png" style="height:20px" alt=""></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnedit" runat="server" Text="Edit" CommandName="EditWPS"
                                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ToolTip="Edit"><img src="../Assets/images/edit-ec.png" style="height:20px" alt=""></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                    <%--   <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnDelete" runat="server" OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("WPSID")) %>'>
                                                            <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnWPSID" runat="server" Value="0" />

                                            <iframe style="display: none;" id="ifrm" runat="server"></iframe>

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

