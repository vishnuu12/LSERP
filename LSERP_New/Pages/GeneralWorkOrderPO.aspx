<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="GeneralWorkOrderPO.aspx.cs" Inherits="Pages_GeneralWorkOrderPO" %>


<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <style type="text/css">
        table#ContentPlaceHolder1_GeneralWorkOrderSubIndent td {
            color: #000;
            font-weight: bold;
        }
                table#ContentPlaceHolder1_gvGeneralWorkOrderPO td {
            color: #000;
            font-weight: bold;
        }
            </style>
    <script type="text/javascript">


        function sharePO(SGWOID) {
            swal({
                title: "Are you sure?",
                text: "If Yes,PO Share for Approval",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Share it!",
                closeOnConfirm: false
            }, function () {
                __doPostBack('SharePO', SGWOID);
            });
            return false;
        }
        function OpenTab(Name) {
            var names = ["OtherCharges", "Tax"];
            var text = document.getElementById('<%=Tax.ClientID %>');

            for (var i = 0; i < names.length; i++) {
                if (Name == names[i]) {
                    var a = text.id.replace('Tax', Name);
                    document.getElementById(a).style.display = "block";
                    document.getElementById('li' + names[i]).className = "active";
                }
                else {
                    var b = text.id.replace('Tax', names[i]);
                    document.getElementById(b).style.display = "none";
                    document.getElementById('li' + names[i]).className = "";
                }
            }
            if (Name == 'Tax') {
                $('#liOtherCharges').removeClass('active');
                $('#liOtherCharges a').removeClass('active');
                $('#liTax a').addClass('active');
                $('#liTax').addClass('active');
            }
            else {
                $('#liTax').removeClass('active');
                $('#liTax a').removeClass('active');
                $('#liOtherCharges a').addClass('active');
                $('#liOtherCharges').addClass('active');
            }
        }

        function ShowTaxPopUp() {
            $('#mpeAddtax').modal('show');
            $('div').removeClass('modal-backdrop in');
            return false;
        }

        function HideTaxPopUp() {
            $('#mpeAddtax').modal('hide');
            $('div').removeClass('modal-backdrop in');
            return false;
        }

        function ViewIndentAttach(index) {
            __doPostBack('ViewIndentAttach', index);
            return false;
        }

        function ViewIndentFile(index) {
            __doPostBack('ViewIndentFile', index);
            return false;
        }

        function ShowPoPrintPopUp() {
            $('#mpePoDetails_p').modal({
                backdrop: 'static'
            });
            return false;
        }

        function PrintWorkOrderPO(Approvedtime, POStatus, ApprovedBy) {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var Address = $('#ContentPlaceHolder1_hdnAddress').val();
            var PhoneAndFaxNo = $('#ContentPlaceHolder1_hdnPhoneAndFaxNo').val();
            var Email = $('#ContentPlaceHolder1_hdnEmail').val();
            var WebSite = $('#ContentPlaceHolder1_hdnWebsite').val();

            var WPOContent = $('#ContentPlaceHolder1_divWorkOrderPoPrint_p').html();

            var MainContent = $('#divmaincontent_p').html();
            var SpecificationTax = $('#divspecificationandtax').html();

            var RowLen = $('#ContentPlaceHolder1_gvWorkOrderPOItemDetails_p').find('tr').length;

            winprint.document.write("<html><head><title>");
            winprint.document.write("Offer");
            winprint.document.write("</title>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='../Assets/css/ep-style.css'/>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='../Assets/css/main.css' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet'  href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/printg.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            //  winprint.document.write("<link rel='stylesheet' href='" + topstrip + "' type='text/css'/>");
            //  winprint.document.write("<style type='text/css'> @media print{ page-break-after: avoid; }  </style>");
            winprint.document.write("<style type='text/css'>  .page_generateoffer { margin:5mm; } .lbltaxothercharges { margin-left:15px; } .spntaxothercharges { margin-right:6px; } </style>");

            winprint.document.write("<div class='print-page' style='position:fixed;border:2px solid #000;width:200mm;height:287mm;margin-top:5mm;margin-bottom:5mm;margin-left:5mm;margin-right:5mm'></div>");
            winprint.document.write("<table style='width:200mm;height:287mm;margin-left:20px;border:0px !important'><thead><tr><td>");
            winprint.document.write("<div class='col-sm-12 print-generateoffer' style='border-bottom:1px solid #000;text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;height:110px'>");
            winprint.document.write("<div>");
            winprint.document.write("<div>");
            winprint.document.write("<div class='col-sm-12 padding0' id='divLSERPLogo' style='margin:20px auto;display:block;padding:5px 0px;'>");
            winprint.document.write("<div class='row' style='position:fixed;width:200mm;left:5mm;'>");
            winprint.document.write("<div class='col-sm-3'>");
            winprint.document.write("<img src=" + $('#ContentPlaceHolder1_hdnLonestarLogo').val() + " alt='lonestar-image' width='90px'>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-6 text-center'>");
            //winprint.document.write("<h3 style='font-weight:600;font-size:24px;font-style:italic;color:#000;font-family: Arial;display: contents;'>LONESTAR</h3>");
            //winprint.document.write("<span style='font-weight:600;font-size:24px ! important;font-family: Times New Roman;padding-left:5px ! important;'>INDUSTRIES</span>");

            winprint.document.write("<h3 style='font-weight:600;font-size:20px;color:#000;font-family: Times New Roman;display: contents;'>" + $('#ContentPlaceHolder1_hdnCompanyName').val() + "</h3>");
            winprint.document.write("<span style='font-weight:600;font-size:11px ! important;font-family: Times New Roman;'>" + $('#ContentPlaceHolder1_hdnFormalCompanyname').val() + "</span>");

            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;width: 100%;padding:0 0px'>" + Address + "</p>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + PhoneAndFaxNo + "</p>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + Email + "</p>");
            winprint.document.write("<p style='font-size:11px !important;font-weight:500;color:#000;padding:0px 0px'>" + WebSite + "</p>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-3'>");
            winprint.document.write("<img src=" + $('#ContentPlaceHolder1_hdnISOLogo').val() + " alt='lonestar-image' height='90px'>");
            winprint.document.write("</div></div>");
            winprint.document.write("</div></div></div>");
            winprint.document.write("</td></tr></thead>");
            winprint.document.write("<tbody><tr><td>");

            //if (parseInt(RowLen) <= 10) {
            winprint.document.write("<div class='page_generateoffer'>");
            winprint.document.write(WPOContent);
            winprint.document.write("</div>");
            //}
            //else {
            //    winprint.document.write("<div class='page_generateoffer'>");
            //    winprint.document.write(MainContent);

            //    winprint.document.write("<div class='p-t-10'>");
            //    winprint.document.write("<table class='table table-hover medium' cellspacing='0' rules='all' border='1' id='ContentPlaceHolder1_gvWorkOrderPOItemDetails_p' style='border-collapse:collapse;'>");
            //    winprint.document.write("<tbody>");

            //    for (var j = 0; j < 10; j++) {
            //        winprint.document.write("<tr align='center'>" + $('#ContentPlaceHolder1_gvWorkOrderPOItemDetails_p').find('tr')[j].innerHTML + "</tr>");
            //    }

            //    //  k = k + 15;
            //    winprint.document.write("</tbody>");
            //    winprint.document.write("</table>");
            //    winprint.document.write("</div>");

            //    winprint.document.write("</div>");

            //    winprint.document.write("<div class='page_generateoffer'>");
            //    winprint.document.write(SpecificationTax);
            //    winprint.document.write("</div>");
            //}

            winprint.document.write("</td></tr></tbody>");
            winprint.document.write("<tfoot><tr><td>");
            winprint.document.write("<div class='row' style='border-top:1px solid #000;padding-top:10px;height:30mm;'>");
            winprint.document.write("<div class='text-center' style='color:#000;padding-left:50px;font-weight:bold;'>KINDLY RETURN THE DUPLICATE COPY OF THE ORDER DULY SIGNED AS A TOKEN OFACCEPTANCE</div>");
            //style='height:100px;display:flex;align-items:flex-end;justify-content:flex-start'
            winprint.document.write("<div class='col-sm-4 p-t-85'>");
            winprint.document.write("<label style='padding-left:15px;font-weight:bold;'>PREPARED BY</label>");
            winprint.document.write("</div>");

            winprint.document.write("<div class='col-sm-4 p-t-85'>");
            winprint.document.write("<label style='padding-left:15px;font-weight:bold;'> CHECKED BY</label>");
            winprint.document.write("</div>");

            winprint.document.write("<div class='col-sm-4 text-center'>");
            winprint.document.write("<label style='display:block;width:100%;font-weight:bold;padding-top:5px;padding-left:84px;'>" + $('#ContentPlaceHolder1_hdnCompanyNameFooter').val() + "</label>");
            if (POStatus == "1") {
                if (ApprovedBy != '') {
                    winprint.document.write("<div id='divdigitalsign' class='p-t-10' style='display: block;'>");
                    winprint.document.write("<div style='text-align: end;' class='p-r-10'><label style='text-align: end;font-weight:bold;'> Digitally Signed By </label></div>");
                    winprint.document.write("<div style='text-align: end;' class='p-r-10'><label style='font-weight:bold;'> " + "UMAMAGESH G" + " </label></div>");
                    winprint.document.write("<div style='text-align: end;' class='p-r-10'><label>" + Approvedtime + "</label></div></div>");
                }
            }
            if (ApprovedBy != '')
                winprint.document.write("<label style='display:block;width:100%;padding-top:10px;font-weight:bold;padding-left:84px;'>AUTHORISED SIGNATORY</label>");
            else
                winprint.document.write("<label style='display:block;width:100%;padding-top:50px;font-weight:bold;'>AUTHORISED SIGNATORY</label>");

            winprint.document.write("</div>");
            winprint.document.write("</div>");
            winprint.document.write("</td></tr></tfoot></table>");
            winprint.document.write("</body></html>");

            //winprint.document.write("<div class='col-sm-12' style='height:30mm;'>");
            //winprint.document.write("<div>");
            //winprint.document.write("<div class='col-sm-12 row' style='margin-bottom:20px;border-top:1px solid #000;position:fixed;width:200mm;bottom:0px'>");
            //winprint.document.write(FooterContent);
            //winprint.document.write("</div>");
            //winprint.document.write("</div></div>");

            setTimeout(function () {
                winprint.document.close();
                winprint.focus();
                winprint.print();
                // winprint.close();
            }, 1000);
        }

        function deleteConfirmOtherCharges(GSPOOCDID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Item will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                __doPostBack('deletegvRowotherCharges', GSPOOCDID);
            });
            return false;
        }

        function deleteConfirmTaxDetailsCharges(GSPOTDID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Item will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                __doPostBack('deletegvRowtaxCharges', GSPOTDID);
            });
            return false;
        }
    </script>
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
                                    <h3 class="page-title-head d-inline-block">General Work Order PO</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Purchase</a></li>
                                <li class="active breadcrumb-item" aria-current="page">General Work Order PO</li>
                            </ol>
                        </nav>
                        <a id="help" href="" alt="" style="margin-top: 4px;">
                            <img src="../Assets/images/help.png" />
                        </a>
                    </div>
                </div>
            </div>
        </div>
                <asp:UpdatePanel ID="upDocumenttype" runat="server">
                <Triggers>
                   <%-- <asp:PostBackTrigger ControlID="btnSave" />
                    <asp:PostBackTrigger ControlID="imgExcel" />--%>
                </Triggers>
                <ContentTemplate>

                    <div class="card-container">
                        <div id="divAdd" runat="server">
                        </div>
             <div id="divInput" runat="server">
                  <div class="col-sm-12 p-t-10" style="overflow-x: auto;" >

                                            <asp:GridView ID="GeneralWorkOrderSubIndent" runat="server" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found"
                                                OnRowCommand="GeneralWorkOrderSubIndent_OnRowCommand"
                                                CssClass="table table-hover table-bordered medium" 
                                                Font-Names="arial" Font-Size="10px"
                                                DataKeyNames="SGWOID,GWOID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="GWI No" SortExpression="General Work Order Indent Number" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="GWOID" runat="server" Value='<%# Bind("GWOID") %>' />
                                                            <asp:Label ID="lblGWI" Text=' <%# Eval("GWI")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Service Description" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="SGWOID" runat="server" Value='<%# Bind("SGWOID") %>' />
                                                            <asp:Label ID="lblServiceDescription" runat="server" Text='<%# Eval("ServiceDescription")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Quantity" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("QuantityUnit")%>' ></asp:Label>
                                                            <asp:Label ID="Name" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Job List" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="JobListId" runat="server" Value='<%# Bind("JobListId") %>' />
                                                            <asp:Label ID="lblJobList" runat="server" Text='<%# Eval("JobList")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Remarks" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                           <%--         <asp:TemplateField HeaderText="Add Tax">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnAddTax" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="AddTax">  <img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="View Attach">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnViewIndentAttach" runat="server"
                                                                Text="View Attach"
                                                                OnClientClick='<%# string.Format("return ViewIndentAttach({0});",((GridViewRow) Container).RowIndex) %>'> <img src="../Assets/images/view.png" alt=""/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                              <asp:HiddenField ID="hdnGWOID1" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnSGWOID1" runat="server" Value="0" />
                                        </div>
                    <div class="text-center">
                        <div class="h-style">
                                                <asp:Label ID="lblGWOI" runat="server" Style="font-weight: 700;color: #d92550;margin-right: -200%;">
                                                    Don't Forget to Add Tax Details
                                                </asp:Label>
                                            </div>
                            <div class="col-sm-12 p-t-10">
                                <div class="col-sm-4 text-right">
                                    <div class="text-left">
                                        <label class="form-label mandatorylbl" style="margin-top: 10px;">Supplier</label>
                                    </div>
                                    <div>
                                        <asp:DropDownList Style="width: 185px;" ID="ddlSupplier" TabIndex="9" runat="server" ToolTip="Select Supplier" AutoPostBack="true"
                                            OnChange="showLoader();" CssClass="form-control mandatoryfield">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4">
                                    <div class="text-left">
                                        <label class="form-label mandatorylbl" style="margin-top: 10px;">Location</label>
                                    </div>
                                    <div>
                                        <asp:DropDownList Style="width: 185px;" ID="ddlLocation" TabIndex="9" runat="server" ToolTip="Select Location" AutoPostBack="true"
                                            OnChange="showLoader();" CssClass="form-control mandatoryfield">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-4  text-right">
                                    <div class="text-left">
                                        <label class="form-label mandatorylbl" style="margin-top: 10px;">Quote Reference No</label>
                                    </div>
                                    <div>
                                        <asp:TextBox runat="server" ID="txtQuote" AutoComplete="off" CssClass="form-control mandatoryfield"
                                            Width="70%" placeholder="Enter Quote" />
                                    </div>
                                </div>
                                </div>
                        <div class="col-sm-12 p-t-10">
                                <div class="col-sm-4">
                                    <div class="text-left">
                                        <label class="form-label mandatorylbl" style="margin-top: 10px;">Delivery Date</label>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtDelivery" runat="server" Width="70%" CssClass="form-control datepicker mandatoryfield"
                                            AutoComplete="off" ToolTip="Enter Delivery Date" placeholder="Enter Delivery Date">
                                        </asp:TextBox>
                                    </div>
                                </div>
                              <div class="col-sm-4">
                                    <div class="text-left">
                                        <label class="form-label mandatorylbl" style="margin-top: 10px;">Quantity</label>
                                    </div>
                                    <div>
                                        <asp:TextBox runat="server" ID="txtQuantity" AutoComplete="off" CssClass="form-control mandatoryfield"
                                            Width="70%" placeholder="Enter Quantity" />
                                    </div>
                                </div>
                              <div class="col-sm-4">
                                    <div class="text-left">
                                        <label class="form-label mandatorylbl" style="margin-top: 10px;">Unit Cost</label>
                                    </div>
                                    <div>
                                        <asp:TextBox runat="server" ID="txtUnitCost" AutoComplete="off" CssClass="form-control mandatoryfield"
                                            Width="70%" placeholder="Enter Unit Cost" />
                                    </div>
                                </div>
                            </div>
                              <div class="col-sm-12 p-t-10">
                                <div class="col-sm-4">
                                    <div class="text-left">
                                        <label class="form-label mandatorylbl" style="margin-top: 10px;">Payment Days</label>
                                    </div>
                                    <div>
                                        <asp:TextBox ID="txtPayment" runat="server" Width="70%" CssClass="form-control mandatoryfield"
                                           ToolTip="For Eg: After Some Days" placeholder="Enter Payment Days"
                                            TextMode="MultiLine" MaxLength="300" autocomplete="nope">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                            <div class="col-sm-4">
                                                <div class="text-left">
                                                    <label style="margin-top: 10px;">Note</label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtNote" runat="server" TabIndex="6" placeholder="Enter Note"
                                                        ToolTip="Enter Note" TextMode="MultiLine" CssClass="form-control "
                                                        MaxLength="300" autocomplete="nope" />
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <div class="text-left">
                                                    <label style="margin-top: 10px;">Remark</label>
                                                </div>
                                                <div>
                                                    <asp:TextBox ID="txtRemark" runat="server" TabIndex="6" placeholder="Enter Remark"
                                                        ToolTip="Enter Remark" TextMode="MultiLine" CssClass="form-control "
                                                        MaxLength="300" autocomplete="nope" />
                                                </div>
                                            </div>
                                            </div>
                         <div class="col-sm-12 text-center p-t-10">
                                                <asp:LinkButton ID="btnSave" Text="Save" CssClass="btn btn-cons btn-save AlignTop"
                                                    OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divInput');" OnClick="btnSave_ClickPO"
                                                    runat="server" />
                                                <asp:LinkButton ID="btnCancel" Text="Cancel" CssClass="btn btn-cons btn-danger AlignTop" OnClick="btnCancel_Click"
                                                    runat="server" />
                                            </div>
                        </div>
                 
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="col-md-12 row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                                    <asp:Label ID="lbltitle" runat="server"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="overflow-x: auto;">

                                            <asp:GridView ID="gvGeneralWorkOrderPO" runat="server" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found"
                                                 OnRowCommand="gvGeneralWorkOrderPO_OnRowCommand"
                                                CssClass="table table-hover table-bordered medium" Font-Names="arial" Font-Size="10px" 
                                                DataKeyNames="SGWOID,GWOID,FileName">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="GWI No" SortExpression="General Work Order Indent Number" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="GWOID" runat="server" Value='<%# Bind("GWOID") %>' />
                                                            <asp:Label ID="lblGWI" Style="white-space:nowrap;" Text=' <%# Eval("GWI")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Service Description" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="SGWOID" runat="server" Value='<%# Bind("SGWOID") %>' />
                                                            <asp:Label ID="lblServiceDescription" runat="server" Text='<%# Eval("ServiceDescription")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Quantity" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("QuantityUnit")%>' ></asp:Label>
                                                            <asp:Label ID="Name" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Job List" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="JobListId" runat="server" Value='<%# Bind("JobListId") %>' />
                                                            <asp:Label ID="lblJobList" runat="server" Text='<%# Eval("JobList")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Remarks" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Add Items">
                                                     <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnAdd" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="AddSPO"><img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                     </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Add Tax">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnAddTax" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                               Visible='<%# Eval("TaxBtn").ToString() == "0" ? false : true %>'
                                                                CommandName="AddTax">  <img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View Attach">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnViewIndentAttach" runat="server"
                                                                Text="View Attach"
                                                                OnClientClick='<%# string.Format("return ViewIndentFile({0});",((GridViewRow) Container).RowIndex) %>'> <img src="../Assets/images/view.png" alt=""/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PDF">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnPDF" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                 Visible='<%# Eval("ShareBtn").ToString() == "1" ? true : false %>'
                                                                CommandName="PDF">  <img src="../Assets/images/pdf.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Share PO">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnShare" runat="server" CssClass="btn btn-cons btn-success" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                Visible='<%# Eval("ShareBtn").ToString() == "1" ? true : false %>'
                                                               OnClientClick='<%# string.Format("return sharePO({0});",Eval("SGWOID")) %>' Text="Share PO" CommandName="SharePO"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnGWOPOID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnGWOID" runat="server" Value="0" />

                                            <asp:HiddenField ID="hdnWPOID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnAddress" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnPhoneAndFaxNo" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnEmail" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnWebsite" runat="server" Value="" />

                                            <asp:HiddenField ID="hdnCompanyName" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnFormalCompanyname" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnCompanyNameFooter" runat="server" Value="" />

                                            <asp:HiddenField ID="hdnLonestarLogo" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnISOLogo" runat="server" Value="" />
                                             
                                        </div>
                                       <iframe id="ifrm" visible="false" runat="server"></iframe>
                                    </div>
                                </div>
                            </div>
                        </div> 
                      
        </div>  
    <div class="modal" id="mpePoDetails_p">
        <div class="modal-dialog" style="height: 100%;">
            <div class="modal-content" style="width: 125%;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always" Style="height: 100%;">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnDownLoad" />
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
                        <div class="modal-body" style="height: 100%;overflow-x: auto;">
                            <div class="inner-container" style="height: 100%;">
                                <div class="ip-div text-center" style="margin-bottom: 10px;">
                                    <asp:LinkButton Text="Download" ID="btnDownLoad" OnClick="btndownloaddocs_Click"
                                        runat="server">   <img src="../Assets/images/pdf.png" /></asp:LinkButton>
                                </div>
                                <div id="divWorkOrderPoPrint_p" runat="server" style="padding: 10px; width: 200mm;">
                                    <div id="divmaincontent_p">
                                        <div class="text-center">
                                            <label style="font-size: 20px !important; text-decoration: underline; font-weight: 700; margin-bottom: 10px;">
                                                General Work Order</label>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6">
                                                <label style="display: block; font-weight: 700">
                                                    To</label>
                                                <asp:Label ID="lblSupplierChainVendorName_p" runat="server" Style="margin-top: 10px; margin-left: 20px"></asp:Label>
                                                <asp:Label ID="lblReceiverAddress_p" runat="server" Style="margin-top: 10px; margin-left: 20px"></asp:Label>
                                                <div class="p-t-10">
                                                    <asp:Label ID="lblGSTNumber_p" Style="margin-left: 20px;" CssClass="p-l-20" runat="server"></asp:Label>
                                                </div>
                                                <div class="p-t-5" style="margin-right: 10px; margin-top: 5px; padding: 5px; border: 1px solid;">
                                                    <asp:Label ID="lblRange_p" Style="font-weight: bold;" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">

                                                <div id="divGSTREV0" runat="server" visible="false">
                                                    <div style="display: inline-block; border: 1px solid #000; padding: 10px">
                                                        <label style="font-size: 20px; font-weight: 700; text-decoration: underline;">
                                                            Consignee Address
                                                        </label>
                                                        <asp:Label ID="lblConsigneeAddress_p" Style="padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                        <asp:Label ID="lblTNGSTNo" Style="display: block; padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                        <asp:Label ID="lblCSTNo" Style="display: block; padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                        <asp:Label ID="lblECCNo" Style="display: block; padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                        <asp:Label ID="lblTINNo" Style="display: block; padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                        <asp:Label ID="lblGSTNo" Style="padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                                <div id="divGSTREV1" runat="server" visible="false">
                                                    <div style="display: inline-block; border: 1px solid #000; padding: 10px">
                                                        <label style="font-size: 20px; font-weight: 700; text-decoration: underline;">
                                                            Consignee Address
                                                        </label>
                                                        <asp:Label ID="lblConsigneeAddressRev1_p" Style="padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                        <asp:Label ID="lblCINNo_P" Style="display: block; padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                        <asp:Label ID="lblPANNo_p" Style="display: block; padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                        <asp:Label ID="lblTANNo_p" Style="display: block; padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                        <asp:Label ID="lblGSTIN_p" Style="display: block; padding-top: 5px; font-weight: bold;" runat="server"></asp:Label>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6">
                                            </div>
                                            <div class="col-sm-6">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6">
                                            </div>
                                            <div class="col-sm-6">
                                                <label style="font-weight: bold; display: inline-block;">
                                                    WO No</label>
                                                <asp:Label ID="lblWoNo_p" runat="server" Style="font-weight: bold"></asp:Label>

                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6">
                                            </div>
                                            <div class="col-sm-6">
                                                <label style="font-weight: bold; display: inline-block;">
                                                    WO Date</label>
                                                <asp:Label ID="lblWODate_p" runat="server" Style="font-weight: bold"></asp:Label>
                                            </div>
                                        </div>
                                        <%--   <div class="row m-t-10">
                                        <div class="col-sm-6">
                                            &nbsp;
                                        </div>
                                        <div class="col-sm-6" style="font-weight: bold">
                                            <div class="m-t-10">
                                                <div class="col-sm-5">
                                                    <label style="font-weight: bold">
                                                        WO No</label>
                                                </div>
                                                <div class="col-sm-2">
                                                    <label style="font-weight: bold">
                                                        :
                                                    </label>
                                                </div>
                                                <div class="col-sm-5">
                                                    <asp:Label ID="lblWoNo_p" runat="server" Style="font-weight: bold"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="m-t-10">
                                                <div class="col-sm-5">
                                                    <label style="font-weight: bold">
                                                        WO Date</label>
                                                </div>
                                                <div class="col-sm-2" style="font-weight: bold">
                                                    :
                                                </div>
                                                <div class="col-sm-5">
                                                    <asp:Label ID="lblWODate_p" runat="server" Style="font-weight: bold"></asp:Label>
                                                </div>
                                            </div>
                                            <label>
                                        </div>
                                    </div>--%>
                                        <div class="row">
                                            <div class="col-sm-6">
                                               <%-- <label style="font-weight: bold">
                                                    AMD Reason
                                                </label>
                                                <asp:Label ID="lblAMDReson_p" runat="server"></asp:Label>--%>
                                            </div>
                                            <div class="col-sm-6">
                                           <%--     AMD Date </label>
                                            <asp:Label ID="lblAMDDate_p" runat="server"></asp:Label>--%>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-6">
                                               <%-- <label style="font-weight: bold">
                                                    RFP No</label>
                                                <asp:Label ID="lblRFPNo_p" runat="server"></asp:Label>--%>
                                            </div>
                                            <div class="col-sm-6">
                                                <label style="font-weight: bold; display: inline-block;">
                                                    QUOTE Ref No</label>
                                            <asp:Label ID="lblQuoteRefNo_p" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                        <div class="p-t-10" style="overflow-x: auto;">
                                            <asp:GridView ID="gvWorkOrderPOItemDetails_p" runat="server" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Indent No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIndentNo" runat="server" Text='<%# Eval("IndentNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartName" runat="server" Text='<%# Eval("MaterialName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="QTY" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPartQuantity" runat="server" Text='<%# Eval("Quantity")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit Cost" HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUnitCost" runat="server" Text='<%# Eval("UnitCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Cost" HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalCost" runat="server" Text='<%# Eval("TotalCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div id="divspecificationandtax">
                                        <div class="row m-t-10">
                                            <div class="col-sm-6" style="font-weight: 700">
                                                <div class="m-t-10">
                                                    <label>
                                                        Rupees In Words</label>
                                                    <asp:Label ID="lblRupeesInwords_p" runat="server"></asp:Label>
                                                </div>
                                                <div class="m-t-10">
                                                    <label>
                                                        Delievery</label>
                                                    <asp:Label ID="lblDeliveryDate_p" runat="server"></asp:Label>
                                                </div>
                                                <div class="m-t-10">
                                                    <label>
                                                        Payment</label>
                                                    <asp:Label ID="lblPayment_p" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-6" style="border: 1px solid #000; padding-left: 0 !important; padding-right: 0 !important; margin-top: -10px;">
                                                <div id="divothercharges_p" class="divothercharges" runat="server">
                                                </div>
                                                <div class="m-t-10" style="width: 100%; float: left">
                                                    <label style="float: left; margin-left: 15px">
                                                        Sub Total INR</label>
                                                    <asp:Label ID="lblSubTotal_p" runat="server" Style="float: right; margin-right: 6px"></asp:Label>
                                                </div>

                                                <div id="divtax_p" class="divtax" runat="server">
                                                </div>
                                                <%--<div class="m-t-10 padd-lr-15" style="width: 100%; float: left">
                                                    <label style="float: left">
                                                        SGST @
                                                    </label>
                                                    <asp:Label ID="lblSGSTPercentage_p" runat="server" Style="float: right; margin-right: 10px"></asp:Label>
                                                </div>
                                                <div class="m-t-10 padd-lr-15" style="width: 100%; float: left">
                                                    <label style="float: left">
                                                        CGST @
                                                    </label>
                                                    <asp:Label ID="lblCGSTPercentage_p" runat="server" Style="float: right; margin-right: 10px"></asp:Label>
                                                </div>--%>
                                                <div class="m-t-10 padd-lr-15 p-t-10" style="width: 100%; float: left; border: 1px solid #000">
                                                    <label style="font-weight: bold;">
                                                        Total Amount</label>
                                                    <asp:Label ID="lblTotalAmount_p" runat="server" Style="float: right; font-weight: bold;"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="m-t-10" style="color: #000">
                                            <h6 style="text-decoration: underline; font-weight: 700">Specifictaion Enclosed</h6>
                                            <ol style="padding-left: 15px">
                                                <li>Test certificate must be submitted for approval prior to dispatch.</li>
                                                <li>Material should be Dispatched Strictly as per dispatch instruction</li>
                                                <li>Material received at store without proper documentation will not be accepted and
                                                    all waiting charges will be on supplier account</li>
                                                <li>Kindly return the Duplicate copy of the order duly signed as a token of acceptance.</li>
                                                <li>Please Mention our Workorder Number in Your Invoice.</li>
                                                <li>Offer terms will be as per our GTC.</li>
                                                <li>
                                                    <asp:Label ID="lblNote_p" runat="server"></asp:Label>
                                                </li>
                                            </ol>
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
    <div class="modal" id="mpeAddtax" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">
                                <asp:Label runat="server" ID="lblSpoNumber_T"></asp:Label></h4>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="docdiv_t" class="docdiv">
                                <div class="inner-container">
                                    <ul class="nav nav-tabs lserptabs" style="display: inline-block; width: 100%; background-color: cadetblue; text-align: right; font-size: x-large; font-weight: bold; color: whitesmoke;">
                                        <li id="liOtherCharges" class="active"><a href="#OtherCharges" class="tab-content" data-toggle="tab" onclick="OpenTab('OtherCharges');">
                                            <p style="margin-left: 10px; text-align: center; color: black;">
                                                Other Charges
                                            </p>
                                        </a></li>
                                        <li id="liTax"><a href="#Tax" class="tab-content active"
                                            data-toggle="tab" onclick="OpenTab('Tax');">
                                            <p style="margin-left: 10px; text-align: center; color: black;">
                                                Tax
                                            </p>
                                        </a></li>
                                    </ul>
                                    <div id="OtherCharges" runat="server" style="display: none;">
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-3">
                                                <label class="form-label mandatorylbl">
                                                    Other Charges
                                                </label>
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="ddlOtherCharges_t" runat="server" TabIndex="1" ToolTip="Select Item Name"
                                                    CssClass="form-control mandatoryfield">
                                                    <asp:ListItem Selected="True" Value="0" Text="-- Select --" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-3">
                                                <label class="form-label mandatorylbl">
                                                    Value
                                                </label>
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtOtherCharges_t" runat="server" TabIndex="6" placeholder="** Please Type value(EX:100)"
                                                    onkeypress="return validationDecimal(this);"
                                                    CssClass="form-control mandatoryfield"
                                                    autocomplete="nope"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-3">
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <asp:LinkButton Text="Save" CssClass="btn btn-cons btn-save  AlignTop" ID="btnSaveOtherCharges"
                                                        CommandName="Othercharges" runat="server" 
                                                        OnClick="btnSaveTaxAndOtherCharges_Click" />
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                            </div>
                                            <div class="col-sm-3">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvSupplierPOOtherCharges" runat="server" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                               DataKeyNames="SGWPOID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("ChargesName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Value" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblValue" runat="server" Text='<%# Eval("Value")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                        HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                OnClientClick='<%# string.Format("return deleteConfirmOtherCharges({0});",Eval("GWPOOCDID")) %>'>   <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                    <div id="Tax" runat="server">
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-3">
                                                <label class="form-label mandatorylbl">
                                                    Tax
                                                </label>
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:DropDownList ID="ddlTax_t" runat="server" TabIndex="1" ToolTip="Select Tax Name"
                                                    CssClass="form-control mandatoryfield">
                                                    <asp:ListItem Selected="True" Value="0" Text="-- Select --" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-3">
                                                <label class="form-label mandatorylbl">
                                                    Value
                                                </label>
                                            </div>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtTaxValue_t" runat="server" TabIndex="6" placeholder="** Please Type Percentage value(EX:4)"
                                                    onkeypress="return validationDecimal(this);"
                                                    CssClass="form-control mandatoryfield"
                                                    autocomplete="nope"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-3">
                                            </div>
                                            <div class="col-sm-3">
                                                <div class="form-group">
                                                    <asp:LinkButton Text="Save" CssClass="btn btn-cons btn-save  AlignTop" ID="btntax"
                                                        CommandName="Tax" runat="server" 
                                                        OnClick="btnSaveTaxAndOtherCharges_Click" />
                                                </div>
                                            </div>
                                            <div class="col-sm-3">
                                            </div>
                                            <div class="col-sm-3">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvTaxDetails" runat="server" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                               DataKeyNames="SGWOID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("TaxName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Value" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblValue" runat="server" Text='<%# Eval("Value")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                        HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDelete" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                OnClientClick='<%# string.Format("return deleteConfirmTaxDetailsCharges({0});",Eval("GWPOTDID")) %>'>   <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnSGWOID" runat="server" Value="0" />
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <div class="col-sm-12 text-center">
                                        <asp:Label ID="lblPOAmount_T" Style="font-size: large; font-weight: bold;color:brown;" runat="server"></asp:Label>
                                    </div>
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
                        <asp:HiddenField ID="hdnPoqty" runat="server" Value="0" />
                        <asp:HiddenField ID="hdnAttachementFlag" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

</asp:Content>
