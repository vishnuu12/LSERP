<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="MaterialInwardQCClearenceDetails.aspx.cs" ClientIDMode="Predictable" ValidateRequest="false"
    Inherits="Pages_MaterialInwardQCClearenceDetails" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="../Assets/css/datatables2.css" />
    <script type="text/javascript" src="../Assets/scripts/datatables2.js"></script>
    <script type="text/javascript">

        function CallBackColumn(i) {

            var colVal = $('#col' + i + '_filter');

            // Custom range filtering function
            $.fn.dataTable.ext.search.push(function (settings, data, dataIndex) {
                var val = colVal.val();
                var data = data[i] || ""; // use data for the age column

                if (data.match(val))
                    return true;
                else if (val == "")
                    return true;
                else
                    return false;
            });

            var table = $('#ContentPlaceHolder1_gvaddeditems').DataTable();

            // Changes to the inputs will trigger a redraw to update the table
            colVal.on('input', function () {
                table.draw();
            });
        }
        function CallBackColumn1(i) {

            var colVal = $('#col' + i + '_filterr');

            // Custom range filtering function
            $.fn.dataTable.ext.search.push(function (settings, data, dataIndex) {
                var val = colVal.val();
                var data = data[i] || ""; // use data for the age column

                if (data.match(val))
                    return true;
                else if (val == "")
                    return true;
                else
                    return false;
            });

            var table = $('#ContentPlaceHolder1_gvaddeditems').DataTable();

            // Changes to the inputs will trigger a redraw to update the table
            colVal.on('input', function () {
                table.draw();
            });
        }

        function showDataTable() {
            $('#ContentPlaceHolder1_gvaddeditems').dataTable({
                "iDisplayLength": 10,
                "bJQueryUI": true,
                "bAutoWidth": false,
                "scrollX": true,
                searchPanes: {
                    columns: [3, 2, 1]
                },
                searchPanes: {
                    columns: [3, 2, 1]
                },
                "autoWidth": false,
                "fnInitComplete": function () {

                    // Enable THEAD scroll bars
                    $('.dataTables_scrollHead').css('overflow', 'auto');

                    // Sync THEAD scrolling with TBODY
                    $('.dataTables_scrollHead').on('scroll', function () {
                        $('.dataTables_scrollBody').scrollLeft($(this).scrollLeft());
                    });
                }
            });

        }

        function ShowAddPopUp() {
            $('#mpePartDetails').modal({
                backdrop: 'static'
            });
            return false;
        }

        function ShowAddQCPopup() {
            $('#mpeAssemplyPlanning').show();
            $('.datepicker').datepicker({
                format: 'dd/mm/yyyy'
            });
            return false;
        }

        function HideAddQCPopup() {
            $('#mpeAssemplyPlanning').hide();
            $('div').removeClass("modal-backdrop");
            return false;
        }

        function HideAddPopUp() {
            $('#mpePartDetails').hide();
            $('div').removeClass('modal-backdrop');
            return false;
        }

        function DCQuantityValidation(ele) {
            if (parseFloat($('#ContentPlaceHolder1_hdnMRNQuantity').val()) < parseFloat($(ele).val()) || parseFloat($(ele).val()) < 0) {
                ErrorMessage('Error', "DCQuantity shouldn't greater than Ordered Quantity");
                $(ele).val($('#ContentPlaceHolder1_hdnMRNQuantity').val());
                return false;
            }
        }

        function ReworkedQtyValidation(ele) {
            if ((parseFloat($('#ContentPlaceHolder1_hdnReworkedQty').val()) < parseFloat($(ele).val())) || (parseFloat($(ele).val()) < 0)) {
                ErrorMessage('Error', "Entered Quantity shouldn't greater than Reworked Quantity");
                $(ele).val('');
                return false;
            }
        }

        function deleteConfirm(MIQCDID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Quantity will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deletegvrow', MIQCDID);
            });
            return false;
        }


        function MRNPrint(epstyleurl, Main, QrCode, style, print, topstrip) {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var MrnContent = $('#ContentPlaceHolder1_divMRNPrint').html();

            winprint.document.write("<html><head><title>");
            winprint.document.write("Offer");
            winprint.document.write("</title>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='" + epstyleurl + "'/>");
            winprint.document.write("<link rel='stylesheet' href='" + Main + "' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet'  href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/print.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");

            winprint.document.write("<style type='text/css'/>  .Qrcode{ float: right; } label{ color: black ! important;font-weight: bold;} p{ margin:0;padding:0; }  @page { size: landscape; } span { padding: 0px 0px; } .ISO tr td { border:1px solid black; }  </style>");

            winprint.document.write("<style type='text/css'/> @media print  { .LandScapeprint{ width:277mm; margin-left:10mm;margin-right:10mm; height:190mm;margin-top:10mm;margin-bottom:10mm;} } </style>");

            winprint.document.write("</head><body>");
            winprint.document.write("<div style='width:297mm;height:210mm'>");
            winprint.document.write("<div class='LandScapeprint' style='border:2px solid black;'>");
            winprint.document.write("<div class='col-sm-12' style='padding-top:10px;margin:0 auto;padding-left:10px;padding-right:10px;'>");
            winprint.document.write("<div>");
            winprint.document.write(MrnContent);
            winprint.document.write("</div>");

            winprint.document.write("<div>");

            winprint.document.write("</div>");

            winprint.document.write("</div>");
            winprint.document.write("</div>");
            winprint.document.write("</div>");
            winprint.document.write("</body></html>");

            setTimeout(function () {
                winprint.document.close();
                winprint.focus();
                winprint.print();
                // winprint.close();
            }, 1000);
        }

        function UpdateQCCompletedStatus() {
            swal({
                title: "No Further Edit Once Shared.",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Share it!",
                closeOnConfirm: false
            }, function () {
                showLoader();
                __doPostBack("ShareQC", "");
            });
            return false;
        }

        function PrintMRN(MIDID) {
            OpenMRNPrint(MIDID);
            return false;
        }

        function OpenMRNPrint(MIDID) {
            var str = window.location.href.split('/');
            var replacevalue = str[str.length - 1];
            window.open(window.location.href.replace(replacevalue, 'MRNPrintDetails.aspx?MIDID=' + MIDID + ''), '_blank');
        }

    </script>

    <style type="text/css">
        div.dataTables_scrollBody > table {
            border-top: none;
            margin-top: -19px !important;
            margin-bottom: 0 !important;
        }

        table tr td .chosen-container {
            width: 138px !important;
        }

        .radio-success label:nth-child(2) {
            color: red !important;
        }

        .radio-success label:nth-child(4) {
            color: #0acc0a !important;
        }

        .modal-content {
            width: 137% !important;
        }

        table#ContentPlaceHolder1_gvaddeditems td {
            color: #000;
            font-weight: bold;
            font-size: smaller;
        }

        tfoot input {
            width: 100%;
            padding: 3px;
            box-sizing: border-box;
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
                                    <h3 class="page-title-head d-inline-block">Material Inward QC Clerance</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <%--href="javascript:void(0);"--%>
                                <li class="breadcrumb-item"><a href="AdminHome.aspx">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Stores</a></li>
                                <li class="active breadcrumb-item" aria-current="page">Material Inward QC Clearance</li>
                            </ol>
                        </nav>
                        <a id="help" href="#" alt="" style="margin-top: 4px;">
                            <img src="../Assets/images/help.png" alt="" />
                        </a>
                    </div>
                </div>
            </div>
        </div>
        <div>
            <asp:UpdatePanel ID="upDocumenttype" runat="server">
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
                                        <asp:RadioButtonList ID="rblMRNChange" runat="server" CssClass="radio radio-success" RepeatDirection="Horizontal"
                                            AutoPostBack="true" OnSelectedIndexChanged="rblMRNChange_OnSelectedChanged" RepeatLayout="Flow">
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
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Location Name</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlLocationName" runat="server" ToolTip="Select Location Name"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlLocationName_OnSelectIndexChanged"
                                            Width="70%" CssClass="form-control mandatoryfield">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divAddNew" runat="server">
                            <div class="ip-div text-center p-t-10">
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server" visible="false">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div id="divAdmin" runat="server" visible="false">
                                            <table cellpadding="3" cellspacing="0" border="0">
                                                <tbody>
                                                    <th id="filter_col1" data-column="0">
                                                        <td align="center">
                                                            <input type="text" placeholder="PONo Search" style="width: 250px;" onclick="CallBackColumn(7)" class="column_filter form-control" id="col7_filter"></td>
                                                    </th>
                                                    <th id="filter_col2" data-column="1">
                                                        <td align="center">
                                                            <input type="text" placeholder="MRNNo Search" style="width: 250px; margin-left: 100px;" onclick="CallBackColumn(8)" class="column_filter form-control" id="col8_filter"></td>
                                                    </th>
                                                    <th id="filter_col3" data-column="2">
                                                        <td align="center">
                                                            <input type="text" placeholder="Category Search" style="width: 250px; margin-left: 100px;" onclick="CallBackColumn(9)" class="column_filter form-control" id="col9_filter"></td>
                                                    </th>
                                                </tbody>
                                            </table>
                                            <table cellpadding="3" style="margin-top: 10px;" cellspacing="0" border="0">
                                                <tbody style="margin-bottom: 5px;">
                                                    <th id="filter_col4" data-column="3">
                                                        <td align="center">
                                                            <input type="text" placeholder="Grade Search" style="width: 250px;" onclick="CallBackColumn(10)" class="column_filter form-control" id="col10_filter"></td>
                                                    </th>
                                                    <th id="filter_col5" data-column="4">
                                                        <td align="center">
                                                            <input type="text" placeholder="THK Search" style="width: 250px; margin-left: 100px;" onclick="CallBackColumn(11)" class="column_filter form-control" id="col11_filter"></td>
                                                    </th>
                                                    <th id="filter_col6" data-column="5">
                                                        <td align="center">
                                                            <input type="text" placeholder="Material Search" style="width: 250px; margin-left: 100px;" onclick="CallBackColumn(13)" class="column_filter form-control" id="col13_filter"></td>
                                                    </th>
                                                </tbody>
                                            </table>
                                        </div>
                                        <div id="divUser" runat="server" visible="false">
                                            <table cellpadding="3" cellspacing="0" border="0">
                                                <tbody>
                                                    <th id="filter_col11" data-column="0">
                                                        <td align="center">
                                                            <input type="text" placeholder="PONo Search" style="width: 250px;" onclick="CallBackColumn1(6)" class="column_filter form-control" id="col6_filterr"></td>
                                                    </th>
                                                    <th id="filter_col12" data-column="1">
                                                        <td align="center">
                                                            <input type="text" placeholder="MRNNo Search" style="width: 250px; margin-left: 100px;" onclick="CallBackColumn1(7)" class="column_filter form-control" id="col7_filterr"></td>
                                                    </th>
                                                    <th id="filter_col13" data-column="2">
                                                        <td align="center">
                                                            <input type="text" placeholder="Category Search" style="width: 250px; margin-left: 100px;" onclick="CallBackColumn1(8)" class="column_filter form-control" id="col8_filterr"></td>
                                                    </th>
                                                </tbody>
                                            </table>
                                            <table cellpadding="3" style="margin-top: 10px;" cellspacing="0" border="0">
                                                <tbody style="margin-bottom: 5px;">
                                                    <th id="filter_col14" data-column="3">
                                                        <td align="center">
                                                            <input type="text" placeholder="Grade Search" style="width: 250px;" onclick="CallBackColumn1(9)" class="column_filter form-control" id="col9_filterr"></td>
                                                    </th>
                                                    <th id="filter_col15" data-column="4">
                                                        <td align="center">
                                                            <input type="text" placeholder="THK Search" style="width: 250px; margin-left: 100px;" onclick="CallBackColumn1(10)" class="column_filter form-control" id="col10_filterr"></td>
                                                    </th>
                                                    <th id="filter_col16" data-column="5">
                                                        <td align="center">
                                                            <input type="text" placeholder="Material Search" style="width: 250px; margin-left: 100px;" onclick="CallBackColumn1(12)" class="column_filter form-control" id="col12_filterr"></td>
                                                    </th>
                                                </tbody>
                                            </table>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvaddeditems" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered"
                                                OnRowCommand="gvaddeditems_OnRowCommand" OnRowDataBound="gvaddeditems_OnRowDataBound" DataKeyNames="SPODID,MIDID,MIQCStatus">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="TC *" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnTC" runat="server" Text="Update"
                                                                CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="TC">
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="TC Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCFStatus" runat="server" Text='<%# Eval("CFStatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Add QC" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnAddQC" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="QC">
                                                            <img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Add TC" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnAdd" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="Certificates">
                                                            <img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="MRN" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnMRN" runat="server"
                                                                OnClientClick='<%# string.Format("return PrintMRN({0});",Eval("MIDID")) %>'>
                                                            <img src="../Assets/images/pdf.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="QC Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMIQCStatus" runat="server" Text='<%# Eval("MIQCStatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="PO No" ItemStyle-HorizontalAlign="left"
                                                        HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsponumber" runat="server" Text='<%# Eval("SPONumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MRN No" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMRNNumber" runat="server" Text='<%# Eval("MRNNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Category" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("CategoryName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Grade" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGradeName" runat="server" Text='<%# Eval("GradeName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="THK" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMaterialThickness" runat="server" Text='<%# Eval("THKValue")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--    <asp:TemplateField HeaderText="Quote Cost" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQuoteCost" runat="server" Text='<%# Eval("Cost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Measurement" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMeasurement" runat="server" Text='<%# Eval("Measurment").ToString().Replace(",", "<br/>")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MTL Type" ItemStyle-HorizontalAlign="left"
                                                        HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTypeName" runat="server" Text='<%# Eval("TypeName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--  <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQuantity" CssClass="lblqty" runat="server" Text='<%# Eval("Quantity")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="DC QTY" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDCQuantity" runat="server" CssClass="lbldcqty" Text='<%# Eval("DCQuantity")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--  <asp:TemplateField HeaderText="Required Weight" ItemStyle-HorizontalAlign="Left"
                                                        HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRW" Width="50px" runat="server" Text='<%# Eval("ReqWeight")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnMIDID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnMIQCDID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnSPODID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnMRNQuantity" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnReworkedQty" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnPdfContent" runat="server" />

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

    <div style="display: none;" runat="server">
        <div id="divMRNPrint" runat="server">

            <div class="col-sm-12 text-center">
                <h3 style='font-weight: 600; font-size: 24px; font-style: italic; color: #000; font-family: Arial; display: contents;'>LONESTAR </h3>
                <span style='font-weight: 600; font-size: 24px ! important; font-family: Times New Roman;'>INDUSTRIES</span>
            </div>
            <div class="col-sm-12 p-t-10" style="text-align: center; font-size: large; color: black; font-weight: bold;">
                Material Receipt Note Cum Receiving Inspection Report
            </div>
            <div class="col-sm-12">
                <div class="col-sm-2">
                    <img src='../Assets/images/lonestar.jpeg' alt='lonestar-image' width='90px'>
                </div>
                <div class="col-sm-4">
                    <div class="col-sm-12 text-left">
                        <label>OFFICE:</label>
                    </div>
                    <div class="col-sm-12">
                        <%--    <p style='font-weight: 500; color: #000; width: 103%;'>
                        <asp:Label ID="lblOfficeAddress_p" runat="server"></asp:Label>
                    </p>
                    <p style='font-weight: 500; color: #000'>
                        <asp:Label ID="lblOfficePhoneAndFaxNo_p" runat="server"></asp:Label>
                    </p>
                    <p style='font-weight: 500; color: #000'>
                        <asp:Label ID="lblOfficeEmail_p" runat="server"></asp:Label>
                    </p>
                    <p style='font-weight: 500; color: #000'>
                        <asp:Label ID="lblOfficeWebsite_p" runat="server"></asp:Label>
                    </p>--%>
                        <asp:Label ID="lblOfficeAddress_p" Style="font-weight: bold;" runat="server"></asp:Label>
                        <asp:Label ID="lblOfficePhoneAndFaxNo_p" Style="font-weight: bold;" runat="server"></asp:Label>
                        <asp:Label ID="lblOfficeEmail_p" Style="font-weight: bold;" runat="server"></asp:Label>
                        <asp:Label ID="lblOfficeWebsite_p" Style="font-weight: bold;" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-4">
                    <div class="col-sm-12 text-left">
                        <label>WORKS:</label>
                    </div>
                    <div class="col-sm-12">
                        <%--  <p style='font-weight: 500; color: #000; width: 103%;'>
                        <asp:Label ID="lblWorkAddress_p" runat="server"></asp:Label>
                    </p>
                    <p style='font-weight: 500; color: #000'>
                        <asp:Label ID="lblWorkPhoneAndFaxNo_p" runat="server"></asp:Label>
                    </p>
                    <p style='font-weight: 500; color: #000'>
                        <asp:Label ID="lblWorkEmail_p" runat="server"></asp:Label>
                    </p>
                    <p style='font-weight: 500; color: #000'>
                        <asp:Label ID="lblWorkWebsite_p" runat="server"></asp:Label>
                    </p>--%>
                        <asp:Label ID="lblWorkAddress_p" Style="font-weight: bold;" runat="server"></asp:Label>
                        <asp:Label ID="lblWorkPhoneAndFaxNo_p" Style="font-weight: bold;" runat="server"></asp:Label>
                        <asp:Label ID="lblWorkEmail_p" Style="font-weight: bold;" runat="server"></asp:Label>
                        <asp:Label ID="lblWorkWebsite_p" Style="font-weight: bold;" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-2" style="padding-left: 0px;">
                    <asp:Label ID="lblMRNNo_p" Style="font-weight: bold;" runat="server"></asp:Label>
                    <asp:Label ID="lblDate_p" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-sm-12 text-center">
                <%--<label>MRN No</label>--%>
            </div>
            <%--   <div class="col-sm-12">
                    <label>Date</label>
                   
                </div>--%>
            <div class="col-sm-12 p-t-10">
                <div class="col-sm-6">
                    <label>Supplier:</label>
                    <asp:Label ID="lblSupplierName_p" runat="server"></asp:Label>
                </div>
                <div class="col-sm-6">
                    <label>Visual:</label>
                    <asp:Label ID="lblVisual_p" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-sm-12 p-t-10">
                <div class="col-sm-6">
                    <label>SupplierDC/InvoiceNo./Date: </label>
                    <asp:Label ID="lblInVoiceNo_p" runat="server"></asp:Label>
                </div>
                <div class="col-sm-6">
                    <label>Check Test</label>
                    <asp:Label ID="lblCheckTest_p" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-sm-12 p-t-10">
                <div class="col-sm-6">
                    <label>PO No And Date:</label>
                    <asp:Label ID="lblPONoDate_p" runat="server"></asp:Label>
                </div>
                <div class="col-sm-6">
                    <label>Addt Test Requirtment</label>
                    <asp:Label ID="lblAddtionalTestRequirtment_p" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-sm-12 p-t-10">
                <div class="col-sm-6">
                    <label>MTR:</label>
                    <asp:Label ID="lblMTR_p" runat="server"></asp:Label>
                </div>
                <div class="col-sm-6">
                    <label>Measured Dimension:</label>
                    <asp:Label ID="lblMeasuredDimension_p" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-sm-12 p-t-10">
                <div class="col-sm-6">
                    <label>TPS No:</label>
                    <asp:Label ID="lblTPSNo_p" runat="server"></asp:Label>
                </div>
                <div class="col-sm-6">
                    <label>Original / Identification Marking </label>
                    <asp:Label ID="lblOriginalMarking_p" runat="server"></asp:Label>
                </div>
            </div>

            <div class="col-sm-12 p-t-10">
                <div class="col-sm-6">
                    <label>PMI :</label>
                    <asp:Label ID="lblPMI_p" runat="server"></asp:Label>
                </div>
                <div class="col-sm-6">
                    <label>Certificate No </label>
                    <asp:Label ID="lblCertificateNo_p" runat="server"></asp:Label>
                </div>
            </div>

            <div class="col-sm-12 p-t-10">
                <div class="col-sm-6">
                </div>
                <div class="col-sm-6" style="margin-left: 50%;">
                    <div class="col-sm-12">
                        <label>Used Instrument Calibration Details:</label>
                    </div>
                    <div class="col-sm-12">
                        <%--  <div class="col-sm-3">
                        <label>Instrument Ref No:</label>
                        <asp:Label ID="lblInstrumentRefNo_p" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <label>Cal Report No:</label>
                        <asp:Label ID="lblCalReportNo_p" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <label>Done On</label>
                        <asp:Label ID="lblDoneOn_p" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-3">
                        <label>Due On:</label>
                        <asp:Label ID="lblDueOn_p" runat="server"></asp:Label>
                    </div>--%>
                        <asp:GridView ID="gvInstrumentDetails_p" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                            <Columns>
                                <asp:TemplateField HeaderText="Instrument Ref No" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("InstrumentRefNo")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cal Report No" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDCQuantity" runat="server" Text='<%# Eval("CalreportNo")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Done On" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMaterialType" runat="server" Text='<%# Eval("DoneOn")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Due On" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMeasurment" runat="server" Text='<%# Eval("DueOn")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <div class="col-sm-12 p-t-10">
                <asp:GridView ID="gvQCClearedDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                    EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                            HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DC Qty" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblDCQuantity" runat="server" Text='<%# Eval("DCQuantity")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Material Type" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblMaterialType" runat="server" Text='<%# Eval("TypeName")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblMeasurment" runat="server" Text='<%# Eval("Measurment")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Accepted" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblQtyAccepted" runat="server" Text='<%# Eval("AcceptedQuantity")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Reworked" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblQtyReworked" runat="server" Text='<%# Eval("ReWorkedQuantity")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Rejected" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblQtyrejected" runat="server" Text='<%# Eval("RejectedQuantity")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <div class="col-sm-12">
                <div class="col-sm-6">
                    <div class="col-sm-12 text-left">
                        <label>Stores In Charge: </label>
                    </div>
                    <div class="col-sm-12 text-left">
                        <label>Received By:</label>
                        <asp:Label ID="lblReceivedBy_p" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-12 text-left">
                        <label>Date:</label>
                        <asp:Label ID="lblReceivedDate_p" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-12 text-left">
                        <label>Signature: </label>
                    </div>
                </div>
                <div class="col-sm-4">

                    <div class="col-sm-12 text-left">
                        <label>Quality In Charge: </label>
                    </div>
                    <div class="col-sm-12 text-left">
                        <label>Inspected By:</label>
                        <asp:Label ID="lblInspectedBy_p" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-12 text-left">
                        <label>Date:</label>
                        <asp:Label ID="lblInspectedDate_p" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-12 text-left">
                        <label>Signature: </label>
                    </div>
                </div>
                <div class="col-sm-2">
                    <asp:Image ID="imgQrcode" ImageUrl="" CssClass="Qrcode" runat="server" />
                </div>
            </div>

            <div class="col-sm-12 p-t-20">
                <table style="width: 50%; margin-left: 25%; height: 5%;" class="ISO">
                    <tr style="vertical-align: middle; text-align: center;">
                        <td>
                            <label>
                                Doc . No</label>
                        </td>
                        <td>
                            <asp:Label ID="lblDocNo_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label>
                                Rev. No
                            </label>
                        </td>
                        <td>
                            <asp:Label ID="lblRevNo_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label>
                                Date
                            </label>
                        </td>
                        <td>
                            <asp:Label ID="lblISODate_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>

    <div class="modal" id="mpePartDetails" style="overflow-y: scroll;">
        <div style="max-width: 100%; padding-left: 3%; padding-top: 5%; width: 70%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSaveCertificate" />
                        <asp:PostBackTrigger ControlID="gvCertficateDetails" />
                        <asp:PostBackTrigger ControlID="gvcertificatesAddedDetails" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">
                                <asp:Label ID="lblcertificateheader_h" runat="server"></asp:Label></h4>
                            <asp:Label ID="lblItemName_P" runat="server" Style="padding-left: 19px; padding-top: 10px; color: #c12424;"></asp:Label>
                            <button type="button" class="close btn-primary-purple" onclick="HideAddPopUp();"
                                data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="divCertificates" class="docdiv">
                                <div class="inner-container">
                                    <div class="col-sm-12">
                                        <asp:GridView ID="gvCertficateDetails" runat="server" AutoGenerateColumns="False"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                            OnRowDataBound="gvCertficateDetails_OnRowDataBound">
                                            <%--DataKeyNames="FileName,CFID"--%>
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="54px" HeaderStyle-CssClass="text-center"
                                                    ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--   <asp:TemplateField HeaderText="Requested By" ItemStyle-HorizontalAlign="left"
                                                HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequestedBy" runat="server" Text='<%# Eval("")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Request Date" ItemStyle-HorizontalAlign="left"
                                                HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequestDate" runat="server" Text='<%# Eval("")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                                <%--     <asp:TemplateField HeaderText="Certificate Status" ItemStyle-HorizontalAlign="left"
                                                HeaderStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCertificateStatus" runat="server"
                                                        Text='<%# Eval("CertificateStatus")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Certificates Name" ItemStyle-HorizontalAlign="left"
                                                    HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <%-- <asp:Label ID="lblCertificateName" CssClass="form-control" runat="server"
                                                        Text='<%# Eval("CertificateName")%>'></asp:Label>--%>
                                                        <asp:DropDownList ID="ddlCertificateName" runat="server" ToolTip="Select Country" TabIndex="6"
                                                            CssClass="form-control mandatoryfield">
                                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Certificates No" ItemStyle-HorizontalAlign="left"
                                                    HeaderStyle-Width="20%" ItemStyle-Width="20%" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtCertificatesNo" CssClass="form-control mandatoryfield" runat="server"
                                                            TextMode="MultiLine" Rows="2" Text='<%# Eval("CertficateNo")%>'></asp:TextBox>
                                                        <%--Text='<%# Eval("CertficateNo")%>'--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Received Date" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="20%"
                                                    ItemStyle-Width="20%" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtReceivedDate" CssClass="form-control mandatoryfield datepicker"
                                                            runat="server"></asp:TextBox>
                                                        <%--Text='<%# Eval("ReceivedDateEdit")%>'--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Attachement" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="20%"
                                                    ItemStyle-Width="20%" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:FileUpload ID="fAttachment" runat="server" TabIndex="12" onchange="DocValidation(this);" CssClass="form-control mandatoryfield"
                                                            ClientIDMode="Static" Width="95%"></asp:FileUpload>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View Attachement" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnView" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            CommandName="ViewAttach"><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="QC Status" ItemStyle-HorizontalAlign="left"
                                                    HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlQCStatus" runat="server" ToolTip="Select QC Status"
                                                            CssClass="form-control">
                                                            <asp:ListItem Value="A" Text="Allotted" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Value="P" Text="Pass"></asp:ListItem>
                                                            <asp:ListItem Value="F" Text="Fail"></asp:ListItem>
                                                            <asp:ListItem Value="W" Text="Waived"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="left"
                                                    HeaderStyle-Width="50%" ItemStyle-Width="50%" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <%--Text='<%# Eval("QC_Remarks")%>'--%>
                                                        <asp:TextBox ID="txtRemarks" TextMode="MultiLine" Rows="2" runat="server"
                                                            CssClass="form-control"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--   <asp:TemplateField HeaderText="QC Done By" ItemStyle-HorizontalAlign="left"
                                                HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequestDate" runat="server" Text='<%# Eval("QCDoneBY")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-sm-12 text-center">
                                        <asp:LinkButton ID="btnSaveCertificate" runat="server" Text="Save" CssClass="btn btn-cons btn-save AlignTop"
                                            OnClientClick="return Mandatorycheck('divCertificates');" OnClick="btnSaveCertificate_Click"></asp:LinkButton>
                                    </div>

                                    <div class="col-sm-12 p-t-10">
                                        <label>Certificates  Added   </label>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <asp:GridView ID="gvcertificatesAddedDetails" runat="server" AutoGenerateColumns="False"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found"
                                            OnRowCommand="gvcertificatesAddedDetails_OnRowCommand"
                                            OnRowDataBound="gvcertificatesAddedDetails_OnRowDataBound"
                                            CssClass="table table-hover table-bordered medium" DataKeyNames="FileName,CFID,MIQCCID">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="54px" HeaderStyle-CssClass="text-center"
                                                    ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--   <asp:TemplateField HeaderText="Requested By" ItemStyle-HorizontalAlign="left"
                                                HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequestedBy" runat="server" Text='<%# Eval("")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Request Date" ItemStyle-HorizontalAlign="left"
                                                HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRequestDate" runat="server" Text='<%# Eval("")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Certificate Status" ItemStyle-HorizontalAlign="left"
                                                    HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCertificateStatus" runat="server"
                                                            Text='<%# Eval("CertificateStatus")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Certificates Name" ItemStyle-HorizontalAlign="left"
                                                    HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCertificateName" runat="server"
                                                            Text='<%# Eval("CertificateName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Certificates No" ItemStyle-HorizontalAlign="left"
                                                    HeaderStyle-Width="20%" ItemStyle-Width="20%" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtCertificatesNo" runat="server"
                                                            Text='<%# Eval("CertficateNo")%>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Received Date" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="20%"
                                                    ItemStyle-Width="20%" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReceivedDate"
                                                            Text='<%# Eval("ReceivedDateEdit")%>' runat="server"></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--      <asp:TemplateField HeaderText="Attachement" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="20%"
                                                    ItemStyle-Width="20%" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:FileUpload ID="fAttachment" runat="server" TabIndex="12"
                                                            onchange="DocValidation(this);" CssClass="form-control"
                                                            ClientIDMode="Static" Width="95%"></asp:FileUpload>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="View Attachement" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnView" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            CommandName="ViewAttach"><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="QC Status" ItemStyle-HorizontalAlign="left"
                                                    HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQCStatus" runat="server" Text='<%# Eval("QCStatus")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="left"
                                                    HeaderStyle-Width="50%" ItemStyle-Width="50%" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtRemarks" runat="server" Text='<%# Eval("QC_Remarks")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="QC Done By" ItemStyle-HorizontalAlign="left"
                                                    HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRequestDate" runat="server" Text='<%# Eval("QCDoneBY")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete CF" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnDeleteCF" runat="server"
                                                            CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            CommandName="DeleteCertificates">  <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                    </div>

                                    <div class="col-sm-12 text-center">
                                        <asp:LinkButton ID="btnShareStockCertificates" runat="server" Text="Share Certificates"
                                            CssClass="btn btn-cons btn-success"
                                            OnClick="btnShareStockCertificates_Click"></asp:LinkButton>
                                    </div>

                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>
                        </div>
                        <iframe id="ifrm" runat="server" style="display: none;"></iframe>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal" id="mpeAssemplyPlanning" style="overflow-y: scroll;">
        <div style="max-width: 70%; margin-left: 2%; padding-top: 3%; margin-right: 2%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD"></h4>
                            <asp:Label ID="lblMrnNumber_h" runat="server" Style="font-weight: bold; padding-left: 50%; color: brown;"></asp:Label>
                            <button type="button" class="close btn-primary-purple" onclick="HideAddQCPopup();"
                                data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="divQCDetails" class="docdiv">
                                <div class="inner-container">
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-3">
                                            <label>
                                                Supplier Name</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:Label ID="lblSupplierName" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-3">
                                            <label>
                                                InVoice No And Date</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:Label ID="lblInVoiceNoAndDate" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-3">
                                            <label>
                                                PO No And Date</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:Label ID="lblPoNoAndDate" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-3">
                                            <label>
                                                Measurment  
                                            </label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:Label ID="lblMeasurment" runat="server"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-3">
                                            <label>
                                                RFP No
                                            </label>
                                            <asp:Label ID="lblRFPNo" Text="RFP-001" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-3">
                                            <label>
                                                Material Grade
                                            </label>
                                            <asp:Label ID="lblMaterialGrade" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-3">
                                            <label>
                                                Material Type
                                            </label>
                                            <asp:Label ID="lblmaterialType" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-3">
                                            <label>
                                                Material Thickness
                                            </label>
                                            <asp:Label ID="lblMaterialthickness" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-3">
                                            <label>Indent Remarks : </label>
                                            <asp:Label ID="lblIndentRemarks" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-3">
                                            <label>MRN Inward Date </label>
                                            <asp:Label ID="lblMRNInwardDate" runat="server"></asp:Label>
                                        </div>
                                    </div>

                                    <div id="divQCQtyDetails" runat="server">
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <label class="mandatorylbl">
                                                    Quantity</label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtQuantity" onkeypress="return validationDecimal(this);" onkeyup="DCQuantityValidation(this);"
                                                    CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                <asp:Label ID="lblAvailableToQCClearanceQty" Style="color: brown;" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <label>
                                                    Material Qty Status</label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:RadioButtonList ID="rblQtyStatus" runat="server" CssClass="radio radio-success"
                                                    RepeatDirection="Horizontal" RepeatLayout="Flow" TabIndex="5">
                                                    <asp:ListItem Selected="True" Value="A">Accepted</asp:ListItem>
                                                    <asp:ListItem Value="RJ">Rejected</asp:ListItem>
                                                    <asp:ListItem Value="RW">ReWorked</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                        <%--<div id="divReworkedQuantity" runat="server" visible="false">
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <label class="">
                                                        Reworked Quantity</label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:TextBox ID="txtReworkedQuantity" onkeypress="return validationDecimal(this);"
                                                        onkeyup="ReworkedQtyValidation(this);" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:Label ID="lblReworkedQty" Style="color: brown;" runat="server"></asp:Label>
                                                </div>
                                                <div class="col-sm-4">
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <label>
                                                        Reworked Qty Status</label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:RadioButtonList ID="rblReworkedQtyStatus" runat="server" CssClass="radio radio-success"
                                                        RepeatDirection="Horizontal" RepeatLayout="Flow" TabIndex="5">
                                                        <asp:ListItem Value="RWC">Accepted</asp:ListItem>
                                                        <asp:ListItem Value="RJ">Rejected</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                                <div class="col-sm-4">
                                                </div>
                                            </div>
                                        </div>--%>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <label>
                                                    Remarks</label>
                                            </div>
                                            <div class="col-sm-4">
                                                <asp:TextBox ID="txtRemarks" CssClass="form-control" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10 text-center">
                                        <asp:LinkButton ID="btnSaveQCQtyDetails" runat="server" Text="Save" CssClass="btn btn-cons btn-save AlignTop"
                                            OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divQCQtyDetails');" OnClick="btnSaveQCQtyDetails_Click"></asp:LinkButton>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <asp:GridView ID="gvQCQtyDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No Records Found" OnRowDataBound="gvQCQtyDetails_OnRowDataBound" CssClass="table table-hover table-bordered medium">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" CssClass="lbldcqty" Text='<%# Eval("MaterialQtyStatus")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qty" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRW" Width="50px" runat="server" Text='<%# Eval("Quantity")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete" runat="server" OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("MIQCDID")) %>'>
                                                            <img src="../Assets/images/del-ec.png" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-3">
                                            <label>
                                                MTR
                                            </label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:RadioButtonList ID="rblMTR" runat="server" CssClass="radio radio-success" RepeatDirection="Horizontal"
                                                RepeatLayout="Flow" TabIndex="5">
                                                <asp:ListItem Selected="True" Value="A">Available</asp:ListItem>
                                                <asp:ListItem Value="NA">Not Available</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="col-sm-3">
                                            <label class="mandatorylbl">
                                                TPS No</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtTPSNO" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-3">
                                            <label>
                                                Visual
                                            </label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:RadioButtonList ID="rblVisual" runat="server" CssClass="radio radio-success"
                                                RepeatDirection="Horizontal" RepeatLayout="Flow" TabIndex="5">
                                                <asp:ListItem Selected="True" Value="Ac">Accepted</asp:ListItem>
                                                <asp:ListItem Value="NA">Not Accepted</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="col-sm-3">
                                            <label class="mandatorylbl">
                                                PMI
                                            </label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtPMI" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-3">
                                            <label>
                                                Check Test
                                            </label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:RadioButtonList ID="rblCheckTest" runat="server" CssClass="radio radio-success"
                                                RepeatDirection="Horizontal" RepeatLayout="Flow" TabIndex="5">
                                                <asp:ListItem Selected="True" Value="A">Accepted</asp:ListItem>
                                                <asp:ListItem Value="NA">Not Accepted</asp:ListItem>
                                                <asp:ListItem Value="NR">Not Required</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="col-sm-3">
                                            <label>
                                                Addtional Requirtment</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:RadioButtonList ID="rblAddtionalRequirtment" runat="server" CssClass="radio radio-success"
                                                RepeatDirection="Horizontal" RepeatLayout="Flow" TabIndex="5">
                                                <asp:ListItem Selected="True" Value="Ap">Applicable</asp:ListItem>
                                                <asp:ListItem Value="NA">Not Applicable</asp:ListItem>
                                                <asp:ListItem Value="Ac">Accepted</asp:ListItem>
                                                <asp:ListItem Value="Nc">Not Accepted</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-3">
                                            <label class="mandatorylbl">
                                                Measured Dimension
                                            </label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtMeasureddimension" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-3">
                                            <label class="mandatorylbl">
                                                Original Marking</label>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:TextBox ID="txtOriginalMarking" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 text-center p-t-10">
                                        <label>Used Instrument Calibration Details</label>
                                    </div>
                                    <div class="col-sm-12">
                                        <table id="tblInstrumentDetails" class="table table-hover table-bordered medium" style="width: 80%; margin-left: 10%;">
                                            <tr>
                                                <th>Instrument Ref No </th>
                                                <th>Cal Report No </th>
                                                <th>Done On</th>
                                                <th>Due On</th>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtInstrumentRefNo" CssClass="form-control" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCalReportNo" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDoneOn" CssClass="form-control datepicker" runat="server"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtDueOn" CssClass="form-control datepicker" runat="server"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtInstrumentRefNo1" CssClass="form-control" Style="margin-top: 10px;" runat="server"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtCalReportNo1" CssClass="form-control mandatoryfield" Style="margin-top: 10px;" runat="server"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtDoneOn1" CssClass="form-control datepicker" Style="margin-top: 10px;" runat="server"></asp:TextBox></td>
                                                <td>
                                                    <asp:TextBox ID="txtDueOn1" CssClass="form-control datepicker" Style="margin-top: 10px;" runat="server"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <asp:GridView ID="gvQcClearanceHeader" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TPS No" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTPSNo" runat="server" CssClass="lbldcqty" Text='<%# Eval("TPSNo")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Instrument Ref No" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInstrumentRefNo" Width="50px" runat="server" Text='<%# Eval("InstrumentRefNo")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Done On" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDoneOn" runat="server" Text='<%# Eval("DoneOn")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-sm-12 p-t-10 text-center">
                                        <asp:LinkButton ID="btnSaveMIQC" runat="server" Text="Save" CssClass="btn btn-cons btn-save AlignTop"
                                            OnClientClick="return Mandatorycheck('divQCDetails');" OnClick="btnSaveMIQC_Click"></asp:LinkButton>
                                    </div>
                                    <div class="col-sm-12 p-t-10 text-center">
                                        <asp:LinkButton ID="btnQCCompleted" runat="server" Text="Share QC" CssClass="btn btn-cons btn-success"
                                            OnClientClick="return UpdateQCCompletedStatus();"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="col-sm-12 text-center">
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
