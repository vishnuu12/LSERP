<%--/*----------------------------------------------------------------------------------------------------                                                                                                                   
 Developer Name          : Vishnu S                                                                                                                
 Purpose                 : Forms and Reports for General Work Order Indent
 Created Date            : 17-10-2022
 Modified Date           :                                                                                                                  
 Modified Developer Name :                                                                                                                    
--------------------------------------------------------------------------------------------------------*/       --%>     
<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="GeneralWorkOrderIndent.aspx.cs" Inherits="Pages_GeneralWorkOrderIndent" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" type="text/css" href="../Assets/css/datatables2.css" />
    <script type="text/javascript" src="../Assets/scripts/datatables2.js"></script>
        <script type="text/javascript">

            function showDataTable() {
                $("#ContentPlaceHolder1_gvGeneralWorkOrder").DataTable();
            }
        </script>
        <style type="text/css">
        table#ContentPlaceHolder1_gvGeneralWorkOrder td {
            color: #000;
            font-weight: bold;
        }

        
        table#ContentPlaceHolder1_gvViewAllGWDetails td {
            color: #000;
            font-weight: bold;
        }
    </style>
     <script type="text/javascript">
       function deleteConfirm(GWOID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Row will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                __doPostBack('deletegvrow', GWOID);
            });
            return false;
         }

         function deleteConfirmfor(SGWOID) {
             swal({
                 title: "Are you sure?",
                 text: "If Yes, the Row will be deleted permanently",
                 type: "warning",
                 showCancelButton: true,
                 confirmButtonColor: "#DD6B55",
                 confirmButtonText: "Yes, Delete it!",
                 closeOnConfirm: false
             }, function () {
                 __doPostBack('deletegvrowfor', SGWOID);
             });
             return false;
         }
