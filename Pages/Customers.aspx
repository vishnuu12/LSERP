<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="Customers.aspx.cs" Inherits="Pages_Customers" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" type="text/css" href="../Assets/css/datatables2.css" />
    <script type="text/javascript" src="../Assets/scripts/datatables2.js"></script>
    <script type="text/javascript">

        function deleteConfirm(CustomerID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Customer Name will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deletegvrow', CustomerID);
            });
            return false;
        }

        function fnAllowNumericAndDot() {
            if ((event.keyCode < 48 || event.keyCode > 57) && event.keyCode != 8 && event.keyCode != 46) {
                event.keyCode = 0;
                return false;
            }
        }

        function checkNumeric(e) {
            if ((e.keyCode >= 48 && e.keyCode <= 57) || (e.keyCode == 46))
                return true;

            return false;
        }
        function showDataTable() {
            $('#<%=gvCustomer.ClientID %>').DataTable({ 'aoColumnDefs': [{
                'bSortable': false,
                'aTargets': [-1, -2] /* 1st one, start by the right */
            }]
            });
        }

        function CheckValues() {
            var msg = true;
            if (document.getElementById("<%=txtComp.ClientID %>").value == "") {
                $('#<%=txtComp.ClientID %>').notify('Field Required.', { arrowShow: true, position: 'r,r', autoHide: true });
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
            else if (isValidEmail(document.getElementById("<%=txtEmail.ClientID %>").value) == false) {
                $('#<%=txtEmail.ClientID %>').notify('Please Enter Valid Email.', { arrowShow: true, position: 'r,r', autoHide: true });
                document.getElementById("<%=txtEmail.ClientID %>").focus();
                msg = false;
            }

            if (document.getElementById("<%=txtCustPanNo.ClientID %>").value != "") {
                var PanNo = document.getElementById("<%=txtCustPanNo.ClientID %>").value;
                if (PanNo.length > 10 || PanNo.length < 10) {
                    $('#<%=txtCustPanNo.ClientID %>').notify('Please Enter Valid PAN Number.', { arrowShow: true, position: 'r,r', autoHide: true });
                    msg = false;
                }
            }
            if (document.getElementById("<%=txtCustTanNo.ClientID %>").value != "") {
                var TanNo = document.getElementById("<%=txtCustTanNo.ClientID %>").value;
                if (TanNo.length > 10 || TanNo.length < 10) {
                    $('#<%=txtCustTanNo.ClientID %>').notify('Please Enter Valid TAN Number.', { arrowShow: true, position: 'r,r', autoHide: true });
                    msg = false;
                }
            }
            if (document.getElementById("<%=txtCustGSTNo.ClientID %>").value != "") {

                var GSTNo = document.getElementById("<%=txtCustGSTNo.ClientID %>").value;
                if (GSTNo.length > 15 || GSTNo.length < 15) {
                    $('#<%=txtCustGSTNo.ClientID %>').notify('Please Enter Valid GST Number.', { arrowShow: true, position: 'r,r', autoHide: true });
                    msg = false;
                }
            }

            if (document.getElementById('<%=ddlCountry.ClientID %>').value == "0") {
                $('#<%=ddlCountry.ClientID %>').notify('Fiels Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }
            if (document.getElementById('<%=ddlState.ClientID %>').value == "0") {
                $('#<%=ddlState.ClientID %>').notify('Fiels Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }

            if (document.getElementById('<%=ddlCity.ClientID %>').value == "0") {
                $('#<%=ddlCity.ClientID %>').notify('Fiels Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }
            if (document.getElementById("<%=ddlCustomerType.ClientID %>").value == "0") {
                $('#<%=ddlCustomerType.ClientID %>').notify('Field Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }
            if (document.getElementById('<%=txtOpeningBalance.ClientID %>').value == "") {
                $('#<%=txtOpeningBalance.ClientID %>').notify('Fiels Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }
            if (document.getElementById('<%=txtContactPerson.ClientID %>').value == "") {
                $('#<%=txtContactPerson.ClientID %>').notify('Fiels Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }
            if (document.getElementById('<%=ddlProspect.ClientID %>').selectedindex == 0) {
                $('#<%=ddlProspect.ClientID %>').notify('Fiels Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }
            if (document.getElementById('<%=ddlRegion.ClientID %>').selectedindex == 0) {
                $('#<%=ddlRegion.ClientID %>').notify('Fiels Required.', { arrowShow: true, position: 'r,r', autoHide: true });
                msg = false;
            }

            if (document.getElementById('<%=txtFaxNo.ClientID %>').value == "") {
                $('#<%=txtFaxNo.ClientID %>').notify('Fiels Required.', { arrowShow: true, position: 'r,r', autoHide: true });
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
                                    <h3 class="page-title-head d-inline-block">
                                        Customers</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Accounts</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Customers</li>
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
            <asp:UpdatePanel ID="upCustomers" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="imgExcel" />
                    <asp:PostBackTrigger ControlID="imgPdf" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" runat="server">
                            <div class="ip-div text-center">
                                <asp:LinkButton ID="btnAddNew" runat="server" Text="Add New Customer" OnClientClick="return ShowInputSection();"
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
                                                <asp:Label ID="lblCmpName" runat="server" class="form-label" Text="Customer Name"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtComp" placeholder="Enter Name" runat="server" CssClass="form-control"
                                                    MaxLength="50" autocomplete="off"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <asp:Label ID="Label1" runat="server" class="form-label" Text="Customer Type"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:DropDownList ID="ddlCustomerType" runat="server" CssClass="form-control">
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
                                                <asp:Label ID="lblAddress" runat="server" class="form-label" Text="Address"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" Height="52px"
                                                    MaxLength="500" TextMode="MultiLine" autocomplete="nope" placeholder="Enter Address"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <asp:Label ID="Label25" runat="server" class="form-label" Text="Country"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" AutoPostBack="true"
                                                    OnChange="showLoader();" OnSelectedIndexChanged="ddlCountry_IndexChanged">
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
                                                <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="true" CssClass="form-control"
                                                    OnChange="showLoader();" OnSelectedIndexChanged="ddlState_IndexChanged">
                                                    <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <asp:Label ID="lblCity" runat="server" class="form-label" Text="City"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control">
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
                                                <asp:Label ID="lblContactPerson" runat="server" class="form-label" Text="Contact Person"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtContactPerson" runat="server" placeholder="Enter Contact Person"
                                                    CssClass="form-control" MaxLength="100" autocomplete="nope"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <asp:Label ID="lblEmail" runat="server" class="form-label" Text="Email id"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtEmail" runat="server" placeholder="Enter Email" CssClass="form-control"
                                                    MaxLength="100" onkeypress="return validateEmail(this);" autocomplete="nope"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <asp:Label ID="lblPhone" runat="server" class="form-label" Text="Phone Number"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtPhoneNo" placeholder="Enter Mobile Number" runat="server" CssClass="form-control"
                                                    onkeypress="return validationNumeric(this);" MaxLength="10" autocomplete="nope"
                                                    AutoCompleteType="Disabled"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <asp:Label ID="Label6" runat="server" class="form-label" Text="Customer PAN Number"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtCustPanNo" runat="server" placeholder="Pancard Number" CssClass="form-control"
                                                    MaxLength="10" onkeypress="return checkAlphaNumeric(event);" autocomplete="nope"
                                                    Style="text-transform: uppercase;"></asp:TextBox>
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
                                                <asp:Label ID="Label7" runat="server" class="form-label" Text="Customer TAN Number"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtCustTanNo" runat="server" placeholder="Enter TAN number" CssClass="form-control"
                                                    MaxLength="10" onkeypress="return checkAlphaNumeric(event);" autocomplete="nope"
                                                    Style="text-transform: uppercase;"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <asp:Label ID="Label23" runat="server" class="form-label" Text="Customer GST Number"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtCustGSTNo" runat="server" placeholder="Enter GST number" CssClass="form-control"
                                                    MaxLength="15" onkeypress="return checkAlphaNumeric(event);" autocomplete="nope"
                                                    Style="text-transform: uppercase;"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <asp:Label ID="lblRegion" runat="server" class="form-label" Text="Region"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:DropDownList ID="ddlRegion" runat="server" CssClass="form-control">
                                                    <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <asp:Label ID="lblFaxNo" runat="server" class="form-label" Text="Fax No"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtFaxNo" runat="server" placeholder="Enter Fax number" CssClass="form-control"
                                                    onkeypress="return validateFAXNo(event);" autocomplete="nope"></asp:TextBox>
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
                                                <asp:Label ID="Label8" runat="server" class="form-label" Text="Bank Name "></asp:Label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtBankName" placeholder="Enter Bank Name" runat="server" autocomplete="nope"
                                                    CssClass="form-control" MaxLength="20"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <asp:Label ID="Label9" runat="server" class="form-label" Text="Bank Account No "></asp:Label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtBankAccNo" placeholder="Enter Bank Account Number" autocomplete="nope"
                                                    runat="server" CssClass="form-control" onkeypress="return checkNumeric(event)"
                                                    MaxLength="12"></asp:TextBox>
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
                                                <asp:Label ID="Label10" runat="server" class="form-label" Text="Bank IFSC Details "></asp:Label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtBankIFSC" runat="server" placeholder="Enter Bank IFSC Details"
                                                    CssClass="form-control" MaxLength="20" autocomplete="nope" Style="text-transform: uppercase;"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <asp:Label ID="Label29" runat="server" class="form-label" Text="Opening Balance"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtOpeningBalance" runat="server" autocomplete="nope" placeholder="Enter Opening Balance"
                                                    CssClass="form-control" onkeypress="return checkNumeric(event)" MaxLength="20"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                    <div id="divProspect" class="col-sm-12 p-t-10" runat="server">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="text-left">
                                                <asp:Label ID="lblProspect" runat="server" class="form-label" Text="Prospect Name"></asp:Label>
                                            </div>
                                            <div>
                                                <asp:DropDownList ID="ddlProspect" runat="server" CssClass="form-control">
                                                    <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10 text-center m-b-20">
                                        <asp:LinkButton ID="btnSubmit" runat="server" CssClass="btn btn-cons btn-save AlignTop"
                                            OnClientClick="return CheckValues();" OnClick="btnSubmit_Click" Text="Submit" />
                                        <asp:LinkButton ID="btnreset" runat="server" CssClass="btn btn-cons btn-danger AlignTop"
                                            OnClick="btnreset_Click" Text="Cancel" OnClientClick="return CusCancel();" />
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
                                                    <asp:Label ID="lbltitle" runat="server" Text="Customer Details"></asp:Label></p>
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
                                            <asp:GridView ID="gvCustomer" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover no-more-tables"
                                                Width="100%" ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" OnRowCommand="gvCustomer_RowCommand"
                                                OnRowDataBound="gvCustomer_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CustomerID" Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCustomerID" runat="server" Text='<%# Eval("CustomerID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Customer Name" SortExpression="asc">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCustomerName" runat="server" Text='<%# Eval("CustomerName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Customer Type" SortExpression="asc">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCustomerTypeName" runat="server" Text='<%# Eval("CustomerTypeName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Address">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Country Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCountryName" runat="server" Text='<%# Eval("CountryName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="State Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStateName" runat="server" Text='<%# Eval("StateName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="City Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCityName" runat="server" Text='<%# Eval("CityName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Contact Person">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblContactPerson" runat="server" Text='<%# Eval("ContactPerson") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Phone Number">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("Phone") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Email id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEMailid" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <%-- <asp:Label Text='<%#Eval("CustomerID") %>' ID="lblCustid" CssClass="" Visible="false"
                                                            runat="server" />--%>
                                                            <asp:LinkButton ID="btnedit" runat="server" Text="Edit" CommandName="EditCustomer"
                                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ToolTip="Edit"><img src="../Assets/images/edit-ec.png" style="height:20px" alt=""></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDelete" runat="server" OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("CustomerID")) %>'>
                                                            <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnCustomerID" runat="server" Value="0" />
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
