<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TributeHomePage.aspx.cs"
    Inherits="Tribute_TributeHomePage_" EnableEventValidation="false" %>

<%@ Register Src="../UserControl/TributeHeader.ascx" TagName="Header" TagPrefix="uc" %>
<%@ Register Src="../UserControl/TributePageHeader.ascx" TagName="TributeHeader"
    TagPrefix="ucTribute" %>
<%@ Register Src="../UserControl/Footer.ascx" TagName="Footer" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:fb="http://www.facebook.com/2008/fbml"
xml:lang="en" lang="en">
<head runat="server">
    <title>Your Tribute</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Content-Language" content="en-ca" />
    <meta http-equiv="imagetoolbar" content="false" />
    <meta name="robots" content="index,follow" />
    <meta name="MSSmartTagsPreventParsing" content="true" />
    <meta id="PageDesc" name="description" runat="server" content="" />
    <link id="PageThumb" rel="image_src" runat="server" href="" />
    <meta id="fbTitle" runat="server" name="og:title" content="" />
    <meta id="fbDesc" runat="server" name="og:description" content="" />
    <meta id="fbThumb" runat="server" name="og:img" content="" />
    <!-- default grid layout stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/layouts/centered1024_1o23.css" />
    <!-- screen elements stylesheet should be here -->
    <link rel="stylesheet" type="text/css" href="<%=Session["APP_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/screen.css" />
    <!-- Selected App Theme -->
    <link runat="server" id="idSheet" rel="stylesheet" type="text/css" media="screen, projection" />
    <!-- Website Favorite Icon -->
    <link rel="Shortcut Icon" type="image/x-icon" href="<%=Session["APP_PATH"]%>assets/images/favicon.ico" />
    <!-- JS libraries -->

    <script type="text/javascript" src="<%=Session["APP_PATH"]%>assets/scripts/mootools-1.11.js"></script>

    <script type="text/javascript" src="<%=Session["APP_PATH"]%>assets/scripts/modalbox.js"></script>

    <script type="text/javascript" src="<%=Session["APP_PATH"]%>assets/scripts/global.js"></script>

    <script type="text/javascript" src="<%=Session["APP_PATH"]%>assets/scripts/styleSwitcher.js"></script>

    <script type="text/javascript" src="<%=Session["APP_PATH"]%>Common/JavaScript/FooterControl.js"></script>

    <script type="text/javascript" language="javascript" src="<%=Session["APP_PATH"]%>Common/JavaScript/TributeHomePage.js"> </script>

    <script type="text/javascript" language="javascript" src="<%=Session["APP_PATH"]%>Common/JavaScript/Common.js"> </script>

    <script type="text/javascript" language="javascript" src="<%=Session["APP_PATH"]%>Common/JavaScript/InternalMessage.js"> </script>

    <!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
    <script type="text/javascript" src="<%=Session["APP_PATH"]%>assets/scripts/BrowserOrTabCloseHandler.js"></script>

    <script src="https://connect.facebook.net/en_US/all.js" type="text/javascript"></script>

    <!-- DJ:17-Mar-10: Script Added for Google Ads -->
    <style type="text/css" media="screen">
        </style>

    <script type='text/javascript' src='http://partner.googleadservices.com/gampad/google_service.js'>
    </script>

    <script type='text/javascript'>
        GS_googleAddAdSenseService("ca-pub-7489783537502280");
        GS_googleEnableAllServices();
    </script>

    <script type='text/javascript'>
        GA_googleAddSlot("ca-pub-7489783537502280", "YourTribute_Wedding_Homepage_Center_300x250");
        GA_googleAddSlot("ca-pub-7489783537502280", "YourTribute_Wedding_Homepage_Bottom_728x90");
        GA_googleAddSlot("ca-pub-7489783537502280", "YourTribute_Anniversary_Homepage_Center_300x250");
        GA_googleAddSlot("ca-pub-7489783537502280", "YourTribute_Anniversary_Homepage_Bottom_728x90");
        GA_googleAddSlot("ca-pub-7489783537502280", "YourTribute_Graduation_Homepage_Center_300x250");
        GA_googleAddSlot("ca-pub-7489783537502280", "YourTribute_Graduation_Homepage_Bottom_728x90");
        GA_googleAddSlot("ca-pub-7489783537502280", "YourTribute_Memorial_Homepage_Center_300x250");
        GA_googleAddSlot("ca-pub-7489783537502280", "YourTribute_Memorial_Homepage_Bottom_728x90");
        GA_googleAddSlot("ca-pub-7489783537502280", "YourTribute_Baby_Homepage_Center_300x250");
        GA_googleAddSlot("ca-pub-7489783537502280", "YourTribute_Baby_Homepage_Bottom_728x90");
        GA_googleAddSlot("ca-pub-7489783537502280", "YourTribute_Birthday_Homepage_Center_300x250");
        GA_googleAddSlot("ca-pub-7489783537502280", "YourTribute_Birthday_Homepage_Bottom_728x90");
    </script>

    <script type='text/javascript'>
        GA_googleFetchAds();
    </script>

    <!-- End Google Ads Code -->

    <script type="text/javascript">
    var IsInTopurl;
    {
    IsInTopurl = (window.location != window.parent.location);
            if(IsInTopurl)
            {
                Tribute_TributeHomePage_.setIsInTopurl(IsInTopurl);
            };
    }
    App_Domain = "<%=Session["APP_BASE_DOMAIN"]%>";
    function hideWideRows() {
	    $$('.yt-colWide').each( function(a) {
		    a.getParent().addClass('yt-HiddenRow');
	    });
	    $$('.yt-ButtonDetails').each( function(a) {
		    a.removeClass('yt-Open');
	    });
    }
    
    function showWideRows() {
	    $$('.yt-colWide').each( function(a) {
		    a.getParent().removeClass('yt-HiddenRow');
	    });
	    $$('.yt-ButtonDetails').each( function(a) {
		    a.addClass('yt-Open');
	    });
    }


        function setIsInTopurl() {
         // LHK: for topurl WordPress
            IsInTopurl = (window.location != window.parent.location);
            if(IsInTopurl)
            {
                Tribute_TributeHomePage_.setIsInTopurl(IsInTopurl);
            }
        }

    function showDetails(theButton) {
	    if(theButton.hasClass('yt-Open')) {
		    hideWideRows();
	    } else {
		    hideWideRows();
		    theButton.getParent().getParent().getNext().removeClass('yt-HiddenRow');
		    theButton.addClass('yt-Open');
	    }
    }

    window.addEvent('domready', function() {
	    // check to see if there is any error info posted from the sponsor popup, if so, show modal box and error
	    if($('yt-SponsorError')) {
		    $('yt-SponsorError').injectBefore('mb_header');
	    }
    });
	
	function Themer(theSelect) {
		SetActiveStyleSheet(theSelect.options[theSelect.selectedIndex].text	);
	}
	
