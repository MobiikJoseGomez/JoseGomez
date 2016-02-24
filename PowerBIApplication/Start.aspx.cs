using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Azure;
using PowerBIApplication.Classes;

namespace PowerBIApplication
{
    public partial class Start : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.AddHeader("X-Frame-Options", "ALLOW-FROM https://mobiik.sharepoint.com");            
            Session[Utils.SESSION_FUNCTIONALITY_PAGE] = Utils.PAGE_METRICS;
            var queryString = Utils.StartRequest();
            Response.Redirect(string.Format(Utils.TEMPLATE_REDIRECT, CloudConfigurationManager.GetSetting(Utils.SETTING_AUTHORITY_URI), queryString));
        }
    }
}