<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageEvent.aspx.cs" Inherits="Event_ManageEvent"
    Title="Add/Edit Event" MasterPageFile="~/Shared/Story.master" %>

<%@ MasterType VirtualPath="~/Shared/Story.master" %>
<asp:Content ID="content1" ContentPlaceHolderID="TabContentPlaceHolder" runat="Server">

    <script language="javascript" type="text/javascript">
    $(document).addEvent('fb_connected', function() {
        $('facebook_share_container').setStyle('display', 'block');
        $('<%= facebook_share.ClientID %>').checked = true;
    });
    
    function typeMoreSelect() 
    {       
        if( document.getElementById('<%= ddlEventType.ClientID %>').value == "other" )
        {
            document.getElementById('<%= txtOtherType.ClientID %>').className  = "yt-Form-Input yt-MoreTypeText yt-Shown";
        }
        else
        {
            document.getElementById('<%= txtOtherType.ClientID %>').className = "yt-Form-Input yt-MoreTypeText";
        }
    }
    
    function AddOtherType(source, args)
    {
        args.IsValid = EventsAddOtherType(document.getElementById('<%= ddlEventType.ClientID %>').value, 
                                            document.getElementById('<%= txtOtherType.ClientID %>').value);
    }
    
    function SetImageURL(url) 
    {
        document.getElementById('<%=imgEventImage.ClientID%>').src = url;
        document.getElementById('<%=hdnEventImageURL.ClientID%>').value = url;
        
        $('yt-ThumbSelection').injectInside('yt-ThumbContainer');
	    customModalBox.close();
    }
    
    function DateRequire(source, args)
    {
        var validation = document.getElementById('<%= lblErrMsg.ClientID %>');
        if(validation)
        {
           validation.innerHTML = '';
           validation.style.visibility = 'hidden';
        }

        var day = Number(document.getElementById('<%= ddlDay.ClientID %>').value);
        var month = Number(document.getElementById('<%= ddlMonth.ClientID %>').value);
        var year = document.getElementById('<%= txtYear.ClientID %>').value;
            
        args.IsValid = EventsDateRequire(day, month, year);
       
    }
    
    function ValidDate(source, args)
    {   
        var day = Number(document.getElementById('<%= ddlDay.ClientID %>').value);
        var month = Number(document.getElementById('<%= ddlMonth.ClientID %>').value);
        var year = Number(document.getElementById('<%= txtYear.ClientID %>').value);
        
        if (EventsDateRequire(day, month, year) == false)
        {
            args.IsValid = true;
        }
        else
        {
            args.IsValid = EventValidDate(day, month, year);
        }
    }
    
    function checkDate(source, args)
    {
        var day = Number(document.getElementById('<%= ddlDay.ClientID %>').value);
        var month = Number(document.getElementById('<%= ddlMonth.ClientID %>').value);
        var year = Number(document.getElementById('<%= txtYear.ClientID %>').value);
            
        var today = new Date( '<%=todayYear%>', '<%=todayMonth%>' - 1, '<%=todayDay%>' );
        args.IsValid = EventCheckDate(day, month, year, today);
    }
    
    function CheckTime(source, args)
    {
        var hourStart = Number(document.getElementById('<%= ddlHourStart.ClientID %>').value);
        var minuteStart = Number(document.getElementById('<%= ddlMinuteStart.ClientID %>').value);
        var AMPMStart = document.getElementById('<%= ddlAMPMStart.ClientID %>').value;
        
        var hourEnd = Number(document.getElementById('<%= ddlHourEnd.ClientID %>').value);
        var minuteEnd = Number(document.getElementById('<%= ddlMinuteEnd.ClientID %>').value);
        var AMPMEnd = document.getElementById('<%= ddlAMPMEnd.ClientID %>').value;
        
        args.IsValid = EventCheckTime(hourStart, minuteStart, AMPMStart, hourEnd, minuteEnd, AMPMEnd);
    }   

    
     function ValidatePhoneNumber(source, args)
     {    
         var number1=  document.getElementById('<%= txtPhoneNumber1.ClientID %>');
         var number2=  document.getElementById('<%= txtPhoneNumber2.ClientID %>');
         var number3=  document.getElementById('<%= txtPhoneNumber3.ClientID %>');
         var validator=  document.getElementById('<%= valPhoneNumber.ClientID %>');     
     
        args.IsValid= PhoneNumberValidate(number1,number2,number3,validator);        
     }
    
    function ValidatePrivacy(source, args)
     {
            var privateVal = document.getElementById('<%= rdoPrivate.ClientID %>');
            var publicVal = document.getElementById('<%= rdoPublic.ClientID %>');                     
            
            if( (privateVal.checked == false) && (publicVal.checked == false) )
            {
                args.IsValid = false;             
            }
            else
            {
                args.IsValid = true;
            }
     }
     
    function SetImage(url) 
    {
        document.getElementById('<%=imgEventImage.ClientID%>').src = url;
        document.getElementById('<%=hdnEventImageURL.ClientID%>').value = url;
    }
    
    function CheckDescLength()
    { 
	     var textarea =  document.getElementById('<%=txtEventDesc.ClientID%>'); 
	     ValidateStoryLength(textarea, 1000);
    }
    
    </script>

    <!--yt-NavPrimary-->
    <div class="yt-Breadcrumbs">
        <a id="aTributeHome" runat="server">
            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
            Home</a> <a href="events.aspx">Events</a> <span class="selected">Add/Edit Event</span>
    </div>
