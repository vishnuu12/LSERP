<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="SMS.aspx.cs" Inherits="Pages_SMS" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="../assets/css/bootstrap-multiselect.css" />
    <script type="text/javascript" src="../assets/scripts/bootstrap-multiselect.js"></script>
    <style type="text/css">
        .multiselect-container
        {
            width: 500px !important;
            overflow-x: auto !important;
        }
        .multiselect-container > li
        {
            margin-left: -15px;
        }
    </style>
    <script type="text/javascript">

        function Dep() {
            $('[id*=lstDepartments]').multiselect({
                includeSelectAllOption: true,
                maxHeight: 150,
                buttonWidth: 500
            });
            $('[id*=lstBatchs]').multiselect({
                includeSelectAllOption: true,
                maxHeight: 150,
                buttonWidth: 500
            });
            $('[id*=lstCourse]').multiselect({
                includeSelectAllOption: true,
                maxHeight: 150,
                buttonWidth: 500
            });
            $('[id*=lstmygroup]').multiselect({
                includeSelectAllOption: true,
                maxHeight: 150,
                buttonWidth: 500
            });
        }

        function CheckValidate() {
            var msg = "0";
            var rbtnMode = document.getElementById("<%=rbtnMode.ClientID %>")
            var rbtnModevalue = rbtnMode.getElementsByTagName("input")
            for (var i = 0; i < rbtnModevalue.length; i++) {
                if ((rbtnModevalue[i].checked) && (rbtnModevalue[i].value == "1")) {

                    var txtMobile = document.getElementById("<%=txtMobile.ClientID %>").value;
                    var txtMessage = document.getElementById("<%=txtMessage.ClientID %>").value;

                    if (txtMobile == "") {
                        $('#<%=txtMobile.ClientID %>').notify('Field Required.', { arrowShow: true, position: 't,r', autoHide: true });
                        msg = "1";
                    }
                    if (txtMessage == "") {
                        $('#<%=txtMessage.ClientID %>').notify('Field Required.', { arrowShow: true, position: 't,r', autoHide: true });
                        msg = "1";
                    }
                }
                else if ((rbtnModevalue[i].checked) && (rbtnModevalue[i].value == "2")) {
                    var txtMessage = document.getElementById("<%=txtMessage.ClientID %>").value;
                    var rbEmailgroup = document.getElementById("<%=rbEmailgroup1.ClientID %>")
                    var rbtgroup = rbEmailgroup.getElementsByTagName("input")
                    for (var i = 0; i < rbtgroup.length; i++) {

                        if ((rbtgroup[i].checked) && (rbtgroup[i].value == "4")) {
                            if (document.getElementById("<%=lstDepartments.ClientID %>").value == "") {
                                $('#<%=lstDepartments.ClientID %>').notify('Select Any Value.', { arrowShow: true, position: 'r,r', autoHide: true });
                                msg = "1";
                            }
                        }

                        else if ((rbtgroup[i].checked) && (rbtgroup[i].value == "9")) {
                            if (document.getElementById("<%=lstmygroup.ClientID %>").value == "") {
                                $('#<%=lstmygroup.ClientID %>').notify('Select Any Value.', { arrowShow: true, position: 'r,r', autoHide: true });
                                msg = "1";
                            }
                        }
                    }
                    if (txtMessage == "") {
                        $('#<%=txtMessage.ClientID %>').notify('Field Required.', { arrowShow: true, position: 't,r', autoHide: true });
                        msg = "1";
                    }
                }
            }

            if (msg == "0") {
                return true;
            }
            else {
                return false;
            }
        }

    </script>
    <%-- <script type="text/javascript" src="../assets/js/jquery-1.9.1.min.js"></script>--%>
    <script type="text/javascript">

        function ShowLoader() {
            var retVal = confirm("Are you sure you want to continue ?");
            if (retVal == true) {
                ShowLoader();
                return true;
            } else {
                return false;
            }
        }

        function DisableButton() {
            document.getElementById("<%=btnSubmit.ClientID %>").disabled = true;
        } window.onbeforeunload = DisableButton; </script>
    <script type="text/javascript">

        function openModelDpt() {
            $('.genmodal, .genmodal-backdrop').fadeIn('fast');
            $("#divDepart").show();
            $("#divBoard").hide();
            $("#divTrans").hide();
            e.preventDefault();
            $("#rbtnBoarding :input").removeAttr('checked');
        }

        function openModelBoard() {
            $('.genmodal, .genmodal-backdrop').fadeIn('fast');
            $("#divTrans").hide();
            $("#divDepart").hide();
            $("#divBoard").show();
            e.preventDefault();
        }

        function openModeltrans() {
            $('.genmodal, .genmodal-backdrop').fadeIn('fast');
            $("#divTrans").show();
            $("#divDepart").hide();
            $("#divBoard").hide();
            e.preventDefault();
        }
        function clearSearchKey() {
            clearcontrols();
        }

        $(function () {
            modalPosition(); $(window).resize(function () {
                modalPosition();
            });

            $('.openModalDept').click(function (e) {
                openModelDpt();
            });

            $('.openModalBoard').click(function (e) {
                openModelBoard();
            });

            $('.openModalTrans').click(function (e) {
                openModeltrans();
            });

            $('.genclose-modal').click(function (e) {
                $('.genmodal, .genmodal-backdrop').fadeOut('fast');
            });
        }); function modalPosition() {
            var width = $('.genmodal').width();
            var pageWidth = $(window).width();
            var x = (pageWidth / 3) - (width / 3);
            $('.genmodal').css({ left: x + "px" });
        }

        //        var prm = Sys.WebForms.PageRequestManager.getInstance();
        //        if (prm != null) {
        //            prm.add_endRequest(function (sender, e) {
        //                if (sender._postBackSettings.panelsToUpdate != null) {

        //                    function clearSearchKey() {
        //                        clearcontrols();
        //                    }
        //                    $('.openModalDept').click(function (e) {
        //                        openModelDpt();
        //                    });

        //                    $('.openModalBoard').click(function (e) {
        //                        openModelBoard();
        //                    });

        //                    $('.openModalTrans').click(function (e) {
        //                        openModeltrans();
        //                    });
        //                }
        //            });
        //        };        
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
                                        SMS-Alerts</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>                                                
                                                    <li class="active breadcrumb-item" aria-current="page">SMS</li>
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
            <asp:UpdatePanel ID="upQuote" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="rbtnMode" />
                    <asp:PostBackTrigger ControlID="btnSubmit" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" runat="server">
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-lg-12 col-md-12 col-sm-12 no-boarder" style="padding-left: 25px;">
                                    <div class="form-group">
                                        <label class="form-label">
                                            Mode
                                        </label>
                                        <div class="col-sm-12" style="padding-left: 0px">
                                            <asp:RadioButtonList ID="rbtnMode" runat="server" CssClass="radio radio-success"
                                                OnSelectedIndexChanged="rbtnMode_OnSelectedIndexChanged" RepeatDirection="Horizontal"
                                                AutoPostBack="true" RepeatLayout="Flow" TabIndex="1">
                                                <asp:ListItem Selected="True" Value="1">Individual</asp:ListItem>
                                                <asp:ListItem Value="2">Group</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                                <div id="divDisplayto" runat="server" visible="false" class="col-lg-12 col-md-12 col-sm-12 no-boarder"
                                    style="margin-left: 10px;">
                                    <div class="form-group">
                                        <label class="form-label">
                                            Display To *
                                        </label>
                                        <div class="">
                                            <div style="width: 100%">
                                                <asp:RadioButtonList ID="rbEmailgroup1" runat="server" CssClass="radio radio-success"
                                                    OnSelectedIndexChanged="rbtnSendto_SelectedIndexChanged" RepeatDirection="Horizontal"
                                                    AutoPostBack="true" RepeatColumns="3" TabIndex="4">
                                                    <asp:ListItem Selected="True" Value="1">Departments</asp:ListItem>
                                                    <asp:ListItem Value="2">Mygroups</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group" id="DivDep" runat="server" style="display: none">
                                        <label class="form-label" style="padding-top: 35px">
                                            Department Names</label>
                                        <div class="col-sm-12">
                                            <div id="divDepartmentDropDownList" runat="server" style="width: 100%">
                                                <asp:ListBox ID="lstDepartments" runat="server" SelectionMode="Multiple" Width="100%">
                                                </asp:ListBox>
                                            </div>
                                            <asp:HiddenField ID="hdnDep" runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group" id="divmygroup" runat="server" style="display: none">
                                        <label class="form-label">
                                            Group Names
                                        </label>
                                        <div class="col-sm-12" style="margin-left: -15px;">
                                            <div class="col-sm-5">
                                                <asp:ListBox ID="lstmygroup" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                                <asp:HiddenField ID="hdnmygroup" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="divStaff" runat="server" class="col-lg-6 col-md-6 col-sm-6 no-boarder" style="padding-left: 25px;">
                                    <div class="form-group">
                                        <label class="form-label">
                                            Staff / Student ID
                                        </label>
                                        <div class="">
                                            <div style="width: 100%;">
                                                <div>
                                                    <div>
                                                        <%-- <asp:TextBox ID="txtID" runat="server" CssClass="form-control" Width="100%" placeholder="Staff / Student Name"
                                                            TabIndex="2"></asp:TextBox>
                                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" BehaviorID="AutoCompleteEx"
                                                            TargetControlID="txtID" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1"
                                                            CompletionInterval="500" CompletionListCssClass="autocomplete_completionListElement"
                                                            OnClientPopulating="ShowProcessImage" OnClientPopulated="HideProcessImage" CompletionListItemCssClass="autocomplete_listItem"
                                                            CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="selectedvalue"
                                                            ServiceMethod="GetStaff" ServicePath="~/pages/SMS.aspx">
                                                        </asp:AutoCompleteExtender>--%>
                                                        <%--OnClick="btnGet_click"--%>
                                                        <%--  <asp:Button ID="btnGet" runat="server" CssClass="button" Text="Load" Style="display: none;" />--%>
                                                        <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control" Width="95%"
                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_OnSelectedIndexChanged"
                                                            ToolTip="Select Employee Name">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div id="divcontact" runat="server" class="col-lg-6 col-md-6 col-sm-6 no-boarder">
                                    <div class="form-group">
                                        <label class="form-label">
                                            Mobile Number
                                        </label>
                                        <div class="">
                                            <div style="width: 100%;">
                                                <div>
                                                    <div>
                                                        <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" MaxLength="200"
                                                            PlaceHolder="Enter Mobile Number here!" TabIndex="3" Width="100%"></asp:TextBox>
                                                    </div>
                                                    <div>
                                                        <asp:Label ID="lblMobileerror" ForeColor="red" runat="server" Text=""></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12 col-md-12 col-sm-12 no-boarder" style="padding-left: 25px;
                                padding-top: 30px;">
                                <div class="form-group">
                                    <label class="form-label">
                                        Select Template
                                    </label>
                                    <div class="">
                                        <div style="width: 100%;">
                                            <div>
                                                <div>
                                                    <%--OnSelectedIndexChanged="ddlSmsTemplate_OnSelectedIndexChanged"--%>
                                                    <asp:DropDownList AutoPostBack="true" ID="ddlSmsTemplate" runat="server" CssClass="form-control"
                                                        TabIndex="4">
                                                    </asp:DropDownList>
                                                </div>
                                                <div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-12 col-md-12 col-sm-12 no-boarder" style="height: 150px; padding-left: 25px">
                                <div class="form-group">
                                    <label class="form-label">
                                        Message
                                    </label>
                                    <div class="">
                                        <div style="width: 100%;">
                                            <div>
                                                <div>
                                                    <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control" MaxLength="200"
                                                        PlaceHolder="Enter your Message here!" TabIndex="5" TextMode="MultiLine" Height="100"></asp:TextBox>
                                                </div>
                                                <div>
                                                </div>
                                                <div>
                                                    &nbsp;
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-6 col-md-6 col-sm-6 no-boarder">
                                <div style="float: right;">
                                    <asp:Button Text="Send Message" CssClass="btn btn-cons btn-success" runat="server"
                                        ID="btnSubmit" TabIndex="37" OnClientClick="return CheckValidate();" OnClick="btnSubmit_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
