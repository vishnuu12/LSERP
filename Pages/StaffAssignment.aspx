<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="StaffAssignment.aspx.cs" Inherits="Pages_StaffAssignment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {
            $('#<%=gvStaffAssignmentDetails.ClientID %>').find('input:text[id$="txtDesignerDeadLineDate"]').datepicker({
                format: 'dd/mm/yyyy'
            });
        });

        //        function ValidateDate(ele) {
        //            var DeadLineDate = $(ele).val();

        //            today = new Date();
        //            var cdd = today.getDate();
        //            var cmm = today.getMonth() + 1; //As January is 0.
        //            var cyyyy = today.getFullYear();

        //            var DDParts = DeadLineDate.spli('/');

        //            var DD = new Date(DDParts[1] + "/" + DDParts[0] + "/" + DDParts[2]);
        //            var CD = new Date(cmm + "/" + cdd + "/" + cyyyy);

        //            if (CD > DD) {
        //                $(event.target).notify('Deadline date shoud allow the current date.', { arrowShow: true, position: 'r,r', autoHide: true });
        //                return false;
        //            }
        //            else {
        //                return true;
        //            }
        //        }


        //changeDate
        $('.datepicker').on('change', function (e) {
            var DeadLineDate = $(this).val();

            today = new Date();
            var cdd = today.getDate();
            var cmm = today.getMonth() + 1; //As January is 0.
            var cyyyy = today.getFullYear();

            var DDParts = DeadLineDate.spli('/');

            var DD = new Date(DDParts[1] + "/" + DDParts[0] + "/" + DDParts[2]);
            var CD = new Date(cmm + "/" + cdd + "/" + cyyyy);

            if (CD > DD) {
                $(event.target).notify('Deadline date shoud allow the current date.', { arrowShow: true, position: 'r,r', autoHide: true });
                return false;
            }
            else {
                return true;
            }
        });

        function ShowEnquiryAttachementsPopUp() {
            $('#mpeEnquiryAttachements').modal({
                backdrop: 'static'
            });
        }

        function ShowViewdocsPopUp() {
            $('#mpeViewdocs').modal({
                backdrop: 'static'
            });
        }

        function ShowProcessImage() {
            var autocomplete = document.getElementById('<%= txtEmployeeID.ClientID %>');
            autocomplete.style.backgroundImage = 'url(../Assets/images/loading.gif)';
            autocomplete.style.backgroundRepeat = 'no-repeat';
            autocomplete.style.backgroundPosition = 'right';
        }

        function HideProcessImage() {
            var autocomplete = document.getElementById('<%= txtEmployeeID.ClientID %>');
            autocomplete.style.backgroundImage = 'none';
        }

        function selectedvalue(sender, e) {
            var RegNo = $get('<%= txtEmployeeID.ClientID %>');
            var id = e.get_value();
            if (id.value == "Record not available") {
                RegNo.value = "";
            }
            else {
                RegNo.value = id;
                document.getElementById("<%= btnGet.ClientID %>").click();
            }
        }

    </script>

    <style type="text/css">
        .radio-success label:nth-child(2) {
            color: red !important;
        }

        .radio-success label:nth-child(4) {
            color: #0acc0a !important;
        }

        .grid-title-new {
            color: #FFFFFF !important;
            background-color: #098E83;
            margin-bottom: 0px;
            border-bottom: 0px;
        }

        /*-- */
        .StyleIcon {
            height: 16px;
            width: 16px;
            color: Gray;
            border-radius: 25% 10% 40% 10%;
            box-shadow: none;
            display: inline-block;
            background: none;
        }

            .StyleIcon.Red {
                height: 16px;
                width: 16px;
                color: Red;
                border-radius: 25% 10% 40% 10%;
                box-shadow: none;
                display: inline-block;
            }

        .fa-check-circle {
            color: #fff;
        }

        .fa-times {
            color: #fff;
        }

        .nav-tabs > li.active > a > span .fa-check-circle, .nav-tabs > li.active > a > span .fa-times {
            color: #448e84;
        }

        .nav-tabs > li > a {
            width: max-content;
        }

        .StyleIcon.Green {
            height: 16px;
            width: 16px;
            color: Green;
            border-radius: 25% 10% 40% 10%;
            box-shadow: 2px 3px 0 0 #afada8;
            display: inline-block;
        }

        .StyleIcon.Yellow {
            height: 16px;
            width: 16px;
            color: Yellow;
            border-radius: 25% 10% 40% 10%;
            box-shadow: 2px 3px 0 0 #afada8;
            display: inline-block;
        }

        .StyleIcon.Gray {
            width: 16px;
            height: 16px;
            display: inline-block;
            position: absolute;
            left: 5px;
        }

        .AlignLeft {
            padding-left: 0px;
        }

        .marginleft {
            margin-left: -15px;
        }

        .uppercase {
            text-transform: uppercase;
        }

        .nav-tabs > li > a {
            display: flex;
            align-items: center;
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
                                        <%--<i class="fa fa-th-large opacity-6 yellow"></i>--%>
                                        <img src="../Assets/images/pages.png" alt="" width="16px" />
                                    </span>
                                    <h3 class="page-title-head d-inline-block">Staff Assignment</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">
                                    <asp:Label ID="lblModuleName" runat="server"></asp:Label></a></li>
                                <li class="active breadcrumb-item" aria-current="page">Staff Assignment</li>
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
            <asp:UpdatePanel ID="upStaffAssignment" runat="server">
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
                                        <asp:RadioButtonList ID="rblEnquirychange" runat="server" CssClass="radio radio-success" RepeatDirection="Horizontal"
                                            AutoPostBack="true" OnSelectedIndexChanged="rblEnquirychange_OnSelectedChanged"
                                            RepeatLayout="Flow">
                                            <asp:ListItem Selected="True" Value="0">  NOT ASSIGNED  </asp:ListItem>
                                            <asp:ListItem Value="1">  ASSIGNED  </asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10" id="divauto" runat="server">
                                    <div class="col-sm-4">
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtEmployeeID" runat="server" CssClass="form-control"
                                            placeholder="Search Enquiry Number"></asp:TextBox>
                                        <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" BehaviorID="AutoCompleteEx"
                                            TargetControlID="txtEmployeeID" MinimumPrefixLength="1" EnableCaching="true"
                                            CompletionSetCount="1" CompletionInterval="500" CompletionListCssClass="autocomplete_completionListElement"
                                            OnClientPopulating="ShowProcessImage" OnClientPopulated="HideProcessImage" CompletionListItemCssClass="autocomplete_listItem"
                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="selectedvalue"
                                            ServiceMethod="GetEmployee" ServicePath="StaffAssignment.aspx">
                                        </cc1:AutoCompleteExtender>
                                        <asp:Button ID="btnGet" runat="server" CssClass="button" Text="Load" OnClick="btnget_click"
                                            ValidationGroup="A" Style="display: none;" />
                                        <%--   OnClientClick="ShowLoader();"--%>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
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
                                                    <asp:Label ID="lbltitle" runat="server" Text="Staff Assignment"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvStaffAssignmentDetails" runat="server" Width="100%" AutoGenerateColumns="false"
                                                ShowFooter="false" ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found"
                                                CssClass="table table-bordered table-hover no-more-tables tablestatesave"
                                                OnRowEditing="gvStaffAssignmentDetails_RowEditing"
                                                OnRowCancelingEdit="gvStaffAssignmentDetails_RowCancelingEdit" OnRowDataBound="gvStaffAssignmentDetails_RowDataBound"
                                                OnRowUpdating="gvStaffAssignmentDetails_RowUpdating" OnRowCommand="gvStaffAssignmentDetails_OnRowCommand"
                                                DataKeyNames="SAID,EnquiryID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Enquiry/Cust Enquiry No" HeaderStyle-CssClass="text-left"
                                                        ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEnquiryNumber" runat="server" Text='<%# Eval("EnquiryID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Customer Name" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProspectName" runat="server" Text='<%# Eval("ProspectName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Enquiry Date" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEnquiryDate" runat="server" Text='<%# Eval("EnquiryDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Staff">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStaffAssignment" runat="server" Text='<%# Eval("Staff") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <%--<EditItemTemplate>
                                                            <asp:TextBox ID="txtBankAcctNo" runat="server" CssClass="form-control" Text='<%#Eval("BankAccountNo") %>'
                                                                Width="100px"></asp:TextBox>                                    
                                                        </EditItemTemplate>--%>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sales Resource">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsalesresource" runat="server" Text='<%# Eval("Salesresource") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmployeeName" runat="server" Text='<%# Eval("EmployeeName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="ddlEmployeeName" runat="server" CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Designer DeadLine" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDesignerDeadLineDate" runat="server" Text='<%# Eval("DesignerDeadLineDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtDesignerDeadLineDate" runat="server" CssClass="form-control datepicker"
                                                                AutoComplete="off" placeholder="Enter Date">
                                                            </asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Offer Submission Date" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOfferSubmissionDate" runat="server" Text='<%# Eval("OfferSubmissionDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Customer DeadLine" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCustomerDeadLineDate" runat="server" Text='<%# Eval("CustomerDeadLine") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField ButtonType="Image" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"
                                                        ShowEditButton="true" EditText="<img src='~/images/icon_edit.png' title='Edit' />"
                                                        EditImageUrl="../Assets/images/edit-ec.png" CancelImageUrl="../Assets/images/icon_cancel.png"
                                                        UpdateImageUrl="../Assets/images/icon_update.png" ItemStyle-Wrap="false" ControlStyle-Width="20px"
                                                        ControlStyle-Height="20px" HeaderText="Edit" ValidationGroup="edit" HeaderStyle-Width="7%">
                                                        <ControlStyle CssClass="UsersGridViewButton" />
                                                    </asp:CommandField>
                                                    <asp:TemplateField HeaderText="View Enq Attachements" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnViewEnqAttch" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="ViewEnqAttachements"><img src="../Assets/images/view.png" /></asp:LinkButton>
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
    <div class="modal" id="mpeEnquiryAttachements" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="upView" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">Enquiry Attachements Details</h4>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="docdiv" class="docdiv">
                                <div class="col-sm-12 p-t-10">
                                    <asp:GridView ID="gvAttachments" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                        EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                        OnRowCommand="gvAttachments_OnRowCommand" OnRowDataBound="gvAttachments_OnRowDataBound"
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
                                            <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFileName_V" runat="server" Visible="false" Text='<%# Eval("FileName")%>'></asp:Label>
                                                    <asp:ImageButton ID="imgbtnView" runat="server" ImageUrl="" CommandName="ViewDocs"
                                                        Width="20px" Height="20px" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'></asp:ImageButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hdnAttachementID" runat="server" Value="0" />
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
                        <asp:HiddenField ID="hdnAttachementFlag" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal" id="mpeViewdocs" style="height: 125%; top: -10%;">
        <div class="modal-dialog" style="height: 100%;">
            <div class="modal-content" style="height: 100%;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always" Style="height: 100%;">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H1">
                                <div class="text-center">
                                    Documents
                                    <asp:Label ID="Label2" Text="" runat="server"></asp:Label>
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal">
                                x</button>
                        </div>
                        <div class="modal-body" style="height: 100%;">
                            <div class="inner-container" style="height: 100%;">
                                <div class="ip-div text-center" style="margin-bottom: 10px;">
                                    <%--   <asp:LinkButton Text="Download" CssClass="btn btn-save btn-cons AlignTop" ID="btndownloaddocs"
                                        OnClick="btndownloaddocs_Click" runat="server" />--%>
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
</asp:Content>
