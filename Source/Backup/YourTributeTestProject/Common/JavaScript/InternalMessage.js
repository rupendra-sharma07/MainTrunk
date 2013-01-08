///Copyright       : Copyright (c) Sopra Group India Ltd 
///Project         : Timeless Tributes
///File Name       : TributePortal.DevelopmentWebsite.Common.JavaScript.InternalMessage.js
///Author          : 
///Creation Date   : 
///Description     : This file is used to display the internal messages
///Audit Trail     : Date of Modification  Modified By         Description

function UserLogin(UserName,Password)
{
var stst=false;    
     var error=$('errorPopPwd');          
     if((UserName)&&(Password))
     {
        stst=true;
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
            stst=false;
        }
        else
        {
            stst=true;
        }
   }   
 return stst;        
}



function Chkvalue(param2)
{
  alert(param2);
}

var xmlHttp
function UserDetails(id,subject)
{ 
   
     xmlHttp=GetXmlHttpObject();
    if (xmlHttp==null)
    {
        alert ("Your browser does not support AJAX!");
        return;
    } 
     var Userid=$('hfToUserid');
     Userid.value=id;     
     var txtSubject=$('txtSubject');
     if(txtSubject)
     txtSubject.value=subject;
     
    

   var thePage="/Tributeportal/ModelPopup/CheckAvailabality.aspx";        
           thePage=thePage+"?UserId="+id;           
     xmlHttp.onreadystatechange=stateChanged;

     xmlHttp.open("POST",thePage,true);     
     xmlHttp.send(null);   
}
function stateChanged() 
{     
    if (xmlHttp.readyState==4)
    {       
      var id=$('txtHint');      
      id.innerHTML=xmlHttp.responseText;                  
      var userinfo_array=id.innerText.trim().split("`");      
      var dlDetails=$('dlDetails');
      var userImg=$('userImg');
      
       userImg.src='';
       //alert(userinfo_array[7]);
       if(userinfo_array[7].length>0)
       {
         userImg.src=userinfo_array[7].replace("~","..");
       }
       var _html="<dt id='dtlblname' runat='server'>Name:</dt>";        
      _html+="<dd id='ddtxtname' runat='server' class='fn'>"+userinfo_array[0]+"</dd>";
        if(userinfo_array[6].trim()=='True')
        {
        _html+="<dt id='dtlblusrname' runat='server' >Username:</dt>";
        _html+="<dd id='ddtxtusrname' runat='server' class='nickname'>"+userinfo_array[1]+"</dd>";
        }
        _html+="<dt id='dtlblmember' runat='server'>Member Since:</dt>";
       _html+="<dd id='ddtxtmember' runat='server'>"+userinfo_array[2]+"</dd>";
       if(userinfo_array[5].trim()=='False')
       {
        var location=userinfo_array[3].split('=');
      _html+="<dt id='dtlbllocation' runat='server'>Location:</dt>";
      _html+="<dd id='ddtxtlocation' runat='server' class='adr'>"+location[0]+"<br />"+location[1]+","+location[2]+"</dd>";
       }
       if(userinfo_array[4].length>0)
       {
      _html+="<dt id='dtlblwebsite' runat='server' >Website:</dt>";
      _html+="<dd id='ddlblwebsite' runat='server' class='url'><a href='http://"+userinfo_array[4]+"'>"+userinfo_array[4]+"</a></dd>";
       }
       if(userinfo_array[8].trim()=='False')
        {
          
          var divpostmessage=$('divpostmessage');          
          if(divpostmessage)          
           divpostmessage.style.visibility = 'hidden';
           var divmessagebtn=$('divmessagebtn');      
           if(divmessagebtn)          
           divmessagebtn.style.visibility = 'hidden';           
        }  
        else
        {
         var divpostmessage=$('divpostmessage');          
          if(divpostmessage)          
           divpostmessage.style.visibility = 'visible';
           var divmessagebtn=$('divmessagebtn');      
           if(divmessagebtn)          
           divmessagebtn.style.visibility = 'visible';  
        }
          
        dlDetails.innerHTML=_html;
        
        
      UserProfileModal();
      
      if($('txtarUserProfileMsg'))
        $('txtarUserProfileMsg').focus();
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
           
     xmlHttp.onreadystatechange=stateChanged1;     
     xmlHttp.open("POST",thePage,true);     
     xmlHttp.send(null);   
}
function stateChanged1() 
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
      
       //stst=true;
       window.reload();
      }
      else
      {
       var pass2=$('txtLoginPassword1');    
       if(pass2)
       {    
        pass2.value="";
        pass2.focus();
       }
      }
    }
}