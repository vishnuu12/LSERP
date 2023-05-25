<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="BOMMaterialSpecSheet.aspx.cs" Inherits="Pages_BOMMaterialSpecSheet"
    ClientIDMode="Predictable" %>

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
                    if ($(this).attr("id").split('_')[1] != 'M/CRATE') {
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
            if ($('#ContentPlaceHolder1_ddlDrawingSequenceNumber').val() == 0) {
                if (RF == "") {
                    RF = "select Drawing Sequence Number";
                }
                else {
                    RF = RF + ',' + "select Drawing Sequence Number";
                }
                msg = false;
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
                var AddtionalPart;
                msg = ValidateInputFields();

                $('#<%=tblInputFields.ClientID %> input[type="text"]').each(function () {
                    if ($(this).val() == "") {
                        MSMIDValue.push(0);
                    }
                    else {
                        MSMIDValue.push($(this).val());
                    }
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

                    if ($('#ContentPlaceHolder1_chkAddtionalPart').is(':checked'))
                        AddtionalPart = 'Yes';
                    else
                        AddtionalPart = 'No';

                    strDetails.push(EnquiryNumber.split('/')[0] + "/" + VersionNumber + "/" + $('#ContentPlaceHolder1_hdnMID').val() + "/" + $('#ContentPlaceHolder1_hdnBomID').val() + "/" + "Testing" + "/" + $('#ContentPlaceHolder1_ddlDrawingSequenceNumber').val() + "/" + AddtionalPart);

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

                    PageMethods.set_path('BOMMaterialSpecSheet.aspx');
                    PageMethods.SaveBOMDetails(SaveBOMDetails, strDetails, $('#ContentPlaceHolder1_txtRemarks').val(), AfterAssignedProcess);
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
                //$("<thead></thead>").append($("#<%=tblInputFields.ClientID %> tr:first")).prependTo($("#<%=tblInputFields.ClientID %>"));
                SuccessMessage("Success", "BOM Details Saved Successfully");
            }
            else if (ReturnMsg == 'Updated') {
                SuccessMessage("Success", "BOM Details Updated successfully");
                //                document.getElementById('<%=ddlEnquiryNumber.ClientID %>').disabled = false;
                //                document.getElementById('<%=ddlVersionNumber.ClientID %>').disabled = false;
                //                document.getElementById('<%=ddlMaterialName.ClientID %>').disabled = false;
            }
            else
                InfoMessage('Information', ReturnMsg);

            document.getElementById('<%=btnLoad.ClientID %>').click();
        }

        function ShowLayoutAttachementsPopUp() {
            $('#mpeAddAttachements').modal('show');
            return false;
        }


        function deleteAllPartBomDetails() {
            swal({
                title: "Are you sure?",
                text: "If Yes, the BOMID Delete permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                __doPostBack('deleteAllBomPart');
            });
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
                                    <h3 class="page-title-head d-inline-block">BOM Material Sheet</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Design</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">BOM Material Sheet</li>
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
                    <asp:PostBackTrigger ControlID="imgViewDrawing" />
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
                                        <asp:Label ID="lblItemQty" Style="color: brown;" runat="server"></asp:Label>
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
                                            <asp:ListItem Text="--Select--" Value="-1" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4">
                                    </div>
                                    <%--OnCheckedChanged="chkAddtionalPart_CheckedChanged"--%>
                                    <div class="col-sm-4">
                                        <asp:CheckBox ID="chkAddtionalPart" Text="AddtionalPart" runat="server" AutoPostBack="false" />
                                    </div>
                                    <div class="col-sm-4">
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
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Drawing Sequence Number
                                        </label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlDrawingSequenceNumber" runat="server" CssClass="form-control mandatoryfield"
                                            Width="70%" ToolTip="Select Material Name" TabIndex="1">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
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
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Remarks</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Rows="2" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
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
                                                HeaderStyle-HorizontalAlign="Center" DataKeyNames="ETCID,MID,BOMID,PartName,LayOutFile">
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
                                                    <asp:TemplateField HeaderText="View Part Lay Out">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnPartLayOut" runat="server" Text="PartLayOut" CommandName="ViewLayOut"
                                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ToolTip="View"> <img src="../Assets/images/view.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <asp:HiddenField ID="hdnBomID" runat="server" />
                                        <asp:HiddenField ID="hdnMID" runat="server" />

                                        <asp:HiddenField ID="hdnBOMRemarks" Value="" runat="server" />

                                        <div class="col-sm-12 p-t-10 text-center">
                                            <label class="form-label" style="display: contents;">
                                                Item BOM Cost</label>
                                            <asp:Label ID="lblItemBomTotalCost" Style="padding-left: inherit;" runat="server"></asp:Label>
                                        </div>
                                        <div class="col-sm-12 p-t-10 text-center">
                                            <label class="form-label" style="color: Blue;">
                                                Addtional Part BOM Cost Details
                                            </label>
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvAddtionalPartDetails" runat="server" AutoGenerateColumns="true"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                OnRowDataBound="gvBOMCostDetails_OnRowDataBound" OnRowCommand="gvBOMCostDetails_OnRowCommand"
                                                HeaderStyle-HorizontalAlign="Center" DataKeyNames="ETCID,MID,BOMID,PartName,LayOutFile">
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
                                                    <asp:TemplateField HeaderText="View Part Lay Out">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnPartLayOut" runat="server" Text="PartLayOut" CommandName="ViewLayOut"
                                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ToolTip="View"> <img src="../Assets/images/view.png" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="col-sm-12 p-t-10 text-center">
                                            <label class="form-label" style="display: contents;">
                                                Addtional BOM Cost</label>
                                            <asp:Label ID="lblAddtionalBOMTotalCost" Style="padding-left: inherit;" runat="server"></asp:Label>
                                        </div>
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
                                        <div class="col-sm-12 p-t-10">
                                            <div class="col-sm-4 text-right">
                                                <label class="form-label">
                                                    Add Layout</label>
                                            </div>
                                            <div class="col-sm-1 text-left">
                                                <asp:LinkButton ID="btnAddLayout" Style="height: 20px; width: 20px;"
                                                    runat="server" OnClick="btnAddLayout_Click"> <img src="../Assets/images/add1.png" /></asp:LinkButton>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 text-center p-t-10">
                                            <asp:LinkButton ID="btnEstimationCompleted" CssClass="btn btn-cons btn-success" runat="server"
                                                Text="Save Estimation" OnClick="btnEstimationCompleted_Click"></asp:LinkButton>
                                        </div>

                                        <div class="col-sm-12 p-t-10 text-center">
                                            <asp:LinkButton ID="lbtnDeleteAll" CssClass="btn btn-cons btn-save AlignTop" runat="server"
                                                Text="Delete All Bom Part" OnClientClick="return deleteAllPartBomDetails();"></asp:LinkButton>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%--<asp:HiddenField ID="hdnBOMId" Value="0" runat="server" />--%>
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
    <div class="modal" id="mpeAddAttachements">
        <div class="modal-dialog" style="height: 100%; margin-right: 40%;">
            <div class="modal-content" style="height: fit-content; width: 1200px;">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always" Style="height: 100%;">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnSaveAttachements" />
                        <asp:PostBackTrigger ControlID="gvPartLayoutDetails" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD text-center" id="H1">
                                <div class="text-center">

                                    <asp:Label ID="lblItemName_Layout" Text="" Style="color: brown;" runat="server"></asp:Label>
                                </div>
                            </h4>
                            <button type="button" cssclass="close btn-primary-purple" data-dismiss="modal">
                                x</button>
                        </div>
                        <div class="modal-body" style="height: 100%;">
                            <div class="inner-container" style="height: 100%;">
                                <div class="ip-div text-center" style="margin-bottom: 10px;">
                                </div>
                                <div id="divAttach" runat="server">
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label mandatorylbl">
                                                Part Name
                                            </label>
                                        </div>
                                        <div class="col-sm-6 text-left">
                                            <asp:DropDownList ID="ddlBomPartName" runat="server" CssClass="form-control mandatoryfield"
                                                Width="70%" ToolTip="Select Part Name" TabIndex="1">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label mandatorylbl">
                                                Layout Attachements
                                            </label>
                                        </div>
                                        <div class="col-sm-6 text-left">
                                            <asp:FileUpload ID="fbLayoutAttach_p" runat="server" onchange="DocValidation(this);"
                                                CssClass="form-control mandatoryfield" Width="70%"></asp:FileUpload>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4 text-right">
                                            <label class="form-label mandatorylbl">
                                                Remarks
                                            </label>
                                        </div>
                                        <div class="col-sm-6 text-left">
                                            <asp:TextBox ID="txtProductionpartRemarks" runat="server" TextMode="MultiLine" PlaceHolder="Enter Remarks" Rows="2" CssClass="form-control mandatoryfield"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-2">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10 text-center">
                                        <asp:LinkButton ID="btnSaveAttachements" CssClass="btn btn-cons btn-save AlignTop"
                                            runat="server" Text="Save" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divAttach');"
                                            OnClick="btnSaveLayOutAttachement_Click"></asp:LinkButton>
                                    </div>
                                </div>
                                <div id="divLayout" class="output_section" runat="server" visible="true">
                                    <div class="page-container" style="float: left; width: 100%">
                                        <div class="main-card">
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <p class="h-style">
                                                            <asp:Label ID="Label3" runat="server"></asp:Label>
                                                        </p>
                                                    </div>
                                                    <div class="col-md-6 ex-icons" id="div2" runat="server" visible="false">
                                                    </div>
                                                </div>

                                                <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                    <asp:GridView ID="gvPartLayoutDetails" runat="server" AutoGenerateColumns="false"
                                                        ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                        OnRowCommand="gvPartLayoutDetails_OnRowCommand"
                                                        HeaderStyle-HorizontalAlign="Center" DataKeyNames="BOMID,LayOutFile">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Part Name" ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center"
                                                                HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPartname" runat="server" Text='<%# Eval("MaterialName")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View Part Lay Out" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnPartLayOut" runat="server" Text="PartLayOut" CommandName="ViewLayout"
                                                                        CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" ToolTip="View"> <img src="../Assets/images/view.png" /></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Remarks" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Edit" HeaderStyle-Width="10%" ItemStyle-Width="10%">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnedit" runat="server" Text="Edit" CommandName="EditLayout" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                        ToolTip="Edit"><img src="../Assets/images/edit-ec.png" style="height:20px" alt=""></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
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
    </div>
</asp:Content>
