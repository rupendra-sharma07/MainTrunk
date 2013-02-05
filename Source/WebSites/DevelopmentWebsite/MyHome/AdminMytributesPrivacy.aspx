<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminMytributesPrivacy.aspx.cs"
    Inherits="MyHome_AdminMytributesPrivacy" %>

<%@ Register Src="../UserControl/Footer.ascx" TagName="Footer" TagPrefix="uc3" %>
<%@ Register Src="../UserControl/TributeBlog.ascx" TagName="TributeBlog" TagPrefix="uc2" %>
<%@ Register Src="../UserControl/Inbox.ascx" TagName="Inbox" TagPrefix="uc1" %>
<%@ Register Src="../UserControl/LeftFeaturedPanel.ascx" TagName="LeftFeaturedPanel"
    TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head id="head1" runat="server">
    <title>My Tributes</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Content-Language" content="en-ca" />
    <meta http-equiv="imagetoolbar" content="false" />
    <meta name="robots" content="index,follow" />
    <meta name="MSSmartTagsPreventParsing" content="true" />
    <!-- really basic, generic html class stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=Session["SECURED_APP_SCRIPT_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/default.css" />
    <!-- default grid layout stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=Session["SECURED_APP_SCRIPT_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/layouts/centered1024_12.css" />
    <!-- print-friendly stylesheet -->
    <link rel="stylesheet" type="text/css" media="print" href="<%=Session["SECURED_APP_SCRIPT_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/print.css" />
    <!-- screen elements stylesheet should be here -->
    <link rel="stylesheet" type="text/css" href="<%=Session["SECURED_APP_SCRIPT_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/screen.css" />
    <!-- larger text stylesheets -->
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="<%=Session["SECURED_APP_SCRIPT_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/medium_text.css"
        title="medium_text" />
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="<%=Session["SECURED_APP_SCRIPT_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/large_text.css"
        title="large_text" />
    <!-- Admin-specific elements stylesheet should be here -->
    <link rel="stylesheet" type="text/css" href="<%=Session["SECURED_APP_SCRIPT_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/admin.css" />
    <!-- Website Favorite Icon -->
    <link rel="Shortcut Icon" type="image/x-icon" href="<%=Session["SECURED_APP_SCRIPT_PATH"]%>assets/images/favicon.ico" />
    <!-- JS libraries -->

    <script type="text/javascript" src="<%=Session["SECURED_APP_SCRIPT_PATH"]%>assets/scripts/mootools-1.11.js"></script>

    <script type="text/javascript" src="<%=Session["SECURED_APP_SCRIPT_PATH"]%>assets/scripts/global.js"></script>

    <script type="text/javascript" src="<%=Session["SECURED_APP_SCRIPT_PATH"]%>assets/scripts/styleSwitcher.js"></script>

    <script type="text/javascript" src="<%=Session["SECURED_APP_SCRIPT_PATH"]%>assets/scripts/modalbox.js"></script>

    <script type="text/javascript" src="<%=Session["SECURED_APP_SCRIPT_PATH"]%>assets/scripts/admin.js"></script>

    <script type="text/javascript" src="<%=Session["SECURED_APP_SCRIPT_PATH"]%>Common/JavaScript/CreditCardValidation.js"></script>

    <script type="text/javascript" src="<%=Session["SECURED_APP_SCRIPT_PATH"]%>Common/JavaScript/Common.js"></script>

    <!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
    <script type="text/javascript" src="<%=Session["SECURED_APP_SCRIPT_PATH"]%>assets/scripts/BrowserOrTabCloseHandler.js"></script>

    <script type="text/javascript">
    
    /* NOTE: may want to move this to an external .js */
    App_Domain = "<%=Session["SECURED_APP_SCRIPT_PATH"]%>";
    InitForm = function() {
        $$('.availabilityNotice').each( function(a) {
		    a.innerHTML = '';
		    a.className = 'availabilityNotice';
	    });
    }
    
    window.addEvent('load', function() {
    
        if($('rdoPersonalAccount')) {
        /* attach personal/business toggle events */
		    $('rdoPersonalAccount').addEvent('click', function() {
			    $$('.business').each( function(a) {
				    a.style.display = 'none';
			    });
			    $$('.personal').each( function(a) {
				    a.style.display = '';
			    });
    			
			    $('yt-SignUpFormContainer').style.display = 'block';
			    InitForm();
		    });
		    SetValidationClear();
	    }
    	
        if($('rdoBusinessAccount')) {
		    $('rdoBusinessAccount').addEvent('click', function() {
			    $$('.personal').each( function(a) {
				    a.style.display = 'none';
			    });
			    $$('.business').each( function(a) {
				    a.style.display = '';
			    });
    			
			    $('yt-SignUpFormContainer').style.display = 'block';
			    InitForm();
		    });
	    }
		
		hideWideRows();
		
	});
	
    </script>

    <script type="text/javascript">    
    
	
	function HideAutorenew()
	{
	    var div1=$('divYearlyAutoRenew');
	    var div2=$('divNotifyBeforeRenew');
	    if(div1)
	    {
	      div1.style.visibility = 'hidden';
	      //div1.innerHTML='';
	    }	
	     if(div2)
	    {
	      div2.style.visibility = 'hidden';
	      //div2.innerHTML='';
	    }	
	    doModalUpgradeExtend();
	}
	
 function SetValidation()
 {  
    var required_validator = document.getElementById('<%= PortalValidationSummary.ClientID %>');
    required_validator.validationGroup = "Email";   
 }
 
 function SetDetailsValidation()
 {  
    var required_validator = document.getElementById('<%= DonationValidationSummary.ClientID %>');
    required_validator.validationGroup = "TributeDetails";   
 }
 
	function SetValidationClear()
	{
	 var required_validator = document.getElementById('<%= PortalValidationSummary.ClientID %>');
     required_validator.validationGroup ="";  
	}
function ValidateAccountType(source, args)
 {
      var bool=false;
           var rdb1=$('rdoMembershipLifetime');
           var rdb2=$('rdoMembershipYearly');
           if((rdb1)&&(rdb2))
           {
                if((!rdb1.checked)&&(!rdb2.checked))
                {             
                    bool=false;   
                }
                else
                {
               // doModalUpgradeExtend();
                    bool=true;
                }
            }            
            else if(rdb1)
            {
                if(!rdb1.checked)
                {             
                  bool=false;   
                }
                else
                {
                  // doModalUpgradeExtend();
                    bool=true;
                }
            }
       args.IsValid=bool;
 }
 
  
 function Check(rdb)
 {
  var rdb1=$('hfPaymentMethod'); 
   rdb1.value= rdb.value;
 }
  
 function validatePaymentMethod(source, args)
 {
      var bool=false;
           var rdb1=$('rdoCCVisa');
           var rdb2=$('rdoCCAmex');
           var rdb3=$('rdoCCDiscover');
           var rdb4=$('rdoCCMasterCard');
           if((!rdb1.checked)&&(!rdb2.checked)&&(!rdb3.checked)&&(!rdb4.checked))           
           bool=false;                      
           else
           bool=true;
           
       args.IsValid=bool;
           
 }
 
 function validateCreditCardLength(source, args)
 {
    
        var bool=false;
        var rdb1=$('rdoCCVisa');
        var rdb2=$('rdoCCAmex');
        var rdb3=$('rdoCCDiscover');
        var rdb4=$('rdoCCMasterCard');
        var val=document.getElementById('<%=txtCCNumber.ClientID%>').value;
        if(rdb1.checked ||rdb4.checked ||rdb3.checked )
        {
       
            if(val.length!=16)
            {
                bool=false;
            }
            else
                bool=true;
        }        
        else
        {
             if(val.length!=15 )
            {
                bool=false;
            }
            else
                bool=true;
        }

        args.IsValid=bool;
 }
 
 
 function validateCreditCardVerificationLength(source, args)
 {
        var bool=false;
        var rdb1=$('rdoCCVisa');
        var rdb2=$('rdoCCAmex');
        var rdb3=$('rdoCCDiscover');
        var rdb4=$('rdoCCMasterCard');
        var val=document.getElementById('<%=txtCCVerification.ClientID%>').value;
    
        if(rdb1.checked ||rdb4.checked ||rdb3.checked )
        {
            if(val.length!=3)
            {
                bool=false;
            }
            else
                bool=true;
        }        
        else
        {
             if(val.length!=4)
            {
                bool=false;
            }
            else
                bool=true;
        }
        
        args.IsValid=bool;
}
 
 function validateExpMonth(source, args)
{
  var bol=true;
  var month =  document.getElementById('<%=ddlCCMonth.ClientID%>'); 
  var year =  document.getElementById('<%=txtCCYear.ClientID%>'); 
  var validat =  document.getElementById('<%=CustomValidator3.ClientID%>');   
  args.IsValid=ExpMonthvalidate(month,year,validat); 
}

function PhoneNumbervalidation(source, args)
{     
     var number1=  document.getElementById('<%= txtPhoneNumber1.ClientID %>');
     var number2=  document.getElementById('<%= txtPhoneNumber2.ClientID %>');
     var number3=  document.getElementById('<%= txtPhoneNumber3.ClientID %>');
     var validator=  document.getElementById('<%= cvPhoneNumber.ClientID %>');     
     
     args.IsValid= PhoneNumberValidate(number1,number2,number3,validator);     
}
  
  
  function CCardLength(source, args)
{
 var CCNumber =  document.getElementById('<%=txtCCNumber.ClientID%>'); 
 args.IsValid=CreditCardCLength(CCNumber);
}
function CVCLength(source, args)
{
 var CCNumber =  document.getElementById('<%=txtCCVerification.ClientID%>');    
 args.IsValid= CVCLengthCC(CCNumber)
}
 function ValidateTandCs(source, args)
 {
        args.IsValid = document.getElementById('<%= chkAgree.ClientID %>').checked;        
         
 }
 
 function ValidateTributeAdmins(source, args)
 {
       var bool=false;
       var admins=document.getElementById('<%= cblstAdminis.ClientID %>');
       if(admins)
       {
        var chkBoxCount= admins.getElementsByTagName("input");
        for(var i=0;i<chkBoxCount.length;i++) 
        {
          if(chkBoxCount[i].checked==true)          
          {            
           bool= true;
          }
        }
       }
     args.IsValid=bool;
 }
	
function ValidateTributeName(source, args)
{
    var txtTributeName =  document.getElementById('<%=txtTributeName.ClientID%>'); 
    args.IsValid=TributeNameValidate(txtTributeName.value);    
    //onload="SetValidationClear();"
}


//method that verifies the conditions for the deletion of a donation box
function DonationChecked()
{
    var donation = document.getElementById('<%=divDonation.ClientID %>');
 
    //if donation box exists for the tribute   
    if(donation != null)
        {   
            var check = document.getElementById('<%=chkDonation.ClientID %>').checked;
            
            //if donation box selected to be deleted i.e. checkbox is checked and display the confirmation message
            if(check)
                return true;
            else //no delete
                return false;
        }
    else //no donation box hence no confirmation message
        {
            return false; 
        }
}

//added to display confirmation on deleting a donation box
function ConfirmDelete()
{
    if (Page_ClientValidate("TributeDetails"))
    {
        //if Donation box exists and deletion of Donation box is selected then show a popup
        if (DonationChecked())
        {
            doModalDonationDeleteConfirm();
            return false;
        }
        //otherwise continue with the save of other tribute details
        else
        {
            return true;
        }
    }
    //do nothing in case of failure of validations
    return false;
}
    </script>
<!--#include file="../analytics.asp"-->
</head>
<body>
    <form action="" runat="server">
        <asp:HiddenField ID="hfPaymentMethod" runat="server" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="divShowModalPopup"></div> 
        <div id="yt-UpgradeExtendContainer">
            <div id="yt-UpgradeExtendError">
                <asp:ValidationSummary CssClass="yt-Error" ID="VSAccountTrpe" ValidationGroup="accounttype"
                    runat="server" Width="630px" HeaderText="<h2>Oops - there is a problem with some account type information.</h2>                                                             <h3>Please correct the errors below:</h3>"
                    ForeColor="Black" />
            </div>
            <div id="yt-UpgradeExtendContent" class="yt-ModalWrapper">
                <div class="yt-Panel-Primary">
                    <h2 id="extendTrb" runat="server">
                        Upgrade/Extend Tribute</h2>
                    <h4 id="h4ym" runat="server" class="yt-InfoBox" >
                        You have chosen to pay for a <span id="yt-MembershipType"></span>membership for
                        the wedding <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%> to John Smith &amp; Mary Walters</h4>
                    <fieldset class="yt-Form">
                        <p>
                            Please enter the following payment information:</p>
                        <p class="yt-requiredFields">
                            <strong>Required fields are indicated with <em class="required">* </em></strong>
                        </p>
                        <div class="yt-Form-Field">
                            <asp:UpdatePanel ID="PnlCoupon" runat="server">
                                <ContentTemplate>
                                    <label for="txtCouponCode">
                                        Enter your coupon code, if you have one:</label>
                                    <asp:TextBox ID="txtCouponCode" runat="server" CssClass="yt-Form-Input-Long" MaxLength="18"></asp:TextBox>
                                    <asp:LinkButton ID="lbtnValidateCoupon" OnClick="lbtnValidateCoupon_Click" runat="server"
                                        CausesValidation="False" CssClass="yt-checkCoupon">Validate Coupon</asp:LinkButton>
                                    <span id="spanCoupon" class="availabilityNotice" runat="server"></span>
                                    <div class="hint">
                                        If you have a coupon code enter it here and click "Validate Coupon". If the coupon
                                        code is correct, the discount will be applied to your total at the bottom of the
                                        page<span class="hintPointer"></span></div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <fieldset class="yt-PaymentSelection yt-CompactRadioList">
                            <legend>* Select your payment method:</legend>
                            <div class="yt-Form-Field yt-Form-Field-Radio" id="yt-CCVisa">
                                <input type="radio" onclick='Check(this);' name="rdoCCType" id="rdoCCVisa" value="Visa" />
                                <label for="rdoCCVisa">
                                    Visa</label>
                            </div>
                            <div class="yt-Form-Field yt-Form-Field-Radio" id="yt-CCMasterCard">
                                <input type="radio" onclick='Check(this);' name="rdoCCType" id="rdoCCMasterCard"
                                    value="MasterCard" />
                                <label for="rdoCCMasterCard">
                                    Master Card</label>
                            </div>
                            <div class="yt-Form-Field yt-Form-Field-Radio" id="yt-CCAmex">
                                <input type="radio" onclick='Check(this);' name="rdoCCType" id="rdoCCAmex" value="Amex" />
                                <label for="rdoCCAmex">
                                    American Express</label>
                            </div>
                            <div class="yt-Form-Field yt-Form-Field-Radio" id="yt-CCDiscover">
                                <input type="radio" onclick='Check(this);' name="rdoCCType" id="rdoCCDiscover" value="Discover" />
                                <label for="rdoCCDiscover">
                                    Discover</label>
                            </div>
                        </fieldset>
                        <div class="yt-Form-Field">
                            <label>
                                * Credit Card Number:</label>
                            <asp:TextBox ID="txtCCNumber" CssClass="yt-Form-Input-Long" runat="server" MaxLength="15"
                                Width="280px" ValidationGroup="accounttype"></asp:TextBox><asp:RequiredFieldValidator
                                    ValidationGroup="accounttype" ID="rfvCCNumber" Font-Bold="True" Font-Size="Medium"
                                    ForeColor="#FF8000" ControlToValidate="txtCCNumber" runat="server" Text="!" ErrorMessage="Credit Card Number is a required field."
                                    Width="1px"></asp:RequiredFieldValidator>
                            <%--  <asp:CustomValidator ID="cvCreditCardNumber" runat="server" Font-Bold="True" Font-Size="Medium"
                                ForeColor="#FF8000" ClientValidationFunction="validateCreditCardLength" Text="!"
                                EnableClientScript="true" ErrorMessage="Please enter a valid Credit Card Number."
                                Width="1px"></asp:CustomValidator>--%>
                            <asp:CustomValidator ID="cvCCNumber" ValidationGroup="accounttype" runat="server"
                                Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" Text="!" ClientValidationFunction="CCardLength"
                                ErrorMessage="Please enter a valid credit card number. " Width="1px"></asp:CustomValidator>
                        </div>
                        <div class="yt-Form-Field">
                            <label>
                                * Card Verification Code (CVC):</label>
                            <asp:TextBox ID="txtCCVerification" ValidationGroup="accounttype" CssClass="yt-Form-Input-Short"
                                runat="server" MaxLength="4" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ValidationGroup="accounttype" ControlToValidate="txtCCVerification"
                                Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ID="rfvCCVerification"
                                Text="!" runat="server" ErrorMessage="Card Verification Code (CVC) is a required field."
                                Width="1px"></asp:RequiredFieldValidator>
                            <%--<asp:CustomValidator ID="cvCreditCardVerification" runat="server" Font-Bold="True"
                                Font-Size="Medium" ForeColor="#FF8000" ClientValidationFunction="validateCreditCardVerificationLength"
                                Text="!" EnableClientScript="true" ErrorMessage="Please enter a valid Credit Card Verification Number."
                                Width="1px"></asp:CustomValidator>--%>
                            <asp:CustomValidator ID="CustomValidator4" ValidationGroup="accounttype" runat="server"
                                Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" Text="!" ClientValidationFunction="CVCLength"
                                ErrorMessage="Please enter a valid card verification code (CVC). " Width="1px"></asp:CustomValidator>
                            <div class="hint">
                                The CVC is located on the back of MasterCard, Visa and Discover credit cards and
                                is a separate group of 3 digits to the right of the signature strip. On American
                                Express cards, the CVC is a separate group of 4 digits on the front right of the
                                card.<span class="hintPointer"></span></div>
                        </div>
                        <fieldset class="yt-Date-Fields">
                            <legend><em class="required">* </em>Expiry Date:</legend>
                            <div class="yt-Form-Field">
                                <asp:DropDownList ID="ddlCCMonth" runat="server" Width="132px">
                                    <asp:ListItem Value="--"></asp:ListItem>
                                    <asp:ListItem Value="1">January</asp:ListItem>
                                    <asp:ListItem Value="2">February</asp:ListItem>
                                    <asp:ListItem Value="3">March</asp:ListItem>
                                    <asp:ListItem Value="4">April</asp:ListItem>
                                    <asp:ListItem Value="5">May</asp:ListItem>
                                    <asp:ListItem Value="6">June</asp:ListItem>
                                    <asp:ListItem Value="7">July</asp:ListItem>
                                    <asp:ListItem Value="8">August</asp:ListItem>
                                    <asp:ListItem Value="9">September</asp:ListItem>
                                    <asp:ListItem Value="10">October</asp:ListItem>
                                    <asp:ListItem Value="11">November</asp:ListItem>
                                    <asp:ListItem Value="12">December</asp:ListItem>
                                </asp:DropDownList>
                                <label>
                                    Month</label>
                            </div>
                            <div class="yt-Form-Field">
                                <asp:TextBox ID="txtCCYear" ValidationGroup="accounttype" CssClass="yt-Form-Input-Short"
                                    MaxLength="4" runat="server"></asp:TextBox>
                                <asp:CustomValidator ID="CustomValidator3" ValidationGroup="accounttype" runat="server"
                                    Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ClientValidationFunction="validateExpMonth"
                                    Text="!" ErrorMessage="Expiry Date is a required field." Width="1px"></asp:CustomValidator>
                                <asp:CompareValidator ID="cpvtxtCCYear" Font-Bold="True" ValidationGroup="accounttype"
                                    Operator="GreaterThanEqual" Font-Size="Medium" ForeColor="#FF8000" runat="server"
                                    ErrorMessage="Expiry Date cannot be less than current date." ControlToValidate="txtCCYear"
                                    Width="1px">!</asp:CompareValidator>
                                <asp:CustomValidator ID="cvCCYear" ValidationGroup="accounttype" Font-Bold="True"
                                    Font-Size="Medium" ForeColor="#FF8000" runat="server" ControlToValidate="txtCCYear"
                                    Text="!" ErrorMessage="Please enter a valid expiry date." Width="1px"></asp:CustomValidator>
                                <span style="color: #FF8000; font-size: Medium; font-weight: bold;" id="SpanExpirDate"
                                    runat="server" visible="false">!</span>
                                <label>
                                    Year</label>
                            </div>
                        </fieldset>
                        <div class="yt-Form-Field">
                            <label>
                                <em class="required">* </em>Name on Card:</label>
                            <asp:TextBox ID="txtCCName" ValidationGroup="accounttype" CssClass="yt-Form-Input-Long"
                                runat="server" MaxLength="50" Width="280px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCCName" ValidationGroup="accounttype" Font-Bold="True"
                                Font-Size="Medium" ForeColor="#FF8000" Text="!" runat="server" ErrorMessage="Name on Card is a required field."
                                ControlToValidate="txtCCName"></asp:RequiredFieldValidator>
                        </div>
                        <div class="yt-Form-Field">
                            <label>
                                <em class="required">* </em>Billing Address:</label>
                            <asp:TextBox ID="txtCCBillingAddress" ValidationGroup="accounttype" CssClass="yt-Form-Input-Long"
                                runat="server" MaxLength="50" Width="280px"></asp:TextBox>
                            <asp:RegularExpressionValidator ValidationGroup="accounttype" ID="RegularExpressionValidator1"
                                runat="server" ControlToValidate="txtCCBillingAddress" ErrorMessage="Billing Address can only contain letters,numbers,'-' and '#'"
                                Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9,\#,\-,\s]*$"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                    ID="rfvCCBillingAddress" runat="server" ValidationGroup="accounttype" Text="!"
                                    Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ControlToValidate="txtCCBillingAddress"
                                    ErrorMessage="Billing Address is a required field."></asp:RequiredFieldValidator>
                        </div>
                        <div class="yt-Form-Field">
                            <asp:TextBox ID="txtCCBillingAddress2" ValidationGroup="accounttype" CssClass="yt-Form-Input-Long"
                                runat="server" MaxLength="50" Width="280px"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ValidationGroup="accounttype"
                                runat="server" ControlToValidate="txtCCBillingAddress2" ErrorMessage="Billing Address only contain letters,numbers,'-' ans '#'"
                                Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9,\#,\-,\s]*$"></asp:RegularExpressionValidator>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <div class="yt-Form-Field">
                                    <label>
                                        <em class="required">* </em>Country:</label>
                                    <asp:DropDownList ID="ddlCCCountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCCCountry_SelectedIndexChanged"
                                        Width="285px">
                                    </asp:DropDownList>
                                </div>
                                <div class="yt-Form-Field">
                                    <label>
                                        State/Province:</label>
                                    <asp:DropDownList ID="ddlCCStateProvince" runat="server" Width="285px">
                                    </asp:DropDownList>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <div class="yt-Form-Field">
                            <label>
                                <em class="required">* </em>City:</label>
                            <asp:TextBox ID="txtCCCity" CssClass="yt-Form-Input-Long" ValidationGroup="accounttype"
                                runat="server" Width="280px" MaxLength="50"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revtxtCCCity" runat="server" ControlToValidate="txtCCCity"
                                ErrorMessage="City only contain letters,numbers,'-' and '#'" Font-Bold="True"
                                Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationGroup="accounttype"
                                ValidationExpression="^[a-zA-Z0-9,\#,\-,\s]*$"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                    ID="rfvCCCity" runat="server" Text="!" ControlToValidate="txtCCCity" ErrorMessage="City is a required field."
                                    Font-Bold="True" Font-Size="Medium" ValidationGroup="accounttype" ForeColor="#FF8000"></asp:RequiredFieldValidator>
                        </div>
                        <div class="yt-Form-Field">
                            <label>
                                <em class="required">* </em>Zip Code/Postal Code:</label>
                            <asp:TextBox ID="txtCCZipCode" ValidationGroup="accounttype" CssClass="yt-Form-Input"
                                runat="server" MaxLength="10"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCCZipCode" ValidationGroup="accounttype" runat="server"
                                Text="!" ControlToValidate="txtCCZipCode" ErrorMessage="Zip Code/Postal Code is a required field."
                                Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"></asp:RequiredFieldValidator>
                        </div>
                        <div class="yt-Form-Field">
                            <label>
                                <em class="required">* </em>Phone Number:</label>
                            (<asp:TextBox ID="txtPhoneNumber1" runat="server" Width="34px" MaxLength="3" TabIndex="15"
                                CssClass="yt-Form-Input-Long"></asp:TextBox>)
                            <asp:TextBox ID="txtPhoneNumber2" runat="server" Width="34px" MaxLength="3" CssClass="yt-Form-Input-Long"
                                TabIndex="16"></asp:TextBox>
                            -
                            <asp:TextBox ID="txtPhoneNumber3" runat="server" Width="40px" MaxLength="4" CssClass="yt-Form-Input-Long"
                                TabIndex="17"></asp:TextBox>
                            <asp:CustomValidator ID="cvPhoneNumber" ValidationGroup="accounttype" Visible="true"
                                runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ErrorMessage="Enter valid phone number.Format should be XXX-XXX-XXXX."
                                ClientValidationFunction="PhoneNumbervalidation">!</asp:CustomValidator>
                            <asp:CustomValidator ID="cvAcceptPolicies" Text="!" ForeColor="Transparent" ErrorMessage="Please accept that you have read and agree to the terms of use, cancellation/refund policy and the privacy policy."
                                runat="server" ValidationGroup="accounttype" ClientValidationFunction="ValidateTandCs"
                                Width="1px"></asp:CustomValidator>
                        </div>
                        <div class="yt-Form-Field yt-Form-Field-Radio" id="divYearlyAutoRenew" runat="server">
                            <asp:RadioButton ID="rdoYearlyAutoRenew" Text="I want this tribute to be renewed automatically on a yearly basis."
                                GroupName="rdoRenew" runat="server" />
                        </div>
                        <div class="yt-Form-Field yt-Form-Field-Radio" id="divNotifyBeforeRenew" runat="server">
                            <asp:RadioButton ID="rdoNotifyBeforeRenew" Text="I do not want this tribute to be renewed automatically on a yearly basis, but I
                                                                    will be notified when the account is near to expiry."
                                GroupName="rdoRenew" runat="server" Checked="True" />
                        </div>
                        <div class="yt-Form-Field yt-Form-Field-Checkbox">
                            <asp:CheckBox ID="chkSaveBillingInfo" Text="I would like to save the above billing information in my profile."
                                runat="server" />
                        </div>
                        <div class="yt-Form-Field yt-Form-Field-Checkbox">
                            <asp:CheckBox ID="chkAgree" runat="server" Text="I have read and agree to the <a href='termsofuse.aspx' target='_blank' >terms of use</a>, the cancellation/refund
                                                                    policy (outlined in the terms of use) and the <a href='privacy.aspx'  target='_blank' >privacy policy</a>."  Checked="True" />
                        </div>
                        <div class="yt-InfoBox" id="yt-PaymentTotal">
                            You will be charged: <span id="BillingTotal" runat="server"></span>
                        </div>
                        <p>
                            If you have reviewed all of the above information and it is correct, you must be
                            ready to...</p>
                        <div class="yt-Form-Buttons">
                            <div class="yt-Form-Submit">
                                <asp:LinkButton ID="lbtnPay" CssClass="yt-Button yt-ArrowButton" ValidationGroup="accounttype"
                                    runat="server" OnClick="lbtnPay_Click">Pay!</asp:LinkButton>
                            </div>
                        </div>
                    </fieldset>
                    <!--yt-Form-->
                </div>
            </div>
        </div>
        <div class="yt-Container yt-Admin yt-MyTributeSettings">
            <div class="yt-HeaderContainer">
                <div class="yt-Header">
                    <a href="<%=Session["APP_BASE_DOMAIN"]%>home.aspx" title="Return to Your Tribute Home Page"
                        class="yt-Logo"></a>
                    <div class="yt-HeaderControls">
                        <div class="yt-NavHeader">
                            <div class="yt-UserInfo">
                                Viewing: My Account
                                <uc1:Inbox ID="Inbox1" runat="server" />
                                <span id="Usernamelong" runat="server"></span><span id="spanLogout" runat="server"></span>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="yt-Tools">
                            <div id="yt-TypeSizeControl" class="yt-TypeSizeControl">
                                <span class="floatLeft">Text Size:&#160;</span> <a href="javascript:void(0);" class="large"
                                    title="Large Text">a</a> <a href="javascript:void(0);" class="medium" title="Medium Text">
                                        a</a> <a href="javascript:void(0);" class="small" title="Small Text">a</a>
                            </div>
                            <a href="<%=Session["APP_BASE_DOMAIN"]%>advancedsearch.aspx" id="yt_SearchLauncher" class="yt-SearchLauncher hideText">
                                Find a Tribute</a>
                            <div id="yt-Search">
                                <h2>
                                    <asp:Label ID="lblFindTribute" runat="server" Text="Find a Tribute"></asp:Label></h2>
                                <fieldset>
                                    <div class="yt-Form-Field yt-SearchKeywords">
                                        <label id="lblSearchFor" runat="server" for="txtSearchKeywords">
                                            Search for:</label>
                                        <asp:TextBox ID="txtSearchKeyword" runat="server" Text="Enter the name of a Tribute"></asp:TextBox>
                                    </div>
                                    <asp:ImageButton ID="btnSearchSubmit" CssClass="yt-Search-Submit" runat="server"
                                        CausesValidation="true" ValidationGroup="ValidSearch" ImageUrl="../assets/images/btn_go.gif"
                                        AlternateText="Search Tributes" OnClick="btnGo_Click" />
                                    <div class="columnLeft">
                                        <div class="yt-Form-Field yt-Form-Field-Checkbox">
                                            <asp:RadioButton ID="rdoSearch_All" GroupName="rdoSearchGroup" runat="server" TabIndex="1"
                                                Checked="true" />
                                            <label id="lblSearch_All" runat="server">
                                                All Tributes</label>
                                        </div>
                                        <div class="yt-Form-Field yt-Form-Field-Checkbox">
                                            <asp:RadioButton ID="rdoSearch_Anniversary" GroupName="rdoSearchGroup" runat="server"
                                                TabIndex="2" Checked="false" />
                                            <label id="lblSearch_Anniversary" runat="server">
                                                Anniversary Tributes</label>
                                        </div>
                                        <div class="yt-Form-Field yt-Form-Field-Checkbox">
                                            <asp:RadioButton ID="rdoSearch_Birthday" GroupName="rdoSearchGroup" runat="server"
                                                TabIndex="3" Checked="false" />
                                            <label id="lblSearch_Birthday" runat="server">
                                                Birthday Tributes</label>
                                        </div>
                                        <div class="yt-Form-Field yt-Form-Field-Checkbox">
                                            <asp:RadioButton ID="rdoSearch_Graduation" GroupName="rdoSearchGroup" runat="server"
                                                TabIndex="4" Checked="false" />
                                            <label id="lblSearch_Graduation" runat="server">
                                                Graduation Tributes</label>
                                        </div>
                                    </div>
                                    <div class="columnRight">
                                        <div class="yt-Form-Field yt-Form-Field-Checkbox">
                                            <asp:RadioButton ID="rdoSearch_Memorial" GroupName="rdoSearchGroup" runat="server"
                                                TabIndex="5" Checked="false" />
                                            <label id="lblSearch_Memorial" runat="server">
                                                Memorial Tributes</label>
                                        </div>
                                        <div class="yt-Form-Field yt-Form-Field-Checkbox">
                                            <asp:RadioButton ID="rdoSearch_NewBaby" GroupName="rdoSearchGroup" runat="server"
                                                TabIndex="6" Checked="false" />
                                            <label id="lblSearch_NewBaby" runat="server">
                                                New Baby Tributes</label>
                                        </div>
                                        <div class="yt-Form-Field yt-Form-Field-Checkbox">
                                            <asp:RadioButton ID="rdoSearch_Wedding" GroupName="rdoSearchGroup" runat="server"
                                                TabIndex="7" Checked="false" />
                                            <label id="lblSearch_Wedding" runat="server">
                                                Wedding Tributes</label>
                                        </div>
                                        <div class="yt-SearchAdvancedLink">
                                            <asp:HyperLink ID="lnkAdvanceSearch" runat="server">Advanced Search</asp:HyperLink>
                                        </div>
                                    </div>
                                </fieldset>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtSearchKeyword"
                                    ValidationGroup="ValidSearch" SetFocusOnError="True" ValidationExpression="^[a-zA-Z0-9\s\?\!\-\@\-\.\:\;\=\+\[\]_\{\}\,\%\(\)\/\&amp;]*$">Please enter valid search keyword</asp:RegularExpressionValidator>
                                <a id="lnkClose" runat="server" href="javascript:void(0);" class="yt-MiniButton yt-CloseSearch"
                                    onclick="yt_Search.toggle();">Close</a>
                                <div class="hack-clearBoth">
                                </div>
                            </div>
                        </div>
                        <!--yt-Tools-->
                    </div>
                    <!--yt-HeaderControls-->
                </div>
                <!--yt-Header-->
            </div>
            <!--yt-HeaderContainer-->
            <div class="hack-clearBoth">
            </div>
            <div class="yt-ContentContainer">
                <div class="yt-ContentContainerInner">
                    <div class="yt-ContentPrimaryContainer">
                        <div class="yt-ContentPrimary">
                            <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>--%>
                            <div id="errormsg" runat="server" visible="false">
                            </div>
                            <asp:ValidationSummary CssClass="yt-Error" ID="PortalValidationSummary" runat="server"
                                Width="630px" HeaderText="   <h2>Oops - there is a problem with tribute updation.</h2>                                                             <h3>Please correct the errors below:</h3>"
                                ForeColor="Black" />
                            <asp:ValidationSummary CssClass="yt-Error" ID="DonationValidationSummary" runat="server"
                                ValidationGroup="TributeDetails" Width="630px" HeaderText="   <h2>Oops - there is a problem with tribute updation.</h2>                                                             <h3>Please correct the errors below:</h3>"
                                ForeColor="Black" />
                            <div class="yt-AdminMain">
                                <ul class="yt-AdminNavPrimary">
                                    <li class="yt-Selected"><a id="lnkTributes" runat="server" >My Tributes</a></li>
                                    <li id="limyfavourite" runat="server"><a href="favorites.aspx">My Favorites</a></li>
                                    <li><a href="inbox.aspx">Inbox</a></li>
                                    <li><a href="userevents.aspx">Events</a></li>
                                    <li id="yt-MyProfileTab"><a href="adminprofilesettings.aspx">My Profile</a></li>
                                </ul>
                                <div class="yt-Panel-Primary">
                                    <div id="yt-TributeList">
                                        <div class="yt-AdminHeader">
                                            <a id="lnkTributesList" runat="server"  class="yt-BackLink">Back to <%= ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> List</a>
                                        </div>
                                        <table cellspacing="0" class="yt-AdminTable">
                                            <thead>
                                                <tr>
                                                    <th>
                                                        My <%= ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>s</th>
                                                    <th>
                                                        <%= ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> Type</th>
                                                    <th>
                                                        Date Created</th>
                                                    <th>
                                                        Account Type </br> (Expiry Date)</th>
                                                    <th>
                                                        Visits</th>
                                                    <th>
                                                        Email Alerts</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <tr>
                                                    <td style="height: 35px">
                                                        <asp:LinkButton ID="lbtnmytribute" runat="server" OnClick="lbtnmytribute_Click" CausesValidation="False">LinkButton</asp:LinkButton>
                                                    </td>
                                                    <td style="height: 35px">
                                                        <asp:Label ID="lbltributetype" runat="server" Text="Label"></asp:Label>
                                                    </td>
                                                    <td style="height: 35px">
                                                        <asp:Label ID="lblCreateddate" runat="server" Text="Label"></asp:Label>
                                                    </td>
                                                    <td style="height: 35px">
                                                        <asp:LinkButton ID="lbtnexpiresdate" runat="server" OnClick="lbtnexpiresdate_Click"
                                                            CausesValidation="False">LinkButton</asp:LinkButton>
                                                    </td>
                                                    <td style="height: 35px">
                                                        <asp:Label ID="lblVisits" runat="server" Text="Label"></asp:Label>
                                                    </td>
                                                    <td style="height: 35px">
                                                        <asp:CheckBox ID="CheckBoxEmailErt" runat="server" Text=" " AutoPostBack="True" OnCheckedChanged="CheckBoxEmailErt_CheckedChanged" />
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                    <!-- yt-TributeList -->
                                    <div>
                                        <asp:MultiView ID="ManageTribute" runat="server">
                                            <asp:View ID="ViewPrivacy" runat="server">
                                                <ul class="yt-AdminNavSecondary">
                                                    <li class="yt-Selected"><a href="javascript:void(0);">Privacy</a></li><li>
                                                        <asp:LinkButton ID="lbtnAdministrators12" runat="server" OnClick="lbtnAdministrators12_Click"
                                                            CausesValidation="False">Administrators</asp:LinkButton></li><li>
                                                                <asp:LinkButton ID="lbtnAccountType13" runat="server" OnClick="lbtnAccountType13_Click"
                                                                    CausesValidation="False">Account Type &amp; Renewal Info</asp:LinkButton></li><li>
                                                                        <asp:LinkButton ID="lbtnTributeDetails14" runat="server" OnClick="lbtnTributeDetails14_Click"
                                                                            CausesValidation="False"><%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> Details</asp:LinkButton></li><li>
                                                                                <asp:LinkButton ID="lbtnDeleteTribute15" runat="server" OnClick="lbtnDeleteTribute15_Click"
                                                                                    CausesValidation="False">Delete <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%></asp:LinkButton></li></ul>
                                                <div class="yt-Tribute-Settings">
                                                    <h3>
                                                        Privacy Settings</h3>
                                                    <fieldset>
                                                        <legend>Please select the level of privacy for this <%= ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>:</legend>
                                                        <div class="yt-Form-Field yt-Form-Field-Checkbox">
                                                            <asp:RadioButton ID="rdoPrivacyPublic" GroupName="rdoPrivacy" runat="server" Text="Public" />
                                                            <p>
                                                                Public <%= ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>s can be found by searching for a <%= ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>. This type of <%= ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%> will
                                                                also be added to the “Featured <%= ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>s” list when they are paid for.</p>
                                                        </div>
                                                        <div class="yt-Form-Field yt-Form-Field-Checkbox">
                                                            <asp:RadioButton ID="rdoPrivacyPrivate" GroupName="rdoPrivacy" runat="server" Text="Private" />
                                                            <p>
                                                                Private <%= ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>s will not show up in search results, and will not be included in
                                                                the “Featured <%= ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>s” list. These types of <%= ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>s will still be accessible to
                                                                all users (that is they won’t be password protected).</p>
                                                        </div>
                                                        <div class="yt-Form-Field yt-Form-Field-Checkbox" style="visibility: hidden">
                                                            <asp:CheckBox ID="chkbGoogleAdSense" runat="server" Text="Remove Google AdSense " />
                                                        </div>
                                                    </fieldset>
                                                    <div class="yt-Form-Buttons">
                                                        <div class="yt-Form-Submit">
                                                            <asp:LinkButton ID="lbtnSavePrivacyChanges" CssClass="yt-Button yt-CheckButton" runat="server"
                                                                OnClick="lbtnSavePrivacyChanges_Click">Save Changes</asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:View>
                                            <asp:View ID="ViewAdministrators" runat="server">
                                                <ul class="yt-AdminNavSecondary">
                                                    <li>
                                                        <asp:LinkButton ID="lbtnPrivacy21" runat="server" OnClick="lbtnPrivacy21_Click" CausesValidation="False">Privacy</asp:LinkButton></li><li
                                                            class="yt-Selected"><a href="javascript:void(0);">Administrators</a></li><li>
                                                                <asp:LinkButton ID="lbtnAccountType23" runat="server" OnClick="lbtnAccountType13_Click"
                                                                    CausesValidation="False">Account Type &amp; Renewal Info</asp:LinkButton></li><li>
                                                                        <asp:LinkButton ID="lbtnTributeDetails24" runat="server" OnClick="lbtnTributeDetails24_Click"
                                                                            CausesValidation="False"><%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> Details</asp:LinkButton></li><li>
                                                                                <asp:LinkButton ID="lbtnDeleteTribute25" runat="server" OnClick="lbtnDeleteTribute25_Click"
                                                                                    CausesValidation="False">Delete <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%></asp:LinkButton></li></ul>
                                                <div class="yt-Tribute-Settings">
                                                    <fieldset class="yt-Form">
                                                        <div>
                                                            <h3>
                                                                Administrators
                                                                <asp:CustomValidator ID="CustomValidator1" runat="server" Font-Bold="False" Font-Size="Medium"
                                                                    ForeColor="#FF8000" Text="!" ClientValidationFunction="ValidateTributeAdmins"
                                                                    ErrorMessage="Please select administrator(s) to delete. " Width="1px"></asp:CustomValidator>
                                                            </h3>
                                                        </div>
                                                        <fieldset>
                                                            <legend>Current Site Administrators: </legend>
                                                            <div class="yt-Form-Field yt-Form-Field-Checkbox">
                                                                <input type="checkbox" id="chkAdmin01" disabled="disabled" class="yt-Disabled" />
                                                                <label for="chkAdmin01" id="lblowner" runat="server">
                                                                </label>
                                                                <br />
                                                                <asp:CheckBoxList CssClass="yt-Form-Field yt-Form-Field-Checkbox" ID="cblstAdminis"
                                                                    runat="server" RepeatLayout="Flow">
                                                                </asp:CheckBoxList>
                                                            </div>
                                                            <div class="yt-Form-Field yt-Form-Field-Checkbox">
                                                            </div>
                                                            <div class="yt-Form-Buttons" id="divbtndeleteadminis" runat="server" visible="false">
                                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                    <ContentTemplate>
                                                                        <asp:LinkButton ID="lbtnDeleteSelectedAdmin" CssClass="yt-MiniButton yt-DeleteButton"
                                                                            runat="server"> Delete Selected</asp:LinkButton>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div id="yt-DeleteConfirmContainer_">
                                                                <div id="yt-DeleteConfirmContent_" class="yt-ModalWrapper">
                                                                    <div class="yt-Panel-Primary">
                                                                        <h2>
                                                                            Delete Administrator(s)</h2>
                                                                        <p id="divConfirmDelete" runat="server">
                                                                            Are you sure you would like to remove the selected users as administrator(s) on
                                                                            this tribute?
                                                                        </p>
                                                                        <p>
                                                                            Deleting the administrator(s) will remove them from this tribute but you may add
                                                                            them again using "Add Administrators:" on this page.</p>
                                                                        <div class="yt-Form-Buttons">
                                                                            <div class="yt-Form-Delete" id="yt-CancelContainer_">
                                                                            </div>
                                                                            <div class="yt-Form-Submit">
                                                                                <asp:LinkButton ID="lbtnDeleteAdministrator" CssClass="yt-Button yt-CheckButton"
                                                                                    runat="server" OnClick="lbtnDeleteAdministrator_Click" CausesValidation="False">Delete Administrator(s)</asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </fieldset>
                                                        <fieldset class="yt-AdminAddFields">
                                                            <legend>Add Administrators:</legend>
                                                            <p>
                                                                Please enter the email address of the person you would like to include as an administrator
                                                                for this <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>. Administrators have access to add, delete, and edit all parts
                                                                of a <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>.</p>
                                                            <div class="yt-Form-Field yt-AdminAdd" id="yt-Admin1">
                                                                <label>
                                                                    Email address:</label>
                                                                <asp:TextBox ID="txtAdminEmail1" CssClass="yt-Form-Input-Long" runat="server" ValidationGroup="Email"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="rEvEmail" runat="server" ControlToValidate="txtAdminEmail1"
                                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Font-Bold="True"
                                                                    Font-Size="Medium" ForeColor="#FF8000" ErrorMessage="Please enter a valid email address."
                                                                    ValidationGroup="Email">!</asp:RegularExpressionValidator>
                                                                <asp:RequiredFieldValidator ValidationGroup="Email" ID="rfvElmail" Font-Bold="True"
                                                                    Font-Size="Medium" ForeColor="#FF8000" ErrorMessage="Please enter a valid email address."
                                                                    runat="server" Text="!" ControlToValidate="txtAdminEmail1"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="yt-Form-Buttons">
                                                                <asp:LinkButton ID="lbtnAddToAdministrators" CssClass="yt-MiniButton yt-AddButton"
                                                                    runat="server" OnClick="lbtnAddToAdministrators_Click" ValidationGroup="Email">Add To Administrators</asp:LinkButton>
                                                            </div>
                                                        </fieldset>
                                                    </fieldset>
                                                </div>
                                            </asp:View>
                                            <asp:View ID="ViewAccountType" runat="server">
                                                <ul class="yt-AdminNavSecondary">
                                                    <li>
                                                        <asp:LinkButton ID="lbtnPrivacy31" runat="server" OnClick="lbtnPrivacy31_Click" CausesValidation="False">Privacy</asp:LinkButton></li><li>
                                                            <asp:LinkButton ID="Administrators31" runat="server" OnClick="Administrators31_Click"
                                                                CausesValidation="False">Administrators</asp:LinkButton></li><li class="yt-Selected">
                                                                    <a href="javascript:void(0);">Account Type &amp; Renewal Info</a></li><li>
                                                                        <asp:LinkButton ID="lbtnTributeDetails34" runat="server" OnClick="lbtnTributeDetails34_Click"
                                                                            CausesValidation="False"><%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> Details</asp:LinkButton></li><li>
                                                                                <asp:LinkButton ID="lbtnDeleteTribute35" runat="server" OnClick="lbtnDeleteTribute35_Click"
                                                                                    CausesValidation="False">Delete <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%></asp:LinkButton></li></ul>
                                                <div class="yt-Tribute-Settings">
                                                    <h3>
                                                        Account Type &amp; Renewal Info</h3>
                                                    <!-- Case 1: For account that has NOT been upgraded -->
                                                    <h4 id="AccounttypeInfo" runat="server">
                                                    </h4>
                                                    <p id="AccountTypeDesc" runat="server" class="yt-InfoBox">
                                                    </p>
                                                    <!-- end case 1 -->
                                                    <!-- Case 2: For account that has been upgraded -->
                                                    <div id="case2" runat="server" visible="false">
                                                        <h4 id="case2headline" runat="server">
                                                        </h4>
                                                        <p class="yt-InfoBox" id="case2desc" runat="server">
                                                        </p>
                                                        <!-- end case 2 -->
                                                    </div>
                                                    <!-- Case 3: For account that has been upgraded AND set to renew automatically -->
                                                    <div id="case3" runat="server" visible="false">
                                                        <h4 id="case3headline" runat="server">
                                                        </h4>
                                                        <p id="case3desc" runat="server" class="yt-InfoBox">
                                                        </p>
                                                        <ul class="yt-InfoBox">
                                                            <li id="liautorenewal" runat="server">To turn off automatic renewal for this <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>,
                                                                please <a href="javascript:void(0);" onclick="doModalAutoRenew();">click here</a></li><li>
                                                                    To update your credit card number and billing information, please <a href="AdminProfileBilling.aspx">
                                                                        click here</a></li></ul>
                                                        <!-- end case 3 -->
                                                        <div id="yt-AutoRenewContainer">
                                                            <div id="yt-AutoRenewContent" class="yt-ModalWrapper">
                                                                <div class="yt-Panel-Primary">
                                                                    <h2>
                                                                        Auto Renew Settings</h2>
                                                                    <h4>
                                                                        Are you sure you would like to stop the
                                                                        <%= _tributeName %>
                                                                        tribute from automatically renewing yearly?</h4>
                                                                    <p>
                                                                        Note that you can enable this feature at any time or upgrade to the lifetime package.</p>
                                                                    <div class="yt-Form-Buttons">
                                                                        <div class="yt-Form-Delete" id="yt-CancelContainer_AR">
                                                                        </div>
                                                                        <div class="yt-Form-Submit">
                                                                            <asp:LinkButton ID="lbtnConfirm" CssClass="yt-Button yt-CheckButton" runat="server"
                                                                                OnClick="lbtnConfirm_Click" CausesValidation="False">Confirm</asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div id="tblccounttype" runat="server" visible="false">
                                                        <div class="yt-Form-Buttons">
                                                            <div class="yt-Form-Submit">
                                                                <asp:LinkButton ID="lbtnUpgradeAccount" CssClass="yt-Button yt-CheckButton" runat="server"
                                                                    OnClick="lbtnUpgradeAccount_Click">Upgrade Account</asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:View>
                                            <asp:View ID="ViewTributeDetails" runat="server">
                                                <ul class="yt-AdminNavSecondary">
                                                    <li>
                                                        <asp:LinkButton ID="lbtnPrivacy41" runat="server" OnClick="lbtnPrivacy41_Click" CausesValidation="False">Privacy</asp:LinkButton></li><li>
                                                            <asp:LinkButton ID="lbtnAdministrators42" runat="server" OnClick="lbtnAdministrators42_Click"
                                                                CausesValidation="False">Administrators</asp:LinkButton></li><li>
                                                                    <asp:LinkButton ID="lbtnAccountType43" runat="server" OnClick="lbtnAccountType43_Click"
                                                                        CausesValidation="False">Account Type &amp; Renewal Info</asp:LinkButton></li><li
                                                                            class="yt-Selected"><a href="javascript:void(0);"><%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> Details</a></li><li>
                                                                                <asp:LinkButton ID="lbtnDeleteTribute45" runat="server" OnClick="lbtnDeleteTribute45_Click"
                                                                                    CausesValidation="False">Delete <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%></asp:LinkButton></li></ul>
                                                <div class="yt-Tribute-Settings">
                                                    <fieldset class="yt-Form">
                                                        <h3>
                                                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> Details</h3>
                                                        <div class="yt-Form-Field">
                                                            <label for="txtTributeName">
                                                                Who is this <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%> for?</label>
                                                            <asp:TextBox ID="txtTributeName" CssClass="yt-Form-Input-XLong" runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvTributeName" Text="!" runat="server" ControlToValidate="txtTributeName"
                                                                Font-Bold="True"
                                                                Font-Size="Medium" ForeColor="#FF8000" ValidationGroup="TributeDetails"> </asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="revTributeName" runat="server" ErrorMessage="Please provide a valid Tribute Name."
                                                                ControlToValidate="txtTributeName" ValidationGroup="TributeDetails" Font-Bold="True"
                                                                Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9_,\-\'\&quot;)(.\s,]*$"></asp:RegularExpressionValidator>
                                                            <%--<asp:CustomValidator ID="cvTributeName" runat="server" ErrorMessage="Invalid Tribute Name,* and ? is not allowed,Please try again. "
                                                                ClientValidationFunction="ValidateTributeName" ControlToValidate="txtTributeName"
                                                                Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ValidationGroup="TributeDetails"
                                                                Text="!"></asp:CustomValidator>--%>
                                                            <p>
                                                                By changing this information, you will be changing the information on the <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                                                                homepage and within the story area. You will not be changing the web address of
                                                                the <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>.</p>
                                                        </div>
                                                        <div id="divDonation" runat="server" class="yt-Form-Field">
                                                            <h3>
                                                                Donations:</h3>
                                                            <label for="txtCharityName">
                                                                Charity Name:</label>
                                                            <asp:TextBox ID="txtCharityName" CssClass="yt-Form-Input-XLong" runat="server" MaxLength="1000"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="revCharityName" runat="server" ErrorMessage="Please provide a valid Charity Name."
                                                                ControlToValidate="txtCharityName" ValidationGroup="TributeDetails" Font-Bold="True"
                                                                Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9,\#,\-.\s]*$"></asp:RegularExpressionValidator>
                                                            <p>
                                                                Changing the charity name will <u>only</u> change the name that appears on your
                                                                tribute homepage in the donation box. To change the charity you are accepting donations
                                                                to, you will need to login to www.epartnersingiving.com and update the charity on
                                                                your donation page.
                                                            </p>
                                                            <label for="txtDonationURL">
                                                                Donation Page URL:</label>
                                                            <asp:TextBox ID="txtDonationURL" CssClass="yt-Form-Input-XLong" runat="server" MaxLength="255"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvDonationURL" Text="!" runat="server" ControlToValidate="txtDonationURL"
                                                                ErrorMessage="Donation Page URL is a required field." Font-Bold="True" Font-Size="Medium"
                                                                ForeColor="#FF8000" ValidationGroup="TributeDetails"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="revDonationURL" runat="server" ErrorMessage="Please provide a valid Donation Page URL."
                                                                ControlToValidate="txtDonationURL" ValidationGroup="TributeDetails" Font-Bold="True"
                                                                Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?"></asp:RegularExpressionValidator>
                                                            <p>
                                                                Please only update this URL if you have created a new donation page at www.epartnersingiving.com.
                                                            </p>
                                                            <label for="txtDonationURL">
                                                                Remove Donation Box:</label>
                                                            <div class="yt-Form-Field yt-Form-Field-Checkbox">
                                                                <asp:CheckBox ID="chkDonation" Text="Check this box to permanently remove the Donation Box from your tribute homepage"
                                                                    runat="server" />
                                                            </div>
                                                            <p>
                                                                * Checking the box above and then clicking "Save Changes" will <u>permanently</u>
                                                                remove the donation box.
                                                                <br />
                                                                To delete your Donation Page, login to www.epartnersingiving.com and delete your
                                                                account there.
                                                            </p>
                                                            <div id="yt-DonationDeleteConfirmContainer">
                                                                <div id="yt-DonationDeleteConfirmContent" class="yt-ModalWrapper">
                                                                    <div class="yt-Panel-Primary">
                                                                        <h2>
                                                                            Delete Donation Box</h2>
                                                                        <p id="divDonationConfirmDelete" runat="server">
                                                                            Are you sure you would like to remove the Donation Box on this tribute?
                                                                        </p>
                                                                        <p>
                                                                            Deleting the Donation Box will remove it from this tribute permanently and you will
                                                                            not be able to add it again. However, to completely delete your donation page, login
                                                                            to <a href='http://www.epartnersingiving.com' target="_blank">www.epartnersingiving.com</a>
                                                                            and delete the donation page.</p>
                                                                        <div class="yt-Form-Buttons">
                                                                            <div class="yt-Form-Delete" id="yt-CancelContainerDonation">
                                                                            </div>
                                                                            <div class="yt-Form-Submit">
                                                                                <asp:LinkButton ID="lbtnDonationDeleteAdministrator" CssClass="yt-Button yt-CheckButton"
                                                                                    runat="server" OnClick="lbtnsaveTributeName_Click" CausesValidation="False">Delete Donation Box</asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="yt-Form-Buttons">
                                                            <div class="yt-Form-Submit">
                                                                <asp:LinkButton ID="lbtnsaveTributeName" CssClass="yt-Button yt-CheckButton" runat="server"
                                                                    OnClientClick="return ConfirmDelete();" OnClick="lbtnsaveTributeName_Click" ValidationGroup="TributeDetails">Save Changes</asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                            </asp:View>
                                            <asp:View ID="ViewDeleteTribute" runat="server">
                                                <ul class="yt-AdminNavSecondary">
                                                    <li>
                                                        <asp:LinkButton ID="lbtnPrivacy51" runat="server" OnClick="lbtnPrivacy51_Click" CausesValidation="False">Privacy</asp:LinkButton></li><li>
                                                            <asp:LinkButton ID="lbtnAdministrators52" runat="server" OnClick="lbtnAdministrators52_Click"
                                                                CausesValidation="False">Administrators</asp:LinkButton></li><li>
                                                                    <asp:LinkButton ID="lbtnAccountType53" runat="server" OnClick="lbtnAccountType53_Click"
                                                                        CausesValidation="False">Account Type &amp; Renewal Info</asp:LinkButton></li><li>
                                                                            <asp:LinkButton ID="lbtnTributeDetails54" runat="server" OnClick="lbtnTributeDetails54_Click"
                                                                                CausesValidation="False"><%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> Details</asp:LinkButton></li><li class="yt-Selected">
                                                                                    <a href="javascript:void(0);">Delete <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%></a></li></ul>
                                                <div class="yt-Tribute-Settings">
                                                    <fieldset class="yt-Form">
                                                        <h3>
                                                            Delete <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%></h3>
                                                        <h4 class="yt-WarningText" style="color: #ff9966">
                                                            Warning!
                                                        </h4>
                                                        <p>
                                                            Deleting your <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%> is NOT reversible. If you choose to delete your <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%> the
                                                            <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString().ToLower()%> site<br />
                                                            along with ALL guestbook entries, photos, videos, comments and ALL other information
                                                            will be removed<br />
                                                            permanently.</p>
                                                        <p>
                                                            Please be certain you would like to remove your <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%> before continuing...</p>
                                                        <div class="yt-Form-Buttons">
                                                            <div class="yt-Form-Submit">
                                                                <a href="javascript:void(0);" onclick="doModalDeleteConfirm();" class="yt-Button yt-CheckButton">
                                                                    Delete <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%></a>
                                                            </div>
                                                        </div>
                                                        <div id="yt-DeleteConfirmContainer">
                                                            <div id="yt-DeleteConfirmContent" class="yt-ModalWrapper">
                                                                <div class="yt-Panel-Primary">
                                                                    <h2>
                                                                        Delete <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%></h2>
                                                                    <h4>
                                                                        Are you sure you would like to delete the
                                                                        <%= _tributeName %>
                                                                        <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>?</h4>
                                                                    <p>
                                                                        Deleting the <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%> will permanently remove the <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%> including all guestbook
                                                                        messages, photos, videos, posts and comments.</p>
                                                                    <div class="yt-Form-Buttons">
                                                                        <div class="yt-Form-Delete" id="yt-CancelContainer">
                                                                        </div>
                                                                        <div class="yt-Form-Submit">
                                                                            <asp:LinkButton ID="lbtnDeleteTribute" CssClass="yt-Button yt-CheckButton" runat="server"
                                                                                OnClick="lbtnDeleteTribute_Click">Yes, Delete <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%></asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </fieldset>
                                                </div>
                                            </asp:View>
                                        </asp:MultiView>
                                    </div>
                                </div>
                                <!--yt-Panel-Primary-->
                            </div>
                            <!--yt-AdminMain-->
                            <%--  </ContentTemplate>
                            </asp:UpdatePanel>--%>
                        </div>
                        <!--yt-ContentPrimary-->
                    </div>
                    <!--yt-ContentPrimaryContainer-->
                    <div class="yt-ContentSecondary">
                        <div class="yt-Panel-System">
                            <h2 id="trbBlog" runat="server">
                                News from Your Tribute</h2>
                            <uc2:TributeBlog ID="TributeBlog1" runat="server" />
                        </div>
                        <!-- By Udham to remove types of tributes -->
                        <div class="yt-Panel yt-Panel-Tributes">
                                <uc4:LeftFeaturedPanel ID="LeftFeaturedPanel1" runat="server" />                             
                        </div>
                    </div>
                    <!--yt-ContentSecondary-->
                    <div class="hack-clearBoth">
                    </div>
                    <div class="yt-ContentContainerImage bgImageBUser">
                    </div>
                </div>
                <!--yt-ContentContainerInner-->
            </div>
            <!--yt-ContentContainer-->
            <div >
                <uc3:Footer ID="Footer1" runat="server" />
            </div>
            <!--yt-Footer-->
        </div>
        <!--yt-Container-->
        <div class="upgrade">
            <h2>
                Please Upgrade Your Browser</h2>
            <p>
                This site's design is only visible in a graphical browser that supports <a href="http://www.webstandards.org/upgrade/"
                    title="The Web Standards Project's BROWSER UPGRADE initiative.">web standards</a>,
                but its content is accessible to any browser or Internet device.</p>
        </div>
        <!--yt-upgrade-->
    </form>

    <script type="text/javascript">
executeBeforeLoad();
    </script>

</body>
</html>