<%--         function showDataTable() {
             $('#<%=gvViewAllGWDetails.ClientID %>').DataTable();
          }--%>
        
         function ViewIndentAttach(index) {
             __doPostBack('ViewIndentAttach', index);
             return false;
         }

     </script>
   
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
                                    <h3 class="page-title-head d-inline-block">General Work Order Indent</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Production</a></li>
                                <li class="active breadcrumb-item" aria-current="page">General Work Order Indent</li>
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
                   <asp:PostBackTrigger ControlID="btnSave" />
                   <asp:PostBackTrigger ControlID="btneditsave" />
                </Triggers>
                <ContentTemplate>

                    <div class="card-container">
                        <div id="divAdd" runat="server">
                        </div>
                       <div id="divInput" runat="server">
                           <div class="ip-div text-center">
                                 <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4 text-right">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Service Description</label>
                                        </div>
                                        <div>
                                           <asp:TextBox ID="txtsdescription" runat="server" TabIndex="6" placeholder="Enter Description"
                                                ToolTip="Enter Description" TextMode="MultiLine" CssClass="form-control mandatoryfield"
                                                MaxLength="300" autocomplete="nope" />
                                        </div>
                                    </div>
                                      <div class="col-sm-4 text-right">
                                        <div class="text-left">
                                            <label class="form-label ">
                                               Remark</label>
                                        </div>
                                        <div>
                                           <asp:TextBox ID="txtsremark" runat="server" TabIndex="6" placeholder="Enter Remark"
                                                ToolTip="Enter Remark" TextMode="MultiLine" CssClass="form-control"
                                                MaxLength="300" autocomplete="nope" />
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
                                            <label class="form-label mandatorylbl">
                                                Quantity (Units)</label>
                                        </div>
                                        <div>
                                            <asp:TextBox runat="server" ID="txtquantity" AutoComplete="off" CssClass="form-control mandatoryfield"
                                                Width="70%" placeholder="Enter Quantity" />
                                        </div>
                                    </div>
                                    <div class="col-sm-4 text-left">


                                        <div>
                                            <label class="form-label mandatorylbl">
                                                UOM Name</label>
                                        </div>
                                        <div>
                                            <asp:DropDownList ID="ddlUOM" runat="server" ToolTip="Select UOM" AutoPostBack="true" 
                                                OnChange="showLoader();"  CssClass="form-control mandatoryfield">
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
                                                    <label class="form-label mandatorylbl">
                                                        Job Operation List
                                                    </label>
                                                </div>
                                                <div>
                                                     <asp:DropDownList ID="LiJobList" runat="server" ToolTip="Select Operation Name" CssClass="form-control mandatoryfield">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                                 
                                                </div>
                                            </div>
                                     <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Attach Details
                                            </label>
                                        </div>
                                        <div>
                                            <asp:FileUpload ID="documentUpload" runat="server" onchange="DocValidation(this);" TabIndex="12"
                                                CssClass="form-control mandatoryfield" Width="95%"></asp:FileUpload>

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
                                                    <label class="form-label mandatorylbl">
                                                        Indent To
                                                    </label>
                                                </div>
                                                <div>
                                                     <asp:DropDownList ID="ddlIndentTo" runat="server" ToolTip="Select Employee" CssClass="form-control mandatoryfield">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                                 
                                                </div>
                                            </div>
                                </div>

                                <div class="col-sm-12 text-center p-t-10">
                                    <asp:LinkButton ID="btnSave" Text="Save" CssClass="btn btn-cons btn-save AlignTop"
                                        OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divInput');" OnClick="btnSave_Click"
                                        runat="server" />
                                    <asp:LinkButton ID="btnCancel" Text="Cancel" CssClass="btn btn-cons btn-danger AlignTop" OnClick="btnCancel_Click"
                                        runat="server" />
                                </div>
                            
                                
                            </div>
                        </div>

                        <div id="divOutput" class="output_section" runat="server">
                           <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div class="col-sm-12 row">
                                            <div class="col-md-6">
                                                <p class="h-style">
                                                    <asp:Label ID="lbltitle" runat="server" Text="General Details"></asp:Label>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            
                                             <asp:GridView ID="gvGeneralWorkOrder" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium tablestatesave"
                                                OnRowCommand="gvGeneralWorkOrder_RowCommand"
                                                OnRowDataBound="gvGeneralWorkOrder_RowDataCommand" DataKeyNames="GWOID,FileName">
                                                <Columns>
                                                 <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="GWI No" SortExpression="General Work Order Indent Number" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                                    <asp:Label ID="lblGWI" Style="white-space:nowrap;" Text=' <%# Eval("GWI")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Service Description" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="IndentTo" runat="server" Value='<%# Bind("IndentTo") %>' />
                                                            <asp:Label ID="lblServiceDescription" runat="server" Text='<%# Eval("ServiceDescription")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Quantity" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQuantity" runat="server" Text='<%# Eval("QuantityUnit")%>'></asp:Label>
                                                             <asp:Label ID="Name" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Job List" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="JobListId" runat="server" Value='<%# Bind("JobListId") %>' />
                                                            <asp:Label ID="lblJobList" runat="server" Text='<%# Eval("JobList")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Remarks" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View Attach">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnViewIndentAttach" runat="server"
                                                                Text="View Attach"
                                                                OnClientClick='<%# string.Format("return ViewIndentAttach({0});",((GridViewRow) Container).RowIndex) %>'> <img src="../Assets/images/view.png" alt=""/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="Add" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnAdd" runat="server" CommandName="ViewGWIndent" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                           <img src="../Assets/images/add1.png" alt="" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDelete" runat="server" OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("GWOID")) %>'>
                                                            <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                </Columns>
                                            </asp:GridView>

                                           
                                       <iframe id="ifrm" visible="false" runat="server"></iframe>
                                        </div>
                                         </div>
                                </div>
                            </div>
                            </div>
                        <div id="divInput1" runat="server" visible="false">
                           <div class="ip-div text-center">
                               <div class="h-style">
                                                <asp:Label ID="lblGWOI" runat="server" Style="font-weight: 700;color: #d92550;"></asp:Label>
                                   <asp:Label ID="GWIId" runat="server" Style="font-weight: 700" Visible="false"></asp:Label>
                                   <asp:Label ID="GWONO" runat="server" Style="font-weight: 700" Visible="false"></asp:Label>
                                            </div>
                             
                                 <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4 text-right">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                               Service Description</label>
                                        </div>
                                        <div>
                                           <asp:TextBox ID="txtsdescription1" runat="server" TabIndex="6" placeholder="Enter Description"
                                                ToolTip="Enter Description" TextMode="MultiLine" CssClass="form-control"
                                                MaxLength="300" autocomplete="nope" />
                                        </div>
                                    </div>
                                      <div class="col-sm-4 text-right">
                                        <div class="text-left">
                                            <label class="form-label ">
                                               Remark</label>
                                        </div>
                                        <div>
                                           <asp:TextBox ID="txtsremark1" runat="server" TabIndex="6" placeholder="Enter Remark"
                                                ToolTip="Enter Remark" TextMode="MultiLine" CssClass="form-control"
                                                MaxLength="300" autocomplete="nope" />
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
                                            <label class="form-label mandatorylbl">
                                                Quantity (Units)</label>
                                        </div>
                                        <div>
                                             
                                            <asp:TextBox runat="server" ID="txtquantity1"  AutoComplete="off" CssClass="form-control mandatoryfield"
                                                Width="70%" placeholder="Enter Quantity" />
                                        </div>
                                    </div>
                                    <div class="col-sm-4 text-left">


                                        <div>
                                            <label class="form-label mandatorylbl">
                                                UOM Name</label>
                                        </div>
                                        <div>
                                            <asp:DropDownList ID="ddlUOM1" runat="server" ToolTip="Select UOM" AutoPostBack="true" 
                                                OnChange="showLoader();"  CssClass="form-control mandatoryfield">
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
                                                    <label class="form-label mandatorylbl">
                                                        Job Operation List
                                                    </label>
                                                </div>
                                                <div>
                                                     <asp:DropDownList ID="LiJobList1" runat="server" ToolTip="Select Operation Name" CssClass="form-control mandatoryfield">
                                                <asp:ListItem Selected="True" Value="0" Text="-- Select --"></asp:ListItem>
                                            </asp:DropDownList>
                                                 
                                                </div>
                                            </div>
                                     <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Attach Details
                                            </label>
                                        </div>
                                        <div>
                                            <asp:FileUpload ID="FileUpload1" runat="server" onchange="DocValidation(this);" TabIndex="12"
                                                CssClass="form-control mandatoryfield" Width="95%"></asp:FileUpload>

                                        </div>
                                    </div>
                                     <div class="col-sm-2">
                                    </div>
                                </div>

                                <div class="col-sm-12 text-center p-t-10">
                                    
                                    <asp:LinkButton ID="btneditsave" Text="Save" CssClass="btn btn-cons btn-save AlignTop"
                                        OnClientClick="return Mandatorycheck('ContentPlaceHolder1_divInput1');" OnClick="btnEditSave_Click"
                                        runat="server" />
                                    <asp:LinkButton ID="btneditcancel" Text="Cancel" CssClass="btn btn-cons btn-danger AlignTop" OnClick="btnEditCancel_Click"
                                        runat="server" />
                                </div>
                            
                                
                            </div>
                        </div>
                        <div id="divOutput1" class="output_section" runat="server">
                           <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                            <asp:GridView ID="gvViewAllGWDetails" runat="server" AutoGenerateColumns="False"
                                                ShowHeaderWhenEmpty="True" AllowPaging="True" PageSize="10" EmptyDataText="No Records Found"
                                                CssClass="table table-hover table-bordered  medium" OnRowCommand="gvViewAllGWDetails_RowCommand" OnRowDataBound="gvViewAllGWDetails_RowDataCommand" DataKeyNames="SGWOID" >
                                                <Columns>
                                                       <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Indent Status" SortExpression="Indent Status" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                                    <asp:Label ID="lblApprovedStatus" Text=' <%# Eval("ApprovedStatus")%>' runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="GWI No" SortExpression="General Work Order Indent Number" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                           
                                                                    <asp:Label ID="lblGWI" Style="white-space:nowrap;" Text=' <%# Eval("GWI")%>' runat="server"></asp:Label> 

                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Service Description" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblServiceDescription1" runat="server" Text='<%# Eval("ServiceDescription")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Quantity" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQuantity1" runat="server" Text='<%# Eval("QuantityUnit")%>' ></asp:Label>
                                                             <asp:Label ID="Name" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Job List" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="JobListId1" runat="server" Value='<%# Bind("JobListId") %>' />
                                                            <asp:Label ID="lblJobList" runat="server" Text='<%# Eval("JobList")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Remarks" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Edit" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEditfor" runat="server" CommandName="EditGeneralWorkOrderfor" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                           <img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnDeletefor" runat="server" OnClientClick='<%# string.Format("return deleteConfirmfor({0});",Eval("SGWOID")) %>'>
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
                   <asp:HiddenField ID="hdnGWOID" Value="0" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

