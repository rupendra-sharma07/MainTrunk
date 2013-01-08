///Copyright       : Copyright (c) Optimus Information
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Common.JavaScript.Event.js
///Author          : 
///Creation Date   : 
///Description     : This file is used to perform basic functions of the Events
///Audit Trail     : Date of Modification  Modified By         Description


function msieversion()
{
var ua = window.navigator.userAgent
var msie = ua.indexOf ( "MSIE " )
if ( msie > 0 )      // is Microsoft Internet Explorer; return version number
   return parseInt ( ua.substring ( msie+5, ua.indexOf ( ".", msie ) ) )
else
   return 0          // is other browser
}


function OpenGuestListFullDetails(srcFile) {
    var cFrame = new Element('iframe').setProperties({id:"iframe1", height:'405px', width:'600px', frameborder:"0", scrolling:"no", src:srcFile}).injectInside(document.body);

    customModalBox.htmlBox('iframe1', '', 'GuestListFullDetails'); 
    $('mb_contents').addClass('yt-Panel-Primary');
    new Element('h2').setHTML('RSVP Details').setProperty('id','mb_Title').injectTop($('mb_contents'));
    GuestListFullDetailsClose();
}



function GuestListFullDetailsClose() {
	$('mb_close_link').addEvent('click', function() {
		if($('iframe1')) $('iframe1').remove();
		if($('mb_Title')) $('mb_Title').remove();
		if($('mb_contents')) $('mb_contents').removeClass('yt-Panel-Primary');
	});

}
