<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminProfilePrivacy.aspx.cs"
    Inherits="MyHome_AdminProfilePrivacy" MasterPageFile="~/Shared/InnerSecure.master" %>

<%@ MasterType VirtualPath="~/Shared/InnerSecure.Master" %>
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
    App_Domain = "<%=Session["APP_BASE_DOMAIN"]%>";
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
	
	});	
    </script>

</asp:Content>
<asp:Content ID="content" ContentPlaceHolderID="BodyContentPlaceHolder" runat="Server">
    <div id="divShowModalPopup">
    </div>
    <div id="yt-ProfileFormContainer">
        <fieldset class="yt-Form">
            <h3>
                Privacy Settings</h3>
            <fieldset>
                <legend>Name Settings</legend>
                <div class="yt-Form-Field yt-Form-Field-Checkbox">
                    <asp:RadioButton ID="rdbDisplayMyFullName" runat="server" GroupName="NameSettings"
                        Text="Display My Full Name" />
                    <p>
                        Your full name will be displayed throughout the Your
                        <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                        website. This includes when users view your profile and you add content to the website
                        (post comments, upload photos, etc)</p>
                </div>
                <div class="yt-Form-Field yt-Form-Field-Checkbox">
                    <asp:RadioButton ID="rdbDisplayMyUsername" runat="server" GroupName="NameSettings"
                        Text="Display My Username" />
                    <p>
                        Your username will be displayed throughout the Your
                        <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                        website. This includes when users view your profile and you add content to the website
                        (post comments, upload photos, etc)</p>
                </div>
            </fieldset>
            <fieldset>
                <legend>Location Settings</legend>
                <div class="yt-Form-Field yt-Form-Field-Checkbox">
                    <asp:RadioButton ID="rdbDisplayMyLocation" runat="server" GroupName="LocationSettings"
                        Text="Display My Location" />
                    <p>
                        Your location will be displayed throughout the Your
                        <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                        website. This includes when users view your profile and you add content to the website
                        (post comments, upload photos, etc)</p>
                </div>
                <div class="yt-Form-Field yt-Form-Field-Checkbox">
                    <asp:RadioButton ID="rdbHideMyLocation" runat="server" GroupName="LocationSettings"
                        Text="Hide My Location" />
                    <p>
                        Your location will not be displayed anywhere on the Your
                        <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                        website.</p>
                </div>
            </fieldset>
            <fieldset>
                <legend>Message Settings</legend>
                <div class="yt-Form-Field yt-Form-Field-Checkbox">
                    <asp:RadioButton ID="rdbAllowUsers" runat="server" GroupName="MESSAGESETTINGS" Text="Allow Users To Send Me Messages" />
                    <p>
                        Users who view your profile have the ability to send you private messages. The user
                        can only send messages using our internal messaging system, your email address will
                        never be displayed on Your
                        <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>.</p>
                    <asp:RadioButton ID="rdbDoNotAllow" runat="server" GroupName="MESSAGESETTINGS" Text="Do Not Allow Users To Send Me Messages" />
                    <p>
                        Users will not be able to send you private messages.</p>
                </div>
            </fieldset>
            <fieldset>
                <legend>Visitor Display Settings</legend>
                <div class="yt-Form-Field yt-Form-Field-Checkbox">
                    <asp:RadioButton ID="rdbDisplayVisitCount" runat="server" GroupName="VisitorDisplaySettings"
                        Text="Display Visitor Counter" />
                    <p>
                        The number of visits Your
                        <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                        receives will be displayed on the <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> homepage.
                    </p>
                    <asp:RadioButton ID="rdbHideVisitCount" runat="server" GroupName="VisitorDisplaySettings"
                        Text="Hide Visitor Counter" />
                    <p>
                        Users will not be able to see the number of visits Your
                        <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                        receives.</p>
                </div>
            </fieldset>
            <div class="yt-Form-Buttons">
                <div class="yt-Form-Submit">
                    <asp:LinkButton ID="btnSaveChanges" CssClass="yt-Button yt-CheckButton" runat="server"
                        OnClick="btnSaveChanges_Click">Save Changes</asp:LinkButton>
                </div>
            </div>
        </fieldset>
        <!--yt-Form-->
    </div>
    <!-- yt-ProfileFormContainer -->
</asp:Content>
<asp:Content ID="modalcontent" ContentPlaceHolderID="ModalContentPlaceHolder" runat="Server">
</asp:Content>
