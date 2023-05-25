<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="OfferStatusHistory.aspx.cs" ClientIDMode="Predictable" Inherits="Pages_OfferStatusHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript"> 

    </script>

    <style type="text/css">
        table#ContentPlaceHolder1_rblOfferStatusType td label {
            color: brown;
            font-weight: bold;
        }

        table#ContentPlaceHolder1_rblOfferStatusType td {
            padding: 5px;
        }

        table#ContentPlaceHolder1_rblOfferStatusType {
            margin-left: 34%;
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
                                    <h3 class="page-title-head d-inline-block">Offer Status History</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Design</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Offer Status History</li>
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

                                <p style="font-size: 21px; font-weight: bold; color: blueviolet;">
                                    Only Offer Dispatched Enquiry And Customer PO Unraised Details Available 
                                </p>

                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Customer Name</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="true" CssClass="form-control mandatoryfield"
                                            OnSelectedIndexChanged="ddlCustomerName_SelectIndexChanged" Width="70%" ToolTip="Select Enquiry Number">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Enquiry Number</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlEnquiryNumber" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEnquiryNumber_SelectIndexChanged"
                                            CssClass="form-control mandatoryfield" Width="70%" ToolTip="Select Enquiry Number">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Offer No
                                        </label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlOfferNo" runat="server" AutoPostBack="true"
                                            CssClass="form-control mandatoryfield" Width="70%" ToolTip="Select Offer Number">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10">
                                    <asp:RadioButtonList ID="rblOfferStatusType" runat="server" CssClass="radio radio-success" AutoPostBack="true"
                                        OnSelectedIndexChanged="rblOfferStatusType_OnSelectIndexChanged" RepeatDirection="Vertical">
                                    </asp:RadioButtonList>
                                </div>

                                <div class="col-sm-12 p-t-10" id="divOfferLost" runat="server" visible="false">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Offer Lost
                                        </label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlOfferLost" runat="server"
                                            CssClass="form-control mandatoryfield" Width="70%" ToolTip="Select Offer Lost">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10" id="divDate" runat="server" visible="false">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Next Review Date
                                        </label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:TextBox ID="txtNextReviewDate" runat="server" placeholder="Enter Next Date"
                                            CssClass="form-control datepicker mandatoryfield"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Remarks
                                        </label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:TextBox ID="txtRemarks" runat="server" Rows="2" TextMode="MultiLine" placeholder="Enter Remarks"
                                            CssClass="form-control mandatoryfield"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-8">
                                        <asp:LinkButton ID="btnOfferReview" CssClass="btn btn-success" runat="server"
                                            Text="Submit" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divInput');" OnClick="btnOfferReviewStatus_Click"></asp:LinkButton>
                                        <asp:LinkButton ID="btncancel" CssClass="btn btn-success" runat="server"
                                            Text="Cancel"
                                            OnClick="btncancel_Click"></asp:LinkButton>
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
                                        <div id="divGrid" runat="server">
                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvOfferStatusHistory" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                    OnRowCommand="gvOfferStatusHistory_OnRowCommand" HeaderStyle-HorizontalAlign="Center" DataKeyNames="OSHID">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Customer Name" ItemStyle-CssClass="text-center"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCustomerName" runat="server" Text='<%# Eval("ProspectName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Enquiry Number" ItemStyle-CssClass="text-center"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEnquiryNo" runat="server" Text='<%# Eval("EnquiryNumber")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Offer No" ItemStyle-CssClass="text-center"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOfferNo" runat="server" Text='<%# Eval("OfferNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Order Status" ItemStyle-CssClass="text-center"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOrderStatus" runat="server" Text='<%# Eval("OrderStatus")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Next Review Date" ItemStyle-CssClass="text-center"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNextDate" runat="server" Text='<%# Eval("ReviewDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Remarks" ItemStyle-CssClass="text-center"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblremarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Created By" ItemStyle-CssClass="text-center"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCreatedBy" runat="server" Text='<%# Eval("CreatedUser")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Created On" ItemStyle-CssClass="text-center"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcreatedon" runat="server" Text='<%# Eval("CreatedDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Modified User" ItemStyle-CssClass="text-center"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblmodifiedname" runat="server" Text='<%# Eval("ModifiedBy")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Modified Date" ItemStyle-CssClass="text-center"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblModifiedDate" runat="server" Text='<%# Eval("ModifiedOn")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Edit" ItemStyle-CssClass="text-center"
                                                            HeaderStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnEdit" runat="server" Text="Add"
                                                                    CommandName="EditOffer" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                                <img src="../Assets/images/edit-ec.png"/></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                            </div>

                                            <asp:HiddenField ID="hdnOSHID" Value="0" runat="server" />
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

