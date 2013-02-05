<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Tribute_RootHomepage"
    EnableViewStateMac="true" %>

<%@ Register Src="~/UserControl/YourTributeHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/FooterHome.ascx" TagName="Footer" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:fb="http://www.facebook.com/2008/fbml"
xml:lang="en" lang="en">
<head>
    <title id="HomeTitle" runat="server" >Your Tribute - Free Online Obituaries & Premium Memorial Websites</title>
    <!--
		
author: Mark Bice
last modified: December 02, 2007

	-->
    <%--<meta name="description" content="Permanent Online Memorials and Free Obituaries at Your Tribute. Create an online obituary or personalized memorial tribute website for a loved one in minutes." />
    <meta name="keywords" content="online memorials, free obituaries, memorial tributes, memorial tribute website, online obituary, permanent memorial, your tribute" />--%>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Content-Language" content="en-ca" />
    <meta http-equiv="imagetoolbar" content="false" />
    <meta name="robots" content="index,follow" />
    <meta name="MSSmartTagsPreventParsing" content="true" />
    <%--<meta name="description" content="Create a personalized website for a wedding, baby, memorial or other special event. Your Tribute offers the best custom websites to celebrate your event." />
    <meta name="keywords" content="Event Website, Wedding Website, Baby Website, Memorial Website" />--%>
    <meta name="verify-v1" content="t/eO7u+WzYZ4+WwZSJgRfJihKZMi/S9+10TXbCDiilk=" />
    <meta name="y_key" content="d87f2529b7dae83b" />
    <meta name="description" content="Create Free Online Obituaries or Premium Memorial Websites for your loved ones with Your Tribute. Share Condolences, Stories, Memorials, Photos and Videos." />
    <meta name="keywords" content="online obituaries, free online obituaries, memorial websites, premium memorial websites, your tribute, yourtribute.com" />
    <!-- really basic, generic html class stylesheet -->
    <!-- These url's will work on Remote server. Comment the above urls -->
    <%--  <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/default.css" />--%>
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/layouts/centered1024_1.css" />
    <%--    <link rel="stylesheet" type="text/css" media="print" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/print.css" />--%>
    <link rel="stylesheet" type="text/css" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/ScreenLatest.css" />
    <!--[if !IE 7]>
     
    <link rel="stylesheet" type="text/css" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/ScreenLatest.css" />
   <![endif]-->
    <!--[if IE 7]>
        <link rel="stylesheet" type="text/css" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/ScreenLatestIE7.css" />
    <![endif]-->
    <%--    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/medium_text.css"
        title="medium_text" />
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/large_text.css"
        title="large_text" />--%>
    <link rel="Shortcut Icon" type="image/x-icon" href="<%=Session["APP_BASE_DOMAIN"]%>assets/images/favicon.ico" />

    <script src="https://connect.facebook.net/en_US/all.js" type="text/javascript"></script>

    <%--LHK: commented javascript not in use.--%>

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/jquery-1.4.2.min.js"></script>

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/Slideshow.js"></script>

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/mootools-1.11.js"></script>

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/global.js"></script>

    <!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/BrowserOrTabCloseHandler.js"></script>

    <%--<script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/styleSwitcher.js"></script>

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/modalbox.js"></script>--%>
    <!--script links Added by ADogra-->
    <!--ADogra ends here-->

    <script type="text/javascript">
        //    function parentSlideshow () {
        //	   var so = new SWFObject('<%=Session["APP_PATH"]%>assets/slideshows/home/parentHomeSlideShow.swf', 'hero', '374', '247', '7', '#ffffff');	
        //		so.addVariable("xmlfile", EncodeUrl('<%=Session["APP_PATH"]%>assets/slideshows/home/images.xml'));
        //	   so.addParam("wmode", "transparent");
        //	   so.write("yt-flashcontent");
        //	}

        //        function openSampleWindow(imgName) {
        //            window.open('http://memorial.yourtribute.com/evelynmsmith/', "Samplewindow", "status=0, width=1024, height=768,left=0,top=0");
        //        }
    </script>
<!--#include file="~/analytics.asp"-->

