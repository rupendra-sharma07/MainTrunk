/*------------------------------------------------------------

global.js

author: Sing Chan, Mark Bice
last modified: November 26, 2007

------------------------------------------------------------*/






/*------------------------------------------------------------

cookies

------------------------------------------------------------*/

function SetCookie(name, value, days) {
  if (days) {
    var date = new Date();
    date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
    var expires = "; expires=" + date.toGMTString();
  } else {
		expires = "";
	}
	
  document.cookie = name + "=" + value + expires + "; path=/";
}

function GetCookie(name) {
  var nameEQ = name + "=";
  var ca = document.cookie.split(';');
  for (var i = 0; i < ca.length; i++) {
    var c = ca[i];
    while (c.charAt(0) == ' ') c = c.substring(1, c.length);
    if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
  }
  return null;
}





/*------------------------------------------------------------

URL encoding, decoding

------------------------------------------------------------*/

function EncodeUrl(s) {
	var noEncode = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz" +
		"0123456789-_.!~*'()"; // chars that don't need encoding.
	var hex = "0123456789ABCDEF";
	var tmp = "";

	for (var i = 0; i < s.length; i++) {
		var ch = s.charAt(i);

		if (ch == " ") {
	    tmp += "+";
		} else if (noEncode.indexOf(ch) != -1) {
	    tmp += ch;
		} else {
		  var charCode = ch.charCodeAt(0);

			if (charCode > 255) {
				// unicode chars can't be encoded using standard URL encoding, just copy it.
				tmp += ch;
			} else {
				tmp += "%";
				tmp += hex.charAt((charCode >> 4) & 0xF);
				tmp += hex.charAt(charCode & 0xF);
			}
		}
	}

	return (tmp);
}

function DecodeUrl(s) {
	var hex = "0123456789ABCDEFabcdef"; 
	var tmp = "";
	var i = 0;

	while (i < s.length) {
		var ch = s.charAt(i);
		if (ch == "+") {
			tmp += " ";
			i++;
		} else if (ch == "%") {
			if (i < (s.length-2) && hex.indexOf(s.charAt(i + 1)) != -1 && hex.indexOf(s.charAt(i + 2)) != -1 ) {
				tmp += unescape(s.substr(i, 3));
				i += 3;
			} else {
				// if the escaped character is not supported, just copy as is
				tmp += ch;
				i++;
			}
		} else {
			tmp += ch;
			i++;
		}
	}

	return (tmp);
}





/*------------------------------------------------------------

this gets rid of the flashing background on rollover in IE6

------------------------------------------------------------*/

try {
	document.execCommand('BackgroundImageCache', false, true);
} catch(e) {}




/*------------------------------------------------------------

custom classes and functionality

------------------------------------------------------------*/


/* search */

var yt_SearchObj = new Class({
    initialize: function(launcher, panel) {
		this.launcher = $(launcher);
		this.container = $(panel);
		this.positioned = false;
		
		if (this.launcher != null) {  //we've rendered the search link
		
		    $(launcher).addEvent('click', function(event) { yt_Search.toggle(); } );
    		
		    var pos = Position.cumulativeOffset(this.launcher);
    		/*
            this.container.style.left = pos[0] - 213 + 'px';
            this.container.style.top = pos[1] + 32 + 'px';
            */
        }
	},

	toggle: function() {
	    this.container.style.display = (this.container.style.display == 'block') ? 'none' : 'block';
	    //var myFx = new Fx.Style(this.container, 'opacity', {duration:200}).start(0, 1);
    }
});


var yt_Search;
var Position = { cumulativeOffset: function(element) { var valueT = 0, valueL = 0; do { valueT += element.offsetTop || 0; valueL += element.offsetLeft || 0; element = element.offsetParent; } while (element); return [valueL, valueT]; } }


/* ajax wrapper */

yt_Ajax = function(url, options) {
    return new Ajax(url, options).request();
}



/* onload event */
window.addEvent('load', function() {
	yt_Search = new yt_SearchObj('yt-SearchLauncher', 'yt-Search');
	
	$$('#yt-TypeSizeControl a').each(function(el) {
	    el.addEvent('click', function() {
	        SetFontSize(this.className + '_text', true, this);
	    });
	    el.removeClass('selected');
	});
});


