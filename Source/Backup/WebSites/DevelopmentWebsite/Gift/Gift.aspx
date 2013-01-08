<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Gift.aspx.cs" Inherits="Gift_Gift"
    Title="Gifts" MasterPageFile="~/Shared/Story.master" ValidateRequest="false" %>

<%@ MasterType VirtualPath="~/Shared/Story.master" %>
<asp:Content ID="content1" ContentPlaceHolderID="TabContentPlaceHolder" runat="Server">
    <!--<script language="javascript" type="text/javascript" src="../assets/scripts/modalbox.js"></script>-->

    <script language="javascript" type="text/javascript">
        FB.init({ appId: '<%= ConfigurationManager.AppSettings["ApiKey"].ToString() %>', status: true, cookie: true, xfbml: true, oauth: true });
        FB.getLoginStatus(function(response) {
            if (response.authResponse) {
                update_user_is_connected();
            }
        });

        $(document).addEvent('fb_connected', function() {
            if ($('facebook_share_container')) $('facebook_share_container').setStyle('display', 'block');

            replace_submit_with_stream('<%=lbtnSubmit.ClientID%>', 'facebook_share', get_msg, get_attachments, get_action_link);
        });

        function get_msg() {
            return "";
        };
        function get_attachments() {
            var ret = {
                name: '<asp:Literal id="giftWallPostSubject" Text="false" runat="server" />', //Matt gave a gift on the: Jon Stiles & Mary Smith Wedding Tribute
                href: '<asp:Literal id="giftWallLink" Text="false" runat="server" />', //gift page link
                caption: '<b>Website:</b> <asp:Literal id="giftWallTributeHome" Text="false" runat="server" />',
                description: "<b>Message:</b> " + $('<%=txtMessage.ClientID%>').value, // link to particular tribute
                media: [{
                    type: "image",
                    src: document.getElementById('<%=imgGiftImage.ClientID%>').src, //gift photo
                    href: '<asp:Literal id="giftWallLink1" Text="false" runat="server" />' //gift page link
}]
                };
                //console.log(ret);
                return ret;
            };
            function get_action_link() {
                var ret = [{ text: 'Visit <asp:Literal id="giftWallTributeType" Text="false" runat="server" /> Tribute', href: '<asp:Literal id="giftWallTributeHome1" Text="false" runat="server" />'}]; //vist tribute_type tribute (link to tribute_type homepage)
                //console.log(ret)
                return ret;
            };
            function maxLength() {
                var txtVal = $('ctl00_ModuleContentPlaceHolder_txtAnnMessage').value;
                return chkForMaxLength(200, txtVal.length);
            }
            //this is for checking the length after clicking on post button
            function maxLength2(source, args) {
                var txtVal = $('ctl00_ModuleContentPlaceHolder_txtAnnMessage').value;
                args.IsValid = chkForMaxLength(200, txtVal.length);
            }

            function CheckGuestBookCommentLength() {
                var textarea = document.getElementById('<%=txtAnnMessage.ClientID%>');
                ValidateStoryLength(textarea, 200);
            }

            function SetImage(url) {
                var Image = document.getElementById('<%=imgGiftImage.ClientID%>');
                Image.src = url;

                (document.getElementById('<%=hdnGiftImageURL.ClientID%>')).value = url;

                $('yt-ThumbSelection').injectInside('yt-ThumbContainer');
                customModalBox.close();
            }

            // function to add value of  name and message textbox to session : by UD
            function setSessionMsg() {
                var messg;
                var ImgName = document.getElementById('<%=hdnGiftImageURL.ClientID%>');
                var name = document.getElementById('<%=txtName.ClientID%>');
                messg = document.getElementById('<%=txtAnnMessage.ClientID%>');
                giftError = document.getElementById('<%=GiftError.ClientID %>');
                if (messg != null && ImgName != null && name != null) {
                    if (name.value == "" || name.value == "Name") {
                        giftError.style.display = "block";
                        document.getElementById('<%=spanErrorMessage.ClientID%>').innerHTML = "!";
                        return false;
                    }

                    else {
                        giftError.style.display = "none";
                        document.getElementById('<%=spanErrorMessage.ClientID%>').innerHTML = "";
                        Gift_Gift.SetSessionValues(name.value, messg.value, ImgName.value);
                        GiveGiftModalpopup(location.href, document.title);
                    }
                }
            }
            // Ashu(11july,2011) : function to check length of message

            function CheckLength() {
                var textarea = document.getElementById('<%=txtMessage.ClientID%>');
                var numberRemaining = document.getElementById('<%=numberRemaining.ClientID%>');
                if (textarea != null && numberRemaining != null) {
                    ValidateMessageLength(textarea, numberRemaining);
                }
            }
            function CheckMessageLength() {
                var textarea = document.getElementById('<%=txtAnnMessage.ClientID%>');
                var numberRemaining = document.getElementById('<%=spanNumberremaining.ClientID%>');
                if (textarea != null && numberRemaining != null) {
                    ValidateMessageLength(textarea, numberRemaining);
                }
            }
            function ValidateMessageLength(textarea, numberRemaining) {


                charlimit = 200;

                if (textarea.value.length <= charlimit) {
                    numberRemaining.innerHTML = charlimit - textarea.value.length;
                }
                else {
                    textarea.value = textarea.value.substr(0, charlimit);
                    numberRemaining.innerHTML = 0;
                }
            }
            //for WordPress
            function setIsInTopurl() {
                // LHK: for topurl WordPress
                var IsInTopurl = (window.location != window.parent.location);
                {
                    Gift_Gift.setIsInTopurl(IsInTopurl);
                }
            } 
    </script>

    <!--yt-NavPrimary-->
    <div class="yt-Breadcrumbs">
        <a id="aTributeHome" runat="server">
            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
            Home</a> <span class="selected">Gifts</span>
    </div>
