<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CouponsList.aspx.cs" Inherits="Coupons_CouponsList"
    Title="CouponsList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head>
    <title>Your Tribute - My Tributes: Favorites</title>
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

    <script type="text/javascript" src="../Common/JavaScript/InternalMessage.js"></script>
    
    <!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
    <script type="text/javascript" src="../assets/scripts/BrowserOrTabCloseHandler.js"></script>

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
	
	
	
	function UserDetails_(para1,para2)
	{
	 UserDetails(para1,para2);
	 HideHeader();
	}
	
    function HideHeader()
    {
       var notice = $('Notice');
       if(notice)
       {	   	   
	   notice.innerHTML='';	 
	   notice.removeClass('yt-Notice');	 
	   notice.removeClass('yt-Error');
	   notice.style.visibility = 'hidden';
	   }
    }
	
	
	function SetHeader(message,type)
	{
	  var notice = $('Notice');
	  if(notice)
	  {
	   if(type==1)
	   notice.className='yt-Notice';
	   else
	   notice.className='yt-Error';
	   alert(message);
	   notice.innerHTML=message;
	   notice.style.visibility = 'visible';
	   
	   
	  }
	}
	
	function dispconfirm()
{
var r=confirm("Are you sure you want to delete the coupon?");
if (r==true)
  {
    return true;
  }
else
  {
    return false;
  }
}
	
    </script>
<!--#include file="../analytics.asp"-->
</head>
<body>
    <form id="Form1" action="" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="false"  runat="server">
        </asp:ScriptManager>
        <div class="yt-Container yt-Admin">
            <div class="yt-HeaderContainer">
                <div class="yt-Header">
                    <a href="home.aspx" title="Return to Your Tribute Home Page"
                        class="yt-Logo"></a>
                    <div class="yt-HeaderControls">
                        <div class="yt-NavHeader">
                            <div class="yt-UserInfo">
                                &nbsp; <span id="Usernamelong" runat="server"></span><span id="spanLogout" runat="server">
                                </span>
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
                    <div class="yt-ContentPrimary">
                        <!-- please display this notice when returning to this screen after deleting a tribute -->
                        <div class="yt-Error" id="errormsg" runat="server" visible="false">
                        </div>
                        <div id="Notice" runat="server" style="visibility: hidden">
                        </div>
                        <div style="font: caption">
                            COUPONS
                        </div>
                        <div>
                            <div>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="gvMyCoupons" CssClass="yt-AdminTable" AutoGenerateColumns="False"
                                            runat="server" Width="786px" OnRowCreated="gvMyCoupons_RowCreated" OnRowCommand="gvMyCoupons_RowCommand" OnRowDataBound="gvMyCoupons_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblTributes" runat="server" Text="Coupon Name"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="txtCoupoName" CommandName="SelectCoupon" Text='<%# Eval("CouponName") %>'
                                                            runat="server"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="GridHeaderStyle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblCoupons" runat="server" Text="Coupons"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCouponsCount" runat="server" Text='<%# Eval("CouponCount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="GridHeaderStyle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblCouponDenomination" runat="server" Text="Discount"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtCouponDenomination" runat="server" Text='<%# Eval("CouponDenomination") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="GridHeaderStyle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblPackage" runat="server" Text="Package"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtPackage" runat="server" Text='<%# Eval("Package") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="GridHeaderStyle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblACreated" runat="server" Text="Created:"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtCreated" runat="server" Text='<%# Bind("ApplicableFromDate","{0:MM/dd/yyyy}")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="GridHeaderStyle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblExpires" runat="server" Text="Expires:"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtExpires" runat="server" Text='<%# Bind("ExpiryDate","{0:MM/dd/yyyy}")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="GridHeaderStyle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:Label ID="lblUsage" runat="server" Text="Usage"></asp:Label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtUsage" runat="server" Text='<%# Eval("Usage") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="GridHeaderStyle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remove" ShowHeader="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButtonDelete" runat="server" CausesValidation="false" CommandName="Remove"
                                                             CommandArgument='<%# Container.DataItemIndex %>' Text="Remove"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ControlStyle CssClass="yt-MiniButton yt-DeleteButton" />
                                                    <HeaderStyle CssClass="GridHeaderWidthStyle" />
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="False">
                                                    <ItemTemplate>
                                                     <asp:Label ID="txtPrimaryCouponID" runat="server" Visible="false" Text='<%# Eval("PrimaryCouponID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/Coupons/CouponCreation.aspx">Create a new coupon</asp:LinkButton>
                                <!-- yt-TributeList -->
                            </div>
                        </div>
                    </div>
                    <!--yt-ContentPrimary-->
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
