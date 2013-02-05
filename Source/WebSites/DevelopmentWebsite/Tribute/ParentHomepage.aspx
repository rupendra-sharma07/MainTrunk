<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ParentHomepage.aspx.cs" Inherits="Tribute_ParentHomepage" %>

<%@ Register Src="../UserControl/Inbox.ascx" TagName="Inbox" TagPrefix="uc2" %>
<%@ Register Src="../UserControl/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head>
    <title>Your Tribute</title>
    <!--
		
author: Mark Bice
last modified: December 02, 2007

	-->
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Content-Language" content="en-ca" />
    <meta http-equiv="imagetoolbar" content="false" />
    <meta name="robots" content="index,follow" />
    <meta name="MSSmartTagsPreventParsing" content="true" />
    <!-- really basic, generic html class stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/default.css" />
    <!-- default grid layout stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/layouts/centered1024_1.css" />
    <!-- print-friendly stylesheet -->
    <link rel="stylesheet" type="text/css" media="print" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/print.css" />
    <!-- screen elements stylesheet should be here -->
    <link rel="stylesheet" type="text/css" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/screen.css" />
    <!-- larger text stylesheets -->
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/medium_text.css"
        title="medium_text" />
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/large_text.css"
        title="large_text" />
    <!-- JS libraries -->

    <script type="text/javascript" src="../assets/scripts/mootools-1.11.js"></script>

    <script type="text/javascript" src="../assets/scripts/global.js"></script>

    <script type="text/javascript" src="../assets/scripts/styleSwitcher.js"></script>

    <script type="text/javascript" src="../assets/scripts/swfobject.js"></script>

    <!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
    <script type="text/javascript" src="../assets/scripts/BrowserOrTabCloseHandler.js"></script>

    <script type="text/javascript">
    
    window.addEvent('load', function() { SlideShowInit(); } );
    
    
    SlideShowInit = function() 
    {
        
    }
	
    </script>
    <!--#include file="../analytics.asp"-->
