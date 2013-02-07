<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManagePhoto.aspx.cs" Inherits="Photo_ManagePhoto"
    Title="ManagePhoto" MasterPageFile="~/Shared/Story.master" %>

<%@ Register TagPrefix="ccPiczard" Namespace="CodeCarvings.Piczard.Web" Assembly="CodeCarvings.Piczard, Version=1.2.3.0, Culture=neutral, PublicKeyToken=88d00d4422733d60" %>
<asp:Content ID="content1" ContentPlaceHolderID="TabContentPlaceHolder" runat="Server">

    <script type="text/javascript" src="../Common/JavaScript/Common.js"></script>

    <script language="javascript" type="text/javascript">
        //to restrict the value of Video Descritpion to 1000 characters
        //this is to restrict characters after 1000 characters
        function maxLength() {
            if (document.getElementById('ctl00_ModuleContentPlaceHolder_txtPhotoDesc') != null) {
                var txtVal = document.getElementById('ctl00_ModuleContentPlaceHolder_txtPhotoDesc').value;
                if (txtVal != "" && txtVal.length > 0)
                    return chkForMaxLength(1000, txtVal.length);
                else return 0;
            }
        }
        //this is for checking the length after clicking on post button
        function maxLength2(source, args) {
            var txtVal = document.getElementById('ctl00_ModuleContentPlaceHolder_txtPhotoDesc').value;
            if (txtVal != null && txtVal.length > 0)
                args.IsValid = chkForMaxLength(1000, txtVal.length);
        }
    </script>

    <div class="yt-Breadcrumbs" id="nvgManagePhoto" runat="server">
    </div>
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ModuleContentPlaceHolder" runat="Server">
    <div id="divShowModalPopup">
    </div>
    <div class="yt-ContentPrimary">
        <asp:ValidationSummary ID="vsErrorSummary" runat="server" CssClass="yt-Error" HeaderText=" <h2>Oops - there was a problem with your photo.</h2></br><h3>Please correct the error(s) below:</h3>"
            ForeColor="Black" />
        <div id="nvgEditPage" runat="server">
        </div>
        <div class="yt-Panel-Primary" style="width: 852px;">
            <h2 id="hPhotoEdit" runat="server">
            </h2>
            <fieldset class="yt-Form">
                <div class="yt-Form-Field">
                    <label for="txtPhotoCaption" id="lblPhotoCaption" runat="server">
                    </label>
                    <asp:TextBox ID="txtPhotoCaption" runat="server" CssClass="yt-Form-Input-Long" MaxLength="100"
                        TabIndex="1"></asp:TextBox>
                </div>
                <div class="yt-Form-Field">
                    <label for="txtPhotoDesc" id="lblPhotoDesc" runat="server">
                    </label>
                    <asp:TextBox ID="txtPhotoDesc" runat="server" CssClass="yt-Form-Textarea-XLong" Columns="50"
                        Rows="6" onkeypress="return maxLength();" MaxLength="1000" TextMode="MultiLine"
                        TabIndex="2" Width="846px"></asp:TextBox>
                    <asp:CustomValidator ID="cvPhotoDesc" runat="server" ClientValidationFunction="maxLength2"
                        Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000">!</asp:CustomValidator>
                </div>
                <div class="yt-Form-Field">
                    <label for="txtPhotoDesc" id="lblEditPhoto" runat="server">
                        Edit Photo:
                    </label>
                    <img id="imgPhoto" runat="server" src="" alt="" style="max-width: 630px;" />
                    <asp:PlaceHolder runat="server" ID="phHeader"></asp:PlaceHolder>
                    <div class="pageContainer">
                        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                            <ContentTemplate>
                                <div class="InlinePanel1" style="padding: 5px; margin-bottom: 10px; display:none">
                                    <asp:DropDownList runat="server" ID="ddlImageSelectionStrategy" CausesValidation="false">
                                        <asp:ListItem Value="Slice" Text="No margins"></asp:ListItem>
                                        <asp:ListItem Value="WholeImage" Text="Select whole image" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="DoNotResize" Text="Do not resize"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:DropDownList runat="server" ID="ddlOutputResolution" AutoPostBack="true" CausesValidation="false">
                                        <asp:ListItem Value="72" Text="72 DPI"></asp:ListItem>
                                        <asp:ListItem Value="96" Text="96 DPI (defalut)" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="300" Text="300 DPI"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Button runat="server" ID="btnLoadImage" Text="Load image" 
                                        CausesValidation="false" />
                                    <asp:Button runat="server" ID="btnProcessImage" Text="Process image &raquo;" 
                                        Enabled="false" CausesValidation="false" />
                                </div>
                                <ccPiczard:InlinePictureTrimmer ID="InlinePictureTrimmer1" runat="server" Width="100%"
                                    Height="500px" ShowImageAdjustmentsPanel="true" Culture="en" AutoFreezeOnFormSubmit="true" />
                                <br />
                                <div class="InlinePanel1" style="padding: 5px; margin-bottom: 10px; height: 36px; display:none">
                                    <div style="float: left;">
                                        Interface:
                                        <asp:DropDownList runat="server" ID="ddlInterfaceMode" AutoPostBack="true" CausesValidation="false"
                                            OnSelectedIndexChanged="ddlInterfaceMode_SelectedIndexChanged">
                                            <asp:ListItem Value="Full" Text="Full" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="Standard" Text="Standard"></asp:ListItem>
                                            <asp:ListItem Value="Easy" Text="Easy"></asp:ListItem>
                                            <asp:ListItem Value="Minimal" Text="Minimal"></asp:ListItem>
                                            <asp:ListItem Value="Poor" Text="Poor"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList runat="server" ID="ddlGfxUnit" AutoPostBack="true" CausesValidation="false">
                                            <asp:ListItem Value="Pixel" Text="Pixel" Selected="True"></asp:ListItem>
                                            <asp:ListItem Value="Point" Text="Point"></asp:ListItem>
                                            <asp:ListItem Value="Pica" Text="Pica"></asp:ListItem>
                                            <asp:ListItem Value="Inch" Text="Inch"></asp:ListItem>
                                            <asp:ListItem Value="Mm" Text="Mm"></asp:ListItem>
                                            <asp:ListItem Value="Cm" Text="Cm"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div style="float: left;">
                                        <div style="float: left;">
                                            <span style="line-height: 34px; vertical-align: middle;">&nbsp;&nbsp;&nbsp;Canvas color:
                                            </span>
                                        </div>
                                        <div style="float: left;">
                                            <div id="divCanvasColor" class="ColorSelector">
                                                <div>
                                                </div>
                                            </div>
                                        </div>
                                        <div style="float: left; margin-top: 2px;">
                                            <asp:TextBox runat="server" ID="txtCanvasColor" Style="width: 70px; text-align: center;"
                                                Enabled="false">#ffffff</asp:TextBox>
                                        </div>
                                        <br style="clear: both;" />
                                    </div>
                                    <br style="clear: both;" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="yt-MediaFoot">
                    <p id="pUploadedBy" runat="server">
                    </p>
                </div>
                <div class="yt-Form-Buttons">
                    <div class="yt-Form-Cancel">
                        <asp:LinkButton ID="lbtnCancel" runat="server" CausesValidation="false" OnClick="lbtnCancel_Click"
                            TabIndex="5" />
                    </div>
                    <div style="margin-right: 15px;">
                        <!-- If page is "Edit Album": -->
                        <div class="yt-Form-Submit">
                            <asp:LinkButton ID="lbtnSavePhoto" runat="server" OnClick="lbtnSavePhoto_Click" CssClass="yt-Button yt-ArrowButton"
                                TabIndex="3" />
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
