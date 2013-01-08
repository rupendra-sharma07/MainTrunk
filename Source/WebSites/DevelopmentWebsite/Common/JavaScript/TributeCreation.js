///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Common.JavaScript.TributeCreation.js
///Author          : 
///Creation Date   : 
///Description     : This file is used to perform checks while tribute creation
///Audit Trail     : Date of Modification  Modified By         Description

function CreditCardCLength(CCNumber)
{
    var val=true;
    if(CCNumber)
    {
        if(CCNumber.value.length>0)
        {
         val=isInteger(CCNumber.value);
         if(val==true)
         {
             if((CCNumber.value.length > 12)&&(CCNumber.value.length  < 18))
             {
                val=true;
             }
             else
             {
              val=false;
             }
         }
         else
         {
           val=false;
         }
       }
    }
 return val;
}
function CVCLengthCC(CCNumber)
{
var val=true;
    if(CCNumber)
    {
        if(CCNumber.value.length>0)
        {
          val=isInteger(CCNumber.value);
          if(val==true)
          {
             if((CCNumber.value.length == 3)||(CCNumber.value.length == 4))
             {
                val=true;
             }
             else
             {
              val=false;
             }
         }
         else
         {
           val=false;
         }
       }
    }
return val;
}
function CCLength(source, args)
{
 var ccNumber=$('ctl00_TributePlaceHolder_txtCCNumber');
    if(ccNumber.value.length==16)
    args.IsValid=true;
    else
    args.IsValid=false;
}  

function SelectAccountType(source, args)
 {
           var rdb1=$('ctl00_TributePlaceHolder_rdoMembershipYearly');
           var rdb2=$('ctl00_TributePlaceHolder_rdoMembershipLifetime');
           var rdb3=$('ctl00_TributePlaceHolder_rdoMembershipFree');
           if((!rdb1.checked)&&(!rdb2.checked)&&(!rdb3.checked))
           args.IsValid=false;
           else
           args.IsValid=true;
 }

function TributeNameValidate(s)
{    
    var i;
    var bol=true;
    for (i = 0; i < s.length; i++)
    {   
        // Check that current character is number.        
        var c = s.charAt(i);             
        if((c=="*")||(c=="?"))         
         bol = false;        
      }
     return bol;
    // All characters are OK.     
}




function UniqueRadioButton(nameregex, current) 
{
   // var t1=document.getElementById(tableid);
    re = new RegExp(nameregex);
    for(i = 0; i < document.forms[0].elements.length; i++) 
    {
        elm = document.forms[0].elements[i]
        if (elm.type == 'radio') 
        {
            if (re.test(elm.name)) 
            {     
                elm.checked = false;                 
            }
        }
        
    }          
    current.checked = true;
   // t1.style.backgroundColor="#ebe2dc"; 
}

function ValidatePrivacy(rdoPrivate, rdoPublic)
 {
        if((!rdoPrivate.checked)&&(!rdoPublic.checked))
            return false;             
        else
            return true;
        
 }
 
 function AddressValidate(addrs1,addrs2,addrs3,addrs4)
 {
            if(
              (!addrs1.checked)&&
              (!addrs2.checked)&&
              (!addrs3.checked)&&
              (addrs4.value.length==0)              
              )    
              {               
               return false;
              }
              else
              {
               return true;
              }
  
 
 }

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


	        function ValidatorVisibility(val)
	        {
	            var txt= document.getElementById('ctl00_UserContentPlaceHolder_txtFirstName');       
	            txt.disabled=val; 
	            //              //validat.style.visibility = 'hidden';
	        }	        
    function IsNumeric()
    {
//      var key = window.event.keyCode;
//      if(isDigit(key));
//      
//       window.event.returnValue = 0; 
    }
     function isDigit (k) 
     {       
        return ((k >= 48) ||(k <= 57)) 
     }
    function SpecialCharacter(k)
    {

    return ((k==95)||(k==46))
    }
    
    function isCharacter(k) 
    {   
    return ((k>=97)&&(k<=122)||(k>=65)&&(k<=90))
    }

