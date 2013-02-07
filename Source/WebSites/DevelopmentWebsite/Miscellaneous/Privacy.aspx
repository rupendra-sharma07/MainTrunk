<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Privacy.aspx.cs" Inherits="Miscellaneous_Privacy"
    Title="Privacy" %>
<%@ Register Src="../UserControl/YourTributeHeader.ascx" TagName="Header" TagPrefix="uc" %>     
<%@ Register Src="../UserControl/FooterHome.ascx" TagName="Footer" TagPrefix="uc" %>
<%@ Register Src="../UserControl/LeftFeaturedPanel.ascx" TagName="LeftFeaturedPanel"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml"  xmlns:fb="http://www.facebook.com/2008/fbml" xml:lang="en" lang="en">
<head>
    <title>Privacy Policy</title>
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
</head>
<body>
    <form id="Form1" action="" runat="server">
    <div id="divShowModalPopup"></div> 
        <div class="yt-Container yt-Tour">
            <uc:Header ID="ytHeader" Section="tribute" NavigationName="None" runat="server" />
          
            <div class="hack-clearBoth">
            </div>
            <div class="yt-Breadcrumbs">
                <a href="<%=Session["APP_BASE_DOMAIN"]%>" >Home</a> <span class="selected"> Privacy Policy </span>
            </div>
            <div class="yt-ContentContainer">
                <div class="yt-ContentContainerInner">
                    <div class="yt-ContentPrimaryContainer">
                        <div class="yt-ContentPrimary">
                            <div class="yt-Panel-Primary yt-Panel-Primary-about">
                                <h2>
                                   PRIVACY POLICY </h2>
                                   <center> <b> YOUR <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToUpper()%> PRIVACY POLICY </b>
                                    <p>   Last Updated: May 2008 </p> </center>

 <h3 class="yt-Bullet">
 1. What This Privacy Policy Covers </h3>
<br/>
<p>Your <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>’s Privacy Policy is designed to help you understand how we collect and use the Personal Information you decide to share, and help you make informed decisions when using Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, located at www.<%=TributesPortal.Utilities.WebConfig.TopLevelDomain%> and its directly associated domains (collectively, "Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>" or "Website")
<br/><br/>
Subject to the above, this Privacy Policy does not apply to the practices of companies that Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> does not own or control or to people that it does not employ or manage.
<br/><br/>
By using or accessing Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, you are accepting the practices described in this Privacy Policy. If you have questions or concerns regarding this statement, you should first contact our privacy staff at <a href="mailto:<%=TributesPortal.Utilities.WebConfig.PrivacyEmail%>"><%=TributesPortal.Utilities.WebConfig.PrivacyEmail%> </a>
<br/><br/>
</p>

 <h3 class="yt-Bullet"> 2. Information We Collect </h3>
 <br/>

<b> a. Personal Information </b><br/><br/>
<ul>
<li>	When you register with Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, you provide us with certain Personal Information, such as you name, your email address, your telephone number, your address and any other information that you provide to us.
</li>
<li>When you use Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, you may set up your personal profile, send messages, perform searches, and transmit information through various channels. We collect this information so that we can provide you the service. In some cases, we retain it so that, for instance, you can return to view prior messages you have sent.
</li>
<li>Any posting of User Content while using Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> can be viewed by the public and is not considered “Personal Information” and is not the type of information protected by this Privacy Policy. You post User Content (as defined in the Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> Terms of Use) on the Website at your own risk. We cannot control the actions of other users with whom you may choose to share your information. Therefore, we cannot and do not guarantee that User Content you post on the Website will not be viewed by unauthorized persons. We are not responsible for circumvention of any privacy settings or security measures contained on the Website. You understand and acknowledge that, even after removal, copies of User Content may remain viewable in cached and archived pages or if other Users have copied or stored your User Content.
</li>
<li>When you create a <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> Website you provide us with certain Personal Information about the person(s) you are creating the Website for. For example, with respect to a Memorial <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, we also collect the name, location, date of birth and date of death of the deceased person.
</li>
<li>If you choose to use our invitation service to tell a friend about our Website, we will ask you for information needed to send the invitation, such as your friend's email address. We will automatically send your friend a one-time email inviting him or her to visit the Website. Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> does not store this information.
</li>
<li>Any improper collection or misuse of information provided on Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> is a violation of the Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> Terms of Use and should be reported to <a href="mailto:<%=TributesPortal.Utilities.WebConfig.PrivacyEmail%>"><%=TributesPortal.Utilities.WebConfig.PrivacyEmail%> </a>.  By using Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, you are consenting to have your personal data transferred to and processed in the United States.
</li>
</ul>

