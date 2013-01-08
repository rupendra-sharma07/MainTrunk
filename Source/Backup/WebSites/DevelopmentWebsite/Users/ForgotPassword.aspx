<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="Users_ForgotPassword"
    Title="ForgotPassword" MasterPageFile="~/Shared/Default.master" %>
<asp:Content ID="content" ContentPlaceHolderID="DefaultContent" Runat="Server">
	<div id="divShowModalPopup"></div>	
		<table style="color: #ffffff; width: 464px;">
		    <tr>
		        <td colspan="2">
                    <asp:Label ID="lblForgotPassword" runat="server" Text="Forgot your password?"></asp:Label>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                </td>
		    </tr>
		    <tr>
		        <td colspan="2">
                    <asp:Label ID="lblNote" runat="server" Text="Enter your email address to reset your password."></asp:Label></td>
		    </tr>
            <tr>
                <td colspan="2">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" Width="366px" />
                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Width="185px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 100px">
                    <asp:Label ID="lblEmail" runat="server" Text="Email Address" Width="117px"></asp:Label></td>
                <td style="width: 3px" align="left">
                    <asp:TextBox ID="txtEmailId" runat="server"></asp:TextBox><asp:RegularExpressionValidator
                        ID="revEmail" runat="server" ErrorMessage="Invalid email id."
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Width="1px" ControlToValidate="txtEmailId">!</asp:RegularExpressionValidator>
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmailId"
                        ErrorMessage="Emailid can not be blank.">!</asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" /></td>
            </tr>
		</table>
		
		
</asp:Content>
