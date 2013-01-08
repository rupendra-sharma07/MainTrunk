///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Common.JavaScript.TributePortal.js
///Author          : 
///Creation Date   : 
///Description     : This file is used to perform general functions of the site
///Audit Trail     : Date of Modification  Modified By         Description


document.attachEvent("onkeydown", myrClick);
function myrClick() 
{ 
  
    switch (event.keyCode)
    {
        case 116 : 
        event.returnValue = false;
        event.keyCode = 0;
        window.status = "We have disabled F5";
        break; 
    }
}
function PageloadCheck_()
{   
   var fontsize= readCookie('Font_Size');
   if (fontsize) 
   {
	 ChangeFont(fontsize);
   }
}

function Test(x)
{
  alert(x);
}

function SaveUserName(txt){
   
    createCookie('UserInfo',txt,0);
}

function SetSize(size)
{   
   
    ChangeFont(size);
    createCookie('Font_Size',size,0);
    return false;
}

function ChangeFont(_FontSize)
{ 
   document.getElementById('mainBody').style.fontSize = _FontSize   
    //var elem = document.getElementById('aspnetForm').elements;
//        for(var i = 0; i < document.forms[0].length; i++)
//        {  
//           aspnetForm[i].style.fontSize=_FontSize;
//        }       
 }
 ///////////////-----------------------------------///////////////////
 // Cookies to maintain State of font for page
 
 function createCookie(name,value,days) 
 {
	if (days) 
	{
		var date = new Date();
		date.setTime(date.getTime()+(days*24*60*60*1000));
		var expires = "; expires="+date.toGMTString();
		alert(expires);
	}
	else var expires = "";
	document.cookie = name+"="+value+expires+"; path=/";
 }
 
 function readCookie(name) 
 {
	var nameEQ = name + "=";
	var ca = document.cookie.split(';');
	for(var i=0;i < ca.length;i++) {
		var c = ca[i];
		while (c.charAt(0)==' ') c = c.substring(1,c.length);
		if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length,c.length);
	}
	return null;
}

function eraseCookie(name) 
{
	createCookie(name,"",-1);
}

//code to set focus on textbox after postback
var clientid;
        function fnSetFocus(txtClientId)
        {
        	clientid=txtClientId;
        	setTimeout("fnFocus()",1000);
            
        }
  
        function fnFocus()
        {
            eval("document.getElementById('"+clientid+"').focus()");
        }
 



