<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="SupplierPO.aspx.cs" Inherits="Pages_SupplierPO" ClientIDMode="Predictable"
    EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function deleteConfirmSPODID(SPODID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Item will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deletegvrowSPODID', SPODID);
            });
            return false;
        }

        function deleteConfirmOtherCharges(SPOOCDID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Item will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deletegvRowotherCharges', SPOOCDID);
            });
            return false;
        }

        function deleteConfirmTaxDetailsCharges(SPOTDID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Item will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deletegvRowtaxCharges', SPOTDID);
            });
            return false;
        }

        function CancelConfirmSPODID(SPODID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Item will be Cancel permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Cancel it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('CancelgvrowSPODID', SPODID);
            });
            return false;
        }

        function ShowViewPopUp() {
            $('#mpeShowDocument').modal({
                backdrop: 'static'
            });
            return false;
        }

        function ShowInwardPopUp() {
            $('#mpeInwardHeader').modal('show');
            return false;
        }

        function ShowPOInVoicePopUp() {
            $('#mpeInwardHeader').modal('hide');
            $('#mpePOInVoicePopUp').modal('show');
            return false;
        }

        //mpePOInVoicePopUp

        //        function ShowAdditemdetailspopup() {
        //            $('.divInput').css("display", "block");
        //            return false;
        //        }

        function deleteConfirm(SPOID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Material will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deletegvrow', SPOID);
            });
            return false;
        }

        function MandatoryGradeThicknessCheck() {
            if ($('#ContentPlaceHolder1_ddlMaterialCategory').val() == '1') {
                if ($('#ContentPlaceHolder1_hdnSPODID').val() != '0') {
                    $('#ContentPlaceHolder1_ddlmaterialGrdae').removeClass('mandatoryfield');
                    $('#ContentPlaceHolder1_ddlThickness').removeClass('mandatoryfield');
                }
                else if ($('#ContentPlaceHolder1_hdnSPODID').val() == '0') {
                    $('#ContentPlaceHolder1_ddlmaterialGrdae').addClass('mandatoryfield');
                    $('#ContentPlaceHolder1_ddlThickness').addClass('mandatoryfield');
                }
                $('#ContentPlaceHolder1_ddlMaterialType').addClass('mandatoryfield')
            }
            else if ($('#ContentPlaceHolder1_ddlMaterialCategory').val() == '7') {
                if ($('#ContentPlaceHolder1_hdnSPODID').val() != '0') {
                    $('#ContentPlaceHolder1_ddlmaterialGrdae').removeClass('mandatoryfield');
                    $('#ContentPlaceHolder1_ddlThickness').removeClass('mandatoryfield');
                }
                else if ($('#ContentPlaceHolder1_hdnSPODID').val() == '0') {
                    $('#ContentPlaceHolder1_ddlmaterialGrdae').addClass('mandatoryfield');
                    $('#ContentPlaceHolder1_ddlThickness').addClass('mandatoryfield');
                }
            }
        }

        function SaveIndent() {
            MandatoryGradeThicknessCheck();
            var mancheck = Mandatorycheck('ContentPlaceHolder1_divAddItems');
            if (mancheck == false) return false;
            var mtfid = '';
            var mtfidsval = '';
            $('#ContentPlaceHolder1_divMTFields').find('input[type="text"]').each(function (index) {
                debugger;
                mtfid = mtfid + $(this).attr('id').split(/[\s_]+/).pop() + ',';
                mtfidsval = mtfidsval + $(this).val() + ',';
            });
            $('#ContentPlaceHolder1_hdn_MTFIDS').val(mtfid.replace(/.$/, ""));
            $('#ContentPlaceHolder1_hdn_MTFIDsValue').val(mtfidsval.replace(/.$/, ""));
        }

        function dynamicvalueretain() {
            var i = 0;
            $('#ContentPlaceHolder1_hdn_MTFIDsValue').val().split(',').forEach(function (arrval) {
                $($('#ContentPlaceHolder1_divMTFields').find('input[type="text"]')[i]).val(arrval);
                i++;
            });
        }

        function checkAll(ele) {
            debugger;
            if ($(ele).is(":checked")) {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', true);
            }
            else {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', false);
            }
        }

        function ShowAddPopUp() {
            $('#mpeView').show();
            $('div').removeClass('modal-backdrop in');
            return false;
        }

        function ShowTaxPopUp() {
            $('#mpeAddtax').modal('show');
            $('div').removeClass('modal-backdrop in');
            return false;
        }

        function HideAddTaxPopUp() {
            $('#mpeAddtax').modal('hide');
            $('div').removeClass('modal-backdrop in');
            return false;
        }

        function HideAddPopUp() {
            $('#mpeView').hide();
            return false;
        }

        function OpenTab(Name) {
            var names = ["OtherCharges", "Tax"];
            var text = document.getElementById('<%=Tax.ClientID %>');

            for (var i = 0; i < names.length; i++) {
                if (Name == names[i]) {
                    var a = text.id.replace('Tax', Name);
                    document.getElementById(a).style.display = "block";
                    document.getElementById('li' + names[i]).className = "active";
                }
                else {
                    var b = text.id.replace('Tax', names[i]);
                    document.getElementById(b).style.display = "none";
                    document.getElementById('li' + names[i]).className = "";
                }
            }
            if (Name == 'Tax') {
                $('#liOtherCharges').removeClass('active');
                $('#liOtherCharges a').removeClass('active');
                $('#liTax a').addClass('active');
                $('#liTax').addClass('active');
            }
            else {
                $('#liTax').removeClass('active');
                $('#liTax a').removeClass('active');
                $('#liOtherCharges a').addClass('active');
                $('#liOtherCharges').addClass('active');
            }
        }

        function POPrint(OtherCharges, Taxes, Currencyname) {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');
            var Content = $('#ContentPlaceHolder1_divPurchaseOrder_PDF').html();

            var FooterContent = $('#ContentPlaceHolder1_divFooterContent_p').html();

            var MainContent = $('#divMainContent_p').html();
            var QualityRequirtments = $('#divQualityRequirtments').html();
            var TermsAndCondtions = $('#divtermsandcondtions').html();

            var RowLen = $('#ContentPlaceHolder1_gvPurchaseOrder_P').find('tr').length;

            var PurchaseOrderDiscription = $('#divPurchaseorderDescription_p').html();
            var taxdetails = $('#divtaxdetails_p').html();

            var oc = [];
            var Tc = [];

            oc = OtherCharges.split('/');
            Tc = Taxes.split('/');

            winprint.document.write("<html><head><title>");
            ///   winprint.document.write("Offer");
            winprint.document.write("</title>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='../Assets/css/ep-style.css'/>");
            winprint.document.write("<link rel='stylesheet' href='../Assets/css/main.css' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet'  href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/printg.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/images/topstrrip.jpg") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<style type='text/css'> #ContentPlaceHolder1_gvPurchaseOrder_P_0 th { background-color: white ! important;color: #000;font-weight: 600; } label{ font-weight: 900; } table{ border: solid; } .header{ width: 205mm;left: 2mm;border: 0px solid #000; } </style>");

            winprint.document.write("</head><body>");

            //  for (var k = 1; k <= parseInt(page); k++) {
            winprint.document.write("<div class='print-page' style='position:fixed;border:1px solid #000;width:200mm;height:287mm;margin-top:5mm;margin-bottom:5mm;margin-left:5mm;margin-right:5mm'></div>");
            winprint.document.write("<table style='border:none;'>");

            winprint.document.write("<thead><tr><td>");
            winprint.document.write("<div class='col-sm-12 print-generateoffer' style='text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;height:28mm;width: 200mm;margin-top: 6mm;margin-left: 5mm;margin-right: 5mm;margin-bottom: 5mm;float: left;'>");

            winprint.document.write("<div class='col-sm-12 padding0' id='divLSERPLogo' style=''>");
            // winprint.document.write("<img src='" + topstrip + "' alt='' height='100px;' style='object-fit:contain;width:100%;display:none'>");
            winprint.document.write("<div class='row' style='position:fixed;width:200mm;'>");
            winprint.document.write("<div class='col-sm-3'>");
            winprint.document.write("<img src='../Assets/images/lonestar.jpeg' alt='lonestar-image' width='90px'>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-6 text-center'>");
            winprint.document.write("<h3 style='font-weight:600;font-size:20px;color:#000;font-family: Times New Roman;display: contents;'>" + $('#ContentPlaceHolder1_lblCompanyName_h').text() + "</h3>");
            winprint.document.write("<span style='font-weight:600;font-size:11px ! important;font-family: Times New Roman;'>" + $('#ContentPlaceHolder1_lblFormarcompanyName_h').text() + "</span>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;width: 100%;padding:0 0px'>" + $('#ContentPlaceHolder1_lblAddress_h').text() + "</p>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + $('#ContentPlaceHolder1_lblPhoneAndFaxNo_h').text() + "</p>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + $('#ContentPlaceHolder1_lblEmail_h').text() + "</p>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + $('#ContentPlaceHolder1_lblwebsite_h').text() + "</p>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-3'>");
            winprint.document.write("<img src='../Assets/images/iso.jpeg' alt='lonestar-image' height='90px'>");

            winprint.document.write("</div>");
            winprint.document.write("</div></div>");
            winprint.document.write("</td></tr></thead>");

            winprint.document.write("<tbody><tr><td>");

            winprint.document.write("<div class='' style='width: 190mm;margin: 0mm 10mm;'>");
            //if (k == 1) {
            winprint.document.write(MainContent);
            // }

            // if (page == 1 || page == 2) {
            //   if (k == 1) {

            winprint.document.write("<div style='width:100%;padding-top: 10px;'>");
            winprint.document.write("<table class='table table-hover table-bordered medium' cellspacing='0' rules='all' border='1' id='ContentPlaceHolder1_gvPurchaseOrder_P_" + 0 + "' style='border-collapse:collapse;'>");
            winprint.document.write("<thead>");
            winprint.document.write($('#ContentPlaceHolder1_gvPurchaseOrder_P').find('tr:first').html());
            winprint.document.write("</thead>");

            winprint.document.write("<tbody>");

            for (var i = 1; i < parseInt(RowLen); i++)
                winprint.document.write("<tr align='center'>" + $('#ContentPlaceHolder1_gvPurchaseOrder_P').find('tr')[i].innerHTML + "</tr>");

            if (OtherCharges != "") {
                $.each(oc, function (index, value) {
                    winprint.document.write("<tr align='center'><td colspan='7' style='text-align:left;color: #000 !important;'>" + value.split('#')[0] + " </td><td style='text-align:end;color: #000 !important;'> " + value.split('#')[1] + " </td></tr>");
                });
            }

            winprint.document.write("<tr align='center'><td colspan='7' style='text-align:left;color: #000 !important;'> Sub Total: </td><td style='text-align:end;color: #000 !important;'> " + $('#ContentPlaceHolder1_lblSubTotal_P').text() + " </td></tr>");

            if (Taxes != "") {
                $.each(Tc, function (index, value) {
                    winprint.document.write("<tr align='center'><td colspan='7' style='text-align:left;color: #000 !important;'>" + value.split('#')[0] + "  </td><td style='text-align:end;color: #000 !important;'> " + value.split('#')[1] + " </td></tr>");
                });
            }

            winprint.document.write("</tbody>");
            winprint.document.write("</table>");

            winprint.document.write("<div style='text-align:end;padding-top: 10px;'>");
            winprint.document.write("<label style='font-size: 15px ! important;'> Total Amount</label> <label style='padding-right: 20px;'> : </label><label style='color: #000 !important;font-size: 15px !important;'>" + $('#ContentPlaceHolder1_lblTotalAmount_P').text() + "</label>");
            winprint.document.write("</div>");

            winprint.document.write("<div class='div-sec p-t-10'>");
            winprint.document.write("<label style='width:15%;'>Rupees in words:</label>" + "<sapn>" + $('#ContentPlaceHolder1_lblAmonutInWords_P').text() + "</span>");
            winprint.document.write("</div>");
            winprint.document.write("</div>");

            // }
            // }

            //if (page == 3) {
            //    winprint.document.write("<div style='width:96%;padding-top: 10px;'>");
            //    winprint.document.write("<table class='table table-hover table-bordered medium' cellspacing='0' rules='all' border='1' id='ContentPlaceHolder1_gvPurchaseOrder_P_" + k + "' style='border-collapse:collapse;'>");
            //    winprint.document.write("<tbody>");

            //    if (k == 1) {
            //        for (var i = 0; i < 6; i++)
            //            winprint.document.write("<tr align='center'>" + $('#ContentPlaceHolder1_gvPurchaseOrder_P').find('tr')[i].innerHTML + "</tr>");
            //    }

            //    else if (k == 2) {

            //        winprint.document.write("<tr align='center'>" + $('#ContentPlaceHolder1_gvPurchaseOrder_P').find('tr')[0].innerHTML + "</tr>");
            //        for (var i = 6; i < parseInt(RowLen); i++)
            //            winprint.document.write("<tr align='center'>" + $('#ContentPlaceHolder1_gvPurchaseOrder_P').find('tr')[i].innerHTML + "</tr>");

            //        if (OtherCharges != "") {
            //            $.each(oc, function (index, value) {
            //                winprint.document.write("<tr align='center'><td colspan='7' style='text-align:left;color: #000 !important;'>" + value.split('#')[0] + " </td><td style='text-align:end;color: #000 !important;'> " + value.split('#')[1] + " </td></tr>");
            //            });
            //        }

            //        winprint.document.write("<tr align='center'><td colspan='7' style='text-align:left;color: #000 !important;'> Sub Total: </td><td style='text-align:end;color: #000 !important;'> " + $('#ContentPlaceHolder1_lblSubTotal_P').text() + " </td></tr>");

            //        if (Taxes != "") {
            //            $.each(Tc, function (index, value) {
            //                winprint.document.write("<tr align='center'><td colspan='7' style='text-align:left;color: #000 !important;'>" + value.split('#')[0] + "  </td><td style='text-align:end;color: #000 !important;'> " + value.split('#')[1] + " </td></tr>");
            //            });
            //        }
            //    }

            //    winprint.document.write("</tbody>");
            //    winprint.document.write("</table>");

            //    if (k == 2) {
            //        winprint.document.write("<div style='text-align:end;padding-top: 10px;'>");
            //        winprint.document.write("<label style='font-size: 15px ! important;'> Total Amount</label> <label style='padding-right: 20px;'> : </label><label style='color: #000 !important;font-size: 15px !important;'>" + $('#ContentPlaceHolder1_lblTotalAmount_P').text() + "</label>");
            //        winprint.document.write("</div>");

            //        winprint.document.write("<div class='div-sec p-t-10'>");
            //        winprint.document.write("<label style='width:15%;'>Rupees in words:</label>" + "<sapn>" + $('#ContentPlaceHolder1_lblAmonutInWords_P').text() + "</span>");
            //        winprint.document.write("</div>");
            //    }

            //    winprint.document.write("</div>");
            //}

            //if (parseInt(page) == 1) {
            winprint.document.write(QualityRequirtments);

            if (Currencyname == "INR")
                winprint.document.write(TermsAndCondtions);

            // }

            //if (page == 2 && k == 2) {
            //    winprint.document.write("<div class='row'> <div class='col-sm-6'> <label> Indent No </label> <label> : </label> <span>" + $('#ContentPlaceHolder1_lblIndentNo_P').text() + "</span> </div> <div class='col-sm-6' style='text-align:end;'> <label> PO Number </label> <label> : </label> <span>" + $('#ContentPlaceHolder1_lblPONumber_P').text() + "</span></div></div>");
            //    winprint.document.write("<div class='row'> <div class='col-sm-6'> <label>RFP No</label><label>:</label> <span>" + $('#ContentPlaceHolder1_lblRFPNo_P').text() + "</span></div>");
            //    winprint.document.write("<div class='col-sm-6' style='text-align:end;'> <label> PO Date </label> <label> : </label> <span>" + $('#ContentPlaceHolder1_lblPOSharedDate_P').text() + "</span> </div></div>");
            //    winprint.document.write(QualityRequirtments);
            //    if (Currencyname == "INR")
            //        winprint.document.write(TermsAndCondtions);
            //}

            //if (page == 3 && k == 3) {
            //    winprint.document.write("<div class='row'> <div class='col-sm-6'> <label> Indent No </label> <label> : </label> <span>" + $('#ContentPlaceHolder1_lblIndentNo_P').text() + "</span> </div> <div class='col-sm-6' style='text-align:end;'> <label> PO Number </label> <label> : </label> <span>" + $('#ContentPlaceHolder1_lblPONumber_P').text() + "</span></div></div>");
            //    winprint.document.write("<div class='row'> <div class='col-sm-6'> <label>RFP No</label><label>:</label> <span>" + $('#ContentPlaceHolder1_lblRFPNo_P').text() + "</span></div>");
            //    winprint.document.write("<div class='col-sm-6' style='text-align:end;'> <label> PO Date </label> <label> : </label> <span>" + $('#ContentPlaceHolder1_lblPOSharedDate_P').text() + "</span> </div></div>");
            //    winprint.document.write(QualityRequirtments);
            //    if (Currencyname == "INR")
            //        winprint.document.write(TermsAndCondtions);
            //}

            winprint.document.write("</div>");

            winprint.document.write("</td></tr></tbody>");

            winprint.document.write("<tfoot><tr><td>");
            winprint.document.write("<div style='height:35mm;'>");
            winprint.document.write(FooterContent);
            winprint.document.write("</div>");
            winprint.document.write("</td></tr></tfoot>");

            winprint.document.write("</table>");
            winprint.document.write("</div>");
            //}

            //     if (Currencyname != "INR") {
            //    winprint.document.write("<div class='print-page'>");
            //    winprint.document.write("<table>");

            //    winprint.document.write("<thead><tr><td>");
            //    winprint.document.write("<div class='col-sm-12 print-generateoffer' style='text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;height:102px'>");

            //    winprint.document.write("<div class='col-sm-12 padding0' id='divLSERPLogo1' style='margin:5px auto;display:block;'>");           
            //    winprint.document.write("<div class='row' style='position:fixed;width:200mm;left:5mm;'>");
            //    winprint.document.write("<div class='col-sm-3'>");
            //    winprint.document.write("<img src='../Assets/images/lonestar.jpeg' alt='lonestar-image' width='90px'>");
            //    winprint.document.write("</div>");
            //    winprint.document.write("<div class='col-sm-6 text-center'>");
            //    winprint.document.write("<h3 style='font-weight:600;font-size:24px;font-style:italic;color:#000;font-family: Arial;display: contents;'>LONESTAR</h3>");
            //    winprint.document.write("<span style='font-weight:600;font-size:24px ! important;font-family: Times New Roman;'>INDUSTRIES</span>");
            //    winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;width: 100%;padding:0 0px'>" + $('#ContentPlaceHolder1_lblAddress_h').text() + "</p>");
            //    winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + $('#ContentPlaceHolder1_lblPhoneAndFaxNo_h').text() + "</p>");
            //    winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + $('#ContentPlaceHolder1_lblEmail_h').text() + "</p>");
            //    winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + $('#ContentPlaceHolder1_lblwebsite_h').text() + "</p>");
            //    winprint.document.write("</div>");
            //    winprint.document.write("<div class='col-sm-3'>");
            //    winprint.document.write("<img src='../Assets/images/iso.jpeg' alt='lonestar-image' height='90px'>");

            //    winprint.document.write("</div>");
            //    winprint.document.write("</div></div>");
            //    winprint.document.write("</td></tr></thead>");

            //    winprint.document.write("<tbody><tr><td>");

            //    winprint.document.write("<div class='page_generateoffer' style='width:190mm;margin:5mm'>");

            //    winprint.document.write("<table class='table table-hover table-bordered medium' cellspacing='0' rules='all' border='1' id='ContentPlaceHolder1_gvForignPOTC_P_" + 1 + "' style='border-collapse:collapse;'>");
            //    winprint.document.write("<tbody>");

            //    for (var i = 0; i < $('#ContentPlaceHolder1_gvForignPOTC_P').find('tr').length; i++)
            //        winprint.document.write("<tr align='center'>" + $('#ContentPlaceHolder1_gvForignPOTC_P').find('tr')[i].innerHTML + "</tr>");

            //    winprint.document.write("</tbody>");
            //    winprint.document.write("</table>");


            //    winprint.document.write("</div>");

            //    winprint.document.write("</td></tr></tbody>");

            //    winprint.document.write("<tfoot><tr><td>");
            //    winprint.document.write("<div style='height:30mm;'>");
            //    winprint.document.write(FooterContent);
            //    winprint.document.write("</div>");
            //    winprint.document.write("</td></tr></tfoot>");

            //    winprint.document.write("</table>");
            //    winprint.document.write("</div>");
            //}

            winprint.document.write("</body></html>");

            setTimeout(function () {
                winprint.document.close();
                winprint.focus();
                winprint.print();
                // winprint.close();
            }, 1000);
        }

        function PrintPurchaseOrder(index) {
            __doPostBack("PrintPO", index);
            return false;
        }

        function checkAllItems(ele) {
            debugger;
            if ($(ele).is(":checked")) {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', true);

                $(ele).closest('table').find('[type="text"]').addClass("mandatoryfield");
                $(ele).closest('table').find('[type="text"]').before('<span class="textboxmandatory" style="color: red;">*</span>');
            }
            else {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', false);
                $(ele).closest('table').find('[type="text"]').removeClass("mandatoryfield");
                $(ele).closest('table').find('[type="text"]').closest('td').find('.textboxmandatory').remove()
            }
        }

        function ShowTCPopUp() {
            $('#mpeTermsAndConditions').show();
            // $('div').removeClass('modal-backdrop in');
            return false;
        }

        function HideTCPopUp() {
            $('#mpeTermsAndConditions').hide();
            // $('div').removeClass('modal-backdrop in');
            return false;
        }

        function ValidateCheck() {
            if ($('#ContentPlaceHolder1_gvPOTermsAndConditions').find('[type="checkbox"]:checked').not('#ContentPlaceHolder1_gvPOTermsAndConditions_chkall').length > 0) {
                return true;
            }
            else {
                ErrorMessage('Error', 'No TC Selected')
                hideLoader();
                return false;
            }
        }

        function POAMD(index) {
            var s = $('#ContentPlaceHolder1_txtamdreason').val();
            if (s != '') {
                swal({
                    title: "Are you sure Want To Change The Amendments?",
                    text: "If Yes, Change Version",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "Yes, Amendments it!",
                    closeOnConfirm: false
                }, function () {
                    //showLoader();
                    __doPostBack('POAMD', index);
                });
                return false;
            }
            else {
                ErrorMessage('Error', 'Please Enter Amd Reason Below of the page');
                return false;
            }
        }

        function ValidateRequiredWeightIndentQty(ele) {
            var AvailQty = $(ele).closest('td').find('[type="hidden"]').val();
            var permission = parseInt($(ele).closest('td').find('.allow').text());

            if (permission == "0") {
                if (parseFloat($(ele).val()) < parseFloat(AvailQty)) {
                    ErrorMessage('Error', 'Entered Qty Should Not Less Than Purchase Indent Qty');
                    $(ele).val(AvailQty);
                }
            }

            if (parseFloat($(ele).val()) <= 0) {
                ErrorMessage('Error', 'Zero Qty Not Allowed');
                $(ele).val(AvailQty);
            }
			
			
            if (parseFloat($(ele).val()) > parseFloat(AvailQty)) {
                ErrorMessage('Error', 'Entered Qty Should Not Greater Than Purchase Indent Qty');
                $(ele).val(AvailQty);
            }
			
            return false;
        }

        function DeleteSupplierPO(index) {
            swal({
                title: "Are you sure Want To Delete The PO?",
                text: "If Yes, Delete",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes,Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('DeleteSPO', index);
            });
            return false;
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
                                    <h3 class="page-title-head d-inline-block">Supplier PO Details</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Purchase</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Supplier PO</li>
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
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Category Name</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlMaterialCategory" runat="server" ToolTip="Select Material Category Name"
                                            Width="70%" AutoPostBack="true" OnSelectedIndexChanged="ddlMaterialCategory_OnSelectIndexChanged"
                                            CssClass="form-control">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Suplier Name</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlSuplierName" runat="server" AutoPostBack="true" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlSuplierName_OnSelectIndexChanged" Width="70%" ToolTip="Select Supplier Name">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <%-- <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                           <label class="form-label">
                                            RFP No</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                         <asp:DropDownList ID="ddlRFPNo" runat="server" AutoPostBack="true" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlRFPNo_SelectIndexChanged" Width="70%" ToolTip="Select RFP No">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>--%>
                                <%--<div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Item Name</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlItemName" runat="server" ToolTip="Select Item Name" Width="70%"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlItemName_OnSelectIndexChanged"
                                            CssClass="form-control mandatoryfield">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>--%>
                            </div>
                        </div>
                        <div class="col-sm-12 text-center p-t-10" id="divAddNew" runat="server">
                            <asp:LinkButton ID="btnAddNew" runat="server" Text="Add New" CssClass="btn btn-success add-emp"
                                OnClick="btnAddNew_Click"></asp:LinkButton>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12">
                                    <asp:Label ID="lblSPOnumber" class="form-label" Style="color: crimson; font-size: large;"
                                        runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4 text-right">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Issue Date</label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtIssueDate" runat="server" CssClass="form-control mandatoryfield datepicker"
                                                ToolTip="Enter Issue Date" autocomplete="nope" placeholder="Enter Issue Date">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label">
                                                QuateReference Number</label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtQuateReferenceNumber" runat="server" CssClass="form-control"
                                                ToolTip="Enter Quate Reference Number" autocomplete="nope" placeholder="Enter Quate reference Number">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label">
                                                Delivery Date</label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtDeliveryDate" runat="server" CssClass="form-control datepicker"
                                                AutoComplete="off" ToolTip="Enter Delivery Date" placeholder="Enter Delivery Date">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Payment Duration
                                            </label>
                                        </div>
                                        <div>
                                            <%--  <asp:TextBox ID="txtPayment" runat="server" CssClass="form-control" AutoComplete="off"
                                                ToolTip="Enter Payment Duration" placeholder="Enter Payment Duration">
                                            </asp:TextBox>--%>
                                            <asp:DropDownList ID="ddlPOPaymentDays" runat="server" ToolTip="Select Payment Name" Width="70%"
                                                AutoPostBack="false"
                                                CssClass="form-control mandatoryfield">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label">
                                                Note</label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" Rows="4" CssClass="form-control" AutoComplete="off"
                                                ToolTip="Enter Notes" placeholder="Enter Notes">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label">
                                                Payment Terms</label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtEnclosure" runat="server" CssClass="form-control" AutoComplete="on"
                                                ToolTip="Enter Enclosure" placeholder="Enter Enclosure">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Location Name</label>
                                        </div>
                                        <div>
                                            <asp:DropDownList ID="ddlLocationName" runat="server" ToolTip="Select Location Name"
                                                CssClass="form-control mandatoryfield">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Currency Master</label>
                                        </div>
                                        <div>
                                            <asp:DropDownList ID="ddlCurrencyname" runat="server" ToolTip="Select Currency Name"
                                                CssClass="form-control mandatoryfield">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-8">

                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Amendment Reason
                                            </label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtamdreason_edit" runat="server" CssClass="form-control">  </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>

                                <%--  <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label">
                                                Tax</label>
                                        </div>
                                        <div class="text-left">
                                            <asp:DropDownList ID="ddlTaxName" runat="server" ToolTip="Select Tax" CssClass="form-control">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label">
                                                Value</label>
                                        </div>
                                        <div class="text-left">
                                            <asp:TextBox ID="txtValue" runat="server" CssClass="form-control" AutoComplete="off"
                                                Onkeypress="return fnAllowNumeric();" ToolTip="Enter Value" placeholder="Enter Value">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>--%>
                                <%-- <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Fright Charges</label>
                                        </div>
                                        <div class="text-left">
                                            <asp:TextBox ID="txtrFrightCharges" runat="server" CssClass="form-control mandatoryfield"
                                                AutoComplete="off" onkeypress="return validationDecimal(this);" ToolTip="Enter Fright Charges"
                                                placeholder="Enter Fright Charges">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Packing And Forwarding Charges
                                            </label>
                                        </div>
                                        <div class="text-left">
                                            <asp:TextBox ID="txtPackingAndForwarding" runat="server" CssClass="form-control mandatoryfield"
                                                onkeypress="return validationDecimal(this);" AutoComplete="off" ToolTip="Enter packing And Forwarding Charges"
                                                placeholder="Enter packing And Forwarding Charges">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label">
                                                CGST(%)</label>
                                        </div>
                                        <div class="text-left">
                                            <asp:TextBox ID="txtCGST" runat="server" CssClass="form-control" AutoComplete="off"
                                                onkeypress="return validationDecimal(this);" ToolTip="Enter CGST Percentage"
                                                placeholder="Enter CGST Percentage">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label">
                                                SGST (%)
                                            </label>
                                        </div>
                                        <div class="text-left">
                                            <asp:TextBox ID="txtSGST" runat="server" CssClass="form-control" AutoComplete="off"
                                                onkeypress="return validationDecimal(this);" ToolTip="Enter SGST Percentage"
                                                placeholder="Enter SGST Percentage">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label">
                                                IGST (%)
                                            </label>
                                        </div>
                                        <div class="text-left">
                                            <asp:TextBox ID="txtIGST" runat="server" CssClass="form-control" AutoComplete="off"
                                                onkeypress="return validationDecimal(this);" ToolTip="Enter SGST Percentage"
                                                placeholder="Enter IGST Percentage">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>--%>
                                <div class="col-sm-12 p-t-20">
                                    <asp:LinkButton ID="btnSave" runat="server" Text="Save" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divInput');"
                                        OnClick="btnSave_Click" CssClass="btn btn-cons btn-save AlignTop"></asp:LinkButton>
                                    <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" CausesValidation="False"
                                        OnClick="btncancel_Click" CssClass="btn btn-cons btn-danger AlignTop btnCancel"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="col-md-12 row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                                    <asp:Label ID="lbltitle" runat="server" Text="Supplier PO Details"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvSupplierPODetails" runat="server" AutoGenerateColumns="False"
                                                OnRowCommand="gvSupplierPODetails_OnRowCommand" ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found"
                                                OnRowDataBound="gvSupplierPODetails_OnRowDataBound" CssClass="table table-hover table-bordered medium orderingfalse"
                                                DataKeyNames="SPOID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SPO Number" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSPONumber" runat="server" Text='<%# Eval("SPONumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="PO Status" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPOStatus" runat="server" Text='<%# Eval("POStatusWord")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Issue Date" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIssueDate" runat="server" Text='<%# Eval("IssueDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delivery Date" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDeliveryDate" runat="server" Text='<%# Eval("DeliveryDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quate Reference Number">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQuateReferenceNumber" runat="server" Text='<%# Eval("QuateReferenceNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Add Tax">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnAddTax" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="AddTax">  <img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="PDF">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnPDF" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                OnClientClick='<%# string.Format("return PrintPurchaseOrder({0});",((GridViewRow) Container).RowIndex) %>'>  <img src="../Assets/images/pdf.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                        HeaderText="Add PO">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnAdd" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="AddSPO"><img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                        HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="EditSP"><img src="../Assets/images/edit-ec.png"/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                        HeaderText="Add TC">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnAddTC" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="AddTC"><img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                        HeaderText="PO InVoice">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnPOInvoice" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="AddPOInvoice"><img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                        HeaderText="PO Amendments">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnPOAmendments" runat="server"
                                                                OnClientClick='<%# string.Format("return POAMD({0});",((GridViewRow) Container).RowIndex) %>'
                                                                CssClass="btn btn-cons btn-success"
                                                                Text="AMD"> </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                        HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnspodelete" runat="server"
                                                                OnClientClick='<%# string.Format("return DeleteSupplierPO({0});",((GridViewRow) Container).RowIndex) %>'
                                                                CssClass="btn btn-cons btn-success"
                                                                Text="AMD"><img src="../Assets/images/del-ec.png" /> </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnSPOID" runat="server" Value="0" />
                                        </div>
                                        <div class="col-sm-12 p-t-20 text-center">
                                            <label>Amendment Reason </label>
                                            <asp:TextBox ID="txtamdreason" Rows="3" TextMode="MultiLine" runat="server" CssClass="form-control">  </asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div id="divPurchaseOrder_PDF" class="p-page" runat="server" style="display: none;">
                            <table>
                                <thead>
                                    <tr>
                                        <td>
                                            <div class='row header-space' style='text-align: center; font-size: 20px; font-weight: bold; padding-left: 0; padding-right: 0; height: 120px'>
                                                <div class='header' style='border-bottom: 1px solid;'>
                                                    <div>
                                                        <div class='row padding0' id='divLSERPLogo' style='margin: 0px auto; display: block; padding: 5px 0px;'>
                                                            <img src='" + topstrip + "' alt='' height='100px;' style='object-fit: contain; width: 100%; display: none'>
                                                            <div class='row'>

                                                                <div class='col-sm-3'>

                                                                    <img src='../Assets/images/lonestar.jpeg' alt='lonestar-image' width='90px'>
                                                                </div>

                                                                <div class='col-sm-6 text-center'>

                                                                    <h3 style='font-weight: 600; font-size: 24px; font-style: italic; color: #000; font-family: Arial; display: contents;'>
                                                                        <asp:Label ID="lblCompanyName_h" runat="server"></asp:Label>
                                                                    </h3>

                                                                    <span style='font-weight: 600; font-size: 24px ! important; font-family: Times New Roman;'>
                                                                        <asp:Label ID="lblFormarcompanyName_h" runat="server"></asp:Label>
                                                                    </span>
                                                                    <p style='font-weight: 500; color: #000; width: 103%;'>
                                                                        <asp:Label ID="lblAddress_h" runat="server"></asp:Label>
                                                                    </p>

                                                                    <p style='font-weight: 500; color: #000'>
                                                                        <asp:Label ID="lblPhoneAndFaxNo_h" runat="server"></asp:Label>
                                                                    </p>

                                                                    <p style='font-weight: 500; color: #000'>
                                                                        <asp:Label ID="lblEmail_h" runat="server"></asp:Label>
                                                                    </p>

                                                                    <p style='font-weight: 500; color: #000'>
                                                                        <asp:Label ID="lblwebsite_h" runat="server"></asp:Label>
                                                                    </p>
                                                                </div>
                                                                <div class='col-sm-3'>

                                                                    <img src='../Assets/images/iso.jpeg' alt='lonestar-image' height='90px'>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                            <div id="divMainContent_p">
                                                <div>
                                                    <div class="text-center div-sec" style="height: 31px; padding-top: 4px; border: 3px solid; border-radius: 10px; margin-left: 39%; margin-right: 39%;">
                                                        <label style="font-weight: 700; font-size: 15px ! important;">
                                                            PURCHASE ORDER</label>
                                                    </div>
                                                    <div class="row div-sec" style="padding-right: 15px; padding-top: 12px;">
                                                        <div class="col-sm-6" style="padding-top: 10px;">
                                                            <label style="font-size: 12px; font-weight: 700">
                                                                To:</label>
                                                            <div class="row">
                                                                <asp:Label ID="lblSupplierName_P" Style="font-weight: bold;" runat="server"></asp:Label>
                                                                <asp:Label ID="lblsupplierAddress_P" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="row">
                                                                <label style="width: 15%;">
                                                                    PH</label>
                                                                <label style="width: 3%;">
                                                                    :</label>
                                                                <asp:Label ID="lblPhoneNumber_P" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="row">
                                                                <label style="width: 15%;">
                                                                    Email</label>
                                                                <label style="width: 3%;">
                                                                    :</label>
                                                                <asp:Label ID="lblEmail_P" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="row" style="padding-top: 10px;">
                                                                <label style="width: 15%;">
                                                                    GST No</label>
                                                                <label style="width: 3%;">
                                                                    :</label>
                                                                <asp:Label ID="lblSupGSTNumber_P" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6" style="padding-left: 8%;">
                                                            <div class="row">
                                                                <label style="width: 25%;">
                                                                    PO No</label>
                                                                <label style="width: 3%;">
                                                                    :</label>
                                                                <asp:Label ID="lblPONumber_P" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="row">
                                                                <label style="width: 25%;">
                                                                    PO Date</label>
                                                                <label style="width: 3%;">
                                                                    :</label>
                                                                <asp:Label ID="lblPOSharedDate_P" runat="server"></asp:Label>
                                                            </div>

                                                            <div>
                                                                <label style="width: 25%;">
                                                                    AMD</label>
                                                                <label style="width: 3%;">
                                                                    :</label>
                                                                <asp:Label ID="lblAmdno_p" runat="server"></asp:Label>
                                                            </div>

                                                            <div class="row">
                                                                <label style="width: 25%;">
                                                                    Quote Ref No</label>
                                                                <label style="width: 3%;">
                                                                    :</label>
                                                                <asp:Label ID="lblQuoteReferenceNumber_P" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="row p-t-10" style="border: solid black; margin-top: 10px; text-align: center; padding-bottom: 10px; padding-left: 5px;">
                                                                <label style="font-size: 12px; font-weight: 700">
                                                                    Consignee Address</label>
                                                                <asp:Label ID="lblConsigneeAddress_P" Style="padding-top: 5px !important; text-align: left;" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row div-sec p-t-10">
                                                        <div class="col-sm-6">
                                                            <div class="row" style="border: solid black; padding-left: 22px; padding: 5px;">
                                                                <%-- <p class="p-t-5">
                                                                    Perungudi IV Range: The Supdt. Of Central Excise
                                                                </p>
                                                                <p class="p-t-5">
                                                                    Perungudi Range, Perungudi Division, New No. 690, Anna Salai
                                                                </p>
                                                                <p class="p-t-5">
                                                                    Nandanam, Chennai - 600035.
                                                                </p>--%>
                                                                <asp:Label ID="lblRange_P" runat="server"></asp:Label>
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-6 p-l-10" style="padding-left: 7%;">
                                                            <div id="divGSTREV0" runat="server" visible="false">
                                                                <div class="p-t-10">
                                                                    <label style="width: 25%;">
                                                                        TNGST No</label>
                                                                    <label style="width: 3%;">
                                                                        :</label>
                                                                    <asp:Label ID="lblTNGSTNumber_P" runat="server"></asp:Label>
                                                                </div>
                                                                <div>
                                                                    <label style="width: 25%;">
                                                                        CST No</label>
                                                                    <label style="width: 3%;">
                                                                        :</label>
                                                                    <asp:Label ID="lblCSTNumber_P" runat="server"></asp:Label>
                                                                </div>
                                                                <div>
                                                                    <label style="width: 25%;">
                                                                        ECC No</label>
                                                                    <label style="width: 3%;">
                                                                        :</label>
                                                                    <asp:Label ID="lblECCNo_P" runat="server"></asp:Label>
                                                                </div>
                                                                <div>
                                                                    <label style="width: 25%;">
                                                                        TIN No</label>
                                                                    <label style="width: 3%;">
                                                                        :</label>
                                                                    <asp:Label ID="lblTINNumber_P" runat="server"></asp:Label>
                                                                </div>
                                                                <div>
                                                                    <label style="width: 25%;">
                                                                        GST No</label>
                                                                    <label style="width: 3%;">
                                                                        :</label>
                                                                    <asp:Label ID="lblLonestatrGSTNo" runat="server"></asp:Label>
                                                                </div>
                                                            </div>

                                                            <div id="divGSTREV1" runat="server" visible="false">
                                                                <div class="p-t-10">
                                                                    <label style="width: 25%;">
                                                                        CIN
                                                                    </label>
                                                                    <label style="width: 3%;">
                                                                        :</label>
                                                                    <asp:Label ID="lblCINNo_P" runat="server"></asp:Label>
                                                                </div>
                                                                <div>
                                                                    <label style="width: 25%;">
                                                                        PAN 
                                                                    </label>
                                                                    <label style="width: 3%;">
                                                                        :</label>
                                                                    <asp:Label ID="lblPANNo_p" runat="server"></asp:Label>
                                                                </div>
                                                                <div>
                                                                    <label style="width: 25%;">
                                                                        TAN
                                                                    </label>
                                                                    <label style="width: 3%;">
                                                                        :</label>
                                                                    <asp:Label ID="lblTANNo_p" runat="server"></asp:Label>
                                                                </div>
                                                                <div>
                                                                    <label style="width: 25%;">
                                                                        GSTIN
                                                                    </label>
                                                                    <label style="width: 3%;">
                                                                        :</label>
                                                                    <asp:Label ID="lblGSTIN_p" runat="server"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row p-t-10 div-sec">
                                                        <div class="col-sm-4">
                                                            <label style="font-size: 12px; font-weight: 700">
                                                                Indent No</label>
                                                            <asp:Label ID="lblIndentNo_P" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <label style="font-size: 12px; font-weight: 700">
                                                                RFP No</label>
                                                            <asp:Label ID="lblRFPNo_P" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <label style="font-size: 12px; font-weight: 700">
                                                                Amd Reason
                                                            </label>
                                                            <asp:Label ID="lblamdreason_p" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="divPurchaseorderDescription_p">
                                                <div class="row div-sec p-t-10" style="width: 100%;">
                                                    <asp:GridView ID="gvPurchaseOrder_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                        OnRowDataBound="gvPurchaseOrder_P_OnRowDataBound" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                                HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Grade">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGrade" runat="server" Text='<%# Eval("GradeName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Material Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMatType" runat="server" Text='<%# Eval("TypeName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Measurment">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMeasurement" runat="server" Text='<%# Eval("Measurment")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="QTY" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblQTY" runat="server" Text='<%# Eval("ReqWeight")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="UOM" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Unit Price" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUnitPrice" runat="server" Text='<%# Eval("UnitCost")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Total Amount" HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTotalAmount" runat="server" Text='<%# Eval("TotalCost")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div id="divtaxdetails_p">
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        &nbsp;
                                                    </div>
                                                    <div class="col-sm-6 sub-table" runat="server" id="divCharges_P">

                                                        <div id="divothercharges_p" runat="server">
                                                        </div>
                                                        <div class="row">
                                                            <label class="col-sm-9" style="border: 1px solid #000;">
                                                                SUB Total</label>
                                                            <asp:Label ID="lblSubTotal_P" CssClass="col-sm-3" Style="border: 1px solid #000; text-align: end;"
                                                                runat="server"></asp:Label>
                                                        </div>
                                                        <div id="divtax_p" runat="server">
                                                        </div>

                                                        <div class="row">
                                                            <label class="col-sm-9" style="border: 1px solid #000;">
                                                                Total Amount</label>
                                                            <asp:Label ID="lblTotalAmount_P" CssClass="col-sm-3" Style="border: 1px solid #000; text-align: end;"
                                                                runat="server"></asp:Label>
                                                        </div>
                                                        <div class="div-sec p-t-10">
                                                            <label style="width: 15%;">
                                                                Rupees in words:</label>
                                                            <asp:Label ID="lblAmonutInWords_P" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div id="divbottomcontent_p">
                                                <div id="divQualityRequirtments">
                                                    <div class="p-t-10 text-center" style="width: 100%;">
                                                        <label>Quality Requirtments</label>
                                                    </div>

                                                    <div class="p-t-10" style="width: 100%;">
                                                        <asp:GridView ID="gvQualityRequirtments_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Certificate Name" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCertificate" runat="server" Text='<%# Eval("Certificate")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                    <div class="div-sec">
                                                        <div class="p-t-5">
                                                            <label>
                                                                Drawing Ref:</label>
                                                            <asp:Label ID="lblDrawingRef_P" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="p-t-5">
                                                            <label>
                                                                Delivery:</label>
                                                            <asp:Label ID="lblDelivery_P" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="p-t-5">
                                                            <label>
                                                                Payment Terms:</label>
                                                            <asp:Label ID="lblPaymentTerms_P" runat="server"></asp:Label>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div id="divtermsandcondtions">
                                                    <div class="div-sec" style="padding-top: 10px;">
                                                        <label style="font-size: 12px; font-weight: 700">
                                                            Terms & Conditions</label>
                                                        <ol style="list-style-position: inside; margin-bottom: 0px;">
                                                            <li>Test certificate must be submitted for approval prior to dispatch</li>
                                                            <li>Material should be Dispatched Strictly as per dispatch instruction.</li>
                                                            <li>Material received at store without proper documentation will not be accepted and
                                                            all waiting charges will be on supplier</li>
                                                            <li>Kindly return the Duplicate copy of the order duly signed as a token of acceptance.</li>
                                                            <li>Please Mention our Purchase Order Number in Your Invoice.</li>
                                                            <li>Offer terms will be as per our GTC.</li>
                                                            <li>
                                                                <asp:Label ID="lblRemarks_P" runat="server"></asp:Label></li>
                                                        </ol>
                                                    </div>
                                                </div>

                                                <div id="divforignPTC" style="display: none;">
                                                    <asp:GridView ID="gvForignPOTC_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                        EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Heading" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblheading" runat="server" Text='<%# Eval("Heading")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Details" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldetails" runat="server" Text='<%# Eval("Details")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>

                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                                <tfoot>
                                    <tr>
                                        <td>
                                            <div id="divFooterContent_p" runat="server">
                                                <div style="margin-bottom: 7mm; position: fixed; width: 200mm; bottom: 0px; left: 5mm;">
                                                    <%--   <p class="text-center" style="margin-bottom: 10px !important">
                                                      <label>KINDLY RETURN THE DUPLICATE COPY OF THE ORDER DULY SIGNED AS A TOKEN OF YOUR ACCEPTANCE</label> 
                                                    </p>--%>
                                                    <div style="text-align: center;">
                                                        <label>KINDLY RETURN THE DUPLICATE COPY OF THE ORDER DULY SIGNED AS A TOKEN OF YOUR ACCEPTANCE</label>
                                                    </div>
                                                    <%-- <div class="row">
                                                        <div class="col-sm-4 text-left p-t-45" style="padding-left: 10px; padding-right: 10px; font-size: 12px; font-weight: 700; color: #000 !important;">
                                                            PREPARED BY
                                                        </div>
                                                        <div class="col-sm-4 text-left p-t-45" style="padding-left: 10px; padding-right: 10px; font-size: 12px; font-weight: 700; color: #000 !important;">
                                                            CHECKED BY
                                                        </div>
                                                        <div class="col-sm-4 text-right" style="padding-left: 10px; padding-right: 10px; font-size: 12px">
                                                            <label class="d-block" style="font-size: 12px; font-weight: 700">
                                                                For LONESTAR INDUSTRIES</label>
                                                            <label class="d-block" style="padding-top: 30px; font-size: 12px; font-weight: 700">
                                                                AUTHORISED SIGNATORY</label>
                                                        </div>
                                                    </div>--%>
                                                    <%--   <div class="row">
                                                        <div class="col-sm-4 text-left">
                                                            PREPARED BY
                                                        </div>
                                                        <div class="col-sm-4 text-left">
                                                            CHECKED BY
                                                        </div>
                                                        <div class="col-sm-4 text-right">
                                                            <label style="font-size: 12px; font-weight: 700">
                                                                For LONESTAR INDUSTRIES</label>
                                                            <label style="padding-top: 30px; font-size: 12px; font-weight: 700">
                                                                AUTHORISED SIGNATORY</label>
                                                        </div>
                                                    </div>--%>
                                                    <div class="p-t-5 p-r-10" style="text-align: end;">
                                                        <label style="font-size: 12px; font-weight: 700">
                                                            <asp:Label ID="lblcompanynamefooter_p" runat="server"></asp:Label> </label>
                                                    </div>

                                                    <div id="divdigitalsign" class="p-t-5" style="display: none;" runat="server">
                                                        <div style="text-align: end;" class="p-r-10">
                                                            <label style="text-align: end;">Digitally Signed By </label>
                                                        </div>
                                                        <div style="text-align: end;" class="p-r-10">
                                                            <label>G. UMA MAGESH </label>
                                                        </div>
                                                        <div style="text-align: end;" class="p-r-10">
                                                            <asp:Label ID="lblApprovedDatetime_p" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="p-t-10 p-l-10">
                                                        <label style="width: 38%;">PREPARED BY </label>

                                                        <label style="width: 30%;">CHECKED BY </label>


                                                        <label style="font-size: 12px; font-weight: 700; width: 30%; text-align: end;">
                                                            AUTHORISED SIGNATORY</label>
                                                    </div>

                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </tfoot>
                            </table>
                        </div>

                        <div class="modal" id="mpeView" style="overflow-y: scroll;">
                            <div style="max-width: 95%; padding-left: 5%; padding-top: 5%;">
                                <div class="modal-content">
                                    <asp:UpdatePanel ID="upView" runat="server">
                                        <Triggers>
                                        </Triggers>
                                        <ContentTemplate>
                                            <div class="modal-header">
                                                <h4 class="modal-title ADD">
                                                    <asp:Label ID="lblpodetailsheader_p" Style="color: brown;" runat="server"></asp:Label>
                                                </h4>
                                                <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true"
                                                    onclick="HideAddPopUp();">
                                                    ×</button>
                                            </div>
                                            <div class="modal-body" style="padding: 0px;">
                                                <div id="docdiv" class="docdiv">
                                                    <div class="inner-container">
                                                        <div id="Item" runat="server">
                                                            <div id="divAddNewItem" runat="server">
                                                                <div class="inner-container">
                                                                    <%--OnClientClick="return ShowAdditemdetailspopup();"--%>
                                                                    <div class="col-sm-12 text-center p-t-10" id="div5" runat="server">
                                                                        <asp:LinkButton ID="btnItemAddNew" runat="server" Text="Add New" CssClass="btn btn-success add-emp"
                                                                            OnClick="btnItemAddNew_Click"></asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="divAddItems" class="divInput" runat="server">
                                                                <div class="ip-div text-center">
                                                                    <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                                        <asp:GridView ID="gvIndentDetails" runat="server" AutoGenerateColumns="False"
                                                                            ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found"
                                                                            CssClass="table table-hover table-bordered medium" OnRowCommand="gvIndentDetails_OnRowCommand"
                                                                            OnRowDataBound="gvIndentDetails_OnRowDataBound" DataKeyNames="PID,MGMID,THKID">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="" ControlStyle-CssClass="nosort" ItemStyle-HorizontalAlign="Center">
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkitems" runat="server" AutoPostBack="true"
                                                                                            OnCheckedChanged="chkitems_OnCheckedChanged" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="PID" ItemStyle-HorizontalAlign="left"
                                                                                    HeaderStyle-HorizontalAlign="left">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblPID" runat="server" Text='<%# Eval("PID")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="PI No" ItemStyle-HorizontalAlign="left"
                                                                                    HeaderStyle-HorizontalAlign="left">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblPINumber" runat="server" Text='<%# Eval("PINumber")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Grade Name" ItemStyle-HorizontalAlign="left"
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

                                                                                <asp:TemplateField HeaderText="Req Qty" ItemStyle-HorizontalAlign="Left"
                                                                                    HeaderStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:HiddenField ID="hdnReqqty" runat="server" Value='<%# Eval("AvailQty")%>' />
                                                                                        <asp:Label ID="lblAllowPer" Text='<%# Eval("APPO")%>' Style="display: none;" CssClass="allow" runat="server" />
                                                                                        <asp:TextBox ID="txtReqWeight" CssClass="form-control" runat="server"
                                                                                            onblur="return ValidateRequiredWeightIndentQty(this);"
                                                                                            onkeypress="return validationDecimal(this);"
                                                                                            Text='<%# Eval("AvailQty")%>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="PO Qty" ItemStyle-HorizontalAlign="Left"
                                                                                    HeaderStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblPOQty" runat="server" Text='<%# Eval("PoQty")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Avail Qty" ItemStyle-HorizontalAlign="Left"
                                                                                    HeaderStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblAvailQty" runat="server" Text='<%# Eval("AvailQty")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Indent Qty" ItemStyle-HorizontalAlign="Left"
                                                                                    HeaderStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblIndentqty" runat="server" Text='<%# Eval("RequiredWeight")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Unit Cost" ItemStyle-HorizontalAlign="Left"
                                                                                    HeaderStyle-HorizontalAlign="Left">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtUnitCost" CssClass="form-control" runat="server" Text='<%# Eval("QuoteCost")%>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Type Name" ItemStyle-HorizontalAlign="left"
                                                                                    HeaderStyle-HorizontalAlign="left">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblTypename" runat="server" Text='<%# Eval("TypeName")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Measurment" ItemStyle-HorizontalAlign="left"
                                                                                    HeaderStyle-HorizontalAlign="left">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblMeasurment" runat="server" Text='<%# Eval("Measurment")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                                <asp:TemplateField HeaderText="Allow/UnAllow" HeaderStyle-CssClass="text-center"
                                                                                    ItemStyle-CssClass="text-center">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="btnAllow" Visible="false" runat="server" CommandName="Allow"
                                                                                            CssClass="btn btn-cons btn-success" Text="Allow"
                                                                                            CommandArgument="<%# ((GridViewRow) Container).RowIndex %>">                                                                                    
                                                                                        </asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>

                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>

                                                                    <div class="col-sm-12 p-t-10">
                                                                        <div class="col-sm-2">
                                                                        </div>
                                                                        <div class="col-sm-4 text-left" id="matrltype" runat="server">
                                                                            <div class="text-left">
                                                                                <label class="form-label">
                                                                                    Material Type</label>
                                                                            </div>
                                                                            <div>
                                                                                <asp:DropDownList ID="ddlMaterialType" runat="server" ToolTip="Select Material type"
                                                                                    OnSelectedIndexChanged="ddlMaterialType_OnSelectIndexChanged" CssClass="form-control"
                                                                                    AutoPostBack="true">
                                                                                    <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4 text-left">
                                                                            <div class="text-left">
                                                                                <label class="form-label mandatorylbl">
                                                                                    Total Price</label>
                                                                            </div>
                                                                            <div>
                                                                                <%--   <asp:TextBox ID="txtTotalQuoteCost" runat="server" CssClass="form-control" AutoComplete="off"
                                                                                    onkeypress="return validationDecimal(this);" ToolTip="Enter Total Quote Cost" placeholder="Enter Total Quote Cost">
                                                                                </asp:TextBox>--%>

                                                                                <asp:Label ID="lblTotalCost" Text="0" Style="color: #000; font-weight: bold;" runat="server">
                                                                                </asp:Label>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-2">
                                                                        </div>
                                                                    </div>
                                                                    <div id="divMTFields" runat="server">
                                                                    </div>
                                                                    <div class="col-sm-12 p-t-10">
                                                                        <div class="col-sm-2" style="margin-top: 38px;">
                                                                            <asp:Label ID="lblreqweight" CssClass="lablqty" runat="server"></asp:Label>
                                                                        </div>
                                                                        <div class="col-sm-4 text-left">
                                                                            <div class="text-left">
                                                                                <label class="form-label mandatorylbl">
                                                                                    Required Quantity</label>
                                                                            </div>
                                                                            <div class="text-left">
                                                                                <%--  <asp:Label ID="lblrequiredweight" Style="color: brown;" runat="server"></asp:Label>
                                                                                <asp:TextBox ID="txtrequiredWeight" runat="server" CssClass="form-control mandatoryfield"
                                                                                    onkeypress="return validationDecimal(this);" AutoComplete="off" ToolTip="Enter Weight" placeholder="Enter Weight">
                                                                                </asp:TextBox>--%>

                                                                                <asp:Label ID="lblrequiredweight" Style="color: brown;" Text="0" runat="server"></asp:Label>


                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4 text-left">
                                                                            <div class="text-left">
                                                                                <label class="form-label mandatorylbl">
                                                                                    UOM</label>
                                                                            </div>
                                                                            <div class="text-left">
                                                                                <asp:DropDownList ID="ddlUOM" runat="server" ToolTip="Select UOM" CssClass="form-control mandatoryfield">
                                                                                    <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-2">
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-12 p-t-10">
                                                                        <div class="col-sm-2">
                                                                        </div>
                                                                        <div class="col-sm-4 text-left">
                                                                            <div class="text-left">
                                                                                <label class="form-label">
                                                                                    Remarks</label>
                                                                            </div>
                                                                            <div>
                                                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" AutoComplete="off"
                                                                                    TextMode="MultiLine" Rows="3" ToolTip="Enter Remarks" placeholder="Enter Remarks">
                                                                                </asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4 text-left">
                                                                            <div class="text-left">
                                                                                <label class="form-label mandatorylbl">
                                                                                    Delivery Date</label>
                                                                            </div>
                                                                            <div>
                                                                                <asp:TextBox ID="txtDeliveryDate_I" runat="server" CssClass="form-control datepicker mandatoryfield"
                                                                                    AutoComplete="off" ToolTip="Enter Delivery Date" placeholder="Enter Delivery Date">
                                                                                </asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-2">
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-12 p-t-10" style="display: none;">
                                                                        <div class="col-sm-2">
                                                                        </div>
                                                                        <div class="col-sm-4 text-left">
                                                                            <div class="text-left">
                                                                                <label class="form-label">
                                                                                    Quantity</label>
                                                                            </div>
                                                                            <div>
                                                                                <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" AutoComplete="off"
                                                                                    ToolTip="Enter Quantity" placeholder="Enter Quantity">
                                                                                </asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-sm-4 text-left">
                                                                        </div>
                                                                        <div class="col-sm-2">
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-12 p-t-20">
                                                                        <%-- OnClick="btnSave_SPO_Click"    OnClick="btnCancel_SPO_Click"--%>
                                                                        <asp:HiddenField ID="hdn_MTFIDS" runat="server" />
                                                                        <asp:HiddenField ID="hdn_MTFIDsValue" runat="server" />
                                                                        <asp:LinkButton ID="btnSaveSPOItem" runat="server" Text="Save" OnClientClick="return SaveIndent();"
                                                                            OnClick="btnSaveSPOItem_Click" CssClass="btn btn-cons btn-save AlignTop"></asp:LinkButton>
                                                                        <asp:LinkButton ID="btnCancelSPOItem" runat="server" Text="Cancel" CausesValidation="False"
                                                                            OnClick="btnCancelSPOItem_Click" CssClass="btn btn-cons btn-danger AlignTop btnCancel"></asp:LinkButton>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div id="divOutputsItems" runat="server">
                                                                <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                                    <asp:GridView ID="gvSupplierPOItemDetails" runat="server" AutoGenerateColumns="False"
                                                                        OnRowCommand="gvSupplierPOItemDetails_OnRowCommand" OnRowDataBound="gvSupplierPOItemDetails_OnRowDataBound" ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                                        DataKeyNames="SPOID,SPODID">
                                                                        <Columns>
                                                                            <%--  <asp:TemplateField HeaderText="" ControlStyle-CssClass="nosort" ItemStyle-HorizontalAlign="Center">
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="chkall" runat="server" onclick="return checkAll(this);" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkitems" runat="server" AutoPostBack="false" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>
                                                                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                                                HeaderStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Category Name" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                                                HeaderStyle-HorizontalAlign="left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("CategoryName")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Grade Name" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                                                HeaderStyle-HorizontalAlign="left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblGradeName" runat="server" Text='<%# Eval("GradeName")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Material Thickness" ItemStyle-HorizontalAlign="Left"
                                                                                HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblMaterialThickness" runat="server" Text='<%# Eval("THKValue")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Total Price" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblQuoteCost" runat="server" Text='<%# Eval("Cost")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Measurement" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblMeasurement" runat="server" Text='<%# Eval("Measurment").ToString().Replace(",", "<br/>")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Material Type Name" ItemStyle-HorizontalAlign="left"
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
                                                                            <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Qty" ItemStyle-HorizontalAlign="Left"
                                                                                HeaderStyle-HorizontalAlign="Left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRW" Width="50px" runat="server" Text='<%# Eval("ReqWeight")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                                                HeaderText="Delete">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                                        OnClientClick='<%# string.Format("return deleteConfirmSPODID({0});",Eval("SPODID")) %>'>   <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                                                HeaderText="Edit">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                                        CommandName="EditPo">
                                                                                             <img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                                                HeaderText="Cancel">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lbtnCancel" runat="server" Visible="false" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                                        OnClientClick='<%# string.Format("return CancelConfirmSPODID({0});",Eval("SPODID")) %>'><img src= "../Assets/images/icon_cancel.png" alt=""  width="25px"/></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12 p-t-20 text-center">
                                                                <asp:LinkButton ID="lbtnSharePO" runat="server" Text="Share PO" CausesValidation="False"
                                                                    OnClick="lbtnSharePO_Click" CssClass="btn btn-cons btn-success"></asp:LinkButton>
                                                                <asp:LinkButton ID="lbtnApprove" runat="server" Text="Send For Approval" CausesValidation="False"
                                                                    OnClick="btnSendForApproval_Click" CssClass="btn btn-cons btn-success"></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="modal-footer">
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:HiddenField ID="hdnSPODID" runat="server" Value="0" />
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
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>

                        <div class="modal" id="mpeShowDocument">
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

                        <div class="modal" id="mpeAddtax" style="overflow-y: scroll;">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                        <Triggers>
                                        </Triggers>
                                        <ContentTemplate>
                                            <div class="modal-header">
                                                <h4 class="modal-title ADD">
                                                    <asp:Label runat="server" ID="lblSpoNumber_T"></asp:Label></h4>
                                                <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                                    ×</button>
                                            </div>
                                            <div class="modal-body" style="padding: 0px;">
                                                <div id="docdiv_t" class="docdiv">
                                                    <div class="inner-container">
                                                        <ul class="nav nav-tabs lserptabs" style="display: inline-block; width: 100%; background-color: cadetblue; text-align: right; font-size: x-large; font-weight: bold; color: whitesmoke;">
                                                            <li id="liOtherCharges" class="active"><a href="#OtherCharges" class="tab-content" data-toggle="tab" onclick="OpenTab('OtherCharges');">
                                                                <p style="margin-left: 10px; text-align: center; color: black;">
                                                                    Other Charges
                                                                </p>
                                                            </a></li>
                                                            <li id="liTax"><a href="#Tax" class="tab-content active"
                                                                data-toggle="tab" onclick="OpenTab('Tax');">
                                                                <p style="margin-left: 10px; text-align: center; color: black;">
                                                                    Tax
                                                                </p>
                                                            </a></li>
                                                        </ul>
                                                        <div id="OtherCharges" runat="server" style="display: none;">
                                                            <div class="col-sm-12 p-t-10">
                                                                <div class="col-sm-3">
                                                                    <label class="form-label mandatorylbl">
                                                                        Other Charges
                                                                    </label>
                                                                </div>
                                                                <div class="col-sm-9">
                                                                    <asp:DropDownList ID="ddlOtherCharges_t" runat="server" TabIndex="1" ToolTip="Select Item Name"
                                                                        CssClass="form-control mandatoryfield">
                                                                        <asp:ListItem Selected="True" Value="0" Text="-- Select --" />
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12 p-t-10">
                                                                <div class="col-sm-3">
                                                                    <label class="form-label mandatorylbl">
                                                                        Value
                                                                    </label>
                                                                </div>
                                                                <div class="col-sm-9">
                                                                    <asp:TextBox ID="txtOtherCharges_t" runat="server" TabIndex="6" placeholder="** Please Type value(EX:100)"
                                                                        onkeypress="return validationDecimal(this);"
                                                                        CssClass="form-control mandatoryfield"
                                                                        autocomplete="nope"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12 p-t-10">
                                                                <div class="col-sm-3">
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <asp:LinkButton Text="Save" CssClass="btn btn-cons btn-save  AlignTop" ID="btnSaveOtherCharges"
                                                                            CommandName="othercharges" runat="server" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_OtherCharges');"
                                                                            OnClick="btnSaveTaxAndOtherCharges_Click" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3">
                                                                </div>
                                                                <div class="col-sm-3">
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                                <asp:GridView ID="gvSupplierPOOtherCharges" runat="server" AutoGenerateColumns="False"
                                                                    ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                                    DataKeyNames="SPOOCDID">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                                            HeaderStyle-HorizontalAlign="left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("ChargesName")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Value" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                                            HeaderStyle-HorizontalAlign="left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblValue" runat="server" Text='<%# Eval("Value")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                                            HeaderText="Delete">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                                    OnClientClick='<%# string.Format("return deleteConfirmOtherCharges({0});",Eval("SPOOCDID")) %>'>   <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                        <div id="Tax" runat="server">
                                                            <div class="col-sm-12 p-t-10">
                                                                <div class="col-sm-3">
                                                                    <label class="form-label mandatorylbl">
                                                                        Tax
                                                                    </label>
                                                                </div>
                                                                <div class="col-sm-9">
                                                                    <asp:DropDownList ID="ddlTax_t" runat="server" TabIndex="1" ToolTip="Select Tax Name"
                                                                        CssClass="form-control mandatoryfield">
                                                                        <asp:ListItem Selected="True" Value="0" Text="-- Select --" />
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12 p-t-10">
                                                                <div class="col-sm-3">
                                                                    <label class="form-label mandatorylbl">
                                                                        Value
                                                                    </label>
                                                                </div>
                                                                <div class="col-sm-9">
                                                                    <asp:TextBox ID="txtTaxValue_t" runat="server" TabIndex="6" placeholder="** Please Type Percentage value(EX:4)"
                                                                        onkeypress="return validationDecimal(this);"
                                                                        CssClass="form-control mandatoryfield"
                                                                        autocomplete="nope"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12 p-t-10">
                                                                <div class="col-sm-3">
                                                                </div>
                                                                <div class="col-sm-3">
                                                                    <div class="form-group">
                                                                        <asp:LinkButton Text="Save" CssClass="btn btn-cons btn-save  AlignTop" ID="btnSavetaxDetails"
                                                                            CommandName="tax" runat="server" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_Tax');"
                                                                            OnClick="btnSaveTaxAndOtherCharges_Click" />
                                                                    </div>
                                                                </div>
                                                                <div class="col-sm-3">
                                                                </div>
                                                                <div class="col-sm-3">
                                                                </div>
                                                            </div>
                                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                                <asp:GridView ID="gvTaxDetails" runat="server" AutoGenerateColumns="False"
                                                                    ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                                    DataKeyNames="SPOTDID">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                                            HeaderStyle-HorizontalAlign="Center">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                                            HeaderStyle-HorizontalAlign="left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("TaxName")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Value" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                                            HeaderStyle-HorizontalAlign="left">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblValue" runat="server" Text='<%# Eval("Value")%>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                                            HeaderText="Delete">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                                    OnClientClick='<%# string.Format("return deleteConfirmTaxDetailsCharges({0});",Eval("SPOTDID")) %>'>   <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="modal-footer">
                                                        <div class="col-sm-12 text-center">
                                                            <asp:Label ID="lblPOAmount_T" Style="font-size: large; font-weight: bold;" runat="server"></asp:Label>
                                                        </div>
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
                                            <asp:HiddenField ID="hdnPoqty" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnAttachementFlag" runat="server" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>


                        <div class="modal" id="mpeInwardHeader" style="overflow-y: scroll;">
                            <div style="max-width: 81%; padding-left: 10%; padding-top: 5%;">
                                <div class="modal-content">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <Triggers>
                                        </Triggers>
                                        <ContentTemplate>
                                            <div class="modal-header">
                                                <h4 class="modal-title ADD">PO Invoice</h4>
                                                <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                                    ×</button>
                                            </div>
                                            <div class="modal-body" style="padding: 0px;">
                                                <div id="divInwardHeader" class="docdiv">
                                                    <div class="inner-container">
                                                        <div id="Div1" runat="server">
                                                            <div id="div2" runat="server">
                                                                <div class="inner-container">
                                                                    <div class="col-sm-12 text-center p-t-10" id="div3" runat="server">
                                                                        <asp:GridView ID="gvMaterialInward" runat="server" AutoGenerateColumns="False"
                                                                            ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="S.No" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="DC Number" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblDCNumber" runat="server" Text='<%# Eval("DCNumber")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="DC Date" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblDCDate" runat="server" Text='<%# Eval("DCDate")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="InVoice Number" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblInVoiceNumber" runat="server" Text='<%# Eval("InVoiceNumber")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="InVoice Date" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblInVoiceDate" runat="server" Text='<%# Eval("InVoiceDate")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Add InVoice" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="btnAddAttach" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                                            CommandName="AddDocs">
                                                                                              <img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="modal-footer">
                                                    </div>
                                                </div>
                                            </div>
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
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>

                        <div class="modal" id="mpePOInVoicePopUp" style="overflow-y: scroll;">
                            <div style="max-width: 81%; padding-left: 10%; padding-top: 5%;">
                                <div class="modal-content">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <Triggers>
                                        </Triggers>
                                        <ContentTemplate>
                                            <div class="modal-header">
                                                <h4 class="modal-title ADD">PO Invoice</h4>
                                                <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                                    ×</button>
                                            </div>
                                            <div class="modal-body" style="padding: 0px;">
                                                <div id="divPOInVoice" class="docdiv">
                                                    <div class="inner-container">
                                                        <div class="col-sm-12 p-t-10">
                                                            <div class="col-sm-4 text-right">
                                                                <label class="form-label mandatorylbl">
                                                                    Invoice Number</label>
                                                            </div>
                                                            <div class="col-sm-6 text-left">
                                                                <asp:TextBox ID="txtInVoiceNumber" runat="server" CssClass="form-control  mandatoryfield"
                                                                    ToolTip="Enter DC Number" placeholder="Enter Invoice Number">
                                                                </asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-2">
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-12 p-t-10">
                                                            <div class="col-sm-4 text-right">
                                                                <label class="form-label mandatorylbl">
                                                                    Invoice Date</label>
                                                            </div>
                                                            <div class="col-sm-6 text-left">
                                                                <asp:TextBox ID="txtInvoiceDate" runat="server" CssClass="form-control datepicker mandatoryfield"
                                                                    placeholder="Enter Invoice Date">
                                                                </asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-2">
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-12 p-t-10">
                                                            <div class="col-sm-4 text-right">
                                                                <label class="form-label mandatorylbl">
                                                                    Document Name</label>
                                                            </div>
                                                            <div class="col-sm-6 text-left">
                                                                <asp:TextBox ID="txtDocumentname" runat="server" CssClass="form-control mandatoryfield"
                                                                    placeholder="Enter Document Name">
                                                                </asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-2">
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-12 p-t-10">
                                                            <div class="col-sm-4 text-right">
                                                                <label class="form-label">
                                                                    InVoice Copy</label>
                                                            </div>
                                                            <div class="col-sm-6 text-left">
                                                                <asp:FileUpload ID="fpInvoiceCopy" runat="server" class="form-control" Width="70%" />
                                                            </div>
                                                            <div class="col-sm-2">
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-12 p-t-10">
                                                            <div class="col-sm-4 text-right">
                                                                <label class="form-label">
                                                                    (InClusive Taxes And Other Charges) InVoice Total Amount</label>
                                                            </div>
                                                            <div class="col-sm-6 text-left">
                                                                <asp:TextBox ID="txtInVoiceTotalAmount" runat="server" CssClass="form-control mandatoryfield"
                                                                    placeholder="Enter Total Amount"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-2">
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-12 p-t-10">
                                                            <asp:LinkButton Text="Save InVoice" CssClass="btn btn-cons btn-save  AlignTop" ID="btnSaveInVoice"
                                                                runat="server" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divPOInVoice');"
                                                                OnClick="btnSaveInVoice_Click" />
                                                        </div>
                                                        <div id="divtaxes" runat="server">
                                                            <%-- <div class="col-sm-12 p-t-10">
                                                                <div class="col-sm-4 text-right">
                                                                    <label class="form-label">
                                                                        InVoice Copy</label>
                                                                </div>
                                                                <div class="col-sm-6 text-left">
                                                                    <asp:TextBox ID="txtInVoiceAmount" runat="server" CssClass="form-control mandatoryfield"
                                                                        placeholder="Enter Document Name">
                                                                </div>
                                                                <div class="col-sm-2">
                                                                </div>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
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
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>


                        <div class="modal" id="mpeTermsAndConditions" style="overflow-y: scroll;">
                            <div style="max-width: 95%; padding-left: 5%; padding-top: 5%;">
                                <div class="modal-content">
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <Triggers>
                                        </Triggers>
                                        <ContentTemplate>
                                            <div class="modal-header">
                                                <h4 class="modal-title ADD">
                                                    <asp:Label ID="Label2" Style="color: brown;" runat="server"></asp:Label>
                                                </h4>
                                                <button type="button" class="close btn-primary-purple" onclick="HideTCPopUp();" data-dismiss="modal" aria-hidden="true">
                                                    ×</button>
                                            </div>
                                            <div class="modal-body" style="padding: 0px;">
                                                <div class="col-sm-12 p-t-10">
                                                    <asp:GridView ID="gvPOTermsAndConditions" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                        EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium" DataKeyNames="SPOTCMID">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                                HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                                ItemStyle-Width="15%">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkall" runat="server" onclick="return checkAllItems(this);" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkQC" runat="server" AutoPostBack="false" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Heading" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblheading" runat="server" Text='<%# Eval("Heading")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Details" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldetails" runat="server" Text='<%# Eval("Details")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>

                                                <div class="col-sm-12 p-t-10">
                                                    <asp:GridView ID="gvPOTermsAndConditionsDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                        EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium" DataKeyNames="SPOTCMID">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                                HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Heading" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblheading" runat="server" Text='<%# Eval("Heading")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Details" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbldetails" runat="server" Text='<%# Eval("Details")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>

                                            </div>
                                            <div class="modal-footer">
                                                <div class="col-sm-12 text-center">
                                                    <asp:LinkButton ID="btnSaveTC" CssClass="btn btn-cons btn-success" Text="Save TC"
                                                        OnClientClick="return ValidateCheck();" OnClick="btnSaveTC_Click" runat="server"></asp:LinkButton>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