</head>
<body>
    <form id="Form1" action="" runat="server">
    <div id="divShowModalPopup">
    </div>
    <div class="SpecialAnnouncement" id="YTAnnouncement" runat="server">
        <b>Special Announcement:</b> Are you looking for
        <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>s
        for a Wedding, Baby, Graduation, Anniversary or Birthday? <b><a style="color: White;"
            href="<%="http://blog.your"+ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToLower()+".com" %>"
            target="_blank">Read our blog for more info...</a></b>
    </div>
    <div class="yt-Container yt-Home yt-AnonymousUser">
        <uc1:Header ID="Header" Section="home" NavigationName="Home" runat="server" />
        <div class="hack-clearBoth">
        </div>
        <div class="yt-ContentContainer">
            <div class="yt-ContentContainerInner">
                <div class="yt-ContentPrimaryContainer">
                    <div class="yt-ContentPrimary">
                        <div id="YMSlider" runat="server" class="yt-Hero">
                            <!--<h2>
                                    Celebrate a significant event or a special someone.</h2>
                                <h3>
                                    Share stories, photos, videos, and virtual gifts, and receive updates and event
                                    information</h3>-->
                            <% if (TributesPortal.Utilities.WebConfig.ApplicationMode.Equals("local"))
                               { %>
                            <ul class="yt-NavHome">
                                <li class="yt-NewBaby yt-NavHome-Left"><a href="<%=Session["APP_PATH"]%>channelhomepage.aspx?Type=New Baby&Theme='BabyDefault'"
                                    class="slideTab" id="slideTab-2">New Baby</a></li>
                                <li class="yt-Birthday yt-NavHome-Right"><a href="<%=Session["APP_PATH"]%>channelhomepage.aspx?Type=Birthday&Theme='BirthdayDefault'"
                                    class="slideTab" id="slideTab-5">Birthday</a></li>
                                <li class="yt-Graduation yt-NavHome-Left"><a href="<%=Session["APP_PATH"]%>channelhomepage.aspx?Type=Graduation&Theme='GraduationDefault'"
                                    class="slideTab" id="slideTab-3">Graduation</a></li>
                                <li class="yt-Memorial yt-NavHome-Right"><a href="<%=Session["APP_PATH"]%>channelhomepage.aspx?Type=Memorial&Theme='MemorialDefault'"
                                    class="slideTab" id="slideTab-6">Memorial</a></li>
                                <li class="yt-Wedding yt-NavHome-Left"><a href="<%=Session["APP_PATH"]%>channelhomepage.aspx?Type=Wedding&Theme='WeddingDefault'"
                                    class="slideTab" id="slideTab-4">Wedding</a></li>
                                <li class="yt-Anniversary yt-NavHome-Right"><a href="<%=Session["APP_PATH"]%>channelhomepage.aspx?Type=Anniversary&Theme='AnniversaryDefault'"
                                    class="slideTab" id="slideTab-7">Anniversary</a></li>
                            </ul>
                            <% }
                               else
                               { %>
                            <!-- These url's will work on Remote server. Comment the above urls -->
                            <ul class="yt-NavHome">
                                <li class="yt-NewBaby yt-NavHome-Left"><a href="http://newbaby.<%=TributesPortal.Utilities.WebConfig.TopLevelDomain%>"
                                    class="slideTab" id="slideTab-2">New Baby</a></li>
                                <li class="yt-Birthday yt-NavHome-Right"><a href="http://birthday.<%=TributesPortal.Utilities.WebConfig.TopLevelDomain%>"
                                    class="slideTab" id="slideTab-5">Birthday</a></li>
                                <li class="yt-Graduation yt-NavHome-Left"><a href="http://graduation.<%=TributesPortal.Utilities.WebConfig.TopLevelDomain%>"
                                    class="slideTab" id="slideTab-3">Graduation</a></li>
                                <li class="yt-Memorial yt-NavHome-Right"><a href="<%=LinkOtherDomain%>" class="slideTab"
                                    id="slideTab-6">Memorial</a></li>
                                <li class="yt-Wedding yt-NavHome-Left"><a href="http://wedding.<%=TributesPortal.Utilities.WebConfig.TopLevelDomain%>"
                                    class="slideTab" id="slideTab-4">Wedding</a></li>
                                <li class="yt-Anniversary yt-NavHome-Right"><a href="http://anniversary.<%=TributesPortal.Utilities.WebConfig.TopLevelDomain%>"
                                    class="slideTab" id="slideTab-7">Anniversary</a></li>
                            </ul>
                            <% } %>
                            <div class="yt-Hero-animation-Container">
                                <div class='yt-Hero-animation-Inner'>
                                    <!--animation block starts here. DO NOT change the IDs of the following-->
                                    <div id='yt-slide-1' class='yt-slide'>
                                        <img src='<%=Session["APP_BASE_DOMAIN"]%>assets/images/slide_Tribute.png' alt='Slide' />
                                        <div class='yt-slideStory'>
                                            <h4 id="YT12" runat="server" class="blue">
                                                Your
                                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%></h4>
                                            <h5>
                                                The smartest and easiest way to plan, share and remember an event.</h5>
                                            <p id="YT13" runat="server">
                                                Create a personal website (a
                                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>)
                                                for your significant event in minutes. Send stylish online invitations with RSVP.
                                                Add photos and videos and let your friends and family do the same. Receive personal
                                                messages in your guestbook as well as virtual gifts. Plus many more easy-to-use
                                                features!
                                            </p>
                                        </div>
                                    </div>
                                    <div id='yt-slide-2' class='yt-slide'>
                                        <img src='<%=Session["APP_BASE_DOMAIN"]%>assets/images/slide_Newbaby.png' alt='Slide' />
                                        <div class='yt-slideStory'>
                                            <h4 class="yellow">
                                                New Baby</h4>
                                            <h5>
                                                The funnest and easiest way to announce and share your new baby.
                                            </h5>
                                            <p id="YT14" runat="server">
                                                Create a personal website (a
                                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>)
                                                for your new baby in minutes. Send beautiful online baby anouncements. Share stories
                                                and add photos and videos from before and after the birth. Receive personal messages
                                                in your guestbook. Plus much more!
                                            </p>
                                            <a href="<%="http://newbaby.your"+ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToLower()+".com" %>"
                                                class="learnMore-2">Learn More &gt;</a>
                                        </div>
                                        <div class="yt-exampleLink">
                                            <p>
                                                <span id="YT15" runat="server">New Baby Tribute:</span> <a href="<%="http://newbaby.your"+ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToLower()+".com/elizabethjohnson/" %>"
                                                    class="learnMore-2" target="_blank">Elizabeth Lynn Johnson</a></p>
                                        </div>
                                    </div>
                                    <div id='yt-slide-3' class='yt-slide'>
                                        <img src='<%=Session["APP_BASE_DOMAIN"]%>assets/images/slide_Graduation.png' alt='Slide' />
                                        <div class='yt-slideStory'>
                                            <h4 class="skyBlue">
                                                Graduation</h4>
                                            <h5>
                                                The easiest and most unique way to share and remember your big day.
                                            </h5>
                                            <p id="YT16" runat="server">
                                                Create a personal website (a
                                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>)
                                                for your graduation in minutes. Send beautiful grad invites. Add photos from before
                                                and after the event. Receive personal messages in your guestbook and virtual gifts.
                                                Plus many more easy-to-use features!
                                            </p>
                                            <a href="<%="http://graduation.your"+ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToLower()+".com" %>"
                                                class="learnMore-3">Learn More &gt;</a>
                                        </div>
                                        <div class="yt-exampleLink">
                                            <p>
                                                <span id="YT17" runat="server">Graduation
                                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>:</span>
                                                <a href="<%="http://graduation.your"+ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToLower()+".com/kellysmith/" %>"
                                                    class="learnMore-3" target="_blank">Kelly Smith</a></p>
                                        </div>
                                    </div>
                                    <div id='yt-slide-4' class='yt-slide'>
                                        <img src='<%=Session["APP_BASE_DOMAIN"]%>assets/images/slide_Wedding.png' alt='Slide' />
                                        <div class='yt-slideStory'>
                                            <h4 class="pink">
                                                Wedding</h4>
                                            <h5>
                                                The smartest and easiest way to plan, share and remember your wedding.</h5>
                                            <p id="YT18" runat="server">
                                                Create a personal website (a
                                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>)
                                                for your wedding in minutes. Send beautiful online invitations with RSVP. Add photos
                                                from before and after the wedding and let your guests do the same. Receive personal
                                                messages in your guestbook. Plus much more!
                                            </p>
                                            <a href="<%="http://wedding.your"+ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToLower()+".com" %>"
                                                class="learnMore-4">Learn More &gt;</a>
                                        </div>
                                        <div class="yt-exampleLink">
                                            <p>
                                                <span id="YT19" runat="server">Wedding
                                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>:
                                                </span><a href="<%="http://wedding.your"+ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToLower()+".com/jonandmary/" %>"
                                                    class="learnMore-4" target="_blank">Jon &amp; Mary</a></p>
                                        </div>
                                    </div>
                                    <div id='yt-slide-5' class='yt-slide'>
                                        <img src='<%=Session["APP_BASE_DOMAIN"]%>assets/images/slide_Birthday.png' alt='Slide' />
                                        <div class='yt-slideStory'>
                                            <h4 class="orange">
                                                Birthday</h4>
                                            <h5>
                                                The easiest and funnest way to plan, share and remember your birthday.
                                            </h5>
                                            <p id="YT20" runat="server">
                                                Create a personal website (a
                                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>)
                                                for your birthday in minutes. Send stylish online birthday invitations with RSVP.
                                                Add photos from before and after the event and let your friends do the same. Receive
                                                messages in your guestbook, virtual gifts, and more!
                                            </p>
                                            <a href="<%="http://birthday.your"+ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToLower()+".com" %>"
                                                class="learnMore-5">Learn More &gt;</a>
                                        </div>
                                        <div class="yt-exampleLink">
                                            <p>
                                                <span id="YT21" runat="server">Birthday
                                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>:
                                                </span><a href="<%="http://birthday.your"+ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToLower()+".com/jensweet16/" %>"
                                                    class="learnMore-5" target="_blank">Jen's Sweet 16</a></p>
                                        </div>
                                    </div>
                                    <div id='yt-slide-6' class='yt-slide'>
                                        <img src='<%=Session["APP_BASE_DOMAIN"]%>assets/images/slide_Memorial.png' alt='Slide' />
                                        <div class='yt-slideStory'>
                                            <h4 class="purple">
                                                Memorial</h4>
                                            <h5>
                                                The most personalized and thoughtful way to remember a loved one.
                                            </h5>
                                            <p id="YT22" runat="server">
                                                Create a personal website (a
                                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>)
                                                for your loved one in minutes. Share their story. Send stylish online thank you
                                                cards. Add photos and videos and let friends and family do the same. Receive personal
                                                messages in the guestbook and much more!
                                            </p>
                                            <a href="http://memorial.yourtribute.com" class="learnMore-6"><b>Learn More &gt;</b></a>
                                        </div>
                                        <div class="yt-exampleLink">
                                            <p>
                                                <span id="YT23" runat="server">Memorial
                                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>:</span>
                                                <a href="http://memorial.yourtribute.com/evelynmsmith/" class="learnMore-6" target="_blank">
                                                    Evelyn Mary Smith</a></p>
                                        </div>
                                    </div>
                                    <div id='yt-slide-7' class='yt-slide'>
                                        <img src='<%=Session["APP_BASE_DOMAIN"]%>assets/images/slide_Anniversary.png' alt='Slide' />
                                        <div class='yt-slideStory'>
                                            <h4 class="green">
                                                Anniversary</h4>
                                            <h5>
                                                The most creative and fun way to share your important milestone.</h5>
                                            <p id="YT24" runat="server">
                                                Create a personal website (a
                                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>)
                                                for your anniversary in minutes. Send stylish online invitations. Add photos and
                                                videos from before and after the event. Receive personal messages in the guestbook
                                                and virtual gifts. Plus many more easy-to-use features!
                                            </p>
                                            <a href="<%="http://anniversary.your"+ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToLower()+".com" %>"
                                                class="learnMore-7">Learn More &gt;</a>
                                        </div>
                                        <div class="yt-exampleLink">
                                            <p>
                                                <span id="YT25" runat="server">Anniversary
                                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>:</span>
                                                <a href="http://anniversary.yourtribute.com/billandjune/" class="learnMore-7" target="_blank">
                                                    Bill &amp; June Stiles</a></p>
                                        </div>
                                    </div>
                                </div>
                                <!--animation block ends here-->
                                <div class="hack-clearBoth">
                                </div>
                                <ul class="yt-animationBulletsBlock">
                                    <li id="slideBullet-1">
                                        <!--DO NOT change the names of the following-->
                                        <a href="#" class="slideBullet"></a></li>
                                    <li id="slideBullet-2"><a href="#" class="slideBullet"></a></li>
                                    <li id="slideBullet-3"><a href="#" class="slideBullet"></a></li>
                                    <li id="slideBullet-4"><a href="#" class="slideBullet"></a></li>
                                    <li id="slideBullet-5"><a href="#" class="slideBullet"></a></li>
                                    <li id="slideBullet-6"><a href="#" class="slideBullet"></a></li>
                                    <li id="slideBullet-7"><a href="#" class="slideBullet"></a></li>
                                </ul>
                            </div>
                            <%-- <div class="yt-Hero-Options">
                                    <a href="<%=Session["APP_BASE_DOMAIN"]%>tributecreation.aspx" class="yt-CreateTributeButton">Create a Tribute</a>
                                    <a href="<%=Session["APP_BASE_DOMAIN"]%>aboutfeatures.aspx" class="yt-TakeTourButton">Take a Tour</a>
                                </div>--%>
                        </div>
                        <!--yt-Hero-->
                        <div class="yt-Hero" id="YTSlider" runat="server">
                            <div class="yt-CreateTribute-Options-MT">
                                <a href="<%=Session["APP_BASE_DOMAIN"]%>pricing.aspx" class="yt-CreateTributeButton-MT">
                                    Create a
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%></a>
                            </div>
                            <div class="yt-Hero-animation-Container-MT">
                                <div class='yt-Hero-animation-Inner'>
                                    <div style="float: left; width: 270px; height: 246px;">
                                        <img src='<%=Session["APP_BASE_DOMAIN"]%>assets/images/slide_Memorial.jpg' alt='Slide' />
                                    </div>
                                    <div class='yt-slideStory'>
                                        <h4 class="purple">
                                            Your
                                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%></h4>
                                        <h5>
                                            Commemorate a life<br />
                                            with a permanent
                                            <br />
                                            online memorial.
                                        </h5>
                                        <p id="P4" runat="server" class="createTextP4">
                                            Create free online obituaries or premium memorial websites for loved ones in minutes.
                                            Easily invite friends and family to share their condolences, stories, memories,
                                            photos and videos on the memorial.
                                        </p>
                                        <br/>
                                        <a href="<%=Session["APP_BASE_DOMAIN"]%>tour.aspx" class="learmore-MT"><b>Learn More &gt;</b></a>
                                    </div>
                                    <div class="hack-clearBoth">
                                    </div>
                                    <div class="yt-SamplePadding">
                                        <p>
                                            <span id="Span1" runat="server"><b>Sample Memorial Website:</b>
                                                 </span>
                                            <a id="Links" href="http://memorial.yourtribute.com/evelynmsmith/" target="_blank"
                                                class="learnMore-6">Evelyn Mary Smith</a></p>
                                    </div>
                                </div>
                            </div>
                            <div class="yt-HomeButtons-Options-MT">
                                <%--COMDIFFRES: (we are assigining tributetype in trubutelink variable so it will not create any problem) Is this variable correct?--%>
                                <a href="<%=Session["APP_BASE_DOMAIN"]%>tour.aspx" class="yt-TakeTourButton-MT">Take
                                    a Tour</a>
                            </div>
                        </div>
                        <%-- Commented code remove by Ashu --%>
                        <div class="hack-clearBoth">
                        </div>
                        <%--
                            <ul class="yt-AdditionalOptions">
                                <li><a href="<%=Session["APP_BASE_DOMAIN"]%>alltribute.aspx">View Tributes</a></li>
                                <li><a href="<%=Session["APP_BASE_DOMAIN"]%>aboutus.aspx">About Us</a></li>
                            </ul>--%>
                    </div>
                    <div class="hack-clearBoth">
                    </div>
                    <!--yt-ContentPrimary-->
                </div>
                <div id="adGrid">
                    <% if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower().Equals("yourmoments"))
                       { %>
                    <div class="padLeft100">
                        <div id="bigButtons" runat="server" class="bigButtons">
                            <a href="<%=Session["APP_BASE_DOMAIN"]%>pricing.aspx" class="leftBigButton">Create a
                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%></a>
                            <a href="<%=Session["APP_BASE_DOMAIN"]%>tour.aspx" class="rightBigButton">Take a Tour</a>
                        </div>
                    </div>
                    <%} %>
                    <div id="topQuote">
                        <% if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower().Equals("yourtribute"))
                           { %>
                        <h1 class="NormalFont">
                            <span class="bold boldTextStyle">Pay
                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToLower()%>
                                to the life of a loved one with an online memorial from Your
                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>.</span>
                        </h1>
                        <span class="underText">Create a free Online Obituary or premium Memorial Website. Get
                            started in seconds. No Credit Card required.</span>
                        <%} %>
                        <%else
                            { %>
                        <h3>
                            Over<span class="bold"> 50,000 people</span> have used <span class="bold">Your
                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%></span> to
                            celebrate a <span id="Span3" runat="server" class="bold">significant event</span>
                            or <span class="bold">special someone</span>.</h3>
                        <%} %>
                    </div>
                    <% if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower().Equals("yourtribute"))
                       { %>
                    <div style="height: 20px; margin-top: 10px; margin-left: -20px; background-color: #BBDFF2;
                        padding-top: 7px; padding-bottom: 7px;">
                        <span style="margin-left: 50px; color: #5BB4E5; font-weight: bold; font-size: 16px;">
                            Searching for an existing online obituary or memorial website?</span> <span style="margin-left: 10px;
                                top: -2px; position: relative;">
                                <asp:TextBox ID="txtsearch" runat="server" CssClass="SearchTxt-MT" Text="Enter a First and Last Name"
                                    onfocus="if(this.value=='Enter a First and Last Name') {this.value='';this.style.color = '#7E84B6';this.style.fontStyle='normal';}"
                                    onkeypress="this.style.color = 'black';this.style.fontStyle='normal';" onblur="if(this.value=='') {this.value='Enter a First and Last Name';this.style.color = '#7E84B6';} else{this.style.color = 'black';this.style.fontStyle='normal';}"></asp:TextBox></span>
                        <span style="margin-left: 5px; top: -2px; position: relative;">
                            <asp:Button ID="btnSearch" ValidationGroup="SearchError" runat="server" CssClass="yt-SearchBtn-MT"
                                Text="Search" OnClick="btnSearch_Click" /></span> <span>
                                    <asp:RequiredFieldValidator ControlToValidate="txtsearch" InitialValue="Enter a First and Last Name"
                                        ID="reqSearch" runat="server" Font-Bold="true" Font-Size="14" ErrorMessage=" ! " Display="Dynamic"
                                        ValidationGroup="SearchError"> </asp:RequiredFieldValidator>
                                </span>
                    </div>
                    <%} %>
                    <div class="whatBlock">
                        <h4 class="Purple-MT" id="YT2" runat="server">
                            What is Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>?</h4>
                        <% if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower().Equals("yourtribute"))
                           { %>
                        <p class="Purple-MT">
                            Your Tribute is an easy-to-use website that allows you to create an online memorial
                            to commemorate a life. Create free online obituaries or memorial websites to remember
                            your loved ones. Share their story, add photos and videos, leave condolences and
                            memorials, and then invite friends and family to do the same.</p>
                        <%} %>
                        <%else
                            { %>
                        <p class="">
                            A web-based tool, that lets you set up a personal website (a
                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>)
                            to plan, share and remember a significant event or special someone. A
                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                            can be created in minutes, but remains online for life to provide an everlasting
                            record of the special occasion.</p>
                        <%} %>
                    </div>
                    <div class="whyBlock">
                        <h4 class="Gray-MT" id="YT4" runat="server">
                            Why Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>?</h4>
                        <% if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower().Equals("yourtribute"))
                           { %>
                        <p class="Gray-MT">
                            Since 2002, friends and family have used Your Tribute to create more than 75,000
                            online obituaries and memorial websites to commemorate the life of loved ones. Our
                            team, with more than 50 years experience in the funeral industry, is dedicated to
                            helping you create a beautiful tribute to a departed person.</p>
                        <%} %>
                        <%else
                            { %>
                        <p class="">
                            It is easy and elegant. Create a personalized
                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                            for your event in minutes. Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                            includes many of the features of popular online invitation, photo sharing, blogging,
                            and social networking websites, in an easy-to-use intuitive interface.</p>
                        <%} %>
                    </div>
                    <% if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower().Equals("yourtribute"))
                       { %>
                    <div id="bButtons" runat="server" class="bigButtons-MT">
                        <a href="<%=Session["APP_BASE_DOMAIN"]%>pricing.aspx" class="leftBigButton">Create a
                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%></a>
                        <a href="<%=Session["APP_BASE_DOMAIN"]%>tour.aspx" class="rightBigButton">Take a Tour</a>
                    </div>
                    <div class="hack-clearBoth">
                    </div>
                    <%} %>
                    <div id="YTDiv" runat="server">
                        <div id="">
                            <div class="HomePage">
                                <h3>
                                    Privacy & Security</h3>
                                <p id="YT6" runat="server">
                                    Your personal info and memories are safe and secure with us. All data is backed
                                    up daily and stored in secure data centers.</p>
                                <div class="HomepageBox_Privacy">
                                </div>
                                <!--<img src="images/col11.gif" alt="good people"/>-->
                            </div>
                            <div class="HomePage">
                                <h3>
                                    Facebook Integration</h3>
                                <p id="P2" runat="server">
                                    Log in using your Facebook account and easily share your memories with your Facebook
                                    friends by posting to your wall.</p>
                                <div class="HomepageBox_Facebook">
                                </div>
                                <!--<img src="images/col12.gif" alt="good people"/>-->
                            </div>
                            <div class="HomePage AvoidRightBorder">
                                <h3>
                                    Lifetime Storage</h3>
                                <p id="P3" runat="server">
                                    Create a memorial websites to ensure your memories, including condolences , photos
                                    and videos, will remain online permanently.</p>
                                <div class="HomepageBox_Lifetime">
                                </div>
                                <!--<img src="images/col13.gif" alt="good people"/>-->
                            </div>
                        </div>
                        <div id="">
                            <div class="HomePageBottom">
                                <h3>
                                    Beautiful Themes</h3>
                                <p id="P1" runat="server">
                                    Personalize all free online obituaries or memorial websites with one of our designer
                                    themes. You can switch the theme any time.</p>
                                <div class="HomepageBox_Themes">
                                </div>
                                <!--<img src="images/col31.gif" alt="good people"/>-->
                            </div>
                            <div class="HomePageBottom">
                                <h3>
                                    Guestbook & Memorials</h3>
                                <p>
                                    Leave a condolence message in the online guestbook or choose a memorial to add to
                                    the online obituary or memorial website.
                                </p>
                                <div class="HomepageBox_Guestbook">
                                </div>
                                <!-- <img src="images/col32.gif" alt="good people"/>-->
                            </div>
                            <div class="HomePageBottom AvoidRightBorder">
                                <h3>
                                    High Resolution Photos</h3>
                                <p>
                                    We store the original version of every photo (up to 3MP). Premium memorial websites
                                    can view and save high resolution photos.</p>
                                <div class="HomepageBox_Photos">
                                </div>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <%if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourtribute")
                          { %>
                        <div class="testimonialBlock-MT">
                            <h2 class="blueHome" id="YT9" runat="server">
                                Celebrate a life and remember a special someone with Your
                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>.</h2>
                            <h2 class="h2TestimonialTextSize">
                                <span class="h2TestimonialTextSize1">Create free </span><span class="h2TestimonialTextSize2">
                                    Online Obituaries</span> <span class="h2TestimonialTextSize1">or premium </span>
                                <span class="h2TestimonialTextSize2" id="YT10" runat="server">Memorial Websites</span><span class="h2TestimonialTextSize1"> in a few simple steps.</span></h2>
                            <%--   <h2 class="h2TestimonialTextSize">
                            Create <b>free</b> personalized <b>Tributes</b> to <b>plan</b>, <b>share</b> and
                            <b>remember</b> life's most <b>important events</b>.</h2>--%>
                            <p class="HomePageFontSize">
                                Share their story, add photos and videos, leave condolences and memorials, and then
                                invite friends and family to do the same.</p>
                        </div>
                        <%} %>
                    </div>
                    <div id="YMDiv" runat="server">
                        <div id="rowOne" style="margin-top: 30px;">
                            <div class="third col11">
                                <h3>
                                    Beautiful Themes</h3>
                                <p id="P5" runat="server">
                                    Choose from our collection of themes created by our top designers. Multiple themes
                                    are available for each
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                    type with more added all the time!</p>
                                <br />
                                <!--<img src="images/col11.gif" alt="good people"/>-->
                            </div>
                            <div class="third col12">
                                <h3>
                                    Stylish Invitations</h3>
                                <p>
                                    Send free online invitations and eCards. Wedding invitations, save-the-dates, baby
                                    announcements, birthday cards and more!</p>
                                <!--<img src="images/col12.gif" alt="good people"/>-->
                            </div>
                            <div class="third last col13">
                                <h3>
                                    Facebook Integration</h3>
                                <p id="YT7" runat="server">
                                    One-step login using your Facebook account. Invite Facebook friends to your
                                    <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToLower()%>
                                    and events and easily publish to your wall in one click.</p>
                                <!--<img src="images/col13.gif" alt="good people"/>-->
                            </div>
                        </div>
                        <div id="rowThree">
                            <div class="third col31 bottom">
                                <h3>
                                    Advanced RSVP</h3>
                                <p>
                                    Easily import contacts (including Facebook friends) and invite them to your event(s).
                                    Collect meal preferences and more. Manage and export your guest lists.</p>
                                <br />
                                <!--<img src="images/col31.gif" alt="good people"/>-->
                            </div>
                            <div class="third col32 bottom">
                                <h3>
                                    High Resolution Photos</h3>
                                <p>
                                    We store the original version of every photo. Your photos are safe and secure with
                                    us.</p>
                                <!-- <img src="images/col32.gif" alt="good people"/>-->
                            </div>
                            <div class="third last col33 bottom">
                                <h3>
                                    Lifetime Storage</h3>
                                <p id="YT8" runat="server">
                                    We don’t think you should have to keep paying to keep your important event online.
                                    Your
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                    and all of its content will remain online for life, we guarantee it!</p>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <%if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments")
                          { %>
                        <div class="testimonialBlock">
                            <h1 class="blueHome" id="H1" runat="server">
                                Celebrate a significant event or a special someone with Your
                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>.</h1>
                            <h2 class="h2TestimonialTextSize">
                                <span class="h2TestimonialTextSize1">Create </span><span class="h2TestimonialTextSize2">
                                    free</span> <span class="h2TestimonialTextSize1">personalized </span><span class="h2TestimonialTextSize2"
                                        id="Span2" runat="server">
                                        <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>s</span>
                                <span class="h2TestimonialTextSize1">to </span><span class="h2TestimonialTextSize2">
                                    plan</span><span class="h2TestimonialTextSize1">,</span> <span class="h2TestimonialTextSize2">
                                        share</span><span class="h2TestimonialTextSize1"> and </span><span class="h2TestimonialTextSize2">
                                            remember</span><span class="h2TestimonialTextSize1"> life's most </span>
                                <span class="h2TestimonialTextSize2">important events</span><span class="h2TestimonialTextSize1">.</span></h2>
                            <%--   <h2 class="h2TestimonialTextSize">
                            Create <b>free</b> personalized <b>Tributes</b> to <b>plan</b>, <b>share</b> and
                            <b>remember</b> life's most <b>important events</b>.</h2>--%>
                            <p class="HomePageFontSize">
                                Send online invitations with RSVP, share photos and videos, receive personal guestbook
                                messages and virtual gifts, plus much more!</p>
                        </div>
                        <%} %>
                    </div>
                    <div class="divider">
                    </div>
                    <div id="divTributeBoxBlue" class="tryTributeBoxBlue rounded-MT">
                        <div class="rightTribute">
                            <p class="actionbuttonPurple" id="CreateBtn" runat="server">
                                <a href="<%=Session["APP_BASE_DOMAIN"]%>pricing.aspx">Create
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%></a>
                            </p>
                        </div>
                        <div class="leftTribute">
                            <h2 id="YT11" runat="server">
                                Create an online memorial for free!</h2>
                            <p>
                                Get started in seconds, no credit card, no commitment.</p>
                        </div>
                    </div>
                    <div class="ytIntro">
                        <p class="bottomBlock">
                            The Importance of Free Online Obituaries
                        </p>
                        <p>
                            An obituary, also referred to as a death notice, is typically published In a local
                            newspaper. The obituary includes the death announcement, life history, family and
                            funeral information. Obituaries are necessary because they notify people of the
                            deceased's passing. However, today more people rely on the Internet for information
                            and free online obituaries are an important way to ensure that people are notified
                            of the death.</p>
                        <br />
                        <p>
                            Free online obituaries create a permanent online record of the person's death that
                            can always be located by friends and family. Furthermore, the online obituary can
                            be shared by email and through social networking websites and can be viewed by people
                            throughout the world. These are major advantages over newspapers, which can only
                            be viewed locally and are often only permanently saved by a few family members.</p>
                        <br />
                        <p>
                            Another important reason to create free online obituaries is to provide friends
                            and family with a location where they can connect and share memories. Online obituaries
                            often include a guestbook feature where people can leave condolences. Your Tribute's
                            free online obituaries include a guestbook, but also allow users to share stories,
                            photos, videos and more.</p>
                        <br />
                        <p class="bottomBlock">
                            The Benefits of Premium Memorial Websites
                        </p>
                        
                        <p>
                            Free online obituaries are a quick and easy way to create an online death notice,
                            but premium memorial websites offer a number of benefits. A memorial website is
                            more personalized and creates an online memorial to memorialize and pay tribute
                            to the life of an individual. A memorial website preserves history and memories
                            and makes them available for future generations to view and contribute to.</p>
                        <br />
                        <p>
                            Premium memorial websites from Your Tribute have no content limits. Users can add
                            an unlimited number of stories, condolences, memorials, photos, videos and more.
                            In addition to being able to add unlimited content, users can upload and view high-resolution
                            photos and HD videos. This gives friends and family a central location where they
                            can share photos and videos of their departed loved one.</p>
                        <br />
                        <p>
                            Some of the other benefits of creating premium memorial websites is that you receive
                            a custom URL for the website which makes it easier to share with friends and family.
                            You also have a wider selection of themes to choose from allowing you to create
                            a more personalized memorial. Finally, creating a permanent memorial will ensure
                            that the tribute remains online forever for future generations to view.</p>
                        <br />
                        <p>
                            <a id="btmPricingLink" href="<%=Session["APP_BASE_DOMAIN"]%>pricing.aspx">Get started with a free Online
                                Obituary or premium Memorial Website.></a></p>
                    </div>
                </div>
                <!--adGrid ends here-->
                <!--yt-ContentPrimaryContainer-->
                <div class="hack-clearBoth">
                </div>
                <div id="Bottombackground" runat="server" class="yt-ContentContainerImage">
                </div>
            </div>
            <!--yt-ContentContainerInner-->
        </div>
        <!--yt-ContentContainer-->
        <!--<div class="yt-Footer">-->
        <uc:Footer ID="Footer1" runat="server" />
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

    <script type="text/javascript">
    executeBeforeLoad();   
    //parentSlideshow(); 
<% if (ConfigurationManager.AppSettings["ApiKey"] != null) { %>
      function update_user_is_connected() {
        header_user_is_connected();
        FB.XFBML.parse();
      }
      function update_user_is_not_connected() {
        header_user_is_not_connected();
        FB.XFBML.parse();
      }        
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


<% }  %>

    </script>

</body>
</html>
