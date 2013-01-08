
//	if ($$('.yt-ThemeSelection')) {    




function LenghtCheck(textarea,numberRemaining)
{	
		charlimit = textarea.getAttribute('rows') * textarea.getAttribute('cols');				 		
		//numberRemaining.value = charlimit - textarea.value.length;		
		if(textarea.value.length <= charlimit) 
		{
				//numberRemaining.value = charlimit - textarea.value.length;
					numberRemaining.innerText = charlimit - textarea.value.length;
				
		}
		 else 
		 {
		  textarea.value = textarea.value.substr(0, charlimit);
		 }
}

function CheckMessageLength(textarea,lblchRemaining)
{	
		lblchRemaining.value ="characters remaining.";				 		
		charlimit = textarea.rows * textarea.cols;			 		
		if(textarea.value.length <= charlimit) 
		{
				lblchRemaining.value = charlimit - textarea.value.length+" characters remaining.";				 		
		}
		 else 
		 {
		  textarea.value = textarea.value.substr(0, charlimit);
		 }
}

InitThemes = function() {
	$$('.yt-ThemeSelection input').each( function(a) {
		a.getParent().removeClass('yt-Selected');
	});
}  
UncheckThemes = function() {
	$$('.yt-ThemeSelection input').each( function(a) {
		a.getParent().removeClass('yt-Selected');
		a.checked = false;
	});
}  

HideThemes = function() {
	$$('.yt-ThemeSet').each( function(a) {
		a.removeClass('yt-ShowBox');
	});
} 
	
UncheckAddressRadios = function() {
	$$('.yt-TributeAddressSelection input').each( function(a) {
		a.checked = false;
	});
}

AddressExclusion = function() {
    $$('.yt-TributeAddressSelection input').each(function(a) {
        a.addEvent('click', function() {
            //	$('txtTributeAddress').value = "";
            $('txtTributeAddressOther').value = "";
        });
    });
    $('txtTributeAddress').addEvent('click', function() {
        UncheckAddressRadios();
        $('txtTributeAddressOther').value = "";
    });
    $('txtTributeAddressOther').addEvent('click', function() {
        UncheckAddressRadios();
        //$('txtTributeAddress').value = "";
    });
};     


function showThemeSelection(themeName) {
	var themeID = 'yt-Themes'+ themeName; 
	//var themeID='yt-Themes'+ 'yt-ThemesNewBaby'; 
	$$('.yt-ChannelSelected').addClass('yt-ShowBox');
	HideThemes();
	$$('.yt-ThemeSet').each( function(a) {
		if (a.id == themeID) {
			a.addClass('yt-ShowBox');
		}
	});
}

InitChannels = function() {
    $$('.yt-TributeChannelSelection input').each(function(a) {
        a.getParent().removeClass('yt-Selected');
    });
};


//function GenerateSuggestionRadios() {
//	var wrapperBefore = '<div class="yt-Form-Field yt-Form-Field-Radio"><input type="radio" name="rdoTributeAddress" id="rdoTributeAddressOption';
//	var wrapperMid1 = '" value="'; /* insert unique name here */
//	var wrapperMid2 = '" /><label for="rdoTributeAddressOption'; /* insert number here */
//	var wrapperMid3 = '">http://subdomain.yourtribute.in/'; /* insert unique name here too */
//	var wrapperAfter = '</label></div>';
//	var SuggestionArray = new Array();
//   
//	SuggestionArray[0] = wrapperBefore + '1' + wrapperMid1 + 'firstnamelastname2007' + wrapperMid2 + '1' + wrapperMid3 + 'firstnamelastname2007' + wrapperAfter;
//	SuggestionArray[1] = wrapperBefore + '2' + wrapperMid1 + 'firstnamelastname1969' + wrapperMid2 + '2' + wrapperMid3 + 'firstnamelastname1969' + wrapperAfter;
//	SuggestionArray[2] = wrapperBefore + '3' + wrapperMid1 + 'firstnamelastnametribute' + wrapperMid2 + '3' + wrapperMid3 + 'firstnamelastnametribute' + wrapperAfter;
//	return SuggestionArray;
//}
		

//CheckAvailabilityComplete = function(el) {
//	//var result = response.transport.responseText;
//	var result = 0;
//	var notice = $(el).getNext();
//	
//	$$('.availabilityNotice').each( function(a) {
//		a.removeClass('availabilityNotice-Loading');
//	});
//	
//	if (result == 1) {
//		notice.addClass('availabilityNotice-Available');
//		notice.innerHTML = 'Available!';
//	}
//	else {
//		notice.addClass('availabilityNotice-Unavailable');
//		notice.innerHTML = 'Unavailable';
//		//$('yt-checkAddress1').setStyle('display','none');
		//$('txtTributeAddress').disabled = true;
