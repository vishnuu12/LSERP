<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="NCQCApproval.aspx.cs" Inherits="Pages_NCQCApproval" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function ShareRaiseProblem() {
            swal({
                title: "Are you sure Want Share The Process?",
                text: "If Yes,Share Else cancel",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Share it!",
                closeOnConfirm: false
            }, function () {
                __doPostBack('ShareRaisedProblem', "");
            });
            return false;
        }

        function ShowActionStatePopUp() {
            $('#mpeActionStatePopUp').modal({
                backdrop: 'static'
            });
            return false;
        }

        function PrintCAPAReport() {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var Address = $('#ContentPlaceHolder1_hdnAddress').val();
            var PhoneAndFaxNo = $('#ContentPlaceHolder1_hdnPhoneAndFaxNo').val();
            var Email = $('#ContentPlaceHolder1_hdnEmail').val();
            var WebSite = $('#ContentPlaceHolder1_hdnWebsite').val();

            var DocNo = $('#ContentPlaceHolder1_hdnDocNo').val();
            var RevNo = $('#ContentPlaceHolder1_hdnRevNo').val();
            var RevDate = $('#ContentPlaceHolder1_hdnRevDate').val();

            var SheetMarkingAndCutting = $('#ContentPlaceHolder1_divSheetMarkingAndCuttingPDF').html();

            winprint.document.write("<html><head><title>");
            winprint.document.write("Offer");
            winprint.document.write("</title>");
            winprint.document.write("<link rel='stylesheet'  href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
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
            }, 1000);
        }

        function PrintCAPAReport(CAPAID) {
            __doPostBack('CAPAPrint', CAPAID);
            return false;
        }

    </script>

    <style type="text/css">
        div#ContentPlaceHolder1_divparem label {
            color: brown;
            font-size: 18px;
        }

        div#ContentPlaceHolder1_divparem span {
            color: #000;
            font-weight: bold;
            font-size: 18px;
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
                                    <h3 class="page-title-head d-inline-block">NC Final QC Approval Pending List </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Reports</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page"> CAPA Report </li>
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
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divInput" runat="server">
                            <div class="ip-div text-left">
                            </div>
                            <div class="col-sm-12 text-center p-t-10" runat="server">
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="col-sm-12">
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvCAPAReport" runat="server"
                                                CssClass="table table-bordered table-hover no-more-tables"
                                                ShowHeaderWhenEmpty="True" OnRowCommand="gvCAPAReport_OnRowCommand"
                                                EmptyDataText="No Records Found" AutoGenerateColumns="false" DataKeyNames="CAPAID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="RFPNo" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRFPNo" runat="server" Text='<%# Eval("RFPNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="NCR No" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNCRNo" runat="server" Text='<%# Eval("NCRNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="NCR Date" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNCRDate" runat="server" Text='<%# Eval("NCRDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="QC Status" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQCStatus" runat="server" Text='<%# Eval("QCStatus")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="ItemName" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Add" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnAddCAPAReport" runat="server" Text="Add"
                                                                CommandName="AddCAPA" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                                <img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Print" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnPrint" runat="server" Text="Add"
                                                                OnClientClick='<%# string.Format("return PrintCAPAReport({0});",Eval("CAPAID")) %>'>
                                                                <img src="../Assets/images/print.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnCAPAID" Value="0" runat="server" />
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

    <div class="modal" id="mpeActionStatePopUp">
        <div class="modal-dialog" style="height: 100%; max-width: 100% !important;">
            <div class="modal-content" style="max-height:fit-content";>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H2">
                                <div class="text-center">
                                    <asp:Label ID="lblHeadeing_h" Style="color: brown; font-weight: bold;"
                                        Text="" runat="server"></asp:Label>
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal">
                                x</button>
                        </div>
                        <div class="modal-body">
                            <div class="inner-container">
                                <div class="ip-div text-center" id="divactionstateprocess">

                                    <div id="divparem" class="divparem" runat="server">
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4 text-left">
                                                <label>Weld Length Meter </label>
                                                <asp:Label ID="lblweldlengthmeter_pp" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-4 text-left">
                                                <label>Inspection From Date </label>
                                                <asp:Label ID="lblinspectionfromdate_pp" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-4 text-left">
                                                <label>Inspection To Date </label>
                                                <asp:Label ID="lblinspectiontodate_pp" runat="server"> </asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4 text-left">
                                                <label>Repeated Rework Location </label>
                                                <asp:Label ID="lblrepeatedreworklocation_pp" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-4 text-left">
                                                <label>weld Defects Found in meter  </label>
                                                <asp:Label ID="lblwelddefectsfoundinmeter_pp" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-4 text-left">
                                                <label>Data From </label>
                                                <asp:Label ID="lbldatafrom_pp" runat="server"> </asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4 text-left">
                                                <label>Repeated Reworks </label>
                                                <asp:Label ID="lblrepeatedreworks_pp" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-4 text-left">
                                                <label>QC Requested On </label>
                                                <asp:Label ID="lblQCRequestOn_pp" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-4 text-left">
                                                <label>QC Request By </label>
                                                <asp:Label ID="lblQCRequestBy_pp" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4 text-left">
                                                <label>Production Done On </label>
                                                <asp:Label ID="lblproductionDoneOn_pp" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-4 text-left">
                                                <label>Production Done By </label>
                                                <asp:Label ID="lblproductionDoneBy_pp" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-4 text-left">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 text-center p-t-10">
                                        <label style="color: blue; font-size: 20px;">ACTION STATE PROCESS </label>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4">
                                        </div>
                                        <div class="col-sm-4">
                                            <label>SELECT  </label>
                                            <asp:DropDownList ID="ddlCAPAActionState" CssClass="form-control mandatoryfield" runat="server"></asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-6">
                                            <label>Description </label>
                                            <asp:TextBox ID="txtDescription" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control mandatoryfield"> </asp:TextBox>
                                        </div>
                                        <div class="col-sm-3">
                                            <label>Incharge Name </label>
                                            <asp:TextBox ID="txtinchargename" runat="server" CssClass="form-control mandatoryfield"> </asp:TextBox>
                                        </div>
                                        <div class="col-sm-3">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <asp:LinkButton ID="btnAction" runat="server" Text="Save" CssClass="btn btn-cons btn-success"
                                            OnClientClick="return Mandatorycheck('divactionstateprocess');"
                                            OnClick="btnAction_Click"></asp:LinkButton>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <asp:GridView ID="gvCAPADetails" runat="server"
                                            CssClass="table table-bordered table-hover no-more-tables"
                                            ShowHeaderWhenEmpty="True" OnRowCommand="gvCAPADetails_OnRowCommand" OnRowDataBound="gvCAPADetails_OnRowDataBound"
                                            EmptyDataText="No Records Found" AutoGenerateColumns="false" DataKeyNames="CAPADID">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="State Name" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblstateName" runat="server" Text='<%# Eval("StateName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldescription" runat="server" Text='<%# Eval("Description")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDate" runat="server" Text='<%# Eval("Date")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Incharge Name" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblinchargeName" runat="server" Text='<%# Eval("InChargeName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delete">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete" runat="server"
                                                            CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            CommandName="DeleteCAPADetails"><img src="../Assets/images/del-ec.png"/></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4">
                                             <label class="form-label">Attachment </label>
                                            <asp:FileUpload ID="fpProblemraisedAttach" runat="server" TabIndex="12" CssClass="form-control Attachement"
                                                onchange="DocValidation(this);"></asp:FileUpload>
                                        </div>
                                        <div class="col-sm-4">
                                            <label class="form-label"> Final QC Status </label>
                                              <asp:DropDownList ID="ddlQCStatus" runat="server" CssClass="form-control mandatoryfield">
                                                  <asp:ListItem Value="1" Text="Accepted" Selected="True"> </asp:ListItem>
                                                  <asp:ListItem Value="7" Text="Rework"> </asp:ListItem>
                                                  <asp:ListItem Value="9" Text="Rejected"> </asp:ListItem>
                                             </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <asp:LinkButton ID="btnShare" runat="server" Text="Save & Share" CssClass="btn btn-cons btn-success"
                                            OnClientClick='return ShareRaiseProblem();'></asp:LinkButton>
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

