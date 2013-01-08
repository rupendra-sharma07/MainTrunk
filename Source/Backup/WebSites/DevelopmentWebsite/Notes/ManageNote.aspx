<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageNote.aspx.cs" Inherits="Notes_ManageNote"
    Title="Add/Edit Note" MasterPageFile="~/Shared/Story.master" ValidateRequest="false" %>
    
    <%@ Register Assembly="CuteEditor" Namespace="CuteEditor" TagPrefix="CE" %>
<%--<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FTB" %>--%>
<asp:Content ID="content1" ContentPlaceHolderID="TabContentPlaceHolder" runat="Server">
    <div class="yt-Breadcrumbs">
        <a id="aTributeHome" runat="server"><%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> Home</a> <a href="notes.aspx">
            Notes</a> <span id="spPageMode" runat="server" class="selected"></span>
    </div>
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ModuleContentPlaceHolder" runat="Server">
    <script language="javascript" type="text/javascript">


        $(document).addEvent('fb_connected', function() {
            $('facebook_share_container').setStyle('display', 'block');
            $('<%= facebook_share.ClientID %>').checked = true;
        });
    
    //to restrict the value of Video Descritpion to 1000 characters
    //this is for checking the length after clicking on post button
    function maxLength2(source, args)
    {
    if ($('ctl00_ModuleContentPlaceHolder_ftbNoteMessage') != null)
    {
        var txtVal = $('ctl00_ModuleContentPlaceHolder_ftbNoteMessage').value;
        //alert(txtVal);
        var txtValWithoutHtml = removeHTMLTags(txtVal);
        $('ctl00_ModuleContentPlaceHolder_hdnValueWithoutHtml').value = txtValWithoutHtml;
       // args.IsValid = chkForMaxLength(10000, txtValWithoutHtml.length);
       args.IsValid = true;  
     }
    }
    
    function hideControl()
    {
        if ($('ctl00_ModuleContentPlaceHolder_lblErrMsg') != null)
        {
            $('ctl00_ModuleContentPlaceHolder_lblErrMsg').style.visibility = "hidden"
            $('ctl00_ModuleContentPlaceHolder_lblErrMsg').style.height = "0"
            $('ctl00_ModuleContentPlaceHolder_lblErrMsg').style.width = "0"
        }
        if($('ctl00_ModuleContentPlaceHolder_spnMessage') != null)
        {
            $('ctl00_ModuleContentPlaceHolder_spnMessage').style.visibility = "hidden"
        }
    }
    </script>
<div id="divShowModalPopup"></div>
    <div class="yt-ContentPrimary">
    <asp:HiddenField ID="hdnValueWithoutHtml" runat="server" />
        <div>
            <asp:ValidationSummary ID="vsErrors" CssClass="yt-Error" runat="server" HeaderText=" <h2>Oops - there was a problem with your note.</h2></br><h3>Please correct the error(s) below:</h3>"
                ForeColor="Black" Width="622px" />
        </div>
        <div id="lblErrMsg" style="text-align:left" runat="server" class="yt-Error" visible="false"></div>
        <div class="yt-Panel-Primary">
            <h2>
                <asp:Label ID="lblNoteHeader" runat="server"></asp:Label></h2>
            <strong>
                <asp:Label ID="lblRequired" runat="server"></asp:Label></strong>
            <fieldset class="yt-Form">
                <div class="yt-Form-Field">
                    <label id="lblNoteTitle" for="txtNoteTitle" runat="server">
                    </label>
                    <asp:TextBox ID="txtNoteTitle" runat="server" CssClass="yt-Form-Input-Long" MaxLength="250"
                        Width="497px" TabIndex="1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNoteTitle" runat="server" ControlToValidate="txtNoteTitle"
                        Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000">!</asp:RequiredFieldValidator>
                </div>
                <div class="yt-Form-Field">
                    <label id="lblNoteMessage" runat="server" for="ftbNoteMessage">
                    </label>
                    <div class="yt-FreeTextBox">
                        <%--<FTB:FCKeditor id="ftbNoteMessage" runat="server" BasePath="../fckeditor/" ToolbarSet="TributePortal" EnableViewState="False" >
                        </FTB:FCKeditor>--%>
                        <CE:Editor ID="ftbNoteMessage"  BreakElement="P" runat="server" TemplateItemList="Bold,Italic,Underline,JustifyLeft,JustifyRight,JustifyCenter" Width="630px" EnableViewState="False">
                                    </CE:Editor>
                        <span id="spnMessage" style="color: #FF8000; font-size: Medium; font-weight: bold;" visible="false" runat="server">!</span>
                     </div>
                            
                    <asp:CustomValidator ID="cvNoteMessage" runat="server" ClientValidationFunction="maxLength2"
                        Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000">!</asp:CustomValidator>
                </div>
                <div class="yt-Form-Buttons">
                    <div class="yt-Form-Cancel">
                        <asp:LinkButton ID="lbtnCancel" runat="server" OnClick="lbtnCancel_Click" CausesValidation="false" TabIndex="4"></asp:LinkButton></div>
                    <div class="yt-Form-Submit">
                        <asp:LinkButton ID="lbtnPost" runat="server" CssClass="yt-Button yt-ArrowButton"
                            OnClick="lbtnPost_Click" TabIndex="3"></asp:LinkButton></div>
                    <div id="facebook_share_container" style="margin-top:7px;float:right;display:none"><asp:CheckBox ID="facebook_share" runat="server" Checked="false" /><label for="<%= facebook_share.ClientID %>">Share on Facebook</label></div>

                </div>
            </fieldset>
        </div>
    </div>
    <!--yt-ContentPrimary-->
</asp:Content>