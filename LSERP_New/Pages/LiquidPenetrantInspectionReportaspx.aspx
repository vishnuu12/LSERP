<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="LiquidPenetrantInspectionReportaspx.aspx.cs" Inherits="Pages_LiquidPenetrantInspectionReportaspx" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">    

        function Validate() {
            var res = true;
            var msg = Mandatorycheck('ContentPlaceHolder1_divInput');

            if (msg == false) {
                res = false;
            }

            if (res)
                return true;
            else {
                hideLoader();
                return false;
            }
        }

        function deleteConfirm(ID) {
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
                __doPostBack('deleteVERID', ID);
            });
            return false;
        }

        function PrintLiquidPenetrantInspectionReport() {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var RecordLength = $('#ContentPlaceHolder1_gvlpireport_p').find('tr').length;

            var Address = $('#ContentPlaceHolder1_hdnAddress').val();
            var PhoneAndFaxNo = $('#ContentPlaceHolder1_hdnPhoneAndFaxNo').val();
            var Email = $('#ContentPlaceHolder1_hdnEmail').val();
            var WebSite = $('#ContentPlaceHolder1_hdnWebsite').val();

            var MainContent = $('#ContentPlaceHolder1_divmainContent_p').html();

            var BelowContent = $('#ContentPlaceHolder1_divbelowcontent_p').html();

            winprint.document.write("<html><head><title>");
            winprint.document.write("Offer");
            winprint.document.write("</title>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='../Assets/css/ep-style.css'/>");
            winprint.document.write("<link rel='stylesheet' href='../Assets/css/main.css' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet'  href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/printg.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='../Assets/images/topstrrip.jpg' type='text/css'/>");

            winprint.document.write("<style type='text/css'/>  label { font-weight:bold;padding-left:5px;  } .divtable_p table tr{ height:50px; } .divtable_p table tr td{ border:1px solid; } </style>");

            winprint.document.write("<style type='text/css'/>  .vam{ vertical-align: middle; } .divtable_p table td span{ padding-left:5px ! important; } </style>");

            winprint.document.write("<div class='print-page' style='position:fixed;border:1px solid #000;width:200mm;height:287mm;margin-top:5mm;margin-bottom:5mm;margin-left:5mm;margin-right:5mm'></div>");
            winprint.document.write("<table style='width:200mm;height:287mm;margin-left:20px;border:0px !important'><thead><tr><td>");
            winprint.document.write("<div class='col-sm-12 print-generateoffer' style='border-bottom:1px solid #000;text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;height:102px'>");
            winprint.document.write("<div>");
            winprint.document.write("<div>");
            winprint.document.write("<div class='col-sm-12 padding0' id='divLSERPLogo' style='margin:20px auto;display:block;padding:5px 0px;'>");
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

            winprint.document.write("<div style='margin: 5mm;'>");

            winprint.document.write(MainContent);

            winprint.document.write("<div class='p-t-10'>");
            winprint.document.write("<table class='table table-hover medium' cellspacing='0' rules='all' border='1' id='ContentPlaceHolder1_gvlpireport_p' style='border-collapse:collapse;'>");
            winprint.document.write("<tbody>");

            for (var j = 0; j < RecordLength; j++) {
                winprint.document.write("<tr align='center'>" + $('#ContentPlaceHolder1_gvlpireport_p').find('tr')[j].innerHTML + "</tr>");
            }

            //  k = k + 15;
            winprint.document.write("</tbody>");
            winprint.document.write("</table>");
            winprint.document.write("</div>");

            winprint.document.write("<div class='p-t-10'>");
            winprint.document.write(BelowContent);
            winprint.document.write("</div>");

            winprint.document.write("</div>");

            winprint.document.write("</td></tr></tbody>");
            winprint.document.write("<tfoot><tr><td>");
            winprint.document.write("<div class='col-sm-12' style='height:10mm;'>");
            winprint.document.write("<div>");
            winprint.document.write("<div  style='margin-bottom:20px;position:fixed;width:200mm;bottom:0px'>");

            //  winprint.document.write(FooterContent);

            winprint.document.write("</div>");
            winprint.document.write("</div></div>");
            winprint.document.write("</td></tr></tfoot></table>");
            winprint.document.write("</body></html>");

            setTimeout(function () {
                winprint.document.close();
                winprint.focus();
                winprint.print();
                // winprint.close();
            }, 1000);
        }

        function Print(RIRHID) {
            __doPostBack('Print', RIRHID);
            return false;
        }

        function clearFields() {
            $('#ContentPlaceHolder1_gvPartDetails').find('[type="text"]').val('')
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
                                    <h3 class="page-title-head d-inline-block">Liquid Penetrant Inspection Report</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Quality</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Liquid Penetrant Inspection Report</li>
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
                    <%-- <asp:PostBackTrigger ControlID="gvLPIDetails" />--%>
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="div" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="" id="divAdd" runat="server">
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
                                                    CssClass="form-control" Width="70%" ToolTip="Select Item No">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-2">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="p-t-10 p-l-0 p-r-0">
                                        <div id="divInput"
                                            runat="server">
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left mandatorylbl">
                                                        <label>Test Date </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtTestDate" CssClass="form-control datepicker mandatoryfield" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">Stage Of Test  </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtStageOfTest" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left">
                                                        <label class="mandatorylbl">Material Specification </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtMaterialSpecification" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left mandatorylbl">
                                                        <label>Bellow Sno </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtBellowSNo" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left mandatorylbl">
                                                        <label>Penetrant Brand </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox CssClass="form-control mandatoryfield" ID="txtPenetrantbrand" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left mandatorylbl">
                                                        <label>Penetrant Batch No </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox CssClass="form-control mandatoryfield" ID="txtBatchNo" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left mandatorylbl">
                                                        <label>Thickness </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox CssClass="form-control mandatoryfield" ID="txtThickness" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left mandatorylbl">
                                                        <label>Cleaner/Remover Brand </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox CssClass="form-control mandatoryfield" ID="txtCleanerRemoverBrand" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left mandatorylbl">
                                                        <label>
                                                            Cleaner/Remover Batch
                                                                    No
                                                        </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtCleanerBatch" runat="server" CssClass="form-control mandatoryfield"
                                                            placeholder="Cleaner Batch"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left mandatorylbl">
                                                        <label>Proceduere And Rev.No</label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox CssClass="form-control mandatoryfield" ID="txtProceduerAndRevNo" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left mandatorylbl">
                                                        <label>DWell Time</label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox CssClass="form-control mandatoryfield" ID="txtDwellTime" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left mandatorylbl">
                                                        <label>Surface Condition </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox CssClass="form-control mandatoryfield" ID="txtSurfaceCondition" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left mandatorylbl">
                                                        <label>Developer Brand</label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox CssClass="form-control mandatoryfield" ID="txtDeveloperBrand" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left mandatorylbl">
                                                        <label>Developer Batch No</label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox CssClass="form-control mandatoryfield" ID="txtDeveloperBatchNo" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left mandatorylbl">
                                                        <label>Surface Temprature</label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox CssClass="form-control mandatoryfield" ID="txtSurfaceTemprature" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left mandatorylbl">
                                                        <label>Developement Time</label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox CssClass="form-control mandatoryfield" ID="txtDevelopementTime" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left mandatorylbl">
                                                        <label>Penetrate System</label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox CssClass="form-control mandatoryfield" ID="txtPenetrateSystem" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left mandatorylbl">
                                                        <label>Lighting Equipement </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox CssClass="form-control mandatoryfield" ID="txtLightningEquipment" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left mandatorylbl">
                                                        <label>Technique</label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox CssClass="form-control mandatoryfield" ID="txtTechnique" TextMode="MultiLine"
                                                            Rows="3" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left mandatorylbl">
                                                        <label>Light Intensity</label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox CssClass="form-control mandatoryfield" ID="txtLightIntensity" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                </div>
                                            </div>

                                            <div class="col-sm-12 text-center p-t-10" runat="server">
                                                <label>WELDING DETAILS </label>
                                            </div>

                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left mandatorylbl">
                                                        <label>Joint Identification </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox CssClass="form-control mandatoryfield" TextMode="MultiLine" Rows="3"
                                                            ID="txtJointidentification"
                                                            runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left mandatorylbl">
                                                        <label>Indication Type </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox CssClass="form-control mandatoryfield"
                                                            ID="txtIdentificationType" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left mandatorylbl">
                                                        <label>Indication Size </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox CssClass="form-control mandatoryfield" ID="txtindicationsize" runat="server" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left mandatorylbl">
                                                        <label>Interpretaion </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox CssClass="form-control mandatoryfield"
                                                            ID="txtinterpretaion"
                                                            runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left mandatorylbl">
                                                        <label>Disposition </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox CssClass="form-control mandatoryfield" ID="txtDisposition" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                </div>
                                            </div>

                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left mandatorylbl">
                                                        <label>Sketch Of Indications</label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox ID="txtSketchOfIndications" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left mandatorylbl">
                                                        <label>Inspection Qty </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox CssClass="form-control mandatoryfield" ID="txtInspectionQty"
                                                            onkeypress="return validationNumeric(this);" runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left mandatorylbl">
                                                        <label>Accepted Qty</label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox CssClass="form-control mandatoryfield" ID="txtAcceptedQty"
                                                            onkeypress="return validationNumeric(this);" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 text-center p-t-10" runat="server">
                                                <label>For LoneStar Industries </label>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left mandatorylbl">
                                                        <label>Name </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox CssClass="form-control mandatoryfield" ID="txtLSIInspectorName"
                                                            runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left mandatorylbl">
                                                        <label>Level </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox CssClass="form-control mandatoryfield" ID="txtLSIInspectionLevel"
                                                            runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left mandatorylbl">
                                                        <label>Date </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox CssClass="form-control mandatoryfield datepicker" ID="txtLSIInspectionDate"
                                                            runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 text-center p-t-10" runat="server">
                                                <label>For Third Party </label>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <div class="col-sm-4">
                                                    <div class="text-left mandatorylbl">
                                                        <label>Name </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox CssClass="form-control mandatoryfield" ID="txtthirdPartyInspectorName"
                                                            runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left mandatorylbl">
                                                        <label>Level </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox CssClass="form-control mandatoryfield" ID="txtThirdPartyInspectorLevelName"
                                                            runat="server" />
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="text-left mandatorylbl">
                                                        <label>Date </label>
                                                    </div>
                                                    <div>
                                                        <asp:TextBox CssClass="form-control mandatoryfield datepicker" ID="txtThirdPartyInspectorDate"
                                                            runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 text-center p-t-10">
                                                <asp:LinkButton ID="btnSaveLPIR" runat="server" CssClass="btn btn-cons btn-save AlignTop"
                                                    Text="Save" OnClientClick="return Validate();" OnClick="btnSaveLPIR_Click">
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-cons btn-save AlignTop" Text="Cancel"
                                                    OnClick="btnCancel_Click">
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>

                                    <div id="divOutPut" class="col-sm-12 p-t-10" style="overflow-x: scroll;" runat="server">
                                        <asp:GridView ID="gvLPIHeader" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                            OnRowCommand="gvLPIHeader_OnRowCommand" DataKeyNames="LPIRID">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Report No" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                    HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReportNo" runat="server" Text='<%# Eval("ReportNo")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Report Date" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                    HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReportDate" runat="server" Text='<%# Eval("ReportDate")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Stage Of Test" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                    HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstageoftest" runat="server" Text='<%# Eval("StageOfTest")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditLPI" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                         <img src="../Assets/images/edit-ec.png" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnAdd" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("LPIRID")) %>'>
                                                         <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PDF" ItemStyle-CssClass="text-center" HeaderStyle-Width="10%"
                                                    ItemStyle-Width="10%" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnpdf" runat="server"
                                                            OnClientClick='<%# string.Format("return Print({0});",Eval("LPIRID")) %>'>
                                                          <img src="../Assets/images/pdf.png" />
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <asp:HiddenField ID="hdnLPIRID" runat="server" Value="0" />
                                        <asp:HiddenField ID="hdnTotalItemQty" runat="server" Value="0" />
                                        <asp:HiddenField ID="hdnAddress" runat="server" Value="" />
                                        <asp:HiddenField ID="hdnPhoneAndFaxNo" runat="server" Value="" />
                                        <asp:HiddenField ID="hdnEmail" runat="server" Value="" />
                                        <asp:HiddenField ID="hdnWebsite" runat="server" Value="" />
                                    </div>

                                </div>
                            </div>
                        </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div id="divLPIReport_p" runat="server" style="display: none;">
        <div id="divmainContent_p" runat="server">
            <div class="text-center">
                <label>LIQUID PENETRANT INSPECTION REPORT </label>
            </div>
            <div class="divtable_p p-t-10">
                <table>
                    <tr>
                        <td style="vertical-align: middle;">
                            <label>Customer Name:   </label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblCustomerName_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblReportNo_p" Style="display: block; border-bottom: 1px solid; margin-top: 5px;" runat="server">
                            </asp:Label>
                            <asp:Label ID="lblReportdate_p" Style="margin-top: 5px;" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle;">
                            <label>RFP No:  </label>
                        </td>
                        <td class="vam">
                            <asp:Label ID="lblRFPNo_p" runat="server"></asp:Label>
                        </td>
                        <td style="width: 15%;">
                            <label>Customer PO No:  </label>
                        </td>
                        <td style="width: 30%;">
                            <asp:Label ID="lblCustomerPONo_p" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td style="vertical-align: middle;">
                            <label>Item Name:  </label>
                        </td>
                        <td class="vam">
                            <asp:Label ID="lblItemName_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label>Job Size:  </label>
                        </td>
                        <td>
                            <asp:Label ID="lblJobsize_p" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td style="vertical-align: middle;">
                            <label>Drawing No:  </label>
                        </td>
                        <td class="vam">
                            <asp:Label ID="lblDrawingNo_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label style="border-bottom: 1px solid; margin-top: 5px; width: 100%;">Project Name:  </label>
                            <label style="margin-top: 5px;">Stage Of Test:  </label>
                        </td>
                        <td>
                            <asp:Label ID="lblProjectName_p" Style="display: block; border-bottom: 1px solid; margin-top: 5px;" runat="server"></asp:Label>
                            <asp:Label ID="lblStageoftest_p" Style="margin-top: 5px;" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td style="vertical-align: middle;">
                            <label>BSL No:  </label>
                        </td>
                        <td class="vam">
                            <asp:Label ID="lblBSLNo_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label>Test Date:  </label>
                        </td>
                        <td>
                            <asp:Label ID="lblTestDate_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle;">
                            <label>Material Specification:  </label>
                        </td>
                        <td class="vam">
                            <asp:Label ID="lblMaterialSpecification_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label>Penetrant:  </label>
                        </td>
                        <td>
                            <asp:Label ID="lblPenetrantbrand_p" Style="display: block;" runat="server"></asp:Label>
                            <asp:Label ID="lblPenetrantbatchno_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle;">
                            <label>Thickess:  </label>
                        </td>
                        <td class="vam">
                            <asp:Label ID="lblThickness_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label>Cleaner / Remover:  </label>
                        </td>
                        <td>
                            <asp:Label ID="lblcleanerbrandno_p" Style="display: block;" runat="server"></asp:Label>
                            <asp:Label ID="lblcleanerbatchno_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle;">
                            <label>Procedure & Rev.No:  </label>
                        </td>
                        <td class="vam">
                            <asp:Label ID="lblProcedureAndRevNo_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label>Dwell Time: </label>
                        </td>
                        <td>
                            <asp:Label ID="lblDwellTime_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle;">
                            <label>Surface Condition:  </label>
                        </td>
                        <td class="vam">
                            <asp:Label ID="lblSurfacecondition_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label>Developer: </label>
                        </td>
                        <td>
                            <asp:Label ID="lbldeveloperbrand_p" Style="display: block;" runat="server"></asp:Label>
                            <asp:Label ID="lbldeveloperbatchno_p" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td style="vertical-align: middle;">
                            <label>Surface Temp:  </label>
                        </td>
                        <td class="vam">
                            <asp:Label ID="lblsurfacetemp_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label>Developement Time: </label>
                        </td>
                        <td>
                            <asp:Label ID="lbldevelopementtime_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle;">
                            <label>Penetrant System:  </label>
                        </td>
                        <td class="vam">
                            <asp:Label ID="lblpenetrantsystem_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label>Lighting Equipment: </label>
                        </td>
                        <td>
                            <asp:Label ID="lbllightingequipment_p" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle;">
                            <label>Technique:  </label>
                        </td>
                        <td class="vam">
                            <asp:Label ID="lbltechnique_p" runat="server"></asp:Label>
                        </td>
                        <td>
                            <label>light Intensity: </label>
                        </td>
                        <td>
                            <asp:Label ID="lbllightintensity_p" runat="server"></asp:Label>
                        </td>
                    </tr>

                </table>
            </div>
        </div>

        <div class="p-t-10">
            <div class="p-t-10" runat="server">
                <asp:GridView ID="gvlpireport_p" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                    CssClass="table table-hover table-bordered medium" EmptyDataText="No Records Found" DataKeyNames="LPIRDID">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                            HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'
                                    Style="text-align: center"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Joint Identification">
                            <ItemTemplate>
                                <asp:Label ID="lblJointIdentification" runat="server" Text='<%# Eval("JointIndentification")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Identification Type">
                            <ItemTemplate>
                                <asp:Label ID="lblIndicationType" runat="server" Text='<%# Eval("IndicationType")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Indication Size">
                            <ItemTemplate>
                                <asp:Label ID="lblIndicationSize" runat="server" Text='<%# Eval("IndicationSize")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Inter Pretation">
                            <ItemTemplate>
                                <asp:Label ID="lblInterPretation" runat="server" Text='<%# Eval("InterPretation")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Disposition">
                            <ItemTemplate>
                                <asp:Label ID="lblDisposition" runat="server" Text='<%# Eval("Disposition")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <div id="divbelowcontent_p" runat="server">
            <div>
                <label>Sketch Of Indications :  </label>
                <asp:Label ID="lblsketchofindications_p" runat="server"></asp:Label>
            </div>
            <div class="row p-t-5">
                <div class="col-sm-6">
                    <label>Inspection Qty </label>
                    <asp:Label ID="lblInspectionQty_p" runat="server"></asp:Label>
                </div>
                <div class="col-sm-6">
                    <label>Accepted Qty </label>
                    <asp:Label ID="lblAcceptedQty_p" runat="server"></asp:Label>
                </div>
            </div>

            <div class="row p-t-5">
                <div class="col-sm-6">
                    <label>For Lonestar Industries </label>
                </div>
                <div class="col-sm-6">
                    <label>For Third Party </label>
                </div>
            </div>
            <div class="row p-t-5">
                <div class="col-sm-6">
                    <label>Name </label>
                </div>
                <div class="col-sm-6">
                    <label>Name </label>
                </div>
            </div>
            <div class="row p-t-5">
                <div class="col-sm-6">
                    <label>Date </label>
                </div>
                <div class="col-sm-6">
                    <label>Date </label>
                </div>
            </div>
            <div class="row p-t-5">
                <div class="col-sm-6">
                    <label>Level </label>
                </div>
                <div class="col-sm-6">
                    <label>Level </label>
                </div>
            </div>
            <div class="row p-t-5">
                <div class="col-sm-6">
                    <label>Signature </label>
                </div>
                <div class="col-sm-6">
                    <label>Signature </label>
                </div>
            </div>
        </div>

    </div>


</asp:Content>
