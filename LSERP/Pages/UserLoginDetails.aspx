<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="UserLoginDetails.aspx.cs" Inherits="UserLoginDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../Assets/scripts/jquery.dataTables.min.js" rel="stylesheet" type="text/css" />
    <script src="../assets/scripts/jquery.dataTables.min.js" type="text/javascript"></script>
    <script src="../assets/scripts/moment.min.js" type="text/javascript"></script>
    <script src="../assets/scripts/datetime-moment.js" type="text/javascript"></script>
    <script type="text/javascript">


        function DataTable() {
            $(document).ready(function () {
                //$.fn.dataTable.moment('DD/MM/YYYY');
                $.fn.dataTable.moment('DD MMM YYYY HH:mm:ss:mmm');
                $('#<%=gvUserLoginDetails.ClientID %>').dataTable();
            });

        }

        $(document).ready(function () {
            $('.datepicker').datepicker({
                format: 'dd/mm/yyyy'
            });
        });

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            function EndRequestHandler(sender, args) {
                $('.datepicker').datepicker({ format: 'dd/mm/yyyy' });
            }
        });
     
    </script>
    <style type="text/css">
        #gvUserLoginDetails span
        {
            display: none;
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
                                    <h3 class="page-title-head d-inline-block">
                                        User Login Details</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Access Control</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">User Login Details</li>
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
                <ContentTemplate>
                    <div id="input-section" style="float:left;">
                    <div class="ip-div col-sm-12 text-center">
                            
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-1">
                                            <label class="form-label">
                                                From Date</label>
                                        </div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker" MaxLength="10"
                                                placeholder="Select From Date" ToolTip="Select From Date"></asp:TextBox></div>
                                        <div class="col-sm-1">
                                        </div>
                                        <div class="col-sm-1">
                                            <label class="form-label">
                                                To Date</label></div>
                                        <div class="col-sm-2">
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker" MaxLength="10"
                                                placeholder="Select From Date" ToolTip="Select To Date"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-cons btn-success" OnClick="btnSubmit_Click"
                                                Text="Submit" ValidationGroup="Report" OnClientClick="return ShowLoader();" />
                                        </div>
                                    
                        </div>
                    </div>
                    <div id="output" class="output_section" style="float:left;">
                     <div class="page-container" style="float: left; width: 100%">
                        <div class="main-card">
                            <div class="card-body">
                            <div class="">
                                    <p class="h-style">Department Details</p>
                                     <div class="f-right">
                                     </div>
                                        <div class="clear"></div>
                                    </div>
                    
                    <div class="col-sm-12 p-t-10 p-l-0 p-r-0 table-container" id="divLoginDetails" runat="server">
                                            <asp:GridView ID="gvUserLoginDetails" class="table table-bordered table-hover no-more-tables"
                                                runat="server" HorizontalAlign="Center" GridLines="None" ShowFooter="false" Width="100%"
                                                AutoGenerateColumns="False" Style="margin: 5px auto;">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Login Id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLoginId" Text='<%# Eval("UserID") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="User Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUsername" Text='<%# Eval("name")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="User">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblusertype" Text='<%# Eval("usertype") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Source">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSource" Text='<%# Eval("Source") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="IP Address">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblipaddress" Text='<%# Eval("ipaddress") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date & Time">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDatetime" Text='<%# Eval("logintime") %>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
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
