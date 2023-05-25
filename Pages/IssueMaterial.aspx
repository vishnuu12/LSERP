<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="IssueMaterial.aspx.cs" Inherits="Pages_IssueMaterial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function UpdateConfirm(JCMRNID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, No Further Edit Once Updated",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Update it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('UpdateStock', JCMRNID);
            });
            return false;
        }

        function ShowAddPopUp() {
            $('#mpeIssueMaterial').show();
            return false;
        }

        function CloseIssueMaterialPopUP() {
            $('#mpeIssueMaterial').hide();
            return false;
        }

        function ValidateIssuedQty() {
            var SIQty = $(event.target).val();
            var RequiredQty = $('#ContentPlaceHolder1_hdnRequiredQuantity').val();

            if (parseFloat(SIQty) < parseFloat(RequiredQty)) {
                ErrorMessage('Error', 'Issued Qty Does not Less  Than Required Qty');
                $('#ContentPlaceHolder1_txtIssuedQty').val('');
                $('#ContentPlaceHolder1_txtReturnableDate').removeClass("mandatoryfield");
                $('.textboxmandatory').remove();
                return false;
            }
            else if (parseFloat(SIQty) > parseFloat(RequiredQty)) {
                $('#ContentPlaceHolder1_divMaterialReturnableDate').css("display", "block");
                $('#ContentPlaceHolder1_txtReturnableDate').addClass("mandatoryfield");
                $('#ContentPlaceHolder1_txtReturnableDate').before('<span class="textboxmandatory" style="color: red;">*</span>');
            }
            else if (parseFloat(SIQty) == parseFloat(RequiredQty)) {

            }
        }

        function ValidateIssuedLength() {
            var SILength = $(event.target).val();
            var RequiredLength = $('#ContentPlaceHolder1_hdnRequiredLength').val();

            if (parseFloat(SILength) < parseFloat(RequiredLength)) {
                ErrorMessage('Error', 'Issued Length Does not Less Than Required Length');
                $('#ContentPlaceHolder1_txtIsuedLength').val('');
                return false;
            }
        }

        function ValidateIssuedWidth() {
            var SIWidth = $(event.target).val();
            var RequiredWidth = $('#ContentPlaceHolder1_hdnRequiredWidth').val();

            if (parseFloat(SIWidth) < parseFloat(RequiredWidth)) {
                ErrorMessage('Error', 'Issued Width Does not Less Than Required Width');
                $('#ContentPlaceHolder1_txtIssuedWidth').val('');
                return false;
            }
        }

        function checkAllItems(ele) {
            debugger;
            if ($(ele).is(":checked")) {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', true);
            }
            else {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', false);
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
                                    <h3 class="page-title-head d-inline-block">Issue Material</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Stores</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Issue Material</li>
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
                    <asp:PostBackTrigger ControlID="gvIssuedMaterial" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" runat="server">
                        </div>
                        <div id="divAddNew" runat="server">
                            <div class="ip-div text-center p-t-10">
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvIssuedMaterial" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                OnRowCommand="gvIssuedMaterial_OnRowCommand" OnRowDataBound="gvIssuedMaterial_OnRowDataBound"
                                                DataKeyNames="JCHID,ReturnedLayout,CutLayout,MRNLocationName,JCMRNID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-Width="15%">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkall" runat="server" onclick="return checkAllItems(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkQC" runat="server" AutoPostBack="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Job Order ID" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJobOrderId" runat="server" Text='<%# Eval("JobCardID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="RFP No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRFPNo" runat="server" Text='<%# Eval("RFPNO")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MRN Number" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMRNNumber" runat="server" Text='<%# Eval("MRNNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Issued Weight" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIssuedWeight" runat="server" Text='<%# Eval("ISSUEDWeight")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Issued Length" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLength" runat="server" Text='<%# Eval("LEngth")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Issued Width" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblWidth" runat="server" Text='<%# Eval("Width")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rtn Length" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="RtnLength" runat="server" Text='<%# Eval("RtnLength")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rtn Width" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="RtnWidth" runat="server" Text='<%# Eval("RtnWidth")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Grade Name" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGradeName" runat="server" Text='<%# Eval("GradeName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Category Name" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("CategoryName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Material Thickness" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMaterialThickness" runat="server" Text='<%# Eval("Thickness")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="User Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Issue Date" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIssueDate" runat="server" Text='<%# Eval("ISSUEDDATE")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cut Layout" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnCutLayout" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="CutLayout">
                                                            <img src="../Assets/images/view.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Returned Layout" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnReturnedLayout" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="ReturnedLayout">
                                                            <img src="../Assets/images/view.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--    <asp:TemplateField HeaderText="Update" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                              <asp:LinkButton ID="btnUpdate" runat="server" OnClientClick='<%# string.Format("return UpdateConfirm({0});",Eval("JCMRNID")) %>'>
                                                            <img src="../Assets/images/icon_update.png" width="40px" alt=""/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnJCHID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnRequiredQuantity" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnRequiredLength" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnRequiredWidth" runat="server" Value="0" />

                                            <iframe id="ifrm" runat="server" style="display: none;"></iframe>

                                        </div>

                                        <div class="col-sm-12 p-t-10 text-center">
                                            <asp:LinkButton ID="btnIssueUpdate" runat="server" Text="Update"
                                                OnClick="btnIssueUpdate_Click" CssClass="btn btn-cons btn-save AlignTop"></asp:LinkButton>
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
    <div class="modal" id="mpeIssueMaterial" style="overflow-y: scroll;">
        <div class="modal-dialog" style="width: 50%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="upView" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnMaterialReturn" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">
                                <asp:Label ID="lblHeader" runat="server"></asp:Label></h4>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" onclick="CloseIssueMaterialPopUP();"
                                aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="docdiv" class="docdiv">
                                <div class="inner-container" style="text-transform: uppercase;">
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-4">
                                            <label>
                                                MRN Number</label>
                                            <asp:Label ID="lblMRNo" runat="server" />
                                        </div>
                                        <div class="col-sm-4">
                                            <label>
                                                Length</label>
                                            <asp:Label ID="lblRequiredLength" runat="server" />
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-4">
                                            <label>
                                                Qty</label>
                                            <asp:Label ID="lblRequiredQty" runat="server" />
                                        </div>
                                        <div class="col-sm-4">
                                            <label>
                                                Width</label>
                                            <asp:Label ID="lblRequiredWidth" runat="server" />
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10" style="padding-left: 37%;">
                                        <%--  <asp:RadioButtonList ID="rblMaterialIssueReturn" runat="server" CssClass="radio radio-success"
                                            OnSelectedIndexChanged="rblMaterialIssueReturn_SelectedIndexChanged" AutoPostBack="true"
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Value="I" Selected="True">Material Issue</asp:ListItem>
                                            <asp:ListItem Value="R">Material Return</asp:ListItem>
                                        </asp:RadioButtonList>--%>
                                        <asp:Label ID="lblMaterialIssueReturn" Style="color: brown; font-size: large;" runat="server"></asp:Label>
                                    </div>
                                    <div id="divMaterialIssue" runat="server" visible="true">
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <label class="mandatorylbl">
                                                    Issued Qty</label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtIssuedQty" runat="server" Width="70%" CssClass="form-control mandatoryfield"
                                                    onblur="ValidateIssuedQty(this);" ToolTip="Enter Po Ref No" placeholder="Enter Issued Qty">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <label class="mandatorylbl">
                                                    Issued Length</label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtIsuedLength" runat="server" Width="70%" CssClass="form-control mandatoryfield"
                                                    onblur="ValidateIssuedLength(this);" ToolTip="Enter Po Ref No" placeholder="Enter Issued Length">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <label class="mandatorylbl">
                                                    Issued Width</label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtIssuedWidth" runat="server" Width="70%" CssClass="form-control mandatoryfield"
                                                    onblur="ValidateIssuedWidth(this);" ToolTip="Enter Po Ref No" placeholder="Enter Issued Width">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10" id="divMaterialReturnableDate" runat="server" style="display: none;">
                                            <div class="col-sm-4">
                                                <label>
                                                    Material Returnable Date</label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtReturnableDate" runat="server" Width="70%" CssClass="form-control datepicker"
                                                    ToolTip="Enter Po Ref No" placeholder="Enter Returnable Date">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 text-center p-t-10">
                                            <asp:Button ID="btnMaterialIssue" Text="Save Material Issue" CssClass="btn btn-cons btn-success"
                                                OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divMaterialIssue');"
                                                OnClick="btnMaterialIssue_Click" runat="server" />
                                        </div>
                                    </div>
                                    <div id="divMaterialReturn" runat="server" visible="false">
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <label>
                                                    Issued Qty</label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:Label ID="lblIssuedQty" runat="server" />
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <label>
                                                    Issued Length</label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:Label ID="lblIssuedLength" runat="server" />
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <label>
                                                    Issued Width</label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:Label ID="lblIssuedWidth" runat="server" />
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <label class="mandatorylbl">
                                                    Returnable Qty</label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtReturnableQty" runat="server" Width="70%" CssClass="form-control mandatoryfield"
                                                    ToolTip="Enter Po Ref No" placeholder="Enter Returnable Qty">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <label class="mandatorylbl">
                                                    Returnable Length</label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtReturnableLength" runat="server" Width="70%" CssClass="form-control mandatoryfield"
                                                    ToolTip="Enter Po Ref No" placeholder="Enter Returnable Length">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <label class="mandatorylbl">
                                                    Returnable Width</label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtReturnableWidth" runat="server" Width="70%" CssClass="form-control mandatoryfield"
                                                    ToolTip="Enter Po Ref No" placeholder="Enter Returnable Width">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <label class="mandatorylbl">
                                                    Balance MRN Layout Attach</label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:FileUpload ID="fAttachement" runat="server" CssClass="form-control mandatoryfield Attachement"
                                                    onchange="DocValidation(this);" ClientIDMode="Static" Width="95%"></asp:FileUpload>
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 text-center p-t-10">
                                            <asp:Button ID="btnMaterialReturn" Text="PDF Inter" CssClass="btn btn-cons btn-success"
                                                OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divMaterialReturn');"
                                                OnClick="btnMaterialReturn_Click" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
