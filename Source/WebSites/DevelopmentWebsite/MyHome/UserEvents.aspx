<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserEvents.aspx.cs" Inherits="MyHome_UserEvents"
    Title="UserEvents" MasterPageFile="~/Shared/Inner.master" %>

<%@ MasterType VirtualPath="~/Shared/Inner.Master" %>
<asp:Content ID="header_script" ContentPlaceHolderID="HeadContentPlaceHolder" runat="Server">

    <script type="text/javascript">
    function update_user_is_connected() {
        header_user_is_connected();
        FB.XFBML.parse();
    }
    function update_user_is_not_connected() {
        header_user_is_not_connected();
        FB.XFBML.parse();
    }                     
    /* NOTE: may want to move this to an external .js */    
    InitForm = function() {
        $$('.availabilityNotice').each( function(a) {
		    a.innerHTML = '';
		    a.className = 'availabilityNotice';
	    });
    }
    
    window.addEvent('load', function() {
    
        if($('rdoPersonalAccount')) {
        /* attach personal/business toggle events */
		    $('rdoPersonalAccount').addEvent('click', function() {
			    $$('.business').each( function(a) {
				    a.style.display = 'none';
			    });
			    $$('.personal').each( function(a) {
				    a.style.display = '';
			    });
    			
			    $('yt-SignUpFormContainer').style.display = 'block';
			    InitForm();
		    });
	    }
    	
        if($('rdoBusinessAccount')) {
		    $('rdoBusinessAccount').addEvent('click', function() {
			    $$('.personal').each( function(a) {
				    a.style.display = 'none';
			    });
			    $$('.business').each( function(a) {
				    a.style.display = '';
			    });
    			
			    $('yt-SignUpFormContainer').style.display = 'block';
			    InitForm();
		    });
	    }
		
		hideWideRows();
		
	});
		
	function UserDetails_(para1,para2)
	{
	 UserDetails(para1,para2);
	 HideHeader();
	}
	
    </script>

</asp:Content>
<asp:Content ID="content" ContentPlaceHolderID="BodyContentPlaceHolder" runat="Server">
<div id="divShowModalPopup"></div> 
    <div id="eventmsg" runat="server" visible="false">
    </div>
    <div id="yt-TributeList">
        <div class="yt-AdminHeader">
            <div class="yt-Pagination" id="paging" runat="server">
                <span id="PagingText" runat="server">Page:</span>
                <ul>
                    <li>
                        <asp:LinkButton ID="lbtnPrev" CssClass="yt-PagePrevNext" CausesValidation="false"
                            Visible="false" runat="server" OnClick="lbtnPrev_Click">Prev</asp:LinkButton>
                        
                    </li>
                    <asp:DataList ID="Paginglist" runat="Server" RepeatDirection="Horizontal" OnItemCommand="Paginglist_ItemCommand"
                        RepeatLayout="Flow">
                        <ItemTemplate>
                            <li>
                                <asp:LinkButton ID="lbtncount" Text="<%# Container.DataItem %>" runat="server"></asp:LinkButton>
                            </li>
                        </ItemTemplate>
                        <SelectedItemStyle CssClass="yt-Selected" />
                    </asp:DataList>
                    <li>
                        <asp:LinkButton ID="lbtnnext" CssClass="yt-PagePrevNext" CausesValidation="false"
                            Visible="false" runat="server" OnClick="lbtnnext_Click">Next</asp:LinkButton>
                      
                    </li>
                </ul>
            </div>
        </div>
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="GridViewEvents" CssClass="yt-AdminTable" AutoGenerateColumns="False"
                        runat="server" OnRowCreated="GridViewEvents_RowCreated" OnRowCommand="GridViewEvents_RowCommand">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblMyEvent" runat="server" Text="My Events:"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnMyEvent" CommandName="Event" Text='<%# Eval("EventName") %>'
                                        runat="server"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblType" runat="server" Text="Type:"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="txtType" runat="server" Text='<%# Eval("EventDesc") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblACreated" runat="server" Text="Event Date:"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblACreated" runat="server" Text='<%# Bind("EventDate","{0:MMMM dd, yyyy}")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblRSVP" runat="server" Text="My RSVP:"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="txtRSVP" runat="server" Text='<%# Eval("EventRsvp") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbtnEventID" Text='<%# Eval("EventID") %>' Visible="false" runat="server"></asp:Label>
                                    <asp:Label ID="lbltributeID" Text='<%# Eval("TributeId") %>' Visible="false" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <!-- yt-TributeList -->
</asp:Content>
<asp:Content ID="modalcontent" ContentPlaceHolderID="ModalContentPlaceHolder" runat="Server">
</asp:Content>
