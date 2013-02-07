<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginPopup.aspx.cs" ValidateRequest="false"
    Inherits="ModelPopup_LoginPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:fb="http://www.facebook.com/2008/fbml" xml:lang="en" lang="en">
<head>
    <title>Login Model Popup</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Content-Language" content="en-ca" />
    <meta http-equiv="imagetoolbar" content="false" />
    <meta name="robots" content="index,follow" />
    <meta name="MSSmartTagsPreventParsing" content="true" />
    <!-- really basic, generic html class stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/default.css" />
    <!-- default grid layout stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/layouts/centered1024_1.css" />
    <!-- print-friendly stylesheet -->
    <link rel="stylesheet" type="text/css" media="print" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/print.css" />
    <!-- screen elements stylesheet should be here -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/screen.css" />
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/LoginPopup.css" />
    <!--  <link rel="stylesheet" type="text/css" media="screen, projection" href="../test/assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/userprofile.css" /> -->
    <!-- JS libraries -->
    <script src="http://connect.facebook.net/en_US/all.js" type="text/javascript"></script>
    <script type="text/javascript" src="../assets/scripts/mootools-1.11.js"></script>

    <script type="text/javascript" src="../assets/scripts/global.js"></script>

    <script type="text/javascript" src="../Common/JavaScript/Common.js"></script>
    
