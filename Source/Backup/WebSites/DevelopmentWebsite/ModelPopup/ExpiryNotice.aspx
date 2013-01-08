<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExpiryNotice.aspx.cs" Inherits="ModelPopup_ExpiryNotice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <div id="divShowModalPopup"></div> 
    <div class="yt-adModalContainer">
        <div class="ya-adModalInnerContainer">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="yt-topOfModal">
                        <div class="modalMainHeading">
                            <h1>
                                Premium Feature
                            </h1>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="modalContentText">
                            <p>
                                To access this feature you must upgrade this tribute to a paid package. Upgrading
                                a tribute only takes a minute and is extremely safe and secure.
                                <asp:LinkButton ID="lbtnTakeOurTour" CausesValidation="false" runat="server" OnClick="btnTakeTourClick">Take our tour</asp:LinkButton>
                                &nbsp;to learn more about what's included in our premium package, or
                                <asp:LinkButton ID="lbtnUpgradeThisTribute" CausesValidation="false" runat="server" OnClick="btnUpGradeClick">upgrade this tribute</asp:LinkButton>
                                right now!
                            </p>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <br />
                        <div class="modalContentBlock">
                            <img alt="clock icon" src="../assets/images/pic_clock.png" class="" />
                            <div class="modalContentImgText">
                                <h3 class="lightBrown bold">
                                    One low fee. Online for life!
                                </h3>
                                <p>
                                    Unlike most websites, we don’t want to keep billing you. We believe that your Tribute
                                    for this important event should remain online for life to view year after year.
                                    We bill you one small time fee, one-time, and that’s it!
                                </p>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="modalContentBlock">
                            <img alt="gift icon" src="../assets/images/pic_gift.png" class="" />
                            <div class="modalContentImgText">
                                <h3 class="lightBrown bold">
                                    Makes a great gift!
                                </h3>
                                <p>
                                    Upgrade this tribute as a gift to your friend and family member who created it.
                                    When you upgrade the tribute you can choose to remain anonymous or have the tribute
                                    creator notified of your generous gift by email.
                                </p>
                            </div>
                        </div>
                    </div>
                    <br />
                    <br />
                    <div class="hack-clearBoth">
                    </div>
                    <div class="yt-adModalbigButtons">
                        <asp:LinkButton ID="lbtnTakeTour" CausesValidation="false" CssClass="adModalleftBigButton"
                            runat="server" OnClick="btnTakeTourClick">Take a Tour</asp:LinkButton>
                        <asp:LinkButton ID="lbtnUpgradeTribute" CausesValidation="false" CssClass="adModalrightBigButton"
                            runat="server" OnClick="btnUpGradeClick">Upgrade the Tribute</asp:LinkButton>
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
