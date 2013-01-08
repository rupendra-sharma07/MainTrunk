<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Themes.aspx.cs" Inherits="Miscellaneous_Themes" %>

<%@ Register Src="../UserControl/YourTributeHeader.ascx" TagName="Header" TagPrefix="uc" %>
<%@ Register Src="../UserControl/FooterHome.ascx" TagName="Footer" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:fb="http://www.facebook.com/2008/fbml"
xml:lang="en" lang="en">
<head>
    <title>Your Tribute Free Online Memorial Websites | Available Designer Themes</title>
<%--    <meta name="description" content="Your Tribute has numerous designer themes available for you to personalize your online memorial website. Theme genres include nature, sky, flowers and more." />
    <meta name="keywords" content="online memorial, memorial theme, designer themes, online memorials, memorial tributes, your tribute" />--%> 
   <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Content-Language" content="en-ca" />
    <meta http-equiv="imagetoolbar" content="false" />
    <meta name="robots" content="index,follow" />
    <meta name="MSSmartTagsPreventParsing" content="true" />
    <%--<meta name="description" content="Create a personalized website for a wedding, baby, memorial or other special event. Your Tribute offers the best custom websites to celebrate your event. Make an online website free for 30 days with Your Tribute’s personal online website creation service."/>
    <meta name="keywords" content="Personal Website, Personal Websites, Personal Webpage"/>--%>
    <meta name="description" content=" Free Online Memorial Websites – With Your Tribute, you can create a free online memorial website for a departed loved one. Build a Your Tribute online memorial using a variety of suitable themes today." />
    <meta name="keywords" content="free online memorial website, free online memorial websites" />
    <!-- really basic, generic html class stylesheet -->
    <!-- These url's will work on Remote server. Comment the above urls -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/default.css" />
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/layouts/centered1024_21.css" />
    <link rel="stylesheet" type="text/css" media="print" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/print.css" />
    <link rel="stylesheet" type="text/css" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/ScreenLatest.css" />
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/medium_text.css"
        title="medium_text" />
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/large_text.css"
        title="large_text" />
    <link rel="Shortcut Icon" type="image/x-icon" href="<%=Session["APP_BASE_DOMAIN"]%>assets/images/favicon.ico" />

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/mootools-1.11.js"></script>

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/global.js"></script>

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/styleSwitcher.js"></script>

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/modalbox.js"></script>

    <script src="https://connect.facebook.net/en_US/all.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
     App_Domain = "<%=Session["APP_BASE_DOMAIN"]%>";   
        function openwindow(imgName)
     {         
	    window.open(App_Domain+"ModelPopup/ThemeFullSizeImage.aspx?ImageName="+imgName,"mywindow","status=0, width=450, height=443");
     }
    </script>

    <!--#include file="../analytics.asp"-->
    <%--12/30/2011 :Client Patch--%>
    <!-- custom -->

    <script type="text/javascript">
        var pkBaseURL = (("https:" == document.location.protocol) ? "https://e-dasher.com/analytics/" : "http://e-dasher.com/analytics/");
        document.write(unescape("%3Cscript src='" + pkBaseURL + "piwik.js' type='text/javascript'%3E%3C/script%3E"));
    </script>

    <script type="text/javascript">
        try {
            var piwikTracker = Piwik.getTracker(pkBaseURL + "piwik.php", 401);
            piwikTracker.trackPageView();
            piwikTracker.enableLinkTracking();
        } catch (err) { }
    </script>

    <noscript>
        <p>
            <img src="http://e-dasher.com/analytics/piwik.php?idsite=401" style="border: 0" alt="" /></p>
    </noscript>
    <!-- End custom Tracking Code -->
    <%--12/30/2011 :Client Patch till here--%>
