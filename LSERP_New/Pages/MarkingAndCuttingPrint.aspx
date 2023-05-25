<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="MarkingAndCuttingPrint.aspx.cs" Inherits="Pages_MarkingAndCuttingPrint"
    ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function PrintMarkinAndCutting(QRCode, Mode) {

            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var Address = $('#ContentPlaceHolder1_hdnAddress').val();
			var CheyyarAddress = $('#ContentPlaceHolder1_hdnCheyyarAddress').val();
            var PhoneAndFaxNo = $('#ContentPlaceHolder1_hdnPhoneAndFaxNo').val();
            var Email = $('#ContentPlaceHolder1_hdnEmail').val();
            var WebSite = $('#ContentPlaceHolder1_hdnWebsite').val();

            var DocNo = $('#ContentPlaceHolder1_hdnDocNo').val();
            var RevNo = $('#ContentPlaceHolder1_hdnRevNo').val();
            var RevDate = $('#ContentPlaceHolder1_hdnRevDate').val();

            //var FabricationWelding = $('#ContentPlaceHolder1_divFabricationAndWeldingPDF').html();

            var MarkingAndCutting = $('#ContentPlaceHolder1_divMarkingAndCuttingPDF').html();

            // var SheetMarkingAndCutting = $('#ContentPlaceHolder1_divSheetMarkingAndCuttingPDF').html();

            //var SheetWelding = $('#ContentPlaceHolder1_divSheetWeldingPDF').html();

            //var BellowFormingTangentCutting = $('#ContentPlaceHolder1_divBellowFormingAndTangentCuttingPDF').html();

            winprint.document.write("<html><head><title>");
            winprint.document.write("Offer");
            winprint.document.write("</title>");
            winprint.document.write("<link rel='stylesheet'  href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/printg.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<style type='text/css'> label { font-weight: bold ! important;color: #000 ! important; } .contractorborder label{padding: 4px 6px; } .contractorborder { padding-top:5px; border: 1px solid;} .sideheading{ display:flex;align-items:flex-start;justify-content:center;margin-top:15px; } .lbl_h{ width:35%; } .lbl_v{ width:65%; } .Qrcode {height: 60px;width: 80px;margin-right: 10px;padding-left: 20px;} </style>");

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
            winprint.document.write("<h3 style='font-weight:600;font-size:22px;color:#000;font-family: Times New Roman;display: contents;'>LSI-MECH ENGINEERS PVT. LTD.</h3>");
            winprint.document.write("<span style='font-weight:600;font-size:24px ! important;font-family: Times New Roman;'></span>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;width: 100%;padding:0 0px'>Office : " + Address + "</p>");
            winprint.document.write("<p style='font-size:10px !important;font-weight:500;color:#000;width: 100%;padding:0 0px'>Works : " + CheyyarAddress + "</p>");
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
            //if (Mode == "SMC")
            winprint.document.write(MarkingAndCutting);
            //else if (Mode == "MC")
            //    winprint.document.write(MarkingAndCutting);
            //else if (Mode == "FW")
            //    winprint.document.write(FabricationWelding);
            //else if (Mode == "SW")
            //    winprint.document.write(SheetWelding);
            //else if (Mode == "BFTC")
            //    winprint.document.write(BellowFormingTangentCutting);

            winprint.document.write("</div>");

            winprint.document.write("</td></tr></tbody>");
            winprint.document.write("<tfoot><tr><td>");
            winprint.document.write("<div class='jobcardprinttfootheight'>");
            //winprint.document.write("<div class='row jobcardprinttfootwidth'>");
            ////winprint.document.write("<div class='col-sm-2 text-center'>");
            ////winprint.document.write("<label style='color:black; font-weight:bolder;'>Quality Incharge</label>");
            ////winprint.document.write("<label style='color:black; font-weight:bolder;display:block;'>Sign & Date</label>");
            ////winprint.document.write("</div>");
            //// winprint.document.write("<div class='col-sm-7 text-center'>");
            //winprint.document.write("<label style='width:30%;color:black;float:left; font-weight:bolder;border: 1px solid black;'>Doc No : " + DocNo + "</label>");
            //winprint.document.write("<label style='width:25%;color:black;float:left; font-weight:bolder;border: 1px solid black;'>Rev No : " + RevNo + "</label>");
            //winprint.document.write("<label style='width:40%;color:black;float:left; font-weight:bolder;border: 1px solid black;'>Rev Date : " + RevDate + "</label>");
            //// winprint.document.write("</div>");
            ////winprint.document.write("<div class='col-sm-3 text-center'>");
            ////winprint.document.write("<label style='color:black; font-weight:bolder;'> Production Incharge</label><label style='color:black; font-weight:bolder;display:block;'>Sign & Date</label>"); //<img src='" + QRCode + "' class='Qrcode'/>
            ////winprint.document.write("</div>");
            //winprint.document.write("</div>");

            winprint.document.write("<div class='row jobcardprinttfootwidth'>");
            winprint.document.write("<div class='col-sm-6 p-l-20 text-left'>");
            winprint.document.write("<label style='color:black; font-weight:bolder;'> Production Enginneer : " + $('#ContentPlaceHolder1_hdnProductionBy').val() + " </label><label style='color:black; font-weight:bolder;display:block;'> Date : " + $('#ContentPlaceHolder1_hdnProductionDoneOn').val() + " </label>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-6 p-r-20 text-right'>");
            winprint.document.write("<label style='color:black; font-weight:bolder;'> Quality Engineer : " + $('#ContentPlaceHolder1_hdnQCDoneBy').val() + " </label>");
            winprint.document.write("<label style='color:black; font-weight:bolder;display:block;'>Date : " + $('#ContentPlaceHolder1_hdnQCDoneOn').val() + " </label>");
            winprint.document.write("</div>");

            winprint.document.write("</div>");

            winprint.document.write("<div class='row col-sm-12 jobcardprinttfootwidth' style='margin-bottom:15mm;'>");
            winprint.document.write("<div class='col-sm-2'>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-8'>");
            winprint.document.write("<label style='width:30%;color:black;float:left; font-weight:bolder;border: 1px solid black;'>Doc No : " + DocNo + "</label>");
            winprint.document.write("<label style='width:25%;color:black;float:left; font-weight:bolder;border: 1px solid black;'>Rev No : " + RevNo + "</label>");
            winprint.document.write("<label style='width:40%;color:black;float:left; font-weight:bolder;border: 1px solid black;'>Rev Date : " + RevDate + "</label>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-2'>");
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
                                    <h3 class="page-title-head d-inline-block">Marking And Cutting Print</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Production</a></li>
                                <li class="active breadcrumb-item" aria-current="page">Marking And Cutting Print</li>
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
                        <div class="col-sm-12 p-b-10 text-center">
                            <asp:LinkButton ID="btnprint" runat="server" CssClass="btn btn-cons btn-success"
                                Text="GET JOB CARD PRINT" OnClick="btnprint_Click">
                            </asp:LinkButton>
                        </div>
                        <div id="divMarkingAndCuttingPDF" runat="server" style="display: block;">
                            <div id="div16" class="FrontPagepopupcontent" runat="server">
                                <div class="text-center" style="background-color: #b0acac; padding-top: 10px; padding-bottom: 10px;">
                                    <asp:Label Style="color: Black; font-weight: bolder; font-size: large ! important;" ID="lblProcessNameHeader_MC_P"
                                        runat="server" Text="">
                                    </asp:Label>
                                </div>
                                <div>
                                    <div class="row p-t-10">
                                        <div class="col-sm-4 text-left">
                                            <label style="color: black;">
                                                Job No</label>
                                            <asp:Label ID="lblJobOrderID_MC_P" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-4">
                                            <label style="color: black;">
                                                RFP No</label>
                                            <asp:Label ID="lblRFPNo_MC_P" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-4">
                                            <label style="color: black;">
                                                Date
                                            </label>
                                            <asp:Label ID="lblDate_MC_P" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div id="divcontractor" runat="server">
                                        <div class="row">
                                            <div class="col-sm-4 text-left">
                                                <label style="color: black;">
                                                    Contractor Name:</label>
                                                <asp:Label ID="lblContractorName_MC_P" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-4">
                                                <label style="color: black;">
                                                    Contractor Team Member Name:</label>
                                                <asp:Label ID="lblContractorTeamMemberName_MC_P" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-4">
                                            </div>
                                        </div>
                                    </div>
                                    <%--   <div class="row contractorborder">
                                        <div class="col-sm-5 text-left">
                                           
                                        </div>
                                        <div class="col-sm-1">
                                            <label style="float: right;">:</label>
                                        </div>
                                        <div class="col-sm-6">
                                           
                                        </div>
                                    </div>
                                    <div class="row contractorborder">
                                        <div class="col-sm-5 text-left">
                                          
                                        </div>
                                        <div class="col-sm-1">
                                            <label style="float: right;">:</label>
                                        </div>
                                        <div class="col-sm-6">
                                          
                                        </div>
                                    </div>
                                    <div id="divcontractor" runat="server">
                                        <div class="row contractorborder">
                                            <div class="col-sm-5 text-left">
                                                <label style="color: black;">
                                                    Contractor Name:</label>
                                            </div>
                                            <div class="col-sm-1">
                                                <label style="float: right;">:</label>
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:Label ID="lblContractorName_MC_P" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row contractorborder">
                                            <div class="col-sm-5 text-left">
                                                <label style="color: black;">
                                                    Contractor Team Member Name:</label>
                                            </div>
                                            <div class="col-sm-1">
                                                <label style="float: right;">:</label>
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:Label ID="lblContractorTeamMemberName_MC_P" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>--%>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6" style="overflow-wrap: break-word;">
                                        <div class="sideheading">
                                            <label style="color: black;" class="lbl_h">
                                                Item Name/Size</label>
                                            <asp:Label ID="lblItemNameSize_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                                        </div>
                                        <div class="sideheading">
                                            <label style="color: black;" class="lbl_h">
                                                Drawing Name</label>
                                            <asp:Label ID="lblDrawingname_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                                        </div>
                                        <div class="sideheading">
                                            <label style="color: black;" class="lbl_h">
                                                Part Name</label>
                                            <asp:Label ID="lblPartName_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                                        </div>
                                        <div class="sideheading">
                                            <label style="color: black;" class="lbl_h">
                                                Part Qty</label>
                                            <asp:Label ID="lblPartQty_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                                        </div>
                                        <div class="sideheading">
                                            <label style="color: black;" class="lbl_h">
                                                Process Name</label>
                                            <asp:Label ID="lblProcessName_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                                        </div>
                                        <div class="sideheading">
                                            <label style="color: black;" class="lbl_h">
                                                Material Category</label>
                                            <asp:Label ID="lblMaterialCategory_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                                        </div>
                                        <div class="sideheading">
                                            <label style="color: black;" class="lbl_h">
                                                Material Grade</label>
                                            <asp:Label ID="lblmaterialGrade_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                                        </div>
                                        <div class="sideheading">
                                            <label style="color: black;" class="lbl_h">
                                                Thickness</label>
                                            <asp:Label ID="lblThickness_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                                        </div>
                                        <div class="sideheading">
                                            <label style="color: black;" class="lbl_h">
                                                MRN Number</label>
                                            <asp:Label ID="lblMRNNumber_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                                        </div>
                                        <div class="sideheading">
                                            <label style="color: black;" class="lbl_h">
                                                Stage Of Activity
                                            </label>
                                            <asp:Label ID="lblJobOrderRemarks_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                                        </div>
                                        <div class="sideheading" id="divtotalcost" runat="server">
                                            <label style="color: black;" class="lbl_h">
                                                Total Cost</label>
                                            <asp:Label ID="lblTotalCost_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                                        </div>
                                        <div class="sideheading">
                                            <label style="color: black;" class="lbl_h">
                                                Target Date
                                            </label>
                                            <asp:Label ID="lblDeadlineDate_MC_P" CssClass="lbl_v" runat="server"></asp:Label>
                                        </div>
                                        <div class="sideheading">
                                            <label style="color: black;" class="lbl_h">
                                                QC Completion Date
                                            </label>
                                            <asp:Label ID="lblQCCompletionDate_MC_P" CssClass="lbl_v" runat="server"></asp:Label>
                                        </div>
                                        <div class="row p-t-10">
                                            <label style="color: Black;">
                                                Issue Details</label>
                                        </div>
                                        <div class="p-t-10" style="display: flex;">
                                            <asp:GridView ID="gvIssueDetails_MC_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MRN Number" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMRNNumber" runat="server" Text='<%# Eval("MRNNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Issued Weight" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIssuedWeight" runat="server" Text='<%# Eval("IssuedWeight")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Length" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLength" runat="server" Text='<%# Eval("Length")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Width" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblWidth" runat="server" Text='<%# Eval("Width")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Material Return" ItemStyle-HorizontalAlign="left"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMatRtn" runat="server" Text='<%# Eval("MaterialReturn")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                            <div id="divQCDimension" class="p-l-60" runat="server" visible="false">
                                                <asp:GridView ID="gvJobCardQCDimensionDetails" runat="server" AutoGenerateColumns="False"
                                                    ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found"
                                                    CssClass="table table-hover table-bordered medium pagingfalse" DataKeyNames="Name">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="As Per DRG">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAsPerDrawing" runat="server" Text='<%# Eval("AsPerDrawing")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Actual(mm)">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblActual" runat="server" Text='<%# Eval("Actual")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="row p-t-10">
                                            <label>Layout Marking Sketch </label>
                                            <div style="min-height: 100px; width: 100%; border: 1px solid;">
                                            </div>
                                        </div>
                                        <div class="row p-t-10">
                                            <label style="color: Black;">
                                                Part Seriel Number</label>
                                        </div>
                                        <div class="row">
                                            <asp:GridView ID="gvPartSerialNo_MC_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item SNO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemSNO" runat="server" Text='<%# Eval("ItemSNO")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Part S.NO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartSNO" runat="server" Text='<%# Eval("PartSNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                        <div style="padding-left: 5px;">
                                            <div class="row p-t-10">
                                                <label style="color: Black;">
                                                    Offer QC test</label>
                                            </div>
                                            <div class="row text-left">
                                                <asp:Label ID="lblOfferQC_MC_P" runat="server"></asp:Label>
                                            </div>
                                            <div class="row p-t-10">
                                                <asp:Label ID="lblFabricationType_MC_P" Style="font-weight: bold;" runat="server"></asp:Label>
                                            </div>
                                            <div id="divfabrication_MC_P" runat="server">
                                            </div>
                                        </div>
                                        <%--<div style="padding-left: 2px;">
                                            <label>
                                                Remarks
                                            </label>
                                            <asp:Label ID="lblOverallremarks_MC_P" runat="server"></asp:Label>
                                        </div>--%>
                                    </div>

                                </div>

                                <div class="col-sm-12 p-t-10">
                                    <asp:GridView ID="gvQCObservationDetails_MC_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                        EmptyDataText="No Records Found"
                                        CssClass="table table-hover table-bordered medium">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center"
                                                ItemStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Stage / Activity">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstageactivity" runat="server" Text='<%# Eval("Stage")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Verification/Availability">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtverificationavailability" Text='<%# Eval("VerficationAvailability")%>' CssClass="mandatoryfield" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reference/Observation">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtreferenceobservation" Text='<%# Eval("ReferenceObservation")%>' CssClass="mandatoryfield"
                                                        runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Verified / By" HeaderStyle-Width="20%" ItemStyle-Width="20%"
                                                HeaderStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtverifiedBy" Text='<%# Eval("VerifiedBy")%>' CssClass="mandatoryfield"
                                                        runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="col-sm-12 p-t-5">
                                    <label>
                                        Remarks
                                    </label>
                                    <asp:Label ID="lblOverallremarks_MC_P" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-12 p-t-5">
                                    <label>Status </label>
                                    <asp:Label ID="lbljobcardStatus_MC_P" runat="server"></asp:Label>
                                </div>

                                <div class="col-sm-12 p-t-10" visible="false" runat="server">
                                    <asp:GridView ID="gvTypeOfCheck_MC_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                        EmptyDataText="No Records Found"
                                        CssClass="table table-hover table-bordered medium">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="text-center"
                                                ItemStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type Of Check">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTypeOfCheck" runat="server" Text='<%# Eval("TypeOfCheck")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTypeOfCheck" runat="server" Text='<%# Eval("QCReMarks")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>



                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hdnAddress" runat="server" Value="" />
                    <asp:HiddenField ID="hdnCheyyarAddress" runat="server" Value="" />
                    <asp:HiddenField ID="hdnPhoneAndFaxNo" runat="server" Value="" />
                    <asp:HiddenField ID="hdnEmail" runat="server" Value="" />
                    <asp:HiddenField ID="hdnWebsite" runat="server" Value="" />

                    <asp:HiddenField ID="hdnDocNo" runat="server" Value="" />
                    <asp:HiddenField ID="hdnRevNo" runat="server" Value="" />
                    <asp:HiddenField ID="hdnRevDate" runat="server" Value="" />

                    <asp:HiddenField ID="hdnProductionBy" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnProductionDoneOn" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnQCDoneOn" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnQCDoneBy" runat="server" Value="0" />

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>


