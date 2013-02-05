<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tour.aspx.cs" Inherits="Miscellaneous_Tour"
    Title="Tour - See how to create a Tribute for your significant event" %>

<%@ Register Src="../UserControl/YourTributeHeader.ascx" TagName="Header" TagPrefix="uc" %>
<%@ Register Src="../UserControl/FooterHome.ascx" TagName="Footer" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:fb="http://www.facebook.com/2008/fbml"
xml:lang="en" lang="en">
<head>
    <title id="tourTitle" runat="server">Tour - Learn how to Create an Online Memorial with
        Your Tribute</title>
    <meta name="description" content="Learn how to create a permanent online memorial for a loved one. Create a free obituary or personalized memorial tribute website in minutes." />
    <meta name="keywords" content="online memorial, free obituary, memorial tribute, personalized memorial, permanent, your tribute" />
    <%--<title>Create a personal website with Your Tribute's online website builder.</title>--%>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Content-Language" content="en-ca" />
    <meta http-equiv="imagetoolbar" content="false" />
    <meta name="robots" content="index,follow" />
    <meta name="MSSmartTagsPreventParsing" content="true" />
    <meta name="description" content="Take our tour and learn how to Create an Online Memorial website with Your Tribute. Online Memorials are easy to create and share with friends and family.">
    <meta name="keywords" content="online memorials, online memorial, online memorial website, online memorial websites">
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
	    window.open(App_Domain+"ModelPopup/StoryFullSizeImage.aspx?ImageName="+imgName,"mywindow","status=0, width=600, height=560");
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
    } catch( err ) {}
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
    <div class="yt-Container yt-Tour" id="yourtribute" runat="server">
        <uc:Header ID="ytHeader" Section="tribute" NavigationName="Tour" runat="server" />
        <div class="hack-clearBoth">
        </div>
        <div class="yt-Breadcrumbs">
            <a href="<%=Session["APP_BASE_DOMAIN"]%>">Home</a> <span class="selected">Tour</span>
        </div>
        <div class="yt-ContentContainer">
            <div class="yt-InnerContentContainer">
                <div class="yt-ContentPrimaryContainer">
                    <div class="yt-Panel-Primary-MT">
                        <h1 class="yt-BiggestHeading OceanBlue-MT">
                            TOUR
                        </h1>
                        <div class="yt-PricingAddText">
                            <h1 class="yt-PricingHeading">
                                <div class="Purple-MT">
                                    Build an Online Memorial with Your Tribute</div>
                            </h1>
                            <p class="textunderPricing NewTextColor-MT">
                                With Your Tribute you can create a beautiful online memorial website to pay tribute
                                to a friend or family member who has passed away. An online memorial provides friends
                                and family with a central location where they can share stories, fond memories,
                                photographs and grieve together. The memorial can remain online for life allowing
                                friends and family to continue to contribute content year after year. Additionally,
                                future generations of family members can visit the online memorial to learn about
                                the deceased person. Take our tour and learn what features are included with a Your
                                Tribute online memorial.
                            </p>
                            <br />
                        </div>
                        <%--<div class="hack-clearBoth"></div>
                            <div class="adSpacer30">&nbsp;</div>--%>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="leftSideTourBlock">
                            <h3>
                                Create a Memorial
                            </h3>
                            <div class="tourText">
                                <p>
                                    Create a free online memorial for your loved one in a few simple steps. Enter their
                                    name, biographical details and upload their photo. Next, choose to make their memorial
                                    website public or private. Finally, review the information and when you're ready,
                                    click create!
                                </p>
                            </div>
                            <div class="tourSmallImage">
                                <a href="javascript: openwindow('tour_CREATE_Fullsize.jpg')">
                                    <img alt="Create a memorial image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/tour_CREATE_small.png" />
                                </a>
                            </div>
                        </div>
                        <div class="rightSideTourBlock">
                            <h3>
                                Choose a Theme
                            </h3>
                            <div class="tourText">
                                <p>
                                    Personalize the online memorial website with one of our designer themes. Theme genres
                                    include nature, sky, flowers, religion and much more. New themes are regularly added.
                                    Switch your theme any time, from any of your pages, using the "Change Theme" link.
                                </p>
                            </div>
                            <div class="tourSmallImage">
                                <a href="javascript: openwindow('tour_THEME_Fullsize.jpg')">
                                    <img alt="Choose a theme image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/tour_THEME_small.png" />
                                </a>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <%--                        <div class="hack-clearBoth">
                        </div>
                        <div class="hack-clearBoth">
                        </div>--%>
                        <div id="divTributeBoxBlue" class="tryTributeBoxskyBlue-MT rounded">
                            <div class="rightTribute">
                                <p class="gettingstartedBlue">
                                    <a href="<%=Session["APP_BASE_DOMAIN"]%>pricing.aspx">Create
                                        <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%></a>
                                </p>
                            </div>
                            <div class="leftTribute">
                                <h2>
                                    Create an online memorial for free!</h2>
                                <p>
                                    Get started in seconds, no credit card, no commitment.</p>
                            </div>
                        </div>
                        <div class="leftSideTourBlock">
                            <h3>
                                Write their Story
                            </h3>
                            <div class="tourText">
                                <p>
                                    The Story page includes personal details such as the person's name and photo, location,
                                    date and more. We recommend that you start the memorial by adding content to this
                                    page. The story could include important dates, milestones, history, places, trivia,
                                    fun facts and more.
                                </p>
                            </div>
                            <div class="tourSmallImage">
                                <a href="javascript: openwindow('tour_STORY_Fullsize.jpg')">
                                    <img alt="Story image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/tour_STORY_small.png" />
                                </a>
                            </div>
                        </div>
                        <div class="rightSideTourBlock">
                            <h3>
                                Add an Event
                            </h3>
                            <div class="tourText">
                                <p>
                                    It is easy to create and manage multiple events. Add the Funeral Service and include
                                    the date, time, location and other relevant information. Choose to make the event
                                    public or private. You can also easily email online invitations to the event and
                                    ask guests to RSVP on the memorial.
                                </p>
                            </div>
                            <div class="tourSmallImage">
                                <a href="javascript: openwindow('tour_EVENT_Fullsize.jpg')">
                                    <img alt="Event image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/tour_EVENT_small.png" />
                                </a>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="tourBlueBox">
                            <div class="imageSect">
                                <a>
                                    <img src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/tour_FACEBOOK_small.png" alt="facebook" />
                                </a>
                            </div>
                            <div class="textSect">
                                <h3>
                                    Facebook Integration
                                </h3>
                                <p>
                                    Your
                                    <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                                    is integrated with Facebook, which makes it easy for you to share the online memorial
                                    website with your Facebook friends. You can log in to Your
                                    <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                                    safely and securely in one simple step using your Facebook account. When you add
                                    content to a memorial you will be prompted if you want to share your post on your
                                    wall. Sharing on your Facebook wall keeps your Facebook friends up-to-date with
                                    what’s happening on Your
                                    <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>.
                                </p>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="adSpacer20">
                            &nbsp;</div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="leftSideTourBlock">
                            <h3>
                                Add Notes (Pages)
                            </h3>
                            <div class="tourText">
                                <p>
                                    Notes can be used like a blog to inform friends and family of important information.
                                    A note can also be used like an extra page on a website. Use the note to include
                                    links, graphics and other information. The most recent note will appear on the memorial's
                                    homepage.
                                </p>
                            </div>
                            <div class="tourSmallImage">
                                <a href="javascript: openwindow('tour_NOTES_Fullsize.jpg')">
                                    <img alt="Notes image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/tour_NOTES_small.png" />
                                </a>
                            </div>
                        </div>
                        <div class="rightSideTourBlock">
                            <h3>
                                Create Photo Albums
                            </h3>
                            <div class="tourText">
                                <p>
                                    You, your friends, and family, can create albums and add photos to the online memorial.
                                    You can also add a title and description to your photos and albums. We store the
                                    originals of every photo uploaded and have a slideshow feature to make it easy to
                                    view your photos.
                                </p>
                            </div>
                            <div class="tourSmallImage">
                                <a href="javascript: openwindow('tour_PHOTOS_Fullsize.jpg')">
                                    <img alt="Photo Albums image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/tour_PHOTOS_small.png" />
                                </a>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div id="div1" class="tryTributeBoxskyBlue-MT rounded">
                            <div class="rightTribute">
                                <p class="gettingstartedBlue">
                                    <a href="<%=Session["APP_BASE_DOMAIN"]%>pricing.aspx">Create
                                        <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%></a>
                                </p>
                            </div>
                            <div class="leftTribute">
                                <h2>
                                    Create an online memorial for free!</h2>
                                <p>
                                    Get started in seconds, no credit card, no commitment.</p>
                            </div>
                        </div>
                        <div class="leftSideTourBlock">
                            <h3>
                                Receive Messages
                            </h3>
                            <div class="tourText">
                                <p>
                                    There are a number of ways your friends and family can leave their condolences and
                                    memories on the memorial website. They can leave a personal message in the online
                                    guestbook or give a virtual gift. They can also add comments to all photos, videos
                                    and notes on the memorial.
                                </p>
                            </div>
                            <div class="tourSmallImage">
                                <a href="javascript: openwindow('tour_MESSAGE_Fullsize.jpg')">
                                    <img alt="Messages image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/tour_MESSAGE_small.png" />
                                </a>
                            </div>
                        </div>
                        <div class="rightSideTourBlock">
                            <h3>
                                Share the Memorial
                            </h3>
                            <div class="tourText">
                                <p>
                                    It is easy to share an online memorial with your friends and family. Each page of
                                    the memorial website has a variety of sharing options under "<%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                    Tools". You can email, share on Facebook, share on Twitter, or bookmark and share
                                    on a variety of other popular websites.
                                </p>
                            </div>
                            <div class="tourSmallImage">
                                <a href="javascript: openwindow('tour_SHARE_Fullsize.jpg')">
                                    <img alt="Share the memorial image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/tour_SHARE_small.png" />
                                </a>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="adSpacer30">
                            &nbsp;</div>
                        <div class="yt-PricingAddText">
                            <div class="examplesText">
                                <h3 class="Purple-MT">
                                    Choose a type of Online Memorial website</h3>
                            </div>
                            <p class="textunderPricing NewTextColor-MT">
                                An Your Tribute has two types of online memorials that can be created to honor a
                                loved one. A free online obituary is a simple and easy way to produce an online
                                memorial in remembrance of a person. The online obituary includes the notice of
                                death as well as a photo and biographical details of the person. Included are the
                                guestbook and memorial features, which allow users to leave condolences and sympathy
                                messages for the family. Also included is the ability to share photos and videos,
                                which are features not typically included with free online memorials.
                            </p>
                            <br />
                            <p class="textunderPricing NewTextColor-MT">
                                For a more personalized online memorial, upgrade to a premium memorial website.
                                A memorial website includes the same features as an online obituary, plus a number
                                of other premium options. Create a permanent tribute to the life of your loved one
                                that will remain online for generations of family to see. With a premium memorial
                                website users can upload high-resolution photos and videos to the website and invite
                                friends and family to do the same. Photo albums can be viewed and shared online
                                and also saved to your computer to create an offline archive. A premium online memorial
                                also includes designer themes, unlimited content, no storage limits and more.
                            </p>
                            <br />
                            <p class="textunderPricing NewTextColor-MT">
                                View the features page for a complete list of features that are included with a
                                Your Tribute online memorial. Or, click the "get started" link to begin creating
                                an obituary or memorial to celebrate the life of a loved one. Creating an online
                                memorial is easy to setup and will be appreciated by everyone who knew the deceased
                                person.
                            </p>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="bigButtonsOnFeatures">
                            <div class="bottomRightButtons">
                                <a href="<%=Session["APP_BASE_DOMAIN"]%>pricing.aspx" class="bottomButtonPricingLeft">
                                    Pricing</a> <a href="<%=Session["APP_BASE_DOMAIN"]%>features.aspx" class="bottomButtonSeeAllFeatures">
                                        See All Features</a>
                            </div>
                        </div>
                    </div>
                    <!--ADogra ends here-->
                    <!--yt-ContentPrimary-->
                </div>
                <!--Commented by ADogra-->
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
    <div class="yt-Container yt-Tour" id="yourmoments" runat="server">
        <uc:Header ID="Header1" Section="tribute" NavigationName="Tour" runat="server" />
        <div class="hack-clearBoth">
        </div>
        <div class="yt-Breadcrumbs">
            <a href="<%=Session["APP_BASE_DOMAIN"]%>">Home</a> <span class="selected">Tour</span>
        </div>
        <div class="yt-ContentContainer">
            <div class="yt-InnerContentContainer">
                <div class="yt-ContentPrimaryContainer">
                    <div class="yt-Panel-Primary">
                        <h1 class="yt-BiggestHeading darkBlue">
                            TOUR
                        </h1>
                        <div class="tourIntro">
                            <p>
                                Your
                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                                is a web-based tool, that lets you set up a free personal website (a
                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>)
                                to plan, share and remember a significant event or special someone. Create a
                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                for a Wedding, Baby, Birthday, Anniversary, Graduation or Memorial.
                            </p>
                            <br />
                            <p>
                                Simple configuration means that creating a
                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                for your event can be completed in minutes. Easy customization allows you to match
                                your
                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                                to your event’s look and feel. Powerful tools allow you to add custom pages, upload
                                photographs, send online invitations, and more, with very little effort.
                            </p>
                            <br />
                            <p>
                                Click the “Create a
                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>”
                                tab to<br />
                                get started for free, or continue
                                <br />
                                to learn more about Your
                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>.
                            </p>
                        </div>
                        <div class="tourMainVideo">
                            <img src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/videoPaceholder.jpg" alt="tour video" />
                        </div>
                        <div class="createTributeFloatingButtonDiv">
                            <a class="createTributeFloatingButton" href="<%=Session["APP_BASE_DOMAIN"]%>pricing.aspx">
                                Create
                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%></a>
                        </div>
                        <%--<div class="hack-clearBoth"></div>
                            <div class="adSpacer30">&nbsp;</div>--%>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="leftSideTourBlock">
                            <h3>
                                Create a
                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                            </h3>
                            <div class="tourText">
                                <p>
                                    Create a free
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                    for your event in a few simple steps – no obligation and no credit card required.
                                    Select the type of event, name your
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                    and choose a personal URL. Next, add your
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                    details, photo, and choose your privacy settings. Finally, choose a package, and
                                    click Create. That’s it!
                                </p>
                            </div>
                            <div class="tourSmallImage">
                                <a href="javascript: openwindow('tour_CREATE_Fullsize.jpg')">
                                    <img alt="story image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/tour_CREATE_small.png" />
                                </a>
                            </div>
                        </div>
                        <div class="rightSideTourBlock">
                            <h3>
                                Choose a Theme
                            </h3>
                            <div class="tourText">
                                <p>
                                    Personalize your
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                    with one of our designer themes. Multiple themes are available for each type of
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                    with new themes being added all the time! You can switch your theme any time, from
                                    any of your pages, using the "Change Theme" link. Live preview lets you see what
                                    the theme will look like.
                                </p>
                            </div>
                            <div class="tourSmallImage">
                                <a href="javascript: openwindow('tour_THEME_Fullsize.jpg')">
                                    <img alt="story image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/tour_THEME_small.png" />
                                </a>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="leftSideTourBlock">
                            <h3 class="altTour">
                                Write your Story
                            </h3>
                            <div class="tourText">
                                <p>
                                    The Story page includes personal details such as the
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                    name and photo, location, date and more. These are set up during
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                    creation, but can be updated at any time. We recommend you start by adding to this
                                    page, it could include important dates, milestones, history, places, trivia, fun
                                    facts and more.
                                </p>
                            </div>
                            <div class="tourSmallImage">
                                <a href="javascript: openwindow('tour_STORY_Fullsize.jpg')">
                                    <img alt="story image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/tour_STORY_small.png" />
                                </a>
                            </div>
                        </div>
                        <div class="rightSideTourBlock">
                            <h3 class="altTour">
                                Create an Event
                            </h3>
                            <div class="tourText">
                                <p>
                                    It is easy to create and manage multiple events on your
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>.
                                    Add standard event details such as date, time, location, organizer, description
                                    and more. Make your event public or private and optionally ask for meal preferences.
                                    You can view and manage your guest list online and even export it to a spreadsheet.
                                </p>
                            </div>
                            <div class="tourSmallImage">
                                <a href="javascript: openwindow('tour_EVENT_Fullsize.jpg')">
                                    <img alt="story image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/tour_EVENT_small.png" />
                                </a>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="tourBlueBox">
                            <div class="imageSect">
                                <a href="javascript: openwindow('stylishinvitations.jpg')">
                                    <img src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/tour_INVITATIONS_small.png"
                                        alt="stylish invitation" />
                                </a>
                            </div>
                            <div class="textSect">
                                <h3>
                                    Stylish Online Invitations
                                </h3>
                                <p>
                                    Send beautiful online invitations, for FREE, from your
                                    <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                                    . Choose from one of our numerous stylish designs and add a custom message to your
                                    online invitation. All invitations include event details, a link to your
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                    and a link to RSVP to your event. Adding your contacts is a breeze. Manually add
                                    email addresses or import contacts from Hotmail, Gmail, AOL or Yahoo. You can also
                                    connect to Facebook and invite your Facebook friends to your event. Add more contacts
                                    and send additional free online invitations any time!
                                </p>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="adSpacer20">
                            &nbsp;</div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="leftSideTourBlock">
                            <h3>
                                Add Notes (Pages)
                            </h3>
                            <div class="tourText">
                                <p>
                                    Notes can be used like a blog and are a great way for you to inform visitors to
                                    your
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                    of important thoughts and updates. A Note can also be used like a page on a website.
                                    Use the Note to include links, graphics and other info. The most recent note will
                                    be featured on your
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>'s
                                    homepage.
                                </p>
                            </div>
                            <div class="tourSmallImage">
                                <a href="javascript: openwindow('tour_NOTES_Fullsize.jpg')">
                                    <img alt="story image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/tour_NOTES_small.png" />
                                </a>
                            </div>
                        </div>
                        <div class="rightSideTourBlock">
                            <h3>
                                Create Photo Albums
                            </h3>
                            <div class="tourText">
                                <p>
                                    Add an unlimited number of photo albums and photos to your
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                    (your friends and family can contribute their photos as well). You can add a title
                                    and description to every photo and album. We store the original of each photo uploaded
                                    and have a slideshow feature to make it easy to view your photos.
                                </p>
                            </div>
                            <div class="tourSmallImage">
                                <a href="javascript: openwindow('tour_PHOTOS_Fullsize.jpg')">
                                    <img alt="story image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/tour_PHOTOS_small.png" />
                                </a>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="hack-clearBoth">
                            &nbsp;</div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="leftSideTourBlock">
                            <h3 class="altTour">
                                Receive Messages
                            </h3>
                            <div class="tourText">
                                <p>
                                    There are a number of ways your friends and family can contribute to your
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>.
                                    Personal messages can be left in your online guestbook. They can give you one of
                                    a variety of virtual gifts and include a short personal message. They can also leave
                                    comments on your photos, videos and notes.
                                </p>
                            </div>
                            <div class="tourSmallImage">
                                <a href="javascript: openwindow('tour_MESSAGE_Fullsize.jpg')">
                                    <img alt="story image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/tour_MESSAGE_small.png" />
                                </a>
                            </div>
                        </div>
                        <div class="rightSideTourBlock">
                            <h3 class="altTour">
                                Share your
                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                            </h3>
                            <div class="tourText">
                                <p>
                                    The best way to announce and share your
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                    is with an eCard. Send one by clicking "Share This
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>"
                                    on your
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                    homepage. Each page in your
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                    also has a variety of sharing options under "Tribute Tools". You can Share on Facebook,
                                    Twitter, or on a variety of other popular websites.
                                </p>
                            </div>
                            <div class="tourSmallImage">
                                <a href="javascript: openwindow('tour_SHARE_Fullsize.jpg')">
                                    <img alt="story image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/tour_SHARE_small.png" />
                                </a>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="tourBlueBox">
                            <div class="imageSect">
                                <a href="javascript: openwindow('facebookintegration.jpg')">
                                    <img src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/tour_FACEBOOK_small.png" alt="facebook" />
                                </a>
                            </div>
                            <div class="textSect">
                                <h3>
                                    Facebook Integration
                                </h3>
                                <p>
                                    Your
                                    <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                                    is integrated with Facebook, which makes it easy for you to share your
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                    with your Facebook friends. You can log in to Your
                                    <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                                    safely and securely in one simple step using your Facebook account. When you add
                                    content to a
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                    you will be asked if you want to share your post on your wall. Sharing on your Facebook
                                    wall keeps your Facebook friends up-to-date with what’s happening on your
                                    <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>. When you create
                                    an event you can even invite your Facebook friends and manage their RSVP’s in your
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>.
                                </p>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="adSpacer30">
                            &nbsp;</div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="bigButtonsOnFeatures">
                            <a href="<%=Session["APP_BASE_DOMAIN"]%>pricing.aspx" class="createTributeInSeconds">
                                Create a
                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%></a>
                            <div class="bottomRightButtons">
                                <a href="<%=Session["APP_BASE_DOMAIN"]%>pricing.aspx" class="bottomButtonPricingLeft">
                                    Pricing</a> <a href="<%=Session["APP_BASE_DOMAIN"]%>features.aspx" class="bottomButtonSeeAllFeatures">
                                        See All Features</a>
                            </div>
                        </div>
                    </div>
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
        <uc:Footer ID="Footer2" runat="server" />
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
       // channelUrl  : 'http://www.yourdomain.com/channel.html', // Custom Channel URL
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