function CheckUsernameLength(UserName)
{
    var bol=true;
    if((UserName.length>=4)&&(UserName.length<=16))                   
    { 
       bol= true;
    }
    else 
    {
       bol= false;    
    }
   return bol;            
}

function CheckPasswordLength(Password)
{   
  var bol=true;       
    if(Password.length!=0)
    {
        if((Password.length>=6)&&(Password.length<=50))               
           bol= true;             
        else 
          bol= false;               
    }
    else
    {
     bol=true;
    }
    return bol;
}	 


//Function to check the max length of text.
//if text length is greater than maxLength return false else return true.
function chkForMaxLength(maximumLength, textLength)
{  
    if (textLength <= maximumLength)
        return true;
    else
        return false;
}
///Date Validation
function NewBabyValidate(ddlmonth,ddlDay,txtYear,cvDate,Tribute)
{
    var bol=true;    
    if((ddlmonth.value!=" ")&&(ddlDay.value!=" ")&&(txtYear.value.length!=0))    
      {
                bol= ValidDate(ddlmonth,ddlDay,txtYear,cvDate,"Due date is required field.");   
                if( bol== false)
                {
                 cvDate.errormessage = "Please enter a valid due date.";
                } 
      } 
    if((ddlmonth.value==" ")&&(ddlDay.value==" ")&&(txtYear.value.length==0))    
    { 
        bol= true;	                       
    }        
       if((ddlmonth.value!=" ")&&(ddlDay.value!=" ")&&(txtYear.value.length==0))    
        {  
        bol= false;	                 
        cvDate.errormessage = "Please enter a valid due date.";
        }  
        if((ddlmonth.value==" ")&&(ddlDay.value!=" ")&&(txtYear.value.length!=0))    
        {  
        bol= false;	                 
       cvDate.errormessage = "Please enter a valid due date.";
        }                 
        if((ddlmonth.value==" ")&&(ddlDay.value!=" ")&&(txtYear.value.length==0))    
        {  
        bol= false;	                 
        cvDate.errormessage = "Please enter a valid due date.";
        }
        if((ddlmonth.value==" ")&&(ddlDay.value!=" ")&&(txtYear.value.length!=0))    
        {  
        bol= false;	                 
       cvDate.errormessage = "Please enter a valid due date.";
        }
        if((ddlmonth.value==" ")&&(ddlDay.value==" ")&&(txtYear.value.length!=0))    
        {  
        bol= false;	                 
       cvDate.errormessage = "Please enter a valid due date.";
        }
        if((ddlmonth.value!=" ")&&(ddlDay.value==" ")&&(txtYear.value.length==0))    
        {  
        bol= false;	                 
        cvDate.errormessage = "Please enter a valid due date.";
        }
        if((ddlmonth.value!=" ")&&(ddlDay.value==" ")&&(txtYear.value.length!=0))    
        {  
        bol= false;	                 
        cvDate.errormessage = "Please enter a valid due date.";
        }
        if((ddlmonth.value!=" ")&&(ddlDay.value==" ")&&(txtYear.value.length==0))    
        {  
        bol= false;	                 
       cvDate.errormessage = "Please enter a valid due date.";
        }
        if((ddlmonth.value==" ")&&(ddlDay.value!=" ")&&(txtYear.value.length==0))    
        {  
        bol= false;	                 
        cvDate.errormessage = "Please enter a valid due date.";
        }     
    return bol;  
}
                         
