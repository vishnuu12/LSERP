<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Pages_Default"
    MasterPageFile="~/Master/CommonMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="../Assets/css/datatables2.css" />
    <%-- <link rel="stylesheet" type="text/css" href="../Assets/css/datatables2.min.css" />--%>
    <script type="text/javascript" src="../Assets/scripts/datatables2.js"></script>
    <script src="../Assets/scripts/ScrollableGridPlugin_ASP.NetAJAX_2.0.js" type="text/javascript"></script>
    <%-- <script type="text/javascript" src="../Assets/scripts/datatables2.min.js"></script>--%>
    <link rel="stylesheet" type="text/css" href="" />
    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=gvDefault.ClientID %>').Scrollable({
                ScrollHeight: 300,
                Width:500
            });
        });
    </script>
    <style type="text/css">
        .form-label
        {
            margin-top: 2px;
            float: left;
            font-weight: 400;
            color: #4d4971;
        }
        
        .divgrid
        {
            height: 200px;
            width: 390px;
        }
        .divgrid table
        {
            width: 390px;
        }
        .divgrid table th
        {
            background-color: Green;
            color: #fff;
            width: auto;
        }
        
        .WrappedText
        {
            word-break: break-all;
            word-wrap: break-word;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-container main-container_close">
        <div class="app-main__outer">
            <div class="app-main__inner">
                <div class="app-page-title">
                    <div class="page-title-left page-title-wrapper">
                    </div>
                </div>
            </div>
        </div>
        <div class="page-container" style="float: left; width: 100%">
            <div class="divgrid">
                <asp:GridView ID="gvDefault" runat="server" ShowFooter="true" OnRowDataBound="gvDefault_RowDataBound"
                    Style="width: 370px;" RowStyle-Wrap="true" AlternatingRowStyle-Wrap="true">
                    
                    <FooterStyle BackColor="Green" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
