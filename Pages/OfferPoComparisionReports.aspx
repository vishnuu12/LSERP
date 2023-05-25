<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="OfferPoComparisionReports.aspx.cs" Inherits="Pages_OfferPoComparisionReports" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        table#OfferPoComparisionReports td {
            color: #000;
            font-weight: bold;
            font-size: smaller;
            white-space: normal;
        }

        table#tblOfferPoComparisionReports td:nth-child(9), td:nth-child(10) {
            font-weight: bold;
            font-size: revert;
        }

        .dataTable > thead > tr > th[class*="sort"]:before,
        .dataTable > thead > tr > th[class*="sort"]:after {
            content: "" !important;
        }

        .table {
            font-size: 13px;
            color: #000;
            border-collapse: collapse;
            border-spacing: 0;
            width: 100%;
            border: 1px solid #ddd;
            text-align: left;
            padding: 8px;
            white-space: nowrap;
        }


            .table tfoot input[type=text] {
                font-size: 10px;
                width: inherit;
            }

        .dataTables_scrollFoot tfoot input[type=text] {
            width: -webkit-fill-available !important;
        }

        .chosen-drop {
            width: max-content !important;
        }

        .chosen-search-input {
            color: #000;
        }

        .chosen-container a span {
            color: #000;
        }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {
            BindDate();
        });

        function BindDate() {
            $('#tblOfferPoComparisionReports').DataTable({

                "retrieve": true,
                "processing": true,
                "serverSide": true,
                "bDeferRender": true,
                "pageLength": 10,

                "bStateSave": true,
                "fnStateSave": function (oSettings, oData) {
                    localStorage.setItem('DataTables_' + window.location.pathname, JSON.stringify(oData));
                },
                "fnStateLoad": function (oSettings) {
                    var data = localStorage.getItem('DataTables_' + window.location.pathname);
                    return JSON.parse(data);
                },
                "stateSaveParams": function (settings, data) {
                    delete data.order;
                    data.length = 100;
                    data.order = [];
                },

                "ajax": ({
                    type: "GET",
                    url: "OfferPoComparisionReports.aspx/GetData", //It calls our web method
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    "data": function (d) {
                        return d;
                    },
                    "dataSrc": function (json) {
                        json.draw = json.d.draw;
                        json.recordsTotal = json.d.recordsTotal;
                        json.recordsFiltered = json.d.recordsFiltered;
                        json.Ldata = json.d.Ldata;
                        var return_data = json;
                        return return_data.Ldata;
                    },
                }),
                "columns": [
                    {
                        'data': null,
                        "mRender": function (data, type, full) {
                            return `<a href="#" onclick="return ViewOfferCopy('${data.EODID}')";><img src= "../Assets/images/print.png"></a>`
                        }
                    },
                    {
                        'data': null,
                        "mRender": function (data, type, full) {
                            return `<a href="#" onclick="return ViewPOCopy('${data.PoCopy}')";><img src= "../Assets/images/print.png"></a>`
                        }
                    },
                    { 'data': 'EnquiryID' },
                    { 'data': 'CustomerName' },
                    { 'data': 'OfferNo' },
                    { 'data': 'CompletedDate' },
                    { 'data': 'PORefNo' },
                    { 'data': 'PODate' },
                    { 'data': 'OfferCost' },
                    { 'data': 'POPrice' },
                    { 'data': 'EnquiryAlloted' },
                ],
            });
        }

        function ViewPOCopy(PoCopy) {
            __doPostBack("ViewPOCopy", PoCopy);
        }
        function ViewOfferCopy(OfferCopy) {
            __doPostBack("ViewOfferCopy", OfferCopy);
        }



        function PrintGenerateOffer(epstyleurl, style, Print, Main, topstrip) {

            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var Address = $('#ContentPlaceHolder1_hdnAddress').val();
            var PhoneAndFaxNo = $('#ContentPlaceHolder1_hdnPhoneAndFaxNo').val();
            var Email = $('#ContentPlaceHolder1_hdnEmail').val();
            var WebSite = $('#ContentPlaceHolder1_hdnWebsite').val();

            var CompanyName = $('#ContentPlaceHolder1_hdnCompanyName').val();
            var FormarlyCompanyName = $('#ContentPlaceHolder1_hdnFormarlyCompanyName').val();

            //Frontpage, Annexutr1, Annexure2, Annexure3, FooterContent         
            var Frontpage = document.getElementById("<%=divFrontpage_p.ClientID %>").innerHTML;
            var FooterContent = document.getElementById("<%=divAnnexure2footer.ClientID %>").innerHTML;

            var Annexure2RowLength = $('#ContentPlaceHolder1_gvAnnexure2_p').find('tr').length;
            var addtionalrowlength = $('#ContentPlaceHolder1_gvOfferChargesDetails_p').find('tr').length;

            if ($('#ContentPlaceHolder1_gvOfferChargesDetails_p').find('tr').find('td')[0].innerText == "No Records Found")
                addtionalrowlength = 1;

            var recordlength = (Annexure2RowLength - 1) + (addtionalrowlength - 1);

            var divAnnexure2HeaderContent = $('#ContentPlaceHolder1_divAnnexure2HeaderContent').html();
            var divAnnexure1HeaderContent = $('#ContentPlaceHolder1_divAnnexure1PDF').html();
            var divAnnexure3HeaderContent = $('#ContentPlaceHolder1_divAnnexure3PDF').html();

            var html = '';

            winprint.document.write("<html><head><title>");
            winprint.document.write("Offer");
            winprint.document.write("</title>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='" + epstyleurl + "'/>");
            winprint.document.write("<link rel='stylesheet' href='" + Main + "' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet'  href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/printg.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<style type='text/css'> #gvAnnexure2_print th { background-color:white;color:#000; } .annexureheader {border: 1px solid;padding: 8px 3px;text-align: center;} </style>");

            winprint.document.write("<div class='print-page jobcardprintoutermargin'></div>");
            winprint.document.write("<table class='jobcardprinttablemargin'><thead><tr><td>");
            winprint.document.write("<div class='col-sm-12 jobcardprinttheadheight' style='text-align:center;font-size:20px;font-weight:bold;border-bottom: 1px solid black;height: 33mm ! important;'>");
            winprint.document.write("<div>");
            winprint.document.write("<div>");
            winprint.document.write("<div class='col-sm-12 padding0' id='divLSERPLogo' style='margin:20px auto;display:block;'>");
            winprint.document.write("<div class='row p-t-5 jobcardprinttheadwidth' style='border-bottom: 0px solid #000 ! important;'>");
            winprint.document.write("<div class='col-sm-3'>");
            winprint.document.write("<img src='../Assets/images/lonestar.jpeg' alt='lonestar-image' width='90px'>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-6 text-center'>");
            winprint.document.write("<h3 style='font-weight:600;font-size:20px;color:#000;font-family: Times New Roman;display: contents;'> " + CompanyName + " </h3>");
            winprint.document.write("<span style='font-weight:600;font-size:11px ! important;font-family: Times New Roman;'> " + FormarlyCompanyName + " </span>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;width: 100%;padding:0 0px'>" + Address + "</p>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + PhoneAndFaxNo + "</p>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + Email + "</p>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + WebSite + "</p>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-3'>");
            winprint.document.write("<img src='../Assets/images/iso.jpeg' alt='lonestar-image' height='90px'>");
            winprint.document.write("</div></div>");
            winprint.document.write("</div></div></div>");
            winprint.document.write("</td></tr></thead>");
            winprint.document.write("<tbody><tr><td>");

            winprint.document.write("<div class='col-sm-12 padding0 page_generateoffer'>");
            winprint.document.write(Frontpage);
            winprint.document.write("</div>");

            if (parseInt($('#ContentPlaceHolder1_hdnAnnexure1CheckedLength').val()) > 0) {
                winprint.document.write("<div class='col-sm-12 padding0 page_generateoffer'>");
                winprint.document.write(divAnnexure1HeaderContent);
                winprint.document.write("<div style='padding-left: 25px; padding-right: 25px;'>");
                $("#ContentPlaceHolder1_gvAnnexure1_p").find('tr').each(function (i, j) {
                    if (i != 0) {
                        winprint.document.write('<h4 style="margin-top:6px;">' + $(this).find('.lblheader').text().replace(':', '') + ":" + '</h4>');
                        winprint.document.write('<p style="margin-top:-9px !important;margin-bottom:10px !important;">' + $(this).find('.lblpoint').text() + '</p>');
                    }
                });
                winprint.document.write("</div>");
                winprint.document.write("</div>");
            }

            var count = 0;
            var recordcount = 0;

            var chargesname = $('#ContentPlaceHolder1_gvOfferChargesDetails_p').find('tr').find('.chargesname').toArray().map(x => (x.innerText))
            var values = $('#ContentPlaceHolder1_gvOfferChargesDetails_p').find('tr').find('.value').toArray().map(x => (x.innerText))

            let Chargesstack = [];
            let valuesstack = [];

            for (var i = chargesname.length - 1; i >= 0; i--) {
                Chargesstack.push(chargesname[i]);
                valuesstack.push(values[i]);
            }

            while (true) {
                count = 0;
                winprint.document.write("<div class='col-sm-12 padding0 page_generateoffer'>");
                winprint.document.write(divAnnexure2HeaderContent);
                winprint.document.write("<div id='divannexure2' class='p-t-10' style='padding-right: 15px; padding-left: 15px;'>");
                winprint.document.write("<table class='table table-hover table-bordered medium' cellspacing='0' rules='all' border='1' id='gvAnnexure2_print' style='border-collapse:collapse;border-width: 1px !important; text-transform: none;'>");

                if (recordcount < (Annexure2RowLength - 1)) {
                    winprint.document.write("<thead>");
                    winprint.document.write($('#ContentPlaceHolder1_gvAnnexure2_p').find('tr:first').html());
                    winprint.document.write("</thead>");
                }
                winprint.document.write("<tbody>");

                for (var j = recordcount + 1; j < parseInt(Annexure2RowLength); j++) {
                    if (count == 14) {
                        break;
                    }
                    else {
                        winprint.document.write("<tr>" + $('#ContentPlaceHolder1_gvAnnexure2_p').find('tr')[j].innerHTML + "</tr>");
                        count++;
                    }
                }

                if (count != 14) {

                    while (true) {
                        if (Chargesstack.length > 0) {
                            winprint.document.write("<tr><td colspan='7' style='text-align:end;'><span>" + Chargesstack.pop() + " ( " + $('#ContentPlaceHolder1_lblCurrencySymbol_p').text() + " ) " + "</span></td><td style='text-align:right;'><span>" + valuesstack.pop() + "</span></td></tr>");
                            count++;
                            if (count == 14)
                                break;
                        }
                        else
                            break;
                    }
                }

                recordcount = recordcount + count

                if (recordcount == recordlength) {
                    winprint.document.write("<tr><td colspan='7' style='text-align:end;'><span style='color:#000;font-weight:bold;'>Total Price Of " + $('#ContentPlaceHolder1_lblExForBasis_p').text() + "( " + $('#ContentPlaceHolder1_lblCurrencySymbol_p').text() + " )</span></td><td style='text-align:right;'><span style='color:#000;font-weight:bold;'>" + $('#ContentPlaceHolder1_lblsumofprice_p').text() + "</span></td></tr>");
                }
                winprint.document.write("</tbody>");
                winprint.document.write("<tfoot>");
                winprint.document.write("</tfoot>");
                winprint.document.write("</table>");
                winprint.document.write("</div>");

                if (recordcount == recordlength) {
                    winprint.document.write("<div class='p-t-10 col-sm-12'>");
                    winprint.document.write("<label style='font-weight:bold;padding-right:15px;padding-left:15px;'>Note : " + $('#ContentPlaceHolder1_lblNote_p').text() + "  </label>");
                    winprint.document.write("</div>");
                }

                winprint.document.write("</div>");

                if (recordcount == recordlength) {
                    break;
                }
                else
                    continue;
            }

            if (parseInt($('#ContentPlaceHolder1_hdnAnnexure3CheckedLength').val()) > 0) {
                winprint.document.write("<div class='col-sm-12 padding0 page_generateoffer'>");
                winprint.document.write(divAnnexure3HeaderContent);
                winprint.document.write("<div style='padding-left: 20px; padding-right: 25px;'>");
                $("#ContentPlaceHolder1_gvAnnexure3_p").find('tr').each(function (i, j) {
                    if (i != 0) {
                        winprint.document.write('<div class="col-12 row" style="display:block;">');
                        winprint.document.write('<h4>' + $(this).find('.lblheader').text().replace(':', '') + ":" + '</h4>');
                        winprint.document.write('<p style="padding-top:0px;">' + $(this).find('.lblpoint').text() + '</p>');
                        winprint.document.write('</div>');
                    }
                });
                winprint.document.write("</div>");
                winprint.document.write("</div>");
            }

            winprint.document.write("</td></tr></tbody>");
            winprint.document.write("<tfoot><tr><td>");
            winprint.document.write("<div class='col-sm-12'>");
            winprint.document.write("<div>");
            winprint.document.write("<div class='col-sm-12 row' style='margin-bottom:10mm;border-top:1px solid #000;position:fixed;width:190mm;bottom:0px;height:25mm;'>");//margin-bottom:10mm;
            winprint.document.write(FooterContent);
            winprint.document.write("</div>");
            winprint.document.write("</div></div>");
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
                                        <img src="../Assets/images/pages.png" alt="" width="16px" />
                                    </span>
                                    <h3 class="page-title-head d-inline-block">OfferPo Comparision Report </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Reports</a></li>
                                <li class="active breadcrumb-item" aria-current="page">OfferPo Comparision Report</li>
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
                    <asp:PostBackTrigger ControlID="imgExceldownload" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" runat="server">
                            <div class="ip-div text-center">
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="col-sm-12 row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                                    <asp:Label ID="lbltitle" runat="server" Text="OfferPo Comparision Reports"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server">
                                                <asp:LinkButton ID="imgExceldownload" runat="server" CssClass="excel_bg" ToolTip="Excel Download"
                                                    OnClick="btnExcel_Click" />
                                            </div>
                                        </div>
                                        <div id="divOfferPoComparisionReports">

                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <table class="table table-hover table-bordered medium"
                                                    id="tblOfferPoComparisionReports" style="width: 100%;">
                                                    <thead>
                                                        <tr>
                                                            <th>Offer Copy </th>
                                                            <th>PO Copy </th>
                                                            <th>Enquiry ID </th>
                                                            <th>Customer Name </th>
                                                            <th>Offer No </th>
                                                            <th>Completed Date </th>
                                                            <th>PO RefNo </th>
                                                            <th>PO Date </th>
                                                            <th>Offer Cost </th>
                                                            <th>PO Price </th>
                                                            <th>Enquiry Alloted </th>
                                                        </tr>
                                                    </thead>
                                                </table>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <iframe id="ifrm" visible="false" runat="server"></iframe>
                    <div id="divGenerateOfferDetails" style="display: none;">

                        <div id="divFrontpage_p" runat="server">
                            <div id="divfrontPagePopUpContent" class="FrontPagepopupcontent" runat="server">
                                <div class="row" style="border-width: 0px 0px 1px 0px; border-style: solid; padding-left: 0; padding-right: 0; padding-top: 0px">
                                    <div class="col-sm-6 text-left pad-20" style="padding-left: 15px; padding-right: 15px; border-right: 1px solid #000;">
                                        <div class="col-sm-12">
                                            To:
                                        </div>
                                        <div class="col-sm-12">
                                            <asp:Label ID="lblCustomerAddress" runat="server" Style="font-weight: 700"></asp:Label>
                                        </div>
                                        <div class="col-sm-12" style="padding-top: 30px">
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <label class="form-label" style="display: contents;">
                                                        Phone
                                                    </label>
                                                </div>
                                                <div class="col-sm-1 text-center">
                                                    :
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblPhoneNumber" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <label class="form-label" style="display: contents;">
                                                        Fax Number
                                                    </label>
                                                </div>
                                                <div class="col-sm-1 text-center">
                                                    :
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblFaxNumber" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <label class="form-label" style="display: contents;">
                                                        Email
                                                    </label>
                                                </div>
                                                <div class="col-sm-1 text-center">
                                                    :
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-20">
                                            <div class="row">
                                                <div class="col-sm-4">
                                                    <label>
                                                        Kind Attention</label>
                                                </div>
                                                <div class="col-sm-1 text-center">
                                                    :
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblCustomerContactName" Style="font-weight: 700" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div>
                                                <div class="row">
                                                    <div class="col-sm-4">
                                                        <label style="display: contents;">
                                                            Phone Number</label>
                                                    </div>
                                                    <div class="col-sm-1 text-center">
                                                        :
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <asp:Label ID="lblCustomerPhoneNumber" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-4">
                                                        <label style="display: contents;">
                                                            Mobile Number</label>
                                                    </div>
                                                    <div class="col-sm-1 text-center">
                                                        :
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <asp:Label ID="lblCustomerMobileNumber" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-4">
                                                        <label style="display: contents;">
                                                            Email</label>
                                                    </div>
                                                    <div class="col-sm-1 text-center">
                                                        :
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <asp:Label ID="lblCustomerEmail" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 text-left pad-20" style="padding-left: 0; padding-right: 0;">
                                        <div class="row" style="display: flex; align-items: baseline; padding-left: 15px">
                                            <div class="col-sm-6">
                                                <label class="form-label" style="display: inline-block; margin-bottom: 5px !important;">
                                                    Offer No:
                                                </label>
                                                <asp:Label ID="lblOfferNo" runat="server" Style="font-weight: 700"></asp:Label>
                                            </div>
                                            <div class="col-sm-6">
                                                <label style="font-weight: 700">
                                                    Dt.</label>
                                                <asp:Label ID="lblOfferDate" runat="server" Style="font-weight: 700"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-30" style="padding-left: 15px; padding-right: 15px">
                                            <label class="form-label" style="display: contents; font-weight: 700">
                                                Subject:
                                            </label>
                                        </div>
                                        <div class="col-sm-12" style="padding-left: 15px; padding-right: 15px">
                                            <asp:Label ID="lblsubjectItems" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="padding-top: 30px; padding-left: 15px; padding-right: 15px">
                                            <label class="form-label" style="font-weight: 700">
                                                Reference:
                                            </label>
                                            <asp:Label ID="lblReference" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="padding-left: 15px; padding-right: 15px">
                                            <label class="form-label" style="font-weight: 700">
                                                Project:
                                            </label>
                                            <asp:Label ID="lblProjectDescription" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12 text-left" style="margin-top: 20px; padding-left: 15px">
                                    <asp:Label ID="lblfrontpageres" Style="font-weight: bolder; padding-left: 15px; padding-right: 15px"
                                        runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-12 text-left" style="padding-left: 15px; padding-right: 15px">
                                    <p class="p-t-30 f-norm">
                                        We acknowledge with thanks the receipt of your above mentioned enquiry.
                                    </p>
                                    <p class="p-t-20">
                                        In response to your enquiry, we are pleased to submit our <span id="lbl_offertype"
                                            runat="server"></span>&nbsp Offer as follows:
                                    </p>
                                </div>
                                <div class="col-sm-12 p-t-10 text-left" id="divAnnexure1HeaderName" runat="server" style="padding-left: 15px; padding-right: 15px; display: none;">
                                    <asp:Label ID="lblAnnexure1HeaderName" runat="server"></asp:Label>
                                    <asp:Label ID="lblAnnexure1Header" CssClass="p-t-10 f-norm" Style="display: contents;"
                                        runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-12 text-left" id="divAnnexure2HeaderName" runat="server" style="padding-left: 15px; padding-right: 15px; display: none;">
                                    <asp:Label ID="lblAnnexure2HeaderName" runat="server"></asp:Label>
                                    <asp:Label ID="lblAnnexure2Header" CssClass="p-t-10 f-norm" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-12 text-left" id="divAnnexure3HeaderName" runat="server" style="padding-left: 15px; padding-right: 15px; display: none;">
                                    <asp:Label ID="lblAnnexure3HeaderName" runat="server"></asp:Label>
                                    <asp:Label ID="lblAnnexure3Header" CssClass="f-norm"
                                        runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-12" style="padding-left: 15px; padding-right: 15px">
                                    <p class="p-t-30 f-norm">
                                        Hope that the above will be in line.
                                    </p>
                                    <p class="p-t-20">
                                        In case of any further details required, Please contact: -
                                    </p>
                                </div>
                                <div class="col-sm-12 p-t-30 text-left" style="padding-left: 15px;">
                                    <asp:Label ID="lblMarketingEngineer" Style="font-weight: bold; color: black; padding-left: 15px; padding-right: 15px"
                                        runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-12 p-t-10 text-left" style="padding-left: 15px; padding-right: 15px">
                                    <label class="form-label" style="display: contents; font-weight: 700;">
                                        Phone Number:
                                    </label>
                                    <asp:Label ID="lblSalesPhoneNumber" runat="server" Style="font-weight: 700; color: Black;"></asp:Label>
                                </div>
                                <div class="col-sm-12 p-t-10 text-left" style="padding-left: 15px; padding-right: 15px">
                                    <label class="form-label" style="display: contents; font-weight: bold; padding-left: 15px; padding-right: 15px">
                                        Mobile Number:
                                    </label>
                                    <asp:Label ID="lblSalesMobileNumber" runat="server" Style="font-weight: 700; color: Black; padding-left: 15px; padding-right: 15px"></asp:Label>
                                </div>
                                <div class="col-sm-12 p-t-10 text-left" style="padding-left: 15px; padding-right: 15px">
                                    <label class="p-t-20 f-norm">
                                        Thanks & Regards</label>
                                </div>
                            </div>
                        </div>

                        <div class="Annexure1PDF" id="divAnnexure1PDF" runat="server" style="display: none;">
                            <div style="text-align: center; width: 100%; display: inline-flex;" class="row" id="Div10" runat="server">
                                <div class="col-sm-4 annexureheader" style="font-weight: 700;">
                                    <asp:Label ID="lblAnnexure1PopUpColumnHead" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-4 annexureheader" style="font-weight: 700;">
                                    <asp:Label ID="lblAnnexure1OfferNumber" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-4 annexureheader" style="font-weight: 700;">
                                    <asp:Label ID="lblAnneure1OfferDate" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <asp:GridView ID="gvAnnexure1_p" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                            Style="text-transform: none" CssClass="table table-hover table-bordered medium"
                            HeaderStyle-HorizontalAlign="Center"
                            EmptyDataText="No Records Found">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'
                                            Style="text-align: center"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Scope Header" HeaderText="Scope Header" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSOWHeader" class="lblheader" runat="server" Text='<%# Eval("HeaderName")%>'
                                            Style="text-align: center"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Scope Point" HeaderText="Scope Point" ItemStyle-Width="35%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsowPoint" class="lblpoint" runat="server" Text='<%# Eval("Point")%>'
                                            Style="text-align: center"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <asp:GridView ID="gvAnnexure3_p" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                            Style="text-transform: none" CssClass="table table-hover table-bordered medium"
                            HeaderStyle-HorizontalAlign="Center"
                            EmptyDataText="No Records Found">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'
                                            Style="text-align: center"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="TAC Header" HeaderText="TAC Header" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTACHeader" class="lblheader" runat="server" Text='<%# Eval("HeaderName")%>'
                                            Style="text-align: center"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="TAC Point" HeaderText="TAC Point" ItemStyle-Width="35%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTACPoint" class="lblpoint" runat="server" Text='<%# Eval("Point")%>'
                                            Style="text-align: center"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <asp:GridView ID="gvOfferChargesDetails_p" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                            Style="text-transform: none" CssClass="table table-hover table-bordered medium"
                            HeaderStyle-HorizontalAlign="Center"
                            EmptyDataText="No Records Found">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'
                                            Style="text-align: center"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Charges Name" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblchargesname" CssClass="chargesname" runat="server" Text='<%# Eval("Chargesname")%>'
                                            Style="text-align: center"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Value" ItemStyle-Width="35%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvalue" CssClass="value" runat="server" Text='<%# Eval("Value")%>'
                                            Style="text-align: center"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <div class="col-sm-12">
                            <asp:Label ID="lblNote_p" runat="server"> </asp:Label>
                        </div>

                        <div class="col-sm-12 p-t-10 p-l-0 p-r-0" id="divgvAnnexure2_p">
                            <asp:GridView ID="gvAnnexure2_p" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                CssClass="table table-hover table-bordered medium" HeaderStyle-HorizontalAlign="Center"
                                EmptyDataText="No Records Found"
                                Style="border-width: 1px !important; text-transform: none">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSno" class="gvsno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'
                                                Style="text-align: center"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tag No" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTagNo" runat="server" Text='<%# Eval("TagNo")%>' Style="text-align: center"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsize" runat="server" Text='<%# Eval("Size")%>' Style="text-align: center"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Name" ItemStyle-Width="40%">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPoint" runat="server" Text='<%# Eval("Itemname")%>' Style="text-align: center"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Drawing No" ItemStyle-Width="35%" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblfilename" runat="server" Text='<%# Eval("DrawingName")%>' Style="text-align: center"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty (No's)" ItemStyle-Width="5%"
                                        ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTACHeader" runat="server" Text='<%# Eval("Qty")%>' Style="text-align: center; display: inline-block"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Price" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label class="lbl_UnitCost gvItemcost Price" ID="lbl_itemccost" Text='<%# Eval("UnitCost")%>'
                                                runat="server" Style="margin: 0px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Price" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label class="lbl_itemccost gvItemcost Price" ID="lbl_itemccost" Text='<%# Eval("FinalCost")%>'
                                                runat="server" Style="margin: 0px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <div id="div6" runat="server">
                                <div class="text-right divtotalprice" style="padding-right: 16px;">
                                    <label class="lbl_totalprice" style="margin-top: 2%; font-weight: bolder;">
                                        Total Price of
                                                    <%--<label class="ForBasis">
                                                        Ex - Works1
                                                    </label>--%>
                                        <asp:Label ID="lblExForBasis_p" runat="server"></asp:Label>
                                        <asp:Label ID="lblCurrencySymbol_p" runat="server"></asp:Label>
                                        <asp:Label ID="lblsumofprice_p" CssClass="Price" Style="display: contents;" runat="server"></asp:Label>
                                        <asp:Label ID="lblquotated_p" class="Quoted" Text="Quoted" Style="display: none;" runat="server"> </asp:Label>
                                    </label>
                                </div>
                            </div>
                        </div>

                        <div class="inner-container divContent" id="divAnnexure2PDF" runat="server">
                            <div id="divAnnexure2HeaderContent" runat="server">
                                <div class="row text-center" style="width: 100%; display: inline-flex;" id="Div8" runat="server">
                                    <div class="col-sm-4 annexureheader" style="font-weight: 600;">
                                        <asp:Label ID="lblAnnexure2PopColumnHead" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-4 annexureheader" style="font-weight: 600;">
                                        <asp:Label ID="lblAnnexure2OfferNo" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-4 annexureheader" style="font-weight: 600;">
                                        <asp:Label ID="lblAnnexure2OfferDate" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12 text-left row" style="padding-left: 15px; padding-right: 15px">
                                    <label style="font-weight: bold; color: black; padding-top: 10px">
                                        Price Schedule</label>
                                </div>
                                <div class="col-sm-12 p-t-10" style="padding-left: 15px; padding-right: 15px;">
                                    <label>
                                        Given below is the Schedule of Prices for "LONESTAR" make Metal Expansion Joints
                                        / Bellows as per Our Drawings.</label>
                                </div>
                            </div>
                            <div id="divannexure2" class="col-sm-12 row annexure2PopUpContent" style="padding-top: 10px; padding-right: 15px; margin-top: 30px; padding-left: 15px;"
                                runat="server">
                            </div>
                            <div id="divAnnexure2OfferNote" runat="server">
                                <div class="offernote">
                                    <asp:Label ID="lbloffernote" Style="font-weight: 700; color: Black !important; margin-left: 10px; margin-top: 10px;"
                                        runat="server">
                                    </asp:Label>
                                </div>
                            </div>
                            <div class="row" style="border: 1px solid #000; width: 200mm" id="divAnnexure2footer" runat="server">
                                <div class="col-sm-6" style="font-weight: 700; padding-top: 10px; padding-left: 15px; padding-right: 15px;">
                                    <div class="col-sm-12">
                                        <label style='font-weight: 700; font-size: 14px; font-family: Times New Roman; display: contents;'>
                                            <asp:Label ID="lblLeftfootercompanyName" runat="server"></asp:Label>
                                        </label>
                                        <%--<span style='font-weight: 700; font-size: 14px; font-family: Times New Roman;'>INDUSTRIES</span>--%>
                                    </div>
                                    <div class="col-sm-12" style="margin-top: 40px">
                                        <asp:Label ID="lblAnnexure2MarketingHead" CssClass="MarketingHead" Style="font-size: 14px; font-weight: 700; color: black;"
                                            runat="server">
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-12">
                                        <asp:Label ID="lblFooterHeadDesignationWihMobNo" runat="server" Style="font-size: 14px; font-weight: bold; color: black;">
                                        </asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-4 text-right" style="font-weight: 700; border-left: 1px solid; height: 100px; padding-top: 10px; padding-right: 0px">
                                    <div class="col-sm-12">
                                        <label style='font-weight: 700; font-size: 14px; font-family: Times New Roman; display: contents;'>
                                            <asp:Label ID="lblrightfootercompanyName" runat="server"></asp:Label>
                                        </label>
                                        <%-- <span style='font-weight: 700; font-size: 14px; font-family: Times New Roman;'>INDUSTRIES</span>--%>
                                    </div>
                                    <div class="col-sm-12" style="margin-top: 40px">
                                        <label style="font-size: 14px; font-weight: 700; color: black;">
                                            <asp:Label ID="lblAnnnexure2MD" runat="server"></asp:Label></label>
                                    </div>
                                    <div class="col-sm-12">
                                        <label style="font-size: 14px; font-weight: 700; color: black;">
                                            <asp:Label ID="lblMDDesignationName" runat="server"></asp:Label>
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <asp:Image ID="imgQrcode" class="Qrcode" ImageUrl="" runat="server" />
                                </div>
                            </div>
                        </div>

                        <div class="Annexure3PDF" id="divAnnexure3PDF" runat="server" style="display: none;">
                            <div class="row" style="width: 100%; display: inline-flex;" id="Div5" runat="server">
                                <div class="col-sm-4 annexureheader" style="font-weight: 600;">
                                    <asp:Label ID="lblAnnexure3PopUpColumnHead" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-4 annexureheader" style="font-weight: 600;">
                                    <asp:Label ID="lblAnnexure3offerNo" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-4 annexureheader" style="font-weight: 600;">
                                    <asp:Label ID="lblAnnexure3OfferDate" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12 text-left row" style="padding-left: 15px; padding-right: 15px;">
                                <label style="font-weight: bold; color: black; padding: 5px;">
                                    Commercial Terms</label>
                            </div>
                        </div>

                    </div>

                    <asp:HiddenField ID="hdnAnnexure3CheckedLength" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnAnnexure1CheckedLength" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnAnnexure1PopUpContent" runat="server" Value="" />
                    <asp:HiddenField ID="hdnAnnexure2PopUpContent" runat="server" Value="" />
                    <asp:HiddenField ID="hdnAnnexure3PopUpContent" runat="server" Value="" />
                    <asp:HiddenField ID="hdnAddress" runat="server" Value="" />
                    <asp:HiddenField ID="hdnPhoneAndFaxNo" runat="server" Value="" />
                    <asp:HiddenField ID="hdnEmail" runat="server" Value="" />
                    <asp:HiddenField ID="hdnWebsite" runat="server" Value="" />

                    <asp:HiddenField ID="hdnCompanyName" runat="server" Value="" />
                    <asp:HiddenField ID="hdnFormarlyCompanyName" runat="server" Value="" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