function ValidateBothDate(ddlmonth,ddlDay,txtYear,ddlmonth2,ddlDay2,txtYear2,cvDate,Tribute)
{
       var bol=true;          
       if(Tribute=="New Baby")
       {
           if((ddlmonth.value==" ")&&(ddlDay.value==" ")&&(txtYear.value.length==0)&&(ddlmonth2.value==" ")&&(ddlDay2.value==" ")&&(txtYear2.value.length==0))
           {                
                cvDate.errormessage = "Date of Birth or a Due Date is required.";	
                bol= false;
           } 
                if((ddlmonth.value!=" ")&&(ddlDay.value!=" ")&&(txtYear.value.length!=0))    
                {
                bol= ValidDate(ddlmonth,ddlDay,txtYear,cvDate,"Date of birth is required field.");   
                if(bol==false) 
                    {
                       cvDate.errormessage = "Please enter a valid birth date.";	
                    }
                }
                if((ddlmonth.value!=" ")&&(ddlDay.value!=" ")&&(txtYear.value.length==0))    
                {  
                bol= false;	                 
                cvDate.errormessage = "Please enter a valid birth date.";	
                }  
                if((ddlmonth.value==" ")&&(ddlDay.value!=" ")&&(txtYear.value.length!=0))    
                {  
                bol= false;	                 
                cvDate.errormessage = "Please enter a valid birth date.";	
                }                 
                if((ddlmonth.value==" ")&&(ddlDay.value!=" ")&&(txtYear.value.length==0))    
                {  
                bol= false;	                 
               cvDate.errormessage = "Please enter a valid birth date.";	
                }
                if((ddlmonth.value==" ")&&(ddlDay.value!=" ")&&(txtYear.value.length!=0))    
                {  
                bol= false;	                 
               cvDate.errormessage = "Please enter a valid birth date.";	
                }
                if((ddlmonth.value==" ")&&(ddlDay.value==" ")&&(txtYear.value.length!=0))    
                {  
                bol= false;	                 
               cvDate.errormessage = "Please enter a valid birth date.";	
                }
                if((ddlmonth.value!=" ")&&(ddlDay.value==" ")&&(txtYear.value.length==0))    
                {  
                bol= false;	                 
               cvDate.errormessage = "Please enter a valid birth date.";	
                }
                if((ddlmonth.value!=" ")&&(ddlDay.value==" ")&&(txtYear.value.length!=0))    
                {  
                bol= false;	                 
               cvDate.errormessage = "Please enter a valid birth date.";	
                }
                if((ddlmonth.value!=" ")&&(ddlDay.value==" ")&&(txtYear.value.length==0))    
                {  
                bol= false;	                 
                cvDate.errormessage = "Please enter a valid birth date.";	
                }
                if((ddlmonth.value==" ")&&(ddlDay.value!=" ")&&(txtYear.value.length==0))    
                {  
                bol= false;	                 
                cvDate.errormessage = "Please enter a valid birth date.";	
                }     
       }
         
     
      return bol;
}

var dtCh= "/";
var minYear=1800;
var maxYear=9999;

function Validate()
{

}


function DateValidation2(ddlmonth,ddlDay,txtYear,ddlmonth2,ddlDay2,txtYear2,cvDate,Tribute)
{
    var bol=true;          
       if(Tribute=="Memorial")
       {
           if((ddlmonth.value==" ")&&(ddlDay.value==" ")&&(txtYear.value.length==0)&&(ddlmonth2.value==" ")&&(ddlDay2.value==" ")&&(txtYear2.value.length==0))
           {
                cvDate.errormessage = "Date of Death is a required field.";	
                bol= false;
           }  
           else   if((ddlmonth2.value==" ")&&(ddlDay2.value==" ")&&(txtYear2.value.length==0))
           {
                cvDate.errormessage = "Date of Death is a required field.";	
                bol= false;
           }  
           
          else
          {
            
                if((ddlmonth2.value==" ")&&(ddlDay2.value==" ")&&(txtYear2.value.length==0))              
                {
                cvDate.errormessage = "Please enter a valid death date.";	
                bol= false;
                }
                if((ddlmonth2.value==" ")&&(ddlDay2.value!=" ")&&(txtYear2.value.length!=0))                  
                {
                cvDate.errormessage = "Please enter a valid death date.";	
                bol= false;
                }
                if((ddlmonth2.value==" ")&&(ddlDay2.value!=" ")&&(txtYear2.value.length==0))    
                {
                cvDate.errormessage = "Please enter a valid death date.";	
                bol= false;
                }                 
                if((ddlmonth2.value==" ")&&(ddlDay2.value!=" ")&&(txtYear2.value.length!=0))    
                {
                cvDate.errormessage = "Please enter a valid death date.";	
                bol= false;
                }	                 
                if((ddlmonth2.value==" ")&&(ddlDay2.value==" ")&&(txtYear2.value.length!=0))    
                {
                cvDate.errormessage = "Please enter a valid death date.";	
                bol= false;
                }	                 
                if((ddlmonth2.value!=" ")&&(ddlDay2.value==" ")&&(txtYear2.value.length==0))    
                {
                cvDate.errormessage = "Please enter a valid death date.";	
                bol= false;
                }	                                               
                if((ddlmonth2.value!=" ")&&(ddlDay2.value==" ")&&(txtYear2.value.length!=0))    
                {
                cvDate.errormessage = "Please enter a valid death date.";		
                bol= false;
                }	                 
                if((ddlmonth2.value!=" ")&&(ddlDay2.value==" ")&&(txtYear2.value.length==0))    
                {
                cvDate.errormessage = "Please enter a valid death date.";	
                bol= false;
                }	                 
                if((ddlmonth2.value==" ")&&(ddlDay2.value!=" ")&&(txtYear2.value.length==0))    
                {
                cvDate.errormessage = "Please enter a valid death date.";	
                bol= false;
                }	                 
                if((ddlmonth2.value!=" ")&&(ddlDay2.value!=" ")&&(txtYear2.value.length==0))    
                {
                cvDate.errormessage = "Please enter a valid death date.";	
                bol= false;
                }	                 	                             
              else if((ddlmonth2.value!=" ")&&(ddlDay2.value!=" ")&&(txtYear2.value.length!=0))          
              {
               var dt=ddlmonth2.value+"/"+ddlDay2.value+"/"+txtYear2.value;         
	           if (isDate(dt,cvDate)==false)
	           {
               cvDate.errormessage = "Please enter a valid death date.";	
	           bol= false;
	           }
	           else	        
               bol= true;
              } 
            
          }
      
     } 
      return bol;
}



