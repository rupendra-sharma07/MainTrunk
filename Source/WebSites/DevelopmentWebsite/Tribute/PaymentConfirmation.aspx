<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaymentConfirmation.aspx.cs"
    Inherits="Tribute_PaymentConfirmation" Title="PaymentConfirmation" %>

<%@ Register Src="../UserControl/Inbox.ascx" TagName="Inbox" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head>
    <title>Your Tribute - Payment Confirmation</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Content-Language" content="en-ca" />
    <meta http-equiv="imagetoolbar" content="false" />
    <meta name="robots" content="index,follow" />
    <meta name="MSSmartTagsPreventParsing" content="true" />
    <!-- really basic, generic html class stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_SCRIPT_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/default.css" />
    <!-- default grid layout stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_SCRIPT_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/layouts/centered1024_1.css" />
    <!-- print-friendly stylesheet -->
    <link rel="stylesheet" type="text/css" media="print" href="<%=Session["APP_SCRIPT_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/print.css" />
    <!-- screen elements stylesheet should be here -->
    <link rel="stylesheet" type="text/css" href="<%=Session["APP_SCRIPT_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/screen.css" />
    <!-- larger text stylesheets -->
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_SCRIPT_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/medium_text.css"
        title="medium_text" />
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_SCRIPT_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/large_text.css"
        title="large_text" />
    <!-- JS libraries -->
    <!-- screen elements stylesheet should be here -->
    <link rel="stylesheet" type="text/css" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/screen.css" />

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/mootools-1.11.js"></script>

    <script type="text/javascript" src="<%=Session["APP_SCRIPT_PATH"]%>assets/scripts/global.js"></script>

    <script type="text/javascript" src="<%=Session["APP_SCRIPT_PATH"]%>assets/scripts/styleSwitcher.js"></script>

    <script type="text/javascript" src="<%=Session["APP_SCRIPT_PATH"]%>assets/scripts/modalbox.js"></script>

    <script type="text/javascript" src="<%=Session["APP_SCRIPT_PATH"]%>assets/scripts/admin.js"></script>

    <script type="text/javascript" src="<%=Session["APP_SCRIPT_PATH"]%>Common/JavaScript/CreditCardValidation.js"></script>

    <script type="text/javascript">
    
    /* NOTE: may want to move this to an external .js */
    
    InitForm = function() {
        $$('.availabilityNotice').each( function(a) {
		    a.innerHTML = '';
		    a.className = 'availabilityNotice';
	    });
    }
    </script>

    <!--#include file="../analytics.asp"-->
</head>
<body>
    <form id="Form1" runat="server" action="">
    <div id="divShowModalPopup">
    </div>
    <div class="yt-Container">
        <div class="yt-HeaderContainer">
            <div class="yt-Header">
                <a href="" title="Return to Your Tribute Home Page" class="yt-Logo"></a>
                <div class="yt-HeaderControls">
                    <div class="yt-NavHeader">
                        <div class="floatLeft" id="divProfile" runat="server">
                            <a id="myprofile" runat="server">My Account</a>
                            <uc1:Inbox ID="Inbox1" runat="server" />
                        </div>
                        <div class="yt-UserInfo">
                            <span>
                                <%=_userName%>
                            </span><span id="spanLogout" runat="server"></span>
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
                        <div class="yt-Panel-Primary">
                            <h3>
                                Your order has been approved!
                            </h3>
                            <div id="divPaid" runat="server">
                                <div id="package1" runat="server">
                                
                                </div>
                                <div id="package2" runat="server">
                                </div>
                                <%--<div id="autoRenew" runat="server">
                                        You can turn off auto-renewal at any time in tribute management.
                                        <br /--%>
                            </div>
                            <div id="recieveMail" runat="server">
                                
                                    <div id="CreditOrderDiv" runat="server" visible="false">
                                        <asp:Label ID="lblCreditOrder" runat="server"></asp:Label>
                                    </div>
                                    
                                
                                <p>
                                    You will recieve your full order receipt by email. If you do not recieve the order
                                    receipt in the next hour, please check your junk email folder. You can also view
                                    your billing history in "MY PROFILE" under "Billing Info", in case you chose to
                                    save it.
                                </p>
                                <p>
                                    <br />
                                    Please print a copy of this page for your records.</p>
                            </div>
                            <div class="yt-Form-Buttons">                           
                            <div class="yt-Form-Buttons">
                                <div class="yt-Form-Submit">
                                    <asp:LinkButton ID="lbtnNext" CssClass="yt-Button yt-ArrowButton" runat="server"
                                        OnClick="lbtnNext_Click">Click here to continue</asp:LinkButton>
                                </div>
                            </div>
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
        <!--yt-Footer-->
    </div>
    <div >
            <uc2:Footer ID="Footer1" runat="server" />
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
    <span class="yt-Form-Field"></span>

    <script type="text/javascript">
executeBeforeLoad();
    </script>
    
    <!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
    <script type="text/javascript" src="<%=Session["APP_SCRIPT_PATH"]%>assets/scripts/BrowserOrTabCloseHandler.js"></script>

</body>
</html>
