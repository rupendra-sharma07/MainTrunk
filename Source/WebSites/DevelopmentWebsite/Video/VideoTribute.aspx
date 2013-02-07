<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VideoTribute.aspx.cs" Inherits="Video_VideoTribute" %>

<%@ Register Src="../UserControl/TributeHeader.ascx" TagName="Header" TagPrefix="uc" %>
<%@ Register Src="../UserControl/TributePageHeader.ascx" TagName="TributeHeader"
    TagPrefix="ucTribute" %>
<%@ Register Src="../UserControl/Footer.ascx" TagName="Footer" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:fb="http://www.facebook.com/2008/fbml"
xml:lang="en" lang="en">
<head id="Head1" runat="server">
    <title></title>
    <!--
		
author: LHK
last modified: November 02, 2010

	-->
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Content-Language" content="en-ca" />
    <meta http-equiv="imagetoolbar" content="false" />
    <meta name="robots" content="index,follow" />
    <meta name="MSSmartTagsPreventParsing" content="true" />
    <!-- really basic, generic html class stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/default.css" />
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/layouts/centered1024_1.css" />
    <link rel="stylesheet" type="text/css" media="print" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/print.css" />
    <link rel="stylesheet" type="text/css" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/ScreenLatest.css" />
    <link rel="stylesheet" type="text/css" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/screen.css" />
    <!-- Selected App Theme -->
    <link runat="server" id="idSheet" rel="stylesheet" type="text/css" media="screen, projection" />
    <!-- JS libraries -->
    <%-- <link rel="stylesheet" type="text/css" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/VideoTribute.css" />--%>

    <script type="text/javascript" src="../assets/scripts/mootools-1.11.js"></script>

    <script type="text/javascript" src="../assets/scripts/modalbox.js"></script>

    <script type="text/javascript" src="../assets/scripts/global.js"></script>

    <script type="text/javascript" src="../assets/scripts/styleSwitcher.js"></script>

    <script type="text/javascript" src="../assets/scripts/videoswfobject.js"></script>

    <script type="text/javascript" src="../Common/JavaScript/Common.js"></script>

    <script type="text/javascript" src="http://s7.addthis.com/js/250/addthis_widget.js"></script>

    <script src="https://connect.facebook.net/en_US/all.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        $(document).addEvent('fb_connected', function() {
            $('facebook_share_container').setStyle('display', 'block');

            replace_submit_with_stream('', 'facebook_share', get_msg, get_attachments, get_action_link);
        });
        function get_msg() {
            return "";
        };
        function get_attachments() {
            var ret = {
                name: '<asp:Literal id="videoWallPostSubject" Text="false" runat="server" />', //Matt commented on a video on the: Jon Stiles & Mary Smith Wedding Tribute
                href: '<asp:Literal id="videoWallLink" Text="false" runat="server" />', //video link
                caption: '<b>Website:</b> <asp:Literal id="videoWallTributeHome" Text="false" runat="server" />',
                description: "<b>Comment:</b> " + $('').value,
                media: [{
                    type: "image",
                    src: '<asp:Literal id="videoWallThumbnail" Text="" runat="server" />', //video thumbnail photo
                    href: '<asp:Literal id="videoWallLink1" Text="false" runat="server" />' //video link
}]
                };
                //console.log(ret);
                return ret;
            };
            function get_action_link() {
                var ret = [{ text: 'Visit <asp:Literal id="videoWallTributeType" Text="false" runat="server" /> Tribute',
                    href: '<asp:Literal id="videoWallTributeHome1" Text="false" runat="server" />'}]; //vist tribute_type tribute (link to tribute_type homepage)
                    //console.log(ret)
                    return ret;
                };
                function launch(url, name) {
                    var win = window.open(url, name, 'width=720,height=480,resizable=no,scrollbars=no');
                    win.focus();
                    return false;
                }
                function checkValidEmail(source, args) {
                    var validator = document.getElementById('<%= cvCheckValidEmail.ClientID %>');
                    var EmailList = (document.getElementById('<%= txtEmailAddress.ClientID %>').value).replace(/\,/g, ";");
                    args.IsValid = EventCheckValidEmail(validator, EmailList);
                }
                function LinkToMailPage() {
                    var validation = document.getElementById('<%= hdnTypeToMail.ClientID %>');
                    validation.value = location.href;
                }

                function showvideo(swfPlayer, video, date, upgradeurl) {
                    var reqVerstion = "9.0.0";
                    var flashvars = { MovieName: video, ExpiryDate: date, UpgradeURL: upgradeurl };
                    var params = { menu: "false", wmode: "transparent", allowScriptAccess: "always" };
                    var attributes = { id: "shell", name: "shell" };
                    swfobject.embedSWF(swfPlayer, "divVideoDisplay", "720", "480", reqVerstion, "expressInstall.swf", flashvars, params, attributes);
                }
                function showvideoOnly(swfPlayer, video, date, upgradeurl) {
                    var reqVerstion = "9.0.0";
                    var flashvars = { MovieName: video, ExpiryDate: date, UpgradeURL: upgradeurl };
                    var params = { menu: "false", wmode: "transparent", allowScriptAccess: "always" };
                    var attributes = { id: "shell", name: "shell" };
                    swfobject.embedSWF(swfPlayer, "divVideoOnlyDisplay", "680", "480", reqVerstion, "expressInstall.swf", flashvars, params, attributes);
                }


                function SetImage(url) {
                    document.getElementById('<%=imgTributeImageView.ClientID%>').src = url;
                    document.getElementById('<%=hdnStoryImageURL.ClientID%>').value = url;
                }
                function ReloadPage() {
                    window.location.reload();

                }
    </script>

    <script type='text/javascript' src='http://partner.googleadservices.com/gampad/google_service.js'>
    </script>

    <script type='text/javascript'>
        GS_googleAddAdSenseService("ca-pub-7489783537502280");
        GS_googleEnableAllServices();
    </script>

    <script type='text/javascript'>
        GA_googleAddSlot("ca-pub-7489783537502280", "YourTribute_Memorial_Homepage_Bottom_728x90");
    </script>

    <script type='text/javascript'>
        GA_googleFetchAds();
    </script>

    <!-- End Google Ads Code -->
    <!--#include file="../analytics.asp"-->
