<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Shared/User.master"
    EnableEventValidation="true" CodeFile="UserRegistration.aspx.cs" Inherits="Users_UserRegistration"
    Title="Sign up" ValidateRequest="false" %>
<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>
<asp:Content ID="Content1" ContentPlaceHolderID="UserContentPlaceHolder" runat="server">

    <script language="javascript" type="text/javascript">
 function fb_logout() {
     FB.logout(function() {
         var url = location.href;
         // append a timestamp to make sure the url seems different to the browser
         url = (url + (url.indexOf('?') > -1 ? '&' : '?') + (new Date()).getTime());
         window.location.href = url;
     });
 }
     
 function ValidateTandCs(source, args)
 {
     args.IsValid = document.getElementById('<%= chkAgreeTermsUse.ClientID %>').checked;
     return args.IsValid;
 }

 function UserNameLength(source, args)
 {
    var UserName= document.getElementById('<%= txtUsername.ClientID %>');    
    args.IsValid=CheckUsernameLength(UserName.value);
 }
 function Clear()
 { 
    var required_validator = document.getElementById('<%= PortalValidationSummary.ClientID %>');
    var cvUsername = document.getElementById('<%= cvUsername.ClientID %>');
    required_validator.validationGroup = "UserName";
    cvUsername.validationGroup = "UserName";
    var lblErrMsg= document.getElementById('<%= lblErrMsg.ClientID %>'); 
 if(lblErrMsg)
 {
   lblErrMsg.innerHTML = '';
   lblErrMsg.style.visibility = 'hidden';  
   
 }
 }
 function SetValidation() {
     var lblErrMsg = document.getElementById('<%= lblErrMsg.ClientID %>');
     if (lblErrMsg) {
         lblErrMsg.innerHTML = '';
         lblErrMsg.style.visibility = 'hidden';
     }
    var required_validator = document.getElementById('<%= PortalValidationSummary.ClientID %>');
    var cvUsername = document.getElementById('<%= cvUsername.ClientID %>');
    required_validator.validationGroup = "";   
    cvUsername.validationGroup = "";
 } 
 
 
 
 function ValidatePasswordLength(source, args)
 {
    var password = document.getElementById('<%= txtPassword.ClientID %>');
    args.IsValid=CheckPasswordLength(password.value);
 } 
function HideIndecator()
{
 var error= document.getElementById('<%= errorVerification.ClientID %>'); 
 var lblErrMsg= document.getElementById('<%= lblErrMsg.ClientID %>'); 
 if(lblErrMsg)
 {
   lblErrMsg.innerHTML = '';
   lblErrMsg.style.visibility = 'hidden';  
   
 }
 
 if(error)
 {
   error.style.visibility = 'hidden';  
 }
}


function ValidatePhoneNumber(source, args)
{     
     var number1=  document.getElementById('<%= txtPhoneNumber1.ClientID %>');
     var number2=  document.getElementById('<%= txtPhoneNumber2.ClientID %>');
     var number3=  document.getElementById('<%= txtPhoneNumber3.ClientID %>');
     var validator=  document.getElementById('<%= cvPhoneNumber.ClientID %>');     
     
     args.IsValid= PhoneNumberValidate(number1,number2,number3,validator);        
}

function CheckUsernameLength(UserName)
{
    var bol=true;  
    if((UserName.length>=4)&&(UserName.length<=16))                   
    { 
       bol= true;
    }
    else 
    {
       bol= false;    
    }
   return bol;            
}
function reloadpage() {  
    window.location.reload();
}

