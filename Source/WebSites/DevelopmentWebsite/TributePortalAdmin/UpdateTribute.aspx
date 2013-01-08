<%@ Page Language="C#" MasterPageFile="~/TributePortalAdmin/PortalAdmin.master" AutoEventWireup="true"
    CodeFile="UpdateTribute.aspx.cs" Inherits="TributePortalAdmin_UpdateTribute"
    Title="Untitled Page" %>

<asp:Content ID="ContentUpdateTrb" ContentPlaceHolderID="DefaultContent" runat="Server">
    <div style="padding-top: 13px;">
        <div id="DivUpdateExpiry" runat="server" class="DivThemeSelected">
            <asp:LinkButton ID="lnkUpdateExpiry" runat="server" Style="text-decoration: none;"
                OnClick="lnkUpdateExpiry_onClick">Update Expiry</asp:LinkButton>
        </div>
        <div id="DivSwitchTribute" runat="server" class="DivTheme">
            <asp:LinkButton ID="lnkSwitchTribute" runat="server" Style="text-decoration: none;"
                OnClick="lnkSwitchTribute_onClick">Switch Tribute</asp:LinkButton>
        </div>
        <div id="DivDegradePackage" runat="server" class="DivTheme">
            <asp:LinkButton ID="lnkDegradePackage" runat="server" Style="text-decoration: none;"
                OnClick="lnkDegradePackage_onClick">Degrade Package</asp:LinkButton>
        </div>
        <div id="DivShowTrans" runat="server" class="DivTheme">
            <asp:LinkButton ID="lbtnShowTrans" runat="server" Style="text-decoration: none;"
                OnClick="lbtnShowTrans_onClick">Display Transactions</asp:LinkButton>
        </div>
        <div id="divExpiry" runat="server" visible="false">
        </div>
        <div id="divSwitch" runat="server" visible="false">
        </div>
        <div id="divPackage" runat="server" visible="false">
        </div>
        <div id="divTribute" runat="server" visible="false">
        </div>
        <div id="divVideoTribute" runat="server" visible="false">
        </div>
    </div>
    <br style="clear: both;" />
    <div style="margin-top: 20px;">
        <table id="TableCommon" width="100%" runat="server" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:Label Style="color: Red;" ID="lblCommonError" Visible="false" runat="server" />
                </td>
            </tr>
            <tr id="ExpiryHeader" runat="server">
                <td colspan="4" class="LabelHeader">
                    <strong>Please enter Details of the Tribute</strong>
                </td>
            </tr>
            <tr id="trTributeId" runat="server">
                <td>
                    <asp:Label Text="Tribute Id" CssClass="LabelText">Tribute Id</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTributeId" runat="server" CausesValidation="True" MaxLength="8"
                        ValidationGroup="UpdateExpiry"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="valPassword" runat="server" ControlToValidate="txtTributeId"
                        Width="1px" ErrorMessage="Please enter Password to login" Font-Bold="True" Font-Size="Medium"
                        ForeColor="#FF8000" ValidationGroup="LoginGroup">!</asp:RequiredFieldValidator>
                </td>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td colspan="2" class="LabelHeader">
                    <asp:Button ID="btnGetExpiry" runat="server" Text="Submit" OnClick="btnGetExpiry_Click" />
                </td>
                <td colspan="2">
                </td>
            </tr>
            <tr id="trGridDetails" runat="server">
                <td colspan="8" class="LabelHeader">
                    <asp:GridView ID="gridDetails" runat="server" />
                </td>
            </tr>
            <%--<tr id="trExpiryMsg" runat="server">
                <td colspan="2">
                    <asp:Label ID="lblExpiryDateMsg" runat="server"></asp:Label>
                </td>
                <td colspan="2">
                </td>
            </tr>--%>
        </table>
        <table id="TableDetails" width="100%" runat="server" cellpadding="0" cellspacing="0">
            <tr id="Tr2" runat="server">
                <td colspan="4" class="LabelHeader">
                    <strong>Update Transactions</strong>
                </td>
            </tr>
            <tr>
                <td colspan="8" class="LabelHeader">
                    <asp:GridView ID="GridViewTransaction" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <asp:Panel ID="pnlUpdateExpiry" runat="server" Style="float: left; width: 70%;">
        <div>
            <asp:ValidationSummary ID="VSUpdateExpiry" runat="server" CssClass="yt-Error" HeaderText=" <h2>Oops - there was a problem with your search criteria.</h2></br><h3>Please correct the error(s) below:</h3>"
                ForeColor="Black" ValidationGroup="UpdateExpiry" />
            <asp:Label runat="server" ID="lblErrorUpdateExpiry" ForeColor="Red" Font-Bold="true" />
        </div>
        <div id="divExpiry">
            <table id="Table1" width="100%" runat="server" cellpadding="0" cellspacing="0">
                <tr id="trExpiryMsg" runat="server">
                    <td colspan="2">
                        <asp:Label ID="lblExpiryDateMsg" runat="server"></asp:Label>
                    </td>
                    <td colspan="2">
                    </td>
                </tr>
                <tr id="trUpdateMsg" runat="server">
                    <td class="LabelHeader">
                        <asp:Label ID="Label1" runat="server">Select New Expiry date in (mm/dd/yyyy) 
                        format</asp:Label>
                    </td>
                    <td>
                        <div>
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <asp:TextBox ID="txtNewExpiry" runat="server"></asp:TextBox>
                            <asp:Calendar ID="ExpiryCalendar" runat="server" Visible="false" />
                        </div>
                    </td>
                    <td class="LabelHeader">
                        <asp:Button ID="btnUpdateExpiry" runat="server" OnClick="btnUpdateExpiry_Click" Text="Update Expiry" />
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlSwitchTribute" runat="server" Style="float: left; width: 70%;">
        <div>
            <asp:ValidationSummary ID="VSSwitchTribute" runat="server" CssClass="yt-Error" HeaderText=" <h2>Oops - there was a problem with your Tribute criteria.</h2></br><h3>Please correct the error(s) below:</h3>"
                ForeColor="Black" ValidationGroup="SwitchTribute" />
            <asp:Label runat="server" ID="lblErrorSwitchTribute" ForeColor="Red" />
        </div>
        <div id="divAlertVideo" style="color: Red; font-weight: bold;" visible="false" runat="server">
        </div>
        <table id="TableSwitch" width="100%" runat="server" cellpadding="0" cellspacing="0">
            <tr id="tr3" runat="server">
                <td colspan="1" class="LabelHeader">
                    <asp:Label ID="Label2" runat="server">Select New <%=appWord%> Type from the 
                    list for TributeUrl : </asp:Label>
                    <asp:TextBox ID="txtboxNewTributeUrl" runat="server" CausesValidation="True"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvTributeAddress" runat="server" ControlToValidate="txtboxNewTributeUrl"
                        ErrorMessage="Tribute web address is a required field." Font-Bold="True" Font-Size="Medium"
                        ForeColor="#FF8000" ValidationGroup="SwitchTribute">!</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revTributeAddressNext" runat="server" ControlToValidate="txtboxNewTributeUrl"
                        Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9_]*$"
                        ValidationGroup="SwitchTribute"></asp:RegularExpressionValidator>
                    <asp:DropDownList ID="ddTributeTypes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddTributeType_OnSelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td colspan="1">
                    <asp:Label ID="lblDate1" runat="server"></asp:Label>
                    <asp:TextBox ID="txtDate1" runat="server" />
                    </br>
                    <asp:Label ID="lblDate2" runat="server"></asp:Label>
                    <asp:TextBox ID="txtDate2" runat="server" />
                </td>
                <td colspan="2" class="LabelHeader">
                    <asp:Button ID="btnSwitchTribute" runat="server" Text="Update Tribute" ValidationGroup="vgSwitch"
                        OnClick="btnSwitchTribute_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlDegradePackage" runat="server" Style="float: left; width: 70%;
        margin-top: 20px;">
        <div>
            <asp:ValidationSummary ID="VSDegradePackage" runat="server" CssClass="yt-Error" HeaderText=" <h2>Oops - there was a problem with your search criteria.</h2></br><h3>Please correct the error(s) below:</h3>"
                ForeColor="Black" ValidationGroup="DeleteTheme" />
            <asp:Label runat="server" ID="lblErrorDegradePackage" ForeColor="Red" />
        </div>
        <table id="TablePackage" width="100%" runat="server" cellpadding="0" cellspacing="0">
            <tr id="tr1" runat="server">
                <td colspan="2" class="LabelHeader">
                    <asp:Label ID="lblPkj" runat="server">Select New Package Type from the list </asp:Label>
                </td>
                <td colspan="2">
                    <div>
                        <asp:DropDownList ID="lstBoxTrbPackage" runat="server">
                        </asp:DropDownList>
                    </div>
                </td>
                <td colspan="2" class="LabelHeader">
                    <asp:Button ID="btnUpgaredPackage" runat="server" Text="Update Package" OnClick="btnUpgaredPackage_Click" />
                </td>
                <td>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
