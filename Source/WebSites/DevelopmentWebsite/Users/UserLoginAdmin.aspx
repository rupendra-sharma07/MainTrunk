<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserLoginAdmin.aspx.cs" Inherits="Users_UserLoginAdmin"
    Title="UserLoginAdmin" MasterPageFile="~/Shared/Default.master" %>
<asp:Content ID="content" ContentPlaceHolderID="DefaultContent" Runat="Server">
		<h1>UserLoginAdmin</h1>
		<div id="divShowModalPopup"></div>
		<table style="width: 557px; color:White">
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblTopMsg" runat="server" Font-Size="9pt" Width="374px">You have been selected to be an administrator of the Tribute:</asp:Label>
                    <asp:Label ID="lblTributeName" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 288px">
                    <asp:Label ID="lblLeftMsg" runat="server" Text="Already a member of your Tribute" Font-Size="9pt"></asp:Label></td>
                <td>
                    <asp:Label ID="lblRightMsg" runat="server" Text="Not a member yet?" Width="251px" Font-Size="9pt"></asp:Label></td>
            </tr>
		    <tr>
		        <td align="center" style="width: 288px" >
                    &nbsp;<asp:Login ID="lgnUserLogin" runat="server"  Width="266px" OnAuthenticate="lgnUserLogin_Authenticate" DisplayRememberMe="False">
                    </asp:Login>
                    <asp:LinkButton ID="lnkForgotPassword" runat="server" Text="Forgot Password?" OnClick="lnkForgotPassword_Click"></asp:LinkButton></td>
		        <td align="left">
                    <asp:LinkButton ID="lnkSignUp" runat="server">Sign-up</asp:LinkButton>
                    <asp:Label ID="lblMsg" runat="server" Text="to become a member of your tribute and contribute to existing tributes - or create your own!"></asp:Label>
                    *&nbsp;
                    <asp:Label ID="lblFast" runat="server" Text="It's Fast"></asp:Label><br />
                    *&nbsp; 
                    <asp:Label ID="lblFree" runat="server" Text="It's Free"></asp:Label></td>
		    </tr>
		    
		</table>
		
</asp:Content>
