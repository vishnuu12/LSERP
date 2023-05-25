<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" 
    CodeFile="ContractorJobAllList.aspx.cs" Inherits="Pages_ContractorJobAllList" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">
        function DCqtyvalidate(ele) {
             if (parseFloat(document.getElementById('<%=lblBalanceAmount.ClientID%>').innerText) < parseFloat($(ele).val())) {
                InfoMessage("Amount shouldn't greater than Total Amount");
                $(ele).val('');
            }
        }

        function AmountPercentage(e) {
            if (document.getElementById('<%=lblAmount.ClientID%>').innerText == 0.00) {
                InfoMessage("Amount is Zero");
                $(e).val('');
            }
            else {
                var percentage = parseFloat($(e).val());
                var total = parseFloat(document.getElementById('<%=lblBalanceAmount.ClientID%>').innerText);
                var sum = (total / 100) * percentage;
                if (total < parseFloat(sum)) {
                    InfoMessage("Amount shouldn't greater than Total Amount");
                    $(e).val('');
                    document.getElementById('<%=txtAmount.ClientID%>').value = '';

                }
                else {
                    document.getElementById('<%=txtAmount.ClientID %>').value = parseFloat(sum);

                }
            }
        }

        function deleteConfirm(CTPDID) {

            swal({
                title: "Are you sure?",
                text: "If Yes, the Row will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('deletegvrow', CTPDID);
            });
            return false;
        }

        function ViewJobComplete(RFPDID) {
            __doPostBack('ViewJobComplete', RFPDID);
        }
        function ViewJobInComplete(RFPDID) {
            __doPostBack('ViewJobInComplete', RFPDID);
        }

        function ShowPartPopUp() {
            $('#mpePartDetails').modal({
                backdrop: 'static'
            });
            ShowMPDetailsDataTable();
            return false;
        }
        function hidePartDetailpopup() {
            $('#mpePartDetails').hide();
            $('div').removeClass('modal-backdrop');
            showDataTable();
            return false;
        }
    </script>
                <style type="text/css">
        table#ContentPlaceHolder1_gvContractorJobOrderDetails td {
            color: #000;
            font-weight: bold;
        }

                table#ContentPlaceHolder1_gvContractorForm td {
            color: #000;
            font-size:small;
            font-weight: bold;
        }                
                table#ContentPlaceHolder1_gvMaterialPlanningDetails td {
            color: #000;
            font-size:small;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                                    <h3 class="page-title-head d-inline-block">Contractor Job List</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Production</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Contractor Job List</li>
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
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                       <div id="divView" runat="server">
                           <div class="ip-div text-center">
                               
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Contractor Name</label>
                                    </div>
                                    <div class="col-sm-6 text-left">
                                            <asp:DropDownList ID="ddContractorName" runat="server" ToolTip="Select Contractor Name" AutoPostBack="true" 
                                                OnChange="showLoader();" OnSelectedIndexChanged="ddContractorName_OnSelectIndexChanged"  CssClass="form-control mandatoryfield">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                           </div>
                       </div>
                        <div id="divAdd" class="btnAddNew" runat="server">
                            <div class="col-sm-12 p-t-10">
                                 <asp:GridView ID="gvContractorJobOrderDetails" runat="server" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" 
                                     OnRowCommand="gvContractorJobOrderDetails_RowCommand"
                                     CssClass="table table-hover table-bordered medium" >
                                    <Columns>
                                       <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                              <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                          </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Contractor Name">
                                          <ItemTemplate>
                                            <asp:LinkButton ID="btnTC" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>' 
                                             CommandName="ContractorAmount">
                                              <asp:Label ID="lblContractorName" runat="server" Text='<%# Eval("ContractorName")%>'></asp:Label>
                                            </asp:LinkButton>
                                          </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Item Name">
                                          <ItemTemplate>
                                              <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName")%>'></asp:Label>
                                          </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Required Weight">
                                          <ItemTemplate>
                                              <asp:Label ID="lblUnitRequiredWeight" runat="server" Text='<%# Eval("RequiredWeight")%>'></asp:Label>
                                          </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Unit Rate">
                                          <ItemTemplate>
                                              <asp:Label ID="lblUnitRate" CssClass="POAvailableAvalibleQty" runat="server" Text='<%# Eval("UnitRate")%>'></asp:Label>
                                          </ItemTemplate>
                                       </asp:TemplateField>
