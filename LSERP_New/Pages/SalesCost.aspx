<%@ Page Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="SalesCost.aspx.cs" Inherits="Pages_SalesCost" ClientIDMode="Predictable" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="../Assets/css/datatables2.css" />
    <script type="text/javascript" src="../Assets/scripts/datatables.js"></script>
    <script type="text/javascript" src="../Assets/scripts/datatables2.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <script language="JavaScript" type="text/javascript">
        $(document).ready(function () {
            debugger;
        });
        var $finalcost;
        function existcostclick(totcost) {
            try {
                InfoMessage('Process', 'Functionality under development. Will be available on next phase.');
            } catch (er) {

            }
        }
        //        function radiobtnchange(bomcost, ddid, itemid, ProductionOverhead, ConsumableCost, PackagingCost, MakingOverhead, MiscExpense, Recommendedcost) {
        //            debugger;
        //            if ($('.lbl_offermade').text() != '' || $('.lbl_waitforaproaval').text()) {
        //                return false;
        //            }
        //            $finalcost = $(event.target);
        //            $('input[id$=hdn_updatetotalcost]').val($finalcost.attr('id'));
        //            $('#modelAddModule').modal({
        //                backdrop: 'static'
        //            });

        //            $('.txtbomcost').text(bomcost);
        //            $('.txt_ProductionOverhead').val(ProductionOverhead);
        //            $('.txt_ConsumableCost').val(ConsumableCost);
        //            $('.txt_PackagingCost').val(PackagingCost);
        //            $('.txt_MakingOverhead').val(MakingOverhead);
        //            $('.txt_MiscExpense').val(MiscExpense);
        //            $('.txt_reccost').val(Recommendedcost);
        //            $('input[id$=hdn_ddid]').val(ddid);
        //            $('input[id$=hdn_itemid]').val(itemid);
        //            totalcost();
        //        }
        function updatetotalcost() {
            try {
                debugger;
                $('#' + $('input[id$=hdn_updatetotalcost]').val()).prop("checked", true);
            }
            catch (er) { }
        }
        function saveConfirm() {
            debugger;
            try {
                if ($('#<%=txt_reccost.ClientID %>')[0].value == "") {
                    $('#<%=txt_reccost.ClientID %>').notify('Field Required.', { arrowShow: true, position: 't,r', autoHide: true });
                    return false;
                }
                if ($('#<%=txt_reccost.ClientID %>')[0].value != "") {
                    var rcost = parseInt($('#<%=txt_reccost.ClientID %>')[0].value);
                    var tcost = parseInt($('.hdn_TotalCost').text());
                    if (rcost < tcost) {
                        $('#<%=txt_reccost.ClientID %>').notify('Recommended Cost should greater than total cost.', { arrowShow: true, position: 't,r', autoHide: true });
                        event.stopPropagation()
                        return false;
                    }
                }
                $('input[id$=hdn_TotalCost]').val($('.hdn_TotalCost').text());
                $('input[id$=hdn_RecCost]').val($('.hdn_reccost').val());
                $('#modelAddModule').modal('toggle');
                //__doPostBack('savecost', enqid);
                //swal({
                //    title: "Add Item in Exist list?",
                //    text: "If Yes, the same value will get on existing radiobutton click",
                //    type: "info",
                //    showCancelButton: true,
                //    confirmButtonColor: "#DD6B55",
                //    confirmButtonText: "Yes, Add it!",
                //    closeOnConfirm: false
                //}, function (isConfirm) {
                //        if (isConfirm) {                   
                //        __doPostBack('savecost', enqid);

                //        } else {                      
                //        __doPostBack('savecost', enqid);
                //    }
                //});
                //return false;
            } catch (er) { }
        }
        function totalcost() {
            try {
                var tc = 0;
                $('.costinclude').each(function () {
                    if ($(this).val() != '') tc = tc + parseInt($(this).val());
                    else if ($(this).text() != '') tc = tc + parseInt($(this).text());
                });
                $('.hdn_TotalCost').text(tc);
                $('input[id$=hdn_TotalCost]').val($('.hdn_TotalCost').text());
                $('input[id$=hdn_RecCost]').val($('.hdn_reccost').val());
            }
            catch (er) { }

        }
        function Hidepopup() {
            try {
                $('#modelAddModule').modal('toggle');
                $('.modal-backdrop').hide();
                $('#ContentPlaceHolder1_bomdiv').css("display", "none");
            } catch (er) { }
        }
        //        function ShowViewPopUp() {
        //            $('#modelAddModule').modal({
        //                backdrop: 'static'

        //            });
        //            return false;
        //        }

        var ForignCurrencyValue;

        function BindMarketingOverHeadCost(ele) {
            var empty = false;
            if ($('#' + ele.id).val() == "") {
                $('#' + ele.id).val(0);
                empty = true;
            }

            ForignCurrencyValue = $('#ContentPlaceHolder1_ddlCurrency').val().split('/')[1];

            var Per = $('#' + ele.id).val();

            var PerValue = parseFloat((100 - Per) / 100).toFixed(2);

            var BomCost = $('#' + ele.id).closest('tr').find('.BomCost').text();

            var MarketingOverHeadCost = parseFloat(parseInt(BomCost) / PerValue).toFixed(2);

            $('#' + ele.id).closest('td').find('span').text(MarketingOverHeadCost);
            // var PackagingCost = parseInt(('#' + ele.id).closest('tr').find('input[type="text"]')[1].value);
            var PackagingCost = 0;
            if ($('#' + ele.id).closest('tr').find('.PackagingCostValue').text() != '')
                PackagingCost = parseInt($('#' + ele.id).closest('tr').find('.PackagingCostValue').text());

            var AddPartBomCost = $('#' + ele.id).closest('tr').find('.AddPartBomCost').text();
            var IssuePartBomCost = $('#' + ele.id).closest('tr').find('.IssuePartBomCost').text();

            var UnitCostINR = parseFloat(parseFloat(MarketingOverHeadCost) + parseFloat(PackagingCost) + parseFloat(AddPartBomCost) - (parseFloat(IssuePartBomCost)).toFixed(2)).toFixed(2);
            $('#' + ele.id).closest('tr').find('.UnitCost').text(UnitCostINR);

            var UnitCostForignCurrency = parseFloat(UnitCostINR) / parseFloat(ForignCurrencyValue);
            $('#' + ele.id).closest('tr').find('.UnitCostForignCurrency').val(parseFloat(UnitCostForignCurrency).toFixed(2));

            var UnitCost = $('#' + ele.id).closest('tr').find('.UnitCost').text();
            var Quantity = $('#' + ele.id).closest('tr').find('.ItemQty').val();

            $('#' + ele.id).closest('tr').find('.totalpriceINR').text(parseFloat(UnitCost * Quantity).toFixed(2));

            $('#' + ele.id).closest('tr').find('.gvItemcost').text(parseFloat(parseFloat(UnitCost) * parseFloat(Quantity)).toFixed(2));

            var TotalCostForignCurrency = parseFloat(UnitCost) * parseFloat(Quantity) / parseFloat(ForignCurrencyValue);

            $('#' + ele.id).closest('tr').find('.TotalCostForignCurrency').text(parseFloat(TotalCostForignCurrency).toFixed(2));

            $('#' + ele.id).closest('tr').find('.RecommendedCost').val(parseFloat(TotalCostForignCurrency).toFixed(2));

            if (empty == true) {
                $('#' + ele.id).val('');
                $('#' + ele.id).closest('td').find('span').text(parseFloat(0.00).toFixed(2));
            }
        }

        function BindPackagingCost(ele) {
            var empty = false;
            if ($('#' + ele.id).val() == "") {
                $('#' + ele.id).val(0);
                empty = true;
            }
            var Per = $('#' + ele.id).val();

            ForignCurrencyValue = $('#ContentPlaceHolder1_ddlCurrency').val().split('/')[1];

            var MarketingCost = $('#' + ele.id).closest('tr').find('.MarketingOverheadValueCost').text();
            var PackagingCost = parseFloat(parseInt(MarketingCost) * (parseInt(Per) / 100)).toFixed(2);
            $('#' + ele.id).closest('td').find('span').text(PackagingCost);

            var MarketingCost = 0;
            if ($('#' + ele.id).closest('tr').find('.MarketingOverheadValueCost').text() != '')
                MarketingCost = parseInt($('#' + ele.id).closest('tr').find('.MarketingOverheadValueCost').text());

            var AddPartBomCost = $('#' + ele.id).closest('tr').find('.AddPartBomCost').text();
            var IssuePartBomCost = $('#' + ele.id).closest('tr').find('.IssuePartBomCost').text();
            var UnitCostINR = parseFloat(parseFloat(PackagingCost) + parseFloat(MarketingCost) + parseFloat(AddPartBomCost) - parseFloat(IssuePartBomCost)).toFixed(2);

            $('#' + ele.id).closest('tr').find('.UnitCost').text(parseFloat(UnitCostINR).toFixed(2));

            var UnitCostForignCurrency = parseFloat(UnitCostINR) / parseFloat(ForignCurrencyValue);

            $('#' + ele.id).closest('tr').find('.UnitCostForignCurrency').val(parseFloat(UnitCostForignCurrency).toFixed(2));

            var UnitCost = $('#' + ele.id).closest('tr').find('.UnitCost').text();
            var Quantity = $('#' + ele.id).closest('tr').find('.ItemQty').val();

            $('#' + ele.id).closest('tr').find('.totalpriceINR').text(parseFloat(UnitCost * Quantity).toFixed(2));

            var TotalCostForignCurrency = parseFloat(UnitCost) * parseFloat(Quantity) / parseFloat(ForignCurrencyValue);

            $('#' + ele.id).closest('tr').find('.TotalCostForignCurrency').text(parseFloat(TotalCostForignCurrency).toFixed(2));

            $('#' + ele.id).closest('tr').find('.gvItemcost').text(parseFloat(parseFloat(UnitCost) * parseFloat(Quantity)).toFixed(2));
            $('#' + ele.id).closest('tr').find('.RecommendedCost').val(parseFloat(TotalCostForignCurrency).toFixed(2));

            if (empty == true) {
                $('#' + ele.id).val('');
                $('#' + ele.id).closest('td').find('span').text(parseFloat(0.00).toFixed(2));
            }
        }

        function BindMarketingOverHead(ele) {
            if ($('#' + ele.id).val() != "") {
                var len = $('#ContentPlaceHolder1_gvsalescost').find('tr').length - 1;
                ForignCurrencyValue = $('#ContentPlaceHolder1_ddlCurrency').val().split('/')[1];

                for (i = 0; i < len; i++) {

                    var Per = $('#ContentPlaceHolder1_gvsalescost_txtMarketingOverHead_' + i + '').val(ele.value);
                    var PerValue = parseFloat((100 - ele.value) / 100).toFixed(2);

                    var MarketingOverHeadCost = parseFloat(parseInt($('#ContentPlaceHolder1_gvsalescost_lblBOMCost_' + i + '').text()) / PerValue).toFixed(2);

                    $('#ContentPlaceHolder1_gvsalescost_lblmarketingOverHeadCost_' + i + '').text(MarketingOverHeadCost);
                    var AddtionalPartCost = $('#ContentPlaceHolder1_gvsalescost_lblAddtionalPartCost_' + i + '').text();
                    var IssuePartCost = $('#ContentPlaceHolder1_gvsalescost_lblIssuePartCost_' + i + '').text();
                    var PackingCost = 0;
                    if ($('#ContentPlaceHolder1_gvsalescost_lblPackagingCosthead_' + i + '').text() != '') PackingCost = $('#ContentPlaceHolder1_gvsalescost_lblPackagingCosthead_' + i + '').text();

                    var UnitCostINR = parseFloat(parseFloat(MarketingOverHeadCost) + parseFloat(PackingCost) + parseFloat(AddtionalPartCost) - parseFloat(IssuePartCost)).toFixed(2);

                    $('#ContentPlaceHolder1_gvsalescost_lblUnitCost_' + i + '').text(parseFloat(UnitCostINR).toFixed(2));

                    var UnitCostForignCurrency = parseFloat(UnitCostINR) / parseFloat(ForignCurrencyValue);
                    $('#ContentPlaceHolder1_gvsalescost_txtUnitCostForignCurrency_' + i + '').val(parseFloat(UnitCostForignCurrency).toFixed(2));

                    var UnitCost = $('#ContentPlaceHolder1_gvsalescost_lblUnitCost_' + i + '').text();
                    var Quantity = $('#ContentPlaceHolder1_gvsalescost_lblTACHeader_' + i + '').val();

                    var TotalCostForignCurrency = parseFloat(UnitCost) * parseFloat(Quantity) / parseFloat(ForignCurrencyValue);
                    $('#ContentPlaceHolder1_gvsalescost_lblTotalForignCurrency_' + i + '').text(parseFloat(TotalCostForignCurrency).toFixed(2));

                    $('#ContentPlaceHolder1_gvsalescost_lbl_itemccost_' + i + '').text(parseFloat(UnitCost) * parseFloat(Quantity));
                    $('#ContentPlaceHolder1_gvsalescost_txtRecommendedCost_' + i + '').val(parseFloat(TotalCostForignCurrency).toFixed(2));
                }
            }
        }

        function BindPackagingHead(ele) {
            if ($('#' + ele.id).val() != "") {
                var len = $('#ContentPlaceHolder1_gvsalescost').find('tr').length - 1;
                ForignCurrencyValue = $('#ContentPlaceHolder1_ddlCurrency').val().split('/')[1];
                for (i = 0; i < len; i++) {
                    var Per = $('#ContentPlaceHolder1_gvsalescost_txtPackagingCost_' + i + '').val(ele.value);

                    var txtval = $('#ContentPlaceHolder1_gvsalescost_txtPackagingCost_' + i + '').val();

                    var PackagingCost = parseFloat(parseInt($('#ContentPlaceHolder1_gvsalescost_lblmarketingOverHeadCost_' + i + '').text()) * ((parseInt(txtval)) / 100)).toFixed(2);
                    $('#ContentPlaceHolder1_gvsalescost_lblPackagingCosthead_' + i + '').text(PackagingCost);

                    var AddtionalPartCost = $('#ContentPlaceHolder1_gvsalescost_lblAddtionalPartCost_' + i + '').text();
                    var IssuePartCost = $('#ContentPlaceHolder1_gvsalescost_lblIssuePartCost_' + i + '').text();
                    var MarketingCost = 0;
                    if ($('#ContentPlaceHolder1_gvsalescost_lblmarketingOverHeadCost_' + i + '').text() != '') MarketingCost = $('#ContentPlaceHolder1_gvsalescost_lblmarketingOverHeadCost_' + i + '').text();

                    var UnitCostINR = parseFloat(parseFloat(MarketingCost) + parseFloat(PackagingCost) + parseFloat(AddtionalPartCost) - parseFloat(IssuePartCost)).toFixed(2);

                    $('#ContentPlaceHolder1_gvsalescost_lblUnitCost_' + i + '').text(parseFloat(UnitCostINR).toFixed(2));

                    var UnitCostForignCurrency = parseFloat(UnitCostINR) / parseFloat(ForignCurrencyValue);
                    $('#ContentPlaceHolder1_gvsalescost_txtUnitCostForignCurrency_' + i + '').val(parseFloat(UnitCostForignCurrency).toFixed(2));

                    var UnitCost = $('#ContentPlaceHolder1_gvsalescost_lblUnitCost_' + i + '').text();
                    var Quantity = $('#ContentPlaceHolder1_gvsalescost_lblTACHeader_' + i + '').val();

                    var TotalCostForignCurrency = parseFloat(UnitCost) * parseFloat(Quantity) / parseFloat(ForignCurrencyValue);
                    $('#ContentPlaceHolder1_gvsalescost_lblTotalForignCurrency_' + i + '').text(parseFloat(TotalCostForignCurrency).toFixed(2));

                    $('#ContentPlaceHolder1_gvsalescost_lbl_itemccost_' + i + '').text(parseFloat(UnitCost) * parseFloat(Quantity));
                    $('#ContentPlaceHolder1_gvsalescost_txtRecommendedCost_' + i + '').val(parseFloat(TotalCostForignCurrency).toFixed(2));
                }
            }
        }

        function CalculateTotalCost(ele) {
            var UnitCost = $('#' + ele.id).closest('tr').find('.UnitCost').text();
            var Quantity = $('#' + ele.id).val();
            ForignCurrencyValue = $('#ContentPlaceHolder1_ddlCurrency').val().split('/')[1];

            var TotalCostForignCurrency = parseFloat(UnitCost) * parseFloat(Quantity) / parseFloat(ForignCurrencyValue);
            $('#' + ele.id).closest('tr').find('.totalpriceINR').text(parseFloat(UnitCost * Quantity).toFixed(2));

            $('#' + ele.id).closest('tr').find('.gvItemcost').text(parseFloat(parseInt(UnitCost) * parseInt(Quantity)).toFixed(2));
            $('#' + ele.id).closest('tr').find('.TotalCostForignCurrency').text(parseFloat(TotalCostForignCurrency).toFixed(2));
            $('#' + ele.id).closest('tr').find('.RecommendedCost').val(parseFloat(TotalCostForignCurrency).toFixed(2));
        }

        function UnitCostForignCurrency(ele) {
            ForignCurrencyValue = $('#ContentPlaceHolder1_ddlCurrency').val().split('/')[1];
            var NewUnitCost = parseFloat($(ele).val()) * parseFloat(ForignCurrencyValue);
            var PrevUnitCost = $('#' + ele.id).closest('tr').find('.UnitCost').text();
            var PrevMarketingCost = $('#' + ele.id).closest('tr').find('.MarketingOverheadValueCost').text();
            var NewMarketingCost;
            var BomCost = $('#' + ele.id).closest('tr').find('.BomCost').text();
            var NewMarketingPercentage;
            var AddPartBomCost = $('#' + ele.id).closest('tr').find('.AddPartBomCost').text();
            var IssuePartBomCost = $('#' + ele.id).closest('tr').find('.IssuePartBomCost').text();
            var PackagingCost = parseInt($('#' + ele.id).closest('tr').find('.PackagingCostValue').text());

            var ValidCost = parseFloat(BomCost) + parseFloat(AddPartBomCost) - parseFloat(IssuePartBomCost)  + parseFloat(PackagingCost);

            if (ValidCost <= NewUnitCost) {
                $('#' + ele.id).closest('tr').find('.UnitCost').text(parseFloat(NewUnitCost).toFixed(2));

                var DifferCost = NewUnitCost - PrevUnitCost;
                if (DifferCost > 0)
                    NewMarketingCost = parseFloat(PrevMarketingCost) + parseFloat(DifferCost);
                else
                    NewMarketingCost = parseFloat(PrevMarketingCost) - parseFloat(Math.abs(DifferCost));

                $('#' + ele.id).closest('tr').find('.MarketingOverheadValueCost').text(parseFloat(NewMarketingCost).toFixed(2));

                NewMarketingPercentage = (1 - (parseFloat(BomCost) / parseFloat(NewMarketingCost))) * 100;
                $('#' + ele.id).closest('tr').find('.Marketing').val(parseFloat(NewMarketingPercentage).toFixed(2));

                var UnitCost = $('#' + ele.id).closest('tr').find('.UnitCost').text();
                var Quantity = $('#' + ele.id).closest('tr').find('.ItemQty').val();

                $('#' + ele.id).closest('tr').find('.totalpriceINR').text(parseFloat(UnitCost * Quantity).toFixed(2));

                var TotalCostForignCurrency = parseFloat(UnitCost) * parseFloat(Quantity) / parseFloat(ForignCurrencyValue);

                $('#' + ele.id).closest('tr').find('.TotalCostForignCurrency').text(parseFloat(TotalCostForignCurrency).toFixed(2));

                $('#' + ele.id).closest('tr').find('.gvItemcost').text(parseFloat(parseFloat(UnitCost) * parseFloat(Quantity)).toFixed(2));
                $('#' + ele.id).closest('tr').find('.RecommendedCost').val(parseFloat(TotalCostForignCurrency).toFixed(2));
            }
            else {
                ErrorMessage('Error', 'Unit Cost Shoulb be Greater Than Equal to BomCost+AddtionalPartCost+PackingCost');
                var OldForignUnitCost = parseFloat(PrevUnitCost) / parseFloat(ForignCurrencyValue);
                $(ele).val(parseFloat(OldForignUnitCost).toFixed(2));
            }
        }

        function RecommendValidation(ele) {
            try {
                var totalcost = $('#' + ele.id).closest('tr').find('.TotalCostForignCurrency').text();
                if ($('#' + ele.id).val() != "") {
                    if (totalcost > $('#' + ele.id).val()) {
                        $('#' + ele.id).val(totalcost);
                        ErrorMessage("Recommended cost can't be lesser than Total Cost");
                    }
                }
                else {
                    $('#' + ele.id).val(totalcost);
                    ErrorMessage("Recommended cost can't be empty");
                }
            } catch { }
        }

        function btnAssignCost() {
            var strMarket = "";
            var strPackag = "";
            var strRecommended = "";

            var strDDIDCostQty = "";
            var strUnitCostForignCurrency = "";
            var msg = true;
            //            $('#ContentPlaceHolder1_gvsalescost').find('.Marketing').closest('td').each(function () {
            //                $(this).find('input[type="text"]').val();
            //                $(this).find('span').text();
            //            });

            var msg = Mandatorycheck('ContentPlaceHolder1_divOutput');

            if (msg) {
                $('#ContentPlaceHolder1_gvsalescost').find('.DDID').each(function () {
                    var DDID = $(this).text();
                    var AddtionalPartCost = $(this).closest('tr').find('.AddPartBomCost ').text();
                    var IssuePartCost = $(this).closest('tr').find('.IssuePartBomCost ').text();
                    var ItemQty = $(this).closest('tr').find('.ItemQty ').val();

                    if (strDDIDCostQty == "") {
                        strDDIDCostQty = DDID + "_" + AddtionalPartCost + "_" + IssuePartCost + "_" + ItemQty;
                    }
                    else {
                        strDDIDCostQty = strDDIDCostQty + ',' + DDID + "_" + AddtionalPartCost + "_" + IssuePartCost + "_" + ItemQty;
                    }
                });

                $('#ContentPlaceHolder1_gvsalescost').find('.Marketing').each(function () {
                    var MarketPer = $(this).closest('td').find('input[type="text"]').val();
                    var marketCost = $(this).closest('td').find('span').text();
                    if (strMarket == "") {
                        strMarket = MarketPer + "_" + marketCost;
                    }
                    else {
                        strMarket = strMarket + ',' + MarketPer + "_" + marketCost;
                    }
                });

                $('#ContentPlaceHolder1_gvsalescost').find('.Packaging').each(function () {
                    var PackagePer = $(this).closest('td').find('input[type="text"]').val();
                    var PackageCost = $(this).closest('td').find('span').text();
                    if (strPackag == "") {
                        strPackag = PackagePer + "_" + PackageCost;
                    }
                    else {
                        strPackag = strPackag + ',' + PackagePer + "_" + PackageCost;
                    }
                });

                $('#ContentPlaceHolder1_gvsalescost').find('.RecommendedCost').each(function () {
                    //    var RecommendedCost = $(this).closest('td').find('input[type="text"]').val();
                    var RecommendedCost = $(this).val();
                    if (parseFloat(RecommendedCost).toFixed(2) >= parseFloat($(this).closest('tr').find('.TotalCostForignCurrency').text()).toFixed(2)) {
                        if (strRecommended == "") {
                            strRecommended = RecommendedCost;
                        }
                        else {
                            strRecommended = strRecommended + ',' + RecommendedCost;
                        }
                    }
                    else {
                        msg = false;
                        ErrorMessage('Error', 'Recommended Cost should not Greater Than Total Price');
                        hideLoader();
                        return false;
                    }
                });


                $('#ContentPlaceHolder1_gvsalescost').find('.UnitCostForignCurrency').each(function () {
                    var UnitCostForignCurrency = $(this).val();
                    if (strUnitCostForignCurrency == "") {
                        strUnitCostForignCurrency = UnitCostForignCurrency;
                    }
                    else {
                        strUnitCostForignCurrency = strUnitCostForignCurrency + ',' + UnitCostForignCurrency;
                    }
                });


                if (msg) {
                    $('#<%=hdnMarket.ClientID %>').val(strMarket);
                    $('#<%=hdnPackag.ClientID %>').val(strPackag);
                    $('#<%=hdnRecommandedCost.ClientID %>').val(strRecommended);
                    $('#<%=hdnDDIDCostQty.ClientID %>').val(strDDIDCostQty);
                    $('#<%=hdnUnitCostForignCurrency.ClientID %>').val(strUnitCostForignCurrency);
                    return true;
                }
                else {
                    $('#<%=hdnMarket.ClientID %>').val('');
                    $('#<%=hdnPackag.ClientID %>').val('');
                    $('#<%=hdnRecommandedCost.ClientID %>').val('');
                    return false;
                }
            }
            else {
                return false;
            }
        }

        function ShowBOMCostDetailsPopUp() {
            $('#mpeBomCostDetailsPopUp').modal({
                backdrop: 'static'

            });
            return false;
        }

        function ShowViewPopUp() {
            $('#mpeView').modal({
                backdrop: 'static'
            });
            return false;
        }

        function Currencyname(ele) {
            ForignCurrencyValue = $(ele).val().split('/')[1];

            if ($(ele).val().split('/')[0] != '0') {
                var ShortCode = $("#ContentPlaceHolder1_ddlCurrency option:selected").text().split('(')[1].replace(')', '').trim();
                if (ForignCurrencyValue != '0') {
                    $('#ContentPlaceHolder1_lblCurrencyName').text('INR Value : ' + parseFloat($(ele).val().split('/')[1]).toFixed(2));
                    $('#ContentPlaceHolder1_gvsalescost').find('.UnitCostForignCurrencyHeader').html('Unit Price' + '<span style="display:block;">( ' + ShortCode + ' )</span>')
                    $('#ContentPlaceHolder1_gvsalescost').find('.TotalCostForignCurrencyHeader').html('Total Price' + '<span style="display:block;">( ' + ShortCode + ' )</span>')

                    for (i = 0; i < $('#ContentPlaceHolder1_gvsalescost').find('tr').length - 1; i++) {
                        var UnitCostINR = $('#ContentPlaceHolder1_gvsalescost_lblUnitCost_' + i + '').text();

                        if (parseFloat(UnitCostINR) > 0) {
                            var UnitCostForignCurrency = parseFloat(UnitCostINR) / parseFloat(ForignCurrencyValue);
                            $('#ContentPlaceHolder1_gvsalescost_txtUnitCostForignCurrency_' + i + '').val(parseFloat(UnitCostForignCurrency).toFixed(2));

                            var TotalCostINR = $('#ContentPlaceHolder1_gvsalescost_lbl_itemccost_' + i + '').text();
                            var TotalCostForignCurrency = parseFloat(TotalCostINR) / parseFloat(ForignCurrencyValue);

                            $('#ContentPlaceHolder1_gvsalescost_lblTotalForignCurrency_' + i + '').text(parseFloat(TotalCostForignCurrency).toFixed(2));
                            $('#ContentPlaceHolder1_gvsalescost_txtRecommendedCost_' + i + '').val(parseFloat(TotalCostForignCurrency).toFixed(2));
                        }
                    }
                }
                else {
                    ErrorMessage('Error', 'Please add INR value for ' + ShortCode + ' in master page');
                    $('#ContentPlaceHolder1_lblCurrencyName').text('INR Value: 0.00');
                    $('#ContentPlaceHolder1_gvsalescost').find('.UnitCostForignCurrencyHeader').html('Unit Price' + '<span style="display:block;"> ( ' + ShortCode + ' )</span>')
                    $('#ContentPlaceHolder1_gvsalescost').find('.TotalCostForignCurrencyHeader').html('Total Price' + '<span style="display:block;"> ( ' + ShortCode + ' )</span>')

                    for (i = 0; i < $('#ContentPlaceHolder1_gvsalescost').find('tr').length - 1; i++) {
                        $('#ContentPlaceHolder1_gvsalescost_txtUnitCostForignCurrency_' + i + '').val(0.00);

                        $('#ContentPlaceHolder1_gvsalescost_lblTotalForignCurrency_' + i + '').text(0.00);
                        $('#ContentPlaceHolder1_gvsalescost_txtRecommendedCost_' + i + '').val(0.00);
                    }
                }
            }
            else {
                $('#ContentPlaceHolder1_gvsalescost').find('.TotalCostForignCurrencyHeader').text('Total Price ( INR/Forign Currency )');
                $('#ContentPlaceHolder1_gvsalescost').find('.UnitCostForignCurrencyHeader').text('Unit Price' + '( INR/Forign Currency )');
                $('#ContentPlaceHolder1_lblCurrencyName').text('INR Value: 0.00');

                for (i = 0; i < $('#ContentPlaceHolder1_gvsalescost').find('tr').length - 1; i++) {

                    var UnitCostForignCurrency = parseFloat(UnitCostINR) / parseFloat(ForignCurrencyValue);
                    $('#ContentPlaceHolder1_gvsalescost_txtUnitCostForignCurrency_' + i + '').val(0.00);

                    $('#ContentPlaceHolder1_gvsalescost_lblTotalForignCurrency_' + i + '').text(0.00);
                    $('#ContentPlaceHolder1_gvsalescost_txtRecommendedCost_' + i + '').val(0.00);
                }
            }

            return true;
        }

        function btnCalculateCost() {
            var bomcost = 0;
            var AddtionalPartCost = 0;
            var RecommendedCost = 0;
            var ItemQty = 0;
            var TotalCost = 0;
            var TotalCostForignCurrency = 0;
            $('table').find('.BomCost').each(function () {
                AddtionalPartCost = $(this).closest('tr').find('.AddPartBomCost').text();
                IssuePartCost = $(this).closest('tr').find('.IssuePartBomCost').text();
                ItemQty = $(this).closest('tr').find('.ItemQty').val();
                TotalCost = parseFloat(TotalCost) + ((parseFloat($(this).text()) + parseFloat(AddtionalPartCost) - parseFloat(IssuePartCost)) * parseFloat(ItemQty));
            });
            //$('table').find('.AddPartBomCost').each(function () {
            //    AddtionalPartCost = parseFloat(AddtionalPartCost) + parseFloat($(this).text())
            //});
            $('table').find('.totalpriceINR').each(function () {
                RecommendedCost = parseFloat(RecommendedCost) + parseFloat($(this).text())
            });
            //$('table').find('.ItemQty').each(function () {
            //    ItemQty = parseInt(ItemQty) + parseInt($(this).val())
            //});

            //  var TotalUnitCost = bomcost + AddtionalPartCost;
            // var TotalCost = TotalUnitCost * (ItemQty / 2);

            $('.TotalCost').text(parseFloat(TotalCost).toFixed(2));
            $('.TotalRecommendedCost').text(parseFloat(RecommendedCost).toFixed(2));

            var a = $('#ContentPlaceHolder1_ddlCurrency option:selected').text();
            if (a.includes('INR')) {
                $('#divtrpforigncostcurrncy').hide();
            }
            else {
                $('#divtrpforigncostcurrncy').show();
                $('table').find('.RecommendedCost').each(function () {
                    TotalCostForignCurrency = parseFloat(TotalCostForignCurrency) + parseFloat($(this).val())
                });
                $('#ContentPlaceHolder1_lbltrpforigncostcurrency').text(parseFloat(TotalCostForignCurrency).toFixed(2));
                $('.trpforigncurrency').text('(' + $('#ContentPlaceHolder1_ddlCurrency option:selected').text().split('(')[1].replace(')', '').trim() + ')');
            }
            return false;
        }

        function SaveConfirm() {
            swal({
                title: "Are you sure?",
                text: "If Yes, No Further Edit Once Cost Shared",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Share it!",
                closeOnConfirm: false
            }, function () {
                __doPostBack("CostAccepted", "");
            });
            return false;
        }

        function ViewCostingprint(epstyleurl, Main, QrCode) {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var RowLength = $('#ContentPlaceHolder1_gvItemCostingDetails_pdf').find('tr').length - 1;

            var PageLength = parseInt(RowLength / 50);

            var CostingHeader = $('#ContentPlaceHolder1_divcostingheader').html();
            var divcostbottomcontent = $('#ContentPlaceHolder1_divcostbottomcontent').html();

            var k = 1;
            var lastrecord = false;
            winprint.document.write("<html><head><title>");
            winprint.document.write("Offer");
            winprint.document.write("</title>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='" + epstyleurl + "'/>");
            winprint.document.write("<link rel='stylesheet' href='" + Main + "' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet'  href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/printg.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<style type='text/css'>@media print,screen { label,table th{ font-weight: bold; font-size: 15px !important;font-family:Times New Roman;color:#000 !important; } @page {size: landscape} } .row{ padding-top:10px; } .page_generateoffer{ margin: 6mm; } table th{ vertical-align: middle; } .page_generateoffer table td {font-size: 8px; font-weight: bold;color: #000;} .table th,.table td {  padding: 2px !important; } </style>");
            winprint.document.write("<style type='text/css'> .page_generateoffer table th { font-size: 8px ! important; font-weight: bold;color: #000 ! important;vertical-align: middle;text-align: center;  background: #fff !important;border: 1px solid #000 !important;}.page_generateoffer{margin-top: 0px !important} </style>");

            //winprint.document.write("<div class='print-page' style='position:fixed;width:200mm;height:287mm;margin-top:5mm;margin-bottom:5mm;margin-left:5mm;margin-right:5mm'></div>");
            winprint.document.write("<table style='width:287mm;height:200mm;margin-left:20px;border:0px !important'>");
            winprint.document.write("<thead><tr><td style='height: 90px;'>");

            winprint.document.write(CostingHeader);

            winprint.document.write("</td></tr></thead>");
            winprint.document.write("<tbody><tr><td>");

            //class='page_generateoffer'

            //   for (var i = 0; i <= PageLength; i++) {
            winprint.document.write("<div class='page_generateoffer'>");
            winprint.document.write("<div style='padding-right: 15px !important; padding-left: 0px !important'>")
            winprint.document.write("<table class='table table-hover medium' cellspacing='0' rules='all' border='1' id='ContentPlaceHolder1_gvItemCostingDetails_pdf' style='border-collapse:collapse;table-layout: fixed;width: 287mm;'>");
            winprint.document.write("<thead>");
            winprint.document.write($('#ContentPlaceHolder1_gvItemCostingDetails_pdf').find('tr:first').html());
            winprint.document.write("</thead>");
            winprint.document.write("<tbody>");

            for (var j = k; j < k + 50; j++) {
                if (RowLength == j) {
                    winprint.document.write("<tr align='center' style='border:white;'>" + $('#ContentPlaceHolder1_gvItemCostingDetails_pdf').find('tr')[j].innerHTML + "</tr>");
                    lastrecord = true;
                    break;
                }
                else
                    winprint.document.write("<tr align='center'>" + $('#ContentPlaceHolder1_gvItemCostingDetails_pdf').find('tr')[j].innerHTML + "</tr>");
            }

            winprint.document.write("</tbody>");
            winprint.document.write("</table>");
            winprint.document.write("</div>");

            if (lastrecord) {
                winprint.document.write(divcostbottomcontent);
            }
            winprint.document.write("</div>");

            //if (lastrecord) {
            //    break;
            //}
            k = k + 50;
            // }

            winprint.document.write("</td></tr></tbody>");
            winprint.document.write("<tfoot><tr><td>");
            winprint.document.write("<div style='height:25mm;'>");
            winprint.document.write("<div class='row' style='margin-bottom:20px;position:fixed;width:200mm;bottom:0px;'>");
            winprint.document.write("<img  class='Qrcode' style='height:80%;' src='" + QrCode + "' />");
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
        .radio-success label:nth-child(2) {
            color: red  ! important;
        }

        .radio-success label:nth-child(4) {
            color: #0acc0a ! important;
        }
    </style>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                    <h3 class="page-title-head d-inline-block">Sales Costing</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Sales</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Sales Costing</li>
                                                </ol>
                                     </nav>
                    </div>
                </div>
            </div>
        </div>
        <div>
            <asp:UpdatePanel ID="upQuote" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlEnquiryNumber" EventName="SelectedIndexChanged" />
                    <asp:PostBackTrigger ControlID="gvsalescost" />
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
                                        <asp:RadioButtonList ID="rblenquirychange" runat="server" CssClass="radio radio-success" RepeatDirection="Horizontal"
                                            AutoPostBack="true" OnSelectedIndexChanged="rblenquirychange_OnSelectedChanged" RepeatLayout="Flow">
                                            <asp:ListItem Selected="True" Value="0">INCOMPLETE</asp:ListItem>
                                            <asp:ListItem Value="1">COMPLETED</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Customer Name</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="true" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlCustomerName_SelectIndexChanged" Width="70%" ToolTip="Select Enquiry Number">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Enquiry Number</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlEnquiryNumber" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEnquiryNumber_SelectIndexChanged"
                                            CssClass="form-control" Width="70%" ToolTip="Select Enquiry Number">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Currency
                                        </label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlCurrency" runat="server" AutoPostBack="false" CssClass="form-control"
                                            OnChange="Currencyname(this);" Width="70%" ToolTip="Select Currency Number">
                                            <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="lblCurrencyName" Style="color: brown;" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-1 text-left">
                                    </div>
                                    <div class="col-sm-1">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <%--style="padding-right: 104px;"--%>
                                        <div class="col-sm-12 text-center">
                                            <asp:Label ID="lblOfferVersionNote" Style="font-size: x-large; color: Blue;" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-12 p-t-10 p-l-0 p-r-0" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvsalescost" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                OnRowCommand="gvsalescost_RowCommand" CssClass="table table-hover table-bordered medium pagingfalse"
                                                OnRowDataBound="gvsalescost_OnRowDataBound" HeaderStyle-HorizontalAlign="Center"
                                                EmptyDataText="No Records Found" DataKeyNames="EnquiryID,DDID,FileName,CostEstimatedBy,CostEstimatedDate,OverAllLength,LseNumber,DrawingName,Qty">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" class="gvsno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'
                                                                Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Item Name" HeaderText="Item Name" ItemStyle-Width="30%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPoint" class="gvItemname" runat="server" Text='<%# Eval("Itemname")%>'
                                                                Style="text-align: center"></asp:Label>
                                                            <asp:Label ID="lblEnquiryid" runat="server" Text='<%# Eval("EnquiryID")%>' Style="display: none"></asp:Label>
                                                            <asp:Label ID="lblDDID" CssClass="DDID" runat="server" Text='<%# Eval("DDID")%>'
                                                                Style="display: none"></asp:Label>
                                                            <asp:Label ID="lblProductid" runat="server" Text='<%# Eval("ProductID")%>' Style="display: none"></asp:Label>
                                                            <%--  <asp:Label ID="lblbomcost" runat="server" Text='<%# Eval("BomCost")%>' Style="display: none"></asp:Label>--%>
                                                            <asp:Label ID="lblexistcost" runat="server" Text='<%# Eval("TotalCost")%>' Style="display: none"></asp:Label>
                                                            <%--   <asp:Label ID="lblProductionOverhead" runat="server" Text='<%# Eval("ProductionOverhead")%>'
                                                                Style="display: none"></asp:Label>
                                                            <asp:Label ID="lblConsumableCost" runat="server" Text='<%# Eval("ConsumableCost")%>'
                                                                Style="display: none"></asp:Label>--%>
                                                            <asp:Label ID="lblPackagingCost" runat="server" Text='<%# Eval("PackagingCost")%>'
                                                                Style="display: none"></asp:Label>
                                                            <asp:Label ID="lblMakingOverhead" runat="server" Text='<%# Eval("MakingOverhead")%>'
                                                                Style="display: none"></asp:Label>
                                                            <%--  <asp:Label ID="lblMiscExpense" runat="server" Text='<%# Eval("MiscExpense")%>' Style="display: none"></asp:Label>--%>
                                                            <asp:Label ID="lblRecommendedcost" runat="server" Text='<%# Eval("Recommendedcost")%>'
                                                                Style="display: none"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Size" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemSize" runat="server" Text='<%# Eval("ItemSize")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tag No/Item Code/Material Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTagNoItemCodeMat" runat="server" Text='<%# Eval("ItemTagNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Drawing No">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDeviationFile" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="ViewDeviationFile">
                                                                <asp:Label ID="lblDrawingNo" runat="server" Text='<%# Eval("DrawingNumber")%>'></asp:Label>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="View BOMCOst" HeaderText="BOM Cost" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBOMCost" class="gvqty BomCost" runat="server" Text='<%# Eval("BomCost")%>'
                                                                Style="text-align: center; display: none;"></asp:Label>
                                                            <asp:LinkButton ID="lbtnViewBOMCost" runat="server" Text='<%# Eval("BomCost")%>'
                                                                CommandArgument='<%#((GridViewRow) Container).RowIndex %>' CommandName="ViewBOMCost"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Addtional Part Cost" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAddtionalPartCost" class="gvqty AddPartBomCost" runat="server"
                                                                Text='<%# Eval("AdditionalPartBomCost")%>' Style="text-align: center;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="FIM Cost" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIssuePartCost" class="gvqty IssuePartBomCost" runat="server"
                                                                Text='<%# Eval("IssuePartBomCost")%>' Style="text-align: center;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--    <asp:TemplateField HeaderText="Cost Estimate" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:RadioButton ID="rdoexist" onclick='<%# string.Format("return existcostclick({0});",Eval("TotalCost")) %>'
                                                                Class="rdobtn" runat="server" GroupName="SuppliersGroup" Text="Exist"></asp:RadioButton>
                                                            <asp:RadioButton ID="rdonew" onclick='<%# string.Format("return radiobtnchange({0},\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\",\"{8}\");",
                                                            Eval("BomCost"),Eval("DDID"),Eval("ProductID"),Eval("ProductionOverhead"),Eval("ConsumableCost"),Eval("PackagingCost"),
                                                            Eval("MakingOverhead"),Eval("MiscExpense"),Eval("Recommendedcost")) %>' Class="rdobtn rdobtnnew"
                                                                runat="server" GroupName="SuppliersGroup" Text="New"></asp:RadioButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderStyle-CssClass="marketingclass">
                                                        <HeaderTemplate>
                                                            <span>Marketing (%)</span>
                                                            <asp:TextBox ID="txtMarketingOverHead_H" runat="server" onblur="return BindMarketingOverHead(this);"
                                                                CssClass="form-control" onkeypress="return validationNumeric(this);"></asp:TextBox>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtMarketingOverHead" CssClass="Marketing form-control mandatoryfield"
                                                                runat="server" onkeyup="return BindMarketingOverHeadCost(this);" onkeypress="return validationNumeric(this);"
                                                                Text='<%# Eval("MarketingCostPercentage")%>'></asp:TextBox>
                                                            <asp:Label ID="lblmarketingOverHeadCost" CssClass="MarketingOverheadValueCost" Text='<%# Eval("MarketingCost").ToString().Replace(".00","")%>'
                                                                runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <span>Packaging (%)</span>
                                                            <asp:TextBox ID="txtPackagingCost_H" runat="server" onblur="return BindPackagingHead(this);"
                                                                CssClass="form-control" onkeypress="return validationNumeric(this);"></asp:TextBox>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtPackagingCost" CssClass="Packaging form-control" runat="server"
                                                                onkeyup="return BindPackagingCost(this);" onkeypress="return validationNumeric(this);"
                                                                Text='<%# Eval("PackagingPercentage")%>'></asp:TextBox>
                                                            <asp:Label ID="lblPackagingCosthead" CssClass="PackagingCostValue" Text='<%# Eval("PackagingCost")%>'
                                                                runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit Price (INR)" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label class="UnitCost" ID="lblUnitCost" Text='<%# Eval("UnitCost")%>' runat="server"
                                                                Style="display: flex; margin: 0px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit Price (INR/Foreign Currency)" ItemStyle-Width="10%"
                                                        HeaderStyle-CssClass="UnitCostForignCurrencyHeader" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox class="UnitCostForignCurrency form-control mandatoryfield" ID="txtUnitCostForignCurrency"
                                                                onblur="return UnitCostForignCurrency(this)" CssClass="UnitCostForignCurrency form-control"
                                                                Text='<%# Eval("UnitCostForignCurrency")%>' runat="server" Style="display: flex; margin: 0px; width: 120px;"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Dead InVentory" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDeadInventory" class="gvqty blinking" runat="server" Text='<%# Eval("DeadInventoryAmount")%>'
                                                                Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Quantity" HeaderText="Qty" ItemStyle-Width="5%"
                                                        ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="lblTACHeader" class="gvqty ItemQty form-control mandatoryfield"
                                                                runat="server" onkeypress="return validationNumeric(this);" onkeyup="return CalculateTotalCost(this);"
                                                                Text='<%# Eval("Qty")%>' Style="text-align: center; width: 100px;"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Price (INR)" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label class="lbl_itemccost gvItemcost" ID="lbl_itemccost" CssClass="totalpriceINR" Text='<%# Eval("TotalCost")%>'
                                                                runat="server" Style="display: flex; margin: 0px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Price (INR/Foreign Currency)" ItemStyle-Width="10%"
                                                        HeaderStyle-CssClass="TotalCostForignCurrencyHeader" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label class="lbl_itemccost gvItemcost" ID="lblTotalForignCurrency" CssClass="TotalCostForignCurrency"
                                                                Text='<%# Eval("TotalCostForignCurrency")%>' runat="server" Style="display: flex; margin: 0px; width: 120px;"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Recommended Price" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <%--   <asp:Label class="lbl_Recommendedcost gvRecommendedcost" ID="lbl_Recommendedcost"
                                                                Text='<%# Eval("Recommendedcost")%>' runat="server" Style="margin: 0px"></asp:Label>--%>
                                                            <asp:TextBox ID="txtRecommendedCost" CssClass="form-control RecommendedCost mandatoryfield"
                                                                runat="server" onkeyup="return RecommendValidation(this);" onkeypress="return validationNumeric(this);"
                                                                Text='<%# Eval("RecommendedCost")%>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Dead InVentory Remarks" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDeadInventoryRemarks" runat="server" Text='<%# Eval("DeadInventoryRemarks")%>'
                                                                Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Approved Price" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label class="lbl_Approvedcost gvRecommendedcost" ID="lbl_Approvedcost" Text='<%# Eval("ApprovedCost")%>'
                                                                runat="server" Style="margin: 0px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Revised Reason" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label class="lbl_revisedreason gvRecommendedcost" ID="lbl_revisedreason" Text='<%# Eval("Revisedreason")%>'
                                                                runat="server" Style="margin: 0px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Previous Order Value">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPrevoiusOrderValue" runat="server" Text='<%# Eval("PrevCost")%>'
                                                                Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="INR Value">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblINRValue" runat="server" Text='<%# Eval("INRValue")%>' Style="text-align: center"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4 text-right">
                                                <label class="form-label">
                                                    Total Cost
                                                </label>
                                            </div>
                                            <div class="col-sm-6 text-left">
                                                <asp:Label ID="lblTotalCost" CssClass="TotalCost" Text="0.00" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-1 text-left">
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4 text-right">
                                                <label class="form-label">
                                                    Total Recommended Price (INR)
                                                </label>
                                            </div>
                                            <div class="col-sm-6 text-left">
                                                <asp:Label ID="lblTotalCalculatedPrice" CssClass="TotalRecommendedCost" Text="0.00"
                                                    runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-1 text-left">
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                        </div>

                                        <div class="col-sm-12 p-t-10" id="divtrpforigncostcurrncy" style="display: none;">
                                            <div class="col-sm-4 text-right">
                                                <label class="form-label">
                                                    Total Recommended Price <span class="trpforigncurrency"></span>
                                                </label>
                                            </div>
                                            <div class="col-sm-6 text-left">
                                                <asp:Label ID="lbltrpforigncostcurrency" CssClass="" Text="0.00"
                                                    runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-1 text-left">
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                        </div>

                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4 text-right">
                                                <label class="form-label">
                                                    Note
                                                </label>
                                            </div>
                                            <div class="col-sm-6 text-left">
                                                <asp:TextBox ID="txtNote" runat="server" CssClass="form-control" TextMode="MultiLine"
                                                    Rows="3"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-1 text-left">
                                            </div>
                                            <div class="col-sm-1">
                                            </div>
                                        </div>
                                        <asp:HiddenField ID="hdnMarket" runat="server" />
                                        <asp:HiddenField ID="hdnPackag" runat="server" />
                                        <asp:HiddenField ID="hdnRecommandedCost" runat="server" />
                                        <asp:HiddenField ID="hdnDDIDCostQty" runat="server" />
                                        <asp:HiddenField ID="hdnUnitCostForignCurrency" runat="server" />
                                        <div class="modal" id="modelAddModule">
                                            <div class="modal-dialog" style="max-width: 95%;">
                                                <div class="modal-content">
                                                    <div class="modal-header">
                                                        <h4 class="modal-title bold" style="color: #800b4f; margin: auto; text-align: center;"
                                                            id="tournamentNameTeam">Sales Cost Sheet</h4>
                                                        <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true"
                                                            style="margin: -1rem 1rem -1rem 1rem;" onclick="return Hidepopup();">
                                                            ×</button>
                                                    </div>
                                                    <div class="inner-container" style="width: 50%; margin: auto;">
                                                        <%-- class="tr_class table"      --%>
                                                        <div id="gvcontent" class="col-sm-12 popupcontent" style="padding-top: 10px; padding-left: 20px;"
                                                            runat="server">
                                                            <table align="center" id="tbl_customerDetails" class="htmltable">
                                                                <tbody>
                                                                    <tr>
                                                                        <td align="left">
                                                                            <label id="lab_drwcost" class="eqdetailslabel">
                                                                                Drawing Cost</label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:Label ID="txt_drwcost" class="txtbomcost form-control costinclude hdn_Drawingcost"
                                                                                runat="server" MaxLength="2000" Style="background-color: #e7e7e7; color: black;"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left">
                                                                            <label id="lab_prodoverhead" class="eqdetailslabel">
                                                                                Production Overhead</label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txt_prodoverhead" runat="server" Class="form-control costinclude txt_ProductionOverhead"
                                                                                ToolTip="Enter Production Overhead" onkeypress="return validationNumeric(this);"
                                                                                onblur="return totalcost()" placeholder="Enter Production Overhead" MaxLength="2000"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left">
                                                                            <label id="lab_conscost" class="eqdetailslabel">
                                                                                Consumable Cost</label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txt_conscost" runat="server" Class="form-control costinclude txt_ConsumableCost"
                                                                                ToolTip="Enter Consumable Cost" onkeypress="return validationNumeric(this);"
                                                                                onblur="return totalcost()" placeholder="Enter Consumable Cost" MaxLength="2000"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left">
                                                                            <label id="lab_packcost" class="eqdetailslabel">
                                                                                Packaging Cost</label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txt_packcost" runat="server" Class="form-control costinclude txt_PackagingCost"
                                                                                ToolTip="Enter Packaging Cost" onkeypress="return validationNumeric(this);" onblur="return totalcost()"
                                                                                placeholder="Enter Packaging Cost" MaxLength="2000"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left">
                                                                            <label id="lab_makoverhead" class="eqdetailslabel">
                                                                                Marketing Overhead</label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txt_makoverhead" runat="server" Class="form-control costinclude txt_MakingOverhead"
                                                                                ToolTip="Enter Making Overhead" onkeypress="return validationNumeric(this);"
                                                                                onblur="return totalcost()" placeholder="Enter Making Overhead" MaxLength="2000"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left">
                                                                            <label id="lab_misc" class="eqdetailslabel">
                                                                                Misc Expense</label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txt_misc" runat="server" Class="form-control costinclude txt_MiscExpense"
                                                                                ToolTip="Enter Misc Expense" onkeypress="return validationNumeric(this);" onblur="return totalcost()"
                                                                                placeholder="Enter Misc Expense" MaxLength="2000"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left">
                                                                            <label id="lab_totoal" class="eqdetailslabel">
                                                                                Total Cost</label>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:Label ID="lbl_totoal" class="hdn_TotalCost form-control" runat="server" MaxLength="2000"
                                                                                Style="background-color: #e7e7e7; color: black;"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="left">
                                                                            <label id="lab_reccost" class="eqdetailslabel">
                                                                                Recommended Quote</label>
                                                                            <span id="Label290" class="requiredfield">*</span>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:TextBox ID="txt_reccost" runat="server" Class="form-control txt_reccost" ToolTip="Enter Recommended Cost"
                                                                                onkeypress="return validationNumeric(this);" placeholder="Enter Recommended Expense"
                                                                                MaxLength="2000"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                        <div class="ip-div text-center" style="padding: 15px;">
                                                            <%--  <asp:Button ID="btnviewcost" runat="server" Text="View BOM Costing" CssClass="btn btn-cons btn-save AlignTop"
                                                                OnClick="btnviewcost_Click" Style="margin-right: 25px;"></asp:Button>
                                                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-cons btn-save AlignTop"
                                                                OnClick="btn_saveclick" OnClientClick="return saveConfirm()"></asp:Button>--%>
                                                        </div>
                                                        <asp:HiddenField ID="hdn_updatetotalcost" runat="server" Value="0" />
                                                        <asp:HiddenField ID="hdn_ddid" runat="server" Value="0" />
                                                        <asp:HiddenField ID="hdn_itemid" runat="server" Value="0" />
                                                        <asp:HiddenField ID="hdn_TotalCost" runat="server" Value="0" />
                                                        <asp:HiddenField ID="hdn_RecCost" runat="server" Value="0" />
                                                    </div>
                                                    <div class="modal-footer">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="ip-div text-center btnfirstprocess">
                                            <span id="lbl_waitforaproaval" class="lbl_waitforaproaval" runat="server" style="color: red">Note : This Enquiry Sent for Recommended Cost approval from Sales Head.</span>
                                            <span id="lbl_offermade" class="lbl_offermade" runat="server" style="color: red">Note
                                                : Final Cost Approved For this Enquiry.</span>
                                        </div>
                                        <%--OnClick="btnAccepted_Click"--%>
                                        <div class="ip-div text-center btnreprocess p-t-10">
                                            <asp:Button ID="btnCalculate" OnClientClick="return btnCalculateCost();" runat="server"
                                                CssClass="btn btn-cons btn-success btnAproval" Text="Calculate Cost" />
                                            <asp:Button ID="btnAccepted" OnClientClick="return SaveConfirm();" runat="server" CssClass="btn btn-cons btn-success btnAproval"
                                                Text="Accept Approved Cost" />
                                            <asp:Button ID="btn_previewcostsheet" OnClientClick="return btnAssignCost();" OnClick="btnAproval_Click"
                                                runat="server" CssClass="btn btn-cons btn-success btnAproval" Text="Send For Approval" />
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
    <div class="modal" id="mpeBomCostDetailsPopUp">
        <%--  <div class="modal-dialog" style="max-width: 95%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H2">
                                <div class="text-center">
                                    <asp:Label ID="lblItemName_P" Text="" runat="server" Style="font-weight: bold; padding-left: 30px;"></asp:Label>
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal">
                                x</button>
                        </div>
                        <div class="modal-body">
                            <div class="inner-container">
                                <div class="col-sm-12" style="overflow-x: auto;">
                                    <div class="col-sm-12 p-t-10 text-center">
                                        <label class="form-label" style="color: Blue;">
                                            Item BOM Cost Details
                                        </label>
                                    </div>
                                    <div class="col-sm-12" id="bomdiv" runat="server" style="overflow-x: auto;">
                                        <asp:GridView ID="gvItemCostingDetails" runat="server" AutoGenerateColumns="true"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                            HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:Label ID="lblBOMCost" Text="" runat="server" Style="color: darkgreen; font-size: x-large; font-family: fantasy;"></asp:Label>
                                    </div>
                                    <div class="col-sm-12 p-t-10 text-center">
                                        <label class="form-label" style="color: Blue;">
                                            Addtional Part BOM Cost Details
                                        </label>
                                    </div>
                                    <div class="col-sm-12 p-t-10" id="AddtionalPartiv" style="overflow-x: scroll;">
                                        <asp:GridView ID="gvAddtionalPartDetails" runat="server" AutoGenerateColumns="true"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                            HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:Label ID="lblAddtionalPartCost" Text="" runat="server" Style="color: darkgreen; font-size: x-large; font-family: fantasy;"></asp:Label>
                                    </div>
                                    <div class="col-sm-12 text-center">
                                        <asp:Label ID="lblTotalBOMCost" Text="" runat="server" Style="color: brown; font-size: x-large; font-family: fantasy;"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                        </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:HiddenField ID="HiddenField1" runat="server" Value="" />
            </div>
        </div>--%>
        <div class="modal-dialog" style="max-width: 95%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btndownloaddocs" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H2">
                                <div class="text-center">
                                    <asp:Label ID="Label2" Text="" runat="server"></asp:Label>
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal">
                                x</button>
                        </div>
                        <div class="modal-body">
                            <div class="inner-container">
                                <div class="col-sm-12 text-center">
                                    <asp:LinkButton ID="btndownloaddocs" OnClientClick="return ShowHideDataTableSearch('hide');"
                                        OnClick="btndownloaddocs_Click" ToolTip="PDF DownLoad" runat="server">
                                                        <img src="../Assets/images/pdf.png" /> </asp:LinkButton>
                                </div>
                                <div id="divViewCostingPrint_p" runat="server">
                                    <div class="col-sm-12 p-t-10 text-center">
                                        <asp:Label ID="lblCustomerName_p" runat="server">                                        
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-3 p-t-10">
                                            <asp:Label ID="lblItemname_p" runat="server">                                        
                                            </asp:Label>
                                        </div>
                                        <div class="col-sm-3 p-t-10">
                                            <label>
                                                Drawing Name:</label>
                                            <asp:LinkButton ID="lbtnDeviationFile" runat="server" OnClick="btnViewDrawingFile">
                                                <asp:Label ID="lblDrawingname_p" runat="server"></asp:Label>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-3 p-t-10">
                                            <label>
                                                Tag No:</label>
                                            <asp:Label ID="lblTagNo_p" runat="server">                                        
                                            </asp:Label>
                                        </div>
                                        <div class="col-sm-3 p-t-10">
                                            <label>
                                                Total Quantity:</label>
                                            <asp:Label ID="lblTotalQty_p" runat="server">                                        
                                            </asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12" style="overflow-x: auto;">
                                        <asp:GridView ID="gvItemCostingDetails" runat="server" AutoGenerateColumns="true"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                            HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:Label ID="lblBOMCost" Text="" runat="server" Style="color: darkgreen; font-size: x-large; font-family: fantasy;"></asp:Label>
                                    </div>
                                    <div class="col-sm-12 p-t-10 text-center">
                                        <label class="form-label" style="color: Blue;">
                                            Addtional Part BOM Cost Details
                                        </label>
                                    </div>
                                    <div class="col-sm-12 p-t-10" id="AddtionalPartiv" style="overflow-x: scroll;">
                                        <asp:GridView ID="gvAddtionalPartDetails" runat="server" AutoGenerateColumns="true"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                            HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:Label ID="lblAddtionalPartCost" Text="" runat="server" Style="color: darkgreen; font-size: x-large; font-family: fantasy;"></asp:Label>
                                    </div>
                                    <div class="col-sm-12 text-center">
                                        <asp:Label ID="lblTotalBOMCost" Text="" runat="server" Style="color: brown; font-size: x-large; font-family: fantasy;"></asp:Label>
                                    </div>
                                    <div class="col-sm-12">
                                        <label>
                                            Over All Length:</label>
                                        <asp:Label ID="lblOverAllLength_p" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-12">
                                        <label>
                                            LSE Number:</label>
                                        <asp:Label ID="lblLSENumber_p" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-12">
                                        <label>
                                            Dead Inventory Remarks:</label>
                                        <asp:Label ID="lblDeadInventoryRemarks_p" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-12">
                                        <label>
                                            Remarks:</label>
                                        <asp:Label ID="lblRemarks_p" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-12">
                                        <label>
                                            Date:</label>
                                        <asp:Label ID="lblDate_p" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-12">
                                        <label>
                                            User Name:</label>
                                        <asp:Label ID="lblUserName_p" runat="server"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Image ID="imgQrcode" class="Qrcode" ImageUrl="" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                        </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:HiddenField ID="HiddenField1" runat="server" Value="" />
            </div>
        </div>
    </div>

    <div class="modal" id="mpeView">
        <div class="modal-dialog" style="height: 100%;">
            <div class="modal-content" style="height: 100%;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always" Style="height: 100%;">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H1">
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
    <div class="modal" id="mpeCosting_pdf">
        <div class="modal-dialog" style="max-width: 95%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H3">
                                <div class="text-center">
                                    <asp:Label ID="Label3" Text="" runat="server"></asp:Label>
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal">
                                x</button>
                        </div>
                        <div class="modal-body">
                            <div class="inner-container">
                                <div id="divViewCostingPrint_pdf" runat="server" style="float: left; width: 100%; margin-right: 10px;">
                                    <div id="divcostingheader" runat="server">
                                        <div class="row text-center" style="padding-left: 0 !important; padding-right: 0px !important">
                                        </div>
                                        <div class="row p-t-10 text-center" style="margin-left: 40%;">
                                            <asp:Label ID="lblCustomerName_pdf" runat="server" Style="font-size: 20px !important; font-weight: 700">                                        
                                            </asp:Label>
                                        </div>
                                        <div class="row p-t-10 text-center" style="display: flex; align-items: center; justify-content: space-around">
                                            <asp:Label ID="lblItemname_pdf" runat="server" Style="font-size: 14px !important; font-weight: 700 !important">                                        
                                            </asp:Label>
                                            <asp:Label ID="lblDrawingname_pdf" runat="server">                                        
                                            </asp:Label>
                                            <label>
                                                Tag No:</label>
                                            <asp:Label ID="lblTagNo_pdf" runat="server">                                        
                                            </asp:Label>
                                            <label>
                                                Total Quantity:</label>
                                            <asp:Label ID="lblTotalQty_pdf" runat="server">                                        
                                            </asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12" style="padding-right: 15px !important; padding-left: 0px !important">
                                        <asp:GridView ID="gvItemCostingDetails_pdf" runat="server" AutoGenerateColumns="true"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium table-bold"
                                            OnRowDataBound="gvItemCostingDetails_pdf_OnRowDataBound" OnDataBound="gvItemCostingDetails_pdf_OnDataBound" Style="font-size: 9px; width: 100% !important" HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <%--<div class="col-sm-12 p-t-10 text-center">
                                        <label class="form-label" style="color: Blue;">
                                            Addtional Part BOM Cost Details
                                        </label>
                                    </div>
                                    <div class="col-sm-12 p-t-10" id="AddtionalPartiv_pdf">
                                        <asp:GridView ID="gvAddtionalPartDetails_pdf" runat="server" AutoGenerateColumns="true"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium table-bold"
                                            Style="font-size: 9px; width: auto !important" HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:Label ID="lblAddtionalPartCost_pdf" Text="" runat="server" Style="color: darkgreen; font-size: x-large; font-family: fantasy;"></asp:Label>
                                    </div>--%>
                                    <div id="divcostbottomcontent" runat="server">
                                        <%--<div class="row text-center" style="padding-right: 15px">
                                            <asp:Label ID="lblTotalBOMCost_pdf" Text="" runat="server" Style="color: brown; font-size: x-large; float: right; font-family: fantasy;"></asp:Label>
                                        </div>--%>
                                        <div class="row">
                                            <label style="font-weight: 700; width: 16%;">
                                                Over All Length:</label>
                                            <asp:Label ID="lblOverAllLength_pdf" runat="server" Style="font-weight: 700; width: 20%;"></asp:Label>
                                            <asp:Label ID="lblBOMCost_pdf" Text="" runat="server" Style="color: darkgreen !important; font-size: x-large; float: right; font-family: fantasy; text-align: end; width: 62%; font-size: 16px ! important;"></asp:Label>
                                        </div>
                                        <div class="row">
                                            <label style="font-weight: 700; width: 16%;">
                                                LSE Number:</label>
                                            <asp:Label ID="lblLSENumber_pdf" runat="server" Style="font-weight: 700; width: 20%;"></asp:Label>
                                            <asp:Label ID="lblAddtionalPartCost_pdf" Text="" runat="server" Style="color: darkgreen !important; font-size: x-large; font-family: fantasy; text-align: end; width: 62%; font-size: 16px ! important;"></asp:Label>
                                        </div>
                                        <div class="row">
                                            <label style="font-weight: 700; width: 16%;">
                                                Dead Inventory Remarks:</label>
                                            <asp:Label ID="lblDeadInventoryRemarks_pdf" runat="server" Style="font-weight: 700; width: 20%;"></asp:Label>
                                            <asp:Label ID="lblTotalBOMCost_pdf" Text="" runat="server" Style="color: brown !important; font-size: x-large; float: right; font-family: fantasy; text-align: end; width: 62%; font-size: 16px ! important;"></asp:Label>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <label style="font-weight: 700">
                                                    Remarks:</label>
                                                <asp:Label ID="lblRemarks_pdf" runat="server" Style="font-weight: 700"></asp:Label>
                                            </div>
                                            <div class="col-sm-6" style="padding-right: 15px">
                                                <div class="col-sm-12 text-right">
                                                    <label style="font-weight: 700">
                                                        Date:</label>
                                                    <asp:Label ID="lblDate_pdf" runat="server" Style="font-weight: 700"></asp:Label>
                                                </div>
                                                <div class="col-sm-12 text-right">
                                                    <label style="font-weight: 700">
                                                        User Name:</label>
                                                    <asp:Label ID="lblUserName_pdf" runat="server" Style="font-weight: 700"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:HiddenField ID="HiddenField2" runat="server" Value="" />
            </div>
        </div>
    </div>

</asp:Content>
