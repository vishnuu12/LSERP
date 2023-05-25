<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Pages_Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="head1">
    <title>Lonestar ERP</title>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta http-equiv="Content-Language" content="en">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no, shrink-to-fit=no" />
    <meta name="description" content="ArchitectUI HTML Bootstrap 4 Dashboard Template">
    <!-- Disable tap highlight on IE -->
    <meta name="msapplication-tap-highlight" content="no">
     <link rel="stylesheet" type="text/css" href="../Assets/css/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="../Assets/css/main.css" />
    <link rel="stylesheet" type="text/css" href="../Assets/css/ep-style.css" />
    <link rel="apple-touch-icon" sizes="57x57" href="../Assets/Images/apple-icon-57x57.png">
    <link rel="apple-touch-icon" sizes="60x60" href="../Assets/Images/apple-icon-60x60.png">
    <link rel="apple-touch-icon" sizes="72x72" href="../Assets/Images/apple-icon-72x72.png">
    <link rel="apple-touch-icon" sizes="76x76" href="../Assets/Images/apple-icon-76x76.png">
    <link rel="apple-touch-icon" sizes="114x114" href="../Assets/Images/apple-icon-114x114.png">
    <link rel="apple-touch-icon" sizes="120x120" href="../Assets/Images/apple-icon-120x120.png">
    <link rel="apple-touch-icon" sizes="144x144" href="../Assets/Images/apple-icon-144x144.png">
    <link rel="apple-touch-icon" sizes="152x152" href="../Assets/Images/apple-icon-152x152.png">
    <link rel="apple-touch-icon" sizes="180x180" href="../Assets/Images/apple-icon-180x180.png">
    <link rel="icon" type="image/png" sizes="192x192" href="../Assets/Images/android-icon-192x192.png">
    <link rel="icon" type="image/png" sizes="32x32" href="../Assets/Images/favicon-32x32.png">
    <link rel="icon" type="image/png" sizes="96x96" href="../Assets/Images/favicon-96x96.png">
    <link rel="icon" type="image/png" sizes="16x16" href="../Assets/Images/favicon-16x16.png">
    <link rel="manifest" href="/manifest.json">
    <meta name="msapplication-TileColor" content="#ffffff">
    <meta name="msapplication-TileImage" content="/ms-icon-144x144.png">
    <meta name="theme-color" content="#ffffff">
    <script type="text/javascript" src="../Assets/scripts/SweetAlert.js"></script>
    <script type="text/javascript" src="../Assets/scripts/jquery-3.3.1.js"></script>
    <script type="text/javascript">
        function ForgotPassword() {
            var text = $('#lnKForgotPassword').text();
            if (text == 'Forgot Password') {
                $('#imglogo').attr('src', '../Assets/images/logo-inverse.png');
                $('#lnKForgotPassword').text("Back");
                $('#header').html("FORGOT PASSWORD" + "<br><span>Enter Your UserName and Password to Get Access</span>");
                $('#txtMobileNo').css('display', 'block');
                $('#txtPassword').css('display', 'none');
                $('#btnLogin').css('display', 'none');
                $('#btnForgotPassword').css('display', 'block');
            }
            else if (text == 'Back') {
                $('#header').html("WELCOME" + "<br><span>Enter Your UserName and Password to Get Access</span>");
                $('#imglogo').attr('src', '../Assets/images/logo-inverse.png');
                $('#lnKForgotPassword').text("Forgot Password");
                $('#txtMobileNo').css('display', 'none');
                $('#txtPassword').css('display', 'block');
                $('#btnLogin').css('display', 'block');
                $('#btnForgotPassword').css('display', 'none');

            }
            $('#txtUsername').val("");
            $('#txtPassword').val("");
            $('#txtMobileNo').val("");
            $('#txtUsername').focus();
            return false;
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="script" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="up" runat="server">
        <ContentTemplate>
            <div class="app-container app-theme-white body-tabs-shadow">
                <div class="app-container">
                    <div class="h-100 bg-animation" style="border-top: 5px solid #ecd537; height: auto;">
                    </div>
                        <!-- bg-plum-plate  -->                        
                        <div class="login-section">
                            <div class="container-fluid">
                                 <div class="col-md-5 sub-section">                                         
                                 </div>
                                 <div class="col-md-7 login-field">  
                                    <div class="title-head">
                                        <img src="../Assets/images/lonestar.png" class="logo1" alt="lonestar" />
                                        <h4 class="h4-sty">Welcome to Lonestar ERP</h4>
                                        <p>Enter Your UserName and Password to Get Access</p><br />
                                    </div>  
                                    <div class="position-relative form-group">
                                                            <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" Text="" TextMode="SingleLine"
                                                                placeholder="Username here..." autocomplete="Off"></asp:TextBox>
                                      </div>
                                    <div class="position-relative form-group">
                                                            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" Text="" TextMode="Password"
                                                                placeholder="Password here..." autocomplete="Off"></asp:TextBox>
                                                            <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" Text="" TextMode="SingleLine"
                                                                MaxLength="10" placeholder="Mobile Number here..." autocomplete="Off" Style="display: none"></asp:TextBox>
                                    </div>
                                    <div class="modal-footer modal-footer-login clearfix">
                                            <div class="float-right">
                                                <asp:Button ID="btnLogin" runat="server" Text=" Login to Dashboard" data-type="success"
                                                    class="btn btn-success" OnClick="btnLogin_Click" />
                                                <asp:Button ID="btnForgotPassword" runat="server" Text="ENTER" data-type="success"
                                                    class="btn btn-success" OnClick="btnForgotPassword_Click" Style="display: none" />
                                            </div>
                                            <div class="float-left">
                                                <asp:LinkButton ID="lnKForgotPassword" runat="server" Text="Forgot Password?" CssClass="btn-lg btn btn-link"
                                                    OnClientClick="return ForgotPassword();"></asp:LinkButton>
                                            </div>
                                            
                                        </div>    
                                   <div class="text-center text-black opacity-8 mt-3 pad-b">
                                    Copyright © 2019-20 Innovasphere Infotech</div>                                                                 
                                 </div>
                            </div>                           
                        </div>
                    </div>
                
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript" src="../Assets/scripts/main.js"></script>
    </form>
</body>
</html>
