<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageVideo.aspx.cs" EnableEventValidation="true"
    Inherits="Video_ManageVideo" MasterPageFile="~/Shared/Story.master" %>

<%@ MasterType VirtualPath="~/Shared/Story.master" %>
<asp:Content ID="content1" ContentPlaceHolderID="TabContentPlaceHolder" runat="Server">
    <div class="yt-Breadcrumbs">
        <a id="aTributeHome" runat="server">
            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
            Home</a> <a href="videos.aspx">Videos</a> <span id="spPageMode" runat="server" class="selected">
            </span>
    </div>
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ModuleContentPlaceHolder" runat="Server">
    <meta http-equiv="X-UA-Compatible" content="IE=8" />

    <script language="javascript" type="text/javascript">
        FB.init({ appId: '<%= ConfigurationManager.AppSettings["ApiKey"].ToString() %>', status: true, cookie: true, xfbml: true, oauth: true });
        FB.getLoginStatus(function(response) {
            if (response.authResponse) {
                update_user_is_connected();
            }
        });
           
            $(document).addEvent('fb_connected', function () {
                $('facebook_share_container').setStyle('display', 'block');

                replace_submit_with_stream('<%=lbtnPost.ClientID%>', 'facebook_share', get_msg, get_attachments, get_action_link);
            });
  
        function get_msg() {
            return "";
        };
        function get_attachments() {
            var ret = {
                name: '<asp:Literal id="videoWallPostSubject" Text="false" runat="server" />', //Matt commented on a video on the: Jon Stiles & Mary Smith Wedding Tribute
                href: '<asp:Literal id="videoWallLink" Text="false" runat="server" />', //video link
                caption: '<b>Website:</b> <asp:Literal id="videoWallTributeHome" Text="false" runat="server" />',
                description: "<b>Comment:</b> " + $('<%=txtVideoComment.ClientID%>').value,
                media: [{
                    type: "image",
                    src: '<asp:Literal id="videoWallThumbnail" Text="" runat="server" />', //video thumbnail photo
                    href: '<asp:Literal id="videoWallLink1" Text="false" runat="server" />' //video link
                }]
        };
        //console.log(ret);
        return ret;
    };
    function get_action_link() {
        var ret = [{ text: 'Visit <asp:Literal id="videoWallTributeType" Text="false" runat="server" /> Tribute', 
                     href: '<asp:Literal id="videoWallTributeHome1" Text="false" runat="server" />'}]; //vist tribute_type tribute (link to tribute_type homepage)
        //console.log(ret)
        return ret;
    };
    
    //to restrict the value of Video Descritpion to 1000 characters
    //this is to restrict characters after 1000 characters
    function maxLength()
    {   
        var txtVal = $('ctl00_ModuleContentPlaceHolder_txtVideoComment').value;
        return chkForMaxLength(1000, txtVal.length);
    }
    //this is for checking the length after clicking on post button
    function maxLength2(source, args)
    {
        var txtVal = $('ctl00_ModuleContentPlaceHolder_txtVideoComment').value;
        args.IsValid = chkForMaxLength(1000, txtVal.length);
    }
    function launch(url, name) 
    {
	    var win = window.open(url,name,'width=720,height=480,resizable=no,scrollbars=no');
	    win.focus();
	    return false;
    }
    //for WordPress
    function setIsInTopurl(){
        // LHK: for topurl WordPress
         var IsInTopurl = (window.location != window.parent.location);
        {
            Video_ManageVideo.SetSessionTopurl(IsInTopurl);
        } 
        
    }
    </script>

    <div id="divShowModalPopup">
    </div>
    <div class="yt-ContentPrimary">
        <div>
            <asp:ValidationSummary ID="vsErrors" CssClass="yt-Error" runat="server" HeaderText=" <h2>Oops - there was a problem with your comment.</h2></br><h3>Please correct the error(s) below:</h3>"
                ForeColor="Black" Width="622px" />
        </div>
        <div class="yt-Panel-Primary">
            <h2 id="hVideoView" runat="server">
            </h2>
            <h4 id="hVideoName" runat="server">
            </h4>
            <div id="divRecordCount" runat="server" class="yt-MediaHeadCss">
                <span id="spRecordCount" runat="server"></span>
                <div id="divPrevious" runat="server" class="yt-PrevNextCss marginLeftVideo">
                    <asp:LinkButton ID="lbtnPre" runat="server" CausesValidation="false" Enabled="false"
                        OnClick="lbtnPre_Click" CssClass="yt-PreviousCss"></asp:LinkButton>
                    <asp:LinkButton ID="lbtnNext" runat="server" CausesValidation="false" CssClass="yt-NextCss"
                        OnClick="lbtnNext_Click"></asp:LinkButton>
                </div>
            </div>
            <div id="divVideoDisplay" runat="server" class="yt-MediaItem">
            </div>
            <div id="divDesc" runat="server" class="yt-MediaFoot">
            </div>
            <div id="divExpired" runat="server" class="yt-MediaFoot">
            </div>
            <hr class="yt-Line" />
            <fieldset class="yt-Form">
                <div id="divPostComment" runat="server" class="yt-Form-Field">
                    <label for="txtVideoComment" id="lblToComment" runat="server" class="yt-Bullet">
                    </label>
                    <asp:TextBox ID="txtVideoComment" CssClass="yt-Form-Textarea-XLong" Columns="50"
                        Rows="6" runat="server" TextMode="MultiLine" onkeypress="return maxLength()"
                        TabIndex="1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvMessage" runat="server" ControlToValidate="txtVideoComment"
                        Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000">!</asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvMessage" runat="server" ClientValidationFunction="maxLength2"
                        Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000">!</asp:CustomValidator>
                    <div class="yt-Form-Submit">
                        <asp:LinkButton ID="lbtnPost" runat="server" CssClass="yt-Button yt-ArrowButton"
                            OnClick="lbtnPost_Click" TabIndex="2" />
                    </div>
                    <div id="facebook_share_container" style="float: right; margin-top: 7px; display: none">
                        <input type="checkbox" id="facebook_share" checked="checked" /><label for="facebook_share"
                            style="display: inline">Share on Facebook</label></div>
                </div>
                <div class="yt-Form-Field" id="divLogin" runat="server">
                </div>
            </fieldset>
            <div id="divComments" runat="server" class="yt-CommentList">
                <div id="divMessage" runat="server" style="visibility: hidden">
                </div>
                <div id="divPagingTop" runat="server" class="yt-ListHead">
                    <span id="spanRecordCountTop" runat="server"></span>
                    <div class="yt-Pagination">
                        <span id="spanPageTop" runat="server"></span><span id="spanPagingTop" runat="server">
                        </span>
                    </div>
                </div>
                <div id="divCommentsList" runat="server" class="yt-ListBody">
                    <asp:DataList ID="dlComments" DataKeyField="UserType" runat="server" OnDeleteCommand="dlComments_DeleteCommand"
                        OnItemDataBound="dlComments_ItemDataBound">
                        <ItemTemplate>
                            <div class="yt-ListItem">
                                <h6>
                                    <a href="javascript:void(0);" onclick="UserProfileModal_1('<%# Eval("UserId")%>');"
                                        class="yt-ListName">
                                        <%# DataBinder.Eval(Container.DataItem, "userName") %></a>
                                    <%# DataBinder.Eval(Container.DataItem, "Location") %>
                                </h6>
                                <span class="yt-ItemPhoto" runat="server" id="itemProfilePicSpn"></span>
                                <img src="" class="yt-ItemPhoto" runat="server" id="itemProfilePicImg" />
                                <p class="yt-ItemDate">
                                    <%# DataBinder.Eval(Container.DataItem, "CreatedDate") %>
                                </p>
                                <p>
                                    <%# DataBinder.Eval(Container.DataItem, "Message") %>
                                </p>
                                <asp:LinkButton ID="btnDelete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "COMMENTID") %>'
                                    runat="server" CssClass="yt-MiniButton yt-DeleteButton" Text="Delete" CommandName="Delete"
                                    CausesValidation="false" />
                                <asp:HiddenField ID="hdnUserId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "UserId") %>' />
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                    <div class="yt-ListFoot">
                    </div>
                </div>
                <div id="divPagingBottom" runat="server" class="yt-ListHead">
                    <span id="spanRecordCountBottom" runat="server"></span>
                    <div class="yt-Pagination">
                        <span id="spanPageBottom" runat="server"></span><span id="spanPagingBottom" runat="server">
                        </span>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--yt-ContentPrimary-->
</asp:Content>
