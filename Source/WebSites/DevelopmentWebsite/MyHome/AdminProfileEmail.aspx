<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminProfileEmail.aspx.cs"
    Inherits="MyHome_AdminProfileEmail" MasterPageFile="~/Shared/InnerSecure.master" %>

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
<div id="divShowModalPopup"></div> 
    <div id="yt-ProfileFormContainer" class="yt-AdminContainer">
        <fieldset class="yt-Form">
            <h3>
                Email Notification Preferences</h3>
            <fieldset>
                <legend><%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> Notifications</legend>
                <p>
                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> notifications apply to all <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>s that you have created or have marked
                    as a favorite. Note that you can turn on/off ALL email alerts for specific <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>s
                    on your “My <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>s” and “My Favorites” pages.</p>
                <div class="yt-Form-Field yt-Form-Field-Checkbox">
                    <asp:CheckBox ID="cbSTORY" runat="server" Text="STORY – Email me when the story is updated." />
                    <%--<input type="checkbox" id="chkNotifyStory" checked="checked" />
                                                    <label for="chkNotifyStory">
                                                        STORY – Email me when the story is updated.</label>--%>
                </div>
                <div class="yt-Form-Field yt-Form-Field-Checkbox">
                    <asp:CheckBox ID="cbNOTES" runat="server" Text="NOTES – Email me when a new note is added." />
                    <%-- <input type="checkbox" id="chkNotifyNotes" checked="checked" />
                                                    <label for="chkNotifyNotes">
                                                        NOTES – Email me when a new note is added.</label>--%>
                </div>
                <div class="yt-Form-Field yt-Form-Field-Checkbox">
                    <asp:CheckBox ID="cbEVENTS" runat="server" Text="EVENTS – Email me when a new event is added or an existing event is updated." />
                    <%-- <input type="checkbox" id="chkNotifyEvents" checked="checked" />
                                                    <label for="chkNotifyEvents">
                                                        EVENTS – Email me when a new event is added or an existing event is updated.</label>--%>
                </div>
                <div class="yt-Form-Field yt-Form-Field-Checkbox">
                    <asp:CheckBox ID="cbGUESTBOOK" runat="server" Text="GUESTBOOK – Email me when there is a new guestbook entry." />
                </div>
                <div class="yt-Form-Field yt-Form-Field-Checkbox">
                    <asp:CheckBox ID="cbGifts" runat="server" Text="GIFTS – Email me when a new gift is given." />
                </div>
                <div class="yt-Form-Field yt-Form-Field-Checkbox">
                    <asp:CheckBox ID="cbPHOTOALBUM" runat="server" Text="PHOTO ALBUM – Email me when a new photo album is created." /></div>
                <div class="yt-Form-Field yt-Form-Field-Checkbox">
                    <asp:CheckBox ID="cbPHOTOS" runat="server" Text="PHOTOS – Email me when a new photos is added to an album." /></div>
                <div class="yt-Form-Field yt-Form-Field-Checkbox">
                    <asp:CheckBox ID="cbVideo" runat="server" Text="VIDEOS – Email me when a new video is added." /></div>
                <div class="yt-Form-Field yt-Form-Field-Checkbox">
                    <asp:CheckBox ID="cbComments" runat="server" Text="COMMENTS – Email me when a comment is made on a note, photo or video." /></div>
            </fieldset>
            <fieldset>
                <legend>General Notifications</legend>
                <p>
                    General notifications apply to your Your <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%> account.</p>
                <div class="yt-Form-Field yt-Form-Field-Checkbox">
                    <asp:CheckBox ID="cbMessages" runat="server" Text="MESSAGES – Email me when I have a new message in my inbox." />
                </div>
                <div class="yt-Form-Field yt-Form-Field-Checkbox">
                    <asp:CheckBox ID="cbNewsLetter" runat="server" Text="NEWSLETTERS – Email me newsletters periodically from Your Tribute with advice and promotions." />
                </div>
                <p>
                    NOTE: Even if you turn off all email notifications, we may sometimes need to email
                    you important notices about your account (<%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%> expiry notices, etc).</p>
                <div class="yt-Form-Buttons">
                    <div class="yt-Form-Submit">
                        <asp:LinkButton ID="btnenSaveChanges" CssClass="yt-Button yt-CheckButton" runat="server"
                            OnClick="btnenSaveChanges_Click">Save Changes</asp:LinkButton>
                    </div>
                </div>
            </fieldset>
        </fieldset>
        <!--yt-Form-->
    </div>
    <!-- yt-BillingFormContainer -->
</asp:Content>
<asp:Content ID="modalcontent" ContentPlaceHolderID="ModalContentPlaceHolder" runat="Server">
</asp:Content>
