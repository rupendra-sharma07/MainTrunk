<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangeEmailPassword.aspx.cs" Inherits="Users_ChangeEmailPassword"
    Title="ChangeEmailPassword" MasterPageFile="~/Shared/Default.master" %>
<asp:Content ID="content" ContentPlaceHolderID="DefaultContent" Runat="Server">
 <div id="divShowModalPopup"></div>   
<asp:Panel ID="pnlEmailPassword" BackColor="white" runat="server" Width="365px">
<div>
<table style="width: 305px">
    <tr>
        <td align="left" colspan="2">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" Width="307px" />
            <asp:Label ID="lblMessage" runat="server" Font-Bold="False" Font-Size="Small" ForeColor="Red"
                Width="304px"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="2" align="left">
            <asp:Label ID="lblEmailTitle" runat="server" Width="117px" Font-Bold="True"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2" align="left">
            *<asp:Label ID="lblEmail" runat="server" Font-Size="Small"></asp:Label>&nbsp;</td>
    </tr>
    <tr>
        <td colspan="2" align="left">
            <asp:TextBox ID="txtEmail" runat="server" Width="227px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                ErrorMessage="Email can not be blank">!</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                ErrorMessage="Invalid Email Address." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">!</asp:RegularExpressionValidator></td>
    </tr>
</table>
</div>
<br/>
<div>
<table style="width: 313px">
    <tr>
        <td colspan="2" align="left">
            <asp:Label ID="lblPasswordTitle" runat="server" Font-Bold="True"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2" align="left">
            *<asp:Label ID="lblPassword" runat="server" Font-Size="Small"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="2" align="left">
            <asp:TextBox ID="txtPassword" runat="server" Width="226px" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                ErrorMessage="Password can not be blank">!</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td colspan="2" align="left">
            *<asp:Label ID="lblConformPassword" runat="server" Font-Size="Small"></asp:Label>
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="2" align="left">
            <asp:TextBox ID="txtConformPassword" runat="server" Width="223px" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvConformPass" runat="server" ControlToValidate="txtConformPassword"
                ErrorMessage="Conform Password can not be blnk">!</asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cvConformPassword" runat="server" ControlToCompare="txtPassword"
                ControlToValidate="txtConformPassword" ErrorMessage="Conform password is not same as password">!</asp:CompareValidator></td>
    </tr>
</table>
</div>
<asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click"
    Width="127px" /></asp:Panel>    
</asp:Content>
