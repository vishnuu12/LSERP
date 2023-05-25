<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="RFPStaffAllocationQuality.aspx.cs"
    Inherits="Pages_RFPStaffAllocationQuality" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="../Assets/scripts/jquery.slim.min.js"></script>
    <link href="../Assets/css/multiselectdropdown.css" rel="stylesheet" />
    <script type="text/javascript" src="../Assets/scripts/jquery.multi-select.js"></script>
    <link rel="stylesheet" type="text/css" href="../Assets/css/datatables2.css" />
    <script type="text/javascript" src="../Assets/scripts/datatables2.js"></script>

    <script type="text/javascript">

        function ShowViewdocsPopUp() {
            $('#mpeViewdocs').modal({
                backdrop: 'static'
            });
            return false;
        }

        function ShowViewPopUp() {
            $('#mpeView').modal({
                backdrop: 'static'
            });
            return false;
        }

        function HideViewPopUp() {
            $('#mpeView').modal("hide");
            return false;
        }

        function PrintHtmlFile() {
            var cotent = $('#ContentPlaceHolder1_hdnPdfContent').val();
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

        function RFPPrint(epstyleurl, Main, QrCode) {
            var winprint = window.open('', 'letf=0,top=0,toolbar=0,scrollbars=1,status=0,Addressbar=no');

            var RowLength = $('#ContentPlaceHolder1_gvItemDetails_p').find('tr').length - 1;
            var BellowRowLength = $('#ContentPlaceHolder1_gvBellowDetails_p').find('tr').length - 1;

            var PageLength = parseInt(RowLength / 15);
            var BellowPageLength = parseInt(BellowRowLength / 25);

            var k = 0;
            if (RowLength > 5) {

                var BellowColLength = $('#ContentPlaceHolder1_gvBellowDetails_p').find('th').length;

                $('#ContentPlaceHolder1_gvItemDetails_p').find('tr:gt(0)').remove();
                $('#ContentPlaceHolder1_gvBellowDetails_p').find('tr:gt(0)').remove();

                $('#ContentPlaceHolder1_gvItemDetails_p').append('<tr style="text-align:center;"><td colspan="7"> As Per Annexure Enclosed </td></tr>');
                $('#ContentPlaceHolder1_gvBellowDetails_p').append('<tr style="text-align:center;"><td colspan="' + BellowColLength + '"> As Per Annexure Enclosed </td></tr>');

                //  $('#ContentPlaceHolder1_gvBellowDetails_p').append('<tr><td colspan="7" stle="text-align:center;"> As Per Annexure Enclosed </td></tr>')
                var FirstpageContent = $('#ContentPlaceHolder1_divPrint').html();
            }
            else {
                var FirstpageContent = $('#ContentPlaceHolder1_divPrint').html();
            }

            winprint.document.write("<html><head><title>");
            winprint.document.write("Offer");
            winprint.document.write("</title>");
            winprint.document.write("<link rel='stylesheet' type='text/css' href='" + epstyleurl + "'/>");
            winprint.document.write("<link rel='stylesheet' href='" + Main + "' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet'  href='<%= ResolveUrl("../Assets/css/style.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<link rel='stylesheet' href='<%= ResolveUrl("../Assets/css/printg.css") + "?rdvalue=" + DateTime.Now.ToString("mmddyyyyhhmmss")%>' type='text/css'/>");
            winprint.document.write("<style type='text/css'>@media print,screen { label,table th{ font-weight: bold; font-size: 15px !important;font-family:Times New Roman;color:#000 !important; }} .row{ padding-top:10px; } .page_generateoffer{ margin: 6mm; } table th{ vertical-align: middle; }  </style>");

            winprint.document.write("<div class='print-page' style='position:fixed;width:200mm;height:287mm;margin-top:5mm;margin-bottom:5mm;margin-left:5mm;margin-right:5mm'></div>");
            winprint.document.write("<table style='width:200mm;height:287mm;margin-left:20px;border:0px !important'><thead><tr><td>");
            winprint.document.write("<div class='col-sm-12 text-center p-b-30' style='padding-top: 10px; font-size: larger; font-weight: bold; font-family: Times New Roman; color: #000 !important;'>");
            winprint.document.write("REQUEST FOR PRODUCTION");
            winprint.document.write("</div>");
            winprint.document.write("</td></tr></thead>");
            winprint.document.write("<tbody><tr><td>");

            if (RowLength > 3) {
                winprint.document.write("<div class='page_generateoffer'>");
                winprint.document.write(FirstpageContent);
                winprint.document.write("</div>");

                for (var i = 0; i <= PageLength; i++) {
                    winprint.document.write("<div class='page_generateoffer'>");

                    winprint.document.write("<div class='row p-t-10'>");
                    winprint.document.write("<div class='col-sm-3 text-left'>");
                    winprint.document.write("<label>RFP No & Date</label>");
                    winprint.document.write("</div>");
                    winprint.document.write("<div class='col-sm-1'>:</div>");
                    winprint.document.write("<div class='col-sm-8'><span>" + $('#ContentPlaceHolder1_lblRFPNo_P').text() + "</span></div>");
                    winprint.document.write("</div>");

                    winprint.document.write("<div class='row p-t-10'>");
                    winprint.document.write("<div class='col-sm-3 text-left'>");
                    winprint.document.write("<label>Purchase Order No & Date</label>");
                    winprint.document.write("</div>");
                    winprint.document.write("<div class='col-sm-1'>:</div>");
                    winprint.document.write("<div class='col-sm-8'><span>" + $('#ContentPlaceHolder1_lblCustomerOrderNo_p').text() + "</span></div>");
                    winprint.document.write("</div>");

                    winprint.document.write("<div class='col-sm-12 p-t-10'>");
                    winprint.document.write("<table class='table table-hover medium' cellspacing='0' rules='all' border='1' id='ContentPlaceHolder1_gvAnnexureItemList_p_" + i + "' style='border-collapse:collapse;'>");
                    winprint.document.write("<tbody>");

                    for (var j = k; j < k + 15; j++) {
                        winprint.document.write("<tr align='center'>" + $('#ContentPlaceHolder1_gvAnnexureItemList_p').find('tr')[j].innerHTML + "</tr>");
                        if (RowLength == j) {
                            break;
                        }
                    }

                    k = k + 15;
                    winprint.document.write("</tbody>");
                    winprint.document.write("</table>");
                    winprint.document.write("</div>");

                    winprint.document.write("</div>");
                }

                k = 0;
                for (var i = 0; i <= BellowPageLength; i++) {
                    winprint.document.write("<div class='page_generateoffer'>");
                    winprint.document.write("<table class='table table-hover medium' cellspacing='0' rules='all' border='1' id='ContentPlaceHolder1_gvAnnexureBellowDetails_p_" + i + "' style='border-collapse:collapse;'>");
                    winprint.document.write("<tbody>");
                    for (var j = k; j < k + 25; j++) {
                        winprint.document.write("<tr align='center'>" + $('#ContentPlaceHolder1_gvAnnexureBellowDetails_p').find('tr')[j].innerHTML + "</tr>");
                        if (RowLength == j) {
                            break;
                        }
                    }
                    k = k + 25;
                    winprint.document.write("</tbody>");
                    winprint.document.write("</table>");
                    winprint.document.write("</div>");
                }
            }
            else {
                winprint.document.write("<div class='page_generateoffer'>");
                winprint.document.write(FirstpageContent);
                winprint.document.write("</div>");
            }

            winprint.document.write("</td></tr></tbody>");
            winprint.document.write("<tfoot><tr><td>");
            winprint.document.write("<div class='col-sm-12 text-center' style='height:20mm;'>");
            winprint.document.write("<img id='imgqrcode' class='Qrcode' src='" + QrCode + "' />");
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

        function ShowStaffPopUp(data) {
            $('#mpeStaffAssignment').modal('show');
            var mySelect = $('#languages');

            var d = JSON.parse(data);
            $.each(d.Staff, function (i, val) {
                mySelect.append(
                    $('<option></option>').val(val.EmployeeID).html(val.EmpName));
            });
            $('#languages').multiSelect();

            $('.multi-select-button').attr('style', 'width:100%;');
            $('.datepicker').datepicker({
                format: 'dd/mm/yyyy'
            });
            return false;
        }

        function CloseStaffPopUp() {
            $('#mpeStaffAssignment').modal('hide');
        }

        function ValidateStaff() {
            if ($('#languages').val() != "" && $('#txtDeadLineDate').val() != "") {
                $('#ContentPlaceHolder1_hdnEmployeeIDs').val($('#languages').val());
                return true;
            }
            else {
                ErrorMessage('Error', 'Mandatory  Required');
                return false;
            }
        }

    </script>
    <style type="text/css">
        #ContentPlaceHolder1_gvJobCardProcessDetails {
            table-layout: auto;
        }

        .radio-success label:nth-child(2) {
            color: red !important;
        }

        .radio-success label:nth-child(4) {
            color: #0acc0a !important;
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
                                    <h3 class="page-title-head d-inline-block">Quality Staff Assignment</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="AdminHome.aspx">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);"><asp:Label ID="lblModuleName" runat="server"></asp:Label></a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Quality Staff Assignment</li>
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
            <asp:UpdatePanel ID="upStaffAssignment" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="gvStaffAssignmentDetails" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divradio" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label" style="padding-top: 7px;">
                                            Select Type
                                        </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:RadioButtonList ID="rblRFPChange" runat="server" CssClass="radio radio-success" RepeatDirection="Horizontal"
                                            AutoPostBack="true" OnSelectedIndexChanged="rblRFPChange_OnSelectedChanged" RepeatLayout="Flow">
                                            <asp:ListItem Selected="True" Value="0">Not Assigned</asp:ListItem>
                                            <asp:ListItem Value="1">Assigned</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
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
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvStaffAssignmentDetails" runat="server" Width="100%" AutoGenerateColumns="false"
                                                ShowFooter="false" ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found"
                                                CssClass="table table-bordered table-hover no-more-tables tablestatesave" OnRowEditing="gvStaffAssignmentDetails_RowEditing"
                                                OnRowCancelingEdit="gvStaffAssignmentDetails_RowCancelingEdit" OnRowDataBound="gvStaffAssignmentDetails_RowDataBound"
                                                OnRowUpdating="gvStaffAssignmentDetails_RowUpdating" OnRowCommand="gvStaffAssignmentDetails_OnRowCommand"
                                                DataKeyNames="SAID,EnquiryID,EN,PoCopy,PoCopyWithoutPrice,RFPNo,RFPHID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SNo" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                <%--    <asp:TemplateField HeaderText="PO Ref Number" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPONumber" runat="server" Text='<%# Eval("PORefNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <%--   <asp:TemplateField HeaderText="Enquiry/Cust Enquiry No" HeaderStyle-CssClass="text-left"
                                                        ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEnquiryNumber" runat="server" Text='<%# Eval("EnquiryID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Unit A Staff" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnaddstaff" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="UnitAStaff"><img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Unit B Staff" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnaddunitbstaff" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="UnitBStaff"><img src="../Assets/images/add1.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="RFP No" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRFPNo" runat="server" Text='<%# Eval("RFPNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Location" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("Location") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Customer Name" HeaderStyle-CssClass="text-left" ItemStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCustomerName" runat="server" Text='<%# Eval("ProspectName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee Name" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmployeeName" runat="server" Text='<%# Eval("EmployeeName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="ddlEmployeeName" runat="server" CssClass="form-control">
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Staff">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStaffAssignment" runat="server" Text='<%# Eval("Staff") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Dead Line Date" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDeadLineDate" runat="server" Text='<%# Eval("DeadLineDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtDeadLineDate" runat="server" CssClass="form-control datepicker"
                                                                Text='<%# Eval("DeadLineDate") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delivery Date" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPODeliveryDate" runat="server" Text='<%# Eval("DateOfDelivery") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- <asp:CommandField ButtonType="Image" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"
                                                        ShowEditButton="true" EditText="<img src='~/images/icon_edit.png' title='Edit' />"
                                                        EditImageUrl="../Assets/images/edit-ec.png" CancelImageUrl="../Assets/images/icon_cancel.png"
                                                        UpdateImageUrl="../Assets/images/icon_update.png" ItemStyle-Wrap="false" ControlStyle-Width="20px"
                                                        ControlStyle-Height="20px" HeaderText="Edit" ValidationGroup="edit" HeaderStyle-Width="7%">
                                                        <ControlStyle CssClass="UsersGridViewButton" />
                                                    </asp:CommandField>--%>

                                                    <asp:TemplateField HeaderText="RFP PDF" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnRFPPDf" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="print"><img src="../Assets/images/pdf.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Attachements View" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnAttachements" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="ViewAttachements"><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PO Copy" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnPoCopy" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="ViewPOCopy"><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Po Copy Without Price" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnPoCopyWithoutprice" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                CommandName="ViewPoCopyWithoutPrice"><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="hdnPdfContent" runat="server" />
                            <asp:HiddenField ID="hdnSAID" runat="server" Value="0" />
                            <asp:HiddenField ID="hdnEmployeeIDs" runat="server" Value="0" />
                            <asp:HiddenField ID="hdnLocationType" runat="server" Value="0" />
                        </div>
                    </div>

                    <div class="col-sm-12" id="divPrint" runat="server" style="display: none;">
                        <div style="">
                            <%--   <div class="col-sm-12 text-center p-b-30" style="padding-top: 10px; font-size: larger; font-weight: bold; font-family: Times New Roman; color: #000 !important;">
                                REQUEST FOR PRODUCTION
                            </div>--%>
                            <div class="row p-t-10">
                                <div class="col-sm-3 text-left">
                                    <label>
                                        RFP No & Date</label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblRFPNo_P"></asp:Label>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-3 text-left">
                                    <label>
                                        Customer Name</label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblCustomerName_p"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 text-left">
                                    <label>
                                        Purchase Order No & Date</label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblCustomerOrderNo_p"></asp:Label>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-sm-3 text-left">
                                    <label>
                                        Project</label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblProject_p"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 text-left">
                                    <label>
                                        Number Of Items</label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblNumberOfItems_p"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 text-left">
                                    <label>
                                        Location Name</label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblLocationName_p"></asp:Label>
                                </div>
                            </div>
                            <div class="col-sm-12 p-t-10">
                                <asp:GridView ID="gvItemDetails_p" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                    EmptyDataText="No Records Found" CssClass="table table-hover medium">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                            HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSno" runat="server" Text='<%# Eval("SNo")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Code/Tag No/MTL Code" HeaderStyle-CssClass="text-center"
                                            HeaderStyle-Width="10%" ItemStyle-Width="10%" ItemStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblitemname" runat="server" Text='<%# Eval("TagNo")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Size (mm)" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"
                                            HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemSize" runat="server" Text='<%# Eval("ItemSize")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Name" HeaderStyle-Width="15%" ItemStyle-Width="15%"
                                            HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblitemname" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DRG No." HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"
                                            HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDrawingNumber" runat="server" Text='<%# Eval("DesignNumber")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item SL.No" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"
                                            HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemSno" runat="server" Text='<%# Eval("ItemSno")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QTY (Nos)" HeaderStyle-CssClass="text-center" HeaderStyle-Width="5%"
                                            ItemStyle-Width="5%" ItemStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblquantity" class="lblqty" runat="server" Text='<%# Eval("Quantity")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="row m-t-10">
                                <div class="col-sm-3 text-left">
                                    <label>
                                        QAP Ref</label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblQAPRefNo_p"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 text-left">
                                    <label>
                                        QAP Approval</label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblQAPApproval_p"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 text-left">
                                    <label>
                                        Drawing Approval</label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblDrawingApproval_p"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <label>
                                        Due Date For Dispatch</label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblDueDateForDispatch_p"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 text-left">
                                    <label>
                                        Inspection
                                    </label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblInspectionRequirtment_p"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 text-left">
                                    <label>
                                        LD Clause</label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblLDClause_p"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 text-left">
                                    <label>
                                        Despatch Details</label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblDespatchDetails_p"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 text-left">
                                    <label>
                                        Prepared By</label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblMarketing_p"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 text-left">
                                    <label>
                                        Checked By</label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblDesign_p"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 text-left">
                                    <label>
                                        Approved By</label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblApprovedBy_p"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 text-left">
                                    <label>
                                        Project Incharge</label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblProjectIncharge_p"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3 text-left">
                                    <label>
                                        Notes Summary</label>
                                </div>
                                <div class="col-sm-1">
                                    :
                                </div>
                                <div class="col-sm-8">
                                    <asp:Label runat="server" ID="lblNotesSummary_p"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <label style="text-align: center; width: 100%; font-size: 20px !important; font-weight: 700;">
                                    Bellow Details</label>
                            </div>
                            <div class="col-sm-12 p-t-10">
                                <asp:GridView ID="gvBellowDetails_p" runat="server" AutoGenerateColumns="true" ShowHeaderWhenEmpty="True"
                                    OnRowDataBound="gvBellowDetails_p_RowDataBound" EmptyDataText="No Records Found"
                                    CssClass="table table-hover medium" HeaderStyle-HorizontalAlign="Center">
                                    <Columns>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-12" id="divAnnexureItemList_p" runat="server" style="display: none; margin-top: 10px; margin-bottom: 10px">
                        <asp:GridView ID="gvAnnexureItemList_p" runat="server" AutoGenerateColumns="False"
                            ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover medium">
                            <Columns>
                                <asp:TemplateField HeaderText="SL.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                                    HeaderStyle-Width="5%" ItemStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSno" runat="server" Text='<%# Eval("SNo")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Code/Tag No/MTL Code" HeaderStyle-CssClass="text-center"
                                    HeaderStyle-Width="10%" ItemStyle-Width="10%" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblitemname" runat="server" Text='<%# Eval("TagNo")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size (mm)" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"
                                    HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemSize" runat="server" Text='<%# Eval("ItemSize")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Name" HeaderStyle-Width="15%" ItemStyle-Width="15%"
                                    HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblitemname" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DRG No." HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"
                                    HeaderStyle-Width="15%" ItemStyle-Width="15%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDrawingNumber" runat="server" Text='<%# Eval("DesignNumber")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item SL.No" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"
                                    HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblItemSno" runat="server" Text='<%# Eval("ItemSno")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="QTY (Nos)" HeaderStyle-CssClass="text-center" HeaderStyle-Width="5%"
                                    ItemStyle-Width="5%" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblquantity" class="lblqty" runat="server" Text='<%# Eval("Quantity")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="col-sm-12" style="width: 100%; display: none;" runat="server" id="divAnnexureBellowList_p">
                        <asp:GridView ID="gvAnnexureBellowDetails_p" runat="server" AutoGenerateColumns="true"
                            OnRowDataBound="gvAnnexureBellowDetails_p_RowDataBound" ShowHeaderWhenEmpty="True"
                            EmptyDataText="No Records Found" CssClass="table table-hover medium" HeaderStyle-HorizontalAlign="Center">
                            <Columns>
                            </Columns>
                        </asp:GridView>
                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal" id="mpeViewdocs" style="height: 125%; top: -10%;">
        <div class="modal-dialog" style="height: 100%;">
            <div class="modal-content" style="height: 100%;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" Style="height: 100%;">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H2">
                                <div class="text-center">
                                    Documents
                                    <asp:Label ID="Label1" Text="" runat="server"></asp:Label>
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal">
                                x</button>
                        </div>
                        <div class="modal-body" style="height: 100%;">
                            <div class="inner-container" style="height: 100%;">
                                <div class="ip-div text-center" style="margin-bottom: 10px;">
                                    <%--     <asp:LinkButton Text="Download" CssClass="btn btn-save btn-cons AlignTop" ID="btndownloaddocs"
                                        OnClick="btndownloaddocs_Click" runat="server" />--%>
                                </div>
                                <div class="col-sm-12" style="height: 100%;">
                                    <iframe runat="server" id="ifrm" src="" style="width: 100%; height: 80%;" frameborder="0"></iframe>
                                </div>
                            </div>
                        </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal" id="mpeView" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="upView" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="gvDrawingFiles" />
                        <asp:PostBackTrigger ControlID="gvAttachments" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">Documents
                            </h4>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="docdiv" class="docdiv">
                                <div class="inner-container">
                                </div>
                                <div class="col-sm-12" style="text-align: center; font-size: 20px;">
                                    <label>
                                        View Drawimg Files</label>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <asp:GridView ID="gvDrawingFiles" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                        EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                        OnRowCommand="gvDrawingFiles_OnRowCommand" DataKeyNames="FileName,EnquiryID">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Drawing Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDrawingName" runat="server" Text='<%# Eval("DrawingName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Drawing Path" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnView" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                        CommandName="ViewDrawings"><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <div class="modal-footer">
                                    <div class="col-sm-12 p-t-10">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <asp:GridView ID="gvAttachments" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                        EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                        OnRowCommand="gvAttachments_OnRowCommand" DataKeyNames="AttachementID">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Attachement Type Name" ItemStyle-HorizontalAlign="left"
                                                HeaderStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAttachementTypeName_V" runat="server" Text='<%# Eval("AttachementTypeName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDescription_V" runat="server" Text='<%# Eval("Description")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="View" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFileName_V" runat="server" Visible="false" Text='<%# Eval("FileName")%>'></asp:Label>
                                                    <asp:LinkButton ID="lbtnView" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                        CommandName="ViewDocs"><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
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
                        <asp:HiddenField ID="hdnAttachementFlag" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div class="modal" id="mpeStaffAssignment" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">
                                <asp:Label ID="lblRFPNumber_staff" Style="color: brown;" runat="server"></asp:Label>
                            </h4>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="docdiv_staff" class="docdiv">
                                <div class="inner-container">
                                </div>

                                <div class="col-sm-12" style="text-align: center; font-size: 20px; padding-top: 20px;">
                                    <div class="col-sm-4">
                                        <label>
                                            Employee Name 
                                        </label>
                                    </div>
                                    <div class="col-sm-4">
                                        <select id="languages" name="languages" class="form-control chosenfalse" multiple="multiple">
                                        </select>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>

                                <div class="col-sm-12" style="text-align: center; font-size: 20px; padding-top: 20px;">
                                    <div class="col-sm-4">
                                        <label>
                                            Dead Line Date
                                        </label>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtDeadLineDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>

                                <div class="col-sm-12 text-center p-t-10">
                                    <asp:LinkButton ID="btnSaveStaff" runat="server" CssClass="btn btn-cons btn-save AlignTop"
                                        Text="Save" OnClientClick="return ValidateStaff();" OnClick="btnSaveStaff_Click">
                                    </asp:LinkButton>
                                </div>

                                <div class="col-sm-12 p-t-10">
                                    <asp:GridView ID="gvRFPStaffDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                        EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                        DataKeyNames="SAID">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Staff List" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStafList" runat="server" Text='<%# Eval("")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dead Line Date" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbldeadlinedate" runat="server" Text='<%# Eval("")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>

                                <div class="modal-footer">
                                </div>

                            </div>
                        </div>
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
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

</asp:Content>

