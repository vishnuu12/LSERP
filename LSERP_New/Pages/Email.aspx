<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="Email.aspx.cs" Inherits="Pages_Email" ClientIDMode="Predictable" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="../Assets/css/bootstrap-multiselect.css" />
    <script type="text/javascript" src="../Assets/scripts/bootstrap-multiselect.js"></script>
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
        .padLeft
        {
            padding-left: 30px;
        }
    </style>
    <script type="text/javascript">

       function Dep() {
            debugger;
            $('[id*=lstDepartments]').multiselect({
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
        }
        window.onbeforeunload = DisableButton;

        $(function () {
            modalPosition(); $(window).resize(function () {
                modalPosition();
            });

            $('.openModalDept').click(function (e) {
                $('.genmodal, .genmodal-backdrop').fadeIn('fast');
                $("#divDepart").show();
                $("#divBoard").hide();
                $("#divTrans").hide();
                e.preventDefault();
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
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('.openModalDept').click(function (e) {
                        $('.genmodal, .genmodal-backdrop').fadeIn('fast');
                        $("#divDepart").show();
                        $("#divBoard").hide();
                        $("#divTrans").hide();
                        e.preventDefault();
                    });
                }
            });
        };

        function CheckValidate() {
            var msg = "0";
            var rbtnMode = document.getElementById("<%=rbtnMode.ClientID %>")
            var rbtnModevalue = rbtnMode.getElementsByTagName("input")
            for (var i = 0; i < rbtnModevalue.length; i++) {
                if ((rbtnModevalue[i].checked) && (rbtnModevalue[i].value == "1")) {
                 
                    var txtEmail = document.getElementById("<%=txtEmail.ClientID %>").value;
                    var txtSubject = document.getElementById("<%=txtSubject.ClientID %>").value;
                    var txtMessage = document.getElementById("<%=txtMessage.ClientID %>").value;
                 
                    if (txtEmail == "") {
                        $('#<%=txtEmail.ClientID %>').notify('Field Required.', { arrowShow: true, position: 't,r', autoHide: true });
                        msg = "1";
                    }
                    if (txtSubject == "") {
                        $('#<%=txtSubject.ClientID %>').notify('Field Required.', { arrowShow: true, position: 't,r', autoHide: true });
                        msg = "1";
                    }
                    if (txtMessage == "") {
                        $('#<%=txtMessage.ClientID %>').notify('Field Required.', { arrowShow: true, position: 't,r', autoHide: true });
                        msg = "1";
                    }
                }
                else if ((rbtnModevalue[i].checked) && (rbtnModevalue[i].value == "2")) {
                    var txtSubject = document.getElementById("<%=txtSubject.ClientID %>").value;
                    var txtMessage = document.getElementById("<%=txtMessage.ClientID %>").value;
                    var rbEmailgroup = document.getElementById("<%=rbEmailgroup1.ClientID %>")
                    var rbtgroup = rbEmailgroup.getElementsByTagName("input")
                    for (var i = 0; i < rbtgroup.length; i++) {
                        <%--if ((rbtgroup[i].checked) && (rbtgroup[i].value == "3")) {
                            if (document.getElementById("<%=lstBatchs.ClientID %>").value == "") {
                                $('#<%=lstBatchs.ClientID %>').notify('Select Any Value.', { arrowShow: true, position: 'r,r', autoHide: true });
                                msg = "1";
                            }
                        }
                        else--%>
                       if ((rbtgroup[i].checked) && (rbtgroup[i].value == "1")) {
                            if (document.getElementById("<%=lstDepartments.ClientID %>").value == "") {
                                $('#<%=lstDepartments.ClientID %>').notify('Select Any Value.', { arrowShow: true, position: 'r,r', autoHide: true });
                                msg = "1";
                            }
                        }
                        <%--else if ((rbtgroup[i].checked) && (rbtgroup[i].value == "5")) {
                            if (document.getElementById("<%=lstCourse.ClientID %>").value == "") {
                                $('#<%=lstCourse.ClientID %>').notify('Select Any Value.', { arrowShow: true, position: 'r,r', autoHide: true });
                                msg = "1";
                            }
                        }--%>
                        else if ((rbtgroup[i].checked) && (rbtgroup[i].value == "2")) {
                            if (document.getElementById("<%=lstmygroup.ClientID %>").value == "") {
                                $('#<%=lstmygroup.ClientID %>').notify('Select Any Value.', { arrowShow: true, position: 'r,r', autoHide: true });
                                msg = "1";
                            }
                        }
                    }
                    if (txtSubject == "") {
                        $('#<%=txtSubject.ClientID %>').notify('Field Required.', { arrowShow: true, position: 't,r', autoHide: true });
                        msg = "1";
                    }
                    if (txtMessage == "") {
                        $('#<%=txtMessage.ClientID %>').notify('Field Required.', { arrowShow: true, position: 't,r', autoHide: true });
                        msg = "1";
                    }                                                            

            if (msg == "0") {              
                return true;
            }
            else
                return false;
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
                                        <img src="../Assets/images/pages.png" alt="" width="16px" />
                                    </span>
                                    <h3 class="page-title-head d-inline-block">
                                        Email Alerts</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>                                                 
                                                    <li class="active breadcrumb-item" aria-current="page">Email Alerts</li>
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
                    <asp:PostBackTrigger ControlID="btnSubmit" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div class="col-lg-12 col-md-12 col-sm-12 no-boarder padLeft">
                            <div class="form-group">
                                <label class="form-label">
                                    Mode
                                </label>
                                <div class="">
                                    <asp:RadioButtonList ID="rbtnMode" Width="300px" runat="server" CssClass="radio radio-success"
                                        OnSelectedIndexChanged="rbtnMode_OnSelectedIndexChanged" AutoPostBack="true"
                                        RepeatDirection="Horizontal" RepeatLayout="Flow" TabIndex="4">
                                        <asp:ListItem Selected="True" Value="1">Individual</asp:ListItem>
                                        <asp:ListItem Value="2">Group
                                        </asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                        </div>
                        <div id="divDisplayto" runat="server" visible="false" class="col-lg-12 col-md-12 col-sm-12 no-boarder"
                            style="padding-left: 28px; padding-bottom: 15px;">
                            <div class="form-group" style="height: 70px !important;">
                                <label class="form-label">
                                    Send To *
                                </label>
                                <div class="">
                                    <div style="width: 100%;">
                                        <div>
                                            <div>
                                                <asp:RadioButtonList ID="rbEmailgroup1" Width="500px" runat="server" CssClass="radio radio-success"
                                                    OnSelectedIndexChanged="rbtnSendto_SelectedIndexChanged" AutoPostBack="true"
                                                    RepeatDirection="Horizontal" RepeatLayout="Flow" TabIndex="4">
                                                    <asp:ListItem Selected="True" Value="1">Departments</asp:ListItem>
                                                    <asp:ListItem Value="2">Mygroups</asp:ListItem>
                                                </asp:RadioButtonList>
                                                </td>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group" id="DivDep" runat="server" style="display: none">
                                    <label class="form-label">
                                        Department Names</label>
                                    <div class="col-sm-12" style="margin-left: -15px;">
                                        <asp:ListBox ID="lstDepartments" runat="server" SelectionMode="Multiple"></asp:ListBox>
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
                        </div>
                        <div id="divStaff" runat="server" class="col-lg-6 col-md-6 col-sm-6 no-boarder padLeft">
                            <div class="form-group">
                                <label class="form-label">
                                    Staff / Student ID
                                </label>
                                <div class="">
                                    <div style="width: 100%;">
                                        <div>
                                            <div>
                                                <%--  <asp:TextBox ID="txtID" runat="server" CssClass="form-control" Width="100%" placeholder="Staff / Student Name"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" BehaviorID="AutoCompleteEx"
                                                    TargetControlID="txtID" MinimumPrefixLength="1" EnableCaching="true" CompletionSetCount="1"
                                                    CompletionInterval="500" CompletionListCssClass="autocomplete_completionListElement"
                                                    OnClientPopulating="ShowProcessImage" OnClientPopulated="HideProcessImage" CompletionListItemCssClass="autocomplete_listItem"
                                                    CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="SelectedValue"
                                                    ServiceMethod="GetStaff" ServicePath="Email.aspx">
                                                </cc1:AutoCompleteExtender>
                                                <%--OnClick="btnGet_click"--%>
                                                <%-- <asp:Button ID="btnGet" runat="server" CssClass="button" Text="Load" Style="display: none;" />--%>
                                                <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="form-control" Width="95%"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddlEmployee_OnSelectedIndexChanged"
                                                    ToolTip="Select Employee Name" TabIndex="13">
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
                                    Email ID
                                </label>
                                <div class="">
                                    <div style="width: 100%;">
                                        <div>
                                            <div>
                                                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" MaxLength="200"
                                                    AutoComplete="off" Text=""></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-12 no-boarder padLeft">
                            <div class="form-group">
                                <label class="form-label">
                                    Subject / Header
                                </label>
                                <div class="">
                                    <div style="width: 100%;">
                                        <div>
                                            <div>
                                                <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" MaxLength="200"
                                                    TabIndex="1"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6 no-boarder padLeft">
                            <div class="form-group">
                                <label class="form-label">
                                    Attachment
                                </label>
                                <div>
                                    <asp:FileUpload ID="fuAttach" runat="server" CssClass="validate[required] form-control"
                                        onchange="ShowImagePreview();" />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-12 col-md-12 col-sm-12 no-boarder padLeft" style="height: 150px;">
                            <div class="form-group">
                                <label class="form-label">
                                    Main Message
                                </label>
                                <div class="">
                                    <div style="width: 100%;">
                                        <div>
                                            <div>
                                                <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control" MaxLength="200"
                                                    TabIndex="1" TextMode="MultiLine" Height="100" Width="900"></asp:TextBox>
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
                                <asp:Button Text="Send Mail" CssClass="btn btn-cons btn-success" runat="server" ID="btnSubmit"
                                    OnClick="btnSubmit_Click" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
