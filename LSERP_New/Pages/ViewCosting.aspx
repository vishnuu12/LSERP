<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="ViewCosting.aspx.cs" Inherits="Pages_ViewCosting" ClientIDMode="Predictable"
    EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function ShowViewPopUP() {
            $('#mpeView').modal({
                backdrop: 'static'
            });
        }

        function ShowViewdocsPopUp() {
            $('#mpeViewdocs').modal({
                backdrop: 'static'
            });
        }

        function UpdateSharedWithHODStatus() {
            swal({
                title: "No Further Edit Once Shared.",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Save it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('updateSharedWithHODStatus', null);
            });
            return false;
        }

        function ShowHideDataTableSearch(flag) {
            if (flag == "hide") {
                $('#ContentPlaceHolder1_gvItemCostingDetails_filter').hide();
            }
            else {
                $('#ContentPlaceHolder1_gvItemCostingDetails_filter').show();
            }
            return true;
        }

        function checkAll(ele) {
            debugger;
            if ($(ele).is(":checked")) {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', true);
            }
            else {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', false);
            }
        }

        function ValidateAll() {
            var checkbox = $('#ContentPlaceHolder1_gvViewCosting').find('[type="checkbox"]').not('#ContentPlaceHolder1_gvViewCosting_chkall').length;
            var checked = $('#ContentPlaceHolder1_gvViewCosting').find('input:checked').not('#ContentPlaceHolder1_gvViewCosting_chkall').length;
            if (checkbox != 0 && checked != 0) {
                if (parseInt(checkbox) == parseInt(checked)) {
                    return true;
                }
                else {
                    ErrorMessage('Error', 'Please Select All');
                    return false;
                }
            }
            else {
                InfoMessage('Information', 'Currently All Item Shared');
                return false;
            }
        }

        function MandatoryField(ele) {
            if ($(ele).is(":checked")) {
                $(ele).closest('tr').find('[type="text"]').addClass("mandatoryfield");
                $(ele).closest('tr').find('[type="text"]').before('<span class="textboxmandatory" style="color: red;">*</span>');
            }
            else {
                $(ele).closest('tr').find('[type="text"]').removeClass("mandatoryfield");
                $(ele).closest('tr').find('[type="text"]').closest('td').find('.textboxmandatory').remove()
            }
        }

        function ViewFileName(RowIndex) {
            __doPostBack("ViewDrawing", RowIndex);
        }

        function ViewCostingprint(epstyleurl, Main, QrCode) {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var RowLength = $('#ContentPlaceHolder1_gvItemCostingDetails_pdf').find('tr').length - 1;

            var PageLength = parseInt(RowLength / 50);

            var CostingHeader = $('#ContentPlaceHolder1_divcostingheader').html();
            var divcostbottomcontent = $('#ContentPlaceHolder1_divcostbottomcontent').html();

            var k = 1;
            var lastrecord = false;
            winprint.document.write("<html><head><title>");
            winprint.document.write("Offer");
            winprint.document.write("</title>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='" + epstyleurl + "'/>");
            winprint.document.write("<link rel='stylesheet' href='" + Main + "' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet'  href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/printg.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<style type='text/css'>@media print,screen { label,table th{ font-weight: bold; font-size: 15px !important;font-family:Times New Roman;color:#000 !important; } @page {size: landscape} } .row{ padding-top:10px; } .page_generateoffer{ margin: 6mm; } table th{ vertical-align: middle; } .page_generateoffer table td {font-size: 8px; font-weight: bold;color: #000;} .table th,.table td {  padding: 2px !important; } </style>");
            winprint.document.write("<style type='text/css'> .page_generateoffer table th { font-size: 8px ! important; font-weight: bold;color: #000 ! important;vertical-align: middle;text-align: center;  background: #fff !important;border: 1px solid #000 !important;}.page_generateoffer{margin-top: 0px !important} </style>");

            //winprint.document.write("<div class='print-page' style='position:fixed;width:200mm;height:287mm;margin-top:5mm;margin-bottom:5mm;margin-left:5mm;margin-right:5mm'></div>");
            winprint.document.write("<table style='width:287mm;height:200mm;margin-left:20px;border:0px !important'>");
            winprint.document.write("<thead><tr><td style='height: 90px;'>");

            winprint.document.write(CostingHeader);

            winprint.document.write("</td></tr></thead>");
            winprint.document.write("<tbody><tr><td>");

            //class='page_generateoffer'

            //   for (var i = 0; i <= PageLength; i++) {
            winprint.document.write("<div class='page_generateoffer'>");
            winprint.document.write("<div style='padding-right: 15px !important; padding-left: 0px !important'>")
            winprint.document.write("<table class='table table-hover medium' cellspacing='0' rules='all' border='1' id='ContentPlaceHolder1_gvItemCostingDetails_pdf' style='border-collapse:collapse;table-layout: fixed;width: 287mm;'>");
            winprint.document.write("<thead>");
            winprint.document.write($('#ContentPlaceHolder1_gvItemCostingDetails_pdf').find('tr:first').html());
            winprint.document.write("</thead>");
            winprint.document.write("<tbody>");

            for (var j = k; j < RowLength; j++) {
               // if (RowLength == j) {
                  //  winprint.document.write("<tr align='center' style='border:white;'>" + $('#ContentPlaceHolder1_gvItemCostingDetails_pdf').find('tr')[j].innerHTML + "</tr>");
                //    lastrecord = true;
                //    break;
               // }
             //   else
                    winprint.document.write("<tr align='center'>" + $('#ContentPlaceHolder1_gvItemCostingDetails_pdf').find('tr')[j].innerHTML + "</tr>");
            }

            winprint.document.write("</tbody>");
            winprint.document.write("</table>");
            winprint.document.write("</div>");

            //if (lastrecord) {
                winprint.document.write(divcostbottomcontent);
           // }
            winprint.document.write("</div>");

            //if (lastrecord) {
            //    break;
            //}
          //  k = k + 50;
            // }

            winprint.document.write("</td></tr></tbody>");
            winprint.document.write("<tfoot><tr><td>");
            winprint.document.write("<div style='height:25mm;'>");
            winprint.document.write("<div class='row' style='margin-bottom:20px;position:fixed;width:200mm;bottom:0px;'>");
            winprint.document.write("<img  class='Qrcode' style='height:80%;' src='" + QrCode + "' />");
            winprint.document.write("</div>");
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

    </script>

    <style type="text/css">
        table#gvItemCostingDetails_pdf tr td {
            font-weight: bold;
            color: black;
        }

        table#gvAddtionalPartDetails_pdf tr td {
            font-weight: bold;
            color: black;
        }


        @media print {
            .table > tbody > tr > td, .table > tbody > tr > th, .table > tfoot > tr > td, .table > tfoot > tr > th, .table > thead > tr > td, .table > thead > tr > th {
                padding: 5px 3px !important;
            }
        }

        #ContentPlaceHolder1_gvItemCostingDetails {
            font-size: 12px;
            font-weight: bold;
            color: #000;
        }

        #ContentPlaceHolder1_gvAddtionalPartDetails {
            font-size: 12px;
            font-weight: bold;
            color: #000;
        }

        .radio-success label:nth-child(2) {
            color: red !important;
        }

        .radio-success label:nth-child(4) {
            color: #0acc0a !important;
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
                                    <h3 class="page-title-head d-inline-block">View Costing</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Design</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">View Costing</li>
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
            <asp:UpdatePanel ID="upQuote" runat="server" UpdateMode="Always">
                <Triggers>
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divradio" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label" style="padding-top: 7px;">
                                            Select Type
                                        </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:RadioButtonList ID="rblEnquiryChange" runat="server" CssClass="radio radio-success" RepeatDirection="Horizontal"
                                            AutoPostBack="true" OnSelectedIndexChanged="rblEnquiryChange_OnSelectedChanged" RepeatLayout="Flow">
                                            <asp:ListItem Selected="True" Value="0">PENDING</asp:ListItem>
                                            <asp:ListItem Value="1">COMPLETED</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divAdd" runat="server">
                            <div class="ip-div text-center">
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Customer Name</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomerName_SelectIndexChanged"
                                            CssClass="form-control" Width="70%" ToolTip="Select Customer Number">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Enquiry Number</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlEnquiryNumber" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEnquiryNumber_SelectIndexChanged"
                                            CssClass="form-control" Width="70%" ToolTip="Select Enquiry Number">
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
                                        <div class="row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                                    <asp:Label ID="lbltitle" runat="server"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>
                                        <%-- <div class="col-sm-12 p-t-10 text-center">
                                            <label class="form-label" style="color: Blue;">
                                                View Costing
                                            </label>
                                        </div>--%>
                                        <div class="col-sm-12">
                                            <div class="col-sm-4">
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:LinkButton ID="btnShareBOM" CssClass="btn btn-success" runat="server" Text="Share With HOD"
                                                    OnClientClick="return ValidateAll();" OnClick="btnShareBOM_Click"></asp:LinkButton>
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvViewCosting" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium pagingfalse"
                                                OnRowCommand="gvViewCosting_OnRowCommand" OnRowDataBound="gvViewCosting_OnRowDataBound"
                                                HeaderStyle-HorizontalAlign="Center" DataKeyNames="DDID,Filename,CostEstimatedBy,CostEstimatedDate,OverAllLength,LseNumber,DrawingName">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" ControlStyle-CssClass="nosort" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkall" runat="server" onclick="return checkAll(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkitems" runat="server" onclick="return MandatoryField(this);"
                                                                AutoPostBack="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View Costing" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnView" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="ViewCosting"><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ItemName" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cost Estimated Status" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCostEstimationStatus" runat="server" Text='<%# Eval("CostEstimated")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Drawing Number" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDeviationFile" runat="server" OnClientClick='<%# string.Format("return ViewFileName({0});",((GridViewRow) Container).RowIndex) %>'>
                                                                <asp:Label ID="lblDrawingNumber" runat="server" Text='<%# Eval("DrawingName")%>'></asp:Label>
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tag Number/Item/Material Code" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemTagNumber" runat="server" Text='<%# Eval("ItemTagNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit Bom Cost" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBOMCost" runat="server" Text='<%# Eval("UnitBomCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Addtional Part Cost" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAddtionalPartCost" runat="server" Text='<%# Eval("AdditionalPartBomCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="FIM Cost" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFIMCost" runat="server" Text='<%# Eval("IssuePartBomCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Quantity")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="HOD Status" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDesignHStatus" runat="server" Text='<%# Eval("DesignHApprovalStatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="HOD Remarks" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDesignHRemarks" runat="server" Text='<%# Eval("DesignHODRemarks")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Dead Inventory Amount" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtDeadInventoryAmount" runat="server" onkeypress="return validationNumeric(this);"
                                                                CssClass="form-control" Text='<%# Eval("DeadInventoryAmount")%>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Dead Inventory Remarks" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtDeadInventoryRemarks" CssClass="form-control" runat="server"
                                                                Text='<%# Eval("DeadInventoryRemarks")%>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Cost" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalCost" runat="server" Text='<%# Eval("TotalCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hdnBOMId" Value="0" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <div>
                <div class="col-sm-12">
                </div>
            </div>
        </div>
    </div>
    <div class="modal" id="mpeView">
        <div class="modal-dialog" style="max-width: 95%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btndownloaddocs" />
                        <asp:PostBackTrigger ControlID="lbtnDeviationFile" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H2">
                                <div class="text-center">
                                    <asp:Label ID="Label1" Text="" runat="server"></asp:Label>
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal">
                                x</button>
                        </div>
                        <div class="modal-body">
                            <div class="inner-container">
                                <div class="col-sm-12 text-center">
                                    <asp:LinkButton ID="btndownloaddocs" OnClientClick="return ShowHideDataTableSearch('hide');"
                                        OnClick="btndownloaddocs_Click" ToolTip="PDF DownLoad" runat="server">
                                                        <img src="../Assets/images/pdf.png" /> </asp:LinkButton>
                                </div>
                                <div id="divViewCostingPrint_p" runat="server">
                                    <div class="col-sm-12 p-t-10 text-center">
                                        <asp:Label ID="lblCustomerName_p" runat="server">                                        
                                        </asp:Label>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-3 p-t-10">
                                            <asp:Label ID="lblItemname_p" runat="server">                                        
                                            </asp:Label>
                                        </div>
                                        <div class="col-sm-3 p-t-10">
                                            <label>
                                                Drawing Name:</label>
                                            <asp:LinkButton ID="lbtnDeviationFile" runat="server" OnClick="btnViewDrawingFile">
                                                <asp:Label ID="lblDrawingname_p" runat="server"></asp:Label>
                                            </asp:LinkButton>
                                        </div>
                                        <div class="col-sm-3 p-t-10">
                                            <label>
                                                Tag No:</label>
                                            <asp:Label ID="lblTagNo_p" runat="server">                                        
                                            </asp:Label>
                                        </div>
                                        <div class="col-sm-3 p-t-10">
                                            <label>
                                                Total Quantity:</label>
                                            <asp:Label ID="lblTotalQty_p" runat="server">                                        
                                            </asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12" style="overflow-x: auto;">
                                        <asp:GridView ID="gvItemCostingDetails" runat="server" AutoGenerateColumns="true"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                            HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:Label ID="lblBOMCost" Text="" runat="server" Style="color: darkgreen; font-size: x-large; font-family: fantasy;"></asp:Label>
                                    </div>
                                    <div class="col-sm-12 p-t-10 text-center">
                                        <label class="form-label" style="color: Blue;">
                                            FIM Cost Details
                                        </label>
                                    </div>
                                    <div class="col-sm-12 p-t-10" id="IssuePartiv" style="overflow-x: scroll;">
                                        <asp:GridView ID="gvIssuePartDetails" runat="server" AutoGenerateColumns="true"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium orderingfalse"
                                            HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:Label ID="lblIssuePartCost" Text="" runat="server" Style="color: darkgreen; font-size: x-large; font-family: fantasy;"></asp:Label>
                                    </div>
                                    
                                    <div class="col-sm-12 p-t-10 text-center">
                                        <label class="form-label" style="color: Blue;">
                                            Addtional Part BOM Cost Details
                                        </label>
                                    </div>
                                    <div class="col-sm-12 p-t-10" id="AddtionalPartiv" style="overflow-x: scroll;">
                                        <asp:GridView ID="gvAddtionalPartDetails" runat="server" AutoGenerateColumns="true"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium orderingfalse"
                                            HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:Label ID="lblAddtionalPartCost" Text="" runat="server" Style="color: darkgreen; font-size: x-large; font-family: fantasy;"></asp:Label>
                                    </div>
                                    <div class="col-sm-12 text-center">
                                        <asp:Label ID="lblTotalBOMCost" Text="" runat="server" Style="color: brown; font-size: x-large; font-family: fantasy;"></asp:Label>
                                    </div>
                                    <div class="col-sm-12">
                                        <label>
                                            Over All Length:</label>
                                        <asp:Label ID="lblOverAllLength_p" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-12">
                                        <label>
                                            LSE Number:</label>
                                        <asp:Label ID="lblLSENumber_p" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-12">
                                        <label>
                                            Dead Inventory Remarks:</label>
                                        <asp:Label ID="lblDeadInventoryRemarks_p" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-12">
                                        <label>
                                            Remarks:</label>
                                        <asp:Label ID="lblRemarks_p" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-12">
                                        <label>
                                            Date:</label>
                                        <asp:Label ID="lblDate_p" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-12">
                                        <label>
                                            User Name:</label>
                                        <asp:Label ID="lblUserName_p" runat="server"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:Image ID="imgQrcode" class="Qrcode" ImageUrl="" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                        </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:HiddenField ID="HiddenField1" runat="server" Value="" />
            </div>
        </div>
    </div>
    <div class="modal" id="mpeViewdocs" style="height: 125%; top: -10%;">
        <div class="modal-dialog" style="height: 100%;">
            <div class="modal-content" style="height: 100%;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always" Style="height: 100%;">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btndownloaddocs" />
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
    <div class="modal" id="mpeCosting_pdf">
        <div class="modal-dialog" style="max-width: 95%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H3">
                                <div class="text-center">
                                    <asp:Label ID="Label3" Text="" runat="server"></asp:Label>
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal">
                                x</button>
                        </div>
                        <div class="modal-body">
                            <div class="inner-container">
                                <div id="divViewCostingPrint_pdf" runat="server" style="float: left; width: 100%; margin-right: 10px;">
                                    <div id="divcostingheader" runat="server">
                                        <div class="row text-center" style="padding-left: 0 !important; padding-right: 0px !important">
                                        </div>
                                        <div class="row p-t-10 text-center" style="margin-left: 40%;">
                                            <asp:Label ID="lblCustomerName_pdf" runat="server" Style="font-size: 20px !important; font-weight: 700">                                        
                                            </asp:Label>
                                        </div>
                                        <div class="row p-t-10 text-center" style="display: flex; align-items: center; justify-content: space-around">
                                            <asp:Label ID="lblItemname_pdf" runat="server" Style="font-size: 14px !important; font-weight: 700 !important">                                        
                                            </asp:Label>
                                            <asp:Label ID="lblDrawingname_pdf" runat="server">                                        
                                            </asp:Label>
                                            <label>
                                                Tag No:</label>
                                            <asp:Label ID="lblTagNo_pdf" runat="server">                                        
                                            </asp:Label>
                                            <label>
                                                Total Quantity:</label>
                                            <asp:Label ID="lblTotalQty_pdf" runat="server">                                        
                                            </asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12" style="padding-right: 15px !important; padding-left: 0px !important">
                                        <asp:GridView ID="gvItemCostingDetails_pdf" runat="server" AutoGenerateColumns="true"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium table-bold"
                                            OnRowDataBound="gvItemCostingDetails_pdf_OnRowDataBound" OnDataBound="gvItemCostingDetails_pdf_OnDataBound" Style="font-size: 9px; width: 100% !important" HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <%--<div class="col-sm-12 p-t-10 text-center">
                                        <label class="form-label" style="color: Blue;">
                                            Addtional Part BOM Cost Details
                                        </label>
                                    </div>
                                    <div class="col-sm-12 p-t-10" id="AddtionalPartiv_pdf">
                                        <asp:GridView ID="gvAddtionalPartDetails_pdf" runat="server" AutoGenerateColumns="true"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium table-bold"
                                            Style="font-size: 9px; width: auto !important" HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:Label ID="lblAddtionalPartCost_pdf" Text="" runat="server" Style="color: darkgreen; font-size: x-large; font-family: fantasy;"></asp:Label>
                                    </div>--%>
                                    <div id="divcostbottomcontent" runat="server">
                                        <%--<div class="row text-center" style="padding-right: 15px">
                                            <asp:Label ID="lblTotalBOMCost_pdf" Text="" runat="server" Style="color: brown; font-size: x-large; float: right; font-family: fantasy;"></asp:Label>
                                        </div>--%>
                                        <div class="row">
                                            <label style="font-weight: 700; width: 16%;">
                                                Over All Length:</label>
                                            <asp:Label ID="lblOverAllLength_pdf" runat="server" Style="font-weight: 700; width: 20%;"></asp:Label>
                                            <asp:Label ID="lblBOMCost_pdf" Text="" runat="server" Style="color: darkgreen !important; font-size: x-large; float: right; font-family: fantasy; text-align: end; width: 62%; font-size: 16px ! important;"></asp:Label>
                                        </div>
                                        <div class="row">
                                            <label style="font-weight: 700; width: 16%;">
                                                LSE Number:</label>
                                            <asp:Label ID="lblLSENumber_pdf" runat="server" Style="font-weight: 700; width: 20%;"></asp:Label>
                                            <asp:Label ID="lblIssuePartCost_pdf" Text="" runat="server" Style="color: darkgreen !important; font-size: x-large; font-family: fantasy; text-align: end; width: 62%; font-size: 16px ! important;"></asp:Label>
                                        </div>
                                        <div class="row">
                                            <label style="font-weight: 700; width: 16%;">
                                                Dead Inventory Remarks:</label>
                                            <asp:Label ID="lblDeadInventoryRemarks_pdf" runat="server" Style="font-weight: 700; width: 20%;"></asp:Label>
                                            <asp:Label ID="lblTotalBOMCost_pdf" Text="" runat="server" Style="color: brown !important; font-size: x-large; font-family: fantasy; font-size: 16px ! important;"></asp:Label>
                                            <asp:Label ID="lblAddtionalPartCost_pdf" Text="" runat="server" Style="color: darkgreen !important; font-size: x-large; float: right; font-family: fantasy; text-align: end; width: 47%; font-size: 16px ! important;"></asp:Label>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <label style="font-weight: 700">
                                                    Remarks:</label>
                                                <asp:Label ID="lblRemarks_pdf" runat="server" Style="font-weight: 700"></asp:Label>
                                            </div>
                                            <div class="col-sm-6" style="padding-right: 15px">
                                                <div class="col-sm-12 text-right">
                                                    <label style="font-weight: 700">
                                                        Date:</label>
                                                    <asp:Label ID="lblDate_pdf" runat="server" Style="font-weight: 700"></asp:Label>
                                                </div>
                                                <div class="col-sm-12 text-right">
                                                    <label style="font-weight: 700">
                                                        User Name:</label>
                                                    <asp:Label ID="lblUserName_pdf" runat="server" Style="font-weight: 700"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:HiddenField ID="HiddenField2" runat="server" Value="" />
            </div>
        </div>
    </div>
</asp:Content>