<b>b. Other Information<br/><br /></b>
<ul>
<li> Like most Websites, Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> also receives and records information on our server logs from your browser automatically and through the use of electronic tools such as cookies. A cookie is a piece of data stored on the user's computer tied to information about the user. We use session ID cookies to confirm that users are logged in. These cookies terminate once the user closes the browser. Our server logs automatically receive and record information from your browser (including, for example, your browser type, your IP address, and the page(s) you visit). This information is gathered for all Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> visitors and is not considered “Personal Information” because it cannot be used to uniquely identify a single person.<br/>
</li>
</ul>

<b> c. Children under the Age of 13<br/><br /> </b>
<ul>
<li>
Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> does not knowingly collect or solicit Personal Information from anyone under the age of 13 or knowingly allow such persons to register. If you are under 13, please do not attempt to register for Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> or send any information about yourself to us, including your name, address, telephone number, or email address. No one under age 13 may provide any Personal Information to or on Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>. In the event that we learn that we have collected Personal Information from a child under age 13 without verification of parental consent, we will delete that information as quickly as possible. If you believe that we might have any information from or about a child under 13, please contact us at <a href="mailto:<%=TributesPortal.Utilities.WebConfig.PrivacyEmail%>"><%=TributesPortal.Utilities.WebConfig.PrivacyEmail%> </a>.<br/>
</li>
</ul>

<b> d. Children between the Ages of 13 and 18<br/> <br /></b>
<ul>
<li>
We recommend that minors over the age of 13 ask their parents for permission before sending any information about themselves to anyone over the Internet.<br/>
</li>
</ul>

<h3 class="yt-Bullet"> 3. How We Use Your Information </h3>
 <br/>
 
We will use your information only as permitted by law, and subject to the terms of our Privacy Policy;
<br />
<ul>
<li>
Personal Information you specifically provide may be used to provide the services we offer, to process transactions and billing, for identification and authentication purposes, to communicate with you concerning transactions, security, privacy, and administrative issues relating to your use of Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, to do something you have asked us to do.
</li>
<li>
Your name or username, location and profile picture thumbnail will be available for other users to view when you post content on Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>. 
</li>
<li>
Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> may send you service-related announcements from time to time through the general operation of the service. For instance, if a friend sends you a new message, you may receive an email alerting you to that fact. Generally, you may opt out of such emails in your privacy settings, though Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> reserves the right to send you notices about your account even if you opt out of all voluntary email notifications.
</li>
</ul>


 <h3 class="yt-Bullet">4. Sharing Your Information with Third Parties </h3>
 <br/>
When you use Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, certain information you voluntarily post or share with third parties, such as Personal Information, comments, messages, photos, videos, or other information, will be shared with other users. All such sharing of information is done at your own risk. Please keep in mind that if you disclose Personal Information in your profile or when posting comments, messages, photos, videos, or other items, this information will become publicly available. 
 <br/>  <br/>
If you post Personal Information that is accessible to the public, you may receive unsolicited messages from other parties in return. Such activities are beyond the control of Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> and this Privacy Policy. In addition, although we employ technology to minimize the spam sent to users and unsolicited, automatic posts to public forums on Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, we cannot ensure such measures to be 100% reliable or satisfactory.
 <br/>  <br/>
We do not provide contact information to third party marketers without your permission. We share your information with third parties only in limited circumstances where we believe such sharing is 1) reasonably necessary to offer the service, 2) legally required or, 3) permitted by you. For example:
 <br/> <br />
<ul>
<li>
We may provide information to service providers to help us bring you the services we offer. Specifically, we may use third parties to facilitate our business, such as to send out email updates about Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, or to process payments for products or services. In connection with these offerings and business operations, our service providers may have access to your Personal Information for use for a limited time in connection with these business activities. Where we utilize third parties for the processing of any Personal Information, we implement reasonable contractual and technical protections limiting the use of that information to the Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> specified purposes.
</li><li>
We may be required to disclose user information pursuant to lawful requests, such as subpoenas or court orders, or in compliance with applicable laws. We do not reveal information until we have a good faith belief that an information request by law enforcement or private litigants meets applicable legal standards. Additionally, we may share account or other information when we believe it is necessary to comply with the law, to protect our interests or property, to prevent fraud or other illegal activity perpetrated through the Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> service or using the Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToLower()%> name, or to prevent imminent bodily harm. This may include sharing information with other companies, lawyers, agents or government agencies.
</li><li>
We let you choose to share information with marketers or e-commerce providers through on-Website offers.
</li><li>
If the ownership of all or substantially all of the Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> business, or individual business units owned by Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc., were to change, your user information may be transferred to the new owner so the service can continue operations. In any such transfer of information, your user information would remain subject to the promises made in any pre-existing Privacy Policy.
</li>
</ul> 


<h3 class="yt-Bullet"> Links </h3>

