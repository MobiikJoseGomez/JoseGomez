using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Specialized;
using System.Web.Script.Serialization;

namespace PBIWebApp
{
    public partial class Inicio : System.Web.UI.Page
    {
        public AuthenticationResult authResult { get; set; }

        protected void Page_Load(object sender, EventArgs e)
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
                        {Utils.PARAM_CLIENT_ID, Properties.Settings.Default.ClientID},

                        //Resource uri to the Power BI resource to be authorized
                        {Utils.PARAM_RESOURCE, Properties.Settings.Default.PowerBIResource},

                        //After user authenticates, Azure AD will redirect back to the web app
                        {Utils.PARAM_REDIRECT_URI, Properties.Settings.Default.RedirectUrl}
                    };

                //Create sign-in query string
                var queryString = HttpUtility.ParseQueryString(string.Empty);
                queryString.Add(@params);

                //Redirect authority
                //Authority Uri is an Azure resource that takes a client id to get an Access token
                var authorityUri = Properties.Settings.Default.AuthorityUri;
                Response.Redirect(String.Format(Utils.TEMPLATE_REDIRECT, authorityUri, queryString));
                     
            
        }

        
    }
}