/*
switch font size
title - string; title attribute of stylesheet link element.
*/
function SetFontSize(title) {
    
    var i, a, main;
    var s="";
    
    for (i = 0; (a = document.getElementsByTagName("link")[i]); i++) {
        if (a.getAttribute("rel").indexOf("style") != -1 && a.getAttribute("title") && a.getAttribute("title").indexOf("_text") != -1) {
          a.disabled = true;
          if (a.getAttribute("title") == title) {
			        a.disabled = false;
		        }
        }
    }
    
    /* save in cookie */

	SetCookie("fontsize", title, 365);
	fontsizeTitle = title;
}



/*------------------------------------------------------------

attaching events to globally used elements (e.g. form hints,
check availability, etc)

------------------------------------------------------------*/

window.addEvent('load', function() {
    
    /* attach hint events */
    $$('.hint').each( function(a) {
        $E('input', a.parentNode).addEvent('focus', function() {
            a.setStyle('display', 'inline');
        });
        
        $E('input', a.parentNode).addEvent('blur', function() {
            a.setStyle('display', 'none');
        });
    });
    
	
//	$$('.yt-checkAvailability').each( function(a) {
//	    /* attach ajax call to check availability button */
//		a.addEvent('click', function() {
//			CheckAvailability(this);
//		});
//		
//		var notice = a.getNext();
//		notice.innerHTML = '';
//	    notice.className = 'availabilityNotice';
//		
//	});

});



/*------------------------------------------------------------

Functions to be run before on load events get called

------------------------------------------------------------*/

/* this gets called from just before closing </body> tag */

function executeBeforeLoad() {
	buttonStyles();
	thumbStyles();
	hrFix();
	fieldsetFix();
	tableFix();
	changeSiteThemeInit();
	addToFavoritesInit();
}

// hide/show for tribute tools change site theme link
function changeSiteThemeInit() {
        
    var yt_SelectedTheme;
    
    if ($$('.yt-Tool-ChangeTheme').length > 0) {  // theme switcher is there, add events

        var input;

        $$('.yt-ThemeSet .yt-Form-Field').each( function(a) {
            input = $(a.getElementsByTagName('input')[0]);
            
            if (input.checked)
                yt_SelectedTheme = input;
            
            input.addEvent('click', function() { changeSiteTheme(this); } );
        });
        
        
        [$$('.yt-Tool-ChangeTheme-Link'), $$('.yt-Tool-ChangeTheme .yt-CancelButton')].each( function(a) {
            a.addEvent('click', function() { if ($$('.yt-ThemeSet')[0].style.display == 'block') { changeSiteTheme(yt_SelectedTheme); } changeSiteThemeToggle(); } );
        });
        
    }
    
}

function changeSiteThemeToggle() {
    var themeSet = $$('.yt-ThemeSet')[0];
    themeSet.setStyle('display', (themeSet.style.display ==  'block') ? 'none' : 'block');
}

function changeSiteTheme(el) {
	$$('.yt-ThemeSet .yt-Form-Field').each( function(a) {
        a.removeClass('yt-Selected');
    })
    $(el.parentNode).addClass('yt-Selected');
    SetActiveStyleSheet(el.value, 0);
    
    el.checked = true;  //double check if we're using this for resetting themes
}

function addToFavoritesInit() {
    [$$('.yt-Tool-AddFavorites-Link'), $$('.yt-Add-Favorites-Confirmation .yt-CancelButton')].each( function(a) {
        a.addEvent('click', function() { toggleFavoritesPanel(); } );
    });
}

function toggleFavoritesPanel() {
    var pnl = $$('.yt-Add-Favorites-Confirmation')[0];
    pnl.style.display = (pnl.style.display == 'block') ? 'none' : 'block';
}



// add in end-cap code to button elements -- called from executeBeforeLoad() 
function buttonStyles() {
	$$('.yt-Button').each( function(a) {
		var leftCap = '<span class="yt-ButtonLeftCap"></span>';
		var rightCap = '<span class="yt-ButtonRightCap"></span>';				 
		a.innerHTML=leftCap+a.innerHTML+rightCap;
	});
}

