<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SponsorTribute.aspx.cs" Inherits="Tribute_SponsorTribute"
    Title="SponsorTribute" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head>
    <title>Your Tribute - Create Tribute Step 5</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Content-Language" content="en-ca" />
    <meta http-equiv="imagetoolbar" content="false" />
    <meta name="robots" content="index,follow" />
    <meta name="MSSmartTagsPreventParsing" content="true" />
    <!-- really basic, generic html class stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../Test/assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/default.css" />
    <!-- default grid layout stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../Test/assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/layouts/centered1024_1.css" />
    <!-- print-friendly stylesheet -->
    <link rel="stylesheet" type="text/css" media="print" href="../Test/assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/print.css" />
    <!-- screen elements stylesheet should be here -->
    <link rel="stylesheet" type="text/css" href="../Test/assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/screen.css" />
    <!-- create process specific stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../Test/assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/tributecreation.css" />
    <!-- larger text stylesheets -->
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="../Test/assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/medium_text.css"
        title="medium_text" />
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="../Test/assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/large_text.css"
        title="large_text" />
    <!-- Website Favorite Icon -->
    <link rel="Shortcut Icon" type="image/x-icon" href="../assets/images/favicon.ico" />
    <!-- JS libraries -->

    <script type="text/javascript" src="../Test/assets/scripts/mootools-1.11.js"></script>

    <script type="text/javascript" src="../Test/assets/scripts/global.js"></script>

    <script type="text/javascript" src="../Test/assets/scripts/styleSwitcher.js"></script>

    <script type="text/javascript" src="../Test/assets/scripts/modalbox.js"></script>

    <script type="text/javascript" src="../Test/assets/scripts/tributecreation.js"></script>

    <!--#include file="../analytics.asp"-->
</head>

<script type="text/javascript">
    <!--
    function hideWideRows() {
	    $$('.yt-colWide').each( function(a) {
		    a.getParent().addClass('yt-HiddenRow');
	    });
	    $$('.yt-ButtonDetails').each( function(a) {
		    a.removeClass('yt-Open');
	    });
    }
    function showWideRows() {
	    $$('.yt-colWide').each( function(a) {
		    a.getParent().removeClass('yt-HiddenRow');
	    });
	    $$('.yt-ButtonDetails').each( function(a) {
		    a.addClass('yt-Open');
	    });
    }

    function showDetails(theButton) {
	    if(theButton.hasClass('yt-Open')) {
		    hideWideRows();
	    } else {
		    hideWideRows();
		    theButton.getParent().getParent().getNext().removeClass('yt-HiddenRow');
		    theButton.addClass('yt-Open');
	    }
    }


    function SelectMembership(membershipType) {
	    switch (membershipType.value) {
		    case 'Lifetime':
			    $('BillingTotal').setHTML("$50");
			    break;
		    case 'Yearly':
			    $('BillingTotal').setHTML("$20");
			    break;
	    }
    }
    
function SelectAccount(source, args)
 {
           var rdb1=document.getElementById('<%= rdoMembershipYearly.ClientID %>');
           var rdb2=document.getElementById('<%= rdoMembershipLifetime.ClientID %>')
           if((!rdb1.checked)&&(!rdb2.checked))
           args.IsValid=false;
           else
           args.IsValid=true;
 }
    -->
    


</script>

