<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserProfile.aspx.cs" Inherits="Users_UserProfile"
    Title="UserProfile" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/default.css" />
    <!-- default grid layout stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/layouts/centered1024_12.css" />
    <!-- print-friendly stylesheet   -->
    <link rel="stylesheet" type="text/css" media="print" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/print.css" />
    <!-- screen elements stylesheet should be here -->
    <link rel="stylesheet" type="text/css" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/screen.css" />
    <!-- larger text stylesheets -->
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/medium_text.css"
        title="medium_text" />
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/large_text.css"
        title="large_text" />

    <script type="text/javascript" src="../assets/scripts/mootools-1.11.js"></script>

    <script type="text/javascript" src="../assets/scripts/global.js"></script>

    <script type="text/javascript" src="../assets/scripts/styleSwitcher.js"></script>

    <script language="javascript" type="text/javascript">
 function ImageValidation() 
 { 
    var imagul= document.getElementById('<%=filMyFile.ClientID %>');               
    if(imagul.value.length<=0)
    {    
       return false;       
    }    
    else
    {      
     return true;
    }    
 }
    function ChangeEmailPassword(source, args)
    {
      var email=document.getElementById('<%=txtEmail.ClientID %>');               
      var password=document.getElementById('<%=txtPassword.ClientID %>');  
      alert(email.value+password.value);
      if((email.value.length==0)&&(password.value.length==0))
      {
        args.IsValid= false;
        }
      else
        {
         args.IsValid= true;
        }
    }
    
    function confirmSubmit()
    {
        //
        
        var msg="Delete Credit Card - ErrorYou have chosen to automatically delete your credit card information – but you arecurrently set to renew 2"+<%= ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() %>+" on a yearly basis, which requires that you to have your credit card info saved. If you choose to delete, your automatically renewing "+<%= ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString() %>+" no longer automatically renew!"
        var agree=confirm(msg);
        if (agree)
	        return true ;
        else
	    return false ;
    }

function Summery()
{

}

    </script>
    <!--#include file="../analytics.asp"-->
