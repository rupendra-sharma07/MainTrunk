<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ModelPopup_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> - My Profile: Billing Information</title>
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
<!--#include file="../analytics.asp"-->
</head>
<body>
    <form id="form1" runat="server">
    <div id="divShowModalPopup"></div> 
    <div style="background-color: #ffffff">
      <a  href="javascript:void(0);" onclick="UserLoginModalpopup(location.href);">Test Model</a>
        </div>
    </form>
</body>
<script type="text/javascript" src="../assets/scripts/modalbox.js"></script>
</html>
