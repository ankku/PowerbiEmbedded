using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using IOC_PBIAdmin;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Data;
using System.IdentityModel.Tokens.Jwt;

namespace PowerBIWebApp.Pages
{
    public  class IndexModel : PageModel
    {
        public string Scopes { get; set; }
        public DataTable TableContent { get; set; }

        [BindProperty]
        public string Query { get; set; }


        private readonly IConfiguration _configuration;
        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resource { get { return _configuration["Authentication:Resource"]; } }

        public ActionResult OnPostSubmit(string data)
        {
            return new JsonResult("Event received");
        }
        public void OnPostData()
        {
            string token = GetToken();

            string baseUri = _configuration["AppSettings:BaseUri"];
            string url = baseUri + Query;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.ContentType = "application/json";
            request.MediaType = "application/json";
            request.Accept = "application/json";

            request.ContentLength = 0;
            request.Method = "GET";
            request.Headers.Add("Authorization", String.Format("Bearer {0}", token));

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string responseContent = (new StreamReader(response.GetResponseStream())).ReadToEnd();

            switch (Query)
            {
                case "groups":
                    PBIGroups groups = JsonConvert.DeserializeObject<PBIGroups>(responseContent);
                    TableContent = ToDataTable(groups.List);
                    break;
                case "reports":
                    PBIReports reports = JsonConvert.DeserializeObject<PBIReports>(responseContent);
                    TableContent = ToDataTable(reports.List);
                    break;
                case "datasets":
                    PBIDataSets datasets = JsonConvert.DeserializeObject<PBIDataSets>(responseContent);
                    TableContent = ToDataTable(datasets.List);
                    break;
                    /*
                                    case "admin/groups":
                                        PBIGroups admingroups = JsonConvert.DeserializeObject<PBIGroups>(responseContent);
                                        TableContent = ListtoDataTable.ToDataTable(admingroups.List);
                                        break;
                    */
            }
        }

        public void OnGet()
        {
            GetToken();

            /*
            string token = GetToken();
            string apiUrl = _configuration["AppSettings:ApiUrl"];

            PowerBIClient pbiClient = new PowerBIClient(new Uri(apiUrl), new Microsoft.Rest.TokenCredentials(token));

            List<AvailableFeature> features = pbiClient.AvailableFeatures.GetAvailableFeatures().Features.ToList();
            List<Gateway> gateways = pbiClient.Gateways.GetGateways().Value.ToList();
            List<Group> groups = pbiClient.Groups.GetGroups().Value.ToList();
            List<Report> reports = pbiClient.Reports.GetReports().Value.ToList();
            List<Dashboard> dashboards = pbiClient.Dashboards.GetDashboards().Value.ToList();
            List<Dataset> datasets = pbiClient.Datasets.GetDatasets().Value.ToList();
            */
        }

        internal string GetToken()
        {
            Task<string> callTask = Task.Run(() => GetTokenasync());
            callTask.Wait();
            var token = callTask.Result;

            Scopes = GetScopes(token);

            return token;
        }

        internal async Task<string> GetTokenasync()
        {
            string accessToken = await HttpContext.GetTokenAsync("access_token");
            return accessToken;
        }

        internal string GetScopes(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken securityToken = (JwtSecurityToken)tokenHandler.ReadToken(token);

            string scopes = null;
            //LM: Evaluation of lambda expression in current VS2017 version has bug:
            //https://developercommunity.visualstudio.com/content/problem/377921/evaluation-of-native-methods-in-this-context-is-no.html
            //after release of fix, the foreach should be replaced with following line
            //scopes = securityToken.Claims.First(x => x.Type.Equals("scp"))

            foreach (var claim in securityToken.Claims)
                if (claim.Type.Equals("scp")) scopes = claim.Value;

            return scopes;
        }

        internal DataTable ToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection props =
            TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < props.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
    }
}
