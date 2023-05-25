<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="VisualInspectionReport.aspx.cs"
    Inherits="Pages_VisualInspectionReport" ClientIDMode="Predictable" %>

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

        function Validate() {
            var res = true;
            var msg = Mandatorycheck('ContentPlaceHolder1_divInput');
            var Report = Mandatorycheck('ContentPlaceHolder1_divLPIReport');
            if (parseInt($('#ContentPlaceHolder1_gvVERDetails').find('[type="checkbox"]:checked').length) > 0) {
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

        function PrintVERReport() {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var RecordLength = $('#ContentPlaceHolder1_gvVERDetails_p').find('tr').length;

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
            winprint.document.write("<table class='table table-hover medium' cellspacing='0' rules='all' border='1' id='ContentPlaceHolder1_gvVERDetails_p' style='border-collapse:collapse;'>");
            winprint.document.write("<tbody>");

            for (var j = 0; j < RecordLength; j++) {
                winprint.document.write("<tr align='center'>" + $('#ContentPlaceHolder1_gvVERDetails_p').find('tr')[j].innerHTML + "</tr>");
            }

            //  k = k + 15;
            winprint.document.write("</tbody>");
            winprint.document.write("</table>");
            winprint.document.write("</div>");

            winprint.document.write("<div class='p-t-10'>");
            winprint.document.write("Remarks:" + $('#ContentPlaceHolder1_lblRemarks_p').text());
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
                                    <h3 class="page-title-head d-inline-block">Visual Inspection Report</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Quality</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Visual Inspection Report</li>
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
                    <asp:PostBackTrigger ControlID="gvVERDetails" />
                    <asp:PostBackTrigger ControlID="gvVEReportHeader" />
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
                                                                <span class="eqdetailslabel">Procedure </span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtProcedure" CssClass="form-control mandatoryfield"
                                                                    runat="server"></asp:TextBox>
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
                                                                <span id="lblPOOr" class="eqdetailslabel">Bellow S.No</span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtBellowSNo" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <span id="Span14" class="eqdetailslabel">Equipement Used </span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtEquipmentUsed" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
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
                                                                <span id="span16" class="eqdetailslabel">Lighting Equipement And Intensity </span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtLightingEquipment" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
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
                                                            <td align="left">
                                                                <span id="span20" class="eqdetailslabel">Surface Condition </span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtSurfaceCondition" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left"></td>
                                                            <td align="left"></td>
                                                        </tr>


                                                        <tr>
                                                            <td align="left">
                                                                <span id="span21" class="eqdetailslabel"> Other </span>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="txtOther" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td align="left"></td>
                                                            <td align="left"></td>
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
                                                                        <asp:GridView ID="gvVERDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                                            CssClass="table table-hover table-bordered medium" EmptyDataText="No Records Found"
                                                                            DataKeyNames="VERID,VERDID">
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
                                                                                <asp:TemplateField HeaderText="Part Location / Description">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtJointIdentification" runat="server" CssClass="form-control" Text='<%# Eval("JointIndentification")%>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Interpretation">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtInterPretation" CssClass="form-control" runat="server" Text='<%# Eval("InterPretation")%>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Result">
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtResult" CssClass="form-control" runat="server" Text='<%# Eval("Result")%>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </div>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td align="left">
                                                                <span id="span22" class="eqdetailslabel">Remarks </span>
                                                            </td>
                                                            <td align="left" colspan="2">
                                                                <asp:TextBox ID="txtRemarks" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                            </td>

                                                        </tr>

                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-sm-12 text-center p-t-10">
                                        <asp:LinkButton ID="btnSaveVER" runat="server" CssClass="btn btn-cons btn-save AlignTop"
                                            Text="Save" OnClientClick="return Validate();" OnClick="btnSaveVER_Click">
                                        </asp:LinkButton>
                                    </div>
                                    <div id="divOutPut" class="col-sm-12 p-t-10" style="overflow-x: scroll;" runat="server">
                                        <asp:GridView ID="gvVEReportHeader" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                            OnRowCommand="gvVEReportHeader_OnRowCommand" DataKeyNames="VERID">
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
                                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditVER" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                         <img src="../Assets/images/edit-ec.png" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnAdd" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("VERID")) %>'>
                                                         <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PDF" ItemStyle-CssClass="text-center" HeaderStyle-Width="10%"
                                                    ItemStyle-Width="10%" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnpdf" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            CommandName="VERPDF"><img src="../Assets/images/pdf.png" />
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:HiddenField ID="hdnVERID" runat="server" Value="0" />
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
                <label style="font-size: 15px ! important;">VISUAL  INSPECTION  REPORT </label>
            </div>

            <div class="divtable p-t-10">
                <table style="width: 100%;">
                    <tr>
                        <td style="width: 15%;">
                            <label>Customer Name  </label>
                        </td>
                        <td style="width: 40%;">
                            <asp:Label ID="lblcustomername_p" runat="server"></asp:Label>
                        </td>
                        <td style="width: 15%;">
                            <label>Procedure  </label>
                        </td>
                        <td style="width: 20%;">
                            <asp:Label ID="lblProcedure_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>PO No  </label>
                        </td>
                        <td>
                            <asp:Label ID="lblPONO_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label>Technique Used  </label>
                        </td>
                        <td>
                            <asp:Label ID="lblTechniqueUsed_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Project Name  </label>
                        </td>
                        <td>
                            <asp:Label ID="lblprojectname_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label>Equipement Used  </label>
                        </td>
                        <td>
                            <asp:Label ID="lblEquipmentused_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Drawing No  </label>
                        </td>
                        <td>
                            <asp:Label ID="lblDrawingNo_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label>Lighting Equipment And Intensity  </label>
                        </td>
                        <td>
                            <asp:Label ID="lblLightingequipmentAndIntensity_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>ITP No  </label>
                        </td>
                        <td>
                            <asp:Label ID="lblITPNo_p" runat="server"></asp:Label>
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
                            <label>Item Name</label>
                        </td>
                        <td>
                            <asp:Label ID="lblItemName_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label>Bellow SI No  </label>
                            <label>Quantity  </label>
                        </td>
                        <td>
                            <asp:Label ID="lblBellowSno_p" runat="server"></asp:Label>
                            <asp:Label ID="lblQuantity_p" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td colspan="2">
                            <label>Surface Condition  </label>
                        </td>

                        <td colspan="2">
                            <asp:Label ID="lblsurfacecondition_p" runat="server"></asp:Label>
                        </td>

                    </tr>

                </table>

            </div>

        </div>

        <div class="p-t-10">
            <div class="p-t-10" runat="server">
                <asp:GridView ID="gvVERDetails_p" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                    CssClass="table table-hover table-bordered medium" EmptyDataText="No Records Found" DataKeyNames="VERDID">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'
                                    Style="text-align: center"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Part Location And Description">
                            <ItemTemplate>
                                <asp:Label ID="lblPartLocationAndDescription" runat="server" Text='<%# Eval("JointIndentification")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Interpretation">
                            <ItemTemplate>
                                <asp:Label ID="lblInterpretation" runat="server" Text='<%# Eval("InterPretation")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Result">
                            <ItemTemplate>
                                <asp:Label ID="lblResult" runat="server" Text='<%# Eval("Result")%>'></asp:Label>
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
                    <label>For Lonestar Industries</label>
                </div>
                <div class="col-sm-6 p-r-10" style="text-align: right;">
                    <label>For Customer</label>
                </div>
            </div>
        </div>

    </div>


</asp:Content>

