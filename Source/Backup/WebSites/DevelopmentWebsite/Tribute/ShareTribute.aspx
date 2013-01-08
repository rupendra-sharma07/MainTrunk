<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShareTribute.aspx.cs" Inherits="Tribute_ShareTribute"
    Title="Your Tribute - Share Tribute" MasterPageFile="~/Shared/ShareTribute.master" %>

<asp:Content ID="content1" ContentPlaceHolderID="TabContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">    
    function SetValidationGroup(groupName) 
    {  
        var validationSumm = document.getElementById('<%= valsError.ClientID %>');
        validationSumm.validationGroup = groupName;
        
        var validation = document.getElementById('<%= lblErrMsg.ClientID %>');
        if(validation)
        {
           validation.innerHTML = '';
           validation.style.visibility = 'hidden';
        }
    }
    
    function doProgressIndicator()
    {
        var serviceName;
        
        var username = document.getElementById('<%= txtUserName.ClientID %>').value;
        var password = document.getElementById('<%= txtPassword.ClientID %>').value;
        if ((username != "") && (password != ""))
        {
        if(document.getElementById('<%= rdoAOL.ClientID %>').checked == true)
        {
            serviceName = "AOL";
        }
        else if (document.getElementById('<%= rdoHotmail.ClientID %>').checked == true)
        {
            serviceName = "Hotmail";
        }
        else if (document.getElementById('<%= rdoYahoo.ClientID %>').checked == true)
        {
            serviceName = "Yahoo";
        }
        else if (document.getElementById('<%= rdoGmail.ClientID %>').checked == true)
        {
            serviceName = "Gmail";
        }
        
       doContactImport(serviceName);
    }
    }
    
    function checkValidEmail(source, args)
    {
        var validator = document.getElementById('<%= valCheckValidEmail.ClientID %>');
        var EmailList = (document.getElementById('<%= txtEmailAddresses.ClientID %>').value).replace(/\,/g, ";");
        
        args.IsValid = EventCheckValidEmail(validator, EmailList);
    }
    
    function closeContactPopup()
    {
        $('yt-ContactContent').injectInside(document.forms[0]);
        customModalBox.close();
    }
    
    window.addEvent('domready', function() {	
		      
		$ES("input", "yt-Addresses").each( function(a) {
			a.addEvent('click', function() {
				countGuests();
			});
		});
		countGuests();
	});
	
	// use following variable to limit Guest count update speed
	countGuests = function() {
		
		var guestCount=0;
		
		$ES("#yt-Addresses input").each( function(a) {
			if (a.checked == true) guestCount ++;
		});
		if (guestCount > 0 ) {
			$('yt-GuestCount').innerHTML = guestCount + ' Guest';
			if (guestCount > 1) $('yt-GuestCount').innerHTML += 's';
			
		} else {
			$('yt-GuestCount').innerHTML = 'No Guests';
		}
	}
	
	//to get message to display in popup for showing number of guests invited
    function ShowGuestCount()
    {   
        var count = getSelectCount('yt-Addresses');
        var textToDisplay;
       
            if (count > 1)
                textToDisplay = count + " guests have been invited to view the tribute.";
            else
                textToDisplay = count + " guest has been invited to view the tribute.";
        $('ctl00_ModuleContentPlaceHolder_lblGuestNotice').innerHTML = textToDisplay;
        doContactSend();
    }
    
    //to get the count of selected guests
    function getSelectCount(parentEl, uncheckFlag) 
    {
        var countChecked = 0;
        var countUnchecked = 0;
	    el = $(parentEl);
	    if (uncheckFlag) 
	    {
		    $ES('input', el).each( function(a) {
			    if((a.type == 'checkbox') && (a.checked)) {
				    countUnchecked = countUnchecked + 1;
			    }
		    });
	    } 
	    else 
	    {
		    $ES('input', el).each( function(a) {
			    if((a.type == 'checkbox') && (a.checked)) {
		            countChecked = countChecked + 1;
			    }
		    });
	    }
	    return countChecked;
    }
    
    </script>

    <!--yt-NavPrimary-->
    <div id="lnkBreadcrumbs" runat="server" class="yt-Breadcrumbs">
    </div>
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ModuleContentPlaceHolder" runat="Server">
    <div id="divShowModalPopup">
    </div>
    <div class="yt-ContentPrimary">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="lblErrMsg" runat="server" class="yt-Error" visible="false">
                </div>
                <div>
                    <asp:ValidationSummary CssClass="yt-Error" ID="valsError" runat="server" ValidationGroup=""
                        ForeColor="Black" HeaderText=" <h2>Oops - there was a problem with your guest detail.</h2> <br>" />
                </div>
                <div class="yt-Panel-Primary">
                    <h2>
                        <asp:Label ID="lblShare" runat="server" Text="Share Tribute"></asp:Label></h2>
                    <h4>
                        <asp:Label ID="lblShareMsg1" runat="server" Text="Invite people to view this tribute!"></asp:Label></h4>
                    <p>
                        <asp:Label ID="lblShareMsg2" runat="server" Text="Choose a design of an ecard to email to your friends and family and include a personal message. Next, enter their email addresses manually or import
                             them from another website. The ecard will include a link to this tribute website."></asp:Label></p>
                    <fieldset class="yt-Form yt-ChooseInviteDesignForm">
                        <div class="yt-SectionWrapper yt-More">
                            <h3>
                                <asp:Label ID="lblChooseDesign" runat="server" Text="Choose a Design" /></h3>
                            <p style="margin: 20px 5px 20px 5px;">
                                <asp:DropDownList ID="ddlEventCategories" runat="server" CssClass="yt-Form-DropDown"
                                    AutoPostBack="true" OnSelectedIndexChanged="ddlEventCategories_SelectedIndexChanged">
                                </asp:DropDownList>
                            </p>
                            <ul id="Ul1" class="yt-EventThemeList">
                                <asp:DataList ID="dlDesign" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
                                    OnItemCommand="dlDesign_ItemCommand" OnItemDataBound="dlDesign_ItemDataBound">
                                    <ItemTemplate>
                                        <li class="list-none">
                                            <asp:ImageButton ID="imgDesign" runat="server" ImageUrl='<%#Eval("ThemeThumbnailImage")%>'
                                                CommandName="DesignClick" />
                                        </li>
                                    </ItemTemplate>
                                </asp:DataList>
                            </ul>
                        </div>
                        <div class="yt-SectionWrapper yt-More">
                            <h3>
                                <label id="lblEnterMessage" runat="server">
                                    Enter a Message</label></h3>
                            <asp:TextBox ID="txtMessage" runat="server" CssClass="yt-Form-Textarea-Message-Long"
                                MaxLength="350" Rows="10" Columns="45" TextMode="MultiLine"></asp:TextBox>
                            <p style="margin-top: 5px">
                                <asp:Label ID="lblMessage" runat="server" Text="* If your Tribute is <b>password protected</b>, be sure to include the password in your message."></asp:Label></p>
                        </div>
                    </fieldset>
                    <fieldset class="yt-Form yt-InvitationPreviewForm">
                        <div class="yt-SectionWrapper yt-More">
                            <h3>
                                <label id="Label1" runat="server">
                                    eCard Preview</label></h3>
                            <p style="border: 2px ridge #CCCCCC; padding: 5px; margin: 1px;">
                                <asp:Image ID="imgPreviewImage" runat="server" ImageUrl="" AlternateText="No Preview Image"
                                    Width="400" Height="500" /></p>
                        </div>
                    </fieldset>
                    <fieldset class="yt-Form yt-EnterContractForm">
                        <div class="yt-ContractMain">
                            <ul class="yt-ContractNavPrimary">
                                <li id="limytribute" runat="server">
                                    <asp:LinkButton ID="lbEnterEmail" runat="server" Text="Enter Emails" OnClick="lbEnterEmail_Click"></asp:LinkButton></li>
                                <li id="limyfavourite" runat="server">
                                    <asp:LinkButton ID="lbImportEmails" runat="server" Text="Import Emails" OnClick="lbImportEmails_Click"></asp:LinkButton></li>
                                <li id="liFacebookImport" runat="server">
                                    <asp:LinkButton ID="lbImportfacebookFriends" runat="server" Text="Import Facebook Friends"
                                        OnClick="lbImportfacebookFriends_Click"></asp:LinkButton></li>
                            </ul>
                            <div class="yt-Panel-Contract">
                                <asp:MultiView ID="multiViewContact" runat="server" ActiveViewIndex="0">
                                    <asp:View ID="viewEmailContact" runat="server">
                                        <h3>
                                            <asp:Label ID="lblEmailHead" runat="server" Text="Enter Email Addresses"></asp:Label></h3>
                                        <div class="yt-Form-Field yt-EmailList">
                                            <asp:Label ID="lblEmailAddress" runat="server" Text="&lt;b&gt;Enter multiple email addresses (separate with a , or ;):&lt;/b&gt;&lt;/br&gt;For example: test@test.com, johndoe@test.com; test@email.com"></asp:Label>
                                            <br></br>
                                            <asp:TextBox ID="txtEmailAddresses" runat="server" Columns="50" Rows="15" CssClass="yt-Form-Textarea-Email-Long"
                                                TextMode="MultiLine"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="valEmailAddress" runat="server" ControlToValidate="txtEmailAddresses"
                                                Width="1px" ErrorMessage="Please enter Email address to Add" Font-Bold="True"
                                                Font-Size="Medium" ForeColor="#FF8000" ValidationGroup="EmailGroup">!</asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="valCheckValidEmail" runat="server" ClientValidationFunction="checkValidEmail"
                                                Width="1px" ErrorMessage="Email Address are not valid" Font-Bold="True" Font-Size="Medium"
                                                ForeColor="#FF8000" ValidationGroup="EmailGroup">!</asp:CustomValidator>
                                            <div class="yt-Form-MiniButtons">
                                                <div class="yt-Form-Submit">
                                                    <asp:LinkButton ID="lbtnAddContact" runat="server" CssClass="yt-MiniButton yt-AddButton"
                                                        ValidationGroup="EmailGroup" OnClick="lbtnAddContact_Click">Add</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:View>
                                    <asp:View ID="viewImportContact" runat="server">
                                        <fieldset class="yt-ContactImport" id="fsContractImport">
                                            <p>
                                                <asp:Label ID="lblEmail" runat="server" Text="Select an email service to import contacts from:"></asp:Label></p>
                                            <div class="yt-Form-Field-Radio" id="yt-Import-Hotmail">
                                                <asp:RadioButton ID="rdoHotmail" GroupName="rdoContactType" runat="server" AutoPostBack="true"
                                                    TabIndex="1" OnCheckedChanged="rdoHotmail_CheckedChanged" />
                                                <label for="rdoHotmail" id="lblHotmail" runat="server">
                                                </label>
                                            </div>
                                            <div class="yt-Form-Field-Radio" id="yt-Import-Yahoo">
                                                <asp:RadioButton ID="rdoYahoo" GroupName="rdoContactType" runat="server" AutoPostBack="true"
                                                    TabIndex="2" OnCheckedChanged="rdoYahoo_CheckedChanged" />
                                                <label for="rdoYahoo" id="lblYahoo" runat="server">
                                                </label>
                                            </div>
                                            <div class="yt-Form-Field-Radio" id="yt-Import-Gmail">
                                                <asp:RadioButton ID="rdoGmail" GroupName="rdoContactType" runat="server" AutoPostBack="true"
                                                    TabIndex="3" OnCheckedChanged="rdoGmail_CheckedChanged" />
                                                <label for="rdoGmail" id="lblGmail" runat="server">
                                                </label>
                                            </div>
                                            <div class="yt-Form-Field-Radio" id="yt-Import-AOL">
                                                <asp:RadioButton ID="rdoAOL" GroupName="rdoContactType" runat="server" AutoPostBack="true"
                                                    TabIndex="4" OnCheckedChanged="rdoAOL_CheckedChanged" />
                                                <label for="rdoAOL" id="lblAOL" runat="server">
                                                </label>
                                            </div>
                                            <div id="divContactImportLogin" visible="false" runat="server">
                                                <div class="yt-Form-Field">
                                                    <asp:Label ID="lblUserName" runat="server" Text="Username:"></asp:Label>
                                                    <asp:TextBox ID="txtUserName" runat="server" CssClass="yt-Form-Input"></asp:TextBox>
                                                    <asp:Label ID="lblEmailExtension" runat="server"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="valUserName" runat="server" ControlToValidate="txtUserName"
                                                        Width="1px" ErrorMessage="Please enter UserName to login" Font-Bold="True" Font-Size="Medium"
                                                        ForeColor="#FF8000" ValidationGroup="LoginGroup">!</asp:RequiredFieldValidator>
                                                </div>
                                                <div class="yt-Form-Field">
                                                    <asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label>
                                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="yt-Form-Input"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="valPassword" runat="server" ControlToValidate="txtPassword"
                                                        Width="1px" ErrorMessage="Please enter Password to login" Font-Bold="True" Font-Size="Medium"
                                                        ForeColor="#FF8000" ValidationGroup="LoginGroup">!</asp:RequiredFieldValidator>
                                                </div>
                                                <div class="yt-Form-Buttons">
                                                    <div class="yt-Form-Submit">
                                                        <asp:LinkButton ID="lbtnLogin" runat="server" CssClass="yt-Button yt-ArrowButton"
                                                            ValidationGroup="LoginGroup" OnClientClick="doProgressIndicator();" OnClick="lbtnLogin_Click">Log 
                                                        in</asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </asp:View>
                                    <asp:View ID="viewFacebookContact" runat="server">
                                        TO Be Implemented
                                    </asp:View>
                                </asp:MultiView>
                            </div>
                        </div>
                    </fieldset>
                    <%--<fieldset class="yt-Form yt-EnterForm">
                <h3><asp:Label ID="lblContactHead" runat="server" Text="Import Contacts"></asp:Label></h3>
                <p><asp:Label ID="lblEmail" runat="server" Text="Select an email service to import contacts from:"></asp:Label></p>
                <fieldset class="yt-ContactImport">
                    <div class="yt-Form-Field-Radio" id="yt-Import-Hotmail">
                        <asp:RadioButton ID="rdoHotmail" GroupName="rdoContactType" runat="server" AutoPostBack="true" TabIndex="1" OnCheckedChanged="rdoHotmail_CheckedChanged" />
                        <label for="rdoHotmail" id="lblHotmail" runat="server" ></label>
                    </div>
                    <div class="yt-Form-Field-Radio" id="yt-Import-Yahoo">
                        <asp:RadioButton ID="rdoYahoo" GroupName="rdoContactType" runat="server" AutoPostBack="true"  TabIndex="2" OnCheckedChanged="rdoYahoo_CheckedChanged" />
                        <label for="rdoYahoo" id="lblYahoo" runat="server" ></label>
                    </div>
                    <div class="yt-Form-Field-Radio" id="yt-Import-Gmail">
                        <asp:RadioButton ID="rdoGmail" GroupName="rdoContactType" runat="server" AutoPostBack="true"  TabIndex="3" OnCheckedChanged="rdoGmail_CheckedChanged" />
                        <label for="rdoGmail" id="lblGmail" runat="server" ></label>
                    </div>
                    <div class="yt-Form-Field-Radio" id="yt-Import-AOL">
                        <asp:RadioButton ID="rdoAOL" GroupName="rdoContactType" runat="server" AutoPostBack="true"  TabIndex="4" OnCheckedChanged="rdoAOL_CheckedChanged" />
                        <label for="rdoAOL" id="lblAOL" runat="server" ></label>
                    </div>
                
                    <div id="divContactImportLogin" visible="false" runat="server" >
                        <div class="yt-Form-Field">
                            <asp:Label ID="lblUserName" runat="server" Text="Username:"></asp:Label>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="yt-Form-Input"></asp:TextBox>
                            <asp:Label ID="lblEmailExtension" runat="server" ></asp:Label>
                            <asp:RequiredFieldValidator ID="valUserName" runat="server" ControlToValidate="txtUserName" Width="1px" 
                                   ErrorMessage="Please enter UserName to login" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ValidationGroup="LoginGroup">!</asp:RequiredFieldValidator>
                        </div>
                        <div class="yt-Form-Field">
                            <asp:Label ID="lblPassword" runat="server" Text="Password:"></asp:Label>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="yt-Form-Input"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="valPassword" runat="server" ControlToValidate="txtPassword" Width="1px" 
                                   ErrorMessage="Please enter Password to login" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ValidationGroup="LoginGroup" >!</asp:RequiredFieldValidator>
                        </div>
                        <div class="yt-Form-Buttons">
                            <div class="yt-Form-Submit">
                                <asp:LinkButton ID="lbtnLogin" runat="server" CssClass="yt-Button yt-ArrowButton" ValidationGroup="LoginGroup" OnClientClick="doProgressIndicator();" OnClick="lbtnLogin_Click">Log in</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </fieldset>
                
                <h3><asp:Label ID="lblEmailHead" runat="server" Text="Enter Email Addresses"></asp:Label></h3>
                <div class="yt-Form-Field yt-EmailList">
                    <asp:Label ID="lblEmailAddress" runat="server" Text="Enter multiple email addresses (separate with a , or ;):"></asp:Label>
                    <asp:TextBox ID="txtEmailAddresses" runat="server" Columns="50" Rows="6" CssClass="yt-Form-Textarea" TextMode="MultiLine" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="valEmailAddress" runat="server" ControlToValidate="txtEmailAddresses" Width="1px" 
                                       ErrorMessage="Please enter Email address to Add" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ValidationGroup="EmailGroup" >!</asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="valCheckValidEmail" runat="server" ClientValidationFunction="checkValidEmail" Width="1px" 
                            ErrorMessage="Email Address are not valid" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ValidationGroup="EmailGroup" >!</asp:CustomValidator>
                    <div class="yt-Form-MiniButtons">
                        <div class="yt-Form-Submit">
                            <asp:LinkButton ID="lbtnAddContact" runat="server" CssClass="yt-MiniButton yt-AddButton" ValidationGroup="EmailGroup" OnClick="lbtnAddContact_Click" >Add</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <h3>
                    <asp:Label ID="lblPersonalMessage" runat="server" Text="Add a Personal Message:"></asp:Label></h3>
                <div class="yt-Form-Field">
                    <asp:TextBox ID="txtShare" Columns="50" Rows="4"  TextMode="MultiLine" CssClass="yt-Form-Textarea-XLong" runat="server"></asp:TextBox>
                </div>
            </fieldset>--%>
                    <asp:UpdatePanel ID="UpdatePanelMoreAbout" runat="server">
                        <ContentTemplate>
                            <fieldset class="yt-Form yt-InviteForm">
                                <h3>
                                    <asp:Label ID="lblGuestInvite" runat="server" Text="Guests to Invite"></asp:Label></h3>
                                <div class="yt-SelectTools">
                                    <a href="javascript:void(0);" onclick="selectAll('yt-Addresses', false);">Select All</a>
                                    / <a href="javascript:void(0);" onclick="selectAll('yt-Addresses', true);">Deselect
                                        All</a>
                                </div>
                                <ul id="yt-Addresses" class="yt-AddressList">
                                    <asp:Repeater ID="repAddressList" runat="server">
                                        <ItemTemplate>
                                            <li class="vcard">
                                                <asp:CheckBox ID="chkAddress" runat="server" Checked="true" />
                                                <asp:Label ID="lblAddress" runat="server"><span class="fn"></span><span class="email">
                                                    <asp:Label ID="lblEmail" runat="server" Text='<%# Container.DataItem %>'></asp:Label></span></asp:Label>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                                <div class="yt-ContactTools">
                                    <span id="yt-GuestCount"></span>
                                    <div class="yt-Form-Submit">
                                        <asp:LinkButton ID="lbtnRemove" runat="server" CssClass="yt-MiniButton yt-DeleteButton"
                                            OnClick="lbtnRemove_Click">Remove</asp:LinkButton>
                                    </div>
                                </div>
                            </fieldset>
                            <div class="yt-Form-Buttons">
                                <div class="yt-Form-Cancel">
                                    <asp:LinkButton ID="lbtnCancel" runat="server" OnClick="lbtnCancel_Click">Cancel</asp:LinkButton>
                                </div>
                                <div class="yt-Form-Submit">
                                    <asp:LinkButton ID="lbtnInviteGuest" runat="server" CssClass="yt-Button yt-ArrowButton"
                                        OnClick="lbtnInviteGuest_Click">Share!</asp:LinkButton>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <!-- Modal for loading contacts -->
                            <div id="yt-ContactSendContent">
                                <div class="yt-Panel-Primary">
                                    <h4>
                                        <asp:Label ID="lblGuestNotice" runat="server" Text=""></asp:Label></h4>
                                    <div class="yt-Form-Submit">
                                        <!-- This is submit button to post entire page form -->
                                        <a id="lbtnPreviousPage" runat="server" class="yt-Button yt-ArrowButton">OK</a>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="hack-clearBoth">
                    </div>
                </div>
                <!--yt-PanelPrimary-->
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <!--yt-ContentPrimary-->
</asp:Content>
<asp:Content ID="content3" ContentPlaceHolderID="ImageListContentPlaceHolder" runat="Server">
    <div id="yt-ContactContent" class="yt-ModalWrapper">
        <div class="yt-Panel-Primary">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <h2>
                        <asp:Label ID="lblContact" runat="server" Text="Import Contacts"></asp:Label></h2>
                    <p>
                        <asp:Label ID="lblContactMsg1" runat="server" Text="20 contacts have been imported from Hotmail."></asp:Label></p>
                    <p>
                        <asp:Label ID="lblContactMsg2" runat="server" Text="Please select who you would like to invite from the list below, then click the ‘Add’ button to continue."></asp:Label></p>
                    <div class="yt-SelectTools">
                        <a href="javascript:void(0);" onclick="selectAll('yt-ContactList', false);">Select All</a>
                        / <a href="javascript:void(0);" onclick="selectAll('yt-ContactList', true);">Deselect
                            All</a>
                    </div>
                    <ul id="yt-ContactList" class="yt-AddressList">
                        <asp:Repeater ID="repContactList" runat="server">
                            <ItemTemplate>
                                <li class="vcard">
                                    <asp:CheckBox ID="chkContact" runat="server" Checked="false" />
                                    <asp:Label ID="lblContact" runat="server"><span class="fn">
                                        <%# Eval("ConatctName")%></span> <span class="email">
                                            <asp:Label ID="lblContactEmail" runat="server" Text='<%# Eval("Email")%>'></asp:Label></span></asp:Label>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                    <div class="yt-Form-Buttons">
                        <div class="yt-Form-Submit">
                            <asp:LinkButton ID="lbtnAdd" runat="server" OnClick="lbtnAdd_Click">Add</asp:LinkButton>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
