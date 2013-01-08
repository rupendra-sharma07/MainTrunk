<%@ Page Language="C#" Inherits="CuteEditor.Dialogs.ThumbnailPage" %>

<script runat="server">
override protected void OnInit(EventArgs args)
{
	if(Context.Request.QueryString["Dialog"]=="Standard")
	{
		if(Context.Request.QueryString["IsFrame"]==null)
		{
			string FrameSrc="Thumbnail.aspx?IsFrame=1&"+Request.ServerVariables["QUERY_STRING"];
			CuteEditor.CEU.WriteDialogOuterFrame(Context,"[[AutoThumbnail]]",FrameSrc);
			Context.Response.End();
		}
	}
		base.OnInit(args);
}
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
	<head runat="server">
		<title>[[AutoThumbnail]] </title>		
		<meta http-equiv="Page-Enter" content="blendTrans(Duration=0.1)" />
		<meta http-equiv="Page-Exit" content="blendTrans(Duration=0.1)" />
		<link href='Load.ashx?type=themecss&file=dialog.css&theme=[[_Theme_]]' type="text/css" rel="stylesheet" />
		<script type="text/javascript" src="Load.ashx?type=dialogscript&verfix=1003&file=DialogHead.js"></script>
	</head>
	<body style="margin:0px;border-width:0px;padding:4px;">
		<form runat="server" id="Form1">
			<input type="hidden" runat="server" id="hiddenDirectory" name="hiddenDirectory" /> 
			<input type="hidden" runat="server" id="hiddenFile" name="hiddenFile" />
			<input type="hidden" runat="server" enableviewstate="false" id="hiddenAlert" name="hiddenAlert" /> 
			<input type="hidden" runat="server" enableviewstate="false" id="hiddenAction" name="hiddenAction" />
		<table border="0" cellpadding="4" cellspacing="0" width="100%">
			<tr>
				<td>
					<table border="0" cellpadding="1" cellspacing="4" class="normal">
					<tr>
						<td style="white-space:nowrap" >
							<fieldset style="height:80px;">
								<table border="0" cellpadding="0" cellspacing="0" class="normal">
									<tr>
										<td style="white-space:nowrap; width:60" >[[Width]]:</td>
										<td>
											<input runat="server" id="inp_width" value="80" maxlength="3" onkeyup="checkConstrains('width');"  onkeypress="return CancelEventIfNotDigit()" style="WIDTH : 70px" name="inp_width" />
										</td>
										<td rowspan="2" align="right" valign="middle"><img src="Load.ashx?type=image&file=locked.gif" id="imgLock" width="25" height="32" title="[[ConstrainProportions]]" /></td>
									</tr>
									<tr>
										<td>[[Height]]:</td>
										<td>
											<input runat="server" id="inp_height" value="80" maxlength="3" onkeyup="checkConstrains('height');"  onkeypress="return CancelEventIfNotDigit()" style="WIDTH : 70px" name="inp_height" />
										</td>
									</tr>
									<tr>
										<td colspan="3">
											<input type="checkbox" id="constrain_prop" checked="checked" onclick="javascript:toggleConstrains();" />
											 [[ConstrainProportions]]
										</td>
									</tr>
								</table>
							</fieldset>		
						</td>
						<td style="white-space:nowrap" >
							<div style="width:100px; height:80px; vertical-align:top;overflow:hidden;BACKGROUND-COLOR: #ffffff;BORDER-RIGHT: buttonhighlight 1px solid;BORDER-TOP: buttonshadow 1px solid;BORDER-LEFT: buttonshadow 1px solid;BORDER-BOTTOM: buttonhighlight 1px solid;">
								<img alt="" id="img_demo" src="Load.ashx?type=image&file=1x1.gif" />
							</div>
						</td>
					</tr>	
					<tr>
						<td>
							<div style="margin-top:8px;text-align:center">
								<asp:Button id="okButton" Text="  [[OK]]  " CssClass="formbutton" Runat="server" OnClick="thumbnailButton_Click" />
								&nbsp;&nbsp;
								<input type="button" value="[[Cancel]]" class="formbutton" onclick="top.returnValue=false;(top.close||top.closeeditordialog)()" />
							</div>
						</td>
					</tr>				
				</table>
			</td>
		</tr>
		</table>
	</form>			
	</body>
