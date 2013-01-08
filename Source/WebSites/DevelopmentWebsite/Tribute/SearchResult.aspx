<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchResult.aspx.cs" Inherits="Tribute_SearchResult"
    Title="Search Results" MasterPageFile="~/Shared/SearchResult.master" %>

<asp:Content ID="content1" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="Server">
    
    <script language="javascript" type="text/javascript"></script>
    
    <!--yt-NavPrimary-->
    <div class="yt-Breadcrumbs">
        <a href="<%=Session["APP_BASE_DOMAIN"]%>">Home</a> <span class="selected">Search Results</span>
    </div>
</asp:Content>

<asp:Content ID="content" ContentPlaceHolderID="ModuleContentPlaceHolder" runat="Server">
 <div id="divShowModalPopup"></div>
    <div class="yt-ContentPrimary">
    
	    <div id="lblNotice" runat="server" class="yt-Notice" visible="false" ></div>
    
    <div class="yt-Panel-Primary">
    	<h2><asp:Label ID="lblSearchResult" runat="server" Text="Search Results:"></asp:Label></h2>
        <div class="yt-SearchResultList">
            <div id="divPagingHead" runat="server" class="yt-ListHead">
                <span id="spanHeadRecordCount" runat="server"></span>
                <div class="yt-Pagination">
                    <span id="PageHead" runat="server">Page:</span> <span id="spanPagingHead" runat="server"></span>
                </div>
            </div>
                
            <div class="yt-SearchFilters">
            	<div class="yt-Form-Field">
                    <asp:Label ID="lblShowMe" runat="server" Text="Show Me:"></asp:Label>
                    <asp:DropDownList ID="ddlTributeType" runat="server" OnSelectedIndexChanged="ddlTributeType_SelectedIndexChanged" AutoPostBack="true" >
                    </asp:DropDownList>
                </div>
            	<div class="yt-Form-Field">
                    <asp:Label ID="lblSortBy" runat="server" Text="Sort By: "></asp:Label>
                    <asp:DropDownList ID="ddlSort" runat="server" OnSelectedIndexChanged="ddlSort_SelectedIndexChanged" AutoPostBack="true" >
                    </asp:DropDownList>
                </div>
            </div>
            
             <div id="divTributeList" runat="server"  class="yt-ListBody">
                <asp:Repeater ID="repSearchTribute" runat="server" Visible="False" OnItemCommand="repSearchTribute_ItemCommand">
                    <ItemTemplate>
                        <div class="yt-ListItem">
                            <h4> <asp:LinkButton ID="lbtnTributeName" CssClass="yt-ListName" CommandArgument='<%#Eval("TributeId")%>'
                                        Text='<%#Eval("TributeName")%>' runat="server"></asp:LinkButton></h4>
                            <p class="yt-ItemDate"> <%# Eval("Date1") %></p>
                            <div class="yt-ItemThumb">
                                <a class="yt-Thumb">
                                    <img src='<%#Eval("TributeImage")%>' alt='<%# Eval("TributeName")%>' width="75" height="75" /></a>
                            </div>
                            <dl class="yt-PrimaryInfo">
                                <dt>Type:</dt>
                                <dd><%#Eval("TributeType")%></dd>
                                <dt>Location:</dt>
                                <dd><%#Eval("Location")%></dd>
                            </dl>
                            <dl class="yt-SecondaryInfo">
                                <dt>Created By:</dt>
                                <dd><a href="javascript:void(0);" onclick="UserProfileModal_1('<%# Eval("UserTributeID")%>'); "><%#Eval("CreatedBy")%></a></dd>
                            	<dt>Created:</dt>
                                <dd><%#Eval("CreatedDate")%></dd>
                            </dl>      
                            <asp:HiddenField ID="hdnTributeType" runat="server" Value='<%#Eval("TributeType")%>' />
                            <asp:HiddenField ID="hdnTributeUrl" runat="server" Value='<%#Eval("TributeUrl")%>' />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            
            <div id="divPagingFoot" runat="server" class="yt-ListFoot">
                <span id="spanFootRecordCount" runat="server"></span>
                <div class="yt-Pagination">
                    <span id="PageFoot" runat="server">Page:</span> <span id="spanPagingFoot" runat="server"></span>
                </div>
            </div>   
                        
        </div>
	</div>
	<!--yt-TributeProcess-->
						
    </div><!--yt-ContentPrimary-->
    
</asp:Content>