</head>
<body>
    <form id="form1" runat="server">
    <div id="divShowModalPopup"></div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table style="width: 903px; background-color:#ffffff" >
            <tr>
                <td>
                    <asp:Button ID="btnProfileSettings" runat="server" Text="Profile Settings" Width="140px"
                        ToolTip="Profile Settings" OnClick="btnProfileSettings_Click" CausesValidation="False" />
                    <asp:Button ID="btnPrivacySettings" runat="server" Text="Privacy Settings" Width="140px"
                        ToolTip="Privacy Settings" OnClick="btnPrivacySettings_Click" CausesValidation="False" />
                    <asp:Button ID="btnChangeEmailPassword" runat="server" Text="Change Email/Password"
                        Width="168px" OnClick="btnChangeEmailPassword_Click" ToolTip="Change Email/Password"
                        CausesValidation="False" />
                    <asp:Button ID="btnEmailNotification" runat="server" Text="Email Notification Preferences"
                        Width="189px" OnClick="btnEmailNotification_Click" CausesValidation="False" />
                    <asp:Button ID="btnBillingInformation" runat="server" Text="Billing Information"
                        Width="140px" ToolTip="Billing Information" OnClick="btnBillingInformation_Click"
                        CausesValidation="False" />
            </tr>
            <tr>
                <td>
                    <div style="text-align:left ">
                        <asp:ValidationSummary CssClass="yt-Error" ID="PortalValidationSummary" runat="server"
                            Width="837px" HeaderText=" <h2>Oops - there was a problem with your sign up.</h2>                                                             <h3>Please correct the errors below:</h3>"
                            ForeColor="Black" />
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 895px">
                        <tr>
                            <td align="center">
                                <asp:MultiView ID="mvProfile" runat="server">
                                    <asp:View ID="tbProfileSettings" runat="server">
                                        <asp:Panel ID="pnlMainPanel" runat="server" Visible="true" BackColor="White" Height="1100px">
                                            <div>
                                                <br />
                                                <!--div class="hack-clearBoth"></div-->
                                                <fieldset class="yt-Form">
                                                    <asp:Panel ID="pnlAccount" runat="server" Width="408px" Visible="true" BorderStyle="None"
                                                        BackColor="White" Height="934px">
                                                        <div class="yt-Form-Field">
                                                            <asp:Panel ID="PicturePanel" runat="server" Width="427px">
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 215px">
                                                                            <asp:Label ID="lblProfilePicture" runat="server">Profile Picture</asp:Label>
                                                                            <br />
                                                                            <br />
                                                                        </td>
                                                                        <td style="width: 198px">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 215px">
                                                                            <asp:Image ID="imgUserImage" runat="server" Width="130px" Height="130px" />
                                                                            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Select Image from before Upload"
                                                                                ClientValidationFunction="ImageValidation">!</asp:CustomValidator>
                                                                            <br />
                                                                            <asp:FileUpload ID="filMyFile" runat="server" /></td>
                                                                        <td style="width: 198px" valign="bottom">
                                                                            &nbsp;<asp:Button ID="btnUpload" runat="server" CausesValidation="False" OnClick="btnUpload_Click"
                                                                                Text="Upload Photo" ToolTip="Upload Photo" /></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 215px">
                                                                            <asp:Label ID="lblProfileInfo" runat="server" Font-Bold="True"></asp:Label>
                                                                        </td>
                                                                        <td style="width: 198px">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </div>
                                                        <div class="yt-Form-Field ">
                                                            <asp:Panel ID="UsernamePanel" runat="server" Width="427px">
                                                                <em class="required">* </em>
                                                                <asp:Label ID="lblUsername" runat="server"></asp:Label><br />
                                                                <asp:TextBox ID="txtUsername" runat="server" MaxLength="100" Width="150px" Enabled="False"></asp:TextBox>
                                                            </asp:Panel>
                                                        </div>
                                                        <div class="yt-Form-Field business">
                                                            <asp:Panel ID="CompanyNamePanel" runat="server" Width="427px">
                                                                <em class="required">* </em>
                                                                <asp:Label ID="lblBusinessName" runat="server"></asp:Label>
                                                                <br />
                                                                &nbsp;<asp:TextBox ID="txtBusinessName" runat="server" Width="250px" MaxLength="255"></asp:TextBox>
                                                                <div class="hint">
                                                                    <asp:Label ID="CBCompanyName" runat="server"></asp:Label>
                                                                    <span class="hintPointer"></span>
                                                                </div>
                                                                <br />
                                                            </asp:Panel>
                                                        </div>
                                                        <div class="yt-Form-Field personal">
                                                            <asp:Panel ID="FirstNamePanel" runat="server" Width="427px">
                                                                <em class="required">* </em>
                                                                <asp:Label ID="lblFirstName" runat="server"></asp:Label>
                                                                <br />
                                                                &nbsp;<asp:TextBox ID="txtFirstName" CssClass="yt-Form-Input-Long" runat="server"
                                                                    Width="250px" MaxLength="250"></asp:TextBox>&nbsp;
                                                                <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName">!</asp:RequiredFieldValidator>
                                                                <div class="hint">
                                                                    <asp:Label ID="CBFirstName" runat="server"></asp:Label>
                                                                    <span class="hintPointer"></span>
                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                        <div class="yt-Form-Field personal">
                                                            <asp:Panel ID="LastNamePanel" runat="server" Width="427px">
                                                                <em class="required">* </em>
                                                                <asp:Label ID="lblLastName" runat="server"></asp:Label>
                                                                <br />
                                                                &nbsp;<asp:TextBox ID="txtLastName" CssClass="yt-Form-Input-Long" runat="server"
                                                                    Width="250px" MaxLength="250"></asp:TextBox>
                                                                <div class="hint">
                                                                    <asp:Label ID="CBLastName" runat="server"></asp:Label>
                                                                    <span class="hintPointer"></span>
                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                        <div class="yt-Form-Field business">
                                                            <asp:Panel ID="BusinessTypePanel" runat="server" Width="425px">
                                                                <em class="required">* </em>
                                                                <asp:Label ID="lblBusinessType" runat="server"></asp:Label><br />
                                                                &nbsp;<asp:DropDownList ID="ddlBusinessType" CssClass="yt-Form-DropDown-Long" runat="server"
                                                                    Width="250px">
                                                                </asp:DropDownList>
                                                            </asp:Panel>
                                                        </div>
                                                        <div class="yt-Form-Field business">
                                                            <asp:Panel ID="StreetAddressPanel" runat="server" Width="427px">
                                                                <br />
                                                                <em class="required">* </em>
                                                                <asp:Label ID="lblStreetAddress" runat="server"></asp:Label>
                                                                <br />
                                                                &nbsp;<asp:TextBox ID="txtStreetAddress" runat="server" Width="250px" MaxLength="100"></asp:TextBox>
                                                                <div class="hint">
                                                                    <asp:Label ID="CBStreetAddress" runat="server"></asp:Label>
                                                                    <span class="hintPointer"></span>
                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                        <div class="yt-Form-Field">
                                                            <br />
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                <ContentTemplate>
                                                                    <em class="required">*</em>
                                                                    <asp:Label ID="lblCountry" runat="server"></asp:Label>
                                                                    <br />
                                                                    <asp:DropDownList ID="ddlCountry" runat="server" Width="250px" AutoPostBack="True"
                                                                        OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <br />
                                                                    <br />
                                                                    <em class="required">*</em>
                                                                    <asp:Label ID="lblStateProvince" runat="server"></asp:Label>
                                                                    <br />
                                                                    <asp:DropDownList ID="ddlStateProvince" runat="server" Width="250px">
                                                                    </asp:DropDownList>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                        <div class="yt-Form-Field">
                                                            <br />
                                                            <asp:Panel ID="CityPanel" runat="server" Width="427px">
                                                                <em class="required">* </em>
                                                                <asp:Label ID="lblCity" runat="server"></asp:Label>
                                                                <br />
                                                                <asp:TextBox ID="txtCity" CssClass="yt-Form-Input-Long" runat="server" Width="250px"
                                                                    MaxLength="50"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="txtCity">!</asp:RequiredFieldValidator>
                                                                <div class="hint">
                                                                    <asp:Label ID="CBCity" runat="server"></asp:Label>
                                                                    <span class="hintPointer"></span>
                                                                </div>
                                                            </asp:Panel>
                                                        </div>
                                                        <div class="yt-Form-Field business">
                                                            <asp:Panel ID="ZipCodePanel" runat="server" Width="427px">
                                                                <em class="required">* </em>
                                                                <asp:Label ID="lblZipCode" runat="server"></asp:Label><br />
                                                                <asp:TextBox ID="txtZipCode" runat="server" Width="250px" MaxLength="20"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvZipCode" runat="server" ControlToValidate="txtZipCode">!</asp:RequiredFieldValidator>
                                                                <div class="hint">
                                                                    <asp:Label ID="CBZipCode" runat="server"></asp:Label><span class="hintPointer"></span></div>
                                                                <br />
                                                            </asp:Panel>
                                                        </div>
                                                        <div class="yt-Form-Field business">
                                                            <asp:Panel ID="WebsitePanel" runat="server" Width="427px">
                                                                <asp:Label ID="lblWebsiteAddress" runat="server"></asp:Label>
                                                                <br />
                                                                <asp:TextBox ID="txtWebsiteAddress" CssClass="yt-Form-Input-Long" Text="http://"
                                                                    runat="server" MaxLength="250"></asp:TextBox>
                                                                <div class="hint">
                                                                    <asp:Label ID="CBWebsiteAddress" runat="server"></asp:Label>
                                                                    <span class="hintPointer"></span>
                                                                </div>
                                                                <br />
                                                                <br />
                                                                <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                                                <br />
                                                                <asp:TextBox ID="txtPhone" CssClass="yt-Form-Input-Long" runat="server" MaxLength="50"></asp:TextBox>
                                                                <asp:RegularExpressionValidator ID="revPhoneNumber" runat="server" ControlToValidate="txtPhone"
                                                                    ErrorMessage="Enter Numeric values only." ValidationExpression="^[0-9]+$">!</asp:RegularExpressionValidator></asp:Panel>
                                                        </div>
                                                        <div>
                                                            <asp:Button ID="btnSave" runat="server" Text="Save Changes" OnClick="btnSave_Click" />
                                                        </div>
                                                    </asp:Panel>
                                                </fieldset>
                                                <!--yt-Form-->
                                            </div>
                                        </asp:Panel>
                                    </asp:View>
                                    <asp:View ID="tbPrivacySettings" runat="server">
                                        <table style="width: 872px">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblPrivacySettings" runat="server" Font-Bold="True" Text="Privacy Settings"
                                                        ToolTip="Privacy Settings" Width="160px"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PrivacySettingsPanel" runat="server" Width="700px">
                                                        <table>
                                                            <tr>
                                                                <td style="width: 693px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 693px">
                                                                    <asp:Label ID="lblNameSettings" runat="server" Text="NAME SETTINGS" ToolTip="NAME SETTINGS"
                                                                        Width="113px"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 693px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 693px">
                                                                    <asp:RadioButton ID="rdbDisplayMyFullName" runat="server" GroupName="NameSettings"
                                                                        Text="Display My Full Name" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 693px">
                                                                    <asp:Label ID="lblFullName" runat="server" ></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 693px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 693px">
                                                                    <asp:RadioButton ID="rdbDisplayMyUsername" runat="server" GroupName="NameSettings"
                                                                        Text="Display My Username" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 693px">
                                                                    <asp:Label ID="Label1" runat="server" ></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 693px; height: 25px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 693px;">
                                                                    <asp:Label ID="lblLOCATIONSETTINGS" runat="server" Text="LOCATION SETTINGS" ToolTip="LOCATION SETTINGS"
                                                                        Width="138px"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 693px;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 693px;">
                                                                    <asp:RadioButton ID="rdbDisplayMyLocation" runat="server" GroupName="LocationSettings"
                                                                        Text="Display My Location" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 693px">
                                                                    <asp:Label ID="lblDisplayLocation" runat="server" ></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 693px;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 693px;">
                                                                    <asp:RadioButton ID="rdbHideMyLocation" runat="server" GroupName="LocationSettings"
                                                                        OnCheckedChanged="rdbHideMyLocation_CheckedChanged" Text="Hide My Location" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 693px;">
                                                                    <asp:Label ID="lblHideLocation" runat="server" ></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 693px; height: 25px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 693px;">
                                                                    <asp:Label ID="lblMESSAGESETTINGS" runat="server" Text="MESSAGE SETTINGS" ToolTip="MESSAGE SETTINGS"
                                                                        Width="138px"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 693px;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 693px;">
                                                                    <asp:RadioButton ID="rdbAllowUsers" runat="server" GroupName="MESSAGESETTINGS" OnCheckedChanged="rdbHideMyLocation_CheckedChanged"
                                                                        Text="Allow Users To Send Me Messages" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 693px;">
                                                                    <asp:Label ID="lblAllowUser" runat="server" ></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 693px;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 693px;">
                                                                    <asp:RadioButton ID="rdbDoNotAllow" runat="server" GroupName="MESSAGESETTINGS" OnCheckedChanged="rdbHideMyLocation_CheckedChanged"
                                                                        Text="Do Not Allow Users To Send Me Messages" /></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 693px;">
                                                                    <asp:Label ID="lblNotAllowed" runat="server" Text="Users will not be able to send you private messages."></asp:Label></td>
                                                            </tr>
                                                        </table>
                                                        <asp:Button ID="btnSaveChanges" runat="server" Text="Save Changes" OnClick="btnSaveChanges_Click"
                                                            ToolTip="Save Changes" />
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:View>
                                    <asp:View ID="tbChangeEmailPassword" runat="server">
                                        <asp:Panel ID="pnlEmailPassword" BackColor="white" runat="server" Width="365px">
                                            <div>
                                                <table style="width: 305px">
                                                    <tr>
                                                        <td align="left" colspan="2">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="left">
                                                            <asp:Label ID="lblEmailTitle" runat="server" Width="117px" Font-Bold="True"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="left">
                                                            *<asp:Label ID="lblEmail" runat="server" Font-Size="Small"></asp:Label>&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="left">
                                                            <asp:TextBox ID="txtEmail" runat="server" Width="227px"></asp:TextBox>&nbsp;
                                                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                                                                ErrorMessage="Invalid Email Address." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">!</asp:RegularExpressionValidator></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <br />
                                            <div>
                                                <table style="width: 313px">
                                                    <tr>
                                                        <td colspan="2" align="left">
                                                            <asp:Label ID="lblPasswordTitle" runat="server" Font-Bold="True"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="left">
                                                            *<asp:Label ID="lblPassword" runat="server" Font-Size="Small"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="left">
                                                            <asp:TextBox ID="txtPassword" runat="server" Width="226px" TextMode="Password"></asp:TextBox>
                                                            <asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="Enter Email or Password to change."
                                                                ClientValidationFunction="ChangeEmailPassword">!</asp:CustomValidator></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="left">
                                                            *<asp:Label ID="lblConformPassword" runat="server" Font-Size="Small"></asp:Label>
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="left">
                                                            <asp:TextBox ID="txtConformPassword" runat="server" Width="223px" TextMode="Password"></asp:TextBox>&nbsp;
                                                            <asp:CompareValidator ID="cvConformPassword" runat="server" ControlToCompare="txtPassword"
                                                                ControlToValidate="txtConformPassword" ErrorMessage="Conform password is not same as password">!</asp:CompareValidator></td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <asp:Button ID="btnSubmitEmailPassword" runat="server" OnClick="btnSubmit_Click"
                                                Width="127px" /></asp:Panel>
                                    </asp:View>
                                    <asp:View ID="tbEmailNotification" runat="server">
                                        <div  class="yt-ContentPrimaryContainer" style="width: 98%; text-align:left ">
                                            <asp:Panel ID="PanelEmailNotification" runat="server">
                                                <div class="yt-Panel-Primary">
                                                    <table>
                                                        <tr>
                                                            <td valign="top" style="width: 721px">
                                                                <div>
                                                                    &nbsp;</div>
                                                                <table class="yt-Error" style="width: 821px">
                                                                    <tr style="width: 111px">
                                                                        <td>
                                                                            <asp:Label ID="Label2" runat="server" Text="Email Notification Preferences" Width="294px"></asp:Label></td>
                                                                    </tr>
                                                                    <tr style="width: 111px">
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="width: 111px">
                                                                        <td style="height: 18px">
                                                                            <asp:Label ID="Label3" runat="server" Font-Bold="True" 
                                                                                Width="294px"></asp:Label></td>
                                                                    </tr>
                                                                    <tr style="width: 111px">
                                                                        <td>
                                                                            <asp:Label ID="Label4" runat="server" ></asp:Label></td>
                                                                    </tr>
                                                                    <tr style="width: 111px">
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="width: 111px">
                                                                        <td style="height: 24px">
                                                                            <asp:CheckBox ID="cbSTORY" runat="server" Text="STORY – Email me when the story is updated." /><br />
                                                                            <asp:CheckBox ID="cbNOTES" runat="server" Text="NOTES – Email me when a new note is added." /><br />
                                                                            <asp:CheckBox ID="cbEVENTS" runat="server" Text="EVENTS – Email me when a new event is added or an existing event is updated." /><br />
                                                                            <asp:CheckBox ID="cbGUESTBOOK" runat="server" Text="GUESTBOOK – Email me when there is a new guestbook entry." /><br />
                                                                            <asp:CheckBox ID="cbGifts" runat="server" Text="GIFTS – Email me when a new gift is given." /><br />
                                                                            <asp:CheckBox ID="cbPHOTOALBUM" runat="server" Text="PHOTO ALBUM – Email me when a new photo album is created." /><br />
                                                                            <asp:CheckBox ID="cbPHOTOS" runat="server" Text="PHOTOS – Email me when a new photos is added to an album." /><br />
                                                                            <asp:CheckBox ID="cbVideo" runat="server" Text="VIDEOS – Email me when a new video is added." /><br />
                                                                            <asp:CheckBox ID="cbComments" runat="server" Text="COMMENTS – Email me when a comment is made on a note, photo or video." /><br />
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="width: 111px">
                                                                        <td>
                                                                            <asp:Label ID="Label5" runat="server" Font-Bold="True" Text="GENERAL NOTIFICATIONS"
                                                                                Width="272px"></asp:Label></td>
                                                                    </tr>
                                                                    <tr style="width: 111px">
                                                                        <td>
                                                                            <asp:Label ID="Label6" runat="server" Text="General notifications apply to your Your Tibute account."></asp:Label></td>
                                                                    </tr>
                                                                    <tr style="width: 111px">
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="width: 111px">
                                                                        <td>
                                                                            <asp:CheckBox ID="cbMessages" runat="server" Text="MESSAGES – Email me when I have a new message in my inbox." /><br />
                                                                            <asp:CheckBox ID="cbNewsLetter" runat="server" /><br />
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="width: 111px">
                                                                        <td>
                                                                            <asp:Label ID="Label7" runat="server" ></asp:Label></td>
                                                                    </tr>
                                                                    <tr style="width: 111px">
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="width: 111px">
                                                                        <td>
                                                                            <asp:Button ID="btnenSaveChanges" runat="server" OnClick="btnenSaveChanges_Click"
                                                                                Text="SaveChanges" /></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </asp:Panel>
                                            <!--yt-Form-->
                                            <!--fieldset-->
                                            <!--yt-ContentPrimary-->
                                        </div>
                                    </asp:View>
                                    <asp:View ID="tbBillingInformation" runat="server">
                                        <table border="0" width="100%">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblBillingHistory" runat="server" Font-Bold="True" Text="Billing History"></asp:Label></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:GridView ID="gvBillingHistory" AutoGenerateColumns="False" runat="server" BackColor="#E0E0E0"
                                                        Width="629px" OnSelectedIndexChanged="gvBillingHistory_SelectedIndexChanged">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="lblDate" runat="server" Text="Date:"></asp:Label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnDate" Text='<%# Bind("StartDate","{0:MMMM dd,yyyy}")%>' CommandName="Select"  runat="server"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="lblTributeName"  runat="server" Text="Tribute Name:"></asp:Label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnTributeName" CommandName="Select" Text='<%# Eval("TributeName") %>' runat="server"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="lblPackageName" runat="server" Text="Package:"></asp:Label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnPackage" CommandName="Select" Text='<%# Eval("PackageName") %>' runat="server"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="lblAmountPaid" runat="server" Text="Amount:"></asp:Label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnAmount" CommandName="Select" Text='<%# Eval("AmountPaid") %>' runat="server"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:Label ID="lblTributeid" Visible="false" runat="server" Text="Tributeid:"></asp:Label>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbtnTributeid"  Text='<%# Eval("Tributeid") %>' Visible="false" runat="server" ></asp:Label>                                                                    
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        
                                                    </asp:GridView>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblCreditCardInformation" runat="server" Text="Credit Card Information"></asp:Label>
                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                    <asp:Button ID="btnDeleteCreditCardInformation" runat="server" Text="Delete Credit Card Information"
                                                        OnClick="btnDeleteCreditCardInformation_Click" /></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="pnlAddCreditInformation" runat="server" Visible="False">
                                                        <asp:Label ID="lblStorednewInfo" runat="server" Text="You have not stored any credit card information. Please select below to add your credit card."
                                                            Width="863px"></asp:Label>
                                                        <br />
                                                        <br />
                                                        <asp:Button ID="lbnAddCreditCardInformation" runat="server" Text="Add Credit Card Information" /></asp:Panel>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PanelCreditCardInfo" runat="server" Width="863px">
                                                        <table style="width: 857px">
                                                            <tr>
                                                                <td >
                                                                    <asp:Label ID="lblSelectyourpaymentmethod" runat="server" Text="* Select your payment method:"></asp:Label></td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:RadioButtonList ID="rdoPaymentMode" runat="server" RepeatDirection="Horizontal"
                                                                        RepeatLayout="Flow">
                                                                    </asp:RadioButtonList></td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 262px">
                                                                    <asp:Label ID="lblCardnumber" runat="server" Text="* Card number:"></asp:Label>
                                                                    <br />
                                                                    <asp:TextBox ID="txtCardNumber" runat="server" Width="250px"></asp:TextBox><br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 262px">
                                                                    <asp:Label ID="lblCardverification" runat="server" Text="* Card verification:"></asp:Label><br />
                                                                    <asp:TextBox ID="txtCardVerification" runat="server" Width="250px"></asp:TextBox><br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 262px">
                                                                    <asp:Label ID="lblExpiryDate" runat="server" Text="* Expiry Date:"></asp:Label><br />
                                                                    <asp:DropDownList ID="ddlExpiryMonth" runat="server" Width="92px">
                                                                        <asp:ListItem Selected="True">--</asp:ListItem>
                                                                        <asp:ListItem>1</asp:ListItem>
                                                                        <asp:ListItem>2</asp:ListItem>
                                                                        <asp:ListItem>3</asp:ListItem>
                                                                        <asp:ListItem>4</asp:ListItem>
                                                                        <asp:ListItem>5</asp:ListItem>
                                                                        <asp:ListItem>6</asp:ListItem>
                                                                        <asp:ListItem>7</asp:ListItem>
                                                                        <asp:ListItem>8</asp:ListItem>
                                                                        <asp:ListItem>9</asp:ListItem>
                                                                        <asp:ListItem>10</asp:ListItem>
                                                                        <asp:ListItem>11</asp:ListItem>
                                                                        <asp:ListItem>12</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    &nbsp;&nbsp;&nbsp;
                                                                    <asp:TextBox ID="txtExpiryYear" runat="server" Width="85px"></asp:TextBox><br />
                                                                    <asp:Label ID="lblmonth" runat="server" Text="Month"></asp:Label>
                                                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                                                    <asp:Label ID="lblYear" runat="server" Text=" Year"></asp:Label>
                                                                    &nbsp; &nbsp; &nbsp; &nbsp;<br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 262px">
                                                                    <asp:Label ID="lblNameofcard" runat="server" Text="* Name of card:"></asp:Label><br />
                                                                    <asp:TextBox ID="txtCardName" runat="server" Width="250px"></asp:TextBox><br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 262px">
                                                                    <asp:Label ID="lblBillingaddress" runat="server" Text="* Billing address:"></asp:Label><br />
                                                                    <asp:TextBox ID="txtBillingAddress" runat="server" Width="250px" Height="55px" TextMode="MultiLine"></asp:TextBox><br />
                                                                    <br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 262px">
                                                                    <asp:UpdatePanel ID="PanelBICountry" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:Label ID="lblBICountry" runat="server" Text="* Country:"></asp:Label><br />
                                                                            <asp:DropDownList ID="ddlCountryExpiry" runat="server" Width="250px" AutoPostBack="True"
                                                                                OnSelectedIndexChanged="ddlCountryExpiry_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                            <br />
                                                                            <br />
                                                                            <asp:Label ID="lblexpiryStateProvince" runat="server" Text="State/Province:"></asp:Label><br />
                                                                            <asp:DropDownList ID="ddlStateExpiry" runat="server" Width="250px">
                                                                            </asp:DropDownList><br />
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 262px">
                                                                    <asp:Label ID="lblPostalCodeZipCode" runat="server" Text="* Postal Code / Zip Code:"></asp:Label><br />
                                                                    <asp:TextBox ID="txtPostalCodeExpiry" runat="server" Width="250px"></asp:TextBox><br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 262px">
                                                                    <asp:Label ID="lblBITelephone" runat="server" Text="* Telephone"></asp:Label><br />
                                                                    <asp:TextBox ID="txtTelephoneExpiry" runat="server" Width="250px"></asp:TextBox><br />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 262px">
                                                                    <asp:Button ID="btnbiSaveChanges" runat="server" Text="Save Changes" OnClick="btnbiSaveChanges_Click" /></td>
                                                            </tr>
                                                        </table>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:View>
                                </asp:MultiView>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
