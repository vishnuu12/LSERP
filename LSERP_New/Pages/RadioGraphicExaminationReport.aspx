<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="RadioGraphicExaminationReport.aspx.cs" Inherits="Pages_RadioGraphicExaminationReport" ClientIDMode="Predictable" %>

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
            var Report = Mandatorycheck('ContentPlaceHolder1_divLPIReport');

            if (parseInt($('#ContentPlaceHolder1_gvRadioGraphicExaminationReportDetails').find('[type="checkbox"]:checked').length) > 0) {
                if (msg == false) {
                    res = false;
                }
                if (Report == false)
                    res = false;
            }
            else {
                ErrorMessage('Error', 'Radio Graphic Details Not Selected');
                res = false;
            }
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


        function PrintRadioGraphicExaminationReport() {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var RecordLength = $('#ContentPlaceHolder1_gvRadioGraphicExaminationDetails_p').find('tr').length;

            var Address = $('#ContentPlaceHolder1_hdnAddress').val();
            var PhoneAndFaxNo = $('#ContentPlaceHolder1_hdnPhoneAndFaxNo').val();
            var Email = $('#ContentPlaceHolder1_hdnEmail').val();
            var WebSite = $('#ContentPlaceHolder1_hdnWebsite').val();

            var MainContent = $('#ContentPlaceHolder1_divmainContent_p').html();
            var FooterContent = $('#divfootercontent_p').html();

            winprint.document.write("<html><head><title>");
            winprint.document.write("Offer");
            winprint.document.write("</title>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='../Assets/css/ep-style.css'/>");
            winprint.document.write("<link rel='stylesheet' href='../Assets/css/main.css' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet'  href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/printg.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='../Assets/images/topstrrip.jpg' type='text/css'/>");

            winprint.document.write("<style type='text/css'/>  label { font-weight:bold;padding-left:5px;  } .divtable table td span{ padding-left:5px ! important; } </style>");

            winprint.document.write("<style type='text/css'/>  .vam{ vertical-align: middle; } .divtable table tr { height:10mm; } .divtable table td{ border:1px solid;padding-top:5px; } </style>");

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
            winprint.document.write("<table class='table table-hover medium' cellspacing='0' rules='all' border='1' id='ContentPlaceHolder1_gvRadioGraphicExaminationDetails_p' style='border-collapse:collapse;'>");
            winprint.document.write("<tbody>");

            for (var j = 0; j < RecordLength; j++) {
                winprint.document.write("<tr align='center'>" + $('#ContentPlaceHolder1_gvRadioGraphicExaminationDetails_p').find('tr')[j].innerHTML + "</tr>");
            }

            //  k = k + 15;
            winprint.document.write("</tbody>");
            winprint.document.write("</table>");
            winprint.document.write("</div>");

            winprint.document.write("<div class='divtable p-t-10'>");
            winprint.document.write(FooterContent);
            winprint.document.write("</div>");

            winprint.document.write("</div>");

            winprint.document.write("</td></tr></tbody>");
            winprint.document.write("<tfoot><tr><td>");
            winprint.document.write("<div class='col-sm-12' style='height:5mm;'>");
            winprint.document.write("<div>");
            winprint.document.write("<div  style='margin-bottom:20px;position:fixed;width:200mm;bottom:0px'>");

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
                                    <h3 class="page-title-head d-inline-block">Radio Graphic Examination Report</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Quality</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">   Radio Graphic Examination Report </li>
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
                    <asp:PostBackTrigger ControlID="gvRadioGraphicHeader" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="div" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="" id="divInput" runat="server">
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
                                        <div id="divInterNational" runat="server" visible="true">
                                            <div class="col-sm-12 p-t-10 p-l-0 p-r-0">
                                                <table align="center" id="tbl_customerDetails" class="tr_class table" style="text-transform: none;">
                                                    <tbody>
                                                        <tr class="eqheading">
                                                            <td>
                                                                <div class="offerFormHeading1">
                                                                    Radio Graphic Examination Report
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
                                                                <span id="Span15" class="eqdetailslabel">No Of Exposures</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtNoOfExposures" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span1" class="eqdetailslabel">IQI</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:RadioButtonList ID="rblIQI" runat="server" CssClass="radio radio-success" RepeatDirection="Horizontal"
                                                                    RepeatLayout="Flow" TabIndex="5">
                                                                    <asp:ListItem Selected="True" Value="Hole">Hole</asp:ListItem>
                                                                    <asp:ListItem Value="Wire">Wire</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span2" class="eqdetailslabel">Technique Sheet And Reference No</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtTechniqueSheetAndReferenceNo" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span6" class="eqdetailslabel">Exposure technique</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:RadioButtonList ID="rblExposuretechnique" runat="server" CssClass="radio radio-success"
                                                                    RepeatDirection="Horizontal" RepeatLayout="Flow" TabIndex="5">
                                                                    <asp:ListItem Selected="True" Value="SWSI">SWSI</asp:ListItem>
                                                                    <asp:ListItem Value="DWSI">DWSI</asp:ListItem>
                                                                    <asp:ListItem Value="DWDI">DWDI</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span7" class="eqdetailslabel">Stage Of Inspection</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtStageOfInspection" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span44" class="eqdetailslabel">Viewing technique</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:RadioButtonList ID="rblViewingTechinque" runat="server" CssClass="radio radio-success"
                                                                    RepeatDirection="Horizontal" RepeatLayout="Flow" TabIndex="5">
                                                                    <asp:ListItem Selected="True" Value="SW">SW</asp:ListItem>
                                                                    <asp:ListItem Value="DW">DW</asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span45" class="eqdetailslabel">SOD / FOD</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtSODFOD" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr class="eqheading">
                                                            <td>
                                                                <div class="offerFormHeading1">
                                                                    Leed Screens
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span8" class="eqdetailslabel">Front</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtLeadScreensFront" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span9" class="eqdetailslabel">Back</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtLeadScreensback" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span10" class="eqdetailslabel">Job Seriel No</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtJobSerielNo" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span11" class="eqdetailslabel">Film</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtFilm" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span20" class="eqdetailslabel">Base Material</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtBasematerial" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span21" class="eqdetailslabel">Technique</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtTechnique" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span22" class="eqdetailslabel">Base Material Size And Thickness</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtBaseMaterialSizeAndThickness" CssClass="form-control mandatoryfield"
                                                                    runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span23" class="eqdetailslabel">Place ment Of Back Scatter</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtPlacementOfBackScatter" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span24" class="eqdetailslabel">Weld Thickness</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtWeldThickness" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span25" class="eqdetailslabel">Sensitivity</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtSensitivity" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span26" class="eqdetailslabel">Weld reinforcement Thickness</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtWeldReinforcementThickness" CssClass="form-control mandatoryfield"
                                                                    runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span27" class="eqdetailslabel">No Of Films in Film Castee</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtNoOfFilmsInFilmCastee" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span46" class="eqdetailslabel">Film Manufacture</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtFilmManufacture" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span47" class="eqdetailslabel">SSOFD</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtSSOFD" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span48" class="eqdetailslabel">Film Type Designation</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtFilmTypeDesignation" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left"></td>
                                                            <td align="left"></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span28" class="eqdetailslabel">Procedure No And revision No</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtProcedureNoAndRevNo" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span29" class="eqdetailslabel">Density</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtDensity" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span31" class="eqdetailslabel">Developing Time</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtDevelopingtime" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span class="eqdetailslabel">Developing Temprature </span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtDevelopingTemprature" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span34" class="eqdetailslabel">SIZE of ‘B’ letter</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtSizeOfBLetter" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span37" class="eqdetailslabel">Placing Of B Letter</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtPlacingOfBLetter" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr class="eqheading">
                                                            <td>
                                                                <div class="offerFormHeading1">
                                                                    X Ray
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span32" class="eqdetailslabel">KV</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtXrayKV" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span33" class="eqdetailslabel">Ma</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtXrayMa" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span35" class="eqdetailslabel">Focal Spot</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtFocalSpot" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left"></td>
                                                            <td align="left"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <div class="offerFormHeading1">
                                                                    Gama Ray Source
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span39" class="eqdetailslabel">Source</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtGamaRaySource" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span40" class="eqdetailslabel">Strength</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtGamaRayStrength" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span41" class="eqdetailslabel">Size</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtGamaRaySize" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left"></td>
                                                            <td align="left"></td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span36" class="eqdetailslabel">Evaluvation Of 'B' Letter</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtEvaluvationOfBleeter" CssClass="form-control mandatoryfield"
                                                                    runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span38" class="eqdetailslabel">RT Performer</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtRTPerformer" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td align="left">
                                                                <span class="eqdetailslabel">Other </span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtOther" CssClass="form-control mandatoryfield"
                                                                    runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td colspan="4">
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
                                                                <div class="col-sm-12 text-center">
                                                                    <div id="divRGEReport" runat="server">
                                                                        <div id="Div1" class="col-sm-12 p-t-10" runat="server">
                                                                            <asp:GridView ID="gvRadioGraphicExaminationReportDetails" runat="server" AutoGenerateColumns="False"
                                                                                ShowHeaderWhenEmpty="True" CssClass="table table-hover table-bordered medium"
                                                                                EmptyDataText="No Records Found" DataKeyNames="RGEDID,RGERID">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="" ItemStyle-Width="2%">
                                                                                        <HeaderTemplate>
                                                                                        </HeaderTemplate>
                                                                                        <ItemTemplate>
                                                                                            <asp:CheckBox ID="chkitems" runat="server" AutoPostBack="false" onclick="return MakeMandatory(this);" />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                                                                                        HeaderStyle-HorizontalAlign="Center">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'
                                                                                                Style="text-align: center"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Joint Identification">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtJointIdentification" runat="server" CssClass="form-control" Text='<%# Eval("JointIndentification")%>'></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Segment">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtSegment" CssClass="form-control" runat="server" Text='<%# Eval("Segment")%>'></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Size Of Film">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtSizeofFilm" CssClass="form-control" runat="server" Text='<%# Eval("SizeOfFilm")%>'></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Density IQI">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtIQIDensity" CssClass="form-control" runat="server" Text='<%# Eval("DensityIQI")%>'></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Density AI">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtDensityAI" CssClass="form-control" runat="server" Text='<%# Eval("DensityAI")%>'></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Type Of Discontinuity">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtTypeOfDiscontinuity" CssClass="form-control" runat="server" Text='<%# Eval("TypeOfDiscontinuity")%>'></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Disposition">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtDisposition" CssClass="form-control" runat="server" Text='<%# Eval("Disposition")%>'></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </td>

                                                        </tr>
                                                        <tr class="eqheading">
                                                            <td>
                                                                <div class="offerFormHeading1">
                                                                    For Lone Star Industries
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr class="eqheading">
                                                            <td>
                                                                <label>
                                                                    Prpared BY
                                                                </label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="s" class="mandatorylbl">Name</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLSEPreparedName" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="lblDeclaration" class="mandatorylbl">Date</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtLSEPreparedDate" runat="server" CssClass="form-control mandatoryfield datepicker"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span30" class="mandatorylbl">Level</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLSEPreparedLevel" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                            </td>
                                                            <td align="left"></td>
                                                            <td align="left"></td>
                                                        </tr>
                                                        <tr class="eqheading">
                                                            <td>
                                                                <label>
                                                                    Reviewed BY
                                                                </label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span42" class="mandatorylbl">Name</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLSEReviwedName" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span43" class="mandatorylbl">Date</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtLSEReviewdDate" runat="server" CssClass="form-control mandatoryfield datepicker"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span49" class="mandatorylbl">Designation</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLSEReviewdDesignation" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                            </td>
                                                            <td align="left"></td>
                                                            <td align="left"></td>
                                                        </tr>
                                                        <tr class="eqheading">
                                                            <td>
                                                                <label>
                                                                    For Customer /TPIA
                                                                </label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span16" class="mandatorylbl">Name</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCustomerReviewerName" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span17" class="mandatorylbl">Designation</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtCustomerReviwereDesignation" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span4" class="mandatorylbl">Date</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtCustomerReviwedDate" runat="server" CssClass="form-control mandatoryfield datepicker"></asp:TextBox>
                                                            </td>
                                                            <td align="left"></td>
                                                            <td align="left"></td>
                                                        </tr>
                                                        <tr class="eqheading">
                                                            <td>
                                                                <label>
                                                                    For Authorized / Inspector
                                                                </label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span12" class="mandatorylbl">Name</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtAIName" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span50" class="mandatorylbl">Designation</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtAIDesignation" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span51" class="mandatorylbl">Date</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="AIDate" runat="server" CssClass="form-control mandatoryfield datepicker"></asp:TextBox>
                                                            </td>
                                                            <td align="left"></td>
                                                            <td align="left"></td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 text-center">
                                        <asp:LinkButton ID="btnSaveRGE" runat="server" CssClass="btn btn-cons btn-save AlignTop"
                                            Text="Save" OnClientClick="return Validate();" OnClick="btnSaveRGE_Click">
                                        </asp:LinkButton>
                                    </div>
                                    <div id="divOutPut" class="col-sm-12 p-t-10" style="overflow-x: scroll;" runat="server">
                                        <asp:GridView ID="gvRadioGraphicHeader" runat="server" AutoGenerateColumns="False"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" OnRowCommand="gvRadioGraphicHeader_OnRowCommand" CssClass="table table-hover table-bordered medium"
                                            DataKeyNames="RGERID">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--   <asp:TemplateField HeaderText="Item Name" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                    HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIfSpecifyItemName" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
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
                                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditRGER" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                         <img src="../Assets/images/edit-ec.png" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnAdd" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("RGERID")) %>'>
                                                         <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PDF" ItemStyle-CssClass="text-center" HeaderStyle-Width="10%"
                                                    ItemStyle-Width="10%" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnPDF" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            CommandName="RGEPDF"><img src="../Assets/images/pdf.png" />
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:HiddenField ID="hsnRGERID" runat="server" Value="0" />
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
    </div>

    <div id="divRadioGraphicExaminationReport_p" runat="server" style="display: none;">
        <div id="divmainContent_p" runat="server">

            <div class="text-center">
                <label style="font-size: 15px !important;">RADIO GRAPHIC EXAMINATION REPORT </label>
            </div>
            <div class="divtable p-t-10">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <label>Customer:</label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblCustomer_p" runat="server"></asp:Label>
                        </td>

                        <td>
                            <label>Report No: </label>
                            <asp:Label ID="lblReportNo_p" runat="server"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Project:</label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblProject_p" runat="server"></asp:Label>
                        </td>

                        <td>
                            <label>Report Date: </label>
                            <asp:Label ID="lblReportdate_p" runat="server"></asp:Label>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Job Description:</label>
                        </td>
                        <td>
                            <asp:Label ID="lblJobdescription_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label>PO No:</label>
                        </td>
                        <td>
                            <asp:Label ID="lblPONo_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Drawing No:</label>
                        </td>
                        <td>
                            <asp:Label ID="lblDrawingNo_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label>RFP No:</label>
                        </td>
                        <td>
                            <asp:Label ID="lblRFPNo_p" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <label>Job Serial No:</label>
                        </td>
                        <td>
                            <asp:Label ID="lblJobSerialNo_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label>Date Of Test:</label>
                        </td>
                        <td>
                            <asp:Label ID="lblDateOftest_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Base Material And Thickness:</label>
                        </td>
                        <td>
                            <asp:Label ID="lblBaseMaterialAndThickness_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label>Image Quality Indicator:</label>
                        </td>
                        <td>
                            <asp:Label ID="lblImageQualityIndicater_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Technique Sheet Reference No:</label>
                        </td>
                        <td>
                            <asp:Label ID="lblTechniqueReferenceNo_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label>Exposure Technique:</label>
                        </td>
                        <td>
                            <asp:Label ID="lblExposureTechnique_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Stage Of Inspection:</label>
                        </td>
                        <td>
                            <asp:Label ID="lblStageOfInspection_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label>Viewing Technique:</label>
                        </td>
                        <td>
                            <asp:Label ID="lblViewingTechnique_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Procedure And Rev No:</label>
                        </td>
                        <td>
                            <asp:Label ID="lblProcedureAndRevNo_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label>Weld Thickness And Reinforcement:</label>
                        </td>
                        <td>
                            <asp:Label ID="lblWeldthicknessreinforcement_p" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <label>Lead Screens </label>
                        </td>
                        <td>
                            <asp:Label ID="lblLeadScreensFront_p" Style="display: block;" runat="server"></asp:Label>
                            <asp:Label ID="lblLeadscreensBack_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label>No Of Films In Castelee </label>
                        </td>
                        <td>
                            <asp:Label ID="lblNoOfFilmsInCastelee_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>SOD / FOD </label>
                        </td>
                        <td>
                            <asp:Label ID="lblSODFOD_p" runat="server"></asp:Label>

                        </td>
                        <td>
                            <label>Film Manufacture </label>
                        </td>
                        <td>
                            <asp:Label ID="lblFilmManufacture_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>SSOFD </label>
                        </td>
                        <td>
                            <asp:Label ID="lblSSOFD_p" runat="server"></asp:Label>

                        </td>
                        <td>
                            <label>Film Type / Designation </label>
                        </td>
                        <td>
                            <asp:Label ID="lblFilmTypeDesignation_p" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td rowspan="3">
                            <label>X-Ray Meachine </label>
                        </td>
                        <td>
                            <asp:Label ID="lblKV_p" runat="server"></asp:Label>
                        </td>
                        <td rowspan="2">
                            <label>Sensitivity </label>
                        </td>
                        <td rowspan="2">
                            <asp:Label ID="lblSensitivity_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>

                        <td>
                            <asp:Label ID="lblMA_p" runat="server"></asp:Label>
                        </td>

                    </tr>
                    <tr>

                        <td>
                            <asp:Label ID="lblFocalSpot_p" runat="server"></asp:Label>
                        </td>
                        <td rowspan="2">
                            <label>Developing </label>
                        </td>
                        <td>
                            <asp:Label ID="lblTime_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>

                        <td rowspan="3">
                            <label>Gamma Ray </label>
                        </td>
                        <td>
                            <asp:Label ID="lblGammaRaySource_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblTemprature_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblstrength_p" runat="server"></asp:Label>
                        </td>
                        <td rowspan="2">
                            <label>No Of Exposures  </label>
                        </td>
                        <td rowspan="2">
                            <asp:Label ID="lblNoOfExposures_p" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="lblSourcesize_p" runat="server"></asp:Label>
                        </td>

                    </tr>

                    <tr>
                        <td>
                            <label>Size Of 'B' Leter </label>
                        </td>
                        <td>
                            <asp:Label ID="lblSizeOfBLetter_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label>Placing Of B letter </label>
                        </td>
                        <td>
                            <asp:Label ID="lblPlacingOfBletter_p" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblEvaluvationOfBLetter_p" runat="server"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblRTPerformer_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>

        <div id="divRadioGraphicExaminationDetails_p" class="p-t-10">
            <asp:GridView ID="gvRadioGraphicExaminationDetails_p" runat="server" AutoGenerateColumns="False"
                ShowHeaderWhenEmpty="True" CssClass="table table-hover table-bordered medium"
                EmptyDataText="No Records Found" DataKeyNames="RGEDID,RGERID">
                <Columns>
                    <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                        HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'
                                Style="text-align: center"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Joint Identification">
                        <ItemTemplate>
                            <asp:Label ID="lblJointIdentification" runat="server" Text='<%# Eval("JointIndentification")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Segment">
                        <ItemTemplate>
                            <asp:Label ID="lblSegment" runat="server" Text='<%# Eval("Segment")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Size Of Film">
                        <ItemTemplate>
                            <asp:Label ID="lblSizeofFilm" runat="server" Text='<%# Eval("SizeOfFilm")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Density IQI">
                        <ItemTemplate>
                            <asp:Label ID="lblIQIDensity" runat="server" Text='<%# Eval("DensityIQI")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Density AI">
                        <ItemTemplate>
                            <asp:Label ID="lblDensityAI" runat="server" Text='<%# Eval("DensityAI")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Type Of Discontinuity">
                        <ItemTemplate>
                            <asp:Label ID="lblTypeOfDiscontinuity" runat="server" Text='<%# Eval("TypeOfDiscontinuity")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Disposition">
                        <ItemTemplate>
                            <asp:Label ID="lblDisposition" runat="server" Text='<%# Eval("Disposition")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

        <div id="divfootercontent_p">
            <table style="width: 100%;">
                <tr>
                    <td colspan="2">
                        <label>For LoneStar Industries </label>
                    </td>
                    <td>
                        <label>For Customer / TPIA </label>
                    </td>
                    <td>
                        <label>For Authorized Inspector </label>
                    </td>

                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblLSIPreparedName_p" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LSIReviewedName_p" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblCustomerReviewedName_p" runat="server"></asp:Label></td>
                    <td>
                        <asp:Label ID="lblAIReviewdname_p" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        <label>Signature </label>
                    </td>
                    <td>
                        <label>Signature </label>
                    </td>
                    <td>
                        <label>Signature </label>
                    </td>
                    <td>
                        <label>Signature </label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblLSIPreparedlevel_p" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblLSIReviewdDesignation_p" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblCustomerDesignation_p" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblAIDesignation_p" runat="server"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblLSIPreparedDate_p" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblLSIReviewedDate_p" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblCustomerReviewdDate_p" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblAIreviewdDate_p" runat="server"></asp:Label></td>
                </tr>

            </table>
        </div>


    </div>

</asp:Content>
