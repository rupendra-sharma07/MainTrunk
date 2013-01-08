<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContactYourTribute.aspx.cs"
    Inherits="Miscellaneous_ContactYourTribute" Title="ContactYourTribute" %>

<%@ Register Src="~/UserControl/YourTributeHeader.ascx" TagName="Header" TagPrefix="uc" %>
<%@ Register Src="~/UserControl/FooterHome.ascx" TagName="Footer" TagPrefix="uc" %>
<%@ Register Src="../UserControl/LeftFeaturedPanel.ascx" TagName="LeftFeaturedPanel"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:fb="http://www.facebook.com/2008/fbml"
xml:lang="en" lang="en">
<head runat="server">
    <title>Contact - Your Tribute</title>
    <meta name="description" content="Your Tribute provides premium permanent online memorials. Create a free obituary or memorial tribute website quickly and easily." />
    <meta name="keywords" content="permanent online memorials, memorial tribute website, premium, free obituary, memorial tribute, your tribute" />
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Content-Language" content="en-ca" />
    <meta http-equiv="imagetoolbar" content="false" />
    <meta name="robots" content="index,follow" />
    <meta name="MSSmartTagsPreventParsing" content="true" />
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

    <script src="https://connect.facebook.net/en_US/all.js" type="text/javascript"></script>

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
    <form id="Form1" action="" runat="server">
    <div id="divShowModalPopup">
    </div>
    <div class="yt-Container yt-Tour">
        <uc:Header ID="ytHeader" Section="tribute" NavigationName="None" runat="server" />
        <div class="hack-clearBoth">
        </div>
        <div class="yt-Breadcrumbs">
            <a href="<%=Session["APP_BASE_DOMAIN"]%>">Home</a> <span class="selected">Contact Your
                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
            </span>
        </div>
        <%if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments") %>
        <% {%>
        <div class="yt-ContentContainerInner">
            <div class="yt-ContentPrimaryContainer">
                <div class="yt-ContentPrimary">
                    <div class="yt-Panel-Primary">
                        <h2>
                            Contact Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                        </h2>
                        <br />
                        <p>
                            <b>Customer Service</b><br />
                            Our customer service representatives will be happy to assist you with your order
                            or help answer any questions you may have. If you require assistance, the primary
                            <%--way of contacting us should be through our online <a href="javascript:void(0);" onclick="OpenContactUS();">--%>
                            way of contacting us should be through our <a target="_blank" href="http://support.yourmoments.com/anonymous_requests/new">
                                online Contact form</a> . Please be aware that we do not provide technical support
                            via the phone, fax, or in person.
                            <br />
                            <br />
                            Note that most technical support questions can be answered immediately using the
                            <%-- <a href="javascript:void(0);" onclick="doModalHelp();">Help</a> link at the bottom--%>
                            <a target="_blank" href="http://support.yourmoments.com">Help</a> link at the bottom
                            of every page.
                        </p>
                        <br />
                        <p>
                            <b>Support Hours: </b>
                            <br />
                            Monday – Sunday (9am – 9pm EST)
                            <br />
                            <br />
                            <b>Telephone:</b><br />
                            866-942-7359 (toll-free)
                            <br />
                            877-942-7359 (fax)
                            <br />
                            702-942-7359 (international)
                            <br />
                            <br />
                            <b>Mailing Address:</b><br />
                            2875 North Lamb Blvd, Bldg 8, Las Vegas, Nevada, 89115 (USA)
                            <br />
                            6952 Greenwood Street, Burnaby, BC, V5A 1X8 (Canada)
                        </p>
                        <p>
                            <b>Feedback</b><br />
                            We hope that your experience with Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                            was a positive one. We would greatly appreciate any feedback you can provide us.
                            Please email all feedback to <a href='mailto:feedback@yourmoments.com'>feedback@yourmoments.com.</a>
                            <br />
                            <br />
                            <b>Press Inquiries</b>
                            <br />
                            For all press and media inquiries, please email <a href='mailto:press@yourmoments.com'>
                                press@yourmoments.com.</a>
                            <br />
                            <br />
                            <b>Partnership Inquiries</b>
                            <br />
                            For partnership opportunities with Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, please email
                            <a href='mailto:partners@yourmoments.com'>partners@yourmoments.com.</a>
                            <br />
                            <br />
                            <b>Advertising Opportunities</b>
                            <br />
                            For advertising opportunities with Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, please email
                            <a href='mailto:advertising@yourmoments.com'>advertising@yourmoments.com.</a>
                            <br />
                            <br />
                            <b>Employment Opportunities</b><br />
                            For information about employment opportunities with Your
                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, please email
                            <a href='mailto:careers@yourmoments.com'>careers@yourmoments.com.</a>
                        </p>
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
                    <uc1:LeftFeaturedPanel ID="LeftFeaturedPanel1" runat="server" />
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
        <%}
          else
          { %>
        <div class="yt-ContentContainer">
            <div class="yt-ContentContainerInner">
                <div class="yt-ContentPrimaryContainer">
                    <div class="yt-ContentPrimary">
                        <div class="yt-Panel-ContactPrimary yt-Panel-ContactPrimary-about">
                            <h2>
                                Contact Your Tribute
                            </h2>
                            <div class="yt-MarginPadding">
                                <div>
                                    <h1>
                                        Customer Service</h1>
                                    <div class="yt-MarginMinus5">
                                        Our customer service representatives will be happy to assist you with your order
                                        or help answer any questions you may have. If you require assistance, the primary
                                        <%--way of contacting us should be through our online <a href="javascript:void(0);" onclick="OpenContactUS();">--%>
                                        way of contacting us should be through our online <a target="_blank" href="http://support.yourtribute.com/anonymous_requests/new">
                                            Help Desk</a>.
                                    </div>
                                    <div class="yt-Margintop15">
                                        Note that most technical support questions can be answered immediately using the
                                        <%-- <a href="javascript:void(0);" onclick="doModalHelp();">Help</a> link at the bottom--%>
                                        <a target="_blank" href="http://support.yourtribute.com">Help</a> link at the bottom
                                        of every page.
                                    </div>
                                    <div class="yt-Margintop15">
                                        <b>Support Hours: </b>
                                        <br />
                                        Monday – Friday (9:00am – 11:00pm EST)
                                        <br />
                                        Weekends & Holidays (11:00am – 5:00pm EST)<br />
                                    </div>
                                </div>
                                <div class="yt-ContactMargin">
                                    <h1>
                                        Other Inquiries</h1>
                                    <div class="yt-MarginMinus5">
                                        For technical support, billing and other customer service related issues, please
                                        contact us through our online <a target="_blank" href="http://support.yourtribute.com/anonymous_requests/new">
                                            Help Desk</a>. For other inquiries, please contact us through one of the email
                                        addresses below.
                                    </div>
                                    <div class="yt-Margintop15">
                                        <b>Feedback</b><br />
                                        We hope that your experience with Your Tribute was a positive one. We would greatly
                                        appreciate any feedback you can provide us. Please email all feedback to <a href='mailto:feedback@yourtribute.com'>
                                            feedback@yourtribute.com.</a>
                                    </div>
                                    <div class="yt-Margintop15">
                                        <b>Press Inquiries</b>
                                        <br />
                                        For all press and media inquiries, please email <a href='mailto:press@yourtribute.com'>
                                            press@yourtribute.com.</a>
                                    </div>
                                    <div class="yt-Margintop15">
                                        <b>Partnership Inquiries</b>
                                        <br />
                                        For partnership opportunities with Your Tribute, please email <a href='mailto:partners@yourtribute.com'>
                                            partners@yourtribute.com.</a>
                                    </div>
                                    <div class="yt-Margintop15">
                                        <b>Advertising Opportunities</b>
                                        <br />
                                        For advertising opportunities with Your Tribute, please email <a href='mailto:advertising@yourtribute.com'>
                                            advertising@yourtribute.com.</a>
                                    </div>
                                    <div class="yt-Margintop15">
                                        <b>Employment Opportunities</b><br />
                                        For information about employment opportunities with Your Tribute, please email <a
                                            href='mailto:careers@yourtribute.com'>careers@yourtribute.com.</a>
                                        <br />
                                    </div>
                                </div>
                                <div class="yt-ContactMargin">
                                    <h1>
                                        Mailing Address</h1>
                                    <div class="yt-MarginMinus5">
                                        Please be aware that we do not provide technical support via the phone, fax, or
                                        in person. For customer service related issues, please contact us through our online
                                        <a target="_blank" href="http://support.yourtribute.com/anonymous_requests/new">Help
                                            Desk</a>.</div>
                                    <div class="yt-Margintop15">
                                        <b>Mailing Address </b>
                                        <br />
                                        105 - 6741 Cariboo Road, Burnaby, BC, Canada V3N 4A3
                                    </div>
                                    <div class="yt-Margintop15">
                                        <b>Telephone </b>
                                        <br />
                                        866-942-7359 (toll-free)
                                        <br />
                                        <br />
                                    </div>
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
                                    <li><a href="about.aspx">About Your Tribute</a></li>
                                    <li class="yt-Selected"><a href="javascript:void(0);">Contact</a></li>
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
