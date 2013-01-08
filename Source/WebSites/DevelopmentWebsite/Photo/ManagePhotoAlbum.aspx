<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManagePhotoAlbum.aspx.cs"
    Inherits="Photo_ManagePhotoAlbum" Title="ManagePhotoAlbum" MasterPageFile="~/Shared/Story.master" %>

<%@ Register Assembly="System.Web.Extensions" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="Aurigma.ImageUploader" Namespace="Aurigma.ImageUploader" TagPrefix="cc1" %>
<asp:Content ID="content1" ContentPlaceHolderID="TabContentPlaceHolder" runat="Server">
    <%--  <script src="~/assets/scripts/multipledescriptions.js" type="text/javascript"></script>--%>

    <script src="../assets/scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

    <script src="../assets/scripts/jquery-ui-1.8.6.custom.min.js" type="text/javascript"></script>

    <script src="../assets/scripts/ExecuteBeforeLoad.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">

        //to restrict the value of Video Descritpion to 1000 characters
        function DescmaxLength() {
            var txtId = document.getElementById('<%= txtAlbumDesc.ClientID %>');
            if (txtId != null) {
                var txtVal = txtId.value;
                if (txtVal != "" && txtVal.length > 0)
                    return chkForMaxLength(1000, txtVal.length);
                else return 0;
            }
        }

        function maxLength() {
            var txtId = document.getElementById('<%= txtAlbumDesc.ClientID %>');
            if (txtId != null) {
                var txtVal = txtId.value;
                if (txtVal != "" && txtVal.length > 0)
                    return chkForMaxLength(1000, txtVal.length);
                else return 0;
            }
        }

        function validateData() {

            var errorMessage = " <h2>Oops - there is a problem in photo album.</h2><br/><h3>Please correct the error(s) below:</h3>";
            var valAlbumId = $('ctl00_ModuleContentPlaceHolder_hdnAlbumId').value;
            var valPhotoCount = $('ctl00_ModuleContentPlaceHolder_hdnPhotoCount').value;

            var error = "";
            if (!isEmpty(valAlbumId)) {
                var valAlbumName = $('ctl00_ModuleContentPlaceHolder_txtAlbumName').value;
                var valAlbumDesc = $('ctl00_ModuleContentPlaceHolder_txtAlbumDesc').value;
                var spanAlbumName = document.getElementById('spnAlbumName');
                var spanAlbumDesc = document.getElementById('spnAlbumDesc');
                var spanUploadPhoto = document.getElementById('spnUploadPhoto');

                if (!isEmpty(valAlbumName)) {
                    error = "error";
                    errorMessage += "<ul>";
                    errorMessage += "<li>";
                    errorMessage += "Album name is required field."
                    errorMessage += "</li>";
                    errorMessage += "</ul>";
                    //alert("Test1 " + spnAlbumName.style.visibility);
                    spanAlbumName.style.visibility = "visible";

                }
                else {
                    //alert("Test2 " + spnAlbumName.style.visibility);
                    spanAlbumName.style.visibility = "hidden";
                }

                //to check for duplicate album name.
                var txtName = $('ctl00_ModuleContentPlaceHolder_txtAlbumName').value;
                if (isEmpty(txtName)) {
                    var txtAlbumList = $('ctl00_ModuleContentPlaceHolder_hdnAlbumList').value;
                    if (!isUnique(txtName, txtAlbumList)) {
                        error = "error";
                        errorMessage += "<ul>";
                        errorMessage += "<li>";
                        errorMessage += "Album name already exists."
                        errorMessage += "</li>";
                        errorMessage += "</ul>";
                        if (spanAlbumName.style.visibility != "visible")
                            spanAlbumName.style.visibility = "visible";
                    }
                }

                //to check album descritpion length.
                if (!chkForMaxLength(1000, valAlbumDesc.length)) {
                    error = "error";
                    errorMessage += "<ul>";
                    errorMessage += "<li>";
                    errorMessage += "Album description is exceeding it's maximum limit of 1000 characters."
                    errorMessage += "</li>";
                    errorMessage += "</ul>";
                    spanAlbumDesc.style.visibility = "visible";
                }
                else {
                    spanAlbumDesc.style.visibility = "hidden";
                }
            }

            if (isEmpty(valAlbumId)) {
                var spanUploadPhoto = document.getElementById('spnUploadPhoto');
            }

            //to check if number of photos is greater than 60 for the selected album
            var UploadPane = document.getElementById("UploadPane");
            var totalPhotos = parseInt(valPhotoCount) + parseInt(UploadPane.childNodes.length);
            if (totalPhotos > 60) {
                error = "error";
                errorMessage += "<ul>";
                errorMessage += "<li>";
                errorMessage += "Number of photos in album exceeding its maximum limit of 60 photos."
                errorMessage += "</li>";
                errorMessage += "</ul>";
                if (spanUploadPhoto.style.visibility != "visible")
                    spanUploadPhoto.style.visibility = "visible";
            }
            else {
                spanUploadPhoto.style.visibility = "hidden";
            }

            //if no photo selected
            if (parseInt(UploadPane.childNodes.length) == 0) {
                error = "error";
                errorMessage += "<ul>";
                errorMessage += "<li>";
                errorMessage += "Please select a photo to upload."
                errorMessage += "</li>";
                errorMessage += "</ul>";
                if (spanUploadPhoto.style.visibility != "visible")
                    spanUploadPhoto.style.visibility = "visible"
            }

            //to display error messages of Upload control for photo title and description.
            var uploaderErrorMessages = ImageUploaderValidation(valPhotoCount);
            if (isEmpty(uploaderErrorMessages)) {
                error = "error";
                var messages = uploaderErrorMessages.split(';');
                for (var i = 0; i < messages.length; i++) {
                    var msg = messages[i];
                    errorMessage += "<ul>";
                    errorMessage += "<li>";
                    errorMessage += msg;
                    errorMessage += "</li>";
                    errorMessage += "</ul>";
                }
                if (spanUploadPhoto.style.visibility != "visible")
                    spanUploadPhoto.style.visibility = "visible";
            }

            //to display error messages
            if (error.length > 0) {
                lblErrMsg.innerHTML = errorMessage;
                lblErrMsg.style.visibility = "visible";
                //window.location.href ="ManagePhotoAlbum.aspx#lblErrMsg";
                return false;
            }
            else {
                lblErrMsg.innerHTML = "";
                lblErrMsg.style.visibility = "hidden";
                return true;
            }

        }

        // function to add value of album name and album desc textbox to session by UD
        function setSessionAlbum() {
            if (document.getElementById('ctl00_ModuleContentPlaceHolder_txtAlbumName') != null && document.getElementById('ctl00_ModuleContentPlaceHolder_txtAlbumDesc')) {
                albumName = document.getElementById('ctl00_ModuleContentPlaceHolder_txtAlbumName').value;
                albumDesc = document.getElementById('ctl00_ModuleContentPlaceHolder_txtAlbumDesc').value;


                Photo_ManagePhotoAlbum.SetAlbumNameDesc(albumName, albumDesc);
            }

        }
    </script>

    <script type="text/javascript" language="javascript">

        function onBeforeUpload() {
            var pageurl = document.location.href;
            if (pageurl.indexOf('#') >= 0) {
                pageurl = pageurl.replace("#1", "");
            }
            var uploader = $au.uploader(uploaderID);
            var f = uploader.files(),
				fileCount = f.count(),
				guids = {},
				i;
            var albumName;
            var albumDesc;
            var result = 0;

            //-- Check validations of album by Ashu
            if (document.getElementById('ctl00_ModuleContentPlaceHolder_txtAlbumName') != null && document.getElementById('ctl00_ModuleContentPlaceHolder_txtAlbumDesc') != null) {
                albumName = document.getElementById('ctl00_ModuleContentPlaceHolder_txtAlbumName').value.trim();
                albumDesc = document.getElementById('ctl00_ModuleContentPlaceHolder_txtAlbumDesc').value.trim();
                if (albumName == "") {
                    document.getElementById('ctl00_ModuleContentPlaceHolder_lblErrMsg').style.display = 'block';
                    document.getElementById('ctl00_ModuleContentPlaceHolder_lblErrMsg').innerHTML = "Please enter Album Name.";
                    document.getElementById('ctl00_ModuleContentPlaceHolder_spnAlbumName').style.display = 'block';
                    document.getElementById('ctl00_ModuleContentPlaceHolder_spnAlbumDesc').style.display = 'none';
                    document.getElementById('ctl00_ModuleContentPlaceHolder_spnUploadPhoto').style.display = 'none';
                    parent.location.href = pageurl + "#1";
                    return false;
                }
                else {
                    document.getElementById('ctl00_ModuleContentPlaceHolder_lblErrMsg').style.display = 'none';
                    document.getElementById('ctl00_ModuleContentPlaceHolder_lblErrMsg').innerHTML = "";
                    document.getElementById('ctl00_ModuleContentPlaceHolder_spnAlbumName').style.display = 'none';
                    result = Photo_ManagePhotoAlbum.CheckAlbumValidation(albumName, albumDesc, fileCount);
                }
            }
            else {
                var hdnFilecount;
                if (document.getElementById('<% =hdnPhotoCount.ClientID%>') != null) {
                    hdnFilecount = document.getElementById('<% =hdnPhotoCount.ClientID%>').value;
                }
                result = Photo_ManagePhotoAlbum.CheckAlbumFileCount(fileCount, hdnFilecount);
            }
            if (result.value < 0) {
                if (document.getElementById('ctl00_ModuleContentPlaceHolder_lblErrMsg') != null)
                    document.getElementById('ctl00_ModuleContentPlaceHolder_lblErrMsg').style.display = 'block';

                if (result.value == -1) {
                    document.getElementById('ctl00_ModuleContentPlaceHolder_lblErrMsg').innerHTML = "This album name already exists. Please provide another album name.";
                    document.getElementById('ctl00_ModuleContentPlaceHolder_spnAlbumName').style.display = 'block';
                    document.getElementById('ctl00_ModuleContentPlaceHolder_spnAlbumDesc').style.display = 'none';
                    document.getElementById('ctl00_ModuleContentPlaceHolder_spnUploadPhoto').style.display = 'none';
                    parent.location.href = pageurl + "#1";
                    return false;
                }
                //error in creation. empty album name
                if (result.value == -2) {
                    document.getElementById('ctl00_ModuleContentPlaceHolder_lblErrMsg').innerHTML = "Please provide a valid album name.";
                    document.getElementById('ctl00_ModuleContentPlaceHolder_spnAlbumName').style.display = 'block';
                    document.getElementById('ctl00_ModuleContentPlaceHolder_spnAlbumDesc').style.display = 'none';
                    document.getElementById('ctl00_ModuleContentPlaceHolder_spnUploadPhoto').style.display = 'none';
                    parent.location.href = pageurl + "#1";
                    return false;
                }
                //error in creation. album name length invalid
                if (result.value == -3) {
                    document.getElementById('ctl00_ModuleContentPlaceHolder_lblErrMsg').innerHTML = "Please provide a valid album name.";
                    document.getElementById('ctl00_ModuleContentPlaceHolder_spnAlbumName').style.display = 'block';
                    document.getElementById('ctl00_ModuleContentPlaceHolder_spnAlbumDesc').style.display = 'none';
                    document.getElementById('ctl00_ModuleContentPlaceHolder_spnUploadPhoto').style.display = 'none';
                    parent.location.href = pageurl + "#1";
                    return false;
                }
                //error in creation. album description length invalid
                if (result.value == -4) {
                    document.getElementById('ctl00_ModuleContentPlaceHolder_lblErrMsg').innerHTML = "Please provide a valid album description.";
                    document.getElementById('ctl00_ModuleContentPlaceHolder_spnAlbumDesc').style.display = 'block';
                    document.getElementById('ctl00_ModuleContentPlaceHolder_spnAlbumName').style.display = 'none';
                    document.getElementById('ctl00_ModuleContentPlaceHolder_spnUploadPhoto').style.display = 'none';
                    parent.location.href = pageurl + "#1";
                    return false;
                }
                //error in creation. no of photos > 60
                if (result.value == -5) {
                    document.getElementById('ctl00_ModuleContentPlaceHolder_lblErrMsg').innerHTML = "An album cannot contain more than 60 photos. Only up to 60 photos can be uploaded.";
                    document.getElementById('ctl00_ModuleContentPlaceHolder_spnUploadPhoto').style.display = 'block';
                    if (document.getElementById('ctl00_ModuleContentPlaceHolder_spnAlbumDesc') != null)
                        document.getElementById('ctl00_ModuleContentPlaceHolder_spnAlbumDesc').style.display = 'none';
                    if (document.getElementById('ctl00_ModuleContentPlaceHolder_spnAlbumName') != null)
                        document.getElementById('ctl00_ModuleContentPlaceHolder_spnAlbumName').style.display = 'none';
                    parent.location.href = pageurl + "#1";
                    return false;
                }
                return false;
            }
            else {

                if (document.getElementById('ctl00_ModuleContentPlaceHolder_lblErrMsg') != null) {
                    document.getElementById('ctl00_ModuleContentPlaceHolder_lblErrMsg').innerHTML = "";
                    document.getElementById('ctl00_ModuleContentPlaceHolder_lblErrMsg').style.display = 'none';
                }
                if (document.getElementById('ctl00_ModuleContentPlaceHolder_spnAlbumName') != null)
                    document.getElementById('ctl00_ModuleContentPlaceHolder_spnAlbumName').style.display = 'none';
                if (document.getElementById('ctl00_ModuleContentPlaceHolder_spnAlbumDesc') != null)
                    document.getElementById('ctl00_ModuleContentPlaceHolder_spnAlbumDesc').style.display = 'none';
                if (document.getElementById('ctl00_ModuleContentPlaceHolder_spnUploadPhoto') != null)
                    document.getElementById('ctl00_ModuleContentPlaceHolder_spnUploadPhoto').style.display = 'none';
            }
        }

        //        multidescription till here


        function AfterImageUpload() {

            if (window.location.href.indexOf("Type") > 0) {
                var TypeDesc = window.location.href.substring(window.location.href.indexOf("Type=") + 5);
                if (window.location.href.indexOf("albummode") > 0) {
                    window.location = "Photos.aspx?post_on_facebook=True&mode=Create&Type=" + TypeDesc;

                }
                else if ((window.location.href.indexOf("mode") > 0) && (window.location.href.indexOf("photoAlbumId") > 0)) {
                    var AlbumId = window.location.href.substring(window.location.href.indexOf("photoAlbumId=") + 13, window.location.href.indexOf("Type=") - 1);
                    window.location = "Photos.aspx?post_on_facebook=True&mode=AddPhotos&Type=" + TypeDesc + "&AlbumId=" + AlbumId;

                }
            }
        }      
        
    </script>

    <script type="text/javascript">

        var uploaderID = '<%= Uploader1.ClientID %>';


    </script>

    <div class="yt-Breadcrumbs" id="nvgCreateAlbum" runat="server">
        <a id="aTributeHome" runat="server">
            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
            Home</a> <a href="photos.aspx">Photos</a> <span class="selected" style="font-family: Times New Roman Courier New">
                Create Album</span>
    </div>
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ModuleContentPlaceHolder" runat="Server">
    <div style="" id="divShowModalPopup">
    </div>
    <div class="yt-ContentPrimary">
        <input id="hdnAlbumName" name="hdnAlbumName" type="hidden" />
        <asp:HiddenField ID="hdnAlbumList" runat="server" />
        <asp:HiddenField ID="hdnResult" runat="server" />
        <span id="spnErrDuplicate" runat="server"></span><a name="1"></a>
        <div id="lblErrMsg" runat="server" class="yt-Error" style="display: none; font-size: 12px;
            text-align: left;">
        </div>
        <div class="yt-Panel-Primary">
            <h2 id="hPhoto" runat="server">
            </h2>
            <strong id="stgRequired" runat="server"></strong>
            <fieldset class="yt-Form">
                <asp:HiddenField ID="hdnAlbumId" runat="server" />
                <asp:HiddenField ID="hdnPhotoCount" runat="server" />
                <asp:HiddenField ID="hdnPhotoUploaderKey" runat="server" />
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div id="divAlbumName" runat="server" class="yt-Form-Field">
                            <label id="lblAlbumName" runat="server">
                            </label>
                            <div class="yt-Form-AlbumNameDiv">
                                <asp:TextBox ID="txtAlbumName" runat="server" CssClass="yt-Form-Input-Long" Width="337px"
                                    onkeyup="javascript:setSessionAlbum();" MaxLength="100" Text="Photo Album"></asp:TextBox>
                            </div>
                            <div class="YT-Form-Error">
                                <span id="spnAlbumName" runat="server" style="color: #FF8000; font-size: Medium;
                                    font-weight: bold; display: none;">!</span>
                            </div>
                            <br style="clear: both;" />
                        </div>
                        <div id="divAlbumDesc" runat="server" class="yt-Form-Field">
                            <label id="lblAlbumDesc" runat="server">
                            </label>
                            <div class="yt-Form-AlbumDescDiv">
                                <asp:TextBox ID="txtAlbumDesc" runat="server" CssClass="yt-Form-Textarea-XLong" Columns="50"
                                    Rows="6" MaxLength="1000" TextMode="MultiLine" onkeyup="javascript:setSessionAlbum();"
                                    TabIndex="1"></asp:TextBox>
                            </div>
                            <div class="YT-Form-Error">
                                <span id="spnAlbumDesc" runat="server" style="color: #FF8000; font-size: Medium;
                                    font-weight: bold; display: none; width: 5px;">!</span>
                            </div>
                            <br style="clear: both;" />
                        </div>
                        <h4 id="hUploadPhoto" runat="server">
                        </h4>
                        <div class="yt-UploadTool">
                            <ul class="yt-Instructions">
                                <li id="liInstruction1" runat="server"></li>
                                <li id="liInstruction2" runat="server"></li>
                                <li id="liInstruction3" runat="server"></li>
                            </ul>
                            <!-- insert Aurigma Upload Plugin here (sample placeholder graphic below) -->
                            <!-- Ashu (7june,2011) : Add TreePane for leftpanel on safari -->
                            <%-- Add Cropping Functionality By Ashu(21 June,2011)--%>
                            <div id="UploadPaneFrame">
                                <div class="yt-Form-UploadDiv">
                                    <div id="UploadPane" class="yt-UploadTool">
                                        <cc1:Uploader ID="Uploader1" runat="server" OnFileUploaded="Uploader1_FileUploaded"
                                            Height="420" Width="630px" LicenseKey="<%$ AppSettings:ImageUploaderLicenseKey %>"
                                            EnableUploadPane="false" EnableDragAndDrop="false" EnableDescriptionEditor="true"
                                            EnableImageEditor="True" EnableOriginalFilesDeletion="False">
                                            <TreePane Width="210" />
                                            <Converters>
                                                <%--<cc1:Converter Mode="*.*=SourceFile" />--%>
                                                <cc1:Converter Mode="*.*=Thumbnail" ThumbnailWidth="640" ThumbnailHeight="480" ThumbnailApplyCrop="true" />
                                                <cc1:Converter Mode="*.*=Thumbnail" ThumbnailWidth="1946" ThumbnailHeight="1459"
                                                    ThumbnailApplyCrop="true" />
                                            </Converters>
                                            <ClientEvents>
                                                <cc1:ClientEvent EventName="BeforeUpload" HandlerName="onBeforeUpload" />
                                                <%-- <cc1:ClientEvent EventName="UploadFileCountChange" HandlerName="onUploadFileCountChange" />--%>
                                                <cc1:ClientEvent EventName="AfterUpload" HandlerName="AfterImageUpload" />
                                            </ClientEvents>
                                            <Restrictions FileMask="*.jpg;*.jpeg;*.png;*.gif" MaxFileCount="60" />
                                            <UploadSettings />
                                            <%--<UploadSettings ActionUrl="http://localhost:4941/DevelopmentWebsite/photo/ManagePhotoAlbum.aspx"   RedirectUrl="Photos.aspx?post_on_facebook=True" />--%>
                                        </cc1:Uploader>
                                        <cc1:InstallationProgress ID="InstallationProgress1" TargetControlID="Uploader1"
                                            runat="server" ProgressCssClass="ip-progress" InstructionsCssClass="ip-instructions">
                                        </cc1:InstallationProgress>
                                        <%-- <ul id="<%= Uploader1.ClientID %>_UploadPane" class="upload-pane" />--%>
                                    </div>
                                </div>
                                <div class="YT-Form-Error">
                                    <span id="spnUploadPhoto" runat="server" style="color: #FF8000; font-size: Medium;
                                        font-weight: bold; display: none;">!</span>
                                </div>
                                <br style="clear: both;" />
                            </div>
                            <!-- please note that for "Add Photos" and "Add All Photos" buttons in Aurigma tool, we will restyle with yt-MiniButton style or similar effect -->
                        </div>
                        <div class="yt-Form-Buttons">
                            <div class="yt-Form-Cancel">
                                <asp:LinkButton ID="lbtnCancel" runat="server" CausesValidation="false" OnClick="lbtnCancel_Click"
                                    TabIndex="4" />
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </fieldset>
        </div>
    </div>
</asp:Content>
