<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="RFPApproval.aspx.cs" Inherits="Pages_RFPApproval" ValidateRequest="false"
    ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function ShowViewPopUp() {
            $('#mpeView').modal({
                backdrop: 'static'
            });
            return false;
        }

        function ShowViewdocsPopUp() {
            $('#mpeViewdocs').modal({
                backdrop: 'static'
            });
            return false;
        }

        function ShowreplyPopUp(elmid) {
            $('#mpeReplyPopUp').modal({
                backdrop: 'static'
            });
            return false;
        }

        function checkAll(ele) {
            debugger;
            if ($(ele).is(":checked")) {
                //  if ($(ele).closest('table').find('[type="checkbox"]').closest('span').attr('css'))
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', true);
            }
            else {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', false);
            }
        }

        function RFPPrint(epstyleurl, Main, QrCode) {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var RowLength = $('#ContentPlaceHolder1_gvItemDetails_p').find('tr').length - 1;
            var BellowRowLength = $('#ContentPlaceHolder1_gvBellowDetails_p').find('tr').length - 1;

            var PageLength = parseInt(RowLength / 15);
            var BellowPageLength = parseInt(BellowRowLength / 25);

            var k = 0;
            if (RowLength > 5) {

                var BellowColLength = $('#ContentPlaceHolder1_gvBellowDetails_p').find('th').length;

                $('#ContentPlaceHolder1_gvItemDetails_p').find('tr:gt(0)').remove();
                $('#ContentPlaceHolder1_gvBellowDetails_p').find('tr:gt(0)').remove();

                $('#ContentPlaceHolder1_gvItemDetails_p').append('<tr style="text-align:center;"><td colspan="7"> As Per Annexure Enclosed </td></tr>');
                $('#ContentPlaceHolder1_gvBellowDetails_p').append('<tr style="text-align:center;"><td colspan="' + BellowColLength + '"> As Per Annexure Enclosed </td></tr>');

                //  $('#ContentPlaceHolder1_gvBellowDetails_p').append('<tr><td colspan="7" stle="text-align:center;"> As Per Annexure Enclosed </td></tr>')
                var FirstpageContent = $('#ContentPlaceHolder1_divPrint').html();
            }
            else {
                var FirstpageContent = $('#ContentPlaceHolder1_divPrint').html();
            }

            winprint.document.write("<html><head><title>");
            winprint.document.write("Offer");
            winprint.document.write("</title>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='" + epstyleurl + "'/>");
            winprint.document.write("<link rel='stylesheet' href='" + Main + "' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet'  href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/printg.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<style type='text/css'>@media print,screen { label,table th{ font-weight: bold; font-size: 15px !important;font-family:Times New Roman;color:#000 !important; }} .row{ padding-top:10px; } .page_generateoffer{ margin: 6mm; } table th{ vertical-align: middle; }  </style>");

            winprint.document.write("<div class='print-page' style='position:fixed;width:200mm;height:287mm;margin-top:5mm;margin-bottom:5mm;margin-left:5mm;margin-right:5mm'></div>");
            winprint.document.write("<table style='width:200mm;height:287mm;margin-left:20px;border:0px !important'><thead><tr><td>");
            winprint.document.write("<div class='col-sm-12 text-center p-b-30' style='padding-top: 10px; font-size: larger; font-weight: bold; font-family: Times New Roman; color: #000 !important;'>");
            winprint.document.write("REQUEST FOR PRODUCTION");
            winprint.document.write("</div>");
            winprint.document.write("</td></tr></thead>");
            winprint.document.write("<tbody><tr><td>");

            if (RowLength > 3) {
                winprint.document.write("<div class='page_generateoffer'>");
                winprint.document.write(FirstpageContent);
                winprint.document.write("</div>");

                for (var i = 0; i <= PageLength; i++) {
                    winprint.document.write("<div class='page_generateoffer'>");

                    winprint.document.write("<div class='row p-t-10'>");
                    winprint.document.write("<div class='col-sm-3 text-left'>");
                    winprint.document.write("<label>RFP No & Date</label>");
                    winprint.document.write("</div>");
                    winprint.document.write("<div class='col-sm-1'>:</div>");
                    winprint.document.write("<div class='col-sm-8'><span>" + $('#ContentPlaceHolder1_lblRFPNo_P').text() + "</span></div>");
                    winprint.document.write("</div>");

                    winprint.document.write("<div class='row p-t-10'>");
                    winprint.document.write("<div class='col-sm-3 text-left'>");
                    winprint.document.write("<label>Purchase Order No & Date</label>");
                    winprint.document.write("</div>");
                    winprint.document.write("<div class='col-sm-1'>:</div>");
                    winprint.document.write("<div class='col-sm-8'><span>" + $('#ContentPlaceHolder1_lblCustomerOrderNo_p').text() + "</span></div>");
                    winprint.document.write("</div>");

                    winprint.document.write("<div class='col-sm-12 p-t-10'>");
                    winprint.document.write("<table class='table table-hover medium' cellspacing='0' rules='all' border='1' id='ContentPlaceHolder1_gvAnnexureItemList_p_" + i + "' style='border-collapse:collapse;'>");
                    winprint.document.write("<tbody>");

                    for (var j = k; j < k + 15; j++) {
                        winprint.document.write("<tr align='center'>" + $('#ContentPlaceHolder1_gvAnnexureItemList_p').find('tr')[j].innerHTML + "</tr>");
                        if (RowLength == j) {
                            break;
                        }
                    }

                    k = k + 15;
                    winprint.document.write("</tbody>");
                    winprint.document.write("</table>");
                    winprint.document.write("</div>");

                    winprint.document.write("</div>");
                }

                k = 0;
                for (var i = 0; i <= BellowPageLength; i++) {
                    winprint.document.write("<div class='page_generateoffer'>");
                    winprint.document.write("<table class='table table-hover medium' cellspacing='0' rules='all' border='1' id='ContentPlaceHolder1_gvAnnexureBellowDetails_p_" + i + "' style='border-collapse:collapse;'>");
                    winprint.document.write("<tbody>");
                    for (var j = k; j < k + 25; j++) {
                        winprint.document.write("<tr align='center'>" + $('#ContentPlaceHolder1_gvAnnexureBellowDetails_p').find('tr')[j].innerHTML + "</tr>");
                        if (RowLength == j) {
                            break;
                        }
                    }
                    k = k + 25;
                    winprint.document.write("</tbody>");
                    winprint.document.write("</table>");
                    winprint.document.write("</div>");
                }
            }
            else {
                winprint.document.write("<div class='page_generateoffer'>");
                winprint.document.write(FirstpageContent);
                winprint.document.write("</div>");
            }

            winprint.document.write("</td></tr></tbody>");
            winprint.document.write("<tfoot><tr><td>");
            winprint.document.write("<div class='col-sm-12 text-center' style='height:20mm;'>");
            winprint.document.write("<img id='imgqrcode' class='Qrcode' src='" + QrCode + "' />");
            winprint.document.write("</div>");
            winprint.document.write("</td></tr></tfoot></table>");
            winprint.document.write("</body></html>");

            setTimeout(function () {
                winprint.document.close();
                winprint.focus();
                winprint.print();
                // winprint.close();
            }, 1000);
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
                                    <h3 class="page-title-head d-inline-block">RFP Approval</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Leads & Enquiry</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">RFP Approval</li>
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
                    <asp:PostBackTrigger ControlID="gvRFPHeader" />
                    <asp:PostBackTrigger ControlID="gvAttachments" />
                    <asp:PostBackTrigger ControlID="gvDrawingFiles" />
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
                                            <asp:ListItem Value="1">APPROVED</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divAdd" class="btnAddNew" visible="false" runat="server">
                            <div class="text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Customer Name</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="true" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlCustomerName_SelectIndexChanged"
                                            Width="70%" ToolTip="Select Customer Number">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Customer PO</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlCustomerPO" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlCustomerPO_SelectIndexChanged"
                                            CssClass="form-control" Width="70%" ToolTip="Select Customer PO">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divInput" class="divInput" visible="false" runat="server">
                            <div class="ip-div text-center">
                            </div>
                        </div>
                        <div id="divOutput" visible="false" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="col-md-12 row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                                    <asp:Label ID="lbltitle" runat="server" Text="RFP Approval Details"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 text-center">
                                            <asp:Button ID="btnApprove" CssClass="btn btn-cons btn-save" runat="server" Text="Approve"
                                                OnClick="btnApprove_Click"></asp:Button>
                                            <asp:Button ID="btnReplyToSales" CssClass="btn btn-cons btn-save" runat="server"
                                                OnClientClick="return ShowreplyPopUp(this);" Text="Reply To Sales"></asp:Button>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvRFPHeader" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" OnRowCommand="gvRFPHeader_OnRowCommand" CssClass="table table-hover table-bordered medium"
                                                OnRowDataBound="gvRFPHeader_OnRowDataBound" DataKeyNames="RFPHID,POHID,EnquiryNumber,PoCopy,PoCopyWithoutPrice,RFPNo">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" ControlStyle-CssClass="nosort" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-CssClass="text-center">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkall" runat="server" onclick="return checkAll(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkitems" runat="server" AutoPostBack="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="RFP Number" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="200px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRFPnumber" runat="server" Text='<%# Eval("RFPNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="RFP Status" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="200px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRFPStatus" runat="server" Text='<%# Eval("RFPApprovalStatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Location Name" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLocationName" runat="server" Text='<%# Eval("Location")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Project Name" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProjectName" runat="server" Text='<%# Eval("ProjectName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delivery Date" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDeliveryDate" runat="server" Text='<%# Eval("DeliveryDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnView" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="ViewRFP"><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="RFP PDF" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnRFPPDf" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="ViewRFPPDF"><img src="../Assets/images/pdf.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PO Copy" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnPoCopy" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="ViewPOCopy"><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Po Copy Without Price" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnPoCopyWithoutprice" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="ViewPoCopyWithoutPrice"><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnRFPHID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnPdfContent" runat="server" Value="0" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12" id="divPrint" runat="server" style="display: none;">
                        <div style="">
                            <%--   <div class="col-sm-12 text-center p-b-30" style="padding-top: 10px; font-size: larger; font-weight: bold; font-family: Times New Roman; color: #000 !important;">
                                REQUEST FOR PRODUCTION
                            </div>--%>
                            <div class="row p-t-10">
                                <div class="col-sm-3 text-left">
                                    <label>
                                        RFP No & Date</label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblRFPNo_P"></asp:Label>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-3 text-left">
                                    <label>
                                        Customer Name</label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblCustomerName_p"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 text-left">
                                    <label>
                                        Purchase Order No & Date</label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblCustomerOrderNo_p"></asp:Label>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-3 text-left">
                                    <label>
                                        Project</label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblProject_p"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 text-left">
                                    <label>
                                        Number Of Items</label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblNumberOfItems_p"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 text-left">
                                    <label>
                                        Location Name</label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblLocationName_p"></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12 p-t-10">
                                <asp:GridView ID="gvItemDetails_p" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                    EmptyDataText="No Records Found" CssClass="table table-hover medium">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSno" runat="server" Text='<%# Eval("SNo")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Code/Tag No/MTL Code" HeaderStyle-CssClass="text-center"
                                            HeaderStyle-Width="10%" ItemStyle-Width="10%" ItemStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblitemname" runat="server" Text='<%# Eval("TagNo")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Size (mm)" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"
                                            HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemSize" runat="server" Text='<%# Eval("ItemSize")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Name" HeaderStyle-Width="15%" ItemStyle-Width="15%"
                                            HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblitemname" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DRG No." HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"
                                            HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDrawingNumber" runat="server" Text='<%# Eval("DesignNumber")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item SL.No" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"
                                            HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemSno" runat="server" Text='<%# Eval("ItemSno")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QTY (Nos)" HeaderStyle-CssClass="text-center" HeaderStyle-Width="5%"
                                            ItemStyle-Width="5%" ItemStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblquantity" class="lblqty" runat="server" Text='<%# Eval("Quantity")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="row m-t-10">
                                <div class="col-sm-3 text-left">
                                    <label>
                                        QAP Ref</label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblQAPRefNo_p"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 text-left">
                                    <label>
                                        QAP Approval</label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblQAPApproval_p"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 text-left">
                                    <label>
                                        Drawing Approval</label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblDrawingApproval_p"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <label>
                                        Due Date For Dispatch</label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblDueDateForDispatch_p"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 text-left">
                                    <label>
                                        Inspection
                                    </label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblInspectionRequirtment_p"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 text-left">
                                    <label>
                                        LD Clause</label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblLDClause_p"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 text-left">
                                    <label>
                                        Despatch Details</label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblDespatchDetails_p"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 text-left">
                                    <label>
                                        Prepared By</label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblMarketing_p"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 text-left">
                                    <label>
                                        Checked By</label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblDesign_p"></asp:Label>
                                </div>
                            </div>
                            <%--  <div class="col-sm-12">
                                <div class="col-sm-5 text-left">
                                    <label>
                                        Data Entry</label>
                                </div>
                                <div class="col-sm-2">
                                    :
                                </div>
                                <div class="col-sm-5">
                                    <asp:Label runat="server" ID="lblDataEntry_p"></asp:Label>
                                </div>
                            </div>--%>
                            <div class="row">
                                <div class="col-sm-3 text-left">
                                    <label>
                                        Approved By</label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblApprovedBy_p"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 text-left">
                                    <label>
                                        Project Incharge</label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblProjectIncharge_p"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 text-left">
                                    <label>
                                        Notes Summary</label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblNotesSummary_p"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <label style="text-align: center; width: 100%; font-size: 20px !important; font-weight: 700;">
                                    Bellow Details</label>
                            </div>
                            <div class="col-sm-12 p-t-10">
                                <asp:GridView ID="gvBellowDetails_p" runat="server" AutoGenerateColumns="true" ShowHeaderWhenEmpty="True"
                                    OnRowDataBound="gvBellowDetails_p_RowDataBound" EmptyDataText="No Records Found"
                                    CssClass="table table-hover medium" HeaderStyle-HorizontalAlign="Center">
                                    <Columns>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12" id="divAnnexureItemList_p" runat="server" style="display: none; margin-top: 10px; margin-bottom: 10px">
                        <asp:GridView ID="gvAnnexureItemList_p" runat="server" AutoGenerateColumns="False"
                            ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover medium">
                            <Columns>
                                <asp:TemplateField HeaderText="SL.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                    HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSno" runat="server" Text='<%# Eval("SNo")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Code/Tag No/MTL Code" HeaderStyle-CssClass="text-center"
                                    HeaderStyle-Width="10%" ItemStyle-Width="10%" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblitemname" runat="server" Text='<%# Eval("TagNo")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size (mm)" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"
                                    HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemSize" runat="server" Text='<%# Eval("ItemSize")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Name" HeaderStyle-Width="15%" ItemStyle-Width="15%"
                                    HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblitemname" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DRG No." HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"
                                    HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDrawingNumber" runat="server" Text='<%# Eval("DesignNumber")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item SL.No" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"
                                    HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemSno" runat="server" Text='<%# Eval("ItemSno")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="QTY (Nos)" HeaderStyle-CssClass="text-center" HeaderStyle-Width="5%"
                                    ItemStyle-Width="5%" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblquantity" class="lblqty" runat="server" Text='<%# Eval("Quantity")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-sm-12" style="width: 100%; display: none;" runat="server" id="divAnnexureBellowList_p">
                        <asp:GridView ID="gvAnnexureBellowDetails_p" runat="server" AutoGenerateColumns="true"
                            OnRowDataBound="gvAnnexureBellowDetails_p_RowDataBound" ShowHeaderWhenEmpty="True"
                            EmptyDataText="No Records Found" CssClass="table table-hover medium" HeaderStyle-HorizontalAlign="Center">
                            <Columns>
                            </Columns>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="modal" id="mpeView" style="overflow-y: scroll;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <asp:UpdatePanel ID="upView" runat="server">
                        <Triggers>
                        </Triggers>
                        <ContentTemplate>
                            <div class="modal-header">
                                <h4 class="modal-title ADD">RFP Details</h4>
                                <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                    ×</button>
                            </div>
                            <div class="modal-body" style="padding: 0px;">
                                <div id="docdiv" class="docdiv">
                                    <div class="inner-container">
                                        <div id="enquirydetailsdiv" class="enquirydetailsdiv" runat="server">
                                            <div class="col-sm-12">
                                                <div class="col-sm-4">
                                                    <div>
                                                        <label class="form-label">
                                                            QAP Ref No</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lblQAPRefNo_V" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="form-label">
                                                            QAP Approval</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lblQAPApproval_V" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="form-label">
                                                            Drawing Approval Name</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lblDrawingApprova_V" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="form-label">
                                                            Inspection requirtment</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lblInspectionRequirtment_V" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="form-label">
                                                            LD Clause</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lblLDClause_V" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="form-label">
                                                            Despatch Details</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lblDespatchDetails_V" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="form-label">
                                                            Notes Summary</label>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lblNotesSummary_V" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                </div>
                                                <div class="col-sm-4">
                                                </div>
                                            </div>
                                        </div>
                                        <%--  <ul class="nav nav-tabs lserptabs" style="display: inline-block; width: 100%; background-color: cadetblue;
                                            text-align: right; font-size: x-large; font-weight: bold; color: whitesmoke;">
                                            <li>Documents</li></ul>
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
                                        </div>--%>
                                    </div>
                                    <div class="col-sm-12" style="text-align: center; font-size: 20px;">
                                        <label>
                                            View Drawimg Files</label>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <asp:GridView ID="gvDrawingFiles" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                            OnRowCommand="gvDrawingFiles_OnRowCommand" DataKeyNames="FileName,EnquiryID">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Drawing Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDrawingName" runat="server" Text='<%# Eval("DrawingName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Drawing Path" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnView" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            CommandName="ViewDrawings"><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="modal-footer">
                                        <div class="col-sm-12 p-t-10">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <asp:GridView ID="gvAttachments" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                            OnRowCommand="gvAttachments_OnRowCommand" DataKeyNames="AttachementID">
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
                                                        <asp:LinkButton ID="lbtnView" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            CommandName="ViewDocs"><img src="../Assets/images/view.png" /></asp:LinkButton>
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
                                    <asp:LinkButton Text="Download" CssClass="btn btn-save btn-cons AlignTop" ID="btndownloaddocs"
                                        OnClick="btndownloaddocs_Click" runat="server" />
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
    <div class="modal" id="mpeReplyPopUp">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H1">
                                <div class="text-center">
                                    Reply Message
                                    <asp:Label ID="Label2" Text="" runat="server"></asp:Label>
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal">
                                x</button>
                        </div>
                        <div class="modal-body">
                            <div class="inner-container" id="messageDiv">
                                <div class="col-sm-12">
                                    <div class="col-sm-4">
                                        <label class="form-label mandatorylbl">
                                            Header
                                        </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtHeader_R" runat="server" CssClass="form-control mandatoryfield"
                                            TextMode="MultiLine" Rows="1"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4">
                                        <label class="form-label mandatorylbl">
                                            Send Message
                                        </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtMessage_R" runat="server" CssClass="form-control mandatoryfield"
                                            TextMode="MultiLine" Rows="3"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="col-sm-12">
                                <div class="col-sm-4">
                                </div>
                                <div class="col-sm-4">
                                    <asp:LinkButton ID="btnSendmailR" runat="server" Text="Send Mail" OnClientClick="return Mandatorycheck('messageDiv');"
                                        OnClick="btnSendmailReply_Click" CssClass="btn btn-cons btn-save  AlignTop" />
                                </div>
                                <div class="col-sm-4">
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
