<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Story.aspx.cs" Inherits="Story_Story"
    Title="Story" EnableEventValidation="false" MasterPageFile="~/Shared/Story.master"
    ValidateRequest="false" %>

<%@ Register Assembly="CuteEditor" Namespace="CuteEditor" TagPrefix="CE" %>

<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %>
<%@ MasterType VirtualPath="~/Shared/Story.master" %>
<asp:Content ID="content1" ContentPlaceHolderID="TabContentPlaceHolder" runat="Server">
    <script language="javascript" type="text/javascript">        
    
        function rewrite_onclick_if_needed() {
            if ($('facebook_share_container') && !$('facebook_share_container').getProperty("parsed")) {
                $('facebook_share_container').setProperty("parsed", 'true');
                $('facebook_share_container').setStyle('display', 'inline');
                replace_submit_with_stream('<%=lbtnSaveStory.ClientID%>', 'facebook_share', get_msg, get_attachments, get_action_link);
            }
        };

        $(document).addEvent('fb_connected', function() {
            setInterval(rewrite_onclick_if_needed, 1000);
        });
        
        function get_msg() {
            return "";
        };
        function get_attachments() {
            var ret = {
            name: '<asp:Literal id="storyWallPostSubject" Text="false" runat="server" />', //Matt gave a gift on the: Jon Stiles & Mary Smith Wedding Tribute
            href: '<asp:Literal id="storyWallLink" Text="false" runat="server" />', //story page link
            caption: '<b>Website:</b> <asp:Literal id="storyWallTributeHome" Text="false" runat="server" />',
                description: "<b>Story:</b> " + $('<%=txtStoryDetail.ClientID%>').value, // link to particular tribute
                media: [{
                    type: "image",
                    src: document.getElementById('<%=imgTributeImageView.ClientID%>').src, //tribute photo
                    href: '<asp:Literal id="storyWallLink1" Text="false" runat="server" />' //story page link
                }]
            };
            //console.log(ret);
            return ret;
        };
        function get_action_link() {
            var ret = [{ text: "Visit <%= _TributeType %> Tribute", href: '<asp:Literal id="storyWallTributeHome1" Text="false" runat="server" />'}]; //vist tribute_type tribute (link to tribute_type homepage)
            //console.log(ret)
            return ret;
        };
            
    function Date1Require(source, args)
    {
        var day = Number(document.getElementById('<%= ddlDate1Day.ClientID %>').value);
        var month = Number(document.getElementById('<%= ddlDate1Month.ClientID %>').value);
        var year = document.getElementById('<%= txtDate1Year.ClientID %>').value;

        args.IsValid = StoryDate1Require(day, month, year, '<%=_TributeType%>', '<%=_NewBaby%>', '<%=_Birhday%>', '<%=_Memorial%>');
       
    }
    
    function Date2Require(source, args)
    {
        var day = Number(document.getElementById('<%= ddlDate2Day.ClientID %>').value);
        var month = Number(document.getElementById('<%= ddlDate2Month.ClientID %>').value);
        var year = document.getElementById('<%= txtDate2Year.ClientID %>').value;
        
        args.IsValid = StoryDate2Require(day, month, year, '<%=_TributeType%>', '<%=_Memorial%>');
    }
    
    function checkNewBaby(source, args)
    {        
        var day1 = document.getElementById('<%= ddlDate1Day.ClientID %>').value;
        var month1 = document.getElementById('<%= ddlDate1Month.ClientID %>').value;
        var year1 = document.getElementById('<%= txtDate1Year.ClientID %>').value;
        
        var day2 = document.getElementById('<%= ddlDate2Day.ClientID %>').value;
        var month2 = document.getElementById('<%= ddlDate2Month.ClientID %>').value;
        var year2 = document.getElementById('<%= txtDate2Year.ClientID %>').value;
        
        if ((StoryDate1Require(day1, month1, year1, '<%=_TributeType%>', '<%=_NewBaby%>', '<%=_Birhday%>', '<%=_Memorial%>') == false) ||
            (StoryDate2Require(day2, month2, year2, '<%=_TributeType%>', '<%=_Memorial%>') == false))
        {
            args.IsValid = true;
        }
        else
        {
            var status = StoryCheckNewBaby(day1, month1, year1, day2, month2, year2,'<%=_TributeType%>', '<%=_NewBaby%>');
            var validator = document.getElementById('<%= valCheckNewBaby.ClientID %>');
            
            if (status == 0)
            {
                validator.errormessage = "Date of Birth or Due Date is required";
                args.IsValid = false;
            }
            else if (status == 2)
            {
                
                validator.errormessage = "Please enter only a date of birth or a due date.";
                args.IsValid = false;
            }
            else if (status == 1)
            {
                args.IsValid = true;
            }
            
        }
    }
    
    function checkDate1(source, args)
    {   
        var day1 = Number(document.getElementById('<%= ddlDate1Day.ClientID %>').value);
        var month1 = Number(document.getElementById('<%= ddlDate1Month.ClientID %>').value);
        var year1 = document.getElementById('<%= txtDate1Year.ClientID %>').value;
        
        var day2 = document.getElementById('<%= ddlDate2Day.ClientID %>');
        var month2 = document.getElementById('<%= ddlDate2Month.ClientID %>');
        var year2 = document.getElementById('<%= txtDate2Year.ClientID %>');

        if (StoryDate1Require(day1, month1, year1, '<%=_TributeType%>', '<%=_NewBaby%>', '<%=_Birhday%>', '<%=_Memorial%>') == false)
        {
            args.IsValid = true;
        }
        else
        {
            args.IsValid = StoryCheckDate1(day1, month1, year1, day2, month2, year2, '<%=_TributeType%>', '<%=_Memorial%>', '<%=_NewBaby%>', '<%=_Birhday%>');
        }
    }

    function checkDate2(source, args)
    {
        var day1 = Number(document.getElementById('<%= ddlDate1Day.ClientID %>').value);
        var month1 = Number(document.getElementById('<%= ddlDate1Month.ClientID %>').value);
        var year1 = document.getElementById('<%= txtDate1Year.ClientID %>').value;
        
        var day2 = Number(document.getElementById('<%= ddlDate2Day.ClientID %>').value);
        var month2 = Number(document.getElementById('<%= ddlDate2Month.ClientID %>').value);
        var year2 = document.getElementById('<%= txtDate2Year.ClientID %>').value;
        
        if (StoryDate2Require(day2, month2, year2, '<%=_TributeType%>', '<%=_Memorial%>') == false)
        {
            args.IsValid = true;
        }
        else
        {
            args.IsValid = StoryCheckDate2(day1, month1, year1, day2, month2, year2, '<%=_TributeType%>', '<%=_NewBaby%>');
        }
    }
    
    function checkFutureDate1(source, args)
    {
        var check = false;
        
        var day1 = Number(document.getElementById('<%= ddlDate1Day.ClientID %>').value);
        var month1 = Number(document.getElementById('<%= ddlDate1Month.ClientID %>').value);
        var year1 = document.getElementById('<%= txtDate1Year.ClientID %>').value;
        
        if (('<%=_TributeType%>' == '<%=_Memorial%>')|| ('<%=_TributeType%>' == '<%=_NewBaby%>'))
        {
            if ('<%=_TributeType%>' == '<%=_NewBaby%>')
            {
                var day2 = Number(document.getElementById('<%= ddlDate2Day.ClientID %>').value);
                var month2 = Number(document.getElementById('<%= ddlDate2Month.ClientID %>').value);
                var year2 = document.getElementById('<%= txtDate2Year.ClientID %>').value;
                
                var status = StoryCheckNewBaby(day1, month1, year1, day2, month2, year2,'<%=_TributeType%>', '<%=_NewBaby%>');
                if ((status == 0) || (status == 2))
                {
                    check = false;
                }
                else if (status == 1)
                {
                    check = true;
                }
            }
            else
            {
                check = true;   
            }
            if(check == true)
            {
                var today = new Date( '<%=todayYear%>', '<%=todayMonth%>' - 1, '<%=todayDay%>' );
                args.IsValid = StoryCheckFutureDate( day1, month1, year1, today );
            }
            else
            {
                args.IsValid = true;
            }
        }
        else
        {
            args.IsValid = true;
        }
    }
    
    function checkFutureDate2(source, args)
    {
        if ('<%=_TributeType%>' == '<%=_Memorial%>')
        {
            var today = new Date( '<%=todayYear%>', '<%=todayMonth%>' - 1, '<%=todayDay%>' );
        
            args.IsValid = StoryCheckFutureDate( Number(document.getElementById('<%= ddlDate2Day.ClientID %>').value),
                                                 Number(document.getElementById('<%= ddlDate2Month.ClientID %>').value),
                                                 document.getElementById('<%= txtDate2Year.ClientID %>').value, today)
        }
        else
        {
            args.IsValid = true
        }
    }
    
    function checkDueDate(source, args)
    {
        var check = false;
        
        if ('<%=_TributeType%>' == '<%=_NewBaby%>')
        {
            var day1 = Number(document.getElementById('<%= ddlDate1Day.ClientID %>').value);
            var month1 = Number(document.getElementById('<%= ddlDate1Month.ClientID %>').value);
            var year1 = document.getElementById('<%= txtDate1Year.ClientID %>').value;
            
            var day2 = Number(document.getElementById('<%= ddlDate2Day.ClientID %>').value);
            var month2 = Number(document.getElementById('<%= ddlDate2Month.ClientID %>').value);
            var year2 = document.getElementById('<%= txtDate2Year.ClientID %>').value;
        
            var status = StoryCheckNewBaby(day1, month1, year1, day2, month2, year2,'<%=_TributeType%>', '<%=_NewBaby%>');

            if ((status == 0) || (status == 2))
            {
                check = false;
            }
            else if (status == 1)
            {
                check = true;
            }
                
            if(check == true)
            {
                var today = new Date( '<%=todayYear%>', '<%=todayMonth%>' - 1, '<%=todayDay%>' );
        
                args.IsValid = StoryCheckDueDate(day2, month2, year2, today);
            }
            else
            {
                args.IsValid = true
            }
       }
       else
       {
           args.IsValid = true;
       }
    }
    
    function DateCompare(source, args)
    {
        if ('<%=_TributeType%>' == '<%=_Memorial%>')
        {
            var day1 = Number(document.getElementById('<%= ddlDate1Day.ClientID %>').value);
            var month1 = Number(document.getElementById('<%= ddlDate1Month.ClientID %>').value);
            var year1 = document.getElementById('<%= txtDate1Year.ClientID %>').value;
            
            var day2 = Number(document.getElementById('<%= ddlDate2Day.ClientID %>').value);
            var month2 = Number(document.getElementById('<%= ddlDate2Month.ClientID %>').value);
            var year2 = document.getElementById('<%= txtDate2Year.ClientID %>').value;
            
            var today = new Date( '<%=todayYear%>', '<%=todayMonth%>' - 1, '<%=todayDay%>' );
        
            args.IsValid = StoryCompareDates(day1, month1, year1, day2, month2, year2, today);
        }
    }
    
    function CheckStoryLength()
    { 
	     var textarea =  document.getElementById('<%=txtStoryDetail.ClientID%>'); 
	     ValidateStoryLength(textarea, 5000);
    }
    
    function CheckNewTopicLength()
    { 
	     var textarea =  document.getElementById('<%=txtTopicAnswer.ClientID%>'); 
	     ValidateStoryLength(textarea, 5000);
    }
    
    function CheckTopicLength()
    {
        ValidateTopicLength('<%=_RowCount%>');
    }
    
    function ClearContent() 
    {       
        if( document.getElementById('<%= txtStoryDetail.ClientID %>').value == "Click Here to write the story..." )
        {
            document.getElementById('<%= txtStoryDetail.ClientID %>').value = "";
        }
    }
    
    function SelectTopicAdd(source, args)
    {
        if(document.getElementById('<%= ddlTopicList.ClientID %>').value == "Select a Topic:")
        {
            args.IsValid = false;
        }
        else
        {
            args.IsValid = true;
        }
    }
    
    function SelectTopicAddNew(source, args)
    {
        args.IsValid = ValidateTopic('<%=_RowCount%>');
    }
    
    function AddNewTopic(source, args)
    {
        if(document.getElementById('<%= ddlTopicList.ClientID %>').value == "Other Topic")
        {
            if(document.getElementById('<%= txtOtherTopic.ClientID %>').value == "")
                args.IsValid = false;
            else
                args.IsValid = true;
        }
    }
    
    function calculateAge() 
    {           
        if ('<%=_TributeType%>' == '<%=_Memorial%>')
        {
            year1 = document.getElementById('<%= txtDate1Year.ClientID %>').value;
            month1 = Number(document.getElementById('<%= ddlDate1Month.ClientID %>').value);
            day1 = Number(document.getElementById('<%= ddlDate1Day.ClientID %>').value);
            
            year2 = document.getElementById('<%= txtDate2Year.ClientID %>').value;
            month2 = Number(document.getElementById('<%= ddlDate2Month.ClientID %>').value);
            day2 = Number(document.getElementById('<%= ddlDate2Day.ClientID %>').value); 
            
            var age = document.getElementById('<%= lblAgeEdit.ClientID %>');
            
            StoryCalculateAge(day1, month1, year1, day2, month2, year2, age);
	   }	   
	}
	
	function ValidateTributeName(source, args)
    {
        var txtTributeName =  document.getElementById('<%=txtName.ClientID%>'); 
        
        args.IsValid=TributeNameValidate(txtTributeName.value);    
    }
    
    function RequireTributeName(source, args)
    {
        var txtTributeName =  document.getElementById('<%=txtName.ClientID%>').value; 

        if(txtTributeName == "")
        {
            args.IsValid = false;
        }
        else
        {
            args.IsValid = true;
        }
    }
    
    function RequireStoryDetail(source, args)
    {
        var txtTributeName =  document.getElementById('<%=txtStoryDetail.ClientID%>').value; 

        if(txtTributeName == "")
        {
            args.IsValid = false;
        }
        else
        {
            args.IsValid = true;
        }
    }
    
    function RequireTopicAnswer(source, args)
    {
        var txtTributeName =  document.getElementById('<%=txtTopicAnswer.ClientID%>').value; 

        if(txtTributeName == "")
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
        document.getElementById('<%=imgStoryImage.ClientID%>').src = url;
        document.getElementById('<%=hdnStoryImageURL.ClientID%>').value = url;
    }
    /* Made by Ashu(15 june,2011) For cuteeditor Safari issue */
    function OpenCuteeditor()
    {
   var Panelid= document.getElementById('<% =panObituaryEdit.ClientID%>');
   var EditpersonalDetails=document.getElementById('<% =lbtnEditPersonalDetail.ClientID%>');
   var EditStory=document.getElementById('<% =lbtnEditStory.ClientID%>');
   var AddMoreAbout=document.getElementById('<% =lbtnAddMoreAbout.ClientID%>');
   var panelView=document.getElementById('<% =panObituaryView.ClientID%>');
   if(Panelid != null && EditpersonalDetails != null && panelView != null && EditStory != null && AddMoreAbout != null)
   {
   Panelid.style.display='block';
   panelView.style.display='none';
   EditpersonalDetails.style.display='none';
   EditStory.style.display='none';
   AddMoreAbout.style.display='none';
   }
    
    }
    
    </script>

    <!--yt-NavPrimary-->
    <div class="yt-Breadcrumbs">
        <a id="aTributeHome" runat="server"><%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString()%> Home</a> <span class="selected">Story</span>
    </div>