</head>
<body id="mainBody" runat="server">
    <form id="Form1" runat="server">
    <!-- Following DIV content is used for modal to share page -->
    <div id="divShowModalPopup">
    </div>
    <div class="yt-Container yt-Home yt-AnonymousUser">
        <uc:Header ID="HeaderHome" Section="tribute" NavigationName="None" runat="server" />
        <!--yt-HeaderContainer-->
        <div class="yt-Container yt-Home yt-AnonymousUser">
            <!--<div class="yt-HeaderContainer">-->
            <!--<div class="yt-Header"> -->
            <div class="yt-HeaderControls" id="divHeaderUC" runat="server">
                <!--Agnesh's Header-->
            </div>
            <!--yt-HeaderControls-->
            <!--</div>-->
            <!--</div>-->
            <!--yt-Header-->
            <ucTribute:TributeHeader ID="TributeCustomHeader" section="Tribute" navigationname="Home"
                runat="server" />
        </div>
        <!--yt-HeaderContainer-->
        <div id="EmptyDivAboveMainPanel" runat="server" visible="False">
            &nbsp;</div>
        <div class="yt-ContentContainer">
            <!--AD for VT removed complete yt-ContentContainerInner from here and replaced it with yt-vt-ContentInner-->
            <div class="yt-vt-ContentInner">
                <div class="hack-clearBoth">
                </div>
                <% if (packageId == 10 || packageId == 14)
                   { %>
                <div class="yt-GoogleAdBox-TopLarge" id="BannerAdBoxBottom" runat="server">
                    <div class="yt-GoogleAdContent">

                        <script type='text/javascript'>
                            GA_googleFillSlot("YourTribute_Memorial_Homepage_Bottom_728x90");  
                        </script>

                    </div>
                </div>
                <% } %>
                <div class="hack-clearBoth">
                </div>
                <!--  LHK-div vidio only start here-->
                <div id="divVidOnly" runat="server">
                 <% if (packageId == 11 || packageId == 12 || packageId == 13)
                    { %>
                    <div style="margin-top:20px;"/>
                   <%} %>
                    <div style="height: 480px; width: 760px;" >
                        <div class="divInnerVideo">
                            <div id="divVideoOnlyDisplay" runat="server" class="yt-vt-rightVideo">
                            </div>
                        </div>
                    </div>
                    <div class="hack-clearBoth">
                    </div>
                    <div class="orderBannerVidOnly">
                        <p class="notice-box yt-theme-link-color">
                            <asp:Label ID="lblVidOnlyErrorMessage" runat="server"></asp:Label>
                        </p>
                    </div>
                    <div class="videoOnlyFooter">
                        <ul class="yt-NavFooter">
                            <li><a href="http://support.yourtribute.com/anonymous_requests/new" target="_blank">
                                Contact</a></li>
                            <li><a href="http://support.yourtribute.com" target="_blank">Help</a></li>
                        </ul>
                        <div class="yt-Legal">
                            <ul class="yt-NavFooter">
                                <li><span>Powered by, </span><a href="<%=Session["APP_BASE_DOMAIN"]%>">Your Tribute</a>.</li>
                            </ul>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                    </div>
                </div>
                <!-- LHK-div vidio only end here-->
                <!--  LHK-div vidio tribute start here-->
                <div id="divVidTribute" runat="server">
                    <div class="yt-vt-leftColumn">
                        <asp:Image ID="imgTributeImageView" runat="server" Height="146px" ImageAlign="Middle"
                            Width="146px" />
                        <div id="dvUploadPhoto" runat="server">
                            <a href="javascript:void(0);" style="margin-top: 8px; float: right;" class="yt-MiniButton yt-UploadPhotoButton"
                                onclick="uploadVideoTributePhoto();">Upload an Image</a></div>
                        <span class="yt-smallerFrame"></span>
                        <!--To be replaced by actual photo-->
                        <asp:HiddenField ID="hdnStoryImageURL" runat="server" />
                        <asp:HiddenField ID="hdnVideoExpiryDate" runat="server" />
                        <asp:HiddenField ID="hdnTypeToMail" runat="server" />
                        <div class="yt-vt-photoDetailsForVideoTribute">
                            <h3>
                                <asp:Label ID="LblOwnwerName" runat="server" Font-Bold="True"></asp:Label></h3>
                            <div style="height: 10px; clear: both">
                            </div>
                            <p class="yt-theme-color">
                                <asp:Label ID="lblDate1" runat="server">Date of Birth:</asp:Label></p>
                            <p>
                                <asp:Label ID="lblOwnerDOB" runat="server" Text="lblOwnerDOB"></asp:Label>
                            </p>
                            <p class="yt-theme-color">
                                Date of Death:</p>
                            <p>
                                <asp:Label ID="lblOwnerDOD" runat="server" Text="lblOwnerDOD"></asp:Label>
                            </p>
                            <p class="yt-theme-color">
                                <asp:Label ID="lblAge" runat="server">Age:</asp:Label></p>
                            <p>
                                <asp:Label ID="lblOwnerAge" runat="server" Text="lblOwnerAge"></asp:Label></p>
                            <p class="yt-theme-color">
                                Location:</p>
                            <p>
                                <%--<asp:Label ID="lblOwnerLocation" runat="server" Text="lblOwnerLocation"></asp:Label>--%>
                                <asp:Label ID="lblLocation" runat="server" Text="lblOwnerLocation"></asp:Label>
                            </p>
                            <div id="divEditButton" runat="server">
                                <a href="javascript:void(0);" class="yt-MiniButton yt-VTUploadPhotoButton" onclick="TributeEditDetailsModalPopUp(location.href,document.title);">
                                    Edit</a>
                            </div>
                            <div style="clear: both;">
                            </div>
                            <div class="yt-vt-shareVideo">
                                <p class="bold">
                                    Share Video Tribute:</p>
                                <%--   Link  Buttons--%>
                                <div class="addthis_toolbox addthis_default_style" id="liShareOnFacebook" runat="server">

                                    <script type="text/javascript">
                                        var addthis_config =
                                                {
                                                    username: "yourtribute",
                                                    ui_cobrand: "Your Tribute",
                                                    ui_header_color: "#52372C",
                                                    ui_header_background: "#F8F1EB",
                                                    services_compact: 'print, favorites, myspace, tumblr, digg, delicious, stumbleupon, google, live, yahoomail,aolmail, more'
                                                }
                                    </script>

                                    <a class="addthis_button_facebook"></a>
                                    <!-- End -->
                                    <!-- Start - Modification on 21-Dec-09 for the enhancement 7 of the Phase 1 -->
                                    <ul class="yt-Tools-Share">
                                        <%--<li id="liEmailPage" runat="server" class="yt-Tool-EmailPage" visible="true"><a
                                    href="javascript:void(0);" onclick="LinkToMailPage(); doModalShare();">Email </a></li>--%>
                                    </ul>
                                    <a id="A1" class="socioIcons vt-mail" href="javascript:void(0);" onclick="LinkToMailPage(); doModalShare();"
                                        runat="server"></a>

                                    <script type="text/javascript">
                                        var addthis_share =
                                                {
                                                    "templates": {
                                                        "twitter": 'Check out the {{title}} {{url}} via @yourtribute'
                                                    }
                                                }
                                    </script>

                                    <a style="display: inline;" class="addthis_button_twitter"></a>
                                    <!-- End -->
                                    <!-- Start - Modification on 22-Dec-09 for the enhancement 9 of the Phase 1 -->
                                    <a class="addthis_button_compact"></a>
                                </div>
                                <!-- End -->
                                <%--Link  Buttons End--%>
                            </div>
                            <div style="clear: both;">
                            </div>
                        </div>
                        <div class="orderBanner" id="divOrderDVD" runat="server" visible="false">
                            <h2>
                                <div class="yt-theme-color">
                                    Order DVD of Video Tribute</div>
                            </h2>
                            <img class="dvd" src="../assets/images/dvd_pic.jpg" alt="DVDCase" />
                            <p>
                                Purchase a high quality version of this video on DVD</p>
                            <div class="hack-clearBoth">
                            </div>
                            <asp:Label ID="lblMailforDVD" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div id="divVideoView" runat="server" class="yt-vt-rightVideo">
                        <div id="divVideoDisplay">
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <%--  laxman--%>
                        <%--<div id="myContent">This contains will be replaced by flash SWF file</div>--%>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="orderBanner">
                            <p class="notice-box yt-theme-link-color">
                                <asp:Label ID="lblErrorMessage" runat="server"></asp:Label>
                            </p>
                        </div>
                        <div id="divIsMemTributeOn" runat="server">
                            <div class="dottedBox" id="divLearnMore" runat="server">
                                <h2>
                                    Create a Memorial Website for
                                    <asp:Label ID="LblTributeNameLearnMoreBtn" runat="server"></asp:Label></h2>
                                <p class="leftInDotted">
                                    Upgrade this video to a personal Memorial Tribute website for your loved one in
                                    minutes. Share their story, add photos and videos and let friends and family do
                                    the same. Receive personal messages in the guestbook and much more. Click the button
                                    to learn more and get started.</p>
                                <a href='javascript: void(0);' onclick='LearnMoreModalPopup(location.href,document.title);'
                                    class="vt-longButtonLearnMore" id="ButtonLearnMore">Learn More</a>
                                <div class="hack-clearBoth">
                                </div>
                            </div>
                            <div class="dottedBox" id="divViewMemorialTribute" runat="server">
                                <h2>
                                    Create a Memorial Tribute website for
                                    <asp:Label ID="LblTributeNameViewTributeBtn" runat="server" /></asp:Label></h2>
                                <p class="leftInDotted">
                                    Upgrade this video to a personal Memorial Tribute website for your loved one in
                                    minutes. Share their story, and photos and videos and let friends and family do
                                    the same. Receive personal messages in the guestbook and much more. Click the button
                                    to learn more and get started.</p>
                                <a href='<%=Session["APP_BASE_DOMAIN"]%><%=Session["TributeURlofLinkMemTrb"]%>/?TributeType=Memorial'
                                    class="vt-longButtonviewMemorial">View memorial Tribute</a>
                                <div class="hack-clearBoth">
                                </div>
                            </div>
                            <div class="hack-clearBoth">
                            </div>
                        </div>
                        <!--IsMemTributeOn ends here --->
                    </div>
                    <!--AD for VT ends here --->
                    <div class="hack-clearBoth">
                    </div>
                    <div>
                        <ul class="yt-NavFooter" style="padding: 9px 9px 0px 0px;">
                            <li><a href="http://support.yourtribute.com/anonymous_requests/new" target="_blank">
                                Contact</a></li>
                            <li><a href="http://support.yourtribute.com" target="_blank">Help</a></li>
                        </ul>
                        <div class="yt-Legal">
                            <ul class="yt-NavFooter" style="padding: 9px 9px 0px 0px;">
                                <li><span>Powered by, </span><a href="<%=Session["APP_BASE_DOMAIN"]%>">Your Tribute</a>.</li>
                            </ul>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                    </div>
                </div>
                <!-- LHK-div vidio tribute end here-->
                <!--yt-ContentContainer-->
            </div>
            <div class="hack-clearBoth">
            </div>
            <div id="yt-ShareContainer">
                <div id="yt-ShareError">
                    <!-- if the yt-SponsorError div has content (including spaces, CR/LF characters, etc, the modal login will show on page load -->
                    <div id="yt-ProfileError">
                        <asp:ValidationSummary ValidationGroup="Share" CssClass="yt-Error" ID="vsError" runat="server"
                            HeaderText="<h2>Oops - there was a problem with some account type information.</h2><h3>Please correct the errors below:</h3>"
                            ForeColor="Black" />
                    </div>
                </div>
                <div id="yt-ShareContent" class="yt-ModalWrapper">
                    <div class="yt-Panel-Primary">
                        <h2 id="hEmailPage" runat="server">
                        </h2>
                        <p id="pRequired" runat="server" class="yt-requiredFields">
                        </p>
                        <div class="yt-Form-Field">
                            <label for="txtSendName" id="lblUserName" runat="server">
                            </label>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="yt-Form-Input-Long" ValidationGroup="Share"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUserName" ValidationGroup="Share" runat="server"
                                ControlToValidate="txtUserName">!</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revTributeAddress" runat="server" ControlToValidate="txtUserName"
                                ErrorMessage="Please Enter Valid Name" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"
                                Text="!" ValidationGroup="Share" ValidationExpression="^[a-zA-Z0-9\s\?\!\-\@\-\.\:\;\=\+\[\]_\{\}\,\%\(\)\/\&amp;]*$"></asp:RegularExpressionValidator>
                        </div>
                        <div class="yt-Form-Field">
                            <label for="txtSendEmail" id="lblUserEmailAddress" runat="server">
                            </label>
                            <asp:TextBox ID="txtUserEmailAddress" runat="server" ValidationGroup="Share" CssClass="yt-Form-Input-Long"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUserEmailAddress" runat="server" ValidationGroup="Share"
                                ControlToValidate="txtUserEmailAddress">!</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revFromEmailAddress" ValidationGroup="Share"
                                runat="server" ControlToValidate="txtUserEmailAddress" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">!</asp:RegularExpressionValidator>
                        </div>
                        <div class="yt-Form-Field yt-EmailList">
                            <label for="txtarEmailAddresses" id="lblEmailAddress" runat="server">
                            </label>
                            <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="yt-Form-Textarea-Short"
                                ValidationGroup="Share" Columns="50" Rows="6" TextMode="MultiLine"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmailAddress" runat="server" ValidationGroup="Share"
                                ControlToValidate="txtEmailAddress">!</asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvCheckValidEmail" runat="server" ValidationGroup="Share"
                                ClientValidationFunction="checkValidEmail" Width="1px" ErrorMessage="Email Address are not valid"
                                Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000">!</asp:CustomValidator>
                            <div class="yt-Form-MiniButtons">
                                <div class="yt-Form-Submit">
                                    <asp:LinkButton ID="lbtnSubmit" ValidationGroup="Share" CssClass="yt-MiniButton yt-AddButton"
                                        runat="server" OnClick="lbtnSubmit_Click"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
    
    <!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
    <script type="text/javascript" src="../assets/scripts/BrowserOrTabCloseHandler.js"></script>

</body>
</html>
