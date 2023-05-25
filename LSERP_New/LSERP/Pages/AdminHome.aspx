<%@ Page Title="" Language="C#"  MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="AdminHome.aspx.cs" Inherits="AdminHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .modal-dialog
        {
            margin-top: 60px !important;
            height: 50% !important;
        }
        
        .modal-content
        {
            height: 85%;
        }
        .modal-body img
        {
            width: auto !important;
        }
    </style>    
    <script type="text/javascript">
        
        
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
                                    <h3 class="page-title-head d-inline-block">
                                        Home Dashboard</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Dashboard</li>
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
            <div class="col-md-12">
            <div class="col-sm-6 spacing-bottom-sm spacing-bottom spacing spacing-left">
                <div class="tiles blue added-margin">
                    <div class="heading">
                        <span>My Approvals</span>
                    </div>
                    <div class="tiles-body">
                        <div class="description">
                            Designs sent for approval - Designer 2
                            <br />
                            Designs reworked and sent for approval - Designer 4</div>
                    </div>
                </div>
            </div>
            <div class="col-sm-6 spacing-bottom-sm spacing-bottom spacing-right">
                <div class="tiles green added-margin">
                    <div class="heading">
                        <span>Group Approvals</span>
                    </div>
                    <div class="tiles-body">
                        <div class="description">
                            Approved for the month : 14<br />
                            Pending for approval : 2</div>
                    </div>
                </div>
            </div>
            <div class="col-sm-6 spacing-bottom spacing-left">
                <div class="tiles red added-margin">
                    <div class="heading">
                        <span>Task for Week</span>
                    </div>
                    <div class="tiles-body">
                        <div class="description">
                            ---
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-6 spacing-bottom spacing-right">
                <div class="tiles purple added-margin">
                    <div class="heading">
                        <span>Feedbacks</span>
                    </div>
                    <div class="tiles-body">
                        <div class="description">
                            --</div>
                    </div>
                </div>
            </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        $("#hi").click(function () {

            $(".header-seperation").toggle();
            $(".page-sidebar").toggleClass("mini");
            $(".page-content").toggleClass("condensed").css();
        });
        function CloseWindow() {
            document.getElementById("btnLogOut").click();
            //            window.open("login.aspx", "_self");
        }
    </script>
</asp:Content>
