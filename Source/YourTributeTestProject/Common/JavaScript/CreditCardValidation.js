///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Common.JavaScript.CreditCardValidation.js
///Author          : 
///Creation Date   : 
///Description     : This file is used to perform basic credit card validations
///Audit Trail     : Date of Modification  Modified By         Description


//Check Integer value
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



// Check Phonenumber.
function  PhoneNumberValidate(number1,number2,number3,validator)
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
// Check Expiremonth.
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
             if((CCNumber.value.length >12)&&(CCNumber.value.length < 18))
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
           val=1;
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



function MakeAutoRenew()
 {
    var rdb=$('rdoYearlyAutoRenew');
    if(rdb)
    {
      if(rdb.checked==true)
      {
       $('chkSaveBillingInfo').checked=true;
       $('chkSaveBillingInfo').disabled=true;
      }
      else
      {
       $('chkSaveBillingInfo').checked=false;
       $('chkSaveBillingInfo').disabled=false;
      }    
    }
 } 