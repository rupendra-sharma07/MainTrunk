var imageUploader1 = null;
var uniqueId = 0;
var prevUploadFileCount = 0;
var dragAndDropEnabled = true;


var allowDrag = false;

function fullPageLoad(){
	imageUploader1 = getImageUploader("ImageUploader1");

	var UploadPane=document.getElementById("UploadPane");
	
	while (UploadPane.childNodes.length > 0){
		UploadPane.removeChild(UploadPane.childNodes[0]);
	}

	//Fix Opera applet z-order bug
	if (__browser.isOpera){
		UploadPane.style.height = "auto";
		UploadPane.style.overflow = "visible";
	}


	//Handle drag & drop.
	if (__browser.isIE || __browser.isSafari){
		var target = __browser.isIE ? UploadPane : document.body;
		target.ondragenter = function(){
			var e=window.event;
			var data = e.dataTransfer;
			if (data.getData('Text')==null){
				this.ondragover();
				data.dropEffect="copy";
				allowDrag=true;
			}
			else{
				allowDrag=false;
			}
		}
		
		target.ondragover=function(){
			var e = window.event;
			e.returnValue = !allowDrag;
		}

		target.ondrop = function(){
			var e = window.event;
			this.ondragover();
			e.dataTransfer.dropEffect = "none";
			processDragDrop();
		}
	}
	else {
		window.captureEvents(Event.DRAGDROP);
		window.addEventListener("dragdrop", function(){
				processDragDrop();
			}, true);
	}

}

function processDragDrop(){
	alert("Adding files with drag & drop can not be implemented in standard version due security reasons. However it can be enabled in private-label version."+
		"\r\n\r\nFor more information please contact us at sales@aurigma.com");
	if (imageUploader1){
		//imageUploader1.AddToUploadList();
	}
}

//To identify items in upload list, GUID are used. However it would work 
//too slow if we use GUIDs directly. To increase performance, we will use 
//hash table which will map the guid to the index in upload list. 

//This function builds and returns the hash table which will be used for
//fast item search.
function getGuidIndexHash(){
	var uploadFileCount = imageUploader1.getUploadFileCount();
	var guidIndexHash = new Object();
	for (var i = 1; i <= uploadFileCount; i++){
		guidIndexHash["" + imageUploader1.getUploadFileGuid(i)] = i;
	}
	return guidIndexHash;
}

//This function returns HTML which represent the single item in the custom upload pane.
//It contains of the Thumbnail object and form elements for each piece of data (in our 
//case - title and description). If you want to upload extra data, you should write
//additional form elements here.
//
//It is highly recommended not to copy this function into the main HTML page to 
//avoid problems with activation of ActiveX controls in Internet Explorer with
//security update 912945. You can read more detailed about activation on Microsoft website:
//
//http://msdn.microsoft.com/library/default.asp?url=/workshop/author/dhtml/overview/activating_activex.asp 
function addUploadFileHtml(index){
	var guid = "" + imageUploader1.getUploadFileGuid(index);
	var fileName = "" + imageUploader1.getUploadFileName(index);

	var h = "<table cellspacing=\"5\"><tbody>";
	h += "<tr>";
	h += "<td class=\"Thumbnail\" align=\"center\" valign=\"middle\">";

	//Add thumbnail control and link it with Image Uploader by its name and GUID.
	var tn = new ThumbnailWriter("Thumbnail" + uniqueId, 96, 96);
	//Copy codebase and version settings from ImageUploaderWriter instance.
	tn.activeXControlCodeBase = iu.activeXControlCodeBase;
	tn.activeXControlVersion = iu.activeXControlVersion;
	tn.javaAppletCodeBase = iu.javaAppletCodeBase;
	tn.javaAppletCached = iu.javaAppletCached;
	tn.javaAppletVersion = iu.javaAppletVersion;

	tn.addParam("ParentControlName", "ImageUploader1");
	tn.addParam("Guid", guid);
	tn.addParam("FileName", fileName);
	h += tn.getHtml();

	h += "</td>";
	h += "<td valign=\"top\">";

	//Add Title element.
	h += "Title:<br />";
	h += "<input id=\"Title" + uniqueId + "\" class=\"Title\" type=\"text\" /><br />";

	//Add Description element.
	h += "Description:<br />";
	h += "<textarea id=\"Description" + uniqueId + "\" class=\"Description\"\"></textarea>";

	h += "</td>";
	h += "</tr>";
	h += "<tr>";
	h += "<td align=\"center\"><a href=\"#\" onclick=\"return Remove_click('" + guid + "');\">Remove</a></td>";
	h += "<td></td>";
	h += "</tr>";
	h += "</tbody></table>";

	//Create DIV element which will represent the upload list item.
	var div = document.createElement("div");
	div.className = "UploadFile";
	div.innerHTML = h;
	div._guid = guid;
	//_uniqueId is used for fast access to the Title and Description form elements.
	div._uniqueId = uniqueId;

	//Append this upload list item to the custom upload pane.
	document.getElementById("UploadPane").appendChild(div);

	//Increase the ID to guaranty uniqueness.
	uniqueId++;
}

