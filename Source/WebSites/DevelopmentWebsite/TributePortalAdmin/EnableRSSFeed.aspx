<%@ Page Language="C#" AutoEventWireup="true" Inherits="TributePortalAdmin_EnableRSSFeed"
    CodeFile="EnableRSSFeed.aspx.cs" Title="Enable RSS Feed" MasterPageFile="PortalAdmin.master" %>

<asp:Content ID="content" ContentPlaceHolderID="DefaultContent" runat="Server">

    <script language="javascript" type="text/javascript">
    </script>

    <div id="divShowModalPopup">
    </div>
    <asp:Panel ID="pnlSubmitUser" runat="server" Style="float: left; width: 70%" DefaultButton="btnSubmit">
        <asp:Label ID="lblUpdatedFeed" runat="server" ForeColor="Red" Visible="false"></asp:Label>
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
                    <strong>Enable XML Feed
                        <asp:RadioButton ID="rdoBtnXML" GroupName="RssXml" runat="server" OnCheckedChanged="RadioSelection_Changed" /></strong>
                </td>
                <td>
                    <strong>Enable RSS Feed
                        <asp:RadioButton ID="rdoBtnRSS" GroupName="RssXml" runat="server" OnCheckedChanged="RadioSelection_Changed" /></strong>
                </td>
            </tr>
            <tr>
                <td align="right" class="LabelText">
                    Search by UserId or UserName
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
            <tr>
                <td>
                    <asp:Label ID="lblLinkText" runat="server" Visible="false"></asp:Label>
                    <asp:Label ID="lblLinkUrl" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table id="Table2" width="100%" runat="server" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:Label ID="lblNoRecord" CssClass="LabelText" Visible="false" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
