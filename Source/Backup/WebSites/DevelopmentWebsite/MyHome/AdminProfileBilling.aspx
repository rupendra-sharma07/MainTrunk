<%@ Page Language="C#" AutoEventWireup="true" Title="AdminProfileBilling" CodeFile="AdminProfileBilling.aspx.cs"
    Inherits="MyHome_AdminProfileBilling" MasterPageFile="~/Shared/InnerSecure.master" %>

<%@ MasterType VirtualPath="~/Shared/InnerSecure.Master" %>
<asp:Content ID="header_script" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">

    <script type="text/javascript">
    function update_user_is_connected() {
        header_user_is_connected();
        FB.XFBML.parse();
    }
    function update_user_is_not_connected() {
        header_user_is_not_connected();
        FB.XFBML.parse();
    }             
    /* NOTE: may want to move this to an external .js */
    
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
	
	});
	   
 function Check(rdb)
 {
    var rdb1=document.getElementById('<%=HiddenField1.ClientID%>');
   rdb1.value= rdb.value;
 };

 function Check_(rdb)
 {
   var rdb1=document.getElementById('<%=HiddenField1.ClientID%>');
   rdb1.value= rdb.value;
 }

 function validatePaymentMethod(source, args)
 {
      var bool=false;
           var rdb1=document.getElementById('<%=rdoCCVisa.ClientID%>');
           var rdb2=document.getElementById('<%=rdoCCMasterCard.ClientID%>');        
           if((!rdb1.checked)&&(!rdb2.checked))           
           {           
             bool=false;                      
           }
           else
           bool=true;
           
       args.IsValid=bool;
          
 }
 
  function validatePaymentMethod1(source, args)
 {
      var bool=false;
           var rdb1=$('1rdoCCVisa');
           var rdb2=$('1rdoCCAmex');
           var rdb3=$('1rdoCCDiscover');
           var rdb4=$('1rdoCCMasterCard');
           if((!rdb1.checked)&&(!rdb2.checked)&&(!rdb3.checked)&&(!rdb4.checked))           
           bool=false;                      
           else
           bool=true;
           
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

function validateExpMonth1(source, args)
{
  var bol=true;
  var month =  document.getElementById('<%=ddlCCMonth1.ClientID%>'); 
  var year =  document.getElementById('<%=txtCCYear1.ClientID%>'); 
  var validat =  document.getElementById('<%=CustomValidator5.ClientID%>');   
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

function PhoneNumbervalidation1(source, args)
{     
     var number1=  document.getElementById('<%= txtPhoneNumber_1.ClientID %>');
     var number2=  document.getElementById('<%= txtPhoneNumber_2.ClientID %>');
     var number3=  document.getElementById('<%= txtPhoneNumber_3.ClientID %>');
     var validator=  document.getElementById('<%= cvPhoneNumber1.ClientID %>');     
     
     args.IsValid= PhoneNumberValidate(number1,number2,number3,validator);     
}  
  
function CCardLength(source, args)
{
    var CCNumber =  document.getElementById('<%=txtCCNumber.ClientID%>'); 
    if(CCNumber)
        {
            var value=CreditCardCLength(CCNumber);
                if(value==1)
                    args.IsValid=true;
                else
                    args.IsValid=false;     
        }            
}
function CCardLength1(source, args)
{
    var CCNumber =  document.getElementById('<%=txtCCNumber1.ClientID%>'); 
    if(CCNumber)
        {
            var value=CreditCardCLength(CCNumber);
                if(value==1)
                    args.IsValid=true;
                else
                    args.IsValid=false;     
        }            
}

function validateCreditCard1Length(source, args)
 {
        var bool=false;
        var rdb1=$('1rdoCCVisa');
        var rdb2=$('1rdoCCAmex');
        var rdb3=$('1rdoCCDiscover');
        var rdb4=$('1rdoCCMasterCard');
        var val=document.getElementById('<%=txtCCNumber1.ClientID%>').value;
      
      if(val.length!=0)
      {
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
             if(val.length!=16 )
            {
                bool=false;
            }
            else
                bool=true;
        }
      }
      else 
      {
       bool=true;  //we dont want the error message to display in case length = 0
       }
        args.IsValid=bool;
 }
 
 
 function validateCreditCardVerification1Length(source, args)
 {
        var bool=false;
        var rdb1=$('1rdoCCVisa');
        var rdb2=$('1rdoCCAmex');
        var rdb3=$('1rdoCCDiscover');
        var rdb4=$('1rdoCCMasterCard');
        var val=document.getElementById('<%=txtCCVerification1.ClientID%>').value;
    
    if(val.length!=0)
      {
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
             if(val.length!=3)
            {
                bool=false;
            }
            else
                bool=true;
        }
       }
          else 
            bool=true;  //we dont want the error message to display in case length = 0
           
        args.IsValid=bool; 
}

