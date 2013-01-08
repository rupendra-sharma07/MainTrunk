<%@ Page Language="C#" MasterPageFile="~/TributePortalAdmin/PortalAdmin.master" AutoEventWireup="true"
    CodeFile="CategoryBasedThemeUpload.aspx.cs" Inherits="TributePortalAdmin_CategoryBasedThemeUpload"
    Title="Add Themes" %>

<asp:Content ID="content" ContentPlaceHolderID="DefaultContent" runat="Server">

  
<div>
    <div id="divShowModalPopup">
    </div>
    <div style="padding-top: 13px;">
        <div id="DivAddTheme" runat="server" class="DivThemeSelected" >
  <asp:LinkButton ID="lnkAddTheme" style="text-decoration:none; color:Black;" runat="server" Text="Add Theme" OnClick="lnkAddTheme_onClick"></asp:LinkButton></div>
        <div id="DivUpdateTheme" runat="server"  class="DivTheme" >
        <asp:LinkButton ID="lnkUpdateTheme" style="text-decoration:none; color:white;" runat="server" OnClick="lnkUpdateTheme_onClick">Update Theme</asp:LinkButton></div>
        <div id="DivDeleteTheme" runat="server"  class="DivTheme" >
        <asp:LinkButton ID="lnkDeleteTheme" style="text-decoration:none; color:white;" runat="server" OnClick="lnkDeleteTheme_onClick">Delete Theme</asp:LinkButton></div>
    </div>
    <br style="clear: both;" />
    <div style="margin-top:20px;">
    <asp:Panel ID="pnlUploadTheme" runat="server" Style="float: left; width: 70%;">
        <table id="Table1" width="100%" runat="server" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="2">
                  <asp:ValidationSummary ID="vsErrorSummary" ValidationGroup="AddTheme" runat="server" CssClass="yt-Error" HeaderText=" <h2>Oops - there was a problem with your search criteria.</h2></br><h3>Please correct the error(s) below:</h3>"
                        ForeColor="Black" />
                    <asp:ValidationSummary ID="vsErrorInvitationSummary" runat="server" CssClass="yt-Error"
                        HeaderText=" <h2>Oops - there was a problem with your search criteria.</h2></br><h3>Please correct the error(s) below:</h3>"
                        ForeColor="Black" ValidationGroup="InvitationCategory" />
                    <asp:Label runat="server" ID="lblErrorMessage" ForeColor="Red" />
                    
                     <%-- <div class="yt-Error" id="DivError" runat="server" style="width: 630px; display:none;">
                            <h2>
                                Oops - there was a problem with your search criteria.</h2>
                            <h3>
                                Please correct the error(s) below:</h3>
                            <ul id="lblErrorMessage" runat="server">
                            </ul>
                        </div>--%>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="LabelHeader">
                    <strong>Add Invitation Category &amp; Theme</strong>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCategory" runat="server" Text="Select Theme Category" CssClass="LabelText"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="LabelHeader">
                    <strong>Add Invitation Sub Category &amp; Theme</strong>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSubCategory" runat="server" Text="Select Sub Category " CssClass="LabelText"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSubCategory" runat="server" AutoPostBack="True" ValidationGroup="InvitationCategory">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rqvSubCategory" runat="server" ControlToValidate="ddlSubCategory"
                        ErrorMessage="Please select sub category" InitialValue="0" ValidationGroup="InvitationCategory">!</asp:RequiredFieldValidator>
                    <asp:Button ID="Button1" runat="server" Text="Add Theme SubCategory" OnClick="btnAddInvitationCategory_Click"
                        CausesValidation="False" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Panel ID="pnlInvitationSubCategory" runat="server">
                        <table width="50%" cellpadding="0" cellspacing="0" style="border-style: ridge">
                            <tr>
                                <td>
                                    <asp:Label ID="lblSubCategoryName" runat="server" Text="Enter Theme Sub Category Name:"
                                        CssClass="LabelText"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtInvitationSubCategory" runat="server" ValidationGroup="InvitationCategory"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqValSubCategoryName" runat="server" ControlToValidate="txtInvitationSubCategory"
                                        ErrorMessage="Please enter sub category name" ValidationGroup="InvitationCategory">!</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnAddSubCategory" runat="server" Text="Save Sub Category" OnClick="btnAddCategory_Click"
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
                    <asp:TextBox ID="txtThemeName" runat="server" MaxLength="50" ValidationGroup="AddTheme"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqValThemeName" runat="server" ControlToValidate="txtThemeName" ValidationGroup="AddTheme"
                        ErrorMessage="Please enter theme name">!</asp:RequiredFieldValidator>
                         <small>e.g. It's a Boy, Cheers! etc.</small>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblThemeValue" runat="server" Text="Enter Theme Value:" CssClass="LabelText"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtThemeValue" runat="server" MaxLength="50" ValidationGroup="AddTheme"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rqvThemeValue" runat="server" ControlToValidate="txtThemeValue" ValidationGroup="AddTheme"
                        ErrorMessage="Please enter theme value">!</asp:RequiredFieldValidator>
                        <small>e.g. BabyBoy, BirthdayCheers etc.</small>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSelectFile" runat="server" Text="Select Zip File:" CssClass="LabelText"></asp:Label>
                </td>
                <td>
                    <asp:FileUpload ID="fileUpload" runat="server" Width="350"  ValidationGroup="AddTheme"/>&nbsp;<asp:RequiredFieldValidator
                        ID="reqValFile" runat="server" ControlToValidate="fileUpload" ValidationGroup="AddTheme"
                        Display="Dynamic" ErrorMessage="Please select zip file">!</asp:RequiredFieldValidator>
                    <small>e.g. zip files contains all the images and theme</small>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="BtnUploadTheme" runat="server" Text="Add Theme"  ValidationGroup="AddTheme"  OnClick="btnAddTheme_Click" />
                    &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                        CausesValidation="False" />&nbsp;
                </td>
            </tr>
        </table>
    </asp:Panel>
  
    <asp:Panel ID="UpdateThemePanel" runat="server" Style="float: left; width: 70%;" >
    <div>
    
                    <asp:ValidationSummary ID="VSUpdateTheme" runat="server" CssClass="yt-Error"
                        HeaderText=" <h2>Oops - there was a problem with your search criteria.</h2></br><h3>Please correct the error(s) below:</h3>"
                        ForeColor="Black" ValidationGroup="UpdateTheme" />
                    <asp:Label runat="server" ID="lblUpdateErrormsg" ForeColor="Red" />
    </div>
    
    <div>
            <div style="float: left; width: 170px;">
                <asp:Label ID="lblThemeUpdateCategory" runat="server" Text="Select Theme Category" CssClass="LabelText"></asp:Label></div>
            <div style="float: left; width: 170px;">
                <asp:DropDownList ID="ddlThemeUpdateCategory" Width="150px" runat="server" ValidationGroup="UpdateTheme" AutoPostBack="true" OnSelectedIndexChanged="ddlThemeUpdateCategory_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
        <br style="clear: both;" />
        <div style="margin-top: 10px;">
            <div style="float: left; width: 170px;">
                <asp:Label ID="lblThemeUpdateSubCategory" runat="server" Text="Select Sub Category " CssClass="LabelText"></asp:Label></div>
            <div style="float: left; width: 170px;">
                <asp:DropDownList ID="ddlThemeUpdateSubCategory" Width="150px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlThemeUpdateSubCategory_SelectedIndexChanged" >
                <asp:ListItem Text="----------------"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvddlThemeUpdateSubCategory" runat="server" ControlToValidate="ddlThemeUpdateSubCategory" 
                ValidationGroup="UpdateTheme" InitialValue="----------------" Display="Dynamic" ErrorMessage="Please select sub category">!</asp:RequiredFieldValidator>
            </div>
        </div>
       
          <br style="clear: both;" />
        <div style="margin-top: 10px;">
            <div style="float: left; width: 170px;">
                <asp:Label ID="lblUpdateTheme" runat="server" Text="Select Theme " CssClass="LabelText"></asp:Label></div>
            <div style="float: left; width: 170px;">
                <asp:DropDownList ID="ddlUpdateTheme" Width="150px" runat="server" >
                <asp:ListItem Text="----------------"></asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvddlUpdateTheme" runat="server" ControlToValidate="ddlUpdateTheme" 
                ValidationGroup="UpdateTheme" InitialValue="----------------" Display="Dynamic" ErrorMessage="Please select theme">!</asp:RequiredFieldValidator>
 
            </div>
        </div>
         <br style="clear: both;" />
        <div style="margin-top: 10px;">
        <div style="float: left; width: 170px;">
                    <asp:Label ID="Label1" runat="server" Text="Select Zip File:" CssClass="LabelText"></asp:Label>
               </div>
               <div style="float: left;">
                    <asp:FileUpload ID="UpdatefileUpload" runat="server" Width="350"  ValidationGroup="UpdateTheme" />&nbsp;<asp:RequiredFieldValidator
                        ID="RequiredFieldValidator1" runat="server" ControlToValidate="UpdatefileUpload" Display="Dynamic" ValidationGroup="UpdateTheme"
                        ErrorMessage="Please select zip file">!</asp:RequiredFieldValidator>
                    <small>e.g. zip files contains all the images and theme</small>
               </div>
        </div>
         <br style="clear: both;" />
         
        <div style="margin-top: 10px;">
        <div style="float: left; margin-left:170px;">
        <asp:Button ID="btnUpdateTheme" runat="server" Text="Update Theme" ValidationGroup="UpdateTheme" OnClick="btnUpdatetheme_Onclick" />
       &nbsp; <asp:Button ID="btnUpdateCancelTheme" runat="server" Text="Cancel"   OnClick="btnUpdateCanceltheme_Onclick" />
        </div>
        </div>
    
    </asp:Panel>
    
   
    <asp:Panel ID="DeleteThemepanel" runat="server" Style="float: left; width: 70%; margin-top: 20px;">
    <div>
   
       <asp:ValidationSummary ID="VSDeleteTheme" runat="server" CssClass="yt-Error"
                        HeaderText=" <h2>Oops - there was a problem with your search criteria.</h2></br><h3>Please correct the error(s) below:</h3>"
                        ForeColor="Black" ValidationGroup="DeleteTheme" />
                    <asp:Label runat="server" ID="lblDeleteErrorMsg" ForeColor="Red" />
    </div>
        <div>
            <div style="float: left; width: 170px;">
                <asp:Label ID="lblThemecategory" runat="server" Text="Select Theme Category" CssClass="LabelText"></asp:Label></div>
            <div style="float: left; width: 170px;">
                <asp:DropDownList ID="ddlThemeCategory" Width="150px" runat="server" ValidationGroup="DeleteTheme" AutoPostBack="true" OnSelectedIndexChanged="ddlThemeCategory_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
        <br style="clear: both;" />
        <div style="margin-top: 10px;">
            <div style="float: left; width: 170px;">
                <asp:Label ID="lblThemeSubCategory" runat="server" Text="Select Sub Category " CssClass="LabelText"></asp:Label></div>
            <div style="float: left; width: 170px;">
                <asp:DropDownList ID="ddlThemeSubCategory" Width="150px" runat="server" ValidationGroup="DeleteTheme" AutoPostBack="True" OnSelectedIndexChanged="ddlThemeSubCategory_SelectedIndexChanged" >
                <asp:ListItem Text="----------------"></asp:ListItem>
                </asp:DropDownList>
                 <asp:RequiredFieldValidator ID="rfvddlThemeSubCategory" runat="server" ControlToValidate="ddlThemeSubCategory" 
                ValidationGroup="DeleteTheme" InitialValue="----------------" Display="Dynamic" ErrorMessage="Please select sub category">!</asp:RequiredFieldValidator>
 
            </div>
        </div>
        <br style="clear: both;" />
        <div style="margin-top: 10px;">
            <div style="float: left; width: 170px;">
                <asp:Label ID="lblTheme" runat="server" Text="Select Theme" CssClass="LabelText"></asp:Label></div>
            <div style="float: left; width: 170px;">
                <asp:DropDownList ID="ddlTheme" Width="150px" runat="server" ValidationGroup="DeleteTheme">
                <asp:ListItem Text="----------------"></asp:ListItem>
                </asp:DropDownList>
                 <asp:RequiredFieldValidator ID="rfvddlTheme" runat="server" ControlToValidate="ddlTheme" 
                ValidationGroup="DeleteTheme" InitialValue="----------------" Display="Dynamic" ErrorMessage="Please select theme">!</asp:RequiredFieldValidator>
 
            </div>
        </div>
        <br style="clear: both;" />
        <div style="margin-top: 10px;">
        <div style="float: left; margin-left:170px;">
        <asp:Button ID="btnDeletetheme" runat="server" Text="Delete Theme" ValidationGroup="DeleteTheme"
        OnClick="btnDeletetheme_Onclick" />
       &nbsp; <asp:Button ID="btncancelTheme" runat="server" Text="Cancel"       
       OnClick="btnCanceltheme_Onclick" />
        </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hdnFolderName" runat="server" Value="" />
    </div>
    <table id="Table2" width="100%" runat="server" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    </div>
</asp:Content>
