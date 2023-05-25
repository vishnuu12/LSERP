<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="Dashboard.aspx.cs" Inherits="Pages_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container main-container_close"> 
       <div class="app-main__outer">
            <div class="app-main__inner">
                <div class="app-page-title col-md-12">
                    <div class="page-title-left page-title-wrapper col-md-6">
                        <div class="page-title-heading">
                            <div>
                                <div class="page-title-head center-elem">
                                    <span class="d-inline-block pr-2">
                                        <%--<i class="fa fa-th-large opacity-6 yellow"></i>--%>
                                        <img src="../Assets/images/pages.png" alt="" width="16px" />
                                    </span>
                                    <h3 class="page-title-head d-inline-block">
                                        Dashboard</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right col-md-6">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Library</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Data</li>
                                                </ol>
                                     </nav>
                        <a id="help" class="pop-rel" style="margin-top: 4px;" onclick="infoPopover()">
                            <div class="help-me">?</div>
                            <%-- <div id="help-popover" class="help-pop">--%>
                            <asp:Label ID="infoText" runat="server" class="inTxt">Write something about yourself.Write something about yourself.Write something about yourself.</asp:Label>
                            <%--</div>--%>
                        </a>
                    </div>
                </div>
            </div>
        </div>
       <div class="col-md-12 padding-15">
       <div class="col-md-6 col-sm-6 col-xs-12 ">
         <div class="white-bg std_b">
             <h5 class="h5-class bg1">Key Performance Indicators</h5>
             <%--<ul class="ul-style">
             <li>Collection for the day : Rs. 0</li>
             <li>Payment for the day : Rs. 0</li>
             <li>Current balance ( Cash + Bank) : Rs. 0</li>
             <li>Approved bills for payment : Rs. 0</li>
             </ul>--%>
             <p class="ul-style">Exam to be scheduled for B.Arch III Year.<br /> Excursion tour for MBA II year</p>
            </div>
        </div>
       <div class="col-md-6 col-sm-6 col-xs-12">
       <div class="white-bg std_g">
         <h5 class="h5-class bg2">Important Tasks / Activities</h5>
            <p class="ul-style">Annual Day planning meet on 27th of November, 4.00 pm.<br /> Admissions Committee meeting to be fixed</p>
         <%-- <ul class="ul-style">
            <li>Collection for the day : Rs. 0</li>
            <li>Payment for the day : Rs. 0</li>
            <li>Current balance ( Cash + Bank) : Rs. 0</li>
            <li>Approved bills for payment : Rs. 0</li>
        </ul>--%>
       </div>
        </div>
       </div>
       <div class="col-md-12 padding-15">
       <div class="col-md-6 col-sm-6 col-xs-12">
         <div class="white-bg std_r">
             <h5 class="h5-class bg3">Key Performance Indicators</h5>
             <%--<ul class="ul-style">
             <li>Collection for the day : Rs. 0</li>
             <li>Payment for the day : Rs. 0</li>
             <li>Current balance ( Cash + Bank) : Rs. 0</li>
             <li>Approved bills for payment : Rs. 0</li>
             </ul>--%>
             <p class="ul-style">Exam to be scheduled for B.Arch III Year.<br /> Excursion tour for MBA II year</p>
            </div>
        </div>
       <div class="col-md-6 col-sm-6 col-xs-12">
       <div class="white-bg std_v">
         <h5 class="h5-class bg4">Important Tasks / Activities</h5>
            <p class="ul-style">Annual Day planning meet on 27th of November, 4.00 pm.<br /> Admissions Committee meeting to be fixed</p>
         <%-- <ul class="ul-style">
            <li>Collection for the day : Rs. 0</li>
            <li>Payment for the day : Rs. 0</li>
            <li>Current balance ( Cash + Bank) : Rs. 0</li>
            <li>Approved bills for payment : Rs. 0</li>
        </ul>--%>
    
        </div>
        </div>
       </div>
    </div>
    <!--CORE-->
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.2.1/js/bootstrap.bundle.min.js"
        crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/metismenu"></script>
    <script src="../assets/scripts/scripts-init/app.js"></script>
    <script src="../assets/scripts/scripts-init/demo.js"></script>
    <!--CHARTS-->
    <!--Apex Charts-->
    <script src="https://code.jquery.com/jquery-3.3.1.min.js" integrity="sha256-FgpCb/KJQlLNfOu91ta32o/NMZxltwRo8QtmkMRdAu8="
        crossorigin="anonymous"></script>
    <script src="../assets/scripts/vendors/charts/apex-charts.js"></script>
    <script src="../assets/scripts/scripts-init/charts/apex-charts.js"></script>
    <script src="../assets/scripts/scripts-init/charts/apex-series.js"></script>
    <!--Sparklines-->
    <script src="../assets/scripts/vendors/charts/charts-sparklines.js"></script>
    <script src="../assets/scripts/scripts-init/charts/charts-sparklines.js"></script>
    <!--Chart.js-->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.4.0/Chart.min.js"></script>
    <script src="../assets/scripts/scripts-init/charts/chartsjs-utils.js"></script>
    <script src="../assets/scripts/scripts-init/charts/chartjs.js"></script>
    <!--FORMS-->
</asp:Content>
