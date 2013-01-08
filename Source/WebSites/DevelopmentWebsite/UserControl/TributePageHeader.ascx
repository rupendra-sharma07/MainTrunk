<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TributePageHeader.ascx.cs"
    Inherits="UserControl_TributePageHeader" %>
<div id="divShowModalPopup">
</div>
<div id="TributePageHeader" class="yt-CustomHeaderContainer" runat="server" visible="false">
    <div class="yt-CustomHeader" id="dvCustomeHeader" runat="server">
        <asp:Image ID="imgTributeImageView" runat="server" Visible="true" Height="100px" AlternateText=" "
            Style="margin: -10px 0px 0px -5px" ImageAlign="Left" Width="250px" />
        <%-- <asp:HiddenField ID="hdnTributeLogo" runat="server" />--%>
        <div class="yt-HeaderControls">
            <div class="yt-NavCustomHeader">
                <div class="yt-vt-Address">
                    <asp:Label ID="lblUserStreet" runat="server"></asp:Label>
                    <p>
                        <asp:Label ID="lblUsercontactNo" runat="server"></asp:Label>
                        <asp:Label ID="lblWebAddress" runat="server">Web: <a id="linkWebAddress" target="_blank"
                            runat="server">
                            <asp:Label ID="lblWebsite" runat="server"></asp:Label></a></asp:Label></p>
                </div>
                <div runat="server" visible="false">
                    <a id="linkViewFuneralHome" target="_blank" runat="server">View Funeral Home Profile</a>
                </div>
                <div id="divFuneralObHomePage" runat="server">
                    <a href='<%=sObituaryURL %>' target="_blank">Go To Funeral Home Obituary Page</a>
                </div>
            </div>
            <!--yt-HeaderControls-->
        </div>
        <!--yt-Header-->
    </div>
</div>

<script type="text/javascript">
    App_Domain = "<%=Session["APP_BASE_DOMAIN"]%>";
</script>