function validateCreditCardLength(source, args)
 {
        var bool=false;
       
        var rdb1=document.getElementById('<%=rdoCCVisa.ClientID%>');        
        var rdb4=document.getElementById('<%=rdoCCMasterCard.ClientID%>');
        var val=document.getElementById('<%=txtCCNumber.ClientID%>').value;
        
        if(val.length!=0)
        {
        if(rdb1.checked)
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
             if(val.length!=16 )
            {
                bool=false;
            }
            else
                bool=true;
        }
        }
           else 
           {
            bool=true; //we dont want the error message to display in case length = 0
            }
        args.IsValid=bool;
 }
 
 
 function validateCreditCardVerificationLength(source, args)
 {
        var bool=false;
        var rdb1=document.getElementById('<%=rdoCCVisa.ClientID%>');        
        var rdb4=document.getElementById('<%=rdoCCMasterCard.ClientID%>');
        var val=document.getElementById('<%=txtCCVerification.ClientID%>').value;
    
    if(val.length!=0)
      {
        if(rdb1.checked)
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
             if(val.length!=3)
            {
                bool=false;
            }
            else
                bool=true;
        }
      } 
      else 
            bool=true;  //we dont want the error message to display in case length = 0
            
        args.IsValid=bool;
}

function CVCLength(source, args)
{
 var CCNumber =  document.getElementById('<%=txtCCVerification.ClientID%>');    
 args.IsValid= CVCLengthCC(CCNumber)
}
function CVCLength1(source, args)
{
 var CCNumber =  document.getElementById('<%=txtCCVerification1.ClientID%>');    
 args.IsValid= CVCLengthCC(CCNumber)
}
  
     
     
     function Test(TributeName,trubuteType,PackageType,BillingDate,ExpiryDate,Name,Address,City,StateProvince,Country,ZipPostal,Telephone,PaymentType,CreditCard,AmountBilled)
        {  
     
    $('ddtrubutename').innerHTML=TributeName;   
    $('ddtrubuteType').innerHTML=trubuteType;
    $('ddExpiryDate').innerHTML=ExpiryDate;  
    $('ddBillingDate').innerHTML=BillingDate;   
    $('ddPackageType').innerHTML=PackageType;   
    
    
    
    $('ddName').innerHTML=Name;  
    $('ddAddress').innerHTML=Address;  
    $('ddCity').innerHTML=City;  
    $('ddStateProvince').innerHTML=StateProvince;  
    $('ddCountry').innerHTML=Country;  
    $('ddZipPostal').innerHTML=ZipPostal;  
    $('ddTelephone').innerHTML=Telephone;  
    
    $('ddPaymentType').innerHTML=PaymentType;  
    $('ddCreditCard').innerHTML=CreditCard;      
    $('ddAmountBilled').innerHTML=AmountBilled;  
    
       doModalAdminReceipt();
     };
     
     function SetValidation()
 {  
    var required_validator = document.getElementById('<%= VSAccountTrpe.ClientID %>');
    var lblErrMsg= document.getElementById('<%= lblErrMsg.ClientID %>'); 
 if(lblErrMsg)
 {
   lblErrMsg.innerHTML = '';
   lblErrMsg.style.visibility = 'hidden';  
   
 }
 } 
     
    </script>

