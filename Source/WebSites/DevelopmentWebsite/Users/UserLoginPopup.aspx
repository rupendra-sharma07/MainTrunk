<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserLoginPopup.aspx.cs" Inherits="Users_UserLoginPopup"
    Title="UserLoginPopup" MasterPageFile="~/Shared/Default.master" %>
<asp:Content ID="content" ContentPlaceHolderID="DefaultContent" Runat="Server">
<script language="javascript" type="text/javascript">
// <!CDATA[

function TABLE1_onclick() {

}

// ]]>
</script>
<div id="divShowModalPopup"></div>
		<h1>UserLoginPopup</h1>
		<table style="width: 314px; color: #ffffff" id="TABLE1" onclick="return TABLE1_onclick()">
            <tr>
                <td align="left" colspan="2">
                </td>
            </tr>
		    <tr>
		        <td style="width: 48px" align="left">
                    <asp:Label ID="lblUserName" runat="server" Text="User Name:" Width="93px"></asp:Label>
		        </td>
		        <td style="width: 395px" align="left" valign="top">
                    <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqUserName" runat="server" ControlToValidate="txtUserName"
                        ErrorMessage="User name required">!</asp:RequiredFieldValidator></td>
		    </tr>
   		    <tr>
		        <td style="width: 48px" align="left">
                    <asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label>
		        </td>
		        <td style="width: 395px" align="left" valign="top">
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="147px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqPassword" runat="server" ControlToValidate="txtPassword"
                        ErrorMessage="User name required">!</asp:RequiredFieldValidator></td>
		    </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Label ID="lblFailureText" runat="server" ForeColor="Red" Text="Label"></asp:Label></td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <asp:Button ID="btnSignin" runat="server" OnClick="btnSignin_Click" Text="Sign In" /></td>
            </tr>
            <tr>
                <td align="left" colspan="2">
                    <asp:Label ID="lblForgetPass" runat="server" Text="Forgot your username or your password?"></asp:Label></td>
            </tr>
            <tr>
                <td align="left" colspan="2">
                    <asp:Label ID="lblNote" runat="server" Text="We’ll send you an email with your username and a link to reset your password."
                        Width="306px"></asp:Label></td>
            </tr>
            <tr>
                <td align="left" colspan="2">
                    <asp:Label ID="lblEmail" runat="server" Text="Enter your email address:"></asp:Label><asp:TextBox
                        ID="txtEmail" runat="server" Width="193px"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="reqEEmail" runat="server" ControlToValidate="txtEmail"
                        ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">!</asp:RegularExpressionValidator>
                    <asp:Button ID="btnSendMail" runat="server" OnClick="btnSendMail_Click" Text="Send Mail" /></td>
            </tr>
            <tr>
                <td align="left" colspan="2" style="height: 20px">
                    <asp:Literal ID="ltrInvaliedEmail" runat="server" Text="Oops!" Visible="False"></asp:Literal></td>
            </tr>

		</table>
</asp:Content>
