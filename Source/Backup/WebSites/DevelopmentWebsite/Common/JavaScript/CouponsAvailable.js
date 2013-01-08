///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Common.JavaScript.CouponsAvailable.js
///Author          : 
///Creation Date   : 
///Description     : This file is used to check for coupons' availability
///Audit Trail     : Date of Modification  Modified By         Description



var xmlHttp
function UserValidate(UserName,Password)
{ 
     xmlHttp=GetXmlHttpObject();
    if (xmlHttp==null)
    {
        alert ("Your browser does not support AJAX!");
        return;
    } 
     var thePage="/DevelopmentWebsite/ModelPopup/CheckAvailabality.aspx";      
           thePage=thePage+"?User="+UserName.value;
           thePage=thePage+"&Pass="+Password.value;                
           
     xmlHttp.onreadystatechange=stateChanged;     
     xmlHttp.open("POST",thePage,false);     
     //xmlHttp.open("GET",thePage,false);     
     xmlHttp.send(null);   
}
function stateChanged() 
{     
    if (xmlHttp.readyState==4)
    {     
      var id=$('txtHint');     
      id.innerHTML=xmlHttp.responseText;  

       if(id.innerText.trim()=='true')
      {     
      var title1 = document.title;       
       if (title1 == "UserRegistration")
            {window.location="UserAccounts.aspx";}
       else
           {window.location = location.href;}
      
       window.reload();
      }
      else
      {		              
       var pass=$('ctl00_txtLoginPassword1');  
       var pass2=$('txtLoginPassword1');  
       if(pass)
       {    
        pass.value="";
        pass.focus();
       }
       if(pass2)
       {    
        pass2.value="";
        pass2.focus();
       }
      }
    }
}
function GetXmlHttpObject()
{
    var xmlHttp=null;
    try
    {
      // Firefox, Opera 8.0+, Safari
      xmlHttp=new XMLHttpRequest();
    }
    catch (e)
    {
      // Internet Explorer
        try
        {
            xmlHttp=new ActiveXObject("Msxml2.XMLHTTP");
        }
            catch (e)
        {
        xmlHttp=new ActiveXObject("Microsoft.XMLHTTP");
        }
    }
    return xmlHttp;
}