<body>
    <form action="" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="divShowModalPopup">
    </div>
    <div class="yt-Container yt-TributeCreation yt-Step5">
        <div class="yt-HeaderContainer">
            <div class="yt-Header">
                <a href="javascript:void(0);" title="Return to Home Page" class="yt-Logo"></a>
                <div class="yt-HeaderControls">
                    <div class="yt-NavHeader">
                        <div class="yt-UserInfo">
                            <span>Usernamelong</span> <a href="javascript:void(0);">Log out</a>
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
                        <div>
                            <asp:ValidationSummary CssClass="yt-Error" ID="ValidationSummary2" runat="server"
                                HeaderText=" <h2>Oops - there was a problem with some account type information.</h2>                                                             <h3>Please correct the errors below:</h3>"
                                ForeColor="Black" ValidationGroup="Sponsor" Width="647px" />
                        </div>
                        <div>
                            <div class="yt-ProcessStepDisplay">
                                <fieldset class="yt-Form">
                                    <table border="0" cellspacing="0" cellpadding="0" class="yt-overlapHead yt-AccountTypeTable"
                                        id="yt-AccountTypeSelection">
                                        <thead>
                                            <tr>
                                                <th>
                                                    choose your account type
                                                    <asp:CustomValidator ID="CustomValidator1" runat="server" ForeColor="Transparent"
                                                        ClientValidationFunction="SelectAccount" ErrorMessage=" Please select the account type for this tribute."
                                                        ValidationGroup="Sponsor">.</asp:CustomValidator>
                                                </th>
                                                <th>
                                                    Account Price
                                                </th>
                                                <th>
                                                    Includes All Features
                                                </th>
                                                <th>
                                                    Advertising Free
                                                </th>
                                                <th>
                                                    No Renewal Required
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <th>
                                                    <div class="yt-Form-Field-Radio">
                                                        <input type="radio" onclick="SelectMembership(this);" name="rdoMembershipType" id="rdoMembershipLifetime"
                                                            value="Lifetime" runat="server" /><label for="rdoMembershipLifetime">Lifetime</label></div>
                                                    <a href="javascript: void(0);" class="yt-ButtonDetails" onclick="showDetails(this);">
                                                        Details</a>
                                                </th>
                                                <td class="yt-Cost">
                                                    &#36;50
                                                </td>
                                                <td>
                                                    <img src="../test/assets/images/icon_check.gif" alt="Yes" width="21" height="21" />
                                                </td>
                                                <td>
                                                    <img src="../test/assets/images/icon_check.gif" alt="Yes" width="21" height="21" />
                                                </td>
                                                <td>
                                                    <img src="../test/assets/images/icon_check.gif" alt="Yes" width="21" height="21" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" class="yt-colWide">
                                                    Full feature set and no advertising on your tribute site. You also never have to
                                                    worry about renewing your membership. If you choose this option, it means forever
                                                    – you will not be able to downgrade to a yearly membership, nor get a refund if
                                                    you choose to get a remove the tribute site at any point.
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>
                                                    <div class="yt-Form-Field-Radio">
                                                        <input type="radio" onclick="SelectMembership(this);" runat="server" name="rdoMembershipType"
                                                            id="rdoMembershipYearly" value="Yearly" /><label for="rdoMembershipYearly">1 Year</label></div>
                                                    <a href="javascript: void(0);" class="yt-ButtonDetails" onclick="showDetails(this);">
                                                        Details</a>
                                                </th>
                                                <td class="yt-Cost">
                                                    &#36;20 / year
                                                </td>
                                                <td>
                                                    <img src="../test/assets/images/icon_check.gif" alt="Yes" width="21" height="21" />
                                                </td>
                                                <td>
                                                    <img src="../test/assets/images/icon_check.gif" alt="Yes" width="21" height="21" />
                                                </td>
                                                <td>
                                                    <img src="../test/assets/images/icon_x.gif" alt="No" width="21" height="21" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" class="yt-colWide">
                                                    Full feature set an no advertising on your tribute site. The year membership begins
                                                    as soon as you have completed this creation process, and near to renewal time you
                                                    and any other administrators will be notified to allow you to renew your membership
                                                    if you’d like. If your tribute site goes past the 1 year membership period without
                                                    renewal of membership, the content you and others have created will be lost. Memberships
                                                    are for a full year – you won’t be eligable for a refund if you choose to remove
                                                    a tribute site at any point during your membership year.
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <fieldset class="yt-Inform">
                                        <legend>Would you like the administrators of the site to know who you are?</legend>
                                        <div class="yt-Form-Field yt-Form-Field-Radio">
                                            <input type="radio" name="rdoInform" runat="server" id="rdoInformYes" value="Yes" />
                                            <label for="rdoInformYes">
                                                Yes</label>
                                        </div>
                                        <div class="yt-Form-Field yt-Form-Field-Radio">
                                            <input type="radio" name="rdoInform" runat="server" id="rdoInformNo" value="No" />
                                            <label for="rdoInformNo">
                                                No</label>
                                        </div>
                                    </fieldset>
                                    <!-- DIV to hide content until membership type is selected -->
                                    <div>
                                        <p>
                                            Please enter the following payment information:</p>
                                        <p class="yt-requiredFields">
                                            <strong>Required fields are indicated with <em class="required">* </em></strong>
                                        </p>
                                        <asp:UpdatePanel ID="PnlCoupon" runat="server">
                                            <ContentTemplate>
                                                <div class="yt-Form-Field">
                                                    <label for="txtCouponCode">
                                                        Enter your coupon code, if you have one:</label>
                                                    <asp:TextBox ID="txtCouponCode" runat="server" CssClass="yt-Form-Input-Long" MaxLength="18"
                                                        ValidationGroup="Sponsor"></asp:TextBox>
                                                    <asp:LinkButton ID="lbtnValidateCoupon" OnClick="lbtnValidateCoupon_Click" runat="server"
                                                        CausesValidation="False" CssClass="yt-checkCoupon" ValidationGroup="Sponsor">Validate Coupon</asp:LinkButton>
                                                    <span id="spanCoupon" class="availabilityNotice" runat="server"></span>
                                                    <div class="hint">
                                                        If you have a coupon code enter it here and click "Validate Coupon". If the coupon
                                                        code is correct, the discount will be applied to your total at the bottom of the
                                                        page<span class="hintPointer"></span></div>
                                                </div>
                                                <fieldset class="yt-PaymentSelection yt-CompactRadioList">
                                                    <legend>* Select your payment method:</legend>
                                                    <asp:Literal ID="ltrPaymentMethod" runat="server"></asp:Literal>
                                                    <asp:CustomValidator ID="cvPaymentMethod" runat="server" Font-Bold="True" Font-Size="Medium"
                                                        ForeColor="#FF8000" ClientValidationFunction="validatePaymentMethod" Text="!"
                                                        ValidationGroup="Sponsor" ErrorMessage="Select your payment method." Width="1px"></asp:CustomValidator>
                                                </fieldset>
                                                <div class="yt-Form-Field">
                                                    <label>
                                                        * Card Number:</label>
                                                    <asp:TextBox ID="txtCCNumber" CssClass="yt-Form-Input-Long" runat="server" MaxLength="16"
                                                        Width="280px" ValidationGroup="Sponsor"></asp:TextBox><asp:RequiredFieldValidator
                                                            ID="rfvCCNumber" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ControlToValidate="txtCCNumber"
                                                            runat="server" Text="!" ErrorMessage="Credit Card Number is a required field."
                                                            Width="1px" ValidationGroup="Sponsor"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="yt-Form-Field">
                                                    <label>
                                                        * Card verification:</label>
                                                    <asp:TextBox ID="txtCCVerification" CssClass="yt-Form-Input-Short" runat="server"
                                                        MaxLength="4" ValidationGroup="Sponsor"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="revCCVerification" runat="server" ControlToValidate="txtCCVerification"
                                                        ErrorMessage="Please enter a valid card verification code (CVC)." Font-Bold="True"
                                                        Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="\d{4}"
                                                        Width="1px" ValidationGroup="Sponsor"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                                            ControlToValidate="txtCCVerification" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"
                                                            ID="rfvCCVerification" Text="!" runat="server" ErrorMessage="Card Verification Code (CVC) is a required field."
                                                            Width="1px" ValidationGroup="Sponsor"></asp:RequiredFieldValidator>
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
                                                        <asp:TextBox ID="txtCCYear" CssClass="yt-Form-Input-Short" MaxLength="4" runat="server"></asp:TextBox><asp:CustomValidator
                                                            ID="CustomValidator3" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"
                                                            ClientValidationFunction="validateExpMonth" Text="!" ValidationGroup="Sponsor"
                                                            ErrorMessage="Expiry Date is a required field." Width="1px"></asp:CustomValidator><asp:CompareValidator
                                                                ID="cpvtxtCCYear" Font-Bold="True" Operator="GreaterThanEqual" Font-Size="Medium"
                                                                ForeColor="#FF8000" ValidationGroup="Sponsor" runat="server" ErrorMessage="Expiry Date cannot be less than current date."
                                                                ControlToValidate="txtCCYear" Visible="false" Width="1px">!</asp:CompareValidator><asp:CustomValidator
                                                                    ID="cvCCYear" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" runat="server"
                                                                    ControlToValidate="txtCCYear" Text="!" ValidationGroup="Sponsor" ErrorMessage="Please enter a valid expiry date."
                                                                    Width="1px"></asp:CustomValidator>
                                                        <label>
                                                            Year</label>
                                                    </div>
                                                </fieldset>
                                                <div class="yt-Form-Field">
                                                    <label>
                                                        <em class="required">* </em>Name on Card:</label>
                                                    <asp:TextBox ID="txtCCName" CssClass="yt-Form-Input-Long" runat="server" MaxLength="50"
                                                        Width="280px" ValidationGroup="Sponsor"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvCCName" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"
                                                        Text="!" runat="server" ErrorMessage="Name on Card is a required field." ControlToValidate="txtCCName"
                                                        ValidationGroup="Sponsor"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="yt-Form-Field">
                                                    <label>
                                                        <em class="required">* </em>Billing Address:</label>
                                                    <asp:TextBox ID="txtCCBillingAddress" CssClass="yt-Form-Input-Long" runat="server"
                                                        MaxLength="50" Width="280px" ValidationGroup="Sponsor"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCCBillingAddress"
                                                        ErrorMessage="Billing Address only contain letters,numbers,'-' and '#'" Font-Bold="True"
                                                        Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9,\#,\-,\s]*$"
                                                        ValidationGroup="Sponsor"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                                            ID="rfvCCBillingAddress" runat="server" Text="!" Font-Bold="True" Font-Size="Medium"
                                                            ForeColor="#FF8000" ControlToValidate="txtCCBillingAddress" ErrorMessage="Billing Address is a required field."
                                                            ValidationGroup="Sponsor"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="yt-Form-Field">
                                                    <asp:TextBox ID="txtCCBillingAddress2" CssClass="yt-Form-Input-Long" runat="server"
                                                        MaxLength="50" Width="280px" ValidationGroup="Sponsor"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtCCBillingAddress2"
                                                        ErrorMessage="Billing Address only contain letters,numbers,'-' ans '#'" Font-Bold="True"
                                                        Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9,\#,\-,\s]*$"
                                                        ValidationGroup="Sponsor"></asp:RegularExpressionValidator>
                                                </div>
                                                <div class="yt-Form-Field">
                                                    <%--<asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>--%>
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
                                                    <%-- </ContentTemplate>
                                                    </asp:UpdatePanel>--%>
                                                </div>
                                                <div class="yt-Form-Field">
                                                    <label>
                                                        <em class="required">* </em>City:</label>
                                                    <asp:TextBox ID="txtCCCity" CssClass="yt-Form-Input-Long" runat="server" Width="280px"
                                                        MaxLength="50" ValidationGroup="Sponsor"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="revtxtCCCity" runat="server" ControlToValidate="txtCCCity"
                                                        ErrorMessage="City only contain letters,numbers,'-' and '#'" Font-Bold="True"
                                                        Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9,\#,\-,\s]*$"
                                                        ValidationGroup="Sponsor"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                                            ID="rfvCCCity" runat="server" Text="!" ControlToValidate="txtCCCity" ErrorMessage="City is a required field."
                                                            Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ValidationGroup="Sponsor"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="yt-Form-Field">
                                                    <label>
                                                        <em class="required">* </em>Zip Code/Postal Code:</label>
                                                    <asp:TextBox ID="txtCCZipCode" CssClass="yt-Form-Input" runat="server" MaxLength="10"
                                                        ValidationGroup="Sponsor"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvCCZipCode" runat="server" Text="!" ControlToValidate="txtCCZipCode"
                                                        ErrorMessage="Zip Code/Postal Code is a required field." Font-Bold="True" Font-Size="Medium"
                                                        ForeColor="#FF8000" ValidationGroup="Sponsor"></asp:RequiredFieldValidator>
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
                                                    <asp:CustomValidator ID="cvPhoneNumber" runat="server" ValidationGroup="Sponsor"
                                                        Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ErrorMessage="Enter valid phone number.Format should be XXX-XXX-XXXX."
                                                        ClientValidationFunction="PhoneNumbervalidation">!</asp:CustomValidator>
                                                </div>
                                                <div class="yt-Form-Field yt-Form-Field-Checkbox">
                                                    <asp:CheckBox ID="chkSaveBillingInfo" Text="I would like to save the above billing information in my profile."
                                                        runat="server" />
                                                </div>
                                                <div class="yt-Form-Field yt-Form-Field-Checkbox">
                                                    <input type="checkbox" name="chkAgree" id="chkAgree" runat="server" value="AgreeToTerms" />
                                                    <label for="chkAgree">
                                                        <em class="required">* </em>I have read and agree to the <a href='termsofuse.aspx'
                                                            target='_blank'>terms of use</a>, the cancellation/refund policy (outlined in
                                                        the terms of use) and the <a href='privacy.aspx' target='_blank'>privacy policy</a>.</label>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="yt-InfoBox" id="yt-PaymentTotal">
                                            You will be charged: <span id="BillingTotal" runat="server"></span>
                                        </div>
                                        <p>
                                            If you have reviewed all of the above information and it is correct, you must be
                                            ready to...
                                            <asp:CustomValidator ID="cvAcceptPolicies" Text="!" ForeColor="White" ErrorMessage="Please accept that you have read and agree to the terms of use, cancellation/refund policy and the privacy policy."
                                                runat="server" ClientValidationFunction="ValidateTandCs" Width="1px" ValidationGroup="Sponsor"></asp:CustomValidator>
                                        </p>
                                        <div class="yt-Form-Buttons">
                                            <div class="yt-Form-Submit">
                                                <asp:LinkButton ID="lbtnPay" ValidationGroup="Sponsor" CssClass="yt-Button yt-ArrowButton"
                                                    runat="server" OnClick="lbtnPay_Click">Pay!</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <!-- end yt-Form-Buttons-->
                                </fieldset>
                            </div>
                            <!--yt-ProcessStepDisplay-->
                        </div>
                        <!--yt-TributeProcess-->
                    </div>
                    <!--yt-ContentPrimary-->
                </div>
                <!--yt-ContentPrimaryContainer-->
                <div class="hack-clearBoth">
                </div>
                <div class="yt-ContentContainerImage">
                </div>
            </div>
            <!--yt-ContentContainerInner-->
        </div>
        <!--yt-ContentContainer-->
        <div class="yt-Footer">
            <ul class="yt-NavFooter">
                <li><a href="javascript:void(0);">Help</a></li>
                <li><a href="javascript:void(0);">Contact Us</a></li>
                <li><a href="javascript:void(0);">About Us</a></li>
            </ul>
            <div class="yt-Legal">
                <ul class="yt-NavFooter">
                    <li>&#169; 2008 <a href="javascript:void(0);">Your Tribute</a></li>
                    <li><a href="javascript:void(0);">Terms of Use</a></li>
                    <li><a href="javascript:void(0);">Privacy Policy</a></li>
                </ul>
            </div>
        </div>
        <!--yt-Footer-->
    </div>
    <!--yt-Container-->
    <div class="upgrade">
        <h2>
            Please Upgrade Your Browser</h2>
        <p>
            This site&#39;s design is only visible in a graphical browser that supports <a href="http://www.webstandards.org/upgrade/"
                title="The Web Standards Project's BROWSER UPGRADE initiative.">web standards</a>,
            but its content is accessible to any browser or Internet device.</p>
    </div>
    <!--yt-upgrade-->
    </form>

    <script type="text/javascript">

executeBeforeLoad();

    </script>
    
    <!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
    <script type="text/javascript" src="../assets/scripts/BrowserOrTabCloseHandler.js"></script>

</body>
</html>
