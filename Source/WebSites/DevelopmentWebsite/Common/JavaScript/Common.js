///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Common.JavaScript.Common.js
///Author          : 
///Creation Date   : 
///Description     : This file is used to define/perform the common methods to be used across the site
///Audit Trail     : Date of Modification  Modified By         Description

function TributeValidate(rdb1, rdb2,rdb3,rdb4,rdb5,rdb6)
{ 
    var bool=false;           
    if((!rdb1.checked)&&(!rdb2.checked)&&(!rdb3.checked)&&(!rdb4.checked)&&(!rdb5.checked)&&(!rdb6.checked))           
    bool=false;                      
    else
    bool=true;  
 return bool;
}

function PaymentMethodvalidate(rdb1,rdb2,rdb3,rdb4)
 {
         var bool=false;           
           if((!rdb1.checked)&&(!rdb2.checked)&&(!rdb3.checked)&&(!rdb4.checked))           
           bool=false;                      
           else
           bool=true;
           
       return bool;

   }
   function PaymentMethodvalidate(rdb1, rdb2) {
       var bool = false;
       if ((!rdb1.checked) && (!rdb2.checked))
           bool = false;
       else
           bool = true;

       return bool;

   }
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
             if((CCNumber.value.length > 12)&&(CCNumber.value.length < 18 ))
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

 function SelectVideoAccountType(source, args) {
     var rdb1 = $('ctl00_TributePlaceHolder_rdoMembershipYearly');
     var rdb2 = $('ctl00_TributePlaceHolder_rdoMembershipLifetime');
     var rdb3 = $('ctl00_TributePlaceHolder_rdoMembershipThirty');
     var rdb4 = $('ctl00_TributePlaceHolder_rdoMembershipNinety');
     if ((!rdb1.checked) && (!rdb2.checked) && (!rdb3.checked) && (!rdb4.checked))
         args.IsValid = false;
     else
         args.IsValid = true;
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
function chkForMaxLength_(maximumLength, textLength)
{  
  
    if (textLength <= maximumLength)
        return true;
    else
        return false;
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
                // var x=getYear();
                 //alert(x);
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
var maxYear=2100;

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

//LHK

function VTDateValidation2(ddlmonth,ddlDay,txtYear,ddlmonth2,ddlDay2,txtYear2,cvDate)
{
    var bol=true;          
     if((ddlmonth2.value==" ")||(ddlDay2.value==" ")||(txtYear2.value.length==0))
       {
            cvDate.errormessage = "Date of Death is a required field.";	
            bol= false;
            }
     return bol;
}
//till here 

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
	if (strDay.length<1 || day<1 || day>31 || (month==2 && day>daysInFebruary(year)) || day > daysInMonth(month)){		
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
function daysInMonth(month){
  var md = [31,29,31,30,31,30,31,31,30,31,30, 31];
  return md[month-1];
}


/*------------------------------------------------------------

STORY FUNCTION

------------------------------------------------------------*/
 
function StoryDate1Require(day, month, year, tributetype, newbaby, birhday, memorial)
{

    // If Tribute Type is Memorial, birthday and New baby then Date1 is not Require
    if ( (tributetype == memorial) || (tributetype == newbaby) )
    {
        if ( (day == 0) && (month == 0) && (year == "") )
        {
           return true;
        }   
    }
    else if (tributetype == birhday)
    {
        if ( (day == 0) || (month == 0) )
        {
           return false;
        }   
    }
    else // If Tribute Type is other than Memorial, birthday and New baby then Date1 is Require
    {
        if ( (day == 0) || (month == 0) || (year == "") )
        {
           return false;
        } 
    }
  
    return true;
}

function StoryDate2Require(day, month, year, tributetype, memorial)
{
    if (tributetype == memorial) 
    {
        if ( (day == 0) || (month == 0) || (year == "") )
        {
           return false;
        }   
    }
    
    return true;
}

function StoryCheckNewBaby(day1, month1, year1, day2, month2, year2,tributetype, newbaby)
{        
    if (tributetype == newbaby)
    {
        if ( ( month1 == 0) && ( day1 == 0) && ( year1 == "") &&
              ( month2 == 0) && ( day2 == 0 ) && ( year2 == "" ) )
        {
           return 0;
        }   
        else  if ( ( month1 != 0) && ( day1 != 0) && ( year1 != "") &&
                   ( month2 != 0) && ( day2 != 0 ) && ( year2 != "" ) )
        {
            return 2;
        }   
        else if ( ( month1 != 0) && ( day1 != 0) && ( year1 != "") &&
                  ( month2 == 0) && ( day2 == 0 ) && ( year2 == "" ) )
        {
            return 1;
        }   
        else if ( ( month1 == 0) && ( day1 == 0) && ( year1 == "") &&
                  ( month2 != 0) && ( day2 != 0 ) && ( year2 != "" ) )
        {
           return 1;
        } 
        else if ( ( month1 != 0) && ( day1 != 0) && ( year1 != "") )
        {
           return 0;
        } 
        else if ( ( month2 != 0) && ( day2 != 0 ) && ( year2 != "" ) )
        {
           return 0;
        } 
    }
    
    return 1;
}

function StoryCheckDate1(day1, month1, year1, day2, month2, year2, tributetype, memorial, newbaby, birhday)
{
    var check = false;

     if ( year1 != "")
    {
        if ( ( Number(year1) < 1753) || ( Number(year1) > 9999))
        {
            return false;
        }
    }
    
    if (tributetype == memorial)
    {
        if ( (month1 == 0) && (day1 == 0) && (year1 == "") )
        {
            return true;
        }
        else if ( (month1 != 0) && (day1 != 0) && (year1 != ""))
        {
            check = true;
        }
        else
        {
            return false;
        }
    }
    else  if (tributetype == newbaby)
    {
        if (( month1 == 0) && (day1 == 0) && (year1 == "") )
        {
            return true;
        }
        else if ((month1 != 0) && (day1 != 0) && (year1 != ""))
        {
            if ((Number(month2.value) == 0)&& (Number(day2.value) == 0) && (year2.value == ""))
            {
                check = true;
            }
            else
            {
                return true;
            }
        }
        else if ((Number(month2.value) != 0) && (Number(day2.value) != 0) && (year2.value != ""))
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
    else if (tributetype == birhday)
    {
        check = true
        if ( (month1 != 0) && (day1 != 0) && (year1 == "") )
        {
            if (month1 == 2)
            {
                if ( (day1 == 30)||(day1 == 31) )
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
    }
    else
    {
        check = true
    }
    
    if ( check == true)
    {
        var dayobj = new Date( Number(year1), month1 - 1, day1 );
        
        if ((dayobj.getMonth() + 1 != month1)||(dayobj.getDate()!= day1)||(dayobj.getFullYear()!= Number(year1)))
        {
           return false;
        }
        else
        {
           return true;
        }
   }
   
   return true;
}

function StoryCheckDate2(day1, month1, year1, day2, month2, year2, tributetype, newbaby)
{
    var check = false;
    
    if ( year2 != "")
    {
        if ( ( Number(year2) < 1800) || ( Number(year2) > 9999))
        {
            return false;
        }
    }
    
    if (tributetype == newbaby)
    {
        if ( (month2 == 0) && (day2 == 0) && (year2 == "") )
        {
             return true;
        }
        else if ((month2 != 0) && (day2 != 0) && (year2 != ""))
        {
            if ((Number(month1) == 0)&& (Number(day1) == 0) && (year1 == ""))
            {
                check = true;
            }
            else
            {
                return true;
            }
        }
        else if ((month1 != 0) && (day1 != 0) && (year1 != ""))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    else
    {
        check = true;
    }
    
    if ( ( month2 == 0 ) && ( day2 == 0 ) && ( year2 == "" ) )
    {
        return true;
    }
    
    if ( check == true)
    {
        var dayobj = new Date( Number(year2), month2 - 1, day2 );
        
        if ((dayobj.getMonth() + 1 != month2)||(dayobj.getDate()!= day2)||(dayobj.getFullYear()!= Number(year2)))
        {
            return false;
        }
        else
        {
             return true;
        }
   }
   
   return true;
}

function StoryCheckFutureDate(day, month, year, today)
{
    if ( (day != "0") && (month != "0") && (year != "") )
    {
        var dayobj = new Date( Number(year), month - 1, day )
                                       
        if ((dayobj.getMonth() + 1 == month)&&(dayobj.getDate()== day)&&(dayobj.getFullYear()== Number(year)))
        {             
            if (dayobj > today)
            {
               return false;
            }
            else
            {
                return true;
            }
       }
   }
   
   return true;      
}

function StoryCheckDueDate(day, month, year, today)
{
    if ( (month != "0") && (day != "0") && (year != "") )
    {   
        var dayobj = new Date( Number(year), month - 1, day );
                               
       if ((dayobj.getMonth() + 1 == month)&&(dayobj.getDate()== day)&&(dayobj.getFullYear()== Number(year)))
       {             
            if (dayobj < today)
            {
               return false;
            }
            else
            {
                return true;
            }
       }
   }
   
   return true;
}
    
function StoryCompareDates(day1, month1, yeardate1, day2, month2, yeardate2, today)
{
    if ( ( day1 != "0") && ( month1 != "0") && ( yeardate1 != "") &&
         ( day2 != "0" ) && ( month2 != "0" ) && ( yeardate2 != "" ) )
    {
        var year1 = Number(yeardate1);
        var year2 = Number(yeardate2);
        
        var dayobj = new Date( year1, month1 - 1, day1 );
                                       
        if ((dayobj.getMonth() + 1 == month1)&&(dayobj.getDate()== day1)&&(dayobj.getFullYear()== year1))
        {             
            if (dayobj > today)
            {
               return true;
            }
            else
            {
                var dayobj = new Date( year2, month2 - 1, day2 );
                                           
                if ((dayobj.getMonth() + 1 == month2)&&(dayobj.getDate()== day2)&&(dayobj.getFullYear()== year2))
                {             
                    if (dayobj > today)
                    {
                       return true;
                    }
                    else if ( year1 < year2 )
                    {
                        return true;
                    } 
                    else if ( year1 > year2 )
                    {
                        return false;
                    } 
                    else if ( month1 < month2 )
                    {
                        return true;
                    } 
                    else if ( month1 > month2 )
                    {
                        return false;
                    } 
                    else if ( day1 < day2 )
                    {
                        return true;
                    } 
                    else if ( day1 > day2 )
                    {
                        return false;
                    }
                }
            }
       }            
   }
       
   return true;
}

/**************LHK for VT************************/
function VideoCompareDates(day1, month1, yeardate1, day2, month2, yeardate2, today)
{
    if ( ( day1 != "0") && ( month1 != "0") && ( yeardate1 != "") &&
         ( day2 != "0" ) && ( month2 != "0" ) && ( yeardate2 != "" ) )
    {
        var year1 = Number(yeardate1);
        var year2 = Number(yeardate2);
        
        var dayobj = new Date( year1, month1 - 1, day1 );
                                       
        if ((dayobj.getMonth() + 1 == month1)&&(dayobj.getDate()== day1)&&(dayobj.getFullYear()== year1))
        {             
            if (dayobj > today)
            {
               return true;
            }
            else
            {
                var dayobj = new Date( year2, month2 - 1, day2 );
                                           
                if ((dayobj.getMonth() + 1 == month2)&&(dayobj.getDate()== day2)&&(dayobj.getFullYear()== year2))
                {             
                    if (dayobj > today)
                    {
                       return true;
                    }
                    else if ( year1 < year2 )
                    {
                        return true;
                    } 
                    else if ( year1 > year2 )
                    {
                        return false;
                    } 
                    else if ( month1 < month2 )
                    {
                        return true;
                    } 
                    else if ( month1 > month2 )
                    {
                        return false;
                    } 
                    else if ( day1 < day2 )
                    {
                        return true;
                    } 
                    else if ( day1 > day2 )
                    {
                        return false;
                    }
                }
            }
       }            
   }
   else
   {
        return false;
   }
}
/**************************************/
function StoryCalculateAge(day1,month1,year1,day2,month2,year2, labelage) 
{       
   
   if ( Number(year2) > Number(year1) && (Number(year2) != 0) && (Number(year1) != 0) )
    {
        var age = Number(year2) -  Number(year1);
        
        if(( month1 > month2 ) || (month2 == month1 && day1 < day2) ) age --

        labelage.innerHTML  = age;
    }
    else
    {
        labelage.innerHTML  = 0;
    }
}

function ValidateStoryLength(textarea, charlimit)
{	
	if(textarea.value.length > Number(charlimit)) 
	{
       textarea.value = textarea.value.substr(0, charlimit);
	}
}

function ValidateTopicLength(rowcount)
{
    charlimit = 2000;	
    var name = "ctl00$ModuleContentPlaceHolder$repMoreAbout$ctl0";
    
    for (var i = 0; i <= rowcount; i++)
    {
        name += i;
        name += "$txtTopicAnswerMoreAbout";
        if ($(name) != null)
        {
            if($(name).value.length > charlimit) 
	        {
               $(name).value = $(name).value.substr(0, charlimit);
	        }
	        
	        return;
        }
        
        name = "ctl00$ModuleContentPlaceHolder$repMoreAbout$ctl0";
    }
    
    return true;
}

function ValidateTopic(rowcount)
{
    var name = "ctl00$ModuleContentPlaceHolder$repMoreAbout$ctl0";

    for (var i = 0; i <= rowcount; i++)
    {
        name += i;
        name += "$ddlTopicListMoreAbout";
        if ($(name) != null)
        {
            if($(name).value == "Select a Topic:")
            {
                return false;
            }
            else
            {
                return true;
            }
            
            return true;
        }
        
         name = "ctl00$ModuleContentPlaceHolder$repMoreAbout$ctl0";
    }
    
    return true;
}

/*------------------------------------------------------------

END OF STORY FUNCTION

------------------------------------------------------------*/


/*------------------------------------------------------------

GIFT FUNCTION

------------------------------------------------------------*/

function ValidateLength(textarea,numberRemaining)
{	
	charlimit = textarea.getAttribute('rows') * textarea.getAttribute('cols');	

	if(textarea.value.length <= charlimit) 
	{
        numberRemaining.innerText = charlimit - textarea.value.length;	
	}
	else 
	{
	    textarea.value = textarea.value.substr(0, charlimit);
	    numberRemaining.innerText = 0;
	}
}

/*------------------------------------------------------------

END OF GIFT FUNCTION

------------------------------------------------------------*/

/*------------------------------------------------------------

EVENT FUNCTION

------------------------------------------------------------*/

function EventsDateRequire(day, month, year)
{
    if ( (day == 0) || (month == 0) || (year == "") )
    {
       return false;
    } 
    
    return true;
}

function EventValidDate(day, month, year)
{
    var dayobj = new Date( year, month - 1, day );
        
    if ((dayobj.getMonth() + 1 != month)||(dayobj.getDate()!= day)||(dayobj.getFullYear()!= year))
    {
       return false;
    }
    else
    {
       return true;
    }
   
   return true;
}

function EventCheckDate(day, month, year, today)
{
    if ( (month != "0") && (day != "0") && (year != "") )
    {   
        var dayobj = new Date( Number(year), month - 1, day );
                               
       if ((dayobj.getMonth() + 1 == month)&&(dayobj.getDate()== day)&&(dayobj.getFullYear()== Number(year)))
       {             
            if (dayobj <= today)
            {
               return false;
            }
            else
            {
                return true;
            }
       }
   }
   
   return true;
}

function EventCheckTime(hourStart, minuteStart, AMPMStart, hourEnd, minuteEnd, AMPMEnd)
{
    if (AMPMStart == AMPMEnd)
    {
        if(AMPMStart == 1 && hourStart != 12)
        {
        hourStart = hourStart + 12;
        }
        if(AMPMEnd == 1 && hourEnd != 12)
        {
        hourEnd = hourEnd + 12;
        }
        if( hourStart < hourEnd )
        {
            return true;
        }
        else if((hourStart == hourEnd && minuteStart < minuteEnd) )
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    return true;
}


function  EventValidatePhoneNumber(number1,number2,number3)
{    
     if ( (number1 == "") && (number2 == "") && (number3 == "") )
     {        
        return true;
     }
     else
     {
        if( (number1.length + number2.length + number3.length) == 10 )
        {
            var phonenumber = Number(number1) + Number(number2) + Number(number3);
            return isInteger(phonenumber);
        } 
        else
        {
            return false;
        }
    }
    
    return true;     
}

function EventsAddOtherType(eventType, otherType)
{
    if(eventType == "other")
    {
        if(otherType == "")
        {
            return false;
        }
    }
    
    return true;
}

function EventCheckValidEmail(validator, EmailList)
{
    var emailReg = "^[\\w-_\.]*[\\w-_\.]\@[\\w]\.+[\\w]+[\\w]$";
    var regex = new RegExp(emailReg);

    var EmailArray = EmailList.split(";");
    
    for(var i = 0; i < EmailArray.length; i++)
    {
        var email = EmailArray[i].trim();
        
        if (email != "")
        {
            if ( !(regex.test(email)) )
            {
                validator.errormessage = EmailArray[i] + " is not a valid Email address";
                return false;
            }
        }
    }
    
    return true;
}
/*------------------------------------------------------------

END OF EVENT FUNCTION

------------------------------------------------------------*/

/*------------------------------------------------------------

START OF SEARCH FUNCTION

------------------------------------------------------------*/

function SearchValidDate(day, month, year)
{
    if ( (day == 0) && (month == 0) && (year == "") )
    {
       return true;
    } 
    else if ( (day == 0) || (month == 0) || (year == "") )
    {
       return false;
    } 
    
    if ( (day != 0) && (month != 0) && (year != "") )
    {
        var dayobj = new Date( year, month - 1, day );
            
        if ((dayobj.getMonth() + 1 != month)||(dayobj.getDate()!= day)||(dayobj.getFullYear()!= year))
        {
           return false;
        }
        else
        {
           return true;
        }
    }
   
   return true;
}

function SearchCompareDates(day1, month1, yeardate1, day2, month2, yeardate2, today)
{
    if ( (day1 != 0) && (month1 != 0) && (yeardate1 != "") &&
         (day2 != 0) && (month2 != 0) && (yeardate2 != "") )
    {
        var year1 = Number(yeardate1);
        var year2 = Number(yeardate2);
        
        var dayobj = new Date( year1, month1 - 1, day1 );
                                       
        if ( (dayobj.getMonth() + 1 == month1)&&(dayobj.getDate()== day1)&&(dayobj.getFullYear()== year1) )
        {             
            var dayobj = new Date( year2, month2 - 1, day2 );
                                           
            if ( (dayobj.getMonth() + 1 == month2)&&(dayobj.getDate()== day2)&&(dayobj.getFullYear()== year2) )
            {             
                if ( year1 < year2 )
                {
                    return true;
                } 
                else if ( year1 > year2 )
                {
                    return false;
                } 
                else if ( month1 < month2 )
                {
                    return true;
                } 
                else if ( month1 > month2 )
                {
                    return false;
                } 
                else if ( day1 < day2 )
                {
                    return true;
                } 
                else if ( day1 > day2 )
                {
                    return false;
                }
            }
       }            
   }
       
   return true;
}

/*------------------------------------------------------------

END OF SEARCH FUNCTION

------------------------------------------------------------*/

function  PhoneNumberValidate_(number1,number2,number3,validator)
{
       var bol=true;          
        var totalLength=number1.value.length+number2.value.length+number3.value.length;
        if(totalLength==10)
        {
            var s=number1.value+number2.value+number3.value;           
            bol=isInteger(s);
            if(bol==false)
            validator.errormessage="Enter 10 digit numeric value in phone number.";
        }
        else
        {
            validator.errormessage="Enter 10 digit numeric value in phone number.";
            bol=false;   
        }
    
    return bol;     
}

// Validate phone number

function  PhoneNumberValidate(number1,number2,number3,validator)
{
     var bol=true;     
     if((number1.value.length==0)&&(number2.value.length==0)&&(number3.value.length==0))
     {        
        bol=true;      
     }
     else
     {
        var totalLength=number1.value.length+number2.value.length+number3.value.length;
        if(totalLength==10)
        {
            var s=number1.value+number2.value+number3.value;           
            bol=isInteger(s);
            if(bol==false)
            validator.errormessage="Enter 10 digit numeric value in phone number.";
        }
        else
        {
            validator.errormessage="Enter 10 digit numeric value in phone number.";
            bol=false;   
        }
    }
    return bol;     
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

//function TributeValidate(count)
//{

//  var bol=false;     
//  for(i=0;i<count;i++)
//  {
//   var x=document.getElementById('ctl00_TributePlaceHolder_rdoTributeTypes_'+i);   
//   if (x.checked)
//   {
//        bol=true;
//        break;
//   }
//  } 
//  return bol;
// }
 
 
 
function ExpMonthvalidate(month,year,validat)
{
  var bol=true;  
  if((year.value.length==4)&&(month.selectedIndex!=0))
  {
        
      bol=isInteger(year.value);      
      if(bol==false)
      {
          validat.errormessage="Please enter a valid expiry date";         
          return bol;
      }
      else
      {
          var YEAR=parseInt(year.value);     
          var MONTH=parseInt(month.selectedIndex);  
          var right_now=new Date();
          var CYEAR=parseInt(right_now.getYear());            
          var CMONTH_=parseInt(right_now.getMonth())+1;          
          if(YEAR>CYEAR)
          {             
               bol=true;
               return bol;                        
          }
          else if(YEAR==CYEAR)
          {
            if(MONTH<CMONTH_)
            {
               validat.errormessage="Please enter a valid expiry date.";
               bol=false;
               return bol;
            }
          }
          else
          {
           validat.errormessage="Please enter a valid expiry date.";
            bol=false;
           return bol;          
          }
      } 
  }
  if((year.value.length!=4)&&(month.selectedIndex!=0))
  {
    validat.errormessage="Please enter a valid expiry date.";
    bol=false;    
  //   return bol;  
  }
  else if((month.selectedIndex==0)&&(year.value.length==0))
  {       
    validat.errormessage="Expiry Date is a required field.";
    bol=false;
  }
  else if((month.selectedIndex!=0)&&(year.value.length==0))
  {      
    validat.errormessage="Please enter a valid expiry date.";
    bol=false;  
    }
  else if((month.selectedIndex==0)&&(year.value.length!=0))
  {    
   validat.errormessage="Please enter a valid expiry date.";
   bol=false;  
  }
  else
  {  
    bol=true;  
  }
    
 return bol;
}



//function to remove the HTML tags from the string.
function removeHTMLTags(strData)
{
	var strInputCode = strData;
	/* 
		This line is optional, it replaces escaped brackets with real ones, 
		i.e. &lt; is replaced with < and &gt; is replaced with >
	*/	
	strInputCode = strInputCode.replace(/&(lt|gt);/g, function (strMatch, p1){
		return (p1 == "lt")? "<" : ">";
	});
	var strTagStrippedText = strInputCode.replace(/<\/?[^>]+(>|$)/g, "");
	//alert("Input code:\n" + strInputCode + "\n\nOutput text:\n" + strTagStrippedText);	
	return strTagStrippedText;
}

//function to check for the empty value
function isEmpty(cntrl)
{   
    if(!trimString(cntrl).length > 0) //if it is empty
    {
        return false;
    }
    else
    {
        return true;
    }
}

//checks if user enters spaces only in the textbox.
function trimString (str) 
{
    //through regular expression
    str = this != window? this : str;
    return str.replace(/^\s+/g, '').replace(/\s+$/g, '');
}

//function to check the length of the title and description entered for photos in the upload tool.
function ImageUploaderValidation()
{
    
    var guidIndexHash = getGuidIndexHash();
    var UploadPane = document.getElementById("UploadPane");
    var error = "";
    var errorMessage = "";
    //alert(UploadPane.childNodes.length);
    for (var i = 0; i < UploadPane.childNodes.length; i++)
    {
        var div = UploadPane.childNodes[i];
        var index = guidIndexHash[div._guid];
	    var valDescription = document.getElementById("Description" + div._uniqueId).value;
        var valTitle = document.getElementById("Title" + div._uniqueId).value;
        
        if (valTitle.length > 100)
        {
            if (isEmpty(errorMessage))
                errorMessage += ";"
            
            errorMessage += "Title of photo number ";
            errorMessage += i + 1 ;
            errorMessage += " is exceeding its maximum length of 100 characters."
        }
        if (valDescription.length > 1000)
        {
            if (isEmpty(errorMessage))
                errorMessage += ";"
            
            errorMessage += "Description of photo number ";
            errorMessage += i + 1;
            errorMessage += " is exceeding its maximum length of 1000 characters."
        }
    }
    return errorMessage;
}

//function to check album name exists in the album list or not.
function isUnique(albumName, albumList)
{
    var albums = albumList.split(';');
    for (var i=0; i<albums.length; i++)
    {
        if (albums[i] == albumName.replace(/^\s+/g, '').replace(/\s+$/g, ''))
            return false;
    }
    return true;
}