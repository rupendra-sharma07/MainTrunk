<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemVideoExpNotice.aspx.cs"
    Inherits="ModelPopup_MemVideoExpNotice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Expiry Notice</title>
    <meta content="text/html; charset=UTF-8" http-equiv="Content-Type" />
    <meta content="en-ca" http-equiv="Content-Language" />
    <meta content="false" http-equiv="imagetoolbar" />
    <meta content="index,follow" name="robots" />
    <meta content="true" name="MSSmartTagsPreventParsing" />
    <meta http-equiv="X-UA-Compatible" content="IE=8" />
    <link href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/default.css" media="screen, projection" type="text/css"
        rel="stylesheet" />
    <link href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/layouts/centered1024_1.css" media="screen, projection"
        type="text/css" rel="stylesheet" />
    <link href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/print.css" media="print" type="text/css" rel="stylesheet" />
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
    <div class="yt-adModalContainer">
    <div id="divShowModalPopup"></div> 
        <div class="ya-adModalInnerContainer">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="yt-topOfModal">
                        <table>
                            <tr>
                                <td width="30%" style="border: 1px;">
                                    <asp:Image ID="imgUserPhoto" runat="server" ImageUrl="#" BorderWidth="1px" BorderColor="#A18F84"
                                        BorderStyle="Solid" />
                                </td>
                                <td width="5%">
                                    &nbsp;
                                </td>
                                <!-- Video Expiry UI Issue(No Wrap of Name ): Mohit Gupta 15-July 2010-->
                                <td width="85%" style="vertical-align: middle; text-align: left;">
                                    <table>
                                        <tr>
                                            <td>
                                                <h3 style="font-weight: bold;">
                                                    <asp:Label ID="lblName" runat="server" Text="" CssClass="yt-TributeName"></asp:Label>&nbsp;
                                                </h3>
                                            </td>
                                        </tr>
                                     
                                        <tr>
                                          
                                            <td>
                                                <asp:Label ID="lblDateInfo" runat="server" Text=""></asp:Label>&nbsp;
                                            </td>
                                        </tr>
                                     
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblCountryInfo" runat="server" Text=""></asp:Label>&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="modalContentUpperText">
                            <p>
                             <!-- Video Expiry UI Issue(No Wrap of Name ): Mohit Gupta 15-July 2010-->
                                To view the <b>Video Tribute</b> for
                                <asp:Label ID="lblUser2" runat="server" Text=""></asp:Label> you must upgrade
                                this Memorial <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>.
                                <asp:LinkButton ID="LinkButton3" CausesValidation="false" runat="server" OnClick="btnUpGradeClick">
                                    <b>See the pricing and upgrade the <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%></b></asp:LinkButton>
                                - it only takes a minute and is safe and secure. Or,
                                <asp:LinkButton ID="LinkButton4" CausesValidation="false" runat="server" OnClick="btnTakeTourClick">
                                    <b>take the tour</b></asp:LinkButton>&nbsp;to learn what else is included with
                                a Memorial <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>.
                            </p>
                        </div>
                        <div class="hack-clearBoth" style="height: 10px;">
                        </div>
                        <div class="modalContentBlock">
                            <img alt="access icon" src="../assets/images/pic_access.png" class="" />
                            <div class="modalContentImgText">
                                <h3 class="lightBrown bold">
                                    Access all features.
                                </h3>
                                <p>
                                    Receive a personalized Memorial Tribute website for
                                    <asp:Label ID="lblUser3" runat="server" Text=""></asp:Label>. Family and friends
                                    can upload photos and videos, share stories, leave personal messages in the guestbook,
                                    send virtual gifts and more!
                                </p>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="modalContentBlock">
                            <img alt="clock icon" src="../assets/images/pic_clock.png" class="" />
                            <div class="modalContentImgText">
                                <h3 class="lightBrown bold">
                                    One low fee. Online for life.
                                </h3>
                                <p>
                                    Unlike most websites, we don’t keep billing you year after year. We believe that
                                    this Memorial Tribute should remain online for life to view forever. We bill you
                                    one small fee, one-time, and that’s it!
                                </p>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="modalContentBlock">
                            <img alt="gift icon" src="../assets/images/pic_gift.png" class="" />
                            <div class="modalContentImgText">
                                <h3 class="lightBrown bold">
                                    Makes a great gift.
                                </h3>
                                <p>
                                    Upgrade this Memorial Tribute as a gift to the family of
                                    <asp:Label ID="lblUser4" runat="server" Text=""></asp:Label>. When you upgrade the
                                    tribute you can choose to remain anonymous or have the family notified of your generous
                                    gift.
                                </p>
                            </div>
                        </div>
                    </div>
                    <div class="hack-clearBoth">
                    </div>
                    <br />
                    <div class="yt-adModalbigButtons">
                        <asp:LinkButton ID="LinkButton2" CausesValidation="false" CssClass="adModalleftBigButton"
                            runat="server" OnClick="btnTakeTourClick">Take a Tour</asp:LinkButton>
                        <%--<a href="http://www.yourtribute.com/aboutfeatures.aspx" class="adModalrightBigButton">
                                Take a Tour</a>--%>
                        <asp:LinkButton ID="LinkButton1" CausesValidation="false" CssClass="adModalrightBigButton"
                            runat="server" OnClick="btnUpGradeClick">Upgrade the <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%></asp:LinkButton>
                    </div>
                    <div class="hack-clearBoth">
                    </div>
                    <%if (Session["PackageId"] != null && Session["PackageId"].Equals(3))
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
    <!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
    <script type="text/javascript" src="../assets/scripts/BrowserOrTabCloseHandler.js"></script>
</body>
</html>
