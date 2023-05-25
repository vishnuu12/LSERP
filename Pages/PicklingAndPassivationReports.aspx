<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="PicklingAndPassivationReports.aspx.cs" Inherits="Pages_PicklingAndPassivationReports" ClientIDMode="Predictable" %>

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

        //Address 
        //PhoneAndFaxNo
        //Email
        //WebSite

        function PrintPicklingAndPassivationReport() {
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
            winprint.document.write("<style type='text/css'/>  .InsideTable{  width: 60% ! important;margin-bottom: 10px;margin-left: 50px; } .InsideTable tr { height:3mm ! important; } </style>");

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
            winprint.document.write("RESULT:" + $('#ContentPlaceHolder1_lblResult_p').text());
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
                                    <h3 class="page-title-head d-inline-block">Pickling And Passivation Report</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Quality Test Reports</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Pickling And Passivation Report</li>
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
                <asp:PostBackTrigger ControlID="gvPAPRHeader" />
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
                                    <div id="divWall" runat="server">
                                        <div class="col-sm-12 p-t-10 p-l-0 p-r-0">
                                            <table align="center" id="tbl_customerDetails" class="tr_class table" style="text-transform: none;">
                                                <tbody>
                                                    <tr class="eqheading">
                                                        <td>
                                                            <div class="offerFormHeading1">
                                                                Pickling And Passivation Reports 
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="lblIV" class="eqdetailslabel">Item Name</span>
                                                        </td>
                                                        <%--OnSelectedIndexChanged="ddlItemName_SelectIndexChanged"--%>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlItemName" runat="server" AutoPostBack="false"
                                                                CssClass="form-control" Width="70%" ToolTip="Select Item No">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td align="left">
                                                            <span id="Span5" class="eqdetailslabel">If Specify Item Name</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtItemName" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
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
                                                            <span id="Span13" class="eqdetailslabel">Customer Name</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtCustomername" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
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
                                                            <span id="lblTestDate" class="eqdetailslabel">PO No</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtPONo" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <span id="Span14" class="eqdetailslabel">Project</span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtProjectName" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
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
                                                            <span id="Span16" class="eqdetailslabel">ITP No </span>
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtITPNo" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td align="left"><span id="Span17" class="eqdetailslabel">PECPL TDC No </span></td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtPECPLTDCNo" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>Work Order No </label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtWorkorderNo" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <label>Part </label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtPart" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>Size </label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSize" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <label>DRG Ref No </label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDRGRefNo" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label>BSL No </label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtBSLNo" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <label>QTY </label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtQTY" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                </tbody>
                                            </table>
                                        </div>

                                        <div class="col-sm-12 p-t-10">
                                            <label>Pre-Cleaning </label>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <label>Date: </label>
                                                <asp:TextBox ID="txtDatePC" CssClass="form-control mandatoryfield datepicker"
                                                    runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                                <label>Start Time: </label>
                                                <asp:TextBox ID="txtStartTimePC" CssClass="form-control mandatoryfield"
                                                    runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                                <label>Completion Time: </label>
                                                <asp:TextBox ID="txtCompletionTimePC" CssClass="form-control mandatoryfield"
                                                    runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <label>Procedure And Results </label>
                                            </div>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtProcedureAndResultsPC" CssClass="form-control mandatoryfield"
                                                    runat="server"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-sm-12 p-t-10">
                                            <label>Acid Pickling-Cleaning </label>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <label>Date: </label>
                                                <asp:TextBox ID="txtDateAPC" CssClass="form-control mandatoryfield datepicker"
                                                    runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                                <label>Start Time: </label>
                                                <asp:TextBox ID="txtstarttimeAPC" CssClass="form-control mandatoryfield"
                                                    runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                                <label>Completion Time: </label>
                                                <asp:TextBox ID="txtCompletiontimeAPC" CssClass="form-control mandatoryfield"
                                                    runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <label>Methodology </label>
                                            </div>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtMethodologyAPC" CssClass="form-control mandatoryfield"
                                                    runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <label>Procedure And Results </label>
                                            </div>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtProcedureandresultsAPC" CssClass="form-control mandatoryfield"
                                                    runat="server"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-sm-12 p-t-10">
                                            <label>Passivation And Cleaning </label>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <label>Date: </label>
                                                <asp:TextBox ID="txtDatePAC" CssClass="form-control mandatoryfield datepicker"
                                                    runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                                <label>Start Time: </label>
                                                <asp:TextBox ID="txtStarttimePAC" CssClass="form-control mandatoryfield"
                                                    runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                                <label>Completion Time: </label>
                                                <asp:TextBox ID="txtCompletiontimePAC" CssClass="form-control mandatoryfield"
                                                    runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <label>Methodology </label>
                                            </div>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtMethodologyPAC" CssClass="form-control mandatoryfield"
                                                    runat="server"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <label>Procedure And Results </label>
                                            </div>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtProcedureAndResultsPAC" CssClass="form-control mandatoryfield"
                                                    runat="server"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <label>Results </label>
                                            </div>
                                            <div class="col-sm-8">
                                                <asp:TextBox ID="txtResults" CssClass="form-control mandatoryfield"
                                                    runat="server"></asp:TextBox>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="col-sm-12 text-center p-t-10">
                                        <asp:LinkButton ID="btnSavePPR" runat="server" CssClass="btn btn-cons btn-save AlignTop"
                                            Text="Save" OnClientClick="return Validate();" OnClick="btnSavePPR_Click">
                                        </asp:LinkButton>
                                    </div>

                                    <div id="divOutPut" class="col-sm-12 p-t-10" style="overflow-x: scroll;" runat="server">
                                        <asp:GridView ID="gvPAPRHeader" runat="server" AutoGenerateColumns="False"
                                            OnRowCommand="gvPAPRHeader_OnRowCommand" ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                            DataKeyNames="PPRHID">
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
                                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditPTR" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                         <img src="../Assets/images/edit-ec.png" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btndelete" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("PPRHID")) %>'>
                                                         <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PDF" ItemStyle-CssClass="text-center" HeaderStyle-Width="10%"
                                                    ItemStyle-Width="10%" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnpdf" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            CommandName="PdfPTR"><img src="../Assets/images/pdf.png" />
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:HiddenField ID="hdnPPRHID" runat="server" Value="0" />
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
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div id="divPicklingAndPassivationReports_p" runat="server" style="display: none;">
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
                <label style="font-size: 24px !important;">
                    Inspection report For Pickling And Passivation Of SS Parts                   
                </label>
            </div>
            <div class="divtable p-t-10">
                <table style="width: 100%;">
                    <tr>
                        <td colspan="2" class="text-center">
                            <asp:Label ID="lblcustomername_p" Style="font-weight: bold; font-size: 15px ! important;" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>PO No </label>
                            <asp:Label ID="lblPONo_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label>QAP No </label>
                            <asp:Label ID="lblQAPNo_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>PECPL TDC No </label>
                            <asp:Label ID="lblPECPLTDCNo_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label>Work Order No </label>
                            <asp:Label ID="lblWorkOrderNo_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="text-center p-t-10">
                            <asp:Label ID="lblItemName_p" runat="server"></asp:Label>
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
                                    <asp:TemplateField HeaderText="Part" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                        HeaderStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPart" runat="server" Text='<%# Eval("Part")%>'></asp:Label>
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
                </table>
            </div>
            <div class="divtable p-t-10">
                <table style="width: 100%;">
                    <tr>
                        <td>
                            <label>Stages </label>
                        </td>
                        <td>
                            <label>Activities </label>
                        </td>
                        <td>
                            <label>Procedure And Results </label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Pre - Cleaning </label>
                        </td>
                        <td>
                            <table style="width: 100%;" class="InsideTable">
                                <tr>
                                    <td>
                                        <label>Date </label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDatePC_p" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Start Time </label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblStarttimePC_p" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Completion Time </label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCompletionTimePC_p" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <asp:Label ID="lblProcedureAndResultsPC_p" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <label>Acid Pickling And Cleaning </label>
                        </td>
                        <td>
                            <table style="width: 100%;" class="InsideTable">
                                <tr>
                                    <td>
                                        <label>Date </label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDateAPC_p" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Start Time </label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblStarttimeAPC_p" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Completion Time </label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCompletionTimeAPC_p" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <label>
                                Methodology
                            </label>
                            <asp:Label ID="lblMethodologyAPC_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblProcedureAndResultsAPC_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>Passivation And  Cleaning </label>
                        </td>
                        <td>
                            <table style="width: 100%;" class="InsideTable">
                                <tr>
                                    <td>
                                        <label>Date </label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDatePAC_p" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Start Time </label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblStarttimePAC_p" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>Completion Time </label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCompletionTimePAC_p" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <label>
                                Methodology
                            </label>
                            <asp:Label ID="lblMethodologyPAC_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblProcedureAndResultsPAC_p" runat="server"></asp:Label>
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
                    <label>For Lonestar Industries</label>
                </div>
                <div class="col-sm-6 p-r-10" style="text-align: right;">
                    <label>For Authorized Inspector</label>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
