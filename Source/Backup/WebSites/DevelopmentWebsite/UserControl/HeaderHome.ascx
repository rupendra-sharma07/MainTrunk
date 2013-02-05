<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HeaderHome.ascx.cs" Inherits="UserControl_HeaderHome" %>
<%@ Register Src="../UserControl/Inbox.ascx" TagName="Inbox" TagPrefix="uc" %>
  <div id="divShowModalPopup"></div>
  <div id="fb-root"></div>
<div class="yt-HeaderContainer">
    <div class="yt-Header">
        <a href="<%= HomeUrl() %>"  title="<%= LogoTitle() %>" class="yt-Logo"></a>
        <!-- Use this line for server and comment the line written above (Line-193) -->
        <div class="yt-HeaderControls">
            <div class="yt-NavHeader">
                <div class="floatLeft" id="divProfile" runat="server">
                    <a id="myprofile" runat="server">My Account</a>                             
                    <%--<asp:LinkButton  style="color: #3EB7A8;"  ID="lbtnCreditCount" runat="server" 
                        onclick="lbtnCreditCount_Click"></asp:LinkButton>--%>
                        <a id="lnCreditCount" runat="server" href="~/ordercredit.aspx" style="color: #5BB4E5;"></a>
                </div>
                <div class="yt-UserInfo">
                    <span id="header_user_name" class="spanUserName">
                        <%=_userName%>
                    </span>
                    <span id="spanLogout" runat="server"></span>                    
                    <span id="spanSignUp" class="yt_HeaderSpanSignUp" runat="server">
                        <span class="yt-horizontalSpacer4"></span>
                        <a id="lnRegistration" runat="server" href="~/Users/UserRegistration.aspx">Sign up</a>
                    </span>
                </div>
            </div>
            <div class="hack-clearBoth">
            </div>
            <div id="topNavigationYT" runat="server" class="yt-topNavigation">
                <ul id="yt-globalNav">
                        <li id="yt-globalMenuItem1" class="<%=HomeNavValue %>"><a href="<%=Session["APP_BASE_DOMAIN"]%>">Home</a></li>
                        <li id="yt-globalMenuItem2" class="<%=TourNavValue %>"><a href="<%=Session["APP_BASE_DOMAIN"]%>tour.aspx">Tour</a></li>
                        <li id="yt-globalMenuItem3" class="<%=FeaturesNavValue %>"><a href="<%=Session["APP_BASE_DOMAIN"]%>features.aspx">Features</a></li>
                        <li id="yt-globalMenuItem4" class="<%=ExamplesNavValue %>"><a href="<%=Session["APP_BASE_DOMAIN"]%>themes.aspx">Themes</a></li>
                        <li id="yt-globalMenuItem5" class="<%=PricingNavValue %>"><a href="<%=Session["APP_BASE_DOMAIN"]%>pricing.aspx">Pricing</a></li>
                </ul>
             </div>   
             <div id="topNavigationYM" runat="server"  class="yt-topNavigation"  visible="false">
               <ul id="ym-globalNav"  >
                    <li id="ym-globalMenuItem1"  class="<%=HomeNavValue %>"><a href="<%=Session["APP_BASE_DOMAIN"]%>">
                        Home</a></li>
                    <li id="ym-globalMenuItem2"  class="<%=TourNavValue %>"><a href="<%=Session["APP_BASE_DOMAIN"]%>tour.aspx">
                        Tour</a></li>
                    <li id="ym-globalMenuItem3"  class="<%=FeaturesNavValue %>"><a href="<%=Session["APP_BASE_DOMAIN"]%>features.aspx">
                        Features</a></li>
                    <li id="ym-globalMenuItem4"  class="<%=ExamplesNavValue %>"><a href="<%=Session["APP_BASE_DOMAIN"]%>examples.aspx">
                        Themes</a></li>
                    <li id="ym-globalMenuItem5"  class="<%=PricingNavValue %>"><a href="<%=Session["APP_BASE_DOMAIN"]%>pricing.aspx">
                        Pricing</a></li>
                </ul>
             </div>        
        <!--yt-HeaderControls-->       
    </div>
    <!--yt-Header-->   
