<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>MT Educare - Scheduling Engine</title>
    <meta name="description" content="User login page" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <!-- basic styles -->
    <link href="assets/css/bootstrap.min.css" rel="stylesheet" />
    <link href="assets/css/bootstrap-responsive.min.css" rel="stylesheet" />
    <link rel="stylesheet" href="assets/css/font-awesome.min.css" />
    <!--[if IE 7]>
		  <link rel="stylesheet" href="assets/css/font-awesome-ie7.min.css" />
		<![endif]-->
    <!-- page specific plugin styles -->
    <!-- ace styles -->
    <link rel="stylesheet" href="assets/css/ace.min.css" />
    <link rel="stylesheet" href="assets/css/ace-responsive.min.css" />
    <!--[if lt IE 9]>
		  <link rel="stylesheet" href="assets/css/ace-ie.min.css" />
		<![endif]-->
</head>
<body class="login-layout" style="zoom:88%">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid" id="main-container">
        <div id="main-content">
            <div class="row-fluid">
                <div class="span12">
                    <div class="login-container">
                        <div class="row-fluid">
                            <div class="center">
                                <img src="images/logo.jpg" alt="MT Educare" />
                                <h4 class="blue">
                                   MT Educare - Scheduling Engine
                                    <br />
                                    <asp:Label ID="Label1" Font-Size="X-Small" runat="server" Text="V2.3"></asp:Label>
                                </h4>
                            </div>
                        </div>
                        <div class="space-6">
                        </div>
                        <div class="row-fluid">
                            <div class="position-relative">
                                <div id="login-box" class="visible widget-box no-border">
                                    <div class="widget-body">
                                        <asp:UpdatePanel ID="UpdatePanelMsgBox" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div class="widget-main">
                                                    <h4 class="header lighter bigger white">
                                                        <i class="icon-coffee yellow"></i> Please Enter Your Information</h4>
                                                    <div class="space-6">
                                                    </div>
                                                    <%--  <form runat= "server" id ="FormLogin">--%>
                                                    <fieldset>
                                                        <label>
                                                            <span class="block input-icon input-icon-right">
                                                                <asp:TextBox runat="server" ID="txtuserName" CssClass="span12" placeholder="Username"
                                                                    ValidationGroup="a" OnTextChanged="txtuserName_TextChanged"></asp:TextBox>
                                                                <i class="icon-user"></i></span>
                                                        </label>
                                                        <label>
                                                            <span class="block input-icon input-icon-right">
                                                                <asp:TextBox runat="server" ID="txtuserPassord" Text="" TextMode="Password" CssClass="span12"
                                                                    placeholder="Password" ValidationGroup="a" OnTextChanged="txtuserPassord_TextChanged"></asp:TextBox>
                                                                <i class="icon-lock"></i></span>
                                                        </label>
                                                        <div class="space">
                                                        </div>
                                                        <div class="row-fluid">
                                                            <!-- <label class="span8">
							                                    <input type="checkbox" /><span class="lbl"> Remember Me</span>
						                                    </label> -->
                                                            <asp:Button class="span4 btn btn-small btn-warning" ID="btnLogin" runat="server"
                                                                Text="Login" OnClick="btnLogin_Click" />
                                                        </div>
                                                    </fieldset>
                                                    <%--</form>--%>
                                                </div>
                                                <!--/widget-main-->
                                                <div class="alert alert-error" id="Msg_Error" visible="false" runat="server">
                                                    <button type="button" class="close" data-dismiss="alert">
                                                        <i class="icon-remove"></i>
                                                    </button>
                                                    <p>
                                                        <strong><i class="icon-remove"></i>Error!</strong>
                                                        <asp:Label ID="lblerror" runat="server" Text="Label"></asp:Label>
                                                    </p>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <!--/widget-body-->
                                </div>
                                <!--/login-box-->
                                <div id="forgot-box" class="widget-box no-border">
                                    <div class="widget-body">
                                        <div class="widget-main">
                                            <h4 class="header red lighter bigger">
                                                <i class="icon-key"></i>Retrieve Password</h4>
                                            <div class="space-6">
                                            </div>
                                            <p>
                                                Enter your email and to receive instructions
                                            </p>
                                            <form>
                                            <fieldset>
                                                <label>
                                                    <span class="block input-icon input-icon-right">
                                                        <input type="email" class="span12" placeholder="Email" />
                                                        <i class="icon-envelope"></i></span>
                                                </label>
                                                <div class="row-fluid">
                                                    <button onclick="return false;" class="span5 offset7 btn btn-small btn-danger">
                                                        <i class="icon-lightbulb"></i>Send Me!</button>
                                                </div>
                                            </fieldset>
                                            </form>
                                        </div>
                                        <!--/widget-main-->
                                        <div class="toolbar center">
                                            <a href="#" onclick="show_box('login-box'); return false;" class="back-to-login-link">
                                                Back to login <i class="icon-arrow-right"></i></a>
                                        </div>
                                    </div>
                                    <!--/widget-body-->
                                </div>
                                <!--/forgot-box-->
                            </div>
                            <!--/position-relative-->
                        </div>
                    </div>
                </div>
                <!--/span-->
            </div>
            <!--/row-->
        </div>
    </div>
    <!--/.fluid-container-->
    <!-- basic scripts -->
    <script src="assets/js/jquery-1.9.1.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        window.jQuery || document.write("<script src='assets/js/jquery-1.9.1.min.js'>\x3C/script>");
    </script>
    <!-- page specific plugin scripts -->
    <!-- inline scripts related to this page -->
    <script type="text/javascript">
        function show_box(id) {
            $('.widget-box.visible').removeClass('visible');
            $('#' + id).addClass('visible');
        }
    </script>
    </form>
</body>
</html>