function DateValidation(ddlmonth,ddlDay,txtYear,cvDate,Tribute)
{
   var bol=true;
   
    switch (Tribute)
    {
        case "Birthday":                   
            bol= ValidBirthDate(ddlmonth,ddlDay,txtYear,cvDate,"Date of Birth is a required field.");             
         //  if(bol==false) 
          //  {cvDate.errormessage = "Please enter a valid birth date.";}
            break;
        case "Graduation":                                
                if ((ddlmonth.value == " ") && (ddlDay.value ==" ") && (txtYear.value == ""))
                {
                    cvDate.errormessage = "Graduation date is a required field.";	
                    bol = false;
                }
                else
                {
                    bol= ValidDate(ddlmonth,ddlDay,txtYear,cvDate,"Graduation date is a required field.");     
                    if(bol==false)        
                    {
                         cvDate.errormessage = "Please enter a valid graduation date.";	
                    }
                }
                break;  
        case "Wedding":   
                if ((ddlmonth.value == " ") && (ddlDay.value ==" ") && (txtYear.value == ""))
                {
                    cvDate.errormessage = "Wedding date is a required field.";	
                    bol = false;
                }
                else
                {
                    bol= ValidDate(ddlmonth,ddlDay,txtYear,cvDate,"Wedding date is a required field.");     
                    if(bol==false)        
                    {
                         cvDate.errormessage = "Please enter a valid wedding date.";	
                    }
                }
                break;       
//            bol= ValidDate(ddlmonth,ddlDay,txtYear,cvDate,"Wedding date is a required field.");   
//            if(bol==false) 
//            {cvDate.errormessage = "Please enter a valid wedding date.";}
//            break;
        case "Anniversary":    
        if ((ddlmonth.value == " ") && (ddlDay.value ==" ") && (txtYear.value == ""))
                {
                    cvDate.errormessage = "Anniversary date is a required field.";	
                    bol = false;
                }
                else
                {
                    bol= ValidDate(ddlmonth,ddlDay,txtYear,cvDate,"Anniversary date is a required field.");     
                    if(bol==false)        
                    {
                         cvDate.errormessage = "Please enter a valid anniversary date.";	
                    }
                }
                break;       
//        bol= ValidDate(ddlmonth,ddlDay,txtYear,cvDate,"Anniversary date is a required field.");    
//        if(bol==false) 
//        {cvDate.errormessage = "Please enter a valid anniversary date.";}
//        break;  
        case "Memorial":                          
              if((ddlmonth.value!=" ")&&(ddlDay.value!=" ")&&(txtYear.value.length!=0))          
              {
               var dt=ddlmonth.value+"/"+ddlDay.value+"/"+txtYear.value;         
	           if (isDate(dt,cvDate)==false)
	           {
	            cvDate.errormessage="Please enter a valid birth date.";  
	            bol= false;	           
	           }	           	       
	           else	        
               bol= true;
              }
              if((ddlmonth.value==" ")&&(ddlDay.value==" ")&&(txtYear.value.length==0))    
              bol= true;	                 
                if((ddlmonth.value==" ")&&(ddlDay.value!=" ")&&(txtYear.value.length!=0))    
                {
                cvDate.errormessage = "Please enter a valid birth date.";	
                bol= false;
                }                   
                if((ddlmonth.value==" ")&&(ddlDay.value!=" ")&&(txtYear.value.length==0))    
                {
                cvDate.errormessage = "Please enter a valid birth date.";	
                bol= false;
                }                 
                if((ddlmonth.value==" ")&&(ddlDay.value!=" ")&&(txtYear.value.length!=0))    
                {
                cvDate.errormessage = "Please enter a valid birth date.";	
                bol= false;
                }                 
                if((ddlmonth.value==" ")&&(ddlDay.value==" ")&&(txtYear.value.length!=0))    
                {
                cvDate.errormessage = "Please enter a valid birth date.";	
                bol= false;
                }                
                if((ddlmonth.value!=" ")&&(ddlDay.value==" ")&&(txtYear.value.length==0))    
                {
                cvDate.errormessage = "Please enter a valid birth date.";	
                bol= false;
                }                                               
                if((ddlmonth.value!=" ")&&(ddlDay.value==" ")&&(txtYear.value.length!=0))    
                {
                cvDate.errormessage = "Please enter a valid birth date.";	
                bol= false;
                }               
                if((ddlmonth.value!=" ")&&(ddlDay.value==" ")&&(txtYear.value.length==0))    
                {
                cvDate.errormessage = "Please enter a valid birth date.";	
                bol= false;
                }                 
                if((ddlmonth.value==" ")&&(ddlDay.value!=" ")&&(txtYear.value.length==0))    
                {
                cvDate.errormessage = "Please enter a valid birth date.";	
                bol= false;
                }	                 
                if((ddlmonth.value!=" ")&&(ddlDay.value!=" ")&&(txtYear.value.length==0))    
                {
                cvDate.errormessage = "Please enter a valid birth date.";	
                bol= false;
                }                	                                   
           break;    
    }
    return bol;
}

