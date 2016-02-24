using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PowerBIApplication.Classes;

namespace PowerBIApplication
{
    public partial class ErrorAuthenticate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            spLabel.InnerHtml = Session[Utils.SESSION_MESSAGE_ERROR].ToString();
            Session[Utils.SESSION_MESSAGE_ERROR] = string.Empty;
        }
    }
}