<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignUpPopup.aspx.cs" Inherits="ModelPopup_SignUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sign Up Model Popup</title>

    <script src="../assets/scripts/mootools-1.11.js" type="text/javascript"></script>

    <script type="text/javascript" src="../assets/scripts/global.js"></script>

    <script type="text/javascript" src="../Common/JavaScript/Common.js"></script>

    <link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/default.css" />
    <!-- default grid layout stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/layouts/centered1024_1.css" />
    <!-- print-friendly stylesheet -->
    <link rel="stylesheet" type="text/css" media="print" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/print.css" />
    <!-- screen elements stylesheet should be here -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/screen.css" />
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/LoginPopup.css" />    

    <script language="javascript" type="text/javascript">
       
 function SignUpUser(source, args)
    {
       setDefault();    
       var  messages= document.getElementById('<%= messages.ClientID %>');
       var _html="";
       _html= SetValidation();
        if(_html=="")
        {
          args.IsValid=true;
        }else
        {
          messages.innerHTML=_html;
            errorToParent();
            args.IsValid=false;
        }
        
      }    
                  
        
        function SetValidation()
        {
        
       var email=document.getElementById('<%= txtEmail.ClientID %>');
       var pswd=document.getElementById('<%= txtPassword.ClientID %>');
       var cpswd=document.getElementById('<%= txtConfirmPassword.ClientID %>');
       var fname=document.getElementById('<%= txtFirstName.ClientID %>');
       var lname=document.getElementById('<%= txtLastName.ClientID %>');
       var country=document.getElementById('<%= ddlCountry.ClientID %>');
       var terms=document.getElementById('<%= chkAgreeTermsUse.ClientID %>');
         var _html="";
         if(email.value.length==0)
        {         
            _html="<li>Email Address is a required field.</li>";    
           
        }      
        else 
        {
             if(echeck(email.value)==false)
             _html="<li>Enter valid email address.</li>";
             
        }
        if(pswd.value.length==0) 
        {
             _html+="<li>Password is a required field.</li>";
             
        }
        else
        { 
          if(CheckPasswordLength(pswd.value)!=true)
          { 
              _html+="<li>Password must be between 6 to 16 characters.</li>";
             
          }
        }   
        if (cpswd.value.length==0)
        {
          _html+="<li>Confirm Password is a required field.</li>";
          
        }
        else
        {
          if(pswd.value!=cpswd.value)
          {
           _html+="<li>Confirm Password is not same as password.</li>";
           
          }
        }
        if (fname.value.length==0)
        {
          _html+="<li>First Name is a required field.</li>";
          
        }
        else
        {
          if(isAlphanumeric(fname)==false)
          _html+="<li>First Name only contain letters,numbers,'-' and '#'.</li>"
        }
        if(lname.value.length==0)
        {
          _html+="<li>Last Name is a required Field.</li>";
          
        }
        else
        {
          if(isAlphanumeric(lname)==false)
          _html+="<li>Last Name only contain letters,numbers,'-' and '#'.</li>"
        }
        if(country.value ==0)
        {
          _html+="<li>Country is a required Field.</li>";
         
        }
        if(terms.checked==false)
        {
           _html+="<li>Please accept terms of use and the privacy policy before submitting.</li>";
           
        }
            return _html;
        }
    function setDefault()
    {
     var error2= document.getElementById('<%= erremail.ClientID %>'); 
     var  messages= document.getElementById('<%= messages.ClientID %>');
     messages.innerHTML="";
     if(error2)
        {
        error2.style.visibility = 'hidden';  
        }     
    }
 
 function isAlphanumeric(str)
 {
	var alphaExp = /^[a-zA-Z0-9,\#,\-.\s]*$/;
	if(str.value.match(alphaExp)){
		return true;
	}else{		
		return false;
	}
}
 
 function echeck(str) {

		var at="@"
		var dot="."
		var lat=str.indexOf(at)
		var lstr=str.length
		var ldot=str.indexOf(dot)
		if (str.indexOf(at)==-1)
		{		
		   return false
		}

		if (str.indexOf(at)==-1 || str.indexOf(at)==0 || str.indexOf(at)==lstr){		  
		   return false
		}

		if (str.indexOf(dot)==-1 || str.indexOf(dot)==0 || str.indexOf(dot)==lstr){		    
		    return false
		}

		 if (str.indexOf(at,(lat+1))!=-1){
		    
		    return false
		 }

		 if (str.substring(lat-1,lat)==dot || str.substring(lat+1,lat+2)==dot){
		  
		    return false
		 }

		 if (str.indexOf(dot,(lat+2))==-1){
		   
		    return false
		 }
		
		 if (str.indexOf(" ")!=-1){
		   
		    return false
		 }

 		 return true					
	}
 
 function errorToParent(source, args)
        {                    
        parentError = window.parent.document.getElementById('mb_Error_Popup');        
        if($('yt-LoginError'))
         {				         
	        parentError.className = 'yt-Error';	        
	        parentError.innerHTML = $('yt-LoginError').innerHTML;				
        }        
        }                   
        
function emailexist(args)
  {  
  if(args==0){
  }
  else
  {  
       setDefault();    
       var  messages= document.getElementById('<%= messages.ClientID %>');
       messages.innerHTML="<li>User already exists for this email. </li>";
       errorToParent();
       args.IsValid=false;}
  }
function setIndicatorPassword_()
{          
 errorToParent();                   
}     
function  chk(locat)
{      
  parent.modalClose_();
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
    </script>

</head>
<body>
    <form id="Form1" action="" runat="server" defaultbutton="btnSignUp" defaultfocus="txtEmail">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="divShowModalPopup">
    </div>
    <div id="SignupMainDiv" class="yt-Container" style="width: 602px;">
        <div id="yt-ContentContainer" class="yt-ModalWrapper">
            <asp:UpdatePanel ID="updatePanel1" runat="server">
                <ContentTemplate>
                    <div id="yt-ContentPrimaryContainer">
                        <div class="yt-Error" id="yt-LoginError" style="width: 630px">
                            <h2>
                                Oops - there was a problem in your login.</h2>
                            <h3>
                                Please correct the errors below:</h3>
                            <ul id="messages" runat="server">
                            </ul>
                        </div>
                        <fieldset class="yt-Form-Popup">
                            <%--for text below header--%>
                            <div id="UpperText">
                                <center>
                                    <div>
                                        <asp:Label ID="lblSignUp" runat="server" Text=" Sign up to Your Tribute" CssClass="yt-Form-Heading"></asp:Label>
                                    </div>
                                    <div class="yt-margin-top">
                                        <div>
                                            <asp:Label ID="lblAccounttxt" runat="server" CssClass="yt-Form-Label" Text="Accounts are free and your information is safe and secure. "></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="lblProfiletxt" Text="Add a profile, add/remove messages, upload photos, and more!"
                                                CssClass="yt-Form-LabelText" runat="server"></asp:Label></div>
                                    </div>
                                </center>
                            </div>
                            <br class="yt-clear-both" />
                            <div id="Form-Field" style="width: 560px; margin-left: 0px; margin-top:10px;">
                                <div id="leftDIv" runat="server" style="float: left; width: 280px; margin-left: 14px;">
                                    <div id="EmailField" runat="server">
                                        <asp:Panel ID="EmailPanel" runat="server" Width="280px">
                                            <div>
                                                <span class="yt-Form-asterisk">*</span> <span>
                                                    <label class="yt-Form-Field-label">
                                                        Email Address: </label></span>
                                            </div>
                                            <div class="yt-Div-txtbox">
                                                <asp:TextBox ID="txtEmail" runat="server" Width="240px" MaxLength="250" TabIndex="1">
                                                </asp:TextBox>
                                                <span id="errorPopEmail" runat="server" style="visibility: hidden;">&nbsp;</span>
                                                <span id="erremail" runat="server" style="color: #FF8000; font-size: Medium; font-weight: bold;"
                                                    visible="false">!</span>
                                                <asp:CustomValidator ID="cvPopupLogin" ErrorMessage="Invalid Username and Password."
                                                    Text="!" ClientValidationFunction="SignUpUser" runat="server" Font-Bold="True"
                                                    Font-Size="Medium" ForeColor="White" Visible="true" ValidateEmptyText="True"></asp:CustomValidator>
                                            </div>
                                        </asp:Panel>
                                        <br class="yt-clear-both" />
                                    </div>
                                    <div id="PasswordField" runat="server" class="yt-margintop">
                                        <asp:Panel ID="PanelPassword" runat="server" Width="280px">
                                            <div>
                                                <span class="yt-Form-asterisk">*</span> <span>
                                                    <label class="yt-Form-Field-label">
                                                        Password: </label></span>
                                            </div>
                                            <div class="yt-Div-txtbox">
                                                <asp:TextBox ID="txtPassword" TabIndex="2" runat="server" Width="240px" MaxLength="16"
                                                    TextMode="Password"></asp:TextBox>
                                                <span id="errorPwd" runat="server" style="color: #FF8000; font-size: Medium; font-weight: bold;"
                                                    visible="false">!</span>
                                            </div>
                                        </asp:Panel>
                                        <br class="yt-clear-both" />
                                    </div>
                                    <div id="confirmPassword" runat="server"  class="yt-margintop">
                                        <asp:Panel ID="PanelConfirmPassword" runat="server" Width="280px">
                                            <div>
                                                <span class="yt-Form-asterisk">*</span> <span>
                                                    <label class="yt-Form-Field-label">
                                                        Confirm Password: </label>
                                                </span>
                                            </div>
                                            <div class="yt-Div-txtbox">
                                                <asp:TextBox ID="txtConfirmPassword" runat="server" Width="240px" MaxLength="16"
                                                    TextMode="Password" TabIndex="3"></asp:TextBox>
                                                <span id="errcpassword" runat="server" style="color: #FF8000; font-size: Medium;
                                                    font-weight: bold;" visible="false">!</span>
                                            </div>
                                        </asp:Panel>
                                        <br class="yt-clear-both" />
                                    </div>
                                </div>
                                <div id="rightDiv" runat="server" style="float: left; width: 253px;">
                                    <div id="FirstName" runat="server">
                                        <asp:Panel ID="FirstNamePanel" runat="server" Width="280px">
                                            <div>
                                                <span class="yt-Form-asterisk">*</span> <span>
                                                    <label class="yt-Form-Field-label">
                                                        First Name: </label>
                                                </span>
                                            </div>
                                            <div class="yt-Div-txtbox">
                                                <asp:TextBox ID="txtFirstName" runat="server" Width="240px" MaxLength="50" TabIndex="4"></asp:TextBox>
                                                <span id="errfname" runat="server" style="color: #FF8000; font-size: Medium; font-weight: bold;"
                                                    visible="false">!</span>
                                            </div>
                                        </asp:Panel>
                                        <br class="yt-clear-both" />
                                    </div>
                                    <div id="LastName" runat="server"  class="yt-margintop">
                                        <asp:Panel ID="LastNamePanel" runat="server" Width="280px">
                                            <div>
                                                <span class="yt-Form-asterisk">*</span> <span>
                                                    <label class="yt-Form-Field-label">
                                                        Last Name: </label></span>
                                            </div>
                                            <div class="yt-Div-txtbox">
                                                <asp:TextBox ID="txtLastName" runat="server" Width="240px" MaxLength="50" TabIndex="5"></asp:TextBox>
                                                <span id="errlname" runat="server" style="color: #FF8000; font-size: Medium; font-weight: bold;"
                                                    visible="false">!</span>
                                            </div>
                                        </asp:Panel>
                                        <br class="yt-clear-both" />
                                    </div>
                                    <div id="Country"  class="yt-margintop">
                                        <div>
                                            <div>
                                                <span class="yt-Form-asterisk">*</span> <span>
                                                    <label class="yt-Form-Field-label">
                                                        Country: </label></span></div>
                                            <div class="yt-Div-txtbox">
                                                <asp:DropDownList ID="ddlCountry" runat="server" Width="246px" AutoPostBack="false" 
                                                    TabIndex="6" Height="22px">
                                                </asp:DropDownList>
                                                <span id="errcountry" runat="server" style="color: #FF8000; font-size: Medium; font-weight: bold;"
                                                    visible="false">!</span>
                                            </div>
                                        </div>
                                    </div>
                                   <br class="yt-clear-both" />
                                </div>
                                <br class="yt-clear-both" />
                            </div>
                            <br class="yt-clear-both" />
                            <div id="LowerDiv" style="margin-left: 15px; margin-top: 15px; width:550px;">
                                <asp:Panel ID="CommonPanel" runat="server" Width="488px">
                                    <span style="color: #FF8000; font-size: Medium; font-weight: bold;" id="errorVerification"
                                        runat="server" visible="false">!</span>
                                    <div>
                                        <span>
                                            <input type="checkbox" id="chkAgreeTermsUse" runat="server" tabindex="8" />
                                        </span><span class="yt-Form-asterisk">*</span> <span>
                                            <label class="yt-Form-Field-labelChkBox">
                                                I have read and agree to the
                                                 <a style="text-decoration:underline; cursor:pointer;" 
                                                 onclick="window.open('<%=Session["APP_BASE_DOMAIN"]%>termsofuse.aspx','Terms','width=900,height=750,scrollbars=yes,left=160,top=130')" >terms of use</a> 
                                                and the 
                                                <a style="text-decoration:underline; cursor:pointer;" onclick="window.open('<%=Session["APP_BASE_DOMAIN"]%>privacy.aspx','Privacy','width=900,height=750,scrollbars=yes,left=160,top=130')" >privacy policy</a>.</label>
                                        </span>
                                    </div>
                                    <div style="width: 505px; margin-top: 10px;">
                                        <div style="float: left; width: 25px; margin-top:-2px;">
                                            <input type="checkbox" id="chkAgreeReceiveNewsletters" runat="server" tabindex="9" />
                                        </div>
                                        <div style="float: left; width: 478px;">
                                            <label class="yt-Form-Field-labelChkBox">
                                                I would like to receive periodic newsletters, including tips and tricks when creating
                                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>s, from Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>.</label>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <br class="yt-clear-both" />
                            <div class="yt-Form-SignUpBtn" style="padding-right:20px; padding-bottom:20px;">
                                <asp:LinkButton ID="btnSignUp" CssClass="yt-Button" TabIndex="10" runat="server"
                                    OnClick="btnSignUp_Click">
                                    <span class="yt-ButtonLeftCap"></span>
Sign up to Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
<span class="yt-ButtonRightCap"></span></asp:LinkButton>
                                    

                                    
                                    
                                    
                            </div>
                        </fieldset>
                    </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    </form>
    <!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
    <script type="text/javascript" src="../assets/scripts/BrowserOrTabCloseHandler.js"></script>
</body> 
</html>
