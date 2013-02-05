<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GuestListFullDetails.aspx.cs"
    Inherits="ModelPopup_GuestListFullDetails" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head>
    <title>RSVP DETAILS</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Content-Language" content="en-ca" />
    <meta http-equiv="imagetoolbar" content="false" />
    <meta name="robots" content="index,follow" />
    <meta name="MSSmartTagsPreventParsing" content="true" />
    <!-- really basic, generic html class stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/default.css" />
    <!-- default grid layout stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/layouts/centered1024_21.css" />
    <!-- print-friendly stylesheet -->
    <link rel="stylesheet" type="text/css" media="print" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/print.css" />
    <!-- screen elements stylesheet should be here -->
    <link rel="stylesheet" type="text/css" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/screen.css" />
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/ContactUs.css" />
    <!-- JS libraries -->

    <script type="text/javascript" src="../assets/scripts/mootools-1.11.js"></script>

    <script type="text/javascript" src="../assets/scripts/global.js"></script>

    <script type="text/javascript" src="../assets/scripts/tributecreation.js"></script>

    <!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
    <script type="text/javascript" src="../assets/scripts/BrowserOrTabCloseHandler.js"></script>

    <script type="text/javascript" src="../Common/JavaScript/Common.js"></script>

    <script type="text/javascript" src="../Common/JavaScript/Event.js"></script>

    <script type="text/javascript">
    window.addEvent('load', function() {	
    	buttonStyles();
	    thumbStyles();
	    hrFix();
	    fieldsetFix();
	});
	
	
	 function ValidateRSVPStatus(source, args)
     {
            var attendingVal = document.getElementById('<%= rdoEventRSVPAttending.ClientID %>');
            var maybeVal = document.getElementById('<%= rdoEventRSVPMaybe.ClientID %>');   
            var notVal = document.getElementById('<%= rdoEventRSVPNot.ClientID %>');                  
            
            if( (attendingVal.checked == false) && (maybeVal.checked == false) && (notVal.checked== false ))
            {
                args.IsValid = false;             
            }
            else
            {
                args.IsValid = true;
            }
     }
     
    </script>
    <!--#include file="../analytics.asp"-->
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="lblErrMsg" align="left" runat="server" class="yt-Error" visible="false">
    </div>
    <div>
        <%--<asp:ValidationSummary CssClass="yt-Error" ID="valsError" runat="server" Width="647px"
            HeaderText=" <h2>Oops - there was a problem with your RSVP details.</h2>                                                             <h3>Please correct the errors below:</h3> "
            ForeColor="Black" />--%>
    </div>
    <div id="divShowModalPopup"></div> 
    <div class="yt-Container">
        <div class="yt-ContentContainer">
            <div class="yt-SectionWrapper yt-More">
                <table width="100%">
                    <tr>
                        <td colspan="2" class="yt-ErrorMes">
                            <asp:Label ID="lblErrMess"   Text="" Visible="false" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFirstName" runat="server" Font-Bold="true" Text="First Name:"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblPhoneNumber" runat="server" Font-Bold="true" Text="Phone Number:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtFirstName" runat="server" Font-Bold="true" Width="80%"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="valFirstName" ControlToValidate="txtFirstName"
                                Width="1px" ErrorMessage="First Name is a required field." Font-Bold="True" Font-Size="Medium"
                                ForeColor="#FF8000">!</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPhoneNumber" runat="server" Font-Bold="true" Width="80%"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPhoneNumber" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"
                             ErrorMessage="Invalid Phone Number" ValidationExpression="^[0-9,\-,]*$">!</asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblLastName" runat="server" Font-Bold="true" Text="Last Name:"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblEmail" runat="server" Font-Bold="true" Text="Email Address:"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtLastName" runat="server" Font-Bold="true" Width="80%"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="valLastName" ControlToValidate="txtLastName"
                                Width="1px" ErrorMessage="Last Name is a required field." Font-Bold="True" Font-Size="Medium"
                                ForeColor="#FF8000">!</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server" Font-Bold="true" Width="80%"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" Font-Bold="True" Font-Size="Medium"
                                ForeColor="#FF8000" ErrorMessage="Invalid Email Address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">!</asp:RegularExpressionValidator>
                        </td>
                    </tr>
                </table>
                <table width="100%">
                    <tr id="trMealOption" runat="server">
                        <td>
                            <asp:Label ID="lblMealOption" runat="server" Font-Bold="true" Text="Meal Option:"></asp:Label>
                            <asp:DropDownList ID="ddlMealOption" runat="server" CssClass="yt-Form-DropDown-Short" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblRsvpStatus" runat="server" Font-Bold="true" Text="RSVP:"></asp:Label>
                            <asp:RadioButton ID="rdoEventRSVPAttending" GroupName="rdoRsvpStatus" runat="server"
                                Text="Attending" TabIndex="1" />
                            <asp:RadioButton ID="rdoEventRSVPMaybe" GroupName="rdoRsvpStatus" runat="server"
                                Checked="false" Text="Maybe Attending" TabIndex="2" />
                            <asp:RadioButton ID="rdoEventRSVPNot" GroupName="rdoRsvpStatus" runat="server" Checked="false"
                                Text="Not Attending" TabIndex="3" />
                            <asp:CustomValidator ID="valRSVPStatus" runat="server" Font-Bold="True" Font-Size="Medium"
                                ForeColor="#FF8000" ClientValidationFunction="ValidateRSVPStatus" ErrorMessage=" Please select RSVP Status">!</asp:CustomValidator>
                        </td>
                    </tr>
                </table>
                <div class="yt-Form-Field">
                    <asp:Label ID="lblComment" runat="server" Font-Bold="true" Text="Comment:"></asp:Label>
                    <asp:TextBox ID="txtComment" runat="server" Height="100px" Width="100%" TextMode="MultiLine"></asp:TextBox>
                </div>
                <div class="adModalButtons">
                    <div class="yt-Form-SubmitRight">
                        <asp:LinkButton ID="lbtnDeleteRsvp" Text="DELETE RSVP" runat="server" CssClass="yt-Button yt-ModalButton"
                            OnClick="lbtnDeleteRsvp_Click" CausesValidation="false"/>
                        <asp:LinkButton ID="lbtnSaveRsvp" Text="SAVE RSVP" runat="server" CssClass="yt-Button yt-ModalButton"
                            OnClick="lbtnSaveRsvp_Click" />
                    </div>
                </div>
            </div>
        </div>
        <!--yt-ContentContainer-->
    </div>
    <!--yt-Container-->
    </form>
</body>
</html>
