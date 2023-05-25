<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="OTHERDCPrint.aspx.cs"
    Inherits="Pages_OTHERDCPrint" ClientIDMode="Predictable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
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
    </style>

    <script type="text/javascript">

        function InternalDCPrint() {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var FromUnitAddress = $('#ContentPlaceHolder1_hdnFromUnitAddress').val();
            var CompanyFormalName = $('#ContentPlaceHolder1_hdnFormalCompanyName').val();
            var Companyname = $('#ContentPlaceHolder1_hdnCompanyname').val();
            var footercontent = $('#ContentPlaceHolder1_divFooterContent').html();

            var lonestarlogourl = $('#ContentPlaceHolder1_hdnlonestarlogo').val();

            var InternalDCContent = $('#ContentPlaceHolder1_divInternalDC_p').html();

            winprint.document.write("<html><head><title>");
            winprint.document.write("Offer");
            winprint.document.write("</title>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/printg.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<style type='text/css'> </style>");

            winprint.document.write("<div class='print-page jobcardprintoutermargin'></div>");
            winprint.document.write("<table class='jobcardprinttablemargin'><thead><tr><td>");
            winprint.document.write("<div class='col-sm-12 jobcardprinttheadheight' style='text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;'>");
            winprint.document.write("<div>");
            winprint.document.write("<div>");
            winprint.document.write("<div class='col-sm-12 padding0' id='divLSERPLogo' style='margin:20px auto;display:block;'>");
            winprint.document.write("<div class='row jobcardprinttheadwidth'>");
            winprint.document.write("<div class='col-sm-3'>");
            winprint.document.write("<img src=" + lonestarlogourl + " alt='lonestar-image' width='90px' style='margin:5px 0;'>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-6 text-center'>");
            winprint.document.write("<h3 style='font-weight:600;font-size:20px;color:#000;font-family: Times New Roman;display: contents;'>" + Companyname + "</h3>");
            winprint.document.write("<span style='font-weight:600;font-size:11px ! important;font-family: Times New Roman;'>" + CompanyFormalName + "</span>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;width: 100%;padding:0 0px'>" + FromUnitAddress + "</p>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-3'>");
            //  winprint.document.write("<img src='../Assets/images/iso.jpeg' alt='lonestar-image' height='90px' style='margin:5px 0;'>");
            winprint.document.write("</div></div>");
            winprint.document.write("</div></div></div>");
            winprint.document.write("</td></tr></thead>");
            winprint.document.write("<tbody><tr><td>");

            winprint.document.write("<div style='margin:0 5mm;'>");
            winprint.document.write(InternalDCContent);
            winprint.document.write("</div>");

            winprint.document.write("</td></tr></tbody>");
            winprint.document.write("<tfoot><tr><td>");
            winprint.document.write("<div class='jobcardprinttfootheight'>");
            winprint.document.write("<div class='row jobcardprinttfootwidth'>");
            winprint.document.write("<div class='col-sm-12'>");
            winprint.document.write(footercontent);
            winprint.document.write("</div>");
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
                                    <h3 class="page-title-head d-inline-block">Internal DC Print</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Stores</a></li>
                                <li class="active breadcrumb-item" aria-current="page">OTHER DC Print</li>
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
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" runat="server">
                        </div>
                        <div id="divAddNew" runat="server">
                            <div class="ip-div text-center p-t-10">
                                <asp:LinkButton ID="btnprint" runat="server" CssClass="btn btn-cons btn-success" Text="INTERNAL DC PRINT" OnClick="btnprint_Click">
                                </asp:LinkButton>
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">

                                <asp:HiddenField ID="hdnFromUnitAddress" runat="server" Value="0" />
                                <asp:HiddenField ID="hdnFormalCompanyName" runat="server" Value="0" />
                                <asp:HiddenField ID="hdnCompanyname" runat="server" Value="0" />
                                <asp:HiddenField ID="hdnFooterCompanyName" runat="server" Value="0" />

                                <div id="divInternalDC_p" runat="server">

                                    <div class="row" style="border: 1px solid #000; padding: 5px;">
                                        <div class="col-sm-4" style="border: 1px solid #000; padding: 10px;">
                                            <label style="font-weight: 700">
                                                DC No</label>
                                            <asp:Label ID="lblDCNo_p" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-8 p-t-10" style="border: 1px solid #000; padding: 10px;">
                                            <label style="font-weight: 700">
                                                DC Date</label>
                                            <asp:Label ID="lblDCDate_p" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row" style="border: 1px solid #000; padding: 5px">
                                        <div class="col-sm-4 p-t-10" style="border: 1px solid #000; padding: 10px;">
                                            <label style="font-weight: 700">
                                                To Unit Address</label>
                                        </div>
                                        <div class="col-sm-8" style="border: 1px solid #000; padding: 10px;">
                                            <asp:Label ID="lbltocompanyname_p" runat="server" Style="font-weight: bolder;"></asp:Label>
                                            <asp:Label ID="lblToUnitAddress_p" runat="server" Style="font-weight: 700"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row" style="border: 1px solid #000; padding: 5px">
                                        <div class="col-sm-4" style="border: 1px solid #000; padding: 10px;">
                                            <label style="font-weight: 700">
                                                Expected Duration</label>
                                        </div>
                                        <div class="col-sm-8" style="border: 1px solid #000; padding: 10px;">
                                            <asp:Label ID="lblExpectedDuration_p" runat="server" Style="font-weight: 700"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row" style="border: 1px solid #000; padding: 5px">
                                        <div class="col-sm-4" style="border: 1px solid #000; padding: 10px;">
                                            <label style="font-weight: 700">
                                                Reason / Remarks</label>
                                        </div>
                                        <div class="col-sm-8" style="border: 1px solid #000; padding: 10px;">
                                            <asp:Label ID="lblReasonRemarks_p" runat="server" Style="font-weight: 700"></asp:Label>
                                        </div>
                                    </div>
                                    <div>
                                        <div class="col-sm-12 p-t-10" style="overflow-x: auto; padding-left: 5px !important; padding-right: 5px !important;">
                                            <asp:GridView ID="gvItemDescriptionDetails_p" runat="server" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium" Style="border: 1px solid #000 !important;">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemDescription" runat="server" Text='<%# Eval("ItemDescription")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("Qty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                                <div id="divFooterContent" runat="server">
                                    <div class="col-sm-12 text-center">
                                        <label>
                                            Not For Sale</label>
                                    </div>
                                    <div class="row p-t-5">
                                        <div class="col-sm-6 text-left" style="font-weight: 700; color: #000">
                                            <label style="font-size: 12px">Received By : </label>
                                            <asp:Label ID="lblReceivedBy_p" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-6 text-right" style="font-weight: 700; color: #000">
                                            <label>
                                                <asp:Label ID="lblfootercompanyname_p" runat="server"> </asp:Label>
                                            </label>
                                            <asp:Label ID="lblSignature_p" runat="server"> </asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hdnOTHID" runat="server" />
                    <asp:HiddenField ID="hdnlonestarlogo" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>