</html>
	
		<script type="text/javascript">
			var OxObfdd=["src","img_demo","inp_width","inp_height","hiddenFile","hiddenAlert","hiddenDirectory","hiddenAction","onload","value","","IMG","width","height","[[ImagetooSmall]]","dir","imgLock","constrain_prop","checked","Load.ashx?type=image\x26file=locked.gif","Load.ashx?type=image\x26file=1x1.gif","preview_image","DIV","cssText","style","position:absolute","body","offsetWidth","offsetHeight","length"];var obj=Window_GetDialogArguments(window);var src=obj[OxObfdd[0]];var img_demo=document.getElementById(OxObfdd[1]);var inp_width=document.getElementById(OxObfdd[2]);var inp_height=document.getElementById(OxObfdd[3]);var hiddenFile=Window_GetElement(window,OxObfdd[4],true);var hiddenAlert=Window_GetElement(window,OxObfdd[5],true);var hiddenDirectory=Window_GetElement(window,OxObfdd[6],true);var hiddenAction=Window_GetElement(window,OxObfdd[7],true);var defaultwidth=<%= secset.ThumbnailWidth %>;var defaultheight=<%= secset.ThumbnailHeight %>;window[OxObfdd[8]]=reset_hiddens;function reset_hiddens(){if(hiddenAction[OxObfdd[9]]!=OxObfdd[10]){if(hiddenAlert[OxObfdd[9]]){alert(hiddenAlert.value);} ;Window_SetDialogReturnValue(window,hiddenAction.value);Window_CloseDialog(window);} else {hiddenAlert[OxObfdd[9]]=OxObfdd[10];hiddenAction[OxObfdd[9]]=OxObfdd[10];} ;} ;SyncToView();function SyncToView(){if(src){var img=document.createElement(OxObfdd[11]);img[OxObfdd[0]]=src;img_demo[OxObfdd[0]]=src;var p1=parseInt(img[OxObfdd[12]]/defaultwidth);var Ox71=parseInt(img[OxObfdd[13]]/defaultheight);if(p1>Ox71){if(img[OxObfdd[12]]<defaultwidth){alert(OxObfdd[14]);inp_width[OxObfdd[9]]=img[OxObfdd[12]];inp_height[OxObfdd[9]]=img[OxObfdd[13]];} else {inp_width[OxObfdd[9]]=defaultwidth;var Ox72=parseInt(defaultwidth*img[OxObfdd[13]]/img[OxObfdd[12]]);inp_height[OxObfdd[9]]=Ox72;} ;} else {if(img[OxObfdd[13]]<defaultheight){alert(OxObfdd[14]);inp_width[OxObfdd[9]]=img[OxObfdd[12]];inp_height[OxObfdd[9]]=img[OxObfdd[13]];} else {inp_height[OxObfdd[9]]=defaultheight;var Ox73=parseInt(defaultwidth*img[OxObfdd[12]]/img[OxObfdd[13]]);inp_width[OxObfdd[9]]=Ox73;} ;} ;hiddenFile[OxObfdd[9]]=src;hiddenDirectory[OxObfdd[9]]=obj[OxObfdd[15]];do_preview();} ;} ;function toggleConstrains(){var Ox75=document.getElementById(OxObfdd[16]);var Ox76=document.getElementById(OxObfdd[17]);if(Ox76[OxObfdd[18]]){Ox75[OxObfdd[0]]=OxObfdd[19];checkConstrains(OxObfdd[12]);} else {Ox75[OxObfdd[0]]=OxObfdd[20];} ;} ;var checkingConstrains=false;function checkConstrains(Ox79){if(checkingConstrains){return ;} ;checkingConstrains=true;try{var Ox76=document.getElementById(OxObfdd[17]);if(Ox76[OxObfdd[18]]){var Ox7a=document.getElementById(OxObfdd[21]);if(Ox7a){var Ox7b=document.createElement(OxObfdd[22]);Ox7b[OxObfdd[24]][OxObfdd[23]]=OxObfdd[25];document[OxObfdd[26]].appendChild(Ox7b);Ox7b.appendChild(Ox7a);Ox7a.removeAttribute(OxObfdd[12]);Ox7a.removeAttribute(OxObfdd[13]);Ox7a[OxObfdd[24]][OxObfdd[12]]=OxObfdd[10];Ox7a[OxObfdd[24]][OxObfdd[13]]=OxObfdd[10];original_width=Ox7a[OxObfdd[27]];original_height=Ox7a[OxObfdd[28]];Ox7b.removeNode(true);} else {var Ox7c=document.createElement(OxObfdd[11]);Ox7c[OxObfdd[0]]=src;original_width=Ox7c[OxObfdd[12]];original_height=Ox7c[OxObfdd[13]];} ;if((original_width>0)&&(original_height>0)){width=inp_width[OxObfdd[9]];height=inp_height[OxObfdd[9]];if(Ox79==OxObfdd[12]){if(width[OxObfdd[29]]==0||isNaN(width)){inp_width[OxObfdd[9]]=OxObfdd[10];inp_height[OxObfdd[9]]=OxObfdd[10];} else {height=parseInt(width*original_height/original_width);inp_height[OxObfdd[9]]=height;} ;} ;if(Ox79==OxObfdd[13]){if(height[OxObfdd[29]]==0||isNaN(height)){inp_width[OxObfdd[9]]=OxObfdd[10];inp_height[OxObfdd[9]]=OxObfdd[10];} else {width=parseInt(height*original_width/original_height);inp_width[OxObfdd[9]]=width;} ;} ;} ;} ;do_preview();} finally{checkingConstrains=false;} ;} ;function do_preview(){img_demo[OxObfdd[12]]=inp_width[OxObfdd[9]];img_demo[OxObfdd[13]]=inp_height[OxObfdd[9]];} ;	
			
		</script>

