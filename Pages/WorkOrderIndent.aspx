<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="WorkOrderIndent.aspx.cs" Inherits="Pages_WorkOrderIndent" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function ShowViewPopUp() {
            $('#mpeView').modal({
                backdrop: 'static'
            });
            return false; gvWorkOrderIndentDetails
        }

        function ShowBOIIssuePopup() {
            $('#mpeBOIMRNIssue').modal("show");
            $('#mpeWo').modal('hide');
            return false;
        }

        function SaveIndent() {
            var mancheck = Mandatorycheck('ContentPlaceHolder1_divInput');
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

        function ShowWoPopup() {
            $('#mpeWo').modal('show');
            return false;
        }

        function ValidateWO() {

            if ($('#ContentPlaceHolder1_hdnWOIHID').val() != '0') {
                $('[type="file"]').removeClass('mandatoryfield');
            }

            var msg = Mandatorycheck('ContentPlaceHolder1_divOutput');
            if (msg) {
                if ($('#ContentPlaceHolder1_gvPartDetails').find('[type="checkbox"]:checked').not('#ContentPlaceHolder1_gvPartDetails_chkall').length > 0) {
                    return true;
                }
                else {
                    ErrorMessage('Error', 'No Part Sno Selected')
                    hideLoader();
                    return false;
                }
            }
            else
                return false;
        }

        function multiselect(multivalue) {
            debugger;
            var test = multivalue.trim();
            var testArray = test.split(',');
            $('#ContentPlaceHolder1_LiJobOperationList').val(testArray);
        }

        function deleteWorkOrderIndent(WOIHID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Indent will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deleteIndent', WOIHID);
            });
            return false;
        }

        function ViewWODrawingFile(index) {
            __doPostBack('ViewWODrawingFile', index);
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

        function ValidateShareQC() {
            if ($('#ContentPlaceHolder1_gvWorkOrderIndentDetails').find('[type="checkbox"]:checked').not('#ContentPlaceHolder1_gvWorkOrderIndentDetails_chkall').length) {
                ShareQC();
                return false;
            }
            else {
                ErrorMessage('Error', 'No Indent Selected')
                hideLoader();
                return false;
            }
        }

        function ShareQC() {
            swal({
                title: "Are you sure?",
                text: "If Yes, the QC Share permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Share it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('ShareQC', '');
            });
            return false;
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

        function ValidateIssuedQuantity(ele) {
            var IssueWeight = parseFloat($(ele).val());
            var AvailWeight = parseFloat($(ele).closest('td').find('[type="hidden"]').val());

            if (IssueWeight > AvailWeight) {
                ErrorMessage('Error', 'Issued Weight Can Not Greater Then Avail Weight "' + $(ele).val() + '"');
                $(ele).val(AvailWeight);
            }
        }

        function ValidateIssueMRN(ele) {
            var msg = Mandatorycheck('ContentPlaceHolder1_divAJMRN');
            var sum = 0;

            var TotalPartQty = parseFloat($('#ContentPlaceHolder1_hdnTotalPartQty').val());
            var TotalPoQty = parseFloat($('#ContentPlaceHolder1_hdnPOQty').val());
            var TotalBlockedQty = parseFloat($('#ContentPlaceHolder1_hdnTotalBlockedQty').val());

            var num = parseFloat(TotalBlockedQty / TotalPartQty);
            var ActualQty = parseFloat(num * TotalPoQty);
            if (msg) {
                $('#ContentPlaceHolder1_gvMRNIssueBOI_WorkOrder').find("[type=text]").each(function () {
                    sum = parseFloat(sum) + parseFloat($(this).val());
                });
                if (ActualQty == sum) {
                    return true;
                }
                else {
                    ErrorMessage('Error', 'Qty Should be Taken Into Given PO Qty');
                    hideLoader();
                    return false;
                }
            }
            else
                return false;
        }

        function WorkOrderIndentPrint(index) {
            __doPostBack("WorkOrderIndentPrint", index);
            return false;
        }

        function PrintWorkorderIndentDetails(QrCode) {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var Address = $('#ContentPlaceHolder1_hdnAddress').val();
            var PhoneAndFaxNo = $('#ContentPlaceHolder1_hdnPhoneAndFaxNo').val();
            var Email = $('#ContentPlaceHolder1_hdnEmail').val();
            var WebSite = $('#ContentPlaceHolder1_hdnWebsite').val();

            var div = $('#ContentPlaceHolder1_divWorkOrderIndent_PDF').html();

            winprint.document.write("<html><head><title>");
            winprint.document.write("Offer");
            winprint.document.write("</title>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='../Assets/css/ep-style.css'/>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='../Assets/css/main.css' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet'  href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/print.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");

            winprint.document.write("<style type='text/css'>  tbody td .col-sm-12 {  padding-top: 15px; } tbody td label {font-weight: bold;} </style>");

            winprint.document.write("<div class='print-page' style='position:fixed;border:1px solid #000;width:200mm;height:287mm;margin-top:5mm;margin-bottom:5mm;margin-left:5mm;margin-right:5mm'></div>");
            winprint.document.write("<table style='width:200mm;height:287mm;margin-left:20px;border:0px !important'><thead><tr><td>");
            winprint.document.write("<div class='col-sm-12' style='text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;height:102px'>");
            winprint.document.write("<div>");
            winprint.document.write("<div>");
            winprint.document.write("<div class='col-sm-12 padding0' id='divLSERPLogo' style='margin:20px auto;display:block;padding:5px 0px;'>");
            winprint.document.write("<div  style='position:fixed;width:200mm;left:5mm;padding-bottom: 5px;border-bottom:1px solid #000;'>");
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
            winprint.document.write("<div style='margin:5mm;'>");
            winprint.document.write(div);
            winprint.document.write("</div>");
            winprint.document.write("</td></tr></tbody>");
            winprint.document.write("<tfoot><tr><td>");

            winprint.document.write("<div style='text-align:center;'>");
            winprint.document.write("<img src='" + QrCode + "' class='Qrcode'>");
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

        function ViewDrawings(index) {
            __doPostBack("ViewDrawings", index);
            return false;
        }

        function ValidateIndentQty(ele) {
            var avilqty = $(ele).closest('td').find('[type="hidden"]').val();

            if (parseInt($(ele).val()) > parseInt(avilqty) || parseInt($(ele).val()) <= 0) {
                $(ele).val(avilqty);
                ErrorMessage('Error', 'Entered Qty Should Not Greater avail Qty');
            }
        }

        function checkAllPartMandatoryPart(ele) {
            debugger;
            if ($(ele).is(":checked")) {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', true);

                $(ele).closest('table').find('[type="text"]').addClass("mandatoryfield");
                $(ele).closest('table').find('[type="text"]').before('<span class="textboxmandatory" style="color: red;">*</span>');
            }
            else {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', false);
                $(ele).closest('table').find('[type="text"]').removeClass("mandatoryfield");
                $(ele).closest('table').find('[type="text"]').closest('td').find('.textboxmandatory').remove();
            }
        }

        function PartMandatoryField(ele) {
            if ($(ele).is(":checked")) {
                $(ele).closest('tr').find('[type="text"]').addClass("mandatoryfield");
                $(ele).closest('tr').find('[type="text"]').before('<span class="textboxmandatory" style="color: red;">*</span>');
            }
            else {
                $(ele).closest('tr').find('[type="text"]').removeClass("mandatoryfield");
                $(ele).closest('tr').find('[type="text"]').closest('td').find('.textboxmandatory').remove()
            }
        }

    </script>

    <style type="text/css">
        td .radio td label {
            color: #000 !important;
            font-weight: bold !important;
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
                                    <h3 class="page-title-head d-inline-block">Work Order Indent</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Production</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Work Order Indent</li>
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
                    <%-- <asp:PostBackTrigger ControlID="gvMPItemDetails" />--%>
                    <asp:PostBackTrigger ControlID="btnSaveWOI" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
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
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="col-sm-12 p-t-10" style="overflow-x:auto;">
                                            <%-- <asp:GridView ID="gvPartSno" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" OnRowDataBound="gvPartSno_OnRowDataBound" CssClass="table table-hover table-bordered medium" DataKeyNames="PRPDID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-Width="15%">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkall" runat="server" onclick="return checkAllItems(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkQC" runat="server" AutoPostBack="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Part Sno" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartSno" runat="server" Text='<%# Eval("PartSNO")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Sno" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemSno" runat="server" Text='<%# Eval("ItemSno")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>--%>

                                            <asp:GridView ID="gvPartDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" OnRowDataBound="gvPartDetails_OnRowDataBound"
                                                CssClass="table table-hover table-bordered medium orderingfalse" DataKeyNames="MPID,RFPDID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-Width="15%">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkall" runat="server" onclick="return checkAllItems(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkQC" runat="server" onclick="return PartMandatoryField(this);" AutoPostBack="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Part Name" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartname" runat="server" Text='<%# Eval("PartName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Grade" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGradename" runat="server" Text='<%# Eval("GradeName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="THK" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTHK" runat="server" Text='<%# Eval("THKValue")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Item Name" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemname" runat="server" Text='<%# Eval("Itemname")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--  <asp:TemplateField HeaderText="Indent Qty" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtIndentQty"
                                                                onkeyup="return ValidateIndentQty(this);" onkeypress="return validationNumeric(this);" runat="server"
                                                                Text='<%# Eval("AvailQty")%>' CssClass="form-control"></asp:TextBox>
                                                            <asp:HiddenField ID="hdnindentQty" runat="server" Value='<%# Eval("AvailQty")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                    <asp:TemplateField HeaderText="Indent QTY" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtIndentQty" Text='<%# Eval("TotalPartQty")%>'
                                                                onkeyup="return ValidateIndentQty(this);"
                                                                onkeypress="return validationNumeric(this);"
                                                                CssClass="form-control" runat="server"></asp:TextBox>
                                                            <asp:HiddenField ID="hdnindentQty" Value='<%# Eval("TotalPartQty")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Job Qty" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtJobCuttingQty"
                                                                onkeypress="return validationNumeric(this);" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Total Part Qty" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalPartQty" runat="server" Text='<%# Eval("TotalPartQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--     <asp:TemplateField HeaderText="Total indent Qty" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalIndentqty" runat="server" Text='<%# Eval("TotalIndentQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="WIP Qty" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblWIPQty" runat="server" Text='<%# Eval("WIPQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Inward Qty" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIndentQty" runat="server" Text='<%# Eval("InwardQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-2">
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="text-left">
                                                    <label class="form-label mandatorylbl">
                                                        Job Description
                                                    </label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtJobDescription" runat="server" placeholder="Enter Project Name"
                                                        CssClass="form-control mandatoryfield" MaxLength="100" TabIndex="3" autocomplete="nope"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="text-left">
                                                    <label class="form-label">
                                                        Remarks
                                                    </label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtremarks" runat="server" TabIndex="4" placeholder="Enter Remarks"
                                                        CssClass="form-control" autocomplete="nope"></asp:TextBox>
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
                                                        Raw Material Quantity
                                                    </label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtRawmaterialQuantity" runat="server" placeholder="Enter Project Name"
                                                        CssClass="form-control mandatoryfield" onkeypress="return validationNumeric(this);" MaxLength="100" TabIndex="3" autocomplete="nope"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <%--   <div class="text-left">
                                                    <label class="form-label mandatorylbl">
                                                        Job Qty
                                                    </label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtJobQty" runat="server" placeholder="Enter Job Qty"
                                                        CssClass="form-control mandatoryfield" onkeypress="return validationNumeric(this);" MaxLength="100" TabIndex="3" autocomplete="nope"></asp:TextBox>
                                                </div>--%>
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
                                                        Job Operation List
                                                    </label>
                                                </div>
                                                <div>
                                                    <asp:ListBox ID="LiJobOperationList" runat="server" CssClass="form-control mandatoryfield"
                                                        SelectionMode="Multiple" ToolTip="Select Operation Name">
                                                        <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                    </asp:ListBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="text-left">
                                                    <label class="form-label mandatorylbl">
                                                        Work Order Drawing File
                                                    </label>
                                                </div>
                                                <div>
                                                    <asp:FileUpload ID="fAttachment" runat="server" TabIndex="12"
                                                        CssClass="form-control mandatoryfield"></asp:FileUpload>
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
                                                        Job Weight   
                                                    </label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtJobWeight" runat="server" placeholder="Enter Job Weight"
                                                        CssClass="form-control" onkeypress="return validationDecimal(this);"
                                                        autocomplete="nope"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="text-left">
                                                    <label class="form-label">
                                                        QC Plan
                                                    </label>
                                                </div>
                                                <div>
                                                    <asp:RadioButtonList ID="rblQCPlan" runat="server" RepeatLayout="Table"
                                                        CssClass="radio radio-success"
                                                        RepeatDirection="Horizontal">
                                                        <asp:ListItem Text="Required" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Not Required" Value="0" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="col-sm-2">
                                            </div>
                                        </div>

                                        <div class="col-sm-12 text-center p-t-10">
                                            <asp:LinkButton ID="btnSaveWOI" runat="server" Text="Save" OnClientClick="return ValidateWO();"
                                                OnClick="btnSaveWOI_Click" CssClass="btn btn-cons btn-save AlignTop"></asp:LinkButton>
                                            <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel"
                                                OnClick="btnCancel_Click" CssClass="btn btn-cons btn-save AlignTop"></asp:LinkButton>
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="overflow-x:auto;">
                                            <asp:GridView ID="gvWorkOrderIndentDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" OnRowCommand="gvWorkOrderIndentDetails_OnRowCommand" CssClass="table table-hover table-bordered medium"
                                                OnRowDataBound="gvWorkOrderIndentDetails_OnRowDataBound" DataKeyNames="WOIHID,WorkOrderDrawingFile,WOID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-Width="15%">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkall" runat="server" onclick="return checkAllItems(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkQC" runat="server" AutoPostBack="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Indent By" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIndentBy" runat="server" Text='<%# Eval("IndentBy")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Indent Date" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIndentDate" runat="server" Text='<%# Eval("IndentDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="WO ID" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblWONumber" runat="server" Text='<%# Eval("WOID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="QC Shared Status" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQCPlanningStatus" runat="server" Text='<%# Eval("QCPlanningStatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Part Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartName" runat="server" Text='<%# Eval("PartName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Quanity" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQuanity" runat="server" Text='<%# Eval("PartQuanity")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Job Description" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJobDescription" runat="server" Text='<%# Eval("JobDescription")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Raw Material Quantity" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrawmaterialquantity" runat="server" Text='<%# Eval("RawMaterialQuantity")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Job Operation" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJoboperation" runat="server" Text='<%# Eval("JobOperationName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:TemplateField HeaderText="Issue BOI MRN" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnIssueRM" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="issueRM"><img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                    <asp:TemplateField HeaderText="WO Drawing File" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnWOView" runat="server"
                                                                OnClientClick='<%# string.Format("return ViewWODrawingFile({0});",((GridViewRow) Container).RowIndex) %>'><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="EditWO">
                                                           <img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDelete" runat="server" OnClientClick='<%# string.Format("return deleteWorkOrderIndent({0});",Eval("WOIHID")) %>'>
                                                            <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Work Order Print" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnPDF" runat="server"
                                                                OnClientClick='<%# string.Format("return WorkOrderIndentPrint({0});",((GridViewRow) Container).RowIndex) %>'> 
                                                                <img src="../Assets/images/pdf.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="col-sm-12 text-center p-t-10">
                                            <asp:LinkButton ID="btnShareQC" runat="server" Text="Share QC" OnClientClick="return ShareQC();"
                                                CssClass="btn btn-cons btn-save AlignTop"></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <asp:HiddenField ID="hdnRFPHID" runat="server" />
                    <asp:HiddenField ID="hdnWOIHID" Value="0" runat="server" />
                    <asp:HiddenField ID="hdnAddress" runat="server" Value="" />
                    <asp:HiddenField ID="hdnPhoneAndFaxNo" runat="server" Value="" />
                    <asp:HiddenField ID="hdnEmail" runat="server" Value="" />
                    <asp:HiddenField ID="hdnWebsite" runat="server" Value="" />
                    <asp:HiddenField ID="hdnTotalPartQty" runat="server" Value="" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div id="divWorkOrderIndent_PDF" runat="server" style="display: none;">
        <div class="col-sm-12 text-center">
            <label>
                Work Order Indent</label>
        </div>
        <div class="col-sm-12">
            <div class="col-sm-6">
                <label>
                    RFP No</label>
            </div>
            <div class="col-sm-6">
                <asp:Label ID="lblRFPNo_P" runat="server"></asp:Label>
            </div>
        </div>
        <div class="col-sm-12">
            <div class="col-sm-6">
                <label>
                    Work Order ID</label>
            </div>
            <div class="col-sm-6">
                <asp:Label ID="lblWorkOrderID_P" runat="server"></asp:Label>
            </div>
        </div>
        <div class="col-sm-12">
            <div class="col-sm-6">
                <label>
                    Item Name
                </label>
            </div>
            <div class="col-sm-6">
                <asp:Label ID="lblItemName_p" runat="server"></asp:Label>
            </div>
        </div>
        <div class="col-sm-12">
            <div class="col-sm-6">
                <label>
                    Part Name
                </label>
            </div>
            <div class="col-sm-6">
                <asp:Label ID="lblPartName_p" runat="server"></asp:Label>
            </div>
        </div>
        <div class="col-sm-12">
            <div class="col-sm-6">
                <label>
                    Part QTY
                </label>
            </div>
            <div class="col-sm-6">
                <asp:Label ID="lblPartQty_p" runat="server"></asp:Label>
            </div>
        </div>

        <div class="col-sm-12">
            <div class="col-sm-6">
                <label>
                    Raw Material QTY
                </label>
            </div>
            <div class="col-sm-6">
                <asp:Label ID="lblRawMaterialQty_p" runat="server"></asp:Label>
            </div>
        </div>

        <div class="col-sm-12">
            <div class="col-sm-6">
                <label>
                    Job Description
                </label>
            </div>
            <div class="col-sm-6">
                <asp:Label ID="lblJobDescription_p" runat="server"></asp:Label>
            </div>
        </div>

        <div class="col-sm-12">
            <div class="col-sm-6">
                <label>
                    Job Opeartion
                </label>
            </div>
            <div class="col-sm-6">
                <asp:Label ID="lblJobOperation_p" runat="server"></asp:Label>
            </div>
        </div>

        <div class="col-sm-12">
            <div class="col-sm-6">
                <label>
                    Remarks
                </label>
            </div>
            <div class="col-sm-6">
                <asp:Label ID="lblRemarks_p" runat="server"></asp:Label>
            </div>
        </div>

        <%--    <div class="col-sm-12">
            <asp:GridView ID="gvWorkOrderIndent_P" runat="server" AutoGenerateColumns="False"
                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
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
                    <asp:TemplateField HeaderText="Item Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Part Quantity" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                        HeaderStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <asp:Label ID="lblPartQuantity" runat="server" Text='<%# Eval("PartQTY")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Material Grade" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                        HeaderStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <asp:Label ID="lblMaterialGrade" runat="server" Text='<%# Eval("GradeName")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Material Thickness" ItemStyle-HorizontalAlign="left"
                        HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <asp:Label ID="lblMaterialThickness" runat="server" Text='<%# Eval("THKValue")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Job Description" ItemStyle-HorizontalAlign="left"
                        HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <asp:Label ID="txtJobDescription" runat="server" CssClass="form-control mandatoryfield mandatorylbl textboxmandatory"
                                Text='<%# Eval("JobDescription")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                        HeaderStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <asp:Label ID="txtRemarks" runat="server" CssClass="form-control mandatoryfield textboxmandatory"
                                Text='<%# Eval("Remarks")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MRN Weight" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                        HeaderStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <asp:Label ID="lblMRNWeight" runat="server" Text='<%# Eval("MRNWeight")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>--%>
        <div class="col-sm-12 text-center">
            <label>
                Note : This Indent By Computer Generated  Indent And Raised By:</label>
            <asp:Label ID="lblIndentRaisedBy" runat="server"></asp:Label>
        </div>
    </div>
    <div class="modal" id="mpeView">
        <div class="modal-dialog" style="height: 100%;">
            <div class="modal-content" style="height: 100%;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" Style="height: 100%;">
                    <Triggers>
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
                                    <%--  <asp:LinkButton Text="Download" CssClass="btn btn-save btn-cons AlignTop" ID="btndownloaddocs"
                                        OnClick="btndownloaddocs_Click" runat="server" />--%>
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

    <div class="modal" id="mpeWo">
        <div class="modal-dialog" style="display: contents;">
            <div class="modal-content" style="width: 95%; margin-left: 2%; margin-top: 2%;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always" Style="height: 100%;">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H2">
                                <div class="text-center">
                                    <asp:Label ID="lblitemname_h" Text="" runat="server"></asp:Label>
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal">
                                x</button>
                        </div>
                        <div class="modal-body" style="height: 100%;">
                            <div class="inner-container" style="height: 100%;">
                                <div class="ip-div text-center" style="margin-bottom: 10px;">
                                    <div id="divWoInput" runat="server">
                                        <%--  <div class="col-sm-12" visible="false">
                                            <div class="col-sm-4 text-right">
                                                <label class="form-label mandatorylbl">
                                                    Part Name</label>
                                            </div>
                                            <div class="col-sm-6 text-left">
                                                <asp:DropDownList ID="ddlPartName" runat="server" CssClass="form-control mandatoryfield" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlPartName_OnSelectedChanged" Width="70%" ToolTip="Select Part Name">
                                                    <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-2">
                                            </div>
                                        </div>--%>
                                    </div>
                                </div>
                            </div>
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
                            <button type="button" class="close btn-primary-purple" onclick="return ShowWoPopup();"
                                data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="divBOIMRN" class="docdiv">
                                <div class="inner-container">
                                    <div id="divadd_BOIMRN" runat="server">

                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <label>PO No </label>
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:DropDownList ID="ddlPONo" runat="server" CssClass="form-control mandatoryfield" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlPONo_OnSelectedChanged" Width="70%" ToolTip="Select PO No">
                                                    <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-2">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-6">
                                                <label>PO Qty </label>
                                                <asp:Label ID="lblPOQty_BOIMRN" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-6">
                                                <label>Total Part Quantity </label>
                                                <asp:Label ID="lblTotalPartQty_BOIMRN" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divoutput_BOIMRN" runat="server">
                                        <div class="col-sm-12 p-t-10 text-left" id="divAJMRN" runat="server">
                                            <asp:GridView ID="gvMRNIssueBOI_WorkOrder" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium" DataKeyNames="MRNID,MRN_LocationID">
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
                                                    <asp:TemplateField HeaderText="MRN Number" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMRNNumber" runat="server" Text='<%# Eval("MRNNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Blocked Qty" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBlockedQty" runat="server" Text='<%# Eval("BlockedWeight")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Balance Qty" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBalanceQty" runat="server" Text='<%# Eval("BalanceWeight")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Issued Qty" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdnBlockedQty" runat="server" Value='<%# Eval("BalanceWeight")%>' />
                                                            <asp:TextBox ID="txtIssuedQty" onkeyup="ValidateIssuedQuantity(this);"
                                                                onkeypress="return validationDecimal(this);" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="col-sm-12 text-center">
                                            <asp:LinkButton ID="btnIssueMRN" runat="server" CssClass="btn btn-cons btn-success"
                                                Text="Issue MRN" OnClientClick="return ValidateIssueMRN(this);" OnClick="btnIssueMRN_Click"></asp:LinkButton>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvBOIIssuedDetails_WorkOrder" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
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
                                                    <asp:TemplateField HeaderText="Blocked Qty" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBlockedQty" runat="server" Text='<%# Eval("BlockedWeight")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Issued Qty" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIssuedQty" runat="server" Text='<%# Eval("ISSUEDWEIGHT")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Stock Status" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStockStatus" runat="server" Text='<%# Eval("StockStatus")%>'></asp:Label>
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>


</asp:Content>
