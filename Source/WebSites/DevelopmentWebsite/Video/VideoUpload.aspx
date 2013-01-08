<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VideoUpload.aspx.cs" Inherits="Video_VideoUpload"
    Title="VideoUpload" ValidateRequest="false" %>

<%@ Register Src="../UserControl/YourTributeHeader.ascx" TagName="Header" TagPrefix="uc" %>
<%@ Register Src="../UserControl/Inbox.ascx" TagName="Inbox" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/Footerhome.ascx" TagName="Footer" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head>
    <title>Your Tribute - Login</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Content-Language" content="en-ca" />
    <meta http-equiv="imagetoolbar" content="false" />
    <meta name="robots" content="index,follow" />
    <meta name="MSSmartTagsPreventParsing" content="true" />
    <!-- really basic, generic html class stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/default.css" />
    <!-- default grid layout stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/layouts/centered1024_1.css" />
    <!-- print-friendly stylesheet -->
    <link rel="stylesheet" type="text/css" media="print" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/print.css" />
    <!-- screen elements stylesheet should be here -->
    <!-- larger text stylesheets -->
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/medium_text.css"
        title="medium_text" />
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/large_text.css"
        title="large_text" />
    <!-- JS libraries -->

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/mootools-1.11.js"></script>

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/global.js"></script>

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/styleSwitcher.js"></script>

    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/screenlatest.css" />

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/modalbox.js"></script>

    <script type="text/javascript">

        /* NOTE: may want to move this to an external .js */

        InitForm = function() {
            $$('.availabilityNotice').each(function(a) {
                a.innerHTML = '';
                a.className = 'availabilityNotice';
            });
        }

        function CheckForAcceptance() {
            doModalContact();
        }
	
    </script>

