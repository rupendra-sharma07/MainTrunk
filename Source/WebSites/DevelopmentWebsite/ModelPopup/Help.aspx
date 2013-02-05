<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Help.aspx.cs" Inherits="ModelPopup_Help" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/help.css" />
    <link rel="stylesheet" type="text/css" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/default.css" />
    <!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
    <script type="text/javascript" src="../assets/scripts/BrowserOrTabCloseHandler.js"></script>
<!--#include file="../analytics.asp"-->
</head>
<body>
    <form id="form1" runat="server">
    <div id="divShowModalPopup"></div> 
        <div class="yt-Container">
            <div class="yt-ContentContainer">
                <div class="yt-ContentPrimaryContainer">
                    <div class="yt-ContentPrimary">
                        <h3><asp:Label runat="server" ID="lblHeader" Font-Size="14px"></asp:Label></h3>
                        <div runat="server" id="frameDiv"> 
                        </div> 
                    </div>
                </div>
                <div class="yt-ContentSecondary">
                    <!--yt-ContentPrimaryContainer-->
                    <asp:TreeView ID="menuTree" runat="server" ImageSet="BulletedList2" OnSelectedNodeChanged="menuTree_SelectedNodeChanged" ShowExpandCollapse="False">
                        <ParentNodeStyle Font-Bold="False" Font-Underline="False" />
                        <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
                        <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="0px" NodeSpacing="0px" VerticalPadding="0px" Font-Underline="True" />
                        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                    </asp:TreeView>
                </div>
                <div id="setfocus" class="hack-clearBoth">
                </div>
            </div>
            <!--yt-ContentContainer-->
        </div>
    </form>
</body>
</html>
