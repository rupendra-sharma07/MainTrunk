<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TributeCreation.aspx.cs"
    Inherits="Tribute_TributeCreation" Title="TributeCreation" ValidateRequest="false"
    EnableEventValidation="false" MasterPageFile="~/Shared/TributeCreation.master" %>

<%@ MasterType VirtualPath="~/Shared/TributeCreation.master" %>
<%@ Register Assembly="CuteEditor" Namespace="CuteEditor" TagPrefix="CE" %>
<asp:Content ID="content" ContentPlaceHolderID="TributePlaceHolder" runat="Server">

    <script type="text/javascript" src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>Common/JavaScript/CreditCardValidation.js"></script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="hfSeledctedTheme" runat="server" />
    <asp:HiddenField ID="hfAdminemail" runat="server" />
    <asp:HiddenField ID="hfPaymentMethod" runat="server" />
    <asp:HiddenField ID="hfTributeValue" runat="server" />
    <asp:HiddenField ID="hfSelectedTribute" runat="server" />
    <asp:HiddenField ID="hdnStoryImageURL" runat="server" />
    <div id="divShowModalPopup">
    </div>
    <div align="left">
        <div id="lblErrMsg" align="left" runat="server" class="yt-Error" visible="false">
        </div>
        
        <asp:ValidationSummary CssClass="yt-Error" ID="PortalValidationSummary" runat="server"
            Width="647px" ForeColor="Black" />
    </div>
    <div>
        <asp:MultiView ID="CreateTributeView" runat="server">
            <asp:View ID="Step1" runat="server">
                <div class="yt-Step2">
                    <asp:Panel ID="PanelStep1" runat="server">
                        <div class="yt-TributeProcess">
                            <ul class="yt-ProcessSteps">
                                <li class="yt-Selected"><a tabindex="301"><strong>1:</strong> Enter
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                    Details</a></li>
                                <li><a tabindex="302"><strong>2:</strong> More
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                    Details</a></li>
                                <li><a tabindex="303"><strong>3:</strong>
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                    Management</a></li>
                                <li><a tabindex="304"><strong>4:</strong> Review</a></li>
                                <li><a tabindex="305"><strong>5:</strong> Choose Account Type</a></li>
                                <li><a tabindex="306"><strong>6:</strong> Done!</a></li>
                            </ul>
                            <div class="yt-ProcessStepDisplay">
                                <fieldset class="yt-Form">
                                    <p class="yt-requiredFields">
                                        <strong>Required fields are indicated with <em class="required">* </em></strong>
                                    </p>
                                    <div class="yt-Form-Field ">
                                        <label>
                                            <em class="required">* </em>Who are you creating this
                                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                            for?    
                                        </label>
                                        <div id="MemorialDetails" runat="server">
                                            <label style="margin-top:5px;">
                                                <em class="required">* </em>First Name:</label>
                                            <asp:TextBox ID="txtTributeFirstName" CssClass="yt-Form-Input-XLong" MaxLength="40"
                                                runat="server" CausesValidation="True" TabIndex="1" Style="width: 265px;" 
                                                OnKeyUp="ValidateInputString()"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Text="!" runat="server"
                                                ControlToValidate="txtTributeFirstName" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"
                                                ValidationGroup="vgNextStep"></asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="ValidateTributeFirstName"
                                                ControlToValidate="txtTributeFirstName" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"
                                                ValidationGroup="vgNextStep">!</asp:CustomValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Please provide a valid Tribute Name."
                                                ControlToValidate="txtTributeFirstName" ValidationGroup="vgNextStep" Font-Bold="True"
                                                Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9_,\-\'\&quot;)(.\s,]*$"></asp:RegularExpressionValidator>
                                            <label style="margin-top:10px;">
                                                <em class="required">* </em>Last Name:</label>
                                            <asp:TextBox ID="txtTributeLastName" CssClass="yt-Form-Input-XLong" MaxLength="40"
                                                runat="server" CausesValidation="True" TabIndex="2" Style="width: 265px;" 
                                                OnKeyUp="ValidateInputString()"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Text="!" runat="server"
                                                ControlToValidate="txtTributeLastName" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"
                                                ValidationGroup="vgNextStep"></asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustomValidator5" runat="server" ClientValidationFunction="ValidateTributeLastName"
                                                ControlToValidate="txtTributeLastName" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"
                                                ValidationGroup="vgNextStep">!</asp:CustomValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Please provide a valid Tribute Name."
                                                ControlToValidate="txtTributeLastName" ValidationGroup="vgNextStep" Font-Bold="True"
                                                Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9_,\-\'\&quot;)(.\s,]*$"></asp:RegularExpressionValidator>                                          
                                        </div>
                                        </div>
                                        <div class="yt-Form-Field yt-Hint-Offset">
                                        <label>
                                            <em class="required">* </em>How would you like the person&#39;s name to appear 
                                        on the
                                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>?:
                                        </label>
                                        <asp:TextBox ID="txtTributeName" CssClass="yt-Form-Input-XLong" MaxLength="40" runat="server"
                                            CausesValidation="True" TabIndex="3" Style="width: 480px;"></asp:TextBox>
                                        <%--Added to remove warning on hitting enter in safari browser.--%>
                                        <asp:Button ID="hdnBtn" runat="server" Style="visibility: hidden; height: 0px; width: 0px;"
                                            OnClick="lbtnNextstep_Click" TabIndex="2"></asp:Button>
                                        <asp:RequiredFieldValidator ID="rfvTirbuteName" Text="!" runat="server" ControlToValidate="txtTributeName"
                                            Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ValidationGroup="vgNextStep"></asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="cvTributeName" runat="server" ClientValidationFunction="ValidateTributeName"
                                            ControlToValidate="txtTributeName" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"
                                            ValidationGroup="vgNextStep">!</asp:CustomValidator>
                                        <asp:RegularExpressionValidator ID="revTributeName" runat="server" ErrorMessage="Please provide a valid Tribute Name."
                                            ControlToValidate="txtTributeName" ValidationGroup="vgNextStep" Font-Bold="True"
                                            Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9_,\-\'\&quot;)(.\s,]*$"></asp:RegularExpressionValidator>
                                        <div class="hint">
                                            Enter the name of the person or people who you are creating this
                                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                                            for. For example: &quot;Aunt Jill&quot; or &quot;Jane and Michael Smith&quot;.You are limited to 40 
                                            characters.<span class="hintPointer"></span></div>
                                    </div>
                                    <fieldset id="TributeTypeChkBox" class="yt-TributeChannelSelection yt-CompactRadioList"
                                        runat="server">
                                        <legend><em class="required">* </em>Select the type of
                                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                                            you would like to create:</legend>
                                        <div class="yt-Form-Field yt-Form-Field-Radio">
                                            <asp:RadioButton ID="NewBaby" GroupName="rdoTributeType" Text="New Baby" runat="server"
                                                AutoPostBack="True" OnCheckedChanged="NewBaby_CheckedChanged" />
                                        </div>
                                        <div class="yt-Form-Field yt-Form-Field-Radio">
                                            <asp:RadioButton ID="Birthday" GroupName="rdoTributeType" Text="Birthday" runat="server"
                                                AutoPostBack="True" OnCheckedChanged="Birthday_CheckedChanged" />
                                        </div>
                                        <div class="yt-Form-Field yt-Form-Field-Radio">
                                            <asp:RadioButton ID="Graduation" GroupName="rdoTributeType" Text="Graduation" runat="server"
                                                AutoPostBack="True" OnCheckedChanged="Graduation_CheckedChanged" />
                                        </div>
                                        <div class="yt-Form-Field yt-Form-Field-Radio">
                                            <asp:RadioButton ID="Wedding" GroupName="rdoTributeType" Text="Wedding" runat="server"
                                                AutoPostBack="True" OnCheckedChanged="Wedding_CheckedChanged" />
                                        </div>
                                        <div class="yt-Form-Field yt-Form-Field-Radio">
                                            <asp:RadioButton ID="Anniversary" GroupName="rdoTributeType" Text="Anniversary" runat="server"
                                                AutoPostBack="True" OnCheckedChanged="Anniversary_CheckedChanged" />
                                        </div>
                                        <div class="yt-Form-Field yt-Form-Field-Radio">
                                            <asp:RadioButton ID="Memorial" GroupName="rdoTributeType" Text="Memorial" runat="server"
                                            AutoPostBack="True" OnCheckedChanged="Memorial_CheckedChanged" />
                                        </div>
                                        <asp:CustomValidator ID="CustomValidator2" runat="server" ClientValidationFunction="ValidateTribute"
                                            ErrorMessage="Select the type of tribute you would like to create." Font-Bold="True"
                                            Font-Size="Medium" ForeColor="#FF8000" Visible="true" ValidationGroup="vgNextStep">!</asp:CustomValidator>
                                        <%--<asp:Button id="TempButton" runat="server" onclick="TempButton_Click" 
                                            Text="TempButton" />--%>
                                    </fieldset>
                                    <div>
                                        
                                    </div>
                                    <fieldset class="yt-TributeChannelSelection" id="pnlSubCategory" runat="server">
                                        <legend><em class="required">* </em>Select the theme you would like to use for this
                                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>:</legend>
                                        <div class="yt-Form-Field">
                                            <asp:DropDownList ID="ddlSubCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </fieldset>
                                    <div class="yt-ThemeDiv" id="dvThemes" runat="server">
                                        <asp:Literal ID="ltrltheme" runat="server" ></asp:Literal>
                                    </div>
                                    <br style="clear: both;" />
                                    <div id="DefaultThemeCheckBox" runat="server" style="margin-top: 10px;" visible="false">
                                        <asp:CheckBox ID="chbxDefaultTheme" runat="server" />
                                        <label style="margin-left: 3px">
                                            Check here to set this as your default theme (Uncheck to remove a default theme)</label>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel1"  runat="server">
                                        <ContentTemplate>
                                            <div id="PanelTributeAddress" runat="server" visible="false" class="yt-Form-Field yt-Hint-Offset yt-ThemeAddress"
                                                style="width: 622px">
                                                <label>
                                                    <em class="required">* </em>Tell us what you would like the web address for this
                                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                                                    to be:</label>
                                                http://<%=_tributeName%>.<%=TributesPortal.Utilities.WebConfig.TopLevelDomain%>/
                                                <asp:TextBox ID="txtTributeAddress" CssClass="yt-Form-Input" runat="server" TabIndex="5"
                                                    MaxLength="100"></asp:TextBox>
                                                <div class="hint">
                                                    Choose a web address that you like and is based on who you are creating the
                                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                                                    for. For example: &quot;janemichaelsmith&quot;. Please use only letters, numbers
                                                    and underscores (URLs with other characters will not be accepted). Note, you will
                                                    not be able to change your web address once you have created it.<span class="hintPointer"></span></div>
                                                <span style="color: #FF8000; font-size: Medium; font-weight: bold;" id="errorAddress"
                                                    runat="server" visible="false">!</span>
                                                <asp:RegularExpressionValidator ID="revTributeAddress" runat="server" ControlToValidate="txtTributeAddress"
                                                    Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9_]*$"
                                                    ValidationGroup="vgCheckAvailability"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfvTributeAddress" runat="server" ControlToValidate="txtTributeAddress"
                                                    ErrorMessage="Tribute web address is a required field." Font-Bold="True" Font-Size="Medium"
                                                    ForeColor="#FF8000" ValidationGroup="vgCheckAvailability">!</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revTributeAddressNext" runat="server" ControlToValidate="txtTributeAddress"
                                                    Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9_]*$"
                                                    ValidationGroup="vgNextStep"></asp:RegularExpressionValidator>
                                                <asp:RequiredFieldValidator ID="rfvTributeAddressNext" runat="server" ControlToValidate="txtTributeAddress"
                                                    Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ValidationGroup="vgNextStep">!</asp:RequiredFieldValidator>
                                                <asp:LinkButton ID="lbtncheckAddress" runat="server" OnClick="lbtncheckAddress_Click"
                                                    Height="15px" TabIndex="6" ValidationGroup="vgCheckAvailability">Check 
                                                Availability</asp:LinkButton>
                                                <span class="availabilityNotice" id="SpanAvailable" runat="server"></span>
                                            </div>
                                            <fieldset class="yt-TributeAddressSelection" id="pnlTributeAddressAvailable" visible="false"
                                                runat="server" style="width: 622px">
                                                <asp:Label ID="lblOtherOptions" runat="server" Text="Other options that are available:"
                                                    Width="253px"></asp:Label>
                                                <br />
                                                <asp:RadioButton ID="rdbAvailableAddress1" runat="server" GroupName="AvailableAddress"
                                                    TabIndex="7" OnCheckedChanged="rdbAvailableAddress1_CheckedChanged" /><br />
                                                <asp:RadioButton ID="rdbAvailableAddress2" runat="server" GroupName="AvailableAddress"
                                                    TabIndex="8" OnCheckedChanged="rdbAvailableAddress2_CheckedChanged" /><br />
                                                <asp:RadioButton ID="rdbAvailableAddress3" runat="server" GroupName="AvailableAddress"
                                                    TabIndex="10" OnCheckedChanged="rdbAvailableAddress3_CheckedChanged" />
                                                <div class="yt-Form-Field yt-Hint-Offset">
                                                    <asp:Label ID="lblTributeAddressOther" runat="server" Text="Or, enter another option:"
                                                        Width="164px"></asp:Label>
                                                    <br />
                                                    http://<%=_tributeName%>.<%=TributesPortal.Utilities.WebConfig.TopLevelDomain%>/
                                                    <asp:TextBox ID="txtTributeAddressOther" CssClass="yt-Form-Input" runat="server"
                                                        TabIndex="11" MaxLength="100"></asp:TextBox>
                                                    <asp:RegularExpressionValidator ID="revTributeaddress2" runat="server" ControlToValidate="txtTributeAddressOther"
                                                        Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9_]*$"
                                                        ValidationGroup="vgCheckAvailability"></asp:RegularExpressionValidator>
                                                    <asp:CustomValidator ID="cvTributeAddressOther" ClientValidationFunction="ValidateAddress"
                                                        runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" Width="1px"
                                                        ValidationGroup="vgCheckAvailability">!</asp:CustomValidator>
                                                    <asp:RegularExpressionValidator ID="revTributeaddressOtherNext" runat="server" ControlToValidate="txtTributeAddressOther"
                                                        Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9_]*$"
                                                        ValidationGroup="vgNextStep"></asp:RegularExpressionValidator>
                                                    <asp:CustomValidator ID="cvTributeAddressOtherNext" ClientValidationFunction="ValidateAddress"
                                                        runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" Width="1px"
                                                        ValidationGroup="vgNextStep">!</asp:CustomValidator>
                                                    <asp:LinkButton ID="lbtncheckAvailability" CssClass="yt-checkAvailability" runat="server"
                                                        OnClick="lbtncheckAvailability_Click" Height="15px" TabIndex="12" ValidationGroup="vgCheckAvailability">Check 
                                                    Availability</asp:LinkButton>
                                                    <span class="availabilityNotice" id="imgMsgStatus2" runat="server"></span>
                                                </div>
                                            </fieldset>
                                            <div id="PanelAccountEmail" runat="server" visible="false" class="yt-Form-Field yt-Hint-Offset yt-EmailAddress"
                                                style="width: 622px">
                                                <label>
                                                    <em class="required">* </em>Tell us what email address we can use to contact you:</label>
                                                <asp:TextBox ID="txtAdminEmail" CssClass="yt-Form-Input-Long" runat="server" TabIndex="7"
                                                    MaxLength="100"></asp:TextBox>
                                                <span style="color: #FF8000; font-size: Medium; font-weight: bold;" id="Span1" runat="server"
                                                    visible="false">!</span>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAdminEmail"
                                                    ErrorMessage="Email is a required field." Font-Bold="True" Font-Size="Medium"
                                                    ForeColor="#FF8000" ValidationGroup="vgCheckAvailability">!</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtAdminEmail"
                                                    ErrorMessage="Please enter a valid email address." Font-Bold="True" Font-Size="Medium"
                                                    ForeColor="#FF8000" Text="!" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                    ValidationGroup="vgNextStep"></asp:RegularExpressionValidator>
                                                <asp:LinkButton ID="lbtncheckEmail" runat="server" CssClass="yt-checkAvailability"
                                                    OnClick="lbtncheckEmail_Click" Height="15px" TabIndex="8" ValidationGroup="vgCheckAvailability">Check 
                                                Availability</asp:LinkButton>
                                                <div class="hint">
                                                    Enter email address you would like YT to contact you and notify about your
                                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                                                    visitors contribution. Note, you will be able to change this email later in your
                                                    profile settings.<span class="hintPointer"></span></div>
                                                <span class="availabilityNotice" id="spanAvailableEmail" runat="server"></span>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="yt-Form-Buttons">
                                        <div class="yt-Form-Cancel">
                                            <% if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower().Equals("yourtribute"))
                                               { %>
                                            <a href="javascript: void(0);" onclick="cancelTributeCreation('tribute');">Cancel
                                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                                                Creation</a>
                                            <%} %>
                                            <%else
                                                { %>
                                            <a href="javascript: void(0);" onclick="cancelTributeCreation('website');">Cancel
                                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                                                Creation</a>
                                            <%} %>
                                        </div>
                                        <div class="yt-Form-Submit">
                                            <asp:LinkButton ID="lbtnNextstep" CausesValidation="true" CssClass="yt-Button yt-ArrowButton"
                                                OnClick="lbtnNextstep_Click" runat="server" TabIndex="13" ValidationGroup="vgNextStep">Next 
                                            step</asp:LinkButton>
                                        </div>
                                    </div>
                                    <!-- end yt-Form-Buttons-->
                                </fieldset>
                            </div>
                            <!--yt-ProcessStepDisplay-->
                        </div>
                    </asp:Panel>
                </div>
            </asp:View>
            <asp:View ID="Step2" runat="server">
                <div class="yt-Step2">
                    <asp:Panel ID="PanelStep2" runat="server" Width="654px">
                        <div class="yt-TributeProcess">
                            <ul class="yt-ProcessSteps">
                                <li class="yt-Visited">
                                    <asp:LinkButton ID="lbtn21EnterTributeDetails" OnClick="lbtn21EnterTributeDetails_Click"
                                        runat="server" CausesValidation="False"><strong>1:</strong>Enter <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> 
                                    Details</asp:LinkButton>
                                </li>
                                <li class="yt-Selected"><a><strong>2:</strong> More
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                    Details</a></li><li><a><strong>3:</strong>
                                        <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                                        Management</a></li><li><a><strong>4:</strong> Review</a></li><li><a><strong>5:</strong>
                                            Choose Account Type</a></li><li><a><strong>6:</strong> Done!</a></li></ul>
                            <div class="yt-ProcessStepDisplay">
                                <p class="yt-requiredFields" id="ptagtribute" runat="server">
                                </p>
                                <p class="yt-requiredFields">
                                    <strong>Required fields are indicated with <em class="required">* </em></strong>
                                </p>
                                <fieldset class="yt-Form">
                                    <fieldset id="PanelDate1" runat="server" class="yt-Date-Fields">
                                        <legend id="lblDate1" runat="server"></legend>
                                        <div class="yt-Form-Field">
                                            <asp:DropDownList ID="ddlMonth" runat="server" TabIndex="14" Width="146px">
                                                <asp:ListItem Value=" "></asp:ListItem>
                                                <asp:ListItem Value="1">January</asp:ListItem>
                                                <asp:ListItem Value="2">February</asp:ListItem>
                                                <asp:ListItem Value="3">March</asp:ListItem>
                                                <asp:ListItem Value="4">April</asp:ListItem>
                                                <asp:ListItem Value="5">May</asp:ListItem>
                                                <asp:ListItem Value="6">June</asp:ListItem>
                                                <asp:ListItem Value="7">July</asp:ListItem>
                                                <asp:ListItem Value="8">August</asp:ListItem>
                                                <asp:ListItem Value="9">September</asp:ListItem>
                                                <asp:ListItem Value="10">October</asp:ListItem>
                                                <asp:ListItem Value="11">November</asp:ListItem>
                                                <asp:ListItem Value="12">December</asp:ListItem>
                                            </asp:DropDownList>
                                            <label>
                                                Month</label>
                                        </div>
                                        <div class="yt-Form-Field">
                                            <asp:DropDownList ID="ddlDay" TabIndex="15" runat="server" Width="50px">
                                            </asp:DropDownList>
                                            <label>
                                                Day</label>
                                        </div>
                                        <div class="yt-Form-Field">
                                            <asp:TextBox ID="txtYear" MaxLength="4" TabIndex="16" CssClass="yt-Form-Input-Short"
                                                Width="50px" runat="server"></asp:TextBox>
                                            <label>
                                                Year</label>
                                        </div>
                                        <span id="Indecator1" runat="server" style="color: #FF8000; font-size: Medium; font-weight: bold;"
                                            visible="false">!</span>
                                        <asp:CompareValidator ID="cvYear" runat="server" Text="!" ErrorMessage="Date of birth should be less or equal to current date."
                                            ControlToValidate="txtYear" Type="Integer" Operator="LessThanEqual" Font-Bold="True"
                                            Font-Size="Medium" ForeColor="White" Width="1px" Visible="False"></asp:CompareValidator>
                                        <asp:CustomValidator ID="cvDate1" runat="server" ClientValidationFunction="ValidateDate"
                                            Text="!" ErrorMessage="Please enter valid date." Font-Bold="True" Font-Size="Medium"
                                            ForeColor="#FF8000" Width="1px"></asp:CustomValidator>
                                        <asp:CustomValidator ID="cvDate12" runat="server" Visible="false" ClientValidationFunction="ValidateDates"
                                            Text="!" ErrorMessage="Please enter valid date in one of them." Font-Bold="True"
                                            Font-Size="Medium" ForeColor="#FF8000"></asp:CustomValidator>
                                        <a href="javascript:void(0);" id="reset1" runat="server" visible="false" onclick="Reset1();">
                                            reset</a>
                                    </fieldset>
                                    <fieldset id="PanelDate2" runat="server" class="yt-Date-Fields">
                                        <legend id="lblDate2" runat="server"></legend>
                                        <div class="yt-Form-Field">
                                            <asp:DropDownList ID="ddlMonth2" Width="146px" TabIndex="17" runat="server">
                                                <asp:ListItem Value=" "></asp:ListItem>
                                                <asp:ListItem Value="1">January</asp:ListItem>
                                                <asp:ListItem Value="2">February</asp:ListItem>
                                                <asp:ListItem Value="3">March</asp:ListItem>
                                                <asp:ListItem Value="4">April</asp:ListItem>
                                                <asp:ListItem Value="5">May</asp:ListItem>
                                                <asp:ListItem Value="6">June</asp:ListItem>
                                                <asp:ListItem Value="7">July</asp:ListItem>
                                                <asp:ListItem Value="8">August</asp:ListItem>
                                                <asp:ListItem Value="9">September</asp:ListItem>
                                                <asp:ListItem Value="10">October</asp:ListItem>
                                                <asp:ListItem Value="11">November</asp:ListItem>
                                                <asp:ListItem Value="12">December</asp:ListItem>
                                            </asp:DropDownList>
                                            <label>
                                                Month</label>
                                        </div>
                                        <div class="yt-Form-Field">
                                            <asp:DropDownList ID="ddlDay2" Width="50px" TabIndex="18" runat="server">
                                            </asp:DropDownList>
                                            <label>
                                                Day</label>
                                        </div>
                                        <div class="yt-Form-Field">
                                            <asp:TextBox ID="txtYear2" MaxLength="4" TabIndex="19" CssClass="yt-Form-Input-Short"
                                                Width="50px" runat="server"></asp:TextBox>
                                            <label>
                                                Year</label>
                                        </div>
                                        <span id="Indecator2" runat="server" style="color: #FF8000; font-size: Medium; font-weight: bold;"
                                            visible="false">!</span>
                                        <asp:CustomValidator ID="cvNewbaby" runat="server" ClientValidationFunction="ValidateNewBaby"
                                            Visible="false" ErrorMessage="Please enter valid date." Font-Bold="True" Font-Size="Medium"
                                            ForeColor="#FF8000" Width="1px">!</asp:CustomValidator>
                                        <asp:CustomValidator ID="cvDate2" runat="server" ClientValidationFunction="ValidateDate2"
                                            Text="!" ErrorMessage="Please  enter  valid date." Font-Bold="True" Font-Size="Medium"
                                            ForeColor="#FF8000" Width="1px"></asp:CustomValidator>
                                        <asp:CompareValidator ID="cpYear" runat="server" ErrorMessage="Due date should be greater or equal to current date."
                                            ControlToValidate="txtYear2" Type="Integer" Operator="GreaterThanEqual" Font-Bold="True"
                                            Font-Size="Medium" ForeColor="White" Visible="False" Width="1px">!</asp:CompareValidator>
                                        <asp:CompareValidator ID="cpYear2" runat="server" Text="!" ErrorMessage="Date of death should be less or equal to current date."
                                            ControlToValidate="txtYear2" Type="Integer" Operator="LessThanEqual" Font-Bold="True"
                                            Font-Size="Medium" ForeColor="White" ValueToCompare="2008" Width="1px" Visible="False"></asp:CompareValidator>
                                        <a href="javascript:void(0);" id="reset2" runat="server" visible="false" onclick="Reset2();">
                                            reset</a>
                                    </fieldset>
                                    <div>
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <div class="yt-Form-Field">
                                                    <label>
                                                        <em class="required">* </em>Country:</label>
                                                    <asp:DropDownList ID="ddlCountry" TabIndex="20" CssClass="yt-Form-DropDown-Long"
                                                        runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="yt-Form-Field">
                                                    <label>
                                                        State/Province:</label>
                                                    <asp:DropDownList ID="ddlStateProvince" TabIndex="21" runat="server" Width="294px"
                                                        CssClass="yt-Form-DropDown-Long">
                                                    </asp:DropDownList>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="yt-Form-Field">
                                        <label>
                                            City:</label>
                                        <asp:TextBox ID="txtCity" CssClass="yt-Form-Input-Long" TabIndex="22" runat="server"
                                            MaxLength="50">
                                        </asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revtxtCity" runat="server" ControlToValidate="txtCity"
                                            ErrorMessage="City only contain letters,numbers,'-' and '#'" Font-Bold="True"
                                            Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9,\#,\-,\s]*$"></asp:RegularExpressionValidator>
                                    </div>
                                    <fieldset>
                                        <legend><em class="required"></em>Photo:</legend>
                                        <div class="yt-Form-Field yt-PhotoField yt-TributePhoto">
                                            <div>
                                                <asp:Image ID="imgTributePhoto" Height="119px" Width="119px" ImageUrl="<%=tributeImgURL %>"
                                                    runat="server" />
                                            </div>
                                            <a href="javascript: void(0);" onclick="uploadTributePhoto();" class="yt-MiniButton">
                                                Upload photo</a>
                                            <div class="yt-shownHint">
                                                If you do not have a photo to upload right now, that is okay! A default photo will
                                                be used, and you can upload a personalized photo for this
                                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                                                after it has been created.</div>
                                        </div>
                                    </fieldset>
                                    <div class="yt-Form-Field yt-MessageField">
                                        <div>
                                            <em class="required">* </em>
                                            <asp:Label ID="lblarMessage" runat="server"></asp:Label>
                                            <asp:RequiredFieldValidator ID="rfvarMessage" ControlToValidate="txtarMessage" Text="!"
                                                runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"></asp:RequiredFieldValidator>
                                        </div>
                                        <asp:TextBox ID="txtarMessage" Rows="5" TabIndex="23" CssClass="yt-Form-Textarea-XLong"
                                            Columns="50" runat="server" TextMode="MultiLine" Width="277px" Height="95px"></asp:TextBox>
                                        <div class="yt-shownHint">
                                            This welcome message will appear on the
                                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                            homepage, and will help to introduce people to your site. We have provided some
                                            default text for you to use if you can’t think of anything else to write, but remember
                                            – a personally created message is always a nice touch!
                                        </div>
                                        <p class="yt-messageRemaining">
                                            <span id="numberRemaining" runat="server"></span>&nbsp;characters remaining.
                                            <%--<asp:TextBox ID="numberRemaining" CssClass="yt-messageRemaining" runat="server" BorderStyle="None"
                                                ReadOnly="True" Width="30px" Font-Bold="False" Font-Size="Small"></asp:TextBox>--%>
                                        </p>
                                    </div>
                                    <div id="ObituaryNote" runat="server" visible="false">
                                        <div style="color: #3F2921; font-size: 120%;" runat="server">
                                            Obituary:</div>
                                        <br />
                                        <div class="yt-shownHint">
                                            The obituary will appear on the Story Page and on the
                                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                            homepage. If you do not have the obituary, this is okay, it is not required. You
                                            can also add an obituary to the
                                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                                            at a later date.
                                        </div>
                                        <br />
                                    </div>
                                    <div class="yt-FreeTextBox" style="display: none;" id="CuteeditorNoteMessage" runat="server">
                                        <%--<FTB:FreeTextBox id="ftbNoteMessage" runat="server" ToolbarLayout="Save,Bold, Italic, Underline,JustifyLeft,JustifyRight, JustifyCenter" height="220" Visible="false"/>
                                            <span id="spnMessage" style="color: #FF8000; font-size: M   edium; font-weight: bold;" visible="false" runat="server">!</span>--%>
                                        <%-- Ashu(17 june,2011): Add Cute editor to creation process --%>
                                        <CE:Editor ID="ftbNoteMessage" runat="server" TemplateItemList="Save,Bold,Italic,Underline,JustifyLeft,JustifyRight,JustifyCenter,FontName,FontSize"
                                            EnableStripScriptTags="false" ThemeType="Office2007" EditCompleteDocument="true"
                                            AllowPasteHtml="true" Width="630px">
                                        </CE:Editor>
                                    </div>
                                    <div class="yt-Form-Buttons">
                                        <div class="yt-Form-Cancel">
                                            <% if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower().Equals("yourtribute"))
                                               { %>
                                            <a href="javascript: void(0);" onclick="cancelTributeCreation('tribute');">Cancel
                                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                                                Creation</a>
                                            <%} %>
                                            <%else
                                                { %>
                                            <a href="javascript: void(0);" onclick="cancelTributeCreation('website');">Cancel
                                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                                                Creation</a>
                                            <%} %>
                                        </div>
                                        <div class="yt-Form-Submit">
                                            <asp:LinkButton ID="lbtn2Nextstep" TabIndex="24" CssClass="yt-Button yt-ArrowButton"
                                                runat="server" OnClick="lbtn2Nextstep_Click">Next step</asp:LinkButton>
                                        </div>
                                    </div>
                                    <!-- end yt-Form-Buttons-->
                                </fieldset>
                            </div>
                            <!--yt-ProcessStepDisplay-->
                        </div>
                    </asp:Panel>
                </div>
            </asp:View>
            <asp:View ID="Step3" runat="server">
                <div class="yt-Step3">
                    <asp:Panel ID="PanelStep3" runat="server" Width="653px">
                        <div class="yt-TributeProcess">
                            <ul class="yt-ProcessSteps">
                                <li class="yt-Visited">
                                    <asp:LinkButton ID="lbtn31EnterTributeDetails" OnClick="lbtn31EnterTributeDetails_Click"
                                        runat="server" CausesValidation="False"><strong>1:</strong>Enter <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> 
                                    Details</asp:LinkButton>
                                </li>
                                <li class="yt-Visited">
                                    <asp:LinkButton ID="lbtn32MoreTributeDetails" OnClick="lbtn32MoreTributeDetails_Click"
                                        runat="server" CausesValidation="False"><strong>2:</strong>More <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> 
                                    Details</asp:LinkButton>
                                </li>
                                <li class="yt-Selected"><a><strong>3:</strong>
                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                    Management</a></li><li><a><strong>4:</strong> Review</a></li><li><a><strong>5:</strong>
                                        Choose Account Type</a></li><li><a><strong>6:</strong> Done!</a></li></ul>
                            <div class="yt-ProcessStepDisplay">
                                <%-- <p class="yt-requiredFields">
                                    Please enter some more information about the
                                    <!-- TRIBUTE TYPE HERE -->
                                    tribute you are creating for Tribute Name.
                                </p>--%>
                                <p class="yt-requiredFields">
                                    <strong>Required fields are indicated with <em class="required"><em class="required">
                                        * </em></em></strong>
                                   <%-- <asp:CustomValidator ID="cvPrivacy" runat="server" Font-Bold="True" Font-Size="Medium"
                                        ForeColor="#FF8000" ClientValidationFunction="ValidateConfirmation">!</asp:CustomValidator>--%>
                                </p>
                                <fieldset class="yt-Form">
                                    <fieldset class="yt-PrivacyFields">
                                        <legend><em class="required">*</em> Please select the level of privacy for this
                                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>:</legend>  <asp:CustomValidator ID="cvPrivacy" runat="server" Font-Bold="True" Font-Size="Medium"
                                        ForeColor="#FF8000" ClientValidationFunction="ValidateConfirmation" Display="Dynamic">!</asp:CustomValidator>
                                        <div class="yt-Form-Field yt-Form-Field-Radio">
                                            <asp:RadioButton ID="rdoPublic" Text="Public" GroupName="rdoPrivacy" runat="server"
                                                TabIndex="1" />
                                            <p>
                                                Public
                                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>s
                                                can be found by anyone who is searching for a
                                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>.
                                                This type of
                                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                                                may also appear on Your
                                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>
                                                in the list of &quot;Most Recently Created&quot; and &quot;Most Viewed&quot;
                                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>s.</p>
                                        </div>
                                        <div class="yt-Form-Field yt-Form-Field-Radio">
                                            <asp:RadioButton ID="rdoPrivate" Text="Private" GroupName="rdoPrivacy" runat="server"
                                                TabIndex="2" />
                                            <p>
                                                Private
                                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>s
                                                will not show up in search results, and will not be featured anywhere on Your
                                                <%=ConfigurationManager.AppSettings["ApplicationWord"].ToString()%>. These types
                                                of
                                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>s
                                                will still be accessible to all users (that is, they will not be password protected).
                                            </p>
                                        </div>
                                    </fieldset>
                                    <fieldset class="yt-AdminAddFields">
                                        <legend>
                                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                            Administrators:</legend>
                                        <p>
                                            Please enter the email address(es) of people you would like to include as administrators
                                            of this
                                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>.
                                            Administrators have access to add, delete, and edit all parts of a
                                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>.</p>
                                        <div class="yt-Form-Field yt-AdminAdd">
                                            <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                    <asp:Repeater ID="RepeaterEmailAddress" runat="server" OnItemDataBound="RepeaterEmailAddress_ItemDataBound">
                                                        <ItemTemplate>
                                                            <div class="yt-Form-Field yt-AdminAdd" id="yt-Admin1">
                                                                <asp:Label ID="lblUniqueID" runat="server" Text="<%# Container.DataItem %>" Visible="false"></asp:Label>
                                                                <asp:Label ID="lblAdminEmailAddress" AssociatedControlID="txtAdminEmailAddress" CssClass="yt-Form-Field"
                                                                    runat="server" Text="Email address:"></asp:Label>
                                                                <asp:TextBox ID="txtAdminEmailAddress" CssClass="yt-Form-Input-Long" MaxLength="250"
                                                                    runat="server"></asp:TextBox>
                                                                <asp:RegularExpressionValidator Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"
                                                                    ID="revEmailAdresses" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                    runat="server" Text="!" ControlToValidate="txtAdminEmailAddress" ErrorMessage="Please enter a valid email address."></asp:RegularExpressionValidator>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="lbtnTributeAdmin" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="yt-Form-Buttons">
                                            <asp:LinkButton ID="lbtnTributeAdmin" CssClass="yt-MiniButton yt-AdminButton" Text="Add Another Administrator"
                                                runat="server" OnClick="lbtnTributeAdmin_Click"></asp:LinkButton>
                                        </div>
                                    </fieldset>
                                    <%--  LHK:Commented on 13-jan-2011--%>
                                    <!--Donation Panel Starts-->
                                    <%--<fieldset class="yt-AdminAddFields">
                                        <legend>Donations:</legend>
                                        <asp:CheckBox runat="server" ID="chkDonationBox" AutoPostBack="true" Text=" Click here to add a Donation Box to your tribute homepage."
                                            OnCheckedChanged="chkDonationBox_CheckedChanged" />
                                        (<asp:Label runat="server" ID="lblDonationSample"></asp:Label>)<br />
                                        <br />
                                        <p>
                                            Adding a donation box to your tribute is a free and easy way for friends and 
                                            family to make donations to your preferred U.S. Charity. Donations are safely 
                                            and securely managed by <a href="<%=TributesPortal.Utilities.WebConfig.DonationURL %>" target="_blank">
                                                eParterns in Giving</a>. After signup you will receive an email with 
                                            instructions on how to manage your donation page.</p>
                                        <div class="yt-Form-Buttons">
                                            <p>
                                                <i>*Note that you can remove the donation box from your tribute and stop 
                                                receiving donations at any time.</i></p>
                                        </div>
                                    </fieldset>--%>
                                    <div runat="server" id="divDonation" visible="false">
                                        <fieldset class="yt-AdminAddFields">
                                            <legend><em class="required">*</em> Email Address:</legend>
                                            <p>
                                                Please enter the email address where you would like to receive notification when
                                                a donation is made.</p>
                                            <asp:TextBox runat="server" ID="txtDonationEmail" Width="300px" MaxLength="255"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvDonationEmail" Display="dynamic" Text="!" runat="server"
                                                ControlToValidate="txtDonationEmail" ErrorMessage="Donation email address is a required field."
                                                Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator Font-Bold="True" Display="Dynamic" Text="!" Font-Size="Medium"
                                                ForeColor="#FF8000" ID="revDonationEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                runat="server" ControlToValidate="txtDonationEmail" ErrorMessage=" Please enter a valid Donation email address."></asp:RegularExpressionValidator>
                                        </fieldset>
                                        <fieldset class="yt-AdminAddFields">
                                            <legend>Charity Name:</legend>
                                            <p>
                                                Please enter the name of charity where you would like donations to be sent. We are
                                                able to submit donations to any registered charity in United States. Leave blank
                                                if you have no preference.</p>
                                            <asp:TextBox runat="server" ID="txtDonationCharityName" Width="450px" MaxLength="1000"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="revCharityNameSplchr" runat="server" ErrorMessage="Please enter valid characters in Charity Name."
                                                ControlToValidate="txtDonationCharityName" Font-Bold="True" Font-Size="Medium"
                                                ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9,\#,\-.\s]*$"></asp:RegularExpressionValidator>
                                            <%--<asp:RegularExpressionValidator ID="revCharityNameSplchr" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ControlToValidate="txtDonationCharityName" ValidationExpression='^[a-zA-Z0-9\s\?\"\!\-\@\.\:\;\=\+\[\]_\,\%\(\)\/\&]+$' runat="server" ErrorMessage=" Please enter valid characters in Charity Name.">!</asp:RegularExpressionValidator>--%>
                                        </fieldset>
                                        <fieldset class="yt-AdminAddFields">
                                            <legend>Charity Address:</legend>
                                            <p>
                                                Please enter the address of the U.S. Charity if you know it. The more information
                                                you proivide, the easier it will be for us to locate your preferred charity. Leave
                                                blank if you do not know the address.</p>
                                        </fieldset>
                                        <fieldset class="yt-AdminAddFields">
                                            <legend><em class="required">*</em> Country:</legend>
                                            <asp:DropDownList runat="server" ID="ddlDonationCountry">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvDonationCountry" Display="dynamic" Text="!" runat="server"
                                                ControlToValidate="ddlDonationCountry" ErrorMessage="Country is a required field."
                                                Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"></asp:RequiredFieldValidator>
                                        </fieldset>
                                        <fieldset class="yt-AdminAddFields">
                                            <legend>State:</legend>
                                            <asp:DropDownList runat="server" ID="ddlDonationState">
                                            </asp:DropDownList>
                                        </fieldset>
                                        <fieldset class="yt-AdminAddFields">
                                            <legend>City:</legend>
                                            <asp:TextBox runat="server" ID="txtDonationCity" Width="300px" MaxLength="50"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="revDonationCitySplChr" runat="server" ControlToValidate="txtDonationCity"
                                                ErrorMessage="Please enter valid characters in City." Font-Bold="True" Font-Size="Medium"
                                                ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9,\#,\-,\s]*$"></asp:RegularExpressionValidator>
                                            <%--<asp:RegularExpressionValidator ID="revDonationCitySplChr" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ControlToValidate="txtDonationCity" ValidationExpression='^[a-zA-Z0-9\s\?\"\!\-\@\.\:\;\=\+\[\]_\,\%\(\)\/\&]+$' runat="server" ErrorMessage=" Please enter valid characters in City.">!</asp:RegularExpressionValidator>--%>
                                        </fieldset>
                                        <fieldset class="yt-AdminAddFields">
                                            <legend>Address:</legend>
                                            <asp:TextBox runat="server" ID="txtDonationAddress" Width="450px" MaxLength="1000"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="revDonationAddress" runat="server" ControlToValidate="txtDonationAddress"
                                                ErrorMessage="Please enter valid characters in Address." Font-Bold="True" Font-Size="Medium"
                                                ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9,_/\#,\-,\;,\s]*$"></asp:RegularExpressionValidator>
                                            <%--<asp:RegularExpressionValidator ID="revDonationAddress" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ControlToValidate="txtDonationAddress" ValidationExpression='^[a-zA-Z0-9\s\?\"\!\-\@\.\:\;\=\+\[\]_\,\%\(\)\/\&]+$' runat="server" ErrorMessage=" Please enter valid characters in Address.">!</asp:RegularExpressionValidator>--%>
                                        </fieldset>
                                        <fieldset class="yt-AdminAddFields">
                                            <div class="ePartnerLogo">
                                            </div>
                                            <div class="divePartnerLogo">
                                                <b>Terms and Conditions:</b><br />
                                                Donations can be made to registered U.S. Charities only. Donations are processed
                                                through e-Partners in Giving, LLC and users of this feature are subject to their
                                                &quot;<a href="<%=TributesPortal.Utilities.WebConfig.DonationTermsURL %>" target="_blank">Terms
                                                    &amp; Conditions</a>&quot; and &quot;<a href="<%=TributesPortal.Utilities.WebConfig.DonationPrivacyURL %>"
                                                        target="_blank">Private Policy</a>&quot;. By clicking &quot;Next step&quot;
                                                and continuing the
                                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                                                creation process, you are indicating that you have agreed to these terms and conditions.
                                            </div>
                                        </fieldset>
                                    </div>
                                    <!--Donation Panel Ends-->
                                    <div class="yt-Form-Buttons">
                                        <div class="yt-Form-Cancel">
                                            <% if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower().Equals("yourtribute"))
                                               { %>
                                            <a href="javascript: void(0);" onclick="cancelTributeCreation('tribute');">Cancel
                                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                                                Creation</a>
                                            <%} %>
                                            <%else
                                                { %>
                                            <a href="javascript: void(0);" onclick="cancelTributeCreation('website');">Cancel
                                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                                                Creation</a>
                                            <%} %>
                                        </div>
                                        <div class="yt-Form-Submit">
                                            <asp:LinkButton ID="lbtn3Nextstep" CssClass="yt-Button yt-ArrowButton" runat="server"
                                                OnClick="lbtn3Nextstep_Click">Next step</asp:LinkButton>
                                        </div>
                                    </div>
                                    <!-- end yt-Form-Buttons-->
                                </fieldset>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </asp:View>
            <asp:View ID="Step4" runat="server">
                <asp:Panel ID="Pane" runat="server" Width="653px">
                </asp:Panel>
                <asp:Panel ID="PanelStep4" runat="server" Width="653px">
                    <div class="yt-Step4">
                        <div class="yt-ContentPrimary">
                            <div class="yt-TributeProcess">
                                <ul class="yt-ProcessSteps">
                                    <li class="yt-Visited">
                                        <asp:LinkButton ID="lbtn41EnterTributeDetails" OnClick="lbtn41EnterTributeDetails_Click"
                                            runat="server" CausesValidation="False"><strong>1:</strong>Enter 
