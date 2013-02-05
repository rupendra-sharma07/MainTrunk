<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BusinessUserTributesHome.aspx.cs"
    Inherits="MyHome_BusinessUserTributesHome" %>

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
    <link rel="stylesheet" type="text/css" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/ScreenPhase2.css" />
    <!-- larger text stylesheets -->
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/medium_text.css"
        title="medium_text" />
    <link rel="alternate stylesheet" type="text/css" media="screen, projection" href="<%=Session["APP_BASE_DOMAIN"]%>assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/large_text.css"
        title="large_text" />
    <!-- Website Favorite Icon -->
    <link rel="Shortcut Icon" type="image/x-icon" href="<%=Session["APP_BASE_DOMAIN"]%>assets/images/favicon.ico" />
    <!-- JS libraries -->

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/mootools-1.11.js"></script>

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/global.js"></script>

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/styleSwitcher.js"></script>

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>Common/JavaScript/Common.js"></script>

    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>Common/JavaScript/FooterControl.js"></script>

    <!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
    <script type="text/javascript" src="<%=Session["APP_BASE_DOMAIN"]%>assets/scripts/BrowserOrTabCloseHandler.js"></script>

    <script language="javascript" type="text/jscript">
    
    function OpenNewWindow(url)
    {  
        window.open(url);
    }
    
    </script>

</head>
<body>
    <form id="Form1" runat="server" action="">
    <%--<div class="yt-Container yt-BusinessHome yt-AllTributes">--%>
        <%--<div class="yt-ContentContainer">--%>
           <%-- <div id="maindiv" runat="server" class="yt-BusinessHome mainDiv">--%>
                <%--<div class="yt-ContentContainerInner">--%>
                   <%-- <div class="yt-ContentPrimaryContainer">--%>
                        <%--<div class="yt-ContentPrimary">--%>
                            <div class="yt-Panel-Primary">                                
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
                        <%--</div>--%>
                        <!--yt-ContentPrimary-->
                   <%-- </div>--%>
                    <!--yt-ContentPrimaryContainer-->
                <%--</div>--%>
                <!--yt-ContentContainerInner-->
            <%--</div>--%>
        <%--</div>--%>
        <!--yt-ContentContainer-->
    <%--</div>--%>
    </form>

    <script type="text/javascript">
    //executeBeforeLoad();

    </script>

</body>
</html>
