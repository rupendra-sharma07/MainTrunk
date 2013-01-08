<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserLoginEvent.aspx.cs" Inherits="Users_UserLoginEvent"
    Title="UserLoginEvent" MasterPageFile="~/Shared/Default.master" %>
<asp:Content ID="content" ContentPlaceHolderID="DefaultContent" Runat="Server">
		<h1>UserLoginEvent</h1>
		<div id="divShowModalPopup"></div>
    <p>
        <table style="width: 643px; color: white">
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblTopMsg" runat="server" Font-Size="9pt" Width="212px">You have been invited to the event</asp:Label>
                    <asp:HyperLink ID="lnkEvent" runat="server">[lnkEvent]</asp:HyperLink>
                    <asp:Label ID="lblSecondTop" runat="server" Font-Size="9pt" Width="318px">To view the event and RSVP please login or sign-up.</asp:Label></td>
            </tr>
            <tr>
                <td style="width: 288px">
                    <asp:Label ID="lblLeftMsg" runat="server" Font-Size="9pt" Text="Already a member of your Tribute"></asp:Label></td>
                <td>
                    <asp:Label ID="lblRightMsg" runat="server" Font-Size="9pt" Text="Not a member yet?"
                        Width="251px"></asp:Label></td>
            </tr>
            <tr>
                <td align="center" style="width: 288px">
                    &nbsp;<asp:Login ID="lgnUserLogin" runat="server" DisplayRememberMe="False" OnAuthenticate="lgnUserLogin_Authenticate"
                        Width="266px">
                    </asp:Login>
                    <asp:LinkButton ID="lnkForgotPassword" runat="server" OnClick="lnkForgotPassword_Click"
                        Text="Forgot Password?"></asp:LinkButton></td>
                <td align="left">
                    <asp:LinkButton ID="lnkSignUp" runat="server">Sign-up</asp:LinkButton>
                    <asp:Label ID="lblMsg" runat="server" Text="to become a member of your tribute and contribute to existing tributes - or create your own!"></asp:Label>
                    *&nbsp;
                    <asp:Label ID="lblFast" runat="server" Text="It's Fast"></asp:Label><br />
                    *&nbsp;
                    <asp:Label ID="lblFree" runat="server" Text="It's Free"></asp:Label></td>
            </tr>
        </table>
    </p>
</asp:Content>
