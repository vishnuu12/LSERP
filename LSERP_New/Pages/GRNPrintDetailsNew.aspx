<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" 
    CodeFile="GRNPrintDetailsNew.aspx.cs" Inherits="Pages_GRNPrintDetailsNew" ClientIDMode="Predictable" %>


<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function GRNPrint(epstyleurl, Main, style, print, topstrip) {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var MrnContent = $('#ContentPlaceHolder1_divGRNPrint').html();

            winprint.document.write("<html><head><title>");
            winprint.document.write("Offer");
            winprint.document.write("</title>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='" + epstyleurl + "'/>");
            winprint.document.write("<link rel='stylesheet' href='" + Main + "' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet'  href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/printg.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");

            winprint.document.write("<style type='text/css'/>  .Qrcode{ float: right; } label{ color: black ! important;font-weight: bold;} p{ margin:0;padding:0; }  @page { size: landscape; } span { padding: 0px 0px; } .ISO tr td { border:1px solid black; }  </style>");

            winprint.document.write("<style type='text/css'/> @media print {.LandScapeprint{ width:277mm; margin-left:10mm;margin-right:10mm; height:190mm;margin-top:10mm;margin-bottom:10mm;} } html, body {height: 99%;page-break-after: avoid;page-break-before: avoid;} </style>");
            //@media print {  }
            //margin-left:5mm;margin-right:10mm;
            //margin-left:5mm;margin-right:5mm;
            // winprint.document.write("<style type='text/css'/> @media print { margin-left:5mm;margin-right:5mm; }  </style>");
            winprint.document.write("</head><body style='height:190mm;'>");
            winprint.document.write("<div style='width:297mm;height:190mm'>");
            winprint.document.write("<div class='LandScapeprint' style='border:2px solid black;'>");
            winprint.document.write("<div class='col-sm-12 row' style='margin:0 auto;padding-left:10px;padding-right:10px;'>");
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
                                    <h3 class="page-title-head d-inline-block">GRN PRINT CARD </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Print</a></li>
                                <li class="active breadcrumb-item" aria-current="page">GRN</li>
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
                    <asp:PostBackTrigger ControlID="btnGRNPrint" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">

                        <div class="text-center">
                            <asp:LinkButton ID="btnGRNPrint" runat="server" Text="Print" CssClass="btn btn-cons btn-success"
                                OnClick="btnGRNPrint_Click"></asp:LinkButton>
                        </div>

                        <div id="divGRNPrint" runat="server">
                            <div class="col-sm-12 row">
                                            <div class='row header-space' style='text-align: center; font-size: 20px; font-weight: bold; padding-left: 0; padding-right: 0; height: 120px'>
                                                <div class='header' style='border: 0px solid ! important;background: none ! important;'>
                                                    <div>
                                                        <div class='row padding0' id='divLSERPLogo' style='margin-left: 30mm;margin: 0px auto; display: block; padding: 5px 0px;'>
                                                           
                                                             <div class='row'>

                                                                <div class='col-sm-3'>

                                                                    <img src='../Assets/images/lonestar.jpeg' style="height: 115px;width: auto;margin-right: 25px;" alt='lonestar-image' width='90px'>
                                                                </div>

                                                                <div class='col-sm-6' style=" font-weight: bold;margin-top: 1mm;margin-left: -10mm;text-align: initial;">
                                                                    <p style='font-weight:600;font-size:30px;color:#000;font-family: Times New Roman;display: contents;'>
                                                                        <asp:Label ID="lblCompanyName_h" runat="server" style="font-size: 22px !important;"></asp:Label>
                                                                    </p>

                                                                    <p style='font-weight: 500; color: #000; width: 103%;'>
                                                                        <asp:Label ID="lblAddress_h" runat="server"></asp:Label>
                                                                    </p>

                                                                    <p style='font-weight: 500; color: #000'>
                                                                        <asp:Label ID="lblPhoneAndFaxNo_h" runat="server"></asp:Label>
                                                                    </p>

                                                                    <p style='font-weight: 500; color: #000'>
                                                                        <asp:Label ID="lblEmail_h" runat="server"></asp:Label>
                                                                    </p>

                                                                    <p style='font-weight: 500; color: #000'>
                                                                        <asp:Label ID="lblwebsite_h" runat="server"></asp:Label>
                                                                    </p>
                                                                </div>
                                                                <div class='col-sm-3'>

                                                                    <img src='../Assets/images/iso.jpeg' style="margin-left: 100mm;" alt='lonestar-image' height='90px'>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                <hr style="width:275mm;text-align:left;margin-left:-3mm;border-width:1px;color:black;background-color:black  ! important;">
                                            </div>
                             <div class="col-sm-12 p-t-10" style="text-align: center;text-decoration: underline;font-size: large; color: black; font-weight: bold;">
                                  GOODS RECEIPT NOTE                             
                            </div>
                              <div class="col-sm-12 row text-center">
                            </div>
                            <div class="col-sm-12 row p-t-10">
                                <div class="col-sm-4">
                                    <label>SUB VENDOR NAME :</label>
                                    <asp:Label ID="lblSupplierName_p" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-4">
                                    <label>VISUAL :</label>
                                    <asp:Label ID="lblVisual_p" runat="server"></asp:Label>
                                </div>
                                 <div class="col-sm-4">
                                    <label>GRN NO :</label>
                                    <asp:Label ID="lblGrnNo_p" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12 row p-t-10">
                                <div class="col-sm-4">
                                    <label>SUPPLIERDC/INVOICENO./DATE : </label>
                                    <asp:Label ID="lblInVoiceNo_p" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-4">
                                    <label>CHECK TEST :</label>
                                    <asp:Label ID="lblCheckTest_p" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-4">
                                    <label>GRN DATE : </label>
                                    <asp:Label ID="lblGrnDate_p" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12 row p-t-10">
                                <div class="col-sm-4">
                                    <label>WO NO AND DATE :</label>
                                    <asp:Label ID="lblPONoDate_p" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-4">
                                    <label>ADDT TEST REQUIRTMENT :</label>
                                    <asp:Label ID="lblAddtionalTestRequirtment_p" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12 row p-t-10">
                                <div class="col-sm-4">
                                    <label>MTR :</label>
                                    <asp:Label ID="lblMTR_p" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-4">
                                    <label>MEASURED DIMENSION :</label>
                                    <asp:Label ID="lblMeasuredDimension_p" runat="server"></asp:Label>
                                </div>
                            </div>

                            <div class="col-sm-12 row p-t-10">
                                <div class="col-sm-4">
                                    <label>PMI :</label>
                                    <asp:Label ID="lblPMI_p" runat="server"></asp:Label>
                                </div>
                                <div class="col-sm-4">
                                    <label>QUALITY INSPECTION : </label>
                                    <asp:Label ID="lblQualityInspection_p" runat="server"></asp:Label>
                                </div>
                            </div>  
                            <div class="col-sm-12 row p-t-10">
                                  </div>  
                             <div class="col-sm-12 row p-t-10">
                                  </div>  
                             <div class="col-sm-12 row p-t-10">
                                  </div>  
                             <div class="col-sm-12 row p-t-10">
                                  </div>  
                            <!-- <div class="col-sm-12 row p-t-10"> -->
                                <asp:GridView ID="gvQCClearedDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                    EmptyDataText="No Records Found" class="table table-hover table-bordered medium uniquedatatable" style="width: 100%;">
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
                            <!-- </div> -->
                            <div class="col-sm-12 row p-t-10">
                                  </div>  
                             <div class="col-sm-12 row p-t-10">
                                  </div>  
                            <div class="col-sm-12 row p-t-10">
                                  </div>  
                             <div class="col-sm-12 row p-t-10">
                                  </div>  
                            <div class="col-sm-12 row p-t-10">
                                  </div>  
                             <div class="col-sm-12 row p-t-10">
                                  </div>  
                            <div class="col-sm-12 row p-t-10">
                                  </div>  
                             <div class="col-sm-12 row p-t-10">
                                  </div>  

                            <div class="col-sm-12 row">
                                <div class="col-sm-8">
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
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>
