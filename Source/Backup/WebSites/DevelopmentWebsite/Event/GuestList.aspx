<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GuestList.aspx.cs" Inherits="Event_GuestList"
    Title="Event" MasterPageFile="~/Shared/ShareTribute.master" %>

<%@ MasterType VirtualPath="~/Shared/ShareTribute.master" %>
<asp:Content ID="content1" ContentPlaceHolderID="TabContentPlaceHolder" runat="Server">

    <script src="http://maps.google.com/maps?file=api&amp;v=2.x&amp;key=<%=TributesPortal.Utilities.WebConfig.GoogleAPIKey%>"
        type="text/javascript"></script>

    <script type="text/javascript" src="../assets/scripts/map.js"></script>

    <script type="text/javascript" language="javascript" src="<%=Session["APP_BASE_DOMAIN"]%>Common/JavaScript/Event.js"></script>

    <script language="javascript" type="text/javascript">
    
    function ValidateRSVP(source, args)
    {
            args.IsValid = true;
    }
     function ValidateRSVPStatus(source, args)
     {
            var attendingVal = document.getElementById('<%= rdoEventRSVPAttending.ClientID %>');
            var maybeVal = document.getElementById('<%= rdoEventRSVPMaybe.ClientID %>');   
            var notVal = document.getElementById('<%= rdoEventRSVPNot.ClientID %>');                  
            
            if( (attendingVal.checked == false) && (maybeVal.checked == false) && (notVal.checked== false ))
            {
                args.IsValid = false;             
            }
            else
            {
                args.IsValid = true;
            }
     } 
    </script>

    <!--yt-NavPrimary-->
    <div class="yt-Breadcrumbs">
        <a id="aTributeHome" runat="server"><%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> Home</a> <a href="events.aspx">Events</a>
        <a id="aEventNameHome" runat="server">
            <%=_EventName %></a> <span class="selected">Guest List</span>
    </div>
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ModuleContentPlaceHolder" runat="Server">
<div id="divShowModalPopup"></div> 
    <div class="yt-ContentPrimary">
        <div id="lblErrMsg" align="left" runat="server" class="yt-Error" visible="false">
        </div>
        <div>
            <asp:ValidationSummary CssClass="yt-Error" ID="valsError" runat="server" Width="647px"
                HeaderText=" <h2>Oops - there was a problem with your RSVP details.</h2>                                                             <h3>Please correct the errors below:</h3> "
                ForeColor="Black" />
        </div>
        <div class="yt-Panel-Primary">
            <h2>
                <asp:Label ID="lblEventTitle" runat="server" Text="GUEST LIST"></asp:Label></h2>
            <div class="yt-EventList yt-Single">
                <div>
                    <div>
                        <h3>
                            <asp:Label ID="lblEventinfo" runat="server" Text="Event Information"></asp:Label></h3>
                        <dl class="adList">
                            <dt>
                                <asp:Label ID="lblName" runat="server" Text="Name:"></asp:Label></dt>
                            <dd>
                                <asp:Label ID="lblEventName" runat="server" Text=""></asp:Label></dd>
                            <dt>
                                <asp:Label ID="lblType" runat="server" Text="Type:"></asp:Label></dt>
                            <dd>
                                <asp:Label ID="lblEventType" runat="server" Text=""></asp:Label></dd>
                        </dl>
                    </div>
                </div>
                <div class="yt-SectionWrapper yt-More">
                    <h3>
                        <asp:Label ID="lblGuest" runat="server" Text="RSVP Statistics"></asp:Label>
                    </h3>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblAttendingCount" runat="server" Font-Bold="true"></asp:Label>
                                <asp:Label ID="lblAttending" runat="server" Text="Attending"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblMaybeAttendingCount" runat="server" Font-Bold="true"></asp:Label>
                                <asp:Label ID="lblMaybeAttending" runat="server" Text="Maybe Attending"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblNotAttendingCount" runat="server" Font-Bold="true"></asp:Label>
                                <asp:Label ID="lblNotAttending" runat="server" Text="Not Attending"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblAwaiting1" runat="server" Text="["></asp:Label>
                                <asp:Label ID="lblAwaitingCount" runat="server" Font-Bold="true"></asp:Label>
                                <asp:Label ID="lblAwaiting" runat="server" Text="Have Not Replied ]"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="yt-SectionWrapper yt-More" id="divRsvpResonse" runat="server">
                    <h3>
                        <asp:Label ID="lblRSVPResponses" runat="server" Text="RSVP Responses"></asp:Label>
                    </h3>
                    <asp:LinkButton ID="lbtnExportToCsv" runat="server" CausesValidation="false" OnClick="lbtnExportToCsv_Click"
                        Text="Export Guest List as a .CSV file" />
                    <asp:Label ID="lblExportToCsv" runat="server" Text=" (This can be opened with Microsoft Excel and other spreadsheet programs.)"></asp:Label>
                    <table width="100%">
                        <asp:Repeater ID="repRSVP" runat="server">
                            <HeaderTemplate>
                                <tr class="adGridHeader">
                                    <td>
                                        RSVP Date:
                                    </td>
                                    <td>
                                        First Name:
                                    </td>
                                    <td>
                                        Last Name:
                                    </td>
                                    <td>
                                        Email Address:
                                    </td>
                                    <td>
                                        RSVP Status:
                                    </td>
                                    <td>
                                        Full Details:
                                    </td>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="adGridRow">
                                    <td>
                                        <%# Eval("RsvpDate")%>
                                    </td>
                                    <td>
                                        <%# Eval("FirstName")%>
                                    </td>
                                    <td>
                                        <%# Eval("LastName")%>
                                    </td>
                                    <td id="tdEmailID" runat="server">
                                        <%# Eval("Email")%>
                                    </td>
                                    <td>
                                        <%# Eval("RsvpStatus")%>
                                    </td>
                                    <td>
                                        <% if (TributesPortal.Utilities.WebConfig.ApplicationMode.Equals("local"))
                                           { %>
                                        <a href="javascript:void(0);" onclick="OpenGuestListFullDetails('<%=Session["APP_BASE_DOMAIN"]%>ModelPopup/GuestListFullDetails.aspx?FirstName=<%# Eval("FirstName")%>&LastName=<%# Eval("LastName")%>&PhoneNumber=<%# Eval("PhoneNumber")%>&Email=<%# Eval("Email")%>&RsvpStatus=<%# Eval("RsvpStatus")%>&Comment=<%# Eval("Comment")%>&GuestId=<%# Eval("GuestId")%>&AdditionalGuestId=<%# Eval("AdditionalGuestId")%>&MealOption=<%# Eval("MealOption")%>');">
                                            More Info</a>
                                        <% }
                                           else
                                           { %>
                                        <a href="javascript:void(0);" onclick="OpenGuestListFullDetails('<%=TributeTypeName%>ModelPopup/GuestListFullDetails.aspx?FirstName=<%# Eval("FirstName")%>&LastName=<%# Eval("LastName")%>&PhoneNumber=<%# Eval("PhoneNumber")%>&Email=<%# Eval("Email")%>&RsvpStatus=<%# Eval("RsvpStatus")%>&Comment=<%# Eval("Comment")%>&GuestId=<%# Eval("GuestId")%>&AdditionalGuestId=<%# Eval("AdditionalGuestId")%>&MealOption=<%# Eval("MealOption")%>');">
                                            More Info</a>
                                        <% } %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <div class="yt-SectionWrapper yt-More">
                    <h3>
                        <asp:Label ID="lblAddRSVP" runat="server" Text="Add RSVP"></asp:Label>
                    </h3>
                    <table width="70%">
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
                                <asp:TextBox ID="txtFirstName" runat="server" CssClass="yt-Form-Input-Long" Font-Bold="true"
                                    Width="80%"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="valFirstName" ControlToValidate="txtFirstName"
                                    Width="1px" ErrorMessage="First Name is a required field." Font-Bold="True" Font-Size="Medium"
                                    ForeColor="#FF8000">!</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="txtPhoneNumber" runat="server" Font-Bold="true" Width="80%"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtPhoneNumber" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"
                            ErrorMessage="Invalid phone number" ValidationExpression="^[0-9,\-,]*$">!</asp:RegularExpressionValidator>
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
                                <asp:TextBox ID="txtLastName" runat="server" Font-Bold="true" Width="80%"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="valLastName" ControlToValidate="txtLastName"
                                    Width="1px" ErrorMessage="Last Name is a required field." Font-Bold="True" Font-Size="Medium"
                                    ForeColor="#FF8000">!</asp:RequiredFieldValidator>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEmail" runat="server" Font-Bold="true" Width="80%"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEmail"
                            Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ErrorMessage="Email address is a required field"
                            Width="1px">!</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
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
                                <asp:Label ID="lblRSVP" runat="server" Font-Bold="true" Text="RSVP:"></asp:Label>
                                <asp:RadioButton ID="rdoEventRSVPAttending" GroupName="rdoEventRSVP" runat="server"
                                    Text="Attending" TabIndex="1" />
                                <asp:RadioButton ID="rdoEventRSVPMaybe" GroupName="rdoEventRSVP" runat="server" Checked="false"
                                    Text="Maybe Attending" TabIndex="2" />
                                <asp:RadioButton ID="rdoEventRSVPNot" GroupName="rdoEventRSVP" runat="server" Checked="false"
                                    Text="Not Attending" TabIndex="2" /> <asp:CustomValidator ID="valRSVPStatus" runat="server" Font-Bold="True" Font-Size="Medium"
                                ForeColor="#FF8000" ClientValidationFunction="ValidateRSVPStatus" ErrorMessage=" Please select RSVP Status">!</asp:CustomValidator>
                            </td>
                        </tr>
                    </table>
                    <div class="yt-Form-Field">
                        <asp:Label ID="lblComment" runat="server" Font-Bold="true" Text="Add a comment (optional):"></asp:Label>
                        <br />
                        <asp:TextBox ID="txtComment" runat="server" Rows="5" TextMode="MultiLine" Width="70%"></asp:TextBox>
                    </div>
                    <div class="yt-Form-Buttons">
                        <div class="yt-Form-Submit">
                            <asp:LinkButton ID="lbtnAddRsvp" runat="server" CssClass="yt-Button" OnClick="lbtnAddRsvp_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <% if (Session["UserPackageID"] != null && (Session["UserPackageID"].Equals(8) || Session["UserPackageID"].Equals(3)))
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
