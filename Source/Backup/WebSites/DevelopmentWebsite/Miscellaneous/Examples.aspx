<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Examples.aspx.cs" Inherits="Miscellaneous_Examples" %>

<%@ Register Src="../UserControl/YourTributeHeader.ascx" TagName="Header" TagPrefix="uc" %>
<%@ Register Src="../UserControl/FooterHome.ascx" TagName="Footer" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:fb="http://www.facebook.com/2008/fbml"
xml:lang="en" lang="en">
<head>
    <title id="exampleTitle" runat="server">Examples - View examples of Your Tribute
 event websites</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Content-Language" content="en-ca" />
    <meta http-equiv="imagetoolbar" content="false" />
    <meta name="robots" content="index,follow" />
    <meta name="MSSmartTagsPreventParsing" content="true" />
    <meta name="description" content="Create a personalized website for a wedding, baby, memorial or other special event. Your Tribute offers the best custom websites to celebrate your event. Make an online website free for 30 days with Your Tribute’s personal online website creation service.">
    <meta name="keywords" content="Personal Website, Personal Websites, Personal Webpage">
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

    <script src="http://connect.facebook.net/en_US/all.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
     App_Domain = "<%=Session["APP_BASE_DOMAIN"]%>";
    </scrip>t
    <!--#include file="../analytics.asp"-->
</head>
<body>
    <form id="Form1" action="" runat="server">
    <div id="divShowModalPopup"></div> 
    <div class="yt-Container yt-Tour">
        <uc:Header ID="ytHeader" Section="tribute" NavigationName="Examples" runat="server" />
        <div class="hack-clearBoth">
        </div>
        <div class="yt-Breadcrumbs">
            <a href="<%=Session["APP_BASE_DOMAIN"]%>">Home</a> <span class="selected">Examples</span>
        </div>
        <div class="yt-ContentContainer">
            <div class="yt-InnerContentContainer">
                <div class="yt-ContentPrimaryContainer">
                    <div class="yt-Panel-Primary">
                        <h1 class="yt-BiggestHeading darkBlue">
                            Examples
                        </h1>
                        <div class="leftSideExamplesBlock">
                            <div class="examplesText">
                                <h3 class="pink">
                                    Wedding <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                </h3>
                                <p>
                                    View Jon & Mary’s Sample Wedding <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> to see how they used Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> to plan
                                    all of their wedding related events. See how they shared photos from before and after the wedding and see wedding photos uploaded by their friends and family!
                                </p>
                                <a href="http://wedding.yourmoments.com/jonandmary/" target="_blank">
                                    <img alt="story image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/example_Wedding.jpg" class="examplesImage" /></a>
                                <a href="http://wedding.yourmoments.com/jonandmary/" target="_blank" class="previewExample">
                                    <span class="pink">http://wedding.yourmoments.com/jonandmary/</span></a>
                            </div>
                        </div>
                        <div class="rightSideExamplesBlock">
                            <div class="examplesText">
                                <h3 class="yellow">
                                    New Baby <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                </h3>
                                <p>
                                    View Elizabeth Johnson’s sample New Baby <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> to see how Carolyn planned her
                                    baby shower and shared photos from before the birth. She then sent her baby anouncements
                                    online and shared photos of her new arrival with her friends and family.
                                </p>
                                <a href="http://newbaby.yourmoments.com/elizabethjohnson/" target="_blank">
                                    <img alt="guestbook image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/example_NewBaby.jpg" class="examplesImage" /></a>
                                <a href="http://newbaby.yourmoments.com/elizabethjohnson/" target="_blank" class="previewExample">
                                    <span class="yellow">http://newbaby.yourmoments.com/elizabethjohnson/</span></a>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="leftSideExamplesBlock">
                            <div class="examplesText">
                                <h3 class="purple">
                                    Memorial <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                </h3>
                                <p>
                                    View Evelyn Smith’s Sample Memorial <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> to see how her family and friends created
                                    a <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> to remember and honor her life. See how friends and family shared stories,
                                    uploaded photos and left personal messages in her online guestbook.
                                </p>
                                <a href="http://memorial.yourmoments.com/evelynmsmith/" target="_blank">
                                    <img alt="story image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/example_Memorial.jpg" class="examplesImage" /></a>
                                <a href="http://memorial.yourmoments.com/evelynmsmith/" target="_blank" class="previewExample pink">
                                    <span class="purple">http://memorial.yourmoments.com/evelynmsmith/</span></a>
                            </div>
                        </div>
                        <div class="rightSideExamplesBlock">
                            <div class="examplesText">
                                <h3 class="lightGreen">
                                    Anniversary <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                </h3>
                                <p>
                                    View Bill & June Stiles’ Sample Anniversary <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> to see how their son created
                                    the <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> to celebrate their milestone anniversary. He planned their party online,
                                    shared photos and got friends and family to leave personal messages in their online
                                    guestbook.
                                </p>
                                <a href="http://anniversary.yourmoments.com/billandjune/" target="_blank">
                                    <img alt="guestbook image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/example_Anniversary.jpg" class="examplesImage" /></a>
                                <a href="http://anniversary.yourmoments.com/billandjune/" target="_blank" class="previewExample">
                                    <span class="green">http://anniversary.yourmoments.com/billandjune/</span></a>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="leftSideExamplesBlock">
                            <div class="examplesText">
                                <h3 class="skyBlue">
                                    Graduation <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                </h3>
                                <p>
                                    View Kelly Smith’s Sample Graduation <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> to see how her mom created a <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                    to acknowledge her big accomplishment. She used the <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> to plan her grad party
                                    online and Kelly’s friends left her messages of congratulations in her online guestbook.
                                </p>
                                <a href="http://graduation.yourmoments.com/kellysmith/" target="_blank">
                                    <img alt="story image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/example_Graduation.jpg" class="examplesImage" /></a>
                                <a href="http://graduation.yourmoments.com/kellysmith/" target="_blank" class="previewExample">
                                    <span class="skyBlue">http://graduation.yourmoments.com/kellysmith/</span></a>
                            </div>
                        </div>
                        <div class="rightSideExamplesBlock">
                            <div class="examplesText">
                                <h3 class="orange">
                                    Birthday <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                </h3>
                                <p>
                                    View Jen’s Birthday <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> to see how she used Your <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> to plan her Sweet 16
                                    party. She sent online birthday invitations and tracked the RSVP’s. After the party,
                                    Jen and her friends uploaded their photos from the event to share with each other.
                                </p>
                                <a href="http://birthday.yourmoments.com/jensweet16/" target="_blank">
                                    <img alt="guestbook image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/example_Birthday.jpg" class="examplesImage" /></a>
                                <a href="http://birthday.yourmoments.com/jensweet16/" target="_blank" class="previewExample">
                                    <span class="orange">http://birthday.yourmoments.com/jensweet16/</span></a>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="bigButtonsOnFeatures">
                            <a href="<%=Session["APP_BASE_DOMAIN"]%>pricing.aspx" class="createTributeInSeconds">Create a <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%></a>
                            <div class="bottomRightButtons">
                                <a href="<%=Session["APP_BASE_DOMAIN"]%>features.aspx" class="bottomButtonFeatures">Features</a> <a href="<%=Session["APP_BASE_DOMAIN"]%>pricing.aspx" class="bottomButtonPricing">
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
                                    Your 
