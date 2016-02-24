var MESSAGE_EVENT = "message";
var ON_MESSAGE_EVENT = "onmessage";
var HIDDEN_ACCESS_TOKEN = "accessTokenTextbox";
var PAGE_LOADED_EVENT_REPORT = "reportPageLoaded";
var IFRAME_EMBED_REPORT = "iFrameEmbedReport";
var HIDDEN_REPORT_ID = "reportId";
var ACTION_LOAD_REPORT = "loadReport";
var EMPTY = "";
var ASTERISK_MESSAGE = "*";

window.onload = function () {
    
    // handle server side post backs, optimize for reload scenarios
    // show embedded report if all fields were filled in.
    var accessTokenElement = document.getElementById(HIDDEN_ACCESS_TOKEN);
    if (null !== accessTokenElement) {
        var accessToken = accessTokenElement.value;
        if (EMPTY !== accessToken) {
            updateEmbedReport();
        }
    }

};


var valB = false;

// update embed report
function updateEmbedReport() {
    
    var hiddenReportId = document.getElementById(HIDDEN_REPORT_ID);
    // to load a report do the following:
    // 1: set the url
    // 2: add a onload handler to submit the auth token
    var iframe = document.getElementById(IFRAME_EMBED_REPORT);
    iframe.src = hiddenReportId.value;
    iframe.onload = postActionLoadReport;    
}

// post the auth token to the iFrame. 
function postActionLoadReport() {
    // get the access token.
    var accessToken = document.getElementById(HIDDEN_ACCESS_TOKEN).value;

    // return if no a
    if (EMPTY === accessToken) {
        return;
    }

    // construct the push message structure
    // this structure also supports setting the reportId, groupId, height, and width.
    // when using a report in a group, you must provide the groupId on the iFrame SRC
    var m = { action: ACTION_LOAD_REPORT, accessToken: accessToken };
    var message = JSON.stringify(m);

    // push the message.
    var iframe = document.getElementById(IFRAME_EMBED_REPORT);
    iframe.contentWindow.postMessage(message, ASTERISK_MESSAGE);
}