
  /*----  Made By Ashu (June 2, 2011) -----*/
    /***********LHK:NewExpiryPopUp*************/
function fnReachLimitExpiryPopup(guid,title,value,tributeUrl,tributeId,appdomain,topHeight)
{
    customModalBox2.init();
    var appHeight = 0;
   if (value == 'UploadPhoto')
    {
        //var srcFile = App_Domain+"ModelPopup/ExpiryNotice.aspx";	
        var srcFile = "../ModelPopup/UploadImage.aspx"        
        if (guid) srcFile += '?location=' + guid+'&title=' + title+'&TributeId='+tributeId+'&TabName='+value; /* sample code to append a unique user ID to page called */	
        cFrame =new Element('iframe').setProperties({id:"iframe-expiry", name:"iframe-expiry", height:'1200px', width:'680px',frameborder:"0", scrolling:"no"}).injectInside($('divShowModalPopup'));
    }
   else if ((value == 'Notes')||(value == 'Events')||(value == 'Photos')||(value == 'Videos')||(value == 'UpgradeAlbum')||(value == 'UpgradePhoto'))
    {

        if ((value == 'UpgradePhoto')||(value == 'Videos'))
        {
            appHeight = 33;
        }
        if (value == 'NonMemo') {


        }
        if ((value == 'UpgradeAlbum')||(value == 'Photos')) {
            //appHeight = -22;
        }
        //var srcFile = App_Domain+"ModelPopup/ExpiryNotice.aspx";	
        var srcFile = "../ModelPopup/ReachLimitModalPopup.aspx"        
        if (guid) srcFile += '?location=' + guid+'&title=' + title+'&TributeId='+tributeId+'&TabName='+value; /* sample code to append a unique user ID to page called */	
        cFrame =new Element('iframe').setProperties({id:"iframe-expiry", name:"iframe-expiry", height:'590px', width:'600px',frameborder:"0", scrolling:"no"}).injectInside($('divShowModalPopup'));
    }
    else
    {     
        //var srcFile = appdomain+"ModelPopup/MemVideoExpNotice.aspx"
        var srcFile = "../ModelPopup/ReachLimitModalPopup.aspx"
        if (guid) srcFile += '?location=' + guid+'&title=' + title+'&TributeId='+tributeId; /* sample code to append a unique user ID to page called */	
        cFrame =new Element('iframe').setProperties({id:"iframe-expiry", name:"iframe-expiry", height:'590px', width:'560px', frameborder:"0", scrolling:"no"}).injectInside($('divShowModalPopup'));
    } 
    
	$('iframe-expiry').src = srcFile;	
	customModalBox2.htmlBox2('iframe-expiry', '', 'expireNotice',1); 
	$('mb_contents2').addClass('yt-Panel-Primary');
	/*new Element('div').setHTML('&nbsp;').setProperty('id','mb_Error').injectTop($('mb_contents'));*/
	new Element('h2').setHTML('UPGRADE TRIBUTE').setProperty('id','mb_Title2').injectTop($('mb_contents2'));
	/*To update the size of the modal Box*/

	var modalHeight = $('ctl00_divContentContainer').offsetHeight;
	
    var modalWidth = $('ctl00_divContentContainer').offsetWidth;
    
    var modalOverlay = $('mb_overlay2');
    var modalOverlayFrame = $('mb_frame2');
    var windowWidth = modalOverlayFrame.offsetWidth;//Window.getWidth();
    var leftOfOverlay = windowWidth/2;
  
    if (parseInt(modalWidth/2) > 0) {
			var marginLeftOfOverlay = '-'+(parseInt(modalWidth/2))+'px';
		} else {
			var marginLeftOfOverlay = (Math.abs(parseInt(modalWidth/2)))+'px';
		}

		modalOverlay.setStyles({ 'top': topHeight + 'px', marginLeft: marginLeftOfOverlay, left: leftOfOverlay, width: modalWidth + 'px', height: (modalHeight + appHeight) + 'px' });
    modalOverlayFrame.setStyles({'top': topHeight+'px'});
	/*update size ends here*/
	/*To update the size of the modal Box when window resizes*/
	
var browserVersion=navigator.appVersion;

var index= browserVersion.indexOf('MSIE 7');


	
	
	window.addEvent('resize', function resizeModalOverlay(){
	    var wWindow = 0; //width of window
	    var mOverlayFrame = $('mb_frame2');
        wWindow = mOverlayFrame.offsetWidth;
	    var wModal = $('ctl00_divContentContainer').offsetWidth;
	    var hModal = $('ctl00_divContentContainer').offsetHeight;
        var mOverlay = $('mb_overlay2');  
        var leftOverlay = wWindow/2;
        if (parseInt(wModal/2) > 0) {
			    var marginOverlay = '-'+(parseInt(wModal/2))+'px';
		    } else {
			    var marginOverlay = (Math.abs(parseInt(wModal/2)))+'px';
		    }  
	if(index ==-1)	      
        mOverlay.setStyles({marginLeft: marginOverlay, left: leftOverlay+'px', width:wModal+'px', height: hModal+'px'});		
    else
        mOverlay.setStyles({marginLeft: marginOverlay, left: leftOverlay+'px',position:absolute, width:wModal+'px', height: hModal+'px'});		    
	});
	/*resizing ends here*/
	/*To update the size of the modal Box when window resizes*/
     window.addEvent('resize', function resizeModalOverlay(){
         var wWindow = 0; //width of window
         var mOverlayFrame = $('mb_frame2');
            wWindow = mOverlayFrame.offsetWidth;
         var wModal = $('ctl00_divContentContainer').offsetWidth;
         var hModal = $('ctl00_divContentContainer').offsetHeight;
            var mOverlay = $('mb_overlay2');  
            var leftOverlay = wWindow/2;
            if (parseInt(wModal/2) > 0) {
           var marginOverlay = '-'+(parseInt(wModal/2))+'px';
          } else {
           var marginOverlay = (Math.abs(parseInt(wModal/2)))+'px';
          }    
          	if(index ==-1)	     
                mOverlay.setStyles({marginLeft: marginOverlay, left: leftOverlay+'px', width:wModal+'px', height: hModal+'px'});  
            else
                mOverlay.setStyles({marginLeft: marginOverlay, left: leftOverlay+'px',position:absolute, width:wModal+'px', height: hModal+'px'});  
     });
 /*resizing ends here*/
    //UpgradeNoticeClose1();  
   //$('mb_close_link').remove();
	
}

