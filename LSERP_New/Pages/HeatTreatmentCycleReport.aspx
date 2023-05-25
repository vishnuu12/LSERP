<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="HeatTreatmentCycleReport.aspx.cs" Inherits="Pages_HeatTreatmentCycleReport" ClientIDMode="Predictable" %>

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
                $(ele).closest('tr').find('[type="text"]').before('<span class="textboxmandatory" style="color: red;">*</span>');
                $(ele).closest('tr').find('[type="text"]').closest('td').find('.textboxmandatory').remove();
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

        function deleteConfirm(LPIRID) {
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
                __doPostBack('deleteLPIRID', LPIRID);
            });
            return false;
        }

        function PrintHeatTreatmentCycleReport() {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

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

            winprint.document.write("</div>");
            winprint.document.write("</td></tr></tbody>");
            winprint.document.write("<tfoot><tr><td>");
            winprint.document.write("<div class='col-sm-12' style='height:10mm;'>");
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
                                    <h3 class="page-title-head d-inline-block">Heat Treatment Cycle Report</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Quality</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Heat Treatment Cycle Report</li>
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
                    <asp:PostBackTrigger ControlID="gvHeatTreatmentCycleReport" />
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
                                                                    DefLection Test Report
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
                                                                <span id="Span13" class="eqdetailslabel">RFP No</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtRFPNo" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="label" class="eqdetailslabel">EQUIP No</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtEQUIPNo" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span15" class="eqdetailslabel">Drawing No</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtDrawingNo" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span2" class="eqdetailslabel">Qty</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtItemQty" onkeypress="return validationNumeric(this);" CssClass="form-control mandatoryfield"
                                                                    runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span6" class="eqdetailslabel">Method Of Heat Treatment</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtMethodOfHeatTreatment" CssClass="form-control mandatoryfield"
                                                                    runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span1" class="eqdetailslabel">JobSize And Material</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtJobSizeAndMaterial" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span4" class="eqdetailslabel">Type Of Heat Treatment</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtTypeOfHeatTreatment" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span7" class="eqdetailslabel">Loading temprature</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtLoadingTemprature" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span9" class="eqdetailslabel">Rate Of Heating</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtRateOfHeating" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span8" class="eqdetailslabel">Soaking temprature</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtSoakingTemprature" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span10" class="eqdetailslabel">Soaking Time Duration</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtSoakingTimeDuration" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span11" class="eqdetailslabel">Rate Of Cooling</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtRateOfCooling" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span12" class="eqdetailslabel">Unloading temprature</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtUnloadingTemprature" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span14" class="eqdetailslabel">Method Of ThermoCouble Attachement</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtThermocoubleAttachement" CssClass="form-control mandatoryfield"
                                                                    runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span16" class="eqdetailslabel">Method Of ThermoCouble Removal</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtThermocoubleRemoval" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span17" class="eqdetailslabel">Method Of Support</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtMethodofsupport" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span18" class="eqdetailslabel">Identification </span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtIdentification" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span19" class="eqdetailslabel">Thermo couble Location Sketch</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtThermocoublelocationSketch" CssClass="form-control mandatoryfield"
                                                                    runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span21" class="eqdetailslabel">Welding Engineer Name</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtWeldingEngineername" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span22" class="eqdetailslabel">Quality Incharge Name </span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtQualityInchargeName" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left"></td>
                                                            <td align="left"></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <span id="Span20" class="eqdetailslabel">Note </span>
                                                            </td>
                                                            <td colspan="3">
                                                                <asp:TextBox ID="txtNote" TextMode="MultiLine" Rows="3" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 text-center">
                                        <asp:LinkButton ID="btnSaveHTCyle" runat="server" CssClass="btn btn-cons btn-save AlignTop"
                                            Text="Save" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divInput');"
                                            OnClick="btnSaveHTCyle_Click">
                                        </asp:LinkButton>
                                    </div>
                                    <div id="divOutPut" class="col-sm-12 p-t-10" style="overflow-x: scroll;" runat="server">
                                        <asp:GridView ID="gvHeatTreatmentCycleReport" runat="server" AutoGenerateColumns="False"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found"
                                            OnRowCommand="gvHeatTreatmentCycleReport_OnRowCommand" CssClass="table table-hover table-bordered medium"
                                            DataKeyNames="HTCRID">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- <asp:TemplateField HeaderText="Item Name" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
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
                                                        <asp:LinkButton ID="btnEdit" runat="server"
                                                            CommandArgument='<%#((GridViewRow) Container).RowIndex %>' CommandName="EditHTC">
                                                         <img src="../Assets/images/edit-ec.png" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnAdd" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("HTCRID")) %>'>
                                                         <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PDF" ItemStyle-CssClass="text-center" HeaderStyle-Width="10%"
                                                    ItemStyle-Width="10%" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnPDF" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            CommandName="PdfHTC"><img src="../Assets/images/pdf.png" />
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:HiddenField ID="hdnHTCRID" runat="server" Value="0" />
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

    <div id="divPressuretestreport_p" runat="server" style="display: none;">
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
                <label style="font-size: 15px !important">
                    HEAT TREATMENT LOADING SHEET       
                </label>
            </div>

            <div class="divtable p-t-10">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <label>DRG No </label>
                        </td>
                        <td>
                            <asp:Label ID="lblDRGNo_p" Style="font-weight: bold; font-size: 15px ! important;" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label>EQUIP No </label>
                        </td>
                        <td>
                            <asp:Label ID="lblEquipNo_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>QTY </label>
                        </td>
                        <td>
                            <asp:Label ID="lblQty_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label>Item Name </label>
                        </td>
                        <td>
                            <asp:Label ID="lblItemname_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <div class="divtable p-t-10">
                    <table style="width: 100%;">
                        <tr>
                            <td colspan="2" class="text-center">
                                <label style="font-size: 15px !important;">HEAT TREATMENT CYCLE </label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Method Of Heat Treatment </label>
                            </td>
                            <td>
                                <asp:Label ID="lblMethodOfHeatTreatment_p" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Job Size And Material </label>
                            </td>
                            <td>
                                <asp:Label ID="lblJobsizeMaterial_p" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Type Of Heat Treatment </label>
                            </td>
                            <td>
                                <asp:Label ID="lblTypeOfHeatTreatment_p" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Loading Temprature </label>
                            </td>
                            <td>
                                <asp:Label ID="lblLoadingTemprature_p" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Rate Of Heating </label>
                            </td>
                            <td>
                                <asp:Label ID="lblRateOfHeading_p" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Soaking Temprature </label>
                            </td>
                            <td>
                                <asp:Label ID="lblSoakingTemprature_p" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Soaking Time </label>
                            </td>
                            <td>
                                <asp:Label ID="lblSoakingTime_p" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Rate Of Cooling </label>
                            </td>
                            <td>
                                <asp:Label ID="lblRateOfCooling_p" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Unloading Temprature </label>
                            </td>
                            <td>
                                <asp:Label ID="lblUnloadingTemprature_p" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Method Of ThermoCouple Attachement </label>
                            </td>
                            <td>
                                <asp:Label ID="lblMethodOfTherMoCoubleAttachement_p" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Method Of ThermoCouple Removal </label>
                            </td>
                            <td>
                                <asp:Label ID="lblMethodOfThermoCoubleRemoval_p" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Method Of Support </label>
                            </td>
                            <td>
                                <asp:Label ID="lblMethodOfSupport_p" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label>Identification </label>
                            </td>
                            <td>
                                <asp:Label ID="lblIdentification_p" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div>
                <label>NOTE : </label>
            </div>
            <div style="padding-left: 20px;">
                <asp:Label ID="lblNote_p" runat="server"></asp:Label>
            </div>
        </div>

        <div id="divfootercontent_p">
            <div class="row" style="padding-bottom: 10mm;">
                <div class="col-sm-6 p-l-10">
                    <label>Prepared By Welding Engineer </label>
                </div>
                <div class="col-sm-6 p-r-10" style="text-align: right;">
                    <label>Approved By Welding Co Ordinator </label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