<%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> 
                                        Details</asp:LinkButton>
                                    </li>
                                    <li class="yt-Visited">
                                        <asp:LinkButton ID="lbtn42MoreTributeDetails" OnClick="lbtn42MoreTributeDetails_Click"
                                            runat="server" CausesValidation="False"><strong>2:</strong>More 
<%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> 
                                        Details</asp:LinkButton>
                                    </li>
                                    <li class="yt-Visited">
                                        <asp:LinkButton ID="lbtn43TributeManagement" OnClick="lbtn43TributeManagement_Click"
                                            runat="server" CausesValidation="False"><strong>3:</strong>
<%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> 
                                        Management</asp:LinkButton>
                                    </li>
                                    <li class="yt-Selected"><a><strong>4:</strong> Review</a></li>
                                    <li><a><strong>5:</strong> Choose Account Type</a></li>
                                    <li><a><strong>6:</strong> Done!</a></li>
                                </ul>
                                <div class="yt-ProcessStepDisplay">
                                    <p>
                                        Please confirm that the information you have entered is correct:
                                    </p>
                                    <br />
                                    <%--  <p>
                                        &nbsp;</p>--%>
                                    <br />
                                    <div class="yt-Panel-Primary" id="yt-TributeConfirmDetails">
                                        <asp:LinkButton ID="lbtnEdit1" CssClass="yt-MiniButton yt-EditButton" runat="server"
                                            OnClick="lbtnEdit1_Click">Edit</asp:LinkButton>
                                        <h2>
                                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                            Details
                                        </h2>
                                        <h4>
                                            You are creating this
                                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                                            for:</h4>
                                        <p>
                                            <asp:Label ID="txttributefor" runat="server" Text=""></asp:Label>
                                        </p>
                                        <div>
                                            <asp:Panel ID="PanelSavedTheme" runat="server" Visible="True" Width="598px">
                                                <h4>
                                                    The type of
                                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                                                    that you have chosen to create:</h4>
                                                <p>
                                                    <asp:Label ID="lblSelectedTributeType" runat="server" Text="New Baby"></asp:Label>
                                                </p>
                                                <h4>
                                                    The theme you have chosen for this
                                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>:</h4>
                                                <asp:Literal ID="step4Litrel" runat="server"></asp:Literal>
                                            </asp:Panel>
                                        </div>
                                        <div id="divTributrUrl" runat="server" visible="false">
                                            <h4>
                                                The web address for this
                                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                                                will be:</h4>
                                            <p>
                                                <asp:Label ID="txtDomainName" runat="server"></asp:Label>
                                            </p>
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <div class="yt-Panel-Primary" id="yt-TributeConfirmMoreDetails">
                                        <h2>
                                            More
                                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                            Details
                                        </h2>
                                        <asp:LinkButton ID="lbtnEdit2" CssClass="yt-MiniButton yt-EditButton" runat="server"
                                            OnClick="lbtnEdit2_Click">Edit</asp:LinkButton>
                                        <asp:Panel ID="PaneleditDate1" runat="server" Visible="false" Width="593px">
                                            <h4>
                                                <asp:Label ID="lblEditBornOn" runat="server" Text="Firstname Lastname was born on:"></asp:Label>
                                            </h4>
                                            <p>
                                                <asp:Label ID="txtEditBornOn" runat="server" Text=" January 1, 2007"></asp:Label>
                                            </p>
                                        </asp:Panel>
                                        <asp:Panel ID="PaneleditDate2" runat="server" Visible="false" Width="593px">
                                            <h4>
                                                <asp:Label ID="lblEditDeathOn" runat="server" Text="Firstname Lastname was dead on:"></asp:Label>
                                            </h4>
                                            <p>
                                                <asp:Label ID="txtEditDeathOn" runat="server" Text=" January 1, 2007"></asp:Label>
                                            </p>
                                        </asp:Panel>
                                        <asp:Panel ID="PaneleditLocation" runat="server" Visible="false" Width="593px">
                                            <h4>
                                                <asp:Label ID="lblEditBornin" runat="server" Text="Firstname Lastname was born in:"></asp:Label>
                                            </h4>
                                            <p>
                                                <asp:Label ID="txtEditBornin" runat="server" Text="Vancouver, BC Canada"></asp:Label>
                                            </p>
                                        </asp:Panel>
                                        <h4>
                                            You have chosen to use this as the profile photograph for the
                                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>:</h4>
                                        <div class="yt-TributePhoto">
                                            <img id="tributephoto_" runat="server" src="" width="119" height="119" alt="" /></div>
                                        <h4>
                                            The welcome message, which will appear on the homepage of this
                                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>,
                                            will read:</h4>
                                        <p id="lblEditMeaasge" runat="server">
                                        </p>
                                    </div>
                                    <br />
                                    <br />
                                    <div class="yt-Panel-Primary" id="yt-TributeConfirmManagement">
                                        <h2>
                                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                            management
                                        </h2>
                                        <asp:LinkButton ID="lbtneditstep3" CssClass="yt-MiniButton yt-EditButton" runat="server"
                                            OnClick="lbtneditstep3_Click">Edit</asp:LinkButton>
                                        <asp:Panel ID="PanelSaveTributeManagement" Visible="true" runat="server" Width="596px"
                                            Style="overflow: hidden;">
                                            <h4>
                                                The level of privacy you have chosen for this
                                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                                                is:</h4>
                                            <p>
                                                <asp:Label ID="lbleditPrivacy" runat="server" Font-Bold="True"></asp:Label>
                                            </p>
                                            <p>
                                                <asp:Label ID="lbleditPrivacyDetail" runat="server"></asp:Label>
                                            </p>
                                            <h4 id="Otheradministrators" visible="false" runat="server">
                                                Other administrators you have designated are:
                                            </h4>
                                            <div>
                                                <asp:Repeater ID="RepeaterEditAdminEmail" runat="server">
                                                    <ItemTemplate>
                                                        <p>
                                                            <asp:Label ID="lblEditAdminEmail" runat="server" Text="<%# Container.DataItem %>"></asp:Label>
                                                        </p>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                            <!--Donation Review Panel Starts-->
                                            <div runat="server" id="divDonationReview" visible="false">
                                                <fieldset class="yt-AdminAddFields">
                                                    <legend>Donations:</legend>
                                                    <p>
                                                        <b>You have chosen to add a Donation Box to your
                                                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                                                            homepage.</b></p>
                                                    <div class="yt-Form-Buttons">
                                                        Email Address:
                                                        <p>
                                                            <asp:Label runat="server" ID="lblEditDonationEmail"></asp:Label></p>
                                                    </div>
                                                    <div class="yt-Form-Buttons">
                                                        Charity Name:
                                                        <p>
                                                            <asp:Label runat="server" ID="lblEditCharityName"></asp:Label></p>
                                                    </div>
                                                    <p>
                                                        Charity Address:</p>
                                                    <p>
                                                        Country:
                                                        <asp:Label runat="server" ID="lblDonationCountry"></asp:Label></p>
                                                    <p>
                                                        State:
                                                        <asp:Label runat="server" ID="lblDonationState"></asp:Label></p>
                                                    <p>
                                                        City:
                                                        <asp:Label runat="server" ID="lblDonationCity"></asp:Label></p>
                                                    <p>
                                                        Address:
                                                        <asp:Label runat="server" ID="lblDonationAddress"></asp:Label></p>
                                                </fieldset>
                                            </div>
                                            <!--Donation Review Panel Ends-->
                                        </asp:Panel>
                                    </div>
                                    <fieldset class="yt-Form">
                                        <div class="yt-Form-Buttons">
                                            <div class="yt-Form-Cancel">
                                                <% if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower().Equals("yourtribute"))
                                                   { %>
                                                <a href="javascript: void(0);" onclick="cancelTributeCreation(tribute');">Cancel
                                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                                                    Creation</a>
                                                <%} %>
                                                <%else
                                                    { %>
                                                <a href="javascript: void(0);" onclick="cancelTributeCreation('website');">Cancel
                                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                                                    Creation</a>
                                                <%} %>
                                            </div>
                                            <div class="yt-Form-Submit">
                                                <asp:LinkButton ID="ltbn4Nextstep" CssClass="yt-Button yt-ArrowButton" runat="server"
                                                    OnClick="ltbn4Nextstep_Click">Next step</asp:LinkButton>
                                            </div>
                                        </div>
                                        <!-- end yt-Form-Buttons-->
                                    </fieldset>
                                </div>
                                <!--yt-ProcessStepDisplay-->
                            </div>
                            <!--yt-TributeProcess-->
                        </div>
                    </div>
                </asp:Panel>
            </asp:View>
            <asp:View ID="Step5" runat="server">
                <div class="yt-Step5">
                    <asp:Panel ID="PanelStep5" runat="server" Width="653px">
                        <div class="yt-ContentPrimary">
                            <div class="yt-TributeProcess">
                                <ul class="yt-ProcessSteps">
                                    <li class="yt-Visited">
                                        <asp:LinkButton ID="lbtn51EnterTributeDetails" runat="server" OnClick="lbtn51EnterTributeDetails_Click"
                                            CausesValidation="False"><strong>1:</strong>Enter 
