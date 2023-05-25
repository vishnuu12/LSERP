<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="AddSmsAndEmailGroup.aspx.cs" Inherits="Pages_AddSmsAndEmailGroup" ClientIDMode="Predictable" %>

<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function OpenTab(Name) {
            var names = ["AG", "VSG"]; var text = document.getElementById('<%=AG.ClientID %>');
            for (var i = 0; i < names.length; i++) {
                if (Name == names[i]) {
                    var a = text.id.replace('AG', Name); document.getElementById(a).style.display = "block"; document.getElementById('li' + names[i]).className = "active";
                } else {
                    var b = text.id.replace('AG', names[i]);
                    document.getElementById(b).style.display = "none"; document.getElementById('li' + names[i]).className = "";
                }
            }
        }

        function Validate() {
            var msg = Mandatorycheck('ContentPlaceHolder1_AG');
            if (msg) {
                if ($('.table').find('[type="checkbox"]:checked').length >= 3) {
                    return true;
                }
                else {
                    ErrorMessage('Error', 'Select Minimum 3 Members in group');
                    hideLoader();
                    return false;
                }
            }
            else
                return false;
        }

        function deleteCommunicationGroupMaster(CGNID) {
            swal({
                title: "Are you sure?",
                text: "If Yes, the Group Name will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, Delete it!",
                closeOnConfirm: false
            }, function () {
                //showLoader();
                __doPostBack('DeleteCommunicationGroup', CGNID);
            });
            return false;
        }

        function ShowAddPopUp() {
            $('#mpeGroupMember').modal({
                backdrop: 'static'
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
                                    <h3 class="page-title-head d-inline-block">Email And SMS Group</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Master Setup</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Email And SMS Group</li>
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
                        <div id="divAdd" runat="server">
                        </div>
                        <div id="divInput" runat="server">
                            <div class="ip-div text-center">
                            </div>
                        </div>
                        <div id="divOutput" class="output_section" runat="server">
                            <div class="page-container" style="float: left; width: 100%">
                                <div class="main-card">
                                    <div class="card-body">
                                        <div id="Tabs" runat="server" class="tabpanel" style="background-color: Green; height: 45px;">
                                            <ul id="Ul1" class="nav nav-tabs grid-title-new no-boarder" runat="server">
                                                <li id="liAG" style="background-color: #026c63;" class="active"><a href="#AG" class="tab-content"
                                                    data-toggle="tab" onclick="OpenTab('AG');">Add Group</a></li>
                                                <li id="liVSG" style="background-color: #026c63;"><a href="#VSG" class="tab-content"
                                                    data-toggle="tab" onclick="OpenTab('VSG');">Email & SMS Group</a></li>
                                            </ul>
                                            <div class="tab-content">
                                                <div class="tab-pane active" id="AG" runat="server">
                                                    <div class="col-sm-12 p-t-10">
                                                        <div class="col-sm-4 right">
                                                            <label class="form-label mandatorylbl">
                                                                Group name</label>
                                                        </div>
                                                        <div class="col-sm-4 left">
                                                            <asp:TextBox ID="txtGroupName" runat="server" Text="" CssClass="form-control mandatoryfield"
                                                                onkeypress="validationAlphaNumeric(this)" autocomplete="off"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-4">
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-12 p-t-10">
                                                        <div class="col-sm-4 rigth">
                                                            <label class="form-label mandatorylbl">
                                                                Group Type
                                                            </label>
                                                        </div>
                                                        <div class="col-sm-4 left">
                                                            <asp:DropDownList ID="ddlGroupType" runat="server" CssClass="form-control mandatoryfield"
                                                                ToolTip="Select Group Type">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-4">
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-12 p-t-10">
                                                        <asp:GridView ID="gvEmployeeList" runat="server" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True"
                                                            EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium pagingfalse"
                                                            OnRowDataBound="gvEmployeeList_OnRowDataBound" HeaderStyle-HorizontalAlign="Center" 
                                                            DataKeyNames="EmployeeID">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="" ControlStyle-CssClass="nosort" ItemStyle-HorizontalAlign="Center">
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkall" runat="server" onclick="return checkAll(this);" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkitems" runat="server" AutoPostBack="false" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Employee Name" HeaderStyle-CssClass="text-left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEmployeeName" runat="server" Text='<%# Eval("EmployeeName")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Department" HeaderStyle-CssClass="text-left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDepartment" runat="server" Text='<%# Eval("DepartmentName")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Roll" HeaderStyle-CssClass="text-left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRole" runat="server" Text='<%# Eval("Role")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Email" HeaderStyle-CssClass="text-left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Mobile Number" HeaderStyle-CssClass="text-left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMobileNumber" runat="server" Text='<%# Eval("MobileNo")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                    <div class="col-sm-12">
                                                        <div class="col-sm-4">
                                                        </div>
                                                        <div class="col-sm-4">
                                                            <asp:LinkButton ID="btnSaveAndShare" CssClass="btn btn-success" runat="server" Text="Save"
                                                                OnClick="btnSaveAndShare_Click" OnClientClick="return Validate()"></asp:LinkButton>
                                                        </div>
                                                        <div class="col-sm-4">
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="tab-pane" id="VSG" runat="server">
                                                    <asp:UpdatePanel ID="up" runat="server" UpdateMode="Always">
                                                        <Triggers>
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <div class="col-sm-12 text-center">
                                                                <asp:RadioButtonList ID="rblGroupType" runat="server" CssClass="radio radio-success"
                                                                    AutoPostBack="true" OnSelectedIndexChanged="rblGroupType_OnSelectIndexChanged"
                                                                    RepeatDirection="Horizontal" RepeatLayout="Flow" TabIndex="5">
                                                                </asp:RadioButtonList>
                                                            </div>
                                                            <div class="margin-0 padding5 second col-sm-12">
                                                                <div class="col-sm-12">
                                                                    <%--OnRowDeleting="gvGroupDetails_RowDelete",OnRowDataBound="gvGroupDetails_RowDataBound",--%>
                                                                    <asp:GridView ID="gvGroupDetails" runat="server" AutoGenerateColumns="false" Width="80%"
                                                                        ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                                                        OnRowCommand="gvGroupDetails_RowCommand" DataKeyNames="CGNID">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="5%" HeaderStyle-CssClass="headCenter"
                                                                                ItemStyle-Width="5%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblSNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Group Name" HeaderStyle-Width="20%" HeaderStyle-CssClass="headCenter"
                                                                                ItemStyle-Width="20%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblGroupName" runat="server" Text='<%#Eval("GroupName") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <%--   <EditItemTemplate>
                                                                                    <asp:Label ID="lblEditGroupName" runat="server" Text='<%#Eval("GroupName") %>' Visible="false"></asp:Label>
                                                                                    <asp:TextBox ID="txtGroupName" runat="server" Text='<%#Eval("GroupName") %>' CssClass="textbox"
                                                                                        autocomplete="off" onkeypress="validationAlphaNumeric(this)"></asp:TextBox>
                                                                                </EditItemTemplate>--%>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Group Count" HeaderStyle-Width="15%" HeaderStyle-CssClass="text-center"
                                                                                ItemStyle-CssClass="text-center" ItemStyle-Width="15%">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblGroupCount" runat="server" Text='<%#Eval("GroupCount") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="View Group" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center"
                                                                                ItemStyle-Width="20%" HeaderStyle-Width="15%">
                                                                                <ItemTemplate>
                                                                                    <%--  <asp:ImageButton ID="imgbtnView" runat="server"  CommandArgument='<%#Eval("SGID")%>'
                                                                                        CommandName="View" OnClientClick="return ShowLoader();" />--%>
                                                                                    <asp:LinkButton ID="btnViewGroup" runat="server" CommandArgument='<%#((GridViewRow) Container).RowIndex %>'
                                                                                        CommandName="ViewGroupDetails"><img src="../Assets/images/view.png" /></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="120px" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Edit" HeaderStyle-CssClass="headCenter" ItemStyle-Width="10%"
                                                                                HeaderStyle-Width="15%">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton runat="server" ID="btnEdit"
                                                                                        CommandArgument='<%#((GridViewRow) Container).RowIndex %>' CommandName="EditGroupDetails"> <img src="../Assets/images/edit-ec.png" alt="" /></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Delete" HeaderStyle-CssClass="headCenter" ItemStyle-Width="10%"
                                                                                HeaderStyle-Width="15%">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lbtnDelete" runat="server" OnClientClick='<%# string.Format("return deleteCommunicationGroupMaster({0});",Eval("CGNID")) %>'>
                                                            <img src="../Assets/images/del-ec.png" alt=""/></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                    <asp:HiddenField ID="hdnCGNID" Value="0" runat="server" />
                                                                </div>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
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
    <div class="modal" id="mpeGroupMember" style="overflow-y: scroll;">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="upView" runat="server">
                    <Triggers>
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header">
                            <h4 class="modal-title ADD">
                                <asp:Label ID="lblGroupNameHeading_p" runat="server"></asp:Label></h4>
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                ×</button>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div class="inner-container">
                                <div id="divQPDetails" runat="server">
                                    <div class="col-sm-12 p-t-10">
                                        <asp:GridView ID="gvGroupMemberDetails_p" runat="server" AutoGenerateColumns="False"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No Records Found" CssClass="table table-hover table-bordered medium"
                                            HeaderStyle-HorizontalAlign="Center">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text=' <%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Employee Name" HeaderStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmployeeName" runat="server" Text='<%# Eval("EmployeeName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Department" HeaderStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDepartment" runat="server" Text='<%# Eval("DepartmentName")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Roll" HeaderStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRole" runat="server" Text='<%# Eval("Role")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email" HeaderStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mobile Number" HeaderStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMobileNumber" runat="server" Text='<%# Eval("MobileNo")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                </div>
                            </div>
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
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