<p> Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> may contain links to other Websites. We are of course not responsible for the privacy practices of other web Websites. We encourage our users to be aware when they leave our Website to read the privacy statements of each and every web Website that collects personally identifiable information. This Privacy Policy applies solely to information collected by Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>.

</p>
<h3 class="yt-Bullet"> Third Party Advertising </h3>

<p>Advertisements that appear on Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> are sometimes delivered (or "served") directly to users by third party advertisers. They automatically receive your IP address when this happens. These third party advertisers may also download cookies to your computer, or use other technologies such as JavaScript and "web beacons" (also known as "1x1 gifs") to measure the effectiveness of their ads and to personalize advertising content. Doing this allows the advertising network to recognize your computer each time they send you an advertisement in order to measure the effectiveness of their ads and to personalize advertising content. In this way, they may compile information about where individuals using your computer or browser saw their advertisements and determine which advertisements are clicked. Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> does not have access to or control of the cookies that may be placed by the third party advertisers. Third party advertisers have no access to your contact information stored on Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> unless you choose to share it with them.</p>


<h3 class="yt-Bullet"> Changing or Removing Information </h3>

<p>Access and control over most Personal Information on Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> is readily available through the profile editing tools. Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> users may modify or delete any of their profile information at any time by logging into their account. Information will be updated immediately. Individuals who wish to deactivate their Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> account may do so on the My Profile page. Removed information may persist in backup copies for a reasonable period of time but will not be generally available to members of Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>.</p>


<h3 class="yt-Bullet"> Security </h3>

<p>Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> takes appropriate precautions to protect our users' information. Your account information is located on a secured server behind a firewall. When you enter sensitive information (such as credit card number or your password), we encrypt that information using secure socket layer technology (SSL).. Because email is not recognized as secure communications, we request that you not send private information to us by email. If you have any questions about the security of Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> Web Website, please contact us at <a href="mailto:<%=TributesPortal.Utilities.WebConfig.PrivacyEmail%>"><%=TributesPortal.Utilities.WebConfig.PrivacyEmail%> </a>.</p>


<h3 class="yt-Bullet"> Terms of Use, Notices and Revisions </h3>

<p>Your use of Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, and any disputes arising from it, is subject to this Privacy Policy as well as our Terms of Use and all of its dispute resolution provisions including arbitration, limitation on damages and choice of law. We reserve the right to change our Privacy Policy and our Terms of Use at any time. Non-material changes and clarifications will take effect immediately, and material changes will take effect within 30 days of their posting on this Website. If we make changes, we will post them and will indicate at the top of this page the policy's new effective date. If we make material changes to this policy, we will notify you here, by email, or through notice on our home page. We encourage you to refer to this policy on an ongoing basis so that you understand our current Privacy Policy. Unless stated otherwise, our current Privacy Policy applies to all information that we have about you and your account.</p>


<h3 class="yt-Bullet"> Contacting Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> </h3>

<p> If you have any questions about this Privacy Policy, please contact us at <a href="mailto:<%=TributesPortal.Utilities.WebConfig.PrivacyEmail%>"><%=TributesPortal.Utilities.WebConfig.PrivacyEmail%> </a>. You may also contact us by mail at Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>, Inc. 2875 North Lamb Blvd., Bldg. 8, Las Vegas, NV 89115. </p>

                                  
                            </div>
                        </div>
                        <!--yt-ContentPrimary-->
                    </div>
                    <!--yt-ContentPrimaryContainer-->
                    <div class="yt-ContentSecondary">
                        <div class="yt-Panel yt-Panel-Tools">
                            <div class="yt-Panel-Body">
                                <h2>
                                    Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%></h2>
                                <div class="yt-TourLinks">
                                    <ul>
                                        <li><a href="about.aspx">About Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%></a></li>
                                      <%--  <li><a href="features.aspx">Tour - Site Features</a></li>
                                        <li><a href="pricing.aspx">Tribute Pricing</a></li>--%>
                                        <!-- <li><a href="affiliates.aspx">Affiliate Program</a></li>
                                        <li><a href="nonprofit.aspx">Non-Profit Support</a></li>
                                        <li ><a href="advertise.aspx">Advertise</a></li> -->
                                        <li><a href="contact.aspx">Contact</a></li>
                                        <li ><a href="advertise.aspx">Advertise</a></li>
                                        <li><a href="termsofuse.aspx">Terms of Use</a></li>
                                        <li class="yt-Selected"><a href="javascript:void(0);">Privacy Policy</a></li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div id="divYM" runat="server" class="yt-Panel" visible="false">
                             <uc1:leftfeaturedpanel id="LeftFeaturedPanel1" runat="server" />
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
                    <uc:Footer ID="Footer1" runat="server" />
                            
          <!--  </div>-->
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
    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/modalbox.js"></script>     
    
    <!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/BrowserOrTabCloseHandler.js"></script>

</body>
</html>


