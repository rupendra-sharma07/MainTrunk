<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditPersonalDetails.aspx.cs"
    Inherits="ModelPopup.ModelPopup_EditPersonalDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
<head>
    <title>Edit personal Details Popup</title>
    <meta content="text/html; charset=UTF-8" http-equiv="Content-Type" />
    <meta content="en-ca" http-equiv="Content-Language" />
    <meta content="false" http-equiv="imagetoolbar" />
    <meta content="index,follow" name="robots" />
    <meta content="true" name="MSSmartTagsPreventParsing" />
    <!-- really basic, generic html class stylesheet -->
    <link href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/default.css"
        media="screen, projection" type="text/css" rel="stylesheet" />
    <!-- default grid layout stylesheet -->
    <link href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/layouts/centered1024_1.css"
        media="screen, projection" type="text/css" rel="stylesheet" />
    <!-- print-friendly stylesheet -->
    <link href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/print.css"
        media="print" type="text/css" rel="stylesheet" />
    <link href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/screen.css"
        media="screen, projection" type="text/css" rel="stylesheet" />
    <!-- screen elements stylesheet should be here -->
    <link href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/ScreenLatest.css"
        media="screen, projection" type="text/css" rel="stylesheet" />
    <!-- screen elements stylesheet should be here -->
    <!--mootools Library Included-->
    <!--Style for Image cropper -->
    <!--<link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/cropper.css" />    -->
    <!--Style for Image cropper End -->
    <!--Link for Image cropper-->

    <script src="../assets/scripts/cropper/prototype.js" type="text/javascript"></script>

    <script src="../assets/scripts/cropper/scriptaculous.js?load=builder,dragdrop" type="text/javascript"></script>

    <script src="../assets/scripts/cropper/cropper.js" type="text/javascript"></script>

    <script type="text/javascript" src="../Common/JavaScript/Common.js"></script>

    <script type="text/javascript">
/*window.addEvent('load', function() {	
        	buttonStyles();
	        thumbStyles();
	        hrFix();    
	        fieldsetFix();
	    }); */
		function onEndCrop( coords, dimensions ) {
	        $( 'hdnX1' ).value = coords.x1;
	        $( 'hdnY1' ).value = coords.y1;
	        $( 'hdnX2' ).value = coords.x2;
	        $( 'hdnY2' ).value = coords.y2;
	        $( 'hdnWidth' ).value = dimensions.width;
	        $( 'hdnHeight' ).value = dimensions.height;
        }
        
        Event.observe( window, 'load', function() {
            new Cropper.ImgWithPreview(
	            'img',
	            {
		            previewWrap: 'previewWrap',
		            minWidth: 194,
		            minHeight: 194,
		            ratioDim: { x: 194, y: 194 },
		            onEndCrop: onEndCrop
	            }
            );
			errorToParent();
        } );
        
         function errorToParent() {
			childError = document.getElementsByClassName('yt-Error');
			if(childError.length > 0) {
				parentError = window.parent.document.getElementById('mb_Error');
				if($('lblErrMsg')) {
					parentError.className = 'yt-Error';
					parentError.innerHTML = childError[0].innerHTML;
				} else {
					parentError.innerHTML = '&nbsp';
					parentError.className = '';
				}
				window.parent.$('yt-CropperFrame').style.top = '0';// Refresh DOM to fix FF bug
			}
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
function ValidateTributeName(source, args)
    {
        var txtTributeName =  document.getElementById('<%=txtName.ClientID%>'); 
        
        args.IsValid=TributeNameValidate(txtTributeName.value);    
    }

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
        alert(day+" "+month+" "+year);
        if((day !=" ")&&(month !=" ")&&(year.length==4))                
        {
        args.IsValid = true;
        }
        else
        {
        args.IsValid = false;
        }
     alert(args.IsValid);
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
    alert('1');
        var day1 = Number(document.getElementById('<%= ddlDate1Day.ClientID %>').value);
        var month1 = Number(document.getElementById('<%= ddlDate1Month.ClientID %>').value);
        var year1 = document.getElementById('<%= txtDate1Year.ClientID %>').value;
        alert("check1 "+day1+" "+month1+" "+year1);
        if((day1 != null)||(month1 != null)||(year1 != null))
        {
            if((day1 !=" ")&&(month1 !=" ")&&(year1.length==4))                
            {
                if(Number(year1)>1753)
                {
                    var today = new Date( '<%=todayYear%>', '<%=todayMonth%>' - 1, '<%=todayDay%>' );
                    var dayobj = new Date( Number(year), month - 1, day )                            
                    if ((dayobj.getMonth() + 1 == month)&&(dayobj.getDate()== day)&&(dayobj.getFullYear()== today(year)))
                    {           
                        if (dayobj > today)
                        {
                           args.IsValid = false;
                        }
                        else
                        {
                            args.IsValid = true;
                        }
                    }
                }
                else
                {
                    args.IsValid = false;
                }
            }
            else
            {
                if((day1 ==" ")&&(month1 ==" ")&&(year1.length==0))                
                {
                args.IsValid = true;
                }
                else
                {
                args.IsValid = false;
                }
            }
        }
        else
        {
            args.IsValid = true;
        }
        alert(args.IsValid);
    };
    
