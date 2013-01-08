<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ThemeFullSizeImage.aspx.cs" Inherits="ModelPopup_ThemeFullSizeImage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Theme Full Image</title>
    <link rel="Shortcut Icon" type="image/x-icon" href="<%=Session["APP_BASE_DOMAIN"]%>assets/images/favicon.ico" />
</head>
<body style=" padding:0px; margin:0px;">
    <form id="form1" runat="server" >
    <div >
    <img  id="fullSizeImage" runat="server" alt="story image" src="" />
    </div>
    </form>
</body>
</html>
