<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LeftFeaturedPanel.ascx.cs" Inherits="UserControl_LeftFeaturedPanel" %>

   <div id="LeftFeaturedPnl" runat="server" class="yt-Panel-Body" visible="false">
                            <h2 >
                                Types of Websites...</h2>
                            <p >
                                You can create a website to celebrate a significant event or a special someone.</p>
                            
    <% if (TributesPortal.Utilities.WebConfig.ApplicationMode.Equals("local")) { %>
    <ul class="yt-TypeList">
        <li class="yt-TypeList-NewBaby"><a href="<%=Session["APP_PATH"]%>channelhomepage.aspx?Type=New Baby&Theme='BabyDefault'">New Baby</a></li>
        <li class="yt-TypeList-Wedding"><a href="<%=Session["APP_PATH"]%>channelhomepage.aspx?Type=Wedding&Theme='WeddingDefault'">Wedding</a></li>
        <li class="yt-TypeList-Anniversary"><a href="<%=Session["APP_PATH"]%>channelhomepage.aspx?Type=Anniversary&Theme='AnniversaryDefault'">Anniversary</a></li>
        <li class="yt-TypeList-Graduation"><a href="<%=Session["APP_PATH"]%>channelhomepage.aspx?Type=Graduation&Theme='GraduationDefault'">Graduation</a></li>
        <li class="yt-TypeList-Birthday"><a href="<%=Session["APP_PATH"]%>channelhomepage.aspx?Type=Birthday&Theme='BirthdayDefault'">Birthday</a></li>
    </ul>
    <% } else { %>
    <ul class="yt-TypeList">
        <li class="yt-TypeList-NewBaby"><a href="http://newbaby.<%=TributesPortal.Utilities.WebConfig.TopLevelDomain%>">New Baby</a></li>
        <li class="yt-TypeList-Wedding"><a href="http://wedding.<%=TributesPortal.Utilities.WebConfig.TopLevelDomain%>">Wedding</a></li>
        <li class="yt-TypeList-Anniversary"><a href="http://anniversary.<%=TributesPortal.Utilities.WebConfig.TopLevelDomain%>">Anniversary</a></li>
        <li class="yt-TypeList-Graduation"><a href="http://graduation.<%=TributesPortal.Utilities.WebConfig.TopLevelDomain%>">Graduation</a></li>
        <li class="yt-TypeList-Birthday"><a href="http://birthday.<%=TributesPortal.Utilities.WebConfig.TopLevelDomain%>">Birthday</a></li>
    </ul>    
    <% } %>
      <div class="yt-Form-Buttons">
                                <a href='<%=Session["APP_BASE_DOMAIN"]%>pricing.aspx' class="yt-Button yt-ArrowButton">
                                    Create a new Website!</a>
                            </div>
                        </div>   
    
    
 
                            
                            
                       


