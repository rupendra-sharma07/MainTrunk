///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.assets.scripts.admin.js
///Author          : 
///Creation Date   : 
///Description     : This file is used by admin part of the website
///Audit Trail     : Date of Modification  Modified By         Description


/*------------------------------------------------------------

Generates modal window with Admin receipt injected

------------------------------------------------------------*/

function doModalAdminReceipt() {
	customModalBox.htmlBox('yt-AdminReceiptContent', '', 'AdminReceipt'); 
	
	
	$('mb_close_link').addEvent('click', function() 
	{
		$('yt-AdminReceiptContent').injectInside(document.forms[0]);
	});
}

//window.addEvent('load', function() {	
//	if($('yt-AdminReceiptContent')) {
//		//doModalAdminReceipt();         --Receipt for Admin
//	}	
////	if($('yt-UpgradeExtendContent'))
////	{
////		//doModalUpgradeExtend();
////	}   
//});


function doModalUpgradeExtend() {
	customModalBox.htmlBox('yt-UpgradeExtendContent', '', 'UpgradeExtend'); 
	$('mb_close_link').addEvent('click', function() 
	{	  
	   // window.location.reload(true);   
		$('yt-UpgradeExtendContent').injectInside($('yt-UpgradeExtendContainer'));				
		//if($('yt-UpgradeExtendError')) $('yt-UpgradeExtendError').remove();
	});
}


function Blanktextboxes()
{
if($('txtCouponCode'))
    $('txtCouponCode').value="";
if($('txtCCNumber1'))
    $('txtCCNumber1').value="";
if($('txtCCVerification1'))
    $('txtCCVerification1').value="";
if($('ddlCCMonth1'))
    $('ddlCCMonth1').selectedIndex=0;
if($('txtCCYear1'))
    $('txtCCYear1').value="";
if($('txtCCName1'))
    $('txtCCName1').value="";
if($('txtCCBillingAddress_'))
    $('txtCCBillingAddress_').value="";
if($('txtCCBillingAddress_2'))
    $('txtCCBillingAddress_2').value="";
if($('txtCCCity1'))
    $('txtCCCity1').value="";
if($('txtCCZipCode1'))
    $('txtCCZipCode1').value="";
if($('txtPhoneNumber_1'))
    $('txtPhoneNumber_1').value="";
if($('txtPhoneNumber_2'))
    $('txtPhoneNumber_2').value="";
if($('txtPhoneNumber_3'))
    $('txtPhoneNumber_3').value="";
if($('rdoNotifyBeforeRenew'))
    $('rdoNotifyBeforeRenew').checked=true;
if($('chkSaveBillingInfo'))
{
    $('chkSaveBillingInfo').checked=false;
    $('chkSaveBillingInfo').disabled=false;
}   
if($('chkAgree'))
{
    $('chkAgree').checked=true;
}  

doModalUpgradeExtend();
}


function Blanktextboxes_()
{
if($('txtCouponCode'))
    $('txtCouponCode').value="";
if($('txtCCNumber'))
    $('txtCCNumber').value="";
if($('txtCCVerification'))
    $('txtCCVerification').value="";
if($('txtCCYear'))
    $('txtCCYear').value="";
if($('txtCCName'))
    $('txtCCName').value="";
if($('txtCCBillingAddress'))
    $('txtCCBillingAddress').value="";
if($('txtCCBillingAddress2'))
    $('txtCCBillingAddress2').value="";
if($('txtCCCity'))
    $('txtCCCity').value="";
if($('txtCCZipCode'))
    $('txtCCZipCode').value="";
if($('txtPhoneNumber1'))
    $('txtPhoneNumber1').value="";
if($('txtPhoneNumber2'))
    $('txtPhoneNumber2').value="";
if($('txtPhoneNumber3'))
    $('txtPhoneNumber3').value="";
if($('rdoNotifyBeforeRenew'))
    $('rdoNotifyBeforeRenew').checked=true;
if($('chkSaveBillingInfo'))
{
    $('chkSaveBillingInfo').checked=false;
    $('chkSaveBillingInfo').disabled=false;
}   
if($('chkAgree'))
{
    $('chkAgree').checked=true;
}  

doModalUpgradeExtend();
}
// Modal for Delete boxes on Delete tribute and Administrators pages

