<%@ Page Title="" Language="C#" MasterPageFile="~/Master/CommonMaster.master" AutoEventWireup="true"
    CodeFile="AsynFileupload.aspx.cs" Inherits="Pages_AsynFileupload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
       
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-sm-12 text-center">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="true">
            <Triggers>
            </Triggers>
            <ContentTemplate>
                <div class="col-sm-12 p-t-50">
                    <div class="col-sm-4">
                    </div>
                    <div class="col-sm-4"><%--OnClientUploadComplete="uploadComplete" OnClientUploadError="uploadError"--%>
                        <cc1:AsyncFileUpload ID="asynF" 
                            ClientIDMode="AutoID" runat="server" CssClass="form-control" Width="95%" OnUploadedComplete="FileUploadComplete" />
                    </div>
                    <div class="col-sm-4">
                    </div>
                </div>
                <br />
                <%--<asp:Image ID="Image1" runat="server" ImageUrl="../Assets/images/delete.png" />--%>
                <%-- <asp:Label ID="lblMesg" runat="server" BackColor="#CECACD"></asp:Label>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
