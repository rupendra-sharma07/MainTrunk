<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VideoTributeCreation.aspx.cs"
    Inherits="Tribute_TributeCreation" Title="TributeCreation" ValidateRequest="false"
    EnableEventValidation="false" MasterPageFile="~/Shared/TributeCreation.master" %>

<%@ MasterType VirtualPath="~/Shared/TributeCreation.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="content" ContentPlaceHolderID="TributePlaceHolder" runat="Server">

    <script type="text/javascript" src="<%=Session["APP_SCRIPT_PATH"]%>Common/JavaScript/CreditCardValidation.js"></script>

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
    <div style="text-align:left " >
        <div id="lblErrMsg" style="text-align:left " runat="server" class="yt-Error" visible="false">
        </div>
        <%--<asp:ValidationSummary CssClass="yt-Error" ID="ValidationSummary2" runat="server"
            Width="647px" HeaderText=" <h2>Oops - there was a problem with your tribute details.</h2>                                                             <h3>Please correct the errors below:</h3>"
            ForeColor="Black" ValidationGroup="vgNextStep" />
        <asp:ValidationSummary CssClass="yt-Error" ID="ValidationSummary1" runat="server"
            Width="647px" HeaderText=" <h2>Oops - there was a problem with your tribute details.</h2>                                                             <h3>Please correct the errors below:</h3>"
            ForeColor="Black" ValidationGroup="vgCheckAvailability" />--%>
        <asp:ValidationSummary CssClass="yt-Error" ID="PortalValidationSummary" runat="server"
            Width="647px" HeaderText=" <h2>Oops - there was a problem with your tribute details.</h2>                                                             <h3>Please correct the errors below:</h3>"
            ForeColor="Black" />
    </div>
    <div>
        <asp:MultiView ID="CreateTributeView" runat="server">
            <asp:View ID="Step1" runat="server">
                <div class="yt-Step2">
                    <asp:Panel ID="PanelStep1" runat="server">
                        <div class="yt-TributeProcess">
                            <ul class="yt-ProcessSteps">
                                <li class="yt-Selected"><a tabindex="301"><strong>1:</strong> Enter Tribute Details</a></li>
                                <li><a tabindex="302"><strong>2:</strong> More Tribute Details</a></li>
                                <li><a tabindex="303"><strong>3:</strong> Tribute Management</a></li>
                                <li><a tabindex="304"><strong>4:</strong> Review</a></li>
                                <li><a tabindex="305"><strong>5:</strong> Choose Account Type</a></li>
                                <li><a tabindex="306"><strong>6:</strong> Done!</a></li>
                            </ul>
                            <div class="yt-ProcessStepDisplay">
                                <fieldset class="yt-Form">
                                    <p class="yt-requiredFields">
                                        <strong>Required fields are indicated with <em class="required">* </em></strong>
                                    </p>
                                    <div class="yt-Form-Field yt-Hint-Offset">
                                        <label>
                                            <em class="required">* </em>Who are you creating this tribute for?:</label>
                                        <asp:TextBox ID="txtTributeName" CssClass="yt-Form-Input-XLong" MaxLength="40" runat="server"
                                            CausesValidation="True" TabIndex="1"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvTributeName" Text="!" runat="server" ControlToValidate="txtTributeName"
                                            ErrorMessage="Who is this tribute for is a required field." Font-Bold="True"
                                            Font-Size="Medium" ForeColor="#FF8000" ValidationGroup="vgNextStep"></asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="cvTributeName" runat="server" ErrorMessage="Invalid Tribute Name,* and ? is not allowed,Please try again. "
                                            ClientValidationFunction="ValidateTributeName" ControlToValidate="txtTributeName"
                                            Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ValidationGroup="vgNextStep">!</asp:CustomValidator>
                                            <asp:RegularExpressionValidator ID="revTributeName" runat="server" ErrorMessage="Please provide a valid Tribute Name."
                                            ControlToValidate="txtTributeName" ValidationGroup="vgNextStep" Font-Bold="True"
                                            Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9_,\-\'\&quot;)(.\s,]*$"></asp:RegularExpressionValidator>
                                        <div class="hint">
                                            Enter the name of the person or people who you are creating this tribute for. For
                                            example: "Aunt Jill" or "Jane and Michael Smith".You are limited to 40 characters.<span
                                                class="hintPointer"></span></div>
                                    </div>
                                    <div class="yt-ChannelSelected yt-ShowBox" id="ThemeBox" runat="server">
                                        <fieldset class='yt-ThemeSelection yt-CompactRadioList'>
                                            <legend><em class='required'>* </em>Select the theme you would like to use for this
                                                tribute:</legend>
                                            <div class="yt-Form-Field">
                                                <asp:DropDownList ID="ddlSubCategory" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSubCategory_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </div>
                                            <div style="border: solid 1px #ccc; height: 195px; width: 600px; overflow-y: scroll;
                                                overflow-x: hidden">
                                                <asp:Literal ID="ltrltheme" runat="server"></asp:Literal>
                                            </div>
                                        </fieldset>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <div id="PanelTributeAddress" runat="server" visible="false" class="yt-Form-Field yt-Hint-Offset yt-ThemeAddress"
                                                style="width: 622px">
                                            </div>
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
                                                    OnClick="lbtncheckEmail_Click" Height="15px" TabIndex="8" ValidationGroup="vgCheckAvailability">Check Availability</asp:LinkButton>
                                                <div class="hint">
                                                    Enter email address you would like YT to contact you and notify about your tribute
                                                    visitors contribution. Note, you will be able to change this email later in your
                                                    profile settings.<span class="hintPointer"></span></div>
                                                <span class="availabilityNotice" id="spanAvailableEmail" runat="server"></span>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="yt-Form-Buttons">
                                        <div class="yt-Form-Cancel">
                                            <a href="javascript: void(0);" onclick="cancelTributeCreation('tribute');">Cancel Tribute Creation</a>
                                        </div>
                                        <div class="yt-Form-Submit">
                                            <asp:LinkButton ID="lbtnNextstep" CausesValidation="true" CssClass="yt-Button yt-ArrowButton"
                                                OnClick="lbtnNextstep_Click" runat="server" TabIndex="13" ValidationGroup="vgNextStep">Next step</asp:LinkButton>
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
                                        runat="server" CausesValidation="False"><strong>1:</strong>Enter Tribute Details</asp:LinkButton>
                                </li>
                                <li class="yt-Selected"><a><strong>2:</strong> More Tribute Details</a></li><li><a><strong>
                                    3:</strong> Tribute Management</a></li><li><a><strong>4:</strong> Review</a></li><li>
                                        <a><strong>5:</strong> Choose Account Type</a></li><li><a><strong>6:</strong> Done!</a></li></ul>
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
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Please provide a valid Tribute Year."
                                                ControlToValidate="txtYear" ValidationGroup="TributeDetails" Font-Bold="True"
                                                Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                                            <label>
                                                Year</label>
                                        </div>
                                        <span id="Indecator1" runat="server" style="color: #FF8000; font-size: Medium; font-weight: bold;"
                                            visible="false">!</span>
                                        <%--<asp:CompareValidator ID="cvYear" runat="server" Text="!" ErrorMessage="Date of birth should be less or equal to current date."
                                            ControlToValidate="txtYear" Type="Integer" Operator="LessThanEqual" Font-Bold="True"
                                            Font-Size="Medium" ForeColor="White" Width="1px" Visible="False"></asp:CompareValidator>--%>
                                        <%--<asp:CustomValidator ID="cvDate1" runat="server" ClientValidationFunction="ValidateDate"
                                            Text="!" ErrorMessage="Please enter valid date." Font-Bold="True" Font-Size="Medium"
                                            ForeColor="#FF8000" Width="1px"></asp:CustomValidator>--%>
                                        <%--<asp:CustomValidator ID="cvDate12" runat="server" Visible="false" ClientValidationFunction="ValidateDates"
                                            Text="!" ErrorMessage="Please enter valid date in one of them." Font-Bold="True"
                                            Font-Size="Medium" ForeColor="#FF8000"></asp:CustomValidator>--%>
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
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Text="!" runat="server" ControlToValidate="txtYear2" Font-Bold="True" ErrorMessage="Year can only be a 4 digit number."
                                                Font-Size="Medium" ForeColor="#FF8000" ValidationGroup="TributeDetails"> </asp:RequiredFieldValidator>    
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Please provide a valid Tribute Year."
                                                ControlToValidate="txtYear2" ValidationGroup="TributeDetails" Font-Bold="True"
                                                Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                                            <label>
                                                Year</label>
                                        </div>
                                        <span id="Indecator2" runat="server" style="color: #FF8000; font-size: Medium; font-weight: bold;"
                                            visible="false">!</span>
                                       <%-- <asp:CustomValidator ID="cvNewbaby" runat="server" ClientValidationFunction="ValidateNewBaby"
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
                                            Font-Size="Medium" ForeColor="White" ValueToCompare="2008" Width="1px" Visible="False"></asp:CompareValidator>--%>
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
                                                <asp:Image ID="imgTributePhoto" Height="119px" Width="119px" AlternateText="Tribute photo"
                                                    ImageUrl="<%=tributeImgURL %>" runat="server" />
                                            </div>
                                            <a href="javascript: void(0);" onclick="uploadTributePhoto();" class="yt-MiniButton">
                                                Upload photo</a>
                                            <div class="yt-shownHint">
                                                If you do not have a photo to upload right now, that is okay! A default photo will
                                                be used, and you can upload a personalized photo for this tribute after it has been
                                                created.</div>
                                        </div>
                                    </fieldset>
                                    <div class="yt-Form-Field yt-MessageField">
                                    </div>
                                    <div class="yt-Form-Buttons">
                                        <div class="yt-Form-Cancel">
                                            <a href="javascript: void(0);" onclick="cancelTributeCreation('tribute');">Cancel Tribute Creation</a>
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
                                        runat="server" CausesValidation="False"><strong>1:</strong>Enter Tribute Details</asp:LinkButton>
                                </li>
                                <li class="yt-Visited">
                                    <asp:LinkButton ID="lbtn32MoreTributeDetails" OnClick="lbtn32MoreTributeDetails_Click"
                                        runat="server" CausesValidation="False"><strong>2:</strong>More Tribute Details</asp:LinkButton>
                                </li>
                                <li class="yt-Selected"><a><strong>3:</strong> Tribute Management</a></li><li><a><strong>
                                    4:</strong> Review</a></li><li><a><strong>5:</strong> Choose Account Type</a></li><li>
                                        <a><strong>6:</strong> Done!</a></li></ul>
                            <div class="yt-ProcessStepDisplay">
                                <%-- <p class="yt-requiredFields">
                                    Please enter some more information about the
                                    <!-- TRIBUTE TYPE HERE -->
                                    tribute you are creating for Tribute Name.
                                </p>--%>
                                <p class="yt-requiredFields">
                                    <strong>Required fields are indicated with <em class="required"><em class="required">
                                        * </em></em></strong>
                                    <%--<asp:CustomValidator ID="cvPrivacy" runat="server" Font-Bold="True" Font-Size="Medium"
                                        ForeColor="#FF8000" ClientValidationFunction="ValidateConfirmation" ErrorMessage=" Please select the level of privacy for this tribute.">!</asp:CustomValidator>--%>
                                </p>
                                <fieldset class="yt-Form">
                                    <fieldset class="yt-PrivacyFields">
                                        <legend><em class="required">*</em> Please select the level of privacy for this tribute:</legend>
                                        <asp:CustomValidator ID="cvPrivacy" runat="server" Font-Bold="True" Font-Size="Medium" Display="Dynamic"
                                        ForeColor="#FF8000" ClientValidationFunction="ValidateConfirmation" ErrorMessage=" Please select the level of privacy for this tribute.">!</asp:CustomValidator>
                                        <div class="yt-Form-Field yt-Form-Field-Radio">
                                            <asp:RadioButton ID="rdoPublic" Text="Public" GroupName="rdoPrivacy" runat="server"
                                                TabIndex="1" />
                                            <p>
                                                Public tributes can be found by anyone who is searching for a tribute. This type
                                                of tribute may also appear on Your Tribute in the list of "Most Recently Created"
                                                and "Most Viewed" tributes.</p>
                                        </div>
                                        <div class="yt-Form-Field yt-Form-Field-Radio">
                                            <asp:RadioButton ID="rdoPrivate" Text="Private" GroupName="rdoPrivacy" runat="server"
                                                TabIndex="2" />
                                            <p>
                                                Private tributes will not show up in search results, and will not be featured anywhere
                                                on Your Tribute. These types of tributes will still be accessible to all users (that
                                                is, they will not be password protected).
                                            </p>
                                        </div>
                                    </fieldset>
                                    <fieldset class="yt-AdminAddFields" id="divOrderDvdStep3" runat="server">
                                        <legend><em class="required">*</em> <b>Select Video Tribute revenue generating options:</b></legend>
                                        <asp:CheckBox runat="server" ID="chkOrderDVD" AutoPostBack="false" Font-Bold="true"
                                            Text="Order DVD Box" />
                                        <div class="yt-Form-Field yt-Form-Field-Radio">
                                            With this on, a box will be displayed on the Video Tribute with a link to contact
                                            you to order a copy of the video on DVD. We highly recommend leaving this on.
                                        </div>
                                        <asp:CheckBox runat="server" ID="chkMemTributeBox" AutoPostBack="false" Font-Bold="true"
                                            Text="Memorial Tribute Box" />
                                        <div class="yt-Form-Field yt-Form-Field-Radio">
                                            With this on, a box will be displayed on the Video Tribute with a link to create
                                            a Memorial Tribute. You will earn 1 credit if the person signs up for a paid account.
                                            This is a great way to earn extra revenue!
                                        </div>
                                    </fieldset>
                                    <div class="yt-Form-Buttons">
                                        <div class="yt-Form-Cancel">
                                            <a href="javascript: void(0);" onclick="cancelTributeCreation('tribute');">Cancel Tribute Creation</a>
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
                                            runat="server" CausesValidation="False"><strong>1:</strong>Enter Tribute Details</asp:LinkButton>
                                    </li>
                                    <li class="yt-Visited">
                                        <asp:LinkButton ID="lbtn42MoreTributeDetails" OnClick="lbtn42MoreTributeDetails_Click"
                                            runat="server" CausesValidation="False"><strong>2:</strong>More Tribute Details</asp:LinkButton>
                                    </li>
                                    <li class="yt-Visited">
                                        <asp:LinkButton ID="lbtn43TributeManagement" OnClick="lbtn43TributeManagement_Click"
                                            runat="server" CausesValidation="False"><strong>3:</strong>Tribute Management</asp:LinkButton>
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
                                            Tribute Details
                                        </h2>
                                        <h4>
                                            You are creating this tribute for:</h4>
                                        <p>
                                            <asp:Label ID="txttributefor" runat="server" Text=""></asp:Label>
                                        </p>
                                        <div>
                                            <asp:Panel ID="PanelSavedTheme" runat="server" Visible="True" Width="598px">
                                                <h4>
                                                    The type of tribute that you have chosen to create:</h4>
                                                <p>
                                                    <asp:Label ID="lblSelectedTributeType" runat="server" Text="New Baby"></asp:Label>
                                                </p>
                                                <h4>
                                                    The theme you have chosen for this tribute:</h4>
                                                <asp:Literal ID="step4Litrel" runat="server"></asp:Literal>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                    <br />
                                    <br />
                                    <div class="yt-Panel-Primary" id="yt-TributeConfirmMoreDetails">
                                        <h2>
                                            More Tribute Details
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
                                            You have chosen to use this as the profile photograph for the tribute:</h4>
                                        <div class="yt-TributePhoto">
                                            <img id="tributephoto_" runat="server" src="" width="119" height="119" alt="" /></div>
                                        <p id="lblEditMeaasge" runat="server">
                                        </p>
                                    </div>
                                    <br />
                                    <br />
                                    <div class="yt-Panel-Primary" id="yt-TributeConfirmManagement">
                                        <h2>
                                            Tribute management
                                        </h2>
                                        <asp:LinkButton ID="lbtneditstep3" CssClass="yt-MiniButton yt-EditButton" runat="server"
                                            OnClick="lbtneditstep3_Click">Edit</asp:LinkButton>
                                        <asp:Panel ID="PanelSaveTributeManagement" Visible="true" runat="server" Width="596px"
                                            Style="overflow: hidden;">
                                            <h4>
                                                The level of privacy you have chosen for this tribute is:</h4>
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
                                                        <b>You have chosen to add a Donation Box to your tribute homepage.</b></p>
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
                                                <a href="javascript: void(0);" onclick="cancelTributeCreation('tribute');">Cancel Tribute Creation</a>
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
                                            CausesValidation="False"><strong>1:</strong>Enter Tribute Details</asp:LinkButton>
                                    </li>
                                    <li class="yt-Visited">
                                        <asp:LinkButton ID="lbtn52MoreTributeDetails" runat="server" OnClick="lbtn52MoreTributeDetails_Click"
                                            CausesValidation="False"><strong>2:</strong>More Tribute Details</asp:LinkButton>
                                    </li>
                                    <li class="yt-Visited">
                                        <asp:LinkButton ID="lbtn53TributeManagement" runat="server" OnClick="lbtn53TributeManagement_Click"
                                            CausesValidation="False"><strong>3:</strong>Tribute Management</asp:LinkButton>
                                    </li>
                                    <li class="yt-Visited">
                                        <asp:LinkButton ID="lbtn54Review" runat="server" OnClick="lbtn54Review_Click" CausesValidation="False"><strong>4:</strong>Review</asp:LinkButton>
                                    </li>
                                    <li class="yt-Selected"><a><strong>5:</strong> Choose Account Type</a></li><li><a><strong>
                                        6:</strong> Done!</a></li></ul>
                                <div class="yt-ProcessStepDisplay">
                                    <fieldset class="yt-Form">
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <table border="0" cellspacing="0" cellpadding="0" class="yt-overlapHead yt-AccountTypeTable"
                                                    id="yt-AccountTypeSelection">
                                                    <thead>
                                                        <tr>
                                                            <th class="yt-colFirst">
                                                                choose your account type
                                                                <asp:CustomValidator ID="CustomValidator1" runat="server" ForeColor="Transparent"
                                                                    ClientValidationFunction="SelectVideoAccountType" ErrorMessage=" Please select the account type for this tribute.">.</asp:CustomValidator>
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
                                                            <th class="yt-colLast">
                                                                No Renewal Required
                                                            </th>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                    <tr class="yt-rowFirst">
                                                            <th>
                                                                <div class="yt-Form-Field-Radio textCap">
                                                                    <asp:RadioButton ID="rdoMembershipThirty" GroupName="rdoMembershipType" Text="Video Tribute (30 Days)"
                                                                        runat="server" AutoPostBack="True" OnCheckedChanged="rdoMembershipThirty_CheckedChanged" />
                                                                </div>
                                                            </th>
                                                            <td class="yt-Cost">
                                                                Free
                                                            </td>
                                                            <td>
                                                                <img src="<%=Session["APP_SCRIPT_PATH"]%>assets/images/icon_check.gif" alt="Yes"
                                                                    width="21" height="21" />
                                                            </td>
                                                            <td>
                                                                <img src="<%=Session["APP_SCRIPT_PATH"]%>assets/images/icon_x.gif" alt="No"
                                                                    width="21" height="21" />
                                                            </td>
                                                            <td class="yt-colLast">
                                                                <img src="<%=Session["APP_SCRIPT_PATH"]%>assets/images/icon_x.gif" alt="No"
                                                                    width="21" height="21" />
                                                            </td>
                                                        </tr><tr>
                                                            <th>
                                                                <div class="yt-Form-Field-Radio textCap">
                                                                    <asp:RadioButton ID="rdoMembershipNinety" GroupName="rdoMembershipType" Text="Video Tribute (90 Days)"
                                                                        runat="server" AutoPostBack="True" OnCheckedChanged="rdoMembershipNinety_CheckedChanged" />
                                                                </div>
                                                            </th>
                                                            <td class="yt-Cost">
                                                                0.5 Credit
                                                            </td>
                                                            <td>
                                                                <img src="<%=Session["APP_SCRIPT_PATH"]%>assets/images/icon_check.gif" alt="Yes"
                                                                    width="21" height="21" />
                                                            </td>
                                                            <td>
                                                                <img src="<%=Session["APP_SCRIPT_PATH"]%>assets/images/icon_check.gif" alt="Yes"
                                                                    width="21" height="21" />
                                                            </td>
                                                            <td class="yt-colLast">
                                                                <img src="<%=Session["APP_SCRIPT_PATH"]%>assets/images/icon_x.gif" alt="No"
                                                                    width="21" height="21" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                <div class="yt-Form-Field-Radio textCap">
                                                                    <asp:RadioButton ID="rdoMembershipYearly" GroupName="rdoMembershipType" Text="Video Tribute (1 Year)"
                                                                        runat="server" AutoPostBack="True" OnCheckedChanged="rdoMembershipYearly_CheckedChanged" />
                                                                </div>
                                                            </th>
                                                            <td class="yt-Cost">
                                                                1 Credit
                                                            </td>
                                                            <td>
                                                                <img src="<%=Session["APP_SCRIPT_PATH"]%>assets/images/icon_check.gif" alt="Yes"
                                                                    width="21" height="21" />
                                                            </td>
                                                            <td>
                                                                <img src="<%=Session["APP_SCRIPT_PATH"]%>assets/images/icon_check.gif" alt="Yes"
                                                                    width="21" height="21" />
                                                            </td>
                                                            <td class="yt-colLast">
                                                                <img src="<%=Session["APP_SCRIPT_PATH"]%>assets/images/icon_x.gif" alt="No" width="21"
                                                                    height="21" />
                                                            </td >
                                                        </tr>
                                                        <tr>
                                                            <th>
                                                                <div class="yt-Form-Field-Radio textCap">
                                                                    <asp:RadioButton ID="rdoMembershipLifetime" GroupName="rdoMembershipType" Text="Video Tribute (Life)"
                                                                        runat="server" AutoPostBack="True" OnCheckedChanged="rdoMembershipLifetime_CheckedChanged" />
                                                                </div>
                                                            </th>
                                                            <td class="yt-Cost">
                                                                4 Credits
                                                            </td>
                                                            <td>
                                                                <img src="<%=Session["APP_SCRIPT_PATH"]%>assets/images/icon_check.gif" alt="Yes"
                                                                    width="21" height="21" />
                                                            </td>
                                                            <td>
                                                                <img src="<%=Session["APP_SCRIPT_PATH"]%>assets/images/icon_check.gif" alt="Yes"
                                                                    width="21" height="21" />
                                                            </td>
                                                            <td class="yt-colLast">
                                                                <img src="<%=Session["APP_SCRIPT_PATH"]%>assets/images/icon_check.gif" alt="Yes"
                                                                    width="21" height="21" />
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                                <h2 style="text-align: right;">
                                                    Total Credits:
                                                    <asp:Label ID="lblTotalCredit" runat="server"></asp:Label>
                                                </h2>
                                                <!-- DIV to hide content until membership type is selected -->
                                                <div class="RemainingCreditPoint" id="creditMsg" runat="server">
                                                    <span id="NetCreditCount" runat="server"></span>
                                                </div>
                                                <div id="divEmptyHeight" runat="server" style="height:25px;"/>
                                                <div>
                                                    <asp:Panel ID="PanelFreeTrial" runat="server" Visible="false" Width="624px">
                                                        <p class="yt-InfoBox">
                                                            You have chosen the free account - you can upgrade at any time within the "My Account"
                                                            area.
                                                        </p>
                                                    </asp:Panel>
                                                </div>
                                                <div>
                                                    <asp:Panel ID="PanelBillingInfo" runat="server" Visible="false" Width="624px">
                                                        <b>Pay-as-you-go credits</b>
                                                        <br />
                                                        Buy as you go credits whenever you need them. Use your credits to purchase tributes.
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
                                                        <p>
                                                            Please enter the following payment information:</p>
                                                        <p class="yt-requiredFields">
                                                            <strong>Required fields are indicated with <em class="required">* </em></strong>
                                                        </p>
                                                        <asp:UpdatePanel ID="PnlCoupon" runat="server">
                                                            <ContentTemplate>
                                                                <div id="PnlPaymentDetails" runat="server" visible="false">
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
                                                                                            <div id="digicertsitesealcode" style="width: 81px; margin: 5px auto 5px 5px; text-align:center ">

                                                                                                <script language="javascript" type="text/javascript" src="https://www.digicert.com/custsupport/sealtable.php?order_id=00192234&amp;seal_type=a&amp;seal_size=large&amp;seal_color=blue&amp;new=1"></script>

                                                                                                <a href="http://www.digicert.com">SSL Certificates</a>

                                                                                                <script language="javascript" type="text/javascript">                                                                                   coderz();</script>

                                                                                            </div>
                                                                                            <!-- END DigiCert Site Seal Code -->
                                                                                            <%--  <!-- BEGIN DigiCert Site Seal Code -->
                                                                                    <div id="digicertsitesealcode" style="width: 81px; margin: 5px 5px 5px auto;" align="center">

                                                                                        <script language="javascript" type="text/javascript" src="https://www.digicert.com/custsupport/sealtable.php?order_id=10000153&amp;seal_type=a&amp;seal_size=large&amp;seal_color=green&amp;new=1"></script>

                                                                                        <a href="http://www.digicert.com/ssl.htm">SSL Certificate</a>

                                                                                        <script language="javascript" type="text/javascript">coderz();</script>

                                                                                    </div>
                                                                                    <!-- END DigiCert Site Seal Code -->--%>
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
                                                                                Width="1px"></asp:CustomValidator><asp:CompareValidator ID="cpvtxtCCYear" Font-Bold="true"
                                                                                    Operator="GreaterThanEqual" Font-Size="Medium" ForeColor="#FF8000" runat="server"
                                                                                    ErrorMessage="Expiry Date cannot be less than current date." Visible="false"
                                                                                    ControlToValidate="txtCCYear" Width="1px">!</asp:CompareValidator><asp:CustomValidator
                                                                                        ID="cvCCYear" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" runat="server"
                                                                                        ControlToValidate="txtCCYear" Text="!" ErrorMessage="Please enter a valid expiry date."
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
                                                                            ForeColor="#FF8000" ErrorMessage="Enter valid phone number.Format should be XXX-XXX-XXXX."
                                                                            ClientValidationFunction="ValidatePhoneNumber">!</asp:CustomValidator>
                                                                        <%-- <asp:CustomValidator ID="cvAcceptPolicies" Text="!" ForeColor="Transparent" ErrorMessage="Please accept that you have read and agree to the terms of use, cancellation/refund policy and the privacy policy."
                                                                    runat="server" ClientValidationFunction="ValidateTandCs" Width="1px"></asp:CustomValidator>--%>
                                                                    </div>
                                                                    <div class="yt-Form-Field">
                                                                        <label>
                                                                            <em class="required">* </em>Email Address:</label>
                                                                        <asp:TextBox ID="txtEmailAddress" CssClass="yt-Form-Input-Long" runat="server" MaxLength="100"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="rfvEmailAddress" runat="server" Text="!" ControlToValidate="txtEmailAddress"
                                                                            ErrorMessage="Email address is a required field." Font-Bold="True" Font-Size="Medium"
                                                                            ForeColor="#FF8000">
                                                                        </asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <fieldset class="yt-SaveBillingInfo">
                                                                        <div class="yt-Form-Field yt-Form-Field-Checkbox">
                                                                            <asp:CheckBox ID="chkSaveBillingInfo" TabIndex="18" Text="I would like to save the above billing information in my profile."
                                                                                runat="server" />
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
                                                                <asp:CheckBox ID="chkAgree" TabIndex="19" runat="server" Text="<em class='required'>*</em> I have read and agree to the <a href='termsofuse.aspx' target='_blank' >terms of use</a>, the cancellation/refund
                                                                    policy (outlined in the terms of use) and the <a href='privacy.aspx'  target='_blank' >privacy policy</a>." Checked="False" />
                                                       
                                                                <asp:CustomValidator ID="CustomValidator4" Text="!" ForeColor="Transparent" ErrorMessage="Please accept that you have read and agree to the terms of use, cancellation/refund policy and the privacy policy."
                                                                    runat="server" ClientValidationFunction="ValidateTandCs" Width="1px"></asp:CustomValidator>
                                                            </div>
                                                        </fieldset>
                                                        <p>
                                                            If you have reviewed all of the above information and it is correct, you must be
                                                            ready to...</p>
                                                    </asp:Panel>
                                                </div>
                                                <!-- end hidden BillingInfo DIV -->
                                                <%--   </ContentTemplate>
                                        </asp:UpdatePanel>--%>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <div class="yt-Form-Buttons">
                                            <div class="yt-Form-Cancel">
                                                <a href="javascript: void(0);" tabindex="20" onclick="cancelTributeCreation('tribute');">Cancel
                                                    Tribute Creation</a>
                                            </div>
                                            <div class="yt-Form-Submit">
                                                <div id="divCreate1" style="float: right; display: block; visibility: visible">
                                                    <asp:LinkButton ID="lbtnCreatetribute" TabIndex="21" runat="server" CssClass="yt-Button yt-ArrowButton"
                                                        OnClick="lbtnCreatetribute_Click" OnClientClick="return OnPayClick();">Create the tribute!</asp:LinkButton>
                                                        <asp:Label ID="lblProcess" runat="server"></asp:Label>
                                                </div>
                                                <div id="divCreate2" style="float: right; display: none; visibility: hidden">
                                                    <a id="lnkPostbackDisabled" href="#" tabindex="21" class="yt-Button yt-ArrowButton">
                                                        Create the tribute!</a>
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
                    </asp:Panel>
                </div>
            </asp:View>
            <asp:View ID="Step6" runat="server">
                <div>
                    <asp:Panel ID="PanelStep6" Visible="false" runat="server" Width="663px">
                        <div class="yt-Step6">
                            <div class="yt-ContentPrimary">
                                <div class="yt-TributeProcess">
                                    <ul class="yt-ProcessSteps">
                                        <li><a><strong>1:</strong> Enter Tribute Details</a></li>
                                        <li><a><strong>2:</strong> More Tribute Details</a></li>
                                        <li><a><strong>3:</strong> Tribute Management</a></li>
                                        <li><a><strong>4:</strong> Review</a></li>
                                        <li><a><strong>5:</strong> Choose Account Type</a></li>
                                        <li class="yt-Selected"><a><strong>6:</strong> Done!</a></li>
                                    </ul>
                                    <div class="yt-ProcessStepDisplay">
                                        <h3>
                                            Congratulations, the <label id="step6Msg" runat="server"/> Tribute has been created!
                                        </h3>
                                        <br />
                                        <div class="RemainingCreditPoint">
                                            <span id="NetCreditPointStep6" runat="server"></span>
                                        </div>
                                        <br />
                                        <br />
                                        <h3>
                                            Video Tribute Links:</h3>
                                        Use one of the following links to add the Video Tribute to your website or email
                                        to the family.
                                        <br />
                                        <br />
                                        Direct Link:
                                        <br />
                                        <asp:TextBox ID="txtDirectLink" Width="500px" Height="20px" runat="server"></asp:TextBox>
                                        <br />
                                        <br />
                                        Website Link:
                                        <br />
                                        <asp:TextBox ID="txtWebsiteLink" Width="500px" TextMode="MultiLine" Height="45px"
                                            Style="overflow: hidden;" runat="server"></asp:TextBox>
                                        <br />
                                        <br />
                                        <br />
                                        <br />
                                        <div class="yt-Form-Buttons">
                                            <div class="yt-Form-Submit">
                                                <asp:LinkButton ID="lbtnStartaddingcontent" CssClass="yt-Button yt-ArrowButton" runat="server"
                                                    OnClick="lbtnStartaddingcontent_Click">View the Video Tribute</asp:LinkButton>
                                            </div>
                                        </div>
                                        <!--yt-ProcessStepDisplay-->
                                        <!--yt-TributeProcess-->
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </asp:View>
        </asp:MultiView>
    </div>

    <script language="javascript" type="text/javascript">

        var isValid = true;
       

        function ValidateTributeName(source, args) {
            var txtTributeName = document.getElementById('<%=txtTributeName.ClientID%>');
            args.IsValid = TributeNameValidate(txtTributeName.value);
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
            var rdb4 = $('rdoCCMasterCard');
                
            args.IsValid = PaymentMethodvalidate(rdb1, rdb4);

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

            if (rdb1.checked ) {

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

            if (rdb1.checked ) {
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

       

        function ValidateConfirmation(source, args) {
            var rdoPrivate = document.getElementById('<%= rdoPrivate.ClientID %>');
            var rdoPublic = document.getElementById('<%= rdoPublic.ClientID %>');
            args.IsValid = ValidatePrivacy(rdoPrivate, rdoPublic)
        }


        //Validate the Donation Email Address
     


        

        function SetUniqueRadioButton(nameregex, current) {
            UniqueRadioButton(nameregex, current);
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
    </script>

</asp:Content>
