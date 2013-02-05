<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InviteAdnminConfirmation.aspx.cs"
    Inherits="Users_InviteAdnminConfirmation" %>
<%@ Register Src="../UserControl/Inbox.ascx" TagName="Inbox" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/Footer.ascx" TagName="Footer" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head>
    <title>Your Tribute - Invite Admin Confirmation</title> <!--COMDIFFRES: Why it has been updated  -->
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
    <!-- larger text stylesheets -->
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/medium_text.css"
        title="medium_text" />
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/large_text.css"
        title="large_text" />
    <!-- Website Favorite Icon -->
    <link rel="Shortcut Icon" type="image/x-icon" href="../assets/images/favicon.ico" />
        
    <!-- JS libraries -->

    <script type="text/javascript" src="../assets/scripts/mootools-1.11.js"></script>

    <script type="text/javascript" src="../assets/scripts/global.js"></script>

    <script type="text/javascript" src="../assets/scripts/styleSwitcher.js"></script>

    <script type="text/javascript" src="../assets/scripts/modalbox.js"></script>

    <!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
    <script type="text/javascript" src="../assets/scripts/BrowserOrTabCloseHandler.js"></script>

    <script type="text/javascript">
    
    /* NOTE: may want to move this to an external .js */
    
    InitForm = function() {
        $$('.availabilityNotice').each( function(a) {
		    a.innerHTML = '';
		    a.className = 'availabilityNotice';
	    });
    }
    
   
	
    </script>

<script language="javascript" type="text/javascript"> 
 function ValidateConfirmation(source, args)
 {  
        if((!document.getElementById('<%= rdbAccept.ClientID %>').checked)&&(!document.getElementById('<%= rdbDecline.ClientID %>').checked))
        {
            args.IsValid=false;
        }         
 } 
 function Hideheader()
    {
    var Notice= document.getElementById('<%= Notice.ClientID %>'); 
    Notice.innerHtml='';
    Notice.style.visibility = 'hidden';  
    
    }

    </script>
    <!--#include file="../analytics.asp"-->
</head>
<body>
    <form action="" runat="server" >
    <div id="divShowModalPopup"></div>
        <div class="yt-Container yt-AnonymousUser yt-InviteAdmin">
            <div class="yt-HeaderContainer">
                <div class="yt-Header">
                    <a href="<%=Session["APP_BASE_DOMAIN"]%>home.aspx" title="Return to Your Tribute Home Page"
                        class="yt-Logo"></a>
                    <div class="yt-HeaderControls">
                        <div class="yt-NavHeader">
                          <div class="floatLeft" id="divProfile" runat="server">
                                <a id="myprofile" runat="server">My Account</a>
                                <uc1:Inbox ID="Inbox1" runat="server" />
                                
                            </div>
                            <div class="yt-UserInfo">
                                 <span>
                                    <asp:Label ID="Usernamelong" runat="server"></asp:Label>
                                </span>
                                <asp:LinkButton ID="lbtnLogout" runat="server" CausesValidation="False" OnClick="lbtnLogout_Click">Log out</asp:LinkButton>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="yt-Tools">
                            <div id="yt-TypeSizeControl" class="yt-TypeSizeControl">
                                <span class="floatLeft">Text Size:&#160;</span> <a href="javascript:void(0);" class="large"
                                    title="Large Text">a</a> <a href="javascript:void(0);" class="medium" title="Medium Text">
                                        a</a> <a href="javascript:void(0);" class="small" title="Small Text">a</a>
                            </div>
                        </div>
                        <!--yt-Tools-->
                    </div>
                    <!--yt-HeaderControls-->
                </div>
                <!--yt-Header-->
            </div>
            <!--yt-HeaderContainer-->
            <div class="hack-clearBoth">
            </div>
            <div class="yt-ContentContainer">
                <div class="yt-ContentContainerInner">
                    <div class="yt-ContentPrimaryContainer">
                        <div class="yt-ContentPrimary">
                            <div class="yt-Notice" id="Notice" runat="server">
                            </div>
                            <div>
                                <asp:ValidationSummary CssClass="yt-Error" ID="PortalValidationSummary" runat="server"
                                    Width="627px" HeaderText=" <h2>Oops - there was a problem with your confirmation.</h2>                                                             <h3>Please correct the errors below:</h3>"
                                    ForeColor="Black" Height="82px" />
                            </div>
                            <div class="yt-Panel-Primary">
                                <h2>
                                    Confirmation</h2>
                                <fieldset class="yt-Form">
                                    <div id="yt-LoginFormContainer">
                                        <h3>
                                            Would you like to become an administrator?
                                            <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="ValidateConfirmation"
                                                ErrorMessage="Please select Accept or Decline">!</asp:CustomValidator>
                                        </h3>
                                        <div class="yt-Form-Field-Radio">
                                            <asp:RadioButton ID="rdbAccept" runat="server" Text="Accept " GroupName="AdminConfirmation" />
                                        </div>
                                        <div class="yt-Form-Field-Radio">
                                            <asp:RadioButton ID="rdbDecline" runat="server" Text="Decline" GroupName="AdminConfirmation" />
                                        </div>
                                        <div class="yt-Form-Buttons">
                                            <div class="yt-Form-Submit">
                                                <asp:LinkButton ID="lbtnSubmit" CssClass="yt-Button yt-ArrowButton" runat="server"
                                                    OnClick="lbtnSubmit_Click">Submit</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                        </div>
                        <!--yt-ContentPrimary-->
                    </div>
                    <!--yt-ContentPrimaryContainer-->
                    <div class="hack-clearBoth">
                    </div>
                    <div class="yt-ContentContainerImage bgImageBUser">
                    </div>
                </div>
                <!--yt-ContentContainerInner-->
            </div>
            <!--yt-ContentContainer-->
            <div >
                <uc1:Footer ID="Footer1" runat="server" />                
            </div>
            <!--yt-Footer-->
        </div>
        <!--yt-Container-->
        <div class="upgrade">
            <h2>
                Please Upgrade Your Browser</h2>
            <p>
                This site's design is only visible in a graphical browser that supports <a href="http://www.webstandards.org/upgrade/"
                    title="The Web Standards Project's BROWSER UPGRADE initiative.">web standards</a>,
                but its content is accessible to any browser or Internet device.</p>
        </div>
        <!--yt-upgrade-->
    </form>

    <script type="text/javascript">
executeBeforeLoad();
    </script>

</body>
</html>
