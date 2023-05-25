<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true" CodeFile="CallibrationMaster.aspx.cs" Inherits="Pages_CallibrationMaster" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" type="text/css" href="../Assets/css/datatables2.css" />
    <script type="text/javascript" src="../Assets/scripts/datatables2.js"></script>
     <script type="text/javascript">
         function deleteConfirm(CID) {
            
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
                 __doPostBack('deletegvrow', CID);
             });
             return false;
         }
         function showDataTable() {
             $('#<%=gvCallibration.ClientID %>').DataTable();
         }

         function ViewIndentAttach(index) {
             __doPostBack('ViewIndentAttach', index);
             return false;
         }

         $(document).ready(function () {
             $('#<%=txtCalibrationdon.ClientID %>').datepicker({
                format: 'dd/mm/yyyy'
            });
                     $('#<%=txtCalibrationdue.ClientID %>').datepicker({
                         format: 'dd/mm/yyyy'
                     });
                     Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
                     function EndRequestHandler(sender, args) {
                         $('.datepicker').datepicker({ format: 'dd/mm/yyyy' });
                     }
                 });

         function opennewtab(Filepath) {
             var FileName = Filepath.split('$');
             for (var i = 0; i < FileName.length; i++)
                 window.open('http://183.82.33.21/LSERPDocs/QualityReportDocs/' + FileName[i], '_blank');
         }
     </script>
   <style type="text/css">
        .modal-content {
            width: 137% !important;
        }

        table#ContentPlaceHolder1_gvCallibration td {
            color: #000;
            font-weight: bold;
            font-size: smaller;
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
                                        <%--<i class="fa fa-th-large opacity-6 yellow"></i>--%>
                                        <img src="../Assets/images/pages.png" alt="" width="16px" />
                                    </span>
                                    <h3 class="page-title-head d-inline-block">Callibration Master</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                            <ol class="breadcrumb">
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                <li class="breadcrumb-item"><a href="javascript:void(0);">Master</a></li>
                                <li class="active breadcrumb-item" aria-current="page">Callibration Master</li>
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
                    <asp:PostBackTrigger ControlID="imgExceldownload" />
                </Triggers>
                <ContentTemplate>
                    <div class="card-container">
                        <div id="divAdd" runat="server">
                            <div class="text-center">
                                <asp:LinkButton ID="btnAddNew" runat="server" Text="Add New" OnClick="btnAddNew_Click"
                                    CssClass="btn btn-success add-emp"></asp:LinkButton>
                            </div>
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                                 <div class="col-sm-12 p-t-10">
                                       <div class="col-sm-2">
                                    </div>
                                    <div class="col-sm-4 text-right">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Code No</label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtCodeno" runat="server" Width="70%" CssClass="form-control mandatoryfield"
                                                ToolTip="Enter Code No" AutoComplete="off" placeholder="Enter Code No">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-4">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Range</label>
                                        </div>
                                        <div>
                                            <asp:TextBox runat="server" ID="txtRange" AutoComplete="off" CssClass="form-control mandatoryfield"
                                                Width="70%" placeholder="Enter Range" />
                                        </div>
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                  </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                      <div class="col-sm-4 text-right">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                               Certificate No
                                            </label>
                                        </div>
                                        <div>
                                            <asp:TextBox ID="txtcertificateno" runat="server" Width="70%" CssClass="form-control mandatoryfield"
                                                ToolTip="Enter Certificate No" AutoComplete="off" placeholder="Enter Certificate No">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="col-sm-4 text-right">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Calibration Done
                                            </label>
                                        </div>
                                        <div>
                                           <asp:TextBox ID="txtCalibrationdon" runat="server" Width="70%" CssClass="form-control datepicker mandatoryfield"
                                                AutoComplete="off" ToolTip="Enter Delivery Date" placeholder="Enter Delivery Date">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                    
                                   
                                    <div class="col-sm-2">
                                    </div>
                                </div>
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-2">
                                    </div>
                                   <div class="col-sm-4 text-right">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Calibration Due</label>
                                        </div>
                                        <div>
                                           <asp:TextBox ID="txtCalibrationdue" runat="server" Width="70%" CssClass="form-control datepicker mandatoryfield"
                                                AutoComplete="off" ToolTip="Enter Calibration Due" placeholder="Enter Calibration Due">
                                            </asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="col-sm-4 text-right">
                                        <div class="text-left">
                                            <label class="form-label mandatorylbl">
                                                Attach Files
                                            </label>
                                        </div>
                                        <div>
                                            <asp:FileUpload ID="documentUpload" runat="server" onchange="DocValidation(this);" TabIndex="12"
                                                CssClass="form-control mandatoryfield" Width="95%"></asp:FileUpload>
                                           <%-- <asp:FileUpload ID="FileUploadControl"  name="FileUploadControl"  runat="server" onchange="DocValidation(this);" TabIndex="12"
                                                CssClass="form-control mandatoryfield" Width="95%"></asp:FileUpload>--%>

                                        </div>
                                    </div>
                                    <div class="col-sm-2">
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
                                                    <asp:Label ID="lbltitle" runat="server" Text="Callibration Master Details"></asp:Label>
                                                </p>
                                            </div>
                                             <div class="col-md-6 ex-icons" id="divDownload" runat="server">
                                                <asp:LinkButton ID="imgExceldownload" runat="server" CssClass="excel_bg" ToolTip="Excel Download"
                                                    OnClick="btnExcel_Click" />
                                            </div>
                                        </div>
                                         <div class="col-sm-12 p-t-10">
                                            
                                             <asp:GridView ID="gvCallibration" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                HeaderStyle-HorizontalAlign="Center" OnRowCommand="gvCallibration_RowCommand"
                                                 OnRowDataBound="gvCallibration_RowDataCommand" DataKeyNames="CID,FileName">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Code No" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblcode" runat="server" Text='<%# Eval("CodeNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Range" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRange" runat="server" Text='<%# Eval("Range")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Certificate No" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCertificateNo" runat="server" Text='<%# Eval("CerfificateNo")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Callibration Done" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCallibrationDone" runat="server" Text='<%# Eval("CalibrationDon", "{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Callibration Due" ItemStyle-CssClass="text-left" HeaderStyle-CssClass="text-left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCallibrationDue" runat="server" Text='<%# Eval("CalibrationDue", "{0:dd-MM-yyyy}")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="View Attach">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnViewIndentAttach" runat="server"
                                                                Text="View Attach"
                                                                OnClientClick='<%# string.Format("return ViewIndentAttach({0});",((GridViewRow) Container).RowIndex) %>'> <img src="../Assets/images/view.png" alt=""/></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-CssClass="text-center">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="EditCallibrationDetails" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'>
                                                           <img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnDelete" runat="server" OnClientClick='<%# string.Format("return deleteConfirm({0});",Eval("CID")) %>'>
                                                            <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                    </ItemTemplate>
                                                
                                                    </asp:TemplateField>
                                                </Columns>
                                             </asp:GridView>

                                           
                                       </div>
                                    </div>
                                </div>
                          </div>
                                       <iframe id="ifrm" visible="false" runat="server"></iframe>
                    </div>
                        <asp:HiddenField ID="CIDHDN" Value="0" runat="server" />
                </ContentTemplate>
          </asp:UpdatePanel>
        </div>
    </div>
     
</asp:Content>
