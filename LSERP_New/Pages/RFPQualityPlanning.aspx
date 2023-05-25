<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="RFPQualityPlanning.aspx.cs" Inherits="Pages_RFPQualityPlanning" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function ShowViewPopUp() {
            $('#mpeView').modal({
                backdrop: 'static'
            });
            return false;
        }

        function hideAddPopUp() {
            $("#mpeView").modal("show");
            $('#mpeAdd').modal("hide");
            return false;
        }

        function ShowAddPopUp() {
            $('#mpeAdd').modal({
                backdrop: 'static'
            });
            $("#mpeView").modal("hide");
            return false;
        }

        function ShowCloseAddPopUP() {
            $("#mpeView").modal("show");
            return false;
        }

        function ShowAssemplyPlanningPopUp() {
            $('#mpeAssemplyPlanning').modal("show");
            $('#mpeView').modal("hide");
            return false;
        }

        function HideAssemplyPlanningPopUp() {
            $('#mpeAssemplyPlanning').modal("hide");
            return false;
        }

        function UpdateRFPQPStatus() {
            swal({
                title: "No Further Edit Once Shared.",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Save it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('UpdateRFPQPStatus', null);
            });
            return false;
        }

        function rblWPSLSIChanged() {
            if ($(event.target).val() == "0") {
                $(event.target).closest('td').find('.chosen-container').css("display", "none");
                $(event.target).closest('td').find('select').removeClass('mandatoryfield');
                $(event.target).closest('td').find('select').closest('td').find('.reqfield').remove();
            }
            else {
                $(event.target).closest('td').find('.chosen-container').css("display", "block");
                $(event.target).closest('td').find('select').before('<span class="reqfield" style="color:red">*</span>');
                $(event.target).closest('td').find('select').addClass('mandatoryfield');
            }
        }


        function rblWPSLSI() {
            $('#ContentPlaceHolder1_gvRFPQualityPlanning').find('select').chosen();
            if ($('#ContentPlaceHolder1_gvRFPQualityPlanning').find('select').closest('td').find('[type="radio"]:checked').val() == "0") {
                $('#ContentPlaceHolder1_gvRFPQualityPlanning').find('select').closest('td').find('.chosen-container').css("display", "none");
                $('#ContentPlaceHolder1_gvRFPQualityPlanning').find('select').closest('td').find('.chosen-container').removeClass('mandatoryfield');
                $('#ContentPlaceHolder1_gvRFPQualityPlanning').find('select').closest('td').find('.chosen-container').find('.reqfield').remove();
            }
            else {
                $('#ContentPlaceHolder1_gvRFPQualityPlanning').find('select').closest('td').find('.chosen-container').css("display", "block");
                $('#ContentPlaceHolder1_gvRFPQualityPlanning').find('select').closest('td').find('.chosen-container').removeClass('mandatoryfield');
                $('#ContentPlaceHolder1_gvRFPQualityPlanning').find('select').closest('td').find('.chosen-container').find('.reqfield').remove();
            }
        }

        function rblLSIQCApplicableChanged(ele) {
            if ($(ele).find('[type="radio"]:checked').val() == '0') {
                $('#ContentPlaceHolder1_gvRFPQualityPlanning').find('.LSIQCApplicable').find($('[value="0"]')).prop('checked', true);
                $(event.target).closest('td').find('select').removeClass('mandatoryfield');
                $('#ContentPlaceHolder1_gvRFPQualityPlanning').find('.chosen-container').css("display", "none");
                $(event.target).closest('td').find('select').closest('td').find('.reqfield').remove();
            }
            else {
                $('#ContentPlaceHolder1_gvRFPQualityPlanning').find('.LSIQCApplicable').find($('[value="1"]')).prop('checked', true);
                $(event.target).closest('td').find('select').addClass('mandatoryfield');
                $('#ContentPlaceHolder1_gvRFPQualityPlanning').find('.chosen-container').css("display", "block");
                $(event.target).closest('td').find('select').before('<span class="reqfield" style="color:red">*</span>');
            }
        }

        function rblListQCAccepted(ele) {
            if ($(ele).find('[type="radio"]:checked').val() == '0') {
                $('#ContentPlaceHolder1_gvRFPQualityPlanning').find('.LSIQCAccepted').find($('[value="0"]')).prop('checked', true);
            }
            else {
                $('#ContentPlaceHolder1_gvRFPQualityPlanning').find('.LSIQCAccepted').find($('[value="1"]')).prop('checked', true);
            }
        }
        function rblClientTPIAApplicable(ele) {
            if ($(ele).find('[type="radio"]:checked').val() == '0') {
                $('#ContentPlaceHolder1_gvRFPQualityPlanning').find('.ClientQCApplicable').find($('[value="0"]')).prop('checked', true);
            }
            else {
                $('#ContentPlaceHolder1_gvRFPQualityPlanning').find('.ClientQCApplicable').find($('[value="1"]')).prop('checked', true);
            }
        }

        function rblClientTPIAAccepted(ele) {
            if ($(ele).find('[type="radio"]:checked').val() == '0') {
                $('#ContentPlaceHolder1_gvRFPQualityPlanning').find('.ClientQCAccepted').find($('[value="0"]')).prop('checked', true);
            }
            else {
                $('#ContentPlaceHolder1_gvRFPQualityPlanning').find('.ClientQCAccepted').find($('[value="1"]')).prop('checked', true);
            }
        }
        function rblDocumentMandatory(ele) {
            if ($(ele).find('[type="radio"]:checked').val() == '0') {
                $('#ContentPlaceHolder1_gvRFPQualityPlanning').find('.DocumentMandatory').find($('[value="0"]')).prop('checked', true);
            }
            else {
                $('#ContentPlaceHolder1_gvRFPQualityPlanning').find('.DocumentMandatory').find($('[value="1"]')).prop('checked', true);
            }
        }

        function deleteConfirm(PAPDID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Assemply Planning will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deleteAssemplyPlanning', PAPDID);
            });
            return false;
        }

        function multiselectPartAssemblyQC(multivalue) {
            debugger;
            var test = multivalue.trim();
            var testArray = test.split(',');
            $('#ContentPlaceHolder1_liPartAssemblyQC').val(testArray);
        }

        function multiselectFinalAssemblyQC(multivalue) {
            debugger;
            var test = multivalue.trim();
            var testArray = test.split(',');
            $('#ContentPlaceHolder1_liFinalAssemblyQC').val(testArray);
        }




    </script>

    <style type="text/css">
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
                                    <h3 class="page-title-head d-inline-block">RFP Quality Planning Details</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="AdminHome.aspx">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Quality</a></li>
                                <li class="active breadcrumb-item" aria-current="page">RFPQualityPlanning</li>
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
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlRFPNo" runat="server" AutoPostBack="true" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlRFPNo_SelectIndexChanged" Width="70%" ToolTip="Select RFP No">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <%--     <div class="col-sm-12 p-t-10">
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
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="col-md-12 row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                                    <asp:Label ID="lbltitle" runat="server" Text="RFP Item Quality Planning Details"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvItemDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
												OnRowDataBound="gvItemDetails_OnRowDataBound"
                                                OnRowCommand="gvItemDetails_OnRowCommand" DataKeyNames="RFPDID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Name" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Drawing Name" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldrawingname" runat="server" Text='<%# Eval("Drawingname")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quality Planning" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnView" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="QPView"><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="QP Status" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPlanningStatus" CssClass="PlanningStatus" runat="server" Text='<%# Eval("QPStatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="AP Status" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAPStatus" runat="server" Text='<%# Eval("APStatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Assemply Planning" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnAPView" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="APView"><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Reverse (AP)" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnAPReverse" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="ReverseAP"><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Need AP?">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnupdateAPNeed" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="NoNeed" Text="UPDATE" CssClass="btn btn-cons btn-success"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnEDID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnRFPDID" runat="server" Value="0" />
                                        </div>
                                        <div class="col-sm-12 p-t-20 text-center">
                                            <asp:LinkButton ID="btnSaveAndShare" runat="server" Text="Save RFP QP Status" OnClick="btnSaveAndShare_Click"
                                                CssClass="btn btn-cons btn-success"></asp:LinkButton>
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
        <div style="max-width: 81%; padding-left: 10%; padding-top: 5%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="upView" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">Part Inspection Planning -</h4>
                            <asp:Label ID="lblPartName_QP" runat="server" Style="font-weight: bold; padding-left: 30px;"></asp:Label>
                            <button type="button" class="close btn-primary-purple" onclick="ShowCloseAddPopUP();"
                                data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="docdiv" class="docdiv">
                                <div class="inner-container">
                                    <div id="MRNBlocking" runat="server">
                                        <div id="divInput_A" runat="server">
                                            <div class="col-sm-12 p-t-10" id="divstockmonitor" runat="server" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvRFPQualityPlanning" runat="server" AutoGenerateColumns="False"
                                                    ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium pagingfalse"
                                                    OnRowDataBound="gvRFPQualityPlanning_OnRowDataBound" OnDataBound="gvRFPQualityPlanning_OnDataBound"
                                                    DataKeyNames="QPDID,RFPQPDID">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Stage" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStage" runat="server" Text='<%# Eval("Stage")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Sequence" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSequence" runat="server" Text='<%# Eval("Sequence")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Type Of Check" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTypeOfCheck" runat="server" Text='<%# Eval("TypeOfCheck")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Applicable/Not Applicable" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                            <HeaderTemplate>
                                                                <span>LSI -QC</span>
                                                                <asp:RadioButtonList ID="rblListQCApplicable_H" runat="server" CssClass="radio radio-success LSIQCApplicable_H"
                                                                    onchange="rblLSIQCApplicableChanged(this);" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                    <asp:ListItem Value="1">Applicable</asp:ListItem>
                                                                    <asp:ListItem Selected="True" Value="0">Not Applicable</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:RadioButtonList ID="rblListQCApplicable" onChange="rblWPSLSIChanged();" runat="server"
                                                                    CssClass="radio radio-success LSIQCApplicable" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                    <asp:ListItem Value="1">Required</asp:ListItem>
                                                                    <asp:ListItem Selected="True" Value="0">Not Required</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                                <asp:DropDownList ID="ddlWPS" runat="server" CssClass="form-control chosenfalse ddlWPSNumber"
                                                                    ToolTip="Select WPS No">
                                                                    <asp:ListItem Selected="True" Value="0" Text="-- Select WPS No --"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--   <asp:TemplateField HeaderText="Accepted/Not Accepted" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                            <HeaderTemplate>
                                                                <span>Accepted/Not Accepted</span>
                                                                <asp:RadioButtonList ID="rblListQCAccepted_H" runat="server" CssClass="radio radio-success"
                                                                    onchange="rblListQCAccepted(this);" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                    <asp:ListItem Selected="True" Value="1">A</asp:ListItem>
                                                                    <asp:ListItem Value="0">NA</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:RadioButtonList ID="rblListQCAccepted" runat="server" CssClass="radio radio-success LSIQCAccepted"
                                                                    RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                    <asp:ListItem Selected="True" Value="1">A</asp:ListItem>
                                                                    <asp:ListItem Value="0">NA</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Accepted/Not Accepted" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                            <HeaderTemplate>
                                                                <span>CLIENT/TPIA </span>
                                                                <asp:RadioButtonList ID="rblClientTPIAApplicable_H" runat="server" CssClass="radio radio-success"
                                                                    onchange="rblClientTPIAApplicable(this);" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                    <asp:ListItem Value="1">Applicable</asp:ListItem>
                                                                    <asp:ListItem Selected="True" Value="0">Not Applicable</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <div id="divWPS" runat="server">
                                                                    <asp:RadioButtonList ID="rblClientTPIAApplicable" runat="server" CssClass="radio radio-success ClientQCApplicable"
                                                                        RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                        <asp:ListItem Value="1">A</asp:ListItem>
                                                                        <asp:ListItem Selected="True" Value="0">NA</asp:ListItem>
                                                                    </asp:RadioButtonList>
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--  <asp:TemplateField HeaderText="Accepted/Not Accepted" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                            <HeaderTemplate>
                                                                <span>Accepted/Not Accepted</span>
                                                                <asp:RadioButtonList ID="rblClientTPIAAccepted_H" runat="server" CssClass="radio radio-success"
                                                                    onchange="rblClientTPIAAccepted(this);" RepkeatDirection="Horizontal" RepeatLayout="Flow">
                                                                    <asp:ListItem Selected="True" Value="1">A</asp:ListItem>
                                                                    <asp:ListItem Value="0">NA</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:RadioButtonList ID="rblClientTPIAAccepted" runat="server" CssClass="radio radio-success ClientQCAccepted"
                                                                    RepkeatDirection="Horizontal" RepeatLayout="Flow">
                                                                    <asp:ListItem Selected="True" Value="1">A</asp:ListItem>
                                                                    <asp:ListItem Value="0">NA</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Upload Document-Mandatory/Not Required" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                            <HeaderTemplate>
                                                                <span>Document-Mandatory/Not Required</span>
                                                                <asp:RadioButtonList ID="rblDocumentMandatory_H" runat="server" CssClass="radio radio-success"
                                                                    onchange="rblDocumentMandatory(this);" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                    <asp:ListItem Value="1">Mandatory</asp:ListItem>
                                                                    <asp:ListItem Selected="True" Value="0">Not Required</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:RadioButtonList ID="rblDocumentMandatory" runat="server" CssClass="radio radio-success DocumentMandatory"
                                                                    RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                    <asp:ListItem Value="1">M</asp:ListItem>
                                                                    <asp:ListItem Selected="True" Value="0">NR</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:HiddenField ID="hdnstockqty" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdnPAPDID" runat="server" Value="0" />
                                            </div>
                                            <div class="col-sm-12 p-t-10 text-left">
                                                <div class="col-sm-4 text-center">
                                                    <label>Remarks</label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:TextBox ID="txtremarks" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="2"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-4">
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-20 text-center">
                                                <asp:LinkButton ID="btnSave" runat="server" Text="Save" OnClick="btnSaveRFPQPDetails"
                                                    OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divInput_A');" CssClass="btn btn-cons btn-success"></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                        </div>
                        </div> </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal" id="mpeView" style="overflow-y: scroll;">
        <div style="max-width: 81%; padding-left: 10%; padding-top: 5%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD"></h4>
                            <asp:Label ID="lblItemName_QP" runat="server" Style="font-weight: bold; padding-left: 30px;"></asp:Label>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="Div2" class="docdiv">
                                <div class="inner-container">
                                    <div id="Div3" runat="server">
                                        <div id="div4" runat="server">
                                            <div class="col-sm-12 p-t-10" id="div5" runat="server" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvPartNameDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium pagingfalse"
                                                    OnRowCommand="gvPartNameDetails_OnRowCommand" DataKeyNames="BOMID">
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

                                                        <asp:TemplateField HeaderText="PLLANNED DATE" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQPDate" runat="server" Text='<%# Eval("QPStartedOn")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Stage 1" ItemStyle-CssClass="text-center"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStage1Status" runat="server" Text='<%# Eval("QPStage1Status")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Stage 2" ItemStyle-CssClass="text-center"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStage2Status" runat="server" Text='<%# Eval("QPStage2Status")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblremarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Stage 1" ItemStyle-CssClass="text-center"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnStage1" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                    CommandName="stage1"><img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Stage 2" ItemStyle-CssClass="text-center"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnStage2" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                    CommandName="stage2"><img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <%--      <asp:TemplateField HeaderText="Inspection Planning" ItemStyle-CssClass="text-center"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnView" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                    CommandName="View"><img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                    </Columns>
                                                </asp:GridView>
                                                <asp:HiddenField ID="hdnBOMID" runat="server" Value="0" />
                                                <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                                            </div>
                                            <div class="col-sm-12 p-t-20 text-center">
                                                <asp:LinkButton ID="btnSaveItemQPStatus" runat="server" Text="SaveQPStatus" OnClick="btnSaveItemQPStatus_Click"
                                                    CssClass="btn btn-cons btn-success"></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                        </div>
                        </div> </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal" id="mpeAssemplyPlanning" style="overflow-y: scroll;">
        <div style="max-width: 81%; padding-left: 10%; padding-top: 5%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD"></h4>
                            <asp:Label ID="lblItemName_AP" runat="server" Style="font-weight: bold; padding-left: 30px; color: brown;"></asp:Label>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="divInput_AP" class="docdiv">
                                <div class="inner-container">
                                    <div class="col-sm-12">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label mandatorylbl">
                                                Part Name 1
                                            </label>
                                        </div>
                                        <div class="col-sm-4 text-left">
                                            <asp:DropDownList ID="ddlPartName1_AP" runat="server" CssClass="form-control mandatoryfield"
                                                Style="width: 336px;" ToolTip="Select Part Name" TabIndex="1">
                                                <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label mandatorylbl">
                                                Part Name 2
                                            </label>
                                        </div>
                                        <div class="col-sm-4 text-left">
                                            <asp:DropDownList ID="ddlPartName2_AP" runat="server" CssClass="form-control mandatoryfield" Style="width: 336px;"
                                                ToolTip="Select Part Name" TabIndex="1">
                                                <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label mandatorylbl">
                                                Select WPS Number
                                            </label>
                                        </div>
                                        <div class="col-sm-4 text-left">
                                            <asp:DropDownList ID="ddlWPSnumber_AP" runat="server" CssClass="form-control mandatoryfield"
                                                ToolTip="Select WPS Number">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label">
                                                QC
                                            </label>
                                        </div>
                                        <div class="col-sm-4 text-left">
                                            <asp:ListBox ID="liPartAssemblyQC" runat="server" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label">
                                                Remarks
                                            </label>
                                        </div>
                                        <div class="col-sm-4 text-left">
                                            <asp:TextBox ID="txtRemarks_AP" runat="server" CssClass="form-control" ToolTip="Enter Remarks"
                                                Width="70%" TextMode="MultiLine" Rows="2" placeholder="Enter Remarks">
                                            </asp:TextBox>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-20 text-center">
                                        <asp:LinkButton ID="btnSaveAP" runat="server" Text="Save" OnClientClick="return Mandatorycheck('divInput_AP')"
                                            OnClick="btnSaveAP_Click" CssClass="btn btn-cons btn-success"></asp:LinkButton>
                                    </div>
                                    <div class="col-sm-12 p-t-10" id="div9" runat="server" style="overflow-x: scroll;">
                                        <asp:GridView ID="gvAssemplyPlanningDetails_AP" runat="server" AutoGenerateColumns="False"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" OnRowCommand="gvAssemplyPlanningDetails_AP_OnRowCommand"
                                            OnRowDataBound="gvAssemplyPlanningDetails_AP_OnRowDataBound" CssClass="table table-hover table-bordered medium" DataKeyNames="PAPDID">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Part1" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPart1" runat="server" Text='<%# Eval("PartName1")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Part2" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPart2" runat="server" Text='<%# Eval("PartName2")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="WPS Number" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWPSNumber" runat="server" Text='<%# Eval("WPSNumber")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTypeOfCheck" runat="server" Text='<%# Eval("TypeOfCheck")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            CommandName="EditAP"><img src="../Assets/images/edit-ec.png" style="height:20px" alt=""></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnDelete" runat="server" OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("PAPDID")) %>'>
                                                            <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label">
                                                Final Assembly QC
                                            </label>
                                        </div>
                                        <div class="col-sm-4 text-left">
                                            <asp:ListBox ID="liFinalAssemblyQC" runat="server" CssClass="form-control" SelectionMode="Multiple"></asp:ListBox>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10 text-center">
                                        <asp:LinkButton ID="btnSaveFinalAssembly_QC" runat="server" Text="Save"
                                            OnClick="btnSaveFinalAssembly_QC_Click" CssClass="btn btn-cons btn-success"></asp:LinkButton>
                                    </div>
                                    <div class="col-sm-12 p-t-10 text-center">
                                        <asp:LinkButton ID="btnShareAssemplyPlanning" runat="server" Text="Share Assemply Planning"
                                            OnClick="btnShareAssemplyPlanning_Click" CssClass="btn btn-cons btn-success"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                        </div>
                        </div> </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