//		var newSuggestions = '';
//		GenerateSuggestionRadios().each( function(a) {
//			newSuggestions += a;
//		});
//		$('yt-TributeAddressSuggestions').innerHTML = newSuggestions;
//		
//		/* if address suggestion radios are clicked: */ 
//		$$('.yt-TributeAddressSelection input').each( function(a) {
//			a.addEvent('click', function () {
//				//$('txtTributeAddress').value = "";
//				$('txtTributeAddressOther').value = "";
//			});
//		});

//		$$('.yt-TributeAddressUnavailable').each( function(a) {
//			a.addClass('yt-ShowBox');
//		});
		/* ie bug fix
		$$('.yt-Form-Buttons').each( function(a) {
			a.style.position = "relative";
//		}); */
//	}
//}




// Code for coupon check

	    CheckCoupon = function(el) {
	        var couponCode = el.parentNode.getElementsByTagName('input')[0].value;
	        var notice = el.getNext();
	        
	        // ajax call made here, we'll then evaluate the output for 1 or 0 (available or not available)
	        // var myAjax = yt_Ajax(URL_GOES_HERE, {method: 'post', postBody: 'couponCode=' + couponCode, onComplete: CheckCouponComplete});
	        
	        notice.innerHTML = '';
	        notice.className = 'couponNotice';
	        notice.addClass('couponNotice-Loading');
	        
	        CheckCouponComplete(el);  // can be removed -- just here to demonstrate what delay would look like with loading gif
	    }


	CheckCouponComplete = function(el) {
		//var result = response.transport.responseText;
		var result = 0;
		var notice = $(el).getNext();
		
		$$('.couponNotice').each( function(a) {
			a.removeClass('couponNotice-Loading');
		});
		
		if (result == 1) {
			notice.addClass('couponNotice-Available');
			notice.innerHTML = 'Coupon is valid!';
		}
		else {
			notice.addClass('couponNotice-Unavailable');
			notice.innerHTML = 'This is not a valid coupon code.';
		}
	}


