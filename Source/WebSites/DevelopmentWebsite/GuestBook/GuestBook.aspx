<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GuestBook.aspx.cs" Inherits="GuestBook_GuestBook"
    Title="Guestbook" MasterPageFile="~/Shared/Story.master" ValidateRequest="false" %>

<%@ MasterType VirtualPath="~/Shared/Story.master" %>
<asp:Content ID="content1" ContentPlaceHolderID="TabContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        FB.init({ appId: '<%= ConfigurationManager.AppSettings["ApiKey"].ToString() %>', status: true, cookie: true, xfbml: true, oauth: true });
        FB.getLoginStatus(function(response) {
            if (response.authResponse) {
                update_user_is_connected();
            }
        });

        $(document).addEvent('fb_connected', function() {
            if ($('facebook_share_container')) $('facebook_share_container').setStyle('display', 'block');
            if (navigator.userAgent.indexOf("MSIE 7.0") != -1) {
                if (document.getElementById('ctl00_ModuleContentPlaceHolder_divSubmitbutton') != null && document.getElementById('facebook_share_container') != null) {
                    if (document.getElementById('facebook_share_container').style.display == 'block') {

                        document.getElementById('ctl00_ModuleContentPlaceHolder_divSubmitbutton').className = "yt-GuestBook-SubmitBtnIE7";
                    }
                }
            }
            replace_submit_with_stream('<%=btnPost.ClientID%>', 'facebook_share', get_msg, get_attachments, get_action_link);
        });


        function get_msg() {
            return "";
        };

        function get_attachments() {
            var ret = {
                name: '<asp:Literal id="gbWallPostSubject" Text="false" runat="server" />', //Matt added a guestbook message to the: Jon Stiles & Mary Smith Wedding Tribute
                href: '<asp:Literal id="gbWallLink" Text="false" runat="server" />', //guestbook link
                caption: '<b>Website:</b> <asp:Literal id="gbWallTributeHome" Text="false" runat="server" />',
                description: "<b>Message:</b> " + $('<%=txtMessage.ClientID%>').value, // link to particular tribute
                media: [{
                    type: "image",
                    src: '<asp:Literal id="gbWallTributeImage" Text="false" runat="server" />', //tribute photo
                    href: '<asp:Literal id="gbWallLink1" Text="false" runat="server" />' //guestbook link
}]
                };
                //console.log(ret);
                return ret;
            };
            function get_action_link() {
                var ret = [{ text: "Visit <%= _tributeType %> Tribute", href: '<asp:Literal id="gbWallTributeHome1" Text="false" runat="server" />'}]; //vist tribute_type tribute (link to tribute_type homepage)
                //console.log(ret)
                return ret;
            };


            //to restrict the value of Video Descritpion to 2000 characters
            //this is to restrict characters after 2000 characters
            function maxLength() {
                var txtVal = $('ctl00_ModuleContentPlaceHolder_txtMessage').value;
                return chkForMaxLength(3000, txtVal.length);
            }
            //this is for checking the length after clicking on post button
            function maxLength2(source, args) {
                var txtVal = $('ctl00_ModuleContentPlaceHolder_txtMessage').value;
                args.IsValid = chkForMaxLength(3000, txtVal.length);
            }

            function CheckGuestBookCommentLength() {
                var textarea = document.getElementById('<%=txtMessage.ClientID%>');
                ValidateStoryLength(textarea, 3000);
            }

            // function to add value of  name and message textbox to session : by Rupendra
            function setSessionMsg() {
                var name = document.getElementById('<%=txtUserName.ClientID%>');
                var messg = document.getElementById('<%=txtMessage.ClientID%>');
                document.getElementById('dvError').innerHTML = "<h2>Oops - there was a problem with your guestbook message.</h2><br><h3>Please correct the error(s) below:</h3>"
                var flag = 0;
                if (messg != null && name != null) {
                    if (messg.value == "" || messg.value == "Message") {
                        flag += 1;
                        document.getElementById('<%=spanErrorMessage.ClientID%>').innerHTML = "!";
                        document.getElementById('dvError').innerHTML += "<ul><li>Please enter a message.</li></ul>";
                        document.getElementById('dvError').style.display = "block";
                    }
                    else {
                        document.getElementById('<%=spanErrorMessage.ClientID%>').innerHTML = "";

                    }
                    if (name.value == "" || name.value == "Name") {
                        flag += 1;
                        document.getElementById('<%=spanErrorName.ClientID%>').innerHTML = "!";
                        document.getElementById('dvError').innerHTML += "<ul><li>Please enter your name.</li></ul>";
                        document.getElementById('dvError').style.display = "block";
                    }

                    if (flag > 0) {

                        return false;

                    }
                    if (flag == 0) {
                        document.getElementById('dvError').style.display = "none";
                        document.getElementById('<%=spanErrorName.ClientID%>').innerHTML = "";
                        document.getElementById('<%=spanErrorMessage.ClientID%>').innerHTML = "";
                        if (GuestBook_GuestBook)
                            GuestBook_GuestBook.SetSessionValues(name.value, messg.value);
                        PostMessageModalpopup(location.href, document.title);
                        return false;

                    }

                }
                return false;
            }

            //for WordPress
            function setIsInTopurl() {
                // LHK: for topurl WordPress
                var IsInTopurl = (window.location != window.parent.location);
                {
                    GuestBook_GuestBook.SetSessionTopurl(IsInTopurl);
                }

            }


            //  // function to validate  message textbox  : by Rupendra
            function validateInput() {
                //LHK:function call for getting value of is iniframe
                setIsInTopurl();
                var messg = document.getElementById('<%=txtMessage.ClientID%>');
                document.getElementById('dvError').innerHTML = "<h2>Oops - there was a problem with your guestbook message.</h2><br><h3>Please correct the error(s) below:</h3>"
                var flag = 0;
                if (messg != null) {
                    if (messg.value == "" || messg.value == "Message") {
                        flag = 1;
                        document.getElementById('<%=spanErrorMessage.ClientID%>').innerHTML = "!";
                        document.getElementById('dvError').innerHTML += "<ul><li>Please enter a message.</li></ul>";
                        document.getElementById('dvError').style.display = "block";
                    }
                    else {
                        document.getElementById('<%=spanErrorMessage.ClientID%>').innerHTML = "";
                        document.getElementById('dvError').style.display = "none";
                    }

                    if (flag == 1) {

                        return false;
                    }
                    else return true;

                }
            }
    </script>

    <!--yt-NavPrimary-->
    <div class="yt-Breadcrumbs">
        <a id="aTributeHome" runat="server"><%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> Home</a> <span class="selected">Guestbook</span>
    </div>
