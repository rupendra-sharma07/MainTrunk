<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AboutFeatures.aspx.cs" Inherits="Miscellaneous_features"
    Title="AboutFeatures" %>

<%@ Register Src="../UserControl/YourTributeHeader.ascx" TagName="Header" TagPrefix="uc" %>
<%@ Register Src="../UserControl/FooterHome.ascx" TagName="Footer" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:fb="http://www.facebook.com/2008/fbml"
xml:lang="en" lang="en">
<head>
    <title id="featuresTitle" runat="server">Features | What is included with a memorial website - Your Tribute</title>
    
    <meta name="description" content="Memorial websites from Your Tribute include photo sharing, messaging, condolences and more in a personalized easy-to-use interface." />	
<meta name="keywords" content="memorial websites, memorial tribute, photo sharing, messaging, condolences, your tribute" />

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

    <script src="https://connect.facebook.net/en_US/all.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
     App_Domain = "<%=Session["APP_BASE_DOMAIN"]%>";
    </script>

    <!--#include file="../analytics.asp"-->
    <%--12/30/2011 :Client Patch--%>
    <!-- custom -->
    <script type="text/javascript">
    var pkBaseURL = (("https:" == document.location.protocol) ? "https://e-dasher.com/analytics/" : "http://e-dasher.com/analytics/");
    document.write(unescape("%3Cscript src='" + pkBaseURL + "piwik.js' type='text/javascript'%3E%3C/script%3E"));
    </script><script type="text/javascript">
    try {
    var piwikTracker = Piwik.getTracker(pkBaseURL + "piwik.php", 401);
    piwikTracker.trackPageView();
    piwikTracker.enableLinkTracking();
    } catch( err ) {}
    </script><noscript><p><img src="http://e-dasher.com/analytics/piwik.php?idsite=401" style="border:0" alt="" /></p></noscript>
    <!-- End custom Tracking Code -->
    <%--12/30/2011 :Client Patch till here--%>
