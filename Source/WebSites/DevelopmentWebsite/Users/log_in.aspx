<%@ Page Language="C#" AutoEventWireup="true" CodeFile="log_in.aspx.cs" Inherits="Users_log_in"
    EnableEventValidation="false" ValidateRequest="false" Title="Log in" %>

<%@ Register Src="../UserControl/YourTributeHeader.ascx" TagName="Header" TagPrefix="uc" %>
<%@ Register Src="../UserControl/FooterHome.ascx" TagName="Footer" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns:fb="https://www.facebook.com/2008/fbml">
<head runat="server">
    <title>Log In - Your
        <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%></title>
    <meta name="description" content=" Create a free account with Your Tribute to add content to other user’s memorials or create your own free obituary. " />
    <meta name="keywords" content="online memorial, memorial tribute, online obituary, free obituary, memorial website, permanent memorial, your tribute" />
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Content-Language" content="en-ca" />
    <meta http-equiv="imagetoolbar" content="false" />
    <meta name="robots" content="index,follow" />
    <meta http-equiv="Expires" content="0" />
    <meta http-equiv="Cache-Control" content="no-store" />
    <meta http-equiv="Cache-Control" content="no-cache" />
    <meta http-equiv="Pragma" content="no-cache" />
    <meta name="MSSmartTagsPreventParsing" content="true" />
    <!-- really basic, generic html class stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/default.css" />
    <!-- default grid layout stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/layouts/centered1024_1.css" />
    <!-- print-friendly stylesheet -->
    <link rel="stylesheet" type="text/css" media="print" href="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/print.css" />
    <!-- screen elements stylesheet should be here -->
    <!--[if !IE 7]>
     <!-->
    <link rel="stylesheet" type="text/css" href="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/ScreenLatest.css" />
    <!--<![endif]-->
    <link rel="stylesheet" type="text/css" href="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/PopUp.css" />
    <!--[if IE 7]>
        <link rel="stylesheet" type="text/css" href="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/ScreenLatestIE7.css" />
    <![endif]-->
    <!-- larger text stylesheets -->
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/medium_text.css"
        title="medium_text" />
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/large_text.css"
        title="large_text" />
    <!-- Website Favorite Icon -->
    <link rel="Shortcut Icon" type="image/x-icon" href="../assets/images/favicon.ico" />
    <%--COMMDIFFRES: (working fine)- path?--%>

    <script type="text/javascript" src="../assets/scripts/mootools-1.11.js"></script>

    <script type="text/javascript" src="../assets/scripts/global.js"></script>

    <script type="text/javascript" src="../assets/scripts/styleSwitcher.js"></script>

    <script type="text/javascript" src="../Common/JavaScript/Common.js"></script>
    
    <!--<script src="https://connect.facebook.net/en_US/all.js" type="text/javascript"></script>-->

    <script src="https://connect.facebook.net/en_US/all.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">

        function HideIndecator() {
            var error1 = document.getElementById('<%= errorUserName.ClientID %>');
            var error2 = document.getElementById('<%= errorPwd.ClientID %>');
            if (error1) {
                error1.style.visibility = 'hidden';
            }
            if (error2) {
                error2.style.visibility = 'hidden';
            }
            Hideheader();
        }

        function Hideheader() {
            var Notice = document.getElementById('<%= Notice.ClientID %>');
            if (Notice != null) {
                Notice.innerHtml = '';
                Notice.style.visibility = 'hidden';
            }
        }
        function checkUserName() {
            if (!window.document.URL.search("localhost")) {
                if ($('txtLoginUsername')) {
                    var txtVal = $('txtLoginUsername').value;
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
                else return true;
            }
            else return true;
        }

        function CheckUsernameLength(UserName) {
            var bol = true;
            if ((UserName.length >= 4) && (UserName.length <= 16)) {
                bol = true;
            }
            else {
                bol = false;
            }
            return bol;
        }

        function CheckPasswordLength(Password) {

            var bol = true;
            if (Password.length != 0) {
                if ((Password.length >= 6) && (Password.length <= 50))
                    bol = true;
                else
                    bol = false;
            }
            else {
                bol = true;
            }
            return bol;
        }

        function ymChanges() {
        LogOut();
            if ('<%= ConfigurationManager.AppSettings["ApplicationType"].ToString() %>' == 'yourmoments') {
                if (document.getElementById('LoginBtnDiv') != null) {
                    document.getElementById('LoginBtnDiv').className = "yt-Form-Buttons";
                }
                if (document.getElementById('<%=divlbtn.ClientID %>') != null) {
                    document.getElementById('<%=divlbtn.ClientID %>').className = "yt-Form-Submit";
                }
                if (document.getElementById('<%=lbtnLogin.ClientID %>') != null) {
                    document.getElementById('<%=lbtnLogin.ClientID %>').className = "yt-Button yt-ArrowButton";
                    document.getElementById('<%=lbtnLogin.ClientID %>').innerHTML = "Log In";
                }
            }
        }	
        //Added by LHK -28 03 2012 - for tribute home page login button functionality.
    function LogOut() {
    var str = window.location.href;
        if((str.indexOf("&session=logout")) > 0)
        {
            Users_log_in.UserLogOut();
            deleteAllCookies();
            fb_logout();
            window.location=str.replace("&session=logout", "");
            //  FB.logout();
            //        window.location.reload();
        }

    }
    function deleteAllCookies() {
        var cookies = document.cookie.split(";");
        for (var i = 0; i < cookies.length; i++) {
            var cookie = cookies[i];
            var eqPos = cookie.indexOf("=");
            var name = eqPos > -1 ? cookie.substr(0, eqPos) : cookie;
            document.cookie = name + "=;expires=Thu, 01 Jan 1970 00:00:00 GMT";
        }

    }  
    </script>

    <!--#include file="../analytics.asp"-->
</head>
<body onload="javascript:ymChanges();">
    <form runat="server" action="">
    <div id="fb-root">
    </div>
    <div class="yt-Container yt-LoginForm yt-AnonymousUser">
        <uc:Header ID="ytHeader" Section="tribute" NavigationName="None" runat="server" />
        <!--yt-HeaderContainer-->
        <div class="hack-clearBoth">
        </div>
        <div class="yt-ContentContainer">
            <div class="yt-ContentContainerInner">
                <div id="divShowModalPopup">
                </div>
                <div class="yt-ContentPrimaryContainer">
                    <div class="yt-ContentPrimary">
                        <div>
                            <div id="Notice" runat="server" visible="false" class="yt-Notice">
                            </div>
                            <asp:Panel ID="Panel3" runat="server" Width="656px">
                                <asp:ValidationSummary CssClass="yt-Error" ID="PortalValidationSummary" runat="server"
                                    HeaderText=" <h2>Oops - there was a problem with your login.</h2>                                                             <h3>Please correct the errors below:</h3>"
                                    ForeColor="Black" Width="622px" />
                            </asp:Panel>
                            <p id="lblMessage" runat="server" visible="false" class="text_center">
                                <br />
                                Login here to connect your facebook login to your account
                            </p>
                            <br />
                            <br />
                        </div>
                        <div class="yt-Panel-Primary">
                            <h2>
                                Already a Member?</h2>
                            <fieldset class="yt-Form">
                                <div id="yt-LoginFormContainer">
                                    <% if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower().Equals("yourtribute"))
                                       { %>
                                    <div class="adSpacer15">
                                    </div>
                                    <p id="connect_fb_login_instructions">
                                        In order to create and contribute, you first need to login to Your Tribute.</p>
                                    <p>
                                        Login to Your Tribute or connect with Facebook:</p>
                                    <div class="adSpacer20">
                                        <%--Added to remove warning on hitting enter in safari browser.--%>
                                        <%--<asp:Button ID="btnLogin" runat="server" style="visibility:hidden; height:0px; width:0px;" OnClick="lbtnLogin_Click"
                                                TabIndex="3" ValidationGroup="LoginCheck"></asp:Button>--%>
                                    </div>
                                    <%} %>
                                    <%else
                                        { %>
                                    <p id="P1">
                                        Login here to connect your facebook login to your account</p>
                                    <%} %>
                                    <div class="yt-Form-Field">
                                        <asp:Label ID="lblUsername" runat="server" Text="Username/Email:" AssociatedControlID="txtLoginUsername"></asp:Label>
                                        <asp:TextBox ID="txtLoginUsername" CssClass="yt-Form-Input" MaxLength="250" runat="server"
                                            ValidationGroup="LoginCheck" TabIndex="1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Username/Email is a required field. "
                                            ControlToValidate="txtLoginUsername" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"
                                            Width="1px" ValidationGroup="LoginCheck">!</asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="cvLoginUsername" ErrorMessage="Invalid Username." Text="!"
                                            ClientValidationFunction="checkUserName" runat="server" Font-Bold="True" Font-Size="Medium"
                                            ForeColor="White" Visible="true" ValidateEmptyText="True" ValidationGroup="LoginCheck"></asp:CustomValidator>
                                        <span style="color: #FF8000; font-size: Medium; font-weight: bold;" id="errorUserName"
                                            runat="server" visible="false">!</span>
                                    </div>
                                    <div class="yt-Form-Field">
                                        <asp:Label ID="lblPassword" runat="server" Text="Password:" AssociatedControlID="txtLoginPassword">                                           
                                        </asp:Label>
                                        <asp:TextBox ID="txtLoginPassword" CssClass="yt-Form-Input yt-Form-Input-Password"
                                            TextMode="Password" MaxLength="50" runat="server" TabIndex="2" ValidationGroup="LoginCheck"></asp:TextBox>
                                        <span id="errorPwd" runat="server" style="color: #FF8000; font-size: Medium; font-weight: bold;"
                                            visible="false">!</span>
                                        <asp:RequiredFieldValidator ID="rfvLoginPassword" runat="server" ErrorMessage="Password is a required field. "
                                            ControlToValidate="txtLoginPassword" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"
                                            Width="1px" ValidationGroup="LoginCheck">!</asp:RequiredFieldValidator>
                                    </div>
                                    <div id="LoginBtnDiv" class="Login-Button">
                                        <% if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower().Equals("yourtribute"))
                                           { %>
                                        <div class="Forget-Password">
                                            <%--<a onclick="UserLoginModalpopupFromLoginPage(location.href,document.title);" href="javascript: void(0);">
                                                Forget your password? Click here.</a>--%>
                                            <a onclick="UserLoginModalpopupFromSubDomain(location.href,document.title);" href="javascript: void(0);">
                                                Forget your password? Click here.</a>
                                        </div>
                                        <%} %>
                                        <div id="divlbtn" runat="server" class="Login-btn">
                                            <p>
                                                <asp:LinkButton ID="lbtnLogin" CssClass="BlueLoginButton" runat="server" ValidationGroup="LoginCheck"
                                                    OnClick="lbtnLogin_Click" TabIndex="3"></asp:LinkButton>
                                            </p>
                                        </div>
                                    </div>
                                    <% if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower().Equals("yourtribute"))
                                       { %>
                                    <div class="hack-clearBoth">
                                    </div>
                                    <%} %>
                                    <%else
                                        { %>
                                    <p>
                                        <a href="#" onclick="UserLoginModalpopupFromLoginPage(location.href,document.title);">
                                            Did you forget your password?</a>
                                    </p>
                                    <%} %>
                                </div>
                            </fieldset>
                        </div>
                        <div id="signUpYt" runat="server" class="yt-Panel-Primary yt-Panel-Extra">
                            <h2 class="PurpleBG-MT">
                                Not a Member Yet?</h2>
                            <div class="adSpacer15">
                            </div>
                            <p>
                                Sign up to become a member of Your Tribute. With your account you can add content
                                to other user’s memorials or create an online memorial of your own.
                            </p>
                            <ul>
                                <div class="adSpacer15">
                                </div>
                                <div class="Bulletedli">
                                </div>
                                <div class="BulletText">
                                    Fast one-step registration.</div>
                                <div class="adSpacer15">
                                </div>
                                <div class="Bulletedli">
                                </div>
                                <div class="BulletText">
                                    No credit card required.</div>
                                <div class="adSpacer15">
                                </div>
                                <div class="Bulletedli">
                                </div>
                                <div class="BulletText">
                                    Safe and secure.</div>
                            </ul>
                            <div class="Signup-Button">
                                <asp:LinkButton ID="LinkButton1" CssClass="BluesignUpButton" CausesValidation="False"
                                    OnClick="lbtnSignup_Click" runat="server"></asp:LinkButton>
                            </div>
                            <div class="yt-Form-Buttons" id="ShortFacebookSignUp">
                                <div class="yt-Form-Submit">
                                    <asp:LinkButton ID="FacebookSignUp" CssClass="yt-Button yt-ArrowButton" CausesValidation="False"
                                        OnClick="lbtnFacebookSignup_Click" runat="server">Just create empty Account with Facebook credentials</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div id="signUpYm" runat="server" class="yt-Panel-Primary yt-Panel-Extra" visible="false">
                            <h2>
                                Not a Member Yet?</h2>
                            <p>
                                Sign up to become a member of Your Tribute, and collaborate on existing tributes
                                or create your own.
                            </p>
                            <ul>
                                <li>It’s fast! </li>
                                <li>It’s free!</li>
                            </ul>
                            <div class="yt-Form-Buttons">
                                <div class="yt-Form-Submit">
                                    <asp:LinkButton ID="LinkButton2" CssClass="yt-Button yt-ArrowButton" CausesValidation="False"
                                        OnClick="lbtnSignup_Click" runat="server">Sign  up</asp:LinkButton>
                                </div>
                            </div>
                            <p>
                                <a href="features.aspx">Take a tour.</a></p>
                            <div class="yt-Form-Buttons" id="ShortFacebookSignUpYm">
                                <div class="yt-Form-Submit">
                                    <asp:LinkButton ID="LinkButton3" CssClass="yt-Button yt-ArrowButton" CausesValidation="False"
                                        OnClick="lbtnFacebookSignup_Click" runat="server">Just create empty Account with Facebook credentials</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div id="ytExamplesBlock" runat="server">
                            <div class="adSpacer35">
                                &nbsp</div>
                            <div class="leftSideExamplesBlock">
                                <div class="examplesText">
                                    <h3 class="Purple-MT">
                                        Your data is safe and secure.
                                    </h3>
                                    <p class="NewTextColor-MT">
                                        Our servers are protected in a high security building with 24-hour surveillance,
                                        biometric locks and advanced fire protection. Our software is updated regularly
                                        with the latest security patches.
                                    </p>
                                </div>
                            </div>
                            <div class="rightSideExamplesBlock">
                                <div class="examplesText LeftMargin15">
                                    <h3 class="Purple-MT">
                                        Your privacy is important.
                                    </h3>
                                    <p class="NewTextColor-MT">
                                        We will never provide your name, email, phone number, credit card or any personal
                                        information to 3rd parties without your written consent. Learn more by reading our
                                        <a href="<%=Session["APP_BASE_DOMAIN"]%>privacy.aspx">privacy policy</a>.
                                    </p>
                                </div>
                            </div>
                            <div class="hack-clearBoth">
                            </div>
                            <div class="leftSideExamplesBlock">
                                <div class="examplesText">
                                    <h3 class="Purple-MT">
                                        Your data is backed up daily.
                                    </h3>
                                    <p class="NewTextColor-MT">
                                        All of the information (photos, videos, text, etc) you have stored with us is backed
                                        up daily. Our backups are stored in multiple locations for additional redundancy
                                        and security.
                                    </p>
                                </div>
                            </div>
                            <div class="rightSideExamplesBlock">
                                <div class="examplesText LeftMargin15">
                                    <h3 class="Purple-MT">
                                        Your account is free. Forever.
                                    </h3>
                                    <p class="NewTextColor-MT">
                                        Create a free account to add content to other user’s memorials or create your own
                                        free obituary. Your data will be stored with us permanently; or, until you remove
                                        it off our website.
                                    </p>
                                </div>
                            </div>
                            <div class="hack-clearBoth">
                            </div>
                        </div>
                    </div>
                    <!--yt-ContentPrimary-->
                </div>
                <!--yt-ContentPrimaryContainer-->
                <div class="hack-clearBoth">
                </div>
                <div class="yt-ContentContainerImage">
                </div>
            </div>
            <!--yt-ContentContainerInner-->
        </div>
        <!--yt-ContentContainer-->
        <!--<div class="yt-Footer">-->
        <uc1:Footer ID="Footer1" runat="server" />
        <!-- </div>-->
        <!--yt-Footer-->
    </div>
    <!--yt-Container-->
    <div class="upgrade">
        <h2>
            Please Upgrade Your Browser</h2>
        <p>
            This site's design is only visible in a graphical browser that supports <a href="http://www.webstandards.org/upgrade/"
                title="The Web Standards Project's BROWSER UPGRADE initiative.">web standards</a>,
            but its content is accessible to any browser or Internet device.</p>
    </div>
    <!--yt-upgrade-->
    </form>
    
    <!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
    <script type="text/javascript" src="../assets/scripts/BrowserOrTabCloseHandler.js"></script>

