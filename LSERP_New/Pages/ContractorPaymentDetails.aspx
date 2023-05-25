<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="ContractorPaymentDetails.aspx.cs"
    Inherits="Pages_ContractorPaymentDetails" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function ValidateBillDetails() {
            var msg = Mandatorycheck('ContentPlaceHolder1_divBill');
            if (msg) {
                if ($('#ContentPlaceHolder1_gvPrimaryJobOrderDetails').find('tr').not(':first').find('[type="checkbox"]:checked').length > 0) {
                    $('#ContentPlaceHolder1_hdnBillAmount').val($('.billamount').text());
                    return true;
                }
                else {
                    ErrorMessage('Error', 'Please select payment');
                    hideLoader();
                    return false;
                }
            }
            else
                return false;
        }

        function checkallcalculate(ele) {
            var sum = 0;
            debugger;
            if ($(ele).is(":checked")) {
                sum = 0;
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', true);
                $('#ContentPlaceHolder1_gvPrimaryJobOrderDetails_chkall').closest('table').find('.cost').each(function () {
                    if (sum == 0)
                        sum = $(this).text();
                    else
                        sum = parseFloat(sum) + parseFloat($(this).text());
                });
                $('.totalcost').text(sum);

                var gc = $('.generalconsumable').val();
                if (gc != "") {
                    sum = sum - parseFloat(gc);
                }
                $('.billamount').text(sum);
            }
            else {
                sum = 0;
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', false);
                $('.totalcost').text(0.00);
                $('.billamount').text(0.00);
            }
        }

        function calculatetotalcost(ele) {
            var sum = 0;
            $('#ContentPlaceHolder1_gvPrimaryJobOrderDetails').find('.cost').each(function () {
                if ($(this).closest('tr').find('[type="checkbox"]').is(":checked")) {
                    if (sum == 0)
                        sum = $(this).text();
                    else
                        sum = parseFloat(sum) + parseFloat($(this).text());
                }
            });
            $('.totalcost').text(parseFloat(sum).toFixed(2));
            var gc = $('.generalconsumable').val();
            if (gc != "") {
                sum = sum - parseFloat(gc);
            }
            $('.billamount').text(parseFloat(sum).toFixed(2));
        }

        function calculategc() {
            if ($(event.target).val() != "" || $(event.target).val() > 0) {
                var billamount = parseFloat($('.billamount').text())
                var gc = parseFloat($(event.target).val());
                if ($('.billamount').text() == "" || billamount < gc) {
                    ErrorMessage('Error', 'General consumable cannot greater bill amount');
                    $(event.target).val(0)
                }
                else
                    $('.billamount').text(parseFloat(billamount - gc).toFixed(2));
            }
        }

        function PrintBillDetails(epstyleurl, Main, QrCode) {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var Address = $('#ContentPlaceHolder1_hdnAddress').val();
            var PhoneAndFaxNo = $('#ContentPlaceHolder1_hdnPhoneAndFaxNo').val();
            var Email = $('#ContentPlaceHolder1_hdnEmail').val();
            var WebSite = $('#ContentPlaceHolder1_hdnWebsite').val();

            winprint.document.write("<html><head><title>");
            winprint.document.write("");
            winprint.document.write("</title>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='" + epstyleurl + "'/>");
            winprint.document.write("<link rel='stylesheet' href='" + Main + "' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet'  href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/printg.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<style type='text/css'> .page_generateoffer{ margin:5mm; }  </style>");
            winprint.document.write("<style type='text/css'> .subheading_p{ color:#000; font-weight:bold; }  </style>");


            winprint.document.write("<div class='print-page' style='position:fixed;border:1px solid #000;width:200mm;height:287mm;margin-top:5mm;margin-bottom:5mm;margin-left:5mm;margin-right:5mm'></div>");
            winprint.document.write("<table style='width:200mm;height:287mm;margin-left:20px;border:0px !important'><thead><tr><td>");
            winprint.document.write("<div class='col-sm-12 print-generateoffer' style='border-bottom:1px solid #000;text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;height:102px'>");
            winprint.document.write("<div>");
            winprint.document.write("<div>");
            winprint.document.write("<div class='col-sm-12 padding0' id='divLSERPLogo' style='margin:20px auto;display:block;padding:5px 0px;'>");
            winprint.document.write("<img src='../Assets/images/topstrrip.jpg' alt='' height='100px;' style='object-fit:contain;width:100%;display:none'>");
            winprint.document.write("<div class='row' style='position:fixed;width:200mm;left:5mm;border-bottom:1px solid #000;'>");
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

            winprint.document.write("<div class='row padding0 page_generateoffer'>");
            winprint.document.write($('#ContentPlaceHolder1_divContractorPayment_p').html());
            winprint.document.write("</div>");

            winprint.document.write("</td></tr></tbody>");
            winprint.document.write("<tfoot><tr><td>");
            winprint.document.write("<div class='col-sm-12' style='height:30mm;'>");
            winprint.document.write("<div>");
            winprint.document.write("<div class='col-sm-12 row' style='margin-bottom:20px;border-top:1px solid #000;position:fixed;width:200mm;bottom:0px'>");
            winprint.document.write("<img id='imgqrcode' class='Qrcode' src='" + QrCode + "' />");
            winprint.document.write("</div>");
            winprint.document.write("</div></div>");
            winprint.document.write("</td></tr></tfoot></table>");
            //winprint.document.write("</div>");
            //winprint.document.write("</div>");
            winprint.document.write("</body></html>");

            setTimeout(function () {
                winprint.document.close();
                winprint.focus();
                winprint.print();
                // winprint.close();
            }, 1000);
        }

        $(document).ready(function () {
            $('.datepicker').datepicker({
                format: 'dd/mm/yyyy'
            });
        });

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
                                    <h3 class="page-title-head d-inline-block">Contractor Payment Details</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Production</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Contractor Payment Details</li>
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
                    <asp:PostBackTrigger ControlID="btnBillCompleted" />
                    <asp:PostBackTrigger ControlID="imgExcel" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" class="btnAddNew" runat="server">
                            <div class="text-center">
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Contractor Name</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlContractorName" runat="server" AutoPostBack="false"
                                            CssClass="form-control"
                                            Width="70%" ToolTip="Select Contractor Name">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            From Date</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:TextBox ID="txtFromDate" runat="server" autocomplete="off" CssClass="form-control mandatoryfield datepicker"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            To Date
                                        </label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:TextBox ID="txtTodate" runat="server" autocomplete="off" CssClass="form-control mandatoryfield datepicker"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 text-center p-t-10">
                                    <asp:LinkButton ID="btnsubmit" runat="server" Text="Submit" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divAdd');"
                                        OnClick="btnsubmit_Click" CssClass="btn btn-cons btn-success"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-6">
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server">
                                                <asp:LinkButton ID="imgExcel" runat="server" CssClass="excel_bg" ToolTip="Excel Download"
                                                    OnClick="btnExcelDownload_Click" />
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvPrimaryJobOrderDetails" runat="server" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                DataKeyNames="CPDID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkall" runat="server" onclick="return checkallcalculate(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkitems" runat="server" onclick="return calculatetotalcost(this);" AutoPostBack="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="S.No" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Job Order ID" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljoborderID" runat="server" Text='<%# Eval("JobCardNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="RFP No" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRFPNo" runat="server" Text='<%# Eval("RFPNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Part Name" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartName" runat="server" Text='<%# Eval("PartName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Process Name" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblprocesName" runat="server" Text='<%# Eval("ProcessName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Cost" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCost" runat="server" CssClass="cost" Text='<%# Eval("Amount")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Voucher No" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVoucherNo" runat="server" Text='<%# Eval("VoucherNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDate" runat="server" Text='<%# Eval("PaymentDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnPJOID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnEDID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnRFPDID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnBillAmount" runat="server" Value="0" />

                                            <asp:HiddenField ID="hdnAddress" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnPhoneAndFaxNo" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnEmail" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnWebsite" runat="server" Value="0" />

                                        </div>
                                        <div class="col-sm-12 p-t-10 text-right">
                                            <asp:Label ID="lblTotalCost" CssClass="totalcost" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divBill" runat="server">
                            <div class="col-sm-12 p-t-10">
                                <div class="col-sm-4 text-right">
                                    <label class="form-label">
                                        Bill Amount
                                    </label>
                                </div>
                                <div class="col-sm-6 text-left">
                                    <asp:Label ID="lblBillAmount" runat="server" CssClass="billamount"></asp:Label>
                                </div>
                                <div class="col-sm-2">
                                </div>
                            </div>
                            <div class="col-sm-12 p-t-10">
                                <div class="col-sm-4 text-right">
                                    <label class="form-label mandatorylbl">
                                        General Consumable
                                    </label>
                                </div>
                                <div class="col-sm-6 text-left">
                                    <asp:TextBox ID="txtGeneralConsumable" runat="server" onkeypress="return validationDecimal(this);" onblur="calculategc();" CssClass="form-control mandatoryfield generalconsumable"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                </div>
                            </div>
                            <div class="col-sm-12 p-t-10">
                                <div class="col-sm-4 text-right">
                                    <label class="form-label mandatorylbl">
                                        Bill No
                                    </label>
                                </div>
                                <div class="col-sm-6 text-left">
                                    <asp:TextBox ID="txtBillNo" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                </div>
                            </div>
                            <div class="col-sm-12 p-t-10">
                                <div class="col-sm-4 text-right">
                                    <label class="form-label mandatorylbl">
                                        Bill Date
                                    </label>
                                </div>
                                <div class="col-sm-6 text-left">
                                    <asp:TextBox ID="txtBillDate" runat="server" CssClass="form-control mandatoryfield datepicker"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                </div>
                            </div>
                            <div class="col-sm-12 p-t-10">
                                <div class="col-sm-4 text-right">
                                    <label class="form-label mandatorylbl">
                                        Remarks
                                    </label>
                                </div>
                                <div class="col-sm-6 text-left">
                                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                </div>
                                <div class="col-sm-2">
                                </div>
                            </div>
                            <div class="col-sm-12 p-t-10">
                                <div class="col-sm-4 text-right">
                                    <label class="form-label mandatorylbl">
                                        Attach File
                                    </label>
                                </div>
                                <div class="col-sm-6 text-left">
                                    <asp:FileUpload ID="fbAttachFile" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-sm-2">
                                </div>
                            </div>
                            <div class="col-sm-12 text-center p-t-10">
                                <asp:LinkButton ID="btnBillCompleted" runat="server" Text="Bill Completed" OnClientClick="return ValidateBillDetails();"
                                    OnClick="btnBillCompleted_Click" CssClass="btn btn-cons btn-success"></asp:LinkButton>
                            </div>

                            <div class="col-sm-12 p-t-10">
                                <asp:GridView ID="gvBillDetails" runat="server" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" OnRowCommand="gvPrimaryJobOrderDetails_OnRowCommand"
                                    CssClass="table table-hover table-bordered medium" DataKeyNames="CPBDID">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center"
                                            HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bill No" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBillNo" runat="server" Text='<%# Eval("BillNo")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("BillAmount")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Gen Consumable" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGenConsumable" runat="server" Text='<%# Eval("GeneralCosumable")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server" Text='<%# Eval("BillDate")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View File" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnviewFile" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                    CommandName="ViewFile" OnClientClick='<%# string.Format("return ViewConfirm({0});",Eval("CPBDID"),Eval("FileName")) %>'><img src="../Assets/images/view.png"/></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Print" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnPrintJO" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                    CommandName="print"><img src="../Assets/images/view.png"/></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div id="divContractorPayment_p" runat="server" style="display: none;">
                        <div class="row col-sm-12 p-t-10">
                            <asp:Label ID="lblPrintedDate_p" runat="server"></asp:Label>
                        </div>
                        <div class="col-sm-12 text-center">
                            <asp:Label ID="lblBillMonthYear_p" Style="font-size: large ! important; color: #000 !important; font-weight: bold;" runat="server"></asp:Label>
                        </div>
                        <div class="row col-sm-12 p-t-10">
                            <div class="col-sm-6">
                                <lable class="subheading_p">Contractor Name</lable>
                                <asp:Label ID="lblContractorName_p" runat="server"></asp:Label>
                            </div>
                            <div class="col-sm-6" style="text-align: end;">
                                <lable class="subheading_p">Team Name</lable>
                                <asp:Label ID="lblteamname_p" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row col-sm-12 p-t-10">
                            <div class="col-sm-4">
                                <lable class="subheading_p">Bill No</lable>
                                <asp:Label ID="lblBillNo_p" runat="server"></asp:Label>
                            </div>
                            <div class="col-sm-4">
                                <lable class="subheading_p">Bill Amount</lable>
                                <asp:Label ID="lblBillAmount_p" runat="server"></asp:Label>
                            </div>
                            <div class="col-sm-4" style="text-align: end;">
                                <lable class="subheading_p">Bill Date</lable>
                                <asp:Label ID="lblBillDate_p" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-sm-12 p-t-10">
                            <asp:GridView ID="gvContractorPaymentDetails_p" runat="server" AutoGenerateColumns="False"
                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found"
                                CssClass="table table-hover table-bordered medium">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center"
                                        HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDate" runat="server" Text='<%# Eval("PaymentDate")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="JO No" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblJONo" runat="server" Text='<%# Eval("JobOrderID")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="RFP No" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRFPNo" runat="server" Text='<%# Eval("RFPNo")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="User Name" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("EmployeeName")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Voucher No" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblVoucherNo" runat="server" Text='<%# Eval("VoucherNo")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmt" runat="server" Text='<%# Eval("Amount")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="row col-sm-12 p-t-10">
                            <lable class="subheading_p">Remarks : </lable>
                            <asp:Label ID="lblRemarks_p" runat="server"></asp:Label>
                        </div>
                        <div class="row col-sm-12 p-t-10">
                            <lable class="subheading_p">General Consumable : </lable>
                            <asp:Label ID="lblGC_p" runat="server"></asp:Label>
                        </div>
                        <div class="row col-sm-12 p-t-10">
                            <lable class="subheading_p">Total Amount : </lable>
                            <asp:Label ID="lblTotalAmount_p" runat="server"></asp:Label>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>


