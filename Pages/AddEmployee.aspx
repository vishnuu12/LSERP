<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="AddEmployee.aspx.cs" Inherits="Pages_AddEmployee" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ MasterType VirtualPath="~/Master/CommonMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {
            $('#<%=txtDoj.ClientID %>').datepicker({
                format: 'dd/mm/yyyy'
            });
            var date = new Date();
            date.setFullYear(date.getFullYear() - 15);
            $('#<%=txtDob.ClientID %>').datepicker({
                format: 'dd/mm/yyyy',
                endDate: new Date(date)
            });
            $('#<%=txtValidityTill.ClientID %>').datepicker({
                format: 'dd/mm/yyyy'
                //    startDate: new Date()
            });
            $('.datepicker').datepicker({ format: 'dd/mm/yyyy' });
        });
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            function EndRequestHandler(sender, args) {
                $('.datepicker').datepicker({ format: 'dd/mm/yyyy' });
            }
        });
        function btnAdd() {
            $('#divAddAcadRec').modal({
                backdrop: 'static'
            });
        }

        function btnEdit() {
            $('#divEditAcadRec').modal({
                backdrop: 'static'
            });
        }

        function showAdd() {
            $('#divView').modal({
                backdrop: 'static'
            });
        }

        function showAddDocs() {
            $('#divView').modal({
                backdrop: 'static'
            });
            OpenTab('Documents');
            if (document.getElementById("<%=hdnDocsOpen.ClientID %>").value == "1")
                showEDocs();
            else {
                document.getElementById("<%=divSDocuments.ClientID %>").style.display = "none";
                document.getElementById("<%=divEDocuments.ClientID %>").style.display = "block";
            }
        }

        function btnEdAdd() {
            var msg = "0";
            if (document.getElementById("<%=txtCertification.ClientID %>").value == "") {
                $('#<%=txtCertification.ClientID %>').notify('Enter Certifications', { arrowShow: true, position: 't,r', autoHide: true });
                msg = "1";
            }
            if (document.getElementById("<%=txtSubjects.ClientID %>").value == "") {
                $('#<%=txtSubjects.ClientID %>').notify('Enter Subjects', { arrowShow: true, position: 't,r', autoHide: true });
                msg = "1";
            }
            if (document.getElementById("<%=txtMarks.ClientID %>").value == "") {
                $('#<%=txtMarks.ClientID %>').notify('Enter Marks/Grade', { arrowShow: true, position: 't,r', autoHide: true });
                msg = "1";
            }
            if ((document.getElementById("<%=txtPercentage.ClientID %>").value == "") || (document.getElementById("<%=txtPercentage.ClientID %>").value == "0")) {
                $('#<%=txtPercentage.ClientID %>').notify('Enter Percentage', { arrowShow: true, position: 't,r', autoHide: true });
                msg = "1";
            }
            if (document.getElementById("<%=txtBoard.ClientID %>").value == "") {
                $('#<%=txtBoard.ClientID %>').notify('Enter Board/University', { arrowShow: true, position: 't,r', autoHide: true });
                msg = "1";
            }
            if (document.getElementById("<%=ddlYOC.ClientID %>").value == "0") {
                $('#<%=ddlYOC.ClientID %>').notify('Enter Percentage', { arrowShow: true, position: 't,r', autoHide: true });
                msg = "1";
            }

            var validFilesTypes = ["doc", "pdf", "docx", "jpg", "jpeg"];
            var file = document.getElementById("<%=fAttachment.ClientID%>");
            var path = file.value;
            var ext = path.substring(path.lastIndexOf(".") + 1, path.length).toLowerCase();
            var isValidFile = false;
            for (var i = 0; i < validFilesTypes.length; i++) {
                if (ext == validFilesTypes[i]) {
                    isValidFile = true;
                    break;
                }
            }
            if (path != "") {
                if (!isValidFile) {
                    $('#<%=fAttachment.ClientID %>').notify('Invalid File.\n Please upload a File with extension: .doc , .docx , .pdf , .jpg , .jpeg', { arrowShow: true, position: 't,r', autoHide: true });
                    return false;
                }
            }
            if (msg == "0") {
                //ShowLoader();
                return true;
            }
            else
                return false;
        }


        function btnEdEdit() {
            var msg = "0";
            //            if (document.getElementById("<%=txtECertification.ClientID %>").value == "") {
            //                $('#<%=txtECertification.ClientID %>').notify('Enter Certifications', { arrowShow: true, position: 't,r', autoHide: true });
            //                msg = "1";
            //            }
            if (document.getElementById("<%=txtESubjects.ClientID %>").value == "") {
                $('#<%=txtESubjects.ClientID %>').notify('Enter Subjects', { arrowShow: true, position: 't,r', autoHide: true });
                msg = "1";
            }
            if (document.getElementById("<%=txtEMarks.ClientID %>").value == "") {
                $('#<%=txtEMarks.ClientID %>').notify('Enter Marks/Grade', { arrowShow: true, position: 't,r', autoHide: true });
                msg = "1";
            }
            if ((document.getElementById("<%=txtEPercentage.ClientID %>").value == "") || (document.getElementById("<%=txtPercentage.ClientID %>").value == "0")) {
                $('#<%=txtEPercentage.ClientID %>').notify('Enter Percentage', { arrowShow: true, position: 't,r', autoHide: true });
                msg = "1";
            }
            if (document.getElementById("<%=txtEBoard.ClientID %>").value == "") {
                $('#<%=txtEBoard.ClientID %>').notify('Enter Board/University', { arrowShow: true, position: 't,r', autoHide: true });
                msg = "1";
            }
            if (document.getElementById("<%=ddlEYOC.ClientID %>").value == "0") {
                $('#<%=ddlEYOC.ClientID %>').notify('Enter Percentage', { arrowShow: true, position: 't,r', autoHide: true });
                msg = "1";
            }

            var validFilesTypes = ["doc", "pdf", "docx", "jpg", "jpeg"];
            var file = document.getElementById("<%=fEAttachment.ClientID%>");
            var path = file.value;
            var ext = path.substring(path.lastIndexOf(".") + 1, path.length).toLowerCase();
            var isValidFile = false;
            for (var i = 0; i < validFilesTypes.length; i++) {
                if (ext == validFilesTypes[i]) {
                    isValidFile = true;
                    break;
                }
            }
            if (path != "") {
                if (!isValidFile) {
                    $('#<%=fEAttachment.ClientID %>').notify('Invalid File.\n Please upload a File with extension:  .doc , .docx , .pdf , .jpg , .jpeg', { arrowShow: true, position: 't,r', autoHide: true });
                    return false;
                }
            }
            if (msg == "0") {
                // ShowLoader();
                return true;
            }
            else
                return false;
        }

        function AddEmployee() {
            document.getElementById("<%=btnAddEmployee.ClientID %>").style.display = "none";
            document.getElementById("<%=txtEmployeeID.ClientID %>").style.display = "none";
            document.getElementById("<%=divBasicInfo.ClientID %>").style.display = "block";
            document.getElementById("<%=btnPCancel.ClientID %>").style.display = "inline-block";
            document.getElementById("<%=hdnNew.ClientID %>").value = "1";

            document.getElementById("<%=fEmpPhoto.ClientID %>").style.display = "block";
            document.getElementById("<%=divSPersonal.ClientID %>").style.display = "block";

            return false;
        }

        function btnPersonal() {
            var msg = "0";
            if (document.getElementById("<%=txtFirstname.ClientID %>").value == "") {
                $('#<%=txtFirstname.ClientID %>').notify('Field Required.', { arrowShow: true, position: 't,r', autoHide: true });
                msg = "1";
            }
            if (document.getElementById("<%=txtLastname.ClientID %>").value == "") {
                $('#<%=txtLastname.ClientID %>').notify('Field Required.', { arrowShow: true, position: 't,r', autoHide: true });
                msg = "1";
            }
            if (document.getElementById("<%=txtInitials.ClientID %>").value == "") {
                $('#<%=txtInitials.ClientID %>').notify('Field Required.', { arrowShow: true, position: 't,r', autoHide: true });
                msg = "1";
            }
            if (document.getElementById("<%=txtDoj.ClientID %>").value == "") {
                $('#<%=txtDoj.ClientID %>').notify('Field Required.', { arrowShow: true, position: 't,r', autoHide: true });
                msg = "1";
            }
            if (document.getElementById("<%=ddlEmpType.ClientID %>").value == "0") {
                $('#<%=ddlEmpType.ClientID %>').notify('Field Required.', { arrowShow: true, position: 't,r', autoHide: true });
                msg = "1";
            }
            else {
                var date = document.getElementById("<%=txtDob.ClientID %>").value.split('/');
                var CurrentDate = new Date();
                var year = CurrentDate.getFullYear();
                year = +year - +15;
                if (!(date[2] <= year)) {
                    $('#<%=txtDob.ClientID %>').notify('Minimun age is 15', { arrowShow: true, position: 't,r', autoHide: true });
                    msg = "1";
                }
                //ShowLoader();
            }

            if (document.getElementById("<%=txtDob.ClientID %>").value == "") {
                $('#<%=txtDob.ClientID %>').notify('Field Required.', { arrowShow: true, position: 't,r', autoHide: true });
                msg = "1";
            }

            if (document.getElementById("<%=ddlDepartment.ClientID %>").selectedIndex == 0) {
                $('#<%=ddlDepartment.ClientID %>').notify('Field Required.', { arrowShow: true, position: 't,r', autoHide: true });
                msg = "1";
            }

            if (document.getElementById("<%=ddlDesignation.ClientID %>").selectedIndex == 0) {
                $('#<%=ddlDesignation.ClientID %>').notify('Field Required.', { arrowShow: true, position: 't,r', autoHide: true });
                msg = "1";
            }
            if (document.getElementById("<%=ddlRoles.ClientID %>").selectedIndex == 0) {
                $('#<%=ddlRoles.ClientID %>').notify('Field Required.', { arrowShow: true, position: 't,r', autoHide: true });
                msg = "1";
            }
            if (document.getElementById("<%=ddlERPRoles.ClientID %>").selectedIndex == 0) {
                $('#<%=ddlERPRoles.ClientID %>').notify('Field Required.', { arrowShow: true, position: 't,r', autoHide: true });
                msg = "1";
            }

            var imagePath = document.getElementById("<%=imgPhoto.ClientID %>").src
            var imageName = imagePath.match(/[\w-]+\.(jpg|png|jpeg)/g);
            var validFilesTypes = ["jpg", "jpeg"];
            var file = document.getElementById("<%=fEmpPhoto.ClientID%>");
            var path = file.value;
            var ext = path.substring(path.lastIndexOf(".") + 1, path.length).toLowerCase();
            var isValidFile = false;
            for (var i = 0; i < validFilesTypes.length; i++) {
                if (ext == validFilesTypes[i]) {
                    isValidFile = true;
                    break;
                }
            }
            if (path != "") {
                if (!isValidFile) {
                    $('#<%=fEmpPhoto.ClientID %>').notify('Invalid File.\n Please upload a File with extension: .jpg , .jpeg', { arrowShow: true, position: 't,r', autoHide: true });
                    return false;
                }
            }

            if (msg == "0") {
                return true;
            }
            else
                return false;
        }

        function btnCommunication() {
            var msg = "0";
            if (document.getElementById("<%=txtEmail.ClientID %>").value != "") {
                if (!(isValidEmail(document.getElementById("<%=txtEmail.ClientID %>").value))) {
                    $('#<%=txtEmail.ClientID %>').notify('Enter Valid Email.', { arrowShow: true, position: 't,r', autoHide: true });
                    document.getElementById("<%=txtEmail.ClientID %>").focus();
                    msg = "1";
                }
            }
            if (!(ValidPhone(document.getElementById("<%=txtMobileno.ClientID %>")))) {
                $('#<%=txtMobileno.ClientID %>').notify('Enter 10 Numbers.', { arrowShow: true, position: 't,r', autoHide: true });
                document.getElementById("<%=txtMobileno.ClientID %>").focus();
                msg = "1";
            }
            if (!(ValidPhone(document.getElementById("<%=txtPhoneno.ClientID %>")))) {
                $('#<%=txtPhoneno.ClientID %>').notify('Enter 10 Numbers.', { arrowShow: true, position: 't,r', autoHide: true });
                document.getElementById("<%=txtPhoneno.ClientID %>").focus();
                msg = "1";
            }
            if (msg == "0") {
                // ShowLoader();
                return true;
            }
            else {
                return false;
            }
        }

        function ViewDoc(LableName) {
            var name = LableName.id.replace("btn", "lbl");
            document.getElementById("<%=hdnFileName.ClientID %>").value = document.getElementById(name).innerText;
            document.getElementById('<%=btnView1.ClientID %>').click();
        }

        function ShowEPersonal() {
            document.getElementById("<%=divSPersonal.ClientID %>").style.display = "block";
            document.getElementById("<%=divEPersonalDetails.ClientID %>").style.display = "none";
            document.getElementById("<%=fEmpPhoto.ClientID %>").style.display = "block";
            return false;
        }

        function ShowEComm() {
            document.getElementById("<%=divSComm.ClientID %>").style.display = "block";
            document.getElementById("<%=divEComm.ClientID %>").style.display = "none";
            return false;
        }

        function ShowERoles() {
            document.getElementById("<%=divSRoles.ClientID %>").style.display = "block";
            document.getElementById("<%=divERoles.ClientID %>").style.display = "none";
            return false;
        }

        function ShowEBankDetails() {
            document.getElementById("<%=divSBankDetails.ClientID %>").style.display = "block";
            document.getElementById("<%=divEBankDetails.ClientID %>").style.display = "none";
            return false;
        }

        function ShowEExperience() {
            document.getElementById("<%=divSExperience.ClientID %>").style.display = "block";
            document.getElementById("<%=divEExperience.ClientID %>").style.display = "none";
            return false;
        }

        function showEDocs() {
            document.getElementById("<%=hdnDocsOpen.ClientID %>").value = "1";
            document.getElementById("<%=divSDocuments.ClientID %>").style.display = "block";
            document.getElementById("<%=divEDocuments.ClientID %>").style.display = "none";
            return false;
        }

        function Cancel() {
            document.getElementById("<%=txtEmployeeID.ClientID %>").value = "";
        }

        function ValidateDoc(input) {
            var validFilesTypes = ["doc", "pdf", "docx", "jpg", "jpeg"];
            var file = document.getElementById(input.id);
            var path = file.value;
            var size = 2048000;
            var ext = path.substring(path.lastIndexOf(".") + 1, path.length).toLowerCase();
            var isValidFile = false;
            for (var i = 0; i < validFilesTypes.length; i++) {
                if (ext == validFilesTypes[i]) {
                    isValidFile = true;
                    break;
                }
            }
            var idName = '#' + input.id;
            if (!isValidFile) {
                $(idName).notify('Invalid File.\n Please upload a File with extension: .doc , .docx , .pdf , .jpg , .jpeg', { arrowShow: true, position: 't,r', autoHide: true });
                return false;
            }
        }

        function btnRoles() {
            var msg = "0";
            var TotYear = document.getElementById("<%=ddlTotExperienceYear.ClientID %>").value;
            var TotMonth = document.getElementById("<%=ddlTotExperienceMonth.ClientID %>").value;
            var RelYear = document.getElementById("<%=ddlRelExperienceYear.ClientID %>").value;
            var RelMonth = document.getElementById("<%=ddlRelExperienceMonth.ClientID %>").value;

            TotYear = parseInt(TotYear) * 12;
            var Total = parseInt(TotYear) + parseInt(TotMonth);

            RelYear = parseInt(RelYear) * 12;
            var Relavent = parseInt(RelYear) + parseInt(RelMonth);

            if (Total < Relavent) {
                $('#<%=ddlRelExperienceYear.ClientID %>').notify('Relavent year lesser than Total year experience.', { arrowShow: true, position: 't,r', autoHide: true });
                msg = "1";
            }
            if (document.getElementById("<%=txtValidityTill.ClientID %>").value != "") {
                var ValidityTill = document.getElementById("<%=txtValidityTill.ClientID %>").value.split('/');
                ValidityTill = new Date(+ValidityTill[2], parseInt(ValidityTill[1], 10) - 1, parseInt(ValidityTill[0], 10));
                var CurrentDate = new Date();
                if (ValidityTill <= CurrentDate) {
                    $('#<%=txtValidityTill.ClientID %>').notify('Validity Till greater than current date', { arrowShow: true, position: 't,r', autoHide: true }); msg = "1";
                    msg = "1";
                }
            }
            if (msg == "0") {
                return true;
            }
            else
                return false;
        }

        function messageRoles(title, message) {
            ErrorMessage(title, message);
            OpenTab('Roles');
        }

        function messageDocs(title, message) {
            ErrorMessage(title, message);
            OpenTab('Documents');
        }

        function messageExperience(title, message) {
            ErrorMessage(title, message);
            OpenTab('Experience');
        }

        function messageEducation(title, message) {
            ErrorMessage(title, message);
            OpenTab('EducationProfile');
        }

        function ShowProcessImage() {
            var autocomplete = document.getElementById('<%= txtEmployeeID.ClientID %>');
            autocomplete.style.backgroundImage = 'url(../images/loading1.gif)';
            autocomplete.style.backgroundRepeat = 'no-repeat';
            autocomplete.style.backgroundPosition = 'right';
        }

        function HideProcessImage() {
            var autocomplete = document.getElementById('<%= txtEmployeeID.ClientID %>');
            autocomplete.style.backgroundImage = 'none';
        }

        function selectedvalue(sender, e) {
            var RegNo = $get('<%= txtEmployeeID.ClientID %>');
            var id = e.get_value();
            if (id.value == "Record not available") {
                RegNo.value = "";
            }
            else {
                RegNo.value = id;
                document.getElementById("<%= btnGet.ClientID %>").click();
            }
        }

        function ShowImagePreview(input) {
            var validFilesTypes = ["jpg", "jpeg"];
            var file = document.getElementById("<%=fEmpPhoto.ClientID%>");
            var path = file.value;
            var size = 2048000;
            var ext = path.substring(path.lastIndexOf(".") + 1, path.length).toLowerCase();
            var isValidFile = false;
            for (var i = 0; i < validFilesTypes.length; i++) {
                if (ext == validFilesTypes[i]) {
                    isValidFile = true;
                    break;
                }
            }
            if (!isValidFile) {
                $('#<%=fEmpPhoto.ClientID %>').notify('Invalid File.\n Please upload a File with extension: .jpg , .jpeg', { arrowShow: true, position: 't,r', autoHide: true });
                return false;
            }
            else if (input.files && input.files[0]) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#<%=imgPhoto.ClientID%>').prop('src', e.target.result)
                                   .width(180)
                                   .height(200);
                };
                reader.readAsDataURL(input.files[0]);
            }
        }

        function OpenTab(Name) {
            var names = ["Communication", "Roles", "BankDetails", "EducationProfile", "Experience", "Documents"];
            var text = document.getElementById('<%=Communication.ClientID %>');

            for (var i = 0; i < names.length; i++) {
                if (Name == names[i]) {
                    var a = text.id.replace('Communication', Name);
                    document.getElementById(a).style.display = "block";
                    var index = names.indexOf(names[i]);
                    document.getElementById('<%=hdnTab.ClientID %>').value = index;
                    document.getElementById('li' + names[i]).className = "active";
                    // document.getElementById('lbl' + names[i] + 'Icon').Style.display=("",""); 
                }
                else {
                    var b = text.id.replace('Communication', names[i]);
                    document.getElementById(b).style.display = "none";
                    document.getElementById('li' + names[i]).className = "";
                    //document.getElementById('li' + names[i]).className = "fas fa-check-circle";
                }
            }
        }

        function GvExperienceClear() {
            $('#<%=gvExperience.ClientID %> input[type=text]').val("");
        }
        
    </script>
    <style type="text/css">
        /*--internal css    */
        .grid-title-new
        {
            color: #FFFFFF !important;
            background-color: #098E83;
            margin-bottom: 0px;
            border-bottom: 0px;
        }
        
        /*-- */
        .StyleIcon
        {
            height: 16px;
            width: 16px;
            color: Gray;
            border-radius: 25% 10% 40% 10%;
            box-shadow: none;
            display: inline-block;
            background: none;
        }
        .StyleIcon.Red
        {
            height: 16px;
            width: 16px;
            color: Red;
            border-radius: 25% 10% 40% 10%;
            box-shadow: none;
            display: inline-block;
        }
        .fa-check-circle
        {
            color: #fff;
        }
        .fa-times
        {
            color: #fff;
        }
        .nav-tabs > li.active > a > span .fa-check-circle, .nav-tabs > li.active > a > span .fa-times
        {
            color: #448e84;
        }
        .nav-tabs > li > a
        {
            width: max-content;
        }
        .StyleIcon.Green
        {
            height: 16px;
            width: 16px;
            color: Green;
            border-radius: 25% 10% 40% 10%;
            box-shadow: 2px 3px 0 0 #afada8;
            display: inline-block;
        }
        .StyleIcon.Yellow
        {
            height: 16px;
            width: 16px;
            color: Yellow;
            border-radius: 25% 10% 40% 10%;
            box-shadow: 2px 3px 0 0 #afada8;
            display: inline-block;
        }
        .StyleIcon.Gray
        {
            width: 16px;
            height: 16px;
            display: inline-block;
            position: absolute;
            left: 5px;
        }
        
        .AlignLeft
        {
            padding-left: 0px;
        }
        .marginleft
        {
            margin-left: -15px;
        }
        .uppercase
        {
            text-transform: uppercase;
        }
        .nav-tabs > li > a
        {
            display: flex;
            align-items: center;
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
                                    <h3 class="page-title-head d-inline-block">
                                        Add Employee</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="page-title-right">
                        <nav class="" aria-label="breadcrumb">
                                                <ol class="breadcrumb">
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Home</a></li>
                                                    <li class="breadcrumb-item"><a href="javascript:void(0);">Human Resources</a></li>
                                                    <li class="active breadcrumb-item" aria-current="page">Add Employee</li>
                                                </ol>
                                     </nav>
                        <a id="help" href="" alt="" style="margin-top: 4px;">
                            <img src="../Assets/images/help.png" />
                        </a>
                    </div>
                </div>
            </div>
        </div>
        <div class="photo-bg">
            <div class="col-sm-12">
                <div class="col-sm-4">
                </div>
                <div class="col-sm-6">
                    <asp:TextBox ID="txtEmployeeID" runat="server" CssClass="form-control" Width="250px"
                        Style="display: none" placeholder="Employee Name"></asp:TextBox>
                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" BehaviorID="AutoCompleteEx"
                        TargetControlID="txtEmployeeID" MinimumPrefixLength="1" EnableCaching="true"
                        CompletionSetCount="1" CompletionInterval="500" CompletionListCssClass="autocomplete_completionListElement"
                        OnClientPopulating="ShowProcessImage" OnClientPopulated="HideProcessImage" CompletionListItemCssClass="autocomplete_listItem"
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem" OnClientItemSelected="selectedvalue"
                        ServiceMethod="GetEmployee" ServicePath="AddEmployee.aspx">
                    </cc1:AutoCompleteExtender>
                    <asp:Button ID="btnGet" runat="server" CssClass="button" Text="Load" OnClick="btnget_click"
                        ValidationGroup="A" Style="display: none;" />
                    <%--   OnClientClick="ShowLoader();"--%>
                </div>
                <div class="col-sm-2">
                    <asp:LinkButton ID="btnAddEmployee" runat="server" CssClass="btn btn-cons btn-success add-emp"
                        Style="display: none" Text="Add Employee" OnClientClick="return AddEmployee();" />
                    <asp:HiddenField ID="hdnNew" runat="server" />
                </div>
            </div>
            <div class="row" style="padding: 0px 10px 0px 10px; display: none;" id="divBasicInfo"
                runat="server">
                <div class="col-sm-12">
                    <h4 class="h-style">
                        Basic Information</h4>
                </div>
                <div class="grid-body">
                    <asp:UpdatePanel ID="divBasicInfodetails" runat="server" UpdateMode="Always">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnPSave" />
                            <asp:PostBackTrigger ControlID="btnPCancel" />
                            <asp:PostBackTrigger ControlID="btnEPersonalCancel" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="col-md-12">
                                <div class="col-lg-4 col-md-6 col-sm-4">
                                    <%-- <asp:UpdatePanel ID="divBasicInfoUP" runat="server" UpdateMode="Always">
                                        <ContentTemplate>--%>
                                    <label class="form-label">
                                        Employee Photo
                                    </label>
                                    <asp:FileUpload CssClass="form-control" ID="fEmpPhoto" runat="server" Style="display: none"
                                        Width="95%" TabIndex="1" onchange="ShowImagePreview(this);" />
                                    <div class="col-sm-12" style="padding: 20px 0px 10px 50px;">
                                        <asp:Image ID="imgPhoto" ImageUrl="../Assets/images/NoPhoto.png" runat="server" Style="height: 200px;
                                            background: #f3f3f3; width: 180px; border: 2px dashed #ddd;" />
                                        <asp:Label ID="lblImageName" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblImageValue" runat="server" Visible="false"></asp:Label>
                                    </div>
                                    <%--  </ContentTemplate>
                                    </asp:UpdatePanel>--%>
                                </div>
                                <div id="divSPersonal" runat="server">
                                    <div class="col-lg-8 col-md-8 col-sm-8">
                                        <div class="p-t-10">
                                            <label class="form-label" style="padding-left: 15px">
                                                Name
                                            </label>
                                            <div class="col-sm-6 ">
                                                <asp:TextBox ID="txtFirstname" runat="server" CssClass="form-control uppercase" Width="95%"
                                                    PlaceHolder="First Name" autocomplete="nope" MaxLength="20" TabIndex="2" onkeypress="return validationAlphabets(this);"></asp:TextBox>
                                            </div>
                                            <%--<div class="col-sm-2" style="display: none">
                                <asp:TextBox ID="txtMiddleName" runat="server" CssClass="form-control uppercase"
                                    Width="95%" PlaceHolder="Middle Name" MaxLength="20" TabIndex="3" onkeypress="return validationAlphabets(this);"></asp:TextBox>
                            </div>--%>
                                            <div class="col-sm-6 ">
                                                <asp:TextBox ID="txtLastname" runat="server" CssClass="form-control uppercase" MaxLength="20"
                                                    PlaceHolder="Last Name" autocomplete="nope" Width="95%" TabIndex="4" onkeypress="return validationAlphabets(this);"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="p-t-10">
                                            <div class="col-sm-6 p-t-10">
                                                <label class="form-label">
                                                    Initials/Sub Name
                                                </label>
                                                <div>
                                                    <asp:TextBox ID="txtInitials" runat="server" CssClass="form-control" Width="95%"
                                                        PlaceHolder="Initals/Sub Name" autocomplete="nope" TabIndex="9" MaxLength="5"
                                                        onkeypress="return validationAlphabets(this);"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 p-t-10">
                                                <label class="form-label">
                                                    Gender
                                                </label>
                                                <div class="col-sm-12 marginleft">
                                                    <asp:RadioButtonList ID="rblGender" runat="server" CssClass="radio radio-success"
                                                        RepeatDirection="Horizontal" RepeatLayout="Flow" TabIndex="5">
                                                        <asp:ListItem Selected="True" Value="Male">Male</asp:ListItem>
                                                        <asp:ListItem Value="Female">Female</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="p-t-10">
                                            <div class="col-sm-6 p-t-10">
                                                <label class="form-label">
                                                    Date of Birth
                                                </label>
                                                <div>
                                                    <asp:TextBox ID="txtDob" runat="server" CssClass="form-control datepicker" TabIndex="7"
                                                        PlaceHolder="Date of Birth" autocomplete="nope" Width="95%"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 p-t-10">
                                                <label class="form-label">
                                                    Date of Joining
                                                </label>
                                                <div>
                                                    <asp:TextBox ID="txtDoj" runat="server" CssClass="form-control datepicker" TabIndex="8"
                                                        PlaceHolder="Date of Joining" autocomplete="nope" Width="95%"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="p-t-10">
                                            <div class="col-sm-6 p-t-10">
                                                <label class="form-label">
                                                    Department
                                                </label>
                                                <div>
                                                    <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control" Width="95%"
                                                        ToolTip="Select Department" TabIndex="11">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <%--  <div class="col-sm-6 p-t-10" style="display: none">
                                <label class="form-label">
                                    Title
                                </label>
                                <div>
                                    <asp:TextBox ID="txtTitle" runat="server" TabIndex="10" CssClass="form-control" onkeypress="return validationAlphabets(this);"></asp:TextBox>
                                </div>
                            </div>--%>
                                            <div class="col-sm-6 p-t-10">
                                                <label class="form-label">
                                                    Designation</label>
                                                <div>
                                                    <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control" Width="95%"
                                                        ToolTip="Select Designation" TabIndex="12">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-lg-4 col-md-4 col-sm-4">
                                            <label class="form-label">
                                                Employment Type
                                            </label>
                                            <div>
                                                <asp:DropDownList ID="ddlEmpType" runat="server" CssClass="form-control" Width="95%"
                                                    TabIndex="6">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                            <div class="col-sm-6">
                                                <label class="form-label">
                                                    Roles</label>
                                                <div>
                                                    <asp:DropDownList ID="ddlRoles" runat="server" CssClass="form-control" Width="95%"
                                                        ToolTip="Select Roles" TabIndex="13">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <label class="form-label">
                                                    ERP Roles</label>
                                                <div>
                                                    <asp:DropDownList ID="ddlERPRoles" runat="server" CssClass="form-control" Width="95%"
                                                        ToolTip="Select ERP Roles" TabIndex="14">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                            <div class="col-sm-4">
                                                <label class="form-label">
                                                    ERP Working Location</label>
                                                <div>
                                                    <asp:DropDownList ID="ddlEmployeeLocation" runat="server" CssClass="form-control" Width="95%"
                                                        ToolTip="Select ERP Roles" TabIndex="14">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                    </div>
                                    <div class="col-sm-12 p-t-40 text-center">
                                        <asp:LinkButton ID="btnPSave" runat="server" CssClass="btn btn-cons btn-save" Text="Save"
                                            TabIndex="11" OnClick="btnPSave_Click" OnClientClick="return btnPersonal();" />
                                        <asp:LinkButton ID="btnPCancel" runat="server" CssClass="btn btn-cons btn-danger"
                                            Text="Cancel" TabIndex="12" OnClick="btnPCancel_Click" OnClientClick="Cancel();" />
                                    </div>
                                </div>
                                <div id="divEPersonalDetails" runat="server" style="display: none">
                                    <div class="col-lg-8 col-md-6 col-sm-6">
                                        <div class="col-sm-6 p-t-10">
                                            <label class="form-label">
                                                First Name
                                            </label>
                                            <div>
                                                <asp:Label ID="lblfirstname" runat="server" Text="" Width="100%"></asp:Label>
                                            </div>
                                        </div>
                                        <%-- <div class="col-sm-4 p-t-10">
                                           <%-- <label class="form-label">
                                                Middle Name
                                            </label>
                                            <div class="">
                                                <asp:Label ID="lblMiddleName" runat="server" Text="" Width="100%"></asp:Label>
                                            </div>
                                        </div>--%>
                                        <div class="col-sm-6 p-t-10">
                                            <label class="form-label">
                                                Last Name
                                            </label>
                                            <div>
                                                <asp:Label ID="lbllastname" runat="server" Text="" Width="100%"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="p-t-10">
                                            <div class="col-sm-6">
                                                <label class="form-label">
                                                    Initials/Sub Name
                                                </label>
                                                <div>
                                                    <asp:Label ID="lblInitials" runat="server" Width="100%" TabIndex="3" MaxLength="5"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-6 p-t-10">
                                                <label class="form-label">
                                                    Gender
                                                </label>
                                                <div class="">
                                                    <asp:Label ID="lblGender" runat="server" Text="" Width="100%"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="p-t-10">
                                            <div class="col-sm-6">
                                                <label class="form-label">
                                                    Date of Birth
                                                </label>
                                                <div>
                                                    <asp:Label ID="lblDob" runat="server" TabIndex="7" Width="100%"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <label class="form-label">
                                                    Date of Joining
                                                </label>
                                                <div>
                                                    <asp:Label ID="lblDoj" runat="server" Width="100%"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="p-t-10">
                                            <div class="col-sm-6">
                                                <label class="form-label">
                                                    Department
                                                </label>
                                                <div>
                                                    <asp:Label ID="lblDepartment" runat="server" Width="100%"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <label class="form-label">
                                                    Designation
                                                </label>
                                                <div>
                                                    <asp:Label ID="lblDesignation" runat="server" Width="100%"></asp:Label>
                                                </div>
                                            </div>
                                            <%-- <div class="col-sm-6 p-t-10">
                                           <%-- <label class="form-label">
                                                Titile
                                            </label>
                                            <div>
                                                <asp:Label ID="lblTitle" runat="server" Width="100%"></asp:Label>
                                            </div>
                                        </div>--%>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-10">
                                        <div class="col-sm-4">
                                            <label class="form-label">
                                                Employment Type
                                            </label>
                                            <div>
                                                <asp:Label ID="lblEmpType" runat="server" Width="100%" TabIndex="11">
                                                </asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-sm-8">
                                            <div class="col-sm-6">
                                                <label class="form-label">
                                                    Roles
                                                </label>
                                                <div>
                                                    <asp:Label ID="lblRoles" runat="server" Width="100%"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-6">
                                                <label class="form-label">
                                                    ERP Roles</label>
                                                <div>
                                                    <asp:Label ID="lblERPRoles" runat="server" Width="100%"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 text-center p-t-40">
                                        <asp:LinkButton CssClass="btn btn-cons btn-success btn-edit" ID="btnEPersonal" runat="server"
                                            TabIndex="20" Text="Edit" OnClientClick="return ShowEPersonal();" />
                                        <asp:LinkButton CssClass="btn btn-cons btn-success btn-danger" ID="btnEPersonalCancel"
                                            runat="server" TabIndex="21" Text="Cancel" OnClientClick="Cancel();" OnClick="btnEPersonalCancel_Click" />
                                    </div>
                                </div>
                            </div>
                            <asp:HiddenField ID="hdnBasicInfoStatus" runat="server" />
                            <asp:HiddenField ID="hdnCommunicationStatus" runat="server" />
                            <asp:HiddenField ID="hdnRolesStatus" runat="server" />
                            <asp:HiddenField ID="hdnBankDetailsStatus" runat="server" />
                            <asp:HiddenField ID="hdnEducationProfileStatus" runat="server" />
                            <asp:HiddenField ID="hdnExperienceStatus" runat="server" />
                            <asp:HiddenField ID="hdnDocumentsStatus" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="row" style="padding: 10px 10px 0px 10px">
                <div id="Tabs" runat="server" class="tabpanel" style="display: none">
                    <ul id="Ul1" class="nav nav-tabs grid-title-new no-boarder" runat="server">
                        <li id="liCommunication" class="active"><a href="#Communication" class="tab-content"
                            data-toggle="tab" onclick="OpenTab('Communication');">
                            <%--  <asp:Label ID="image1" CssClass="StyleIcon Gray" runat="server" Style="margin-right: 10px">
                            </asp:Label>--%>
                            <asp:Label ID="lblImg1" runat="server">
                            </asp:Label>
                            <p style="margin-left: 10px">
                                Communication Details</p>
                        </a></li>
                        <li id="liRoles"><a href="#Roles" class="tab-content" data-toggle="tab" onclick="OpenTab('Roles');">
                            <%--  <asp:Label ID="image2" CssClass="StyleIcon Gray" runat="server" Style="margin-right: 10px">
                            </asp:Label>--%>
                            <asp:Label ID="lblImg2" runat="server">
                            </asp:Label>
                            <p style="margin-left: 10px">
                                Roles/Access Details</p>
                        </a></li>
                        <li id="liBankDetails"><a href="#BankDetails" class="tab-content" data-toggle="tab"
                            onclick="OpenTab('BankDetails');">
                            <%--<asp:Label ID="image3" CssClass="StyleIcon Gray" runat="server" Style="margin-right: 10px">
                            </asp:Label> --%>
                            <asp:Label ID="lblImg3" runat="server">
                            </asp:Label>
                            <p style="margin-left: 10px">
                                Bank Details</p>
                        </a></li>
                        <li id="liEducationProfile"><a href="#EducationProfile" class="tab-content" data-toggle="tab"
                            onclick="OpenTab('EducationProfile');">
                            <%-- <asp:Label ID="image4" CssClass="StyleIcon Gray" runat="server" Style="margin-right: 10px">
                            </asp:Label>--%>
                            <asp:Label ID="lblImg4" runat="server">
                            </asp:Label>
                            <p style="margin-left: 10px">
                                Education Profile</p>
                        </a></li>
                        <li id="liExperience"><a href="#Experience" class="tab-content" data-toggle="tab"
                            onclick="OpenTab('Experience');">
                            <%-- <asp:Label ID="image5" runat="server">
                            </asp:Label>--%>
                            <asp:Label ID="lblImg5" runat="server">
                            </asp:Label>
                            <p style="margin-left: 10px">
                                Experience Details</p>
                        </a></li>
                        <li id="liDocuments"><a href="#Documents" class="tab-content" data-toggle="tab" onclick="OpenTab('Documents');">
                            <%--<asp:Label ID="image6" CssClass="StyleIcon Gray" runat="server" Style="margin-right: 10px">
                            </asp:Label>--%>
                            <asp:Label ID="lblImg6" runat="server">
                            </asp:Label>
                            <p style="margin-left: 10px">
                                Documents</p>
                        </a></li>
                    </ul>
                    <div class="tab-content" style="padding: 10px 10px 10px 10px;">
                        <div class="tab-pane active" id="Communication" runat="server">
                            <asp:UpdatePanel ID="divCommunication" runat="server" UpdateMode="Always">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnCSave" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="row" id="divSComm" runat="server" style="display: none">
                                        <div class="col-sm-12">
                                            <h4 class="h-style">
                                                Permanent Address
                                            </h4>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    House Number
                                                </label>
                                                <div>
                                                    <asp:TextBox ID="txtPPbox" runat="server" CssClass="form-control" Width="95%" MaxLength="30"
                                                        PlaceHolder="Enter House/Door Number" autocomplete="nope" TabIndex="12"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    Street/Road
                                                </label>
                                                <div>
                                                    <asp:TextBox ID="txtPStreet" runat="server" autocomplete="nope" CssClass="form-control"
                                                        Width="95%" PlaceHolder="Enter Street Name" TabIndex="13" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    Country
                                                </label>
                                                <div>
                                                    <asp:DropDownList ID="ddlPCon" runat="server" CssClass="form-control" TabIndex="14"
                                                        OnSelectedIndexChanged="ddlPCoun_SelectedIndexChanged" AutoPostBack="True" Width="95%">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    State/Province
                                                </label>
                                                <div>
                                                    <asp:DropDownList ID="ddlPState" runat="server" AutoPostBack="True" CssClass="form-control"
                                                        OnSelectedIndexChanged="ddlPState_SelectedIndexChanged" Width="95%" TabIndex="15">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    City
                                                </label>
                                                <div>
                                                    <asp:DropDownList ID="ddlPCity" runat="server" CssClass="form-control" Width="95%"
                                                        TabIndex="16">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    Zip Code/Pin Code
                                                </label>
                                                <div>
                                                    <asp:TextBox ID="txtPZip" runat="server" autocomplete="nope" CssClass="form-control"
                                                        Width="95%" onkeypress="return validationNumeric(this);" PlaceHolder="Enter your 6 digit Zipcode/Pincode Number"
                                                        MaxLength="6"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <asp:CheckBox ID="chkAddress" runat="server" AutoPostBack="true" OnCheckedChanged="chkAddress_CheckedChanged"
                                                CssClass="Checkbox" />
                                            <label style="padding: 5px 0px 0px 10px;">
                                                same as Permanent Address</label>
                                        </div>
                                        <div class="col-sm-12">
                                            <h4 class="h-style">
                                                Communication Address
                                            </h4>
                                        </div>
                                        <div class="col-sm-12 no-boarder p-t-10">
                                            <div class="col-sm-4">
                                                <label class="form-label">
                                                    House Number
                                                </label>
                                                <div>
                                                    <asp:TextBox ID="txtCpbox" autocomplete="nope" runat="server" CssClass="form-control"
                                                        PlaceHolder="Enter House/Door Number" MaxLength="30" Width="95%"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <label class="form-label">
                                                    Street/Road</label>
                                                <div>
                                                    <asp:TextBox ID="txtCStreet" autocomplete="nope" runat="server" CssClass="form-control"
                                                        PlaceHolder="Enter Street Name" MaxLength="50" Width="95%"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <label class="form-label">
                                                    Country</label>
                                                <div>
                                                    <asp:DropDownList ID="ddlCCoun" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCCoun_SelectedIndexChanged"
                                                        CssClass="form-control" Width="95%">
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txtCCoun" runat="server" CssClass="form-control" Visible="False"
                                                        Width="95%" MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 no-boarder p-t-10">
                                            <div class="col-sm-4">
                                                <label class="form-label">
                                                    State/Province</label>
                                                <div>
                                                    <asp:DropDownList ID="ddlCState" runat="server" AutoPostBack="True" CssClass="form-control"
                                                        Width="95%" OnSelectedIndexChanged="ddlCState_SelectedIndexChanged">
                                                        <%-- onchange="ShowLoader();--%>
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txtCSt" runat="server" CssClass="form-control" Visible="False" MaxLength="50"
                                                        Width="95%"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <label class="form-label">
                                                    City</label>
                                                <div>
                                                    <asp:DropDownList ID="ddlCCity" runat="server" CssClass="form-control" Width="95%">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txtCCity" runat="server" CssClass="form-control" Width="95%" Visible="False"
                                                        MaxLength="50"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4">
                                                <label class="form-label">
                                                    Zipcode/Pincode
                                                </label>
                                                <div>
                                                    <asp:TextBox ID="txtCZip" autocomplete="nope" runat="server" CssClass="form-control"
                                                        onkeypress="return validationNumeric(this);" PlaceHolder="Enter your 6 digit Zipcode/Pincode Number"
                                                        MaxLength="6" Width="95%"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <h4 class="h-style">
                                                Contact Details :
                                            </h4>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    Personal Email Id
                                                </label>
                                                <div>
                                                    <asp:TextBox ID="txtEmail" autocomplete="nope" runat="server" AutoCompleteType="Email"
                                                        CssClass="form-control" Width="95%" placeHolder="Enter EmailID e.g:'example@example.com'"
                                                        MaxLength="75" onkeypress="return validateEmail(this);"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    Mobile Number
                                                </label>
                                                <div>
                                                    <asp:TextBox ID="txtMobileno" autocomplete="nope" runat="server" AutoCompleteType="Cellular"
                                                        CssClass="form-control" Width="95%" placeholder="Enter Valid 10 digit Mobile Number"
                                                        MaxLength="10" onkeypress="return validatePhoneNo(this);">
                                                    </asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    Alternate Mobile Number
                                                </label>
                                                <div>
                                                    <asp:TextBox ID="txtPhoneno" autocomplete="nope" Width="95%" runat="server" AutoCompleteType="HomePhone"
                                                        placeholder="Enter Valid 10 digit Alternate Mobile Number" CssClass="form-control"
                                                        MaxLength="10" onkeypress="return validatePhoneNo(this);"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    Corporate Email Id
                                                </label>
                                                <div>
                                                    <asp:TextBox ID="txtCorporateEMail" autocomplete="nope" runat="server" AutoCompleteType="Email"
                                                        CssClass="form-control" Width="95%" placeHolder="Enter EmailID e.g:'example@example.com'"
                                                        MaxLength="75" onkeypress="return validateEmail(this);"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    Primary Email
                                                </label>
                                                <div class="col-sm-12 AlignLeft">
                                                    <asp:RadioButtonList ID="rblPrimaryEmail" runat="server" CssClass="radio radio-success"
                                                        RepeatDirection="Horizontal" Width="95%" AutoPostBack="false">
                                                        <asp:ListItem Text="Personal Email" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="Corporate Email" Value="1" Selected="True"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    Primary Mobile Number
                                                </label>
                                                <div class="col-sm-12 AlignLeft">
                                                    <asp:RadioButtonList ID="rblPrimaryMobileNo" runat="server" CssClass="radio radio-success"
                                                        RepeatDirection="Horizontal" Width="95%" AutoPostBack="false">
                                                        <asp:ListItem Text="Personal Mobile Number" Value="0" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="AlterNate Mobile Number" Value="1"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-20 text-center">
                                            <asp:LinkButton ID="btnCSave" runat="server" CssClass="btn btn-cons btn-save" Text="Save"
                                                OnClick="btnCSave_Click" OnClientClick="return btnCommunication();" />
                                        </div>
                                    </div>
                                    <div class="col-sm-12" id="divEComm" runat="server" style="display: none">
                                        <div class="p-t-10">
                                            <h4>
                                                <span class="">Permanent Address</span>
                                            </h4>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-sm-4 p-t-10">
                                                    <label class="form-label">
                                                        House Number
                                                    </label>
                                                    <div>
                                                        <asp:Label ID="lblPbox" runat="server" Text="" Width="100%"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4 p-t-10">
                                                    <label class="form-label">
                                                        Street/Road
                                                    </label>
                                                    <div>
                                                        <asp:Label ID="lblPStreet" runat="server" Text="" Width="100%"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4 p-t-10">
                                                    <label class="form-label">
                                                        Country
                                                    </label>
                                                    <div>
                                                        <asp:Label ID="lblPCoun" runat="server" Text="" Width="100%"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="col-sm-4 p-t-10">
                                                    <label class="form-label">
                                                        State/Province
                                                    </label>
                                                    <div>
                                                        <asp:Label ID="lblPState" runat="server" Text="" Width="100%"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4 p-t-10">
                                                    <label class="form-label">
                                                        City
                                                    </label>
                                                    <div>
                                                        <asp:Label ID="lblPCity" runat="server" Text="" Width="100%"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4 p-t-10">
                                                    <label class="form-label">
                                                        Zipcode/Pincode
                                                    </label>
                                                    <div>
                                                        <asp:Label ID="lblPZip" runat="server" Text="" Width="100%"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="p-t-10">
                                            <h4>
                                                <span class="">Communication Address</span>
                                            </h4>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-sm-4 p-t-10">
                                                    <label class="form-label">
                                                        House Number
                                                    </label>
                                                    <div>
                                                        <asp:Label ID="lblCpbox" runat="server" Text="" Width="100%"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4 p-t-10">
                                                    <label class="form-label">
                                                        Street/Road
                                                    </label>
                                                    <div>
                                                        <asp:Label ID="lblCStreet" runat="server" Text="" Width="100%"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4 p-t-10">
                                                    <label class="form-label">
                                                        Country
                                                    </label>
                                                    <div>
                                                        <asp:Label ID="lblCCoun" runat="server" Text="" Width="100%"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="col-sm-4 p-t-10">
                                                    <label class="form-label">
                                                        State/Province
                                                    </label>
                                                    <div>
                                                        <asp:Label ID="lblCState" runat="server" Text="" Width="100%"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4 p-t-10">
                                                    <label class="form-label">
                                                        City
                                                    </label>
                                                    <div>
                                                        <asp:Label ID="lblCCity" runat="server" Text="" Width="100%"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4 p-t-10">
                                                    <label class="form-label">
                                                        Zipcode/Pincode
                                                    </label>
                                                    <div>
                                                        <asp:Label ID="lblCZip" runat="server" Text="" Width="100%"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="p-t-10">
                                            <h4>
                                                <span class="semi-bold">Contact Details</span>
                                            </h4>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-sm-4 p-t-10">
                                                    <label class="form-label">
                                                        Presonal Email Id
                                                    </label>
                                                    <div class="">
                                                        <asp:Label ID="lblSEmail" runat="server" Text="" Width="100%"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4 p-t-10">
                                                    <label class="form-label">
                                                        Mobile Number
                                                    </label>
                                                    <div>
                                                        <asp:Label ID="lblStudMobileNo" runat="server" Text="" Width="100%"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4 p-t-10">
                                                    <label class="form-label">
                                                        Alternate Phone Number
                                                    </label>
                                                    <div>
                                                        <asp:Label ID="lblPhoneNo" runat="server" Text="" Width="100%"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-sm-4 p-t-10">
                                                    <label class="form-label">
                                                        Corporate Email Id
                                                    </label>
                                                    <div class="">
                                                        <asp:Label ID="lblCorporateEmail" runat="server" Text="" Width="100%"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4 p-t-10">
                                                    <label class="form-label">
                                                        Primary Mail
                                                    </label>
                                                    <div>
                                                        <asp:Label ID="lblPrimaryEmail" runat="server" Text="" Width="100%"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4 p-t-10">
                                                    <label class="form-label">
                                                        Primary Mobile Number
                                                    </label>
                                                    <div>
                                                        <asp:Label ID="lblPrimaryMobileNo" runat="server" Text="" Width="100%"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-20 text-center" style="margin-bottom: 20px;">
                                            <asp:LinkButton ID="btnEComm" runat="server" CssClass="btn btn-cons btn-success btn-edit"
                                                Text="Edit" TabIndex="41" OnClientClick="return ShowEComm();" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="tab-pane" id="Roles" runat="server">
                            <asp:UpdatePanel ID="divRoles" runat="server" UpdateMode="Always">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnRSave" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="row" id="divSRoles" runat="server" style="display: none">
                                        <div class="col-sm-12">
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    Access Card No
                                                </label>
                                                <div>
                                                    <asp:TextBox ID="txtAccessCardNo" autocomplete="nope" runat="server" CssClass="form-control"
                                                        Width="95%" ToolTip="Enter Access Card No" placeholder="Enter Access Card No"
                                                        TabIndex="24" MaxLength="20"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    Reporting To</label>
                                                <asp:DropDownList runat="server" TabIndex="22" ID="ddlReportingTo" CssClass="form-control"
                                                    ToolTip="Select Reporting To" Width="95%">
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    Qualification
                                                </label>
                                                <div>
                                                    <asp:TextBox runat="server" autocomplete="nope" TabIndex="5" CssClass="form-control"
                                                        ID="txtQualification" ToolTip="Enter Qualification" placeholder="Enter Qualification"
                                                        Width="95%" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    Employee Code
                                                </label>
                                                <div>
                                                    <asp:TextBox ID="txtEmployeeCode" autocomplete="nope" TabIndex="27" runat="server"
                                                        CssClass="form-control" ToolTip="Enter Employee Code" placeholder="Enter Employee Code"
                                                        Width="95%"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    Religion
                                                </label>
                                                <div>
                                                    <asp:DropDownList ID="ddlReligion" runat="server" CssClass="form-control" Width="95%"
                                                        ToolTip="Select Religion" TabIndex="9">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    Blood Group
                                                </label>
                                                <div>
                                                    <asp:DropDownList ID="ddlBloodGroup" runat="server" CssClass="form-control" Width="95%"
                                                        ToolTip="Select Blood Group" TabIndex="9">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    Nationality
                                                </label>
                                                <div>
                                                    <asp:DropDownList ID="ddlNationality" runat="server" CssClass="form-control" Width="95%"
                                                        ToolTip="Select Nationality" TabIndex="9">
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    Validity Till
                                                </label>
                                                <div>
                                                    <asp:TextBox ID="txtValidityTill" runat="server" CssClass="form-control" Width="95%"
                                                        ToolTip="Enter Validity Till Date" autocomplete="nope" placeholder="Enter Validity Till Date"
                                                        TabIndex="9"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    Reference ID
                                                </label>
                                                <div>
                                                    <asp:TextBox ID="txtrefID" runat="server" CssClass="form-control" ToolTip="Enter Reference ID"
                                                        placeholder="Enter Reference ID" autocomplete="nope" Width="95%" TabIndex="9"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-20">
                                            <div class="text-center">
                                                <asp:LinkButton ID="btnRSave" runat="server" CssClass="btn btn-cons btn-save" Text="Save Roles"
                                                    OnClick="btnRSave_Click" OnClientClick="return btnRoles();" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" id="divERoles" runat="server" style="display: none;">
                                        <div class="col-sm-12">
                                            <div class="col-sm-4 p-t-10 text-center">
                                                <label class="form-label">
                                                    Access Card No
                                                </label>
                                                <div>
                                                    <asp:Label ID="lblAccessCardNo" runat="server" Width="100%"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10 text-center">
                                                <label class="form-label">
                                                    Reporting To
                                                </label>
                                                <div>
                                                    <asp:Label ID="lblReportingTo" runat="server" Width="100%"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10 text-center">
                                                <label class="form-label">
                                                    Highest Qualification
                                                </label>
                                                <div>
                                                    <asp:Label runat="server" TabIndex="5" Width="100%" ID="lblQualification" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-4 p-t-10 text-center">
                                                <label class="form-label">
                                                    Employee Code
                                                </label>
                                                <div>
                                                    <asp:Label ID="lblEmployeeCode" TabIndex="26" Width="100%" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10 text-center">
                                                <label class="form-label">
                                                    Religion
                                                </label>
                                                <div>
                                                    <asp:Label ID="lblReligion" runat="server" Width="100%" TabIndex="9" />
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10 text-center">
                                                <label class="form-label">
                                                    Blood Group
                                                </label>
                                                <div>
                                                    <asp:Label ID="lblBloodGroup" runat="server" Width="100%" TabIndex="9" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-4 p-t-10 text-center">
                                                <label class="form-label">
                                                    Nationality
                                                </label>
                                                <div>
                                                    <asp:Label ID="lblNational" runat="server" Width="100%"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10 text-center">
                                                <label class="form-label">
                                                    Validity Till
                                                </label>
                                                <div class="col-sm-12 AlignLeft">
                                                    <asp:Label ID="lblValidityTill" runat="server" Width="100"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10 text-center">
                                                <label class="form-label">
                                                    Reference ID
                                                </label>
                                                <div class="col-sm-12 AlignLeft">
                                                    <asp:Label ID="lblRefID" runat="server" Width="100"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-20 text-center m-b-20">
                                            <asp:LinkButton CssClass="btn btn-cons btn-success btn-edit" ID="btnERoles" runat="server"
                                                Text="Edit Roles" TabIndex="84" OnClientClick="return ShowERoles();" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="tab-pane" id="BankDetails" runat="server">
                            <asp:UpdatePanel ID="divBankDetails" runat="server" UpdateMode="Always">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnBSave" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="row" id="divSBankDetails" runat="server" style="display: none">
                                        <div class="col-sm-12">
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    PAN Number
                                                </label>
                                                <div>
                                                    <asp:TextBox ID="txtPanNumber" TabIndex="26" runat="server" CssClass="form-control"
                                                        ToolTip="Enter PAN Number" autocomplete="nope" placeholder="Enter PAN Number"
                                                        Width="95%"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    PF Number
                                                </label>
                                                <div>
                                                    <asp:TextBox ID="txtPFNumber" TabIndex="28" runat="server" CssClass="form-control"
                                                        ToolTip="Enter PF Number" autocomplete="nope" placeholder="Enter PF Number" Width="95%"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    Salary Payment Mode
                                                </label>
                                                <div>
                                                    <asp:DropDownList ID="ddlSalarypaymentMode" runat="server" CssClass="form-control"
                                                        ToolTip="Enter Salary Payment Mode" placeholder="Enter Salary Payment Mode" Width="95%">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    Employee ESI No
                                                </label>
                                                <div>
                                                    <asp:TextBox runat="server" TabIndex="32" ID="txtEmployeeESINo" CssClass="form-control"
                                                        ToolTip="Enter Employee ESI No" autocomplete="nope" placeholder="Enter Employee ESI No"
                                                        Width="95%" />
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    Aadhar Number
                                                </label>
                                                <div>
                                                    <asp:TextBox runat="server" TabIndex="32" autocomplete="nope" ID="txtAadharNo" CssClass="form-control"
                                                        ToolTip="Enter Aadhar Number" placeholder="Enter Aadhar Number" MaxLength="12"
                                                        Width="95%" onkeypress="return validationNumeric(this);" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <h4 class="h-style">
                                                Bank Details :
                                            </h4>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    Bank Name
                                                </label>
                                                <div>
                                                    <asp:TextBox runat="server" TabIndex="29" autocomplete="nope" ID="txtBankName" CssClass="form-control"
                                                        ToolTip="Enter Bank Name" placeholder="Enter Bank Name" Width="95%" />
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    Bank Account Number
                                                </label>
                                                <div>
                                                    <asp:TextBox ID="txtAccountNo" TabIndex="30" autocomplete="nope" runat="server" CssClass="form-control"
                                                        ToolTip="Enter Bank Account Number" placeholder="Enter Bank Account Number" Width="95%"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    Bank IFSC Code
                                                </label>
                                                <div>
                                                    <asp:TextBox runat="server" TabIndex="31" autocomplete="nope" ID="txtBankIFSCCode"
                                                        CssClass="form-control" ToolTip="Enter Bank IFSC Code" placeholder="Enter Bank IFSC Code"
                                                        Width="95%" />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-20 text-center">
                                            <asp:LinkButton ID="btnBSave" runat="server" CssClass="btn btn-cons btn-save" Text="Save"
                                                OnClick="btnBSave_Click" />
                                            <%--OnClientClick="ShowLoader();"--%>
                                        </div>
                                    </div>
                                    <div class="row" id="divEBankDetails" runat="server" style="display: none">
                                        <div class="col-sm-12">
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    PAN Number
                                                </label>
                                                <div>
                                                    <asp:Label ID="lblPanNumber" TabIndex="26" runat="server" Width="100%"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    PF Number
                                                </label>
                                                <div>
                                                    <asp:Label ID="lblPFNumber" TabIndex="26" runat="server" Width="100%"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    Employee ESI No
                                                </label>
                                                <div>
                                                    <asp:Label ID="lblEmployeeESINo" TabIndex="26" runat="server" Width="100%"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    Aadhar Number
                                                </label>
                                                <div>
                                                    <asp:Label ID="lblAadharNo" TabIndex="26" runat="server" Width="100%"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    Salary Payment Mode
                                                </label>
                                                <div>
                                                    <asp:Label ID="lblSalaryPaymentMode" TabIndex="26" runat="server" Width="100%"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10">
                                            <h4 class="h-style">
                                                BANK DETAILS :
                                            </h4>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    Bank Name
                                                </label>
                                                <div>
                                                    <asp:Label ID="lblBankName" TabIndex="26" runat="server" Width="100%"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    Bank Account Number
                                                </label>
                                                <div>
                                                    <asp:Label ID="lblAccountNo" TabIndex="26" runat="server" Width="100%"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10">
                                                <label class="form-label">
                                                    Bank IFSC Code
                                                </label>
                                                <div>
                                                    <asp:Label ID="lblBankIFSCCode" TabIndex="26" runat="server" Width="100%"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-20 m-b-20 text-center">
                                            <asp:LinkButton ID="btnEBankDetails" runat="server" CssClass="btn btn-cons btn-success btn-edit"
                                                Text="Edit" OnClientClick="return ShowEBankDetails()" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="tab-pane" id="EducationProfile" runat="server">
                            <asp:UpdatePanel ID="divEducationalProfile" runat="server" UpdateMode="Always">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnEdSave" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <div class="row" id="divSEducationProfile" runat="server">
                                            <div>
                                                <p class="semi-bold text-center" style="color: Red; font-size: 12px;">
                                                    **Update your educational records till date</p>
                                            </div>
                                            <div class="table-container">
                                                <div class="p-l-100">
                                                    <asp:GridView ID="gvShowAcaDetails" runat="server" ShowHeader="true" AutoGenerateColumns="false"
                                                        CssClass="table table-bordered table-hover no-more-tables" Width="100%" OnRowCommand="gvShowAcaDetails_RowCommand"
                                                        OnRowDataBound="gvShowAcaDetails_RowDataBound" DataKeyNames="EARID" ShowHeaderWhenEmpty="true"
                                                        OnRowEditing="gvShowAcaDetails_RowEditing" EmptyDataText="No Records Found">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Certification">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" Text='<%# Eval("Certification") %>' Font-Size="12px" ID="lblCertification" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Subject">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" Text='<%# Eval("Subject") %>' Font-Size="12px" ID="lblSubject" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Marks/Grade">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" Text='<%# Eval("Marks") %>' Font-Size="12px" ID="lblMarks" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Percentage">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" Text='<%# Eval("Percentage") %>' Font-Size="12px" ID="lblPercentage" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Board/University">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" Text='<%# Eval("Location") %>' Font-Size="12px" ID="lblLocation" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="YOC">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" Text='<%# Eval("YOC") %>' Font-Size="12px" ID="lblYOC" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Attachments">
                                                                <ItemTemplate>
                                                                    <asp:Label runat="server" Text='<%# Eval("Attachments") %>' Font-Size="12px" ID="lblAttachments" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="View">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnView" runat="server" Text="View" CommandName="View" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                        ToolTip="View">
                                                                       <%--  OnClientClick="ShowLoader();"--%>
                                                                               <img src="../Assets/images/view.png" style="height:20px" alt="" /></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Edit">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnEdit" runat="server" Text="View" CommandName="Edit" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                                        ToolTip="View">
                                                                     <%--   OnClientClick="ShowLoader();"--%>
                                                                                <img src="../Assets/images/edit-ec.png" style="height:20px" alt="" /></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-20 text-center">
                                                <asp:LinkButton ID="btnAdd" runat="server" CssClass="btn btn-cons btn-success add-emp"
                                                    Style="margin-bottom: 20px" OnClientClick="return btnAdd();" Text="Add Certification" />
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="tab-pane" id="Experience" runat="server">
                            <asp:UpdatePanel ID="divExperience" runat="server" UpdateMode="Always">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnExSave" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <div class="row" id="divSExperience" runat="server" style="display: none">
                                            <div class="col-sm-12">
                                                <div class="col-sm-6 paddingtop-10">
                                                    <label class="form-label">
                                                        Total Experience
                                                    </label>
                                                    <div class="col-sm-12 marginleft--15">
                                                        <div>
                                                            <asp:DropDownList ID="ddlTotExperienceYear" runat="server" CssClass="form-control"
                                                                Style="display: inline-block" Width="25%" TabIndex="6">
                                                                <asp:ListItem Value="0">0</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                                <asp:ListItem Value="6">6</asp:ListItem>
                                                                <asp:ListItem Value="7">7</asp:ListItem>
                                                                <asp:ListItem Value="8">8</asp:ListItem>
                                                                <asp:ListItem Value="9">9</asp:ListItem>
                                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                                <asp:ListItem Value="11">11</asp:ListItem>
                                                                <asp:ListItem Value="12">12</asp:ListItem>
                                                                <asp:ListItem Value="13">13</asp:ListItem>
                                                                <asp:ListItem Value="14">14</asp:ListItem>
                                                                <asp:ListItem Value="15">15</asp:ListItem>
                                                                <asp:ListItem Value="16">16</asp:ListItem>
                                                                <asp:ListItem Value="17">17</asp:ListItem>
                                                                <asp:ListItem Value="18">18</asp:ListItem>
                                                                <asp:ListItem Value="19">19</asp:ListItem>
                                                                <asp:ListItem Value="20">20</asp:ListItem>
                                                                <asp:ListItem Value="21">21</asp:ListItem>
                                                                <asp:ListItem Value="22">22</asp:ListItem>
                                                                <asp:ListItem Value="23">23</asp:ListItem>
                                                                <asp:ListItem Value="24">24</asp:ListItem>
                                                                <asp:ListItem Value="25">25</asp:ListItem>
                                                                <asp:ListItem Value="26">26</asp:ListItem>
                                                                <asp:ListItem Value="27">27</asp:ListItem>
                                                                <asp:ListItem Value="28">28</asp:ListItem>
                                                                <asp:ListItem Value="29">29</asp:ListItem>
                                                                <asp:ListItem Value="30">30</asp:ListItem>
                                                                <asp:ListItem Value="31">30+</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <label>
                                                                &nbsp; Years &nbsp;</label>
                                                            <asp:DropDownList ID="ddlTotExperienceMonth" runat="server" CssClass="form-control"
                                                                Style="display: inline-block" Width="25%" TabIndex="6">
                                                                <asp:ListItem Value="0">0</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                                <asp:ListItem Value="6">6</asp:ListItem>
                                                                <asp:ListItem Value="7">7</asp:ListItem>
                                                                <asp:ListItem Value="8">8</asp:ListItem>
                                                                <asp:ListItem Value="9">9</asp:ListItem>
                                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                                <asp:ListItem Value="11">11</asp:ListItem>
                                                                <asp:ListItem Value="12">12</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <label>
                                                                &nbsp;Months</label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 paddingtop-10">
                                                    <label class="form-label">
                                                        Relavent Experience
                                                    </label>
                                                    <div class="col-sm-12 marginleft--15">
                                                        <div>
                                                            <asp:DropDownList ID="ddlRelExperienceYear" runat="server" CssClass="form-control"
                                                                Style="display: inline-block" Width="25%" TabIndex="6">
                                                                <asp:ListItem Value="0">0</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                                <asp:ListItem Value="6">6</asp:ListItem>
                                                                <asp:ListItem Value="7">7</asp:ListItem>
                                                                <asp:ListItem Value="8">8</asp:ListItem>
                                                                <asp:ListItem Value="9">9</asp:ListItem>
                                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                                <asp:ListItem Value="11">11</asp:ListItem>
                                                                <asp:ListItem Value="12">12</asp:ListItem>
                                                                <asp:ListItem Value="13">13</asp:ListItem>
                                                                <asp:ListItem Value="14">14</asp:ListItem>
                                                                <asp:ListItem Value="15">15</asp:ListItem>
                                                                <asp:ListItem Value="16">16</asp:ListItem>
                                                                <asp:ListItem Value="17">17</asp:ListItem>
                                                                <asp:ListItem Value="18">18</asp:ListItem>
                                                                <asp:ListItem Value="19">19</asp:ListItem>
                                                                <asp:ListItem Value="20">20</asp:ListItem>
                                                                <asp:ListItem Value="21">21</asp:ListItem>
                                                                <asp:ListItem Value="22">22</asp:ListItem>
                                                                <asp:ListItem Value="23">23</asp:ListItem>
                                                                <asp:ListItem Value="24">24</asp:ListItem>
                                                                <asp:ListItem Value="25">25</asp:ListItem>
                                                                <asp:ListItem Value="26">26</asp:ListItem>
                                                                <asp:ListItem Value="27">27</asp:ListItem>
                                                                <asp:ListItem Value="28">28</asp:ListItem>
                                                                <asp:ListItem Value="29">29</asp:ListItem>
                                                                <asp:ListItem Value="30">30</asp:ListItem>
                                                                <asp:ListItem Value="31">30+</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <label>
                                                                &nbsp; Years &nbsp;</label>
                                                            <asp:DropDownList ID="ddlRelExperienceMonth" runat="server" CssClass="form-control"
                                                                Style="display: inline-block" Width="25%" TabIndex="6">
                                                                <asp:ListItem Value="0">0</asp:ListItem>
                                                                <asp:ListItem Value="1">1</asp:ListItem>
                                                                <asp:ListItem Value="2">2</asp:ListItem>
                                                                <asp:ListItem Value="3">3</asp:ListItem>
                                                                <asp:ListItem Value="4">4</asp:ListItem>
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                                <asp:ListItem Value="6">6</asp:ListItem>
                                                                <asp:ListItem Value="7">7</asp:ListItem>
                                                                <asp:ListItem Value="8">8</asp:ListItem>
                                                                <asp:ListItem Value="9">9</asp:ListItem>
                                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                                <asp:ListItem Value="11">11</asp:ListItem>
                                                                <asp:ListItem Value="12">12</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <label>
                                                                &nbsp; Months</label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <span class="semi-bold Paddingtop-10 text-center" style="color: Red; font-size: 12px">
                                                    ** List your work experience in reverse chronological order</span>
                                            </div>
                                            <div class="table-container">
                                                <asp:GridView ID="gvExperience" runat="server" ShowHeader="true" AutoGenerateColumns="false"
                                                    CssClass="table table-bordered table-hover no-more-tables" Width="100%" OnRowDataBound="gvExperience_OnRowDataBound"
                                                    DataKeyNames="EEID">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Name of Organization">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtExpOrg" PlaceHolder="Enter name of the organisation" CssClass="form-control"
                                                                    Style="text-align: left" Text='<%# Bind("Organization") %>' autocomplete="nope"
                                                                    MaxLength="100" runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Last Designation">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtExpDesig" PlaceHolder="Enter Designation" MaxLength="100" autocomplete="nope"
                                                                    CssClass="form-control" Style="text-align: left" Text='<%# Bind("Designation") %>'
                                                                    runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="From">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtExpFrom" PlaceHolder="Enter Designation" MaxLength="100" autocomplete="nope"
                                                                    CssClass="form-control datepicker" Style="text-align: left" Text='<%# Bind("StartYear") %>'
                                                                    runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="To">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtExpTo" PlaceHolder="Enter Designation" MaxLength="100" autocomplete="nope"
                                                                    CssClass="form-control datepicker" Style="text-align: left" Text='<%# Bind("EndYear") %>'
                                                                    runat="server"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-12 p-t-20 text-center">
                                                <asp:LinkButton ID="btnExSave" runat="server" CssClass="btn btn-cons btn-success btn-save"
                                                    Style="margin-bottom: 20px" Text="Save" OnClick="btnExSave_Click" />
                                                <%-- OnClientClick="ShowLoader();"--%>
                                            </div>
                                        </div>
                                        <div class="row" id="divEExperience" runat="server" style="display: none">
                                            <div class="col-sm-12">
                                                <div class="col-sm-6 paddingtop-10">
                                                    <label class="form-label" style="width: 100%">
                                                        Total Experience
                                                    </label>
                                                    <div>
                                                        <asp:Label ID="lblTotYear" runat="server" Text=""></asp:Label>
                                                        <label>
                                                            &nbsp; Years &nbsp;</label>
                                                        <asp:Label ID="lblTotMonth" runat="server" Text=""></asp:Label>
                                                        <label>
                                                            &nbsp; Months &nbsp;</label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-6 paddingtop-10">
                                                    <label class="form-label" style="width: 100%">
                                                        Relavent Experience
                                                    </label>
                                                    <div>
                                                        <asp:Label ID="lblRelYear" runat="server" Text=""></asp:Label>
                                                        <label>
                                                            &nbsp; Years &nbsp;</label>
                                                        <asp:Label ID="lblRelMonth" runat="server" Text=""></asp:Label>
                                                        <label>
                                                            &nbsp; Months &nbsp;</label>
                                                    </div>
                                                </div>
                                                <span class="semi-bold" style="color: Red; font-size: 12px">** List your work experience
                                                    in reverse chronological order</span>
                                            </div>
                                            <div class="table-container">
                                                <asp:GridView ID="gvShowExperience" runat="server" ShowHeader="true" AutoGenerateColumns="false"
                                                    ShowHeaderWhenEmpty="true" EmptyDataText="No Records Found" OnRowDataBound="gvShowExperience_OnRowDataBound"
                                                    CssClass="table table-bordered table-hover no-more-tables" Width="99%">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Name of Organization">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNameOfOrg" runat="server" Text='<%#Eval("Organization") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Last Designation">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDesignation" runat="server" Text='<%#Eval("Designation") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="From">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblExpFrom" runat="server" Text='<%#Eval("StartYear") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="To">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblExpTo" runat="server" Text='<%#Eval("EndYear") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-12 p-t-20  text-center m-b-10">
                                                <asp:LinkButton ID="btnEExperience" runat="server" CssClass="btn btn-cons btn-success btn-edit"
                                                    Text="Edit" OnClientClick="return ShowEExperience()" />
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="tab-pane" id="Documents" runat="server">
                            <asp:UpdatePanel ID="divDocs" runat="server" UpdateMode="Always">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnDSave" />
                                    <asp:PostBackTrigger ControlID="btnView1" />
                                </Triggers>
                                <ContentTemplate>
                                    <div class="row" id="divSDocuments" runat="server" style="display: none">
                                        <div class="col-sm-12">
                                            <div class="col-sm-12 p-t-10">
                                                <label class="form-label">
                                                    Appointment Letter
                                                </label>
                                                <div class="col-sm-12 AlignLeft">
                                                    <div class="col-sm-4 AlignLeft">
                                                        <asp:FileUpload ID="fAppointLetter" TabIndex="41" runat="server" CssClass="form-control"
                                                            onchange="ValidateDoc(this);" Width="95%" /></div>
                                                    <div class="col-sm-4">
                                                        <div class="col-sm-12 AlignLeft" id="divAppointmentLetter" runat="server" style="display: none">
                                                            <asp:Label ID="lblfAppointmentLetter" runat="server"></asp:Label>
                                                            <asp:LinkButton ID="btnfAppointmentLetter" runat="server" ToolTip="view" Style="padding-left: 10px;"
                                                                OnClientClick="ViewDoc(this);"><img src="../Assets/images/view.png" style="height:20px" alt="" /></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <label class="form-label">
                                                    Resume
                                                </label>
                                                <div class="col-sm-12 AlignLeft">
                                                    <div class="col-sm-4 AlignLeft">
                                                        <asp:FileUpload ID="fResume" TabIndex="41" runat="server" CssClass="form-control"
                                                            onchange="ValidateDoc(this);" Width="95%" /></div>
                                                    <div class="col-sm-4">
                                                        <div class="col-sm-12 AlignLeft" id="divResume" runat="server" style="display: none">
                                                            <asp:Label ID="lblfResume" runat="server"></asp:Label>
                                                            <asp:LinkButton ID="btnfResume" runat="server" ToolTip="view" Style="padding-left: 10px;"
                                                                OnClientClick="ViewDoc(this);"><img src="../Assets/images/view.png" style="height:20px" alt="" /></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <label class="form-label">
                                                    PAN Card
                                                </label>
                                                <div class="col-sm-12 AlignLeft">
                                                    <div class="col-sm-4 AlignLeft">
                                                        <asp:FileUpload ID="fPanCard" TabIndex="41" runat="server" CssClass="form-control"
                                                            onchange="ValidateDoc(this);" Width="95%" /></div>
                                                    <div class="col-sm-4">
                                                        <div class="col-sm-12  AlignLeft" id="divPanCard" runat="server" style="display: none">
                                                            <asp:Label ID="lblfPanCard" runat="server"></asp:Label>
                                                            <asp:LinkButton ID="btnfPanCard" runat="server" ToolTip="view" Style="padding-left: 10px;"
                                                                OnClientClick="ViewDoc(this);"><img src="../Assets/images/view.png" style="height:20px" alt="" /></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <label class="form-label">
                                                    Medical Report
                                                </label>
                                                <div class="col-sm-12 AlignLeft">
                                                    <div class="col-sm-4 AlignLeft">
                                                        <asp:FileUpload ID="fMedicalReport" TabIndex="41" runat="server" CssClass="form-control"
                                                            onchange="ValidateDoc(this);" Width="95%" /></div>
                                                    <div class="col-sm-4">
                                                        <div class="col-sm-12 AlignLeft" id="divMedical" runat="server" style="display: none">
                                                            <asp:Label ID="lblfMedical" runat="server"></asp:Label>
                                                            <asp:LinkButton ID="btnfMedical" runat="server" ToolTip="view" Style="padding-left: 10px;"
                                                                OnClientClick="ViewDoc(this);"><img src="../Assets/images/view.png" style="height:20px" alt="" /></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <label class="form-label">
                                                    Resignation Letter
                                                </label>
                                                <div class="col-sm-12 AlignLeft">
                                                    <div class="col-sm-4 AlignLeft">
                                                        <asp:FileUpload ID="fResignLetter" TabIndex="41" runat="server" CssClass="form-control"
                                                            onchange="ValidateDoc(this);" Width="95%" /></div>
                                                    <div class="col-sm-4">
                                                        <div class="col-sm-12 AlignLeft" id="divResignLetter" runat="server" style="display: none">
                                                            <asp:Label ID="lblfResignLetter" runat="server"></asp:Label>
                                                            <asp:LinkButton ID="btnfResignLetter" runat="server" ToolTip="view" Style="padding-left: 10px;"
                                                                OnClientClick="ViewDoc(this);"><img src="../Assets/images/view.png" style="height:20px" alt="" /></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <label class="form-label">
                                                    Education Papers
                                                </label>
                                                <div class="col-sm-12 AlignLeft">
                                                    <div class="col-sm-4 AlignLeft">
                                                        <asp:FileUpload ID="fEduPaper" TabIndex="41" runat="server" CssClass="form-control"
                                                            onchange="ValidateDoc(this);" Width="95%" /></div>
                                                    <div class="col-sm-4">
                                                        <div class="col-sm-12 AlignLeft" id="divEduPapers" runat="server" style="display: none">
                                                            <asp:Label ID="lblfEduPapers" runat="server"></asp:Label>
                                                            <asp:LinkButton ID="btnfEduPapers" runat="server" ToolTip="view" Style="padding-left: 10px;"
                                                                OnClientClick="ViewDoc(this);"><img src="../Assets/images/view.png" style="height:20px" alt="" /></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <label class="form-label">
                                                    PF Declaration Form
                                                </label>
                                                <div class="col-sm-12 AlignLeft">
                                                    <div class="col-sm-4 AlignLeft">
                                                        <asp:FileUpload ID="fPF" TabIndex="41" runat="server" CssClass="form-control" onchange="ValidateDoc(this);"
                                                            Width="95%" /></div>
                                                    <div class="col-sm-4 AlignLeft">
                                                        <div class="col-sm-12 AlignLeft" id="divPF" runat="server" style="display: none">
                                                            <asp:Label ID="lblfPF" runat="server"></asp:Label>
                                                            <asp:LinkButton ID="btnfPF" runat="server" ToolTip="view" Style="padding-left: 10px;"
                                                                OnClientClick="ViewDoc(this);"><img src="../Assets/images/view.png" style="height:20px" alt="" /></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <label class="form-label">
                                                    Bank Account Copy
                                                </label>
                                                <div class="col-sm-12 AlignLeft">
                                                    <div class="col-sm-4 AlignLeft">
                                                        <asp:FileUpload ID="fBankAcc" TabIndex="41" runat="server" CssClass="form-control"
                                                            onchange="ValidateDoc(this);" Width="95%" /></div>
                                                    <div class="col-sm-4 AlignLeft">
                                                        <div class="col-sm-12 AlignLeft" id="divBankAcc" runat="server" style="display: none">
                                                            <asp:Label ID="lblfBankAcc" runat="server"></asp:Label>
                                                            <asp:LinkButton ID="btnfBankAcc" runat="server" ToolTip="view" Style="padding-left: 10px;"
                                                                OnClientClick="ViewDoc(this);"><img src="../Assets/images/view.png" style="height:20px" alt="" /></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <label class="form-label">
                                                    Aadhar
                                                </label>
                                                <div class="col-sm-12 AlignLeft">
                                                    <div class="col-sm-4 AlignLeft">
                                                        <asp:FileUpload ID="fAadhar" TabIndex="41" runat="server" CssClass="form-control"
                                                            onchange="ValidateDoc(this);" Width="95%" /></div>
                                                    <div class="col-sm-4 AlignLeft">
                                                        <div class="col-sm-12 AlignLeft" id="divAadhar" runat="server" style="display: none">
                                                            <asp:Label ID="lblfAadhar" runat="server"></asp:Label>
                                                            <asp:LinkButton ID="btnfAadhar" runat="server" ToolTip="view" Style="padding-left: 10px;"
                                                                OnClientClick="ViewDoc(this);"><img src="../Assets/images/view.png" style="height:20px" alt="" /></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-sm-4 AlignLeft">
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <label class="form-label">
                                                    Other Document Name <span style="font-size: 8px; color: red">(If Applicable)</span>
                                                </label>
                                                <div class="col-sm-12 AlignLeft">
                                                    <div class="col-sm-4 AlignLeft">
                                                        <asp:TextBox ID="txtOther1Name" runat="server" CssClass="form-control" Width="95%" />
                                                    </div>
                                                    <div class="col-sm-4 AlignLeft">
                                                        <asp:FileUpload ID="fOther1DOC" runat="server" CssClass="form-control" Width="95%"
                                                            onchange="ValidateDoc(this);" />
                                                    </div>
                                                    <div class="col-sm-4 AlignLeft">
                                                        <div class="col-sm-12 AlignLeft" id="divOther1" runat="server" style="display: none">
                                                            <asp:Label ID="lblOther1" runat="server"></asp:Label>
                                                            <asp:LinkButton ID="btnOther1" runat="server" ToolTip="view" Style="padding-left: 10px;"
                                                                OnClientClick="ViewDoc(this);"><img src="../Assets/images/view.png" style="height:20px" alt="" /></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <label class="form-label">
                                                    Other Document Name <span style="font-size: 8px; color: red">(If Applicable)</span>
                                                </label>
                                                <div class="col-sm-12 AlignLeft">
                                                    <div class="col-sm-4 AlignLeft">
                                                        <asp:TextBox ID="txtOther2Name" runat="server" CssClass="form-control" Width="95%" />
                                                    </div>
                                                    <div class="col-sm-4 AlignLeft">
                                                        <asp:FileUpload ID="fOther2DOC" runat="server" CssClass="form-control" Width="95%"
                                                            onchange="ValidateDoc(this);" />
                                                    </div>
                                                    <div class="col-sm-4 AlignLeft">
                                                        <div class="col-sm-12 AlignLeft" id="divOther2" runat="server" style="display: none">
                                                            <asp:Label ID="lblOther2" runat="server"></asp:Label>
                                                            <asp:LinkButton ID="btnOther2" runat="server" ToolTip="view" Style="padding-left: 10px;"
                                                                OnClientClick="ViewDoc(this);"><img src="../Assets/images/view.png" style="height:20px" alt="" /></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <label class="form-label">
                                                    Other Document Name <span style="font-size: 8px; color: red">(If Applicable)</span>
                                                </label>
                                                <div class="col-sm-12 AlignLeft">
                                                    <div class="col-sm-4 AlignLeft">
                                                        <asp:TextBox ID="txtOther3Name" runat="server" CssClass="form-control" Width="95%" />
                                                    </div>
                                                    <div class="col-sm-4 AlignLeft">
                                                        <asp:FileUpload ID="fOther3DOC" runat="server" CssClass="form-control" Width="95%"
                                                            onchange="ValidateDoc(this);" />
                                                    </div>
                                                    <div class="col-sm-4 AlignLeft">
                                                        <div class="col-sm-12 AlignLeft" id="divOther3" runat="server" style="display: none">
                                                            <asp:Label ID="lblOther3" runat="server"></asp:Label>
                                                            <asp:LinkButton ID="btnOther3" runat="server" ToolTip="view" Style="padding-left: 10px;"
                                                                OnClientClick="ViewDoc(this);"><img src="../Assets/images/view.png" style="height:20px" alt="" /></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <label class="form-label">
                                                    Other Document Name <span style="font-size: 8px; color: red">(If Applicable)</span>
                                                </label>
                                                <div class="col-sm-12 AlignLeft">
                                                    <div class="col-sm-4 AlignLeft">
                                                        <asp:TextBox ID="txtOther4Name" runat="server" CssClass="form-control" Width="95%" />
                                                    </div>
                                                    <div class="col-sm-4 AlignLeft">
                                                        <asp:FileUpload ID="fOther4DOC" runat="server" CssClass="form-control" Width="95%"
                                                            onchange="ValidateDoc(this);" />
                                                    </div>
                                                    <div class="col-sm-4 AlignLeft">
                                                        <div class="col-sm-12 AlignLeft" id="divOther4" runat="server" style="display: none">
                                                            <asp:Label ID="lblOther4" runat="server"></asp:Label>
                                                            <asp:LinkButton ID="btnOther4" runat="server" ToolTip="view" Style="padding-left: 10px;"
                                                                OnClientClick="ViewDoc(this);"><img src="../Assets/images/view.png" style="height:20px" alt="" /></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12 p-t-10">
                                                <label class="form-label">
                                                    Other Document Name <span style="font-size: 8px; color: red">(If Applicable)</span>
                                                </label>
                                                <div class="col-sm-12 AlignLeft">
                                                    <div class="col-sm-4 AlignLeft">
                                                        <asp:TextBox ID="txtOther5Name" runat="server" CssClass="form-control" Width="95%" />
                                                    </div>
                                                    <div class="col-sm-4 AlignLeft">
                                                        <asp:FileUpload ID="fOther5DOC" runat="server" CssClass="form-control" Width="95%"
                                                            onchange="ValidateDoc(this);" />
                                                    </div>
                                                    <div class="col-sm-4 AlignLeft">
                                                        <div class="col-sm-12 AlignLeft" id="divOther5" runat="server" style="display: none">
                                                            <asp:Label ID="lblOther5" runat="server"></asp:Label>
                                                            <asp:LinkButton ID="btnOther5" runat="server" ToolTip="view" Style="padding-left: 10px;"
                                                                OnClientClick="ViewDoc(this);"><img src="../Assets/images/view.png" style="height:20px" alt="" /></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-20 text-center">
                                            <asp:LinkButton ID="btnDSave" runat="server" CssClass="btn btn-cons btn-success btn-save"
                                                Text="Submit" OnClick="btnDSave_Click" />
                                            <%-- OnClientClick="ShowLoader();"--%>
                                            <asp:Button ID="btnView1" runat="server" OnClick="btnView_Click" Style="display: none" />
                                            <asp:HiddenField ID="hdnFilePath" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row" id="divEDocuments" runat="server" style="display: none">
                                        <div class="col-sm-12">
                                            <div class="col-sm-4 p-t-10" id="divEAppointmentLetter" runat="server">
                                                <label class="form-label">
                                                    Appointment Letter
                                                </label>
                                                <div>
                                                    <asp:Label ID="lblEAppointmentLetter" runat="server" Width="100%"></asp:Label>
                                                    <asp:LinkButton ID="btnEAppointmentLetter" runat="server" ToolTip="view" Style="padding-left: 10px;
                                                        display: none" OnClientClick="ViewDoc(this);"><img src="../Assets/images/view.png" style="height:20px" alt="" /></asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10" id="divEResume" runat="server">
                                                <label class="form-label">
                                                    Resume
                                                </label>
                                                <div>
                                                    <asp:Label ID="lblEResume" runat="server" Width="100%"></asp:Label>
                                                    <asp:LinkButton ID="btnEResume" runat="server" OnClientClick="ViewDoc(this);" Style="padding-left: 10px;
                                                        display: none" ToolTip="view"><img src="../Assets/images/view.png" style="height:20px" alt="" /></asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10" id="divEPANCard" runat="server">
                                                <label class="form-label">
                                                    PAN Card
                                                </label>
                                                <div>
                                                    <asp:Label ID="lblEPANCard" runat="server" Width="100%"></asp:Label>
                                                    <asp:LinkButton ID="btnEPANCard" runat="server" OnClientClick="ViewDoc(this);" Style="padding-left: 10px;
                                                        display: none" ToolTip="view"><img src="../Assets/images/view.png" style="height:20px" alt="" /></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-4 p-t-10" id="divEMedicalReport" runat="server">
                                                <label class="form-label">
                                                    Medical Report
                                                </label>
                                                <div>
                                                    <asp:Label ID="lblEMedicalReport" runat="server" Width="100%"></asp:Label>
                                                    <asp:LinkButton ID="btnEMedicalReport" runat="server" OnClientClick="ViewDoc(this);"
                                                        Style="padding-left: 10px; display: none" ToolTip="view"><img src="../Assets/images/view.png" style="height:20px" alt="" /></asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10" id="divEResignationLetter" runat="server">
                                                <label class="form-label">
                                                    Resignation Letter
                                                </label>
                                                <div>
                                                    <asp:Label ID="lblEResignationLetter" runat="server" Width="100%"></asp:Label>
                                                    <asp:LinkButton ID="btnEResignationLetter" runat="server" OnClientClick="ViewDoc(this);"
                                                        Style="padding-left: 10px; display: none" ToolTip="view"><img src="../Assets/images/view.png" style="height:20px" alt="" /></asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10" id="divEEducationPapers" runat="server">
                                                <label class="form-label">
                                                    Education Papers
                                                </label>
                                                <div>
                                                    <asp:Label ID="lblEEducationPapers" runat="server" Width="100%"></asp:Label>
                                                    <asp:LinkButton ID="btnEEducationPapers" runat="server" OnClientClick="ViewDoc(this);"
                                                        Style="padding-left: 10px; display: none" ToolTip="view"><img src="../Assets/images/view.png" style="height:20px" alt="" /></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12">
                                            <div class="col-sm-4 p-t-10" id="divEPFDeclarationForm" runat="server">
                                                <label class="form-label">
                                                    PF Declaration Form
                                                </label>
                                                <div>
                                                    <asp:Label ID="lblEPFDeclarationForm" runat="server" Width="100%"></asp:Label>
                                                    <asp:LinkButton ID="btnEPFDeclarationForm" runat="server" OnClientClick="ViewDoc(this);"
                                                        Style="padding-left: 10px; display: none" ToolTip="view"><img src="../Assets/images/view.png" style="height:20px" alt="" /></asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10" id="divEBankAccountCopy" runat="server">
                                                <label class="form-label">
                                                    Bank Account Copy
                                                </label>
                                                <div>
                                                    <asp:Label ID="lblEBankAccountCopy" runat="server" Width="100%"></asp:Label>
                                                    <asp:LinkButton ID="btnEBankAccountCopy" runat="server" OnClientClick="ViewDoc(this);"
                                                        Style="padding-left: 10px; display: none" ToolTip="view"><img src="../Assets/images/view.png" style="height:20px" alt="" /></asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="col-sm-4 p-t-10" id="divEAadhar" runat="server">
                                                <label class="form-label">
                                                    Aadhar
                                                </label>
                                                <div>
                                                    <asp:Label ID="lblEAadhar" runat="server" Width="100%"></asp:Label>
                                                    <asp:LinkButton ID="btnEAadhar" runat="server" OnClientClick="ViewDoc(this);" Style="padding-left: 10px;
                                                        display: none" ToolTip="view"><img src="../Assets/images/view.png" style="height:20px" alt="" /></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10" style="display: none" id="divEOthers1" runat="server">
                                            <label class="form-label">
                                                Other Documents
                                            </label>
                                            <div class="col-sm-12">
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblOther1Name" runat="server" Width="95%" />
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:Label runat="server" ID="lblEOther1" />
                                                    <asp:LinkButton ID="btnEOther1" runat="server" ToolTip="view" Style="padding-left: 10px;"
                                                        OnClientClick="ViewDoc(this);"><img src="../Assets/images/view.png" style="height:20px" alt="" /></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10" id="divEOthers2" runat="server" style="display: none">
                                            <label class="form-label">
                                                Other Documents
                                            </label>
                                            <div class="col-sm-12">
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblOther2Name" runat="server" Width="95%" />
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:Label runat="server" ID="lblEOther2" />
                                                    <asp:LinkButton ID="btnEOther2" runat="server" ToolTip="view" Style="padding-left: 10px;"
                                                        OnClientClick="ViewDoc(this);"><img src="../Assets/images/view.png" style="height:20px" alt="" /></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10" id="divEOthers3" runat="server" style="display: none">
                                            <label class="form-label">
                                                Other Documents
                                            </label>
                                            <div class="col-sm-12">
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblOther3Name" runat="server" Width="95%" />
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:Label runat="server" ID="lblEOther3" />
                                                    <asp:LinkButton ID="btnEOther3" runat="server" ToolTip="view" Style="padding-left: 10px;"
                                                        OnClientClick="ViewDoc(this);"><img src="../Assets/images/view.png" style="height:20px" alt="" /></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10" id="divEOthers4" runat="server" style="display: none">
                                            <label class="form-label">
                                                Other Documents
                                            </label>
                                            <div class="col-sm-12">
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblOther4Name" runat="server" Width="95%" />
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:Label runat="server" ID="lblEOther4" />
                                                    <asp:LinkButton ID="btnEOther4" runat="server" ToolTip="view" Style="padding-left: 10px;"
                                                        OnClientClick="ViewDoc(this);"><img src="../Assets/images/view.png" style="height:20px" alt="" /></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-10" id="divEOthers5" runat="server" style="display: none">
                                            <label class="form-label">
                                                Other Documents
                                            </label>
                                            <div class="col-sm-12">
                                                <div class="col-sm-6">
                                                    <asp:Label ID="lblOther5Name" runat="server" Width="95%" />
                                                </div>
                                                <div class="col-sm-6">
                                                    <asp:Label runat="server" ID="lblEOther5" />
                                                    <asp:LinkButton ID="btnEOther5" runat="server" ToolTip="view" Style="padding-left: 10px;"
                                                        OnClientClick="ViewDoc(this);"><img src="../Assets/images/view.png" style="height:20px" alt="" /></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-12 p-t-20 text-center">
                                            <asp:LinkButton CssClass="btn btn-cons btn-success btn-doc btn-edit" ID="btnEDocs"
                                                runat="server" Text="Edit" TabIndex="111" OnClientClick="return showEDocs();" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hdnTab" runat="server" Value="0" />
            <asp:HiddenField ID="hdnFileName" runat="server" Value="" />
            <asp:HiddenField ID="hdnDocsOpen" runat="server" Value="0" />
        </div>
    </div>
    <div class="modal" id="divView">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="Updateview" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-header">
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                ×</button>
                            <h4 class="modal-title ADD" style="color: #fff;" id="H1">
                                Documnet
                            </h4>
                        </div>
                        <div class="modal-body">
                            <div class="inner-container">
                                <asp:Panel ID="pnlContent" runat="server" Style="z-index: 2;" Enabled="false">
                                    <div id="divContent" runat="server" class="holder-popheight" style="height: 450px;">
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                Close</button>
                        </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal" id="divAddAcadRec">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="upAddAcadRec" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnEdSave" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header" style="flex-direction: row-reverse; border-bottom: 0; background: #50a698;">
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                ×</button>
                            <h4 class="modal-title ADD" style="color: #fff; font-size: 24px; text-transform: capitalize;
                                font-weight: 500; flex-flow: row-reverse;">
                                Add Educational Qualification
                            </h4>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div class="inner-container">
                                <div class="col-sm-12 p-t-10">
                                    <div class="col-sm-12 paddingtop-10">
                                        <div class="col-sm-6">
                                            <label class="form-label p-t-10">
                                                Certification
                                            </label>
                                            <asp:TextBox ID="txtCertification" runat="server" CssClass="form-control" Width="95%"
                                                MaxLength="100" ToolTip="Enter Certification Name" autocomplete="nope" placeholder="Enter Certification Name"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-6">
                                            <label class="form-label p-t-10">
                                                Subject
                                            </label>
                                            <asp:TextBox ID="txtSubjects" runat="server" CssClass="form-control" Width="95%"
                                                MaxLength="100" ToolTip="Enter Subjects" autocomplete="nope" placeholder="Enter Subjects"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 paddingtop-10">
                                        <div class="col-sm-6">
                                            <label class="form-label p-t-10">
                                                Marks/Grade
                                            </label>
                                            <asp:TextBox ID="txtMarks" runat="server" CssClass="form-control" Width="95%" autocomplete="nope"
                                                ToolTip="Enter Marks/Grade" MaxLength="10" placeholder="Enter Marks/Grade"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-6">
                                            <label class="form-label p-t-10">
                                                Percentage
                                            </label>
                                            <asp:TextBox ID="txtPercentage" runat="server" CssClass="form-control" autocomplete="nope"
                                                Width="95%" onkeypress="return validationDecimal(this);" MaxLength="5" ToolTip="Enter Percentage"
                                                placeholder="Enter Precentage"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 paddingtop-10">
                                        <div class="col-sm-6">
                                            <label class="form-label p-t-10">
                                                Board/University
                                            </label>
                                            <asp:TextBox ID="txtBoard" runat="server" autocomplete="nope" CssClass="form-control"
                                                Width="95%" ToolTip="Enter Bord/University" MaxLength="100" placeholder="Enter Board/University"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-6">
                                            <label class="form-label p-t-10">
                                                YOC
                                            </label>
                                            <asp:DropDownList ID="ddlYOC" runat="server" CssClass="form-control" Width="95%"
                                                ToolTip="Select Year of Completeion" AutoPostBack="false">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 paddingtop-10">
                                        <div class="col-sm-6">
                                            <label class="form-label p-t-10">
                                                Attachment
                                            </label>
                                            <asp:FileUpload ID="fAttachment" runat="server" class="form-control" Width="95%" />
                                        </div>
                                        <div class="col-sm-6">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 p-t-20 text-center">
                                        <asp:LinkButton ID="btnEdSave" runat="server" CssClass="btn btn-cons btn-save" Text="Save"
                                            OnClick="btnEdSave_Click" OnClientClick="return btnEdAdd();" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <div class="modal" id="divEditAcadRec">
        <div class="modal-dialog">
            <div class="modal-content">
                <asp:UpdatePanel ID="upEditAcadRec" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnEdESave" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal-header" style="flex-direction: row-reverse; border-bottom: 0; background: #50a698;">
                            <button type="button" class="close btn-primary-purple" data-dismiss="modal" aria-hidden="true">
                                ×</button>
                            <h4 class="modal-title ADD" style="color: #fff; font-size: 24px; text-transform: capitalize;
                                font-weight: 500; flex-flow: row-reverse;">
                                Edit Educational Qualification
                            </h4>
                        </div>
                        <div class="modal-body" style="padding: 0px;">
                            <div class="inner-container">
                                <div class="col-sm-12 paddingtop-10">
                                    <div class="col-sm-12 paddingtop-10">
                                        <div class="col-sm-6">
                                            <label class="form-label">
                                                Certification
                                            </label>
                                            <asp:TextBox ID="txtECertification" runat="server" CssClass="form-control" Width="95%"
                                                Enabled="false" MaxLength="100" ToolTip="Enter Certification Name" placeholder="Enter Certification Name"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-6">
                                            <label class="form-label">
                                                Subject
                                            </label>
                                            <asp:TextBox ID="txtESubjects" runat="server" CssClass="form-control" Width="95%"
                                                MaxLength="100" ToolTip="Enter Subjects" placeholder="Enter Subjects"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 paddingtop-10">
                                        <div class="col-sm-6">
                                            <label class="form-label">
                                                Marks/Grade
                                            </label>
                                            <asp:TextBox ID="txtEMarks" runat="server" CssClass="form-control" Width="95%" ToolTip="Enter Marks/Grade"
                                                MaxLength="10" placeholder="Enter Marks/Grade"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-6">
                                            <label class="form-label">
                                                Percentage
                                            </label>
                                            <asp:TextBox ID="txtEPercentage" runat="server" CssClass="form-control" Width="95%"
                                                onkeypress="return validationDecimal(this);" MaxLength="5" ToolTip="Enter Percentage"
                                                placeholder="Enter Precentage"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 paddingtop-10">
                                        <div class="col-sm-6">
                                            <label class="form-label">
                                                Board/University
                                            </label>
                                            <asp:TextBox ID="txtEBoard" runat="server" CssClass="form-control" Width="95%" ToolTip="Enter Bord/University"
                                                MaxLength="100" placeholder="Enter Board/University"></asp:TextBox>
                                        </div>
                                        <div class="col-sm-6">
                                            <label class="form-label">
                                                YOC
                                            </label>
                                            <asp:DropDownList ID="ddlEYOC" runat="server" CssClass="form-control" Width="95%"
                                                ToolTip="Select Year of Completeion" AutoPostBack="false">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-sm-12 paddingtop-10">
                                        <div class="col-sm-6">
                                            <label class="form-label">
                                                Attachment
                                            </label>
                                            <asp:FileUpload ID="fEAttachment" runat="server" class="form-control" Width="95%" />
                                        </div>
                                        <div class="col-sm-6">
                                        </div>
                                    </div>
                                    <div class="col-sm-12 paddingtop-10">
                                        <div class="col-sm-4">
                                        </div>
                                        <div class="col-sm-3">
                                            <asp:Button ID="btnEdESave" runat="server" CssClass="btn btn-cons btn-success" Text="Save Education Profile"
                                                OnClick="btnEdESave_Click" OnClientClick="return btnEdEdit();" />
                                        </div>
                                        <div class="col-sm-5">
                                        </div>
                                    </div>
                                    <asp:HiddenField ID="hdnEARID" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