</asp:Content>
<asp:Content ID="content" ContentPlaceHolderID="ModuleContentPlaceHolder" runat="Server">
    <asp:UpdatePanel ID="UpdatePanelLocation" UpdateMode="Always" runat="server">
        <ContentTemplate>
            <div id="divShowModalPopup">
            </div>
            <div class="yt-ContentPrimary">
                <div>
                    <asp:ValidationSummary CssClass="yt-Error" ID="valsError" runat="server" ForeColor="Black"
                        HeaderText=" <h2>Oops - there was a problem with your story detail.</h2> <br />" />
                </div>
                <div class="yt-Panel-Primary">
                    <h2>
                        STORY</h2>
                    <!--This is Panel For Personal Detail View mode-->
                    <asp:Panel ID="panPersonalDetailView" runat="server" CssClass="yt-SectionWrapper yt-Story-PersonalDetails">
                        <h3>
                            <asp:Label ID="lblPersonalDetailView" runat="server" Text="Personal Details"></asp:Label></h3>
                        <asp:LinkButton ID="lbtnEditPersonalDetail" CssClass="yt-MiniButton yt-EditButton"
                            runat="server" OnClick="lbtnEditPersonalDetail_Click" CausesValidation="False">Edit</asp:LinkButton>
                        <div class="yt-Tribute-Photo" style="text-align:center;">
                            <asp:Image ID="imgTributeImageView" runat="server" Height="194px" />
                            <span class="yt-PhotoFrame"></span>
                        </div>
                        <dl>
                            <dt>
                                <asp:Label ID="lblName" runat="server" Text="Name:"></asp:Label>
                            </dt>
                            <dd>
                                <asp:Label ID="lblNameView" runat="server"></asp:Label>
                            </dd>
                            <dt>
                                <asp:Label ID="lblDate1" runat="server" Text="Date of Birth:"></asp:Label>
                            </dt>
                            <dd>
                                <asp:Label ID="lblDate1View" runat="server"></asp:Label>
                            </dd>
                        </dl>
                        <dl id="panDate2View" runat="server">
                            <dt>
                                <asp:Label ID="lblDate2" runat="server" Text="Date of Death:"></asp:Label>
                            </dt>
                            <dd>
                                <asp:Label ID="lblDate2View" runat="server"></asp:Label>
                            </dd>
                            <dt>
                                <asp:Label ID="lblAge" runat="server" Text="Age:"></asp:Label>
                            </dt>
                            <dd>
                                <asp:Label ID="lblAgeView" runat="server"></asp:Label>
                            </dd>
                        </dl>
                        <dl>
                            <dt>
                                <asp:Label ID="lblLocation" runat="server" Text="Location:"></asp:Label>
                            </dt>
                            <dd>
                                <asp:Label ID="lblLocationView" runat="server"></asp:Label>
                            </dd>
                        </dl>
                    </asp:Panel>
                    <!--This is Panel For Personal Detail Edit mode-->
                    <asp:Panel ID="panPersonalDetailEdit" runat="server" CssClass="yt-SectionWrapper yt-Story-PersonalDetails">
                        <h3>
                            <asp:Label ID="lblPersonalDetailEdit" runat="server" Text="Personal Details"></asp:Label></h3>
                        <div class="yt-Tribute-Photo">
                            <img id="imgStoryImage" runat="server" src="" alt="" width="194" height="194" />
                            <span class="yt-PhotoFrame"></span><a href="javascript:void(0);" class="yt-MiniButton yt-UploadPhotoButton"
                                onclick="uploadTributePhoto();">Upload an Image</a>
                            <asp:HiddenField ID="hdnStoryImageURL" runat="server" />
                        </div>
                        <fieldset class="yt-Form">
                            <div class="yt-Form-Field">
                                <label id="lblRequiredFields" runat="server">
                                    Required fields are indicated with<em class="required">* </em>
                                </label>
                            </div>
                            <div class="yt-Form-Field">
                                <label for="txtName" id="lblNameEdit" runat="server">
                                    <em class="required">* </em>Name:</label>
                                <asp:TextBox ID="txtName" runat="server" CssClass="yt-Form-Input-Long" MaxLength="40"
                                    TabIndex="2"></asp:TextBox>
                                &nbsp;
                                <asp:CustomValidator ID="valTributeName" runat="server" Font-Bold="True" Font-Size="Medium"
                                    ForeColor="#FF8000" ErrorMessage="Name is Require Field " ClientValidationFunction="RequireTributeName">!</asp:CustomValidator>
                                <asp:CustomValidator ID="valNameExp" runat="server" Font-Bold="True" Font-Size="Medium"
                                    ForeColor="#FF8000" 
                                    ClientValidationFunction="ValidateTributeName" ControlToValidate="txtName">!</asp:CustomValidator>
                                <div class="hint">
                                    <asp:Label ID="lblNameHint" runat="server" ></asp:Label>
                                    <span class="hintPointer"></span>
                                </div>
                            </div>
                            <div id="divDate1Edit" runat="server">
                                <fieldset class="yt-Date-Fields">
                                    <legend id="lblDate1Edit" runat="server"><em class="required">* </em>Date of Birth:</legend>
                                    <div class="yt-Form-Field">
                                        <asp:DropDownList ID="ddlDate1Month" runat="server" CssClass="yt-Form-DropDown" TabIndex="3">
                                        </asp:DropDownList>
                                        <label for="ddlDate1Month" id="lblDate1Month" runat="server">
                                            Month</label>
                                    </div>
                                    <div class="yt-Form-Field">
                                        <asp:DropDownList ID="ddlDate1Day" runat="server" CssClass="yt-Form-DropDown-Short"
                                            TabIndex="4">
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
                                        <label for="ddlDate1Day" id="lblDate1Day" runat="server">
                                            Day</label>
                                    </div>
                                    <div class="yt-Form-Field">
                                        <asp:TextBox ID="txtDate1Year" runat="server" MaxLength="4" CssClass="yt-Form-Input-Short"
                                            TabIndex="5"></asp:TextBox>&nbsp;
                                        <asp:CustomValidator ID="valRequireDate1" runat="server" ClientValidationFunction="Date1Require"
                                            ErrorMessage="Date is Require Field" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000">!</asp:CustomValidator>
                                        <asp:CustomValidator ID="valCheckDate1" runat="server" ClientValidationFunction="checkDate1"
                                            ErrorMessage="Date is not in valid form" Font-Bold="True" Font-Size="Medium"
                                            ForeColor="#FF8000">!</asp:CustomValidator>
                                        <asp:CustomValidator ID="valCheckFutureDate1" runat="server" ClientValidationFunction="checkFutureDate1"
                                            ErrorMessage="future date can't be entered" Font-Bold="True" Font-Size="Medium"
                                            ForeColor="#FF8000">!</asp:CustomValidator>
                                        <label for="txtDate1Year" id="lblDate1Year" runat="server">
                                            Year</label>
                                    </div>
                                </fieldset>
                            </div>
                            <div id="divDate2Edit" runat="server">
                                <fieldset class="yt-Date-Fields">
                                    <legend id="lblDate2Edit" runat="server"><em class="required">* </em>Date of Death:</legend>
                                    <div class="yt-Form-Field">
                                        <asp:DropDownList ID="ddlDate2Month" runat="server" CssClass="yt-Form-DropDown" TabIndex="6">
                                        </asp:DropDownList>
                                        <label for="ddlDate2Month" id="lblDate2Month" runat="server">
                                            Month</label>
                                    </div>
                                    <div class="yt-Form-Field">
                                        <asp:DropDownList ID="ddlDate2Day" runat="server" CssClass="yt-Form-DropDown-Short"
                                            TabIndex="7">
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
                                        <label for="ddlDate2Day" id="lblDate2day" runat="server">
                                            Day</label>
                                    </div>
                                    <div class="yt-Form-Field">
                                        <asp:TextBox ID="txtDate2Year" runat="server" MaxLength="4" CssClass="yt-Form-Input-Short"
                                            TabIndex="8"></asp:TextBox>&nbsp;<asp:CustomValidator ID="valRequireDate2" runat="server"
                                                ClientValidationFunction="Date2Require" ErrorMessage="Date is Require Field"
                                                Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000">!</asp:CustomValidator>
                                        <asp:CustomValidator ID="ValDateCompare" runat="server" ClientValidationFunction="DateCompare"
                                            ErrorMessage="Born Date should be less than the Death date" Font-Bold="True"
                                            Font-Size="Medium" ForeColor="#FF8000">!</asp:CustomValidator>
                                        <asp:CustomValidator ID="valCheckDate2" runat="server" ClientValidationFunction="checkDate2"
                                            ErrorMessage="Date is not in valid form" Font-Bold="True" Font-Size="Medium"
                                            ForeColor="#FF8000">!</asp:CustomValidator>
                                        <asp:CustomValidator ID="valCheckFutureDate2" runat="server" ClientValidationFunction="checkFutureDate2"
                                            ErrorMessage="future date can't be entered" Font-Bold="True" Font-Size="Medium"
                                            ForeColor="#FF8000">!</asp:CustomValidator>
                                        <asp:CustomValidator ID="valCheckNewBaby" runat="server" ClientValidationFunction="checkNewBaby"
                                            ErrorMessage="Please enter either date of birth or Due Date" Font-Bold="True"
                                            Font-Size="Medium" ForeColor="#FF8000">!</asp:CustomValidator>
                                        <asp:CustomValidator ID="valCheckDueDate" runat="server" ClientValidationFunction="checkDueDate"
                                            ErrorMessage="Due Date can't be less than current date" Font-Bold="True" Font-Size="Medium"
                                            ForeColor="#FF8000">!</asp:CustomValidator>
                                        <label for="txtDate2Year" id="lblDate2Year" runat="server">
                                            Year</label>
                                    </div>
                                </fieldset>
                                <% if (TributeType == "Memorial")
                                   { %><h4>
                                       Age:</h4>
                                <%} %>
                                <asp:Label ID="lblAgeEdit" runat="server"></asp:Label>
                            </div>
                            <div class="yt-Form-Field">
                                <label for="txtCity" id="lblCityEdit" runat="server">
                                    City:</label>
                                <asp:TextBox ID="txtCity" runat="server" CssClass="yt-Form-Input-Long" MaxLength="50"
                                    TabIndex="9"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="valCity" runat="server" ControlToValidate="txtCity"
                                    ErrorMessage="Alphanumeric characters are allowed" ValidationExpression="^[A-Za-z0-9\-\#\s]+$"
                                    Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000">!</asp:RegularExpressionValidator>
                            </div>
                            <div class="yt-Form-Field">
                                <label for="ddlState" id="lblStateEdit" runat="server">
                                    <em class="required">* </em>State / Province:</label>
                                <asp:DropDownList ID="ddlState" runat="server" CssClass="yt-Form-DropDown-Long" TabIndex="10">
                                </asp:DropDownList>
                            </div>
                            <div class="yt-Form-Field">
                                <label for="ddlCountry" id="lblCountryEdit" runat="server">
                                    <em class="required">* </em>Country:</label>
                                <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="True" CssClass="yt-Form-DropDown-Long"
                                    OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" TabIndex="11">
                                </asp:DropDownList>
                            </div>
                            <div class="yt-Form-MiniButtons">
                                <asp:LinkButton ID="lbtnCancelPersonalDetail" runat="server" CssClass="yt-MiniButton yt-CancelButton"
                                    OnClick="lbtnCancelPersonalDetail_Click" CausesValidation="False" TabIndex="13">Cancel</asp:LinkButton>
                                <asp:LinkButton ID="lbtnSavePersonalDetail" runat="server" CssClass="yt-MiniButton yt-SaveButton"
                                    OnClick="lbtnSavePersonalDetail_Click" TabIndex="12">Save</asp:LinkButton>
                            </div>
                        </fieldset></asp:Panel>
                    <br />
                    <!--This is Panel For ObituaryNote View-->
                    <asp:Panel ID="panObituaryView" runat="server" style="display:none;" CssClass="yt-SectionWrapper yt-Story-Text">
                        <h3>
                            <asp:Label ID="lblObituaryHeadView" runat="server" Text="Obituary"></asp:Label></h3>
                            <%--  Made by Ashu(15 june,2011) For cuteeditor Safari issue--%>

                        <a id="lbtnEditObituary" runat="server" class="yt-MiniButton yt-EditButton"
                         onclick="javascript:OpenCuteeditor();">Edit</a>
                        <fieldset class="yt-Form">
                            <div class="yt-Form-Field yt-MessageField">
                                <asp:Label ID="lblObituaryDetail" CssClass="yt-Form-Textarea-XLong" runat="server" ><asp:Literal ID="Literal2" runat="server"></asp:Literal></asp:Label>
                                <%--Text="TIP: Use the “Obituary” section to give visitors more detailed information on the person(s) this tribute was created for. Use the “More About” section to include biographical information, dates, places, milestones, history, significant events, and more. Click the EDIT button to begin adding your Obituary."></asp:Label>--%>
                            </div>
                        </fieldset></asp:Panel>
                    <!--This is Panel For Obituary Edit mode-->
                    <asp:Panel ID="panObituaryEdit" runat="server" CssClass="yt-SectionWrapper yt-Story-Text" style="display:none;">
                        <h3>
                            <asp:Label ID="lblObituaryHeadEdit" runat="server" Text="Obituary"></asp:Label></h3>
                        <%--<fieldset class="yt-Form">--%>
                            <div class="yt-Form-Field yt-MessageField">
                                <div class="yt-FreeTextBox">
                                    <%--LHK--%>
                 <%--<FTB:FreeTextBox ID="ftbNoteMessage" runat="server" 
                                        ToolbarLayout="Save,Bold, Italic, Underline,JustifyLeft, JustifyRight, JustifyCenter" 
                                        onsaveclick="lbtnSaveObituary_Click" />--%>
                                    <CE:Editor ID="ftbNoteMessage" runat="server"
                                    TemplateItemList="Save,Bold,Italic,Underline,JustifyLeft,JustifyRight,JustifyCenter,FontName,FontSize"
                                    EnableStripScriptTags="false" ThemeType="Office2007" EditCompleteDocument="true" AllowPasteHtml="true"
                                     Width="630px">

                                    </CE:Editor>  
                                    <span id="spnMessage" style="color: #FF8000; font-size: Medium; font-weight: bold;"
                                        visible="false" runat="server">!</span>
                                </div>
                                &nbsp;
                                <%--<asp:CustomValidator ID="valStoryDetail" runat="server" ClientValidationFunction="RequireStoryDetail"
                                    ErrorMessage="Story is Reuired Field" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000">!</asp:CustomValidator>--%>
                            </div>
                            <div class="yt-Form-MiniButtons">
                                <span id="facebook_share_container" style="display: none">
                                    <input type="checkbox" id="facebook_share" checked="checked" /><label for="facebook_share">Share
                                        on Facebook</label></span>
                                <asp:LinkButton ID="lbtnCancelObituary" runat="server" CssClass="yt-MiniButton yt-CancelButton"
                                     CausesValidation="False" TabIndex="16" OnClick="lbtnCancelObituary_Click">Cancel</asp:LinkButton>
                                <asp:LinkButton ID="lbtnSaveObituary" runat="server" CssClass="yt-MiniButton yt-SaveButton"
                                     TabIndex="15" OnClick="lbtnSaveObituary_Click">Save</asp:LinkButton>
                            </div>
                        <%--</fieldset>--%>
                    </asp:Panel>
                    <br />
                    <!--This is Panel For Story View mode-->
                    <asp:Panel ID="panStoryView" runat="server" CssClass="yt-SectionWrapper yt-Story-Text">
                        <h3>
                            <asp:Label ID="lblStoryHeadView" runat="server" Text="Story"></asp:Label></h3>
                        <asp:LinkButton ID="lbtnEditStory" runat="server" CssClass="yt-MiniButton yt-EditButton"
                            OnClick="lbtnEditStory_Click" CausesValidation="False">Edit</asp:LinkButton>
                        <fieldset class="yt-Form">
                            <div class="yt-Form-Field yt-MessageField">
                                <asp:Label ID="lblStoryDetail" CssClass="yt-Form-Textarea-XLong" runat="server" Text="TIP: Use the “Story” section to give visitors more detailed information on the person(s) this tribute was created for. Use the “More About” section to include biographical information, dates, places, milestones, history, significant events, and more. Click the EDIT button to begin adding your story."></asp:Label>
                            </div>
                        </fieldset></asp:Panel>
                    <!--This is Panel For Story Edit mode-->
                    <asp:Panel ID="panStoryEdit" runat="server" CssClass="yt-SectionWrapper yt-Story-Text">
                        <h3>
                            <asp:Label ID="lblStoryHeadEdit" runat="server" Text="Story"></asp:Label></h3>
                        <fieldset class="yt-Form">
                            <div class="yt-Form-Field yt-MessageField">
                                <asp:TextBox ID="txtStoryDetail" runat="server" CssClass="yt-Form-Textarea-XLong"
                                    Rows="6" Columns="50" TextMode="MultiLine" Text="Click Here to write the story..."
                                    MaxLength="5000" TabIndex="14"></asp:TextBox>
                                &nbsp;
                                <asp:CustomValidator ID="valStoryDetail" runat="server" ClientValidationFunction="RequireStoryDetail"
                                    ErrorMessage="Story is Reuired Field" Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000">!</asp:CustomValidator>
                            </div>
                            <div class="yt-Form-MiniButtons">
                                <span id="facebook_share_container" style="display: none">
                                    <input type="checkbox" id="facebook_share" checked="checked" /><label for="facebook_share">Share
                                        on Facebook</label></span>
                                <asp:LinkButton ID="lbtnCancelStory" runat="server" CssClass="yt-MiniButton yt-CancelButton"
                                    OnClick="lbtnCancelStory_Click" CausesValidation="False" TabIndex="16">Cancel</asp:LinkButton>
                                <asp:LinkButton ID="lbtnSaveStory" runat="server" CssClass="yt-MiniButton yt-SaveButton"
                                    OnClick="lbtnSaveStory_Click" TabIndex="15">Save</asp:LinkButton>
                            </div>
                        </fieldset></asp:Panel>
                    <br />
                    <!--This is Panel For More About View and Edit mode Section-->
                    <asp:Panel ID="panMoreAbout" runat="server" CssClass="yt-SectionWrapper yt-Story-More">
                        <h3>
                            <asp:Label ID="lblMoreAbout" runat="server" Text="More About..."></asp:Label></h3>
                        <fieldset class="yt-Form">
                            <dl>
                                <asp:Repeater ID="repMoreAbout" runat="server" OnItemCommand="repMoreAbout_ItemCommand"
                                    OnItemDataBound="repMoreAbout_ItemDataBound">
                                    <ItemTemplate>
                                        <asp:Panel ID="panMoreAboutViewTopic" runat="server">
                                            <dt id="dtTitle" runat="server">
                                                <asp:Label ID="lblSecondaryTitle" runat="server" Text='<%#Eval("SecondaryTitle")%>'></asp:Label>
                                                <asp:LinkButton ID="lbtnEditTopic" runat="server" CommandName="Edit" CssClass="yt-MiniButton yt-EditButton"
                                                    CausesValidation="False">Edit</asp:LinkButton>
                                            </dt>
                                            <dd id="dtAnswer" runat="server">
                                                <asp:Label ID="lblSectionAnswer" runat="server" Text='<%#Eval("SectionAnswer")%>'></asp:Label></dd>
                                            <asp:Label ID="lblUserBiographyId" runat="server" Text='<%#Eval("UserBiographyId")%>'
                                                Visible="false"></asp:Label>
                                            <asp:Label ID="lblSectionId" runat="server" Text='<%#Eval("SectionId")%>' Visible="false"></asp:Label>
                                        </asp:Panel>
                                        <fieldset class="yt-Form">
                                            <asp:Panel ID="panMoreAboutAddNewTopic" runat="server" Visible="false">
                                                <div class="yt-Form-Field " id="yt-Story-More1">
                                                    <dt>
                                                        <asp:LinkButton ID="lbtnDeleteTopicMoreAbout" runat="server" CssClass="yt-MiniButton yt-DeleteButton"
                                                            CommandName="Delete">Delete</asp:LinkButton>
                                                        <asp:DropDownList ID="ddlTopicListMoreAbout" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTopicListMoreAbout_SelectedIndexChanged"
                                                            TabIndex="18">
                                                            <asp:ListItem Value="0" Text="Select a topic:"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:CustomValidator ID="valSelectTopicAddNew" runat="server" Font-Bold="True" Font-Size="Medium"
                                                            ForeColor="#FF8000" ErrorMessage="Select a topic" ClientValidationFunction="SelectTopicAddNew">!</asp:CustomValidator>
                                                        <asp:TextBox ID="txtOtherTopicMoreAbout" runat="server" Text="Type Your Topic Here"
                                                            CssClass="yt-Form-Input-Long" Visible="false" MaxLength="255" TabIndex="19"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="valOtherTopicMoreAbout" runat="server" ControlToValidate="txtOtherTopicMoreAbout"
                                                            ErrorMessage="Topic is a reuired field" Width="1px" Font-Bold="True" Font-Size="Medium"
                                                            ForeColor="#FF8000">!</asp:RequiredFieldValidator>
                                                    </dt>
                                                    <dd>
                                                        <asp:TextBox ID="txtTopicAnswerMoreAbout" runat="server" CssClass="yt-Form-Textarea-XLong"
                                                            Rows="6" Columns="50" TextMode="MultiLine" MaxLength="5000" TabIndex="20"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="valTopicAnswerMoreAbout" runat="server" ControlToValidate="txtTopicAnswerMoreAbout"
                                                            ErrorMessage="Answer is Reuired Field" Width="1px" Font-Bold="True" Font-Size="Medium"
                                                            ForeColor="#FF8000">!</asp:RequiredFieldValidator>
                                                    </dd>
                                                </div>
                                                <div class="yt-Form-MiniButtons">
                                                    <asp:LinkButton ID="lbtnCancelMoreAbout" runat="server" CssClass="yt-MiniButton yt-CancelButton"
                                                        CommandName="Cancel" CausesValidation="false" TabIndex="22">Cancel</asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnSaveTopicMoreAbout" runat="server" CssClass="yt-MiniButton yt-SaveButton"
                                                        CommandName="Save" TabIndex="21">Save</asp:LinkButton>
                                                </div>
                                            </asp:Panel>
                                        </fieldset>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </dl>
                            <div id="panAddButton" runat="server" class="yt-Form-MiniButtons" visible="false">
                                <asp:LinkButton ID="lbtnAddMoreAbout" runat="server" CssClass="yt-MiniButton yt-AddButton"
                                    OnClick="lbtnAddMoreAbout_Click" TabIndex="17">Add A Topic</asp:LinkButton>
                            </div>
                        </fieldset></asp:Panel>
                    <!--This is Panel For Add New Topic Section-->
                    <div id="panAddNewTopic" runat="server">
                        <fieldset class="yt-Form">
                            <div class="yt-Form-Field " id="yt-Story-More1">
                                <asp:DropDownList ID="ddlTopicList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTopicList_SelectedIndexChanged"
                                    TabIndex="23">
                                    <asp:ListItem Value="0" Text="Select a topic:"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:CustomValidator ID="valSelectTopicAdd" runat="server" ErrorMessage="Select a topic"
                                    Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ClientValidationFunction="SelectTopicAdd">!</asp:CustomValidator>
                                <asp:TextBox ID="txtOtherTopic" runat="server" Text="Type Your Topic Here" CssClass="yt-Form-Input-Long"
                                    Visible="False" MaxLength="255" TabIndex="24"></asp:TextBox>
                                <asp:CustomValidator ID="valAddNewtopic" runat="server" ClientValidationFunction="AddNewTopic"
                                    Font-Bold="True" Font-Size="Medium" ForeColor="#FF8000" ErrorMessage="Topic is a Required Field">!</asp:CustomValidator><br />
                                <br />
                                <asp:TextBox ID="txtTopicAnswer" runat="server" CssClass="yt-Form-Textarea-XLong"
                                    Rows="6" Columns="50" TextMode="MultiLine" MaxLength="5000" TabIndex="25"></asp:TextBox>
                                <asp:CustomValidator ID="valTopicAnswer" runat="server" ClientValidationFunction="RequireTopicAnswer"
                                    ErrorMessage="Topic Answer is Reuired Field" Font-Bold="True" Font-Size="Medium"
                                    ForeColor="#FF8000">!</asp:CustomValidator>
                            </div>
                            <div class="yt-Form-MiniButtons">
                                <asp:LinkButton ID="lbtnCancelTopic" runat="server" CssClass="yt-MiniButton yt-CancelButton"
                                    OnClick="lbtnCancelTopic_Click" CausesValidation="False" TabIndex="27">Cancel</asp:LinkButton>
                                <asp:LinkButton ID="lbtnSaveTopic" runat="server" CssClass="yt-MiniButton yt-SaveButton"
                                    OnClick="lbtnSaveTopic_Click" TabIndex="26">Save</asp:LinkButton>
                            </div>
                        </fieldset>
                    </div>
                </div>
                <% if (this.Master._packageId == 8 || this.Master._packageId == 3)
                   { %>
                <div class="yt-GoogleAdBox-BottomSmall" id="BannerAdBoxBottom" runat="server">
                    <div class="yt-Scissors">
                    </div>
                    <div class="yt-GoogleAdContent">
                        <div>

                            <script type='text/javascript'>
                                <% if (Request.Url.ToString().ToLower().Contains("wedding"))
                                   {%>                               
                                   
                                    GA_googleFillSlot("YourTribute_Wedding_Story_Bottom_468x60");
                               
                                <% } %>
                                <% else if (Request.Url.ToString().ToLower().Contains("anniversary"))
                                    {%>
                                
                                     GA_googleFillSlot("YourTribute_Anniversary_Story_Bottom_468x60");
                               
                                <% } %>
                                <% else if (Request.Url.ToString().ToLower().Contains("graduation"))
                                    {%>
                               
                                     GA_googleFillSlot("YourTribute_Graduation_Story_Bottom_468x60");
                                    
                                <% } %>
                                <% else if (Request.Url.ToString().ToLower().Contains("memorial"))
                                    {%>
                                
                                     GA_googleFillSlot("YourTribute_Memorial_Story_Bottom_468x60");
                                   
                                <% } %>
                                <% else if (Request.Url.ToString().ToLower().Contains("baby"))
                                    {%>
                               
                                     GA_googleFillSlot("YourTribute_Baby_Story_Bottom_468x60");
                                   
                                <% } %>
                                <% else if (Request.Url.ToString().ToLower().Contains("birthday"))
                                    {%>                                
                                     GA_googleFillSlot("YourTribute_Birthday_Story_Bottom_468x60");                                    
                                <% } %>
                            </script>

                        </div>
                        <p class="infoMessage">
                            *Upgrade this <%=ConfigurationManager.AppSettings["ApplicationWordForInternalUse"].ToString().ToLower()%> to remove this advertisement</p>
                    </div>
                </div>
                <% } %>
            </div>
            <!--yt-ContentPrimary-->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