//    function checkFutureDate2(source, args)
//    {
//        if ('<%=_TributeType%>' == '<%=_Memorial%>')
//        {
//            var today = new Date( '<%=todayYear%>', '<%=todayMonth%>' - 1, '<%=todayDay%>' );
//        
//            args.IsValid = StoryCheckFutureDate( Number(document.getElementById('<%= ddlDate2Day.ClientID %>').value),
//                                                 Number(document.getElementById('<%= ddlDate2Month.ClientID %>').value),
//                                                 document.getElementById('<%= txtDate2Year.ClientID %>').value, today)
//        }
//        else
//        {
//            args.IsValid = true
//        }
//    }
    function DateCompare(source, args)
    {
    alert('inDateCompare');
        var day1 = Number(document.getElementById('<%= ddlDate1Day.ClientID %>').value);
        var month1 = Number(document.getElementById('<%= ddlDate1Month.ClientID %>').value);
        var year1 = document.getElementById('<%= txtDate1Year.ClientID %>').value;
        
        var day2 = Number(document.getElementById('<%= ddlDate2Day.ClientID %>').value);
        var month2 = Number(document.getElementById('<%= ddlDate2Month.ClientID %>').value);
        var year2 = document.getElementById('<%= txtDate2Year.ClientID %>').value;
        
        var today = new Date( '<%=todayYear%>', '<%=todayMonth%>' - 1, '<%=todayDay%>' );
    
        alert(day1+" "+ month1+" "+ year1+" "+ day2+" "+ month2+" "+ year2+" "+ today);
        if((day1 !=" ")&&(month1 !=" ")&&(year1.length==4))   
        {
            alert('a'); 
            args.IsValid = VideoCompareDates(day1, month1, year1, day2, month2, year2, today);
        }
        else
        {
        alert('b');
            if(Number(year2)>1753)
            {
            alert('c');
                var tday = new Date( '<%=todayYear%>', '<%=todayMonth%>' - 1, '<%=todayDay%>' );
                var dayobj = new Date( Number(year), month - 1, day )                            
                if ((dayobj.getMonth() + 1 == month)&&(dayobj.getDate()== day)&&(dayobj.getFullYear()== tday(year)))
                {     
                alert('d');        
                    if (dayobj > today)
                    {
                    alert('e');
                       args.IsValid = false;
                    }
                    else
                    {
                    alert('f');
                        args.IsValid = true;
                    }
                }
            }
            else
            {
                alert('g');
                args.IsValid = false;
            }
        }
        alert(args.IsValid);
    };
    </script>

    <!--Link for Image cropper End -->
    <link href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/ModelPopup.css"
        type="text/css" rel="Stylesheet" />

    <script src="../assets/scripts/mootools-1.11.js" type="text/javascript"></script>

    <script src="../assets/scripts/global.js" type="text/javascript"></script>

    <script src="../Common/JavaScript/Common.js" type="text/javascript"></script>

    <script type="text/javascript" src="../assets/scripts/mootools-1.11.js"></script>

    <script type="text/javascript" src="../assets/scripts/global.js"></script>

    <script type="text/javascript" src="../assets/scripts/tributecreation.js"></script>

    <script type="text/javascript" src="../assets/scripts/modalbox.js"></script>

    <!--#End mootools Library Included -->
    <!--####################################################################-->
    <!--#Stylsheet for Image Cropper By A S Software-->
    <link rel="stylesheet" type="text/css" media="screen, projection" href="../assets/<%= ConfigurationManager.AppSettings["CssDir"].ToString() %>/cropper.css" />
    <!--#End Stylsheet for Image Cropper By A S Software-->
    <!-- #JS libraries For Image Cropper-->

    <script src="../assets/scripts/cropper/prototype.js" type="text/javascript"></script>

    <script src="../assets/scripts/cropper/scriptaculous.js?load=builder,dragdrop" type="text/javascript"></script>

    <script src="../assets/scripts/cropper/cropper.js" type="text/javascript"></script>

    <script type="text/javascript">
        
        function onEndCrop(coords, dimensions) {

            $('hdnX1').value = coords.x1;
            $('hdnY1').value = coords.y1;
            $('hdnX2').value = coords.x2;
            $('hdnY2').value = coords.y2;
            $('hdnWidth').value = dimensions.width;
            $('hdnHeight').value = dimensions.height;

        }

        Event.observe(window, 'load', function() {
            new Cropper.ImgWithPreview(
	                'img',
	                {
	                    previewWrap: 'previewWrap',
	                    minWidth: 195,
	                    minHeight: 195,
	                    ratioDim: { x: 195, y: 195 },
	                    onEndCrop: onEndCrop
	                }
                );
            errorToParent();
        });

        function errorToParent() {
            //debugger;
            childError = document.getElementsByClassName('yt-Error');
            if (childError.length > 0) {
                parentError = window.parent.document.getElementById('mb_Error');
                if ($('lblErrMsg')) {
                    parentError.className = 'yt-Error';
                    parentError.innerHTML = childError[0].innerHTML;
                } else {
                    if (parentError != null) {
                        parentError.innerHTML = '&nbsp';
                        parentError.className = '';
                    }
                }
                if (window.parent.$('yt-CropperFrame') != null) {
                    window.parent.$('yt-CropperFrame').style.top = '0'; // Refresh DOM to fix FF bug
                }
            }
        }

    </script>

    <!--#include file="../analytics.asp"-->

    <script type="text/javascript">
    function popupmodule()
    {

       document.getElementById("ctl00_ModuleContentPlaceHolder_panPersonalDetailView").style.display='none';
       document.getElementById("editprofile").style.display='none';
       document.getElementById("crropper").style.display='block';
       parent.modalClose();
  	   uploadTributePhoto();       
    }   


      function displaycropper()
    {
    
       // alert("display");
        document.getElementById("ctl00_ModuleContentPlaceHolder_panPersonalDetailView").style.display='block';
       document.getElementById("editprofile").style.display='block';
       document.getElementById("crropper").style.display='none'; 
    }
    </script>

    <!--#End Of Image Cropper-->
    <style type="text/css">
        lable
        {
            float: left;
        }
    </style>
