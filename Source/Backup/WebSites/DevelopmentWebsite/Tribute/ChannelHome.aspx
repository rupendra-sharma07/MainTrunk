<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChannelHome.aspx.cs" Inherits="Tribute_ChannelHome"
    Title="ChannelHome" MasterPageFile="~/Shared/Default.master" %>
<asp:Content ID="content" ContentPlaceHolderID="DefaultContent" Runat="Server">
<div id="divShowModalPopup"></div>
		<h1>
            <asp:Label ID="lblNoRecord" runat="server" Font-Size="X-Small" Text="No featured tribute exists."
                Width="124px"></asp:Label>ChannelHome</h1>
		<asp:Repeater ID="rptFeaturedTributes" runat="server">
		<ItemTemplate>
            <tr class="search_row" >
                <td class="result_field" style="vertical-align:middle ; text-align:left ; height:100px; width:100px;"  >
                    <img id="Image" src="<%#Eval("TributeImage")%>" alt="" width="100" height="100" />
                </td>
                <td class="result_field" align="center" valign="middle"><%#Eval("TributeName")%> </td>
                <td class="result_field" align="center" valign="middle"><%#Eval("City")%> </td>
                <td class="result_field" align="center" valign="middle"><%#Eval("State")%> </td>
                <td class="result_field" align="center" valign="middle"><%#Eval("Country")%> </td>
                <td class="result_field" align="center" valign="middle"><%#Eval("CreatedDate")%></td>
                <td class="result_field" align="center" valign="middle"><%#Eval("NoRecord")%></td>
            </tr>
         </ItemTemplate>
        </asp:Repeater>
            
</asp:Content>
