<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditPhotoAlbum.aspx.cs" Inherits="Photo_EditPhotoAlbum"
    Title="EditPhotoAlbum" MasterPageFile="~/Shared/Story.master" %>
    
<asp:Content ID="content1" ContentPlaceHolderID="TabContentPlaceHolder" runat="Server">
    <script language="javascript" type="text/javascript">
    //to restrict the value of Video Descritpion to 1000 characters
    //this is to restrict characters after 1000 characters
    function maxLength()
    {   
        var txtId=document.getElementById('ctl00_ModuleContentPlaceHolder_txtPhotoAlbumDesc');
        if(txtId != null)
       {
          var txtVal = txtId.value;
          if(txtVal != "" && txtVal.length >0)
             return chkForMaxLength(1000, txtVal.length);
          else return 0;
        }
    }
    //this is for checking the length after clicking on post button
    function maxLength2(source, args)
    {
        var txtId =document.getElementById('ctl00_ModuleContentPlaceHolder_txtPhotoAlbumDesc');
        if(txtId != null)
        {
        var txtVal = txtId.value;
        if(txtVal != "" && txtVal.length >0)
        args.IsValid = chkForMaxLength(1000, txtVal.length);
        }
    }
    
    //function to check for duplicate album name.
    function chkForDuplicate(source, args)
    {
        var txtName = document.getElementById('ctl00_ModuleContentPlaceHolder_txtPhotoAlbumCaption').value;
        if (isEmpty(txtName))
        {
            var txtOldName = $('ctl00_ModuleContentPlaceHolder_hdnOldName').value;
     
            if (txtOldName != txtName)
            {
                var txtAlbumList = $('ctl00_ModuleContentPlaceHolder_hdnAlbumList').value;
                args.IsValid = isUnique(txtName, txtAlbumList);
            }
            else
            {
                args.IsValid = true;
            }
        }
    }
    
//     </script>
    <div class="yt-Breadcrumbs" id="nvgEditPhotoAlbum" runat="server"></div>
</asp:Content>

<asp:Content ID="content2" ContentPlaceHolderID="ModuleContentPlaceHolder" runat="Server">
<asp:HiddenField ID="hdnAlbumList" runat="server" />
<asp:HiddenField ID="hdnOldName" runat="server" />
 <div id="divShowModalPopup"></div>
 <div class="yt-ContentPrimary">
     <div id="lblErrMsg" runat="server" align="left" class="yt-Error">
        </div>
        <asp:ValidationSummary ID="vsErrorSummary" runat="server" CssClass="yt-Error" HeaderText=" <h2>Oops - there is a problem with your album.</h2></br><h3>Please correct the error(s) below:</h3>"
            ForeColor="Black" />
        <div class="yt-Panel-Primary">
            <h2 id="hPhotoAlbumEdit" runat="server">
            </h2>
            <fieldset class="yt-Form">
                <div class="yt-Form-Field">
                    <label for="txtPhotoAlbumCaption" id="lblPhotoAlbumCaption" runat="server">
                    </label>
                    <asp:TextBox ID="txtPhotoAlbumCaption" runat="server" CssClass="yt-Form-Input-Long" MaxLength="100"
                        TabIndex="1" ValidationGroup="AlbumName"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPhotoAlbumCaption" runat="server" ValidationGroup="AlbumName" ControlToValidate="txtPhotoAlbumCaption" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000">!</asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvAlbumNameDuplicate" runat="server" ClientValidationFunction="chkForDuplicate"
                            Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ValidationGroup="AlbumName">!</asp:CustomValidator>
                   <%-- <asp:RegularExpressionValidator ID="reAlbumName" ControlToValidate="txtPhotoAlbumCaption"
                      Text="!" runat="server" ErrorMessage="Album name only contain letters and numbers"
                     ValidationGroup="AlbumName" ValidationExpression="^[A-Za-z0-9 _]*[A-Za-z0-9][A-Za-z0-9 _]*$" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"></asp:RegularExpressionValidator> --%>
                </div>
                <div class="yt-MediaFoot">
                    <div class="yt-Form-Field">
                        <label for="txtPhotoAlbumDesc" id="lblPhotoAlbumDesc" runat="server">
                        </label>
                        <asp:TextBox ID="txtPhotoAlbumDesc" runat="server" CssClass="yt-Form-Textarea-XLong" Columns="50"
                            Rows="6" onkeypress="return maxLength();" MaxLength="1000" TextMode="MultiLine"
                            TabIndex="2"></asp:TextBox>
                        <asp:CustomValidator ID="cvPhotoAlbumDesc" runat="server" ClientValidationFunction="maxLength2"
                            Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000">!</asp:CustomValidator>
                    </div>
                </div>
                <div class="yt-Form-Buttons">
                    <div class="yt-Form-Cancel">
                        <asp:LinkButton ID="lbtnCancel" runat="server" CausesValidation="false" OnClick="lbtnCancel_Click" TabIndex="5" />
                    </div>
                     <div style="margin-right:14px;">
                    <!-- If page is "Edit Album": -->
                    <div class="yt-Form-Submit">
                        <asp:LinkButton ID="lbtnSavePhotoAlbum" runat="server" CssClass="yt-Button yt-ArrowButton" OnClick="lbtnSavePhotoAlbum_Click" ValidationGroup="AlbumName" TabIndex="3" />
                    </div>
                    <div class="yt-Form-Submit">
                        <asp:LinkButton ID="lbtnDeletePhotoAlbum" runat="server" CssClass="yt-Button yt-ArrowButton" OnClick="lbtnDeletePhotoAlbum_Click" TabIndex="4" />
                    </div>
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
    <!--yt-ContentPrimary-->
</asp:Content>

