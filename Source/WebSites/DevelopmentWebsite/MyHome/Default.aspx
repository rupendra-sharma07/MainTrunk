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
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../test/assets/default.css" />
    <!-- default grid layout stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../test/assets/layouts/centered1024_12.css" />
    <!-- print-friendly stylesheet -->
    <link rel="stylesheet" type="text/css" media="print" href="../test/assets/print.css" />
    <!-- screen elements stylesheet should be here -->
    <link rel="stylesheet" type="text/css" href="../test/assets/screen.css" />
    <!-- larger text stylesheets -->
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="../test/assets/medium_text.css"
        title="medium_text" />
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="../test/assets/large_text.css"
        title="large_text" />
    <!-- Admin-specific elements stylesheet should be here -->
    <link rel="stylesheet" type="text/css" href="../test/assets/admin.css" />
    <!-- JS libraries -->

    <script type="text/javascript" src="../assets/scripts/mootools-1.11.js"></script>

    <script type="text/javascript" src="../assets/scripts/global.js"></script>

    <script type="text/javascript" src="../assets/scripts/styleSwitcher.js"></script>

    <script type="text/javascript" src="../assets/scripts/modalbox.js"></script>

    <script type="text/javascript" src="../assets/scripts/admin.js"></script>

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

</head>
<body>
    <form action="" runat="server" id="form1">
    <div id="divShowModalPopup"></div> 
    <div id="yt-AdminReceiptContent" class="yt-ModalWrapper">
        <div class="yt-Panel-Primary">
            <address class="vcard">
                <span class="fn org">
                    <img src="assets/images/logo_AdminReceipt.gif" alt="Your Tribute" /><span>Your Tribute</span></span>
                <span class="adr"><span class="street-address">6952 Greenwood Street</span><br />
                    <span class="locality">Burnaby</span>,
                    <abbr class="region" title="British Columbia">
                        BC</abbr>, <span class="postal-code">V5A 1X8</span> <span class="country-name">Canada</span><br />
                </span><a class="email" href="mailto:<%=TributesPortal.Utilities.WebConfig.YourTributeEmail%>">
                    <%=TributesPortal.Utilities.WebConfig.YourTributeEmail%></a><br />
                <span class="tel">1.866.468.4996 </span>
            </address>
            <h3>
                Payment Receipt</h3>
            <h4>
                Tribute Details:</h4>
            <dl>
                <dt>Tribute Name:</dt>
                <dd>
                    John and Mary Smith</dd>
                <dt>Tribute Type: </dt>
                <dd>
                    Wedding</dd>
                <dt>Package Type:</dt>
                <dd>
                    1 Year</dd>
                <dt>Expiry Date: </dt>
                <dd>
                    September 28, 2008</dd>
            </dl>
            <h4>
                Billing Addroess:</h4>
            <dl>
                <dt>Name: </dt>
                <dd>
                    John Smith</dd>
                <dt>Address:</dt>
                <dd>
                    555 Tree Street</dd>
                <dt>City:</dt>
                <dd>
                    Vancouver</dd>
                <dt>State/Province:</dt>
                <dd>
                    British Columbia</dd>
                <dt>Country:</dt>
                <dd>
                    Canada</dd>
                <dt>Zip/Postal Code:</dt>
                <dd>
                    V6H 1S2</dd>
                <dt>Telephone: </dt>
                <dd>
                    555.555.5555</dd>
            </dl>
            <h4>
                Billing Details:</h4>
            <dl>
                <dt>Billing Date:</dt>
                <dd>
                    September 28, 2007</dd>
                <dt>Payment Type: </dt>
                <dd>
                    Visa</dd>
                <dt>Credit Card:</dt>
                <dd>
                    xxxx xxxx xxx 555</dd>
                <dt>Amount Billed:</dt>
                <dd>
                    $20 USD</dd>
            </dl>
            <div class="yt-Form-MiniButtons">
                <div class="yt-Form-Submit">
                    <a href="javascript:void(0);" class="yt-MiniButton yt-PrintButton" onclick="printModal();">
                        Print</a>
                </div>
            </div>
        </div>
    </div>
    <div class="yt-Container yt-Admin yt-AnonymousUser">
        <div class="yt-HeaderContainer">
            <div class="yt-Header">
                <a href="homepage_tribute.htm" title="Return to Your Tribute Home Page" class="yt-Logo">
                </a>
                <div class="yt-HeaderControls">
                    <div class="yt-NavHeader">
                        <div class="yt-UserInfo">
                            Viewing: My Profile <a href="javascript: void(0);" class="yt-Inbox">Inbox (2)</a>
                            <span class="yt-UserName">Usernameislong</span><a href="javascript: void(0);">Log out</a>
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
                        <div class="yt-Error">
                            <h2>
                                Oops - there was a problem with your billing information.</h2>
                            <h3>
                                Please correct the errors below:</h3>
                            <ul>
                                <li>Email Address is a required field.</li>
                                <li>Username is a required field.</li>
                                <li>Password is a required field.</li>
                                <li>First Name is a required field.</li>
                            </ul>
                        </div>
                        <div class="yt-AdminMain">
                            <ul class="yt-AdminNavPrimary">
                                <li><a href="javascript:void(0);">My Tributes</a></li>
                                <li><a href="javascript:void(0);">My Favorites</a></li>
                                <li><a href="javascript:void(0);">Inbox</a></li>
                                <li><a href="javascript:void(0);">Events</a></li>
                                <li class="yt-Selected" id="yt-MyProfileTab"><a href="javascript:void(0);">My Profile</a></li>
                            </ul>
                            <div class="yt-Panel-Primary">
                                <ul class="yt-AdminNavSecondary">
                                    <li><a href="javascript:void(0);">Profile Settings</a></li>
                                    <li><a href="javascript:void(0);">Privacy Settings</a></li>
                                    <li><a href="javascript:void(0);">Change Email/Password</a></li>
                                    <li><a href="javascript:void(0);">Email Notifications</a></li>
                                    <li class="yt-Selected"><a href="javascript:void(0);">Billing Info</a></li>
                                </ul>
                                <div id="yt-BillingFormContainer">
                                    <h3 class="personal yt-AccountTypeDescription">
                                        Billing History:</h3>
                                    <table cellspacing="0" class="yt-AdminTable">
                                        <col class="yt-Col-Date" />
                                        <col class="yt-Col-Name" />
                                        <col class="yt-Col-AcctType" />
                                        <col class="yt-Col-Amount" />
                                        <thead>
                                            <tr>
                                                <th>
                                                    Billing Date
                                                </th>
                                                <th>
                                                    Tribute Name
                                                </th>
                                                <th>
                                                    Account Type
                                                </th>
                                                <th>
                                                    Amount
                                                </th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    14-Aug-07
                                                </td>
                                                <td>
                                                    John and Mary Smith
                                                </td>
                                                <td>
                                                    Lifetime
                                                </td>
                                                <td>
                                                    $50
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    31-Dec-07
                                                </td>
                                                <td>
                                                    Jane Doe
                                                </td>
                                                <td>
                                                    1 Year
                                                </td>
                                                <td>
                                                    $20
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <fieldset class="yt-Form">
                                        <h4>
                                            Credit Card Information:</h4>
                                        <p class="yt-requiredFields">
                                            <strong>Required fields are indicated with <em class="required">* </em></strong>
                                        </p>
                                        <fieldset class="yt-PaymentSelection yt-CompactRadioList">
                                            <legend>* Select your payment method:</legend>
                                            <div class="yt-Form-Field yt-Form-Field-Radio" id="yt-CCMasterCard">
                                                <input type="radio" name="rdoTributeType" id="rdoCCMasterCard" value="MasterCard" />
                                                <label for="rdoCCMasterCard">
                                                    Master Card</label>
                                            </div>
                                            <div class="yt-Form-Field yt-Form-Field-Radio" id="yt-CCVisa">
                                                <input type="radio" name="rdoCCType" id="rdoCCVisa" value="Visa" />
                                                <label for="rdoCCVisa">
                                                    Visa</label>
                                            </div>
                                            <div class="yt-Form-Field yt-Form-Field-Radio" id="yt-CCAmex">
                                                <input type="radio" name="rdoCCType" id="rdoCCAmex" value="Amex" />
                                                <label for="rdoCCAmex">
                                                    American Express</label>
                                            </div>
                                            <div class="yt-Form-Field yt-Form-Field-Radio" id="yt-CCMDiscover">
                                                <input type="radio" name="rdoCCType" id="rdoCCDiscover" value="Discover" />
                                                <label for="rdoCCDiscover">
                                                    Discover</label>
                                            </div>
                                        </fieldset>
                                        <div class="yt-Form-Field">
                                            <label for="txtCCNumber">
                                                * Card number:</label>
                                            <input type="text" class="yt-Form-Input-Long" id="txtCCNumber" />
                                            <span class="yt-Form-Field-Error">&nbsp;</span>
                                            <div class="hint">
                                                hint for card number...<span class="hintPointer"></span></div>
                                        </div>
                                        <div class="yt-Form-Field">
                                            <label for="txtCCVerification">
                                                * Card verification:</label>
                                            <input type="text" class="yt-Form-Input-Short" id="txtCCVerification" />
                                            <span class="yt-Form-Field-Error">&nbsp;</span>
                                            <div class="hint">
                                                hint for card verification...<span class="hintPointer"></span></div>
                                        </div>
                                        <fieldset class="yt-Date-Fields">
                                            <legend><em class="required">* </em>Expiry Date:</legend>
                                            <div class="yt-Form-Field">
                                                <select name="ddlCCMonth" class="yt-Form-DropDown" id="ddlCCMonth">
                                                    <option value=""></option>
                                                    <option value="January">January</option>
                                                    <option value="February">February</option>
                                                    <option value="March">March</option>
                                                    <option value="April">April</option>
                                                    <option value="May">May</option>
                                                    <option value="June">June</option>
                                                    <option value="July">July</option>
                                                    <option value="August">August</option>
                                                    <option value="September">September</option>
                                                    <option value="October">October</option>
                                                    <option value="November">November</option>
                                                    <option value="December">December</option>
                                                </select><span class="yt-Form-Field-Error">&nbsp;</span>
                                                <label for="ddlCCMonth">
                                                    Month</label>
                                            </div>
                                            <div class="yt-Form-Field">
                                                <input type="text" id="txtCCYear" class="yt-Form-Input-Short" /><span class="yt-Form-Field-Error">&nbsp;</span>
                                                <label for="txtCCYear">
                                                    Year</label>
                                            </div>
                                        </fieldset>
                                        <div class="yt-Form-Field">
                                            <label for="txtCCName">
                                                <em class="required">* </em>Name on card:</label>
                                            <input type="text" id="txtCCName" class="yt-Form-Input-Long" />
                                            <span class="yt-Form-Field-Error">&nbsp;</span>
                                            <div class="hint">
                                                hint for name on card...<span class="hintPointer"></span></div>
                                        </div>
                                        <div class="yt-Form-Field">
                                            <label for="txtCCBillingAddress">
                                                <em class="required">* </em>Billing address:</label>
                                            <input type="text" class="yt-Form-Input-Long" id="txtCCBillingAddress" />
                                            <span class="yt-Form-Field-Error">&nbsp;</span>
                                            <div class="hint">
                                                hint for billing address<span class="hintPointer"></span></div>
                                        </div>
                                        <div class="yt-Form-Field">
                                            <input type="text" class="yt-Form-Input-Long" id="txtCCBillingAddress2" />
                                            <div class="hint">
                                                hint for billing address line 2<span class="hintPointer"></span></div>
                                        </div>
                                        <div class="yt-Form-Field">
                                            <label for="ddlCCCountry">
                                                <em class="required">* </em>Country:</label>
                                            <select id="ddlCCCountry" name="ddlCCCountry" class="yt-Form-DropDown-Long">
                                                <option value=""></option>
                                                <option value="Canada">Canada</option>
                                            </select>
                                        </div>
                                        <div class="yt-Form-Field">
                                            <label for="ddlCCStateProvince">
                                                <em class="required">* </em>State / Province:</label>
                                            <select id="ddlCCStateProvince" name="ddlCCStateProvince" class="yt-Form-DropDown-Long">
                                                <option value=""></option>
                                                <option value="BC">British Columbia</option>
                                            </select>
                                        </div>
                                        <div class="yt-Form-Field">
                                            <label for="txtCCCity">
                                                <em class="required">* </em>City:</label>
                                            <input type="text" id="txtCCCity" class="yt-Form-Input-Long" />
                                            <div class="hint">
                                                hint for city...<span class="hintPointer"></span></div>
                                        </div>
                                        <div class="yt-Form-Field">
                                            <label for="txtCCZipCode">
                                                <em class="required">* </em>Zip Code / Postal Code:</label>
                                            <input type="text" class="yt-Form-Input" id="txtCCZipCode" />
                                            <div class="hint">
                                                hint for zip code...<span class="hintPointer"></span></div>
                                        </div>
                                        <div class="yt-Form-Field">
                                            <label for="txtCCPhone">
                                                <em class="required">* </em>Telephone:</label>
                                            <input type="text" id="txtCCPhone" class="yt-Form-Input-Long" />
                                            <div class="hint">
                                                hint for phone number...<span class="hintPointer"></span></div>
                                        </div>
                                        <div class="yt-Form-Buttons">
                                            <div class="yt-Form-Delete">
                                                <a href="javascript:void(0);" class="yt-Button yt-XButton">Delete Credit Card Information</a></div>
                                            <div class="yt-Form-Submit">
                                                <a href="javascript:void(0);" class="yt-Button yt-CheckButton">Save Changes</a></div>
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
                <div class="yt-ContentSecondary">
                    <div class="yt-Panel-System">
                        <h2 >
                        News from Your Tribute
                           
</h2>
                        <div class="yt-Panel-Body">
                            <ul>
                                <li><a href="javascript:void(0);">Ten new themes added</a><br />
                                    February 14, 2008</li>
                                <li><a href="javascript:void(0);">Customer feedback too good to keep to ourselves</a><br />
                                    January 29, 2008</li>
                                <li><a href="javascript:void(0);">Five new Wedding Tribute themes added</a><br />
                                    January 25, 2008</li>
                                <li><a href="javascript:void(0);">Online help area expanded</a><br />
                                    January 15, 2008</li>
                                <li><a href="javascript:void(0);">
                                    <%=TributesPortal.Utilities.WebConfig.TopLevelDomain%>
                                    launched!</a><br />
                                    January 01, 2008</li>
                            </ul>
                            <div class="yt-Form-Buttons">
                                <div class="yt-Form-Submit">
                                    <a href="javascript:void(0);" class="yt-Button yt-ArrowButton">View entire blog</a></div>
                            </div>
                        </div>
                    </div>
                </div>
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
