<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="RFPQCSItemStatusCard.aspx.cs"
    Inherits="Pages_RFPQCSItemStatusCard" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">

        table {
            color: #000;
            font-weight: bold;
        }

        .app-container {
            display: none;
        }

        .app-header {
            display: none;
        }

        .main-container_close {
            width: 98% !important;
            margin-left: 2% !important;
        }

        table#tblItem td, th {
            border: 1px solid black;
        }

    </style>

    <script type="text/javascript">

        function PrintRFPItemStatusCard() {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var Address = $('#ContentPlaceHolder1_hdnAddress').val();
            var PhoneAndFaxNo = $('#ContentPlaceHolder1_hdnPhoneAndFaxNo').val();
            var Email = $('#ContentPlaceHolder1_hdnEmail').val();
            var WebSite = $('#ContentPlaceHolder1_hdnWebsite').val();

            var DocNo = $('#ContentPlaceHolder1_hdnDocNo').val();
            var RevNo = $('#ContentPlaceHolder1_hdnRevNo').val();
            var RevDate = $('#ContentPlaceHolder1_hdnRevDate').val();

            var SheetMarkingAndCutting = $('#ContentPlaceHolder1_divItemQCStatusCard').html();


            winprint.document.write("<html><head><title>");
            winprint.document.write("Offer");
            winprint.document.write("</title>");
            winprint.document.write("<link rel='stylesheet'  href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/printg.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<style type='text/css'> label { font-weight: bold ! important;color: #000 ! important; } .contractorborder label{padding: 4px 6px; } .contractorborder { padding-top:5px; border: 1px solid;} .sideheading{ display:flex;align-items:flex-start;justify-content:center;margin-top:15px; } .lbl_h{ width:35%; } .lbl_v{ width:65%; } .Qrcode {height: 60px;width: 80px;margin-right: 10px;padding-left: 20px;} .table-bordered {border-collapse: collapse;} </style>");

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
            winprint.document.write(SheetMarkingAndCutting);

            winprint.document.write("</div>");

            winprint.document.write("</td></tr></tbody>");
            winprint.document.write("<tfoot><tr><td>");
            winprint.document.write("<div class='jobcardprinttfootheight'>");
            winprint.document.write("<div class='row jobcardprinttfootwidth'>");

            winprint.document.write("<div class='col-sm-2 text-center'>");

            winprint.document.write("<label style='color:black; font-weight:bolder;'>Quality Incharge</label>");
            winprint.document.write("<label style='color:black; font-weight:bolder;display:block;'>Sign & Date</label>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-7 text-center'>");
            winprint.document.write("<label style='width:30%;color:black;float:left; font-weight:bolder;border: 1px solid black;'>Doc No : " + DocNo + "</label>");
            winprint.document.write("<label style='width:25%;color:black;float:left; font-weight:bolder;border: 1px solid black;'>Rev No : " + RevNo + "</label>");
            winprint.document.write("<label style='width:40%;color:black;float:left; font-weight:bolder;border: 1px solid black;'>Rev Date : " + RevDate + "</label>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-3 text-center'>");
            winprint.document.write("<label style='color:black; font-weight:bolder;'> Production Incharge</label><label style='color:black; font-weight:bolder;display:block;'>Sign & Date</label>"); //<img src='" + QRCode + "' class='Qrcode'/>
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
                                    <h3 class="page-title-head d-inline-block">RFP Item QC Status Card </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Reports</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Status Card</li>
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
                    <asp:PostBackTrigger ControlID="btnprint" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" runat="server">
                            <div class="ip-div text-center">
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12 p-t-10">
                                    <asp:Label ID="lblRFPNo" Style="color: brown; font-weight: bold; font-size: x-large;"
                                        runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <asp:LinkButton ID="btnprint" runat="server" CssClass="btn btn-cons btn-success" Text="GET ITEM STATUS CARD PRINT" OnClick="btnprint_Click">
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="col-sm-12">
                                        <label style="font-size: 18px; color: #000;">
                                            Part Job Card QC Status
                                        </label>
                                    </div>
                                    <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                        <asp:GridView ID="gvRFPItemQCStatusCard" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Part Name" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPartName" runat="server" Text='<%# Eval("PartName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Job Type" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbljobtype" runat="server" Text='<%# Eval("JobType")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Category Name" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcategoryname" runat="server" Text='<%# Eval("CategoryName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MP Status" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMPStatus" runat="server" Text='<%# Eval("MPStatus")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="MRN QC Status" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMRNQCStatus" runat="server" Text='<%# Eval("MRNQCStatus")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cutting QC" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMRNQCStatus" runat="server" Text='<%# Eval("CuttingQC")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Welding QC" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMRNQCStatus" runat="server" Text='<%# Eval("WeldingQC")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Forming QC" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMRNQCStatus" runat="server" Text='<%# Eval("FormingQC")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                        <asp:HiddenField ID="hdnAddress" runat="server" Value="" />
                                        <asp:HiddenField ID="hdnPhoneAndFaxNo" runat="server" Value="" />
                                        <asp:HiddenField ID="hdnEmail" runat="server" Value="" />
                                        <asp:HiddenField ID="hdnWebsite" runat="server" Value="" />
                                        <asp:HiddenField ID="hdnDocNo" runat="server" Value="" />
                                        <asp:HiddenField ID="hdnRevNo" runat="server" Value="" />
                                        <asp:HiddenField ID="hdnRevDate" runat="server" Value="" />
                                    </div>
                                    <div class="col-sm-12">
                                        <label style="font-size: 18px; color: #000;">
                                            Assemply Welding Job Card QC Status
                                        </label>
                                    </div>
                                    <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                        <asp:GridView ID="gvRFPItemAssemplyWeldingQCStatusCard" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Part Name 1" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPartName1" runat="server" Text='<%# Eval("PartName1")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Part Name 2" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpartname2" runat="server" Text='<%# Eval("PartName2")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="WPS No" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblwpsno" runat="server" Text='<%# Eval("WPSNumber")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="QC Status" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblqcstatus" runat="server" Text='<%# Eval("QCStatus")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Request By" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrequestby" runat="server" Text='<%# Eval("RequestBy")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Request On" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRequestOn" runat="server" Text='<%# Eval("RequestOn")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="QC Done By" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblqcdoneby" runat="server" Text='<%# Eval("QCDoneBy")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="QC Done On" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblqcdoneon" runat="server" Text='<%# Eval("QCDoneOn")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div id="divItemQCStatusCard" style="display: none;" runat="server">
        <div class="col-sm-12 text-center" style="background-color: #b0acac; padding-top: 10px; padding-bottom: 10px;">
            <asp:Label Style="color: Black; font-weight: bolder; font-size: large ! important;" ID="lblProcessName_P"
                runat="server" Text="">
            </asp:Label>
        </div>
        <div class="row p-t-10">
            <div class="col-sm-2">
                <label>RFP No </label>
            </div>

            <div class="col-sm-4">
                <asp:Label ID="lblRFPNo_p" runat="server"></asp:Label>
            </div>
            <div class="col-sm-2">
                <label>Date </label>
            </div>
            <div class="col-sm-4">
                <asp:Label ID="lblDate_p" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row p-t-10">
            <div class="col-sm-2">
                <label>Customer  </label>
            </div>
            <div class="col-sm-4">
                <asp:Label ID="lblcustomer_p" runat="server"></asp:Label>
            </div>
            <div class="col-sm-2 p-t-10">
                <label>QTY </label>
            </div>
            <div class="col-sm-4 p-t-10">
                <asp:Label ID="lblqty_p" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row p-t-10">
            <div class="col-sm-2">
                <label>Item Name  </label>
            </div>
            <div class="col-sm-4">
                <asp:Label ID="lblitemname_p" runat="server"></asp:Label>
            </div>
            <div class="col-sm-2">
                <label>Size </label>
            </div>
            <div class="col-sm-4">
                <asp:Label ID="lblsize_p" runat="server"></asp:Label>
            </div>
        </div>
        <div class="row p-t-10">
            <div class="col-sm-2">
                <label>Drawing Name  </label>
            </div>
            <div class="col-sm-4">
                <asp:Label ID="lbldrawingname_p" runat="server"></asp:Label>
            </div>
            <div class="col-sm-2">
                <label>Stage Of Activity </label>
            </div>
            <div class="col-sm-4">
                <asp:Label ID="lblstatgeofactivity_p" runat="server"></asp:Label>
            </div>
        </div>

        <div class="col-sm-12 p-t-10">
            <table id="tblItem" class="table table-hover table-bordered">
                <thead>
                    <tr>
                        <th>Process
                        </th>
                        <th>Status
                        </th>
                        <th>QC Done By
                        </th>
                        <th>QC Done On
                        </th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>RECEIPT OF RAW MATERIAL INSPECTION
                        </td>
                        <td>
                            <asp:Label ID="lblRawMaterialInspectionStatus_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblRawMaterialInspectionQCDoneBy_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblRawMaterialInspectionQCDoneOn_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>RECEIPT OF BOUGHT OUT ITEM INSPECTION
                        </td>
                        <td>
                            <asp:Label ID="lblBoughtOutItemInspectionstatus_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblBoughtOutItemInspectionQCDoneBy_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblBoughtOutItemInspectionQCDoneOn_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>SHEET MARKING AND CUTTING PROCESS
                        </td>
                        <td>
                            <asp:Label ID="lblSheetMarkingAndCuttingProcessstatus_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblsheetmarkingandcuttingQCDoneBy_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblsheetmarkingandcuttingQCDoneOn_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>SHEET WELDING PROCESS
                        </td>
                        <td>
                            <asp:Label ID="lblsheetweldingProcessStatus_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblsheetweldingQCDoneBy_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblSheetWeldingQCDoneOn" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>BELLLOW FORMING PROCESS
                        </td>
                        <td>
                            <asp:Label ID="lblBellowFormingProcessStatus_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblBellowFormingQCDoneBy_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblBellowFormingQCDoneOn_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>MARKING AND CUTTING PROCESS
                        </td>
                        <td>
                            <asp:Label ID="lblMarkingAndCuttingProcessStatus_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblMarkingAndCuttingProcessQCDoneBy_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblMarkingAndCuttingProcessQCDoneOn_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>FABRICATION AND WELDING PROCESS
                        </td>
                        <td>
                            <asp:Label ID="lblfabricationandweldingprocessstatus_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblfabricationandweldingprocessQCDoneBy_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblfabricationandweldingprocessQCDoneOn_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>BELLOW  TO PIPE WELDING PROCESS
                        </td>
                        <td>
                            <asp:Label ID="lblBellowToPipeWeldingProcessstatus_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblBellowToPipeWeldingProcessQCDoneBy_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblBellowToPipeWeldingProcessQCDoneOn_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>ALL OTHER ATTACHEMENT FITMENT WELDING PROCESS
                        </td>
                        <td>
                            <asp:Label ID="lblotherpartweldingprocessstatus_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblotherpartweldingprocessQCDoneBy_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblotherpartweldingprocessQCDoneOn_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>LEAK TESTING AND INTERNAL INSPECTION
                        </td>
                        <td>
                            <asp:Label ID="lblleaktestingandinternalinspectionprocessStatus_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblleaktestinginternalinspectionprocessQCDoneBy_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblleaktestinginternalinspectionprocessQCDoneOn_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>CUSTOMER INSPECTION PROCESS
                        </td>
                        <td>
                            <asp:Label ID="lblcustomerinspectionprocessstatus_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblcustomerinspectionprocessQCDoneBy_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblcustomerinspectionprocessQCDoneOn_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>SURFACE PREPARATION AND PAINTING ACTIVITIES
                        </td>
                        <td>
                            <asp:Label ID="lblsurfacepreparationandpaintingactivitiesStatus_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblsurfacepreparationandpaintingactivitiesQCDoneBy_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblsurfacepreparationandpaintingactivitiesQCDoneOn_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>PACKING AND MARKING INSPECTION
                        </td>
                        <td>
                            <asp:Label ID="lblPackingAndMarkingInspectionstatus_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblPackingAndMarkingInspectionQCDoneBy_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblPackingAndMarkingInspectionQCDoneOn_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>

</asp:Content>

