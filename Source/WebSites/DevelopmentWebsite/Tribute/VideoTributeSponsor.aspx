<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VideoTributeSponsor.aspx.cs"
    Inherits="Tribute_VideoTributeSponsor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../UserControl/Header.ascx" TagName="Header" TagPrefix="uc" %>
<%@ Register Src="../UserControl/Footer.ascx" TagName="Footer" TagPrefix="uc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:fb="http://www.facebook.com/2008/fbml"
xml:lang="en" lang="en">
<head>
    <title>Video Tribute Sponsor</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Content-Language" content="en-ca" />
    <meta http-equiv="imagetoolbar" content="false" />
    <meta name="robots" content="index,follow" />
    <meta name="MSSmartTagsPreventParsing" content="true" />
    <!-- really basic, generic html class stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_SCRIPT_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/default.css" />
    <!-- default grid layout stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_SCRIPT_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/layouts/centered1024_1.css" />
    <!-- print-friendly stylesheet -->
    <link rel="stylesheet" type="text/css" media="print" href="<%=Session["APP_SCRIPT_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/print.css" />
    <!-- screen elements stylesheet should be here -->
    <link rel="stylesheet" type="text/css" href="<%=Session["APP_SCRIPT_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/screen.css" />
    <!-- create process specific stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_SCRIPT_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/tributecreation.css" />
    <!-- larger text stylesheets -->
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_SCRIPT_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/medium_text.css"
        title="medium_text" />
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_SCRIPT_PATH"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/large_text.css"
        title="large_text" />
    <!-- Website Favorite Icon -->
    <link rel="Shortcut Icon" type="image/x-icon" href="<%=Session["APP_SCRIPT_PATH"]%>assets/images/favicon.ico" />
    <!-- JS libraries -->

    <script type="text/javascript" src="<%=Session["APP_SCRIPT_PATH"]%>assets/scripts/mootools-1.11.js"></script>

    <script type="text/javascript" src="<%=Session["APP_SCRIPT_PATH"]%>assets/scripts/global.js"></script>

    <script type="text/javascript" src="<%=Session["APP_SCRIPT_PATH"]%>assets/scripts/styleSwitcher.js"></script>

    <script type="text/javascript" src="<%=Session["APP_SCRIPT_PATH"]%>assets/scripts/tributecreation.js"></script>

    <script type="text/javascript" src="<%=Session["APP_SCRIPT_PATH"]%>Common/JavaScript/Common.js"></script>

    <script type="text/javascript" language="javascript" src="<%=Session["APP_SCRIPT_PATH"]%>Common/JavaScript/TributeHomePage.js"> </script>

   <script src="https://connect.facebook.net/en_US/all.js" type="text/javascript"></script>

    <!--#include file="../analytics.asp"-->
</head>

