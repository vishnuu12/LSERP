<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="InternalDC.aspx.cs" Inherits="Pages_InternalDC" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        @media print {
            span {
                padding: 4px;
                display: inline-block
            }

            .table-bordered th {
                background: #fff;
                border: 1px solid #ddd !important;
                color: #000
            }
        }
    </style>
    <script type="text/javascript">

        function ShowAddPopUp() {
            $('#mpeIssueMaterial').modal('show');
            return false;
        }

        function CloseIssueMaterialPopUP() {
            $('#mpeIssueMaterial').modal('hide');
            return false;
        }

        function ShowReceiverPoPup() {
            $('#mpeReceiver').modal('show');
            return false;
        }

        function HideReceiverPopUp() {
            $('#mpeReceiver').modal('hide');
            return false;
        }
        function btnDCClick() {
            if ($('#ContentPlaceHolder1_gvReceiverDC').find('input:checked').length > 0) {
                if (msg) {
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                ErrorMessage('Error', 'No Item Selected');
                return false;
            }
        }

        function ValidateReturnableQty() {
            var returnQty = $(event.target).val();
            var AvailableQty = $(event.target).closest('tr').find('.returnableqty').text();

            if (parseInt(returnQty) > parseInt(AvailableQty)) {
                ErrorMessage('Error', 'Retured Qty Should Not Greater Then Returned qty');
                $(event.target).val('');
                return false;
            }
            else
                return true;
        }

        function ValidateReceivedQty() {
            var returnQty = $(event.target).val();
            var AvailableQty = $(event.target).closest('tr').find('.availableqty').text();

            if (parseInt(returnQty) > parseInt(AvailableQty)) {
                ErrorMessage('Error', 'Received Qty Should Not Greater Then available qty');
                $(event.target).val('');
                return false;
            }
            else
                return true;
        }

        function ShowPrintPopUp() {
            $('#mpeInternalDC_P').modal('show');
            return false;
        }

        function deleteConfirm(ID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Part Name will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deleteIDCIssueDetails', ID);
            });
            return false;
        }

        function deleteInternalDC(ID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the DC Will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('DeleteInterNalDC', ID);
            });
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
                                    <h3 class="page-title-head d-inline-block">Internal DC</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Stores</a></li>
                                <li class="active breadcrumb-item" aria-current="page">Internal DC</li>
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
                        <div id="divAdd" runat="server">
                        </div>
                        <div id="divAddNew" runat="server">
                            <div class="ip-div text-center p-t-10">
                                <asp:LinkButton ID="btnAddNew" runat="server" Text="Add New" OnClick="btnAddNew_Click"
                                    CssClass="btn ic-w-btn add-btn"></asp:LinkButton>
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            DC Date</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:TextBox ID="txtDCNumber" runat="server" CssClass="form-control mandatoryfield datepicker"
                                            Width="70%" ToolTip="Enter DC Number" placeholder="Enter DC Number">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            From Unit</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlFromUnit" runat="server" ToolTip="Select Location Name"
                                            Width="70%" CssClass="form-control mandatoryfield">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            To Unit</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlToUnit" runat="server" ToolTip="Select Location Name" Width="70%"
                                            CssClass="form-control mandatoryfield">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Duration</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:TextBox ID="txtDuration" runat="server" CssClass="form-control mandatoryfield"
                                            Width="70%" onkeypress="return fnAllowNumeric();" ToolTip="Enter Remarks" placeholder="Enter Duration">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Received To</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlReceivedTo" runat="server" ToolTip="Select Employee Name"
                                            Width="70%" CssClass="form-control mandatoryfield">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Remarks</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control mandatoryfield"
                                            Width="70%" ToolTip="Enter Remarks" placeholder="Enter Remarks">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 text-center p-t-10">
                                    <asp:LinkButton ID="btnSaveIDC" Text="Save" CssClass="btn btn-cons btn-save AlignTop"
                                        OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divInput');" OnClick="btnInternalDC_Click"
                                        runat="server" />
                                    <asp:LinkButton ID="btnCancel" Text="Cancel" CssClass="btn btn-cons btn-danger AlignTop"
                                        OnClick="btnCancel_Click" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvInternalDC" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                OnRowCommand="gvInternalDC_OnRowCommand" OnRowDataBound="gvInternalDC_OnRowDataBound"
                                                DataKeyNames="IDCID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="DC No" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDCNo" runat="server" Text='<%# Eval("DCNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Created By" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcreatedBy" runat="server" Text='<%# Eval("CreatedBy")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="DC Date" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDCDate" runat="server" Text='<%# Eval("DCDate")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="From Unit" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFromUnit" runat="server" Text='<%# Eval("FromUnit")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="To Unit" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblToUnit" runat="server" Text='<%# Eval("ToUnit")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Duration" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDuration" runat="server" Text='<%# Eval("Duration")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Dispatch DC" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnAdd" runat="server" Text="Add DC" ToolTip="Add DC" CommandName="AddInternalDC"
                                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Receiver" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnreceiverTo" runat="server" Text="Add DC" ToolTip="Add DC"
                                                                CommandName="ReceivedTo" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"> <img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PDF" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnPrint" runat="server" Text="Add DC" CommandName="InternalDCPDF"
                                                                ToolTip="Print" OnClientClick='<%# string.Format("return PrintDC({0});",Eval("IDCID")) %>'> <img src="../Assets/images/pdf.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDelete" runat="server"
                                                                OnClientClick='<%# string.Format("return deleteInternalDC({0});",Eval("IDCID")) %>'>
                                                            <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnIDCID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnIDCMIDID" runat="server" Value="0" />
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
    <div class="modal" id="mpeIssueMaterial" style="overflow-y: scroll;">
        <div class="modal-dialog" style="width: 50%;">
            <div class="modal-content" style="width: 133%;">
                <asp:UpdatePanel ID="upView" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">
                                <asp:Label ID="lblDCNumber" runat="server" Style="color: brown;"></asp:Label></h4>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true"
                                onclick="CloseIssueMaterialPopUP();">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="docdiv" class="docdiv">
                                <div class="inner-container" style="text-transform: uppercase;">
                                    <div id="divDCIssuedDetails" runat="server" visible="true">
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <label class="mandatorylbl">
                                                    Item Description</label>
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtItemDescription" runat="server" Width="70%" CssClass="form-control mandatoryfield"
                                                    TextMode="MultiLine" Rows="3" ToolTip="Enter Po Ref No" placeholder="Enter Item Description">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-2">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4">
                                                <label class="mandatorylbl">
                                                    Issued Qty</label>
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtIssuedQty" runat="server" Width="70%" CssClass="form-control mandatoryfield"
                                                    onkeypress="return fnAllowNumeric();" ToolTip="Enter Po Ref No" placeholder="Enter Issued Qty">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-2">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10" id="divMaterialReturnableDate" runat="server">
                                            <div class="col-sm-4">
                                                <label>
                                                    Returnable Qty</label>
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:TextBox ID="txtreturnableQty" runat="server" Width="70%" CssClass="form-control"
                                                    onkeypress="return fnAllowNumeric();" placeholder="Enter Returnable Qty">
                                                </asp:TextBox>
                                            </div>
                                            <div class="col-sm-2">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 text-center p-t-10">
                                            <asp:Button ID="btnMaterialIssue" Text="Save Material Issue" CssClass="btn btn-cons btn-success"
                                                OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divDCIssuedDetails');"
                                                OnClick="btnMaterialIssue_Click" runat="server" />
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvInternalDCMaterialIssueDetails" runat="server" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" OnRowEditing="gvInternalDCMaterialIssueDetails_RowEditing"
                                                OnRowCancelingEdit="gvInternalDCMaterialIssueDetails_RowCancelingEdit" OnRowDataBound="gvInternalDCMaterialIssueDetails_RowDataBound"
                                                OnRowUpdating="gvInternalDCMaterialIssueDetails_RowUpdating"
                                                CssClass="table table-hover table-bordered medium"
                                                DataKeyNames="IDCMIDID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Description" ItemStyle-HorizontalAlign="left"
                                                        HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemDescription" runat="server" Text='<%# Eval("ItemDescription")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Issued Qty" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIssuedQty" runat="server" Text='<%# Eval("IssuedQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Returnable Qty" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReturnableQty" runat="server" Text='<%# Eval("ReturnableQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Returned Qty" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReturnedQty" Text='<%# Eval("ReturnedQty")%>' runat="server">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtReturnedQty" CssClass="form-control returnqty" onkeyup="return ValidateReturnableQty(this);"
                                                                onkeypress="return fnAllowNumeric();" runat="server">
                                                            </asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Balance Return Qty" ItemStyle-HorizontalAlign="left"
                                                        HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBalanceReturnQty" CssClass="returnableqty" runat="server" Text='<%# Eval("BalanceQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField ButtonType="Image" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"
                                                        ShowEditButton="true" EditText="<img src='~/images/icon_edit.png' title='Edit' />"
                                                        EditImageUrl="../Assets/images/edit-ec.png" CancelImageUrl="../Assets/images/icon_cancel.png"
                                                        UpdateImageUrl="../Assets/images/icon_update.png" ItemStyle-Wrap="false" ControlStyle-Width="20px"
                                                        ControlStyle-Height="20px" HeaderText="Edit" ValidationGroup="edit" HeaderStyle-Width="7%">
                                                        <ControlStyle CssClass="UsersGridViewButton" />
                                                    </asp:CommandField>

                                                    <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDelete" runat="server"
                                                                OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("IDCMIDID")) %>'>
                                                            <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="col-sm-12 text-center p-t-10">
                                            <asp:Button ID="btnShare" Text="Share DC" CssClass="btn btn-cons btn-success" OnClick="btnShareDC_Clickk"
                                                runat="server" />
                                        </div>
                                    </div>
                                    <div id="divMaterialReturn" runat="server" visible="false">
                                        <div class="col-sm-12 text-center p-t-10">
                                            <%--    <asp:Button ID="btnMaterialReturn" Text="PDF Inter" CssClass="btn btn-cons btn-success"
                                                OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divMaterialReturn');"
                                                OnClick="btnMaterialReturn_Click" runat="server" />--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal" id="mpeReceiver" style="overflow-y: scroll;">
        <div class="modal-dialog" style="width: 50%;">
            <div class="modal-content" style="width: 133%;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">
                                <asp:Label ID="lblReceiverDCNumber" runat="server" Style="color: brown;"></asp:Label></h4>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true"
                                onclick="HideReceiverPopUp();">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="Div2" class="docdiv">
                                <div class="inner-container" style="text-transform: uppercase;">
                                    <div id="div3" runat="server" visible="true">
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvReceiverDC" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                DataKeyNames="IDCMIDID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" ControlStyle-CssClass="nosort" HeaderStyle-Width="5%"
                                                        ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderTemplate>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkitems" runat="server" AutoPostBack="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Description" ItemStyle-HorizontalAlign="left"
                                                        HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemDescription" runat="server" Text='<%# Eval("ItemDescription")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Issued Qty" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIssuedQty" runat="server" Text='<%# Eval("IssuedQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Returnable Qty" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReturnableQty" runat="server" Text='<%# Eval("ReturnableQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Received Qty" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtReceivedQty" CssClass="form-control" onkeyup="return ValidateReceivedQty(this);"
                                                                onkeypress="return fnAllowNumeric();" runat="server" Text='<%# Eval("ReceivedQty")%>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Balance Qty" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtBalanceQty" runat="server" CssClass="availableqty" Text='<%# Eval("BalacedQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRemarks" CssClass="form-control" runat="server"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="col-sm-12 text-center p-t-10">
                                            <asp:Button ID="btnReceived" Text="Save" CssClass="btn btn-cons btn-success" OnClick="btnReceived_Click"
                                                OnClientClick="return btnDCClick();" runat="server" />
                                        </div>
                                    </div>
                                    <div id="div5" runat="server" visible="false">
                                        <div class="col-sm-12 text-center p-t-10">
                                            <%--    <asp:Button ID="btnMaterialReturn" Text="PDF Inter" CssClass="btn btn-cons btn-success"
                                                OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divMaterialReturn');"
                                                OnClick="btnMaterialReturn_Click" runat="server" />--%>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal" id="mpeInternalDC_P" style="overflow-y: auto;">
        <div class="modal-dialog" style="width: 50%;">
            <div class="modal-content" style="width: 100%;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btndownloaddocs" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">
                                <asp:Label ID="Label1" runat="server" Style="color: brown;"></asp:Label></h4>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true"
                                onclick="HideReceiverPopUp();">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="Div4" class="docdiv">
                                <div class="inner-container" style="text-transform: uppercase;">
                                    <div class="col-sm-12 text-center">
                                        <asp:LinkButton ID="btndownloaddocs" OnClick="btndownloaddocs_Click" ToolTip="PDF DownLoad"
                                            runat="server">
                                                        <img src="../Assets/images/pdf.png" /> </asp:LinkButton>
                                    </div>
                                    <div id="divInternalDC_p" runat="server" style="border: 2px solid #000; float: left; width: 200mm; float: left; margin-left: 1mm">
                                        <div class="col-sm-12 text-center">
                                            <label style="font-size: 20px ! important; margin-top: 30px; font-weight: 700; display: block;">
                                                <asp:Label ID="lblCompanyName_p" Style="font-size: 25px ! important" runat="server"></asp:Label>
                                            </label>
                                            <label>
                                                <asp:Label ID="lblFormarlyCompanyName_p" runat="server"> </asp:Label>
                                            </label>
                                        </div>
                                        <div class="col-sm-12 text-center">
                                            <asp:Label ID="lblFromUnitAddress_p" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-12 text-center" style="font-size: 20px; font-weight: 700 !important; color: #000">
                                            BELLOWS DIVISION DELIVERY CHALLAN
                                        </div>
                                        <div class="col-sm-12" style="border: 1px solid #000; padding: 5px">
                                            <div class="col-sm-4" style="border: 1px solid #000;">
                                                <label style="font-weight: 700">
                                                    DC No</label>
                                                <asp:Label ID="lblDCNo_p" runat="server"></asp:Label>
                                            </div>
                                            <div class="col-sm-8" style="border: 1px solid #000;">
                                                <label style="font-weight: 700">
                                                    DC Date</label>
                                                <asp:Label ID="lblDCDate_p" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-12" style="border: 1px solid #000; padding: 5px">
                                            <div class="col-sm-4" style="border: 1px solid #000;">
                                                <label style="font-weight: 700">
                                                    To Unit Address</label>
                                            </div>
                                            <div class="col-sm-8" style="border: 1px solid #000;">
                                                <asp:Label ID="lblToUnitAddress_p" runat="server" Style="font-weight: 700"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-12" style="border: 1px solid #000; padding: 5px">
                                            <div class="col-sm-4" style="border: 1px solid #000;">
                                                <label style="font-weight: 700">
                                                    Expected Duration</label>
                                            </div>
                                            <div class="col-sm-8" style="border: 1px solid #000;">
                                                <asp:Label ID="lblExpectedDuration_p" runat="server" Style="font-weight: 700"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-12" style="border: 1px solid #000; padding: 5px">
                                            <div class="col-sm-4" style="border: 1px solid #000;">
                                                <label style="font-weight: 700">
                                                    Reason / Remarks</label>
                                            </div>
                                            <div class="col-sm-8" style="border: 1px solid #000;">
                                                <asp:Label ID="lblReasonRemarks_p" runat="server" Style="font-weight: 700"></asp:Label>
                                            </div>
                                        </div>
                                        <div>
                                            <div class="col-sm-12 p-t-10" style="overflow-x: auto; padding-left: 5px !important; padding-right: 5px !important;">
                                                <asp:GridView ID="gvItemDescriptionDetails_p" runat="server" AutoGenerateColumns="False"
                                                    ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium" Style="border: 1px solid #000 !important;">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Description" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblItemDescription" runat="server" Text='<%# Eval("ItemDescription")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("IssuedQty")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 text-left" style="border: 1px solid; border-width: 1px 0px 1px 0px">
                                            <label>
                                                Not For Sale</label>
                                        </div>
                                        <div class="col-sm-12 text-left" style="padding: 20px">
                                            <div class="col-sm-6" style="font-weight: 700; color: #000">
                                                <div class="col-sm-4">
                                                    <label style="font-size: 12px">Received By</label>
                                                </div>
                                                <div class="col-sm-2">: </div>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblReceivedBy_p" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 text-right" style="font-weight: 700; color: #000">
                                                <label>
                                                    <asp:Label ID="lblfootercompanyname_p" runat="server">  </asp:Label></label>

                                                <asp:Label ID="lblSignature_p" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10 text-right">
                                            <asp:Image ID="imgQrcode" class="Qrcode" ImageUrl="" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
