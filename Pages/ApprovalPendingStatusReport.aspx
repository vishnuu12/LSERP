<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="ApprovalPendingStatusReport.aspx.cs" Inherits="Pages_ApprovalPendingStatusReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%-- <link rel="stylesheet" type="text/css" href="../Assets/css/datatables2.css" />
    <script type="text/javascript" src="../Assets/scripts/datatables2.js"></script>--%>
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
                    return false;
                }
                else {
                    return true;
                }
            }
            else {
                return false;
            }
        }
        //        function ShowDataTable() {
        //            $('#<%=gvApprovalPendingStatusReport.ClientID %>').DataTable();
        //        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<Message:Uc ID="ucMessage" runat="server" />--%>
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
                                    <h3 class="page-title-head d-inline-block">
                                        Approval Pending Status Reports</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Sales</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Enquiry Approval Pending Status Report</li>
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
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="col-md-12 row">
                                            <div class="col-md-4">
                                                <p class="h-style">
                                                    <asp:Label ID="lbltitle" runat="server" Text="Approval Pending Status Report"></asp:Label></p>
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="form-label mandatorylbl">
                                                    From Date</label>
                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="mandatoryfield form-control datepicker"
                                                    PlaceHolder="Enter From Date" ToolTip="Enter From Date"></asp:TextBox>
                                            </div>
                                            <div class="col-sm-2">
                                                <label class="form-label mandatorylbl">
                                                    To Date</label>
                                                <asp:TextBox ID="txtToDate" runat="server" placeholder="Enter To Date" ToolTip="Enter Deadline Date"
                                                    CssClass="mandatoryfield form-control datepicker"></asp:TextBox>
                                            </div>
                                            <div class="col-md-2 p-t-35">
                                                <asp:LinkButton ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return Validate();"
                                                    OnClick="btnSubmit_Click" CssClass="btn btn-cons btn-save  AlignTop" />
                                            </div>
                                            <div class="col-md-2">
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <asp:GridView ID="gvApprovalPendingStatusReport" runat="server" CssClass="table table-bordered table-hover no-more-tables"
                                                AutoGenerateColumns="true">
                                                <Columns>
                                                </Columns>
                                            </asp:GridView>
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
</asp:Content>
