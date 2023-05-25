<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="StockMonitorReport.aspx.cs" Inherits="Pages_StockMonitorReport" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {
            BindDate();
        });
        function ColumnChange() {
            $('#gvStockMonitorDetails').DataTable().draw();
            //$('#ContentPlaceHolder1_txtCategoryName').on('keyup', function () {
            //    table.search(this.value).draw();
            //});
            return false;
        }

        function ShowAddPopUp() {
            $('#mpeView').modal({
                backdrop: 'static'
            });
            $('div').removeClass('modal-backdrop in');
            return false;
        }

        $(function () {
            $(".wrapper1").scroll(function () {
                $(".wrapper2").scrollLeft($(".wrapper1").scrollLeft());
            });
            $(".wrapper2").scroll(function () {
                $(".wrapper1").scrollLeft($(".wrapper2").scrollLeft());
            });
        });

        $(document).ready(function () {

            BindDate('InStock');

        });

        function IndividualChange() {
            $('#tblEnquiryHeader').DataTable().draw();
            //$('#ContentPlaceHolder1_txttemp').on('keyup', function () {
            //    //table.search(this.value).draw();
            //});
            return false;
        }

        function rblChange() {
            var Mode = $("input[type='radio']:checked").val();
            BindDate(Mode);
        }

        function BindDate(Mode) {
            $('#gvStockMonitorDetails').DataTable({
                "retrieve": true,
                "processing": true,
                "serverSide": true,
                // "stateSave": true,
                "ordering": false,
                "bSort": false,
                "bDeferRender": true,
                "pageLength": 100,
                "bStateSave": true,
                "fnStateSave": function (oSettings, oData) {
                    localStorage.setItem('DataTables_' + window.location.pathname, JSON.stringify(oData));
                },
                "fnStateLoad": function (oSettings) {
                    var data = localStorage.getItem('DataTables_' + window.location.pathname);
                    return JSON.parse(data);
                },
                "stateSaveParams": function (settings, data) {
                    delete data.order;
                    data.length = 100;
                    data.order = [];
                },
                "ajax": ({
                    type: "GET",
                    url: "StockMonitorReport.aspx/GetData", //It calls our web method                  
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",

                    "data": function (d) {
                        d.CategoryName = $('#ContentPlaceHolder1_txtCategoryName').val().trim(),
                            d.MRNNo = $('#ContentPlaceHolder1_txtMRNNo').val().trim(),
                            d.Type = $('#ContentPlaceHolder1_txtType').val().trim(),
                            d.Grade = $('#ContentPlaceHolder1_txtGrade').val().trim(),
                            d.THK = $('#ContentPlaceHolder1_txtTHK').val().trim(),
                            d.Location = $('#ContentPlaceHolder1_txtLocation').val().trim(),
                            d.Mode = $("input[type='radio']:checked").val();
                        return d;
                    },

                    "dataSrc": function (json) {
                        json.draw = json.d.draw;
                        json.recordsTotal = json.d.recordsTotal;
                        json.recordsFiltered = json.d.recordsFiltered;
                        json.Ldata = json.d.Ldata;
                        var return_data = json;
                        return return_data.Ldata;
                    },

                }),
                "columns": [

                    { 'data': 'CategoryName' },
                    { 'data': 'MRNNumber' },
                    { 'data': 'MaterialTypeName' },
                    { 'data': 'GradeName' },
                    { 'data': 'Measurment' },
                    { 'data': 'THKValue' },
                    { 'data': 'InwardedQty' },
                    { 'data': 'UOM' },
                    { 'data': 'BlockedQty' },
                    { 'data': 'ConsumedQty' },
                    { 'data': 'ActualStock' },
                    { 'data': 'InStockQty' },
                    { 'data': 'UnitQuoteCost' },
                    { 'data': 'Cost' },
                    { 'data': 'RFPNo' },
                    { 'data': 'LocationName' },
                    {
                        'data': null,
                        "mRender": function (data, type, full) {
                            return `<a href="#" onclick="return OpenMRNPrint('${data.MIDID}')";><img src= "../Assets/images/print.png"></a>`
                        }
                    },
                    {
                        'data': null,
                        "mRender": function (data, type, full) {
                            if (data.CertificateName != null) {
                                return `<a href="#" onclick="return opennewtab('${data.CertificateName}')";><img src= "../Assets/images/view.png"></a>`
                            }
                            else {
                                return "NA";
                            }
                        }

                    }

                ],
            });
        }

        function opennewtab(Filepath) {
            var FileName = Filepath.split('/');
            for (var i = 0; i < FileName.length; i++)
                window.open('http://183.82.33.21/LSERPDocs/StoresDocs/MaterialInwardQCCertificates/' + FileName[i], '_blank');
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
            margin-top: -27px !important;
            margin-bottom: 0 !important;
        }

        .dataTables_scrollHead {
            height: 78px !important;
        }

        .dataTable > thead > tr > th[class*="sort"]:before,
        .dataTable > thead > tr > th[class*="sort"]:after {
            content: "" !important;
        }


        .table th:nth-child(1), th:nth-child(7), th:nth-child(10) {
            width: 80px !important;
        }

        .table th:nth-child(2), th:nth-child(3), th:nth-child(4), th:nth-child(5), th:nth-child(9), th:nth-child(7), th:nth-child(6) {
            width: 50px !important;
        }

        .table th:nth-child(11), th:nth-child(12), th:nth-child(13), th:nth-child(14), th:nth-child(15), th:nth-child(16), th:nth-child(17), th:nth-child(18) {
            width: 35px !important;
        }

        .table {
            font-size: smaller;
            font-weight: bold;
            color: #000;
            border-collapse: collapse;
            border-spacing: 0;
            width: 100%;
            border: 1px solid #ddd;
            text-align: left;
            padding: 8px;
            white-space: nowrap;
        }


            .table tfoot input[type=text] {
                font-size: 10px;
                width: inherit;
            }

        .dataTables_scrollFoot tfoot input[type=text] {
            width: -webkit-fill-available !important;
        }

        .chosen-drop {
            width: max-content !important;
        }

        .chosen-search-input {
            color: #000;
        }

        .chosen-container a span {
            color: #000;
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
                                        <img src="../Assets/images/pages.png" alt="" width="16px" />
                                    </span>
                                    <h3 class="page-title-head d-inline-block">Stock Monitor Report Details</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Stores</a></li>
                                <li class="active breadcrumb-item" aria-current="page">Stock monitor Report</li>
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
            <asp:UpdatePanel ID="upDocumenttype" runat="server">
                <Triggers>
                    <%-- <asp:PostBackTrigger ControlID="btnSave" />--%>
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" class="btnAddNew" runat="server">
                            <div class="text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right p-t-5">
                                        <label class="form-label">
                                            Select MRN</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:RadioButtonList ID="rblMRN" runat="server" CssClass="radio radio-success" RepeatDirection="Horizontal"
                                            onchange="rblChange();" RepeatLayout="Flow"
                                            TabIndex="5">
                                            <asp:ListItem Selected="True" Value="InStock">InStock</asp:ListItem>
                                            <asp:ListItem Value="ALL">All MRN</asp:ListItem>
                                        </asp:RadioButtonList>
                                        </textarea>
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
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-3">
                                                <div class="text-left">
                                                    <label style="font-weight: bold; color: #000;">
                                                        Category Name
                                                    </label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtCategoryName" PlaceHolder="Search Category Name" onkeyup="return ColumnChange();" CssClass="form-control mandatoryfield"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="text-left">
                                                    <label style="font-weight: bold; color: #000;">
                                                        MRN NO
                                                    </label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtMRNNo" PlaceHolder="Search MRN No" onkeyup="return ColumnChange();" CssClass="form-control mandatoryfield"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="text-left">
                                                    <label style="font-weight: bold; color: #000;">
                                                        Type
                                                    </label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtType" PlaceHolder="Search Type" onkeyup="return ColumnChange();" CssClass="form-control mandatoryfield"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="text-left">
                                                    <label style="font-weight: bold; color: #000;">
                                                        Grade
                                                    </label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtGrade" PlaceHolder="Search Grade" onkeyup="return ColumnChange();" CssClass="form-control mandatoryfield"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">

                                            <div class="col-sm-3">
                                                <div class="text-left">
                                                    <label style="font-weight: bold; color: #000;">
                                                        Thickness
                                                    </label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtTHK" PlaceHolder="Search THK" onkeyup="return ColumnChange();" CssClass="form-control mandatoryfield"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="text-left">
                                                    <label style="font-weight: bold; color: #000;">
                                                        Location
                                                    </label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtLocation" PlaceHolder="Search Location" onkeyup="return ColumnChange();" CssClass="form-control mandatoryfield"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="col-sm-3" style="border: 1px solid; padding: 10px; box-shadow: 0px 10px #888888;">
                                                <div class="text-left">
                                                    <label style="font-weight: bold; color: #000;">
                                                        LW - Length and Width
                                                    </label>
                                                </div>
                                                <div class="text-left">
                                                    <label style="font-weight: bold; color: #000;">
                                                        THK - Thickness
                                                    </label>
                                                </div>
                                                <div class="text-left">
                                                    <label style="font-weight: bold; color: #000;">
                                                        I-Qty - Inward Quantity
                                                    </label>
                                                </div>
                                                <div class="text-left">
                                                    <label style="font-weight: bold; color: #000;">
                                                        B-Qty - Blocked Quantity
                                                    </label>
                                                </div>
                                            </div>


                                            <div class="col-sm-3" style="border: 1px solid; padding: 10px; box-shadow: 5px 10px #888888;">
                                                <div class="text-left">
                                                    <label style="font-weight: bold; color: #000;">
                                                        C-Qty - Consumed Quantity
                                                    </label>
                                                </div>
                                                <div class="text-left">
                                                    <label style="font-weight: bold; color: #000;">
                                                        A-Stock - Actual Stock
                                                    </label>
                                                </div>
                                                <div class="text-left">
                                                    <label style="font-weight: bold; color: #000;">
                                                        In-Qty - InStock Quantity
                                                    </label>
                                                </div>
                                                <div class="text-left">
                                                    <label style="font-weight: bold; color: #000;">
                                                        U-Cost - Unit Cost
                                                    </label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <table class="table table-hover table-bordered medium " style="width: 100%;"
                                                id="gvStockMonitorDetails">
                                                <thead>
                                                    <tr>



                                                        <th>CategoryName</th>
                                                        <th>MRN No</th>
                                                        <th>Type</th>
                                                        <th>Grade</th>
                                                        <th title="Length and Width">LW</th>
                                                        <th title="Thickness">THK</th>
                                                        <%--<th>Inward Qty</th>--%>
                                                        <th title="Inward Quantity">I-Qty</th>
                                                        <th>UOM</th>
                                                        <%--<th>Blocked Qty</th>--%>
                                                        <th title="Blocked Quantity">B-Qty</th>
                                                        <%--<th>Consumed Qty</th>--%>
                                                        <th title="Consumed Quantity">C-Qty</th>
                                                        <%--<th>Actual Stock</th>--%>
                                                        <th title="Actual Stock">A-Stock</th>
                                                        <%--<th>InStock Qty</th>--%>
                                                        <th title="InStock Quantity">In-Qty</th>
                                                        <%--<th>Unit Cost</th>--%>
                                                        <th title="Unit Cost">U-Cost</th>
                                                        <%--<th>Total Cost</th>--%>
                                                        <th title="Total Cost">Total</th>
                                                        <th>RFP No</th>
                                                        <th>Location</th>
                                                        <th>MRN Print </th>
                                                        <th>Certificates </th>
                                                    </tr>
                                                </thead>
                                                <%-- <tfoot>
                                                </tfoot>--%>
                                            </table>
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
    <div class="modal" id="mpeView" style="overflow-y: scroll;">
        <div style="max-width: 81%; padding-left: 10%; padding-top: 5%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="upView" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">Certficate Details</h4>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="docdiv" class="docdiv">
                                <div class="inner-container">
                                    <div id="Certificates" runat="server">
                                        <div id="divAddItems" class="divInput" runat="server">
                                            <div class="ip-div text-center">
                                            </div>
                                        </div>
                                        <div id="divOutputsItems" runat="server">
                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvCertificates" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                    EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Certificate Name" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCertificatename" runat="server" Text='<%# Eval("CertificateName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CertficateNo" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCertficateNo" Width="50px" runat="server" Text='<%# Eval("CertficateNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hdnSPODID" runat="server" Value="0" />
                        <div class="modal-footer">
                            <div class="col-sm-12 p-t-10">
                                <div class="col-sm-10">
                                </div>
                                <div class="col-sm-2">
                                    <div class="form-group">
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
