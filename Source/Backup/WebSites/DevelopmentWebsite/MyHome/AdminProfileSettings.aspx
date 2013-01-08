<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminProfileSettings.aspx.cs"
    Inherits="MyHome_AdminProfileSettings" MasterPageFile="~/Shared/InnerSecure.master" %>

<%@ MasterType VirtualPath="~/Shared/InnerSecure.Master" %>
<%--<%@ Register Src="ucColorPicker.ascx" TagName="ucColorPicker" TagPrefix="uc1" %>
--%>
<asp:Content ID="header_script" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">

    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/scripts/jscolor/jscolor.js"></script>

    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/scripts/mootools.v1.11.js"></script>

    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/scripts/mooRainbow/mooRainbow.js"></script>

    <!-- mooRainbow Color Picker LHK -->
    <link rel="stylesheet" type="text/css" href="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/scripts/mooRainbow/mooRainbow.css" />

    <script type="text/javascript">  
    
    function update_user_is_connected(){
        var user_box = document.getElementById("ProfileSettingsFacebookConnect");
        user_box.innerHTML = "<a href='#' onclick=\"facebook_disconnect(); \" "+
          "class='fb_button fb_button_medium'> "+
          "<span id='RES_ID_fb_login_text' class='fb_button_text'>Disconnect</span> </a><br/>"+
//          user_box.innerHTML="<fb:login-button autologoutlink=\"true\" ><fb:intl>Disconnect</fb:intl></fb:login-button><br/>"+
              "Logged in to Facebook as: <fb:name class = 'FB_ElementReady' uid=loggedinuser useyou='false' ></fb:name>";

//<a class="fb_button fb_button_medium"><span class="fb_button_text"><fb:intl>Disconnect</fb:intl></span></a>

        header_user_is_connected();
        if ($('RES_ID_fb_login')) {
            $('RES_ID_fb_login').setStyle('display', 'none');
        }
        FB.XFBML.parse();
    }
    function update_user_is_not_connected(){
        var user_box = document.getElementById("ProfileSettingsFacebookConnect");
        //user_box.innerHTML = "<fb:login-button v=\"2\"  onlogin=\"fb_login_handler();\" autologoutlink=\"true\"><fb:intl>Connect with Facebook</fb:intl></fb:login-button>";
        user_box.innerHTML="<fb:login-button onlogin=\"fb_login_handler();\" length=\"long\" ><fb:intl>Connect with Facebook</fb:intl></fb:login-button>";
        //user_box.innerHTML = "<a href='#' onclick=\"fb_login_handler(); return false;\" "+
        //  "class='fbconnect_login_button FBConnectButton FBConnectButton_Medium'> <span id='RES_ID_fb_login_text' class='FBConnectButton_Text'>Connect with Facebook</span> </a>";

        header_user_is_not_connected();
        if ($('RES_ID_fb_login')) {
            $('RES_ID_fb_login').setStyle('display', '');
        }
        FB.XFBML.parse();
        //document.getElementById("ProfileSettingsFacebookStatus").innerHTML = ""; // temporarily
    }
    function ReceiveFacebookLoginProcessed(rValue) {
        document.getElementById('<%= ProfileSettingsFacebookStatus.ClientID %>').innerHTML = rValue;
    }
    function fb_login_handler() {
      //update_user_is_connected();
        //CallFacebookLoginProcess("connect_"+FB.Connect.get_loggedInUser());
      new Ajax("<%= Session["APP_BASE_DOMAIN"] %>AjaxLogin.aspx", {
	    method: 'get',
	    data: {"action":'facebookConnect',"source":'profileSettings'},
	    onComplete: function(rs){
	        res = Json.evaluate(rs);
	        document.getElementById('<%= ProfileSettingsFacebookStatus.ClientID %>').innerHTML=res.messageText;
	        if (res.refreshPage) {
	          location.href = '<%= Session["APP_BASE_DOMAIN"] %>log_in.aspx';
	        }	        
	    }
      }).request();
    };    
    
