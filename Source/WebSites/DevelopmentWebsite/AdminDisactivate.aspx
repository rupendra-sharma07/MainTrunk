<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminDisactivate.aspx.cs" Inherits="AdminDisactivate" %>
<%@ Register Src="UserControl/Header.ascx" TagName="Header" TagPrefix="uc" %>    
<%@ Register Src="UserControl/Footer.ascx" TagName="Footer" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml"  xmlns:fb="http://www.facebook.com/2008/fbml" xml:lang="en" lang="en">
<head runat="server">
    <title>YT Tester's tools</title>
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_SCRIPT_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/default.css" />
    <!-- default grid layout stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_SCRIPT_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/layouts/centered1024_1.css" />
    <!-- print-friendly stylesheet -->
    <link rel="stylesheet" type="text/css" media="print" href="<%=Session["APP_SCRIPT_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/print.css" />
    <!-- screen elements stylesheet should be here -->
    <link rel="stylesheet" type="text/css" href="<%=Session["APP_SCRIPT_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/screen.css" />
    <!-- create process specific stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_SCRIPT_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/tributecreation.css" />
    <!-- larger text stylesheets -->
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_SCRIPT_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/medium_text.css"
        title="medium_text" />
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_SCRIPT_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/large_text.css"
        title="large_text" />
    <!-- Website Favorite Icon -->
    <link rel="Shortcut Icon" type="image/x-icon" href="<%=Session["APP_SCRIPT_PATH"]%>assets/images/favicon.ico" />
    <!-- JS libraries -->
    <script type="text/javascript" src="<%=Session["APP_SCRIPT_PATH"]%>assets/scripts/mootools-1.11.js"></script>
    <script type="text/javascript" src="<%=Session["APP_SCRIPT_PATH"]%>assets/scripts/global.js"></script>
    <script type="text/javascript" src="<%=Session["APP_SCRIPT_PATH"]%>assets/scripts/styleSwitcher.js"></script>  
             
    <script src="https://connect.facebook.net/en_US/all.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="divShowModalPopup"></div>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
        </asp:ScriptManager>
        <div class="yt-Container yt-Admin">
            <uc:Header ID="ytHeader" Section="home" runat="server" />
            <div class="hack-clearBoth">
            </div>
            <div class="yt-ContentContainer">
                <div class="yt-ContentContainerInner">
                    <div class="yt-ContentPrimaryContainer">
                        <div class="yt-ContentPrimary">
                            <div id="lblErrMsg" runat="server" class="yt-Error" visible="false">
                            </div>
                            <div id="yt-AdminTools" class="yt-ModalWrapper">
                                <asp:ValidationSummary ID="ValidationSummary1" CssClass="yt-Error" HeaderText=" <h2>Oops - there was a problem with some account type information.</h2>                                                             <h3>Please correct the errors below:</h3>"
                                    ForeColor="Black" runat="server" Width="652px" />
                                <div class="yt-Panel-Primary">
                                    <h2>Disactivate Account</h2>  
                                    <fieldset class="yt-Form">
                                        <p class="yt-requiredFields">
                                            <strong>Required fields are indicated with <em class="required">* </em></strong>
                                        </p>  
                                        <div class="yt-Form-Field">
                                        <label><em class="required">* </em>Tester's Password:</label>
                                            <asp:TextBox ID="txtPassword" CssClass="yt-Form-Input yt-Form-Input-Password"
                                                TextMode="Password" MaxLength="50" runat="server" TabIndex="2"></asp:TextBox>
                                            <span id="errorPwd" runat="server" style="color: #FF8000; font-size: Medium; font-weight: bold;"
                                                visible="false">!</span><asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                                                    ErrorMessage="Password is a required field. " ControlToValidate="txtPassword"
                                                    Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" Width="1px">!</asp:RequiredFieldValidator>
                                        </div> 
                                        <div class="yt-Form-Field">
                                            <label><em class="required">* </em>UserName:</label>
                                            <asp:TextBox ID="txtUsername" CssClass="yt-Form-Input" MaxLength="250" runat="server"
                                                TabIndex="1"></asp:TextBox>
                                            <span style="color: #FF8000; font-size: Medium; font-weight: bold;" id="errorUserName"
                                                runat="server" visible="false">!</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                    ErrorMessage="UserName is a required field. " ControlToValidate="txtUsername"
                                                    Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" Width="1px">!</asp:RequiredFieldValidator>
                                        </div>     
                                        <div class="yt-Form-Buttons">
                                            <div class="yt-Form-Submit">
                                                <asp:LinkButton ID="btnDisactivate" CssClass="yt-Button yt-ArrowButton" runat="server" 
                                                 OnClick="btnDisactivate_Click">Delete!</asp:LinkButton>
                                            </div>
                                        </div>                                                                                                                                                    
                                    </fieldset>                              
                                </div>    
                            </div>
                        </div> 
                    </div>  
                    <!--yt-ContentPrimaryContainer-->
                    <!--yt-ContentSecondary-->
                    <div class="hack-clearBoth">
                    </div>
                    <div class="yt-ContentContainerImage">
                    </div>                        
                </div>  
                <!--yt-ContentContainerInner-->
                
            </div>      
            <!--yt-ContentContainer-->
            <div >
                <uc:Footer ID="Footer1" runat="server" />
            </div>
            <!--yt-Footer-->                
            </div>   
           
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
</script>  
<!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
<script type="text/javascript" src="<%=Session["APP_SCRIPT_PATH"]%>assets/scripts/BrowserOrTabCloseHandler.js"></script>   
</body>
<script type="text/javascript" src="<%=Session["APP_SCRIPT_PATH"]%>assets/scripts/modalbox.js"></script>
</html>
