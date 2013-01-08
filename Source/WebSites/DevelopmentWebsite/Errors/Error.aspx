<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Errors_Error" %>


<%@ Register Src="../UserControl/YourTributeHeader.ascx" TagName="Header" TagPrefix="uc" %>
<%@ Register Src="../UserControl/FooterHome.ascx" TagName="Footer" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Error - Your
        <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%></title>
    <meta name="MSSmartTagsPreventParsing" content="true" />
    <!-- really basic, generic html class stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/default.css" />
    <!-- default grid layout stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/layouts/centered1024_1.css" />
    <!-- print-friendly stylesheet -->
    <link rel="stylesheet" type="text/css" media="print" href="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/print.css" />
    <!-- screen elements stylesheet should be here -->
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
        //Added by LHK -28 03 2012 - for tribute home page login button functionality.
        function LogOut() {
            var str = window.location.href;
            if ((str.indexOf("&session=logout")) > 0) {
                Users_log_in.UserLogOut();
                deleteAllCookies();
                fb_logout();
                window.location = str.replace("&session=logout", "");
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

</head>
<body>
    <form id="Form1" runat="server" action="">
    <div class="yt-Container yt-LoginForm yt-AnonymousUser">
        <uc:Header ID="ytHeader" Section="tribute" NavigationName="None" runat="server" />
        <!--yt-HeaderContainer-->
        <div class="hack-clearBoth">
        </div>
        <div class="yt-ContentContainer">
            <div class="yt-ContentContainerInner">
                <div id="divShowModalPopup">
                </div>
                <div class="yt-ContentPrimaryContainer YMHeightError" style="margin:30px 0 300px;">
                    <div>
                        <div class="yt-ContentPrimaryContainer">
                            <br>
                            <br>
                            <br>
                            <br>
                            <div class="yt-Error">
                                <h2>
                                    The page or tribute you were looking for could not be found.</h2>
                                <br>
                                <b>Please check that you entered the correct URL or click the Back button and try again.</b>
                                <br>
                                <br>
                                <h3>
                                    Still can't find what you were looking for? Try our <a href="../AdvancedSearch.aspx">
                                        advanced search</a> ...
                                </h3>
                            </div>
                            <br>
                            <br>
                        </div>
                        <!--yt-ContentPrimaryContainer-->
                        <!--yt-ContentSecondary-->
                        <div class="hack-clearBoth">
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="yt-ContentContainerImage bgImageBUser">
                        </div>
                    </div>
                </div>
                <!--yt-ContentPrimaryContainer-->
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
</body>
</html>
