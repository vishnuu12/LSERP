<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" 
    CodeFile="IndentToQuotationReports.aspx.cs" Inherits="Pages_IndentToQuotationReports" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="../Assets/css/datatables2.css" />
    <script type="text/javascript" src="../Assets/scripts/datatables2.js"></script>
    <script type="text/javascript"> 


        $(document).ready(function () {
            ShowDataTable();
        });
        // A $( document ).ready() block.
        function ShowDataTable() {
            $('#ContentPlaceHolder1_gvQuateSubmission').dataTable({
                "pageLength": 10,
                "bJQueryUI": true,
                "bAutoWidth": false,
                "scrollX": true,
                "autoWidth": false,
                "fnInitComplete": function () {

                    // Enable THEAD scroll bars
                    $('.dataTables_scrollHead').css('overflow', 'auto');

                    // Sync THEAD scrolling with TBODY
                    $('.dataTables_scrollHead').on('scroll', function () {
                        $('.dataTables_scrollBody').scrollLeft($(this).scrollLeft());
                    });
                }
            });
        }
        function GetFileName(ele) {
            $('#<%=hdnFileName.ClientID %>').val(ele.title);
            document.getElementById('<%=btnviewattachment.ClientID %>').click();
            return true;
        }

        function ShowViewPopUp() {
            $('#mpeViewdocs').modal({
                backdrop: 'static'
            });
            return false;
        }

        function CheckRadiobtnCondition() {
            try {
                var radiorslt = true;
                $('#ContentPlaceHolder1_gvQuateSubmission').find('.suppliercolumn').each(function () {
                    if ($(this).find('[type="radio"]:checked').length > 0) {
                        $(this).closest('td').find('[type="hidden"]').val($(this).find('[type="radio"]:checked').val());

                    }
                    else {
                        InfoMessage('', 'Please Select Supplier Name for Row number ' + $(this).closest('tr').find('.lblsno').text() + '.');
                        radiorslt = false;
                        return false;
                    }
                });
            } catch { }
        }
    </script>
    <style type="text/css">
        div.dataTables_scrollBody > table {
    border-top: none;
    margin-top: -19px !important;
    margin-bottom: 0 !important;
}
        table#ContentPlaceHolder1_gvQuateSubmission td {
            font-weight: bold;
            font-size: smaller;
            white-space: normal;
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
                                    <h3 class="page-title-head d-inline-block">Quotation Reports</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Production</a></li>
                                <li class="active breadcrumb-item" aria-current="page">Quotation Reports</li>
                            </ol>
                        </nav>
                        <a id="help" href="#" alt="" style="margin-top: 4px;">
                            <img src="../Assets/images/help.png" alt="" />
                        </a>
                    </div>
                </div>
            </div>
        </div>
        <div>
            <asp:UpdatePanel ID="upQuote" runat="server" UpdateMode="Always">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnviewattachment" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" runat="server">
                            <div class="ip-div text-center">
                                <div class="ip-div text-center">
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label">
                                                PI Number</label>
                                        </div>
                                        <div class="col-sm-6 text-left">
                                            <asp:DropDownList ID="ddlQNNumber" runat="server" AutoPostBack="true" CssClass="form-control"
                                                OnSelectedIndexChanged="ddlQNNumber_SelectIndexChanged" Width="70%" ToolTip="Select QN Number">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div class="ip-div text-center">
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-4">
                                            <label>RFP No </label>
                                            <asp:Label ID="lblRFPNo" runat="server" CssClass="form-label"></asp:Label>
                                        </div>
                                        <div class="col-sm-4">
                                            <label>Indent By </label>
                                            <asp:Label ID="lblindentby" runat="server" CssClass="form-label"></asp:Label>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
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
                                                    <asp:Label ID="lbltitle" runat="server"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvQuateSubmission" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                HeaderStyle-HorizontalAlign="Center" DataKeyNames="PID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" CssClass="lblsno" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQuateStatus" runat="server" Text='<%# Eval("QuoteStatusNew")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Approved Supplier" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQuotedSupplier" Style="font-weight: bold; color: #0b8914;" runat="server" Text='<%# Eval("QuotedSupplier")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="PO No" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPONo" runat="server" Text=' <%# Eval("PONo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="MTL CATG" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMaterialCategory" runat="server" Text=' <%# Eval("CategoryName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="MTL TYP" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMaterialType" runat="server" Text=' <%# Eval("TypeName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="MTL Grade" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMaterialGrade" runat="server" Text=' <%# Eval("GradeName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Measurement" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMeasurment" runat="server" Text=' <%# Eval("Measurment").ToString().Replace(",", "<br/>")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="MTL THK" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMaterialThickness" runat="server" Text=' <%# Eval("THKValue")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="L1 Status" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblApprovedStatus" runat="server" Text='<%# Eval("AppStatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="L1" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblApprovedBy" runat="server" Text='<%# Eval("L1ApprovedBy")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="L1 Date" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblApprovedOn" runat="server" Text='<%# Eval("L1ApprovedOn")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="L2 Status" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblApprovedStatus2" runat="server" Text='<%# Eval("AppStatus2")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="L2" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl2ApprovedBy" runat="server" Text='<%# Eval("L2ApprovedBy")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="L2 Date" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl2ApprovedOn" runat="server" Text='<%# Eval("L2ApprovedOn")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--    <asp:TemplateField HeaderText="Last Quote" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLastQuote" runat="server" Text='<%# Eval("LastQuote")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>  --%>
                                                    <asp:TemplateField HeaderText="Suppliers" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                        HeaderStyle-Width="30%" ItemStyle-Width="30%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSupplierName" CssClass="suppliercolumn" runat="server" Text='<%# Eval("SupplierDetails").ToString().Replace(",", "<br/>")%>'
                                                                Style="float: left; white-space: nowrap;">
                                                            </asp:Label>
                                                            <asp:HiddenField runat="server" ID="hdn_SUPID" Value="" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="ip-div text-center">
                                <div class="col-sm-12 p-t-10">
                                    <asp:LinkButton ID="btnviewattachment" CssClass="btnviewattachment" runat="server"
                                        OnClick="VieAttachment_Click" Style="display: none;"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hdnFileName" Value="0" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal" id="mpeViewdocs" style="height: 125%; top: -10%;">
        <div class="modal-dialog" style="height: 100%;">
            <div class="modal-content" style="height: 100%;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" Style="height: 100%;">
                    <Triggers>
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
                                </div>
                                <div class="col-sm-12" style="height: 100%;">
                                    <iframe runat="server" id="ifrm" src="#" style="width: 100%; height: 80%;" frameborder="0"></iframe>
                                    <asp:Image ID="imgDocs" runat="server" ImageUrl="#" />
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

