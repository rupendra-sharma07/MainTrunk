<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Newsletters.aspx.cs" Inherits="Newsletters" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>MailChimp Test</title>
    </head>
<body>
    <form id="form1" runat="server">
    <asp:Label ID="lblError" runat="server" style="color:Red;"></asp:Label>
    </br>
    Enter your Email id to subscribe to our news letters :
    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
    <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
        onclick="btnSubmit_Click" />
    </form>
</body>
</html>
