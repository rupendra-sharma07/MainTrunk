<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddRemoveCredits.aspx.cs"
    Inherits="TributePortalAdmin_AddRemoveCredits" Title="Add or Remove Credits"
    MasterPageFile="PortalAdmin.master" %>

<asp:Content ID="content" ContentPlaceHolderID="DefaultContent" runat="Server">

    <script language="javascript" type="text/javascript">
       


    </script>

    <div id="divShowModalPopup">
    </div>
    <asp:Panel ID="pnlSubmitUser" runat="server" Style="float: left; width: 70%" DefaultButton="btnSubmit">
    <asp:Label ID="lblUpdatedCredit" runat="server" ForeColor="Red"></asp:Label>
        <table id="Table1" width="100%" runat="server" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="2">
                    <asp:ValidationSummary ID="vsErrorSummary" runat="server" CssClass="yt-Error" HeaderText=" <h2>Oops - there was a problem with your search criteria.</h2></br><h3>Please correct the error(s) below:</h3>"
                        ForeColor="Black" />
                    <asp:Label runat="server" Visible="false" ID="lblErrorMessage" ForeColor="Red" />
                </td>
            </tr>            
            <tr>
                <td colspan="2" class="LabelHeader">
                    <strong>Add and Remove Credits</strong>
                </td>
            </tr>
            <tr>
                <td align="right" class="LabelText">
                    Search by UserId or UserName
                </td>
                <td>
                    <asp:RadioButton ID="rdoUserId" GroupName="UserIdOrUsername" runat="server" 
                        />
                    UserId
                    <asp:RadioButton ID="rdoUserName" GroupName="UserIdOrUsername" runat="server" />
                    UserName
                </td>
            </tr>
            <tr>
                <td align="right" class="LabelText">
                    <asp:Label ID="lblUserIdOrUserName" runat="server" Text="UserId"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUserIdOrUsername" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="LabelText">
                    Account Type
                </td>
                <td class="LabelText">
                    <asp:RadioButton ID="rdoAdd"  GroupName="AddOrRemoveCredit" runat="server" />
                    Add
                    <asp:RadioButton ID="rdoRemove" GroupName="AddOrRemoveCredit" runat="server" />
                    Remove
                    
                </td>
            </tr>
            <tr>
                <td align="right" class="LabelText">
                    Enter the number of Credits
                </td>
                <td>
                    <asp:TextBox ID="txtCreditCount" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="LabelText">
                </td>
                <td>
                    <asp:CustomValidator ID="cvCreatedAfter" runat="server" 
                        Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000">!</asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CustomValidator ID="cvChkDate" runat="server"
                        Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000">!</asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"/>&nbsp;
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />&nbsp;
                  
                </td>
            </tr>
            <tr>
                <td>
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
