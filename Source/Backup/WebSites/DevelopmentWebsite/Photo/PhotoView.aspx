<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PhotoView.aspx.cs" Inherits="Photo_PhotoView"
    Title="PhotoView" MasterPageFile="~/Shared/Story.master" %>
<%@ MasterType  virtualPath="~/Shared/Story.master" %>
<asp:Content ID="content1" ContentPlaceHolderID="TabContentPlaceHolder" runat="Server">
    <div id="nvgPhoto" runat="server" class="yt-Breadcrumbs">
    </div>
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ModuleContentPlaceHolder" runat="Server">
    
    <script language="javascript" type="text/javascript">
        FB.init({ appId: '<%= ConfigurationManager.AppSettings["ApiKey"].ToString() %>', status: true, cookie: true, xfbml: true, oauth: true });
        FB.getLoginStatus(function(response) {
            if (response.authResponse) {
                update_user_is_connected();
            }
        });

        $(document).addEvent('fb_connected', function() {
            $('facebook_share_container').setStyle('display', 'block');

            replace_submit_with_stream('<%=lbtnPost.ClientID%>', 'facebook_share', get_msg, get_attachments, get_action_link);
        });
        function get_msg() {
            return "";
        };
        function get_attachments() {
//            var ret = {
//                name: "test name", //Matt commented on a video on the: Jon Stiles & Mary Smith Wedding Tribute
//                href: "http://www.eza.com/hi", //photo link
//                caption: "<b>Website:</b> http://www.example.com",
//                description: "<b>Comment:</b> " + $('<%=txtPhotoComment.ClientID%>').value,
//                media: [{
//                    type: "image",
//                    src: "http://your-tribute-dev.dyndns.org/TributePhotos/images/baby_TributePhoto.jpg", //thumbnail photo
//                    href: "http://www.exa.com/hihi" //photo link
//                }]
            //        };
            var ret = {
                name: '<asp:Literal id="photoWallPostSubject" Text="false" runat="server" />', //Matt commented on a video on the: Jon Stiles & Mary Smith Wedding Tribute
                href: '<asp:Literal id="photoWallLink" Text="false" runat="server" />', //video link
                caption: '<b>Website:</b> <asp:Literal id="photoWallTributeHome" Text="false" runat="server" />',
                description: "<b>Comment:</b> " + $('<%=txtPhotoComment.ClientID%>').value,
                media: [{
                    type: "image",
                    src: '<asp:Literal id="photoWallThumbnail" Text="" runat="server" />', //video thumbnail photo
                    href: '<asp:Literal id="photoWallLink1" Text="false" runat="server" />' //video link
}]
                };

        //console.log(ret);
        return ret;
    };
    function get_action_link() {
        //var ret = [{ text: "Visit tribute_type Tribute", href: "http://www.example.com"}]; //vist tribute_type tribute (link to tribute_type homepage)
        //console.log(ret)
        var ret = [{ text: 'Visit <asp:Literal id="photoWallTributeType" Text="false" runat="server" /> Tribute',
            href: '<asp:Literal id="photoWallTributeHome1" Text="false" runat="server" />'}]; //vist tribute_type tribute (link to tribute_type homepage)
      
        return ret;
    };
    
    //to restrict the value of Video Descritpion to 1000 characters
    //this is to restrict characters after 1000 characters
    function CheckmaxLength()
    {  
    
       var CommentId= document.getElementById('ctl00_ModuleContentPlaceHolder_txtPhotoComment');
       if(CommentId != null)
        {
        var txtVal = CommentId.value;
        if(txtVal != "" && txtVal.length >0)
        return chkForMaxLength(1000, txtVal.length);
        else
        return 0;
        }
    }
    //this is for checking the length after clicking on post button
    function maxLength2(source, args)
    {
       var CommentId= document.getElementById('ctl00_ModuleContentPlaceHolder_txtPhotoComment');
       if(CommentId != null)
        {
        var txtVal = CommentId.value;
        if(txtVal != "" && txtVal.length >0)
        args.IsValid = chkForMaxLength(1000, txtVal.length);
        else return 0;
        }
    }
    //for WordPress
    function setIsInTopurl(){
        // LHK: for topurl WordPress
         var IsInTopurl = (window.location != window.parent.location);
        {
            Photo_PhotoView.SetSessionTopurl(IsInTopurl);
        } 
    } 
    </script>
 <div id="divShowModalPopup"></div>
    <div class="yt-ContentPrimary">
        <asp:ValidationSummary ID="vsErrorSummary" runat="server" CssClass="yt-Error" HeaderText=" <h2>Oops - there was a problem with your comment.</h2></br><h3>Please correct the error(s) below:</h3>"
            ForeColor="Black" />
        <div class="yt-Panel-Primary">
            <h2 id="hPhoto" runat="server">Photo: View</h2>
            <h4 id="hPhotoCaption" runat="server"></h4>
                <div id="divRecordCount" runat="server" class="yt-MediaHeadCss">
                <span id="spRecordCount" runat="server" class="yt-FloatLeft"></span>
                    <a href="javascript:void(0)" onclick="startSlideShow('<%=xmlPath %>?id=12345', <%=recordNumber %>);"
                    class="yt-SlideLink">View Slideshow</a>
                <div id="divPrevious" runat="server" class="yt-PrevNextCss">
                
                 
                    <asp:LinkButton ID="lbtnPre" runat="server" CausesValidation="false" Enabled="false"
                        OnClick="lbtnPre_Click" CssClass="yt-PreviousCss"></asp:LinkButton>
                        <asp:LinkButton ID="lbtnNext" runat="server" CausesValidation="false" CssClass="yt-NextCss" 
                        OnClick="lbtnNext_Click"></asp:LinkButton>
                   
                </div>
            </div>
            <div id="divPhotoDisplay" runat="server" class="yt-MediaItem">
            </div>
            <div class="yt-MediaFoot">
                <p id="pPhotoDesc" runat="server"></p>
                <p id="pUploadedBy" runat="server"></p>
            </div>
            <hr class="yt-Line" />
            <fieldset class="yt-Form">
                <div id="divPostComment" runat="server" class="yt-Form-Field">
              
                    <label for="txtPhotoComment" id="lblToComment" runat="server" class="yt-Bullet"></label>
                    <asp:TextBox ID="txtPhotoComment" CssClass="yt-Form-Textarea-XLong" Columns="50"
                        Rows="6" runat="server" TextMode="MultiLine" onkeypress="return CheckmaxLength();" TabIndex="1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvMessage" runat="server" ControlToValidate="txtPhotoComment" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000">!</asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvMessage" runat="server" ClientValidationFunction="maxLength2" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000">!</asp:CustomValidator>
                
                    <div style="float:right;margin-right:7px;">
                   
                         <div id="facebook_share_container" style="float:left; margin-top:5px; display:none;">
                        <div style="float:left; width:20px; margin-top:2px;"> <input type="checkbox" id="facebook_share" checked="checked" /></div>
                        <div style="float:left;"> <label for="facebook_share">Share on Facebook</label></div></div>
                       <div style="float:left;">  <asp:LinkButton ID="lbtnPost" runat="server" CssClass="yt-Button yt-ArrowButton"
                            OnClick="lbtnPost_Click" TabIndex="2" /></div>
                  
                    </div>
                </div>
                                <div class="yt-Form-Field" id="divLogin" runat="server"></div>
            </fieldset>
            <div id="divComments" runat="server" class="yt-CommentList">
                <div id="divMessage" runat="server" style="visibility:hidden">
                </div>
                <div id="divPagingTop" runat="server" class="yt-ListHead">
                    <span id="spanRecordCountTop" runat="server"></span>
                    <div class="yt-Pagination">
                        <span id="spanPageTop" runat="server"></span><span id="spanPagingTop" runat="server">
                        </span>
                    </div>
                </div>
                <div id="divCommentsList" runat="server" class="yt-ListBody">
                <asp:Repeater ID="rptComments" runat="server" OnItemDataBound="rptComments_ItemDataBound" 
                        OnItemCommand="rptComments_ItemCommand">
                <ItemTemplate>
                    <div class="yt-ListItem">
                        <h6>
                            <a href="javascript:void(0);" onclick="UserProfileModal_1('<%# Eval("UserId")%>');" class="yt-ListName"><%# DataBinder.Eval(Container.DataItem, "userName") %></a>
                             <%# DataBinder.Eval(Container.DataItem, "Location") %>
                        </h6>
                        <span class="yt-ItemPhoto" runat="server" id="itemProfilePicSpn"></span> 
                        <img class="yt-ItemPhoto" runat="server" id="itemProfilePicImg" src=""/>
                           <%-- <img src="<%# DataBinder.Eval(Container.DataItem, "UserImage") %>" alt="Photo of <%# DataBinder.Eval(Container.DataItem, "userName") %>" width="48"
                                height="48" /></div>--%>
                        <p class="yt-ItemDate">
                            <%# DataBinder.Eval(Container.DataItem, "CreatedDate") %></p>
                        <p>
                            <%# DataBinder.Eval(Container.DataItem, "Message") %></p>
                        <asp:LinkButton ID="btnDelete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "COMMENTID") %>'
                                    runat="server" CssClass="yt-MiniButton yt-DeleteButton" Text="Delete" CommandName="Delete"
                                    CausesValidation="false" /> 
                        <asp:HiddenField ID="hdnUserId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "UserId") %>' />
                    </div>
                    </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="yt-ListFoot">
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
