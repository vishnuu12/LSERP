<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master"
    AutoEventWireup="true" CodeFile="ProjectExpensesEntry.aspx.cs" ClientIDMode="Predictable" Inherits="Pages_ProjectExpensesEntry" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function checkAllItems(ele) {
            debugger;
            if ($(ele).is(":checked")) {
                $('.dataTables_scrollBody').find('table').find('[type="checkbox"]').prop('checked', true);
                $('.dataTables_scrollBody').find('table').find('[type="text"]').addClass('mandatoryfield');

                $('.dataTables_scrollBody').find('table').find('[type="text"]').addClass("mandatoryfield");
                $('.dataTables_scrollBody').find('table').find('[type="text"]').before('<span class="textboxmandatory" style="color: red;">*</span>');

            }
            else {
                $('.dataTables_scrollBody').find('table').find('[type="checkbox"]').prop('checked', false);

                $('.dataTables_scrollBody').find('table').find('[type="text"]').removeClass("mandatoryfield");
                $('.dataTables_scrollBody').find('table').find('[type="text"]').closest('td').find('.textboxmandatory').remove();
            }
            return true;
        }

        function ValidateRFP(ele) {
            var msg = Mandatorycheck('ContentPlaceHolder1_divOutput');
            if (msg) {
                if ($('#ContentPlaceHolder1_gvRFPDetails').find('[type="checkbox"]:checked').not('#ContentPlaceHolder1_gvRFPDetails_chkall').length > 0) {
                    return true;
                }
                else {
                    ErrorMessage('Error', 'No RFP Selected');
                    hideLoader();
                    return false;
                }
            }
            else
                return false;
        }

        function RFPMandatoryField(ele) {
            if ($(ele).is(":checked")) {
                $(ele).closest('tr').find('[type="text"]').addClass("mandatoryfield");
                $(ele).closest('tr').find('[type="text"]').before('<span class="textboxmandatory" style="color: red;">*</span>');
            }
            else {
                $(ele).closest('tr').find('[type="text"]').removeClass("mandatoryfield");
                $(ele).closest('tr').find('[type="text"]').closest('td').find('.textboxmandatory').remove()
            }
        }

        function CalculateProductionMonthValue(ele) {
            var POValue = parseFloat($(ele).closest('tr').find('[type="hidden"]').val());
            var Per = parseFloat($(ele).val());

            $(ele).closest('tr').find('.cmpvalue').text(parseFloat((POValue * Per) / 100).toFixed(2));

            if ($(ele).val() == '')
                $(ele).closest('tr').find('.cmpvalue').text(0);

            $(ele).closest('tr').find('.osf').keyup();
        }

        function CalculateClosingSemiFinshed(ele) {
            var cmpval = $(ele).closest('tr').find('.cmpvalue').text();
            var osf = $(ele).val();

            $(ele).closest('tr').find('.csf').text(parseFloat(parseFloat(cmpval) + parseFloat(osf)).toFixed(2));
            if ($(ele).val() == '')
                $(ele).closest('tr').find('.csf').text(0);
            $(ele).closest('tr').find('.cmdval').keyup();
        }

        function CalculateFreshProduction(ele) {
            var cmpval = $(ele).closest('tr').find('.csf').text();
            var cmdvalue = $(ele).val();
            var osf = $(ele).closest('tr').find('.osf').val();

            $(ele).closest('tr').find('.fpcm').text(parseFloat((parseFloat(cmpval) + parseFloat(cmdvalue)) - parseFloat(osf)).toFixed(2));

            if ($(ele).val() == '')
                $(ele).closest('tr').find('.fpcm').text(0);
        }

        function CalculateTotalIssue(ele) {
            var currentmonthissue = parseFloat($(ele).closest('tr').find('.cmpissue').text());
            var expensevalue = parseFloat($(ele).val());

            $(ele).closest('tr').find('.totalissue').text(parseFloat(currentmonthissue + expensevalue).toFixed(2));
            if ($(ele).val() == '')
                $(ele).closest('tr').find('.totalissue').text(0);
        }

        function calculateConsumptionvalue() {
            var ope = $(event.target).closest('tr').find('.ope').val();
            var cpv = $(event.target).closest('tr').find('.cpv').val();
            var cmpissue = $(event.target).closest('tr').find('.cmpissue').text();

            if (ope == '')
                ope = 0;
            if (cpv == '')
                cpv = 0;

            $(event.target).closest('tr').find('.consumptionvalue').text(parseFloat(parseFloat(ope) + parseFloat(cmpissue) - parseFloat(cpv)).toFixed(2));
        }
        //Mandatorycheck('ContentPlaceHolder1_divInput');

        function deleteConfirm(PartId) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Part Name will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deletegvrow', PartId);
            });
            return false;
        }
        $(document).ready(function () {
            $('.table').DataTable({
                "retrieve": true,
                "paging": false,
                "order": [],
                scrollY: 550,
                scrollX: 550,
                fixedHeader: true
                //  responsive: true                             
            });
        });


    </script>

    <style type="text/css">
        .dataTables_scrollHead table th {
            font-size: 10px !important;
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
                                    <h3 class="page-title-head d-inline-block">Project Expenses Entry</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Production</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Project Expenses Entry</li>
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
            <asp:UpdatePanel ID="upPartMaster" runat="server">
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
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="col-sm-12">
                                            <asp:GridView ID="gvRFPDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium uniquedatatable"
                                                HeaderStyle-HorizontalAlign="Center"
                                                DataKeyNames="RFPHID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="" ItemStyle-CssClass="text-center"
                                                        HeaderStyle-CssClass="text-center">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkall" runat="server" onclick="return checkAllItems(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkQC" runat="server" onclick="return RFPMandatoryField(this);" AutoPostBack="false" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="RFP No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRFPNo" runat="server" Text='<%# Eval("RFPNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Customer Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCustomerName" runat="server" Text='<%# Eval("ProspectName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="PO Value">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPOValue" runat="server" Text='<%# Eval("POValue")%>'></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="P.O.V">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPendingOrderValues" Text='<%# Eval("PendingOrderValue")%>' runat="server"></asp:Label>
                                                            <asp:HiddenField ID="hdnPopendingValue" Value='<%# Eval("PendingOrderValue")%>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="C.M.P ( % )">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtCMPPer" runat="server" Style="width: 100px;" onkeypress="return validationDecimal(this);"
                                                                onkeyup="CalculateProductionMonthValue(this);" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="C.M.V">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCMPValue" CssClass="cmpvalue" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="O.S.F">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtOSF" runat="server" Style="width: 100px;" onkeypress="return validationDecimal(this);"
                                                                onkeyup="CalculateClosingSemiFinshed(this);" CssClass="form-control osf"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="C.S.F">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCSF" CssClass="csf" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="C.M.D.V">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtCurrentMonthDispatchValue" Style="width: 100px;" onkeypress="return validationDecimal(this);"
                                                                onkeyup="CalculateFreshProduction(this);" runat="server" CssClass="form-control cmdval"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="F.P.C.M">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFPCM" CssClass="fpcm" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="O.P.E">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtOpenProjectExpenses" Style="width: 100px;" onkeyup="calculateConsumptionvalue(this);" runat="server"
                                                                CssClass="form-control ope"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="C.M.I">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCurrentMonthIssue" CssClass="cmpissue" runat="server"
                                                                Text='<%# Eval("CurrentMonthIssuedQty")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="C.P.V">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtCPV" runat="server" Style="width: 100px;"
                                                                onkeypress="return validationDecimal(this);" onkeyup="calculateConsumptionvalue(this);"
                                                                CssClass="form-control cpv"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="C.V">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCV" CssClass="consumptionvalue"
                                                                runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>
                                            <asp:HiddenField ID="hdnPartID" runat="server" Value="0" />
                                        </div>

                                        <div class="col-sm-12 text-center">
                                            <asp:LinkButton ID="btnProjectExpenses" runat="server" Text="Save" CssClass="btn btn-cons btn-save AlignTop"
                                                OnClientClick="return ValidateRFP(this);"
                                                OnClick="btnProjectExpenses_Click"></asp:LinkButton>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <asp:GridView ID="gvProjectExpensesDetails" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                HeaderStyle-HorizontalAlign="Center"
                                                DataKeyNames="PEEID">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="RFP No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRFPNo" runat="server" Text='<%# Eval("RFPNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Customer Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCustomerName" runat="server" Text='<%# Eval("ProspectName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Current Month Value">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCMPValue" Text='<%# Eval("CurrentMonthValue")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Opening Semi Finished">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOSFValue" Text='<%# Eval("OpeningSemiFinshed")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Closing Semi Finished">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCSFValue" Text='<%# Eval("ClosingSemiFinshed")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Current Month Dispatch Value">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCMDvalue" Text='<%# Eval("CurrentMonthDispatchValue")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Fresh Production For Current Month">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFPCM" Text='<%# Eval("FreshProductionCurrentMonth")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Open Project Expenses">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblope" Text='<%# Eval("OpenProjectExpenses")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Closing Project Value">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcpv" Text='<%# Eval("ClosingProjectvalue")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Consumption Value">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcv" Text='<%# Eval("Consumptionvalue")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Delete">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDelete" runat="server" OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("PEEID")) %>'>
                                                            <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
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
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

