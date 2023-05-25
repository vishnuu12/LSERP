<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="AddtionalPartRequest.aspx.cs" Inherits="Pages_AddtionalPartRequest" ClientIDMode="Predictable" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="https://unpkg.com/mathjs/lib/browser/math.js"></script>
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

        function deleteConfirm(DDID, BOMID) {
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
                    __doPostBack('deletegvrow', DDID + '/' + BOMID);
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

        function CalculateFomula(){
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

            if ($('#ContentPlaceHolder1_LiPartSno').val() == '') {
                if ($('#ContentPlaceHolder1_hdnPartSnoValid').val() == '1') {
                    if (RF == "")
                        RF = "Part Sno";
                    else
                        RF = RF + ',' + "Select Part Sno";
                    msg = false;
                }
            }

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
                    var PRPDID = "";
                    var RFPDID = "";
                    var frows = $('#<%=tblFormulaFields.ClientID %> tbody >tr');

                    DDID = $('#<%=ddlItemName.ClientID %>').children("option:selected").val().split('/')[0];
                    RFPDID = $('#<%=ddlItemName.ClientID %>').children("option:selected").val().split('/')[1];

                    MaterialName = $('#<%=ddlMaterialName.ClientID %>').children("option:selected").val().split('/')[0];

                    strDetails.push(DDID + "/" + $('#ContentPlaceHolder1_hdnMID').val() + "/" + $('#ContentPlaceHolder1_hdnBomID').val() + "/" + $('#ContentPlaceHolder1_hdnUserID').val() + "/" + "test" + "/" + RFPDID);

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
                    if (lblMOC != undefined) {
                        if (lblMOC.split('_')[1] == "MOC") {
                            var MSMIDofMOC = $('.lbl_MOC').text();
                            var MSMIDValueMOC = $('.ddlMOC').val().split('/')[0];
                            SaveBOMDetails.push(MSMIDofMOC + ':' + MSMIDValueMOC);
                        }
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

                    PageMethods.set_path('AddtionalPartRequest.aspx');
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

        function ShowViewPopUp() {
            $('#mpeView').modal({
                backdrop: 'static'
            });
            return false;
        }

        function AfterAssignedProcess(ReturnMsg) {
            if (ReturnMsg == 'Added') {

                SuccessMessage("Success", "BOM Details Saved Successfully");
            }
            else if (ReturnMsg == "Updated") {
                SuccessMessage("Success", "BOM Details Updated successfully");
            }
            else {
                InfoMessage('Information', ReturnMsg);
            }
            document.getElementById('<%=btnLoad.ClientID %>').click();
        }

        function checkAll(ele) {
            debugger;
            if ($(ele).is(":checked")) {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', true);
            }
            else {
                $(ele).closest('table').find('[type="checkbox"]').prop('checked', false);
            }
        }

        function ValidateApproval() {
            if ($('#ContentPlaceHolder1_gvBOMCostDetails').find('input:checked').not('#ContentPlaceHolder1_gvBOMCostDetails_chkall').length < 1) {
                ErrorMessage('Error', 'No Part Slected');
                return false;
            }
            else {
                return true;
            }
        }

        function AssignPartQty() {
            var PartQty = $("#ContentPlaceHolder1_LiPartSno :selected").length;
            $('#ContentPlaceHolder1_QTY').val(PartQty);
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
                                    <h3 class="page-title-head d-inline-block">Addtional Part Request</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Production</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Addtional Part Request</li>
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
                    <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
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
                                            RFP No</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlRFPNo" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlRFPNo_SelectIndexChanged"
                                            CssClass="form-control" Width="70%" ToolTip="Select RFP No">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12  p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Item Name</label>
                                        <asp:Label ID="lblDrawingNumber" Style="color: brown; display: none;" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlItemName" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlItemName_SelectIndexChanged"
                                            CssClass="form-control" Width="70%" ToolTip="Select Item Name">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblItemQty" Style="color: brown; display: none;" runat="server"></asp:Label>
                                        <%-- <asp:CheckBox ID="chkAddtionalPart" Text="New Part" AutoPostBack="true" OnCheckedChanged="chkAddtionalPart_OnCheckedChanged"
                                            runat="server" />--%>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Label ID="lblMaterialWarning" CssClass="blinking budgetaryhighligt lablqty"
                                            runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Part Name
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
                                <div class="col-sm-12 text-center p-t-20" visible="false" id="divNewPart" runat="server">
                                    <asp:LinkButton ID="lbtnSaveNewPart" CssClass="btn btn-cons btn-save" runat="server"
                                        OnClick="btnSaveNewPart_Click" Text="Save New Part"></asp:LinkButton>
                                </div>
                                <div id="divSpecsDetails" runat="server">
                                    <%--<div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label">
                                                Part Sno
                                            </label>
                                        </div>
                                        <div class="col-sm-6 text-left">
                                            <asp:ListBox ID="LiPartSno" runat="server" CssClass="form-control" Width="70%" SelectionMode="Multiple"
                                                OnChange="AssignPartQty();" ToolTip="Select Part Sno"></asp:ListBox>
                                        </div>
                                    </div>--%>
                                    <div class="col-sm-12 p-t-10" visible="false" id="divDrawing" runat="server">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label">
                                                View Drawing</label>
                                        </div>
                                        <div class="col-sm-6 text-left">
                                            <asp:ImageButton ID="imgViewDrawing" ImageUrl="../Assets/images/view.png" Style="height: 20px; width: 20px;"
                                                runat="server" OnClick="imgViewDrawing_Click" />
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 text-center">
                                        <asp:Label ID="lblPartName_Edit" Style="color: brown; font-size: larger;" runat="server"></asp:Label>
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
                                            <table id="tblFormulaFields" runat="server" style="width: 100%; overflow-wrap: anywhere;">
                                            </table>
                                        </div>
                                        <div class="col-sm-12 text-center p-t-10">
                                            <asp:LinkButton ID="btnCalculate" CssClass="btn btn-cons btn-save" runat="server"
                                                Text="CalCulate" OnClientClick="CalculateFomula();return false;"></asp:LinkButton>
                                            <asp:LinkButton ID="btnSave" CssClass="btn btn-cons btn-save" runat="server" Text="Save"
                                                OnClientClick="return SaveBOMSheet();"></asp:LinkButton>
                                            <asp:LinkButton ID="btnCancel" CssClass="btn btn-cons btn-save" runat="server" Text="Cancel"
                                                OnClick="btnCancel_Click"></asp:LinkButton>
                                            <asp:Button ID="btnLoad" OnClick="btnLoad_Click" runat="server" Style="display: none;"></asp:Button>
                                        </div>
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
                                        <div class="col-sm-12 p-t-10 text-center">
                                            <label class="form-label" style="color: Blue;">
                                                Item BOM Cost Details
                                            </label>
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvBOMCostDetails" runat="server" AutoGenerateColumns="true" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                OnRowDataBound="gvBOMCostDetails_OnRowDataBound" OnRowCommand="gvBOMCostDetails_OnRowCommand"
                                                HeaderStyle-HorizontalAlign="Center" DataKeyNames="DDID,MID,BOMID,PartName,LayOutFile,ProductionReworkBOMID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" ControlStyle-CssClass="nosort" ItemStyle-HorizontalAlign="Center">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkall" runat="server" onclick="return checkAll(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkitems" runat="server" AutoPostBack="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnedit" runat="server" Text="Edit" CommandName="EditBOM" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                ToolTip="Edit"><img src="../Assets/images/edit-ec.png" style="height:20px" alt=""></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" OnClientClick='<%# string.Format("return deleteConfirm({0},{1});",Eval("DDID"),Eval("BOMID")) %>'
                                                                ToolTip="Delete"> <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View Part Lay Out">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnPartLayOut" runat="server" Text="PartLayOut" CommandName="ViewLayOut"
                                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ToolTip="View"> <img src="../Assets/images/view.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="col-sm-12 text-center p-t-20">
                                            <asp:LinkButton ID="btnSendForApproval" CssClass="btn btn-cons btn-success" runat="server"
                                                Text="Send For Approval" OnClientClick="return ValidateApproval();" OnClick="btnSendForApproval_Click"></asp:LinkButton>
                                        </div>
                                        <asp:HiddenField ID="hdnBomID" runat="server" />
                                        <asp:HiddenField ID="hdnMID" runat="server" />
                                        <div class="col-sm-12 p-t-10 text-center" style="display: none;">
                                            <label class="form-label" style="display: contents;">
                                                Item BOM Cost</label>
                                            <asp:Label ID="lblItemBomTotalCost" Style="padding-left: inherit;" runat="server"></asp:Label>
                                        </div>
                                        <div id="idBomCost" style="display: none;">
                                            <div class="col-sm-12 p-t-20">
                                                <div class="col-sm-6 text-right">
                                                    <label class="form-label">
                                                        Total BOM Cost
                                                    </label>
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblCost" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 text-center">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--<asp:HiddenField ID="hdnBOMId" Value="0" runat="server" />--%>
                    <asp:HiddenField ID="hdnUserID" Value="0" runat="server" />
                    <asp:HiddenField ID="PartQty" runat="server" />
                    <asp:HiddenField ID="hdnPartSnoValid" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal" id="mpeView">
        <div class="modal-dialog" style="height: 100%;">
            <div class="modal-content" style="height: 100%;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" Style="height: 100%;">
                    <Triggers>
                        <%--<asp:PostBackTrigger ControlID="btndownloaddocs" />--%>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H2">
                                <div class="text-center">
                                    Documents
                                    <asp:Label ID="Label1" Text="" runat="server"></asp:Label>
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal">
                                x</button>
                        </div>
                        <div class="modal-body" style="height: 100%;">
                            <div class="inner-container" style="height: 100%;">
                                <div class="ip-div text-center" style="margin-bottom: 10px;">
                                    <%-- <asp:LinkButton Text="Download" CssClass="btn btn-save btn-cons AlignTop" ID="btndownloaddocs"
                                        OnClick="btndownloaddocs_Click" runat="server" />--%>
                                </div>
                                <div class="col-sm-12" style="height: 100%;">
                                    <iframe runat="server" id="ifrm" src="" style="width: 100%; height: 80%;" frameborder="0"></iframe>
                                </div>
                            </div>
                        </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