/// Validate BirthDaty Date 
function ValidBirthDate(ddlmonth,ddlDay,txtYear,cvDate,Tributemsg)
{ 
       
         var bol=true;   
         var dt=ddlmonth.value+"/"+ddlDay.value+"/"+txtYear.value;             
         if((ddlmonth.value!=" ")&&(ddlDay.value!=" ")&&(txtYear.value.length==0)) 
         {
            //cvDate.errormessage = Tributemsg;	                        
            bol= true;
         }
         else if((ddlmonth.selectedIndex==0)&&(ddlDay.selectedIndex==0)&&(txtYear.value.length==0)) 
         {            
            cvDate.errormessage = Tributemsg;	            
            bol= false;
         }
         else
         {
            if((ddlmonth.value==" ")||(ddlDay.value==" ")||(txtYear.value.length==0))                
            {
             cvDate.errormessage="Please enter a valid birth date.";              
             bol= false;
            } 
            else
            {           
	        if (isDate(dt,cvDate)==false)	        
	        {
	         cvDate.errormessage="Please enter a valid birth date."; 
		    bol= false;
		    }
	        else
            bol= true;
            }
        }
        
        return bol;
}

/// Validate Date 1
function ValidDate(ddlmonth,ddlDay,txtYear,cvDate,Tributemsg)
{ 
         var bol=true;   
         var dt=ddlmonth.value+"/"+ddlDay.value+"/"+txtYear.value;             
         if((ddlmonth.value==" ")&&(ddlDay.value==" ")&&(txtYear.value.length==0)) 
         {
            cvDate.errormessage = Tributemsg;	
            bol= false;
         }
         else
         {
            if((ddlmonth.value==" ")||(ddlDay.value==" ")||(txtYear.value.length==0))                
            {
             cvDate.errormessage="Please enter valid date.";
             bol= false;
            } 
            else
            {           
	        if (isDate(dt,cvDate)==false)	        
		    bol= false;
	        else
            bol= true;
            }
        }
        
        return bol;
}

