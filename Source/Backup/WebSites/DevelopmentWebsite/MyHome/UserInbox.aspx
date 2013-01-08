<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeFile="UserInbox.aspx.cs"
    Inherits="MyHome_UserInbox" MasterPageFile="~/Shared/Inner.master" %>

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
	// HideHeader();
	}
	
	function GetUserDetail(para2)
	{
	 //var para1= $('HiddenField1').value;	  
	 alert('<%=SendbyUser%>');
	 // UserDetails_(para1,para2)
	}	
    </script>

</asp:Content>
<asp:Content ID="content" ContentPlaceHolderID="BodyContentPlaceHolder" runat="Server">
    <asp:HiddenField ID="hfToUserid" runat="server" />
    <asp:HiddenField ID="hfToSubject" runat="server" />
    <asp:HiddenField ID="hfPaymentMethod" runat="server" />
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <div id="divShowModalPopup"></div> 
    <div id="yt-TributeList">
        <div>
            <asp:MultiView ID="MultiViewInbox" runat="server">
                <asp:View ID="ViewInbox" runat="server">
                    <div class="yt-AdminHeader">
                        <div class="yt-Inbox-TitleNav">
                            <h4 class="yt-Inbox-Option">
                                Inbox</h4>
                            <span class="yt-Inbox-Option">
                                <asp:LinkButton ID="lbtnsentmessages1" runat="server" OnClick="lbtnsentmessages1_Click">Sent Messages</asp:LinkButton>
                            </span>
                        </div>
                        <div id="InboxMsg" runat="server" visible="false">
                        </div>
                        <div class="yt-Pagination" id="paging" runat="server">
                            <span>Page:</span>
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
                                        Visible="false" runat="server" OnClick="lbtnnext_Click">Next</asp:LinkButton></li></ul>
                        </div>
                    </div>
                    <ul class="yt-Inbox-Tools" id="inboxtools" runat="server">
                        <li>
                            <label for="ddlInboxSelect">
                                Select:</label>
                            <asp:DropDownList ID="ddlSelectInbox" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSelectInbox_SelectedIndexChanged">
                                <asp:ListItem>...</asp:ListItem>
                                <asp:ListItem>All</asp:ListItem>
                                <asp:ListItem>None</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li>
                            <asp:LinkButton ID="lbtnMarkasRead" runat="server" OnClick="lbtnMarkasRead_Click">Mark as read</asp:LinkButton></li><li>
                                <asp:LinkButton ID="lbtnMarkasUnRead" runat="server" OnClick="lbtnMarkasUnRead_Click">Mark as unread</asp:LinkButton></li><li>
                                    <asp:LinkButton ID="lbtnDelete" runat="server" OnClick="lbtnDelete_Click">Delete</asp:LinkButton></li></ul>
                    <asp:GridView ID="GridViewInbox" CssClass="yt-AdminTable" AutoGenerateColumns="False"
                        runat="server" OnRowCommand="GridViewInbox_RowCommand" OnRowCreated="GridViewInbox_RowCreated"
                        Width="643px" OnRowDataBound="GridViewInbox_RowDataBound">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbxinbox" runat="server" AutoPostBack="false" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblSender" runat="server" Text="SENDER:"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnSender" Text='<%# Eval("SendByUser") %>' runat="server"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblSubject" runat="server" Text="SUBJECT:"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnSubject" CommandName="Subject" Text='<%# Eval("Subject") %>'
                                        runat="server"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblACreated" runat="server" Text="DATE:"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblACreated" runat="server" Text='<%# Bind("SendDate","{0:MMMM dd, yyyy}")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="GridHeaderStyle" />
                            </asp:TemplateField>
                            <asp:TemplateField Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lbtnMessageId" Text='<%# Eval("MessageId") %>' Visible="false" runat="server"></asp:Label>
                                    <asp:Label ID="lblBody" Text='<%# Eval("Body") %>' Visible="false" runat="server"></asp:Label>
                                    <asp:Label ID="lblStatus" Text='<%# Eval("Status") %>' Visible="false" runat="server"></asp:Label>
                                    <asp:Label ID="lblUserImage" Text='<%# Eval("UserImage") %>' Visible="false" runat="server"></asp:Label>
                                    <asp:Label ID="lblSendByUserId" Text='<%# Eval("SendByUserId") %>' Visible="false"
                                        runat="server"></asp:Label>
                                    <asp:Label ID="lblParantMsgId" Text='<%# Eval("ParantMsgId") %>' Visible="false"
                                        runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:View>
                <asp:View ID="ViewSentMessages" runat="server">
                    <div class="yt-AdminHeader">
                        <div class="yt-Inbox-TitleNav">
                            <span class="yt-Inbox-Option">
                                <asp:LinkButton ID="lbtnInbox2" runat="server" OnClick="lbtnInbox2_Click">Inbox</asp:LinkButton>
                            </span>
                            <h4 class="yt-Inbox-Option" id="H4_1">
                                Sent Messages</h4>
                        </div>
                        <div id="divSentMsg" runat="server" visible="false">
                        </div>
                        <div class="yt-Pagination" id="paging1" runat="server">
                            <span>Page:</span>
                            <ul>
                                <li>
                                    <asp:LinkButton ID="lbtnPrev1" CssClass="yt-PagePrevNext" CausesValidation="false"
                                        Visible="false" runat="server" OnClick="lbtnPrev1_Click">Prev</asp:LinkButton>
                                </li>
                                <asp:DataList ID="Paginglist1" runat="Server" RepeatDirection="Horizontal" OnItemCommand="Paginglist1_ItemCommand"
                                    RepeatLayout="Flow">
                                    <ItemTemplate>
                                        <li>
                                            <asp:LinkButton ID="lbtncount" Text="<%# Container.DataItem %>" runat="server"></asp:LinkButton>
                                        </li>
                                    </ItemTemplate>
                                    <SelectedItemStyle CssClass="yt-Selected" />
                                </asp:DataList>
                                <li>
                                    <asp:LinkButton ID="lbtnNext1" CssClass="yt-PagePrevNext" CausesValidation="false"
                                        Visible="false" runat="server" OnClick="lbtnNext1_Click">Next</asp:LinkButton>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <ul class="yt-Inbox-Tools" id="senttools" runat="server">
                        <li>
                            <label for="ddlInboxSelect">
                                Select:</label>
                            <asp:DropDownList ID="ddlSelectSent" runat="server" AutoPostBack="True" Width="74px"
                                OnSelectedIndexChanged="ddlSelectSent_SelectedIndexChanged">
                                <asp:ListItem>...</asp:ListItem>
                                <asp:ListItem>All</asp:ListItem>
                                <asp:ListItem>None</asp:ListItem>
                            </asp:DropDownList>
                        </li>
                        <li>
                            <asp:LinkButton ID="lbtnDeleteSentMessages" runat="server" OnClick="LinkButton13_Click">Delete</asp:LinkButton></li></ul>
                    <div>
                        <asp:GridView ID="GridViewSentMessages" CssClass="yt-AdminTable" AutoGenerateColumns="False"
                            runat="server" Width="655px" OnRowCommand="GridViewSentMessages_RowCommand" OnRowCreated="GridViewSentMessages_RowCreated"
                            OnRowDataBound="GridViewSentMessages_RowDataBound">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbxinbox" runat="server" AutoPostBack="false" />
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="GridHeaderStyle" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblSender" runat="server" Text="SENT TO:"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnSender" Text='<%# Eval("SendByUser") %>' runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="GridHeaderStyle" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblSubject" runat="server" Text="SUBJECT:"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnSubject" CommandName="Subject" Text='<%# Eval("Subject") %>'
                                            runat="server"></asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="GridHeaderStyle" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:Label ID="lblACreated" runat="server" Text="DATE:"></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblACreated" runat="server" Text='<%# Bind("SendDate","{0:MMMM dd,yyyy}")%>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="GridHeaderStyle" />
                                </asp:TemplateField>
                                <asp:TemplateField Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lbtnMessageId" Text='<%# Eval("MessageId") %>' Visible="false" runat="server"></asp:Label>
                                        <asp:Label ID="lblBody" Text='<%# Eval("Body") %>' Visible="false" runat="server"></asp:Label>
                                        <asp:Label ID="lblStatus" Text='<%# Eval("Status") %>' Visible="false" runat="server"></asp:Label>
                                        <asp:Label ID="lblUserImage" Text='<%# Eval("UserImage") %>' Visible="false" runat="server"></asp:Label>
                                        <asp:Label ID="lblSendByUserId" Text='<%# Eval("SendByUserId") %>' runat="server"
                                            Visible="false"></asp:Label>
                                        <asp:Label ID="lblParantSentMsgId" Text='<%# Eval("ParantMsgId") %>' Visible="false"
                                            runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </asp:View>
                <asp:View ID="ViewFullMassage" runat="server">
                    <%--<div class="yt-Panel-Primary">--%>
                    <div class="yt-AdminHeader">
                        <div class="yt-Inbox-TitleNav">
                            <span class="yt-Inbox-Option">
                                <asp:LinkButton ID="lbtnInbox" runat="server" OnClick="lbtnInbox_Click">Inbox</asp:LinkButton>
                            </span><span class="yt-Inbox-Option">
                                <asp:LinkButton ID="lbtnSentMessages" runat="server" OnClick="lbtnSentMessages_Click">Sent Messages</asp:LinkButton>
                            </span>
                        </div>
                        <div class="yt-Pagination">
                            <ul>
                                <li>
                                    <asp:LinkButton ID="lbtnPreviousMessage" runat="server" CausesValidation="False"
                                        OnClick="lbtnPreviousMessage_Click"> Previous Message</asp:LinkButton></li>
                                <li>
                                    <asp:LinkButton ID="lbtnNextMessage" runat="server" CausesValidation="False" OnClick="lbtnNextMessage_Click">Next Message</asp:LinkButton></li>
                            </ul>
                        </div>
                    </div>
                    <div class="yt-MessageList yt-MessageView">
                        <div class="yt-ListItem">
                            <h4 id="lblMessageSubject" runat="server">
                            </h4>
                            <asp:Literal ID="litrelMailThread" runat="server"></asp:Literal>
                            <div class="yt-Form-MiniButtons" id="divbuttons" runat="server">
                                <div class="yt-Form-Submit">
                                    <asp:LinkButton CssClass="yt-MiniButton yt-DeleteButton" ID="lbtnDeleteOpenMsg" runat="server"
                                        OnClick="lbtnDeleteOpenMsg_Click">Delete</asp:LinkButton>
                                    <asp:LinkButton CssClass="yt-MiniButton yt-UndoButton" ID="lbtnMarkUnReadOpenMsg"
                                        runat="server" OnClick="lbtnMarkUnReadOpenMsg_Click">Mark as UnRead</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                    <fieldset class="yt-Form">
                        <div class="yt-Form-Field" id="divtxtPostMessage" runat="server">
                            <label for="txtarMessageDescription">
                                Reply:</label>
                            <asp:TextBox ID="txtPostMessage" TextMode="MultiLine" CssClass="yt-Form-Textarea-XLong"
                                Rows="50" Columns="6" runat="server"></asp:TextBox>
                            <div class="yt-Form-Submit">
                                <asp:LinkButton ID="ltnSendMessages" CssClass="yt-Button yt-ArrowButton" runat="server"
                                    OnClick="ltnSendMessages_Click">Send Message</asp:LinkButton>
                            </div>
                        </div>
                    </fieldset>
                    <%--  </div>--%>
                </asp:View>
            </asp:MultiView>
        </div>
    </div>
    <!-- yt-TributeList -->
</asp:Content>
<asp:Content ID="modalcontent" ContentPlaceHolderID="ModalContentPlaceHolder" runat="Server">
</asp:Content>