function doModalDeleteConfirm() {

	customModalBox.htmlBox('yt-DeleteConfirmContent', '', 'DeleteConfirm'); 
	var btnCancel = $('mb_close_link');
	btnCancel.addClass("yt-Button yt-XButton").setHTML("No, don't delete").injectInside('yt-CancelContainer');
	//buttonStyles(); //apply the dropshadow effect to our new close button	//COMDIFFRES: (this change is done by rakesh to fix a defect) uncomment this
	btnCancel.addEvent('click', function() {
		
		btnCancel.setHTML("Close").injectInside($('mb_header'));//COMDIFFRES: (this change is done by rakesh to fix a defect) comment this
	});
}

function doModalDeleteConfirm_() {
	customModalBox.htmlBox('yt-DeleteConfirmContent_', '', 'DeleteConfirm'); 
	var btnCancel = $('mb_close_link');
	btnCancel.addClass("yt-Button yt-XButton").setHTML("No, don't delete").injectInside('yt-CancelContainer_');
	buttonStyles(); //apply the dropshadow effect to our new close button	
	btnCancel.addEvent('click', function() {
		//btnCancel.setHTML("Close").injectInside($('mb_header'));
	});
}


function doModalDonationDeleteConfirm() {
	customModalBox.htmlBox('yt-DonationDeleteConfirmContent', '', 'DeleteConfirm'); 
	var btnCancel = $('mb_close_link');
	btnCancel.addClass("yt-Button yt-XButton").setHTML("No, don't delete").injectInside('yt-CancelContainerDonation');
	//buttonStyles(); //apply the dropshadow effect to our new close button	
	btnCancel.addEvent('click', function() {
		btnCancel.setHTML("Close").injectInside($('mb_header'));
	});
}


// Modal for Confirm auto-renewal box upgrade/extend pages

function doModalAutoRenew() {
	customModalBox.htmlBox('yt-AutoRenewContent', '', 'AutoRenew'); 
	var btnCancel = $('mb_close_link');
	btnCancel.addClass("yt-Button yt-XButton").setHTML("Cancel").injectInside('yt-CancelContainer_AR');
	buttonStyles(); //apply the dropshadow effect to our new close button	
	btnCancel.addEvent('click', function() {
		//btnCancel.setHTML("Close").injectInside($('mb_header'));
	});

}



// Modal Print button function
function printModal() {
	window.print();
}


// Functions for pages with billing forms and option tables:

function hideWideRows() {
	$$('.yt-colWide').each( function(a) {
		a.getParent().addClass('yt-HiddenRow');
	});
	$$('.yt-ButtonDetails').each( function(a) {
		a.removeClass('yt-Open');
	});
}
function showWideRows() {
	$$('.yt-colWide').each( function(a) {
		a.getParent().removeClass('yt-HiddenRow');
	});
	$$('.yt-ButtonDetails').each( function(a) {
		a.addClass('yt-Open');
	});
}

function showDetails(theButton) {
	if(theButton.hasClass('yt-Open')) {
		hideWideRows();
	} else {
		hideWideRows();
		theButton.getParent().getParent().getNext().removeClass('yt-HiddenRow');
		theButton.addClass('yt-Open');
	}
}


function SelectMembership(membershipType) {


	switch (membershipType.value) 
	{
//	$('txtCouponCode').value="";
//           $('spanCoupon').innerHTML="";
//           $('spanCoupon').className = 'availabilityNotice';
		case 'Lifetime':
			$('BillingTotal').setHTML("$50");
			if ($('yt-MembershipType')) $('yt-MembershipType').innerHTML = 'Lifetime';

			break;
		case 'Yearly':
			$('BillingTotal').setHTML("$20");
			if ($('yt-MembershipType')) $('yt-MembershipType').innerHTML = '1 Year';
			break;
	}
}

window.addEvent('load', function() {	
	if($('yt-UpgradeExtendError')) {
		//doModalUpgradeExtend();
		$('yt-UpgradeExtendError').injectBefore('mb_header');
	}
});


//window.addEvent('load', function() {	
//	if($('yt-UpgradeExtendError_')) 
//	{
//		$('yt-UpgradeExtendError_').injectBefore('mb_header');
//	}
//});


// Image cropper for User Profile photo

//function uploadUserPhoto1() {
//	popupCropper_('../ModelPopup/ModelPopup.aspx');
//}

//function uploadUserPhoto() {
//	popupCropper('AdminImageCropper.aspx');
//}

// JavaScript Document