</body>

<script type="text/javascript" src="../assets/scripts/modalbox.js"></script>

<script type="text/javascript">    
 <% if (ConfigurationManager.AppSettings["ApiKey"] != null) { %>
    
    window.fbAsyncInit = function () {
         FB.init({ appId: '<%= ConfigurationManager.AppSettings["ApiKey"].ToString() %>', status: true, cookie: true, xfbml: true,oauth:true});
       
         FB.getLoginStatus(function(response) {
        if (response.authResponse) update_user_is_connected();
        else update_user_is_not_connected();
    });
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
       var url = location.href;
       url = (url+(url.indexOf('?') > -1 ? '&' : '?')+(new Date()).getTime());
       window.location.href = url;
     });
    }
    
    var doFbLogout = <asp:Literal id="doFbLogout" Text="false" runat="server" />;
    var showFbDialog = <asp:Literal id="showFbDialog" Text="false" runat="server" />;
    
    function update_user_is_connected() {
        var user_box = document.getElementById("lblUsername");
        user_box.innerHTML = "Username/Email:";
         var x= document.getElementById("connect_fb_login_instructions");
//        $('connect_fb_login_instructions').style.display = 'block';       
        if(showFbDialog){    
        FB.login(function (response) {
                    if (response.authResponse) {    
                        // user authorized
                        popupFbSignUp(); 
                    } else {
                        // user cancelled
                    }
                }, { scope: 'email' });                      
         }
         else 
         {
               window.location.reload();          
         }
    }
    function update_user_is_not_connected() {
       if ('<%= ConfigurationManager.AppSettings["ApplicationType"].ToString() %>' == 'yourmoments') {
    var user_box = document.getElementById("lblUsername");
        if(user_box)
        user_box.innerHTML = "Username/Email:<span id='login_option'>OR</span><fb:login-button onlogin=\"doAjaxLogin();\" length=\"small\" scope=\"email,user_about_me,user_location\" ><fb:intl>Connect with Facebook</fb:intl></fb:login-button>";//<a href=\"#\" onclick=\"FB.getLoginStatus(function(){window.location = window.location;}); return false;\" class=\"fbconnect_login_button FBConnectButton FBConnectButton_Small\"> <span id=\"RES_ID_fb_login_text\" class=\"FBConnectButton_Text\">Connect with Facebook</span> </a>";
     }
    else
    {
     var user_box = document.getElementById("lblPassword");
        if(user_box)
        user_box.innerHTML = "Password:<span id='login' >or</span><fb:login-button onlogin=\"doAjaxLogin();\" length=\"small\" scope=\"email,user_about_me,user_location\" ><fb:intl>Connect</fb:intl></fb:login-button>";
    }      
          FB.XFBML.parse();
    }
    
    function doFacebookSignup(){
      if ($('yt-fb-signup')) $('yt-fb-signup').remove();
      new Element('img').setProperties({"src":'assets/images/ajax-loader.gif', "class":'loader_indicator'}).injectInside($('mb_contents'));            
      __doPostBack('FacebookSignUp','');    
    }
    
    function popupFbSignUp() {
        var closeFacebookSignUp = function() {
            if ($('mb_Title')) $('mb_Title').remove();
            if ($('yt-fb-signup')) $('yt-fb-signup').remove();
        }
        var indicator_img = new Image(48,48); 
        indicator_img.src="assets/images/ajax-loader.gif"; 
            
        
         FB.api('/me', function (response) {
                    var query = FB.Data.query('select name from user where uid={0}', response.id);
                    query.wait(function (rows) {     
            customModalBox.htmlBox('', '<div id="yt-fb-signup">You will now be connected to Your Tribute<br/>through Facebook as ' +
        "<a href='http://www.facebook.com/profile.php?id=" + response.id +
        "' target='_blank'>" + rows[0].name + "</a>" +
        '<a href="javascript:doFacebookSignup()" class="btn_fb_signup yt-Button" id="btn_fb_create">CONTINUE</a>'+        
        'Do you already have a Your Tribute Account?'+
        '<a href="#" class="btn_fb_signup" id="btn_fb_associate">Click here to connect it to Facebook</a></div>',
        'FacebookSignUp');

            //buttonStyles();
                        
            $('mb_contents').addClass('yt-Panel-Primary');
            new Element('h2').setHTML('Facebook Connected').setProperty('id', 'mb_Title').injectTop($('mb_contents'));
           
            $('mb_close_link').addEvent('click', closeFacebookSignUp);
            $('btn_fb_associate').addEvent('click', function() {
                $('mb_close_link').fireEvent('click');
                customModalBox.close();
            });
        });   
   });
   };     
<% }  %>
             
</script>

</html>
