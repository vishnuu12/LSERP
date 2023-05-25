<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="DuplicateBOMSheet.aspx.cs" Inherits="Pages_DuplicateBOMSheet" ClientIDMode="Predictable" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   <script type="text/javascript" src="../Assets/scripts/math.js"></script>
    <link rel="stylesheet" type="text/css" href="../Assets/css/datatables2.css" />
    <script type="text/javascript" src="../Assets/scripts/datatables2.js"></script>
    <script type="text/javascript">
        var RF = "";
        function MOCChanged(ele) {
            var ControlID = ele.id;
            var MainValue = $('#' + ControlID).val();
            var MOC = MainValue.split('/')[0];
            var SWT = MainValue.split('/')[1];
            var Cost = MainValue.split('/')[2];
            $('.SWT').val(SWT);
            $('.MRATE').val(Cost);
            $('.SuggestedMrateCost').text('Suggested Cost(' + Cost + ')');
        }

        function deleteConfirm(ETCID, BOMID) {
            if ($(event.target).closest('a').attr('class') == 'aspNetDisabled') {
                return false;
            }
            else {
                swal({
                    title: "Are you sure?",
                    text: "If Yes, the Material will be deleted permanently",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "Yes, Delete it!",
                    closeOnConfirm: false
                }, function () {
                    //showLoader();
                    __doPostBack('deletegvrow', ETCID + '/' + BOMID);
                });
                return false;
            }
        }

        //        function UpdateBOMStatus() {
        //            swal({
        //                title: "Are you sure you want to save?",
        //                text: "Once this bom estimation has been saved you can't edit further more and it'll be moved to next stage.",
        //                type: "warning",
        //                showCancelButton: true,
        //                confirmButtonColor: "#DD6B55",
        //                confirmButtonText: "Yes, Save it!",
        //                closeOnConfirm: false
        //            }, function () {
        //                //showLoader();
        //                __doPostBack('UpdateBOMStatus', null);
        //            });
        //            return false;
        //        }

        function CalculateFomula() {
            try {
                var FN;
                var msg;
                var FormulaName = [];
                var FormulaID = [];
                $('#<%=tblFormulaFields.ClientID %> input[type="text"]').each(function () {
                    // FN = .replace(/SQUARE/g, "2");
                    FormulaName.push($(this).val());
                    ControlID = $(this).attr('id').split('_');
                    FormulaID.push(ControlID[0] + "_" + ControlID[2]);
                });
                msg = ValidateInputFields();
                if (msg) {
                    debugger;
                    return formulafieldvalueassign(FormulaName, FormulaID);
                }
                else {
                    ErrorMessage('Error', 'Field Required "' + RF + '"');
                    return false;
                }
            } catch (er) { return false }
            return true;
        }

        function formulafieldvalueassign(formula, formlaid) {
            debugger;
            try {
                var IDVal;
                var result;
                var ControlID;
                var FormulaCode = [];
                var FormulaName = [];
                FormulaName = formula;
                var Formulaindexreloop = [];
                var FormulaIDreloop = [];
                var reloop = false;
                var allformulas = true;
                var FormulaID = [];
                FormulaID = formlaid;
                var exitloop = false;
                $.each(FormulaName, function (FNIndex, value) {
                    var valueasign = true;
                    var separators = ['\\\+', '-', '\\*', '/', '\\(', '\\)', '[0-9]', '\\.'];
                    FormulaCode = FormulaName[FNIndex].trim().split(new RegExp(separators.join('|'), 'g')).filter(function (e) { return e != ""; });
                    var tmpFormulaName = FormulaName[FNIndex];
                    $.each(FormulaCode, function (index, value) {
                        //IDVal = GetParameter(FormulaCode[index]);                        

                        if (FormulaCode[index] == "SQUARE") {
                            FormulaCode[index] = FormulaCode[index + 1];
                            FormulaName[FNIndex] = FormulaName[FNIndex].replace('SQUARE', FormulaCode[index + 1]);
                        }

                        if ($('#ContentPlaceHolder1_' + FormulaCode[index]).length > 0) {
                            if ($('#ContentPlaceHolder1_' + FormulaCode[index]).text() != '') IDVal = $('#ContentPlaceHolder1_' + FormulaCode[index]).text();
                            else if ($('#ContentPlaceHolder1_' + FormulaCode[index]).val() != '' && $('#ContentPlaceHolder1_' + FormulaCode[index]).val() != undefined) IDVal = $('#ContentPlaceHolder1_' + FormulaCode[index]).val();
                            else IDVal = 'novalue';
                            if (IDVal == 'novalue') {
                                valueasign = false;
                            }
                            else { FormulaName[FNIndex] = FormulaName[FNIndex].replace(FormulaCode[index], IDVal); }
                        }
                        else {
                            ErrorMessage('Error', FormulaCode[index] + ' Missing');
                            exitloop = true;
                            return false;
                        }
                    });
                    if (exitloop == true) return false;
                    if (valueasign == true) {
                        $('#' + FormulaID[FNIndex]).text(math.evaluate(FormulaName[FNIndex]).toFixed(2));
                        allformulas = false;
                    }
                    else {
                        reloop = true;
                        Formulaindexreloop.push(tmpFormulaName);
                        FormulaIDreloop.push(FormulaID[FNIndex]);
                    }
                });
                if (exitloop == true) return false;
                if (reloop == true && allformulas == false) {
                    try {
                        FormulaName.length = 0;
                        $.each(Formulaindexreloop, function (FLindex) {
                            FormulaName.push(Formulaindexreloop[FLindex]);
                        });
                        Formulaindexreloop.length = 0;
                        FormulaID.length = 0;
                        $.each(FormulaIDreloop, function (FIDindex) {
                            FormulaID.push(FormulaIDreloop[FIDindex]);
                        });
                        FormulaIDreloop.length = 0;
                        reloop = false;
                        formulafieldvalueassign(FormulaName, FormulaID);
                    } catch (er) {
                        debugger;
                        reloop = false;
                    }
                }
            }
            catch (er) {
                debugger;
                ErrorMessage('Error', 'Error occured');
                return false;
            }
            return true;
        }
        function GetParameter(Val) {
            var b = 'formula';
            try {
                $('#<%=tblInputFields.ClientID %> input[type="text"]').each(function () {
                    var MOC = $(this).attr("class");
                    if (!MOC.includes("chosen-search-input")) {
                        if ($(this).attr('id').split('_')[1] == Val) {
                            b = $(this).val();
                            return false;
                        }
                    }
                });
                //added for formula field value
                if ($('#ContentPlaceHolder1_' + Val).text() != '') b = $('#ContentPlaceHolder1_' + Val).text();
            }
            catch (er) { }
            return b;
        }

        function ValidateInputFields() {
            var msg = true;
            $('#<%=tblInputFields.ClientID %> input[type="text"]').each(function () {
                var MOC = $(this).attr("class");
                if (!MOC.includes("chosen-search-input")) {
                    if ($(this).val() == "") {
                        if (RF == "") {
                            RF = $(this).attr("id").split('_')[1];
                        }
                        else {
                            RF = RF + ',' + $(this).attr("id").split('_')[1];
                        }
                        msg = false;
                    }
                }
                else {
                    if ($('.ddlMOC').val().split('/')[0] == "0") {
                        if (RF == "") {
                            RF = $('.lblMOC').text();
                        }
                        else {
                            RF = RF + ',' + $('.lblMOC').text();
                        }
                        msg = false;
                    }
                }
            });
            return msg;
        }

        //        function ShowDataTable() {
        //            $('#<%=gvBOMCostDetails.ClientID %>').DataTable();
        //        }

        function SaveBOMSheet() {
            var btnSaveEnapled = $('#<%=btnSave.ClientID %>').attr('class');

            if (!btnSaveEnapled.includes("btnSaveDisabled")) {
                var msg;
                var strDetails = [];
                var MSMIDs = [];
                var MSMIDValue = [];
                var SaveBOMDetails = [];
                var EnquiryNumber;
                var VersionNumber;
                var MaterialName;
                msg = ValidateInputFields();

                $('#<%=tblInputFields.ClientID %> input[type="text"]').each(function () {
                    MSMIDValue.push($(this).val());
                });

                if (msg) {
                    var calculated = CalculateFomula();
                    if (calculated == false) return false;
                    var rows = $('#<%=tblInputFields.ClientID %> tbody >tr');
                    var k = 0;
                    var frows = $('#<%=tblFormulaFields.ClientID %> tbody >tr');

                    EnquiryNumber = $('#<%=ddlItemName.ClientID %>').children("option:selected").val();
                    VersionNumber = $('#<%=ddlVersionNumber.ClientID %>').children("option:selected").text();
                    MaterialName = $('#<%=ddlMaterialName.ClientID %>').children("option:selected").val();

                   // strDetails.push(EnquiryNumber.split('/')[0] + "/" + VersionNumber + "/" + MaterialName.split('/')[1] + "/" + MaterialName.split('/')[0]);
                     strDetails.push(EnquiryNumber.split('/')[0] + "/" + VersionNumber + "/" + $('#ContentPlaceHolder1_hdnMID').val() + "/" + $('#ContentPlaceHolder1_hdnBomID').val() + "/" + $('#ContentPlaceHolder1_txtRemarks').val());
                    for (var i = 0; i < rows.length; i++) {
                        columns = $(rows[i]).find('td');
                        for (var j = 0; j < columns.length; j = j + 2) {
                            var MSMID = rows[i].cells[j].children[1].innerText;
                            if (rows[i].cells[j].children[1].className != "lbl_MOC") {
                                SaveBOMDetails.push(MSMID + ':' + MSMIDValue[k]);
                            }
                            k++;
                        }
                    }
                    var lblMOC = $('.lbl_MOC').attr('class');
                    if (lblMOC.split('_')[1] == "MOC") {
                        var MSMIDofMOC = $('.lbl_MOC').text();
                        var MSMIDValueMOC = $('.ddlMOC').val().split('/')[0];
                        SaveBOMDetails.push(MSMIDofMOC + ':' + MSMIDValueMOC);
                    }

                    for (var i = 0; i < frows.length; i++) {
                        var fcolumns = $(frows[i]).find('td');
                        var fk = 1;
                        for (var j = 0; j < fcolumns.length; j = j + 2) {
                            var FMSMID = frows[i].cells[j].children[0].id.split('_')[2];
                            var FMSMIDValue = frows[i].cells[fk].children[0].innerText;
                            SaveBOMDetails.push(FMSMID + ':' + FMSMIDValue);
                            fk = fk + 2;
                        }
                    }

                    PageMethods.set_path('DuplicateBOMSheet.aspx');
                    PageMethods.SaveBOMDetails(SaveBOMDetails, strDetails, AfterAssignedProcess);
                }
                else {
                    ErrorMessage('Error', 'Field Required:' + RF + '');
                }
                return false;
            }
            else {

                ErrorMessage('Error', 'Item Moved Into Next Stage');
                return false;
            }

        }


        function AfterAssignedProcess(ReturnMsg) {
            if (ReturnMsg == 'Added') {
                //$("<thead></thead>").append($("#<%=tblInputFields.ClientID %> tr:first")).prependTo($("#<%=tblInputFields.ClientID %>"));
                SuccessMessage("Success", "BOM Details Saved Successfully");
            }
            else {
                SuccessMessage("Success", "BOM Details Updated successfully");
                //                document.getElementById('<%=ddlEnquiryNumber.ClientID %>').disabled = false;
                //                document.getElementById('<%=ddlVersionNumber.ClientID %>').disabled = false;
                //                document.getElementById('<%=ddlMaterialName.ClientID %>').disabled = false;
            }
            document.getElementById('<%=btnLoad.ClientID %>').click();
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
                                    <h3 class="page-title-head d-inline-block">Duplicate BOM Sheet</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Design</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Duplicate BOM Sheet</li>
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
            <asp:UpdatePanel ID="upQuote" runat="server" UpdateMode="Always">
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
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Customer Name</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlCustomerName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCustomerName_SelectIndexChanged"
                                            CssClass="form-control" Width="70%" ToolTip="Select Customer Number">
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
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12  p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Item Name</label>
                                        <asp:Label ID="lblDrawingNumber" Style="color: brown;" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlItemName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlItemName_SelectIndexChanged"
                                            CssClass="form-control" Width="70%" ToolTip="Select Item Name">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Label ID="lblMaterialWarning" CssClass="blinking budgetaryhighligt lablqty"
                                            runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Revision Number</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlVersionNumber" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlVersionNumber_SelectIndexChanged"
                                            CssClass="form-control" Width="70%" ToolTip="Select Enquiry Number" TabIndex="1">
                                            <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10" runat="server" style="display: none;">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Material Name
                                        </label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlMaterialName" runat="server" CssClass="form-control" Width="70%"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlMaterialName_SelectIndexChanged"
                                            ToolTip="Select Material Name">
                                            <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4">
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:RadioButtonList ID="rblItemDuplicate" runat="server" Visible="false" OnSelectedIndexChanged="rblItemDuplicate_SelectIndexChanged"
                                            AutoPostBack="true" CssClass="radio radio-success" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="ItemDuplicate From" Value="ItemDup" Selected="False"></asp:ListItem>
                                            <asp:ListItem Text="EnquiryDuplicate" Value="EnquiryDup" Selected="False"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>
                                <div id="divItemDuplicate" runat="server" visible="false">
                                    <div class="col-sm-12 p-t-10" runat="server" id="divItemDuplicateEnquiryNumber" visible="false">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label mandatorylbl">
                                               Enquiry Number</label>
                                        </div>
                                        <div class="col-sm-4 text-left">
                                            <asp:DropDownList ID="ddlDuplicateEnquiryNumber" runat="server" CssClass="form-control mandatoryfield"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlDuplicateEnquiryNumber_SelectIndexChanged"
                                                ToolTip="Select Enquiry Number">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label mandatorylbl">
                                                Item Name From</label>
                                        </div>
                                        <div class="col-sm-4 text-left">
                                            <asp:DropDownList ID="ddlDuplicateItemName" runat="server" CssClass="form-control mandatoryfield"
                                                AutoPostBack="true" OnSelectedIndexChanged="ddlDuplicateItemName_SelectIndexChanged"
                                                ToolTip="Select Item Name">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-4 text-center">
                                            <asp:LinkButton ID="btnDuplicate" CssClass="btn btn-cons btn-save" runat="server"
                                                OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divItemDuplicate')"
                                                OnClick="btnDuplicate_Click" Text="Duplicate"></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div id="divEnquiryNumberDuplicate" runat="server" visible="false">
                                    <div class="col-sm-12">
                                    </div>
                                </div>
                                <div class="col-sm-12" id="divFields" runat="server" visible="false">
                                    <div class="col-sm-12 p-t-10 text-center">
                                        <label class="form-label">
                                            Input Fields</label>
                                    </div>
                                    <div class="col-sm-12" style="padding-left: 20%">
                                        <table id="tblInputFields" runat="server" style="text-align: center;">
                                        </table>
                                    </div>
                                    <div class="col-sm-12 p-t-20 text-center">
                                        <label class="form-label">
                                            Formula Fields</label>
                                    </div>
                                    <div class="col-sm-12 p-t-10" style="padding-left: 20%">
                                        <table id="tblFormulaFields" runat="server">
                                        </table>
                                    </div>
                                    <div class="col-sm-12 text-center p-t-10">
                                        <asp:LinkButton ID="btnCalculate" CssClass="btn btn-cons btn-save" runat="server"
                                            Text="CalCulate" OnClientClick="return CalculateFomula();"></asp:LinkButton>
                                        <asp:LinkButton ID="btnSave" CssClass="btn btn-cons btn-save" runat="server" Text="Save"
                                            OnClientClick="return SaveBOMSheet();"></asp:LinkButton>
                                        <asp:Button ID="btnLoad" OnClick="btnLoad_Click" runat="server"></asp:Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server" visible="false">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                                    <asp:Label ID="lbltitle" runat="server"></asp:Label>
                                                </p>
                                            </div>
                                            <div class="col-md-6 ex-icons" id="divDownload" runat="server" visible="false">
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvBOMCostDetails" runat="server" AutoGenerateColumns="true" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                OnRowCommand="gvBOMCostDetails_OnRowCommand" OnRowDataBound="gvBOMCostDetails_OnRowDataBound"
                                                HeaderStyle-HorizontalAlign="Center" DataKeyNames="ETCID,MID,BOMID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnedit" runat="server" Text="Edit" CommandName="EditBOM" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                ToolTip="Edit"><img src="../Assets/images/edit-ec.png" style="height:20px" alt=""></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" OnClientClick='<%# string.Format("return deleteConfirm({0},{1});",Eval("ETCID"),Eval("BOMID")) %>'
                                                                ToolTip="Delete"> <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-6 text-right">
                                                <label class="form-label">
                                                    Total BOM Cost
                                                </label>
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:Label ID="lblCost" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvItemDuplicateDetails" runat="server" AutoGenerateColumns="true"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                HeaderStyle-HorizontalAlign="Center">
                                            </asp:GridView>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-6 text-right">
                                                <label class="form-label">
                                                    Total Duplicate Item From BOM Cost
                                                </label>
                                            </div>
                                            <div class="col-sm-6">
                                                <asp:Label ID="lblDuplicateItemFrom" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:HiddenField ID="hdnBOMId" Value="0" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
