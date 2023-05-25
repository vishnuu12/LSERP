<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="WorkOrderPo.aspx.cs" Inherits="Pages_WorkOrderPo" ClientIDMode="Predictable" %>

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

        function ShowViewPopUp() {
            $('#mpeShowDocument').modal({
                backdrop: 'static'
            });
            return false;
        }

        function ShowWorkOrderPOAmendmentsPopUp() {
            $('#mpeWorkOrderPOAmendments').modal({
                backdrop: 'static'
            });
            return false;
        }

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

        //function checkAll(ele) {
        //    debugger;
        //    if ($(ele).is(":checked")) {
        //        $(ele).closest('table').find('[type="checkbox"]').prop('checked', true);
        //    }
        //    else {
        //        $(ele).closest('table').find('[type="checkbox"]').prop('checked', false);
        //    }
        //}

        function ShowAddPopUp() {
            $('#mpeWorkOrderIndentDetails').modal({
                backdrop: 'static'
            });
            $('div').removeClass('modal-backdrop in');
            return false;
        }

        function CalculateTotalCost(ele) {
            var msg = true;
            var AvailQty = parseInt($(ele).closest('td').find('[type="hidden"]').val());
            var permission = parseInt($(ele).closest('td').find('.allow').text());

            if (parseInt($(ele).val()) > AvailQty) {
                ErrorMessage('Error', 'Entered Qty Should Not Greater Than Avail Qty');
                $(ele).val(AvailQty);
                msg = false;
            }

            if (permission == "0") {
                if (parseInt($(ele).val()) < AvailQty) {
                    ErrorMessage('Error', 'Entered Qty Should Not Less Than Indent Qty If You Enter Less Than Contact Administartor');
                    $(ele).val(AvailQty);
                    msg = false;
                }
            }

            if (msg) {
                var PoType = $('#ContentPlaceHolder1_rbPoTypeChanged').find('[type="radio"]:checked').val();
                if ($(ele).closest('tr').find('.qty').val() == "")
                    $(ele).closest('tr').find('.qty').val(0);

                if ($(ele).closest('tr').find('.unitcost').val() == "")
                    $(ele).closest('tr').find('.unitcost').val(0);

                var Qty = parseFloat($(ele).closest('tr').find('.qty').val());
                var UnitCost = parseFloat($(ele).closest('tr').find('.unitcost').val());

                if (PoType == "WPO") {
                    $(ele).closest('tr').find('.totalcost').text(parseFloat(Qty * UnitCost).toFixed(2));
                }
                else {
                    var weight = parseFloat($(ele).closest('tr').find('.unitweight').val());
                    $(ele).closest('tr').find('.totalcost').text(parseFloat(Qty * weight * UnitCost).toFixed(2));
                }

                var PartQty = $(event.target).closest('tr').find('.qty').text();
                var UnitCost = $(event.target).val();
                $(event.target).closest('tr').find('.totalcost').val(parseFloat(UnitCost * PartQty));
            }
        }

        function MandatoryField(ele) {
            if ($(ele).is(":checked")) {
                $(ele).closest('tr').find('[type="text"]').addClass("mandatoryfield");
                $(ele).closest('tr').find('[type="text"]').before('<span class="textboxmandatory" style="color: red;">*</span>');
            }
            else {
                $(ele).closest('tr').find('[type="text"]').removeClass("mandatoryfield");
                $(ele).closest('tr').find('[type="text"]').closest('td').find('.textboxmandatory').remove()
            }
        }


        function ValidatePOItemDetails() {
            var msg = Mandatorycheck('ContentPlaceHolder1_divOutputsItems');
            if (msg) {
                if ($('#ContentPlaceHolder1_gvWorkOrderIndent').find('[type="checkbox"]').not('#ContentPlaceHolder1_gvWorkOrderIndent_chkall').is(':checked')) {
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

        function ShowPoPrintPopUp() {
            $('#mpePoDetails_p').modal({
                backdrop: 'static'
            });
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

        function ValidateSharePO() {
            var po = parseInt($('#ContentPlaceHolder1_gvWorkOrderPO').find('[type="checkbox"]:checked').not('#ContentPlaceHolder1_gvWorkOrderPO_chkall').length);
            if (po > 0) {
                SharePOApproval();
                return false;
            }
            else {
                ErrorMessage('Error', 'No Po Selected');
                return false;
            }
        }

        function ValidateRequiredWeight(ele) {
            var availWeight = parseFloat($(ele).closest('td').find('[type="hidden"]').val());
            var qty = parseFloat($(ele).val());

            if (qty > availWeight) {
                ErrorMessage('Error', 'Entered Qty Should Not Greater Avail Qty');
                $(ele).val(availWeight);
                return false;
            }
        }

        function SharePOApproval() {
            swal({
                title: "Are you sure?",
                text: "If Yes, PO Shared",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Share it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack("SharePOApproval", "");
            });
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

        function ShowTaxPopUp() {
            $('#mpeAddtax').modal('show');
            $('div').removeClass('modal-backdrop in');
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

        function CancelConfirmWPOID(WPOID) {
            var s = $('#ContentPlaceHolder1_txtamdreason').val();
            if (s != '') {
                swal({
                    title: "Are you sure?",
                    text: "If Yes, the PO will be Amendmend ",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "Yes, Amendment it!",
                    closeOnConfirm: false
                }, function () {
                    //showLoader();
                    __doPostBack('WPOCancel', WPOID);
                });
                return false;
            }
            else {
                ErrorMessage('Error', 'Please Enter Amd Reason Below of the page');
                return false;
            }
        }

        function PrintWorkOrderPO(Approvedtime, POStatus, ApprovedBy) {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var Address = $('#ContentPlaceHolder1_hdnAddress').val();
            var PhoneAndFaxNo = $('#ContentPlaceHolder1_hdnPhoneAndFaxNo').val();
            var Email = $('#ContentPlaceHolder1_hdnEmail').val();
            var WebSite = $('#ContentPlaceHolder1_hdnWebsite').val();

            var WPOContent = $('#ContentPlaceHolder1_divWorkOrderPoPrint_p').html();

            var MainContent = $('#divmaincontent_p').html();
            var SpecificationTax = $('#divspecificationandtax').html();

            var RowLen = $('#ContentPlaceHolder1_gvWorkOrderPOItemDetails_p').find('tr').length;

            winprint.document.write("<html><head><title>");
            winprint.document.write("Offer");
            winprint.document.write("</title>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='../Assets/css/ep-style.css'/>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='../Assets/css/main.css' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet'  href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/printg.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            //  winprint.document.write("<link rel='stylesheet' href='" + topstrip + "' type='text/css'/>");
            //  winprint.document.write("<style type='text/css'> @media print{ page-break-after: avoid; }  </style>");
            winprint.document.write("<style type='text/css'>  .page_generateoffer { margin:5mm; } .lbltaxothercharges { margin-left:15px; } .spntaxothercharges { margin-right:6px; } </style>");

            winprint.document.write("<div class='print-page' style='position:fixed;border:2px solid #000;width:200mm;height:287mm;margin-top:5mm;margin-bottom:5mm;margin-left:5mm;margin-right:5mm'></div>");
            winprint.document.write("<table style='width:200mm;height:287mm;margin-left:20px;border:0px !important'><thead><tr><td>");
            winprint.document.write("<div class='col-sm-12 print-generateoffer' style='border-bottom:1px solid #000;text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;height:110px'>");
            winprint.document.write("<div>");
            winprint.document.write("<div>");
            winprint.document.write("<div class='col-sm-12 padding0' id='divLSERPLogo' style='margin:20px auto;display:block;padding:5px 0px;'>");
            winprint.document.write("<div class='row' style='position:fixed;width:200mm;left:5mm;'>");
            winprint.document.write("<div class='col-sm-3'>");
            winprint.document.write("<img src=" + $('#ContentPlaceHolder1_hdnLonestarLogo').val() + " alt='lonestar-image' width='90px'>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-6 text-center'>");
            //winprint.document.write("<h3 style='font-weight:600;font-size:24px;font-style:italic;color:#000;font-family: Arial;display: contents;'>LONESTAR</h3>");
            //winprint.document.write("<span style='font-weight:600;font-size:24px ! important;font-family: Times New Roman;padding-left:5px ! important;'>INDUSTRIES</span>");

            winprint.document.write("<h3 style='font-weight:600;font-size:20px;color:#000;font-family: Times New Roman;display: contents;'>" + $('#ContentPlaceHolder1_hdnCompanyName').val() + "</h3>");
            winprint.document.write("<span style='font-weight:600;font-size:11px ! important;font-family: Times New Roman;'>" + $('#ContentPlaceHolder1_hdnFormalCompanyname').val() + "</span>");

            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;width: 100%;padding:0 0px'>" + Address + "</p>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + PhoneAndFaxNo + "</p>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + Email + "</p>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + WebSite + "</p>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-3'>");
            winprint.document.write("<img src=" + $('#ContentPlaceHolder1_hdnISOLogo').val() + " alt='lonestar-image' height='90px'>");
            winprint.document.write("</div></div>");
            winprint.document.write("</div></div></div>");
            winprint.document.write("</td></tr></thead>");
            winprint.document.write("<tbody><tr><td>");

            //if (parseInt(RowLen) <= 10) {
            winprint.document.write("<div class='page_generateoffer'>");
            winprint.document.write(WPOContent);
            winprint.document.write("</div>");
            //}
            //else {
            //    winprint.document.write("<div class='page_generateoffer'>");
            //    winprint.document.write(MainContent);

            //    winprint.document.write("<div class='p-t-10'>");
            //    winprint.document.write("<table class='table table-hover medium' cellspacing='0' rules='all' border='1' id='ContentPlaceHolder1_gvWorkOrderPOItemDetails_p' style='border-collapse:collapse;'>");
            //    winprint.document.write("<tbody>");

            //    for (var j = 0; j < 10; j++) {
            //        winprint.document.write("<tr align='center'>" + $('#ContentPlaceHolder1_gvWorkOrderPOItemDetails_p').find('tr')[j].innerHTML + "</tr>");
            //    }

            //    //  k = k + 15;
            //    winprint.document.write("</tbody>");
            //    winprint.document.write("</table>");
            //    winprint.document.write("</div>");

            //    winprint.document.write("</div>");

            //    winprint.document.write("<div class='page_generateoffer'>");
            //    winprint.document.write(SpecificationTax);
            //    winprint.document.write("</div>");
            //}

            winprint.document.write("</td></tr></tbody>");
            winprint.document.write("<tfoot><tr><td>");
            winprint.document.write("<div class='row' style='border-top:1px solid #000;padding-top:10px;height:30mm;'>");
            winprint.document.write("<div class='text-center' style='color:#000;padding-left:50px;font-weight:bold;'>KINDLY RETURN THE DUPLICATE COPY OF THE ORDER DULY SIGNED AS A TOKEN OFACCEPTANCE</div>");
            //style='height:100px;display:flex;align-items:flex-end;justify-content:flex-start'
            winprint.document.write("<div class='col-sm-4 p-t-85'>");
            winprint.document.write("<label style='padding-left:15px;font-weight:bold;'>PREPARED BY</label>");
            winprint.document.write("</div>");

            winprint.document.write("<div class='col-sm-4 p-t-85'>");
            winprint.document.write("<label style='padding-left:15px;font-weight:bold;'> CHECKED BY</label>");
            winprint.document.write("</div>");

            winprint.document.write("<div class='col-sm-4 text-center'>");
            winprint.document.write("<label style='display:block;width:100%;font-weight:bold;padding-top:5px;padding-left:30px;'>" + $('#ContentPlaceHolder1_hdnCompanyNameFooter').val() + "</label>");
            if (POStatus == "1") {
                if (ApprovedBy != '') {
                    winprint.document.write("<div id='divdigitalsign' class='p-t-10' style='display: block;'>");
                    winprint.document.write("<div style='text-align: end;' class='p-r-10'><label style='text-align: end;font-weight:bold;'> Digitally Signed By </label></div>");
                    winprint.document.write("<div style='text-align: end;' class='p-r-10'><label style='font-weight:bold;'> " + ApprovedBy + " </label></div>");
                    winprint.document.write("<div style='text-align: end;' class='p-r-10'><label>" + Approvedtime + "</label></div></div>");
                }
            }
            if (ApprovedBy != '')
                winprint.document.write("<label style='display:block;width:100%;padding-top:10px;font-weight:bold;padding-left:84px;'>AUTHORISED SIGNATORY</label>");
            else
                winprint.document.write("<label style='display:block;width:100%;padding-top:50px;font-weight:bold;'>AUTHORISED SIGNATORY</label>");

            winprint.document.write("</div>");
            winprint.document.write("</div>");
            winprint.document.write("</td></tr></tfoot></table>");
            winprint.document.write("</body></html>");

            //winprint.document.write("<div class='col-sm-12' style='height:30mm;'>");
            //winprint.document.write("<div>");
            //winprint.document.write("<div class='col-sm-12 row' style='margin-bottom:20px;border-top:1px solid #000;position:fixed;width:200mm;bottom:0px'>");
            //winprint.document.write(FooterContent);
            //winprint.document.write("</div>");
            //winprint.document.write("</div></div>");

            setTimeout(function () {
                winprint.document.close();
                winprint.focus();
                winprint.print();
                // winprint.close();
            }, 1000);
        }

        function ViewWODrawingFile(index) {
            __doPostBack('ViewWODrawingFile', index);
        }

        function ViewWODrawingFileAFO(index) {
            __doPostBack('ViewWODrawingFileAFO', index);
        }

        function ViewWODrawingFilePen(index) {
            __doPostBack('ViewWODrawingFilePending', index);
        }

        function Orderingfalse() {
            $('#ContentPlaceHolder1_gvWorkOrderPO').DataTable({
                "bSort": false,
                "order": [],
                "pageLength": 50
            });
        }

        function ViewIndentAttach(index) {
            __doPostBack('ViewIndentAttach', index);
            return false;
        }

        function opennewtab(Filepath) {
            var FileName = Filepath.split('$');
            for (var i = 0; i < FileName.length; i++)
                window.open('http://183.82.33.21/LSERPDocs/WorkOrderIndent/' + FileName[i], '_blank');
        }

    </script>
    <style type="text/css">
        .radio label {
            color: #000;
            font-weight: bold;
        }

        .overflow {
            max-height: 67vh;
            overflow: auto;
        }

            .overflow table thead tr {
                position: sticky;
                top: 0;
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
                                    <h3 class="page-title-head d-inline-block">Workorder PO Details</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Purchase</a></li>
                                <li class="active breadcrumb-item" aria-current="page">WPO</li>
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
                    <asp:PostBackTrigger ControlID="gvWorkOrderPO" />
                    <asp:PostBackTrigger ControlID="btnSave" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" class="btnAddNew" runat="server">
                            <div class="text-center">

                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right p-t-10">
                                        <label class="form-label">
                                            Select PO Type</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:RadioButtonList ID="rbPoTypeChanged" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rbPoTypeChanged_OnSelectIndexChanged"
                                            CssClass="radio radio-success" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Work Order PO" Value="WPO"></asp:ListItem>
                                            <asp:ListItem Text="Sub Work order PO" Value="SWPO"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Suplier Chain Vendor</label>
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
                                                Location Name</label>
                                        </div>
                                        <div>
                                            <asp:DropDownList ID="ddlLocationName" runat="server" ToolTip="Select Location Name"
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
                                        <%-- <div class="text-left">
                                            <label class="form-label">
                                                Handling Charges</label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txthandlingcharges" runat="server" CssClass="form-control" Onkeypress="return fnAllowNumeric();"
                                                AutoComplete="off" ToolTip="Enter Handling Charges" placeholder="Enter Handling Charges">
                                            </asp:TextBox>
                                        </div>--%>
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Payment Duration
                                            </label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtPayment" runat="server" CssClass="form-control" AutoComplete="off"
                                                ToolTip="Enter Payment Duration" placeholder="Enter Payment Duration">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label">
                                                Note</label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtNote" runat="server" CssClass="form-control" AutoComplete="off"
                                                ToolTip="Enter Notes" placeholder="Enter Notes">
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
                                                Enclosure</label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtEnclosure" runat="server" CssClass="form-control" AutoComplete="off"
                                                ToolTip="Enter Enclosure" placeholder="Enter Enclosure">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label">
                                                Remarks</label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtworemarks" runat="server" CssClass="form-control" AutoComplete="off"
                                                ToolTip="Enter Remarks" placeholder="Enter Remarks">
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
                                                Terms</label>
                                        </div>
                                        <div class="text-left">
                                            <asp:FileUpload ID="fAttachment" runat="server" TabIndex="12" CssClass="form-control"
                                                onchange="DocValidation(this);" Width="95%"></asp:FileUpload>
                                            <asp:Label ID="lbltermsupload" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
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
                                                    <asp:Label ID="lbltitle" runat="server"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="overflow-x:auto;">
                                            <asp:GridView ID="gvWorkOrderPO" runat="server" AutoGenerateColumns="False"
                                                OnRowCommand="gvWorkOrderPO_OnRowCommand"
                                                OnRowDataBound="gvWorkOrderPO_OnRowDataBound" ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found"
                                                CssClass="table table-hover table-bordered medium orderingfalse" DataKeyNames="WPOID,POStatus,ItemsAdded,DrawingFile">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkall" runat="server" onclick="return checkAllItems(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkitems" runat="server" onclick="return MandatoryField(this);"
                                                                AutoPostBack="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="WPO Number" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSPONumber" runat="server" Text='<%# Eval("Wonumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="WPO Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblWPOStatus" runat="server" Text='<%# Eval("POStatus")%>'></asp:Label>
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
                                                    <asp:TemplateField HeaderText="Note">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNote" runat="server" Text='<%# Eval("Note")%>'></asp:Label>
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
                                                                CommandName="PDF">  <img src="../Assets/images/pdf.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                        HeaderText="Add WPO">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnAdd" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="AddWPO"><img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Update AMD" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnupdateamd" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="updateAMD"><img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                        HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="EditWP"><img src="../Assets/images/edit-ec.png"/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center"
                                                        HeaderText="AMD">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnAMD" runat="server" Visible="false"
                                                                Style="min-width: 70px;" Text="AMD" CssClass="btn btn-cons btn-success"
                                                                CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                OnClientClick='<%# string.Format("return CancelConfirmWPOID({0});",Eval("WPOID")) %>'>                                                               
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View WO Drawing">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnViewIndentAttach" runat="server"
                                                                Text="View Attach"
                                                                OnClientClick='<%# string.Format("return ViewIndentAttach({0});",((GridViewRow) Container).RowIndex) %>'> <img src="../Assets/images/view.png" alt=""/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                            <asp:HiddenField ID="hdnWPOID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnAddress" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnPhoneAndFaxNo" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnEmail" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnWebsite" runat="server" Value="" />

                                            <asp:HiddenField ID="hdnCompanyName" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnFormalCompanyname" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnCompanyNameFooter" runat="server" Value="" />

                                            <asp:HiddenField ID="hdnLonestarLogo" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnISOLogo" runat="server" Value="" />

                                        </div>

                                        <div class="col-sm-12 p-t-20 text-center">
                                            <label>Amendment Reason </label>
                                            <asp:TextBox ID="txtamdreason" Rows="3" TextMode="MultiLine" runat="server" CssClass="form-control">  </asp:TextBox>
                                        </div>

                                        <div class="col-sm-12 p-t-20 text-center">
                                            <asp:Button ID="btnSharePO" Text="Send For Approval" CssClass="btn btn-cons btn-success" OnClientClick="return ValidateSharePO();"
                                                OnClick="btnSharePO_Click" runat="server" />
                                        </div>

                                        <div class="col-sm-12  overflow">
                                            <asp:GridView ID="gvPendingIndentListDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found"
                                                CssClass="table table-hover table-bordered orderingfalse medium" DataKeyNames="IndentNo,FileName">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Indent No" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIndentNo" runat="server" Text='<%# Eval("IndentNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Indent By" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIndentBy" runat="server" Text='<%# Eval("IndentBy")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Indent Raised On" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="IndentDate" runat="server" Text='<%# Eval("IndentDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="RFP No" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRFPNo" runat="server" Text='<%# Eval("RFPNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Part Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartName" runat="server" Text='<%# Eval("PartName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Job  QTY" ItemStyle-HorizontalAlign="left"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblActualQty" runat="server" Text='<%# Eval("ActualQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Raw Material QTY" ItemStyle-HorizontalAlign="left"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRawMatQty" runat="server" Text='<%# Eval("RawMaterialQuantity")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Job Description" ItemStyle-HorizontalAlign="left"
                                                        HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJobDescription" runat="server" CssClass="" Text='<%# Eval("JobDescription")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemarks" runat="server" CssClass="" Text='<%# Eval("Remarks")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Job Operation" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJobOperation" runat="server" Text='<%# Eval("JobOperationName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="WO Drawing File">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnWOView" runat="server"
                                                                OnClientClick='<%# string.Format("return ViewWODrawingFilePen({0});",((GridViewRow) Container).RowIndex) %>'><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="modal" id="mpeWorkOrderIndentDetails" style="overflow-y: scroll;">
                            <div style="max-width: 100%; height: fit-content;">
                                <div class="modal-content">
                                    <asp:UpdatePanel ID="upView" runat="server">
                                        <Triggers>
                                        </Triggers>
                                        <ContentTemplate>
                                            <div class="modal-header">
                                                <h4 class="modal-title ADD">
                                                    <asp:Label ID="lblWPONo_H" runat="server"></asp:Label></h4>
                                                <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                                    ×</button>
                                            </div>
                                            <div class="modal-body" style="padding: 0px;">
                                                <div id="docdiv" class="docdiv">
                                                    <div class="inner-container">
                                                        <div id="Item" runat="server">
                                                            <div id="divAddItems" class="divInput" runat="server">
                                                                <div class="ip-div text-center">
                                                                </div>
                                                            </div>
                                                            <div id="divOutputsItems" runat="server">
                                                                <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                                    <asp:GridView ID="gvWorkOrderIndent" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                                        EmptyDataText="No Records Found"
                                                                        CssClass="table table-hover table-bordered medium tablestatesave"
                                                                        OnRowCommand="gvWorkOrderIndent_OnRowCommand" OnRowDataBound="gvWorkOrderIndent_OnRowDataBound"
                                                                        DataKeyNames="WOIHID,MPID,FileName,UnitRequiredWeight,IndentNo,APPO,AvailableQty">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="">
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="chkall" runat="server" onclick="return checkAllItems(this);" />
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:CheckBox ID="chkitems" runat="server" onclick="return MandatoryField(this);"
                                                                                        AutoPostBack="false" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                                                HeaderStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Indent No" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblIndentNo" runat="server" Text='<%# Eval("IndentNo")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Indent Date" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="IndentDate" runat="server" Text='<%# Eval("IndentDate")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="RFP No" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRFPNo" runat="server" Text='<%# Eval("RFPNo")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Item Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Part Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPartName" runat="server" Text='<%# Eval("PartName")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Job  QTY" ItemStyle-HorizontalAlign="left"
                                                                                HeaderStyle-HorizontalAlign="left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblActualQty" runat="server" Text='<%# Eval("ActualQty")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Job Available QTY" ItemStyle-HorizontalAlign="left"
                                                                                HeaderStyle-HorizontalAlign="left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAvailableQty" runat="server" Text='<%# Eval("AvailableQty")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Raw Material QTY" ItemStyle-HorizontalAlign="left"
                                                                                HeaderStyle-HorizontalAlign="left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRawMatQty" runat="server" Text='<%# Eval("RawMaterialQuantity")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Unit Weight">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtActualWeight" runat="server"
                                                                                        CssClass="unitweight form-control" onkeyup="return ValidateRequiredWeight(this);" Text='<%# Eval("UnitRequiredWeight")%>'></asp:TextBox>
                                                                                    <asp:HiddenField ID="hdnUnitRequiredWeight" runat="server" Value='<%# Eval("UnitRequiredWeight")%>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="6%">
                                                                                <ItemTemplate>
                                                                                    <asp:HiddenField ID="hdnAvailQty" runat="server" Value='<%# Eval("AvailableQty")%>' />
                                                                                    <asp:Label ID="lblAllowPer" Style="display: none;" CssClass="allow" runat="server" Text='<%# Eval("APPO")%>' />
                                                                                    <asp:TextBox ID="txtPoQty" runat="server"
                                                                                        onkeyup="return CalculateTotalCost(this);" Onkeypress="return fnAllowNumeric();"
                                                                                        CssClass="form-control qty"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Unit Cost" HeaderStyle-Width="6%">
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtUnitCost" runat="server" Onkeypress="return validationDecimal(this);"
                                                                                        onkeyup="return CalculateTotalCost(this);" CssClass="form-control unitcost"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--<asp:TemplateField HeaderText="Total Cost">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbltotalcost" runat="server" Text='<%# Eval("")%>'
                                                                                        CssClass="totalcost"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>
                                                                            <%--<asp:TemplateField HeaderText="PO Qty">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPoQty" runat="server" Text='<%# Eval("POQty")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>

                                                                            <asp:TemplateField HeaderText="Job Description" ItemStyle-HorizontalAlign="left"
                                                                                HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblJobDescription" runat="server" CssClass="" Text='<%# Eval("JobDescription")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                                                HeaderStyle-HorizontalAlign="left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRemarks" runat="server" CssClass="" Text='<%# Eval("Remarks")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Job Operation" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblJobOperation" runat="server" Text='<%# Eval("JobOperationName")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="WO Drawing File">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="btnWOView" runat="server"
                                                                                        OnClientClick='<%# string.Format("return ViewWODrawingFile({0});",((GridViewRow) Container).RowIndex) %>'><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Indent Date">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblIndentDate" runat="server" Text='<%# Eval("IndentDate")%>'></asp:Label>
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

                                                                            <%--   <asp:TemplateField HeaderText="View Indent Layout" ItemStyle-HorizontalAlign="left"
                                                                                HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="btnIndentLayout" runat="server" CommandName="ViewIndentLayOut"
                                                                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ToolTip="View">                                                                  
                                                                               <img src="../Assets/images/view.png" style="height:20px" alt="" /></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>--%>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                                <div class="col-sm-12 p-t-20 text-center">
                                                                    <asp:Button ID="btnWPOD" Text="Save" CssClass="btn btn-cons btn-success" OnClientClick="return ValidatePOItemDetails();"
                                                                        OnClick="btnWorkOrderPODetails_Click" runat="server" />
                                                                </div>
                                                                <div class="col-sm-12 p-t-20 text-center">
                                                                    <asp:GridView ID="gvWorkOrderPODetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                                        EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                                        OnRowDataBound="gvWorkOrderPODetails_OnRowDataBound" OnRowCommand="gvWorkOrderPODetails_OnRowCommand"
                                                                        DataKeyNames="WPODID,IndentNo,FileName">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                                                HeaderStyle-HorizontalAlign="Center">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Indent No" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblIndentNo" runat="server" Text='<%# Eval("IndentNo")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="RFP No" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRFPNo" runat="server" Text='<%# Eval("RFPNo")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Item Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Part Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPartName" runat="server" Text='<%# Eval("PartName")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="PO Qty" HeaderStyle-Width="6%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPOQty" runat="server"
                                                                                        Text='<%# Eval("POQty")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Total Cost" HeaderStyle-Width="6%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTotalCost" runat="server"
                                                                                        Text='<%# Eval("TotalCost")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="WO Drawing File" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="btnWoDrawingFile" runat="server"
                                                                                        OnClientClick='<%# string.Format("return ViewWODrawingFileAFO({0});",((GridViewRow) Container).RowIndex) %>'><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                                                HeaderText="Delete">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="btndelete" runat="server" CommandName="DeletePOItem"
                                                                                        CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                                                        <img src="../Assets/images/del-ec.png"/></asp:LinkButton>
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
                                                </div>
                                            </div>
                                            <asp:HiddenField ID="hdnSPODID" runat="server" Value="0" />
                                            <div class="modal-footer">
                                                <div class="col-sm-12 p-t-10">
                                                    <div class="col-sm-10">
                                                    </div>
                                                    <div class="col-sm-2">
                                                        <div class="form-group">
                                                            <iframe runat="server" id="ifrm" style="display: none;"></iframe>
                                                        </div>
                                                    </div>
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

    <div class="modal" id="mpePoDetails_p">
        <div class="modal-dialog" style="height: 100%;">
            <div class="modal-content" style="width: fit-content;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always" Style="height: 100%;">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnDownLoad" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H1">
                                <div class="text-center">
                                    Documents
                                    <asp:Label ID="Label2" Text="" runat="server"></asp:Label>
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal">
                                x</button>
                        </div>
                        <div class="modal-body" style="height: 100%;">
                            <div class="inner-container" style="height: 100%;">
                                <div class="ip-div text-center" style="margin-bottom: 10px;">
                                    <asp:LinkButton Text="Download" ID="btnDownLoad" OnClick="btndownloaddocs_Click"
                                        runat="server">   <img src="../Assets/images/pdf.png" /></asp:LinkButton>
                                </div>
                                <div id="divWorkOrderPoPrint_p" runat="server" style="padding: 10px; width: 200mm;">
                                    <div id="divmaincontent_p">
                                        <div class="text-center">
                                            <label style="font-size: 20px !important; text-decoration: underline; font-weight: 700; margin-bottom: 10px;">
                                                Work Order</label>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <label style="display: block; font-weight: 700">
                                                    To</label>
                                                <asp:Label ID="lblSupplierChainVendorName_p" runat="server" Style="margin-top: 10px; margin-left: 20px"></asp:Label>
                                                <asp:Label ID="lblReceiverAddress_p" runat="server" Style="margin-top: 10px; margin-left: 20px"></asp:Label>
                                                <div class="p-t-10">
                                                    <asp:Label ID="lblGSTNumber_p" Style="margin-left: 20px;" CssClass="p-l-20" runat="server"></asp:Label>
                                                </div>
                                                <div class="p-t-5" style="margin-right: 10px; margin-top: 5px; padding: 5px; border: 1px solid;">
                                                    <asp:Label ID="lblRange_p" Style="font-weight: bold;" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">

                                                <div id="divGSTREV0" runat="server" visible="false">
                                                    <div style="display: inline-block; border: 1px solid #000; padding: 10px">
                                                        <label style="font-size: 20px; font-weight: 700; text-decoration: underline;">
                                                            Consignee Address
                                                        </label>
                                                        <asp:Label ID="lblConsigneeAddress_p" Style="padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                        <asp:Label ID="lblTNGSTNo" Style="display: block; padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                        <asp:Label ID="lblCSTNo" Style="display: block; padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                        <asp:Label ID="lblECCNo" Style="display: block; padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                        <asp:Label ID="lblTINNo" Style="display: block; padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                        <asp:Label ID="lblGSTNo" Style="padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div id="divGSTREV1" runat="server" visible="false">
                                                    <div style="display: inline-block; border: 1px solid #000; padding: 10px">
                                                        <label style="font-size: 20px; font-weight: 700; text-decoration: underline;">
                                                            Consignee Address
                                                        </label>
                                                        <asp:Label ID="lblConsigneeAddressRev1_p" Style="padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                        <asp:Label ID="lblCINNo_P" Style="display: block; padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                        <asp:Label ID="lblPANNo_p" Style="display: block; padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                        <asp:Label ID="lblTANNo_p" Style="display: block; padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                        <asp:Label ID="lblGSTIN_p" Style="display: block; padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6">
                                            </div>
                                            <div class="col-sm-6">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6">
                                            </div>
                                            <div class="col-sm-6">
                                                <label style="font-weight: bold; display: inline-block;">
                                                    WO No</label>
                                                <asp:Label ID="lblWoNo_p" runat="server" Style="font-weight: bold"></asp:Label>

                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6">
                                            </div>
                                            <div class="col-sm-6">
                                                <label style="font-weight: bold; display: inline-block;">
                                                    WO Date</label>
                                                <asp:Label ID="lblWODate_p" runat="server" Style="font-weight: bold"></asp:Label>
                                            </div>
                                        </div>
                                        <%--   <div class="row m-t-10">
                                        <div class="col-sm-6">
                                            &nbsp;
                                        </div>
                                        <div class="col-sm-6" style="font-weight: bold">
                                            <div class="m-t-10">
                                                <div class="col-sm-5">
                                                    <label style="font-weight: bold">
                                                        WO No</label>
                                                </div>
                                                <div class="col-sm-2">
                                                    <label style="font-weight: bold">
                                                        :
                                                    </label>
                                                </div>
                                                <div class="col-sm-5">
                                                    <asp:Label ID="lblWoNo_p" runat="server" Style="font-weight: bold"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="m-t-10">
                                                <div class="col-sm-5">
                                                    <label style="font-weight: bold">
                                                        WO Date</label>
                                                </div>
                                                <div class="col-sm-2" style="font-weight: bold">
                                                    :
                                                </div>
                                                <div class="col-sm-5">
                                                    <asp:Label ID="lblWODate_p" runat="server" Style="font-weight: bold"></asp:Label>
                                                </div>
                                            </div>
                                            <label>
                                        </div>
                                    </div>--%>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <label style="font-weight: bold">
                                                    AMD Reason
                                                </label>
                                                <asp:Label ID="lblAMDReson_p" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-6">
                                                AMD Date </label>
                                            <asp:Label ID="lblAMDDate_p" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <label style="font-weight: bold">
                                                    RFP No</label>
                                                <asp:Label ID="lblRFPNo_p" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-6">
                                                QUOTE Ref No</label>
                                            <asp:Label ID="lblQuoteRefNo_p" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                        <div class="p-t-10" style="overflow-x: auto;">
                                            <asp:GridView ID="gvWorkOrderPOItemDetails_p" runat="server" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Indent No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIndentNo" runat="server" Text='<%# Eval("IndentNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartName" runat="server" Text='<%# Eval("MaterialName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="QTY" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartQuantity" runat="server" Text='<%# Eval("Quantity")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit Cost" HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUnitCost" runat="server" Text='<%# Eval("UnitCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Cost" HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalCost" runat="server" Text='<%# Eval("TotalCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div id="divspecificationandtax">
                                        <div class="row m-t-10">
                                            <div class="col-sm-6" style="font-weight: 700">
                                                <div class="m-t-10">
                                                    <label>
                                                        Rupees In Words</label>
                                                    <asp:Label ID="lblRupeesInwords_p" runat="server"></asp:Label>
                                                </div>
                                                <div class="m-t-10">
                                                    <label>
                                                        Delievery</label>
                                                    <asp:Label ID="lblDeliveryDate_p" runat="server"></asp:Label>
                                                </div>
                                                <div class="m-t-10">
                                                    <label>
                                                        Payment</label>
                                                    <asp:Label ID="lblPayment_p" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-6" style="border: 1px solid #000; padding-left: 0 !important; padding-right: 0 !important; margin-top: -10px;">
                                                <div id="divothercharges_p" class="divothercharges" runat="server">
                                                </div>
                                                <div class="m-t-10" style="width: 100%; float: left">
                                                    <label style="float: left; margin-left: 15px">
                                                        Sub Total INR</label>
                                                    <asp:Label ID="lblSubTotal_p" runat="server" Style="float: right; margin-right: 6px"></asp:Label>
                                                </div>

                                                <div id="divtax_p" class="divtax" runat="server">
                                                </div>
                                                <%--<div class="m-t-10 padd-lr-15" style="width: 100%; float: left">
                                                    <label style="float: left">
                                                        SGST @
                                                    </label>
                                                    <asp:Label ID="lblSGSTPercentage_p" runat="server" Style="float: right; margin-right: 10px"></asp:Label>
                                                </div>
                                                <div class="m-t-10 padd-lr-15" style="width: 100%; float: left">
                                                    <label style="float: left">
                                                        CGST @
                                                    </label>
                                                    <asp:Label ID="lblCGSTPercentage_p" runat="server" Style="float: right; margin-right: 10px"></asp:Label>
                                                </div>--%>
                                                <div class="m-t-10 padd-lr-15 p-t-10" style="width: 100%; float: left; border: 1px solid #000">
                                                    <label style="font-weight: bold;">
                                                        Total Amount</label>
                                                    <asp:Label ID="lblTotalAmount_p" runat="server" Style="float: right; font-weight: bold;"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-t-10" style="color: #000">
                                            <h6 style="text-decoration: underline; font-weight: 700">Specifictaion Enclosed</h6>
                                            <ol style="padding-left: 15px">
                                                <li>Test certificate must be submitted for approval prior to dispatch.</li>
                                                <li>Material should be Dispatched Strictly as per dispatch instruction</li>
                                                <li>Material received at store without proper documentation will not be accepted and
                                                    all waiting charges will be on supplier account</li>
                                                <li>Kindly return the Duplicate copy of the order duly signed as a token of acceptance.</li>
                                                <li>Please Mention our Workorder Number in Your Invoice.</li>
                                                <li>Offer terms will be as per our GTC.</li>
                                                <li>
                                                    <asp:Label ID="lblNote_p" runat="server"></asp:Label>
                                                </li>
                                            </ol>
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

    <div class="modal" id="mpeAddtax" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                                                DataKeyNames="WPOOCDID">
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
                                                                OnClientClick='<%# string.Format("return deleteConfirmOtherCharges({0});",Eval("WPOOCDID")) %>'>   <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
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
                                                DataKeyNames="WPOTDID">
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
                                                                OnClientClick='<%# string.Format("return deleteConfirmTaxDetailsCharges({0});",Eval("WPOTDID")) %>'>   <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
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


    <div class="modal" id="mpeWorkOrderPOAmendments" style="overflow-y: scroll;">
        <div style="max-width: 100%; height: fit-content;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">
                                <asp:Label ID="Label1" runat="server"></asp:Label></h4>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="divamd" class="docdiv">
                                <div class="inner-container">
                                    <div id="Div1" runat="server">
                                        <div id="div2" class="divInput" runat="server">
                                            <div class="ip-div text-center">
                                            </div>
                                        </div>
                                        <div id="div3" runat="server">
                                            <div class="col-sm-12 p-t-20 text-center">
                                                <asp:GridView ID="gvWorkOrderPOAmendsDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                    OnRowCommand="gvWorkOrderPODetails_OnRowCommand"
                                                    OnRowEditing="gvWorkOrderPOAmendsDetails_RowEditing"
                                                    OnRowCancelingEdit="gvWorkOrderPOAmendsDetails_RowCancelingEdit"
                                                    OnRowUpdating="gvWorkOrderPOAmendsDetails_RowUpdating"
                                                    DataKeyNames="WPODID,IndentNo,FileName">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Indent No" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblIndentNo" runat="server" Text='<%# Eval("IndentNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="RFP No" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRFPNo" runat="server" Text='<%# Eval("RFPNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="PO Qty" HeaderStyle-Width="6%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPOQty" runat="server"
                                                                    Text='<%# Eval("POQty")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Unit Cost">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblunitcost" runat="server"
                                                                    Text='<%# Eval("UnitCost")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtUnitCost" CssClass="form-control" Text='<%# Eval("UnitCost")%>' runat="server"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="WO Drawing File" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="btnWoDrawingFile" runat="server"
                                                                    OnClientClick='<%# string.Format("return ViewWODrawingFileAFO({0});",((GridViewRow) Container).RowIndex) %>'><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField ButtonType="Image" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"
                                                            ShowEditButton="true" EditText="<img src='~/images/icon_edit.png' title='Edit' />"
                                                            EditImageUrl="../Assets/images/edit-ec.png" CancelImageUrl="../Assets/images/icon_cancel.png"
                                                            UpdateImageUrl="../Assets/images/icon_update.png" ItemStyle-Wrap="false" ControlStyle-Width="20px"
                                                            ControlStyle-Height="20px" HeaderText="Edit" ValidationGroup="edit" HeaderStyle-Width="7%">
                                                            <ControlStyle CssClass="UsersGridViewButton" />
                                                        </asp:CommandField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                        <div class="modal-footer">
                            <div class="col-sm-12 p-t-10">
                                <div class="col-sm-10">
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
                                        <iframe runat="server" id="Iframe1" style="display: none;"></iframe>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

</asp:Content>
