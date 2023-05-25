<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="RawMaterialInspectionReportPrint.aspx.cs"
    Inherits="Pages_RawMaterialInspectionReportPrint" ClientIDMode="Predictable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
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
    </style>

    <script type="text/javascript">

        function PrintRawMIRReport() {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var RecordLength = $('#ContentPlaceHolder1_gvRawMIRDetails_p').find('tr').length;

            var Address = $('#ContentPlaceHolder1_hdnAddress').val();
            var PhoneAndFaxNo = $('#ContentPlaceHolder1_hdnPhoneAndFaxNo').val();
            var Email = $('#ContentPlaceHolder1_hdnEmail').val();
            var WebSite = $('#ContentPlaceHolder1_hdnWebsite').val();

            var MainContent = $('#ContentPlaceHolder1_divmainContent_p').html();
            var FooterContent = $('#divfootercontent_p').html();

            winprint.document.write("<html><head><title>");
            winprint.document.write("Offer");
            winprint.document.write("</title>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/printg.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<style type='text/css'> .divtable table td{ border:1px solid;padding: 10px;vertical-align: middle; } label { font-weight: bold ! important;color: #000 ! important; } .contractorborder label{padding: 4px 6px; } .contractorborder { padding-top:5px; border: 1px solid;} .sideheading{ display:flex;align-items:flex-start;justify-content:center;margin-top:15px; } .lbl_h{ width:35%; } .lbl_v{ width:65%; } .Qrcode {height: 60px;width: 80px;margin-right: 10px;padding-left: 20px;}</style>");

            winprint.document.write("<div class='print-page jobcardprintoutermargin'></div>");
            winprint.document.write("<table class='jobcardprinttablemargin'><thead><tr><td>");
            winprint.document.write("<div class='col-sm-12 jobcardprinttheadheight' style='text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;'>");
            winprint.document.write("<div>");
            winprint.document.write("<div>");
            winprint.document.write("<div class='col-sm-12 padding0' id='divLSERPLogo' style='margin:20px auto;display:block;'>");
            winprint.document.write("<div class='row jobcardprinttheadwidth'>");
            winprint.document.write("<div class='col-sm-3'>");
            winprint.document.write("<img src='../Assets/images/lonestar.jpeg' alt='lonestar-image' width='90px' style='margin:5px 0;'>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-6 text-center'>");
            winprint.document.write("<h3 style='font-weight:600;font-size:24px;font-style:italic;color:#000;font-family: Arial;display: contents;'>LONESTAR</h3>");
            winprint.document.write("<span style='font-weight:600;font-size:24px ! important;font-family: Times New Roman;'>INDUSTRIES</span>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;width: 100%;padding:0 0px'>" + Address + "</p>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + PhoneAndFaxNo + "</p>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + Email + "</p>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + WebSite + "</p>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-3'>");
            winprint.document.write("<img src='../Assets/images/iso.jpeg' alt='lonestar-image' height='90px' style='margin:5px 0;'>");
            winprint.document.write("</div></div>");
            winprint.document.write("</div></div></div>");
            winprint.document.write("</td></tr></thead>");
            winprint.document.write("<tbody><tr><td>");

            winprint.document.write("<div style='margin:0 5mm;'>");
            winprint.document.write(MainContent);

            winprint.document.write("<div class='p-t-10'>");
            winprint.document.write("<table class='table table-hover medium' cellspacing='0' rules='all' border='1' id='ContentPlaceHolder1_gvRawMIRDetails_p' style='border-collapse:collapse;'>");
            winprint.document.write("<tbody>");

            for (var j = 0; j < RecordLength; j++) {
                winprint.document.write("<tr align='center'>" + $('#ContentPlaceHolder1_gvRawMIRDetails_p').find('tr')[j].innerHTML + "</tr>");
            }

            winprint.document.write("</tbody>");
            winprint.document.write("</table>");
            winprint.document.write("</div>");

            winprint.document.write("<div class='p-t-10'>");
            winprint.document.write("<label> Remarks: </label>" + $('#ContentPlaceHolder1_lblRemarks_p').text());
            winprint.document.write("</div>");

            winprint.document.write("</td></tr></tbody>");
            winprint.document.write("<tfoot><tr><td>");
            winprint.document.write("<div class='jobcardprinttfootheight'>");
            winprint.document.write("<div class='row jobcardprinttfootwidth'>");
            winprint.document.write(FooterContent);
            winprint.document.write("</div>");
            winprint.document.write("</div>");
            winprint.document.write("</td></tr></tfoot></table>");
            winprint.document.write("</body></html>");

            setTimeout(function () {
                winprint.document.close();
                winprint.focus();
                winprint.print();
            }, 1000);
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
                                    <h3 class="page-title-head d-inline-block">Raw Material Inspection Report</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Quality</a></li>
                                <li class="active breadcrumb-item" aria-current="page">Raw Material Inspection Report</li>
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
            <asp:UpdatePanel ID="upQuote" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnprint" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div class="col-sm-12 p-b-10 text-center">
                            <asp:LinkButton ID="btnprint" runat="server" CssClass="btn btn-cons btn-success" Text="GET PRINT" OnClick="btnprint_Click">
                            </asp:LinkButton>
                        </div>
                        <div id="div" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div id="divLPIReport_p" runat="server">
                                        <div id="divmainContent_p" runat="server">
                                            <div class="col-sm-12 p-t-10 text-center" style="background-color: #b0acac; padding-top: 10px; padding-bottom: 10px;">
                                                <label style="color: Black; font-weight: bolder; font-size: large ! important;">RAW MATRIAL INSPECTION  REPORT </label>
                                            </div>
                                            <div class="row p-t-10">
                                                <div class="col-sm-4 text-center">
                                                    <label>RFP No</label>
                                                    <asp:Label ID="lblRFPNo_p" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-4 text-center">
                                                    <label>Report No</label>
                                                    <asp:Label ID="lblReportNo_p" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-4 text-center">
                                                    <label>Date</label>
                                                    <asp:Label ID="lblDate_p" runat="server"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="divtable p-t-10">
                                                <table style="width: 100%; border-collapse: collapse;">
                                                    <tr>
                                                        <td style="width: 15%;">
                                                            <label>Customer  </label>
                                                        </td>
                                                        <td style="width: 30%;">
                                                            <asp:Label ID="lblcustomername_p" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 15%;">
                                                            <label>PO No  </label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblPONO_p" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>QAP No  </label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblQAPNo_p" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <label>Project Name  </label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblprojectname_p" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>Size  </label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblSize_p" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <label>Item Name</label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblItemName_p" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>Drawing No  </label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblDrawingNo_p" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <label>Bellow SL.No  </label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblBellowSno_p" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>QTY  </label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblQuantity_p" runat="server"></asp:Label>
                                                        </td>
                                                        <td></td>
                                                        <td></td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                        <div class="p-t-10">
                                            <div class="p-t-10" runat="server">
                                                <asp:GridView ID="gvRawMIRDetails_p" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    CssClass="table table-hover table-bordered medium" EmptyDataText="No Records Found">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'
                                                                    Style="text-align: center"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Part No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPartNo" runat="server" Text='<%# Eval("PartNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Part Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPartName" runat="server" Text='<%# Eval("PartName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Material Grade">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMaterialSpecification" runat="server" Text='<%# Eval("GradeName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="THK">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTHK" runat="server" Text='<%# Eval("THKValue")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MRN No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMRNNo" runat="server" Text='<%# Eval("MRNNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Material TC No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMaterialTestCertificateNoDate" runat="server" Text='<%# Eval("MaterialTCNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Heat/COIL/Plate No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblHeatCastPlateNo" runat="server" Text='<%# Eval("PlateNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div id="divbelowcontent_p" runat="server">
                                            <div>
                                                <asp:Label ID="lblRemarks_p" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div id="divfootercontent_p">
                                            <div class="p-l-20" style="width: 50%">
                                                <label>For Lonestar Industries</label>
                                            </div>
                                            <div class="p-r-20" style="text-align: right; width: 50%">
                                                <label>For Customer</label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hdnAddress" runat="server" Value="" />
                    <asp:HiddenField ID="hdnPhoneAndFaxNo" runat="server" Value="" />
                    <asp:HiddenField ID="hdnEmail" runat="server" Value="" />
                    <asp:HiddenField ID="hdnWebsite" runat="server" Value="" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>

