<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="CrashReport.aspx.cs" Inherits="Pages_CrashReport" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {
            BindDate();

        });
        function BindDate() {
            $('#tblEnquiryHeader').dataTable({
                "iDisplayLength": 50,
                "bJQueryUI": true,
                "bAutoWidth": false,
                "scrollX": true,
                "retrieve": true,
                "processing": true,
                "serverSide": true,
                "fnInitComplete": function () {

                    // Enable THEAD scroll bars
                    $('.dataTables_scrollHead').css('overflow', 'auto');

                    // Sync THEAD scrolling with TBODY
                    $('.dataTables_scrollHead').on('scroll', function () {
                        $('.dataTables_scrollBody').scrollLeft($(this).scrollLeft());
                    });
                },
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
                    url: "CrashReport.aspx/GetData", //It calls our web method  
                    // url:"https://innovasphere.com/LSERP/pages/PurchaseIndentMaterialInwardStatusReport/GetData",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    "data": function (d) {
                        d.ItemName = $('#ContentPlaceHolder1_txtitemname').val().trim(),
                            d.Size = $('#ContentPlaceHolder1_txtSize').val().trim(),
                            d.Temp = $('#ContentPlaceHolder1_txttemp').val().trim(),
                            d.Pressure = $('#ContentPlaceHolder1_txtPressure').val().trim(),
                            d.Application = $('#ContentPlaceHolder1_txtApplication').val().trim(),
                            d.Movement = $('#ContentPlaceHolder1_txtmovement').val().trim(),
                            d.TagNo = $('#ContentPlaceHolder1_txttagNo').val().trim(),
                            d.Location = $('#ContentPlaceHolder1_txtLocation').val().trim()
                        // "TagNo", "Movement", "Location"
                        //d.
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
                    {
                        'data': null,
                        "mRender": function (data, type, full) {
                            return '<a href="#" id="' + data.EnquiryID + '" onclick="return showpopup()";>' + data.CustomerEnquiryNumber + '</a>';
                        }
                    },

                    { 'data': 'ProspectName' },
                    { 'data': 'ItemName' },
                    { 'data': 'Size' },
                    { 'data': 'Temperature' },
                    { 'data': 'Pressure' },
                    { 'data': 'EnquiryApplication' },
                    { 'data': 'TagNo' },
                    { 'data': 'Location' },
                    { 'data': 'OfferNo' },
                    { 'data': 'PORefNo' },
                    { 'data': 'RFPNo' }
                ],
                "autoWidth": false
            });
        }

        function IndividualChange() {
            $('#tblEnquiryHeader').DataTable().draw();
            //$('#ContentPlaceHolder1_txttemp').on('keyup', function () {
            //    //table.search(this.value).draw();
            //});
            return false;
        }


        // Movement

        function showpopup() {
            var val = $(event.target).closest('a').attr('id');
            $('#ContentPlaceHolder1_hdnEnquiryID').val(val);
            $('#tbItemdetails tbody').find('tr').remove();
            jQuery.ajax({
                type: "GET",
                url: "CrashReport.aspx/GetItemStatusDetailsByEnquiryID", //It calls our web method  
                contentType: "application/json; charset=utf-8",
                dataType: "JSON",
                data: {
                    EnquiryID: val,
                },
                success: function (data) {
                    if (data.d[0] != "") {
                        var d = JSON.parse(data.d[0]);
                        //QPStage1Status	QPStage2Status	MPStatus                    
                        $.each(d, function (i, val) {
                            $('#tbItemdetails tbody').append('<tr><td>' + parseInt(i + 1) + ' </td><td> '
                                + d[i].ItemName + ' </td> <td> ' + d[i].ItemSize + ' </td> <td> '
                                + d[i].TagNo + '</td> <td>' + d[i].Pressure + '</td> <td> ' + d[i].Temperature + ' </td> <td> ' + d[i].Movement + ' </td> </tr>');
                        });
                        BindDatatable('tbItemdetails');
                    }
                    else {
                        $('#tbItemdetails tbody').find('tr').remove();
                        $('#tbItemdetails tbody').append('<tr><td colspan="7"> No Data Found </td> </tr>');
                    }
                    if (data.d[1] != "") {
                        var res1 = JSON.parse(data.d[1]);
                        $('#lblEnquiryNo_h').text(res1[0].CustomerEnquiryNo);
                    }
                    else
                        $('#lblEnquiryNo_h').text("");

                    if (data.d[2] != "") {
                        $('#tblEnquiryDetails tbody').find('tr').remove();
                        var d = JSON.parse(data.d[2]);
                        $.each(d, function (i, val) {
                            $('#tblEnquiryDetails tbody').append('<tr><td>' + parseInt(i + 1) + ' </td><td> '
                                + d[i].ContactNumber + ' </td> <td> ' + d[i].ContactPerson + ' </td> <td> '
                                + d[i].Email + '</td> <td>' + d[i].UserName + '</td> <td> ' + d[i].EnquiryDate + ' </td></tr>');
                        });
                    }
                    else {
                        $('#tblEnquiryDetails tbody').find('tr').remove();
                        $('#tblEnquiryDetails tbody').append('<tr><td colspan="6"> No Data Found </td> </tr>');
                    }
                },
                error: function (data) {
                }
            });
            $('#mpeEnquiryDetails').modal('show');
        }

        function BindDatatable(id) {
            $('#' + id + '').DataTable({
                "retrieve": true,
                "pageLength": 10,
                //  responsive: true                             
            });
        }

        function viewAllEnquiryDocs() {
            var url = location.href;
            url = url.toLowerCase();
            var pagename = url.split('/');

            var Replacevalue = pagename[pagename.length - 1];
            var Page = url.replace(Replacevalue, "ViewAllDepartMentDocs.aspx?EnquiryID=" + $('#ContentPlaceHolder1_hdnEnquiryID').val() + "");

            window.open(Page, '_blank');
            return false;
        }

    </script>

    <style type="text/css">
        div.dataTables_scrollBody > table {
            border-top: none;
            margin-top: -19px !important;
            margin-bottom: 0 !important;
        }

        .modal-dialog {
            margin-left: 2%;
            max-width: 98%;
        }

        table tbody {
            font-weight: bold;
            color: #000;
            font-size: smaller;
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
                                    <h3 class="page-title-head d-inline-block">Crash Report </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Reports</a></li>
                                <li class="active breadcrumb-item" aria-current="page">Crash Report</li>
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
                        <div id="divAdd" runat="server">
                            <div class="ip-div text-center">
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
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
                                                        Item Name
                                                    </label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtitemname" PlaceHolder="Search Item Name" onkeyup="return IndividualChange();" CssClass="form-control mandatoryfield"
                                                        runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="text-left">
                                                    <label class="" style="font-weight: bold; color: #000;">Size  </label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtSize" PlaceHolder="Search Size" onkeyup="return IndividualChange();" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="text-left">
                                                    <label class="" style="font-weight: bold; color: #000;">Temp  </label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txttemp" PlaceHolder="Search Temp" onkeyup="return IndividualChange();" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="text-left">
                                                    <label class="" style="font-weight: bold; color: #000;">Pressure  </label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtPressure" PlaceHolder="Search Pressure" onkeyup="return IndividualChange();" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-3">
                                                <div class="text-left">
                                                    <label style="font-weight: bold; color: #000;">Application </label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtApplication" PlaceHolder="Search Application" onkeyup="return IndividualChange();" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="text-left">
                                                    <label style="font-weight: bold; color: #000;">Tag No </label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txttagNo" PlaceHolder="Search Tag No" onkeyup="return IndividualChange();" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="text-left">
                                                    <label style="font-weight: bold; color: #000;">Location </label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtLocation" PlaceHolder="Search Location" onkeyup="return IndividualChange();" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="text-left">
                                                    <label style="font-weight: bold; color: #000;">Movement </label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtmovement" PlaceHolder="Search Movement" onkeyup="return IndividualChange();" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <table class="table table-hover table-bordered medium uniquedatatable"
                                                id="tblEnquiryHeader" width="100%">
                                                <thead>
                                                    <tr>
                                                        <th>Enquiry Number</th>
                                                        <th>Customer Name</th>
                                                        <th>Item Name </th>
                                                        <th>Size </th>
                                                        <th>Temp </th>
                                                        <th>Pressure </th>
                                                        <th>Application </th>
                                                        <th>tag No </th>
                                                        <th>Location </th>
                                                        <th>Offer No</th>
                                                        <th>PO No</th>
                                                        <th>RFP No</th>
                                                    </tr>
                                                </thead>
                                                <tfoot>
                                                </tfoot>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hdnEnquiryID" Value="0" runat="server" />

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="modal" id="mpeEnquiryDetails" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="upView" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">
                                <label style="color: brown; font-weight: bold;" id="lblEnquiryNo_h"></label>
                            </h4>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="docdiv" class="docdiv">
                                <div class="inner-container">
                                    <div id="enquirydetailsdiv" class="enquirydetailsdiv" runat="server">
                                        <div class="col-sm-12 p-t-10 text-center">
                                            <button id="btnViewAllDocs" onclick="return viewAllEnquiryDocs();" class="btn btn-cons btn-success">View All Docs </button>
                                        </div>

                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <table class="table table-hover table-bordered medium uniquedatatable"
                                                id="tblEnquiryDetails"
                                                width="100%">
                                                <thead>
                                                    <tr>
                                                        <th>S No</th>
                                                        <th>Contact Number</th>
                                                        <th>Contact Person</th>
                                                        <th>Email</th>
                                                        <th>User Name</th>
                                                        <th>Enquiry Date</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                </tbody>
                                            </table>
                                        </div>

                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <table class="table table-hover table-bordered medium uniquedatatable"
                                                id="tbItemdetails"
                                                width="100%">
                                                <thead>
                                                    <tr>
                                                        <th>S.No </th>
                                                        <th>Item Name </th>
                                                        <th>Size </th>
                                                        <th>Tag No </th>
                                                        <th>Pressure </th>
                                                        <th>Temprature </th>
                                                        <th>Movement</th>
                                                    </tr>
                                                </thead>
                                                <tbody>
                                                </tbody>
                                            </table>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                            </div>
                        </div>
                        </div>
                        <asp:HiddenField ID="hdnAttachementID" runat="server" Value="0" />
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

    <div class="modal" id="mpepartdetailspopup" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">
                                <label id="lblPartHeading" style="color: brown;"></label>
                            </h4>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div class="docdiv">
                                <div class="inner-container">
                                    <div id="Div1" class="enquirydetailsdiv" runat="server">
                                        <div class="col-sm-12">
                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <table class="table table-hover table-bordered medium uniquedatatable"
                                                    id="tblPartDetails"
                                                    width="100%">
                                                    <thead>
                                                        <tr>
                                                            <th>S.No     </th>
                                                            <th>Part Name </th>
                                                            <th>QP Stage 1 </th>
                                                            <th>QP Stage 2 </th>
                                                            <th>MP Status  </th>
                                                            <th>Job Card Status  </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
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



