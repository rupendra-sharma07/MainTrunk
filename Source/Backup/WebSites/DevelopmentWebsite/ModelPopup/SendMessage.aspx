<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SendMessage.aspx.cs" Inherits="ModelPopup_SendMessage" %>

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
    <div id="divShowModalPopup"></div> 
        <div class="yt-Container">
            <div class="yt-ContentContainer">
                <div>
                    <div>
                        <h4>
                            Please use this form to send us an email.</h4>
                        <fieldset class="yt-Form">
                            <div class="yt-Form-Field">
                                <label for="txtEmail">
                                    <em class="required">* </em>Email Address:</label>
                                <asp:TextBox ID="txtEmail" ValidationGroup="ContactUs" CssClass="yt-Form-Input-Long"
                                    runat="server"></asp:TextBox><asp:RequiredFieldValidator ValidationGroup="ContactUs"
                                        Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ID="rfvtxtEmail" ControlToValidate="txtEmail"
                                        runat="server" Text="!" Width="1px"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                            ValidationGroup="ContactUs" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"
                                            ID="revtxtEmail" ControlToValidate="txtEmail" runat="server" Text="!" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            Width="1px"></asp:RegularExpressionValidator>
                            </div>
                            <div class="yt-Form-Field">
                                <label for="txtName">
                                    <em class="required">* </em>Your Name:</label>
                                <asp:TextBox ID="txtName" ValidationGroup="ContactUs" CssClass="yt-Form-Input-Long"
                                    runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ValidationGroup="ContactUs" Font-Bold="True" Font-Size="Medium"
                                    ForeColor="#FF8000" ID="rfvtxtName" ControlToValidate="txtName" runat="server"
                                    Text="!"></asp:RequiredFieldValidator>
                            </div>
                            <div class="yt-Form-Field">
                                <label for="ddlReason">
                                    <em class="required">* </em>Subject:</label>
                                <asp:TextBox ID="ddlReason" ValidationGroup="ContactUs" CssClass="yt-Form-Input-Long"
                                    runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ValidationGroup="ContactUs" Font-Bold="True" Font-Size="Medium"
                                    ForeColor="#FF8000" ID="RequiredFieldValidator1" ControlToValidate="ddlReason" runat="server"
                                    Text="!"></asp:RequiredFieldValidator>
                               
                            </div>
                            <div class="yt-Form-Field">
                                <label for="txtarMessage">
                                    <em class="required">* </em>Message:</label>
                                <asp:TextBox ID="txtarEmailMessage" Rows="6" ValidationGroup="ContactUs" Columns="6"
                                    TextMode="MultiLine" CssClass="yt-Form-Textarea-XLong" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator Font-Bold="True" ValidationGroup="ContactUs" Font-Size="Medium"
                                    ForeColor="#FF8000" ID="rfvtxtarEmailMessage" ControlToValidate="txtarEmailMessage"
                                    runat="server" Text="!"></asp:RequiredFieldValidator>
                            </div>
                            <div class="yt-Form-Buttons" style="right: auto">
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                &nbsp;<asp:LinkButton ID="lbtnSendMessages" ValidationGroup="ContactUs" CssClass="yt-Button yt-ArrowButton"
                                    runat="server" Width="87px" OnClick="lbtnSendMessages_Click">Send Message</asp:LinkButton>
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
</body>
</html>
