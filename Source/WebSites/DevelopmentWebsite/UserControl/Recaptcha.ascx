<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Recaptcha.ascx.cs" Inherits="UserControl_Recaptcha" %>
<asp:ScriptManager ID="smRecaptcha" runat="server">
</asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table>
            <tr>
                <td>
                    <div style="font-weight: bolder; font-size: 200%; background-image: url(../assets/images/bg.gif);
                        width: 150px; color: #ffffff; height: 32px; font-family: Algerian;" id="recaptcha"
                        runat="server">
                        sdfsdf
                    </div>
                </td>
                <td>
                    <asp:LinkButton ID="lbtnRefresh" runat="server" OnClick="LinkButton1_Click">Refresh</asp:LinkButton>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
