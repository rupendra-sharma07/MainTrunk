
var isClose = false;

// Immediate function to bind events in Body
// Added by Varun on 29 Jan 2013 to bind events to body of each page
(function () {
if(window.attachEvent) {
    window.attachEvent('onload', AttachEventsToBody());
} else {
    if(window.onload) {    
        var curronload = window.onload;
        var newonload = function() {
            curronload();
            AttachEventsToBody();
        };
        window.onload = newonload;
    } else {
        window.onload = AttachEventsToBody;
    }   
}   
} ())

// Added by Varun on 29 Jan 2013 to bind events to body of each page
function AttachEventsToBody()
{
    var body = document.getElementsByTagName("body")[0];
    bindEvent(body, 'mousedown', function () {
        isClose = true;
    });

    window.onunload = bodyUnload;
}

function bodyUnload() {
    if (!isClose) {
        var request = null;        
        if (window.XMLHttpRequest) {
            //incase of IE7,FF, Opera and Safari browser
            request = new XMLHttpRequest();
        }
        else {
            //for old browser like IE 6.x and IE 5.x
            request = new ActiveXObject('MSXML2.XMLHTTP.3.0');
        }
        request.open("GET", "Miscellaneous/NoRedirectionSessionDeletionHandler.aspx", false);
        request.send();
        return false;
    }
}

//this code will handle the F5 or Ctrl+F5 key
//need to handle more cases like ctrl+R whose codes are not listed here
document.onkeydown = checkKeycode
function checkKeycode(e) {
    var keycode;
    if (window.event)
        keycode = window.event.keyCode;
    else if (e)
        keycode = e.which;
    if (keycode == 116) {
        isClose = true;
    }
}

function GetRequest() {
    var request = null;
    if (window.XMLHttpRequest) {
        //incase of IE7,FF, Opera and Safari browser
        request = new XMLHttpRequest();
    }
    else {
        //for old browser like IE 6.x and IE 5.x
        request = new ActiveXObject('MSXML2.XMLHTTP.3.0');
    }
    return request;
}

function bindEvent(element, type, handler) {   
   if(element.addEventListener) {
      element.addEventListener(type, handler, false);         
   } else {
      element.attachEvent('on'+type, handler);           
   }
}
