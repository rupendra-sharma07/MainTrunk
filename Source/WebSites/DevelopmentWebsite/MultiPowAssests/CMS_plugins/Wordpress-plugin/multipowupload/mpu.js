/*
MultiPowUpload plugin 

Java script code for flash based MultiPowUpload uploader.

Author: Viktor Shelekhov
*/

function MultiPowUpload_onServerResponse(fileObj) 
{	
	uploadSuccess(fileObj, fileObj.serverResponse);
}

function MultiPowUpload_onFileComplete(fileObj) 
{	
	if(!mpu_getParamBool("sendThumbnails") || 
		(mpu_getParamBool("sendThumbnails") && (mpu_getParamBool("sendOriginalImages") || 
			(!mpu_getParamBool("sendOriginalImages") && !fileObj.isValidImage))))
	{		
		//Create media-item
		fileQueued(fileObj);
		//and handle complete
		uploadComplete(fileObj);	
	}
}

function mpu_getParamBool(name)
{
	var val = MultiPowUpload.getParameter(name);
	if(!val || val.indexOf("false") >=0 )
		return false;
	return true;
}

/*
	Thumbnails as separate file, because MultiPowUpload can be configured to upload both of original image and thumbnail
*/
function MultiPowUpload_onThumbnailUploadComplete(fileObj, response)
{	
	
	fileObj.id = "mpu_thmb-"+fileObj.id;
	fileObj.name = fileObj.thumbnailFileName;	
	//Create media-item
	fileQueued(fileObj);
	//and handle complete
	uploadComplete(fileObj);	
	
	//handle uploadsuccess	
	uploadSuccess(fileObj, response);	
}

function MultiPowUpload_onError(file, message)
{
	wpQueueError(message);
}

function MultiPowUpload_onErrorMessage(message, displayed)
{
	if(!displayed)
		wpQueueError(message);
} 

function countFilesInQueue()
{
	var list = MultiPowUpload.getFiles();
	var counter =0;
	for(var i=0; i<list.length; i++)
		if(list[i].status < 2)
			counter++;	
	return {"files_queued": counter};
}

