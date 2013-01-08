<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AllTribute.aspx.cs" Inherits="Tribute_AllTribute"
    Title="Tributes" MasterPageFile="~/Shared/SearchResult.master" %>
    
<asp:Content ID="content1" ContentPlaceHolderID="HeaderContentPlaceHolder" runat="Server">
    
    <script language="javascript" type="text/javascript"></script>
    
    <!--yt-NavPrimary-->
    <div class="yt-Breadcrumbs">
        <a href="<%=Session["APP_BASE_DOMAIN"]%>">Home</a> <span class="selected">All Tributes</span>
    </div>
</asp:Content>

<asp:Content ID="content2" ContentPlaceHolderID="ModuleContentPlaceHolder" runat="Server">

    <div class="yt-ContentPrimary">
    
	    <div id="lblNotice" runat="server" class="yt-Notice" visible="false" ></div>
    
    <div class="yt-Panel-Primary">
    	<h2><asp:Label ID="lblAllTrubute" runat="server" Text="All Tributes"></asp:Label></h2>
    	
    	<div id="divMessage" runat="server"></div>
        <div id="divTributeMain" class="yt-SearchResultList" runat="server" >
            <div class="yt-ListHead">
                <div class="yt-Pagination">
                    <ul>
                        <li id="liRecentlyCreated1" runat="server" ><asp:LinkButton ID="lbtnRecentlyCreated1" runat="server" OnClick="lbtnRecentlyCreated_Click">20 Most Recently Created</asp:LinkButton></li>
                        <li id="liPopular1" runat="server" ><asp:LinkButton ID="lbtnPopular1" runat="server" OnClick="lbtnPopular_Click">20 Most Popular</asp:LinkButton></li>
                    </ul>
                </div>
            </div>
            
             <div id="divTributeList" runat="server"  class="yt-ListBody">
                <asp:Repeater ID="repSearchTribute" runat="server" Visible="False" OnItemCommand="repSearchTribute_ItemCommand">
                    <ItemTemplate>
                        <div class="yt-ListItem">
                            <h4><asp:LinkButton ID="lbtnTributeName" CssClass="yt-ListName" CommandArgument='<%#Eval("TributeId")%>'
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
                                <dt>Views:</dt>
                                <dd><%#Eval("Hits")%></dd>
                                <dt>Created By:</dt>
                                <dd><a href="javascript:void(0);" onclick="UserProfileModal_1('<%# Eval("UserTributeID")%>');"><%#Eval("CreatedBy")%></a></dd>
                            	<dt>Created:</dt>
                                <dd><%#Eval("CreatedDate")%></dd>
                            </dl>      
                            <asp:HiddenField ID="hdnTributeType" runat="server" Value='<%#Eval("TributeType")%>' />
                            <asp:HiddenField ID="hdnTributeUrl" runat="server" Value='<%#Eval("TributeUrl")%>' />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            
            <div class="yt-ListFoot">
                <div class="yt-Pagination">
                    <ul>
                        <li id="liRecentlyCreated2" runat="server"><asp:LinkButton ID="lbtnRecentlyCreated2" runat="server" OnClick="lbtnRecentlyCreated_Click">20 most Recently Created</asp:LinkButton></li>
                        <li id="liPopular2" runat="server" ><asp:LinkButton ID="lbtnPopular2" runat="server" OnClick="lbtnPopular_Click">20 Most Popular</asp:LinkButton></li>
                    </ul>
                </div>
            </div>                    
                        
        </div>
	</div>
	<!--yt-TributeProcess-->
						
    </div><!--yt-ContentPrimary-->
    
</asp:Content>

