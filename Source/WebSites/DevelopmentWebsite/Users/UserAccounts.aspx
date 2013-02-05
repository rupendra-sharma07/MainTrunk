<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserAccounts.aspx.cs" Inherits="Users_UserAccounts"
    Title="UserAccounts"  %>



<%--<%@ OutputCache NoStore="true" Duration="1" VaryByParam=none%>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head id="Head1" runat="server">
    <title>Your Tribute - Login</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Content-Language" content="en-ca" />
    <meta http-equiv="imagetoolbar" content="false" />
    <meta name="robots" content="index,follow" />
    <meta http-equiv="Expires" content="0"/>
    <meta http-equiv="Cache-Control" content="no-store"/>
    <meta http-equiv="Cache-Control" content="no-cache"/>
    <meta http-equiv="Pragma" content="no-cache"/>
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
    <!-- JS libraries -->

    <script type="text/javascript" src="../assets/scripts/mootools-1.11.js"></script>

    <script type="text/javascript" src="../assets/scripts/global.js"></script>

    <script type="text/javascript" src="../assets/scripts/styleSwitcher.js"></script>

    <!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
    <script type="text/javascript" src="../assets/scripts/BrowserOrTabCloseHandler.js"></script>

    <script type="text/javascript" src="../Common/JavaScript/Common.js"></script>
    <!--#include file="../analytics.asp"-->
</head>
<body>
    <form id="Form1" runat="server" action="">
    <div id="divShowModalPopup"></div>
        <div class="yt-Container yt-LoginForm yt-AnonymousUser">
            <div class="yt-HeaderContainer">
                <div class="yt-Header">
                    <a href=<%=Session["APP_BASE_DOMAIN"]%>home.aspx title="Return to Your Tribute Home Page" class="yt-Logo"></a>
                    <div class="yt-HeaderControls">
                        <div class="yt-NavHeader">
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
                            <div>

    <div class="yt-ContentPrimaryContainer" style="width: 98%; text-align:center">
        <div>            
            <div style=" text-align:center">
                <asp:Button ID="Button1" runat="server" PostBackUrl="pricing.aspx"
                    Text="Create Tribute." />&nbsp;
                <asp:Button ID="Button2" runat="server" OnClick="Button2_Click1" Text="Button" /></div>
            <div class="yt-Panel-Primary" style="text-align:left ">
                <asp:Repeater ID="repSearchTribute" runat="server" Visible="False" OnItemCommand="repSearchTribute_ItemCommand">
                    <ItemTemplate>
                        <table style="background-color:#ffffff" >
                            <tr>
                                <td align="left" valign="middle">
                                    <img id="imgTributeImage" alt="" height="50" src="../assets/images/bg_TributePhoto.gif"
                                        width="100" />
                                </td>
                                <td align="center" valign="middle">
                                    <asp:LinkButton ID="lbtnTributeName" CommandArgument='<%#Eval("TributeId")%>' Text='<%#Eval("TributeName")%>'
                                        runat="server">LinkButton</asp:LinkButton>
                                    <br>
                                    <br>
                                    <asp:Label ID="lblTributeType" runat="server" Text='<%#Eval("TypeDescription")%>'></asp:Label>
                                </td>
                                <td align="right" valign="top">
                                    <asp:Label ID="lblDate1" runat="server" Text='<%#Eval("CreatedDate")%>'></asp:Label>
                                </td>
                                <asp:HiddenField ID="hdnTributeName" runat="server" Value='<%#Eval("TributeName")%>' />
                                <asp:HiddenField ID="hdnTributeType" runat="server" Value='<%#Eval("TypeDescription")%>' />
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:Repeater>
                <!--yt-Form-->
            </div>
            <!--fieldset-->
        </div>
        <!--yt-ContentPrimary-->
    </div>
    
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
        </div> 
        </div>
        </div> 
        <!--yt-upgrade-->
        </div>
    </form>

    <script type="text/javascript">
executeBeforeLoad();
    </script>

</body>
</html>

