using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Azure;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using PowerBIApplication.Classes;

namespace PowerBIApplication
{
    public partial class Redirect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.AddHeader("X-Frame-Options", "ALLOW-FROM https://mobiik.sharepoint.com");
            try
            {
                //Redirect uri must match the redirect_uri used when requesting Authorization code.
                var redirectUri = CloudConfigurationManager.GetSetting(Utils.SETTING_REDIRECT_URL);
                var authorityUri = CloudConfigurationManager.GetSetting(Utils.SETTING_AUTHORITY_URI);

                // Get the auth code
                var code = Request.Params.GetValues(Utils.ZERO)[Utils.ZERO];

                // Get auth token from auth code       
                var tc = new TokenCache();

                var ac = new AuthenticationContext(authorityUri, tc);

                var cc = new ClientCredential
                (CloudConfigurationManager.GetSetting(Utils.SETTING_CLIENT_ID),
                CloudConfigurationManager.GetSetting(Utils.SETTING_CLIENT_SECRET_KEY));

                var ar = ac.AcquireTokenByAuthorizationCode(code, new Uri(redirectUri), cc);

                //Set Session "authResult" index string to the AuthenticationResult
                Session[Utils.SESSION_AUTH_RESULT] = ar;

                //Redirect back to Default.aspx                
                //Response.Redirect(Utils.PAGE_METRICS,false);
                Response.Redirect(Session[Utils.SESSION_FUNCTIONALITY_PAGE].ToString(), false);
            }
            catch(Exception ex)
            {
                Session[Utils.SESSION_MESSAGE_ERROR] = Utils.MESSAGE_ERROR_ACTIVE_DIRECTORY;
                Response.Redirect(Utils.PAGE_ERROR,false);
            }
        }
    }
}