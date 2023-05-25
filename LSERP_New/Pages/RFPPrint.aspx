<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="RFPPrint.aspx.cs" Inherits="Pages_RFPPrint" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function RFPPrint(epstyleurl, Main, QrCode) {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var RowLength = $('#ContentPlaceHolder1_gvItemDetails_p').find('tr').length - 1;
            var BellowRowLength = $('#ContentPlaceHolder1_gvBellowDetails_p').find('tr').length - 1;

            var divcontent1 = $('#divcontent1_p').html();
            var divcontent2 = $('#divcontent2_p').html();

            winprint.document.write("<html><head><title>");
            winprint.document.write("Offer");
            winprint.document.write("</title>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='" + epstyleurl + "'/>");
            winprint.document.write("<link rel='stylesheet' href='" + Main + "' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet'  href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/printg.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<style type='text/css'>@media print,screen { label,table th{ font-weight: bold; font-size: 15px !important;font-family:Times New Roman;color:#000 !important; }}.Qrcode{margin-top: 0px !important; margin-bottom: 10px;} .row{ padding-top:10px; } .page_generateoffer{ margin: 6mm; } table th{ vertical-align: middle; } .table thead th {background: none !important; border-bottom: 1px solid #000 !important} </style>");

            winprint.document.write("<div class='print-page' style='position:fixed;width:200mm;height:287mm;margin-top:5mm;margin-bottom:5mm;margin-left:5mm;margin-right:5mm'></div>");
            winprint.document.write("<table style='width:200mm;height:287mm;margin-left:20px;border:0px !important'><thead><tr><td>");
            winprint.document.write("<div style = 'height: 54px;'>");
            winprint.document.write("<div class='col-sm-12 text-center p-b-30' style='padding-top: 10px; font-size: larger; font-weight: bold; font-family: Times New Roman; color: #000 !important;padding-top: 30px;float: left; position: fixed;'>");
            winprint.document.write("REQUEST FOR PRODUCTION");
            winprint.document.write("</div>");
            winprint.document.write("</div>");
            winprint.document.write("</td></tr></thead>");
            winprint.document.write("<tbody><tr><td>");

            winprint.document.write(divcontent1);

            winprint.document.write("<div class='col-sm-12 p-t-10'>");
            winprint.document.write("<table class='table table-hover medium' cellspacing='0' rules='all' border='1' id='gvitemList_p' style='border-collapse:collapse;'>");
            winprint.document.write("<thead>");
            winprint.document.write("<tr align='center'>" + $('#ContentPlaceHolder1_gvItemDetails_p').find('tr')[0].innerHTML + "</tr>");
            winprint.document.write("</thead>");
            winprint.document.write("<tbody>");

            for (var j = 1; j <= RowLength; j++) {
                winprint.document.write("<tr align='center'>" + $('#ContentPlaceHolder1_gvItemDetails_p').find('tr')[j].innerHTML + "</tr>");
            }

            winprint.document.write("</tbody>");
            winprint.document.write("</table>");
            winprint.document.write("</div>");

            winprint.document.write(divcontent2);

            winprint.document.write("<div class='col-sm-12 p-t-10'>");
            winprint.document.write("<table class='table table-hover medium' cellspacing='0' rules='all' border='1' id='gvbellowList_p' style='border-collapse:collapse;'>");
            winprint.document.write("<thead>");
            winprint.document.write("<tr align='center'>" + $('#ContentPlaceHolder1_gvBellowDetails_p').find('tr')[0].innerHTML + "</tr>");
            winprint.document.write("</thead>");
            winprint.document.write("<tbody>");

            for (var j = 1; j <= BellowRowLength; j++) {
                winprint.document.write("<tr align='center'>" + $('#ContentPlaceHolder1_gvBellowDetails_p').find('tr')[j].innerHTML + "</tr>");
            }

            winprint.document.write("</tbody>");
            winprint.document.write("</table>");
            winprint.document.write("</div>");

            winprint.document.write("</td></tr></tbody>");
            winprint.document.write("<tfoot><tr><td>");
            winprint.document.write("<div style='height:20mm;'>");
            winprint.document.write("<div class='col-sm-12 text-center' style='position: fixed;bottom:0'>");
            winprint.document.write("<img id='imgqrcode' class='Qrcode' src='" + QrCode + "' />");
            winprint.document.write("</div>");
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
        label {
            font-weight: bold;
            font-size: 15px !important;
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
                                        <img src="../Assets/images/pages.png" alt="" width="16px" />
                                    </span>
                                    <h3 class="page-title-head d-inline-block">RFP Print Sheet</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Sale</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">RFP Print Sheet</li>
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
                        <div id="divAdd" class="btnAddNew" runat="server">
                            <div class="text-center">
                            </div>
                        </div>
                        <div id="divInput" class="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12" id="divPrint" runat="server">
                                    <div style="">
                                        <div id="divcontent1_p">
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

                                        </div>

                                        <div class="col -sm-12 p-t-10">
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

                                        <div id="divcontent2_p">

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
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <asp:HiddenField ID="hdnRFPHID" Value="0" runat="server" />
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


