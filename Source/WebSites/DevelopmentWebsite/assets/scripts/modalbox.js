

/******************************************************************/
/*                        customModalBox 1.0.0                    */
/* A modal box (inline popup), used to display content            */
/* loaded using AJAX, written for the mootools framework          */
/*         based on MOOdalBox written by                          */
/*         Razvan Brates, razvan [at] e-magine.ro                 */
/*                                                                */
/* mootools found at:                                             */
/* http://mootools.net/                                           */
/*                                                                */
/* Original code based on "Slimbox", by Christophe Beyls:         */
/* http://www.digitalia.be/software/slimbox                       */
/******************************************************************/

// Constants defined here can be changed for easy config / translation
// (defined as vars, because of MSIE's lack of support for const)

var _ERROR_MESSAGE = "Oops.. there was a problem with your request.<br /><br />" +
					"Please try again.<br /><br />" +
					"<em>Click anywhere to close.</em>"; // the error message displayed when the request has a problem

// The customModalBox object in its beauty
var customModalBox1 = {
    // init the customModalBox
    init: function(options) {


        // scan anchors for those opening a customModalBox
        this.anchors = [];
        $A($$('a')).each(function(el) {
            // we use a regexp to check for links that 
            // have a rel attribute starting with "modalbox"
            if (el.rel && el.href && el.rel.test('^modalbox', 'i')) {
                el.onclick = this.click.pass(el, this);
                this.anchors.push(el);
            }
        }, this);

        // add event listeners
        this.eventPosition = this.position.bind(this);
        
       
        // init the HTML elements
        // the overlay (clickable to close)
        //this.varInjectInside = document.body;

        this.overlay = new Element('div').setProperty('id', 'mb_overlay_Popup').injectInside($('divShowModalPopup'));
        this.overlayFrame = new Element('iframe').setProperties({ 'id': 'mb_frame_Popup', 'scrolling': 'no', 'frameborder': '0' }).injectBefore(this.overlay);

        this.overlayFrame.setStyle('height', '1px');

        // the center element
        this.center = new Element('div').setProperty('id', 'mb_center_Popup').setStyle('display', 'none').injectTop(document.forms[0]);
        this.header = new Element('div').setProperty('id', 'mb_header_Popup').injectInside(this.center);


        // the actual page contents
        this.contents = new Element('div').setProperty('id', 'mb_contents_Popup').injectInside(this.center);



        //this.caption = new Element('div').setProperty('id', 'mb_caption').injectInside(this.center);

        // This block may be unneccessary
        this.center.onclick = '';
        //this.sTop = '0px';
        this.isShortModal = 0;

    },


    position: function() {
        this.overlay.setStyles({ top: '0px', height: Window.getScrollHeight() + 'px' });
        this.overlayFrame.setStyles({ top: '0px', height: Window.getScrollHeight() + 'px' });
    },

    setup: function(open) {
        var elements = $A($$('object'));
        elements.extend($$(window.ActiveXObject ? 'select' : 'embed'));
        elements.each(function(el) { el.style.visibility = open ? 'hidden' : ''; });
        var fn = open ? 'addEvent' : 'removeEvent';
        //window[fn]('scroll', this.eventPosition)[fn]('resize', this.eventPosition);
    },



    htmlBox1: function(sContents, sHtml, sLinkTitle, isShortModal) {
        this.title = sLinkTitle;

        // create a unique classname based on passed title 
        // for outer container so we can style box with css
        // name format is "mb-{title}Modal"
        this.center.className = 'mb-' + this.title.replace(/ /g, '') + 'Modal';
        // set the box centered (optional if coded in CSS)
        if ((parseInt(this.center.getStyle('width')) / 2) > 0) {
            var leftVal = '-' + (parseInt(this.center.getStyle('width')) / 2) + 'px';
        } else 
        {
            var leftVal = (Math.abs(parseInt(this.center.getStyle('width')) / 2)) + 'px';
       }
        this.center.setStyles({ cursor: 'default', marginLeft: leftVal });


        // Set the modal top position (optional if hardcoded in CSS)
        if (isShortModal == 1)
            this.top = '250';
        else
            this.top = Window.getScrollTop() + (Window.getHeight() / 15);
        this.center.setStyles({ top: this.top + 'px', display: '' });



        //Show overlay
        this.overlay.setStyle('opacity', 0.7);
        this.overlayFrame.setStyle('opacity', 0.1);

        if (sHtml) this.contents.setHTML(sHtml);
        if ($(sContents)) $(sContents).injectInside(this.contents);
       if ($('mb_close_link')) {
            $('mb_close_link').remove();
        }
        this.closelink = new Element('a').setProperties({ id: 'mb_close_link', href: 'javascript: void(0);' }).injectInside(this.header);
        // attach the close event to the close button / the overlay
        this.closelink.onclick = this.close.bind(this);
        this.closelink.innerHTML = 'Close';
        
       
        this.position();

        //hide page contents for printing the modal only
        //must have .yt-NoPrint {display:none} in CSS
        //no print style is removed on modal close
        document.forms[0].getChildren().each(function(el) {
            if (el.id != "mb_center_Popup") {
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
        this.overlay.setStyle('opacity', 0);
        this.overlayFrame.setStyle('opacity', 0);
        this.overlayFrame.setStyle('height', '1px');
        //this.setup(false);  //re-enable dropdowns, objects

        if ($$('.mb-TributeVideoModal #yt-flashcontent')) {
            $$('.mb-TributeVideoModal #yt-flashcontent').setHTML('');  //clear any videos that might be loaded
        }        
       $('mb_contents_Popup').innerHTML = "";
        
   
       
        return false;
    }
    
};
    
    
// The customModalBox object in its beauty
var customModalBox = {
    // init the customModalBox
    init: function(options) {


        // scan anchors for those opening a customModalBox
        this.anchors = [];
        $A($$('a')).each(function(el) {
            // we use a regexp to check for links that 
            // have a rel attribute starting with "modalbox"
            if (el.rel && el.href && el.rel.test('^modalbox', 'i')) {
                el.onclick = this.click.pass(el, this);
                this.anchors.push(el);
            }
        }, this);

        // add event listeners
        this.eventPosition = this.position.bind(this);
        
       
        // init the HTML elements
        // the overlay (clickable to close)
        //this.varInjectInside = document.body;

        this.overlay = new Element('div').setProperty('id', 'mb_overlay').injectInside($('divShowModalPopup'));
        this.overlayFrame = new Element('iframe').setProperties({ 'id': 'mb_frame', 'scrolling': 'no', 'frameborder': '0' }).injectBefore(this.overlay);

        this.overlayFrame.setStyle('height', '1px');

        // the center element
        this.center = new Element('div').setProperty('id', 'mb_center').setStyle('display', 'none').injectTop(document.forms[0]);
        this.header = new Element('div').setProperty('id', 'mb_header').injectInside(this.center);


        // the actual page contents
        this.contents = new Element('div').setProperty('id', 'mb_contents').injectInside(this.center);



        //this.caption = new Element('div').setProperty('id', 'mb_caption').injectInside(this.center);

        // This block may be unneccessary
        this.center.onclick = '';
        //this.sTop = '0px';
        this.isShortModal = 0;

    },


    position: function() {
        this.overlay.setStyles({ top: '0px', height: Window.getScrollHeight() + 'px' });
        this.overlayFrame.setStyles({ top: '0px', height: Window.getScrollHeight() + 'px' });
    },

    setup: function(open) {
        var elements = $A($$('object'));
        elements.extend($$(window.ActiveXObject ? 'select' : 'embed'));
        elements.each(function(el) { el.style.visibility = open ? 'hidden' : ''; });
        var fn = open ? 'addEvent' : 'removeEvent';
        //window[fn]('scroll', this.eventPosition)[fn]('resize', this.eventPosition);
    },



    htmlBox: function(sContents, sHtml, sLinkTitle, isShortModal) {
        // create a unique classname based on passed title 
        // for outer container so we can style box with css
        // name format is "mb-{title}Modal"
        //if(document.getElementById("divShowModalPopup").center != null)
     
        if( this.center != null)
        {
            this.center.className = 'mb-' + sLinkTitle.replace(/ /g, '') + 'Modal';
        }
        // set the box centered (optional if coded in CSS)
        if ((parseInt(this.center.getStyle('width')) / 2) > 0) {
            var leftVal = '-' + (parseInt(this.center.getStyle('width')) / 2) + 'px';
        } else 
        {
            var leftVal = (Math.abs(parseInt(this.center.getStyle('width')) / 2)) + 'px';
       }
        this.center.setStyles({ cursor: 'default', marginLeft: leftVal });


        // Set the modal top position (optional if hardcoded in CSS)
        if (isShortModal == 1)
            this.top = '250';
        else
            this.top = Window.getScrollTop() + (Window.getHeight() / 15);
        this.center.setStyles({ top: this.top + 'px', display: '' });



        //Show overlay
        this.overlay.setStyle('opacity', 0.7);
        this.overlayFrame.setStyle('opacity', 0.1);

        if (sHtml) this.contents.setHTML(sHtml);
        if ($(sContents)) $(sContents).injectInside(this.contents);
       if ($('mb_close_link')) {
            $('mb_close_link').remove();
        }
        this.closelink = new Element('a').setProperties({ id: 'mb_close_link', href: 'javascript: void(0);' }).injectInside(this.header);
        // attach the close event to the close button / the overlay
        this.closelink.onclick = this.close.bind(this);
        this.closelink.innerHTML = 'Close';
        
       
        this.position();

        //hide page contents for printing the modal only
        //must have .yt-NoPrint {display:none} in CSS
        //no print style is removed on modal close
        document.forms[0].getChildren().each(function(el) {
            if (el.id != "mb_center") {
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
        this.overlay.setStyle('opacity', 0);
        this.overlayFrame.setStyle('opacity', 0);
        this.overlayFrame.setStyle('height', '1px');
        //this.setup(false);  //re-enable dropdowns, objects

        if ($$('.mb-TributeVideoModal #yt-flashcontent')) {
            $$('.mb-TributeVideoModal #yt-flashcontent').setHTML('');  //clear any videos that might be loaded
        }        
  //  $('mb_contents').innerHTML = "";
        
   
       
        return false;
    }
    
};
    
      


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





/* ---------------Made by Ashu(29june,2011) for Modal popup with multiple buttons ------------------------------*/


var customModalBox3 = {
    // init the customModalBox
    init: function(options) {


        // scan anchors for those opening a customModalBox
        this.anchors = [];
        $A($$('a')).each(function(el) {
            // we use a regexp to check for links that 
            // have a rel attribute starting with "modalbox"
            if (el.rel && el.href && el.rel.test('^modalbox', 'i')) {
                el.onclick = this.click.pass(el, this);
                this.anchors.push(el);
            }
        }, this);

        // add event listeners
        this.eventPosition = this.position.bind(this);
        
       
        // init the HTML elements
        // the overlay (clickable to close)
        //this.varInjectInside = document.body;
        this.overlay = new Element('div').setProperty('id', 'mb_overlay3').injectInside($('divShowModalPopup'));
        this.overlayFrame = new Element('iframe').setProperties({ 'id': 'mb_frame3', 'scrolling': 'no', 'frameborder': '0' }).injectBefore(this.overlay);

        this.overlayFrame.setStyle('height', '1px');

        // the center element
        this.center = new Element('div').setProperty('id', 'mb_center3').setStyle('display', 'none').injectTop(document.forms[0]);
        this.header = new Element('div').setProperty('id', 'mb_header3').injectInside(this.center);


        // the actual page contents
        this.contents = new Element('div').setProperty('id', 'mb_contents3').injectInside(this.center);



        //this.caption = new Element('div').setProperty('id', 'mb_caption').injectInside(this.center);

        // This block may be unneccessary
        this.center.onclick = '';
        //this.sTop = '0px';
        this.isShortModal = 0;

    },


    position: function() {
        this.overlay.setStyles({ top: '0px', height: Window.getScrollHeight() + 'px' });
        this.overlayFrame.setStyles({ top: '0px', height: Window.getScrollHeight() + 'px' });
    },

    setup: function(open) {
        var elements = $A($$('object'));
        elements.extend($$(window.ActiveXObject ? 'select' : 'embed'));
        elements.each(function(el) { el.style.visibility = open ? 'hidden' : ''; });
        var fn = open ? 'addEvent' : 'removeEvent';
        //window[fn]('scroll', this.eventPosition)[fn]('resize', this.eventPosition);
    },



    htmlBox3: function(sContents, sHtml, sLinkTitle, isShortModal) {
        this.title = sLinkTitle;

        // create a unique classname based on passed title 
        // for outer container so we can style box with css
        // name format is "mb-{title}Modal"
        this.center.className = 'mb-' + this.title.replace(/ /g, '') + 'Modal';
        // set the box centered (optional if coded in CSS)
        if ((parseInt(this.center.getStyle('width')) / 2) > 0) {
            var leftVal = '-' + (parseInt(this.center.getStyle('width')) / 2) + 'px';
        } else 
        {
            var leftVal = (Math.abs(parseInt(this.center.getStyle('width')) / 2)) + 'px';
       }
        this.center.setStyles({ cursor: 'default', marginLeft: leftVal });


        // Set the modal top position (optional if hardcoded in CSS)
        if (isShortModal == 1)
            this.top = '250';
        else
            this.top = Window.getScrollTop() + (Window.getHeight() / 15);
        this.center.setStyles({ top: this.top + 'px', display: '' });



        //Show overlay
        this.overlay.setStyle('opacity', 0.7);
        this.overlayFrame.setStyle('opacity', 0.1);

        if (sHtml) this.contents.setHTML(sHtml);
        if ($(sContents)) $(sContents).injectInside(this.contents);
       if ($('mb_close_link')) {
            $('mb_close_link').remove();
        }
         if ($('mb_Login_link')) {
            $('mb_Login_link').remove();
           
        }
          if ($('mb_SignUp_link')) {
            $('mb_SignUp_link').remove();
            }
        this.closelink = new Element('a').setProperties({ id: 'mb_close_link', href: 'javascript: void(0);' }).injectInside(this.header);
        // attach the close event to the close button / the overlay
        this.closelink.onclick = this.close.bind(this);
        this.closelink.innerHTML = 'Close';
        
         this.loginlink = new Element('a').setProperties({ id: 'mb_Login_link', href: 'javascript: void(0);' }).injectInside(this.header);
        // attach the login event to the login button / the overlay
        this.loginlink.click = UserLoginPopup();
        this.loginlink.innerHTML = '';
        
          this.SignUplink = new Element('a').setProperties({ id: 'mb_SignUp_link', href: 'javascript: void(0);' }).injectInside(this.header);
        // attach the Signup event to the Signup button / the overlay
        this.SignUplink.click = UserSignUpPopup();
        this.SignUplink.innerHTML = '';
        
        this.position();

        //hide page contents for printing the modal only
        //must have .yt-NoPrint {display:none} in CSS
        //no print style is removed on modal close
        document.forms[0].getChildren().each(function(el) {
            if (el.id != "mb_center3") {
                el.addClass('yt-NoPrint');
            }
        });
        this.closelink.addEvent('click', function() {
            document.forms[0].getChildren().each(function(el) {
                el.removeClass('yt-NoPrint');
            });
        });
        
         this.loginlink.addEvent('click', function() {
            document.forms[0].getChildren().each(function(el) {
                 if (el.id != "mb_center3") {
                el.addClass('yt-NoPrint');
            }
            });
        });
        
          this.SignUplink.addEvent('click', function() {
            document.forms[0].getChildren().each(function(el) {
                 if (el.id != "mb_center3") {
                el.addClass('yt-NoPrint');
            }
            });
        });

    },


    close: function() {
        this.center.style.display = 'none';
        this.overlay.setStyle('opacity', 0);
        this.overlayFrame.setStyle('opacity', 0);
        this.overlayFrame.setStyle('height', '1px');
        //this.setup(false);  //re-enable dropdowns, objects

        if ($$('.mb-TributeVideoModal #yt-flashcontent')) {
            $$('.mb-TributeVideoModal #yt-flashcontent').setHTML('');  //clear any videos that might be loaded
        }        
        $('mb_contents3').innerHTML = "";
        
   
       
        return false;
    }


};

/* Added By Ashu on Sept 19, 2011 for Resize Image Cropper */

// The customModalBox object in its beauty
var customModalBox4 = {
    // init the customModalBox
    init: function(options) {


        // scan anchors for those opening a customModalBox
        this.anchors = [];
        $A($$('a')).each(function(el) {
            // we use a regexp to check for links that 
            // have a rel attribute starting with "modalbox"
            if (el.rel && el.href && el.rel.test('^modalbox', 'i')) {
                el.onclick = this.click.pass(el, this);
                this.anchors.push(el);
            }
        }, this);

        // add event listeners
        this.eventPosition = this.position.bind(this);


        // init the HTML elements
        // the overlay (clickable to close)
        //this.varInjectInside = document.body;

        this.overlay = new Element('div').setProperty('id', 'mb_overlay_ImgCropper').injectInside($('divShowModalPopup'));
        this.overlayFrame = new Element('iframe').setProperties({ 'id': 'mb_frame_ImgCropper', 'scrolling': 'no', 'frameborder': '0' }).injectBefore(this.overlay);

        this.overlayFrame.setStyle('height', '1px');

        // the center element
        this.center = new Element('div').setProperty('id', 'mb_center_ImgCropper').setStyle('display', 'none').injectTop(document.forms[0]);
        this.header = new Element('div').setProperty('id', 'mb_header_ImgCropper').injectInside(this.center);


        // the actual page contents
        this.contents = new Element('div').setProperty('id', 'mb_contents_ImgCropper').injectInside(this.center);



        //this.caption = new Element('div').setProperty('id', 'mb_caption').injectInside(this.center);

        // This block may be unneccessary
        this.center.onclick = '';
        //this.sTop = '0px';
        this.isShortModal = 0;

    },


    position: function() {
        this.overlay.setStyles({ top: '0px', height: Window.getScrollHeight() + 'px' });
        this.overlayFrame.setStyles({ top: '0px', height: Window.getScrollHeight() + 'px' });
    },

    setup: function(open) {
        var elements = $A($$('object'));
        elements.extend($$(window.ActiveXObject ? 'select' : 'embed'));
        elements.each(function(el) { el.style.visibility = open ? 'hidden' : ''; });
        var fn = open ? 'addEvent' : 'removeEvent';
        //window[fn]('scroll', this.eventPosition)[fn]('resize', this.eventPosition);
    },



    htmlBox4: function(sContents, sHtml, sLinkTitle, isShortModal) {
        this.title = sLinkTitle;

        // create a unique classname based on passed title 
        // for outer container so we can style box with css
        // name format is "mb-{title}Modal"
        this.center.className = 'mb-' + this.title.replace(/ /g, '') + 'Modal_ImgCropper';
        // set the box centered (optional if coded in CSS)
        if ((parseInt(this.center.getStyle('width')) / 2) > 0) {
            var leftVal = '-' + (parseInt(this.center.getStyle('width')) / 2) + 'px';
        } else {
            var leftVal = (Math.abs(parseInt(this.center.getStyle('width')) / 2)) + 'px';
        }
        this.center.setStyles({ cursor: 'default', marginLeft: leftVal });


        // Set the modal top position (optional if hardcoded in CSS)
        if (isShortModal == 1)
            this.top = '250';
        else
            this.top = Window.getScrollTop() + (Window.getHeight() / 15);
        this.center.setStyles({ top: this.top + 'px', display: '' });



        //Show overlay
        this.overlay.setStyle('opacity', 0.7);
        this.overlayFrame.setStyle('opacity', 0.1);

        if (sHtml) this.contents.setHTML(sHtml);
        if ($(sContents)) $(sContents).injectInside(this.contents);
        if ($('mb_close_link')) {
            $('mb_close_link').remove();
        }
        this.closelink = new Element('a').setProperties({ id: 'mb_close_link', href: 'javascript: void(0);' }).injectInside(this.header);
        // attach the close event to the close button / the overlay
        this.closelink.onclick = this.close.bind(this);
        this.closelink.innerHTML = 'Close';


        this.position();

        //hide page contents for printing the modal only
        //must have .yt-NoPrint {display:none} in CSS
        //no print style is removed on modal close
        document.forms[0].getChildren().each(function(el) {
            if (el.id != "mb_center_ImgCropper") {
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
        this.overlay.setStyle('opacity', 0);
        this.overlayFrame.setStyle('opacity', 0);
        this.overlayFrame.setStyle('height', '1px');
        //this.setup(false);  //re-enable dropdowns, objects

        if ($$('.mb-TributeVideoModal #yt-flashcontent')) {
            $$('.mb-TributeVideoModal #yt-flashcontent').setHTML('');  //clear any videos that might be loaded
        }
        //  $('mb_contents').innerHTML = "";



        return false;
    }

};
/* Till here */

// startup
Window.onDomReady(customModalBox.init.bind(customModalBox));
Window.onDomReady(customModalBox1.init.bind(customModalBox1));
Window.onDomReady(customModalBox2.init.bind(customModalBox2));
Window.onDomReady(customModalBox3.init.bind(customModalBox3));
Window.onDomReady(customModalBox4.init.bind(customModalBox4));





// Functions governing various modal boxes used throughout site



function modalClose() {		
		if($('mb_Title')) $('mb_Title').remove();
		if($('mb_Error')) $('mb_Error').remove();
		if($('mb_header')) $('mb_header').removeClass('yt-Panel-Primary');

		$('mb_center').style.display = 'none';
		
		$('mb_contents').getChildren()[0].remove();
		$('mb_overlay').setStyle('opacity',0);
		$('mb_frame').setStyle('opacity',0);
		window.location.reload(true); 
}
function modalCloseRedirect(url) {		
		if($('mb_Title')) $('mb_Title').remove();
		if($('mb_Error')) $('mb_Error').remove();
		if($('mb_header')) $('mb_header').removeClass('yt-Panel-Primary');

		$('mb_center').style.display = 'none';
		
		$('mb_contents').getChildren()[0].remove();
		$('mb_overlay').setStyle('opacity',0);
		$('mb_frame').setStyle('opacity',0);
		window.location=url; 
}


function ImageCroppermodalClose() {
    if ($('mb_Title_ImgCropper')) $('mb_Title_ImgCropper').remove();
    if ($('mb_Error_ImgCropper')) $('mb_Error_ImgCropper').remove();
    if ($('mb_header_ImgCropper')) $('mb_header_ImgCropper').removeClass('yt-Panel-Primary_ImgCropper');

    $('mb_center_ImgCropper').style.display = 'none';

    $('mb_contents_ImgCropper').getChildren()[0].remove();
    $('mb_overlay_ImgCropper').setStyle('opacity', 0);
    $('mb_frame_ImgCropper').setStyle('opacity', 0);
}


function modalClose_() {		
		if($('mb_Title_Popup')) $('mb_Title_Popup').remove();
		if($('mb_Error_Popup')) $('mb_Error_Popup').remove();
		if($('mb_header_Popup')) $('mb_header_Popup').removeClass('yt-Panel-Primary_Popup');

		$('mb_center_Popup').style.display = 'none';
		
		$('mb_contents_Popup').getChildren()[0].remove();
		$('mb_overlay_Popup').setStyle('opacity',0);
		$('mb_frame_Popup').setStyle('opacity',0);
		//window.location.reload(true); 		
		window.location.reload(true);
}
function modalCloseLogin(){		
		if($('mb_Title_Popup')) $('mb_Title').remove();
		if($('mb_Error_Popup')) $('mb_Error').remove();
		if($('mb_header_Popup')) $('mb_header').removeClass('yt-Panel-Primary_Popup');

		$('mb_center_Popup').style.display = 'none';
		//Changed by rupendra to remove error on 26jul2011
		if($('mb_contents_Popup').getChildren()[0])
		$('mb_contents_Popup').getChildren()[0].remove();
		$('mb_overlay_Popup').setStyle('opacity',0);
		$('mb_frame_Popup').setStyle('opacity',0);
		
		window.location.reload(true); 		
}

function setLocation(locatin)
{
        window.location=locatin;
        //window.reload();//COMDIFFRES: (this is commented to fix a defect in .com site by ankush)uncomment this
        //window.location.href="http://home.netscape.com/";
}


/*------------------------------------------------------------

Generates modal window with Tribute Video displayed

------------------------------------------------------------*/


function PopupVideo(swfsrc) {
	customModalBox.htmlBox('', '<div id="yt-flashcontent">Video flash here</div>', 'TributeVideo');
	var so = new SWFObject(swfsrc, 'yt-TributeVideo', '720', '480', '7', '#000000');	
	so.addParam("wmode", "transparent");
	so.write("yt-flashcontent");
}


/*------------------------------------------------------------

Generates modal window with photo slide show displayed

------------------------------------------------------------*/


function startSlideShow(sourceURL, startID) {

	customModalBox.htmlBox('', '<div id="yt-flashcontent">Photo show flash here</div>', 'SlideShow');

	//var so = new SWFObject('../assets/slideshows/photos/photosSlideShow.swf', 'yt-SlideShow', '512', '512', '7', '#000000');	//COMDIFFRES: (Remove absolute path of .net) absolute URL with yourtribute.net
	var so = new SWFObject('http://www.yourtribute.com/assets/slideshows/photos/photosSlideShow.swf', 'yt-SlideShow', '512', '512', '7', '#000000');	//COMDIFFRES: (Remove absolute path of .net) absolute URL with yourtribute.net
	
	so.addVariable("xmlfile", EncodeUrl(sourceURL));	
	so.addVariable("startingPhotoID", startID);
	//so.addParam("wmode", "transparent");
	so.write("yt-flashcontent");
}


/*------------------------------------------------------------

Generates modal window with Gift Selection / Event image box injected

------------------------------------------------------------*/

function chooseThumb() {
    if ($('mb_contents')) {
        $('mb_contents').className="";
    }
	customModalBox.htmlBox('yt-ThumbSelection', '', 'Thumb'); 
	chooseThumbClose();
	return false;

}

function chooseEventThumb() {
	customModalBox.htmlBox('yt-ThumbSelection', '', 'EventThumb'); 
	chooseThumbClose();
}


function chooseThumbClose() {
	$('mb_close_link').addEvent('click', function() {
	if($('yt-ThumbSelection'))
		$('yt-ThumbSelection').injectInside('yt-ThumbContainer');
		
	});
	  
}



/*------------------------------------------------------------

Generates modal window with Contact Info (to be passed in via AJAX)

------------------------------------------------------------*/

function UserProfileModal() {		
	customModalBox.htmlBox('yt-UserProfileContent', '', 'UserProfile'); 
	$('mb_close_link').addEvent('click', function() {
		$('yt-UserProfileContent').injectInside(document.forms[0]);		
		if($('txtarUserProfileMsg'))
		$('txtarUserProfileMsg').value="";
		
	//	window.location.reload(true); 
	});

	
}

/*------------------------------------------------------------

Generates modal window with Login Form injected

------------------------------------------------------------*/

function doModalLogin() {
	customModalBox.htmlBox('yt-LoginContent_Popup', '', 'Log in'); 
	$('ctl00_txtLoginUsername1').focus();	
	loginClose();
}


function loginClose() {
	$('mb_close_link').addEvent('click', function() {
		$('yt-LoginContent').injectInside('yt-LoginContentContainer_Popup');
		$('ctl00_txtLoginUsername1').value = "";
		$('ctl00_txtLoginPassword1').value = "";
		$('ctl00_txtLoginEmail1').value = "";
		$('ctl00_UserContentPlaceHolder_txtEmail').focus();		
		if($('yt-LoginError_Popup'))
		{
				 // $('ctl00_PortalValidationSummary').style.visibility = 'hidden';
		}
		window.location.reload(true); 

	});
}



window.addEvent('load', function() {	
	// check to see if there is any error info posted from the login popup, if so, show modal box and error
	if($('yt-LoginError_Popup')) {
		//doModalLogin();
		$('yt-LoginError_Popup').injectBefore('mb_header_Popup');
	}
});


/*------------------------------------------------------------

Generates modal window with Contact Form injected

------------------------------------------------------------*/

function doModalContact() {
	customModalBox.htmlBox('yt-ContactContent', '', 'Contact'); 
	$('mb_close_link').addEvent('click', function() {
		$('yt-ContactContent').injectInside(document.forms[0]);
	});
}


/*------------------------------------------------------------

Generates "loading" modal window while importing contacts

------------------------------------------------------------*/

doContactImport = function(serviceName) { 
// This is a stub function to show "Importing Contacts" status modal
// Please modify as needed to post information to Hotmail, Gmail, etc.
	//var serviceName = "Hotmail";
	//When doing actual form submission you could use: var serviceName = document.forms[0].rdoContactType.value;
	var innerText = "<p>Importing contacts from " + serviceName + "</p>";
	innerText += '<img src="../assets/images/icon_loader.gif" class="yt-LoadingImage">';//COMDIFFRES: (remove absolute path and now working fine) absolute URL with yourtribute.net
	innerText = '<div class="yt-Panel-Primary">' + innerText + '</div>';
	customModalBox.htmlBox('', innerText, 'ContactLoading');
}

/*------------------------------------------------------------

Shows modal box confirming invited guests/shared tribute

------------------------------------------------------------*/

doContactSend = function() { 
	customModalBox.htmlBox('yt-ContactSendContent', '', 'ContactSend');
}

doRSVPSend = function()  {
  
    customModalBox.htmlBox('yt-RSVPSendContent', '', 'RSVPContactSend');
}


/*------------------------------------------------------------

Generates modal window with Share Tribute textarea injected

------------------------------------------------------------*/

function doModalShare() {
	customModalBox.htmlBox('yt-ShareContent', '', 'Share'); 
	$('mb_close_link').addEvent('click', function() {
		$('yt-ShareContent').injectInside($('yt-ShareContainer'));
		if($('yt-ShareError')) $('yt-ShareError').remove();
	});
}

window.addEvent('load', function() {	
	if($('yt-ShareError') && $('mb_header')) {
		//doModalShare();
		$('yt-ShareError').injectBefore('mb_header');
	}
});

function doVTModalShare() {
	customModalBox.htmlBox('yt-ShareContent', '', 'Share'); 
	$('mb_close_link').addEvent('click', function() {
		$('yt-ShareContent').injectInside($('yt-ShareContainer'));
		if($('yt-ShareError')) $('yt-ShareError').remove();
	});
}

window.addEvent('load', function() {	
	if($('yt-ShareError') && $('mb_header')) {
		//doModalShare();
		$('yt-ShareError').injectBefore('mb_header');
	}
});

/*------------------------------------------------------------

Generates modal window with Share Tribute textarea injected

------------------------------------------------------------*/

function doModalSponsor() {
	showWideRows();
	customModalBox.htmlBox('yt-SponsorContent', '', 'Sponsor');
	$('mb_close_link').addEvent('click', function() {
		$('yt-SponsorContent').injectInside($('yt-SponsorContainer'));
		//$('yt-UpgradeExtendContent').injectInside($('yt-UpgradeExtendContainer'));			
		//if($('yt-SponsorError')) $('yt-SponsorError').remove();
		window.location.reload(true); 
	});

	hideWideRows();
	
}

window.addEvent('load', function() {	
	if($('yt-SponsorError')) {
		//doModalUpgradeExtend();
		$('yt-SponsorError').injectBefore('mb_header');
	}
});


/*------------------------------------------------------------

Generates notice/error/message modal window

------------------------------------------------------------*/


function showNotice(nMessage, nBtnOkText, nBtnCancelText,application) {
	var innerText = '<div id="mb_Notice" class="yt-Panel-Primary">' + nMessage + '</div>';
	customModalBox.htmlBox('', innerText, 'Notice');

	btnContainer = new Element('div').addClass('yt-Form-Buttons').injectInside($('mb_Notice'));
	btnCancel = new Element('div').addClass('yt-Form-Delete').injectInside(btnContainer);
	btnOk = new Element('div').addClass('yt-Form-Submit').injectInside(btnContainer);
	if (application.toString().toLowerCase() == 'tribute') {
	    okLink = new Element('a').setProperties({ id: 'mb_ok', href: 'tributes.aspx' }).injectInside(btnOk);
	}
	else {
	    okLink = new Element('a').setProperties({ id: 'mb_ok', href: 'moments.aspx' }).injectInside(btnOk);
	}
	okLink.addClass("yt-Button yt-CheckButton");
	okLink.innerHTML = nBtnOkText;
	cancelLink = $('mb_close_link').setProperty('id','mb_cancel').injectInside(btnCancel);
	//cancelLink = new Element('a').setProperties({id: 'mb_cancel', href: 'javascript: void(0);'}).injectInside(btnCancel);	
	cancelLink.addClass("yt-Button yt-XButton");
	cancelLink.innerHTML = nBtnCancelText;
	buttonStyles();		
		
}

/*------------------------------------------------------------

Generates Google Map modal window

------------------------------------------------------------*/


function showMapModal(street, city, state, country) {

	//alert (street + "  "+ city+ "  "+  state+ "  "+ country);
	var innerText = '<div class="yt-Panel-Primary"><div id="yt_MapArea">&nbsp;</div></div>';
	customModalBox.htmlBox('', innerText, 'Map');

	var directionsText = '<div class="yt-Form-Field"><label for="yt_FromAddress">Get directions to this address from:</label><input type="text" class="yt-Form-Input-Long" id="yt_FromAddress" value="" /> <a href="javascript:void(0);" onclick="showDirections()" class="yt-GoButton">Go!</a></div>';

	new Element('h2').setHTML('Location').setProperty('id','mb_MapTitle').injectTop($('mb_contents').getChildren()[0]);	
	new Element('div').setHTML(directionsText).setProperty('id','yt_MapDirections').injectAfter('yt_MapArea');	
	mapInit();
	showAddress(street, city, state, country);
	showMapModalClose();
	
}

function showMapModalClose() {
	$('mb_close_link').addEvent('click', function() {
		if($('yt_MapArea')) $('yt_MapArea').remove();
		if($('mb_MapTitle')) $('mb_MapTitle').remove();
		if($('yt_FromAddress')) $('yt_FromAddress').remove();
		if($('mb_contents')) $('mb_contents').removeClass('yt-Panel-Primary');
		if($('yt_MapDirections')) $('yt_MapDirections').remove();
		
		$('mb_contents').getChildren()[0].remove();
		$('mb_overlay').setStyle('opacity',0);
		$('mb_frame').setStyle('opacity',0);
	});
}

/*------------------------------------------------------------

Generates modal window with help system injected

------------------------------------------------------------*/





/*------------------------------------------------------------

Generates image cropper modal window

------------------------------------------------------------*/

function popupCropper(newSrc) {
 customModalBox.init();
	var srcFile = "image_cropper.htm";
	if (newSrc) srcFile = newSrc;
	var cFrame = new Element('iframe').setProperties({id:"yt-CropperFrame", height:'360px', width:'600px', frameborder:"0", scrolling:"no", src:srcFile}).injectInside(document.body);
	$('yt-CropperFrame').src = srcFile;			
	customModalBox.htmlBox('yt-CropperFrame', '', 'Cropper'); 
	$('mb_contents').addClass('yt-Panel-Primary');
	new Element('div').setHTML('&nbsp;').setProperty('id','mb_Error').injectTop($('mb_center'));
	new Element('h2').setHTML('Image Cropper').setProperty('id','mb_Title').injectTop($('mb_contents'));
	cropperClose();
}

// Quick function to remove cropper object when modal is closed
// Useful for if we decide to reset the contents of the cropper tool on close
function cropperClose() {
	$('mb_close_link').addEvent('click', function() {
		if($('yt-CropperFrame')) $('yt-CropperFrame').remove();
		if($('mb_Title')) $('mb_Title').remove();
		if ($('mb_Error')) $('mb_Error').remove();
		if ($('mb_contents')) $('mb_contents').removeClass('mb_contents');
		if($('mb_header')) $('mb_header').removeClass('yt-Panel-Primary');
	});
}

/* Added By Ashu on sSept 19, 2011 for Resize Image Cropper */
function AdminpopupCropper(newSrc) {
    var srcFile = "image_cropper.htm";
    if (newSrc) srcFile = newSrc;
    var cFrame = new Element('iframe').setProperties({ id: "yt-CropperFrame", height: '500px', width: '600px', frameborder: "0", scrolling: "no", src: srcFile }).injectInside(document.body);
    $('yt-CropperFrame').src = srcFile;
    customModalBox4.htmlBox4('yt-CropperFrame', '', 'Cropper');
    $('mb_contents_ImgCropper').addClass('yt-Panel-Primary_ImgCropper');
    new Element('div').setHTML('&nbsp;').setProperty('id', 'mb_Error_ImgCropper').injectTop($('mb_center_ImgCropper'));
    new Element('h2').setHTML('Image Cropper').setProperty('id', 'mb_Title_ImgCropper').injectTop($('mb_contents_ImgCropper'));
    AdmincropperClose();
}

// Quick function to remove cropper object when modal is closed
// Useful for if we decide to reset the contents of the cropper tool on close
function AdmincropperClose() {
    $('mb_close_link').addEvent('click', function() {
        if ($('yt-CropperFrame')) $('yt-CropperFrame').remove();
        if ($('mb_Title_ImgCropper')) $('mb_Title_ImgCropper').remove();
        if ($('mb_Error_ImgCropper')) $('mb_Error_ImgCropper').remove();
        if ($('mb_contents_ImgCropper')) $('mb_contents_ImgCropper').removeClass('mb_contents_ImgCropper');
        if ($('mb_header_ImgCropper')) $('mb_header_ImgCropper').removeClass('yt-Panel-Primary_ImgCropper');
    });
}
/* Till here */
function setpopdefault()
{     
    var bol=true;
    if(event.which || event.keyCode)
    {
     if ((event.which == 13) || (event.keyCode == 13))
     {
       $('ctl00_popuplbtnSignup').focus();
       //
       bol= false;
      }
   }  
    return bol;         
}

function setpopdefault2()
{    
    var bol=true;
    if(event.which || event.keyCode)
    {
     if ((event.which == 13) || (event.keyCode == 13))
     {
       $('ctl00_popuplbtnSignup').focus();
       bol= false;
      }
   }  
    return bol;         
}

function setpopemail()
{
    $('ctl00_PortalValidationSummary').validationGroup = "popupemail";
    var bol=true;
    if(event.which || event.keyCode)
    {
     if ((event.which == 13) || (event.keyCode == 13))
     {
       $('ctl00_popuplbtnSendemail').focus();
       bol= false;
      }
   }  
    return bol;         
}

/*------------------------------------------------------------

Generates modal window with Login Form injected for Tribute Creation.

------------------------------------------------------------*/

function doModalLogin_() {
	customModalBox.htmlBox('yt-LoginContent_Popup', '', 'Log in'); 
	loginClose_();
}


function loginClose_() {
	$('mb_close_link').addEvent('click', function() {
		$('yt-LoginContent').injectInside('yt-LoginContentContainer');
		$('txtLoginUsername1').value = "";
		$('txtLoginPassword1').value = "";
		$('txtLoginEmail1').value = "";		
//		if($('yt-LoginError'))
//		{
//				 // $('ctl00_PortalValidationSummary').style.visibility = 'hidden';
//		}
		window.location.reload(true); 

	});
}


function UserProfileModal_1(guid)
    {		
	var srcFile = App_Domain+"ModelPopup/UserProfile.aspx";	
	if (guid) srcFile += '?userid=' + guid; /* sample code to append a unique user ID to page called */	
	var cFrame = new Element('iframe').setProperties({id:"yt-UserProfileContent", name:"yt-UserProfileContent", height:'500px', width:'640px', frameborder:"0", scrolling:"no"}).injectInside(document.body);
	$('yt-UserProfileContent').src = srcFile;
	customModalBox.htmlBox('yt-UserProfileContent', '', 'UserProfile'); 
	$('mb_contents').addClass('yt-Panel-Primary');
	new Element('div').setHTML('&nbsp;').setProperty('id','mb_Error').injectTop($('mb_center'));
	new Element('h2').setHTML('Member Profile').setProperty('id','mb_Title').injectTop($('mb_contents'));
	
	UserProfileClose();
}

function UserProfileClose() {
	$('mb_close_link').addEvent('click', function() {
		//if($('yt-UserProfileContent')) $('yt-UserProfileContent').remove();
		if($('yt-UserProfileContent')) $('yt-UserProfileContent').remove();
		if($('mb_Title')) $('mb_Title').remove();
		if($('mb_contents')) $('mb_contents').removeClass('yt-Panel-Primary');
		if($('mb_Error')) $('mb_Error').remove();
	});
}


/* Done changes by Ashu for new mock up */

function UserLoginModalpopup(guid,title) {
    
  //  var srcFile = App_Domain+"ModelPopup/LoginPopup.aspx";	
    var srcFile = "../ModelPopup/LoginPopup.aspx";	
	if (guid) srcFile += '?location=' + guid+'&title=' + title; /* sample code to append a unique user ID to page called */
	var cFrame = new Element('iframe').setProperties({ id: "iframe-login", name: "iframe-login", height: '420px', width: '584px', frameborder: "0", scrolling: "no" }).injectInside(document.body);

	$('iframe-login').src = srcFile;
	customModalBox1.htmlBox1('iframe-login', '', 'Log in'); 
	$('mb_contents_Popup').addClass('yt-Panel-Primary_Popup');
	new Element('div').setHTML('').setProperty('id','mb_Error_Popup').injectTop($('mb_center_Popup'));
	new Element('h2').setHTML('Log in').setProperty('id','mb_Title_Popup').injectTop($('mb_contents_Popup'));
    	
	UserProfileClose1(); 
	
}

function UserSignupModalpopupFromSubDomain(guid,title)
{
    var srcFile = "../ModelPopup/SignUpPopup.aspx"; 
 if (guid) srcFile += '?location=' + guid+'&title=' + title; /* sample code to append a unique user ID to page called */ 
    var cFrame = new Element('iframe').setProperties({id:"iframe-signup", name:"iframe-signup", height:'420px', width:'584px', frameborder:"0", scrolling:"no"}).injectInside(document.body);
 $('iframe-signup').src = srcFile;
 customModalBox1.htmlBox1('iframe-signup', '', 'Sign up'); 
 $('mb_contents_Popup').addClass('yt-Panel-Primary_Popup');
 new Element('div').setHTML('').setProperty('id','mb_Error_Popup').injectTop($('mb_center_Popup'));
 
 new Element('h2').setHTML('Sign UP').setProperty('id','mb_Title_Popup').injectTop($('mb_contents_Popup'));
   
   $('mb_center_Popup').setStyle('z-index','2005');
   $('mb_overlay_Popup').setStyle('z-index','2004');
     
 UserSignUpClose(); 
 
}

function UserSignUpClose() {
	$('mb_close_link').addEvent('click', function() {
		//if($('yt-UserProfileContent1')) $('yt-UserProfileContent1').remove();
		if($('iframe-signup')) $('iframe-signup').remove();
		if ($('mb_Title_Popup')) $('mb_Title_Popup').remove();
		if($('mb_contents_Popup')) $('mb_contents_Popup').removeClass('yt-Panel-Primary_Popup');
		if($('mb_Error_Popup')) $('mb_Error_Popup').remove();
		$('mb_overlay_Popup').setStyle('z-index','1600');
	});
}

// ---------------Made by Ashu(23june,2011) for give gift modal popup ------------------------------
var GUID;
var TITLE;
function GiveGiftModalpopup(guid,title)
{
    var srcFile = App_Domain+"ModelPopup/Popup.aspx";	
    var srcFile = "../ModelPopup/Popup.aspx";	
    GUID=guid;
    TITLE=title;
	if (guid) srcFile += '?location=' + guid+'&title=' + title +'&Type=Gift'; /* sample code to append a unique user ID to page called */	
    var cFrame = new Element('iframe').setProperties({id:"iframe-gift", name:"iframe-gift", height:'420px', width:'584px', frameborder:"0", scrolling:"no"}).injectInside(document.body);
	$('iframe-gift').src = srcFile;
	customModalBox3.htmlBox3('iframe-gift', '', 'give gift'); 
	$('mb_contents3').addClass('yt-Panel-Primary3');
	new Element('div').setHTML('').setProperty('id','mb_Error3').injectTop($('mb_center3'));
	new Element('h2').setHTML('give gift').setProperty('id','mb_Title3').injectTop($('mb_contents3'));
    	
	UserGiveGiftClose(); 
	UserLoginPopup(); 
	UserSignUpPopup();
  	
}
function UserGiveGiftClose() {

	$('mb_close_link').addEvent('click', function() {
		if($('iframe-gift')) $('iframe-gift').remove();
		if($('mb_Title3')) $('mb_Title3').remove();
		if($('mb_contents3')) $('mb_contents3').removeClass('yt-Panel-Primary3');
		if($('mb_Error3')) $('mb_Error3').remove();
		$('mb_overlay3').setStyle('z-index','1600');
	
	});
}
function modaBoxclose()
{
      if($('iframe-gift')) $('iframe-gift').remove();
      if($('mb_Title3')) $('mb_Title3').remove();
		if($('mb_Error3')) $('mb_Error3').remove();
		if($('mb_header3')) $('mb_header3').removeClass('yt-Panel-Primary3');

		$('mb_center3').style.display = 'none';
		$('mb_overlay3').setStyle('opacity',0);
		$('mb_frame3').setStyle('opacity',0);
}

function UserLoginPopup(){

	$('mb_Login_link').addEvent('click', function() {
		modaBoxclose();
		LoginModalpopup();
	});
}
function UserSignUpPopup(){

	$('mb_SignUp_link').addEvent('click', function() {
		modaBoxclose();
		UserSignupModalpopup();
	});
}

function LoginModalpopup()
{
	
    var srcFile = "../ModelPopup/LoginPopup.aspx";	
    if (GUID) srcFile += '?location=' + GUID+'&title=' + TITLE ; /* sample code to append a unique user ID to page called */	
	 var cFrame = new Element('iframe').setProperties({id:"iframe-login", name:"iframe-login", height:'420px', width:'584px', frameborder:"0", scrolling:"no"}).injectInside(document.body);
	$('iframe-login').src = srcFile;
	customModalBox1.htmlBox1('iframe-login', '', 'Log in'); 
	$('mb_contents_Popup').addClass('yt-Panel-Primary_Popup');
	new Element('div').setHTML('').setProperty('id','mb_Error_Popup').injectTop($('mb_center_Popup'));
	new Element('h2').setHTML('Log in').setProperty('id','mb_Title_Popup').injectTop($('mb_contents_Popup'));
    	
	UserProfileClose1(); 
	
}

function UserSignupModalpopup()
{
    var srcFile = "../ModelPopup/SignUpPopup.aspx"; 
if (GUID) srcFile += '?location=' + GUID+'&title=' + TITLE ; /* sample code to append a unique user ID to page called */ 
    var cFrame = new Element('iframe').setProperties({id:"iframe-signup", name:"iframe-signup", height:'420px', width:'584px', frameborder:"0", scrolling:"no"}).injectInside(document.body);
 $('iframe-signup').src = srcFile;
 customModalBox1.htmlBox1('iframe-signup', '', 'Sign up'); 
 $('mb_contents_Popup').addClass('yt-Panel-Primary_Popup');
 new Element('div').setHTML('').setProperty('id','mb_Error_Popup').injectTop($('mb_center_Popup'));
 
 new Element('h2').setHTML('Sign UP').setProperty('id','mb_Title_Popup').injectTop($('mb_contents_Popup'));
    
      $('mb_center_Popup').setStyle('z-index','2005');
   $('mb_overlay_Popup').setStyle('z-index','2004');
     
 UserSignUpClose(); 
 
}

//--------------------------- End  give gift modal popup ----------------------------------



// ---------------Made by Ashu(23june,2011) for Post Message modal popup ------------------------------

function PostMessageModalpopup(guid,title)
{

    var srcFile = "ModelPopup/Popup.aspx";	
    var srcFile = "../ModelPopup/Popup.aspx";
    GUID=guid;
    TITLE=title;	
	if (guid) srcFile += '?location=' + guid+'&title=' + title+'&Type=Message'; /* sample code to append a unique user ID to page called */	
    var cFrame = new Element('iframe').setProperties({id:"iframe-message", name:"iframe-message", height:'420px', width:'584px', frameborder:"0", scrolling:"no"}).injectInside(document.body);
	$('iframe-message').src = srcFile;
	customModalBox3.htmlBox3('iframe-message', '', 'post message'); 
	$('mb_contents3').addClass('yt-Panel-Primary3');
	new Element('div').setHTML('').setProperty('id','mb_Error3').injectTop($('mb_center3'));
	new Element('h2').setHTML('post message').setProperty('id','mb_Title3').injectTop($('mb_contents3'));
    	
	UserPostMessageClose(); 
	UserLoginPopup(); 
	UserSignUpPopup();
	
}

function UserPostMessageClose() {

	$('mb_close_link').addEvent('click', function() {
		if($('iframe-message')) $('iframe-message').remove();
		if($('mb_Title3')) $('mb_Title3').remove();
		if($('mb_contents3')) $('mb_contents3').removeClass('yt-Panel-Primary3');
		if($('mb_Error3')) $('mb_Error3').remove();
		$('mb_overlay3').setStyle('z-index','1600');
	});
}

//--------------------------- End  Post Message modal popup ----------------------------------






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

function UserLoginModalpopupFromLoginPage(guid,title)
{
    var srcFile = "../ModelPopup/LoginPopup.aspx";
    if (guid) srcFile += '?location=' + guid + '&title=' + title + '&source=Login'; /* sample code to append a unique user ID to page called */
    var cFrame = new Element('iframe').setProperties({id:"iframe-login", name:"iframe-login", height:'420px', width:'584px', frameborder:"0", scrolling:"no"}).injectInside(document.body);
	$('iframe-login').src = srcFile;
	customModalBox1.htmlBox1('iframe-login', '', 'Log in'); 
	$('mb_contents_Popup').addClass('yt-Panel-Primary_Popup');
	new Element('div').setHTML('').setProperty('id','mb_Error_Popup').injectTop($('mb_center_Popup'));
	new Element('h2').setHTML('Log in').setProperty('id','mb_Title_Popup').injectTop($('mb_contents_Popup'));
	
}

function UserProfileClose1() {
	$('mb_close_link').addEvent('click', function() {
		//if($('yt-UserProfileContent1')) $('yt-UserProfileContent1').remove();
		if($('iframe-login')) $('iframe-login').remove();
		if ($('mb_Title_Popup')) $('mb_Title_Popup').remove();
		if($('mb_contents_Popup')) $('mb_contents_Popup').removeClass('yt-Panel-Primary_Popup');
		if($('mb_Error_Popup')) $('mb_Error_Popup').remove();
		$('mb_overlay_Popup').setStyle('z-index','1600');
	});
}
/*------------------------------------------------------------

Generates modal window with Expired Tribute textarea injected

------------------------------------------------------------*/

function doModalExpired() {
	customModalBox.htmlBox('yt-ExpiredContent', '', 'Expired'); 
	$('mb_close_link').remove();
	$('mb_overlay').setStyle('opacity',0.5);
}

  window.addEvent('domready', function() {	
	if($('yt-ExpiredContent')) {
	    doModalExpired();
	}
});  



/*------------------------------------------------------------

Generates modal window for getting the acceptance for removal of existing video tribute.

------------------------------------------------------------*/

function doModalContact() {
	    customModalBox.htmlBox('yt-ContactContent', '', 'Contact'); 
	    $('mb_close_link').addEvent('click', function() {
		    $('yt-ContactContent').injectInside(document.forms[0]);
	    });
	
}


/*------------------------------------------------------------

Generates modal window for welcome message

------------------------------------------------------------*/

function showWelcome()
 {
// switched off on client request
//	var srcFile = "../ModelPopup/WelcomeModal.aspx";	//COMDIFF: prepend "../"
//	var cFrame = new Element('iframe').setProperties({id:"yt-HelpContent", height:'580px', width:'600px', frameborder:"0", scrolling:"no", src:srcFile}).injectInside(document.body);
//														
//	customModalBox.htmlBox('yt-HelpContent', '', 'Help'); 
//	$('mb_contents').addClass('yt-Panel-Primary');
//	new Element('h2').setHTML('Welcome to Your Tribute').setProperty('id','mb_Title').injectTop($('mb_contents'));
//	welcomeClose();
}

function welcomeClose() {
	$('mb_close_link').addEvent('click', function() {
		if($('yt-HelpContent')) $('yt-HelpContent').remove();		
		if($('mb_Title')) $('mb_Title').remove();
		if($('mb_contents')) $('mb_contents').removeClass('yt-Panel-Primary');
	});
}



function PopupVideoForFlashPlayer(swfpath, swfsrc) {
    var swf = swfpath + "/swfPlayer.swf";
    
    //Google Chrome Issue: Mohit Gupta
    customModalBox.htmlBox('', '<object id="photopulse" classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=10,0,0,0" width="730" height="530" align="middle"> <param name="allowScriptAccess" value="sameDomain" /> <param name="allowFullScreen" value="false" /> <param name="FlashVars" value="MovieName=' + swfsrc + '" /> <param name="movie" value="' + swf + '" /><param name="quality" value="high" /><param name="bgcolor" value="#ffffff" /><param name="wmode" value="transparent" display="none" />   <param name="scale" value="default" /> <embed src="' + swf + '" quality="high" scale="exactfit" salign="l" wmode="transparent" flashvars="MovieName=' + swfsrc + '" bgcolor="#ffffff" width="100%" height="100%" name="swfPlayer" align="middle" allowScriptAccess="sameDomain" allowFullScreen="false" type="application/x-shockwave-flash" pluginspage="http://www.adobe.com/go/getflashplayer" /> </object>', 'TributeVideo');
}




/*********LHK :for LearnMoreButton on VideoTributeDisplay paeg***********/

function LearnMoreModalPopup(guid,title)
{
    
    var srcFile = "../ModelPopup/VideoUpgrade.aspx";	
	if (guid) srcFile += '?location=' + guid+'&title=' + title; /* sample code to append a unique user ID to page called */	
    var cFrame = new Element('iframe').setProperties({id:"iframe-login", name:"iframe-login", height:'640px', width:'635px', frameborder:"0", scrolling:"no"}).injectInside(document.body);
	$('iframe-login').src = srcFile;
	customModalBox.htmlBox('iframe-login', '', 'LearnMore'); 
	$('mb_contents').addClass('yt-Panel-Primary');
	new Element('div').setHTML('&nbsp;').setProperty('id','mb_Error').injectTop($('mb_center'));
	
	new Element('h2').setHTML('Upgrade Tribute').setProperty('id','mb_Title').injectTop($('mb_contents'));
//    $('mb_center').setStyle('z-index','2005');
//    $('mb_overlay').setStyle('z-index','2004');
    
    
    
    //  $('mb_center').setStyle('z-index','2005');
  // $('mb_overlay').setStyle('z-index','2004');
    	
	//UserProfileClose1(); 
	
}
//LHK:VideoUpgrade
function VideoUpgradeModalPopup(guid,title)
{
    
    var srcFile = "../ModelPopup/VideoUpgrade.aspx";	
	if (guid) srcFile += '?location=' + guid+'&title=' + title; /* sample code to append a unique user ID to page called */	
    var cFrame = new Element('iframe').setProperties({id:"iframe-login", name:"iframe-login", height:'640px', width:'635px', frameborder:"0", scrolling:"no"}).injectInside(document.body);
	$('iframe-login').src = srcFile;
	customModalBox.htmlBox('iframe-login', '', 'LearnMore'); 
	$('mb_contents').addClass('yt-Panel-Primary');
	new Element('div').setHTML('&nbsp;').setProperty('id','mb_Error').injectTop($('mb_center'));
	
	new Element('h2').setHTML('Upgrade Tribute').setProperty('id','mb_VideoTitle').injectTop($('mb_contents'));
}

function TributeEditDetailsModalPopUp(guid,title)
{
  var srcFile = "../ModelPopup/EditPersonalDetails.aspx";	
	if (guid) srcFile += '?location=' + guid+'&title=' + title; /* sample code to append a unique user ID to page called */	
    var cFrame = new Element('iframe').setProperties({id:"iframe-EditPersonal", name:"iframe-EditPersonal", height:'520px', width:'500px', frameborder:"0", scrolling:"no"}).injectInside(document.body);
	$('iframe-EditPersonal').src = srcFile;
	customModalBox.htmlBox('iframe-EditPersonal', '', 'EditDetails'); 
	$('mb_contents').addClass('yt-Panel-Primary');
	new Element('div').setHTML('&nbsp;').setProperty('id','mb_Error').injectTop($('mb_center'));	
	new Element('h2').setHTML('Edit Personal Details').setProperty('id','mb_Title').injectTop($('mb_contents'));
 //   $('mb_center').setStyle('z-index','2005');
 //   $('mb_overlay').setStyle('z-index','2004');
    
//   $('mb_center').setStyle('left','30%');
//   $('mb_overlay').setStyle('z-index','2004');
    	
	editClose(); 
}

function editClose() {
	$('mb_close_link').addEvent('click', function() {
		if($('iframe-EditPersonal')) $('iframe-EditPersonal').remove();
		if($('mb_Title')) $('mb_Title').remove();
		if($('mb_Error')) $('mb_Error').remove();
		if($('mb_header')) $('mb_header').removeClass('yt-Panel-Primary');
	});
}


function UserLoginModalpopupFromSubDomain(guid,title)
{
    var srcFile = "../ModelPopup/LoginPopup.aspx";
    if (guid) srcFile += '?location=' + guid + '&title=' + title + '&source=Login'; /* sample code to append a unique user ID to page called */
    var cFrame = new Element('iframe').setProperties({ id: "iframe-login", name: "iframe-login", height: '420px', width: '584px', frameborder: "0", scrolling: "no" }).injectInside(document.body);

	$('iframe-login').src = srcFile;
	customModalBox1.htmlBox1('iframe-login', '', 'Log in'); 
	$('mb_contents_Popup').addClass('yt-Panel-Primary_Popup');
	new Element('div').setHTML('').setProperty('id','mb_Error_Popup').injectTop($('mb_center_Popup'));
	
	new Element('h2').setHTML('Log in').setProperty('id','mb_Title_Popup').injectTop($('mb_contents_Popup'));
//    $('mb_center').setStyle('z-index','2005');
//    $('mb_overlay').setStyle('z-index','2004');
    
    
    
      $('mb_center_Popup').setStyle('z-index','2005');
   $('mb_overlay_Popup').setStyle('z-index','2004');
    	
	UserProfileClose1(); 
	
}


function fnExpiryNoticePopup(guid,title,value,tributeId,appdomain,topHeight)
{   
    customModalBox2.init();
   
    if (value == 'NonMemo')
    {
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
	
    modalOverlay.setStyles({'top':topHeight +'px', marginLeft: marginLeftOfOverlay, left: leftOfOverlay, width:modalWidth+'px', height: modalHeight+'px'});
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
    
     document.body.style .cursor='Hand';
}

function fnDefaultStyle()
{
    
     document.body.style .cursor='Default';
}

/***********LHK:NewExpiryPopUp*************/
function fnReachLimitExpiryPopup(guid,title,value,tributeUrl,tributeId,appdomain,topHeight)
{   
    customModalBox2.init();
   if (value == 'UploadPhoto')
    {
        //var srcFile = App_Domain+"ModelPopup/ExpiryNotice.aspx";	
        var srcFile = "../ModelPopup/UploadImage.aspx"        
        if (guid) srcFile += '?location=' + guid+'&title=' + title+'&TributeId='+tributeId+'&TabName='+value; /* sample code to append a unique user ID to page called */	
        cFrame =new Element('iframe').setProperties({id:"iframe-expiry", name:"iframe-expiry", height:'1200px', width:'680px',frameborder:"0", scrolling:"no"}).injectInside($('divShowModalPopup'));
    }
   else if ((value == 'Notes')||(value == 'Events')||(value == 'Photos')||(value == 'Videos')||(value == 'UpgradeAlbum')||(value == 'UpgradePhoto'))
    {
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
	
    modalOverlay.setStyles({'top':topHeight +'px', marginLeft: marginLeftOfOverlay, left: leftOfOverlay, width:modalWidth+'px', height: modalHeight+'px'});
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
 
function change_parent_url(url)
    {
    document.location=url;
    }; 

/**********LHK:till here*****************/




