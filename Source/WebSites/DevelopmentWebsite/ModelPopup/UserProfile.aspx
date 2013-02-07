<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserProfile.aspx.cs" Inherits="ModelPopup_UserProfile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:fb="http://www.facebook.com/2008/fbml" xml:lang="en" lang="en">
<head>
    <title>Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> - User profile</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Content-Language" content="en-ca" />
    <meta http-equiv="imagetoolbar" content="false" />
    <meta name="robots" content="index,follow" />
    <meta name="MSSmartTagsPreventParsing" content="true" />
    <!-- really basic, generic html class stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/default.css" />
    <!-- default grid layout stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/layouts/centered1024_1.css" />
    <!-- print-friendly stylesheet -->
    <link rel="stylesheet" type="text/css" media="print" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/print.css" />
    <!-- screen elements stylesheet should be here -->
    <link rel="stylesheet" type="text/css" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/screen.css" />
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/userprofile.css" />
    <!-- JS libraries -->
    <script type="text/javascript" src="../assets/scripts/mootools-1.11.js"></script>
    <script type="text/javascript" src="../assets/scripts/global.js"></script>
    <script src="https://connect.facebook.net/en_US/all.js" type="text/javascript"></script>   
    <script type="text/javascript">

       window.addEvent('load', function() {	
        	buttonStyles();
	        thumbStyles();
	        hrFix();    
	        fieldsetFix();	       
	    });
	
        function CheckSubject(source, args)
        {
            var  messages= document.getElementById('<%= subjectes.ClientID %>');
            var  subject= document.getElementById('<%= txtUserProfileMsgSubj.ClientID %>');
            var reSpecialCharacter=/^[a-zA-Z0-9\s\?\"\!\-\@\.\:\;\=\+\[\]_\,\%\(\)\/\&]+$/ 
              if(subject.value.length==0)
              {
               var _html="<li>Subject is a required field.</li>";
               subjectes.innerHTML=_html;
               setIndicatorSubject();
               errorToParent();
               args.IsValid=false;
            }
            else
            {
            if (subject.value.search(reSpecialCharacter)==-1)
            {
             var html1="<li>Please provide valid input characters in subject field.</li>";
               subject.innerHTML = html1;
               setIndicatorSubject();
               errorToParent();
               args.IsValid=false;
            }
            else
            {
                //subject.innerHTML = "";
                }
           }
         }
       
          function Checkmessage(source, args)
          {
           var  messages= document.getElementById('<%= messages.ClientID %>');
            var  message= document.getElementById('<%= txtarUserProfileMsg.ClientID %>');
            var reSpecialCharacter=/^[a-zA-Z0-9\s\?\"\!\-\@\.\:\;\=\+\[\]_\,\%\(\)\/\&]+$/ 
           if(message.value.length==0) 
            {
               var html2="";
               messages.innerHTML=html2;
              
            }
           else
            {
            if (message.value.search(reSpecialCharacter)==-1)
            {
             var html3="<li>Please provide valid input characters in message field.</li>";
               messages.innerHTML=html3;
               setIndicatorMessage();
               errorToParent();
               args.IsValid=false;
            }
            else
            {
             var _html="";
               messages.innerHTML=_html;
             
             }
            }
          
          }
		function errorToParent() {
				parentError = window.parent.document.getElementById('mb_Error');
				if($('yt-ProfileError')) {
					parentError.className = 'yt-Error';
					parentError.innerHTML = $('yt-ProfileError').innerHTML;
				} 
				
				window.parent.$('yt-UserProfileContent').style.top = '0';// Refresh DOM to fix FF bug
		}
       function setIndicatorSubject()
       {
          var  erroru= document.getElementById('<%= errorSubject.ClientID %>');
          erroru.innerText='!';
          erroru.style.color='#FF8000';
          erroru.style.fontSize='Medium';
          erroru.style.fontWeight='bold';
          erroru.style.visibility = 'visible';
    
       }
 function setIndicatorMessage()
       {
          var  erroru= document.getElementById('<%= errorMessage1.ClientID %>');
          erroru.innerText='!';
          erroru.style.color='#FF8000';
          erroru.style.fontSize='Medium';
          erroru.style.fontWeight='bold';
          erroru.style.visibility = 'visible';
    
       }
    </script>
    <script src="https://connect.facebook.net/en_US/all.js" type="text/javascript"></script>
    <!--#include file="../analytics.asp"-->
</head>
<body>
    <form id="Form1" action="" runat="server" defaultfocus="txtUserProfileMsgSubj" defaultbutton="ltbnSendMessages" >
    <div id="divShowModalPopup"></div> 
    <div id="fb-root"></div>
        <div class="yt-Container">
            <div class="yt-ContentContainer">
                <div class="yt-ContentPrimaryContainer">
                    <div class="yt-Error" id="yt-ProfileError" style="width: 320px">
                                <h2>
                                    Oops - there was a problem in send message.</h2>
                                <h3>
                                    Please correct the errors below:</h3>
                                   <ul id="subjectes" runat="server">
                                </ul>
                                <ul id="messages" runat="server">
                                </ul>
                               
                            </div>
                    <div id="divResult" style="width: 320px; color:Red;" runat="server" visible="false"> 
                    <h3>This message has been sent.</h3>
                    </div>
                    <div class="yt-ContentPrimary">
                        <div class="yt-ListItem">
                            <!-- if the user has no profile photo uploaded and is logged in, clicking on the
                        blank profile photo should take them to the My Profile page to upload a photo -->
                            <a id="aUserProfileImg" runat="server" class="yt-Thumb" href="javascript:void(0);" style="cursor:default;">
                                <img src="../assets/images/bg_ProfilePhotoUpload.gif" id="UserProfileImage"
                                    runat="server" alt="" class="yt-ProfilePic"/>                                    
                            </a>
                            <div id="FacebookProfileImage" runat="server"></div>
                            <dl class="vcard">
                                <asp:Literal ID="ltrlUserProfile" runat="server"></asp:Literal>
                            </dl>
                        </div>
                        <div id="divlogin" runat="server" visible="false">
                            <label>
                                To send a personal message please <a href="<%=Session["APP_BASE_DOMAIN"] %>log_in.aspx" target="_parent">Login</a> or <a
                                    href="<%=Session["APP_BASE_DOMAIN"] %>userregistration.aspx" target="_parent">Sign Up</a>.
                            </label>
                        </div>
                        <fieldset id="fldMessage" runat="server">
                            <legend>Send a Personal Message:</legend>
                            <div class="yt-Form-Field">
                                <label for="txtUserProfileMsgSubj">
                                    Subject:</label>
                                <asp:TextBox ID="txtUserProfileMsgSubj" CssClass="yt-Form-Input-Long" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator Font-Bold="True" ValidationGroup="ContactUs" Font-Size="Medium"
                                    ForeColor="#FF8000" ID="RequiredFieldValidator1" ControlToValidate="txtUserProfileMsgSubj"
                                    runat="server" Text="!"></asp:RequiredFieldValidator>
                                <span id="errorSubject" runat="server" visible="false">&nbsp;</span>
                                  <asp:CustomValidator ID="rfvSubject"  ClientValidationFunction="CheckSubject" runat="server" Font-Bold="True"
                                        Font-Size="Medium" ForeColor="White" ></asp:CustomValidator>
                                
                            </div>
                            <div class="yt-Form-Field">
                                <label for="txtarUserProfileMsg">
                                    Message:</label>
                                <asp:TextBox ID="txtarUserProfileMsg" Columns="50" Rows="6" TextMode="MultiLine" CssClass="yt-Form-Textarea-Long" runat="server"></asp:TextBox>   
                                <asp:RequiredFieldValidator Font-Bold="True" ValidationGroup="ContactUs" Font-Size="Medium"
                                    ForeColor="#FF8000" ID="rfvtxtarEmailMessage" ControlToValidate="txtarUserProfileMsg"
                                    runat="server" Text="!"></asp:RequiredFieldValidator>
                                 <span id="errorMessage1" runat="server" style="visibility: hidden;">&nbsp;</span>
                                 <asp:CustomValidator ID="CustomValidator1" ClientValidationFunction="Checkmessage" runat="server" Font-Bold="True"
                                        Font-Size="Medium" ForeColor="White" ></asp:CustomValidator>
                                  <p>
                                    FYI - This message will be seen in this member’s personal Inbox.</p>
                            </div>
                            <div class="yt-Form-Buttons">
                                <div class="yt-Form-Submit">
                                    <asp:LinkButton ID="ltbnSendMessages" CssClass="yt-Button yt-ArrowButton" runat="server" OnClick="ltbnSendMessages_Click" ValidationGroup="ContactUs">Send Message</asp:LinkButton>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                    <!--yt-ContentPrimary-->
                </div>
                <!--yt-ContentPrimaryContainer-->
            </div>
            <!--yt-ContentContainer-->
        </div>
        <!--yt-Container-->
    </form>
<% if (ConfigurationManager.AppSettings["ApiKey"] != null) { %>     
    <script type="text/javascript">
//       window.addEvent('domready', function(){     
//         FB.init('<%= ConfigurationManager.AppSettings["ApiKey"].ToString() %>', 
//                 "/xd_receiver.htm");
        //       });
        window.fbAsyncInit = function () {
            FB.init({
                appId: '<%= ConfigurationManager.AppSettings["ApiKey"].ToString() %>',
                status: true, // check login status
                cookie: true, // enable cookies to allow the server to access the session
                xfbml: true,  // parse XFBML
                //channelUrl: 'http://www.yourdomain.com/channel.html', // Custom Channel URL
                oauth: true //enables OAuth 2.0
            });
        }(function () {

        var e = document.createElement('script');

        e.type = 'text/javascript';

        e.src = document.location.protocol +

                    '//connect.facebook.net/en_US/all.js';

        e.async = true;

        document.getElementById('fb-root').appendChild(e);

    } ());

    </script>
<% } %>     
 <!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
    <script type="text/javascript" src="../assets/scripts/BrowserOrTabCloseHandler.js"></script>   
</body>
</html>
