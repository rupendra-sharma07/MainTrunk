<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BusinessUserHome.aspx.cs"
    Inherits="MyHome_BusinessUserHome" Title="BusinessUserHome" %>
<%@ Register Src="../UserControl/TributeHeader.ascx" TagName="Header" TagPrefix="uc" %>
<%@ Register Src="../UserControl/TributePageHeader.ascx" TagName="TributeHeader"
    TagPrefix="ucTribute" %>
<%@ Register Src="../UserControl/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Your Tribute - Company Name's Tributes</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="Content-Language" content="en-ca" />
    <meta http-equiv="imagetoolbar" content="false" />
    <meta name="robots" content="index,follow" />
    <meta name="MSSmartTagsPreventParsing" content="true" />
    <!-- really basic, generic html class stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/default.css" />
    <!-- default grid layout stylesheet -->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/layouts/centered1024_21.css" />
    <!-- print-friendly stylesheet -->
    <link rel="stylesheet" type="text/css" media="print" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/print.css" />
    <!-- screen elements stylesheet should be here -->
    <link rel="stylesheet" type="text/css" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/screen.css" />
    <!-- larger text stylesheets -->
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/medium_text.css"
        title="medium_text" />
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/large_text.css"
        title="large_text" />
    <!-- Website Favorite Icon -->
    <link rel="Shortcut Icon" type="image/x-icon" href="<%=Session["APP_BASE_DOMAIN"]%>assets/images/favicon.ico" />
    <!-- JS libraries -->

    <script src="http://maps.google.com/maps?file=api&amp;v=2.x&amp;key=<%=TributesPortal.Utilities.WebConfig.GoogleAPIKey%>"
        type="text/javascript"></script>

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/map.js"></script>

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/mootools-1.11.js"></script>

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/global.js"></script>

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/styleSwitcher.js"></script>

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/modalbox.js"></script>

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>Common/JavaScript/Common.js"></script>

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>Common/JavaScript/FooterControl.js"></script>

    <script language="javascript" type="text/jscript">
    
    function SetImage(url) 
    {       
        document.getElementById('<%=imgLogo.ClientID%>').src = url;
        document.getElementById('<%=hdnLogoURL.ClientID%>').value = url;
        
       
    }
    
    function OpenNewWindow(url)
    {  
        window.open(url);
    }
    function ReloadPage()
        {
        window.location.reload();
        
        }
    </script>

