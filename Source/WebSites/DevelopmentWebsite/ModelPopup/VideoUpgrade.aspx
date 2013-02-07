<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VideoUpgrade.aspx.cs" Inherits="ModelPopup_VideoUpgrade" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Expiry Notice</title>
    <meta content="text/html; charset=UTF-8" http-equiv="Content-Type" />
    <meta content="en-ca" http-equiv="Content-Language" />
    <meta content="false" http-equiv="imagetoolbar" />
    <meta content="index,follow" name="robots" />
    <meta content="true" name="MSSmartTagsPreventParsing" />
    <!-- really basic, generic html class stylesheet -->
    <link href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/default.css" media="screen, projection" type="text/css"
        rel="stylesheet" />
    <!-- default grid layout stylesheet -->
    <link href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/layouts/centered1024_1.css" media="screen, projection"
        type="text/css" rel="stylesheet" />
    <!-- print-friendly stylesheet -->
    <link href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/print.css" media="print" type="text/css" rel="stylesheet" />
    <!-- screen elements stylesheet should be here -->
    <%--<link href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/ScreenLatest.css" media="screen, projection" type="text/css"
        rel="stylesheet" />--%>
    <link href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/screen.css" media="screen, projection" type="text/css"
        rel="stylesheet" />
    <link href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/ModelPopup.css" type="text/css" rel="Stylesheet" />

    <script src="../assets/scripts/mootools-1.11.js" type="text/javascript"></script>

    <script src="../assets/scripts/global.js" type="text/javascript"></script>

    <script src="../Common/JavaScript/Common.js" type="text/javascript"></script>

    <!-- AG:19-Mar-10: Script Added for Google Ads -->
    <style type="text/css" media="screen">
        </style>

    <script type='text/javascript' src='http://partner.googleadservices.com/gampad/google_service.js'>
    </script>

    <script type='text/javascript'>
        GS_googleAddAdSenseService("ca-pub-7489783537502280");
        GS_googleEnableAllServices();
    </script>

    <script type='text/javascript'>
        GA_googleAddSlot("ca-pub-7489783537502280", "YourTribute_Anniversary_ExpiredNotice_Bottom_468x60");
        GA_googleAddSlot("ca-pub-7489783537502280", "YourTribute_Baby_ExpiredNotice_Bottom_468x60");
        GA_googleAddSlot("ca-pub-7489783537502280", "YourTribute_Birthday_ExpiredNotice_Bottom_468x60");
        GA_googleAddSlot("ca-pub-7489783537502280", "YourTribute_Graduation_ExpiredNotice_Bottom_468x60");
        GA_googleAddSlot("ca-pub-7489783537502280", "YourTribute_Memorial_ExpiredNotice_Bottom_468x60");
        GA_googleAddSlot("ca-pub-7489783537502280", "YourTribute_Wedding_ExpiredNotice_Bottom_468x60");
    </script>

    <script type='text/javascript'>
        GA_googleFetchAds();
    </script>

    <!--#include file="../analytics.asp"-->
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="divShowModalPopup">
    </div>
    <div class="yt-adModalContainer">
        <div class="ya-adModalInnerContainer">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <%--<div class="yt-topOfModal">--%>
                    <div>
                        <div class="modalMainHeading">
                            <h1>
                                Create a Memorial Website</h1>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="videomodalContentText">
                            <p>
                                Upgrade this video to a personal Memorial Tribute website for your loved one in
                                minutes. Share their story, add photos and videos and let friends and family do
                                the same. Receive personal messages in the guestbook and much more. Click the button
                                to learn more and get started.</p>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <%--<div class="modalContentBlock">--%>
                        
                        <table cellspacing="0" cellpadding="0">
                            <tr>
                                <td class="brownHeading" style="text-align: left; padding: 0px 0px 0px 18px;">
                                    Premium Features
                                </td>
                                <td class="brownHeading">
                                    Photo Tribute
                                </td>
                                <td class="brownHeading">
                                    Tribute
                                </td>
                            </tr>
                            <tr>
                                <td class="leftOfTableRow">
                                    Designer Themes
                                </td>
                                <td class="rightOfTableRow">
                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                </td>
                                <td class="rightOfTableRow">
                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                </td>
                            </tr>
                            <tr style="height: 10px">
                                <td class="leftOfTableRow">
                                    Story (Photo, Biography, etc)
                                </td>
                                <td class="rightOfTableRow">
                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                </td>
                                <td class="rightOfTableRow">
                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftOfTableRow">
                                    Guestbook
                                </td>
                                <td class="rightOfTableRow">
                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                </td>
                                <td class="rightOfTableRow">
                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftOfTableRow">
                                    Virtual Gifts
                                </td>
                                <td class="rightOfTableRow">
                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                </td>
                                <td class="rightOfTableRow">
                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftOfTableRow">
                                    Social Sharing Tools
                                </td>
                                <td class="rightOfTableRow">
                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                </td>
                                <td class="rightOfTableRow">
                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                </td>
                            </tr>
                            <tr>
                                <td class="leftOfTableRow">
                                <p style="margin:0;">
                                    Events (Online Invitations & RSVP)
                                    </p>
                                </td>
                                <td class="rightOfTableRow">
                                    <p style="margin:0;">
                                         <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                    </p>
                                </td>
                                <td class="rightOfTableRow">
                                <p style="margin:0;">
                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td class="leftOfTableRow">
                                <p style="margin:0;">
                                    Notes/Pages
                                    </p>
                                </td>
                                <td class="rightOfTableRow">
                                    <p style="margin:0;" >
                                        <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                    </p>
                                </td>
                                <td class="rightOfTableRow">
                                <p style="margin:0;">
                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td class="leftOfTableRow">
                                <p style="margin:0;">
                                    Videos
                                    </p>
                                </td>
                                <td class="rightOfTableRow">
                                    <p style="margin:0;">
                                        <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                    </p>
                                </td>
                                <td class="rightOfTableRow">
                                <p style="margin:0;">
                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td class="leftOfTableRow">
                                <p style="margin:0;">
                                    Photo Albums
                                    </p>
                                </td>
                                <td class="rightOfTableRow">
                                    <p style="margin:0;">
                                        (2 Albums)
                                    </p>
                                </td>
                                <td class="rightOfTableRow">
                                <p style="margin:0;">
                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td class="leftOfTableRow">
                                <p style="margin:0;">
                                    View/Download Full-Size Photos
                                    </p>
                                </td>
                                <td class="rightOfTableRow">
                                    <p style="margin:0;">
                                        -</p>
                                </td>
                                <td class="rightOfTableRow">
                                <p style="margin:0;">
                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td class="leftOfTableRow">
                                <p style="margin:0;">
                                    Unlimited Storage
                                    </p>
                                </td>
                                <td class="rightOfTableRow">
                                    <p style="margin:0;">
                                        -</p>
                                </td>
                                <td class="rightOfTableRow">
                                <p style="margin:0;">
                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td class="leftOfTableRow">
                                <p style="margin:0;">
                                    Custom URL
                                    </p>
                                </td>
                                <td class="rightOfTableRow">
                                    <p style="margin:0;"> 
                                        -</p>
                                </td>
                                <td class="rightOfTableRow">
                                <p style="margin:0;">
                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td class="leftOfTableRow">
                                    No Advertising
                                </td>
                                <td class="rightOfTableRow">
                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                </td>
                                <td class="rightOfTableRow">
                                    <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                </td>
                            </tr>
                            <tr>
                                <td class="alignBottom">
                                    <p>
                                        Not ready to upgrade?
                                        <asp:LinkButton ID="lbtnContinue" CausesValidation="false" runat="server" OnClick="btnContinueClick">Click here to continue</asp:LinkButton>
                                    </p>
                                </td>
                                <td class="rightOfTableRowLastSignUp">
                                    <asp:LinkButton ID="LinkButton1" class="upgradeButton" CausesValidation="false" runat="server"
                                        OnClick="lbtnUpgradeClick2">Upgrade</asp:LinkButton>
                                </td>
                                <td class="rightOfTableRowLastSignUp">
                                    <asp:LinkButton ID="LinkButton2" class="upgradeButton" CausesValidation="false" runat="server"
                                        OnClick="lbtnUpgradeClick3">Upgrade</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                        <%--</div>--%>
                        <div class="hack-clearBoth">
                        </div>
                    </div>
                    <div class="hack-clearBoth">
                    </div>
                    <%if (Session["PackageId"] != null && (Session["PackageId"].Equals(8) || Session["PackageId"].Equals(3)))
                      {%>
                    <div class="yt-GoogleOuter">
                        <div class="yt-GoogleAdBox-BottomSmall" id="BannerAdBoxBottom" runat="server">
                            <div class="yt-GoogleAdContentExp">
                                <br />
                                <br />
                            </div>
                        </div>
                    </div>
                    <%} %>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
    
    <!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
    <script type="text/javascript" src="../assets/scripts/BrowserOrTabCloseHandler.js"></script>

</body>
</html>
