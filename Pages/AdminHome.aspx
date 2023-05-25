<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="AdminHome.aspx.cs" Inherits="AdminHome" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .modal-dialog {
            margin-top: 60px !important;
            height: 50% !important;
        }

        .modal-content {
            height: 85%;
        }

        .modal-body img {
            width: auto !important;
        }

        ::-webkit-scrollbar {
            width: 0px !important;
            background: transparent !important;
            display: none;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            debugger;
            // $('.description').find('table').hide();
            $('.collapsible').click(function () {
                if ($(this).closest('.descrptncontent').find('table').is(':visible') == true) {
                    $(this).closest('.descrptncontent').find('table').hide();
                    $(this).removeClass("active");
                }
                else {
                    $(this).closest('.descrptncontent').find('table').show();
                    if ($(this).closest('.descrptncontent').find('table').length > 0) $(this).addClass("active");
                }
            });
        });

        function Approvals(ele) {
            __doPostBack('Approvals', ele.id);
            return false;
        }

        function Inbox(ele) {
            __doPostBack('Inbox', ele.id);
            return false;
        }

        function Task(ele) {
            __doPostBack('Task', ele.id);
            return false;
        }

        function Alerts(ele) {
            __doPostBack('Alerts', ele.id);
            return false;
        }

        function piechart() {
            //labels = labels.replace(/-/g, '"').replace(/_/g, '",').replace(/,\s*$/, '');
            //datas = datas.replace(/_/g, ",").replace(/,\s*$/, "");
            try {
                // Set new default font family and font color to mimic Bootstrap's default styling
                Chart.defaults.global.defaultFontFamily = 'Nunito', '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
                Chart.defaults.global.defaultFontColor = '#858796';

                // Pie Chart Example
                var ctx = document.getElementById("myPieChart");
                var myPieChart = new Chart(ctx, {
                    type: 'doughnut',
                    data: {
                        labels: [],
                        datasets: [{
                            data: [],
                            backgroundColor: ['#4e73df', '#1cc88a'],
                            hoverBackgroundColor: ['#2e59d9', '#17a673'],
                            hoverBorderColor: "rgba(234, 236)",
                        }],
                    },
                    options: {
                        maintainAspectRatio: false,
                        tooltips: {
                            backgroundColor: "rgb(255,255,255)",
                            bodyFontColor: "#858796",
                            borderColor: '#dddfeb',
                            borderWidth: 1,
                            xPadding: 15,
                            yPadding: 15,
                            displayColors: false,
                            caretPadding: 10,
                        },
                        legend: {
                            display: true
                        },
                        cutoutPercentage: 80,
                    },
                });
                debugger;
                var json = JSON.parse(document.getElementById("ContentPlaceHolder1_hdnjsonstring").value);
                $.each(json, function (i, item) {
                    myPieChart.data.labels.push(json[i].Location);
                    myPieChart.data.datasets[0].data.push(json[i].Count);
                });
            } catch{ }
        }
        function areachart() {
            try {
                // Set new default font family and font color to mimic Bootstrap's default styling
                Chart.defaults.global.defaultFontFamily = 'Nunito', '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
                Chart.defaults.global.defaultFontColor = '#858796';

                // Area Chart Example
                var ctx = document.getElementById("myAreaChart");
                var myLineChart = new Chart(ctx, {
                    type: 'line',
                    data: {
                        labels: [],
                        datasets: [{
                            label: "Earnings",
                            lineTension: 0.3,
                            backgroundColor: "rgba(78, 115, 223, 0.05)",
                            borderColor: "rgba(78, 115, 223, 1)",
                            pointRadius: 3,
                            pointBackgroundColor: "rgba(78, 115, 223, 1)",
                            pointBorderColor: "rgba(78, 115, 223, 1)",
                            pointHoverRadius: 3,
                            pointHoverBackgroundColor: "rgba(78, 115, 223, 1)",
                            pointHoverBorderColor: "rgba(78, 115, 223, 1)",
                            pointHitRadius: 10,
                            pointBorderWidth: 2,
                            data: [],
                        }],
                    },
                    options: {
                        maintainAspectRatio: false,
                        layout: {
                            padding: {
                                left: 10,
                                right: 25,
                                top: 25,
                                bottom: 0
                            }
                        },
                        scales: {
                            xAxes: [{
                                time: {
                                    unit: 'date'
                                },
                                gridLines: {
                                    display: false,
                                    drawBorder: false
                                },
                                ticks: {
                                    maxTicksLimit: 7
                                }
                            }],
                            yAxes: [{
                                ticks: {
                                    maxTicksLimit: 5,
                                    padding: 10,
                                    // Include a dollar sign in the ticks
                                    callback: function (value, index, values) {
                                        return '₹' + number_format(value);
                                    }
                                },
                                gridLines: {
                                    color: "rgb(234, 236, 244)",
                                    zeroLineColor: "rgb(234, 236, 244)",
                                    drawBorder: false,
                                    borderDash: [2],
                                    zeroLineBorderDash: [2]
                                }
                            }],
                        },
                        legend: {
                            display: false
                        },
                        tooltips: {
                            backgroundColor: "rgb(255,255,255)",
                            bodyFontColor: "#858796",
                            titleMarginBottom: 10,
                            titleFontColor: '#6e707e',
                            titleFontSize: 14,
                            borderColor: '#dddfeb',
                            borderWidth: 1,
                            xPadding: 15,
                            yPadding: 15,
                            displayColors: false,
                            intersect: false,
                            mode: 'index',
                            caretPadding: 10,
                            callbacks: {
                                label: function (tooltipItem, chart) {
                                    var datasetLabel = chart.datasets[tooltipItem.datasetIndex].label || '';
                                    return datasetLabel + ': ₹' + number_format(tooltipItem.yLabel);
                                }
                            }
                        }
                    }
                });
                debugger;
                var jsonmonth = JSON.parse(document.getElementById("ContentPlaceHolder1_hdnmonthjsonstring").value);
                $.each(jsonmonth, function (i, item) {
                    myLineChart.data.labels.push(jsonmonth[i].MonthYear);
                    myLineChart.data.datasets[0].data.push(jsonmonth[i].FinalCost);
                });
            }
            catch{ }
        }
        function number_format(number, decimals, dec_point, thousands_sep) {
            // *     example: number_format(1234.56, 2, ',', ' ');
            // *     return: '1 234,56'
            number = (number + '').replace(',', '').replace(' ', '');
            var n = !isFinite(+number) ? 0 : +number,
                prec = !isFinite(+decimals) ? 0 : Math.abs(decimals),
                sep = (typeof thousands_sep === 'undefined') ? ',' : thousands_sep,
                dec = (typeof dec_point === 'undefined') ? '.' : dec_point,
                s = '',
                toFixedFix = function (n, prec) {
                    var k = Math.pow(10, prec);
                    return '' + Math.round(n * k) / k;
                };
            // Fix for IE parseFloat(0.55).toFixed(0) = 0;
            s = (prec ? toFixedFix(n, prec) : '' + Math.round(n)).split('.');
            if (s[0].length > 3) {
                s[0] = s[0].replace(/\B(?=(?:\d{3})+(?!\d))/g, sep);
            }
            if ((s[1] || '').length < prec) {
                s[1] = s[1] || '';
                s[1] += new Array(prec - s[1].length + 1).join('0');
            }
            return s.join(dec);
        }
    </script>

    <style type="text/css">
        .collapsible {
            border-bottom: none !important;
        }
        .tiles .heading{
             border-bottom: none !important;
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
                                    <h3 class="page-title-head d-inline-block">Home Dashboard</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>                                                    
                                                    <li class="active breadcrumb-item">Dashboard</li>
                                                </ol>
                                     </nav>
                        <a id="help" href="" alt="" style="margin-top: 4px;">
                            <img src="../Assets/images/help.png" />
                        </a>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="col-sm-6 spacing-bottom-sm spacing-bottom spacing spacing-left" style="max-height: 20%">
                    <div class="tiles blue added-margin">
                        <div class="heading">
                            <span>Approvals</span>
                        </div>
                        <div class="dashbrdbody tiles-body" style="max-height: 135px !important;">
                            <div class="description" style="width: 20%;">
                                <ul style="list-style-type: none;" id="ulList" runat="server">
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6 spacing-bottom-sm spacing-bottom spacing-right">
                    <div class="tiles green added-margin">
                        <div class="heading">
                            <span>Alerts</span>
                        </div>
                        <div class="dashbrdbody tiles-body" style="max-height: 135px !important;">
                            <div class="description" style="width: 20%;">

                                <ul style="list-style-type: none;" id="ulist_Alerts" runat="server">
                                </ul>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-12">
                <div class="col-sm-6 spacing-bottom spacing-left">
                    <div class="tiles red added-margin">
                        <div class="heading">
                            <span>InBox</span>
                        </div>
                        <div class="dashbrdbody tiles-body" style="max-height: 135px;">
                            <div class="description" style="width: 20%;">
                                <ul style="list-style-type: none;" id="ulList_Inbox" runat="server">
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6 spacing-bottom spacing-right">
                    <div class="tiles purple added-margin">
                        <div class="heading">
                            <span>Task</span>
                        </div>
                        <div class="dashbrdbody tiles-body" style="max-height: 135px;">
                            <div class="description" style="width: 20%;">
                                <ul style="list-style-type: none;" id="ulList_Task" runat="server">
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div id="divgraph" class="container-fluid" runat="server" visible="false">
                <!-- Page Heading -->
                <%-- <div class="d-sm-flex align-items-center justify-content-between mb-4">
                        <a href="#" class="d-none d-sm-inline-block btn btn-sm btn-primary shadow-sm"><i class="fas fa-download fa-sm text-white-50"></i>Generate Report</a>
                    </div>--%>
                <!-- Content Row -->
                <div class="row">
                    <!-- Earnings (Monthly) Card Example -->
                    <div class="col-xl-3 col-md-6 mb-4">
                        <div class="card border-left-primary shadow h-100 py-2">
                            <div class="card-body">
                                <div class="row no-gutters align-items-center">
                                    <div class="col mr-2">
                                        <div class="text-xs font-weight-bold text-primary text-uppercase mb-1">
                                            Enquiries (Monthly)
                                        </div>
                                        <div class="h5 mb-0 font-weight-bold text-gray-800">
                                            <asp:Label ID="lblMonthyEnquiries" runat="server" Text="">0</asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-auto">
                                        <i class="fas fa-calendar fa-2x text-gray-300"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- Earnings (Monthly) Card Example -->
                    <div class="col-xl-3 col-md-6 mb-4">
                        <div class="card border-left-success shadow h-100 py-2">
                            <div class="card-body">
                                <div class="row no-gutters align-items-center">
                                    <div class="col mr-2">
                                        <div class="text-xs font-weight-bold text-success text-uppercase mb-1">
                                            Orders (Monthly)
                                        </div>
                                        <div class="h5 mb-0 font-weight-bold text-gray-800">
                                            <asp:Label ID="lblMonthlyPOOrder" runat="server" Text="">0</asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-auto">
                                        <i class="fas fa-calendar fa-2x text-gray-300"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- Earnings (Monthly) Card Example -->
                    <div class="col-xl-3 col-md-6 mb-4">
                        <div class="card border-left-success shadow h-100 py-2">
                            <div class="card-body">
                                <div class="row no-gutters align-items-center">
                                    <div class="col mr-2">
                                        <div class="text-xs font-weight-bold text-success text-uppercase mb-1">
                                            Pending Enquiries
                                        </div>
                                        <div class="h5 mb-0 font-weight-bold text-gray-800">
                                            <asp:Label ID="lblpendingenquiries" runat="server" Text="">0</asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-auto">
                                        <i class="fas fa-clipboard-list fa-2x text-gray-300"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- Pending Requests Card Example -->
                    <div class="col-xl-3 col-md-6 mb-4">
                        <div class="card border-left-warning shadow h-100 py-2">
                            <div class="card-body">
                                <div class="row no-gutters align-items-center">
                                    <div class="col mr-2">
                                        <div class="text-xs font-weight-bold text-warning text-uppercase mb-1">
                                            Pending Orders
                                        </div>
                                        <div class="h5 mb-0 font-weight-bold text-gray-800">
                                            <asp:Label ID="lblpendingorders" runat="server" Text="">0</asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-auto">
                                        <i class="fas fa-comments fa-2x text-gray-300"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Content Row -->
                <div class="row">
                    <!-- Area Chart -->
                    <div class="col-xl-8 col-lg-7">
                        <div class="card shadow mb-4">
                            <!-- Card Header - Dropdown -->
                            <div class="card-header py-3 d-flex flex-row align-items-center justify-content-between">
                                <h6 class="m-0 font-weight-bold text-primary">Offer Provided</h6>
                                <div class="dropdown no-arrow">
                                    <a class="dropdown-toggle" href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown"
                                        aria-haspopup="true" aria-expanded="false"><i class="fas fa-ellipsis-v fa-sm fa-fw text-gray-400"></i></a>
                                    <div class="dropdown-menu dropdown-menu-right shadow animated--fade-in" aria-labelledby="dropdownMenuLink">
                                        <div class="dropdown-header">
                                            Dropdown Header:
                                        </div>
                                        <a class="dropdown-item" href="#">Action</a> <a class="dropdown-item" href="#">Another
                                                action</a>
                                        <div class="dropdown-divider">
                                        </div>
                                        <a class="dropdown-item" href="#">Something else here</a>
                                    </div>
                                </div>
                            </div>
                            <!-- Card Body -->
                            <div class="card-body">
                                <div class="chart-area">
                                    <div class="chartjs-size-monitor">
                                        <div class="chartjs-size-monitor-expand">
                                            <div class="">
                                            </div>
                                        </div>
                                        <div class="chartjs-size-monitor-shrink">
                                            <div class="">
                                            </div>
                                        </div>
                                    </div>
                                    <canvas id="myAreaChart" style="display: block; height: 320px; width: 782px;" width="977"
                                        height="400" class="chartjs-render-monitor"></canvas>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- Pie Chart -->
                    <div class="col-xl-4 col-lg-5">
                        <div class="card shadow mb-4">
                            <!-- Card Header - Dropdown -->
                            <div class="card-header py-3 d-flex flex-row align-items-center justify-content-between">
                                <h6 class="m-0 font-weight-bold text-primary">Enquiry Location</h6>
                                <div class="dropdown no-arrow">
                                    <a class="dropdown-toggle" href="#" role="button" id="dropdownMenuLink" data-toggle="dropdown"
                                        aria-haspopup="true" aria-expanded="false"><i class="fas fa-ellipsis-v fa-sm fa-fw text-gray-400"></i></a>
                                    <div class="dropdown-menu dropdown-menu-right shadow animated--fade-in" aria-labelledby="dropdownMenuLink">
                                        <div class="dropdown-header">
                                            Dropdown Header:
                                        </div>
                                        <a class="dropdown-item" href="#">Action</a> <a class="dropdown-item" href="#">Another
                                                action</a>
                                        <div class="dropdown-divider">
                                        </div>
                                        <a class="dropdown-item" href="#">Something else here</a>
                                    </div>
                                </div>
                            </div>
                            <!-- Card Body -->
                            <div class="card-body">
                                <div class="chart-pie pt-4 pb-2">
                                    <div class="chartjs-size-monitor">
                                        <div class="chartjs-size-monitor-expand">
                                            <div class="">
                                            </div>
                                        </div>
                                        <div class="chartjs-size-monitor-shrink">
                                            <div class="">
                                            </div>
                                        </div>
                                    </div>
                                    <canvas id="myPieChart" width="447" height="306" class="chartjs-render-monitor" style="display: block; height: 245px; width: 358px;"></canvas>
                                </div>
                                <%--<div class="mt-8 text-center small">
                                        <span class="mr-2"><i class="fas fa-circle text-primary"></i>Domestic</span>
                                        <span class="mr-2"><i class="fas fa-circle text-success"></i>International</span>
                                    </div>--%>
                            </div>
                            <asp:HiddenField ID="hdnjsonstring" runat="server" Value="0" />
                            <asp:HiddenField ID="hdnmonthjsonstring" runat="server" Value="0" />
                        </div>
                    </div>
                </div>
                <!-- Content Row -->
                <div class="row">
                    <%-- <div class="col-lg-6 mb-4">

                            <!-- Illustrations -->
                            <div class="card shadow mb-4">
                                <div class="">
                <div class="tiles blue added-margin" style="
    /* color: black; */
">
                    <div class="heading">
                        <span>Alerts &amp; Tasks</span>
                    </div>
                    <div class="dashbrdbody tiles-body" style="max-height: 135px !important;min-height: 112px;">
                         <div class="description" style="width:20%;">
                          <div class="descrptncontent">
                                 <button type="button" class="collapsible">
                                     <span id="ContentPlaceHolder1_lab_aprvd">Sales Staff Allocation - 5</span></button><br>
                                 <div>

</div>
                             </div>
                            <div class="descrptncontent">
                                <button type="button" class="collapsible">
                                    <span id="ContentPlaceHolder1_lab_aprvd">Offer Pending for Price Approval - 4
</span></button><br>
                                <div>

</div>
                            </div>
                        </div> 
                    </div>
                </div>
            </div>
                                
                            </div>

                            <!-- Approach -->


                        </div>--%>
                    <!-- Content Column -->
                    <div class="col-lg-12 mb-4">
                        <!-- Project Card Example -->
                        <div class="card shadow mb-4">
                            <div class="card-header py-3">
                                <h6 class="m-0 font-weight-bold text-primary">PRODUCTION STATUS</h6>
                            </div>
                            <div class="card-body">
                                <h4 class="small font-weight-bold">Dispatched<span class="float-right">20%</span></h4>
                                <div class="progress mb-4">
                                    <div class="progress-bar bg-danger" role="progressbar" style="width: 20%" aria-valuenow="20"
                                        aria-valuemin="0" aria-valuemax="100">
                                    </div>
                                </div>
                                <h4 class="small font-weight-bold">Assembled<span class="float-right">40%</span></h4>
                                <div class="progress mb-4">
                                    <div class="progress-bar bg-warning" role="progressbar" style="width: 40%" aria-valuenow="40"
                                        aria-valuemin="0" aria-valuemax="100">
                                    </div>
                                </div>
                                <h4 class="small font-weight-bold">Secondary Job Card Issued<span class="float-right">60%</span></h4>
                                <div class="progress mb-4">
                                    <div class="progress-bar" role="progressbar" style="width: 60%" aria-valuenow="60"
                                        aria-valuemin="0" aria-valuemax="100">
                                    </div>
                                </div>
                                <h4 class="small font-weight-bold">Primary Job Card Issued<span class="float-right">80%</span></h4>
                                <div class="progress mb-4">
                                    <div class="progress-bar bg-info" role="progressbar" style="width: 80%" aria-valuenow="80"
                                        aria-valuemin="0" aria-valuemax="100">
                                    </div>
                                </div>
                                <h4 class="small font-weight-bold">Material Planned<span class="float-right">Complete!</span></h4>
                                <div class="progress">
                                    <div class="progress-bar bg-success" role="progressbar" style="width: 100%" aria-valuenow="100"
                                        aria-valuemin="0" aria-valuemax="100">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- Color System -->
                    </div>
                </div>
            </div>
        </div>
    </div>


    <script type="text/javascript">
        $("#btnheader").click(function () {

            $(".header-seperation").toggle();
            $(".page-sidebar").toggleClass("mini");
            $(".page-content").toggleClass("condensed").css();
        });
        function CloseWindow() {
            document.getElementById("btnLogOut").click();
            //            window.open("login.aspx", "_self");
        }
    </script>
    <link rel="stylesheet" type="text/css" href="../Assets/css/sb-admin-2.min.css" />
</asp:Content>
