<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmailUrl.aspx.cs" Inherits="MessagingSystem_EmailUrl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head>
    <title>Your Tribute - EmailUrl</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Content-Language" content="en-ca" />
    <meta http-equiv="imagetoolbar" content="false" />
    <meta name="robots" content="index,follow" />
    <meta name="MSSmartTagsPreventParsing" content="true" />
    <!-- really basic, generic html class stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/default.css" />
    <!-- default grid layout stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/layouts/centered1024_1.css" />
    <!-- print-friendly stylesheet -->
    <link rel="stylesheet" type="text/css" media="print" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/print.css" />
    <!-- screen elements stylesheet should be here -->
    <link rel="stylesheet" type="text/css" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/screen.css" />
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/userprofile.css" />
    <!-- JS libraries -->

    <script type="text/javascript" src="../assets/scripts/mootools-1.11.js"></script>

    <script type="text/javascript" src="../assets/scripts/global.js"></script>

    <script type="text/javascript" src="../Common/JavaScript/Common.js"></script>

    <!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
    <script src="../assets/scripts/BrowserOrTabCloseHandler.js" type="text/javascript"></script>

    <script type="text/javascript">
	
    
    function checkValidEmail(source, args)
    {
        var validator = document.getElementById('<%= cvCheckValidEmail.ClientID %>');
        var EmailList = (document.getElementById('<%= txtEmailAddress.ClientID %>').value).replace(/\,/g, ";");
        var test = EventCheckValidEmail(validator, EmailList);
        alert(test);
        args.IsValid = EventCheckValidEmail(validator, EmailList);
    }
    </script>

</head>
<body>
    <form id="Form1" action="" runat="server">
    <div id="divShowModalPopup"></div> 
        <div class="yt-Container">
            <div class="yt-ContentContainer">
                <div class="yt-ContentPrimaryContainer">
                    <div class="yt-ContentPrimary">
                        <div id="yt-ProfileError">
                            <asp:ValidationSummary CssClass="yt-Error" ID="vsError" runat="server" Width="868px"
                                HeaderText="<h2>Oops - there was a problem with some account type information.</h2><h3>Please correct the errors below:</h3>"
                                ForeColor="Black" />
                        </div>
                        <div id="yt-ShareContent" class="yt-ModalWrapper">
                            <div class="yt-Panel-Primary">
                                <h2 id="hEmailPage" runat="server">
                                </h2>
                                <p id="pRequired" runat="server" class="yt-requiredFields">
                                </p>
                                <div class="yt-Form-Field">
                                    <label for="txtUserName" id="lblUserName" runat="server">
                                    </label>
                                    <asp:TextBox ID="txtUserName" runat="server" CssClass="yt-Form-Input-Long"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName">!</asp:RequiredFieldValidator>
                                </div>
                                <div class="yt-Form-Field">
                                    <label for="txtSendEmail" id="lblUserEmailAddress" runat="server">
                                    </label>
                                    <asp:TextBox ID="txtUserEmailAddress" runat="server" CssClass="yt-Form-Input-Long"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="rfvFromEmailAddress" runat="server" ControlToValidate="txtUserEmailAddress"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">!</asp:RegularExpressionValidator>
                                </div>
                                <div class="yt-Form-Field yt-EmailList">
                                    <label for="txtarEmailAddresses" id="lblEmailAddress" runat="server">
                                    </label>
                                    <asp:TextBox ID="txtEmailAddress" runat="server" CssClass="yt-Form-Textarea-Short"
                                        Columns="50" Rows="6"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvEmailAddress" runat="server" ControlToValidate="txtEmailAddress">!</asp:RequiredFieldValidator>
                                    <asp:CustomValidator ID="cvCheckValidEmail" runat="server" ClientValidationFunction="checkValidEmail"
                                        Width="1px" ErrorMessage="Email Address are not valid" Font-Bold="True" Font-Size="Medium"
                                        ForeColor="#FF8000">!</asp:CustomValidator>
                                    <div class="yt-Form-MiniButtons">
                                        <div class="yt-Form-Submit">
                                            <asp:LinkButton ID="lbtnSubmit" CssClass="yt-MiniButton yt-AddButton" runat="server"
                                                OnClick="lbtnSubmit_Click"></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--yt-ContentPrimary-->
                </div>
                <!--yt-ContentPrimaryContainer-->
            </div>
            <!--yt-ContentContainer-->
        </div>
        <!--yt-Container-->
    </form>
</body>
</html>