function viewThemeSample(linkObj) {  
   var parentDivId = linkObj.getParent().id;
   //var parentDivId = 'yt-ThemesNewBaby';
	var themeName = parentDivId.substring('3',parentDivId.length);
	//alert(themeName);
	//alert("<div class='yt-ModalWrapper'><div class='yt-Panel-Primary'><div id='yt-'"+themeName+"'_Preview'><img src='assets/images/wedding_autumn.jpg' alt='Wedding autumn'>;</div></div></div>");
   if(parentDivId =='yt-WeddingFall')
    {
        customModalBox.htmlBox('', '<div class="yt-ModalWrapper"><div class="yt-Panel-Primary"><div id="yt-'+themeName+'_Preview"><img src="assets/images/wedding_autumn.jpg" alt="Wedding autumn">;</div></div></div>', 'TributeThemePreview'); 
    }
   else if(parentDivId =='yt-WeddingDefault')
	{
        customModalBox.htmlBox('', '<div class="yt-ModalWrapper"><div class="yt-Panel-Primary"><div id="yt-'+themeName+'_Preview"><img src="assets/images/wedding_modern.jpg" alt="Wedding modern">;</div></div></div>', 'TributeThemePreview'); 
    }
    else if(parentDivId =='yt-WeddingFormal')
	{
        customModalBox.htmlBox('', '<div class="yt-ModalWrapper"><div class="yt-Panel-Primary"><div id="yt-'+themeName+'_Preview"><img src="assets/images/wedding_blacktie.jpg" alt="Wedding formal">;</div></div></div>', 'TributeThemePreview'); 
    }
    else if(parentDivId =='yt-WeddingSpring')
	{
        customModalBox.htmlBox('', '<div class="yt-ModalWrapper"><div class="yt-Panel-Primary"><div id="yt-'+themeName+'_Preview"><img src="assets/images/wedding_spring.jpg" alt="Wedding spring">;</div></div></div>', 'TributeThemePreview'); 
    }
    else if(parentDivId =='yt-WeddingTropical')
	{
        customModalBox.htmlBox('', '<div class="yt-ModalWrapper"><div class="yt-Panel-Primary"><div id="yt-'+themeName+'_Preview"><img src="assets/images/wedding_tropical.jpg" alt="Wedding tropical">;</div></div></div>', 'TributeThemePreview'); 
    }
    else if(parentDivId =='yt-WeddingWinter')
	{
        customModalBox.htmlBox('', '<div class="yt-ModalWrapper"><div class="yt-Panel-Primary"><div id="yt-'+themeName+'_Preview"><img src="assets/images/wedding_winter.jpg" alt="Wedding winter">;</div></div></div>', 'TributeThemePreview'); 
    }
    else if(parentDivId =='yt-BabyDefault')
	{
        customModalBox.htmlBox('', '<div class="yt-ModalWrapper"><div class="yt-Panel-Primary"><div id="yt-'+themeName+'_Preview"><img src="assets/images/newbaby_bundleofjoy.jpg" alt="new baby">;</div></div></div>', 'TributeThemePreview'); 
    } 
    else if(parentDivId =='yt-BabyBoy')
	{
        customModalBox.htmlBox('', '<div class="yt-ModalWrapper"><div class="yt-Panel-Primary"><div id="yt-'+themeName+'_Preview"><img src="assets/images/newbaby_boy.jpg" alt="new baby boy">;</div></div></div>', 'TributeThemePreview'); 
    } 
    else if(parentDivId =='yt-BabyGirl')
	{
        customModalBox.htmlBox('', '<div class="yt-ModalWrapper"><div class="yt-Panel-Primary"><div id="yt-'+themeName+'_Preview"><img src="assets/images/newbaby_girl.jpg" alt="new baby girl">;</div></div></div>', 'TributeThemePreview'); 
    } 
    else if(parentDivId =='yt-BirthdayDefault')
	{
        customModalBox.htmlBox('', '<div class="yt-ModalWrapper"><div class="yt-Panel-Primary"><div id="yt-'+themeName+'_Preview"><img src="assets/images/birthday_party.jpg" alt="birthday party">;</div></div></div>', 'TributeThemePreview'); 
    } 
    else if(parentDivId =='yt-BirthdayCheers')
	{
        customModalBox.htmlBox('', '<div class="yt-ModalWrapper"><div class="yt-Panel-Primary"><div id="yt-'+themeName+'_Preview"><img src="assets/images/birthday_cheers.jpg" alt="birthday cheers">;</div></div></div>', 'TributeThemePreview'); 
    } 
    else if(parentDivId =='yt-GraduationDefault')
	{
        customModalBox.htmlBox('', '<div class="yt-ModalWrapper"><div class="yt-Panel-Primary"><div id="yt-'+themeName+'_Preview"><img src="assets/images/graduation_bluered.jpg" alt="graduation default">;</div></div></div>', 'TributeThemePreview'); 
    } 
    else if(parentDivId =='yt-GraduationAlternate')
	{
        customModalBox.htmlBox('', '<div class="yt-ModalWrapper"><div class="yt-Panel-Primary"><div id="yt-'+themeName+'_Preview"><img src="assets/images/graduation_navygold.jpg" alt="graduation other">;</div></div></div>', 'TributeThemePreview'); 
    }
    else if(parentDivId =='yt-AnniversaryFall')
    {
        customModalBox.htmlBox('', '<div class="yt-ModalWrapper"><div class="yt-Panel-Primary"><div id="yt-'+themeName+'_Preview"><img src="assets/images/anniversary_autumn.jpg" alt="Anniversary autumn">;</div></div></div>', 'TributeThemePreview'); 
    }
   else if(parentDivId =='yt-AnniversaryDefault')
	{
        customModalBox.htmlBox('', '<div class="yt-ModalWrapper"><div class="yt-Panel-Primary"><div id="yt-'+themeName+'_Preview"><img src="assets/images/anniversary_celebration.jpg" alt="celebration of love">;</div></div></div>', 'TributeThemePreview'); 
    }
    else if(parentDivId =='yt-AnniversaryFormal')
	{
        customModalBox.htmlBox('', '<div class="yt-ModalWrapper"><div class="yt-Panel-Primary"><div id="yt-'+themeName+'_Preview"><img src="assets/images/anniversary_blacktie.jpg" alt="Anniversary formal">;</div></div></div>', 'TributeThemePreview'); 
    }
    else if(parentDivId =='yt-AnniversarySpring')
	{
        customModalBox.htmlBox('', '<div class="yt-ModalWrapper"><div class="yt-Panel-Primary"><div id="yt-'+themeName+'_Preview"><img src="assets/images/anniversary_spring.jpg" alt="Anniversary spring">;</div></div></div>', 'TributeThemePreview'); 
    }
    else if(parentDivId =='yt-AnniversaryTropical')
	{
        customModalBox.htmlBox('', '<div class="yt-ModalWrapper"><div class="yt-Panel-Primary"><div id="yt-'+themeName+'_Preview"><img src="assets/images/anniversary_tropical.jpg" alt="Anniversary tropical">;</div></div></div>', 'TributeThemePreview'); 
    }
    else if(parentDivId =='yt-AnniversaryWinter')
	{
        customModalBox.htmlBox('', '<div class="yt-ModalWrapper"><div class="yt-Panel-Primary"><div id="yt-'+themeName+'_Preview"><img src="assets/images/anniversary_winter.jpg" alt="Anniversary winter">;</div></div></div>', 'TributeThemePreview'); 
    }
    else if(parentDivId =='yt-MemorialDefault')
	{
        customModalBox.htmlBox('', '<div class="yt-ModalWrapper"><div class="yt-Panel-Primary"><div id="yt-'+themeName+'_Preview"><img src="assets/images/memorial_legacy.jpg" alt="memorial legacy">;</div></div></div>', 'TributeThemePreview'); 
    }
    else if(parentDivId =='yt-MemorialFlowers')
	{
        customModalBox.htmlBox('', '<div class="yt-ModalWrapper"><div class="yt-Panel-Primary"><div id="yt-'+themeName+'_Preview"><img src="assets/images/memorial_flowers.jpg" alt="memorial flowers">;</div></div></div>', 'TributeThemePreview'); 
    }
    else if(parentDivId =='yt-MemorialPatriotic')
	{
        customModalBox.htmlBox('', '<div class="yt-ModalWrapper"><div class="yt-Panel-Primary"><div id="yt-'+themeName+'_Preview"><img src="assets/images/memorial_patriotic.jpg" alt="memorial patriotic">;</div></div></div>', 'TributeThemePreview'); 
    }
    else if(parentDivId =='yt-MemorialReligious')
	{
        customModalBox.htmlBox('', '<div class="yt-ModalWrapper"><div class="yt-Panel-Primary"><div id="yt-'+themeName+'_Preview"><img src="assets/images/memorial_religious.jpg" alt="memorial religious">;</div></div></div>', 'TributeThemePreview'); 
    }
    else if(parentDivId =='yt-MemorialReligiousII')
	{
        customModalBox.htmlBox('', '<div class="yt-ModalWrapper"><div class="yt-Panel-Primary"><div id="yt-'+themeName+'_Preview"><img src="assets/images/memorial_celestial.jpg" alt="memorial celestial">;</div></div></div>', 'TributeThemePreview'); 
    }
    else if(parentDivId =='yt-MemorialSky')
	{
        customModalBox.htmlBox('', '<div class="yt-ModalWrapper"><div class="yt-Panel-Primary"><div id="yt-'+themeName+'_Preview"><img src="assets/images/memorial_sky.jpg" alt="memorial sky">;</div></div></div>', 'TributeThemePreview'); 
        //customModalBox.htmlBox('', '<div class="yt-ModalWrapper"><div class="yt-Panel-Primary"><div id="yt-'+themeName+'_Preview"><img src="assets/images/memorial_sky.jpg" alt="memorial sky">;</div></div></div>', 'TributeThemePreview'); 
    }
}

