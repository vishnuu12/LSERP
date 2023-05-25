<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" 
    CodeFile="WorkOrderPOApprovalPendingStatusReport.aspx.cs" Inherits="Pages_WorkOrderPOApprovalPendingStatusReport" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function ValidateAll() {
            //var checkbox = $('#ContentPlaceHolder1_gvViewCosting').find('[type="checkbox"]').not('#ContentPlaceHolder1_gvViewCosting_chkall').length;
            var checked = $('#ContentPlaceHolder1_gvWorkOrderPO').find('input:checked').not('#ContentPlaceHolder1_gvWorkOrderPO_chkall').length;
            if (checked > 0) {
                if ($(event.target).attr('id') == 'ContentPlaceHolder1_btnReject') {
                    if ($('#ContentPlaceHolder1_txtRemarks').val() == '') {
                        ErrorMessage('Error', 'Plese Enter Remarks');
                        return false;
                    }
                    else {
                        return true;
                    }
                }
                else
                    return true;
            }
            else {
                ErrorMessage('Error', 'Please Select Item');
                return false;
            }
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

        function ShowPoPrintPopUp() {
            $('#mpePoDetails_p').modal({
                backdrop: 'static'
            });
            return false;
        }

        function PrintWorkOrderPO(Approvedtime, POStatus) {
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
            winprint.document.write("<div class='col-sm-12 print-generateoffer' style='border-bottom:1px solid #000;text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;height:102px'>");
            winprint.document.write("<div>");
            winprint.document.write("<div>");
            winprint.document.write("<div class='col-sm-12 padding0' id='divLSERPLogo' style='margin:20px auto;display:block;padding:5px 0px;'>");
            winprint.document.write("<div class='row' style='position:fixed;width:200mm;left:5mm;border-bottom:1px solid #000;'>");
            winprint.document.write("<div class='col-sm-3'>");
            winprint.document.write("<img src='../Assets/images/lonestar.jpeg' alt='lonestar-image' width='90px'>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-6 text-center'>");
            winprint.document.write("<h3 style='font-weight:600;font-size:24px;font-style:italic;color:#000;font-family: Arial;display: contents;'>LONESTAR</h3>");
            winprint.document.write("<span style='font-weight:600;font-size:24px ! important;font-family: Times New Roman;padding-left:5px ! important;'>INDUSTRIES</span>");
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
            winprint.document.write("<div class='text-center' style='color:#000;padding-left:50px;'>KINDLY RETURN THE DUPLICATE COPY OF THE ORDER DULY SIGNED AS A TOKEN OFACCEPTANCE</div>");
            //style='height:100px;display:flex;align-items:flex-end;justify-content:flex-start'
            winprint.document.write("<div class='col-sm-4 p-t-60'>");
            winprint.document.write("<label style='padding-left:15px'>PREPARED BY</label>");
            winprint.document.write("</div>");

            winprint.document.write("<div class='col-sm-4 p-t-60'>");
            winprint.document.write("<label style='padding-left:15px'> CHECKED BY</label>");
            winprint.document.write("</div>");

            winprint.document.write("<div class='col-sm-4 text-center'>");
            winprint.document.write("<label style='display:block;width:100%;'>For LONESTAR INDUSTRIES</label>");

            if (POStatus == "1") {
                winprint.document.write("<div id='divdigitalsign' class='p-t-5' style='display: block;'>");
                winprint.document.write("<div style='text-align: end;' class='p-r-10'><label style='text-align: end;'> Digitally Signed By </label></div>");
                winprint.document.write("<div style='text-align: end;' class='p-r-10'><label> G. UMA MAGESH </label></div>");
                winprint.document.write("<div style='text-align: end;' class='p-r-10'><label>" + Approvedtime + "</label></div></div>");
            }

            winprint.document.write("<label style='display:block;width:100%;padding-top:45px;'>AUTHORISED SIGNATORY</label>");
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
                                    <h3 class="page-title-head d-inline-block">WPO Approval Pending Status Report</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Purchase</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">PO Approval</li>
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
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" class="btnAddNew" runat="server">
                        </div>
                        <div id="divInput" runat="server">
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
                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvWorkOrderPO" runat="server" AutoGenerateColumns="False" OnRowCommand="gvWorkOrderPO_OnRowCommand"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found"
                                                CssClass="table table-hover table-bordered medium" DataKeyNames="WPOID,POStatus,Wonumber,PORevision">
                                                <Columns>
                                                 
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vendor Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVendorName" runat="server" Text='<%# Eval("VendorName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="WPO Number" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSPONumber" runat="server" Text='<%# Eval("Wonumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="L2 Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblWPOStatus" runat="server" Text='<%# Eval("L2Status")%>'></asp:Label>
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
                                                    <%--   <asp:TemplateField HeaderText="Quate Reference Number">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQuateReferenceNumber" runat="server" Text='<%# Eval("QuateReferenceNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <%--    <asp:TemplateField HeaderText="Note">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNote" runat="server" Text='<%# Eval("Note")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                    <asp:TemplateField HeaderText="L2 Approved By">
                                                        <ItemTemplate>
                                                            <asp:Label ID="L2ApprovedBy" runat="server" Text='<%# Eval("WPOL2ApprovedBy")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="L2 Approved Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="L2ApprovedDate" runat="server" Text='<%# Eval("WPOL2ApprovedDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="PDF">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnPDF" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="PDF">  <img src="../Assets/images/pdf.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnWPOID" runat="server" Value="0" />

                                            <asp:HiddenField ID="hdnAddress" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnPhoneAndFaxNo" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnEmail" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnWebsite" runat="server" Value="" />

                                        </div>
                                        <div class="col-sm-12 text-center p-t-10">
                                            <div class="col-sm-4">
                                                <label>Remarks</label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control mandatoryfield"
                                                    TextMode="MultiLine" Rows="3" autocomplete="nope" placeholder="Enter Remarks">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 text-center p-t-10">
                                            <asp:LinkButton ID="btnApprove" Text="Approve" CssClass="btn btn-cons btn-success" runat="server" OnClientClick="return ValidateAll();"
                                                OnClick="btnApprovalReject_Click" CommandName="Approve"></asp:LinkButton>
                                            <asp:LinkButton ID="btnReject" CssClass="btn btn-cons btn-success" Text="Reject" runat="server" OnClientClick="return ValidateAll();"
                                                OnClick="btnApprovalReject_Click" CommandName="Reject"></asp:LinkButton>
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

    <div class="modal" id="mpePoDetails_p">
        <div class="modal-dialog" style="height: 100%; width: fit-content;">
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
                                                <div style="display: inline-block; border: 1px solid #000; padding: 10px">
                                                    <label style="font-size: 20px; font-weight: 700; text-decoration: underline;">
                                                        Consignee Address</label>
                                                    <asp:Label ID="lblConsigneeAddress_p" Style="padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                    <asp:Label ID="lblTNGSTNo" Style="display: block; padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                    <asp:Label ID="lblCSTNo" Style="display: block; padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                    <asp:Label ID="lblECCNo" Style="display: block; padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                    <asp:Label ID="lblTINNo" Style="display: block; padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                    <asp:Label ID="lblGSTNo" Style="padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
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

</asp:Content>

