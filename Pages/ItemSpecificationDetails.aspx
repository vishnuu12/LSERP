<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="ItemSpecificationDetails.aspx.cs"
    Inherits="Pages_ItemSpecificationDetails" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function OpenTab(Name) {
          <%--  var names = ["Design", "Drafting"];
            //, "FEA", "Quality", "Refractory", "Heattreatment", "Painting"
            var text = document.getElementById('<%=Design.ClientID %>');            
            for (var i = 0; i < names.length; i++) {
                if (Name == names[i]) {
                    var a = text.id.replace('Design', Name);
                    document.getElementById(a).style.display = "block";
                    document.getElementById('li' + names[i]).className = "active";
                }
                else {
                    var b = text.id.replace('Design', names[i]);
                    document.getElementById(b).style.display = "none";
                    document.getElementById('li' + names[i]).className = "";
                    //document.getElementById('li' + names[i]).className = "fas fa-check-circle";
                }
            }--%>
            if ($(event.target).closest('li').is('.active') == false) {
                $('#ContentPlaceHolder1_hdnTabName').val(Name);
                document.getElementById('<%=btntab.ClientID %>').click();
            }
            else
                return false;
        }

        function ActiveTab(Name) {
            var names = ["Design", "Drafting", "FEA", "Quality", "Refractory", "Heattreatment", "Painting"];
            for (var i = 0; i < names.length; i++) {
                if (Name == names[i]) {
                    document.getElementById('ContentPlaceHolder1_li' + names[i]).className = "active";
                }
                else {
                    document.getElementById('ContentPlaceHolder1_li' + names[i]).className = "";
                    //document.getElementById('li' + names[i]).className = "fas fa-check-circle";
                }
            }
        }

        function Validate() {
            var len = $('#ContentPlaceHolder1_gvItemnamelist').find('input:checked').length;
            var msg = Mandatorycheck('divInput');
            if (msg) {
                if (len > 0) {
                    return true;
                }
                else {
                    hideLoader();
                    ErrorMessage('Error', 'Currently No Item Selected');
                    return false;
                }
            }
            else {
                return false;
            }
        }

        function ShareFile() {
            swal({
                title: "Are you sure?",
                text: "If Yes, Process will be Shared",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Share it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('ShareFile', "");
            });
            return false;
        }

    </script>
    <style type="text/css">
        .grid-title-new {
            color: #FFFFFF !important;
            background-color: #098E83;
            margin-bottom: 0px;
            border-bottom: 0px;
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
                                    <h3 class="page-title-head d-inline-block">Item Specification Details</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Design</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Item Specification Details</li>
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
            <asp:UpdatePanel ID="upStaffAssignment" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" class="ip-div text-center input_section" runat="server" visible="false">
                            <div class="col-sm-12">
                                <div class="col-sm-4 text-right">
                                    <label class="form-label">
                                        Customer Name</label>
                                </div>
                                <div class="col-sm-6 text-left">
                                    <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="true" CssClass="form-control"
                                        OnSelectedIndexChanged="ddlCustomerName_SelectIndexChanged" Width="70%" ToolTip="Select Enquiry Number">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                </div>
                            </div>
                            <div class="col-sm-12 p-t-10">
                                <div class="col-sm-4 text-right">
                                    <label class="form-label">
                                        Enquiry Number</label>
                                </div>
                                <div class="col-sm-6 text-left">
                                    <asp:DropDownList ID="ddlEnquiryNumber" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEnquiryNumber_SelectIndexChanged"
                                        CssClass="form-control" Width="70%" ToolTip="Select Enquiry Number">
                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                </div>
                            </div>
                        </div>

                        <div id="divOutput" class="output_section" runat="server" visible="false">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="col-md-12 row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div id="Tabs" runat="server">
                                                <ul id="Ul1" class="nav nav-tabs grid-title-new no-boarder" runat="server">
                                                    <li id="liDesign" runat="server"><a href="#Design" class="tab-content"
                                                        data-toggle="tab" onclick="OpenTab('Design');">
                                                        <p style="margin-left: 10px">
                                                            Design
                                                        </p>
                                                    </a></li>
                                                    <li id="liDrafting" runat="server"><a href="#Drafting" class="tab-content" data-toggle="tab" onclick="OpenTab('Drafting');">
                                                        <p style="margin-left: 10px">
                                                            Drafting
                                                        </p>
                                                    </a></li>
                                                    <li id="liFEA" runat="server"><a href="#FEA" class="tab-content" onclick="OpenTab('FEA');">
                                                        <p style="margin-left: 10px">
                                                            FEA
                                                        </p>
                                                    </a></li>
                                                    <li id="liQuality" runat="server"><a href="#Quality" class="tab-content"
                                                        onclick="OpenTab('Quality');">
                                                        <p style="margin-left: 10px">
                                                            Quality
                                                        </p>
                                                    </a></li>
                                                    <li id="liRefractory" runat="server"><a href="#Refractory" class="tab-content"
                                                        onclick="OpenTab('Refractory');">
                                                        <p style="margin-left: 10px">
                                                            Refractory
                                                        </p>
                                                    </a></li>
                                                    <li id="liHeattreatment" runat="server"><a href="#Heattreatment" class="tab-content"
                                                        onclick="OpenTab('Heattreatment');">
                                                        <p style="margin-left: 10px">
                                                            Heat treatment
                                                        </p>
                                                    </a></li>
                                                    <li id="liPainting" runat="server"><a href="#Painting" class="tab-content" onclick="OpenTab('Painting');">
                                                        <p style="margin-left: 10px">
                                                            Painting
                                                        </p>
                                                    </a></li>
                                                </ul>
                                                <div class="tab-content" style="padding: 10px 10px 10px 10px;">
                                                    <div class="tab-pane active" id="Design" runat="server">
                                                        <div class="col-sm-12 p-t-10">
                                                            <asp:GridView ID="gvItemnamelist" runat="server" AutoGenerateColumns="False"
                                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found"
                                                                CssClass="table table-hover table-bordered medium" DataKeyNames="EDID">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="" ControlStyle-CssClass="nosort" ItemStyle-HorizontalAlign="Center">
                                                                        <HeaderTemplate>
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkitems" runat="server" AutoPostBack="false" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Item Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblitemname" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblQuantity" runat="server" Style="color: brown;" Text='<%# Eval("Quantity")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                        <div id="divInput">
                                                            <div class="row p-t-10" style="padding-left: 30%;">
                                                                <asp:FileUpload ID="fpAttach" runat="server" CssClass="form-control mandatoryfield" Style="width: 50%;"
                                                                    onchange="DocValidation(this);" />
                                                            </div>
                                                        </div>
                                                        <div class="row p-t-10" style="padding-left: 40%">
                                                            <asp:LinkButton ID="btnSave" runat="server" Text="Save"
                                                                OnClick="btnSave_Click" OnClientClick="return Validate('ContentPlaceHolder1_divInput');" CssClass="btn btn-cons btn-success"></asp:LinkButton>
                                                        </div>
                                                        <div class="col-sm-12 p-t-10">
                                                            <asp:GridView ID="gvItemSpecDetails" runat="server" AutoGenerateColumns="False"
                                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found"
                                                                CssClass="table table-hover table-bordered medium" DataKeyNames="EDID">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Item Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblitemname" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="File Name">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="btnFileName" runat="server" Text="design"
                                                                                OnClick="btnSave_Click"><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                        <div class="row p-t-10" style="padding-left: 40%">
                                                            <asp:LinkButton ID="btnShareFile" runat="server" Text="Share File" OnClientClick="return ShareFile();"
                                                                CssClass="btn btn-cons btn-success"></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div style="display: none;">
                        <asp:LinkButton ID="btntab" runat="server" Text="drafting"
                            OnClick="btntab_Click" CssClass="btn btn-cons btn-success"></asp:LinkButton>
                    </div>

                    <asp:HiddenField ID="hdnTabName" runat="server" Value="Design" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

