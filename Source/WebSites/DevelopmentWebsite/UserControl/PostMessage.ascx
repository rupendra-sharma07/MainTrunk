<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PostMessage.ascx.cs" Inherits="UserControl_PostMessage" %>

<div id="PostMessage" style="display: block;">
    <div id="divShowModalPopup">
    </div>
    <div style="width: 550px; padding-left: 15px; padding-right: 15px;">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div id="yt-ContentPrimaryContainer">
                    <div class="yt-margin-top">
                        <center>
                            <div>
                                <asp:Label ID="lbllogin" runat="server" Text="Log in to  Post Message" CssClass="yt-Form-Heading"></asp:Label>
                            </div>
                            <div class="yt-margin-top">
                                <asp:Label ID="lblemailinfo" runat="server" CssClass="yt-Form-Label" Text="You can log in using your existing account with: "></asp:Label>
                            </div>
                            <br class="yt-clear-both" />
                            <div style="position:relative; top:15px; margin-left:277px;">
                                <div style="float: left; padding-top: 5px; margin-right: 8px;">
                                    <label style="text-decoration: underline;">
                                        or</label>
                                </div>
                                <div id="fb-root" style="float: left;left: 15px; position:relative; height: 30px; width: 190px; margin-top:4px;">
                                    <fb:login-button length="long" onlogin="doAjaxLogin();"><fb:intl>Login with Facebook</fb:intl></fb:login-button>
                                </div>
                                <br class="yt-clear-both" />
                            </div>
                            <div id="PostMessageBorder" runat="server" class="yt-dotted-Border">
                            </div>
                            <div style="margin-top: 25px;">
                                <asp:Label ID="Label1" runat="server" Text="Not a member? Join now. It's fast and free. "
                                    CssClass="yt-Form-Heading"></asp:Label>
                            </div>
                            <div style="margin-top: 20px;">
                                <asp:Label ID="Label2" runat="server" CssClass="yt-Form-Label" Text="Create an account to interact with friends and family. "></asp:Label>
                            </div>
                            <div>
                                <asp:Label ID="Label3" runat="server" CssClass="yt-Form-Label" Text="Add your profile, share stories, post messages, upload photos and more. "></asp:Label>
                            </div>
                        </center>
                    </div>
                    <div class="yt-dashed-Border" style="margin-top: 105px;">
                    </div>
                    <div style="height: 40px;">
                        <div style="float: left; width: 400px; padding-top: 20px;">
                            <asp:Label CssClass="yt-Form-Label" runat="server">Not ready to sign up?
                                <asp:LinkButton ID="lbtnPostMessage" runat="server" OnClick="lbtnPostMessage_Click"
                                    Style="font-size: 14px; font-weight: bold;">Click here to post your message. </asp:LinkButton></asp:Label>
                        </div>
                        <div class="yt-footer-popup">
                            <div class="yt-float-right">
                                <label class="yt-footer-label">
                                    Powered by:</label></div>
                            <br class="yt-clear-both" />
                            <div class="yt-footer-logoimg">
                                <img src="../assets/images/logo_AdminReceipt.gif" width="120px" height="26px" alt=" " />
                            </div>
                        </div>
                    </div>
                    <br style="clear: both;" />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>

<script type="text/javascript">
    App_Domain = "<%=Session["APP_BASE_DOMAIN"]%>";
    var user_connected_to_fb = false;

         window.fbAsyncInit = function () {
         FB.init({ appId: '<%= ConfigurationManager.AppSettings["ApiKey"].ToString() %>',
          status: true,
           cookie: true,
            xfbml: true,
            oauth:true});    
     }(function () {

        var e = document.createElement('script');

        e.type = 'text/javascript';

        e.src = document.location.protocol +

                    '//connect.facebook.net/en_US/all.js';

        e.async = true;

        document.getElementById('fb-root').appendChild(e);

    } ());
     

    function fb_logout() {
    FB.logout(function(response) {
  window.location='<%= TributesPortal.Utilities.WebConfig.CurrentSubDomain %>Logout.aspx';
  });      
    }
    
    function fb_err_logout(){
      FB.logout(function() {
         var url = location.href;
         // append a timestamp to make sure the url seems different to the browser
         url = (url + (url.indexOf('?') > -1 ? '&' : '?') + (new Date()).getTime());
         window.location.href = url;
      });
    }   
    
    function doAjaxLogin() {
       UserControl_PostMessage.FacebookLogin();
       window.parent.location.reload();
    }
        
    function doFacebookSignup(){
      if ($('yt-fb-signup')) $('yt-fb-signup').remove();
      new Element('img').setProperties({"src":'assets/images/ajax-loader.gif', 
                                        "class":'loader_indicator',
                                        "id":'waitIndicator'}).injectInside($('mb_contents'));            
       new Ajax("<%= TributesPortal.Utilities.WebConfig.CurrentSubDomain %>AjaxLogin.aspx", {
		method: 'get',
		data: {"action":'facebookSignup'},
        onComplete: function(res){
		        res = Json.evaluate(res);       
                if(res.messageText.length>0) {
                    if ($('waitIndicator')) $('waitIndicator').remove();
                    $('mb_Error').innerHtml = res.messageText;
                } else if (res.refreshPage) {
                    window.location.reload();
                }
		    }
	    }).request();
    }                  
    
</script>