//Synchronize custom upload pane with Image Uploader upload list when 
//some files are added or removed.
function ImageUploader_UploadFileCountChange(){
	if (imageUploader1){
		var uploadFileCount  = imageUploader1.getUploadFileCount();

		//Files are being added.
		if (prevUploadFileCount <= uploadFileCount){
			for (var i = prevUploadFileCount + 1; i <= uploadFileCount; i++){
				addUploadFileHtml(i);
			}
		}
		//Files are being removed.
		else{
			var guidIndexHash = getGuidIndexHash();
			var UploadPane = document.getElementById("UploadPane");
			var i = UploadPane.childNodes.length - 1;
			while (i >= 0){
				if (guidIndexHash["" + UploadPane.childNodes[i]._guid] == undefined){
					UploadPane.removeChild(UploadPane.childNodes[i]);
				}
				i--;
			}
		}

		prevUploadFileCount = uploadFileCount;

		//document.getElementById("UploadButton").disabled = (uploadFileCount == 0);
	}
}

//Append the additional data entered by the user (title and description)
//to the upload. If you add more fields, do not forget to modify this event 
//handler to call AddField for these fields.
function ImageUploader_BeforeUpload(){
	var guidIndexHash = getGuidIndexHash();

	var UploadPane = document.getElementById("UploadPane");

	for (var i = 0; i < UploadPane.childNodes.length; i++){
		var div = UploadPane.childNodes[i];

		var index = guidIndexHash[div._guid];

		//Description will be sent as a native Description POST field 
		//provided by Image Uploader.
		imageUploader1.setUploadFileDescription(index,
			document.getElementById("Description" + div._uniqueId).value);

		//Title will be sent as a custom Title_N POST field, where N is an 
		//index of the file.
			
		    imageUploader1.AddField("Title_" + index, document.getElementById("Title" + div._uniqueId).value);
		
	}
	//to get the AlbumName and Album Description added in the textboxes.
	var valId = document.forms['aspnetForm'].ctl00$ModuleContentPlaceHolder$hdnAlbumId.value;
	if (!isEmpty(valId))
	{
	    imageUploader1.AddField('AlbumName',document.forms['aspnetForm'].ctl00$ModuleContentPlaceHolder$txtAlbumName.value );
	    imageUploader1.AddField('AlbumDesc',document.forms['aspnetForm'].ctl00$ModuleContentPlaceHolder$txtAlbumDesc.value );
	}
	else
	{
	    imageUploader1.AddField('AlbumName',"");
	    imageUploader1.AddField('AlbumDesc',"");
	}
	imageUploader1.AddField('AlbumId',document.forms['aspnetForm'].ctl00$ModuleContentPlaceHolder$hdnAlbumId.value );
}

//This function is used to handle Remove link click. It removes an item 
//from the custom upload pane by specified GUID.
function Remove_click(guid){
	var guidIndexHash = getGuidIndexHash();
	imageUploader1.UploadFileRemove(guidIndexHash[guid]);
}

//This function posts data on server.
function UploadButton_click(){
    if (validateData())
    {
	    imageUploader1.Send();
	}
}