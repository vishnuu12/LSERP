<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="fatigue.aspx.cs"
    Inherits="Pages_fatigue" ClientIDMode="Predictable" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function clearfields() {
            $('#ContentPlaceHolder1_lblfatiguelifecycle').text('');
        }

        function ResetValues() {
            $('#ContentPlaceHolder1_txtTemparature').val('');
            $('#ContentPlaceHolder1_txtstress').val('');
            $('#ContentPlaceHolder1_lblfatiguelifecycle').text('');
            const ddl = document.querySelector('#ContentPlaceHolder1_ddlMOC')
            ddl.selectedIndex = 0;
            document.querySelector('.chosen-single > span').innerText = '--Select--'
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
                                        <%--<i class="fa fa-th-large opacity-6 yellow"></i>--%>
                                        <img src="../Assets/images/pages.png" alt="" width="16px" />
                                    </span>
                                    <h3 class="page-title-head d-inline-block">Fatigue Life Calculation Of Bellows </h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Sales</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Fatigue</li>
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
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" runat="server">
                            <div class="ip-div text-center">
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            MOC 
                                        </label>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:DropDownList ID="ddlMOC" runat="server" onchange="clearfields();"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlMOC_OnSelectIndexChanged" CssClass="form-control mandatoryfield"
                                            Width="70%" ToolTip="Select MOC">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:Label ID="lbltemparaturerange" style="font-weight:bold;color:red;font-size:20px;" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Temparature  
                                        </label>

                                    </div>
                                    <div class="col-sm-2">
                                        <asp:TextBox ID="txtTemparature" onkeyup="clearfields();" onkeypress="return validationDecimal(this);" CssClass="form-control mandatoryfield" runat="server">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-6 text-left p-t-10">
                                        <span style="color: brown;">( Deg.C ) </span>
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Stress  
                                        </label>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:TextBox ID="txtstress" onkeyup="clearfields();" onkeypress="return validationDecimal(this);" CssClass="form-control mandatoryfield" runat="server">
                                        </asp:TextBox>
                                    </div>
                                    <div class="col-sm-6 text-left p-t-10">
                                        <span style="color: brown;">( MPa ) </span>
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Fatigue Life Cycles   
                                        </label>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Label ID="lblfatiguelifecycle" Style="font-weight: bold; font-size: 20px; color: #000;"
                                            runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-6 text-left p-t-10">
                                        <span style="color: brown;">( Cycles ) </span>
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-20">
                                    <asp:LinkButton ID="btncalculate" runat="server"
                                        OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divInput');"
                                        OnClick="btncalculate_Click" Text="Calculate" CssClass="btn btn-cons btn-save AlignTop"></asp:LinkButton>
                                    <asp:LinkButton ID="btnreset" runat="server" Text="Reset" OnClientClick="return ResetValues();" CssClass="btn btn-cons btn-danger AlignTop"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10 p-l-0 p-r-0">
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

