<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModelExpired.aspx.cs" Inherits="ModelPopup_ModelExpired" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head>
    <title>Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> - Help</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Content-Language" content="en-ca" />
    <meta http-equiv="imagetoolbar" content="false" />
    <meta name="robots" content="index,follow" />
    <meta name="MSSmartTagsPreventParsing" content="true" />
    <!-- really basic, generic html class stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/default.css" />
    <!-- default grid layout stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/layouts/centered1024_21.css" />
    <!-- print-friendly stylesheet -->
    <link rel="stylesheet" type="text/css" media="print" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/print.css" />
    <!-- screen elements stylesheet should be here -->
    <link rel="stylesheet" type="text/css" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/screen.css" />
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/ContactUs.css" />
    <!-- JS libraries -->

    <script type="text/javascript" src="../assets/scripts/mootools-1.11.js"></script>
    <script type="text/javascript" src="../assets/scripts/global.js"></script>
    <script type="text/javascript" src="../assets/scripts/tributecreation.js"></script>
    <!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
    <script type="text/javascript" src="../assets/scripts/BrowserOrTabCloseHandler.js"></script>
    <!--#include file="../analytics.asp"-->
</head>
<body>
    <form id="Form1" action="" runat="server">
    <div id="divShowModalPopup"></div> 
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="yt-Container">
                    <div class="yt-ContentContainer">
                        <div>
                            <p>
                                The Memorial <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> for JOHN MICHAEL SMITH expired 156 days ago on September 15,
                                2007.</p>
                            <p class="yt-WarningText">
                                If not renewed, the <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%> will be permanently deleted on September 15, 2008.</p>
                            <p>
                                <a href="javascript:void(0);">Click here to sponsor this <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%> and keep it online!</a></p>
                        </div>
                        <!--yt-ContentPrimaryContainer-->
                        <div class="hack-clearBoth">
                        </div>
                    </div>
                    <!--yt-ContentContainer-->
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <!--yt-Container-->
    </form>
</body>
</html>
