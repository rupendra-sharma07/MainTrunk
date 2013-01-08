<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CouponCreation.aspx.cs" Inherits="Coupons_CouponCreation"
    Title="CouponCreation" %>

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
     App_Domain = "<%=Session["APP_BASE_DOMAIN"]%>";
 
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
	
	

function ValidateDiscount(source, args)
{
    var txtDolar =  document.getElementById('<%=txtDolar.ClientID%>'); 
    var txtPercentage =  document.getElementById('<%=txtPercentage.ClientID%>'); 
    var cvDiscount =  document.getElementById('<%=cvDiscount.ClientID%>');
    
    if((txtDolar.value.length==0)&&(txtPercentage.value.length==0)||(txtDolar.value.length!=0)&&(txtPercentage.value.length!=0))
    {
        args.IsValid=false;
        cvDiscount.errormessage="Please enter one of them in discount.";
    }
    else
    { 
      if(txtPercentage.value.length!=0)
      {
        var Perc=parseInt(txtPercentage.value);
        if((Perc>=1)&&(Perc<=100))
        args.IsValid=true;
        else
        {
         args.IsValid=false;
         cvDiscount.errormessage="Percentage value should be in between 1 and 100.";
        }
      }
      else
      {
        var chk=isInteger(txtDolar.value);
        if(chk==false)
        {
         cvDiscount.errormessage="Please eneter numeric value in discount.";
         args.IsValid=false;
        }
        }
      }
    
    }
    
    function isInteger(s)
    {   var i;
    for (i = 0; i < s.length; i++)
    {   
    // Check that current character is number.
    var c = s.charAt(i);
    if (((c < "0") || (c > "9"))) return false;
    }
    // All characters are numbers.
    return true;
    }
    
    
    function ValidateCoupondate(source, args)
    {
    
        var Year =  document.getElementById('<%=txtCCYear.ClientID%>'); 
        var Month =  document.getElementById('<%=ddlCCMonth.ClientID%>'); 
        var day =  document.getElementById('<%=ddlDay.ClientID%>'); 
         var validator =  document.getElementById('<%=CustomValidator3.ClientID%>'); 
        
        if((Year.value.length!=0)&&(Month.selectedIndex!=0)&&(day.selectedIndex!=0))
        {   
         var thisDate=Month.selectedIndex+"/"+day.selectedIndex+"/"+Year.value;                          
         var valid=isDate(thisDate,validator);             
         if(valid==true)
         {
                var currentTime = new Date();
                var month_ = currentTime.getMonth() + 1;
                var day_ = currentTime.getDate();
                var year_ = currentTime.getFullYear();
                var thisDate_=month_+"/"+day_+"/"+year_;    
                if (Date.parse(thisDate) < Date.parse(thisDate_)) 
                {
                    validator.errormessage="Expiry date should be greater than current date.";
                    args.IsValid= false;
                }
         }
         else
         {
            args.IsValid=false;
         }
        }
        else
        {
            args.IsValid=false;
            validator.errormessage="Expiry date is a required field.";
        }
    }
 
var dtCh= "/";
var minYear=1900;
var maxYear=2100;


function stripCharsInBag(s, bag){
	var i;
    var returnString = "";
    // Search through string's characters one by one.
    // If character is not in bag, append to returnString.
    for (i = 0; i < s.length; i++){   
        var c = s.charAt(i);
        if (bag.indexOf(c) == -1) returnString += c;
    }
    return returnString;
}

