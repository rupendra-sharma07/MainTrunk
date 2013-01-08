<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminMytributesHome.aspx.cs"
    Inherits="MyHome_AdminMytributesHome" MasterPageFile="~/Shared/Inner.master" %>

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
	
    </script>

</asp:Content>
<asp:Content ID="content" ContentPlaceHolderID="BodyContentPlaceHolder" runat="Server">
    <div id="divShowModalPopup">
    </div>
    <div id="yt-TributeList">
        <div class="yt-AdminHeader">
            <div class="yt-Pagination" runat="server" id="paging">
                <span>Page:</span>
            </div>
        </div>
        <div>
            <asp:GridView ID="grvMyTributes" CssClass="yt-AdminTable" AutoGenerateColumns="False"
                runat="server" OnSelectedIndexChanged="grvMyTributes_SelectedIndexChanged" OnRowCommand="grvMyTributes_RowCommand"
                OnLoad="grvMyTributes_Load" OnRowCreated="grvMyTributes_RowCreated">
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                        <% if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower().Equals("yourmoments"))
                           { %>
                            <asp:Label ID="Label1" Text="My Websites" runat="server"></asp:Label>
                            <% }
                           else
                           { %>
                            <asp:Label ID="lblDate" Text="My Tributes" runat="server"></asp:Label>
                           <% } %>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtntributeName" PostBackUrl='<%#Eval("TributeHomeUrl") %>' CommandArgument='<%#Eval("TributeId") %>'
                                CommandName="Select" Text='<%# Eval("TributeName") %>' runat="server"><%--<a runat="server" href='<%#Eval("TributeHomeUrl") %>' ><%# Eval("TributeName") %></a>--%></asp:LinkButton>
                            <%--LHK: change march 17 2012--%>
                        </ItemTemplate>
                        <HeaderStyle CssClass="GridHeaderStyle" />
                        <ItemStyle Wrap="True" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:DropDownList ID="FilterDropDown" runat="server" OnSelectedIndexChanged="FilterDropDown_IndexChanged"
                                AutoPostBack="true">
                            </asp:DropDownList>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblTypeDescription" runat="server" Text='<%# Eval("TypeDescription") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="GridHeaderStyle" />
                    </asp:TemplateField>
                    <asp:ButtonField HeaderText="Manage" CommandName="Manage">
                        <HeaderStyle CssClass="GridHeaderStyle" />
                        <ControlStyle CssClass="yt-ManageLink" />
                    </asp:ButtonField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblACreated" runat="server" Text="Date Created" Width="100px"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblACreated" runat="server" Width="100px" Text='<%# Bind("StartDate","{0:MM/dd/yyyy}")%>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="GridHeaderStyle" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblExpires" runat="server" Text="Account Type (Expiry Date)" Width="100px"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnExpires" Text='<%# Bind("Enddate")%>' CommandName="Expires"
                                runat="server"></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle CssClass="GridHeaderStyle" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblVisits" runat="server" Text="Visits" Width="10px"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="txtVisits" Width="10px" runat="server" Text='<%# Eval("Visit") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="GridHeaderWidthStyle" />
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID="lblEmailAlerts" runat="server" Text="Email Alerts" Width="10px"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="cbxEmailAlerts" runat="server" AutoPostBack="true" OnCheckedChanged="cbxEmailAlerts_CheckedChanged"
                                Checked='<%# Eval("EmailAlert") %>' Width="10px" />
                        </ItemTemplate>
                        <HeaderStyle CssClass="GridHeaderWidthStyle" />
                    </asp:TemplateField>
                    <asp:TemplateField Visible="False">
                        <HeaderTemplate>
                            <asp:Label ID="lblTributeid" Visible="false" runat="server" Text="Tributeid:"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblTributeUrl" Width="1px" Text='<%# Eval("TributeUrl") %>' Visible="false"
                                runat="server"></asp:Label>
                            <asp:Label ID="lbtnTributeid" Width="1px" Text='<%# Eval("TributeId") %>' Visible="false"
                                runat="server"></asp:Label>
                            <asp:Label ID="lblRenewaldate" Width="1px" Text='<%# Bind("Renewaldate","{0:MM/dd/yyyy}")%>'
                                Visible="false" runat="server"></asp:Label>
                            <asp:Label ID="lblExpiredOn" Width="1px" Text='<%# Eval("ExpiredOn") %>' Visible="false"
                                runat="server"></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle CssClass="GridHeaderWidth" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <!-- yt-TributeList -->
</asp:Content>
<asp:Content ID="modalcontent" ContentPlaceHolderID="ModalContentPlaceHolder" runat="Server">
</asp:Content>
