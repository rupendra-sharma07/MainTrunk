<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="CouponDetail.aspx.cs" Inherits="Coupons_CouponDetail"
    Title="CouponDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head>
    <title>Your Tribute - My Profile: Billing Information</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Content-Language" content="en-ca" />
    <meta http-equiv="imagetoolbar" content="false" />
    <meta name="robots" content="index,follow" />
    <meta name="MSSmartTagsPreventParsing" content="true" />
    <!-- really basic, generic html class stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/default.css" />
    <!-- default grid layout stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/layouts/centered1024_12.css" />
    <!-- print-friendly stylesheet -->
    <link rel="stylesheet" type="text/css" media="print" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/print.css" />
    <!-- screen elements stylesheet should be here -->
    <link rel="stylesheet" type="text/css" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/screen.css" />
    <!-- larger text stylesheets -->
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/medium_text.css"
        title="medium_text" />
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/large_text.css"
        title="large_text" />
    <!-- Admin-specific elements stylesheet should be here -->
    <link rel="stylesheet" type="text/css" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/admin.css" />
    <!-- Website Favorite Icon -->
    <link rel="Shortcut Icon" type="image/x-icon" href="../assets/images/favicon.ico" />
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
	
	});
	
	

    </script>
<!--#include file="../analytics.asp"-->
</head>
<body>
    <form id="Form1" action="" runat="server">
        &nbsp;&nbsp;
        <div class="yt-Container yt-Admin yt-AnonymousUser">
            <div class="yt-HeaderContainer">
                <div class="yt-Header">
                    <a href="home.aspx" title="Return to Your Tribute Home Page"
                        class="yt-Logo"></a>
                    <div class="yt-HeaderControls">
                        <div class="yt-NavHeader">
                            <div class="yt-UserInfo">
                                Coupon Creation&nbsp;<span id="spanLogout" runat="server"></span>
                            </div>
                        </div>
                        <div class="hack-clearBoth">
                        </div>
                        <div class="yt-Tools">
                            <div id="yt-TypeSizeControl" class="yt-TypeSizeControl">
                                <span class="floatLeft">Text Size:&#160;</span> <a href="javascript:void(0);" class="large"
                                    title="Large Text">a</a> <a href="javascript:void(0);" class="medium" title="Medium Text">
                                        a</a> <a href="javascript:void(0);" class="small" title="Small Text">a</a>&nbsp;</div>
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
                            <div id="errormsg" runat="server" visible="false">
                            </div>
                            <div class="yt-AdminMain">
                                <div class="yt-Panel-Primary">
                                    <div id="yt-BillingFormContainer">
                                        <h3 class="personal yt-AccountTypeDescription">
                                            COUPON DETAILS:</h3>
                                        <fieldset class="yt-Form" id="CCdetails" runat="server">
                                            <div>
                                                <label>
                                                    COUPON NAME:</label>
                                                <label id="lblCouponname" runat="server">
                                                </label>
                                                <br />
                                                <br />
                                            </div>
                                            <div>
                                                <label>
                                                    NUMBER OF COUPONS TO CREATE:</label>
                                                <label id="lblCouponNumbers" runat="server">
                                                </label>
                                                <br />
                                                <br />
                                            </div>
                                            <div>
                                                <label>
                                                    USAGE:</label>
                                                <label id="lblUsage" runat="server">
                                                </label>
                                                <br />
                                                <br />
                                            </div>
                                            <div>
                                                <label>
                                                    EXPIRY DATE:</label>
                                                <label id="lblExpiryDate" runat="server">
                                                </label>
                                                <br />
                                                <br />
                                            </div>
                                            <div>
                                                <label>
                                                    DISCOUNT:</label>
                                                <label id="lblDiscount" runat="server">
                                                </label>
                                                <br />
                                                <br />
                                            </div>
                                            <div>
                                                <label>
                                                    APPLY TO PACKAGE:</label>
                                                <label id="lblPackage" runat="server">
                                                </label>
                                                <br />
                                                <br />
                                            </div>
                                            <div>
                                                <br />
                                                <br />
                                                <br />
                                            </div>
                                            <div>
                                            <h3 class="personal yt-AccountTypeDescription">
                                            COUPONS:</h3>
                                            </div>
                                            <div>
                                                <asp:Repeater ID="rptrCoupons" runat="server" >
                                                    <ItemTemplate >
                                                     <p>
                                                        <asp:Label ID="lblCoupon" runat="server"  Text='<%# Eval("CouponCode") %>'></asp:Label>
                                                        </p>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                            <div>
                                                <br />
                                                <br />
                                                <br />
                                            </div>
                                            <div class="yt-Form-Buttons">
                                                <div class="yt-Form-Delete">
                                                    <a href="CouponsList.aspx" class="yt-Button yt-CheckButton">BACK</a>
                                                </div>
                                            </div>
                                            <!--yt-SignUpFormContainer-->
                                        </fieldset>
                                        <!--yt-Form-->
                                    </div>
                                    <!-- yt-BillingFormContainer -->
                                </div>
                            </div>
                        </div>
                        <!--yt-ContentPrimary-->
                    </div>
                    <!--yt-ContentPrimaryContainer-->
                    <!--yt-ContentSecondary-->
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
                    <li><a href="#">Help</a></li>
                    <li><a href="#">Contact Us</a></li>
                    <li><a href="#">About Us</a></li>
                </ul>
                <div class="yt-Legal">
                    <ul class="yt-NavFooter">
                        <li>&#169; 2008 <a href="#">Your Tribute</a></li>
                        <li><a href="#">Terms of Use</a></li>
                        <li><a href="#">Privacy Policy</a></li>
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
