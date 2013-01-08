<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EventFullView.aspx.cs" Inherits="Event_EventFullView"
    Title="Event" MasterPageFile="~/Shared/Story.master" %>

<%@ MasterType VirtualPath="~/Shared/Story.master" %>
<asp:Content ID="content1" ContentPlaceHolderID="TabContentPlaceHolder" runat="Server">

    <script type="text/javascript" src="../assets/scripts/modalbox.js"></script>

    <script src="http://maps.google.com/maps?file=api&amp;v=2.x&amp;key=<%=TributesPortal.Utilities.WebConfig.GoogleAPIKey%>"
        type="text/javascript"></script>

    <script type="text/javascript" src="../assets/scripts/map.js"></script>

    <script language="javascript" type="text/javascript">
    
    function ValidateRSVP(source, args)
    {
            args.IsValid = true;
    }   
    
    function ShowRSVPStatus()
    {
    }
    </script>

    <!--yt-NavPrimary-->
    <div class="yt-Breadcrumbs">
        <a id="aTributeHome" runat="server">
            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
            Home</a> <a href="events.aspx">Events</a> <span class="selected">
                <%=_EventName %>
            </span>
    </div>
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ModuleContentPlaceHolder" runat="Server">
    <div id="divShowModalPopup">
    </div>
    <div class="yt-ContentPrimary">
        <div id="lblErrMsg" align="left" runat="server" class="yt-Error" visible="false">
        </div>
        <div>
            <asp:ValidationSummary CssClass="yt-Error" ID="valsError" runat="server" Width="620px"
                HeaderText=" <h2>Oops - there was a problem with your Event details.</h2>                                                             <h3>Please correct the errors below:</h3> "
                ForeColor="Black" />
        </div>
        <div class="yt-Panel-Primary">
            <h2>
                <asp:Label ID="lblEventTitle" runat="server" Text="Event"></asp:Label></h2>
            <div class="yt-EventList yt-Single">
                <div class="yt-Form-Buttons">
                    <div class="yt-Form-Submit">
                        <asp:HyperLink ID="lnkEditEvent" runat="server" CssClass="yt-Button yt-ArrowButton">Edit Event</asp:HyperLink>
                    </div>
                </div>
                <div class="yt-ListBody">
                    <div class="yt-ListItem">
                        <h3>
                            <asp:Label ID="lblEventinfo" runat="server" Text="Event Information"></asp:Label></h3>
                        <div class="yt-ItemThumb">
                            <a class="yt-Thumb">
                                <img id="imgEventImage" runat="server" src="../assets/images/gift_placeholder.gif"
                                    alt="" width="194" height="194" /></a>
                        </div>
                        <dl>
                            <dt>
                                <asp:Label ID="lblName" runat="server" Text="Name:"></asp:Label></dt>
                            <dd>
                                <asp:Label ID="lblEventName" runat="server" Text=""></asp:Label></dd>
                            <dt>
                                <asp:Label ID="lblType" runat="server" Text="Type:"></asp:Label></dt>
                            <dd>
                                <asp:Label ID="lblEventType" runat="server" Text=""></asp:Label></dd>
                            <dt>
                                <asp:Label ID="lblCreatedBy" runat="server" Text="Created By:" Visible="false"></asp:Label></dt>
                            <dd id="ddCreatedBy" runat="server" visible="false">
                                &nbsp;</dd>
                        </dl>
                    </div>
                </div>
                <div class="yt-SectionWrapper yt-More">
                    <h3>
                        <asp:Label ID="lblTimePlace" runat="server" Text="Time and Place"></asp:Label></h3>
                    <dl>
                        <dt>
                            <asp:Label ID="lblDate" runat="server" Text="Date:"></asp:Label></dt>
                        <dd>
                            <asp:Label ID="lblEventDate" runat="server" Text=""></asp:Label></dd>
                        <dt>
                            <asp:Label ID="lblTime" runat="server" Text="Time:"></asp:Label></dt>
                        <dd>
                            <asp:Label ID="lblEventTime" runat="server" Text=""></asp:Label></dd>
                        <dt>
                            <asp:Label ID="lblLocation" runat="server" Text="Location:"></asp:Label></dt>
                        <dd>
                            <asp:Label ID="lblEventLocation" runat="server" Text=""></asp:Label></dd>
                        <dt>
                            <asp:Label ID="lblAddress" runat="server" Text="Street:"></asp:Label></dt>
                        <dd>
                            <asp:Label ID="lblEventAddress" runat="server" Text=""></asp:Label></dd>
                        <dt>
                            <asp:Label ID="lblCity" runat="server" Text="City:"></asp:Label></dt>
                        <dd>
                            <asp:Label ID="lblEventCity" runat="server" Text=""></asp:Label></dd>
                        <dt>
                            <asp:Label ID="lblState" runat="server" Text="State/Province:"></asp:Label></dt>
                        <dd>
                            <asp:Label ID="lblEventState" runat="server" Text=""></asp:Label></dd>
                        <dt>
                            <asp:Label ID="lblCountry" runat="server" Text="Country:"></asp:Label></dt>
                        <dd>
                            <asp:Label ID="lblEventCountry" runat="server" Text=""></asp:Label></dd>
                    </dl>
                    <a href="javascript:void(0);" class="yt-MiniButton yt-MapButton" onclick="showMapModal(<%=_ShowMapParam%>);">
                        Show Map</a>
                </div>
                <div class="yt-SectionWrapper yt-More">
                    <h3>
                        <asp:Label ID="lblEventHost" runat="server" Text="Event Host"></asp:Label></h3>
                    <dl>
                        <dt>
                            <asp:Label ID="lblHost" runat="server" Text="Host:"></asp:Label></dt>
                        <dd>
                            <asp:Label ID="lblEventHostName" runat="server" Text=""></asp:Label></dd>
                        <dt>
                            <asp:Label ID="lblPhone" runat="server" Text="Phone:"></asp:Label></dt>
                        <dd>
                            <asp:Label ID="lblEventPhone" runat="server" Text=""></asp:Label></dd>
                        <dt>
                            <asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label></dt>
                        <dd>
                            <asp:Label ID="lblEmailHost" runat="server" Text=""></asp:Label></dd>
                    </dl>
                </div>
                <div id="divDesc" runat="server" class="yt-SectionWrapper yt-More">
                    <h3>
                        <asp:Label ID="lblDesc" runat="server" Text="Event Description"></asp:Label></h3>
                    <p>
                        <asp:Label ID="lblEventDesc" runat="server" Text=""></asp:Label></p>
                </div>
                <div id="divRsvpStatistics" runat="server" class="yt-SectionWrapper yt-More">
                    <h3>
                        <asp:Label ID="lblGuest" runat="server" Text="RSVP Statistics"></asp:Label>
                    </h3>
                    <div>
                        <span>
                            <asp:Label ID="lblAttendingCount" runat="server" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblAttending" runat="server" Text="Attending"></asp:Label>
                        </span><span style="padding-left: 40px">
                            <asp:Label ID="lblMaybeAttendingCount" runat="server" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblMaybeAttending" runat="server" Text="Maybe Attending"></asp:Label>
                        </span><span style="padding-left: 40px">
                            <asp:Label ID="lblNotAttendingCount" runat="server" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblNotAttending" runat="server" Text="Not Attending"></asp:Label>
                        </span><span style="padding-left: 40px">
                            <asp:Label ID="lblAwaiting1" runat="server" Text="["></asp:Label>
                            <asp:Label ID="lblAwaitingCount" runat="server" Font-Bold="true"></asp:Label>
                            <asp:Label ID="lblAwaiting" runat="server" Text="Have Not Replied ]"></asp:Label>
                        </span>
                    </div>
                </div>
                <div class="yt-SectionWrapper yt-More">
                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <h3>
                                <asp:Label ID="Label1" runat="server" Text="My RSVP"></asp:Label>
                            </h3>
                            <fieldset class="yt-EventRSVP" id="fsTotalGuests" runat="server">
                                <div class="yt-Form-Field yt-Form-DropDown-Short">
                                    <asp:Label ID="lblTotalGuests" runat="server" Text="Total Guests: "></asp:Label>
                                    <asp:DropDownList ID="ddlTotalGuests" runat="server" CssClass="yt-Form-DropDown-Short"
                                        OnSelectedIndexChanged="ddlTotalGuests_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblTotalGuests1" runat="server" Text="(including you)"></asp:Label>
                                </div>
                            </fieldset>
                            <div class="hack-clearBoth">
                            </div>
                            <asp:Repeater ID="repRSVP" runat="server" OnItemDataBound="repRSVP_ItemDataBound">
                                <ItemTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblFirstName" runat="server" Font-Bold="true" Text="First Name:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPhoneNumber" runat="server" Font-Bold="true" Text="Phone Number:"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtFirstName" runat="server" Width="80%"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="valFirstName" ControlToValidate="txtFirstName"
                                                    Width="1px" ErrorMessage="First Name is a required field." Font-Bold="True" Font-Size="Medium"
                                                    ForeColor="#FF8000">!</asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPhoneNumber" runat="server" Width="80%"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPhoneNumber" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"
                                                    ErrorMessage="Invalid Phone Number" ValidationExpression="^[0-9,\-,]*$">!</asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblLastName" runat="server" Font-Bold="true" Text="Last Name:"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblEmail" runat="server" Font-Bold="true" Text="Email Address:"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtLastName" runat="server" Width="80%"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="valLastName" ControlToValidate="txtLastName"
                                                    Width="1px" ErrorMessage="Last Name is a required field." Font-Bold="True" Font-Size="Medium"
                                                    ForeColor="#FF8000">!</asp:RequiredFieldValidator>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEmail" runat="server" Width="80%"></asp:TextBox>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtEmail"
                                                    Width="1px" ErrorMessage="Email is a required field." Font-Bold="True" Font-Size="Medium"
                                                    ForeColor="#FF8000">!</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEmail" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"
                                                    ErrorMessage="Invalid Email Address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">!</asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="100%">
                                        <tr id="trMealOption" runat="server">
                                            <td>
                                                <asp:Label ID="lblMealOption" runat="server" Font-Bold="true" Text="Meal Option:"></asp:Label>
                                                <asp:DropDownList ID="ddlMealOption" runat="server" CssClass="yt-Form-DropDown-Short" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table width="100%">
                                                    <tr>
                                                        <td width="7%" valign="baseline">
                                                            <asp:Label ID="lblRsvp" runat="server" Font-Bold="true" Text="RSVP:"></asp:Label>
                                                        </td>
                                                        <td width="58%" align="left">
                                                            <asp:RadioButtonList ID="rblRSVP" runat="server" RepeatDirection="Horizontal">
                                                                <asp:ListItem Text="Attending" Value="1" Enabled="true"></asp:ListItem>
                                                                <asp:ListItem Text="Maybe Attending" Value="2" Enabled="true"></asp:ListItem>
                                                                <asp:ListItem Text="Not Attending" Value="3" Enabled="true"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            <td width="35%" align="left">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="#FF8000" Font-Bold="True"
                                                                    Font-Size="Medium" ControlToValidate="rblRSVP" runat="server" ErrorMessage="RSVP Status is a required field."
                                                                    Display="Dynamic">!</asp:RequiredFieldValidator>
                                                            </td>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <%-- <asp:RadioButton ID="rdoEventRSVPAttending" GroupName="rdoEventRSVP" runat="server"
                                                    Text="Attending" TabIndex="1" />
                                                <asp:RadioButton ID="rdoEventRSVPMaybe" GroupName="rdoEventRSVP" runat="server" Checked="false"
                                                    Text="Maybe Attending" TabIndex="2" />
                                                <asp:RadioButton ID="rdoEventRSVPNot" GroupName="rdoEventRSVP" runat="server" Checked="false"
                                                    Text="Not Attending" TabIndex="2" />      --%>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:Repeater>
                            <fieldset class="yt-EventRSVP" id="fsSelfRSVP" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblFirstNameSelfRSVP" runat="server" Font-Bold="true" Text="First Name:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblPhoneNumberSelfRSVP" runat="server" Font-Bold="true" Text="Phone Number:"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtFirstNameSelfRSVP" runat="server" Width="80%"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="valFirstNameSelfRSVP" ControlToValidate="txtFirstNameSelfRSVP"
                                                Width="1px" ErrorMessage="First Name is a required field." Font-Bold="True" Font-Size="Medium"
                                                ForeColor="#FF8000">!</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPhoneNumberSelfRSVP" runat="server" Width="80%"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtPhoneNumberSelfRSVP" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"
                                                    ErrorMessage="Invalid Phone Number" ValidationExpression="^[0-9,\-,]*$">!</asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblLastNameSelfRSVP" runat="server" Font-Bold="true" Text="Last Name:"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEmailAddressSelfRSVP" runat="server" Font-Bold="true" Text="Email Address:"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="txtLastNameSelfRSVP" runat="server" Width="80%"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="valLastNameSelfRSVP" ControlToValidate="txtLastNameSelfRSVP"
                                                Width="1px" ErrorMessage="Last Name is a required field." Font-Bold="True" Font-Size="Medium"
                                                ForeColor="#FF8000">!</asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEmailSelfRSVP" runat="server" Width="80%"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%">
                                    <tr id="trMealOptionSelfRSVP" runat="server">
                                        <td>
                                            <asp:Label ID="lblMealOptionSelfRSVP" runat="server" Font-Bold="true" Text="Meal Option:"></asp:Label>
                                            <asp:DropDownList ID="ddlMealOptionSelfRSVP" runat="server" CssClass="yt-Form-DropDown-Short" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblRsvp" runat="server" Font-Bold="true" Text="RSVP:"></asp:Label>
                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                                            </asp:RadioButtonList>
                                            <asp:RadioButton ID="rdoSelfRSVPAttending" GroupName="rdoEventSelfRSVP" runat="server"
                                                Text="Attending" TabIndex="1" />
                                            <asp:RadioButton ID="rdoSelfRSVPMaybe" GroupName="rdoEventSelfRSVP" runat="server"
                                                Checked="false" Text="Maybe Attending" TabIndex="2" />
                                            <asp:RadioButton ID="rdoSelfRSVPNot" GroupName="rdoEventSelfRSVP" runat="server"
                                                Checked="false" Text="Not Attending" TabIndex="2" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                            <div class="yt-Form-Field">
                                <asp:Label ID="lblComment" runat="server" Font-Bold="true" Text="Add a comment (optional):"></asp:Label>
                                <asp:TextBox ID="txtComment" runat="server" Height="100px" Rows="5" TextMode="MultiLine"
                                    Width="100%" MaxLength="500"></asp:TextBox>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="yt-Form-Buttons">
                        <div class="yt-Form-Submit">
                            <asp:LinkButton ID="lbtnSaveMyRsvp" runat="server" CssClass="yt-Button" OnClick="lbtnSaveMyRsvp_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <!-- Modal for loading contacts -->
            <div id="yt-RSVPSendContent">
                <div class="yt-Panel-Primary">
                    <h4>
                        <asp:Label ID="lblRSVPCount" runat="server"></asp:Label></h4>
                    <div class="yt-Form-Submit">
                        <!-- This is submit button to post entire page form -->
                        <a href="event.aspx" class="yt-Button yt-ArrowButton">OK</a>
                    </div>
                </div>
            </div>
            <div class="hack-clearBoth">
            </div>
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
                        *Upgrade this
                        <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                        to remove this advertisement</p>
                </div>
            </div>
        </div>
        <% } %>
    </div>
    <!--yt-ContentPrimary-->
</asp:Content>
