<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AboutUs.aspx.cs" Inherits="Miscellaneous_AboutUs"
    Title="AboutUs" %>

<%@ Register Src="../UserControl/YourTributeHeader.ascx" TagName="Header" TagPrefix="uc" %>
<%@ Register Src="../UserControl/FooterHome.ascx" TagName="Footer" TagPrefix="uc" %>
<%@ Register Src="../UserControl/LeftFeaturedPanel.ascx" TagName="LeftFeaturedPanel"
    TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:fb="http://www.facebook.com/2008/fbml"
xml:lang="en" lang="en">
<head>
    <title>About - Commemorate a life with Memorial Tributes from Your Tribute</title>
    <meta name="description" content="Learn about Your Tribute and how to commemorate a life with Memorial Tributes. Build a Memorial Tribute website to share memories and remember a loved one." />
    <meta name="keywords" content="memorial tributes, memorial tribute, online memorial tributes, online memorial tribute, about your tribute" />
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Content-Language" content="en-ca" />
    <meta http-equiv="imagetoolbar" content="false" />
    <meta name="robots" content="index,follow" />
    <meta name="MSSmartTagsPreventParsing" content="true" />
    <%--<meta name="description" content="Create a personalized website for a wedding, baby, memorial or other special event. Your Tribute offers the best custom websites to celebrate your event. Make an online website free for 30 days with Your Tribute’s personal online website creation service.">
    <meta name="keywords" content="Online Website Builder, Personal Website Creator, Website Creation Service">--%>
    <!-- really basic, generic html class stylesheet -->
    <!-- These url's will work on Remote server. Comment the above urls -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/default.css" />
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/layouts/centered1024_21.css" />
    <link rel="stylesheet" type="text/css" media="print" href="<%=Session["APP_BASE_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/print.css" />
    <link rel="stylesheet" type="text/css" href="<%=Session["APP_BASE_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/ScreenLatest.css" />
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/medium_text.css"
        title="medium_text" />
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/large_text.css"
        title="large_text" />
    <link rel="Shortcut Icon" type="image/x-icon" href="<%=Session["APP_BASE_PATH"]%>assets/images/favicon.ico" />

    <script type="text/javascript" src="<%=Session["APP_BASE_PATH"]%>assets/scripts/mootools-1.11.js"></script>

    <script type="text/javascript" src="<%=Session["APP_BASE_PATH"]%>assets/scripts/global.js"></script>

    <script type="text/javascript" src="<%=Session["APP_BASE_PATH"]%>assets/scripts/styleSwitcher.js"></script>

    <script type="text/javascript" src="<%=Session["APP_BASE_PATH"]%>assets/scripts/modalbox.js"></script>

    <script src="https://connect.facebook.net/en_US/all.js" type="text/javascript"></script>

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
        <uc:Header ID="ytHeader" Section="tribute" NavigationName="None" runat="server" />
        <div class="hack-clearBoth">
        </div>
        <div class="yt-Breadcrumbs">
            <a href="<%=Session["APP_BASE_DOMAIN"]%>">Home</a> <span class="selected">About Your
                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
            </span>
        </div>
        <%if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments") %>
        <% {%>
        <div class="yt-ContentContainer">
            <div class="yt-ContentContainerInner">
                <div class="yt-ContentPrimaryContainer">
                    <div class="yt-ContentPrimary">
                        <div class="yt-Panel-Primary yt-Panel-Primary-about">
                            <h2>
                                About Your
                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%></h2>
                            <br />
                            <p>
                                Your
                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                                brings a new dimension to personal websites by creating a single destination to
                                celebrate life’s significant events online.
                            </p>
                            <br />
                            <p>
                                With Your
                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>.com, users can
                                quickly and easily create a
                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                                website for a wedding, baby, anniversary, birthday, graduation or memorial. Your
                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                                combines many of the features of popular online invitation, photo sharing, blogging,
                                and social networking websites, in an easy-to-use intuitive interface.
                            </p>
                            <br />
                            <p>
                                A
                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                Home provides friends and family a safe and secure environment where they can connect
                                and collaborate on an event. They can record sentiments; convey best wishes and
                                heartfelt stories; send online invitations and RSVP to events; and share photos
                                and videos.
                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                                websites remain online for life to preserve important memories and provide an everlasting
                                record of the special occasion.
                            </p>
                            <br />
                            <p>
                                Your
                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                                is a privately held company based out of Vancouver, BC with a US subsidiary office
                                located in Las Vegas, NV.</p>
                            <br />
                            <br />
                            <br />
                            <h3>
                                <b>Our History</b></h3>
                            <br />
                            <p>
                                Your
                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                                was originally developed to help friends and families commemorate loved one’s lives
                                throughinteractive memorial websites. Our customers loved the website but many wished
                                that they had been able to share their special memories with friends and family
                                before they lost their loved one. This gave us an idea. Why not create a single
                                destination where users could celebrate all of life’s important events online.
                            </p>
                            <br />
                            <p>
                                In 2011 we began design and development of a new website. We kept the Your
                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                                name, but the entire website was rebuilt from the ground up without a single piece
                                of code reused. In January 2009 the new website launched in public beta. The beta
                                period lasted a long 18 months, during which we conducted extensive user testing
                                and market research
                            </p>
                            <br />
                            <p>
                                Finally in October of 2011, Your
                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                                announced the official launch of its new website and expansion into the special
                                events market. Your
                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                                launched with numerous enhancements and feature changes that resulted from the 18-month
                                beta period. With Your
                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                                users can now create
                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>s
                                for all of life’s significant events: births, weddings, anniversaries, graduations
                                and birthdays.
                            </p>
                            <br />
                            <br />
                            <br />
                            <p>
                                For ongoing information about Your
                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, please read
                                our <a href="<%=TributesPortal.Utilities.WebConfig.BlogUrlMain%>">company blog</a>.
                                We appreciate your feedback, so feel free to comment on the blog - or email us your
                                suggestions.
                            </p>
                            <br />
                            <p>
                                To get started with your own
                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>,
                                <a href="<%=Session["APP_BASE_DOMAIN"]%>pricing.aspx">create an account.</a></p>
                        </div>
                    </div>
                    <!--yt-ContentPrimary-->
                </div>
                <!--yt-ContentPrimaryContainer-->
                <div class="yt-ContentSecondary">
                    <div class="yt-Panel yt-Panel-Tools">
                        <div class="yt-Panel-Body">
                            <h2>
                                Your
                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%></h2>
                            <div class="yt-TourLinks">
                                <ul>
                                    <li><a href="about.aspx">About Your
                                        <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%></a></li>
                                    <li class="yt-Selected"><a href="contact.aspx">Contact</a></li>
                                    <li><a href="advertise.aspx">Advertise</a></li>
                                    <li><a href="termsofuse.aspx">Terms of Use</a></li>
                                    <li><a href="privacy.aspx">Privacy Policy</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="yt-Panel">
                        <uc:LeftFeaturedPanel ID="LeftFeaturedPanel1" runat="server" />
                    </div>
                </div>
                <!--yt-ContentSecondary-->
                <div class="hack-clearBoth">
                </div>
                <div class="hack-clearBoth">
                </div>
                <div class="yt-ContentContainerImage">
                </div>
            </div>
            <!--yt-ContentContainerInner-->
        </div>
        <%}
          else
          { %>
        <div class="yt-ContentContainer">
            <div class="yt-ContentContainerInner">
                <div class="yt-ContentPrimaryContainer">
                    <div class="yt-ContentPrimary">
                        <div class="yt-Panel-ContactPrimary yt-Panel-ContactPrimary-about">
                            <h2>
                                About Your Tribute</h2>
                            <div class="yt-MarginPadding">
                                <h1>
                                    Our Company
                                </h1>
                                <div class="yt-MarginMinus5">
                                    Your Tribute was founded in 2002 to provide friends and family with a safe and secure
                                    online environment to connect and share memories. Individuals could use Your Tribute
                                    to create a personalized memorial tribute website to commemorate the life of a loved
                                    one. For the past 10+ years we have been dedicated to helping families create more
                                    than 75,000 memorial tributes to celebrate the life of their departed friends and
                                    family members.
                                </div>
                                <div class="yt-Margintop15">
                                    When Your Tribute started, the options for creating online memorial tributes was
                                    very limited and most families expressed condolences by email or through sympathy
                                    cards. We recognized the need for a website where families could create a personalized
                                    memorial tribute for their loved one that they could share with friends and family
                                    around the world. Memorial tributes allowed families to share condolences online
                                    and also provided an easy way to share photos and other memories.
                                </div>
                                <div class="yt-Margintop15">
                                    Combined, Your Tribute’s founders have more than 50 years experience in the funeral
                                    industry. Our management team’s experience includes manufacturing and distribution
                                    of funeral products, development of funeral technology and executive positions in
                                    funeral associations. Your Tribute remains active in the funeral industry and provides
                                    obituary and video tribute hosting for hundreds of funeral homes throughout North
                                    America.
                                </div>
                                <div class="yt-Margintop15">
                                    It is our goal to provide family and friends with a personalized, easy-to-use memorial
                                    tribute website that they can use to honor a life. Memorializing a loved one is
                                    an important part of the grieving and funeral process. Memorial tributes allow friends
                                    and relatives to connect online in a way they were previously unable. We hope that
                                    Your Tribute will help individuals remember their loved ones and give everyone who
                                    uses our service an opportunity to come together and share fond memories.
                                </div>
                                <div class="yt-ContactMargin">
                                    <h1>
                                        Our Product</h1>
                                    <div class="yt-MarginMinus5">
                                        Your Tribute provides two types of memorial tributes that can be setup to honor
                                        a departed person. Families can create a free online obituary or premium memorial
                                        website for a loved one in minutes. No matter which option you select, it is our
                                        goal to provide you with the highest quality online memorial tribute to honor your
                                        departed friend or family member.
                                    </div>
                                    <div class="yt-Margintop15">
                                        An online obituary is free and easy to setup and we recommend that all families
                                        make an online obituary to create a permanent online record of their loved one’s
                                        life. An obituary includes the person’s photo, biography and death notice. Friends
                                        and relatives can leave condolences on the obituary and share their favorite photos
                                        and videos.
                                    </div>
                                    <div class="yt-Margintop15">
                                        Families who want to create a more personalized and sophisticated memorial tribute
                                        can create a premium memorial website. A memorial website includes the same features
                                        of an online obituary, but is more personalized and includes a number of other premium
                                        options. Users can share high-resolution photos and videos, add unlimited condolences
                                        and memorials, and more. Also, memorial websites can remain online for life to preserve
                                        important memories and provide an everlasting record of the person’s life.
                                    </div>
                                    <div class="yt-Margintop15">
                                        Celebrate a life and remember a special someone with memorial tributes from Your
                                        Tribute. Choose to create a free online obituary or premium memorial website in
                                        a few simple steps. Share their story, add photos and videos, leave condolences
                                        and memorials, and then invite friends and family to do the same.<a href="<%=Session["APP_BASE_DOMAIN"]%>pricing.aspx">Click
                                            here to create a memorial tribute for your loved one.</a>
                                    </div>
                                </div>
                                <div class="yt-ContactMargin">
                                    <h1>
                                        Our Blog
                                    </h1>
                                    <div class="yt-MarginMinus5">
                                        For ongoing information about Your Tribute, please read our <a href="http://blog.yourtribute.com/"
                                            target="_blank">company blog</a>. We appreciate your feedback, so feel free
                                        to comment on the blog - or email us your suggestions.
                                    </div>
                                    <br />
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--yt-ContentPrimary-->
                </div>
                <!--yt-ContentPrimaryContainer-->
                <div class="yt-ContentSecondary">
                    <div class="yt-Panel yt-Panel-Tools">
                        <div class="yt-Panel-Body">
                            <h2>
                                Your Tribute</h2>
                            <div class="yt-TourLinks">
                                <ul>
                                    <li class="yt-Selected"><a href="javascript:void(0);">About Your Tribute</a></li>
                                    <li><a href="contact.aspx">Contact</a></li>
                                    <li><a href="advertise.aspx">Advertise</a></li>
                                    <li><a href="termsofuse.aspx">Terms of Use</a></li>
                                    <li><a href="privacy.aspx">Privacy Policy</a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                </div>
                <!--yt-ContentSecondary-->
                <div class="hack-clearBoth">
                </div>
                <div class="hack-clearBoth">
                </div>
                <div class="yt-ContentContainerImage">
                </div>
            </div>
            <!--yt-ContentContainerInner-->
        </div>
        <%} %>
        <uc:Footer ID="Footer1" runat="server" />
        <!--</div>-->
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
