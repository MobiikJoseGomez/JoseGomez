var COMBO_TILE_NAME = "MainContent_ddlTiles";
var CHANGE_EVENT = "change";
var ON_CHANGE_EVENT = "onchange";
var MESSAGE_EVENT = "message";
var ON_MESSAGE_EVENT = "onmessage";
var HIDDEN_ACCESS_TOKEN = "accessTokenTextbox";
var TILE_CLICKED_EVENT = "tileClicked";
var DASHBOARD_ID_EQUAL = "dashboardId=";
var AMPERSON = "&";
var SLASH_EMBED = "/embed";
var DASHBOARD_ZERO = "/dashboards/{0}";
var ZERO_EMBRACE = "{0}";
var IFRAME_EMBED_TILE = "iFrameEmbedTile";
var PARAM_WIDTH = "&width=";
var PARAM_HEIGHT = "&height=";
var ACTION_LOAD_TILE = "loadTile";
var ASTERISK_MESSAGE = "*";
var EMPTY = "";
var ZERO = 0;
var ONE = 1;
var WIDTH_VALUE = 500;
var HEIGHT_VALUE = 500;

window.onload = function () {

    // client side click to embed a selected tile.
    var el = document.getElementById(COMBO_TILE_NAME);
    if (el) {
        if (el.addEventListener) {
            el.addEventListener(CHANGE_EVENT, updateEmbedTile, false);
        } else {
            el.attachEvent(ON_CHANGE_EVENT, updateEmbedTile);
        }
    }

    //How to navigate from a Power BI Tile to the dashboard
    // listen for message to receive tile click messages.
    if (window.addEventListener) {
        window.addEventListener(MESSAGE_EVENT, receiveMessage, false);
    } else {
        window.attachEvent(ON_MESSAGE_EVENT, receiveMessage);
    }

    //How to handle server side post backs
    // handle server side post backs, optimize for reload scenarios
    // show embedded tile if all fields were filled in.
    var accessTokenElement = document.getElementById(HIDDEN_ACCESS_TOKEN);
    if (null !== accessTokenElement) {
        var accessToken = accessTokenElement.value;
        if ("" !== accessToken) {
            updateEmbedTile();
        }
    }

};

var width = WIDTH_VALUE;
var height = HEIGHT_VALUE;

//How to navigate from a Power BI Tile to the dashboard
// The embedded tile posts message for click to parent window.  
// Listen and handle as appropriate
// The sample shows how to open the tile source.
function receiveMessage(event) {
    if (event.data) {
        try {
            var messageData = JSON.parse(event.data);
            if (messageData.event === TILE_CLICKED_EVENT) {
                //Get IFrame source and construct dashboard url
                var iFrameSrc = document.getElementById(event.srcElement.iframe.id).src;

                //Split IFrame source to get dashboard id
                var dashboardId = iFrameSrc.split(DASHBOARD_ID_EQUAL)[ONE].split(AMPERSON)[ZERO];

                //Get PowerBI service url
                var urlVal = iFrameSrc.split(SLASH_EMBED)[ZERO] + DASHBOARD_ZERO;
                urlVal = urlVal.replace(ZERO_EMBRACE, dashboardId);

                window.open(urlVal);
            }
        } catch (e) {
            
        }
    }
}

// update embed tile
function updateEmbedTile() {
    // check if the embed url was selected
    var comboTile = document.getElementById(COMBO_TILE_NAME);
    if (comboTile) {
        var embedTileUrl = comboTile.value;

        // to load a tile do the following:
        // 1: set the url, include size.
        // 2: add a onload handler to submit the auth token
        var iframe = document.getElementById(IFRAME_EMBED_TILE);
        if (EMPTY === embedTileUrl) {
            iframe.src = EMPTY;
            return;
        } else {
            iframe.src = embedTileUrl + PARAM_WIDTH + width + PARAM_HEIGHT + height;
            iframe.onload = postActionLoadTile;
        }
    }
}

// post the auth token to the iFrame. 
function postActionLoadTile() {
    // get the access token.
    var accessToken = document.getElementById(HIDDEN_ACCESS_TOKEN).value;

    // return if no a
    if (EMPTY === accessToken) {
        return;
    }

    var h = height;
    var w = width;

    // construct the push message structure
    var m = { action: ACTION_LOAD_TILE, accessToken: accessToken, height: h, width: w };
    var message = JSON.stringify(m);

    // push the message.
    var iframe = document.getElementById(IFRAME_EMBED_TILE);
    iframe.contentWindow.postMessage(message, ASTERISK_MESSAGE);
}
