<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="WeldPlanDetails.aspx.cs" Inherits="Pages_WeldPlanDetails" ClientIDMode="Predictable" %>

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

                $(ele).closest('tr').find('[type="textarea"]').before('<span class="textboxmandatory" style="color: red;">*</span>');
                $(ele).closest('tr').find('[type="textarea"]').addClass('mandatoryfield');
            }
            else {
                $(ele).closest('tr').find('[type="text"]').before('<span class="textboxmandatory" style="color: red;">*</span>');
                $(ele).closest('tr').find('[type="text"]').closest('td').find('.textboxmandatory').remove();

                $(ele).closest('tr').find('[type="textarea"]').before('<span class="textboxmandatory" style="color: red;">*</span>');
                $(ele).closest('tr').find('[type="textarea"]').closest('td').find('.textboxmandatory').remove();
            }
        }

        function Validate() {
            var res = true;
            var msg = Mandatorycheck('ContentPlaceHolder1_divInput');
            var Report = Mandatorycheck('ContentPlaceHolder1_divLPIReport');
            if (parseInt($('#ContentPlaceHolder1_gvWeldPlanDetails').find('[type="checkbox"]:checked').length) > 0) {
                if (msg == false) {
                    res = false;
                }
                if (Report == false)
                    res = false;
            }
            else {
                ErrorMessage('Error', 'LPI Details Not Selected');
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

        function PrintWeldPlanReport() {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var RecordLength = $('#ContentPlaceHolder1_gvWeldPlanDetails_p').find('tr').length;

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

            winprint.document.write("<style type='text/css'/> @media print{@page {size: landscape}}  </style>");

            winprint.document.write("<style type='text/css'/>  label { font-weight:bold;padding-left:5px;  } .divtable table td span{ padding-left:5px ! important; } </style>");

            winprint.document.write("<style type='text/css'/>  .vam{ vertical-align: middle; } .divtable table tr { height:10mm; } .divtable table td{ border:1px solid;padding-top:5px; } </style>");

            winprint.document.write("<div class='print-page' style='position:fixed;border:1px solid #000;width:270mm;height:205mm;margin-top:5mm;margin-bottom:5mm;margin-left:5mm;margin-right:5mm'></div>");
            winprint.document.write("<table style='width:270mm;height:287mm;margin-left:20px;border:0px !important'><thead><tr><td>");
            winprint.document.write("<div style='text-align:center;' class='p-t-20'>");
            winprint.document.write("<h3 style='font-weight:600;font-size:24px;font-style:italic;color:#000;font-family: Arial;display: contents;'>LONESTAR</h3>");
            winprint.document.write("<span style='font-weight:600;font-size:24px ! important;font-family: Times New Roman;'>INDUSTRIES</span>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='row print-generateoffer' style='border-bottom:1px solid #000;text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;height:102px'>");

            winprint.document.write("<div class='col-sm-2'>");
            winprint.document.write("<img src='../Assets/images/lonestar.jpeg' alt='lonestar-image' width='90px'>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-5'>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;width: 100%;padding:0 0px'>" + Address + "</p>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + PhoneAndFaxNo + "</p>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + Email + "</p>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + WebSite + "</p>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-5'>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;width: 100%;padding:0 0px'>" + Address + "</p>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + PhoneAndFaxNo + "</p>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + Email + "</p>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + WebSite + "</p>");
            //winprint.document.write("<img src='../Assets/images/iso.jpeg' alt='lonestar-image' height='90px'>");
            winprint.document.write("</div>");
            winprint.document.write("</div>");
            winprint.document.write("</td></tr></thead>");
            winprint.document.write("<tbody><tr><td>");

            winprint.document.write("<div style='margin: 5mm;'>");

            winprint.document.write(MainContent);

            winprint.document.write("<div class='p-t-10'>");
            winprint.document.write("<table class='table table-hover medium' cellspacing='0' rules='all' border='1' id='ContentPlaceHolder1_gvWeldPlanDetails_p' style='border-collapse:collapse;'>");
            winprint.document.write("<tbody>");

            for (var j = 0; j < RecordLength; j++) {
                winprint.document.write("<tr align='center'>" + $('#ContentPlaceHolder1_gvWeldPlanDetails_p').find('tr')[j].innerHTML + "</tr>");
            }

            //  k = k + 15;
            winprint.document.write("</tbody>");
            winprint.document.write("</table>");
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

    </script>
    <style type="text/css">
        .main-container {
            width: 99% !important;
            margin-left: 2% !important;
        }

        .sidebar-shadow {
            display: none;
        }

        .fixed-sidebar {
            display: none;
        }

        .subheader {
            background-color: white !important;
            color: #000 !important;
            text-align: center !important;
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
                                        <%--<i class="fa fa-th-large opacity-6 yellow"></i>--%>
                                        <img src="../Assets/images/pages.png" alt="" width="16px" />
                                    </span>
                                    <h3 class="page-title-head d-inline-block">Weld Plan Report</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                    <ol class="breadcrumb">
                                        <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                        <li class="breadcrumb-item"><a href="javascript:void(0);">Quality</a></li>
                                        <li class="active breadcrumb-item" aria-current="page">Weld Plan Report</li>
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
                    <asp:PostBackTrigger ControlID="gvWPRHeader" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="div" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="" id="divInput" runat="server">
                                        <div id="divInterNational" runat="server" visible="true">
                                            <div class="col-sm-12 p-t-10 p-l-0 p-r-0">
                                                <table align="center" id="tbl_customerDetails" class="tr_class table" style="text-transform: none;">
                                                    <tbody>
                                                        <tr class="eqheading">
                                                            <td>
                                                                <div class="offerFormHeading1">
                                                                    Quality Details
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span class="eqdetailslabel">Customer Name</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtCustomerName" CssClass="form-control mandatoryfield"
                                                                    runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span class="eqdetailslabel">RFP No</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtRFPNo" CssClass="form-control mandatoryfield"
                                                                    runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span class="eqdetailslabel">Customer PO No</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtCustomerPONo" CssClass="form-control mandatoryfield"
                                                                    runat="server"></asp:TextBox>
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
                                                                <span id="Span13" class="eqdetailslabel">Technique Used </span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtTechniqueUsed" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="label" class="eqdetailslabel">Project Name</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtProjectName" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span17" class="eqdetailslabel">ITP No </span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtITPNo" CssClass="form-control" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td align="left">
                                                                <span id="span18" class="eqdetailslabel">Size </span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtSize" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <span id="Span19" class="eqdetailslabel">Quantity </span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtQuantity" CssClass="form-control" runat="server"></asp:TextBox>
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

                                                                <div id="divVERReport" runat="server">
                                                                    <div class="col-sm-12 p-t-10" runat="server">
                                                                        <asp:GridView ID="gvWeldPlanDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                                            CssClass="table table-hover table-bordered medium" EmptyDataText="No Records Found"
                                                                            Style="overflow: scroll;" OnRowCreated="gvWeldPlanDetails_OnRowCreated" DataKeyNames="WPRDID,WPRHID">
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="" ItemStyle-Width="2%">
                                                                                    <HeaderTemplate>
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkitems" runat="server" AutoPostBack="false" onclick="return MakeMandatory(this);" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <%--  <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                                                                                    HeaderStyle-HorizontalAlign="Center">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'
                                                                                            Style="text-align: center"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>--%>
                                                                                <asp:TemplateField HeaderText="Joint Type">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtJointType" runat="server" CssClass="form-control" Text='<%# Eval("JointType")%>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Part 1">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtBaseMetalThickPart1" runat="server" CssClass="form-control" Text='<%# Eval("BMThicknessPart1")%>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Part 2">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtbaseMetalthickpart2" CssClass="form-control" runat="server" Text='<%# Eval("BMThicknessPart2")%>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Part 1">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtBaseMetalSpecificationPart1" runat="server" CssClass="form-control" Text='<%# Eval("BMSpecificationPart1")%>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="P.No" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtBaseMetalSpecificationPNo1" runat="server" CssClass="form-control" Text='<%# Eval("BMSpecificationPNo1")%>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Part 2">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtBaseMetalSpecificationPart2" CssClass="form-control" runat="server" Text='<%# Eval("BMSpecificationPart2")%>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="P.No" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtBaseMetalSpecificationPNo2" runat="server" CssClass="form-control" Text='<%# Eval("BMSpecificationPNo2")%>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Part No to Part No">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtPartNotoPartNo" runat="server" CssClass="form-control" Text='<%# Eval("PartNoToPartNo")%>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="WPS">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtWPSNo" runat="server" CssClass="form-control" Text='<%# Eval("WPSNo")%>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Process">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtProcess" runat="server" CssClass="form-control" Text='<%# Eval("Process")%>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control" Text='<%# Eval("Remarks")%>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-12 text-center p-t-10">
                                        <asp:LinkButton ID="btnSaveWPR" runat="server" CssClass="btn btn-cons btn-save AlignTop"
                                            Text="Save" OnClientClick="return Validate();" OnClick="btnSaveWPR_Click">
                                        </asp:LinkButton>
                                    </div>
                                    <div id="divOutPut" class="col-sm-12 p-t-10" style="overflow-x: scroll;" runat="server">
                                        <asp:GridView ID="gvWPRHeader" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                            OnRowCommand="gvWPRHeader_OnRowCommand" DataKeyNames="WPRHID">
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
                                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditWPR" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                         <img src="../Assets/images/edit-ec.png" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnAdd" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("WPRHID")) %>'>
                                                         <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PDF" ItemStyle-CssClass="text-center" HeaderStyle-Width="10%"
                                                    ItemStyle-Width="10%" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnpdf" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            CommandName="WPRPDF"><img src="../Assets/images/pdf.png" />
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:HiddenField ID="hdnWPRHID" runat="server" Value="0" />
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


    <div id="divLPIReport_p" runat="server" style="display: none;">
        <div id="divmainContent_p" runat="server">
            <div class="row p-t-10">
                <div class="col-sm-4 text-center">
                    <label>RFP No</label>
                    <asp:Label ID="lblRFPNo_p" runat="server"></asp:Label>
                </div>
                <div class="col-sm-4 text-center">
                    <label>Report No</label>
                    <asp:Label ID="lblReportNo_p" runat="server"></asp:Label>
                </div>
                <div class="col-sm-4 text-center">
                    <label>Date</label>
                    <asp:Label ID="lblDate_p" runat="server"></asp:Label>
                </div>
            </div>
            <div class="p-t-10 text-center">
                <label style="font-size: 15px ! important;">WELD PLAN  REPORT </label>
            </div>

            <div class="divtable p-t-10">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <label>Customer Name  </label>
                        </td>
                        <td>
                            <asp:Label ID="lblCustomername_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label>QAP No  </label>
                        </td>
                        <td>
                            <asp:Label ID="lblQAPNo_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Project  </label>
                        </td>
                        <td>
                            <asp:Label ID="lblProject_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label>Size  </label>
                        </td>
                        <td>
                            <asp:Label ID="lblSize_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Quantity  </label>
                        </td>
                        <td>
                            <asp:Label ID="lblQuantity_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label>Drawing No  </label>
                        </td>
                        <td>
                            <asp:Label ID="lblDrawingNo_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>PONo  </label>
                        </td>
                        <td>
                            <asp:Label ID="lblPoNo_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label>Item Name  </label>
                        </td>
                        <td>
                            <asp:Label ID="lblItemname_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>

        <div class="p-t-10">
            <div class="p-t-10" runat="server">
                <asp:GridView ID="gvWeldPlanDetails_p" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                    CssClass="table table-hover table-bordered medium" EmptyDataText="No Records Found" DataKeyNames="WPRDID">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'
                                    Style="text-align: center"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Joint Type">
                            <ItemTemplate>
                                <asp:Label ID="txtJointType" runat="server" Text='<%# Eval("JointType")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Part 1">
                            <ItemTemplate>
                                <asp:Label ID="txtBaseMetalThickPart1" runat="server" Text='<%# Eval("BMThicknessPart1")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Part 2">
                            <ItemTemplate>
                                <asp:Label ID="txtbaseMetalthickpart2" runat="server" Text='<%# Eval("BMThicknessPart2")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Part 1">
                            <ItemTemplate>
                                <asp:Label ID="txtBaseMetalSpecificationPart1" runat="server" Text='<%# Eval("BMSpecificationPart1")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="P.No" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="txtBaseMetalSpecificationPNo1" runat="server" Text='<%# Eval("BMSpecificationPNo1")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Part 2">
                            <ItemTemplate>
                                <asp:Label ID="txtBaseMetalSpecificationPart2" runat="server" Text='<%# Eval("BMSpecificationPart2")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="P.No" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="txtBaseMetalSpecificationPNo2" runat="server" Text='<%# Eval("BMSpecificationPNo2")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Part No to Part No" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="txtPartNotoPartNo" runat="server" Text='<%# Eval("PartNoToPartNo")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="WPS">
                            <ItemTemplate>
                                <asp:Label ID="txtWPSNo" runat="server" Text='<%# Eval("WPSNo")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Process" HeaderStyle-Width="5%" ItemStyle-Width="5%">
                            <ItemTemplate>
                                <asp:Label ID="txtProcess" runat="server" Text='<%# Eval("Process")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks" HeaderStyle-Width="15%" ItemStyle-Width="15%">
                            <ItemTemplate>
                                <asp:Label ID="txtRemarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <div id="divbelowcontent_p" runat="server">
            <div>
                <asp:Label ID="lblRemarks_p" runat="server"></asp:Label>
            </div>
        </div>
        <div id="divfootercontent_p">
            <div class="row" style="padding-bottom: 10mm;">
                <div class="col-sm-6 p-l-10">
                    <label>Prepared By Weld Engineer </label>
                </div>
                <div class="col-sm-6 p-r-10" style="text-align: right;">
                    <label>Approved By Quality Incharge </label>
                </div>
            </div>
        </div>

    </div>
</asp:Content>

