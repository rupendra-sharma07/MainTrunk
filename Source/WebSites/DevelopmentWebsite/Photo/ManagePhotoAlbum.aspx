<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManagePhotoAlbum.aspx.cs"
    Inherits="Photo_ManagePhotoAlbum" Title="ManagePhotoAlbum" MasterPageFile="~/Shared/Story.master" %>

<asp:Content ID="content1" ContentPlaceHolderID="TabContentPlaceHolder" runat="Server">
    <%--    <script src="../assets/scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

    <script src="../assets/scripts/jquery-ui-1.8.6.custom.min.js" type="text/javascript"></script>--%>

    <script src="../assets/scripts/ExecuteBeforeLoad.js" type="text/javascript"></script>

    <script type="text/javascript" src="../MultiPowAssests/swfobject.js"></script>

    <script language="javascript" type="text/javascript">

        function validateData() {

            var errorMessage = " <h2>Oops - there is a problem in photo album.</h2><br/><h3>Please correct the error(s) below:</h3>";
            var valAlbumId = $('ctl00_ModuleContentPlaceHolder_hdnAlbumId').value;
            var valPhotoCount = $('ctl00_ModuleContentPlaceHolder_hdnPhotoCount').value;


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
        
    </script>

    <script type="text/javascript" language="javascript">

        function onBeforeUpload() {
            var pageurl = document.location.href;
            if (pageurl.indexOf('#') >= 0) {
                pageurl = pageurl.replace("#1", "");
            }
            var albumName;
            var albumDesc;
            var result = 0;
            var fileCount = 0;

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
            return true;
        }

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

        // function to add value of album name and album desc textbox to session by UD
        function setSessionAlbum() {
            if (document.getElementById('ctl00_ModuleContentPlaceHolder_txtAlbumName') != null && document.getElementById('ctl00_ModuleContentPlaceHolder_txtAlbumDesc')) {
                albumName = document.getElementById('ctl00_ModuleContentPlaceHolder_txtAlbumName').value;
                albumDesc = document.getElementById('ctl00_ModuleContentPlaceHolder_txtAlbumDesc').value;


                Photo_ManagePhotoAlbum.SetAlbumNameDesc(albumName, albumDesc);
            }
        }
       
    </script>

    <div class="yt-Breadcrumbs" id="nvgCreateAlbum" runat="server">
        <a id="aTributeHome" runat="server">
            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
            Home</a> <a href="photos.aspx">Photos</a> <span class="selected" style="font-family: Times New Roman Courier New">
                Create Album</span>
    </div>
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ModuleContentPlaceHolder" runat="Server">
    <asp:HiddenField ID="hdnImageNames" runat="server" />
    <div style="" id="divShowModalPopup">
    </div>
    <div class="yt-ContentPrimary">
        <div id="divNavigation" runat="server">
        </div>
        <input id="hdnAlbumName" name="hdnAlbumName" type="hidden" />
        <asp:HiddenField ID="hdnAlbumList" runat="server" />
        <asp:HiddenField ID="hdnResult" runat="server" />
        <span id="spnErrDuplicate" runat="server"></span><a name="1"></a>
        <div id="lblErrMsg" runat="server" class="yt-Error" style="display: none; font-size: 12px;
            text-align: left;">
        </div>
        <div class="yt-Panel-Primary" style="width: 852px;">
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
                                    TabIndex="1" Width="846px"></asp:TextBox>
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
                                <li id="liInstruction3" runat="server"></li>
                            </ul>
                            <h4 id="hHighResolPhotos" runat="server">
                                <asp:CheckBox ID="chbxhighResPhoto" runat="server" />
                                Upload High-Resolution Photos
                            </h4>
                            <ul class="yt-Instructions">
                                <li id="li1" runat="server">High Resolution Photos can only be viewed on paid accounts.
                                    Only select this option if you have a premium account or plan to upgrade.</li>
                                <li id="li2" runat="server">Uploading high-resolution photos will significantly increase
                                    the time taken to upload the photos.</li>
                            </ul>
                            <br />
                            <p>
                                1. Click "Add Files..."to select your photos, then click "Upload". You can
                                then add or remove photos as necessary.
                            </p>
                            <p id="secondLine" runat="server">
                                2. After you have finished uploading your photos, click "Create photo album" at
                                the bottom of the page to create the album.</p>
                            <br />
                            <div id="multiPowUpload">
                                <div>
                                   <!-- text below wil be shown if JavaScript disabled at browser -->
                                    <span id="noscriptdiv" style="border: 1px  solid #FF0000; display: block; padding: 5px;
                                        text-align: left; background: #FDF2F2; color: #000;">Active Scripting (JavaScript)
                                        should be enabled in your browser for this application to function properly!</span>

                                    <script type="text/javascript">
                                        document.getElementById('noscriptdiv').style.visibility = 'hidden';
                                        document.getElementById('noscriptdiv').style.height = 0;
                                        document.getElementById('noscriptdiv').style.padding = 0;
                                        document.getElementById('noscriptdiv').style.border = 0;
                                    </script>
                                    <br>
                                    <br>
                                    <div id="MultiPowUpload_holder">
                                        <strong>You need at least 10 version of Flash player!</strong> <a href="http://www.adobe.com/go/getflashplayer">
                                            &nbsp;<img border="0" src="../MultiPowAssests/CMS_plugins/Wordpress-plugin/multipowupload/get_flash_player.gif"
                                                alt="Get Adobe Flash player" /></a>
                                    </div>
                                    <script type="text/javascript" src="../MultiPowAssests/swfobject.js"></script>
                                    <script type="text/javascript">
                                        var params = {
                                            BGColor: "#FFFFFF"
                                        };

                                        var attributes = {
                                            id: "MultiPowUpload",
                                            name: "MultiPowUpload"
                                        };

                                        // get count of uploaded photos 
                                        if (document.getElementById('<% =hdnPhotoCount.ClientID%>') != null) {
                                            hdnFilecount = document.getElementById('<% =hdnPhotoCount.ClientID%>').value;
                                        }
                                        var total = 60;
                                        total = total - hdnFilecount;
                                        
                                        var flashvars = {
                                            "language.autoDetect": "true",
                                            "serialNumber": "",
                                            "uploadUrl": "../Photo/uploadfiles.aspx?stayHere=true",
                                            "fileFilter.types": "Images|*.jpg:*.jpeg:*.gif:*.png:*.bmp",
                                            "fileFilter.maxCount": total,
                                            "messages.filesCountExceeded": "Number of photos in album exceeding its maximum limit of 60 photos.",
                                            "fileFilter.maxSize": "3145728",
                                            "sendThumbnails": "true",
                                            "sendOriginalImages": "false",
                                            "uploadButton.action": "2",
                                            "fileView.defaultView": "thumbnails",
                                            "useExternalInterface": "true",
                                            "thumbnail.width": "100",
                                            "thumbnail.height": "100",
                                            "thumbnail.resizeMode": "fit",
                                            "thumbnail.format": "JPG",
                                            "thumbnail.jpgQuality": "85",
                                            "thumbnail.backgroundColor": "#000000",
                                            "thumbnail.transparentBackground": "true",
                                            "thumbnail.autoRotate": "true",
                                            "readImageMetadata": "true",
                                            "thumbnailView.allowRotate": "true",
                                            "thumbnailView.allowCrop": "true",
                                            //"thumbnailView.topPanel.inputTextBox.visible": "true", 
                                            "thumbnailView.bottomPanel.showEditIcon": "true",
                                            "listView.description.visible": "true",
                                            "progressBar.visible": "true",
                                            "statusLabel.visible": "true"

                                        };

                                        swfobject.embedSWF("../MultiPowAssests/ElementITMultiPowUpload.swf", "MultiPowUpload_holder", "600", "450", "10.0.0", "../MultiPowAssests/expressInstall.swf", flashvars, params, attributes);
                                    </script>

                                    <script type="text/javascript">
                                        var path_to_file = "";
                                        var cancelled = false;
                                        var currentCycle = 0;
                                        var sizes = new Array();
                                        var prefixes;

                                        function MultiPowUpload_onButtonClick(buttonName) {
                                            switch (buttonName) {
                                                case "uploadButton":
                                                    {
                                                        if (onBeforeUpload()) {
                                                            MultiPowUpload.uploadAll();
                                                        }
                                                        break;
                                                    }
                                            }
                                        }

                                        function MultiPowUpload_onStart() {
                                            cancelled = false;
                                            // High Resolution Photos
                                            var chbx = document.getElementById("ctl00_ModuleContentPlaceHolder_chbxhighResPhoto");
                                            //dimensions for different thumbnails
                                            sizes[0] = new Array(100, 100);
                                            sizes[1] = new Array(600, 480);
                                            if (chbx.checked == true) {
                                                sizes[2] = new Array(1024, 768);
                                                //prefixes for different thumbnails
                                                prefixes = new Array("thumbnail_", "DSC_", "Big_");
                                            }
                                            else {
                                                prefixes = new Array("thumbnail_", "DSC_");
                                            }
                                            setThumbailDimensions(currentCycle);
                                        }

                                        function MultiPowUpload_onCancel() {
                                            cancelled = true;
                                        }

                                        function MultiPowUpload_onMovieLoad() {
                                            //set initial thumbnails dimensions
                                            //setThumbailDimensions(currentCycle);
                                        }

                                        function setThumbailDimensions(index) {
                                            if (sizes.length > index) {
                                                MultiPowUpload.setParameter("thumbnail.height", sizes[index][0]);
                                                MultiPowUpload.setParameter("thumbnail.width", sizes[index][1]);
                                                MultiPowUpload.setParameter("thumbnail.fileName", prefixes[index] + "<FILENAME>");
                                            }
                                        }
                                        function MultiPowUpload_onComplete() {
                                            //upload finished let's check what we shpould do now
                                            currentCycle++;
                                            setThumbailDimensions(currentCycle);
                                            if (sizes.length > currentCycle) {
                                                if (!cancelled)
                                                    MultiPowUpload.uploadAll();
                                            }
                                            else {
                                                currentCycle = 0;
                                                setThumbailDimensions(currentCycle);
                                            }
                                        }

                                        function MultiPowUpload_onThumbnailUploadComplete(li, response) {
                                            //after first cycle reset crop rect
                                            /*if(currentCycle == 0)
                                            MultiPowUpload.resetImageCrop(li.id);*/

                                            //get current file processing script and combine path to file
                                            path_to_file = MultiPowUpload.getParameter("uploadUrl");
                                            path_to_file = path_to_file.substring(0, path_to_file.lastIndexOf("/") + 1) + "UploadedFiles/";

                                            //Here we need parse server response
                                            //and find url to uploaded thumbnails	

                                            var keyword = 'File ';
                                            var keywor_end = " was successfully uploaded";
                                            var ind = response.indexOf(keyword, 0);
                                            while (ind >= 0) {
                                                url = response.substring(ind + keyword.length, response.indexOf(keywor_end, ind));
                                                //addThumbnail(url);
                                                ind = response.indexOf(keyword, ind + keyword.length + 1);
                                            }
                                        }
                                        function addThumbnail(source) {
                                            var Img = document.createElement("img");
                                            Img.style.margin = "5px";
                                            Img.src = path_to_file + source + "?" + (new Date()).getTime();
                                            document.getElementById("thumbnails").appendChild(Img);
                                        }
                                    </script>

                                </div>
                            </div>
                            <div class="YT-Form-Error">
                                <span id="spnUploadPhoto" runat="server" style="color: #FF8000; font-size: Medium;
                                    font-weight: bold; display: none;">!</span>
                            </div>
                            <br style="clear: both;" />
                            <!-- please note that for "Add Photos" and "Add All Photos" buttons in Aurigma tool, we will restyle with yt-MiniButton style or similar effect -->
                        </div>
                        <div class="yt-Form-Buttons">
                            <div class="yt-Form-Cancel">
                                <asp:LinkButton ID="lbtnCancel" runat="server" CausesValidation="false" OnClick="lbtnCancel_Click"
                                    TabIndex="4" />
                            </div>
                            <div style="margin-right: 15px;">
                                <!-- If page is "Edit Album": -->
                                <div class="yt-Form-Submit">
                                    <asp:LinkButton ID="lbtnSavePhoto" runat="server" OnClientClick="return onBeforeUpload();"
                                        OnClick="lbtnCreateAlbum_Click" CausesValidation="true" CssClass="yt-Button yt-ArrowButton"
                                        TabIndex="3" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </fieldset>
        </div>
    </div>
</asp:Content>