</asp:Content>
<asp:Content ID="content" ContentPlaceHolderID="ModuleContentPlaceHolder" runat="Server">
 <div id="divShowModalPopup">
    </div>
    <div class="yt-ContentPrimary">
        <div style="color: Black; width: 622px; display: none" class="yt-Error" id="dvError">
            <%-- <asp:ValidationSummary CssClass="yt-Error" ID="PortalValidationSummary" runat="server"
                HeaderText=" <h2>Oops - there was a problem with your message.</h2></br><h3>Please correct the error(s) below:</h3>"
                ForeColor="Black" Width="622px" ValidationGroup="Comments" />--%>
        </div>
        <div class="yt-Panel-Primary">
            <h2>
                Guestbook</h2>
            <fieldset class="yt-Form" style="width: 800px">
                <label for="txtMessage" id="lblMessage" runat="server" class="yt-Bullet">
                </label>
                <div class="yt-GuestbookForm-Field" id="divPostMessage" runat="server">
                    <div class="yt-ItemThumb">
                   <%-- <div style="width: 80px; float: left; margin-top: -2px; margin-left: 14px;" id="dvPicUser"
                        runat="server">--%>
                        <%--<a id="aimguser" runat="server" href="javascript:void(0);" class="yt-Thumb" style="cursor: default;">
                            </a><img class="yt-ItemPhoto" alt="" runat="server" id="imgUser" width="54" height="54" />--%>
                            <%--<span class="yt-ItemPhoto" id="facbookImage" runat="server" width="54" height="54" ></span>--%>
                        
                        <%-- <div id="fbimage" runat="server" ></div>--%>
                    <%--</div>--%>
                    <%-- </div>--%>
                    <div id="divImage" runat="server" style="width: 80px; float: left; margin-top: -2px; margin-left: 14px;">                    
                    </div>
                    </div>
                    <asp:TextBox ID="txtMessage" MaxLength="3000" CssClass="yt-Form-Textarea-XLong" 
                        Text="Message" Rows="6" runat="server" TextMode="MultiLine" 
                         onfocus="if(this.value=='Message') {this.value='';this.style.color = '#949494';this.style.fontStyle='normal';}"
                        onblur="if(this.value=='') {this.value='Message';this.style.color = '#949494';this.style.fontStyle='normal';}"
                        onkeypress="this.style.color = 'black';this.style.fontStyle='normal'; return funcTextCount(this);"></asp:TextBox><span
                            id="spanErrorMessage" runat="server" style="color: Red; font-weight: bold; margin-left: 2px;"></span>
                    <div id="divAuthUser" runat="server" class="yt-guestbook-authUser">
                        <img runat="server" id="imgAppLogo" class="yt-guestbook-imguser"  />
                        <div style="width:auto" >
                            <label id="lblUserName" runat="server" style="width: 200px; font-size: 12px; margin-left: 16px">
                            </label>
                        </div>
                    </div>
                    <div  class="yt-guestbook-unauthUser"
                        id="divUnAuthUser" runat="server">
                        <asp:TextBox Style="margin-left: 100px; color: #949494;" ID="txtUserName" CssClass="yt-Form-Input"
                            Width="225px" Text="Name" onfocus="if(this.value=='Name') {this.value='';this.style.color = '#949494';}"
                            onBlur="if(this.value=='') {this.value='Name';this.style.color = '#949494';}"
                            onkeypress="this.style.color = 'black';" runat="server" Height="19"></asp:TextBox>
                        <span id="spanErrorName" runat="server" style="color: Red; font-weight: bold;"></span>
                    </div>
                    <div id="facebook_share_container" style="float: left; margin-top: 6px; display: none;">
                        <input type="checkbox" id="facebook_share" checked="checked" style="margin-top: 5px;" /><label
                            for="facebook_share" style="display: inline; font-size: 12px; top: -2px; position:relative; margin-right:1px">Share
                            on Facebook</label></div>
                    <div style="width: 200px; text-align: center; float: left; display: none">
                        <p class="yt-messageRemaining">
                            <span id="numberRemaining" runat="server">3000 characters remaining.</span></p>
                    </div>
                    <div class="yt-GuestBook-SubmitBtn"  id="divSubmitbutton" runat="server">
                        <asp:LinkButton ID="btnPost" Width="90" Height="20" runat="server" CssClass="yt-Button yt-ArrowButton"
                            Text="Post message" ValidationGroup="Comments" OnClick="btnPost_Click" />
                    </div>
                </div>
                <!-- Added new on 20-jun-2011 for YT phase 4 -->
                <%-- <div class="yt-Form-Field" id="divLogin" runat="server" >
                </div>--%>
            </fieldset>
            <div id="divMessage" runat="server" class="yt-ListBody">
            </div>
            <hr class="yt-Line" />
            <div id="divComments" runat="server" class="yt-MessageList">
                <div id="divPagingTop" runat="server" class="yt-ListHead" style="margin-top: -14px;
                    margin-bottom: 0px;">
                    <span id="spanRecordCountTop" runat="server"></span>
                    <div class="yt-Pagination" style="height: 0px;">
                        <span id="Page">Page:</span> <span id="spanPagingTop" runat="server"></span>
                    </div>
                </div>
                <fieldset style="margin-top: -28px;">
                    <div id="divCommentList" runat="server" class="yt-ListBody">
                        <asp:DataList ID="dlComments" DataKeyField="UserType" runat="server" OnDeleteCommand="dlComments_DeleteCommand"
                            OnItemDataBound="dlComments_ItemDataBound">
                            <ItemTemplate>
                                <div class="yt-ListItem" style="margin-top: -3px;">
                                    <h6>
                                        <a href="javascript:void(0);" runat="server" id="anchrUserName" class="yt-ListName">
                                            <%# DataBinder.Eval(Container.DataItem, "userName").ToString().Trim()%></a>
                                        <%--<span runat="server" id="spnAddrs"></span>--%>
                                        <%# DataBinder.Eval(Container.DataItem, "Location") %>
                                    </h6>
                                    <span class="yt-ItemPhoto" runat="server" id="itemProfilePicSpn"></span>
                                    <img class="yt-ItemPhoto" runat="server" id="itemProfilePicImg" src="" />
                                    <p class="yt-ItemDate">
                                        <%# DataBinder.Eval(Container.DataItem, "CreatedDate") %>
                                    </p>
                                    <p>
                                        <%# DataBinder.Eval(Container.DataItem, "Message") %>
                                    </p>
                                    <asp:LinkButton ID="btnDelete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "COMMENTID") %>'
                                        TableType='<%# DataBinder.Eval(Container.DataItem, "tableType")%>' runat="server"
                                        CssClass="yt-MiniButton yt-DeleteButton" Text="Delete" CommandName="Delete" CausesValidation="false" />
                                    <asp:HiddenField ID="hdnUserId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "UserId") %>' />
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </fieldset>
                <div id="divPagingFoot" runat="server" class="yt-ListFoot" style="margin-top: -8px">
                    <span id="spanRecordCountBottom" runat="server"></span>
                    <div class="yt-Pagination">
                        <span>Page:</span> <span id="spanPagingBottom" runat="server"></span>
                    </div>
                </div>
            </div>
        </div>
        <% if (this.Master._packageId == 8 || this.Master._packageId==3)
           { %>
        <div class="yt-GoogleAdBox-BottomSmall" id="BannerAdBoxBottom" runat="server">
            <div class="yt-Scissors">
            </div>
            <div class="yt-GoogleAdContent">
                <div>

                    <script type='text/javascript'>
                                <% if (Request.Url.ToString().ToLower().Contains("wedding"))
                                   {%>                               
                                   
                                    GA_googleFillSlot("YourTribute_Wedding_Guestbook_Bottom_468x60");
                               
                                <% } %>
                                <% else if (Request.Url.ToString().ToLower().Contains("anniversary"))
                                    {%>
                                
                                     GA_googleFillSlot("YourTribute_Anniversary_Guestbook_Bottom_468x60");
                               
                                <% } %>
                                <% else if (Request.Url.ToString().ToLower().Contains("graduation"))
                                    {%>
                               
                                    GA_googleFillSlot("YourTribute_Graduation_Guestbook_Bottom_468x60");
                                    
                                <% } %>
                                <% else if (Request.Url.ToString().ToLower().Contains("memorial"))
                                    {%>
                                
                                     GA_googleFillSlot("YourTribute_Memorial_Guestbook_Bottom_468x60");
                                   
                                <% } %>
                                <% else if (Request.Url.ToString().ToLower().Contains("baby"))
                                    {%>
                               
                                     GA_googleFillSlot("YourTribute_Baby_Guestbook_Bottom_468x60");
                                   
                                <% } %>
                                <% else if (Request.Url.ToString().ToLower().Contains("birthday"))
                                    {%>                                
                                     GA_googleFillSlot("YourTribute_Birthday_Guestbook_Bottom_468x60");                                 
                                <% } %>
                    </script>

                </div>
                <p class="infoMessage">
                    *Upgrade this <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%> to remove this advertisement</p>
            </div>
        </div>
        <% } %>
    </div>
    <!--yt-ContentPrimary-->
    <%--script to validate User name and Comment Message--%>

    <script language="javascript" type="text/javascript">

        var txtName, txtMessage;
        function Initialize_Controls() {
            txtName = document.getElementById('<%= txtUserName.ClientID%>');
            txtMessage = document.getElementById('<%= txtMessage.ClientID%>');
        }
        function Validate_Comments() {
            Initialize_Controls();
            if (txtMessage.value.length == 0 && txtMessage.value != " ") {
                alert('Fill your comments');
                txtMessage.focus();
                return false;
            }
            if (txtName.value.length == 0 && txtName.value == "Name") {

                if (document.getElementById('<%= divUnAuthUser.ClientID %>').style.display != "none") {
                    alert('Fill your name');
                    txtName.focus();
                }
                return false;
            }

        }

        function funcTextCount(txtMsg) {
            if (txtMsg.value.length == 3000) {
                return false;

            }
            else {

                return true;
            }
        }
    
    </script>

</asp:Content>
