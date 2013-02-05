<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManagePhotoAlbum.aspx.cs"
    Inherits="Photo_ManagePhotoAlbum" Title="ManagePhotoAlbum" MasterPageFile="~/Shared/Story.master" %>

<asp:Content ID="content1" ContentPlaceHolderID="TabContentPlaceHolder" runat="Server">
    <%--    <script src="../assets/scripts/jquery-1.4.2.min.js" type="text/javascript"></script>

    <script src="../assets/scripts/jquery-ui-1.8.6.custom.min.js" type="text/javascript"></script>--%>

    <script src="../assets/scripts/ExecuteBeforeLoad.js" type="text/javascript"></script>

    <link rel="stylesheet" href="../BlueImpAssets/BlueImp/bootstrap.css">
    <link rel="stylesheet" href="../BlueImpAssets/BlueImp/jquery.css">

    <script language="javascript" type="text/javascript">
//        function handleFiles(files) {
//            if (!files.length) {
//                body.innerHTML = "<p>No files selected!</p>";
//            } else {
//                var list = document.createElement("ul");
//                for (var i = 0; i < files.length; i++) {
//                    var li = document.createElement("li");
//                    list.appendChild(li);

//                    var img = document.createElement("img");
//                    alert(files[i].toSource());
//                    img.src = window.URL.createObjectURL(files[i]);
//                    
//                    img.height = 60;
//                    img.onload = function(e) {
//                        window.URL.revokeObjectURL(this.src);
//                    }
//                    li.appendChild(img);

//                    var info = document.createElement("span");
//                    info.innerHTML = files[i].name + ": " + files[i].size + " bytes";
//                    li.appendChild(info);
//                }
//                body.appendChild(list);
//            }
//        }

//        function PreviewImage() {
//            oFReader = new FileReader();
//            oFReader.readAsDataURL(document.getElementById("uploadImage").files[0]);

