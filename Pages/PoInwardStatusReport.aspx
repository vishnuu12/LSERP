<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Master/CommonMaster.master" CodeFile="PoInwardStatusReport.aspx.cs"
    Inherits="Pages_PoInwardStatusReport" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        // var oTable;
        $(document).ready(function () {
            BindDate();
            // oTable = $('#tblPOInwardStatusReport').dataTable();
        });

        function BindDate() {
            $('#tblPOInwardStatusReport').DataTable({
                "retrieve": true,
                "processing": true,
                "serverSide": true,
                // "stateSave": true,
                //"ordering": false,
                //"bSort": false,               
                "bDeferRender": true,
                "pageLength": 100,
                "bSort": false,
                "order": [],

                "bStateSave": true,
                "fnStateSave": function (oSettings, oData) {
                    localStorage.setItem('DataTables_' + window.location.pathname, JSON.stringify(oData));
                },
                "fnStateLoad": function (oSettings) {
                    var data = localStorage.getItem('DataTables_' + window.location.pathname);
                    return JSON.parse(data);
                },
                "stateSaveParams": function (settings, data) {
                    delete data.order;
                    data.length = 100;
                    data.order = [];
                },

                "ajax": ({
                    type: "GET",
                    url: "PoInwardStatusReport.aspx/GetData", //It calls our web method
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
                    { 'data': 'InwardStatus' },
                    { 'data': 'PONO' },
                    { 'data': 'PODate' },
                    { 'data': 'SupplierName' },
                    { 'data': 'CategoryName' },
                    { 'data': 'GradeName' },
                    { 'data': 'THKValue' },
                    { 'data': 'TypeName' },
                    { 'data': 'UOM' },
                    { 'data': 'ReqWeight' },
                    { 'data': 'DeliveryDate' },
                    { 'data': 'Measurment' },
                    { 'data': 'UnitQuoteCost' },
                    { 'data': 'TotalCost' },
                    {
                        'data': null,
                        "mRender": function (data, type, full) {
                            if (data.InwardCost === null)
                                return `<a href="#" onclick="return openSPOPrint('${data.SPOID}')";></a>`
                            else
                                return `<a href="#" onclick="return ViewInwardDetails('${data.SPOID}')";>${data.InwardCost}</a>`
                        }
                    },
                    { 'data': 'Remarks' },
                    {
                        'data': null,
                        "mRender": function (data, type, full) {
                            return `<a href="#" onclick="return openSPOPrint('${data.SPOID}')";><img src="../Assets/images/print.png" /></a>`
                        }
                    },
                ],

                "rowCallback": function (row, data, displayIndex, displayIndexFull) {
                    if (data.TotalCost != data.InwardCost) {
                        if (data.InwardCost == null)
                            $(row).css('background-color', 'none');
                        else
                            $(row).css('background-color', '#10fafa');
                    }
                    else
                        $(row).css('background-color', 'lightgreen');
                },
            });
        }

        function BindDatatable(id) {
            $('#' + id + '').DataTable({
                "retrieve": true,
                "pageLength": 50,
                //  responsive: true                             
            });
        }

        function ExcelDownload() {
            jQuery.ajax({
                type: "GET",
                url: "PoInwardStatusReport.aspx/ExcelDownLoad", //It calls our web method
                contentType: "application/json; charset=utf-8",
                dataType: "JSON",
                success: function (data) {
                    //var response = JSON.parse(data.d);
                    window.location.href = "/Pages/PoInwardStatusReport.aspx?FileName=" + data.d.Data.FileName + "";
                        // Users can skip the loading process if they want.
                        //$('.skip').click(function () {
                        //    $('.overlay, body').addClass('loaded');
                        //})

                        //// Will wait for everything on the page to load.
                        //$(window).bind('load', function () {
                        //    $('.overlay, body').addClass('loaded');
                        //    setTimeout(function () {
                        //        $('.overlay').css({ 'display': 'none' })
                        //    }, 2000)
                        //});

                        //// Will remove overlay after 1min for users cannnot load properly.
                        //setTimeout(function () {
                        //    $('.overlay, body').addClass('loaded');
                        //}, 60000);
                },
                error: function (data) {
                }
            });
            return false;
        }

        function openSPOPrint(SPOID) {
            var url = window.location.href;
            var pagename = url.split('/');
            var Replacevalue = pagename[pagename.length - 1];
            var Page = url.replace(Replacevalue, "SupplierPOPrint.aspx?SPOID=" + SPOID + "");
            window.open(Page, '_blank');
        }

    </script>

    <style type="text/css">
        .dataTables_scrollHeadInner {
            height: 40px;
        }

            .dataTables_scrollHeadInner table thead th {
                vertical-align: initial;
            }

        .dataTables_scroll thead tr th:nth-child(1), .dataTables_scroll tbody tr td:nth-child(1),
        .dataTables_scroll tfoot tr th:nth-child(1), .dataTables_scroll tfoot tr th:nth-child(1) input {
            width: 78px !important;
        }

        .modal-dialog {
            margin-left: 2%;
            max-width: 98%;
        }

        .table {
            color: #000;
            font-weight: bold;
        }
/*
        @import url('https://fonts.googleapis.com/css?family=Nunito:400,600,700|Roboto:300,400,500,700');


body {
	margin: 0;
	padding: 0;
	overflow: hidden;
	&.loaded {
		overflow-y: auto;
	}
}

.overlay {
	position: fixed;
	top: 0;
	left: 0;
	width: 100%;
	height: 100%;
	z-index: 100000000;
	.overlayDoor {
		&:before, &:after {
			content: "";
			position: absolute;
			width: 50%;
			height: 100%;
			background: #111;
			transition: .5s cubic-bezier(.77,0,.18,1);
			transition-delay: .8s;
		}
		&:before {
			left: 0;
		}
		&:after {
			right: 0;
		}
	}
	&.loaded {
		.overlayDoor {
			&:before {
				left: -50%;
			}
			&:after {
				right: -50%;
			}
		}
		.overlayContent {
			opacity: 0;
			margin-top: -15px;
		}
	}
	.overlayContent {
		position: relative;
		width: 100%;
		height: 100%;
		display: flex;
		justify-content: center;
		align-items: center;
		flex-direction: column;
		transition: .5s cubic-bezier(.77,0,.18,1);
		.skip {
			display: block;
			width: 130px;
			text-align: center;
			margin: 50px auto 0;
			cursor: pointer;
			color: #fff;
			font-family: 'Nunito';
			font-weight: 700;
			padding: 12px 0;
			border: 2px solid #fff;
			border-radius: 3px;
			transition: 0.2s ease;
			&:hover {
				background: #ddd;
				color: #444;
				border-color: #ddd;
			}
		}
	}
}
.loader {
	width: 128px;
	height: 128px;
	border: 3px solid #fff;
	border-bottom: 3px solid transparent;
	border-radius: 50%;
	position: relative;
	animation: spin 1s linear infinite;
	display: flex;
	justify-content: center;
	align-items: center;
	.inner {
		width: 64px;
		height: 64px;
		border: 3px solid transparent;
		border-top: 3px solid #fff;
		border-radius: 50%;
		animation: spinInner 1s linear infinite;
	}
}
@keyframes spin {
	0% {
		transform: rotate(0deg);
	}
	100% {
		transform: rotate(360deg);
	}
}
@keyframes spinInner {
	0% {
		transform: rotate(0deg);
	}
	100% {
		transform: rotate(-720deg);
	}
}*/

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
                                    <h3 class="page-title-head d-inline-block">PO Inward Status Report </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Reports</a></li>
                                <li class="active breadcrumb-item" aria-current="page">PO Inward Status Report</li>
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
                    <asp:PostBackTrigger ControlID="imgExcel" />
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
                                        <div class="col-sm-12 p-t-10">
                                            <span style="font-size: 16px; color: red; font-weight: bold;">Only Approved PO No Show Total Cost And Inward Cost Without  Tax And Other Charges Value </span>
                                        </div>
                                        <div class="col-sm-12 p-t-5">
                                            <div class="col-sm-6">
                                            </div>
                                            <div class="col-sm-6 text-right">
                                                <%--  <asp:LinkButton ID="imgExcel" runat="server" CssClass="excel_bg" ToolTip="Excel Download"
                                                OnClick="btnExcelDownload_Click" />--%>
                                                <asp:LinkButton ID="imgExcel" runat="server" CssClass="excel_bg" ToolTip="Excel Download"
                                                onclientclick="return ExcelDownload();" />
                                               <%-- <button class="excel_bg"  onclick="return ExcelDownload();"></button>--%>
                                            </div>
                                        </div>
                               <%--         <div class="overlay">
                                            <div class="overlayDoor"></div>
                                            <div class="overlayContent">
                                                <div class="loader">
                                                    <div class="inner"></div>
                                                </div>
                                                <div class="skip">SKIP</div>
                                            </div>
                                        </div>--%>
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <table class="table table-hover table-bordered medium uniquedatatable"
                                                id="tblPOInwardStatusReport"
                                                width="100%">
                                                <thead>
                                                    <tr>
                                                        <th>Inward Status</th>
                                                        <th>PO No </th>
                                                        <th>PO Date </th>
                                                        <th>Supplier Name</th>
                                                        <th>Category Name</th>
                                                        <th>Grade Name</th>
                                                        <th>THKValue</th>
                                                        <th>TypeName</th>
                                                        <th>UOM</th>
                                                        <th>ReqWeight</th>
                                                        <th>DeliveryDate</th>
                                                        <th>Measurment</th>
                                                        <th>UnitQuoteCost</th>
                                                        <th>Total Cost</th>
                                                        <th>Inward Cost</th>
                                                        <th>Remarks</th>
                                                        <th>Print PO</th>
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
