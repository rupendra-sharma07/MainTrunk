var OxOaa5c=["top","opener","_dialog_arguments","document","dialogArguments","editor","window","element","changed","propertyName","value","checked","trim","prototype",""];function Window_GetDialogTop(Ox1a7){return Ox1a7[OxOaa5c[0]];} ;function Window_FindDialogArguments(Ox1a7){var Ox238=Ox1a7[OxOaa5c[0]];try{var Ox239=Ox238[OxOaa5c[1]];if(Ox239&&Ox239[OxOaa5c[3]][OxOaa5c[2]]){return Ox239[OxOaa5c[3]][OxOaa5c[2]];} ;} catch(x){} ;if(Ox238[OxOaa5c[3]][OxOaa5c[2]]){return Ox238[OxOaa5c[3]][OxOaa5c[2]];} ;if(Ox238[OxOaa5c[4]]){return Ox238[OxOaa5c[4]];} ;return Ox238[OxOaa5c[3]][OxOaa5c[2]];} ;var arg=Window_FindDialogArguments(window);var editor=arg[OxOaa5c[5]];var editwin=arg[OxOaa5c[6]];var editdoc=arg[OxOaa5c[3]];var element=arg[OxOaa5c[7]];var syncingtoview=false;Window_GetDialogTop(window)[OxOaa5c[8]]=Window_GetDialogTop(window)[OxOaa5c[8]]||arg[OxOaa5c[8]];function OnPropertyChange(){if(syncingtoview){return ;} ;var Ox331=Event_GetEvent();if(Ox331[OxOaa5c[9]]!=OxOaa5c[10]&&Ox331[OxOaa5c[9]]!=OxOaa5c[11]){return ;} ;FireUIChanged();} ;function FireUIChanged(){Window_GetDialogTop(window)[OxOaa5c[8]]=true;SyncTo(element);UpdateState();} ;function SyncToViewInternal(){syncingtoview=true;try{SyncToView();UpdateState();} finally{syncingtoview=false;} ;} ;String[OxOaa5c[13]][OxOaa5c[12]]=function (){return this.replace(/(^\s*)|(\s*$)/g,OxOaa5c[14]);} ;function UpdateState(){} ;function SyncTo(element){} ;function SyncToView(){} ;