<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/TributePortalAdmin/PortalAdmin.master"
    CodeFile="DeleteCategoryAndTheme.aspx.cs" Inherits="TributePortalAdmin_DeleteCategoryAndTheme"
    Title="Delete Theme and category" %>

<asp:Content ID="content" ContentPlaceHolderID="DefaultContent" runat="Server">
    <div id="divShowModalPopup">
    </div>
    <asp:Panel ID="pnlUploadTheme" runat="server" Style="float: left; width: 70%">
        <table id="Table1" width="100%" runat="server" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="2" class="LabelHeader">
                    <asp:Label ID="lbl_delCategoryName" runat="server" Style="color: Red; font-size: 13px"></asp:Label>
                    <br />
                    <strong>Delete Invitation Category &amp; Theme</strong>
                    <asp:TextBox ID="txtEventThemeName" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtBackgroundcolor" runat="server" Visible="false"></asp:TextBox>
                    <asp:TextBox ID="txtInvitationCategory" runat="server" Visible="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTributeType" runat="server" Text="Select Tribute Type" CssClass="LabelText"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlTributeType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTributeType_SelectedIndexChanged"
                        ValidationGroup="InvitationCategory">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblInvitationCategory" runat="server" Text="Select Invitation Category"
                        CssClass="LabelText"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlInvitationCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlInvitationCategory_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <br />
                    <asp:Label ID="lblDelCategoryMessage" Text="(Note: Press 'Delete' to delete the category) "
                        runat="server" Style="font-family: sans-serif; font-style: italic"></asp:Label>
                    <asp:Label ID="lblDelThemeMessage" Text="(Note: Press 'Delete' to delete the theme) " runat="server"
                        Style="font-family: sans-serif; font-style: italic">
                    </asp:Label>
                    <asp:Label ID="lblNoCategoryExist" Text=" No Category exist for this tribute type!!"
                        runat="server" Style="font-family: sans-serif; color: Red"></asp:Label>
                    <asp:Label ID="lblNoThemeExist" Text="No Theme exist for this category!! " runat="server"
                        Style="font-family: sans-serif; color: Red"></asp:Label>
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gridCategories" AutoGenerateColumns="False" CssClass="yt-AdminTable yt-ContentContainer yt-ContentPrimary"
                        runat="server" AllowPaging="True" PageSize="10" OnPageIndexChanging="gridCategories_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="InvitationCategoryId" HeaderText="Category" ReadOnly="True"
                                Visible="false" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button runat="server" Text="Delete" ID="btndeleteCategory" CausesValidation="false"
                                        OnClick="GridRowDeleteCategory" OnClientClick="return confirm('Are you sure you want to delete this Category?');"
                                        CommandName='<%#Eval("InvitationCategoryName") %>' CommandArgument='<%#Eval("InvitationCategoryId")%>' />
                                </ItemTemplate>
                                <ControlStyle CssClass="yt-MiniButton yt-DeleteButton" />
                                <HeaderStyle BackColor="#8E7C71" ForeColor="White" />
                                <ItemStyle Wrap="True" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="InvitationCategoryName" HeaderText="Category" HeaderStyle-BackColor="#8E7C71"
                                HeaderStyle-ForeColor="White" />
                        </Columns>
                    </asp:GridView>
                    <asp:GridView ID="GridThemes" AutoGenerateColumns="False" runat="server" CssClass="yt-AdminTable yt-ContentContainer yt-ContentPrimary"
                        AllowPaging="True" PageSize="10" OnPageIndexChanging="GridThemes_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="InvitationCategoryId" HeaderText="Themes" ReadOnly="True"
                                Visible="false" />
                            <asp:BoundField DataField="EventThemeId" HeaderText="Themes" ReadOnly="True" Visible="false" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button runat="server" Text="Delete" ID="btndeleteTheme" CausesValidation="false"
                                        OnClick="GridRowDeleteThemes" OnClientClick="return confirm('Are you sure you want to delete this Theme?');"
                                        CommandName='<%#Eval("EventThemeName") %>' CommandArgument='<%#Eval("EventThemeId")%>' />
                                </ItemTemplate>
                                <ControlStyle CssClass="yt-MiniButton yt-DeleteButton" />
                                <HeaderStyle BackColor="#8E7C71" ForeColor="White" />
                                <ItemStyle Wrap="True" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="EventThemeName" HeaderText="Themes" HeaderStyle-BackColor="#8E7C71"
                                HeaderStyle-ForeColor="White" ReadOnly="True" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table id="Table2" width="100%" runat="server" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