function CheckPasswordLength(Password)
{   

  var bol=true;       
    if(Password.length!=0)
    {
        if((Password.length>=6)&&(Password.length<=50))               
           bol= true;             
        else 
          bol= false;               
    }
    else
    {
     bol=true;
    }
    return bol;
}
    </script>

    <div id="divShowModalPopup">
    </div>
    <div id="fb-root">
    </div>
    <div class="yt-ContentPrimaryContainer">
        <div class="yt-ContentPrimary">
            <div id="lblErrMsg" class="yt-Error" style="text-align: left" runat="server" visible="false">
            </div>
            <div style="text-align: left">
                <asp:ValidationSummary ID="PortalValidationSummary" runat="server" ForeColor="Black"
                    HeaderText="   <h2>Oops - there was a problem with your sign up.</h2>                                                             <h3>Please correct the errors below:</h3>"
                    Width="409px" CssClass="yt-Error"></asp:ValidationSummary>
            </div>
            <div class="yt-Panel-Primary" style="text-align: left">
                <h2>
                    Sign Up</h2>
                <%--class="yt-Form-Field yt-Form-Field-Radio"--%>
                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Always" RenderMode="Inline" runat="server">
                    <ContentTemplate>
                        <fieldset class="yt-Form">
                            <div class="yt-AccountTypeSelection">
                                <div class="yt-Form-Field yt-Form-Field-Radio">
                                    <asp:RadioButton ID="rdoPersonalAccount" TabIndex="200" runat="server" Text="Personal Account"
                                        AutoPostBack="True" OnCheckedChanged="rdoPersonalAccount_CheckedChanged" GroupName="rdoAccountType">
                                    </asp:RadioButton>
                                </div>
                                <div class="yt-Form-Field yt-Form-Field-Radio">
                                    <asp:RadioButton ID="rdoBusinessAccount" TabIndex="201" runat="server" Text="Business Account"
                                        AutoPostBack="True" OnCheckedChanged="rdoBusinessAccount_CheckedChanged" GroupName="rdoAccountType">
                                    </asp:RadioButton>
                                </div>
                                <div class="hack-clearBoth">
                                </div>
                            </div>
                            <div>
                                <div>
                                    <asp:Panel ID="pnlAccount" runat="server" BackColor="White" BorderStyle="None" Visible="False">
                                        <p style="width: 427px" id="lblPersonalAcctDesc" class="personal yt-AccountTypeDescription"
                                            runat="server">
                                            A personal account is for most users - you will be able to contribute to existing
                                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"]%>s
                                            as well as create your own.</p>
                                        <div runat="server" id="SignUpOptions">
                                            <div id="SignUpOptions-Container">
                                                <div id="SignUpConventional">
                                                    Sign up below</div>
                                                <span id="SignUpOptionsOr">-OR-</span> <span id="SignUpFacebookConnect">
                                                    <fb:login-button scope="email,user_about_me,user_location" onlogin="reloadpage();">
                                                        <fb:intl>Connect with Facebook</fb:intl>
                                                    </fb:login-button>
                                                </span>
                                            </div>
                                            <div class="hack-clearBoth">
                                            </div>
                                        </div>
                                        <p style="width: 427px" id="lblBusinessAcctDesc" class="business yt-AccountTypeDescription"
                                            runat="server">
                                            A business account is for those who might be creating
                                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"]%>s
                                            on behalf of clients. For example, funeral homes and event planners.</p>
                                        <p style="width: 427px" id="lblPerRequiredFields" class="yt-requiredFields" runat="server">
                                            <strong>Required fields are indicated with <em class="required">*</em> </strong>
                                        </p>
                                                                           
                                        <div class="yt-Form-Field">
                                            <asp:Panel ID="EmailPanel" runat="server" Width="427px">
                                                <asp:Literal ID="lblEmail" runat="server">
                                                </asp:Literal>
                                                <asp:TextBox ID="txtEmail" CssClass="yt-Form-Input-Long" runat="server" Width="250px"
                                                    MaxLength="250" TabIndex="1">
                                                </asp:TextBox>
                                                <asp:RegularExpressionValidator ID="rEvEmail" runat="server" ControlToValidate="txtEmail"
                                                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Font-Bold="True"
                                                    Font-Size="Medium" ForeColor="#FF8000">!</asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ForeColor="#FF8000" ControlToValidate="txtEmail"
                                                    Font-Bold="True" Font-Size="Medium">!</asp:RequiredFieldValidator>
                                                <div class="hint">
                                                    <asp:Label ID="CBEmail" runat="server">
                                                    </asp:Label>
                                                    <span class="hintPointer"></span>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div class="yt-Form-Field business">
                                            <asp:Panel ID="WebsitePanel" runat="server" Width="427px">
                                                <asp:Literal ID="lblWebsiteAddress" runat="server"></asp:Literal>
                                                <asp:TextBox ID="txtWebsiteAddress" CssClass="yt-Form-Input-Long" Text="http://"
                                                    runat="server" MaxLength="100" Width="250px" TabIndex="2"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="reWebsiteAddress" runat="server" Text="!" ControlToValidate="txtWebsiteAddress"
                                                    ErrorMessage="Not a valid website address " ValidationExpression="(http(s)?://)?([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?"
                                                    Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"></asp:RegularExpressionValidator>
                                                <div class="hint">
                                                    <asp:Label ID="CBWebsiteAddress" runat="server">
                                                    </asp:Label>
                                                    <span class="hintPointer"></span>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div class="yt-Form-Field">
                                            <asp:Panel ID="PanelUserName" runat="server" Width="427px">
                                                <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                                                    <ContentTemplate>
                                                        <asp:Literal ID="lblUsername" runat="server"></asp:Literal>
                                                        <asp:TextBox ID="txtUsername" runat="server" MaxLength="16" ValidationGroup="UserName"
                                                            TabIndex="3" CssClass="yt-Form-Input ">
                                                        </asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="reUsername" ControlToValidate="txtUsername" Text="!"
                                                            runat="server" ErrorMessage="Username only contain letters and numbers" ValidationExpression="^[a-zA-Z0-9]*$"
                                                            ValidationGroup="UserName" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"></asp:RegularExpressionValidator>
                                                        <asp:RegularExpressionValidator ID="reUsername2" ControlToValidate="txtUsername"
                                                            Text="!" runat="server" ErrorMessage="Username only contain letters and numbers"
                                                            ValidationExpression="^[a-zA-Z0-9]*$" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"></asp:RegularExpressionValidator>
                                                        <asp:CustomValidator ID="cvUsername" runat="server" ControlToValidate="txtUsername"
                                                            Text="!" ErrorMessage=" Your username must be between 4 to 16 characters" ClientValidationFunction="UserNameLength"
                                                            ValidationGroup="UserName" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"
                                                            Width="1px"></asp:CustomValidator>
                                                        <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="txtUsername"
                                                            Text="!" ForeColor="#FF8000" Font-Bold="True" Font-Size="Medium" Width="1px"></asp:RequiredFieldValidator>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUsername"
                                                            Text="!" ForeColor="#FF8000" Font-Bold="True" ErrorMessage="Username is a required field."
                                                            ValidationGroup="UserName" Font-Size="Medium" Width="1px"></asp:RequiredFieldValidator>
                                                        <asp:LinkButton ID="ImgBtnChkAvailabalaty" CssClass="yt-checkAvailability" runat="server"
                                                            OnClick="ImgBtnChkAvailabalaty_Click" ValidationGroup="UserName" OnClientClick="Clear();"
                                                            CausesValidation="true" Height="13px" TabIndex="4">Check Availability</asp:LinkButton>
                                                        <span class="availabilityNotice" id="Imgability" runat="server"></span>
                                                        <div class="hint">
                                                            <asp:Label ID="CBUsername" runat="server"></asp:Label>
                                                            <span class="hintPointer"></span>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </div>
                                        <div class="yt-Form-Field">
                                            <asp:Panel ID="PanelPassword" runat="server" CssClass="signup-Password">
                                                <asp:Literal ID="lblPassword" runat="server">
                                                </asp:Literal>
                                                <asp:TextBox ID="txtPassword" TabIndex="5" runat="server" Width="150px" CssClass="yt-Form-Input yt-Form-Input-Password"
                                                    MaxLength="16" TextMode="Password" OnPreRender="txtPassword_PreRender"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ForeColor="#FF8000" Width="1px"
                                                    Font-Size="Medium" Font-Bold="True" ControlToValidate="txtPassword">!</asp:RequiredFieldValidator>
                                                <asp:CustomValidator ID="cvPassword" runat="server" ForeColor="#FF8000" Width="1px"
                                                    Font-Size="Medium" Font-Bold="True" ErrorMessage="Password must be between 6 to 16 characters"
                                                    ClientValidationFunction="ValidatePasswordLength">!</asp:CustomValidator>
                                                <asp:RegularExpressionValidator ID="revPassword" runat="server" ForeColor="#FF8000"
                                                    Text="!" Font-Size="Medium" Font-Bold="True" ValidationExpression="^[a-zA-Z0-9]*$"
                                                    ControlToValidate="txtPassword" ErrorMessage="Password only contain letters and numbers"></asp:RegularExpressionValidator>
                                                <div class="hint">
                                                    <asp:Label ID="CBPassword" runat="server"></asp:Label>
                                                    <span class="hintPointer"></span>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div class="yt-Form-Field">
                                            <asp:Panel ID="PanelConfirmPassword" runat="server" CssClass="signup-Password">
                                                <asp:Literal ID="lblConfirmPassword" runat="server"></asp:Literal>
                                                <asp:TextBox ID="txtConfirmPassword" runat="server" Width="150px" MaxLength="16"
                                                    TextMode="Password" TabIndex="6" CssClass="yt-Form-Input yt-Form-Input-Password"></asp:TextBox>
                                                <asp:CompareValidator ID="cpvConfirmPassword" runat="server" Font-Bold="True" Font-Size="Medium"
                                                    ForeColor="#FF8000" Width="1px" ErrorMessage="Confirm Password is not same as password."
                                                    ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword">!</asp:CompareValidator>
                                                <asp:RequiredFieldValidator ErrorMessage="Confirm Password is a required field."
                                                    ID="rfvConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword"
                                                    ForeColor="#FF8000" Font-Bold="True" Font-Size="Medium" Width="1px">!</asp:RequiredFieldValidator>
                                                <div class="hint">
                                                    <asp:Label ID="CBConfirmPassword" runat="server"></asp:Label>
                                                    <span class="hintPointer"></span>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                        <div class="yt-Form-Field business">
                                            <asp:Panel ID="CompanyNamePanel" runat="server" Width="427px">
                                                <asp:Literal ID="lblBusinessName" runat="server"></asp:Literal>
                                                <asp:TextBox ID="txtBusinessName" runat="server" Width="250px" MaxLength="50" TabIndex="7"
                                                    CssClass="yt-Form-Input-Long"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvBusinessName" runat="server" ControlToValidate="txtBusinessName"
                                                    ErrorMessage="Business Name is a required field." Font-Bold="True" Font-Size="Medium"
                                                    ForeColor="#FF8000">!</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revBusinessName" ControlToValidate="txtBusinessName"
                                                    Text="!" runat="server" ErrorMessage="Business Name only contain letters,numbers,'-' and '#'"
                                                    ValidationExpression="^[a-zA-Z0-9,\#,\-.\s]*$" Font-Bold="True" Font-Size="Medium"
                                                    ForeColor="#FF8000"></asp:RegularExpressionValidator>
                                                <div class="hint">
                                                    <asp:Label ID="CBCompanyName" runat="server"></asp:Label>
                                                    <span class="hintPointer"></span>
                                                </div>
                                                <br />
                                            </asp:Panel>
                                        </div>
                                        <div class="yt-Form-Field personal">
                                            <asp:Panel ID="FirstNamePanel" runat="server" Width="427px">
                                                <asp:Literal ID="lblFirstName" runat="server">
                                                </asp:Literal>
                                                <asp:TextBox ID="txtFirstName" CssClass="yt-Form-Input-Long" runat="server" Width="250px"
                                                    MaxLength="50" TabIndex="8"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvFirstName" ForeColor="#FF8000" runat="server"
                                                    ControlToValidate="txtFirstName" Font-Bold="True" Font-Size="Medium" Width="1px">!</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revFirstName" ControlToValidate="txtFirstName"
                                                    Text="!" runat="server" ErrorMessage="First Name only contain letters,numbers,'-' and '#'"
                                                    ValidationExpression="^[a-zA-Z0-9,\#,\-,\s]*$" Font-Bold="True" Font-Size="Medium"
                                                    ForeColor="#FF8000"></asp:RegularExpressionValidator>
                                            </asp:Panel>
                                        </div>
                                        <div class="yt-Form-Field personal">
                                            <asp:Panel ID="LastNamePanel" runat="server" Width="427px">
                                                <asp:Literal ID="lblLastName" runat="server"></asp:Literal>
                                                <asp:TextBox ID="txtLastName" CssClass="yt-Form-Input-Long" runat="server" Width="250px"
                                                    MaxLength="50" TabIndex="9"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName"
                                                    ErrorMessage="Last Name is a required Field." ForeColor="#FF8000" Font-Bold="True"
                                                    Font-Size="Medium" Width="1px">!</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revLastName" ControlToValidate="txtLastName"
                                                    Text="!" runat="server" ErrorMessage="Last Name only contain letters,numbers,'-' and '#'"
                                                    ValidationExpression="^[a-zA-Z0-9,\#,\-.\s]*$" Font-Bold="True" Font-Size="Medium"
                                                    ForeColor="#FF8000"></asp:RegularExpressionValidator>
                                            </asp:Panel>
                                        </div>
                                        <div class="yt-Form-Field business">
                                            <asp:Panel ID="BusinessTypePanel" runat="server" Width="427px">
                                                <asp:Literal ID="lblBusinessType" runat="server"></asp:Literal>
                                                <asp:DropDownList ID="ddlBusinessType" runat="server" Width="258px" TabIndex="10">
                                                </asp:DropDownList>
                                            </asp:Panel>
                                        </div>
                                        <div class="yt-Form-Field business">
                                            <asp:Panel ID="StreetAddressPanel" runat="server" Width="427px">
                                                <asp:Literal ID="lblStreetAddress" runat="server"></asp:Literal>
                                                <asp:TextBox ID="txtStreetAddress" runat="server" Width="250px" MaxLength="100" TabIndex="11"
                                                    CssClass="yt-Form-Input-Long"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="revStreetAddress" ControlToValidate="txtStreetAddress"
                                                    Text="!" runat="server" ErrorMessage="Street Address only contain letters,numbers,'-' and '#'"
                                                    ValidationExpression="^[a-zA-Z0-9,\#,\-,\s\'\.]*$" Font-Bold="True" Font-Size="Medium"
                                                    ForeColor="#FF8000">
                                                </asp:RegularExpressionValidator>
                                            </asp:Panel>
                                        </div>       
                                        <div class="yt-Form-Field">                       
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="yt-Form-Field">
                                                        <asp:Literal ID="lblCountry" runat="server"></asp:Literal>
                                                        <asp:DropDownList ID="ddlCountry" runat="server" Width="258px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" TabIndex="12">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="yt-Form-Field">
                                                        <asp:Literal ID="lblStateProvince" runat="server"></asp:Literal>
                                                        <asp:DropDownList ID="ddlStateProvince" runat="server" Width="258px" TabIndex="13">
                                                        </asp:DropDownList>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            </div>
                                        <div class="yt-Form-Field">       
                                            <asp:Panel ID="CityPanel" runat="server" Width="427px">
                                                <asp:Literal ID="lblCity" runat="server"></asp:Literal>
                                                <asp:TextBox ID="txtCity" CssClass="yt-Form-Input-Long" runat="server" Width="250px"
                                                    MaxLength="50" TabIndex="14"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvtxtCity" runat="server" Text="!" Visible="false"
                                                    ControlToValidate="txtCity" ErrorMessage="City is a required field." Font-Bold="True"
                                                    Font-Size="Medium" ForeColor="#FF8000"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revtxtCity" ControlToValidate="txtCity" Text="!"
                                                    runat="server" ErrorMessage="City only contain letters,numbers,'-' and '#'" ValidationExpression="^[a-zA-Z0-9,\#,\-,\s\'\.]*$"
                                                    Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"></asp:RegularExpressionValidator>
                                            </asp:Panel>                                           
                                            
                                        </div>
                                        <div class="yt-Form-Field business">
                                            <asp:Panel ID="PanelPhone" runat="server" Width="427px">
                                                <asp:Literal ID="lblPhoneNumber" runat="server"></asp:Literal>
                                                (<asp:TextBox ID="txtPhoneNumber1" runat="server" Width="34px" MaxLength="3" TabIndex="15"
                                                    CssClass="yt-Form-Input-Long"></asp:TextBox>)
                                                <asp:TextBox ID="txtPhoneNumber2" runat="server" Width="34px" MaxLength="3" CssClass="yt-Form-Input-Long"
                                                    TabIndex="16"></asp:TextBox>
                                                -
                                                <asp:TextBox ID="txtPhoneNumber3" runat="server" Width="40px" MaxLength="4" CssClass="yt-Form-Input-Long"
                                                    TabIndex="17"></asp:TextBox>
                                                <asp:CustomValidator ID="cvPhoneNumber" runat="server" Font-Bold="True" Font-Size="Medium"
                                                    ForeColor="#FF8000" ErrorMessage="Enter valid phone number.Format should be XXX-XXX-XXXX."
                                                    ClientValidationFunction="ValidatePhoneNumber">!</asp:CustomValidator>
                                            </asp:Panel>
                                        </div>
                                        <div class="yt-Form-Field business">
                                            <asp:Panel ID="ZipCodePanel" runat="server" Width="427px">
                                                <asp:Literal ID="lblZipCode" runat="server"></asp:Literal>
                                                <asp:TextBox ID="txtZipCode" runat="server" Width="250px" MaxLength="16" TabIndex="18"
                                                    CssClass="yt-Form-Input-Long"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvZipCode" runat="server" ControlToValidate="txtZipCode"
                                                    Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000">!</asp:RequiredFieldValidator>
                                            </asp:Panel>
                                        </div>
                                        
                                    </asp:Panel>
                                    <!--yt-Form-->
                                </div>
                            </div>
                        </fieldset>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div>
                    <asp:Panel ID="RecaptchaPanel" runat="server" Width="427px">
                    <asp:label ID="lblPersonalRecaptcha" runat="server"></asp:label>
                        <recaptcha:RecaptchaControl ID="RecaptchaControl1" runat="server" PublicKey="6Lf4LwUAAAAAAAAgRboKdp8-Tt13HxY2KXsygFqM"
                            PrivateKey="6Lf4LwUAAAAAAGGMUulb2_d8-8syZUpFZzMkbabp" EnableTheming="true" Theme="clean"
                            CssClass="recaptchacss" />    
                            <asp:label ID="lblBusinessRecaptcha" runat="server"></asp:label>   
                    </asp:Panel>
                    <asp:Panel ID="CommonPanel" runat="server" Width="427px">
                        <div class="yt-Form-Field">
                            
                        </div>
                        <div class="yt-Form-Field yt-Form-Field-Checkbox">
                            <input type="checkbox" id="chkAgreeTermsUse" runat="server" tabindex="20" />
                            <label>
                                <em class="required">* </em>I have read and agree to the <a href="<%=Session["APP_BASE_DOMAIN"]%>termsofuse.aspx"
                                    target="_blank">terms of use</a> and the <a href="<%=Session["APP_BASE_DOMAIN"]%>privacy.aspx"
                                        target="_blank">privacy policy</a>. <asp:CustomValidator ID="cvAcceptPolicies" ForeColor="Red" runat="server" ClientValidationFunction="ValidateTandCs"
                                Width="1px">!</asp:CustomValidator></label>
                                        <span style="color: #FF8000; font-size: Medium; font-weight: bold;" id="errorVerification"
                                runat="server" visible="false">!</span>
                            
                        </div>
                        <div class="yt-Form-Field yt-Form-Field-Checkbox">
                            <input type="checkbox" id="chkAgreeReceiveNewsletters" runat="server" tabindex="21" />
                            <label id="SignupNewsLetter" runat="server">
                                I would like to receive periodic newsletters, including tips and tricks when creating
                                tributes,from Your Tribute.</label>                               
                        </div>
                    </asp:Panel>
                </div>
                <div class="yt-Form-Submit">
                    <asp:LinkButton ID="ImgBtnSignMe" TabIndex="22" OnClick="ImgBtnSignMe_Click" runat="server"
                        CssClass="yt-Button" OnClientClick="SetValidation();">OK, sign me up</asp:LinkButton>
                </div>
                <!--yt-ContentSecondary-->
            </div>
            <br />
        </div>
    </div>
    <div class="yt-ContentSecondary" style="text-align: left">
        <div class="yt-Panel-System">
            <h2>
                Benefits of Joining</h2>
            <div class="yt-Panel-Body">
                <p>
                    Becoming a registered member of Your
                    <%=ConfigurationManager.AppSettings["ApplicationWord"]%>
                    has advantages:</p>
                <ul>
                    <li><strong>You can contribute to existing
                        <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"]%>s
                        with photos, videos, gifts, and comments.</strong></li>
                    <li><strong>You can easily create your own
                        <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"]%>s
                        to celebrate all of life’s special occasions.</strong></li>
                </ul>
                <p class="iconTime">
                    Sign up is <strong>fast and easy</strong>, because there is no need for verification
                    or password changes - when you complete the form you can begin adding content.</p>
                <p class="iconSecure">
                    Becoming a member is also <strong>secure</strong> &ndash; your information is kept
                    private and will never be sold to a third party. Read our <a href="http://www.yourtribute.com/privacy.aspx">
                        privacy policy</a> for more details.</p>
            </div>
        </div>
    </div>
</asp:Content>