</head>
<body>
    <form runat="server" action="">
    <div id="divShowModalPopup">
    </div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="hdnReplace" runat="server" />
    <div class="yt-Container yt-VideoUpload">
        <div class="yt-HeaderContainer">
            <div class="yt-Header">
                <a href="" title="Return to Your Tribute Home Page" class="yt-Logo"></a>
                <div class="yt-HeaderControls">
                    <div class="yt-NavHeader">
                        <div class="floatLeft" id="divProfile" runat="server">
                            <a id="myprofile" runat="server" href="tributes.aspx">My Account</a>
                            <uc1:Inbox ID="Inbox1" runat="server" />
                            &nbsp; &nbsp; <a id="lnCreditCount" runat="server" href="~/ordercredit.aspx" style="color: #5BB4E5;">
                            </a>
                        </div>
                        <div class="yt-UserInfo">
                            <span id="header_user_name" class="spanUserName">
                                <%=_userName%>
                            </span><span id="spanLogout" runat="server"></span>
                        </div>
                    </div>
                    <!-- Modal for loading contacts -->
                    <div id="yt-ContactContent">
                        <div class="yt-Panel-Primary">
                            <h4>
                                <asp:Label ID="lblMessage" runat="server"></asp:Label></h4>
                            <asp:Label ID="lblYes" runat="server"></asp:Label>
                            <asp:Label ID="lblNo" runat="server"></asp:Label>
                            <div class="yt-Form-Submit">
                                <!-- This is submit button to post entire page form -->
                                <asp:LinkButton ID="lbtnYes" runat="server" OnClick="lbtnYes_Click" CssClass="yt-Button yt-ArrowButton">Yes</asp:LinkButton>
                                <asp:LinkButton ID="lbtnNo" runat="server" CssClass="yt-Button yt-ArrowButton">No</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="hack-clearBoth">
                    </div>
                    <div class="yt-topNavigation">
                        <ul id="yt-globalNav">
                            <li id="yt-globalMenuItem1" class="<%=HomeNavValue %>"><a href="<%=Session["APP_BASE_DOMAIN"]%>">
                                Home</a></li>
                            <li id="yt-globalMenuItem2" class="<%=TourNavValue %>"><a href="<%=Session["APP_BASE_DOMAIN"]%>tour.aspx">
                                Tour</a></li>
                            <li id="yt-globalMenuItem3" class="<%=FeaturesNavValue %>"><a href="<%=Session["APP_BASE_DOMAIN"]%>features.aspx">
                                Features</a></li>
                            <li id="yt-globalMenuItem4" class="<%=ExamplesNavValue %>"><a href="<%=Session["APP_BASE_DOMAIN"]%>themes.aspx">
                                Examples</a></li>
                            <li id="yt-globalMenuItem5" class="<%=PricingNavValue %>"><a href="<%=Session["APP_BASE_DOMAIN"]%>pricing.aspx">
                                Pricing &amp; Sign Up</a></li>
                        </ul>
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
                    <div class="yt-Panel-Primary">
                        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Always" runat="server">
                            <ContentTemplate>
                                <div class="yt-ContentPrimary">
                                    <asp:ValidationSummary ID="vsErrorSummary" runat="server" CssClass="yt-Error" HeaderText=" <h2>Oops - there was a problem with your video tribute.</h2></br><h3>Please correct the error(s) below:</h3>"
                                        ForeColor="Black" />
                                    <div id="ytNotice" class="yt-Notice">
                                        <!-- if the yt-LoginError div has content (including spaces, CR/LF characters, etc, the modal login will show on page load -->
                                        <div id="headline">
                                            You have successfully uploaded your video! Please select one of the four following
                                            options to continue:</div>
                                    </div>
                                    <div class="hack-clearBoth">
                                    </div>
                                    <br />
                                    <br />
                                    <!--  Mohit table     -->
                                    <h1 class="yt-BiggestHeading darkBlue">
                                        Create a Tribute
                                    </h1>
                                    <table cellspacing="0" cellpadding="0" class="alignTable">
                                        <tr>
                                            <td class="brownHeading RightEdgesWhite-YM" style="text-align: left; padding: 7px 0px 0px 18px;">
                                                <p>
                                                    Tribute Features</p>
                                            </td>
                                            <td class="brownHeading PurpleEdges-YM">
                                                Video Only
                                            </td>
                                            <td class="brownHeading PurpleEdges-YM">
                                                VIDEO + OBITUARY
                                            </td>
                                            <td class="brownHeading PurpleEdges-YM">
                                                <p>
                                                    PREMIUM OBITUARY</p>
                                            </td>
                                            <td class="brownHeading LeftEdgesWhite-YM">
                                                Memorial Tribute
                                            </td>
                                        </tr>
                                        <tr>
                                        </tr>
                                        
                                        <tr class="topBorderOnly">
                                            <td class="leftOfTableRow">
                                                <p class="paraVideo">
                                                    Designer Theme</p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                                </p>
                                            </td>
                                        </tr>
                                        <tr style="height: 10px">
                                            <td class="leftOfTableRow">
                                                <p class="paraVideo">
                                                    Story (Obituary, Photo, Biography, etc)</p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                                </p>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftOfTableRow">
                                                <p class="paraVideo">
                                                    Guestbook (Unlimited Messages)</p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    -
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftOfTableRow">
                                                <p class="paraVideo">
                                                    Free Virtual Gifts
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    -
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftOfTableRow">
                                                <p class="paraVideo">
                                                    Social Sharing Tools</p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p style="vertical-align: bottom; text-align: center;">
                                                    -
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftOfTableRow">
                                                <p class="paraVideo">
                                                    Events (Online Invitations & RSVP)
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p style="vertical-align: bottom; text-align: center;">
                                                    -
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                   <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftOfTableRow">
                                                <p class="paraVideo">
                                                    Photo Albums
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    -
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    (2 Albums)
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    (2 Albums)
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftOfTableRow">
                                                <p class="paraVideo">
                                                    Videos
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    -
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftOfTableRow">
                                                <p class="paraVideo">
                                                    Notes/Pages
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    -
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" /></p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" /></p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftOfTableRow">
                                                <p class="paraVideo">
                                                    View/Download High Res. Photos
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    -
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    -</p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    -</p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftOfTableRow">
                                                <p class="paraVideo">
                                                    Unlimited Storage
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    -</p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    -
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    -</p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftOfTableRow">
                                                <p class="paraVideo">
                                                    Custom URL
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    -
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    -</p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    -</p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftOfTableRow">
                                                <p class="paraVideo">
                                                    No Advertising
                                                </p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" /></p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    -</p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" /></p>
                                            </td>
                                            <td class="rightOfTableRow">
                                                <p class="paraVideo">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftOfTableRow video3rdLastBGcolor">
                                                <p class="paraVideo" style="padding-top: 0px;">
                                                    Video Tribute
                                                </p>
                                            </td>
                                            <td class="rightOfTableRowLast video3rdLastBGcolor" style="border-bottom: 1px solid #796359">
                                                <p class="paraVideo" style="padding-top: 0px;">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick_transBackgrd.png" />
                                                </p>
                                            </td>
                                            <td class="rightOfTableRowLastLight video3rdLastBGcolor">
                                                <p class="paraVideo" style="padding-top: 0px;">
                                                    (30 Days)</p>
                                            </td>
                                            <td class="rightOfTableRowLast video3rdLastBGcolor">
                                                <p class="paraVideo" style="padding-top: 0px;">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick_transBackgrd.png" />
                                                </p>
                                            </td>
                                            <td class="rightOfTableRowLast video3rdLastBGcolor">
                                                <p id="secondLastRow" class="paraVideo" style="padding-top: 0px;">
                                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick_transBackgrd.png" />
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftOfTableRow">
                                                <p id="secondLastRow" class="Font15">
                                                    PRICE (1 YEAR)
                                                </p>
                                            </td>
                                            <td class="rightOfTableRowLast">
                                                <p id="secondLastRow" class="Font15">
                                                    1 Credit</p>
                                            </td>
                                            <td class="rightOfTableRowLast">
                                                <p id="secondLastRow" class="Font15">
                                                    -</p>
                                            </td>
                                            <td class="rightOfTableRowLast">
                                                <p id="secondLastRow" class="Font15">
                                                    <asp:Label ID="lblPhotoTributeYearlyCost" runat="server" Text="Label"></asp:Label>
                                                </p>
                                            </td>
                                            <td class="rightOfTableRowLast">
                                                <p id="secondLastRow">
                                                    <asp:Label ID="lblTributeYearlyCost" runat="server" Text="Label"></asp:Label>
                                                </p>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="leftOfTableRow NewTextColor-MT Font15 yt-colFirst">
                                                <h4>
                                                    <strong>
                                                        <div class="pricingMargin">
                                                            PRICE (LIFETIME)</div>
                                                    </strong>
                                                </h4>
                                            </td>
                                            <td class="rightOfTableRowLast NewTextColor-MT Font15">
                                                <h4>
                                                    <strong>
                                                        <div class="pricingMargin">
                                                            2 Credits</div>
                                                    </strong>
                                                </h4>
                                            </td>
                                            <td class="rightOfTableRowLast NewTextColor-MT Font15">
                                                <h4>
                                                    <strong>
                                                        <div class="pricingMargin">
                                                            FREE</div>
                                                    </strong>
                                                </h4>
                                            </td>
                                            <td class="rightOfTableRowLast NewTextColor-MT Font15">
                                                <h4>
                                                    <strong>
                                                        <div class="pricingMargin">
                                                            <asp:Label ID="lblPhotoTributeLifeTimeCost" runat="server" Text="Label"></asp:Label>
                                                        </div>
                                                    </strong>
                                                </h4>
                                            </td>
                                            <td class="rightOfTableRowLast NewTextColor-MT Font15">
                                                <h4>
                                                    <strong>
                                                        <div class="pricingMargin">
                                                            <asp:Label ID="lblTributeLifeTimeCost" runat="server" Text="Label"></asp:Label>
                                                        </div>
                                                    </strong>
                                                </h4>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td class="rightOfTableRowLastSignUp">
                                                <a class="videoSelectButton" href="<%=Session["APP_BASE_DOMAIN"]%>Tribute/VideoTributeCreation.aspx">
                                                    Sign Up</a>
                                            </td>
                                            <td class="rightOfTableRowLastSignUp">
                                                <a class="videoSelectButton" href="<%=Session["APP_BASE_DOMAIN"]%>Create.aspx?Type=Memorial&AccountType=1">
                                                    Sign Up</a>
                                            </td>
                                            <td class="rightOfTableRowLastSignUp">
                                                <a class="videoSelectButton" href="<%=Session["APP_BASE_DOMAIN"]%>Create.aspx?Type=Memorial&AccountType=2">
                                                    Sign Up</a>
                                            </td>
                                            <td class="rightOfTableRowLastSignUp">
                                                <a class="videoSelectButton" href="<%=Session["APP_BASE_DOMAIN"]%>Create.aspx?Type=Memorial&AccountType=3">
                                                    Sign Up</a>
                                            </td>
                                        </tr>
                                    </table>
                                    <!--  Mohit table     -->
                                    <br />
                                    <br />
                                    <br />
                                    <div class="leftSideExamplesBlockforVideo">
                                        <div class="examplesText">
                                            <h3 class="lightBrown">
                                                Your data is safe and secure.
                                            </h3>
                                            <p>
                                                Our servers are protected in a high security building with 24-hour surveillance,
                                                biometric locks and advanced fire protection. Our software is updated regularly
                                                with the latest security patches.
                                            </p>
                                        </div>
                                    </div>
                                    <div class="yt-Panel-Primary yt-Panel-Extra">
                                        <h2>
                                            Add to an existing Tribute</h2>
                                        <p id="replaceTributeBox">
                                            You can add your video tribute to any tribute you have previously created or are
                                            the administrator of.</p>
                                        <div class="yt-Form-Field">
                                            <label for="lstTributes">
                                                Select a tribute from the list to continue:</label>
                                            <asp:ListBox ID="lstTributes" runat="server" CssClass="yt-Form-DropDown-Long" Rows="5"
                                                AutoPostBack="true" OnSelectedIndexChanged="lstTributes_SelectedIndexChanged">
                                            </asp:ListBox>
                                            <asp:RequiredFieldValidator ID="rfvTribute" runat="server" ControlToValidate="lstTributes"
                                                ErrorMessage="Select a tribute">!</asp:RequiredFieldValidator>
                                            <div class="yt-Form-Buttons">
                                                <div class="yt-Form-Submit">
                                                    <asp:LinkButton ID="lbtnAddVideo" runat="server" CssClass="yt-Button yt-ArrowButton"
                                                        Text="Add video to this tribute!" OnClick="lbtnAddVideo_Click"></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="leftSideExamplesBlock">
                                        <div class="examplesText">
                                            <h3 class="blue">
                                                Your data is backed up daily.
                                            </h3>
                                            <p>
                                                All of the information (photos, videos, text, etc) you have stored with us is backed
                                                up daily. Our backups are stored in multiple locations for additional redundancy
                                                and security.
                                            </p>
                                        </div>
                                    </div>
                                    <div class="leftSideExamplesBlock">
                                        <div class="examplesText">
                                            <h3 class="lightBrown">
                                                Your credit card is safe with us.
                                            </h3>
                                            <p>
                                                All credit card transactions are procesed with 128/256 Bit Strong SSL Encryption.
                                                Note that we currently accept Visa, MasterCard, American Express and Discover Card
                                                and all prices are in USD.
                                            </p>
                                        </div>
                                    </div>
                                    <div class="hack-clearBoth">
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <!--yt-ContentPrimary-->
                    </div>
                </div>
                <!--yt-ContentPrimaryContainer-->
                <div class="hack-clearBoth">
                </div>
                <div class="yt-ContentContainerImage" id="BottomGrass">
                </div>
            </div>
            <!--yt-ContentContainerInner-->
        </div>
        <!--yt-ContentContainer-->
        <div class="hack-clearBoth">
        </div>
        <uc2:Footer ID="Footer1" runat="server" />
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
    <span class="yt-Form-Field"></span>

    <script type="text/javascript">
        executeBeforeLoad();
    </script>

</body>
</html>
