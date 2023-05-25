<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="PartProcessMaster.aspx.cs" Inherits="Pages_PartProcessMaster" ClientIDMode="Predictable" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function deleteConfirm(PMID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Process Master Details Delete permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                __doPostBack('deletegvrow', PMID);
            });
            return false;
        }

        function ShowAddPopUp() {
            $('#mpeAdd').modal({
                backdrop: 'static'
            });
            return false;
        }

        function ProcessRateValidate(ele) {
            if ($('#' + ele.id).is(":checked")) {
                $('#' + ele.id).closest('tr').find('.reqfield').remove();
                $('#' + ele.id).closest('tr').find("[type='text']").after('<span class="reqfield" style="color:red">*</span>');
                $('#' + ele.id).closest('tr').find("[type='text']").addClass('mandatoryfield');
            }
            else {
                $('#' + ele.id).closest('tr').find('span').remove('span.reqfield')
                $('#' + ele.id).closest('tr').find("[type='text']").removeClass('mandatoryfield');
            }
            // return false;
        }

        //process assigned to some rfp
        //Split up of Process rate (98%) should be equal to 100%

        function Validate() {

            var bool = true;
            var msg = Mandatorycheck('ContentPlaceHolder1_upDocumenttype');

            if (msg) {

                if ($('#<%=gvPartProcessDetails.ClientID %>').find('input:checked').length < 1) {
                    ErrorMessage('No Part Slected');
                    bool = false;
                }
                else {

                    var sum = 0;
                    $('#ContentPlaceHolder1_gvPartProcessDetails').find('.mandatoryfield').each(function () {
                        sum = parseFloat(sum) + parseFloat($(this).val());
                    });

                    if (sum != 100) {
                        ErrorMessage('Error', 'Split up of Process rate (' + sum + '%) should be equal to 100%')
                        bool = false;
                    }
                }
            }
            else {
                $('#ContentPlaceHolder1_ddlMaterialName').focus();
                bool = false;
            }

            if (bool) {

                var str = "";

                $('#ContentPlaceHolder1_gvPartProcessDetails').find('.mandatoryfield').each(function () {
                    if (str == "") {
                        str = $(this).val();
                    }
                    else {
                        str = str + ',' + $(this).val();
                    }
                });

                $('#<%=hdnProcessRate.ClientID %>').val(str);

                return true;
            }
            else {
                return false;
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
                                    <h3 class="page-title-head d-inline-block">
                                        Part Process Master</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Master</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Part Process Master</li>
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
                        <div class="ip-div text-center">
                            <div id="divAdd" runat="server">
                                <div class="col-sm-12 text-center">
                                    <div class="col-sm-4">
                                        <label class="form-label mandatorylbl">
                                            Part Name
                                        </label>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:DropDownList ID="ddlMaterialName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMaterialName_SelectIndexChanged"
                                            CssClass="form-control mandatoryfield" Width="70%" ToolTip="Select Material Name"
                                            TabIndex="1">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>
                            </div>
                            <div id="divAddNew" runat="server">
                                <div class="col-sm-12 text-center p-t-20">
                                    <asp:LinkButton ID="lbtnAddNew" runat="server" Text="Add New Process" OnClientClick="return ShowAddPopUp();"
                                        CssClass="btn btn-success add-emp"></asp:LinkButton>
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
                                                    <asp:Label ID="lbltitle" runat="server" Text="Part Process Details"></asp:Label></p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvPartProcessDetails" runat="server" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                OnRowDataBound="gvPartProcessDetails_OnRowDataBound" HeaderStyle-HorizontalAlign="Center"
                                                DataKeyNames="PMID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="10%" ItemStyle-Width="10%" ControlStyle-CssClass="nosort"
                                                        ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkitems" runat="server" onclick="ProcessRateValidate(this);" AutoPostBack="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="10%" ItemStyle-Width="10%"
                                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Process Name" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProcessName" runat="server" Text='<%# Eval("ProcessName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="process rate split up ( % )" ItemStyle-CssClass="text-left"
                                                        HeaderStyle-Width="20%" ItemStyle-Width="20%" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtProcessRate" runat="server" onkeypress="return fnAllowNumeric();"
                                                                Text='<%# Eval("ProcessRate")%>' CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10%" ItemStyle-Width="10%"
                                                        ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDelete" runat="server" OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("PMID")) %>'>
                                                            <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnProcessRate" runat="server" />
                                        </div>
                                        <div class="col-sm-12 text-center">
                                            <asp:LinkButton ID="lbtnSubmit" runat="server" Text="Submit" OnClientClick="return Validate();"
                                                OnClick="btnSubmit_Click" CssClass="btn btn-success"></asp:LinkButton>
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
                                Part Process Details</h4>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div class="inner-container">
                                <div id="divQPDetails" runat="server">
                                    <div class="col-sm-12">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label mandatorylbl">
                                                Process Name</label>
                                        </div>
                                        <div class="col-sm-8 text-left">
                                            <asp:TextBox ID="txtProcessName" runat="server" Width="70%" CssClass="form-control mandatoryfield"
                                                placeholder="Enter Process Name">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10 text-center">
                                        <asp:LinkButton Text="Save" CssClass="btn btn-cons btn-save  AlignTop" ID="btnSaveProcessDetails"
                                            runat="server" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divQPDetails');"
                                            OnClick="btnSaveProcessDetails_Click" />
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                </div>
                            </div>
                            <div class="modal-footer">
                            </div>
                        </div>
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
