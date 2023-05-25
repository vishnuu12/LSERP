<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="PressureTestReport.aspx.cs" Inherits="Pages_PressureTestReport" ClientIDMode="Predictable" %>

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

        function clearFields() {
            $('#ContentPlaceHolder1_divRawMaterialInspection').find('[type="text"]').val('')
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
            var msg = Mandatorycheck('ContentPlaceHolder1_divRawMaterialInspection');

            if (msg == false)
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

        function deleteConfirm(PTRHID) {
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
                __doPostBack('deleteVERID', PTRHID);
            });
            return false;
        }

        function PrintPressureTestReport() {
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

            winprint.document.write("<div class='p-t-10'>");
            winprint.document.write("RESULT:" + $('#ContentPlaceHolder1_lblRemarks_p').text());
            winprint.document.write("</div>");

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

        function Print(RIRHID) {
            __doPostBack('Print', RIRHID);
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
                                        <%--<i class="fa fa-th-large opacity-6 yellow"></i>--%>
                                        <img src="../Assets/images/pages.png" alt="" width="16px" />
                                    </span>
                                    <h3 class="page-title-head d-inline-block">Pressure Test Report</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Quality Test Reports</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Pressure Test Report</li>
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
            </Triggers>
            <ContentTemplate>
                <div class="card-container">
                    <div id="div" class="output_section" runat="server">
                        <div class="page-container" style="float: left; width: 100%">
                            <div class="main-card">
                                <div id="divAdd" runat="server">
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
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label">
                                                Item Name
                                            </label>
                                        </div>
                                        <div class="col-sm-6 text-left">
                                            <asp:DropDownList ID="ddlItemName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlItemName_SelectIndexChanged"
                                                CssClass="form-control" Width="70%" ToolTip="Select Item No">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                </div>
                                <div class="p-t-10 p-l-0 p-r-0">
                                    <div id="divRawMaterialInspection"
                                        runat="server">
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <div class="text-left mandatorylbl">
                                                    <label>Test Date </label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtTestDate" CssClass="form-control datepicker mandatoryfield" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="text-left">
                                                    <label class="mandatorylbl">BSL No  </label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtBSLNo" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="text-left">
                                                    <label class="mandatorylbl">QTY </label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtQTY" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <div class="text-left mandatorylbl">
                                                    <label>Approved Test Procedure No </label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtApprovedTestProcedureNo" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="text-left">
                                                    <label class="mandatorylbl">Type Of Test </label>
                                                </div>
                                                <div>
                                                    <asp:DropDownList ID="ddlTypeOftest" runat="server"
                                                        CssClass="form-control mandatoryfield" Width="70%" ToolTip="Select Item No">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="text-left">
                                                    <label class="mandatorylbl">Medium </label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtMedium" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <div class="text-left">
                                                    <label class="mandatorylbl">Test Pressure </label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txttestpressure" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="text-left">
                                                    <label class="mandatorylbl">Holding Time </label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtHoldingTime" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="text-left">
                                                    <label class="mandatorylbl">Calibration No 1 </label>
                                                </div>
                                                <div>
                                                    <asp:DropDownList ID="ddlCalibrationNo1" runat="server"
                                                        CssClass="form-control" ToolTip="Select Calibration No 1">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <div class="text-left">
                                                    <label class="mandatorylbl">Calibration No 2 </label>
                                                </div>
                                                <div>
                                                    <asp:DropDownList ID="ddlCalibrationNo2" runat="server"
                                                        CssClass="form-control" ToolTip="Select Calibration No 2">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-8">
                                                <div class="text-left">
                                                    <label class="mandatorylbl">Result </label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtResult" CssClass="form-control mandatoryfield" TextMode="MultiLine" Rows="3" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-12 text-center p-t-10">
                                            <asp:LinkButton ID="btnSavePTReport" runat="server" CssClass="btn btn-cons btn-save AlignTop"
                                                Text="Save" OnClientClick="return Validate('ContentPlaceHolder1_divRawMaterialInspection');"
                                                OnClick="btnSavePTReport_Click">
                                            </asp:LinkButton>
                                            <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-cons btn-save AlignTop" Text="Cancel"
                                                OnClick="btnCancel_Click">
                                            </asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                                <div id="divOutPut" class="col-sm-12 p-t-10" style="overflow-x: scroll;" runat="server">
                                    <asp:GridView ID="gvPressureTestReport" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                        EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                        OnRowCommand="gvPressureTestReport_OnrowCommand" DataKeyNames="PTRHID">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno1" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
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
                                                    <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditPT" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                         <img src="../Assets/images/edit-ec.png" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnAdd" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                        OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("PTRHID")) %>'>
                                                         <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PDF" ItemStyle-CssClass="text-center" HeaderStyle-Width="10%"
                                                ItemStyle-Width="10%" HeaderStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnpdf" runat="server"
                                                        OnClientClick='<%# string.Format("return Print({0});",Eval("PTRHID")) %>'
                                                        CommandName="PDFRMI"><img src="../Assets/images/pdf.png" />
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                    <asp:HiddenField ID="hdnPTRHID" runat="server" Value="0" />
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

    <div id="divPressuretestreport_p" runat="server" style="display: none;">
        <div id="divmainContent_p" runat="server">
            <div class="row p-t-10">
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
                <label>
                    PRESSURE TEST REPORT -  
                    <asp:Label ID="lblTypeOfTestHeader_p" runat="server"></asp:Label>
                </label>
            </div>
            <div class="divtable p-t-10">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <label>Customer Name </label>
                        </td>
                        <td>
                            <asp:Label ID="lblcustomername_p" Style="font-weight: bold; font-size: 15px ! important;" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>PO No </label>
                        </td>
                        <td>
                            <asp:Label ID="lblPONo_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Project </label>
                        </td>
                        <td>
                            <asp:Label ID="lblProject_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Description </label>
                        </td>
                        <td>
                            <asp:Label ID="lblItemName_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>ITP No </label>
                        </td>
                        <td>
                            <asp:Label ID="lblITPNo_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="gvItemDetails_p" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                        HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size (mm)" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                        HeaderStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSize" runat="server" Text='<%# Eval("Size")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DRG. REF" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                        HeaderStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDRGREF" runat="server" Text='<%# Eval("DrawingNo")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BSL No" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                        HeaderStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBSLNo" runat="server" Text='<%# Eval("BSLNo")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="QTY" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                        HeaderStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblQTY" runat="server" Text='<%# Eval("AcceptedQty")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Approved Test Procedure No </label>
                        </td>
                        <td>
                            <asp:Label ID="lblApprovedTestProcedureNo_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Type Of Test </label>
                        </td>
                        <td>
                            <asp:Label ID="lblTypeOfTest_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Medium </label>
                        </td>
                        <td>
                            <asp:Label ID="lblMedium_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Design Pressure </label>
                        </td>
                        <td>
                            <asp:Label ID="lblDesignPressure_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Test Pressure </label>
                        </td>
                        <td>
                            <asp:Label ID="lblTestPressure_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Test Temprature </label>
                        </td>
                        <td>
                            <asp:Label ID="lblTestTemprature_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Holding Time </label>
                        </td>
                        <td>
                            <asp:Label ID="lblHoldingTime_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="divtable p-t-10">
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2" class="text-center">
                            <label>Calibration Details Of Pressure Gauge </label>
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <label>Code No </label>
                        </td>
                        <td>
                            <asp:Label ID="lblCodeno_p" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <label>Range </label>
                        </td>
                        <td>
                            <asp:Label ID="lblRange_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Calibration Ref </label>
                        </td>
                        <td>
                            <asp:Label ID="lblCalibrationRef_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Calibration Done On </label>
                        </td>
                        <td>
                            <asp:Label ID="lblCalibrationDoneOn_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Calibration Due On </label>
                        </td>
                        <td>
                            <asp:Label ID="lblCalibrationDueOn_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>

        <div id="divResult_p">
            <label>Result:</label>
            <asp:Label ID="lblResult_p" runat="server"></asp:Label>
        </div>
        <div id="divfootercontent_p">
            <div class="row" style="padding-bottom: 10mm;">
                <div class="col-sm-6 p-l-10">
                    <label>For NTPC Rio Chennai</label>
                </div>
                <div class="col-sm-6 p-r-10" style="text-align: right;">
                    <label>For Lonestar Industries</label>
                </div>
            </div>
        </div>
    </div>


</asp:Content>