<script type="text/javascript">
    <!--
    function hideWideRows() {
        $$('.yt-colWide').each(function(a) {
            a.getParent().addClass('yt-HiddenRow');
        });
        $$('.yt-ButtonDetails').each(function(a) {
            a.removeClass('yt-Open');
        });
    }
    function showWideRows() {
        $$('.yt-colWide').each(function(a) {
            a.getParent().removeClass('yt-HiddenRow');
        });
        $$('.yt-ButtonDetails').each(function(a) {
            a.addClass('yt-Open');
        });
    }

    function showDetails(theButton) {
        if (theButton.hasClass('yt-Open')) {
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
                DivRenew.style.visibility = "hidden";
                break;
            case 'Yearly':
                $('BillingTotal').setHTML("$30");
                DivRenew.style.visibility = "visible";
                break;
        }
    }


    function validateExpMonth(source, args) {
        var bol = true;
        var month = document.getElementById('<%=ddlCCMonth.ClientID%>');
        var year = document.getElementById('<%=txtCCYear.ClientID%>');
        var validat = document.getElementById('<%=CustomValidator3.ClientID%>');
        args.IsValid = ExpMonthvalidate(month, year, validat);
    }

    function ValidateTandCs(source, args) {
        args.IsValid = document.getElementById('<%= chkAgree.ClientID %>').checked;

    }
    function SelectAccount(source, args) {

        var rdb1 = document.getElementById('<%= rdoMembershipYearly.ClientID %>');
        var rdb2 = document.getElementById('<%= rdoMembershipLifetime.ClientID %>')
        if (rdb1 != null) {
            if ((!rdb1.checked) && (!rdb2.checked))
                args.IsValid = false;
            else
                args.IsValid = true;
        }
    }
    function validatePaymentMethod(source, args) {
        var bool = false;
        var rdb1 = $('rdoCCVisa');
        var rdb4 = $('rdoCCMasterCard');
        var rdb2 = $('rdoCCAmex');
        var rdb3 = $('rdoCCDiscover');
        if ((!rdb1.checked)  && (!rdb4.checked))
            bool = false;
        else
            bool = true;

        args.IsValid = bool;

    }
    function validateCreditCardLength(source, args) {

        var bool = false;
        var rdb1 = $('rdoCCVisa');
        var rdb2 = $('rdoCCAmex');
        var rdb3 = $('rdoCCDiscover');
        var rdb4 = $('rdoCCMasterCard');
        var val = $('txtCCNumber').value;

        if (val.length == 0) {
            bool = true;
            args.IsValid = bool;
            return;
        }

        if (rdb1.checked || rdb4.checked || rdb3.checked) {

            if (val.length != 16) {
                bool = false;
            }
            else
                bool = true;
        }
        else {
            if (val.length != 15) {
                bool = false;
            }
            else
                bool = true;
        }

        args.IsValid = bool;
    }


    function validateCreditCardVerificationLength(source, args) {
        var bool = false;
        var rdb1 = $('rdoCCVisa');
        var rdb2 = $('rdoCCAmex');
        var rdb3 = $('rdoCCDiscover');
        var rdb4 = $('rdoCCMasterCard');
        var val = $('txtCCVerification').value;

        if (val.length == 0) {
            bool = true;
            args.IsValid = bool;
            return;
        }


        if (rdb1.checked || rdb4.checked || rdb3.checked) {
            if (val.length != 3) {
                bool = false;
            }
            else
                bool = true;
        }
        else {
            if (val.length != 4) {
                bool = false;
            }
            else
                bool = true;
        }

        args.IsValid = bool;
    }

    function Check(rdb) {
        var rdb1 = $('hfPaymentMethod');
        rdb1.value = rdb.value;
    }

    function LoginUser(source, args) {
        var UserName = $('txtLoginUsername1');
        var Password = $('txtLoginPassword1');
        args.IsValid = UserLogin(UserName, Password);
    }

    function PhoneNumbervalidation(source, args) {
        var number1 = document.getElementById('<%= txtPhoneNumber1.ClientID %>');
        var number2 = document.getElementById('<%= txtPhoneNumber2.ClientID %>');
        var number3 = document.getElementById('<%= txtPhoneNumber3.ClientID %>');
        var validator = document.getElementById('<%= cvPhoneNumber.ClientID %>');

        args.IsValid = PhoneNumberValidate1(number1, number2, number3, validator);
    }

    function maxLength(txtid) {
        var txtVal = txtid.value;
        return chkForMaxLength_(250, txtVal.length);
    }

    function MakeAutoRenew() {
        var rdb = $('rdoAutoRenew');
        var chk = document.getElementById('<%=chkSaveBillingInfo.ClientID%>');
        if (rdb) {
            if (rdb.checked == true) {
                chk.checked = true;
                chk.disabled = true;
            }
            else {
                chk.checked = false;
                chk.disabled = false;
            }
        }
    }

    function OnPayClick() {
        if (Page_ClientValidate()) {
            document.getElementById("lblProcess").innerHTML = "Please wait";
            document.getElementById("lbtnPay").style.display = "none";
        }
    }
-->
</script>

