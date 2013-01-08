<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Event.aspx.cs" Inherits="Event_Event"
    Title="Events" MasterPageFile="~/Shared/Story.master" %>
<%@ MasterType VirtualPath="~/Shared/Story.master" %>

<asp:Content ID="content1" ContentPlaceHolderID="TabContentPlaceHolder" runat="Server">
    <!--yt-NavPrimary-->
    <div class="yt-Breadcrumbs">
        <a id="aTributeHome" runat="server"><%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> Home</a> <span class="selected">Events</span>
    </div>
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ModuleContentPlaceHolder" runat="Server">
<div id="divShowModalPopup"></div> 
    <div class="yt-ContentPrimary">
        <div class="yt-Panel-Primary">
            <h2>
                <asp:Label ID="lblHead" runat="server" Text="Events"></asp:Label></h2>
            <div class="yt-EventList">
                <div id="divButton" runat="server" class="yt-Form-Buttons">
                    <div class="yt-Form-Submit">
                        <%-- asp:HyperLink ID="lnkAddEvent" runat="server" CssClass="yt-Button yt-ArrowButton" NavigateUrl="~/manageevent.aspx">Add Event</asp:HyperLink --%>
                        <asp:LinkButton ID="lnkAddEvent" runat="server" CssClass="yt-Button yt-ArrowButton"
                            OnClick="lnkAddEvent_Click"></asp:LinkButton>
                    </div>
                </div>
                <div id="divMessage" runat="server">
                </div>
                <%-- <div id="divPagingTop" runat="server" class="yt-ListHead">
                    <span id="spanRecordCountTop" runat="server"></span>
                    <div class="yt-Pagination">
                        <span id="Page">Page:</span> <span id="spanPagingTop" runat="server"></span>
                    </div>
                </div>--%>
                <div class="yt-ListBody">
                    <asp:Repeater ID="repEventList" runat="server" OnItemCommand="repEventList_ItemCommand"
                        OnItemDataBound="repEventList_ItemDataBound">
                        <ItemTemplate>
                            <div class="yt-ListItem">
                                <h4>
                                    <asp:Label ID="lblEventName" runat="server" Text='<%# Eval("EventName")%>'></asp:Label></h4>
                                <div class="yt-ItemThumb">
                                    <a class="yt-Thumb">
                                        <asp:Image ID="imgEventImage" runat="server" ImageUrl='<%# Eval("EventImage")%>'
                                            AlternateText='<%# Eval("EventName")%>' Width="194" Height="194" /></a>
                                </div>
                                <dl>
                                    <dt>
                                        <asp:Label ID="lblDate" runat="server" Text="When:"></asp:Label></dt>
                                    <dd>
                                        <asp:Label ID="lblEventDate" runat="server" Text='<%# Eval("EventDateAndTime")%>'></asp:Label></dd>
                                    <dt>
                                        <asp:Label ID="lblPlace" runat="server" Text="Where:"></asp:Label></dt>
                                    <dd>
                                        <asp:Label ID="lblEventPlace" runat="server" Text='<%# Eval("EventPlace")%>'></asp:Label></dd>
                                    <!--<dt><asp:Label ID="lblCreatedBy" runat="server" Text="Created By:" Visible="false"></asp:Label></dt>
                        	        <dd id="Dd1" visible="false" runat="server"><a href="javascript:void(0);" visible="false" onclick="UserProfileModal_1('<%# Eval("UserId")%>'); "><%# Eval("UserName")%></a></dd>-->
                                </dl>
                                <p>
                                    <asp:HyperLink ID="lbtnFullEvent" runat="server">Click for full event details...</asp:HyperLink>
                                </p>
                                <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="yt-MiniButton yt-DeleteButton"
                                    CommandName="Delete">Delete</asp:LinkButton>
                                <asp:HiddenField ID="hdnEventId" runat="server" Value='<%# Eval("EventID")%>' />
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <%-- <div id="divPagingFoot" runat="server" class="yt-ListFoot">
                    <span id="spanRecordCountBottom" runat="server"></span>
                    <div class="yt-Pagination">
                        <span>Page:</span> <span id="spanPagingBottom" runat="server"></span>
                    </div>
                </div>--%>
            </div>
            <!-- this is the contents of the modal box to confirm the delete.
	             The "Delete" button in the modal is the final submit button 
                 to delete the tribute 
           
            <div id="yt-DeleteConfirmContainer">
                <div id="yt-DeleteConfirmContent" class="yt-ModalWrapper">
    	            <div class="yt-Panel-Primary">
                        <h2>Delete Event</h2>
                        <h4>Are you sure you would like to delete the Event?</h4>                                
                        <div class="yt-Form-Buttons">
                            <div class="yt-Form-Delete" id="yt-CancelContainer"></div>
                            <div class="yt-Form-Submit"><a href="javascript:void(0);" class="yt-Button yt-CheckButton">Yes, Delete Tribute</a></div>
                        </div>
		            </div>
                </div>
            </div>-->
        </div>
        <% if (this.Master._packageId == 8 || this.Master._packageId==3)
           { %>
        <div class="yt-GoogleOuter">
            <div class="yt-GoogleAdBox-BottomSmall" id="BannerAdBoxBottom" runat="server">
                <div class="yt-Scissors">
                </div>
                <div class="yt-GoogleAdContent">
                    <div>

                        <script type='text/javascript'>
                                <% if (Request.Url.ToString().ToLower().Contains("wedding"))
                                   {%>
                                    GA_googleFillSlot("YourTribute_Wedding_Events_Bottom_468x60");
                               
                                <% } %>
                                <% else if (Request.Url.ToString().ToLower().Contains("anniversary"))
                                    {%>
                                
                                     GA_googleFillSlot("YourTribute_Anniversary_Events_Bottom_468x60");
                               
                                <% } %>
                                <% else if (Request.Url.ToString().ToLower().Contains("graduation"))
                                    {%>
                               
                                    GA_googleFillSlot("YourTribute_Graduation_Events_Bottom_468x60");
                                    
                                <% } %>
                                <% else if (Request.Url.ToString().ToLower().Contains("memorial"))
                                    {%>
                                
                                     GA_googleFillSlot("YourTribute_Memorial_Events_Bottom_468x60");
                                   
                                <% } %>
                                <% else if (Request.Url.ToString().ToLower().Contains("baby"))
                                    {%>
                               
                                     GA_googleFillSlot("YourTribute_Baby_Events_Bottom_468x60");
                                   
                                <% } %>
                                <% else if (Request.Url.ToString().ToLower().Contains("birthday"))
                                    {%>                                
                                     GA_googleFillSlot("YourTribute_Birthday_Events_Bottom_468x60");                                   
                                <% } %>
                        </script>

                    </div>
                    <p class="infoMessage">
                        *Upgrade this <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%> to remove this advertisement</p>
                </div>
            </div>
        </div>
        <% } %>
    </div>
    <!--yt-ContentPrimary-->
</asp:Content>