// add in code to style thumbnails -- called from executeBeforeLoad() 
function thumbStyles() {
	$$('a.yt-Thumb img').each( function(a) {				 
		var thumbWrapper = new Element('div').addClass('yt-Thumb').injectBefore(a.getParent());
		a.getParent().removeClass('yt-Thumb').injectInside(thumbWrapper);
		new Element('span').addClass('yt-Thumb-Header').setHTML(' ').injectBefore(a);	
		new Element('span').addClass('yt-Thumb-Footer').setHTML(' ').injectAfter(a);	
	});
	$$('label.yt-Thumb img').each( function(a) {				 
		var thumbWrapper = new Element('div').addClass('yt-Thumb').injectBefore(a.getParent());
		a.getParent().removeClass('yt-Thumb').injectInside(thumbWrapper);
		new Element('span').addClass('yt-Thumb-Header').setHTML(' ').injectBefore(a);	
		new Element('span').addClass('yt-Thumb-Footer').setHTML(' ').injectAfter(a);	
	});
}


// add in code to style HR lines -- called from executeBeforeLoad() 
function hrFix() {
	$$('hr').each( function(a) {				 
		wrapper = new Element('div').injectBefore(a);
		a.injectInside(wrapper);
		wrapper.className = a.getProperty('class');
		a.removeProperty('class');
	});
}

// wrap all fieldsets with container DIV (class yt-FieldsetWrapper) and insert SPAN tags into legends
// function allows more versatility in styling fieldsets for all browsers
function fieldsetFix() {
	$$('fieldset').each( function(a) {
		if(!a.hasClass('yt-Form')) {
			wrapper = new Element('div').injectBefore(a);
			a.injectInside(wrapper);
			wrapper.className = a.getProperty('class');
			wrapper.addClass('yt-FieldsetWrapper');
			a.removeProperty('class');
		}
	});
	$$('legend').each( function(a) {
		newHTML = '<span>' + a.innerHTML + '</span>';
		a.setHTML(newHTML);
	});
}
       
// Add odd/even, first column/row, last column/row to tables
function tableFix() {
	$$('table').each( function(a) {
		$ES('thead', a).each( function(b) {
			$ES('tr', b).each( function(c) {
				//alert(c.tagName);
				if(c.getFirst() != null)
				{
				c.getFirst().addClass('yt-colFirst');
				c.getLast().addClass('yt-colLast');
				}
			});
		});
		$ES('tbody', a).each( function(b) {
			b.getFirst().addClass('yt-rowFirst');
			b.getLast().addClass('yt-rowLast');
			$ES('tr', b).each( function(c) {
				//alert(c.tagName);
				if(c.getFirst() != null)
				{
				c.getFirst().addClass('yt-colFirst');
				c.getLast().addClass('yt-colLast');
				}
			});
		});
	});
}

        /*------------------------------------------------------------    
            Called upon completion of XHR call for checking username 
            availability
        ------------------------------------------------------------ */
        
        CheckAvailabilityComplete = function(el) {
            //var result = response.transport.responseText;
            var result = 0;
            var notice = $(el).getNext();
            
            $$('.availabilityNotice').each( function(a) {
                a.removeClass('availabilityNotice-Loading');
            });
            
            if (result == 1) {
                notice.addClass('availabilityNotice-Available');
                notice.innerHTML = 'Available!';
            }
            else {
                notice.addClass('availabilityNotice-Unavailable');
                notice.innerHTML = 'Unavailable';
            }
        }
	    
	    
	    
	    /*------------------------------------------------------------    
	        Makes XHR call to see if username is available 
	    ------------------------------------------------------------*/
	    
	    CheckAvailability = function(el) {
	        var username = el.parentNode.getElementsByTagName('input')[0].value;
	        var notice = el.getNext();
	        
	        // ajax call made here, we'll then evaluate the output for 1 or 0 (available or not available)
	        // var myAjax = yt_Ajax(URL_GOES_HERE, {method: 'post', postBody: 'username=' + username, onComplete: CheckAvailabilityComplete});
	        
	        notice.innerHTML = '';
	        notice.className = 'availabilityNotice';
	        notice.addClass('availabilityNotice-Loading');
	        
	        CheckAvailabilityComplete(el);  // can be removed -- just here to demonstrate what delay would look like with loading gif
	    }





// Function to calculate age in years. Will always round down.

function displayAge(yStart, mStart, dStart, yEnd, mEnd, dEnd){
	if ((yStart != '') &&
		 (yEnd != '') &&
		 (!isNaN(yStart)) &&
		 (!isNaN(yEnd)) &&
		 (mStart > 0 ) &&
		 (dStart > 0 ) &&
		 (mEnd > 0 ) &&
		 (dEnd > 0 )) {
		var Age = yEnd - yStart;
		if(mEnd < mStart) {
			Age --;
		} else if (mEnd == mStart) {
			if(dEnd < dStart) {
				Age --;
			}
		}
		if (Age == 0) Age = 'less than 1 year old'; //customize this message?
		if (Age < 0) Age = '';
	} else {
		Age = '';
	}
	return(Age);
}