function viewThemeFolderSample(folderName) 
{ 
   customModalBox.htmlBox('', '<div class="yt-ModalWrapper"><div class="yt-Panel-Primary"><div id="yt-Sample-Preview"><img src="assets/themes/'+folderName+'/theme_sample_image.jpg" alt="MemorialFlower">;</div></div></div>', 'TributeThemePreview');   
}

function viewDonationSample(linkObj) 
{  
    customModalBox.htmlBox('', '<div class="yt-ModalWrapper"><div class="yt-Panel-Primary"><div id="yt-WeddingFall_Preview"><img src="assets/images/wedding_autumn.jpg" alt="Wedding autumn">;</div></div></div>', 'TributeThemePreview'); 
}


function uploadTributePhoto() {
    AdminpopupCropper('Modelpopup/ImageCropper.aspx');
	

}

function cancelTributeCreation(applicationName) {
    var theMessage = '<h2>Cancel ' + applicationName + ' Creation</h2><p>Are you sure that you want to cancel the creation of this ' + applicationName + '?<br />Any information that you may have entered will be lost forever...</p>';
	test = showNotice(theMessage, 'Yes, Cancel', 'No, Don\'t Cancel',applicationName);
}

function addTributeAdmin() 
{
  
	var adminCount = $$('.yt-AdminAdd').length;	
	var newDiv = new Element('div').setProperty('id', 'yt-Admin' + (adminCount+1)).addClass('yt-Form-Field').addClass('yt-AdminAdd').injectAfter('yt-Admin' + adminCount);
	var newLabel = new Element('label').setProperty('for', 'txtAdminEmail' + (adminCount+1)).injectInside(newDiv);
	var newInput = new Element('input').setProperties({'id': 'txtAdminEmail' + (adminCount+1),'name': 'txtAdminEmail' + (adminCount+1)}).addClass('yt-Form-Input-Long').injectAfter(newLabel);				
	newLabel.setHTML('Email address:');

}



