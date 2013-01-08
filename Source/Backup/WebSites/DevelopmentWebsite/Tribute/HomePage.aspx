<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomePage.aspx.cs" Inherits="Tribute_HomePage" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head runat="server">
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
<link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/layouts/centered1024_1o23.css" />
<!-- print-friendly stylesheet -->
<link rel="stylesheet" type="text/css" media="print" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/print.css" />
<!-- screen elements stylesheet should be here -->
<link rel="stylesheet" type="text/css" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/screen.css" />
<!-- Selected App Theme -->
<link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/themes/memorial/theme.css" />
<!-- larger text stylesheets -->
<link rel="alternate stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/medium_text.css" title="medium_text" />
<link rel="alternate stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/large_text.css" title="large_text" />
<!-- JS libraries -->
<script type="text/javascript" src="../assets/scripts/mootools-1.11.js"></script>
<script type="text/javascript" src="../assets/scripts/global.js"></script>
<script type="text/javascript" src="../assets/scripts/styleSwitcher.js"></script>
  <script language="javascript">      
      window.history.forward(1);
    </script>
    <!--#include file="../analytics.asp"-->
</head>
<body>
<form  runat="server" action="">
<div id="divShowModalPopup"></div>
  <div class="yt-Container yt-Channel-Memorial">
    <div class="yt-HeaderContainer">
      <div class="yt-Header"> <a href="javascript:void(0);" title="Return to Memorial Home Page" class="yt-Logo"> Your Tribute - Memorial </a>
        <div class="yt-HeaderControls">
          <div class="yt-NavHeader">
            <div class="floatLeft"> <a href="../Users/UserProfile.aspx">My Account</a> <a href="javascript:void(0);">Inbox (0)</a> </div>
            <div class="yt-UserInfo"> <span>
              <asp:Label ID="Usernamelong" runat="server"></asp:Label>
            </span> <a href="../Users/log_in.aspx">Log out</a> </div>
          </div>
          <!--yt-NavHeader-->
          <div class="hack-clearBoth"></div>
          <div class="yt-Tools">
            <div id="yt-TypeSizeControl" class="yt-TypeSizeControl"> <span class="floatLeft">Text Size:&#160;</span> <a href="javascript:void(0);" class="large" title="Large Text">a</a> <a href="javascript:void(0);" class="medium" title="Medium Text">a</a> <a href="javascript:void(0);" class="small" title="Small Text">a</a> </div>
            <a href="javascript:void(0);" id="yt-SearchLauncher" class="yt-SearchLauncher hideText">Find a Tribute</a>
            <div id="yt-Search">
              <h2>Find a Tribute</h2>
              <fieldset>
              <div class="yt-Form-Field yt-SearchKeywords">
                <label for="txtSearchKeywords">Search for:</label>
                <input type="text" class="yt-Form-Input" id="txtSearchKeywords" onclick="this.select();" value="Enter the name of a Tribute" />
              </div>
              <input id="btnSearchSubmit" type="image" value="Search Tributes" class="yt-Search-Submit" src="../assets/images/btn_go.gif" />
              <div class="columnLeft">
                <div class="yt-Form-Field yt-Form-Field-Checkbox">
                  <input id="rdoSearch_All" name="rdoTributeType" type="radio" />
                  <label for="rdoSearch_All">All Tributes</label>
                </div>
                <div class="yt-Form-Field yt-Form-Field-Checkbox">
                  <input id="rdoSearch_Anniversary" name="rdoTributeType" type="radio" />
                  <label for="rdoSearch_Anniversary">Anniversary Tributes</label>
                </div>
                <div class="yt-Form-Field yt-Form-Field-Checkbox">
                  <input id="rdoSearch_Birthday" name="rdoTributeType" type="radio" />
                  <label for="rdoSearch_Birthday">Birthday Tributes</label>
                </div>
                <div class="yt-Form-Field yt-Form-Field-Checkbox">
                  <input id="rdoSearch_Graduation" name="rdoTributeType" type="radio" />
                  <label for="rdoSearch_Graduation">Graduation Tributes</label>
                </div>
              </div>
              <div class="columnRight">
                <div class="yt-Form-Field yt-Form-Field-Checkbox">
                  <input id="rdoSearch_Memorial" name="rdoTributeType" type="radio" />
                  <label for="rdoSearch_Memorial">Memorial Tributes</label>
                </div>
                <div class="yt-Form-Field yt-Form-Field-Checkbox">
                  <input id="rdoSearch_NewBaby" name="rdoTributeType" type="radio" />
                  <label for="rdoSearch_NewBaby">New Baby Tributes</label>
                </div>
                <div class="yt-Form-Field yt-Form-Field-Checkbox">
                  <input id="rdoSearch_Wedding" name="rdoTributeType" type="radio" />
                  <label for="rdoSearch_Wedding">Wedding Tributes</label>
                </div>
                <div class="yt-SearchAdvancedLink"> <a href="javascript:void(0);">Advanced Search</a> </div>
              </div>
              </fieldset>
              <a href="javascript:void(0);" class="yt-MiniButton yt-CloseSearch" onclick="yt_Search.toggle();">Close</a>
              <div class="hack-clearBoth"></div>
            </div>
            <!--yt-Search-->
          </div>
          <!--yt-Tools-->
        </div>
        <!--yt-HeaderControls-->
      </div>
      <!--yt-Header-->
    </div>
    <!--yt-HeaderContainer-->
    <h1 class="yt-tributeTitle">Carmen San Diego and Robert Snalulovich</h1>
    <ul class="yt-NavPrimary">
      <li class="yt-Story "><a href="javascript:void(0);">Story</a></li>
      <li class="yt-Notes selected"><a href="javascript:void(0);">Notes</a></li>
      <li class="yt-Events"><a href="javascript:void(0);">Events</a></li>
      <li class="yt-GuestBook"><a href="guestBook.aspx?TributeId=<%=_tributeId %>&TributeName=<%=_tributeName %>">Guestbook</a></li>
      <li class="yt-Gifts"><a href="javascript:void(0);">Gifts</a></li>
      <li class="yt-Photos"><a href="javascript:void(0);">Notes</a></li>
      <li class="yt-Videos"><a href="javascript:void(0);">Notes</a></li>
    </ul>
    <!--yt-NavPrimary-->
    <div class="yt-ContentContainer">
      <div class="yt-ContentContainerInner">
        <div class="yt-ContentPrimaryContainer">
          <div class="yt-ContentPrimary">
            <div class="yt-Hero">
              <h2>
                  <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="pricing.aspx" >Create Tribute</asp:LinkButton>&nbsp;</h2>
                <h2>
                    Welcome</h2>
              <p>Welcome to our tribute, which is dedicated to celebrating the life of George Burns - the entertainment legend. This 
                site will help us share our memories with the ones we care about – and you can share your thoughts and memories with us, too!</p>
            </div>
            <p>It is coming up to George's favorite time of year, ipsum dolor sit amet, consectetuer adipiscing elit. Morbi vel enim eget massa semper aliquam. Nam eget est.
              Donec enim massa, feugiat in, lobortis eu, viverra ut, neque. Proin nisi mi, nonummy nec, tempor eu, vestibulum non, mauris. Nam egestas aliquam dui.</p>
            <p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Morbi vel enim eget massa semper aliquam. Nam eget est.
              Donec enim massa, feugiat in, lobortis eu, viverra ut, neque. Proin nisi mi, nonummy nec, tempor eu, vestibulum non, mauris. Nam egestas aliquam dui.</p>
            <p><a href="javascript:void(0);">Lorem ipsum dolor sit amet</a>, consectetuer adipiscing elit. Morbi vel enim eget massa semper aliquam. Nam eget est.
              Donec enim massa, feugiat in, lobortis eu, viverra ut, neque. Proin nisi mi, nonummy nec, tempor eu, vestibulum non, mauris. Nam egestas aliquam dui.</p>
            <p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Morbi vel enim eget massa semper aliquam. Nam eget est.
              Donec enim massa, feugiat in, lobortis eu, viverra ut, neque. Proin nisi mi, nonummy nec, tempor eu, vestibulum non, mauris. Nam egestas aliquam dui.</p>
            <p>Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Morbi vel enim eget massa semper aliquam. Nam eget est.
              Donec enim massa, feugiat in, lobortis eu, viverra ut, neque. Proin nisi mi, nonummy nec, tempor eu, vestibulum non, mauris. Nam egestas aliquam dui.</p>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
          </div>
          <!--yt-ContentPrimary-->
        </div>
        <!--yt-ContentPrimaryContainer-->
        <div class="yt-ContentSecondary">
          <h2 class="first">Recent Tribute Activity</h2>
          <p><a href="javascript:void(0);">Lorem ipsum dolor sit amet</a>, consectetuer adipiscing elit. Morbi vel enim eget massa semper aliquam. Nam eget est.</p>
          <p>Donec enim massa, feugiat in, lobortis eu, viverra ut, neque. Proin nisi mi, nonummy nec, tempor eu, vestibulum non, mauris. Nam egestas aliquam dui.</p>
        </div>
        <!--yt-ContentSecondary-->
        <div class="yt-ContentTertiary">
          <h2 class="first">Add a story!</h2>
          <p>You can add a more detailed story to this tribute. We give you a few tips on what to write, and even provide an area where you can 
            add bits of trivia for visitors to read!  No story information has been added to this tribute yet – but you can create one by 
            going to <a href="javascript:void(0);">the story page</a>.</p>
          <h2>Special Announcement</h2>
          <h2>About This Tribute</h2>
          <h2>Types of Tributes</h2>
        </div>
        <!--yt-ContentTertiary-->
        <div class="hack-clearBoth"> </div>
        <div class="yt-ContentContainerImage"></div>
      </div>
      <!--yt-ContentContainerInner-->
    </div>
    <!--yt-ContentContainer-->
    <div class="yt-Footer">
      <ul class="yt-NavFooter">
        <li><a href="javascript:void(0);">Terms of Use</a></li>
        <li><a href="javascript:void(0);">Privacy Policy</a></li>
        <li><a href="javascript:void(0);">Help</a></li>
        <li><a href="javascript:void(0);">Contact Us</a></li>
        <li><a href="javascript:void(0);">About Us</a></li>
      </ul>
      <div class="yt-Copyright">&#169; 2007 Your Tribute</div>
    </div>
    <!--yt-Footer-->
  </div>
  <!--yt-Container-->
  <div class="upgrade">
    <h2>Please Upgrade Your Browser</h2>
    <p>This site's design is only visible in a graphical browser that supports <a href="http://www.webstandards.org/upgrade/" title="The Web Standards Project's BROWSER UPGRADE initiative.">web standards</a>, but its content is accessible to any browser or Internet device.</p>
  </div>
  <!--yt-upgrade-->
</form>

<script type="text/javascript">
executeBeforeLoad();
</script>
</body>
</html>
