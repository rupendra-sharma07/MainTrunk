<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdmintratorLogin.aspx.cs"
    Inherits="TributePortalAdmin_AdmintratorLogin" Title="AdmintratorLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Tribute Portal::Adminstrator</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Content-Language" content="en-ca" />
    <meta http-equiv="imagetoolbar" content="false" />
    <meta name="robots" content="index,follow" />
    <meta name="MSSmartTagsPreventParsing" content="true" />
    <link rel="stylesheet" type="text/css" href="../assets/css/screen.css" />
    <link href="../assets/css/Siteadmin.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="divShowModalPopup"></div>
        <div class="yt-Container yt-SignUpForm yt-AnonymousUser">
            <div class="yt-HeaderContainer">
                <div class="yt-Header">
                    <a href="javascript:void(0);" title="Return to Your Tribute Home Page" class="yt-Logo">
                    </a>
                    <div class="yt-HeaderControls">
                        <div class="yt-NavHeader">
                            <div class="yt-UserInfo">
                                <div class="yt-UserInfo">
                                    Tribute Portal Admintrator
                                </div>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                    </div>
                </div>
            </div>
            <div class="TableBackground">
                <table width="50%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="2" class="LabelHeader">
                            User Login
                        </td>
                    </tr>
                    <tr>
                        <td class="LabelText" style="width:100px;">
                            Username:
                        </td>
                        <td>
                            <asp:TextBox ID="txtUsername" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="LabelText" style="width:100px;">
                            Password:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPassword" TextMode="Password" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
                        </td>
                        <td align="right">
                            <asp:Label ID="lblErrorMsg" runat="server" ForeColor="DarkRed"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
    </form>
</body>
</html>
