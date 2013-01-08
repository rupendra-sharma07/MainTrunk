<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CCModelPopup.aspx.cs" Inherits="ModelPopup_CCModelPopup" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head>
    <title>Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> - My <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>: Privacy Settings</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Content-Language" content="en-ca" />
    <meta http-equiv="imagetoolbar" content="false" />
    <meta name="robots" content="index,follow" />
    <meta name="MSSmartTagsPreventParsing" content="true" />
    <!-- really basic, generic html class stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../test/assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/default.css" />
    <!-- default grid layout stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../test/assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/layouts/centered1024_12.css" />
    <!-- print-friendly stylesheet -->
    <link rel="stylesheet" type="text/css" media="print" href="../test/assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/print.css" />
    <!-- screen elements stylesheet should be here -->
    <link rel="stylesheet" type="text/css" href="../test/assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/screen.css" />
    <!-- larger text stylesheets -->
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="../test/assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/medium_text.css"
        title="medium_text" />
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="../test/assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/large_text.css"
        title="large_text" />
    <!-- Admin-specific elements stylesheet should be here -->
    <link rel="stylesheet" type="text/css" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/admin.css" />
    <!-- JS libraries -->

    <script type="text/javascript" src="../assets/scripts/mootools-1.11.js"></script>

    <script type="text/javascript" src="../assets/scripts/global.js"></script>

    <script type="text/javascript" src="../assets/scripts/styleSwitcher.js"></script>

    <script type="text/javascript" src="../assets/scripts/modalbox.js"></script>

    <script type="text/javascript" src="../assets/scripts/admin.js"></script>

    <script type="text/javascript" src="../Common/JavaScript/CreditCardValidation.js"></script>

    <script type="text/javascript">
    
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
		
		hideWideRows();
		
	});
	
	
	

    </script>
<!--#include file="../analytics.asp"-->
</head>
<body onload="SetValidationClear();">
    <form id="Form1" action="" runat="server">
    <div id="divShowModalPopup"></div> 
        <asp:HiddenField ID="hfPaymentMethod" runat="server" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div >
            <div >
                <asp:ValidationSummary CssClass="yt-Error" ID="VSAccountTrpe" ValidationGroup="accounttype"
                    runat="server" Width="630px" HeaderText="<h2>Oops - there is a problem with some account type information.</h2>                                                             <h3>Please correct the errors below:</h3>"
                    ForeColor="Black" />
            </div>
            <div >
                <div class="yt-Panel-Primary">
                    <h2>
                        Upgrade/Extend <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>s</h2>
                    <h4 class="yt-InfoBox">
                        You have chosen to pay for a <span id="yt-MembershipType"></span>membership for
                        the wedding <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> to John Smith &amp; Mary Walters</h4>
                    <fieldset class="yt-Form">
                        <p>
                            Please enter the following payment information:</p>
                        <p class="yt-requiredFields">
                            <strong>Required fields are indicated with <em class="required">* </em></strong>
                        </p>
                        <div class="yt-Form-Field">
                            <label for="txtCouponCode">
                                Enter your coupon code, if you have one:</label>
                            <input type="text" id="txtCouponCode" name="txtCouponCode" class="yt-Form-Input-Long" /><a
                                href="javascript: void(0);" class="yt-checkCoupon">Validate Coupon</a> <span class="availabilityNotice">
                                </span>
                            <div class="hint">
                                hint for coupon code...<span class="hintPointer"></span></div>
                        </div>
                        <fieldset class="yt-PaymentSelection yt-CompactRadioList">
                            <legend>* Select your payment method:</legend>
                            <div class="yt-Form-Field yt-Form-Field-Radio" id="yt-CCMasterCard">
                                <input type="radio" onclick='Check(this);' name="rdoCCType" id="rdoCCMasterCard" value="MasterCard" />
                                <label for="rdoCCMasterCard">
                                    Master Card</label>
                            </div>
                            <div class="yt-Form-Field yt-Form-Field-Radio" id="yt-CCVisa">
                                <input type="radio" onclick='Check(this);' name="rdoCCType" id="rdoCCVisa" value="Visa" />
                                <label for="rdoCCVisa">
                                    Visa</label>
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
                            <asp:TextBox ID="txtCCNumber" CssClass="yt-Form-Input-Long" runat="server" MaxLength="16"
                                Width="280px" ValidationGroup="accounttype"></asp:TextBox><asp:RequiredFieldValidator
                                    ValidationGroup="accounttype" ID="rfvCCNumber" Font-Bold="True" Font-Size="Medium"
                                    ForeColor="#FF8000" ControlToValidate="txtCCNumber" runat="server" Text="!" ErrorMessage="Credit Card Number is a required field."
                                    Width="1px"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="cvCCNumber" ValidationGroup="accounttype" runat="server"
                                Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" Text="!" ClientValidationFunction="CCardLength"
                                ErrorMessage="Please enter a valid credit card number. " Width="1px"></asp:CustomValidator>
                        </div>
                        <div class="yt-Form-Field">
                            <label>
                                * Card Verification Code (CVC):</label>
                            <asp:TextBox ID="txtCCVerification" ValidationGroup="accounttype" CssClass="yt-Form-Input-Short"
                                runat="server" MaxLength="4"></asp:TextBox>
                            <asp:RequiredFieldValidator ValidationGroup="accounttype" ControlToValidate="txtCCVerification"
                                Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ID="rfvCCVerification"
                                Text="!" runat="server" ErrorMessage="Card Verification Code (CVC) is a required field."
                                Width="1px"></asp:RequiredFieldValidator>
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
                                runat="server" ControlToValidate="txtCCBillingAddress" ErrorMessage="Billing Address only contain letters,numbers,'-' and '#'"
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
                            <asp:RadioButton ID="rdoYearlyAutoRenew" Checked="True" Text="I want this tribute to be renewed automatically on a yearly basis."
                                GroupName="rdoRenew" runat="server" />
                        </div>
                        <div class="yt-Form-Field yt-Form-Field-Radio" id="divNotifyBeforeRenew" runat="server">
                            <asp:RadioButton ID="rdoNotifyBeforeRenew" Text="I do not want this tribute to be renewed automatically on a yearly basis, but I
                                                                    will be notified when the account is near to expiry."
                                GroupName="rdoRenew" runat="server" />
                        </div>
                        <div class="yt-Form-Field yt-Form-Field-Checkbox">
                            <asp:CheckBox ID="chkSaveBillingInfo" Text="I would like to save the above billing information in my profile."
                                runat="server" />
                        </div>
                        <div class="yt-Form-Field yt-Form-Field-Checkbox">
                            <asp:CheckBox ID="chkAgree" runat="server" Text="I have read and agree to the <a href='termsofuse.aspx' target='_blank' >terms of use</a>, the cancellation/refund
                                                                    policy (outlined in the terms of use) and the <a href='privacy.aspx'  target='_blank' >privacy policy</a>."
                                Checked="True" />
                        </div>
                        <div class="yt-InfoBox" id="yt-PaymentTotal">
                           You will be charged: <span id="yt-BillingTotal"></span>
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
       
        <!--yt-upgrade-->
    </form>

    <script type="text/javascript">
executeBeforeLoad();
    </script>

</body>
</html>
