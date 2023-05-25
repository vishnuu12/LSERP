<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="StandardItemEntry.aspx.cs" Inherits="Pages_StandardItemEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../Assets/scripts/math.js"></script>
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

        function deleteConfirm(SITCID, BOMID) {
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
                    __doPostBack('deletegvrow', SITCID + '/' + BOMID);
                });
                return false;
            }
        }

        function ShareStandardItem(SIEHID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, Once Item Shared No Further Edit",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Share it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('ShareStandardItem', SIEHID);
            });
            return false;
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
            var btnSaveEnapled = $('#<%=btnSaveBom.ClientID %>').attr('class');

            //if (!btnSaveEnapled.includes("btnSaveDisabled")) {
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

              <%--      EnquiryNumber = $('#<%=ddlItemName.ClientID %>').children("option:selected").val();
                    VersionNumber = $('#<%=ddlVersionNumber.ClientID %>').children("option:selected").text();--%>
                MaterialName = $('#<%=ddlMaterialName.ClientID %>').children("option:selected").val();

                // strDetails.push(EnquiryNumber.split('/')[0] + "/" + VersionNumber + "/" + MaterialName.split('/')[1] + "/" + MaterialName.split('/')[0]);

                if ($('#ContentPlaceHolder1_chkAddtionalPart').is(':checked'))
                    AddtionalPart = 'Yes';
                else
                    AddtionalPart = 'No';

                strDetails.push(0 + "/" + $('#ContentPlaceHolder1_hdnSIEHID').val() + "/" + $('#ContentPlaceHolder1_hdnMID').val() + "/" + $('#ContentPlaceHolder1_hdnBomID').val() + "/" + $('#ContentPlaceHolder1_txtRemarks').val() + "/" + $('#ContentPlaceHolder1_ddlDrawingSequenceNumber').val() + "/" + AddtionalPart);

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

                PageMethods.set_path('StandardItemEntry.aspx');
                PageMethods.SaveBOMDetails(SaveBOMDetails, strDetails, AfterAssignedProcess);
            }
            else {
                ErrorMessage('Error', 'Field Required:' + RF + '');
            }
            return false;
            //}
            //else {

            //    ErrorMessage('Error', 'Item Moved Into Next Stage');
            //    return false;
            //}
        }

        function AfterAssignedProcess(ReturnMsg) {
            if (ReturnMsg == 'Added') {
                //$("<thead></thead>").append($("#<%=tblInputFields.ClientID %> tr:first")).prependTo($("#<%=tblInputFields.ClientID %>"));
                SuccessMessage("Success", "BOM Details Saved Successfully");
            }
            else {
                SuccessMessage("Success", "BOM Details Updated successfully");
                //                document.getElementById('<%=ddlMaterialName.ClientID %>').disabled = false;
            }
            document.getElementById('<%=btnLoad.ClientID %>').click();
        }

        function AddBomPopUp() {
            $('#mpeBom').modal('show');
            return false;
        }


    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                                    <h3 class="page-title-head d-inline-block">Standard Item Entry</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Design</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">standard Item Entry</li>
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
            <asp:UpdatePanel ID="upQuote" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnSave" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" runat="server">
                            <div class="col-sm-12 text-center p-t-10" id="btngroup" runat="server">
                                <asp:LinkButton ID="btnAddNew" runat="server" Text="Add Item" CssClass="btn btn-success add-emp btnAddNew" OnClick="btnAddNew_Click"
                                    Style="margin: 5px;"></asp:LinkButton>
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center input_section">
                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label">
                                            Item Name</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:DropDownList ID="ddlItemname" runat="server" AutoPostBack="false" CssClass="form-control"
                                            Width="70%" ToolTip="Select Item Name">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Drawing No</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                        <asp:TextBox ID="txtDrawingname" runat="server" CssClass="form-control mandatoryfield"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-2 text-right">
                                        <label class="form-label mandatorylbl">
                                            Revision Number</label>
                                    </div>
                                    <div class="col-sm-4 text-left">
                                        <asp:TextBox ID="txtRevision" runat="server" onkeypress="return validationNumeric(this);" CssClass="form-control mandatoryfield"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-2 text-right">
                                        <label class="form-label mandatorylbl">
                                            Size</label>
                                    </div>
                                    <div class="col-sm-4 text-left">
                                        <asp:DropDownList ID="ddlsize" runat="server" ToolTip="Select Size" CssClass="form-control mandatoryfield">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --" />
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4">
                                        <asp:RadioButtonList ID="rblTagNoItemCodeMatCode" runat="server" CssClass="radio radio-success"
                                            RepeatDirection="Horizontal">
                                            <asp:ListItem Text=" Tag No" Value="TN"></asp:ListItem>
                                            <asp:ListItem Text="Item Code" Value="IC"></asp:ListItem>
                                            <asp:ListItem Text="Material Code" Value="MC"></asp:ListItem>
                                            <asp:ListItem Text="None" Value="None" Selected="True"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-sm-4 text-left">
                                        <asp:TextBox ID="txt_tagno" runat="server" TabIndex="6" placeholder="Enter Code"
                                            ToolTip="Enter Tag no" TextMode="MultiLine" CssClass="form-control" MaxLength="300"
                                            autocomplete="nope" />
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-2 text-right">
                                        <label class="form-label">
                                            Description</label>
                                    </div>
                                    <div class="col-sm-4 text-left">
                                        <asp:TextBox ID="txt_description" runat="server" TabIndex="6" placeholder="Enter Item Description"
                                            ToolTip="Enter Attachment Description" TextMode="MultiLine" CssClass="form-control"
                                            MaxLength="300" autocomplete="nope" />
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-2 text-right">
                                        <label class="form-label mandatorylbl">
                                            Pressure
                                        </label>
                                        <span style="color: coral;"></span>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txt_pressure" runat="server" TabIndex="6" placeholder="Enter Item Pressure"
                                            ToolTip="Enter Item Pressure" TextMode="MultiLine" CssClass="form-control mandatoryfield"
                                            MaxLength="300" autocomplete="nope" />
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:DropDownList ID="ddlPresureUnits" runat="server" ToolTip="Select Presure Units"
                                            CssClass="form-control mandatoryfield">
                                            <asp:ListItem Selected="True" Value="0" Text="-- Select --" />
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-2 text-right">
                                        <label class="form-label mandatorylbl">
                                            Temperature</label><span style="color: coral;">(Deg. C)</span>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txt_temperature" runat="server" TabIndex="6" placeholder="Enter Item Temperature"
                                            ToolTip="Enter Item Temperature" TextMode="MultiLine" CssClass="form-control mandatoryfield"
                                            MaxLength="300" autocomplete="nope" />
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4" style="text-align: end;">
                                        <label class="form-label mandatorylbl">
                                            Movement
                                        </label>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:TextBox ID="txt_movement" runat="server" TabIndex="6" placeholder="Enter Item Movement"
                                            ToolTip="Enter Item Movement" TextMode="MultiLine" CssClass="form-control mandatoryfield"
                                            MaxLength="300" autocomplete="nope" />
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4" style="text-align: end;">
                                        <label class="form-label mandatorylbl">
                                            Drawing File</label>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:FileUpload ID="fAttachment" onchange="DocValidation(this);" runat="server" class="form-control mandatoryfield"
                                            ClientIDMode="Static" />
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <asp:LinkButton Text="Save" CssClass="btn btn-cons btn-save  AlignTop" ID="btnSave"
                                        OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divInput')" OnClick="btnSaveStandardItemEntry_Click"
                                        runat="server" />
                                    <asp:LinkButton Text="Cancel" CssClass="btn btn-cons btn-save  AlignTop" ID="btnCancel"
                                        OnClick="btnCancel_Click"
                                        runat="server" />
                                </div>
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="col-sm-12 p-t-10 p-l-0 p-r-0" style="overflow-x: scroll;">
                                            <asp:GridView ID="gvStandardItemDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium" OnRowCommand="gvStandardItemDetails_OnRowCommand"
                                                DataKeyNames="SIEHID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left"
                                                        HeaderStyle-Width="20%" ItemStyle-Width="20%">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnaddBom" runat="server" CommandName="AddBom" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                                <asp:Label ID="lblitemname" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                            </asp:LinkButton>
                                                            <asp:Label ID="lblItemName_h" runat="server" Style="display: none;" Text='<%# Eval("ItemName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Size" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitemsize" runat="server" Text='<%# Eval("ItemSize")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tag No" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltagno" runat="server" Text='<%# Eval("TagNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldescription" runat="server" Text='<%# Eval("Description")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Revision Number" ItemStyle-HorizontalAlign="left"
                                                        HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblrevisionno" runat="server" Text='<%# Eval("RevisionNumber")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pressure" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblpressure" runat="server" Text='<%# Eval("Pressure")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Temperature" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbltemp" runat="server" Text='<%# Eval("Temperature")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Movement" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblmovement" runat="server" Text='<%# Eval("Movement")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEdit_itemdetails" runat="server"
                                                                CommandArgument='<%#((GridViewRow) Container).RowIndex %>' CommandName="EditItem"><img src="../Assets/images/edit-ec.png" alt="" />
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Share" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnShare" runat="server"
                                                                OnClientClick='<%# string.Format("return ShareStandardItem({0});",Eval("SIEHID")) %>'>
                                                                <img src="../Assets/images/icon_update.png" alt="" width="30px" />
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="col-sm-12 p-t-10 text-center" id="divsharebutton" runat="server" visible="false">
                                            <asp:LinkButton Text="Share Item" CssClass="btn btn-cons btn-save  AlignTop" ID="btnShare"
                                                runat="server" OnClientClick="return ShareItem();" />
                                        </div>
                                        <asp:HiddenField ID="hdnSIEHID" Value="0" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="modal" id="mpeBom" style="padding-left: 0px !important;">
        <div class="modal-dialog" style="max-width: 91% !important;">
            <div class="modal-content">
                <asp:UpdatePanel ID="upView" runat="server" UpdateMode="Always">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 id="popuplbl" runat="server" style="color: black;" class="modal-title ADD">
                                <asp:Label ID="lblitemname_h" runat="server"></asp:Label></h4>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="detailsdiv" runat="server">
                                <div class="inner-container">
                                    <div id="divAddBom" runat="server">
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
                                                <asp:LinkButton ID="btnSaveBom" CssClass="btn btn-cons btn-save" runat="server" Text="Save"
                                                    OnClientClick="return SaveBOMSheet();"></asp:LinkButton>
                                                <asp:LinkButton ID="btnCancelBom" CssClass="btn btn-cons btn-save" runat="server" Text="Cancel"
                                                    OnClick="btnCancelBom_Click"></asp:LinkButton>
                                                <asp:Button ID="btnLoad" OnClick="btnLoad_Click" runat="server" Style="display: none;"></asp:Button>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="divOutputBom" class="output_section" runat="server" visible="false">
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
                                                            HeaderStyle-HorizontalAlign="Center" DataKeyNames="SITCID,MID,BOMID,PartName,LayOutFile">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Edit">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnedit" runat="server" Text="Edit" CommandName="EditBOM" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                            ToolTip="Edit"><img src="../Assets/images/edit-ec.png" style="height:20px" alt=""></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" OnClientClick='<%# string.Format("return deleteConfirm({0},{1});",Eval("SITCID"),Eval("BOMID")) %>'
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
                                                            HeaderStyle-HorizontalAlign="Center" DataKeyNames="SITCID,MID,BOMID,PartName,LayOutFile">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Edit">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnedit" runat="server" Text="Edit" CommandName="EditBOM" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                            ToolTip="Edit"><img src="../Assets/images/edit-ec.png" style="height:20px" alt=""></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Delete">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnDelete" runat="server" Text="Delete" OnClientClick='<%# string.Format("return deleteConfirm({0},{1});",Eval("SITCID"),Eval("BOMID")) %>'
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
                                                    <div class="col-sm-12 text-center p-t-10">
                                                        <%-- <asp:LinkButton ID="btnEstimationCompleted" CssClass="btn btn-cons btn-success" runat="server"
                                                            Text="Save Estimation" OnClick="btnEstimationCompleted_Click"></asp:LinkButton>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="hdnPODID" runat="server" Value="0" />
                            <div class="modal-footer">
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-10">
                                    </div>
                                    <div class="col-sm-2">
                                        <div class="form-group">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="PODID" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>

