<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/CommonMaster.master"
    CodeFile="Generateoffer.aspx.cs" Inherits="Pages_MakeOffer" ValidateRequest="false"
    ClientIDMode="Predictable" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="offerContent1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="../Assets/css/datatables2.css" />
    <script type="text/javascript" src="../Assets/scripts/datatables.js"></script>
    <script type="text/javascript" src="../Assets/scripts/datatables2.js"></script>
    <script language="JavaScript" type="text/javascript">
        $(document).ready(function () {
            //            $('.ddlpoints').change(function () {
            //                debugger;
            //                var ddltext = $(this).children("option:selected").text();
            //                $(this).closest('tr').find('.lblpoint').text(ddltext);
            //            });           

            $('body').on('click', '.btnShowcostannexure', function () {
                try {
                    debugger;
                    if ($('.lbl_itemccost').text() == '') {
                        SuccessMessage('Error', 'No records found');
                        return false;
                    }
                    $('#previewmodule').modal({
                        backdrop: 'static'
                    });
                    var gvcontnt = '';
                    var $poupcontnt1 = $($('#popuptbody').html());
                    $(this).closest('.card-body').find('table:first').find('tr').each(function () {
                        debugger;
                        if ($(this).find('td').length > 0) {
                            $poupcontnt = $poupcontnt1;
                            $poupcontnt.find('#popupsno').text($(this).find('.gvsno').text());
                            $poupcontnt.find('#popupItemname').text($(this).find('.gvItemname').text());
                            $poupcontnt.find('#popupDrawingnumber').text('AS PER ATTACHED SKETCh');
                            $poupcontnt.find('#popupSize').text('');
                            $poupcontnt.find('#popupcost').text($(this).find('.gvItemcost').text());
                            $poupcontnt.find('#popupqty').text($(this).find('.gvqty').text());
                            gvcontnt += '<tr class="rgRow" id="popuptr" style="width:180px;text-align:center;">' + $poupcontnt.html() + '</tr>';
                        }
                    });
                    $('#popuptbody').html(gvcontnt);
                    $('.popupcolumnhead').text('Annexure 2');
                    $('.popuptitle').text('Annexure 2 Preview');
                    return false;
                }
                catch (er) { }

            });
            $('body').on('click', '.btnShowpopup', function () {
                debugger;
                try {
                    $('#modelAddModule').modal({
                        backdrop: 'static'
                    });
                    var gvcontnt = '';
                    $(this).closest('.card-body').find('table').find('tr').each(function () {
                        if ($(this).find('td').length > 0 && $(this).find('[type="checkbox"]').is(':checked') == true) {
                            gvcontnt += '<h4>' + $(this).find('.lblheader').text() + '</h4>';
                            gvcontnt += '<p style="margin-top:10px !important;margin-bottom:10px !important;">' + $(this).find('.lblpoint').text() + '</p>';
                        }
                    });
                    $('.popupcontent').html(gvcontnt);
                    var anxrno = $(this).closest('.card-body').find('.lbltitle').text();
                    $('.popupcolumnhead').text(anxrno);
                    anxrno = anxrno + " Preview";
                    $('.popuptitle').text(anxrno);

                    $('.MarketingHead').text($('#ContentPlaceHolder1_txt_HeadName').val());

                    $('.popupColumnOfferNo').text($('#ContentPlaceHolder1_hdnOfferNo').val());
                    $('.popupOfferDate').text($('#ContentPlaceHolder1_hdnOfferSubmissionDate').val());

                    return false;
                } catch (er) { }
            });
            //$('.txtothercharges').blur(function () {
            //    debugger;
            //    var sumprice = 0;
            //    $('.txtothercharges').each(function () {
            //        if ($(this).val().trim() != '') {					
            //            sumprice = sumprice + parseInt($(this).val().trim());
            //        }
            //    });  
            //    sumprice = sumprice + parseInt($('#ContentPlaceHolder1_lblSumOfPrice').text());
            //    $('#ContentPlaceHolder1_lblSumOfPrice').text(sumprice);                
            //});
        });

        function Annexure3PointsChange(ele) {
            var ddltext = $(ele).children("option:selected").text();
            $(ele).closest('tr').find('.lblpoint').text(ddltext);
        }

        function Annexure1PointsChange(ele) {
            var ddltext = $(ele).children("option:selected").text();
            $(ele).closest('tr').find('.lblpoint').text(ddltext);
        }

        function sumtotalprice() {
            debugger;
            try {
                var sumprice = 0.00;
                $('.labelothercharges').remove();
                $('.divtotalprice').find('br').remove();
                $('.txtothercharges').each(function () {
                    if ($(this).val().trim() != '') {
                        $("<label class='labelothercharges' style='margin-top:1%;'>" + $(this).closest('td').prev().text() + " " + $('#ContentPlaceHolder1_lblCurrencySymbol').text() + parseFloat($(this).val().trim()).toFixed(2) + "</label><br/>").insertBefore(".lbl_totalprice");
                        sumprice = parseInt(sumprice) + parseInt($(this).val().trim());
                    }
                });
                sumprice = parseInt(sumprice) + parseInt($('#ContentPlaceHolder1_hdnSumOfPrice').val());
                $('#ContentPlaceHolder1_lblSumOfPrice').text(parseFloat(sumprice).toFixed(2));
                if ($('.txtoffernote').val().trim() != '') $('#ContentPlaceHolder1_hdnoffernote').val('Note : ' + $('.txtoffernote').val().trim());
            } catch{ $('#ContentPlaceHolder1_lblSumOfPrice').text($('#ContentPlaceHolder1_hdnSumOfPrice').val()); }
        }
        function Annxure2PopUP() {
            try {
                $('#mpeAnnexurte2').modal({
                    backdrop: 'static'
                });
                var anxrno = $(event.target).closest('.card-body').find('.lbltitle').text();
                $('.popupcolumnhead').text(anxrno);
                $('.MarketingHead').text($('#ContentPlaceHolder1_txt_HeadName').val());
                $('.annexure2PopUpContent').html($('#divgvAnnexure2').html())
                //  $('.annexure2PopUpContent').html(gvcontnt);
                return false;
            } catch (er) { }

            return false;
        }

        function ShowFrontPagePopUp() {
            $('#mpeFrontPage').modal({
                backdrop: 'static'
            });
            $('.MarketingHead').text($('#ContentPlaceHolder1_txt_HeadName').val());
            return false;
        }

        function GeneratePDF() {
            var mand = Mandatorycheck('ContentPlaceHolder1_divOutput', 'noprogress');
            if (mand == false) return false;
            sumtotalprice();
            var gvcontentAnnexure1 = '';
            var gvcontentAnnexure3 = "";
            $("#ContentPlaceHolder1_gvAnnexure1").find('tr').each(function () {
                if ($(this).find('td').length > 0 && $(this).find('[type="checkbox"]').is(':checked') == true) {
                    gvcontentAnnexure1 += '<h4 style="margin-top:6px;">' + $(this).find('.lblheader').text().replace(':', '') + ":" + '</h4>';
                    gvcontentAnnexure1 += '<p style="margin-top:-9px !important;margin-bottom:10px !important;">' + $(this).find('.lblpoint').text() + '</p>';
                }
            });
            $("#ContentPlaceHolder1_gvAnnexure3").find('tr').each(function () {
                if ($(this).find('td').length > 0 && $(this).find('[type="checkbox"]').is(':checked') == true) {
                    gvcontentAnnexure3 += '<div class="col-12 row">'
                    gvcontentAnnexure3 += '<h4>' + $(this).find('.lblheader').text().replace(':', '') + ":" + '</h4>';
                    gvcontentAnnexure3 += '<p>' + $(this).find('.lblpoint').text() + '</p>';
                    gvcontentAnnexure3 += '</div>'
                }
            });

            var Annexure1CheckedLength = $('#ContentPlaceHolder1_gvAnnexure1').find('[type="checkbox"]:checked').length;
            var Annexure3CheckedLength = $('#ContentPlaceHolder1_gvAnnexure3').find('[type="checkbox"]:checked').length;

            $('#ContentPlaceHolder1_hdnAnnexure1PopUpContent').val(gvcontentAnnexure1);
            $('#ContentPlaceHolder1_hdnAnnexure3PopUpContent').val(gvcontentAnnexure3);
            $('#ContentPlaceHolder1_hdnAnnexure2PopUpContent').val($('#divgvAnnexure2').html());
            $('#ContentPlaceHolder1_hdnAnnexure1CheckedLength').val(Annexure1CheckedLength);

            var Annexure2TableHeader = $('#ContentPlaceHolder1_gvAnnexure2').find('tr:first').html();

            var k = 0;
            var html = "";
            var TotalRowLength = $('#ContentPlaceHolder1_gvAnnexure2').find('tr').length - 1;
            var RecordLength = parseInt(($('#ContentPlaceHolder1_gvAnnexure2').find('tr').length - 1) / 15);
            for (var j = k; j < k + 15; j++) {
                if (j + 1 == k + 15) {
                    k = k + 15;
                    html = html + '#' + "<tr>" + $('#ContentPlaceHolder1_gvAnnexure2').find('tr')[j + 1].innerHTML + "</tr>";
                }
                else {
                    if (html == '')
                        html = "<tr>" + $('#ContentPlaceHolder1_gvAnnexure2').find('tr')[j + 1].innerHTML + "</tr>";
                    else
                        html = html + "<tr>" + $('#ContentPlaceHolder1_gvAnnexure2').find('tr')[j + 1].innerHTML + "</tr>";
                }
                if (TotalRowLength == j + 1) {
                    break;
                }
            }

            var divAnnexure2HeaderContent = $('#ContentPlaceHolder1_divAnnexure2HeaderContent').html();
            var divAnnexure2OfferNote = $('#ContentPlaceHolder1_divAnnexure2OfferNote').html();
            var divAnnexure2TotalPrice = $('#ContentPlaceHolder1_divAnnexure2TotalPrice').html();


            var SHDesignnation = $('#ContentPlaceHolder1_txt_SHdesignation').val();
            var Annxure2MD = $('#ContentPlaceHolder1_txt_HeadName').text();
            $('#ContentPlaceHolder1_lblFooterHeadDesignationWihMobNo').text(SHDesignnation);
            $('#ContentPlaceHolder1_lblAnnnexure2MD').text(Annxure2MD);

            var Annexure2FooterContent = $('#ContentPlaceHolder1_divAnnexure2footer_S').html();

            $('#ContentPlaceHolder1_hdntablerows').val(html);
            // $('#ContentPlaceHolder1_hdndivAnnexure2HeaderContent').val(divAnnexure2HeaderContent);
            $('#ContentPlaceHolder1_hdndivAnnexure2OfferNote').val(divAnnexure2OfferNote);
            $('#ContentPlaceHolder1_hdndivAnnexure2TotalPrice').val(divAnnexure2TotalPrice);
            $('#ContentPlaceHolder1_hdndivAnnexure2TableHeader').val(Annexure2TableHeader);
            $('#ContentPlaceHolder1_hdnRecordLength').val(RecordLength);
            $('#ContentPlaceHolder1_hdnTotalRowLength').val(TotalRowLength);
            $('#ContentPlaceHolder1_hdnAnnexure3CheckedLength').val(Annexure3CheckedLength);
            $('#ContentPlaceHolder1_hdnAnnexure2FooterContent').val(Annexure2FooterContent);

            __doPostBack('GeneratePDF');
            return false;
        }

        //function ShowPopup() {
        //    debugger;
        //    $('#modelAddModule').modal({
        //        backdrop: 'static'
        //    });
        //    debugger;
        //    var gvcontnt = '';
        //    $('#ContentPlaceHolder1_gvAnnexure1,gvAnnexure1').find('tr').each(function () {
        //        if ($(this).find('td').length > 0 && $(this).find('[type="checkbox"]').is(':checked') == true) {
        //            gvcontnt += '<h4>' + $(this).find('.sowheadername').text() + '</h4>';
        //            gvcontnt += '<p style="margin-top:25px !important;margin-bottom:25px !important;">' + $(this).find('.sowpointname').text() + '</p>';
        //        }
        //    });
        //    $('#gvcontent,#ContentPlaceHolder1_gvcontent').html(gvcontnt);
        //    return false;
        //}

        function PrintGenerateOffer(epstyleurl, style, Print, Main, topstrip) {

            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var Address = $('#ContentPlaceHolder1_hdnAddress').val();
            var PhoneAndFaxNo = $('#ContentPlaceHolder1_hdnPhoneAndFaxNo').val();
            var Email = $('#ContentPlaceHolder1_hdnEmail').val();
            var WebSite = $('#ContentPlaceHolder1_hdnWebsite').val();

            //Frontpage, Annexutr1, Annexure2, Annexure3, FooterContent         
            var Frontpage = document.getElementById("<%=divFrontpage_p.ClientID %>").innerHTML;
            var FooterContent = document.getElementById("<%=divAnnexure2footer.ClientID %>").innerHTML;

            var Annexure2RowLength = $('#ContentPlaceHolder1_gvAnnexure2_p').find('tr').length;

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

            winprint.document.write("<div class='print-page' style='position:fixed;border:1px solid #000;width:200mm;height:287mm;margin-top:5mm;margin-bottom:5mm;margin-left:5mm;margin-right:5mm'></div>");
            winprint.document.write("<table style='width:200mm;height:287mm;margin-left:5mm;margin-right:5mm;border:0px !important'><thead><tr><td>");
            winprint.document.write("<div class='col-sm-12 print-generateoffer' style='border-bottom:1px solid #000;text-align:center;font-size:20px;font-weight:bold;padding:10px 0px;'>");
            winprint.document.write("<div>");
            winprint.document.write("<div>");
            winprint.document.write("<div class='col-sm-12 padding0' id='divLSERPLogo' style='display:block;width:200mm;height:25mm;margin-top:5mm;'>");
            winprint.document.write("<div class='row' style='position:fixed;width:200mm;'>");
            winprint.document.write("<div class='col-sm-3'>");
            winprint.document.write("<img src='../Assets/images/lonestar.jpeg' alt='lonestar-image' width='90px'>");
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

            winprint.document.write("<div class='col-sm-12 padding0 page_generateoffer'>");

            winprint.document.write(divAnnexure2HeaderContent);
            winprint.document.write("<div id='divannexure2' class='p-t-10' style='padding-right: 15px; padding-left: 15px;'>");
            winprint.document.write("<table class='table table-hover table-bordered medium' cellspacing='0' rules='all' border='1' id='gvAnnexure2_print' style='border-collapse:collapse;border-width: 1px !important; text-transform: none;'>");
            winprint.document.write("<thead>");
            winprint.document.write($('#ContentPlaceHolder1_gvAnnexure2_p').find('tr:first').html());
            winprint.document.write("</thead>");
            winprint.document.write("<tbody>");
            for (var j = 1; j < parseInt(Annexure2RowLength); j++) {
                winprint.document.write("<tr>" + $('#ContentPlaceHolder1_gvAnnexure2_p').find('tr')[j].innerHTML + "</tr>");
            }

            $("#ContentPlaceHolder1_gvOfferChargesDetails_p").find('tr').each(function (i, j) {
                if (i != 0) {
                    winprint.document.write("<tr><td colspan='7' style='text-align:end;'><span>" + $(this).find('.chargesname').text() + " ( " + $('#ContentPlaceHolder1_lblCurrencySymbol_p').text() + " ) " + "</span></td><td style='text-align:right;'><span>" + $(this).find('.value').text() + "</span></td></tr>");
                }
            });

            winprint.document.write("<tr><td colspan='7' style='text-align:end;'><span style='color:#000;font-weight:bold;'>Total Price Of EX-Works ( " + $('#ContentPlaceHolder1_lblCurrencySymbol_p').text() + " )</span></td><td style='text-align:right;'><span style='color:#000;font-weight:bold;'>" + $('#ContentPlaceHolder1_lblsumofprice_p').text() + "</span></td></tr>");

            winprint.document.write("</tbody>");
            winprint.document.write("</table>");

            winprint.document.write("</div>");
            winprint.document.write("</div>");

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
            winprint.document.write("<div class='col-sm-12 row' style='margin-bottom:5mm;border-top:1px solid #000;position:fixed;width:200mm;bottom:0px;height:25mm;'>");
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

        function chkQuoted() {
            if ($(event.target).prop("checked")) {
                $('#ContentPlaceHolder1_gvAnnexure2').find('.Quoted').css('display', 'block');
                $('.Quoted').css('display', 'block');
                $('#ContentPlaceHolder1_gvAnnexure2').find('.Price').css('display', 'none');
                $('.Price').css('display', 'none');
            }
            else {
                $('#ContentPlaceHolder1_gvAnnexure2').find('.Quoted').css('display', 'none');
                $('.Quoted').css('display', 'none');
                $('#ContentPlaceHolder1_gvAnnexure2').find('.Price').css('display', 'block');
                $('.Price').css('display', 'block');
            }
        }

        function chkBasis() {
            if ($(event.target).prop("checked")) {
                $('.ForBasis').text('FOR Basis');
            }
            else {
                $('.ForBasis').text('Ex - Works');
            }
        }

    </script>
</asp:Content>
<asp:Content ID="offerContent2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                    <h3 class="page-title-head d-inline-block">Generate Offer</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Sales</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Generate Offer</li>
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
                    <asp:PostBackTrigger ControlID="btnAnnexure" />
                    <asp:PostBackTrigger ControlID="btnOfferPDF" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%;overflow-x: scroll;">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="col-sm-12">
                                            <div class="col-sm-4 text-right">
                                                <label>
                                                    Offer Type</label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:DropDownList ID="ddlOfferName" CssClass="form-control" runat="server" ToolTip="Select Offer Name">
                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10 p-l-0 p-r-0">
                                            <table align="center" id="tbl_customerDetails" class="tr_class table" style="text-transform: none">
                                                <tbody>
                                                    <tr class="eqheading">
                                                        <td>
                                                            <div class="offerFormHeading1">
                                                                Customer Details
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <label id="lab_Customername" class="eqdetailslabel">
                                                                Customer Name</label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlcustomers" CssClass="form-control" runat="server" OnChange="showLoader();"
                                                                OnSelectedIndexChanged="ddlcustomers_IndexChanged" AutoPostBack="true" ToolTip="Select Cust Name"
                                                                Style="width: 380px !important">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left">
                                                            <span id="lab_Enquirynumber" class="eqdetailslabel mandatorylbl">Enquiry Number</span>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlenquiries" CssClass="form-control mandatoryfield" runat="server"
                                                                OnChange="showLoader();" OnSelectedIndexChanged="ddlenquiries_IndexChanged" AutoPostBack="true"
                                                                ToolTip="Select Enquiry Number">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="lab_offerno" class="eqdetailslabel">Offer No</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label class="offertextbox" ID="txtofferno" runat="server"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <span id="lab_offerrevision" class="eqdetailslabel">Revision No</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label class="offertextbox" ID="txt_offerrevision" runat="server"></asp:Label>
                                                            <%--<asp:Label class="offertextbox" ID="txt_CustomerphoneNo" runat="server"></asp:Label>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="lab_FaxNo" class="eqdetailslabel">Fax No</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox class="offertextbox" ID="txt_FaxNo" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <span id="lab_CustomerphoneNo" class="eqdetailslabel"> Land Line No </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox CssClass="form-control" ID="txt_CustomerphoneNo" runat="server" />
                                                            <%--<asp:Label class="offertextbox" ID="txt_CustomerphoneNo" runat="server"></asp:Label>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="lab_CustomerEmail" class="eqdetailslabel">Email</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox class="offertextbox" ID="txt_CustomerEmail" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <span id="lab_ContactPerson" class="eqdetailslabel">Contact Person</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox CssClass="form-control" ID="txt_ContactPerson" runat="server" />
                                                            <%--<asp:Label class="offertextbox" ID="txt_ContactPerson" runat="server"></asp:Label>--%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="lbl_CustomerMobile" class="eqdetailslabel">Customer Mobile No</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox CssClass="form-control" ID="txt_CustomerMobile" runat="server" />
                                                            <%--<asp:Label class="offertextbox" ID="txt_CustomerMobile" runat="server"></asp:Label>--%>
                                                        </td>
                                                        <td align="left">
                                                            <span id="lbl_ContactPersonEmail" class="eqdetailslabel">ContactPerson Email</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox CssClass="form-control" ID="txt_ContactPersonEmail" runat="server" />
                                                            <%--<asp:Label class="offertextbox" ID="txt_ContactPersonEmail" runat="server"></asp:Label>--%>
                                                        </td>
                                                    </tr>
                                                    <tr class="eqheading">
                                                        <td>
                                                            <div class="offerFormHeading1">
                                                                Marketing Engineer
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="lbl_MarktEngg" class="eqdetailslabel mandatorylbl">Markt Engg</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox class="offertextbox" ID="txt_MarktEngg" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <span id="lbl_MarktDesignation" class="eqdetailslabel">Designation</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox class="offertextbox" ID="txt_Marktdesignation" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="lab_MarktMobileNo" class="eqdetailslabel">Mobile No</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox class="offertextbox" ID="txt_MarktMobileNo" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <span id="lbl_Marktemail" class="eqdetailslabel">E-Mail</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox class="offertextbox" ID="txt_Marktemail" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="lbl_MarktOfficePhoneNo" class="eqdetailslabel">Office Phone No</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox class="offertextbox" ID="txt_MarktOfficePhoneNo" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr class="eqheading">
                                                        <td>
                                                            <div class="offerFormHeading1">
                                                                Managing Director
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="lbl_HeadName" class="eqdetailslabel mandatorylbl">Name</span>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="txt_HeadName" CssClass="offertextbox" runat="server"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <span id="lbl_HDesignation" class="eqdetailslabel">Designation</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="txt_HDesignation" CssClass="offertextbox" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="lbl_HMobileNo" class="eqdetailslabel">Mobile No</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="txt_HMobileNo" CssClass="offertextbox" runat="server"></asp:Label>
                                                        </td>
                                                        <td align="left">
                                                            <span id="lbl_HEmail" class="eqdetailslabel">E-Mail</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:Label ID="txt_HEmail" CssClass="offertextbox" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr class="eqheading">
                                                        <td>
                                                            <div class="offerFormHeading1">
                                                                Head
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="lbl_subheadname" class="eqdetailslabel mandatorylbl">Name</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox class="form-control" ID="txt_SubHeadName" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <span id="lbl_SHdesignation" class="eqdetailslabel">Designation</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox class="form-control" ID="txt_SHdesignation" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="lbl_SHMobileNo" class="eqdetailslabel">Mobile No</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox class="form-control" ID="txt_SHMobileNo" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <span id="lbl_SHEmail" class="eqdetailslabel">E-Mail</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox class="form-control" ID="txt_SHEmail" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                        <div class="ip-div text-center">
                                        </div>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <p class="h-style">
                                                    <asp:Label ID="Label5" runat="server" class="lbltitle" Text="Front Page"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10 p-l-0 p-r-0">
                                            <table align="center" id="tbl_frontpage" class="tr_class table" style="text-transform: none">
                                                <tbody>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="lbl_subitemname" class="eqdetailslabel mandatorylbl">Subject Item</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control mandatoryfield" ID="txtSubjectItem" runat="server" />
                                                        </td>
                                                        <td align="left">
                                                            <span id="lbl_projectname" class="eqdetailslabel">Project Name</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox CssClass="form-control" Style="width: 68%;" ID="txtProjectName" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="lbl_Reference" class="eqdetailslabel mandatorylbl">Reference</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control mandatoryfield" ID="txtReference" runat="server" />
                                                        </td>
                                                        <td align="left">
                                                            <span id="lbl_frntpage" class="eqdetailslabel">Front Page</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlfornt" CssClass="form-control" runat="server" ToolTip="Front Page">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Dear Sir / Madam," Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Dear Sir," Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="Dear Madam," Value="3"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                        <div class="ip-div text-center">
                                            <asp:Button runat="server" ID="btnFrontPage" OnClick="btnFrontPage_Click" CssClass="btn btn-cons btn-success btnFrontPagePopUp"
                                                Text="Preview" />
                                        </div>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <p class="h-style">
                                                    <asp:Label ID="lbltitle" runat="server" class="lbltitle" Text="Annexure 1"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10 p-l-0 p-r-0" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvAnnexure1" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                Style="text-transform: none" CssClass="table table-hover table-bordered medium"
                                                OnRowDataBound="gvAnnexure1_RowDataBound" HeaderStyle-HorizontalAlign="Center"
                                                EmptyDataText="No Records Found" DataKeyNames="SOWID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'
                                                                Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Scope to Display" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox Checked="true" ID="chksow" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Scope Header" HeaderText="Scope Header" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSOWHeader" class="lblheader" runat="server" Text='<%# Eval("HeaderName")%>'
                                                                Style="text-align: center"></asp:Label>
                                                            <asp:Label ID="lblSOWID" runat="server" Text='<%# Eval("SOWID")%>' Style="display: none"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Scope Point" HeaderText="Scope Point" ItemStyle-Width="35%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsowPoint" class="lblpoint" runat="server" Text='<%# Eval("Point")%>'
                                                                Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Select Scope Point" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlsowpoints" runat="server" onChange="Annexure1PointsChange(this);" CssClass="form-control ddlpoints">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnAnnexure1SOWID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnOfferSubmissionDate" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnOfferNo" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnoffernote" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnAnnexure1CheckedLength" runat="server" Value="0" />

                                            <asp:HiddenField ID="hdndivAnnexure2HeaderContent" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdntablerows" runat="server" Value="" />
                                            <asp:HiddenField ID="hdndivAnnexure2OfferNote" runat="server" Value="" />
                                            <asp:HiddenField ID="hdndivAnnexure2TotalPrice" runat="server" Value="" />
                                            <asp:HiddenField ID="hdndivAnnexure2TableHeader" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnRecordLength" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnTotalRowLength" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnAnnexure3CheckedLength" runat="server" Value="0" />

                                            <asp:HiddenField ID="hdnAnnexure2FooterContent" runat="server" Value="0" />

                                        </div>
                                        <div class="ip-div text-center">
                                            <asp:Button runat="server" CssClass="btn btn-cons btn-success btnShowpopup" Text="Preview" />
                                        </div>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <p class="h-style">
                                                    <asp:Label ID="Label2" runat="server" class="lbltitle" Text="Annexure 2"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10 p-l-0 p-r-0" id="divgvAnnexure2">
                                            <asp:GridView ID="gvAnnexure2" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                CssClass="table table-hover table-bordered medium" HeaderStyle-HorizontalAlign="Center"
                                                OnRowDataBound="gvAnnexure2_OnRowDataBound" EmptyDataText="No Records Found"
                                                DataKeyNames="EnquiryID" Style="border-width: 1px !important; text-transform: none">
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
                                                    <asp:TemplateField SortExpression="Item Name" HeaderText="Item Name" ItemStyle-Width="40%">
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
                                                    <asp:TemplateField SortExpression="Quantity" HeaderText="Qty (No's)" ItemStyle-Width="5%"
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
                                                            <asp:Label class="Quoted" Text="Quoted" runat="server" Style="margin: 0px; display: none;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Price" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label class="lbl_itemccost gvItemcost Price" ID="lbl_itemccost" Text='<%# Eval("FinalCost")%>'
                                                                runat="server" Style="margin: 0px"></asp:Label>
                                                            <asp:Label class="Quoted" Text="Quoted" runat="server" Style="margin: 0px; display: none;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <div id="divAnnexure2TotalPrice" runat="server">
                                                <div class="text-right divtotalprice" style="padding-right: 16px;">
                                                    <label class="lbl_totalprice" style="margin-top: 2%; font-weight: bolder;">
                                                        Total Price of
                                                    <label class="ForBasis">
                                                        Ex - Works
                                                    </label>
                                                        <asp:Label ID="lblCurrencySymbol" runat="server"></asp:Label>
                                                        <asp:Label ID="lblSumOfPrice" CssClass="Price" Style="display: contents;" runat="server"></asp:Label>
                                                        <asp:Label ID="lblQoted" class="Quoted" Text="Quoted" Style="display: none;" runat="server"> </asp:Label>
                                                    </label>
                                                    <asp:HiddenField ID="hdnSumOfPrice" runat="server" Value="0" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="ip-div text-center">
                                            <asp:Button runat="server" CssClass="btn btn-cons btn-success btnAnnexure2PopUp"
                                                OnClientClick="return Annxure2PopUP();" Text="Preview" />
                                        </div>
                                    </div>
                                    <div class="card-body" id="divothercharges" runat="server" visible="false">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <p class="h-style">
                                                    <asp:Label ID="Label6" runat="server" class="lbltitle" Text="Other Charges" />
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10 p-l-0 p-r-0">
                                            <table align="center" id="tbl_othercharges" class="tr_class table" style="text-transform: none">
                                                <tbody>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="lbl_GST">GST</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox onkeypress="fnAllowNumeric();" onblur="sumtotalprice();" CssClass="form-control txtothercharges"
                                                                ID="txt_GST" runat="server" />
                                                        </td>
                                                        <td align="left">
                                                            <span id="lbl_Freight">Freight</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox onkeypress="fnAllowNumeric();" onblur="sumtotalprice();" CssClass="form-control txtothercharges"
                                                                ID="txt_Freight" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="lbl_TransitInsurance">Transit Insurance</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control txtothercharges" onkeypress="fnAllowNumeric();"
                                                                onblur="sumtotalprice();" ID="txt_TransitInsurance" runat="server" />
                                                        </td>
                                                        <td align="left">
                                                            <span id="lbl_Inspection">Inspection</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control txtothercharges" onkeypress="fnAllowNumeric();"
                                                                onblur="sumtotalprice();" ID="txt_Inspection" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="lbl_Delivery">Delivery</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control txtothercharges" onkeypress="fnAllowNumeric();"
                                                                onblur="sumtotalprice();" ID="txt_Delivery" runat="server" />
                                                        </td>
                                                        <td align="left">
                                                            <span id="lbl_Guarantee">Guarantee</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control txtothercharges" onkeypress="fnAllowNumeric();"
                                                                onblur="sumtotalprice();" ID="txt_Guarantee" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="lbl_PaymentTerms">Payment Terms</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control txtothercharges" onkeypress="fnAllowNumeric();"
                                                                onblur="sumtotalprice();" ID="txt_PaymentTerms" runat="server" />
                                                        </td>
                                                        <td align="left">
                                                            <span id="lbl_Validity">Validity</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control txtothercharges" onkeypress="fnAllowNumeric();"
                                                                onblur="sumtotalprice();" ID="txt_Validity" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="lbl_LDClause">LD Clause</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control txtothercharges" onkeypress="fnAllowNumeric();"
                                                                onblur="sumtotalprice();" ID="txt_LDClause" runat="server" />
                                                        </td>
                                                        <td align="left">
                                                            <span id="lbl_Settlement">Settlement</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control txtothercharges" onkeypress="fnAllowNumeric();"
                                                                onblur="sumtotalprice();" ID="txt_Settlement" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="lbl_Legalization">Legalization</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control txtothercharges" onkeypress="fnAllowNumeric();"
                                                                onblur="sumtotalprice();" ID="txt_Legalization" runat="server" />
                                                        </td>
                                                        <td align="left">
                                                            <span id="lbl_ForceMajeure">Force Majeure</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control txtothercharges" onkeypress="fnAllowNumeric();"
                                                                onblur="sumtotalprice();" ID="txt_ForceMajeure" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="lbl_CountryofOrigin">Country of Origin</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control txtothercharges" onkeypress="fnAllowNumeric();"
                                                                onblur="sumtotalprice();" ID="txt_CountryofOrigin" runat="server" />
                                                        </td>
                                                        <td align="left">
                                                            <span id="lbl_PortLocationofShipment">Port & Location of Shipment</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control txtothercharges" onkeypress="fnAllowNumeric();"
                                                                onblur="sumtotalprice();" ID="txt_PortLocationofShipment" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="lbl_Insurance">Insurance</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control txtothercharges" onkeypress="fnAllowNumeric();"
                                                                onblur="sumtotalprice();" ID="txt_Insurance" runat="server" />
                                                        </td>
                                                        <td align="left">
                                                            <span id="lbl_DrawingGivenByCustomer">Drawing Given By Customer</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control txtothercharges" onkeypress="fnAllowNumeric();"
                                                                onblur="sumtotalprice();" ID="txt_DrawingGivenByCustomer" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="lbl_UStampValue">U'Stamp Value</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control txtothercharges" onkeypress="fnAllowNumeric();"
                                                                onblur="sumtotalprice();" ID="txt_UStampValue" runat="server" />
                                                        </td>
                                                        <td align="left">
                                                            <span id="lbl_IBRValue">IBR Value</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control txtothercharges" onkeypress="fnAllowNumeric();"
                                                                onblur="sumtotalprice();" ID="txt_IBRValue" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="lbl_SupervisionCharges">Supervision Charges</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control txtothercharges" onkeypress="fnAllowNumeric();"
                                                                onblur="sumtotalprice();" ID="txt_SupervisionCharges" runat="server" />
                                                        </td>
                                                        <td align="left">
                                                            <span id="lbl_III PartyInspectionCharges">III Party Inspection Charges</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control txtothercharges" onkeypress="fnAllowNumeric();"
                                                                onblur="sumtotalprice();" ID="txt_partyinspectioncharges" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="lbl_SeaFreight">Sea Freight</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control txtothercharges" onkeypress="fnAllowNumeric();"
                                                                onblur="sumtotalprice();" ID="txt_SeaFreight" runat="server" />
                                                        </td>
                                                        <td align="left">
                                                            <span id="lbl_AirFreight">Air Freight</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control txtothercharges" onkeypress="fnAllowNumeric();"
                                                                onblur="sumtotalprice();" ID="txt_AirFreight" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="lbl_Insurance1">Insurance</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control txtothercharges" onkeypress="fnAllowNumeric();"
                                                                onblur="sumtotalprice();" ID="txt_Insurance1" runat="server" />
                                                        </td>
                                                        <td align="left">
                                                            <span id="lbl_FOBCharges">FOB Charges</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control txtothercharges" onkeypress="fnAllowNumeric();"
                                                                onblur="sumtotalprice();" ID="txt_FOBCharges" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="lbl_ReplaceEx-Works">Replace Ex-Works</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control txtothercharges" onkeypress="fnAllowNumeric();"
                                                                onblur="sumtotalprice();" ID="TextBox3" runat="server" />
                                                        </td>
                                                        <td align="left">
                                                            <span id="lbl_ReplacePackingCharges">Replace Packing Charges(4% on rate)</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control txtothercharges" onkeypress="fnAllowNumeric();"
                                                                onblur="sumtotalprice();" ID="txt_ReplacePackingCharges" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="lbl_SeaWorthCharges">Export Packing Charges</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control txtothercharges" onkeypress="fnAllowNumeric();"
                                                                onblur="sumtotalprice();" ID="txt_SeaWorthCharges" runat="server" />
                                                        </td>
                                                        <td align="left">
                                                            <span id="lbl_ASMECodeCharges">ASME Code Charges</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control txtothercharges" onkeypress="fnAllowNumeric();"
                                                                onblur="sumtotalprice();" ID="txt_ASMECodeCharges" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="lbl_OceanFreightCharges">Ocean Freight Charges</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control txtothercharges" onkeypress="fnAllowNumeric();"
                                                                onblur="sumtotalprice();" ID="txt_OceanFreightCharges" runat="server" />
                                                        </td>
                                                        <td align="left">
                                                            <span id="lbl_Note">Note</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox CssClass="form-control txtoffernote" ID="txt_Note" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:CheckBox runat="server" ID="chckpricequoted" onChange="chkQuoted();" />
                                                            <span id="lbl_Quoted">Quoted Price</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:CheckBox runat="server" ID="chchkforbasis" onChange="chkBasis();" />
                                                            <span id="lbl_forbasis">FOR Basis</span>
                                                        </td>
                                                        <td align="left" />
                                                        <td />
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </div>
                                        <div class="ip-div text-center">
                                        </div>
                                    </div>
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <p class="h-style">
                                                    <asp:Label ID="lblanxr3" class="lbltitle" runat="server" Text="Annexure 3"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10 p-l-0 p-r-0" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvAnnexure3" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                Style="text-transform: none" CssClass="table table-hover table-bordered medium"
                                                OnRowDataBound="gvAnnexure3_RowDataBound" HeaderStyle-HorizontalAlign="Center"
                                                EmptyDataText="No Records Found" DataKeyNames="TACID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'
                                                                Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TAC to Display" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox Checked="true" ID="chktac" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="TAC Header" HeaderText="TAC Header" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTACHeader" class="lblheader" runat="server" Text='<%# Eval("HeaderName")%>'
                                                                Style="text-align: center"></asp:Label>
                                                            <asp:Label ID="lblTACID" runat="server" Text='<%# Eval("TACID")%>' Style="display: none"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="TAC Point" HeaderText="TAC Point" ItemStyle-Width="35%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTACPoint" class="lblpoint" runat="server" Text='<%# Eval("Point")%>'
                                                                Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Select TAC Point" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddltacpoints" runat="server" onChange="Annexure3PointsChange(this);"
                                                                CssClass="form-control ddlpoints">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnAnnexure3TACID" runat="server" Value="0" />
                                        </div>
                                        <div class="ip-div text-center">
                                            <asp:Button runat="server" CssClass="btn btn-cons btn-success btnShowpopup" Text="Preview" />
                                            <asp:Button ID="btnAnnexure" Text="Save Offer Details" CssClass="btn btn-cons btn-success" runat="server"
                                                OnClientClick="return GeneratePDF();" />
                                            <asp:Button ID="btnOfferPDF" Text="PDF" Visible="false" OnClick="btnOfferPrint_Click" CssClass="btn btn-cons btn-success" runat="server" />
                                        </div>
                                    </div>

                                    <%-- <div class="col-sm-12" style="padding-top: 10px;" id="Div13" runat="server">
                                        <div class="col-sm-6" style="font-weight: 600; padding-top: 10px; border: 1px solid;">
                                            <div class="col-sm-12">
                                                <label style="font-size: large; font-weight: bolder; color: black;">
                                                    For LONESTAR Industries</label>
                                            </div>
                                            <div class="col-sm-12">
                                                <asp:Label ID="lblAnnexure1MarketigHead" CssClass="MarketingHead" Style="font-size: large;
                                                    font-weight: bolder; color: black;" runat="server">
                                                </asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label style="font-size: large; font-weight: bolder; color: black;">
                                                    Sr.MANAGER</label>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 text-right" style="font-weight: 600; padding-top: 10px; border: 1px solid;">
                                            <div class="col-sm-12">
                                                <label style="font-size: large; font-weight: bolder; color: black;">
                                                    For LONESTAR Industries</label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label style="font-size: large; font-weight: bolder; color: black;">
                                                    G.UMAMAGESH</label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label style="font-size: large; font-weight: bolder; color: black;">
                                                    MANAGING PARTNER</label>
                                            </div>
                                        </div>
                                    </div>--%>

                                    <%-- <div class="col-sm-12" style="padding-top: 10px;" id="divFooter" runat="server">
                                        <div class="col-sm-6" style="font-weight: 600; padding-top: 10px; border: 1px solid;">
                                            <div class="col-sm-12">
                                                <label style="font-size: large; font-weight: bolder; color: black;">
                                                    For LONESTAR Industries</label>
                                            </div>
                                            <div class="col-sm-12">
                                                <asp:Label ID="lblAnnexure3MarketingHead" CssClass="MarketingHead" Style="font-size: large;
                                                    font-weight: bolder; color: black;" runat="server">
                                                </asp:Label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label style="font-size: large; font-weight: bolder; color: black;">
                                                    Sr.MANAGER</label>
                                            </div>
                                        </div>
                                        <div class="col-sm-6 text-right" style="font-weight: 600; padding-top: 10px; border: 1px solid;">
                                            <div class="col-sm-12">
                                                <label style="font-size: large; font-weight: bolder; color: black;">
                                                    For LONESTAR Industries</label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label style="font-size: large; font-weight: bolder; color: black;">
                                                    G.UMAMAGESH</label>
                                            </div>
                                            <div class="col-sm-12">
                                                <label style="font-size: large; font-weight: bolder; color: black;">
                                                    MANAGING PARTNER</label>
                                            </div>
                                        </div>
                                    </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>

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
                                                    <label class="ForBasis">
                                                        Ex - Works
                                                    </label>
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
                                        <label style='font-weight: 700; font-size: 14px; font-style: italic; color: black; font-family: Arial; display: contents;'>
                                            For LONESTAR
                                        </label>
                                        <span style='font-weight: 700; font-size: 14px; font-family: Times New Roman;'>INDUSTRIES</span>
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
                                        <label style='font-weight: 700; font-size: 14px; font-style: italic; color: black; font-family: Arial; display: contents;'>
                                            For LONESTAR
                                        </label>
                                        <span style='font-weight: 700; font-size: 14px; font-family: Times New Roman;'>INDUSTRIES</span>
                                    </div>
                                    <div class="col-sm-12" style="margin-top: 40px">
                                        <label style="font-size: 14px; font-weight: 700; color: black;">
                                            <asp:Label ID="lblAnnnexure2MD" runat="server"></asp:Label></label>
                                    </div>
                                    <div class="col-sm-12">
                                        <label style="font-size: 14px; font-weight: 700; color: black;">
                                            MANAGING PARTNER</label>
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

                    <asp:HiddenField ID="hdnAnnexure1PopUpContent" runat="server" Value="" />
                    <asp:HiddenField ID="hdnAnnexure2PopUpContent" runat="server" Value="" />
                    <asp:HiddenField ID="hdnAnnexure3PopUpContent" runat="server" Value="" />
                    <asp:HiddenField ID="hdnAddress" runat="server" Value="" />
                    <asp:HiddenField ID="hdnPhoneAndFaxNo" runat="server" Value="" />
                    <asp:HiddenField ID="hdnEmail" runat="server" Value="" />
                    <asp:HiddenField ID="hdnWebsite" runat="server" Value="" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal" id="mpeFrontPage">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always" Style="height: 100%;">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title bold popuptitle" style="color: #800b4f; margin: auto; text-align: center;"
                                id="H1"></h4>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true"
                                style="margin: -1rem 1rem -1rem 1rem;">
                                ×</button>
                        </div>
                        <div class="">
                            <div class="inner-container divContent" id="divFrontagePDF" runat="server">
                            </div>
                            <div class="row" style="padding-top: 10px;" id="divfrontpageFooter" runat="server">
                                <div class="col-sm-6" style="font-weight: 600; padding-top: 10px; border: 1px solid;">
                                    <div class="col-sm-12">
                                        <label style="font-size: large; font-weight: bolder; color: black;">
                                            For LONESTAR Industries</label>
                                    </div>
                                    <div class="col-sm-12">
                                        <asp:Label ID="lblFrontPageMarketingHead" CssClass="MarketingHead" Style="font-size: large; font-weight: bolder; color: black;"
                                            runat="server">
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-12">
                                        <label style="font-size: large; font-weight: bolder; color: black;">
                                            Sr.MANAGER
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-6 text-right" style="font-weight: 600; padding-top: 10px; border: 1px solid;">
                                    <div class="col-sm-12">
                                        <label style="font-size: large; font-weight: bolder; color: black;">
                                            For LONESTAR Industries</label>
                                    </div>
                                    <div class="col-sm-12">
                                        <label style="font-size: large; font-weight: bolder; color: black;">
                                            <asp:Label ID="lblFrontPageMD" runat="server"></asp:Label></label>
                                    </div>
                                    <div class="col-sm-12">
                                        <label style="font-size: large; font-weight: bolder; color: black;">
                                            MANAGING PARTNER
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        </div>
                        <div class="modal-footer">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal" id="mpeAnnexurte2">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always" Style="height: 100%;">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title bold popuptitle" style="color: #800b4f; margin: auto; text-align: center;"
                                id="H2"></h4>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true"
                                style="margin: -1rem 1rem -1rem 1rem;">
                                ×</button>
                        </div>
                        <div class="thirdPage">
                        </div>
                        <div class="modal-footer">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal" id="modelAddModule">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" Style="height: 100%;">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title bold popuptitle" style="color: #800b4f; margin: auto; text-align: center;"
                                id="tournamentNameTeam1"></h4>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true"
                                style="margin: -1rem 1rem -1rem 1rem;">
                                ×</button>
                        </div>
                        <div class="">
                            <div class="inner-container divContent" id="divContent" runat="server">
                                <div class="col-sm-12" style="padding-top: 30px;">
                                    <img src="../Assets/images/topstrrip.jpg" alt="" width="100%">
                                </div>
                                <div class="col-sm-12" style="padding-top: 10px;" id="trLevel4" runat="server">
                                    <div class="col-sm-4 popupcolumnhead" style="font-weight: 600; padding-top: 10px; border: 1px solid;">
                                    </div>
                                    <div class="col-sm-4 popupColumnOfferNo" style="font-weight: 600; padding-top: 10px; border: 1px solid;">
                                    </div>
                                    <div class="col-sm-4 popupOfferDate" style="font-weight: 600; padding-top: 10px; border: 1px solid;">
                                    </div>
                                </div>
                                <div id="gvcontent" class="col-sm-12 popupcontent" style="padding-top: 10px; padding-left: 20px;"
                                    runat="server">
                                </div>
                                <div class="col-sm-12" style="padding-top: 10px;" id="Div1" runat="server">
                                    <div class="col-sm-6" style="font-weight: 600; padding-top: 10px; border: 1px solid;">
                                        <div class="col-sm-12">
                                            <label style="font-size: large; font-weight: bold; color: black;">
                                                For LONESTAR Industries</label>
                                        </div>
                                        <div class="col-sm-12">
                                            <label class="MarketingHead" style="font-size: large; font-weight: bold; color: black;">
                                            </label>
                                        </div>
                                        <div class="col-sm-12">
                                            <label style="font-size: large; font-weight: bold; color: black;">
                                                Sr.MANAGER</label>
                                        </div>
                                    </div>
                                    <div class="col-sm-6 text-right" style="font-weight: 600; padding-top: 10px; border: 1px solid;">
                                        <div class="col-sm-12">
                                            <label style="font-size: large; font-weight: bold; color: black;">
                                                For LONESTAR Industries</label>
                                        </div>
                                        <div class="col-sm-12">
                                            <label style="font-size: large; font-weight: bold; color: black;">
                                                <asp:Label ID="lblMD" runat="server"></asp:Label></label>
                                        </div>
                                        <div class="col-sm-12">
                                            <label style="font-size: large; font-weight: bold; color: black;">
                                                MANAGING PARTNER</label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal" id="previewmodule">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title bold popuptitle" style="color: #800b4f; margin: auto; text-align: center;"
                        id="tournamentNameTeam"></h4>
                    <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true"
                        style="margin: -1rem 1rem -1rem 1rem;">
                        ×</button>
                </div>
                <div class="">
                    <div class="inner-container" runat="server">
                        <div class="col-sm-12" style="padding-top: 30px;">
                            <img src="../Assets/images/topstrrip.jpg" alt="" width="100%">
                        </div>
                        <div class="col-sm-12" style="padding-top: 10px;" id="Div2" runat="server">
                            <div class="col-sm-4 popupcolumnhead" style="font-weight: 600; padding-top: 10px; border: 1px solid;">
                            </div>
                            <div class="col-sm-4 popupColumnOfferNo" style="font-weight: 600; padding-top: 10px; border: 1px solid;">
                            </div>
                            <div class="col-sm-4 popupOfferDate" style="font-weight: 600; padding-top: 10px; border: 1px solid;">
                            </div>
                        </div>
                        <div id="Div3" class="col-sm-12 previewpopupcontent" style="padding-top: 10px; padding-left: 20px;"
                            runat="server">
                            <h4>Price Schedule:</h4>
                            <p style="margin-top: 10px !important; margin-bottom: 10px !important;">
                                Given below is the Schedule of prices for LONESTAR Make Metal Expansion Joints /
                                Bellows as per our Drawings
                            </p>
                            <table cellspacing="0" class="rgMasterTable" border="0" id="radgv_makeoffer_ctl00"
                                style="width: 100%; table-layout: auto; empty-cells: show; border: 1px solid; text-transform: none">
                                <colgroup>
                                    <col style="width: 180px">
                                    <col style="width: 180px">
                                    <col style="width: 180px">
                                    <col style="width: 180px">
                                    <col style="width: 180px">
                                    <col style="width: 180px">
                                </colgroup>
                                <thead style="background-color: whitesmoke; border: 1px solid;">
                                    <tr>
                                        <th scope="col" class="rgHeader" style="text-align: center;">SI.No
                                        </th>
                                        <th scope="col" class="rgHeader" style="text-align: center;">Item Name
                                        </th>
                                        <th scope="col" class="rgHeader" style="text-align: center;">Drawing Number
                                        </th>
                                        <th scope="col" class="rgHeader" style="text-align: center;">Size
                                        </th>
                                        <th scope="col" class="rgHeader" style="text-align: center;">Total Price
                                        </th>
                                        <th scope="col" class="rgHeader" style="text-align: center;">Quantity
                                        </th>
                                    </tr>
                                </thead>
                                <tbody id="popuptbody">
                                    <tr class="rgRow" id="popuptr" style="width: 180px; text-align: center;">
                                        <td>
                                            <span id="popupsno">1</span>
                                        </td>
                                        <td>
                                            <span id="popupItemname"></span>
                                        </td>
                                        <td>
                                            <span id="popupDrawingnumber"></span>
                                        </td>
                                        <td>
                                            <span id="popupSize">1000 ID</span>
                                        </td>
                                        <td>
                                            <span id="popupcost"></span>
                                        </td>
                                        <td>
                                            <span id="popupqty"></span>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="col-sm-12" style="padding-top: 100px;" id="Div4" runat="server">
                            <div class="col-sm-6" style="font-weight: 600; padding-top: 10px; border: 1px solid;">
                                Footer will come here
                            </div>
                            <div class="col-sm-6" style="font-weight: 600; padding-top: 10px; border: 1px solid;">
                                Footer will come here
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
    </div>



</asp:Content>
