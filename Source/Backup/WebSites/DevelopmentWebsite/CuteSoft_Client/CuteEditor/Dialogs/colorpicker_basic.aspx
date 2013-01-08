<%@ Page Language="C#" Inherits="CuteEditor.EditorUtilityPage" %>
<script runat="server">
string GetDialogQueryString;
override protected void OnInit(EventArgs args)
{
	if(Context.Request.QueryString["Dialog"]=="Standard")
	{	
	if(Context.Request.QueryString["IsFrame"]==null)
	{
		string FrameSrc="colorpicker_basic.aspx?IsFrame=1&"+Request.ServerVariables["QUERY_STRING"];
		CuteEditor.CEU.WriteDialogOuterFrame(Context,"[[MoreColors]]",FrameSrc);
		Context.Response.End();
	}
	}
	string s="";
	if(Context.Request.QueryString["Dialog"]=="Standard")	
		s="&Dialog=Standard";
	
	GetDialogQueryString="Theme="+Context.Request.QueryString["Theme"]+s;	
	base.OnInit(args);
}
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
	<head runat="server">
		<meta http-equiv="Page-Enter" content="blendTrans(Duration=0.1)" />
		<meta http-equiv="Page-Exit" content="blendTrans(Duration=0.1)" />
		<script type="text/javascript" src="Load.ashx?type=dialogscript&verfix=1003&file=DialogHead.js"></script>
		<script type="text/javascript" src="Load.ashx?type=dialogscript&verfix=1003&file=Dialog_ColorPicker.js"></script>
		<link href='Load.ashx?type=themecss&file=dialog.css&theme=[[_Theme_]]' type="text/css"
			rel="stylesheet" />
		<style type="text/css">
			.colorcell
			{
				width:16px;
				height:17px;
				cursor:hand;
			}
			.colordiv,.customdiv
			{
				border:solid 1px #808080;
				width:16px;
				height:17px;
				font-size:1px;
			}
			#ajaxdiv{padding:10px;margin:0;text-align:center; background:#eeeeee;}
		</style>
		<title>[[NamedColors]]</title>
		<script>
								
		var OxO9a89=["Green","#008000","Lime","#00FF00","Teal","#008080","Aqua","#00FFFF","Navy","#000080","Blue","#0000FF","Purple","#800080","Fuchsia","#FF00FF","Maroon","#800000","Red","#FF0000","Olive","#808000","Yellow","#FFFF00","White","#FFFFFF","Silver","#C0C0C0","Gray","#808080","Black","#000000","DarkOliveGreen","#556B2F","DarkGreen","#006400","DarkSlateGray","#2F4F4F","SlateGray","#708090","DarkBlue","#00008B","MidnightBlue","#191970","Indigo","#4B0082","DarkMagenta","#8B008B","Brown","#A52A2A","DarkRed","#8B0000","Sienna","#A0522D","SaddleBrown","#8B4513","DarkGoldenrod","#B8860B","Beige","#F5F5DC","HoneyDew","#F0FFF0","DimGray","#696969","OliveDrab","#6B8E23","ForestGreen","#228B22","DarkCyan","#008B8B","LightSlateGray","#778899","MediumBlue","#0000CD","DarkSlateBlue","#483D8B","DarkViolet","#9400D3","MediumVioletRed","#C71585","IndianRed","#CD5C5C","Firebrick","#B22222","Chocolate","#D2691E","Peru","#CD853F","Goldenrod","#DAA520","LightGoldenrodYellow","#FAFAD2","MintCream","#F5FFFA","DarkGray","#A9A9A9","YellowGreen","#9ACD32","SeaGreen","#2E8B57","CadetBlue","#5F9EA0","SteelBlue","#4682B4","RoyalBlue","#4169E1","BlueViolet","#8A2BE2","DarkOrchid","#9932CC","DeepPink","#FF1493","RosyBrown","#BC8F8F","Crimson","#DC143C","DarkOrange","#FF8C00","BurlyWood","#DEB887","DarkKhaki","#BDB76B","LightYellow","#FFFFE0","Azure","#F0FFFF","LightGray","#D3D3D3","LawnGreen","#7CFC00","MediumSeaGreen","#3CB371","LightSeaGreen","#20B2AA","DeepSkyBlue","#00BFFF","DodgerBlue","#1E90FF","SlateBlue","#6A5ACD","MediumOrchid","#BA55D3","PaleVioletRed","#DB7093","Salmon","#FA8072","OrangeRed","#FF4500","SandyBrown","#F4A460","Tan","#D2B48C","Gold","#FFD700","Ivory","#FFFFF0","GhostWhite","#F8F8FF","Gainsboro","#DCDCDC","Chartreuse","#7FFF00","LimeGreen","#32CD32","MediumAquamarine","#66CDAA","DarkTurquoise","#00CED1","CornflowerBlue","#6495ED","MediumSlateBlue","#7B68EE","Orchid","#DA70D6","HotPink","#FF69B4","LightCoral","#F08080","Tomato","#FF6347","Orange","#FFA500","Bisque","#FFE4C4","Khaki","#F0E68C","Cornsilk","#FFF8DC","Linen","#FAF0E6","WhiteSmoke","#F5F5F5","GreenYellow","#ADFF2F","DarkSeaGreen","#8FBC8B","Turquoise","#40E0D0","MediumTurquoise","#48D1CC","SkyBlue","#87CEEB","MediumPurple","#9370DB","Violet","#EE82EE","LightPink","#FFB6C1","DarkSalmon","#E9967A","Coral","#FF7F50","NavajoWhite","#FFDEAD","BlanchedAlmond","#FFEBCD","PaleGoldenrod","#EEE8AA","Oldlace","#FDF5E6","Seashell","#FFF5EE","PaleGreen","#98FB98","SpringGreen","#00FF7F","Aquamarine","#7FFFD4","PowderBlue","#B0E0E6","LightSkyBlue","#87CEFA","LightSteelBlue","#B0C4DE","Plum","#DDA0DD","Pink","#FFC0CB","LightSalmon","#FFA07A","Wheat","#F5DEB3","Moccasin","#FFE4B5","AntiqueWhite","#FAEBD7","LemonChiffon","#FFFACD","FloralWhite","#FFFAF0","Snow","#FFFAFA","AliceBlue","#F0F8FF","LightGreen","#90EE90","MediumSpringGreen","#00FA9A","PaleTurquoise","#AFEEEE","LightCyan","#E0FFFF","LightBlue","#ADD8E6","Lavender","#E6E6FA","Thistle","#D8BFD8","MistyRose","#FFE4E1","Peachpuff","#FFDAB9","PapayaWhip","#FFEFD5"];var colorlist=[{n:OxO9a89[0],h:OxO9a89[1]},{n:OxO9a89[2],h:OxO9a89[3]},{n:OxO9a89[4],h:OxO9a89[5]},{n:OxO9a89[6],h:OxO9a89[7]},{n:OxO9a89[8],h:OxO9a89[9]},{n:OxO9a89[10],h:OxO9a89[11]},{n:OxO9a89[12],h:OxO9a89[13]},{n:OxO9a89[14],h:OxO9a89[15]},{n:OxO9a89[16],h:OxO9a89[17]},{n:OxO9a89[18],h:OxO9a89[19]},{n:OxO9a89[20],h:OxO9a89[21]},{n:OxO9a89[22],h:OxO9a89[23]},{n:OxO9a89[24],h:OxO9a89[25]},{n:OxO9a89[26],h:OxO9a89[27]},{n:OxO9a89[28],h:OxO9a89[29]},{n:OxO9a89[30],h:OxO9a89[31]}];var colormore=[{n:OxO9a89[32],h:OxO9a89[33]},{n:OxO9a89[34],h:OxO9a89[35]},{n:OxO9a89[36],h:OxO9a89[37]},{n:OxO9a89[38],h:OxO9a89[39]},{n:OxO9a89[40],h:OxO9a89[41]},{n:OxO9a89[42],h:OxO9a89[43]},{n:OxO9a89[44],h:OxO9a89[45]},{n:OxO9a89[46],h:OxO9a89[47]},{n:OxO9a89[48],h:OxO9a89[49]},{n:OxO9a89[50],h:OxO9a89[51]},{n:OxO9a89[52],h:OxO9a89[53]},{n:OxO9a89[54],h:OxO9a89[55]},{n:OxO9a89[56],h:OxO9a89[57]},{n:OxO9a89[58],h:OxO9a89[59]},{n:OxO9a89[60],h:OxO9a89[61]},{n:OxO9a89[62],h:OxO9a89[63]},{n:OxO9a89[64],h:OxO9a89[65]},{n:OxO9a89[66],h:OxO9a89[67]},{n:OxO9a89[68],h:OxO9a89[69]},{n:OxO9a89[70],h:OxO9a89[71]},{n:OxO9a89[72],h:OxO9a89[73]},{n:OxO9a89[74],h:OxO9a89[75]},{n:OxO9a89[76],h:OxO9a89[77]},{n:OxO9a89[78],h:OxO9a89[79]},{n:OxO9a89[80],h:OxO9a89[81]},{n:OxO9a89[82],h:OxO9a89[83]},{n:OxO9a89[84],h:OxO9a89[85]},{n:OxO9a89[86],h:OxO9a89[87]},{n:OxO9a89[88],h:OxO9a89[89]},{n:OxO9a89[90],h:OxO9a89[91]},{n:OxO9a89[92],h:OxO9a89[93]},{n:OxO9a89[94],h:OxO9a89[95]},{n:OxO9a89[96],h:OxO9a89[97]},{n:OxO9a89[98],h:OxO9a89[99]},{n:OxO9a89[100],h:OxO9a89[101]},{n:OxO9a89[102],h:OxO9a89[103]},{n:OxO9a89[104],h:OxO9a89[105]},{n:OxO9a89[106],h:OxO9a89[107]},{n:OxO9a89[108],h:OxO9a89[109]},{n:OxO9a89[110],h:OxO9a89[111]},{n:OxO9a89[112],h:OxO9a89[113]},{n:OxO9a89[114],h:OxO9a89[115]},{n:OxO9a89[116],h:OxO9a89[117]},{n:OxO9a89[118],h:OxO9a89[119]},{n:OxO9a89[120],h:OxO9a89[121]},{n:OxO9a89[122],h:OxO9a89[123]},{n:OxO9a89[124],h:OxO9a89[125]},{n:OxO9a89[126],h:OxO9a89[127]},{n:OxO9a89[128],h:OxO9a89[129]},{n:OxO9a89[130],h:OxO9a89[131]},{n:OxO9a89[132],h:OxO9a89[133]},{n:OxO9a89[134],h:OxO9a89[135]},{n:OxO9a89[136],h:OxO9a89[137]},{n:OxO9a89[138],h:OxO9a89[139]},{n:OxO9a89[140],h:OxO9a89[141]},{n:OxO9a89[142],h:OxO9a89[143]},{n:OxO9a89[144],h:OxO9a89[145]},{n:OxO9a89[146],h:OxO9a89[147]},{n:OxO9a89[148],h:OxO9a89[149]},{n:OxO9a89[150],h:OxO9a89[151]},{n:OxO9a89[152],h:OxO9a89[153]},{n:OxO9a89[154],h:OxO9a89[155]},{n:OxO9a89[156],h:OxO9a89[157]},{n:OxO9a89[158],h:OxO9a89[159]},{n:OxO9a89[160],h:OxO9a89[161]},{n:OxO9a89[162],h:OxO9a89[163]},{n:OxO9a89[164],h:OxO9a89[165]},{n:OxO9a89[166],h:OxO9a89[167]},{n:OxO9a89[168],h:OxO9a89[169]},{n:OxO9a89[170],h:OxO9a89[171]},{n:OxO9a89[172],h:OxO9a89[173]},{n:OxO9a89[174],h:OxO9a89[175]},{n:OxO9a89[176],h:OxO9a89[177]},{n:OxO9a89[178],h:OxO9a89[179]},{n:OxO9a89[180],h:OxO9a89[181]},{n:OxO9a89[182],h:OxO9a89[183]},{n:OxO9a89[184],h:OxO9a89[185]},{n:OxO9a89[186],h:OxO9a89[187]},{n:OxO9a89[188],h:OxO9a89[189]},{n:OxO9a89[190],h:OxO9a89[191]},{n:OxO9a89[192],h:OxO9a89[193]},{n:OxO9a89[194],h:OxO9a89[195]},{n:OxO9a89[196],h:OxO9a89[197]},{n:OxO9a89[198],h:OxO9a89[199]},{n:OxO9a89[200],h:OxO9a89[201]},{n:OxO9a89[202],h:OxO9a89[203]},{n:OxO9a89[204],h:OxO9a89[205]},{n:OxO9a89[206],h:OxO9a89[207]},{n:OxO9a89[208],h:OxO9a89[209]},{n:OxO9a89[210],h:OxO9a89[211]},{n:OxO9a89[212],h:OxO9a89[213]},{n:OxO9a89[214],h:OxO9a89[215]},{n:OxO9a89[216],h:OxO9a89[217]},{n:OxO9a89[218],h:OxO9a89[219]},{n:OxO9a89[220],h:OxO9a89[221]},{n:OxO9a89[156],h:OxO9a89[157]},{n:OxO9a89[222],h:OxO9a89[223]},{n:OxO9a89[224],h:OxO9a89[225]},{n:OxO9a89[226],h:OxO9a89[227]},{n:OxO9a89[228],h:OxO9a89[229]},{n:OxO9a89[230],h:OxO9a89[231]},{n:OxO9a89[232],h:OxO9a89[233]},{n:OxO9a89[234],h:OxO9a89[235]},{n:OxO9a89[236],h:OxO9a89[237]},{n:OxO9a89[238],h:OxO9a89[239]},{n:OxO9a89[240],h:OxO9a89[241]},{n:OxO9a89[242],h:OxO9a89[243]},{n:OxO9a89[244],h:OxO9a89[245]},{n:OxO9a89[246],h:OxO9a89[247]},{n:OxO9a89[248],h:OxO9a89[249]},{n:OxO9a89[250],h:OxO9a89[251]},{n:OxO9a89[252],h:OxO9a89[253]},{n:OxO9a89[254],h:OxO9a89[255]},{n:OxO9a89[256],h:OxO9a89[257]},{n:OxO9a89[258],h:OxO9a89[259]},{n:OxO9a89[260],h:OxO9a89[261]},{n:OxO9a89[262],h:OxO9a89[263]},{n:OxO9a89[264],h:OxO9a89[265]},{n:OxO9a89[266],h:OxO9a89[267]},{n:OxO9a89[268],h:OxO9a89[269]},{n:OxO9a89[270],h:OxO9a89[271]},{n:OxO9a89[272],h:OxO9a89[273]}];
		
		</script>
	</head>
	<body>
		<div id="ajaxdiv">
			<div class="tab-pane-control tab-pane" id="tabPane1">
				<div class="tab-row">
					<h2 class="tab">
						<a tabindex="-1" href='colorpicker.aspx?<%=GetDialogQueryString%>'>
							<span style="white-space:nowrap;">
								[[WebPalette]]
							</span>
						</a>
					</h2>
					<h2 class="tab selected">
							<a tabindex="-1" href='colorpicker_basic.aspx?<%=GetDialogQueryString%>'>
								<span style="white-space:nowrap;">
									[[NamedColors]]
								</span>
							</a>
					</h2>
					<h2 class="tab">
							<a tabindex="-1" href='colorpicker_more.aspx?<%=GetDialogQueryString%>'>
								<span style="white-space:nowrap;">
									[[CustomColor]]
								</span>
							</a>
					</h2>
				</div>
				<div class="tab-page">			
					<table class="colortable" align="center">
						<tr>
							<td colspan="16" height="16"><p align="left">Basic:
								</p>
							</td>
						</tr>
						<tr>
							<script>
								var OxOd22b=["length","\x3Ctd class=\x27colorcell\x27\x3E\x3Cdiv class=\x27colordiv\x27 style=\x27background-color:","\x27 title=\x27"," ","\x27 cname=\x27","\x27 cvalue=\x27","\x27\x3E\x3C/div\x3E\x3C/td\x3E",""];var arr=[];for(var i=0;i<colorlist[OxOd22b[0]];i++){arr.push(OxOd22b[1]);arr.push(colorlist[i].n);arr.push(OxOd22b[2]);arr.push(colorlist[i].n);arr.push(OxOd22b[3]);arr.push(colorlist[i].h);arr.push(OxOd22b[4]);arr.push(colorlist[i].n);arr.push(OxOd22b[5]);arr.push(colorlist[i].h);arr.push(OxOd22b[6]);} ;document.write(arr.join(OxOd22b[7]));
							</script>
						</tr>
						<tr>
							<td colspan="16" height="12"><p align="left"></p>
							</td>
						</tr>
						<tr>
							<td colspan="16"><p align="left">Additional:
								</p>
							</td>
						</tr>
						<script>
							var OxO6bcb=["length","\x3Ctr\x3E","\x3Ctd class=\x27colorcell\x27\x3E\x3Cdiv class=\x27colordiv\x27 style=\x27background-color:","\x27 title=\x27"," ","\x27 cname=\x27","\x27 cvalue=\x27","\x27\x3E\x3C/div\x3E\x3C/td\x3E","\x3C/tr\x3E",""];var arr=[];for(var i=0;i<colormore[OxO6bcb[0]];i++){if(i%16==0){arr.push(OxO6bcb[1]);} ;arr.push(OxO6bcb[2]);arr.push(colormore[i].n);arr.push(OxO6bcb[3]);arr.push(colormore[i].n);arr.push(OxO6bcb[4]);arr.push(colormore[i].h);arr.push(OxO6bcb[5]);arr.push(colormore[i].n);arr.push(OxO6bcb[6]);arr.push(colormore[i].h);arr.push(OxO6bcb[7]);if(i%16==15){arr.push(OxO6bcb[8]);} ;} ;if(colormore%16>0){arr.push(OxO6bcb[8]);} ;document.write(arr.join(OxO6bcb[9]));
						</script>
						<tr>
							<td colspan="16" height="8">
							</td>
						</tr>
						<tr>
							<td colspan="16" height="12">
								<input checked id="CheckboxColorNames" style="width: 16px; height: 20px" type="checkbox">
								<span style="width: 118px;">Use color names</span>
							</td>
						</tr>
						<tr>
							<td colspan="16" height="12">
							</td>
						</tr>
						<tr>
							<td colspan="16" valign="middle" height="24">
							<span style="height:24px;width:50px;vertical-align:middle;">Color : </span>&nbsp;
							<input type="text" id="divpreview" size="7" maxlength="7" style="width:180px;height:24px;border:#a0a0a0 1px solid; Padding:4;"/>
					
							</td>
						</tr>
				</table>
			</div>
		</div>
		<div id="container-bottom">
			<input type="button" id="buttonok" value="[[OK]]" class="formbutton" style="width:70px"	onclick="do_insert();" /> 
			&nbsp;&nbsp;&nbsp;&nbsp; 
			<input type="button" id="buttoncancel" value="[[Cancel]]" class="formbutton" style="width:70px"	onclick="do_Close();" />	
		</div>
	</div>
	</body>
</html>