<%--                                       <asp:TemplateField HeaderText="Unit Rate">
                                          <ItemTemplate>
                                              <asp:Label ID="lblUnitRate" runat="server" Text='<%# Eval("UnitRate")%>'></asp:Label>
                                          </ItemTemplate>
                                       </asp:TemplateField>--%>
                                       <asp:TemplateField HeaderText="Total Rate">
                                          <ItemTemplate>
                                              <asp:Label ID="lblTotalUnitRate" CssClass="POAvailableAvalibleQty" runat="server" Text='<%# Eval("TotalRate")%>'></asp:Label>
                                          </ItemTemplate>
                                       </asp:TemplateField>
                                       
                                       <asp:TemplateField HeaderText="View Job Complete" HeaderStyle-CssClass="text-center"
                                       ItemStyle-CssClass="text-center">
                                          <ItemTemplate>
                                              <asp:LinkButton ID="lbtnComplete" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                            OnClientClick='<%# string.Format("return ViewJobComplete({0});",Eval("RFPDID")) %>'>  <img src="../Assets/images/view.png" /></asp:LinkButton>
                                          </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="View Job InComplete" HeaderStyle-CssClass="text-center"
                                       ItemStyle-CssClass="text-center">
                                          <ItemTemplate>
                                             <asp:LinkButton ID="lbtnInComplete" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                             OnClientClick='<%# string.Format("return ViewJobInComplete({0});",Eval("RFPDID")) %>'>  <img src="../Assets/images/view.png" /></asp:LinkButton>
                                          </ItemTemplate>
                                       </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:HiddenField ID="hdnCTDID" Value="0" runat="server" /> 
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">

                                <div class="col-sm-12">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                           Contractor Name :  
                                        </label>
                                    </div>
                                    <div class="col-sm-4 text-left">
                                        <asp:Label ID="txtContractorName" Style="color: brown;font-size: 20px;font-family: none;font-weight: bold;" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Amount In Percentage
                                        </label>
                                    </div>
                                    <div class="col-sm-4 text-left">
                                        <asp:TextBox ID="txtAmountPercentage" runat="server" placeholder="Enter Amount in Percentage" onblur="AmountPercentage(this);" Rows="3" CssClass="form-control mandatoryfield"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-1 text-left">
                                         <span class="form-label " style="color:brown;"> Total : </span>
                                    </div>
                                    <div class="col-sm-1 text-left">
                                        <asp:Label ID="lblAmount" Style="color: red;font-size: 15px;font-weight: bold;" CssClass="lblAmountTotal" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                 </div>

                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Amount
                                        </label>
                                    </div>
                                    <div class="col-sm-4 text-left">
                                        <asp:TextBox ID="txtAmount" runat="server" onkeypress="return validationDecimal(this);" onblur="DCqtyvalidate(this);" placeholder="Enter Amount" Rows="3" CssClass="form-control mandatoryfield"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-1 text-left">
                                        <span class="form-label " style="color:brown;"> Paid : </span>
                                    </div>
                                    <div class="col-sm-1 text-left">
                                         <asp:Label ID="lblPaidAmount" Style="color: red;font-size: 15px;font-weight: bold;" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label mandatorylbl">
                                            Payment Date
                                        </label>
                                    </div>
                                    <div class="col-sm-4 text-left">
                                           <asp:TextBox ID="txtPaymentDate" runat="server" Width="70%" CssClass="form-control datepicker mandatoryfield"
                                                AutoComplete="off" ToolTip="Enter Payment Date" placeholder="Enter Payment Date">
                                            </asp:TextBox>
                                    </div>
                                    <div class="col-sm-1 text-left"">
                                        <span class="form-label " style="color:brown;"> Balance: </span>
                                    </div>
                                    <div class="col-sm-1 text-left">
                                        <asp:Label ID="lblBalanceAmount" Style="color: red;font-size: 15px;font-weight: bold;" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-4 text-right">
                                        <label class="form-label ">
                                            Remarks
                                        </label>
                                    </div>
                                    <div class="col-sm-4 text-left">
                                        <asp:TextBox ID="txtRemarks" runat="server" placeholder="Enter Remarks" Rows="3" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-4">
                                    </div>
                                </div>

                                <div class="col-sm-12 p-t-10">
                                    <asp:LinkButton ID="btnSaveContractor" OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divInput');" OnClick="btnSaveContractor_Click" runat="server" Text="Save" CssClass="btn btn-cons btn-save AlignTop"></asp:LinkButton>
                                    <asp:LinkButton ID="btnCancel" OnClick="btnCancel_Click" runat="server" Text="Cancel" CssClass="btn btn-cons btn-danger AlignTop"></asp:LinkButton>
                                </div>

                            </div>
                        </div>
                    </div>
                        
                        <div id="divOutput" runat="server">
                            <div class="col-sm-12 p-t-10">
                                 <asp:GridView ID="gvContractorForm" runat="server" AutoGenerateColumns="False"
                                    ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" 
                                     CssClass="table table-hover table-bordered medium" 
                                     OnRowCommand="gvContractorForm_RowCommand" DataKeyNames="CTPDID">
                                    <Columns>
                                       <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                          <ItemTemplate>
                                              <asp:Label ID="lblSno" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                          </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Contractor Name">
                                          <ItemTemplate>
                                              <asp:Label ID="lblContractorsName" runat="server" Text='<%# Eval("CONTRACTORNAME")%>'></asp:Label>
                                          </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Amount In Percentage">
                                          <ItemTemplate>
                                              <asp:Label ID="lblAmountInPercentage" runat="server" Text='<%# Eval("AmountInPercentage")%>'></asp:Label>
                                          </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Amount">
                                          <ItemTemplate>
                                              <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount")%>'></asp:Label>
                                          </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Payment Date">
                                          <ItemTemplate>
                                              <asp:Label ID="lblPaymentDate" runat="server" Text='<%# Eval("PaymentDate")%>'></asp:Label>
                                          </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Remarks">
                                          <ItemTemplate>
                                              <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label>
                                          </ItemTemplate>
                                       </asp:TemplateField>
                                      <%-- <asp:TemplateField HeaderText="Edit" ItemStyle-CssClass="text-center">
                                           <ItemTemplate>
                                              <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                              <img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                          </ItemTemplate>
                                       </asp:TemplateField>--%>
                                       <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                          <ItemTemplate>
                                             <asp:LinkButton ID="lbtnDelete" runat="server" OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("CTPDID")) %>'>
                                             <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                          </ItemTemplate>
                                       </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <asp:HiddenField ID="hdnCTPDID" Value="0" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
        
    <div class="modal" id="mpePartDetails" style="overflow-y: scroll;">
        <div style="max-width: 100%; padding-left: 0%;">
            <div class="modal-content">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close btn-primary-purple" onclick="hidePartDetailpopup();"
                                data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div id="Div3" class="docdiv">
                                <div class="inner-container">
                                    <div id="Div4" runat="server">
                                        <div id="div5" class="divInput" runat="server">
                                            <div class="ip-div text-center">
                                            </div>
                                        </div>
                                        <div id="div6" runat="server">
                                            <div class="col-sm-12 p-t-10" style="overflow-x: scroll;">
                                                <asp:GridView ID="gvMaterialPlanningDetails" runat="server" AutoGenerateColumns="False"
                                                    ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" 
                                                    CssClass="table table-hover table-bordered medium orderingfalse uniquedatatable"
                                                    DataKeyNames="MPID,BOMID">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="54px"
                                                            HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Part Name" ItemStyle-HorizontalAlign="left" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPartName" runat="server" Text='<%# Eval("PartName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Part Quantity" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPartQuantity" runat="server" Text='<%# Eval("PartQtyDesign")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Material Planned" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMaterialPlanned" runat="server" Text='<%# Eval("MPStatus")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Category Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("CategoryName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Grade Name Design" ItemStyle-HorizontalAlign="Left"
                                                            HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGradeNameDesign" runat="server" Text='<%# Eval("GradeNameDesign")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Thickness" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="105px"
                                                            HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblThicknessValue" runat="server" Text='<%# Eval("Thickness")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Unit Required Weight" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRequiredWeight" runat="server" Text='<%# Eval("RequiredWeight")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Required Weight" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTotalRequiredWeight" runat="server" Text='<%# Eval("TotalRequiredWeight")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MRN No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMRNNo" runat="server" Text='<%# Eval("MRNNo")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Grade Name Production" ItemStyle-HorizontalAlign="left"
                                                            HeaderStyle-Width="105px" HeaderStyle-HorizontalAlign="left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblGradeName" runat="server" Text='<%# Eval("GradeNameProduction")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Job Type" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblJobType" runat="server" Text='<%# Eval("JobType")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <div class="col-sm-12 text-center">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>

