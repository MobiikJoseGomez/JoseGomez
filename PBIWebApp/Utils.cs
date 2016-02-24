using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PBIWebApp
{
    public static class Utils
    {
        public const string SELECTION_ICON = "icon";
        public const string SELECTION_REPORT = "report";

        public const string BASE_URI = "https://api.powerbi.com/beta/myorg/";
        public const string TEMPLATE_DASHBORD = "{0}dashboards";
        public const string TEMPLATE_TILES = "{0}Dashboards/{1}/Tiles";
        public const string TEMPLATE_REPORTS = "{0}reports";
        public const string TEMPLATE_REDIRECT = "{0}?{1}";

        public const string PARAM_RESPONSE_TYPE = "response_type";
        public const string PARAM_CLIENT_ID = "client_id";
        public const string PARAM_RESOURCE = "resource";
        public const string PARAM_REDIRECT_URI = "redirect_uri";
        public const string CODE_VALUE = "code";

        public const string SESSION_AUTH_RESULT = "authResult";

        public const string PAGE_ERROR = "/ErrorAuthenticate.aspx";
        public const string PAGE_ELEMENTS = "/Elementos.aspx";

        public const string GET_METHOD = "GET";
        public const string HEADER_AUTHORIZATION = "Authorization";
        public const string BEARER = "Bearer {0}";

        public const int ZERO = 0;

        public const string DASHBOARD_ID_TEXT = "id";
        public const string DASHBOARD_DISPLAY_NAME_TEXT = "displayName";
        public const string EMBED_URL_TEXT = "embedUrl";
        public const string TILE_TITLE_TEXT = "title";
        public const string REPORT_NAME_TEXT = "";

        public const string COMBO_SELECT_VALUE = "Seleccione..";
    }
}