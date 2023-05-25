<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="ProsPect.aspx.cs" EnableEventValidation="false" Inherits="Pages_ProsPect" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="../Assets/css/datatables2.css" />
    <script type="text/javascript" src="../Assets/scripts/datatables2.js"></script>
    <script type="text/javascript">

        function showDataTable() {
            $('#<%=gvProsPect.ClientID %>').DataTable({
                'aoColumnDefs': [{
                    'bSortable': false,
                    'aTargets': [-1] /* 1st one, start by the right */
                }]
            });
        }

        function CheckValues() {

            var msg = true;
            if (document.getElementById("<%=txtProsPectName.ClientID %>").value == "") {
                $('#<%=txtProsPectName.ClientID %>').notify('Field Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }
            if (document.getElementById("<%=txtAddress.ClientID %>").value == "") {
                $('#<%=txtAddress.ClientID %>').notify('Field Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }

            if (document.getElementById("<%=txtPhoneNo.ClientID %>").value == "") {
                $('#<%=txtPhoneNo.ClientID %>').notify('Field Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }

            if (document.getElementById("<%=txtEmail.ClientID %>").value == "") {
                $('#<%=txtEmail.ClientID %>').notify('Field Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }
          <%--  else if (isValidEmail(document.getElementById("<%=txtEmail.ClientID %>").value) == false) {
                //ErrorMessage('Error', 'Please Enter Valid Email');
                document.getElementById("<%=txtEmail.ClientID %>").focus();
                $('#<%=txtEmail.ClientID %>').notify('Please Enter Valid Email.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }--%>

            if (document.getElementById('<%=ddlCountry.ClientID %>').value == "0") {
                $('#<%=ddlCountry.ClientID %>').notify('Fiels Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }
           <%-- if (document.getElementById('<%=ddlState.ClientID %>').value == "0") {
                $('#<%=ddlState.ClientID %>').notify('Fiels Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }

            if (document.getElementById('<%=ddlCity.ClientID %>').value == "0") {
                $('#<%=ddlCity.ClientID %>').notify('Fiels Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }--%>

            if (document.getElementById('<%=txtContactPerson.ClientID %>').value == "") {
                $('#<%=txtContactPerson.ClientID %>').notify('Fiels Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }

            if (document.getElementById('<%=ddlRegion.ClientID %>').selectedIndex == 0) {
                $('#<%=ddlRegion.ClientID %>').notify('Fiels Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }

            if (document.getElementById('<%=ddlSource.ClientID %>').selectedIndex == 0) {
                $('#<%=ddlSource.ClientID %>').notify('Fiels Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }

            if (msg)
                return true;
            else
                return false;
        }

        function ShowInputSection() {
            document.getElementById('<%=divAdd.ClientID %>').style.display = "none";
            document.getElementById('<%=divInput.ClientID %>').style.display = "block";
            document.getElementById('<%=divOutput.ClientID %>').style.display = "none";
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
                                    <h3 class="page-title-head d-inline-block">Prospect Master</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Sales</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Prospect</li>
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
            <asp:UpdatePanel ID="upProsPect" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="imgExcel" />
                    <asp:PostBackTrigger ControlID="imgPdf" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" runat="server">
                            <div class="ip-div text-center">
                                <asp:LinkButton ID="btnAddNew" runat="server" Text="Add New Prospect" OnClientClick="return ShowInputSection();"
                                    CssClass="btn ic-w-btn add-btn"></asp:LinkButton>
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                <div class="col-md-12">
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <asp:Label ID="lblProspectName" runat="server" class="form-label" Text="Prospect Name"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtProsPectName" placeholder="Enter Prospect Name" runat="server"
                                                    TabIndex="1" CssClass="form-control" autocomplete="nope" ToolTip="Enter ProsPect Name"
                                                    MaxLength="300"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <asp:Label ID="lblContactPerson" runat="server" class="form-label" Text="Contact Person"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtContactPerson" runat="server" placeholder="Enter Contact Person"
                                                    CssClass="form-control" ToolTip="Contact Person" TabIndex="2" autocomplete="nope"
                                                    MaxLength="300"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <asp:Label ID="lblEmail" runat="server" class="form-label" Text="Email id"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtEmail" runat="server" placeholder="Enter Email" CssClass="form-control"
                                                    MaxLength="300" ToolTip="Enter Email" TabIndex="3" autocomplete="nope"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <asp:Label ID="lblPhone" runat="server" class="form-label" Text="Phone Number"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtPhoneNo" placeholder="Enter Mobile Number" runat="server" CssClass="form-control"
                                                    onkeypress="return validationNumeric(this);" ToolTip="Enter Mobile Number" autocomplete="nope"
                                                    TabIndex="4" MaxLength="20"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <asp:Label ID="lblAddress" runat="server" class="form-label" Text="Address"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" Height="52px"
                                                    MaxLength="300" TextMode="MultiLine" autocomplete="nope" ToolTip="Enter Address"
                                                    TabIndex="5" placeholder="Enter Address"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <asp:Label ID="Label25" runat="server" class="form-label" Text="Country"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:DropDownList ID="ddlCountry" runat="server" ToolTip="Select Country" TabIndex="6"
                                                    CssClass="form-control" AutoPostBack="true" OnChange="showLoader();" OnSelectedIndexChanged="ddlCountry_IndexChanged">
                                                    <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <asp:Label ID="Label26" runat="server" class="form-label" Text="State"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:DropDownList ID="ddlState" runat="server" ToolTip="Select State" TabIndex="7"
                                                    AutoPostBack="true" CssClass="form-control" OnChange="showLoader();" OnSelectedIndexChanged="ddlState_IndexChanged">
                                                    <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <asp:Label ID="lblCity" runat="server" class="form-label" Text="City"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:DropDownList ID="ddlCity" runat="server" ToolTip="Select City" TabIndex="8"
                                                    CssClass="form-control">
                                                    <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>

                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <asp:Label ID="lblstate" runat="server" class="form-label" Text="State"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtState" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <asp:Label ID="Label2" runat="server" class="form-label" Text="City"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtCity" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>

                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <asp:Label ID="lblRegion" runat="server" class="form-label" Text="Region"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:DropDownList ID="ddlRegion" runat="server" ToolTip="Select Region" TabIndex="8"
                                                    AutoPostBack="false" CssClass="form-control">
                                                    <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <asp:Label ID="lblSource" runat="server" class="form-label" Text="How Sourced"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:DropDownList ID="ddlSource" runat="server" ToolTip="Select Source" TabIndex="9"
                                                    AutoPostBack="false" CssClass="form-control">
                                                    <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Adverdisement/Enquiry"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Referral"></asp:ListItem>
                                                    <asp:ListItem Value="3" Text="Sales Cold Call"></asp:ListItem>
                                                    <asp:ListItem Value="4" Text="Other---"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <asp:Label ID="Label1" runat="server" class="form-label" Text="Fax No"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtFaxNo" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                    <%--OnClientClick="return CusCancel();"--%>
                                    <div class="col-sm-12 p-t-10 text-center m-b-20">
                                        <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-cons btn-save AlignTop"
                                            OnClientClick="return CheckValues();" OnClick="btnSubmit_Click" Text="Submit" />
                                        <asp:LinkButton ID="btnreset" runat="server" CssClass="btn btn-cons btn-danger AlignTop"
                                            OnClick="btnreset_Click" Text="Cancel" />
                                    </div>
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
                                                    <asp:Label ID="lbltitle" runat="server" Text="Prospect Details"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                                <asp:LinkButton ID="btnprint" runat="server" CssClass="print_bg" ToolTip="print" />
                                                <asp:LinkButton ID="imgExcel" runat="server" CssClass="excel_bg" ToolTip="Excel Download"
                                                    OnClick="btnExcelDownload_Click" />
                                                <asp:LinkButton ID="imgPdf" runat="server" CssClass="pdf_bg" ToolTip="PDF Download"
                                                    OnClick="btnPDFDownload_Click" />
                                            </div>
                                        </div>
                                        <div class="col-sm-12" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvProsPect" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover no-more-tables"
                                                Width="100%" ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" OnRowCommand="gvProsPect_RowCommand"
                                                DataKeyNames="ProsPectID,Flag">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Prospect Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCustomerName" runat="server" Text='<%# Eval("ProspectName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Address" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Country Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCountryName" runat="server" Text='<%# Eval("CountryName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="State Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStateName" runat="server" Text='<%# Eval("StateName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="City Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCityName" runat="server" Text='<%# Eval("CityName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Contact Person" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblContactPerson" runat="server" Text='<%# Eval("ContactPerson") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Phone Number" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("PhoneNumber") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Email id" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEMailid" runat="server" Text='<%# Eval("EmailID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Region Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRegionName" runat="server" Text='<%# Eval("RegionName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <%-- <asp:Label Text='<%#Eval("CustomerID") %>' ID="lblCustid" CssClass="" Visible="false"
                                                            runat="server" />--%>
                                                            <asp:LinkButton ID="btnedit" runat="server" Text="Edit" CommandName="EditProsPect"
                                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ToolTip="Edit"><img src="../Assets/images/edit-ec.png" style="height:20px" alt=""></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnProsPectID" runat="server" Value="0" />
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
