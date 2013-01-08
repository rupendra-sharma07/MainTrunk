<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PhotoAlbum.aspx.cs" Inherits="Photo_PhotoAlbum"
    Title="Photo Album" MasterPageFile="~/Shared/Story.master" %>
<%@ MasterType  virtualPath="~/Shared/Story.master" %>
<asp:Content ID="content1" ContentPlaceHolderID="TabContentPlaceHolder" runat="Server">
    <div id="nvgPhotoAlbum" runat="server" class="yt-Breadcrumbs">
        <a id="aTributeHome" runat="server"><%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> Home</a> <a href="photos.aspx">Photos</a> 
                <span class="selected"><%=photoAlbumCaption%></span>
    </div>
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ModuleContentPlaceHolder" runat="Server">
<div id="divShowModalPopup"></div>
    <div class="yt-ContentPrimary">
        <div class="yt-Panel-Primary">
            <h2 id="hPhoto" runat="server"><%=strPhotoHeader%></h2>
            <h3 id="hAlbumCaption" runat="server"></h3>
            <p id="pAlbumDesc" runat="server"></p>
            <p id="pCreatedBy" runat="server"></p>
            <p id="pNoRecord" runat="server"></p>
            <p id="pClick" runat="server"></p>
            <div class="yt-Form-Buttons" id="divCreateAlbum" runat="server">
                <div class="yt-Form-Submit">
                    <asp:LinkButton ID="lbtnCreateAlbum" runat="server" CssClass="yt-Button yt-ArrowButton" OnClick="lbtnCreateAlbum_Click"></asp:LinkButton>
                </div>
            </div>
            <div id="divPhotos" runat="server" class="yt-MediaList yt-AlbumView">
                <div id="divPagingTop" runat="server" class="yt-ListHead">
                    <span id="spRecordCountTop" runat="server"></span>
                    <div class="yt-Pagination">
                        <span id="spPageTop" runat="server"></span><span id="spPagingTop" runat="server"></span>
                    </div>
                </div>
                <ul class="yt-ListBody">
                    <asp:Repeater ID="rptPhotoList" runat="server">
                        <ItemTemplate>
                            <li>
                                <a href="photo.aspx?PhotoId=<%#Eval("PhotoId")%>" class="yt-Comments">
                                    <img src='<%#Eval("PhotoImage")%>' alt="" /></a> 
                                    <%#Eval("CommentCount")%> <%=strCommentsCount %></a> 
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
                <div id="divPagingBottom" runat="server" class="yt-ListFoot">
                    <span id="spRecordCountBottom" runat="server"></span>
                    <div class="yt-Pagination">
                        <span id="spPageBottom" runat="server"></span><span id="spPagingBottom" runat="server">
                        </span>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--yt-ContentPrimary-->
</asp:Content>