// function to show Membership billing info form (step 5)

function SelectMembership(membershipType) {
	if (membershipType.value != "FreeTrial" ) {
		$('yt-BillingInfo').addClass('yt-ShowBox');
		$('yt-FreeTrialInfo').removeClass('yt-ShowBox');
		switch (membershipType.value) {
			case 'Lifetime':
				$('BillingTotal').setHTML("$50");
				break;
			case 'Yearly':
				$('BillingTotal').setHTML("$20");
				break;
		}
	} else {
		$('yt-FreeTrialInfo').addClass('yt-ShowBox');
		$('yt-BillingInfo').removeClass('yt-ShowBox');
	}
}


// function to hide wide rows in step 5 and show them with click on "DETAILS"

function hideWideRows() 
{

	$$('.yt-colWide').each( function(a) 
	{
		a.getParent().addClass('yt-HiddenRow');
	});
	$$('.yt-ButtonDetails').each( function(a) 
	{
		a.removeClass('yt-Open');
	});
}

function showDetails(theButton) 
{
	        if(theButton.hasClass('yt-Open')) 
	        {	  

		    hideWideRows();		    
	        }
	 else 
	 {
		hideWideRows();
		theButton.getParent().getParent().getNext().removeClass('yt-HiddenRow');		
		theButton.addClass('yt-Open');		
	 }
	
}

// Window loader functions for Tribute Creation

window.addEvent('load', function() 
{


	if($$('.yt-ThemeSelection')) {

		/* check to see if there are any previously selected themes and change the background to match*/
		$$('.yt-ThemeSelection input').each(function(a){
			if(a.checked) {
				a.getParent().addClass('yt-Selected');
			}
		});
		
		/* attach theme selection events */
		$$('.yt-ThemeSelection input').each(function(a){
			a.addEvent('click', function () {
				InitThemes();
				a.getParent().addClass('yt-Selected');
			});
		});
	}
		
	if($$('.yt-TributeChannelSelection')) {
		
		$$('.yt-TributeChannelSelection input').each(function(a){
			if(a.checked) {
				a.getParent().addClass('yt-Selected');
				showThemeSelection(a.value);
			}
		});
		
		/* attach theme selection events */
		$$('.yt-TributeChannelSelection input').each(function(a){
			a.addEvent('click', function () {
				InitChannels();
				UncheckThemes();
				a.getParent().addClass('yt-Selected');
				showThemeSelection(a.value);
			});
		});
	}
		
	if($('txtTributeAddress')) {
		
		/* attach events to clear unselected address options */
		
		/* if main address input is clicked: */ 
		$('txtTributeAddress').addEvent('focus', function () {
			UncheckAddressRadios();
			$('txtTributeAddressOther').value = "";
		});


		
		
		/* if other address input is clicked: */ 
		$('txtTributeAddressOther').addEvent('focus', function () {
			UncheckAddressRadios();
			//$('txtTributeAddress').value = "";
		});
	}
	
	if($('txtarMessage')) 
	{
		textarea = $('txtarMessage');	
		
		charlimit = textarea.getAttribute('rows') * textarea.getAttribute('cols');
		$('numberRemaining').innerHTML = charlimit - textarea.value.length;
		textarea.addEvent('keyup', function() 
		{		  
			if(textarea.value.length <= charlimit) {
				$('numberRemaining').innerHTML = charlimit - textarea.value.length;
			} else {
				textarea.value = textarea.value.substr(0, charlimit);
			}
		}
		);
	}
	
	if(document.getElementById('<%=txtarMessage.ClientID%>')!=null)
	{
	   textarea = document.getElementById('<%=txtarMessage.ClientID%>');		
  	   
	}
	
	
    
	
//	$$('.yt-checkCoupon').each( function(a) {
//	    /* attach ajax call to check availability button */
//		a.addEvent('click', function() {
//			CheckCoupon(this);
//		});
//		
//		var notice = a.getNext();
//		notice.innerHTML = '';
//	    notice.className = 'couponNotice';
//		
//	});
	
	hideWideRows();
	
	
});

//}