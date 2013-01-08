<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TributeBlog.ascx.cs" Inherits="UserControl_TributeBlog" %>

<div class="yt-Panel-Body">
<asp:Literal ID="ltrlBlogs" runat="server"></asp:Literal>    
    <div class="yt-Form-Buttons">
        <div class="yt-Form-Submit">
            <a href="<%=TributesPortal.Utilities.WebConfig.BlogUrlMain%>" class="yt-Button yt-ArrowButton" target="_blank">View entire blog</a></div>
    </div>
</div>
