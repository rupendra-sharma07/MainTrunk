<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WelcomeModal.aspx.cs" Inherits="ModelPopup_WelcomeModal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Welcome to Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%></title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Content-Language" content="en-ca" />
    <meta http-equiv="imagetoolbar" content="false" />
    <meta name="robots" content="index,follow" />
    <meta name="MSSmartTagsPreventParsing" content="true" />
    <!-- really basic, generic html class stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/default.css" />
    <!-- default grid layout stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/layouts/centered1024_21.css" />
    <!-- print-friendly stylesheet -->
    <link rel="stylesheet" type="text/css" media="print" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/print.css" />
    <!-- screen elements stylesheet should be here -->
    <link rel="stylesheet" type="text/css" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/screen.css" />
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/help.css" />
    <!-- JS libraries -->

    <script type="text/javascript" src="../assets/scripts/mootools-1.11.js"></script>

    <script type="text/javascript" src="../assets/scripts/global.js"></script>

    <script type="text/javascript" src="../assets/scripts/admin.js"></script>

    <script type="text/javascript" src="../assets/scripts/modalbox.js"></script>

    <script type="text/javascript" src="../Common/JavaScript/Common.js"></script>

    <script type="text/javascript">
 window.addEvent('load', function() {	
        	buttonStyles();
	        thumbStyles();
	        hrFix();    
	        fieldsetFix();
	    });
    </script>
    <!--#include file="../analytics.asp"-->
</head>
<body>
    <form action="" runat="server">
    <div id="divShowModalPopup"></div> 
        <div class="yt-ModalWrapper">
            <b style="font-family: Arial; font-size: 16px">Welcome to the new Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>.</b>
            <br />
            <br />
            <p>
                After more than a year of development we are excited to release the brand new, fully
                redesigned, <%=TributesPortal.Utilities.WebConfig.TopLevelDomain%>. If this is your first visit to our website, a "Tribute"
                is a website that is created to celebrate a significant event or special someone.
                A tribute website can be created for a wedding, anniversary, new baby, graduation,
                birthday or memorial.
            </p>
            <p>
                We invite you to explore the many new features of <%=TributesPortal.Utilities.WebConfig.TopLevelDomain%>. Create a tribute
                and write a STORY about the person, post NOTES to inform visitors and invite guests
                to EVENTS. Visitors can share sentiments using the GUESTBOOK and COMMENT features
                and send virtual GIFTS. All users can upload PHOTOS and share their favorite VIDEOS.
            </p>
            <p>
                We hope that Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> makes it easy for you to create and share an everlasting
                record of your significant occasion.
            </p>
            <br />
            Sincerely,
            <br />
            Jason Ropchan, Founder & CEO
            <br />
            <br />
            <br />
            <b style="font-family: Arial; font-size: 14px">Before you begin, here are answers
                to a few questions you may have...</b>
            <br />
            <br />
            <p>
                <b>How do I add content to a <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>?</b><br />
                Anyone can add guestbook messages, gifts, photos, videos and comments to <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>s.
                To add content you must first sign-up with Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>. Registration is fast and
                easy – click the "Sign up" link at the top of the page to get started.
            </p>
            <br />
            <p>
                <b>How do I modify a <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>?</b><br />
                To change a <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>'s information, edit the story, write a note, or create an event,
                you must be a <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%> administrator. To become an administrator you must contact
                the <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%> creator. To do this, find the "about this tribute" box on the right-side
                of the <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%> homepage and click the persons name under "<%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> created by:" Send
                them a brief message with your administrator request and be sure to include your
                email address so that they can add you.
            </p>
            <br />
            <p>
                <b>How do I renew an expired <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%> or extend an active <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>?</b><br />
                To renew an expired <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>, click the renewal link located in the expiration message.
                To extend an active <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>, click the "sponsor this tribute" link on the tribute
                homepage. <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>s can be paid for yearly or once to be kept online for life.
            </p>
            <br />
            <p>
                <b>*For more help click the “Help” link at the bottom of any page.</b>
            </p>
            <div class="yt-Form-MiniButtons">
                <div class="yt-Form-Submit">
                    <a href="javascript:void(0);" class="yt-MiniButton yt-PrintButton" onclick="printModal();">
                        Print</a>
                </div>
            </div>
        </div>
    </form>
    
    <!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
    <script type="text/javascript" src="../assets/scripts/BrowserOrTabCloseHandler.js"></script>

</body>
</html>
