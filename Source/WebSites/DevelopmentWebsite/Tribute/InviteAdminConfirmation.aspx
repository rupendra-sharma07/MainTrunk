<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InviteAdminConfirmation.aspx.cs"
    Inherits="Tribute_InviteAdminConfirmation" %>

<%@ Register Src="../UserControl/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>

<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head>
    <title>Your Tribute - Login</title>
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
    <link rel="stylesheet" type="text/css" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/screen.css" />
    <!-- larger text stylesheets -->
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/medium_text.css"
        title="medium_text" />
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/large_text.css"
        title="large_text" />
    <!-- Website Favorite Icon -->  
    <link rel="Shortcut Icon" type="image/x-icon" href="../assets/images/favicon.ico" />
    
    <!-- JS libraries -->

    <script type="text/javascript" src="../assets/scripts/mootools-1.11.js"></script>

    <script type="text/javascript" src="../assets/scripts/global.js"></script>

    <script type="text/javascript" src="../assets/scripts/styleSwitcher.js"></script>

    <script type="text/javascript" src="../assets/scripts/modalbox.js"></script>

    <!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
    <script type="text/javascript" src="../assets/scripts/BrowserOrTabCloseHandler.js"></script>

    <script type="text/javascript">
    
    /* NOTE: may want to move this to an external .js */
    
    InitForm = function() {
        $$('.availabilityNotice').each( function(a) {
		    a.innerHTML = '';
		    a.className = 'availabilityNotice';
	    });
    }
    
   
	
    </script>
    <!--#include file="../analytics.asp"-->
</head>
<body>
    <form action="">
    <div id="divShowModalPopup"></div>
        <div class="yt-Container yt-AnonymousUser yt-InviteAdmin">
            <div class="yt-HeaderContainer">
                <div class="yt-Header">
                    <a href="home.aspx" title="Return to Your Tribute Home Page" class="yt-Logo">
                    </a>
                    <div class="yt-HeaderControls">
                        <div class="yt-NavHeader">
                            <div class="yt-UserInfo">
                                <a href="javascript: void(0);" onclick="doModalLogin();">Log in</a>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="yt-Tools">
                            <div id="yt-TypeSizeControl" class="yt-TypeSizeControl">
                                <span class="floatLeft">Text Size:&#160;</span> <a href="javascript:void(0);" class="large"
                                    title="Large Text">a</a> <a href="javascript:void(0);" class="medium" title="Medium Text">
                                        a</a> <a href="javascript:void(0);" class="small" title="Small Text">a</a>
                            </div>
                        </div>
                        <!--yt-Tools-->
                    </div>
                    <!--yt-HeaderControls-->
                </div>
                <!--yt-Header-->
            </div>
            <!--yt-HeaderContainer-->
            <div class="hack-clearBoth">
            </div>
            <div class="yt-ContentContainer">
                <div class="yt-ContentContainerInner">
                    <div class="yt-ContentPrimaryContainer">
                        <div class="yt-ContentPrimary">
                            <div class="yt-Notice">
                                <h2>
                                    You have been selected to be an administrator of the Tribute: <a href="javascript:void(0);">Tribute Name</a></h2>
                                <h3>
                                    A tribute administrator has complete control over the tribute. Administrators can
                                    modify the tribute settings (theme, privacy, tribute details) and can manage content
                                    (delete photos, guestbook entries, comments)</h3>
                            </div>
                            <div class="yt-Panel-Primary">
                                <h2>
                                    Confirmation</h2>
                                <fieldset class="yt-Form">
                                    <div id="yt-LoginFormContainer">
                                        <h3>
                                            Would you like to become an administrator?</h3>
                                        <div class="yt-Form-Field-Radio">
                                            <input type="radio" id="rdoConfirmationAccept" value="Accept" name="rdoConfirmation" />
                                            <label for="rdoConfirmationAccept">
                                                Accept</label>
                                        </div>
                                        <div class="yt-Form-Field-Radio">
                                            <input type="radio" id="rdoConfirmationDecline" value="Decline" name="rdoConfirmation" />
                                            <label for="rdoConfirmationDecline">
                                                Decline</label>
                                        </div>
                                        <div class="yt-Form-Buttons">
                                            <div class="yt-Form-Submit">
                                                <a href="javascript: void(0);" class="yt-Button yt-ArrowButton">Submit</a>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                        <!--yt-ContentPrimary-->
                    </div>
                    <!--yt-ContentPrimaryContainer-->
                    <div class="hack-clearBoth">
                    </div>
                    <div class="yt-ContentContainerImage bgImageBUser">
                    </div>
                </div>
                <!--yt-ContentContainerInner-->
            </div>
            <!--yt-ContentContainer-->
            <div >
                <uc1:Footer ID="Footer1" runat="server" />
            
            </div>
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

    <script type="text/javascript">
executeBeforeLoad();
    </script>

</body>
</html>