function Themer_(theSelect) 
{
    SetActiveStyleSheet(theSelect);  
    if($('Expired'))
    {
        $('Expired').addClass('yt-Expired');  
        doModalExpired();      
    }   
}

function Themer(theSelect) 
{
    SetActiveStyleSheet(theSelect);  
}

 
    function LoginUser(source, args)
    {
        var UserName=$('txtLoginUsername1');
        var Password=$('txtLoginPassword1'); 
        args.IsValid =UserLogin(UserName,Password);
    }
   
     
     
 function maxLength(txtid)
 {
  var txtVal = txtid.value;     
  return chkForMaxLength_(250, txtVal.length);
 }
 
    </script>

    <script language="javascript" type="text/javascript">

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
    </script>

    <!--#include file="../analytics.asp"-->
</head>
<body id="body" runat="server">
    <form id="Form1" method="post" runat="server">
    <asp:HiddenField ID="hfToUserid" runat="server" />
    <asp:HiddenField ID="hfToSubject" runat="server" />
    <asp:HiddenField ID="hfPaymentMethod" runat="server" />
    <asp:HiddenField ID="hdnTypeToMail" runat="server" />
    <asp:HiddenField ID="hdnTopUrl" runat="server" />
    <asp:HiddenField ID="hdnFullmessage" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <!-- Sample output of DIV for contact info popup, to be populated via AJAX -->
    
    <div id="divShowModalPopup">
    </div>
    <div id="mainDiv" runat="server" class="yt-Container yt-TributeHome yt-Channel-Memorial">
        <uc:Header ID="ytHeader" Section="tribute" runat="server" />
        <ucTribute:TributeHeader ID="TributePageHeader" section="Tribute" navigationname="Home"
            runat="server" />
        <div id="EmptyDivAboveMainPanel" runat="server" visible="false">
            &nbsp;</div>
        <h1 class="yt-tributeTitle">
            <asp:Literal ID="lTributeName1" runat="server" /></h1>
        <ul class="yt-NavPrimary">
            <li class="yt-Story"><a href="story.aspx<%= _query_string %>">Story</a></li>
            <li class="yt-Notes"><a href="notes.aspx<%= _query_string %>">Notes</a></li>
            <li class="yt-Events"><a href="events.aspx<%= _query_string %>">Events</a></li>
            <li class="yt-GuestBook"><a href="guestbook.aspx<%= _query_string %>">Guestbook</a></li>
            <li class="yt-Gifts"><a href="gift.aspx<%= _query_string %>">Gifts</a></li>
            <li class="yt-Photos"><a href="photos.aspx<%= _query_string %>">Photos</a></li>
            <li class="yt-Videos"><a href="videos.aspx<%= _query_string %>">Videos</a></li>
        </ul>
        <!--yt-NavPrimary-->
        <div class="yt-ContentContainer">
            <div class="yt-ContentContainerInner">
                <div class="yt-ContentPrimaryContainer">
                    <div class="yt-ContentPrimary">
                        <div class="<%=divWecome %>">
                            <h2>
                                Welcome</h2>
                            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                                <ContentTemplate>
                                    <div id="SaveMsg" runat="server">
                                        <div class="yt-Form-Field">
                                            <asp:TextBox ID="txtarWelcome" CssClass="yt-Form-Textarea-Short" Columns="50" Rows="6"
                                                runat="server" TextMode="MultiLine" onkeypress="return maxLength(this)" Visible="true"></asp:TextBox>
                                        </div>
                                        <div class="yt-Form-MiniButtons">
                                            <asp:LinkButton ID="lnkbCancel" CssClass="yt-MiniButton yt-CancelButton" runat="server"
                                                OnClick="lnkbCancel_Click">Cancel</asp:LinkButton>
                                            <asp:LinkButton ID="lbtnSave" CssClass="yt-MiniButton yt-SaveButton" OnClick="lbtnSave_Click"
                                                runat="server">Save</asp:LinkButton>
                                        </div>
                                    </div>
                                    <div id="editMsg" runat="server">
                                        <p id="lblWelcomeMsg" runat="server">
                                        </p>
                                        <asp:LinkButton ID="lbtnEdit" CausesValidation="false" CssClass="yt-MiniButton yt-EditButton"
                                            runat="server" OnClick="lbtnEdit_Click">Edit</asp:LinkButton>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <!--yt-Welcome-->
                        <div class="yt-Tribute-Photo">
                            <a href="story.aspx<%= _query_string %>">
                                <div style="text-align: center;">
                                    <asp:Image ID="imgTributeImage" runat="server" Height="196px" ImageUrl="../assets/images/bg_TributePhoto.gif"
                                        AlternateText=" " />
                                </div>
                                <span class="yt-PhotoFrame" style="left: 0px; top: 0px;"></span></a>
                        </div>
                        <div class="yt-Details">
                            <h2>
                                <asp:Literal ID="lTributeName2" runat="server" /></h2>
                            <dl>
                                <dt id="lblDate1" runat="server"></dt>
                                <dd id="txtDate1" runat="server">
                                </dd>
                                <dt id="lblDate2" runat="server"></dt>
                                <dd id="txtDate2" runat="server">
                                </dd>
                                <dt id="lblAge1" runat="server"></dt>
                                <dd id="txtAge1" runat="server">
                                </dd>
                                <dt id="lblLocation" runat="server">Location:</dt>
                                <dd id="txtLocation" runat="server">
                                </dd>
                            </dl>
                        </div>
                        <!--yt-Details-->
                        <div id="divLogin" class="yt-Form-Buttons" runat="server">
                            <asp:LinkButton ID="lbtnShareTribute" runat="server" CssClass="yt-Button yt-ArrowButton"
                                CausesValidation="false" OnClick="lbtnShareTribute_Click">Log in | Sign Up</asp:LinkButton>
                        </div>
                    </div>
                    <!--yt-ContentPrimary-->
                </div>
                <!--yt-ContentPrimaryContainer-->
                <div class="yt-ContentSecondary">
                    <div class="yt-Panel-Primary" id="obituaryBlock" runat="server">
                        <div class="yt-ContentTertiary">
                            <div class="yt-Panel-Announcement">
                                <h2>
                                    Obituary</h2>
                            </div>
                        </div>
                        <asp:Literal ID="Literal2" runat="server"></asp:Literal>
                    </div>
                    <%--LHK: to add guestbook message box--%>
                    <div class="divGuestBook">
                        <div class="Margin10">
                            <div style="color: Black; width: 500px; display: none" class="yt-Error" id="dvError">
                            </div>
                            <div class="marginTopBotttom">
                                <div class="yt-Bullet">
                                    <asp:Label ID="lblGbHeader" runat="server"></asp:Label>
                                </div>
                            </div>
                            <asp:TextBox ID="txtMessage" MaxLength="3000" class="GBTxtBox" Text="Message" Rows="6"
                                runat="server" TextMode="MultiLine" onfocus="if(this.value=='Message') {this.value='';this.style.color = '#949494';this.style.fontStyle='normal';}"
                                onblur="if(this.value=='') {this.value='Message';this.style.color = '#949494';this.style.fontStyle='normal';}"
                                onkeypress="this.style.color = 'black';this.style.fontStyle='normal'; return funcTextCount(this);"></asp:TextBox>
                            <span id="spanErrorMessage" runat="server" style="color: Red; font-weight: bold;
                                margin-top: 5px;"></span>
                            <div>
                                <div class="allignLeft MarginTop12">
                                    <div id="divHomeAuthUser" runat="server">
                                        <img runat="server" id="imgAppLogo" class="yt-guestbook-imguser" />
                                        <div style="width: 250px;">
                                            <label id="lblUserName" runat="server" style="width: 200px; font-size: 12px;">
                                            </label>
                                        </div>
                                    </div>
                                    <div id="divUnAuthUser" runat="server">
                                        <asp:TextBox Style="color: #949494; font-family: 'Lucida Sans','Lucida Grande',sans-serif;"
                                            ID="txtUserName" CssClass="yt-Form-Input" Width="250px" Text="Name" onfocus="if(this.value=='Name') {this.value='';this.style.color = '#949494';}"
                                            onBlur="if(this.value=='') {this.value='Name';this.style.color = '#949494';}"
                                            onkeypress="this.style.color = 'black';" runat="server" Height="19 "></asp:TextBox>
                                        <span id="spanErrorName" runat="server" style="color: Red; font-weight: bold;"></span>
                                    </div>
                                    <div id="facebook_share_container" style="float: left; margin-top: 6px; display: none;">
                                        <input type="checkbox" id="facebook_share" checked="checked" style="margin-top: 5px;" /><label
                                            for="facebook_share" style="display: inline; font-size: 12px; top: -2px; position: relative;
                                            margin-right: 1px">Share on Facebook</label></div>
                                </div>
                                <div class="allighright">
                                    <div style="width: 200px; text-align: center; float: left; display: none">
                                        <p class="yt-messageRemaining">
                                            <span id="numberRemaining" runat="server">3000 characters remaining.</span></p>
                                    </div>
                                    <div id="divSubmitbutton" runat="server">
                                        <asp:LinkButton ID="btnPost" Width="90" Height="20" runat="server" CssClass="yt-Button yt-ArrowButton"
                                            Text="Post message" ValidationGroup="Comments" OnClick="btnPost_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="hack-clearBoth">
                            </div>
                            <asp:LinkButton runat="server" ID="linkLongCondolences" OnClick="liCondolences_Click"></asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="yt-ContentTertiary">
                    <div class="yt-Panel yt-Panel-Note" id="addstory" runat="server">
                        <h2 class="first">
                            <a href="story.aspx<%= _query_string %>">Add a story!</a></h2>
                        <p>
                            You can add a more detailed story to this
                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>.
                            We give you a few tips on what to write, and even provide an area where you can
                            add bits of trivia for visitors to read! No story information has been added to
                            this
                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                            yet – but you can create one by going to <a href="story.aspx<%= _query_string %>">the
                                story page</a>.</p>
                        <asp:LinkButton ID="lbtnRemoveMessage" CssClass="yt-NoteButton" CausesValidation="false"
                            runat="server" OnClick="lbtnRemoveMessage_Click">Remove this message</asp:LinkButton>
                    </div>
                    <div class="yt-Panel yt-Panel-Announcement" id="Announcement" runat="server">
                        <h2>
                            Special Announcement</h2>
                        <h3 id="title" runat="server">
                        </h3>
                        <p id="NoteMessage" runat="server">
                        </p>
                        <div class="yt-Form-Buttons">
                            <div class="yt-Form-Submit">
                                <asp:LinkButton ID="lbtnReadNode" CssClass="yt-Button yt-ArrowButton" runat="server"
                                    OnClick="lbtnReadNode_Click">Read the full note</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="yt-Panel-Primary" style="min-height: 110px; margin-top: 45px;">
                        <h2>
                            Share Your Memories</h2>
                        <div class="ShareMemories">
                            <div>
                                <li id="liCondolences" class="iCondolences" runat="server"><a style="color: #494949;"
                                    href="guestbook.aspx<%= _query_string %>">
                                    <asp:Label ID="lblGuestBook" runat="server"></asp:Label></a></li></div>
                            <div>
                                <li id="livirtualGift" class="ivirtualGift" runat="server"><a style="color: #494949;"
                                    href="gift.aspx<%= _query_string %>">Give a free virtual gift</a></li></div>
                            <div>
                                <li id="LiGift" class="iPhotos" runat="server"><a style="color: #494949;" href="photos.aspx<%= _query_string %>">
                                    Upload your photos</a></li></div>
                            <div>
                                <li id="liVideo" class="iVideo" runat="server"><a style="color: #494949;" href="videos.aspx<%= _query_string %>">
                                    Add a video</a></li></div>
                        </div>
                    </div>
                    <div class="yt-Panel ShareBox" style="margin: 20px 0 0 !important;">
                        <p style="margin-top: -5px !important;">
                            <asp:Label ID="lblMessageInvite" runat="server"></asp:Label>
                        </p>
                        <div id="liEmailPage" cssclass="iEmailPage">                            
                            <div class="addthis_toolbox addthis_default_style">
                                <a class="addthis_button_email" style="color: #494949;">
                                    <div>
                                    </div>
                                    Share by email</a>                                
                            </div>
                        </div>
                        <div id="liInternetShare" cssclass="iInternetShare">
                            <div class="yt-Tools-Sharing addthis_toolbox addthis_default_style" id="liBookmarkandShare"
                                runat="server">
                                <a class="addthis_button_compact" style="color: #494949;">
                                    <div>
                                    </div>
                                    Share on the internet</a>
                            </div>
                        </div>
                        <div class="dasshedTop">
                            <div style="margin-top: 8px; padding: 5px;">
                                <!--Old AddThis Button BEGIN -->
                                <div class="addthis_toolbox addthis_default_style ">
                                    <div style="float: left; width: 110px">
                                        <a class="addthis_button_facebook_like" fb: like:layout="button_count"></a>
                                    </div>
                                    <div style="float: left; width: 120px">
                                        <a class="addthis_button_tweet"></a>
                                    </div>
                                    <div id="lblPlusone" style="float: left; width: 60px" runat="server">                                        
                                    </div>
                                </div>

                                <script type="text/javascript" src="http://s7.addthis.com/js/250/addthis_widget.js#pubid=yourtribute"></script>
                             
                            </div>
                        </div>
                    </div>
                    <div class="yt-Panel yt-Panel-About" style="margin: 20px 0 0 !important;">
                        <div class="yt-PanelContent">
                            <h2 id="YT2" runat="server">
                                About This
                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%></h2>
                            <dl>
                                <!--Start - Modification on 7-Dec-09 for the enhancement 5 of the Phase 1-->                               
                                <dt id="lblTributeCreatedByOrProvidedBy" runat="server"></dt>
                                <!--End-->
                                <dd id="lbtnCreatedby" runat="server">
                                </dd>
                                <!-- Start - Modification on 17-Dec-09 for the enhancement 6 of the Phase 1 -->
                            </dl>
                            <div id="divCompanyLogo" runat="server">
                                <img id="imgCompanyLogo" runat="server" alt="Company Logo" width="173" height="100" />
                            </div>
                            <dl>
                                <!-- End -->
                                <dt id="lblOtherAdminist" runat="server">Other
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                    administrators:</dt>
                                <dd id="rtrOtherAdminist" runat="server">
                                </dd>
                                <dt id="lblAccountExpire" runat="server"></dt>
                                <dd id="txtAccountExpire" runat="server">
                                </dd>
                            </dl>
                            <div class="yt-Form-Buttons" id="btnsponcer" runat="server">
                                <div class="yt-Form-Submit">
                                    <asp:LinkButton ID="lnkSponsor" runat="server" CausesValidation="false" OnClick="lnkSponsor_Click"
                                        CssClass="yt-Button yt-ArrowButton">Upgrade this <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%></asp:LinkButton>
                                </div>
                            </div>
                            <p id="Visit" runat="server">
                            </p>
                        </div>
                    </div>
                    <% if (_packageId == 8 || _packageId == 3)
                       { %>
                    <div class="yt-GoogleAdBox-SideSquare" id="BannerAdBoxRight" runat="server">
                        <div class="yt-Scissors">
                        </div>
                        <div class="yt-GoogleAdContent">
                            <div>

                                <script type='text/javascript'>
                                <% if (Request.Url.ToString().ToLower().Contains("wedding"))
                                   {%>                               
                                   
                                    GA_googleFillSlot("YourTribute_Wedding_Homepage_Center_300x250");
                               
                                <% } %>
                                <% else if (Request.Url.ToString().ToLower().Contains("anniversary"))
                                    {%>
                                
                                     GA_googleFillSlot("YourTribute_Anniversary_Homepage_Center_300x250");
                               
                                <% } %>
                                <% else if (Request.Url.ToString().ToLower().Contains("graduation"))
                                    {%>
                               
                                     GA_googleFillSlot("YourTribute_Graduation_Homepage_Center_300x250");
                                    
                                <% } %>
                                <% else if (Request.Url.ToString().ToLower().Contains("memorial"))
                                    {%>
                                
                                     GA_googleFillSlot("YourTribute_Memorial_Homepage_Center_300x250");
                                   
                                <% } %>
                                <% else if (Request.Url.ToString().ToLower().Contains("baby"))
                                    {%>
                               
                                     GA_googleFillSlot("YourTribute_Baby_Homepage_Center_300x250");
                                   
                                <% } %>
                                <% else if (Request.Url.ToString().ToLower().Contains("birthday"))
                                    {%>
                                
                                     GA_googleFillSlot("YourTribute_Birthday_Homepage_Center_300x250");
                                    
                                <% } %>
                                </script>

                            </div>
                            <p class="infoMessage" id="YT7" runat="server">
                                *Upgrade this
                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                                to remove this advertisement</p>
                        </div>
                    </div>
                    <% } %>
                </div>
                <div class="yt-ContentSecondary" style="margin-top: 10px !important;">
                    <div class="yt-Panel-Primary yt-Feed">
                        <h2>
                            Latest
                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                            Activity</h2>
                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                    </div>
                </div>
                <!--yt-ContentTertiary-->
                <div class="hack-clearBoth">
                </div>
                <% if (_packageId == 8 || _packageId == 3)
                   { %>
                <div class="yt-GoogleAdBox-BottomLarge" id="BannerAdBoxBottom" runat="server">
                    <div class="yt-Scissors">
                    </div>
                    <div class="yt-GoogleAdContent">
                        <div>

                            <script type='text/javascript'>
                                <% if (Request.Url.ToString().ToLower().Contains("wedding"))
                                   {%>                               
                                   
                                    GA_googleFillSlot("YourTribute_Wedding_Homepage_Bottom_728x90");
                               
                                <% } %>
                                <% else if (Request.Url.ToString().ToLower().Contains("anniversary"))
                                    {%>
                                
                                     GA_googleFillSlot("YourTribute_Anniversary_Homepage_Bottom_728x90");
                               
                                <% } %>
                                <% else if (Request.Url.ToString().ToLower().Contains("graduation"))
                                    {%>
                               
                                     GA_googleFillSlot("YourTribute_Graduation_Homepage_Bottom_728x90");
                                    
                                <% } %>
                                <% else if (Request.Url.ToString().ToLower().Contains("memorial"))
                                    {%>
                                
                                     GA_googleFillSlot("YourTribute_Memorial_Homepage_Bottom_728x90");
                                   
                                <% } %>
                                <% else if (Request.Url.ToString().ToLower().Contains("baby"))
                                    {%>
                               
                                     GA_googleFillSlot("YourTribute_Baby_Homepage_Bottom_728x90");
                                   
                                <% } %>
                                <% else if (Request.Url.ToString().ToLower().Contains("birthday"))
                                    {%>
                                
                                     GA_googleFillSlot("YourTribute_Birthday_Homepage_Bottom_728x90");
                                    
                                <% } %>
                            </script>

                        </div>
                        <p class="infoMessage" id="YT8" runat="server">
                            *Upgrade this tribute to remove this advertisement</p>
                    </div>
                </div>
                <% } %>
                <div class="yt-ContentContainerImage">
                </div>
            </div>
        </div>
        <div >
            <uc:Footer ID="Footer1" runat="server" />
        </div>
    </div>
    <div class="upgrade">
        <h2>
            Please Upgrade Your Browser</h2>
        <p>
            This site&#39;s design is only visible in a graphical browser that supports <a href="http://www.webstandards.org/upgrade/"
                title="The Web Standards Project's BROWSER UPGRADE initiative.">web standards</a>,
            but its content is accessible to any browser or Internet device.</p>
    </div>
    </form>

    <script type="text/javascript">
        executeBeforeLoad();           
        <% if (ConfigurationManager.AppSettings["ApiKey"] != null) { %> 
        function update_user_is_connected() {
            header_user_is_connected();
            FB.XFBML.parse();
        }
        function update_user_is_not_connected() {
            header_user_is_not_connected();
            FB.XFBML.parse();
        }                          
        window.fbAsyncInit = function() {
    FB.init({
        appId  : '<%= ConfigurationManager.AppSettings["ApiKey"].ToString() %>',
        status : true, // check login status
        cookie : true, // enable cookies to allow the server to access the session
        xfbml  : true,  // parse XFBML
        //channelUrl  : 'http://www.yourdomain.com/channel.html', // Custom Channel URL
        oauth : true //enables OAuth 2.0
    });

    FB.getLoginStatus(function(response) {
        if (response.authResponse) update_user_is_connected();
        else update_user_is_not_connected();
    });

    // This will be triggered as soon as the user logs into Facebook (through your site)
    FB.Event.subscribe('auth.login', function(response) {
        update_user_is_connected();
    });
};
        
        <% } %>        
//    LHK: for GuestBook posts

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
    function validateInput() {
                //LHK:function call for getting value of is iniframe
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
        function funcTextCount(txtMsg) {
            if (txtMsg.value.length == 3000) {
                return false;

            }
            else {

                return true;
            }
        }
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
                        if (Tribute_TributeHomePage_)
                            Tribute_TributeHomePage_.SetSessionValues(name.value, messg.value);
                        PostMessageModalpopup(location.href, document.title);
                        return false;

                    }

                }
                return false;
            }
            
            function LinkToMailPage() {
            var validation = document.getElementById('<%= hdnTypeToMail.ClientID %>');
            if (document.getElementById('<%= hdnTopUrl.ClientID %>') != null)
                topurl = document.getElementById('<%= hdnTopUrl.ClientID %>').value;
            if ((window.location != window.parent.location) && (topurl != "")) {
                validation.value = topurl + '?' + location.href;
            }
            else {
                validation.value = location.href;
            }
        }
        
    function change_parent_location(url)
    {
    window.setlocation=url;
    }; 


    </script>

</body>
</html>
