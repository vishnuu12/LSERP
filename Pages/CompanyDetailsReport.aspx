<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="CompanyDetailsReport.aspx.cs" Inherits="Pages_CompanyDetailsReport" ClientIDMode="Predictable" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="https://canvasjs.com/assets/script/jquery.canvasjs.min.js"> </script>
    <script type="text/javascript">
        $(document).ready(function () {
            var options = {
                animationEnabled: true,
                theme: "light2", //"light1", "light2", "dark1", "dark2"
                title: {
                    text: "Company Detail Report"
                },
                data: [{
                    type: "funnel",
                    toolTipContent: "<b>{label}</b>: {y} <b>({percentage}%)</b>",
                    indexLabel: "{label} ({percentage}%)",
                    dataPoints: [
                        //{ "y": 1800, "label": "Leads" },
                        //{ "y": 1552, "label": "Initial Communication" },
                        //{ "y": 1320, "label": "Customer Evaluation" },
                        //{ "y": 885, "label": "Negotiation" },
                        //{ "y": 678, "label": "Order Received" },
                        //{ "y": 617, "label": "Payment" }

                        // { "y": 1744, "label": "Total Enquiry" }, { "y": 1103, "label": "Design Start" }, { "y": 155, "label": "Offer Send Customer" }, { "y": 215, "label": "Order Enquiry" }, { "y": 16, "label": "Offer Lost" }, { "y": 206, "label": "RFP Released Enquiry" }
                    ]
                }]
            };

            //options.data[0].dataPoints.push($('#ContentPlaceHolder1_hdndataString').val());

            var obj = JSON.parse($('#ContentPlaceHolder1_hdndataString').val());

            $.each(obj, function (i, item) {
                options.data[0].dataPoints.push(obj[i]);
            });

            calculatePercentage();

            $("#chartContainer").CanvasJSChart(options);

            function calculatePercentage() {
                var dataPoint = options.data[0].dataPoints;
                var total = dataPoint[0].y;
                for (var i = 0; i < dataPoint.length; i++) {
                    if (i == 0) {
                        options.data[0].dataPoints[i].percentage = 100;
                    } else {
                        options.data[0].dataPoints[i].percentage = ((dataPoint[i].y / total) * 100).toFixed(2);
                    }
                }
            }

        });



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
                                    <h3 class="page-title-head d-inline-block">Company Detail Report </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Reports</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Company Detail Report</li>
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
                    <asp:PostBackTrigger ControlID="btnViewDetails" />
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

                                        <div class="col-sm-12" id="chartContainer" style="height: 370px; width: 100%;">
                                        </div>

                                        <div class="col-sm-12">

                                            <%--   <asp:Chart ID="Chart1" runat="server">
                                                    <Series>
                                                        <asp:Series Name="Series1" runat="server" ChartType="Funnel">
                                                            <Points>
                                                            
                                                            </Points>
                                                        </asp:Series>
                                                    </Series>
                                                    <ChartAreas>
                                                        <asp:ChartArea Name="ChartArea1">
                                                        </asp:ChartArea>
                                                    </ChartAreas>
                                                </asp:Chart>--%>

                                            <asp:GridView ID="gvCompanyDetailsReport" runat="server" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Enquiry" HeaderStyle-CssClass="text-center"
                                                        ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("label")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Count" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblValue" runat="server" Text='<%# Eval("y")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                    <div class="col-sm-12 p-t-20 text-center">
                                        <asp:LinkButton ID="btnViewDetails" runat="server" Text="View Summary Report"
                                            OnClick="btnViewDetails_Click"
                                            CssClass="btn btn-cons btn-success" />
                                    </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hdnBOMId" Value="0" runat="server" />
                    <asp:HiddenField ID="hdndataString" Value="" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>