// The customModalBox object in its beauty
var customModalBox2 = {
	// init the customModalBox
	init: function (options) {
		

		// scan anchors for those opening a customModalBox
		this.anchors = [];
		$A($$('a')).each(function(el){
			// we use a regexp to check for links that 
			// have a rel attribute starting with "modalbox"
			if(el.rel && el.href && el.rel.test('^modalbox', 'i')) {
				el.onclick = this.click.pass(el, this);
				this.anchors.push(el);
			}
		}, this);
		
		// add event listeners
		this.eventPosition = this.position.bind(this);
		
		// init the HTML elements
		// the overlay (clickable to close)
		//this.varInjectInside = document.body;
		this.overlay = new Element('div').setProperty('id','mb_overlay2').injectInside($('divShowModalPopup'));
		this.overlayFrame = new Element('iframe').setProperties({'id':'mb_frame2', 'scrolling':'no', 'frameborder':'0'}).injectBefore(this.overlay);
		
		this.overlayFrame.setStyle('height','1px');
		
		// the center element
		this.center = new Element('div').setProperty('id', 'mb_center2').setStyle('display','none').injectTop(document.forms[0]);
		this.header = new Element('div').setProperty('id', 'mb_header2').injectInside(this.center);


		// the actual page contents
		this.contents = new Element('div').setProperty('id', 'mb_contents2').injectInside(this.center);
		
		
		
		//this.caption = new Element('div').setProperty('id', 'mb_caption').injectInside(this.center);
		
		// This block may be unneccessary
		this.center.onclick = '';
		//this.sTop = '0px';
		this.isShortModal = 0;		

	},


	position: function() {
		this.overlay.setStyles({top: '0px', height: Window.getScrollHeight()+'px'});
		this.overlayFrame.setStyles({top: '0px', height: Window.getScrollHeight()+'px'});
	},

	setup: function(open) {
		var elements = $A($$('object'));
		elements.extend($$(window.ActiveXObject ? 'select' : 'embed'));
		elements.each(function(el){ el.style.visibility = open ? 'hidden' : ''; });
		var fn = open ? 'addEvent' : 'removeEvent';
		//window[fn]('scroll', this.eventPosition)[fn]('resize', this.eventPosition);
	},
	

	
	htmlBox2: function(sContents, sHtml, sLinkTitle, isShortModal) {

		this.title = sLinkTitle;
		
		// create a unique classname based on passed title 
		// for outer container so we can style box with css
		// name format is "mb-{title}Modal"
		this.center.className = 'mb-'+this.title.replace(/ /g,'')+'Modal';		
		// set the box centered (optional if coded in CSS)
		if ((parseInt(this.center.getStyle('width'))/2) > 0) {
			var leftVal = '-'+(parseInt(this.center.getStyle('width'))/2)+'px';
		} else {
			var leftVal = (Math.abs(parseInt(this.center.getStyle('width'))/2))+'px';
		}
		this.center.setStyles({cursor: 'default', marginLeft: leftVal});
		
		
		// Set the modal top position (optional if hardcoded in CSS)
		if(isShortModal == 1)
		    this.top = '250';
		else
		    this.top = Window.getScrollTop() + (Window.getHeight() / 15);
		this.center.setStyles({top: this.top+'px', display: ''});

		
		
		//Show overlay
		this.overlay.setStyle('opacity',0.7);
		this.overlayFrame.setStyle('opacity',0.1);
		
		if (sHtml) this.contents.setHTML(sHtml);
		if ($(sContents)) $(sContents).injectInside(this.contents);
		
		if ($('mb_close_link')) {
			$('mb_close_link').remove();
		}
		this.closelink = new Element('a').setProperties({id: 'mb_close_link', href: 'javascript: void(0);'}).injectInside(this.header);
		// attach the close event to the close button / the overlay
		this.closelink.onclick = this.close.bind(this);
		this.closelink.innerHTML = 'Close';
		
		this.position();
		
		//hide page contents for printing the modal only
		//must have .yt-NoPrint {display:none} in CSS
		//no print style is removed on modal close
		document.forms[0].getChildren().each(function(el) {
			if (el.id != "mb_center2") {
				el.addClass('yt-NoPrint');
			}
		});
		this.closelink.addEvent('click', function() {
			document.forms[0].getChildren().each(function(el) {
				el.removeClass('yt-NoPrint');
			});
		});
		
	},

	
	close: function() {
		this.center.style.display = 'none';
		this.overlay.setStyle('opacity',0);
		this.overlayFrame.setStyle('opacity',0);
		this.overlayFrame.setStyle('height','1px');
		//this.setup(false);  //re-enable dropdowns, objects
		
		if ($$('.mb-TributeVideoModal #yt-flashcontent')) {
            $$('.mb-TributeVideoModal #yt-flashcontent').setHTML('');  //clear any videos that might be loaded
            }
		
		return false;
	}
		
};


 

