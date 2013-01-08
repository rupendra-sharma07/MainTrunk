<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PhotoGallery.aspx.cs" Inherits="Photo_PhotoGallery"
    Title="Photos" MasterPageFile="~/Shared/Story.master" EnableEventValidation="true" %>
<%@ MasterType  virtualPath="~/Shared/Story.master" %>

<asp:Content ID="content1" ContentPlaceHolderID="TabContentPlaceHolder" runat="Server">
    <div class="yt-Breadcrumbs">
        <a id="aTributeHome" runat="server"><%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> Home</a> <span class="selected">Photos</span>
    </div>
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ModuleContentPlaceHolder" runat="Server">
    <div>

        <script type="text/javascript">
            <% if (Equals(Request.QueryString["post_on_facebook"], "True")) { %>
            $(document).addEvent('fb_connected', function() {
                var attachments = {
                    name: '<asp:Literal ID="wallSubject" runat="server" Text="Test" />', 
                    href: '<asp:Literal ID="photoAlbumLink" runat="Server" Text="" />', 
                    caption: '<b>Website:</b> <asp:Literal ID="turl" runat="Server" Text="" />', 
                    description: '<b>Photo Album:</b> <asp:Literal id="pa_caption" runat="server" text="" />', 
                    media: [<asp:Literal ID="mediaJson" runat="Server" Text="{type:'image', src: 'http://www.example.com', href:'http://www.example.com'}" />]
                };
                var action_link = [{
                    text: 'Visit <%= _tributeType %> Website',
                    href: '<asp:Literal ID="photoAlbumLink2" runat="Server" Text="" />'
                }];
                publish_stream("", attachments, action_link, null, null, function() {});
            });
            <% } %>
        </script>

    </div>
    <div id="divShowModalPopup">
    </div>
    <div class="yt-ContentPrimary">
        <div class="yt-Panel-Primary">
            <h2 id="hPhoto" runat="server"><%=strPhotoAlbumHeader%></h2>
            <div id="divNoAlbum" runat="server">
            <p id="pNoRecord" runat="server">No albums exist!</p>
            <p id="pClick" runat="server">Click the button to create and album and share your photos:</p>
            <div id="divButton" runat="server" class="yt-Form-Buttons">
                <div class="yt-Form-Submit">
                    <asp:LinkButton ID="lbtnAddPhoto" runat="server" CssClass="yt-Button yt-ArrowButton" OnClick="lbtnAddPhoto_Click"></asp:LinkButton>
                </div>
            </div>
            </div>
            <div id="divPhoto" runat="server" class="yt-MediaList">
                <div id="divPagingTop" runat="server" class="yt-ListHead">
                    <span id="spRecordCountTop" runat="server"></span>
                    <div class="yt-Pagination">
                        <span id="spPageTop" runat="server"></span><span id="spPagingTop" runat="server"></span>
                    </div>
                </div>
                <div class="yt-Form-Field" id="divLogin" runat="server"></div>    
                <ul class="yt-ListBody">
                    <asp:Repeater ID="repPhotoAlbumList" runat="server">
                        <ItemTemplate>
                            <li>
                                <h4>
                                    <%#Eval("PhotoAlbumCaption")%>
                                </h4>
                                <span class="yt-Meta-Count">
                                    <%#Eval("PhotoCount")%>
                                    <%=strPhotosCount %></span> <a href="photoalbum.aspx?PhotoAlbumId=<%#Eval("PhotoAlbumId") %>" class="yt-Thumb">
                                        <img src="<%#Eval("PhotoImage")%>" alt="" /></a> <span class="yt-Meta-Date">
                                            <%=strCreated%>
                                            <%#Eval("CreationDate")%>
                                        </span><span class="yt-Meta-Updated"><%=strUpdated%>
                                            <%#Eval("ModificationDate")%>
                                        </span><span class="yt-Meta-Author"><%=strCreatedBy %> <a href="#" onclick="UserProfileModal_1('<%# Eval("CreatedBy")%>');">
                                            <%#Eval("UserName")%>
                                        </a></span>
                                        <span id="spComment" runat="server"><a href="photoalbum.aspx?PhotoAlbumId=<%#Eval("PhotoAlbumId") %>" class="yt-Comments">
                                            <%#Eval("CommentCount")%>
                                            <%=strCommentsCount %></a></span>
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
