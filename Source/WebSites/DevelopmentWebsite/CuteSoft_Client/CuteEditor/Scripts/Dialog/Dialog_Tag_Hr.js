var OxOdf04=["inp_width","eenheid","alignment","hrcolor","hrcolorpreview","shade","sel_size","width","style","value","px","%","size","align","color","backgroundColor","noShade","noshade","","onclick"];var inp_width=Window_GetElement(window,OxOdf04[0],true);var eenheid=Window_GetElement(window,OxOdf04[1],true);var alignment=Window_GetElement(window,OxOdf04[2],true);var hrcolor=Window_GetElement(window,OxOdf04[3],true);var hrcolorpreview=Window_GetElement(window,OxOdf04[4],true);var shade=Window_GetElement(window,OxOdf04[5],true);var sel_size=Window_GetElement(window,OxOdf04[6],true);UpdateState=function UpdateState_Hr(){} ;SyncToView=function SyncToView_Hr(){if(element[OxOdf04[8]][OxOdf04[7]]){if(element[OxOdf04[8]][OxOdf04[7]].search(/%/)<0){eenheid[OxOdf04[9]]=OxOdf04[10];inp_width[OxOdf04[9]]=element[OxOdf04[8]][OxOdf04[7]].split(OxOdf04[10])[0];} else {eenheid[OxOdf04[9]]=OxOdf04[11];inp_width[OxOdf04[9]]=element[OxOdf04[8]][OxOdf04[7]].split(OxOdf04[11])[0];} ;} ;sel_size[OxOdf04[9]]=element[OxOdf04[12]];alignment[OxOdf04[9]]=element[OxOdf04[13]];hrcolor[OxOdf04[9]]=element[OxOdf04[14]];if(element[OxOdf04[14]]){hrcolor[OxOdf04[8]][OxOdf04[15]]=element[OxOdf04[14]];} ;if(element[OxOdf04[16]]){shade[OxOdf04[9]]=OxOdf04[17];} else {shade[OxOdf04[9]]=OxOdf04[18];} ;} ;SyncTo=function SyncTo_Hr(element){if(sel_size[OxOdf04[9]]){element[OxOdf04[12]]=sel_size[OxOdf04[9]];} ;if(hrcolor[OxOdf04[9]]){element[OxOdf04[14]]=hrcolor[OxOdf04[9]];} ;if(alignment[OxOdf04[9]]){element[OxOdf04[13]]=alignment[OxOdf04[9]];} ;if(shade[OxOdf04[9]]==OxOdf04[17]){element[OxOdf04[16]]=true;} else {element[OxOdf04[16]]=false;} ;if(inp_width[OxOdf04[9]]){element[OxOdf04[8]][OxOdf04[7]]=inp_width[OxOdf04[9]]+eenheid[OxOdf04[9]];} ;} ;hrcolor[OxOdf04[19]]=hrcolorpreview[OxOdf04[19]]=function hrcolor_onclick(){SelectColor(hrcolor,hrcolorpreview);} ;