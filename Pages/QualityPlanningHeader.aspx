<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="QualityPlanningHeader.aspx.cs" Inherits="Pages_QualityPlanningHeader" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function ShowAddPopUp() {
            $('#mpeAdd').modal({
                backdrop: 'static'
            });
            return false;
        }

        function deleteConfirm(QPHID) {
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
                __doPostBack('deletegvrow', QPHID);
            });
            return false;
        }

        function deleteConfirmQPDID(QPDID) {
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
                __doPostBack('deleteQPDID', QPDID);
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
                                    <h3 class="page-title-head d-inline-block">Quality Planning</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Master Setup</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Quality Planning Details</li>
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
                        <div class="col-sm-12">
                            <div class="col-sm-4 text-right p-t-5">
                                <label class="form-label">
                                    Select Process</label>
                            </div>
                            <div class="col-sm-6 text-left">
                                <asp:RadioButtonList ID="rblProcess" runat="server" CssClass="radio radio-success" RepeatDirection="Horizontal"
                                    AutoPostBack="true" OnSelectedIndexChanged="rblProcess_OnSelectIndexChanged" RepeatLayout="Flow"
                                    TabIndex="5">
                                    <asp:ListItem Selected="True" Value="PA">Part</asp:ListItem>
                                    <asp:ListItem Value="AWFA">Assemply Welding  And Final Assemply</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                            <div class="col-sm-2">
                            </div>
                        </div>
                        <div id="divAdd" runat="server">
                            <div class="col-sm-12 text-center">
                                <div class="col-sm-4">
                                    <label class="form-label">
                                        Part Name
                                    </label>
                                </div>
                                <div class="col-sm-4">
                                    <asp:DropDownList ID="ddlMaterialName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMaterialName_SelectIndexChanged"
                                        CssClass="form-control" Width="70%" ToolTip="Select Material Name" TabIndex="1">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-4">
                                </div>
                            </div>
                        </div>
                        <div id="divAddNew" runat="server">
                            <div class="col-sm-12 text-center p-t-20">
                                <asp:LinkButton ID="btnAddNew" runat="server" Text="Add New" OnClick="btnAddNew_Click"
                                    CssClass="btn btn-success add-emp"></asp:LinkButton>
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Sequence</label>
                                    </div>
                                    <div class="col-sm-8 text-left">
                                        <asp:TextBox ID="txtSequence" runat="server" Width="70%" CssClass="form-control mandatoryfield"
                                            ToolTip="Enter Sequence" placeholder="Enter Sequence">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-20">
                                    <asp:LinkButton ID="btnSave" runat="server" Text="Save" CssClass="btn btn-cons btn-save AlignTop"
                                        OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divInput');" OnClick="btnSave_Click"></asp:LinkButton>
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
                                                    <asp:Label ID="lbltitle" runat="server" Text=""></asp:Label>
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvQualityPlanningHeader" runat="server" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                HeaderStyle-HorizontalAlign="Center" OnRowCommand="gvQualityPlanningHeader_RowCommand"
                                                DataKeyNames="QPHID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sequence" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSequence" runat="server" Text='<%# Eval("Sequence")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Add" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnAdd" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="Add">
                                                           <img src="../Assets/images/add1.png" alt="" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="EditQuality">
                                                           <img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDelete" runat="server" OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("QPHID")) %>'>
                                                            <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnQPHID" runat="server" Value="0" />
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
    <div class="modal" id="mpeAdd" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="upView" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">
                                <asp:Label ID="lblProcessName" runat="server"></asp:Label></h4>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div class="inner-container">
                                <div id="divQPDetails" runat="server">
                                    <div class="col-sm-12">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label mandatorylbl">
                                                Stage</label>
                                        </div>
                                        <div class="col-sm-8 text-left">
                                            <asp:TextBox ID="txtStage" runat="server" Width="70%" CssClass="form-control mandatoryfield"
                                                placeholder="Enter Stage">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label mandatorylbl">
                                                Type Check</label>
                                        </div>
                                        <div class="col-sm-8 text-left">
                                            <asp:TextBox ID="txtTypeCheck" runat="server" Width="70%" CssClass="form-control mandatoryfield"
                                                placeholder="Enter Type Check">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10 text-center">
                                        <asp:LinkButton Text="Save" CssClass="btn btn-cons btn-save  AlignTop" ID="btnSaveQPDetails"
                                            runat="server" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divQPDetails');"
                                            OnClick="btnSave_Click" />
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <asp:GridView ID="gvQPDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                        EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                        HeaderStyle-HorizontalAlign="Center" OnRowCommand="gvQPDetails_RowCommand" DataKeyNames="QPDID">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Stage" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStage" runat="server" Text='<%# Eval("Stage")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type Of Check" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTypeOfCheck" runat="server" Text='<%# Eval("TypeOfCheck")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--     <asp:TemplateField HeaderText="Edit" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            CommandName="EditQuality">
                                                           <img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            <asp:TemplateField HeaderText="Delete" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnDelete" runat="server" OnClientClick='<%# string.Format("return deleteConfirmQPDID({0});",Eval("QPDID")) %>'>
                                                            <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="modal-footer">
                            </div>
                        </div>
                        <asp:HiddenField ID="hdnQPDID" runat="server" Value="0" />
                        <div class="modal-footer">
                            <div class="col-sm-12 p-t-10">
                                <div class="col-sm-10">
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
