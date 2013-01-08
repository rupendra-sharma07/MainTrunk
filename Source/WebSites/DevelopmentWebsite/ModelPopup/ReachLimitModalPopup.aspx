<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReachLimitModalPopup.aspx.cs"
    Inherits="ModelPopup_ReachLimitModalPopup" %>

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
    <link href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/ScreenLatest.css" media="screen, projection" type="text/css"
        rel="stylesheet" />
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
                    <div>
                        <div class="modalMainHeading">
                            <h1>
                                Premium Feature
                            </h1>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="modalContentText">
                            <div id="divNotes" runat="server" visible="false">
                                <p>
                                    <b>You have reached your 2 Note Limit.</b> To add more notes, and access all premium
                                    features, you must upgrade this <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>. Upgrading the <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%> only takes a minute
                                    and is safe and secure. Click "Upgrade" below to view the pricing options, or click
                                    "close" to continue without upgrading.
                                </p>
                            </div>
                            <div id="divEvents" runat="server" visible="false">
                                <p>
                                    <b>You have reached your 1 Event Limit.</b> To add more events, and access all premium
                                    features, you must upgrade this <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>. Upgrading the <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%> only takes a minute
                                    and is safe and secure. Click "Upgrade" below to view the pricing options, or click
                                    "close" to continue without upgrading.
                                </p>
                            </div>
                            <div id="divPhotoAlbum" runat="server" visible="false">
                                <p>
                                    <b>You have reached your 2 Photo Album Limit.</b> To add more photos, and access
                                    all premium features, you must upgrade this <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>. Upgrading the <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%> only
                                    takes a minute and is safe and secure. Click "Upgrade" below to view the pricing
                                    options, or click "close" to continue without upgrading.
                                </p>
                            </div>
                            <div id="divVideo" runat="server" visible="false">
                                <p>
                                    You have reached your 2 Video Limit.</b> To add more videos, and access all premium
                                    features, you must upgrade this <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>. Upgrading the <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%> only takes a minute
                                    and is safe and secure. Click "Upgrade" below to view the pricing options, or click
                                    "close" to continue without upgrading.
                                </p>
                            </div>
                            <div id="divUpgradeAlbum" runat="server" visible="false">
                                <p>
                                    To download this photo album, and access all premium features, you must upgrade
                                    this <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>. Upgrading the <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%> only takes a minute and is safe and secure.
                                    Click "Upgrade" below to view the pricing options, or click "close" to continue
                                    without upgrading.
                                </p>
                            </div>
                            <div id="divUpgradeFullView" runat="server" visible="false">
                                <p>
                                    To view full-size photos, and access all premium features, you must upgrade this
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>. Upgrading the <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%> only take a minute and is safe and secure. Click
                                    “Upgrade” below to View the pricing options, or click “close” to continue without
                                    upgrading.
                                </p>
                            </div>
                            <div class="hack-clearBoth">
                            </div>
                            <br />
                            <table cellspacing="0" cellpadding="0" class="alignTable">
                                <tr>
                                    <td class="brownHeading" style="text-align: left; padding: 0px 0px 0px 18px;">
                                        Premium Features
                                    </td>
                                    <td class="brownHeading">
                                        <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> Account
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftOfTableRow">
                                        Designer Themes
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
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftOfTableRow">
                                        Guestbook
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
                                </tr>
                                <tr>
                                    <td class="leftOfTableRow">
                                        Announcements (E-cards)
                                    </td>
                                    <td class="rightOfTableRow">
                                        <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftOfTableRow">
                                        Unlimited Events (Online Invitations & RSVP)
                                    </td>
                                    <td class="rightOfTableRow">
                                        <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftOfTableRow">
                                        Unlimited Notes/Pages
                                    </td>
                                    <td class="rightOfTableRow">
                                        <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftOfTableRow">
                                        Unlimited Videos
                                    </td>
                                    <td class="rightOfTableRow">
                                        <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftOfTableRow">
                                        Unlimited Photo Albums
                                    </td>
                                    <td class="rightOfTableRow">
                                        <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftOfTableRow">
                                        View/Download High Resolution Photos
                                    </td>
                                    <td class="rightOfTableRow">
                                        <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftOfTableRow">
                                        Unlimited Storage
                                    </td>
                                    <td class="rightOfTableRow">
                                        <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftOfTableRow">
                                        Custom URL
                                    </td>
                                    <td class="rightOfTableRow">
                                        <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="leftOfTableRow">
                                        No Advertising
                                    </td>
                                    <td class="rightOfTableRow">
                                        <img alt="yes" src="<%=Session["APP_BASE_DOMAIN"]%>assets/images/greenTick.jpg" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="alignBottom">
                                        <p>
                                            Not ready to upgrade?
                                            <asp:LinkButton ID="lbtnContinue" CausesValidation="false" runat="server" OnClick="btnContinueClick"> Click here to continue</asp:LinkButton>
                                        </p>
                                    </td>
                                    <td class="rightOfTableRowLastSignUp">
                                        <%--<a class="upgradeButton" href="<%=_upgradeUrl%>">
                                        Select</a>--%>
                                        <asp:LinkButton ID="LinkButton1" class="upgradeButton" CausesValidation="false" runat="server"
                                            OnClick="lbtnUpgradeClick">Upgrade</asp:LinkButton>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <br />
                        <div class="hack-clearBoth">
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <%if (Session["PackageId"] != null && (Session["PackageId"].Equals(8) || Session["PackageId"].Equals(3)))
                          {%>
                        <div class="yt-GoogleOuter">
                            <div class="yt-GoogleAdBox-BottomSmall" id="BannerAdBoxBottom" runat="server">
                                <div class="yt-Scissors">
                                </div>
                                <div class="yt-GoogleAdContentExp">
                                    <div>

                                        <script type='text/javascript'>
                                <% if (Session["TributeType"]!=null && Session["TributeType"].Equals("wedding"))
                                   {%>
                                    GA_googleFillSlot("YourTribute_Wedding_ExpiredNotice_Bottom_468x60");
                               
                                <% } %>
                                <% else if (Session["TributeType"]!=null && Session["TributeType"].Equals("anniversary"))
                                    {%>
                                
                                     GA_googleFillSlot("YourTribute_Anniversary_ExpiredNotice_Bottom_468x60");
                               
                                <% } %>
                                <% else if (Session["TributeType"]!=null && Session["TributeType"].Equals("graduation"))
                                    {%>
                               
                                     GA_googleFillSlot("YourTribute_Graduation_ExpiredNotice_Bottom_468x60");
                                    
                                <% } %>
                                <% else if (Session["TributeType"]!=null && Session["TributeType"].Equals("memorial"))
                                    {%>
                                
                                     GA_googleFillSlot("YourTribute_Memorial_ExpiredNotice_Bottom_468x60");
                                   
                                <% } %>
                                <% else if (Session["TributeType"]!=null && Session["TributeType"].Equals("newbaby"))
                                    {%>
                               
                                     GA_googleFillSlot("YourTribute_Baby_ExpiredNotice_Bottom_468x60");
                                   
                                <% } %>
                                <% else if (Session["TributeType"]!=null && Session["TributeType"].Equals("birthday"))
                                    {%>                                
                                     GA_googleFillSlot("YourTribute_Birthday_ExpiredNotice_Bottom_468x60");                                  
                                <% } %>
                                        </script>

                                    </div>
                                    <p class="infoMessage">
                                        *Upgrade this <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%> to remove this, and all other, advertisements.</p>
                                </div>
                            </div>
                        </div>
                        <%} %>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
</body>
</html>