//        FB.Event.subscribe('auth.logout', function (response) {
//   //FB.logout(fb_disconnect_handler); 
//   //fb_disconnect_handler();    
//  window.location.reload();  
//}); 

function facebook_disconnect()
{
        FB.logout();
        setTimeout("fb_disconnect_handler()",2500);        
}
    function fb_disconnect_handler(){    
        //update_user_is_not_connected(); 


      new Ajax("<%= Session["APP_BASE_DOMAIN"] %>AjaxLogin.aspx", {
	    method: 'get',
	    data: {"action":'facebookDisconnect',"source":'profileSettings'},
	    onComplete: function(rs){
	        res = Json.evaluate(rs);
	        //document.getElementById('<%= ProfileSettingsFacebookStatus.ClientID %>').innerHTML=res.messageText;
	         window.location.reload();
	        if (res.refreshPage) {
	          window.location.reload();
	        }
	    }
      }).request();
        //CallFacebookLoginProcess("disconnect"); 
        window.location.reload();
    };

    /* NOTE: may want to move this to an external .js */
    
    InitForm = function() {
        $$('.availabilityNotice').each( function(a) {
		    a.innerHTML = '';
		    a.className = 'availabilityNotice';
	    });
    };
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
	    };
    	
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
    function ValidatePhoneNumber(source, args)
    {     
         var number1=  document.getElementById('<%= txtPhoneNumber1.ClientID %>');
         var number2=  document.getElementById('<%= txtPhoneNumber2.ClientID %>');
         var number3=  document.getElementById('<%= txtPhoneNumber3.ClientID %>');
         var validator=  document.getElementById('<%= cvPhoneNumber.ClientID %>');
         if((number1.value != "") || (number2.value != "")||(number3.value != ""))
         {
         args.IsValid= PhoneNumberValidate(number1,number2,number3,validator);        
         }
         else
         {
         return true;
         }
    }


    
    
    function SetImage(url) 
    {
        document.getElementById('<%=ImgUserImage.ClientID%>').src = url;
        document.getElementById('<%=hdnStoryImageURL.ClientID%>').value = url;
       
    }
    
    function SetImageURL(url) 
    {
        var Image =  document.getElementById('<%=ImgUserImage.ClientID%>');
        Image.src = url;
         document.getElementById('<%=hdnStoryImageURL.ClientID%>').value = url;
        //(document.getElementById('<%=hdnStoryImageURL.ClientID%>')).value = url;
        
        $('yt-ThumbSelection').injectInside('yt-ThumbContainer');
	    customModalBox.close();
    }
    

    </script>

