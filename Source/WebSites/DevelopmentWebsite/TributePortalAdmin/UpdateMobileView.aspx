<%@ Page Language="C#" MasterPageFile="~/TributePortalAdmin/PortalAdmin.master" AutoEventWireup="true" CodeFile="UpdateMobileView.aspx.cs" Inherits="TributePortalAdmin_UpdateMobileView" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DefaultContent" Runat="Server">

<asp:Panel ID="pnlSubmitUser" runat="server" Style="float: left; width: 70%" DefaultButton="btnSubmit">
        <asp:Label ID="lblUpdatedRecord" runat="server" ForeColor="Red" Visible="false"></asp:Label>
        <table id="Table1" width="100%" runat="server" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="2">
                    <asp:ValidationSummary ID="vsErrorSummary" runat="server" CssClass="yt-Error" HeaderText=" <h2>Oops - there was a problem with your search criteria.</h2></br><h3>Please correct the error(s) below:</h3>"
                        ForeColor="Black" />
                    <asp:Label runat="server" Visible="false" ID="lblErrorMessage" ForeColor="Red" />
                </td>
            </tr>
            <tr>
                <td>
                    <strong>Enable MobileView
                        </strong>
                </td>
                
            </tr>
            <tr>
                <td align="right" class="LabelText">
                    Update by UserId or UserName
                </td>
                <td>
                    <asp:RadioButton ID="rdoUserId" GroupName="UserIdOrUsername" runat="server" />
                    UserId
                    <asp:RadioButton ID="rdoUserName" GroupName="UserIdOrUsername" runat="server" />
                    UserName
                </td>
            </tr>
            <tr>
                <td align="right" class="LabelText">
                    <asp:Label ID="lblUserIdOrUserName" runat="server" Text="UserId/UserName"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUserIdOrUsername" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" />
                    &nbsp;
                    <asp:Button ID="btnSubmit" runat="server" Text="Enable" OnClick="btnSubmit_Click"
                        Width="60px" />
                    &nbsp;
                    <asp:Button ID="btnDisable" runat="server" Text="Disable" Style="text-align: center"
                        OnClick="btnDisable_Click" Width="60px" />
                </td>
            </tr>
        </table>
    </asp:Panel>


</asp:Content>

