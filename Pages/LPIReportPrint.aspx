<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="LPIReportPrint.aspx.cs"
    Inherits="Pages_LPIReportPrint" %>

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

        function Print() {
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
            winprint.document.write("<style type='text/css'> .divweldingdetails th { border-right: 1px solid;} .divweldingdetails table td{ border:1px solid;padding: 5px;vertical-align: middle; }  .divtable table td{ border:1px solid;padding: 5px;vertical-align: middle; } label { font-weight: bold ! important;color: #000 ! important; } .contractorborder label{padding: 4px 6px; } .contractorborder { padding-top:5px; border: 1px solid;} .sideheading{ display:flex;align-items:flex-start;justify-content:center;margin-top:15px; } .lbl_h{ width:35%; } .lbl_v{ width:65%; } .Qrcode {height: 60px;width: 80px;margin-right: 10px;padding-left: 20px;}</style>");

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
                                    <h3 class="page-title-head d-inline-block">LPI Report</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Quality</a></li>
                                <li class="active breadcrumb-item" aria-current="page"> LPI </li>
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
                                                <label style="color: Black; font-weight: bolder; font-size: large ! important;">LIQUID PENETRANT INSPECTION REPORT </label>
                                            </div>
                                            <div class="divtable p-t-10">
                                                <table style="width: 100%; border-collapse: collapse;">
                                                    <tr>
                                                        <td style="width: 15%;" rowspan="2">
                                                            <label>Customer  </label>
                                                        </td>
                                                        <td style="width: 30%;" rowspan="2">
                                                            <asp:Label ID="lblcustomername" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="width: 15%;">
                                                            <label>Report No  </label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblReportNo" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>Report Date  </label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblreportDate" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>RFP No </td>
                                                        <td>
                                                            <asp:Label ID="lblRFPNo" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <label>PO No  </label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblPONo" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>Size  </label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblSize" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <label>Item Name</label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblItemName" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>Drawing No  </label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblDrawingNo" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <label>Project Name  </label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblprojectname" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>QAP No  </label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblQAPNo" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <label>Stage Of Test  </label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblstageoftest" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>BSL No  </label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblBSLNo" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <label>Test Date  </label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbltestdate" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>Material Specification  </label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblmaterialspecification" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <label>Penetrant  </label>
                                                        </td>
                                                        <td>
                                                            <label>Brand </label>
                                                            <asp:Label ID="lblpenetrantbrandNo" runat="server"></asp:Label>
                                                            <label>Batch No </label>
                                                            <asp:Label ID="lblpenetrantbatchno" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>thickness  </label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblthickness" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <label>Cleaner   </label>
                                                        </td>
                                                        <td>
                                                            <label>Brand </label>
                                                            <asp:Label ID="lblcleanerbrandNo" runat="server"></asp:Label>
                                                            <label>Batch No </label>
                                                            <asp:Label ID="lblcleanerbatchNo" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>Procedure Rev No  </label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblprocedurerevNo" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <label>Dwell Time   </label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblDwelltime" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>Surface Condition </label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblsurfacecondition" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <label>Developer   </label>
                                                        </td>
                                                        <td>
                                                            <label>Brand </label>
                                                            <asp:Label ID="lbldeveloperbrandNo" runat="server"></asp:Label>
                                                            <label>Batch No </label>
                                                            <asp:Label ID="lbldeveloperbatchNo" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>Surface Temp </label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblsurfacetemp" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <label>Developement Time   </label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbldevelopementtime" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>Penetrant System  </label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblpenetrantsystem" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <label>Lighting Equipement   </label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbllightingequipement" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>Technique  </label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbltechnique" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <label>light Intensity   </label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lbllightintensity" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="col-sm-12 p-t-10 divweldingdetails">
                                                <table style="width: 100%; border-collapse: collapse;">
                                                    <tr>
                                                        <th>Joint Identification</th>
                                                        <th>Indication Type</th>
                                                        <th>Indication Size</th>
                                                        <th>Interpretation</th>
                                                        <th>Disposition</th>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lbljointidentification" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblindicationtype" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblindicationsize" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblinterpretation" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblDisposition" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>Sketch Of Indications </label>
                                                        </td>
                                                        <td colspan="4">
                                                            <asp:Label ID="lblSketchofindications" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            <label>Inspection QTY  </label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblinspectionQTY" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <label>Accepted QTY </label>
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:Label ID="lblacceptedQTY" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <label>For Lone Star Industries </label>
                                                        </td>
                                                        <td colspan="3">
                                                            <label>For Third Party </label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>Name  </label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblLSIInspectorName" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <label>Name </label>
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:Label ID="lblThirdPartyInspectorName" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>Date  </label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblLSIInspectionDate" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <label>Date </label>
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:Label ID="lblthirdPartyInspectionDate" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>Level  </label>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblLSIInspectionLevel" runat="server"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <label>Level </label>
                                                        </td>
                                                        <td colspan="2">
                                                            <asp:Label ID="lblThirdPartyInspectionLevel" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                     
                                        <div id="divbelowcontent_p" runat="server">
                                        </div>
                                        <div id="divfootercontent_p">
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


