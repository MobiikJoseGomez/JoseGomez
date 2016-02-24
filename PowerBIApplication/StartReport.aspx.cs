using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PowerBIApplication.Classes;
using Microsoft.Azure;

namespace PowerBIApplication
{
    public partial class StartReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session[Utils.SESSION_FUNCTIONALITY_PAGE] = Utils.PAGE_REPORT_TIME;
            var queryString = Utils.StartRequest();
            Response.Redirect(string.Format(Utils.TEMPLATE_REDIRECT, CloudConfigurationManager.GetSetting(Utils.SETTING_AUTHORITY_URI), queryString));
        }
    }
}