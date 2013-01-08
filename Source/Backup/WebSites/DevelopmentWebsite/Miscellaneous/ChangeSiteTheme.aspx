<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangeSiteTheme.aspx.cs" Inherits="Miscellaneous_ChangeSiteTheme"
    Title="ChangeSiteTheme" MasterPageFile="~/Shared/Default.master" %>
<asp:Content ID="content" ContentPlaceHolderID="DefaultContent" Runat="Server">
<div id="divShowModalPopup"></div> 
    <table style="width: 553px; background-color: #ffffff">
        <tr>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:RadioButtonList ID="rblThemes" runat="server">
                </asp:RadioButtonList></td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
               
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="btnChangeTheme" runat="server" OnClick="btnChangeTheme_Click" Text="Change Theme" /></td>
            <td>
            </td>
        </tr>
    </table>
		
</asp:Content>