//            oFReader.onload = function(oFREvent) {
//                document.getElementById("uploadPreview").src = oFREvent.target.result;
//            };
//        }
        
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
            //            var uploader = $au.uploader(uploaderID);
            //            var f = uploader.files(),
            //				fileCount = f.count(),
            //				guids = {},
            //				i;
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


            // Adding uploaded files in hidden fields

            var table = document.getElementById('ctl00_ModuleContentPlaceHolder_uploadFilesTable');
            var rows = table.getElementsByTagName('tr');
            var hiddenField = document.getElementById('ctl00_ModuleContentPlaceHolder_hdnImageNames');

            var imageFiles = "";
            for (r = 0; r < rows.length; r++) {
                if (rows[r].className == "template-download fade in") {
                    var cells = rows[r].getElementsByTagName('td');
                    for (c = 0; c < cells.length; c++) {
                        if (cells[c].className == "name") {
                            if (cells[c].innerHTML) {
                                imageFiles = imageFiles + cells[c].innerHTML + ",";
                            }
                        }
                    }
                }
            }
            hiddenField.value = imageFiles;
            if (hiddenField.value == "") {
                document.getElementById('ctl00_ModuleContentPlaceHolder_lblErrMsg').style.display = 'block';
                document.getElementById('ctl00_ModuleContentPlaceHolder_lblErrMsg').innerHTML = "Please select a photo to upload.";
                parent.location.href = pageurl + "#1";
                return false;
            }
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
    <form action="../BlueImpAssets/Handler.ashx" method="POST" enctype="multipart/form-data">
    <asp:HiddenField ID="hdnImageNames" runat="server" />
    <div style="" id="divShowModalPopup">
    </div>
    </form>
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
                                1. Click "Add Files..."to select your photos, then click "Start Upload". You can
                                then add or remove photos as necessary.
                            </p>
                            <p id="secondLine" runat="server">
                                2. After you have finished uploading your photos, click "Create photo album" at
                                the bottom of the page to create the album.</p>
                            <br />
                            <div id="fileupload">
                                <!-- The file upload form used as target for the file upload widget -->
                                <form action="../BlueImpAssets/Handler.ashx" method="POST" enctype="multipart/form-data">
                                <!-- Redirect browsers with JavaScript disabled to the origin page -->
                                <!-- The fileupload-buttonbar contains buttons to add/delete files and start/cancel the upload -->
                                <div class="row fileupload-buttonbar">
                                    <div class="span7">
                                        <!-- The fileinput-button span is used to style the file input field as button -->
                                        <span class="btn btn-success fileinput-button"><i class="icon-plus icon-white"></i><span>
                                            Add files...</span>
                                            <input name="files[]" multiple="" type="file" id="fileElementId" runat="server">
                                        </span>
                                        <button type="submit" class="btn btn-primary start">
                                            <i class="icon-upload icon-white"></i><span>Start upload</span>
                                        </button>
                                        <button type="reset" class="btn btn-warning cancel">
                                            <i class="icon-ban-circle icon-white"></i><span>Cancel upload</span>
                                        </button>
                                        <button type="button" class="btn btn-danger delete">
                                            <i class="icon-trash icon-white"></i><span>Delete</span>
                                        </button>
                                        <input class="toggle" type="checkbox">
                                    </div>
                                    <!-- The global progress information -->
                                    <div class="span5 fileupload-progress fade">
                                        <!-- The global progress bar -->
                                        <div class="progress progress-success progress-striped active" role="progressbar"
                                            aria-valuemin="0" aria-valuemax="100">
                                            <div class="bar" style="width: 0%;">
                                            </div>
                                        </div>
                                        <!-- The extended global progress information -->
                                        <div class="progress-extended">
                                            &nbsp;</div>
                                    </div>
                                </div>
                                <!-- The loading indicator is shown during file processing -->
                                <div class="fileupload-loading">
                                </div>
                                <br>
                                <!-- The table listing the files available for upload/download -->
                                <table role="presentation" class="table table-striped">
                                    <tbody class="files" data-toggle="modal-gallery" data-target="#modal-gallery" id="uploadFilesTable"
                                        runat="server">
                                    </tbody>
                                </table>
                                </form>
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

    <script id="template-upload" type="text/x-tmpl">
{% for (var i=0, file; file=o.files[i]; i++) { %}
    <tr class="template-upload fade">
        <td class="preview"><span class="fade"></span></td>
        <td class="name"><span>{%=file.name%}</span></td>
        <td class="size"><span>{%=o.formatFileSize(file.size)%}</span></td>
        {% if (file.error) { %}
            <td class="error" colspan="2"><span class="label label-important">Error</span> {%=file.error%}</td>
        {% } else if (o.files.valid && !i) { %}
            <td>
                <div class="progress progress-success progress-striped active" role="progressbar" aria-valuemin="0" aria-valuemax="100" aria-valuenow="0"><div class="bar" style="width:0%;"></div></div>
            </td>
            <td class="start" style="display:none">{% if (!o.options.autoUpload) { %}
                <button class="btn btn-primary">
                    <i class="icon-upload icon-white"></i>
                    <span>Start</span>
                </button>
            {% } %}</td>
        {% } else { %}
            <td colspan="2"></td>
        {% } %}
        <td class="cancel">{% if (!i) { %}
            <button class="btn btn-warning">
                <i class="icon-ban-circle icon-white"></i>
                <span>Cancel</span>
            </button>
        {% } %}</td>
    </tr>
{% } %}
    </script>

    <!-- The template to display files available for download -->

    <script id="template-download" type="text/x-tmpl">
{% for (var i=0, file; file=o.files[i]; i++) { %}
    <tr class="template-download fade">
        {% if (file.Error) { %}
            <td></td>
            <td class="name"><span>{%=file.Name%}</span></td>
            <td class="size"><span>{%=o.formatFileSize(file.fileSize)%}</span></td>
            <td class="error" colspan="2"><span class="label label-important">Error</span> {%=file.Error%}</td>
        {% } else { %}
            <td class="preview">{% if (file.ThumbnailUrl) { %}
                <a href="{%=file.Url%}" target="_blank" title="{%=file.Name%}" data-gallery="gallery" download="{%=file.Name%}"><img src="{%=file.ThumbnailUrl%}" width="80" height="50" ></a>
            {% } %}</td>
            <td class="name">
                <a href="{%=file.Url%}" target="_blank" title="{%=file.Name%}" data-gallery="{%=file.ThumbnailUrl&&'gallery'%}" download="{%=file.Name%}">{%=file.Name%}</a>
            </td>
            <td class="size"><span>{%=o.formatFileSize(file.Size)%}</span></td>
            <td colspan="2"></td>
        {% } %}
        <td class="delete">
            <button class="btn btn-danger" data-type="{%=file.delete_type%}" data-url="{%=file.delete_url%}"{% if (file.delete_with_credentials) { %} data-xhr-fields='{"withCredentials":true}'{% } %}>
                <i class="icon-trash icon-white"></i>
                <span>Delete</span>
            </button>
            <input type="checkbox" name="delete" value="1">
        </td>
    </tr>
{% } %}
    </script>

    <script src="../BlueImpAssets/BlueImp/jquery_003.js"></script>

    <script src="../BlueImpAssets/BlueImp/jquery_006.js"></script>

    <script src="../BlueImpAssets/BlueImp/tmpl.js"></script>

    <script src="../BlueImpAssets/BlueImp/load-image.js"></script>

    <script src="../BlueImpAssets/BlueImp/jquery_004.js"></script>

    <script src="../BlueImpAssets/BlueImp/jquery.js"></script>

    <script src="../BlueImpAssets/BlueImp/main.js"></script>

</asp:Content>