</head>
<body>
    <form id="Form1" action="" runat="server">
    <div id="divShowModalPopup">
    </div>
    <div class="yt-Container yt-Tour">
        <uc:Header ID="ytHeader" Section="tribute" NavigationName="Examples" runat="server" />
        <div class="hack-clearBoth">
        </div>
        <div class="yt-Breadcrumbs">
            <a href="<%=Session["APP_BASE_DOMAIN"]%>">Home</a> <span class="selected">Themes</span>
        </div>
        <div class="yt-ContentContainer">
            <div class="yt-InnerContentContainer">
                <div class="yt-ContentPrimaryContainer">
                    <div class="yt-Panel-Primary-MT">
                        <h3 class="yt-BoldHeading OceanBlue-MT">
                            Themes
                        </h3>
                        <div class="tourIntro">
                            <p>
                                Your Tribute has a variety of designer themes available for you to personalize the
                                free online memorial website that you create. Most free online memorial websites
                                themes look inexpensive. At Your Tribute we believe that whether you create a free
                                memorial or premium memorial tribute, your loved one’s memorial deserves a premium
                                theme.
                            </p>
                            <br />
                            <p>
                                Theme genres include nature, sky, flowers, religion and much more. We regularly
                                add new themes. If you have a suggestion for a theme you would like to see on Your
                                Tribute, please contact us. We will be happy to discuss custom theme options with
                                you.
                            </p>
                            <br />
                            <p>
                                To preview a theme, click on an image below. When you are ready to get started,
                                click “get started” to select a package and create a memorial to permanently remember
                                your loved one.
                            </p>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="leftSideExamplesBlock">
                            <div class="examplesText">
                                <h3>
                                    Legacy
                                </h3>
                                <a href="javascript: openwindow('legacy_large.jpg')">
                                    <img alt="Legacy image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/legacy_thumb.jpg"
                                        class="examplesImage" /></a>
                            </div>
                        </div>
                        <div class="rightSideExamplesBlock">
                            <div class="examplesText">
                                <h3>
                                    Flowers
                                </h3>
                                <a href="javascript: openwindow('flowers_large.jpg')">
                                    <img alt="Flowers image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/flowers_thumb.jpg"
                                        class="examplesImage" /></a>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div id="div1" class="tryTributeBoxskyBlue-MT rounded">
                            <div class="rightTribute">
                                <p class="gettingstartedBlue">
                                    <a href="<%=Session["APP_BASE_DOMAIN"]%>pricing.aspx">Create Tribute</a>
                                </p>
                            </div>
                            <div class="leftTribute">
                                <h2>
                                    Create an online memorial for free!</h2>
                                <p>
                                    Get started in seconds, no credit card, no commitment.</p>
                            </div>
                        </div>
                        <div class="leftSideExamplesBlock">
                            <div class="examplesText">
                                <h3>
                                    Patriotic (American)
                                </h3>
                                <a href="javascript: openwindow('patriotic_large.jpg')">
                                    <img alt="Patriotic image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/patriotic_thumb.jpg"
                                        class="examplesImage" /></a>
                            </div>
                        </div>
                        <div class="rightSideExamplesBlock">
                            <div class="examplesText">
                                <h3>
                                    Religious
                                </h3>
                                <a href="javascript: openwindow('religious_large.jpg')">
                                    <img alt="Religious image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/religious_thumb.jpg"
                                        class="examplesImage" /></a>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="leftSideExamplesBlock">
                            <div class="examplesText">
                                <h3>
                                    Celestial
                                </h3>
                                <a href="javascript: openwindow('celestial_large.jpg')">
                                    <img alt="Celestial image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/celestial_thumb.jpg"
                                        class="examplesImage" /></a>
                            </div>
                        </div>
                        <div class="rightSideExamplesBlock">
                            <div class="examplesText">
                                <h3>
                                    Sky
                                </h3>
                                <a href="javascript: openwindow('sky_large.jpg')">
                                    <img alt="Sky image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/sky_theme.jpg"
                                        class="examplesImage" /></a>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="leftSideExamplesBlock">
                            <div class="examplesText">
                                <h3>
                                    Beach
                                </h3>
                                <a href="javascript: openwindow('beach_large.jpg')">
                                    <img alt="Beach image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/beach_thumb.jpg"
                                        class="examplesImage" /></a>
                            </div>
                        </div>
                        <div class="rightSideExamplesBlock">
                            <div class="examplesText">
                                <h3>
                                    Grass (Spring)
                                </h3>
                                <a href="javascript: openwindow('grass_large.jpg')">
                                    <img alt="Grass image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/grass_thumb.jpg"
                                        class="examplesImage" /></a>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div id="divTributeBoxBlue" class="tryTributeBoxskyBlue-MT rounded">
                            <div class="rightTribute">
                                <p class="gettingstartedBlue">
                                    <a href="<%=Session["APP_BASE_DOMAIN"]%>pricing.aspx">Create Tribute</a>
                                </p>
                            </div>
                            <div class="leftTribute">
                                <h2 style="margin-right: 10px;">
                                    Create an online memorial for free!</h2>
                                <p>
                                    Get started in seconds, no credit card, no commitment.</p>
                            </div>
                        </div>
                        <div class="leftSideExamplesBlock">
                            <div class="examplesText">
                                <h3>
                                    Wheat (Autumn)
                                </h3>
                                <a href="javascript: openwindow('wheat_large.jpg')">
                                    <img alt="Wheat image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/wheat_thumb.jpg"
                                        class="examplesImage" /></a>
                            </div>
                        </div>
                        <div class="rightSideExamplesBlock">
                            <div class="examplesText">
                                <h3>
                                    New Theme
                                </h3>
                                <a>
                                    <img alt="New Theme image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/comingsoon_thumb.jpg"
                                        class="examplesImage" /></a>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <% if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower().Equals("yourtribute"))
                           { %>
                        <div class="yt-PricingAddText">
                            <h1 class="yt-PricingHeading">
                                <div class="Purple-MT">
                                    Designer themes for Free Online Memorial Website.</div>
                            </h1>
                            <p class="textunderPricing NewTextColor-MT">
                                Designer themes at Your Tribute are available to personalize all free online memorial
                                websites. Each theme has been designed to beautifully and eloquently reflect your
                                loved ones personality. The most popular themes are flowers, sky and patriotic;
                                however, we have a wide selection of themes to choose from to personalize the memorial
                                website.</p><br />
                            <p class="textunderPricing NewTextColor-MT">
                                If you choose to create a free online memorial website or premium memorial, you
                                have a choice of all our premium themes. After you create a memorial website it
                                is easy to switch your theme any time. There is no fee to choose a new theme.</p>
                            <p class="textunderPricing NewTextColor-MT"><br />
                                Want to create your own designer theme? For a fee we can work with you to create
                                your own personalized theme for your memorial website. Contact us to discuss what
                                custom theme options are available. To view all free online memorial websites that
                                you can create for your loved one, click the “Pricing & Sign Up” link at the top
                                of the page.</p>
                        </div>
                        <%} %>
                        <div class="bigButtonsOnFeatures">
                            <div class="bottomRightButtons">
                                <a href="<%=Session["APP_BASE_DOMAIN"]%>features.aspx" class="bottomButtonFeatures">
                                    Features</a> <a href="<%=Session["APP_BASE_DOMAIN"]%>pricing.aspx" class="bottomButtonPricing">
                                        Pricing Options</a>
                            </div>
                        </div>
                    </div>
                    <!--Panel Primary ends here-->
                    <!--ADogra ends here-->
                    <!--yt-ContentPrimary-->
                </div>
                <!--Commented by ADogra-->
                <!--yt-ContentPrimaryContainer-->
                <!--<div class="yt-ContentSecondary">
                        <div class="yt-Panel yt-Panel-Tools">
                            <div class="yt-Panel-Body">
                                <h2>
                                    Your Tribute</h2>

                                <div class="yt-TourLinks">
                                    <ul>
                                        <li><a href="aboutus.aspx">About Your Tribute</a></li>
                                        <li class="yt-Selected"><a href="javascript:void(0);">Tour - Site Features</a></li>
                                        <li><a href="pricing.aspx">Tribute Pricing</a></li>
                                         <li><a href="affiliates.aspx">Affiliate Program</a></li>
                                        <li><a href="nonprofit.aspx">Non-Profit Support</a></li>
                                        <li><a href="advertise.aspx">Advertise</a></li> 
                                        <li><a href="contactyourtribute.aspx">Contact</a></li>

                                        <li><a href="termsofuse.aspx">Terms of Use</a></li>
                                        <li><a href="privacy.aspx">Privacy Policy</a></li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="yt-Panel">
                            <div class="yt-Panel-Body">

                                <h2>
                                    Types of Tributes...</h2>
                                <p>
                                    You can create a tribute to celebrate a significant event or a special someone.</p>

    <ul class="yt-TypeList">
        <li class="yt-TypeList-NewBaby"><a href="http://newbaby.yourtribute.com">New Baby</a></li>
        <li class="yt-TypeList-Wedding"><a href="http://wedding.yourtribute.com">Wedding</a></li>

        <li class="yt-TypeList-Anniversary"><a href="http://anniversary.yourtribute.com">Anniversary</a></li>
        <li class="yt-TypeList-Memorial"><a href="http://memorial.yourtribute.com">Memorial</a></li>
        <li class="yt-TypeList-Graduation"><a href="http://graduation.yourtribute.com">Graduation</a></li>
        <li class="yt-TypeList-Birthday"><a href="http://birthday.yourtribute.com">Birthday</a></li>
    </ul>

                                <div class="yt-Form-Buttons">
                                    <a href="http://www.yourtribute.com/create.aspx" class="yt-Button yt-ArrowButton">Create a new tribute!</a>

                                </div>
                            </div>
                        </div>
                    </div>-->
                <!--yt-ContentSecondary

                    <div class="hack-clearBoth">
                    </div>-->
                <!--Comment by ADogra ends here-->
                <div class="hack-clearBoth">
                </div>
                <!--ADogra starts here-->
                <div class="yt-ContentContainerImageOnFeatures">
                </div>
                <!--ADogra ends here-->
            </div>
            <!--yt-ContentContainerInner-->
        </div>
        <!--yt-ContentContainer-->
        <uc:Footer ID="Footer1" runat="server" />
        <!--yt-Footer-->
    </div>
    <!--yt-Container-->
    <div class="upgrade">
        <h2>
            Please Upgrade Your Browser</h2>
        <p>
            This site&#39;s design is only visible in a graphical browser that supports <a href="http://www.webstandards.org/upgrade/"
                title="The Web Standards Project's BROWSER UPGRADE initiative.">web standards</a>,
            but its content is accessible to any browser or Internet device.</p>
    </div>
    <!--yt-upgrade-->
    </form>

    <script type="text/javascript">
        executeBeforeLoad();
      <% if (ConfigurationManager.AppSettings["ApiKey"] != null) { %>  
        function update_user_is_connected() {
            header_user_is_connected();
            FB.XFBML.parse();
        }
        function update_user_is_not_connected() {
            header_user_is_not_connected();
            FB.XFBML.parse();
        }                  
//        FB.init('<%= ConfigurationManager.AppSettings["ApiKey"].ToString() %>', "/xd_receiver.htm",
//                 {"ifUserConnected": update_user_is_connected,
//                  "ifUserNotConnected": update_user_is_not_connected});
window.fbAsyncInit = function() {
    FB.init({
        appId  : '<%= ConfigurationManager.AppSettings["ApiKey"].ToString() %>',
        status : true, // check login status
        cookie : true, // enable cookies to allow the server to access the session
        xfbml  : true,  // parse XFBML
        //channelUrl  : 'http://www.yourdomain.com/channel.html', // Custom Channel URL
        oauth : true //enables OAuth 2.0
    });

    FB.getLoginStatus(function(response) {
        if (response.authResponse) update_user_is_connected();
        else update_user_is_not_connected();
    });

    // This will be triggered as soon as the user logs into Facebook (through your site)
    FB.Event.subscribe('auth.login', function(response) {
        update_user_is_connected();
    });
};

      <% } %>          
    </script>

</body>
</html>