</asp:Content>
<asp:Content ID="modalcontent" ContentPlaceHolderID="ModalContentPlaceHolder" runat="Server">
    <div id="divShowModalPopup">
    </div>
    <div id="yt-UpgradeExtendContainer">
        <div id="yt-UpgradeExtendError">
        <div id="lblErrMsg" class="yt-Error" style="text-align: left" runat="server" visible="false">
            </div>
            <asp:ValidationSummary CssClass="yt-Error" ID="VSAccountTrpe" ValidationGroup="accounttype"
                runat="server" Width="630px" HeaderText="<h2>Oops - there is a problem with some account type information.</h2>                                                             <h3>Please correct the errors below:</h3>"
                ForeColor="Black" />
        </div>
        <div id="yt-UpgradeExtendContent" class="yt-ModalWrapper">
            <div class="yt-Panel-Primary">
                <h2 style="width: 219px">
                    Add Credit Card Information</h2>
                <h4 class="yt-InfoBox">
                    You have chosen to add new Credit card information for auto renewal of your
                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>s.</h4>
                <fieldset class="yt-Form">
                    <p>
                        Please enter the following payment information:</p>
                    <p class="yt-requiredFields">
                        <strong>Required fields are indicated with <em class="required">* </em></strong>
                    </p>
                    <fieldset class="yt-PaymentSelection yt-CompactRadioList">
                        <legend>* Select your payment method:</legend>
                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                        <asp:CustomValidator ID="CustomValidator7" runat="server" Font-Bold="True" Font-Size="Medium"
                            ForeColor="#FF8000" ClientValidationFunction="validatePaymentMethod1" Text="!"
                            ValidationGroup="accounttype" ErrorMessage="Select your payment method." Width="1px"></asp:CustomValidator>
                    </fieldset>
                    <div class="yt-Form-Field">
                        <label>
                            * Credit Card Number:</label>
                        <asp:TextBox ID="txtCCNumber1" CssClass="yt-Form-Input-Long" runat="server" MaxLength="16"
                            Width="280px" ValidationGroup="accounttype">
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="accounttype" ID="RequiredFieldValidator1"
                            Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ControlToValidate="txtCCNumber1"
                            runat="server" Text="!" ErrorMessage="Credit Card Number is a required field."
                            Width="1px"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="cvCreditCardNumber" runat="server" Font-Bold="True" Font-Size="Medium"
                            ForeColor="#FF8000" ClientValidationFunction="validateCreditCard1Length" Text="!"
                            ValidationGroup="accounttype" EnableClientScript="true" ErrorMessage="Please enter a valid Credit Card Number."
                            Width="1px"></asp:CustomValidator>
                    </div>
                    <div class="yt-Form-Field">
                        <label>
                            * Card Verification Code (CVC):</label>
                        <asp:TextBox ID="txtCCVerification1" ValidationGroup="accounttype" CssClass="yt-Form-Input-Short"
                            runat="server" MaxLength="3" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ValidationGroup="accounttype" ControlToValidate="txtCCVerification1"
                            Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ID="RequiredFieldValidator2"
                            Text="!" runat="server" ErrorMessage="Card Verification Code (CVC) is a required field."
                            Width="1px"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="cvValidateVerification" runat="server" Font-Bold="True"
                            ValidationGroup="accounttype" Font-Size="Medium" ForeColor="#FF8000" ClientValidationFunction="validateCreditCardVerification1Length"
                            Text="!" EnableClientScript="true" ErrorMessage="Please enter a valid Credit Card Verification Number."
                            Width="1px"></asp:CustomValidator>
                        <%--   <asp:CustomValidator ID="CustomValidator2" ValidationGroup="accounttype" runat="server"
                                Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" Text="!" ClientValidationFunction="CVCLength1"
                                ErrorMessage="Please enter a valid card verification code (CVC). " Width="1px"></asp:CustomValidator>--%>
                        <div class="hint">
                            The CVC is located on the back of MasterCard, Visa and Discover credit cards and
                            is a separate group of 3 digits to the right of the signature strip. On American
                            Express cards, the CVC is a separate group of 4 digits on the front right of the
                            card.<span class="hintPointer"></span></div>
                    </div>
                    <fieldset class="yt-Date-Fields">
                        <legend><em class="required">* </em>Expiry Date:</legend>
                        <div class="yt-Form-Field">
                            <asp:DropDownList ID="ddlCCMonth1" runat="server" Width="132px">
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
                            <asp:TextBox ID="txtCCYear1" ValidationGroup="accounttype" CssClass="yt-Form-Input-Short"
                                MaxLength="4" runat="server"></asp:TextBox>
                            <asp:CustomValidator ID="CustomValidator5" ValidationGroup="accounttype" runat="server"
                                Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ClientValidationFunction="validateExpMonth1"
                                Text="!" ErrorMessage="Expiry Date is a required field." Width="1px"></asp:CustomValidator>
                            <asp:CompareValidator ID="CompareValidator1" Font-Bold="True" ValidationGroup="accounttype"
                                Operator="GreaterThanEqual" Font-Size="Medium" ForeColor="#FF8000" runat="server"
                                ErrorMessage="Expiry Date cannot be less than current date." ControlToValidate="txtCCYear1"
                                Width="1px">!</asp:CompareValidator>
                            <asp:CustomValidator ID="CustomValidator6" ValidationGroup="accounttype" Font-Bold="True"
                                Font-Size="Medium" ForeColor="#FF8000" runat="server" ControlToValidate="txtCCYear1"
                                Text="!" ErrorMessage="Please enter a valid expiry date." Width="1px"></asp:CustomValidator>
                            <span style="color: #FF8000; font-size: Medium; font-weight: bold;" id="Span1" runat="server"
                                visible="false">!</span>
                            <label>
                                Year</label>
                        </div>
                    </fieldset>
                    <div class="yt-Form-Field">
                        <label>
                            <em class="required">* </em>Name on Card:</label>
                        <asp:TextBox ID="txtCCName1" ValidationGroup="accounttype" CssClass="yt-Form-Input-Long"
                            runat="server" MaxLength="50" Width="280px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="accounttype"
                            Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" Text="!" runat="server"
                            ErrorMessage="Name on Card is a required field." ControlToValidate="txtCCName1"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revNameCard" ControlToValidate="txtCCName1" Text="!"
                            ValidationGroup="accounttype" runat="server" ErrorMessage="Name on Card can only contain letters,numbers,'-' and '#'"
                            ValidationExpression="^[a-zA-Z0-9,\#,\-,\s]*$" Font-Bold="True" Font-Size="Medium"
                            ForeColor="#FF8000"></asp:RegularExpressionValidator>
                    </div>
                    <div class="yt-Form-Field">
                        <label>
                            <em class="required">* </em>Billing Address:</label>
                        <asp:TextBox ID="txtCCBillingAddress_" ValidationGroup="accounttype" CssClass="yt-Form-Input-Long"
                            runat="server" MaxLength="50" Width="280px"></asp:TextBox>
                        <asp:RegularExpressionValidator ValidationGroup="accounttype" ID="RegularExpressionValidator3"
                            runat="server" ControlToValidate="txtCCBillingAddress_" ErrorMessage="Billing Address (line 1) can only contain letters,numbers,'-' and '#'"
                            Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9,\#,\-,\s]*$"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="accounttype"
                            Text="!" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ControlToValidate="txtCCBillingAddress_"
                            ErrorMessage="Billing Address is a required field."></asp:RequiredFieldValidator>
                    </div>
                    <div class="yt-Form-Field">
                        <asp:TextBox ID="txtCCBillingAddress_2" ValidationGroup="accounttype" CssClass="yt-Form-Input-Long"
                            runat="server" MaxLength="50" Width="280px"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ValidationGroup="accounttype"
                            runat="server" ControlToValidate="txtCCBillingAddress_2" ErrorMessage="Billing Address (line 2) can only contain letters,numbers,'-' ans '#'"
                            Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9,\#,\-,\s]*$"></asp:RegularExpressionValidator>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <div class="yt-Form-Field">
                                <label>
                                    <em class="required">* </em>Country:</label>
                                <asp:DropDownList ID="ddlCCCountry1" runat="server" AutoPostBack="True" Width="285px"
                                    OnSelectedIndexChanged="ddlCCCountry1_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="yt-Form-Field">
                                <label>
                                    State/Province:</label>
                                <asp:DropDownList ID="ddlCCStateProvince1" runat="server" Width="285px">
                                </asp:DropDownList>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="yt-Form-Field">
                        <label>
                            <em class="required">* </em>City:</label>
                        <asp:TextBox ID="txtCCCity1" CssClass="yt-Form-Input-Long" ValidationGroup="accounttype"
                            runat="server" Width="280px" MaxLength="50"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtCCCity1"
                            ErrorMessage="City only contain letters,numbers,'-' and '#'" Font-Bold="True"
                            Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationGroup="accounttype"
                            ValidationExpression="^[a-zA-Z0-9,\#,\-,\s]*$"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Text="!"
                            ControlToValidate="txtCCCity1" ErrorMessage="City is a required field." Font-Bold="True"
                            Font-Size="Medium" ValidationGroup="accounttype" ForeColor="#FF8000"></asp:RequiredFieldValidator>
                    </div>
                    <div class="yt-Form-Field">
                        <label>
                            <em class="required">* </em>Zip Code/Postal Code:</label>
                        <asp:TextBox ID="txtCCZipCode1" ValidationGroup="accounttype" CssClass="yt-Form-Input"
                            runat="server" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ValidationGroup="accounttype"
                            runat="server" Text="!" ControlToValidate="txtCCZipCode1" ErrorMessage="Zip Code/Postal Code is a required field."
                            Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revZipCode" runat="server" ControlToValidate="txtCCZipCode1"
                            ValidationGroup="accounttype" ErrorMessage="Zip Code/Postal Code can only contain letters and numbers"
                            Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9]*$"></asp:RegularExpressionValidator>
                    </div>
                    <div class="yt-Form-Field">
                        <label>
                            <em class="required">* </em>Phone Number:</label>
                        (<asp:TextBox ID="txtPhoneNumber_1" runat="server" Width="34px" MaxLength="3" TabIndex="15"
                            CssClass="yt-Form-Input-Long"></asp:TextBox>)
                        <asp:TextBox ID="txtPhoneNumber_2" runat="server" Width="34px" MaxLength="3" CssClass="yt-Form-Input-Long"
                            TabIndex="16"></asp:TextBox>
                        -
                        <asp:TextBox ID="txtPhoneNumber_3" runat="server" Width="40px" MaxLength="4" CssClass="yt-Form-Input-Long"
                            TabIndex="17"></asp:TextBox>
                        <asp:CustomValidator ID="cvPhoneNumber1" ValidationGroup="accounttype" Visible="true"
                            runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ErrorMessage="Enter valid phone number.Format should be XXX-XXX-XXXX."
                            ClientValidationFunction="PhoneNumbervalidation1">!</asp:CustomValidator>
                        <%-- <asp:CustomValidator ID="cvAcceptPolicies" Text="!" ForeColor="Transparent" ErrorMessage="Please accept that you have read and agree to the terms of use, cancellation/refund policy and the privacy policy."
                                runat="server" ValidationGroup="accounttype" ClientValidationFunction="ValidateTandCs"
                                Width="1px"></asp:CustomValidator>--%>
                    </div>
                    <%-- <div class="yt-Form-Field yt-Form-Field-Radio" id="divYearlyAutoRenew" runat="server">
                            <asp:RadioButton ID="rdoYearlyAutoRenew" Text="I want this tribute to be renewed automatically on a yearly basis."
                                GroupName="rdoRenew" runat="server" />
                        </div>
                        <div class="yt-Form-Field yt-Form-Field-Radio" id="divNotifyBeforeRenew" runat="server">
                            <asp:RadioButton ID="rdoNotifyBeforeRenew" Text="I do not want this tribute to be renewed automatically on a yearly basis, but I
                                                                    will be notified when the account is near to expiry."
                                GroupName="rdoRenew" Checked="True" runat="server" />
                        </div>
                        <div class="yt-Form-Field yt-Form-Field-Checkbox">
                            <asp:CheckBox ID="chkSaveBillingInfo" Text="I would like to save the above billing information in my profile."
                                runat="server" />
                        </div>
                        <div class="yt-Form-Field yt-Form-Field-Checkbox">
                            <asp:CheckBox ID="chkAgree" runat="server" Text="I have read and agree to the terms of use, the cacellation/refund
                                                                    policy (outlined in the terms of use) and the privacy policy."
                                Checked="True" />
                        </div>
                        <div class="yt-InfoBox" id="yt-PaymentTotal">
                            Your credit card will be charged: <span id="yt-BillingTotal">$20</span>
                        </div>
                        <p>
                            If you have reviewed all of the above information and it is correct, you must be
                            ready to...</p>--%>
                    <div class="yt-Form-Buttons">
                        <div class="yt-Form-Submit">
                            <asp:LinkButton ID="lbtnPay" CssClass="yt-Button yt-ArrowButton" ValidationGroup="accounttype"
                                runat="server" OnClick="lbtnPay_Click">Save!</asp:LinkButton>
                        </div>
                    </div>
                </fieldset>
                <!--yt-Form-->
            </div>
        </div>
    </div>
    <div class="yt-ModalWrapper">
        <div id="yt-AdminReceiptContent">
            <div class="yt-Panel-Primary">
                <address class="vcard">
                    <span class="fn org">
                        <img src="../assets/images/logo_AdminReceipt.gif" alt="Your Tribute" /><span>Your Tribute,
                            Inc.</span></span> <span class="adr">Your Tribute, Inc.<br />
                                <span class="street-address">2875 North Lamb Blvd,</span> <span class="locality">Bldg
                                    8</span><br />
                                <span class="locality">Las Vegas, </span>
                                <%--<abbr class="region" title="British Columbia">BC</abbr>--%>
                                <span class="postal-code">NV</span> <span class="country-name">89115</span><br />
                            </span><a class="email" href="mailto:<%=TributesPortal.Utilities.WebConfig.BillingEmail%>">
                                <%=TributesPortal.Utilities.WebConfig.BillingEmail%>
                            </a>
                    <br />
                    <span class="tel"></span>
                </address>
                <h3>
                    Payment Receipt</h3>
                <h4>
                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                    Details:</h4>
                <dl>
                    <dt>
                        <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                        Name:</dt>
                    <dd id="ddtrubutename" runat="server">
                    </dd>
                    <dt>
                        <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                        Type: </dt>
                    <dd id="ddtrubuteType" runat="server">
                    </dd>
                    <dt>Package Type:</dt>
                    <dd id="ddPackageType" runat="server">
                    </dd>
                    <dt>Expiry Date: </dt>
                    <dd id="ddExpiryDate" runat="server">
                    </dd>
                </dl>
                <h4>
                    Billing Address:</h4>
                <dl>
                    <dt>Name: </dt>
                    <dd id="ddName" runat="server">
                    </dd>
                    <dt>Address:</dt>
                    <dd id="ddAddress" runat="server">
                    </dd>
                    <dt>City:</dt>
                    <dd id="ddCity" runat="server">
                    </dd>
                    <dt>State/Province:</dt>
                    <dd id="ddStateProvince" runat="server">
                    </dd>
                    <dt>Country:</dt>
                    <dd id="ddCountry" runat="server">
                    </dd>
                    <dt>Zip/Postal Code:</dt>
                    <dd id="ddZipPostal" runat="server">
                    </dd>
                    <dt>Telephone: </dt>
                    <dd id="ddTelephone" runat="server">
                        555.555.5555</dd>
                </dl>
                <h4>
                    Billing Details:</h4>
                <dl>
                    <dt>Billing Date:</dt>
                    <dd id="ddBillingDate" runat="server">
                    </dd>
                    <dt>Payment Type: </dt>
                    <dd id="ddPaymentType" runat="server">
                    </dd>
                    <dt>Credit Card:</dt>
                    <dd id="ddCreditCard" runat="server">
                        xxxx xxxx xxx 555</dd>
                    <dt>Amount Billed:</dt>
                    <dd id="ddAmountBilled" runat="server">
                    </dd>
                </dl>
                <div class="yt-Form-MiniButtons">
                    <div class="yt-Form-Submit">
                        <a href="javascript:void(0);" class="yt-MiniButton yt-PrintButton" onclick="printModal();">
                            Print</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="content" ContentPlaceHolderID="BodyContentPlaceHolder" runat="Server">
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="hfPaymentMethod" runat="server" />
    <input type="hidden" name="hidden1" id="hidden1" value="" runat="server" />
    <div id="yt-BillingFormContainer">
        <h3 class="personal yt-AccountTypeDescription">
            Billing History:</h3>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="gvBillingHistory" CssClass="yt-AdminTable" AutoGenerateColumns="False"
                    runat="server" OnSelectedIndexChanged="gvBillingHistory_SelectedIndexChanged"
                    OnRowCommand="gvBillingHistory_OnRowCommand">
                    <Columns>
                        <asp:TemplateField HeaderStyle-BackColor="#73BFE8" HeaderStyle-ForeColor="#ffffff">
                            <HeaderTemplate>
                                <asp:Label ID="lblDate" CssClass="yt-Col-Date" runat="server" Text="Billing Date:"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblBillingDate" runat="server" Text='<%# Bind("StartDate","{0:MMMM dd, yyyy}")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-BackColor="#73BFE8" HeaderStyle-ForeColor="#ffffff">
                            <HeaderTemplate>
                                <asp:Label ID="lblTributeName" CssClass="yt-Col-Name" runat="server" Text="Tribute Name:"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnTributeName" CausesValidation="false" CommandName="SelectTribute"
                                    Text='<%# Eval("TributeName") %>' runat="server" CommandArgument='<%# Eval("Tributeid") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-BackColor="#73BFE8" HeaderStyle-ForeColor="#ffffff">
                            <HeaderTemplate>
                                <asp:Label ID="lblPackageName" CssClass="yt-Col-AcctType" runat="server" Text="Account Type:"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblPackage" runat="server" Text='<%# Eval("PackageName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderStyle-BackColor="#73BFE8" HeaderStyle-ForeColor="#ffffff">
                            <HeaderTemplate>
                                <asp:Label ID="lblAmountPaid" CssClass="yt-Col-Amount" runat="server" Text="Amount:"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" CommandName="Select" Text='<%# Eval("AmountToDisplay") %>'
                                    CausesValidation="false" runat="server"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <HeaderTemplate>
                                <asp:Label ID="lblTributeid" Visible="false" runat="server" Text="Tributeid:"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbtnTributeid" Text='<%# Eval("Tributeid") %>' Visible="false" runat="server"></asp:Label>
                                <asp:Label ID="lblTributePackageId" Text='<%# Eval("TributePackageId") %>' Visible="false"
                                    runat="server"></asp:Label>
                                <asp:LinkButton ID="lbtnAmount" CommandName="Select" Text='<%# Eval("AmountPaid") %>'
                                    CausesValidation="false" runat="server" Visible="false"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <fieldset class="yt-Form" id="CCdetails" runat="server" visible="false">
            <h4>
                Credit Card Information:</h4>
            <p class="yt-requiredFields">
                <strong>Required fields are indicated with <em class="required">* </em></strong>
            </p>
            <fieldset class="yt-PaymentSelection yt-CompactRadioList">
                <legend>* Select your payment method:</legend>
                <%--<asp:Literal ID="ltrPaymentMethod" runat="server">--%>
                <div class='yt-Form-Field yt-Form-Field-Radio' id='yt-CCVisa'>
                    <input type='radio' name='rdoCCType' runat='server' id='rdoCCVisa'
                        value='Visa' checked='True' />
                    <label for='rdoCCVisa'>
                        Visa</label>
                </div>
                <div class='yt-Form-Field yt-Form-Field-Radio' id='yt-CCMasterCard'>
                    <input type='radio' name='rdoCCType' runat='server' id='rdoCCMasterCard'
                        value='MasterCard' />
                    <label for='rdoCCMasterCard'>
                        MasterCard</label>
                </div>
                <%--</asp:Literal>--%>
                <asp:CustomValidator ID="cvPaymentMethod" runat="server" Font-Bold="True" Font-Size="Medium"
                    ForeColor="#FF8000" ClientValidationFunction="validatePaymentMethod" Text="!"
                    ValidationGroup="Update" ErrorMessage="Select your payment method." Width="1px"></asp:CustomValidator>
            </fieldset>
            <div class="yt-Form-Field">
                <label>
                    * Credit Card Number:</label>
                <asp:TextBox ID="txtCCNumber" CssClass="yt-Form-Input-Long" runat="server" MaxLength="16"
                    Width="280px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCCNumber" Font-Bold="True" Font-Size="Medium"
                    ValidationGroup="Update" ForeColor="#FF8000" ControlToValidate="txtCCNumber"
                    runat="server" Text="!" ErrorMessage="Credit Card Number is a required field."
                    Width="1px"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="cvValidateCCNumber" runat="server" Font-Bold="True" Font-Size="Medium"
                    ValidationGroup="Update" ForeColor="#FF8000" ClientValidationFunction="validateCreditCardLength"
                    Text="!" EnableClientScript="true" ErrorMessage="Please enter a valid Credit Card Number."
                    Width="1px"></asp:CustomValidator>
                <asp:CustomValidator ID="cvCCNumber" runat="server" Font-Bold="True" Font-Size="Medium"
                    ValidationGroup="Update" ForeColor="#FF8000" Text="!" ClientValidationFunction="CCardLength"
                    ErrorMessage="Please enter a valid credit card number. " Width="1px"></asp:CustomValidator>
            </div>
            <div class="yt-Form-Field">
                <label>
                    * Card Verification Code (CVC):</label>
                <asp:TextBox ID="txtCCVerification" CssClass="yt-Form-Input-Short" runat="server"
                    MaxLength="4" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ControlToValidate="txtCCVerification" Font-Bold="True"
                    ValidationGroup="Update" Font-Size="Medium" ForeColor="#FF8000" ID="rfvCCVerification"
                    Text="!" runat="server" ErrorMessage="Card Verification Code (CVC) is a required field."
                    Width="1px"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="cvCreditCardVerification" runat="server" Font-Bold="True"
                    Font-Size="Medium" ForeColor="#FF8000" ClientValidationFunction="validateCreditCardVerificationLength"
                    Text="!" EnableClientScript="true" ErrorMessage="Please enter a valid Credit Card Verification Number."
                    ValidationGroup="Update" Width="1px"></asp:CustomValidator>
                <asp:CustomValidator ID="CustomValidator4" runat="server" Font-Bold="True" Font-Size="Medium"
                    ValidationGroup="Update" ForeColor="#FF8000" Text="!" ClientValidationFunction="CVCLength"
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
                    <asp:TextBox ID="txtCCYear" CssClass="yt-Form-Input-Short" MaxLength="4" runat="server"></asp:TextBox>
                    <asp:CustomValidator ID="CustomValidator3" runat="server" Font-Bold="True" Font-Size="Medium"
                        ValidationGroup="Update" ForeColor="#FF8000" ClientValidationFunction="validateExpMonth"
                        Text="!" ErrorMessage="Expiry Date is a required field." Width="1px"></asp:CustomValidator>
                    <asp:CompareValidator ID="cpvtxtCCYear" Font-Bold="True" Operator="GreaterThanEqual"
                        ValidationGroup="Update" Font-Size="Medium" ForeColor="#FF8000" runat="server"
                        ErrorMessage="Expiry Date cannot be less than current date." ControlToValidate="txtCCYear"
                        Width="1px">!</asp:CompareValidator>
                    <asp:CustomValidator ID="cvCCYear" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"
                        ValidationGroup="Update" runat="server" ControlToValidate="txtCCYear" Text="!"
                        ErrorMessage="Please enter a valid expiry date." Width="1px"></asp:CustomValidator>
                    <span style="color: #FF8000; font-size: Medium; font-weight: bold;" id="SpanExpirDate"
                        runat="server" visible="false">!</span>
                    <label>
                        Year</label>
                </div>
            </fieldset>
            <div class="yt-Form-Field">
                <label>
                    <em class="required">* </em>Name on Card:</label>
                <asp:TextBox ID="txtCCName" CssClass="yt-Form-Input-Long" runat="server" MaxLength="50"
                    Width="280px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCCName" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"
                    ValidationGroup="Update" Text="!" runat="server" ErrorMessage="Name on Card is a required field."
                    ControlToValidate="txtCCName"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="revNameCard1" ControlToValidate="txtCCName" Text="!"
                    ValidationGroup="Update" runat="server" ErrorMessage="Name on Card can only contain letters,numbers,'-' and '#'"
                    ValidationExpression="^[a-zA-Z0-9,\#,\-,\s]*$" Font-Bold="True" Font-Size="Medium"
                    ForeColor="#FF8000"></asp:RegularExpressionValidator>
            </div>
            <div class="yt-Form-Field">
                <label>
                    <em class="required">* </em>Billing Address:</label>
                <asp:TextBox ID="txtCCBillingAddress" CssClass="yt-Form-Input-Long" runat="server"
                    MaxLength="50" Width="280px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCCBillingAddress"
                    ErrorMessage="Billing Address (line 1) can only contain letters,numbers,'-' and '#'"
                    ValidationGroup="Update" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"
                    Text="!" ValidationExpression="^[a-zA-Z0-9,\#,\-,\s]*$"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="rfvCCBillingAddress" runat="server" Text="!" Font-Bold="True"
                    ValidationGroup="Update" Font-Size="Medium" ForeColor="#FF8000" ControlToValidate="txtCCBillingAddress"
                    ErrorMessage="Billing Address is a required field."></asp:RequiredFieldValidator>
            </div>
            <div class="yt-Form-Field">
                <asp:TextBox ID="txtCCBillingAddress2" CssClass="yt-Form-Input-Long" runat="server"
                    MaxLength="50" Width="280px"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtCCBillingAddress2"
                    ErrorMessage="Billing Address (line 2) can only contain letters,numbers,'-' ans '#'"
                    ValidationGroup="Update" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"
                    Text="!" ValidationExpression="^[a-zA-Z0-9,\#,\-,\s]*$"></asp:RegularExpressionValidator>
            </div>
            <div class="yt-Form-Field">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <label>
                            <em class="required">* </em>Country:</label>
                        <asp:DropDownList ID="ddlCCCountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCCCountry_SelectedIndexChanged"
                            Width="285px">
                        </asp:DropDownList>
                        <br />
                        <label>
                            State/Province:</label>
                        <asp:DropDownList ID="ddlCCStateProvince" runat="server" Width="285px">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="yt-Form-Field">
                <label>
                    <em class="required">* </em>City:</label>
                <asp:TextBox ID="txtCCCity" CssClass="yt-Form-Input-Long" runat="server" Width="280px"
                    MaxLength="50"></asp:TextBox>
                <asp:RegularExpressionValidator ID="revtxtCCCity" runat="server" ControlToValidate="txtCCCity"
                    ValidationGroup="Update" ErrorMessage="City only contain letters,numbers,'-' and '#'"
                    Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9,\#,\-,\s]*$"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                        ID="rfvCCCity" runat="server" Text="!" ControlToValidate="txtCCCity" ErrorMessage="City is a required field."
                        Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"></asp:RequiredFieldValidator>
            </div>
            <div class="yt-Form-Field">
                <label>
                    <em class="required">* </em>Zip Code/Postal Code:</label>
                <asp:TextBox ID="txtCCZipCode" CssClass="yt-Form-Input" runat="server" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvCCZipCode" runat="server" Text="!" ControlToValidate="txtCCZipCode"
                    ErrorMessage="Zip Code/Postal Code is a required field." Font-Bold="True" Font-Size="Medium"
                    ValidationGroup="Update" ForeColor="#FF8000"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtCCZipCode"
                    ErrorMessage="Zip Code/Postal Code can only contain letters and numbers" Font-Bold="True"
                    ValidationGroup="Update" Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9]*$"></asp:RegularExpressionValidator>
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
                <asp:CustomValidator ID="cvPhoneNumber" Visible="true" runat="server" Font-Bold="True"
                    ValidationGroup="Update" Font-Size="Medium" ForeColor="#FF8000" ErrorMessage="Enter valid phone number.Format should be XXX-XXX-XXXX."
                    ClientValidationFunction="PhoneNumbervalidation">!</asp:CustomValidator>
            </div>
            <div class="yt-Form-Buttons">
                <div class="yt-Form-Delete">
                    <a href="javascript:void(0);" onclick="doModalDeleteConfirm();" class="yt-Button yt-XButton"
                        id="A1">Delete Credit Card Information</a>
                </div>
                <div class="yt-Form-Submit">
                    <asp:LinkButton ID="lbtnSaveChanges" CssClass="yt-Button yt-CheckButton" runat="server"
                        ValidationGroup="Update" OnClick="lbtnSaveChanges_Click">Save Changes</asp:LinkButton>
                </div>
            </div>
            <div id="yt-DeleteConfirmContainer">
                <div id="yt-DeleteConfirmContent" class="yt-ModalWrapper">
                    <div class="yt-Panel-Primary">
                        <h2>
                            Delete Credit Card</h2>
                        <h4>
                            Delete Credit Card - Error</h4>
                        <p>
                            You have chosen to automatically delete your credit card information – but if you
                            choose to delete, your automatically renewing
                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>s
                            no longer automatically renew!</p>
                        <div class="yt-Form-Buttons">
                            <div class="yt-Form-Delete" id="yt-CancelContainer">
                            </div>
                            <div class="yt-Form-Submit">
                                <asp:LinkButton ID="lbtndeleteCCinfo" CssClass="yt-Button yt-CheckButton" runat="server"
                                    OnClick="lbtndeleteCCinfo_Click" CausesValidation="False">That’s Okay, I want to Delete</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!--yt-SignUpFormContainer-->
        </fieldset>
        <fieldset class="yt-Form" id="Fieldset1" runat="server" visible="false">
            <div class="yt-Panel-Primary">
                <h4>
                    Credit Card Information:</h4>
                <p>
                    You have not stored any credit card information. Please select below to add your
                    credit card.</p>
                <div class="yt-Form-Buttons">
                    <div class="yt-Form-Delete">
                        <%-- <a href="javascript:void(0);" onclick="Blanktextboxes();" class="yt-Button yt-CheckButton">Add Credit
                                                            Card Information</a>--%>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:LinkButton ID="lbtnAddCreditCardInformation" CausesValidation="false" CssClass="yt-Button yt-CheckButton"
                                    runat="server" OnClick="lbtnAddCreditCardInformation_Click">Add Credit Card Information</asp:LinkButton>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </fieldset>
        <!--yt-Form-->
    </div>
    <!-- yt-BillingFormContainer -->
</asp:Content>
