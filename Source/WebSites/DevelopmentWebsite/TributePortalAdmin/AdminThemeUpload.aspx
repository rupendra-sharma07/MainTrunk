<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/TributePortalAdmin/PortalAdmin.master"
    CodeFile="AdminThemeUpload.aspx.cs" Inherits="TributePortalAdmin_AdminThemeUpload"
    Title="AdminThemeUpload" %>

<asp:Content ID="content" ContentPlaceHolderID="DefaultContent" runat="Server">
    <div id="divShowModalPopup">
    </div>
    <asp:Panel ID="pnlUploadTheme" runat="server" Style="float: left; width: 70%" DefaultButton="btnUploadTheme">
        <table id="Table1" width="100%" runat="server" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="2">
                    <asp:ValidationSummary ID="vsErrorSummary" runat="server" CssClass="yt-Error" HeaderText=" <h2>Oops - there was a problem with your search criteria.</h2></br><h3>Please correct the error(s) below:</h3>"
                        ForeColor="Black" />
                    <asp:ValidationSummary ID="vsErrorInvitationSummary" runat="server" CssClass="yt-Error"
                        HeaderText=" <h2>Oops - there was a problem with your search criteria.</h2></br><h3>Please correct the error(s) below:</h3>"
                        ForeColor="Black" ValidationGroup="InvitationCategory" />
                    <asp:Label runat="server" ID="lblErrorMessage" ForeColor="Red" />
                </td>
            </tr>
            <tr>
                <td colspan="2" class="LabelHeader">
                    <strong>Add Invitation Category &amp; Theme</strong>
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
                    <asp:RequiredFieldValidator ID="reqValTributeType" runat="server" ControlToValidate="ddlTributeType"
                        InitialValue="0" >!</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblInvitationCategory" runat="server" Text="Select Invitation Category"
                        CssClass="LabelText"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlInvitationCategory" runat="server">
                    </asp:DropDownList>
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlInvitationCategory"
                        ErrorMessage="Please select invitation category" InitialValue="0">!</asp:RequiredFieldValidator>
                    &nbsp;
                    <asp:Button ID="btnAddInvitationCategory" runat="server" Text="Add Invitation Category"
                        OnClick="btnAddInvitationCategory_Click" CausesValidation="False" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Panel ID="pnlInvitationCategory" runat="server">
                        <table width="50%" cellpadding="0" cellspacing="0" style="border-style: ridge">
                            <tr>
                                <td>
                                    <asp:Label ID="lblCategoryName" runat="server" Text="Enter Invitation Category Name:"
                                        CssClass="LabelText"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtInvitationCategory" runat="server" ValidationGroup="InvitationCategory"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqValCategoryName" runat="server" ControlToValidate="txtInvitationCategory"
                                        ErrorMessage="Please enter category name" ValidationGroup="InvitationCategory">!</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnAddCategory" runat="server" Text="Save Category" OnClick="btnAddCategory_Click"
                                        ValidationGroup="InvitationCategory" />&nbsp;
                                    <asp:Button ID="btnCancelCategory" runat="server" Text="Cancel" OnClick="btnCancelCategory_Click"
                                        CausesValidation="False" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEventThemeName" runat="server" Text="Enter Theme Name:" CssClass="LabelText"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEventThemeName" runat="server" MaxLength="50" ValidationGroup="EventTheme"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqValThemeName" runat="server" ControlToValidate="txtEventThemeName"
                        ErrorMessage="Please enter theme name">!</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSelectThumbnailImage" runat="server" Text="Select Thumbnail Image:"
                        CssClass="LabelText"></asp:Label>
                </td>
                <td>
                    <asp:FileUpload ID="fileUploadThumbnailImage" runat="server" Width="350" />&nbsp;<asp:RequiredFieldValidator
                        ID="reqValThumbnailImage" runat="server" ControlToValidate="fileUploadThumbnailImage"
                        Display="Dynamic" ErrorMessage="Please select thumbnail image">!</asp:RequiredFieldValidator>
                    <small>e.g. 80x100</small>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSelectPreviewImage" runat="server" Text="Select Preview Image:"
                        CssClass="LabelText"></asp:Label>
                </td>
                <td>
                    <asp:FileUpload ID="fileUploadPreviewImage" runat="server" Width="350" />&nbsp;<asp:RequiredFieldValidator
                        ID="reValPreviewImage" runat="server" ControlToValidate="fileUploadPreviewImage"
                        ErrorMessage="Please enter preview image">!</asp:RequiredFieldValidator>
                    <small>e.g. 400x500</small>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFullSizeImage" runat="server" Text="Select Full Size Image:" CssClass="LabelText"></asp:Label>
                </td>
                <td>
                    <asp:FileUpload ID="fileUploadFullSizeImage" runat="server" Width="350" />&nbsp;<asp:RequiredFieldValidator
                        ID="reqValFullSizeImage" runat="server" ControlToValidate="fileUploadFullSizeImage"
                        ErrorMessage="Please enter full size image">!</asp:RequiredFieldValidator>
                    <small>e.g. 520x340</small>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblBackgroundColor" runat="server" Text="Enter Background Color:"
                        CssClass="LabelText"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtBackgroundcolor" runat="server" MaxLength="10"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator
                        ID="reqValBackgroundColor" runat="server" ControlToValidate="txtBackgroundcolor"
                        ErrorMessage="Please enter background color">!</asp:RequiredFieldValidator><small>e.g.
                            #d2ddb3</small>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnUploadTheme" runat="server" Text="Add Theme" OnClick="btnAddTheme_Click" />&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                        CausesValidation="False" />&nbsp;
                </td>
            </tr>            
        </table>
    </asp:Panel>
    <table id="Table2" width="100%" runat="server" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