<%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> Details</asp:LinkButton>
                                    </li>
                                    <li class="yt-Visited">
                                        <asp:LinkButton ID="lbtn52MoreTributeDetails" runat="server" OnClick="lbtn52MoreTributeDetails_Click"
                                            CausesValidation="False"><strong>2:</strong>More 
<%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> Details</asp:LinkButton>
                                    </li>
                                    <li class="yt-Visited">
                                        <asp:LinkButton ID="lbtn53TributeManagement" runat="server" OnClick="lbtn53TributeManagement_Click"
                                            CausesValidation="False"><strong>3:</strong>
<%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> Management</asp:LinkButton>
                                    </li>
                                    <li class="yt-Visited">
                                        <asp:LinkButton ID="lbtn54Review" runat="server" OnClick="lbtn54Review_Click" CausesValidation="False"><strong>4:</strong>Review</asp:LinkButton>
                                    </li>
                                    <li class="yt-Selected"><a><strong>5:</strong> Choose Account Type</a></li><li><a><strong>
                                        6:</strong> Done!</a></li></ul>
                                <div class="yt-ProcessStepDisplay">
                                    <fieldset class="yt-Form">
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table border="0" cellspacing="0" cellpadding="0" class="yt-overlapHead yt-AccountTypeTable"
                                                    id="yt-AccountTypeSelection">
                                                    <thead>
                                                        <tr>
                                                            <th >
                                                                choose your account type
                                                                <%--<asp:CustomValidator ID="CustomValidator1" runat="server" ForeColor="Transparent"
                                                            ClientValidationFunction="SelectAccountType" ErrorMessage=" Please select the account type for this tribute.">.</asp:CustomValidator>--%>
                                                            </th>
                                                            <th style="width: 87px">
                                                                Account Price
                                                            </th>
                                                            <th>
                                                                Includes All Features
                                                            </th>
                                                            <th>
                                                                Advertising Free
                                                            </th>
                                                            <th>
                                                                No Renewal Required
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <%--for AccountType 1--%>
                                                    <tbody id="tableBodyAnnouncement" runat="server" visible="false">
                                                        <tr id="Yearly" runat="server">
                                                            <th>
                                                                <div class="yt-Form-Field-Radio">
                                                                    <asp:RadioButton ID="rdoMembershipFree" GroupName="rdoMembershipType" Checked="false"
                                                                        runat="server" />
                                                                </div>
                                                            </th>
                                                            <td class="yt-Cost">
                                                                Free
                                                            </td>
                                                            <td>
                                                                <img src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/icon_check.gif"
                                                                    alt="Yes" width="21" height="21" />
                                                            </td>
                                                            <td>
                                                                <img src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/icon_x.gif"
                                                                    alt="No" width="21" height="21" />
                                                            </td>
                                                            <td>
                                                                <img src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/icon_check.gif"
                                                                    alt="Yes" width="21" height="21" />
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                    <tbody id="tBodyPersonalUser" runat="server" visible="false">
                                                        <tr>
                                                            <th>
                                                                <div class="yt-Form-Field-Radio">
                                                                    <asp:RadioButton ID="rdbAnnounceFree" GroupName="rdoMembershipType" Checked="false" onClick="hideValidationSummary()"
                                                                        runat="server" AutoPostBack="True" OnCheckedChanged="rdbAnnounceFree_CheckedChanged" />
                                                                </div>
                                                            </th>
                                                            <td class="yt-Cost">
                                                                Free
                                                            </td>
                                                            <td>
                                                                <img src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/icon_check.gif"
                                                                    alt="Yes" width="21" height="21" />
                                                            </td>
                                                            <td>
                                                                <img src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/icon_x.gif"
                                                                    alt="Yes" width="21" height="21" />
                                                            </td>
                                                            <td>
                                                                <img src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/icon_check.gif"
                                                                    alt="No" width="21" height="21" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                <div class="yt-Form-Field-Radio">
                                                                    <asp:RadioButton ID="rdbAnnounceFreeNoAds" GroupName="rdoMembershipType" Checked="false"
                                                                        runat="server" AutoPostBack="True" OnCheckedChanged="rdbAnnounceFreeNoAds_CheckedChanged" />
                                                                </div>
                                                            </th>
                                                            <td class="yt-Cost">
                                                                <asp:Label ID="lblYearlyNoAdsAmount" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <img src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/icon_check.gif"
                                                                    alt="Yes" width="21" height="21" />
                                                            </td>
                                                            <td>
                                                                <img src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/icon_check.gif"
                                                                    alt="Yes" width="21" height="21" />
                                                            </td>
                                                            <td>
                                                                <img src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/icon_check.gif"
                                                                    alt="Yes" width="21" height="21" />
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                    <%--account type ==2 & 3--%>
                                                    <tbody id="tableBodyPhoto" runat="server" visible="false">
                                                        <tr>
                                                            <th>
                                                                <div class="yt-Form-Field-Radio">
                                                                    <asp:RadioButton ID="rdoMembershipYearly" GroupName="rdoMembershipType" Checked="false"
                                                                        runat="server" AutoPostBack="True" OnCheckedChanged="rdoMembershipYearly_CheckedChanged" />
                                                                </div>
                                                            </th>
                                                            <td class="yt-Cost">
                                                                <asp:Label ID="lblYearlyCost" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <img src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/icon_check.gif"
                                                                    alt="Yes" width="21" height="21" />
                                                            </td>
                                                            <td>
                                                                <img src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/icon_check.gif"
                                                                    alt="Yes" width="21" height="21" />
                                                            </td>
                                                            <td>
                                                                <img src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/icon_x.gif"
                                                                    alt="No" width="21" height="21" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                <div class="yt-Form-Field-Radio">
                                                                    <asp:RadioButton ID="rdoMembershipLifetime" GroupName="rdoMembershipType" Checked="false"
                                                                        runat="server" AutoPostBack="True" OnCheckedChanged="rdoMembershipLifetime_CheckedChanged" />
                                                                </div>
                                                            </th>
                                                            <td class="yt-Cost">
                                                                <asp:Label ID="lblLifetimeCost" runat="server" Text="Label"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <img src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/icon_check.gif"
                                                                    alt="Yes" width="21" height="21" />
                                                            </td>
                                                            <td>
                                                                <img src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/icon_check.gif"
                                                                    alt="Yes" width="21" height="21" />
                                                            </td>
                                                            <td>
                                                                <img src="<%=ConfigurationManager.AppSettings["Secured_APP_BASE_DOMAIN"].ToString()%>assets/images/icon_check.gif"
                                                                    alt="Yes" width="21" height="21" />
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                    <%--for Photo Announcement--%>
                                                </table>
                                                <!-- DIV to hide content until membership type is selected -->
                                                <div id="divTotalCredit" runat="server" visible="false">
                                                    <h2 style="text-align: right;">
                                                        Total Credits:
                                                        <asp:Label ID="lblTotalCredit" runat="server"></asp:Label>
                                                    </h2>
                                                </div>
                                                <!-- DIV to hide content until membership type is selected -->
                                                <div id="divCreditPointMessage" class="RemainingCreditPoint" runat="server" visible="false">
                                                    <span id="NetCreditCount" runat="server"></span>
                                                </div>
                                                <!-- For free account -->
                                                <div>
                                                    <asp:Panel ID="PanelFreeTrial" runat="server" Visible="false" Width="624px">
                                                        <p class="yt-InfoBox">
                                                            You have chosen the free account - you can upgrade at any time within the &quot;My
                                                            Account&quot; area.
                                                        </p>
                                                    </asp:Panel>
                                                </div>
                                                <div>
                                                    <asp:Panel ID="PanelBillingInfo" runat="server" Visible="false" Width="624px">
                                                        <div id="CreditGrid" runat="server" visible="False">
                                                            <br />
                                                            <br />
                                                            <p class="yt-requiredFields">
                                                                <strong>Pay-as-you-go credits</strong></p>
                                                            <br />
                                                            Buy as you go credits whenever you need them. Use your credits to create
                                                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>s.
                                                            <br />
                                                            Credits cost as little as 6 USD/credit and never expire.
                                                            <br />
                                                            <div>
                                                                <asp:GridView ID="grdCreditCostTable" class="grdCreditCostheader" GridLines="None"
                                                                    AutoGenerateColumns="false" runat="server" OnRowDataBound="grdCreditCostTable_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Credits" ItemStyle-CssClass="grdCreditCostMapping">
                                                                            <ItemTemplate>
                                                                                <asp:RadioButton ID="rbtnCreditSelection" runat="server" CssClass="grdCreditCostRadioBorder"
                                                                                    GroupName="CreditSelection" AutoPostBack="true" OnCheckedChanged="rbtncreditSelection_CheckedChanged" />
                                                                                <asp:Label ID="lblCreditPoint" runat="server" Text='<%# Bind("CreditPoints") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Price(USD)" ItemStyle-CssClass="grdCreditCostMapping">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblTotalCost" runat="server" ItemStyle-CssClass="grdCreditCostMapping"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Price(per credit)" ItemStyle-CssClass="grdCreditCostMapping">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblCostPercredit" runat="server" Text='<%# Bind("CostPerCredit") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                        <%--CreditGrid end here--%>
                                                        <div class="yt-Form-Field">
                                                            <p>
                                                                Please enter the following payment information:</p>
                                                            <p class="yt-requiredFields">
                                                                <strong>Required fields are indicated with <em class="required">* </em></strong>
                                                            </p>
                                                        </div>
                                                        <div class="yt-Form-Field" id="divCouponCode" runat="server" visible="false">
                                                            <label for="txtCouponCode">
                                                                Enter your coupon code, if you have one:</label>
                                                            <asp:TextBox ID="txtCouponCode" TabIndex="1" runat="server" CssClass="yt-Form-Input-Long"
                                                                MaxLength="18"></asp:TextBox>
                                                            <asp:LinkButton ID="lbtnValidateCoupon" OnClick="lbtnValidateCoupon_Click" runat="server"
                                                                CausesValidation="False" CssClass="yt-checkCoupon">Validate Coupon</asp:LinkButton>
                                                            <span id="spanCoupon" class="availabilityNotice" runat="server"></span>
                                                            <div class="hint">
                                                                If you have a coupon code enter it here and click &quot;Validate Coupon&quot;. If
                                                                the coupon code is correct, the discount will be applied to your total at the bottom
                                                                of the page<span class="hintPointer"></span></div>
                                                        </div>
                                                        <asp:UpdatePanel ID="PnlCoupon" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <div id="PnlPaymentDetails" runat="server" visible="False">
                                                                    <fieldset class="yt-PaymentSelection yt-CompactRadioList">
                                                                        <legend><em class="required">* </em>Select your payment method: </legend>
                                                                        <asp:Literal ID="ltrPaymentMethod" runat="server"></asp:Literal>
                                                                        <asp:CustomValidator ID="cvPaymentMethod" runat="server" Font-Bold="True" Font-Size="Medium"
                                                                            ForeColor="#FF8000" ClientValidationFunction="validatePaymentMethod" Text="!"
                                                                            ErrorMessage="Select your payment method." Width="1px"></asp:CustomValidator>
                                                                    </fieldset>
                                                                    <br />
                                                                    <!-- Start - Modification on 14-Dec-09 for the enhancement 2 of the Phase 1 -->
                                                                    <table>
                                                                        <tr>
                                                                            <td>
                                                                                <div class="yt-Form-Field">
                                                                                    <label>
                                                                                        <em class="required">* </em>Credit Card Number:</label>
                                                                                    <asp:TextBox ID="txtCCNumber" CssClass="yt-Form-Input-Long" runat="server" MaxLength="16"
                                                                                        Width="280px" TabIndex="2">
                                                                                    </asp:TextBox><asp:RequiredFieldValidator ID="rfvCCNumber" Font-Bold="True" Font-Size="Medium"
                                                                                        ForeColor="#FF8000" ControlToValidate="txtCCNumber" runat="server" Text="!" ErrorMessage="Credit Card Number is a required field."
                                                                                        Width="1px"></asp:RequiredFieldValidator>
                                                                                    <%--<asp:CustomValidator ID="cvCCNumber" runat="server" Font-Bold="True" Font-Size="Medium"
                                                                                        ForeColor="#FF8000" Text="!" ClientValidationFunction="CCardLength" ErrorMessage="Please enter a valid credit card number. "
                                                                                        Width="1px"></asp:CustomValidator> --%>
                                                                                    <asp:CustomValidator ID="cvCreditCardNumber" runat="server" Font-Bold="True" Font-Size="Medium"
                                                                                        ForeColor="#FF8000" ClientValidationFunction="validateCreditCardLength" Text="!"
                                                                                        EnableClientScript="true" ErrorMessage="Please enter a valid Credit Card Number."
                                                                                        Width="1px"></asp:CustomValidator>
                                                                                </div>
                                                                                <div class="yt-Form-Field">
                                                                                    <label>
                                                                                        <em class="required">* </em>Card Verification Code (CVC):</label>
                                                                                    <asp:TextBox ID="txtCCVerification" CssClass="yt-Form-Input-Short" runat="server"
                                                                                        MaxLength="4" TabIndex="3" TextMode="Password"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ControlToValidate="txtCCVerification" Font-Bold="True"
                                                                                        Font-Size="Medium" ForeColor="#FF8000" ID="rfvCCVerification" Text="!" runat="server"
                                                                                        ErrorMessage="Card Verification Code (CVC) is a required field." Width="1px"></asp:RequiredFieldValidator>
                                                                                    <%-- <asp:CustomValidator ID="CustomValidator4" runat="server" Font-Bold="True" Font-Size="Medium"
                                                                                        ForeColor="#FF8000" Text="!" ClientValidationFunction="CVCLength" ErrorMessage="Please enter a valid card verification code (CVC). "
                                                                                        Width="1px"></asp:CustomValidator>--%>
                                                                                    <asp:CustomValidator ID="cvCreditCardVerification" runat="server" Font-Bold="True"
                                                                                        Font-Size="Medium" ForeColor="#FF8000" ClientValidationFunction="validateCreditCardVerificationLength"
                                                                                        Text="!" EnableClientScript="true" ErrorMessage="Please enter a valid Credit Card Verification Number."
                                                                                        Width="1px"></asp:CustomValidator>
                                                                                    <div class="hint">
                                                                                        The CVC is located on the back of MasterCard, Visa and Discover credit cards and
                                                                                        is a separate group of 3 digits to the right of the signature strip. On American
                                                                                        Express cards, the CVC is a separate group of 4 digits on the front right of the
                                                                                        card.<span class="hintPointer"></span></div>
                                                                                </div>
                                                                            </td>
                                                                            <td>
                                                                                <table>
                                                                                    <tr>
                                                                                        <td>
                                                                                            <!-- BEGIN DigiCert Site Seal Code -->
                                                                                            <div id="digicertsitesealcode" style="width: 81px; margin: 5px auto 5px 5px;" align="center">

                                                                                                <script language="javascript" type="text/javascript" src="https://www.digicert.com/custsupport/sealtable.php?order_id=00192234&amp;seal_type=a&amp;seal_size=large&amp;seal_color=blue&amp;new=1"></script>

                                                                                                <a href="https://www.digicert.com">SSL Certificates</a>

                                                                                                <script language="javascript" type="text/javascript">                                                                                                    coderz();</script>

                                                                                            </div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <div>
                                                                                                <a href="https://www.bbb.org/online/consumer/cks.aspx?id=10906081881" target="_blank">
                                                                                                    <img id="imgBBB" runat="server" alt="BBB Online" src="~/assets/images/bbb.gif" />
                                                                                                </a>
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <!-- End -->
                                                                    <fieldset class="yt-Date-Fields">
                                                                        <legend><em class="required">* </em>Expiry Date:</legend>
                                                                        <div class="yt-Form-Field">
                                                                            <asp:DropDownList ID="ddlCCMonth" runat="server" TabIndex="4" Width="132px">
                                                                                <asp:ListItem Value="--"></asp:ListItem>
                                                                                <asp:ListItem Value="1">January</asp:ListItem>
                                                                                <asp:ListItem Value="2">February</asp:ListItem>
                                                                                <asp:ListItem Value="3">March</asp:ListItem>
                                                                                <asp:ListItem Value="4">April</asp:ListItem>
                                                                                <asp:ListItem Value="5">May</asp:ListItem>
                                                                                <asp:ListItem Value="6">June</asp:ListItem>
                                                                                <asp:ListItem Value="7">July</asp:ListItem>
                                                                                <asp:ListItem Value="8">August</asp:ListItem>
                                                                                <asp:ListItem Value="9">September</asp:ListItem>
                                                                                <asp:ListItem Value="10">October</asp:ListItem>
                                                                                <asp:ListItem Value="11">November</asp:ListItem>
                                                                                <asp:ListItem Value="12">December</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <label>
                                                                                Month</label>
                                                                        </div>
                                                                        <div class="yt-Form-Field">
                                                                            <asp:TextBox ID="txtCCYear" TabIndex="5" CssClass="yt-Form-Input-Short" MaxLength="4"
                                                                                runat="server"></asp:TextBox>
                                                                            <asp:CustomValidator ID="CustomValidator3" runat="server" Font-Bold="True" Font-Size="Medium"
                                                                                ForeColor="#FF8000" ClientValidationFunction="validateExpMonth" Text="!" ErrorMessage="Expiry Date is a required field."
                                                                                Width="1px"></asp:CustomValidator>
                                                                            <span style="color: #FF8000; font-size: Medium; font-weight: bold;" id="SpanExpirDate"
                                                                                runat="server" visible="false">!</span>
                                                                            <label>
                                                                                Year</label>
                                                                        </div>
                                                                    </fieldset>
                                                                    <div class="yt-Form-Field">
                                                                        <label>
                                                                            <em class="required">* </em>Name on Card:</label>
                                                                        <asp:TextBox ID="txtCCName" TabIndex="6" CssClass="yt-Form-Input-Long" runat="server"
                                                                            MaxLength="50" Width="280px"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvCCName" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"
                                                                            Text="!" runat="server" ErrorMessage="Name on Card is a required field." ControlToValidate="txtCCName"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="revNameCard" ControlToValidate="txtCCName" Text="!"
                                                                            runat="server" ErrorMessage="Name on Card can only contain letters,numbers,'-' and '#'"
                                                                            ValidationExpression="^[a-zA-Z0-9,\#,\-,\s]*$" Font-Bold="True" Font-Size="Medium"
                                                                            ForeColor="#FF8000"></asp:RegularExpressionValidator>
                                                                    </div>
                                                                    <div class="yt-Form-Field">
                                                                        <label>
                                                                            <em class="required">* </em>Billing Address:</label>
                                                                        <asp:TextBox ID="txtCCBillingAddress" TabIndex="7" CssClass="yt-Form-Input-Long"
                                                                            runat="server" MaxLength="50" Width="280px"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCCBillingAddress"
                                                                            ErrorMessage="Billing Address (line 1) can only contain letters,numbers,'-' and '#'"
                                                                            Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9,\#,\-,\s\'\.]*$"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                                                                ID="rfvCCBillingAddress" runat="server" Text="!" Font-Bold="True" Font-Size="Medium"
                                                                                ForeColor="#FF8000" ControlToValidate="txtCCBillingAddress" ErrorMessage="Billing Address is a required field."></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="yt-Form-Field">
                                                                        <asp:TextBox ID="txtCCBillingAddress2" TabIndex="8" CssClass="yt-Form-Input-Long"
                                                                            runat="server" MaxLength="50" Width="280px"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtCCBillingAddress2"
                                                                            ErrorMessage="Billing Address (line 2) can only contain letters,numbers,'-' and '#'"
                                                                            Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9,\#,\-,\s\'\.]*$"></asp:RegularExpressionValidator>
                                                                    </div>
                                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                        <ContentTemplate>
                                                                            <div class="yt-Form-Field">
                                                                                <label>
                                                                                    <em class="required">* </em>Country:</label>
                                                                                <asp:DropDownList ID="ddlCCCountry" TabIndex="9" runat="server" AutoPostBack="True"
                                                                                    OnSelectedIndexChanged="ddlCCCountry_SelectedIndexChanged" Width="285px">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                            <div class="yt-Form-Field">
                                                                                <label>
                                                                                    State/Province:</label>
                                                                                <asp:DropDownList ID="ddlCCStateProvince" TabIndex="10" runat="server" Width="285px">
                                                                                </asp:DropDownList>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                    <div class="yt-Form-Field">
                                                                        <label>
                                                                            <em class="required">* </em>City:</label>
                                                                        <asp:TextBox ID="txtCCCity" CssClass="yt-Form-Input-Long" TabIndex="11" runat="server"
                                                                            Width="280px" MaxLength="50"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="revtxtCCCity" runat="server" ControlToValidate="txtCCCity"
                                                                            ErrorMessage="City only contain letters,numbers,'-' and '#'" Font-Bold="True"
                                                                            Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9,\#,\-,\s\'\.]*$"></asp:RegularExpressionValidator><asp:RequiredFieldValidator
                                                                                ID="rfvCCCity" runat="server" Text="!" ControlToValidate="txtCCCity" ErrorMessage="City is a required field."
                                                                                Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000"></asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="yt-Form-Field">
                                                                        <label>
                                                                            <em class="required">* </em>Zip Code/Postal Code:</label>
                                                                        <asp:TextBox ID="txtCCZipCode" TabIndex="12" CssClass="yt-Form-Input" runat="server"
                                                                            MaxLength="10"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvCCZipCode" runat="server" Text="!" ControlToValidate="txtCCZipCode"
                                                                            ErrorMessage="Zip Code/Postal Code is a required field." Font-Bold="True" Font-Size="Medium"
                                                                            ForeColor="#FF8000"></asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="revZipCode" runat="server" ControlToValidate="txtCCZipCode"
                                                                            ErrorMessage="Zip Code/Postal Code can only contain letters and numbers" Font-Bold="True"
                                                                            Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9\s]*$"></asp:RegularExpressionValidator>
                                                                    </div>
                                                                    <div class="yt-Form-Field">
                                                                        <label>
                                                                            <em class="required">* </em>Phone Number:</label>
                                                                        (<asp:TextBox ID="txtPhoneNumber1" TabIndex="13" runat="server" Width="34px" MaxLength="3"
                                                                            CssClass="yt-Form-Input-Long"></asp:TextBox>)
                                                                        <asp:TextBox ID="txtPhoneNumber2" runat="server" TabIndex="14" Width="34px" MaxLength="3"
                                                                            CssClass="yt-Form-Input-Long"></asp:TextBox>
                                                                        -
                                                                        <asp:TextBox ID="txtPhoneNumber3" runat="server" Width="40px" TabIndex="15" MaxLength="4"
                                                                            CssClass="yt-Form-Input-Long"></asp:TextBox>
                                                                        <asp:CustomValidator ID="cvPhoneNumber" runat="server" Font-Bold="True" Font-Size="Medium"
                                                                            ForeColor="#FF8000" ErrorMessage="XXX-XXXX." ClientValidationFunction="ValidatePhoneNumber">!</asp:CustomValidator>
                                                                    </div>
                                                                    <div class="yt-Form-Field">
                                                                        <label>
                                                                            <em class="required">* </em>Email Address:</label>
                                                                        <asp:TextBox ID="txtEmailAddress" CssClass="yt-Form-Input-Long" runat="server" MaxLength="100"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvEmailAddress" runat="server" Text="!" ControlToValidate="txtEmailAddress"
                                                                            ErrorMessage="Email address is a required field." Font-Bold="True" Font-Size="Medium"
                                                                            ForeColor="#FF8000">
                                                                        </asp:RequiredFieldValidator>
                                                                        <asp:RegularExpressionValidator ID="revEmailAddress" runat="server" Text="!" ControlToValidate="txtEmailAddress"
                                                                            ErrorMessage="Enter valid email address." Font-Bold="True" Font-Size="Medium"
                                                                            ForeColor="#FF8000" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                                    </div>
                                                                    <fieldset class="yt-RenewalSelection" id="PamentSel" runat="server">
                                                                        <legend><em class="required">* </em>Renewal Options</legend>
                                                                        <div class="yt-Form-Field yt-Form-Field-Radio">
                                                                            <asp:RadioButton ID="rdoNotifyBeforeRenew" TabIndex="16" GroupName="rdoRenew" runat="server"
                                                                                Checked="True" />
                                                                        </div>
                                                                        <div class="yt-Form-Field yt-Form-Field-Radio">
                                                                            <asp:RadioButton ID="rdoYearlyAutoRenew" TabIndex="17" GroupName="rdoRenew" runat="server" />
                                                                        </div>
                                                                    </fieldset>
                                                                    <fieldset class="yt-SaveBillingInfo">
                                                                        <div class="yt-Form-Field yt-Form-Field-Checkbox">
                                                                            <asp:CheckBox ID="chkSaveBillingInfo" TabIndex="18" runat="server" />
                                                                        </div>
                                                                    </fieldset>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                            <ContentTemplate>
                                                                <div class="yt-InfoBox" id="yt-PaymentTotal">
                                                                    You will be charged:<span id="BillingTotal" runat="server"></span>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                        <fieldset class="yt-TermsOfUse">
                                                            <div class="yt-Form-Field yt-Form-Field-Checkbox">
                                                                <asp:CheckBox ID="chkAgree" TabIndex="19" runat="server" Text="<em class='required'>*</em> I have read and agree to the <a href='termsofuse.aspx' target='_blank' >terms of use</a>, the cancellation/refund                                                                   policy (outlined in the terms of use) and the <a href='privacy.aspx'  target='_blank' >privacy policy</a>."
                                                                    Checked="False" />
                                                                <asp:CustomValidator ID="CustomValidator4" Text="!" ForeColor="Transparent" ErrorMessage="Please accept that you have read and agree to the terms of use, cancellation/refund policy and the privacy policy."
                                                                    runat="server" ClientValidationFunction="ValidateTandCs" Width="1px"></asp:CustomValidator>
                                                            </div>
                                                        </fieldset>
                                                        <p>
                                                            If you have reviewed all of the above information and it is correct, you must be
                                                            ready to...</p>
                                                    </asp:Panel>
                                                </div>
                                                                                        <asp:Panel ID="pnlLowerBillingInfo" runat="server" Visible="false">
                                            <div class="yt-InfoBox" id="LowerBillingInfo">
                                                You will be charged:<span id="SpanLowerBillingInfo" runat="server"></span>
                                            </div>
                                        </asp:Panel>
                                                <!-- end hidden BillingInfo DIV -->
                                                <%--   </ContentTemplate>
                                        </asp:UpdatePanel>--%>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                        <div class="yt-Form-Buttons">
                                            <div class="yt-Form-Cancel">
                                                <% if (ConfigurationManager.AppSettings["ApplicationType"].ToString().ToLower().Equals("yourtribute"))
                                                   { %>
                                                <a href="javascript: void(0);" onclick="cancelTributeCreation('tribute');">Cancel
                                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                                                    Creation</a>
                                                <%} %>
                                                <%else
                                                    { %>
                                                <a href="javascript: void(0);" onclick="cancelTributeCreation('website');">Cancel
                                                    <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                                                    Creation</a>
                                                <%} %>
                                            </div>
                                            <div class="yt-Form-Submit">
                                                <div id="divCreate1" style="float: right; display: block; visibility: visible">
                                                    <asp:LinkButton ID="lbtnCreatetribute" TabIndex="21" runat="server" CssClass="yt-Button yt-ArrowButton"
                                                        OnClick="lbtnCreatetribute_Click" OnClientClick="return OnPayClick();">Create the 
                                                <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>!</asp:LinkButton>
                                                    <asp:Label ID="lblProcess" runat="server"></asp:Label>
                                                </div>
                                                                                            </div>
                                        </div>
                                        <!-- end yt-Form-Buttons-->
                                    </fieldset>
                                </div>
                                <!--yt-ProcessStepDisplay-->
                            </div>
                            <!--yt-TributeProcess-->
                        </div>
                        <%--yt-ContentPrimary--%></asp:Panel>
                </div>
            </asp:View>
            <asp:View ID="Step6" runat="server">
                <div>
                    <asp:Panel ID="PanelStep6" Visible="false" runat="server" Width="663px">
                        <div class="yt-Step6">
                            <div class="yt-ContentPrimary">
                                <div class="yt-TributeProcess">
                                    <ul class="yt-ProcessSteps">
                                        <li><a><strong>1:</strong> Enter
                                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                            Details</a></li>
                                        <li><a><strong>2:</strong> More
                                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                            Details</a></li>
                                        <li><a><strong>3:</strong>
                                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%>
                                            Management</a></li>
                                        <li><a><strong>4:</strong> Review</a></li>
                                        <li><a><strong>5:</strong> Choose Account Type</a></li>
                                        <li class="yt-Selected"><a><strong>6:</strong> Done!</a></li>
                                    </ul>
                                    <div class="yt-ProcessStepDisplay">
                                        <div id="divPaid" runat="server">
                                            <h2>
                                                Your order has been approved!
                                            </h2>
                                            <div id="package11" runat="server">
                                            </div>
                                            <div id="package12" runat="server">
                                            </div>
                                            <div id="package2" runat="server">
                                            </div>
                                            <div id="autoRenew" runat="server">
                                                You can turn off auto-renewal at any time in tribute management.
                                                <br />
                                            </div>
                                            <div id="recieveMail" runat="server">
                                                <p id="emailreceipt1" runat="server">
                                                    You will receive your full order receipt by email. If you do not receive the order
                                                    receipt in the next hour, please check your junk email folder. You can also view
                                                    your billing history in &quot;MY PROFILE&quot; under &quot;Billing Info&quot;,in
                                                    case you chose to save it.
                                                </p>
                                                <p id="emailreceipt2" runat="server">
                                                    Please print a copy of this page for your records.</p>
                                            </div>
                                            <br />
                                            <br />
                                        </div>
                                        <h3>
                                            Now that your
                                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                                            has been created it is time to add content!</h3>
                                        <p>
                                            You could start by creating <a href="<%=_tributeURL %>/story.aspx">the story</a>,
                                            where visitors can find out more about who the
                                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                                            is dedicated to. Or, you could post a <a href="<%=_tributeURL %>/events.aspx">new event</a>
                                            related to the new
                                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>,
                                            and invite people too. <a href="<%=_tributeURL %>/photos.aspx">Photo albums</a>
                                            and <a href="<%=_tributeURL %>/videos.aspx">videos</a> are also a great place to
                                            start — it is really up to you!</p>
                                        <p>
                                            What we can tell you is that the best
                                            <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%>
                                            sites are the ones that are loaded with content — that way visitors will spend time
                                            exploring and will be encouraged to contribute.</p>
                                        <div id="trial" runat="server">
                                            <h3>
                                                And remember, your trial period starts now!</h3>
                                        </div>
                                        <div class="yt-Form-Buttons">
                                            <div class="yt-Form-Submit">
                                                <asp:LinkButton ID="lbtnStartaddingcontent" CssClass="yt-Button yt-ArrowButton" runat="server"
                                                    OnClick="lbtnStartaddingcontent_Click">Start adding content!</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
                <!-- Google Code for Purchase Conversion Page -->

                <script type="text/javascript">
