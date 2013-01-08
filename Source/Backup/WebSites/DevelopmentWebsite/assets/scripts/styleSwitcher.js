/*------------------------------------------------------------

styleSwitcher.js

original style sheet switcher script by: Paul Sowden
http://www.alistapart.com/stories/alternate/

Dependencies:
    global.js (cookie functionality)

author: Sing Chan, Mark Bice
last modified: November 26, 2007

------------------------------------------------------------*/




/*
switch style sheets
title - string; title attribute of stylesheet link element.
save - boolean; set a cookie to load stylesheet next time.
*/
function SetActiveStyleSheet(title, save) {
  var i, a, main;
  var s="";
  for (i = 0; (a = document.getElementsByTagName("link")[i]); i++) {
    if (a.getAttribute("rel").indexOf("style") != -1 && a.getAttribute("title")) {
      a.disabled = true;
      if (a.getAttribute("title") == title) {
				a.disabled = false;
			}
      s += a.getAttribute("title") + ": " + a.disabled + "\n";
    }
  }
  if (title != "print_preview" && save) {
		SetCookie("style", title, 365);
		styleTitle = title;
	}
}

function GetActiveStyleSheet() {
  var i, a;
  for(i=0; (a = document.getElementsByTagName("link")[i]); i++) {
    if(a.getAttribute("rel").indexOf("style") != -1 && a.getAttribute("title") && !a.disabled) return a.getAttribute("title");
  }
  return null;
}

function GetPreferredStyleSheet() {
  var i, a;
  for (i = 0; (a = document.getElementsByTagName("link")[i]); i++) {
    if (a.getAttribute("rel").indexOf("style") != -1 && a.getAttribute("rel").indexOf("alt") == -1 && a.getAttribute("title")) {
			return a.getAttribute("title");
		}
  }
  return null;
}



/*
appends stylesheet title to the body element's class name attribute.
*/
/*
function AppendBodyClass() {
	var body = document.getElementsByTagName("body")[0];
	if (body.className && body.className != "") {
		bodyClassName = body.className;
		body.className += " " + styleTitle;
	} else {
		body.className = styleTitle;
	}
}


AddEvent(window, "load", AppendBodyClass, false);
*/

/* must run stylesheet cookie check at parse-time, otherwise IE blows up and crashes */
if (document.getElementsByTagName) {
	var styleCookie = GetCookie("style");
	var styleTitle = styleCookie ? (styleCookie != "null" ? styleCookie : "default") : GetPreferredStyleSheet();
	SetActiveStyleSheet(styleTitle);

	var fontsizeCookie = GetCookie("fontsize");
	var fontsizeTitle = fontsizeCookie ? (fontsizeCookie != "null" ? fontsizeCookie : "default") : "";
	SetFontSize(fontsizeTitle);
}
