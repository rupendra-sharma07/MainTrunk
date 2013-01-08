

/* Made By Ashu (June 3, 2011)*/    


function fnCursorStyle()
{
    
     document.body.style.cursor='Hand';
}

function fnDefaultStyle()
{
    
     document.body.style.cursor='Default';
}


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
    
    var myElement=$('.yt-Tool-ChangeTheme');  
    var yt_SelectedTheme;
//    if(myElement.size>0)  
//    {
//    
    
    if ($('.yt-Tool-ChangeTheme').length > 0) {  // theme switcher is there, add events

        var input;
        
        $('.yt-ThemeSet .yt-Form-Field').each( function(a) {
            if(a.tagName!=undefined)
            {
                input = a.getElementsByTagName('input');
                
                if(input.length>0 )
                {
                    if (input.checked)
                        yt_SelectedTheme = input;
                    
                    input.addEvent('click', function() { changeSiteTheme(this); }
                    
                    
                     );
                 }
            }
        });
        
        var myElement=$('.yt-Tool-ChangeTheme-Link');
        var myElement2=$('.yt-Tool-ChangeTheme .yt-CancelButton');
        
        if(myElement.size()>0 && myElement2.size()>0)
        {
//            [$('.yt-Tool-ChangeTheme-Link'), $('.yt-Tool-ChangeTheme .yt-CancelButton')].each( function(a)
//            {
//              alert(a)
//                if(a!=null && a.tagName!=undefined)
//                alert(a)
//              //  a.addEvent('click', function() { if ($$('.yt-ThemeSet')[0].style.display == 'block') { changeSiteTheme(yt_SelectedTheme); } changeSiteThemeToggle(); } );
//            });
        }
        
    //}
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
var myElement=$('.yt-Tool-AddFavorites-Link');
var myElement2=$('.yt-Add-Favorites-Confirmation .yt-CancelButton');

// if(myElement.length>0 && myElement2.length>0)
//   {
//        [$$('.yt-Tool-AddFavorites-Link'), $$('.yt-Add-Favorites-Confirmation .yt-CancelButton')].each( function(a) {
//            a.addEvent('click', function() { toggleFavoritesPanel(); } );
//        });
//    }
}

function toggleFavoritesPanel() {
    var pnl = $$('.yt-Add-Favorites-Confirmation')[0];
    pnl.style.display = (pnl.style.display == 'block') ? 'none' : 'block';
}



// add in end-cap code to button elements -- called from executeBeforeLoad() 
function buttonStyles() {
   if($('#a').hasClass('yt-Button'))
   {
        if($$('.yt-Button')!=null)
        {
	        $$('.yt-Button').each( function(a) {
		        var leftCap = '<span class="yt-ButtonLeftCap"></span>';
		        var rightCap = '<span class="yt-ButtonRightCap"></span>';				 
		        a.innerHTML=leftCap+a.innerHTML+rightCap;
	        });
	}
	}
}

// add in code to style thumbnails -- called from executeBeforeLoad() 
function thumbStyles() {

//var myElement2=$('.yt-Thumb .img');
    if($('#a').hasClass('yt-Thumb') && $('#a').hasClass('img'))
    {
	        $$('a.yt-Thumb img').each( function(a) {				 
		        var thumbWrapper = new Element('div').addClass('yt-Thumb').injectBefore(a.getParent());
		        a.getParent().removeClass('yt-Thumb').injectInside(thumbWrapper);
		        new Element('span').addClass('yt-Thumb-Header').setHTML(' ').injectBefore(a);	
		        new Element('span').addClass('yt-Thumb-Footer').setHTML(' ').injectAfter(a);	
	        });
    }
    if($('#label').hasClass('yt-Thumb') && $('#a').hasClass('img'))
    {
            //var myElement=$('.yt-Thumb .img');
	    $$('label.yt-Thumb img').each( function(a) {				 
		    var thumbWrapper = new Element('div').addClass('yt-Thumb').injectBefore(a.getParent());
		    a.getParent().removeClass('yt-Thumb').injectInside(thumbWrapper);
		    new Element('span').addClass('yt-Thumb-Header').setHTML(' ').injectBefore(a);	
		    new Element('span').addClass('yt-Thumb-Footer').setHTML(' ').injectAfter(a);	
	    });
    }
}


// add in code to style HR lines -- called from executeBeforeLoad() 
function hrFix() {

    if($('#hr').length>0)
    {
	    $$('hr').each( function(a) {				 
		    wrapper = new Element('div').injectBefore(a);
		    a.injectInside(wrapper);
		    wrapper.className = a.getProperty('class');
		    a.removeProperty('class');
	    });
	}
}

// wrap all fieldsets with container DIV (class yt-FieldsetWrapper) and insert SPAN tags into legends
// function allows more versatility in styling fieldsets for all browsers
function fieldsetFix() {
    if($('#fieldset').length>0)
    {
	    $$('fieldset').each( function(a) {
		    if(!a.hasClass('yt-Form')) {
			    wrapper = new Element('div').injectBefore(a);
			    a.injectInside(wrapper);
			    wrapper.className = a.getProperty('class');
			    wrapper.addClass('yt-FieldsetWrapper');
			    a.removeProperty('class');
		    }
	    });
	}
    if($('#legend').length>0)
    {
	    $$('legend').each( function(a) {
		    newHTML = '<span>' + a.innerHTML + '</span>';
		    a.setHTML(newHTML);
	    });
	}
}
       
// Add odd/even, first column/row, last column/row to tables
function tableFix() {
if($('#table').length>0)
    {
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
}