<%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
</h2>

                                <div class="yt-TourLinks">
                                    <ul>
                                        <li><a href="aboutus.aspx">About Your 
<%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
</a></li>
                                        <li class="yt-Selected"><a href="javascript:void(0);">Tour - Site Features</a></li>
                                        <li><a href="pricing.aspx"><%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> Pricing</a></li>
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
                                    Types of <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>s...</h2>
                                <p>
                                    You can create a <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> to celebrate a significant event or a special someone.</p>

    <ul class="yt-TypeList">
        <li class="yt-TypeList-NewBaby"><a href="http://newbaby.yourmoments.com">New Baby</a></li>
        <li class="yt-TypeList-Wedding"><a href="http://wedding.yourmoments.com">Wedding</a></li>

        <li class="yt-TypeList-Anniversary"><a href="http://anniversary.yourmoments.com">Anniversary</a></li>
        <li class="yt-TypeList-Memorial"><a href="http://memorial.yourmoments.com">Memorial</a></li>
        <li class="yt-TypeList-Graduation"><a href="http://graduation.yourmoments.com">Graduation</a></li>
        <li class="yt-TypeList-Birthday"><a href="http://birthday.yourmoments.com">Birthday</a></li>
    </ul>

                                <div class="yt-Form-Buttons">
                                    <a href="http://www.yourmoments.com/create.aspx" class="yt-Button yt-ArrowButton">Create a new <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>!</a>

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
