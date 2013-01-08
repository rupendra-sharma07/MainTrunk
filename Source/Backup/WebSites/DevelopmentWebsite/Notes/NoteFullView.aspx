<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NoteFullView.aspx.cs" Inherits="Notes_NoteFullView"
    Title="Note" MasterPageFile="~/Shared/Story.master" %>
<%@ MasterType  virtualPath="~/Shared/Story.master" %>
<asp:Content ID="content1" ContentPlaceHolderID="TabContentPlaceHolder" runat="Server">
    <div class="yt-Breadcrumbs">
        <a id="aTributeHome" runat="server"><%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> Home</a> <a href="notes.aspx?noteId=<%=_noteId %>">
            Notes</a> <span class="selected">
                <%=_noteTitle%>
            </span>
    </div>
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ModuleContentPlaceHolder" runat="Server">
    <script language="javascript" type="text/javascript">

        $(document).addEvent('fb_connected', function() {
            $('facebook_share_container').setStyle('display', 'block');

            replace_submit_with_stream('<%=lbtnPost.ClientID%>', 'facebook_share', get_msg, get_attachments, get_action_link);
        });
        function get_msg() {
            return "";
        };
        function get_attachments() {
            var ret = {
                name: '<asp:Literal id="noteCommentWallPostSubject" Text="false" runat="server" />', //Matt commented on a video on the: Jon Stiles & Mary Smith Wedding Tribute
                href: '<asp:Literal id="noteWallLink" Text="http://www.eza.com/hi" runat="server" />', //photo link
                caption: '<b>Website:</b> <asp:Literal id="noteWallTributeHome" Text="http://www.example.com" runat="server" />',
                description: "<b>Comment:</b> " + $('<%=txtMessage.ClientID%>').value,
                media: [{
                    type: "image",
                    src: '<asp:Literal id="noteWallTributeImage" Text="http://your-tribute-dev.dyndns.org/TributePhotos/images/baby_TributePhoto.jpg" runat="server" />', 
                    href: '<asp:Literal id="noteWallLink1" Text="http://www.exa.com/hihi" runat="server" />' 
                }]
            };
            //console.log(ret);
            return ret;
        };
    function get_action_link() {
        var ret = [{ text: 'Visit <asp:Literal id="noteWallTributeType" Text="false" runat="server" /> Tribute', 
                     href: '<asp:Literal id="noteWallTributeHome1" Text="http://www.example.com" runat="server" />'}]; 
        //console.log(ret)
        return ret;
    };
    
    //to restrict the value of Video Descritpion to 1000 characters
    //this is to restrict characters after 1000 characters
    function maxLength()
    {   
        var txtVal = $('ctl00_ModuleContentPlaceHolder_txtMessage').value;
        return chkForMaxLength(1000, txtVal.length);
    }
    //this is for checking the length after clicking on post button
    function maxLength2(source, args)
    {
        var txtVal = $('ctl00_ModuleContentPlaceHolder_txtMessage').value;
        args.IsValid = chkForMaxLength(1000, txtVal.length);
    }
    </script>
<div id="divShowModalPopup"></div>
    <div class="yt-ContentPrimary">
        <div>
            <asp:ValidationSummary ID="vsErrors" CssClass="yt-Error" runat="server" HeaderText=" <h2>Oops - there was a problem with your comment.</h2></br><h3>Please correct the error(s) below:</h3>"
                ForeColor="Black" Width="622px" />
        </div>
        <div class="yt-Panel-Primary">
            <h2>
                <asp:Label ID="lblNotesHeader" runat="server"></asp:Label></h2>
            <div class="yt-NoteList yt-NoteView">
                <div class="yt-ListItem">
                    <div class="yt-Form-Submit">
                        <asp:LinkButton ID="lbtnEditNote" CssClass="yt-Button yt-ArrowButton" runat="server"
                            OnClick="lbtnEditNote_Click" CausesValidation="false"></asp:LinkButton>
                    </div>
                    <h4 id="hTitle" runat="server">
                    </h4>
                    <h6 id="hPostedBy" runat="server">
                    </h6>
                    <div id="divImage" runat="server" class="yt-ItemPhoto">
                    </div>
                    <p id="pDate" runat="server" class="yt-ItemDate">
                    </p><br/>
                    <p id="pMessage" runat="server">
                    </p><br/>
                </div>
            </div>
            <hr class="yt-Line" />
            <fieldset class="yt-Form">
                <div id="divPostComment" runat="server" class="yt-Form-Field">
                    <label for="txtMessage" id="lblMessage" runat="server" class="yt-Bullet">
                    </label>
                    <asp:TextBox ID="txtMessage" CssClass="yt-Form-Textarea-XLong" Columns="50" Rows="6"
                        runat="server" TextMode="MultiLine"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvMessage" runat="server" ControlToValidate="txtMessage"
                        Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000">!</asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvMessage" runat="server" ClientValidationFunction="maxLength2"
                        Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000">!</asp:CustomValidator>
                    <div class="yt-Form-Submit">
                        <asp:LinkButton ID="lbtnPost" runat="server" CssClass="yt-Button yt-ArrowButton"
                            OnClick="lbtnPost_Click" />
                    </div>
                    <div id="facebook_share_container" style="float:right;margin-top:7px;display:none"><input type="checkbox" id="facebook_share" checked="checked" /><label for="facebook_share" style='display:inline;'>Share on Facebook</label></div>
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
                                        <%# DataBinder.Eval(Container.DataItem, "userName") %>
                                    </a>
                                    <%# DataBinder.Eval(Container.DataItem, "Location") %>
                                </h6>
                                <span class="yt-ItemPhoto" runat="server" id="itemProfilePicSpn">
                                </span>
                                <img class="yt-ItemPhoto" runat="server" id="itemProfilePicImg" src="" />
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