</div>
</div>
<script type="text/javascript">
    App_Domain = "<%=Session["APP_BASE_DOMAIN"]%>";
    var user_connected_to_fb = false;

         window.fbAsyncInit = function () {
         FB.init({ appId: '<%= ConfigurationManager.AppSettings["ApiKey"].ToString() %>', status: true, cookie: true, xfbml: true,oauth: true });

         /* All the events registered */
         FB.Event.subscribe('auth.login', function (response) {
             // do something with response
             doAjaxLogin();
         });
         FB.getLoginStatus(function(response) {
        if (response.authResponse) update_user_is_connected();
        else update_user_is_not_connected();
    });

    // This will be triggered as soon as the user logs into Facebook (through your site)
    FB.Event.subscribe('auth.login', function(response) {
        update_user_is_connected();
    });
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
    
function publish_stream(msg, attachment, action_link, target_id, msg_prompt, callback_fn) {
        if(user_connected_to_fb) {   
            FB.api('/me', function (response) {                    
//                    FB.ui(response.name, attachment, action_link, target_id, msg_prompt);
                    FB.ui(
                {
                    method: 'stream.publish',
                    message: msg,
                    attachment:attachment ,
                    action_links: action_link,
                    user_prompt_message: msg_prompt
                },callback_fn
                );
                });
        }
    };

    function doAjaxLogin() {
       new Ajax("<%= TributesPortal.Utilities.WebConfig.CurrentSubDomain %>AjaxLogin.aspx", {
		method: 'get',
		data: {"action":'facebookLogin'},
		onComplete: function(rs){
		    res = Json.evaluate(rs);
            if(res.showDialog) {
            FB.login(function (response) {
                    if (response.authResponse) {    
                        // user authorized
                        popupFbSignUp(); 
                    } else {
                        // user cancelled
                    }
                }, { scope: 'email' });
            } else if (res.refreshPage) {
                window.location.reload();
            } else if(res.messageText.length>0){
                if($('mb_Error'))
                $('mb_Error').innerHtml = res.messageText;
            }		  
		}
	  }).request();
    };
        
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
    
    function popupFbSignUp() {
        var closeFacebookSignUp = function() {
            if ($('mb_Title')) $('mb_Title').remove();
            if ($('yt-fb-signup')) $('yt-fb-signup').remove();
        }
        var indicator_img = new Image(48,48); 
        indicator_img.src="assets/images/ajax-loader.gif"; 
        
        FB.api('/me', function (response) {
                    var query = FB.Data.query('select name from user where uid={0}', response.id);
                    query.wait(function (rows) {     
            customModalBox.htmlBox('', '<div id="yt-fb-signup">You will now be connected to Your Tribute<br/>through Facebook as ' +
        "<a href='http://www.facebook.com/profile.php?id=" + response.id +
        "' target='_blank'>" + rows[0].name + "</a>" +
        '<a href="javascript:doFacebookSignup()" class="btn_fb_signup yt-Button" id="btn_fb_create">CONTINUE</a>'+        
        'Do you already have a Your Tribute Account?'+
        '<a href="<%= TributesPortal.Utilities.WebConfig.CurrentSubDomain %>log_in.aspx?ytfblink=yes&location='+encodeURIComponent(location.href)+'" class="btn_fb_signup" id="btn_fb_associate">Click here to connect it to Facebook</a></div>',
        'FacebookSignUp');

            buttonStyles();
                        
            $('mb_contents').addClass('yt-Panel-Primary');
            new Element('h2').setHTML('Facebook Connected').setProperty('id', 'mb_Title').injectTop($('mb_contents'));
           
            $('mb_close_link').addEvent('click', closeFacebookSignUp);
            $('btn_fb_associate').addEvent('click', function() {
                $('mb_close_link').fireEvent('click');
                customModalBox.close();
            });
        });
   });
    };
    function replace_submit_with_stream(click_id, facebook_box_id, msg_fn, attach_fn, action_fn) {
        var old_href = $(click_id).getProperty('href');
        if(old_href) {
            old_href.replace("javascript:", "");
        }
        var old_href_fn = function() {
            try {
                eval(old_href);
            } catch(e) {
                //console.log(e);
            }
        };
        
        $(click_id).setProperty('href', "javascript:");
        $(click_id).addEvent('click', function() {
            //check to make sure the box was checked to publish the stream, if not, just call the old_href function
            if($$('#'+facebook_box_id).length == 0 || !$$('#'+facebook_box_id)[0].checked) {
                old_href_fn();
                return;
            }
            publish_stream(msg_fn(), attach_fn(), action_fn(), null, null, old_href_fn);
        });
    };
    
    function header_user_is_connected() {
        user_connected_to_fb = true;
        $(document).fireEvent("fb_connected");
        if ($('header_logout')) {
            var logout_link = $('header_logout');            
            logout_link.addEvent('click', fb_logout);
	        logout_link.href = "#";
        }
    }

    function header_user_is_not_connected() {
        if ($('header_logout')) {
            var logout_link = $('header_logout');
            logout_link.innerHTML = "Log out";
            logout_link.href = '<%= TributesPortal.Utilities.WebConfig.CurrentSubDomain %>Logout.aspx';
            logout_link.removeEvent('click', fb_logout);
        }
                  
    }
</script>
