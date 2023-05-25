<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" 
    CodeFile="MaterialInwardQCClearenceList.aspx.cs"  ClientIDMode="Predictable" ValidateRequest="false"
    Inherits="Pages_MaterialInwardQCClearenceList" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function MRNPrint(epstyleurl, Main, QrCode, style, print, topstrip) {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var MrnContent = $('#ContentPlaceHolder1_divMRNPrint').html();

            winprint.document.write("<html><head><title>");
            winprint.document.write("Offer");
            winprint.document.write("</title>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='" + epstyleurl + "'/>");
            winprint.document.write("<link rel='stylesheet' href='" + Main + "' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet'  href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/print.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");

            winprint.document.write("<style type='text/css'/>  .Qrcode{ float: right; } label{ color: black ! important;font-weight: bold;} p{ margin:0;padding:0; }  @page { size: landscape; } span { padding: 0px 0px; } .ISO tr td { border:1px solid black; }  </style>");

            winprint.document.write("<style type='text/css'/> @media print  { .LandScapeprint{ width:277mm; margin-left:10mm;margin-right:10mm; height:190mm;margin-top:10mm;margin-bottom:10mm;} } </style>");

            winprint.document.write("</head><body>");
            winprint.document.write("<div style='width:297mm;height:210mm'>");
            winprint.document.write("<div class='LandScapeprint' style='border:2px solid black;'>");
            winprint.document.write("<div class='col-sm-12' style='padding-top:10px;margin:0 auto;padding-left:10px;padding-right:10px;'>");
            winprint.document.write("<div>");
            winprint.document.write(MrnContent);
            winprint.document.write("</div>");

            winprint.document.write("<div>");

            winprint.document.write("</div>");

            winprint.document.write("</div>");
            winprint.document.write("</div>");
            winprint.document.write("</div>");
            winprint.document.write("</body></html>");

            setTimeout(function () {
                winprint.document.close();
                winprint.focus();
                winprint.print();
            }, 1000);
        }

        function PrintMRN(MIDID) {
            OpenMRNPrint(MIDID);
            return false;
        }

        function OpenMRNPrint(MIDID) {
            var str = window.location.href.split('/');
            var replacevalue = str[str.length - 1];
            window.open(window.location.href.replace(replacevalue, 'MRNPrintDetails.aspx?MIDID=' + MIDID + ''), '_blank');
        }

    </script>
    <style type="text/css">
        table tr td .chosen-container {
            width: 138px !important;
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
                                    <h3 class="page-title-head d-inline-block">Material Inward QC Clerance Reports</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="AdminHome.aspx">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Stores</a></li>
                                <li class="active breadcrumb-item" aria-current="page">Material Inward QC Clearance Reports</li>
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
                                        <asp:RadioButtonList ID="rblMRNChange" runat="server" CssClass="radio radio-success" RepeatDirection="Horizontal"
                                            AutoPostBack="true" OnSelectedIndexChanged="rblMRNChange_OnSelectedChanged" RepeatLayout="Flow">
                                            <asp:ListItem Selected="True" Value="0">PENDING</asp:ListItem>
                                            <asp:ListItem Value="1">COMPLETED</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divAdd" runat="server">
                            <div class="ip-div text-center">
                            </div>
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
                                            <asp:GridView ID="gvaddeditems" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium tablestatesave"
                                                OnRowCommand="gvaddeditems_OnRowCommand" OnRowDataBound="gvaddeditems_OnRowDataBound" DataKeyNames="SPODID,MIDID,MIQCStatus">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="MRN" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnMRN" runat="server"
                                                                OnClientClick='<%# string.Format("return PrintMRN({0});",Eval("MIDID")) %>'>
                                                            <img src="../Assets/images/pdf.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Location">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("Location")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="TC Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCFStatus" runat="server" Text='<%# Eval("CFStatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="QC Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMIQCStatus" runat="server" Text='<%# Eval("MIQCStatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="PO No" ItemStyle-HorizontalAlign="left"
                                                        HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsponumber" runat="server" Text='<%# Eval("SPONumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MRN No" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMRNNumber" runat="server" Text='<%# Eval("MRNNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("CategoryName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Grade" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGradeName" runat="server" Text='<%# Eval("GradeName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="THK" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMaterialThickness" runat="server" Text='<%# Eval("THKValue")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Measurement" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMeasurement" runat="server" Text='<%# Eval("Measurment").ToString().Replace(",", "<br/>")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MTL Type" ItemStyle-HorizontalAlign="left"
                                                        HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTypeName" runat="server" Text='<%# Eval("TypeName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DC QTY" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDCQuantity" runat="server" CssClass="lbldcqty" Text='<%# Eval("DCQuantity")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnMIDID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnMIQCDID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnSPODID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnMRNQuantity" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnReworkedQty" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnPdfContent" runat="server" />

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

    <div style="display: none;" runat="server">
        <div id="divMRNPrint" runat="server">

            <div class="col-sm-12 text-center">
                <h3 style='font-weight: 600; font-size: 24px; font-style: italic; color: #000; font-family: Arial; display: contents;'>LONESTAR </h3>
                <span style='font-weight: 600; font-size: 24px ! important; font-family: Times New Roman;'>INDUSTRIES</span>
            </div>
            <div class="col-sm-12 p-t-10" style="text-align: center; font-size: large; color: black; font-weight: bold;">
                Material Receipt Note Cum Receiving Inspection Report
            </div>
            <div class="col-sm-12">
                <div class="col-sm-2">
                    <img src='../Assets/images/lonestar.jpeg' alt='lonestar-image' width='90px'>
                </div>
                <div class="col-sm-4">
                    <div class="col-sm-12 text-left">
                        <label>OFFICE:</label>
                    </div>
                    <div class="col-sm-12">
                    </p>--%>
                        <asp:Label ID="lblOfficeAddress_p" Style="font-weight: bold;" runat="server"></asp:Label>
                        <asp:Label ID="lblOfficePhoneAndFaxNo_p" Style="font-weight: bold;" runat="server"></asp:Label>
                        <asp:Label ID="lblOfficeEmail_p" Style="font-weight: bold;" runat="server"></asp:Label>
                        <asp:Label ID="lblOfficeWebsite_p" Style="font-weight: bold;" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-4">
                    <div class="col-sm-12 text-left">
                        <label>WORKS:</label>
                    </div>
                    <div class="col-sm-12">
                        <asp:Label ID="lblWorkAddress_p" Style="font-weight: bold;" runat="server"></asp:Label>
                        <asp:Label ID="lblWorkPhoneAndFaxNo_p" Style="font-weight: bold;" runat="server"></asp:Label>
                        <asp:Label ID="lblWorkEmail_p" Style="font-weight: bold;" runat="server"></asp:Label>
                        <asp:Label ID="lblWorkWebsite_p" Style="font-weight: bold;" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-2" style="padding-left: 0px;">
                    <asp:Label ID="lblMRNNo_p" Style="font-weight: bold;" runat="server"></asp:Label>
                    <asp:Label ID="lblDate_p" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-sm-12 text-center">
            <div class="col-sm-12 p-t-10">
                <div class="col-sm-6">
                    <label>Supplier:</label>
                    <asp:Label ID="lblSupplierName_p" runat="server"></asp:Label>
                </div>
                <div class="col-sm-6">
                    <label>Visual:</label>
                    <asp:Label ID="lblVisual_p" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-sm-12 p-t-10">
                <div class="col-sm-6">
                    <label>SupplierDC/InvoiceNo./Date: </label>
                    <asp:Label ID="lblInVoiceNo_p" runat="server"></asp:Label>
                </div>
                <div class="col-sm-6">
                    <label>Check Test</label>
                    <asp:Label ID="lblCheckTest_p" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-sm-12 p-t-10">
                <div class="col-sm-6">
                    <label>PO No And Date:</label>
                    <asp:Label ID="lblPONoDate_p" runat="server"></asp:Label>
                </div>
                <div class="col-sm-6">
                    <label>Addt Test Requirtment</label>
                    <asp:Label ID="lblAddtionalTestRequirtment_p" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-sm-12 p-t-10">
                <div class="col-sm-6">
                    <label>MTR:</label>
                    <asp:Label ID="lblMTR_p" runat="server"></asp:Label>
                </div>
                <div class="col-sm-6">
                    <label>Measured Dimension:</label>
                    <asp:Label ID="lblMeasuredDimension_p" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-sm-12 p-t-10">
                <div class="col-sm-6">
                    <label>TPS No:</label>
                    <asp:Label ID="lblTPSNo_p" runat="server"></asp:Label>
                </div>
                <div class="col-sm-6">
                    <label>Original / Identification Marking </label>
                    <asp:Label ID="lblOriginalMarking_p" runat="server"></asp:Label>
                </div>
            </div>

            <div class="col-sm-12 p-t-10">
                <div class="col-sm-6">
                    <label>PMI :</label>
                    <asp:Label ID="lblPMI_p" runat="server"></asp:Label>
                </div>
                <div class="col-sm-6">
                    <label>Certificate No </label>
                    <asp:Label ID="lblCertificateNo_p" runat="server"></asp:Label>
                </div>
            </div>

            <div class="col-sm-12 p-t-10">
                <div class="col-sm-6">
                </div>
                <div class="col-sm-6" style="margin-left: 50%;">
                    <div class="col-sm-12">
                        <label>Used Instrument Calibration Details:</label>
                    </div>
                    <div class="col-sm-12">
                        <asp:GridView ID="gvInstrumentDetails_p" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                            <Columns>
                                <asp:TemplateField HeaderText="Instrument Ref No" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("InstrumentRefNo")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cal Report No" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDCQuantity" runat="server" Text='<%# Eval("CalreportNo")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Done On" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaterialType" runat="server" Text='<%# Eval("DoneOn")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Due On" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMeasurment" runat="server" Text='<%# Eval("DueOn")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <div class="col-sm-12 p-t-10">
                <asp:GridView ID="gvQCClearedDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                    EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                            HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DC Qty" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblDCQuantity" runat="server" Text='<%# Eval("DCQuantity")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Material Type" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblMaterialType" runat="server" Text='<%# Eval("TypeName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblMeasurment" runat="server" Text='<%# Eval("Measurment")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Accepted" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblQtyAccepted" runat="server" Text='<%# Eval("AcceptedQuantity")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Reworked" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblQtyReworked" runat="server" Text='<%# Eval("ReWorkedQuantity")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Rejected" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblQtyrejected" runat="server" Text='<%# Eval("RejectedQuantity")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <div class="col-sm-12">
                <div class="col-sm-6">
                    <div class="col-sm-12 text-left">
                        <label>Stores In Charge: </label>
                    </div>
                    <div class="col-sm-12 text-left">
                        <label>Received By:</label>
                        <asp:Label ID="lblReceivedBy_p" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-12 text-left">
                        <label>Date:</label>
                        <asp:Label ID="lblReceivedDate_p" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-12 text-left">
                        <label>Signature: </label>
                    </div>
                </div>
                <div class="col-sm-4">

                    <div class="col-sm-12 text-left">
                        <label>Quality In Charge: </label>
                    </div>
                    <div class="col-sm-12 text-left">
                        <label>Inspected By:</label>
                        <asp:Label ID="lblInspectedBy_p" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-12 text-left">
                        <label>Date:</label>
                        <asp:Label ID="lblInspectedDate_p" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-12 text-left">
                        <label>Signature: </label>
                    </div>
                </div>
                <div class="col-sm-2">
                    <asp:Image ID="imgQrcode" ImageUrl="" CssClass="Qrcode" runat="server" />
                </div>
            </div>

            <div class="col-sm-12 p-t-20">
                <table style="width: 50%; margin-left: 25%; height: 5%;" class="ISO">
                    <tr style="vertical-align: middle; text-align: center;">
                        <td>
                            <label>
                                Doc . No</label>
                        </td>
                        <td>
                            <asp:Label ID="lblDocNo_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label>
                                Rev. No
                            </label>
                        </td>
                        <td>
                            <asp:Label ID="lblRevNo_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label>
                                Date
                            </label>
                        </td>
                        <td>
                            <asp:Label ID="lblISODate_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </div>

</asp:Content>

