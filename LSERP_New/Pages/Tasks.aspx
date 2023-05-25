<%@ Page Title="" Language="C#" MasterPageFile="~/Master/commonMaster.master" AutoEventWireup="true"
    CodeFile="Tasks.aspx.cs" Inherits="Tasks" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="../Assets/css/font-awesome.min.css" />
    <%--  <script type="text/javascript" src="../Assets/scripts/jquery-ui.js"></script>
    <script type="text/javascript" src="../Assets/scripts/todoFunctions.js"></script>--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.datepicker').datepicker({ dateFormat: 'dd-mm-yyyy' });
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
                                    <h3 class="page-title-head d-inline-block">
                                        Tasks</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>                                                
                                                    <li class="active breadcrumb-item" aria-current="page">Tasks</li>
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
                    <asp:AsyncPostBackTrigger ControlID="btnSubmit" />
                    <asp:AsyncPostBackTrigger ControlID="lvTask" />
                    <asp:AsyncPostBackTrigger ControlID="btnActive" EventName="Click" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" runat="server">
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-lg-12">
                                    <div class="todoWrapper">
                                        <div class="sort-todo">
                                            <a class="all-todo" href="#"></a>
                                            <asp:Button ID="btnAll" CssClass="btn btn-cons btn-success" runat="server" Text="All Tasks"
                                                OnClick="btnAll_Click" />
                                            <asp:Button ID="btnActive" CssClass="btn btn-cons btn-success" runat="server" Text="To be Completed"
                                                OnClick="btnActive_Click" />
                                            <asp:Button ID="btnComplete" CssClass="btn btn-cons btn-success" runat="server" Text="Completed Tasks"
                                                OnClick="btnComplete_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12" style="padding-top: 10px;">
                                    <div class="col-sm-12 add-todo">
                                        <div class="input-group input-group-lg input-primary">
                                            <asp:TextBox ID="txtTask" placeholder="Write  your tasks" class="form-control name-of-todo"
                                                runat="server"></asp:TextBox><span class="input-group-addon border-right" id="basic-addon2"><i
                                                    class="fa fa-pencil"> </i></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-sm-12" style="padding-top: 10px;">
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txtCompletionDate" runat="server" placeholder="Select Completion Date"
                                            ToolTip="Enter Issue Date" CssClass="form-control mandatoryfield datepicker"
                                            MaxLength="300" autocomplete="off"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-8">
                                    </div>
                                </div>
                                <div class="col-sm-12" style="padding-top: 10px; border-bottom: 1px dotted black;
                                    padding-bottom: 10px;">
                                    <div class="col-sm-4">
                                        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Save" CssClass="btn btn-cons btn-success" />
                                        <div class="col-sm-8">
                                        </div>
                                    </div>
                                </div>
                                <div id="divcheckall" runat="server">
                                    <div class="col-sm-12">
                                        <div class="col-sm-5">
                                            <h4>
                                                <span class="semi-bold">Task List</span></h4>
                                        </div>
                                        <div class="col-sm-7">
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="panel">
                                            <div class="panel-body todo">
                                                <ul class="list-group">
                                                    <asp:ListView ID="lvTask" DataKeyNames="TaskID" runat="server">
                                                        <ItemTemplate>
                                                            <li class="list-group-item">
                                                                <div class="check-success">
                                                                    <asp:CheckBox AutoPostBack="true" CssClass="check-success" ToolTip='<%#Eval("Status") %>'
                                                                        OnCheckedChanged="chkTask_OnCheckedChanged" Checked='<%# Convert.ToBoolean(Eval("Status")) %>'
                                                                        ID="chkTask" Text='<%# Eval("TaskName")%>' runat="server" />
                                                                    <asp:HiddenField ID="hiddenID" runat="server" Value='<%#Eval("TaskID") %>' />
                                                                </div>
                                                                <div>
                                                                    <asp:Label ID="lblCompleteionDate" runat="server" Text='<%#Eval("CompletionDate") %>' />
                                                                </div>
                                                                <div class="pull-right action-btns">
                                                                    <asp:ImageButton ID="imgdelete" BorderWidth="0" ImageUrl="../Assets/images/del-ec.png"
                                                                        OnClick="Content_Load" runat="server" />
                                                                </div>
                                                            </li>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="checkbox clear-todo pull-left">
                                            <asp:CheckBox CssClass="taskchkbox" Style="margin-left: 17px;" AutoPostBack="true"
                                                OnCheckedChanged="chkTaskall_OnCheckedChanged" ID="chkTaskall" Text="Mark All As Complete"
                                                runat="server" />
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
