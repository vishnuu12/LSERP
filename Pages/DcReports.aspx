<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="DcReports.aspx.cs" Inherits="Pages_DcReports" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script type="text/javascript">

         function ColumnChange() {
             $('#tblDcReport').DataTable().draw();
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

             var Mode = $("input[type='radio']:checked").val();

             rblChange(Mode);

         });


         function rblChange() {
             var Mode = $("input[type='radio']:checked").val();
            
             if (Mode == 0) {
                 BindDate(Mode);
             }
             else if (Mode == '1') {
                 InternalData(Mode);
             }
             else if (Mode == '2') {
                 OtherData(Mode);
             }
         }

         function BindDate(Mode) {
             $('#ContentPlaceHolder1_divVendor').show();
             $('#ContentPlaceHolder1_divInternal').hide();
             $('#ContentPlaceHolder1_divOther').hide();
             $('#tblDcReport tfoot').empty();
             $('.dataTables_scrollFoot table tfoot').empty();
             $('#tblDcReport').DataTable().destroy();

             $('#tblDcReport tfoot').append("<tr></tr>");
             $('#tblDcReport thead th').each(function () {
                 var title = $(this).text();
                 $('#tblDcReport tfoot tr').append('<th> <input type="text" class="form-control" placeholder="Search ' + title + '" /> </th>');
             });
             $('#tblDcReport').DataTable({
                 initComplete: function () {
                     // Apply the search
                     this.api().columns().every(function () {
                         var that = this;
                         $('input', this.footer()).on('keyup change clear', function () {
                             if (that.search() !== this.value) {
                                 that
                                     .search(this.value)
                                     .draw();
                             }
                         });
                     });
                 },

                 "retrieve": true,
                 "processing": true,
                 "serverSide": true,
                 "ordering": false,
                 "bSort": false,
                 "bDeferRender": true,
                 "pageLength": 20,
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
                     url: "DcReports.aspx/GetData", //It calls our web method  
                     contentType: "application/json; charset=utf-8",
                     dataType: "json",
                     "data": function (d) {
                         d.Mode = Mode;
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
                             return `<a href="#" onclick="return VendorDC('${data.VDCID}')";><img src= "../Assets/images/print.png"></a>`
                         }
                     },
                     {
                         'data': null,
                         "mRender": function (data, type, full) {
                             return `<a href="#" onclick="return POPrint('${data.WPOID}')";><img src= "../Assets/images/print.png"></a>`
                         }
                     },
                     { 'data': 'DCNo' },
                     { 'data': 'DCDate' },
                     { 'data': 'FormJJNo' },
                     { 'data': 'TariffClassification' },
                     { 'data': 'Duration' },
                     { 'data': 'DutyDetailsDate', },
                     { 'data': 'Location' },
                     { 'data': 'RawMatQty' },
                     { 'data': 'Value' },
                    
                 ],


                 scrollY: 550,
                 scrollX: 550,
                 fixedHeader: true
             });
         }

         function InternalData(Mode) {
             $('#ContentPlaceHolder1_divInternal').show();
             $('#ContentPlaceHolder1_divVendor').hide();
             $('#ContentPlaceHolder1_divOther').hide();
             $('#tblInternalDcReport tfoot').empty();
             $('.dataTables_scrollFoot table tfoot').empty();
             $('#tblInternalDcReport').DataTable().destroy();

             $('#tblInternalDcReport tfoot').append("<tr></tr>");
             $('#tblInternalDcReport thead th').each(function () {
                 var title = $(this).text();
                 $('#tblInternalDcReport tfoot tr').append('<th> <input type="text" class="form-control" placeholder="Search ' + title + '" /> </th>');
             });
             $('#tblInternalDcReport').DataTable({
                 initComplete: function () {
                     // Apply the search
                     this.api().columns().every(function () {
                         var that = this;
                         $('input', this.footer()).on('keyup change clear', function () {
                             if (that.search() !== this.value) {
                                 that
                                     .search(this.value)
                                     .draw();
                             }
                         });
                     });
                 },

                 "retrieve": true,
                 "processing": true,
                 "serverSide": true,
                 "ordering": false,
                 "bSort": false,
                 "bDeferRender": true,
                 "pageLength": 20,
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
                     url: "DcReports.aspx/GetData", //It calls our web method  
                     contentType: "application/json; charset=utf-8",
                     dataType: "json",
                     "data": function (d) {
                         d.Mode = Mode;
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
                             return `<a href="#" onclick="return PrintDC('${data.IDCID}')";><img src= "../Assets/images/print.png"></a>`
                         }
                     },
                     { 'data': 'DCNumber' },
                     { 'data': 'CreatedBy' },
                     { 'data': 'DCDate' },
                     { 'data': 'FromUnit' },
                     { 'data': 'ToUnit' },
                     { 'data': 'Duration', },
                     { 'data': 'remarks' },
                     
                 ],


                 scrollY: 550,
                 scrollX: 550,
                 fixedHeader: true
             });
         }

         function OtherData(Mode) {
             $('#ContentPlaceHolder1_divOther').show();
             $('#ContentPlaceHolder1_divInternal').hide();
             $('#ContentPlaceHolder1_divVendor').hide();
             //tblDcReport.style.visibility = "hidden";
             $('#tblOtherDcReport tfoot').empty();
             $('.dataTables_scrollFoot table tfoot').empty();
             $('#tblOtherDcReport').DataTable().destroy();

             $('#tblOtherDcReport tfoot').append("<tr></tr>");
             $('#tblOtherDcReport thead th').each(function () {
                 var title = $(this).text();
                 $('#tblInternalDcReport tfoot tr').append('<th> <input type="text" class="form-control" placeholder="Search ' + title + '" /> </th>');
             });
             $('#tblOtherDcReport').DataTable({
                 initComplete: function () {
                     // Apply the search
                     this.api().columns().every(function () {
                         var that = this;
                         $('input', this.footer()).on('keyup change clear', function () {
                             if (that.search() !== this.value) {
                                 that
                                     .search(this.value)
                                     .draw();
                             }
                         });
                     });
                 },

                 "retrieve": true,
                 "processing": true,
                 "serverSide": true,
                 "ordering": false,
                 "bSort": false,
                 "bDeferRender": true,
                 "pageLength": 20,
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
                     url: "DcReports.aspx/GetData", //It calls our web method  
                     contentType: "application/json; charset=utf-8",
                     dataType: "json",
                     "data": function (d) {
                         d.Mode = Mode;
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
                             return `<a href="#" onclick="return PrintODC('${data.OTHID}')";><img src= "../Assets/images/print.png"></a>`
                         }
                     },
                     { 'data': 'DCNo' },
                     { 'data': 'ToUNitAddress' },
                     { 'data': 'Remarks' },
                     
                 ],


                 scrollY: 550,
                 scrollX: 550,
                 fixedHeader: true
             });
         }

         function VendorDC(index) {
             __doPostBack("PrintVendorDC", index);
             return false;
         }

         function PrintVendorDC() {
             var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

             var TINNo = $('#ContentPlaceHolder1_hdnTINNo').val();
             var CodeNo = $('#ContentPlaceHolder1_hdnCodeNo').val();
             var CSTNo = $('#ContentPlaceHolder1_hdnCSTNo').val();
             var GSTNumber = $('#ContentPlaceHolder1_hdnGSTNumber').val();
             var Address = $('#ContentPlaceHolder1_hdnAddress').val();
             var CompanyName = $('#ContentPlaceHolder1_hdnCompanyName').val();

             var divc1 = $('#ContentPlaceHolder1_divc1').html();
             var divc3 = $('#ContentPlaceHolder1_divc3').html();


             winprint.document.write("<html><head><title>");
             winprint.document.write("Offer");
             winprint.document.write("</title>");

             winprint.document.write("<link rel='stylesheet' type='text/css' href='../Assets/css/ep-style.css'/>");
             winprint.document.write("<link rel='stylesheet' type='text/css' href='../Assets/css/main.css' type='text/css'/>");
             winprint.document.write("<link rel='stylesheet'  href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/print.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");

            winprint.document.write("<div class='print-page'>");
            winprint.document.write("<table style='border-width:0px'><thead><tr><td>");
            winprint.document.write("<div class='col-sm-12 header-space' style='text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;height:120px'>");
            winprint.document.write("<div class='header' style='background:transparent;border:0px solid #000'>");
            winprint.document.write("<div>");
            winprint.document.write("<div class='col-sm-12 padding0' id='divLSERPLogo' style='margin:0px auto;display:block;padding:5px 0px;'>");

            winprint.document.write("<div class='col-sm-12 text-center'>");
            winprint.document.write("<h3 style='font-weight:600;font-size:24px;color:#000;font-family: Arial;display: contents;font-weight:700'>" + CompanyName + "</h3>");
            winprint.document.write("<p style='font-weight:500;color:#000; word-break: break-word;'>" + Address + " <span> | </span>GST No:" + GSTNumber + " <span> | </span></p>");

            winprint.document.write("</div>");
            winprint.document.write("</div></div></div>");
            winprint.document.write("</td></tr></thead>");
            winprint.document.write("<tbody><tr><td>");
            winprint.document.write("<div class='col-sm-12 padding:0' style='padding-top:0px;'>");

            winprint.document.write(divc1);

            winprint.document.write("<div class='col-sm-12 p-t-10'>");

            winprint.document.write("<table class='table table-hover table-bordered medium' cellspacing='0' rules='all' border='1' id='ContentPlaceHolder1_gvPurchaseOrder_P' style='border-collapse:collapse;'>");
            winprint.document.write("<tbody>");

            var mergerowlen = $('#ContentPlaceHolder1_gvWorkOrderPOItemDetails_p').find('tr').length - 1;

            $('#ContentPlaceHolder1_gvWorkOrderPOItemDetails_p > tbody > tr').each(function (index, tr) {
                if (index == 0)
                    winprint.document.write("<tr align='center'>" + $('#ContentPlaceHolder1_gvWorkOrderPOItemDetails_p').find('tr')[0].innerHTML + "</tr>");
                else {
                    winprint.document.write("<tr align='center'><td>" + $(this).find('td')[0].innerHTML + "</td>");
                    winprint.document.write("<td>" + $(this).find('td')[1].innerHTML + "</td>");
                    if (index == 1) {
                        winprint.document.write("<td rowspan='" + mergerowlen + "'>" + $(this).find('td')[2].innerHTML + "</td>");
                        winprint.document.write("<td rowspan='" + mergerowlen + "'>" + $(this).find('td')[3].innerHTML + "</td>");
                    }
                    winprint.document.write("</tr>");
                }
            });


            winprint.document.write("</tbody>");
            winprint.document.write("</table>");

            winprint.document.write("</div>");

            winprint.document.write(divc3);

            winprint.document.write("</div>");
            winprint.document.write("</td></tr></tbody>");
            winprint.document.write("<tfoot><tr><td>");
            winprint.document.write("</td></tr></tfoot></table>");
            winprint.document.write("</div>");
            winprint.document.write("</html>");


            setTimeout(function () {
                winprint.document.close();
                winprint.focus();
                winprint.print();
            }, 1000);
         }
         function POPrint(index) {
             __doPostBack("PrintPO", index);
             return false;
         }

         function PrintDC() {

             var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

             var Address = $('#ContentPlaceHolder1_hdnAddress').val();
             var PhoneAndFaxNo = $('#ContentPlaceHolder1_hdnPhoneAndFaxNo').val();
             var Email = $('#ContentPlaceHolder1_hdnEmail').val();
             var WebSite = $('#ContentPlaceHolder1_hdnWebsite').val();

             var DocNo = $('#ContentPlaceHolder1_hdnDocNo').val();
             var RevNo = $('#ContentPlaceHolder1_hdnRevNo').val();
             var RevDate = $('#ContentPlaceHolder1_hdnRevDate').val();

             var CAPAContent = $('#ContentPlaceHolder1_divCAPAReport_p').html();

             winprint.document.write("<html><head><title>");
             winprint.document.write("Offer");
             winprint.document.write("</title>");
             winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
             winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/printg.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
             winprint.document.write("<style type='text/css'> label { font-weight: bold ! important;color: #000 ! important; } .contractorborder label{padding: 4px 6px; } .contractorborder { padding-top:5px; border: 1px solid;} .sideheading{ display:flex;align-items:flex-start;justify-content:center;margin-top:15px; } .lbl_h{ width:35%; } .lbl_v{ width:65%; } .Qrcode {height: 60px;width: 80px;margin-right: 10px;padding-left: 20px;}</style>");

             winprint.document.write("<div class='print-page jobcardprintoutermargin'></div>");
             winprint.document.write("<table class='jobcardprinttablemargin'><thead><tr><td>");
             winprint.document.write("<div class='col-sm-12 jobcardprinttheadheight' style='text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;'>");
             winprint.document.write("<div>");
             winprint.document.write("<div>");
             winprint.document.write("<div class='col-sm-12 padding0' id='divLSERPLogo' style='margin:20px auto;display:block;'>");
             winprint.document.write("<div class='row jobcardprinttheadwidth'>");
             winprint.document.write("<div class='col-sm-3'>");
             winprint.document.write("<img src='../Assets/images/lonestar.jpeg' alt='lonestar-image' width='90px' style='margin:5px 0;'>");
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
             winprint.document.write("<img src='../Assets/images/iso.jpeg' alt='lonestar-image' height='90px' style='margin:5px 0;'>");
             winprint.document.write("</div></div>");
             winprint.document.write("</div></div></div>");
             winprint.document.write("</td></tr></thead>");
             winprint.document.write("<tbody><tr><td>");

             winprint.document.write("<div style='margin:0 5mm;'>");
             winprint.document.write(CAPAContent);

             winprint.document.write("</div>");

             winprint.document.write("</td></tr></tbody>");
             winprint.document.write("<tfoot><tr><td>");
             winprint.document.write("<div class='jobcardprinttfootheight'>");
             winprint.document.write("<div class='row jobcardprinttfootwidth'>");
             winprint.document.write("<div class='col-sm-2 text-center'>");
             winprint.document.write("</div>");
             winprint.document.write("<div class='col-sm-7 text-center'>");
             winprint.document.write("<label style='width:30%;color:black;float:left; font-weight:bolder;border: 1px solid black;'>Doc No : " + DocNo + "</label>");
             winprint.document.write("<label style='width:25%;color:black;float:left; font-weight:bolder;border: 1px solid black;'>Rev No : " + RevNo + "</label>");
             winprint.document.write("<label style='width:40%;color:black;float:left; font-weight:bolder;border: 1px solid black;'>Rev Date : " + RevDate + "</label>");
             winprint.document.write("</div>");
             winprint.document.write("<div class='col-sm-3 text-center'>");
             winprint.document.write("</div>");
             winprint.document.write("</div>");
             winprint.document.write("</td></tr></tfoot></table>");
             winprint.document.write("</body></html>");

             setTimeout(function () {
                 winprint.document.close();
                 winprint.focus();
                 winprint.print();
             }, 1000);
         }

         function PrintDC(IDCID) {
             __doPostBack('PrintDC', IDCID);
         }

         function PrintODC() {
             var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

             var Address = $('#ContentPlaceHolder1_hdnAddress').val();
             var PhoneAndFaxNo = $('#ContentPlaceHolder1_hdnPhoneAndFaxNo').val();
             var Email = $('#ContentPlaceHolder1_hdnEmail').val();
             var WebSite = $('#ContentPlaceHolder1_hdnWebsite').val();

             var DocNo = $('#ContentPlaceHolder1_hdnDocNo').val();
             var RevNo = $('#ContentPlaceHolder1_hdnRevNo').val();
             var RevDate = $('#ContentPlaceHolder1_hdnRevDate').val();

             var CAPAContent = $('#ContentPlaceHolder1_divCAPAReport_p').html();

             winprint.document.write("<html><head><title>");
             winprint.document.write("Offer");
             winprint.document.write("</title>");
             winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
             winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/printg.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
             winprint.document.write("<style type='text/css'> label { font-weight: bold ! important;color: #000 ! important; } .contractorborder label{padding: 4px 6px; } .contractorborder { padding-top:5px; border: 1px solid;} .sideheading{ display:flex;align-items:flex-start;justify-content:center;margin-top:15px; } .lbl_h{ width:35%; } .lbl_v{ width:65%; } .Qrcode {height: 60px;width: 80px;margin-right: 10px;padding-left: 20px;}</style>");

             winprint.document.write("<div class='print-page jobcardprintoutermargin'></div>");
             winprint.document.write("<table class='jobcardprinttablemargin'><thead><tr><td>");
             winprint.document.write("<div class='col-sm-12 jobcardprinttheadheight' style='text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;'>");
             winprint.document.write("<div>");
             winprint.document.write("<div>");
             winprint.document.write("<div class='col-sm-12 padding0' id='divLSERPLogo' style='margin:20px auto;display:block;'>");
             winprint.document.write("<div class='row jobcardprinttheadwidth'>");
             winprint.document.write("<div class='col-sm-3'>");
             winprint.document.write("<img src='../Assets/images/lonestar.jpeg' alt='lonestar-image' width='90px' style='margin:5px 0;'>");
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
             winprint.document.write("<img src='../Assets/images/iso.jpeg' alt='lonestar-image' height='90px' style='margin:5px 0;'>");
             winprint.document.write("</div></div>");
             winprint.document.write("</div></div></div>");
             winprint.document.write("</td></tr></thead>");
             winprint.document.write("<tbody><tr><td>");

             winprint.document.write("<div style='margin:0 5mm;'>");
             winprint.document.write(CAPAContent);

             winprint.document.write("</div>");

             winprint.document.write("</td></tr></tbody>");
             winprint.document.write("<tfoot><tr><td>");
             winprint.document.write("<div class='jobcardprinttfootheight'>");
             winprint.document.write("<div class='row jobcardprinttfootwidth'>");
             winprint.document.write("<div class='col-sm-2 text-center'>");
             winprint.document.write("</div>");
             winprint.document.write("<div class='col-sm-7 text-center'>");
             winprint.document.write("<label style='width:30%;color:black;float:left; font-weight:bolder;border: 1px solid black;'>Doc No : " + DocNo + "</label>");
             winprint.document.write("<label style='width:25%;color:black;float:left; font-weight:bolder;border: 1px solid black;'>Rev No : " + RevNo + "</label>");
             winprint.document.write("<label style='width:40%;color:black;float:left; font-weight:bolder;border: 1px solid black;'>Rev Date : " + RevDate + "</label>");
             winprint.document.write("</div>");
             winprint.document.write("<div class='col-sm-3 text-center'>");
             winprint.document.write("</div>");
             winprint.document.write("</div>");
             winprint.document.write("</td></tr></tfoot></table>");
             winprint.document.write("</body></html>");

             setTimeout(function () {
                 winprint.document.close();
                 winprint.focus();
                 winprint.print();
             }, 1000);
         }

         function PrintODC(IDCID) {
             __doPostBack('PrintODC', IDCID);
         }
     </script>
      <style type="text/css">
        .dataTable > thead > tr > th[class*="sort"]:before,
        .dataTable > thead > tr > th[class*="sort"]:after {
            content: "" !important;
        }

        .table {
            font-family: serif;
            font-size: 10px;
            color: #000;
            border-collapse: collapse;
            border-spacing: 0;
            width: 100%;
            border: 1px solid #ddd;
            text-align: left;
            white-space: nowrap;
        }
            .table tfoot input[type=text] {
                font-size: 10px;
                width: 40px !important;
            }

        .dataTables_scrollFoot tfoot input[type=text] {
            width: 40px !important;
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                                    <h3 class="page-title-head d-inline-block">DC Reports </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Reports</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">DC Reports</li>
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
            <asp:UpdatePanel ID="upDC" runat="server" UpdateMode="Always">
                <Triggers>
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                           <div id="divradio" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-2">
                                        <label class="form-label" style="padding-top: 7px;">
                                            Select Type
                                        </label>
                                    </div>
                                    <div class="col-sm-8">
                                        <asp:RadioButtonList ID="dcChange" runat="server" CssClass="radio radio-success" RepeatDirection="Horizontal"
                                            OnClick="rblChange();" RepeatLayout="Flow"
                                            TabIndex="5">
                                            <asp:ListItem Selected="True" Value="0">Vendor DC</asp:ListItem>
                                            <asp:ListItem Value="1">Internal DC</asp:ListItem>
                                            <asp:ListItem Value="2">Other DC</asp:ListItem>
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
                            </div>
                        </div>
                        <div id="divVendor" class="output_section" runat="server" >
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">

                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <table class="table table-hover table-bordered medium uniquedatatable"
                                                id="tblDcReport"
                                                width="100%">
                                                <thead>
                                                    <tr>
                                                        <th>Print DC</th>
                                                        <th>PO Print</th>
                                                       <th>DC No</th>
                                                        <th>DC Date</th>
                                                        <th>E-Way Bill No</th>
                                                        <th>Tarrif Classification</th>
                                                        <th>Duration</th>
                                                        <th>E-way Bill Date</th>
                                                        <th>Location</th>
                                                        <th>RM QTY</th>
                                                        <th>Value</th>
                                                    </tr>
                                                </thead>
                                                <tfoot>
                                                </tfoot>
                                            </table>
                                            <asp:HiddenField ID="hdnVDCID" runat="server" Value="0" />
                                             <asp:HiddenField ID="hdnWPOID" runat="server" Value="0" />

                                            <asp:HiddenField ID="hdnCodeNo" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnTINNo" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnCSTNo" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnGSTNumber" runat="server" Value="0" />

                                            <asp:HiddenField ID="hdnAddress" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnCompanyName" runat="server" Value="0" />
                                        </div>


                                    </div>
                                </div>
                            </div>
                        </div>
                          <div id="divInternal" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">

                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <table class="table table-hover table-bordered medium uniquedatatable"
                                                id="tblInternalDcReport"
                                                width="100%">
                                                <thead>
                                                    <tr>
                                                        <th>PDF</th>
                                                       <th>DC No</th>
                                                        <th>Created By</th>
                                                        <th>DC Date</th>
                                                        <th>From Unit</th>
                                                        <th>To Unit</th>
                                                        <th>Duration</th>
                                                        <th>Remarks</th>
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
                        <div id="divOther" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">

                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <table class="table table-hover table-bordered medium uniquedatatable"
                                                id="tblOtherDcReport"
                                                width="100%">
                                                <thead>
                                                    <tr>
                                                        <th>PDF</th>
                                                       <th>DC No</th>
                                                        <th>To Address</th>
                                                        <th>Remarks</th>
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
                    

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
     <div class="modal" id="mpeVendorDC_p">
        <div class="modal-dialog" style="height: 100%;">
            <div class="modal-content" style="height: 100%;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always" Style="height: 100%;">
                    <Triggers>
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
                                <div id="divVendorDCPrint_p" runat="server">

                                    <div id="divc1" runat="server">

                                        <div class="col-sm-12">
                                            <label>
                                                Vendor DC</label>
                                        </div>
                                        <div class="col-sm-12 text-center m-t-10">
                                            <label style="font-size: 16px !important; font-weight: 700">
                                                Bellows Devision
                                            </label>
                                            <br />
                                            <label style="font-size: 16px !important; font-weight: 700">
                                                Delivery Challan
                                            </label>
                                        </div>
                                        <div class="col-sm-12 m-t-10">
                                            <label>
                                                [Challan for movement of inputs or partially processed goods under Rule 57F (4)
                                            and / or Notification No. 214/86, Dated 25.03.1986 from one factory to another factory
                                            for further processing / operation]
                                            </label>
                                        </div>
                                        <div class="col-sm-12" style="border: 1px solid #000; padding: 2px">
                                            <div class="col-sm-6" style="font-weight: 600; border: 1px solid #000">
                                                <label>
                                                    W.O.No:</label>
                                                <asp:Label ID="lblWONo_p" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-6" style="font-weight: 600; border: 1px solid #000">
                                                <label>
                                                    Form JJ No:</label>
                                                <asp:Label ID="lblFormJJno_p" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-12" style="border: 1px solid #000; padding: 2px">
                                            <div class="col-sm-6" style="font-weight: 600; border: 1px solid #000">
                                                <label>
                                                    D.C.No:</label>
                                                <asp:Label ID="lblDCno_p" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-6" style="font-weight: 600; border: 1px solid #000">
                                                <label>
                                                    Date:</label>
                                                <asp:Label ID="lblDate_p" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-12" style="font-weight: 600; border: 1px solid #000; padding: 2px">
                                            <label>
                                                To</label>
                                            <asp:Label ID="lblSuppliername_p" runat="server"></asp:Label>
                                            <div class="col-sm-12">
                                                <asp:Label ID="lblSupplierAddress_p" Style="font-weight: 400;" runat="server"></asp:Label>
                                            </div>
                                        </div>

                                    </div>

                                    <div id="divc2" runat="server">
                                        <div class="col-sm-12 p-t-10" style="overflow-x: auto;">
                                            <asp:GridView ID="gvWorkOrderPOItemDetails_p" runat="server" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartName" runat="server" Text='<%# Eval("ItemDescription")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Issued Quantity" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIssuedQty" runat="server" Text='<%# Eval("IssuedQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Value" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblValue" runat="server" Text='<%# Eval("Value")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                    <div id="divc3" runat="server">

                                        <div class="col-sm-12">
                                            Not for sale
                                        </div>
                                        <div class="col-sm-12" style="border: 1px solid #000; padding: 2px">
                                            <div class="col-sm-6 text-left" style="border: 1px solid #000">
                                                <label>
                                                    Tariff Classification</label>
                                            </div>
                                            <div class="col-sm-6" style="border: 1px solid #000">
                                                <asp:Label ID="lbltarrifClassification_p" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-12" style="border: 1px solid #000; padding: 2px">
                                            <div class="col-sm-6 text-left" style="border: 1px solid #000">
                                                <label>
                                                    Expected Duration Of Processing / Manufacturing</label>
                                            </div>
                                            <div class="col-sm-6" style="border: 1px solid #000">
                                                <asp:Label ID="lblExpectedDurationofProcessing_p" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-6">
                                                <label style="padding-top: 50px">
                                                    Received By</label>
                                            </div>
                                            <div class="col-sm-6 text-center">
                                                <div style="display: flex;justify-content: center;"> 
                                                    <p >FOR</p>
                                                <p style="text-indent: 10px;" align="right" ID="CompanyName_P" runat="server"></p>
                                                </div>
                                               
                                               
                                                <label style="padding-top: 30px">
                                                    Signature</label>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 text-center" style="margin-top: 20px">
                                            <label style="font-weight: 700;">
                                                TO BE FILLED BY THE PROCESSING FACTORY IN ORIGINAL AND DUPLICATE CHALLAN</label>
                                        </div>
                                        <div class="col-sm-12 text-left" style="border: 1px solid #000; color: #000">
                                            <div class="col-sm-8" style="border-right: 1px solid #000; font-size: 12px">
                                                Date and time of dispatch of finished goods to parent factory / another manufacturer
                                                and Entry No. and date of receipt in the account in the processing factory or Date
                                                & time of dispatch of finished goods without payment of duty for export under bond
                                                or on payment of duty for export or on payment of duty for home consumption G.P
                                                No. and date. Quantum of duty paid (Both figures & words)
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 text-left" style="border: 1px solid #000; color: #000">
                                            <div class="col-sm-8" style="border-right: 1px solid #000; font-size: 12px">
                                                Quantity dispatch (Nos. Weight / Litre / Metre) as entered in Account
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 text-left" style="border: 1px solid #000; color: #000">
                                            <div class="col-sm-8" style="border-right: 1px solid #000; font-size: 12px">
                                                Nature of processing / Manufacturing done
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 text-left" style="border: 1px solid #000; color: #000">
                                            <div class="col-sm-8" style="border-right: 1px solid #000; font-size: 12px">
                                                Quantity of waste material returned to the parent factory or cleared for home consumption
                                                Invoice No. & Date. Quantum of duty paid (Both figure & words)
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                        <div class="col-sm-12" style="margin-top: 20px; color: #000">
                                            <div class="col-sm-6 text-left">
                                                Place:
                                            </div>
                                            <div class="col-sm-6 text-center">
                                                Signature Of Processor
                                            </div>
                                        </div>
                                        <div class="col-sm-12" style="margin-top: 50px; color: #000">
                                            <div class="col-sm-6 text-left">
                                                Date:
                                            </div>
                                            <div class="col-sm-6 text-center">
                                                Name Of Factory:
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
    </div>

</asp:Content>