/**********LHK:till here*****************/

function fnExpiryNoticePopupClose() {

	
		if($('iframe-expiry')) $('iframe-expiry').remove();
		if($('mb_Title')) $('mb_Title').remove();
		if($('mb_contents')) $('mb_contents').removeClass('yt-Panel-Primary');		
		if($('mb_Error')) $('mb_Error').remove();
		if($('mb_center')) $('mb_center').removeClass('mb-expireNoticeModal');	
	
		if($('mb_center')) $('mb_center').remove();
        if($('mb_overlay')) $('mb_overlay').remove();
        if($('mb_overlay')) $('mb_overlay').remove();		

}

function fnExpiryNoticePopup(guid,title,value,tributeId,appdomain,topHeight)
{   
    customModalBox2.init();
    var applHeight = 0;
    if (value == 'NonMemo') {
        applHeight = -22;
        //var srcFile = App_Domain+"ModelPopup/ExpiryNotice.aspx";	
        var srcFile = "../ModelPopup/ExpiryNotice.aspx"        
        if (guid) srcFile += '?location=' + guid+'&title=' + title+'&TributeId='+tributeId; /* sample code to append a unique user ID to page called */	
        cFrame =new Element('iframe').setProperties({id:"iframe-expiry", name:"iframe-expiry", height:'510px', width:'590px',frameborder:"0", scrolling:"no"}).injectInside($('divShowModalPopup'));

    }
    else
    {       
        //var srcFile = appdomain+"ModelPopup/MemVideoExpNotice.aspx"
        var srcFile = "../ModelPopup/MemVideoExpNotice.aspx"
        if (guid) srcFile += '?location=' + guid+'&title=' + title+'&TributeId='+tributeId; /* sample code to append a unique user ID to page called */	
        cFrame =new Element('iframe').setProperties({id:"iframe-expiry", name:"iframe-expiry", height:'677px', width:'590px', frameborder:"0", scrolling:"no"}).injectInside($('divShowModalPopup'));
    } 
    
    
    
	$('iframe-expiry').src = srcFile;	
	customModalBox2.htmlBox2('iframe-expiry', '', 'expireNotice',1); 
	$('mb_contents2').addClass('yt-Panel-Primary');
	/*new Element('div').setHTML('&nbsp;').setProperty('id','mb_Error').injectTop($('mb_contents'));*/
	new Element('h2').setHTML('UPGRADE TRIBUTE').setProperty('id','mb_Title2').injectTop($('mb_contents2'));
	/*To update the size of the modal Box*/

	var modalHeight = $('ctl00_divContentContainer').offsetHeight;
	
    var modalWidth = $('ctl00_divContentContainer').offsetWidth;
    
    var modalOverlay = $('mb_overlay2');
    var modalOverlayFrame = $('mb_frame2');
    var windowWidth = modalOverlayFrame.offsetWidth;//Window.getWidth();
    var leftOfOverlay = windowWidth/2;
  
    if (parseInt(modalWidth/2) > 0) {
			var marginLeftOfOverlay = '-'+(parseInt(modalWidth/2))+'px';
		} else {
			var marginLeftOfOverlay = (Math.abs(parseInt(modalWidth/2)))+'px';
		}

		modalOverlay.setStyles({ 'top': topHeight + 'px', marginLeft: marginLeftOfOverlay, left: leftOfOverlay, width: modalWidth + 'px', height: (modalHeight + applHeight) + 'px' });
    modalOverlayFrame.setStyles({'top': topHeight+'px'});
	/*update size ends here*/
	/*To update the size of the modal Box when window resizes*/
	
var browserVersion=navigator.appVersion;

var index= browserVersion.indexOf('MSIE 7');


	
	
	window.addEvent('resize', function resizeModalOverlay(){
	    var wWindow = 0; //width of window
	    var mOverlayFrame = $('mb_frame2');
        wWindow = mOverlayFrame.offsetWidth;
	    var wModal = $('ctl00_divContentContainer').offsetWidth;
	    var hModal = $('ctl00_divContentContainer').offsetHeight;
        var mOverlay = $('mb_overlay2');  
        var leftOverlay = wWindow/2;
        if (parseInt(wModal/2) > 0) {
			    var marginOverlay = '-'+(parseInt(wModal/2))+'px';
		    } else {
			    var marginOverlay = (Math.abs(parseInt(wModal/2)))+'px';
		    }  
	if(index ==-1)	      
        mOverlay.setStyles({marginLeft: marginOverlay, left: leftOverlay+'px', width:wModal+'px', height: hModal+'px'});		
    else
        mOverlay.setStyles({marginLeft: marginOverlay, left: leftOverlay+'px',position:absolute, width:wModal+'px', height: hModal+'px'});		    
	});
	/*resizing ends here*/
	/*To update the size of the modal Box when window resizes*/
     window.addEvent('resize', function resizeModalOverlay(){
         var wWindow = 0; //width of window
         var mOverlayFrame = $('mb_frame2');
            wWindow = mOverlayFrame.offsetWidth;
         var wModal = $('ctl00_divContentContainer').offsetWidth;
         var hModal = $('ctl00_divContentContainer').offsetHeight;
            var mOverlay = $('mb_overlay2');  
            var leftOverlay = wWindow/2;
            if (parseInt(wModal/2) > 0) {
           var marginOverlay = '-'+(parseInt(wModal/2))+'px';
          } else {
           var marginOverlay = (Math.abs(parseInt(wModal/2)))+'px';
          }    
          	if(index ==-1)	     
                mOverlay.setStyles({marginLeft: marginOverlay, left: leftOverlay+'px', width:wModal+'px', height: hModal+'px'});  
            else
                mOverlay.setStyles({marginLeft: marginOverlay, left: leftOverlay+'px',position:absolute, width:wModal+'px', height: hModal+'px'});  
     });
 /*resizing ends here*/
    //UpgradeNoticeClose1();  
   $('mb_close_link').remove();
	
}

function fnCursorStyle()
{
    
     document.body.style.cursor='Hand';
}

function fnDefaultStyle()
{
    
     document.body.style.cursor='Default';
}