/* <![CDATA[ */
var google_conversion_id = 1040748494;
var google_conversion_language = "en";
var google_conversion_format = "3";
var google_conversion_color = "ffffff";
var google_conversion_label = "t8aCCOKp1wEQzp-i8AM";
var google_conversion_value = 0;
if (<%= totalValue %>) {
  google_conversion_value = <%= totalValue %>;
}
/* ]]> */
                </script>

                <script type="text/javascript" src="https://www.googleadservices.com/pagead/conversion.js">
                </script>

                <noscript>
                    <div style="display: inline;">
                        <img height="1" width="1" style="border-style: none;" alt="" src="https://www.googleadservices.com/pagead/conversion/1040748494/??value=<%= totalValue %>&amp;label=t8aCCOKp1wEQzp-i8AM&amp;guid=ON&amp;script=0" />
                    </div>
                </noscript>
            </asp:View>
        </asp:MultiView>
    </div>

    <script language="javascript" type="text/javascript">

        var isValid = true;

        function MakeAutoRenew() {
            var rdb = document.getElementById('<%=rdoYearlyAutoRenew.ClientID%>');
            var chk = document.getElementById('<%=chkSaveBillingInfo.ClientID%>');
            if (rdb) {
                if (rdb.checked == true) {
                    chk.checked = true;
                    chk.disabled = true;
                }
                else {
                    chk.checked = false;
                    chk.disabled = false;
                }
            }
        }

        function ValidateTributeName(source, args) {
            var txtTributeName = document.getElementById('<%=txtTributeName.ClientID%>');
            args.IsValid = TributeNameValidate(txtTributeName.value);
        }

        function ValidateTributeFirstName(source, args) {
            var txtTributeFirstName = document.getElementById('<%=txtTributeFirstName.ClientID%>');
            args.IsValid = TributeNameValidate(txtTributeFirstName.value);
        }

        function ValidateTributeLastName(source, args) {
            var txtTributeLastName = document.getElementById('<%=txtTributeLastName.ClientID%>');
            args.IsValid = TributeNameValidate(txtTributeLastName.value);
        }

        function CCardLength(source, args) {
            var CCNumber = document.getElementById('<%=txtCCNumber.ClientID%>');
            if (CCNumber) {
                var value = CreditCardCLength(CCNumber);
                if (value == 1)
                    args.IsValid = true;
                else
                    args.IsValid = false;
            }
        }

        function CVCLength(source, args) {
            var CCNumber = document.getElementById('<%=txtCCVerification.ClientID%>');
            args.IsValid = CVCLengthCC(CCNumber)
        }

        function Check(rdb) {
            var rdb1 = $('ctl00_TributePlaceHolder_hfPaymentMethod');
            rdb1.value = rdb.value;
        }

        function validatePaymentMethod(source, args) {

            var rdb1 = $('rdoCCVisa');
            var rdb2 = $('rdoCCMasterCard');
            args.IsValid = PaymentMethodvalidate(rdb1, rdb2);

            if (args.IsValid == false) {
                isValid = false;
            }
            ToggleCreateTributeLink();
        }

        function validateCreditCardLength(source, args) {
            var bool = false;
            var rdb1 = $('rdoCCVisa');

            var rdb4 = $('rdoCCMasterCard');
            var val = document.getElementById('<%=txtCCNumber.ClientID%>').value;

            if (val.length == 0) {
                bool = true;
                args.IsValid = bool;
                isValid = false;

                return;
            }

            if (rdb1.checked) {

                if (val.length != 16) {
                    bool = false;
                }
                else
                    bool = true;
            }
            else {
                if (val.length != 15) {
                    bool = false;
                }
                else
                    bool = true;
            }

            args.IsValid = bool;
            if (args.IsValid == false) {
                isValid = false;
            }
            ToggleCreateTributeLink();
        }


        function validateCreditCardVerificationLength(source, args) {
            var bool = false;
            var rdb1 = $('rdoCCVisa');

            var rdb4 = $('rdoCCMasterCard');
            var val = document.getElementById('<%=txtCCVerification.ClientID%>').value;
            if (val.length == 0) {
                bool = true;
                args.IsValid = bool;
                isValid = false;
                return;
            }

            if (rdb1.checked) {
                if (val.length != 3) {
                    bool = false;
                }
                else
                    bool = true;
            }
            else {
                if (val.length != 4) {
                    bool = false;
                }
                else
                    bool = true;
            }

            args.IsValid = bool;

            if (args.IsValid == false) {
                isValid = false;
            }
            ToggleCreateTributeLink();
        }

        function validateExpMonth(source, args) {
            var bol = true;
            var month = document.getElementById('<%=ddlCCMonth.ClientID%>');
            var year = document.getElementById('<%=txtCCYear.ClientID%>');
            var validat = document.getElementById('<%=CustomValidator3.ClientID%>');
            args.IsValid = ExpMonthvalidate(month, year, validat);

            if (args.IsValid == false) {
                isValid = false;
            }
            ToggleCreateTributeLink();
        }

        function CheckLenght() {
            var textarea = document.getElementById('<%=txtarMessage.ClientID%>');
            var numberRemaining = document.getElementById('<%=numberRemaining.ClientID%>');

            LenghtCheck(textarea, numberRemaining);
        }

        function ValidateConfirmation(source, args) {
            var rdoPrivate = document.getElementById('<%= rdoPrivate.ClientID %>');
            var rdoPublic = document.getElementById('<%= rdoPublic.ClientID %>');
            args.IsValid = ValidatePrivacy(rdoPrivate, rdoPublic)
        }

        //Validate the Donation Email Address

        function ValidateDates(source, args) {
            var ddlmonth = document.getElementById('<%= ddlMonth.ClientID %>');
            var ddlDay = document.getElementById('<%= ddlDay.ClientID %>');
            var txtYear = document.getElementById('<%= txtYear.ClientID %>');
            var ddlmonth2 = document.getElementById('<%= ddlMonth2.ClientID %>');
            var ddlDay2 = document.getElementById('<%= ddlDay2.ClientID %>');
            var txtYear2 = document.getElementById('<%= txtYear2.ClientID %>');
            var cvDate12 = document.getElementById('<%= cvDate12.ClientID %>');
            var Tribute = document.getElementById('<%= hfTributeValue.ClientID %>').value;
            args.IsValid = ValidateBothDate(ddlmonth, ddlDay, txtYear, ddlmonth2, ddlDay2, txtYear2, cvDate12, Tribute);
        }

        function ValidateDate(source, args) {
            var ddlmonth = document.getElementById('<%= ddlMonth.ClientID %>');
            var ddlDay = document.getElementById('<%= ddlDay.ClientID %>');
            var txtYear = document.getElementById('<%= txtYear.ClientID %>');
            var cvDate1 = document.getElementById('<%= cvDate1.ClientID %>');
            var Tribute = document.getElementById('<%= hfTributeValue.ClientID %>').value;
            args.IsValid = DateValidation(ddlmonth, ddlDay, txtYear, cvDate1, Tribute);
        }

        function ValidateDate2(source, args) {
            var ddlmonth = document.getElementById('<%= ddlMonth.ClientID %>');
            var ddlDay = document.getElementById('<%= ddlDay.ClientID %>');
            var txtYear = document.getElementById('<%= txtYear.ClientID %>');
            var ddlmonth2 = document.getElementById('<%= ddlMonth2.ClientID %>');
            var ddlDay2 = document.getElementById('<%= ddlDay2.ClientID %>');
            var txtYear2 = document.getElementById('<%= txtYear2.ClientID %>');
            var cvDate2 = document.getElementById('<%= cvDate2.ClientID %>');
            var Tribute = document.getElementById('<%= hfTributeValue.ClientID %>').value;
            args.IsValid = DateValidation2(ddlmonth, ddlDay, txtYear, ddlmonth2, ddlDay2, txtYear2, cvDate2, Tribute);
        }
        //        LHK
        function ValidateVTDate2(source, args) {
            var ddlmonth = document.getElementById('<%= ddlMonth.ClientID %>');
            var ddlDay = document.getElementById('<%= ddlDay.ClientID %>');
            var txtYear = document.getElementById('<%= txtYear.ClientID %>');
            var ddlmonth2 = document.getElementById('<%= ddlMonth2.ClientID %>');
            var ddlDay2 = document.getElementById('<%= ddlDay2.ClientID %>');
            var txtYear2 = document.getElementById('<%= txtYear2.ClientID %>');
            var cvDate2 = document.getElementById('<%= cvDate2.ClientID %>');
            args.IsValid = VTDateValidation2(ddlmonth, ddlDay, txtYear, ddlmonth2, ddlDay2, txtYear2, cvDate2);
        }
        // till here
        function ValidateNewBaby(source, args) {
            var ddlmonth2 = document.getElementById('<%= ddlMonth2.ClientID %>');
            var ddlDay2 = document.getElementById('<%= ddlDay2.ClientID %>');
            var txtYear2 = document.getElementById('<%= txtYear2.ClientID %>');
            var cvNewbaby = document.getElementById('<%= cvNewbaby.ClientID %>');
            var Tribute = document.getElementById('<%= hfTributeValue.ClientID %>').value;
            args.IsValid = NewBabyValidate(ddlmonth2, ddlDay2, txtYear2, cvNewbaby, Tribute);
        }


        function SetUniqueRadioButton(nameregex, current) {
            UniqueRadioButton(nameregex, current);
        }

        function ValidateAddress(source, args) {
            var addrs1 = document.getElementById('<%= rdbAvailableAddress1.ClientID %>');
            var addrs2 = document.getElementById('<%= rdbAvailableAddress2.ClientID %>');
            var addrs3 = document.getElementById('<%= rdbAvailableAddress3.ClientID %>');
            var addrs4 = document.getElementById('<%= txtTributeAddressOther.ClientID %>');
            args.IsValid = AddressValidate(addrs1, addrs2, addrs3, addrs4);
        }

        function TributeCancelation() {
            return confirm("Do you want to cancel tribute?");
        }

        function ValidateTandCs(source, args) {
            args.IsValid = document.getElementById('<%= chkAgree.ClientID %>').checked;

            if (args.IsValid == false) {
                isValid = false;
            }
            ToggleCreateTributeLink();
        }

        function Reset1() {
            var ddlMonth = document.getElementById('<%= ddlMonth.ClientID %>');
            var ddlDay = document.getElementById('<%= ddlDay.ClientID %>');
            var txtYear = document.getElementById('<%= txtYear.ClientID %>');
            txtYear.value = "";
            ddlMonth.selectedIndex = 0;
            ddlDay.selectedIndex = 0;
            HideIndicator3();
        }

        function Reset2() {
            var ddlMonth = document.getElementById('<%= ddlMonth2.ClientID %>');
            var ddlDay = document.getElementById('<%= ddlDay2.ClientID %>');
            var txtYear = document.getElementById('<%= txtYear2.ClientID %>');
            txtYear.value = "";
            ddlMonth.selectedIndex = 0;
            ddlDay.selectedIndex = 0;
            HideIndicator3();
        }


        function CalloutBox() {
            /* attach hint events */
            $$('.hint').each(function(a) {
                $E('input', a.parentNode).addEvent('focus', function() {
                    a.setStyle('display', 'inline');
                });

                $E('input', a.parentNode).addEvent('blur', function() {
                    a.setStyle('display', 'none');
                });
            });
        }

        function HideIndicator() {
            var ExpirDate = document.getElementById('<%= SpanExpirDate.ClientID %>');
            if (ExpirDate)
                ExpirDate.style.visibility = 'hidden';
            hidesummery();
        }

        function HideIndicator2() {
            var ExpirDate = document.getElementById('<%= errorAddress.ClientID %>');
            if (ExpirDate)
                ExpirDate.style.visibility = 'hidden';
            HideIndicator3();
            hidesummery();
        }

        function hidesummery() {
            var lblErrMsg = document.getElementById('<%= lblErrMsg.ClientID %>');
            if (lblErrMsg) {
                lblErrMsg.innerHTML = '';
                lblErrMsg.style.visibility = 'hidden';
            }
        }

        function HideIndicator3() {
            var Indecator1 = document.getElementById('<%= Indecator1.ClientID %>');
            var Indecator2 = document.getElementById('<%= Indecator2.ClientID %>');
            if (Indecator1)
                Indecator1.style.visibility = 'hidden';
            if (Indecator2)
                Indecator2.style.visibility = 'hidden';

            hidesummery();
        }

        function SetTheme(bdrid, value, rdb) {
            var HiddenField1 = document.getElementById('<%= HiddenField1.ClientID %>');
            HiddenField1.value = bdrid + ":" + value + ":" + rdb;
        }

        function SetThemeFolder(bdrid, value, rdb, folderName) {
            var HiddenField1 = document.getElementById('<%= HiddenField1.ClientID %>');
            HiddenField1.value = bdrid + ":" + value + ":" + rdb + ":" + folderName;
        }

        function ValidateTribute(source, args) {
            var rdb1 = document.getElementById('<%= NewBaby.ClientID %>');
            var rdb2 = document.getElementById('<%= Birthday.ClientID %>');
            var rdb3 = document.getElementById('<%= Graduation.ClientID %>');
            var rdb4 = document.getElementById('<%= Wedding.ClientID %>');
            var rdb5 = document.getElementById('<%= Anniversary.ClientID %>');
            var rdb6 = document.getElementById('<%= Memorial.ClientID %>');
            args.IsValid = TributeValidate(rdb1, rdb2, rdb3, rdb4, rdb5, rdb6);
        }

        function ValidatePhoneNumber(source, args) {
            var number1 = document.getElementById('<%= txtPhoneNumber1.ClientID %>');
            var number2 = document.getElementById('<%= txtPhoneNumber2.ClientID %>');
            var number3 = document.getElementById('<%= txtPhoneNumber3.ClientID %>');
            var validator = document.getElementById('<%= cvPhoneNumber.ClientID %>');

            args.IsValid = PhoneNumberValidate_(number1, number2, number3, validator);

            if (args.IsValid == false) {
                isValid = false;
            }
            ToggleCreateTributeLink();
        }

        function confirmSubmit(control) {
            var reply = confirm("You will lost the information");
            if (reply)
                control.checked = true;
            else
                control.checked = false;
        }


        var defaultChecked = -1;
        function check(theBut, idx) {
            if (true) {
                if (confirm('Are you sure you want to change the tributeType? All tribute details will be lost.')) {
                    return true;
                }
                else {
                    var txt = document.getElementById('<%= hfSelectedTribute.ClientID %>');
                    var rdb = document.getElementById(txt.value);
                    rdb.checked = true;
                    return false;
                }
            }
            defaultChecked = idx
        }


        function SetImage(url) {
            document.getElementById('<%=imgTributePhoto.ClientID%>').src = url;
            document.getElementById('<%=hdnStoryImageURL.ClientID%>').value = url;

        }

        function SetYearlyAmount() {
            //$('ctl00_TributePlaceHolder_lblTotalPayment').innerHTML='$20';
        }

        function SetLifetimeAmount() {
            // $('ctl00_TributePlaceHolder_lblTotalPayment').innerHTML='$50';
        }

        // this function is used to toggle the lbtnCreatetribute and lnkPostbackDisabled (to avoid multiple submits)
        function ToggleCreateTributeLink() {
            var e = $get('divCreate1');
            var d = $get('divCreate2');
            if (e && d) {
                if (isValid == true) {
                    d.style.display = 'block';
                    d.style.visibility = 'visible';
                    e.style.display = 'none';
                    e.style.visibility = 'hidden';
                }
                else {
                    e.style.display = 'block';
                    e.style.visibility = 'visible';
                    d.style.display = 'none';
                    d.style.visibility = 'hidden';
                }
            }
        }

        function ToggleCreateTributeLink_Old(state) {
            var e = $get('divCreate1');
            var d = $get('divCreate2');

            if (e && d) {
                if (state == true) {
                    d.style.display = 'block';
                    d.style.visibility = 'visible';
                    e.style.display = 'none';
                    e.style.visibility = 'hidden';
                }
                else {
                    e.style.display = 'block';
                    e.style.visibility = 'visible';
                    d.style.display = 'none';
                    d.style.visibility = 'hidden';
                }
            }
        }
        function OnPayClick() {
            if (typeof (Page_ClientValidate) == 'function') {
                var isPageValid = Page_ClientValidate();
                if (isPageValid) {
                    document.getElementById('<%=lblProcess.ClientID%>').innerHTML = "Please wait";
                    document.getElementById('<%=lbtnCreatetribute.ClientID%>').style.display = "none";
                    return true;
                }
                else {
                    return false;
                }
            }
            else {
                var IsTrial = document.getElementById('<%=PnlPaymentDetails.ClientID%>')
                if (IsTrial == null) {
                    document.getElementById('<%=lblProcess.ClientID%>').innerHTML = "Please wait";
                    document.getElementById('<%=lbtnCreatetribute.ClientID%>').style.display = "none";
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        
        
        function hideValidationSummary() {
            document.getElementById('<%=PortalValidationSummary.ClientID%>').style.display = 'none';
        }
        

        function ValidateInputString() {
        var fName = document.getElementById('<%=txtTributeFirstName.ClientID%>');
        var lName = document.getElementById('<%=txtTributeLastName.ClientID%>');
        var TributeName = document.getElementById('<%=txtTributeName.ClientID%>');
        if (lName.value.length > 0) {
            TributeName.value = fName.value.replace(/[^a-zA-Z0-9]/g, '') + ' ' + lName.value.replace(/[^a-zA-Z0-9]/g, '');
        }
        else {
            TributeName.value = (fName.value).replace(/[^a-zA-Z0-9]/g, '');
        }
        };


        Sys.Browser.WebKit = {};
        if (navigator.userAgent.indexOf('WebKit/') > -1) {
            Sys.Browser.agent = Sys.Browser.WebKit;
            Sys.Browser.version = parseFloat(navigator.userAgent.match(/WebKit\/(\d+(\.\d+)?)/)[1]);
            Sys.Browser.name = 'WebKit';
        }
        
        

    </script>

    <!-- Google Code for Tribute Creation (Completed) Conversion Page -->
    <!-- Google Code for Sign Up Conversion Page -->

    <script type="text/javascript">
        /* <![CDATA[ */
        var google_conversion_id = 1040748494;
        var google_conversion_language = "en";
        var google_conversion_format = "3";
        var google_conversion_color = "ffffff";
        var google_conversion_label = "t8aCCOKp1wEQzp-i8AM";
        var google_conversion_value = 0;
        if (1) {
            google_conversion_value = 1;
        }
        /* ]]> */
    </script>

    <script type="text/javascript" src="https://www.googleadservices.com/pagead/conversion.js">
    </script>

    <noscript>
        <div style="display: inline;">
            <img height="1" width="1" style="border-style: none;" alt="" src="https://www.googleadservices.com/pagead/conversion/1016004997/?value=1&amp;label=ZA68CLOXkAIQhYO85AM&amp;guid=ON&amp;script=0" />
        </div>
    </noscript>
</asp:Content>