function daysInFebruary (year){
	// February has 29 days in any year evenly divisible by four,
    // EXCEPT for centurial years which are not also divisible by 400.
    return (((year % 4 == 0) && ( (!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28 );
}
function DaysArray(n) {
	for (var i = 1; i <= n; i++) {
		this[i] = 31
		if (i==4 || i==6 || i==9 || i==11) {this[i] = 30}
		if (i==2) {this[i] = 29}
   } 
   return this
}

function isDate(dtStr,validator)
{
	var daysInMonth = DaysArray(12)
	var pos1=dtStr.indexOf(dtCh)
	var pos2=dtStr.indexOf(dtCh,pos1+1)
	var strMonth=dtStr.substring(0,pos1)
	var strDay=dtStr.substring(pos1+1,pos2)
	var strYear=dtStr.substring(pos2+1)
	strYr=strYear
	if (strDay.charAt(0)=="0" && strDay.length>1) strDay=strDay.substring(1)
	if (strMonth.charAt(0)=="0" && strMonth.length>1) strMonth=strMonth.substring(1)
	for (var i = 1; i <= 3; i++) {
		if (strYr.charAt(0)=="0" && strYr.length>1) strYr=strYr.substring(1)
	}
	month=parseInt(strMonth)
	day=parseInt(strDay)
	year=parseInt(strYr)
	if (pos1==-1 || pos2==-1){
		validator.errormessage="Please eneter valid expiry date.";
		return false
	}
	if (strMonth.length<1 || month<1 || month>12){
		validator.errormessage="Please eneter valid expiry date.";
		return false
	}
	if (strDay.length<1 || day<1 || day>31 || (month==2 && day>daysInFebruary(year)) || day > daysInMonth[month]){
		validator.errormessage="Please eneter valid expiry date.";
		return false
	}
	if (strYear.length != 4 || year==0 || year<minYear || year>maxYear)
	{
	    validator.errormessage="Please eneter valid expiry date.";
		return false
	}
	if (dtStr.indexOf(dtCh,pos2+1)!=-1 || isInteger(stripCharsInBag(dtStr, dtCh))==false)
	{		
		validator.errormessage="Please eneter valid expiry date.";
		return false
	}
return true
}

	function HideIndicator()
    {
     var lblErrMsg= document.getElementById('<%= errormsg.ClientID %>'); 
     if(lblErrMsg)
     {
       lblErrMsg.innerHTML = '';
       lblErrMsg.style.visibility = 'hidden';
     }
    }


function CouponNumberTest(source, args)
{
    var bol=false;
   var CouponNumb =  document.getElementById('<%=txtCouponnumbers.ClientID%>'); 
   if(CouponNumb.value=="0")
    bol=false;
    else
    {
      bol=isInteger(CouponNumb.value);      
    }
    
args.IsValid=bol;    
}

//Check Integer value
function isInteger(s){
	var i;
    for (i = 0; i < s.length; i++){   
        // Check that current character is number.        
        var c = s.charAt(i);
        if (((c < "0") || (c > "9"))) return false;
    }
    // All characters are numbers.
    return true;
}
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
                            <div>
                                <asp:ValidationSummary CssClass="yt-Error" ID="PortalValidationSummary" runat="server"
                                    Width="631px" HeaderText=" <h2>Oops - there was a problem in coupon creation.</h2>                                                             <h3>Please correct the errors below:</h3>"
                                    ForeColor="Black" />
                            </div>
                            <div id="errormsg" runat="server" visible="false">
                            </div>
                            <div class="yt-AdminMain">
                                <div class="yt-Panel-Primary">
                                    <div id="yt-BillingFormContainer">
                                        <h3 class="personal yt-AccountTypeDescription">
                                            COUPON CREATION:</h3>
                                        <fieldset class="yt-Form" id="CCdetails" runat="server">
                                            <p class="yt-requiredFields">
                                                <strong>Required fields are indicated with <em class="required">* </em></strong>
                                            </p>
                                            <div class="yt-Form-Field">
                                                <label>
                                                    <em class="required">* </em>COUPON NAME:</label>
                                                <asp:TextBox ID="txtCouponName" MaxLength="50" runat="server" Width="285px"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvCCNumber" Font-Bold="True" Font-Size="Medium"
                                                    ForeColor="#FF8000" ControlToValidate="txtCouponName" runat="server" Text="!"
                                                    ErrorMessage="Coupon name is a required field." Width="1px"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="yt-Form-Field">
                                                <label>
                                                    <em class="required">* </em>NUMBER OF COUPONS TO CREATE:</label>
                                                <asp:TextBox ID="txtCouponnumbers" CssClass="yt-Form-Input-Short" runat="server"
                                                    MaxLength="4">1</asp:TextBox>
                                                <asp:RequiredFieldValidator ControlToValidate="txtCouponnumbers" Font-Bold="True"
                                                    Font-Size="Medium" ForeColor="#FF8000" ID="rfvCCVerification" Text="!" runat="server"
                                                    ErrorMessage="Number of coupons is a required field." Width="1px"></asp:RequiredFieldValidator>
                                                <asp:CustomValidator ID="CustomValidator1" ClientValidationFunction="CouponNumberTest" runat="server" Font-Bold="True" Font-Size="Medium"
                                                    ForeColor="#FF8000" Text="!" ErrorMessage="Please enter valid coupon number. "></asp:CustomValidator>
                                            </div>
                                            <div class="yt-Form-Field">
                                                <label>
                                                    USAGE:</label>
                                                <asp:DropDownList ID="ddlusage" runat="server" Width="285px">
                                                </asp:DropDownList>
                                            </div>
                                            <fieldset class="yt-Date-Fields">
                                                <legend><em class="required">* </em>EXPIRY DATE:</legend>
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
                                                    <asp:DropDownList ID="ddlDay" runat="server" Width="50px">
                                                    </asp:DropDownList>
                                                    <label>
                                                        Day</label>
                                                </div>
                                                <div class="yt-Form-Field">
                                                    <asp:TextBox ID="txtCCYear" CssClass="yt-Form-Input-Short" MaxLength="4" runat="server"></asp:TextBox>
                                                    <asp:CustomValidator ID="CustomValidator3" runat="server" Font-Bold="True" Font-Size="Medium"
                                                        ForeColor="#FF8000" ClientValidationFunction="ValidateCoupondate" Text="!" ErrorMessage="Expiry Date is a required field."
                                                        Width="1px" Visible="True"></asp:CustomValidator>
                                                    <span style="color: #FF8000; font-size: Medium; font-weight: bold;" id="SpanExpirDate"
                                                        runat="server" visible="false">!</span>
                                                    <label>
                                                        Year</label>
                                                </div>
                                            </fieldset>
                                            <div class="yt-Form-Field">
                                                <label>
                                                    <em class="required">* </em>DISCOUNT:</label>$
                                                <asp:TextBox ID="txtDolar" runat="server" MaxLength="3" Width="51px"></asp:TextBox>
                                                &nbsp;&nbsp; or&nbsp;
                                                <asp:TextBox ID="txtPercentage" runat="server" MaxLength="3" Width="51px"></asp:TextBox>&nbsp;
                                                %&nbsp;
                                                <asp:CustomValidator ID="cvDiscount" runat="server" Font-Bold="True" Font-Size="Medium"
                                                    ForeColor="#FF8000" ClientValidationFunction="ValidateDiscount" Text="!" ErrorMessage="Please enter one of them."
                                                    Width="1px" Visible="True"></asp:CustomValidator>
                                            </div>
                                            <div class="yt-Form-Field">
                                                <label>
                                                    APPLY TO PACKAGE:</label>
                                                <asp:DropDownList ID="ddlPackageType" runat="server" Width="285px">
                                                </asp:DropDownList>
                                                <br />
                                                <br />
                                                <br />
                                            </div>
                                            <div class="yt-Form-Buttons">
                                                <div class="yt-Form-Delete">
                                                    <a href="CouponsList.aspx" class="yt-Button yt-XButton">Cancel</a>
                                                </div>
                                                <div class="yt-Form-Submit">
                                                    <asp:LinkButton ID="lbtnSaveChanges" CssClass="yt-Button yt-CheckButton" runat="server"
                                                        OnClick="lbtnSaveChanges_Click">Create</asp:LinkButton>
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
                    <div class="yt-ContentContainerImage bgImageBUser">
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
