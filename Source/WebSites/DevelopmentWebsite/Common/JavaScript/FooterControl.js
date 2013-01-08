///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Common.JavaScript.FooterControl.js
///Author          : 
///Creation Date   : 
///Description     : This file is used to perform basic functions of the footer control used on the site
///Audit Trail     : Date of Modification  Modified By         Description


function msieversion()
// return Microsoft Internet Explorer (major)
// version number, or 0 for others.
// This function works by finding the "MSIE "
// string and extracting the version number
// following the space, up to the decimal point
// for the minor version, which is ignored.
{
var ua = window.navigator.userAgent
var msie = ua.indexOf ( "MSIE " )
if ( msie > 0 )      // is Microsoft Internet Explorer; return version number
   return parseInt ( ua.substring ( msie+5, ua.indexOf ( ".", msie ) ) )
else
   return 0          // is other browser
}


function OpenContactUS() {

	//var browserName=navigator.appName; 
    var srcFile = "../ModelPopup/ContactUs.aspx";      
    var cFrame = new Element('iframe').setProperties({id:"iframe1", height:'610px', width:'615px', frameborder:"0", scrolling:"no", src:srcFile}).injectInside(document.body);

    // commented var browsername and the following lines as per mail from client

//    if (browserName=="Microsoft Internet Explorer" )
//    {  
//        var x=msieversion();
//        if (x<=6)
//          alert("You need to upgrade your browser.");
//    }        

    customModalBox.htmlBox('iframe1', '', 'ContactUs'); 
    $('mb_contents').addClass('yt-Panel-Primary');
//    new Element('div').setHTML('&nbsp;').setProperty('id','mb_Error').injectTop($('mb_center'));
    new Element('h2').setHTML('Contact Us').setProperty('id','mb_Title').injectTop($('mb_contents'));
    ContactUsClose();
}

function OpenContactUSNew(str) {

	//var browserName=navigator.appName; 
    var srcFile = str + "ModelPopup/ContactUs.aspx";      
    var cFrame = new Element('iframe').setProperties({id:"iframe1", height:'610px', width:'615px', frameborder:"0", scrolling:"no", src:srcFile}).injectInside(document.body);

    // commented var browsername and the following lines as per mail from client

//    if (browserName=="Microsoft Internet Explorer" )
//    {  
//        var x=msieversion();
//        if (x<=6)
//          alert("You need to upgrade your browser.");
//    }        

    customModalBox.htmlBox('iframe1', '', 'ContactUs'); 
    $('mb_contents').addClass('yt-Panel-Primary');
//    new Element('div').setHTML('&nbsp;').setProperty('id','mb_Error').injectTop($('mb_center'));
    new Element('h2').setHTML('Contact Us').setProperty('id','mb_Title').injectTop($('mb_contents'));
    ContactUsClose();
}

function ContactUsClose() {

	$('mb_close_link').addEvent('click', function() {
		if($('iframe1')) $('iframe1').remove();
		if($('mb_Title')) $('mb_Title').remove();
		if($('mb_contents')) $('mb_contents').removeClass('yt-Panel-Primary');
//		if($('mb_Error')) $('mb_Error').remove();  //COMDIFFRES: (this change is done by apurva to fix a defect) why this line has been commented? this line is not commented in .com version 
	});
}

function OpenSendMessage(emailId)
{	
   var guid=0;
	var srcFile = "../ModelPopup/SendMessage.aspx?emailId=" + emailId;		
	var cFrame = new Element('iframe').setProperties({id:"iframe2", name:"iframe2", height:'450px', width:'600px', frameborder:"0", scrolling:"no"}).injectInside(document.body);
	$('iframe2').src = srcFile;
	customModalBox.htmlBox('iframe2', '', 'Send Us A Message'); 
	$('mb_contents').addClass('yt-Panel-Primary');	
	new Element('div').setHTML('&nbsp;').setProperty('id','mb_Error').injectTop($('mb_center'));
	new Element('h2').setHTML('Send Us A Message').setProperty('id','mb_Title').injectTop($('mb_contents'));
	SendMessageClose();
}

function SendMessageClose() {
	$('mb_close_link').addEvent('click', function() {
		if($('iframe2')) $('iframe2').remove();
		if($('mb_Title')) $('mb_Title').remove();
		if($('mb_contents')) $('mb_contents').removeClass('yt-Panel-Primary');
		if($('mb_Error')) $('mb_Error').remove();
		
	});
}

function doModalHelpNew(str) {
	
	//var browserName=navigator.appName; 
	var srcFile = str + "ModelPopup/Help.aspx";	
	//alert(srcFile);
	//var srcFile = "../DevelopmentWebsite/ModelPopup/Help.aspx";	
	var cFrame = new Element('iframe').setProperties({id:"yt-HelpContent", height:'435px', width:'600px', frameborder:"0", scrolling:"no", src:srcFile}).injectInside(document.body);
	
	// commented var browsername and the following lines as per mail from client	
//    if (browserName=="Microsoft Internet Explorer" )
//    {  
//        var x=msieversion();
//        if (x<=6)
//            alert("You need to upgrade your browser.");
//    }        
														
	customModalBox.htmlBox('yt-HelpContent', '', 'Help'); 
	$('mb_contents').addClass('yt-Panel-Primary');
	new Element('h2').setHTML('Help').setProperty('id','mb_Title').injectTop($('mb_contents'));
	helpClose();
}
function doModalHelp() {
	
	//var browserName=navigator.appName; 
	var srcFile = str + "../ModelPopup/Help.aspx";	
	var cFrame = new Element('iframe').setProperties({id:"yt-HelpContent", height:'435px', width:'600px', frameborder:"0", scrolling:"no", src:srcFile}).injectInside(document.body);
	
	// commented var browsername and the following lines as per mail from client	
//    if (browserName=="Microsoft Internet Explorer" )
//    {  
//        var x=msieversion();
//        if (x<=6)
//            alert("You need to upgrade your browser.");
//    }        
														
	customModalBox.htmlBox('yt-HelpContent', '', 'Help'); 
	$('mb_contents').addClass('yt-Panel-Primary');
	new Element('h2').setHTML('Help').setProperty('id','mb_Title').injectTop($('mb_contents'));
	helpClose();
}

function helpClose() {
	$('mb_close_link').addEvent('click', function() {
		if($('yt-HelpContent')) $('yt-HelpContent').remove();
		if($('mb_Title')) $('mb_Title').remove();
		if($('mb_contents')) $('mb_contents').removeClass('yt-Panel-Primary');
	});
}

//function doModalExpired() {
//	customModalBox.htmlBox('yt-ExpiredContent', '', 'Expired'); 
//	$('mb_close_link').remove();
//	$('mb_overlay').setStyle('opacity',0.5);
//}

//window.addEvent('domready', function() {	
//	if($('yt-ExpiredContent')) {
//		doModalExpired();
//	}
//});