</asp:Content>
<asp:Content ID="content2" ContentPlaceHolderID="ModuleContentPlaceHolder" runat="Server">
    <div id="divShowModalPopup">
    </div>
    <div class="yt-ContentPrimary">
        <div id="lblErrMsg" runat="server" class="yt-Error" visible="false">
        </div>
        <asp:ValidationSummary CssClass="yt-Error" ID="valsError" runat="server" ForeColor="Black"
            HeaderText=" <h2>Oops - there was a problem with your Event detail.</h2> <br/>" />
        <div class="yt-Panel-Primary">
            <h2>
                <asp:Label ID="lblHead" runat="server" Text="Add/Edit Event"></asp:Label></h2>
            <fieldset class="yt-Form">
                <div class="yt-Form-Field">
                    <label id="lblRequiredFields" runat="server">
                        Required fields are indicated with<em class="required">* </em>
                    </label>
                </div>
                <div class="yt-EventList yt-Single">
                    <div class="yt-ListBody">
                        <div class="yt-ListItem" style="left: 0px; width: 628px; top: 0px">
                            <h3>
                                <asp:Label ID="lblEventinfo" runat="server" Text="Event Information"></asp:Label></h3>
                            <div class="yt-ItemThumb">
                                <a class="yt-Thumb" id="yt-SelectedThumb">
                                    <img id="imgEventImage" runat="server" src="../assets/images/gift_placeholder.gif"
                                        alt="" width="194" height="194" />
                                </a><a href="javascript:void(0);" class="yt-MiniButton yt-UploadPhotoButton" onclick="chooseThumb();">
                                    Choose an Image</a> <a href="javascript:void(0);" class="yt-MiniButton yt-UploadPhotoButton"
                                        onclick="uploadTributePhoto();">Upload an Image</a>
                                <asp:HiddenField ID="hdnEventImageURL" runat="server" />
                            </div>
                            <div class="yt-Form-Field">
                                <label id="lblEventName" runat="server">
                                    <em class="required">* </em>Name:</label>
                                <asp:TextBox ID="txtEventName" runat="server" CssClass="yt-Form-Input" MaxLength="40"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="valEventName" runat="server" ControlToValidate="txtEventName"
                                    Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ErrorMessage="Name is a required field"
                                    Width="1px">!</asp:RequiredFieldValidator>
                            </div>
                            <div class="yt-Form-Field">
                                <label id="lblEventType" runat="server">
                                    <em class="required">* </em>Type:</label>
                                <asp:DropDownList ID="ddlEventType" runat="server" CssClass="yt-Form-DropDown">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtOtherType" runat="server" CssClass="yt-Form-Input yt-MoreTypeText"
                                    MaxLength="100"></asp:TextBox>
                                <asp:CustomValidator ID="valAddOtherTpe" runat="server" ClientValidationFunction="AddOtherType"
                                    Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ErrorMessage="Event Type is a Required Field">!</asp:CustomValidator>&nbsp;
                            </div>
                            <dl>
                                <dt>
                                    <asp:Label ID="lblCreatedBy" runat="server" Text="Created By:" Visible="false"></asp:Label></dt>
                                <dd id="ddCreatedBy" runat="server" visible="false">
                                    &nbsp;</dd>
                            </dl>
                        </div>
                    </div>
                </div>
                <div class="yt-SectionWrapper yt-More">
                    <h3>
                        <asp:Label ID="lblTime" runat="server" Text="Time and Place"></asp:Label></h3>
                    <fieldset class="yt-Date-Fields">
                        <legend id="lblEventDate" runat="server"><em class="required">* </em>Date:</legend>
                        <div class="yt-Form-Field">
                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="yt-Form-DropDown">
                            </asp:DropDownList>
                            <label id="lblMonth" runat="server">
                                Month</label>
                        </div>
                        <div class="yt-Form-Field">
                            <asp:DropDownList ID="ddlDay" runat="server" CssClass="yt-Form-DropDown-Short">
                                <asp:ListItem Value="0" Text=""></asp:ListItem>
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
                                <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                <asp:ListItem Value="31" Text="31"></asp:ListItem>
                            </asp:DropDownList>
                            <label id="lblDay" runat="server">
                                Day</label>
                        </div>
                        <div class="yt-Form-Field">
                            <asp:TextBox ID="txtYear" runat="server" MaxLength="4" CssClass="yt-Form-Input-Short"></asp:TextBox>&nbsp;
                            <span id="Indecator1" runat="server" style="color: #FF8000; font-size: Medium; font-weight: bold;"
                                visible="false">!</span>
                            <asp:CustomValidator ID="valRequireDate" runat="server" ClientValidationFunction="DateRequire"
                                ErrorMessage="Date is Require Field" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000">!</asp:CustomValidator>
                            <asp:CustomValidator ID="valValidDate" runat="server" ClientValidationFunction="ValidDate"
                                ErrorMessage="Date is not in valid form" Font-Bold="True" Font-Size="Medium"
                                ForeColor="#FF8000">!</asp:CustomValidator>
                            <asp:CustomValidator ID="valCheckDate" runat="server" ClientValidationFunction="checkDate"
                                ErrorMessage="Event date can't be less than the current date" Font-Bold="True"
                                Font-Size="Medium" ForeColor="#FF8000">!</asp:CustomValidator>
                            <label id="lblYear" runat="server">
                                Year</label>
                        </div>
                    </fieldset>
                    <fieldset class="yt-Date-Fields">
                        <legend id="lblEventTime" runat="server"><em class="required">* </em>Time:</legend>
                        <div class="yt-Form-Field">
                            <asp:DropDownList ID="ddlHourStart" runat="server" CssClass="yt-Form-DropDown-Short">
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
                                <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                <asp:ListItem Value="12" Text="12"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="yt-Form-Field">
                            <asp:DropDownList ID="ddlMinuteStart" runat="server" CssClass="yt-Form-DropDown-Short">
                                <asp:ListItem Value="0" Text="00"></asp:ListItem>
                                <asp:ListItem Value="1" Text="05"></asp:ListItem>
                                <asp:ListItem Value="2" Text="10"></asp:ListItem>
                                <asp:ListItem Value="3" Text="15"></asp:ListItem>
                                <asp:ListItem Value="4" Text="20"></asp:ListItem>
                                <asp:ListItem Value="5" Text="25"></asp:ListItem>
                                <asp:ListItem Value="6" Text="30"></asp:ListItem>
                                <asp:ListItem Value="7" Text="35"></asp:ListItem>
                                <asp:ListItem Value="8" Text="40"></asp:ListItem>
                                <asp:ListItem Value="9" Text="45"></asp:ListItem>
                                <asp:ListItem Value="10" Text="50"></asp:ListItem>
                                <asp:ListItem Value="11" Text="55"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="yt-Form-Field">
                            <asp:DropDownList ID="ddlAMPMStart" runat="server" CssClass="yt-Form-DropDown-Short">
                                <asp:ListItem Value="0" Text="am"></asp:ListItem>
                                <asp:ListItem Value="1" Text="pm"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <span class="yt-To">to</span>
                        <div class="yt-Form-Field">
                            <asp:DropDownList ID="ddlHourEnd" runat="server" CssClass="yt-Form-DropDown-Short">
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
                                <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                <asp:ListItem Value="12" Text="12"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="yt-Form-Field">
                            <asp:DropDownList ID="ddlMinuteEnd" runat="server" CssClass="yt-Form-DropDown-Short">
                                <asp:ListItem Value="0" Text="00"></asp:ListItem>
                                <asp:ListItem Value="1" Text="05"></asp:ListItem>
                                <asp:ListItem Value="2" Text="10"></asp:ListItem>
                                <asp:ListItem Value="3" Text="15"></asp:ListItem>
                                <asp:ListItem Value="4" Text="20"></asp:ListItem>
                                <asp:ListItem Value="5" Text="25"></asp:ListItem>
                                <asp:ListItem Value="6" Text="30"></asp:ListItem>
                                <asp:ListItem Value="7" Text="35"></asp:ListItem>
                                <asp:ListItem Value="8" Text="40"></asp:ListItem>
                                <asp:ListItem Value="9" Text="45"></asp:ListItem>
                                <asp:ListItem Value="10" Text="50"></asp:ListItem>
                                <asp:ListItem Value="11" Text="55"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="yt-Form-Field">
                            <asp:DropDownList ID="ddlAMPMEnd" runat="server" CssClass="yt-Form-DropDown-Short">
                                <asp:ListItem Value="0" Text="am"></asp:ListItem>
                                <asp:ListItem Value="1" Text="pm"></asp:ListItem>
                            </asp:DropDownList>
                            <asp:CustomValidator ID="valCheckTime" runat="server" ClientValidationFunction="CheckTime"
                                ErrorMessage="Start Time should be less than the End Time" Font-Bold="True" Font-Size="Medium"
                                ForeColor="#FF8000">!</asp:CustomValidator>
                        </div>
                    </fieldset>
                    <div class="yt-Form-Field">
                        <label id="lblLocation" runat="server">
                            <em class="required">* </em>Location:</label>
                        <asp:TextBox ID="txtLocation" runat="server" CssClass="yt-Form-Input-Long"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="valRequireLocation" runat="server" ControlToValidate="txtLocation"
                            Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ErrorMessage="Location is a required field"
                            Width="1px">!</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="valLocation" runat="server" ControlToValidate="txtLocation"
                            ErrorMessage="Please enter valid Location." ValidationExpression="^[a-zA-Z0-9,\#,\-,\s\'\.]*$"
                            Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000">!</asp:RegularExpressionValidator>
                    </div>
                    <div class="yt-Form-Field">
                        <label id="lblAddress" runat="server">
                            <em class="required">* </em>Address:</label>
                        <asp:TextBox ID="txtAddress" runat="server" CssClass="yt-Form-Input-Long" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="valRequireAddress" runat="server" ControlToValidate="txtAddress"
                            Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ErrorMessage="Event Address is a required field."
                            Width="1px">!</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="valaddress" runat="server" ControlToValidate="txtAddress"
                            ErrorMessage="Please enter valid Address." ValidationExpression="^[a-zA-Z0-9,\#,\-,\s\'\.]*$"
                            Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000">!</asp:RegularExpressionValidator>
                    </div>
                    <div class="yt-Form-Field">
                        <label id="lblCity" runat="server">
                            <em class="required">* </em>City:</label>
                        <asp:TextBox ID="txtCity" runat="server" CssClass="yt-Form-Input-Long" MaxLength="50"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="valRequireCity" runat="server" ControlToValidate="txtCity"
                            Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ErrorMessage="City is a required field"
                            Width="1px">!</asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="valCity" runat="server" ControlToValidate="txtCity"
                            ErrorMessage="Invalid City. Only Letters, numbers, - and # are allowed" ValidationExpression="^[a-zA-Z0-9,\#,\-,\s\'\.]*$"
                            Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000">!</asp:RegularExpressionValidator>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanelLocation" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <div class="yt-Form-Field">
                                <label id="lblCountry" runat="server">
                                    <em class="required">* </em>Country:</label>
                                <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" CssClass="yt-Form-DropDown-Long"
                                    OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                            <div class="yt-Form-Field">
                                <label id="lblState" runat="server">
                                    State / Province:</label>
                                <asp:DropDownList ID="ddlState" runat="server" CssClass="yt-Form-DropDown-Long">
                                </asp:DropDownList>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="yt-SectionWrapper yt-More">
                    <h3>
                        <asp:Label ID="lblEventHost" runat="server" Text="Event Host"></asp:Label></h3>
                    <div class="yt-Form-Field">
                        <label id="lblHost" runat="server">
                            <em class="required">* </em>Host:</label>
                        <asp:TextBox ID="txtHost" runat="server" CssClass="yt-Form-Input-Long" MaxLength="40"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="valHost" runat="server" ControlToValidate="txtHost"
                            Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ErrorMessage="Host Name is a required field"
                            Width="1px">!</asp:RequiredFieldValidator>
                    </div>
                    <div class="yt-Form-Field">
                        <label id="lblPhone" runat="server">
                            Phone Number:</label>
                        (<asp:TextBox ID="txtPhoneNumber1" runat="server" Width="34px" MaxLength="3" CssClass="yt-Form-Input-Long"></asp:TextBox>)
                        <asp:TextBox ID="txtPhoneNumber2" runat="server" Width="34px" MaxLength="3" CssClass="yt-Form-Input-Long"></asp:TextBox>
                        -
                        <asp:TextBox ID="txtPhoneNumber3" runat="server" Width="40px" MaxLength="4" CssClass="yt-Form-Input-Long"></asp:TextBox>
                        <asp:CustomValidator ID="valPhoneNumber" runat="server" Font-Bold="True" Font-Size="Medium"
                            ForeColor="#FF8000" ErrorMessage="Enter 10 digit numeric value in phone number."
                            ClientValidationFunction="ValidatePhoneNumber">!</asp:CustomValidator>
                    </div>
                    <div class="yt-Form-Field">
                        <label id="lblEmail" runat="server">
                            Email address:</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="yt-Form-Input-Long" MaxLength="250"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                            ErrorMessage="Invalid Email Address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">!</asp:RegularExpressionValidator>
                    </div>
                </div>
                <div id="divDesc" runat="server" class="yt-SectionWrapper yt-More">
                    <h3>
                        <label id="lblEventDesc" runat="server">
                            Description:</label></h3>
                    <asp:TextBox ID="txtEventDesc" runat="server" CssClass="yt-Form-Textarea-XLong" MaxLength="1000"
                        Rows="6" Columns="50" TextMode="MultiLine"></asp:TextBox>
                </div>
                <!--Start- Controls Added for Meal Selections and Additional option-->
                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div class="yt-SectionWrapper yt-More">
                            <h3>
                                <label id="lblMealSelections" runat="server">
                                    Meal Selections</label></h3>
                            <asp:RadioButtonList ID="rbtnlstMealSelection" RepeatDirection="Vertical" runat="server"
                                RepeatLayout="Flow" OnSelectedIndexChanged="rbtnlstMealSelection_SelectedIndexChanged"
                                CausesValidation="false" AutoPostBack="true">
                                <%--<asp:ListItem Text="Ask for meal preferences" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Don't ask for meal preferences" Value="0"></asp:ListItem>--%>
                            </asp:RadioButtonList>
                            <br />
                            <br />
                            <div id="divMealOptions" runat="server">
                                <div class="yt-Form-Field">
                                    <label id="lblMealOption" runat="server">
                                        Meal option:</label>
                                </div>
                                <div class="yt-Form-Field">
                                    <asp:TextBox ID="txtMealOption" runat="server" CssClass="yt-Form-Input-Long" MaxLength="40"></asp:TextBox>
                                    &nbsp;<asp:LinkButton ID="lbtnAddMealOption" class="yt-MiniButton yt-AddButton" runat="server"
                                        OnClick="lbtnAddMealOption_Click" CausesValidation="false">Add</asp:LinkButton>
                                </div>
                                <div class="yt-Form-Field">
                                    <asp:ListBox ID="lbMealOptions" runat="server" CssClass="yt-Form-Field select" Rows="8"
                                        Width="300"></asp:ListBox>
                                    &nbsp;<asp:LinkButton ID="lbtnRemoveMealOption" runat="server" CssClass="yt-MiniButton yt-DeleteMealButton"
                                        OnClick="lbtnRemoveMealOption_Click" CausesValidation="false">Remove</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="yt-SectionWrapper yt-More">
                            <h3>
                                <label id="lblAdditionalOptions" runat="server">
                                    Additional Options</label></h3>
                            <asp:CheckBox ID="chkAllowAdditionalPeople" runat="server" Text="Allow guests to bring additional people"
                                Checked="true" /><br />
                            <asp:CheckBox ID="chkSendEmailReminder" runat="server" Text="Send an email reminder to guests 1 week before the event"
                                Checked="true" /><br />
                            <asp:CheckBox ID="chkShowRsvpStatistics" runat="server" Text="Show RSVP statistics to your guests (# of people attending and not attending)" /><br />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <!--End- Controls Added for Meal Selections and Additional option-->
                <div class="yt-SectionWrapper yt-More">
                    <h3>
                        <label id="lblPrivacy" runat="server">
                            <em class="required">* </em>Privacy:</label></h3>
                    <div class="yt-Form-Field yt-Form-Field-Radio">
                        <asp:RadioButton ID="rdoPublic" Text="Public" GroupName="rdoPrivacy" runat="server" />
                        <label id="lblPublic" runat="server">
                            Public Event (Anyone can see the event information and the guest list)</label>
                    </div>
                    <div class="yt-Form-Field yt-Form-Field-Radio">
                        <asp:RadioButton ID="rdoPrivate" Text="Private" GroupName="rdoPrivacy" runat="server" />
                        <label id="lblPriavte" runat="server">
                            Private Event (Only invited guests can see the event information and the guest list)</label>
                    </div>
                    <asp:CustomValidator ID="valPrivacy" runat="server" Font-Bold="True" Font-Size="Medium"
                        ForeColor="#FF8000" ClientValidationFunction="ValidatePrivacy" ErrorMessage=" Please select if this is a public or private event.">!</asp:CustomValidator>
                    <div class="yt-Form-Buttons">
                        <div id="facebook_share_container" style="margin-top: 5px; margin-right: 5px; float: left;
                            display: none">
                            <asp:CheckBox ID="facebook_share" runat="server" Checked="false" Text="Share on Facebook" />
                        </div>
                        <div class="yt-Form-Cancel">
                            <asp:LinkButton ID="lbtnCancelEvent" runat="server" CausesValidation="False" OnClick="lbtnCancelEvent_Click">Cancel</asp:LinkButton>
                        </div>
                        <!-- for Edit : -->
                        <div class="yt-Form-Submit">
                            <asp:LinkButton ID="lbtnEditEvent" runat="server" CssClass="yt-Button" OnClick="lbtnEditEvent_Click">Save</asp:LinkButton>
                        </div>
                        <!-- for Add: -->
                        <div class="yt-Form-Submit">
                            <asp:LinkButton ID="lbtnSaveEvent" runat="server" CssClass="yt-Button" OnClick="lbtnSaveEvent_Click">Save and Invite Guests</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </fieldset>
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
<asp:Content ID="content3" ContentPlaceHolderID="ImageListContentPlaceHolder" runat="Server">
    <div id="yt-ThumbContainer">
        <div id="yt-ThumbSelection" class="yt-Panel-Primary">
            <h2>
                Choose an image</h2>
            <div class="yt-ThumbList">
                <p class="yt-Bullet">
                    Select an image for your event:</p>
                <ul class="yt-ListBody">
                    <asp:Repeater ID="repImage" runat="server" OnItemDataBound="repImage_ItemDataBound">
                        <ItemTemplate>
                            <li class="yt-Form-Field">
                                <label class="yt-Thumb">
                                    <asp:Image ID="imgImageList" runat="server" ImageUrl='<%#Eval("ImageUrl")%>' />
                                </label>
                                <asp:RadioButton ID="rdoImage" runat="server" />
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
    </div>
</asp:Content>
