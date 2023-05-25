<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="MaterialPlanning.aspx.cs" Inherits="Pages_MaterialPlanning" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function AwtValuevalidate(ele) {
            if (parseFloat(document.getElementById('<%=lblAWTValue.ClientID%>').innerText) < parseFloat($(ele).val())) {
                InfoMessage("Actual Weight/QTY shouldn't greater than Total BOM Actual Weight/QTY");
                $(ele).val('');
            }
        }


        function ShowMPDetailsDataTable() {
            $('#ContentPlaceHolder1_gvMaterialPlanningDetails').DataTable({
                "paging": true,
                "retrieve": true,
                "pageLength": 50,
                //  responsive: true,
                // columnDefs: [
                //{ responsivePriority: 1, targets: 0 },
                //{ responsivePriority: 10001, targets: 4 },
                //{ responsivePriority: 10002, targets: 5 },
                //{ responsivePriority: 2, targets: -1 },
                //{ responsivePriority: 3, targets: -2 },
                //  ]
                //'lengthMenu': [[10, 25, 50, -1], [10, 25, 50, 'All']],
            });
        }

        function ShowAddPopUp() {
            $('#mpeAdd').modal({
                backdrop: 'static'
            });
            $('#mpePartDetails').hide();
            ShowMPDetailsDataTable();
            return false;
        }

        function CloseAddPopUp() {
            $('#mpeAdd').modal('hide');
            $('#mpePartDetails').show();
            ShowMPDetailsDataTable();
        }

        function Blockedweightvalidate(ele) {
            debugger;
            if ((parseFloat($('#ContentPlaceHolder1_txtBlockedWeight').val()) + parseFloat($('#ContentPlaceHolder1_hdnblockedqty').val())) > parseFloat($('#ContentPlaceHolder1_hdnrequiredqty').val())) {
                InfoMessage("Blocking weight shouldn't greater than Planned/Actual qty weight " + $('#ContentPlaceHolder1_hdnrequiredqty').val());
                $(ele).val('');
                return false;
            }
        }

        function ShowCertificates() {
            $('#mpCertificates').modal({
                backdrop: 'static'
            });
            return false;
        }

        function ShowViewPopUp() {
            $('#mpeViewdocs').modal({
                backdrop: 'static'
            });
            return false;
        }

        function ShowPartPopUp() {
            $('#mpePartDetails').modal({
                backdrop: 'static'
            });
            ShowMPDetailsDataTable();
            return false;
        }

        function ClosePartItemPopUP() {
            $('#mpePartDetails').show();
            $('#mpeAddPartItem').modal('hide');

            ShowMPDetailsDataTable();

            return false;
        }

        function hidePartDetailpopup() {
            $('#mpePartDetails').hide();
            $('div').removeClass('modal-backdrop');
            return false;
        }

        function ShowAddPartItemPopUP() {
            $('#mpeAddPartItem').modal({
                backdrop: 'static'
            });
            $('#mpePartDetails').hide();
            return false;
        }

        function UpdateRFPStatus() {
            swal({
                title: "No Further Edit Once Shared.",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Save it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('UpdateRFPStatus', null);
            });
            return false;
        }

        function deleteConfirm(AttachementID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Attachement will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deletegvrow', AttachementID);
            });
            return false;
        }

        function deleteConfirmMPMD(MPMD) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the MRN Details will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deletegvrowMPMD', MPMD);
            });
            return false;
        }

        function joborderweight(ele) {
            if ($(ele).find('[type="radio"]:checked').val() == 'jobOrder') {
                $('#ContentPlaceHolder1_divjoborderweight').show();
                $(this).find('[type="text"]').addClass("mandatoryfield");
            }
            else if ($(ele).find('[type="radio"]:checked').val() == 'BoughtOutItems') {
                $('#ContentPlaceHolder1_divjoborderweight').show();
                $(this).find('[type="text"]').addClass("mandatoryfield");
            }
            else {
                $('#ContentPlaceHolder1_divjoborderweight').hide();
                $(this).find('[type="text"]').removeClass("mandatoryfield");
            }
            //            if ($(ele).prop("checked") == true) {
            //                $('#ContentPlaceHolder1_divjoborderweight').show();
            //                $(this).find('[type="text"]').addClass("mandatoryfield");
            //            }
            //            else {
            //                $('#ContentPlaceHolder1_divjoborderweight').hide();
            //                $(this).find('[type="text"]').removeClass("mandatoryfield");
            //            }
        }

        //        function closeMRNBlockingPopUP() {
        //            $('#mpeAddMRN').hide();
        //            $('#mpeAddPartItem').show();
        //            return false;
        //        }


        function ShowCertificatesandBomFilePopUp() {
            $('#mpeBomFilesViewCertificates').modal('show');
        }

        function ViewDrawings(index) {
            __doPostBack("ViewDrawings", index);
            return false;
        }


        function ReviewMP(index) {
            swal({
                title: "Are you sure?",
                text: "If Yes, Material Planning Details Review The Process",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Review it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('ReviewMP', index);
            });
        }

        function ShowProcessPlanningPopup() {
            $('#mpeProcessPlanningDetails').modal({
                backdrop: 'static'
            });
            return false;
        }

        function checkAllItems(ele) {
            debugger;
            if ($(ele).is(":checked")) {
                $(ele).closest('table').find('.chkpartname').find('[type="checkbox"]').prop('checked', true);
            }
            else {
                $(ele).closest('table').find('.chkpartname').find('[type="checkbox"]').prop('checked', false);
            }
        }

        function SaveprocessPlanning() {
            swal({
                title: "Are you sure?",
                text: "If Yes, No Further Edit Once Shared Process Planning Share Permanaently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Share it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('SharePP', '');
            });
            return false;
        }

        function deleteProcessPlanningName(PPDID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Process name will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Process name it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deleteProcessPlanning', PPDID);
            });
            return false;
        }

        function ViewCFDocs(FilePath) {          
                __doPostBack('ViewCFDocs', FilePath);            
            return false;
        }

        function opennewtab(Filepath) {
            var FileName = Filepath.split('/');
            for (var i = 0; i < FileName.length; i++)
                window.open('http://103.31.214.61/LSERPDocs/StoresDocs/MaterialInwardQCCertificates/' + FileName[i], '_blank');
            return false;
        }

    </script>

    <style type="text/css">
        #ContentPlaceHolder1_ddlRFPNo_chosen {
            width: 108% !important;
        }

        #ContentPlaceHolder1_divsave .chosen-container {
            width: 88% !important;
        }

        #ContentPlaceHolder1_gvProcessPlanningDetails td table td {
            border: none;
        }

        #ContentPlaceHolder1_gvProcessPlanningDetails td table {
            width: 100%;
        }

            #ContentPlaceHolder1_gvProcessPlanningDetails td table td label {
                padding-left: 0%;
            }

        .radio-success label:nth-child(2) {
            color: red !important;
        }

        .radio-success label:nth-child(4) {
            color: #0acc0a !important;
        }
    </style>

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
                                    <h3 class="page-title-head d-inline-block">Material Planning Details</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Production</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Material Planning</li>
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
                    <asp:PostBackTrigger ControlID="btnRFPQC" />
                    <asp:PostBackTrigger ControlID="btnViewMP" />

                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divradio" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label" style="padding-top: 7px;">
                                            Select Type
                                        </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:RadioButtonList ID="rblRFPChange" runat="server" CssClass="radio radio-success" RepeatDirection="Horizontal"
                                            AutoPostBack="true" OnSelectedIndexChanged="rblRFPChange_OnSelectedChanged" RepeatLayout="Flow">
                                            <asp:ListItem Selected="True" Value="0">PENDING</asp:ListItem>
                                            <asp:ListItem Value="1">COMPLETED</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divAdd" class="btnAddNew" runat="server">
                            <div class="text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Customer Name</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="true" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlCustomerName_OnSelectIndexChanged" Width="70%" ToolTip="Select Customer Number">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
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
                                    <div class="col-sm-4 text-left">
                                        <asp:DropDownList ID="ddlRFPNo" runat="server" AutoPostBack="true" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlRFPNo_SelectIndexChanged" Width="70%" ToolTip="Select RFP No">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:LinkButton Text="RFP QC" CssClass="btn btn-cons btn-success"
                                            ID="btnRFPQC" OnClick="btnRFPQC_Click" runat="server" />
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:LinkButton Text="View MP" CssClass="btn btn-cons btn-success"
                                            ID="btnViewMP" OnClick="btnViewMP_Click" runat="server" />
                                    </div>
                                </div>
                                <%--  <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Item Name</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlItemName" runat="server" ToolTip="Select Item Name" Width="70%"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlItemName_OnSelectIndexChanged"
                                            CssClass="form-control mandatoryfield">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="lblitemqty" CssClass="lablqty" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>--%>
                            </div>
                        </div>
                        <div class="col-sm-12 text-center p-t-10" id="divAddNew" runat="server">
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
                                                    <asp:Label ID="lbltitle" runat="server" Text="Material Planning Details"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvMPItemDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                OnRowDataBound="gvMPItemDetails_OnRowDataBound" OnRowCommand="gvMPItemDetails_OnRowCommand"
                                                DataKeyNames="RFPDID,Filename,EnquiryID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="QP Status" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQPStatus" Width="50px" runat="server" Text='<%# Eval("QPStatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Material Planned" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMaterialPlanned" Width="50px" runat="server" Text='<%# Eval("MPStatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Add Process Planning" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnAddPP" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="AddProcessPlanning"><img src="../Assets/images/add1.png"/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Add Material Planning" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnAdd" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="ADD"><img src="../Assets/images/add1.png"/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Quantity" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemQuantity" Width="50px" runat="server" Text='<%# Eval("QTY")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Drawing Name" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDrawingName" runat="server" Text='<%# Eval("Drawingname")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View Drawings" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <%--  <asp:LinkButton ID="btnView" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="ViewDrawings"><img src="../Assets/images/view.png" /></asp:LinkButton>--%>
                                                            <asp:LinkButton ID="btnView" runat="server"
                                                                OnClientClick='<%# string.Format("return ViewDrawings({0});",((GridViewRow) Container).RowIndex) %>'>
                                                            <img src="../Assets/images/view.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Review MP" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <%--   <asp:LinkButton ID="btnReviewMP" runat="server" Text="Review MP" 
                                                                CssClass="btn btn-cons btn-success" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="ReviewMP"></asp:LinkButton>--%>
                                                            <asp:LinkButton ID="btnReviewMP" runat="server" Text="Reverse" CssClass="btn btn-cons btn-success"
                                                                OnClientClick='<%# string.Format("return ReviewMP({0});",((GridViewRow) Container).RowIndex) %>'>
                                                            </asp:LinkButton>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnMPID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnblockweight" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnRFPDID" Value="0" runat="server" />
                                            <asp:HiddenField ID="hdnEDID" Value="0" runat="server" />
                                            <asp:HiddenField ID="hdnblockedqty" Value="0" runat="server" />
                                            <asp:HiddenField ID="hdnrequiredqty" Value="0" runat="server" />
                                        </div>
                                        <div class="col-sm-12 text-center">
                                            <asp:LinkButton Text="Share Material Planning" CssClass="btn btn-cons btn-success"
                                                ID="btnShareMaterialPlanning" OnClick="btnShareMaterialPlanning_Click" runat="server" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="modal" id="mpeAdd" style="overflow-y: scroll;">
            <div style="max-width: 81%; padding-left: 10%; padding-top: 5%;">
                <div class="modal-content">
                    <asp:UpdatePanel ID="upView" runat="server">
                        <Triggers>
                            <%--    <asp:PostBackTrigger ControlID="btnSaveAttachements" />--%>
                            <%-- <asp:PostBackTrigger ControlID="btnBlockingMRN" />--%>
                            <asp:PostBackTrigger ControlID="gvStockMonitorDetails" />

                        </Triggers>
                        <ContentTemplate>
                            <div class="modal-header">
                                <h4 class="modal-title ADD">Material Planning MRN Details -</h4>
                                <asp:Label ID="lblPartGradeThkIN_A" runat="server" Style="font-weight: bold; padding-left: 30px;"></asp:Label>
                                <button type="button" class="close btn-primary-purple" onclick="ClosePartItemPopUP();"
                                    data-dismiss="modal" aria-hidden="true">
                                    ×</button>
                            </div>
                            <div class="modal-body" style="padding: 0px;">
                                <div id="docdiv" class="docdiv">
                                    <div class="inner-container">
                                        <%-- <ul class="nav nav-tabs lserptabs" style="display: inline-block; width: 100%; background-color: cadetblue;
                                            text-align: right; font-size: x-large; font-weight: bold; color: whitesmoke;">
                                            <li id="liMRNBlocking" class="active"><a href="#Item" class="tab-content active"
                                                data-toggle="tab" onclick="OpenTab('MRNBlocking');">
                                                <p style="margin-left: 10px; text-align: center; color: black;">
                                                    MRN Blocking
                                                </p>
                                            </a></li>
                                            <li id="liDocuments"><a href="#Documents" class="tab-content" data-toggle="tab" onclick="OpenTab('Documents');">
                                                <p style="margin-left: 10px; text-align: center; color: black;">
                                                    Documents</p>
                                            </a></li>
                                        </ul>--%>
                                        <%--     <div id="Documents" runat="server" style="display: none;">
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-3">
                                                    <label class="form-label mandatorylbl">
                                                        Type Name
                                                    </label>
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:DropDownList ID="ddlTypeName" runat="server" TabIndex="1" ToolTip="Select Attachement type"
                                                        CssClass="form-control mandatoryfield">
                                                        <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="Enquiry Related"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Drawing"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="Financial Documents"></asp:ListItem>
                                                        <asp:ListItem Value="4" Text="Others"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-3">
                                                    <label class="form-label mandatorylbl">
                                                        Attachment Description
                                                    </label>
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:TextBox ID="txtDescription" runat="server" TabIndex="6" placeholder="Enter Attachement Description"
                                                        ToolTip="Enter Attachment Description" TextMode="MultiLine" CssClass="form-control mandatoryfield"
                                                        MaxLength="300" autocomplete="nope"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-3">
                                                    <label class="form-label mandatorylbl">
                                                        Attachement
                                                    </label>
                                                </div>
                                                <div class="col-sm-9">
                                                    <asp:FileUpload ID="fAttachment" runat="server" TabIndex="12" CssClass="form-control mandatoryfield"
                                                        ClientIDMode="Static" Width="95%"></asp:FileUpload>
                                                    <asp:Label ID="lblAttachementFileName" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <asp:GridView ID="gvAttachments" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    OnRowCommand="gvAttachments_OnRowCommand" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                    DataKeyNames="AttachementID">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Attachement Type Name" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAttachementTypeName_V" runat="server" Text='<%# Eval("AttachementTypeName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDescription_V" runat="server" Text='<%# Eval("Description")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="View" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblFileName_V" runat="server" Visible="false" Text='<%# Eval("FileName")%>'></asp:Label>
                                                                <asp:LinkButton ID="lbtnView" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                    CommandName="ViewDocs"><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnDelete" runat="server" OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("AttachementID")) %>'
                                                                    CommandName="DeleteAttachement">
                                                           <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-3">
                                                </div>
                                                <div class="col-sm-3">
                                                    <div class="form-group">
                                                        <asp:LinkButton Text="Submit" CssClass="btn btn-cons btn-save  AlignTop" ID="btnSaveAttachements"
                                                            runat="server" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_docdiv');"
                                                            OnClick="btnSaveAttchement_Click" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-3">
                                                </div>
                                                <div class="col-sm-3">
                                                </div>
                                            </div>
                                        </div>--%>
                                        <div id="MRNBlocking" runat="server">
                                            <div id="divAdd_A" class="divInput" runat="server">
                                                <div class="ip-div text-center">
                                                    <div class="col-sm-12 p-t-10">
                                                        <div class="col-sm-3">
                                                            <label class="form-label mandatorylbl">
                                                                MRN Number
                                                            </label>
                                                        </div>
                                                        <div class="col-sm-9">
                                                            <asp:DropDownList ID="ddlMRNNumber_A" runat="server" TabIndex="1" ToolTip="Select MRN Number"
                                                                AutoPostBack="true" OnSelectedIndexChanged="ddlMRNNumber_A_OnSelectIndexchanged"
                                                                CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="divInput_A" runat="server">
                                                <div class="col-sm-12 p-t-10" id="divstockmonitor" runat="server" style="overflow-x: scroll;">
                                                    <asp:GridView ID="gvStockMonitorDetails" runat="server" AutoGenerateColumns="False"
                                                        OnRowCommand="gvStockMonitorDetails_OnRowCommand" ShowHeaderWhenEmpty="True" OnRowDataBound="gvStockMonitorDetails_OnRowDataBound"
                                                        EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                        DataKeyNames="SIID,BalanceLayout,LocationName">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                                HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MRN Number" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                                HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="MRNNumber" runat="server" Text='<%# Eval("MRNNumber")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View Certificates" ItemStyle-HorizontalAlign="Center"
                                                                HeaderStyle-Width="54px" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnView" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                        CommandName="ViewCertificates"><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Material Name" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                                HeaderStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGradeName" runat="server" Text='<%# Eval("GradeName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Material Category" ItemStyle-HorizontalAlign="left"
                                                                HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("CategoryName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Material Type Name" ItemStyle-HorizontalAlign="left"
                                                                HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTypeName" runat="server" Text='<%# Eval("MaterialTypeName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Thickness" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblThickness" runat="server" Text='<%# Eval("THKValue")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Measurement" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMeasurement" runat="server" Text='<%# Eval("Measurment").ToString().Replace(",", "<br/>")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="RFP No" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                                HeaderStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRFPNo" runat="server" Text='<%# Eval("RFPNo")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Old MRN Number" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                                HeaderStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblOldMRNNumber" runat="server" Text='<%# Eval("OldMRNNumber")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Location" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                                HeaderStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("LocationName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Inward Quantity" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblInwardQuantity" runat="server" Text='<%# Eval("InwardedQty")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Blocked Quantity" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBlockedQty" runat="server" Text='<%# Eval("BlockedQty")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Consumed Quantity" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblConsumedQty" runat="server" Text='<%# Eval("ConsumedQty")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Actual Stock Qty" ItemStyle-HorizontalAlign="Left"
                                                                HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblActualStockQty" runat="server" Text='<%# Eval("ActualStock")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="In Stock Qty" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblInStockQty" runat="server" Text='<%# Eval("InStockQty")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Balance Layout" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnLayout" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                        CommandName="ViewCuttingLayout"><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:HiddenField ID="hdnstockqty" runat="server" Value="0" />
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-3">
                                                        <label class="form-label mandatorylbl">
                                                            Blocking Weight
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox ID="txtBlockedWeight" runat="server" TabIndex="6" placeholder="Enter Blocking Weight"
                                                            CssClass="form-control mandatoryfield" onblur="Blockedweightvalidate(this);"></asp:TextBox>
                                                        <%----%>
                                                    </div>
                                                </div>
                                                <%--  <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-3">
                                                        <label class="form-label mandatorylbl">
                                                            Layout Attachement
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-9">
                                                        <asp:FileUpload ID="fRequiredShape" runat="server" TabIndex="12" CssClass="form-control mandatoryfield"
                                                            Width="95%"></asp:FileUpload>
                                                        <asp:Label ID="Label2" runat="server"></asp:Label>
                                                    </div>
                                                </div>--%>
                                                <div class="col-sm-12 p-t-20 text-center">
                                                    <asp:LinkButton ID="btnBlockingMRN" runat="server" Text="Save" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_MRNBlocking');"
                                                        OnClick="btnBlockingMRN_Click" CssClass="btn btn-cons btn-save AlignTop"></asp:LinkButton>
                                                </div>
                                            </div>
                                            <div id="divOutput_A" runat="server">
                                                <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                    <asp:GridView ID="gvBlockingMRN" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                        EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                        OnRowCommand="gvBlockingMRN_OnRowCommand" OnRowDataBound="gvBlockingMRN_OnRowDataBound"
                                                        DataKeyNames="FileName,MPMD,MPID">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MPID" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMPID" runat="server" Text='<%# Eval("MPID")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Approval Status" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblApprovalStatus" runat="server" Text='<%# Eval("ApprovalStatus")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Approved By" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAprovedBy" runat="server" Text='<%# Eval("ApprovedBy")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Approval Date" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblApprovalDate" runat="server" Text='<%# Eval("ApprovedDate")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Approval Remarks" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblApprovalRemarks" runat="server" Text='<%# Eval("MRNQCRemarks")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MRN Number" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMRNNumber" runat="server" Text='<%# Eval("MRNNumber")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Actual Qty" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblActualQty" Width="50px" runat="server" Text='<%# Eval("ActualQty")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Blocked Qty" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBlockedQty" Width="50px" runat="server" Text='<%# Eval("BlockedWeight")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--   <asp:TemplateField HeaderText="View Required Shape" ItemStyle-CssClass="text-center"
                                                                HeaderStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnView" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                        CommandName="ViewDocs"><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                            <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                                HeaderText="Delete">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                        OnClientClick='<%# string.Format("return deleteConfirmMPMD({0});",Eval("MPMD")) %>'>   <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
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
                            <div class="modal-footer">
                            </div>
                            </div> </div>
                            <asp:HiddenField ID="hdnAttachementID" runat="server" Value="0" />
                            <asp:HiddenField ID="hdnAttachementFlag" runat="server" />

                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <div class="modal" id="mpCertificates" style="overflow-y: scroll;">
        <div style="max-width: 81%; padding-left: 10%; padding-top: 5%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">Certficate Details</h4>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="Div1" class="docdiv">
                                <div class="inner-container">
                                    <div id="Certificates" runat="server">
                                        <div id="divAddItems" class="divInput" runat="server">
                                            <div class="ip-div text-center">
                                            </div>
                                        </div>
                                        <div id="divOutputsItems" runat="server">
                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvCertificates" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Certificate Name" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCertificatename" runat="server" Text='<%# Eval("CertificateName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CertficateNo" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCertficateNo" Width="50px" runat="server" Text='<%# Eval("CertficateNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hdnSPODID" runat="server" Value="0" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal" id="mpeViewdocs" style="height: 125%; top: -10%;">
        <div class="modal-dialog" style="height: 100%;">
            <div class="modal-content" style="height: 100%;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" Style="height: 100%;">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btndownloaddocs" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H2">
                                <div class="text-center">
                                    Documents
                                    <asp:Label ID="Label1" Text="" runat="server"></asp:Label>
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal">
                                x</button>
                        </div>
                        <div class="modal-body" style="height: 100%;">
                            <div class="inner-container" style="height: 100%;">
                                <div class="ip-div text-center" style="margin-bottom: 10px;">
                                    <%-- OnClick="btndownloaddocs_Click" --%>
                                    <asp:LinkButton Text="Download" CssClass="btn btn-save btn-cons AlignTop" ID="btndownloaddocs"
                                        runat="server" />
                                </div>
                                <div class="col-sm-12" style="height: 100%;">
                                    <iframe runat="server" id="ifrm" src="" style="width: 100%; height: 80%;" frameborder="0"></iframe>
                                </div>
                            </div>
                        </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div class="modal" id="mpePartDetails" style="overflow-y: scroll;">
        <div style="max-width: 100%; padding-left: 0%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">

                            <asp:Label ID="lblItemName_P" runat="server" Style="padding-left: 19px; padding-top: 10px; color: #c12424;"></asp:Label>
                            <label style="color: brown; font-size: 21px;">Part Type  ( M -  Main Part , C - Sub Part ) </label>
                            <label style="color: #000; font-size: 21px;">Material Planning  Only Doing Main Part Else Dont. </label>
                            <button type="button" class="close btn-primary-purple" onclick="hidePartDetailpopup();"
                                data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="Div3" class="docdiv">
                                <div class="inner-container">
                                    <div id="Div4" runat="server">
                                        <div id="div5" class="divInput" runat="server">
                                            <div class="ip-div text-center">
                                            </div>
                                        </div>
                                        <div id="div6" runat="server">
                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvMaterialPlanningDetails" runat="server" AutoGenerateColumns="False"
                                                    ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium orderingfalse uniquedatatable"
                                                    OnRowDataBound="gvMaterialPlanningDetails_RowDataBound" OnRowCommand="gvMaterialPlanningDetails_OnRowCommand"
                                                    DataKeyNames="MPID,BOMID">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Part Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPartName" runat="server" Text='<%# Eval("PartName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Part Quantity" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPartQuantity" runat="server" Text='<%# Eval("PartQtyDesign")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bom Part Remarks" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblbompartremarks" runat="server" Text='<%# Eval("BomPartRemarks")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Bom Part Drawing" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnviewbomdrawing" runat="server"
                                                                    CommandArgument='<%#((GridViewRow) Container).RowIndex %>'><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Material Planned" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMaterialPlanned" runat="server" Text='<%# Eval("MPStatus")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Deviation Status" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDeviationStatus" runat="server" Text='<%# Eval("DeviationStatus")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Add Material Planning" HeaderStyle-CssClass="text-center"
                                                            ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnAdd" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                    CommandName="addPMP"><img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Category Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("CategoryName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--  <asp:TemplateField HeaderText="Part Qty Design" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartQtyDesign" runat="server" Text='<%# Eval("PartQtyDesign")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Grade Name Design" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGradeNameDesign" runat="server" Text='<%# Eval("GradeNameDesign")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Thickness" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblThicknessValue" runat="server" Text='<%# Eval("Thickness")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Unit Required Weight" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRequiredWeight" runat="server" Text='<%# Eval("RequiredWeight")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Required Weight" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTotalRequiredWeight" runat="server" Text='<%# Eval("TotalRequiredWeight")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Blocked Weight" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTotalBlockedWeight" runat="server" Text='<%# Eval("TotalBlockedWeight")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MRN No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMRNNo" runat="server" Text='<%# Eval("MRNNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Blocking MRN" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnAddAttach" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                    CommandName="AddAttachements"><img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Grade Name Production" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGradeName" runat="server" Text='<%# Eval("GradeNameProduction")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Job Type" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblJobType" runat="server" Text='<%# Eval("JobType")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--  <asp:TemplateField HeaderText="Unit Job Weight" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblJobWeight" runat="server" Text='<%# Eval("JobWeight")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Job Weight" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTotalJobWeight" runat="server" Text='<%# Eval("TotalJobWeight")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>

                                                        <asp:TemplateField HeaderText="View Certificates" ItemStyle-CssClass="text-center"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnViewCertificates" runat="server"
                                                                    OnClientClick='<%# string.Format("return ViewCFDocs({0});",Eval("CFName")) %>'>
                                                                    <img src="../Assets/images/view.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--  <asp:TemplateField HeaderText="Bom Files" ItemStyle-CssClass="text-center"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnViewBomFiles" runat="server" 
                                                            CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                    CommandName="ViewBomFiles"><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Part Type" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblparttype" runat="server" Text='<%# Eval("PartType")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MPID" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMPID" runat="server" Text='<%# Eval("MPID")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <div class="col-sm-12 text-center">
                                        <asp:LinkButton Text="SaveMaterialPlanning" CssClass="btn btn-cons btn-success" ID="btnMaterialPlanningStatus"
                                            OnClick="btnMaterialPlanningStatus_Click" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>


    <div class="modal" id="mpeAddPartItem" style="overflow-y: scroll;">
        <div style="max-width: 81%; padding-left: 10%; padding-top: 5%;">
            <div class="modal-content" style="width: 100%;">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">Add
                            </h4>
                            <asp:Label ID="lblPartName_API" runat="server" Style="padding-left: 19px; padding-top: 10px; color: #c12424;"></asp:Label>
                            <button type="button" class="close btn-primary-purple" onclick="ClosePartItemPopUP();"
                                data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="Div2" class="docdiv">
                                <div class="inner-container">
                                    <div id="divsave" runat="server">
                                        <div id="div8" class="divInput" runat="server">
                                            <div class="ip-div text-center">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-2">
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="text-left">
                                                    <label class="form-label mandatorylbl">
                                                        Material Category
                                                    </label>
                                                </div>
                                                <div>
                                                    <asp:DropDownList ID="ddlMaterialCategory" runat="server" ToolTip="Select Material Category Name"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlMaterialCategory_SelectIndexChanged"
                                                        CssClass="form-control mandatoryfield">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="text-left">
                                                    <label class="form-label mandatorylbl">
                                                        Material Name</label>
                                                </div>
                                                <div>
                                                    <asp:DropDownList ID="ddlMaterialNameProduction" runat="server" ToolTip="Select Material Name"
                                                        CssClass="form-control mandatoryfield">
                                                        <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:Label ID="lblMaterialNameDesign" CssClass="lablqty"
                                                    runat="server" Style="float: left; margin-top: 38px; display: none;"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-2 text-right" style="padding-top: 39px;">
                                                <asp:Label ID="lblThkInBOM" CssClass="lablqty" Style="display: none;" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="text-left">
                                                    <label class="form-label mandatorylbl">
                                                        Thickness Value</label>
                                                </div>
                                                <div>
                                                    <asp:DropDownList ID="ddlThickness" runat="server" ToolTip="Select Thickness Value"
                                                        CssClass="form-control mandatoryfield">
                                                        <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="text-left">
                                                    <label class="form-label mandatorylbl">
                                                        Required Weight/QTY</label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtRequiredWeight" runat="server" CssClass="form-control mandatoryfield"
                                                        onkeypress="fnAllowNumeric();" autocomplete="nope" PlaceHolder="Enter Required Weight"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                                <asp:Label ID="lblRequiredWeight" CssClass="lablqty" runat="server" Style="float: left; margin-top: 38px; display: none;"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-2">
                                                    <span class="form-label " style="color:brown;font-size: 13px;font-weight: bold;white-space:nowrap;">BOM AWT Value : </span>
                                                
                                         <asp:Label ID="lblAWTValue" Style="color: brown;font-size: 12px;font-weight: bold;white-space:nowrap;" runat="server"></asp:Label>
                                                </div>
                                            <div class="col-sm-4">
                                                <div class="text-left">
                                                    <label class="form-label mandatorylbl">
                                                        Required Actual Weight/QTY</label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtRequiredActualWeight" runat="server" CssClass="form-control mandatoryfield"
                                                        onkeypress="return validationDecimal(this);" onblur="AwtValuevalidate(this);" autocomplete="nope" PlaceHolder="Enter Actual Required Weight"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                            <div class="col-sm-2">
                                            </div>
                                         </div>
                                        <div class="col-sm-12 p-t-10" id="divJobOrderWeightCheckBox"
                                            runat="server">
                                            <div class="col-sm-2">
                                            </div>
                                            <div class="col-sm-4">
                                                <%--  <asp:CheckBox ID="chckworkorder" onclick="return joborderweight(this);" AutoPostBack="false"
                                                    runat="server" /><label class="form-label" style="width: 70% !important; float: initial;
                                                        margin-left: 5px;">Job Order</label>--%>
                                                <%--    <asp:RadioButtonList ID="rblJobType" runat="server" CssClass="radio radio-success"
                                                    OnChange="return joborderweight(this);" RepeatDirection="Horizontal">
                                                    <asp:ListItem Text="Job Order" Value="jobOrder" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Work Order" Value="WorkOrder"></asp:ListItem>
                                                    <asp:ListItem Text="Bought Out" Value="BoughtOutItems"></asp:ListItem>
                                                </asp:RadioButtonList>--%>
                                            </div>
                                            <div id="divjoborderweight" runat="server" style="display: none;">
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="form-label mandatorylbl">
                                                            Job Order Weight/QTY</label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtJoborderWeight" runat="server" CssClass="form-control" autocomplete="nope"
                                                            onkeypress="fnAllowNumeric();" Text="0" PlaceHolder="Enter Joborder Weight"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-2">
                                                    <asp:Label ID="lblAWT" CssClass="lablqty" runat="server"
                                                        Style="float: left; margin-top: 38px; margin-left: -26px; display: none;"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-20 text-center">
                                            <asp:LinkButton ID="btnSave" runat="server" Text="Save" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divsave');"
                                                OnClick="btnSave_Click" CssClass="btn btn-cons btn-save AlignTop"></asp:LinkButton>
                                            <%--      <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" CausesValidation="False"
                                                OnClick="btncancel_Click" CssClass="btn btn-cons btn-danger AlignTop btnCancel"></asp:LinkButton>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                        </div>
                        <asp:HiddenField ID="HiddenField2" runat="server" Value="0" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div class="modal" id="mpeBomFilesViewCertificates" style="overflow-y: scroll;">
        <div style="max-width: 81%; padding-left: 10%; padding-top: 5%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="gvBomFiles" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">
                                <asp:Label ID="lblPartname" runat="server"></asp:Label></h4>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="djd" class="docdiv">
                                <div class="inner-container">
                                    <div id="Div7" runat="server">
                                        <div id="div9" class="divInput" runat="server">
                                            <div class="ip-div text-center">
                                            </div>
                                        </div>
                                        <div id="divMaterialInspectionCertificates" runat="server" visible="false">
                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvMaterialInspectionCertificates" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Certificate Name" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCertificatename" runat="server" Text='<%# Eval("TypeOfCheck")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Certificate Name" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCertificatename" runat="server" Text='<%# Eval("TypeOfCheck")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div id="divBomFiles" runat="server" visible="false">
                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvBomFiles" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    OnRowCommand="gvBomFiles_OnRowCommand" EmptyDataText="No Records Found"
                                                    CssClass="table table-hover table-bordered medium" DataKeyNames="LayoutName,EnquiryID">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DownLoad" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDownLoad" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                    CommandName="DownLoadLayoutAttachement">
                                                           <img src="../Assets/images/pdf.png" alt=""/></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="HiddenField3" runat="server" Value="0" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div class="modal" id="mpeProcessPlanningDetails" style="overflow-y: scroll;">
        <div style="max-width: 100%; padding-left: 0%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">Process Planning Details</h4>
                            <asp:Label ID="Label2" runat="server" Style="padding-left: 19px; padding-top: 10px; color: #c12424;"></asp:Label>
                            <button type="button" class="close btn-primary-purple" onclick="hidePartDetailpopup();"
                                data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="divProcessPlanning" class="docdiv">
                                <div class="inner-container">
                                    <div id="Div10" runat="server">
                                        <div id="div11" class="divInput" runat="server">
                                            <div class="ip-div text-center">
                                            </div>
                                        </div>
                                        <div id="div12" runat="server">
                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvProcessPlanningDetails" runat="server" AutoGenerateColumns="False"
                                                    ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium orderingfalse"
                                                    OnRowDataBound="gvProcessPlanningDetails_OnRowDataBound"
                                                    DataKeyNames="PPDID,BOMID">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                            ItemStyle-Width="15%">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="chkall" runat="server" onclick="return checkAllItems(this);" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkQC" CssClass="chkpartname" runat="server" AutoPostBack="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Part Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPartName" runat="server" Text='<%# Eval("PartName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:CheckBoxList ID="chkPP" runat="server" RepeatDirection="Horizontal"></asp:CheckBoxList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-12 p-t-10 text-center">
                                                <asp:LinkButton Text="Save Process Planning" OnClick="btnSavePP_Click" CssClass="btn btn-cons btn-success" ID="btnSavePP"
                                                    runat="server" />
                                            </div>

                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvProcessPlanning" runat="server" AutoGenerateColumns="False"
                                                    ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found"
                                                    CssClass="table table-hover table-bordered medium orderingfalse"
                                                    OnRowCommand="gvProcessPlanning_OnRowCommand" OnRowDataBound="gvProcessPlanning_OnRowDataBound"
                                                    DataKeyNames="PPDID">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Part Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPartName" runat="server" Text='<%# Eval("PartName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Process Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProcessName" runat="server" Text='<%# Eval("PlanningName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                    CommandName="DeletePlanning">
                                                          <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <div class="col-sm-12 text-center">
                                        <asp:LinkButton Text="Share" CssClass="btn btn-cons btn-success" ID="btnShare"
                                            OnClientClick="return SaveprocessPlanning();" runat="server" />
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
