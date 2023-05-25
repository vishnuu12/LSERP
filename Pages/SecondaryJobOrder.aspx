<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="SecondaryJobOrder.aspx.cs" Inherits="Pages_SecondaryJobOrder" ValidateRequest="false" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function hidePartDetailpopup() {
            $('#mpePartDetails').modal('hide');
            $('div').removeClass('modal-backdrop');
            return false;
        }

        function ShowAddPartPopUp() {
            $('#mpePartDetails').modal('show');
            return false;
        }

        function ShowJobCardPopUp() {
            $('#mpeJobCardDetails').modal('show');
            $('#mpePartDetails').modal('hide');
            return false;
        }

        function HideJobCardPopUp() {
            $('#mpePartDetails').modal('show');
            $('#mpeJobCardDetails').modal('hide');
            return false;
        }

        var PartQty;

        function BindPartSNoAndMRNDetails(ele) {
            if (PartQty != $(ele).val()) {
                var PartQty = $(ele).val();
                var TotalQty = parseInt($('#ContentPlaceHolder1_txtPartQuantity_J').closest('.col-sm-12').find('span').text().replace('(', '').replace(')', ''));

                if (parseInt(PartQty) <= TotalQty) {
                    __doPostBack('GetMRN', PartQty);
                    return true;
                }
                else {
                    ErrorMessage('Error', 'Part Quantity Should be less than Total Quantity<=' + TotalQty + '')
                    return false;
                }
            }
            else
                return false;
        }

        function ValidateIssuedWeight() {
            if ($(event.target).val() != '') {
                var IssuedWeight = parseFloat($(event.target).val());
                var AvailableWeight = parseFloat($(event.target).closest('tr').find('.AvailableWeight').text());
                if (IssuedWeight <= AvailableWeight) {
                    return true;
                }
                else {
                    ErrorMessage('Error', 'Issued Weight Should Not be Greate Available Weight');
                    $(event.target).val('');
                    return false;
                }
            }
            else {
                return false;
            }
        }

        function MandatoryField(ele) {
            if ($(ele).is(":checked")) {
                $(ele).closest('tr').find('[type="text"]').addClass("mandatoryfield");
                $(ele).closest('tr').find('[type="text"]').before('<span class="textboxmandatory" style="color: red;">*</span>');
                if ($('#ContentPlaceHolder1_hdnEditJCHID').val() == "0") {
                    $(ele).closest('tr').find('[type="file"]').addClass("mandatoryfield");
                    $(ele).closest('tr').find('[type="file"]').before('<span class="textboxmandatory" style="color: red;">*</span>');
                }
            }
            else {
                $(ele).closest('tr').find('[type="text"]').removeClass("mandatoryfield");
                $(ele).closest('tr').find('[type="text"]').closest('td').find('.textboxmandatory').remove();
                if ($('#ContentPlaceHolder1_hdnEditJCHID').val() == "0") {
                    $(ele).closest('tr').find('[type="file"]').removeClass("mandatoryfield");
                    $(ele).closest('tr').find('[type="file"]').closest('td').find('.textboxmandatory').remove();
                }
            }
        }

        function GetProcessOrder(ele) {
            $('#ContentPlaceHolder1_hdnProcessOrder_PDF').val($(ele).attr('id'));
            document.getElementById('<%=btnPDF.ClientID %>').click();
            return false;
        }

        function CalculateMrnReqWeight() {
            var PartCount = parseInt($('#ContentPlaceHolder1_gvItemPartSNODetails').find('input:checked').not('#ContentPlaceHolder1_gvItemPartSNODetails_chkall').length);
            var UnitWeight = parseFloat($('#ContentPlaceHolder1_hdnPartUnitReqWeight').val());
            $('#ContentPlaceHolder1_hdnPQReqWeight').val(parseFloat(PartCount * UnitWeight).toFixed(2));
        }

        function ValidateSave() {
            var msg = Mandatorycheck('ContentPlaceHolder1_divInput_Job');
            var PQReqWeight = parseFloat($('#ContentPlaceHolder1_hdnPQReqWeight').val());

            var PartCount = parseInt($('#ContentPlaceHolder1_gvItemPartSNODetails').find('input:checked').not('#ContentPlaceHolder1_gvItemPartSNODetails_chkall').length);
            if (PartCount > 0) {
                if (msg) {

                    if ($('#ContentPlaceHolder1_gvMRNDetails').find('.MRN').find('input:checked').length > 0) {
                        var TotalIssuedWeight = parseFloat(0);

                        $('#ContentPlaceHolder1_gvMRNDetails').find('.MRN').find('[type="checkbox"]').each(function (index, value) {
                            if ($(this).prop("checked")) {
                                var Issued = $(this).closest('tr').find('[type="text"]')[0].value;
                                TotalIssuedWeight = parseFloat(TotalIssuedWeight) + parseFloat(Issued);
                            }
                        });
                        if (PQReqWeight == parseFloat(TotalIssuedWeight).toFixed(2)) {
                            return true;
                        }
                        else {
                            ErrorMessage('Error', 'Total Issued Weight Should Not Greater selected Part Quantity Required Weight ' + PQReqWeight + '');
                            hideLoader();
                            return false;
                        }
                    }
                    else {
                        ErrorMessage('Error', 'No MRN Selected');
                        hideLoader();
                        return false;
                    }
                }
                else {
                    return false;
                }
            }
            else {
                ErrorMessage('Error', 'Select Part Sno');
                hideLoader();
                return false;
            }
        }
        function ValidateQCRequest() {
            if ($('#ContentPlaceHolder1_btnSaveQCRequest').hasClass('WorkInProgress')) {
                if ($('#ContentPlaceHolder1_gvQCRequest').find('[type="checkbox"]:checked').not('#ContentPlaceHolder1_gvQCRequest_chkall').length > 0) {
                    return true;
                }
                else {
                    ErrorMessage('Error', 'No Part Selected');
                    hideLoader();
                    return false;
                }
            }
            else
                return true;
        }

        function BindStages(ele) {
            if ($(ele).val() != "") {
                if ($(ele).val() != $('#<%=hdnStages.ClientID %>').val()) {
                    var Stages = $(event.target).val();
                    $('#<%=hdnStages.ClientID %>').val(Stages);
                    document.getElementById('<%=btnStages.ClientID %>').click();
                    return true;
                }
                else
                    return false;
            }
            else {
                ErrorMessage('Error', 'Stages Can not be Empty');
                return false;
            }
        }

        function ValidateAssemplyJobCard() {
            var msg = Mandatorycheck('divInput_AP');
            if (msg) {
                if ($('#ContentPlaceHolder1_gvPartAssemplyPlanningDetails_AP').find('[type="checkbox"]:checked').not('#ContentPlaceHolder1_gvPartAssemplyPlanningDetails_AP_chkall').length > 0) {
                    {
                        if ($('#ContentPlaceHolder1_gvBellowSno_AP').find('[type="checkbox"]:checked').not('#ContentPlaceHolder1_gvBellowSno_AP_chkall').length > 0)
                            return true;
                        else {
                            ErrorMessage('Error', 'No Item Selected');
                            hideLoader();
                            return false;
                        }
                    }
                }
                else {
                    ErrorMessage('Error', 'No Part Selected');
                    hideLoader();
                    return false;
                }
            }
            else
                return false;
        }

        function ValidateSheetWelding() {
            var msg = Mandatorycheck('divSheetWelding');
            if (msg) {
                if ($('#ContentPlaceHolder1_gvPartSerielNumber_SW').find('[type="checkbox"]:checked').not('#ContentPlaceHolder1_gvPartSerielNumber_SW_chkall').length > 0) {
                    return true;
                }
                else {
                    ErrorMessage('Error', 'No Part Selected');
                    hideLoader();
                    return false;
                }
            }
            else
                return false;
        }

        function ValidateBelloFormingAndTangentCutting() {
            var msg = Mandatorycheck('ContentPlaceHolder1_divBellowTangetCutting_BTC');
            if (msg) {
                if ($('#ContentPlaceHolder1_gvPartSNO_BTC').find('[type="checkbox"]:checked').not('#ContentPlaceHolder1_gvPartSNO_BTC_chkall').length > 0) {
                    return true;
                }
                else {
                    ErrorMessage('Error', 'No Part Selected');
                    hideLoader();
                    return false;
                }
            }
            else
                return false;
        }

        function ShowQCRequestPopUp() {
            $('#mpeQCRequest').modal('show');
            return false;
        }

        function hidempeQCRequestPopUp() {
            $('#mpeQCRequest').modal('hide');
            $('#mpePartDetails').modal('show');
            return false;
        }

        function ShowJobCardPreview() {
            $('#mpeViewdocs').modal('show');
            return false;
        }

        function ShowSheetWeldingPopUp() {
            $('#mpeSheetWelding').modal('show');
            $('#mpePartDetails').modal('hide');
            return false;
        }

        function HideSheetWeldingPopUp() {
            $('#mpePartDetails').modal('show');
            $('#mpeSheetWelding').modal('hide');
            return false;
        }

        function ShowSheetMarkingCuttingPopUP() {
            $('#mpeSheetMarkingCutting').modal('show');
            $('#mpePartDetails').modal('hide');
        }

        function ShowBellowsTangetCutPopup() {
            $('#mpeBellowTangetCutting').modal('show');
            $('#mpePartDetails').modal('hide');
        }

        function hideBellowsTangetCutPopUP() {
            $('#mpePartDetails').modal('show');
            $('#mpeBellowTangetCutting').modal('hide');
        }

        function ShowAssemplyPlanningPopUp() {
            //            $('#mpeAssemplyPlanning').modal({
            //                backdrop: 'static'
            //            });
            $('#mpeAssemplyPlanning').modal('show');
            $('.datepicker').datepicker({
                format: 'dd/mm/yyyy'
            });
            return false;
        }

        function hideAssemplyJobCardPopup() {
            $('#mpeAssemplyPlanning').modal('hide');
            $('div').removeClass("modal-backdrop");
            return false;
        }

        function ShowMRNBOIPopup() {
            $('#mpeBOIMRNIssue').modal('show');
            $('#mpeAssemplyPlanning').modal('hide');
        }

        function HideMRNBOIPopup() {
            $('#mpeBOIMRNIssue').modal('hide');
            $('#mpeAssemplyPlanning').modal('show');
        }

        function validateJobCardQuantity() {
            var PartCompleted = $(event.target).closest('td').find('[type="hidden"]').val();
            var jobCardQty = $(event.target).val();
            if (parseInt(jobCardQty) > parseInt(PartCompleted)) {
                ErrorMessage('Error', 'Job Card Quantity Should be Less Than  equal to Available Qty (' + PartCompleted + ')');
                $(event.target).val('');
                return false;
            }
        }

        function Edit(ele) {
            $('#ContentPlaceHolder1_hdnProcessName').val($(ele).attr('name'));
            $('#ContentPlaceHolder1_hdnEditJCHID').val($(ele).attr('id').split('/')[1]);
            document.getElementById('<%=btnEdit.ClientID %>').click();
            return false;
        }

        function PrintHtmlFile() {
            var cotent = $('#ContentPlaceHolder1_hdnpdfContent').val();
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');
            winprint.document.write(cotent);
            setTimeout(function () {
                winprint.document.close();
                winprint.focus();
                winprint.print();
                // winprint.close();
            }, 1000);

            return false;
        }

        function checkAllItems(ele) {
            debugger;
            if ($(ele).is(":checked")) {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', true);
            }
            else {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', false);
            }
            $('#ContentPlaceHolder1_btnLoadSno').click();
        }

        function CheckAllBellowSno(ele) {
            debugger;
            if ($(ele).is(":checked")) {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', true);
            }
            else {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', false);
            }
        }

        function DeleteAJD(JCHID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Job Card will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deleteAJ', JCHID);
            });
            return false;
        }


        function DeleteAssemplyMRNIssue(JCMRNID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the MRN will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deleteAssemplyMRNIssue', JCMRNID);
            });
            return false;
        }

        function ValidateAssemplyMRNWeightIssue(ele) {
            var IssueWeight = parseFloat($(ele).val());
            var AvailWeight = parseFloat($(ele).closest('td').find('[type="hidden"]').val());

            if (IssueWeight > AvailWeight) {
                ErrorMessage('Error', 'Issued Weight Can Not Greater Then Avail Weight "' + $(ele).val() + '"');
                $(ele).val(AvailWeight);
            }
        }

        function chekAllMRN(ele) {
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

        function chekAllPartSno(ele) {
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
            CalculateMrnReqWeight();
        }

        function MRNMandatoryField(ele) {
            if ($(ele).is(":checked")) {
                $(ele).closest('tr').find('[type="text"]').addClass("mandatoryfield");
                $(ele).closest('tr').find('[type="text"]').before('<span class="textboxmandatory" style="color: red;">*</span>');
            }
            else {
                $(ele).closest('tr').find('[type="text"]').removeClass("mandatoryfield");
                $(ele).closest('tr').find('[type="text"]').closest('td').find('.textboxmandatory').remove()
            }
        }

        function ValidateAssemplyMRNIssue() {
            var msg = Mandatorycheck('ContentPlaceHolder1_divAJMRN');
            if (msg) {
                if ($('#ContentPlaceHolder1_gvMRNIssueBOI_AP').find('[type="checkbox"]:checked').not('#ContentPlaceHolder1_gvMRNIssueBOI_AP_chkall').length) {
                    return true;
                }

                else {
                    ErrorMessage('Error', 'No MRN Selected');
                    hideLoader();
                    return false;
                }
            }
            else
                return false;
        }

        function PrintAssemplyJobCard(JCHID) {
            __doPostBack('PrintAssemplyJobCard', JCHID);
        }

        function ShowAssemplyJobCardDetails() {
            $('#mpeAssemplyJobCardDetails').modal('show');
            $('#mpeAssemplyPlanning').modal('hide');
        }

        function hideAssemplyJobCardDetailsPopUp() {
            $('#mpeAssemplyPlanning').modal('show');
            $('#mpeAssemplyJobCardDetails').modal('hide');
        }

        function PrintAssemplyPlanningPDF(QrCode, Address, PhoneAndFaxNo, Email, WebSite) {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var APInnerHtml = $('#ContentPlaceHolder1_divSubAssemplyWeldingPDF').html();

            winprint.document.write("<html><head><title>");
            winprint.document.write("");
            winprint.document.write("</title>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='../Assets/css/ep-style.css'/>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='../Assets/css/main.css'/>");

            winprint.document.write("<link rel='stylesheet'  href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/print.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");

            winprint.document.write("<style type='text/css'> label { font-weight: bold ! important;color: #000 ! important; }   .col-sm-12.contractorborder { padding-top:5px; border-left: 2px solid;border-right: 2px solid; border-bottom: 2px solid;} .sideheading{ display:flex;align-items:flex-start;justify-content:center;margin-top:15px; } .lbl_h{ width:35%; } .lbl_v{ width:65%; } .Qrcode {height: 60px;width: 80px;margin-right: 10px;padding-left: 20px;} </style>");
            winprint.document.write("</head><body>");

            winprint.document.write("<div class='print-page'>");
            winprint.document.write("<table><thead><tr><td>");
            winprint.document.write("<div class='col-sm-12 header-space' style='text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;height:120px'>");
            winprint.document.write("<div class='header' style='border-bottom:1px solid;'>");
            winprint.document.write("<div>");
            winprint.document.write("<div class='col-sm-12 padding0' id='divLSERPLogo' style='margin:0px auto;display:block;padding:5px 0px;'>");

            winprint.document.write("<div class='row'>");
            winprint.document.write("<div class='col-sm-3'>");
            winprint.document.write("<img src='../Assets/images/lonestar.jpeg' alt='lonestar-image' width='90px'>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-6 text-center'>");
            winprint.document.write("<h3 style='font-weight:600;font-size:24px;font-style:italic;color:#000;font-family: Arial;display: contents;'>LONESTAR </h3>");
            winprint.document.write("<span style='font-weight:600;font-size:24px ! important;font-family: Times New Roman;'>INDUSTRIES</span>");
            winprint.document.write("<p style='font-weight:500;color:#000;width: 103%;'>" + Address + "</p>");
            winprint.document.write("<p style='font-weight:500;color:#000'>" + PhoneAndFaxNo + "</p>");
            winprint.document.write("<p style='font-weight:500;color:#000'>" + Email + "</p>");
            winprint.document.write("<p style='font-weight:500;color:#000'>" + WebSite + "</p>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-3'>");
            winprint.document.write("<img src='../Assets/images/iso.jpeg' alt='lonestar-image' height='90px'>");
            winprint.document.write("</div></div>");
            winprint.document.write("</div></div></div>");
            winprint.document.write("</td></tr></thead>");
            winprint.document.write("<tbody><tr><td>");
            winprint.document.write("<div  style='padding-top:0px;margin:2mm;'>");
            winprint.document.write(APInnerHtml);
            winprint.document.write("</div>");
            winprint.document.write("</td></tr></tbody>");
            winprint.document.write("<tfoot class='footer-space'><tr><td>");

            winprint.document.write("<div style='margin:2mm;'>");
            winprint.document.write("<div class='col-sm-12' style='padding-top:50px;'>");
            winprint.document.write("<div class='col-sm-6 p-t-20'><label style='color:black; font-weight:bolder;float:left;'>Quality Incharge</label></div>");
            winprint.document.write("<div class='col-sm-6' style='padding-left:16%;'><label style='color:black; font-weight:bolder;'> Production Incharge</label><img src='" + QrCode + "' class='Qrcode' /></div>");
            winprint.document.write("</div></div>");
            winprint.document.write("</td></tr></tfoot></table>");
            winprint.document.write("</div>");

            winprint.document.write("</body></html>");

            setTimeout(function () {
                winprint.document.close();
                winprint.focus();
                winprint.print();
                // winprint.close();
            }, 1000);
        }

        function ValidateAJDetails() {
            if ($('#ContentPlaceHolder1_gvAJDetails_AJD').find('[type="checkbox"]:checked').not('#ContentPlaceHolder1_gvAJDetails_AJD_chkall').length > 0) {
                return true;
            }
            else {
                ErrorMessage('Error', 'No Part Selected');
                hideLoader();
                return false;
            }
        }

        function DeleteJobCard(JCHID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Job card will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deleteJobCardDetailsByJCHID', JCHID);
            });
            return false;
        }

        function PrintMarkinAndCutting(QRCode, Mode) {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var Address = $('#ContentPlaceHolder1_hdnAddress').val();
            var PhoneAndFaxNo = $('#ContentPlaceHolder1_hdnPhoneAndFaxNo').val();
            var Email = $('#ContentPlaceHolder1_hdnEmail').val();
            var WebSite = $('#ContentPlaceHolder1_hdnWebsite').val();

            var DocNo = $('#ContentPlaceHolder1_hdnDocNo').val();
            var RevNo = $('#ContentPlaceHolder1_hdnRevNo').val();
            var RevDate = $('#ContentPlaceHolder1_hdnRevDate').val();

            var FabricationWelding = $('#ContentPlaceHolder1_divFabricationAndWeldingPDF').html();

            var MarkingAndCutting = $('#ContentPlaceHolder1_divMarkingAndCuttingPDF').html();

            var SheetMarkingAndCutting = $('#ContentPlaceHolder1_divSheetMarkingAndCuttingPDF').html();

            var SheetWelding = $('#ContentPlaceHolder1_divSheetWeldingPDF').html();

            var BellowFormingTangentCutting = $('#ContentPlaceHolder1_divBellowFormingAndTangentCuttingPDF').html();

            winprint.document.write("<html><head><title>");
            winprint.document.write("Offer");
            winprint.document.write("</title>");
            //  winprint.document.write("<link rel='stylesheet' type='text/css' href='" + epstyleurl + "'/>");
            // winprint.document.write("<link rel='stylesheet' href='" + Main + "' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet'  href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/printg.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            //   winprint.document.write("<link rel='stylesheet' href='" + topstrip + "' type='text/css'/>");           
            winprint.document.write("<style type='text/css'> label { font-weight: bold ! important;color: #000 ! important; } .contractorborder label{padding: 4px 6px; } .contractorborder { padding-top:5px; border: 1px solid;} .sideheading{ display:flex;align-items:flex-start;justify-content:center;margin-top:15px; } .lbl_h{ width:35%; } .lbl_v{ width:65%; } .Qrcode {height: 60px;width: 80px;margin-right: 10px;padding-left: 20px;} </style>");

            winprint.document.write("<div class='print-page' style='position:fixed;border:1px solid #000;width:200mm;height:287mm;margin-top:5mm;margin-bottom:5mm;margin-left:5mm;margin-right:5mm'></div>");
            winprint.document.write("<table style='width:200mm;height:287mm;margin-left:20px;border:0px !important'><thead><tr><td>");
            winprint.document.write("<div class='col-sm-12' style='text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;height:112px'>");
            winprint.document.write("<div>");
            winprint.document.write("<div>");
            winprint.document.write("<div class='col-sm-12 padding0' id='divLSERPLogo' style='margin:20px auto;display:block;'>");
            //  winprint.document.write("<img src='" + topstrip + "' alt='' height='100px;' style='object-fit:contain;width:100%;display:none'>");
            winprint.document.write("<div class='row' style='position:fixed;width:200mm;left:5mm;border-bottom:1px solid #000;'>");
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
            if (Mode == "SMC")
                winprint.document.write(SheetMarkingAndCutting);
            else if (Mode == "MC")
                winprint.document.write(MarkingAndCutting);
            else if (Mode == "FW")
                winprint.document.write(FabricationWelding);
            else if (Mode == "SW")
                winprint.document.write(SheetWelding);
            else if (Mode == "BFTC")
                winprint.document.write(BellowFormingTangentCutting);

            winprint.document.write("</div>");

            winprint.document.write("</td></tr></tbody>");
            winprint.document.write("<tfoot><tr><td>");
            winprint.document.write("<div style='height:30mm;'>");
            winprint.document.write("<div class='row' style='margin-bottom:20px;width:200mm;bottom:0px;position:fixed;'>");

            winprint.document.write("<div class='col-sm-3 text-center'>");
            winprint.document.write("<label style='color:black; font-weight:bolder;'>Quality Incharge</label>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-6 text-center'>");
            winprint.document.write("<label style='width:25%;color:black;float:left; font-weight:bolder;border: 1px solid black;'>Doc No : " + DocNo + "</label>");
            winprint.document.write("<label style='width:25%;color:black;float:left; font-weight:bolder;border: 1px solid black;'>Rev No : " + RevNo + "</label>");
            winprint.document.write("<label style='width:50%;color:black;float:left; font-weight:bolder;border: 1px solid black;'>Rev Date : " + RevDate + "</label>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-3 text-center'>");
            winprint.document.write("<label style='color:black; font-weight:bolder;'> Production Incharge</label><img src='" + QRCode + "' class='Qrcode'/>");
            winprint.document.write("</div>");

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
		
		function printPartToPartjobcard(PAPDID) {
            __doPostBack("PartToPartAssemplyJobCardPrint", PAPDID);
            return false;
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
                                    <h3 class="page-title-head d-inline-block">Secondary Job Order</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Production</a></li>
                                <li class="active breadcrumb-item" aria-current="page">Secondary Job Order</li>
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
                    <asp:PostBackTrigger ControlID="btnPDF" />
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
                                            <asp:ListItem Value="1">COMPLETED</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divAdd" class="btnAddNew" runat="server">
                            <div class="text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Customer Name</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="true" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlCustomerName_OnSelectIndexChanged" Width="70%" ToolTip="Select Customer Number">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
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
                                </div>
                            </div>
                        </div>
                        <div id="divInput" class="divInput" runat="server">
                            <div class="ip-div text-center">
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <%--  <div class="col-md-12 row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                                    <asp:Label ID="lbltitle" runat="server" Text=""></asp:Label></p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>--%>
                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvSecondaryJobOrderDetails" runat="server" AutoGenerateColumns="False"
                                                OnRowDataBound="gvSecondaryJobOrderDetails_OnRowDataBound" OnRowCommand="gvSecondaryJobOrderDetails_OnRowCommand"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                DataKeyNames="PRIDID,RFPDID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item S.NO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemSNO" runat="server" Text='<%# Eval("ItemSNO")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Name" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Drawing Name" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldrawingname" runat="server" Text='<%# Eval("DrawingName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="PRIDID" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPRIDID" runat="server" Text='<%# Eval("PRIDID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Job Order Status" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljoborderstatus" runat="server" Text='<%# Eval("JobOrderStatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Assemply Status" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblassemplystatus" runat="server" Text='<%# Eval("AssemplyStatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Add Job" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnAdd" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="Job"><img src="../Assets/images/add1.png"/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Assembly" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnAssemply" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="Assemply"><img src="../Assets/images/add1.png"/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnPJOID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnRFPDID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnPRIDID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnMPID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnPRPDID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnNextProcess" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnMID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnPQReqWeight" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnPDID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnProcessOrder_PDF" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnProcessOrder" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnJCHID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnEditJCHID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnStages" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnPAPDID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnPartUnitReqWeight" runat="server" Value="0" />

                                            <asp:HiddenField ID="hdnMaterialIssuedStatus" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnpdfContent" runat="server" Value="0" />

                                            <asp:HiddenField ID="hdnAddress" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnPhoneAndFaxNo" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnEmail" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnWebsite" runat="server" Value="" />

                                            <asp:HiddenField ID="hdnDocNo" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnRevNo" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnRevDate" runat="server" Value="" />

                                            <asp:Button ID="btnStages" runat="server" Style="display: none;" OnClick="btnStages_Click" />
                                            <asp:LinkButton Text="PDF" Style="display: none;" CssClass="btn btn-cons btn-save  AlignTop"
                                                ID="btnPDF" runat="server" OnClick="btnPDFClick_Click" />
                                            <asp:LinkButton Text="PDF" Style="display: none;" CssClass="btn btn-cons btn-save  AlignTop"
                                                ID="btnEdit" runat="server" OnClick="btnEdit_Click" />
                                            <asp:HiddenField ID="hdnProcessName" runat="server" Value="0" />

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="divSubAssemplyWeldingPDF" style="display: none;" runat="server">
                        <div class="col-sm-12 text-center" style="background-color: #b0acac; height: 20px;">
                            <asp:Label Style="color: Black; font-weight: bolder; font-size: 15px ! important;" ID="lblProcessName_AP_PDFHeader"
                                runat="server" Text="">
                            </asp:Label>
                        </div>
                        <div style="border-style: solid;">
                            <div class="col-sm-12 p-t-10">
                                <div class="col-sm-6 text-left">
                                    <label style="color: black;">
                                        Job Order ID</label>
                                </div>
                                <div class="col-sm-6">
                                    <asp:Label ID="lblJobOrderID_AP_PDF" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12 p-t-10">
                                <div class="col-sm-6 text-left">
                                    <label style="color: black;">
                                        Date
                                    </label>
                                </div>
                                <div class="col-sm-6">
                                    <asp:Label ID="lblDate_AP_PDF" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12 p-t-10">
                                <div class="col-sm-6 text-left">
                                    <label style="color: black;">
                                        RFP No</label>
                                </div>
                                <div class="col-sm-6">
                                    <asp:Label ID="lblRFPNo_AP_PDF" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12 p-t-10">
                                <div class="col-sm-6 text-left">
                                    <label style="color: black;">
                                        Contractor Name:</label>
                                </div>
                                <div class="col-sm-6">
                                    <asp:Label ID="lblContractorName_AP_PDF" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12 p-t-10">
                                <div class="col-sm-6 text-left">
                                    <label style="color: black;">
                                        Contractor Team Member Name:</label>
                                </div>
                                <div class="col-sm-6">
                                    <asp:Label ID="lblContractorTeamMemberName_AP_PDF" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <div class="col-sm-6" style="overflow-wrap: break-word;">
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-6 text-left">
                                        <label style="color: black;">
                                            Item Name/Size</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:Label ID="lblItemNameSize_AP_PDF" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-6 text-left">
                                        <label style="color: black;">
                                            Drawing No</label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblDrawingName_AP_PDF" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-6 text-left">
                                        <label style="color: black;">
                                            Part Name</label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblPartName_AP_PDF" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-6 text-left">
                                        <label style="color: black;">
                                            Item Qty</label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblItemQty_AP_PDF" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-6 text-left">
                                        <label style="color: black;">
                                            Process Name</label>
                                    </div>
                                    <div class="col-sm-5">
                                        <asp:Label ID="lblProcessName_AP_PDF" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-6 text-left">
                                        <label style="color: black;">
                                            Weld Plan Remarks</label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblWeldPlanRemarks_AP_PDF" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-6 text-left">
                                        <label style="color: black;">
                                            Job Order Remarks</label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblJobOrderRemarks_AP_PDF" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-6 text-left">
                                        <label style="color: black;">
                                            Total Cost</label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:Label ID="lblTotalCost_AP_PDF" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="col-sm-12 p-t-10">
                                    <label style="color: Black;">
                                        Process Seriel Number Details</label>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <asp:GridView ID="gvPartSerielNumber_AP_PDF" runat="server" AutoGenerateColumns="False"
                                        ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item SNO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemSNO" runat="server" Text='<%# Eval("ItemSno")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Part to Part" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPartSNO" runat="server" Text='<%# Eval("Pair")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="col-sm-12">
                                    <label style="color: Black;">
                                        Offer QC test</label>
                                    <div class="col-sm-12 text-left">
                                        <asp:Label ID="lblOfferQCtest_AP_PDF" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divWPSDetails_AP" runat="server">
                            <div class="col-sm-12 text-center p-t-10">
                                <label style="color: Black;">
                                    WPS DETAILS</label>
                            </div>
                            <div class="col-sm-12 p-t-10 text-left">
                                <asp:GridView ID="gvWPSDetails_AP_PDF" runat="server" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                            HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Part To Part" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPartToPart" runat="server" Text='<%# Eval("PartToPart")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="WPS Number" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWPSNumber" runat="server" Text='<%# Eval("WPSNumber")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Material Grade" HeaderStyle-CssClass="text-center"
                                            ItemStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMaterialGrade" runat="server" Text='<%# Eval("MaterialGrade")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Thickness" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblThickness" runat="server" Text='<%# Eval("Thickness")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Process" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProcess" runat="server" Text='<%# Eval("Process")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Filler Grade" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFillerGrade" runat="server" Text='<%# Eval("FillerGrade")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amps" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmps" runat="server" Text='<%# Eval("Amps")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Polarity" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPolarity" runat="server" Text='<%# Eval("Polarity")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="GasLevel" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGasLevel" runat="server" Text='<%# Eval("GasLevel")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Part Assemply Amt" HeaderStyle-CssClass="text-center"
                                            ItemStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPartAssemplyAmt" runat="server" Text='<%# Eval("PartAssemplyAmount")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <div class="col-sm-12 text-center p-t-10">
                            <label style="color: Black;">
                                BOUGHT OUT ITEM ISSUE DETAILS
                            </label>
                        </div>
                        <div class="col-sm-12 p-t-10 text-left">
                            <asp:GridView ID="gvBoughtOutItemIssuedDetails_AP_PDF" runat="server" AutoGenerateColumns="False"
                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                        HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Part Name" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPartToPart" runat="server" Text='<%# Eval("PartName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Material Grade" HeaderStyle-CssClass="text-center"
                                        ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaterialGrade" runat="server" Text='<%# Eval("GradeName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MRN Number" HeaderStyle-CssClass="text-center"
                                        ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMRNNumber" runat="server" Text='<%# Eval("MRNNumber")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty" HeaderStyle-CssClass="text-center"
                                        ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQty" runat="server" Text='<%# Eval("ISSUEDWEIGHT")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UOM" HeaderStyle-CssClass="text-center"
                                        ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUomName" runat="server" Text='<%# Eval("UomName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                </Columns>
                            </asp:GridView>
                        </div>

                        <%--<div class="col-sm-12 p-t-20 text-right">
                            <div class="col-sm-4">
                                <label style="color: black; font-weight: bolder;">
                                    Quality Incharge</label>
                            </div>
                            <div class="col-sm-4">
                            </div>
                            <div class="col-sm-4">
                                <label style="color: black; font-weight: bolder;">
                                    Production Incharge</label>
                            </div>
                        </div>--%>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div id="divSheetMarkingAndCuttingPDF" runat="server" style="display: none;">
        <div id="div13" class="FrontPagepopupcontent" runat="server">
            <div class="col-sm-12 text-center" style="background-color: #b0acac; padding-top: 10px; padding-bottom: 10px;">
                <asp:Label Style="color: Black; font-weight: bolder; font-size: large ! important;" ID="lblProcessName_SMC_P_H"
                    runat="server" Text="">
                </asp:Label>
            </div>
            <div>
                <div class="row p-t-10" style="border: 2px solid; margin-top: 10px;">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Job Order ID</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblJobOrderID_SMC_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Date
                        </label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblDate_SMC_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            RFP No</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblRFPNo_SMC_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Contractor Name:</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblContractorName_SMC_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Contractor Team Member Name:</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblContractorTeamname_SMC_P" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6" style="overflow-wrap: break-word;">
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Item Name/Size</label>
                        <asp:Label ID="lblItemNameSize_SMC_P" runat="server" CssClass="lbl_v"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Drawing Name</label>
                        <asp:Label ID="lblDrawingName_SMC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Part Name</label>
                        <asp:Label ID="lblPartname_SMC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Part Qty</label>
                        <asp:Label ID="lblPartQty_SMC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Process Name</label>
                        <asp:Label ID="lblProcessname_SMC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Material Category</label>

                        <asp:Label ID="lblMaterialCategory_SMC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>

                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Material Grade</label>

                        <asp:Label ID="lblMaterialGrade_SMC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Thickness</label>

                        <asp:Label ID="lblThickness_SMC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            MRN Number</label>

                        <asp:Label ID="lblMRNNumber_SMC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Tube ID</label>

                        <asp:Label ID="lblTubeID_SMC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Tube Length</label>

                        <asp:Label ID="lblTubeLength_SMC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Job Order Remarks</label>

                        <asp:Label ID="lblJobOrderRemarks_SMC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Total Cost</label>
                        <asp:Label ID="lblTotalCost_SMC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Dead Line Date
                        </label>
                        <asp:Label ID="lbldeadlineDate_SMC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-12 p-t-10">
                        <label style="color: Black;">
                            Issue Details</label>
                    </div>
                    <div class="col-sm-12 p-t-10">
                        <asp:GridView ID="gvMRNIssueDetails_SMC_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MRN Number" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMRNNumber" runat="server" Text='<%# Eval("MRNNumber")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issued Weight" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIssuedWeight" runat="server" Text='<%# Eval("IssuedWeight")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Length" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLength" runat="server" Text='<%# Eval("Length")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Width" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWidth" runat="server" Text='<%# Eval("Width")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material Return" ItemStyle-HorizontalAlign="left"
                                    HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMatRtn" runat="server" Text='<%# Eval("MaterialReturn")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="row p-t-10">
                        <label>Layout Marking Sketch </label>
                        <div style="min-height: 100px; width: 100%; border: 1px solid;">
                        </div>
                    </div>
                    <div class="col-sm-12">
                        <label style="color: Black;">
                            Part Seriel Number</label>
                    </div>
                    <div class="col-sm-12">
                        <asp:GridView ID="gvPartSno_SMC_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item SNO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemSNO" runat="server" Text='<%# Eval("ItemSNO")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Part S.NO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPartSNO" runat="server" Text='<%# Eval("PartSNo")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-sm-12 p-t-10">
                        <asp:GridView ID="gvPLYDetails_SMC_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="NOP" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNOP" runat="server" Text='<%# Eval("NOP")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Circumference" HeaderStyle-CssClass="text-center"
                                    ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCircumference" runat="server" Text='<%# Eval("Circumferance")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Length" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLength" runat="server" Text='<%# Eval("Length")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="THK" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTHK" runat="server" Text='<%# Eval("THK")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Weight" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWeight" runat="server" Text='<%# Eval("Weight")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>

                    <div class="col-sm-12 p-t-10">
                        <label style="color: Black;">
                            Offer QC test</label>
                    </div>
                    <div class="col-sm-12 text-left">
                        <asp:Label ID="lblOfferQCTest_SMC_P" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-12 p-t-5">
                        <label>
                            Remarks
                        </label>
                        <asp:Label ID="lblOverAllRemarks_SMC_P" runat="server">
                        </asp:Label>
                    </div>
                    <div class="col-sm-12 p-t-5">
                        <label>Status </label>
                        <asp:Label ID="lbljobcardstatus_SMC_P" runat="server"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="col-sm-12 p-t-10">
                <asp:GridView ID="gvQCObservationdetails_SMC_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                    EmptyDataText="No Records Found"
                    CssClass="table table-hover table-bordered medium">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center"
                            ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Stage / Activity">
                            <ItemTemplate>
                                <asp:Label ID="lblstageactivity" runat="server" Text='<%# Eval("Stage")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Verification/Availability">
                            <ItemTemplate>
                                <asp:Label ID="txtverificationavailability" Text='<%# Eval("VerficationAvailability")%>' CssClass="mandatoryfield" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reference/Observation">
                            <ItemTemplate>
                                <asp:Label ID="txtreferenceobservation" Text='<%# Eval("ReferenceObservation")%>' CssClass="mandatoryfield"
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Verified / By" HeaderStyle-Width="20%" ItemStyle-Width="20%"
                            HeaderStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="txtverifiedBy" Text='<%# Eval("VerifiedBy")%>' CssClass="mandatoryfield"
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

        </div>
    </div>
    <div id="divMarkingAndCuttingPDF" runat="server" visible="false">
        <div id="div16" class="FrontPagepopupcontent" runat="server">
            <div class="col-sm-12 text-center" style="background-color: #b0acac; margin-top: 10px; padding-top: 10px; padding-bottom: 10px;">
                <asp:Label Style="color: Black; font-weight: bolder; font-size: large ! important;" ID="lblProcessNameHeader_MC_P"
                    runat="server" Text="">
                </asp:Label>
            </div>
            <div>
                <div class="col-sm-12 p-t-10" style="border: 2px solid; margin-top: 10px;">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Job Order ID</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblJobOrderID_MC_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-12 contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Date
                        </label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblDate_MC_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-12 contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            RFP No</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblRFPNo_MC_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-12 contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Contractor Name:</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblContractorName_MC_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-12 contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Contractor Team Member Name:</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblContractorTeamMemberName_MC_P" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="col-sm-12">
                <div class="col-sm-6" style="overflow-wrap: break-word;">
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Item Name/Size</label>
                        <asp:Label ID="lblItemNameSize_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Drawing Name</label>
                        <asp:Label ID="lblDrawingname_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Part Name</label>
                        <asp:Label ID="lblPartName_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Part Qty</label>
                        <asp:Label ID="lblPartQty_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Process Name</label>
                        <asp:Label ID="lblProcessName_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Material Category</label>
                        <asp:Label ID="lblMaterialCategory_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Material Grade</label>
                        <asp:Label ID="lblmaterialGrade_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Thickness</label>
                        <asp:Label ID="lblThickness_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            MRN Number</label>
                        <asp:Label ID="lblMRNNumber_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Job Order Remarks</label>
                        <asp:Label ID="lblJobOrderRemarks_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Total Cost</label>
                        <asp:Label ID="lblTotalCost_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="col-sm-12 p-t-10">
                        <label style="color: Black;">
                            Part Seriel Number</label>
                    </div>
                    <div class="col-sm-12">
                        <asp:GridView ID="gvPartSerialNo_MC_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item SNO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemSNO" runat="server" Text='<%# Eval("ItemSNO")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Part S.NO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPartSNO" runat="server" Text='<%# Eval("PartSNo")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-sm-12 p-t-10">
                        <label style="color: Black;">
                            Offer QC test</label>
                    </div>
                    <div class="col-sm-12 text-left">
                        <asp:Label ID="lblOfferQC_MC_P" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-12 p-t-10">
                        <asp:Label ID="lblFabricationType_MC_P" Style="font-weight: bold;" runat="server"></asp:Label>
                    </div>
                    <div id="divfabrication_MC_P" runat="server">
                        <%--  <div class="col-sm-12">
                            <div class="col-sm-6">
                                <label>fabricationName</label>
                            </div>
                            <div class="col-sm-1">
                                <label>:</label>
                            </div>
                            <div class="col-sm-5">
                                <label>fabricationValue</label>
                            </div>
                        </div>--%>
                    </div>
                    <div class="col-sm-12 p-t-10">
                        <label style="color: Black;">
                            Issue Details</label>
                    </div>
                    <div class="col-sm-12 p-t-10">
                        <asp:GridView ID="gvIssueDetails_MC_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MRN Number" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMRNNumber" runat="server" Text='<%# Eval("MRNNumber")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issued Weight" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIssuedWeight" runat="server" Text='<%# Eval("IssuedWeight")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Length" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLength" runat="server" Text='<%# Eval("Length")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Width" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWidth" runat="server" Text='<%# Eval("Width")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Material Return" ItemStyle-HorizontalAlign="left"
                                    HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMatRtn" runat="server" Text='<%# Eval("MaterialReturn")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>

                </div>
            </div>

        </div>
    </div>

    <div id="divSheetWeldingPDF" runat="server" style="display: none;">
        <div id="d" class="FrontPagepopupcontent" runat="server">
            <div class="col-sm-12 text-center" style="background-color: #b0acac; padding-top: 10px; padding-bottom: 10px;">
                <asp:Label Style="color: Black; font-weight: bolder; font-size: large ! important;" ID="lblProcessName_SW_P_H"
                    runat="server" Text="">
                </asp:Label>
            </div>
            <div>
                <div class="row p-t-10" style="border: 2px solid; margin-top: 10px;">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Job Order ID</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblJobOrderID_SW_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Date
                        </label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblDate_SW_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            RFP No</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblRFPNo_SW_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Contractor Name:</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblContractorName_SW_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Contractor Team Member Name:</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblContractorTeamname_SW_P" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6" style="overflow-wrap: break-word;">
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Item Name/Size</label>
                        <asp:Label ID="lblItemNameSize_SW_P" runat="server" CssClass="lbl_v"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Drawing Name</label>
                        <asp:Label ID="lblDrawingName_SW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Part Name</label>
                        <asp:Label ID="lblPartname_SW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Part Qty</label>
                        <asp:Label ID="lblPartQty_SW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Process Name</label>
                        <asp:Label ID="lblProcessname_SW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Material Category</label>

                        <asp:Label ID="lblMaterialCategory_SW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>

                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Material Grade</label>

                        <asp:Label ID="lblMaterialGrade_SW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Thickness</label>

                        <asp:Label ID="lblThickness_SW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>

                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            NOP</label>

                        <asp:Label ID="lblNOP_SW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>

                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            MRN Number</label>

                        <asp:Label ID="lblMRNNumber_SW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>

                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Job Order Remarks</label>

                        <asp:Label ID="lblJobOrderRemarks_SW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Total Cost</label>
                        <asp:Label ID="lblTotalCost_SW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Dead Line Date
                        </label>
                        <asp:Label ID="lblDeadlineDate_SW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="row p-t-10">
                        <label>Layout Marking Sketch </label>
                        <div style="min-height: 100px; width: 100%; border: 1px solid;">
                        </div>
                    </div>
                    <div class="col-sm-12 p-t-10">
                        <label style="color: Black;">
                            Part Seriel Number</label>
                    </div>
                    <div class="col-sm-12">
                        <asp:GridView ID="gvPartSno_SW_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item SNO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemSNO" runat="server" Text='<%# Eval("ItemSNO")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Part S.NO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPartSNO" runat="server" Text='<%# Eval("PartSNo")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-sm-12 p-t-10">
                        <label style="color: Black;">
                            Offer QC test</label>
                    </div>
                    <div class="col-sm-12 text-left">
                        <asp:Label ID="lblOfferQCTest_SW_P" runat="server"></asp:Label>
                    </div>
                    <div style="padding-left: 2px;">
                        <label>
                            Remarks
                        </label>
                        <asp:Label ID="lblRemarks_SW_P" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-12 p-t-5">
                        <label>Status </label>
                        <asp:Label ID="lbljobcardstatus_SW_P" runat="server"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="col-sm-12 p-t-10">
                <label style="color: Black;">
                    WPS Details</label>
            </div>
            <div class="col-sm-12 p-t-10">
                <asp:GridView ID="gvWPSDetails_SW_P" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="True"
                    EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                    <Columns>
                        <asp:TemplateField HeaderText="WPS Number" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblMRNNumber" runat="server" Text='<%# Eval("WPSNumber")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Material Grade" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblIssuedWeight" runat="server" Text='<%# Eval("MaterialGrade")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="THK" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblLength" runat="server" Text='<%# Eval("Thickness")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Process" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblWidth" runat="server" Text='<%# Eval("Process")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Filler Grade" ItemStyle-HorizontalAlign="left"
                            HeaderStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblMatRtn" runat="server" Text='<%# Eval("FillerGrade")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amps" ItemStyle-HorizontalAlign="left"
                            HeaderStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblMatRtn" runat="server" Text='<%# Eval("Amps")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Polarity" ItemStyle-HorizontalAlign="left"
                            HeaderStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblMatRtn" runat="server" Text='<%# Eval("Polarity")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Gas Level" ItemStyle-HorizontalAlign="left"
                            HeaderStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblMatRtn" runat="server" Text='<%# Eval("Gaslevel")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <div class="text-center">
                <label>BEFORE WELDING </label>
            </div>
            <div class="col-sm-12 p-t-10">
                <asp:GridView ID="gvbeforewelding_SW_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                    EmptyDataText="No Records Found"
                    CssClass="table table-hover table-bordered medium">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center"
                            ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Stage / Activity">
                            <ItemTemplate>
                                <asp:Label ID="lblstageactivity" runat="server" Text='<%# Eval("Stage")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Verification/Availability">
                            <ItemTemplate>
                                <asp:Label ID="txtverificationavailability" Text='<%# Eval("VerficationAvailability")%>' CssClass="mandatoryfield" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reference/Observation">
                            <ItemTemplate>
                                <asp:Label ID="txtreferenceobservation" Text='<%# Eval("ReferenceObservation")%>' CssClass="mandatoryfield"
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Verified / By" HeaderStyle-Width="20%" ItemStyle-Width="20%"
                            HeaderStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="txtverifiedBy" Text='<%# Eval("VerifiedBy")%>' CssClass="mandatoryfield"
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <div class="text-center p-t-10">
                <label>DURING WELDING </label>
            </div>
            <div class="col-sm-12 p-t-10">
                <asp:GridView ID="gvduringwelding_SW_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                    EmptyDataText="No Records Found"
                    CssClass="table table-hover table-bordered medium">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center"
                            ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Stage / Activity">
                            <ItemTemplate>
                                <asp:Label ID="lblstageactivity" runat="server" Text='<%# Eval("Stage")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Verification/Availability">
                            <ItemTemplate>
                                <asp:Label ID="txtverificationavailability" Text='<%# Eval("VerficationAvailability")%>' CssClass="mandatoryfield" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reference/Observation">
                            <ItemTemplate>
                                <asp:Label ID="txtreferenceobservation" Text='<%# Eval("ReferenceObservation")%>' CssClass="mandatoryfield"
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Verified / By" HeaderStyle-Width="20%" ItemStyle-Width="20%"
                            HeaderStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="txtverifiedBy" Text='<%# Eval("VerifiedBy")%>' CssClass="mandatoryfield"
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <div class="text-center p-t-10">
                <label>FINAL WELDING </label>
            </div>
            <div class="col-sm-12 p-t-10">
                <asp:GridView ID="gvfinalwelding_SW_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                    EmptyDataText="No Records Found"
                    CssClass="table table-hover table-bordered medium">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center"
                            ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Stage / Activity">
                            <ItemTemplate>
                                <asp:Label ID="lblstageactivity" runat="server" Text='<%# Eval("Stage")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Verification/Availability">
                            <ItemTemplate>
                                <asp:Label ID="txtverificationavailability" Text='<%# Eval("VerficationAvailability")%>' CssClass="mandatoryfield" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reference/Observation">
                            <ItemTemplate>
                                <asp:Label ID="txtreferenceobservation" Text='<%# Eval("ReferenceObservation")%>' CssClass="mandatoryfield"
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Verified / By" HeaderStyle-Width="20%" ItemStyle-Width="20%"
                            HeaderStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="txtverifiedBy" Text='<%# Eval("VerifiedBy")%>' CssClass="mandatoryfield"
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

        </div>
    </div>

    <div id="divFabricationAndWeldingPDF" runat="server" visible="false">
        <div id="d1" class="FrontPagepopupcontent" runat="server">
            <div class="col-sm-12 text-center" style="background-color: #b0acac; margin-top: 10px; padding-top: 10px; padding-bottom: 10px;">
                <asp:Label Style="color: Black; font-weight: bolder; font-size: large ! important;" ID="lblProcessName_FW_P_H"
                    runat="server" Text="">
                </asp:Label>
            </div>
            <div>
                <div class="col-sm-12 p-t-10" style="border: 2px solid; margin-top: 10px;">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Job Order ID</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblJobOrderID_FW_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-12 contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Date
                        </label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblDate_FW_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-12 contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            RFP No</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblRFPNo_FW_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-12 contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Contractor Name:</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblContractorName_FW_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-12 contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Contractor Team Member Name:</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblContractorTeamname_FW_P" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="col-sm-12">
                <div class="col-sm-6" style="overflow-wrap: break-word;">
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Item Name/Size</label>
                        <asp:Label ID="lblItemNameSize_FW_P" runat="server" CssClass="lbl_v"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Drawing Name</label>
                        <asp:Label ID="lblDrawingName_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Part Name</label>
                        <asp:Label ID="lblPartname_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Part Qty</label>
                        <asp:Label ID="lblPartQty_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Process Name</label>
                        <asp:Label ID="lblProcessname_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Material Category</label>

                        <asp:Label ID="lblMaterialCategory_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>

                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Material Grade</label>

                        <asp:Label ID="lblMaterialGrade_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Thickness</label>

                        <asp:Label ID="lblThickness_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>

                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            NOP</label>

                        <asp:Label ID="lblNOP_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>

                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            MRN Number</label>

                        <asp:Label ID="lblMRNNumber_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>

                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Job Order Remarks</label>

                        <asp:Label ID="lblJobOrderRemarks_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Total Cost</label>
                        <asp:Label ID="lblTotalCost_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="col-sm-12 p-t-10">
                        <label style="color: Black;">
                            Part Seriel Number</label>
                    </div>
                    <div class="col-sm-12">
                        <asp:GridView ID="gvPartSno_FW_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item SNO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemSNO" runat="server" Text='<%# Eval("ItemSNO")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Part S.NO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPartSNO" runat="server" Text='<%# Eval("PartSNo")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-sm-12 p-t-10">
                        <label style="color: Black;">
                            Offer QC test</label>
                    </div>
                    <div class="col-sm-12 text-left">
                        <asp:Label ID="lblOfferQCTest_FW_P" runat="server"></asp:Label>
                    </div>

                    <div class="col-sm-12 p-t-10">
                        <asp:Label ID="lblFabricationType_FW_P" Style="font-weight: bold;" runat="server"></asp:Label>
                    </div>
                    <div id="divfabrication_FW_P" runat="server">
                    </div>
                </div>
                <div class="col-sm-12 p-t-10">
                    <label style="color: Black;">
                        WPS Details</label>
                </div>
                <div class="col-sm-12 p-t-10">
                    <asp:GridView ID="gvWPSDetails_FW_P" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="True"
                        EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                        <Columns>
                            <asp:TemplateField HeaderText="WPS Number" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                <ItemTemplate>
                                    <asp:Label ID="lblMRNNumber" runat="server" Text='<%# Eval("WPSNumber")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Material Grade" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                <ItemTemplate>
                                    <asp:Label ID="lblIssuedWeight" runat="server" Text='<%# Eval("MaterialGrade")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="THK" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                <ItemTemplate>
                                    <asp:Label ID="lblLength" runat="server" Text='<%# Eval("Thickness")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Process" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                <ItemTemplate>
                                    <asp:Label ID="lblWidth" runat="server" Text='<%# Eval("Process")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Filler Grade" ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="left">
                                <ItemTemplate>
                                    <asp:Label ID="lblMatRtn" runat="server" Text='<%# Eval("FillerGrade")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amps" ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="left">
                                <ItemTemplate>
                                    <asp:Label ID="lblMatRtn" runat="server" Text='<%# Eval("Amps")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Polarity" ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="left">
                                <ItemTemplate>
                                    <asp:Label ID="lblMatRtn" runat="server" Text='<%# Eval("Polarity")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Gas Level" ItemStyle-HorizontalAlign="left"
                                HeaderStyle-HorizontalAlign="left">
                                <ItemTemplate>
                                    <asp:Label ID="lblMatRtn" runat="server" Text='<%# Eval("Gaslevel")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>

    <div id="divBellowFormingAndTangentCuttingPDF" runat="server" style="display: none;">
        <div id="BFTC" class="FrontPagepopupcontent" runat="server">
            <div class="col-sm-12 text-center" style="background-color: #b0acac; padding-top: 10px; padding-bottom: 10px;">
                <asp:Label Style="color: Black; font-weight: bolder; font-size: large ! important;" ID="lblProcessName_BFTC_P_H"
                    runat="server" Text="">
                </asp:Label>
            </div>
            <div>
                <div class="row p-t-10" style="border: 2px solid; margin-top: 10px;">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Job Order ID</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblJobOrderID_BFTC_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Date
                        </label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblDate_BFTC_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            RFP No</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblRFPNo_BFTC_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Contractor Name:</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblContractorName_BFTC_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Contractor Team Member Name:</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblContractorTeamname_BFTC_P" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6" style="overflow-wrap: break-word;">
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Item Name/Size</label>
                        <asp:Label ID="lblItemNameSize_BFTC_P" runat="server" CssClass="lbl_v"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Drawing Name</label>
                        <asp:Label ID="lblDrawingName_BFTC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Part Name</label>
                        <asp:Label ID="lblPartname_BFTC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Part Qty</label>
                        <asp:Label ID="lblPartQty_BFTC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Process Name</label>
                        <asp:Label ID="lblProcessname_BFTC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Material Category</label>

                        <asp:Label ID="lblMaterialCategory_BFTC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>

                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Material Grade</label>

                        <asp:Label ID="lblMaterialGrade_BFTC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Thickness</label>

                        <asp:Label ID="lblThickness_BFTC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>

                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            NOP</label>

                        <asp:Label ID="lblNOP_BFTC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>

                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            MRN Number</label>

                        <asp:Label ID="lblMRNNumber_BFTC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>

                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Job Order Remarks</label>

                        <asp:Label ID="lblJobOrderRemarks_BFTC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Total Cost</label>
                        <asp:Label ID="lblTotalCost_BFTC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Dead Line Date
                        </label>
                        <asp:Label ID="lbldeadlineDate_BFTC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="col-sm-12 p-t-10">
                        <label style="color: Black;">
                            Part Seriel Number</label>
                    </div>
                    <div class="col-sm-12">
                        <asp:GridView ID="gvPartSno_BFTC_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item SNO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemSNO" runat="server" Text='<%# Eval("ItemSNO")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Part S.NO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPartSNO" runat="server" Text='<%# Eval("PartSNo")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-sm-12 p-t-10">
                        <label style="color: Black;">
                            Offer QC test</label>
                    </div>
                    <div class="col-sm-12 text-left">
                        <asp:Label ID="lblOfferQCTest_BFTC_P" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-12 p-t-10">
                        <asp:Label ID="lblBellowDetails_BFTC_P" Style="font-weight: bold;" runat="server"></asp:Label>
                    </div>
                    <div id="divBellowDetails_BFTC_P" runat="server">
                    </div>
                    <div style="padding-left: 2px;">
                        <label>
                            Remarks
                        </label>
                        <asp:Label ID="lblremarks_BETC_P" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-12 p-t-5">
                        <label>Status </label>
                        <asp:Label ID="lbljobcardstatus_BFTC_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div id="divNumberOfStages_BFTC_P" runat="server" style="display: none;">
                    <div class="col-sm-12 p-t-10">
                        <asp:GridView ID="gvNumberOfStages_BFTC_P" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="True"
                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                            <Columns>
                                <asp:TemplateField HeaderText="Stages" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMRNNumber" runat="server" Text='<%# Eval("Stages")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inner" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMRNNdumber" runat="server" Text='<%# Eval("Inner")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Outer" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMRdfNNumber" runat="server" Text='<%# Eval("outer")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Gap" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMRNsfNumber" runat="server" Text='<%# Eval("gap")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <div class="col-sm-12 p-t-10">
                <asp:GridView ID="gvStageActivity_BFTC_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                    EmptyDataText="No Records Found"
                    CssClass="table table-hover table-bordered medium">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center"
                            ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Stage / Activity">
                            <ItemTemplate>
                                <asp:Label ID="lblstageactivity" runat="server" Text='<%# Eval("Stage")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Verification/Availability">
                            <ItemTemplate>
                                <asp:Label ID="txtverificationavailability" Text='<%# Eval("VerficationAvailability")%>' CssClass="mandatoryfield" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reference/Observation">
                            <ItemTemplate>
                                <asp:Label ID="txtreferenceobservation" Text='<%# Eval("ReferenceObservation")%>' CssClass="mandatoryfield"
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Verified / By" HeaderStyle-Width="20%" ItemStyle-Width="20%"
                            HeaderStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="txtverifiedBy" Text='<%# Eval("VerifiedBy")%>' CssClass="mandatoryfield"
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

    <div class="modal" id="mpePartDetails" style="overflow-y: scroll;">
        <div style="max-width: 81%; padding-left: 10%; padding-top: 5%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">Part Details</h4>
                            <asp:Label ID="lblItemName_P" runat="server" Style="padding-left: 19px; padding-top: 10px; color: #c12424;"></asp:Label>
                            <div style="display: grid; text-align: center; color: #000; font-weight: bold;">
                                <span>PA - Pyment Added </span>
                                <span>PNA - Pyment Not Added </span>
                            </div>
                            <button type="button" class="close btn-primary-purple" onclick="hidePartDetailpopup();"
                                data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="Div3" class="docdiv">
                                <div class="inner-container">
                                    <div id="Div4" runat="server">
                                        <div id="div5" class="divInput" runat="server">
                                        </div>
                                        <div id="div6" runat="server">

                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvPartDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium orderingfalse"
                                                    OnRowCommand="gvPartDetails_OnRowCommand" OnRowDataBound="gvPartDetails_OnRowDataBound"
                                                    DataKeyNames="PRPDID,MPID,NextProcess,MID,JCHID">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Part Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPartName" runat="server" Text='<%# Eval("PartName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Part S.NO" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPartSNO" runat="server" Text='<%# Eval("PartSNO")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Process Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblProcessName" runat="server" Text='<%# Eval("Process").ToString().Replace(",", "<br/>").Replace("commareplace", ",").Replace("&lt;", "<").Replace("&gt;", ">")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Add Job Card" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnAddJobCard" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                    CommandName="addJobCard">
                                                                    <img src="../Assets/images/add1.png" />
                                                                </asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <div class="col-sm-12 text-center">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal" id="mpeJobCardDetails" style="overflow-y: scroll;">
        <div style="max-width: 95%; padding-left: 5%; padding-top: 5%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSaveJobCard" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">Job Card Details</h4>
                            <asp:Label ID="lblParNameAndtProcessName_J" runat="server" Style="padding-left: 19px; padding-top: 10px; color: #c12424;"></asp:Label>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true"
                                onclick="HideJobCardPopUp();">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="Div2" class="docdiv">
                                <div class="inner-container">
                                    <div id="divInput_Job" runat="server">
                                        <div id="div8" class="divInput" runat="server">
                                            <div class="ip-div text-center">
                                                <div class="col-sm-12">
                                                    <label style="font-size: larger; font-weight: bolder;">
                                                        Job Order ID:
                                                    </label>
                                                    <asp:Label ID="lblSecondaryJobOrderID_J" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-12">
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            RFP NO:
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblRFPNo_J" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-3 text-right">
                                                        <label>
                                                            Item Name/Size
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblItemSizeName_J" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Drawing Name
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblDrawingName_J" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-3 text-right">
                                                        <label>
                                                            Part Name
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblPartName_J" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Category Name
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblCategoryName_J" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-3 text-right">
                                                        <label>
                                                            Material Grade Name
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblGradeName_J" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Thickness
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblThickness_J" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Offer QC</label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblOfferQC_J" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div id="divMarkingAndCutting_J" runat="server" visible="false">
                                                    <div class="col-sm-12 p-t-10" style="margin-left: 30%;">
                                                        <%--    <label class="form-label">
                                                            Part Name Measurment Details</label>--%>
                                                        <asp:RadioButtonList ID="rblPartOperation" runat="server" CssClass="radio radio-success" AutoPostBack="true"
                                                            OnSelectedIndexChanged="rblPartOperation_IndexChanged" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                            <asp:ListItem Selected="True" Value="PlateCutting">If Plate Cutting</asp:ListItem>
                                                            <asp:ListItem Value="Rings">If Rings</asp:ListItem>
                                                            <asp:ListItem Value="Gimbal">If Gimbal</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                    <div id="divPlateCutting_J" runat="server">
                                                        <div class="col-sm-12 p-t-10">
                                                            <div class="col-sm-4 text-right">
                                                                <label class="form-label">
                                                                    Diameter
                                                                </label>
                                                            </div>
                                                            <div class="col-sm-4">
                                                                <%-- <asp:Label ID="lblDiameter_J" runat="server"></asp:Label>--%>
                                                                <asp:TextBox ID="txtDiameter_J" runat="server" placeholder="Enter Diameter" ToolTip="Enter Diameter"
                                                                    CssClass="form-control mandatoryfield" MaxLength="300" autocomplete="nope"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-4">
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-12 p-t-10">
                                                            <div class="col-sm-4 text-right">
                                                                <label class="form-label">
                                                                    Plate  Length
                                                                </label>
                                                            </div>
                                                            <div class="col-sm-4 text-left">
                                                                <asp:TextBox ID="txtPlateLength_J" runat="server" placeholder="Enter Plate Length" ToolTip="Enter Plate Length"
                                                                    CssClass="form-control mandatoryfield" MaxLength="300" autocomplete="nope"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-4 text-left">
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-12 p-t-10">
                                                            <div class="col-sm-4 text-right">
                                                                <label class="form-label">
                                                                    Plate  Width
                                                                </label>
                                                            </div>
                                                            <div class="col-sm-4 text-left">
                                                                <asp:TextBox ID="txtPlateWidth_J" runat="server" placeholder="Enter Plate Width" ToolTip="Enter Plate Width"
                                                                    CssClass="form-control mandatoryfield" MaxLength="300" autocomplete="nope"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-4 text-left">
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div id="divRings_J" runat="server" visible="false">
                                                        <div class="col-sm-12 p-t-10">
                                                            <div class="col-sm-4 text-right">
                                                                <label class="form-label">
                                                                    OD
                                                                </label>
                                                            </div>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="txtOD_J" runat="server" placeholder="Enter OD" ToolTip="Enter OD"
                                                                    CssClass="form-control mandatoryfield" MaxLength="300" autocomplete="nope"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-4">
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-12 p-t-10">
                                                            <div class="col-sm-4 text-right">
                                                                <label class="form-label">
                                                                    ID
                                                                </label>
                                                            </div>
                                                            <div class="col-sm-4 text-left">
                                                                <asp:TextBox ID="txtID_J" runat="server" placeholder="Enter ID" ToolTip="Enter ID"
                                                                    CssClass="form-control mandatoryfield" MaxLength="300" autocomplete="nope"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-4 text-left">
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-12 p-t-10">
                                                            <div class="col-sm-4 text-right">
                                                                <label class="form-label">
                                                                    NO Of Segments
                                                                </label>
                                                            </div>
                                                            <div class="col-sm-4 text-left">
                                                                <asp:TextBox ID="txtNoOfSegments_J" runat="server" placeholder="Enter No Of Segments" ToolTip="Enter No Of Segments"
                                                                    CssClass="form-control mandatoryfield" MaxLength="300" autocomplete="nope"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-4 text-left">
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div id="divGimbal_J" runat="server" visible="false">
                                                        <div class="col-sm-12 p-t-10">
                                                            <div class="col-sm-4 text-right">
                                                                <label class="form-label">
                                                                    Plate 1
                                                                </label>
                                                            </div>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="txtPlate1_J" runat="server" placeholder="Enter Plate 1"
                                                                    CssClass="form-control mandatoryfield" MaxLength="300" autocomplete="nope"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-4">
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-12 p-t-10">
                                                            <div class="col-sm-4 text-right">
                                                                <label class="form-label">
                                                                    Plate 2
                                                                </label>
                                                            </div>
                                                            <div class="col-sm-4 text-left">
                                                                <asp:TextBox ID="txtPlate2_J" runat="server" placeholder="Enter Plate 2"
                                                                    CssClass="form-control mandatoryfield" MaxLength="300" autocomplete="nope"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-4 text-left">
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-12 p-t-10">
                                                            <div class="col-sm-4 text-right">
                                                                <label class="form-label">
                                                                    Width
                                                                </label>
                                                            </div>
                                                            <div class="col-sm-4 text-left">
                                                                <asp:TextBox ID="txtWidth_J" runat="server" placeholder="Enter Width"
                                                                    CssClass="form-control mandatoryfield" MaxLength="300" autocomplete="nope"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-4 text-left">
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-12 p-t-10">
                                                            <div class="col-sm-4 text-right">
                                                                <label class="form-label">
                                                                    Plate 1 Qty
                                                                </label>
                                                            </div>
                                                            <div class="col-sm-4">
                                                                <asp:TextBox ID="txtPlate1Qty_J" runat="server" placeholder="Enter Plate 1 Qty"
                                                                    CssClass="form-control mandatoryfield" MaxLength="300" autocomplete="nope"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-4">
                                                            </div>
                                                        </div>
                                                        <div class="col-sm-12 p-t-10">
                                                            <div class="col-sm-4 text-right">
                                                                <label class="form-label">
                                                                    Plate 2 Qty
                                                                </label>
                                                            </div>
                                                            <div class="col-sm-4 text-left">
                                                                <asp:TextBox ID="txtPlate2Qty_J" runat="server" placeholder="Enter Plate 2 Qty"
                                                                    CssClass="form-control mandatoryfield" MaxLength="300" autocomplete="nope"></asp:TextBox>
                                                            </div>
                                                            <div class="col-sm-4 text-left">
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-12 p-t-10">
                                                        <div class="col-sm-3 text-right">
                                                            <label class="form-label mandatorylbl">
                                                                Cutting Process
                                                            </label>
                                                        </div>
                                                        <div class="col-sm-3 text-left">
                                                            <%--   <asp:Label ID="lblCuttingProcess_J" runat="server"></asp:Label>--%>
                                                            <asp:DropDownList ID="ddlCuttingProcess_J" runat="server" CssClass="form-control mandatoryfield"
                                                                ToolTip="Select Cutting Process Name">
                                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-3 text-left">
                                                        </div>
                                                        <div class="col-sm-3">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10" id="divSheetMarkingAndCutting_J" runat="server" visible="false"
                                                    style="overflow-x: scroll;">
                                                    <asp:GridView ID="gvPLYDetails_J" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                        EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                                HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="NOP" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNOP" runat="server" Text='<%# Eval("NOP")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Circumference" HeaderStyle-CssClass="text-center"
                                                                ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCircumference" runat="server" Text='<%# Eval("Circumferance")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Length" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLength" runat="server" Text='<%# Eval("Length")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="THK" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTHK" runat="server" Text='<%# Eval("THK")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Weight" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWeight" runat="server" Text='<%# Eval("Weight")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                                <div class="col-sm-12 p-t-10" id="divcontractornamedetails_J" runat="server">
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label mandatorylbl">
                                                            Contractor Name</label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:DropDownList ID="ddlContractorName_J" runat="server" CssClass="form-control mandatoryfield"
                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlContractorName_J_OnSelectedIndexChanged"
                                                            ToolTip="Select Contractor Name">
                                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label mandatorylbl">
                                                            Contractor Team Member Name</label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:DropDownList ID="ddlContractorTeamMemberName_J" runat="server" CssClass="form-control mandatoryfield"
                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlContractorTeamMemberName_J_OnSelectedIndexChanged"
                                                            ToolTip="Select Contractor Team Member Name">
                                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label mandatorylbl">
                                                            Issue Date</label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:TextBox ID="txtIssueDate_J" runat="server" placeholder="Enter Issue Date" ToolTip="Enter Issue Date"
                                                            CssClass="form-control mandatoryfield datepicker" MaxLength="300" autocomplete="nope"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Delivery Date</label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:TextBox ID="txtDeliveryDate_J" runat="server" placeholder="Enter Delivery Date"
                                                            ToolTip="Enter Delivery Date" CssClass="form-control mandatoryfield datepicker"
                                                            MaxLength="300" autocomplete="nope"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-6">
                                                        <div id="divpartqty" runat="server" visible="false">
                                                            <div class="col-sm-3 text-right">
                                                                <label class="form-label mandatorylbl" style="display: contents;">
                                                                    Part Quantity</label>
                                                                <asp:Label ID="lblPartQuantity_J" Style="color: #c12424;" runat="server"></asp:Label>
                                                            </div>
                                                            <div class="col-sm-3 text-left">
                                                                <asp:TextBox ID="txtPartQuantity_J" runat="server" placeholder="Enter Part Quantity"
                                                                    onblur="BindPartSNoAndMRNDetails(this);" ToolTip="Enter Part Quantity" CssClass="form-control mandatoryfield"
                                                                    autocomplete="nope"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <label class="form-label" style="display: contents;">
                                                            Upload</label>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:FileUpload ID="fpAttchements" runat="server" TabIndex="12" CssClass="form-control"></asp:FileUpload>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10" runat="server" id="divTubeIDAndTubeLength_J" visible="false">
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label mandatorylbl" style="display: contents;">
                                                            Tube Id
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:TextBox ID="txtTubeId_J" runat="server" placeholder="Enter Tube Id" ToolTip="Enter Tube Id"
                                                            CssClass="form-control mandatoryfield" MaxLength="300" autocomplete="nope"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <label class="form-label" style="display: contents;">
                                                            Tube Length</label>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtTubeLength_J" runat="server" placeholder="Enter Tube Length" ToolTip="Enter Tube Length"
                                                            CssClass="form-control mandatoryfield" MaxLength="300" autocomplete="nope"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Job Order Remarks</label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:TextBox ID="txtJobOrderRemarks_J" runat="server" placeholder="Enter Remarks"
                                                            CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-3">
                                                    </div>
                                                    <div class="col-sm-3">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <label>
                                                Process Seriel Number Details</label>
                                        </div>

                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvItemPartSNODetails" runat="server" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                OnRowDataBound="gvItemPartSNODetails_OnRowDataBound" DataKeyNames="PRPDID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkall" runat="server" onclick="return chekAllPartSno(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkPartSno" runat="server" onchange="CalculateMrnReqWeight();"
                                                                AutoPostBack="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item SNO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemSNO" runat="server" Text='<%# Eval("ItemSNO")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Part S.NO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartSNO" runat="server" Text='<%# Eval("PartSNO")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <label>
                                                MRN Issue Details</label>
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll; display: block;" runat="server" id="divMRNDetails">
                                            <asp:GridView ID="gvMRNDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium" OnRowCommand="gvMRNDetails_OnRowCommand"
                                                DataKeyNames="MRNID,JCMRNID,ReturnedLayout,JobLayoutAttachID,MRNLocationName,MRNLocationID" OnRowDataBound="gvMRNDetails_OnRowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkMRN" runat="server" CssClass="MRN" onclick="return MandatoryField(this);"
                                                                AutoPostBack="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MRN Number" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnMRNNumber" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="ViewMRNDetails">
                                                                <asp:Label ID="lblMRNNumber" runat="server" Text='<%# Eval("MRNNumber")%>'></asp:Label>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Blocked Weight" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBlockedWeight" runat="server" Text='<%# Eval("BlockedWeight")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Availabel Weight" ItemStyle-HorizontalAlign="left"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAvalableweight" CssClass="AvailableWeight" runat="server" Text='<%# Eval("AVAILABLE")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Issued Weight" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtIssuedWeight" runat="server" Text='<%# Eval("ISSUEDWEIGHT")%>'
                                                                onkeypress="return fnAllowNumeric();" onblur="ValidateIssuedWeight();" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Issued Length" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtLength" runat="server" onkeypress="return fnAllowNumeric();"
                                                                Text='<%# Eval("LENGTH")%>' CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Issued Width" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtWidth" runat="server" onkeypress="return fnAllowNumeric();" Text='<%# Eval("WIDTH")%>'
                                                                CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rtn Length" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRtnLength" runat="server" onkeypress="return fnAllowNumeric();"
                                                                Text='<%# Eval("RtnLength")%>' CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rtn Width" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRtnWidth" runat="server" onkeypress="return fnAllowNumeric();" Text='<%# Eval("RtnWidth")%>'
                                                                CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cut Layout" HeaderStyle-Width="15%" ItemStyle-Width="15%"
                                                        ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:FileUpload ID="fpCutLayout" runat="server" CssClass="form-control"></asp:FileUpload>
                                                            <asp:LinkButton ID="btnCutLayout" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="ViewCutLayout">
                                                            <img src="../Assets/images/view.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Returned Layout" HeaderStyle-Width="15%" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="left"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:FileUpload ID="fpReturnedLayout" runat="server" CssClass="form-control"></asp:FileUpload>
                                                            <asp:LinkButton ID="btnViewReturnedLayout" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="ViewReturnedLayout">
                                                            <img src="../Assets/images/view.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--  <asp:TemplateField HeaderText="Material Return" ItemStyle-HorizontalAlign="left"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkMaterialReturn" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <div class="col-sm-12 text-center">
                                        <asp:LinkButton Text="Save" CssClass="btn btn-cons btn-save  AlignTop" ID="btnSaveJobCard"
                                            runat="server" OnClientClick="return ValidateSave();" OnClick="btnSaveJobCard_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="HiddenField2" runat="server" Value="0" />
                        <ifram id="ifrm" style="display: none;" runat="server"></ifram>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal" id="mpeViewdocs" style="height: 125%; top: -10%;">
        <div class="modal-dialog" style="height: 200%;">
            <div class="modal-content" style="height: 100%;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always" Style="height: 100%;">
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
                                    <asp:LinkButton ID="btndownloaddocs" OnClick="btndownloaddocs_Click" ToolTip="PDF DownLoad"
                                        runat="server">
                                        <img src="../Assets/images/pdf.png" />
                                    </asp:LinkButton>
                                </div>
                                <div id="divJoborderPdf" class="FrontPagepopupcontent" runat="server">
                                    <div class="col-sm-12" id="divLSERPLogo" style="padding-top: 30px;" runat="server">
                                        <img src="../Assets/images/topstrrip.jpg" alt="" width="100%">
                                    </div>
                                    <div class="col-sm-12 text-center" style="background-color: #b0acac;">
                                        <asp:Label Style="color: Black; font-weight: bolder; font-size: large;" ID="lblProcessNameHeader_p"
                                            runat="server" Text="">
                                        </asp:Label>
                                    </div>
                                    <div style="border-style: solid;">
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-6 text-left">
                                                <label style="color: black;">
                                                    Job Order ID</label>
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:Label ID="lblJobOrderID_p" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-6 text-left">
                                                <label style="color: black;">
                                                    Date
                                                </label>
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:Label ID="lblDate_p" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-6 text-left">
                                                <label style="color: black;">
                                                    RFP No</label>
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:Label ID="lblRFPNo_p" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-6 text-left">
                                                <label style="color: black;">
                                                    Contractor Name:</label>
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:Label ID="lblContractorName_p" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-6 text-left">
                                                <label style="color: black;">
                                                    Contractor Team Member Name:</label>
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:Label ID="lblContractorTeamMemberName_p" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-6" style="overflow-wrap: break-word;">
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-6 text-left">
                                                    <label style="color: black;">
                                                        Item Name/Size</label>
                                                </div>
                                                <div class="col-sm-6 text-left">
                                                    <asp:Label ID="lblItemNameSize_p" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-6 text-left">
                                                    <label style="color: black;">
                                                        Drawing Name</label>
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblDrawingName_p" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-6 text-left">
                                                    <label style="color: black;">
                                                        Part Name</label>
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblPartName_p" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-6 text-left">
                                                    <label style="color: black;">
                                                        Part Qty</label>
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblPartQty_p" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-6 text-left">
                                                    <label style="color: black;">
                                                        Process Name</label>
                                                </div>
                                                <div class="col-sm-5">
                                                    <asp:Label ID="lblProcessName_p" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-6 text-left">
                                                    <label style="color: black;">
                                                        Material Category</label>
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblMaterialCategory_p" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-6 text-left">
                                                    <label style="color: black;">
                                                        Material Grade</label>
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblMaterialGrade_p" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-6 text-left">
                                                    <label style="color: black;">
                                                        Thickness</label>
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblThickness_p" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-6 text-left">
                                                    <label style="color: black;">
                                                        MRN Number</label>
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblMrnNumber_p" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-6 text-left">
                                                    <label style="color: black;">
                                                        Job Order Remarks</label>
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblJobOrderRemarks_p" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-6 text-left">
                                                    <label style="color: black;">
                                                        Total Cost</label>
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblProcessTotalCost_p" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="col-sm-12">
                                                <label style="color: Black;">
                                                    Part Seriel Number</label>
                                            </div>
                                            <div class="col-sm-12">
                                                <asp:GridView ID="gvParSNO_p" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item SNO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblItemSNO" runat="server" Text='<%# Eval("ItemSNO")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Part S.NO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPartSNO" runat="server" Text='<%# Eval("PartSNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-12">
                                                <label style="color: Black;">
                                                    Offer QC test</label>
                                            </div>
                                            <div class="col-sm-12 text-left">
                                                <asp:Label ID="lblOfferQC_p" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-12" id="divBellowTangetCutting_p" style="display: none;" runat="server">
                                                <div class="col-sm-12">
                                                    <label style="color: black;">
                                                        Bellow Details</label>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-6 text-left">
                                                        <label style="color: black;">
                                                            ID</label>
                                                    </div>
                                                    <div class="col-sm-6 text-right">
                                                        <asp:Label ID="lblID_p" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12">
                                                    <div class="col-sm-6 text-left">
                                                        <label style="color: black;">
                                                            OD</label>
                                                    </div>
                                                    <div class="col-sm-6 text-right">
                                                        <asp:Label ID="lblOD_p" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12">
                                                    <div class="col-sm-6 text-left">
                                                        <label style="color: black;">
                                                            Pitch</label>
                                                    </div>
                                                    <div class="col-sm-6 text-right">
                                                        <asp:Label ID="lblPitch_p" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12">
                                                    <div class="col-sm-6 text-left">
                                                        <label style="color: black;">
                                                            Depth</label>
                                                    </div>
                                                    <div class="col-sm-6 text-right">
                                                        <asp:Label ID="lblDepth_p" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12">
                                                    <div class="col-sm-6 text-left">
                                                        <label style="color: black;">
                                                            Number Of Convolutions</label>
                                                    </div>
                                                    <div class="col-sm-6 text-right">
                                                        <asp:Label ID="lblNumberOfConvolutions_p" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="divBellowTangetCuttingRoll_p" runat="server" style="display: none;">
                                                <div class="col-sm-12">
                                                    <div class="col-sm-6 text-left">
                                                        <label style="color: black;">
                                                            Roll Forming Developed Pitch</label>
                                                    </div>
                                                    <div class="col-sm-6 text-right">
                                                        <asp:Label ID="lblRollFormingDevelopedPitch_p" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12">
                                                    <div class="col-sm-6 text-left">
                                                        <label style="color: black;">
                                                            Roll Forming Initial Depth</label>
                                                    </div>
                                                    <div class="col-sm-6 text-right">
                                                        <asp:Label ID="lblRollFormingInitialDepth_p" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12">
                                                    <div class="col-sm-6 text-left">
                                                        <label style="color: black;">
                                                            Number Of Stages</label>
                                                    </div>
                                                    <div class="col-sm-6 text-right">
                                                        <asp:Label ID="lblNumberOfStages_p" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <asp:GridView ID="gvStages_p" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                        EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Stages" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="12%"
                                                                ItemStyle-Width="12%" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStages" runat="server" Text='<%# Eval("Stages")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Inner" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtInner" runat="server" Text='<%# Eval("INNER")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Outer" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtOuter" runat="server" Text='<%# Eval("OUTER")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Gap" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtGap" runat="server" Text='<%# Eval("Gap")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div id="divBellowTangentCuttingExpandal_p" runat="server" style="display: none;">
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-5 text-right">
                                                        <label style="color: black;">
                                                            Expandal Developed Pitch</label>
                                                    </div>
                                                    <div class="col-sm-2 text-center">
                                                        :
                                                    </div>
                                                    <div class="col-sm-5">
                                                        <asp:Label ID="lblExpandalDevelopedPitch_p" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-5 text-right">
                                                        <label style="color: black;">
                                                            Expandal Final Roller</label>
                                                    </div>
                                                    <div class="col-sm-2 text-center">
                                                        :
                                                    </div>
                                                    <div class="col-sm-5">
                                                        <asp:Label ID="lblExpandalFinalRolller_p" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="divMRNDetails_p" style="display: none;" runat="server">
                                                <div class="col-sm-12">
                                                    <label style="color: Black;">
                                                        Issue Details</label>
                                                </div>
                                                <div class="col-sm-12 p-t-10 text-left">
                                                    <asp:GridView ID="gvMRNDetails_p" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                        EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                                HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MRN Number" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMRNNumber" runat="server" Text='<%# Eval("MRNNumber")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Issued Weight" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIssuedWeight" runat="server" Text='<%# Eval("IssuedWeight")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Length" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLength" runat="server" Text='<%# Eval("Length")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Width" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWidth" runat="server" Text='<%# Eval("Width")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Material Return" ItemStyle-HorizontalAlign="left"
                                                                HeaderStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMatRtn" runat="server" Text='<%# Eval("MaterialReturn")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div style="display: none;" id="divWPSDetails_p" runat="server">
                                                <div class="col-sm-12">
                                                    <label style="color: Black;">
                                                        WPS Details</label>
                                                </div>
                                                <div class="col-sm-12 p-t-10 text-left">
                                                    <asp:GridView ID="gvWPSDetails_p" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                        EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                                HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="WPS Number" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWPSNumber" runat="server" Text='<%# Eval("WPSNumber")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Material Grade" HeaderStyle-CssClass="text-center"
                                                                ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMaterialGrade" runat="server" Text='<%# Eval("MaterialGrade")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Thickness" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblThickness" runat="server" Text='<%# Eval("Thickness")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Process" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblProcess" runat="server" Text='<%# Eval("Process")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Filler Grade" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFillerGrade" runat="server" Text='<%# Eval("FillerGrade")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amps" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAmps" runat="server" Text='<%# Eval("Amps")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Polarity" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPolarity" runat="server" Text='<%# Eval("Polarity")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="GasLevel" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGasLevel" runat="server" Text='<%# Eval("GasLevel")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div id="divMarkingAndCutting_p" style="display: none;" runat="server">
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-5 text-right">
                                                        <label style="color: black;">
                                                            Diameter</label>
                                                    </div>
                                                    <div class="col-sm-2 text-center">
                                                        :
                                                    </div>
                                                    <div class="col-sm-5">
                                                        <asp:Label ID="lblDiameter_p" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-5 text-right">
                                                        <label style="color: black;">
                                                            Length</label>
                                                    </div>
                                                    <div class="col-sm-2 text-center">
                                                        :
                                                    </div>
                                                    <div class="col-sm-5">
                                                        <asp:Label ID="lblLength_p" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-5 text-right">
                                                        <label style="color: black;">
                                                            Width</label>
                                                    </div>
                                                    <div class="col-sm-2 text-center">
                                                        :
                                                    </div>
                                                    <div class="col-sm-5">
                                                        <asp:Label ID="lblWidth_p" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-5 text-right">
                                                        <label style="color: black;">
                                                            Weigth</label>
                                                    </div>
                                                    <div class="col-sm-2 text-center">
                                                        :
                                                    </div>
                                                    <div class="col-sm-5">
                                                        <asp:Label ID="lblWeight_p" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-5 text-right">
                                                        <label style="color: black;">
                                                            Cutting Process</label>
                                                    </div>
                                                    <div class="col-sm-2 text-center">
                                                        :
                                                    </div>
                                                    <div class="col-sm-5">
                                                        <asp:Label ID="lblCuttingProcess_p" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="divSheetMarkingAndCutting_p" runat="server" style="display: none;">
                                                <asp:GridView ID="gvPLYDetails_p" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="NOP" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNOP" runat="server" Text='<%# Eval("NOP")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Circumference" HeaderStyle-CssClass="text-center"
                                                            ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCircumference" runat="server" Text='<%# Eval("Circumferance")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Length" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLength" runat="server" Text='<%# Eval("Length")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="THK" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTHK" runat="server" Text='<%# Eval("THK")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Weight" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblWeight" runat="server" Text='<%# Eval("Weight")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-20 text-right">
                                        <div class="col-sm-4">
                                            <label style="color: black; font-weight: bolder;">
                                                Quality Incharge</label>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                        <div class="col-sm-4">
                                            <label style="color: black; font-weight: bolder;">
                                                Production Incharge</label>
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
    </div>
    <div class="modal" id="mpeQCRequest">
        <div class="modal-dialog" style="max-width: 81%; padding-left: 10%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H1">
                                <div class="text-center">
                                    <asp:Label ID="lblPartName_Production" Text="" Style="color: brown;" runat="server"></asp:Label>
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal" onclick="hidempeQCRequestPopUp();">
                                x</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="Div1" class="docdiv">
                                <div class="inner-container">
                                    <div id="Div7" runat="server">
                                        <div id="div9" class="divInput" runat="server">
                                            <div class="ip-div text-center">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;" id="divQCrequest" runat="server">
                                            <asp:GridView ID="gvQCRequest" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                DataKeyNames="PRPDID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkall" runat="server" onclick="return chekAllMRN(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkQC" runat="server" AutoPostBack="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Part SNO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartSNO" runat="server" Text='<%# Eval("PartSNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="col-sm-12" id="divQCFailureStages" runat="server">
                                            <asp:GridView ID="gvQCFailureStages" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                DataKeyNames="PRPDID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="15%" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Type Of Check" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCheck" runat="server" Text='<%# Eval("TypeOfCheck")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="QC Status" HeaderStyle-Width="20%" ItemStyle-Width="20%"
                                                        HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQCStatus" runat="server" Text='<%# Eval("QCStatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="30%" ItemStyle-Width="30%"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtRemarks" Text='<%# Eval("QCRemarks")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <%--   <div class="col-sm-12 p-t-10" style="overflow-x: scroll;" id="divQCFailureSingleStage"
                                            runat="server">
                                            <asp:GridView ID="gvQCFaliureSingleStage" runat="server" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                DataKeyNames="PRPDID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Part SNO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartSNO" runat="server" Text='<%# Eval("PartSNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="QC Status" ItemStyle-Width="20px" HeaderStyle-Width="20px"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQCStatus" runat="server" Text='<%# Eval("QCStatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="30%" ItemStyle-Width="30%"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtRemarks" Text='<%# Eval("QCRemarks")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="col-sm-12 text-center">
                                <asp:LinkButton Text="Job Completed" CssClass="btn btn-cons btn-save AlignTop" ID="btnSaveQCRequest"
                                    runat="server" OnClientClick="return ValidateQCRequest();" OnClick="btnSaveQCRequest_Click" />
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal" id="mpeSheetWelding" style="overflow-y: scroll;">
        <div class="modal-dialog" style="max-width: 81%; padding-left: 10%; padding-top: 5%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Always">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">Job Card Details</h4>
                            <asp:Label ID="lblProcessName_SW" runat="server" Style="padding-left: 19px; padding-top: 10px; color: #c12424;"></asp:Label>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true"
                                onclick="HideSheetWeldingPopUp();">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="divSheetWelding" class="docdiv">
                                <div class="inner-container">
                                    <div id="div12" runat="server">
                                        <div id="div" class="divInput" runat="server">
                                            <div class="ip-div text-center">
                                                <div class="col-sm-12">
                                                    <label style="font-size: larger; font-weight: bolder;">
                                                        Job Order ID:
                                                    </label>
                                                    <asp:Label ID="lblJobOrderID_SW" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-12">
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            RFP NO:
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblRFPNo_SW" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Date:
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                    </div>
                                                </div>
                                                <%--  <div class="col-sm-12">
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Contractor Name:
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblContractorName_SW" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Contractor Team Member Name:
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblCTMName_SW" runat="server"></asp:Label>
                                                    </div>
                                                </div>--%>
                                                <div class="col-sm-12">
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Item Name/Size
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblItemNameSize_SW" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Drawing Name
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblDrawingName_SW" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Part Name
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblPartName_SW" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Part QTy
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblPartQty_SW" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Category Name
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblCategoryName_SW" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Material Grade Name
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblGradeName_SW" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Thickness
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblThickness_SW" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Offer QC</label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblOfferQC_SW" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            MRN Number
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblMRNNumber_SW" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Cutting Process Name
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblCuttingProcess_SW" runat="server"></asp:Label>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12" id="divcontractornamedetails_SW" runat="server">
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label mandatorylbl">
                                                            Contractor Name</label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:DropDownList ID="ddlContractorName_SW" runat="server" CssClass="form-control mandatoryfield"
                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlContractorName_J_OnSelectedIndexChanged" ToolTip="Select Contractor Name">
                                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label mandatorylbl">
                                                            Contractor Team Member Name</label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:DropDownList ID="ddlContractorTeamName_SW" runat="server" CssClass="form-control mandatoryfield"
                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlContractorTeamMemberName_J_OnSelectedIndexChanged" ToolTip="Select Contractor Team Member Name">
                                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label mandatorylbl">
                                                            Issue Date</label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:TextBox ID="txtIssuedDate_SW" runat="server" placeholder="Enter Issue Date"
                                                            ToolTip="Enter Issue Date" CssClass="form-control mandatoryfield datepicker"
                                                            MaxLength="300" autocomplete="nope"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Delivery Date</label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:TextBox ID="txtDeliveryDate_SW" runat="server" placeholder="Enter Delivery Date"
                                                            ToolTip="Enter Delivery Date" CssClass="form-control mandatoryfield datepicker"
                                                            MaxLength="300" autocomplete="nope"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-20">
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Job Order Remarks</label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:TextBox ID="txtJobOrderRemarks_SW" runat="server" placeholder="Enter Job Order Remarks"
                                                            CssClass="form-control" MaxLength="300" autocomplete="nope"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <label class="form-label" style="display: contents;">
                                                            Upload</label>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:FileUpload ID="fpUpload_SW" runat="server" TabIndex="12" CssClass="form-control"></asp:FileUpload>
                                                    </div>
                                                </div>
                                                <%-- <div class="col-sm-12 p-t-10">
                                                    <label class="form-label">
                                                        Part Name Measurment Details</label>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Diameter
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblDiameter_SW" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Length</label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblLength_SW" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Width
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblWidth_SW" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Weight</label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblWeight_SW" runat="server"></asp:Label>
                                                    </div>
                                                </div>--%>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <label>
                                                Process Seriel Number Details</label>
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvPartSerielNumber_SW" runat="server" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                OnRowDataBound="gvPartSerielNumber_SW_OnRowDataBound" DataKeyNames="PRPDID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkall" runat="server" onclick="return checkAllItems(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkitems" runat="server"
                                                                AutoPostBack="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item SNO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemSNO" runat="server" Text='<%# Eval("ItemSNO")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Part S.NO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartSNO" runat="server" Text='<%# Eval("PartSNO")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="col-sm-12 p-t-10" runat="server" visible="false">
                                            <div class="col-sm-3 text-right">
                                                <label class="form-label mandatorylbl">
                                                    Select WPS Number
                                                </label>
                                            </div>
                                            <div class="col-sm-3 text-left">
                                                <asp:DropDownList ID="ddlWPSnumber_SW" runat="server" CssClass="form-control mandatoryfield"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlWPSnumber_SW_OnSelectedIndexChanged"
                                                    ToolTip="Select WPS  Number">
                                                    <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-3 text-left">
                                            </div>
                                            <div class="col-sm-3">
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <label>
                                                WPS Details</label>
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvWPSDetails_SW" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                DataKeyNames="WPSID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="WPS Number" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblWPSNumber" runat="server" Text='<%# Eval("WPSNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Material Grade" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMaterialGrade" runat="server" Text='<%# Eval("MaterialGrade")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Thickness" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblThickness" runat="server" Text='<%# Eval("Thickness")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Process" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProcess" runat="server" Text='<%# Eval("Process")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Filler Grade" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFillerGrade" runat="server" Text='<%# Eval("FillerGrade")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amps" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAmps" runat="server" Text='<%# Eval("Amps")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Polarity" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPolarity" runat="server" Text='<%# Eval("Polarity")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="GasLevel" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGasLevel" runat="server" Text='<%# Eval("GasLevel")%>'></asp:Label>
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
                            <div class="col-sm-12 text-center">
                                <asp:Button ID="btnSaveSheetWelding" runat="server" CssClass="btn btn-cons btn-success"
                                    Text="Save" OnClientClick="return ValidateSheetWelding();" OnClick="btnSaveSheetWelding_Click" />
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal" id="mpeBellowTangetCutting" style="overflow-y: scroll;">
        <div class="modal-dialog" style="max-width: 81%; padding-left: 10%; padding-top: 5%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Always">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">Job Card Details</h4>
                            <asp:Label ID="lblProcessName_BTC" runat="server" Style="padding-left: 19px; padding-top: 10px; color: #c12424;"></asp:Label>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" onclick="hideBellowsTangetCutPopUP();"
                                aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="div11" class="docdiv">
                                <div class="inner-container">
                                    <div id="divBellowTangetCutting_BTC" runat="server">
                                        <div id="div14" class="divInput" runat="server">
                                            <div class="ip-div text-center">
                                                <div class="col-sm-12">
                                                    <label style="font-size: larger; font-weight: bolder;">
                                                        Job Order ID:
                                                    </label>
                                                    <asp:Label ID="lblJobOrderID_BTC" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-12">
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            RFP NO:
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblRFPNo_BTC" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Date:
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                    </div>
                                                </div>
                                                <%--  <div class="col-sm-12">
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Contractor Name:
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblContractorName_BTC" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Contractor Team Member Name:
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblContractorTeamMemberName_BTC" runat="server"></asp:Label>
                                                    </div>
                                                </div>--%>
                                                <div class="col-sm-12">
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Item Name/Size
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblItemName_BTC" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Drawing Name
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblDrawingName_BTC" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Part Name
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblPartName_BTC" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Part QTy
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblPartQty_BTC" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Category Name
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblCategoryName_BTC" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Material Grade Name
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblGradeName_BTC" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Thickness
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblThickness_BTC" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            Offer QC</label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblOfferQC_BTC" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label">
                                                            MRN Number
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:Label ID="lblMRNNumber_BTC" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-3 text-right">
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                    </div>
                                                </div>
                                                <div runat="server" id="divBellowDetails">
                                                    <div class="col-sm-12 p-t-10">
                                                        <label class="form-label">
                                                            Bellow Details</label>
                                                    </div>
                                                    <div class="col-sm-12 p-t-10">
                                                        <div class="col-sm-3 text-right">
                                                            <label class="form-label">
                                                                ID
                                                            </label>
                                                        </div>
                                                        <div class="col-sm-3 text-left">
                                                            <asp:Label ID="lblID_BTC" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-3 text-right">
                                                            <label class="form-label">
                                                                OD</label>
                                                        </div>
                                                        <div class="col-sm-3 text-left">
                                                            <asp:Label ID="lblOD_BTC" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-12 p-t-10">
                                                        <div class="col-sm-3 text-right">
                                                            <label class="form-label">
                                                                Depth
                                                            </label>
                                                        </div>
                                                        <div class="col-sm-3 text-left">
                                                            <asp:Label ID="lblDepth_BTC" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-3 text-right">
                                                            <label class="form-label">
                                                                Pitch</label>
                                                        </div>
                                                        <div class="col-sm-3 text-left">
                                                            <asp:Label ID="lblPitch_BTC" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-12 p-t-10">
                                                        <div class="col-sm-3 text-right">
                                                            <label class="form-label">
                                                                No Of Convolutions
                                                            </label>
                                                        </div>
                                                        <div class="col-sm-3 text-left">
                                                            <asp:Label ID="lblNoOfConvolutions_BTC" runat="server"></asp:Label>
                                                        </div>
                                                        <div class="col-sm-3 text-right">
                                                        </div>
                                                        <div class="col-sm-3 text-left">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label mandatorylbl">
                                                            Contractor Name</label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:DropDownList ID="ddlContractorName_BTC" runat="server" CssClass="form-control mandatoryfield"
                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlContractorName_J_OnSelectedIndexChanged" ToolTip="Select Contractor Name">
                                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label mandatorylbl">
                                                            Contractor Team Member Name</label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:DropDownList ID="ddlContractorTeamName_BTC" runat="server" CssClass="form-control mandatoryfield"
                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlContractorTeamMemberName_J_OnSelectedIndexChanged" ToolTip="Select Contractor Team Member Name">
                                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label mandatorylbl">
                                                            Issue Date</label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:TextBox ID="txtIssueDate_BTC" runat="server" placeholder="Enter Issue Date"
                                                            ToolTip="Enter Issue Date" CssClass="form-control mandatoryfield datepicker"
                                                            MaxLength="300" autocomplete="nope"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label mandatorylbl">
                                                            Delivery Date</label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:TextBox ID="txtDeliveryDate_BTC" runat="server" placeholder="Enter Delivery Date"
                                                            ToolTip="Enter Delivery Date" CssClass="form-control mandatoryfield datepicker"
                                                            MaxLength="300" autocomplete="nope"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-10" id="divTangetCuttingExpandal_BTC" runat="server" visible="false">
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label mandatorylbl">
                                                            Expandal Developed Pitch</label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:TextBox ID="txtExpandalDevelopedPitch_BTC" runat="server" placeholder="Enter Expandal Developed Pitch"
                                                            onkeypress="return fnAllowNumeric();" ToolTip="Enter Issue Date" CssClass="form-control mandatoryfield"
                                                            MaxLength="300" autocomplete="nope"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label mandatorylbl">
                                                            Expandal Final Roller</label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:TextBox ID="txtExpandalFinalOver_BTC" runat="server" placeholder="Enter Expandal Roll Over"
                                                            onkeypress="return fnAllowNumeric();" ToolTip="Expan" CssClass="form-control mandatoryfield"
                                                            MaxLength="300" autocomplete="nope"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div id="divTangetCuttRoll_BTC" runat="server" visible="false">
                                                    <div class="col-sm-12 p-t-10">
                                                        <div class="col-sm-3 text-right">
                                                            <label class="form-label mandatorylbl">
                                                                Roll Forming Development Pitch</label>
                                                        </div>
                                                        <div class="col-sm-3 text-left">
                                                            <asp:TextBox ID="txtRollFormingDevelopementPitch_BTC" runat="server" placeholder="Enter Roll Forming Developed Pitch"
                                                                onkeypress="return fnAllowNumeric();" ToolTip="Enter Issue Date" CssClass="form-control mandatoryfield"
                                                                MaxLength="300" autocomplete="nope"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-3 text-right">
                                                            <label class="form-label mandatorylbl">
                                                                Roll Forming Initial Depth</label>
                                                        </div>
                                                        <div class="col-sm-3 text-left">
                                                            <asp:TextBox ID="txtRollFormingInitialDepth_BTC" runat="server" placeholder="Enter Roll Forming Initial Depth"
                                                                onkeypress="return fnAllowNumeric();" CssClass="form-control mandatoryfield"
                                                                MaxLength="300" autocomplete="nope"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-12 p-t-10">
                                                        <div class="col-sm-3 text-right">
                                                            <label class="form-label mandatorylbl">
                                                                Number Of Stages
                                                            </label>
                                                        </div>
                                                        <div class="col-sm-3 text-left">
                                                            <asp:TextBox ID="txtNumberOfStages_BTC" runat="server" placeholder="Enter Stages"
                                                                onkeypress="return fnAllowNumeric();" onblur="BindStages(this);" CssClass="form-control mandatoryfield"
                                                                MaxLength="300" autocomplete="nope"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-3 text-right">
                                                        </div>
                                                        <div class="col-sm-3 text-left">
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-12 p-t-10">
                                                        <asp:GridView ID="gvStages_BTC" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Stages" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="12%"
                                                                    ItemStyle-Width="12%" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStages" runat="server" Text=' <%# Eval("Stages")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Inner" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtInner" runat="server" onkeypress="return fnAllowNumeric();" CssClass="form-control mandatoryfield"
                                                                            Text='<%# Eval("INNER")%>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Outer" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtOuter" runat="server" CssClass="form-control mandatoryfield"
                                                                            onkeypress="return fnAllowNumeric();" Text='<%# Eval("OUTER")%>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Gap" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtGap" runat="server" CssClass="form-control mandatoryfield" onkeypress="return fnAllowNumeric();"
                                                                            Text='<%# Eval("Gap")%>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                <div class="col-sm-12 p-t-20">
                                                    <div class="col-sm-3 text-right">
                                                        <label class="form-label mandatorylbl">
                                                            Job Order Remarks</label>
                                                    </div>
                                                    <div class="col-sm-3 text-left">
                                                        <asp:TextBox ID="txtJobOrderRemarks_BTC" runat="server" placeholder="Enter Job Order Remarks"
                                                            CssClass="form-control mandatoryfield" MaxLength="300" autocomplete="nope"></asp:TextBox>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <label class="form-label" style="display: contents;">
                                                            Upload</label>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:FileUpload ID="fbBellowTC_BTC" runat="server" TabIndex="12" CssClass="form-control"></asp:FileUpload>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <label>
                                                Process Seriel Number Details</label>
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvPartSNO_BTC" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                OnRowDataBound="gvPartSNO_BTC_OnRowDataBound" DataKeyNames="PRPDID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkall" runat="server" onclick="return checkAllItems(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkitems" runat="server"
                                                                AutoPostBack="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item SNO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemSNO" runat="server" Text='<%# Eval("ItemSNO")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Part S.NO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartSNO" runat="server" Text='<%# Eval("PartSNO")%>'></asp:Label>
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
                            <div class="col-sm-12 text-center">
                                <asp:Button ID="btnBellowTangetCutting" runat="server" CssClass="btn btn-cons btn-success"
                                    Text="Save" OnClientClick="return ValidateBelloFormingAndTangentCutting();"
                                    OnClick="btnBellowTangetCutting_Click" />
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div class="modal" id="mpeAssemplyPlanning" style="overflow-y: scroll;">
        <div style="max-width: 81%; padding-left: 10%; padding-top: 5%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <Triggers>
                        <%--  <asp:PostBackTrigger ControlID="gvAssemplyPlanningDetails_AP" />--%>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD"></h4>
                            <asp:Label ID="lblItemName_AP" runat="server" Style="font-weight: bold; padding-left: 30px; color: brown;"></asp:Label>
                            <button type="button" class="close btn-primary-purple" onclick="hideAssemplyJobCardPopup();"
                                data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="divInput_AP" class="docdiv">
                                <div class="inner-container">
                                    <div class="col-sm-12 text-center">
                                        <label style="font-size: larger; font-weight: bolder;">
                                            Job Order ID:
                                        </label>
                                        <asp:Label ID="lblJobOrderId_AP" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-3 text-right">
                                            <label class="form-label">
                                                RFP NO:
                                            </label>
                                        </div>
                                        <div class="col-sm-3 text-left">
                                            <asp:Label ID="lblRFPNo_AP" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-3 text-right">
                                            <label class="form-label">
                                                Item Name/Size
                                            </label>
                                        </div>
                                        <div class="col-sm-3 text-left">
                                            <asp:Label ID="lblItemNameSize_AP" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <%--<div class="col-sm-12 p-t-10">
                                        <div class="col-sm-3 text-right">
                                            <label class="form-label">
                                                Drawing Name
                                            </label>
                                        </div>
                                        <div class="col-sm-3 text-left">
                                            <asp:Label ID="lblDrawingName_AP" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-3 text-right">
                                            <label class="form-label">
                                                Part Name
                                            </label>
                                        </div>
                                        <div class="col-sm-3 text-left">
                                            <asp:Label ID="lblPartName_AP" runat="server"></asp:Label>
                                        </div>
                                    </div>--%>
                                    <div class="col-sm-12 p-t-10" style="display: none;">
                                        <div class="col-sm-3 text-right">
                                            <label class="form-label">
                                                Contractor Name</label>
                                        </div>
                                        <div class="col-sm-3 text-left">
                                            <asp:DropDownList ID="ddlContractorName_AP" runat="server" CssClass="form-control" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlContractorName_J_OnSelectedIndexChanged" ToolTip="Select Contractor Name">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3 text-right">
                                            <label class="form-label">
                                                Contractor Team Member Name</label>
                                        </div>
                                        <div class="col-sm-3 text-left">
                                            <asp:DropDownList ID="ddlContractorTeamMemberName_AP" runat="server" CssClass="form-control" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlContractorTeamMemberName_J_OnSelectedIndexChanged" ToolTip="Select Contractor Team Member Name">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-3 text-right">
                                            <label class="form-label mandatorylbl">
                                                Issue Date</label>
                                        </div>
                                        <div class="col-sm-3 text-left">
                                            <asp:TextBox ID="txtIssueDate_AP" runat="server" placeholder="Enter Issue Date" ToolTip="Enter Issue Date"
                                                CssClass="form-control mandatoryfield datepicker" MaxLength="300" autocomplete="nope"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-3 text-right">
                                            <label class="form-label">
                                                Delivery Date</label>
                                        </div>
                                        <div class="col-sm-3 text-left">
                                            <asp:TextBox ID="txtDeliveryDate_AP" runat="server" placeholder="Enter Delivery Date"
                                                ToolTip="Enter Delivery Date" CssClass="form-control mandatoryfield datepicker"
                                                MaxLength="300" autocomplete="nope"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-3 text-right">
                                            <label class="form-label">
                                                Job Order Remarks</label>
                                        </div>
                                        <div class="col-sm-3 text-left">
                                            <asp:TextBox ID="txtJobOrderRemarks_AP" runat="server" placeholder="Enter Job Order Remarks"
                                                CssClass="form-control" MaxLength="300" autocomplete="nope"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-3 text-right">
                                        </div>
                                        <div class="col-sm-3 text-left">
                                        </div>
                                    </div>
                                    <%--OnRowCommand="gvAssemplyPlanningDetails_AP_OnRowCommand" OnRowDataBound="gvAssemplyPlanningDetails_AP_OnRowDataBound"--%>
                                    <div class="col-sm-12 p-t-10" id="div10" runat="server" style="overflow-x: scroll;">
                                        <asp:GridView ID="gvPartAssemplyPlanningDetails_AP" runat="server" AutoGenerateColumns="False"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                            OnRowDataBound="gvPartAssemplyPlanningDetails_AP_OnRowDataBound" DataKeyNames="PAPDID">
                                            <Columns>
                                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkall" runat="server" onclick="return checkAllItems(this);" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%--   <asp:CheckBox ID="chkitems" runat="server" onclick="return MandatoryField(this);"
                                                            AutoPostBack="false" />--%>
                                                        <asp:CheckBox ID="chkitems" runat="server" AutoPostBack="true"
                                                            OnCheckedChanged="chkitems_OnCheckedChanged" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="S.No" HeaderStyle-CssClass="text-center" ItemStyle-Width="2%"
                                                    ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Part1 Completed Qty" HeaderStyle-CssClass="text-center"
                                                    ItemStyle-Width="2%" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPart1CompletedQty" CssClass="PartCompletedQuantity" Text='<%# Eval("Part1CompletedQty")%>'
                                                            runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Part2 Completed Qty" HeaderStyle-CssClass="text-center"
                                                    ItemStyle-Width="2%" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPart2CompletedQty" CssClass="PartCompletedQuantity" Text='<%# Eval("Part2CompletedQty")%>'
                                                            runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Part1 Qty" HeaderStyle-CssClass="text-center"
                                                    ItemStyle-Width="2%" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPart1Qty" Text='<%# Eval("Part1Qty")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Part2 Qty" HeaderStyle-CssClass="text-center"
                                                    ItemStyle-Width="2%" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPart2Qty" Text='<%# Eval("Part2Qty")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Part Name 1" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPart1" runat="server" Text='<%# Eval("PartName1")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Part Name 2" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPart2" runat="server" Text='<%# Eval("PartName2")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Qty" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQty" runat="server" Text='<%# Eval("ItemQty")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Done" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemCompleted" runat="server" Text='<%# Eval("ItemCompleted")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                    <div class="col-sm-12 p-t-10">
                                        <asp:GridView ID="gvBellowSno_AP" runat="server" AutoGenerateColumns="False"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found"
                                            CssClass="table table-hover table-bordered medium" DataKeyNames="PRIDID">
                                            <Columns>
                                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkall" runat="server" onclick="return CheckAllBellowSno(this);" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkitems" runat="server"
                                                            AutoPostBack="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="S.No" HeaderStyle-CssClass="text-center"
                                                    ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Sno" HeaderStyle-CssClass="text-center"
                                                    ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemSno" Text='<%# Eval("ItemSno")%>'
                                                            runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                    <div class="col-sm-12 text-center p-t-10">
                                        <asp:Button ID="btnSaveAssemplyjobCard" runat="server" CssClass="btn btn-cons btn-success"
                                            Text="Save" OnClientClick="return ValidateAssemplyJobCard();" OnClick="btnSaveAssemplyjobCard_Click" />
                                        <asp:Button ID="btnLoadSno" runat="server" CssClass="btn btn-cons btn-success" Style="display: none;"
                                            Text="Save" OnClick="btnLoadSno_Click" />
                                    </div>

                                    <div class="col-sm-12 p-t-10">
                                        <label style="color: #000; font-weight: bold; font-size: 24px;">Please Click On The View Assemply To Share QC else Your Job Card Is Incomplete </label>
                                    </div>

                                    <div class="col-sm-12 p-t-10" id="div15" runat="server" style="overflow-x: scroll;">
                                        <asp:GridView ID="gvAssemplyJobCardHeader" runat="server" AutoGenerateColumns="False"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" OnRowDataBound="gvAssemplyJobCardHeader_OnRowDataBound"
                                            CssClass="table table-hover table-bordered medium" OnRowCommand="gvAssemplyJobCardHeader_OnRowCommand" DataKeyNames="JCHID,Status">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No" HeaderStyle-CssClass="text-center" ItemStyle-Width="2%"
                                                    ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JO No" HeaderStyle-CssClass="text-center"
                                                    ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJoNo" Text='<%# Eval("JoNo")%>'
                                                            runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Issue Date" HeaderStyle-CssClass="text-center"
                                                    ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIssueDate" Text='<%# Eval("IssueDate")%>'
                                                            runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delivery Date" HeaderStyle-CssClass="text-center"
                                                    ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeliveryDate" Text='<%# Eval("DeliveryDate")%>'
                                                            runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View Assemply">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnviewAssemplyWeldingDetails" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            CommandName="viewAssemplyWeldingDetails"><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="QC Status" HeaderStyle-CssClass="text-center"
                                                    ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQCStatus" Text='<%# Eval("QCStatus")%>'
                                                            runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="PDF">
                                                   <%--    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnPDF" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            OnClientClick='<%# string.Format("return PrintAssemplyJobCard({0});",Eval("JCHID")) %>'>  <img src="../Assets/images/pdf.png" /></asp:LinkButton>
                                                    </ItemTemplate>--%>
                                                </asp:TemplateField>
                                                <%--  <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="text-center"
                                                    ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            CommandName="EditAJD"><img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="text-center"
                                                    ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete" runat="server"
                                                            CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            OnClientClick='<%# string.Format("return DeleteAJD({0});",Eval("JCHID")) %>'>
                                                            <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Issue MRN" HeaderStyle-CssClass="text-center"
                                                    ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnIssueMRN" runat="server"
                                                            CommandName="IssueMRN" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>                                                            
                                                            <img src="../Assets/images/add1.png"/></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <%-- <div class="col-sm-12 text-center p-t-10">
                                        <asp:Button ID="btnItemCompleted" runat="server" CssClass="btn btn-cons btn-success"
                                            Text="Share Item" OnClick="btnItemCompleted_Click" />
                                    </div>--%>
									 <div class="col-sm-12 p-t-10">
                                        <label style="color: #000; font-weight: bold; font-size: 24px;">After QC Clearance Job Card Print</label>
                                    </div>
									<div class="col-sm-12 p-t-10" id="div18" runat="server" style="overflow-x: scroll;">
                                        <asp:GridView ID="gvAssemplyJobCardHeaderPartToPartPrint" runat="server" AutoGenerateColumns="False"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" 
                                            CssClass="table table-hover table-bordered medium" DataKeyNames="JCHID,PAPDID">
                                            <Columns>
                                                 <asp:TemplateField HeaderText="S.No" HeaderStyle-CssClass="text-center" ItemStyle-Width="2%"
                                                    ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JO No" HeaderStyle-CssClass="text-center"
                                                    ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJoNo" Text='<%# Eval("JoNo")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Part To Part" HeaderStyle-CssClass="text-center"
                                                    ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPartToPart" Text='<%# Eval("PartToPart")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Job Card Print" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnPDF" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            OnClientClick='<%# string.Format("return printPartToPartjobcard({0});",Eval("PAPDID")) %>'>  <img src="../Assets/images/pdf.png" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                            <asp:HiddenField ID="hdnJCHIDD" runat="server" Value="0" />
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
    <div class="modal" id="mpeBOIMRNIssue" style="overflow-y: scroll;">
        <div style="max-width: 81%; padding-left: 10%; padding-top: 5%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD"></h4>
                            <asp:Label ID="lblJobNo_BOI" runat="server" Style="font-weight: bold; padding-left: 30px; color: brown;"></asp:Label>
                            <button type="button" class="close btn-primary-purple" onclick="HideMRNBOIPopup();"
                                data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="divBOIMRN" class="docdiv">
                                <div class="inner-container">
                                    <div class="col-sm-12 p-t-10 text-left" id="divAJMRN" runat="server">
                                        <asp:GridView ID="gvMRNIssueBOI_AP" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                            OnRowDataBound="gvMRNIssueBOI_AP_OnRowDataBound" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium" DataKeyNames="MRNID,MRN_LocationID,MPID">
                                            <Columns>
                                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkall" runat="server" onclick="return chekAllMRN(this);" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkitems" runat="server" onclick="return MRNMandatoryField(this);"
                                                            AutoPostBack="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Part Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPartName" runat="server" Text='<%# Eval("PartName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MRN Number" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMRNNumber" runat="server" Text='<%# Eval("MRNNumber")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Blocked Qty" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBlockedWeight" runat="server" Text='<%# Eval("BlockedWeight")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Issue Qty" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdnAvailWeight" runat="server" Value='<%# Eval("AvailWeight")%>' />
                                                        <asp:TextBox ID="txtAvailWeight" CssClass="form-control" onkeyup="ValidateAssemplyMRNWeightIssue(this);"
                                                            onkeypress="return validationDecimal(this);" Text='<%# Eval("AvailWeight")%>' runat="server"></asp:TextBox>
                                                        <%--   <asp:Label ID="lblTakenWeight" runat="server" Text='<%# Eval("AvailWeight")%>'></asp:Label>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Remaining Qty" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRemainQty" runat="server" Text='<%# Eval("AvailWeight")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%-- <asp:TemplateField HeaderText="Issued Weight" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtIssuedWeight" Text='<%# Eval("IssuedWeight")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                    <div class="col-sm-12 text-center">
                                        <asp:LinkButton ID="btnIssueMRN" runat="server" CssClass="btn btn-cons btn-success"
                                            Text="Issue MRN" OnClientClick="return ValidateAssemplyMRNIssue();" OnClick="btnIssueMRN_Click"></asp:LinkButton>
                                    </div>

                                    <div class="col-sm-12 p-t-10 text-left">
                                        <asp:GridView ID="gvAssemplyMRNIssuedDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No Records Found" OnRowDataBound="gvAssemplyMRNIssuedDetails_OnRowDataBound"
                                            CssClass="table table-hover table-bordered medium">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Part Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPartName" runat="server" Text='<%# Eval("PartName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MRN Number" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMRNNumber" runat="server" Text='<%# Eval("MRNNumber")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Blocked Weight" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBlockedWeight" runat="server" Text='<%# Eval("BlockedWeight")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Issued Weight" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtIssuedWeight" Text='<%# Eval("ISSUEDWEIGHT")%>' runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="text-center"
                                                    ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete" runat="server"
                                                            CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            OnClientClick='<%# string.Format("return DeleteAssemplyMRNIssue({0});",Eval("JCMRNID")) %>'>
                                                            <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
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


    <div class="modal" id="mpeAssemplyJobCardDetails" style="overflow-y: scroll;">
        <div style="max-width: 81%; padding-left: 10%; padding-top: 5%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD"></h4>
                            <asp:Label ID="lblJobNo_AJD" runat="server" Style="font-weight: bold; padding-left: 30px; color: brown;"></asp:Label>
                            <button type="button" class="close btn-primary-purple" onclick="hideAssemplyJobCardDetailsPopUp();"
                                data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="divAJD" class="docdiv">
                                <div class="inner-container">
                                    <div class="col-sm-12 p-t-10 text-left" id="div17" runat="server">
                                        <asp:GridView ID="gvAJDetails_AJD" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                            OnRowDataBound="gvAJDetails_AJD_OnRowDataBound" DataKeyNames="PRIDID,PAPDID">
                                            <Columns>
                                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkall" runat="server" onclick="return chekAllMRN(this);" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkitems" runat="server"
                                                            AutoPostBack="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Part To Part" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPartToPart" runat="server" Text='<%# Eval("PartToPart")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bellow Sno" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBellowSno" runat="server" Text='<%# Eval("ItemSNo")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="QC Status" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQCStatus" runat="server" Text='<%# Eval("Status")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                    <div class="col-sm-12 p-t-10 text-center">
                                        <asp:Button ID="btnAJCompleted" runat="server" CssClass="btn btn-cons btn-success"
                                            Text="Share QC" OnClientClick="return ValidateAJDetails();" OnClick="btnAJCompleted_Click" />
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
</asp:Content>
