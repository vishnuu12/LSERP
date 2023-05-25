<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="CAPAReportPrint.aspx.cs" Inherits="Pages_CAPAReportPrint" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function PrintCAPAReport() {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var Address = $('#ContentPlaceHolder1_hdnAddress').val();
            var CompanyName = $('#ContentPlaceHolder1_hdnCompanyName').val();
            var PhoneAndFaxNo = $('#ContentPlaceHolder1_hdnPhoneAndFaxNo').val();
            var Email = $('#ContentPlaceHolder1_hdnEmail').val();
            var WebSite = $('#ContentPlaceHolder1_hdnWebsite').val();

            var DocNo = $('#ContentPlaceHolder1_hdnDocNo').val();
            var RevNo = $('#ContentPlaceHolder1_hdnRevNo').val();
            var RevDate = $('#ContentPlaceHolder1_hdnRevDate').val();

            var CAPAContent = $('#ContentPlaceHolder1_divCAPAReport_p').html();

            winprint.document.write("<html><head><title>");
            winprint.document.write("Offer");
            winprint.document.write("</title>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/printg.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<style type='text/css'> label { font-weight: bold ! important;color: #000 ! important; } .contractorborder label{padding: 4px 6px; } .contractorborder { padding-top:5px; border: 1px solid;} .sideheading{ display:flex;align-items:flex-start;justify-content:center;margin-top:15px; } .lbl_h{ width:35%; } .lbl_v{ width:65%; } .Qrcode {height: 60px;width: 80px;margin-right: 10px;padding-left: 20px;}</style>");

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
            winprint.document.write("<h3 style='font-weight:600;font-size:22px;color:#000;font-family: Arial;display: contents;'>" + CompanyName + "</span > </h3>");
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
            winprint.document.write(CAPAContent);

            winprint.document.write("</div>");

            winprint.document.write("</td></tr></tbody>");
            winprint.document.write("<tfoot><tr><td>");
            winprint.document.write("<div class='jobcardprinttfootheight'>");
            winprint.document.write("<div class='row jobcardprinttfootwidth'>");
            winprint.document.write("<div class='col-sm-2 text-center'>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-7 text-center'>");
            winprint.document.write("<label style='width:30%;color:black;float:left; font-weight:bolder;border: 1px solid black;'>Doc No : " + DocNo + "</label>");
            winprint.document.write("<label style='width:25%;color:black;float:left; font-weight:bolder;border: 1px solid black;'>Rev No : " + RevNo + "</label>");
            winprint.document.write("<label style='width:40%;color:black;float:left; font-weight:bolder;border: 1px solid black;'>Rev Date : " + RevDate + "</label>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-3 text-center'>");
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
                                    <h3 class="page-title-head d-inline-block">CAPA Report Print</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Production</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">CAPA</li>
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
                    <asp:PostBackTrigger ControlID="btnprint" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div class="col-sm-12 p-b-10 text-center">
                            <asp:LinkButton ID="btnprint" runat="server" CssClass="btn btn-cons btn-success" Text="GET CAPA PRINT" OnClick="btnprint_Click">
                            </asp:LinkButton>
                        </div>
                        <div id="divCAPAReport_p" class="p-t-10" runat="server" style="display: block;">
                            <div id="div13" class="FrontPagepopupcontent" runat="server">
                                <div class="col-sm-12 text-center" style="background-color: #b0acac; padding-top: 10px; padding-bottom: 10px;">
                                    <asp:Label Style="color: Black; font-weight: bolder; font-size: large ! important;" ID="lblheading_p"
                                        runat="server" Text="">
                                    </asp:Label>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <table class="table table-hover table-bordered medium" style="border-collapse: collapse;">

                                        <tr>
                                            <td>
                                                <label>NCR No </label>
                                            </td>
                                            <td>
                                                <label>: </label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblNCRNo_p" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <label>Date  </label>
                                            </td>
                                            <td>
                                                <label>: </label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblNCRdate_p" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Inspection Period </label>
                                            </td>
                                            <td>
                                                <label>: </label>
                                            </td>
                                            <td>
                                                <div style="display: flex; flex-direction: column">
                                                    <div style="display: flex; justify-content: space-between">
                                                        <label>From Date </label>
                                                        <asp:Label ID="lblinspectionfromdate_p" runat="server">  </asp:Label>
                                                    </div>
                                                    <div style="display: flex; justify-content: space-between">
                                                        <label>To Date </label>
                                                        <asp:Label ID="lblinspectionToDate_p" runat="server">  </asp:Label>
                                                    </div>
                                                </div>
                                            </td>
                                            <td>
                                                <label>Inspection Of Weld Length In Meter / QTY </label>
                                            </td>
                                            <td>
                                                <label>: </label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblinspectionofweldlengthinmeterqty_p" runat="server">  </asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Repeated Rework Location </label>
                                            </td>
                                            <td>
                                                <label>: </label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblrepeatedreworklocation_p" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <label>Weld Defects Found In Meter </label>
                                            </td>
                                            <td>
                                                <label>: </label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblwelddefectsfoundinmeter_p" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Data From </label>
                                            </td>
                                            <td>
                                                <label>: </label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lbldatafrom_p" runat="server"></asp:Label>
                                            </td>

                                            <td>
                                                <label>Repeated Reworks </label>
                                            </td>
                                            <td>
                                                <label>: </label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblrepeatedreworks_p" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>RFP No </label>
                                            </td>
                                            <td>
                                                <label>: </label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblRFPNo_p" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <label>Size </label>
                                            </td>
                                            <td>
                                                <label>: </label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSize_p" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Item Name </label>
                                            </td>
                                            <td>
                                                <label>: </label>
                                            </td>
                                            <td colspan="4">
                                                <asp:Label ID="lblitemName_p" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Job Sno </label>
                                            </td>
                                            <td>
                                                <label>: </label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblJobSno_p" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <label>NC Stage </label>
                                            </td>
                                            <td>
                                                <label>: </label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblNCSatge_p" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <table class="table table-hover table-bordered medium" style="border-collapse: collapse;">
                                        <tr>
                                            <td>
                                                <label>Description Of Non Conformity </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div>
                                                    <asp:Label ID="lbldescriptionofNonConformity_p"
                                                        Text="Bellow To Flange Attachement Weld Join Leak Found During Numatic Luke texting" runat="server">
                                                    </asp:Label>
                                                </div>
                                                <div class="row p-t-20">
                                                    <div class="col-sm-3">
                                                        <label>Date :    </label>
                                                        <asp:Label ID="lbldncdate_p" runat="server">  </asp:Label>
                                                    </div>
                                                    <div class="col-sm-7">
                                                        <label>Quality Engineer: </label>
                                                        <asp:Label ID="lbldncqcengineer_p" runat="server">  </asp:Label>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label>Signature : </label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Correction </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div>
                                                    <asp:Label ID="lblcorrection_p"
                                                        Text="Bellow To Flange Attachement Weld Join Leak Found During Numatic Luke texting" runat="server">
                                                    </asp:Label>
                                                </div>
                                                <div class="row p-t-20">
                                                    <div class="col-sm-3">
                                                        <label>Date :    </label>
                                                        <asp:Label ID="lblcorrectiondate_p" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-7">
                                                        <label>Quality Engineer: </label>
                                                        <asp:Label ID="lblcorrectionengineername_p" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label>Signature : </label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Root Cause </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div>
                                                    <asp:Label ID="lblrootcause_p"
                                                        Text="Bellow To Flange Attachement Weld Join Leak Found During Numatic Luke texting" runat="server">
                                                    </asp:Label>
                                                </div>
                                                <div class="row p-t-20">
                                                    <div class="col-sm-3">
                                                        <label>Date :    </label>
                                                        <asp:Label ID="lblrootcausedate_p" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-7">
                                                        <label>Production Engineer: </label>
                                                        <asp:Label ID="lblrootcauseenginner_p" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label>Signature : </label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Corrective Action  </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div>
                                                    <asp:Label ID="lblcorrectiveaction_p"
                                                        Text="Bellow To Flange Attachement Weld Join Leak Found During Numatic Luke texting" runat="server">
                                                    </asp:Label>
                                                </div>
                                                <div class="row p-t-20">
                                                    <div class="col-sm-3">
                                                        <label>Time Target :    </label>
                                                        <asp:Label ID="lblcorrectiveactiondate_p" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-7">
                                                        <label style="display: block;">Responsibility  </label>
                                                        <label>Production Incharge : </label>
                                                        <asp:Label ID="lblcorrectiveactionengineername_p" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label>Signature : </label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Effectiveness Of Action </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div>
                                                    <asp:Label ID="lbleffectivenessofaction_p"
                                                        Text="Bellow To Flange Attachement Weld Join Leak Found During Numatic Luke texting" runat="server">
                                                    </asp:Label>
                                                </div>
                                                <div class="row p-t-20">
                                                    <div class="col-sm-3">
                                                        <label>Date :    </label>
                                                        <asp:Label ID="lbleffectivenessofactiondate_p" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-7">
                                                        <label>Quality Incharge : </label>
                                                        <asp:Label ID="lbleffectivenessofactionengineername_p" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label>Signature : </label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Final Disposition </label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <div>
                                                    <asp:Label ID="lblfinaldisposition_p"
                                                        Text="Bellow To Flange Attachement Weld Join Leak Found During Numatic Luke texting" runat="server">
                                                    </asp:Label>
                                                </div>
                                                <div class="row p-t-20">
                                                    <div class="col-sm-3">
                                                        <label>Date :    </label>
                                                        <asp:Label ID="lblfinaldispositiondate_p" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-7">
                                                        <label>Quality Incharge : </label>
                                                        <asp:Label ID="lblfinaldispositionengineername_p" runat="server"> </asp:Label>
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <label>Signature : </label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hdnAddress" runat="server" Value="" />
                    <asp:HiddenField ID="hdnPhoneAndFaxNo" runat="server" Value="" />
                    <asp:HiddenField ID="hdnEmail" runat="server" Value="" />
                    <asp:HiddenField ID="hdnWebsite" runat="server" Value="" />
					<asp:HiddenField ID="hdnCompanyName" runat="server" Value="" />
                    <asp:HiddenField ID="hdnDocNo" runat="server" Value="" />
                    <asp:HiddenField ID="hdnRevNo" runat="server" Value="" />
                    <asp:HiddenField ID="hdnRevDate" runat="server" Value="" />

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