<%--      <!--for ajax not working on staging server-->
    <script type="text/javascript" src="/DevelopementWebSite/ajaxpro/prototype.ashx"></script>
    <script type="text/javascript" src="/DevelopementWebSite/ajaxpro/core.ashx"></script>
    <script type="text/javascript" src="/DevelopementWebSite/ajaxpro/converter.ashx"></script>
    <script type="text/javascript" src="/DevelopementWebSite/ajaxpro/DevelopementWebSite.LoginPopup,DevelopementWebSite.ashx"></script> --%>


    <script type="text/javascript">
        window.addEvent('load', function() {
            buttonStyles();
            thumbStyles();
            hrFix();
            fieldsetFix();
        });
        function chk(locat) {
            parent.modalClose_();
        }

        function LoginUser(source, args) {
            setDefault();
            var messages = document.getElementById('<%= messages.ClientID %>');
            var username = document.getElementById('<%= txtLoginUsername1.ClientID %>');
            var password = document.getElementById('<%= txtLoginPassword1.ClientID %>');

            if ((username.value.length == 0) && (password.value.length == 0)) {
                var _htmlMsg = "<li>Username is a required field.</li>";
                _htmlMsg += "<li>Password is a required field.</li>";
                setIndicatorPassword();
                setIndicatorUser();
                messages.innerHTML = _htmlMsg;
                errorToParent();
                args.IsValid = false;
            }
            else if ((username.value.length != 0) && (password.value.length == 0)) {
                var _htmlMsg = "<li>Password is a required field.</li>";
                setIndicatorPassword();
                messages.innerHTML = _htmlMsg;
                errorToParent();
                args.IsValid = false;
            }
            else if ((username.value.length == 0) && (password.value.length != 0)) {
                var _htmlMsg = "<li>Username is a required field.</li>";
                setIndicatorUser();
                messages.innerHTML = _htmlMsg;
                errorToParent();
                args.IsValid = false;
            }
            else if (!checkUserName(username.value)) {
                var _htmlMsg = "<li>Invalid username or password.</li>";
                setIndicatorUser();
                messages.innerHTML = _htmlMsg;
                errorToParent();
                args.IsValid = false;
            }
            else
                args.IsValid = true;


        }




        function setIndicatorUser() {
            var erroru = document.getElementById('<%= errorPopUser.ClientID %>');
            erroru.innerText = '!';
            erroru.style.color = '#FF8000';
            erroru.style.fontSize = 'Medium';
            erroru.style.fontWeight = 'bold';
            erroru.style.visibility = 'visible';

        }

        function setIndicatorPassword() {
            var error = document.getElementById('<%= errorPopPwd.ClientID %>');
            if (error) {
                error.innerText = '!';
                error.style.color = '#FF8000';
                error.style.fontSize = 'Medium';
                error.style.fontWeight = 'bold';
                error.style.visibility = 'visible';
            }

        }

        function setForgotPassword_() {

            var _htmlMsg = "<li>Email Id does not exist.</li>";
            $('messages').innerHTML = _htmlMsg;
            errorToParent();



        }
        function setIndicatorPassword_() {
            var _htmlMsg = "<li>Invalid username or password.</li>";

            $('messages').innerHTML = _htmlMsg;
            errorToParent();



        }

        function setDefault() {
            var error2 = document.getElementById('<%= errorPwd.ClientID %>');
            var messages = document.getElementById('<%= messages.ClientID %>');
            messages.innerHTML = "";
            if (error2) {
                error2.style.visibility = 'hidden';
            }

        }

        function hideUserIndicator() {
            var error1 = document.getElementById('<%= errorPopUser.ClientID %>');
            var error2 = document.getElementById('<%= errorPopPwd.ClientID %>');
            var error3 = document.getElementById('<%= errorPwd.ClientID %>');
            if (error1)
                error1.style.visibility = 'hidden';
            if (error2)
                error2.style.visibility = 'hidden';
            if (error3)
                error3.style.visibility = 'hidden';
        }




        function errorToParent() {
            parentError = window.parent.document.getElementById('mb_Error_Popup');
            if ($('yt-LoginError')) {
                parentError.className = 'yt-Error';
                parentError.innerHTML = $('yt-LoginError').innerHTML;
            }
        }

        function validateEmail(source, args) {
            hideUserIndicator();
            var txtemail = document.getElementById('<%= txtLoginEmail1.ClientID %>');
            var messages = document.getElementById('<%= messages.ClientID %>');

            var retval = ValidateForm(txtemail);
            if (retval == false) {
                var _htmlMsg = "<li>Please enter a valid email address.</li>";
                messages.innerHTML = _htmlMsg;
                errorToParent();
            }
            args.IsValid = retval;

        }

        
        function echeck(str) {

            var at = "@"
            var dot = "."
            var lat = str.indexOf(at)
            var lstr = str.length
            var ldot = str.indexOf(dot)
            if (str.indexOf(at) == -1) {
                return false
            }

            if (str.indexOf(at) == -1 || str.indexOf(at) == 0 || str.indexOf(at) == lstr) {
                return false
            }

            if (str.indexOf(dot) == -1 || str.indexOf(dot) == 0 || str.indexOf(dot) == lstr) {
                return false
            }

            if (str.indexOf(at, (lat + 1)) != -1) {

                return false
            }

            if (str.substring(lat - 1, lat) == dot || str.substring(lat + 1, lat + 2) == dot) {

                return false
            }

            if (str.indexOf(dot, (lat + 2)) == -1) {

                return false
            }

            if (str.indexOf(" ") != -1) {

                return false
            }

            return true
        }

        function ValidateForm(emailID) {
            if ((emailID.value == null) || (emailID.value == "")) {
                emailID.focus()
                return false
            }
            if (echeck(emailID.value) == false) {
                emailID.value = ""
                emailID.focus()
                return false
            }
            return true
        }

        function checkUserName(username) {
            var txtVal = username;
            var regEmailStr = "[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}";
            var regUserNameReg = "[A-Za-z0-9]";
            var EmailReg = new RegExp(regEmailStr);
            var UserNameReg = new RegExp(regUserNameReg);
            if (txtVal.match(EmailReg))
                return true;
            else if (txtVal.match(UserNameReg))
                return chkForMaxLength(16, txtVal.length);
            else
                return false;

        }

        function setCss() {
            if (navigator.userAgent.indexOf("Safari") != -1) {
                if (document.getElementById('<%= dashBorder.ClientID %>') != null) {
                    document.getElementById('<%= dashBorder.ClientID %>').className = "yt-dashed-Border_safari";
                }
            }
            if (navigator.userAgent.indexOf("MSIE 7.0") != -1) {
                if (document.getElementById('ForgetPassword') != null) {
                    document.getElementById('ForgetPassword').className = "yt-ForgetmarginIE7";
                }
            }
        }
    </script>

    <!--#include file="../analytics.asp"-->
    <style type="text/css">
        #Div1
        {
            height: 20px;
        }
    </style>
