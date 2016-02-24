using Microsoft.Azure;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Web;

namespace PowerBIApplication.Classes
{
    public static class Utils
    {
        #region Constants

        public const string SELECTION_ICON = "icon";
        public const string SELECTION_REPORT = "report";
        
        public const string TEMPLATE_DASHBORD = "{0}dashboards";
        public const string TEMPLATE_TILES = "{0}Dashboards/{1}/Tiles";
        public const string TEMPLATE_REPORTS = "{0}reports";
        public const string TEMPLATE_REDIRECT = "{0}?{1}";

        public const string METRICS_TEXT = "Métricas";
        public const string TIME_TEXT = "Time";
        public const string USE_PER_MONTH_TEXT = "Utilización x Mes";

        public const string SESSION_AUTH_RESULT = "authResult";
        public const string SESSION_MESSAGE_ERROR = "messageError";
        public const string SESSION_FUNCTIONALITY_PAGE = "functionalityPage";

        public const string MESSAGE_ERROR_ACTIVE_DIRECTORY = "Error al autenticarse en Azure Active Directory";
        public const string MESSAGE_ERROR_WITHOUT_PBI = "No tiene acceso a ningún tablero de Power BI";
        public const string MESSAGE_ERROR_WITHOUT_REPORT = "No tiene acceso a ningún reporte de Power BI";


        public const string PAGE_ERROR = "/ErrorAuthenticate.aspx";
        public const string PAGE_METRICS = "/TimeMetrics.aspx";
        public const string PAGE_REPORT_TIME = "/ReportTime.aspx";        

        public const string PARAM_RESPONSE_TYPE = "response_type";
        public const string PARAM_CLIENT_ID = "client_id";
        public const string PARAM_RESOURCE = "resource";
        public const string PARAM_REDIRECT_URI = "redirect_uri";
        public const string CODE_VALUE = "code";

        public const string SETTING_CLIENT_ID = "ClientID";
        public const string SETTING_POWER_BI_RESOURCE = "PowerBIResource";
        public const string SETTING_REDIRECT_URL = "RedirectUrl";
        public const string SETTING_AUTHORITY_URI = "AuthorityUri";
        public const string SETTING_CLIENT_SECRET_KEY = "ClientSecretKey";
        public const string SETTING_BASE_URI = "BaseUri";
        public const string SETTING_DEFAULT_TILE = "DefaultTile";
        public const string SETTING_REPORT_TO_SHOW = "ReportShow";

        public const string TILE_EMBED_URL = "EmbedUrl";
        public const string TILE_TITLE = "Title";

        public const string COMBO_SELECT_VALUE = "Seleccione..";

        public const string GET_METHOD = "GET";
        public const string HEADER_AUTHORIZATION = "Authorization";
        public const string BEARER = "Bearer {0}";

        public const int ZERO = 0;

        #endregion Constants

        #region Methods

        public static NameValueCollection StartRequest()
        {
            //Create a query string
            //Create a sign-in NameValueCollection for query string
            var @params = new NameValueCollection
            {
                //Azure AD will return an authorization code. 
                //See the Redirect class to see how "code" is used to AcquireTokenByAuthorizationCode
                {Utils.PARAM_RESPONSE_TYPE, Utils.CODE_VALUE},

                //Client ID is used by the application to identify themselves to the users that they are requesting permissions from. 
                //You get the client id when you register your Azure app.
                {Utils.PARAM_CLIENT_ID, CloudConfigurationManager.GetSetting(Utils.SETTING_CLIENT_ID)},

                //Resource uri to the Power BI resource to be authorized
                {Utils.PARAM_RESOURCE, CloudConfigurationManager.GetSetting(Utils.SETTING_POWER_BI_RESOURCE)},

                //After user authenticates, Azure AD will redirect back to the web app
                {Utils.PARAM_REDIRECT_URI, CloudConfigurationManager.GetSetting(Utils.SETTING_REDIRECT_URL)}
            };

            //Create sign-in query string
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString.Add(@params);

            return queryString;
        }

        public static WebRequest GetRequest(string requestUriString, string accessToken)
        {
            var request = System.Net.WebRequest.Create(requestUriString) as System.Net.HttpWebRequest;
            request.Method = Utils.GET_METHOD;
            request.ContentLength = Utils.ZERO;
            request.Headers.Add(Utils.HEADER_AUTHORIZATION, String.Format(Utils.BEARER, accessToken));

            return request;
        }

        public static void CreateHeader(HttpResponse response, HttpRequest request)
        {
            
        }

        #endregion Methods
    }
}