</head>
<body>
    <form action="" runat="server">
    <div id="divShowModalPopup"></div>
        <div class="yt-Container yt-Home yt-AnonymousUser">
            <div class="yt-HeaderContainer">
                <div class="yt-Header">
                    <a href="../Tribute/ParentHomepage.aspx" title="Return to Your Tribute Home Page"
                        class="yt-Logo"></a>
                    <div class="yt-HeaderControls">
                        <div class="yt-NavHeader">
                            <div class="floatLeft">
                                <a id="myprofile" runat="server" href="../MyHome/AdminMytributesHome.aspx">My Account</a><uc2:Inbox
                                    ID="Inbox1" runat="server" />
                            </div>
                            <div class="yt-UserInfo">
                                <span>
                                    <%=_userName%>
                                </span><span id="spanLogout" runat="server"></span><a id="lnRegistration" runat="server"
                                    href="../Users/UserRegistration.aspx">Sign up</a>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <!--yt-Tools-->
                    </div>
                    <!--yt-HeaderControls-->
                </div>
                <!--yt-Header-->
            </div>
            <!--yt-HeaderContainer-->
            <div class="hack-clearBoth">
            </div>
            <div class="yt-ContentContainer">
                <div class="yt-ContentContainerInner">
                    <div class="yt-ContentPrimaryContainer">
                        <div class="yt-ContentPrimary">
                            <div class="yt-Hero">
                                <h2>
                                    Celebrate a significant event or a special someone.</h2>
                                <h3>
                                    Share stories, photos, videos, and virtual gifts, and receive updates and event
                                    information</h3>
                                <ul class="yt-NavHome">
                                    <li class="yt-NewBaby yt-NavHome-Left"><a href="../Tribute/ChannelHomePage.aspx?Type=New Baby&Theme='BabyDefault'">
                                        New Baby</a></li>
                                    <li class="yt-Birthday yt-NavHome-Right"><a href="../Tribute/ChannelHomePage.aspx?Type=Birthday&Theme='BirthdayDefault'">
                                        Birthday</a></li>
                                    <li class="yt-Graduation yt-NavHome-Left"><a href="../Tribute/ChannelHomePage.aspx?Type=Graduation&Theme='GraduationDefault'">
                                        Graduation</a></li>
                                    <li class="yt-Memorial yt-NavHome-Right"><a href="../Tribute/ChannelHomePage.aspx?Type=Memorial&Theme='MemorialDefault'">
                                        Memorial</a></li>
                                    <li class="yt-Wedding yt-NavHome-Left"><a href="../Tribute/ChannelHomePage.aspx?Type=Wedding&Theme='WeddingDefault'">
                                        Wedding</a></li>
                                    <li class="yt-Anniversary yt-NavHome-Right"><a href="../Tribute/ChannelHomePage.aspx?Type=Anniversary&Theme='AnniversaryDefault'">
                                        Anniversary</a></li>
                                </ul>
                                <div class="yt-Hero-Image-Container">
                                    <div class="yt-Hero-Image">
                                        <div id="yt-flashcontent">
                                        </div>

                                        <script type="text/javascript">
                                       var so = new SWFObject('../assets/slideshows/home/parentHomeSlideShow.swf', 'hero', '374', '247', '7', '#ffffff');	
									   so.addVariable("xmlfile", EncodeUrl('../assets/slideshows/home/images.xml'));
									   so.addParam("wmode", "transparent");
                                       so.write("yt-flashcontent");
                                        </script>

                                    </div>
                                </div>
                                <div class="yt-Hero-Options">
                                    <a href="../Miscellaneous/pricing.aspx" class="yt-CreateTributeButton">Create a Tribute</a>
                                    <a href="../Miscellaneous/features.aspx" class="yt-TakeTourButton">Take a Tour</a>
                                </div>
                            </div>
                            <!--yt-Hero-->
                            <div class="yt-Panel-System">
                                <h2>
                                    Express Yourself</h2>
                                <div class="yt-Panel-Body">
                                    <p>
                                        Record your sentiments, show your photos, or give a gift to a friend or loved one.</p>
                                    <ul>
                                        <li><strong><a href="../Tribute/AdvanceSearch.aspx">Find a tribute</a> that's already
                                            been created.</strong></li>
                                        <li><strong><a href="../Tribute/AllTribute.aspx">View other tributes</a>.</strong></li>
                                    </ul>
                                </div>
                            </div>
                            <div class="yt-Panel-System">
                                <h2>
                                    Celebrate Life's Events</h2>
                                <div class="yt-Panel-Body">
                                    <p>
                                        Learn more about creating tributes to honour a 
                                        <a href="../Tribute/ChannelHomePage.aspx?Type=New Baby&Theme='BabyDefault'">birth</a>, 
                                        <a href="../Tribute/ChannelHomePage.aspx?Type=Graduation&Theme='GraduationDefault'">graduation</a>,
                                        <a href="../Tribute/ChannelHomePage.aspx?Type=Wedding&Theme='WeddingDefault'">wedding</a>, 
                                        <a href="../Tribute/ChannelHomePage.aspx?Type=Birthday&Theme='BirthdayDefault'">birthday</a>,
                                         <a href="../Tribute/ChannelHomePage.aspx?Type=Anniversary&Theme='AnniversaryDefault'">anniversary</a>,
                                        or <a href="../Tribute/ChannelHomePage.aspx?Type=Memorial&Theme='MemorialDefault'">life</a>.</p>
                                    <ul>
                                        <li><strong><a href="../Miscellaneous/features.aspx">Take a tour</a></strong>.</li>
                                        <li><strong>Learn more <a href="javascript:void(0);">about Your Tribute</a>.</strong></li>
                                    </ul>
                                </div>
                            </div>
                            <div class="yt-Panel-System yt-Panel-System-Last">
                                <h2>
                                    Create Community</h2>
                                <div class="yt-Panel-Body">
                                    <p>
                                        Collaborate with friends and family and mark a special occasion. <a href="../Miscellaneous/pricing.aspx">
                                            Create a tribute</a>.</p>
                                    <ul>
                                        <li><strong>FREE for 30 days!</strong></li>
                                        <li><strong>&#36;20 per tribute for 1 year</strong></li>
                                        <li><strong>&#36;50 per tribute for life</strong></li>
                                    </ul>
                                </div>
                            </div>
                            <div class="hack-clearBoth">
                            </div>
                            <ul class="yt-AdditionalOptions">
                                <li><a href="../Tribute/AllTribute.aspx">View Tributes</a></li>
                                <li><a href="javascript:void(0);">About Us</a></li>
                            </ul>
                        </div>
                        <!--yt-ContentPrimary-->
                    </div>
                    <!--yt-ContentPrimaryContainer-->
                    <div class="hack-clearBoth">
                    </div>
                    <div class="yt-ContentContainerImage">
                    </div>
                </div>
                <!--yt-ContentContainerInner-->
            </div>
            <!--yt-ContentContainer-->
            <div >
                <uc1:Footer ID="Footer1" runat="server" />
            </div>
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
    </script>

</body>
</html>
