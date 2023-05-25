<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="ViewMaterialPlanningServerSide.aspx.cs" Inherits="Pages_ViewMaterialPlanningServerSide" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {
            BindDate();
        });

        function BindDate() {
            $('#tblPIStatusReport tfoot').append("<tr></tr>");
            $('#tblPIStatusReport thead th').each(function () {
                var title = $(this).text();
                $('#tblPIStatusReport tfoot tr').append('<th> <input type="text" class="form-control" placeholder="Search ' + title + '" /> </th>');
            });

            $('#tblPIStatusReport').DataTable({

                initComplete: function () {
                    // Apply the search
                    this.api().columns().every(function () {
                        var that = this;
                        $('input', this.footer()).on('keyup change clear', function () {
                            if (that.search() !== this.value) {
                                that
                                    .search(this.value)
                                    .draw();
                            }
                        });
                    });
                },

                "retrieve": true,
                "processing": true,
                "serverSide": true,
                "stateSave": true,
                //"ordering": false,
                //"bSort": false,                     
                "bDeferRender": true,
                "ajax": ({
                    type: "GET",
                    url: "ViewMaterialPlanningServerSide.aspx/GetData", //It calls our web method  
                    // url:"https://innovasphere.com/LSERP/pages/PurchaseIndentMaterialInwardStatusReport/GetData",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    "data": function (d) {
                        return d;
                    },
                    "dataSrc": function (json) {
                        json.draw = json.d.draw;
                        json.recordsTotal = json.d.recordsTotal;
                        json.recordsFiltered = json.d.recordsFiltered;
                        json.Ldata = json.d.Ldata;
                        var return_data = json;
                        return return_data.Ldata;
                    },
                }),
                "columns": [
                    { 'data': 'RFPNo' },
                    { 'data': 'BlockedWeight' },
                    { 'data': 'PartName' },
                    { 'data': 'GradeNameDesign' },
                    { 'data': 'GradeNameProduction' },
                    { 'data': 'Username' },
                    { 'data': 'BlockedDate' },
                    { 'data': 'MRNNumber' },
                    { 'data': 'THKValue' },
                    { 'data': 'UOM' },
                    { 'data': 'ItemName' }
                ],

            });
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
                                    <h3 class="page-title-head d-inline-block">View Material planning</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Production</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">View material Planning</li>
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
            <asp:UpdatePanel ID="upQuote" runat="server" UpdateMode="Always">
                <Triggers>
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" runat="server">
                            <div class="ip-div text-center">
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <table class="table table-hover table-bordered medium uniquedatatable"
                                                id="tblPIStatusReport"
                                                width="100%">
                                                <thead>
                                                    <tr>
                                                        <th>RFPNo</th>
                                                        <th>BlockedWeight</th>
                                                        <th>PartName</th>
                                                        <th>GradeNameDesign</th>
                                                        <th>GradeNameProduction</th>
                                                        <th>Username</th>
                                                        <th>BlockedDate</th>
                                                        <th>MRNNumber</th>
                                                        <th>THKValue</th>
                                                        <th>UOM</th>
                                                        <th>ItemName</th>
                                                    </tr>
                                                </thead>
                                                <%--   <tbody>
                                                </tbody>--%>
                                                <tfoot>
                                                </tfoot>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hdnBOMId" Value="0" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