</head>
<body>
    <form action="" runat="server">
    <div id="divShowModalPopup">
    </div>
    <div class="yt-Container yt-Tour" id="yourtribute" runat="server">
        <uc:Header ID="ytHeader" Section="tribute" NavigationName="Features" runat="server" />
        <div class="hack-clearBoth">
        </div>
        <div class="yt-Breadcrumbs">
            <a href="<%=Session["APP_BASE_DOMAIN"]%>">Home</a> <span class="selected">Features</span>
        </div>
        <div class="yt-ContentContainer">
            <div class="yt-InnerContentContainer">
                <div class="yt-ContentPrimaryContainer">
                    <div class="yt-Panel-Primary-MT">
                        <h1 class="yt-BiggestHeading OceanBlue-MT">
                            FEATURES
                        </h1>
                        <div class="leftSideFeaturesBlock">
                            <img alt="time image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/feature_Time.png"
                                class="featuresImage" />
                            <div class="featuredText">
                                <h3>
                                    Online Permanently
                                </h3>
                                <p>
                                    We don’t think you should have to pay to keep an obituary online for life. The online
                                    obituary, including the Story, Guestbook and Gifts pages will remain online for
                                    life; we guarantee it! Plus, all of the data (photos, text, etc) you have stored
                                    with us is backed up daily.
                                </p>
                            </div>
                        </div>
                        <div class="rightSideFeaturesBlock">
                            <img alt="security image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/feature_Security.png"
                                class="featuresImage" />
                            <div class="featuredText">
                                <h3>
                                    Safe and Secure
                                </h3>
                                <p>
                                    Your personal info and memories are safe and secure with us. We will never provide
                                    your personal information to 3rd parties. Our servers are protected in a high security
                                    building with 24-hour surveillance and fire protection. All account and billing
                                    pages are secured with 128/256 Bit Strong SSL Encryption.
                                </p>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="leftSideFeaturesBlock">
                            <img alt="social image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/feature_Social.png"
                                class="featuresImage" />
                            <div class="featuredText">
                                <h3>
                                    Social Media Integration
                                </h3>
                                <p>
                                    Each page of the online memorial has a variety of sharing options. Share on Facebook,
                                    Send a Tweet, or Bookmark and Share on numerous other websites. You can also login
                                    to Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> with your Facebook account, then invite your friends and share content
                                    on your wall.
                                </p>
                            </div>
                        </div>
                        <div class="rightSideFeaturesBlock">
                            <img alt="theme image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/feature_Themes.png"
                                class="featuresImage" />
                            <div class="featuredText">
                                <h3>
                                    Designer Themes
                                </h3>
                                <p>
                                    Personalize the online memorial website with one of our designer themes. Theme genres
                                    include nature, sky, flowers, religion and much more. New themes are regularly added.
                                    Switch your theme any time, from any of your pages, using the "Change Theme" link.
                                </p>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div id="divTributeBoxBlue" class="tryTributeBoxskyBlue-MT rounded">
                            <div class="rightTribute">
                                <p class="gettingstartedBlue">
                                    <a href="<%=Session["APP_BASE_DOMAIN"]%>pricing.aspx">Create <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%></a>
                                </p>
                            </div>
                            <div class="leftTribute">
                                <h2>
                                    Create an online memorial for free!</h2>
                                <p>
                                    Get started in seconds, no credit card, no commitment.</p>
                            </div>
                        </div>
                        <div class="leftSideFeaturesBlock">
                            <img alt="story image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/featuresStory.png"
                                class="featuresImage" />
                            <div class="featuredText">
                                <h3>
                                    Story
                                </h3>
                                <p>
                                    The story page includes personal details such as the person's name and photo, location,
                                    date and other biographical info. Add more to the story by including important dates,
                                    milestones, history, places, trivia, fun facts and other significant info.
                                </p>
                                <a href="http://memorial.yourtribute.com/evelynmsmith/story.aspx" target="_blank"
                                    class="previewFeatureLink">Preview Feature</a>
                            </div>
                        </div>
                        <div class="rightSideFeaturesBlock">
                            <img alt="guestbook image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/feature_Guestbook.png"
                                class="featuresImage" />
                            <div class="featuredText">
                                <h3>
                                    Guestbook
                                </h3>
                                <p>
                                    The online guestbook feature allows friends and family to post comments, thoughts,
                                    memories, or wishes for everyone to see. The memorial creator and administrators
                                    are notified of new guestbook messages and can delete any they see fit.
                                </p>
                                <a href="http://memorial.yourtribute.com/evelynmsmith/guestbook.aspx" target="_blank"
                                    class="previewFeatureLink">Preview Feature</a>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="leftSideFeaturesBlock">
                            <img alt="events image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/feature_Events.png"
                                class="featuresImage" />
                            <div class="featuredText">
                                <h3>
                                    Events
                                </h3>
                                <p>
                                    Create and manage the memorial related events. Add the event details, make your
                                    event public or private, ask for meal preferences, and invite guests using our contact
                                    importer. Email a stylish online invitation then view and manage RSVP's online.
                                </p>
                                <a href="http://memorial.yourtribute.com/evelynmsmith/events.aspx" target="_blank"
                                    class="previewFeatureLink">Preview Feature</a>
                            </div>
                        </div>
                        <div class="rightSideFeaturesBlock">
                            <img alt="photos image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/feature_Photos.png
