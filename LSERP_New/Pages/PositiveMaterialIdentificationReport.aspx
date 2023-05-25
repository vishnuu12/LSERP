﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="PositiveMaterialIdentificationReport.aspx.cs" Inherits="Pages_PositiveMaterialIdentificationReport" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function ddlPenetrantChange() {
            var Val = $(event.target).val();
            if ($(event.target).id == "ContentPlaceHolder1_ddlPenetrantBrand")
                $('#ContentPlaceHolder1_txtBatchNo').val(Val.split('/')[1]);
            else if ($(event.target).id == "ContentPlaceHolder1_ddlCleanerBrand")
                $('#ContentPlaceHolder1_txtCleanerBatch').val(Val.split('/')[1]);
            else if ($(event.target).id == "ContentPlaceHolder1_ddlDeveloperBrand")
                $('#ContentPlaceHolder1_txtDeveloperBatchNo').val(Val.split('/')[1]);
        }

        function MakeMandatory(ele) {
            debugger;
            if ($(ele).is(":checked")) {
                $(ele).closest('tr').find('[type="text"]').before('<span class="textboxmandatory" style="color: red;">*</span>');
                $(ele).closest('tr').find('[type="text"]').addClass('mandatoryfield');
            }
            else {
                $(ele).closest('tr').find('[type="text"]').closest('td').find('.textboxmandatory').remove();
                $(ele).closest('tr').find('[type="text"]').removeClass('mandatoryfield');
            }
        }

        function Validate() {
            var res = true;
            var msg = Mandatorycheck('ContentPlaceHolder1_divInput');

            if ($('#ContentPlaceHolder1_gvPartDetails').find('input:checked').length > 0)
                var Report = Mandatorycheck('ContentPlaceHolder1_divPartDetails');
            else {
                res = false
                ErrorMessage('Error', 'Please Select The Part');
            }

            if (msg == false) {
                res = false;
            }
            if (Report == false)
                res = false;

            if (res)
                return true;
            else {
                hideLoader();
                return false;
            }
        }

        function ItemQtyValidation(ele) {
            var TotalItemQty = $('#ContentPlaceHolder1_hdnTotalItemQty').val();
            var Qty = $(ele).val();

            if (parseInt(TotalItemQty) < parseInt(Qty)) {
                ErrorMessage('Error', 'Total Item Qty ' + TotalItemQty + 'should Less Than Equal to Entered Qty');
                $(ele).val('');
            }
        }


        function deleteConfirm(VERID) {
            swal({
                title: "Are you sure?",
                text: "If Yes,record will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deleteVERID', VERID);
            });
            return false;
        }

        function PrintPositiveMaterialIdentificationReport() {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var RecordLength = $('#ContentPlaceHolder1_gvPMIReportDetails_p').find('tr').length;

            var Address = $('#ContentPlaceHolder1_hdnAddress').val();
            var PhoneAndFaxNo = $('#ContentPlaceHolder1_hdnPhoneAndFaxNo').val();
            var Email = $('#ContentPlaceHolder1_hdnEmail').val();
            var WebSite = $('#ContentPlaceHolder1_hdnWebsite').val();

            var MainContent = $('#ContentPlaceHolder1_divmainContent_p').html();

            var FooterContent = $('#divfootercontent_p').html();

            var InstrumentTable = $('#divinstrument_p').html();

            winprint.document.write("<html><head><title>");
            winprint.document.write("Offer");
            winprint.document.write("</title>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='../Assets/css/ep-style.css'/>");
            winprint.document.write("<link rel='stylesheet' href='../Assets/css/main.css' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet'  href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/printg.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='../Assets/images/topstrrip.jpg' type='text/css'/>");

            winprint.document.write("<style type='text/css'/>  label { font-weight:bold; } </style>");

            winprint.document.write("<div class='print-page' style='position:fixed;border:1px solid #000;width:200mm;height:287mm;margin-top:5mm;margin-bottom:5mm;margin-left:5mm;margin-right:5mm'></div>");
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

            winprint.document.write("<div style='margin: 5mm;'>");

            winprint.document.write(MainContent);

            winprint.document.write("<div class='p-t-10'>");
            winprint.document.write("<table class='table table-hover medium' cellspacing='0' rules='all' border='1' id='ContentPlaceHolder1_gvPMIReportDetails_p' style='border-collapse:collapse;'>");
            winprint.document.write("<tbody>");

            var len = parseInt(RecordLength) - 1;

            for (var j = 0; j < RecordLength; j++) {
                if (j == 0)
                    winprint.document.write("<tr align='center'>" + '<th align="left" scope="col" style="width:105px;">Chemical</th>' + $('#ContentPlaceHolder1_gvPMIReportDetails_p').find('tr')[j].innerHTML + '<th align="left" scope="col" style="width:105px;">PMI Result</th>' + "</tr>");
                else if (j == 1)
                    winprint.document.write("<tr align='center'>" + '<td align="left" rowspan="' + len + '"><span id="ContentPlaceHolder1_gvPMIReportDetails_p_lblChemical_0">elments</span></td>' + $('#ContentPlaceHolder1_gvPMIReportDetails_p').find('tr')[j].innerHTML + '<td align="left" rowspan="' + len + '"><span id="ContentPlaceHolder1_gvPMIReportDetails_p_lblPMIResult_0">accepted</span></td>' + "</tr>");
                else
                    winprint.document.write("<tr align='center'>" + $('#ContentPlaceHolder1_gvPMIReportDetails_p').find('tr')[j].innerHTML + "</tr>");
            }

            //  k = k + 15;
            winprint.document.write("</tbody>");
            winprint.document.write("</table>");
            winprint.document.write("</div>");

            winprint.document.write("<div class='p-t-10'>");
            winprint.document.write("Remarks:" + $('#ContentPlaceHolder1_lblRemarks_p').text());
            winprint.document.write("</div>");

            winprint.document.write("<div class='p-t-10'>");
            winprint.document.write(InstrumentTable);
            winprint.document.write("</div>");

            winprint.document.write("</div>");

            winprint.document.write("</td></tr></tbody>");
            winprint.document.write("<tfoot><tr><td>");
            winprint.document.write("<div class='col-sm-12' style='height:30mm;'>");
            winprint.document.write("<div>");
            winprint.document.write("<div  style='margin-bottom:20px;position:fixed;width:200mm;bottom:0px'>");

            winprint.document.write(FooterContent);

            winprint.document.write("</div>");
            winprint.document.write("</div></div>");
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
                                        <%--<i class="fa fa-th-large opacity-6 yellow"></i>--%>
                                        <img src="../Assets/images/pages.png" alt="" width="16px" />
                                    </span>
                                    <h3 class="page-title-head d-inline-block">Positive Material Identification Report</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Quality</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Positive Material Identification Report</li>
                                                </ol>
                                     </nav>
                        <a id="help" href="" alt="" style="margin-top: 4px;">
                            <img src="../Assets/images/help.png" />
                        </a>
                    </div>
                </div>
            </div>
        </div>
        <asp:UpdatePanel ID="upPMI" runat="server">
            <Triggers>
                <asp:PostBackTrigger ControlID="gvPMIReportHeader" />
            </Triggers>
            <ContentTemplate>
                <div class="card-container">
                    <div id="div" class="output_section" runat="server">
                        <div class="page-container" style="float: left; width: 100%">
                            <div class="main-card">
                                <div id="divInput" runat="server">
                                    <div class="col-sm-12 p-t-10" style="padding-left: 35%;">
                                        <div class="col-sm-6 text-left">
                                            <label class="form-label">
                                                Report No</label>
                                            <asp:Label ID="lblReportNo" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-6 text-left">
                                            <label class="form-label">
                                                Convolution Of Records</label>
                                            <asp:TextBox ID="txtConvolutionOfRecords" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label">
                                                Customer Name</label>
                                        </div>
                                        <div class="col-sm-6 text-left">
                                            <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="true" CssClass="form-control"
                                                OnSelectedIndexChanged="ddlCustomerName_OnSelectedIndexChanged" Width="70%" ToolTip="Select Customer Number">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label">
                                                Customer PO</label>
                                        </div>
                                        <div class="col-sm-6 text-left">
                                            <asp:DropDownList ID="ddlCustomerPO" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomerPO_SelectIndexChanged"
                                                CssClass="form-control" Width="70%" ToolTip="Select Customer PO">
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
                                            <asp:DropDownList ID="ddlRFPNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRFPNo_SelectIndexChanged"
                                                CssClass="form-control" Width="70%" ToolTip="Select RFP No">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                    <div runat="server">
                                        <div class="col-sm-12 p-t-10 p-l-0 p-r-0">
                                            <table align="center" id="tbl_customerDetails" class="tr_class table" style="text-transform: none;">
                                                <tbody>
                                                    <tr class="eqheading">
                                                        <td>
                                                            <div class="offerFormHeading1">
                                                                Positive Material Identification Report
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="Span3" class="eqdetailslabel">Report Date</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtReportDate" CssClass="form-control mandatoryfield datepicker"
                                                                runat="server"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <span id="lblIV" class="eqdetailslabel">Item Name</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlItemName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlItemName_SelectIndexChanged"
                                                                CssClass="form-control" Width="70%" ToolTip="Select Item No">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="Span5" class="eqdetailslabel">If Specify Item Name</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtItemName" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <span id="Span13" class="eqdetailslabel">Customer Name</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtCustomername" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="label" class="eqdetailslabel">Job Description</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtJobDescription" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <span id="lblTestDate" class="eqdetailslabel">PO No</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtPONo" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="Span18" class="eqdetailslabel">RFP No</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtRFPNo" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <span id="Span19" class="eqdetailslabel">Test Date</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtTestDate" CssClass="form-control mandatoryfield datepicker" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="Span14" class="eqdetailslabel">Drawing No</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtDrawingNo" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <span id="Span15" class="eqdetailslabel">QAP</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtQAPNo" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="Span52" class="eqdetailslabel">Size</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtSize" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <span id="Span53" class="eqdetailslabel">Qty</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtItemQty" CssClass="form-control mandatoryfield" onkeypress="return fnAllowNumeric(this);"
                                                                runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="Span1" class="eqdetailslabel">BSL No</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtBSLNo" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <span class="eqdetailslabel">ITP No</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtITPDocNo" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td align="left">
                                                            <span class="eqdetailslabel">Tag No</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtTagNo" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <span class="eqdetailslabel">EIL Specification </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtEILSpecification" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td align="left">
                                                            <span class="eqdetailslabel">Date Of Cal </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtDateOfCall" CssClass="form-control datepicker mandatoryfield" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <span class="eqdetailslabel">Due For Cal </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtDueForCal" CssClass="form-control datepicker mandatoryfield" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td align="left">
                                                            <span class="eqdetailslabel">Instrument </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtInstrument" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <span class="eqdetailslabel">Report Reference </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtReportReference" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                        </td>

                                                    </tr>

                                                    <tr>
                                                        <td align="left">
                                                            <span class="eqdetailslabel">Project </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtProject" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td align="left">
                                                            <span class="eqdetailslabel">DRG Part No </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtDRGPartNo_p" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td align="left">
                                                            <span class="eqdetailslabel">Remarks </span>
                                                        </td>
                                                        <td align="left" colspan="2">
                                                            <asp:TextBox ID="txtRemarks" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td align="left"></td>
                                                        <td align="left"></td>

                                                    </tr>

                                                </tbody>
                                            </table>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 text-center">
                                        <div class="col-sm-4">
                                            <label>
                                                Number Of Rows</label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtNumberOfRows" runat="server" onkeypress="return fnAllowNumeric(this);"
                                                CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-6 text-left">
                                            <asp:LinkButton ID="btnAddRow" OnClick="btnAdd_Click" CssClass="btn btn-cons btn-success"
                                                Text="Get Rows" runat="server"> 
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div id="divPartDetails" runat="server">
                                    <div class="col-sm-12 p-t-20">
                                        <asp:GridView ID="gvPartDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                            DataKeyNames="PMIPDID,PMIRID">
                                            <Columns>
                                                <asp:TemplateField HeaderText="" ItemStyle-Width="2%">
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkitems" runat="server" AutoPostBack="false" onclick="return MakeMandatory(this);" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--      <asp:TemplateField HeaderText="Part Name" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                    HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtPartName" runat="server" CssClass="form-control" Text='<%# Eval("PartName")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Chemical" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                    HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtElements" runat="server" CssClass="form-control" Text='<%# Eval("Elements")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--    <asp:TemplateField HeaderText="STD Requirtment" ItemStyle-HorizontalAlign="left"
                                                    HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtSTDRequirtment" runat="server" CssClass="form-control" Text='<%# Eval("STDRequirtment")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Requirtment" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                    HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtRequirtment" runat="server" CssClass="form-control" Text='<%# Eval("Value1")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Observed" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                    HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtobserved" runat="server" CssClass="form-control" Text='<%# Eval("Value2")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- <asp:TemplateField HeaderText="Value - %" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                    HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtValue3" runat="server" CssClass="form-control" Text='<%# Eval("Value3")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Value - %" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                    HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtValue4" runat="server" CssClass="form-control" Text='<%# Eval("Value4")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <%--    <asp:TemplateField HeaderText="Value - %" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                    HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtValue5" runat="server" CssClass="form-control" Text='<%# Eval("Value5")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="PMI Result" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                    HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtPMIResult" runat="server" CssClass="form-control" Text='<%# Eval("PMIResult")%>'></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-sm-12 text-center">
                                        <asp:LinkButton ID="btnSavePMI" runat="server" CssClass="btn btn-cons btn-save AlignTop"
                                            Text="Save" OnClientClick="return Validate();" OnClick="btnSavePMIReport_Click">
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                <div id="divOutPut" class="col-sm-12 p-t-10" style="overflow-x: scroll;" runat="server">
                                    <asp:GridView ID="gvPMIReportHeader" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                        EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                        OnRowCommand="gvPMIReportHeader_OnRowCommand" DataKeyNames="PMIRID">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Name" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                HeaderStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIfSpecifyItemName" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Report No" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                HeaderStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReportNo" runat="server" Text='<%# Eval("ReportNo")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Report Date" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                HeaderStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReportDate" runat="server" Text='<%# Eval("ReportDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditPMI" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                             <img src="../Assets/images/edit-ec.png" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnAdd" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                        OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("PMIRID")) %>'>
                                                         <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PDF" ItemStyle-CssClass="text-center" HeaderStyle-Width="10%"
                                                ItemStyle-Width="10%" HeaderStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnPDF" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                        CommandName="PDF"><img src="../Assets/images/pdf.png" />
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:HiddenField ID="hdnPMIRID" runat="server" Value="0" />
                                    <asp:HiddenField ID="hdnTotalItemQty" runat="server" Value="0" />

                                    <asp:HiddenField ID="hdnAddress" runat="server" Value="" />
                                    <asp:HiddenField ID="hdnPhoneAndFaxNo" runat="server" Value="" />
                                    <asp:HiddenField ID="hdnEmail" runat="server" Value="" />
                                    <asp:HiddenField ID="hdnWebsite" runat="server" Value="" />

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>


    <div id="divFerriteTestReport_p" runat="server" style="display: none;">
        <div id="divmainContent_p" runat="server">
            <div class="row">
                <div class="col-sm-4">
                    <label>RFP No</label>
                    <asp:Label ID="lblRFPNo_p" runat="server"></asp:Label>
                </div>
                <div class="col-sm-4">
                    <label>Report No</label>
                    <asp:Label ID="lblReportNo_p" runat="server"></asp:Label>
                </div>
                <div class="col-sm-4">
                    <label>Date</label>
                    <asp:Label ID="lblDate_p" runat="server"></asp:Label>
                </div>
            </div>
            <div class="p-t-10 text-center">
                <label style="font-size: 15px ! important;">POSITIVE MATERIAL IDENTIFICATION (PMI) REPORT </label>
            </div>
            <div>
                <div class="row p-t-10">
                    <div class="col-sm-4">
                        <label>Customer :</label>
                    </div>
                    <div class="col-sm-8">
                        <asp:Label ID="lblCustomer_p" Style="font-weight: bold;"
                            runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row p-t-10">
                    <div class="col-sm-4">
                        <label>PO No :</label>
                        <asp:Label ID="lblPONo_p" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-4">
                        <label>Project :</label>
                        <asp:Label ID="lblProject_p" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-4">
                        <label>DRG No :</label>
                        <asp:Label ID="lblDRGNo_p" runat="server"></asp:Label>
                    </div>
                </div>

                <div class="row p-t-10">
                    <div class="col-sm-4">
                        <label>ITP No :</label>
                        <asp:Label ID="lblITPNo_p" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-4">
                        <label>Size :</label>
                        <asp:Label ID="lblSize_p" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-4">
                        <label>BSL No :</label>
                        <asp:Label ID="lblBSLNo_p" runat="server"></asp:Label>
                    </div>
                </div>

                <div class="row p-t-10">
                    <div class="col-sm-6">
                        <label>QTY :</label>
                        <asp:Label ID="lblQTY_p" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-6">
                        <label>Description :</label>
                        <asp:Label ID="lblDescription_p" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row p-t-10">
                    <div class="col-sm-6">
                        <label>DRG Part No :</label>
                        <asp:Label ID="lblDRGPartNo_p" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-6">
                        <label>TAG No :</label>
                        <asp:Label ID="lblTagNo_p" runat="server"></asp:Label>
                    </div>

                </div>
                <div class="row p-t-10">
                    <div class="col-sm-6">
                        <label>EIL Specification :</label>
                        <asp:Label ID="lblEILSpecification_p" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-6">
                    </div>

                </div>
            </div>
        </div>

        <div>
            <asp:GridView ID="gvPMIReportDetails_p" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                EmptyDataText="No Records Found" OnRowDataBound="gvPMIReportDetails_p_OnRowDataBound" CssClass="table table-hover table-bordered medium">
                <Columns>

                    <asp:TemplateField HeaderText="Requirtment" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                        HeaderStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <asp:Label ID="lblRequirtment" runat="server" Text='<%# Eval("Requirtment")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Observed" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                        HeaderStyle-HorizontalAlign="left">
                        <ItemTemplate>
                            <asp:Label ID="lblObserved" runat="server" Text='<%# Eval("Observed")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>

        <div>
            <asp:Label ID="lblRemarks_p" runat="server"></asp:Label>
        </div>

        <div id="divinstrument_p">
            <table class='table table-hover medium' cellspacing='0' rules='all' border='1' style='border-collapse: collapse; width: 100%;'>
                <tr>
                    <td>
                        <label>Instrument </label>
                    </td>
                    <td>
                        <label>Date Of Cal </label>
                    </td>
                    <td>
                        <label>Due Of Cal </label>
                    </td>
                    <td>
                        <label>Report Reference </label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblInstrument_p" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDateOfCal_p" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblDueOfcal_p" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblReportReference_p" runat="server"></asp:Label>
                    </td>
                </tr>

            </table>
        </div>

        <div id="divfootercontent_p">
            <div class="row" style="padding-bottom: 10mm;">
                <div class="col-sm-6 p-l-10">
                    <label>For Customer</label>
                </div>
                <div class="col-sm-6 p-r-10" style="text-align: right;">
                    <label>For Lonestar Industries</label>
                </div>
            </div>
        </div>
    </div>

</asp:Content>