function isInteger(s){
	var i;
    for (i = 0; i < s.length; i++){   
        // Check that current character is number.        
        var c = s.charAt(i);
        if (((c < "0") || (c > "9"))) return false;
    }
    // All characters are numbers.
    return true;
}

  function isDate(dtStr,cvDate)
  {    
	var daysInMonth = DaysArray(12)
	var pos1=dtStr.indexOf(dtCh)
	var pos2=dtStr.indexOf(dtCh,pos1+1)
	var strMonth=dtStr.substring(0,pos1)
	var strDay=dtStr.substring(pos1+1,pos2)
	var strYear=dtStr.substring(pos2+1)
	strYr=strYear	
	if (strDay.charAt(0)=="0" && strDay.length>1) strDay=strDay.substring(1)
	if (strMonth.charAt(0)=="0" && strMonth.length>1) strMonth=strMonth.substring(1)
	for (var i = 1; i <= 3; i++) {
		if (strYr.charAt(0)=="0" && strYr.length>1) strYr=strYr.substring(1)
	}
	month=parseInt(strMonth)
	day=parseInt(strDay)
	year=parseInt(strYr)
	if (pos1==-1 || pos2==-1){
	//cvDate.errormessage = "The date format should be : mm/dd/yyyy.";		
		return false
	}
	if (strMonth.length<1 || month<1 || month>12)
	{
	//cvDate.errormessage = "Please enter a valid month.";		
		return false
	}
	if (strDay.length<1 || day<1 || day>31 || (month==2 && day>daysInFebruary(year)) || day > daysInMonth[month]){		
		//cvDate.errormessage = "Invalid Date,please enter";
		return false
	}
	if (strYear.length != 4 || year==0 || year<minYear || year>maxYear)
	{		
		cvDate.errormessage = "Please enter a valid 4 digit year between "+minYear+" and "+maxYear;
		return false
	}
	//if (dtStr.indexOf(dtCh,pos2+1)!=-1 || isInteger(stripCharsInBag(dtStr, dtCh))==false)	
	if (isInteger(strYr)==false)	
	{  
	//    cvDate.errormessage = "Invalid date,Please enter numeric in year.";		
		return false
	}
return true
}

function stripCharsInBag(s, bag){
	var i;
    var returnString = "";
    // Search through string's characters one by one.
    // If character is not in bag, append to returnString.
    for (i = 0; i < s.length; i++){   
        var c = s.charAt(i);
        if (bag.indexOf(c) == -1) returnString += c;
    }
    return returnString;
}

function daysInFebruary (year){
	// February has 29 days in any year evenly divisible by four,
    // EXCEPT for centurial years which are not also divisible by 400.
    return (((year % 4 == 0) && ( (!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28 );
}
function DaysArray(n) {
	for (var i = 1; i <= n; i++) {
		this[i] = 31
		if (i==4 || i==6 || i==9 || i==11) {this[i] = 30}
		if (i==2) {this[i] = 29}
   } 
   return this
}



function isInteger(s)
{   var i;
    for (i = 0; i < s.length; i++)
    {   
        // Check that current character is number.
        var c = s.charAt(i);
        if (((c < "0") || (c > "9"))) return false;
    }
    // All characters are numbers.
    return true;
}

function TributeValidate(count)
{

  var bol=false;     
  for(i=0;i<count;i++)
  {
   var x=document.getElementById('ctl00_TributePlaceHolder_rdoTributeTypes_'+i);   
   if (x.checked)
   {
        bol=true;
        break;
   }
  } 
  return bol;
 }
 
 
 