</head>
<body onload="javascript:setCss()";>
    <form id="Form1" action="" runat="server" defaultbutton="popuplbtnLogin" defaultfocus="txtLoginUsername1">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="divShowModalPopup">
    </div>    
    <div class="yt-Container" style="width: 550px; padding-left: 15px; padding-right: 10px;">
        <div id="yt-ContentContainer" class="yt-ModalWrapper">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div id="yt-ContentPrimaryContainer">
                        <div class="yt-Error" id="yt-LoginError" style="width: 632px">
                            <h2>
                                Oops - there was a problem in your login.</h2>
                            <h3>
                                Please correct the errors below:</h3>
                            <ul id="messages" runat="server">
                            </ul>
                        </div>
                        <fieldset class="yt-Form">
                            <div class="yt-padding">
                                <center>
                                    <div>
                                        <asp:Label ID="lbllogin" runat="server" Text="Log in to  Your Tribute" CssClass="yt-Form-Heading"></asp:Label>
                                    </div>
                                </center>
                                <div class="yt-margin-top">
                                    <div class="yt-label-left" style="color: #676B72;">
                                        <asp:Label ID="lblemailinfo" runat="server" CssClass="yt-Form-Label" Text="Enter your Your Tribute login info:"></asp:Label>
                                    </div>
                                    <div style="padding-left:5px; width:278px; float:left;">
                                        <asp:Label ID="lblFb" Text='Click "Connect" to log in using Facebook:' CssClass="yt-Form-Label"
                                            runat="server" style="position:relative;left:25px"></asp:Label></div>
                                </div>
                                <br class="yt-clear-both" />
                                <div style="float: left; width: 330px;">
                                    <div class="yt-margin-top">
                                        <div>
                                            <span class="yt-Form-asterisk">*</span> <span>
                                                <label class="yt-Form-Field-label">
                                                    Username/Email Address:</label></span>
                                        </div>
                                        <div class="yt-Div-txtbox">
                                            <asp:TextBox ID="txtLoginUsername1" CssClass="yt-Form-Field-Txtbox" runat="server"
                                                TabIndex="1"></asp:TextBox>
                                            <span id="errorPopUser" runat="server" style="visibility: hidden;">&nbsp;</span>
                                            <asp:CustomValidator ID="cvPopupLogin" ErrorMessage="Invalid Username and Password."
                                                Text="!" ClientValidationFunction="LoginUser" runat="server" Font-Bold="True"
                                                Font-Size="Medium" ForeColor="White" Visible="true" ValidateEmptyText="True"></asp:CustomValidator>
                                        </div>
                                        <div class="yt-Or-Label">
                                            <label class="yt-Form-Field-label">
                                                or</label>
                                        </div>
                                        <br class="yt-clear-both" />
                                    </div>
                                    <div class="yt-margintop">
                                        <div>
                                            <span class="yt-Form-asterisk">*</span> <span>
                                                <label class="yt-Form-Field-label">
                                                    Password:</label></span>
                                        </div>
                                        <div class="yt-Div-txtbox">
                                            <asp:TextBox ID="txtLoginPassword1" CssClass="yt-Form-Field-Txtbox yt-Form-Input-Password"
                                                runat="server" TextMode="Password" TabIndex="2"></asp:TextBox>
                                            <span id="errorPopPwd" runat="server"></span><span id="errorPwd" runat="server" style="color: #FF8000;
                                                font-size: Medium; font-weight: bold;" visible="false">!</span>
                                        </div>
                                    </div>
                                </div>
                                <div id="fb-root" style="float: left; margin-left: 15px; height: 30px; width: 190px;  margin-top:60px">
                                    <fb:login-button  onlogin="doAjaxLogin();" length="medium" ><fb:intl>Connect</fb:intl></fb:login-button>
                                </div>
                                <br class="yt-clear-both" />
                                <div class="yt-Form-SubmitBtn">
                                    <asp:LinkButton ID="popuplbtnLogin" CssClass="yt-Button" runat="server" OnClick="popuplbtnLogin_Click1"
                                        TabIndex="3">Log in to Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%></asp:LinkButton>
                                </div>
                                <br class="yt-clear-both" />
                                <div id="Forgetpassword" runat="server" class="yt-Forgetmargin">
                                    <center>
                                        <div>
                                            <asp:Label ID="lblForgotpwd" runat="server" Text="Forgot Your Password?" CssClass="yt-Form-Heading"></asp:Label>
                                        </div>
                                        <div style="margin-top: 10px;">
                                            <asp:Label ID="Label1" runat="server" Text="Enter the email address you used to sign up and we will email you your password:"
                                                CssClass="yt-Form-Label"></asp:Label>
                                        </div>
                                    </center>
                               
                                <%-- <div class="yt-SignUpLink">
                                    Not a member yet?
                                    <asp:LinkButton ID="popuplbtnSignup" CausesValidation="false" runat="server" OnClick="popuplbtnSignup_Click1">Sign up!</asp:LinkButton>
                                </div>--%>
                                <%-- <a href="javascript: void(0);" tabindex="4" class="yt-ForgetUserNamePassword-Link"
                                    onclick="ToggleElementDisplay('yt-ForgetUserNamePassword');">Did you forget your
                                    username or password?</a>--%>
                                <div id="yt-ForgetUserNamePassword" style="margin-top: 20px;">
                                    <div class="yt-Form-Field">
                                        <div class="yt-div-forgetpassword">
                                            <span class="yt-div-forget-asterisk">*</span> <span class="yt-Email-label">
                                                <label class="yt-Form-Field-label">
                                                    Email Address:</label></span>
                                        </div>
                                        <div class="yt-Text-left">
                                            <asp:TextBox ID="txtLoginEmail1" ValidationGroup="email" TabIndex="5" runat="server"
                                                CssClass="yt-Form-Field-Txtbox"></asp:TextBox>
                                            <asp:CustomValidator Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ID="CustomValidator1"
                                                ClientValidationFunction="validateEmail" runat="server" ValidationGroup="email">!</asp:CustomValidator>
                                        </div>
                                        <div class="yt-forget-sendbutton">
                                            <asp:LinkButton ID="popuplbtnSendemail" ValidationGroup="email" TabIndex="6" CssClass="yt-Button"
                                                runat="server" OnClick="popuplbtnSendemail_Click">Send password</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                 </div>
                                <br class="yt-clear-both" />
                            </div>
                        </fieldset>
                        <div class="yt-dashed-Border" id="dashBorder" runat="server">
                        </div>
                        <div class="yt-footer-popup">
                            <div class="yt-float-right">
                                <label class="yt-footer-label">
                                    Powered by:</label></div>
                            <br class="yt-clear-both" />
                            <div class="yt-footer-logoimg">
                                <img src="../assets/images/yourtribute_poweredby.gif" width="120px" height="25px" alt=" " />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
    
    <!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
    <script type="text/javascript" src="../assets/scripts/BrowserOrTabCloseHandler.js"></script>

