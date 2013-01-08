///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Common.JavaScript.TributeHomePage.js
///Author          : 
///Creation Date   : 
///Description     : This file is used to perform general functions of the tribute home page
///Audit Trail     : Date of Modification  Modified By         Description

 function checkLength()
    {
      var txt=$('txtWelcomeMsg');      
      if(txt.value.length<=300)      
      {
      $('txtCharacterRemaining').value=300-txt.value.length;   
      return true;
      }
      else
      {
       return false;
      }
      
    }
    
    function UnderConst()    
    {
    var settings = "titlebar:no;dialogHide:0;dialogTop=100px;dialogHeight:500px;dialogWidth:500px;scroll:no;status:no;help:no;center:yes;resizable:no";
    window.showModalDialog('../Miscellaneous/UnderConstruction.aspx','',  settings);    
    }
    function UnderConst_()    
    {
    var settings = "titlebar:no;dialogHide:0;dialogTop=100px;dialogHeight:500px;dialogWidth:500px;scroll:no;status:no;help:no;center:yes;resizable:no";
    window.showModalDialog('../Miscellaneous/UnderConstruction.aspx','',  settings);
    return false;    
    }

function  PhoneNumberValidate1(number1,number2,number3,validator)
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

