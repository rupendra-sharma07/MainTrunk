<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ContactUs.aspx.cs" Inherits="ModelPopup_ContactUs"
    ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head>
    <title>Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> - Contact Us</title>
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

    <script type="text/javascript">
    window.addEvent('load', function() {	
    	buttonStyles();
	    thumbStyles();
	    hrFix();
	    fieldsetFix();
	});
    </script>

    <!--#include file="../analytics.asp"-->
</head>
<body>
    <form action="" runat="server" defaultbutton="lbtnSendMessages" defaultfocus="txtarEmailMessage">
    <div id="divShowModalPopup">
    </div>
    <div align="left">
        <asp:ValidationSummary ID="PortalValidationSummary" runat="server" ForeColor="Black"
            Visible="true" ValidationGroup="ContactUs" HeaderText="   <h2>Oops - there was a problem with your contact information.</h2>                                                             <h3>Please correct the errors below:</h3>"
            Width="409px" CssClass="yt-Error"></asp:ValidationSummary>
    </div>
    <div class="yt-Container">
        <div class="yt-ContentContainer">
            <div>
                <div>
                    <h4>
                        Please use this form to send us an email.</h4>
                    <p>
                        If you need technical support, please visit our Help section first, it contains
                        answers to our most frequently asked questions.
                    </p>
                    <fieldset class="yt-Form">
                        <div class="yt-Form-Field">
                            <label for="txtEmail">
                                <em class="required">* </em>Email Address:</label>
                            <asp:TextBox ID="txtEmail" ValidationGroup="ContactUs" CssClass="yt-Form-Input-Long"
                                runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ValidationGroup="ContactUs" Font-Bold="True" Font-Size="Medium"
                                ForeColor="#FF8000" ID="rfvtxtEmail" ControlToValidate="txtEmail" runat="server"
                                ErrorMessage="Email Address is a required field." Text="!" Width="1px"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ValidationGroup="ContactUs" Font-Bold="True" Font-Size="Medium"
                                ForeColor="#FF8000" ID="revtxtEmail" ControlToValidate="txtEmail" runat="server"
                                Text="!" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                Width="1px" ErrorMessage="Enter valid email address."></asp:RegularExpressionValidator>
                        </div>
                        <div class="yt-Form-Field">
                            <label for="txtName">
                                <em class="required">* </em>Your Name:</label>
                            <asp:TextBox ID="txtName" ValidationGroup="ContactUs" CssClass="yt-Form-Input-Long"
                                runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ValidationGroup="ContactUs" Font-Bold="True" Font-Size="Medium"
                                ForeColor="#FF8000" ID="rfvtxtName" ControlToValidate="txtName" runat="server"
                                ErrorMessage="Name is a required field." Text="!"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revName" ControlToValidate="txtName" ValidationGroup="ContactUs"
                                Text="!" runat="server" ErrorMessage="Name only contain letters,numbers,'-' and '#'"
                                ValidationExpression="^[a-zA-Z0-9,\#,\-,\s]*$" Font-Bold="True" Font-Size="Medium"
                                ForeColor="#FF8000"></asp:RegularExpressionValidator>
                        </div>
                        <div class="yt-Form-Field">
                            <label for="ddlReason">
                                <em class="required">* </em>Reason:</label>
                            <asp:DropDownList ID="ddlReason" CssClass="yt-Form-DropDown-Long" runat="server">
                                <asp:ListItem Selected="True" Value="needtechsupport">I need technical support</asp:ListItem>
                                <asp:ListItem Value="generalinquiry">I have a general inquiry</asp:ListItem>
                                <asp:ListItem Value="billingquestion">I have a billing question</asp:ListItem>
                                <asp:ListItem Value="newfeature">I have a comment / suggestion</asp:ListItem>
                                <asp:ListItem Value="advertising">I want to discuss advertising opportunities</asp:ListItem>
                                <asp:ListItem Value="partnership">I want to discuss business partnerships</asp:ListItem>
                                <asp:ListItem Value="inappropriate">I want to report inappropriate content</asp:ListItem>
                                <asp:ListItem Value="Other">Other</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="yt-Form-Field">
                            <label for="txtarMessage">
                                <em class="required">* </em>Message:</label>
                            <asp:TextBox ID="txtarEmailMessage" Rows="6" ValidationGroup="ContactUs" Columns="6"
                                TextMode="MultiLine" CssClass="yt-Form-Textarea-XLong" runat="server" Height="117px"></asp:TextBox>
                            <asp:RequiredFieldValidator Font-Bold="True" ValidationGroup="ContactUs" Font-Size="Medium"
                                ForeColor="#FF8000" ID="rfvtxtarEmailMessage" ControlToValidate="txtarEmailMessage"
                                runat="server" Text="!" ErrorMessage="Message is a required field">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revVideoDescSpecialchar" Font-Bold="True" Font-Size="Medium"
                                ForeColor="#FF8000" ControlToValidate="txtarEmailMessage" ValidationGroup="ContactUs"
                                ErrorMessage="Please provide valid input characters in message field." ValidationExpression='^[a-zA-Z0-9\s\?\"\!\-\@\.\:\;\=\+\[\]_\,\%\(\)\/\&]+$'
                                runat="server">!</asp:RegularExpressionValidator>
                        </div>
                        <div class="yt-Form-Buttons" style="right: auto">
                            <asp:LinkButton ID="lbtnSendMessages" ValidationGroup="ContactUs" CssClass="yt-Button yt-ArrowButton"
                                runat="server" Width="100px" OnClick="lbtnSendMessages_Click">Send Message</asp:LinkButton>
                        </div>
                    </fieldset>
                </div>
            </div>
            <!--yt-ContentPrimaryContainer-->
            <div id="Setfocus" class="hack-clearBoth">
            </div>
        </div>
        <!--yt-ContentContainer-->
    </div>
    <!--yt-Container-->
    </form>
    
    <!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
    <script type="text/javascript" src="../assets/scripts/BrowserOrTabCloseHandler.js"></script>

</body>
</html>