</head>
<body>
    <form id="Form1" runat="server" action="">
    <div id="divShowModalPopup">
    </div>
    <asp:HiddenField ID="hdnLogoURL" runat="server" />
    <div class="yt-Container yt-BusinessHome yt-AllTributes">
    <uc:Header ID="ytHeader" Section="tribute" runat="server" />
            <ucTribute:TributeHeader ID="TributeCustomHeader" section="Tribute" navigationname="Home"
                runat="server" />
                <!--yt-HeaderContainer-->
        <div id="BlankTopDiv" class="hack-clearBoth" style="height:10px;" runat="server">
        </div>
        <div class="yt-ContentContainer">
            <div id="Div1" runat="server">
            </div>
            <div id="maindiv" runat="server" class="yt-BusinessHome mainDiv">
                <div class="yt-ContentContainerInner">
                    <div class="yt-ContentPrimaryContainer">
                    
                    <%--<span style="float:right;" id=""><a href="javascript: void(0);" onclick="UserLoginModalpopupFromSubDomain(location.href,document.title);"></a></span>--%>
                        <div class="yt-UserInfo" style="float:right;">
                            <span id="spanLogout" runat="server"></span>
                        </div>
                        <div class="yt-ContentPrimary">
                            <div id="lblNotice" runat="server" class="yt-Notice" visible="false">
                            </div>
                            <div class="yt-Panel-Primary">
                                <h2>
                                    <asp:Label ID="lblAllTrubute" runat="server" Text="Tributes"></asp:Label></h2>
                                <div id="divMessage" runat="server">
                                </div>
                                <div class="yt-SearchResultList">
                                    <div id="divPagingHead" runat="server" class="yt-ListHead">
                                        <span id="spanHeadRecordCount" runat="server"></span>
                                        <div class="yt-Pagination">
                                            <span id="PageHead" runat="server">Page:</span> <span id="spanPagingHead" runat="server">
                                            </span>
                                        </div>
                                    </div>
                                    <div class="yt-SearchFilters">
                                        <div class="yt-Form-Field">
                                            <asp:Label ID="lblShowMe" runat="server" Text="Show Me:"></asp:Label>
                                            <asp:DropDownList ID="ddlTributeType" runat="server" OnSelectedIndexChanged="ddlTributeType_SelectedIndexChanged"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="yt-Form-Field">
                                            <asp:Label ID="lblSortBy" runat="server" Text="Sort By: "></asp:Label>
                                            <asp:DropDownList ID="ddlSort" runat="server" OnSelectedIndexChanged="ddlSort_SelectedIndexChanged"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div id="divTributeList" runat="server" class="yt-ListBody">
                                        <asp:Repeater ID="repSearchTribute" runat="server" OnItemCommand="repSearchTribute_ItemCommand"
                                            OnItemDataBound="repSearchTribute_ItemCommand">
                                            <ItemTemplate>
                                                <div class="yt-ListItem">
                                                    <h4>
                                                        <asp:LinkButton ID="lbtnTributeName" CssClass="yt-ListName" CommandArgument='<%#Eval("TributeId")%>'
                                                            CommandName="TributeHome" Text='<%#Eval("TributeName")%>' runat="server"></asp:LinkButton></h4>
                                                    <p class="yt-ItemDate">
                                                        <%# Eval("Date1") %>
                                                    </p>
                                                    <div class="yt-ItemThumb">
                                                        <a class="yt-Thumb">
                                                            <img src='<%#Eval("TributeImage")%>' alt='<%# Eval("TributeName")%>' width="75" height="75" /></a>
                                                    </div>
                                                    <dl class="yt-PrimaryInfo">
                                                        <dt>Type:</dt>
                                                        <dd>
                                                            <%#Eval("TributeType")%>
                                                        </dd>
                                                        <dt>Location:</dt>
                                                        <dd>
                                                            <%#Eval("Location")%>
                                                        </dd>
                                                        <span>
                                                            <br />
                                                            <asp:LinkButton ID="lbtnVideoTribute" CssClass="yt-ListName" CommandArgument='<%#Eval("TributeId")%>'
                                                                CommandName="WatchVideo" Text='Watch the Video Tribute' runat="server"></asp:LinkButton>
                                                        </span>
                                                    </dl>
                                                    <dl class="yt-SecondaryInfo">
                                                        <dt>Created:</dt>
                                                        <dd>
                                                            <%#Eval("CreatedDate")%>
                                                        </dd>
                                                        <dt>Views:</dt>
                                                        <dd>
                                                            <%#Eval("Hits")%>
                                                        </dd>
                                                    </dl>
                                                    <asp:HiddenField ID="hdnTributeType" runat="server" Value='<%#Eval("TributeType")%>' />
                                                    <asp:HiddenField ID="hdnTributeUrl" runat="server" Value='<%#Eval("TributeUrl")%>' />
                                                    <asp:HiddenField ID="hdnVideoTributeId" runat="server" Value='<%#Eval("VideoTributeId")%>' />
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <div id="divPagingFoot" runat="server" class="yt-ListFoot">
                                        <span id="spanFootRecordCount" runat="server"></span>
                                        <div class="yt-Pagination">
                                            <span id="PageFoot" runat="server">Page:</span> <span id="spanPagingFoot" runat="server">
                                            </span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!--yt-TributeProcess-->
                        </div>
                        <!--yt-ContentPrimary-->
                    </div>
                    <!--yt-ContentPrimaryContainer-->
                    <div class="yt-ContentSecondary">
                        <div class="yt-Panel yt-Panel-Branding">
                            <div class="yt-Panel-Body">
                                <h2>
                                    <!-- Start - Modification on 17-Dec-09 for the enhancement 4 of the Phase 1 -->
                                    <img id="imgLogo" runat="server" src="../assets/images/bg_CompanyLogo.gif.gif" alt="Upload your company logo"
                                        width="175" height="112" />
                                    <!-- width="130" height="112" -->
                                    <!-- End -->
                                </h2>
                                <div class="yt-Form-MiniButtons">
                                    <a id="lbtnUpload" runat="server" href="javascript: void(0);" onclick="uploadLogo();"
                                        class="yt-MiniButton yt-UploadPhotoButton">Upload photo</a>
                                </div>
                                <div id="divEdit" runat="server" visible="false">
                                    <div class="yt-Form-Field">
                                        <asp:TextBox ID="txtWelcomeMessage" runat="server" Columns="20" Rows="10" CssClass="yt-Form-Textarea-Short"
                                            TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                    <div class="yt-Form-MiniButtons">
                                        <asp:LinkButton ID="lbtnSaveMessage" runat="server" CssClass="yt-MiniButton yt-SaveButton"
                                            OnClick="lbtnSaveMessage_Click">Save</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnCancelMessage" runat="server" CssClass="yt-MiniButton yt-CancelButton"
                                            CausesValidation="False" OnClick="lbtnCancelMessage_Click">Cancel</asp:LinkButton>
                                    </div>
                                </div>
                                <div id="divView" runat="server">
                                    <p>
                                        <asp:Label ID="lblWelcomeMessage" runat="server"></asp:Label></p>
                                    <div class="yt-Form-MiniButtons">
                                        <asp:LinkButton ID="lbtnEdit" runat="server" CssClass="yt-MiniButton yt-EditButton"
                                            CausesValidation="False" OnClick="lbtnEdit_Click">Edit</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="yt-Panel yt-Panel-Tools">
                            <div class="yt-Panel-Body">
                                <h2>
                                    Contact Us</h2>
                                <address class="vcard">
                                    <span class="fn org">
                                        <asp:Label ID="lblOrg" runat="server"></asp:Label>
                                    </span><span class="adr"><span class="street-address">
                                        <asp:Label ID="lblStreetAddress" runat="server"></asp:Label>
                                    </span>
                                        <br />
                                        <span class="locality">
                                            <asp:Label ID="lblLocality" runat="server"></asp:Label>
                                        </span>
                                        <abbr class="region" title="British Columbia">
                                            <asp:Label ID="lblRegion" runat="server"></asp:Label>
                                        </abbr>
                                        <br />
                                        <span class="country-name">
                                            <asp:Label ID="lblCountry" runat="server"></asp:Label>
                                        </span><span class="postal-code">
                                            <asp:Label ID="lblPostalCode" runat="server"></asp:Label>
                                        </span><p><span><a href="javascript:void(0);" class="yt-MiniButton yt-MapButton" onclick="showMapModal(<%=_ShowMapParam%>);">
                                            Show Map</a> </span></p></span><span class="tel">
                                                <asp:Label ID="lblTelephone" runat="server"></asp:Label>
                                            </span><a id="lnkURL" runat="server" href="javascript:void(0);"></a>
                                    <br />
                                    <a id="lnkEmail" runat="server" href="javascript:void(0);">Send Us a Message</a>
                                </address>                                
                            </div>
                        </div>
                        <div id="divSearch" runat="server" class="yt-Panel yt-Panel-Tools">
                            <div class="yt-Panel-Body">
                                <h2>
                                    Search</h2>
                                <div class="yt-SearchTools">
                                    <fieldset class="yt-Form">
                                        <div class="yt-Form-Field yt-SearchKeywords">
                                            <label id="lblSearchFor" runat="server" for="txtSearchKeywords">
                                                Search for Tributes created by us:</label>
                                            <asp:TextBox ID="txtSearchKeyword" runat="server" Text="Enter the name of a Tribute"></asp:TextBox>
                                        </div>
                                        <div class="yt-SearchAdvancedLink">
                                            <a href="advancedsearch.aspx">Advanced Search</a>
                                        </div>
                                        <asp:ImageButton ID="btnSearchSubmit" CssClass="yt-Search-Submit" runat="server"
                                            ImageUrl="../assets/images/btn_go.gif" AlternateText="Search Tributes" OnClick="btnSearchSubmit_Click" />
                                    </fieldset>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--yt-ContentSecondary-->
                    <div class="hack-clearBoth">
                    </div>
                    <div class="hack-clearBoth">
                    </div>
                    <div class="yt-ContentContainerImage bgImageBUser">
                    </div>
                </div>
                <!--yt-ContentContainerInner-->
            </div>
        </div>
        <!--yt-ContentContainer-->
        <div >
            <uc2:Footer ID="Footer1" runat="server" />
        </div>
        <!--yt-Footer-->
    </div>
    <!--yt-Container-->
    <div class="upgrade">
        <h2>
            Please Upgrade Your Browser</h2>
        <p>
            This site&#39;s design is only visible in a graphical browser that supports <a href="http://www.webstandards.org/upgrade/"
                title="The Web Standards Project's BROWSER UPGRADE initiative.">web standards</a>,
            but its content is accessible to any browser or Internet device.</p>
    </div>
    </form>

    <script type="text/javascript">
    executeBeforeLoad();

    </script>

    <div id="txtHint" visible="false">
        <b></b>
    </div>
    
    <!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/BrowserOrTabCloseHandler.js"></script>

</body>
</html>