<body>
    <form id="Form1" action="" runat="server">
    <asp:HiddenField ID="hfPaymentMethod" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="false">
    </asp:ScriptManager>
    <div id="divShowModalPopup">
    </div>
    <div class="yt-Container yt-Admin">
        <uc:Header ID="ytHeader" Section="tribute" runat="server" />
        <div class="hack-clearBoth">
        </div>
        <div class="yt-ContentContainer">
            <div class="yt-ContentContainerInner">
                <div class="yt-ContentPrimaryContainer">
                    <div class="yt-ContentPrimary">
                        <div>
                            <div id="lblErrMsg" runat="server" class="yt-Error" visible="false">
                            </div>
                            <div id="yt1">
                                <!-- if the yt-SponsorError div has content (including spaces, CR/LF characters, etc, the modal login will show on page load -->
                            </div>
                        </div>
                        <!-- please display this notice when returning to this screen after deleting a tribute -->
                        <div id="yt-SponsorContent" class="yt-ModalWrapper">
                            <asp:ValidationSummary ID="ValidationSummary1" CssClass="yt-Error" HeaderText=" <h2>Oops - there was a problem with some account type information.</h2>                                                             <h3>Please correct the errors below:</h3>"
                                ForeColor="Black" runat="server" Width="652px" />
                            <div class="yt-Panel-Primary">
                                <h2>
                                    Sponsor Video Tribute:</h2>
                                <fieldset class="yt-Form">
                                    <div id="chooseoption" runat="server">
                                        <h4>
                                            You can choose one of the following options to pay for the tribute :</h4>
                                    </div>
                                    <table border="0" cellspacing="0" cellpadding="0" class="yt-overlapHead yt-AccountTypeTable"
                                        id="Table1">
                                        <thead>
                                            <tr>
                                                <th>
                                                    choose your account type
                                                    <asp:CustomValidator ID="CustomValidator1" runat="server" ForeColor="Transparent"
                                                        ClientValidationFunction="SelectAccount" ErrorMessage=" Please select the account type for this tribute.">.</asp:CustomValidator>
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
                                            <tr id="trYearly" runat="server">
                                                <th>
                                                    <div class="yt-Form-Field-Radio">
                                                        <asp:RadioButton ID="rdoMembershipYearly" GroupName="rdoMembershipType" Text="Video Tribute (1 Year)"
                                                            runat="server" AutoPostBack="True" OnCheckedChanged="rdoMembershipYearly_CheckedChanged" />
                                                    </div>
                                                    </th>
                                                <td class="yt-Cost">
                                                    $19.95
                                                </td>
                                                <td>
                                                    <img src="<%=Session["APP_SCRIPT_PATH"]%>assets/images/icon_check.gif" alt="Yes"
                                                        width="21" height="21" />
                                                </td>
                                                <td>
                                                    <img src="<%=Session["APP_SCRIPT_PATH"]%>assets/images/icon_check.gif" alt="Yes"
                                                        width="21" height="21" />
                                                </td>
                                                <td>
                                                    <img src="<%=Session["APP_SCRIPT_PATH"]%>assets/images/icon_x.gif" alt="No" width="21"
                                                        height="21" />
                                                </td>
                                            </tr>                                           
                                            <tr>
                                                <th>
                                                    <div class="yt-Form-Field-Radio">
                                                        <asp:RadioButton ID="rdoMembershipLifetime" GroupName="rdoMembershipType" Text="Video Tribute (Life)"
                                                            runat="server" AutoPostBack="True" OnCheckedChanged="rdoMembershipLifetime_CheckedChanged" />
                                                    </div>                                                    
                                                </th>
                                                <td class="yt-Cost">
                                                    &#36;49.95
                                                </td>
                                                <td>
                                                    <img src="<%=Session["APP_SCRIPT_PATH"]%>assets/images/icon_check.gif" alt="Yes"
                                                        width="21" height="21" />
                                                </td>
                                                <td>
                                                    <img src="<%=Session["APP_SCRIPT_PATH"]%>assets/images/icon_check.gif" alt="Yes"
                                                        width="21" height="21" />
                                                </td>
                                                <td>
                                                    <img src="<%=Session["APP_SCRIPT_PATH"]%>assets/images/icon_check.gif" alt="Yes"
                                                        width="21" height="21" />
                                                </td>
                                            </tr>                                           
                                        </tbody>
                                    </table>                                    
                                    <p>
                                        Please enter the following payment information:</p>
                                    <p class="yt-requiredFields">
                                        <strong>Required fields are indicated with <em class="required">* </em></strong>
                                    </p>
                                    <asp:UpdatePanel ID="pnlCoupon" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="yt-Form-Field">
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
                                            </div>
                                            <div id="PnlPaymentDetails" runat="server" visible="false">
                                                <fieldset class="yt-PaymentSelection yt-CompactRadioList">
                                                    <legend><em class="required">* </em>Select your payment method:</legend>
                                                    <asp:Literal ID="ltrPaymentMethod" runat="server"></asp:Literal>
                                                    <asp:CustomValidator ID="cvPaymentMethod" runat="server" Font-Bold="True" Font-Size="Medium"
                                                        ForeColor="#FF8000" ClientValidationFunction="validatePaymentMethod" Text="!"
                                                        EnableClientScript="true" ErrorMessage="Select your payment method." Width="1px"></asp:CustomValidator>
                                                </fieldset>
                                                <div id="pnlChecks">
                                                    <div class="yt-Form-Field">
                                                        <label>
                                                            <em class="required">* </em>Card Number:</label>
                                                        <asp:TextBox ID="txtCCNumber" CssClass="yt-Form-Input-Long" runat="server" MaxLength="16"
                                                            Width="280px"></asp:TextBox>
                                                        <asp:CustomValidator ID="cvCreditCardNumber" runat="server" Font-Bold="True" Font-Size="Medium"
                                                            ForeColor="#FF8000" ClientValidationFunction="validateCreditCardLength" Text="!"
                                                            EnableClientScript="true" ErrorMessage="Please enter a valid Credit Card Number."
                                                            Width="1px"></asp:CustomValidator>
                                                        <asp:RequiredFieldValidator ID="rfvCCNumber" Font-Bold="True" Font-Size="Medium"
                                                            ForeColor="#FF8000" ControlToValidate="txtCCNumber" runat="server" ErrorMessage="Credit Card Number is a required field.">!</asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="yt-Form-Field">
                                                        <label>
                                                            <em class="required">* </em>Card verification:</label>
                                                        <asp:TextBox ID="txtCCVerification" CssClass="yt-Form-Input-Short" runat="server"
                                                            MaxLength="4" TextMode="Password"></asp:TextBox>
                                                        <asp:CustomValidator ID="cvCreditCardVerification" runat="server" Font-Bold="True"
                                                            Font-Size="Medium" ForeColor="#FF8000" ClientValidationFunction="validateCreditCardVerificationLength"
                                                            Text="!" EnableClientScript="true" ErrorMessage="Please enter a valid Credit Card Verification Number."
                                                            Width="1px"></asp:CustomValidator>
                                                        <%--   <asp:RegularExpressionValidator ID="revCCVerification" runat="server" ControlToValidate="txtCCVerification"
                                                ErrorMessage="Please enter a valid card verification code (CVC)." Font-Bold="True"
                                                Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="\d{4}"
                                                Width="1px">
                                                </asp:RegularExpressionValidator>--%>
                                                        <asp:RequiredFieldValidator ControlToValidate="txtCCVerification" Font-Bold="True"
                                                            Font-Size="Medium" ForeColor="#FF8000" ID="rfvCCVerification" Text="!" runat="server"
                                                            ErrorMessage="Card Verification Code (CVC) is a required field." Width="1px"></asp:RequiredFieldValidator>
                                                        <div class="hint">
                                                            The CVC is located on the back of MasterCard, Visa and Discover credit cards and
                                                            is a separate group of 3 digits to the right of the signature strip. On American
                                                            Express cards, the CVC is a separate group of 4 digits on the front right of the
                                                            card.<span class="hintPointer"></span></div>
                                                    </div>
                                                    <!-- BEGIN DigiCert Site Seal Code -->
                                                    <div id="digicertsitesealcode" style="width: 81px; margin: 5px auto 5px 5px; text-align:center" >

                                                        <script language="javascript" type="text/javascript" src="https://www.digicert.com/custsupport/sealtable.php?order_id=00192234&amp;seal_type=a&amp;seal_size=large&amp;seal_color=blue&amp;new=1"></script>

                                                        <a href="http://www.digicert.com">SSL Certificates</a>

                                                        <script language="javascript" type="text/javascript">                                                                                   coderz();</script>

                                                    </div>
                                                    <!-- END DigiCert Site Seal Code -->
                                                    <%--<!-- BEGIN DigiCert Site Seal Code -->
                                                <div id="digicertsitesealcode" style="" align="center">

                                                    <script language="javascript" type="text/javascript" src="https://www.digicert.com/custsupport/sealtable.php?order_id=10000153&amp;seal_type=a&amp;seal_size=large&amp;seal_color=green&amp;new=1"></script>

                                                    <a href="http://www.digicert.com/ssl.htm">SSL Certificate</a>

                                                    <script language="javascript" type="text/javascript">                                                        coderz();</script>

                                                </div>
                                                <!-- END DigiCert Site Seal Code -->--%>
                                                    <div id="accredited_check">
                                                        <a href="https://www.bbb.org/online/consumer/cks.aspx?id=10906081881" target="_blank">
                                                            <img id="imgBBB" runat="server" alt="BBB Online" src="~/assets/images/bbb.gif" />
                                                        </a>
                                                    </div>
                                                    <!-- End -->
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
                                                            ForeColor="#FF8000" ClientValidationFunction="validateExpMonth" Text="!" ErrorMessage="Expiry Date is a required field."
                                                            Width="1px"></asp:CustomValidator><asp:CompareValidator ID="cpvtxtCCYear" Font-Bold="True"
                                                                Operator="GreaterThanEqual" Font-Size="Medium" ForeColor="#FF8000" runat="server"
                                                                ErrorMessage="Expiry Date cannot be less than current date." ControlToValidate="txtCCYear"
                                                                Visible="false" Width="1px">!</asp:CompareValidator><asp:CustomValidator ID="cvCCYear"
                                                                    Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" runat="server" ControlToValidate="txtCCYear"
                                                                    Text="!" ErrorMessage="Please enter a valid expiry date." Width="1px"></asp:CustomValidator>
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
                                                        Text="!" runat="server" ErrorMessage="Name on Card is a required field." ControlToValidate="txtCCName"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="revNameCard" ControlToValidate="txtCCName" Text="!"
                                                        runat="server" ErrorMessage="Name on Card can only contain letters,numbers,'-' and '#'"
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
                                                        Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9,\#,\-,\s]*$"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                                            ID="rfvCCBillingAddress" runat="server" Text="!" Font-Bold="True" Font-Size="Medium"
                                                            ForeColor="#FF8000" ControlToValidate="txtCCBillingAddress" ErrorMessage="Billing Address is a required field."></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="yt-Form-Field">
                                                    <asp:TextBox ID="txtCCBillingAddress2" CssClass="yt-Form-Input-Long" runat="server"
                                                        MaxLength="50" Width="280px"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtCCBillingAddress2"
                                                        ErrorMessage="Billing Address (line 2)  can only contain letters,numbers,'-' and '#'"
                                                        Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9,\#,\-,\s]*$"></asp:RegularExpressionValidator>
                                                </div>
                                                <div>
                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <div class="yt-Form-Field">
                                                                <label>
                                                                    <em class="required">* </em>Country:</label>
                                                                <asp:DropDownList ID="ddlCCCountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCCCountry_SelectedIndexChanged"
                                                                    Width="285px">
                                                                </asp:DropDownList>
                                                                <br />
                                                            </div>
                                                            <div class="yt-Form-Field">
                                                                <label>
                                                                    State/Province:</label>
                                                                <asp:DropDownList ID="ddlCCStateProvince" runat="server" Width="285px">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                                <div class="yt-Form-Field">
                                                    <label>
                                                        <em class="required">* </em>City:</label>
                                                    <asp:TextBox ID="txtCCCity" CssClass="yt-Form-Input-Long" runat="server" Width="280px"
                                                        MaxLength="50"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="revtxtCCCity" runat="server" ControlToValidate="txtCCCity"
                                                        ErrorMessage="City only contain letters,numbers,'-' and '#'" Font-Bold="True"
                                                        Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9,\#,\-,\s]*$"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                                            ID="rfvCCCity" runat="server" Text="!" ControlToValidate="txtCCCity" ErrorMessage="City is a required field."
                                                            Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="yt-Form-Field">
                                                    <label>
                                                        <em class="required">* </em>Zip Code/Postal Code:</label>
                                                    <asp:TextBox ID="txtCCZipCode" CssClass="yt-Form-Input" runat="server" MaxLength="10"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvCCZipCode" runat="server" Text="!" ControlToValidate="txtCCZipCode"
                                                        ErrorMessage="Zip Code/Postal Code is a required field." Font-Bold="True" Font-Size="Medium"
                                                        ForeColor="#FF8000"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="revZipCode" runat="server" ControlToValidate="txtCCZipCode"
                                                        ErrorMessage="Zip Code/Postal Code can only contain letters and numbers" Font-Bold="True"
                                                        Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9\s]*$"></asp:RegularExpressionValidator>
                                                </div>
                                                <div class="yt-Form-Field">
                                                    <label>
                                                        <em class="required">* </em>Phone Number:</label>
                                                    (<asp:TextBox ID="txtPhoneNumber1" runat="server" Width="34px" MaxLength="3" CssClass="yt-Form-Input-Long"></asp:TextBox>)
                                                    <asp:TextBox ID="txtPhoneNumber2" runat="server" Width="34px" MaxLength="3" CssClass="yt-Form-Input-Long"></asp:TextBox>
                                                    -
                                                    <asp:TextBox ID="txtPhoneNumber3" runat="server" Width="40px" MaxLength="4" CssClass="yt-Form-Input-Long"></asp:TextBox>
                                                    <asp:CustomValidator ID="cvPhoneNumber" runat="server" Font-Bold="True" Font-Size="Medium"
                                                        ForeColor="#FF8000" ErrorMessage="Enter valid phone number.Format should be XXX-XXX-XXXX."
                                                        ClientValidationFunction="PhoneNumbervalidation">!</asp:CustomValidator>
                                                </div>
                                                <div class="yt-Form-Field">
                                                    <label>
                                                        <em class="required">* </em>Email Address:</label>
                                                    <asp:TextBox ID="txtEmailAddress" CssClass="yt-Form-Input-Long" runat="server" MaxLength="100"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvEmailAddress" runat="server" Text="!" ControlToValidate="txtEmailAddress"
                                                        ErrorMessage="Email address is a required field." Font-Bold="True" Font-Size="Medium"
                                                        ForeColor="#FF8000">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                                <div id="DivRenew" runat="server" visible ="false">
                                                    <% if (_userName != null)
                                                       { %>
                                                    <span id="Renewal" runat="server"><em>* </em>Renewal Options</span>
                                                    <div class="yt-Form-Field yt-Form-Field-Radio">
                                                        <input id="ctl00_TributePlaceHolder_rdoNotifyBeforeRenew" checked="checked" name="ctl00$TributePlaceHolder$rdoRenew"
                                                            onclick="MakeAutoRenew();" type="radio" value="rdoNotifyBeforeRenew" />
                                                        <label for="ctl00_TributePlaceHolder_rdoNotifyBeforeRenew">
                                                            I do not want this tribute to be renewed automatically on a yearly basis, but I
                                                            will be notified when the account is near to expiry.</label>
                                                    </div>
                                                    <div class="yt-Form-Field yt-Form-Field-Radio">
                                                        <input id="rdoAutoRenew" name="ctl00$TributePlaceHolder$rdoRenew" onclick="MakeAutoRenew();"
                                                            type="radio" value="rdoYearlyAutoRenew" runat="server" />
                                                        <label for="ctl00_TributePlaceHolder_rdoYearlyAutoRenew">
                                                            I want this tribute to be renewed automatically on a yearly basis.</label>
                                                    </div>
                                                    <br />
                                                    <% } %>
                                                </div>
                                                <% if (_userName != null)
                                                   { %>
                                                <div class="yt-Form-Field yt-Form-Field-Checkbox">
                                                    <asp:CheckBox ID="chkSaveBillingInfo" Enabled="true" Text="I would like to save the above billing information in my profile."
                                                        runat="server" />
                                                </div>
                                                <% } %>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="yt-InfoBox" id="yt-PaymentTotal">
                                        You will be charged: <span id="BillingTotal" runat="server"></span>
                                    </div>
                                    <div class="yt-Form-Field yt-Form-Field-Checkbox">
                                        <input type="checkbox" name="chkAgree" id="chkAgree" runat="server" value="AgreeToTerms" />
                                        <label for="chkAgree">
                                            <em class="required">* </em>I have read and agree to the <a href='termsofuse.aspx'
                                                target='_blank'>terms of use</a>, the cancellation/refund policy (outlined in
                                            the terms of use) and the <a href='privacy.aspx' target='_blank'>privacy policy</a>.</label><br />
                                    </div>
                                    <br />
                                    <p>
                                        If you have reviewed all of the above information and it is correct, you must be
                                        ready to...
                                        <asp:CustomValidator ID="cvAcceptPolicies" Text="!" ForeColor="White" ErrorMessage="Please accept that you have read and agree to the terms of use, cancellation/refund policy and the privacy policy."
                                            runat="server" ClientValidationFunction="ValidateTandCs" Width="1px"></asp:CustomValidator>
                                    </p>
                                    <div class="yt-Form-Buttons">
                                        <div class="yt-Form-Submit">
                                            <asp:LinkButton ID="lbtnPay" CssClass="yt-Button yt-ArrowButton" runat="server" OnClick="lbtnPay_Click"
                                                OnClientClick="OnPayClick()">Upgrade this tribute!</asp:LinkButton>
                                            <asp:Label ID="lblProcess" runat="server"></asp:Label>
                                        </div>
                                        <asp:LinkButton ID="btnCancel" runat="server" CausesValidation="false" Text="Cancel Payment"
                                            OnClick="btnCancel_Click" />
                                        <span style="color: #0000ff"></span>
                                    </div>
                                    <!-- end yt-Form-Buttons-->
                                    <%--  </ContentTemplate>--%>
                                    <%--</asp:UpdatePanel>--%>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                    <!--yt-ContentPrimary-->
                </div>
                <!--yt-ContentPrimaryContainer-->
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
            <uc:Footer ID="Footer1" runat="server" />
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
      <% if (ConfigurationManager.AppSettings["ApiKey"] != null) { %>  
        function update_user_is_connected() {
            header_user_is_connected();
            FB.XFBML.parse();
        }
        function update_user_is_not_connected() {
            header_user_is_not_connected();
            FB.XFBML.parse();
        }  
        window.fbAsyncInit = function() {
    FB.init({
        appId  : '<%= ConfigurationManager.AppSettings["ApiKey"].ToString() %>',
        status : true, // check login status
        cookie : true, // enable cookies to allow the server to access the session
        xfbml  : true,  // parse XFBML
        //channelUrl  : 'http://www.yourdomain.com/channel.html', // Custom Channel URL
        oauth : true //enables OAuth 2.0
    });

    FB.getLoginStatus(function(response) {
        if (response.authResponse) update_user_is_connected();
        else update_user_is_not_connected();
    });

    // This will be triggered as soon as the user logs into Facebook (through your site)
    FB.Event.subscribe('auth.login', function(response) {
        update_user_is_connected();
    });
};

//   window.addEvent('domready', function(){     
//     FB.init('<%= ConfigurationManager.AppSettings["ApiKey"].ToString() %>', 
//             "/xd_receiver_ssl.htm",
//             {"ifUserConnected": update_user_is_connected,
//              "ifUserNotConnected": update_user_is_not_connected,
//              "doNotUseCachedConnectState":"true"});
//   });                        
      <% } %>   
    </script>
    
    <!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
    <script type="text/javascript" src="<%=Session["APP_SCRIPT_PATH"]%>assets/scripts/BrowserOrTabCloseHandler.js"></script>

</body>

<script type="text/javascript" src="<%=Session["APP_SCRIPT_PATH"]%>assets/scripts/modalbox.js"></script>

</html>