</body>
 <script type="text/javascript">    
 <% if (ConfigurationManager.AppSettings["ApiKey"] != null) { %>

         window.fbAsyncInit = function () {
         FB.init({ appId: '<%= ConfigurationManager.AppSettings["ApiKey"].ToString() %>',
          status: true,
           cookie: true,
            xfbml: true,
            oauth:true});    
     }(function () {

        var e = document.createElement('script');

        e.type = 'text/javascript';

        e.src = document.location.protocol +

                    '//connect.facebook.net/en_US/all.js';

        e.async = true;

        document.getElementById('fb-root').appendChild(e);

    } ());



    function fb_logout() {
      FB.logout(function(){
      // we are logged out of facebook, erase my app's cookie

       var url = location.href;
     // append a timestamp to make sure the url seems different to the browser
       url = (url+(url.indexOf('?') > -1 ? '&' : '?')+(new Date()).getTime());
       window.location.href = url;
     });
    }
        
    function doAjaxLogin(){ 
        ModelPopup_LoginPopup.FacebookLogin();  
       window.parent.location.reload();
    }

    function update_user_is_connected() {
        var user_box = document.getElementById("lblUsername");
        if(user_box)
        user_box.innerHTML = "Username/Email:";
        $('connect_fb_login_instructions').style.display = 'block';
        
        if(showFbDialog){
            FB.Facebook.apiClient.users_hasAppPermission("email",function(has){
              if (has == 0) {
                 FB.Connect.showPermissionDialog("email", function(granted){
                   window.console && console.log && console.log(granted);//streampublish(share,true);
                   popupFbSignUp(); 
                 }); 
              }
              else {
                 window.console && console.log && console.log("Email permission was fiven earlier");//streampublish(share,true); 
                 popupFbSignUp(); 
              }
            });                    
         }
    }
    function update_user_is_not_connected() {
        var user_box = document.getElementById("lblUsername");
        if(user_box)
        user_box.innerHTML = "Username/Email:<span id='login_option'>OR</span><a href=\"#\" onclick=\"FB.Connect.requireSession(function(){window.location = window.location;}); return false;\" class=\"fbconnect_login_button FBConnectButton FBConnectButton_Small\"> <span id=\"RES_ID_fb_login_text\" class=\"FBConnectButton_Text\">Connect with Facebook</span> </a>";
        FB.XFBML.parse();
        buttonStyles();
    }  


   // FB.Facebook.get_sessionState().waitUntilReady(fb_session_ready);    
<% }  %>
             
 </script> 
</html>
