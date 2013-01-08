<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminProfileEmailpassword.aspx.cs"
    Inherits="MyHome_AdminProfileEmailpassword" MasterPageFile="~/Shared/InnerSecure.master" %>

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
	
	function ValidateConformPassword(source, args)
	{
	  var password=document.getElementById('<%=txtPassword.ClientID %>');        
	  var Cpassword=document.getElementById('<%=txtConfirmPassword.ClientID %>');        
	  if(password.value.length!=0)
	  {
	    if(Cpassword.value.length==0)
	     args.IsValid= false;
	     else
	     args.IsValid= true;
	  }
	   else
	    args.IsValid= true;
	}
	
	function ChangeEmailPassword(source, args)
    {
      var email=document.getElementById('<%=txtEmail.ClientID %>');                   
      if(email.value.trim().length==0)
      {
        args.IsValid= false;
        }
      else
        {
         args.IsValid= true;
        }
    }
    
    function ValidatePasswordLength(source, args)
    {
    var password = document.getElementById('<%= txtPassword.ClientID %>');
    args.IsValid=CheckPasswordLength(password.value);
    } 
    </script>

</asp:Content>
<asp:Content ID="content" ContentPlaceHolderID="BodyContentPlaceHolder" runat="Server">
    <div id="divShowModalPopup">
    </div>
    <div id="yt-ProfileFormContainer">
        <fieldset class="yt-Form">
            <h3>
                Change Email/Password</h3>
            <fieldset>
                <legend>Change Your Email Address:</legend>
                <div class="yt-Form-Field">
                    <label>
                        Email Address:</label>
                    <asp:TextBox ID="txtEmail" CssClass="yt-Form-Input-Long" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                        ErrorMessage="Please enter a valid email address. " Font-Bold="True" Font-Size="Medium"
                        ForeColor="#FF8000" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">!</asp:RegularExpressionValidator>
                    <asp:CustomValidator ID="CustomValidator2" runat="server" Font-Bold="True" Font-Size="Medium"
                        ForeColor="#FF8000" ErrorMessage="Email Address is a required field." ClientValidationFunction="ChangeEmailPassword"
                        Width="1px">!</asp:CustomValidator>
                </div>
            </fieldset>
            <fieldset>
                <legend>Change Your Password:</legend>
                <div class="yt-Form-Field">
                    <label>
                        Password:</label>
                    <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="yt-Form-Input" runat="server"></asp:TextBox>
                    <asp:CustomValidator ID="cvPassword" runat="server" ClientValidationFunction="ValidatePasswordLength"
                        ErrorMessage="Password must be between 6 to 16 characters" Font-Bold="True" Font-Size="Medium"
                        ForeColor="#FF8000" Width="1px">!</asp:CustomValidator>
                    <asp:RegularExpressionValidator ID="revPassword" ControlToValidate="txtPassword"
                        Text="!" runat="server" ErrorMessage="Password only contain letters and numbers"
                        ValidationExpression="^[a-zA-Z0-9]*$" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"></asp:RegularExpressionValidator>
                </div>
                <div class="yt-Form-Field">
                    <label>
                        Confirm Password:</label>
                    <asp:TextBox ID="txtConfirmPassword" TextMode="Password" CssClass="yt-Form-Input"
                        runat="server"></asp:TextBox>
                    <asp:CompareValidator ID="cvConformPassword" runat="server" ControlToCompare="txtPassword"
                        ControlToValidate="txtConfirmPassword" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"
                        ErrorMessage="Your passwords did not match.">!</asp:CompareValidator>
                    <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="ValidateConformPassword"
                        ErrorMessage="Confirm Password is a required field." Font-Bold="True" Font-Size="Medium"
                        ForeColor="#FF8000" Width="1px">!</asp:CustomValidator>
                </div>
            </fieldset>
            <div class="yt-Form-Buttons">
                <div class="yt-Form-Submit">
                    <asp:LinkButton ID="lbtnSaveChanges" CssClass="yt-Button yt-CheckButton" runat="server"
                        OnClick="lbtnSaveChanges_Click">Save Changes</asp:LinkButton>
                </div>
            </div>
        </fieldset>
        <!--yt-Form-->
    </div>
    <!-- yt-BillingFormContainer -->
</asp:Content>
<asp:Content ID="modalcontent" ContentPlaceHolderID="ModalContentPlaceHolder" runat="Server">
</asp:Content>
