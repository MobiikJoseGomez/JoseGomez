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
using System.Net;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace PBIWebApp
{
    public partial class Elementos : System.Web.UI.Page
    {
        public AuthenticationResult authResult { get; set; }

        #region Eventos
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session[Utils.SESSION_AUTH_RESULT] != null)
            {
                //Get the authentication result from the session
                authResult = (AuthenticationResult)Session[Utils.SESSION_AUTH_RESULT];

                accessTokenTextbox.Value = authResult.AccessToken;
            }

            if (!IsPostBack)
            {
                LoadDashbords();
                LoadReports();
            }
        }

        protected void rblElementos_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableIconsPanel(rblElementos.SelectedValue == Utils.SELECTION_ICON);
        }

        protected void ddlDasbords_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlDasbords.SelectedIndex > Utils.ZERO)
            {
                LoadTiles(ddlDasbords.SelectedValue);
            }
        }

        #endregion Eventos

        #region Métodos Privados

        private void EnableIconsPanel(bool enable)
        {
            divIcons.Visible = enable;
            divReports.Visible = !enable;
        }
        

        private WebRequest GetRequest(string requestUriString)
        {
            var request = System.Net.WebRequest.Create(requestUriString) as System.Net.HttpWebRequest;
            request.Method = Utils.GET_METHOD;
            request.ContentLength = Utils.ZERO;
            request.Headers.Add(Utils.HEADER_AUTHORIZATION, String.Format(Utils.BEARER, authResult.AccessToken));

            return request;
        }

        private void LoadDashbords()
        {
            var responseContent = string.Empty;
            var request = GetRequest(string.Format(Utils.TEMPLATE_DASHBORD, Utils.BASE_URI));

            //Get dashboards response from request.GetResponse()
            using (var response = request.GetResponse() as System.Net.HttpWebResponse)
            {
                //Get reader from response stream
                using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    responseContent = reader.ReadToEnd();

                    //Deserialize JSON string
                    var pbiDashboards = JsonConvert.DeserializeObject<PBIDashboards>(responseContent);                   

                    
                    ddlDasbords.DataValueField = Utils.DASHBOARD_ID_TEXT;
                    ddlDasbords.DataTextField = Utils.DASHBOARD_DISPLAY_NAME_TEXT;
                    ddlDasbords.DataSource = pbiDashboards.value;
                    ddlDasbords.DataBind();
                    ddlDasbords.Items.Insert(Utils.ZERO,new ListItem(Utils.COMBO_SELECT_VALUE,string.Empty) );
                }
            }
        }

        private void LoadTiles(string dashboardId)
        {
            var responseContent = string.Empty;
            var request = GetRequest(string.Format(Utils.TEMPLATE_TILES, Utils.BASE_URI, dashboardId));

            using (var response = request.GetResponse() as System.Net.HttpWebResponse)
            {
                //Get reader from response stream
                using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    responseContent = reader.ReadToEnd();

                    //Deserialize JSON string
                    var pbiTiles = JsonConvert.DeserializeObject<PBITiles>(responseContent);                    

                    ddlIconos.DataValueField = Utils.EMBED_URL_TEXT;
                    ddlIconos.DataTextField = Utils.TILE_TITLE_TEXT;
                    ddlIconos.DataSource = pbiTiles.value;
                    ddlIconos.DataBind();
                    ddlIconos.Items.Insert(Utils.ZERO, new ListItem(Utils.COMBO_SELECT_VALUE, string.Empty));
                }
            }

        }

        private void LoadReports()
        {
            var responseContent = string.Empty;
            var request = GetRequest(string.Format(Utils.TEMPLATE_REPORTS, Utils.BASE_URI));

            //Get reports response from request.GetResponse()
            using (var response = request.GetResponse() as System.Net.HttpWebResponse)
            {
                //Get reader from response stream
                using (var reader = new System.IO.StreamReader(response.GetResponseStream()))
                {
                    responseContent = reader.ReadToEnd();

                    //Deserialize JSON string
                    var pbiReports = JsonConvert.DeserializeObject<PBIReports>(responseContent);                    

                    ddlReports.DataValueField = Utils.EMBED_URL_TEXT;
                    ddlReports.DataTextField = Utils.REPORT_NAME_TEXT;
                    ddlReports.DataSource = pbiReports.value;
                    ddlReports.DataBind();
                    ddlReports.Items.Insert(Utils.ZERO, new ListItem(Utils.COMBO_SELECT_VALUE, string.Empty));

                }
            }
        }

        #endregion Métodos Privados

    }
}