</head>
<body>
    <form id="Form1" action="" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
    </div>
    <h3 class="personaldetail-editmodal" style="border-bottom: 1px dotted #D8C6B9;">
        <span id="Span1">Personal Details</span></h3>
    <!--This is Panel For Personal Detail View mode-->
    <div id="ctl00_ModuleContentPlaceHolder_panPersonalDetailView" class="yt-Panel-ModalPopUpPrimary"
        style="float: left; width: 380px;" runat="server">
        <%--<form class="yt-vt-edit-modal-form" runat="server">--%>
        <p>
            Required fields are indicated with*</p>
        <div class="yt-vt-inputblock" style="right: 0; top: 30px">
            <label>
                *Name:</label>
            <asp:TextBox ID="txtName" runat="server" CssClass="yt-Form-Input-Long" MaxLength="280"
                TabIndex="2" Style="width: 290px;"></asp:TextBox>
            <asp:RequiredFieldValidator ID="valTributeName" runat="server" Font-Bold="True" Font-Size="Medium"
                ForeColor="#FF8000" ErrorMessage="Name is Require Field " ControlToValidate="txtName">!</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revTributeName" runat="server" ErrorMessage="Please provide a valid Tribute Name."
                ControlToValidate="txtName" ValidationGroup="TributeDetails" Font-Bold="True"
                Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[a-zA-Z0-9_,\#,\-.\s,]*$"></asp:RegularExpressionValidator>
        </div>
        <div class="yt-vt-inputblock" style="right: 0; top: 20px">
            <div id="divDate1Edit" class="yt-vt-inputblock" runat="server">
                <div class="yt-vt-inputblock">
                    <label>
                        Date of Birth:</label>
                    <asp:DropDownList ID="ddlDate1Month" runat="server" CssClass="yt-Form-DropDown" TabIndex="3">
                    </asp:DropDownList>
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
                    <asp:TextBox ID="txtDate1Year" runat="server" MaxLength="4" CssClass="yt-Form-Input-Short"
                        TabIndex="5"></asp:TextBox>&nbsp;
                    <asp:Label ID="error1" runat="server" Font-Bold="True"
                        Font-Size="Medium" ForeColor="#FF8000" Text="!" Visible="false"></asp:Label>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please provide a valid Tribute Year."
                ControlToValidate="txtDate1Year" ValidationGroup="TributeDetails" Font-Bold="True"
                Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                    <%--<asp:CustomValidator ID="valCheckFutureDate1" runat="server" ClientValidationFunction="checkFutureDate1"
                        ErrorMessage="future date can't be entered" Font-Bold="True" Font-Size="Medium"
                        ForeColor="#FF8000">!</asp:CustomValidator>--%>
                    <div style="clear: both">
                    </div>
                    <p style="width: 120px; float: left; font-size: 9px">
                        Month</p>
                    <p style="width: 50px; float: left; font-size: 9px; margin-left: 5px;">
                        Day</p>
                    <p style="width: 70px; float: left; font-size: 9px; margin-left: 5px;">
                        Year</p>
                    <div style="clear: both">
                    </div>
                </div>
            </div>
        </div>
        <div class="yt-vt-inputblock">
            <div id="divDate2Edit" runat="server">
                <div class="yt-vt-inputblock">
                    <label>
                        *Date of Death:</label>
                    <asp:DropDownList ID="ddlDate2Month" runat="server" CssClass="yt-Form-DropDown" TabIndex="6">
                    </asp:DropDownList>
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
                    <asp:TextBox ID="txtDate2Year" runat="server" MaxLength="4" CssClass="yt-Form-Input-Short"
                        TabIndex="8"></asp:TextBox>&nbsp;
                    <asp:Label ID="error2" runat="server" Font-Bold="True"
                        Font-Size="Medium" ForeColor="#FF8000" Text="!" Visible="false"></asp:Label>
                    <asp:RequiredFieldValidator ID="rfvTributeName" Text="!" runat="server" ControlToValidate="txtDate2Year" Font-Bold="True"
                        Font-Size="Medium" ForeColor="#FF8000" ValidationGroup="TributeDetails"> </asp:RequiredFieldValidator>    
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Please provide a valid Tribute Year."
                        ControlToValidate="txtDate2Year" ValidationGroup="TributeDetails" Font-Bold="True"
                        Font-Size="Medium" ForeColor="#FF8000" Text="!" ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                    <%--<asp:CustomValidator ID="ValDateCompare" runat="server" ClientValidationFunction="DateCompare"
                        ErrorMessage="Born Date should be less than the Death date" Font-Bold="True"
                        Font-Size="Medium" ForeColor="#FF8000">!</asp:CustomValidator>--%>
                    <div style="clear: both">
                    </div>
                    <p style="width: 120px; float: left; font-size: 9px">
                        Month</p>
                    <p style="width: 50px; float: left; font-size: 9px; margin-left: 5px;">
                        Day</p>
                    <p style="width: 70px; float: left; font-size: 9px; margin-left: 5px;">
                        Year</p>
                    <div style="clear: both">
                    </div>
                </div>
            </div>
            <div>
                <label>
                    Age:</label>
                <asp:Label ID="lblAge" Font-Size="10" runat="server"></asp:Label>
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
        </div>
        <div class="yt-Form-MiniButtons">
            <asp:LinkButton ID="lbtnCancelPersonalDetail" runat="server" CssClass="yt-MiniButton yt-CancelButton"
                OnClick="lbtnCancelPersonalDetail_Click" CausesValidation="False" TabIndex="13">Cancel</asp:LinkButton>
            <asp:LinkButton ID="lbtnSavePersonalDetail" runat="server" CssClass="yt-MiniButton yt-SaveButton"
                OnClick="lbtnSavePersonalDetail_Click" CausesValidation="true" TabIndex="12">Save</asp:LinkButton>
        </div>
    </div>
    </form>
    
    <!-- Included for Mobile Redirection functionality -- Detect Browser close and delete NoRedirection keyvalue from database -->
    <script type="text/javascript" src="../assets/scripts/BrowserOrTabCloseHandler.js"></script>

</body>
</html>
