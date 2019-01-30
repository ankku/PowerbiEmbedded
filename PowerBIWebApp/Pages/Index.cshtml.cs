using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using IOC_PBIAdmin;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Data;
using Microsoft.PowerBI.Api.V2;
using Microsoft.PowerBI.Api.V2.Models;
using Newtonsoft.Json;

namespace PowerBIWebApp.Pages
{
    public class IndexModel : PageModel
    {
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
            var token = GetToken();

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
                    TableContent = ListtoDataTable.ToDataTable(groups.List);
                    break;
                case "reports":
                    PBIReports reports= JsonConvert.DeserializeObject<PBIReports>(responseContent);
                    TableContent = ListtoDataTable.ToDataTable(reports.List);
                    break;
                case "datasets":
                    PBIDataSets datasets = JsonConvert.DeserializeObject<PBIDataSets>(responseContent);
                    TableContent = ListtoDataTable.ToDataTable(datasets.List);
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

            return token;
        }

        internal async Task<string> GetTokenasync()
        {
            string accessToken = await HttpContext.GetTokenAsync("access_token");
            return accessToken;
        }

    }

    public static class ListtoDataTable
    {
        public static DataTable ToDataTable<T>(this IList<T> data)
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
