<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TributeSponsor.aspx.cs" EnableEventValidation="false"
    Inherits="Tribute_TributeSponsor" Title="TributeSponsor" ValidateRequest="false" %>

<%@ Register Src="../UserControl/YourTributeHeader.ascx" TagName="Header" TagPrefix="uc" %>
<%@ Register Src="../UserControl/FooterHome.ascx" TagName="Footer" TagPrefix="uc" %>
<%--<%@ Register Src="../UserControl/Footer.ascx" TagName="Footer" TagPrefix="uc" %>
--%><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:fb="http://www.facebook.com/2008/fbml"
xml:lang="en" lang="en">
<head runat="server">
    <title id="sponsorTitle" runat="server">
        <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
        Sponsor</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Content-Language" content="en-ca" />
    <meta http-equiv="imagetoolbar" content="false" />
    <meta name="robots" content="index,follow" />
    <meta name="MSSmartTagsPreventParsing" content="true" />
    <!-- really basic, generic html class stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/default.css" />
    <!-- default grid layout stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/layouts/centered1024_1.css" />
    <!-- print-friendly stylesheet -->
    <link rel="stylesheet" type="text/css" media="print" href="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/print.css" />
    <!-- screen elements stylesheet should be here -->
    <link rel="stylesheet" type="text/css" href="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/screen.css" />
    <!-- create process specific stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/tributecreation.css" />
    <!-- larger text stylesheets -->
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/medium_text.css"
        title="medium_text" />
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/large_text.css"
        title="large_text" />
    <!-- Website Favorite Icon -->
    <link rel="Shortcut Icon" type="image/x-icon" href="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/favicon.ico" />
    <!-- JS libraries -->

    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/scripts/mootools-1.11.js"></script>

    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/scripts/global.js"></script>

    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/scripts/styleSwitcher.js"></script>

    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/scripts/tributecreation.js"></script>

    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>Common/JavaScript/Common.js"></script>

    <script type="text/javascript" language="javascript" src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>Common/JavaScript/TributeHomePage.js"> </script>

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
        
        var rdb2 = document.getElementById('<%= rdoPhotoMembershipLifeTime.ClientID %>');
        var rdb3 = document.getElementById('<%= rdoTributeMembershipYearly.ClientID %>');
        var rdb4 = document.getElementById('<%= rdoTributeMembershipLifeTime.ClientID %>');
        if (rdb2 != null) {
            if ( (!rdb2.checked) && (!rdb3.checked) && (!rdb4.checked))
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
        if ((!rdb1.checked) && (!rdb4.checked))
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
            return true;
            }
            else
            {
            return false;
            }
        }
        

    function ValidateAddress(source, args) {
        var addrs1 = document.getElementById('<%= rdbAvailableAddress1.ClientID %>');
        var addrs2 = document.getElementById('<%= rdbAvailableAddress2.ClientID %>');
        var addrs3 = document.getElementById('<%= rdbAvailableAddress3.ClientID %>');
        var addrs4 = document.getElementById('<%= txtTributeAddressOther.ClientID %>');
        args.IsValid = AddressValidate(addrs1, addrs2, addrs3, addrs4);
    }
    function OpenSSLWindow() {
        window.open('http://www.digicert.com/custsupport/sspopup.php?order_id=00246067', "Samplewindow", "status=0, width=500, height=550,left=400,top=250");
    }
    
    function validateExpYear(source, args) {
        var bol = true;
        var month = document.getElementById('<%=ddlCCMonth.ClientID%>');
        var year = document.getElementById('<%=txtCCYear.ClientID%>');
        var validat = document.getElementById('<%=CustomValidator3.ClientID%>');
        var currYear = Number(<%=todayYear %>);
        var currMonth = Number(<%=todayMonth %>);
        if(!(month == "--"))
        {
            if ((((Number(month.value)) >= currMonth)||((Number(year.value)) >= currYear)))
            {
            args.IsValid = true;
            }
            else
            {
            args.IsValid = false;
            }
        }
    };
    
    function NumberOnly(){
var t=window.event.keyCode;
 
if(t>=48 & t<=57)
return;
window.event.keyCode=0;
} 



function OnPayClick() {
        if (Page_ClientValidate()) {
            document.getElementById("lblProcess").innerHTML = "Please wait";
            document.getElementById("lbtnPay").style.display = "none";
            return true;
            }
            else
            {
            return false;
            }
        }




-->
</script>

