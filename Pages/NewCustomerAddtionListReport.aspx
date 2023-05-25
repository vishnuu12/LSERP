<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="NewCustomerAddtionListReport.aspx.cs" Inherits="Pages_NewCustomerAddtionListReport" ClientIDMode="Predictable" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {
            $('#<%=txtFromDate.ClientID %>').datepicker({
                format: 'dd/mm/yyyy'
            });
            $('#<%=txtToDate.ClientID %>').datepicker({
                format: 'dd/mm/yyyy'
            });
        });

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {
                $('.datepicker').datepicker({ format: 'dd/mm/yyyy' });
            }
        });

        function Validate() {
            if (Mandatorycheck('ContentPlaceHolder1_divOutput')) {
                var FromDate = $('#<%=txtFromDate.ClientID %>').val();
                var ToDate = $('#<%=txtToDate.ClientID %>').val();
                var FDparts = FromDate.split("/");
                var TDParts = ToDate.split("/");
                var FD = new Date(FDparts[1] + "/" + FDparts[0] + "/" + FDparts[2]);
                var TD = new Date(TDParts[1] + "/" + TDParts[0] + "/" + TDParts[2]);
                if (FD > TD) {
                    $('#<%=txtToDate.ClientID %>').notify('The To Date Should Be greater Than From Date.', { arrowShow: true, position: 'r,r', autoHide: true });
                    hideLoader();
                    return false;
                }
                else {
                    return true;
                }
            }
            else {
                hideLoader();
                return false;
            }
        }

        function submit() {
            var msg;
            msg = Validate();

            if (msg) {
                $('#ContentPlaceHolder1_btnSubmit').click();
                //return true;
            }
            else {
                //return false;
            }
        }

        function PrintNewCustomerAddtionList(QRCode) {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var Address = $('#ContentPlaceHolder1_hdnAddress').val();
            var PhoneAndFaxNo = $('#ContentPlaceHolder1_hdnPhoneAndFaxNo').val();
            var Email = $('#ContentPlaceHolder1_hdnEmail').val();
            var WebSite = $('#ContentPlaceHolder1_hdnWebsite').val();

            var DocNo = $('#ContentPlaceHolder1_hdnDocNo').val();
            var RevNo = $('#ContentPlaceHolder1_hdnRevNo').val();
            var RevDate = $('#ContentPlaceHolder1_hdnRevDate').val();

            var divCustomerDetails = $('#ContentPlaceHolder1_divCustomerAddtionList_p').html();

            winprint.document.write("<html><head><title>");
            winprint.document.write("Offer");
            winprint.document.write("</title>");
            winprint.document.write("<link rel='stylesheet'  href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/printg.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<style type='text/css'> .Qrcode {height: 60px;width: 80px;margin-right: 10px;padding-left: 20px;} </style>");

            winprint.document.write("<div class='print-page' style='position:fixed;border:1px solid #000;width:200mm;height:287mm;margin-top:5mm;margin-bottom:5mm;margin-left:5mm;margin-right:5mm'></div>");
            winprint.document.write("<table style='width:200mm;height:287mm;margin-left:20px;border:0px !important'><thead><tr><td>");
            winprint.document.write("<div class='col-sm-12' style='text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;height:112px'>");
            winprint.document.write("<div>");
            winprint.document.write("<div>");
            winprint.document.write("<div class='col-sm-12 padding0' id='divLSERPLogo' style='margin:20px auto;display:block;'>");
            winprint.document.write("<div class='row' style='position:fixed;width:200mm;left:5mm;border-bottom:1px solid #000;'>");
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
            winprint.document.write(divCustomerDetails);
            winprint.document.write("</div>");

            winprint.document.write("</td></tr></tbody>");
            winprint.document.write("<tfoot><tr><td>");
            winprint.document.write("<div style='height:30mm;'>");
            winprint.document.write("<div class='row' style='margin-bottom:20px;width:200mm;bottom:0px;position:fixed;'>");

            winprint.document.write("<div class='col-sm-2 text-center'>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-7 text-center'>");
            winprint.document.write("<label style='width:30%;color:black;float:left; font-weight:bolder;border: 1px solid black;'>Doc No : " + DocNo + "</label>");
            winprint.document.write("<label style='width:25%;color:black;float:left; font-weight:bolder;border: 1px solid black;'>Rev No : " + RevNo + "</label>");
            winprint.document.write("<label style='width:40%;color:black;float:left; font-weight:bolder;border: 1px solid black;'>Rev Date : " + RevDate + "</label>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-3 text-center'>");
            winprint.document.write("<img src='" + QRCode + "' class='Qrcode'/>");
            winprint.document.write("</div>");

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
                                    <h3 class="page-title-head d-inline-block">New Customer Addtion List Report  </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Reports</a></li>
                                <li class="active breadcrumb-item" aria-current="page">Customer Addtion List Report</li>
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
                    <asp:PostBackTrigger ControlID="btnprint" />
                    <asp:PostBackTrigger ControlID="btnexcel" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divInput" runat="server">
                            <div class="ip-div text-left">
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="col-sm-12">
                                            <div class="col-sm-4">
                                                <label class="form-label mandatorylbl">
                                                    From Date</label>
                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="mandatoryfield form-control datepicker"
                                                    PlaceHolder="Enter From Date" ToolTip="Enter From Date" AutoComplete="off"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <label class="form-label mandatorylbl">
                                                    To Date</label>
                                                <asp:TextBox ID="txtToDate" runat="server" placeholder="Enter To Date" ToolTip="Enter Deadline Date"
                                                    AutoComplete="off" CssClass="mandatoryfield form-control datepicker"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <asp:LinkButton ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return Validate();"
                                                    OnClick="btnSubmit_Click"
                                                    CssClass="btn btn-cons btn-success" />
                                            </div>
                                            <div>
                                            </div>
                                        </div>

                                        <div id="divNewCustomer" visible="false" runat="server">

                                            <div class="col-sm-12 p-t-10 text-center">
                                                <asp:LinkButton ID="btnprint" runat="server" Text="Print"
                                                    OnClick="btnprint_Click"
                                                    CssClass="btn btn-cons btn-success" />
                                                <asp:LinkButton ID="btnexcel" runat="server" Style="float: right;"
                                                    OnClick="btnExcelDownload_Click"
                                                    CssClass="excel_bg" />
                                            </div>

                                            <div class="col-sm-12 p-t-10 text-center">
                                                <label style="color: brown; font-size: large;">
                                                    Total Number Of Customer Added During This Period
                                                </label>
                                                <asp:Label ID="lblNumberOfCustomer" Style="color: #000; font-size: large; font-weight: bold;" runat="server"></asp:Label>
                                            </div>

                                            <div class="col-sm-12 p-t-10">
                                                <asp:GridView ID="gvcustomerEnquiryDetails" runat="server"
                                                    CssClass="table table-bordered orderingfalse table-hover no-more-tables"
                                                    ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found"
                                                    AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--    <asp:TemplateField HeaderText="Enquiry No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEnquiryNo" runat="server" Text='<%# Eval("EnquiryID")%>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Customer Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOfferSubmitted" runat="server" Text='<%# Eval("CustomerName")%>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%-- <asp:TemplateField HeaderText="PO Date">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOrderLostEnquiry" runat="server" Text='<%# Eval("EnquiryDate")%>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>

                                                        <asp:TemplateField HeaderText="Address">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("Address")%>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Country">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("CountryName")%>'>
                                                                </asp:Label>
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
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div id="divCustomerAddtionList_p" runat="server" style="display: none;">
                        <div class="col-sm-12 p-t-10 text-center">
                            <asp:Label ID="lbldate_p" Style="color: brown; font-size: large;" runat="server">  </asp:Label>
                            <asp:Label ID="lblTotalNoOfCustomer_p" Style="color: #000; font-size: large; font-weight: bold;" runat="server"></asp:Label>
                        </div>
                        <div class="col-sm-12 p-t-10">
                            <asp:GridView ID="gvCustomerEnquiryDetails_p" runat="server"
                                CssClass="table table-bordered orderingfalse table-hover no-more-tables"
                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Enquiry No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEnquiryNo" runat="server" Text='<%# Eval("EnquiryID")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOfferSubmitted" runat="server" Text='<%# Eval("CustomerName")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Enquiry Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOrderLostEnquiry" runat="server" Text='<%# Eval("EnquiryDate")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
