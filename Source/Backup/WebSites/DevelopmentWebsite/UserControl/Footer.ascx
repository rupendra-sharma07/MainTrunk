<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Footer.ascx.cs" Inherits="UserControl_Footer" %>

<script type="text/javascript" language="javascript" src="<%=Session["APP_SCRIPT_PATH"]%>Common/JavaScript/FooterControl.js"></script>

<div>
<div class="yt-Footer">
<ul class="yt-NavFooter">
    <%if (ConfigurationManager.AppSettings["ApplicationType"].ToLower() == "yourmoments") %>
    <%{ %>
    <li><a href="http://support.yourmoments.com" target="_blank">Help</a></li>
    <li><a href="<%=ConfigurationManager.AppSettings["MomentsBlogUrlMain"]%>">
        Blog</a></li>
    <li><a href="http://support.yourmoments.com/anonymous_requests/new" target="_blank">
        Contact</a></li>
        <li><a href="<%=Session["APP_BASE_DOMAIN"]%>about.aspx">About</a></li>
    <%}
      else
      { %>
    <li id="LiFuneralResources"><a href="http://resources.yourtribute.com/" target="_blank">Funeral Resources</a></li>
    <li id="LiGriefandLoss"><a href="http://resources.yourtribute.com/grief-and-loss/" target="_blank">Grief and Loss</a></li>
    <li id="LiResourcePlanning"><a href="http://resources.yourtribute.com/funeral-planning/" target="_blank">
        Funeral Planning</a></li>
    <%} %>    
</ul>
<div class="yt-Legal">
    
    <%if (ConfigurationManager.AppSettings["ApplicationType"].ToLower() == "yourmoments") %>
    <%{ %>
    <ul class="yt-NavFooter">
        <li>&copy;
            <asp:Label ID="lblCopyRight" CssClass="CopyRightText" runat="server" Text=""></asp:Label>&nbsp;
            <a href="<%=Session["APP_BASE_DOMAIN"]%>">Your
                <%=ConfigurationManager.AppSettings["ApplicationWord"]%></a></li>
        <li><a href="<%=Session["APP_BASE_DOMAIN"]%>termsofuse.aspx">Terms</a></li>
        <li><a href="<%=Session["APP_BASE_DOMAIN"]%>privacy.aspx">Privacy</a></li> </ul>
        <%}
      else
      { %>  
            <ul class="yt-NavFooter">
        <li id="liFooterAbout"><a href="<%=Session["APP_BASE_DOMAIN"]%>about.aspx">About Your Tribute</a></li>
        <li id="liFooterContact"><a href="<%=Session["APP_BASE_DOMAIN"]%>contact.aspx">Contact Us</a></li>
        <li id="liFooterHelp"><a href="http://support.yourtribute.com" target="_blank">Help & Support</a></li> 
               </ul>
         <%} %>    
   
</div>
</div>
<%if (ConfigurationManager.AppSettings["ApplicationType"].ToLower() == "yourtribute") %>
    <%{ %>
<div id="CopyRightDiv" runat="server" style="margin: -38px 20px 0 7px;float:right">
<div id="CopyRightSize" runat="server">
        <div class="yt-FloatLeft">
        &copy;<asp:Label ID="lblCopyRightyt" runat="server" Text=""></asp:Label>&nbsp;<span
            id="copyRight" runat="server">Your Tribute, Inc. All Rights Reserved.</span>
            </div>
        <div class="copyRightDivLinks">
            <ul class="yt-FloatLeft">
                <li id="copyRightDivLiHome"><a href="<%=Session["APP_BASE_DOMAIN"]%>">Home</a></li>
                <li id="copyRightDivLiTerms"><a href="<%=Session["APP_BASE_DOMAIN"]%>termsofuse.aspx">Terms of Use</a></li>
                <li id="copyRightDivLiPrivacy" ><a href="<%=Session["APP_BASE_DOMAIN"]%>privacy.aspx">Privacy Policy</a></li>
            </ul>
        </div>
        </div> 
        </div>       
 <%} %>   
 </div>