" class="featuresImage" />
                            <div class="featuredText">
                                <h3>
                                    Photos
                                </h3>
                                <p>
                                    Create your own photo album or add photos to an existing album. Crop, rotate and
                                    add descriptions to your photos when you upload them. Friends and family can also
                                    add their photos to the memorial. We even store the originals of every photo!
                                </p>
                                <a href="http://memorial.yourtribute.com/evelynmsmith/photos.aspx" target="_blank"
                                    class="previewFeatureLink">Preview Feature</a>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="leftSideFeaturesBlock">
                            <img alt="gifts image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/feature_Gifts.png"
                                class="featuresImage" />
                            <div class="featuredText">
                                <h3>
                                    Gifts
                                </h3>
                                <p>
                                    Gifts are a fun and free way to offer condolences. Friends and family can give free
                                    virtual gifts and optionally leave their name and a short message. There are currently
                                    20 gifts to choose from, with more gifts being added all the time.
                                </p>
                                <a href="http://memorial.yourtribute.com/evelynmsmith/gift.aspx" target="_blank"
                                    class="previewFeatureLink">Preview Feature</a>
                            </div>
                        </div>
                        <div class="rightSideFeaturesBlock">
                            <img alt="videos image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/feature_Videos.png"
                                class="featuresImage" />
                            <div class="featuredText">
                                <h3>
                                    Videos
                                </h3>
                                <p>
                                    Quickly and easily embed your favorite YouTube videos into a memorial. You can add
                                    unlimited videos and add titles and descriptions to each video. We currently
                                    support YouTube videos and will be adding other video sharing services soon.
                                </p>
                                <a href="http://memorial.yourtribute.com/evelynmsmith/videos.aspx" target="_blank"
                                    class="previewFeatureLink">Preview Feature</a>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="leftSideFeaturesBlock">
                            <img alt="notes image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/feature_Notes.png"
                                class="featuresImage" />
                            <div class="featuredText">
                                <h3>
                                    Notes
                                </h3>
                                <p>
                                    Notes can be used like a blog to record your thoughts for friends and family to
                                    see. You can also create notes, like web pages, to add important info and links
                                    related to the memorial. The most recent note is displayed on memorial's homepage.
                                </p>
                                <a href="http://memorial.yourtribute.com/evelynmsmith/notes.aspx" target="_blank"
                                    class="previewFeatureLink">Preview Feature</a>
                            </div>
                        </div>
                        <div class="rightSideFeaturesBlock">
                            <img alt="comments image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/feature_Comments.png"
                                class="featuresImage" />
                            <div class="featuredText">
                                <h3>
                                    Comments
                                </h3>
                                <p>
                                    Comments can be added to notes, photos and videos. Comments are an easy way for
                                    friends and family to leave their thoughts about content on the memorial. The memorial
                                    creator and administrators are notified by email of new comments.
                                </p>
                                <a href="http://memorial.yourtribute.com/evelynmsmith/photo.aspx?PhotoId=12258" target="_blank"
                                    class="previewFeatureLink">Preview Feature</a>
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
                                <h2 id="YT11" runat="server">
                                    Create an online memorial for free!</h2>
                                <p>
                                    Get started in seconds, no credit card, no commitment.</p>
                            </div>
                        </div>
                        <div class="leftSideFeaturesBlock">
                            <img alt="donations image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/feature_Admins.png"
                                class="featuresImage" />
                            <div class="featuredText">
                                <h3>
                                    Multiple Administrators
                                </h3>
                                <p>
                                    It is easy to add friends & family as administrators to a memorial website. An administrator
                                    is able to use all the features of the memorial and can remove content posted by
                                    other users. The memorial creator will always have full control and can add or remove
                                    administrators at any time.
                                </p>
                            </div>
                        </div>
                        <div class="rightSideFeaturesBlock">
                            <img alt="email image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/feature_Email.png"
                                class="featuresImage" />
                            <div class="featuredText">
                                <h3>
                                    Email Notifications
                                </h3>
                                <p>
                                    The memorial creator and administrators will automatically receive an email whenever
                                    new content is added to their memorial. In your privacy settings you can choose
                                    which emails you want to receive. Visitors to the memorial can also choose to receive
                                    email updates when new content is added.
                                </p>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="leftSideFeaturesBlock">
                            <img alt="no banner image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/feature_NoBanner.png"
                                class="featuresImage" />
                            <div class="featuredText">
                                <h3>
                                    No Advertising
                                </h3>
                                <p>
                                    Our free online obituaries include a few tasteful well-targeted banner ads. If you
                                    choose one of our paid packages, all of the banner ads will be removed from the
                                    memorial. Paid memorial websites will never have advertising on them, ever.
                                </p>
                                <p class="Purple-MT">
                                    * Requires a paid account
                                </p>
                            </div>
                        </div>
                        <div class="rightSideFeaturesBlock">
                            <img alt="time image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/features_Album.png"
                                class="featuresImage" />
                            <div class="featuredText">
                                <h3>
                                    Download Photo Albums
                                </h3>
                                <p>
                                    We store the original high resolution image (up to 3 megapixels) of every photo
                                    uploaded. Users who have a premium paid Memorial Tribute account can view the high
                                    resolution images and can also download entire photo albums.
                                </p>
                                <p class="Purple-MT">
                                    * Requires a paid account.
                                </p>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="bigButtonsOnFeatures">
                            <div class="bottomRightButtons">
                                <a href="<%=Session["APP_BASE_DOMAIN"]%>tour.aspx" class="bottomButtonTour">Tour</a>
                                <a href="<%=Session["APP_BASE_DOMAIN"]%>themes.aspx" class="bottomButtonExamples">View
                                    Themes</a>
                            </div>
                        </div>
                    </div>
                    <!--Primary Panel ends here-->
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
                                        <li><a href="contact.aspx">Contact</a></li>

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
                <%--  <div class="yt-ContentContainerImageOnFeatures">
                </div>--%>
                <!--ADogra ends here-->
            </div>
            <!--yt-ContentContainerInner-->
        </div>
        <!--yt-ContentContainer-->
        <uc:Footer ID="Footer1" runat="server" />
        <!--yt-Footer-->
    </div>
    
    <div class="yt-Container yt-Tour" id="Yourmoments" runat="server" >
        <uc:Header ID="Header1" Section="tribute" NavigationName="Features" runat="server" />
        <div class="hack-clearBoth">
        </div>
        <div class="yt-Breadcrumbs">
            <a href="<%=Session["APP_BASE_DOMAIN"]%>">Home</a> <span class="selected">Features</span>
        </div>
        <div class="yt-ContentContainer">
            <div class="yt-InnerContentContainer">
                <div class="yt-ContentPrimaryContainer">
                    <div class="yt-Panel-Primary">
                        <h1 class="yt-BiggestHeading darkBlue">
                            FEATURES
                        </h1>
                        <div class="leftSideFeaturesBlock">
                            <img alt="story image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/featuresStory.png"
                                class="featuresImage" />
                            <div class="featuredText">
                                <h3>
                                    STORY
                                </h3>
                                <p>
                                    The story page includes personal details such as the <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> name and photo, location,
                                    date and other biographical info. Add more to the story by including important dates,
                                    milestones, history, places, trivia, fun facts and other info significant to your
                                    event.
                                </p>
                                <a href="http://wedding.yourmoments.com/jonandmary/story.aspx" target="_blank" class="previewFeatureLink">
                                    Preview Feature</a>
                            </div>
                        </div>
                        <div class="rightSideFeaturesBlock">
                            <img alt="guestbook image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/feature_Guestbook.png"
                                class="featuresImage" />
                            <div class="featuredText">
                                <h3>
                                    GUESTBOOK
                                </h3>
                                <p>
                                    The online guestbook feature allows friends and family to post comments, thoughts,
                                    ideas, memories, or wishes for everyone to see. The <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> creator and administrators
                                    are notified of new guestbook messages and can delete any they see fit.
                                </p>
                                <a href="http://wedding.yourmoments.com/jonandmary/guestbook.aspx" target="_blank"
                                    class="previewFeatureLink">Preview Feature</a>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="leftSideFeaturesBlock">
                            <img alt="events image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/feature_Events.png"
                                class="featuresImage" />
                            <div class="featuredText">
                                <h3 class="altRow">
                                    EVENTS
                                </h3>
                                <p>
                                    Create and manage an unlimited number of events. Add the event details, make your
                                    event public or private, ask for meal preferences, and invite guests using our contact
                                    importer. Email a stylish online invitation then view and manage RSVPs online.
                                </p>
                                <a href="http://wedding.yourmoments.com/jonandmary/events.aspx" target="_blank" class="previewFeatureLink">
                                    Preview Feature</a>
                            </div>
                        </div>
                        <div class="rightSideFeaturesBlock">
                            <img alt="photos image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/feature_Photos.png
