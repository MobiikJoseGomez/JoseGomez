using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Azure;
using Newtonsoft.Json;
using PowerBIApplication.Classes;

namespace PowerBIApplication
{
    public partial class ReportTime : System.Web.UI.Page
    {
        #region Variables

        public AuthenticationResult authResult { get; set; }
        public string reportId { get; set; }

        #endregion Variables

        #region Events

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Utils.SESSION_AUTH_RESULT] != null)
            {
                //Get the authentication result from the session
                authResult = (AuthenticationResult)Session[Utils.SESSION_AUTH_RESULT];
            }

            if (!IsPostBack)
            {
                LoadReport();
            }
        }

        #endregion Events

        #region Private Methods

        private void LoadReport()
        {
            var responseContent = string.Empty;

            try
            {
                var request = Utils.GetRequest(String.Format(Utils.TEMPLATE_REPORTS, CloudConfigurationManager.GetSetting(Utils.SETTING_BASE_URI)), authResult.AccessToken);

                //Get reports response from request.GetResponse()
                using (var response = request.GetResponse() as System.Net.HttpWebResponse)
                {
                    //Get reader from response stream
                    using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();

                        //Deserialize JSON string
                        var reports = JsonConvert.DeserializeObject<ReportsTimeFromJson>(responseContent);

                        var reportToShow = (from r in reports.Value
                                            where r.Name.Contains(CloudConfigurationManager.GetSetting(Utils.SETTING_REPORT_TO_SHOW))
                                            select r).FirstOrDefault();

                        if (reportToShow != null)
                        {
                            reportId = reportToShow.EmbedUrl;
                        }
                        else
                        {
                            Session[Utils.SESSION_MESSAGE_ERROR] = Utils.MESSAGE_ERROR_WITHOUT_REPORT;
                            Response.Redirect(Utils.PAGE_ERROR, false);
                        }
                    }
                }
            }
            catch (Exception)
            {
                Session[Utils.SESSION_MESSAGE_ERROR] = Utils.MESSAGE_ERROR_WITHOUT_REPORT;
                Response.Redirect(Utils.PAGE_ERROR, false);
            }
            
        }

        #endregion PrivateMethods
    }
}