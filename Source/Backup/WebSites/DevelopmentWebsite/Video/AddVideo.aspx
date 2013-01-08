<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddVideo.aspx.cs" Inherits="Video_AddVideo"
    Title="Add/Edit Video" MasterPageFile="~/Shared/Story.master" ValidateRequest="false" %>

<asp:Content ID="content1" ContentPlaceHolderID="TabContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">

        $(document).addEvent('fb_connected', function() {
            $('facebook_share_container').setStyle('display', 'block');
            $('<%= facebook_share.ClientID %>').checked = true;
        });
    
    function hidesummery()
 {
     var lblErrMsg= document.getElementById('<%= lblErrMsg.ClientID %>'); 
     if(lblErrMsg)
     {
       lblErrMsg.innerHTML = '';
       lblErrMsg.style.visibility = 'hidden';
     }
 
 }
        function ValidateUrlAndId(source, args)
        {
            var txtVideoUrl = $('ctl00_ModuleContentPlaceHolder_txtVideoUrl').value;
            var txtVideoId = $('ctl00_ModuleContentPlaceHolder_txtVideoId').value;
            
            if ((txtVideoUrl =="") && (txtVideoId ==""))
            {
              args.IsValid = false;
              if ($('ctl00_ModuleContentPlaceHolder_lblErrMsg') != null)
                $('ctl00_ModuleContentPlaceHolder_lblErrMsg').style.visibility = "hidden";
            
            }
            else
                args.IsValid = true;
        }
        
        //to restrict the value of Video Descritpion to 1000 characters
        function maxLength()
        {
            var txtVal = $('ctl00_ModuleContentPlaceHolder_txtVideoDesc').value;
            return chkForMaxLength(1000, txtVal.length);
        }
        //this is for checking the length after clicking on post button
        function maxLength2(source, args)
        {
            var txtVal = $('ctl00_ModuleContentPlaceHolder_txtVideoDesc').value;
            args.IsValid = chkForMaxLength(1000, txtVal.length);
        }
        
        function isNumberKey(evt)
        {
            var charCode = (evt.which) ? evt.which : event.keyCode;       
            var txtVal = $('ctl00_ModuleContentPlaceHolder_txtVideoId').value;             
            var chkLength = txtVal.indexOf('.');
            chkLength = chkLength + 3;        
            if (txtVal.length > 2 && txtVal.indexOf('.') > 0 && txtVal.length == chkLength)
            {
                return false;
            }
            else
            {
                if (charCode > 31 && (charCode < 48 || charCode > 57))
                {
                    if (charCode == 46 && txtVal.length > 0 && txtVal.indexOf('.') == -1)
                        return true;        
                        
                    return false;
                }
                return true;
            }       
        }
    </script>

    <div class="yt-Breadcrumbs">
        <a id="aTributeHome" runat="server"><%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> Home</a> <a href="videos.aspx">Videos</a>
        <span id="spPageMode" runat="server" class="selected"></span>
    </div>
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ModuleContentPlaceHolder" runat="Server">
    <div id="divShowModalPopup">
    </div>
    <div class="yt-ContentPrimary">
        <div id="lblErrMsg" align="left" runat="server" class="yt-Error" visible="false">
        </div>
        <asp:ValidationSummary ID="vsErrorSummary" runat="server" CssClass="yt-Error" HeaderText=" <h2>Oops - there was a problem with your video.</h2></br><h3>Please correct the error(s) below:</h3>"
            ForeColor="Black" />
        <div class="yt-Panel-Primary">
            <h2 id="hAddVideo" runat="server">
            </h2>
            <strong id="stgRequired" runat="server"></strong>
            <fieldset class="yt-Form">
                <div class="yt-Form-Field">
                    <label id="lblVideoName" runat="server">
                    </label>
                    <asp:TextBox ID="txtVideoName" runat="server" CssClass="yt-Form-Input-Long" Width="337px"
                        MaxLength="100" TabIndex="1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvVideoName" runat="server" ControlToValidate="txtVideoName"
                        Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000">!</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revVideoNameSpecialchar" Font-Bold="True" Font-Size="Medium"
                        ForeColor="#FF8000" ControlToValidate="txtVideoName" ValidationExpression='^[a-zA-Z0-9\s\?\"\!\-\@\.\:\;\=\+\[\]_\,\%\(\)\/\&]+$'
                        runat="server">!</asp:RegularExpressionValidator>
                </div>
                <div class="yt-Form-Field">
                    <label id="lblVideoDescription" runat="server">
                    </label>
                    <asp:TextBox ID="txtVideoDesc" runat="server" CssClass="yt-Form-Textarea-XLong" Columns="50"
                        Rows="6" onkeypress="return maxLength();" MaxLength="1000" TextMode="MultiLine"
                        TabIndex="2"></asp:TextBox>
                    <asp:CustomValidator ID="cvVideoDesc" runat="server" ClientValidationFunction="maxLength2"
                        Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000">!</asp:CustomValidator>
                    <asp:RegularExpressionValidator ID="revVideoDescSpecialchar" Font-Bold="True" Font-Size="Medium"
                        ForeColor="#FF8000" ControlToValidate="txtVideoDesc" ValidationExpression='^[a-zA-Z0-9\s\?\"\!\-\@\.\:\;\=\+\[\]_\,\%\(\)\/\&]+$'
                        runat="server">!</asp:RegularExpressionValidator>
                </div>
                <h4 id="hAdd" runat="server">
                </h4>
                <ul id="ulAddVideo" runat="server">
                    <li id="liText1" runat="server"></li>
                    <li id="liText2" runat="server"></li>
                    <li id="liText3" runat="server"></li>
                    <li id="liText4" runat="server"></li>
                </ul>
                <div id="divVideoUrl" runat="server" class="yt-Form-Field">
                    <label id="lblVideoUrl" runat="server">
                    </label>
                    <asp:TextBox ID="txtVideoUrl" runat="server" CssClass="yt-Form-Input-Long" MaxLength="100"
                        TabIndex="3"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="reValVideoUrl" runat="server" ControlToValidate="txtVideoUrl"
                        Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ValidationExpression="http://(www\.)?youtube\.com\/watch\?v\=[a-zA-Z\d_-]+$">!</asp:RegularExpressionValidator>
                </div>
                <div id="instructions" runat="server">
                    The Video URL must start with "http://www."
                    <ul id="ul1">
                        <li id="li1">Incorrect: http://ca.youtube.com/watch?v=a1BcD2ef3gh </li>
                        <li id="li2">Incorrect: www.youtube.com/watch?v=a1BcD2ef3gh </li>
                        <li id="li3"><span style="color: Green; font-weight: bold">Correct: http://www.youtube.com/watch?v=a1BcD2ef3gh
                        </span></li>
                    </ul>
                    <br />
                    The Video URL must also end with the Video ID (i.e. "a1BcD2ef3gh")
                    <ul id="ul2">
                        <li id="li4">Incorrect: http://www.youtube.com/watch?v=a1BcD2ef3gh&feature=bz302
                        </li>
                        <li id="li5">Incorrect: http://www.youtube.com/watch?v=a1BcD2ef3gh&feature=user
                        </li>
                        <li id="li6"><span style="color: Green; font-weight: bold">Correct: http://www.youtube.com/watch?v=a1BcD2ef3gh</span>
                        </li>
                    </ul>
                </div>
                <strong id="stgOr" runat="server"></strong>
                <div id="divVideoId" runat="server" class="yt-Form-Field">
                    <label id="lblVideoId" runat="server">
                    </label>
                    <asp:TextBox ID="txtVideoId" runat="server" CssClass="yt-Form-Input" MaxLength="100"
                        TabIndex="4"></asp:TextBox>
                    <asp:CustomValidator ID="cvYouTubeVideo" runat="server" ClientValidationFunction="ValidateUrlAndId"
                        Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000">!</asp:CustomValidator>
                    <asp:RegularExpressionValidator ID="revVideoIdSpecialchar" Font-Bold="True" Font-Size="Medium"
                        ForeColor="#FF8000" ControlToValidate="txtVideoId" ValidationExpression='^[a-zA-Z0-9\s\?\"\!\-\@\.\:\;\=\+\[\]_\,\%\(\)\/\&]+$'
                        runat="server">!</asp:RegularExpressionValidator>
                </div>
                <div id="idinstructions" runat="server">
                    The Video ID is alphanumeric and comes after "watch?v=".
                    <br />
                    The Video ID is highlighted in green in this sample URL: http://www.youtube.com/watch?v=<span
                        style="color: Green; font-weight: bold">a1BcD2ef3gh</span>
                </div>
                <div class="yt-Form-Buttons">
                    <div class="yt-Form-Cancel">
                        <asp:LinkButton ID="lbtnCancel" runat="server" CausesValidation="false" OnClick="lbtnCancel_Click"
                            TabIndex="6" /></div>
                    <div class="yt-Form-Submit">
                        <asp:LinkButton ID="lbtnAddVideo" runat="server" OnClick="lbtnAddVideo_Click" CssClass="yt-Button yt-ArrowButton"
                            TabIndex="5" /></div>
                    <div class="yt-Form-Submit">
                        <asp:LinkButton ID="lbtnDeleteVideo" runat="server" CssClass="yt-Button yt-ArrowButton"
                            OnClick="lbtnDeleteVideo_Click" TabIndex="7" /></div>
                    <div id="facebook_share_container" style="float: right; margin-top: 7px; display: none">
                        <asp:CheckBox ID="facebook_share" runat="server" Checked="false" Text="Share on Facebook" /></div>
                </div>
            </fieldset>
        </div>
    </div>
    <!--yt-ContentPrimary-->
</asp:Content>
