<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VideoGallery.aspx.cs" Inherits="Video_VideoGallery"
    Title="Videos" MasterPageFile="~/Shared/Story.master" %>
<%@ MasterType  virtualPath="~/Shared/Story.master" %>
<asp:Content ID="content1" ContentPlaceHolderID="TabContentPlaceHolder" runat="Server">
    <div class="yt-Breadcrumbs">
        <a id="aTributeHome" runat="server"><%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> Home</a> <span class="selected">Videos</span>
    </div>
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ModuleContentPlaceHolder" runat="Server">
<div id="divShowModalPopup"></div>
    <div class="yt-ContentPrimary">
        <div class="yt-Panel-Primary">
            <h2 id="hVideo" runat="server"></h2>
            <h3 id="hVideoTribute" runat="server"></h3>
            <div id="divVideoTribute" runat="server" class="yt-MediaList">
                <ul class="yt-ListBody">
                    <li>
                        <h4 id="hVideoTributeName" runat="server"><%=videoTributeName%></h4>
                        <a href="video.aspx?mode=view&videoType=videotribute&videoId=<%=videoTributeId%>" class="yt-VideoLink yt-Thumb">
                            <img src='<%=videoTributeImage %>' width="130" height="97" alt="" /><span class="yt-Click">Click to View</span></a>
                        <span class="yt-Meta-Date"><%=strCreatedOn %> <%=videoTributeCreatedOn%></span> <span class="yt-Meta-Author">
                            <%=strCreatedBy %> <a href="javascript:void(0);" onclick="UserProfileModal_1('<%=CreatorId%>');"><%=videoTributeCreatedBy %></a></span> <a href="managevideo.aspx?mode=view&videoType=videotribute&videoId=<%=videoTributeId%>" class="yt-Comments"><%=videoTributeComments%> <%=txtComment %></a>
                    </li>
                </ul>
            </div>
            <hr id="hrLine" runat="server" class="yt-Line" />
            <h3 id="hVideos" runat="server"></h3>
            <div id="divVideos" runat="server" class="yt-MediaList">
                <div id="divNoRecord" runat="server" visible="false"></div>
                <br />
                <div id="divLogin" runat="server"></div>
                <div id="divAddVideo" runat="server" class="yt-Form-Buttons">
                    <div class="yt-Form-Submit">
                        <asp:LinkButton ID="lbtnAddVideo" runat="server" CssClass="yt-Button yt-ArrowButton"
                            OnClick="lbtnAddVideo_Click"></asp:LinkButton>
                    </div>
                </div>
                <div id="divPagingTop" runat="server" class="yt-ListHead">
                    <span id="spRecordCountTop" runat="server"></span>
                    <div class="yt-Pagination">
                        <span id="spPageTop" runat="server"></span><span id="spPagingTop" runat="server"></span>
                    </div>
                </div>
                <ul id="ulVideos" runat="server" class="yt-ListBody">
                <asp:DataList ID="dlVideos" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table" RepeatColumns="3">
                    <ItemTemplate>
                    <li>
                        <h4>
                            <%#Eval("Videos.VideoCaption")%></h4>
                        <a href="video.aspx?mode=view&videoId=<%#Eval("Videos.VideoId")%>" class="yt-VideoLink yt-Thumb">
                            <img src='http://img.youtube.com/vi/<%#Eval("IdForDisplay")%>/default.jpg' alt="" onclick="javascript:window.location.href='video.aspx?mode=view&videoId=<%#Eval("Videos.VideoId")%>'" /><span class="yt-Click">Click to View</span></a>
                        <span class="yt-Meta-Date"><%=strCreatedOn %> <%#Eval("CreatedDate")%></span> <span class="yt-Meta-Author">
                            <%=strCreatedBy %> <a href="javascript:void(0);" onclick="UserProfileModal_1('<%# Eval("Videos.UserId")%>');"><%#Eval("UserName")%></a></span> <a href="video.aspx?mode=view&videoId=<%#Eval("Videos.VideoId")%>" class="yt-Comments"><%#Eval("CommentCount")%> <%=txtComment %></a>
                    </li>
                    </ItemTemplate>
                </asp:DataList>
                </ul>
                <div id="divPagingBottom" runat="server" class="yt-ListFoot">
                    <span id="spRecordCountBottom" runat="server"></span>
                    <div class="yt-Pagination">
                        <span id="spPageBottom" runat="server"></span>
                        <span id="spPagingBottom" runat="server"></span>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--yt-ContentPrimary-->
</asp:Content>