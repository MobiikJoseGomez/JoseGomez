using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using Microsoft.Azure;
using PowerBIApplication.Classes;
using Newtonsoft.Json;

namespace PowerBIApplication
{
    public partial class TimeMetrics : System.Web.UI.Page
    {
        #region Variables

        public AuthenticationResult authResult { get; set; }

        #endregion Variables


        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (Session[Utils.SESSION_AUTH_RESULT] != null)
            {
                //Get the authentication result from the session
                authResult = (AuthenticationResult)Session[Utils.SESSION_AUTH_RESULT];                
            }

            if (!IsPostBack)
            {
                LoadDashbords();
                LoadTiles();
            }
        }

        protected void ddlTiles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #endregion Eventos

        #region Métodos Privados

        private void LoadDashbords()
        {
            var responseContent = string.Empty;
            

            try
            {
                var request = GetRequest(String.Format(Utils.TEMPLATE_DASHBORD, CloudConfigurationManager.GetSetting(Utils.SETTING_BASE_URI)));

                using (var response = request.GetResponse() as System.Net.HttpWebResponse)
                {
                    //Get reader from response stream
                    using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();

                        //Deserialize JSON string
                        var dashboards = JsonConvert.DeserializeObject<DashboardsFromJson>(responseContent);

                        var metricTimeDashboard = (from d in dashboards.Value
                                                   where d.DisplayName.Contains(Utils.METRICS_TEXT) &&
                                                         d.DisplayName.Contains(Utils.TIME_TEXT)
                                                   select d).FirstOrDefault();

                        hfMetricTimeDashboardId.Value = metricTimeDashboard != null
                            ? metricTimeDashboard.Id
                            : string.Empty;

                    }
                }
            }
            catch (Exception ex)
            {
                Session[Utils.SESSION_MESSAGE_ERROR] = Utils.MESSAGE_ERROR_WITHOUT_PBI;
                Response.Redirect(Utils.PAGE_ERROR, false);
            }
            //Get dashboards response from request.GetResponse()
            
        }

        private void LoadTiles()
        {
            var responseContent = string.Empty;
            var dashboardId = hfMetricTimeDashboardId.Value;
            
            try
            {
                var request = GetRequest(String.Format(Utils.TEMPLATE_TILES, CloudConfigurationManager.GetSetting(Utils.SETTING_BASE_URI), dashboardId));
                using (var response = request.GetResponse() as System.Net.HttpWebResponse)
                {
                    //Get reader from response stream
                    using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();

                        //Deserialize JSON string
                        var tiles = JsonConvert.DeserializeObject<TilesFromJson>(responseContent);

                        var usePerMonthTile = (from t in tiles.Value
                                               where t.Title.Contains(Utils.USE_PER_MONTH_TEXT)
                                               select t).FirstOrDefault();


                        ddlTiles.DataValueField = Utils.TILE_EMBED_URL;
                        ddlTiles.DataTextField = Utils.TILE_TITLE;
                        ddlTiles.DataSource = tiles.Value;
                        ddlTiles.DataBind();
                        ddlTiles.Items.Insert(0, new ListItem(Utils.COMBO_SELECT_VALUE, string.Empty));

                        ddlTiles.SelectedValue = usePerMonthTile != null ? usePerMonthTile.EmbedUrl : string.Empty;

                    }
                }
            }
            catch (Exception)
            {

                Session[Utils.SESSION_MESSAGE_ERROR] = Utils.MESSAGE_ERROR_WITHOUT_PBI;
                Response.Redirect(Utils.PAGE_ERROR, false);
            }
            
        }

        private WebRequest GetRequest(string requestUriString)
        {
            var request = System.Net.WebRequest.Create(requestUriString) as System.Net.HttpWebRequest;
            request.Method = Utils.GET_METHOD;
            request.ContentLength = 0;
            request.Headers.Add(Utils.HEADER_AUTHORIZATION, String.Format(Utils.BEARER, authResult.AccessToken));

            return request;
        }

        #endregion Métodos Privados

        #region WebMethods
        #endregion WebMethods


    }
}