<body>
    <form id="Form1" action="" runat="server">
    <asp:HiddenField ID="hfPaymentMethod" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
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
                                ForeColor="Black" runat="server" Width="652px" ValidationGroup="sponser" />
                                                       
                            <div class="yt-Panel-Primary">
                                <h2>
                                    Sponsor This Memorial:</h2>
                                <fieldset class="yt-Form">
                                    <div id="chooseoption" runat="server">
                                        <p>
                                            <h4>
                                                <b>1) Choose one of the following options to upgrade this account:</b></h4>
                                        </p>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table cellspacing="0" cellpadding="0" class="alignTable">
                                                <tr>
                                                    <td class="brownHeading" style="text-align: left; padding: 0px 0px 0px 18px;">
                                                        Features
                                                    </td>
                                                    <td class="brownHeading" id="midColumn1" runat="server">
                                                        <%if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments") %>
                                                        <%{ %>
                                                        Photo Announcement
                                                        <%}
                                                          else
                                                          { %>
                                                        PREMIUM OBITUARY
                                                        <%} %>
                                                    </td>
                                                    <td class="brownHeading">
                                                        <%if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower() == "yourmoments") %>
                                                        <%{ %>
                                                        WEBSITE
                                                        <%}
                                                          else
                                                          { %>
                                                        MEMORIAL TRIBUTE
                                                        <%} %>
                                                    </td>
                                                </tr>
                                                <tr class="topBorderOnly">
                                                    <td class="leftOfTableRow">
                                                        Designer Theme
                                                    </td>
                                                    <td class="rightOfTableRow" id="midColumn2" runat="server">
                                                        <img alt="yes" src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/greenTick.jpg" />
                                                    </td>
                                                    <td class="rightOfTableRow">
                                                        <img alt="yes" src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/greenTick.jpg" />
                                                    </td>
                                                </tr>
                                                <tr style="height: 10px;">
                                                    <td class="leftOfTableRow">
                                                        Story (Obituary, Photo, Biography, etc)
                                                    </td>
                                                    <td class="rightOfTableRow" id="midColumn3" runat="server">
                                                        <img alt="yes" src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/greenTick.jpg" />
                                                    </td>
                                                    <td class="rightOfTableRow">
                                                        <img alt="yes" src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/greenTick.jpg" />
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="leftOfTableRow">
                                                        Guestbook (Unlimited Messages)
                                                    </td>
                                                    <td class="rightOfTableRow" id="midColumn4" runat="server">
                                                        <img alt="yes" src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/greenTick.jpg" />
                                                    </td>
                                                    <td class="rightOfTableRow">
                                                        <img alt="yes" src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/greenTick.jpg" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="leftOfTableRow">
                                                        Free Virtual Gifts
                                                    </td>
                                                    <td class="rightOfTableRow" id="midColumn5" runat="server">
                                                        <img alt="yes" src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/greenTick.jpg" />
                                                    </td>
                                                    <td class="rightOfTableRow">
                                                        <img alt="yes" src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/greenTick.jpg" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="leftOfTableRow">
                                                        Social Sharing Tools
                                                    </td>
                                                    <td class="rightOfTableRow" id="midColumn6" runat="server">
                                                        <img alt="yes" src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/greenTick.jpg" />
                                                    </td>
                                                    <td class="rightOfTableRow">
                                                        <img alt="yes" src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/greenTick.jpg" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="leftOfTableRow">
                                                        Events (Online Invitations & RSVP)
                                                    </td>
                                                    <td class="rightOfTableRow" id="midColumn7" runat="server">
                                                        <img alt="yes" src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/greenTick.jpg" />
                                                    </td>
                                                    <td class="rightOfTableRow">
                                                        <img alt="yes" src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/greenTick.jpg" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="leftOfTableRow">
                                                        Photo Albums
                                                    </td>
                                                    <td class="rightOfTableRow" id="midColumn8" runat="server">
                                                        (2 Albums)
                                                    </td>
                                                    <td class="rightOfTableRow">
                                                        <img alt="yes" src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/greenTick.jpg" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="leftOfTableRow">
                                                        Videos
                                                    </td>
                                                    <td class="rightOfTableRow" id="midColumn9" runat="server">
                                                        <img alt="yes" src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/greenTick.jpg" />
                                                    </td>
                                                    <td class="rightOfTableRow">
                                                        <img alt="yes" src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/greenTick.jpg" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="leftOfTableRow">
                                                        Notes/Pages
                                                    </td>
                                                    <td class="rightOfTableRow" id="midColumn10" runat="server">
                                                        <img alt="yes" src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/greenTick.jpg" />
                                                    </td>
                                                    <td class="rightOfTableRow">
                                                        <img alt="yes" src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/greenTick.jpg" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="leftOfTableRow">
                                                        View/Download High Resolution Photos
                                                    </td>
                                                    <td class="rightOfTableRow" id="midColumn11" runat="server">
                                                        -
                                                    </td>
                                                    <td class="rightOfTableRow">
                                                        <img alt="yes" src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/greenTick.jpg" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="leftOfTableRow">
                                                        Unlimited Storage
                                                    </td>
                                                    <td class="rightOfTableRow" id="midColumn12" runat="server">
                                                        -
                                                    </td>
                                                    <td class="rightOfTableRow">
                                                        <img alt="yes" src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/greenTick.jpg" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="leftOfTableRow">
                                                        Custom URL
                                                    </td>
                                                    <td class="rightOfTableRow" id="midColumn13" runat="server">
                                                        -
                                                    </td>
                                                    <td class="rightOfTableRow">
                                                        <img alt="yes" src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/greenTick.jpg" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="leftOfTableRow">
                                                        No Advertising
                                                    </td>
                                                    <td class="rightOfTableRow" id="midColumn14" runat="server">
                                                        <img alt="yes" src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/greenTick.jpg" />
                                                    </td>
                                                    <td class="rightOfTableRow">
                                                        <img alt="yes" src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/greenTick.jpg" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="leftOfTableRow" style="color: #494949; font-size: 15px;">
                                                        PRICE (1 YEAR)
                                                    </td>
                                                    <td class="rightOfTableRowLast" id="secondLastRow" runat="server">
                                                        <asp:RadioButton ID="rdoPhotoMembershipYearly" GroupName="rdoMembershipType" Text="" Visible="false"
                                                            runat="server" AutoPostBack="True" OnCheckedChanged="rdoPhotoMembershipYearly_CheckedChanged" />

                                                    </td>
                                                    <td class="rightOfTableRowLast" style="padding-left: 7px;" id="secondLastRow">
                                                        <asp:RadioButton ID="rdoTributeMembershipYearly" GroupName="rdoMembershipType" Text=""
                                                            runat="server" AutoPostBack="True" OnCheckedChanged="rdoTributeMembershipYearly_CheckedChanged" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="leftOfTableRow Font15">
                                                        <h4>
                                                            <strong>
                                                                <div class="pricingMargin">
                                                                    PRICE (LIFETIME)</div>
                                                            </strong>
                                                    </td>
                                                    <td class="rightOfTableRowLast" id="midColumn16" runat="server">
                                                        <h4>
                                                            <strong>
                                                                <div class="priceMargin">
                                                                    <asp:RadioButton ID="rdoPhotoMembershipLifeTime" GroupName="rdoMembershipType" Text=""
                                                                        runat="server" AutoPostBack="True" OnCheckedChanged="rdoPhotoMembershipLifeTime_CheckedChanged" />
                                                                </div>
                                                            </strong>
                                                            <h4>
                                                            </h4>
                                                            <h4>
                                                            </h4>
                                                        </h4>
                                                    </td>
                                                    <td class="rightOfTableRowLast" style="padding-left: 7px;">
                                                        <h4>
                                                            <strong>
                                                                <div class="priceMargin">
                                                                    <asp:RadioButton ID="rdoTributeMembershipLifeTime" GroupName="rdoMembershipType"
                                                                        Text="" runat="server" AutoPostBack="True" OnCheckedChanged="rdoTributeMembershipLifeTime_CheckedChanged" />
                                                                </div>
                                                            </strong>
                                                            <h4>
                                                            </h4>
                                                            <h4>
                                                            </h4>
                                                        </h4>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <div class="yt-Form-Field">
                                                <label for="txtCouponCode">
                                                    Enter your coupon code, if you have one:</label>
                                                <asp:TextBox ID="txtCouponCode" runat="server" CssClass="yt-Form-Input-Long" MaxLength="18"></asp:TextBox>
                                                <asp:LinkButton ID="lbtnValidateCoupon" OnClick="lbtnValidateCoupon_Click" runat="server"
                                                    CausesValidation="False" CssClass="yt-checkCoupon">Validate Coupon</asp:LinkButton>
                                                <span id="spanCoupon" class="availabilityNotice" runat="server"></span>
                                                <div class="hint">
                                                    If you have a coupon code enter it here and click &quot;Validate Coupon&quot;. If
                                                    the coupon code is correct, the discount will be applied to your total at the bottom
                                                    of the page<span class="hintPointer"></span></div>
                                            </div>
                                            <div class="yt-InfoBox" id="yt-PaymentTotalAbove">
                                                You will be charged: <span id="BillingTotalAbove" runat="server"></span>
                                            </div>
                                            <p>
                                                <h4>
                                                    <b>2) Provide the following details: </b>
                                                </h4>
                                                <p>
                                                </p>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <fieldset id="BecomeFieldsAdmin" runat="server" class="yt-Inform" style="float: none">
                                                            <div id="sponsPageLongDivText">
                                                                a. This
                                                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                                                                does not have an administrator, would you like to become an administrator so that
                                                                you can add/remove/modify the
                                                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                                                content?</div>
                                                            <br />
                                                            <div>
                                                                <asp:RadioButton ID="rdoAdminYes" runat="server" AutoPostBack="true" GroupName="BeAdmin"
                                                                    OnCheckedChanged="rdoAdminYes_CheckedChanged" Text="Yes" />
                                                                <asp:RadioButton ID="rdoAdminNo" runat="server" AutoPostBack="true" GroupName="BeAdmin"
                                                                    OnCheckedChanged="rdoAdminNo_CheckedChanged" Text="No" />
                                                            </div>
                                                        </fieldset>
                                                        <fieldset id="haveAccountFields" runat="server" class="yt-Inform">
                                                            <legend>Do you currently have an account on Your
                                                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>?</legend>
                                                            <div>
                                                                <asp:RadioButton ID="rdoHaveAccountYes" runat="server" AutoPostBack="true" GroupName="haveAccount"
                                                                    OnCheckedChanged="rdoHaveAccountYes_CheckedChanged" Text="Yes" />
                                                                <asp:RadioButton ID="rdoHaveAccountNo" runat="server" AutoPostBack="true" GroupName="haveAccount"
                                                                    OnCheckedChanged="rdoHaveAccountNo_CheckedChanged" Text="No" />
                                                            </div>
                                                        </fieldset>
                                                        <fieldset id="loginInfoFields" runat="server" class="yt-Inform">
                                                            <legend>Please enter the email and password you use to log in to Your
                                                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>:</legend>
                                                            <div class="yt-Form-Field">
                                                                Email Address:<asp:TextBox ID="txtEmail" runat="server" CssClass="yt-Form-Input-Long"></asp:TextBox>
                                                                &nbsp; &nbsp; &nbsp;Password:<asp:TextBox ID="txtPassword" runat="server" CssClass="yt-Form-Input"
                                                                    TextMode="Password"></asp:TextBox>
                                                            </div>
                                                        </fieldset>
                                                        <fieldset id="SignUpFields" runat="server" class="yt-Inform">
                                                            <legend>Please enter the email and password you use to log in to Your
                                                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>:</legend>
                                                            <div class="yt-Form-Field">
                                                                Email Address:<asp:TextBox ID="txtEmailSignUp" runat="server" CssClass="yt-Form-Input-Long"></asp:TextBox>
                                                                <br />
                                                                <br />
                                                                Password:&nbsp;&nbsp; &nbsp;<asp:TextBox ID="txtPasswordSignUp" runat="server" CssClass="yt-Form-Input"
                                                                    TextMode="Password"></asp:TextBox>
                                                                &nbsp; &nbsp; &nbsp; &nbsp; Confirm Password:&nbsp;<asp:TextBox ID="txtConfrmPassword"
                                                                    runat="server" CssClass="yt-Form-Input" TextMode="Password"></asp:TextBox>
                                                                <br />
                                                                <br />
                                                                First Name:&nbsp;&nbsp;<asp:TextBox ID="txtFirstName" runat="server" CssClass="yt-Form-Input"></asp:TextBox>
                                                                &nbsp;&nbsp; &nbsp; &nbsp; &nbsp;Last Name: &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:TextBox
                                                                    ID="txtLastName" runat="server" CssClass="yt-Form-Input"></asp:TextBox>
                                                                <br />
                                                                <br />
                                                                Country:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlCountry" runat="server"
                                                                    AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"
                                                                    Width="285px">
                                                                </asp:DropDownList>
                                                                <br />
                                                                <br />
                                                                State/Prov: &nbsp;<asp:DropDownList ID="ddlState" runat="server" Width="285px">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </fieldset>
                                                        <fieldset id="asGiftOptions" runat="server" class="yt-Inform">
                                                            <legend>a. Are you upgrading this
                                                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                                                as a gift to someone else?</legend>
                                                            <div>
                                                                <asp:RadioButton ID="rdoAsGiftYes" runat="server" AutoPostBack="true" GroupName="giftFields"
                                                                    OnCheckedChanged="rdoAsGiftYes_CheckedChanged" Text="Yes" />
                                                                <asp:RadioButton ID="rdoAsGiftNo" runat="server" AutoPostBack="true" GroupName="giftFields"
                                                                    OnCheckedChanged="rdoAsGiftNo_CheckedChanged" Text="No" />
                                                            </div>
                                                            <br />
                                                        </fieldset>
                                                        <fieldset id="knowIdentityFields" runat="server" class="yt-Inform">
                                                            <legend>Would you like the
                                                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                                                creator to know who you are?</legend>
                                                            <div>
                                                                <asp:RadioButton ID="rdoKnowYouYes" runat="server" AutoPostBack="true" GroupName="identityFields"
                                                                    OnCheckedChanged="rdoKnowYouYes_CheckedChanged" Text="Yes" />
                                                                <asp:RadioButton ID="rdoKnowYouNo" runat="server" AutoPostBack="true" GroupName="identityFields"
                                                                    OnCheckedChanged="rdoKnowYouNo_CheckedChanged" Text="No" />
                                                            </div>
                                                            <br />
                                                        </fieldset>
                                                        <fieldset id="NameMsgFields" runat="server" class="yt-Inform">
                                                            <legend>Enter the following information for us to email the
                                                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                                                creator:</legend>
                                                            <div class="yt-Form-Field">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            Name:
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtNameForGift" runat="server" CssClass="yt-Form-Input-Long"></asp:TextBox>
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNameForGift"
                                                                                Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ValidationGroup="vgCheckAvailability">!</asp:RequiredFieldValidator>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="vertical-align: top;">
                                                                            Message:<br />
                                                                            (optional):
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtMessage" runat="server" Columns="85" CssClass="yt-Form-Input"
                                                                                Rows="8" TextMode="MultiLine"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </fieldset>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                    <ContentTemplate>
                                                        <div id="PanelChangeAddress" runat="server" style="width: 622px">
                                                            <br />
                                                            <%--rajat--%>
                                                            <div>
                                                                b. Do you want to change the URL for this tribute?</div>
                                                            <br />
                                                            <div>
                                                                <asp:RadioButton ID="rdoChangeAddressYes" GroupName="rdoChangeAddress" runat="server"
                                                                    AutoPostBack="True" OnCheckedChanged="rdoChangeAddressYes_CheckedChanged" Text="Yes" />
                                                                <asp:RadioButton ID="rdoChangeAddressNo" GroupName="rdoChangeAddress" runat="server"
                                                                    AutoPostBack="True" OnCheckedChanged="rdoChangeAddressNo_CheckedChanged" Text="No" />
                                                            </div>
                                                        </div>
                                                        <div id="PanelTributeAddress" runat="server" class="yt-Form-Field yt-Hint-Offset yt-ThemeAddress"
                                                            style="width: 622px">
                                                            <br />
                                                            <label>
                                                                A
                                                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                                                requires a custom URL, what would you like the web address for this
                                                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                                                                to be:
                                                            </label>
                                                            http://<%=_tributeType%>.<%=TributesPortal.Utilities.WebConfig.TopLevelDomain%>/
                                                            <asp:TextBox ID="txtTributeAddress" runat="server" CssClass="yt-Form-Input" MaxLength="100"
                                                                TabIndex="5"></asp:TextBox>
                                                            <div class="hint">
                                                                Choose a web address that you like and is based on who you are creating the
                                                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                                                for. For example: &quot;janemichaelsmith&quot;. Please use only letters, numbers
                                                                and underscores (URLs with other characters will not be accepted). Note, you will
                                                                not be able to change your web address once you have created it.<span class="hintPointer"></span></div>
                                                            <span id="errorAddress" runat="server" style="color: #FF8000; font-size: Medium;
                                                                font-weight: bold;" visible="false">!</span>
                                                            <asp:RegularExpressionValidator ID="revTributeAddress" runat="server" ControlToValidate="txtTributeAddress"
                                                                Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9_]*$"
                                                                ValidationGroup="vgCheckAvailability"></asp:RegularExpressionValidator>
                                                            <asp:RequiredFieldValidator ID="rfvTributeAddress" runat="server" ControlToValidate="txtTributeAddress"
                                                                Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ValidationGroup="vgCheckAvailability">!</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="revTributeAddressNext" runat="server" ControlToValidate="txtTributeAddress"
                                                                Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9_]*$"
                                                                ValidationGroup="vgNextStep"></asp:RegularExpressionValidator>
                                                            <asp:RequiredFieldValidator ID="rfvTributeAddressNext" runat="server" ControlToValidate="txtTributeAddress"
                                                                Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ValidationGroup="vgNextStep">!</asp:RequiredFieldValidator>
                                                            <asp:LinkButton ID="lbtncheckAddress" runat="server" CssClass="yt-checkAvailability"
                                                                Height="15px" OnClick="lbtncheckAddress_Click" TabIndex="6" ValidationGroup="vgCheckAvailability">Check Availability</asp:LinkButton>
                                                            <span id="SpanAvailable" runat="server" class="availabilityNotice"></span>
                                                            <br />
                                                            <br />
                                                        </div>
                                                        <fieldset id="pnlTributeAddressAvailable" runat="server" class="yt-TributeAddressSelection"
                                                            style="width: 622px" visible="false">
                                                            <asp:Label ID="lblOtherOptions" runat="server" Text="Other options that are available:"
                                                                Width="253px"></asp:Label>
                                                            <br />
                                                            <asp:RadioButton ID="rdbAvailableAddress1" runat="server" GroupName="AvailableAddress"
                                                                OnCheckedChanged="rdbAvailableAddress1_CheckedChanged" TabIndex="7" />
                                                            <br />
                                                            <asp:RadioButton ID="rdbAvailableAddress2" runat="server" GroupName="AvailableAddress"
                                                                OnCheckedChanged="rdbAvailableAddress2_CheckedChanged" TabIndex="8" />
                                                            <br />
                                                            <asp:RadioButton ID="rdbAvailableAddress3" runat="server" GroupName="AvailableAddress"
                                                                OnCheckedChanged="rdbAvailableAddress3_CheckedChanged" TabIndex="10" />
                                                            <div class="yt-Form-Field yt-Hint-Offset">
                                                                <asp:Label ID="lblTributeAddressOther" runat="server" Text="Or, enter another option:"
                                                                    Width="164px"></asp:Label>
                                                                <br />
                                                                http://<%=_tributeType%>.<%=TributesPortal.Utilities.WebConfig.TopLevelDomain%>/
                                                                <asp:TextBox ID="txtTributeAddressOther" runat="server" CssClass="yt-Form-Input"
                                                                    MaxLength="100" TabIndex="11"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="revTributeaddress2" runat="server" ControlToValidate="txtTributeAddressOther"
                                                                    Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9_]*$"
                                                                    ValidationGroup="vgCheckAvailability"></asp:RegularExpressionValidator>
                                                                <asp:CustomValidator ID="cvTributeAddressOther" runat="server" ClientValidationFunction="ValidateAddress"
                                                                    Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ValidationGroup="vgCheckAvailability"
                                                                    Width="1px">!</asp:CustomValidator>
                                                                <asp:RegularExpressionValidator ID="revTributeaddressOtherNext" runat="server" ControlToValidate="txtTributeAddressOther"
                                                                    Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9_]*$"
                                                                    ValidationGroup="vgNextStep"></asp:RegularExpressionValidator>
                                                                <asp:CustomValidator ID="cvTributeAddressOtherNext" runat="server" ClientValidationFunction="ValidateAddress"
                                                                    Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ValidationGroup="vgNextStep"
                                                                    Width="1px">!</asp:CustomValidator>
                                                                <asp:LinkButton ID="lbtncheckAvailability" runat="server" CssClass="yt-checkAvailability"
                                                                    Height="15px" OnClick="lbtncheckAvailability_Click" TabIndex="12" ValidationGroup="vgCheckAvailability">Check Availability</asp:LinkButton>
                                                                <span id="imgMsgStatus2" runat="server" class="availabilityNotice"></span>
                                                            </div>
                                                        </fieldset>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <asp:UpdatePanel ID="pnlCoupon" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div id="PnlPaymentDetails" runat="server" visible="false">
                                                            <p>
                                                                <h4>
                                                                    <b>3) Enter the following payment information:</b></h4>
                                                                <p>
                                                                </p>
                                                                <p class="yt-requiredFields">
                                                                    <strong>Required fields are indicated with <em class="required">* </em></s* </em="">
                                                                    </strong>
                                                                </p>
                                                                <fieldset class="yt-PaymentSelection yt-CompactRadioList">
                                                                    <legend><em class="required">* </em>Select your payment method:</legend>
                                                                    <asp:Literal ID="ltrPaymentMethod" runat="server"></asp:Literal>
                                                                    <asp:CustomValidator ID="cvPaymentMethod" runat="server" ClientValidationFunction="validatePaymentMethod"
                                                                        EnableClientScript="true" ErrorMessage="Select your payment method." Font-Bold="True"
                                                                        Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationGroup="sponser" Width="1px"></asp:CustomValidator>
                                                                </fieldset>
                                                                <div id="pnlChecks">
                                                                    <div class="yt-Form-Field">
                                                                        <label>
                                                                            <em class="required">* </em>Card Number:</label>
                                                                        <asp:TextBox ID="txtCCNumber" runat="server" CssClass="yt-Form-Input-Long" MaxLength="16"
                                                                            Width="280px"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvCCNumber" runat="server" ControlToValidate="txtCCNumber"
                                                                            ErrorMessage="Credit Card Number is a required field." Font-Bold="True" Font-Size="Medium"
                                                                            ForeColor="#FF8000" ValidationGroup="sponser">!</asp:RequiredFieldValidator>
                                                                        <asp:CustomValidator ID="cvCreditCardNumber" runat="server" ClientValidationFunction="validateCreditCardLength"
                                                                            EnableClientScript="true" ErrorMessage="Please enter a valid Credit Card Number."
                                                                            Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationGroup="sponser"
                                                                            Width="1px"></asp:CustomValidator>
                                                                    </div>
                                                                    <div class="yt-Form-Field">
                                                                        <label>
                                                                            <em class="required">* </em>Card verification:</label>
                                                                        <asp:TextBox ID="txtCCVerification" runat="server" CssClass="yt-Form-Input-Short"
                                                                            MaxLength="4" TextMode="Password"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvCCVerification" runat="server" ControlToValidate="txtCCVerification"
                                                                            ErrorMessage="Card Verification Code (CVC) is a required field." Font-Bold="True"
                                                                            Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationGroup="sponser" Width="1px"></asp:RequiredFieldValidator>   
                                                                        <asp:CustomValidator ID="cvCreditCardVerification" runat="server" ClientValidationFunction="validateCreditCardVerificationLength"
                                                                            EnableClientScript="true" ErrorMessage="Please enter a valid Credit Card Verification Number."
                                                                            Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationGroup="sponser"
                                                                            Width="1px"></asp:CustomValidator>
                                                                        
                                                                        <div class="hint">
                                                                            The CVC is located on the back of MasterCard, Visa and Discover credit cards and
                                                                            is a separate group of 3 digits to the right of the signature strip. On American
                                                                            Express cards, the CVC is a separate group of 4 digits on the front right of the
                                                                            card.<span class="hintPointer"></span></div>
                                                                    </div>
                                                                    <!-- BEGIN DigiCert Site Seal Code -->
                                                                    <div id="digicertsitesealcode">
                                                                        <a href="javascript:OpenSSLWindow();">
                                                                            <div class="SSLCertificate">
                                                                            </div>
                                                                        </a><a class="SSLCertificateText" href="javascript:OpenSSLWindow();">SSL Certificates</a>
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
                                                                        <asp:TextBox ID="txtCCYear" runat="server" CssClass="yt-Form-Input-Short" MaxLength="4"></asp:TextBox>
                                                                        <asp:Label ID="error1" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" Text="!" Visible="false"></asp:Label>
                                                                        <asp:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="validateExpMonth"
                                                                            ErrorMessage="Enter a valid Expiry Month." Font-Bold="True" Font-Size="Medium"
                                                                            ForeColor="#FF8000" Text="!" ValidationGroup="sponser"></asp:CustomValidator>
                                                                        <asp:CompareValidator ID="cpvtxtCCYear" runat="server" ControlToValidate="txtCCYear"
                                                                            ErrorMessage="Expiry Date cannot be less than current date." Font-Bold="True"
                                                                            Font-Size="Medium" ForeColor="#FF8000" Operator="GreaterThanEqual" ValidationGroup="sponser"
                                                                            Visible="false" Width="1px">!</asp:CompareValidator>
                                                                        <asp:CustomValidator ID="cvCCYear" runat="server" ControlToValidate="txtCCYear" ErrorMessage="Please enter a valid Expiry Year."
                                                                            Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationGroup="sponser" ClientValidationFunction="validateExpYear"
                                                                            Width="1px"></asp:CustomValidator>                                                                            
                                                                                                                                                
                                                                        <label>
                                                                            Year</label>
                                                                    </div>
                                                                </fieldset>
                                                                <div class="yt-Form-Field">
                                                                    <label>
                                                                        <em class="required">* </em>Name on Card:</label>
                                                                    <asp:TextBox ID="txtCCName" runat="server" CssClass="yt-Form-Input-Long" MaxLength="50"
                                                                        Width="280px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvCCName" runat="server" ControlToValidate="txtCCName"
                                                                        ErrorMessage="Name on Card is a required field." Font-Bold="True" Font-Size="Medium"
                                                                        ForeColor="#FF8000" Text="!" ValidationGroup="sponser"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="revNameCard" runat="server" ControlToValidate="txtCCName"
                                                                        ErrorMessage="Name on Card can only contain letters,numbers,'-' and '#'" Font-Bold="True"
                                                                        Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9,\#,\-,\s]*$"
                                                                        ValidationGroup="sponser"></asp:RegularExpressionValidator>
                                                                </div>
                                                                <div class="yt-Form-Field">
                                                                    <label>
                                                                        <em class="required">* </em>Billing Address:</label>
                                                                    <asp:TextBox ID="txtCCBillingAddress" runat="server" CssClass="yt-Form-Input-Long"
                                                                        MaxLength="50" Width="280px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvAddrs1" runat="server" ControlToValidate="txtCCBillingAddress"
                                                                        ErrorMessage="Address1 is a required field." Font-Bold="True" Font-Size="Medium"
                                                                        ForeColor="#FF8000" Text="!" ValidationGroup="sponser"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCCBillingAddress"
                                                                        ErrorMessage="Billing Address (line 1) can only contain letters,numbers,'-' and '#'"
                                                                        Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9,\#,\-,\s]*$"
                                                                        ValidationGroup="sponser"></asp:RegularExpressionValidator>                                                                   
                                                                </div>
                                                                <div class="yt-Form-Field">
                                                                    <asp:TextBox ID="txtCCBillingAddress2" runat="server" CssClass="yt-Form-Input-Long"
                                                                        MaxLength="50" Width="280px"></asp:TextBox>                                                                    
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtCCBillingAddress2"
                                                                        ErrorMessage="Billing Address (line 2)  can only contain letters,numbers,'-' and '#'"
                                                                        Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9,\#,\-,\s]*$"
                                                                        ValidationGroup="sponser"></asp:RegularExpressionValidator>
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
                                                                    <asp:TextBox ID="txtCCCity" runat="server" CssClass="yt-Form-Input-Long" MaxLength="50"
                                                                        Width="280px"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="txtCCCity"
                                                                        ErrorMessage="City is a required field." Font-Bold="True" Font-Size="Medium"
                                                                        ForeColor="#FF8000" Text="!" ValidationGroup="sponser"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="revtxtCCCity" runat="server" ControlToValidate="txtCCCity"
                                                                        ErrorMessage="City only contain letters,numbers,'-' and '#'" Font-Bold="True"
                                                                        Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9,\#,\-,\s]*$"
                                                                        ValidationGroup="sponser"></asp:RegularExpressionValidator>
                                                                    
                                                                </div>
                                                                <div class="yt-Form-Field">
                                                                    <label>
                                                                        <em class="required">* </em>Zip Code/Postal Code:</label>
                                                                    <asp:TextBox ID="txtCCZipCode" runat="server" CssClass="yt-Form-Input" MaxLength="10"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvCCZipCode" runat="server" ControlToValidate="txtCCZipCode"
                                                                        ErrorMessage="Zip Code/Postal Code is a required field." Font-Bold="True" Font-Size="Medium"
                                                                        ForeColor="#FF8000" Text="!" ValidationGroup="sponser"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="revZipCode" runat="server" ControlToValidate="txtCCZipCode"
                                                                        ErrorMessage="Zip Code/Postal Code can only contain letters and numbers" Font-Bold="True"
                                                                        Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9\s]*$"
                                                                        ValidationGroup="sponser"></asp:RegularExpressionValidator>
                                                                </div>
                                                                <div class="yt-Form-Field">
                                                                    <label>
                                                                        <em class="required">* </em>Phone Number:</label>
                                                                    (<asp:TextBox ID="txtPhoneNumber1" runat="server" CssClass="yt-Form-Input-Long" MaxLength="3"
                                                                        Width="34px" onkeypress="NumberOnly()"></asp:TextBox>
                                                                    )
                                                                    <asp:TextBox ID="txtPhoneNumber2" runat="server" CssClass="yt-Form-Input-Long" MaxLength="3"
                                                                        Width="34px" onkeypress="NumberOnly()"></asp:TextBox>
                                                                    -
                                                                    <asp:TextBox ID="txtPhoneNumber3" runat="server" CssClass="yt-Form-Input-Long" MaxLength="4"
                                                                        Width="40px" onkeypress="NumberOnly()"></asp:TextBox>
                                                                     <asp:RequiredFieldValidator runat="server" ID="valFirstName" ControlToValidate="txtFirstName"
                                                                        Width="1px" ErrorMessage="Phone number is a required field." Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000">!</asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtPhoneNumber3" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"
                                                                        ErrorMessage="Invalid phone number" ValidationExpression="^[0-9,]*$">!</asp:RegularExpressionValidator>
                                                                    <asp:CustomValidator ID="cvPhoneNumber" runat="server" ClientValidationFunction="PhoneNumbervalidation"
                                                                        ErrorMessage="Enter valid phone number.Format should be XXX-XXX-XXXX." Font-Bold="True"
                                                                        Font-Size="Medium" ForeColor="#FF8000" ValidationGroup="sponser">!</asp:CustomValidator>                                                                       
                                                                        
                                                                </div>
                                                                <div class="yt-Form-Field">
                                                                    <label>
                                                                        <em class="required">* </em>Email Address:</label>
                                                                    <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="yt-Form-Input-Long" MaxLength="100"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvEmailAddress" runat="server" ControlToValidate="txtEmailAddress"
                                                                        ErrorMessage="Email address is a required field." Font-Bold="True" Font-Size="Medium"
                                                                        ForeColor="#FF8000" Text="!" ValidationGroup="sponser">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>
                                                                <div id="DivRenew" runat="server">
                                                                    <% if (_userName != null)
                                                                       { %>
                                                                    <span id="Renewal" runat="server"><em>* </em>Renewal Options</span>
                                                                    <div class="yt-Form-Field yt-Form-Field-Radio">
                                                                        <input id="ctl00_TributePlaceHolder_rdoNotifyBeforeRenew" checked="checked" name="ctl00$TributePlaceHolder$rdoRenew"
                                                                            onclick="MakeAutoRenew();" type="radio" value="rdoNotifyBeforeRenew" />
                                                                        <label for="ctl00_TributePlaceHolder_rdoNotifyBeforeRenew">
                                                                            I do not want this
                                                                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                                                                            to be renewed automatically on a yearly basis, but I will be notified when the account
                                                                            is near to expiry.</label>
                                                                    </div>
                                                                    <div class="yt-Form-Field yt-Form-Field-Radio">
                                                                        <input id="rdoAutoRenew" runat="server" name="ctl00$TributePlaceHolder$rdoRenew"
                                                                            onclick="MakeAutoRenew();" type="radio" value="rdoYearlyAutoRenew" />
                                                                        <label for="ctl00_TributePlaceHolder_rdoYearlyAutoRenew">
                                                                            I want this
                                                                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToString()%>
                                                                            to be renewed automatically on a yearly basis.</label>
                                                                    </div>
                                                                    <br />
                                                                    <% } %>
                                                                </div>
                                                                <% if (_userName != null)
                                                                   { %>
                                                                <div class="yt-Form-Field yt-Form-Field-Checkbox">
                                                                    <asp:CheckBox ID="chkSaveBillingInfo" runat="server" Enabled="true" Text="I would like to save the above billing information in my profile." />
                                                                </div>
                                                                <% } %>
                                                                <div id="yt-PaymentTotal" class="yt-InfoBox">
                                                                    You will be charged: <span id="BillingTotal" runat="server"></span>
                                                                </div>
                                                                <p>
                                                                </p>
                                                            </p>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <%--<div id="divLowerGreenPanel" runat="server">--%>
                                                <%--</div>--%>
                                                <div class="yt-Form-Field yt-Form-Field-Checkbox">
                                                    <input id="chkAgree" runat="server" name="chkAgree" type="checkbox" value="AgreeToTerms" />
                                                    <label for="chkAgree">
                                                        <em class="required">* </em>I have read and agree to the <a href="termsofuse.aspx"
                                                            target="_blank">terms of use</a>, the cancellation/refund policy (outlined in
                                                        the terms of use) and the <a href="privacy.aspx" target="_blank">privacy policy</a>.
                                                        <asp:Label ID="ErrorAccept" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red" Text="!" Visible="false"></asp:Label>
                                                        <asp:CustomValidator ID="cvAcceptPolicies" runat="server" ClientValidationFunction="ValidateTandCs"
                                                        ErrorMessage="Please accept that you have read and agree to the terms of use, cancellation/refund policy and the privacy policy."
                                                        ForeColor="Red" Font-Bold="True" Text="!" ValidationGroup="sponser" Width="1px"></asp:CustomValidator>                                                        
                                                        </label>
                                                        <br />
                                                </div>
                                                <br />
                                                <p>
                                                    If you have reviewed all of the above information and it is correct, you must be
                                                    ready to...
                                                    <%--<asp:CustomValidator ID="cvAcceptPolicies" runat="server" ClientValidationFunction="ValidateTandCs"
                                                        ErrorMessage="Please accept that you have read and agree to the terms of use, cancellation/refund policy and the privacy policy."
                                                        ForeColor="Red" Text="!" ValidationGroup="sponser" Width="1px"></asp:CustomValidator>--%>
                                                </p>
                                                <div class="yt-Form-Buttons">
                                                    <div class="yt-Form-Submit">
                                                        <asp:LinkButton ID="lbtnPay" runat="server" CssClass="yt-Button yt-ArrowButton" OnClick="lbtnPay_Click"
                                                            OnClientClick="return OnPayClick();" ValidationGroup="vgNextStep">Upgrade this tribute!</asp:LinkButton>
                                                        <asp:Label ID="lblProcess" runat="server"></asp:Label>
                                                    </div>
                                                    <asp:LinkButton ID="btnCancel" runat="server" CausesValidation="false" OnClick="btnCancel_Click"
                                                        Text="Cancel Payment" />
                                                    <span style="color: #0000ff"></span>
                                                </div>
                                                <!-- end yt-Form-Buttons-->
                                                <%--  </ContentTemplate>--%>
                                                <%--</asp:UpdatePanel>--%>
                                                <p>
                                                </p>
                                            </p>
                                        </ContentTemplate>
                                        <Triggers>
                                            <%--<asp:AsyncPostBackTrigger ControlID="lbtnValidateCoupon" EventName="Click" />--%>
                                            <asp:PostBackTrigger ControlID="lbtnValidateCoupon" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </fieldset>
                            </div>
                        </div>
                    </div>
                    <!--yt-ContentPrimary-->
                </div>
                <div class="hack-clearBoth">
                </div>
                <div class="yt-ContentContainerImage">
                </div>
            </div>
            <!--yt-ContentContainerInner-->
        </div>
        <!--yt-ContentContainer-->
        <uc:Footer ID="Footer1" runat="server" />
        <!--yt-Footer-->
    </div>
    <!--yt-Container-->
    <div class="upgrade">
        <h2>
            Please Upgrade Your Browser</form>

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
      <% } %>   
    </script>
    
    <!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/scripts/BrowserOrTabCloseHandler.js"></script>

</body>

<script type="text/javascript" src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/scripts/modalbox.js"></script>

</html>
