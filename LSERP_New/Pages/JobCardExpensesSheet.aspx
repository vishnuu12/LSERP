<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master"
    AutoEventWireup="true" CodeFile="JobCardExpensesSheet.aspx.cs" Inherits="Pages_JobCardExpensesSheet"
    ClientIDMode="Predictable" ValidateRequest="false" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function deleteConfirm(CJPCDID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Job Card Rate will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deletegvrow', CJPCDID);
            });
            return false;
        }

        function PrintJobCard(index) {
            __doPostBack('printJobcard', index);
        }

        function PrintHtmlFile() {
            var cotent = $('#ContentPlaceHolder1_hdnpdfContent').val();
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');
            winprint.document.write(cotent);
            setTimeout(function () {
                winprint.document.close();
                winprint.focus();
                winprint.print();
                // winprint.close();
            }, 1000);
            return false;
        }

        function PrintAssemplyPlanningPDF(QrCode, Address, PhoneAndFaxNo, Email, WebSite) {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var APInnerHtml = $('#ContentPlaceHolder1_divSubAssemplyWeldingPDF').html();

            winprint.document.write("<html><head><title>");
            winprint.document.write("");
            winprint.document.write("</title>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='../Assets/css/ep-style.css'/>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='../Assets/css/main.css'/>");

            winprint.document.write("<link rel='stylesheet'  href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/print.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");

            winprint.document.write("<style type='text/css'> label { font-weight: bold ! important;color: #000 ! important; }   .col-sm-12.contractorborder { padding-top:5px; border-left: 2px solid;border-right: 2px solid; border-bottom: 2px solid;} .sideheading{ display:flex;align-items:flex-start;justify-content:center;margin-top:15px; } .lbl_h{ width:35%; } .lbl_v{ width:65%; } .Qrcode {height: 60px;width: 80px;margin-right: 10px;padding-left: 20px;} </style>");
            winprint.document.write("</head><body>");

            winprint.document.write("<div class='print-page'>");
            winprint.document.write("<table><thead><tr><td>");
            winprint.document.write("<div class='col-sm-12 header-space' style='text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;height:120px'>");
            winprint.document.write("<div class='header' style='border-bottom:1px solid;'>");
            winprint.document.write("<div>");
            winprint.document.write("<div class='col-sm-12 padding0' id='divLSERPLogo' style='margin:0px auto;display:block;padding:5px 0px;'>");

            winprint.document.write("<div class='row'>");
            winprint.document.write("<div class='col-sm-3'>");
            winprint.document.write("<img src='../Assets/images/lonestar.jpeg' alt='lonestar-image' width='90px'>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-6 text-center'>");
            winprint.document.write("<h3 style='font-weight:600;font-size:24px;font-style:italic;color:#000;font-family: Arial;display: contents;'>LONESTAR </h3>");
            winprint.document.write("<span style='font-weight:600;font-size:24px ! important;font-family: Times New Roman;'>INDUSTRIES</span>");
            winprint.document.write("<p style='font-weight:500;color:#000;width: 103%;'>" + Address + "</p>");
            winprint.document.write("<p style='font-weight:500;color:#000'>" + PhoneAndFaxNo + "</p>");
            winprint.document.write("<p style='font-weight:500;color:#000'>" + Email + "</p>");
            winprint.document.write("<p style='font-weight:500;color:#000'>" + WebSite + "</p>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-3'>");
            winprint.document.write("<img src='../Assets/images/iso.jpeg' alt='lonestar-image' height='90px'>");
            winprint.document.write("</div></div>");
            winprint.document.write("</div></div></div>");
            winprint.document.write("</td></tr></thead>");
            winprint.document.write("<tbody><tr><td>");
            winprint.document.write("<div  style='padding-top:0px;margin:2mm;'>");
            winprint.document.write(APInnerHtml);
            winprint.document.write("</div>");
            winprint.document.write("</td></tr></tbody>");
            winprint.document.write("<tfoot class='footer-space'><tr><td>");

            winprint.document.write("<div style='margin:2mm;'>");
            winprint.document.write("<div class='col-sm-12' style='padding-top:50px;'>");
            winprint.document.write("<div class='col-sm-6 p-t-20'><label style='color:black; font-weight:bolder;float:left;'>Quality Incharge</label></div>");
            winprint.document.write("<div class='col-sm-6' style='padding-left:16%;'><label style='color:black; font-weight:bolder;'> Production Incharge</label><img src='" + QrCode + "' class='Qrcode' /></div>");
            winprint.document.write("</div></div>");
            winprint.document.write("</td></tr></tfoot></table>");
            winprint.document.write("</div>");

            winprint.document.write("</body></html>");

            setTimeout(function () {
                winprint.document.close();
                winprint.focus();
                winprint.print();
                // winprint.close();
            }, 1000);
        }

        function PrintMarkinAndCutting(QRCode, Mode) {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var Address = $('#ContentPlaceHolder1_hdnAddress').val();
            var PhoneAndFaxNo = $('#ContentPlaceHolder1_hdnPhoneAndFaxNo').val();
            var Email = $('#ContentPlaceHolder1_hdnEmail').val();
            var WebSite = $('#ContentPlaceHolder1_hdnWebsite').val();

            var FabricationWelding = $('#ContentPlaceHolder1_divFabricationAndWeldingPDF').html();
            var MarkingAndCutting = $('#ContentPlaceHolder1_divMarkingAndCuttingPDF').html();

            //var SheetMarkingAndCutting = $('#ContentPlaceHolder1_divSheetMarkingAndCuttingPDF').html();
            //var SheetWelding = $('#ContentPlaceHolder1_divSheetWeldingPDF').html();
            //var BellowFormingTangentCutting = $('#ContentPlaceHolder1_divBellowFormingAndTangentCuttingPDF').html();

            winprint.document.write("<html><head><title>");
            winprint.document.write("Offer");
            winprint.document.write("</title>");
            // winprint.document.write("<link rel='stylesheet' type='text/css' href='" + epstyleurl + "'/>");
            // winprint.document.write("<link rel='stylesheet' href='" + Main + "' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet'  href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/printg.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            //   winprint.document.write("<link rel='stylesheet' href='" + topstrip + "' type='text/css'/>");           
            winprint.document.write("<style type='text/css'> label { font-weight: bold ! important;color: #000 ! important; } .contractorborder label{padding: 4px 6px; } .contractorborder { padding-top:5px; border: 1px solid;} .sideheading{ display:flex;align-items:flex-start;justify-content:center;margin-top:15px; } .lbl_h{ width:35%; } .lbl_v{ width:65%; } .Qrcode {height: 60px;width: 80px;margin-right: 10px;padding-left: 20px;} </style>");

            winprint.document.write("<div class='print-page' style='position:fixed;border:1px solid #000;width:200mm;height:287mm;margin-top:5mm;margin-bottom:5mm;margin-left:5mm;margin-right:5mm'></div>");
            winprint.document.write("<table style='width:200mm;height:287mm;margin-left:20px;border:0px !important'><thead><tr><td>");
            winprint.document.write("<div class='col-sm-12' style='text-align:center;font-size:20px;font-weight:bold;padding-left:0;padding-right:0;height:112px'>");
            winprint.document.write("<div>");
            winprint.document.write("<div>");
            winprint.document.write("<div class='col-sm-12 padding0' id='divLSERPLogo' style='margin:20px auto;display:block;'>");
            //  winprint.document.write("<img src='" + topstrip + "' alt='' height='100px;' style='object-fit:contain;width:100%;display:none'>");
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

            if (Mode == "MC")
                winprint.document.write(MarkingAndCutting);
            else if (Mode == "FW")
                winprint.document.write(FabricationWelding);

            winprint.document.write("</div>");

            winprint.document.write("</td></tr></tbody>");
            winprint.document.write("<tfoot><tr><td>");
            winprint.document.write("<div style='height:30mm;'>");
            winprint.document.write("<div class='row' style='margin-bottom:20px;width:200mm;bottom:0px;position:fixed;'>");

            winprint.document.write("<div class='col-sm-3 text-center'>");
            winprint.document.write("<label style='color:black; font-weight:bolder;'>Quality Incharge</label>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-6 text-center'>");
            winprint.document.write("<label style='width:25%;color:black;float:left; font-weight:bolder;border: 1px solid black;'>Doc No : 12</label>");
            winprint.document.write("<label style='width:25%;color:black;float:left; font-weight:bolder;border: 1px solid black;'>Rev No : 11</label>");
            winprint.document.write("<label style='width:50%;color:black;float:left; font-weight:bolder;border: 1px solid black;'>Rev Date : 19.1.2021</label>");
            winprint.document.write("</div>");
            winprint.document.write("<div class='col-sm-3 text-center'>");
            winprint.document.write("<label style='color:black; font-weight:bolder;'> Production Incharge</label><img src='" + QRCode + "' class='Qrcode'/>");
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
                                    <h3 class="page-title-head d-inline-block">Job Card Expenses Sheet  </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                    <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Production</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Job Card Expenses Sheet</li>
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
            <asp:UpdatePanel ID="upPartMaster" runat="server">
                <Triggers>
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            RFP No</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlRFPNo" runat="server" AutoPostBack="true" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlRFPNo_SelectIndexChanged" Width="70%" ToolTip="Select RFP No">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Job Card No 
                                        </label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlJobCardNo" runat="server" AutoPostBack="true" CssClass="form-control"
                                            OnSelectedIndexChanged="ddlJobCardNo_OnSelectIndexChanged" Width="70%" ToolTip="Select Job Card Number">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>

                            </div>
                        </div>

                        <div id="divAddNew" runat="server">
                            <div class="col-sm-12 p-t-10 text-center">
                                <asp:LinkButton ID="btnAddNew" runat="server" Text="Add New" OnClick="btnAddNew_OnClick"
                                    CssClass="btn ic-w-btn add-btn"></asp:LinkButton>
                            </div>
                        </div>

                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">

                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            RFP No  
                                        </label>
                                    </div>
                                    <div class="col-sm-4 text-left">
                                        <asp:Label ID="lblRFPNo" Style="color: brown;" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Job Card No
                                        </label>
                                    </div>
                                    <div class="col-sm-4 text-left">
                                        <asp:Label ID="lbljobcardNo" Style="color: brown;" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Contractor Team Name</label>
                                    </div>
                                    <div class="col-sm-4 text-left">
                                        <asp:DropDownList ID="ddlContractorTeamMemberName_AP" runat="server" CssClass="form-control mandatoryfield" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlContractorTeamMemberName_J_OnSelectedIndexChanged" ToolTip="Select Contractor Team Member Name">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Job Process Name
                                        </label>
                                    </div>
                                    <div class="col-sm-4 text-left">
                                        <asp:DropDownList ID="ddlJobProcessName" runat="server" CssClass="form-control mandatoryfield" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlJobProcessName_OnSelectedIndexChanged" ToolTip="Select Contractor Team Member Name">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            <asp:Label ID="lbluom" Style="color: chocolate; font-size: large;"
                                                runat="server"></asp:Label>
                                        </label>
                                    </div>
                                    <div class="col-sm-4 text-left">
                                        <asp:TextBox ID="txtqty" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Unit Rate 
                                        </label>
                                    </div>
                                    <div class="col-sm-4 text-left">
                                        <asp:Label ID="lblrate" onkeypress="return validationDecimal(this);" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Remarks
                                        </label>
                                    </div>
                                    <div class="col-sm-4 text-left">
                                        <asp:TextBox ID="txtRemarks" runat="server" Rows="3" TextMode="MultiLine" CssClass="form-control mandatoryfield"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10">
                                    <asp:LinkButton ID="btnSaveExpenses" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divInput');" OnClick="btnSaveExpenses_Click" runat="server" Text="Save" CssClass="btn btn-cons btn-save AlignTop"></asp:LinkButton>
                                    <asp:LinkButton ID="btnCancel" OnClick="btnCancel_Click" runat="server" Text="Cancel" CssClass="btn btn-cons btn-danger AlignTop"></asp:LinkButton>
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
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>
                                        <%--[ContractorTeamName],t9.JobName,--%>
                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvJobCardNoProcessCostDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                OnRowCommand="gvJobCardNoProcessCostDetails_OnRowCommand" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                HeaderStyle-HorizontalAlign="Center" DataKeyNames="CJPCDID,ProcessName">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Contractor Team" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblContractorTeamName" runat="server" Text='<%# Eval("ContractorTeamName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="JobName" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbljobname" runat="server" Text='<%# Eval("JobName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Qty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="UOM" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbluom" runat="server" Text='<%# Eval("UOM")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit Cost" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUnitCost" runat="server" Text='<%# Eval("UnitCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Total Cost" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltotalcost" runat="server" Text='<%# Eval("TotalCost")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remarks" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEdit" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="EditJobCardRate">
                                                           <img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDelete" runat="server"
                                                                OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("CJPCDID")) %>'>
                                                            <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Print" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnPrint" runat="server" Text="Add DC" CommandName="PrintJobCard" ToolTip="Print"
                                                                OnClientClick='<%# string.Format("return PrintJobCard({0});",((GridViewRow) Container).RowIndex) %>'>
                                                                <img src="../Assets/images/pdf.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnCJPCDID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnJCHID" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdnpdfContent" runat="server" Value="0" />

                                            <asp:HiddenField ID="hdnAddress" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnPhoneAndFaxNo" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnEmail" runat="server" Value="" />
                                            <asp:HiddenField ID="hdnWebsite" runat="server" Value="" />

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

    <div id="divMarkingAndCuttingPDF" runat="server" style="display: none;">
        <div id="div16" class="FrontPagepopupcontent" runat="server">
            <div class="text-center" style="background-color: #b0acac; padding-top: 10px; padding-bottom: 10px;">
                <asp:Label Style="color: Black; font-weight: bolder; font-size: large ! important;" ID="lblProcessNameHeader_MC_P"
                    runat="server" Text="">
                </asp:Label>
            </div>
            <div style="margin-top: 10px;">
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Job Order ID</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblJobOrderID_MC_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Date
                        </label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblDate_MC_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            RFP No</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblRFPNo_MC_P" runat="server"></asp:Label>
                    </div>
                </div>
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
                            Job Order Remarks</label>
                        <asp:Label ID="lblJobOrderRemarks_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Total Cost</label>
                        <asp:Label ID="lblTotalCost_MC_P" runat="server" CssClass="lbl_v"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Dead Line Date
                        </label>
                        <asp:Label ID="lblDeadlineDate_MC_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="row p-t-10">
                        <label style="color: Black;">
                            Issue Details</label>
                    </div>
                    <div class="row p-t-10">
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
                            <%--  <div class="col-sm-12">
                            <div class="col-sm-6">
                                <label>fabricationName</label>
                            </div>
                            <div class="col-sm-1">
                                <label>:</label>
                            </div>
                            <div class="col-sm-5">
                                <label>fabricationValue</label>
                            </div>
                        </div>--%>
                        </div>

                        <div style="padding-left: 2px;">
                            <label>
                                Remarks
                            </label>
                            <asp:Label ID="lblOverallremarks_MC_P" runat="server"></asp:Label>
                        </div>
                        <div class="col-sm-12 p-t-5">
                            <label>Status </label>
                            <asp:Label ID="lbljobcardStatus_MC_P" runat="server"></asp:Label>
                        </div>
                    </div>
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
        </div>
    </div>

    <div id="divFabricationAndWeldingPDF" runat="server" style="display: none;">
        <div id="d1" class="FrontPagepopupcontent" runat="server">
            <div class="col-sm-12 text-center" style="background-color: #b0acac; padding-top: 10px; padding-bottom: 10px;">
                <asp:Label Style="color: Black; font-weight: bolder; font-size: large ! important;" ID="lblProcessName_FW_P_H"
                    runat="server" Text="">
                </asp:Label>
            </div>
            <div>
                <div class="row p-t-10" style="border: 2px solid; margin-top: 10px;">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Job Order ID</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblJobOrderID_FW_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Date
                        </label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblDate_FW_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            RFP No</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblRFPNo_FW_P" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row contractorborder">
                    <div class="col-sm-5 text-left">
                        <label style="color: black;">
                            Contractor Name:</label>
                    </div>
                    <div class="col-sm-1">
                        <label style="float: right;">:</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblContractorName_FW_P" runat="server"></asp:Label>
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
                        <asp:Label ID="lblContractorTeamname_FW_P" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6" style="overflow-wrap: break-word;">
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Item Name/Size</label>
                        <asp:Label ID="lblItemNameSize_FW_P" runat="server" CssClass="lbl_v"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Drawing Name</label>
                        <asp:Label ID="lblDrawingName_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Part Name</label>
                        <asp:Label ID="lblPartname_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Part Qty</label>
                        <asp:Label ID="lblPartQty_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Process Name</label>
                        <asp:Label ID="lblProcessname_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Material Category</label>

                        <asp:Label ID="lblMaterialCategory_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>

                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Material Grade</label>

                        <asp:Label ID="lblMaterialGrade_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Thickness</label>

                        <asp:Label ID="lblThickness_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>

                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            NOP</label>

                        <asp:Label ID="lblNOP_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>

                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            MRN Number</label>

                        <asp:Label ID="lblMRNNumber_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>

                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Job Order Remarks</label>

                        <asp:Label ID="lblJobOrderRemarks_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Total Cost</label>
                        <asp:Label ID="lblTotalCost_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                    <div class="sideheading">
                        <label style="color: black;" class="lbl_h">
                            Dead Line Date
                        </label>
                        <asp:Label ID="lblDeadLineDate_FW_P" CssClass="lbl_v" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="row p-t-10">
                        <label>Weld Joint Sketch </label>
                        <div style="min-height: 100px; width: 100%; border: 1px solid;">
                        </div>
                    </div>
                    <div class="col-sm-12 p-t-10">
                        <label style="color: Black;">
                            Part Seriel Number</label>
                    </div>
                    <div class="col-sm-12">
                        <asp:GridView ID="gvPartSno_FW_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
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
                    <div class="col-sm-12 p-t-10">
                        <label style="color: Black;">
                            Offer QC test</label>
                    </div>
                    <div class="col-sm-12 text-left">
                        <asp:Label ID="lblOfferQCTest_FW_P" runat="server"></asp:Label>
                    </div>

                    <div class="col-sm-12 p-t-10">
                        <asp:Label ID="lblFabricationType_FW_P" Style="font-weight: bold;" runat="server"></asp:Label>
                    </div>
                    <div id="divfabrication_FW_P" runat="server">
                    </div>
                    <div style="padding-left: 2px;">
                        <label>
                            Remarks
                        </label>
                        <asp:Label ID="lblRemarks_FW_P" runat="server"></asp:Label>
                    </div>
                    <div class="col-sm-12 p-t-5">
                        <label>Status </label>
                        <asp:Label ID="lbljobcardStatus_FW_P" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="col-sm-12 p-t-10">
                <label style="color: Black;">
                    WPS Details</label>
            </div>
            <div class="col-sm-12 p-t-10">
                <asp:GridView ID="gvWPSDetails_FW_P" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="True"
                    EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                    <Columns>
                        <asp:TemplateField HeaderText="WPS Number" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblMRNNumber" runat="server" Text='<%# Eval("WPSNumber")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Material Grade" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblIssuedWeight" runat="server" Text='<%# Eval("MaterialGrade")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="THK" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblLength" runat="server" Text='<%# Eval("Thickness")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Process" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblWidth" runat="server" Text='<%# Eval("Process")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Filler Grade" ItemStyle-HorizontalAlign="left"
                            HeaderStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblMatRtn" runat="server" Text='<%# Eval("FillerGrade")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amps" ItemStyle-HorizontalAlign="left"
                            HeaderStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblMatRtn" runat="server" Text='<%# Eval("Amps")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Polarity" ItemStyle-HorizontalAlign="left"
                            HeaderStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblMatRtn" runat="server" Text='<%# Eval("Polarity")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Gas Level" ItemStyle-HorizontalAlign="left"
                            HeaderStyle-HorizontalAlign="left">
                            <ItemTemplate>
                                <asp:Label ID="lblMatRtn" runat="server" Text='<%# Eval("Gaslevel")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <div class="text-center">
                <label>BEFORE WELDING </label>
            </div>
            <div class="col-sm-12 p-t-10">
                <asp:GridView ID="gvBeforeWelding_FW_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
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

            <div class="text-center p-t-10">
                <label>DURING WELDING </label>
            </div>
            <div class="col-sm-12 p-t-10">
                <asp:GridView ID="gvDuringWelding_FW_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
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

            <div class="text-center p-t-10">
                <label>FINAL WELDING </label>
            </div>
            <div class="col-sm-12 p-t-10">
                <asp:GridView ID="gvFinalWelding_FW_P" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
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
        </div>
    </div>

    <div id="divSubAssemplyWeldingPDF" style="display: none;" runat="server">
        <div class="col-sm-12 text-center" style="background-color: #b0acac; height: 20px;">
            <asp:Label Style="color: Black; font-weight: bolder; font-size: 15px ! important;" ID="lblProcessName_AP_PDFHeader"
                runat="server" Text="">
            </asp:Label>
        </div>
        <div style="border-style: solid;">
            <div class="col-sm-12 p-t-10">
                <div class="col-sm-6 text-left">
                    <label style="color: black;">
                        Job Order ID</label>
                </div>
                <div class="col-sm-6">
                    <asp:Label ID="lblJobOrderID_AP_PDF" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-sm-12 p-t-10">
                <div class="col-sm-6 text-left">
                    <label style="color: black;">
                        Date
                    </label>
                </div>
                <div class="col-sm-6">
                    <asp:Label ID="lblDate_AP_PDF" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-sm-12 p-t-10">
                <div class="col-sm-6 text-left">
                    <label style="color: black;">
                        RFP No</label>
                </div>
                <div class="col-sm-6">
                    <asp:Label ID="lblRFPNo_AP_PDF" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-sm-12 p-t-10">
                <div class="col-sm-6 text-left">
                    <label style="color: black;">
                        Contractor Name:</label>
                </div>
                <div class="col-sm-6">
                    <asp:Label ID="lblContractorName_AP_PDF" runat="server"></asp:Label>
                </div>
            </div>
            <div class="col-sm-12 p-t-10">
                <div class="col-sm-6 text-left">
                    <label style="color: black;">
                        Contractor Team Member Name:</label>
                </div>
                <div class="col-sm-6">
                    <asp:Label ID="lblContractorTeamMemberName_AP_PDF" runat="server"></asp:Label>
                </div>
            </div>
        </div>
        <div class="col-sm-12">
            <div class="col-sm-6" style="overflow-wrap: break-word;">
                <div class="col-sm-12 p-t-10">
                    <div class="col-sm-6 text-left">
                        <label style="color: black;">
                            Item Name/Size</label>
                    </div>
                    <div class="col-sm-6 text-left">
                        <asp:Label ID="lblItemNameSize_AP_PDF" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-12 p-t-10">
                    <div class="col-sm-6 text-left">
                        <label style="color: black;">
                            Drawing No</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblDrawingName_AP_PDF" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-12 p-t-10">
                    <div class="col-sm-6 text-left">
                        <label style="color: black;">
                            Part Name</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblPartName_AP_PDF" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-12 p-t-10">
                    <div class="col-sm-6 text-left">
                        <label style="color: black;">
                            Item Qty</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblItemQty_AP_PDF" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-12 p-t-10">
                    <div class="col-sm-6 text-left">
                        <label style="color: black;">
                            Process Name</label>
                    </div>
                    <div class="col-sm-5">
                        <asp:Label ID="lblProcessName_AP_PDF" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-12 p-t-10">
                    <div class="col-sm-6 text-left">
                        <label style="color: black;">
                            Weld Plan Remarks</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblWeldPlanRemarks_AP_PDF" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-12 p-t-10">
                    <div class="col-sm-6 text-left">
                        <label style="color: black;">
                            Job Order Remarks</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblJobOrderRemarks_AP_PDF" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="col-sm-12 p-t-10">
                    <div class="col-sm-6 text-left">
                        <label style="color: black;">
                            Total Cost</label>
                    </div>
                    <div class="col-sm-6">
                        <asp:Label ID="lblTotalCost_AP_PDF" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="col-sm-6">
                <div class="col-sm-12 p-t-10">
                    <label style="color: Black;">
                        Process Seriel Number Details</label>
                </div>
                <div class="col-sm-12 p-t-10">
                    <asp:GridView ID="gvPartSerielNumber_AP_PDF" runat="server" AutoGenerateColumns="False"
                        ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item SNO" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblItemSNO" runat="server" Text='<%# Eval("ItemSno")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Part to Part" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label ID="lblPartSNO" runat="server" Text='<%# Eval("Pair")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="col-sm-12">
                    <label style="color: Black;">
                        Offer QC test</label>
                    <div class="col-sm-12 text-left">
                        <asp:Label ID="lblOfferQCtest_AP_PDF" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div id="divWPSDetails_AP" runat="server">
            <div class="col-sm-12 text-center p-t-10">
                <label style="color: Black;">
                    WPS DETAILS</label>
            </div>
            <div class="col-sm-12 p-t-10 text-left">
                <asp:GridView ID="gvWPSDetails_AP_PDF" runat="server" AutoGenerateColumns="False"
                    ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                    <Columns>
                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                            HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Part To Part" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblPartToPart" runat="server" Text='<%# Eval("PartToPart")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="WPS Number" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblWPSNumber" runat="server" Text='<%# Eval("WPSNumber")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Material Grade" HeaderStyle-CssClass="text-center"
                            ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblMaterialGrade" runat="server" Text='<%# Eval("MaterialGrade")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Thickness" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblThickness" runat="server" Text='<%# Eval("Thickness")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Process" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblProcess" runat="server" Text='<%# Eval("Process")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Filler Grade" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblFillerGrade" runat="server" Text='<%# Eval("FillerGrade")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amps" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblAmps" runat="server" Text='<%# Eval("Amps")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Polarity" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblPolarity" runat="server" Text='<%# Eval("Polarity")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="GasLevel" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblGasLevel" runat="server" Text='<%# Eval("GasLevel")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Part Assemply Amt" HeaderStyle-CssClass="text-center"
                            ItemStyle-CssClass="text-center">
                            <ItemTemplate>
                                <asp:Label ID="lblPartAssemplyAmt" runat="server" Text='<%# Eval("PartAssemplyAmount")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="col-sm-12 text-center p-t-10">
            <label style="color: Black;">
                BOUGHT OUT ITEM ISSUE DETAILS
            </label>
        </div>
        <div class="col-sm-12 p-t-10 text-left">
            <asp:GridView ID="gvBoughtOutItemIssuedDetails_AP_PDF" runat="server" AutoGenerateColumns="False"
                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                <Columns>
                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                        HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Part Name" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:Label ID="lblPartToPart" runat="server" Text='<%# Eval("PartName")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Material Grade" HeaderStyle-CssClass="text-center"
                        ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:Label ID="lblMaterialGrade" runat="server" Text='<%# Eval("GradeName")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="MRN Number" HeaderStyle-CssClass="text-center"
                        ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:Label ID="lblMRNNumber" runat="server" Text='<%# Eval("MRNNumber")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty" HeaderStyle-CssClass="text-center"
                        ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:Label ID="lblQty" runat="server" Text='<%# Eval("ISSUEDWEIGHT")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UOM" HeaderStyle-CssClass="text-center"
                        ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <asp:Label ID="lblUomName" runat="server" Text='<%# Eval("UomName")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>

