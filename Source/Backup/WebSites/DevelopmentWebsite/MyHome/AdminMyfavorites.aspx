<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="AdminMyfavorites.aspx.cs"
    Inherits="MyHome_AdminMyfavorites" MasterPageFile="~/Shared/Inner.master" %>

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
	
    function HideHeader()
    {
       var notice = $('Notice');
       if(notice)
       {	   	   
	   notice.innerHTML='';	 
	   notice.removeClass('yt-Notice');	 
	   notice.removeClass('yt-Error');
	   notice.style.visibility = 'hidden';
	   }
    }
	
	
	function SetHeader(message,type)
	{
	  var notice = $('Notice');
	  if(notice)
	  {
	   if(type==1)
	   notice.className='yt-Notice';
	   else
	   notice.className='yt-Error';	   
	   notice.innerHTML=message;
	   notice.style.visibility = 'visible';
	   
	   
	  }
	}
	
    </script>

</asp:Content>
<asp:Content ID="content" ContentPlaceHolderID="BodyContentPlaceHolder" runat="Server">
    <div id="msg" runat="server" visible="false">
    </div>
    <div id="divShowModalPopup"></div> 
    <div id="yt-TributeList">
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="yt-AdminHeader">
                        <div class="yt-Pagination" id="paging" runat="server">
                            <span>Page:</span>
                            <ul>
                                <li><a id="ctl00_DefaultContent_lbtnPre" visible="false" runat="server" class="yt-PagePrevNext">
                                    Prev</a> </li>
                                <asp:DataList ID="Paginglist" runat="Server" RepeatDirection="Horizontal" OnItemCommand="Paginglist_ItemCommand"
                                    RepeatLayout="Flow">
                                    <ItemTemplate>
                                        <li>
                                            <asp:LinkButton ID="lbtncount" Text="<%# Container.DataItem %>" runat="server"></asp:LinkButton>
                                        </li>
                                    </ItemTemplate>
                                    <SelectedItemStyle CssClass="yt-Selected" />
                                </asp:DataList>
                                <li><a id="ctl00_DefaultContent_lbtnNext" runat="server" href="javascript:void(0);"
                                    visible="false" class="yt-PagePrevNext">Next</a></li>
                            </ul>
                        </div>
                    </div>
                    <asp:GridView ID="gvMyFavourites" CssClass="yt-AdminTable" AutoGenerateColumns="False"
                        runat="server" OnLoad="gvMyFavourites_Load" OnRowCommand="gvMyFavourites_RowCommand"
                        OnRowCreated="gvMyFavourites_RowCreated" OnRowDataBound="gvMyFavourite_RowDataBound"
                        Width="653px">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblTributes" runat="server" Text="My Tributes"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="txtTributes" CommandName="SelectTribute" Text='<%# Eval("TributeName") %>'
                                        runat="server"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:DropDownList ID="ddlFavourite" runat="server" OnSelectedIndexChanged="ddlFavourite_IndexChanged"
                                        AutoPostBack="true">
                                    </asp:DropDownList>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTypeDescription" runat="server" Text='<%# Eval("TypeDescription") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblACreated" Text="User" runat="server"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%-- <asp:Label ID="lblACreated" runat="server" Text='<%# Bind("Enddate")%>'></asp:Label>--%>
                                    <asp:LinkButton ID="lblACreated2" CommandName="SelectUser" Text='<%# Eval("Enddate") %>'
                                        runat="server"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblEmailAlerts" runat="server" Text="Email Alerts"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbxEmailAlerts" runat="server" AutoPostBack="True" Checked='<%# Eval("EmailAlert") %>'
                                        OnCheckedChanged="cbxFavEmailAlerts_CheckedChanged" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                            </asp:TemplateField>
                            <asp:ButtonField Text="Remove" CommandName="Remove" HeaderStyle-Width="100px" HeaderText="Remove">
                                <ControlStyle CssClass="yt-MiniButton yt-DeleteButton" />
                                <HeaderStyle CssClass="GridHeaderStyle" />
                            </asp:ButtonField>
                            <asp:TemplateField Visible="False">
                                <HeaderTemplate>
                                    <asp:Label ID="lblTributeid" Visible="false" runat="server" Text="Tributeid:"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTributeUrl" Width="1px" Text='<%# Eval("TributeUrl") %>' Visible="false"
                                        runat="server"></asp:Label>
                                    <asp:Label ID="hdnlTributeid" Text='<%# Eval("TributeId") %>' Visible="false" runat="server"></asp:Label>
                                    <asp:Label ID="lblUserId" Text='<%# Eval("UserId") %>' Visible="false" runat="server"></asp:Label>
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