</asp:Content>
<asp:Content ID="content" ContentPlaceHolderID="ModuleContentPlaceHolder" runat="Server">
    <div id="divShowModalPopup">
    </div>
    <div class="yt-ContentPrimary" id="divContentContainer">
        <div id="GiftError" runat="server" style="display: none;" class="yt-Error">
            <h2>
                Oops - there was a problem with your gift.</h2>
            </br><h3>
                Please correct the error(s) below:</h3>
            <ul>
                <li>Please enter your name.</li></ul>
        </div>
        <div class="yt-Panel-Primary">
            <h2>
                Gifts</h2>
            <h4 class="yt-Bullet" style="margin-top: 0;">
                <p>
                    <%--<a href="javascript:void(0);"
                    id="lnkChooseGift"
                    runat="server"
                    onclick="chooseThumb();">
                    Choose a gift
                    to give...</a>--%>
                    Choose a gift<asp:Label ID="lblNonLoggedIn" runat="server" />optionally write a
                    short message, then click "Give Gift".</p>
            </h4>
            <div class="yt-GiftItem">
                <fieldset class="yt-Form">
                    <div>
                        <div style="width: 95px; float: left;">
                            <a href="javascript:void(0);" class="yt-Thumb" style="border: medium none;" onclick="chooseThumb();">
                                <img id="imgGiftImage" src="../assets/images/sample_gift-01.jpg" alt="Choose a Gift"
                                    runat="server" width="75" height="75" />
                            </a>
                            <div style="width: 80px;">
                                <asp:Button ID="btnChooseGigt" runat="server" class="yt-GiftMiniButton" Text="CHOOSE GIFT"
                                    OnClientClick="return chooseThumb();" />
                                <asp:HiddenField ID="hdnGiftImageURL" runat="server" />
                            </div>
                        </div>
                        <div style="float: left; width: 535px;">
                            <!-- Use this block for logged in registered users -->
                            <div id="divRegisteredUser" runat="server">
                                <div>
                                    <label id="lblMessage" visible="false" runat="server">
                                        Your Message:</label>
                                    <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" Text="Message" CssClass="txtcss"
                                        onfocus="if(this.value=='Message') {this.value='';this.style.color = '#949494';this.style.fontStyle='normal';}"
                                        onkeypress="this.style.color = 'black';this.style.fontStyle='normal';" onkeyup="javascript: CheckLength();"
                                        onblur="if(this.value=='') {this.value='Message';this.style.color = '#949494';} else{this.style.color = 'black';this.style.fontStyle='normal';}"></asp:TextBox>
                                </div>
                                <div style="margin-top: 10px;" class="yt-messageRemaining">
                                    <div style="float: left; width: 15px; height: 15px;">
                                        <img id="imgYT" src="../assets/images/bbg_TributeLogo.gif" alt="TributeLogo" runat="server"
                                            width="15" height="15" />
                                        <img id="imgFB" src="~/assets/images/icon_Facebook.gif" alt="TributeLogo" runat="server"
                                            width="15" height="15" visible="false" /></div>
                                    <div style="float: left; margin-left: 5px; width: 200px;">
                                        Logged in as
                                        <%= UserName.ToString() %>
                                        <div id="facebook_share_container" style="margin-top: 7px; margin-left: -25px; display: none">
                                            <input type="checkbox" id="facebook_share" checked="checked" /><label for="facebook_share"
                                                style="display: inline">Share on Facebook</label></div>
                                    </div>
                                    <div style="float: left; margin-left: 40px;">
                                        <span id="numberRemaining" runat="server">200</span>&nbsp;characters remaining.</div>
                                </div>
                            </div>
                            <!-- Use this block for anonymous users -->
                            <div id="divAnonymousUser" runat="server">
                                <div>
                                    <asp:TextBox ID="txtAnnMessage" MaxLength="200" runat="server" TextMode="MultiLine"
                                        Text="Message" CssClass="txtcss" onfocus="if(this.value=='Message') {this.value='';this.style.color = '#949494';this.style.fontStyle='normal';}"
                                        onkeypress="this.style.color = 'black';this.style.fontStyle='normal'; " onkeyup="javascript:CheckMessageLength();"
                                        onblur="if(this.value=='') {this.value='Message';this.style.color = '#949494';} else{this.style.color = 'black';this.style.fontStyle='normal';}"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvMessage" runat="server" Display="Dynamic" ControlToValidate="txtMessage"
                                        Style="border: solid 1px black;" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000">!</asp:RequiredFieldValidator>
                                    <asp:CustomValidator ID="cvMessage" runat="server" Display="Dynamic" ClientValidationFunction="maxLength2"
                                        Style="border: solid 1px black;" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000">!</asp:CustomValidator>
                                </div>
                                <label id="lblName" runat="server" visible="False">
                                    Your Name:</label>
                                <div style="margin-top: 10px;">
                                    <div style="float: left; width: 260px;">
                                        <asp:TextBox ID="txtName" runat="server" CssClass="txtNameCss" MaxLength="40" ValidationGroup="GiftValidation"
                                            Text="Name" Style="color: #949494" onfocus="if(this.value=='Name') {this.value='';this.style.color = '#949494';this.style.fontStyle='normal';}"
                                            onkeypress="this.style.color = 'black';this.style.fontStyle='normal';" onblur="if(this.value=='') {this.value='Name';this.style.color = '#949494';} else{this.style.color = 'black';this.style.fontStyle='normal';}"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ErrorMessage="Please enter your name."
                                            ValidationGroup="GiftValidation" InitialValue="Name" Display="Dynamic" ControlToValidate="txtName">!</asp:RequiredFieldValidator>
                                        <span id="spanErrorMessage" runat="server" style="color: Red;"></span>
                                    </div>
                                    <div style="float: left; padding-top: 4px; margin-left: 2px;">
                                        <span id="spanNumberremaining" runat="server">200</span>&nbsp;characters remaining.</div>
                                </div>
                                <div id="divlogin" runat="server" visible="false">
                                    <h4>
                                        <label id="lbllogin" runat="server">
                                            To add a message to your gift, please
                                        </label>
                                        <a href="javascript: void(0);" onclick="UserLoginModalpopupFromSubDomain(location.href,document.title);"
                                            id="lnkLogin" runat="server">Log in</a>
                                        <label>
                                            or
                                        </label>
                                        <a href="userregistration.aspx" id="lnkSignUp" runat="server">Sign up</a>
                                    </h4>
                                </div>
                            </div>
                            <div style="margin-right: 17px;">
                                <div class="yt-Form-Submit" style="margin-top: -6px;">
                                    <asp:LinkButton ID="lbtnSubmit" runat="server" CssClass="yt-Button yt-ArrowButton"
                                        ValidationGroup="GiftValidation" Style="margin-left: -25px;" OnClick="lbtnSubmit_Click">Give Gift</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </fieldset>
                <div id="divMessage" runat="server">
                </div>
            </div>
            <!-- yt-GiftItem -->
            <hr class="yt-Line" />
            <div id="divGiftList" runat="server" class="yt-GiftList">
                <div id="divPagingHead" runat="server" class="yt-ListHead">
                    <span id="spanHeadRecordCount" runat="server"></span>
                    <div class="yt-Pagination">
                        <span id="Page">Page:</span> <span id="spanPagingHead" runat="server"></span>
                    </div>
                </div>
                <div class="yt-ListBody">
                    <asp:Repeater ID="repGift" runat="server" OnItemCommand="repGift_ItemCommand" OnItemDataBound="repGift_ItemDataBound">
                        <ItemTemplate>
                            <div class="yt-ListItem">
                                <h6>
                                    <a href="javascript:void(0);" onclick="UserProfileModal_1('<%# Eval("UserId")%>');"
                                        class="yt-ListName">
                                        <%# Eval("UserName")%></a>
                                    <%# Eval("Location") %>
                                    <span class="yt-ListName">
                                        <%# Eval("GiftSentBy")%></span>
                                </h6>
                                <div class="yt-ItemThumb">
                                    <a class="yt-Thumb">
                                        <img src='<%# Eval("ImageUrl")%>' alt='<%# Eval("UserName")%>' width="75" height="75" /></a>
                                </div>
                                <p class="yt-ItemDate">
                                    <%# Eval("CreatedDate") %></p>
                                <p>
                                    <%# Eval("GiftMessage")%>
                                </p>
                                <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="yt-MiniButton yt-DeleteButton"
                                    CommandName="Delete" CausesValidation="false">Delete</asp:LinkButton>
                                <asp:HiddenField ID="hdnUserId" runat="server" Value='<%# Eval("UserId")%>' />
                                <asp:HiddenField ID="hdnGiftId" runat="server" Value='<%# Eval("GiftId")%>' />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div id="divPagingFoot" runat="server" class="yt-ListFoot">
                    <span id="spanFootRecordCount" runat="server"></span>
                    <div class="yt-Pagination">
                        <span>Page:</span> <span id="spanPagingFoot" runat="server"></span>
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
                                <%if (Request.Url.ToString().ToLower().Contains("wedding"))
                                   {%>                               
                                   
                                    GA_googleFillSlot("YourTribute_Wedding_Gifts_Bottom_468x60");
                               
                                <% } %>
                                <% else if (Request.Url.ToString().ToLower().Contains("anniversary"))
                                    {%>
                                
                                     GA_googleFillSlot("YourTribute_Anniversary_Gifts_Bottom_468x60");
                               
                                <% } %>
                                <% else if (Request.Url.ToString().ToLower().Contains("graduation"))
                                    {%>
                               
                                     GA_googleFillSlot("YourTribute_Graduation_Gifts_Bottom_468x60");
                                    
                                <% } %>
                                <% else if (Request.Url.ToString().ToLower().Contains("memorial"))
                                    {%>
                                
                                     GA_googleFillSlot("YourTribute_Memorial_Gifts_Bottom_468x60");
                                   
                                <% } %>
                                <% else if (Request.Url.ToString().ToLower().Contains("baby"))
                                    {%>
                               
                                     GA_googleFillSlot("YourTribute_Baby_Gifts_Bottom_468x60");
                                   
                                <% } %>
                                <% else if (Request.Url.ToString().ToLower().Contains("birthday"))
                                    {%>                                
                                     GA_googleFillSlot("YourTribute_Birthday_Gifts_Bottom_468x60");                                    
                                <% } %>
                    </script>

                </div>
                <p class="infoMessage">
                    *Upgrade this
                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                    to remove this advertisement</p>
            </div>
        </div>
        <% } %>
    </div>
    <!--yt-ContentPrimary-->
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ImageListContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
        function SetImage(url) {
            var Image = document.getElementById('<%=imgGiftImage.ClientID%>');
            Image.src = url;

            (document.getElementById('<%=hdnGiftImageURL.ClientID%>')).value = url;

            $('yt-ThumbSelection').injectInside('yt-ThumbContainer');
            customModalBox.close();
        }
    </script>

    <div id="yt-ThumbContainer">
        <div id="yt-ThumbSelection" class="yt-Panel-Primary">
            <h2>
                Choose a gift</h2>
            <div class="yt-ThumbList">
                <p class="yt-Bullet">
                    To choose a gift to give, click on an image below.</p>
                <ul class="yt-ListBody">
                    <asp:Repeater ID="repImage" runat="server" OnItemDataBound="repImage_ItemDataBound">
                        <ItemTemplate>
                            <li class="yt-Form-Field">
                                <label class="yt-Thumb">
                                    <asp:Image ID="imgImageList" runat="server" ImageUrl='<%#Eval("ImageUrl")%>' />
                                </label>
                                <asp:RadioButton ID="rdoImage" runat="server" />
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
