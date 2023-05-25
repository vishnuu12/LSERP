<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="AddtionalPartAndinternalPartExpensesSheet.aspx.cs"
    Inherits="Pages_AddtionalPartAndinternalPartExpensesSheet" ClientIDMode="Predictable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function bindIndentDetails() {

            let FromDate = $('#ContentPlaceHolder1_txtFromDate').val();
            let ToDate = $('#ContentPlaceHolder1_txtToDate').val();
            let PartType = $('[type="radio"]:checked').val();

            let msg = true;

            if (FromDate == "") {
                ErrorMessage('Error', 'Enter From Date');
                msg = false;
            }

            else if (ToDate == "") {
                ErrorMessage('Error', 'Enter To Date');
                msg = false;
            }

            $('#tblPIStatusReport').DataTable().destroy();

            if (msg) {
                $('#divPIStatus').attr('style', 'display:block;');

                $('#tblPIStatusReport').DataTable({
                    "retrieve": true,
                    "processing": true,
                    "serverSide": true,
                    "stateSave": true,
                    "bSort": false,
                    "pageLength": 50,
                    "order": [],
                    "ajax": ({
                        type: "GET",
                        url: "AddtionalPartAndinternalPartExpensesSheet.aspx/GetData", // It calls our web method //                   
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        "data": function (d) {
                            d.PartType = PartType;
                            d.FromDate = FromDate;
                            d.ToDate = ToDate;
                            return d;
                        },
                        "dataSrc": function (json) {
                            json.draw = json.d.draw;
                            json.recordsTotal = json.d.recordsTotal;
                            json.recordsFiltered = json.d.recordsFiltered;
                            json.data = json.d.data;
                            var return_data = json;
                            return return_data.data;
                        },
                    }),
                    "columns": [
                        { 'data': 'RFPNo' },
                        { 'data': 'MaterialName' },
                        { 'data': 'MRNNumber' },
                        { 'data': 'ISSUEDWEIGHT' },
                        { 'data': 'UnitQuoteCost' },
                        { 'data': 'TotalCost' },
                    ],
                });
            }
            else
                $('#divPIStatus').attr('style', 'display:none;');
            return false;
        }

    </script>

    <style type="text/css">
        .divradio span {
            text-align: center;
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
                                    <h3 class="page-title-head d-inline-block">Addtional Part Expenses Sheet </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Purchase</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page"> Addtional Part Expenses Sheet </li>
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
                        <div id="divAdd" class="btnAddNew" runat="server">
                            <div class="text-center">
                               <div class="col-sm-12 p-t-10 divradio">
                                    <asp:RadioButtonList ID="rblparttype" runat="server" CssClass="radio radio-success" RepeatDirection="Horizontal"
                                        AutoPostBack="false" RepeatLayout="Flow">
                                        <asp:ListItem Selected="True" Value="AP">AP</asp:ListItem>
                                        <asp:ListItem Value="IP">IP</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">Form Date </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker">  </asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">To Date </label>
                                    </div>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker">  </asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                             
                                <div class="col-sm-12 p-t-10">
                                    <asp:LinkButton ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return bindIndentDetails();"
                                        CssClass="btn btn-cons btn-success" />
                                </div>
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div id="divPIStatus" style="display: none;">
                                            <div class="col-sm-12 p-t-10">
                                            </div>
                                            <div class="col-sm-12" style="overflow-x: scroll;" runat="server">
                                                <table class="table table-hover table-bordered medium uniquedatatable" style="width: 100%;"
                                                    id="tblPIStatusReport">
                                                    <thead>
                                                        <tr>
                                                            <th>RFP No</th>
                                                            <th>Material Name</th>
                                                            <th>MRN Number</th>
                                                            <th>Issued Qty</th>
                                                            <th>Unit Cost</th>
                                                            <th>Total Cost</th>
                                                        </tr>
                                                    </thead>
                                                    <tfoot>
                                                    </tfoot>
                                                </table>
                                            </div>
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

