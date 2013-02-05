<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Popup.aspx.cs" Inherits="ModelPopup_Popup" %>
<%@ Register Src="~/UserControl/GiveGift.ascx" TagName="GiveGift_UserControl" TagPrefix="UC1" %>
<%@ Register Src="~/UserControl/PostMessage.ascx" TagName="PostMessage_UserControl" TagPrefix="UC2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:fb="https://www.facebook.com/2008/fbml"  >
<head runat="server">
    <title id="PopupTitle" runat="server"></title>
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
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/screen.css" />
     <link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/LoginPopup.css" />
      <script src="https://connect.facebook.net/en_US/all.js" type="text/javascript"></script>
    <script type="text/javascript" src="../assets/scripts/mootools-1.11.js"></script>   
    <script type="text/javascript" src="../assets/scripts/global.js"></script>
    <!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
    <script type="text/javascript" src="../assets/scripts/BrowserOrTabCloseHandler.js"></script>
    <script type="text/javascript" src="../Common/JavaScript/Common.js"></script>
 <script type="text/javascript" src="../assets/scripts/modalbox.js"></script>
 <script type="text/javascript">
        function chk(locat) {
            parent.modalClose_();
        }
        function setIECss() {
            if (navigator.userAgent.indexOf("MSIE 7.0") != -1) {
                if (document.getElementById('ucPostmessage_PostMessageBorder') != null) {
                    document.getElementById('ucPostmessage_PostMessageBorder').className = "yt-dotted-BorderIE7";
                }
                if (document.getElementById('ucGivegift_GiveGiftBorder') != null)
                    document.getElementById('ucGivegift_GiveGiftBorder').className = "yt-dotted-BorderIE7";


            }
        }
 </script>       
</head>
<body onload="javascript:setIECss()";>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   <div id="DivGift" runat="server" visible="true">
   <UC1:GiveGift_UserControl ID="ucGivegift" runat="server" />
   </div>
   <div id="DivMessage" runat="server"  visible="false">
   <UC2:PostMessage_UserControl ID="ucPostmessage" runat="server" />
   </div>
    </form>

</body>
  <script type="text/javascript" src="<%=Session["APP_SCRIPT_PATH"]%>assets/scripts/modalbox.js"></script>
    <script type="text/javascript">
      executeBeforeLoad();  

window.fbAsyncInit = function() {
    FB.init({
        appId  : '<%= ConfigurationManager.AppSettings["ApiKey"].ToString() %>',
        status : true, // check login status
        cookie : true, // enable cookies to allow the server to access the session
        xfbml  : true,  // parse XFBML
        //channelUrl  : 'http://www.yourdomain.com/channel.html', // Custom Channel URL
        oauth : true //enables OAuth 2.0
    });
};             
   
    </script>
</html>