</asp:Content>
<asp:Content ID="content" ContentPlaceHolderID="BodyContentPlaceHolder" runat="Server">
    <asp:UpdatePanel ID="updateMainPanel" runat="server" UpdateMode="conditional">
        <ContentTemplate>
            <asp:HiddenField ID="hdnStoryImageURL" runat="server" />
            <asp:HiddenField ID="hdnHeaderLogo" runat="server" />
            <div id="divShowModalPopup">
            </div>
            <div id="yt-ProfileFormContainer">
                <h4>
                    Profile Picture:</h4>
                <div class="yt-ItemThumb">
                    <div class="yt-PhotoField ">
                        <a class="yt-Thumb">
                            <img id="ImgUserImage" runat="server" src="../assets/images/bg_ProfilePhoto.gif"
                                alt="" width="119" style="height: 110px" />
                        </a><a href="javascript:void(0);" class="yt-MiniButton yt-UploadPhotoButton" onclick="chooseThumb();">
                            Choose Photo</a> <a href="javascript:void(0);" class="yt-MiniButton yt-UploadPhotoButton"
                                onclick="uploadUserPhoto();">Upload photo</a>
                    </div>
                    <div id="ProfileSettingsFacebookConnect">
                    </div>
                </div>
                <fieldset class="yt-Form">
                    <div id="ProfileSettingsFacebookStatus" runat="server">
                    </div>
                    <h4>
                        Profile Information:</h4>
                    <p class="yt-requiredFields">
                        <strong>Required fields are indicated with <em class="required">*</em></strong></p>
                    <div class="yt-Form-Field">
                        <label>
                            <em class="required">* </em>Username:</label>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="yt-Form-Input yt-Disabled"
                            Enabled="False"></asp:TextBox>
                    </div>
                    <div class="yt-Form-Field business">
                        <asp:Panel ID="CompanyNamePanel" runat="server">
                            <label>
                                <em class="required">* </em>Business Name:</label>
                            <asp:TextBox ID="txtBusinessName" runat="server" CssClass="yt-Form-Input-Long" MaxLength="50"
                                TabIndex="1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvBusinessName" runat="server" ControlToValidate="txtBusinessName"
                                ErrorMessage="Business name is a required field." Font-Bold="True" Font-Size="Medium"
                                ForeColor="#FF8000">!</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revBusinessName" ControlToValidate="txtBusinessName"
                                Text="!" runat="server" ErrorMessage="Business name only contain letters,numbers,'-' ans '#'"
                                ValidationExpression="^[a-zA-Z0-9,\#,\-,\s]*$" Font-Bold="True" Font-Size="Medium"
                                ForeColor="#FF8000"></asp:RegularExpressionValidator>
                        </asp:Panel>
                    </div>
                    <div class="yt-Form-Field personal">
                        <asp:Panel ID="FirstNamePanel" runat="server">
                            <label>
                                <em class="required">* </em>First Name:</label>
                            <asp:TextBox ID="txtFirstName" CssClass="yt-Form-Input-Long" runat="server" MaxLength="50"
                                TabIndex="2"></asp:TextBox>&nbsp;
                            <asp:RequiredFieldValidator ID="rfvFirstName" ForeColor="#FF8000" runat="server"
                                ControlToValidate="txtFirstName" Font-Bold="True" Font-Size="Medium" Width="1px"
                                ErrorMessage="First name is a required field.">!</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revFirstName" ControlToValidate="txtFirstName"
                                Text="!" runat="server" ErrorMessage="First name only contain letters,numbers,'-' ans '#'"
                                ValidationExpression="^[a-zA-Z0-9,\#,\-,\s]*$" Font-Bold="True" Font-Size="Medium"
                                ForeColor="#FF8000"></asp:RegularExpressionValidator>
                        </asp:Panel>
                    </div>
                    <div class="yt-Form-Field personal">
                        <asp:Panel ID="LastNamePanel" runat="server">
                            <label>
                                <em class="required">* </em>Last Name:</label>
                            <asp:TextBox ID="txtLastName" CssClass="yt-Form-Input-Long" runat="server" MaxLength="50"
                                TabIndex="3"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName"
                                ErrorMessage="Last name is a required Field." ForeColor="#FF8000" Font-Bold="True"
                                Font-Size="Medium" Width="1px">!</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revLastName" ControlToValidate="txtLastName"
                                Text="!" runat="server" ErrorMessage="Last name only contain letters,numbers,'-' ans '#'"
                                ValidationExpression="^[a-zA-Z0-9,\#,\-,\s]*$" Font-Bold="True" Font-Size="Medium"
                                ForeColor="#FF8000"></asp:RegularExpressionValidator>
                        </asp:Panel>
                    </div>
                    <div class="yt-Form-Field business">
                        <asp:Panel ID="BusinessTypePanel" runat="server">
                            <label>
                                <em class="required">* </em>Business Type:</label>
                            <asp:DropDownList ID="ddlBusinessType" CssClass="yt-Form-DropDown-Long" runat="server"
                                TabIndex="4">
                            </asp:DropDownList>
                        </asp:Panel>
                    </div>
                    <div class="yt-Form-Field business">
                        <asp:Panel ID="StreetAddressPanel" runat="server">
                            <label>
                                Street Address:</label>
                            <asp:TextBox ID="txtStreetAddress" runat="server" MaxLength="50" CssClass="yt-Form-Input-Long"
                                TabIndex="5"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revStreetAddress" ControlToValidate="txtStreetAddress"
                                Text="!" runat="server" ErrorMessage="Street address only contain letters,numbers,'-' ans '#'"
                                ValidationExpression="^[a-zA-Z0-9,\#,\-,\s]*$" Font-Bold="True" Font-Size="Medium"
                                ForeColor="#FF8000"></asp:RegularExpressionValidator>
                            <asp:CheckBox ID="ChkBoxIsAddressOn" runat="server" Text="Show in the custom header?"
                                class="inlineCheckbox" />
                        </asp:Panel>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="yt-Form-Field">
                                <label>
                                    <em class="required">* </em>Country:</label>
                                <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"
                                    CssClass="yt-Form-DropDown-Long" TabIndex="6">
                                </asp:DropDownList>
                            </div>
                            <div class="yt-Form-Field">
                                <label>
                                    State/Province:</label>
                                <asp:DropDownList ID="ddlStateProvince" runat="server" CssClass="yt-Form-DropDown-Long"
                                    TabIndex="7">
                                </asp:DropDownList>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="yt-Form-Field">
                        <asp:Panel ID="CityPanel" runat="server">
                            <asp:Literal ID="lblCity" runat="server"></asp:Literal>
                            <asp:TextBox ID="txtCity" CssClass="yt-Form-Input-Long" runat="server" MaxLength="50"
                                TabIndex="8"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvtxtCity" runat="server" Text="!" ControlToValidate="txtCity"
                                ErrorMessage="City is a required field." Font-Bold="True" Font-Size="Medium"
                                ForeColor="#FF8000" Visible="False"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtCity" ControlToValidate="txtCity" Text="!"
                                runat="server" ErrorMessage="City only contain letters,numbers,'-' ans '#'" ValidationExpression="^[a-zA-Z0-9,\#,\-,\s]*$"
                                Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"></asp:RegularExpressionValidator>
                        </asp:Panel>
                    </div>
                    <div class="yt-Form-Field business">
                        <asp:Panel ID="ZipCodePanel" runat="server">
                            <label>
                                <em class="required">* </em>ZipCode :</label>
                            <asp:TextBox ID="txtZipCode" runat="server" MaxLength="16" CssClass="yt-Form-Input-Long"
                                TabIndex="9"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvZipCode" runat="server" ControlToValidate="txtZipCode"
                                Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ErrorMessage="Zip code is a required field.">!</asp:RequiredFieldValidator>
                        </asp:Panel>
                    </div>
                    <div class="yt-Form-Field business">
                        <asp:Panel ID="WebsitePanel" runat="server">
                            <label>
                                Website Address :</label>
                            <asp:TextBox ID="txtWebsiteAddress" CssClass="yt-Form-Input-Long" Text="http://"
                                runat="server" MaxLength="250" TabIndex="10"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="reWebsiteAddress" runat="server" Text="!" ControlToValidate="txtWebsiteAddress"
                                ErrorMessage="Please enter a valid website address " ValidationExpression="(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?"
                                Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"></asp:RegularExpressionValidator>
                            <asp:CheckBox ID="ChkBoxIsWebAddressOn" runat="server" Text="Show in the custom header?"
                                class="inlineCheckbox" />
                        </asp:Panel>
                    </div>
                    <div class="yt-Form-Field business">
                        <asp:Panel ID="PanelPhone" runat="server">
                            <label>
                                Phone Number :</label>
                            (<asp:TextBox ID="txtPhoneNumber1" runat="server" Width="34px" MaxLength="3" TabIndex="11"
                                CssClass="yt-Form-Input-Long"></asp:TextBox>)
                            <asp:TextBox ID="txtPhoneNumber2" runat="server" Width="34px" MaxLength="3" CssClass="yt-Form-Input-Long"
                                TabIndex="12"></asp:TextBox>
                            -
                            <asp:TextBox ID="txtPhoneNumber3" runat="server" Width="40px" MaxLength="4" CssClass="yt-Form-Input-Long"
                                TabIndex="13"></asp:TextBox>
                            <asp:CustomValidator ID="cvPhoneNumber" runat="server" Font-Bold="True" Font-Size="Medium"
                                ForeColor="#FF8000" ErrorMessage="Enter valid phone number.Format should be XXX-XXX-XXXX."
                                ClientValidationFunction="ValidatePhoneNumber">!</asp:CustomValidator>
                            <asp:CheckBox ID="ChkBoxIsPhoneNoOn" runat="server" Text="Show in the custom header?"
                                class="inlineCheckbox" />
                        </asp:Panel>
                    </div>
                    <div class="yt-Form-Field business">
                        <asp:Panel ID="ObPagePanel" runat="server">
                            <label>
                                Link to Obituary Page:</label>
                            <asp:TextBox ID="txtObPage" CssClass="yt-Form-Input-Long" Text="http://" runat="server"
                                MaxLength="250" TabIndex="14"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Text="!"
                                ControlToValidate="txtObPage" ErrorMessage="Please enter a valid Obituary Page address "
                                ValidationExpression="(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?" Font-Bold="True"
                                Font-Size="Medium" ForeColor="#FF8000"></asp:RegularExpressionValidator>
                            <asp:CheckBox ID="ChkBoxIsObUrlLinkOn" runat="server" Text="Show in the custom header?"
                                class="inlineCheckbox" />
                        </asp:Panel>
                    </div>
                    <!--LHK: for Custom Header-->
                    <div class="yt-Form-Field business" id="divCustomHeader" runat="server">
                        <label>
                            <h4>
                                Custom Header</h4>
                        </label>
                        <label>
                            <asp:CheckBox ID="ChkBoxDisplayCustomHeader" CssClass="chkBoxCustomHeader" runat="server"
                                Text="Check to display the custom header?" />
                            <br />
                            <br />
                            Upload Logo for Header:
                        </label>
                        <asp:FileUpload ID="FileUploadHeaderLogo" CssClass="yt-Form-Input-Long" runat="server"
                            size="45" />
                        <br />
                        *Logo must be a JPG or GIF. The Recommended dimensions are 250 x 100 pixels.
                        <label>
                            <br />
                            Header Background Color:
                        </label>
                        <p>
                            Enter a HEX value:
                            <asp:TextBox ID="colorPickerSpan" runat="server" Style="text-transform: uppercase"
                                Height="22px" Width="60px" Text="F8F1EB" MaxLength="6" />
                            &nbsp;Or, click to choose a color:
                            <input id="txtColorPicker" runat="server" class="colorPicker" onchange="document.getElementById('ctl00_BodyContentPlaceHolder_colorPickerSpan').value = this.color;"
                                style="text-indent: -99px; border: 1px solid #c8c8c8; width: 60px" />
                        </p>
                    </div>
                    <div class="yt-Form-Buttons">
                        <div class="yt-Form-Submit">
                            <asp:LinkButton ID="lbtnSaveChanges" CssClass="yt-Button yt-CheckButton" runat="server"
                                OnClick="lbtnSaveChanges_Click" TabIndex="14">Save Changes</asp:LinkButton>
                        </div>
                    </div>
                    <!--yt-SignUpFormContainer-->
                </fieldset>
                <!--yt-Form-->
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lbtnSaveChanges" />
        </Triggers>
    </asp:UpdatePanel>
    <!-- yt-BillingFormContainer -->
</asp:Content>
<asp:Content ID="modalcontent" ContentPlaceHolderID="ModalContentPlaceHolder" runat="Server">
    <div id="yt-ThumbContainer">
        <div id="yt-ThumbSelection" class="yt-Panel-Primary">
            <h2>
                Choose an image</h2>
            <div class="yt-ThumbList">
                <p class="yt-Bullet">
                    Select an image for your event:</p>
                <ul class="yt-ListBody">
                    <asp:Repeater ID="repImage" runat="server" OnItemDataBound="repImage_ItemDataBound">
                        <ItemTemplate>
                            <li class="yt-Form-Field">
                                <label class="yt-Thumb">
                                    <asp:Image ID="imgImageList" runat="server" ImageUrl='<%#Eval("ImageUrl")%>' />
                                </label>
                                <asp:RadioButton ID="rdoImage" runat="server" />
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
