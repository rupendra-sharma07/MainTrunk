<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pricing.aspx.cs" Inherits="Miscellaneous_Pricing"
    Title="Pricing" %>

<%@ Register Src="../UserControl/YourTributeHeader.ascx" TagName="Header" TagPrefix="uc" %>
<%@ Register Src="../UserControl/FooterHome.ascx" TagName="Footer" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:fb="http://www.facebook.com/2008/fbml"
xml:lang="en" lang="en">
<head>
    <title id="pricingTitle" runat="server">Sign Up - Create a Memorial or Make an Obituary
        with Your Tribute</title>
    <%--<meta name="description" content="Create a permanent free online obituary or personalized memorial tribute website for a loved one in minutes. " />
    <meta name="keywords" content="online memorial, free obituary, memorial tribute, free trial, permanent memorial, free memorial, your tribute" />--%>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Content-Language" content="en-ca" />
    <meta http-equiv="imagetoolbar" content="false" />
    <meta name="robots" content="index,follow" />
    <meta name="MSSmartTagsPreventParsing" content="true" />
    <meta name="description" content="View pricing and sign up to Make an Obituary or Create a Memorial with Your Tribute. It is easy to Create an Obituary or Make a Memorial for a loved one." />
    <meta name="keywords" content="make an obituary, create an obituary, create a memorial, make a memorial, your tribute pricing, your tribute sign up" />
    <!-- really basic, generic html class stylesheet -->
    <!-- These url's will work on Remote server. Comment the above urls -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"] %>/default.css" />
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"] %>/layouts/centered1024_21.css" />
    <link rel="stylesheet" type="text/css" media="print" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"] %>/print.css" />
    <link rel="stylesheet" type="text/css" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"] %>/ScreenLatest.css" />
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"] %>/medium_text.css"
        title="medium_text" />
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"] %>/large_text.css"
        title="large_text" />
    <link rel="Shortcut Icon" type="image/x-icon" href="<%=Session["APP_BASE_DOMAIN"]%>assets/images/favicon.ico" />

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/mootools-1.11.js"></script>

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/global.js"></script>

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/styleSwitcher.js"></script>

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/modalbox.js"></script>

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
        <uc:Header ID="ytHeader" Section="tribute" NavigationName="Pricing" runat="server" />
        <div class="hack-clearBoth">
        </div>
        <div class="yt-Breadcrumbs">
            <a class="NewTextColor-MT" href="<%=Session["APP_BASE_DOMAIN"]%>">Home</a> <span
                class="selected NewTextColor-MT">Pricing & Sign Up </span>
        </div>
        <div class="yt-ContentContainer">
            <div class="yt-InnerContentContainer LightGreyBG-MT">
                <div class="yt-ContentPrimaryContainer">
                    <div class="yt-Panel-Primary">
                        <h3 class="yt-BoldHeading darkBlue">
                            <div class="OceanBlue-MT">
                                Pricing & Sign Up</div>
                        </h3>
                        <%if (ConfigurationManager.AppSettings["ApplicationType"].ToLower() == "yourmoments") %>
                        <%{ %>
                        <!--Commented by LHK; change on 3:35 PM 9/8/2011 for YM-->
                        <p class="textunderPricing NewTextColor-MT">
                            Creating a Website for your event is quick and easy! Choose the "Announce" package
                            to try all features free for 30-day - no credit card required! You can upgrade your
                            Website to a higher package at any time.</p>
                        <%}
                          else
                          { %>
                        <div class="examplesText">
                            <h3 class="Purple-MT">
                                Create a Memorial with Your Tribute</h3>
                        </div>
                        <p class="textunderPricing NewTextColor-MT">
                            It is quick and easy to create a memorial website or make an obituary for your loved
                            one. Your Tribute provides family and friends a private, safe and secure environment
                            to connect and share memories. With Your Tribute when you create a memorial website
                            it can remain online forever to provide an everlasting record of the person's life.
                            Or, make an obituary to simply notify friends and family of the death and receive
                            condolences. Select a package below to create a memorial to pay tribute to your
                            loved one.</p>
                        <%} %>
                        <br />
                        <div id="divBusinessUser" runat="server" visible="false">
                            <table cellspacing="0" cellpadding="0" class="alignTable NewTextColor-MT">
                                <tr>
                                    <td class="brownHeading PurpleBG-MT PurpleBorder-YM RightEdgesWhite-YM" style="text-align: left;
                                        padding: 0px 0px 0px 18px;">
                                        <p>
                                            Features</p>
                                    </td>
                                    <td class="brownHeading PurpleBG-MT PurpleEdges-YM">
                                        <%if (ConfigurationManager.AppSettings["ApplicationType"].ToLower() == "yourmoments") %>
                                        <%{ %>
                                        Announcement
                                        <%}
                                          else
                                          { %>
                                        FREE OBITUARY
                                        <%} %>
                                    </td>
                                    <td class="brownHeading PurpleBG-MT PurpleEdges-YM">
                                        <p>
                                            <%if (ConfigurationManager.AppSettings["ApplicationType"].ToLower() == "yourmoments") %>
                                            <%{ %>
                                            Photo Announcement
                                            <%}
                                              else
                                              { %>
                                            PREMIUM OBITUARY
                                            <%} %>
                                        </p>
                                    </td>
                                    <td class="brownHeading PurpleBG-MT PurpleBorder-YM">
                                        <%if (ConfigurationManager.AppSettings["ApplicationType"].ToLower() == "yourmoments") %>
                                        <%{ %>
                                        WEBSITE
                                        <%}
                                          else
                                          { %>
                                        MEMORIAL TRIBUTE
                                        <%} %>
                                    </td>
                                </tr>
                                <tr>
                                </tr>
                                <tr class="topBorderOnly">
                                    <td class="leftOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            Designer Theme</p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                </tr>
                                <tr style="height: 10px">
                                    <td class="leftOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            Story (Obituary, Photo, Biography, etc)</p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            Guestbook (Unlimited Messages)</p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT ">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            Free Virtual Gifts
                                        </p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            Social Sharing Tools</p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p style="vertical-align: bottom; text-align: center;" class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            Events (Online Invitations & RSVP)
                                        </p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            Photo Albums
                                        </p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            (2 Albums)
                                        </p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            (2 Albums)
                                        </p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            Videos
                                        </p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            Notes/Pages
                                        </p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            View/Download High Resolution Photos
                                        </p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            -</p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            -</p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            Unlimited Storage
                                        </p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            -</p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            -</p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            Custom URL
                                        </p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            -</p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            -</p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            No Advertising
                                        </p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            -</p>
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                    </td>
                                    <td class="rightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftOfTableRow NewTextColor-MT TopBoldBorder">
                                        <p id="secondLastRow NewTextColor-MT">
                                            <div class="Font15 pricingMargin">
                                                PRICE (1 YEAR)</div>
                                        </p>
                                    </td>
                                    <td class="rightOfTableRowLast NewTextColor-MT TopBoldBorder">
                                        <p id="secondLastRow Font15 pricingMargin">
                                            -</p>
                                    </td>
                                    <td class="rightOfTableRowLast NewTextColor-MT TopBoldBorder">
                                        <p id="secondLastRow Font15 pricingMargin">
                                            <asp:Label ID="lblPhotoYearlyAmount" runat="server"></asp:Label>
                                        </p>
                                    </td>
                                    <td class="rightOfTableRowLast NewTextColor-MT TopBoldBorder">
                                        <p id="secondLastRow Font15 pricingMargin">
                                            <asp:Label ID="lblTributeYearlyAmount" runat="server"></asp:Label>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftOfTableRow NewTextColor-MT Font15 TopDottedBorder">
                                        <h4>
                                            <strong>
                                                <div class="pricingMargin">
                                                    PRICE (LIFETIME)</div>
                                            </strong>
                                        </h4>
                                    </td>
                                    <td class="rightOfTableRowLast NewTextColor-MT Font15 TopDottedBorder">
                                        <h4>
                                            <strong>
                                                <div class="pricingMargin">
                                                    FREE</div>
                                            </strong>
                                        </h4>
                                    </td>
                                    <td class="rightOfTableRowLast NewTextColor-MT Font15 TopDottedBorder">
                                        <h4>
                                            <strong>
                                                <div class="pricingMargin">
                                                    <asp:Label ID="lblPhotoLifeTimeAmount" runat="server"></asp:Label></div>
                                            </strong>
                                        </h4>
                                    </td>
                                    <td class="rightOfTableRowLast NewTextColor-MT Font15 TopDottedBorder">
                                        <h4>
                                            <strong>
                                                <div class="pricingMargin">
                                                    <asp:Label ID="lblTributeLifeTimeAmount" runat="server"></asp:Label></div>
                                            </strong>
                                        </h4>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td class="rightOfTableRowLastSignUp-MT">
                                        <a class="selectButton" href="<%=Session["APP_BASE_DOMAIN"]%>Create.aspx?AccountType=1<%=AppendString%>">
                                            Select</a>
                                    </td>
                                    <td class="rightOfTableRowLastSignUp-MT">
                                        <a class="selectButton" href="<%=Session["APP_BASE_DOMAIN"]%>Create.aspx?AccountType=2<%=AppendString%>">
                                            Select</a>
                                    </td>
                                    <td class="rightOfTableRowLastSignUp-MT">
                                        <a class="selectButton" href="<%=Session["APP_BASE_DOMAIN"]%>Create.aspx?AccountType=3<%=AppendString%>">
                                            Select</a>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divPersonalUser" runat="server" visible="true" style="margin-left: 6px;">
                            <table cellspacing="0" cellpadding="0" class="alignTable NewTextColor-MT">
                                <tr>
                                    <td class="brownHeading PurpleBG-MT PurpleBorder-YM RightEdgesWhite-YM" style="text-align: left;
                                        padding: 0px 0px 0px 18px;">
                                        <p>
                                            Features</p>
                                    </td>
                                    <td class="brownHeading PurpleBG-MT PurpleEdges-YM">
                                        <%if (ConfigurationManager.AppSettings["ApplicationType"].ToLower() == "yourmoments") %>
                                        <%{ %>
                                        Announcement
                                        <%}
                                          else
                                          { %>
                                        FREE OBITUARY
                                        <%} %>
                                    </td>
                                    <td class="brownHeading PurpleBG-MT PurpleBorder-YM">
                                        <%if (ConfigurationManager.AppSettings["ApplicationType"].ToLower() == "yourmoments") %>
                                        <%{ %>
                                        WEBSITE
                                        <%}
                                          else
                                          { %>
                                        MEMORIAL TRIBUTE
                                        <%} %>
                                    </td>
                                </tr>
                                <tr>
                                </tr>
                                <tr class="topBorderOnly">
                                    <td class="ytleftOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            Personalized Memorial Website</p>
                                        <label>
                                            (Choice of theme. Add their Obituary, Photo, Biography and Story)</label>
                                    </td>
                                    <td class="ytrightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                    <td class="ytrightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                </tr>
                                <tr style="height: 10px">
                                    <td class="ytleftOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            Guestbook & Memorials</p>
                                        <label>
                                            (Unlimited Messages and Memorials)</label>
                                    </td>
                                    <td class="ytrightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                    <td class="ytrightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ytleftOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            Events</p>
                                        <label>
                                            (Funeral Service Details, Location, Map, etc)</label>
                                    </td>
                                    <td class="ytrightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                    <td class="ytrightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ytleftOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            Notes
                                        </p>
                                        <label>
                                            (Add Important Information, Journal, etc)</label>
                                    </td>
                                    <td class="ytrightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                    <td class="ytrightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ytleftOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            Videos</p>
                                        <label>
                                            (Unlimited Embedded YouTube Videos)</label>
                                    </td>
                                    <td class="ytrightOfTableRow NewTextColor-MT">
                                        <p style="vertical-align: bottom; text-align: center;" class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                    <td class="ytrightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ytleftOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            Photo Albums
                                        </p>
                                        <label>
                                            (Upload 60 Photos Per Album)</label>
                                    </td>
                                    <td class="ytrightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            (2 Albums)
                                        </p>
                                    </td>
                                    <td class="ytrightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ytleftOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            High Resolution Photos
                                        </p>
                                        <label>
                                            (View and Download Photos and Albums)</label>
                                    </td>
                                    <td class="ytrightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                        </p>
                                    </td>
                                    <td class="ytrightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ytleftOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            Custom URL
                                        </p>
                                        <label>
                                            (Choose a unique URL for your Memorial Tribute)</label>
                                    </td>
                                    <td class="ytrightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                        </p>
                                    </td>
                                    <td class="ytrightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ytleftOfTableRow NewTextColor-MT">
                                        <p class="paraPricing ">
                                            No Advertising
                                        </p>
                                        <label>
                                            (Remove banner ads and all other advertising)</label>
                                    </td>
                                    <td class="ytrightOfTableRow NewTextColor-MT">
                                        <p style="color: #5BB4E5; font-size: 15px; font-style: normal;">
                                            *</p>
                                    </td>
                                    <td class="ytrightOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ytleftOfTableRow NewTextColor-MT">
                                        <p class="paraPricing">
                                            Technical Support
                                        </p>
                                    </td>
                                    <td colspan="2" style="border-left: 1px solid; border-right: 1px solid; background-color: #D3E9F5">
                                        <p style="margin-left: 17px; margin-top: 3px; font-size: 11px;">
                                            All plans come with unlimited support by email. 7 days per week.</p>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ytleftOfTableRow NewTextColor-MT TopBoldBorder" style="border-top: 2px solid #6B5982;">
                                        <p id="P1">
                                            <div class="Font15 pricingMargin">
                                                PRICE (per year)</div>
                                        </p>
                                    </td>
                                    <td class="rightOfTableRowLast NewTextColor-MT TopBoldBorder">
                                        <p id="P2">
                                            -</p>
                                    </td>
                                    <td class="rightOfTableRowLast NewTextColor-MT TopBoldBorder">
                                        <p id="P4">
                                            <asp:Label ID="lblPrsnlUserTributeYearly" runat="server"></asp:Label>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="ytleftOfTableRow NewTextColor-MT Font15 TopDottedBorder" style="border-top: 2px dotted #6B5982;">
                                        <h4>
                                            <strong>
                                                <div class="pricingMargin">
                                                    PRICE (LIFETIME)</div>
                                            </strong>
                                        </h4>
                                    </td>
                                    <td class="rightOfTableRowLast NewTextColor-MT Font15 TopDottedBorder">
                                        <h4>
                                            <strong>
                                                <div class="pricingMargin">
                                                    FREE</div>
                                            </strong>
                                        </h4>
                                    </td>
                                    <td class="rightOfTableRowLast NewTextColor-MT Font15 TopDottedBorder">
                                        <h4>
                                            <strong>
                                                <div class="pricingMargin">
                                                    <asp:Label ID="lblPrsnlUserTributeLifeTime" runat="server"></asp:Label></div>
                                            </strong>
                                        </h4>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div style="margin-left: 20px;">
                                            <p>
                                                <label style="color: #5BB4E5; font-size: 18px; font-style: normal;">
                                                    *</label>
                                                Advertising can be permanently removed from the
                                            </p>
                                            <p style="margin-left: 10px;">
                                                Free Obituary for a low one-time fee of $19.95.</p>
                                        </div>
                                    </td>
                                    <td class="rightOfTableRowLastSignUp-MT">
                                        <a class="ytselectButton" href="<%=Session["APP_BASE_DOMAIN"]%>Create.aspx?AccountType=1<%=AppendString%>">
                                            Select</a>
                                    </td>
                                    <td class="rightOfTableRowLastSignUp-MT">
                                        <a class="ytselectButton" href="<%=Session["APP_BASE_DOMAIN"]%>Create.aspx?AccountType=3<%=AppendString%>">
                                            Select</a>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="adSpacer30">
                            &nbsp</div>
                        <div class="leftSideExamplesBlock">
                            <div class="examplesText">
                                <h3 class="Purple-MT">
                                    Your data is safe and secure.
                                </h3>
                                <p class="NewTextColor-MT">
                                    Our servers are protected in a high security building with 24-hour surveillance,
                                    biometric locks and advanced fire protection. Our software is updated regularly
                                    with the latest security patches.
                                </p>
                            </div>
                        </div>
                        <div class="rightSideExamplesBlock">
                            <div class="examplesText">
                                <h3 class="Purple-MT">
                                    Your credit card is safe with us.
                                </h3>
                                <p class="NewTextColor-MT">
                                    All credit card transactions are procesed with 128/256 Bit Strong SSL Encryption.
                                    Note that we currently accept Visa, MasterCard, American Express and Discover Card
                                    and all prices are in USD.
                                </p>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="leftSideExamplesBlock">
                            <div class="examplesText">
                                <h3 class="Purple-MT">
                                    Your data is backed up daily.
                                </h3>
                                <p class="NewTextColor-MT">
                                    Create a memorial knowing that your personal information is safe with us. All of
                                    the information (photos, videos, text, etc) you have stored with us is backed up
                                    daily. Our backups are stored in multiple locations for additional redundancy and
                                    security.
                                </p>
                            </div>
                        </div>
                        <div class="rightSideExamplesBlock">
                            <div class="examplesText">
                                <h3 class="Purple-MT">
                                    Your privacy is important.
                                </h3>
                                <p class="NewTextColor-MT">
                                    When you create online memorial websites or contribute to other users memorials,
                                    we will never provide your name, email, phone number, credit card or any personal
                                    information to 3rd parties without your written consent. Learn more by reading our
                                    <a href="<%=Session["APP_BASE_DOMAIN"]%>privacy.aspx">privacy policy</a>.
                                </p>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <% if (ConfigurationManager.AppSettings["ApplicationType"].ToLower().Equals("yourtribute"))
                           { %>
                           <br />
                        <div class="yt-PricingAddText">
                            <div class="examplesText">
                                <h3 class="Purple-MT">
                                    Make an Obituary for free in a few easy steps</h3>
                            </div>
                            <p class="textunderPricing NewTextColor-MT">
                                If you are unsure if you want to create a memorial website, it is easy to make an
                                obituary for your loved one for free. Use the obituary to notify friends and family
                                of the death, add the person's photo and biography and then share the obituary with
                                others so that they can contribute condolences and memorials. You can also make
                                an obituary to easily share photos and videos of the deceased with friends and relatives.
                            </p>
                        </div>
                        <div class="yt-PricingAddText">
                            <div class="examplesText">
                                <h3 class="Purple-MT">
                                    Create a Memorial to pay tribute to a loved one</h3>
                            </div>
                            <p class="textunderPricing NewTextColor-MT">
                                If you make an obituary for free, it is easy to upgrade to a premium memorial website
                                at any time. Or, create a memorial website to start and begin using all premium
                                features right away. A memorial website includes designer themes, high-resolution
                                photos and videos, unlimited content and more premium features.
                            </p>
                            <br />
                            <p class="textunderPricing NewTextColor-MT">
                                View the table above for a complete list of features that are included with each
                                account type. To make an obituary no credit card is required and you can begin setting
                                up the obituary in a few simple steps. Or, create a memorial and complete the sign
                                up process to produce a beautiful permanent memorial to honor the life of your loved
                                one.
                            </p>
                        </div>
                        <%} %>
                        <div class="bigButtonsOnFeatures">
                            <div class="bottomRightButtons">
                                <a href='<%=ConfigurationManager.AppSettings["ApplicationType"].ToLower().Equals("yourtribute") ?Session["APP_BASE_DOMAIN"] +"themes.aspx" :Session["APP_BASE_DOMAIN"] +"examples.aspx"%>'
                                    class="bottomButtonExamplesLeft">Themes</a> <a href="<%=Session["APP_BASE_DOMAIN"]%>tour.aspx"
                                        class="bottomButtonTakeTour">Take Tour</a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="hack-clearBoth">
                </div>
                <div class="yt-ContentContainerImageOnFeatures">
                </div>
            </div>
        </div>
        <uc:Footer ID="Footer1" runat="server" />
        <div class="upgrade">
            <h2>
                Please Upgrade Your Browser</h2>
            <p>
                This site&#39;s design is only visible in a graphical browser that supports <a href="http://www.webstandards.org/upgrade/"
                    title="The Web Standards Project's BROWSER UPGRADE initiative.">web standards</a>,
                but its content is accessible to any browser or Internet device.</p>
        </div>
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
    
    <!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/BrowserOrTabCloseHandler.js"></script>

</body>
</html>
