<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddNote.aspx.cs" Inherits="Notes_AddNote"
    Title="Add/Edit Note" MasterPageFile="~/Shared/Default.master" validateRequest="false" %>

<%@ Register Assembly="FreeTextBox" Namespace="FreeTextBoxControls" TagPrefix="FTB" %>
<asp:Content ID="content" ContentPlaceHolderID="DefaultContent" runat="Server">
<script type="text/javascript" src="../Common/JavaScript/Common.js"></script>
<script language="javascript" type="text/javascript">
    //to restrict the value of Video Descritpion to 1000 characters
    //this is for checking the length after clicking on post button
    function maxLength2(source, args)
    {
        var txtVal = document.getElementById('<%=ftbNoteMessage.ClientID%>').value;
        args.IsValid = chkForMaxLength(10000, txtVal.length);
    }
</script>
 <div id="divShowModalPopup"></div>
<table style="background-color: #ffffff; vertical-align: middle; width: 756px; text-align: left;">
    <tr>
        <td colspan="2">
            <asp:ValidationSummary ID="vsErrors" runat="server" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="lblNoteHeader" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="lblRequired" runat="server"></asp:Label>
        </td>
        
    </tr>
    <tr>
        <td style="width: 57px">
            <asp:Label ID="lblNoteTitle" runat="server"></asp:Label>
        </td>
        <td style="width: 104px">
            <asp:TextBox ID="txtNoteTitle" runat="server" MaxLength="250" Width="497px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNoteTitle" runat="server" ControlToValidate="txtNoteTitle"
                ErrorMessage="!"></asp:RequiredFieldValidator></td>
    </tr>
    <tr>
        <td style="width: 57px">
            <asp:Label ID="lblNoteMessage" runat="server"></asp:Label>
        </td>
        <td style="width: 104px">
            <FTB:FreeTextBox ID="ftbNoteMessage" runat="server" EnableHtmlMode="False"
                ToolbarLayout="bold,italic|JustifyLeft, JustifyRight, JustifyCenter">
            </FTB:FreeTextBox>
            <asp:CustomValidator ID="cvNoteMessage" runat="server" ErrorMessage="!" ClientValidationFunction="maxLength2"></asp:CustomValidator>
        </td>
    </tr>
    <tr>
        <td style="width: 57px">
            <asp:LinkButton ID="lbtnCancel" runat="server"></asp:LinkButton></td>
        <td style="width: 104px">
            <asp:LinkButton ID="lbtnPost" runat="server" OnClick="lbtnPost_Click"></asp:LinkButton></td>
    </tr>
</table>


</asp:Content>

