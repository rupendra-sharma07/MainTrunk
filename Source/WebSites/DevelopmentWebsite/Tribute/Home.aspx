<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Tribute_ParentHomepage"
    EnableViewStateMac="true" %>

<%@ Register Src="../UserControl/YourTributeHeader.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/FooterHome.ascx" TagName="Footer" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:fb="http://www.facebook.com/2008/fbml"
xml:lang="en" lang="en">
<head>
    <title id="HomeTitle" runat="server" >Your Tribute’s Free Obituaries Online | Permanent Online Memorials</title>
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
    <meta name="description" content="Free Obituaries Online – Let Your Tribute help you celebrate the lives of your deceased loved ones through our free obituaries online and more. Sign up and share precious memories online with your friends and family today." />
    <meta name="keywords" content="free obituaries online" />
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

    <!--#include file="../analytics.asp"-->
</head>
<body>
    <form id="Form1" action="" runat="server">
    <div id="divShowModalPopup">
    </div>
    <div class="SpecialAnnouncement" id="YTAnnouncement" runat="server">
        <b>Special Announcement:</b> Are you looking for <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>s for a Wedding, Baby, Graduation,
        Anniversary or Birthday? <b><a style="color: White;" href="<%="http://blog.your"+ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToLower()+".com" %>"
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
                                <li class="yt-Memorial yt-NavHome-Right"><a href="<%=LinkOtherDomain%>"
                                    class="slideTab" id="slideTab-6">Memorial</a></li>
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
                                                Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%></h4>
                                            <h5>
                                                The smartest and easiest way to plan, share and remember an event.</h5>
                                            <p id="YT13" runat="server">
                                                Create a personal website (a <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>) for your significant event in minutes. Send
                                                stylish online invitations with RSVP. Add photos and videos and let your friends
                                                and family do the same. Receive personal messages in your guestbook as well as virtual
                                                gifts. Plus many more easy-to-use features!
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
                                                Create a personal website (a <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>) for your new baby in minutes. Send beautiful
                                                online baby anouncements. Share stories and add photos and videos from before and
                                                after the birth. Receive personal messages in your guestbook. Plus much more!
                                            </p>
                                            <a href="<%="http://newbaby.your"+ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToLower()+".com" %>" class="learnMore-2">Learn More &gt;</a>
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
                                                Create a personal website (a <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>) for your graduation in minutes. Send beautiful
                                                grad invites. Add photos from before and after the event. Receive personal messages
                                                in your guestbook and virtual gifts. Plus many more easy-to-use features!
                                            </p>
                                            <a href="<%="http://graduation.your"+ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToLower()+".com" %>" class="learnMore-3">Learn More &gt;</a>
                                        </div>
                                        <div class="yt-exampleLink">
                                            <p>
                                                <span id="YT17" runat="server">Graduation <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>:</span> <a href="<%="http://graduation.your"+ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToLower()+".com/kellysmith/" %>"
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
                                                Create a personal website (a <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>) for your wedding in minutes. Send beautiful
                                                online invitations with RSVP. Add photos from before and after the wedding and let
                                                your guests do the same. Receive personal messages in your guestbook. Plus much
                                                more!
                                            </p>
                                            <a href="<%="http://wedding.your"+ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToLower()+".com" %>" class="learnMore-4">Learn More &gt;</a>
                                        </div>
                                        <div class="yt-exampleLink">
                                            <p>
                                                <span id="YT19" runat="server">Wedding <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>: </span><a href="<%="http://wedding.your"+ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToLower()+".com/jonandmary/" %>"
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
                                                Create a personal website (a <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>) for your birthday in minutes. Send stylish
                                                online birthday invitations with RSVP. Add photos from before and after the event
                                                and let your friends do the same. Receive messages in your guestbook, virtual gifts,
                                                and more!
                                            </p>
                                            <a href="<%="http://birthday.your"+ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToLower()+".com" %>" class="learnMore-5">Learn More &gt;</a>
                                        </div>
                                        <div class="yt-exampleLink">
                                            <p>
                                                <span id="YT21" runat="server">Birthday <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>: </span><a href="<%="http://birthday.your"+ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToLower()+".com/jensweet16/" %>" 
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
                                                Create a personal website (a <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>) for your loved one in minutes. Share their
                                                story. Send stylish online thank you cards. Add photos and videos and let friends
                                                and family do the same. Receive personal messages in the guestbook and much more!
                                            </p>
                                            <a href="http://memorial.yourtribute.com" class="learnMore-6">Learn More &gt;</a>
                                        </div>
                                        <div class="yt-exampleLink">
                                            <p>
                                                <span id="YT23" runat="server">Memorial <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>:</span> <a href="http://memorial.yourtribute.com/evelynmsmith/"
                                                    class="learnMore-6" target="_blank">Evelyn Mary Smith</a></p>
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
                                                Create a personal website (a <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>) for your anniversary in minutes. Send stylish
                                                online invitations. Add photos and videos from before and after the event. Receive
                                                personal messages in the guestbook and virtual gifts. Plus many more easy-to-use
                                                features!
                                            </p>
                                            <a href="<%="http://anniversary.your"+ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToLower()+".com" %>" class="learnMore-7">Learn More &gt;</a>
                                        </div>
                                        <div class="yt-exampleLink">
                                            <p>
                                                <span id="YT25" runat="server">Anniversary <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>:</span> <a href="http://anniversary.yourtribute.com/billandjune/"
                                                    class="learnMore-7" target="_blank">Bill &amp; June Stiles</a></p>
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
                                    Create a <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%></a>
                            </div>
                            <div class="yt-Hero-animation-Container-MT">
                                <div class='yt-Hero-animation-Inner'>
                                    <div style="float: left; width: 270px; height: 246px;">
                                        <img src='<%=Session["APP_BASE_DOMAIN"]%>assets/images/slide_Memorial.jpg' alt='Slide' />
                                    </div>
                                    <div class='yt-slideStory'>
                                        <h4 class="purple">
                                            Your <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%></h4>
                                        <h5>
                                            Commemorate a life<br />
                                            with a permanent
                                            <br />
                                            online memorial.
                                        </h5>
                                        <p id="P4" runat="server">
                                            Create a free online obituary or personalized memorial <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%> website for a loved
                                            one in minutes. Invite friends and family to share condolences, stories, memories,
                                            photos and videos on the memorial.
                                        </p>
                                        <a href="<%=Session["APP_BASE_DOMAIN"]%>tour.aspx" class="learmore-MT">Learn More &gt;</a>
                                    </div>
                                    <div class="hack-clearBoth">
                                    </div>
                                    <div class="yt-SamplePadding">
                                        <p>
                                            <span id="Span1" runat="server">Sample Memorial <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>:</span> <a id="Links" href="http://memorial.yourtribute.com/evelynmsmith/"
                                                target="_blank" class="learnMore-6">Evelyn Mary Smith</a></p>
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
                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%></a> <a href="<%=Session["APP_BASE_DOMAIN"]%>tour.aspx" class="rightBigButton">
                                Take a Tour</a>
                    </div>
                    </div>
                    <%} %>
                    <div id="topQuote">
                        <% if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower().Equals("yourtribute"))
                           { %>
                        <h1 class="NormalFont">
                            Over<span class="bold"> 40,000 families</span> have used 
                            <span id="YT1" runat="server" class="bold">Your <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>'s Free Obituaries </span> online to <span
                                class="bold">remember a life</span>.</h1>
                        <%} %>
                        <%else
                            { %>
                        <h3>
                            Over<span class="bold"> 50,000 people</span> have used <span class="bold">Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%></span>
                            to celebrate a <span id="Span3" runat="server" class="bold">significant event</span>
                            or <span class="bold">special someone</span>.</h3>
                        <%} %>
                    </div>
                    <% if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower().Equals("yourtribute"))
                       { %>
                    <div style="height: 20px; margin-top: 10px; margin-left: -20px; background-color: #BBDFF2;
                        padding-top: 7px; padding-bottom: 7px;">
                        <span style="margin-left: 165px; color: #5BB4E5; font-weight: bold; font-size: 16px;">
                            Searching for an existing memorial?</span> <span style="margin-left: 10px; top: -2px;
                                position: relative;">
                                <asp:TextBox ID="txtsearch" runat="server" CssClass="SearchTxt-MT" Text="Enter a First and Last Name"
                                    onfocus="if(this.value=='Enter a First and Last Name') {this.value='';this.style.color = '#7E84B6';this.style.fontStyle='normal';}"
                                    onkeypress="this.style.color = 'black';this.style.fontStyle='normal';" onblur="if(this.value=='') {this.value='Enter a First and Last Name';this.style.color = '#7E84B6';} else{this.style.color = 'black';this.style.fontStyle='normal';}"></asp:TextBox></span>
                        <span style="margin-left: 5px; top: -2px; position: relative;">
                            <asp:Button ID="btnSearch" ValidationGroup="SearchError" runat="server" CssClass="yt-SearchBtn-MT"
                                Text="Search" OnClick="btnSearch_Click" /></span> <span>
                                    <asp:RequiredFieldValidator ControlToValidate="txtsearch" InitialValue="Enter a First and Last Name"
                                        ID="reqSearch" runat="server" ErrorMessage="Please enter some text" Display="Dynamic"
                                        ValidationGroup="SearchError"> </asp:RequiredFieldValidator>
                                </span>
                    </div>
                    <%} %>
                    <div class="whatBlock">
                        <h4 class="Purple-MT" id="YT2" runat="server">
                            What is Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>?</h4>
                        <% if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower().Equals("yourtribute"))
                           { %>
                        <p class="Purple-MT">
                            A web-based tool that lets you create a permanent online memorial to remember the
                            life of a loved one. An online memorial provides the same features as popular social
                            networking and photo sharing websites in a personalized easy-to-use interface.</p>
                        <%} %>
                        <%else
                            { %>
                        <p class="">
                            A web-based tool, that lets you set up a personal website (a <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>) to plan, share
                            and remember a significant event or special someone. A <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> can be created in
                            minutes, but remains online for life to provide an everlasting record of the special
                            occasion.</p>
                        <%} %>
                    </div>
                    <div class="whyBlock">
                        <h4 class="Gray-MT" id="YT4" runat="server">
                            Why Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>?</h4>
                        <% if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower().Equals("yourtribute"))
                           { %>
                        <p class="Gray-MT">
                            Since 2002 we have provided a safe and secure environment for friends and family to connect and share memories. Free obituaries online can be created in minutes, but remain online forever to provide an everlasting record of the person’s life.</p>
                        <%} %>
                        <%else
                            { %>
                        <p class="">
                            It is easy and elegant. Create a personalized <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> for your event in minutes.
                            Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> includes many of the features of popular online invitation, photo sharing,
                            blogging, and social networking websites, in an easy-to-use intuitive interface.</p>
                        <%} %>
                    </div>
                    <% if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower().Equals("yourtribute"))
                       { %>
                    <div id="bButtons" runat="server" class="bigButtons-MT">
                        <a href="<%=Session["APP_BASE_DOMAIN"]%>pricing.aspx" class="leftBigButton">Create a
                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%></a> <a href="<%=Session["APP_BASE_DOMAIN"]%>tour.aspx" class="rightBigButton">
                                Take a Tour</a>
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
                                    All online memorials, including stories, guestbook, messages, photos and videos
                                    will remain online permanently.</p>
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
                                    Choose from our collection of themes created by top designers. Tired of your theme?
                                    Switching to a new theme is easy!</p>
                                <div class="HomepageBox_Themes">
                                </div>
                                <!--<img src="images/col31.gif" alt="good people"/>-->
                            </div>
                            <div class="HomePageBottom">
                                <h3>
                                    Guestbook & Gifts</h3>
                                <p>
                                    Leave a personal message in the guestbook or choose from a collection of free virtual
                                    gifts to give to the family.</p>
                                <div class="HomepageBox_Guestbook">
                                </div>
                                <!-- <img src="images/col32.gif" alt="good people"/>-->
                            </div>
                            <div class="HomePageBottom AvoidRightBorder">
                                <h3>
                                    High Resolution Photos</h3>
                                <p>
                                    We store the original version of every photo (up to 3 MP). Memorial can view and
                                    download high resolution photos.</p>
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
                                Celebrate a life and remember a special someone with Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>.</h2>
                            <h2 class="h2TestimonialTextSize">
                                <span class="h2TestimonialTextSize1">Create permanent </span><span class="h2TestimonialTextSize2">
                                    free Obituaries</span> <span class="h2TestimonialTextSize1"> online or personalized
                                
                                
                                
                                </span><span class="h2TestimonialTextSize2" id="YT10" runat="server">Memorial
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%></span>
                                <span class="h2TestimonialTextSize1">websites</span><span class="h2TestimonialTextSize1">.</span></h2>
                            <%--   <h2 class="h2TestimonialTextSize">
                            Create <b>free</b> personalized <b>Tributes</b> to <b>plan</b>, <b>share</b> and
                            <b>remember</b> life's most <b>important events</b>.</h2>--%>
                            <p class="HomePageFontSize">
                                Share stories and memories, add photos and videos, receive personal guestbook messages
                                and virtual gifts, plus much more!</p>
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
                    <p>
                        Founded in 2002, Your Tribute is a leading provider of free obituaries online. For
                        the past 10 years we have provided a safe and secure environment for friends and
                        family to connect and share memories. We provide free obituaries and memorial tribute
                        websites to families. We also work with funeral homes worldwide as their obituary
                        provider.</p><br />
                    <p>
                        Free obituaries online are fast and easy to setup. They include the obituary as
                        well as the story page where families can add personal information and write their
                        loved one’s story. All obituaries include free virtual gifts as well as unlimited
                        guestbook messages. The obituary can easily be shared with relatives and friends
                        by email and through social websites like Facebook and Twitter.</p><br />
                    <p>
                        It is easy to upgrade free obituaries online to premium memorial tribute websites.
                        Personalized memorial websites are enhanced versions of the free obituaries. The
                        premium memorial tributes allow you to add information about the funeral and related
                        events. You can invite friends and family and send them virtual thank you cards.
                        Friends and family can also add photos and videos to the memorial website.</p><br />
                    <p>
                        If you have any questions about creating free obituaries online, please contact
                        us at any time. We can provide you with suggestion and help create a beautiful obituary
                        or memorial tribute website for your loved one.<a href="<%=Session["APP_BASE_DOMAIN"]%>pricing.aspx">Click here to get started for free.</a></p>
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
