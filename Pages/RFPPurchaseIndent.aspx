<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="RFPPurchaseIndent.aspx.cs" Inherits="Pages_RFPPurchaseIndent" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">

        function multiselect(multivalue) {
            debugger;
            var test = multivalue.trim();
            var testArray = test.split(',');
            $('#ContentPlaceHolder1_liCertificates').val(testArray);
        }

        function ValidateReqQty(ele) {
            var Indentqty = $(ele).closest('td').find('[type="hidden"]').val();

            if (parseFloat(Indentqty) <= parseFloat($(ele).val())) {
                return true;
            }
            else {
                $(ele).val(Indentqty);
                ErrorMessage('Error', 'Indent Qty Should Not Less Than Blocked Qty');
                return false;
            }
        }

        function SaveIndent() {
            var mancheck = Mandatorycheck('ContentPlaceHolder1_divInput');
            if (mancheck == false) {
                return false;
            }
            else {
                if ($('#ContentPlaceHolder1_gvMaterialplanningIndentDetails').find('[type="checkbox"]:checked').length > 0) {
                    var mtfid = '';
                    var mtfidsval = '';
                    $('#ContentPlaceHolder1_divMTFields').find('input[type="text"]').each(function (index) {
                        debugger;
                        mtfid = mtfid + $(this).attr('id').split(/[\s_]+/).pop() + ',';
                        mtfidsval = mtfidsval + $(this).val() + ',';
                    });
                    $('#ContentPlaceHolder1_hdn_MTFIDS').val(mtfid.replace(/.$/, ""));
                    $('#ContentPlaceHolder1_hdn_MTFIDsValue').val(mtfidsval.replace(/.$/, ""));

                    return true;
                }
                else {
                    ErrorMessage('Error', 'Please Select Item');
                    hideLoader();
                    return false;
                }
            }
        }

        function deleteConfirmMPMD(PID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Indentwill be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deletegvrowMPMD', PID);
            });
            return false;
        }

        function ViewFileName(RowIndex) {
            __doPostBack("ViewIndentCopy", RowIndex);
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
                                    <h3 class="page-title-head d-inline-block">RFP Purchase Indent</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Purchase</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">RFP Purchase Indent</li>
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
                    <asp:PostBackTrigger ControlID="btnSaveRFPItem" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="Item" runat="server">
                            <div id="divAdd" runat="server">
                                <div class="inner-container">
                                    <%--OnClientClick="return ShowAdditemdetailspopup();"--%>
                                    <div class="col-sm-12">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label">
                                                Customer Name</label>
                                        </div>
                                        <div class="col-sm-6 text-left">
                                            <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="true" CssClass="form-control"
                                                Width="70%" ToolTip="Select Customer Number">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label">
                                                RFP No</label>
                                        </div>
                                        <div class="col-sm-6 text-left">
                                            <asp:DropDownList ID="ddlRFPNo" runat="server" AutoPostBack="true" CssClass="form-control"
                                                OnSelectedIndexChanged="ddlRFPNo_SelectIndexChanged" Width="70%" ToolTip="Select RFP No">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="divAddNew" runat="server">
                                <div class="inner-container">
                                    <%--OnClientClick="return ShowAdditemdetailspopup();"--%>
                                    <div class="col-sm-12 text-center p-t-10" id="div5" runat="server">
                                        <asp:LinkButton ID="btnItemAddNew" runat="server" Text="Add New" CssClass="btn btn-success add-emp"
                                            OnClick="btnItemAddNew_Click"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div id="divInput" class="divInput" runat="server">
                                <div class="ip-div text-center">
                                    <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                        <asp:GridView ID="gvMaterialplanningIndentDetails" runat="server" AutoGenerateColumns="False"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found"
                                            CssClass="table table-hover table-bordered medium" OnRowDataBound="gvMaterialplanningIndentDetails_OnRowDatabound"
                                            DataKeyNames="MPID,MGMID,THKID,BlockedWeight">
                                            <Columns>

                                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                    HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="" ControlStyle-CssClass="nosort" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkitems" runat="server" AutoPostBack="true"
                                                            OnCheckedChanged="chkitems_OnCheckedChanged" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="MPID" ItemStyle-HorizontalAlign="left"
                                                    HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMPID" runat="server" Text='<%# Eval("MPID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Drawing Name" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDrawingName" runat="server" Text='<%# Eval("DrawingName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Item Size" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemSize" runat="server" Text='<%# Eval("ItemSize")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Part Name" ItemStyle-HorizontalAlign="left"
                                                    HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPartname" runat="server" Text='<%# Eval("PartName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Category Name" ItemStyle-HorizontalAlign="left"
                                                    HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("CategoryName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Grade Name" ItemStyle-HorizontalAlign="left"
                                                    HeaderStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGradeName" runat="server" Text='<%# Eval("GradeName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="THK" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMaterialThickness" runat="server" Text='<%# Eval("THKvalue")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Req Qty" ItemStyle-HorizontalAlign="Left"
                                                    HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtReqWeight" runat="server"
                                                            onkeyup="return ValidateReqQty(this);"
                                                            Text='<%# Eval("BlockedWeight")%>'></asp:Label>
                                                        <asp:HiddenField ID="hdnBlockedQty" Value='<%# Eval("BlockedWeight")%>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <asp:HiddenField ID="hdnPID" runat="server" Value="0" />
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-4 text-left" id="matrltype" runat="server">
                                            <div class="text-left">
                                                <label class="form-label">
                                                    Material Type</label>
                                            </div>
                                            <div>
                                                <asp:DropDownList ID="ddlMaterialType" runat="server" ToolTip="Select Material type"
                                                    OnSelectedIndexChanged="ddlMaterialType_OnSelectIndexChanged" CssClass="form-control"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 text-left">
                                            <div class="text-left">
                                                <label class="form-label mandatorylbl">
                                                    Required Quantity</label>
                                            </div>
                                            <div class="text-left">
                                                <%--  <asp:Label ID="lblrequiredweight" Style="color: brown;" runat="server"></asp:Label>
                                                                                <asp:TextBox ID="txtrequiredWeight" runat="server" CssClass="form-control mandatoryfield"
                                                                                    onkeypress="return validationDecimal(this);" AutoComplete="off" ToolTip="Enter Weight" placeholder="Enter Weight">
                                                                                </asp:TextBox>--%>

                                                <%--  <asp:Label ID="lblrequiredweight" Style="color: brown;" Text="0" runat="server"></asp:Label>--%>

                                                <asp:TextBox ID="txtReqQty" onkeypress="return validationDecimal(this);" CssClass="form-control mandatoryfield" runat="server"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                    <div id="divMTFields" runat="server">
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-2" style="margin-top: 38px;">
                                            <asp:Label ID="lblreqweight" CssClass="lablqty" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-4 text-left">
                                            <div class="text-left">
                                                <label class="form-label mandatorylbl">
                                                    UOM</label>
                                            </div>
                                            <div class="text-left">
                                                <asp:DropDownList ID="ddlUOM" runat="server" ToolTip="Select UOM" CssClass="form-control mandatoryfield">
                                                    <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 text-left">
                                            <div class="text-left">
                                                <label class="form-label">
                                                    Remarks</label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" AutoComplete="off"
                                                    TextMode="MultiLine" Rows="3" ToolTip="Enter Remarks" placeholder="Enter Remarks">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>

                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-4 text-left">
                                            <div class="text-left">
                                                <label class="form-label mandatorylbl">
                                                    Delivery Date</label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtDeliveryDate_I" runat="server" CssClass="form-control datepicker mandatoryfield"
                                                    AutoComplete="off" ToolTip="Enter Delivery Date" placeholder="Enter Delivery Date">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 text-left">
                                            <div class="text-left">
                                                <label class="form-label mandatorylbl">
                                                    Available Certificates</label>
                                            </div>
                                            <div>
                                                <asp:ListBox ID="liCertificates" runat="server" CssClass="form-control mandatoryfield"
                                                    SelectionMode="Multiple"></asp:ListBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>

                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-2" style="margin-top: 38px;">
                                            <asp:Label ID="Label3" CssClass="lablqty" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-4 text-left">
                                            <div class="text-left">
                                                <label class="form-label">
                                                    Upload</label>
                                            </div>
                                            <div>
                                                <asp:FileUpload ID="fPUpload" runat="server" class="form-control" />
                                            </div>
                                        </div>
                                        <div class="col-sm-4 text-left">
                                            <div class="text-left">
                                                <label class="form-label">
                                                    Drawing name</label>
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtDrawingname" runat="server" CssClass="form-control" AutoComplete="off"
                                                    placeholder="Enter Drawing name">
                                                </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>

                                    <div class="col-sm-12 p-t-20">
                                        <%-- OnClick="btnSave_SPO_Click"    OnClick="btnCancel_SPO_Click"--%>
                                        <asp:HiddenField ID="hdn_MTFIDS" runat="server" />
                                        <asp:HiddenField ID="hdn_MTFIDsValue" runat="server" />

                                        <asp:LinkButton ID="btnSaveRFPItem" runat="server" Text="Save" OnClientClick="return SaveIndent();"
                                            OnClick="btnSaveRFPIndent_Click"
                                            CssClass="btn btn-cons btn-save AlignTop"></asp:LinkButton>
                                        <asp:LinkButton ID="btnCancelRFPtem" runat="server" Text="Cancel"
                                            OnClick="btnCancelRFPtem_Click" CssClass="btn btn-cons btn-danger AlignTop btnCancel"></asp:LinkButton>

                                    </div>
                                </div>
                            </div>

                            <div id="divOutput" runat="server">
                                <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                    <asp:GridView ID="gvRFPIndentDetails" runat="server" AutoGenerateColumns="False"
                                        OnRowCommand="gvRFPIndentDetails_OnRowCommand" OnRowDataBound="gvRFPIndentDetails_OnRowDataBound"
                                        ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium orderingfalse"
                                        DataKeyNames="PID,FileName">
                                        <Columns>

                                            <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                HeaderStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Indent Status" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                HeaderStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRNo" runat="server" Text='<%# Eval("PID")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Indent No" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                HeaderStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIndentNo" runat="server" Text='<%# Eval("IndentNo")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="RFP No" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                HeaderStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRFPNo" runat="server" Text='<%# Eval("RFPNo")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category Name" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                HeaderStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("CategoryName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Part Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblitemname" runat="server" Text='<%# Eval("PartName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Material Type Name" ItemStyle-HorizontalAlign="left"
                                                HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTypeName" runat="server" Text='<%# Eval("TypeName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Grade Name" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                HeaderStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGradeName" runat="server" Text='<%# Eval("GradeName")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="THK" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                HeaderStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTHK" runat="server" Text='<%# Eval("THKValue")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Measurments" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                HeaderStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMeasurments" runat="server" Text=' <%# Eval("Measurment").ToString().Replace(",", "<br/>")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                HeaderStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemarks" runat="server"
                                                        Text='<%# Eval("Remarks").ToString() %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Indent By" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIndentBy" runat="server" Text='<%# Eval("EmpIndentBy")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Indent To" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIndentTo" runat="server" Text='<%# Eval("EmpIndentTo")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delivery Date" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDeliveryDate" runat="server" Text='<%# Eval("DeliveryDate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cerificates" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                HeaderStyle-HorizontalAlign="left">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCertificates" runat="server" Text='<%# Eval("Certificate")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Required Qty" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReqQty" runat="server" Text='<%# Eval("RequiredWeight")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                HeaderText="View">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnView" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                        OnClientClick='<%# string.Format("return ViewFileName({0});",((GridViewRow) Container).RowIndex) %>'>
                                                                <img src="../Assets/images/view.png" /></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                HeaderText="Edit">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnEdit" runat="server" Visible='<%# Eval("PidStatus").ToString() == "Having Record" ? false : true %>' CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                        CommandName="EditPI"><img src="../Assets/images/edit-ec.png"/></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnDelete" runat="server" Visible='<%# Eval("PidStatus").ToString() == "Having Record" ? false : true %>' CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                        OnClientClick='<%# string.Format("return deleteConfirmMPMD({0});",Eval("PID")) %>'>   <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div class="col-sm-12 p-t-20 text-center">
                                <%-- <asp:LinkButton ID="lbtnSharePO" runat="server" Text="Share PO" CausesValidation="False"
                                    OnClick="lbtnSharePO_Click" CssClass="btn btn-cons btn-success"></asp:LinkButton>
                                <asp:LinkButton ID="lbtnApprove" runat="server" Text="Send For Approval" CausesValidation="False"
                                    OnClick="btnSendForApproval_Click" CssClass="btn btn-cons btn-success"></asp:LinkButton>--%>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <iframe runat="server" id="ifrm" src="" style="display: none;" frameborder="0"></iframe>
</asp:Content>