function ToggleElementDisplay(id) {
    var el = $('yt-ForgetUserNamePassword');
    el.style.display = (el.style.display == 'block') ? 'none' : 'block';
}

//Function to call image cropper - Added by LHK
function uploadVideoTributePhoto() {
	popupCropper('../Modelpopup/VideoImageCropper.aspx');
}

// Function to call image cropper - Added by Parul Jain

function uploadTributePhoto() {
    AdminpopupCropper('../Modelpopup/ImageCropper.aspx');
}

function uploadUserPhoto() {
    AdminpopupCropper('Modelpopup/AdminImageCropper.aspx'); //COMDIFFRES: make this '../Modelpopup
}

function uploadLogo() {
	popupCropper('Modelpopup/LogoImageCropper.aspx');//LHK
}

// Function to select all checkboxes inside an element passed in via arguments

function selectAll(parentEl, uncheckFlag) {
	el = $(parentEl);
	if (uncheckFlag) {
		$ES('input', el).each( function(a) {
			if((a.type == 'checkbox') && (a.checked)) {
				a.checked = false;
			}
		});
	
	} else {
		$ES('input', el).each( function(a) {
			if((a.type == 'checkbox') && (!a.checked)) {
				a.checked = true;
			}
		});
	}
}


// Flash embedding function for IE7 compatibility

EmbedVideo = function(srcURL) {
	window.addEvent('domready', function() {
		var so = new SWFObject(srcURL, 'yt-VideoItem', '425', '355', '8', '#000000');
		so.addParam("wmode", "transparent"); 
		so.write("yt-flashcontent");
	});
}

// Flash embedding function for IE7 compatibility

EmbedVideoTribute = function(srcURL) {
	window.addEvent('domready', function() {
		var so = new SWFObject(srcURL, 'yt-VideoItem', '720', '360', '8', '#000000');
		so.addParam("wmode", "transparent"); 
		so.write("yt-flashcontentOnVideoTribute");
	});
}

// old embed function (now using SWFObject)
EmbedFlash = function( swfsrc, swfid, width, height, flashvars, bgcolor )
{
	document.write("<OBJECT classid='clsid:D27CDB6E-AE6D-11cf-96B8-444553540000' codebase='" + window.location.protocol + "://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0' ID=" + swfid + " WIDTH=" + width + " HEIGHT=" + height + ">" );
	document.write("<PARAM NAME=movie VALUE='"+ swfsrc +"'>\n");
	document.write("<PARAM NAME=menu VALUE=\"false\">\n");
	document.write("<PARAM NAME=quality VALUE=\"high\">\n");
	document.write("<PARAM NAME=wmode VALUE=\"transparent\">\n");
	document.write("<PARAM NAME=bgcolor VALUE=\""+ bgcolor+"\">\n");
	document.write("<PARAM NAME=flashvars VALUE=\""+ flashvars+"\">\n");
	document.write("<EMBED NAME=\"" + swfid + "\" src='" + swfsrc + "' quality=high wmode=transparent menu=false bgcolor=" + bgcolor + " flashvars=\"" + flashvars + "\" WIDTH=" + width + " HEIGHT=" + height + " TYPE='application/x-shockwave-flash' PLUGINSPAGE='" + window.location.protocol + "://www.macromedia.com/go/getflashplayer'></EMBED></OBJECT>" );
}





function CheckGridviewControlSelection( gridView)
{
    var cb;
    var link; 
    var objField;       
    var OK  = false; 
    var td;
    var tr;   
    var tbl = createObject(gridView); 
    if ( tbl != null ) { 
        if ( tbl.rows.length > 0 ) {      
            tr = tbl.getElementsByTagName('tr');
            for (var i = 1; i < tbl.rows.length; i++) {
                td = tr[i].getElementsByTagName('td');
                cb = td[0].getElementsByTagName('input');
                if ( i == 0 ) objField = cb[0];
                if ( cb[0].checked ) {
                    OK = true;               
                    break;
                }                
            }       
        }
    }
    return OK;    
}

 

function createObject( objectID )

{

    var obj = document.getElementById( objectID );

    return obj;

}
