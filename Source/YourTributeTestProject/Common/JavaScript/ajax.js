///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Common.JavaScript.ajax.js
///Author          : 
///Creation Date   : 
///Description     : This file is used to define/perform the methods to be used under the use of ajax
///Audit Trail     : Date of Modification  Modified By         Description


function THValidateUser(source, args)
{    
     var UserName=$('txtLoginUsername1');
     var Password=$('txtLoginPassword1');   
     var error=$('errorPopPwd');  
     if((UserName.value.length>0)&&(Password.value.length>0))  
     {          
      UserValidate(UserName,Password); 
      if(error)
      {
          error.innerText='!';
          error.style.color='#FF8000';
          error.style.fontSize='Medium';
          error.style.fontWeight='bold';
          error.style.visibility = 'visible';
      }   
       args.IsValid=false;
     }
     else
     {
       args.IsValid=true;
     }
}

function ValidateUser(source, args)
{    
     var UserName=$('ctl00_txtLoginUsername1');
     var Password=$('ctl00_txtLoginPassword1');
     var error=$('ctl00_errorPopPwd');
     if((UserName.value.length>0)&&(Password.value.length>0))  
     {          
      UserValidate(UserName,Password);  
      if(error)
      {
          error.innerText='!';
          error.style.color='#FF8000';
          error.style.fontSize='Medium';
          error.style.fontWeight='bold';
          error.style.visibility = 'visible';
      }
      args.IsValid=false;
     }
     else
     {
       args.IsValid=true;
     }
}
    var xmlHttp
function UserValidate(UserName,Password)
{ 
     xmlHttp=GetXmlHttpObject();
    if (xmlHttp==null)
    {
        alert ("Your browser does not support AJAX!");
        return;
    } 
     var thePage="/Tributeportal/ModelPopup/CheckAvailabality.aspx";        
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
            {window.location="http://localhost:2111/DevelopmentWebsite/Home.aspx";}
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