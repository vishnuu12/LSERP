<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="ViewAllDepartMentDocs.aspx.cs" Inherits="Pages_ViewAllDepartMentDocs" ClientIDMode="Predictable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function OpenTab(Name) {
            if ($(event.target).closest('li').is('.active') == false) {
                $('#ContentPlaceHolder1_hdnTabName').val(Name);
                document.getElementById('<%=btntab.ClientID %>').click();
            }
            else
                return false;
        }

        function ActiveTab(Name) {
            var names = ["Sales", "Design", "Production", "Quality"];
            for (var i = 0; i < names.length; i++) {
                if (Name == names[i])
                    document.getElementById('ContentPlaceHolder1_li' + names[i]).className = "active";
                else
                    document.getElementById('ContentPlaceHolder1_li' + names[i]).className = "";
            }
        }

        $(document).ready(function () {
            var Name = $('#ContentPlaceHolder1_hdnTabName').val();
            var names = ["Sales", "Design", "Production", "Quality"];
            for (var i = 0; i < names.length; i++) {
                if (Name == names[i])
                    document.getElementById('ContentPlaceHolder1_li' + names[i]).className = "active";
                else
                    document.getElementById('ContentPlaceHolder1_li' + names[i]).className = "";
            }
        });

        function PrintOffer() {
            var cotent = $('#ContentPlaceHolder1_hdnpdfContent').val();
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');
            winprint.document.write(cotent);
            setTimeout(function () {
                winprint.document.close();
                winprint.focus();
                winprint.print();
                // winprint.close();
            }, 1000);

            return false;
        }


    </script>

    <style type="text/css">
        .grid-title-new {
            color: #FFFFFF !important;
            background-color: #098E83;
            margin-bottom: 0px;
            border-bottom: 0px;
        }

        .app-container {
            display: none;
        }

        .app-header {
            display: none;
        }

        .main-container_close {
            width: 98% !important;
            margin-left: 2% !important;
        }

        table {
            font-weight: bold;
            color: #000;
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
                                    <h3 class="page-title-head d-inline-block">View All Docs</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Reports</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">View All Docs</li>
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
                    <asp:PostBackTrigger ControlID="gvEnquiryAttachemnts" />
                    <asp:PostBackTrigger ControlID="gvEnquiryOfferDocs" />
                    <asp:PostBackTrigger ControlID="gvCustomerPODocs" />
                    <asp:PostBackTrigger ControlID="gvItemostingDetailsBOM" />
                    <asp:PostBackTrigger ControlID="gvRFPSheets" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" runat="server">
                            <div class="ip-div text-center">
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="col-sm-12">
                                <div id="Tabs" runat="server">
                                    <ul id="Ul1" class="nav nav-tabs grid-title-new no-boarder" runat="server">
                                        <li id="liSales" runat="server"><a href="#Sales" class="tab-content"
                                            data-toggle="tab" onclick="OpenTab('Sales');">
                                            <p style="margin-left: 10px">
                                                Sales
                                            </p>
                                        </a></li>
                                        <li id="liDesign" runat="server"><a href="#Design" class="tab-content" data-toggle="tab" onclick="OpenTab('Design');">
                                            <p style="margin-left: 10px">
                                                Design
                                            </p>
                                        </a></li>
                                        <li id="liProduction" runat="server"><a href="#Production" class="tab-content" onclick="OpenTab('Production');">
                                            <p style="margin-left: 10px">
                                                Production
                                            </p>
                                        </a></li>
                                        <li id="liQuality" runat="server"><a href="#Quality" class="tab-content"
                                            onclick="OpenTab('Quality');">
                                            <p style="margin-left: 10px">
                                                Quality
                                            </p>
                                        </a></li>
                                    </ul>

                                </div>
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">

                                        <div id="Sales" runat="server">
                                            <div class="col-sm-12 text-center">
                                                <label style="font-weight: bold; color: brown; font-size: 20px;">
                                                    Customer Enquiry Attachemnts
                                                </label>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <asp:GridView ID="gvEnquiryAttachemnts" runat="server" AutoGenerateColumns="False"
                                                    ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found"
                                                    OnRowCommand="gvEnquiryAttachemnts_OnRowCommand" CssClass="table table-hover table-bordered medium" DataKeyNames="FileName">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDescription_V" runat="server" Text='<%# Eval("Description")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="File Upload Date" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCreatedDate_V" runat="server" Text='<%# Eval("CreatedDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="View Enquiry Docs">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgbtnViewEnquiryDocs" runat="server" CommandName="ViewEnquiryDocs"
                                                                    Width="20px" Height="20px" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'></asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-12 text-center">
                                                <label style="font-weight: bold; color: brown; font-size: 20px;">
                                                    Offer Attachements
                                                </label>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <asp:GridView ID="gvEnquiryOfferDocs" runat="server" AutoGenerateColumns="False"
                                                    ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found"
                                                    OnRowCommand="gvEnquiryOfferDocs_OnRowCommand" CssClass="table table-hover table-bordered medium"
                                                    DataKeyNames="OfferNoPrintName">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Print Offer Details" HeaderStyle-CssClass="text-center"
                                                            ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnViewoffer" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                    CommandName="ViewOfferDetails"><img src="../Assets/images/pdf.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Offer No" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOfferNo" runat="server" Text='<%# Eval("OfferNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Offer No Rev">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOfferNoRev" runat="server" Text='<%# Eval("OfferNoRevision")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Offer Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblofferdate" runat="server" Text='<%# Eval("OfferCostApprovedDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-12 text-center">
                                                <label style="font-weight: bold; color: brown; font-size: 20px;">
                                                    Customer PO Docs
                                                </label>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <asp:GridView ID="gvCustomerPODocs" runat="server" AutoGenerateColumns="False"
                                                    ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found"
                                                    OnRowCommand="gvCustomerPODocs_OnRowCommand" CssClass="table table-hover table-bordered medium"
                                                    DataKeyNames="EnquiryNumber,POCopy,POCopyWithoutPrice">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PO No" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPONo" runat="server" Text='<%# Eval("PORefNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PoCopy" ItemStyle-CssClass="text-center" HeaderStyle-Width="10%"
                                                            ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnPoCopy"
                                                                    runat="server"
                                                                    CommandName="ViewPOCopy" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PoCopy Without Price" ItemStyle-CssClass="text-center"
                                                            HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnPoCopyWithoutPrice" runat="server"
                                                                    CommandName="ViewPoCopyWithoutPrice"
                                                                    CommandArgument='<%#((GridViewRow) Container).RowIndex %>'><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="User Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblusername" runat="server" Text='<%# Eval("Username")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PO Entry Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblpodate" runat="server" Text='<%# Eval("POEntryDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>

                                        <div id="Design" runat="server">
                                            <div class="col-sm-12 p-t-10 text-center">
                                                <label style="font-weight: bold; color: brown; font-size: 20px;">
                                                    Item Costing Details BOM
                                                </label>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <asp:GridView ID="gvItemostingDetailsBOM" runat="server" AutoGenerateColumns="False"
                                                    ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found"
                                                    OnRowCommand="gvItemostingDetailsBOM_OnRowCommand" CssClass="table table-hover table-bordered medium"
                                                    DataKeyNames="DDID,FileName">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblitemname" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Size" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblItemSize" runat="server" Text='<%# Eval("ItemSize")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Drawing Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDrawingName" runat="server" Text='<%# Eval("DrawingName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Tag No" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblItemTagNo" runat="server" Text='<%# Eval("ItemTagNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cost Estimated Date" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcostestimateddate" runat="server" Text='<%# Eval("CostEstimatedDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Estimated By" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblestimatedBy" runat="server" Text='<%# Eval("CostEstimatedBy")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="View Costing" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnviewcosting" runat="server" CommandName="ViewCosting"
                                                                    CommandArgument='<%#((GridViewRow) Container).RowIndex %>'><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Drawing File" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtndrawingfile" runat="server" CommandName="ViewFileName"
                                                                    CommandArgument='<%#((GridViewRow) Container).RowIndex %>'><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>

                                            <div class="col-sm-12 p-t-10 text-center">
                                                <label style="font-weight: bold; color: brown; font-size: 20px;">
                                                    RFP Sheets And Attachements
                                                </label>
                                            </div>

                                            <div class="col-sm-12 p-t-10">
                                                <asp:GridView ID="gvRFPSheets" runat="server" AutoGenerateColumns="False"
                                                    ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found"
                                                    OnRowCommand="gvRFPSheets_OnRowCommand" CssClass="table table-hover table-bordered medium"
                                                    DataKeyNames="RFPHID,FileName">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="RFP No" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRFPNo" runat="server" Text='<%# Eval("RFPNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="RFP Date" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRFPDate" runat="server" Text='<%# Eval("RFPDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Type Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTypename" runat="server" Text='<%# Eval("TypeName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Attach Date" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblattachdate" runat="server" Text='<%# Eval("AttachDate")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="View Attach" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtndrawingfile" runat="server" CommandName="ViewFileName"
                                                                    CommandArgument='<%#((GridViewRow) Container).RowIndex %>'><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="RFP Print" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnrfpprint" runat="server" CommandName="viewRFPPrint"
                                                                    CommandArgument='<%#((GridViewRow) Container).RowIndex %>'><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>

                                        <div id="Quality" runat="server">
                                        </div>
                                        <div id="Production" runat="server">
                                        </div>

                                        <div style="display: none;">
                                            <asp:LinkButton ID="btntab" runat="server" Text="drafting"
                                                OnClick="btntab_Click" CssClass="btn btn-cons btn-success"></asp:LinkButton>
                                        </div>
                                    </div>
                                    <asp:HiddenField ID="hdnTabName" runat="server" Value="Sales" />
                                    <asp:HiddenField ID="hdnpdfContent" runat="server" />

                                    <iframe id="ifrm" runat="server" style="display: none;"></iframe>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>

