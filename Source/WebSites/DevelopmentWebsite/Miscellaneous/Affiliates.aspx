<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Affiliates.aspx.cs" Inherits="Miscellaneous_Affiliates"
    Title="Affiliates" %>

<%@ Register Src="../UserControl/HeaderHome.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/FooterHome.ascx" TagName="Footer" TagPrefix="uc3" %>
<%@ Register Src="../UserControl/Inbox.ascx" TagName="Inbox" TagPrefix="uc2" %>
<%@ Register Src="../UserControl/LeftFeaturedPanel.ascx" TagName="LeftFeaturedPanel"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head>
    <title>Affiliate Program</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Content-Language" content="en-ca" />
    <meta http-equiv="imagetoolbar" content="false" />
    <meta name="robots" content="index,follow" />
    <meta name="MSSmartTagsPreventParsing" content="true" />
    <!-- really basic, generic html class stylesheet -->
    <!-- These url's will work on Remote server. Comment the above urls -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_DOMAIN"]%>assets/css/default.css" />
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_DOMAIN"]%>assets/css/layouts/centered1024_21.css" />
    <link rel="stylesheet" type="text/css" media="print" href="<%=Session["APP_BASE_DOMAIN"]%>assets/css/print.css" />
    <link rel="stylesheet" type="text/css" href="<%=Session["APP_BASE_DOMAIN"]%>assets/css/ScreenLatest.css" />
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_DOMAIN"]%>assets/css/medium_text.css"
        title="medium_text" />
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_DOMAIN"]%>assets/css/large_text.css"
        title="large_text" />
    <link rel="Shortcut Icon" type="image/x-icon" href="<%=Session["APP_BASE_DOMAIN"]%>assets/images/favicon.ico" />

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/mootools-1.11.js"></script>

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/global.js"></script>

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/styleSwitcher.js"></script>

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/modalbox.js"></script>

    <!--#include file="../analytics.asp"-->
</head>
<body>
    <form id="Form1" action="" runat="server">
    <div id="divShowModalPopup">
    </div>
    <div class="yt-Container yt-Tour">
        <uc1:Header ID="ytHeader" Section="tribute" NavigationName="None" runat="server" />
        <div class="hack-clearBoth">
        </div>
        <div class="yt-Breadcrumbs">
            <a href="<%#Session["APP_BASE_DOMAIN"]%>home.aspx">Home</a> <span class="selected">Affiliate
                Program </span>
        </div>
        <div class="yt-ContentContainer">
            <div class="yt-ContentContainerInner">
                <div class="yt-ContentPrimaryContainer">
                    <div class="yt-ContentPrimary">
                        <div class="yt-Panel-Primary">
                            <h2>
                                Affiliate Program</h2>
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
                                    <li><a href="features.aspx">Tour - Site Features</a></li>
                                    <li><a href="pricing.aspx">Tribute Pricing</a></li>
                                    <li class="yt-Selected"><a href="javascript:void(0);">Affiliate Program</a></li>
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
            <!--yt-ContentContainerInner-->
        </div>
        <!--yt-ContentContainer-->
        <!--<div class="yt-Footer">-->
        <uc3:Footer ID="Footer1" runat="server" />
        <!-- </div>-->
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

    </script>
    
    <!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/BrowserOrTabCloseHandler.js"></script>

</body>
</html>
