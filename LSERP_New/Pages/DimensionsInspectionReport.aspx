<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="DimensionsInspectionReport.aspx.cs"
    Inherits="Pages_DimensionsInspectionReport" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function deleteConfirm(DIRDID) {
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
                __doPostBack('deleteDIRDID', DIRDID);
            });
            return false;
        }

        function checkAllItems(ele) {
            debugger;
            if ($(ele).is(":checked")) {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', true);
            }
            else {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', false);
            }
        }

        function SaveDimensionInspectionReport() {
            var RowLen = $('#tblDimension').find('tr').length;
            var Content;
            var BslContent;
            var BellowNoColumn = "";
            for (var i = 0; i < RowLen; i++) {
                Content = "";
                BslContent = "";
                BellowNoColumn = "";

                if ($('#tblDimension').find('#chkitems_' + i + '').is(":checked")) {
                    Content = $('#tblDimension').find('#txtDescription_' + i + '').val();
                    Content = Content + "#" + $('#tblDimension').find('#txtAsPerDrawing_' + i + '').val();

                    $('#tblDimension').find('th').each(function (index) {
                        if (index >= 3) {
                            var id = $(this).text().split(" ").join("");
                            id = id.replace(')', '').replace('(', '');
                            var BSLNo = $(this).text().split(" ").join("");
                            BSLNo = BSLNo.replace(')', '').replace('Actual-(', '');
                            if (BellowNoColumn == "") {
                                BellowNoColumn = BSLNo;
                                BslContent = $('#tblDimension').find('#' + 'txt' + id + '_' + i).val();
                            }
                            else {
                                BellowNoColumn = BellowNoColumn + "#" + BSLNo;
                                BslContent = BslContent + "#" + $('#tblDimension').find('#' + 'txt' + id + '_' + i).val();
                            }
                        }
                    });

                    jQuery.ajax({
                        type: "POST",
                        url: "DimensionsInspectionReport.aspx/SaveDimensionInspectionReport", //It calls our web method  
                        contentType: "application/json; charset=utf-8",
                        data: "{'Content':'" + Content + "','BellowNoColumn':'" + BellowNoColumn + "','BslContent':'" + BslContent + "','RFPHID':'" + $('#ContentPlaceHolder1_ddlRFPNo').val() + "','RFPDID':'" + $('#ContentPlaceHolder1_ddlItemName').val().split('/')[0] + "'}",
                        dataType: "JSON",
                        success: function (data) {
                            SuccessMessage('Success', 'Dimension Details Saved Succesfully');
                            document.getElementById('<%=btnGetDIR.ClientID %>').click();
                        },
                        error: function (d) {
                        }
                    });
                }
            }
        }

        function GetRows() {
            var Content;
            $('#tblDimension tr').remove();
            $('#tblDimension').append('<tr> <th><input id="tblDimensionChkAll_chkall" type="checkbox" onclick="return checkAllItems(this);"> </th> <th> Description </th> <th> As Per Drawing </th><tr>');
            var BsNoCount = $('#ContentPlaceHolder1_gvBellowSnoDetails').find('[type="checkbox"]:checked').length;
            $('#ContentPlaceHolder1_gvBellowSnoDetails').find('tr').not('tr:first').each(function (index) {
                if ($(this).find('[type="checkbox"]').is(":checked")) {
                    var BSLNo = $(this).find(".bellowsno").text();
                    $('#tblDimension').find('tr').append("<th> Actual - (" + BSLNo + ") </th>");
                }
            });
            $('#tblDimension').find('tr').not('tr:first').remove();

            for (var i = 0; i < 10; i++) {
                Content = "";
                Content = '<tr><td><input id="chkitems_' + i + '" type="checkbox"></td>';
                Content = Content + '<td><input type="text" id="txtDescription_' + i + '" class="form-control"></td>';
                Content = Content + '<td><input type="text" id="txtAsPerDrawing_' + i + '" class="form-control"></td>';

                $('#tblDimension').find('th').each(function (index) {
                    if (index >= 3) {
                        var BSLNo = $(this).text().split(" ").join("");
                        Content = Content + '<td><input type="text" id="txt' + BSLNo.replace(')', '').replace('(', '') + '_' + i + '" class="form-control"></td>';
                    }
                });
                Content = Content + "</tr>";
                $('#tblDimension').append(Content);
            }
            return false;
        }

        function PrintDIReport(Address, PhoneAndFaxNo, Email, WebSite) {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var Content = $('#divcontent_p').html();
            var RowLength = $('#ContentPlaceHolder1_gvDimensions_p').find('tr').length;

            winprint.document.write("<html><head><title>");
            winprint.document.write("Offer");
            winprint.document.write("</title>");
            // winprint.document.write("<link rel='stylesheet' type='text/css' href='" + epstyleurl + "'/>");
            // winprint.document.write("<link rel='stylesheet' href='" + Main + "' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet'  href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/printg.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<style type='text/css'>@media print,screen { label,table th{ font-weight: bold; font-size: 15px !important;font-family:Times New Roman;color:#000 !important; }} .row{ padding-top:10px; } .page_generateoffer{ margin: 6mm; } table th{ vertical-align: middle; }  </style>");

            winprint.document.write("<div class='print-page' style='position:fixed;width:200mm;height:287mm;margin-top:5mm;margin-bottom:5mm;margin-left:5mm;margin-right:5mm'></div>");
            winprint.document.write("<table style='width:200mm;height:287mm;margin-left:20px;border:0px !important'><thead><tr><td>");

            winprint.document.write("<div class='col-sm-12 print-generateoffer' style='border-bottom:1px solid #000;text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;height:102px'>");
            winprint.document.write("<div>");
            winprint.document.write("<div>");
            winprint.document.write("<div class='col-sm-12 padding0' id='divLSERPLogo' style='margin:20px auto;display:block;padding:5px 0px;'>");
            winprint.document.write("<img src='<%= ResolveUrl("../Assets/images/topstrrip.jpg") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' alt='' height='100px;' style='object-fit:contain;width:100%;display:none'>");
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

            winprint.document.write("<div class='page_generateoffer'>");
            winprint.document.write(Content);

            ////var i = 0;
            ////var k = 0;
            ////winprint.document.write("<div class='col-sm-12 p-t-10'>");
            ////winprint.document.write("<table class='table table-hover medium' cellspacing='0' rules='all' border='1' id='ContentPlaceHolder1_gvDimensions_p_" + i + "' style='border-collapse:collapse;'>");
            ////winprint.document.write("<tbody>");

            ////for (var j = k; j < k + 15; j++) {
            ////    winprint.document.write("<tr align='center'>" + $('#ContentPlaceHolder1_gvDimensions_p').find('tr')[j].innerHTML + "</tr>");
            ////    if (RowLength == j) {
            ////        break;
            ////    }
            ////}

            //////  k = k + 15;
            ////winprint.document.write("</tbody>");
            ////winprint.document.write("</table>");
            ////winprint.document.write("</div>");

            winprint.document.write("</div>");

            winprint.document.write("</td></tr></tbody>");
            winprint.document.write("<tfoot><tr><td>");
            winprint.document.write("<div class='col-sm-12 text-center' style='height:20mm;'>");
            winprint.document.write("<img id='imgqrcode' class='Qrcode' src='" + QrCode + "' />");
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
                                    <h3 class="page-title-head d-inline-block">Dimension Inspection Report</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Quality</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Dimension Inspection Report</li>
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

                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4 text-right">
                                                <label class="form-label">
                                                    Item Name  
                                                </label>
                                            </div>
                                            <div class="col-sm-6 text-left">
                                                <asp:DropDownList ID="ddlItemName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlItemName_SelectIndexChanged"
                                                    CssClass="form-control" Width="70%" ToolTip="Select Item Name">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-2">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10 text-center">
                                        <asp:GridView ID="gvBellowSnoDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium" DataKeyNames="PRIDID">
                                            <Columns>
                                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkall" runat="server" onclick="return checkAllItems(this);" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkitems" runat="server"
                                                            AutoPostBack="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--  <asp:TemplateField HeaderText="Size" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                    HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSize" runat="server" Text='<%# Eval("")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="DRG No" ItemStyle-HorizontalAlign="left"
                                                    HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDRGNo" runat="server" Text='<%# Eval("DRGNo")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bellow SNo" ItemStyle-HorizontalAlign="left"
                                                    HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBellowSno" runat="server" CssClass="bellowsno" Text='<%# Eval("ItemSno")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-sm-12 p-t-10 text-center">
                                        <asp:LinkButton ID="btnGetRows" OnClientClick="return GetRows();" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                            <img src="../Assets/images/add1.png"/></asp:LinkButton>
                                    </div>
                                    <div id="divOutPut" class="col-sm-12 p-t-10" style="overflow-x: scroll;" runat="server">
                                        <table id="tblDimension" class="table table-hover table-bordered medium">
                                        </table>
                                    </div>
                                    <div class="col-sm-12 p-t-10 text-center">
                                        <asp:LinkButton ID="btnSaveDimensionInspectionReport" Text="Save" CssClass="btn btn-cons btn-success" OnClientClick="return SaveDimensionInspectionReport();" runat="server"
                                            CommandArgument='<%#((GridViewRow) Container).RowIndex %>'></asp:LinkButton>
                                    </div>

                                    <div class="col-sm-12 p-t-10 text-center">
                                        <asp:GridView ID="gvDimensionInspectionDetails" runat="server" AutoGenerateColumns="true" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium" DataKeyNames="DIRDID">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete" runat="server" Text="Delete"
                                                            OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("DIRDID")) %>'
                                                            ToolTip="Delete"> <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                    <div class="col-sm-12 p-t-10 text-center">
                                        <asp:LinkButton ID="btnPrintDIR" Text="Print" CssClass="btn btn-cons btn-success" OnClick="btnPrintDIR_click" runat="server">
                                        </asp:LinkButton>
                                    </div>

                                    <asp:LinkButton ID="btnGetDIR" Text="Save" OnClick="btnGetDIR_Click" Style="display: none;" runat="server">
                                    </asp:LinkButton>

                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div id="divDimensionInspectionReport_P" runat="server">
        <div id="divcontent_p">
            <div class="col-sm-12">
                <label>RFP No:</label>
                <asp:Label ID="lblRFPNO_p" runat="server"></asp:Label>
                <label>Report No:</label>
                <asp:Label ID="lblReportNo_p" runat="server"></asp:Label>
                <label>Date:</label>
                <asp:Label ID="lblDate_p" runat="server"></asp:Label>
            </div>
            <div class="col-sm-12">
                <label>Dimension Inspection Report</label>
            </div>
            <div class="col-sm-12">
                <label>Customer Name</label>
                <asp:Label ID="lblCustomerName_p" runat="server"></asp:Label>
            </div>
            <div class="col-sm-12">
                <label>PO No</label>
                <asp:Label ID="lblPONo_p" runat="server"></asp:Label>
            </div>
            <div class="col-sm-12">
                <label>Project</label>
                <asp:Label ID="lblProject_p" runat="server"></asp:Label>
            </div>
            <div class="col-sm-12">
                <label>QAP No</label>
                <asp:Label ID="lblQAPNo_p" runat="server"></asp:Label>
            </div>
            <div class="col-sm-12">
                <label>Item Name</label>
                <asp:Label ID="lblItemName" runat="server"></asp:Label>
            </div>
            <div class="col-sm-12">
                <label>DRG No</label>
                <asp:Label ID="lblDRGNo" runat="server"></asp:Label>
            </div>
        </div>
        <div class="col-sm-12">
            <%--   <asp:GridView ID="gvDimensions_p" runat="server" AutoGenerateColumns="true" ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found"
                CssClass="table table-hover table-bordered medium">
                <Columns>
                </Columns>
            </asp:GridView>--%>
            <asp:GridView ID="gvDimensions_p" runat="server" AutoGenerateColumns="true" ShowHeaderWhenEmpty="True"
                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium" DataKeyNames="DIRDID">
                <Columns>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDelete" runat="server" Text="Delete"
                                OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("DIRDID")) %>'
                                ToolTip="Delete"> <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>
    </div>
</asp:Content>