" class="featuresImage" />
                            <div class="featuredText">
                                <h3 class="altRow">
                                    PHOTOS
                                </h3>
                                <p>
                                    There is no limit to the number of photos you can upload. Create an unlimited number
                                    of albums and add titles and descriptions to your photos. Your friends and family
                                    can also add their photos to your <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>. We even store the originals of every
                                    photo!
                                </p>
                                <a href="http://wedding.yourmoments.com/jonandmary/photos.aspx" target="_blank" class="previewFeatureLink">
                                    Preview Feature</a>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="leftSideFeaturesBlock">
                            <img alt="gifts image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/feature_Gifts.png"
                                class="featuresImage" />
                            <div class="featuredText">
                                <h3>
                                    GIFTS
                                </h3>
                                <p>
                                    Gifts are fun and free to give. Visitors to your <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> can send you free virtual
                                    gifts and optionally leave their name and a short message. Each <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> type currently
                                    has 20 gifts to choose from, with more gifts being added all the time.
                                </p>
                                <a href="http://wedding.yourmoments.com/jonandmary/gift.aspx" target="_blank" class="previewFeatureLink">
                                    Preview Feature</a>
                            </div>
                        </div>
                        <div class="rightSideFeaturesBlock">
                            <img alt="videos image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/feature_Videos.png"
                                class="featuresImage" />
                            <div class="featuredText">
                                <h3>
                                    VIDEOS
                                </h3>
                                <p>
                                    Quickly and easily embed your favorite YouTube videos into your <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>. You can
                                    add unlimited videos and add titles and descriptions to all of your videos. We currently
                                    support YouTube videos and will be adding other video sharing services soon.
                                </p>
                                <a href="http://wedding.yourmoments.com/jonandmary/videos.aspx" target="_blank" class="previewFeatureLink">
                                    Preview Feature</a>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="leftSideFeaturesBlock">
                            <img alt="notes image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/feature_Notes.png"
                                class="featuresImage" />
                            <div class="featuredText">
                                <h3 class="altRow">
                                    NOTES
                                </h3>
                                <p>
                                    Notes can be used like a blog to record your thoughts for friends and family to
                                    see. You can also create notes, like web pages, to add important info and links
                                    related to your event. The most recent note is displayed on your <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> homepage.
                                </p>
                                <a href="http://wedding.yourmoments.com/jonandmary/notes.aspx" target="_blank" class="previewFeatureLink">
                                    Preview Feature</a>
                            </div>
                        </div>
                        <div class="rightSideFeaturesBlock">
                            <img alt="comments image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/feature_Comments.png"
                                class="featuresImage" />
                            <div class="featuredText">
                                <h3 class="altRow">
                                    COMMENTS
                                </h3>
                                <p>
                                    Comments can be added to notes, photos and videos. Comments are an easy way for
                                    your friends and family to leave their thoughts about content on your <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>. The
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> creator and administrators are notified by email of new comments.
                                </p>
                                <a href="http://newbaby.yourmoments.com/elizabethjohnson/photo.aspx?PhotoId=3543"
                                    target="_blank" class="previewFeatureLink">Preview Feature</a>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="leftSideFeaturesBlock">
                            <img alt="social image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/feature_Social.png"
                                class="featuresImage" />
                            <div class="featuredText">
                                <h3>
                                    SOCIAL NETWORKING
                                </h3>
                                <p>
                                    Each page on your <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> has a variety of sharing options. Share on Facebook, Send
                                    a Tweet, or Bookmark and Share on numerous other websites. Connect to your <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                    using Facebook then invite your friends and share content on your wall.
                                </p>
                            </div>
                        </div>
                        <div class="rightSideFeaturesBlock">
                            <img alt="theme image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/feature_Themes.png"
                                class="featuresImage" />
                            <div class="featuredText">
                                <h3>
                                    DESIGNER THEMES
                                </h3>
                                <p>
                                    Personalize your <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> with one of our designer themes. Multiple themes are available
                                    for each type of <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> with new themes being added all the time. You can switch
                                    your theme any time, and see what it will look like using our live preview function.
                                </p>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="leftSideFeaturesBlock">
                            <img alt="ecard image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/feature_eCards.png"
                                class="featuresImage" />
                            <div class="featuredText">
                                <h3 class="altRow">
                                    FREE ECARDS
                                </h3>
                                <p>
                                    Create a <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> for your event then send an eCard by clicking the "Share <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>"
                                    link on your <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> homepage. Choose from one of our stylish eCards and include
                                    a personal message. Easily add email addresses with our online contact importer.
                                </p>
                            </div>
                        </div>
                        <div class="rightSideFeaturesBlock">
                            <img alt="security image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/feature_Security.png"
                                class="featuresImage" />
                            <div class="featuredText">
                                <h3 class="altRow">
                                    SECURITY &amp; PRIVACY
                                </h3>
                                <p>
                                    We will never provide your personal information to 3rd parties. Our servers are
                                    protected in a high security building with 24-hour surveillance and fire protection.
                                    All account and billing pages are secured with 128/256 Bit Strong SSL Encryption.
                                </p>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="leftSideFeaturesBlock">
                            <img alt="donations image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/feature_Donations.png"
                                class="featuresImage" />
                            <div class="featuredText">
                                <h3>
                                    DONATION MODULE
                                </h3>
                                <p>
                                    When creating a <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> you can choose to accept donations to any US charity. You
                                    will automatically be set up with a donation page for your charity. Visitors to
                                    your <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> can donate to the charity online and you will be notified of all donations
                                    by email.
                                </p>
                            </div>
                        </div>
                        <div class="rightSideFeaturesBlock">
                            <img alt="email image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/feature_Email.png"
                                class="featuresImage" />
                            <div class="featuredText">
                                <h3>
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> EMAIL UPDATES
                                </h3>
                                <p>
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> creators and administrators will automatically receive an email whenever
                                    new content is added to their <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>. In your privacy settings you can choose which
                                    emails you want to receive. Visitors to your <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> can also choose to receive
                                    email updates.
                                </p>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="leftSideFeaturesBlock">
                            <img alt="unlimited image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/feature-Unlimited.png"
                                class="featuresImage" />
                            <div class="featuredText">
                                <h3 class="altRow">
                                    UNLIMITED CONTENT
                                </h3>
                                <p>
                                    There is no limit to the amount of content you, and your friends and family, can
                                    add to your <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>. This includes an unlimited number of photos, photo albums,
                                    videos, guestbook messages, virtual gifts, notes, comments and more.
                                </p>
                            </div>
                        </div>
                        <div class="rightSideFeaturesBlock">
                            <img alt="url image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/feature_URL.png"
                                class="featuresImage" />
                            <div class="featuredText">
                                <h3 class="altRow">
                                    CUSTOM URL
                                </h3>
                                <p>
                                    Each <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> comes with a personalized URL to make it easy to share your <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                    with friends and family. You can optionally purchase your own domain name from us,
                                    such as www.jonmarywedding.com, and we'll show you how to link it to your <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>.
                                </p>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="leftSideFeaturesBlock">
                            <img alt="no banner image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/feature_NoBanner.png"
                                class="featuresImage" />
                            <div class="featuredText">
                                <h3>
                                    NO BANNER ADS
                                </h3>
                                <p>
                                    Our free <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>s include a few tasteful well-targeted banner ads. If you choose
                                    one of our paid packages, all of the banner ads will be removed from your <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>.
                                    Paid <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>s will never have advertising on them, ever!
                                </p>
                            </div>
                        </div>
                        <div class="rightSideFeaturesBlock">
                            <img alt="time image" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/feature_Time.png"
                                class="featuresImage" />
                            <div class="featuredText">
                                <h3>
                                    ONLINE FOR LIFE
                                </h3>
                                <p>
                                    We don’t think you should have to keep paying to keep your important event online.
                                    Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> and all of its content will remain online for life; we guarantee it!
                                    Plus, all of the data (photos, text, etc) you have stored with us is backed up daily!
                                </p>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="bigButtonsOnFeatures">
                            <a href="<%=Session["APP_BASE_DOMAIN"]%>pricing.aspx" class="createTributeInSeconds">
                                Create a <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%></a>
                            <div class="bottomRightButtons">
                                <a href="<%=Session["APP_BASE_DOMAIN"]%>tour.aspx" class="bottomButtonTour">Tour</a>
                                <a href="<%=Session["APP_BASE_DOMAIN"]%>examples.aspx" class="bottomButtonExamples">
                                    View Examples</a>
                            </div>
                        </div>
                    </div>
                    <!--Primary Panel ends here-->
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
                                        <li><a href="contact.aspx">Contact</a></li>

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
