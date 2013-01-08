<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManagePhoto.aspx.cs" Inherits="Photo_ManagePhoto"
    Title="ManagePhoto" MasterPageFile="~/Shared/Story.master" %>

<asp:Content ID="content1" ContentPlaceHolderID="TabContentPlaceHolder" runat="Server">

    <script type="text/javascript" src="../Common/JavaScript/Common.js"></script>

    <script language="javascript" type="text/javascript">
    //to restrict the value of Video Descritpion to 1000 characters
    //this is to restrict characters after 1000 characters
     function maxLength()
    {   if(document.getElementById('ctl00_ModuleContentPlaceHolder_txtPhotoDesc') != null)
        {
        var txtVal =  document.getElementById('ctl00_ModuleContentPlaceHolder_txtPhotoDesc').value;
        if(txtVal != "" && txtVal.length >0)
        return chkForMaxLength(1000, txtVal.length);
        else return 0;
        }
    }
    //this is for checking the length after clicking on post button
    function maxLength2(source, args)
    {
        var txtVal =  document.getElementById('ctl00_ModuleContentPlaceHolder_txtPhotoDesc').value;
        if(txtVal != null && txtVal.length >0)
        args.IsValid = chkForMaxLength(1000, txtVal.length);
    }
    </script>

    <div class="yt-Breadcrumbs" id="nvgManagePhoto" runat="server">
    </div>
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ModuleContentPlaceHolder" runat="Server">
 <div id="divShowModalPopup"></div>
    <div class="yt-ContentPrimary">
        <asp:ValidationSummary ID="vsErrorSummary" runat="server" CssClass="yt-Error" HeaderText=" <h2>Oops - there was a problem with your photo.</h2></br><h3>Please correct the error(s) below:</h3>"
            ForeColor="Black" />
        <div class="yt-Panel-Primary">
            <h2 id="hPhotoEdit" runat="server">
            </h2>
            <fieldset class="yt-Form">
                <div class="yt-Form-Field">
                    <label for="txtPhotoCaption" id="lblPhotoCaption" runat="server">
                    </label>
                    <asp:TextBox ID="txtPhotoCaption" runat="server" CssClass="yt-Form-Input-Long" MaxLength="100"
                        TabIndex="1"></asp:TextBox>
                </div>
                <div class="yt-MediaItem">
                    <img id="imgPhoto" runat="server" src="" alt="" style="max-width:630px;" /></div>
                <div class="yt-MediaFoot">
                    <div class="yt-Form-Field">
                        <label for="txtPhotoDesc" id="lblPhotoDesc" runat="server">
                        </label>
                        <asp:TextBox ID="txtPhotoDesc" runat="server" CssClass="yt-Form-Textarea-XLong" Columns="50"
                            Rows="6" onkeypress="return maxLength();" MaxLength="1000" TextMode="MultiLine"
                            TabIndex="2"></asp:TextBox>
                        <asp:CustomValidator ID="cvPhotoDesc" runat="server" ClientValidationFunction="maxLength2"
                            Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000">!</asp:CustomValidator>
                    </div>
                    <p id="pUploadedBy" runat="server">
                    </p>
                </div>
                <div class="yt-Form-Buttons">
                    <div class="yt-Form-Cancel">
                        <asp:LinkButton ID="lbtnCancel" runat="server" CausesValidation="false" OnClick="lbtnCancel_Click" TabIndex="5" />
                    </div>
                    <div style="margin-right:15px;">
                    <!-- If page is "Edit Album": -->
                    <div class="yt-Form-Submit">
                        <asp:LinkButton ID="lbtnSavePhoto" runat="server" OnClick="lbtnSavePhoto_Click" CssClass="yt-Button yt-ArrowButton" TabIndex="3" />
                    </div>
                    <div class="yt-Form-Submit">
                        <asp:LinkButton ID="lbtnDeletePhoto" runat="server" CssClass="yt-Button yt-ArrowButton"
                            OnClick="lbtnDeletePhoto_Click" TabIndex="4" />
                    </div>
                    </div>
                </div>
            </fieldset>
        </div>
    </div>
    <!--yt-ContentPrimary-->
</asp:Content>
